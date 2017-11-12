using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace V2Switcher
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (Util.checkEnvironment() == false)
            {
                MessageBox.Show("需要V2Ray核心程序和至少一个Json配置文件","V2Switcher无法启动",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
            else
            {
                using (ProcessIcon pi = new ProcessIcon())
                {
                    pi.Display();
                    Application.Run();
                }
            }
        }
    }
}
