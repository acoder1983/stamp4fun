using System;
using System.Data;
using System.Data.OracleClient;
using System.Collections.Generic;
using System.Text;

namespace Stamp4Fun.DBUtil
{
    public class Stamp : DataObject
    {
        public Stamp()
        {
            TABLE_NAME = "v_stamp";
            PROC_INSERT = "pkg_stamp.insert_stamp";
            PROC_UPDATE = "pkg_stamp.update_stamp";
            PROC_DELETE = "pkg_stamp.delete_stamp";
        }

        public DataTable GetStampBySetId(object setId)
        {
            return Comm.GetData(string.Format(@"select * from {1} where set_id = '{0}' order by no", setId, TABLE_NAME), TABLE_NAME);    
        }

        public bool IsStampExist(object stampNo, object nationId)
        {
            return Comm.GetData(string.Format(
                "select * from v_stamp where no='{0}' and nation_id={1}", stampNo, nationId), "").Rows.Count == 1;
        }

        protected override void AddInsParams(OracleCommand cmd,DataRow dr)
        {
            cmd.Parameters.Add(new OracleParameter("i_id", dr["id"]));
            cmd.Parameters.Add(new OracleParameter("i_set_id", dr["set_id"]));
            cmd.Parameters.Add(new OracleParameter("i_pic_id", dr["pic_id"]));
            cmd.Parameters.Add(new OracleParameter("i_no", dr["no"]));
            cmd.Parameters.Add(new OracleParameter("i_pic_no", dr[ "pic_no"]));
            cmd.Parameters.Add(new OracleParameter("i_denom", dr["denom"]));
            cmd.Parameters.Add(new OracleParameter("i_color", dr["color"]));
            cmd.Parameters.Add(new OracleParameter("i_cv_new", dr["cv_new"]));
            cmd.Parameters.Add(new OracleParameter("i_cv_old", dr[ "cv_old"]));
            cmd.Parameters.Add(new OracleParameter("i_desp", dr["desp"]));
            cmd.Parameters.Add(new OracleParameter("i_user_id", DBUtil.User.CURRENT_USER_ID));
        }

        protected override void AddDelParams(OracleCommand cmd, DataRow dr)
        {
            cmd.Parameters.Add(new OracleParameter("i_id", dr["id", DataRowVersion.Original]));
        }

       
    }
}
