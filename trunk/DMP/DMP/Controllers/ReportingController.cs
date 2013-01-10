using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMP.Controllers {
    public class ReportingController : Controller {


        public ActionResult MonthWisePlanAndActual() {
            return PartialView("MonthWisePlanAndActual");
        }

    }
}
