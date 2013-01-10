using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMP.Models;

namespace DMP.ModelsView {
    public class TrainingProfileViewModel {
        public IEnumerable<KeyValuePair<int, string>> Trainings { get; set; }
        public int ProfileId { get; set; }
        public TrainingProfileModel Training { get; set; }
    }
}