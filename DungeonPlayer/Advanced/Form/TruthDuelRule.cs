using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class TruthDuelRule : MotherForm // 後編編集
    {
        public TruthDuelRule()
        {
            InitializeComponent();
        }

        private void TruthDuelRule_MouseClick(object sender, MouseEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void TruthDuelRule_KeyDown(object sender, KeyEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

        }

        private void label1_MouseClick(object sender, MouseEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void label4_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
