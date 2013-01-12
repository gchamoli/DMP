using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMP.Models;

namespace DMP.ModelsView {
    public class CsmDealerViewModel {
        public DealerManpowerModel Manpower { get; set; }
        public IEnumerable<DealerManpowerModel> Manpowers { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Designations { get; set; }
        public IEnumerable<KeyValuePair<int, string>> TrainingLevels { get; set; }
        public IEnumerable<CompetencyProfileModel> Competencies { get; set; }
        public IEnumerable<TrainingProfileModel> Trainings { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Products { get; set; }
        public IEnumerable<KeyValuePair<int, string>> Attritions { get; set; }
        public IEnumerable<KeyValuePair<int, string>> AllTrainings { get; set; }
        public AttritionProfileModel Attrition { get; set; }
        public TrainingProfileModel Training { get; set; }
    }

    public class ProfileViewModel {
        public DealerManpowerModel Manpower { get; set; }
        public IEnumerable<TrainingProfileModel> Trainings { get; set; }
        public IEnumerable<string> Months { get; set; }
        public IEnumerable<string> Varients { get; set; }
    }

    public class ManageDseViewModel {
        public IEnumerable<DealerManpowerModel> Manpowers { get; set; }
    }
}