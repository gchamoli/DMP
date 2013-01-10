using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DMP.Models;
using DMP.ModelsView;
using DMP.Repository;
using DMP.Services;
using DMP.Services.Interface;
using OfficeOpenXml;

namespace DMP.Controllers {
    public class DealerController : Controller {

        private readonly IDealerManpowerService manpowerService;
        private readonly IUserService userService;
        private readonly IProfileService profileService;
        private readonly IMasterService masterService;
        private readonly ICompetencyProfileMapService competencyProfileMapService;
        private readonly ITrainingProfileMapService trainingProfileMapService;
        private readonly IAttritionProfileMapService attritionProfileMapService;
        private readonly IDealerManpowerTargetService manpowerTargetService;
        private readonly IUserDealerMapService userDealerMapServiceService;
        private readonly ITargetService targetService;
        private readonly IIncentiveService incentiveService;
        private readonly IManpowerSalaryService salaryService;

        public DealerController(IDealerManpowerService manpowerService, IUserService userService, IProfileService profileService, IMasterService masterService, ICompetencyProfileMapService competencyProfileMapService, ITrainingProfileMapService trainingProfileMapService, IAttritionProfileMapService attritionProfileMapService, IDealerManpowerTargetService manpowerTargetService, IUserDealerMapService userDealerMapServiceService, ITargetService targetService, IIncentiveService incentiveService, IManpowerSalaryService salaryService) {
            this.manpowerService = manpowerService;
            this.userService = userService;
            this.profileService = profileService;
            this.masterService = masterService;
            this.competencyProfileMapService = competencyProfileMapService;
            this.trainingProfileMapService = trainingProfileMapService;
            this.attritionProfileMapService = attritionProfileMapService;
            this.manpowerTargetService = manpowerTargetService;
            this.userDealerMapServiceService = userDealerMapServiceService;
            this.targetService = targetService;
            this.incentiveService = incentiveService;
            this.salaryService = salaryService;
            if (System.Web.HttpContext.Current.Session["BreadcrumbList"] == null) {
                System.Web.HttpContext.Current.Session["BreadcrumbList"] = new List<BreadcrumbModel>();
            }
        }

        #region ManpowerTarget

        [Authorize(Roles = "HQ")]
        public ActionResult ManpowerTargets() {
            Session["BreadcrumbList"] = Utils.HtmlExtensions.SetBreadcrumbs((List<BreadcrumbModel>)Session["BreadcrumbList"], "/Dealer/ManpowerTargets", "Manpower");
            var currentDate = DateTime.Now.Date;
            var month = masterService.FindAndCreateMonth(currentDate.ToString("MMMM"), currentDate.Year);
            var products = masterService.GetAllProducts().OrderBy(x => x.Id);
            var months = masterService.FindMonths(x => x.ObjectInfo.DeletedDate == null && x.Year <= month.Year);
            var monthList = (from mnth in months let tempdate = new DateTime(mnth.Year, masterService.GetMonthIndex(mnth.Name), 1) where tempdate <= currentDate select new KeyValuePair<int, string>(mnth.Id, string.Format("{0}-{1}", mnth.Name, mnth.Year))).ToList();
            var model = new ManpowerTargetViewModel {
                MonthId = month.Id,
                MonthName = string.Format("{0} - {1}", month.Name, month.Year),
                TargetPlans = new List<ManpowerTargetPlanModel>(),
                Products = products.Select(x => new KeyValuePair<int, string>(x.Id, x.Name)),
                Months = monthList,
                Users = userService.FindUsers(x => x.Role == "CSM" && x.ObjectInfo.DeletedDate == null).Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList()
            };
            ViewBag.List = Session["BreadcrumbList"];
            return View(model);
        }

        [Authorize(Roles = "HQ")]
        public ActionResult SaveMnpowerTargets(ManpowerTargetViewModel model) {
            var csmId = model.CsmId;
            var monthId = model.MonthId;
            foreach (var target in model.TargetPlans) {
                var dealerId = target.DealerId;
                foreach (var plan in target.Targets) {
                    var manpowerTarget = ManpowerTargetPlanModel.ToDomainModel(plan);
                    manpowerTarget.UserId = csmId;
                    manpowerTarget.DealerId = dealerId;
                    manpowerTarget.MonthId = monthId;
                    if (manpowerTarget.Id > 0) {
                        manpowerTargetService.UpdateDealerManpowerTarget(manpowerTarget);
                    } else {
                        manpowerTargetService.AddDealerManpowerTarget(new[] { manpowerTarget });
                    }
                }
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "HQ")]
        public ActionResult GetManpowerTargets(int monthId, int csmId) {
            var month = masterService.GetMonth(monthId);
            var products = masterService.GetAllProducts().OrderBy(x => x.Name);
            var dealers = userDealerMapServiceService.FindUserDealerMaps(x => x.UserId == csmId).Select(x => x.Dealer);
            var data = manpowerTargetService.FindDealerManpowerTargets(x => x.UserId == csmId && x.MonthId == month.Id).ToList();
            var list = new List<ManpowerTargetPlanModel>();
            foreach (var dealer in dealers) {
                var tempModel = new ManpowerTargetPlanModel {
                    DealerId = dealer.Id,
                    Dealer = dealer.Name
                };
                var targetList = new List<ManpowerProductTargetModel>();
                foreach (var product in products) {
                    var all = data.Where(x => x.DealerId == dealer.Id && x.ProductId == product.Id);
                    var total = all.Sum(x => x.Planned);
                    targetList.Add(new ManpowerProductTargetModel {
                        ProductId = product.Id,
                        Planned = total,
                        Id = all.Any() ? all.First().Id : 0
                    });
                }
                tempModel.Targets = targetList;
                list.Add(tempModel);
            }
            var mnthName = string.Format("{0} - {1}", month.Name, month.Year);
            return Json(new { success = true, targetPlan = list, monthName = mnthName }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ManpowerProfile

        public ActionResult ManpowerProfile(int id, int dealerId) {
            Session["BreadcrumbList"] = Utils.HtmlExtensions.SetBreadcrumbs((List<BreadcrumbModel>)Session["BreadcrumbList"], string.Format("/Dealer/ManpowerProfile/{0}", id), "Profile");
            var csm = userService.GetUserByUserName(User.Identity.Name);
            var model = new CsmDealerViewModel {
                Manpower = id > 0 ? DealerManpowerModel.FromDomainModel(manpowerService.GetDealerManpower(id)) : new DealerManpowerModel(),
                Designations = Enumeration.GetAll<Designation>(),
                TrainingLevels = Enumeration.GetAll<TrainingLevel>(),
                Attrition = new AttritionProfileModel(),
                Training = new TrainingProfileModel(),
                Attritions = masterService.GetAllAttritions().Select(x => new KeyValuePair<int, string>(x.Id, x.Name)),
                AllTrainings = masterService.GetAllTrainings().Select(x => new KeyValuePair<int, string>(x.Id, x.Name)),
                Products = masterService.GetAllProducts().Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList()
            };
            model.Manpower.DealerId = dealerId;
            model.Manpower.UserId = csm.Id;
            if (id > 0) {
                var competencies = competencyProfileMapService.FindCompetencyProfileMaps(x => x.DealerManpowerId == id);
                model.Manpower.Competency = competencies != null && competencies.Any()
                                              ? competencies.Average(x => x.Score)
                                              : 0;
                var trainings = trainingProfileMapService.FindTrainingProfileMaps(x => x.DealerManpowerId == id).GroupBy(
                        x => x.Training);
                var list = (from training in trainings
                            let maxDate = training.Max(x => x.LastTrainingDate)
                            let date = DateTime.Now - maxDate
                            select new TrainingProfileModel {
                                Id = 0, TrainingId = training.Key.Id, Training = training.Key.Name, TrainingDate = maxDate, NumberOfDays = date.HasValue ? date.Value.Days : 0
                            }).ToList();
                model.Trainings = list;
                var attritionProfileMap = attritionProfileMapService.FindAttritionProfileMaps(x => x.DealerManpower.Id == id);
                if (attritionProfileMap != null && attritionProfileMap.Any()) {
                    model.Attrition = AttritionProfileModel.FromDomainModel(attritionProfileMap.First());
                }
            }
            ViewBag.List = Session["BreadcrumbList"];
            return View(model);
        }

        [HttpPost]
        public ActionResult SaveManpower(CsmDealerViewModel model) {

            var profile = DealerManpowerModel.ToProfileDomain(model.Manpower);
            var manpower = DealerManpowerModel.ToManpowerDomain(model.Manpower);
            profile.DealerManpower = manpower;

            if (profile.Id > 0) {
                profileService.UpdateProfile(profile);
            } else {
                profileService.AddProfile(new[] { profile });
            }

            //Add/Update Competency
            var competencies = masterService.GetAllCompetencies();
            var competencyList = competencies.Select(comp => new CompetencyProfileMap {
                Id = 0, DealerManpowerId = manpower.Id, CompetencyId = comp.Id, Score = 0
            }).ToList();
            competencyProfileMapService.AddCompetencyProfileMap(competencyList);

            //Add Salary
            if (model.Manpower.Salary > 0) {
                salaryService.AddManpowerSalary(new[]{new ManpowerSalary
                                                          {
                                                              Id=0,
                                                              Salary = model.Manpower.Salary,
                                                              DealerManpowerId = manpower.Id
                                                          }});
            }
            //manpower = manpowerService.GetDealerManpower(manpower.Id);
            //var manpowerModel = DealerManpowerModel.FromDomainModel(manpower);
            //return Json(new { success = true, manpower = manpowerModel }, JsonRequestBehavior.AllowGet);
            return RedirectToAction("DseProfile", "Dealer", new { id = manpower.Id });
        }

        public ActionResult DeleteManpower(int id) {
            manpowerService.DeleteDealerManpower(id);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult Competency(int id) {
            var model = new CompetencyProfileViewModel {
                Competencies = competencyProfileMapService.FindCompetencyProfileMaps(x => x.DealerManpowerId == id).Select(x => new CompetencyProfileModel { Id = x.Id, Competency = x.Competency.Name, ComptencyId = x.CompetencyId, Score = x.Score }),
                ProfileId = id
            };
            return PartialView("CompetencyPartial", model);
        }

        public ActionResult DseProfile(int id) {
            var trainings = trainingProfileMapService.FindTrainingProfileMaps(x => x.DealerManpowerId == id).GroupBy(
                        x => x.Training);
            var model = new ProfileViewModel {
                Manpower = DealerManpowerModel.FromDomainModel(manpowerService.GetDealerManpower(id)),
                Trainings = (from training in trainings
                             let maxDate = training.Max(x => x.LastTrainingDate)
                             let date = DateTime.Now - maxDate
                             select new TrainingProfileModel {
                                 Id = 0, TrainingId = training.Key.Id, Training = training.Key.Name, TrainingDate = maxDate, NumberOfTrainings = training.Count(), NumberOfDays = date.HasValue ? date.Value.Days : 0
                             }).ToList(),
                Months = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }
            };
            return View("Profile", model);
        }

        [HttpPost]
        public void Competency(CompetencyProfileViewModel model) {
            if (!model.Competencies.Any()) {
                return;
            }
            var competencies = model.Competencies.Select(CompetencyProfileModel.ToDomainModel);
            foreach (var comp in competencies) {
                comp.DealerManpowerId = model.ProfileId;
                if (comp.Id > 0) {
                    competencyProfileMapService.UpdateCompetencyProfileMap(comp);
                } else {
                    competencyProfileMapService.AddCompetencyProfileMap(new[] { comp });
                }
            }
        }

        public PartialViewResult Attrition(int id) {
            var manpower = manpowerService.GetDealerManpower(id);
            var model = new AttritionProfileViewModel {
                Attritions = masterService.GetAllAttritions().Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList(),
                ProfileId = id,
                Attrition = manpower.AttritionProfileMap != null ? AttritionProfileModel.FromDomainModel(manpower.AttritionProfileMap) : new AttritionProfileModel()
            };
            return PartialView("AttritionPartial", model);
        }

        [HttpPost]
        public void Attrition(AttritionProfileViewModel model) {
            var attrition = AttritionProfileModel.ToDomainModel(model.Attrition);
            if (attrition.AttritionId > 0 && attrition.DateOfLeaving.HasValue && attrition.DateOfLeaving.Value.Ticks > 0) {
                attrition.DealerManpower = manpowerService.GetDealerManpower(model.ProfileId);
                if (attrition.Id > 0) {
                    attritionProfileMapService.UpdateAttritionProfileMap(attrition);
                } else {
                    attritionProfileMapService.AddAttritionProfileMap(new[] { attrition });
                }
            }
        }

        public PartialViewResult Training(int id) {
            var model = new TrainingProfileViewModel {
                Trainings = masterService.GetAllTrainings().Select(x => new KeyValuePair<int, string>(x.Id, x.Name)),
                ProfileId = id,
                Training = new TrainingProfileModel()
            };
            return PartialView("TrainingPartial", model);
        }

        [HttpPost]
        public void Training(TrainingProfileViewModel model) {
            var training = TrainingProfileModel.ToDomainModel(model.Training);
            if (training.TrainingId <= 0 || !training.LastTrainingDate.HasValue || training.LastTrainingDate.Value.Ticks <= 0) {
                return;
            }
            training.DealerManpowerId = model.ProfileId;
            trainingProfileMapService.AddTrainingProfileMap(new[] { training });
        }

        #endregion

        public ActionResult ManageDse(int csmId, int dealerId) {
            Session["BreadcrumbList"] = Utils.HtmlExtensions.SetBreadcrumbs((List<BreadcrumbModel>)Session["BreadcrumbList"], string.Format("/Dealer/ManageDse?csmId={0}&dealerId={1}", csmId, dealerId), "Manage DSE");
            var model = new ManageDseViewModel() {
                Manpowers = manpowerService.FindDealerManpowers(x => x.UserId == csmId && x.DealerId == dealerId).Select(DealerManpowerModel.FromDomainModel)
            };
            ViewBag.List = Session["BreadcrumbList"];
            return View(model);
        }

        public ActionResult Dealer(int dealerId, int csmId) {
            Session["BreadcrumbList"] = Utils.HtmlExtensions.SetBreadcrumbs((List<BreadcrumbModel>)Session["BreadcrumbList"], string.Format("/Dealer/Dealer?dealerId={0}&csmId={1}", dealerId, csmId), "Dealer");
            var currentDate = DateTime.Now;
            var currentMonth = masterService.FindAndCreateMonth(currentDate.ToString("MMMM"), currentDate.Year);
            var manpowerUsers = manpowerService.FindDealerManpowers(x => x.DealerId == dealerId && x.UserId == csmId);
            var products = masterService.FindProducts(x => x.ObjectInfo.DeletedDate == null).OrderBy(x => x.Id);
            var trainingLevels = Enumeration.GetAll<TrainingLevel>();
            var reportList = new List<ReportManpowerModel>();

            var targets = manpowerTargetService.FindDealerManpowerTargets(x => x.DealerId == dealerId && x.UserId == csmId);
            var targetList = new List<TotalManpower>();
            foreach (var product in products) {
                targetList.Add(new TotalManpower {
                    Plan = targets.Where(x => x.ProductId == product.Id).Sum(x => x.Planned),
                    Actual = manpowerUsers.Count(x => x.ProductId == product.Id)
                });
            }
            var trainingLevelList = new List<ManpowerTrainingLevel>();
            foreach (var level in trainingLevels) {
                trainingLevelList.Add(new ManpowerTrainingLevel {
                    Level = level.Value,
                    LevelCount = manpowerUsers.Count(x => x.Profile.TrainingLevel == level.Value)
                });
            }
            reportList.Add(new ReportManpowerModel {
                Manpowers = targetList,
                TrainingLevels = trainingLevelList
            });
            var productStatList = new List<ProductStatModel>();
            foreach (var product in products) {
                var productVarientIds = masterService.FindProductVarient(x => x.ProductId == product.Id).Select(x => x.Id);
                var manpowers = manpowerService.FindDealerManpowers(x => x.UserId == csmId && x.DealerId == dealerId && x.ProductId == product.Id);
                var exitMnapowers = manpowers.Count(x => x.Profile.DateOfLeaving != null);
                var averageEmployee = masterService.FindAverageEmployee(currentDate.AddMonths(-1));
                var manpowerIds = manpowers.Select(x => x.Id);
                var dealerTargets =
                    targetService.FindTargets(
                        x => x.MonthId == currentMonth.Id && productVarientIds.Contains(x.ProductVarientId) && manpowerIds.Contains(x.DealerManpowerId));
                productStatList.Add(new ProductStatModel {
                    Product = product.Name,
                    Competency = manpowers.Any() ? Math.Round(manpowers.Average(x => x.CompetencyProfileMaps.Average(y => y.Score)), 2) : 0,
                    Productivity = dealerTargets.Any() ? Math.Round(dealerTargets.Average(x => x.Actual), 2) : 0,
                    Attrition = exitMnapowers / (averageEmployee > 0 ? averageEmployee : 1)
                });
            }
            var model = new CsmUserViewModel {
                ReportManpower = new ReportManpowerViewModel {
                    ReportManpowers = reportList,
                    Products = products.Select(x => x.Name),
                    TrainingLevels = trainingLevels.Select(x => x.Value)
                },
                ProductStatistics = productStatList
            };
            ViewBag.DealerId = dealerId;
            ViewBag.CsmId = csmId;
            ViewBag.List = Session["BreadcrumbList"];
            ViewBag.UserName = masterService.GetDealer(dealerId).Name;
            return View(model);
        }

        public ActionResult Targets(int id) {
            Session["BreadcrumbList"] = Utils.HtmlExtensions.SetBreadcrumbs((List<BreadcrumbModel>)Session["BreadcrumbList"], string.Format("/Dealer/Targets/{0}", id), "Target");
            var date = DateTime.Now;
            var month = masterService.FindAndCreateMonth(date.ToString("MMMM"), date.Year);
            date = DateTime.Now.AddMonths(-1);
            var previousMonth = masterService.FindAndCreateMonth(date.ToString("MMMM"), date.Year);
            var csm = userService.GetUserByUserName(User.Identity.Name);
            var manpowers = manpowerService.FindDealerManpowers(x => x.UserId == csm.Id && x.DealerId == id).OrderBy(y => y.Name).ToList();
            var manpowerIds = manpowers.Select(x => x.Id);
            var allVarients = masterService.GetAllProductVarients();
            var products = masterService.FindProducts(x => x.ProductVarients.Count > 0).OrderBy(x => x.Id);
            var data = targetService.FindTargets(x => manpowerIds.Contains(x.DealerManpowerId) && x.MonthId == month.Id);
            var previousTargets =
                targetService.FindTargets(x => manpowerIds.Contains(x.DealerManpowerId) && x.MonthId == previousMonth.Id);
            var targetList = new List<TargetModel>();

            foreach (var manpower in manpowers) {
                var targetModel = new TargetModel();
                targetModel.ManpowerId = manpower.Id;
                targetModel.Manpower = manpower.Name;
                targetModel.MonthId = month.Id;
                var targetPlanList = new List<TargetPlanModel>();
                var manpowerProductVarientIds = allVarients.Where(x => x.ProductId == manpower.ProductId).Select(x => x.Id);
                foreach (var varient in allVarients) {
                    var tempData = data.Where(x => x.ProductVarientId == varient.Id && x.DealerManpowerId == manpower.Id);
                    var preTempData =
                        previousTargets.Where(x => x.ProductVarientId == varient.Id && x.DealerManpowerId == manpower.Id);
                    targetPlanList.Add(new TargetPlanModel {
                        Id = tempData.Any() ? tempData.First().Id : 0,
                        Target1 = tempData.Any() ? tempData.Sum(x => x.Target1) : 0,
                        Target2 = tempData.Any() ? tempData.Sum(x => x.Target2) : 0,
                        Actual = preTempData.Any() ? preTempData.Sum(x => x.Actual) : 0,
                        ProductVarientId = varient.Id,
                        preTarget1 = preTempData.Any() ? preTempData.Sum(x => x.Target1) : 0,
                        preTarget2 = preTempData.Any() ? preTempData.Sum(x => x.Target2) : 0,
                        IsEditable = manpowerProductVarientIds.Contains(varient.Id)
                    });
                }
                targetModel.Targets = targetPlanList;
                targetList.Add(targetModel);
            }
            var model = new TargetViewModel {
                CsmId = csm.Id,
                MonthId = month.Id,
                DealerId = id,
                Targets = targetList,
            };
            model.ProductVarients =
                products.Select(
                    x =>
                    new ProductVarients { Product = x.Name, Varients = x.ProductVarients.OrderBy(y => y.Name).Select(y => y.Name).ToList() }).ToList();

            ViewBag.Month = string.Format("{0} - {1}", month.Name, month.Year);
            ViewBag.List = Session["BreadcrumbList"];
            return View(model);
        }

        public ActionResult SaveTargets(TargetViewModel model) {
            foreach (var targetModel in model.Targets) {
                var manpowerId = targetModel.ManpowerId;
                var monthId = targetModel.MonthId;
                foreach (var plan in targetModel.Targets) {
                    var targetPlan = TargetModel.ToDomainModel(plan);
                    targetPlan.MonthId = monthId;
                    targetPlan.DealerManpowerId = manpowerId;
                    if (targetPlan.Id > 0) {
                        targetService.UpdateTarget(targetPlan);
                    } else {
                        targetService.AddTarget(new[] { targetPlan });
                    }
                }
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        #region FileUpload

        public ActionResult UploadFile() {
            var model = new FileUploadModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult UploadFile(FileUploadModel model) {
            string tempPath = null;
            var submittedFile = Request.Files["File"];

            if (submittedFile != null && submittedFile.ContentLength > 0) {
                AppUtil.GetTempPath(submittedFile.FileName, out tempPath);
                submittedFile.SaveAs(tempPath);
            }

            if (tempPath == null) {
                return View(model);
            }

            var existingFile = new FileInfo(tempPath);
            var extension = existingFile.Extension;
            var headerData = new List<string>();

            switch (extension) {
                case ".xlsx": {
                        using (var package = new ExcelPackage(existingFile)) {
                            var workSheet = package.Workbook.Worksheets[1];
                            if (workSheet != null && workSheet.Dimension != null && workSheet.Dimension.End.Row > 1) {
                                for (var col = 1; col <= workSheet.Dimension.End.Column; col++) {
                                    if (workSheet.Cells[1, col].Value != null)
                                        headerData.Add(workSheet.Cells[1, col].Value.ToString());
                                }
                            }
                        }
                    }
                    break;
            }
            AppUtil.DeleteDirectory(new FileInfo(tempPath).DirectoryName);
            return View(model);
        }

        #endregion

        public ActionResult Test() {
            return Json(new { success = true, data = "gaurav" }, JsonRequestBehavior.AllowGet);
        }

    }
}
