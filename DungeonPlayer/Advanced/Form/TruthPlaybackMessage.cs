using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class TruthPlaybackMessage : MotherForm
    {
        public enum infoStyle
        {
            normal,
            notify,
            scene,
        };

        const int MAX_LIST = 100;
        const int VIEW_LIST = 10;
        const int HEIGHT = 76;
        public List<string> messageList = new List<string>();
        public List<infoStyle> infoStyleList = new List<infoStyle>();
        protected Label[] labelList = new Label[MAX_LIST];
        protected int scrollPosition = 0;
        public TruthPlaybackMessage()
        {
            InitializeComponent();

            for (int ii = 0; ii < MAX_LIST; ii++)
            {
                labelList[ii] = new Label();
                labelList[ii].Location = new Point(0, (Database.HEIGHT_768 - HEIGHT) - ii * HEIGHT);
                labelList[ii].Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                labelList[ii].Text = "";
                labelList[ii].Size = new Size(1024, HEIGHT-1);
                labelList[ii].Name = "label" + ii.ToString();
                labelList[ii].TabIndex = ii;
                labelList[ii].BackColor = Color.LightYellow;
                labelList[ii].Visible = false;
                this.Controls.Add(labelList[ii]);
            }
        }

        void SetupLabelStyle(infoStyle style, ref Label lbl)
        {
            if (style == infoStyle.notify)
            {
                lbl.BackColor = Color.LightPink;
                lbl.TextAlign = ContentAlignment.MiddleRight;
            }
            else if (style == infoStyle.scene)
            {
                lbl.BackColor = Color.LightGreen;
                lbl.TextAlign = ContentAlignment.MiddleCenter;
            }
            else
            {
                lbl.BackColor = Color.LightYellow;
                lbl.TextAlign = ContentAlignment.MiddleLeft;
            }

        }
        private void TruthPlaybackMessage_Load(object sender, EventArgs e)
        {
            for (int ii = 0; ii < this.messageList.Count; ii++)
            {
                labelList[ii].Text = this.messageList[ii];
                SetupLabelStyle(infoStyleList[ii], ref labelList[ii]);
                labelList[ii].Visible = true;
            }
        }

        void ScrollUp()
        {
            if (this.scrollPosition + VIEW_LIST >= this.messageList.Count) { return; }
            this.scrollPosition++;
            for (int ii = 0; ii < VIEW_LIST; ii++)
            {
                labelList[ii].Text = messageList[ii + scrollPosition];
                SetupLabelStyle(infoStyleList[ii + scrollPosition], ref labelList[ii]);
            }
        }

        void ScrollDown()
        {
            if (this.scrollPosition <= 0) { return; }
            this.scrollPosition--;
            for (int ii = 0; ii < VIEW_LIST; ii++)
            {
                labelList[ii].Text = messageList[ii + scrollPosition];
                SetupLabelStyle(infoStyleList[ii + scrollPosition], ref labelList[ii]);
            }
        }

        void TruthPlaybackMessage_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Delta == 0) { return; }
            if (e.Delta < 0)
            {
                ScrollUp();
            }
            else
            {
                ScrollDown();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 上へスクロール
            ScrollUp();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // 下へスクロール
            ScrollDown();
        }
    }
}
