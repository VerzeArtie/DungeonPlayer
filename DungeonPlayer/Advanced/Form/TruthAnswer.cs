using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class TruthAnswer : MotherForm
    {
        System.Windows.Forms.Button[] buttonData;
        int selectCounter = 0;

        public TruthAnswer()
        {
            InitializeComponent();
            buttonData = new System.Windows.Forms.Button[15];
            buttonData[0] = button1;
            buttonData[1] = button2;
            buttonData[2] = button3;
            buttonData[3] = button4;
            buttonData[4] = button5;
            buttonData[5] = button6;
            buttonData[6] = button7;
            buttonData[7] = button8;
            buttonData[8] = button9;
            buttonData[9] = button10;
            buttonData[10] = button11;
            buttonData[11] = button12;
            buttonData[12] = button13;
            buttonData[13] = button14;
            buttonData[14] = button15;

            for (int ii = 0; ii < 15; ii++)
            {
                buttonData[ii].Enabled = true;
            }

            button11.Select();
            button11.Focus();

            this.SetStyle(ControlStyles.UserPaint, true);
            this.Invalidate();

        }

        bool failFlag = false;
        private void button1_Click(object sender, EventArgs e)
        {
            ((Button)sender).Enabled = false;
            if (buttonData[selectCounter].Equals((Button)sender) == false)
            {
                failFlag = true;
            }
            selectCounter++;

            if (selectCounter >= 5) { this.BackColor = Color.Red; for (int ii = 0; ii < 15; ii++) { buttonData[ii].ForeColor = Color.White; } }
            if (selectCounter >= 10) { this.BackColor = Color.DarkSlateGray; for (int ii = 0; ii < 15; ii++) { buttonData[ii].ForeColor = Color.White; } }
            if (selectCounter >= 13) { this.BackColor = Color.White; for (int ii = 0; ii < 15; ii++) { buttonData[ii].ForeColor = Color.Black; } }
            for (int ii = 0; ii < 15; ii++)
            {
                if (buttonData[ii].Enabled) return;
            }

            if (failFlag) this.DialogResult = System.Windows.Forms.DialogResult.No;
            else this.DialogResult = System.Windows.Forms.DialogResult.Yes;
        }
    }
}
