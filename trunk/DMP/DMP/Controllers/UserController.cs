using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DMP.Models;
using DMP.ModelsView;
using DMP.Services;
using DMP.Services.Interface;

namespace DMP.Controllers {
    public class UserController : Controller {
        private readonly IUserService userService;
        private readonly IUserDealerMapService userDealerMapService;
        private readonly IDealerManpowerTargetService manpowerTargetService;
        private readonly IDealerManpowerService manpowerService;
        private readonly IMasterService masterService;
        private readonly ITargetService targetService;

        public UserController(IUserService userService, IUserDealerMapService userDealerMapService, IDealerManpowerTargetService manpowerTargetService, IDealerManpowerService manpowerService, IMasterService masterService, ITargetService targetService) {
            this.userService = userService;
            this.userDealerMapService = userDealerMapService;
            this.manpowerTargetService = manpowerTargetService;
            this.manpowerService = manpowerService;
            this.masterService = masterService;
            this.targetService = targetService;
            if (System.Web.HttpContext.Current.Session["BreadcrumbList"] == null) {
                System.Web.HttpContext.Current.Session["BreadcrumbList"] = new List<BreadcrumbModel>();
            }
        }

        public ActionResult HqUser(int id) {
            if (User.IsInRole("HQ")) {
                Session["BreadcrumbList"] = Utils.HtmlExtensions.SetBreadcrumbs((List<BreadcrumbModel>)Session["BreadcrumbList"], string.Format("/User/HqUser/{0}", id), "Home");
            }
            var currentDate = DateTime.Now;
            var currentMonth = masterService.FindAndCreateMonth(currentDate.ToString("MMMM"), currentDate.Year);
            var rmUsers = userService.FindUsers(x => x.ParentId == id).ToList();
            var products = masterService.FindProducts(x => x.ObjectInfo.DeletedDate == null).OrderBy(x => x.Id);
            var trainingLevels = Enumeration.GetAll<TrainingLevel>();
            var reportList = new List<ReportManpowerModel>();

            foreach (var rm in rmUsers) {
                var rsmIds = userService.FindUsers(x => x.ParentId == rm.Id).Select(x => x.Id);
                var csmIds = userService.FindUsers(x => rsmIds.Contains(x.ParentId)).Select(x => x.Id);
                var dealerIds = userDealerMapService.FindUserDealerMaps(x => csmIds.Contains(x.UserId)).Select(x => x.DealerId);
                var targets = manpowerTargetService.FindDealerManpowerTargets(x => dealerIds.Contains(x.DealerId) && csmIds.Contains(x.UserId) && x.MonthId == currentMonth.Id);
                var manpowers = manpowerService.FindDealerManpowers(x => csmIds.Contains(x.UserId) && dealerIds.Contains(x.DealerId)).ToList();
                var targetList = new List<TotalManpower>();
                foreach (var product in products) {
                    targetList.Add(new TotalManpower {
                        Plan = targets.Where(x => x.ProductId == product.Id).Sum(x => x.Planned),
                        Actual = manpowers.Count(x => x.ProductId == product.Id)
                    });
                }
                var trainingLevelList = new List<ManpowerTrainingLevel>();
                foreach (var level in trainingLevels) {
                    trainingLevelList.Add(new ManpowerTrainingLevel {
                        Level = level.Value,
                        LevelCount = manpowers.Count(x => x.Profile.TrainingLevel == level.Value)
                    });
                }
                reportList.Add(new ReportManpowerModel {
                    User = rm.Name,
                    UserUrl = string.Format("/User/RmUser/{0}", rm.Id),
                    Manpowers = targetList,
                    TrainingLevels = trainingLevelList
                });
            }
            var productStatList = new List<ProductStatModel>();
            foreach (var product in products) {
                var productVarientIds = masterService.FindProductVarient(x => x.ProductId == product.Id).Select(x => x.Id);
                var rmIds = rmUsers.Select(x => x.Id);
                var rsmIds = userService.FindUsers(x => rmIds.Contains(x.ParentId)).Select(x => x.Id);
                var csmIds = userService.FindUsers(x => rsmIds.Contains(x.ParentId)).Select(x => x.Id);
                var manpowers = manpowerService.FindDealerManpowers(x => csmIds.Contains(x.UserId) && x.ProductId == product.Id);
                var exitMnapowers = manpowers.Count(x => x.Profile.DateOfLeaving != null);
                var averageEmployee = masterService.FindAverageEmployee(currentDate.AddMonths(-1));
                var manpowerIds = manpowers.Select(x => x.Id);
                var targets =
                    targetService.FindTargets(
                        x => x.MonthId == currentMonth.Id && productVarientIds.Contains(x.ProductVarientId) && manpowerIds.Contains(x.DealerManpowerId));
                productStatList.Add(new ProductStatModel {
                    Product = product.Name,
                    Competency = manpowers.Any() ? Math.Round(manpowers.Average(x => x.CompetencyProfileMaps.Average(y => y.Score)), 2) : 0,
                    Productivity = targets.Any() ? Math.Round(targets.Average(x => x.Actual), 2) : 0,
                    Attrition = exitMnapowers / (averageEmployee > 0 ? averageEmployee : 1)
                });
            }
            var model = new HqUserViewModel {
                ReportManpower = new ReportManpowerViewModel {
                    ReportManpowers = reportList,
                    Products = products.Select(x => x.Name),
                    TrainingLevels = trainingLevels.Select(x => x.Value)
                },
                ProductStatistics = productStatList
            };
            ViewBag.List = Session["BreadcrumbList"];
            ViewBag.Role = "HQ";
            return View(model);
        }

        public ActionResult RmUser(int id) {
            if (User.IsInRole("RM")) {
                Session["BreadcrumbList"] = Utils.HtmlExtensions.SetBreadcrumbs((List<BreadcrumbModel>)Session["BreadcrumbList"], string.Format("/User/RmUser/{0}", id), "Home");
            } else {
                Session["BreadcrumbList"] = Utils.HtmlExtensions.SetBreadcrumbs((List<BreadcrumbModel>)Session["BreadcrumbList"], string.Format("/User/RmUser/{0}", id), "RM");
                ViewBag.UserName = userService.GetUser(id).Name;
            }
            var currentDate = DateTime.Now;
            var currentMonth = masterService.FindAndCreateMonth(currentDate.ToString("MMMM"), currentDate.Year);
            var rsmUsers = userService.FindUsers(x => x.ParentId == id);
            var products = masterService.FindProducts(x => x.ObjectInfo.DeletedDate == null).OrderBy(x => x.Id);
            var trainingLevels = Enumeration.GetAll<TrainingLevel>();
            var reportList = new List<ReportManpowerModel>();
            foreach (var rsm in rsmUsers) {
                var csmIds = userService.FindUsers(x => x.ParentId == rsm.Id).Select(x => x.Id);
                var dealerIds = userDealerMapService.FindUserDealerMaps(x => csmIds.Contains(x.UserId)).Select(x => x.DealerId);
                var targets = manpowerTargetService.FindDealerManpowerTargets(x => dealerIds.Contains(x.DealerId) && csmIds.Contains(x.UserId) && x.MonthId == currentMonth.Id);
                var manpowers = manpowerService.FindDealerManpowers(x => csmIds.Contains(x.UserId) && dealerIds.Contains(x.DealerId)).ToList();
                var targetList = new List<TotalManpower>();
                foreach (var product in products) {
                    targetList.Add(new TotalManpower {
                        Plan = targets.Where(x => x.ProductId == product.Id).Sum(x => x.Planned),
                        Actual = manpowers.Count(x => x.ProductId == product.Id)
                    });
                }
                var trainingLevelList = new List<ManpowerTrainingLevel>();
                foreach (var level in trainingLevels) {
                    trainingLevelList.Add(new ManpowerTrainingLevel {
                        Level = level.Value,
                        LevelCount = manpowers.Count(x => x.Profile.TrainingLevel == level.Value)
                    });
                }
                reportList.Add(new ReportManpowerModel {
                    User = rsm.Name,
                    UserUrl = string.Format("/User/RsmUser/{0}", rsm.Id),
                    Manpowers = targetList,
                    TrainingLevels = trainingLevelList
                });
            }
            var productStatList = new List<ProductStatModel>();
            foreach (var product in products) {
                var productVarientIds = masterService.FindProductVarient(x => x.ProductId == product.Id).Select(x => x.Id);
                var rsmIds = userService.FindUsers(x => x.ParentId == id).Select(x => x.Id);
                var csmIds = userService.FindUsers(x => rsmIds.Contains(x.ParentId)).Select(x => x.Id);
                var manpowers = manpowerService.FindDealerManpowers(x => csmIds.Contains(x.UserId) && x.ProductId == product.Id);
                var exitMnapowers = manpowers.Count(x => x.Profile.DateOfLeaving != null);
                var averageEmployee = masterService.FindAverageEmployee(currentDate.AddMonths(-1));
                var manpowerIds = manpowers.Select(x => x.Id);
                var targets =
                    targetService.FindTargets(
                        x => x.MonthId == currentMonth.Id && productVarientIds.Contains(x.ProductVarientId) && manpowerIds.Contains(x.DealerManpowerId));
                productStatList.Add(new ProductStatModel {
                    Product = product.Name,
                    Competency = manpowers.Any() ? Math.Round(manpowers.Average(x => x.CompetencyProfileMaps.Average(y => y.Score)), 2) : 0,
                    Productivity = targets.Any() ? Math.Round(targets.Average(x => x.Actual), 2) : 0,
                    Attrition = exitMnapowers / (averageEmployee > 0 ? averageEmployee : 1)
                });
            }
            var model = new RmUserViewModel {
                ReportManpower = new ReportManpowerViewModel {
                    ReportManpowers = reportList,
                    Products = products.Select(x => x.Name),
                    TrainingLevels = trainingLevels.Select(x => x.Value)
                },
                ProductStatistics = productStatList
            };
            ViewBag.List = Session["BreadcrumbList"];
            ViewBag.Role = "RM";
            return View(model);
        }

        public ActionResult RsmUser(int id) {
            if (User.IsInRole("RSM")) {
                Session["BreadcrumbList"] = Utils.HtmlExtensions.SetBreadcrumbs((List<BreadcrumbModel>)Session["BreadcrumbList"], string.Format("/User/RsmUser/{0}", id), "Home");
            } else {
                Session["BreadcrumbList"] = Utils.HtmlExtensions.SetBreadcrumbs((List<BreadcrumbModel>)Session["BreadcrumbList"], string.Format("/User/RsmUser/{0}", id), "RSM");
                ViewBag.UserName = userService.GetUser(id).Name;
            }
            var currentDate = DateTime.Now;
            var currentMonth = masterService.FindAndCreateMonth(currentDate.ToString("MMMM"), currentDate.Year);
            var csmUsers = userService.FindUsers(x => x.ParentId == id);
            var products = masterService.FindProducts(x => x.ObjectInfo.DeletedDate == null).OrderBy(x => x.Id);
            var trainingLevels = Enumeration.GetAll<TrainingLevel>();
            var reportList = new List<ReportManpowerModel>();
            foreach (var csm in csmUsers) {
                var dealerIds = userDealerMapService.FindUserDealerMaps(x => x.UserId == csm.Id).Select(x => x.DealerId);
                var targets = manpowerTargetService.FindDealerManpowerTargets(x => dealerIds.Contains(x.DealerId) && x.UserId == csm.Id && x.MonthId == currentMonth.Id);
                var manpowers = manpowerService.FindDealerManpowers(x => x.UserId == csm.Id && dealerIds.Contains(x.DealerId)).ToList();
                var targetList = new List<TotalManpower>();
                foreach (var product in products) {
                    targetList.Add(new TotalManpower {
                        Plan = targets.Where(x => x.ProductId == product.Id).Sum(x => x.Planned),
                        Actual = manpowers.Count(x => x.ProductId == product.Id)
                    });
                }
                var trainingLevelList = new List<ManpowerTrainingLevel>();
                foreach (var level in trainingLevels) {
                    trainingLevelList.Add(new ManpowerTrainingLevel {
                        Level = level.Value,
                        LevelCount = manpowers.Count(x => x.Profile.TrainingLevel == level.Value)
                    });
                }
                reportList.Add(new ReportManpowerModel {
                    User = csm.Name,
                    UserUrl = string.Format("/User/CsmUser/{0}", csm.Id),
                    Manpowers = targetList,
                    TrainingLevels = trainingLevelList
                });
            }
            var productStatList = new List<ProductStatModel>();
            foreach (var product in products) {
                var productVarientIds = masterService.FindProductVarient(x => x.ProductId == product.Id).Select(x => x.Id);
                var csmIds = userService.FindUsers(x => x.ParentId == id).Select(x => x.Id);
                var manpowers = manpowerService.FindDealerManpowers(x => csmIds.Contains(x.UserId) && x.ProductId == product.Id);
                var exitMnapowers = manpowers.Count(x => x.Profile.DateOfLeaving != null);
                var averageEmployee = masterService.FindAverageEmployee(currentDate.AddMonths(-1));
                var manpowerIds = manpowers.Select(x => x.Id);
                var targets =
                    targetService.FindTargets(
                        x => x.MonthId == currentMonth.Id && productVarientIds.Contains(x.ProductVarientId) && manpowerIds.Contains(x.DealerManpowerId));
                productStatList.Add(new ProductStatModel {
                    Product = product.Name,
                    Competency = manpowers.Any() ? Math.Round(manpowers.Average(x => x.CompetencyProfileMaps.Average(y => y.Score)), 2) : 0,
                    Productivity = targets.Any() ? Math.Round(targets.Average(x => x.Actual), 2) : 0,
                    Attrition = exitMnapowers / (averageEmployee > 0 ? averageEmployee : 1)
                });
            }
            var model = new RsmUserViewModel {
                ReportManpower = new ReportManpowerViewModel {
                    ReportManpowers = reportList,
                    Products = products.Select(x => x.Name),
                    TrainingLevels = trainingLevels.Select(x => x.Value)
                },
                ProductStatistics = productStatList
            };
            ViewBag.List = Session["BreadcrumbList"];
            ViewBag.Role = "RSM";
            return View(model);
        }

        public ActionResult CsmUser(int id) {
            if (User.IsInRole("CSM")) {
                Session["BreadcrumbList"] = Utils.HtmlExtensions.SetBreadcrumbs((List<BreadcrumbModel>)Session["BreadcrumbList"], string.Format("/User/CsmUser/{0}", id), "Home");
            } else {
                Session["BreadcrumbList"] = Utils.HtmlExtensions.SetBreadcrumbs((List<BreadcrumbModel>)Session["BreadcrumbList"], string.Format("/User/CsmUser/{0}", id), "CSM");
                ViewBag.UserName = userService.GetUser(id).Name;
            }
            var currentDate = DateTime.Now;
            var currentMonth = masterService.FindAndCreateMonth(currentDate.ToString("MMMM"), currentDate.Year);
            var dealers = userDealerMapService.FindUserDealerMaps(x => x.UserId == id).Select(x => x.Dealer);
            var products = masterService.FindProducts(x => x.ObjectInfo.DeletedDate == null).OrderBy(x => x.Id);
            var trainingLevels = Enumeration.GetAll<TrainingLevel>();
            var reportList = new List<ReportManpowerModel>();
            foreach (var dealer in dealers) {
                var targets = manpowerTargetService.FindDealerManpowerTargets(x => x.DealerId == dealer.Id && x.UserId == id && x.MonthId == currentMonth.Id);
                var manpowers = manpowerService.FindDealerManpowers(x => x.UserId == id && x.DealerId == dealer.Id).ToList();
                var targetList = new List<TotalManpower>();
                foreach (var product in products) {
                    targetList.Add(new TotalManpower {
                        Plan = targets.Where(x => x.ProductId == product.Id).Sum(x => x.Planned),
                        Actual = manpowers.Count(x => x.ProductId == product.Id)
                    });
                }
                var trainingLevelList = new List<ManpowerTrainingLevel>();
                foreach (var level in trainingLevels) {
                    trainingLevelList.Add(new ManpowerTrainingLevel {
                        Level = level.Value,
                        LevelCount = manpowers.Count(x => x.Profile.TrainingLevel == level.Value)
                    });
                }
                reportList.Add(new ReportManpowerModel {
                    User = dealer.Name,
                    UserUrl = string.Format("/Dealer/Dealer?dealerId={0}&csmId={1}", dealer.Id, id),
                    Manpowers = targetList,
                    TrainingLevels = trainingLevelList
                });
            }
            var productStatList = new List<ProductStatModel>();
            foreach (var product in products) {
                var productVarientIds = masterService.FindProductVarient(x => x.ProductId == product.Id).Select(x => x.Id);
                var manpowers = manpowerService.FindDealerManpowers(x => x.UserId == id && x.ProductId == product.Id);
                var exitMnapowers = manpowers.Count(x => x.Profile.DateOfLeaving != null);
                var averageEmployee = masterService.FindAverageEmployee(currentDate.AddMonths(-1));
                var manpowerIds = manpowers.Select(x => x.Id);
                var targets =
                    targetService.FindTargets(
                        x => x.MonthId == currentMonth.Id && productVarientIds.Contains(x.ProductVarientId) && manpowerIds.Contains(x.DealerManpowerId));
                productStatList.Add(new ProductStatModel {
                    Product = product.Name,
                    Competency = manpowers.Any() ? Math.Round(manpowers.Average(x => x.CompetencyProfileMaps.Average(y => y.Score)), 2) : 0,
                    Productivity = targets.Any() ? Math.Round(targets.Average(x => x.Actual), 2) : 0,
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
            ViewBag.List = Session["BreadcrumbList"];
            ViewBag.Role = "CSM";
            return View(model);
        }

    }
}
