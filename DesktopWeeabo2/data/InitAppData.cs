using DesktopWeeabo2.Infrastructure.Database;
using DesktopWeeabo2.Properties;
using System.IO;
using System.Threading.Tasks;

namespace DesktopWeeabo2.Data {
	public static class InitAppData {

        public static void Init() {
            CheckFiles();
        }

		public static bool CheckRootDir() => Directory.Exists(GlobalConfig.AppDir);

		private static void CheckFiles() {
			if (!CheckRootDir()) Directory.CreateDirectory(GlobalConfig.AppDir);
			if (!File.Exists(GlobalConfig.AppDir + "\\entries.db")) DbActions.InitDB();
            if (!File.Exists(GlobalConfig.AppDir + "\\config.json")) GlobalConfig.SerializeConfig();
        }
	}
}
