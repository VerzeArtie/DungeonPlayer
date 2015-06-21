using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class YesNoRequestMini : MotherForm
    {
        protected bool large = false;
        public bool Large
        {
            get { return large; }
            set { large = value; }
        }

        public YesNoRequestMini()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
        }

        private void buttonYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
        }

        private void YesNoRequestMini_Load(object sender, EventArgs e)
        {
            if (large)
            {
                buttonYes.Size = new Size(120, 60);
                buttonNo.Size = new Size(120, 60);
                buttonNo.Location = new Point(buttonNo.Location.X + 20, buttonNo.Location.Y);
                this.Size = new Size(240, 60);
            }

        }
    }
}
