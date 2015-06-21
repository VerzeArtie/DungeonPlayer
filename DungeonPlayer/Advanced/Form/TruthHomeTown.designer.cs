namespace DungeonPlayer
{
    partial class TruthHomeTown
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TruthHomeTown));
            this.dayLabel = new System.Windows.Forms.Label();
            this.mainMessage = new System.Windows.Forms.Label();
            this.buttonDungeon = new System.Windows.Forms.Button();
            this.buttonGanz = new System.Windows.Forms.Button();
            this.buttonRana = new System.Windows.Forms.Button();
            this.buttonHanna = new System.Windows.Forms.Button();
            this.buttonPotion = new System.Windows.Forms.Button();
            this.buttonDuel = new System.Windows.Forms.Button();
            this.buttonShinikia = new System.Windows.Forms.Button();
            this.labelEnding = new System.Windows.Forms.Label();
            this.labelEnding2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dayLabel
            // 
            this.dayLabel.BackColor = System.Drawing.Color.GhostWhite;
            this.dayLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dayLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dayLabel.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dayLabel.Location = new System.Drawing.Point(12, 9);
            this.dayLabel.Name = "dayLabel";
            this.dayLabel.Size = new System.Drawing.Size(109, 53);
            this.dayLabel.TabIndex = 3;
            this.dayLabel.Text = "１日目";
            this.dayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mainMessage
            // 
            this.mainMessage.BackColor = System.Drawing.Color.GhostWhite;
            this.mainMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mainMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.mainMessage.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.mainMessage.Location = new System.Drawing.Point(0, 708);
            this.mainMessage.Name = "mainMessage";
            this.mainMessage.Size = new System.Drawing.Size(1024, 60);
            this.mainMessage.TabIndex = 2;
            this.mainMessage.Text = "アイン：さて、何すっかな";
            this.mainMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonDungeon
            // 
            this.buttonDungeon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDungeon.Location = new System.Drawing.Point(132, 436);
            this.buttonDungeon.Name = "buttonDungeon";
            this.buttonDungeon.Size = new System.Drawing.Size(126, 40);
            this.buttonDungeon.TabIndex = 6;
            this.buttonDungeon.Text = "DungeonPlayer!";
            this.buttonDungeon.UseVisualStyleBackColor = true;
            this.buttonDungeon.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonGanz
            // 
            this.buttonGanz.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGanz.Location = new System.Drawing.Point(676, 485);
            this.buttonGanz.Name = "buttonGanz";
            this.buttonGanz.Size = new System.Drawing.Size(148, 40);
            this.buttonGanz.TabIndex = 7;
            this.buttonGanz.Text = "天下一品 ガンツの武具店";
            this.buttonGanz.UseVisualStyleBackColor = true;
            this.buttonGanz.Click += new System.EventHandler(this.button4_Click);
            // 
            // buttonRana
            // 
            this.buttonRana.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonRana.Location = new System.Drawing.Point(373, 498);
            this.buttonRana.Name = "buttonRana";
            this.buttonRana.Size = new System.Drawing.Size(124, 40);
            this.buttonRana.TabIndex = 4;
            this.buttonRana.Text = "幼なじみのラナと会話";
            this.buttonRana.UseVisualStyleBackColor = true;
            this.buttonRana.Click += new System.EventHandler(this.button3_Click);
            // 
            // buttonHanna
            // 
            this.buttonHanna.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonHanna.Location = new System.Drawing.Point(212, 566);
            this.buttonHanna.Name = "buttonHanna";
            this.buttonHanna.Size = new System.Drawing.Size(136, 40);
            this.buttonHanna.TabIndex = 5;
            this.buttonHanna.Text = "ハンナのゆったり宿屋";
            this.buttonHanna.UseVisualStyleBackColor = true;
            this.buttonHanna.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonPotion
            // 
            this.buttonPotion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonPotion.Location = new System.Drawing.Point(731, 436);
            this.buttonPotion.Name = "buttonPotion";
            this.buttonPotion.Size = new System.Drawing.Size(143, 40);
            this.buttonPotion.TabIndex = 4;
            this.buttonPotion.Text = "ラナのランラン薬品店♪";
            this.buttonPotion.UseVisualStyleBackColor = true;
            this.buttonPotion.Visible = false;
            this.buttonPotion.Click += new System.EventHandler(this.button6_Click);
            // 
            // buttonDuel
            // 
            this.buttonDuel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDuel.Location = new System.Drawing.Point(444, 447);
            this.buttonDuel.Name = "buttonDuel";
            this.buttonDuel.Size = new System.Drawing.Size(108, 40);
            this.buttonDuel.TabIndex = 4;
            this.buttonDuel.Text = "DUEL闘技場";
            this.buttonDuel.UseVisualStyleBackColor = true;
            this.buttonDuel.Visible = false;
            this.buttonDuel.Click += new System.EventHandler(this.button7_Click);
            // 
            // buttonShinikia
            // 
            this.buttonShinikia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonShinikia.Location = new System.Drawing.Point(80, 485);
            this.buttonShinikia.Name = "buttonShinikia";
            this.buttonShinikia.Size = new System.Drawing.Size(143, 40);
            this.buttonShinikia.TabIndex = 4;
            this.buttonShinikia.Text = "ゲート裏　転送装置";
            this.buttonShinikia.UseVisualStyleBackColor = true;
            this.buttonShinikia.Visible = false;
            this.buttonShinikia.Click += new System.EventHandler(this.button8_Click);
            // 
            // labelEnding
            // 
            this.labelEnding.BackColor = System.Drawing.Color.White;
            this.labelEnding.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelEnding.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelEnding.Location = new System.Drawing.Point(0, 768);
            this.labelEnding.Name = "labelEnding";
            this.labelEnding.Size = new System.Drawing.Size(512, 4000);
            this.labelEnding.TabIndex = 9;
            this.labelEnding.Visible = false;
            // 
            // labelEnding2
            // 
            this.labelEnding2.BackColor = System.Drawing.Color.White;
            this.labelEnding2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.labelEnding2.Font = new System.Drawing.Font("MS UI Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelEnding2.Location = new System.Drawing.Point(512, 768);
            this.labelEnding2.Name = "labelEnding2";
            this.labelEnding2.Size = new System.Drawing.Size(512, 4000);
            this.labelEnding2.TabIndex = 10;
            this.labelEnding2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.labelEnding2.Visible = false;
            // 
            // TruthHomeTown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.labelEnding2);
            this.Controls.Add(this.labelEnding);
            this.Controls.Add(this.buttonDungeon);
            this.Controls.Add(this.buttonGanz);
            this.Controls.Add(this.buttonDuel);
            this.Controls.Add(this.buttonShinikia);
            this.Controls.Add(this.buttonPotion);
            this.Controls.Add(this.buttonRana);
            this.Controls.Add(this.buttonHanna);
            this.Controls.Add(this.dayLabel);
            this.Controls.Add(this.mainMessage);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TruthHomeTown";
            this.Text = "TruthHomeTown";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TruthHomeTown_FormClosing);
            this.Load += new System.EventHandler(this.TruthHomeTown_Load);
            this.Shown += new System.EventHandler(this.TruthHomeTown_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TruthHomeTown_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label dayLabel;
        private System.Windows.Forms.Label mainMessage;
        private System.Windows.Forms.Button buttonDungeon;
        private System.Windows.Forms.Button buttonGanz;
        private System.Windows.Forms.Button buttonRana;
        private System.Windows.Forms.Button buttonHanna;
        private System.Windows.Forms.Button buttonPotion;
        private System.Windows.Forms.Button buttonDuel;
        private System.Windows.Forms.Button buttonShinikia;
        private System.Windows.Forms.Label labelEnding;
        private System.Windows.Forms.Label labelEnding2;
    }
}