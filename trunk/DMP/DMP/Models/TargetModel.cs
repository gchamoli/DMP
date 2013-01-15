using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMP.Repository;

namespace DMP.Models {
    public class TargetModel {

        public int MonthId { get; set; }

        public int ManpowerId { get; set; }

        public string Manpower { get; set; }

        public string Designation { get; set; }

        public bool HasMap {
            get { return Designation.ToLower() == "dsm"; }
        }

        public IEnumerable<TargetPlanModel> Targets { get; set; }

        public static Target ToDomainModel(TargetPlanModel model) {
            return new Target {
                Id = model.Id,
                ProductVarientId = model.ProductVarientId,
                Target1 = model.Target1,
                Target2 = model.Target2,
                Actual = model.Actual
            };
        }
    }

    public class TargetPlanModel {
        public int Id { get; set; }
        public int ProductVarientId { get; set; }
        public int preTarget1 { get; set; }
        public int preTarget2 { get; set; }
        public int Actual { get; set; }
        public int Target1 { get; set; }
        public int Target2 { get; set; }
        public bool IsEditable { get; set; }
        public string PreviousTarget {
            get { return string.Format("{0} | {1} | {2}", preTarget1, preTarget2, Actual); }
        }
    }

    public class ProductVarients {
        public string Product { get; set; }
        public List<string> Varients { get; set; }
    }

}