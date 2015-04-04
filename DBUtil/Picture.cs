using System;
using System.IO;
using System.Data;
using System.Data.OracleClient;
using System.Collections.Generic;
using System.Text;

namespace Stamp4Fun.DBUtil
{
    public class Picture : DataObject
    {
        public Picture()
        {
            TABLE_NAME = "v_picture";
            PROC_INSERT = "pkg_stamp.insert_pic";
            PROC_UPDATE = "pkg_stamp.update_pic";
            PROC_DELETE = "pkg_stamp.delete_pic";
        }

        public DataTable GetPicById(object picId)
        {
            return Comm.GetData(string.Format("select id,no,image_large,image_small,desp_en,desp_cn, user_id from {1} where id = '{0}'",
                picId == DBNull.Value ? -1 : picId, TABLE_NAME), TABLE_NAME);
        }

        public DataTable GetPicByCond(object cond)
        {
            return Comm.GetData(string.Format("select id, no, image_large, image_small, desp_en, desp_cn, user_id from {1} where {0}",
                cond, TABLE_NAME), TABLE_NAME);
        }

        public DataTable GetPicByCondOnClntLoad(object cond)
        {
            return Comm.GetData(string.Format("select id, no, image_small as image_large, image_small, desp_en, desp_cn, user_id from {1} where {0}",
                cond, TABLE_NAME), TABLE_NAME);
        }

        public object GetPicIdByElemId(object elemId)
        {
            DataTable dt = Comm.GetData(string.Format(
                "select pic_id from t_picture_r_elem where elem_id='{0}'", elemId), "pic_id");
            return dt.Rows.Count == 0 ? DBNull.Value : dt.Rows[0][0];
        }

        public static void SaveSmallPic(DataRow dr, string smallPic)
        {
            byte[] bytes = dr["image_small"] as byte[];
            FileStream fs = new FileStream(smallPic, FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
        }

        public static void SaveLargePic(DataRow dr, string largePic)
        {
            byte[] bytes = dr["image_large"] as byte[];
            FileStream fs = new FileStream(largePic, FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
        }

        protected override void AddInsParams(OracleCommand cmd, DataRow dr)
        {
            cmd.Parameters.Add(new OracleParameter("i_id", dr["id"]));
            cmd.Parameters.Add(new OracleParameter("i_no", dr["no"]));
            cmd.Parameters.Add(new OracleParameter("i_desp_en", dr["desp_en"]));
            cmd.Parameters.Add(new OracleParameter("i_desp_cn", dr["desp_cn"]));
            cmd.Parameters.Add(new OracleParameter("i_imgs", dr["image_small_blob"]));
            cmd.Parameters.Add(new OracleParameter("i_imgl", dr["image_large_blob"]));
            cmd.Parameters.Add(new OracleParameter("i_user_id", DBUtil.User.CURRENT_USER_ID));
        }

        protected override void AddDelParams(OracleCommand cmd, DataRow dr)
        {
            cmd.Parameters.Add(new OracleParameter("i_id", dr["id", DataRowVersion.Original]));
        }

        void GenOracleBlob(DataRow dr, OracleConnection conn, OracleTransaction trans)
        {
            byte[] bytesSmallPic = dr["image_small"] as byte[];
            OracleLob lob_s = DBUtil.Comm.CreateTempLob(
                new OracleCommand("", conn, trans), OracleType.Blob);
            if (!dr.Table.Columns.Contains("image_small_blob"))
            {
                dr.Table.Columns.Add("image_small_blob", lob_s.GetType());
                dr.Table.Columns.Add("image_large_blob", lob_s.GetType());
            }
            lob_s.Write(bytesSmallPic, 0, bytesSmallPic.Length);
            dr["image_small_blob"] = lob_s;
            byte[] bytesLargePic = dr["image_large"] as byte[];
            OracleLob lob_l = DBUtil.Comm.CreateTempLob(
                new OracleCommand("", conn, trans), OracleType.Blob);
            lob_l.Write(bytesLargePic, 0, bytesLargePic.Length);
            dr["image_large_blob"] = lob_l;
        }

        protected override void BeforeInsertRow(OracleCommand cmd, DataRow dr)
        {
            GenOracleBlob(dr, cmd.Connection, cmd.Transaction);
        }

        protected override void BeforeUpdateRow(OracleCommand cmd, DataRow dr)
        {
            GenOracleBlob(dr, cmd.Connection, cmd.Transaction);
        }
        
        
    }
}