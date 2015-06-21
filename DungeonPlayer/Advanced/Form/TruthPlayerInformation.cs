using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class TruthPlayerInformation : Form
    {
        public string SetupMessage = String.Empty; 
        int messagePhase = 0; // メッセージを続けて連続で表示したい場合

        public TruthPlayerInformation()
        {
            InitializeComponent();
        }

        private void TruthPlayerInformation_Load(object sender, EventArgs e)
        {
            if (SetupMessage == String.Empty)
            {
                // 何もしない
            }
            else
            {
                mainMessage.Text = SetupMessage;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (SetupMessage == String.Empty)
            {
                if (messagePhase == 0)
                {
                    mainMessage.Text = "また、ダンジョン内のモンスターも出現しなくなります。";
                    messagePhase++;
                }
                else if (messagePhase == 1)
                {
                    mainMessage.Text = "引き続き、ダンジョンの進行を進めてください。";
                    messagePhase++;
                }
                else
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }
    }
}
