using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using V2Switcher.Properties;
using System.Diagnostics;
using System.ComponentModel;

namespace V2Switcher
{
    public class ProcessIcon : IDisposable
    {
        NotifyIcon ni;
        ContextMenuStrip cm;
        ProxyManager pm;

        public ProcessIcon()
        {
            ni = new NotifyIcon();
            cm = new ContextMenuStrip();
            cm.Opening += new CancelEventHandler(OnContextMenuOpening);
            pm = ProxyManager.GetInstance();
            pm.Current = new Config(Util.loadDefault());
        }

        public void Display()
        {
            ni.MouseDoubleClick += new MouseEventHandler(ni_MouseClick);
            ni.Icon = pm.Enable ? Resource.v2enable:Resource.v2disable;
            ni.Text = "V2Switcher";
            ni.Visible = true;
            ni.ContextMenuStrip = cm;
        }

        public void Dispose()
        {
            ni.Dispose();
        }

        void ni_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                pm.Enable = !pm.Enable;
                ni.Icon = pm.Enable ? Resource.v2enable : Resource.v2disable;
            }
        }

        void OnContextMenuOpening(object sender, CancelEventArgs e)
        {
            ToolStripMenuItem item;
            ToolStripSeparator sep;

            cm.Items.Clear();
            item = new ToolStripMenuItem();
            item.Text = "启用代理";
            item.Click += new System.EventHandler(Proxy_Click);
            item.Checked = pm.Enable;
            cm.Items.Add(item);

            sep = new ToolStripSeparator();
            cm.Items.Add(sep);

            foreach (string configname in Util.configsName)
            {
                item = new ToolStripMenuItem();
                item.Text = configname;
                item.Click += new System.EventHandler(Server_Click);
                item.Checked = pm.currentConfigName == configname;
                cm.Items.Add(item);
            }

            sep = new ToolStripSeparator();
            cm.Items.Add(sep);

            item = new ToolStripMenuItem();
            item.Text = "退出";
            item.Click += new System.EventHandler(Exit_Click);
            cm.Items.Add(item);
        }

        void Exit_Click(object sender, EventArgs e)
        {
            Array.ForEach(Process.GetProcessesByName(Util.executeName), x => x.Kill());
            Application.Exit();
        }

        void Proxy_Click(object sender, EventArgs e)
        {
            pm.Enable =! pm.Enable;
            ni.Icon = pm.Enable ? Resource.v2enable : Resource.v2disable;
        }

        void Server_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem Caller = (ToolStripMenuItem)sender;
            pm.Current = new Config(Caller.Text);
        }
    }
}
