using System;
using System.Collections.Generic;
using DMP.Repository;

namespace DMP.Services.Interface {
    public interface IIncentiveService {
        Incentive GetIncentive(int id);

        void AddIncentive(IEnumerable<Incentive> incentives);

        void UpdateIncentive(Incentive incentive);

        void DeleteIncentive(int id);

        IEnumerable<Incentive> GetAllIncentives();

        IEnumerable<Incentive> FindIncentives(Func<Incentive, bool> predicate);

        SpecialSchemeIncentive GetSpecialSchemeIncentive(int id);

        void AddSpecialSchemeIncentive(IEnumerable<SpecialSchemeIncentive> specialIncentives);

        void UpdateSpecialSchemeIncentive(SpecialSchemeIncentive specialIncentives);

        void DeleteSpecialIncentive(int id);

        IEnumerable<SpecialSchemeIncentive> GetAllSpecialIncentives();

        IEnumerable<SpecialSchemeIncentive> FindSpecialIncentives(Func<SpecialSchemeIncentive, bool> predicate);

    }
}