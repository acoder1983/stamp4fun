using System;
using System.Data;
using System.Data.OracleClient;
using System.Collections.Generic;
using System.Text;

namespace Stamp4Fun.DBUtil
{
    public class StampEx : DataObject
    {
        public StampEx()
        {
            TABLE_NAME = "v_stampex";
            PROC_INSERT = "pkg_stamp.insert_stampex";
            PROC_UPDATE = "pkg_stamp.update_stampex";
            PROC_DELETE = "pkg_stamp.delete_stampex";
        }
        
        public DataTable GetStampExByStampId(object exId)
        {
            return Comm.GetData(string.Format("select * from {0} where id = '{1}'", TABLE_NAME, exId), TABLE_NAME);
        }

        public DataTable GetStampExBySetId(object exId)
        {
            return Comm.GetData(string.Format("select * from {0} where set_id = '{1}'", TABLE_NAME, exId), TABLE_NAME);
        }

        protected override void AddInsParams(OracleCommand cmd,DataRow dr)
        {
            cmd.Parameters.Add(new OracleParameter("i_id", dr["id"]));
            cmd.Parameters.Add(new OracleParameter("i_stamp_id", dr["stamp_id"]));
            cmd.Parameters.Add(new OracleParameter("i_no", dr["no"]));
            cmd.Parameters.Add(new OracleParameter("i_desp_en", dr["desp_en"]));
            cmd.Parameters.Add(new OracleParameter("i_desp_cn", dr["desp_cn"]));
            cmd.Parameters.Add(new OracleParameter("i_user_id", DBUtil.User.CURRENT_USER_ID));
        }

        protected override void AddDelParams(OracleCommand cmd, DataRow dr)
        {
            cmd.Parameters.Add(new OracleParameter("i_id", dr["id", DataRowVersion.Original]));
        }

    }
}
