using System;
using System.Data;
using System.Data.OracleClient;
using System.Collections.Generic;
using System.Web;

namespace Stamp4Fun.DBUtil
{
    public class Nation : DataObject, ITreeObject
    {
        public Nation()
        {
            TABLE_NAME = "v_nation";
            PROC_INSERT = "pkg_stamp.insert_nation";
            PROC_UPDATE = "pkg_stamp.update_nation";
            PROC_DELETE = "pkg_stamp.delete_nation";
        }

        public DataTable GetNations()
        {
            return Comm.GetData(string.Format(
                "select * from {0} order by name_en", TABLE_NAME), TABLE_NAME);
        }


        public DataTable GetTopObjects()
        {
            return GetNations();
        }

        public DataTable GetChildObjects(object pid)
        {
            return new DataTable();
        }

        public bool HasSets(object id)
        {
            DataTable dt = Comm.GetData(string.Format("select count(*) from v_set where nation_id={0}", id), "");
            return Convert.ToInt32(dt.Rows[0][0]) > 0;
        }

        public string NameCol
        {
            get { return "name_cn"; }
        }

        public object GetNextId()
        {
            DataTable dt = Comm.GetData(string.Format(
                "select max(id) from {0}", TABLE_NAME), TABLE_NAME);
            if (dt.Rows[0][0] is DBNull)
            {
                return 1;
            }
            else
            {
                return Convert.ToInt32(dt.Rows[0][0]) + 1;
            }

        }

        public string GetNationIdByNameCn(string name_cn)
        {
            DataTable dt = Comm.GetData(
                string.Format("select id from {1} where name_cn='{0}'", name_cn, TABLE_NAME), "name_cn");
            string ret = string.Empty;
            if (dt.Rows.Count > 0)
            {
                ret = dt.Rows[0]["id"].ToString();
            }
            return ret;
        }

        public DataTable GetNationById(object id)
        {
            return Comm.GetData(string.Format(
                "select * from {1} where id='{0}'", id, TABLE_NAME), TABLE_NAME);
        }

        public DataTable GetNationByNameEn(string name)
        {
            return Comm.GetData(string.Format(
                "select * from {1} where name_en='{0}'", name, TABLE_NAME), TABLE_NAME);
        }

        protected override void AddInsParams(OracleCommand cmd, DataRow dr)
        {
            cmd.Parameters.Add(new OracleParameter("i_id", dr["id"]));
            cmd.Parameters.Add(new OracleParameter("i_name_en", dr["name_en"]));
            cmd.Parameters.Add(new OracleParameter("i_name_cn", dr["name_cn"]));
            cmd.Parameters.Add(new OracleParameter("i_desp_en", dr["desp_en"]));
            cmd.Parameters.Add(new OracleParameter("i_desp_cn", dr["desp_cn"]));
            cmd.Parameters.Add(new OracleParameter("i_user_id", User.CURRENT_USER_ID));
        }

        protected override void AddDelParams(OracleCommand cmd, DataRow dr)
        {
            cmd.Parameters.Add(new OracleParameter("i_id", dr["id", DataRowVersion.Original]));
        }
    }
}