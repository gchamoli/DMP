using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMP.Models {
    public class ReportManpowerModel {
        public int UserId { get; set; }
        public string User { get; set; }
        public string UserUrl { get; set; }
        public IEnumerable<TotalManpower> Manpowers { get; set; }
        public IEnumerable<ManpowerTrainingLevel> TrainingLevels { get; set; }
    }

    public class TotalManpower {
        public int Plan { get; set; }
        public int Actual { get; set; }
    }

    public class ManpowerTrainingLevel {
        public string Level { get; set; }
        public int LevelCount { get; set; }
    }

    public class ProductStatModel {
        public string Product { get; set; }
        public double Competency { get; set; }
        public double Productivity { get; set; }
        public double Attrition { get; set; }
    }
}