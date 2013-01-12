using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMP.ModelsView {
    public class DsmDseMapViewModel {
        public IEnumerable<KeyValuePair<int, string>> DsmList { get; set; }
        public IEnumerable<KeyValuePair<int, string>> DseList { get; set; }
    }
}