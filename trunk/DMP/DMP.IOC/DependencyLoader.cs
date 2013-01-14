using DMP.Repository;
using DMP.Services;
using DMP.Services.Interface;
using DMP.Services.Service;
using Ninject;

namespace DMP.IOC {
    public class DependencyLoader {

        public static IKernel LoadModules(IKernel iKernel) {
            LoadDependency(iKernel);
            return iKernel;
        }
        public static void LoadDependency(IKernel kernel) {
            //Binding Repository
            kernel.Bind<IRepository<Region>>().To<Repository<Region>>();
            kernel.Bind<IRepository<State>>().To<Repository<State>>();
            kernel.Bind<IRepository<Training>>().To<Repository<Training>>();
            kernel.Bind<IRepository<Dealer>>().To<Repository<Dealer>>();
            kernel.Bind<IRepository<DealerManpower>>().To<Repository<DealerManpower>>();
            kernel.Bind<IRepository<ProductCategory>>().To<Repository<ProductCategory>>();
            kernel.Bind<IRepository<Product>>().To<Repository<Product>>();
            kernel.Bind<IRepository<ProductVarient>>().To<Repository<ProductVarient>>();
            kernel.Bind<IRepository<Profile>>().To<Repository<Profile>>();
            kernel.Bind<IRepository<User>>().To<Repository<User>>();
            kernel.Bind<IRepository<Attrition>>().To<Repository<Attrition>>();
            kernel.Bind<IRepository<Competency>>().To<Repository<Competency>>();
            kernel.Bind<IRepository<Month>>().To<Repository<Month>>();
            kernel.Bind<IRepository<CompetencyProfileMap>>().To<Repository<CompetencyProfileMap>>();
            kernel.Bind<IRepository<TrainingProfileMap>>().To<Repository<TrainingProfileMap>>();
            kernel.Bind<IRepository<AttritionProfileMap>>().To<Repository<AttritionProfileMap>>();
            kernel.Bind<IRepository<DealerManpowerTargets>>().To<Repository<DealerManpowerTargets>>();
            kernel.Bind<IRepository<UserDealerMap>>().To<Repository<UserDealerMap>>();
            kernel.Bind<IRepository<Incentive>>().To<Repository<Incentive>>();
            kernel.Bind<IRepository<Target>>().To<Repository<Target>>();
            kernel.Bind<IRepository<SpecialScheme>>().To<Repository<SpecialScheme>>();
            kernel.Bind<IRepository<ManpowerSalary>>().To<Repository<ManpowerSalary>>();
            kernel.Bind<IRepository<SpecialSchemeIncentive>>().To<Repository<SpecialSchemeIncentive>>();
            kernel.Bind<IRepository<UserRegionMap>>().To<Repository<UserRegionMap>>();
            kernel.Bind<IRepository<DsmDseTargetMap>>().To<Repository<DsmDseTargetMap>>();

            //Binding Services
            kernel.Bind<IFormsAuthenticationService>().To<FormsAuthenticationService>();
            kernel.Bind<IMembershipService>().To<AccountMembershipService>();
            kernel.Bind<IMasterService>().To<MasterService>();
            kernel.Bind<IDealerManpowerService>().To<DealerManpowerService>();
            kernel.Bind<IProfileService>().To<ProfileService>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<ICompetencyProfileMapService>().To<CompetencyProfileMapService>();
            kernel.Bind<ITrainingProfileMapService>().To<TrainingProfileMapService>();
            kernel.Bind<IAttritionProfileMapService>().To<AttritionProfileMapService>();
            kernel.Bind<IDealerManpowerTargetService>().To<DealerManpowerTargetService>();
            kernel.Bind<IUserDealerMapService>().To<UserDealerMapService>();
            kernel.Bind<IIncentiveService>().To<IncentiveService>();
            kernel.Bind<ITargetService>().To<TargetService>();
            kernel.Bind<IManpowerSalaryService>().To<ManpowerSalaryService>();
            kernel.Bind<IDsmDseTargetMapService>().To<DsmDseTargetMapService>();
        }
    }
}
