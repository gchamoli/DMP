using System;
using System.Collections.Generic;
using System.Linq;
using DMP.Repository;
using DMP.Services.Interface;

namespace DMP.Services.Service {
    public class IncentiveService : IIncentiveService {

        private readonly IRepository<Incentive> incentiveRepo;
        private readonly IRepository<SpecialSchemeIncentive> specialIncentiveRepo;

        public IncentiveService(IRepository<Incentive> incentiveRepo, IRepository<SpecialSchemeIncentive> specialIncentiveRepo) {
            this.incentiveRepo = incentiveRepo;
            this.specialIncentiveRepo = specialIncentiveRepo;
        }

        public Incentive GetIncentive(int id) {
            return incentiveRepo.Single(x => x.Id == id);
        }

        public void AddIncentive(IEnumerable<Incentive> incentives) {
            foreach (var incentive in incentives) {
                incentiveRepo.Add(incentive);
            }
            incentiveRepo.SaveChanges();
        }

        public void UpdateIncentive(Incentive incentive) {
            var oldIncentive = GetIncentive(incentive.Id);
            oldIncentive.MonthId = incentive.MonthId;
            oldIncentive.DealerManpowerId = incentive.DealerManpowerId;
            oldIncentive.Dealer = incentive.Dealer;
            oldIncentive.Volvo = incentive.Volvo;
            incentiveRepo.SaveChanges();
        }

        public void DeleteIncentive(int id) {
            var incentive = GetIncentive(id);
            incentiveRepo.Delete(incentive);
            incentiveRepo.SaveChanges();
        }

        public IEnumerable<Incentive> GetAllIncentives() {
            return incentiveRepo.GetAll();
        }

        public IEnumerable<Incentive> FindIncentives(Func<Incentive, bool> predicate) {
            return incentiveRepo.Find(predicate);
        }

        public SpecialSchemeIncentive GetSpecialSchemeIncentive(int id) {
            return specialIncentiveRepo.Single(x => x.Id == id);
        }

        public void AddSpecialSchemeIncentive(IEnumerable<SpecialSchemeIncentive> specialIncentives) {
            foreach (var specialIncentive in specialIncentives) {
                specialIncentiveRepo.Add(specialIncentive);
            }
            specialIncentiveRepo.SaveChanges();
        }

        public void UpdateSpecialSchemeIncentive(SpecialSchemeIncentive specialIncentive) {
            var oldSpecialIncentive = GetSpecialSchemeIncentive(specialIncentive.Id);
            oldSpecialIncentive.IncentiveId = specialIncentive.IncentiveId;
            oldSpecialIncentive.SpecialSchemeId = specialIncentive.SpecialSchemeId;
            oldSpecialIncentive.SpecialIncentive = specialIncentive.SpecialIncentive;
            specialIncentiveRepo.SaveChanges();
        }

        public void DeleteSpecialIncentive(int id) {
            var specialIncentive = GetSpecialSchemeIncentive(id);
            specialIncentiveRepo.Delete(specialIncentive);
            specialIncentiveRepo.SaveChanges();
        }

        public IEnumerable<SpecialSchemeIncentive> GetAllSpecialIncentives() {
            return specialIncentiveRepo.GetAll().Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public IEnumerable<SpecialSchemeIncentive> FindSpecialIncentives(Func<SpecialSchemeIncentive, bool> predicate) {
            return specialIncentiveRepo.Find(predicate).Where(x => x.ObjectInfo.DeletedDate == null);
        }
    }
}