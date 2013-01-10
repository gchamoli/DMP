using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMP.Models;

namespace DMP.ModelsView {

    public class HqUserViewModel {
        public ReportManpowerViewModel ReportManpower { get; set; }
        public IEnumerable<ProductStatModel> ProductStatistics { get; set; }
    }

    public class RmUserViewModel {
        public ReportManpowerViewModel ReportManpower { get; set; }
        public IEnumerable<ProductStatModel> ProductStatistics { get; set; }
    }

    public class RsmUserViewModel {
        public ReportManpowerViewModel ReportManpower { get; set; }
        public IEnumerable<ProductStatModel> ProductStatistics { get; set; }
    }

    public class CsmUserViewModel {
        public ReportManpowerViewModel ReportManpower { get; set; }
        public IEnumerable<ProductStatModel> ProductStatistics { get; set; }
    }
}