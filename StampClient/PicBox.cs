using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Stamp4Fun.StampClient
{
    public partial class PicBox : UserControl
    {
        public PicBox()
        {
            InitializeComponent();
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

        }

        public string ImageFile
        {
            get { return this.pictureBox1.ImageLocation; }
            set
            {
                this.pictureBox1.ImageLocation = value;
                this.checkBox1.Text = (new FileInfo(this.ImageFile)).Name;
            }
        }
    }
}
