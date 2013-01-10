using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMP.Models;

namespace DMP.ModelsView {
    public class ReportManpowerViewModel {
        public IEnumerable<ReportManpowerModel> ReportManpowers { get; set; }
        public IEnumerable<string> Products { get; set; }
        public IEnumerable<string> TrainingLevels { get; set; }
    }
}