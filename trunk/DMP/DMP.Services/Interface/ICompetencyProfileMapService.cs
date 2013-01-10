using System;
using System.Collections.Generic;
using DMP.Repository;

namespace DMP.Services.Interface {
    public interface ICompetencyProfileMapService {
        CompetencyProfileMap GetCompetencyProfileMap(int id);

        void AddCompetencyProfileMap(IEnumerable<CompetencyProfileMap> maps);

        void UpdateCompetencyProfileMap(CompetencyProfileMap map);

        void DeleteCompetencyProfileMap(int id);

        IEnumerable<CompetencyProfileMap> GetAllCompetencyProfileMaps();

        IEnumerable<CompetencyProfileMap> FindCompetencyProfileMaps(Func<CompetencyProfileMap, bool> predicate);
    }
}