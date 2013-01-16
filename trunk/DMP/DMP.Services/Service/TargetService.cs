using System;
using System.Collections.Generic;
using System.Linq;
using DMP.Repository;
using DMP.Services.Interface;

namespace DMP.Services.Service
{
    public class TargetService : ITargetService
    {

        private readonly IRepository<Target> _targetRepo;
        private readonly IRepository<DsmDseTargetMap> _mapRepo;

        public TargetService(IRepository<Target> targetRepo, IRepository<DsmDseTargetMap> mapRepo)
        {
            _targetRepo = targetRepo;
            _mapRepo = mapRepo;
        }

        public Target GetTarget(int id)
        {
            return _targetRepo.Single(x => x.Id == id);
        }

        public void AddTarget(IEnumerable<Target> targets)
        {
            foreach (var profile in targets)
            {
                _targetRepo.Add(profile);
            }
            _targetRepo.SaveChanges();
        }

        public void AddTarget(Target target)
        {
            _targetRepo.Add(target);
            _targetRepo.SaveChanges();
        }

        public void UpdateTarget(Target target)
        {
            var oldTarget = GetTarget(target.Id);
            oldTarget.DealerManpowerId = target.DealerManpowerId;
            oldTarget.Description = target.Description;
            oldTarget.MonthId = target.MonthId;
            oldTarget.ProductVarientId = target.ProductVarientId;
            oldTarget.Target1 = target.Target1;
            oldTarget.Target2 = target.Target2;
            _targetRepo.SaveChanges();
        }

        public void DeleteTarget(int id)
        {
            var target = GetTarget(id);
            _targetRepo.Delete(target);
            _targetRepo.SaveChanges();
        }

        public IEnumerable<Target> GetAllTargets()
        {
            return _targetRepo.GetAll().Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public IEnumerable<Target> FindTargets(Func<Target, bool> predicate)
        {
            return _targetRepo.Find(predicate).Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public void UpdateDsmTarget(int dsmId, int userId, int monthId)
        {
            var dsmDseMap = _mapRepo.Find(x => x.DsmId == dsmId && x.UserId == userId && x.MonthId == monthId).ToList();
            var dseIds = dsmDseMap.Select(x => x.DseId);
            var targets =
                _targetRepo.Find(x => dseIds.Contains(x.DealerManpowerId) && x.MonthId == monthId).GroupBy(x => x.ProductVarientId);
            foreach (var target in targets)
            {
                var dsmTarget =
                    _targetRepo.First(
                        x => x.MonthId == monthId && x.ProductVarientId == target.Key && x.MonthId == monthId&&x.DealerManpowerId==dsmId);
                if (dsmTarget == null)
                {
                    var dsmTarget1 = new Target();
                    dsmTarget1.MonthId = monthId;
                    dsmTarget1.ProductVarientId = target.Key;
                    dsmTarget1.DealerManpowerId = dsmId;
                    dsmTarget1.Actual = target.Sum(x => x.Actual);
                    dsmTarget1.Target1 = target.Sum(x => x.Target1);
                    dsmTarget1.Target2 = target.Sum(x => x.Target2);
                    AddTarget(dsmTarget1);
                }
                else
                {
                    dsmTarget.Actual = target.Sum(x => x.Actual);
                    dsmTarget.Target1 = target.Sum(x => x.Target1);
                    dsmTarget.Target2 = target.Sum(x => x.Target2);
                }

            }
            _targetRepo.SaveChanges();

        }

        
    }
}