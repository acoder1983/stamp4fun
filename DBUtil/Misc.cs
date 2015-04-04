using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;

namespace Stamp4Fun.DBUtil
{
    public class Misc
    {
        
        public static DataTable GetPaperTypes()
        {
            return Comm.GetData("select id, desp from t_paper_type order by xh", "paper_type");
        }

        public static DataTable GetMonths()
        {
            return Comm.GetData("select id, desp from t_month order by xh", "month");
        }

        public static string GetElemLastLogTime(object id)
        {
            DataTable dt = Comm.GetData(string.Format(
                "select * from t_log where elem_id='{0}' order by time desc", id), "");
            string ret = string.Empty;
            if (dt.Rows.Count > 0)
            {
                ret = dt.Rows[0]["time"].ToString();
            }
            return ret;
        }

        public static DataTable GetRecentLogs()
        {
            return Comm.GetData(string.Format(
                @"select g.elem_id, g.op, u.username, g.time from t_log g, t_user u where 
                    g.user_id <> (select id from t_user t where t.username='acoder1983') and
                    g.user_id = u.id order by time desc"), "Log");
        }
    }

    public class PrintType : ITreeObject
    {
        public DataTable GetTopObjects()
        {
            return GetPrintTypes();
        }

        public DataTable GetChildObjects(object pid)
        {
            return new DataTable();
        }

        private DataTable GetPrintTypes()
        {
            return Comm.GetData("select id, desp from t_print_type order by xh", "print_type");
        }

        public string NameCol
        {
            get { return "desp"; }
        }
    }
}
