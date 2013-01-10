using System.Collections.Generic;
using DMP.Models;

namespace DMP.ModelsView {
    public class AttritionProfileViewModel {
        public AttritionProfileModel Attrition { get; set; }
        public int ProfileId { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Attritions { get; set; }
    }
}