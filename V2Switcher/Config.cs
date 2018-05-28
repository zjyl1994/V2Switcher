using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace V2Switcher
{
    class Config
    {
        private string configPath;
        private string proxyAddr;
        private string proxyName;
        public Config(string configName)
        {
            configPath = configName+".json";
            proxyAddr = JObject.Parse(File.ReadAllText(configPath)).SelectToken("inbound.protocol").ToString() + "=127.0.0.1:" + JObject.Parse(File.ReadAllText(configPath)).SelectToken("inbound.port").ToString();
            proxyName = configName;
        }
        public string Filename {
            get { return configPath; }
        }
        public string Name {
            get { return proxyName; }
        }
        public string Address {
            get {
                return proxyAddr;
            }
        }
    }
}
