using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMP.Repository;

namespace DMP.Models {
    public class IncentiveModel {
        public int Id { get; set; }
        public int DealerManpowerId { get; set; }
        public string DealerManpower { get; set; }
        public double DealerIncentive { get; set; }
        public double VolvoIncentive { get; set; }
        public IEnumerable<SpecialIncentiveModel> SpecialIncentives { get; set; }
        public int MonthId { get; set; }

        public static Incentive ToDomainModel(IncentiveModel model) {
            return new Incentive {
                Id = model.Id,
                Dealer = model.DealerIncentive,
                Volvo = model.VolvoIncentive,
                DealerManpowerId = model.DealerManpowerId
            };
        }

        public static IncentiveModel FromDomainModel(Incentive incentive) {
            return new IncentiveModel {
                Id = incentive.Id,
                DealerManpowerId = incentive.DealerManpowerId,
                DealerManpower = incentive.DealerManpower.Name,
                DealerIncentive = incentive.Dealer,
                VolvoIncentive = incentive.Volvo,
                SpecialIncentives = incentive.SpecialSchemeIncentives != null && incentive.SpecialSchemeIncentives.Any() ? incentive.SpecialSchemeIncentives.Select(SpecialIncentiveModel.FromDomainModel) : new List<SpecialIncentiveModel>()
            };
        }
    }

    public class SpecialIncentiveModel {
        public int Id { get; set; }
        public int SpecialSchemeId { get; set; }
        public double SpecialIncentive { get; set; }

        public static SpecialSchemeIncentive ToDomainModel(SpecialIncentiveModel model) {
            return new SpecialSchemeIncentive {
                Id = model.Id,
                SpecialSchemeId = model.SpecialSchemeId,
                SpecialIncentive = model.SpecialIncentive
            };
        }

        public static SpecialIncentiveModel FromDomainModel(SpecialSchemeIncentive specialIncentive) {
            return new SpecialIncentiveModel {
                Id = specialIncentive.Id,
                SpecialSchemeId = specialIncentive.SpecialSchemeId,
                SpecialIncentive = specialIncentive.SpecialIncentive
            };
        }
    }
}