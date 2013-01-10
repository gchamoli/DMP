using System;
using System.Collections.Generic;
using DMP.Repository;

namespace DMP.Services.Interface {
    public interface IAttritionProfileMapService {
        AttritionProfileMap GetAttritionProfileMap(int id);

        void AddAttritionProfileMap(IEnumerable<AttritionProfileMap> maps);

        void UpdateAttritionProfileMap(AttritionProfileMap map);

        void DeleteAttritionProfileMap(int id);

        IEnumerable<AttritionProfileMap> GetAllAttritionProfileMaps();

        IEnumerable<AttritionProfileMap> FindAttritionProfileMaps(Func<AttritionProfileMap, bool> predicate);    
    }
}