using Newtonsoft.Json;
using System;
using System.IO;

namespace DesktopWeeabo2.Data {
	public static class GlobalConfig {

		public static readonly string AppDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DesktopWeeabo2";

        public static void SerializeConfig() {
            File.WriteAllText(AppDir + "\\config.json", JsonConvert.SerializeObject(new SerializableConfig {
                AppDir = AppDir
            }));
        }
    }
}
