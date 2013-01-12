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
                var map = mapRepo.Single(x => x.DseId == dse.DseId && x.MonthId == dse.MonthId && x.UserId == dse.UserId);
                dseTargetMaps.Add(map);
            }
            return dseTargetMaps.GroupBy(x => x.DsmId).Select(x => x.ElementAt(0)).ToList();


        }
    }
}