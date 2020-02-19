using DesktopWeeabo2.Core.Config;
using System.IO;

namespace DesktopWeeabo2.Helpers {

	public static class AppInitHelpers {

		public static void Init() {
			CheckFiles();
		}

		public static bool CheckRootDir() =>
			Directory.Exists(ConfigurationManager.Config.AppDir);

		private static void CheckFiles() {
			if (!CheckRootDir()) 
				Directory.CreateDirectory(ConfigurationManager.Config.AppDir);

			if (!File.Exists(ConfigurationManager.Config.AppDir + "\\config.json"))
				ConfigurationManager.SaveConfig();
			else
				ConfigurationManager.LoadConfig();
		}
	}
}