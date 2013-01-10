using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMP.Models;

namespace DMP.ModelsView {
    public class IncentiveViewModel {
        public IEnumerable<IncentiveModel> Incentives { get; set; }
        public int MonthId { get; set; }
        public string MonthName { get; set; }
        public IEnumerable<string> SpecialSchemes { get; set; }
    }
}