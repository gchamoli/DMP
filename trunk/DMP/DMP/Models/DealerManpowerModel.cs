using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using DMP.Repository;

namespace DMP.Models {
    public class DealerManpowerModel {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Designation")]
        public string Type { get; set; }

        public int DealerId { get; set; }

        public string Dealer { get; set; }

        public int UserId { get; set; }

        public string User { get; set; }

        public int ProfileId { get; set; }

        [Required]
        [Display(Name = "Segment Representing")]
        public int ProductId { get; set; }

        [Display(Name = "Segment Representing")]
        public string Product { get; set; }

        [Required]
        [Display(Name = "Contact")]
        public string ContactNumber { get; set; }

        public string Address { get; set; }

        public string Designation { get; set; }

        public string Email { get; set; }

        [Display(Name = "PAN Number")]
        public string PANNumber { get; set; }

        [Display(Name = "Previous Company")]
        public string PreviousCompany { get; set; }

        [Display(Name = "Previous Job Profile")]
        public string PreviousJobProfile { get; set; }

        [Display(Name = "Total Work Experience")]
        public double? TotalWorkExperience { get; set; }

        [Display(Name = "Experience With ETB")]
        public double? ExperienceWithVE { get; set; }

        [Display(Name = "TIV Representing")]
        public string TIVRepresenting { get; set; }

        [Display(Name = "DDC Training")]
        public string TrainingLevel { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Date of Birth")]
        public string DOBString {
            get { return DateOfBirth.HasValue && DateOfBirth.Value.Ticks > 0 ? DateOfBirth.Value.ToString("dd-MMMM-yyyy") : string.Empty; }
            set {
                DateTime date;
                if (!DateTime.TryParseExact(value, "dd-MMMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date)) {
                    DateOfBirth = DateTime.Now;
                }
                DateOfBirth = date;
            }
        }

        public DateTime? DateOfJoining { get; set; }

        [Display(Name = "Date of Joining")]
        public string DOJString {
            get { return DateOfJoining.HasValue && DateOfJoining.Value.Ticks > 0 ? DateOfJoining.Value.ToString("dd-MMMM-yyyy") : string.Empty; }
            set {
                DateTime date;
                if (!DateTime.TryParseExact(value, "dd-MMMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date)) {
                    DateOfJoining = DateTime.Now;
                }
                DateOfJoining = date;
            }
        }

        public DateTime? DateOfLeaving { get; set; }

        [Display(Name = "Date of Leaving")]
        public string DOLString { get; set; }

        public string Description { get; set; }

        public double Competency { get; set; }

        public double Productivity { get; set; }

        public double Salary { get; set; }

        [Display(Name = "SAP Code")]
        public string SAPId { get; set; }

        [Display(Name = "Area Representing")]
        public string AreaRepresenting { get; set; }

        public static DealerManpower ToManpowerDomain(DealerManpowerModel model) {
            return new DealerManpower {
                Id = model.Id,
                Name = model.Name,
                Type = model.Type,
                Description = model.Description,
                UserId = model.UserId,
                DealerId = model.DealerId,
                ProductId = model.ProductId
            };
        }

        public static Profile ToProfileDomain(DealerManpowerModel model) {
            return new Profile {
                Id = model.ProfileId,
                ContactNumber = model.ContactNumber,
                Address = model.Address,
                Designation = model.Type,
                Email = model.Email,
                PANNumber = model.PANNumber,
                DateOfJoining = model.DateOfJoining.HasValue && model.DateOfJoining.Value.Ticks > 0 ? model.DateOfJoining : null,
                PreviousCompany = model.PreviousCompany,
                PreviousJobProfile = model.PreviousJobProfile,
                TotalWorkExperience = model.TotalWorkExperience,
                ExperienceWithVE = model.ExperienceWithVE,
                TIVRepresenting = model.TIVRepresenting,
                TrainingLevel = model.TrainingLevel,
                DateOfLeaving = model.DateOfLeaving,
                DateOfBirth = model.DateOfBirth.HasValue && model.DateOfBirth.Value.Ticks > 0 ? model.DateOfBirth : null,
                Description = model.Description,
                SAPCode = model.SAPId,
                AreaRepresenting = model.AreaRepresenting
            };
        }

        public static DealerManpowerModel FromDomainModel(DealerManpower manpower) {
            var model = new DealerManpowerModel {
                Id = manpower.Id,
                Name = manpower.Name,
                Type = manpower.Type,
                DealerId = manpower.DealerId,
                Dealer = manpower.Dealer.Name,
                UserId = manpower.UserId,
                User = manpower.User.Name,
                ProductId = manpower.ProductId,
                Product = manpower.Product.Name,
                ProfileId = manpower.Profile.Id,
                ContactNumber = manpower.Profile.ContactNumber,
                Address = manpower.Profile.Address,
                Designation = manpower.Profile.Designation,
                Email = manpower.Profile.Email,
                PANNumber = manpower.Profile.PANNumber,
                DateOfJoining = manpower.Profile.DateOfJoining,
                PreviousCompany = manpower.Profile.PreviousCompany,
                PreviousJobProfile = manpower.Profile.PreviousJobProfile,
                TotalWorkExperience = manpower.Profile.TotalWorkExperience,
                ExperienceWithVE = manpower.Profile.ExperienceWithVE,
                TIVRepresenting = manpower.Profile.TIVRepresenting,
                AreaRepresenting = manpower.Profile.AreaRepresenting,
                TrainingLevel = manpower.Profile.TrainingLevel,
                DateOfLeaving = manpower.Profile.DateOfLeaving,
                DateOfBirth = manpower.Profile.DateOfBirth,
                Description = manpower.Profile.Description,
                SAPId = manpower.Profile.SAPCode,
                DOLString = manpower.AttritionProfileMap != null ? (manpower.AttritionProfileMap.DateOfLeaving.HasValue ? manpower.AttritionProfileMap.DateOfLeaving.Value.ToString("dd-MMMM-yyyy") : string.Empty) : string.Empty,
                Competency = manpower.CompetencyProfileMaps != null && manpower.CompetencyProfileMaps.Any() ? manpower.CompetencyProfileMaps.Average(x => x.Score) : 0,
                Salary = manpower.ManpowerSalaries != null && manpower.ManpowerSalaries.Any() ? manpower.ManpowerSalaries.OrderByDescending(x => x.ObjectInfo.CreatedDate).First().Salary : 0,
                Productivity = 0
            };
            return model;
        }
    }

    public class CompetencyProfileModel {
        public int Id { get; set; }
        public int ComptencyId { get; set; }
        public string Competency { get; set; }
        [Range(0,10,ErrorMessage = "Please select a value betwwen 0 and 10")]
        public double Score { get; set; }

        public static CompetencyProfileMap ToDomainModel(CompetencyProfileModel model) {
            return new CompetencyProfileMap {
                Id = model.Id,
                CompetencyId = model.ComptencyId,
                Score = model.Score
            };
        }
    }

    public class TrainingProfileModel {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        [Display(Name = "Training")]
        public int TrainingId { get; set; }
        public string Training { get; set; }
        public int NumberOfDays { get; set; }
        public int NumberOfTrainings { get; set; }
        public DateTime? TrainingDate { get; set; }
        [Display(Name = "Training Date")]
        public string TDString {
            get { return TrainingDate.HasValue ? TrainingDate.Value.ToString("dd-MMMM-yyyy") : string.Empty; }
            set {
                DateTime date;
                if (!DateTime.TryParseExact(value, "dd-MMMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date)) {
                    TrainingDate = DateTime.UtcNow;
                }
                TrainingDate = date;
            }
        }

        public static TrainingProfileMap ToDomainModel(TrainingProfileModel model) {
            return new TrainingProfileMap {
                Id = model.Id,
                DealerManpowerId = model.ProfileId,
                TrainingId = model.TrainingId,
                LastTrainingDate = model.TrainingDate
            };
        }

    }

    public class AttritionProfileModel {
        public int Id { get; set; }
        [Display(Name = "Attrition")]
        public int AttritionId { get; set; }
        public DateTime? DateOfLeaving { get; set; }
        [Display(Name = "Leaving Date")]
        public string DOLString {
            get { return DateOfLeaving.HasValue ? DateOfLeaving.Value.ToString("dd-MMMM-yyyy") : string.Empty; }
            set {
                DateTime date;
                if (!DateTime.TryParseExact(value, "dd-MMMM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date)) {
                    DateOfLeaving = DateTime.UtcNow;
                }
                DateOfLeaving = date;
            }
        }

        public static AttritionProfileModel FromDomainModel(AttritionProfileMap map) {
            return new AttritionProfileModel {
                Id = map.Id,
                AttritionId = map.AttritionId,
                DateOfLeaving = map.DateOfLeaving
            };
        }

        public static AttritionProfileMap ToDomainModel(AttritionProfileModel model) {
            return new AttritionProfileMap {
                Id = model.Id,
                AttritionId = model.AttritionId,
                DateOfLeaving = model.DateOfLeaving
            };
        }
    }
}