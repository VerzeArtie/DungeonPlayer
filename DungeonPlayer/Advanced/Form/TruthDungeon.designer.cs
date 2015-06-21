namespace DungeonPlayer
{
    partial class TruthDungeon
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TruthDungeon));
            this.Player = new System.Windows.Forms.PictureBox();
            this.movementTimer = new System.Windows.Forms.Timer(this.components);
            this.agilityRoomTimer = new System.Windows.Forms.Timer(this.components);
            this.VigilanceMode = new System.Windows.Forms.Label();
            this.PathfindingMode = new System.Windows.Forms.Label();
            this.menuFocus = new System.Windows.Forms.Label();
            this.EndArea = new System.Windows.Forms.Label();
            this.LoadArea = new System.Windows.Forms.Label();
            this.SaveArea = new System.Windows.Forms.Label();
            this.BattleSetttingArea = new System.Windows.Forms.Label();
            this.StatusArea = new System.Windows.Forms.Label();
            this.labelVigilance = new System.Windows.Forms.Label();
            this.ThirdPlayerPanel = new System.Windows.Forms.Panel();
            this.currentManaValue3 = new TransparentLabel();
            this.currentManaPoint3 = new System.Windows.Forms.Label();
            this.currentSkillValue3 = new TransparentLabel();
            this.currentSkillPoint3 = new System.Windows.Forms.Label();
            this.backManaPoint3 = new System.Windows.Forms.Label();
            this.currentLifeValue3 = new TransparentLabel();
            this.currentLife3 = new System.Windows.Forms.Label();
            this.backSkillPoint3 = new System.Windows.Forms.Label();
            this.ThirdPlayerName = new System.Windows.Forms.Label();
            this.backLife3 = new System.Windows.Forms.Label();
            this.SecondPlayerPanel = new System.Windows.Forms.Panel();
            this.currentManaValue2 = new TransparentLabel();
            this.currentSkillValue2 = new TransparentLabel();
            this.currentManaPoint2 = new System.Windows.Forms.Label();
            this.currentLifeValue2 = new TransparentLabel();
            this.currentSkillPoint2 = new System.Windows.Forms.Label();
            this.backManaPoint2 = new System.Windows.Forms.Label();
            this.currentLife2 = new System.Windows.Forms.Label();
            this.backSkillPoint2 = new System.Windows.Forms.Label();
            this.SecondPlayerName = new System.Windows.Forms.Label();
            this.backLife2 = new System.Windows.Forms.Label();
            this.FirstPlayerPanel = new System.Windows.Forms.Panel();
            this.currentManaValue1 = new TransparentLabel();
            this.currentSkillValue1 = new TransparentLabel();
            this.currentLifeValue1 = new TransparentLabel();
            this.currentManaPoint1 = new System.Windows.Forms.Label();
            this.currentSkillPoint1 = new System.Windows.Forms.Label();
            this.backManaPoint1 = new System.Windows.Forms.Label();
            this.currentLife1 = new System.Windows.Forms.Label();
            this.backSkillPoint1 = new System.Windows.Forms.Label();
            this.FirstPlayerName = new System.Windows.Forms.Label();
            this.backLife1 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.mainMessage = new System.Windows.Forms.Label();
            this.dungeonAreaLabel = new System.Windows.Forms.Label();
            this.dayLabel = new System.Windows.Forms.Label();
            this.dungeonField = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Player)).BeginInit();
            this.ThirdPlayerPanel.SuspendLayout();
            this.SecondPlayerPanel.SuspendLayout();
            this.FirstPlayerPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dungeonField)).BeginInit();
            this.SuspendLayout();
            // 
            // Player
            // 
            this.Player.Location = new System.Drawing.Point(0, 0);
            this.Player.Name = "Player";
            this.Player.Size = new System.Drawing.Size(28, 28);
            this.Player.TabIndex = 9;
            this.Player.TabStop = false;
            // 
            // movementTimer
            // 
            this.movementTimer.Interval = 1;
            this.movementTimer.Tick += new System.EventHandler(this.movementTimer_Tick);
            // 
            // agilityRoomTimer
            // 
            this.agilityRoomTimer.Tick += new System.EventHandler(this.agilityRoomTimer_Tick);
            // 
            // VigilanceMode
            // 
            this.VigilanceMode.Location = new System.Drawing.Point(762, 56);
            this.VigilanceMode.Name = "VigilanceMode";
            this.VigilanceMode.Size = new System.Drawing.Size(24, 24);
            this.VigilanceMode.TabIndex = 12;
            this.VigilanceMode.Click += new System.EventHandler(this.VigilanceMode_Click);
            // 
            // PathfindingMode
            // 
            this.PathfindingMode.Location = new System.Drawing.Point(794, 56);
            this.PathfindingMode.Name = "PathfindingMode";
            this.PathfindingMode.Size = new System.Drawing.Size(24, 24);
            this.PathfindingMode.TabIndex = 12;
            this.PathfindingMode.Click += new System.EventHandler(this.PathfindingMode_Click);
            // 
            // menuFocus
            // 
            this.menuFocus.BackColor = System.Drawing.Color.Firebrick;
            this.menuFocus.Location = new System.Drawing.Point(820, 415);
            this.menuFocus.Name = "menuFocus";
            this.menuFocus.Size = new System.Drawing.Size(160, 3);
            this.menuFocus.TabIndex = 11;
            // 
            // EndArea
            // 
            this.EndArea.BackColor = System.Drawing.Color.Transparent;
            this.EndArea.Location = new System.Drawing.Point(820, 616);
            this.EndArea.Name = "EndArea";
            this.EndArea.Size = new System.Drawing.Size(160, 60);
            this.EndArea.TabIndex = 10;
            this.EndArea.Click += new System.EventHandler(this.EndArea_Click);
            this.EndArea.Enter += new System.EventHandler(this.StatusArea_Enter);
            this.EndArea.Leave += new System.EventHandler(this.StatusArea_Leave);
            this.EndArea.MouseEnter += new System.EventHandler(this.StatusArea_Enter);
            this.EndArea.MouseLeave += new System.EventHandler(this.StatusArea_Leave);
            // 
            // LoadArea
            // 
            this.LoadArea.BackColor = System.Drawing.Color.Transparent;
            this.LoadArea.Location = new System.Drawing.Point(820, 548);
            this.LoadArea.Name = "LoadArea";
            this.LoadArea.Size = new System.Drawing.Size(160, 60);
            this.LoadArea.TabIndex = 10;
            this.LoadArea.Click += new System.EventHandler(this.LoadArea_Click);
            this.LoadArea.Enter += new System.EventHandler(this.StatusArea_Enter);
            this.LoadArea.Leave += new System.EventHandler(this.StatusArea_Leave);
            this.LoadArea.MouseEnter += new System.EventHandler(this.StatusArea_Enter);
            this.LoadArea.MouseLeave += new System.EventHandler(this.StatusArea_Leave);
            // 
            // SaveArea
            // 
            this.SaveArea.BackColor = System.Drawing.Color.Transparent;
            this.SaveArea.Location = new System.Drawing.Point(820, 483);
            this.SaveArea.Name = "SaveArea";
            this.SaveArea.Size = new System.Drawing.Size(160, 60);
            this.SaveArea.TabIndex = 10;
            this.SaveArea.Click += new System.EventHandler(this.SaveArea_Click);
            this.SaveArea.Enter += new System.EventHandler(this.StatusArea_Enter);
            this.SaveArea.Leave += new System.EventHandler(this.StatusArea_Leave);
            this.SaveArea.MouseEnter += new System.EventHandler(this.StatusArea_Enter);
            this.SaveArea.MouseLeave += new System.EventHandler(this.StatusArea_Leave);
            // 
            // BattleSetttingArea
            // 
            this.BattleSetttingArea.BackColor = System.Drawing.Color.Transparent;
            this.BattleSetttingArea.Location = new System.Drawing.Point(820, 421);
            this.BattleSetttingArea.Name = "BattleSetttingArea";
            this.BattleSetttingArea.Size = new System.Drawing.Size(160, 60);
            this.BattleSetttingArea.TabIndex = 10;
            this.BattleSetttingArea.Click += new System.EventHandler(this.BattleSetttingArea_Click);
            this.BattleSetttingArea.Enter += new System.EventHandler(this.StatusArea_Enter);
            this.BattleSetttingArea.Leave += new System.EventHandler(this.StatusArea_Leave);
            this.BattleSetttingArea.MouseEnter += new System.EventHandler(this.StatusArea_Enter);
            this.BattleSetttingArea.MouseLeave += new System.EventHandler(this.StatusArea_Leave);
            // 
            // StatusArea
            // 
            this.StatusArea.BackColor = System.Drawing.Color.Transparent;
            this.StatusArea.Location = new System.Drawing.Point(820, 352);
            this.StatusArea.Name = "StatusArea";
            this.StatusArea.Size = new System.Drawing.Size(160, 60);
            this.StatusArea.TabIndex = 10;
            this.StatusArea.Click += new System.EventHandler(this.StatusArea_Click);
            this.StatusArea.Enter += new System.EventHandler(this.StatusArea_Enter);
            this.StatusArea.Leave += new System.EventHandler(this.StatusArea_Leave);
            this.StatusArea.MouseEnter += new System.EventHandler(this.StatusArea_Enter);
            this.StatusArea.MouseLeave += new System.EventHandler(this.StatusArea_Leave);
            // 
            // labelVigilance
            // 
            this.labelVigilance.BackColor = System.Drawing.Color.Transparent;
            this.labelVigilance.Font = new System.Drawing.Font("HGP行書体", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelVigilance.Location = new System.Drawing.Point(620, 50);
            this.labelVigilance.Name = "labelVigilance";
            this.labelVigilance.Size = new System.Drawing.Size(134, 35);
            this.labelVigilance.TabIndex = 8;
            this.labelVigilance.Text = "警戒モード";
            this.labelVigilance.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelVigilance.Click += new System.EventHandler(this.labelVigilance_Click);
            // 
            // ThirdPlayerPanel
            // 
            this.ThirdPlayerPanel.BackColor = System.Drawing.Color.Transparent;
            this.ThirdPlayerPanel.Controls.Add(this.currentManaValue3);
            this.ThirdPlayerPanel.Controls.Add(this.currentManaPoint3);
            this.ThirdPlayerPanel.Controls.Add(this.currentSkillValue3);
            this.ThirdPlayerPanel.Controls.Add(this.currentSkillPoint3);
            this.ThirdPlayerPanel.Controls.Add(this.backManaPoint3);
            this.ThirdPlayerPanel.Controls.Add(this.currentLifeValue3);
            this.ThirdPlayerPanel.Controls.Add(this.currentLife3);
            this.ThirdPlayerPanel.Controls.Add(this.backSkillPoint3);
            this.ThirdPlayerPanel.Controls.Add(this.ThirdPlayerName);
            this.ThirdPlayerPanel.Controls.Add(this.backLife3);
            this.ThirdPlayerPanel.Location = new System.Drawing.Point(825, 260);
            this.ThirdPlayerPanel.Name = "ThirdPlayerPanel";
            this.ThirdPlayerPanel.Size = new System.Drawing.Size(157, 85);
            this.ThirdPlayerPanel.TabIndex = 6;
            // 
            // currentManaValue3
            // 
            this.currentManaValue3.AllowTransparency = true;
            this.currentManaValue3.Font = new System.Drawing.Font("HGP行書体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentManaValue3.ForeColor = System.Drawing.Color.White;
            this.currentManaValue3.Location = new System.Drawing.Point(28, 53);
            this.currentManaValue3.Name = "currentManaValue3";
            this.currentManaValue3.Size = new System.Drawing.Size(107, 24);
            this.currentManaValue3.TabIndex = 2;
            this.currentManaValue3.Text = "1000000";
            this.currentManaValue3.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // currentManaPoint3
            // 
            this.currentManaPoint3.BackColor = System.Drawing.Color.Blue;
            this.currentManaPoint3.Location = new System.Drawing.Point(5, 63);
            this.currentManaPoint3.Name = "currentManaPoint3";
            this.currentManaPoint3.Size = new System.Drawing.Size(130, 10);
            this.currentManaPoint3.TabIndex = 1;
            // 
            // currentSkillValue3
            // 
            this.currentSkillValue3.AllowTransparency = true;
            this.currentSkillValue3.Font = new System.Drawing.Font("HGP行書体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentSkillValue3.ForeColor = System.Drawing.Color.White;
            this.currentSkillValue3.Location = new System.Drawing.Point(28, 36);
            this.currentSkillValue3.Name = "currentSkillValue3";
            this.currentSkillValue3.Size = new System.Drawing.Size(107, 24);
            this.currentSkillValue3.TabIndex = 2;
            this.currentSkillValue3.Text = "1000000";
            this.currentSkillValue3.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // currentSkillPoint3
            // 
            this.currentSkillPoint3.BackColor = System.Drawing.Color.DarkGreen;
            this.currentSkillPoint3.Location = new System.Drawing.Point(5, 46);
            this.currentSkillPoint3.Name = "currentSkillPoint3";
            this.currentSkillPoint3.Size = new System.Drawing.Size(130, 10);
            this.currentSkillPoint3.TabIndex = 1;
            // 
            // backManaPoint3
            // 
            this.backManaPoint3.BackColor = System.Drawing.Color.Black;
            this.backManaPoint3.Location = new System.Drawing.Point(5, 63);
            this.backManaPoint3.Name = "backManaPoint3";
            this.backManaPoint3.Size = new System.Drawing.Size(130, 10);
            this.backManaPoint3.TabIndex = 1;
            // 
            // currentLifeValue3
            // 
            this.currentLifeValue3.AllowTransparency = true;
            this.currentLifeValue3.Font = new System.Drawing.Font("HGP行書体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentLifeValue3.ForeColor = System.Drawing.Color.White;
            this.currentLifeValue3.Location = new System.Drawing.Point(28, 19);
            this.currentLifeValue3.Name = "currentLifeValue3";
            this.currentLifeValue3.Size = new System.Drawing.Size(107, 24);
            this.currentLifeValue3.TabIndex = 2;
            this.currentLifeValue3.Text = "1000000";
            this.currentLifeValue3.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // currentLife3
            // 
            this.currentLife3.BackColor = System.Drawing.Color.Crimson;
            this.currentLife3.Location = new System.Drawing.Point(5, 29);
            this.currentLife3.Name = "currentLife3";
            this.currentLife3.Size = new System.Drawing.Size(130, 10);
            this.currentLife3.TabIndex = 1;
            // 
            // backSkillPoint3
            // 
            this.backSkillPoint3.BackColor = System.Drawing.Color.Black;
            this.backSkillPoint3.Location = new System.Drawing.Point(5, 46);
            this.backSkillPoint3.Name = "backSkillPoint3";
            this.backSkillPoint3.Size = new System.Drawing.Size(130, 10);
            this.backSkillPoint3.TabIndex = 1;
            // 
            // ThirdPlayerName
            // 
            this.ThirdPlayerName.AutoSize = true;
            this.ThirdPlayerName.Font = new System.Drawing.Font("HGP行書体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.ThirdPlayerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.ThirdPlayerName.Location = new System.Drawing.Point(8, 5);
            this.ThirdPlayerName.Name = "ThirdPlayerName";
            this.ThirdPlayerName.Size = new System.Drawing.Size(112, 16);
            this.ThirdPlayerName.TabIndex = 0;
            this.ThirdPlayerName.Text = "オル・ランディス";
            // 
            // backLife3
            // 
            this.backLife3.BackColor = System.Drawing.Color.Black;
            this.backLife3.Location = new System.Drawing.Point(5, 29);
            this.backLife3.Name = "backLife3";
            this.backLife3.Size = new System.Drawing.Size(130, 10);
            this.backLife3.TabIndex = 1;
            // 
            // SecondPlayerPanel
            // 
            this.SecondPlayerPanel.BackColor = System.Drawing.Color.Transparent;
            this.SecondPlayerPanel.Controls.Add(this.currentManaValue2);
            this.SecondPlayerPanel.Controls.Add(this.currentSkillValue2);
            this.SecondPlayerPanel.Controls.Add(this.currentManaPoint2);
            this.SecondPlayerPanel.Controls.Add(this.currentLifeValue2);
            this.SecondPlayerPanel.Controls.Add(this.currentSkillPoint2);
            this.SecondPlayerPanel.Controls.Add(this.backManaPoint2);
            this.SecondPlayerPanel.Controls.Add(this.currentLife2);
            this.SecondPlayerPanel.Controls.Add(this.backSkillPoint2);
            this.SecondPlayerPanel.Controls.Add(this.SecondPlayerName);
            this.SecondPlayerPanel.Controls.Add(this.backLife2);
            this.SecondPlayerPanel.Location = new System.Drawing.Point(825, 165);
            this.SecondPlayerPanel.Name = "SecondPlayerPanel";
            this.SecondPlayerPanel.Size = new System.Drawing.Size(157, 85);
            this.SecondPlayerPanel.TabIndex = 6;
            // 
            // currentManaValue2
            // 
            this.currentManaValue2.AllowTransparency = true;
            this.currentManaValue2.Font = new System.Drawing.Font("HGP行書体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentManaValue2.ForeColor = System.Drawing.Color.White;
            this.currentManaValue2.Location = new System.Drawing.Point(28, 53);
            this.currentManaValue2.Name = "currentManaValue2";
            this.currentManaValue2.Size = new System.Drawing.Size(107, 24);
            this.currentManaValue2.TabIndex = 2;
            this.currentManaValue2.Text = "1000000";
            this.currentManaValue2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // currentSkillValue2
            // 
            this.currentSkillValue2.AllowTransparency = true;
            this.currentSkillValue2.Font = new System.Drawing.Font("HGP行書体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentSkillValue2.ForeColor = System.Drawing.Color.White;
            this.currentSkillValue2.Location = new System.Drawing.Point(28, 36);
            this.currentSkillValue2.Name = "currentSkillValue2";
            this.currentSkillValue2.Size = new System.Drawing.Size(107, 24);
            this.currentSkillValue2.TabIndex = 2;
            this.currentSkillValue2.Text = "1000000";
            this.currentSkillValue2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // currentManaPoint2
            // 
            this.currentManaPoint2.BackColor = System.Drawing.Color.Blue;
            this.currentManaPoint2.Location = new System.Drawing.Point(5, 63);
            this.currentManaPoint2.Name = "currentManaPoint2";
            this.currentManaPoint2.Size = new System.Drawing.Size(130, 10);
            this.currentManaPoint2.TabIndex = 1;
            // 
            // currentLifeValue2
            // 
            this.currentLifeValue2.AllowTransparency = true;
            this.currentLifeValue2.Font = new System.Drawing.Font("HGP行書体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentLifeValue2.ForeColor = System.Drawing.Color.White;
            this.currentLifeValue2.Location = new System.Drawing.Point(28, 19);
            this.currentLifeValue2.Name = "currentLifeValue2";
            this.currentLifeValue2.Size = new System.Drawing.Size(107, 24);
            this.currentLifeValue2.TabIndex = 2;
            this.currentLifeValue2.Text = "1000000";
            this.currentLifeValue2.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // currentSkillPoint2
            // 
            this.currentSkillPoint2.BackColor = System.Drawing.Color.DarkGreen;
            this.currentSkillPoint2.Location = new System.Drawing.Point(5, 46);
            this.currentSkillPoint2.Name = "currentSkillPoint2";
            this.currentSkillPoint2.Size = new System.Drawing.Size(130, 10);
            this.currentSkillPoint2.TabIndex = 1;
            // 
            // backManaPoint2
            // 
            this.backManaPoint2.BackColor = System.Drawing.Color.Black;
            this.backManaPoint2.Location = new System.Drawing.Point(5, 63);
            this.backManaPoint2.Name = "backManaPoint2";
            this.backManaPoint2.Size = new System.Drawing.Size(130, 10);
            this.backManaPoint2.TabIndex = 1;
            // 
            // currentLife2
            // 
            this.currentLife2.BackColor = System.Drawing.Color.Crimson;
            this.currentLife2.Location = new System.Drawing.Point(5, 29);
            this.currentLife2.Name = "currentLife2";
            this.currentLife2.Size = new System.Drawing.Size(130, 10);
            this.currentLife2.TabIndex = 1;
            // 
            // backSkillPoint2
            // 
            this.backSkillPoint2.BackColor = System.Drawing.Color.Black;
            this.backSkillPoint2.Location = new System.Drawing.Point(5, 46);
            this.backSkillPoint2.Name = "backSkillPoint2";
            this.backSkillPoint2.Size = new System.Drawing.Size(130, 10);
            this.backSkillPoint2.TabIndex = 1;
            // 
            // SecondPlayerName
            // 
            this.SecondPlayerName.AutoSize = true;
            this.SecondPlayerName.Font = new System.Drawing.Font("HGP行書体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.SecondPlayerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.SecondPlayerName.Location = new System.Drawing.Point(8, 5);
            this.SecondPlayerName.Name = "SecondPlayerName";
            this.SecondPlayerName.Size = new System.Drawing.Size(94, 16);
            this.SecondPlayerName.TabIndex = 0;
            this.SecondPlayerName.Text = "ラナ・アミリア";
            // 
            // backLife2
            // 
            this.backLife2.BackColor = System.Drawing.Color.Black;
            this.backLife2.Location = new System.Drawing.Point(5, 29);
            this.backLife2.Name = "backLife2";
            this.backLife2.Size = new System.Drawing.Size(130, 10);
            this.backLife2.TabIndex = 1;
            // 
            // FirstPlayerPanel
            // 
            this.FirstPlayerPanel.BackColor = System.Drawing.Color.Transparent;
            this.FirstPlayerPanel.Controls.Add(this.currentManaValue1);
            this.FirstPlayerPanel.Controls.Add(this.currentSkillValue1);
            this.FirstPlayerPanel.Controls.Add(this.currentLifeValue1);
            this.FirstPlayerPanel.Controls.Add(this.currentManaPoint1);
            this.FirstPlayerPanel.Controls.Add(this.currentSkillPoint1);
            this.FirstPlayerPanel.Controls.Add(this.backManaPoint1);
            this.FirstPlayerPanel.Controls.Add(this.currentLife1);
            this.FirstPlayerPanel.Controls.Add(this.backSkillPoint1);
            this.FirstPlayerPanel.Controls.Add(this.FirstPlayerName);
            this.FirstPlayerPanel.Controls.Add(this.backLife1);
            this.FirstPlayerPanel.Location = new System.Drawing.Point(825, 70);
            this.FirstPlayerPanel.Name = "FirstPlayerPanel";
            this.FirstPlayerPanel.Size = new System.Drawing.Size(157, 85);
            this.FirstPlayerPanel.TabIndex = 6;
            // 
            // currentManaValue1
            // 
            this.currentManaValue1.AllowTransparency = true;
            this.currentManaValue1.Font = new System.Drawing.Font("HGP行書体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentManaValue1.ForeColor = System.Drawing.Color.White;
            this.currentManaValue1.Location = new System.Drawing.Point(28, 53);
            this.currentManaValue1.Name = "currentManaValue1";
            this.currentManaValue1.Size = new System.Drawing.Size(107, 24);
            this.currentManaValue1.TabIndex = 2;
            this.currentManaValue1.Text = "1000000";
            this.currentManaValue1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // currentSkillValue1
            // 
            this.currentSkillValue1.AllowTransparency = true;
            this.currentSkillValue1.Font = new System.Drawing.Font("HGP行書体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentSkillValue1.ForeColor = System.Drawing.Color.White;
            this.currentSkillValue1.Location = new System.Drawing.Point(28, 36);
            this.currentSkillValue1.Name = "currentSkillValue1";
            this.currentSkillValue1.Size = new System.Drawing.Size(107, 24);
            this.currentSkillValue1.TabIndex = 2;
            this.currentSkillValue1.Text = "1000000";
            this.currentSkillValue1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // currentLifeValue1
            // 
            this.currentLifeValue1.AllowTransparency = true;
            this.currentLifeValue1.Font = new System.Drawing.Font("HGP行書体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentLifeValue1.ForeColor = System.Drawing.Color.White;
            this.currentLifeValue1.Location = new System.Drawing.Point(28, 19);
            this.currentLifeValue1.Name = "currentLifeValue1";
            this.currentLifeValue1.Size = new System.Drawing.Size(107, 24);
            this.currentLifeValue1.TabIndex = 2;
            this.currentLifeValue1.Text = "1000000";
            this.currentLifeValue1.TextAlign = System.Drawing.ContentAlignment.BottomRight;
            // 
            // currentManaPoint1
            // 
            this.currentManaPoint1.BackColor = System.Drawing.Color.Blue;
            this.currentManaPoint1.Location = new System.Drawing.Point(5, 63);
            this.currentManaPoint1.Name = "currentManaPoint1";
            this.currentManaPoint1.Size = new System.Drawing.Size(130, 10);
            this.currentManaPoint1.TabIndex = 1;
            // 
            // currentSkillPoint1
            // 
            this.currentSkillPoint1.BackColor = System.Drawing.Color.DarkGreen;
            this.currentSkillPoint1.Location = new System.Drawing.Point(5, 46);
            this.currentSkillPoint1.Name = "currentSkillPoint1";
            this.currentSkillPoint1.Size = new System.Drawing.Size(130, 10);
            this.currentSkillPoint1.TabIndex = 1;
            // 
            // backManaPoint1
            // 
            this.backManaPoint1.BackColor = System.Drawing.Color.Black;
            this.backManaPoint1.Location = new System.Drawing.Point(5, 63);
            this.backManaPoint1.Name = "backManaPoint1";
            this.backManaPoint1.Size = new System.Drawing.Size(130, 10);
            this.backManaPoint1.TabIndex = 1;
            // 
            // currentLife1
            // 
            this.currentLife1.BackColor = System.Drawing.Color.Crimson;
            this.currentLife1.Location = new System.Drawing.Point(5, 29);
            this.currentLife1.Name = "currentLife1";
            this.currentLife1.Size = new System.Drawing.Size(130, 10);
            this.currentLife1.TabIndex = 1;
            // 
            // backSkillPoint1
            // 
            this.backSkillPoint1.BackColor = System.Drawing.Color.Black;
            this.backSkillPoint1.Location = new System.Drawing.Point(5, 46);
            this.backSkillPoint1.Name = "backSkillPoint1";
            this.backSkillPoint1.Size = new System.Drawing.Size(130, 10);
            this.backSkillPoint1.TabIndex = 1;
            // 
            // FirstPlayerName
            // 
            this.FirstPlayerName.AutoSize = true;
            this.FirstPlayerName.Font = new System.Drawing.Font("HGP行書体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.FirstPlayerName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.FirstPlayerName.Location = new System.Drawing.Point(8, 5);
            this.FirstPlayerName.Name = "FirstPlayerName";
            this.FirstPlayerName.Size = new System.Drawing.Size(137, 16);
            this.FirstPlayerName.TabIndex = 0;
            this.FirstPlayerName.Text = "アイン・ウォーレンス";
            // 
            // backLife1
            // 
            this.backLife1.BackColor = System.Drawing.Color.Black;
            this.backLife1.Location = new System.Drawing.Point(5, 29);
            this.backLife1.Name = "backLife1";
            this.backLife1.Size = new System.Drawing.Size(130, 10);
            this.backLife1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(10, 328);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 12);
            this.label1.TabIndex = 9;
            // 
            // mainMessage
            // 
            this.mainMessage.BackColor = System.Drawing.Color.Transparent;
            this.mainMessage.Font = new System.Drawing.Font("ＭＳ Ｐゴシック", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.mainMessage.Location = new System.Drawing.Point(140, 589);
            this.mainMessage.Name = "mainMessage";
            this.mainMessage.Size = new System.Drawing.Size(679, 94);
            this.mainMessage.TabIndex = 8;
            this.mainMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dungeonAreaLabel
            // 
            this.dungeonAreaLabel.BackColor = System.Drawing.Color.Transparent;
            this.dungeonAreaLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dungeonAreaLabel.Font = new System.Drawing.Font("HGP行書体", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dungeonAreaLabel.ForeColor = System.Drawing.Color.White;
            this.dungeonAreaLabel.Location = new System.Drawing.Point(199, 50);
            this.dungeonAreaLabel.Name = "dungeonAreaLabel";
            this.dungeonAreaLabel.Size = new System.Drawing.Size(89, 35);
            this.dungeonAreaLabel.TabIndex = 7;
            this.dungeonAreaLabel.Text = "１　階";
            this.dungeonAreaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dayLabel
            // 
            this.dayLabel.BackColor = System.Drawing.Color.Transparent;
            this.dayLabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dayLabel.Font = new System.Drawing.Font("HGP行書体", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.dayLabel.ForeColor = System.Drawing.Color.White;
            this.dayLabel.Location = new System.Drawing.Point(69, 50);
            this.dayLabel.Name = "dayLabel";
            this.dayLabel.Size = new System.Drawing.Size(124, 35);
            this.dayLabel.TabIndex = 6;
            this.dayLabel.Text = "１日目";
            this.dayLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dungeonField
            // 
            this.dungeonField.BackColor = System.Drawing.Color.Transparent;
            this.dungeonField.Location = new System.Drawing.Point(69, 86);
            this.dungeonField.Name = "dungeonField";
            this.dungeonField.Size = new System.Drawing.Size(750, 500);
            this.dungeonField.TabIndex = 0;
            this.dungeonField.TabStop = false;
            this.dungeonField.Paint += new System.Windows.Forms.PaintEventHandler(this.dungeonField_Paint);
            // 
            // TruthDungeon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.VigilanceMode);
            this.Controls.Add(this.PathfindingMode);
            this.Controls.Add(this.menuFocus);
            this.Controls.Add(this.EndArea);
            this.Controls.Add(this.LoadArea);
            this.Controls.Add(this.SaveArea);
            this.Controls.Add(this.BattleSetttingArea);
            this.Controls.Add(this.StatusArea);
            this.Controls.Add(this.labelVigilance);
            this.Controls.Add(this.ThirdPlayerPanel);
            this.Controls.Add(this.SecondPlayerPanel);
            this.Controls.Add(this.FirstPlayerPanel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mainMessage);
            this.Controls.Add(this.dungeonAreaLabel);
            this.Controls.Add(this.dayLabel);
            this.Controls.Add(this.dungeonField);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "TruthDungeon";
            this.Text = "TruthDungeon";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TruthDungeon_FormClosing);
            this.Load += new System.EventHandler(this.TruthDungeon_Load);
            this.Shown += new System.EventHandler(this.TruthDungeon_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TruthDungeon_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TruthDungeon_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.Player)).EndInit();
            this.ThirdPlayerPanel.ResumeLayout(false);
            this.ThirdPlayerPanel.PerformLayout();
            this.SecondPlayerPanel.ResumeLayout(false);
            this.SecondPlayerPanel.PerformLayout();
            this.FirstPlayerPanel.ResumeLayout(false);
            this.FirstPlayerPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dungeonField)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.PictureBox dungeonField;
        private System.Windows.Forms.Label dungeonAreaLabel;
        private System.Windows.Forms.Label dayLabel;
        private System.Windows.Forms.Label mainMessage;
        private System.Windows.Forms.PictureBox Player;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelVigilance;
        public System.Windows.Forms.Panel FirstPlayerPanel;
        private System.Windows.Forms.Label FirstPlayerName;
        public System.Windows.Forms.Label currentManaPoint1;
        public System.Windows.Forms.Label currentSkillPoint1;
        private System.Windows.Forms.Label backManaPoint1;
        public System.Windows.Forms.Label currentLife1;
        private System.Windows.Forms.Label backSkillPoint1;
        private System.Windows.Forms.Label backLife1;
        public System.Windows.Forms.Panel SecondPlayerPanel;
        public System.Windows.Forms.Label currentManaPoint2;
        public System.Windows.Forms.Label currentSkillPoint2;
        private System.Windows.Forms.Label backManaPoint2;
        public System.Windows.Forms.Label currentLife2;
        private System.Windows.Forms.Label backSkillPoint2;
        private System.Windows.Forms.Label SecondPlayerName;
        private System.Windows.Forms.Label backLife2;
        public System.Windows.Forms.Panel ThirdPlayerPanel;
        public System.Windows.Forms.Label currentManaPoint3;
        public System.Windows.Forms.Label currentSkillPoint3;
        private System.Windows.Forms.Label backManaPoint3;
        public System.Windows.Forms.Label currentLife3;
        private System.Windows.Forms.Label backSkillPoint3;
        private System.Windows.Forms.Label ThirdPlayerName;
        private System.Windows.Forms.Label backLife3;
        private System.Windows.Forms.Timer movementTimer;
        private System.Windows.Forms.Timer agilityRoomTimer;
        private System.Windows.Forms.Label StatusArea;
        private System.Windows.Forms.Label BattleSetttingArea;
        private System.Windows.Forms.Label SaveArea;
        private System.Windows.Forms.Label LoadArea;
        private System.Windows.Forms.Label EndArea;
        private System.Windows.Forms.Label menuFocus;
        private System.Windows.Forms.Label PathfindingMode;
        private System.Windows.Forms.Label VigilanceMode;
        private TransparentLabel currentLifeValue1;
        private TransparentLabel currentSkillValue1;
        private TransparentLabel currentManaValue1;
        private TransparentLabel currentManaValue3;
        private TransparentLabel currentSkillValue3;
        private TransparentLabel currentLifeValue3;
        private TransparentLabel currentManaValue2;
        private TransparentLabel currentSkillValue2;
        private TransparentLabel currentLifeValue2;
    }
}