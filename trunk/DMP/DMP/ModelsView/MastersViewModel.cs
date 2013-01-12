using System.Collections.Generic;
using DMP.Models;

namespace DMP.ModelsView {
    public class MastersViewModel {
    }

    public class RegionViewModel {
        public IEnumerable<RegionModel> Regions { get; set; }
    }

    public class StateViewModel {
        public IEnumerable<StateModel> States { get; set; }
        public StateModel State { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Regions { get; set; }
    }

    public class ProductCategoryViewModel {
        public IEnumerable<ProductCategoryModel> Categories { get; set; }
    }

    public class ProductViewModel {
        public ProductModel Product { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Categories { get; set; }
    }

    public class ProductVarientViewModel {
        public ProductVarientModel ProductVarient { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Products { get; set; }
    }

    public class TrainingViewModel {
        public TrainingModel Training { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Designations { get; set; }
    }

    public class DealerViewModel {
        public DealerModel Dealer { get; set; }
        public IEnumerable<KeyValuePair<int, string>> States { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Users { get; set; }
    }

    public class UserViewModel {
        public IEnumerable<UserModel> Users { get; set; }
        public UserModel User { get; set; }
        public IEnumerable<KeyValuePair<string, string>> Roles { get; set; }
        public IEnumerable<KeyValuePair<int, string>> ParentUsers { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Regions { get; set; }
        public ResetPasswordModel ResetPassword { get; set; }
    }

    public class AttritionViewModel {
        public AttritionModel Attrition { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Designations { get; set; }
    }

    public class CompetencyViewModel {
        public CompetencyModel Competency { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Designations { get; set; }
    }

    public class SpecialSchemeViewModel {
        public IEnumerable<SpecialSchemeModel> SpecialSchemes { get; set; }
        public SpecialSchemeModel SpecialScheme { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Regions { get; set; }
        public IEnumerable<KeyValuePair<int, string>> States { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Dealers { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Months { get; set; }
    }
}