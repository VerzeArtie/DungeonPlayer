using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace DungeonPlayer
{
    public partial class MessageDisplay : MotherForm
    {
        Thread th;
        
        protected string message;
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

        protected bool needAnimation = false;
        public bool NeedAnimation
        {
            get { return needAnimation; }
            set { needAnimation = value; }
        }

        protected bool endSign = false;
        protected int basePosY = 0;

        private int autoKillTimer = 0;
        public int AutoKillTimer
        {
            get { return autoKillTimer; }
            set { autoKillTimer = value; }
        }

        public MessageDisplay()
        {
            InitializeComponent();
        }

        private void MessageDisplay_Load(object sender, EventArgs e)
        {
            label1.Text = message;

            if (needAnimation)
            {
                th = new System.Threading.Thread(new System.Threading.ThreadStart(UpdateMessagePosition));
                th.Start();
                th.IsBackground = true;
            }
            this.basePosY = this.Location.Y;

            if (this.autoKillTimer > 0)
            {
                timerAutoKill.Interval = autoKillTimer;
                timerAutoKill.Enabled = true;
                timerAutoKill.Start();
            }
        }

        private void MessageDisplay_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Escape) || (e.KeyCode == Keys.Enter) || (e.KeyCode == Keys.Space))
            {
                if (needAnimation)
                {
                    endSign = true;
                }
                else
                {
                    this.Close();
                }
            }
        }

        void UpdateMessagePosition()
        {
            int counter = 0;
            while (true)
            {
                System.Threading.Thread.Sleep(1);
                if (!endSign)
                {
                    if (counter <= 30)
                    {
                        this.Location = new Point(this.Location.X, this.Location.Y - 10);
                    }
                }
                else
                {
                    if (this.Location.Y <= 10)
                    {
                        this.endSign = false;
                        this.Location = new Point(this.Location.X, this.basePosY);
                        this.Close();
                        break;
                    }
                    else
                    {
                        this.Location = new Point(this.Location.X, this.Location.Y - 10);
                    }
                }
                counter++;
            }
        }

        private void MessageDisplay_MouseDown(object sender, MouseEventArgs e)
        {
            if (needAnimation)
            {
                endSign = true;
            }
            else
            {
                this.Close();
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (needAnimation)
            {
                endSign = true;
            }
            else
            {
                this.Close();
            }
        }

        private void timerAutoKill_Tick(object sender, EventArgs e)
        {
            timerAutoKill.Stop();
            timerAutoKill.Enabled = false;
            this.Close();
        }
    }
}