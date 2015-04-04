using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;

namespace Stamp4Fun.DBUtil
{
    public abstract class DataObject
    {
        public string TABLE_NAME;
        protected string PROC_INSERT;
        protected string PROC_UPDATE;
        protected string PROC_DELETE;

        protected abstract void AddInsParams(OracleCommand cmd,DataRow dr);
        protected abstract void AddDelParams(OracleCommand cmd, DataRow dr);

        internal void SaveToDBNoCommit(OracleConnection conn, OracleTransaction trans, DataTable dt)
        {
            DataTable dt2 = dt.Clone();
            DataTable dtDel = dt.GetChanges(DataRowState.Deleted);
            if (dtDel != null)
                dt2.Merge(dtDel);
            DataTable dtAdd = dt.GetChanges(DataRowState.Added);
            if (dtAdd != null)
                dt2.Merge(dtAdd);
            DataTable dtMod = dt.GetChanges(DataRowState.Modified);
            if (dtMod != null)
                dt2.Merge(dtMod);
            foreach (DataRow dr in dt2.Rows)
            {
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.Transaction = trans;
                cmd.CommandType = CommandType.StoredProcedure;

                if (dr.RowState == DataRowState.Added)
                {
                    BeforeInsertRow(cmd, dr);
                    cmd.CommandText = PROC_INSERT;
                    AddInsParams(cmd, dr);
                }
                else if (dr.RowState == DataRowState.Modified)
                {
                    BeforeUpdateRow(cmd, dr);
                    cmd.CommandText = PROC_UPDATE;
                    AddInsParams(cmd, dr);
                }
                else if (dr.RowState == DataRowState.Deleted)
                {
                    cmd.CommandText = PROC_DELETE;
                    AddDelParams(cmd, dr);    
                }
                cmd.ExecuteNonQuery();
                
            }

            dt.AcceptChanges();
        }

        protected virtual void BeforeInsertRow(OracleCommand cmd, DataRow dr)
        {

        }

        protected virtual void BeforeUpdateRow(OracleCommand cmd, DataRow dr)
        {

        }

        public void SaveToDB(DataTable dt)
        {
            OracleConnection conn = null;
            OracleTransaction trans = null;

            try
            {
                conn = DBUtil.Comm.OpenConn();
                trans = conn.BeginTransaction();

                SaveToDBNoCommit(conn, trans, dt);

                trans.Commit();
            }
            catch (System.Exception ex)
            {
                if (trans != null)
                {
                    trans.Rollback();
                }
                throw ex;
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

    }
}
