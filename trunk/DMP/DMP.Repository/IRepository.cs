using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMP.Repository {
    public interface IRepository<T> where T : class {
        IQueryable<T> Fetch();
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T, bool> predicate);
        int Count(Func<T, bool> predicate);
        T Single(Func<T, bool> predicate);
        T First(Func<T, bool> predicate);
        void Add(T entity);
        void Delete(T entity);
        void Attach(T entity);
        void SaveChanges();
    }
}
