using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Data.Objects;
using System.Linq;
using System.Text;

namespace DMP.Repository {
    class AppDb : ObjectContext {

        public AppDb(EntityConnection connection) : base(connection) {}
        public AppDb(string connectionString) : base(connectionString) {}
        protected AppDb(string connectionString, string defaultContainerName) : base(connectionString, defaultContainerName) {}
        protected AppDb(EntityConnection connection, string defaultContainerName) : base(connection, defaultContainerName) {}

    }
}
