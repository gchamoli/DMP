using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using DMP.Repository;

namespace DMP.Models {
    public class MasterModel {
    }

    public class RegionModel {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public static Region ToDomainModel(RegionModel model) {
            return new Region {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description
            };
        }

        public static RegionModel FromDomainModel(Region region) {
            return new RegionModel {
                Id = region.Id,
                Name = region.Name,
                Description = region.Description
            };
        }
    }

    public class StateModel {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "Region")]
        public int RegionId { get; set; }

        public string Region { get; set; }

        public static State ToDomainModel(StateModel model) {
            return new State {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                RegionId = model.RegionId
            };
        }

        public static StateModel FromDomainModel(State state) {
            return new StateModel {
                Id = state.Id,
                Name = state.Name,
                Description = state.Description,
                Region = state.Region.Name,
                RegionId = state.RegionId
            };
        }
    }

    public class ProductCategoryModel {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public static ProductCategory ToDomainModel(ProductCategoryModel model) {
            return new ProductCategory {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description
            };
        }

        public static ProductCategoryModel FromDomainModel(ProductCategory category) {
            return new ProductCategoryModel {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }
    }

    public class ProductModel {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsCommon { get; set; }

        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public string Category { get; set; }

        public static Product ToDomainModel(ProductModel model) {
            return new Product {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                ProductCategoryId = model.CategoryId,
                IsCommon = model.IsCommon
            };
        }

        public static ProductModel FromDomainModel(Product product) {
            return new ProductModel {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Category = product.ProductCategory.Name,
                IsCommon = product.IsCommon
            };
        }
    }

    public class ProductVarientModel {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "Segment")]
        public int ProductId { get; set; }

        public string Product { get; set; }

        public static ProductVarient ToDomainModel(ProductVarientModel model) {
            return new ProductVarient {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                ProductId = model.ProductId
            };
        }

        public static ProductVarientModel FromDomainModel(ProductVarient varient) {
            return new ProductVarientModel {
                Id = varient.Id,
                Name = varient.Name,
                Description = varient.Description,
                Product = varient.Product.Name
            };
        }
    }

    public class TrainingModel {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }

        [Required]
        public string Designation { get; set; }

        public string Description { get; set; }

        public static Training ToDomainModel(TrainingModel model) {
            return new Training {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Designation = model.Designation,
                Code = model.Code
            };
        }

        public static TrainingModel FromDomainModel(Training training) {
            return new TrainingModel {
                Id = training.Id,
                Name = training.Name,
                Description = training.Description,
                Code = training.Code,
                Designation = training.Designation
            };
        }
    }

    public class DealerModel {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Display(Name = "State")]
        public int StateId { get; set; }

        public string State { get; set; }

        [Display(Name = "CSM")]
        public int UserId { get; set; }

        public static Dealer ToDomainModel(DealerModel model) {
            return new Dealer {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                StateId = model.StateId
            };
        }

        public static DealerModel FromDomainModel(Dealer dealer) {
            return new DealerModel {
                Id = dealer.Id,
                Name = dealer.Name,
                Description = dealer.Description,
                State = dealer.State.Name,
                StateId = dealer.StateId
            };
        }
    }

    public class UserModel {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9-]+)*\\.([a-z]{2,4})$", ErrorMessage = "Enter a Valid Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Role { get; set; }

        public string Description { get; set; }

        [Display(Name = "Parent")]
        public int? ParentId { get; set; }

        [Display(Name = "Region")]
        public int? RegionId { get; set; }

        public static User ToDomainModel(UserModel model) {
            return new User {
                Id = model.Id,
                Name = model.Name,
                Email = model.Email,
                Role = model.Role,
                Description = model.Description,
                ParentId = model.ParentId ?? 0
            };
        }

        public static UserModel FromDomainModel(User user) {
            return new UserModel {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                Description = user.Description,
                ParentId = user.ParentId,
                RegionId = user.UserRegionMaps != null && user.UserRegionMaps.Any() ? user.UserRegionMaps.First().RegionId : 0
            };
        }
    }

    public class AttritionModel {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Designation { get; set; }

        public string Description { get; set; }

        public static Attrition ToDomainModel(AttritionModel model) {
            return new Attrition {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Designation = model.Designation
            };
        }

        public static AttritionModel FromDomainModel(Attrition attrition) {
            return new AttritionModel {
                Id = attrition.Id,
                Name = attrition.Name,
                Description = attrition.Description,
                Designation = attrition.Designation
            };
        }
    }

    public class CompetencyModel {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Designation { get; set; }

        public string Description { get; set; }

        public static Competency ToDomainModel(CompetencyModel model) {
            return new Competency {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Designation = model.Designation
            };
        }

        public static CompetencyModel FromDomainModel(Competency competency) {
            return new CompetencyModel {
                Id = competency.Id,
                Name = competency.Name,
                Description = competency.Description,
                Designation = competency.Designation
            };
        }
    }

    public class SpecialSchemeModel {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name = "Region")]
        public int? RegionId { get; set; }

        public string Region { get; set; }

        [Display(Name = "State")]
        public int? StateId { get; set; }

        [Display(Name = "Dealer")]
        public int? DealerId { get; set; }

        public int MonthId { get; set; }

        [Required]
        [Display(Name = "Month")]
        public int DisplayMonthId { get; set; }

        public string Month { get; set; }

        public string Description { get; set; }

        public static SpecialScheme ToDomainModel(SpecialSchemeModel model) {
            return new SpecialScheme {
                Id = model.Id,
                Name = model.Name,
                RegionId = model.RegionId ?? 0,
                StateId = model.StateId ?? 0,
                DealerId = model.DealerId ?? 0,
                Description = model.Description,
                MonthId = model.MonthId
            };
        }

        public static SpecialSchemeModel FromDomainModel(SpecialScheme scheme) {
            return new SpecialSchemeModel {
                Id = scheme.Id,
                Name = scheme.Name,
                RegionId = scheme.RegionId,
                Region = scheme.Region.Name,
                StateId = scheme.StateId,
                DealerId = scheme.DealerId,
                Description = scheme.Description,
                MonthId = scheme.MonthId,
                Month = string.Format("{0}-{1}", scheme.Month.Name, scheme.Month.Year)
            };
        }
    }
}