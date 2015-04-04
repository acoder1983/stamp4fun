using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Stamp4Fun.StampClient
{
    public partial class PicNaviDlg : Form
    {
        EventHandler onChangeCheckState;
        public PicNaviDlg()
        {
            InitializeComponent();
            this.Text = "图片顺序";
            this.flowLayoutPanel1.AutoScroll = true;
            this.Width = 900;
            this.Height = 600;
            onChangeCheckState = new EventHandler(checkBox1_CheckStateChanged);
            this.Load += new EventHandler(MultiPicNaviDlg_Load);
            this.btnSubmit.Click += new EventHandler(btnSubmit_Click);
            this.btnMoveFirst.Click += new EventHandler(btnMoveFirst_Click);
            this.btnMovePrev.Click += new EventHandler(btnMovePrev_Click);
            this.btnMoveNext.Click += new EventHandler(btnMoveNext_Click);
            this.btnMoveLast.Click += new EventHandler(btnMoveLast_Click);
        }

        enum MOVE_DIRECTION { FIRST, PREV, NEXT, LAST };
        void MovePic(MOVE_DIRECTION move)
        {
            int idx = -1;
            for (int i = 0; i < this.flowLayoutPanel1.Controls.Count; ++i)
            {
                PicBox pb = this.flowLayoutPanel1.Controls[i] as PicBox;
                if (pb.checkBox1.CheckState== CheckState.Checked)
                {
                    idx = i;
                    break;
                }
            }
            if (idx != -1)
            {
                int dest=-1;
                switch (move)
                {
                    case MOVE_DIRECTION.FIRST:
                        dest = 0;
                        break;
                    case MOVE_DIRECTION.PREV:
                        if (idx == 0)
                            dest = idx;
                        else
                            dest = idx - 1;
                        break;
                    case MOVE_DIRECTION.NEXT:
                        if (idx == this.flowLayoutPanel1.Controls.Count-1)
                            dest = idx;
                        else
                            dest = idx + 1;
                        break;
                    case MOVE_DIRECTION.LAST:
                        dest = this.flowLayoutPanel1.Controls.Count - 1;
                        break;
                    default:
                        break;
                }
                if (idx < dest)
                {
                    for (int i = idx; i < dest; ++i)
                    {
                        string f = ((PicBox)this.flowLayoutPanel1.Controls[i]).ImageFile;
                        ((PicBox)this.flowLayoutPanel1.Controls[i]).ImageFile =
                            ((PicBox)this.flowLayoutPanel1.Controls[i + 1]).ImageFile;
                        ((PicBox)this.flowLayoutPanel1.Controls[i + 1]).ImageFile = f;
                    }
                }
                else
                {
                    for (int i = idx; i > dest; --i)
                    {
                        string f = ((PicBox)this.flowLayoutPanel1.Controls[i]).ImageFile;
                        ((PicBox)this.flowLayoutPanel1.Controls[i]).ImageFile =
                            ((PicBox)this.flowLayoutPanel1.Controls[i - 1]).ImageFile;
                        ((PicBox)this.flowLayoutPanel1.Controls[i - 1]).ImageFile = f;
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择一张图片");
            }
        }

        void btnMoveFirst_Click(object sender, EventArgs e)
        {
            MovePic(MOVE_DIRECTION.FIRST);
        }

        void btnMovePrev_Click(object sender, EventArgs e)
        {
            MovePic(MOVE_DIRECTION.PREV);
        }

        void btnMoveNext_Click(object sender, EventArgs e)
        {
            MovePic(MOVE_DIRECTION.NEXT);
        }

        void btnMoveLast_Click(object sender, EventArgs e)
        {
            MovePic(MOVE_DIRECTION.LAST);
        }

        void btnSubmit_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.flowLayoutPanel1.Controls.Count;++i )
            {
                PicBox pb = this.flowLayoutPanel1.Controls[i] as PicBox;
                this.ImageFiles[i] = pb.ImageFile;
            }
            this.Close();
        }

        void MultiPicNaviDlg_Load(object sender, EventArgs e)
        {
            foreach (string file in ImageFiles)
            {
                PicBox pb = new PicBox();
                pb.ImageFile = file;
                pb.checkBox1.CheckStateChanged += this.onChangeCheckState;
                this.flowLayoutPanel1.Controls.Add(pb);
            }
        }

        void checkBox1_CheckStateChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb.Checked)
            {
                for (int i = 0; i < this.flowLayoutPanel1.Controls.Count; ++i)
                {
                    PicBox pb = this.flowLayoutPanel1.Controls[i] as PicBox;
                    if (pb.checkBox1 != cb)
                    {
                        pb.checkBox1.CheckStateChanged -= this.onChangeCheckState;
                        pb.checkBox1.CheckState = CheckState.Unchecked;
                        pb.checkBox1.CheckStateChanged += this.onChangeCheckState;
                    }
                }
            }
        }

      
        public string[] ImageFiles = new string[]{};
    }
}
