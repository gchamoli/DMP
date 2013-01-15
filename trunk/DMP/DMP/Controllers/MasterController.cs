using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DMP.Models;
using DMP.ModelsView;
using DMP.Repository;
using DMP.Services;
using DMP.Services.Interface;

namespace DMP.Controllers {

    [Authorize(Roles = "HQ,HQR")]
    public class MasterController : Controller {
        private readonly IMasterService masterService;
        private readonly ICompetencyProfileMapService competencyProfileMapService;
        private readonly ITrainingProfileMapService trainingProfileMapService;
        private readonly IDealerManpowerService dealerManpowerService;
        private readonly IUserService userService;
        private readonly IUserDealerMapService userDealerMapService;

        public MasterController(IMasterService masterService, ICompetencyProfileMapService competencyProfileMapService, ITrainingProfileMapService trainingProfileMapService, IDealerManpowerService dealerManpowerService, IUserService userService, IUserDealerMapService userDealerMapService) {
            this.masterService = masterService;
            this.competencyProfileMapService = competencyProfileMapService;
            this.trainingProfileMapService = trainingProfileMapService;
            this.dealerManpowerService = dealerManpowerService;
            this.userService = userService;
            this.userDealerMapService = userDealerMapService;
        }

        #region Regions

        public ActionResult Regions() {
            var model = new RegionViewModel {
                Regions = masterService.GetAllRegions().Select(RegionModel.FromDomainModel).ToList()
            };
            ViewBag.List = Session["BreadcrumbList"];
            return View(model);
        }

        public ActionResult EditRegion(int id) {
            var model = id > 0 ? RegionModel.FromDomainModel(masterService.GetRegion(id)) : new RegionModel();
            return PartialView("EditRegion", model);
        }

        [HttpPost]
        public void EditRegion(RegionModel model) {
            if (ModelState.IsValid) {
                var region = RegionModel.ToDomainModel(model);
                if (region.Id > 0) {
                    masterService.UpdateRegion(region);
                } else {
                    masterService.AddRegions(new[] { region });
                }
            }
            //return RedirectToAction("Regions");
        }

        public ActionResult DeleteRegion(int id) {
            masterService.DeleteRegion(id);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region State

        public ActionResult States() {
            var states = masterService.GetAllStates().Select(StateModel.FromDomainModel).ToList();
            ViewBag.List = Session["BreadcrumbList"];
            return View(states);
        }

        public ActionResult EditState(int id) {
            var state = id > 0 ? StateModel.FromDomainModel(masterService.GetState(id)) : new StateModel();
            var model = new StateViewModel {
                State = state,
                Regions = masterService.GetAllRegions().Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList()
            };
            return PartialView("EditState", model);
        }

        [HttpPost]
        public void EditState(StateViewModel model) {
            if (!ModelState.IsValid) {
                return;
            }
            var state = StateModel.ToDomainModel(model.State);
            if (state.Id > 0) {
                masterService.UpdateState(state);
            } else {
                masterService.AddStates(new[] { state });
            }
        }

        public ActionResult DeleteState(int id) {
            masterService.DeleteState(id);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ProductCategory

        public ActionResult Categories() {
            var model = new ProductCategoryViewModel {
                Categories = masterService.GetAllCategories().Select(ProductCategoryModel.FromDomainModel).ToList()
            };
            ViewBag.List = Session["BreadcrumbList"];
            return View(model);
        }

        public ActionResult EditCategory(int id) {
            var model = id > 0
                          ? ProductCategoryModel.FromDomainModel(masterService.GetProductCategory(id))
                          : new ProductCategoryModel();
            return PartialView("EditCategory", model);
        }

        [HttpPost]
        public void EditCategory(ProductCategoryModel model) {
            if (!ModelState.IsValid) {
                return;
            }
            var category = ProductCategoryModel.ToDomainModel(model);
            if (category.Id > 0) {
                masterService.UpdateCategory(category);
            } else {
                masterService.AddCategory(new[] { category });
            }
        }

        public ActionResult DeleteCategory(int id) {
            masterService.DeleteCategory(id);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Product

        public ActionResult Products() {
            var products = masterService.GetAllProducts().Select(ProductModel.FromDomainModel).ToList();
            ViewBag.List = Session["BreadcrumbList"];
            return View(products);
        }

        public ActionResult EditProduct(int id) {
            var product = id > 0 ? ProductModel.FromDomainModel(masterService.GetProduct(id)) : new ProductModel();
            var model = new ProductViewModel {
                Product = product,
                Categories = masterService.GetAllCategories().Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList()
            };
            return PartialView("EditProduct", model);
        }

        [HttpPost]
        public void EditProduct(ProductViewModel model) {
            var product = ProductModel.ToDomainModel(model.Product);
            if (product.Id > 0) {
                masterService.UpdateProduct(product);
            } else {
                masterService.AddProduct(new[] { product });
            }
        }

        public ActionResult DeleteProduct(int id) {
            masterService.DeleteProduct(id);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region ProductVarient

        public ActionResult Varients() {
            var varients = masterService.GetAllProductVarients().Select(ProductVarientModel.FromDomainModel).ToList();
            ViewBag.List = Session["BreadcrumbList"];
            return View(varients);
        }

        public ActionResult EditVarient(int id) {
            var varient = id > 0
                            ? ProductVarientModel.FromDomainModel(masterService.GetProductVarient(id))
                            : new ProductVarientModel();
            var model = new ProductVarientViewModel {
                ProductVarient = varient,
                Products = masterService.GetAllProducts().Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList()
            };
            return PartialView("EditVarient", model);
        }

        [HttpPost]
        public void EditVarient(ProductVarientViewModel model) {
            if (!ModelState.IsValid) {
                return;
            }
            var varient = ProductVarientModel.ToDomainModel(model.ProductVarient);
            if (varient.Id > 0) {
                masterService.UpdateProductVarient(varient);
            } else {
                masterService.AddProductVarient(new[] { varient });
            }
        }

        public ActionResult DeleteVarient(int id) {
            masterService.DeleteProductVarient(id);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Training

        public ActionResult Trainings() {
            var trainings = masterService.GetAllTrainings().Select(TrainingModel.FromDomainModel).ToList();
            ViewBag.List = Session["BreadcrumbList"];
            return View(trainings);
        }

        public ActionResult EditTraining(int id) {
            var model = new TrainingViewModel {
                Designations = Enumeration.GetAll<Designation>(),
                Training = id > 0 ? TrainingModel.FromDomainModel(masterService.GetTraining(id)) : new TrainingModel()
            };
            return PartialView("EditTraining", model);
        }

        [HttpPost]
        public void EditTraining(TrainingViewModel model) {
            if (!ModelState.IsValid) {
                return;
            }
            var training = TrainingModel.ToDomainModel(model.Training);
            if (training.Id > 0) {
                masterService.UpdateTraining(training);
            } else {
                masterService.AddTraining(new[] { training });
                //Add New Training to TrainingProfileMap
                var manpowerIds =
                    dealerManpowerService.GetAllDealerManpowers().Select(x => x.Id);
                var list = manpowerIds.Select(id => new TrainingProfileMap() { Id = 0, TrainingId = training.Id, DealerManpowerId = id }).ToList();
                trainingProfileMapService.AddTrainingProfileMap(list);
            }
        }

        public ActionResult DeleteTraining(int id) {
            masterService.DeleteTraining(id);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Dealer

        public ActionResult Dealers() {
            var dealers = masterService.GetAllDealers().Select(DealerModel.FromDomainModel).ToList();
            ViewBag.List = Session["BreadcrumbList"];
            return View(dealers);
        }

        public ActionResult EditDealer(int id) {
            var dealer = id > 0 ? DealerModel.FromDomainModel(masterService.GetDealer(id)) : new DealerModel();
            var states = masterService.GetAllStates().Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList();
            var stateId = id > 0 ? dealer.StateId : (states.Any() ? states.First().Key : 0);
            var model = new DealerViewModel {
                Dealer = dealer,
                States = states,
                Users = GetCsmUserFromState(stateId).Select(x => new KeyValuePair<int, string>(x.Id, x.Name))
            };
            return PartialView("EditDealer", model);
        }

        [HttpPost]
        public void EditDealer(DealerViewModel model) {
            if (model.Dealer.UserId > 0) {
                var dealer = DealerModel.ToDomainModel(model.Dealer);
                if (dealer.Id > 0) {
                    masterService.UpdateDealer(dealer);
                } else {
                    masterService.AddDealer(new[] { dealer });
                }
                //Add-Update Dealer-User Map
                var maps = userDealerMapService.FindUserDealerMaps(x => x.UserId == model.Dealer.UserId && x.DealerId == dealer.Id);
                if (maps.Any()) {
                    var map = maps.First();
                    map.UserId = model.Dealer.UserId;
                    userDealerMapService.UpdateUserDealerMap(map);
                } else {
                    userDealerMapService.AddUserDealerMap(new[] { new UserDealerMap { Id = 0, DealerId = dealer.Id, UserId = model.Dealer.UserId } });
                }
            }
        }

        public ActionResult DeleteDealer(int id) {
            masterService.DeleteDealer(id);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCsmFromState(string stateId) {
            int id;
            int.TryParse(stateId, out id);
            var users = GetCsmUserFromState(id).Select(x => new KeyValuePair<int, string>(x.Id, x.Name));
            return Json(new { success = true, csmUsers = users }, JsonRequestBehavior.AllowGet);
        }

        private IEnumerable<User> GetCsmUserFromState(int stateId) {
            if (stateId > 0) {
                var state = masterService.GetState(stateId);
                var rmIds = state != null ? state.Region.UserRegionMaps.Select(x => x.UserId) : new List<int>();
                var rsmIds = rmIds.Any()
                               ? userService.FindUsers(x => rmIds.Contains(x.ParentId)).Select(x => x.Id)
                               : new List<int>();
                var users = rsmIds.Any()
                              ? userService.FindUsers(x => rsmIds.Contains(x.ParentId)).ToList()
                              : new List<User>();
                return users;
            }
            return new List<User>();
        }

        #endregion

        #region Attrition

        public ActionResult Attrition() {
            var attritions = masterService.GetAllAttritions().Select(AttritionModel.FromDomainModel).ToList();
            ViewBag.List = Session["BreadcrumbList"];
            return View(attritions);
        }

        public ActionResult EditAttrition(int id) {
            var model = new AttritionViewModel {
                Attrition = id > 0 ? AttritionModel.FromDomainModel(masterService.GetAttrition(id)) : new AttritionModel(),
                Designations = Enumeration.GetAll<Designation>(),
            };
            return PartialView("EditAttrition", model);
        }

        [HttpPost]
        public void EditAttrition(AttritionViewModel model) {
            var attrition = AttritionModel.ToDomainModel(model.Attrition);
            if (attrition.Id > 0) {
                masterService.UpdateAttrition(attrition);
            } else {
                masterService.AddAttritions(new[] { attrition });
            }
        }

        public ActionResult DeleteAttrition(int id) {
            masterService.DeleteAttrition(id);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Competency

        public ActionResult Competency() {
            var competencies = masterService.GetAllCompetencies().Select(CompetencyModel.FromDomainModel).ToList();
            ViewBag.List = Session["BreadcrumbList"];
            return View(competencies);
        }

        public ActionResult EditCompetency(int id) {
            var model = new CompetencyViewModel {
                Competency = id > 0 ? CompetencyModel.FromDomainModel(masterService.GetCompetency(id)) : new CompetencyModel(),
                Designations = Enumeration.GetAll<Designation>()
            };
            return PartialView("EditCompetency", model);
        }

        [HttpPost]
        public void EditCompetency(CompetencyViewModel model) {
            var competency = CompetencyModel.ToDomainModel(model.Competency);
            if (competency.Id > 0) {
                masterService.UpdateCompetency(competency);
            } else {
                masterService.AddCompetencies(new[] { competency });
                //Add New Competency to CompetencyProfileMap
                var manpowerIds =
                    dealerManpowerService.GetAllDealerManpowers().Select(x => x.Id);
                var list = manpowerIds.Select(id => new CompetencyProfileMap { Id = 0, CompetencyId = competency.Id, DealerManpowerId = id, Score = 0 }).ToList();
                competencyProfileMapService.AddCompetencyProfileMap(list);
            }
        }

        public ActionResult DeleteCompetency(int id) {
            masterService.DeleteCompetency(id);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region SpecialScheme

        public ActionResult SpecialSchemes() {
            var model = new SpecialSchemeViewModel {
                SpecialSchemes = masterService.GetSpecialSchemes().Select(SpecialSchemeModel.FromDomainModel)
            };
            ViewBag.List = Session["BreadcrumbList"];
            return View(model);
        }

        public ActionResult EditScheme(int id) {
            var model = new SpecialSchemeViewModel {
                SpecialScheme = id > 0 ? SpecialSchemeModel.FromDomainModel(masterService.GetSpecialScheme(id)) : new SpecialSchemeModel(),
                Regions = masterService.GetAllRegions().Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList(),
                States = id > 0 ? masterService.GetAllStates().Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList() : new List<KeyValuePair<int, string>>(),
                Dealers = id > 0 ? masterService.GetAllDealers().Select(x => new KeyValuePair<int, string>(x.Id, x.Name)).ToList() : new List<KeyValuePair<int, string>>(),
                Months = Enumeration.GetAll<Months>(),
            };
            model.SpecialScheme.DisplayMonthId = id > 0 ? masterService.GetMonthIndex(model.SpecialScheme.Month.Split('-')[0]) : DateTime.Now.Month;
            return PartialView("EditScheme", model);
        }

        [HttpPost]
        public void EditScheme(SpecialSchemeViewModel model) {
            var enumMonth = (Months)model.SpecialScheme.DisplayMonthId;
            var currentDate = DateTime.Now;
            var month = currentDate.Month > model.SpecialScheme.DisplayMonthId
                          ? masterService.FindAndCreateMonth(enumMonth.ToString(), currentDate.AddYears(1).Year)
                          : masterService.FindAndCreateMonth(enumMonth.ToString(), currentDate.Year);

            var specialScheme = SpecialSchemeModel.ToDomainModel(model.SpecialScheme);
            specialScheme.MonthId = month.Id;
            if (specialScheme.Id > 0) {
                masterService.UpdateSpecialScheme(specialScheme);
            } else {
                var regionIds = new[] { model.SpecialScheme.RegionId ?? 0 }.ToList();
                var stateIds = new[] { model.SpecialScheme.StateId ?? 0 }.ToList();
                var dealerIds = new[] { model.SpecialScheme.DealerId ?? 0 }.ToList();
                if (model.SpecialScheme.RegionId.HasValue && model.SpecialScheme.RegionId > 0) {
                    if (model.SpecialScheme.StateId.HasValue && model.SpecialScheme.StateId > 0) {
                        if (model.SpecialScheme.DealerId.HasValue && model.SpecialScheme.DealerId <= 0) {
                            dealerIds = masterService.FindDealers(x => x.StateId == model.SpecialScheme.StateId).Select(x => x.Id).ToList();
                        }
                    } else {
                        stateIds = masterService.FindStates(x => x.RegionId == model.SpecialScheme.RegionId).Select(x => x.Id).ToList();
                        dealerIds = masterService.FindDealers(x => stateIds.Contains(x.StateId)).Select(x => x.Id).ToList();
                    }
                } else {
                    regionIds = masterService.GetAllRegions().Select(x => x.Id).ToList();
                    stateIds = masterService.FindStates(x => regionIds.Contains(x.RegionId)).Select(x => x.Id).ToList();
                    dealerIds = masterService.FindDealers(x => stateIds.Contains(x.StateId)).Select(x => x.Id).ToList();
                }
                var schemeList = (from region in regionIds from state in stateIds from dealer in dealerIds select new SpecialScheme { Id = 0, Name = specialScheme.Name, RegionId = region, StateId = state, DealerId = dealer, Description = specialScheme.Description, MonthId = month.Id }).ToList();
                masterService.AddSpecialScheme(schemeList);
            }
        }

        public ActionResult DeleteScheme(int id) {
            masterService.DeleteSpecialScheme(id);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetStateFromRegion(string regionId) {
            int id;
            int.TryParse(regionId, out id);
            var states = id > 0
                           ? masterService.FindStates(x => x.RegionId == id)
                           : masterService.GetAllStates();

            var statesList = states.Select(x => new KeyValuePair<int, string>(x.Id, x.Name));
            return Json(new { success = true, states = statesList }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDealerFromState(string stateId) {
            int id;
            int.TryParse(stateId, out id);
            var dealers = id > 0
                           ? masterService.FindDealers(x => x.StateId == id)
                           : masterService.GetAllDealers();

            var dealerList = dealers.Select(x => new KeyValuePair<int, string>(x.Id, x.Name));
            return Json(new { success = true, dealers = dealerList }, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}
