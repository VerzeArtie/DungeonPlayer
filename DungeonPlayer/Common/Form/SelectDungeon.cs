using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class SelectDungeon : MotherForm
    {
        protected int targetDungeon = -1;
        public int TargetDungeon
        {
            get { return targetDungeon; }
            set { targetDungeon = value; }
        }

        protected int maxSelectable = 1;
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

        // [�x��]�F��ʉ������߂�ɂ́AMC,SC,TC�I�u�W�F�N�g�͌Ăяo�����ɂ��āA�p�����^�m�F�͂�����Ŏ�������ׂ��ł͂���܂���B
        protected bool enablePopUpInfo = false;
        public bool EnablePopUpInfo
        {
            get { return enablePopUpInfo; }
            set { enablePopUpInfo = value; }
        }
        protected MainCharacter mc = null;
        public MainCharacter MC
        {
            get { return mc; }
            set { mc = value; }
        }
        protected MainCharacter sc = null;
        public MainCharacter SC
        {
            get { return sc; }
            set { sc = value; }
        }
        protected MainCharacter tc = null;
        public MainCharacter TC
        {
            get { return tc; }
            set { tc = value; }
        }

        // s ��Ғǉ�
        protected int adjustWidth = 105;
        public int AdjustWidth
        {
            get { return adjustWidth; }
            set { adjustWidth = value; }
        }
        // e ��Ғǉ�

        public SelectDungeon()
        {
            InitializeComponent();
        }

        // �z�񉻂��Ă��ǂ����A�v�f�����Ȃ��V���v���ł��邽�߁A���v���O���~���O�̂܂܂Ƃ���B

        private void SelectDungeon_Load(object sender, EventArgs e)
        {
            // s ��Ғǉ�
            this.button1.Width = adjustWidth - 25;
            this.button2.Width = adjustWidth - 25;
            this.button3.Width = adjustWidth - 25;
            this.button4.Width = adjustWidth - 25;
            this.button5.Width = adjustWidth - 25;
            // e ��Ғǉ�

            if (maxSelectable == 1)
            {
                this.Controls.Remove(button2);
                this.Controls.Remove(button3);
                this.Controls.Remove(button4);
                this.Controls.Remove(button5);
                this.Size = new Size(adjustWidth, 50); // c ��ҕҏW
            }
            else if (maxSelectable == 2)
            {
                this.Controls.Remove(button3);
                this.Controls.Remove(button4);
                this.Controls.Remove(button5);
                this.Size = new Size(adjustWidth, 95); // c ��ҕҏW
            }
            else if (maxSelectable == 3)
            {
                this.Controls.Remove(button4);
                this.Controls.Remove(button5);
                this.Size = new Size(adjustWidth, 140); // c ��ҕҏW
            }
            else if (maxSelectable == 4)
            {
                this.Controls.Remove(button5);
                this.Size = new Size(adjustWidth, 185); // c ��ҕҏW
            }
            else if (maxSelectable == 5)
            {
                this.Size = new Size(adjustWidth, 230); // c ��ҕҏW
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

            if (this.enablePopUpInfo)
            {
                button1.MouseMove += new MouseEventHandler(button_MouseMove);
                button2.MouseMove += new MouseEventHandler(button_MouseMove);
                button3.MouseMove += new MouseEventHandler(button_MouseMove);
                button4.MouseMove += new MouseEventHandler(button_MouseMove);
                button5.MouseMove += new MouseEventHandler(button_MouseMove);

                button1.MouseLeave += new EventHandler(button_MouseLeave);
                button2.MouseLeave += new EventHandler(button_MouseLeave);
                button3.MouseLeave += new EventHandler(button_MouseLeave);
                button4.MouseLeave += new EventHandler(button_MouseLeave);
                button5.MouseLeave += new EventHandler(button_MouseLeave);
            }
        }
    
        private void button1_Click(object sender, EventArgs e)
        {
            this.targetDungeon = 1;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.targetDungeon = 2;
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.targetDungeon = 3;
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.targetDungeon = 4;
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.targetDungeon = 5;
            this.Close();
        }

        private void SelectDungeon_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.targetDungeon = -1;
                this.Close();
            }
        }

        // [�x��]�F�R�R�̓_���W�����p�ɋL�ڂ������̂ł͂Ȃ��A�X�e�[�^�X�v���C���[��ʂ��A�q�[�����鎞�̃��C�t�m�F�Ŏ����������̂ł��B
        private PopUpMini popupInfo = null;
        void button_MouseMove(object sender, MouseEventArgs e)
        {
            if (popupInfo == null)
            {
                popupInfo = new PopUpMini();
            }

            popupInfo.StartPosition = FormStartPosition.Manual;
            popupInfo.Location = new Point(this.Location.X + ((Button)sender).Location.X + e.X + 10, this.Location.Y + ((Button)sender).Location.Y + e.Y + 10);
            popupInfo.PopupColor = Color.Black;
            // s ��ҕҏW
            System.OperatingSystem os = System.Environment.OSVersion;
            int osNumber = os.Version.Major;
            if (osNumber != 5)
            {
                popupInfo.Opacity = 0.7f;
            }
            //popupInfo.Opacity = 0.7f; // ��ҍ폜
            //popupInfo.PopupTextColor = Brushes.White; // ��ҍ폜
            // e ��ҕҏW
            if (sender == button1)
            {
                popupInfo.CurrentInfo = mc.CurrentLife.ToString() + " / " + mc.MaxLife.ToString();
            }
            else if (sender == button2)
            {
                popupInfo.CurrentInfo = sc.CurrentLife.ToString() + " / " + sc.MaxLife.ToString();
            }
            else if (sender == button3)
            {
                popupInfo.CurrentInfo = tc.CurrentLife.ToString() + " / " + tc.MaxLife.ToString();
            }
            popupInfo.Show();
        }

        void button_MouseLeave(object sender, EventArgs e)
        {
            if (popupInfo != null)
            {
                popupInfo.Close();
                popupInfo = null;
            }
        }

    }
}