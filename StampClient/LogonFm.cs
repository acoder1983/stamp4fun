using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Stamp4Fun.StampClient
{
    public partial class LogonFm : Form
    {
        public LogonFm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormClosed +=  new FormClosedEventHandler(OnFormClosed);
        }

        private void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            this.IsAbandon = !IsLogSuccess;
        }

        private void btnLogon_Click(object sender, EventArgs e)
        {
            // check username and pwd
            if (string.IsNullOrEmpty(tbUsername.Text) ||
                string.IsNullOrEmpty(tbPassword.Text))
            {
                MessageBox.Show("请输入用户名和密码");
                return;
            }
            // verify in db
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                DBUtil.User user = new DBUtil.User();
                DataTable dt = user.GetUserByLogonInfo(tbUsername.Text, tbPassword.Text);
                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("用户名或密码不正确");

                }
                else
                {
                    LOGON_USER = dt.Rows[0]["username"].ToString();
                    DBUtil.User.CURRENT_USER_ID = dt.Rows[0]["id"].ToString();
                    IsLogSuccess = true;
                    this.Close();
                }
            }
            catch (System.Exception ex)
            {
                string msg;
                if (ex.Message.IndexOf("目标主机") != -1)
                {
                    msg = "连接服务器失败，请稍候再试";
                }
                else
                {
                    msg = ex.Message;
                }
                MessageBox.Show(msg);
            }
            Cursor.Current = Cursors.Default;
        }

        public static string LOGON_USER;
        
        public bool IsLogSuccess = false;
        public bool IsAbandon = false;
        private void btnReg_Click(object sender, EventArgs e)
        {
            RegFm fm = new RegFm();
            fm.ShowDialog();
            if (fm.IsRegSuccess)
            {
                this.IsLogSuccess = true;
                this.Close();
            }
        }
    }
}
