using System;
using System.Collections.Generic;
using DMP.Repository;

namespace DMP.Services.Interface {
    public interface IMasterService {

        #region Region

        Region GetRegion(int id);

        void AddRegions(IEnumerable<Region> regions);

        void UpdateRegion(Region region);

        void DeleteRegion(int id);

        IEnumerable<Region> GetAllRegions();

        IEnumerable<Region> FindRegions(Func<Region, bool> predicate);

        #endregion

        #region State

        State GetState(int id);

        void AddStates(IEnumerable<State> states);

        void UpdateState(State state);

        void DeleteState(int id);

        IEnumerable<State> GetAllStates();

        IEnumerable<State> FindStates(Func<State, bool> predicate);

        #endregion

        #region Category

        ProductCategory GetProductCategory(int id);

        void AddCategory(IEnumerable<ProductCategory> categories);

        void UpdateCategory(ProductCategory category);

        void DeleteCategory(int id);

        IEnumerable<ProductCategory> GetAllCategories();

        IEnumerable<ProductCategory> FindCategory(Func<ProductCategory, bool> predicate);

        #endregion

        #region Product

        Product GetProduct(int id);

        void AddProduct(IEnumerable<Product> products);

        void UpdateProduct(Product product);

        void DeleteProduct(int id);

        IEnumerable<Product> GetAllProducts();

        IEnumerable<Product> FindProducts(Func<Product, bool> predicate);

        #endregion

        #region ProductVarient

        ProductVarient GetProductVarient(int id);

        void AddProductVarient(IEnumerable<ProductVarient> varients);

        void UpdateProductVarient(ProductVarient varient);

        void DeleteProductVarient(int id);

        IEnumerable<ProductVarient> GetAllProductVarients();

        IEnumerable<ProductVarient> FindProductVarient(Func<ProductVarient, bool> predicate);

        #endregion

        #region Training

        Training GetTraining(int id);

        void AddTraining(IEnumerable<Training> trainings);

        void UpdateTraining(Training training);

        void DeleteTraining(int id);

        IEnumerable<Training> GetAllTrainings();

        IEnumerable<Training> FindTraining(Func<Training, bool> predicate);

        #endregion

        #region Dealer

        Dealer GetDealer(int id);

        void AddDealer(IEnumerable<Dealer> dealers);

        void UpdateDealer(Dealer dealer);

        void DeleteDealer(int id);

        IEnumerable<Dealer> GetAllDealers();

        IEnumerable<Dealer> FindDealers(Func<Dealer, bool> predicate);

        #endregion

        #region Competency

        Competency GetCompetency(int id);

        void AddCompetencies(IEnumerable<Competency> competencies);

        void UpdateCompetency(Competency competency);

        void DeleteCompetency(int id);

        IEnumerable<Competency> GetAllCompetencies();

        IEnumerable<Competency> FindCompetencies(Func<Competency, bool> predicate);

        #endregion

        #region Attrition

        Attrition GetAttrition(int id);

        void AddAttritions(IEnumerable<Attrition> attritions);

        void UpdateAttrition(Attrition attrition);

        void DeleteAttrition(int id);

        IEnumerable<Attrition> GetAllAttritions();

        IEnumerable<Attrition> FindAttritions(Func<Attrition, bool> predicate);

        #endregion

        #region Month

        Month GetMonth(int id);

        void AddMonths(IEnumerable<Month> months);

        void UpdateMonth(Month month);

        void DeleteMonth(int id);

        IEnumerable<Month> GetAllMonths();

        IEnumerable<Month> FindMonths(Func<Month, bool> predicate);

        Month FindAndCreateMonth(string monthName, int year);

        void UpdateTotalEmployee(int monthId, int employeeCount);

        double FindAverageEmployee(DateTime date);

        int GetMonthIndex(string month);

        string GetMonthName(int index);

        IEnumerable<Month> GetAllFinancialMonths(string month, int year);

        #endregion

        #region SpecialScheme

        SpecialScheme GetSpecialScheme(int id);

        void AddSpecialScheme(IEnumerable<SpecialScheme> specialSchemes);

        void UpdateSpecialScheme(SpecialScheme specialScheme);

        void DeleteSpecialScheme(int id);

        IEnumerable<SpecialScheme> GetSpecialSchemes();

        IEnumerable<SpecialScheme> FindSpecialSchemes(Func<SpecialScheme, bool> predicate);

        #endregion

        #region UserRegionMap

        UserRegionMap GetUserRegionMap(int id);

        void AddUserRegionMap(IEnumerable<UserRegionMap> maps);

        void UpdateUserRegionMap(UserRegionMap map);

        void DeleteUserRegionMap(int id);

        IEnumerable<UserRegionMap> GetAllUserRegionMaps();

        IEnumerable<UserRegionMap> FindUserRegionMaps(Func<UserRegionMap, bool> predicate);

        #endregion
    }
}