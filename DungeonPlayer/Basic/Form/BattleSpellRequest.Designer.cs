namespace DungeonPlayer
{
    partial class BattleSpellRequest
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BattleSpellRequest));
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.labelNoSpell = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(12, 443);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(616, 25);
            this.button2.TabIndex = 10;
            this.button2.Text = "戻る";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.Enter += new System.EventHandler(this.BattleSpellRequest_Enter);
            this.button2.Leave += new System.EventHandler(this.BattleSpellRequest_Leave);
            this.button2.MouseEnter += new System.EventHandler(this.BattleSpellRequest_MouseEnter);
            this.button2.MouseLeave += new System.EventHandler(this.BattleSpellRequest_Leave);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(30, 416);
            this.button1.TabIndex = 0;
            this.button1.Text = "<<";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.Enter += new System.EventHandler(this.BattleSpellRequest_Enter);
            this.button1.Leave += new System.EventHandler(this.BattleSpellRequest_Leave);
            this.button1.MouseEnter += new System.EventHandler(this.BattleSpellRequest_MouseEnter);
            this.button1.MouseLeave += new System.EventHandler(this.BattleSpellRequest_Leave);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.Location = new System.Drawing.Point(598, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(30, 416);
            this.button3.TabIndex = 9;
            this.button3.Text = ">>";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            this.button3.Enter += new System.EventHandler(this.BattleSpellRequest_Enter);
            this.button3.Leave += new System.EventHandler(this.BattleSpellRequest_Leave);
            this.button3.MouseEnter += new System.EventHandler(this.BattleSpellRequest_MouseEnter);
            this.button3.MouseLeave += new System.EventHandler(this.BattleSpellRequest_Leave);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.CornflowerBlue;
            this.label1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(48, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(544, 50);
            this.label1.TabIndex = 3;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelNoSpell
            // 
            this.labelNoSpell.BackColor = System.Drawing.Color.LightGray;
            this.labelNoSpell.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNoSpell.Location = new System.Drawing.Point(48, 62);
            this.labelNoSpell.Name = "labelNoSpell";
            this.labelNoSpell.Size = new System.Drawing.Size(544, 366);
            this.labelNoSpell.TabIndex = 3;
            this.labelNoSpell.Text = "魔法を習得していません。";
            this.labelNoSpell.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BattleSpellRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aquamarine;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.Controls.Add(this.labelNoSpell);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BattleSpellRequest";
            this.ShowInTaskbar = false;
            this.Text = "BattleSpellRequest";
            this.Load += new System.EventHandler(this.BattleSpellRequest_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelNoSpell;
    }
}