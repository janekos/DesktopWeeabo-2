using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.data.db
{
    class ObjectContext : DbContext
    {
        public ObjectContext() : base(new SQLiteConnection() { ConnectionString = ""; })
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
