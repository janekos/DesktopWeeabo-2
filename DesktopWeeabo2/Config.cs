using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopWeeabo2
{
    public static class Config
    {
        private static string appDir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/DesktopWeeabo2";        
        public static string getAppDir() { return appDir; }
    }
}
