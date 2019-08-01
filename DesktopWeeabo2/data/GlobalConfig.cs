using Newtonsoft.Json;
using System;
using System.IO;

namespace DesktopWeeabo2.Data {
	public static class GlobalConfig {

        private static string _AppDir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DesktopWeeabo2";
        public static string AppDir { get { return _AppDir; } }

        public static void SerializeConfig() {
            File.WriteAllText(_AppDir + "\\config.json", JsonConvert.SerializeObject(new SerializableConfig {
                AppDir = _AppDir
            }));
        }
    }
}
