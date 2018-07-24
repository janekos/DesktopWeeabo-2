using DesktopWeeabo2.data.db.entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.data.db
{
    public class EntityContext : DbContext
    {
        public EntityContext() : base("name=EntriesDB") { Database.CreateIfNotExists(); }

        public DbSet<AnimeEntity> AnimeEntities { get; set; }
        public DbSet<MangaEntity> MangaEntity { get; set; }
    }
}
