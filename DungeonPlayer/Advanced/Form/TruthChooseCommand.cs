using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class TruthChooseCommand : Form
    {
        protected string chooseCommand = String.Empty;
        public string ChooseCommand
        {
            get { return chooseCommand; }
        }

        Button[] btnCommand;
        public TruthChooseCommand()
        {
            InitializeComponent();
        }

        private void TruthChooseCommand_Load(object sender, EventArgs e)
        {
            string[] spellList = TruthActionCommand.GetSpellList();

            btnCommand = new Button[Database.TOTAL_SPELL_NUM];
            const int COLUMN = 21;
            const int WIDTH = 150;
            const int HEIGHT = 27;
            for (int ii = 0; ii < btnCommand.Length; ii++)
            {
                btnCommand[ii] = new Button();
                btnCommand[ii].BackColor = System.Drawing.Color.LightCyan;
                btnCommand[ii].FlatStyle = System.Windows.Forms.FlatStyle.Flat;
                btnCommand[ii].Font = new System.Drawing.Font("Meiryo UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                btnCommand[ii].Location = new System.Drawing.Point(12 + (ii / COLUMN) * WIDTH, 12 + (ii % COLUMN) * HEIGHT);
                btnCommand[ii].Margin = new System.Windows.Forms.Padding(0);
                btnCommand[ii].Name = "command"+(ii+1).ToString();
                btnCommand[ii].Text = spellList[ii];
                btnCommand[ii].Size = new System.Drawing.Size(150, 27);
                btnCommand[ii].TabIndex = ii+1;
                btnCommand[ii].UseVisualStyleBackColor = false;
                btnCommand[ii].MouseClick += new MouseEventHandler(TruthChooseCommand_MouseClick);
                this.Controls.Add(btnCommand[ii]);
            }
        }

        void TruthChooseCommand_MouseClick(object sender, MouseEventArgs e)
        {
            this.chooseCommand = ((Button)sender).Text;
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
