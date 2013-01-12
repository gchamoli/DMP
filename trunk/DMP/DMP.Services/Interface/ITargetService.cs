using System;
using System.Collections.Generic;
using DMP.Repository;

namespace DMP.Services.Interface
{
    public interface ITargetService
    {
        Target GetTarget(int id);

        void AddTarget(IEnumerable<Target> targets);

        void AddTarget(Target target);

        void UpdateTarget(Target target);

        void DeleteTarget(int id);

        IEnumerable<Target> GetAllTargets();

        IEnumerable<Target> FindTargets(Func<Target, bool> predicate);
        void UpdateDsmTarget(int dsmId, int userId, int monthId);
        }
}