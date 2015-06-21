using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class YesNoReqWithMessage : MotherForm
    {
        public string MainMessage = string.Empty;

        public YesNoReqWithMessage()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
        }

        private void YesNoReqWithMessage_Load(object sender, EventArgs e)
        {
            this.mainMessage.Text = MainMessage;
        }
    }
}