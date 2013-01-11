using System;
using System.Collections.Generic;
using System.Linq;
using DMP.Repository;
using DMP.Services.Interface;

namespace DMP.Services.Service {
    public class CompetencyProfileMapService : ICompetencyProfileMapService {

        private readonly IRepository<CompetencyProfileMap> mapRepo;
        public CompetencyProfileMapService(IRepository<CompetencyProfileMap> mapRepo) {
            this.mapRepo = mapRepo;
        }

        public CompetencyProfileMap GetCompetencyProfileMap(int id) {
            return mapRepo.Single(x => x.Id == id);
        }

        public void AddCompetencyProfileMap(IEnumerable<CompetencyProfileMap> maps) {
            foreach (var competencyProfileMap in maps) {
                mapRepo.Add(competencyProfileMap);
            }
            mapRepo.SaveChanges();
        }

        public void UpdateCompetencyProfileMap(CompetencyProfileMap map) {
            var oldMap = GetCompetencyProfileMap(map.Id);
            oldMap.Score = map.Score;
            mapRepo.SaveChanges();
        }

        public void DeleteCompetencyProfileMap(int id) {
            var map = GetCompetencyProfileMap(id);
            mapRepo.Delete(map);
            mapRepo.SaveChanges();
        }

        public IEnumerable<CompetencyProfileMap> GetAllCompetencyProfileMaps() {
            return mapRepo.GetAll().Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public IEnumerable<CompetencyProfileMap> FindCompetencyProfileMaps(Func<CompetencyProfileMap, bool> predicate) {
            return mapRepo.Find(predicate).Where(x => x.ObjectInfo.DeletedDate == null);
        }
    }
}