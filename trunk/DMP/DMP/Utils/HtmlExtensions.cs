using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using DMP.Models;

namespace DMP.Utils {
    public static class HtmlExtensions {
        public static string ToJSON(this HtmlHelper htmlHelper, object data) {
            return new JavaScriptSerializer().Serialize(data);
        }

        public static List<BreadcrumbModel> SetBreadcrumbs(List<BreadcrumbModel> list, string url, string urlName) {
            var seq = 0;
            if (list.All(x => x.UrlName.ToLower() != urlName.ToLower())) {
                list.Add(new BreadcrumbModel { Url = url, UrlName = urlName, Sequence = list.Count + 1 });
            } else {
                var index = list.FindIndex(x => x.UrlName.ToLower() == urlName.ToLower());
                list[index].Url = url;
            }
            foreach (var link in list) {
                if (link.UrlName.ToLower() == urlName.ToLower()) {
                    seq = link.Sequence;
                }
            }
            var tempList = new List<BreadcrumbModel>();
            for (var i = 0; i < seq; i++) {
                tempList.Add(list[i]);
            }
            return tempList;
        }
    }
}