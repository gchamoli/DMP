using System;
using System.Collections.Generic;
using System.Linq;
using DMP.Repository;
using DMP.Services.Interface;

namespace DMP.Services.Service {
    public class ManpowerSalaryService : IManpowerSalaryService {

        private readonly IRepository<ManpowerSalary> salaryRepo;

        public ManpowerSalaryService(IRepository<ManpowerSalary> salaryRepo) {
            this.salaryRepo = salaryRepo;
        }

        public ManpowerSalary GetManpowerSalary(int id) {
            return salaryRepo.Single(x => x.Id == id);
        }

        public void AddManpowerSalary(IEnumerable<ManpowerSalary> salaries) {
            foreach (var salary in salaries) {
                salaryRepo.Add(salary);
            }
            salaryRepo.SaveChanges();
        }

        public void UpdateManpowerSalary(ManpowerSalary salary) {
            var oldSalary = GetManpowerSalary(salary.Id);
            oldSalary.Salary = salary.Salary;
            oldSalary.DealerManpowerId = salary.DealerManpowerId;
            oldSalary.Description = salary.Description;
        }

        public void DeleteManpowerSalary(int id) {
            var salary = GetManpowerSalary(id);
            salaryRepo.Delete(salary);
            salaryRepo.SaveChanges();
        }

        public IEnumerable<ManpowerSalary> GetAllManpowerSalaries() {
            return salaryRepo.GetAll().Where(x => x.ObjectInfo.DeletedDate == null);
        }

        public IEnumerable<ManpowerSalary> FindManpowerSalaries(Func<ManpowerSalary, bool> predicate) {
            return salaryRepo.Find(predicate).Where(x => x.ObjectInfo.DeletedDate == null);
        }
    }
}