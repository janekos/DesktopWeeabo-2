using DesktopWeeabo2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Data {
	// amen
	// https://stackoverflow.com/questions/31780749/unable-to-determine-the-provider-name-for-provider-factory-of-type-system-data
	public class SQLiteConfiguration : DbConfiguration {
		public SQLiteConfiguration() {
			SetProviderFactory("System.Data.SQLite", SQLiteFactory.Instance);
			SetProviderFactory("System.Data.SQLite.EF6", SQLiteProviderFactory.Instance);
			SetProviderServices("System.Data.SQLite", (DbProviderServices)SQLiteProviderFactory.Instance.GetService(typeof(DbProviderServices)));
		}
	}

	class EntriesContext : DbContext {
		public EntriesContext() : base(new SQLiteConnection() { ConnectionString = @"data source=" + GlobalConfig.AppDir + @"\entries.db" }, true) { DbConfiguration.SetConfiguration(new SQLiteConfiguration()); Database.SetInitializer<EntriesContext>(null); }

        public DbSet<AnimeModel> AnimeItems { get; set; }
        public DbSet<MangaModel> MangaItems { get; set; }
    }
}
