using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DMP.Models;

namespace DMP.ModelsView {
    public class CompetencyProfileViewModel {
        public IEnumerable<CompetencyProfileModel> Competencies { get; set; }
        public int ProfileId { get; set; }
    }
}