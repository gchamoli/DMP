﻿using System;
using System.Collections.Generic;
using DMP.Repository;

namespace DMP.Services.Interface {
    public interface IDsmDseTargetMapService {
        DsmDseTargetMap GetDsmDseTargetMap(int id);

        void AddDsmDseTargetMap(IEnumerable<DsmDseTargetMap> maps);

        void UpdateDsmDseTargetMap(DsmDseTargetMap map);

        void DeleteDsmDseTargetMap(int id);

        IEnumerable<DsmDseTargetMap> GetAllDsmDseTargetMaps();

        IEnumerable<DsmDseTargetMap> FindDsmDseTargetMaps(Func<DsmDseTargetMap, bool> predicate);

        IEnumerable<DsmDseTargetMap> GetDsmDseTargetMap(IEnumerable<DsmDseTargetMap> dseList);
    }
}