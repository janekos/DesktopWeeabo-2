using DesktopWeeabo2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Data {
    class EntriesContext : DbContext {
        public EntriesContext() : base("name=EntriesDB") { Database.SetInitializer<EntriesContext>(null); }

        public DbSet<AnimeModel> AnimeItems { get; set; }
        public DbSet<MangaModel> MangaItems { get; set; }
    }
}
