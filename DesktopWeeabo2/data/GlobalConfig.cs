using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2.data
{
    public static class GlobalConfig
    {
        private static string appDir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\DesktopWeeabo2";
        public static string getAppDir() { return appDir; }

        public static void serializeConfig()
        {
            File.WriteAllText(appDir + "\\config.json", JsonConvert.SerializeObject( new SerializableConfig
            {
                appDir = appDir
            }));
        }
    }
}
