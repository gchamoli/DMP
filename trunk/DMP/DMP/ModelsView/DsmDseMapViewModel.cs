using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMP.ModelsView {
    public class DsmDseMapViewModel {
        public string Dsm { get; set; }
        public IEnumerable<KeyValuePair<int,string>> DseList { get; set; }
        public IEnumerable<int> DseIds { get; set; }
        public int DsmId { get; set; }
    }

    public class DseDsmMapViewModel
    {
        public string Dsm { get; set; }
        public IEnumerable<KeyValuePair<KeyValuePair<int, bool>, string>> DseList { get; set; }
        public IEnumerable<int> DseIds { get; set; }
        public int DsmId { get; set; }
    }
}