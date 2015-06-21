using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    // [警告]：SelectTargetと類似しており、汎用性の高いＵＩが示唆されます。
    // 拡張性を上げたあと、SelectTargetと統一していってください。
    public partial class SelectAction : MotherForm // 後編編集
    {
        protected int targetNum = -1;
        public int TargetNum
        {
            get { return targetNum; }
            set { targetNum = value; }
        }

        protected string elementA = String.Empty;
        public string ElementA
        {
            get { return elementA; }
            set { elementA = value; }
        }

        protected string elementB = String.Empty;
        public string ElementB
        {
            get { return elementB; }
            set { elementB = value; }
        }

        protected string elementC = String.Empty;
        public string ElementC
        {
            get { return elementC; }
            set { elementC = value; }
        }

        protected int forceChangeWidth = 0;
        public int ForceChangeWidth
        {
            get { return forceChangeWidth; }
            set { forceChangeWidth = value; }
        }

        public SelectAction()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            targetNum = 0; // つかう
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            targetNum = 1; // わたす・すてる
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            targetNum = 2; // すてる
            this.Close();
        }


        private void SelectAction_Load(object sender, EventArgs e)
        {
            if (elementA != string.Empty)
            {
                this.button1.Text = elementA;
            }
            if (elementB != string.Empty)
            {
                this.button3.Text = elementB;
            }
            if (elementC != string.Empty)
            {
                this.button2.Text = elementC;
            }
            else
            {
                this.button2.Visible = false;
                this.Height -= 50;
            }
            if (forceChangeWidth != 0)
            {
                this.Width = forceChangeWidth;
                this.button1.Width = forceChangeWidth - 24;
                this.button3.Width = forceChangeWidth - 24;
                this.button2.Width = forceChangeWidth - 24;
            }
        }

        private bool isShift;
        public bool IsShift
        {
            get { return isShift; }
            set { isShift = value; }
        }

        private void SelectAction_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                targetNum = -1; // なにもしない
                this.Close();
            }
            else if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey)
            {
                this.isShift = true;
            }
        }

        private void SelectAction_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey)
            {
                this.isShift = false;
            }

        }
    }
}