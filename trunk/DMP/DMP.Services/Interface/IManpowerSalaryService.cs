using System;
using System.Collections.Generic;
using DMP.Repository;

namespace DMP.Services.Interface {
    public interface IManpowerSalaryService {
        ManpowerSalary GetManpowerSalary(int id);

        void AddManpowerSalary(IEnumerable<ManpowerSalary> salaries);

        void UpdateManpowerSalary(ManpowerSalary salary);

        void DeleteManpowerSalary(int id);

        IEnumerable<ManpowerSalary> GetAllManpowerSalaries();

        IEnumerable<ManpowerSalary> FindManpowerSalaries(Func<ManpowerSalary, bool> predicate);  
    }
}