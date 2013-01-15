using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DMP.Models;
using DMP.ModelsView;
using DMP.Repository;
using DMP.Services;
using DMP.Services.Interface;

namespace DMP.Controllers {

    public class HomeController : Controller {

        private readonly IMasterService masterService;
        private readonly IUserService userService;
        private readonly IUserDealerMapService userDealerMapService;

        public HomeController(IMasterService masterService, IUserService userService, IUserDealerMapService userDealerMapService) {
            this.masterService = masterService;
            this.userService = userService;
            this.userDealerMapService = userDealerMapService;
            if (System.Web.HttpContext.Current.Session["BreadcrumbList"] == null) {
                System.Web.HttpContext.Current.Session["BreadcrumbList"] = new List<BreadcrumbModel>();
            }
        }

        public void Init() {
            // Add Regions
            var regions = new List<Region>
                              {
                                  new Region {Id=0, Name="North", Description="north region"},
                                  new Region {Id=0, Name="South", Description="south region"},
                                  new Region {Id=0, Name="East", Description="east region"},
                                  new Region {Id=0, Name="West", Description="west region"}
                              };
            masterService.AddRegions(regions);

            // Add States
            var states = new List<State>
                             {
                                 new State {Id=0, Name="State1", Description="state",RegionId = 1},
                                 new State {Id=0, Name="State2", Description="state",RegionId = 2},
                                 new State {Id=0, Name="State3", Description="state",RegionId = 3},
                                 new State {Id=0, Name="State4", Description="state",RegionId = 4}
                             };
            masterService.AddStates(states);

            ////Add Dealer
            //var dealers = new List<Dealer>
            //                 {
            //                     new Dealer {Id=0, Name="Dealer1", Description="dealer",StateId = 1},
            //                     new Dealer {Id=0, Name="Dealer2", Description="dealer",StateId = 2},
            //                     new Dealer {Id=0, Name="Dealer3", Description="dealer",StateId = 3},
            //                     new Dealer {Id=0, Name="Dealer4", Description="dealer",StateId = 4}
            //                 };
            //masterService.AddDealer(dealers);

            //Add Category
            var category = new List<ProductCategory>
                             {
                                 new ProductCategory {Id=0, Name="HDV", Description="HDV"}
                             };
            masterService.AddCategory(category);

            //Add Product
            var products = new List<Product>
                             {
                                 new Product {Id=0, Name="Haulage", Description="Haulage",ProductCategoryId = 1,IsCommon = false},
                                 new Product {Id=0, Name="Tipper", Description="Tipper",ProductCategoryId = 1,IsCommon = false},
                                 new Product {Id=0, Name="Common", Description="common",ProductCategoryId = 1,IsCommon = true}
                             };
            masterService.AddProduct(products);

            //Add Varients
            var varients = new List<ProductVarient>
                             {
                                 new ProductVarient {Id=0, Name="H1.0", Description="varient",ProductId = 1},
                                 new ProductVarient {Id=0, Name="H2.0", Description="varient",ProductId = 1},
                                 new ProductVarient {Id=0, Name="H3.0", Description="varient",ProductId = 1},
                                 new ProductVarient {Id=0, Name="H4.0", Description="varient",ProductId = 1},
                                 new ProductVarient {Id=0, Name="H5.0", Description="varient",ProductId = 1},
                                 new ProductVarient {Id=0, Name="T1.0", Description="varient",ProductId = 2},
                                 new ProductVarient {Id=0, Name="T2.0", Description="varient",ProductId = 2}
                             };
            masterService.AddProductVarient(varients);

            //Add Months
            var months = new List<Month>
                           {
                               new Month {Id=0, Name="November", Description="month",Year = 2012,NumberOfEmployee = 10},
                                 new Month {Id=0, Name="December", Description="month",Year = 2012,NumberOfEmployee = 20},
                                 new Month {Id=0, Name="January", Description="month",Year = 2013,NumberOfEmployee = 30},
                                 new Month {Id=0, Name="February", Description="month",Year = 2013,NumberOfEmployee = 40}
                           };
            masterService.AddMonths(months);

            // Add Attrition
            var attriions = new List<Attrition>
                              {
                                  new Attrition {Id=0, Name="Attrition1", Description="attrition",Designation = "Dsm"},
                                  new Attrition {Id=0, Name="Attrition2", Description="attrition",Designation = "Dsm"},
                                  new Attrition {Id=0, Name="Attrition3", Description="attrition",Designation = "Dse"},
                                  new Attrition {Id=0, Name="Attrition4", Description="attrition",Designation = "Dse"}
                              };
            masterService.AddAttritions(attriions);

            // Add Competency
            var competency = new List<Competency>
                              {
                                  new Competency {Id=0, Name="Competency1", Description="competency",Designation = "Dsm"},
                                  new Competency {Id=0, Name="Competency2", Description="competency",Designation = "Dsm"},
                                  new Competency {Id=0, Name="Competency3", Description="competency",Designation = "Dse"},
                                  new Competency {Id=0, Name="Competency4", Description="competency",Designation = "Dse"}
                              };
            masterService.AddCompetencies(competency);

            //Add Users
            var users = new List<User>
                              {
                                  new User {Id=0, Name="hq",Email= "hq@eicher.in",Role="HQ",Description="user",ParentId = 0},
                              };

            userService.AddUser(users);

            ////Add User-Region Map
            //var userRegionMaps = new List<UserRegionMap>
            //                  {
            //                      new UserRegionMap {Id=0, RegionId= 1,UserId = 2,Description="user-region"},
            //                      new UserRegionMap {Id=0, RegionId= 2,UserId = 6,Description="user-region"},
            //                      new UserRegionMap {Id=0, RegionId= 3,UserId = 7,Description="user-region"},
            //                      new UserRegionMap {Id=0, RegionId= 4,UserId = 8,Description="user-region"},
            //                  };
            //masterService.AddUserRegionMap(userRegionMaps);

            ////Add UserDealerMap
            //var userDealerMaps = new List<UserDealerMap>
            //                  {
            //                      new UserDealerMap {Id=0, UserId= 4,DealerId = 1,Description="user-dealer maps"},
            //                      new UserDealerMap {Id=0, UserId= 4,DealerId = 2,Description="user-dealer maps"}
            //                  };
            //userDealerMapService.AddUserDealerMap(userDealerMaps);

            // Add Training
            var trainings = new List<Training>
                              {
                                  new Training {Id=0, Name="Training1", Description="training",Code="T1",Designation = "DSM"},
                                  new Training {Id=0, Name="Training2", Description="training",Code="T2",Designation = "DSM"},
                                  new Training {Id=0, Name="Training3", Description="training",Code="T3",Designation = "DSE"},
                                  new Training {Id=0, Name="Training4", Description="training",Code="T4",Designation = "DSE"}
                              };
            masterService.AddTraining(trainings);

            //// Add SpecialScheme
            //var schemes = new List<SpecialScheme>
            //                  {
            //                      new SpecialScheme {Id=0, Name="Diwali", Description="Diwali special",MonthId= 3,RegionId = 1,StateId = 1,DealerId = 1},
            //                      new SpecialScheme {Id=0, Name="Cristmas", Description="Cristmas special",MonthId= 2,RegionId = 1,StateId = 1,DealerId = 1},
            //                      new SpecialScheme {Id=0, Name="New Year", Description="New Year special",MonthId= 3,RegionId = 1,StateId = 1,DealerId = 1}
            //                  };
            //masterService.AddSpecialScheme(schemes);

        }

        public ActionResult Index() {
            var user = userService.GetUserByUserName(User.Identity.Name);
            if (User.IsInRole("HQ") || User.IsInRole("HQR")) {
                return RedirectToAction("HqUser", "User", new { id = user.Id });
            }
            if (User.IsInRole("CSM")) {
                return RedirectToAction("CsmUser", "User", new { id = user.Id });
            }
            if (User.IsInRole("RSM")) {
                return RedirectToAction("RsmUser", "User", new { id = user.Id });
            }
            return User.IsInRole("RM") ? RedirectToAction("RmUser", "User", new { id = user.Id }) : RedirectToAction("Login", "Account");
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public ActionResult HtmlTest() {
            var list = new List<ProductVarients>
                           {
                               new ProductVarients
                                   {
                                       Product="product1",
                                       Varients=new[] {"varient1", "varient1"}.ToList()
                                   },
                               new ProductVarients
                                   {
                                       Product="product2",
                                       Varients=new[] {"varient2", "varient2"}.ToList()
                                   }
                           };
            ViewBag.List = Session["BreadcrumbList"];
            return View(list);
        }

        public void DeleteAllUsers() {

        }
    }
}
