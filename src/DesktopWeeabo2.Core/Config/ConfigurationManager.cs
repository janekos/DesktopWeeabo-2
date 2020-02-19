using Newtonsoft.Json;
using System.IO;

namespace DesktopWeeabo2.Core.Config {

	public static class ConfigurationManager {
		public static Config Config = new Config();

		public static void LoadConfig() {
			Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Config.AppDir + "\\config.json"));
		}

		public static void SaveConfig() {
			File.WriteAllText(Config.AppDir + "\\config.json", JsonConvert.SerializeObject(Config));
		}
	}
}