using System;
using System.Data;
using System.Linq;

namespace DMP.Repository {
    public partial class DmpModelContainer {

        public new int SaveChanges() {

            foreach (var entity in ObjectStateManager.GetObjectStateEntries(EntityState.Added).Select(x => x.Entity).Where(entry => entry != null).Cast<IObjectInfo>()) {
                entity.ObjectInfo.CreatedDate = DateTime.UtcNow;
                entity.ObjectInfo.ModifiedDate = DateTime.UtcNow;
            }
            foreach (var entity in ObjectStateManager.GetObjectStateEntries(EntityState.Modified).Select(x => x.Entity).Where(entry => entry != null).Cast<IObjectInfo>()) {
                entity.ObjectInfo.ModifiedDate = DateTime.UtcNow;
            }
            return base.SaveChanges();
        }
    }
}
