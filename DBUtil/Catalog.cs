using System;
using System.Drawing;
using System.IO;
using System.Data;
using System.Data.OracleClient;
using System.Collections.Generic;
using System.Text;

namespace Stamp4Fun.DBUtil
{
    public class Catalog
    {
        public static DataSet ToDataSet(ScottCatalog.Catalogue cata, string userId)
        {
            DataSet ds = new DataSet();
            User user = new User();
            DataTable dtUser = user.GetUserByUserId(userId);
            Nation nation = new Nation();
            DataTable dtNation = nation.GetNationByNameEn(cata.Nation.Name);
            if(dtNation.Rows.Count == 1)
            {
                cata.Nation.Id = dtNation.Rows[0]["id"];
            }
            else
            {
                cata.Nation.Id = nation.GetNextId();
                DataRow dr = dtNation.NewRow();
                dr["id"] = cata.Nation.Id;
                dr["name_en"] = cata.Nation.Name;
                dtNation.Rows.Add(dr);
            }

            // load dict
            string[] tabNames = new string[] { "t_month", "t_print_type", "t_paper_type" };
            DataTable[] dtDicts=new DataTable[tabNames.Length];
            Dictionary<string, int>[] setDicts = new Dictionary<string, int>[] {
                new Dictionary<string, int>(), new Dictionary<string, int>(), new Dictionary<string, int>() };
            for (int i = 0; i < tabNames.Length; ++i)
            {
                dtDicts[i] = Comm.GetData(string.Format(
                    "select * from {0}", tabNames[i]), tabNames[i]);

                foreach (DataRow dr in dtDicts[i].Rows)
                {
                    setDicts[i].Add(dr["name"].ToString(), Convert.ToInt32(dr["id"]));
                }

                if (i == 1)
                {
                    setDicts[i].Add("Engraved and Typographed", 1);
                    setDicts[i].Add("Typo. and Engr.", 1);
                    
                    setDicts[i].Add("Photo. and Typo.", 3);

                    setDicts[i].Add("Typographed or Lithographed", 4);
                    
                    setDicts[i].Add("Engr Photo", 5);
                    setDicts[i].Add("Photo., Engr. & Typo.", 5);
                    setDicts[i].Add("Photo. & Engr.", 5);
                    setDicts[i].Add("Photo & Engr.", 5);
                    setDicts[i].Add("Engr. & Photo.", 5);
                    setDicts[i].Add("Engr. and Photo.", 5);
                    setDicts[i].Add("Photo. and Engr.", 5);
                    setDicts[i].Add("Engraved and Photogravure", 5);
                    setDicts[i].Add("Photogravure and Engraved", 5);

                    setDicts[i].Add("Engr Litho", 6);
                    setDicts[i].Add("Litho. & Engr.", 6);
                    setDicts[i].Add("Litho. and Engr.", 6);
                    setDicts[i].Add("Engr. and Litho.", 6);
                    setDicts[i].Add("Lithographed and Envraved", 6);
                    setDicts[i].Add("Lithographed and Engraved", 6);
                    setDicts[i].Add("Engraved and Lithographed", 6);
                    
                    setDicts[i].Add("Litho Embossed", 7);
                    setDicts[i].Add("Photo. & Embossed", 7);
                    
                }

            }

            string[] files = Directory.GetFiles(
                new FileInfo(cata.File).DirectoryName, "*.jpg");
            int idx = 0;
            string[] picNos = new string[cata.PicDict.Count];
            cata.PicDict.Keys.CopyTo(picNos, 0);
            //Array.Sort<string>(picNos);
            // build pic table
            Picture picture = new Picture();
            DataTable dtPic = picture.GetPicById(-1);
            foreach (string no in picNos)
            {
                ScottCatalog.Picture p = cata.PicDict[no];
                DataRow dr = dtPic.NewRow();
                dr["id"] = p.Id;
                dr["no"] = p.No;
                dr["desp_en"] = p.Desp;
                dtPic.Rows.Add(dr);
                string pfile = files[idx++];
                dr["image_large"] = File.ReadAllBytes(pfile);
                Image img = Image.FromFile(pfile);
                dr["image_small"]= Stamp4Fun.Comm.Image.ImageToBytes(
                    Stamp4Fun.Comm.Image.ShrinkImage(new Bitmap(img), 200, 200, System.Drawing.Color.Black));
                dr["user_id"] = userId;
            }

            // build set table
            Set set = new Set();
            DataTable dtSet = set.GetSetById(-1);
            Stamp stamp = new Stamp();
            DataTable dtStamp = stamp.GetStampBySetId(-1);
            StampEx stampex = new StampEx();
            DataTable dtStampEx = stampex.GetStampExByStampId(-1);
            Topic2Elem topic = new Topic2Elem();
            DataTable dtTopic = topic.GetElemTopics(-1);

            foreach (ScottCatalog.Set s in cata.Sets)
            {
                DataRow drSet = dtSet.NewRow();
                dtSet.Rows.Add(drSet);
                drSet["id"] = s.Id;
                drSet["year"] = s.Year;
                drSet["day"] = s.Day;
                drSet["perf"] = s.Perf;
                drSet["desp_en"] = s.Desp;
                drSet["nation_id"] = cata.Nation.Id;
                drSet["user_id"] = userId;

                // find month
                if (!string.IsNullOrEmpty(s.Month))
                {
                    drSet["month_id"] = setDicts[0][s.Month];
                    drSet["month_desp"] = dtDicts[0].Select(string.Format("id={0}", drSet["month_id"]))[0]["desp"];
                }

                if (!string.IsNullOrEmpty(s.Print))
                {
                    drSet["print_id"] = setDicts[1][s.Print];
                    drSet["print_desp"] = dtDicts[1].Select(string.Format("id={0}", drSet["print_id"]))[0]["desp"];
                }

                if (!string.IsNullOrEmpty(s.Paper))
                {
                    drSet["paper_id"] = setDicts[2][s.Paper];
                    drSet["paper_desp"] = dtDicts[2].Select(string.Format("id={0}", drSet["paper_id"]))[0]["desp"];
                }

                foreach (ScottCatalog.Stamp m in s.Stamps)
                {
                    DataRow drStamp = dtStamp.NewRow();
                    dtStamp.Rows.Add(drStamp);
                    drStamp["set_id"] = s.Id;
                    drStamp["id"] = m.Id;
                    drStamp["no"] = m.No;
                    if (m.Pic != null)
                        drStamp["pic_id"] = m.Pic.Id;
                    drStamp["pic_no"] = m.PicNo;
                    drStamp["denom"] = m.Denom;
                    drStamp["color"] = m.Color;
                    drStamp["cv_new"] = m.CvNew;
                    drStamp["cv_old"] = m.CvUsed;
                    drStamp["user_id"] = userId;

                    foreach (ScottCatalog.StampExpand se in m.StampExpands)
                    {
                        DataRow drse = dtStampEx.NewRow();
                        dtStampEx.Rows.Add(drse);
                        drse["id"] = se.Id;
                        drse["stamp_id"] = m.Id;
                        drse["no"] = se.No;
                        drse["desp_en"] = se.Desp;
                        drse["user_id"] = userId;
                    }
                }

                // add set stamp type
                DataRow[] rows = dtStamp.Select(
                    string.Format("set_id='{0}'", drSet["id"]));
                //if (Stamp4Fun.Comm.Util.IsNumber(rows[0]["no"].ToString()))
                {
                    drSet["type_id"] = "01";
                }
                //else
                {
                    //System.Diagnostics.Debug.Assert(false);
                }
            }

            ds.Tables.AddRange(new DataTable[]{
                dtUser, dtNation, dtPic, dtSet, dtStamp,dtStampEx, dtTopic});

            return ds;
        }

     
         public static  void SaveToDB(DataSet ds)
         {
             OracleConnection conn = null;
             OracleTransaction trans = null;
             try
             {
                 conn = DBUtil.Comm.OpenConn();
                 trans = conn.BeginTransaction();

                 // save pic
                 Picture picture = new Picture();
                 picture.SaveToDBNoCommit(conn, trans, ds.Tables[picture.TABLE_NAME]);
                 
                 // save sets
                 Set set = new Set();
                 set.SaveToDBNoCommit(conn, trans, ds.Tables[set.TABLE_NAME]);

                 // save topics
                 Topic2Elem t2e = new Topic2Elem();
                 t2e.SaveToDBNoCommit(conn, trans, ds.Tables[t2e.TABLE_NAME]);
                 
                 // save stamps
                 Stamp stamp = new Stamp();
                 stamp.SaveToDBNoCommit(conn, trans, ds.Tables[stamp.TABLE_NAME]);
                 
                 // save stampex
                 StampEx se = new StampEx();
                 se.SaveToDBNoCommit(conn, trans, ds.Tables[se.TABLE_NAME]);
                 
                 trans.Commit();
                 ds.AcceptChanges();
             }
             catch (System.Exception ex)
             {
                 if (trans != null)
                     trans.Rollback();
                 throw ex;

             }
             finally
             {
                 conn.Close();
             }
         }
     

    }
}
