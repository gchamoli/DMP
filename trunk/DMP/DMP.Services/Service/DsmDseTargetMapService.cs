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
        private IMasterService _masterService;

        public DsmDseTargetMapService(IRepository<DsmDseTargetMap> mapRepo, IMasterService masterService)
        {
            this.mapRepo = mapRepo;
            _masterService = masterService;
        }

        public DsmDseTargetMap GetDsmDseTargetMap(int id)
        {
            return mapRepo.Single(x => x.Id == id);
        }

        public void AddDsmDseTargetMap(IEnumerable<DsmDseTargetMap> maps)
        {
            foreach (var dsmDseTargetMap in maps)
            {
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


    }
}