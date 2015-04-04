using System;
using System.Data;
using System.Data.OracleClient;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Stamp4Fun.DBUtil
{
    public class Set : DataObject
    {
        public Set()
        {
            TABLE_NAME = "v_set";
            PROC_INSERT = "pkg_stamp.insert_set";
            PROC_UPDATE = "pkg_stamp.update_set";
            PROC_DELETE = "pkg_stamp.delete_set";
        }
        
        public DataTable GetSetIdData(string cond)
        {
            DataTable dt = Comm.GetData(string.Format(
                "select set_id from v_set_id where {0}", cond), "SetId");
            // remove the repeated set_id
            Collection<int> delRows = new Collection<int>();
            for (int i = 0; i < dt.Rows.Count-1; ++i)
            {
                if (dt.Rows[i][0].ToString() == dt.Rows[i + 1][0].ToString())
                {
                    delRows.Add(i + 1);
                }
            }
            for (int i = 0; i < delRows.Count; ++i)
            {
                dt.Rows[delRows[i]].Delete();
            }
            dt.AcceptChanges();
            return dt;
        }

        public DataTable GetSetById(object setId)
        {
            return Comm.GetData(string.Format("select * from {1} where id = '{0}'", 
                setId, TABLE_NAME), TABLE_NAME);
        }

        
        protected override void AddInsParams(OracleCommand cmd, DataRow dr)
        {
            cmd.Parameters.Add(new OracleParameter("i_id", dr["id"]));
            cmd.Parameters.Add(new OracleParameter("i_nation_id", dr["nation_id"]));
            cmd.Parameters.Add(new OracleParameter("i_year", dr["year"]));
            cmd.Parameters.Add(new OracleParameter("i_month", dr["month_id"]));
            cmd.Parameters.Add(new OracleParameter("i_day", dr["day"]));
            cmd.Parameters.Add(new OracleParameter("i_print", dr["print_id"]));
            cmd.Parameters.Add(new OracleParameter("i_paper", dr["paper_id"]));
            cmd.Parameters.Add(new OracleParameter("i_perf", dr["perf"]));
            cmd.Parameters.Add(new OracleParameter("i_desp_en", dr["desp_en"]));
            cmd.Parameters.Add(new OracleParameter("i_desp_cn", dr["desp_cn"]));
            cmd.Parameters.Add(new OracleParameter("i_user_id", DBUtil.User.CURRENT_USER_ID));
            cmd.Parameters.Add(new OracleParameter("i_type_id", dr["type_id"]));
            cmd.Parameters.Add(new OracleParameter("i_pic_id", dr["pic_id"]));
        }

        protected override void AddDelParams(OracleCommand cmd, DataRow dr)
        {
            cmd.Parameters.Add(new OracleParameter("i_id", dr["id", DataRowVersion.Original]));
        }

    }
}
