using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMP.Models;

namespace DMP.ModelsView {
    public class TargetViewModel {
        public IEnumerable<TargetModel> Targets { get; set; }
        public IEnumerable<ProductVarients> ProductVarients { get; set; }
        public int CsmId { get; set; }
        public int MonthId { get; set; }
        public int DealerId { get; set; }
    }
}