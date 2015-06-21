using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    // [åxçê]ÅFSelectDungeonÇ∆óﬁéóÇµÇƒÇ®ÇËÅAîƒópê´ÇÃçÇÇ¢ÇtÇhÇ™é¶ç¥Ç≥ÇÍÇ‹Ç∑ÅB
    public partial class SelectTarget : MotherForm
    {
        protected int targetNum = 0;
        public int TargetNum
        {
            get { return targetNum; }
            set { targetNum = value; }
        }

        protected int maxSelectable = 2; // éÂêlåˆàÍêlÅAìGàÍêlÇ≈ç≈í·ÇÕÇQ
        public int MaxSelectable
        {
            get { return maxSelectable; }
            set { maxSelectable = value; }
        }

        protected string firstName = string.Empty;
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        protected string secondName = string.Empty;
        public string SecondName
        {
            get { return secondName; }
            set { secondName = value; }
        }
        protected string thirdName = string.Empty;
        public string ThirdName
        {
            get { return thirdName; }
            set { thirdName = value; }
        }
        protected string fourthName = string.Empty;
        public string FourthName
        {
            get { return fourthName; }
            set { fourthName = value; }
        }
        protected string fifthName = string.Empty;
        public string FifthName
        {
            get { return fifthName; }
            set { fifthName = value; }
        }

        protected bool ignoreEscCancel = false;
        public bool IgnoreEscCancel
        {
            get { return ignoreEscCancel; }
            set { ignoreEscCancel = value; }
        }

        public SelectTarget()
        {
            InitializeComponent();
        }

        private void SelectTarget_Load(object sender, EventArgs e)
        {
            if (maxSelectable == 1)
            {
                this.Controls.Remove(button2);
                this.Controls.Remove(button3);
                this.Controls.Remove(button4);
                this.Controls.Remove(button5);
                this.Size = new Size(112, 50);
            }
            else if (maxSelectable == 2)
            {
                this.Controls.Remove(button3);
                this.Controls.Remove(button4);
                this.Controls.Remove(button5);
                this.Size = new Size(112, 108);
            }
            else if (maxSelectable == 3)
            {
                this.Controls.Remove(button4);
                this.Controls.Remove(button5);
                this.Size = new Size(112, 157);
            }
            else if (maxSelectable == 4)
            {
                this.Controls.Remove(button5);
                this.Size = new Size(112, 210);
            }
            else if (maxSelectable == 5)
            {
                this.Size = new Size(112, 257);
            }

            if (firstName != string.Empty)
            {
                button1.Text = firstName;
            }
            if (secondName != string.Empty)
            {
                button2.Text = secondName;
            }
            if (thirdName != string.Empty)
            {
                button3.Text = thirdName;
            }
            if (fourthName != string.Empty)
            {
                button4.Text = fourthName;
            }
            if (fifthName != string.Empty)
            {
                button5.Text = fifthName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            targetNum = 1; // ÉAÉCÉì
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            targetNum = 2; // ÉâÉi
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            targetNum = 3; // ÉîÉFÉãÉ[
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            targetNum = 4; // ìG
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            targetNum = 5; // ìG2
            this.Close();
        }

        private void SelectTarget_KeyDown(object sender, KeyEventArgs e)
        {
            targetNum = 0; // É^Å[ÉQÉbÉgÇ»Çµ
            this.Close();
        }



    }
}