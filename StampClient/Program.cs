using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace Stamp4Fun.StampClient
{   
    public class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            string oraclePath = Environment.CurrentDirectory;
            Environment.SetEnvironmentVariable("PATH", oraclePath, EnvironmentVariableTarget.Process);
            //Environment.SetEnvironmentVariable("TNS_ADMIN", oraclePath, EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("NLS_LANG", "SIMPLIFIED CHINESE_CHINA.ZHS16GBK", EnvironmentVariableTarget.Process);

            DBUtil.Comm.ConnSource = DBUtil.Comm.CONN_SOURCE_TYPE.WEB;

            // login
            LogonFm logFm = new LogonFm();
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = "Stamp4Fun.User.dll";
            Configuration cfg = ConfigurationManager.OpenMappedExeConfiguration(
                map, ConfigurationUserLevel.None);
            if (cfg != null && cfg.AppSettings.Settings.Count > 1)
            {
                logFm.tbUsername.Text = cfg.AppSettings.Settings["username"].Value;
                logFm.tbPassword.Text = cfg.AppSettings.Settings["password"].Value;
            }
            logFm.ShowDialog();
            if (logFm.IsAbandon) return;
            while (!logFm.IsLogSuccess)
            {
                logFm.ShowDialog();
                if (logFm.IsAbandon) return;    
            }
            if (cfg != null)
            {
                cfg.AppSettings.Settings["username"].Value = logFm.tbUsername.Text;
                cfg.AppSettings.Settings["password"].Value = logFm.tbPassword.Text;
                cfg.Save();
            }
            MainFm fm = new MainFm();
            Application.Run(fm);
        }

    }
}