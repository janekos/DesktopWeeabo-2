using DesktopWeeabo2.Core;
using DesktopWeeabo2.Infrastructure.Properties;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Infrastructure.Database {
	public static class DbActions {
		public static void InitDB() {
			string dbPath = GlobalConfig.AppDir + "\\entries.db";

			SQLiteConnection.CreateFile(dbPath);

			using (var db = new SQLiteConnection("Data Source=" + dbPath)) {

				db.Open();

				new SQLiteCommand(Resources.ResourceManager.GetString("CreateAnimeTable"), db).ExecuteNonQuery();
				new SQLiteCommand(Resources.ResourceManager.GetString("CreateMangaTable"), db).ExecuteNonQuery();

				db.Close();
			}
		}

		public static async Task<bool> WakeDB() {
			using (var db = new EntriesContext()) {
				await db.AnimeItems.FindAsync(0);
			}
			return true;
		}
	}
}
