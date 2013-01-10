using System;
using System.Collections.Generic;
using DMP.Repository;

namespace DMP.Services.Interface {
    public interface IDealerManpowerService {
        DealerManpower GetDealerManpower(int id);

        void AddDealerManpower(IEnumerable<DealerManpower> manpowers);

        void UpdateDealerManpower(DealerManpower manpower);

        void DeleteDealerManpower(int id);

        IEnumerable<DealerManpower> GetAllDealerManpowers();

        IEnumerable<DealerManpower> FindDealerManpowers(Func<DealerManpower, bool> predicate);  
    }
}