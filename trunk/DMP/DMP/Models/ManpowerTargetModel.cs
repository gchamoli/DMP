using System.Collections.Generic;
using DMP.Repository;

namespace DMP.Models {
    public class ManpowerTargetModel {

        public int Id { get; set; }

        public int MonthId { get; set; }

        public string Month { get; set; }

        public int UserId { get; set; }

        public string User { get; set; }

        public int ProductId { get; set; }

        public string Product { get; set; }

        public int Planned { get; set; }

        public string Description { get; set; }

        public int DealerId { get; set; }

        public string Dealer { get; set; }

        public static DealerManpowerTargets ToDomainModel(ManpowerTargetModel model) {
            return new DealerManpowerTargets {
                Id = model.Id,
                MonthId = model.MonthId,
                UserId = model.UserId,
                ProductId = model.ProductId,
                DealerId = model.DealerId,
                Planned = model.Planned,
                Description = model.Description
            };
        }
    }

    public class ManpowerProductTargetModel {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Planned { get; set; }
    }

    public class ManpowerTargetPlanModel {
        public int DealerId { get; set; }
        public string Dealer { get; set; }
        public IEnumerable<ManpowerProductTargetModel> Targets { get; set; }

        public static DealerManpowerTargets ToDomainModel(ManpowerProductTargetModel model) {
            return new DealerManpowerTargets {
                Id = model.Id,
                ProductId = model.ProductId,
                Planned = model.Planned
            };
        }
    }
}