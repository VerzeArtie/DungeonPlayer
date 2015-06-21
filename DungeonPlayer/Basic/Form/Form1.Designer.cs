namespace DungeonPlayer
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Player = new System.Windows.Forms.PictureBox();
            this.mainMessage = new System.Windows.Forms.Label();
            this.dayLabel = new System.Windows.Forms.Label();
            this.FirstPlayerPanel = new System.Windows.Forms.Panel();
            this.MP1 = new System.Windows.Forms.Label();
            this.SP1 = new System.Windows.Forms.Label();
            this.HP1 = new System.Windows.Forms.Label();
            this.currentManaPoint1 = new System.Windows.Forms.Label();
            this.currentSkillPoint1 = new System.Windows.Forms.Label();
            this.backManaPoint1 = new System.Windows.Forms.Label();
            this.currentLife1 = new System.Windows.Forms.Label();
            this.backSkillPoint1 = new System.Windows.Forms.Label();
            this.FirstPlayerName = new System.Windows.Forms.Label();
            this.backLife1 = new System.Windows.Forms.Label();
            this.SecondPlayerPanel = new System.Windows.Forms.Panel();
            this.MP2 = new System.Windows.Forms.Label();
            this.SP2 = new System.Windows.Forms.Label();
            this.HP2 = new System.Windows.Forms.Label();
            this.currentManaPoint2 = new System.Windows.Forms.Label();
            this.currentSkillPoint2 = new System.Windows.Forms.Label();
            this.backManaPoint2 = new System.Windows.Forms.Label();
            this.currentLife2 = new System.Windows.Forms.Label();
            this.backSkillPoint2 = new System.Windows.Forms.Label();
            this.SecondPlayerName = new System.Windows.Forms.Label();
            this.backLife2 = new System.Windows.Forms.Label();
            this.ThirdPlayerPanel = new System.Windows.Forms.Panel();
            this.MP3 = new System.Windows.Forms.Label();
            this.SP3 = new System.Windows.Forms.Label();
            this.HP3 = new System.Windows.Forms.Label();
            this.currentManaPoint3 = new System.Windows.Forms.Label();
            this.currentSkillPoint3 = new System.Windows.Forms.Label();
            this.backManaPoint3 = new System.Windows.Forms.Label();
            this.currentLife3 = new System.Windows.Forms.Label();
            this.backSkillPoint3 = new System.Windows.Forms.Label();
            this.ThirdPlayerName = new System.Windows.Forms.Label();
            this.backLife3 = new System.Windows.Forms.Label();
            this.dungeonAreaLabel = new System.Windows.Forms.Label();
            this.labelVigilance = new System.Windows.Forms.Label();
            this.dungeonField = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Player)).BeginInit();
            this.FirstPlayerPanel.SuspendLayout();
            this.SecondPlayerPanel.SuspendLayout();
            this.ThirdPlayerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dungeonField)).BeginInit();
            this.dungeonField.SuspendLayout();
            this.SuspendLayout();
            // 
            // Player
            // 
            this.Player.Location = new System.Drawing.Point(1, 1);
            this.Player.Name = "Player";
            this.Player.Size = new System.Drawing.Size(14, 14);
            this.Player.TabIndex = 1;
            this.Player.TabStop = false;
            // 
            // mainMessage
            // 
            this.mainMessage.BackColor = System.Drawing.Color.GhostWhite;
            this.mainMessage.Location = new System.Drawing.Point(0, 440);
            this.mainMessage.Name = "mainMessage";
            this.mainMessage.Size = new System.Drawing.Size(640, 40);
            this.mainMessage.TabIndex = 4;
            this.mainMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dayLabel
            // 
            this.dayLabel.BackColor = System.Drawing.Color.GhostWhite;
            this.dayLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dayLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dayLabel.Location = new System.Drawing.Point(12, 9);
            this.dayLabel.Name = "dayLabel";
            this.dayLabel.Size = new System.Drawing.Size(80, 37);
            this.dayLabel.TabIndex = 5;
            this.dayLabel.Text = "１日目";
            this.dayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // FirstPlayerPanel
            // 
            this.FirstPlayerPanel.BackColor = System.Drawing.Color.LightSkyBlue;
            this.FirstPlayerPanel.Controls.Add(this.MP1);
            this.FirstPlayerPanel.Controls.Add(this.SP1);
            this.FirstPlayerPanel.Controls.Add(this.HP1);
            this.FirstPlayerPanel.Controls.Add(this.currentManaPoint1);
            this.FirstPlayerPanel.Controls.Add(this.currentSkillPoint1);
            this.FirstPlayerPanel.Controls.Add(this.backManaPoint1);
            this.FirstPlayerPanel.Controls.Add(this.currentLife1);
            this.FirstPlayerPanel.Controls.Add(this.backSkillPoint1);
            this.FirstPlayerPanel.Controls.Add(this.FirstPlayerName);
            this.FirstPlayerPanel.Controls.Add(this.backLife1);
            this.FirstPlayerPanel.Location = new System.Drawing.Point(12, 85);
            this.FirstPlayerPanel.Name = "FirstPlayerPanel";
            this.FirstPlayerPanel.Size = new System.Drawing.Size(134, 73);
            this.FirstPlayerPanel.TabIndex = 6;
            // 
            // MP1
            // 
            this.MP1.AutoSize = true;
            this.MP1.Location = new System.Drawing.Point(4, 53);
            this.MP1.Name = "MP1";
            this.MP1.Size = new System.Drawing.Size(21, 12);
            this.MP1.TabIndex = 2;
            this.MP1.Text = "MP";
            // 
            // SP1
            // 
            this.SP1.AutoSize = true;
            this.SP1.Location = new System.Drawing.Point(4, 39);
            this.SP1.Name = "SP1";
            this.SP1.Size = new System.Drawing.Size(19, 12);
            this.SP1.TabIndex = 2;
            this.SP1.Text = "SP";
            // 
            // HP1
            // 
            this.HP1.AutoSize = true;
            this.HP1.Location = new System.Drawing.Point(4, 25);
            this.HP1.Name = "HP1";
            this.HP1.Size = new System.Drawing.Size(20, 12);
            this.HP1.TabIndex = 2;
            this.HP1.Text = "HP";
            // 
            // currentManaPoint1
            // 
            this.currentManaPoint1.BackColor = System.Drawing.Color.Crimson;
            this.currentManaPoint1.Location = new System.Drawing.Point(27, 53);
            this.currentManaPoint1.Name = "currentManaPoint1";
            this.currentManaPoint1.Size = new System.Drawing.Size(100, 10);
            this.currentManaPoint1.TabIndex = 1;
            // 
            // currentSkillPoint1
            // 
            this.currentSkillPoint1.BackColor = System.Drawing.Color.DarkGreen;
            this.currentSkillPoint1.Location = new System.Drawing.Point(27, 39);
            this.currentSkillPoint1.Name = "currentSkillPoint1";
            this.currentSkillPoint1.Size = new System.Drawing.Size(100, 10);
            this.currentSkillPoint1.TabIndex = 1;
            // 
            // backManaPoint1
            // 
            this.backManaPoint1.BackColor = System.Drawing.Color.White;
            this.backManaPoint1.Location = new System.Drawing.Point(27, 53);
            this.backManaPoint1.Name = "backManaPoint1";
            this.backManaPoint1.Size = new System.Drawing.Size(100, 10);
            this.backManaPoint1.TabIndex = 1;
            // 
            // currentLife1
            // 
            this.currentLife1.BackColor = System.Drawing.Color.Blue;
            this.currentLife1.Location = new System.Drawing.Point(27, 25);
            this.currentLife1.Name = "currentLife1";
            this.currentLife1.Size = new System.Drawing.Size(100, 10);
            this.currentLife1.TabIndex = 1;
            // 
            // backSkillPoint1
            // 
            this.backSkillPoint1.BackColor = System.Drawing.Color.White;
            this.backSkillPoint1.Location = new System.Drawing.Point(27, 39);
            this.backSkillPoint1.Name = "backSkillPoint1";
            this.backSkillPoint1.Size = new System.Drawing.Size(100, 10);
            this.backSkillPoint1.TabIndex = 1;
            // 
            // FirstPlayerName
            // 
            this.FirstPlayerName.AutoSize = true;
            this.FirstPlayerName.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FirstPlayerName.Location = new System.Drawing.Point(8, 5);
            this.FirstPlayerName.Name = "FirstPlayerName";
            this.FirstPlayerName.Size = new System.Drawing.Size(113, 13);
            this.FirstPlayerName.TabIndex = 0;
            this.FirstPlayerName.Text = "アイン・ウォーレンス";
            // 
            // backLife1
            // 
            this.backLife1.BackColor = System.Drawing.Color.White;
            this.backLife1.Location = new System.Drawing.Point(27, 25);
            this.backLife1.Name = "backLife1";
            this.backLife1.Size = new System.Drawing.Size(100, 10);
            this.backLife1.TabIndex = 1;
            // 
            // SecondPlayerPanel
            // 
            this.SecondPlayerPanel.BackColor = System.Drawing.Color.Pink;
            this.SecondPlayerPanel.Controls.Add(this.MP2);
            this.SecondPlayerPanel.Controls.Add(this.SP2);
            this.SecondPlayerPanel.Controls.Add(this.HP2);
            this.SecondPlayerPanel.Controls.Add(this.currentManaPoint2);
            this.SecondPlayerPanel.Controls.Add(this.currentSkillPoint2);
            this.SecondPlayerPanel.Controls.Add(this.backManaPoint2);
            this.SecondPlayerPanel.Controls.Add(this.currentLife2);
            this.SecondPlayerPanel.Controls.Add(this.backSkillPoint2);
            this.SecondPlayerPanel.Controls.Add(this.SecondPlayerName);
            this.SecondPlayerPanel.Controls.Add(this.backLife2);
            this.SecondPlayerPanel.Location = new System.Drawing.Point(12, 164);
            this.SecondPlayerPanel.Name = "SecondPlayerPanel";
            this.SecondPlayerPanel.Size = new System.Drawing.Size(134, 73);
            this.SecondPlayerPanel.TabIndex = 6;
            // 
            // MP2
            // 
            this.MP2.AutoSize = true;
            this.MP2.Location = new System.Drawing.Point(4, 53);
            this.MP2.Name = "MP2";
            this.MP2.Size = new System.Drawing.Size(21, 12);
            this.MP2.TabIndex = 2;
            this.MP2.Text = "MP";
            // 
            // SP2
            // 
            this.SP2.AutoSize = true;
            this.SP2.Location = new System.Drawing.Point(4, 39);
            this.SP2.Name = "SP2";
            this.SP2.Size = new System.Drawing.Size(19, 12);
            this.SP2.TabIndex = 2;
            this.SP2.Text = "SP";
            // 
            // HP2
            // 
            this.HP2.AutoSize = true;
            this.HP2.Location = new System.Drawing.Point(4, 25);
            this.HP2.Name = "HP2";
            this.HP2.Size = new System.Drawing.Size(20, 12);
            this.HP2.TabIndex = 2;
            this.HP2.Text = "HP";
            // 
            // currentManaPoint2
            // 
            this.currentManaPoint2.BackColor = System.Drawing.Color.Crimson;
            this.currentManaPoint2.Location = new System.Drawing.Point(27, 53);
            this.currentManaPoint2.Name = "currentManaPoint2";
            this.currentManaPoint2.Size = new System.Drawing.Size(100, 10);
            this.currentManaPoint2.TabIndex = 1;
            // 
            // currentSkillPoint2
            // 
            this.currentSkillPoint2.BackColor = System.Drawing.Color.DarkGreen;
            this.currentSkillPoint2.Location = new System.Drawing.Point(27, 39);
            this.currentSkillPoint2.Name = "currentSkillPoint2";
            this.currentSkillPoint2.Size = new System.Drawing.Size(100, 10);
            this.currentSkillPoint2.TabIndex = 1;
            // 
            // backManaPoint2
            // 
            this.backManaPoint2.BackColor = System.Drawing.Color.White;
            this.backManaPoint2.Location = new System.Drawing.Point(27, 53);
            this.backManaPoint2.Name = "backManaPoint2";
            this.backManaPoint2.Size = new System.Drawing.Size(100, 10);
            this.backManaPoint2.TabIndex = 1;
            // 
            // currentLife2
            // 
            this.currentLife2.BackColor = System.Drawing.Color.Blue;
            this.currentLife2.Location = new System.Drawing.Point(27, 25);
            this.currentLife2.Name = "currentLife2";
            this.currentLife2.Size = new System.Drawing.Size(100, 10);
            this.currentLife2.TabIndex = 1;
            // 
            // backSkillPoint2
            // 
            this.backSkillPoint2.BackColor = System.Drawing.Color.White;
            this.backSkillPoint2.Location = new System.Drawing.Point(27, 39);
            this.backSkillPoint2.Name = "backSkillPoint2";
            this.backSkillPoint2.Size = new System.Drawing.Size(100, 10);
            this.backSkillPoint2.TabIndex = 1;
            // 
            // SecondPlayerName
            // 
            this.SecondPlayerName.AutoSize = true;
            this.SecondPlayerName.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SecondPlayerName.Location = new System.Drawing.Point(8, 5);
            this.SecondPlayerName.Name = "SecondPlayerName";
            this.SecondPlayerName.Size = new System.Drawing.Size(76, 13);
            this.SecondPlayerName.TabIndex = 0;
            this.SecondPlayerName.Text = "ラナ・アミリア";
            // 
            // backLife2
            // 
            this.backLife2.BackColor = System.Drawing.Color.White;
            this.backLife2.Location = new System.Drawing.Point(27, 25);
            this.backLife2.Name = "backLife2";
            this.backLife2.Size = new System.Drawing.Size(100, 10);
            this.backLife2.TabIndex = 1;
            // 
            // ThirdPlayerPanel
            // 
            this.ThirdPlayerPanel.BackColor = System.Drawing.Color.Silver;
            this.ThirdPlayerPanel.Controls.Add(this.MP3);
            this.ThirdPlayerPanel.Controls.Add(this.SP3);
            this.ThirdPlayerPanel.Controls.Add(this.HP3);
            this.ThirdPlayerPanel.Controls.Add(this.currentManaPoint3);
            this.ThirdPlayerPanel.Controls.Add(this.currentSkillPoint3);
            this.ThirdPlayerPanel.Controls.Add(this.backManaPoint3);
            this.ThirdPlayerPanel.Controls.Add(this.currentLife3);
            this.ThirdPlayerPanel.Controls.Add(this.backSkillPoint3);
            this.ThirdPlayerPanel.Controls.Add(this.ThirdPlayerName);
            this.ThirdPlayerPanel.Controls.Add(this.backLife3);
            this.ThirdPlayerPanel.Location = new System.Drawing.Point(12, 243);
            this.ThirdPlayerPanel.Name = "ThirdPlayerPanel";
            this.ThirdPlayerPanel.Size = new System.Drawing.Size(134, 73);
            this.ThirdPlayerPanel.TabIndex = 6;
            // 
            // MP3
            // 
            this.MP3.AutoSize = true;
            this.MP3.Location = new System.Drawing.Point(4, 53);
            this.MP3.Name = "MP3";
            this.MP3.Size = new System.Drawing.Size(21, 12);
            this.MP3.TabIndex = 2;
            this.MP3.Text = "MP";
            // 
            // SP3
            // 
            this.SP3.AutoSize = true;
            this.SP3.Location = new System.Drawing.Point(4, 39);
            this.SP3.Name = "SP3";
            this.SP3.Size = new System.Drawing.Size(19, 12);
            this.SP3.TabIndex = 2;
            this.SP3.Text = "SP";
            // 
            // HP3
            // 
            this.HP3.AutoSize = true;
            this.HP3.Location = new System.Drawing.Point(4, 25);
            this.HP3.Name = "HP3";
            this.HP3.Size = new System.Drawing.Size(20, 12);
            this.HP3.TabIndex = 2;
            this.HP3.Text = "HP";
            // 
            // currentManaPoint3
            // 
            this.currentManaPoint3.BackColor = System.Drawing.Color.Crimson;
            this.currentManaPoint3.Location = new System.Drawing.Point(27, 53);
            this.currentManaPoint3.Name = "currentManaPoint3";
            this.currentManaPoint3.Size = new System.Drawing.Size(100, 10);
            this.currentManaPoint3.TabIndex = 1;
            // 
            // currentSkillPoint3
            // 
            this.currentSkillPoint3.BackColor = System.Drawing.Color.DarkGreen;
            this.currentSkillPoint3.Location = new System.Drawing.Point(27, 39);
            this.currentSkillPoint3.Name = "currentSkillPoint3";
            this.currentSkillPoint3.Size = new System.Drawing.Size(100, 10);
            this.currentSkillPoint3.TabIndex = 1;
            // 
            // backManaPoint3
            // 
            this.backManaPoint3.BackColor = System.Drawing.Color.White;
            this.backManaPoint3.Location = new System.Drawing.Point(27, 53);
            this.backManaPoint3.Name = "backManaPoint3";
            this.backManaPoint3.Size = new System.Drawing.Size(100, 10);
            this.backManaPoint3.TabIndex = 1;
            // 
            // currentLife3
            // 
            this.currentLife3.BackColor = System.Drawing.Color.Blue;
            this.currentLife3.Location = new System.Drawing.Point(27, 25);
            this.currentLife3.Name = "currentLife3";
            this.currentLife3.Size = new System.Drawing.Size(100, 10);
            this.currentLife3.TabIndex = 1;
            // 
            // backSkillPoint3
            // 
            this.backSkillPoint3.BackColor = System.Drawing.Color.White;
            this.backSkillPoint3.Location = new System.Drawing.Point(27, 39);
            this.backSkillPoint3.Name = "backSkillPoint3";
            this.backSkillPoint3.Size = new System.Drawing.Size(100, 10);
            this.backSkillPoint3.TabIndex = 1;
            // 
            // ThirdPlayerName
            // 
            this.ThirdPlayerName.AutoSize = true;
            this.ThirdPlayerName.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ThirdPlayerName.Location = new System.Drawing.Point(8, 5);
            this.ThirdPlayerName.Name = "ThirdPlayerName";
            this.ThirdPlayerName.Size = new System.Drawing.Size(101, 13);
            this.ThirdPlayerName.TabIndex = 0;
            this.ThirdPlayerName.Text = "ヴェルゼ・アーティ";
            // 
            // backLife3
            // 
            this.backLife3.BackColor = System.Drawing.Color.White;
            this.backLife3.Location = new System.Drawing.Point(27, 25);
            this.backLife3.Name = "backLife3";
            this.backLife3.Size = new System.Drawing.Size(100, 10);
            this.backLife3.TabIndex = 1;
            // 
            // dungeonAreaLabel
            // 
            this.dungeonAreaLabel.BackColor = System.Drawing.Color.GhostWhite;
            this.dungeonAreaLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dungeonAreaLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dungeonAreaLabel.Location = new System.Drawing.Point(95, 9);
            this.dungeonAreaLabel.Name = "dungeonAreaLabel";
            this.dungeonAreaLabel.Size = new System.Drawing.Size(51, 37);
            this.dungeonAreaLabel.TabIndex = 5;
            this.dungeonAreaLabel.Text = "１　階";
            this.dungeonAreaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelVigilance
            // 
            this.labelVigilance.BackColor = System.Drawing.Color.AliceBlue;
            this.labelVigilance.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelVigilance.Location = new System.Drawing.Point(14, 395);
            this.labelVigilance.Name = "labelVigilance";
            this.labelVigilance.Size = new System.Drawing.Size(132, 34);
            this.labelVigilance.TabIndex = 8;
            this.labelVigilance.Text = "警戒モード";
            this.labelVigilance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelVigilance.Click += new System.EventHandler(this.labelVigilance_Click);
            // 
            // dungeonField
            // 
            this.dungeonField.Controls.Add(this.Player);
            this.dungeonField.Location = new System.Drawing.Point(160, 0);
            this.dungeonField.Name = "dungeonField";
            this.dungeonField.Size = new System.Drawing.Size(480, 320);
            this.dungeonField.TabIndex = 9;
            this.dungeonField.TabStop = false;
            this.dungeonField.Paint += new System.Windows.Forms.PaintEventHandler(this.dungeonField_Paint);
            // 
            // DungeonPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.RoyalBlue;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.Controls.Add(this.dungeonField);
            this.Controls.Add(this.labelVigilance);
            this.Controls.Add(this.ThirdPlayerPanel);
            this.Controls.Add(this.SecondPlayerPanel);
            this.Controls.Add(this.FirstPlayerPanel);
            this.Controls.Add(this.dungeonAreaLabel);
            this.Controls.Add(this.dayLabel);
            this.Controls.Add(this.mainMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DungeonPlayer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DungeonPlayer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DungeonPlayer_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.Player)).EndInit();
            this.FirstPlayerPanel.ResumeLayout(false);
            this.FirstPlayerPanel.PerformLayout();
            this.SecondPlayerPanel.ResumeLayout(false);
            this.SecondPlayerPanel.PerformLayout();
            this.ThirdPlayerPanel.ResumeLayout(false);
            this.ThirdPlayerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dungeonField)).EndInit();
            this.dungeonField.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox Player;
        private System.Windows.Forms.Label mainMessage;
        private System.Windows.Forms.Label dayLabel;
        public System.Windows.Forms.Panel FirstPlayerPanel;
        private System.Windows.Forms.Label FirstPlayerName;
        private System.Windows.Forms.Label MP1;
        private System.Windows.Forms.Label SP1;
        private System.Windows.Forms.Label HP1;
        public System.Windows.Forms.Label currentManaPoint1;
        public System.Windows.Forms.Label currentSkillPoint1;
        private System.Windows.Forms.Label backManaPoint1;
        public System.Windows.Forms.Label currentLife1;
        private System.Windows.Forms.Label backSkillPoint1;
        private System.Windows.Forms.Label backLife1;
        public System.Windows.Forms.Panel SecondPlayerPanel;
        private System.Windows.Forms.Label MP2;
        private System.Windows.Forms.Label SP2;
        private System.Windows.Forms.Label HP2;
        public System.Windows.Forms.Label currentManaPoint2;
        public System.Windows.Forms.Label currentSkillPoint2;
        private System.Windows.Forms.Label backManaPoint2;
        public System.Windows.Forms.Label currentLife2;
        private System.Windows.Forms.Label backSkillPoint2;
        private System.Windows.Forms.Label SecondPlayerName;
        private System.Windows.Forms.Label backLife2;
        public System.Windows.Forms.Panel ThirdPlayerPanel;
        private System.Windows.Forms.Label MP3;
        private System.Windows.Forms.Label SP3;
        private System.Windows.Forms.Label HP3;
        public System.Windows.Forms.Label currentManaPoint3;
        public System.Windows.Forms.Label currentSkillPoint3;
        private System.Windows.Forms.Label backManaPoint3;
        public System.Windows.Forms.Label currentLife3;
        private System.Windows.Forms.Label backSkillPoint3;
        private System.Windows.Forms.Label ThirdPlayerName;
        private System.Windows.Forms.Label backLife3;
        private System.Windows.Forms.Label dungeonAreaLabel;
        private System.Windows.Forms.Label labelVigilance;
        private System.Windows.Forms.PictureBox dungeonField;
    }
}

