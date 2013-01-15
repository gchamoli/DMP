using System;
using System.Collections.Generic;
using System.Linq;
using DMP.Repository;
using DMP.Services.Interface;

namespace DMP.Services.Service {
    public class MasterService : IMasterService {

        private readonly IRepository<Region> regionRepo;
        private readonly IRepository<State> stateRepo;
        private readonly IRepository<ProductCategory> categoryRepo;
        private readonly IRepository<Product> productRepo;
        private readonly IRepository<ProductVarient> varientRepo;
        private readonly IRepository<Training> trainingRepo;
        private readonly IRepository<Dealer> dealerRepo;
        private readonly IRepository<Attrition> attritionRepo;
        private readonly IRepository<Competency> competencyRepo;
        private readonly IRepository<Month> monthRepo;
        private readonly IRepository<SpecialScheme> specialSchemeRepo;
        private readonly IRepository<UserRegionMap> userRegionMapRepo;

        public MasterService(IRepository<Region> regionRepo, IRepository<State> stateRepo, IRepository<ProductCategory> categoryRepo, IRepository<Product> productRepo, IRepository<ProductVarient> varientRepo, IRepository<Training> trainingRepo, IRepository<Dealer> dealerRepo, IRepository<Attrition> attritionRepo, IRepository<Competency> competencyRepo, IRepository<Month> monthRepo, IRepository<SpecialScheme> specialSchemeRepo, IRepository<UserRegionMap> userRegionMapRepo) {
            this.regionRepo = regionRepo;
            this.stateRepo = stateRepo;
            this.categoryRepo = categoryRepo;
            this.productRepo = productRepo;
            this.varientRepo = varientRepo;
            this.trainingRepo = trainingRepo;
            this.dealerRepo = dealerRepo;
            this.attritionRepo = attritionRepo;
            this.competencyRepo = competencyRepo;
            this.monthRepo = monthRepo;
            this.specialSchemeRepo = specialSchemeRepo;
            this.userRegionMapRepo = userRegionMapRepo;
        }

        #region Region

        public Region GetRegion(int id) {
            return regionRepo.Single(x => x.Id == id);
        }

        public void AddRegions(IEnumerable<Region> regions) {
            foreach (var region in regions) {
                regionRepo.Add(region);
            }
            regionRepo.SaveChanges();
        }

        public void UpdateRegion(Region region) {
            var oldRegion = GetRegion(region.Id);
            oldRegion.Name = region.Name;
            oldRegion.Description = region.Description;
            regionRepo.SaveChanges();
        }

        public void DeleteRegion(int id) {
            var region = GetRegion(id);
            region.ObjectInfo.DeletedDate = DateTime.Now;
            regionRepo.SaveChanges();
        }

        public IEnumerable<Region> GetAllRegions() {
            return regionRepo.GetAll().Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public IEnumerable<Region> FindRegions(Func<Region, bool> predicate) {
            return regionRepo.Find(predicate).Where(x => x.ObjectInfo.DeletedDate == null);
        }

        #endregion

        #region State

        public State GetState(int id) {
            return stateRepo.Single(x => x.Id == id);
        }

        public void AddStates(IEnumerable<State> states) {
            foreach (var state in states) {
                stateRepo.Add(state);
            }
            stateRepo.SaveChanges();
        }

        public void UpdateState(State state) {
            var oldState = GetState(state.Id);
            oldState.Name = state.Name;
            oldState.Region = state.Region;
            oldState.Description = state.Description;
            stateRepo.SaveChanges();
        }

        public void DeleteState(int id) {
            var state = GetState(id);
            state.ObjectInfo.DeletedDate = DateTime.Now;
            stateRepo.SaveChanges();
        }

        public IEnumerable<State> GetAllStates() {
            return stateRepo.GetAll().Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public IEnumerable<State> FindStates(Func<State, bool> predicate) {
            return stateRepo.Find(predicate).Where(x => x.ObjectInfo.DeletedDate == null);
        }

        #endregion

        #region ProductCategory

        public ProductCategory GetProductCategory(int id) {
            return categoryRepo.Single(x => x.Id == id);
        }

        public void AddCategory(IEnumerable<ProductCategory> categories) {
            foreach (var category in categories) {
                categoryRepo.Add(category);
            }
            categoryRepo.SaveChanges();
        }

        public void UpdateCategory(ProductCategory category) {
            var oldCategory = GetProductCategory(category.Id);
            oldCategory.Name = category.Name;
            oldCategory.Description = category.Description;
            categoryRepo.SaveChanges();
        }

        public void DeleteCategory(int id) {
            var category = GetProductCategory(id);
            category.ObjectInfo.DeletedDate = DateTime.Now;
            categoryRepo.SaveChanges();
        }

        public IEnumerable<ProductCategory> GetAllCategories() {
            return categoryRepo.GetAll().Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public IEnumerable<ProductCategory> FindCategory(Func<ProductCategory, bool> predicate) {
            return categoryRepo.Find(predicate).Where(x => x.ObjectInfo.DeletedDate == null);
        }

        #endregion

        #region Product

        public Product GetProduct(int id) {
            return productRepo.Single(x => x.Id == id);
        }

        public void AddProduct(IEnumerable<Product> products) {
            foreach (var product in products) {
                productRepo.Add(product);
            }
            productRepo.SaveChanges();
        }

        public void UpdateProduct(Product product) {
            var oldProduct = GetProduct(product.Id);
            oldProduct.Name = product.Name;
            oldProduct.Description = product.Description;
            oldProduct.IsCommon = product.IsCommon;
            oldProduct.ProductCategoryId = product.ProductCategoryId;
            productRepo.SaveChanges();
        }

        public void DeleteProduct(int id) {
            var product = GetProduct(id);
            product.ObjectInfo.DeletedDate = DateTime.Now;
            productRepo.SaveChanges();
        }

        public IEnumerable<Product> GetAllProducts() {
            return productRepo.GetAll().Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public IEnumerable<Product> FindProducts(Func<Product, bool> predicate) {
            return productRepo.Find(predicate).Where(x => x.ObjectInfo.DeletedDate == null);
        }

        #endregion

        #region ProductVarient

        public ProductVarient GetProductVarient(int id) {
            return varientRepo.Single(x => x.Id == id);
        }

        public void AddProductVarient(IEnumerable<ProductVarient> varients) {
            foreach (var varient in varients) {
                varientRepo.Add(varient);
            }
            varientRepo.SaveChanges();
        }

        public void UpdateProductVarient(ProductVarient varient) {
            var oldVarient = GetProductVarient(varient.Id);
            oldVarient.Name = varient.Name;
            oldVarient.Description = varient.Description;
            oldVarient.ProductId = varient.ProductId;
            varientRepo.SaveChanges();
        }

        public void DeleteProductVarient(int id) {
            var varient = GetProductVarient(id);
            varient.ObjectInfo.DeletedDate = DateTime.Now;
            varientRepo.SaveChanges();
        }

        public IEnumerable<ProductVarient> GetAllProductVarients() {
            return varientRepo.GetAll().Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public IEnumerable<ProductVarient> FindProductVarient(Func<ProductVarient, bool> predicate) {
            return varientRepo.Find(predicate).Where(x => x.ObjectInfo.DeletedDate == null);
        }

        #endregion

        #region Training

        public Training GetTraining(int id) {
            return trainingRepo.Single(x => x.Id == id);
        }

        public void AddTraining(IEnumerable<Training> trainings) {
            foreach (var training in trainings) {
                trainingRepo.Add(training);
            }
            trainingRepo.SaveChanges();
        }

        public void UpdateTraining(Training training) {
            var oldTraining = GetTraining(training.Id);
            oldTraining.Name = training.Name;
            oldTraining.Code = training.Code;
            oldTraining.Description = training.Description;
            oldTraining.Designation = training.Designation;
            trainingRepo.SaveChanges();
        }

        public void DeleteTraining(int id) {
            var training = GetTraining(id);
            training.ObjectInfo.DeletedDate = DateTime.Now;
            trainingRepo.SaveChanges();
        }

        public IEnumerable<Training> GetAllTrainings() {
            return trainingRepo.GetAll().Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public IEnumerable<Training> FindTraining(Func<Training, bool> predicate) {
            return trainingRepo.Find(predicate).Where(x => x.ObjectInfo.DeletedDate == null);
        }

        #endregion

        #region Dealer

        public Dealer GetDealer(int id) {
            return dealerRepo.Single(x => x.Id == id);
        }

        public void AddDealer(IEnumerable<Dealer> dealers) {
            foreach (var dealer in dealers) {
                dealerRepo.Add(dealer);
            }
            dealerRepo.SaveChanges();
        }

        public void UpdateDealer(Dealer dealer) {
            var oldDealer = GetDealer(dealer.Id);
            oldDealer.Name = dealer.Name;
            oldDealer.Description = dealer.Description;
            oldDealer.StateId = dealer.StateId;
            dealerRepo.SaveChanges();
        }

        public void DeleteDealer(int id) {
            var dealer = GetDealer(id);
            dealer.ObjectInfo.DeletedDate = DateTime.Now;
            dealerRepo.SaveChanges();
        }

        public IEnumerable<Dealer> GetAllDealers() {
            return dealerRepo.GetAll().Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public IEnumerable<Dealer> FindDealers(Func<Dealer, bool> predicate) {
            return dealerRepo.Find(predicate).Where(x => x.ObjectInfo.DeletedDate == null);
        }

        #endregion

        #region Attrition

        public Attrition GetAttrition(int id) {
            return attritionRepo.Single(x => x.Id == id);
        }

        public void AddAttritions(IEnumerable<Attrition> attritions) {
            foreach (var attrition in attritions) {
                attritionRepo.Add(attrition);
            }
            attritionRepo.SaveChanges();
        }

        public void UpdateAttrition(Attrition attrition) {
            var oldAttrition = GetAttrition(attrition.Id);
            oldAttrition.Name = attrition.Name;
            oldAttrition.Description = attrition.Description;
            oldAttrition.Designation = attrition.Designation;
            attritionRepo.SaveChanges();
        }

        public void DeleteAttrition(int id) {
            var attrition = GetRegion(id);
            attrition.ObjectInfo.DeletedDate = DateTime.Now;
            regionRepo.SaveChanges();
        }

        public IEnumerable<Attrition> GetAllAttritions() {
            return attritionRepo.GetAll().Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public IEnumerable<Attrition> FindAttritions(Func<Attrition, bool> predicate) {
            return attritionRepo.Find(predicate).Where(x => x.ObjectInfo.DeletedDate == null);
        }

        #endregion

        #region Competency

        public Competency GetCompetency(int id) {
            return competencyRepo.Single(x => x.Id == id);
        }

        public void AddCompetencies(IEnumerable<Competency> competencies) {
            foreach (var competency in competencies) {
                competencyRepo.Add(competency);
            }
            competencyRepo.SaveChanges();
        }

        public void UpdateCompetency(Competency competency) {
            var oldCompetency = GetCompetency(competency.Id);
            oldCompetency.Name = competency.Name;
            oldCompetency.Description = competency.Description;
            oldCompetency.Designation = competency.Designation;
            competencyRepo.SaveChanges();
        }

        public void DeleteCompetency(int id) {
            var competency = GetCompetency(id);
            competency.ObjectInfo.DeletedDate = DateTime.Now;
            competencyRepo.SaveChanges();
        }

        public IEnumerable<Competency> GetAllCompetencies() {
            return competencyRepo.GetAll().Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public IEnumerable<Competency> FindCompetencies(Func<Competency, bool> predicate) {
            return competencyRepo.Find(predicate).Where(x => x.ObjectInfo.DeletedDate == null);
        }

        #endregion

        #region Month

        public Month GetMonth(int id) {
            return monthRepo.Single(x => x.Id == id);
        }

        public void AddMonths(IEnumerable<Month> months) {
            foreach (var month in months) {
                monthRepo.Add(month);
            }
            monthRepo.SaveChanges();
        }

        public void UpdateMonth(Month month) {
            var oldMonth = GetMonth(month.Id);
            oldMonth.Name = month.Name;
            oldMonth.Year = month.Year;
            oldMonth.Description = month.Description;
            monthRepo.SaveChanges();
        }

        public void DeleteMonth(int id) {
            var month = GetMonth(id);
            month.ObjectInfo.DeletedDate = DateTime.Now;
            monthRepo.SaveChanges();
        }

        public IEnumerable<Month> GetAllMonths() {
            return monthRepo.GetAll().Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public IEnumerable<Month> FindMonths(Func<Month, bool> predicate) {
            return monthRepo.Find(predicate).Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public int GetMonthIndex(string month) {
            var months = new[] { "January", "February", "March", "April", "May", "June", "July", "August", "Sepetember", "October", "November", "December" };
            for (var m = 0; m < months.Length; m++) {
                if (month.ToLower() == months[m].ToLower())
                    return m + 1;
            }
            return 0;
        }

        public string GetMonthName(int index) {
            var months = new[] { "January", "February", "March", "April", "May", "June", "July", "August", "Sepetember", "October", "November", "December" };
            return months[index - 1];
        }

        private DateTime? FindDate(string month, int year) {
            var index = GetMonthIndex(month);
            if (index > 0) {
                var date = new DateTime(year, index, 1);
                return date;
            }
            return null;
        }

        public Month FindAndCreateMonth(string monthName, int year) {
            var months = FindMonths(x => x.Name.ToLower() == monthName.ToLower() && x.Year == year).ToList();
            if (months.Any()) {
                return months.First();
            }
            var totalEmployees = 0;
            var date = FindDate(monthName, year);
            if (date.HasValue) {
                var previousMonth = date.Value.AddMonths(-1);
                var preMonths = FindMonths(x => x.Name.ToLower() == previousMonth.ToString("MMMM").ToLower() && x.Year == previousMonth.Year).ToList();
                if (preMonths.Any()) {
                    totalEmployees = preMonths.First().NumberOfEmployee;
                }
            }
            var month = new Month {
                Id = 0,
                Name = monthName,
                Year = year,
                NumberOfEmployee = totalEmployees
            };
            monthRepo.Add(month);
            monthRepo.SaveChanges();
            return month;
        }

        public IEnumerable<Month> GetAllFinancialMonths(string month, int year) {
            var list = new List<Month>();
            var index = GetMonthIndex(month);
            for (var i = 1; i <= 12; i++) {
                var tempYear = index >= 4 ? (i < 4 ? year + 1 : year) : (i < 4 ? year : year - 1);
                list.Add(FindAndCreateMonth(GetMonthName(i), tempYear));
            }
            return list.OrderBy(x => x.Year);
        }

        public void UpdateTotalEmployee(int monthId, int employeeCount) {
            var month = GetMonth(monthId);
            month.NumberOfEmployee = month.NumberOfEmployee + employeeCount;
            monthRepo.SaveChanges();
        }

        public double FindAverageEmployee(DateTime date) {
            var result = 0;
            var total = 0;
            var count = 0;
            var previousMonth = date.AddMonths(-1);
            var months = FindMonths(x => x.Year <= previousMonth.Year);
            if (months.Any()) {
                foreach (var month in months) {
                    var tempdate = new DateTime(month.Year, GetMonthIndex(month.Name), 1);
                    if (tempdate <= previousMonth) {
                        total += month.NumberOfEmployee;
                        count++;
                    }
                }
                if (count > 0) {
                    result = (total / count);
                }
            }
            return result;
        }

        #endregion

        #region SpecialScheme

        public SpecialScheme GetSpecialScheme(int id) {
            return specialSchemeRepo.Single(x => x.Id == id);
        }

        public void AddSpecialScheme(IEnumerable<SpecialScheme> specialSchemes) {
            foreach (var specialScheme in specialSchemes) {
                specialSchemeRepo.Add(specialScheme);
            }
            specialSchemeRepo.SaveChanges();
        }

        public void UpdateSpecialScheme(SpecialScheme specialScheme) {
            var oldScheme = GetSpecialScheme(specialScheme.Id);
            oldScheme.RegionId = specialScheme.RegionId;
            oldScheme.StateId = specialScheme.StateId;
            oldScheme.DealerId = specialScheme.DealerId;
            oldScheme.Name = specialScheme.Name;
            oldScheme.MonthId = specialScheme.MonthId;
            oldScheme.Description = specialScheme.Description;
            specialSchemeRepo.SaveChanges();
        }

        public void DeleteSpecialScheme(int id) {
            var scheme = GetSpecialScheme(id);
            scheme.ObjectInfo.DeletedDate = DateTime.Now;
            specialSchemeRepo.SaveChanges();
        }

        public IEnumerable<SpecialScheme> GetSpecialSchemes() {
            return specialSchemeRepo.GetAll().Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public IEnumerable<SpecialScheme> FindSpecialSchemes(Func<SpecialScheme, bool> predicate) {
            return specialSchemeRepo.Find(predicate).Where(x => x.ObjectInfo.DeletedDate == null);
        }

        #endregion

        #region UserRegionMap

        public UserRegionMap GetUserRegionMap(int id) {
            return userRegionMapRepo.Single(x => x.Id == id);
        }

        public void AddUserRegionMap(IEnumerable<UserRegionMap> maps) {
            foreach (var userRegionMap in maps) {
                userRegionMapRepo.Add(userRegionMap);
            }
            userRegionMapRepo.SaveChanges();
        }

        public void UpdateUserRegionMap(UserRegionMap map) {
            var oldMap = GetUserRegionMap(map.Id);
            oldMap.RegionId = map.RegionId;
            oldMap.UserId = map.UserId;
            oldMap.Description = map.Description;
            userRegionMapRepo.SaveChanges();
        }

        public void DeleteUserRegionMap(int id) {
            var map = GetUserRegionMap(id);
            map.ObjectInfo.DeletedDate = DateTime.Now;
            userRegionMapRepo.SaveChanges();
        }

        public IEnumerable<UserRegionMap> GetAllUserRegionMaps() {
            return userRegionMapRepo.GetAll().Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public IEnumerable<UserRegionMap> FindUserRegionMaps(Func<UserRegionMap, bool> predicate) {
            return userRegionMapRepo.Find(predicate).Where(x => x.ObjectInfo.DeletedDate == null);
        }

        #endregion

    }
}