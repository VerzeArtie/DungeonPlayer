using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class OKRequest : MotherForm
    {
        protected int layoutType = 0;
        public int LayoutType
        {
            get { return layoutType; }
            set { layoutType = value; }
        }

        protected bool blackImage = false;
        public bool BlackImage
        {
            get { return blackImage; }
            set { blackImage = value; }
        }

        public OKRequest()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OKRequest_Load(object sender, EventArgs e)
        {
            if (this.LayoutType == 1)
            {
                this.MinimumSize = new Size(10, 10);
                this.Size = new Size(121, 33);
                this.button1.Size = new Size(121, 33);
                this.button1.Text = "";
                this.button1.Image = Image.FromFile(Database.BaseResourceFolder + Database.OK_BUTTON_IMAGE);
            }

            if (this.blackImage)
            {
                this.button1.Image = Image.FromFile(Database.BaseResourceFolder + Database.OK_BUTTON_IMAGE_BLACK);
            }
        }
    }
}