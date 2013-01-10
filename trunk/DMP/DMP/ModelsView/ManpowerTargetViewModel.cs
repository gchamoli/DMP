using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMP.Models;

namespace DMP.ModelsView {
    public class ManpowerTargetViewModel {
        public int CsmId { get; set; }
        public int MonthId { get; set; }
        public string MonthName { get; set; }
        public IEnumerable<ManpowerTargetPlanModel> TargetPlans { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Products { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Months { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Users { get; set; }
    }
}