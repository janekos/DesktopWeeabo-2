using DesktopWeeabo2.Properties;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Data {
    public static class InitAppData {

        public static void Init() {
            CheckFiles();
            SetDataDir();
        }

		public static async Task<bool> WakeDB() {
			using(var db = new EntriesContext()) {
				await db.AnimeItems.FindAsync(0);
			}
			return true;
		}

		private static void CheckFiles() {
            if (!Directory.Exists(GlobalConfig.AppDir)) Directory.CreateDirectory(GlobalConfig.AppDir);
            if (!File.Exists(GlobalConfig.AppDir + "\\entries.db")) InitDB();
            if (!File.Exists(GlobalConfig.AppDir + "\\config.json")) GlobalConfig.SerializeConfig();
        }

        private static void InitDB() {

			string dbPath = GlobalConfig.AppDir + "\\entries.db";

			SQLiteConnection.CreateFile(dbPath);

            using (var db = new SQLiteConnection("Data Source=" + dbPath)) {

                db.Open();

                new SQLiteCommand(Resources.ResourceManager.GetString("CreateAnimeTable"), db).ExecuteNonQuery();
                new SQLiteCommand(Resources.ResourceManager.GetString("CreateMangaTable"), db).ExecuteNonQuery();

				db.Close();
            }
        }

        private static void SetDataDir() => AppDomain.CurrentDomain.SetData("DataDirectory", GlobalConfig.AppDir);
	}
}
