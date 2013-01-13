using System;
using System.Collections.Generic;
using DMP.Repository;
using DMP.Services.Interface;
using System.Linq;

namespace DMP.Services.Service
{
    public class DsmDseTargetMapService : IDsmDseTargetMapService
    {

        private readonly IRepository<DsmDseTargetMap> mapRepo;

        public DsmDseTargetMapService(IRepository<DsmDseTargetMap> mapRepo)
        {
            this.mapRepo = mapRepo;
        }

        public DsmDseTargetMap GetDsmDseTargetMap(int id)
        {
            return mapRepo.Single(x => x.Id == id);
        }

        public void AddDsmDseTargetMap(IEnumerable<DsmDseTargetMap> maps)
        {
            foreach (var dsmDseTargetMap in maps)
            {
                var map =
                    mapRepo.Single(
                        x =>
                        x.DsmId == dsmDseTargetMap.DsmId && x.DseId == dsmDseTargetMap.DseId &&
                        x.UserId == dsmDseTargetMap.UserId && x.MonthId == dsmDseTargetMap.MonthId);
                if (map == null)
                    mapRepo.Add(dsmDseTargetMap);
            }
            mapRepo.SaveChanges();
        }

        public void UpdateDsmDseTargetMap(DsmDseTargetMap map)
        {
            var oldMap = GetDsmDseTargetMap(map.Id);
            oldMap.DsmId = map.DsmId;
            oldMap.DseId = map.DseId;
            oldMap.MonthId = map.MonthId;
            oldMap.UserId = map.UserId;
            oldMap.Description = map.Description;
            mapRepo.SaveChanges();
        }

        public void DeleteDsmDseTargetMap(int id)
        {
            var map = GetDsmDseTargetMap(id);
            map.ObjectInfo.DeletedDate = DateTime.Now;
            mapRepo.SaveChanges();
        }

        public IEnumerable<DsmDseTargetMap> GetAllDsmDseTargetMaps()
        {
            return mapRepo.GetAll().Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public IEnumerable<DsmDseTargetMap> FindDsmDseTargetMaps(Func<DsmDseTargetMap, bool> predicate)
        {
            return mapRepo.Find(predicate).Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public IEnumerable<DsmDseTargetMap> GetDsmDseTargetMap(IEnumerable<DsmDseTargetMap> dseList)
        {
            var dseTargetMaps = new List<DsmDseTargetMap>();
            foreach (var dse in dseList)
            {
                var maps = mapRepo.Find(x => x.DseId == dse.DseId && x.MonthId == dse.MonthId && x.UserId == dse.UserId);
                if (maps.Any())
                {
                    dseTargetMaps.Add(maps.First());
                }
            }
            return dseTargetMaps.GroupBy(x => x.DsmId).Select(x => x.ElementAt(0)).ToList();


        }

        public void UpdateDsmDseTargetMap(IEnumerable<DsmDseTargetMap> dsmDseTargetMaps,int userId=0 ,int monthId=0,int dsmId=0,int dseId=0)
        {
            var dseTargetMaps = mapRepo.Fetch().Where(x => (dsmId == 0 || x.DsmId == dsmId) && (monthId == 0 || x.MonthId == monthId) && (userId == 0 || x.UserId == userId) && (dseId == 0 || x.DseId == dseId)).ToList();
            var mapsToBeAdded =
                dsmDseTargetMaps.Where(
                    x =>
                    !dseTargetMaps.Any(
                        y => y.DsmId == x.DsmId && y.DseId == x.DseId && y.MonthId == x.MonthId && y.UserId == x.UserId));
            var mapsToBeDeleted =
                dseTargetMaps.Where(
                    x =>
                    !dsmDseTargetMaps.Any(
                        y => y.DsmId == x.DsmId && y.DseId == x.DseId && y.MonthId == x.MonthId && y.UserId == x.UserId));
            foreach (var map in mapsToBeAdded)
            {
                mapRepo.Add(map);
            }
            foreach (var map in mapsToBeDeleted)
            {
                mapRepo.Delete(map);
            }
            mapRepo.SaveChanges();
        }
    }
}