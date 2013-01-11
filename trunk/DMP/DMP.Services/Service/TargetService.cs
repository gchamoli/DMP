using System;
using System.Collections.Generic;
using System.Linq;
using DMP.Repository;
using DMP.Services.Interface;

namespace DMP.Services.Service {
    public class TargetService : ITargetService {

        private readonly IRepository<Target> targetRepo;

        public TargetService(IRepository<Target> targetRepo) {
            this.targetRepo = targetRepo;
        }

        public Target GetTarget(int id) {
            return targetRepo.Single(x => x.Id == id);
        }

        public void AddTarget(IEnumerable<Target> targets) {
            foreach (var profile in targets) {
                targetRepo.Add(profile);
            }
            targetRepo.SaveChanges();
        }

        public void UpdateTarget(Target target) {
            var oldTarget = GetTarget(target.Id);
            oldTarget.DealerManpowerId = target.DealerManpowerId;
            oldTarget.Description = target.Description;
            oldTarget.MonthId = target.MonthId;
            oldTarget.ProductVarientId = target.ProductVarientId;
            oldTarget.Target1 = target.Target1;
            oldTarget.Target2 = target.Target2;
            targetRepo.SaveChanges();
        }

        public void DeleteTarget(int id) {
            var target = GetTarget(id);
            targetRepo.Delete(target);
            targetRepo.SaveChanges();
        }

        public IEnumerable<Target> GetAllTargets() {
            return targetRepo.GetAll().Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public IEnumerable<Target> FindTargets(Func<Target, bool> predicate) {
            return targetRepo.Find(predicate).Where(x => x.ObjectInfo.DeletedDate == null);
        }
    }
}