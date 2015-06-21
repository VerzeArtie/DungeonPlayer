using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class TruthDecision : MotherForm
    {
        public string MainMessage { get; set; }
        public string FirstMessage { get; set; }
        public string SecondMessage { get; set; }

        public TruthDecision()
        {
            InitializeComponent();
        }

        private void TruthDecision_Load(object sender, EventArgs e)
        {
            mainMessage.Text = this.MainMessage;
            button1.Text = "１　　" + this.FirstMessage;
            button2.Text = "２　　" + this.SecondMessage;
        }

        // 1
        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
        }

        // 2
        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
        }
    }
}
