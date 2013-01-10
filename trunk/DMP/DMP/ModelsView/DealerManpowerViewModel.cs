using System.Collections.Generic;
using DMP.Models;

namespace DMP.ModelsView {
    public class DealerManpowerViewModel {
        public IEnumerable<DealerManpowerModel> Manpowers { get; set; }
        public DealerManpowerModel Manpower { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Dealers { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Users { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Designations { get; set; }
    }
}