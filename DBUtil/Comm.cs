using System;
using System.IO;
using System.Data;
using System.Data.OracleClient;
using System.Collections.Generic;
using System.Text;

namespace Stamp4Fun.DBUtil
{
    public class Comm
    {
        public static OracleConnection OpenConn()
        {
            OracleConnection conn = new OracleConnection();

            conn.ConnectionString =
                string.Format(@"Persist Security Info=True;User ID=stamp;Password=Lovestamp$1983;
                    Data Source= (DESCRIPTION =(ADDRESS_LIST =(ADDRESS = (PROTOCOL = TCP)(
                    HOST = {0})(PORT = 1521)))(CONNECT_DATA =(SERVICE_NAME = oracq)));",
                    ConnSource == CONN_SOURCE_TYPE.LOCAL ? "localhost" : "www.stamp4fun.org");

            conn.Open(); 
            return conn;
        }

        public enum CONN_SOURCE_TYPE { LOCAL, WEB };
        public static CONN_SOURCE_TYPE ConnSource = CONN_SOURCE_TYPE.LOCAL;

        public static DataTable GetData(string sql, string tabName)
        {
            OracleConnection conn = Comm.OpenConn();
            OracleDataAdapter oda = new OracleDataAdapter(sql, conn);
            DataTable dt = new DataTable(tabName);
            oda.Fill(dt);
            conn.Close();
            return dt;
        }

        // CreateTempLob
        public static OracleLob CreateTempLob(
          OracleCommand cmd, OracleType lobtype)
        {
            //Oracle server syntax to obtain a temporary LOB.
            cmd.CommandText = "DECLARE A " + lobtype + "; " +
                           "BEGIN " +
                              "DBMS_LOB.CREATETEMPORARY(A, FALSE); " +
                              ":LOC := A; " +
                           "END;";
            cmd.Parameters.Clear();
            //cmd.CommandType = CommandType.StoredProcedure;
            //Bind the LOB as an output parameter.
            OracleParameter p = cmd.Parameters.Add("LOC", lobtype);
            p.Direction = ParameterDirection.Output;

            //Execute (to receive the output temporary LOB).
            cmd.ExecuteNonQuery();

            //Return the temporary LOB.
            return (OracleLob)p.Value;
        }

        public static OracleLob LoadPic(OracleCommand cmd, string pic)
        {
            OracleLob blob = CreateTempLob(cmd, OracleType.Blob);
            byte[] bytes = File.ReadAllBytes(pic);
            blob.Write(bytes, 0, bytes.Length);
            return blob;
        }
    }
}
