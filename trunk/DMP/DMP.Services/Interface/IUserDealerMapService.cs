using System;
using System.Collections.Generic;
using DMP.Repository;

namespace DMP.Services.Interface {
    public interface IUserDealerMapService {
        UserDealerMap GetUserDealerMap(int id);

        void AddUserDealerMap(IEnumerable<UserDealerMap> maps);

        void UpdateUserDealerMap(UserDealerMap map);

        void DeleteUserDealerMap(int id);

        IEnumerable<UserDealerMap> GetAllUserDealerMaps();

        IEnumerable<UserDealerMap> FindUserDealerMaps(Func<UserDealerMap, bool> predicate); 
    }
}