using System;
using System.Collections.Generic;
using DMP.Repository;

namespace DMP.Services.Interface {
    public interface IDealerManpowerTargetService {
        DealerManpowerTargets GetDealerManpowerTarget(int id);

        void AddDealerManpowerTarget(IEnumerable<DealerManpowerTargets> targets);

        void UpdateDealerManpowerTarget(DealerManpowerTargets target);

        void DeleteDealerManpowerTarget(int id);

        IEnumerable<DealerManpowerTargets> GetAllDealerManpowerTargets();

        IEnumerable<DealerManpowerTargets> FindDealerManpowerTargets(Func<DealerManpowerTargets, bool> predicate);   
    }
}