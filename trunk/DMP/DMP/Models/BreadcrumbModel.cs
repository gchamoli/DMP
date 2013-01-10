using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMP.Models {
    public class BreadcrumbModel {
        public string Url { get; set; }
        public string UrlName { get; set; }
        public int Sequence { get; set; }
    }
}