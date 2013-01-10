using System;
using System.Collections.Generic;
using DMP.Repository;

namespace DMP.Services.Interface {
    public interface ITrainingProfileMapService {
        TrainingProfileMap GetTrainingProfileMap(int id);

        void AddTrainingProfileMap(IEnumerable<TrainingProfileMap> maps);

        void UpdateTrainingProfileMap(TrainingProfileMap map);

        void DeleteTrainingProfileMap(int id);

        IEnumerable<TrainingProfileMap> GetAllTrainingProfileMaps();

        IEnumerable<TrainingProfileMap> FindTrainingProfileMaps(Func<TrainingProfileMap, bool> predicate);   
    }
}