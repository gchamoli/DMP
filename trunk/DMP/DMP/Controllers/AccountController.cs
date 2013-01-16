using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DMP.ModelsView;
using DMP.Repository;
using DMP.Services.Interface;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using DMP.Filters;
using DMP.Models;

namespace DMP.Controllers {
    //[Authorize]
    //[InitializeSimpleMembership]
    public class AccountController : Controller {

        private readonly IFormsAuthenticationService formsService;
        private readonly IMembershipService membershipService;
        private readonly IUserService userService;
        private readonly IMasterService masterService;

        public AccountController(IFormsAuthenticationService formsService, IMembershipService membershipService, IUserService userService, IMasterService masterService) {
            this.formsService = formsService;
            this.membershipService = membershipService;
            this.userService = userService;
            this.masterService = masterService;
        }


        [AllowAnonymous]
        public ActionResult Login(string returnUrl) {
            if (User.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model, string returnUrl) {
            if (ModelState.IsValid && membershipService.ValidateUser(model.UserName, model.Password)) {
                var user = userService.GetUserByUserName(model.UserName);
                if (user != null && user.ObjectInfo.DeletedDate == null) {
                    formsService.SignIn(model.UserName, model.RememberMe);
                    Session.Add("BreadcrumbList", new List<BreadcrumbModel>());
                    return Url.IsLocalUrl(returnUrl) ? (ActionResult)Redirect(returnUrl) : RedirectToAction("Index", "Home");
                }
            }

            // If we got this far, something failed, redisplay form
            ModelState.AddModelError("", "The user name or password provided is incorrect.");
            return View(model);
        }

        //
        // POST: /Account/LogOff


        //[ValidateAntiForgeryToken]
        public ActionResult LogOff() {
            formsService.SignOut();
            Session.Remove("BreadcrumbList");
            return RedirectToAction("Login", "Account");
        }

        public PartialViewResult LogInPartial() {
            return PartialView("_LoginPartial");
        }

        //
        // GET: /Account/Register

        [Authorize(Roles = "HQ,HQR")]
        public ActionResult Users() {
            var users = userService.FindUsers(x => x.Role != "HQ").Select(UserModel.FromDomainModel).ToList();
            return View(users);
        }

        [Authorize(Roles = "HQ")]
        public ActionResult EditUser(int id) {
            var roles = Roles.GetAllRoles().Select(x => new KeyValuePair<string, string>(x, x)).ToList();
            var user = id > 0 ? userService.GetUser(id) : new User();
            var model = new UserViewModel {
                User = UserModel.FromDomainModel(user),
                Roles = roles,
                ParentUsers = id > 0 ? GetUsers(user.Role) : GetUsers(roles.First().Key),
                Regions = masterService.GetAllRegions().Select(x => new KeyValuePair<int, string>(x.Id, x.Name))
            };
            if (id > 0) {
                model.User.Password = "password";
                model.User.ConfirmPassword = "password";
            }
            return PartialView("EditUser", model);
        }

        [HttpPost]
        public void EditUser(UserViewModel model) {
            var flag = false;
            if (model.User.Role == "HQ" || model.User.Role == "HQR") {
                model.User.ParentId = 0;
                flag = true;
            } else {
                if (model.User.ParentId.HasValue && model.User.ParentId.Value > 0) {
                    if (model.User.Role == "RM") {
                        if (model.User.RegionId.HasValue && model.User.RegionId.Value > 0) {
                            flag = true;
                        }
                    } else {
                        flag = true;
                        model.User.RegionId = 0;
                    }
                }
            }

            if (flag) {
                var user = UserModel.ToDomainModel(model.User);
                if (user.Id > 0) {
                    var oldUser = userService.GetUser(user.Id);
                    if (Roles.RoleExists(oldUser.Role) && Roles.IsUserInRole(oldUser.Email, oldUser.Role)) {
                        Roles.RemoveUserFromRole(oldUser.Email, oldUser.Role);
                    }
                    if (Roles.RoleExists(user.Role) && !Roles.IsUserInRole(user.Email, user.Role)) {
                        Roles.AddUserToRole(user.Email, user.Role);
                    }
                    userService.UpdateUser(user);
                } else {
                    var status = membershipService.CreateUser(model.User.Email, model.User.Password);
                    if (Roles.RoleExists(user.Role) && !Roles.IsUserInRole(user.Email, user.Role)) {
                        Roles.AddUserToRole(user.Email, user.Role);
                    }
                    switch (status) {
                        case MembershipCreateStatus.Success:
                            userService.AddUser(new[] { user });
                            break;
                        case MembershipCreateStatus.DuplicateUserName:
                            break;
                    }
                }
                //Add region map for rm
                if (model.User.RegionId.HasValue && model.User.RegionId.Value > 0) {
                    var maps = masterService.FindUserRegionMaps(x => x.UserId == user.Id);
                    if (maps.Any()) {
                        var regionMap = maps.First();
                        regionMap.RegionId = model.User.RegionId.Value;
                        masterService.UpdateUserRegionMap(regionMap);
                    } else {
                        masterService.AddUserRegionMap(new[] { new UserRegionMap { Id = 0, RegionId = model.User.RegionId.Value, UserId = user.Id } });
                    }
                }
            }
        }

        [Authorize(Roles = "HQ")]
        public ActionResult DeleteUser(int id) {
            var user = userService.GetUser(id);
            if (Roles.RoleExists(user.Role) && Roles.IsUserInRole(user.Email, user.Role)) {
                Roles.RemoveUserFromRole(user.Email, user.Role);
            }
            membershipService.DeleteUser(user.Email);
            userService.DeleteUser(id);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public void DeleteMembershipUser(string userName, string role) {
            membershipService.DeleteUser(userName);
            if (Roles.RoleExists(role) && Roles.IsUserInRole(userName, role)) {
                Roles.RemoveUserFromRole(userName, role);
            }
        }

        public List<KeyValuePair<int, string>> GetUsers(string role) {
            var resultRole = string.Empty;
            if (string.IsNullOrEmpty(role)) {
                role = "HQ";
            }
            switch (role.ToLower()) {
                case "csm":
                    resultRole = "RSM";
                    break;
                case "rsm":
                    resultRole = "RM";
                    break;
                case "rm":
                    resultRole = "HQ";
                    break;
                case "hq":
                    resultRole = "";
                    break;
            }
            var roleUsers = userService.FindUsers(x => x.Role == resultRole).Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList();
            return roleUsers;
        }

        public ActionResult GetUsersFromRole(string role) {
            var roleUsers = GetUsers(role);
            return Json(new { success = true, users = roleUsers }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "HQ")]
        public ActionResult ResetPassword(int id) {
            var model = new ResetPasswordModel { Id = id };
            return PartialView("ResetPassword", model);
        }

        [HttpPost]
        [Authorize(Roles = "HQ")]
        public void ResetPassword(ResetPasswordModel model) {
            if (ModelState.IsValid) {
                var userName = userService.GetUser(model.Id).Email;
                var user = membershipService.GetUserByUserName(userName);
                var password = membershipService.ResetPassword(userName);
                user.ChangePassword(password, model.Password);
            }
        }

        [Authorize]
        public ActionResult Credential() {
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult Credential(ChangePasswordModel model) {
            if (ModelState.IsValid) {
                if (membershipService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword)) {
                    ViewBag.Success = true;
                } else {
                    ModelState.AddModelError("", "You are not authorised to change credentials");
                }
            }
            return View(model);
        }

        public PartialViewResult LogOnPartial() {
            var user = userService.GetUserByUserName(User.Identity.Name);
            return PartialView("_LoginPartial", LoggedInUserModel.FromDomainModel(user));
        }

        [AllowAnonymous]
        public ActionResult Register() {
            return View();
        }

        //
        // POST: /Account/Register

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model) {
            if (ModelState.IsValid) {
                // Attempt to register the user
                try {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password);
                    WebSecurity.Login(model.UserName, model.Password);
                    return RedirectToAction("Index", "Home");
                } catch (MembershipCreateUserException e) {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/Disassociate

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Disassociate(string provider, string providerUserId) {
            string ownerAccount = OAuthWebSecurity.GetUserName(provider, providerUserId);
            ManageMessageId? message = null;

            // Only disassociate the account if the currently logged in user is the owner
            if (ownerAccount == User.Identity.Name) {
                // Use a transaction to prevent the user from deleting their last login credential
                using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = IsolationLevel.Serializable })) {
                    bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
                    if (hasLocalAccount || OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name).Count > 1) {
                        OAuthWebSecurity.DeleteAccount(provider, providerUserId);
                        scope.Complete();
                        message = ManageMessageId.RemoveLoginSuccess;
                    }
                }
            }

            return RedirectToAction("Manage", new { Message = message });
        }

        //
        // GET: /Account/Manage

        public ActionResult Manage(ManageMessageId? message) {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : "";
            ViewBag.HasLocalPassword = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Manage(ChangePasswordModel model) {
            bool hasLocalAccount = OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            ViewBag.HasLocalPassword = hasLocalAccount;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasLocalAccount) {
                if (ModelState.IsValid) {
                    // ChangePassword will throw an exception rather than return false in certain failure scenarios.
                    bool changePasswordSucceeded;
                    try {
                        changePasswordSucceeded = WebSecurity.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword);
                    } catch (Exception) {
                        changePasswordSucceeded = false;
                    }

                    if (changePasswordSucceeded) {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    } else {
                        ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                    }
                }
            } else {
                // User does not have a local password so remove any validation errors caused by a missing
                // OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null) {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid) {
                    try {
                        WebSecurity.CreateAccount(User.Identity.Name, model.NewPassword);
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    } catch (Exception e) {
                        ModelState.AddModelError("", e);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl) {
            return new ExternalLoginResult(provider, Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/ExternalLoginCallback

        [AllowAnonymous]
        public ActionResult ExternalLoginCallback(string returnUrl) {
            AuthenticationResult result = OAuthWebSecurity.VerifyAuthentication(Url.Action("ExternalLoginCallback", new { ReturnUrl = returnUrl }));
            if (!result.IsSuccessful) {
                return RedirectToAction("ExternalLoginFailure");
            }

            if (OAuthWebSecurity.Login(result.Provider, result.ProviderUserId, createPersistentCookie: false)) {
                return RedirectToLocal(returnUrl);
            }

            if (User.Identity.IsAuthenticated) {
                // If the current user is logged in add the new account
                OAuthWebSecurity.CreateOrUpdateAccount(result.Provider, result.ProviderUserId, User.Identity.Name);
                return RedirectToLocal(returnUrl);
            } else {
                // User is new, ask for their desired membership name
                string loginData = OAuthWebSecurity.SerializeProviderUserId(result.Provider, result.ProviderUserId);
                ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(result.Provider).DisplayName;
                ViewBag.ReturnUrl = returnUrl;
                return View("ExternalLoginConfirmation", new RegisterExternalLoginModel { UserName = result.UserName, ExternalLoginData = loginData });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLoginConfirmation(RegisterExternalLoginModel model, string returnUrl) {
            string provider = null;
            string providerUserId = null;

            if (User.Identity.IsAuthenticated || !OAuthWebSecurity.TryDeserializeProviderUserId(model.ExternalLoginData, out provider, out providerUserId)) {
                return RedirectToAction("Manage");
            }

            if (ModelState.IsValid) {
                // Insert a new user into the database
                using (UsersContext db = new UsersContext()) {
                    UserProfile user = db.UserProfiles.FirstOrDefault(u => u.UserName.ToLower() == model.UserName.ToLower());
                    // Check if user already exists
                    if (user == null) {
                        // Insert name into the profile table
                        db.UserProfiles.Add(new UserProfile { UserName = model.UserName });
                        db.SaveChanges();

                        OAuthWebSecurity.CreateOrUpdateAccount(provider, providerUserId, model.UserName);
                        OAuthWebSecurity.Login(provider, providerUserId, createPersistentCookie: false);

                        return RedirectToLocal(returnUrl);
                    } else {
                        ModelState.AddModelError("UserName", "User name already exists. Please enter a different user name.");
                    }
                }
            }

            ViewBag.ProviderDisplayName = OAuthWebSecurity.GetOAuthClientData(provider).DisplayName;
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // GET: /Account/ExternalLoginFailure

        [AllowAnonymous]
        public ActionResult ExternalLoginFailure() {
            return View();
        }

        [AllowAnonymous]
        [ChildActionOnly]
        public ActionResult ExternalLoginsList(string returnUrl) {
            ViewBag.ReturnUrl = returnUrl;
            return PartialView("_ExternalLoginsListPartial", OAuthWebSecurity.RegisteredClientData);
        }

        [ChildActionOnly]
        public ActionResult RemoveExternalLogins() {
            ICollection<OAuthAccount> accounts = OAuthWebSecurity.GetAccountsFromUserName(User.Identity.Name);
            List<ExternalLogin> externalLogins = new List<ExternalLogin>();
            foreach (OAuthAccount account in accounts) {
                AuthenticationClientData clientData = OAuthWebSecurity.GetOAuthClientData(account.Provider);

                externalLogins.Add(new ExternalLogin {
                    Provider = account.Provider,
                    ProviderDisplayName = clientData.DisplayName,
                    ProviderUserId = account.ProviderUserId,
                });
            }

            ViewBag.ShowRemoveButton = externalLogins.Count > 1 || OAuthWebSecurity.HasLocalAccount(WebSecurity.GetUserId(User.Identity.Name));
            return PartialView("_RemoveExternalLoginsPartial", externalLogins);
        }

        #region Helpers
        private ActionResult RedirectToLocal(string returnUrl) {
            if (Url.IsLocalUrl(returnUrl)) {
                return Redirect(returnUrl);
            } else {
                return RedirectToAction("Index", "Home");
            }
        }

        public enum ManageMessageId {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
        }

        internal class ExternalLoginResult : ActionResult {
            public ExternalLoginResult(string provider, string returnUrl) {
                Provider = provider;
                ReturnUrl = returnUrl;
            }

            public string Provider { get; private set; }
            public string ReturnUrl { get; private set; }

            public override void ExecuteResult(ControllerContext context) {
                OAuthWebSecurity.RequestAuthentication(Provider, ReturnUrl);
            }
        }

        private static string ErrorCodeToString(MembershipCreateStatus createStatus) {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus) {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }
        #endregion
    }
}
