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

        // [警告]：一般化を求めるには、MC,SC,TCオブジェクトは呼び出し元にして、パラメタ確認はこちらで実装するべきではありません。
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

        // s 後編追加
        protected int adjustWidth = 105;
        public int AdjustWidth
        {
            get { return adjustWidth; }
            set { adjustWidth = value; }
        }
        // e 後編追加

        public SelectDungeon()
        {
            InitializeComponent();
        }

        // 配列化しても良いが、要素が少なくシンプルであるため、芋プログラミングのままとする。

        private void SelectDungeon_Load(object sender, EventArgs e)
        {
            // s 後編追加
            this.button1.Width = adjustWidth - 25;
            this.button2.Width = adjustWidth - 25;
            this.button3.Width = adjustWidth - 25;
            this.button4.Width = adjustWidth - 25;
            this.button5.Width = adjustWidth - 25;
            // e 後編追加

            if (maxSelectable == 1)
            {
                this.Controls.Remove(button2);
                this.Controls.Remove(button3);
                this.Controls.Remove(button4);
                this.Controls.Remove(button5);
                this.Size = new Size(adjustWidth, 50); // c 後編編集
            }
            else if (maxSelectable == 2)
            {
                this.Controls.Remove(button3);
                this.Controls.Remove(button4);
                this.Controls.Remove(button5);
                this.Size = new Size(adjustWidth, 95); // c 後編編集
            }
            else if (maxSelectable == 3)
            {
                this.Controls.Remove(button4);
                this.Controls.Remove(button5);
                this.Size = new Size(adjustWidth, 140); // c 後編編集
            }
            else if (maxSelectable == 4)
            {
                this.Controls.Remove(button5);
                this.Size = new Size(adjustWidth, 185); // c 後編編集
            }
            else if (maxSelectable == 5)
            {
                this.Size = new Size(adjustWidth, 230); // c 後編編集
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

        // [警告]：ココはダンジョン用に記載したものではなく、ステータスプレイヤー画面より、ヒールする時のライフ確認で実装したものです。
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
            // s 後編編集
            System.OperatingSystem os = System.Environment.OSVersion;
            int osNumber = os.Version.Major;
            if (osNumber != 5)
            {
                popupInfo.Opacity = 0.7f;
            }
            //popupInfo.Opacity = 0.7f; // 後編削除
            //popupInfo.PopupTextColor = Brushes.White; // 後編削除
            // e 後編編集
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