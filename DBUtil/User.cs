using System;
using System.Data;
using System.Data.OracleClient;
using System.Collections.Generic;
using System.Web;

namespace Stamp4Fun.DBUtil
{
    public class User : DataObject
    {
        public User()
        {
            TABLE_NAME = "t_user";
            PROC_INSERT = "pkg_stamp.insert_user";
            PROC_UPDATE = "pkg_stamp.update_user";
            PROC_DELETE = "pkg_stamp.delete_user";
        }

        public static string CURRENT_USER_ID = null;

        public DataTable GetUsers()
        {
            return Comm.GetData("select * from v_user_lognum", TABLE_NAME);
        }

        public DataTable GetUserByLogonInfo(string username, string password)
        {
            return  Comm.GetData(
                string.Format("select * from {2} where username='{0}' and password='{1}'", username, password, TABLE_NAME), "");
        }

        public DataTable GetUserByUserId(string userId)
        {
            return Comm.GetData(
                string.Format("select * from {1} where id='{0}'", userId, TABLE_NAME), TABLE_NAME);
        }

        public bool IsUserExist(string username)
        {
            return Comm.GetData(string.Format("select username from {1} where username = '{0}'", username, TABLE_NAME), "").Rows.Count > 0;
        }

        protected override void AddInsParams(OracleCommand cmd, DataRow dr)
        {
            cmd.Parameters.Add(new OracleParameter("i_id", dr["id"]));
            cmd.Parameters.Add(new OracleParameter("i_username", dr["username"]));
            cmd.Parameters.Add(new OracleParameter("i_password", dr["password"]));
            cmd.Parameters.Add(new OracleParameter("i_email", dr["email"]));
        }

        protected override void AddDelParams(OracleCommand cmd, DataRow dr)
        {
            cmd.Parameters.Add(new OracleParameter("i_id", dr["id"]));
        }

    }
}
