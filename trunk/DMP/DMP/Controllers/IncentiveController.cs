using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DMP.Models;
using DMP.ModelsView;
using DMP.Repository;
using DMP.Services.Interface;

namespace DMP.Controllers {
    [Authorize]
    public class IncentiveController : Controller {

        private readonly IMasterService masterService;
        private readonly IIncentiveService incentiveService;
        private readonly IDealerManpowerService manpowerService;

        public IncentiveController(IMasterService masterService, IIncentiveService incentiveService, IDealerManpowerService manpowerService) {
            this.masterService = masterService;
            this.incentiveService = incentiveService;
            this.manpowerService = manpowerService;
            if (System.Web.HttpContext.Current.Session["BreadcrumbList"] == null) {
                System.Web.HttpContext.Current.Session["BreadcrumbList"] = new List<BreadcrumbModel>();
            }
        }

        [Authorize(Roles = "CSM")]
        public ActionResult Incentive(int id) {
            Session["BreadcrumbList"] = Utils.HtmlExtensions.SetBreadcrumbs((List<BreadcrumbModel>)Session["BreadcrumbList"], string.Format("/Incentive/Incentive/{0}", id), "Incentive");
            var month = masterService.FindAndCreateMonth(DateTime.Now.ToString("MMMM"), DateTime.Now.Year);
            var specialSchems = masterService.FindSpecialSchemes(x => x.MonthId == month.Id && x.DealerId == id).ToList();
            var manpowers = manpowerService.FindDealerManpowers(x => x.DealerId == id);
            var incentiveList = new List<IncentiveModel>();
            foreach (var manpower in manpowers) {
                var incentives = manpower.Incentives.Where(x => x.MonthId == month.Id);
                var incentive = incentives.Any() ? incentives.First() : new Incentive();
                incentiveList.Add(new IncentiveModel {
                    Id = incentive.Id,
                    DealerManpowerId = manpower.Id,
                    DealerManpower = manpower.Name,
                    DealerIncentive = incentive.Dealer,
                    VolvoIncentive = incentive.Volvo,
                    SpecialIncentives = incentive.SpecialSchemeIncentives != null && incentive.SpecialSchemeIncentives.Any() ? incentive.SpecialSchemeIncentives.Select(SpecialIncentiveModel.FromDomainModel) : specialSchems.Select(x => new SpecialIncentiveModel { Id = 0, SpecialSchemeId = x.Id, SpecialIncentive = 0 })
                });
            }

            var model = new IncentiveViewModel {
                MonthId = month.Id,
                MonthName = string.Format("{0} - {1}", month.Name, month.Year),
                Incentives = incentiveList,
                SpecialSchemes = specialSchems.Select(x => x.Name).ToList()
            };
            ViewBag.List = Session["BreadcrumbList"];
            return View(model);
        }

        [Authorize(Roles = "CSM")]
        public ActionResult SaveIncentives(IncentiveViewModel model) {
            var monthId = model.MonthId;
            foreach (var incentiveModel in model.Incentives) {
                var incentive = IncentiveModel.ToDomainModel(incentiveModel);
                incentive.MonthId = monthId;
                if (incentive.Id > 0) {
                    incentiveService.UpdateIncentive(incentive);
                } else {
                    incentiveService.AddIncentive(new[] { incentive });
                }
                if (incentiveModel.SpecialIncentives == null || !incentiveModel.SpecialIncentives.Any()) {
                    continue;
                }
                var specialIncentives = incentiveModel.SpecialIncentives.Select(SpecialIncentiveModel.ToDomainModel);
                foreach (var specialIncentive in specialIncentives) {
                    specialIncentive.IncentiveId = incentive.Id;
                    if (specialIncentive.Id > 0) {
                        incentiveService.UpdateSpecialSchemeIncentive(specialIncentive);
                    } else {
                        incentiveService.AddSpecialSchemeIncentive(new[] { specialIncentive });
                    }
                }
            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

    }
}
