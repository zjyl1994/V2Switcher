using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace V2Switcher
{
    class ProxyManager
    {
        // 刷新代理设置
        [DllImport("wininet.dll")]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int dwBufferLength);
        public const int INTERNET_OPTION_SETTINGS_CHANGED = 39;
        public const int INTERNET_OPTION_REFRESH = 37;
        bool settingsReturn, refreshReturn;

        private static ProxyManager uniqueInstance; // 单例句柄

        private Config current; // 当前配置

        public Config Current {
            get {
                return current;
            }
            set {
                current = value;
                Util.saveDefault(current.Name);
                if (Enable)
                {
                    Array.ForEach(Process.GetProcessesByName(Util.executeName),x=>x.Kill());
                    Process.Start(Util.executeName, "-config " + current.Filename);
                    RegistryKey internetSetting = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings",true);
                    internetSetting.SetValue("ProxyServer", current.Address, RegistryValueKind.String);
                    internetSetting.Close();
                }
            }
        }

        public static ProxyManager GetInstance()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = new ProxyManager();
            }
            return uniqueInstance;
        }

        private ProxyManager()
        {
        }

        public string currentConfigName {
            get { return current?.Name ?? ""; }
        }

        public bool Enable {
            get {
                RegistryKey internetSetting = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings");
                bool enable = Convert.ToBoolean(internetSetting.GetValue("ProxyEnable"));
                internetSetting.Close();
                return enable;
            }
            set {
                RegistryKey internetSetting = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Internet Settings",true);
                if (value)
                {
                    internetSetting.SetValue("ProxyServer", current.Address, RegistryValueKind.String);
                    internetSetting.SetValue("ProxyEnable", 1, RegistryValueKind.DWord);
                    Process.Start(Util.executeName, "-config " + current.Filename);
                }
                else
                {
                    Array.ForEach(Process.GetProcessesByName(Util.executeName), x => x.Kill());
                    internetSetting.SetValue("ProxyEnable", 0, RegistryValueKind.DWord);
                }
                internetSetting.Close();
                // 刷新代理设置
                settingsReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_SETTINGS_CHANGED, IntPtr.Zero, 0);
                refreshReturn = InternetSetOption(IntPtr.Zero, INTERNET_OPTION_REFRESH, IntPtr.Zero, 0);
            }
        }
    }
}
