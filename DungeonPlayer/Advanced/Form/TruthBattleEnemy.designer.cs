using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class TruthBattleEnemy
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
            this.timerBattleStart = new System.Windows.Forms.Timer(this.components);
            this.MatrixDragonTalk = new System.Windows.Forms.Label();
            this.pbMatrixDragon = new System.Windows.Forms.PictureBox();
            this.keyNum3_9 = new System.Windows.Forms.Label();
            this.keyNum3_8 = new System.Windows.Forms.Label();
            this.keyNum3_7 = new System.Windows.Forms.Label();
            this.keyNum2_9 = new System.Windows.Forms.Label();
            this.keyNum2_8 = new System.Windows.Forms.Label();
            this.keyNum2_7 = new System.Windows.Forms.Label();
            this.keyNum3_6 = new System.Windows.Forms.Label();
            this.keyNum2_6 = new System.Windows.Forms.Label();
            this.keyNum1_9 = new System.Windows.Forms.Label();
            this.keyNum1_8 = new System.Windows.Forms.Label();
            this.keyNum1_7 = new System.Windows.Forms.Label();
            this.keyNum3_5 = new System.Windows.Forms.Label();
            this.keyNum2_5 = new System.Windows.Forms.Label();
            this.keyNum1_6 = new System.Windows.Forms.Label();
            this.keyNum3_4 = new System.Windows.Forms.Label();
            this.keyNum2_4 = new System.Windows.Forms.Label();
            this.keyNum1_5 = new System.Windows.Forms.Label();
            this.keyNum3_3 = new System.Windows.Forms.Label();
            this.keyNum2_3 = new System.Windows.Forms.Label();
            this.keyNum1_4 = new System.Windows.Forms.Label();
            this.keyNum3_2 = new System.Windows.Forms.Label();
            this.keyNum2_2 = new System.Windows.Forms.Label();
            this.keyNum1_3 = new System.Windows.Forms.Label();
            this.keyNum3_1 = new System.Windows.Forms.Label();
            this.keyNum2_1 = new System.Windows.Forms.Label();
            this.keyNum1_2 = new System.Windows.Forms.Label();
            this.keyNum1_1 = new System.Windows.Forms.Label();
            this.BattleMenuPanel = new System.Windows.Forms.Panel();
            this.RunAwayButton = new System.Windows.Forms.PictureBox();
            this.BattleSettingButton = new System.Windows.Forms.PictureBox();
            this.UseItemButton = new System.Windows.Forms.PictureBox();
            this.StackNameLabel = new System.Windows.Forms.Label();
            this.StackInTheCommandLabel = new System.Windows.Forms.Label();
            this.UseItemGauge = new System.Windows.Forms.Label();
            this.labelDamage2 = new System.Windows.Forms.Label();
            this.labelDamage3 = new System.Windows.Forms.Label();
            this.labelCritical3 = new System.Windows.Forms.Label();
            this.labelCritical2 = new System.Windows.Forms.Label();
            this.labelEnemyCritical2 = new System.Windows.Forms.Label();
            this.labelEnemyCritical3 = new System.Windows.Forms.Label();
            this.labelEnemyCritical1 = new System.Windows.Forms.Label();
            this.labelCritical1 = new System.Windows.Forms.Label();
            this.labelDamage1 = new System.Windows.Forms.Label();
            this.labelEnemyDamage3 = new System.Windows.Forms.Label();
            this.labelEnemyDamage2 = new System.Windows.Forms.Label();
            this.labelEnemyDamage1 = new System.Windows.Forms.Label();
            this.BuffPanel2 = new System.Windows.Forms.Panel();
            this.BuffPanel3 = new System.Windows.Forms.Panel();
            this.PanelBuffEnemy3 = new System.Windows.Forms.Panel();
            this.PanelBuffEnemy2 = new System.Windows.Forms.Panel();
            this.PanelBuffEnemy1 = new System.Windows.Forms.Panel();
            this.BuffPanel1 = new System.Windows.Forms.Panel();
            this.labelBattleTurn = new System.Windows.Forms.Label();
            this.currentManaPoint3 = new System.Windows.Forms.Label();
            this.currentSkillPoint3 = new System.Windows.Forms.Label();
            this.currentManaPoint2 = new System.Windows.Forms.Label();
            this.currentSkillPoint2 = new System.Windows.Forms.Label();
            this.currentEnemyManaPoint1 = new System.Windows.Forms.Label();
            this.currentEnemyInstantPoint3 = new System.Windows.Forms.Label();
            this.currentEnemyInstantPoint2 = new System.Windows.Forms.Label();
            this.currentEnemyInstantPoint1 = new System.Windows.Forms.Label();
            this.currentInstantPoint3 = new System.Windows.Forms.Label();
            this.currentInstantPoint2 = new System.Windows.Forms.Label();
            this.currentInstantPoint1 = new System.Windows.Forms.Label();
            this.currentManaPoint1 = new System.Windows.Forms.Label();
            this.currentEnemySkillPoint1 = new System.Windows.Forms.Label();
            this.currentSkillPoint1 = new System.Windows.Forms.Label();
            this.enemyNameLabel1 = new System.Windows.Forms.Label();
            this.lblLifeEnemy1 = new System.Windows.Forms.Label();
            this.pbEnemyTargetTarget3 = new System.Windows.Forms.PictureBox();
            this.pbPlayerTargetTarget3 = new System.Windows.Forms.PictureBox();
            this.pbEnemyTargetTarget1 = new System.Windows.Forms.PictureBox();
            this.pbPlayerTargetTarget1 = new System.Windows.Forms.PictureBox();
            this.pbEnemyTargetTarget2 = new System.Windows.Forms.PictureBox();
            this.pbPlayerTargetTarget2 = new System.Windows.Forms.PictureBox();
            this.buttonTargetPlayer2 = new System.Windows.Forms.PictureBox();
            this.playerActionLabel2 = new System.Windows.Forms.Label();
            this.nameLabel2 = new System.Windows.Forms.Label();
            this.lifeLabel2 = new System.Windows.Forms.Label();
            this.buttonTargetPlayer3 = new System.Windows.Forms.PictureBox();
            this.buttonTargetEnemy2 = new System.Windows.Forms.PictureBox();
            this.buttonTargetEnemy3 = new System.Windows.Forms.PictureBox();
            this.buttonTargetPlayer1 = new System.Windows.Forms.PictureBox();
            this.buttonTargetEnemy1 = new System.Windows.Forms.PictureBox();
            this.battleSpeedBar = new System.Windows.Forms.TrackBar();
            this.TimeSpeedLabel = new System.Windows.Forms.Label();
            this.playerActionLabel3 = new System.Windows.Forms.Label();
            this.enemyActionLabel3 = new System.Windows.Forms.Label();
            this.enemyActionLabel2 = new System.Windows.Forms.Label();
            this.enemyActionLabel1 = new System.Windows.Forms.Label();
            this.playerActionLabel1 = new System.Windows.Forms.Label();
            this.enemyNameLabel3 = new System.Windows.Forms.Label();
            this.enemyNameLabel2 = new System.Windows.Forms.Label();
            this.nameLabel3 = new System.Windows.Forms.Label();
            this.nameLabel1 = new System.Windows.Forms.Label();
            this.pbBattleTimerBar = new System.Windows.Forms.PictureBox();
            this.pbPlayer1 = new System.Windows.Forms.PictureBox();
            this.lblLifeEnemy3 = new System.Windows.Forms.Label();
            this.lifeLabel3 = new System.Windows.Forms.Label();
            this.lblLifeEnemy2 = new System.Windows.Forms.Label();
            this.lifeLabel1 = new System.Windows.Forms.Label();
            this.BattleStart = new System.Windows.Forms.Button();
            this.txtBattleMessage = new System.Windows.Forms.TextBox();
            this.ActionButton29 = new DungeonPlayer.TruthImage();
            this.ActionButton28 = new DungeonPlayer.TruthImage();
            this.ActionButton27 = new DungeonPlayer.TruthImage();
            this.ActionButton26 = new DungeonPlayer.TruthImage();
            this.ActionButton25 = new DungeonPlayer.TruthImage();
            this.ActionButton24 = new DungeonPlayer.TruthImage();
            this.ActionButton23 = new DungeonPlayer.TruthImage();
            this.ActionButton22 = new DungeonPlayer.TruthImage();
            this.ActionButton21 = new DungeonPlayer.TruthImage();
            this.ActionButton39 = new DungeonPlayer.TruthImage();
            this.ActionButton38 = new DungeonPlayer.TruthImage();
            this.ActionButton37 = new DungeonPlayer.TruthImage();
            this.ActionButton36 = new DungeonPlayer.TruthImage();
            this.ActionButton35 = new DungeonPlayer.TruthImage();
            this.ActionButton34 = new DungeonPlayer.TruthImage();
            this.ActionButton33 = new DungeonPlayer.TruthImage();
            this.ActionButton32 = new DungeonPlayer.TruthImage();
            this.ActionButton31 = new DungeonPlayer.TruthImage();
            this.ActionButton19 = new DungeonPlayer.TruthImage();
            this.ActionButton18 = new DungeonPlayer.TruthImage();
            this.ActionButton17 = new DungeonPlayer.TruthImage();
            this.ActionButton16 = new DungeonPlayer.TruthImage();
            this.ActionButton15 = new DungeonPlayer.TruthImage();
            this.ActionButton14 = new DungeonPlayer.TruthImage();
            this.ActionButton13 = new DungeonPlayer.TruthImage();
            this.ActionButton12 = new DungeonPlayer.TruthImage();
            this.ActionButton11 = new DungeonPlayer.TruthImage();
            this.IsSorcery11 = new DungeonPlayer.TruthImage();
            this.IsSorcery12 = new DungeonPlayer.TruthImage();
            this.IsSorcery13 = new DungeonPlayer.TruthImage();
            this.IsSorcery14 = new DungeonPlayer.TruthImage();
            this.IsSorcery15 = new DungeonPlayer.TruthImage();
            this.IsSorcery16 = new DungeonPlayer.TruthImage();
            this.IsSorcery17 = new DungeonPlayer.TruthImage();
            this.IsSorcery18 = new DungeonPlayer.TruthImage();
            this.IsSorcery19 = new DungeonPlayer.TruthImage();
            this.IsSorcery21 = new DungeonPlayer.TruthImage();
            this.IsSorcery22 = new DungeonPlayer.TruthImage();
            this.IsSorcery23 = new DungeonPlayer.TruthImage();
            this.IsSorcery24 = new DungeonPlayer.TruthImage();
            this.IsSorcery25 = new DungeonPlayer.TruthImage();
            this.IsSorcery26 = new DungeonPlayer.TruthImage();
            this.IsSorcery27 = new DungeonPlayer.TruthImage();
            this.IsSorcery28 = new DungeonPlayer.TruthImage();
            this.IsSorcery29 = new DungeonPlayer.TruthImage();
            this.IsSorcery31 = new DungeonPlayer.TruthImage();
            this.IsSorcery32 = new DungeonPlayer.TruthImage();
            this.IsSorcery33 = new DungeonPlayer.TruthImage();
            this.IsSorcery34 = new DungeonPlayer.TruthImage();
            this.IsSorcery35 = new DungeonPlayer.TruthImage();
            this.IsSorcery36 = new DungeonPlayer.TruthImage();
            this.IsSorcery37 = new DungeonPlayer.TruthImage();
            this.IsSorcery38 = new DungeonPlayer.TruthImage();
            this.IsSorcery39 = new DungeonPlayer.TruthImage();
            this.FieldBuff1 = new DungeonPlayer.TruthImage();
            this.FieldBuff2 = new DungeonPlayer.TruthImage();
            this.FieldBuff3 = new DungeonPlayer.TruthImage();
            this.FieldBuff4 = new DungeonPlayer.TruthImage();
            this.FieldBuff5 = new DungeonPlayer.TruthImage();
            this.FieldBuff6 = new DungeonPlayer.TruthImage();
            this.pbSandglass = new System.Windows.Forms.PictureBox();
            this.lblTimerCount = new System.Windows.Forms.Label();
            this.pbAnimeSandGlass = new System.Windows.Forms.PictureBox();
            this.labelSpecialInstant = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbMatrixDragon)).BeginInit();
            this.BattleMenuPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RunAwayButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BattleSettingButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UseItemButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEnemyTargetTarget3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlayerTargetTarget3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEnemyTargetTarget1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlayerTargetTarget1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEnemyTargetTarget2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlayerTargetTarget2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonTargetPlayer2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonTargetPlayer3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonTargetEnemy2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonTargetEnemy3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonTargetPlayer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonTargetEnemy1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.battleSpeedBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBattleTimerBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlayer1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton29)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton39)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton38)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton37)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton36)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton35)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton34)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton33)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton31)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery21)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery22)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery23)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery24)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery25)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery26)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery27)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery28)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery29)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery31)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery32)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery33)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery34)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery35)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery36)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery37)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery38)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery39)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FieldBuff1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FieldBuff2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FieldBuff3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FieldBuff4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FieldBuff5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FieldBuff6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSandglass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAnimeSandGlass)).BeginInit();
            this.SuspendLayout();
            // 
            // timerBattleStart
            // 
            this.timerBattleStart.Interval = 500;
            this.timerBattleStart.Tick += new System.EventHandler(this.timerBattleStart_Tick);
            // 
            // MatrixDragonTalk
            // 
            this.MatrixDragonTalk.BackColor = System.Drawing.Color.Black;
            this.MatrixDragonTalk.Font = new System.Drawing.Font("HG正楷書体-PRO", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.MatrixDragonTalk.ForeColor = System.Drawing.Color.White;
            this.MatrixDragonTalk.Location = new System.Drawing.Point(9, 699);
            this.MatrixDragonTalk.Name = "MatrixDragonTalk";
            this.MatrixDragonTalk.Size = new System.Drawing.Size(1024, 100);
            this.MatrixDragonTalk.TabIndex = 62;
            this.MatrixDragonTalk.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.MatrixDragonTalk.Visible = false;
            // 
            // pbMatrixDragon
            // 
            this.pbMatrixDragon.Location = new System.Drawing.Point(-300, -300);
            this.pbMatrixDragon.Name = "pbMatrixDragon";
            this.pbMatrixDragon.Size = new System.Drawing.Size(100, 50);
            this.pbMatrixDragon.TabIndex = 61;
            this.pbMatrixDragon.TabStop = false;
            this.pbMatrixDragon.Visible = false;
            // 
            // keyNum3_9
            // 
            this.keyNum3_9.AutoSize = true;
            this.keyNum3_9.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum3_9.Location = new System.Drawing.Point(411, 465);
            this.keyNum3_9.Name = "keyNum3_9";
            this.keyNum3_9.Size = new System.Drawing.Size(15, 13);
            this.keyNum3_9.TabIndex = 60;
            this.keyNum3_9.Text = "L";
            this.keyNum3_9.Visible = false;
            // 
            // keyNum3_8
            // 
            this.keyNum3_8.AutoSize = true;
            this.keyNum3_8.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum3_8.Location = new System.Drawing.Point(361, 465);
            this.keyNum3_8.Name = "keyNum3_8";
            this.keyNum3_8.Size = new System.Drawing.Size(16, 13);
            this.keyNum3_8.TabIndex = 60;
            this.keyNum3_8.Text = "K";
            this.keyNum3_8.Visible = false;
            // 
            // keyNum3_7
            // 
            this.keyNum3_7.AutoSize = true;
            this.keyNum3_7.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum3_7.Location = new System.Drawing.Point(311, 465);
            this.keyNum3_7.Name = "keyNum3_7";
            this.keyNum3_7.Size = new System.Drawing.Size(15, 13);
            this.keyNum3_7.TabIndex = 60;
            this.keyNum3_7.Text = "J";
            this.keyNum3_7.Visible = false;
            // 
            // keyNum2_9
            // 
            this.keyNum2_9.AutoSize = true;
            this.keyNum2_9.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum2_9.Location = new System.Drawing.Point(411, 310);
            this.keyNum2_9.Name = "keyNum2_9";
            this.keyNum2_9.Size = new System.Drawing.Size(17, 13);
            this.keyNum2_9.TabIndex = 60;
            this.keyNum2_9.Text = "O";
            this.keyNum2_9.Visible = false;
            // 
            // keyNum2_8
            // 
            this.keyNum2_8.AutoSize = true;
            this.keyNum2_8.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum2_8.Location = new System.Drawing.Point(361, 310);
            this.keyNum2_8.Name = "keyNum2_8";
            this.keyNum2_8.Size = new System.Drawing.Size(11, 13);
            this.keyNum2_8.TabIndex = 60;
            this.keyNum2_8.Text = "I";
            this.keyNum2_8.Visible = false;
            // 
            // keyNum2_7
            // 
            this.keyNum2_7.AutoSize = true;
            this.keyNum2_7.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum2_7.Location = new System.Drawing.Point(311, 310);
            this.keyNum2_7.Name = "keyNum2_7";
            this.keyNum2_7.Size = new System.Drawing.Size(16, 13);
            this.keyNum2_7.TabIndex = 60;
            this.keyNum2_7.Text = "U";
            this.keyNum2_7.Visible = false;
            // 
            // keyNum3_6
            // 
            this.keyNum3_6.AutoSize = true;
            this.keyNum3_6.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum3_6.Location = new System.Drawing.Point(261, 465);
            this.keyNum3_6.Name = "keyNum3_6";
            this.keyNum3_6.Size = new System.Drawing.Size(16, 13);
            this.keyNum3_6.TabIndex = 60;
            this.keyNum3_6.Text = "H";
            this.keyNum3_6.Visible = false;
            // 
            // keyNum2_6
            // 
            this.keyNum2_6.AutoSize = true;
            this.keyNum2_6.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum2_6.Location = new System.Drawing.Point(261, 310);
            this.keyNum2_6.Name = "keyNum2_6";
            this.keyNum2_6.Size = new System.Drawing.Size(16, 13);
            this.keyNum2_6.TabIndex = 60;
            this.keyNum2_6.Text = "Y";
            this.keyNum2_6.Visible = false;
            // 
            // keyNum1_9
            // 
            this.keyNum1_9.AutoSize = true;
            this.keyNum1_9.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum1_9.Location = new System.Drawing.Point(411, 150);
            this.keyNum1_9.Name = "keyNum1_9";
            this.keyNum1_9.Size = new System.Drawing.Size(15, 13);
            this.keyNum1_9.TabIndex = 60;
            this.keyNum1_9.Text = "9";
            this.keyNum1_9.Visible = false;
            // 
            // keyNum1_8
            // 
            this.keyNum1_8.AutoSize = true;
            this.keyNum1_8.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum1_8.Location = new System.Drawing.Point(361, 150);
            this.keyNum1_8.Name = "keyNum1_8";
            this.keyNum1_8.Size = new System.Drawing.Size(15, 13);
            this.keyNum1_8.TabIndex = 60;
            this.keyNum1_8.Text = "8";
            this.keyNum1_8.Visible = false;
            // 
            // keyNum1_7
            // 
            this.keyNum1_7.AutoSize = true;
            this.keyNum1_7.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum1_7.Location = new System.Drawing.Point(311, 150);
            this.keyNum1_7.Name = "keyNum1_7";
            this.keyNum1_7.Size = new System.Drawing.Size(15, 13);
            this.keyNum1_7.TabIndex = 60;
            this.keyNum1_7.Text = "7";
            this.keyNum1_7.Visible = false;
            // 
            // keyNum3_5
            // 
            this.keyNum3_5.AutoSize = true;
            this.keyNum3_5.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum3_5.Location = new System.Drawing.Point(211, 465);
            this.keyNum3_5.Name = "keyNum3_5";
            this.keyNum3_5.Size = new System.Drawing.Size(17, 13);
            this.keyNum3_5.TabIndex = 60;
            this.keyNum3_5.Text = "G";
            this.keyNum3_5.Visible = false;
            // 
            // keyNum2_5
            // 
            this.keyNum2_5.AutoSize = true;
            this.keyNum2_5.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum2_5.Location = new System.Drawing.Point(211, 310);
            this.keyNum2_5.Name = "keyNum2_5";
            this.keyNum2_5.Size = new System.Drawing.Size(16, 13);
            this.keyNum2_5.TabIndex = 60;
            this.keyNum2_5.Text = "T";
            this.keyNum2_5.Visible = false;
            // 
            // keyNum1_6
            // 
            this.keyNum1_6.AutoSize = true;
            this.keyNum1_6.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum1_6.Location = new System.Drawing.Point(261, 150);
            this.keyNum1_6.Name = "keyNum1_6";
            this.keyNum1_6.Size = new System.Drawing.Size(15, 13);
            this.keyNum1_6.TabIndex = 60;
            this.keyNum1_6.Text = "6";
            this.keyNum1_6.Visible = false;
            // 
            // keyNum3_4
            // 
            this.keyNum3_4.AutoSize = true;
            this.keyNum3_4.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum3_4.Location = new System.Drawing.Point(161, 465);
            this.keyNum3_4.Name = "keyNum3_4";
            this.keyNum3_4.Size = new System.Drawing.Size(15, 13);
            this.keyNum3_4.TabIndex = 60;
            this.keyNum3_4.Text = "F";
            this.keyNum3_4.Visible = false;
            // 
            // keyNum2_4
            // 
            this.keyNum2_4.AutoSize = true;
            this.keyNum2_4.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum2_4.Location = new System.Drawing.Point(161, 310);
            this.keyNum2_4.Name = "keyNum2_4";
            this.keyNum2_4.Size = new System.Drawing.Size(16, 13);
            this.keyNum2_4.TabIndex = 60;
            this.keyNum2_4.Text = "R";
            this.keyNum2_4.Visible = false;
            // 
            // keyNum1_5
            // 
            this.keyNum1_5.AutoSize = true;
            this.keyNum1_5.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum1_5.Location = new System.Drawing.Point(211, 150);
            this.keyNum1_5.Name = "keyNum1_5";
            this.keyNum1_5.Size = new System.Drawing.Size(15, 13);
            this.keyNum1_5.TabIndex = 60;
            this.keyNum1_5.Text = "5";
            this.keyNum1_5.Visible = false;
            // 
            // keyNum3_3
            // 
            this.keyNum3_3.AutoSize = true;
            this.keyNum3_3.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum3_3.Location = new System.Drawing.Point(111, 465);
            this.keyNum3_3.Name = "keyNum3_3";
            this.keyNum3_3.Size = new System.Drawing.Size(16, 13);
            this.keyNum3_3.TabIndex = 60;
            this.keyNum3_3.Text = "D";
            this.keyNum3_3.Visible = false;
            // 
            // keyNum2_3
            // 
            this.keyNum2_3.AutoSize = true;
            this.keyNum2_3.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum2_3.Location = new System.Drawing.Point(111, 310);
            this.keyNum2_3.Name = "keyNum2_3";
            this.keyNum2_3.Size = new System.Drawing.Size(15, 13);
            this.keyNum2_3.TabIndex = 60;
            this.keyNum2_3.Text = "E";
            this.keyNum2_3.Visible = false;
            // 
            // keyNum1_4
            // 
            this.keyNum1_4.AutoSize = true;
            this.keyNum1_4.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum1_4.Location = new System.Drawing.Point(161, 150);
            this.keyNum1_4.Name = "keyNum1_4";
            this.keyNum1_4.Size = new System.Drawing.Size(15, 13);
            this.keyNum1_4.TabIndex = 60;
            this.keyNum1_4.Text = "4";
            this.keyNum1_4.Visible = false;
            // 
            // keyNum3_2
            // 
            this.keyNum3_2.AutoSize = true;
            this.keyNum3_2.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum3_2.Location = new System.Drawing.Point(61, 465);
            this.keyNum3_2.Name = "keyNum3_2";
            this.keyNum3_2.Size = new System.Drawing.Size(16, 13);
            this.keyNum3_2.TabIndex = 60;
            this.keyNum3_2.Text = "S";
            this.keyNum3_2.Visible = false;
            // 
            // keyNum2_2
            // 
            this.keyNum2_2.AutoSize = true;
            this.keyNum2_2.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum2_2.Location = new System.Drawing.Point(61, 310);
            this.keyNum2_2.Name = "keyNum2_2";
            this.keyNum2_2.Size = new System.Drawing.Size(18, 13);
            this.keyNum2_2.TabIndex = 60;
            this.keyNum2_2.Text = "W";
            this.keyNum2_2.Visible = false;
            // 
            // keyNum1_3
            // 
            this.keyNum1_3.AutoSize = true;
            this.keyNum1_3.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum1_3.Location = new System.Drawing.Point(111, 150);
            this.keyNum1_3.Name = "keyNum1_3";
            this.keyNum1_3.Size = new System.Drawing.Size(15, 13);
            this.keyNum1_3.TabIndex = 60;
            this.keyNum1_3.Text = "3";
            this.keyNum1_3.Visible = false;
            // 
            // keyNum3_1
            // 
            this.keyNum3_1.AutoSize = true;
            this.keyNum3_1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum3_1.Location = new System.Drawing.Point(11, 465);
            this.keyNum3_1.Name = "keyNum3_1";
            this.keyNum3_1.Size = new System.Drawing.Size(16, 13);
            this.keyNum3_1.TabIndex = 60;
            this.keyNum3_1.Text = "A";
            this.keyNum3_1.Visible = false;
            // 
            // keyNum2_1
            // 
            this.keyNum2_1.AutoSize = true;
            this.keyNum2_1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum2_1.Location = new System.Drawing.Point(11, 310);
            this.keyNum2_1.Name = "keyNum2_1";
            this.keyNum2_1.Size = new System.Drawing.Size(17, 13);
            this.keyNum2_1.TabIndex = 60;
            this.keyNum2_1.Text = "Q";
            this.keyNum2_1.Visible = false;
            // 
            // keyNum1_2
            // 
            this.keyNum1_2.AutoSize = true;
            this.keyNum1_2.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum1_2.Location = new System.Drawing.Point(61, 150);
            this.keyNum1_2.Name = "keyNum1_2";
            this.keyNum1_2.Size = new System.Drawing.Size(15, 13);
            this.keyNum1_2.TabIndex = 60;
            this.keyNum1_2.Text = "2";
            this.keyNum1_2.Visible = false;
            // 
            // keyNum1_1
            // 
            this.keyNum1_1.AutoSize = true;
            this.keyNum1_1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.keyNum1_1.Location = new System.Drawing.Point(11, 150);
            this.keyNum1_1.Name = "keyNum1_1";
            this.keyNum1_1.Size = new System.Drawing.Size(15, 13);
            this.keyNum1_1.TabIndex = 60;
            this.keyNum1_1.Text = "1";
            this.keyNum1_1.Visible = false;
            // 
            // BattleMenuPanel
            // 
            this.BattleMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.BattleMenuPanel.Controls.Add(this.RunAwayButton);
            this.BattleMenuPanel.Controls.Add(this.BattleSettingButton);
            this.BattleMenuPanel.Controls.Add(this.UseItemButton);
            this.BattleMenuPanel.Location = new System.Drawing.Point(734, 598);
            this.BattleMenuPanel.Name = "BattleMenuPanel";
            this.BattleMenuPanel.Size = new System.Drawing.Size(276, 50);
            this.BattleMenuPanel.TabIndex = 59;
            // 
            // RunAwayButton
            // 
            this.RunAwayButton.BackColor = System.Drawing.Color.AliceBlue;
            this.RunAwayButton.Location = new System.Drawing.Point(202, 0);
            this.RunAwayButton.Name = "RunAwayButton";
            this.RunAwayButton.Size = new System.Drawing.Size(50, 50);
            this.RunAwayButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.RunAwayButton.TabIndex = 3;
            this.RunAwayButton.TabStop = false;
            this.RunAwayButton.Text = "逃げる";
            this.RunAwayButton.Click += new System.EventHandler(this.RunAwayButton_Click);
            // 
            // BattleSettingButton
            // 
            this.BattleSettingButton.BackColor = System.Drawing.Color.AliceBlue;
            this.BattleSettingButton.Location = new System.Drawing.Point(26, 0);
            this.BattleSettingButton.Name = "BattleSettingButton";
            this.BattleSettingButton.Size = new System.Drawing.Size(50, 50);
            this.BattleSettingButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.BattleSettingButton.TabIndex = 3;
            this.BattleSettingButton.TabStop = false;
            this.BattleSettingButton.Text = "バトル設定";
            this.BattleSettingButton.Visible = false;
            this.BattleSettingButton.Click += new System.EventHandler(this.BattleSettingButton_Click);
            // 
            // UseItemButton
            // 
            this.UseItemButton.BackColor = System.Drawing.Color.AliceBlue;
            this.UseItemButton.Location = new System.Drawing.Point(114, 0);
            this.UseItemButton.Name = "UseItemButton";
            this.UseItemButton.Size = new System.Drawing.Size(50, 50);
            this.UseItemButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.UseItemButton.TabIndex = 3;
            this.UseItemButton.TabStop = false;
            this.UseItemButton.Text = "アイテム使用";
            this.UseItemButton.Click += new System.EventHandler(this.UseItemButton_Click);
            // 
            // StackNameLabel
            // 
            this.StackNameLabel.BackColor = System.Drawing.Color.Red;
            this.StackNameLabel.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.StackNameLabel.ForeColor = System.Drawing.Color.White;
            this.StackNameLabel.Location = new System.Drawing.Point(10, 598);
            this.StackNameLabel.Name = "StackNameLabel";
            this.StackNameLabel.Size = new System.Drawing.Size(1000, 24);
            this.StackNameLabel.TabIndex = 58;
            this.StackNameLabel.Text = "ファイア・ボール";
            this.StackNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StackInTheCommandLabel
            // 
            this.StackInTheCommandLabel.BackColor = System.Drawing.Color.Red;
            this.StackInTheCommandLabel.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.StackInTheCommandLabel.ForeColor = System.Drawing.Color.White;
            this.StackInTheCommandLabel.Location = new System.Drawing.Point(10, 622);
            this.StackInTheCommandLabel.Name = "StackInTheCommandLabel";
            this.StackInTheCommandLabel.Size = new System.Drawing.Size(1000, 24);
            this.StackInTheCommandLabel.TabIndex = 58;
            this.StackInTheCommandLabel.Text = "3000";
            this.StackInTheCommandLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UseItemGauge
            // 
            this.UseItemGauge.BackColor = System.Drawing.Color.DarkBlue;
            this.UseItemGauge.Font = new System.Drawing.Font("MS UI Gothic", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.UseItemGauge.Location = new System.Drawing.Point(10, 655);
            this.UseItemGauge.Name = "UseItemGauge";
            this.UseItemGauge.Size = new System.Drawing.Size(1000, 10);
            this.UseItemGauge.TabIndex = 45;
            this.UseItemGauge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelDamage2
            // 
            this.labelDamage2.AutoSize = true;
            this.labelDamage2.BackColor = System.Drawing.Color.Transparent;
            this.labelDamage2.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelDamage2.Location = new System.Drawing.Point(561, 353);
            this.labelDamage2.Name = "labelDamage2";
            this.labelDamage2.Size = new System.Drawing.Size(22, 21);
            this.labelDamage2.TabIndex = 57;
            this.labelDamage2.Text = "0";
            this.labelDamage2.Visible = false;
            // 
            // labelDamage3
            // 
            this.labelDamage3.AutoSize = true;
            this.labelDamage3.BackColor = System.Drawing.Color.Transparent;
            this.labelDamage3.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelDamage3.Location = new System.Drawing.Point(560, 510);
            this.labelDamage3.Name = "labelDamage3";
            this.labelDamage3.Size = new System.Drawing.Size(22, 21);
            this.labelDamage3.TabIndex = 57;
            this.labelDamage3.Text = "0";
            this.labelDamage3.Visible = false;
            // 
            // labelCritical3
            // 
            this.labelCritical3.BackColor = System.Drawing.Color.Transparent;
            this.labelCritical3.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelCritical3.Location = new System.Drawing.Point(560, 487);
            this.labelCritical3.Name = "labelCritical3";
            this.labelCritical3.Size = new System.Drawing.Size(78, 23);
            this.labelCritical3.TabIndex = 57;
            this.labelCritical3.Text = "Critical";
            this.labelCritical3.Visible = false;
            // 
            // labelCritical2
            // 
            this.labelCritical2.BackColor = System.Drawing.Color.Transparent;
            this.labelCritical2.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelCritical2.Location = new System.Drawing.Point(561, 330);
            this.labelCritical2.Name = "labelCritical2";
            this.labelCritical2.Size = new System.Drawing.Size(78, 23);
            this.labelCritical2.TabIndex = 57;
            this.labelCritical2.Text = "Critical";
            this.labelCritical2.Visible = false;
            // 
            // labelEnemyCritical2
            // 
            this.labelEnemyCritical2.BackColor = System.Drawing.Color.Transparent;
            this.labelEnemyCritical2.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelEnemyCritical2.Location = new System.Drawing.Point(679, 330);
            this.labelEnemyCritical2.Name = "labelEnemyCritical2";
            this.labelEnemyCritical2.Size = new System.Drawing.Size(78, 23);
            this.labelEnemyCritical2.TabIndex = 57;
            this.labelEnemyCritical2.Text = "Critical";
            this.labelEnemyCritical2.Visible = false;
            // 
            // labelEnemyCritical3
            // 
            this.labelEnemyCritical3.BackColor = System.Drawing.Color.Transparent;
            this.labelEnemyCritical3.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelEnemyCritical3.Location = new System.Drawing.Point(680, 487);
            this.labelEnemyCritical3.Name = "labelEnemyCritical3";
            this.labelEnemyCritical3.Size = new System.Drawing.Size(78, 23);
            this.labelEnemyCritical3.TabIndex = 57;
            this.labelEnemyCritical3.Text = "Critical";
            this.labelEnemyCritical3.Visible = false;
            // 
            // labelEnemyCritical1
            // 
            this.labelEnemyCritical1.BackColor = System.Drawing.Color.Transparent;
            this.labelEnemyCritical1.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelEnemyCritical1.Location = new System.Drawing.Point(679, 170);
            this.labelEnemyCritical1.Name = "labelEnemyCritical1";
            this.labelEnemyCritical1.Size = new System.Drawing.Size(78, 23);
            this.labelEnemyCritical1.TabIndex = 57;
            this.labelEnemyCritical1.Text = "Critical";
            this.labelEnemyCritical1.Visible = false;
            // 
            // labelCritical1
            // 
            this.labelCritical1.BackColor = System.Drawing.Color.Transparent;
            this.labelCritical1.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelCritical1.Location = new System.Drawing.Point(559, 170);
            this.labelCritical1.Name = "labelCritical1";
            this.labelCritical1.Size = new System.Drawing.Size(80, 23);
            this.labelCritical1.TabIndex = 57;
            this.labelCritical1.Text = "Critical";
            this.labelCritical1.Visible = false;
            // 
            // labelDamage1
            // 
            this.labelDamage1.AutoSize = true;
            this.labelDamage1.BackColor = System.Drawing.Color.Transparent;
            this.labelDamage1.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelDamage1.Location = new System.Drawing.Point(559, 193);
            this.labelDamage1.Name = "labelDamage1";
            this.labelDamage1.Size = new System.Drawing.Size(22, 21);
            this.labelDamage1.TabIndex = 57;
            this.labelDamage1.Text = "0";
            this.labelDamage1.Visible = false;
            // 
            // labelEnemyDamage3
            // 
            this.labelEnemyDamage3.AutoSize = true;
            this.labelEnemyDamage3.BackColor = System.Drawing.Color.Transparent;
            this.labelEnemyDamage3.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelEnemyDamage3.Location = new System.Drawing.Point(680, 510);
            this.labelEnemyDamage3.Name = "labelEnemyDamage3";
            this.labelEnemyDamage3.Size = new System.Drawing.Size(22, 21);
            this.labelEnemyDamage3.TabIndex = 57;
            this.labelEnemyDamage3.Text = "0";
            this.labelEnemyDamage3.Visible = false;
            // 
            // labelEnemyDamage2
            // 
            this.labelEnemyDamage2.AutoSize = true;
            this.labelEnemyDamage2.BackColor = System.Drawing.Color.Transparent;
            this.labelEnemyDamage2.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelEnemyDamage2.Location = new System.Drawing.Point(679, 353);
            this.labelEnemyDamage2.Name = "labelEnemyDamage2";
            this.labelEnemyDamage2.Size = new System.Drawing.Size(22, 21);
            this.labelEnemyDamage2.TabIndex = 57;
            this.labelEnemyDamage2.Text = "0";
            this.labelEnemyDamage2.Visible = false;
            // 
            // labelEnemyDamage1
            // 
            this.labelEnemyDamage1.AutoSize = true;
            this.labelEnemyDamage1.BackColor = System.Drawing.Color.Transparent;
            this.labelEnemyDamage1.Font = new System.Drawing.Font("MS UI Gothic", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelEnemyDamage1.Location = new System.Drawing.Point(679, 193);
            this.labelEnemyDamage1.Name = "labelEnemyDamage1";
            this.labelEnemyDamage1.Size = new System.Drawing.Size(22, 21);
            this.labelEnemyDamage1.TabIndex = 57;
            this.labelEnemyDamage1.Text = "0";
            this.labelEnemyDamage1.Visible = false;
            // 
            // BuffPanel2
            // 
            this.BuffPanel2.Location = new System.Drawing.Point(10, 264);
            this.BuffPanel2.Name = "BuffPanel2";
            this.BuffPanel2.Size = new System.Drawing.Size(450, 40);
            this.BuffPanel2.TabIndex = 56;
            this.BuffPanel2.Visible = false;
            // 
            // BuffPanel3
            // 
            this.BuffPanel3.Location = new System.Drawing.Point(10, 422);
            this.BuffPanel3.Name = "BuffPanel3";
            this.BuffPanel3.Size = new System.Drawing.Size(450, 40);
            this.BuffPanel3.TabIndex = 56;
            this.BuffPanel3.Visible = false;
            // 
            // PanelBuffEnemy3
            // 
            this.PanelBuffEnemy3.BackColor = System.Drawing.Color.GhostWhite;
            this.PanelBuffEnemy3.Location = new System.Drawing.Point(664, 420);
            this.PanelBuffEnemy3.Name = "PanelBuffEnemy3";
            this.PanelBuffEnemy3.Size = new System.Drawing.Size(346, 40);
            this.PanelBuffEnemy3.TabIndex = 56;
            this.PanelBuffEnemy3.Visible = false;
            // 
            // PanelBuffEnemy2
            // 
            this.PanelBuffEnemy2.BackColor = System.Drawing.Color.GhostWhite;
            this.PanelBuffEnemy2.Location = new System.Drawing.Point(663, 262);
            this.PanelBuffEnemy2.Name = "PanelBuffEnemy2";
            this.PanelBuffEnemy2.Size = new System.Drawing.Size(349, 40);
            this.PanelBuffEnemy2.TabIndex = 56;
            this.PanelBuffEnemy2.Visible = false;
            // 
            // PanelBuffEnemy1
            // 
            this.PanelBuffEnemy1.BackColor = System.Drawing.Color.GhostWhite;
            this.PanelBuffEnemy1.Location = new System.Drawing.Point(663, 104);
            this.PanelBuffEnemy1.Name = "PanelBuffEnemy1";
            this.PanelBuffEnemy1.Size = new System.Drawing.Size(349, 40);
            this.PanelBuffEnemy1.TabIndex = 56;
            this.PanelBuffEnemy1.Visible = false;
            // 
            // BuffPanel1
            // 
            this.BuffPanel1.Location = new System.Drawing.Point(10, 106);
            this.BuffPanel1.Name = "BuffPanel1";
            this.BuffPanel1.Size = new System.Drawing.Size(451, 40);
            this.BuffPanel1.TabIndex = 56;
            this.BuffPanel1.Visible = false;
            // 
            // labelBattleTurn
            // 
            this.labelBattleTurn.AutoSize = true;
            this.labelBattleTurn.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelBattleTurn.Location = new System.Drawing.Point(1, 9);
            this.labelBattleTurn.Name = "labelBattleTurn";
            this.labelBattleTurn.Size = new System.Drawing.Size(76, 19);
            this.labelBattleTurn.TabIndex = 55;
            this.labelBattleTurn.Text = "ターン １";
            // 
            // currentManaPoint3
            // 
            this.currentManaPoint3.BackColor = System.Drawing.Color.Violet;
            this.currentManaPoint3.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentManaPoint3.Location = new System.Drawing.Point(10, 546);
            this.currentManaPoint3.Name = "currentManaPoint3";
            this.currentManaPoint3.Size = new System.Drawing.Size(450, 12);
            this.currentManaPoint3.TabIndex = 52;
            this.currentManaPoint3.Text = "100 / 100";
            this.currentManaPoint3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.currentManaPoint3.Visible = false;
            // 
            // currentSkillPoint3
            // 
            this.currentSkillPoint3.BackColor = System.Drawing.Color.GreenYellow;
            this.currentSkillPoint3.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentSkillPoint3.Location = new System.Drawing.Point(10, 534);
            this.currentSkillPoint3.Name = "currentSkillPoint3";
            this.currentSkillPoint3.Size = new System.Drawing.Size(450, 12);
            this.currentSkillPoint3.TabIndex = 53;
            this.currentSkillPoint3.Text = "100 / 100";
            this.currentSkillPoint3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.currentSkillPoint3.Visible = false;
            // 
            // currentManaPoint2
            // 
            this.currentManaPoint2.BackColor = System.Drawing.Color.Violet;
            this.currentManaPoint2.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentManaPoint2.Location = new System.Drawing.Point(10, 388);
            this.currentManaPoint2.Name = "currentManaPoint2";
            this.currentManaPoint2.Size = new System.Drawing.Size(450, 12);
            this.currentManaPoint2.TabIndex = 48;
            this.currentManaPoint2.Text = "100 / 100";
            this.currentManaPoint2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.currentManaPoint2.Visible = false;
            // 
            // currentSkillPoint2
            // 
            this.currentSkillPoint2.BackColor = System.Drawing.Color.GreenYellow;
            this.currentSkillPoint2.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentSkillPoint2.Location = new System.Drawing.Point(10, 376);
            this.currentSkillPoint2.Name = "currentSkillPoint2";
            this.currentSkillPoint2.Size = new System.Drawing.Size(450, 12);
            this.currentSkillPoint2.TabIndex = 49;
            this.currentSkillPoint2.Text = "100 / 100";
            this.currentSkillPoint2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.currentSkillPoint2.Visible = false;
            // 
            // currentEnemyManaPoint1
            // 
            this.currentEnemyManaPoint1.BackColor = System.Drawing.Color.Violet;
            this.currentEnemyManaPoint1.Font = new System.Drawing.Font("MS UI Gothic", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentEnemyManaPoint1.Location = new System.Drawing.Point(835, 395);
            this.currentEnemyManaPoint1.Name = "currentEnemyManaPoint1";
            this.currentEnemyManaPoint1.Size = new System.Drawing.Size(175, 15);
            this.currentEnemyManaPoint1.TabIndex = 45;
            this.currentEnemyManaPoint1.Text = "100 / 100";
            this.currentEnemyManaPoint1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.currentEnemyManaPoint1.Visible = false;
            // 
            // currentEnemyInstantPoint3
            // 
            this.currentEnemyInstantPoint3.BackColor = System.Drawing.Color.Aquamarine;
            this.currentEnemyInstantPoint3.Font = new System.Drawing.Font("MS UI Gothic", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentEnemyInstantPoint3.Location = new System.Drawing.Point(835, 580);
            this.currentEnemyInstantPoint3.Name = "currentEnemyInstantPoint3";
            this.currentEnemyInstantPoint3.Size = new System.Drawing.Size(175, 15);
            this.currentEnemyInstantPoint3.TabIndex = 45;
            this.currentEnemyInstantPoint3.Text = "300 / 300";
            this.currentEnemyInstantPoint3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.currentEnemyInstantPoint3.Visible = false;
            // 
            // currentEnemyInstantPoint2
            // 
            this.currentEnemyInstantPoint2.BackColor = System.Drawing.Color.Aquamarine;
            this.currentEnemyInstantPoint2.Font = new System.Drawing.Font("MS UI Gothic", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentEnemyInstantPoint2.Location = new System.Drawing.Point(835, 564);
            this.currentEnemyInstantPoint2.Name = "currentEnemyInstantPoint2";
            this.currentEnemyInstantPoint2.Size = new System.Drawing.Size(175, 15);
            this.currentEnemyInstantPoint2.TabIndex = 45;
            this.currentEnemyInstantPoint2.Text = "300 / 300";
            this.currentEnemyInstantPoint2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.currentEnemyInstantPoint2.Visible = false;
            // 
            // currentEnemyInstantPoint1
            // 
            this.currentEnemyInstantPoint1.BackColor = System.Drawing.Color.Aquamarine;
            this.currentEnemyInstantPoint1.Font = new System.Drawing.Font("MS UI Gothic", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentEnemyInstantPoint1.Location = new System.Drawing.Point(835, 412);
            this.currentEnemyInstantPoint1.Name = "currentEnemyInstantPoint1";
            this.currentEnemyInstantPoint1.Size = new System.Drawing.Size(175, 15);
            this.currentEnemyInstantPoint1.TabIndex = 45;
            this.currentEnemyInstantPoint1.Text = "300 / 300";
            this.currentEnemyInstantPoint1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.currentEnemyInstantPoint1.Visible = false;
            // 
            // currentInstantPoint3
            // 
            this.currentInstantPoint3.BackColor = System.Drawing.Color.Aquamarine;
            this.currentInstantPoint3.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentInstantPoint3.Location = new System.Drawing.Point(10, 558);
            this.currentInstantPoint3.Name = "currentInstantPoint3";
            this.currentInstantPoint3.Size = new System.Drawing.Size(450, 12);
            this.currentInstantPoint3.TabIndex = 45;
            this.currentInstantPoint3.Text = "300 / 300";
            this.currentInstantPoint3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.currentInstantPoint3.Visible = false;
            // 
            // currentInstantPoint2
            // 
            this.currentInstantPoint2.BackColor = System.Drawing.Color.Aquamarine;
            this.currentInstantPoint2.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentInstantPoint2.Location = new System.Drawing.Point(10, 400);
            this.currentInstantPoint2.Name = "currentInstantPoint2";
            this.currentInstantPoint2.Size = new System.Drawing.Size(450, 12);
            this.currentInstantPoint2.TabIndex = 45;
            this.currentInstantPoint2.Text = "300 / 300";
            this.currentInstantPoint2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.currentInstantPoint2.Visible = false;
            // 
            // currentInstantPoint1
            // 
            this.currentInstantPoint1.BackColor = System.Drawing.Color.Aquamarine;
            this.currentInstantPoint1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentInstantPoint1.Location = new System.Drawing.Point(10, 242);
            this.currentInstantPoint1.Name = "currentInstantPoint1";
            this.currentInstantPoint1.Size = new System.Drawing.Size(450, 12);
            this.currentInstantPoint1.TabIndex = 45;
            this.currentInstantPoint1.Text = "300 / 300";
            this.currentInstantPoint1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.currentInstantPoint1.Visible = false;
            // 
            // currentManaPoint1
            // 
            this.currentManaPoint1.BackColor = System.Drawing.Color.Violet;
            this.currentManaPoint1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentManaPoint1.Location = new System.Drawing.Point(10, 230);
            this.currentManaPoint1.Name = "currentManaPoint1";
            this.currentManaPoint1.Size = new System.Drawing.Size(450, 12);
            this.currentManaPoint1.TabIndex = 45;
            this.currentManaPoint1.Text = "100 / 100";
            this.currentManaPoint1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.currentManaPoint1.Visible = false;
            // 
            // currentEnemySkillPoint1
            // 
            this.currentEnemySkillPoint1.BackColor = System.Drawing.Color.GreenYellow;
            this.currentEnemySkillPoint1.Font = new System.Drawing.Font("MS UI Gothic", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentEnemySkillPoint1.Location = new System.Drawing.Point(835, 378);
            this.currentEnemySkillPoint1.Name = "currentEnemySkillPoint1";
            this.currentEnemySkillPoint1.Size = new System.Drawing.Size(175, 15);
            this.currentEnemySkillPoint1.TabIndex = 45;
            this.currentEnemySkillPoint1.Text = "100 / 100";
            this.currentEnemySkillPoint1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.currentEnemySkillPoint1.Visible = false;
            // 
            // currentSkillPoint1
            // 
            this.currentSkillPoint1.BackColor = System.Drawing.Color.GreenYellow;
            this.currentSkillPoint1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.currentSkillPoint1.Location = new System.Drawing.Point(10, 218);
            this.currentSkillPoint1.Name = "currentSkillPoint1";
            this.currentSkillPoint1.Size = new System.Drawing.Size(450, 12);
            this.currentSkillPoint1.TabIndex = 45;
            this.currentSkillPoint1.Text = "100 / 100";
            this.currentSkillPoint1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.currentSkillPoint1.Visible = false;
            // 
            // enemyNameLabel1
            // 
            this.enemyNameLabel1.AutoSize = true;
            this.enemyNameLabel1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.enemyNameLabel1.Location = new System.Drawing.Point(760, 148);
            this.enemyNameLabel1.Name = "enemyNameLabel1";
            this.enemyNameLabel1.Size = new System.Drawing.Size(46, 16);
            this.enemyNameLabel1.TabIndex = 42;
            this.enemyNameLabel1.Text = "label2";
            this.enemyNameLabel1.Visible = false;
            // 
            // lblLifeEnemy1
            // 
            this.lblLifeEnemy1.AutoSize = true;
            this.lblLifeEnemy1.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblLifeEnemy1.Location = new System.Drawing.Point(800, 170);
            this.lblLifeEnemy1.Name = "lblLifeEnemy1";
            this.lblLifeEnemy1.Size = new System.Drawing.Size(22, 24);
            this.lblLifeEnemy1.TabIndex = 41;
            this.lblLifeEnemy1.Text = "0";
            this.lblLifeEnemy1.Visible = false;
            // 
            // pbEnemyTargetTarget3
            // 
            this.pbEnemyTargetTarget3.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.pbEnemyTargetTarget3.Location = new System.Drawing.Point(927, 668);
            this.pbEnemyTargetTarget3.Name = "pbEnemyTargetTarget3";
            this.pbEnemyTargetTarget3.Size = new System.Drawing.Size(15, 15);
            this.pbEnemyTargetTarget3.TabIndex = 40;
            this.pbEnemyTargetTarget3.TabStop = false;
            this.pbEnemyTargetTarget3.Visible = false;
            // 
            // pbPlayerTargetTarget3
            // 
            this.pbPlayerTargetTarget3.Location = new System.Drawing.Point(908, 667);
            this.pbPlayerTargetTarget3.Name = "pbPlayerTargetTarget3";
            this.pbPlayerTargetTarget3.Size = new System.Drawing.Size(15, 15);
            this.pbPlayerTargetTarget3.TabIndex = 40;
            this.pbPlayerTargetTarget3.TabStop = false;
            this.pbPlayerTargetTarget3.Visible = false;
            // 
            // pbEnemyTargetTarget1
            // 
            this.pbEnemyTargetTarget1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.pbEnemyTargetTarget1.Location = new System.Drawing.Point(891, 667);
            this.pbEnemyTargetTarget1.Name = "pbEnemyTargetTarget1";
            this.pbEnemyTargetTarget1.Size = new System.Drawing.Size(15, 15);
            this.pbEnemyTargetTarget1.TabIndex = 40;
            this.pbEnemyTargetTarget1.TabStop = false;
            this.pbEnemyTargetTarget1.Visible = false;
            // 
            // pbPlayerTargetTarget1
            // 
            this.pbPlayerTargetTarget1.BackColor = System.Drawing.Color.DarkRed;
            this.pbPlayerTargetTarget1.Location = new System.Drawing.Point(872, 667);
            this.pbPlayerTargetTarget1.Name = "pbPlayerTargetTarget1";
            this.pbPlayerTargetTarget1.Size = new System.Drawing.Size(15, 15);
            this.pbPlayerTargetTarget1.TabIndex = 40;
            this.pbPlayerTargetTarget1.TabStop = false;
            this.pbPlayerTargetTarget1.Visible = false;
            // 
            // pbEnemyTargetTarget2
            // 
            this.pbEnemyTargetTarget2.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.pbEnemyTargetTarget2.Location = new System.Drawing.Point(963, 667);
            this.pbEnemyTargetTarget2.Name = "pbEnemyTargetTarget2";
            this.pbEnemyTargetTarget2.Size = new System.Drawing.Size(15, 15);
            this.pbEnemyTargetTarget2.TabIndex = 40;
            this.pbEnemyTargetTarget2.TabStop = false;
            this.pbEnemyTargetTarget2.Visible = false;
            // 
            // pbPlayerTargetTarget2
            // 
            this.pbPlayerTargetTarget2.Location = new System.Drawing.Point(944, 667);
            this.pbPlayerTargetTarget2.Name = "pbPlayerTargetTarget2";
            this.pbPlayerTargetTarget2.Size = new System.Drawing.Size(15, 15);
            this.pbPlayerTargetTarget2.TabIndex = 40;
            this.pbPlayerTargetTarget2.TabStop = false;
            this.pbPlayerTargetTarget2.Visible = false;
            // 
            // buttonTargetPlayer2
            // 
            this.buttonTargetPlayer2.BackColor = System.Drawing.Color.Violet;
            this.buttonTargetPlayer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonTargetPlayer2.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonTargetPlayer2.Location = new System.Drawing.Point(570, 323);
            this.buttonTargetPlayer2.Name = "buttonTargetPlayer2";
            this.buttonTargetPlayer2.Size = new System.Drawing.Size(60, 60);
            this.buttonTargetPlayer2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.buttonTargetPlayer2.TabIndex = 39;
            this.buttonTargetPlayer2.TabStop = false;
            this.buttonTargetPlayer2.Visible = false;
            this.buttonTargetPlayer2.Click += new System.EventHandler(this.buttonTargetPlayer2_Click);
            this.buttonTargetPlayer2.MouseEnter += new System.EventHandler(this.buttonTargetPlayer2_MouseEnter);
            // 
            // playerActionLabel2
            // 
            this.playerActionLabel2.AutoSize = true;
            this.playerActionLabel2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.playerActionLabel2.Location = new System.Drawing.Point(485, 396);
            this.playerActionLabel2.Name = "playerActionLabel2";
            this.playerActionLabel2.Size = new System.Drawing.Size(40, 16);
            this.playerActionLabel2.TabIndex = 38;
            this.playerActionLabel2.Text = "攻撃";
            this.playerActionLabel2.Visible = false;
            // 
            // nameLabel2
            // 
            this.nameLabel2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.nameLabel2.Location = new System.Drawing.Point(479, 303);
            this.nameLabel2.Name = "nameLabel2";
            this.nameLabel2.Size = new System.Drawing.Size(150, 16);
            this.nameLabel2.TabIndex = 32;
            this.nameLabel2.Text = "ラナ";
            this.nameLabel2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.nameLabel2.Visible = false;
            // 
            // lifeLabel2
            // 
            this.lifeLabel2.AutoSize = true;
            this.lifeLabel2.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lifeLabel2.Location = new System.Drawing.Point(500, 326);
            this.lifeLabel2.Name = "lifeLabel2";
            this.lifeLabel2.Size = new System.Drawing.Size(22, 24);
            this.lifeLabel2.TabIndex = 31;
            this.lifeLabel2.Text = "0";
            this.lifeLabel2.Visible = false;
            // 
            // buttonTargetPlayer3
            // 
            this.buttonTargetPlayer3.BackColor = System.Drawing.Color.Yellow;
            this.buttonTargetPlayer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonTargetPlayer3.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonTargetPlayer3.Location = new System.Drawing.Point(570, 481);
            this.buttonTargetPlayer3.Name = "buttonTargetPlayer3";
            this.buttonTargetPlayer3.Size = new System.Drawing.Size(60, 60);
            this.buttonTargetPlayer3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.buttonTargetPlayer3.TabIndex = 29;
            this.buttonTargetPlayer3.TabStop = false;
            this.buttonTargetPlayer3.Visible = false;
            this.buttonTargetPlayer3.Click += new System.EventHandler(this.buttonTargetPlayer3_Click);
            this.buttonTargetPlayer3.MouseEnter += new System.EventHandler(this.buttonTargetPlayer3_MouseEnter);
            // 
            // buttonTargetEnemy2
            // 
            this.buttonTargetEnemy2.BackColor = System.Drawing.Color.DarkGoldenrod;
            this.buttonTargetEnemy2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonTargetEnemy2.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonTargetEnemy2.Location = new System.Drawing.Point(686, 323);
            this.buttonTargetEnemy2.Name = "buttonTargetEnemy2";
            this.buttonTargetEnemy2.Size = new System.Drawing.Size(60, 60);
            this.buttonTargetEnemy2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.buttonTargetEnemy2.TabIndex = 29;
            this.buttonTargetEnemy2.TabStop = false;
            this.buttonTargetEnemy2.Visible = false;
            this.buttonTargetEnemy2.Click += new System.EventHandler(this.buttonTargetEnemy2_Click);
            this.buttonTargetEnemy2.MouseEnter += new System.EventHandler(this.buttonTargetEnemy2_MouseEnter);
            // 
            // buttonTargetEnemy3
            // 
            this.buttonTargetEnemy3.BackColor = System.Drawing.Color.DarkOliveGreen;
            this.buttonTargetEnemy3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonTargetEnemy3.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonTargetEnemy3.Location = new System.Drawing.Point(687, 481);
            this.buttonTargetEnemy3.Name = "buttonTargetEnemy3";
            this.buttonTargetEnemy3.Size = new System.Drawing.Size(60, 60);
            this.buttonTargetEnemy3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.buttonTargetEnemy3.TabIndex = 29;
            this.buttonTargetEnemy3.TabStop = false;
            this.buttonTargetEnemy3.Visible = false;
            this.buttonTargetEnemy3.Click += new System.EventHandler(this.buttonTargetEnemy3_Click);
            this.buttonTargetEnemy3.MouseEnter += new System.EventHandler(this.buttonTargetEnemy3_MouseEnter);
            // 
            // buttonTargetPlayer1
            // 
            this.buttonTargetPlayer1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.buttonTargetPlayer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonTargetPlayer1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonTargetPlayer1.Location = new System.Drawing.Point(570, 165);
            this.buttonTargetPlayer1.Name = "buttonTargetPlayer1";
            this.buttonTargetPlayer1.Size = new System.Drawing.Size(60, 60);
            this.buttonTargetPlayer1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.buttonTargetPlayer1.TabIndex = 29;
            this.buttonTargetPlayer1.TabStop = false;
            this.buttonTargetPlayer1.Visible = false;
            this.buttonTargetPlayer1.Click += new System.EventHandler(this.buttonTargetPlayer1_Click);
            this.buttonTargetPlayer1.MouseEnter += new System.EventHandler(this.buttonTargetPlayer1_MouseEnter);
            // 
            // buttonTargetEnemy1
            // 
            this.buttonTargetEnemy1.BackColor = System.Drawing.Color.DarkRed;
            this.buttonTargetEnemy1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buttonTargetEnemy1.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.buttonTargetEnemy1.Location = new System.Drawing.Point(686, 165);
            this.buttonTargetEnemy1.Name = "buttonTargetEnemy1";
            this.buttonTargetEnemy1.Size = new System.Drawing.Size(60, 60);
            this.buttonTargetEnemy1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.buttonTargetEnemy1.TabIndex = 29;
            this.buttonTargetEnemy1.TabStop = false;
            this.buttonTargetEnemy1.Visible = false;
            this.buttonTargetEnemy1.Click += new System.EventHandler(this.buttonTargetEnemy1_Click);
            this.buttonTargetEnemy1.MouseEnter += new System.EventHandler(this.buttonTargetEnemy1_MouseEnter);
            // 
            // battleSpeedBar
            // 
            this.battleSpeedBar.LargeChange = 1;
            this.battleSpeedBar.Location = new System.Drawing.Point(818, 711);
            this.battleSpeedBar.Maximum = 9;
            this.battleSpeedBar.Minimum = 1;
            this.battleSpeedBar.Name = "battleSpeedBar";
            this.battleSpeedBar.Size = new System.Drawing.Size(194, 45);
            this.battleSpeedBar.TabIndex = 28;
            this.battleSpeedBar.Value = 5;
            this.battleSpeedBar.Scroll += new System.EventHandler(this.battleSpeedBar_Scroll);
            // 
            // TimeSpeedLabel
            // 
            this.TimeSpeedLabel.AutoSize = true;
            this.TimeSpeedLabel.Font = new System.Drawing.Font("MS UI Gothic", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.TimeSpeedLabel.Location = new System.Drawing.Point(816, 685);
            this.TimeSpeedLabel.Name = "TimeSpeedLabel";
            this.TimeSpeedLabel.Size = new System.Drawing.Size(162, 24);
            this.TimeSpeedLabel.TabIndex = 15;
            this.TimeSpeedLabel.Text = "時間速度 x1.0";
            // 
            // playerActionLabel3
            // 
            this.playerActionLabel3.AutoSize = true;
            this.playerActionLabel3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.playerActionLabel3.Location = new System.Drawing.Point(484, 553);
            this.playerActionLabel3.Name = "playerActionLabel3";
            this.playerActionLabel3.Size = new System.Drawing.Size(40, 16);
            this.playerActionLabel3.TabIndex = 13;
            this.playerActionLabel3.Text = "攻撃";
            this.playerActionLabel3.Visible = false;
            // 
            // enemyActionLabel3
            // 
            this.enemyActionLabel3.AutoSize = true;
            this.enemyActionLabel3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.enemyActionLabel3.Location = new System.Drawing.Point(790, 553);
            this.enemyActionLabel3.Name = "enemyActionLabel3";
            this.enemyActionLabel3.Size = new System.Drawing.Size(40, 16);
            this.enemyActionLabel3.TabIndex = 13;
            this.enemyActionLabel3.Text = "攻撃";
            this.enemyActionLabel3.Visible = false;
            // 
            // enemyActionLabel2
            // 
            this.enemyActionLabel2.AutoSize = true;
            this.enemyActionLabel2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.enemyActionLabel2.Location = new System.Drawing.Point(789, 396);
            this.enemyActionLabel2.Name = "enemyActionLabel2";
            this.enemyActionLabel2.Size = new System.Drawing.Size(40, 16);
            this.enemyActionLabel2.TabIndex = 13;
            this.enemyActionLabel2.Text = "攻撃";
            this.enemyActionLabel2.Visible = false;
            // 
            // enemyActionLabel1
            // 
            this.enemyActionLabel1.AutoSize = true;
            this.enemyActionLabel1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.enemyActionLabel1.Location = new System.Drawing.Point(789, 239);
            this.enemyActionLabel1.Name = "enemyActionLabel1";
            this.enemyActionLabel1.Size = new System.Drawing.Size(40, 16);
            this.enemyActionLabel1.TabIndex = 13;
            this.enemyActionLabel1.Text = "攻撃";
            this.enemyActionLabel1.Visible = false;
            // 
            // playerActionLabel1
            // 
            this.playerActionLabel1.AutoSize = true;
            this.playerActionLabel1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.playerActionLabel1.Location = new System.Drawing.Point(483, 239);
            this.playerActionLabel1.Name = "playerActionLabel1";
            this.playerActionLabel1.Size = new System.Drawing.Size(40, 16);
            this.playerActionLabel1.TabIndex = 13;
            this.playerActionLabel1.Text = "攻撃";
            this.playerActionLabel1.Visible = false;
            // 
            // enemyNameLabel3
            // 
            this.enemyNameLabel3.AutoSize = true;
            this.enemyNameLabel3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.enemyNameLabel3.Location = new System.Drawing.Point(761, 462);
            this.enemyNameLabel3.Name = "enemyNameLabel3";
            this.enemyNameLabel3.Size = new System.Drawing.Size(46, 16);
            this.enemyNameLabel3.TabIndex = 10;
            this.enemyNameLabel3.Text = "label2";
            this.enemyNameLabel3.Visible = false;
            // 
            // enemyNameLabel2
            // 
            this.enemyNameLabel2.AutoSize = true;
            this.enemyNameLabel2.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.enemyNameLabel2.Location = new System.Drawing.Point(760, 303);
            this.enemyNameLabel2.Name = "enemyNameLabel2";
            this.enemyNameLabel2.Size = new System.Drawing.Size(46, 16);
            this.enemyNameLabel2.TabIndex = 10;
            this.enemyNameLabel2.Text = "label2";
            this.enemyNameLabel2.Visible = false;
            // 
            // nameLabel3
            // 
            this.nameLabel3.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.nameLabel3.Location = new System.Drawing.Point(480, 462);
            this.nameLabel3.Name = "nameLabel3";
            this.nameLabel3.Size = new System.Drawing.Size(150, 16);
            this.nameLabel3.TabIndex = 9;
            this.nameLabel3.Text = "ランディス";
            this.nameLabel3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.nameLabel3.Visible = false;
            // 
            // nameLabel1
            // 
            this.nameLabel1.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.nameLabel1.Location = new System.Drawing.Point(479, 148);
            this.nameLabel1.Name = "nameLabel1";
            this.nameLabel1.Size = new System.Drawing.Size(150, 16);
            this.nameLabel1.TabIndex = 9;
            this.nameLabel1.Text = "アイン";
            this.nameLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.nameLabel1.Visible = false;
            // 
            // pbBattleTimerBar
            // 
            this.pbBattleTimerBar.Location = new System.Drawing.Point(90, 32);
            this.pbBattleTimerBar.Name = "pbBattleTimerBar";
            this.pbBattleTimerBar.Size = new System.Drawing.Size(910, 10);
            this.pbBattleTimerBar.TabIndex = 5;
            this.pbBattleTimerBar.TabStop = false;
            // 
            // pbPlayer1
            // 
            this.pbPlayer1.Location = new System.Drawing.Point(80, 12);
            this.pbPlayer1.Name = "pbPlayer1";
            this.pbPlayer1.Size = new System.Drawing.Size(932, 50);
            this.pbPlayer1.TabIndex = 1;
            this.pbPlayer1.TabStop = false;
            this.pbPlayer1.Paint += new System.Windows.Forms.PaintEventHandler(this.pbPlayer1_Paint);
            // 
            // lblLifeEnemy3
            // 
            this.lblLifeEnemy3.AutoSize = true;
            this.lblLifeEnemy3.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblLifeEnemy3.Location = new System.Drawing.Point(797, 484);
            this.lblLifeEnemy3.Name = "lblLifeEnemy3";
            this.lblLifeEnemy3.Size = new System.Drawing.Size(22, 24);
            this.lblLifeEnemy3.TabIndex = 4;
            this.lblLifeEnemy3.Text = "0";
            this.lblLifeEnemy3.Visible = false;
            // 
            // lifeLabel3
            // 
            this.lifeLabel3.AutoSize = true;
            this.lifeLabel3.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lifeLabel3.Location = new System.Drawing.Point(500, 484);
            this.lifeLabel3.Name = "lifeLabel3";
            this.lifeLabel3.Size = new System.Drawing.Size(22, 24);
            this.lifeLabel3.TabIndex = 4;
            this.lifeLabel3.Text = "0";
            this.lifeLabel3.Visible = false;
            // 
            // lblLifeEnemy2
            // 
            this.lblLifeEnemy2.AutoSize = true;
            this.lblLifeEnemy2.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblLifeEnemy2.Location = new System.Drawing.Point(796, 326);
            this.lblLifeEnemy2.Name = "lblLifeEnemy2";
            this.lblLifeEnemy2.Size = new System.Drawing.Size(22, 24);
            this.lblLifeEnemy2.TabIndex = 4;
            this.lblLifeEnemy2.Text = "0";
            this.lblLifeEnemy2.Visible = false;
            // 
            // lifeLabel1
            // 
            this.lifeLabel1.AutoSize = true;
            this.lifeLabel1.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lifeLabel1.Location = new System.Drawing.Point(500, 170);
            this.lifeLabel1.Name = "lifeLabel1";
            this.lifeLabel1.Size = new System.Drawing.Size(22, 24);
            this.lifeLabel1.TabIndex = 4;
            this.lifeLabel1.Text = "0";
            this.lifeLabel1.Visible = false;
            // 
            // BattleStart
            // 
            this.BattleStart.BackColor = System.Drawing.Color.AliceBlue;
            this.BattleStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BattleStart.Location = new System.Drawing.Point(10, 673);
            this.BattleStart.Name = "BattleStart";
            this.BattleStart.Size = new System.Drawing.Size(797, 23);
            this.BattleStart.TabIndex = 3;
            this.BattleStart.Text = "戦闘開始！";
            this.BattleStart.UseVisualStyleBackColor = false;
            this.BattleStart.Click += new System.EventHandler(this.BattleStart_Click);
            // 
            // txtBattleMessage
            // 
            this.txtBattleMessage.BackColor = System.Drawing.Color.Lavender;
            this.txtBattleMessage.Location = new System.Drawing.Point(10, 702);
            this.txtBattleMessage.Multiline = true;
            this.txtBattleMessage.Name = "txtBattleMessage";
            this.txtBattleMessage.ReadOnly = true;
            this.txtBattleMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBattleMessage.Size = new System.Drawing.Size(798, 42);
            this.txtBattleMessage.TabIndex = 2;
            this.txtBattleMessage.TabStop = false;
            // 
            // ActionButton29
            // 
            this.ActionButton29.BackColor = System.Drawing.Color.HotPink;
            this.ActionButton29.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton29.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton29.Count = 0;
            this.ActionButton29.Cumulative = 0;
            this.ActionButton29.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton29.ImageName = "";
            this.ActionButton29.Location = new System.Drawing.Point(410, 323);
            this.ActionButton29.Name = "ActionButton29";
            this.ActionButton29.Size = new System.Drawing.Size(50, 50);
            this.ActionButton29.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton29.TabIndex = 40;
            this.ActionButton29.TabStop = false;
            this.ActionButton29.Visible = false;
            this.ActionButton29.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton29_MouseClick);
            // 
            // ActionButton28
            // 
            this.ActionButton28.BackColor = System.Drawing.Color.HotPink;
            this.ActionButton28.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton28.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton28.Count = 0;
            this.ActionButton28.Cumulative = 0;
            this.ActionButton28.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton28.ImageName = "";
            this.ActionButton28.Location = new System.Drawing.Point(360, 323);
            this.ActionButton28.Name = "ActionButton28";
            this.ActionButton28.Size = new System.Drawing.Size(50, 50);
            this.ActionButton28.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton28.TabIndex = 40;
            this.ActionButton28.TabStop = false;
            this.ActionButton28.Visible = false;
            this.ActionButton28.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton28_MouseClick);
            // 
            // ActionButton27
            // 
            this.ActionButton27.BackColor = System.Drawing.Color.HotPink;
            this.ActionButton27.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton27.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton27.Count = 0;
            this.ActionButton27.Cumulative = 0;
            this.ActionButton27.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton27.ImageName = "";
            this.ActionButton27.Location = new System.Drawing.Point(310, 323);
            this.ActionButton27.Name = "ActionButton27";
            this.ActionButton27.Size = new System.Drawing.Size(50, 50);
            this.ActionButton27.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton27.TabIndex = 40;
            this.ActionButton27.TabStop = false;
            this.ActionButton27.Visible = false;
            this.ActionButton27.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton27_MouseClick);
            // 
            // ActionButton26
            // 
            this.ActionButton26.BackColor = System.Drawing.Color.HotPink;
            this.ActionButton26.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton26.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton26.Count = 0;
            this.ActionButton26.Cumulative = 0;
            this.ActionButton26.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton26.ImageName = "";
            this.ActionButton26.Location = new System.Drawing.Point(260, 323);
            this.ActionButton26.Name = "ActionButton26";
            this.ActionButton26.Size = new System.Drawing.Size(50, 50);
            this.ActionButton26.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton26.TabIndex = 40;
            this.ActionButton26.TabStop = false;
            this.ActionButton26.Visible = false;
            this.ActionButton26.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton26_MouseClick);
            // 
            // ActionButton25
            // 
            this.ActionButton25.BackColor = System.Drawing.Color.HotPink;
            this.ActionButton25.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton25.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton25.Count = 0;
            this.ActionButton25.Cumulative = 0;
            this.ActionButton25.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton25.ImageName = "";
            this.ActionButton25.Location = new System.Drawing.Point(210, 323);
            this.ActionButton25.Name = "ActionButton25";
            this.ActionButton25.Size = new System.Drawing.Size(50, 50);
            this.ActionButton25.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton25.TabIndex = 40;
            this.ActionButton25.TabStop = false;
            this.ActionButton25.Visible = false;
            this.ActionButton25.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton25_MouseClick);
            // 
            // ActionButton24
            // 
            this.ActionButton24.BackColor = System.Drawing.Color.HotPink;
            this.ActionButton24.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton24.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton24.Count = 0;
            this.ActionButton24.Cumulative = 0;
            this.ActionButton24.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton24.ImageName = "";
            this.ActionButton24.Location = new System.Drawing.Point(160, 323);
            this.ActionButton24.Name = "ActionButton24";
            this.ActionButton24.Size = new System.Drawing.Size(50, 50);
            this.ActionButton24.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton24.TabIndex = 40;
            this.ActionButton24.TabStop = false;
            this.ActionButton24.Visible = false;
            this.ActionButton24.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton24_MouseClick);
            // 
            // ActionButton23
            // 
            this.ActionButton23.BackColor = System.Drawing.Color.HotPink;
            this.ActionButton23.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton23.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton23.Count = 0;
            this.ActionButton23.Cumulative = 0;
            this.ActionButton23.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton23.ImageName = "";
            this.ActionButton23.Location = new System.Drawing.Point(110, 323);
            this.ActionButton23.Name = "ActionButton23";
            this.ActionButton23.Size = new System.Drawing.Size(50, 50);
            this.ActionButton23.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton23.TabIndex = 40;
            this.ActionButton23.TabStop = false;
            this.ActionButton23.Visible = false;
            this.ActionButton23.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton23_MouseClick);
            // 
            // ActionButton22
            // 
            this.ActionButton22.BackColor = System.Drawing.Color.HotPink;
            this.ActionButton22.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton22.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton22.Count = 0;
            this.ActionButton22.Cumulative = 0;
            this.ActionButton22.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton22.ImageName = "";
            this.ActionButton22.Location = new System.Drawing.Point(60, 323);
            this.ActionButton22.Name = "ActionButton22";
            this.ActionButton22.Size = new System.Drawing.Size(50, 50);
            this.ActionButton22.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton22.TabIndex = 40;
            this.ActionButton22.TabStop = false;
            this.ActionButton22.Visible = false;
            this.ActionButton22.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton22_MouseClick);
            // 
            // ActionButton21
            // 
            this.ActionButton21.BackColor = System.Drawing.Color.HotPink;
            this.ActionButton21.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton21.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton21.Count = 0;
            this.ActionButton21.Cumulative = 0;
            this.ActionButton21.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton21.ImageName = "";
            this.ActionButton21.Location = new System.Drawing.Point(10, 323);
            this.ActionButton21.Name = "ActionButton21";
            this.ActionButton21.Size = new System.Drawing.Size(50, 50);
            this.ActionButton21.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton21.TabIndex = 40;
            this.ActionButton21.TabStop = false;
            this.ActionButton21.Visible = false;
            this.ActionButton21.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton21_MouseClick);
            // 
            // ActionButton39
            // 
            this.ActionButton39.BackColor = System.Drawing.Color.Yellow;
            this.ActionButton39.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton39.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton39.Count = 0;
            this.ActionButton39.Cumulative = 0;
            this.ActionButton39.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton39.ImageName = "";
            this.ActionButton39.Location = new System.Drawing.Point(410, 481);
            this.ActionButton39.Name = "ActionButton39";
            this.ActionButton39.Size = new System.Drawing.Size(50, 50);
            this.ActionButton39.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton39.TabIndex = 40;
            this.ActionButton39.TabStop = false;
            this.ActionButton39.Visible = false;
            this.ActionButton39.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton39_MouseClick);
            // 
            // ActionButton38
            // 
            this.ActionButton38.BackColor = System.Drawing.Color.Yellow;
            this.ActionButton38.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton38.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton38.Count = 0;
            this.ActionButton38.Cumulative = 0;
            this.ActionButton38.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton38.ImageName = "";
            this.ActionButton38.Location = new System.Drawing.Point(360, 481);
            this.ActionButton38.Name = "ActionButton38";
            this.ActionButton38.Size = new System.Drawing.Size(50, 50);
            this.ActionButton38.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton38.TabIndex = 40;
            this.ActionButton38.TabStop = false;
            this.ActionButton38.Visible = false;
            this.ActionButton38.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton38_MouseClick);
            // 
            // ActionButton37
            // 
            this.ActionButton37.BackColor = System.Drawing.Color.Yellow;
            this.ActionButton37.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton37.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton37.Count = 0;
            this.ActionButton37.Cumulative = 0;
            this.ActionButton37.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton37.ImageName = "";
            this.ActionButton37.Location = new System.Drawing.Point(310, 481);
            this.ActionButton37.Name = "ActionButton37";
            this.ActionButton37.Size = new System.Drawing.Size(50, 50);
            this.ActionButton37.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton37.TabIndex = 40;
            this.ActionButton37.TabStop = false;
            this.ActionButton37.Visible = false;
            this.ActionButton37.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton37_MouseClick);
            // 
            // ActionButton36
            // 
            this.ActionButton36.BackColor = System.Drawing.Color.Yellow;
            this.ActionButton36.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton36.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton36.Count = 0;
            this.ActionButton36.Cumulative = 0;
            this.ActionButton36.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton36.ImageName = "";
            this.ActionButton36.Location = new System.Drawing.Point(260, 481);
            this.ActionButton36.Name = "ActionButton36";
            this.ActionButton36.Size = new System.Drawing.Size(50, 50);
            this.ActionButton36.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton36.TabIndex = 40;
            this.ActionButton36.TabStop = false;
            this.ActionButton36.Visible = false;
            this.ActionButton36.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton36_MouseClick);
            // 
            // ActionButton35
            // 
            this.ActionButton35.BackColor = System.Drawing.Color.Yellow;
            this.ActionButton35.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton35.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton35.Count = 0;
            this.ActionButton35.Cumulative = 0;
            this.ActionButton35.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton35.ImageName = "";
            this.ActionButton35.Location = new System.Drawing.Point(210, 481);
            this.ActionButton35.Name = "ActionButton35";
            this.ActionButton35.Size = new System.Drawing.Size(50, 50);
            this.ActionButton35.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton35.TabIndex = 40;
            this.ActionButton35.TabStop = false;
            this.ActionButton35.Visible = false;
            this.ActionButton35.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton35_MouseClick);
            // 
            // ActionButton34
            // 
            this.ActionButton34.BackColor = System.Drawing.Color.Yellow;
            this.ActionButton34.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton34.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton34.Count = 0;
            this.ActionButton34.Cumulative = 0;
            this.ActionButton34.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton34.ImageName = "";
            this.ActionButton34.Location = new System.Drawing.Point(160, 481);
            this.ActionButton34.Name = "ActionButton34";
            this.ActionButton34.Size = new System.Drawing.Size(50, 50);
            this.ActionButton34.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton34.TabIndex = 40;
            this.ActionButton34.TabStop = false;
            this.ActionButton34.Visible = false;
            this.ActionButton34.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton34_MouseClick);
            // 
            // ActionButton33
            // 
            this.ActionButton33.BackColor = System.Drawing.Color.Yellow;
            this.ActionButton33.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton33.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton33.Count = 0;
            this.ActionButton33.Cumulative = 0;
            this.ActionButton33.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton33.ImageName = "";
            this.ActionButton33.Location = new System.Drawing.Point(110, 481);
            this.ActionButton33.Name = "ActionButton33";
            this.ActionButton33.Size = new System.Drawing.Size(50, 50);
            this.ActionButton33.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton33.TabIndex = 40;
            this.ActionButton33.TabStop = false;
            this.ActionButton33.Visible = false;
            this.ActionButton33.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton33_MouseClick);
            // 
            // ActionButton32
            // 
            this.ActionButton32.BackColor = System.Drawing.Color.Yellow;
            this.ActionButton32.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton32.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton32.Count = 0;
            this.ActionButton32.Cumulative = 0;
            this.ActionButton32.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton32.ImageName = "";
            this.ActionButton32.Location = new System.Drawing.Point(60, 481);
            this.ActionButton32.Name = "ActionButton32";
            this.ActionButton32.Size = new System.Drawing.Size(50, 50);
            this.ActionButton32.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton32.TabIndex = 40;
            this.ActionButton32.TabStop = false;
            this.ActionButton32.Visible = false;
            this.ActionButton32.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton32_MouseClick);
            // 
            // ActionButton31
            // 
            this.ActionButton31.BackColor = System.Drawing.Color.Yellow;
            this.ActionButton31.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton31.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton31.Count = 0;
            this.ActionButton31.Cumulative = 0;
            this.ActionButton31.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton31.ImageName = "";
            this.ActionButton31.Location = new System.Drawing.Point(10, 481);
            this.ActionButton31.Name = "ActionButton31";
            this.ActionButton31.Size = new System.Drawing.Size(50, 50);
            this.ActionButton31.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton31.TabIndex = 40;
            this.ActionButton31.TabStop = false;
            this.ActionButton31.Visible = false;
            this.ActionButton31.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton31_MouseClick);
            // 
            // ActionButton19
            // 
            this.ActionButton19.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ActionButton19.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton19.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton19.Count = 0;
            this.ActionButton19.Cumulative = 0;
            this.ActionButton19.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton19.ImageName = "";
            this.ActionButton19.Location = new System.Drawing.Point(411, 165);
            this.ActionButton19.Name = "ActionButton19";
            this.ActionButton19.Size = new System.Drawing.Size(50, 50);
            this.ActionButton19.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton19.TabIndex = 40;
            this.ActionButton19.TabStop = false;
            this.ActionButton19.Visible = false;
            this.ActionButton19.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton19_MouseClick);
            // 
            // ActionButton18
            // 
            this.ActionButton18.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ActionButton18.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton18.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton18.Count = 0;
            this.ActionButton18.Cumulative = 0;
            this.ActionButton18.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton18.ImageName = "";
            this.ActionButton18.Location = new System.Drawing.Point(360, 165);
            this.ActionButton18.Name = "ActionButton18";
            this.ActionButton18.Size = new System.Drawing.Size(50, 50);
            this.ActionButton18.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton18.TabIndex = 40;
            this.ActionButton18.TabStop = false;
            this.ActionButton18.Visible = false;
            this.ActionButton18.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton18_MouseClick);
            // 
            // ActionButton17
            // 
            this.ActionButton17.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ActionButton17.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton17.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton17.Count = 0;
            this.ActionButton17.Cumulative = 0;
            this.ActionButton17.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton17.ImageName = "";
            this.ActionButton17.Location = new System.Drawing.Point(310, 165);
            this.ActionButton17.Name = "ActionButton17";
            this.ActionButton17.Size = new System.Drawing.Size(50, 50);
            this.ActionButton17.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton17.TabIndex = 40;
            this.ActionButton17.TabStop = false;
            this.ActionButton17.Visible = false;
            this.ActionButton17.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton17_MouseClick);
            // 
            // ActionButton16
            // 
            this.ActionButton16.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ActionButton16.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton16.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton16.Count = 0;
            this.ActionButton16.Cumulative = 0;
            this.ActionButton16.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton16.ImageName = "";
            this.ActionButton16.Location = new System.Drawing.Point(260, 165);
            this.ActionButton16.Name = "ActionButton16";
            this.ActionButton16.Size = new System.Drawing.Size(50, 50);
            this.ActionButton16.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton16.TabIndex = 40;
            this.ActionButton16.TabStop = false;
            this.ActionButton16.Visible = false;
            this.ActionButton16.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton16_MouseClick);
            // 
            // ActionButton15
            // 
            this.ActionButton15.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ActionButton15.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton15.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton15.Count = 0;
            this.ActionButton15.Cumulative = 0;
            this.ActionButton15.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton15.ImageName = "";
            this.ActionButton15.Location = new System.Drawing.Point(210, 165);
            this.ActionButton15.Name = "ActionButton15";
            this.ActionButton15.Size = new System.Drawing.Size(50, 50);
            this.ActionButton15.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton15.TabIndex = 40;
            this.ActionButton15.TabStop = false;
            this.ActionButton15.Visible = false;
            this.ActionButton15.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton15_MouseClick);
            // 
            // ActionButton14
            // 
            this.ActionButton14.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ActionButton14.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton14.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton14.Count = 0;
            this.ActionButton14.Cumulative = 0;
            this.ActionButton14.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton14.ImageName = "";
            this.ActionButton14.Location = new System.Drawing.Point(160, 165);
            this.ActionButton14.Name = "ActionButton14";
            this.ActionButton14.Size = new System.Drawing.Size(50, 50);
            this.ActionButton14.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton14.TabIndex = 40;
            this.ActionButton14.TabStop = false;
            this.ActionButton14.Visible = false;
            this.ActionButton14.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton14_MouseClick);
            // 
            // ActionButton13
            // 
            this.ActionButton13.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ActionButton13.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton13.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton13.Count = 0;
            this.ActionButton13.Cumulative = 0;
            this.ActionButton13.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton13.ImageName = "";
            this.ActionButton13.Location = new System.Drawing.Point(110, 165);
            this.ActionButton13.Name = "ActionButton13";
            this.ActionButton13.Size = new System.Drawing.Size(50, 50);
            this.ActionButton13.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton13.TabIndex = 40;
            this.ActionButton13.TabStop = false;
            this.ActionButton13.Visible = false;
            this.ActionButton13.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton13_MouseClick);
            // 
            // ActionButton12
            // 
            this.ActionButton12.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ActionButton12.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton12.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton12.Count = 0;
            this.ActionButton12.Cumulative = 0;
            this.ActionButton12.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton12.ImageName = "";
            this.ActionButton12.Location = new System.Drawing.Point(60, 165);
            this.ActionButton12.Name = "ActionButton12";
            this.ActionButton12.Size = new System.Drawing.Size(50, 50);
            this.ActionButton12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton12.TabIndex = 40;
            this.ActionButton12.TabStop = false;
            this.ActionButton12.Visible = false;
            this.ActionButton12.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton12_MouseClick);
            // 
            // ActionButton11
            // 
            this.ActionButton11.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ActionButton11.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ActionButton11.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.ActionButton11.Count = 0;
            this.ActionButton11.Cumulative = 0;
            this.ActionButton11.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.ActionButton11.ImageName = "";
            this.ActionButton11.Location = new System.Drawing.Point(10, 165);
            this.ActionButton11.Name = "ActionButton11";
            this.ActionButton11.Size = new System.Drawing.Size(50, 50);
            this.ActionButton11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ActionButton11.TabIndex = 40;
            this.ActionButton11.TabStop = false;
            this.ActionButton11.Visible = false;
            this.ActionButton11.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ActionButton11_MouseClick);
            // 
            // IsSorcery11
            // 
            this.IsSorcery11.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery11.Count = 0;
            this.IsSorcery11.Cumulative = 0;
            this.IsSorcery11.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery11.ImageName = "";
            this.IsSorcery11.Location = new System.Drawing.Point(32, 145);
            this.IsSorcery11.Name = "IsSorcery11";
            this.IsSorcery11.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery11.TabIndex = 63;
            this.IsSorcery11.TabStop = false;
            // 
            // IsSorcery12
            // 
            this.IsSorcery12.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery12.Count = 0;
            this.IsSorcery12.Cumulative = 0;
            this.IsSorcery12.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery12.ImageName = "";
            this.IsSorcery12.Location = new System.Drawing.Point(82, 145);
            this.IsSorcery12.Name = "IsSorcery12";
            this.IsSorcery12.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery12.TabIndex = 63;
            this.IsSorcery12.TabStop = false;
            // 
            // IsSorcery13
            // 
            this.IsSorcery13.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery13.Count = 0;
            this.IsSorcery13.Cumulative = 0;
            this.IsSorcery13.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery13.ImageName = "";
            this.IsSorcery13.Location = new System.Drawing.Point(132, 145);
            this.IsSorcery13.Name = "IsSorcery13";
            this.IsSorcery13.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery13.TabIndex = 63;
            this.IsSorcery13.TabStop = false;
            // 
            // IsSorcery14
            // 
            this.IsSorcery14.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery14.Count = 0;
            this.IsSorcery14.Cumulative = 0;
            this.IsSorcery14.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery14.ImageName = "";
            this.IsSorcery14.Location = new System.Drawing.Point(182, 145);
            this.IsSorcery14.Name = "IsSorcery14";
            this.IsSorcery14.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery14.TabIndex = 63;
            this.IsSorcery14.TabStop = false;
            // 
            // IsSorcery15
            // 
            this.IsSorcery15.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery15.Count = 0;
            this.IsSorcery15.Cumulative = 0;
            this.IsSorcery15.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery15.ImageName = "";
            this.IsSorcery15.Location = new System.Drawing.Point(232, 145);
            this.IsSorcery15.Name = "IsSorcery15";
            this.IsSorcery15.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery15.TabIndex = 63;
            this.IsSorcery15.TabStop = false;
            // 
            // IsSorcery16
            // 
            this.IsSorcery16.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery16.Count = 0;
            this.IsSorcery16.Cumulative = 0;
            this.IsSorcery16.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery16.ImageName = "";
            this.IsSorcery16.Location = new System.Drawing.Point(282, 145);
            this.IsSorcery16.Name = "IsSorcery16";
            this.IsSorcery16.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery16.TabIndex = 63;
            this.IsSorcery16.TabStop = false;
            // 
            // IsSorcery17
            // 
            this.IsSorcery17.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery17.Count = 0;
            this.IsSorcery17.Cumulative = 0;
            this.IsSorcery17.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery17.ImageName = "";
            this.IsSorcery17.Location = new System.Drawing.Point(332, 145);
            this.IsSorcery17.Name = "IsSorcery17";
            this.IsSorcery17.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery17.TabIndex = 63;
            this.IsSorcery17.TabStop = false;
            // 
            // IsSorcery18
            // 
            this.IsSorcery18.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery18.Count = 0;
            this.IsSorcery18.Cumulative = 0;
            this.IsSorcery18.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery18.ImageName = "";
            this.IsSorcery18.Location = new System.Drawing.Point(382, 145);
            this.IsSorcery18.Name = "IsSorcery18";
            this.IsSorcery18.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery18.TabIndex = 63;
            this.IsSorcery18.TabStop = false;
            // 
            // IsSorcery19
            // 
            this.IsSorcery19.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery19.Count = 0;
            this.IsSorcery19.Cumulative = 0;
            this.IsSorcery19.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery19.ImageName = "";
            this.IsSorcery19.Location = new System.Drawing.Point(432, 145);
            this.IsSorcery19.Name = "IsSorcery19";
            this.IsSorcery19.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery19.TabIndex = 63;
            this.IsSorcery19.TabStop = false;
            // 
            // IsSorcery21
            // 
            this.IsSorcery21.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery21.Count = 0;
            this.IsSorcery21.Cumulative = 0;
            this.IsSorcery21.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery21.ImageName = "";
            this.IsSorcery21.Location = new System.Drawing.Point(35, 303);
            this.IsSorcery21.Name = "IsSorcery21";
            this.IsSorcery21.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery21.TabIndex = 63;
            this.IsSorcery21.TabStop = false;
            // 
            // IsSorcery22
            // 
            this.IsSorcery22.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery22.Count = 0;
            this.IsSorcery22.Cumulative = 0;
            this.IsSorcery22.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery22.ImageName = "";
            this.IsSorcery22.Location = new System.Drawing.Point(85, 303);
            this.IsSorcery22.Name = "IsSorcery22";
            this.IsSorcery22.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery22.TabIndex = 63;
            this.IsSorcery22.TabStop = false;
            // 
            // IsSorcery23
            // 
            this.IsSorcery23.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery23.Count = 0;
            this.IsSorcery23.Cumulative = 0;
            this.IsSorcery23.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery23.ImageName = "";
            this.IsSorcery23.Location = new System.Drawing.Point(135, 303);
            this.IsSorcery23.Name = "IsSorcery23";
            this.IsSorcery23.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery23.TabIndex = 63;
            this.IsSorcery23.TabStop = false;
            // 
            // IsSorcery24
            // 
            this.IsSorcery24.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery24.Count = 0;
            this.IsSorcery24.Cumulative = 0;
            this.IsSorcery24.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery24.ImageName = "";
            this.IsSorcery24.Location = new System.Drawing.Point(185, 303);
            this.IsSorcery24.Name = "IsSorcery24";
            this.IsSorcery24.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery24.TabIndex = 63;
            this.IsSorcery24.TabStop = false;
            // 
            // IsSorcery25
            // 
            this.IsSorcery25.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery25.Count = 0;
            this.IsSorcery25.Cumulative = 0;
            this.IsSorcery25.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery25.ImageName = "";
            this.IsSorcery25.Location = new System.Drawing.Point(235, 303);
            this.IsSorcery25.Name = "IsSorcery25";
            this.IsSorcery25.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery25.TabIndex = 63;
            this.IsSorcery25.TabStop = false;
            // 
            // IsSorcery26
            // 
            this.IsSorcery26.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery26.Count = 0;
            this.IsSorcery26.Cumulative = 0;
            this.IsSorcery26.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery26.ImageName = "";
            this.IsSorcery26.Location = new System.Drawing.Point(285, 303);
            this.IsSorcery26.Name = "IsSorcery26";
            this.IsSorcery26.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery26.TabIndex = 63;
            this.IsSorcery26.TabStop = false;
            // 
            // IsSorcery27
            // 
            this.IsSorcery27.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery27.Count = 0;
            this.IsSorcery27.Cumulative = 0;
            this.IsSorcery27.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery27.ImageName = "";
            this.IsSorcery27.Location = new System.Drawing.Point(335, 303);
            this.IsSorcery27.Name = "IsSorcery27";
            this.IsSorcery27.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery27.TabIndex = 63;
            this.IsSorcery27.TabStop = false;
            // 
            // IsSorcery28
            // 
            this.IsSorcery28.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery28.Count = 0;
            this.IsSorcery28.Cumulative = 0;
            this.IsSorcery28.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery28.ImageName = "";
            this.IsSorcery28.Location = new System.Drawing.Point(385, 303);
            this.IsSorcery28.Name = "IsSorcery28";
            this.IsSorcery28.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery28.TabIndex = 63;
            this.IsSorcery28.TabStop = false;
            // 
            // IsSorcery29
            // 
            this.IsSorcery29.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery29.Count = 0;
            this.IsSorcery29.Cumulative = 0;
            this.IsSorcery29.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery29.ImageName = "";
            this.IsSorcery29.Location = new System.Drawing.Point(435, 303);
            this.IsSorcery29.Name = "IsSorcery29";
            this.IsSorcery29.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery29.TabIndex = 63;
            this.IsSorcery29.TabStop = false;
            // 
            // IsSorcery31
            // 
            this.IsSorcery31.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery31.Count = 0;
            this.IsSorcery31.Cumulative = 0;
            this.IsSorcery31.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery31.ImageName = "";
            this.IsSorcery31.Location = new System.Drawing.Point(35, 461);
            this.IsSorcery31.Name = "IsSorcery31";
            this.IsSorcery31.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery31.TabIndex = 63;
            this.IsSorcery31.TabStop = false;
            // 
            // IsSorcery32
            // 
            this.IsSorcery32.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery32.Count = 0;
            this.IsSorcery32.Cumulative = 0;
            this.IsSorcery32.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery32.ImageName = "";
            this.IsSorcery32.Location = new System.Drawing.Point(85, 461);
            this.IsSorcery32.Name = "IsSorcery32";
            this.IsSorcery32.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery32.TabIndex = 63;
            this.IsSorcery32.TabStop = false;
            // 
            // IsSorcery33
            // 
            this.IsSorcery33.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery33.Count = 0;
            this.IsSorcery33.Cumulative = 0;
            this.IsSorcery33.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery33.ImageName = "";
            this.IsSorcery33.Location = new System.Drawing.Point(135, 461);
            this.IsSorcery33.Name = "IsSorcery33";
            this.IsSorcery33.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery33.TabIndex = 63;
            this.IsSorcery33.TabStop = false;
            // 
            // IsSorcery34
            // 
            this.IsSorcery34.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery34.Count = 0;
            this.IsSorcery34.Cumulative = 0;
            this.IsSorcery34.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery34.ImageName = "";
            this.IsSorcery34.Location = new System.Drawing.Point(185, 461);
            this.IsSorcery34.Name = "IsSorcery34";
            this.IsSorcery34.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery34.TabIndex = 63;
            this.IsSorcery34.TabStop = false;
            // 
            // IsSorcery35
            // 
            this.IsSorcery35.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery35.Count = 0;
            this.IsSorcery35.Cumulative = 0;
            this.IsSorcery35.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery35.ImageName = "";
            this.IsSorcery35.Location = new System.Drawing.Point(235, 461);
            this.IsSorcery35.Name = "IsSorcery35";
            this.IsSorcery35.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery35.TabIndex = 63;
            this.IsSorcery35.TabStop = false;
            // 
            // IsSorcery36
            // 
            this.IsSorcery36.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery36.Count = 0;
            this.IsSorcery36.Cumulative = 0;
            this.IsSorcery36.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery36.ImageName = "";
            this.IsSorcery36.Location = new System.Drawing.Point(285, 461);
            this.IsSorcery36.Name = "IsSorcery36";
            this.IsSorcery36.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery36.TabIndex = 63;
            this.IsSorcery36.TabStop = false;
            // 
            // IsSorcery37
            // 
            this.IsSorcery37.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery37.Count = 0;
            this.IsSorcery37.Cumulative = 0;
            this.IsSorcery37.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery37.ImageName = "";
            this.IsSorcery37.Location = new System.Drawing.Point(335, 461);
            this.IsSorcery37.Name = "IsSorcery37";
            this.IsSorcery37.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery37.TabIndex = 63;
            this.IsSorcery37.TabStop = false;
            // 
            // IsSorcery38
            // 
            this.IsSorcery38.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery38.Count = 0;
            this.IsSorcery38.Cumulative = 0;
            this.IsSorcery38.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery38.ImageName = "";
            this.IsSorcery38.Location = new System.Drawing.Point(385, 461);
            this.IsSorcery38.Name = "IsSorcery38";
            this.IsSorcery38.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery38.TabIndex = 63;
            this.IsSorcery38.TabStop = false;
            // 
            // IsSorcery39
            // 
            this.IsSorcery39.BuffMode = DungeonPlayer.TruthImage.buffType.None;
            this.IsSorcery39.Count = 0;
            this.IsSorcery39.Cumulative = 0;
            this.IsSorcery39.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.IsSorcery39.ImageName = "";
            this.IsSorcery39.Location = new System.Drawing.Point(435, 461);
            this.IsSorcery39.Name = "IsSorcery39";
            this.IsSorcery39.Size = new System.Drawing.Size(20, 20);
            this.IsSorcery39.TabIndex = 63;
            this.IsSorcery39.TabStop = false;
            // 
            // FieldBuff1
            // 
            this.FieldBuff1.BuffMode = DungeonPlayer.TruthImage.buffType.Large;
            this.FieldBuff1.Count = 0;
            this.FieldBuff1.Cumulative = 0;
            this.FieldBuff1.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.FieldBuff1.ImageName = "時間律【憎業】";
            this.FieldBuff1.Location = new System.Drawing.Point(467, 68);
            this.FieldBuff1.Name = "FieldBuff1";
            this.FieldBuff1.Size = new System.Drawing.Size(50, 65);
            this.FieldBuff1.TabIndex = 64;
            this.FieldBuff1.TabStop = false;
            this.FieldBuff1.MouseEnter += new System.EventHandler(this.FieldBuff_MouseEnter);
            this.FieldBuff1.MouseLeave += new System.EventHandler(this.FieldBuff_MouseLeave);
            this.FieldBuff1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FieldBuff_MouseMove);
            // 
            // FieldBuff2
            // 
            this.FieldBuff2.BuffMode = DungeonPlayer.TruthImage.buffType.Large;
            this.FieldBuff2.Count = 0;
            this.FieldBuff2.Cumulative = 0;
            this.FieldBuff2.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.FieldBuff2.ImageName = "時間律【零空】";
            this.FieldBuff2.Location = new System.Drawing.Point(523, 68);
            this.FieldBuff2.Name = "FieldBuff2";
            this.FieldBuff2.Size = new System.Drawing.Size(50, 65);
            this.FieldBuff2.TabIndex = 64;
            this.FieldBuff2.TabStop = false;
            this.FieldBuff2.MouseEnter += new System.EventHandler(this.FieldBuff_MouseEnter);
            this.FieldBuff2.MouseLeave += new System.EventHandler(this.FieldBuff_MouseLeave);
            this.FieldBuff2.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FieldBuff_MouseMove);
            // 
            // FieldBuff3
            // 
            this.FieldBuff3.BuffMode = DungeonPlayer.TruthImage.buffType.Large;
            this.FieldBuff3.Count = 0;
            this.FieldBuff3.Cumulative = 0;
            this.FieldBuff3.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.FieldBuff3.ImageName = "時間律【盛栄】";
            this.FieldBuff3.Location = new System.Drawing.Point(579, 68);
            this.FieldBuff3.Name = "FieldBuff3";
            this.FieldBuff3.Size = new System.Drawing.Size(50, 65);
            this.FieldBuff3.TabIndex = 64;
            this.FieldBuff3.TabStop = false;
            this.FieldBuff3.MouseEnter += new System.EventHandler(this.FieldBuff_MouseEnter);
            this.FieldBuff3.MouseLeave += new System.EventHandler(this.FieldBuff_MouseLeave);
            this.FieldBuff3.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FieldBuff_MouseMove);
            // 
            // FieldBuff4
            // 
            this.FieldBuff4.BuffMode = DungeonPlayer.TruthImage.buffType.Large;
            this.FieldBuff4.Count = 0;
            this.FieldBuff4.Cumulative = 0;
            this.FieldBuff4.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.FieldBuff4.ImageName = "時間律【絶剣】";
            this.FieldBuff4.Location = new System.Drawing.Point(635, 68);
            this.FieldBuff4.Name = "FieldBuff4";
            this.FieldBuff4.Size = new System.Drawing.Size(50, 65);
            this.FieldBuff4.TabIndex = 64;
            this.FieldBuff4.TabStop = false;
            this.FieldBuff4.MouseEnter += new System.EventHandler(this.FieldBuff_MouseEnter);
            this.FieldBuff4.MouseLeave += new System.EventHandler(this.FieldBuff_MouseLeave);
            this.FieldBuff4.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FieldBuff_MouseMove);
            // 
            // FieldBuff5
            // 
            this.FieldBuff5.BuffMode = DungeonPlayer.TruthImage.buffType.Large;
            this.FieldBuff5.Count = 0;
            this.FieldBuff5.Cumulative = 0;
            this.FieldBuff5.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.FieldBuff5.ImageName = "時間律【緑永】";
            this.FieldBuff5.Location = new System.Drawing.Point(691, 68);
            this.FieldBuff5.Name = "FieldBuff5";
            this.FieldBuff5.Size = new System.Drawing.Size(50, 65);
            this.FieldBuff5.TabIndex = 64;
            this.FieldBuff5.TabStop = false;
            this.FieldBuff5.MouseEnter += new System.EventHandler(this.FieldBuff_MouseEnter);
            this.FieldBuff5.MouseLeave += new System.EventHandler(this.FieldBuff_MouseLeave);
            this.FieldBuff5.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FieldBuff_MouseMove);
            // 
            // FieldBuff6
            // 
            this.FieldBuff6.BuffMode = DungeonPlayer.TruthImage.buffType.Large;
            this.FieldBuff6.Count = 0;
            this.FieldBuff6.Cumulative = 0;
            this.FieldBuff6.CumulativeAlign = DungeonPlayer.TruthImage.CumulativeTextAlign.TopRight;
            this.FieldBuff6.ImageName = "完全絶対時間律【終焉】";
            this.FieldBuff6.Location = new System.Drawing.Point(747, 68);
            this.FieldBuff6.Name = "FieldBuff6";
            this.FieldBuff6.Size = new System.Drawing.Size(50, 65);
            this.FieldBuff6.TabIndex = 64;
            this.FieldBuff6.TabStop = false;
            this.FieldBuff6.MouseEnter += new System.EventHandler(this.FieldBuff_MouseEnter);
            this.FieldBuff6.MouseLeave += new System.EventHandler(this.FieldBuff_MouseLeave);
            this.FieldBuff6.MouseMove += new System.Windows.Forms.MouseEventHandler(this.FieldBuff_MouseMove);
            // 
            // pbSandglass
            // 
            this.pbSandglass.Location = new System.Drawing.Point(17, 32);
            this.pbSandglass.Name = "pbSandglass";
            this.pbSandglass.Size = new System.Drawing.Size(38, 53);
            this.pbSandglass.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbSandglass.TabIndex = 65;
            this.pbSandglass.TabStop = false;
            // 
            // lblTimerCount
            // 
            this.lblTimerCount.AutoSize = true;
            this.lblTimerCount.Font = new System.Drawing.Font("MS UI Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lblTimerCount.Location = new System.Drawing.Point(20, 88);
            this.lblTimerCount.Name = "lblTimerCount";
            this.lblTimerCount.Size = new System.Drawing.Size(35, 13);
            this.lblTimerCount.TabIndex = 66;
            this.lblTimerCount.Text = "3.00";
            // 
            // pbAnimeSandGlass
            // 
            this.pbAnimeSandGlass.Location = new System.Drawing.Point(482, 711);
            this.pbAnimeSandGlass.Name = "pbAnimeSandGlass";
            this.pbAnimeSandGlass.Size = new System.Drawing.Size(90, 90);
            this.pbAnimeSandGlass.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbAnimeSandGlass.TabIndex = 67;
            this.pbAnimeSandGlass.TabStop = false;
            this.pbAnimeSandGlass.Visible = false;
            // 
            // labelSpecialInstant
            // 
            this.labelSpecialInstant.BackColor = System.Drawing.Color.DarkViolet;
            this.labelSpecialInstant.Font = new System.Drawing.Font("MS UI Gothic", 12F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.labelSpecialInstant.ForeColor = System.Drawing.Color.White;
            this.labelSpecialInstant.Location = new System.Drawing.Point(837, 463);
            this.labelSpecialInstant.Name = "labelSpecialInstant";
            this.labelSpecialInstant.Size = new System.Drawing.Size(175, 15);
            this.labelSpecialInstant.TabIndex = 68;
            this.labelSpecialInstant.Text = "300 / 300";
            this.labelSpecialInstant.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelSpecialInstant.Visible = false;
            // 
            // TruthBattleEnemy
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.labelSpecialInstant);
            this.Controls.Add(this.pbAnimeSandGlass);
            this.Controls.Add(this.lblTimerCount);
            this.Controls.Add(this.pbSandglass);
            this.Controls.Add(this.FieldBuff6);
            this.Controls.Add(this.FieldBuff5);
            this.Controls.Add(this.FieldBuff3);
            this.Controls.Add(this.FieldBuff4);
            this.Controls.Add(this.FieldBuff2);
            this.Controls.Add(this.FieldBuff1);
            this.Controls.Add(this.IsSorcery39);
            this.Controls.Add(this.IsSorcery29);
            this.Controls.Add(this.IsSorcery19);
            this.Controls.Add(this.IsSorcery38);
            this.Controls.Add(this.IsSorcery28);
            this.Controls.Add(this.IsSorcery18);
            this.Controls.Add(this.IsSorcery37);
            this.Controls.Add(this.IsSorcery27);
            this.Controls.Add(this.IsSorcery17);
            this.Controls.Add(this.IsSorcery36);
            this.Controls.Add(this.IsSorcery26);
            this.Controls.Add(this.IsSorcery16);
            this.Controls.Add(this.IsSorcery35);
            this.Controls.Add(this.IsSorcery25);
            this.Controls.Add(this.IsSorcery15);
            this.Controls.Add(this.IsSorcery34);
            this.Controls.Add(this.IsSorcery24);
            this.Controls.Add(this.IsSorcery33);
            this.Controls.Add(this.IsSorcery14);
            this.Controls.Add(this.IsSorcery23);
            this.Controls.Add(this.IsSorcery32);
            this.Controls.Add(this.IsSorcery13);
            this.Controls.Add(this.IsSorcery22);
            this.Controls.Add(this.IsSorcery31);
            this.Controls.Add(this.IsSorcery12);
            this.Controls.Add(this.IsSorcery21);
            this.Controls.Add(this.IsSorcery11);
            this.Controls.Add(this.MatrixDragonTalk);
            this.Controls.Add(this.pbMatrixDragon);
            this.Controls.Add(this.keyNum3_9);
            this.Controls.Add(this.keyNum3_8);
            this.Controls.Add(this.keyNum3_7);
            this.Controls.Add(this.keyNum2_9);
            this.Controls.Add(this.keyNum2_8);
            this.Controls.Add(this.keyNum2_7);
            this.Controls.Add(this.keyNum3_6);
            this.Controls.Add(this.keyNum2_6);
            this.Controls.Add(this.keyNum1_9);
            this.Controls.Add(this.keyNum1_8);
            this.Controls.Add(this.keyNum1_7);
            this.Controls.Add(this.keyNum3_5);
            this.Controls.Add(this.keyNum2_5);
            this.Controls.Add(this.keyNum1_6);
            this.Controls.Add(this.keyNum3_4);
            this.Controls.Add(this.keyNum2_4);
            this.Controls.Add(this.keyNum1_5);
            this.Controls.Add(this.keyNum3_3);
            this.Controls.Add(this.keyNum2_3);
            this.Controls.Add(this.keyNum1_4);
            this.Controls.Add(this.keyNum3_2);
            this.Controls.Add(this.keyNum2_2);
            this.Controls.Add(this.keyNum1_3);
            this.Controls.Add(this.keyNum3_1);
            this.Controls.Add(this.keyNum2_1);
            this.Controls.Add(this.keyNum1_2);
            this.Controls.Add(this.keyNum1_1);
            this.Controls.Add(this.BattleMenuPanel);
            this.Controls.Add(this.StackNameLabel);
            this.Controls.Add(this.StackInTheCommandLabel);
            this.Controls.Add(this.UseItemGauge);
            this.Controls.Add(this.labelDamage2);
            this.Controls.Add(this.labelDamage3);
            this.Controls.Add(this.labelCritical3);
            this.Controls.Add(this.labelCritical2);
            this.Controls.Add(this.labelEnemyCritical2);
            this.Controls.Add(this.labelEnemyCritical3);
            this.Controls.Add(this.labelEnemyCritical1);
            this.Controls.Add(this.labelCritical1);
            this.Controls.Add(this.labelDamage1);
            this.Controls.Add(this.labelEnemyDamage3);
            this.Controls.Add(this.labelEnemyDamage2);
            this.Controls.Add(this.labelEnemyDamage1);
            this.Controls.Add(this.BuffPanel2);
            this.Controls.Add(this.BuffPanel3);
            this.Controls.Add(this.PanelBuffEnemy3);
            this.Controls.Add(this.PanelBuffEnemy2);
            this.Controls.Add(this.PanelBuffEnemy1);
            this.Controls.Add(this.BuffPanel1);
            this.Controls.Add(this.labelBattleTurn);
            this.Controls.Add(this.currentManaPoint3);
            this.Controls.Add(this.currentSkillPoint3);
            this.Controls.Add(this.currentManaPoint2);
            this.Controls.Add(this.currentSkillPoint2);
            this.Controls.Add(this.currentEnemyManaPoint1);
            this.Controls.Add(this.currentEnemyInstantPoint3);
            this.Controls.Add(this.currentEnemyInstantPoint2);
            this.Controls.Add(this.currentEnemyInstantPoint1);
            this.Controls.Add(this.currentInstantPoint3);
            this.Controls.Add(this.currentInstantPoint2);
            this.Controls.Add(this.currentInstantPoint1);
            this.Controls.Add(this.currentManaPoint1);
            this.Controls.Add(this.currentEnemySkillPoint1);
            this.Controls.Add(this.currentSkillPoint1);
            this.Controls.Add(this.enemyNameLabel1);
            this.Controls.Add(this.lblLifeEnemy1);
            this.Controls.Add(this.pbEnemyTargetTarget3);
            this.Controls.Add(this.ActionButton29);
            this.Controls.Add(this.ActionButton28);
            this.Controls.Add(this.ActionButton27);
            this.Controls.Add(this.ActionButton26);
            this.Controls.Add(this.ActionButton25);
            this.Controls.Add(this.ActionButton24);
            this.Controls.Add(this.ActionButton23);
            this.Controls.Add(this.ActionButton22);
            this.Controls.Add(this.ActionButton21);
            this.Controls.Add(this.ActionButton39);
            this.Controls.Add(this.ActionButton38);
            this.Controls.Add(this.ActionButton37);
            this.Controls.Add(this.ActionButton36);
            this.Controls.Add(this.ActionButton35);
            this.Controls.Add(this.ActionButton34);
            this.Controls.Add(this.ActionButton33);
            this.Controls.Add(this.ActionButton32);
            this.Controls.Add(this.ActionButton31);
            this.Controls.Add(this.ActionButton19);
            this.Controls.Add(this.ActionButton18);
            this.Controls.Add(this.ActionButton17);
            this.Controls.Add(this.ActionButton16);
            this.Controls.Add(this.ActionButton15);
            this.Controls.Add(this.ActionButton14);
            this.Controls.Add(this.ActionButton13);
            this.Controls.Add(this.ActionButton12);
            this.Controls.Add(this.ActionButton11);
            this.Controls.Add(this.pbPlayerTargetTarget3);
            this.Controls.Add(this.pbEnemyTargetTarget1);
            this.Controls.Add(this.pbPlayerTargetTarget1);
            this.Controls.Add(this.pbEnemyTargetTarget2);
            this.Controls.Add(this.pbPlayerTargetTarget2);
            this.Controls.Add(this.buttonTargetPlayer2);
            this.Controls.Add(this.playerActionLabel2);
            this.Controls.Add(this.nameLabel2);
            this.Controls.Add(this.lifeLabel2);
            this.Controls.Add(this.buttonTargetPlayer3);
            this.Controls.Add(this.buttonTargetEnemy2);
            this.Controls.Add(this.buttonTargetEnemy3);
            this.Controls.Add(this.buttonTargetPlayer1);
            this.Controls.Add(this.buttonTargetEnemy1);
            this.Controls.Add(this.battleSpeedBar);
            this.Controls.Add(this.TimeSpeedLabel);
            this.Controls.Add(this.playerActionLabel3);
            this.Controls.Add(this.enemyActionLabel3);
            this.Controls.Add(this.enemyActionLabel2);
            this.Controls.Add(this.enemyActionLabel1);
            this.Controls.Add(this.playerActionLabel1);
            this.Controls.Add(this.enemyNameLabel3);
            this.Controls.Add(this.enemyNameLabel2);
            this.Controls.Add(this.nameLabel3);
            this.Controls.Add(this.nameLabel1);
            this.Controls.Add(this.pbBattleTimerBar);
            this.Controls.Add(this.pbPlayer1);
            this.Controls.Add(this.lblLifeEnemy3);
            this.Controls.Add(this.lifeLabel3);
            this.Controls.Add(this.lblLifeEnemy2);
            this.Controls.Add(this.lifeLabel1);
            this.Controls.Add(this.BattleStart);
            this.Controls.Add(this.txtBattleMessage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "TruthBattleEnemy";
            this.ShowInTaskbar = false;
            this.Text = "TruthBattleEnemy";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TruthBattleEnemy_FormClosing);
            this.Load += new System.EventHandler(this.TruthBattleEnemy_Load);
            this.Shown += new System.EventHandler(this.TruthBattleEnemy_Shown);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.TruthBattleEnemy_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TruthBattleEnemy_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbMatrixDragon)).EndInit();
            this.BattleMenuPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RunAwayButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BattleSettingButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UseItemButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEnemyTargetTarget3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlayerTargetTarget3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEnemyTargetTarget1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlayerTargetTarget1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbEnemyTargetTarget2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlayerTargetTarget2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonTargetPlayer2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonTargetPlayer3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonTargetEnemy2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonTargetEnemy3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonTargetPlayer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.buttonTargetEnemy1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.battleSpeedBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBattleTimerBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbPlayer1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton29)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton39)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton38)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton37)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton36)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton35)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton34)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton33)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton32)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton31)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ActionButton11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery21)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery22)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery23)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery24)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery25)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery26)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery27)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery28)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery29)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery31)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery32)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery33)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery34)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery35)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery36)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery37)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery38)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.IsSorcery39)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FieldBuff1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FieldBuff2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FieldBuff3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FieldBuff4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FieldBuff5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FieldBuff6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSandglass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbAnimeSandGlass)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbPlayer1;
        private System.Windows.Forms.TextBox txtBattleMessage;
        private System.Windows.Forms.Button BattleStart;
        private System.Windows.Forms.Label lifeLabel1;
        private System.Windows.Forms.PictureBox pbBattleTimerBar;
        private System.Windows.Forms.Label nameLabel1;
        private System.Windows.Forms.Label playerActionLabel1;
        private System.Windows.Forms.Label TimeSpeedLabel;
        private System.Windows.Forms.TrackBar battleSpeedBar;
        private System.Windows.Forms.Label lblLifeEnemy2;
        private System.Windows.Forms.Label enemyNameLabel2;
        private System.Windows.Forms.Label lblLifeEnemy3;
        private System.Windows.Forms.Label enemyNameLabel3;
        private System.Windows.Forms.Label lifeLabel3;
        private System.Windows.Forms.Label nameLabel3;
        private System.Windows.Forms.Label playerActionLabel3;
        private System.Windows.Forms.PictureBox buttonTargetEnemy1;
        private System.Windows.Forms.PictureBox buttonTargetPlayer1;
        private System.Windows.Forms.PictureBox buttonTargetPlayer3;
        private System.Windows.Forms.PictureBox buttonTargetEnemy3;
        private System.Windows.Forms.PictureBox buttonTargetEnemy2;
        private System.Windows.Forms.PictureBox buttonTargetPlayer2;
        private System.Windows.Forms.Label playerActionLabel2;
        private System.Windows.Forms.Label nameLabel2;
        private System.Windows.Forms.Label lifeLabel2;
        private System.Windows.Forms.PictureBox pbPlayerTargetTarget2;
        private System.Windows.Forms.PictureBox pbEnemyTargetTarget2;
        private System.Windows.Forms.PictureBox pbPlayerTargetTarget1;
        private System.Windows.Forms.PictureBox pbEnemyTargetTarget1;
        private System.Windows.Forms.PictureBox pbPlayerTargetTarget3;
        private System.Windows.Forms.PictureBox pbEnemyTargetTarget3;
        private System.Windows.Forms.Label enemyNameLabel1;
        private System.Windows.Forms.Label lblLifeEnemy1;
        private System.Windows.Forms.Label currentSkillPoint1;
        private System.Windows.Forms.Label currentManaPoint1;
        private System.Windows.Forms.Label currentManaPoint2;
        private System.Windows.Forms.Label currentSkillPoint2;
        private System.Windows.Forms.Label enemyActionLabel1;
        private System.Windows.Forms.Label enemyActionLabel2;
        private System.Windows.Forms.Label enemyActionLabel3;
        private System.Windows.Forms.Timer timerBattleStart;
        private System.Windows.Forms.Label labelBattleTurn;
        private System.Windows.Forms.Label currentEnemySkillPoint1;
        private System.Windows.Forms.Label currentEnemyManaPoint1;
        private TruthImage ActionButton11;
        private TruthImage ActionButton21;
        private TruthImage ActionButton31;
        private TruthImage ActionButton12;
        private TruthImage ActionButton13;
        private TruthImage ActionButton14;
        private TruthImage ActionButton15;
        private TruthImage ActionButton16;
        private TruthImage ActionButton17;
        private TruthImage ActionButton22;
        private TruthImage ActionButton23;
        private TruthImage ActionButton24;
        private TruthImage ActionButton25;
        private TruthImage ActionButton26;
        private TruthImage ActionButton27;
        private TruthImage ActionButton32;
        private TruthImage ActionButton33;
        private TruthImage ActionButton34;
        private TruthImage ActionButton35;
        private TruthImage ActionButton36;
        private TruthImage ActionButton37;
        private System.Windows.Forms.Label currentInstantPoint1;
        private System.Windows.Forms.Panel BuffPanel1;
        private System.Windows.Forms.Panel BuffPanel2;
        private System.Windows.Forms.Panel BuffPanel3;
        private System.Windows.Forms.Panel PanelBuffEnemy1;
        private System.Windows.Forms.Panel PanelBuffEnemy2;
        private System.Windows.Forms.Panel PanelBuffEnemy3;
        private System.Windows.Forms.Label currentInstantPoint2;
        private System.Windows.Forms.Label labelEnemyDamage1;
        private System.Windows.Forms.Label labelDamage1;
        private System.Windows.Forms.Label labelDamage2;
        private System.Windows.Forms.Label labelDamage3;
        private System.Windows.Forms.Label labelEnemyDamage3;
        private System.Windows.Forms.Label labelEnemyDamage2;
        private System.Windows.Forms.Label StackInTheCommandLabel;
        private System.Windows.Forms.Label StackNameLabel;
        private System.Windows.Forms.Label UseItemGauge;
        private System.Windows.Forms.Label currentEnemyInstantPoint1;
        private System.Windows.Forms.Label labelCritical1;
        private System.Windows.Forms.Label labelCritical2;
        private System.Windows.Forms.Label labelCritical3;
        private System.Windows.Forms.Label labelEnemyCritical1;
        private System.Windows.Forms.Label labelEnemyCritical2;
        private System.Windows.Forms.Label labelEnemyCritical3;
        private System.Windows.Forms.Label currentEnemyInstantPoint2;
        private System.Windows.Forms.Label currentEnemyInstantPoint3;
        private System.Windows.Forms.PictureBox UseItemButton;
        private System.Windows.Forms.PictureBox BattleSettingButton;
        private System.Windows.Forms.PictureBox RunAwayButton;
        private System.Windows.Forms.Panel BattleMenuPanel;
        private System.Windows.Forms.Label currentInstantPoint3;
        private System.Windows.Forms.Label currentManaPoint3;
        private System.Windows.Forms.Label currentSkillPoint3;
        private System.Windows.Forms.Label keyNum1_1;
        private System.Windows.Forms.Label keyNum1_2;
        private System.Windows.Forms.Label keyNum1_3;
        private System.Windows.Forms.Label keyNum1_4;
        private System.Windows.Forms.Label keyNum1_5;
        private System.Windows.Forms.Label keyNum1_6;
        private System.Windows.Forms.Label keyNum1_7;
        private System.Windows.Forms.Label keyNum2_1;
        private System.Windows.Forms.Label keyNum2_2;
        private System.Windows.Forms.Label keyNum2_3;
        private System.Windows.Forms.Label keyNum2_4;
        private System.Windows.Forms.Label keyNum2_5;
        private System.Windows.Forms.Label keyNum2_6;
        private System.Windows.Forms.Label keyNum2_7;
        private System.Windows.Forms.Label keyNum3_1;
        private System.Windows.Forms.Label keyNum3_2;
        private System.Windows.Forms.Label keyNum3_3;
        private System.Windows.Forms.Label keyNum3_4;
        private System.Windows.Forms.Label keyNum3_5;
        private System.Windows.Forms.Label keyNum3_6;
        private System.Windows.Forms.Label keyNum3_7;
        private System.Windows.Forms.PictureBox pbMatrixDragon;
        private System.Windows.Forms.Label MatrixDragonTalk;
        private TruthImage ActionButton18;
        private TruthImage ActionButton19;
        private System.Windows.Forms.Label keyNum1_8;
        private System.Windows.Forms.Label keyNum1_9;
        private TruthImage ActionButton28;
        private TruthImage ActionButton29;
        private TruthImage ActionButton38;
        private TruthImage ActionButton39;
        private System.Windows.Forms.Label keyNum2_8;
        private System.Windows.Forms.Label keyNum2_9;
        private System.Windows.Forms.Label keyNum3_8;
        private System.Windows.Forms.Label keyNum3_9;
        private TruthImage IsSorcery11;
        private TruthImage IsSorcery12;
        private TruthImage IsSorcery13;
        private TruthImage IsSorcery14;
        private TruthImage IsSorcery15;
        private TruthImage IsSorcery16;
        private TruthImage IsSorcery17;
        private TruthImage IsSorcery18;
        private TruthImage IsSorcery19;
        private TruthImage IsSorcery21;
        private TruthImage IsSorcery22;
        private TruthImage IsSorcery23;
        private TruthImage IsSorcery24;
        private TruthImage IsSorcery25;
        private TruthImage IsSorcery26;
        private TruthImage IsSorcery27;
        private TruthImage IsSorcery28;
        private TruthImage IsSorcery29;
        private TruthImage IsSorcery31;
        private TruthImage IsSorcery32;
        private TruthImage IsSorcery33;
        private TruthImage IsSorcery34;
        private TruthImage IsSorcery35;
        private TruthImage IsSorcery36;
        private TruthImage IsSorcery37;
        private TruthImage IsSorcery38;
        private TruthImage IsSorcery39;
        private TruthImage FieldBuff1;
        private TruthImage FieldBuff2;
        private TruthImage FieldBuff3;
        private TruthImage FieldBuff4;
        private TruthImage FieldBuff5;
        private TruthImage FieldBuff6;
        private PictureBox pbSandglass;
        private Label lblTimerCount;
        private PictureBox pbAnimeSandGlass;
        private Label labelSpecialInstant;
    }
}