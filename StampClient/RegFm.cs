using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Stamp4Fun.StampClient
{
    public partial class RegFm : Form
    {
        public RegFm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnReg_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbUsername.Text) ||
                string.IsNullOrEmpty(tbPwd1.Text) ||
                string.IsNullOrEmpty(tbPwd2.Text))
            {
                MessageBox.Show("请输入用户名和密码");
                return;
            }
            if (tbPwd1.Text != tbPwd2.Text)
            {
                MessageBox.Show("两次密码不符");
                return;
            }

            // verify in db
            DBUtil.User user = new DBUtil.User();
            if (user.IsUserExist(tbUsername.Text))
            {
                MessageBox.Show("用户名已注册");

            }
            else
            {
                try
                {
                    string userid = Comm.Util.GenGuid();
                    DataTable dt = user.GetUserByUserId("-1");
                    DataRow dr = dt.NewRow();
                    dr.ItemArray = new object[] { userid, tbUsername.Text, tbPwd2.Text, DateTime.Now, "" };
                    dt.Rows.Add(dr);
                    user.SaveToDB(dt);
                    LogonFm.LOGON_USER = tbUsername.Text;
                    DBUtil.User.CURRENT_USER_ID = userid;
                    this.IsRegSuccess = true;
                    this.Close();
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public bool IsRegSuccess=false;

    }
}
