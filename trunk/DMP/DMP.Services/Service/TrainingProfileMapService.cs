using System;
using System.Collections.Generic;
using System.Linq;
using DMP.Repository;
using DMP.Services.Interface;

namespace DMP.Services.Service {
    public class TrainingProfileMapService : ITrainingProfileMapService {

        private readonly IRepository<TrainingProfileMap> mapRepo;

        public TrainingProfileMapService(IRepository<TrainingProfileMap> mapRepo) {
            this.mapRepo = mapRepo;
        }

        public TrainingProfileMap GetTrainingProfileMap(int id) {
            return mapRepo.Single(x => x.Id == id);
        }

        public void AddTrainingProfileMap(IEnumerable<TrainingProfileMap> maps) {
            foreach (var trainingProfileMap in maps) {
                mapRepo.Add(trainingProfileMap);
            }
            mapRepo.SaveChanges();
        }

        public void UpdateTrainingProfileMap(TrainingProfileMap map) {
            var oldMap = GetTrainingProfileMap(map.Id);
            oldMap.LastTrainingDate = map.LastTrainingDate;
            mapRepo.SaveChanges();
        }

        public void DeleteTrainingProfileMap(int id) {
            var map = GetTrainingProfileMap(id);
            mapRepo.Delete(map);
            mapRepo.SaveChanges();
        }

        public IEnumerable<TrainingProfileMap> GetAllTrainingProfileMaps() {
            return mapRepo.GetAll().Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public IEnumerable<TrainingProfileMap> FindTrainingProfileMaps(Func<TrainingProfileMap, bool> predicate) {
            return mapRepo.Find(predicate).Where(x => x.ObjectInfo.DeletedDate == null);
        }
    }
}