using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace V2Switcher
{
    static class Util
    {
        public static string[] configsName {
            get { return Directory.GetFiles(Application.StartupPath, "*.json").Select(e => Path.GetFileNameWithoutExtension(e)).ToArray(); }
        }
        private static string exeFileName;
        public static string executeName {
            get { return exeFileName; }
        }

        public static bool checkEnvironment(){
            bool hasJson = configsName != null && configsName.Length != 0;
            bool hasCore = false;
            if (File.Exists("v2ray.exe"))
            {
                exeFileName = "v2ray";
                hasCore = true;
            }
            if (File.Exists("wv2ray.exe"))
            {
                exeFileName = "wv2ray";
                hasCore = true;
            }
            return hasJson && hasCore;
        }
        public static void saveDefault(string cfgName)
        {
            File.WriteAllText("default.lck", cfgName);
        }
        public static string loadDefault()
        {
            if (File.Exists("default.lck"))
            {
                string cfgName = File.ReadAllText("default.lck").Trim();
                return File.Exists(cfgName+".json") ? cfgName: configsName[0];
            }
            else
            {
                return configsName[0];
            }
        }
    }
}
