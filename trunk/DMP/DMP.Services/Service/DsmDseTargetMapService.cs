using System;
using System.Collections.Generic;
using DMP.Repository;
using DMP.Services.Interface;

namespace DMP.Services.Service {
    public class DsmDseTargetMapService : IDsmDseTargetMapService {

        private readonly IRepository<DsmDseTargetMap> mapRepo;

        public DsmDseTargetMapService(IRepository<DsmDseTargetMap> mapRepo) {
            this.mapRepo = mapRepo;
        }

        public DsmDseTargetMap GetDsmDseTargetMap(int id) {
            throw new NotImplementedException();
        }

        public void AddDsmDseTargetMap(IEnumerable<DsmDseTargetMap> maps) {
            throw new NotImplementedException();
        }

        public void UpdateDsmDseTargetMap(DsmDseTargetMap map) {
            throw new NotImplementedException();
        }

        public void DeleteDsmDseTargetMap(int id) {
            throw new NotImplementedException();
        }

        public IEnumerable<DsmDseTargetMap> GetAllDsmDseTargetMaps() {
            throw new NotImplementedException();
        }

        public IEnumerable<DsmDseTargetMap> FindDsmDseTargetMaps(Func<DsmDseTargetMap, bool> predicate) {
            throw new NotImplementedException();
        }
    }
}