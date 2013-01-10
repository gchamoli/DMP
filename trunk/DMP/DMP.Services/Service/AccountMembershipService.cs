using System;
using System.Web.Security;
using DMP.Services.Interface;

namespace DMP.Services.Service {
    public class AccountMembershipService : IMembershipService {

        private readonly MembershipProvider provider;

        public AccountMembershipService()
            : this(null) {

        }

        public AccountMembershipService(MembershipProvider provider) {
            this.provider = provider ?? Membership.Provider;
        }

        public int MinPasswordLength { get { return provider.MinRequiredPasswordLength; } }

        public bool ValidateUser(string userName, string password) {
            if (String.IsNullOrEmpty(userName))
                throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(password))
                throw new ArgumentException("Value cannot be null or empty.", "password");

            var res = provider.ValidateUser(userName, password);
            return res;
        }

        public MembershipCreateStatus CreateUser(string email, string password) {
            if (String.IsNullOrEmpty(email))
                throw new ArgumentException("Value cannot be null or empty.", "email");
            if (String.IsNullOrEmpty(password))
                throw new ArgumentException("Value cannot be null or empty.", "password");

            MembershipCreateStatus status;
            provider.CreateUser(email, password, email, null, null, true, null, out status);
            return status;
        }

        public void UpdateUser(MembershipUser user) {
            throw new NotImplementedException();
        }

        public bool ChangePassword(string userName, string oldPassword, string newPassword) {
            if (String.IsNullOrEmpty(userName))
                throw new ArgumentException("Value cannot be null or empty.", "userName");
            if (String.IsNullOrEmpty(oldPassword))
                throw new ArgumentException("Value cannot be null or empty.", "oldPassword");
            if (String.IsNullOrEmpty(newPassword))
                throw new ArgumentException("Value cannot be null or empty.", "newPassword");

            // The underlying ChangePassword() will throw an exception rather
            // than return false in certain failure scenarios.
            try {
                MembershipUser currentUser = provider.GetUser(userName, true /* userIsOnline */);
                if (currentUser != null) {
                    var result = currentUser.ChangePassword(oldPassword, newPassword);
                    return result;
                }
                return false;
            } catch (ArgumentException) {
                return false;
            } catch (MembershipPasswordException) {
                return false;
            }
        }

        public MembershipUser GetUserByUserName(string userName) {
            return Membership.GetUser(userName);
        }

        public bool IsInRole(string userName, string role) {
            return Roles.IsUserInRole(userName, role);
        }

        public bool DeleteUser(string userName) {
            return provider.DeleteUser(userName, true);
        }

        public string ResetPassword(string userName) {
            var user = provider.GetUser(userName, false);
            return user.ResetPassword();
        }
    }
}
