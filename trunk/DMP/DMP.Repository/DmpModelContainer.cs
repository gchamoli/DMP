using System;
using System.Data;
using System.Linq;

namespace DMP.Repository {
    public partial class DmpModelContainer {

        public new int SaveChanges() {

            foreach (var entity in ObjectStateManager.GetObjectStateEntries(EntityState.Added).Select(x => x.Entity).Where(entry => entry != null).Cast<IObjectInfo>()) {
                entity.ObjectInfo.CreatedDate = DateTime.Now;
                entity.ObjectInfo.ModifiedDate = DateTime.Now;
            }
            foreach (var entity in ObjectStateManager.GetObjectStateEntries(EntityState.Modified).Select(x => x.Entity).Where(entry => entry != null).Cast<IObjectInfo>()) {
                entity.ObjectInfo.ModifiedDate = DateTime.Now;
            }
            return base.SaveChanges();
        }
    }
}
