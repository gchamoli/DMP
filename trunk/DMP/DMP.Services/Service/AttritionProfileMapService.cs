using System;
using System.Collections.Generic;
using System.Linq;
using DMP.Repository;
using DMP.Services.Interface;

namespace DMP.Services.Service {
    public class AttritionProfileMapService : IAttritionProfileMapService {

        private readonly IRepository<AttritionProfileMap> mapRepo;

        public AttritionProfileMapService(IRepository<AttritionProfileMap> mapRepo) {
            this.mapRepo = mapRepo;
        }

        public AttritionProfileMap GetAttritionProfileMap(int id) {
            return mapRepo.Single(x => x.Id == id);
        }

        public void AddAttritionProfileMap(IEnumerable<AttritionProfileMap> maps) {
            foreach (var attritionProfileMap in maps) {
                mapRepo.Add(attritionProfileMap);
            }
            mapRepo.SaveChanges();
        }

        public void UpdateAttritionProfileMap(AttritionProfileMap map) {
            var oldAttrition = GetAttritionProfileMap(map.Id);
            oldAttrition.AttritionId = map.AttritionId;
            oldAttrition.DateOfLeaving = map.DateOfLeaving;
            mapRepo.SaveChanges();
        }

        public void DeleteAttritionProfileMap(int id) {
            var map = GetAttritionProfileMap(id);
            mapRepo.Delete(map);
            mapRepo.SaveChanges();
        }

        public IEnumerable<AttritionProfileMap> GetAllAttritionProfileMaps() {
            return mapRepo.GetAll().Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public IEnumerable<AttritionProfileMap> FindAttritionProfileMaps(Func<AttritionProfileMap, bool> predicate) {
            return mapRepo.Find(predicate).Where(x => x.ObjectInfo.DeletedDate == null);
        }
    }
}