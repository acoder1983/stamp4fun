using System;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;

namespace Stamp4Fun.Upgrade
{
    static class Program
    {
        internal static string WEB_VER_PATH = "http://www.stamp4fun.org:1983/stamp/Version";
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                int localVer = -1;
                int newVer = -1;
                string VER_FILE = "Stamp4Fun.Version.dll";
                if (File.Exists(VER_FILE))
                {
                    string[] lines = File.ReadAllLines(VER_FILE);
                    localVer = Convert.ToInt32(lines[0]);
                }

                
                Util.DownloadWebFile(string.Format("{0}/{1}",
                    WEB_VER_PATH, VER_FILE));
                string[] verLines = File.ReadAllLines(VER_FILE);
                newVer = Convert.ToInt32(verLines[0]);

                if (newVer > localVer)
                {
                    Process[] procs = Process.GetProcessesByName("目录客户端");
                    foreach (Process p in procs)
                    {
                        if (p.Id != Process.GetCurrentProcess().Id)
                        {
                            p.Kill();
                            p.WaitForExit(10000);
                        }
                    }

                    MainFm frm = new MainFm();
                    frm.VerFiles = verLines;
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();
                    MessageBox.Show("升级成功，请重新启动程序");
                }
                else
                {
                    string EXE_FILE = "Stamp4Fun.StampClient.dll";
                    Assembly asm = Assembly.LoadFrom(Environment.CurrentDirectory + "/" + EXE_FILE);
                    Type t = asm.GetType("Stamp4Fun.StampClient.Program");
                    t.InvokeMember("Main",
                        BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null, null, null);
                }
            }
            catch (System.Exception ex)
            {
                string msg;
                if (ex.Message.IndexOf("调用的目标") != -1)
                {
                    msg = "请稍候重启程序";
                }
                else
                {
                    msg = ex.Message;
                }
                MessageBox.Show(msg);
            }
        }
    }
}
