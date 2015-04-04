using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;

namespace Stamp4Fun.DBUtil
{
    public interface ITreeObject
    {
        DataTable GetTopObjects();
        DataTable GetChildObjects(object pid);
        string NameCol { get; }
    }

    public class Topic : DataObject, ITreeObject
    {
        public Topic()
        {
            TABLE_NAME = "v_topic";
            PROC_INSERT = "pkg_stamp.insert_topic";
            PROC_UPDATE = "pkg_stamp.update_topic";
            PROC_DELETE = "pkg_stamp.delete_topic";
        }

        public DataTable GetTopObjects()
        {
            return Comm.GetData(string.Format(
                "select * from {0} where pid is null order by xh", TABLE_NAME), TABLE_NAME);
            //return new DataTable();
        }

        public DataTable GetChildObjects(object pid)
        {
            return Comm.GetData(string.Format(
                "select * from {0} where pid ={1} order by xh", TABLE_NAME, pid), TABLE_NAME);
            //return new DataTable();
        }

        public DataTable GetChildObjects(DataTable dtAll, object pid)
        {
            DataView dv = new DataView(dtAll, string.Format("pid = {0}", pid), "xh", DataViewRowState.CurrentRows);
            DataTable dt = dtAll.Clone();
            foreach (DataRowView drv in dv)
            {
                dt.ImportRow(drv.Row);
            }
            return dt;
        }

        public DataTable GetAllObjects()
        {
            return Comm.GetData(string.Format("select * from {0}", TABLE_NAME), TABLE_NAME);
        }

        public DataTable GetTopObjects(DataTable dtAll)
        {
            DataView dv = new DataView(dtAll, string.Format("pid is null"), "xh", DataViewRowState.CurrentRows);
            DataTable dt = dtAll.Clone();
            foreach (DataRowView drv in dv)
            {
                dt.ImportRow(drv.Row);
            }
            return dt;
        }

        public string NameCol
        {
            get { return "name"; }
        }

        public int GetMaxId()
        {
            DataTable dt = Comm.GetData(string.Format(
                "select max(id) from {0}", TABLE_NAME), TABLE_NAME);
            if (dt.Rows[0][0] is DBNull)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(dt.Rows[0][0]);
            }

        }

        public string GetTopicFullName(int id)
        {
            DataTable dt = Comm.GetData(string.Format(
                "select name from {1} start with id={0} connect by prior pid=id", id, TABLE_NAME), TABLE_NAME);
            string fullName = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                fullName = string.Format("/{0}{1}", dr[0], fullName);
            }
            return fullName;
            
        }

        protected override void AddInsParams(OracleCommand cmd, DataRow dr)
        {
            cmd.Parameters.Add(new OracleParameter("i_id", dr["id"]));
            cmd.Parameters.Add(new OracleParameter("i_pid", dr["pid"]));
            cmd.Parameters.Add(new OracleParameter("i_name", dr["name"]));
            cmd.Parameters.Add(new OracleParameter("i_xh", dr["xh"]));
            cmd.Parameters.Add(new OracleParameter("i_user_id", User.CURRENT_USER_ID));
        }

        protected override void AddDelParams(OracleCommand cmd, DataRow dr)
        {
            cmd.Parameters.Add(new OracleParameter("i_id", dr["id", DataRowVersion.Original]));
        }

    }

    public class Topic2Elem : DataObject
    {
        public Topic2Elem()
        {
            TABLE_NAME = "v_topic_r_elem";
            PROC_INSERT = "pkg_stamp.insert_topic_r_elem";
            PROC_UPDATE = "pkg_stamp.insert_topic_r_elem";
            PROC_DELETE = "pkg_stamp.delete_topic_r_elem";
        }


        public DataTable GetElemTopics(object elemId)
        {
            DataTable dt = Comm.GetData(string.Format(
                "select * from {1} where elem_id='{0}'", elemId, TABLE_NAME), TABLE_NAME);
            Topic t = new Topic();
            foreach(DataRow dr in dt.Rows)
            {
                dr["topic_name"] = t.GetTopicFullName(Convert.ToInt32(dr["topic_id"]));
            }
            dt.AcceptChanges();
            return dt;
        }

        protected override void AddInsParams(OracleCommand cmd, DataRow dr)
        {
            cmd.Parameters.Add(new OracleParameter("i_elem_id", dr["elem_id"]));
            cmd.Parameters.Add(new OracleParameter("i_topic_id", dr["topic_id"]));
        }

        protected override void AddDelParams(OracleCommand cmd, DataRow dr)
        {
            cmd.Parameters.Add(new OracleParameter("i_elem_id", dr["elem_id", DataRowVersion.Original]));
            cmd.Parameters.Add(new OracleParameter("i_topic_id", dr["topic_id", DataRowVersion.Original]));
        }

    }
}
