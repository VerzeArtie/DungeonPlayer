using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class MotherForm : Form
    {
        public MotherForm()
        {
            InitializeComponent();
        }

        protected override void WndProc(ref Message m)
        {
            const int WM_SYSCOMMAND = 0x112;
            const int SC_CLOSE = 0xF060;


            if (m.Msg == WM_SYSCOMMAND && m.WParam.ToInt32() == SC_CLOSE)
            {
                if (!GroundOne.WE2.RealWorld)
                {
                    Application.Exit();
                }
                else
                {
                    if (GroundOne.WE2.SeekerEvent507)
                    {
                        Application.Exit();
                    }
                }
                return;
            }
            base.WndProc(ref m);
        }

        private void MotherForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F1:
                    if (GroundOne.information == null)
                    {
                        GroundOne.information = new TruthInformation();
                        GroundOne.information.StartPosition = FormStartPosition.CenterParent;
                        GroundOne.information.ShowDialog();
                        GroundOne.information = null;
                        GC.Collect();
                    }
                    break;
                case Keys.F2:
                    if (GroundOne.playback == null)
                    {
                        GroundOne.playback = new TruthPlaybackMessage();
                        GroundOne.playback.StartPosition = FormStartPosition.CenterParent;
                        GroundOne.playback.messageList = GroundOne.playbackMessage;
                        GroundOne.playback.infoStyleList = GroundOne.playbackInfoStyle;
                        GroundOne.playback.ShowDialog();
                        GroundOne.playback = null;
                        GC.Collect();
                    }
                    break;
            }
        }
    }
}
