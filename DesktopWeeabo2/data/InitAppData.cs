using DesktopWeeabo2.data.db;
using DesktopWeeabo2.Properties;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.data {
    public static class InitAppData {

        public static void init() {
            checkFiles();
            setDataDir();
        }

        private static void checkFiles() {
            if (!Directory.Exists(GlobalConfig.getAppDir())) { Directory.CreateDirectory(GlobalConfig.getAppDir()); }
            if (!File.Exists(GlobalConfig.getAppDir() + "\\entries.db")) { initDB(GlobalConfig.getAppDir() + "\\entries.db"); }
            if (!File.Exists(GlobalConfig.getAppDir() + "\\config.json")) { GlobalConfig.serializeConfig(); }
        }

        private static void initDB(string dbPath) {

            SQLiteConnection.CreateFile(dbPath);

            using (var db = new SQLiteConnection("Data Source=" + dbPath)) {

                db.Open();

                new SQLiteCommand(Resources.ResourceManager.GetString("CreateAnimeTable"), db).ExecuteNonQuery();
                new SQLiteCommand(Resources.ResourceManager.GetString("CreateMangaTable"), db).ExecuteNonQuery();
            }
        }

        private static void setDataDir() => AppDomain.CurrentDomain.SetData("DataDirectory", GlobalConfig.getAppDir());
    }
}
