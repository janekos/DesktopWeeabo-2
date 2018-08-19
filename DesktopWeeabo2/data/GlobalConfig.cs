using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
