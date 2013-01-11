using System;
using System.Collections.Generic;
using System.Linq;
using DMP.Repository;
using DMP.Services.Interface;

namespace DMP.Services.Service {

    public class DealerManpowerTargetService : IDealerManpowerTargetService {

        private readonly IRepository<DealerManpowerTargets> manpowerTargetRepo;

        public DealerManpowerTargetService(IRepository<DealerManpowerTargets> manpowerTargetRepo) {
            this.manpowerTargetRepo = manpowerTargetRepo;
        }

        public DealerManpowerTargets GetDealerManpowerTarget(int id) {
            return manpowerTargetRepo.Single(x => x.Id == id);
        }

        public void AddDealerManpowerTarget(IEnumerable<DealerManpowerTargets> targets) {
            foreach (var target in targets) {
                manpowerTargetRepo.Add(target);
            }
            manpowerTargetRepo.SaveChanges();
        }

        public void UpdateDealerManpowerTarget(DealerManpowerTargets target) {
            var oldTarget = GetDealerManpowerTarget(target.Id);
            oldTarget.MonthId = target.MonthId;
            oldTarget.Planned = target.Planned;
            oldTarget.Description = target.Description;
            oldTarget.UserId = target.UserId;
            oldTarget.DealerId = target.DealerId;
            oldTarget.ProductId = target.ProductId;
            manpowerTargetRepo.SaveChanges();
        }

        public void DeleteDealerManpowerTarget(int id) {
            var target = GetDealerManpowerTarget(id);
            manpowerTargetRepo.Delete(target);
        }

        public IEnumerable<DealerManpowerTargets> GetAllDealerManpowerTargets() {
            return manpowerTargetRepo.GetAll().Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public IEnumerable<DealerManpowerTargets> FindDealerManpowerTargets(Func<DealerManpowerTargets, bool> predicate) {
            var data = manpowerTargetRepo.Find(predicate).Where(x => x.ObjectInfo.DeletedDate == null);
            return data;
        }
    }
}
