using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class TruthWill : Form
    {
        Button[] buttonData;
        int selectCounter = 0;

        public TruthWill()
        {
            InitializeComponent();
            buttonData = new System.Windows.Forms.Button[17];
            buttonData[0] = button10;
            buttonData[1] = button15;
            buttonData[2] = button9;
            buttonData[3] = button12;
            buttonData[4] = button17;
            buttonData[5] = button8;
            buttonData[6] = button16;
            buttonData[7] = button5;
            buttonData[8] = button13;
            buttonData[9] = button14;
            buttonData[10] = button11;
            buttonData[11] = button4;
            buttonData[12] = button1;
            buttonData[13] = button7;
            buttonData[14] = button2;
            buttonData[15] = button6;
            buttonData[16] = button3;

            for (int ii = 0; ii < 17; ii++)
            {
                buttonData[ii].Enabled = true;
            }
            button1.Select();
            button1.Focus();

            this.SetStyle(ControlStyles.UserPaint, true);
            this.Invalidate();
        }

        bool failFlag = false;
        private void button11_Click(object sender, EventArgs e)
        {
            ((Button)sender).Enabled = false;
            if (buttonData[selectCounter].Equals((Button)sender) == false)
            {
                failFlag = true;
            }
            selectCounter++;

            if (selectCounter >= 4) { this.BackColor = Color.YellowGreen; for (int ii = 0; ii < 17; ii++) { buttonData[ii].ForeColor = Color.Black; } }
            if (selectCounter >= 9) { this.BackColor = Color.MediumPurple; for (int ii = 0; ii < 17; ii++) { buttonData[ii].ForeColor = Color.White; } }
            if (selectCounter >= 14) { this.BackColor = Color.White; for (int ii = 0; ii < 17; ii++) { buttonData[ii].ForeColor = Color.Black; } }
            for (int ii = 0; ii < 17; ii++)
            {
                if (buttonData[ii].Enabled) return;
            }

            if (failFlag) this.DialogResult = System.Windows.Forms.DialogResult.No;
            else this.DialogResult = System.Windows.Forms.DialogResult.Yes;
        }
    }
}
