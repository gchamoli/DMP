using System;
using System.Collections.Generic;
using System.Linq;
using DMP.Repository;
using DMP.Services.Interface;

namespace DMP.Services.Service {
    public class UserDealerMapService : IUserDealerMapService {

        private readonly IRepository<UserDealerMap> mapRepo;

        public UserDealerMapService(IRepository<UserDealerMap> mapRepo) {
            this.mapRepo = mapRepo;
        }

        public UserDealerMap GetUserDealerMap(int id) {
            return mapRepo.Single(x => x.Id == id);
        }

        public void AddUserDealerMap(IEnumerable<UserDealerMap> maps) {
            foreach (var trainingProfileMap in maps) {
                mapRepo.Add(trainingProfileMap);
            }
            mapRepo.SaveChanges();
        }

        public void UpdateUserDealerMap(UserDealerMap map) {
            var oldMap = GetUserDealerMap(map.Id);
            oldMap.DealerId = map.DealerId;
            oldMap.UserId = map.UserId;
            oldMap.Description = map.Description;
            mapRepo.SaveChanges();
        }

        public void DeleteUserDealerMap(int id) {
            var map = GetUserDealerMap(id);
            mapRepo.Delete(map);
            mapRepo.SaveChanges();
        }

        public IEnumerable<UserDealerMap> GetAllUserDealerMaps() {
            return mapRepo.GetAll().Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public IEnumerable<UserDealerMap> FindUserDealerMaps(Func<UserDealerMap, bool> predicate) {
            return mapRepo.Find(predicate).Where(x => x.ObjectInfo.DeletedDate == null);
        }
    }
}