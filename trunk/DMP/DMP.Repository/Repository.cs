using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Objects;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DMP.Repository {
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class {

        private readonly DmpModelContainer db;
        private readonly IObjectSet<TEntity> dbSet;

        public Repository(DmpModelContainer dbEntities) {
            db = dbEntities;
            dbSet = db.CreateObjectSet<TEntity>();
        }

        public IQueryable<TEntity> Fetch() {
            return dbSet;
        }

        public IEnumerable<TEntity> GetAll() {
            return dbSet.AsEnumerable();
        }

        public IEnumerable<TEntity> Find(Func<TEntity, bool> predicate) {
            return dbSet.Where(predicate);
        }

        public int Count(Func<TEntity, bool> predicate) {
            return dbSet.Count(predicate);
        }

        public TEntity Single(Func<TEntity, bool> predicate) {
            return dbSet.SingleOrDefault(predicate);
        }

        public TEntity First(Func<TEntity, bool> predicate) {
            return dbSet.First(predicate);
        }

        public void Add(TEntity entity) {
            dbSet.AddObject(entity);
        }

        public void Delete(TEntity entity) {
            dbSet.DeleteObject(entity);
        }

        public void Attach(TEntity entity) {
            dbSet.Attach(entity);
        }

        public void SaveChanges() {
            db.SaveChanges();
        }
    }
}
