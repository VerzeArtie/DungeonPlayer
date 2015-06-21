using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.Reflection;
using SharpDX.DirectInput;

namespace DungeonPlayer
{
    public partial class TruthBattleEnemy : MotherForm
    {
        public enum CriticalType
        {
            None,
            Random,
            Absolute,
        }

        MainCharacter mc;
        public MainCharacter MC 
        {
            get { return mc; }
            set { mc = value; }
        }
        MainCharacter sc;
        public MainCharacter SC
        {
            get { return sc; }
            set { sc = value; }
        }
        MainCharacter tc;
        public MainCharacter TC
        {
            get { return tc; }
            set { tc = value; }
        }

        TruthEnemyCharacter ec1;
        public TruthEnemyCharacter EC1
        {
            get { return ec1; }
            set { ec1 = value; }
        }

        TruthEnemyCharacter ec2;
        public TruthEnemyCharacter EC2
        {
            get { return ec2; }
            set { ec2 = value; }
        }

        TruthEnemyCharacter ec3;
        public TruthEnemyCharacter EC3
        {
            get { return ec3; }
            set { ec3 = value; }
        }

        private WorldEnvironment we;
        public WorldEnvironment WE
        {
            get { return we; }
            set { we = value; }
        }

        Thread keyTh = null; // �L�[���͌��m�X���b�h
        Thread th = null; // ���C���퓬�̃X���b�h
        bool tempStopFlag = false; // [�퓬��~�v�{�^����ESC�L�[�ŁA�퓬����U��~�����������Ɏg���t���O
        bool endFlag = false; // ���C���퓬�̃��[�v�𔲂��鎞�Ɏg���t���O
        bool cannotRunAway = false; // �퓬���瓦�����邩�ǂ����������t���O

        Bitmap bmpPlayer1;
        Bitmap bmpPlayer2;
        Bitmap bmpPlayer3;
        Bitmap bmpPlayer4;
        Bitmap bmpEnemy1;
        Bitmap bmpEnemy2;
        Bitmap bmpEnemy3;
        Bitmap bmpShadowEnemy1_2;
        Bitmap bmpShadowEnemy1_3;

        private YesNoRequest yesno;

        public bool DuelMode { get; set; }

        private TruthImage[] pbBuffPlayer1;
        private TruthImage[] pbBuffPlayer2;
        private TruthImage[] pbBuffPlayer3;
        private TruthImage[] pbBuffEnemy1;
        private TruthImage[] pbBuffEnemy2;
        private TruthImage[] pbBuffEnemy3;

        Thread StackThread = null; // �X�^�b�N�C���U�R�}���h�̃X���b�h
        bool NowStackInTheCommand = false; // �X�^�b�N�C���U�R�}���h�ň�U��~�����������Ɏg���t���O

        bool NowTimeStop = false; // �^�C���X�g�b�v�u�S�́v�̃t���O

        public bool HiSpeedAnimation { get; set; } // �ʏ�_���[�W�A�j���[�V�����𑁂߂邽�߂Ɏg�p
        public bool FinalBattle { get; set; } // �ŏI�퓬�A�X�^�b�N�R�}���h�̓���𑁂߂邽�߂Ɏg�p
        public bool LifeCountBattle { get; set; } // �ŏI�퓬�Ń��C�t�J�E���g��\�����邽�߂Ɏg�p

        delegate void _AnimationFinal1(string message);
        private void AnimationFinal1(string message)
        {
            Label targetLabel = MatrixDragonTalk;
            targetLabel.Size = new System.Drawing.Size(1024, 100);
            targetLabel.Text = message;
            targetLabel.Width = 2048;
            targetLabel.Location = new Point(-1024, 400);
            targetLabel.Visible = true;
            targetLabel.BringToFront();

            targetLabel.TextAlign = ContentAlignment.MiddleCenter;
            targetLabel.Font = new System.Drawing.Font("HG��������-PRO", 44F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            targetLabel.Visible = true;
            targetLabel.Update();
            int waitTime = 400;

            for (int ii = 0; ii < waitTime; ii++)
            {
                System.Threading.Thread.Sleep(1);
                if (ii < 25)
                {
                    targetLabel.Width -= 30;
                    targetLabel.Location = new Point(targetLabel.Location.X + 30, targetLabel.Location.Y);
                }
                else if (ii < 1024 - (25 * 30))
                {
                    targetLabel.Width -= 1;
                    targetLabel.Location = new Point(targetLabel.Location.X + 1, targetLabel.Location.Y);
                }
                else
                {
                    targetLabel.Width += 25;
                    targetLabel.Height -= 2;
                    targetLabel.Location = new Point(targetLabel.Location.X, targetLabel.Location.Y + 1);
                }
                this.Update();
            }
            System.Threading.Thread.Sleep(500);
            targetLabel.Visible = false;
            targetLabel.TextAlign = ContentAlignment.MiddleLeft;
            targetLabel.Font = new System.Drawing.Font("HG��������-PRO", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            targetLabel.Update();
        }

        delegate void _AnimationSandGlass();
        private void AnimationSandGlass()
        {
            Label targetLabel = MatrixDragonTalk;
            targetLabel.Location = new Point(0, 400);
            targetLabel.Size = new System.Drawing.Size(1024, 100);
            targetLabel.Text = String.Empty;
            targetLabel.Visible = true;
            targetLabel.BringToFront();
            PictureBox sandGlass = pbAnimeSandGlass;
            sandGlass.Location = new Point(25, 405);
            sandGlass.Visible = true;
            sandGlass.BringToFront();


            targetLabel.TextAlign = ContentAlignment.MiddleCenter;
            targetLabel.Font = new System.Drawing.Font("HG��������-PRO", 44F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            targetLabel.Text = this.BattleTurnCount.ToString();                
            targetLabel.Visible = true;
            targetLabel.Update();
            sandGlass.Update();
            int waitTime = 52;

            for (int ii = 0; ii < waitTime; ii++)
            {
                System.Threading.Thread.Sleep(10);
                if (ii > 26)
                {
                    System.Threading.Thread.Sleep(5);
                    sandGlass.Image = this.imageAnimeSandGlass[ii - 27];
                    sandGlass.Location = new Point(sandGlass.Location.X + (ii-27)*3, 405);
                    sandGlass.Update();

                    if (ii == 42)
                    {
                        targetLabel.Text = (this.BattleTurnCount + 1).ToString();
                        targetLabel.Update();
                    }
                    this.Update();
                }
            }
            System.Threading.Thread.Sleep(500);
            targetLabel.Visible = false;
            targetLabel.TextAlign = ContentAlignment.MiddleLeft;
            targetLabel.Font = new System.Drawing.Font("HG��������-PRO", 18F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            targetLabel.Update();
            sandGlass.Visible = false;
            sandGlass.Image = this.imageAnimeSandGlass[0];
            sandGlass.Update();
        }

        delegate void _AnimationMessageFadeOut(string message);
        private void AnimationMessageFadeOut(string message)
        {
            Label targetLabel = MatrixDragonTalk;
            targetLabel.Location = new Point(0, 350);
            targetLabel.Size = new System.Drawing.Size(1024, 100);
            targetLabel.Text = message;
            targetLabel.Visible = true;
            targetLabel.BringToFront();

 
            targetLabel.Visible = true;
            targetLabel.Update();
            int waitTime = 300;

            for (int ii = 0; ii < waitTime; ii++)
            {
                System.Threading.Thread.Sleep(10);
                if (ii > 250)
                {
                    targetLabel.Size = new Size(targetLabel.Size.Width, targetLabel.Size.Height-2);
                    targetLabel.Location = new Point(targetLabel.Location.X, targetLabel.Location.Y + 1);
                    this.Update();
                }
                targetLabel.Update();
            }
            targetLabel.Visible = false;
            targetLabel.Update();
        }

        // �_���[�W�A�j���[�V�����A���W�J�͌�ł���Ă��������B          
        // ��������Ă���A�n���O�A�b�v�������B�����炭�X���b�h�Ăяo������I�u�W�F�N�g���ڂ�����Ă��邽�߁B
        // ���̃��x���E�s�N�`���{�b�N�X�Ȃǂւ̐G��͑S�ĉ��W�J�ΏۂƂ���B
        delegate void _AnimationDamage(double damage, MainCharacter target, int interval, System.Drawing.Color plusValue, bool avoid, bool critical, string customString);
        /// <summary>
        /// �_���[�W�E�N���e�B�J���E������ʂȂǂ��A�j���[�V�����\�����郁�\�b�h
        /// </summary>
        /// <param name="damage">�_���[�W�ʁi������ʂ₻�̑��̏ꍇ�A���������p�����^�j</param>
        /// <param name="target">�ΏۂƂȂ�^�[�Q�b�g</param>
        /// <param name="interval">�A�j���[�V�����^�C���i�O�̏ꍇ�A�f�t�H���g�^�C���j</param>
        /// <param name="plusValue">True�F�_���[�W�����Z�����ꍇ</param>
        /// <param name="avoid">True�F������ꂽ���������ꍇ</param>
        /// <param name="critical">True�F�N���e�B�J�����������Ă��鎖�������ꍇ</param>
        /// <param name="customString">�C�ӂ̕����F�C�ӂ̕������\���������ꍇ</param>
        private void AnimationDamage(double damage, MainCharacter target, int interval, System.Drawing.Color plusValue, bool avoid, bool critical, string customString)
        {
            Label targetLabel = null;
            Label targetCriticalLabel = null;
            if (target == ec1)
            {
                targetLabel = labelEnemyDamage1;
                targetCriticalLabel = labelEnemyCritical1;
            }
            if (target == ec2)
            {
                targetLabel = labelEnemyDamage2;
                targetCriticalLabel = labelEnemyCritical2;
            }
            if (target == ec3)
            {
                targetLabel = labelEnemyDamage3;
                targetCriticalLabel = labelEnemyCritical3;
            }
            else if (target == mc)
            {
                targetLabel = labelDamage1;
                targetCriticalLabel = labelCritical1;
            }
            else if (target == sc)
            {
                targetLabel = labelDamage2;
                targetCriticalLabel = labelCritical2;
            }
            else if (target == tc)
            {
                targetLabel = labelDamage3;
                targetCriticalLabel = labelCritical3;
            }

            targetLabel.ForeColor = plusValue;

            if (avoid)
            {
                targetLabel.Text = "�~�X";
            }
            else
            {
                targetLabel.Text = Convert.ToString((int)damage);
            }

            if (customString != String.Empty)
            {
                targetLabel.Text = customString;
            }

            int waitTime = 60;
            if (TIMER_SPEED == 40) waitTime = 150;
            else if (TIMER_SPEED == 20) waitTime = 90;
            else if (TIMER_SPEED == 10) waitTime = 60;
            else if (TIMER_SPEED == 5) waitTime = 40;
            else if (TIMER_SPEED == 2) waitTime = 20;
            if (this.HiSpeedAnimation) { waitTime = waitTime / 2; }
            if (interval > 0) waitTime = interval;

            if (critical)
            {
                targetLabel.Font = new System.Drawing.Font(targetLabel.Font.FontFamily, targetLabel.Font.Size + 4.0F, targetLabel.Font.Style);
                targetCriticalLabel.Visible = true;
                targetCriticalLabel.Update();
            }

            targetLabel.Visible = true;
            targetLabel.Update();
            Point basePoint = targetLabel.Location;
            Point basePointCritical = targetCriticalLabel.Location;

            for (int ii = 0; ii < waitTime; ii++)
            {
                int movement = 1;
                if (ii > 10) movement = 0;
                targetLabel.Location = new Point(targetLabel.Location.X + movement, targetLabel.Location.Y);
                targetCriticalLabel.Location = new Point(targetCriticalLabel.Location.X + movement, targetCriticalLabel.Location.Y);
                System.Threading.Thread.Sleep(10);
                targetLabel.Update();
                targetCriticalLabel.Update();
            }
            targetLabel.Visible = false;
            targetLabel.Location = basePoint;
            targetLabel.Update();

            targetCriticalLabel.Visible = false;
            targetCriticalLabel.Location = basePointCritical;
            targetCriticalLabel.Update();

            if (critical)
            {
                targetLabel.Font = new System.Drawing.Font(targetLabel.Font.FontFamily, targetLabel.Font.Size - 4.0F, targetLabel.Font.Style);
            }
        }

        public TruthBattleEnemy()
        {
            InitializeComponent();

            StackNameLabel.Width = 0;
            StackNameLabel.Update();
            StackInTheCommandLabel.Width = 0;
            StackInTheCommandLabel.Update();

            yesno = new YesNoRequest();
            yesno.StartPosition = FormStartPosition.CenterParent;

            pbBattleTimerBar.Image = new Bitmap(Database.BaseResourceFolder + "BattleTimeBar.bmp");

            bmpPlayer1 = new Bitmap(Database.BaseResourceFolder + "player1Arrow.bmp");
            bmpPlayer1.MakeTransparent(Color.White);
            bmpPlayer2 = new Bitmap(Database.BaseResourceFolder + "player2Arrow.bmp");
            bmpPlayer2.MakeTransparent(Color.White);
            bmpPlayer3 = new Bitmap(Database.BaseResourceFolder + "player3Arrow.bmp");
            bmpPlayer3.MakeTransparent(Color.White);
            bmpPlayer4 = new Bitmap(Database.BaseResourceFolder + "player4Arrow.bmp");
            bmpPlayer4.MakeTransparent(Color.White);
            bmpEnemy1 = new Bitmap(Database.BaseResourceFolder + "enemyArrow1.bmp");
            bmpEnemy1.MakeTransparent(Color.White);
            bmpEnemy2 = new Bitmap(Database.BaseResourceFolder + "enemyArrow2.bmp");
            bmpEnemy2.MakeTransparent(Color.White);
            bmpEnemy3 = new Bitmap(Database.BaseResourceFolder + "enemyArrow3.bmp");
            bmpEnemy3.MakeTransparent(Color.White);
            bmpShadowEnemy1_2 = new Bitmap(Database.BaseResourceFolder + "enemyArrow2.bmp");
            bmpShadowEnemy1_2.MakeTransparent(Color.White);
            bmpShadowEnemy1_3 = new Bitmap(Database.BaseResourceFolder + "enemyArrow3.bmp");
            bmpShadowEnemy1_3.MakeTransparent(Color.White);

            pbBuffPlayer1 = new TruthImage[Database.BUFF_NUM];
            pbBuffPlayer2 = new TruthImage[Database.BUFF_NUM];
            pbBuffPlayer3 = new TruthImage[Database.BUFF_NUM];
            pbBuffEnemy1 = new TruthImage[Database.BUFF_NUM];
            pbBuffEnemy2 = new TruthImage[Database.BUFF_NUM];
            pbBuffEnemy3 = new TruthImage[Database.BUFF_NUM];
            for (int ii = 0; ii < Database.BUFF_NUM; ii++)
            {
                pbBuffPlayer1[ii] = new TruthImage();
                pbBuffPlayer1[ii].Location = new System.Drawing.Point(-25, 0);
                pbBuffPlayer1[ii].Name = "pbBuffPlayer1" + ii;
                pbBuffPlayer1[ii].Size = new System.Drawing.Size(Database.BUFF_SIZE_X, Database.BUFF_SIZE_Y);
                pbBuffPlayer1[ii].SizeMode = PictureBoxSizeMode.StretchImage;
                pbBuffPlayer1[ii].TabStop = false;
                pbBuffPlayer1[ii].BuffMode = TruthImage.buffType.Small;
                pbBuffPlayer1[ii].MouseEnter += new EventHandler(TruthBattleEnemy_MouseEnter);
                pbBuffPlayer1[ii].MouseMove += new MouseEventHandler(TruthBattleEnemy_MouseMove);
                pbBuffPlayer1[ii].MouseLeave += new EventHandler(TruthBattleEnemy_MouseLeave);
                this.BuffPanel1.Controls.Add(pbBuffPlayer1[ii]);

                pbBuffPlayer2[ii] = new TruthImage();
                pbBuffPlayer2[ii].Location = new System.Drawing.Point(-25, 0);
                pbBuffPlayer2[ii].Name = "pbBuffPlayer2" + ii;
                pbBuffPlayer2[ii].Size = new System.Drawing.Size(Database.BUFF_SIZE_X, Database.BUFF_SIZE_Y);
                pbBuffPlayer2[ii].SizeMode = PictureBoxSizeMode.StretchImage;
                pbBuffPlayer2[ii].TabStop = false;
                pbBuffPlayer2[ii].BuffMode = TruthImage.buffType.Small;
                pbBuffPlayer2[ii].MouseEnter += new EventHandler(TruthBattleEnemy_MouseEnter);
                pbBuffPlayer2[ii].MouseMove += new MouseEventHandler(TruthBattleEnemy_MouseMove);
                pbBuffPlayer2[ii].MouseLeave += new EventHandler(TruthBattleEnemy_MouseLeave);
                this.BuffPanel2.Controls.Add(pbBuffPlayer2[ii]);

                pbBuffPlayer3[ii] = new TruthImage();
                pbBuffPlayer3[ii].Location = new System.Drawing.Point(-25, 0);
                pbBuffPlayer3[ii].Name = "pbBuffPlayer3" + ii;
                pbBuffPlayer3[ii].Size = new System.Drawing.Size(Database.BUFF_SIZE_X, Database.BUFF_SIZE_Y);
                pbBuffPlayer3[ii].SizeMode = PictureBoxSizeMode.StretchImage;
                pbBuffPlayer3[ii].TabStop = false;
                pbBuffPlayer3[ii].BuffMode = TruthImage.buffType.Small;
                pbBuffPlayer3[ii].MouseEnter += new EventHandler(TruthBattleEnemy_MouseEnter);
                pbBuffPlayer3[ii].MouseMove += new MouseEventHandler(TruthBattleEnemy_MouseMove);
                pbBuffPlayer3[ii].MouseLeave += new EventHandler(TruthBattleEnemy_MouseLeave);
                this.BuffPanel3.Controls.Add(pbBuffPlayer3[ii]);

                pbBuffEnemy1[ii] = new TruthImage();
                pbBuffEnemy1[ii].Location = new System.Drawing.Point(-25, 0);
                pbBuffEnemy1[ii].Name = "pbBuffEnemy1" + ii;
                pbBuffEnemy1[ii].Size = new System.Drawing.Size(Database.BUFF_SIZE_X, Database.BUFF_SIZE_Y);
                pbBuffEnemy1[ii].SizeMode = PictureBoxSizeMode.StretchImage;
                pbBuffEnemy1[ii].TabStop = false;
                pbBuffEnemy1[ii].BuffMode = TruthImage.buffType.Small;
                pbBuffEnemy1[ii].MouseEnter += new EventHandler(TruthBattleEnemy_MouseEnter);
                pbBuffEnemy1[ii].MouseMove += new MouseEventHandler(TruthBattleEnemy_MouseMove);
                pbBuffEnemy1[ii].MouseLeave += new EventHandler(TruthBattleEnemy_MouseLeave);
                this.PanelBuffEnemy1.Controls.Add(pbBuffEnemy1[ii]);

                pbBuffEnemy2[ii] = new TruthImage();
                pbBuffEnemy2[ii].Location = new System.Drawing.Point(-25, 0);
                pbBuffEnemy2[ii].Name = "pbBuffEnemy2" + ii;
                pbBuffEnemy2[ii].Size = new System.Drawing.Size(Database.BUFF_SIZE_X, Database.BUFF_SIZE_Y);
                pbBuffEnemy2[ii].SizeMode = PictureBoxSizeMode.StretchImage;
                pbBuffEnemy2[ii].TabStop = false;
                pbBuffEnemy2[ii].BuffMode = TruthImage.buffType.Small;
                pbBuffEnemy2[ii].MouseEnter += new EventHandler(TruthBattleEnemy_MouseEnter);
                pbBuffEnemy2[ii].MouseMove += new MouseEventHandler(TruthBattleEnemy_MouseMove);
                pbBuffEnemy2[ii].MouseLeave += new EventHandler(TruthBattleEnemy_MouseLeave);
                this.PanelBuffEnemy2.Controls.Add(pbBuffEnemy2[ii]);

                pbBuffEnemy3[ii] = new TruthImage();
                pbBuffEnemy3[ii].Location = new System.Drawing.Point(-25, 0);
                pbBuffEnemy3[ii].Name = "pbBuffEnemy3" + ii;
                pbBuffEnemy3[ii].Size = new System.Drawing.Size(Database.BUFF_SIZE_X, Database.BUFF_SIZE_Y);
                pbBuffEnemy3[ii].SizeMode = PictureBoxSizeMode.StretchImage;
                pbBuffEnemy3[ii].TabStop = false;
                pbBuffEnemy3[ii].BuffMode = TruthImage.buffType.Small;
                pbBuffEnemy3[ii].MouseEnter += new EventHandler(TruthBattleEnemy_MouseEnter);
                pbBuffEnemy3[ii].MouseMove += new MouseEventHandler(TruthBattleEnemy_MouseMove);
                pbBuffEnemy3[ii].MouseLeave += new EventHandler(TruthBattleEnemy_MouseLeave);
                this.PanelBuffEnemy3.Controls.Add(pbBuffEnemy3[ii]);
            }
        }

        int activatePlayerNumber = 0;
        SortedList<int, MainCharacter> ActiveList = new SortedList<int, MainCharacter>();
        private static Random rand = new Random(DateTime.Now.Millisecond * System.Environment.TickCount);

        private void ActivateSomeCharacter(MainCharacter player, MainCharacter target,
            Label name, Label life, Label backSkillPoint, Label currentSkillPoint, Label backManaPoint, Label currentManaPoint, Label currentInstantPoint, Label currentSpecialInstant,
            TruthImage action1, TruthImage action2, TruthImage action3, TruthImage action4, TruthImage action5, TruthImage action6, TruthImage action7, TruthImage action8, TruthImage action9,
            Label actionLabel,
            Panel buffPanel,
            PictureBox mainObject, System.Drawing.Color mainColor, PictureBox targetTarget, Bitmap mainFaceArrow, Bitmap shadowFaceArrow2, Bitmap shadowFaceArrow3,
            Label damageLabel, Label criticalLabel,
            TruthImage[] buffList,
            Label keyNum1, Label keyNum2, Label keyNum3, Label keyNum4, Label keyNum5, Label keyNum6, Label keyNum7, Label keyNum8, Label keyNum9,
            PictureBox sorcery1, PictureBox sorcery2, PictureBox sorcery3, PictureBox sorcery4, PictureBox sorcery5, PictureBox sorcery6, PictureBox sorcery7, PictureBox sorcery8, PictureBox sorcery9
            )
        {
            if (player != null)
            {
                player.RealTimeBattle = true;

                // �퓬���UI�ւ̏����ݒ�
                // MainCharacter�N���X���e�Ɛ퓬���UI�̊��蓖��
                player.labelName = name;
                player.labelName.Text = player.Name;

                player.labelLife = life;
                UpdateLife(player);

                player.labelCurrentSkillPoint = currentSkillPoint;
                if (player.labelCurrentSkillPoint != null)
                {
                    player.labelCurrentSkillPoint.Text = player.CurrentSkillPoint.ToString() + " / " + player.MaxSkillPoint.ToString();
                }
                
                player.labelCurrentManaPoint = currentManaPoint;
                if (player.labelCurrentManaPoint != null)
                {
                    player.labelCurrentManaPoint.Text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();
                }

                player.CurrentInstantPoint = 0; // ��Ғǉ� // �u�R�����g�v���������ł�MAX�l�ɖ߂��Ă����ق��������Ǝv�������A�v���C���Ă݂Ă͂��߂͂O�̂ق����A�Q�[�����͖ʔ�����������Ǝv�����B
                player.labelCurrentInstantPoint = currentInstantPoint;
                if (player.labelCurrentInstantPoint != null)
                {
                    player.labelCurrentInstantPoint.Text = player.CurrentInstantPoint.ToString() + " / " + player.MaxInstantPoint.ToString();
                }
                player.labelCurrentSpecialInstant = currentSpecialInstant;
                if (player.labelCurrentSpecialInstant != null)
                {
                    player.labelCurrentSpecialInstant.Text = player.CurrentSpecialInstant.ToString() + " / " + player.MaxSpecialInstant.ToString();
                }

                player.TextBattleMessage = this.txtBattleMessage;

                player.ActionButton1 = action1;
                player.ActionButton2 = action2;
                player.ActionButton3 = action3;
                player.ActionButton4 = action4;
                player.ActionButton5 = action5;
                player.ActionButton6 = action6;
                player.ActionButton7 = action7;
                player.ActionButton8 = action8;
                player.ActionButton9 = action9;
                if (player.ActionButtonList == null)
                {
                    player.ActionButtonList = new TruthImage[CURRENT_ACTION_NUM];
                    player.ActionButtonList[0] = player.ActionButton1;
                    player.ActionButtonList[1] = player.ActionButton2;
                    player.ActionButtonList[2] = player.ActionButton3;
                    player.ActionButtonList[3] = player.ActionButton4;
                    player.ActionButtonList[4] = player.ActionButton5;
                    player.ActionButtonList[5] = player.ActionButton6;
                    player.ActionButtonList[6] = player.ActionButton7;
                    player.ActionButtonList[7] = player.ActionButton8;
                    player.ActionButtonList[8] = player.ActionButton9;
                }
                for (int ii = 0; ii < CURRENT_ACTION_NUM; ii++)
                {
                    if (player.ActionButtonList[ii] != null)
                    {
                        player.ActionButtonList[ii].MouseMove += new MouseEventHandler(TruthBattleEnemy_Action_MouseMove);
                        player.ActionButtonList[ii].MouseLeave += new EventHandler(TruthBattleEnemy_Action_Leave);
                    }
                }

                player.ActionKeyNum1 = keyNum1; if (player.ActionKeyNum1 != null) player.ActionKeyNum1.Visible = true;
                player.ActionKeyNum2 = keyNum2; if (player.ActionKeyNum2 != null) player.ActionKeyNum2.Visible = true;
                player.ActionKeyNum3 = keyNum3; if (player.ActionKeyNum3 != null) player.ActionKeyNum3.Visible = true;
                player.ActionKeyNum4 = keyNum4; if (player.ActionKeyNum4 != null) player.ActionKeyNum4.Visible = true;
                player.ActionKeyNum5 = keyNum5; if (player.ActionKeyNum5 != null) player.ActionKeyNum5.Visible = true;
                player.ActionKeyNum6 = keyNum6; if (player.ActionKeyNum6 != null) player.ActionKeyNum6.Visible = true;
                player.ActionKeyNum7 = keyNum7; if (player.ActionKeyNum7 != null) player.ActionKeyNum7.Visible = true;
                player.ActionKeyNum8 = keyNum8; if (player.ActionKeyNum8 != null) player.ActionKeyNum8.Visible = true;
                player.ActionKeyNum9 = keyNum9; if (player.ActionKeyNum9 != null) player.ActionKeyNum9.Visible = true;

                player.IsSorceryMark1 = sorcery1;
                player.IsSorceryMark2 = sorcery2;
                player.IsSorceryMark3 = sorcery3;
                player.IsSorceryMark4 = sorcery4;
                player.IsSorceryMark5 = sorcery5;
                player.IsSorceryMark6 = sorcery6;
                player.IsSorceryMark7 = sorcery7;
                player.IsSorceryMark8 = sorcery8;
                player.IsSorceryMark9 = sorcery9;

                player.ActionLabel = actionLabel;
                player.BuffPanel = buffPanel;
                player.BuffPanel.Visible = true;

                player.MainObjectButton = mainObject;
                player.MainColor = mainColor;
                player.MainObjectButton.BackColor = mainColor;
                player.pbTargetTarget = targetTarget;
                player.MainFaceArrow = mainFaceArrow;
                if (player.Name == Database.ENEMY_LAST_SIN_VERZE_ARTIE) { player.ShadowFaceArrow2 = shadowFaceArrow2; player.ShadowFaceArrow3 = shadowFaceArrow3; } // �ŏI�탔�F���[�̂݁A���g�̋Z���g���B
                player.DamageLabel = damageLabel;
                player.CriticalLabel = criticalLabel;

                // BUFF���X�g��o�^                
                int num = 0;
                player.pbProtection = buffList[num]; buffList[num].ImageName = Database.PROTECTION; num++;
                player.pbAbsorbWater = buffList[num]; buffList[num].ImageName = Database.ABSORB_WATER; num++;
                player.pbShadowPact = buffList[num]; buffList[num].ImageName = Database.SHADOW_PACT; num++;
                player.pbFlameAura = buffList[num]; buffList[num].ImageName = Database.FLAME_AURA; num++;
                player.pbHeatBoost = buffList[num]; buffList[num].ImageName = Database.HEAT_BOOST; num++;
                player.pbSaintPower = buffList[num]; buffList[num].ImageName = Database.SAINT_POWER; num++;
                player.pbWordOfLife = buffList[num]; buffList[num].ImageName = Database.WORD_OF_LIFE; num++;
                player.pbGlory = buffList[num]; buffList[num].ImageName = Database.GLORY; num++;
                player.pbVoidExtraction = buffList[num]; buffList[num].ImageName = Database.VOID_EXTRACTION; num++;
                player.pbOneImmunity = buffList[num]; buffList[num].ImageName = Database.ONE_IMMUNITY; num++;
                player.pbGaleWind = buffList[num]; buffList[num].ImageName = Database.GALE_WIND; num++;
                player.pbWordOfFortune = buffList[num]; buffList[num].ImageName = Database.WORD_OF_FORTUNE; num++;
                player.pbBloodyVengeance = buffList[num]; buffList[num].ImageName = Database.BLOODY_VENGEANCE; num++;
                player.pbRiseOfImage = buffList[num]; buffList[num].ImageName = Database.RISE_OF_IMAGE; num++;
                player.pbImmortalRave = buffList[num]; buffList[num].ImageName = Database.IMMORTAL_RAVE; num++;
                player.pbHighEmotionality = buffList[num]; buffList[num].ImageName = Database.HIGH_EMOTIONALITY; num++;
                player.pbBlackContract = buffList[num]; buffList[num].ImageName = Database.BLACK_CONTRACT; num++;
                player.pbAetherDrive = buffList[num]; buffList[num].ImageName = Database.AETHER_DRIVE; num++;
                player.pbEternalPresence = buffList[num]; buffList[num].ImageName = Database.ETERNAL_PRESENCE; num++;
                player.pbMirrorImage = buffList[num]; buffList[num].ImageName = Database.MIRROR_IMAGE; num++;
                player.pbDeflection = buffList[num]; buffList[num].ImageName = Database.DEFLECTION; num++;
                player.pbTruthVision = buffList[num]; buffList[num].ImageName = Database.TRUTH_VISION; num++;
                player.pbStanceOfFlow = buffList[num]; buffList[num].ImageName = Database.STANCE_OF_FLOW; num++;
                player.pbPromisedKnowledge = buffList[num]; buffList[num].ImageName = Database.PROMISED_KNOWLEDGE; num++;
                player.pbStanceOfDeath = buffList[num]; buffList[num].ImageName = Database.STANCE_OF_DEATH; num++;
                player.pbAntiStun = buffList[num]; buffList[num].ImageName = Database.ANTI_STUN; num++;

                player.pbStanceOfEyes = buffList[num]; buffList[num].ImageName = Database.STANCE_OF_EYES; num++;
                player.pbNegate = buffList[num]; buffList[num].ImageName = Database.NEGATE; num++;
                player.pbCounterAttack = buffList[num]; buffList[num].ImageName = Database.COUNTER_ATTACK; num++;
                player.pbStanceOfStanding = buffList[num]; buffList[num].ImageName = Database.STANCE_OF_STANDING; num++;

                player.pbPainfulInsanity = buffList[num]; buffList[num].ImageName = Database.PAINFUL_INSANITY; num++;
                player.pbDamnation = buffList[num]; buffList[num].ImageName = Database.DAMNATION; num++;
                player.pbAbsoluteZero = buffList[num]; buffList[num].ImageName = Database.ABSOLUTE_ZERO; num++;
                player.pbNothingOfNothingness = buffList[num]; buffList[num].ImageName = Database.NOTHING_OF_NOTHINGNESS; num++;

                player.pbPoison = buffList[num]; buffList[num].ImageName = Database.EFFECT_POISON; num++;
                player.pbStun = buffList[num]; buffList[num].ImageName = Database.EFFECT_STUN; num++;
                player.pbSilence = buffList[num]; buffList[num].ImageName = Database.EFFECT_SILENCE; num++;
                player.pbParalyze = buffList[num]; buffList[num].ImageName = Database.EFFECT_PARALYZE; num++;
                player.pbFrozen = buffList[num]; buffList[num].ImageName = Database.EFFECT_FROZEN; num++;
                player.pbTemptation = buffList[num]; buffList[num].ImageName = Database.EFFECT_TEMPTATION; num++;
                player.pbNoResurrection = buffList[num]; buffList[num].ImageName = Database.EFFECT_NORESURRECTION; num++;
                player.pbSlow = buffList[num]; buffList[num].ImageName = Database.EFFECT_SLOW; num++;
                player.pbBlind = buffList[num]; buffList[num].ImageName = Database.EFFECT_BLIND; num++;
                player.pbSlip = buffList[num]; buffList[num].ImageName = Database.EFFECT_SLIP; num++;
                player.pbNoGainLife = buffList[num]; buffList[num].ImageName = Database.EFFECT_NOGAIN_LIFE; num++;

                player.pbBuff1 = buffList[num]; buffList[num].ImageName = String.Empty; num++; // ���g�p
                player.pbBuff2 = buffList[num]; buffList[num].ImageName = String.Empty; num++; // ���g�p
                player.pbBuff3 = buffList[num]; buffList[num].ImageName = String.Empty; num++; // ���g�p

                player.pbPhysicalAttackUp = buffList[num]; buffList[num].ImageName = Database.PHYSICAL_ATTACK_UP; num++;
                player.pbPhysicalAttackDown = buffList[num]; buffList[num].ImageName = Database.PHYSICAL_ATTACK_DOWN; num++;
                player.pbPhysicalDefenseUp = buffList[num]; buffList[num].ImageName = Database.PHYSICAL_DEFENSE_UP; num++;
                player.pbPhysicalDefenseDown = buffList[num]; buffList[num].ImageName = Database.PHYSICAL_DEFENSE_DOWN; num++;
                player.pbMagicAttackUp = buffList[num]; buffList[num].ImageName = Database.MAGIC_ATTACK_UP; num++;
                player.pbMagicAttackDown = buffList[num]; buffList[num].ImageName = Database.MAGIC_ATTACK_DOWN; num++;
                player.pbMagicDefenseUp = buffList[num]; buffList[num].ImageName = Database.MAGIC_DEFENSE_UP; num++;
                player.pbMagicDefenseDown = buffList[num]; buffList[num].ImageName = Database.MAGIC_DEFENSE_DOWN; num++;
                player.pbSpeedUp = buffList[num]; buffList[num].ImageName = Database.BATTLE_SPEED_UP; num++;
                player.pbSpeedDown = buffList[num]; buffList[num].ImageName = Database.BATTLE_SPEED_DOWN; num++;
                player.pbReactionUp = buffList[num]; buffList[num].ImageName = Database.BATTLE_REACTION_UP; num++;
                player.pbReactionDown = buffList[num]; buffList[num].ImageName = Database.BATTLE_REACTION_DOWN; num++;
                player.pbPotentialUp = buffList[num]; buffList[num].ImageName = Database.POTENTIAL_UP; num++;
                player.pbPotentialDown = buffList[num]; buffList[num].ImageName = Database.POTENTIAL_DOWN; num++;

                player.pbStrengthUp = buffList[num]; buffList[num].ImageName = Database.EFFECT_STRENGTH_UP; num++;
                player.pbAgilityUp = buffList[num]; buffList[num].ImageName = Database.EFFECT_AGILITY_UP; num++;
                player.pbIntelligenceUp = buffList[num]; buffList[num].ImageName = Database.EFFECT_INTELLIGENCE_UP; num++;
                player.pbStaminaUp = buffList[num]; buffList[num].ImageName = Database.EFFECT_STAMINA_UP; num++;
                player.pbMindUp = buffList[num]; buffList[num].ImageName = Database.EFFECT_MIND_UP; num++;

                player.pbResistLightUp = buffList[num]; buffList[num].ImageName = Database.RESIST_LIGHT_UP; num++;
                player.pbResistShadowUp = buffList[num]; buffList[num].ImageName = Database.RESIST_SHADOW_UP; num++;
                player.pbResistFireUp = buffList[num]; buffList[num].ImageName = Database.RESIST_FIRE_UP; num++;
                player.pbResistIceUp = buffList[num]; buffList[num].ImageName = Database.RESIST_ICE_UP; num++;
                player.pbResistForceUp = buffList[num]; buffList[num].ImageName = Database.RESIST_FORCE_UP; num++;
                player.pbResistWillUp = buffList[num]; buffList[num].ImageName = Database.RESIST_WILL_UP; num++;

                player.pbResistStun = buffList[num]; buffList[num].ImageName = Database.RESIST_STUN; num++;
                player.pbResistSilence = buffList[num]; buffList[num].ImageName = Database.RESIST_SILENCE; num++;
                player.pbResistPoison = buffList[num]; buffList[num].ImageName = Database.RESIST_POISON; num++;
                player.pbResistTemptation = buffList[num]; buffList[num].ImageName = Database.RESIST_TEMPTATION; num++;
                player.pbResistFrozen = buffList[num]; buffList[num].ImageName = Database.RESIST_FROZEN; num++;
                player.pbResistParalyze = buffList[num]; buffList[num].ImageName = Database.RESIST_PARALYZE; num++;
                player.pbResistNoResurrection = buffList[num]; buffList[num].ImageName = Database.RESIST_NORESURRECTION; num++;
                player.pbResistSlow = buffList[num]; buffList[num].ImageName = Database.RESIST_SLOW; num++;
                player.pbResistBlind = buffList[num]; buffList[num].ImageName = Database.RESIST_BLIND; num++;
                player.pbResistSlip = buffList[num]; buffList[num].ImageName = Database.RESIST_SLIP; num++;

                player.pbPsychicTrance = buffList[num]; buffList[num].ImageName = Database.PSYCHIC_TRANCE; num++;
                player.pbBlindJustice = buffList[num]; buffList[num].ImageName = Database.BLIND_JUSTICE; num++;
                player.pbTranscendentWish = buffList[num]; buffList[num].ImageName = Database.TRANSCENDENT_WISH; num++;
                player.pbFlashBlaze = buffList[num]; buffList[num].ImageName = Database.FLASH_BLAZE; num++;
                player.pbSkyShield = buffList[num]; buffList[num].ImageName = Database.SKY_SHIELD; num++;
                player.pbEverDroplet = buffList[num]; buffList[num].ImageName = Database.EVER_DROPLET; num++;
                player.pbHolyBreaker = buffList[num]; buffList[num].ImageName = Database.HOLY_BREAKER; num++;
                player.pbStarLightning = buffList[num]; buffList[num].ImageName = Database.STAR_LIGHTNING; num++;
                player.pbBlackFire = buffList[num]; buffList[num].ImageName = Database.BLACK_FIRE; num++;
                player.pbWordOfMalice = buffList[num]; buffList[num].ImageName = Database.WORD_OF_MALICE; num++;
                player.pbDarkenField = buffList[num]; buffList[num].ImageName = Database.DARKEN_FIELD; num++;
                player.pbFrozenAura = buffList[num]; buffList[num].ImageName = Database.FROZEN_AURA; num++;
                player.pbEnrageBlast = buffList[num]; buffList[num].ImageName = Database.ENRAGE_BLAST; num++;
                player.pbImmolate = buffList[num]; buffList[num].ImageName = Database.IMMOLATE; num++;
                player.pbVanishWave = buffList[num]; buffList[num].ImageName = Database.VANISH_WAVE; num++;
                player.pbSeventhMagic = buffList[num]; buffList[num].ImageName = Database.SEVENTH_MAGIC; num++;
                player.pbStanceOfDouble = buffList[num]; buffList[num].ImageName = Database.STANCE_OF_DOUBLE; num++;
                player.pbSwiftStep = buffList[num]; buffList[num].ImageName = Database.SWIFT_STEP; num++;
                player.pbSmoothingMove = buffList[num]; buffList[num].ImageName = Database.SMOOTHING_MOVE; num++;
                player.pbFutureVision = buffList[num]; buffList[num].ImageName = Database.FUTURE_VISION; num++;
                player.pbReflexSpirit = buffList[num]; buffList[num].ImageName = Database.REFLEX_SPIRIT; num++;
                player.pbTrustSilence = buffList[num]; buffList[num].ImageName = Database.TRUST_SILENCE; num++;
                player.pbStanceOfMystic = buffList[num]; buffList[num].ImageName = Database.STANCE_OF_MYSTIC; num++;
                player.pbPreStunning = buffList[num]; buffList[num].ImageName = Database.EFFECT_PRESTUNNING; num++;
                player.pbBlinded = buffList[num]; buffList[num].ImageName = Database.EFFECT_BLINDED; num++;
                player.pbConcussiveHit = buffList[num]; buffList[num].ImageName = Database.CONCUSSIVE_HIT; num++;
                player.pbOnslaughtHit = buffList[num]; buffList[num].ImageName = Database.ONSLAUGHT_HIT; num++;
                player.pbImpulseHit = buffList[num]; buffList[num].ImageName = Database.IMPULSE_HIT; num++;
                player.pbExaltedField = buffList[num]; buffList[num].ImageName = Database.EXALTED_FIELD; num++;
                player.pbRisingAura = buffList[num]; buffList[num].ImageName = Database.RISING_AURA; num++;
                player.pbBlazingField = buffList[num]; buffList[num].ImageName = Database.BLAZING_FIELD; num++;
                player.pbPhantasmalWind = buffList[num]; buffList[num].ImageName = Database.PHANTASMAL_WIND; num++;
                player.pbParadoxImage = buffList[num]; buffList[num].ImageName = Database.PARADOX_IMAGE; num++;
                player.pbStaticBarrier = buffList[num]; buffList[num].ImageName = Database.STATIC_BARRIER; num++;
                player.pbAscensionAura = buffList[num]; buffList[num].ImageName = Database.ASCENSION_AURA; num++;
                player.pbNourishSense = buffList[num]; buffList[num].ImageName = Database.NOURISH_SENSE; num++;
                player.pbVigorSense = buffList[num]; buffList[num].ImageName = Database.VIGOR_SENSE; num++;
                player.pbOneAuthority = buffList[num]; buffList[num].ImageName = Database.ONE_AUTHORITY; num++;

                player.pbSyutyuDanzetsu = buffList[num]; buffList[num].ImageName = Database.ARCHETYPE_EIN; num++;
                player.pbJunkanSeiyaku = buffList[num]; buffList[num].ImageName = Database.ARCHETYPE_RANA; num++;

                player.pbHymnContract = buffList[num]; buffList[num].ImageName = Database.HYMN_CONTRACT; num++;
                player.pbSigilOfHomura = buffList[num]; buffList[num].ImageName = Database.SIGIL_OF_HOMURA; num++;
                player.pbAusterityMatrix = buffList[num]; buffList[num].ImageName = Database.AUSTERITY_MATRIX; num++;
                player.pbRedDragonWill = buffList[num]; buffList[num].ImageName = Database.RED_DRAGON_WILL; num++;
                player.pbBlueDragonWill = buffList[num]; buffList[num].ImageName = Database.BLUE_DRAGON_WILL; num++;
                player.pbEclipseEnd = buffList[num]; buffList[num].ImageName = Database.ECLIPSE_END; num++;
                player.pbTimeStop = buffList[num]; buffList[num].ImageName = Database.TIME_STOP; num++;
                player.pbSinFortune = buffList[num]; buffList[num].ImageName = Database.SIN_FORTUNE; num++;

                player.pbLightUp = buffList[num]; buffList[num].ImageName = Database.BUFF_LIGHT_UP; num++;
                player.pbLightDown = buffList[num]; buffList[num].ImageName = Database.BUFF_LIGHT_DOWN; num++;
                player.pbShadowUp = buffList[num]; buffList[num].ImageName = Database.BUFF_SHADOW_UP; num++;
                player.pbShadowDown = buffList[num]; buffList[num].ImageName = Database.BUFF_SHADOW_DOWN; num++;
                player.pbFireUp = buffList[num]; buffList[num].ImageName = Database.BUFF_FIRE_UP; num++;
                player.pbFireDown = buffList[num]; buffList[num].ImageName = Database.BUFF_FIRE_DOWN; num++;
                player.pbIceUp = buffList[num]; buffList[num].ImageName = Database.BUFF_ICE_UP; num++;
                player.pbIceDown = buffList[num]; buffList[num].ImageName = Database.BUFF_ICE_DOWN; num++;
                player.pbForceUp = buffList[num]; buffList[num].ImageName = Database.BUFF_FORCE_UP; num++;
                player.pbForceDown = buffList[num]; buffList[num].ImageName = Database.BUFF_FORCE_DOWN; num++;
                player.pbWillUp = buffList[num]; buffList[num].ImageName = Database.BUFF_WILL_UP; num++;
                player.pbWillDown = buffList[num]; buffList[num].ImageName = Database.BUFF_WILL_DOWN; num++;

                player.pbAfterReviveHalf = buffList[num]; buffList[num].ImageName = Database.BUFF_DANZAI_KAGO; num++;
                player.pbFireDamage2 = buffList[num]; buffList[num].ImageName = Database.BUFF_FIREDAMAGE2; num++;
                player.pbBlackMagic = buffList[num]; buffList[num].ImageName = Database.BUFF_BLACK_MAGIC; num++;
                player.pbChaosDesperate = buffList[num]; buffList[num].ImageName = Database.BUFF_CHAOS_DESPERATE; num++;

                player.pbFeltus = buffList[num]; buffList[num].ImageName = Database.BUFF_FELTUS; num++;
                player.pbJuzaPhantasmal = buffList[num]; buffList[num].ImageName = Database.BUFF_JUZA_PHANTASMAL; num++;
                player.pbEternalFateRing = buffList[num]; buffList[num].ImageName = Database.BUFF_ETERNAL_FATE_RING; num++;
                player.pbLightServant = buffList[num]; buffList[num].ImageName = Database.BUFF_LIGHT_SERVANT; num++;
                player.pbShadowServant = buffList[num]; buffList[num].ImageName = Database.BUFF_SHADOW_SERVANT; num++;
                player.pbAdilBlueBurn = buffList[num]; buffList[num].ImageName = Database.BUFF_ADIL_BLUE_BURN; num++;
                player.pbMazeCube = buffList[num]; buffList[num].ImageName = Database.BUFF_MAZE_CUBE; num++;
                player.pbShadowBible = buffList[num]; buffList[num].ImageName = Database.BUFF_SHADOW_BIBLE; num++;
                player.pbDetachmentOrb = buffList[num]; buffList[num].ImageName = Database.BUFF_DETACHMENT_ORB; num++;
                player.pbDevilSummonerTome = buffList[num]; buffList[num].ImageName = Database.BUFF_DEVIL_SUMMONER_TOME; num++;
                player.pbVoidHymnsonia = buffList[num]; buffList[num].ImageName = Database.BUFF_VOID_HYMNSONIA; num++;

                player.pbIchinaruHomura = buffList[num]; buffList[num].ImageName = Database.BUFF_ICHINARU_HOMURA; num++;
                player.pbAbyssFire = buffList[num]; buffList[num].ImageName = Database.BUFF_ABYSS_FIRE; num++;
                player.pbLightAndShadow = buffList[num]; buffList[num].ImageName = Database.BUFF_LIGHT_AND_SHADOW; num++;
                player.pbEternalDroplet = buffList[num]; buffList[num].ImageName = Database.BUFF_ETERNAL_DROPLET; num++;
                player.pbAusterityMatrixOmega = buffList[num]; buffList[num].ImageName = Database.BUFF_AUSTERITY_MATRIX_OMEGA; num++;
                player.pbVoiceOfAbyss = buffList[num]; buffList[num].ImageName = Database.BUFF_VOICE_OF_ABYSS; num++;
                player.pbAbyssWill = buffList[num]; buffList[num].ImageName = Database.BUFF_ABYSS_WILL; num++;
                player.pbTheAbyssWall = buffList[num]; buffList[num].ImageName = Database.BUFF_THE_ABYSS_WALL; num++;

                player.pbSagePotionMini = buffList[num]; buffList[num].ImageName = Database.BUFF_SAGE_POTION_MINI; num++;
                player.pbGenseiTaima = buffList[num]; buffList[num].ImageName = Database.BUFF_GENSEI_TAIMA; num++;
                player.pbShiningAether = buffList[num]; buffList[num].ImageName = Database.BUFF_SHINING_AETHER; num++;
                player.pbBlackElixir = buffList[num]; buffList[num].ImageName = Database.BUFF_BLACK_ELIXIR; num++;
                player.pbElementalSeal = buffList[num]; buffList[num].ImageName = Database.BUFF_ELEMENTAL_SEAL; num++;
                player.pbColoressAntidote = buffList[num]; buffList[num].ImageName = Database.BUFF_COLORESS_ANTIDOTE; num++;

                player.pbLifeCount = buffList[num]; buffList[num].ImageName = Database.BUFF_LIFE_COUNT; num++;
                player.pbChaoticSchema = buffList[num]; buffList[num].ImageName = Database.BUFF_CHAOTIC_SCHEMA; num++;

                // �o�^�𔽉f
                player.BuffElement = buffList;

                // �e�v���C���[�̃^�[�Q�b�g�I��
                player.Target = target;
                player.Target2 = player; // �����I���̓f�t�H���g�ł͎������g�Ƃ��Ă����B
                if ((player.Name == Database.ENEMY_SEA_STAR_KNIGHT_AEGIRU) ||
                    (player.Name == Database.ENEMY_SEA_STAR_KNIGHT_AMARA))
                {
                    player.Target2 = ec1;
                }

                // �e�v���C���[�̏����s����I��
                player.PA = PlayerAction.NormalAttack;

                // �e�v���C���[�̐퓬�o�[�̈ʒu
                if (DuelMode)
                {
                    player.BattleBarPos = 0;
                }
                else
                {
                    player.BattleBarPos = rand.Next(100, 400);
                    if (player.Name == Database.ENEMY_JELLY_EYE_DEEP_BLUE)
                    {
                        player.BattleBarPos = ec1.BattleBarPos + 250;
                        if (player.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH)
                        {
                            player.BattleBarPos -= Database.BASE_TIMER_BAR_LENGTH;
                        }
                    }

                    if (player.Name == Database.ENEMY_SEA_STAR_KNIGHT_AEGIRU)
                    {
                        player.BattleBarPos = ec1.BattleBarPos + 150;
                        if (player.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH)
                        {
                            player.BattleBarPos -= Database.BASE_TIMER_BAR_LENGTH;
                        }
                    }
                    if (player.Name == Database.ENEMY_SEA_STAR_KNIGHT_AMARA)
                    {
                        player.BattleBarPos = ec1.BattleBarPos + 300;
                        if (player.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH)
                        {
                            player.BattleBarPos -= Database.BASE_TIMER_BAR_LENGTH;
                        }
                    }
                }

                // �e�v���C���[�̐퓬�s������^�C�~���O�̐ݒ�i�G��p�j
                player.DecisionTiming = 250;

                // �e�v���C���[��\���\�ɂ���
                player.ActivateCharacter();

                // �e�v���C���[�̐퓬�ւ̎Q��
                ActiveList.Add(this.activatePlayerNumber, player);
                this.activatePlayerNumber++;

                // �e�v���C���[�̃X�L���J������
                if (!player.AvailableSkill)
                {
                    if (player.labelCurrentSkillPoint != null)
                    {
                        player.labelCurrentSkillPoint.Visible = false;
                    }
                }
                
                // �e�v���C���[�̖��@�J������
                if (!player.AvailableMana)
                {
                    if (player.labelCurrentManaPoint != null)
                    {
                        player.labelCurrentManaPoint.Visible = false;
                    }
                }

                // �e�v���C���[�̃C���X�^���g�R�}���h�̊J������
                if (!this.we.AvailableInstantCommand)
                {
                    if (player.labelCurrentInstantPoint != null)
                    {
                        player.labelCurrentInstantPoint.Visible = false;
                    }
                }
                if ((player.Name == Database.ENEMY_BOSS_KARAMITUKU_FLANSIS) ||
                    (player.Name == Database.ENEMY_BRILLIANT_SEA_PRINCE))
                {
                    player.labelCurrentInstantPoint.Visible = true;
                }

                // �������A���@�E�X�L�����Z�b�g�A�b�v
                // �v���C���[�X�L���E���@�K���ɉ����āA�A�N�V�����{�^����o�^
                UpdateBattleCommandSetting(player, action1, action2, action3, action4, action5, action6, action7, action8, action9, sorcery1, sorcery2, sorcery3, sorcery4, sorcery5, sorcery6, sorcery7, sorcery8, sorcery9);

                // �G���A���O�̐F�Ɗe�t�h�|�W�V�������Ĕz�u
                if (player == ec1 || player == ec2 || player == ec3)
                {
                    if (((TruthEnemyCharacter)player).Rare == TruthEnemyCharacter.RareString.Blue)
                    {
                        player.labelName.ForeColor = Color.Blue;
                    }
                    else if (((TruthEnemyCharacter)player).Rare == TruthEnemyCharacter.RareString.Red)
                    {
                        player.labelName.ForeColor = Color.Red;

                        if (player.Name == Database.ENEMY_SEA_STAR_KNIGHT_AEGIRU)
                        {
                            player.labelName.Location = new Point(496, 175);
                            player.ActionLabel.Location = new Point(503, 212);
                            player.labelCurrentInstantPoint.Size = new System.Drawing.Size(150, 15);
                            player.labelCurrentInstantPoint.Location = new Point(460, 235);
                        }
                        if (player.Name == Database.ENEMY_SEA_STAR_KNIGHT_AMARA)
                        {
                            player.labelName.Location = new Point(496, 260);
                            player.ActionLabel.Location = new Point(503, 300);
                            player.labelCurrentInstantPoint.Size = new System.Drawing.Size(150, 15);
                            player.labelCurrentInstantPoint.Location = new Point(460, 320);
                        }

                    }
                    else if (((TruthEnemyCharacter)player).Rare == TruthEnemyCharacter.RareString.Gold)
                    {
                        player.labelName.ForeColor = Color.DarkOrange;
                        player.labelCurrentInstantPoint.BackColor = Color.Gold;

                        // 640x480����
                        // �{�X��̏ꍇ�A�l�[�����x����BUFF�̕\���ꏊ��ύX���܂��B

                        //player.labelName.ForeColor = Color.DarkOrange;
                        //player.labelCurrentInstantPoint.BackColor = Color.Gold;

                        //if (player.Name == Database.ENEMY_BOSS_KARAMITUKU_FLANSIS)
                        //{
                        //    player.labelName.Text = "�y�P�K�̎��ҁz\r\n\r\n���݂��t�����V�X";
                        //}
                        //if (player.Name == Database.ENEMY_BOSS_LEVIATHAN)
                        //{
                        //    player.labelName.Text = "�y�Q�K�̎��ҁz\r\n\r\n��C�փ����B�A�T��";
                        //}

                        //player.MainObjectButton.Location = new Point(400, 182);
                        //player.labelLife.Location = new Point(510, 186);
                        //player.CriticalLabel.Location = new Point(393, 190);
                        //player.DamageLabel.Location = new Point(393, 213);
                        //player.ActionLabel.Location = new Point(503, 223);
                        //player.labelName.Location = new Point(430, 115);
                        //player.labelName.Size = new System.Drawing.Size(200, 100);
                        //player.labelName.Font = new System.Drawing.Font(player.labelName.Font.FontFamily, 14, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)128));
                        //player.labelCurrentInstantPoint.Location = new Point(400, 250);
                        //player.labelCurrentInstantPoint.Size = new System.Drawing.Size(200, 30);
                        //player.labelCurrentInstantPoint.Font = new Font(player.labelCurrentInstantPoint.Font.FontFamily, 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), GraphicsUnit.Point, ((byte)128));
                        //player.BuffPanel.Location = new Point(381, 300);

                        //if (player.Name == Database.ENEMY_JELLY_EYE_BRIGHT_RED)
                        //{
                        //    player.labelName.Location = new Point(430, 70);
                        //    player.labelLife.Location = new Point(514, 87);
                        //    player.MainObjectButton.Location = new Point(400, 89);
                        //    player.CriticalLabel.Location = new Point(393, 99);
                        //    player.DamageLabel.Location = new Point(393, 109);
                        //    player.ActionLabel.Location = new Point(503, 116);
                        //    player.labelCurrentInstantPoint.Location = new Point(400, 139);
                        //    player.BuffPanel.Location = new Point(390, 172); 
                        //}
                        //if (player.Name == Database.ENEMY_JELLY_EYE_DEEP_BLUE)
                        //{
                        //    player.labelName.Location = new Point(430, 207);
                        //    player.labelLife.Location = new Point(514, 228);
                        //    player.MainObjectButton.Location = new Point(400, 230);
                        //    player.CriticalLabel.Location = new Point(393, 240);
                        //    player.DamageLabel.Location = new Point(393, 250);
                        //    player.ActionLabel.Location = new Point(503, 257);
                        //    player.labelCurrentInstantPoint.Location = new Point(400, 280);
                        //    player.BuffPanel.Location = new Point(390, 310); 
                        //}

                        //if (player.Name == Database.ENEMY_SEA_STAR_ORIGIN_KING)
                        //{
                        //    player.labelName.Location = new Point(496, 80);
                        //    player.ActionLabel.Location = new Point(503, 128);
                        //    player.MainObjectButton.Location = new Point(400, 97);
                        //    player.CriticalLabel.Location = new Point(393, 102);
                        //    player.DamageLabel.Location = new Point(393, 125);
                        //    player.labelLife.Location = new Point(514, 102);
                        //    player.labelName.Size = new System.Drawing.Size(200, 100);
                        //    player.labelName.Font = new System.Drawing.Font(player.labelName.Font.FontFamily, 14, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)128));
                        //    player.labelCurrentInstantPoint.Location = new Point(460, 145);
                        //    player.labelCurrentInstantPoint.Size = new System.Drawing.Size(150, 20);
                        //    player.labelCurrentInstantPoint.Font = new Font(player.labelCurrentInstantPoint.Font.FontFamily, 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), GraphicsUnit.Point, ((byte)128));
                        //    player.BuffPanel.Location = new Point(377, 66);
                        //}

                        // 1024 x 768
                        player.MainObjectButton.Location         = new Point(TruthLayout.BOSS_LINE_LOC_X,   TruthLayout.BOSS_MAIN_OBJ_LOC_Y);
                        player.labelLife.Location                = new Point(TruthLayout.BOSS_STATUS_LOC_X, TruthLayout.BOSS_LIFE_LABEL_LOC_Y);
                        player.labelName.Location                = new Point(TruthLayout.BOSS_LINE_LOC_X,   TruthLayout.BOSS_NAME_LABEL_LOC_Y);
                        player.ActionLabel.Location              = new Point(TruthLayout.BOSS_STATUS_LOC_X, TruthLayout.BOSS_ACTION_LABEL_LOC_Y);
                        player.CriticalLabel.Location            = new Point(TruthLayout.BOSS_LINE_LOC_X,   TruthLayout.BOSS_CRITICAL_LABEL_LOC_Y);
                        player.DamageLabel.Location              = new Point(TruthLayout.BOSS_LINE_LOC_X,   TruthLayout.BOSS_DAMAGE_LABEL_LOC_Y);
                        player.labelCurrentInstantPoint.Location = new Point(TruthLayout.BOSS_LINE_LOC_X,   TruthLayout.BOSS_INSTANT_LABEL_LOC_Y);
                        player.BuffPanel.Location                = new Point(TruthLayout.BOSS_LINE_LOC_X,   TruthLayout.BOSS_BUFF_LOC_Y);
                        player.labelCurrentInstantPoint.Size = new System.Drawing.Size(TruthLayout.BOSS_INSTANT_LABEL_WIDTH, TruthLayout.BOSS_INSTANT_LABEL_HEIGHT);
                        player.labelCurrentInstantPoint.Font = new Font(player.labelCurrentInstantPoint.Font.FontFamily, 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), GraphicsUnit.Point, ((byte)128));
                        player.labelName.Font = new System.Drawing.Font(player.labelName.Font.FontFamily, 18, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)128));

                        if (player.Name == Database.ENEMY_BOSS_HOWLING_SEIZER)
                        {
                            player.labelName.Text = "�y�O�K�̎��ҁz\r\n\r\n����n�E�����O�E�V�[�U�[";
                        }
                        else if (player.Name == Database.ENEMY_BOSS_LEGIN_ARZE_3)
                        {
                            player.labelCurrentManaPoint.Location = new Point(TruthLayout.BOSS_LINE_LOC_X, TruthLayout.BOSS_MANA_LABEL_LOC_Y);
                            player.labelCurrentManaPoint.Size = new Size(TruthLayout.BOSS_MANA_LABEL_WIDTH, TruthLayout.BOSS_MANA_LABEL_HEIGHT);
                            player.labelCurrentManaPoint.Font = new Font(player.labelCurrentManaPoint.Font.FontFamily, 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), GraphicsUnit.Point, ((byte)128));
                        }
                        else if (player.Name == Database.ENEMY_BOSS_LEGIN_ARZE_1)
                        {
                            player.labelName.Text = "�y�l�K�̎��ҁz\r\n\r\n�ŉ����M�B���E�A�[�[�yᏋC�z";
                        }
                        else if (player.Name == Database.ENEMY_BOSS_LEGIN_ARZE_2)
                        {
                            player.labelName.Text = "�y�l�K�̎��ҁz\r\n\r\n�ŉ����M�B���E�A�[�[�y�����z";
                        }
                        else if (player.Name == Database.ENEMY_BOSS_LEGIN_ARZE_3)
                        {
                            player.labelName.Text = "�y�l�K�̎��ҁz\r\n\r\n�ŉ����M�B���E�A�[�[�y�[���z";
                        }
                        else if (player.Name == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
                        {
                            player.labelName.Text = "�y�܊K�̎��ҁz\r\n\r\n�x�@�z�@��";
                            //player.labelCurrentSkillPoint.Visible = false;
                            //player.labelCurrentManaPoint.Visible = false;
                        }
                    }
                    else if (((TruthEnemyCharacter)player).Rare == TruthEnemyCharacter.RareString.Purple)
                    {
                        player.labelName.ForeColor = Color.Purple;
                        player.labelName.Visible = false;
                        pbMatrixDragon.Visible = true;
                        pbMatrixDragon.Size = new System.Drawing.Size(250, 100);
                        pbMatrixDragon.SizeMode = PictureBoxSizeMode.StretchImage;
                        if (player.Name == Database.ENEMY_DRAGON_SOKUBAKU_BRIYARD)
                        {
                            pbMatrixDragon.Image = Image.FromFile(Database.BaseResourceFolder + Database.IMAGE_DRAGON_BRIYARD);
                        }
                        else if (player.Name == Database.ENEMY_DRAGON_TINKOU_DEEPSEA)
                        {
                            pbMatrixDragon.Image = Image.FromFile(Database.BaseResourceFolder + Database.IMAGE_DRAGON_DEEPSEA);
                        }
                        else if (player.Name == Database.ENEMY_DRAGON_DESOLATOR_AZOLD)
                        {
                            pbMatrixDragon.Image = Image.FromFile(Database.BaseResourceFolder + Database.IMAGE_DRAGON_AZOLD);
                        }
                        else if (player.Name == Database.ENEMY_DRAGON_IDEA_CAGE_ZEED)
                        {
                            pbMatrixDragon.Image = Image.FromFile(Database.BaseResourceFolder + Database.IMAGE_DRAGON_ZEED);
                        }
                        else if (player.Name == Database.ENEMY_DRAGON_ALAKH_VES_T_ETULA)
                        {
                            pbMatrixDragon.Image = Image.FromFile(Database.BaseResourceFolder + Database.IMAGE_DRAGON_ETULA);
                        }
                        pbMatrixDragon.Location = new Point(700, 150);
                        player.labelName.ForeColor = Color.DarkOrange;
                        this.cannotRunAway = true; 
                    }
                    else if (((TruthEnemyCharacter)player).Rare == TruthEnemyCharacter.RareString.Legendary)
                    {
                        player.BuffPanel.Location = new Point(663, 80);

                        player.labelCurrentSkillPoint.Location = new Point(700, 270);
                        player.labelCurrentSkillPoint.Size = new Size(300, 30);
                        player.labelCurrentSkillPoint.Font = new Font(player.labelCurrentSkillPoint.Font.FontFamily, 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), GraphicsUnit.Point, ((byte)128));

                        player.labelCurrentManaPoint.Location = new Point(700, 300);
                        player.labelCurrentManaPoint.Size = new Size(300, 30);
                        player.labelCurrentManaPoint.Font = new Font(player.labelCurrentManaPoint.Font.FontFamily, 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), GraphicsUnit.Point, ((byte)128));

                        player.labelCurrentInstantPoint.Location = new Point(700, 330);
                        player.labelCurrentInstantPoint.Size = new Size(300, 30);
                        player.labelCurrentInstantPoint.Font = new Font(player.labelCurrentInstantPoint.Font.FontFamily, 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), GraphicsUnit.Point, ((byte)128));

                        player.labelName.ForeColor = Color.OrangeRed;
                        player.labelName.Location = new Point(TruthLayout.BOSS_LINE_LOC_X, TruthLayout.LAST_BOSS_NAME_LABEL_LOC_Y);
                        player.labelName.Font = new System.Drawing.Font(player.labelName.Font.FontFamily, 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)128));

                        if (player.labelCurrentSpecialInstant != null)
                        {
                            player.labelCurrentSpecialInstant.Location = new Point(700, 460); // �y�x���z�Ȃ��R�U�O�ł̓��C�A�E�g����Ă��܂��̂��H
                            player.labelCurrentSpecialInstant.Size = new Size(300, 30);
                            player.labelCurrentSpecialInstant.Font = new Font(player.labelCurrentManaPoint.Font.FontFamily, 16, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), GraphicsUnit.Point, ((byte)128));
                        }
                    }
                }

                // �G���A����BUFF���Z�b�g�A�b�v
                if (player == ec1 && player.Name == Database.ENEMY_JELLY_EYE_BRIGHT_RED)
                {
                    player.CurrentResistFireUp = Database.INFINITY;
                    player.CurrentResistFireUpValue = 2000;
                    player.ActivateBuff(player.pbResistFireUp, Database.BaseResourceFolder + "ResistFireUp.bmp", Database.INFINITY);
                }
                if (player == ec2 && player.Name == Database.ENEMY_JELLY_EYE_DEEP_BLUE)
                {
                    player.CurrentResistIceUp = Database.INFINITY;
                    player.CurrentResistIceUpValue = 2000;
                    player.ActivateBuff(player.pbResistIceUp, Database.BaseResourceFolder + "ResistIceUp.bmp", Database.INFINITY);
                }

                // ����ł���ꍇ�A�O���[������
                if (player.Dead)
                {
                    player.DeadPlayer();
                }
            }
        }

        private void UpdateBattleCommandSetting(MainCharacter player, PictureBox action1, PictureBox action2, PictureBox action3, PictureBox action4, PictureBox action5, PictureBox action6, PictureBox action7, PictureBox action8, PictureBox action9,
            PictureBox sorcery1, PictureBox sorcery2, PictureBox sorcery3, PictureBox sorcery4, PictureBox sorcery5, PictureBox sorcery6, PictureBox sorcery7, PictureBox sorcery8, PictureBox sorcery9)
        {
            if (player == null) return;

            string fileExt = ".bmp";
            Bitmap sorceryMark = new Bitmap(Database.BaseResourceFolder + "sorcery_mark.bmp");
            Bitmap instantMark = new Bitmap(Database.BaseResourceFolder + "instant_mark.bmp");
            if (player == mc || player == sc || player == tc)
            {
                if (player.BattleActionCommand1 != "") { action1.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand1 + fileExt); if (TruthActionCommand.GetTimingType(player.BattleActionCommand1) == TruthActionCommand.TimingType.Sorcery) { sorcery1.Image = sorceryMark; } else { sorcery1.Image = instantMark; sorcery1.Update(); } }
                if (player.BattleActionCommand2 != "") { action2.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand2 + fileExt); if (TruthActionCommand.GetTimingType(player.BattleActionCommand2) == TruthActionCommand.TimingType.Sorcery) { sorcery2.Image = sorceryMark; } else { sorcery2.Image = instantMark; sorcery2.Update(); } }
                if (player.BattleActionCommand3 != "") { action3.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand3 + fileExt); if (TruthActionCommand.GetTimingType(player.BattleActionCommand3) == TruthActionCommand.TimingType.Sorcery) { sorcery3.Image = sorceryMark; } else { sorcery3.Image = instantMark; sorcery3.Update(); } }
                if (player.BattleActionCommand4 != "") { action4.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand4 + fileExt); if (TruthActionCommand.GetTimingType(player.BattleActionCommand4) == TruthActionCommand.TimingType.Sorcery) { sorcery4.Image = sorceryMark; } else { sorcery4.Image = instantMark; sorcery4.Update(); } }
                if (player.BattleActionCommand5 != "") { action5.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand5 + fileExt); if (TruthActionCommand.GetTimingType(player.BattleActionCommand5) == TruthActionCommand.TimingType.Sorcery) { sorcery5.Image = sorceryMark; } else { sorcery5.Image = instantMark; sorcery5.Update(); } }
                if (player.BattleActionCommand6 != "") { action6.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand6 + fileExt); if (TruthActionCommand.GetTimingType(player.BattleActionCommand6) == TruthActionCommand.TimingType.Sorcery) { sorcery6.Image = sorceryMark; } else { sorcery6.Image = instantMark; sorcery6.Update(); } }
                if (player.BattleActionCommand7 != "") { action7.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand7 + fileExt); if (TruthActionCommand.GetTimingType(player.BattleActionCommand7) == TruthActionCommand.TimingType.Sorcery) { sorcery7.Image = sorceryMark; } else { sorcery7.Image = instantMark; sorcery7.Update(); } }
                if (player.BattleActionCommand8 != "") { action8.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand8 + fileExt); if (TruthActionCommand.GetTimingType(player.BattleActionCommand8) == TruthActionCommand.TimingType.Sorcery) { sorcery8.Image = sorceryMark; } else { sorcery8.Image = instantMark; sorcery8.Update(); } }
                if (player.BattleActionCommand9 != "") { action9.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand9 + fileExt); if (TruthActionCommand.GetTimingType(player.BattleActionCommand9) == TruthActionCommand.TimingType.Sorcery) { sorcery9.Image = sorceryMark; } else { sorcery9.Image = instantMark; sorcery9.Update(); } }

                player.BattleActionCommandList = new string[CURRENT_ACTION_NUM];
                player.BattleActionCommandList[0] = player.BattleActionCommand1;
                player.BattleActionCommandList[1] = player.BattleActionCommand2;
                player.BattleActionCommandList[2] = player.BattleActionCommand3;
                player.BattleActionCommandList[3] = player.BattleActionCommand4;
                player.BattleActionCommandList[4] = player.BattleActionCommand5;
                player.BattleActionCommandList[5] = player.BattleActionCommand6;
                player.BattleActionCommandList[6] = player.BattleActionCommand7;
                player.BattleActionCommandList[7] = player.BattleActionCommand8;
                player.BattleActionCommandList[8] = player.BattleActionCommand9;
            }
        }

        private Bitmap SelectPlayerArrow(MainCharacter player)
        {
            if (player.Name == Database.EIN_WOLENCE) { return bmpPlayer1; }
            else if (player.Name == Database.RANA_AMILIA) { return bmpPlayer2; }
            else if (player.Name == Database.OL_LANDIS) { return bmpPlayer3; }
            else if (player.Name == Database.SINIKIA_KAHLHANZ) { return bmpPlayer4; }
            else { return bmpPlayer1; }
        }
        DirectInput directInput;
        Keyboard device;
        const int PARTY_POSITION_1 = 85;
        const int PARTY_POSITION_21 = 30;
        const int PARTY_POSITION_22 = 50;
        Image[] imageSandglass = null;
        const int SANDGLASS_NUM = 8;
        Image[] imageAnimeSandGlass = null;
        const int ANIMESANDGLASS_NUM = 25;
        private void TruthBattleEnemy_Load(object sender, EventArgs e)
        {
            this.directInput = new DirectInput();
            Guid keyGuid = Guid.Empty;
            foreach(var g in this.directInput.GetDevices(DeviceType.Keyboard, DeviceEnumerationFlags.AllDevices))
            {
                keyGuid = g.InstanceGuid;
                break;
            }
            this.device = new Keyboard(this.directInput);
            this.device.Acquire(); // �L�[���͊J�n

            this.imageSandglass = new Image[SANDGLASS_NUM];
            for (int ii = 0; ii < SANDGLASS_NUM; ii++)
            {
                this.imageSandglass[ii] = Image.FromFile(Database.BaseResourceFolder + "SandGlass" + ii.ToString() + ".bmp");
            }
            this.pbSandglass.Image = this.imageSandglass[0];

            this.imageAnimeSandGlass = new Image[ANIMESANDGLASS_NUM];
            for (int ii = 0; ii < ANIMESANDGLASS_NUM; ii++)
            {
                this.imageAnimeSandGlass[ii] = Image.FromFile(Database.BaseResourceFolder + "AnimeSandGlass" + ii.ToString() + ".bmp");
            }
            this.pbAnimeSandGlass.Image = this.imageAnimeSandGlass[0];

            if (this.DuelMode)
            {
                RunAwayButton.Text = "�~�Q����";
                BattleStart.Text = "DUEL�J�n";
                battleSpeedBar.Enabled = false;
            }

            // ������
            if (mc != null)
            {
                ActivateSomeCharacter(mc, ec1, nameLabel1, lifeLabel1, null, currentSkillPoint1, null, currentManaPoint1, currentInstantPoint1, null, ActionButton11, ActionButton12, ActionButton13, ActionButton14, ActionButton15, ActionButton16, ActionButton17, ActionButton18, ActionButton19, playerActionLabel1, BuffPanel1, buttonTargetPlayer1, mc.PlayerBattleColor, pbPlayerTargetTarget1, SelectPlayerArrow(mc), null, null, labelDamage1, labelCritical1, pbBuffPlayer1, keyNum1_1, keyNum1_2, keyNum1_3, keyNum1_4, keyNum1_5, keyNum1_6, keyNum1_7, keyNum1_8, keyNum1_9, IsSorcery11, IsSorcery12, IsSorcery13, IsSorcery14, IsSorcery15, IsSorcery16, IsSorcery17, IsSorcery18, IsSorcery19);
            }

            if (sc != null)
            {
                ActivateSomeCharacter(sc, ec1, nameLabel2, lifeLabel2, null, currentSkillPoint2, null, currentManaPoint2, currentInstantPoint2, null, ActionButton21, ActionButton22, ActionButton23, ActionButton24, ActionButton25, ActionButton26, ActionButton27, ActionButton28, ActionButton29, playerActionLabel2, BuffPanel2, buttonTargetPlayer2, sc.PlayerBattleColor, pbPlayerTargetTarget2, SelectPlayerArrow(sc), null, null, labelDamage2, labelCritical2, pbBuffPlayer2, keyNum2_1, keyNum2_2, keyNum2_3, keyNum2_4, keyNum2_5, keyNum2_6, keyNum2_7, keyNum2_8, keyNum2_9, IsSorcery21, IsSorcery22, IsSorcery23, IsSorcery24, IsSorcery25, IsSorcery26, IsSorcery27, IsSorcery28, IsSorcery29);
            }

            if (tc != null)
            {
                ActivateSomeCharacter(tc, ec1, nameLabel3, lifeLabel3, null, currentSkillPoint3, null, currentManaPoint3, currentInstantPoint3, null, ActionButton31, ActionButton32, ActionButton33, ActionButton34, ActionButton35, ActionButton36, ActionButton37, ActionButton38, ActionButton39, playerActionLabel3, BuffPanel3, buttonTargetPlayer3, tc.PlayerBattleColor, pbPlayerTargetTarget3, SelectPlayerArrow(tc), null, null, labelDamage3, labelCritical3, pbBuffPlayer3, keyNum3_1, keyNum3_2, keyNum3_3, keyNum3_4, keyNum3_5, keyNum3_6, keyNum3_7, keyNum3_8, keyNum3_9, IsSorcery31, IsSorcery32, IsSorcery33, IsSorcery34, IsSorcery35, IsSorcery36, IsSorcery37, IsSorcery38, IsSorcery39);
            }

            // �G��
            if (this.DuelMode)
            {
                Label specialInstant = null;
                if (((TruthEnemyCharacter)ec1).Area == TruthEnemyCharacter.MonsterArea.LastBoss) 
                {
                    specialInstant = this.labelSpecialInstant;
                }

                ActivateSomeCharacter(ec1, mc, enemyNameLabel1, lblLifeEnemy1, null, currentEnemySkillPoint1, null, currentEnemyManaPoint1, currentEnemyInstantPoint1, specialInstant, null, null, null, null, null, null, null, null, null, enemyActionLabel1, PanelBuffEnemy1, buttonTargetEnemy1, Color.DarkRed, pbEnemyTargetTarget1, bmpEnemy1, bmpShadowEnemy1_2, bmpShadowEnemy1_3, labelEnemyDamage1, labelEnemyCritical1, pbBuffEnemy1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
                ActivateSomeCharacter(ec2, mc, enemyNameLabel2, lblLifeEnemy2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, enemyActionLabel2, PanelBuffEnemy2, buttonTargetEnemy2, Color.DarkGoldenrod, pbEnemyTargetTarget2, bmpEnemy2, null, null, labelEnemyDamage2, labelEnemyCritical2, pbBuffEnemy2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
                ActivateSomeCharacter(ec3, mc, enemyNameLabel3, lblLifeEnemy3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, enemyActionLabel3, PanelBuffEnemy3, buttonTargetEnemy3, Color.DarkOliveGreen, pbEnemyTargetTarget3, bmpEnemy3, null, null, labelEnemyDamage2, labelEnemyCritical2, pbBuffEnemy3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
            }
            else
            {
                MainCharacter target = mc;

                if (ec1 != null)
                {
                    target = ec1.Targetting(mc, sc, tc);

                    Label currentLabel1 = null;
                    Label enemyManaLabel = null;
                    if ((ec1.Rare == TruthEnemyCharacter.RareString.Gold) ||
                        (ec1.Rare == TruthEnemyCharacter.RareString.Red && ec1.Name == Database.ENEMY_SEA_STAR_KNIGHT_AEGIRU) ||
                        (ec1.Rare == TruthEnemyCharacter.RareString.Red && ec1.Name == Database.ENEMY_SEA_STAR_KNIGHT_AMARA))
                    {
                        currentLabel1 = currentEnemyInstantPoint1;
                    }
                    if (ec1.Rare == TruthEnemyCharacter.RareString.Gold && ec1.Name == Database.ENEMY_BOSS_LEGIN_ARZE_3)
                    {
                        enemyManaLabel = currentEnemyManaPoint1;
                    }
                    ActivateSomeCharacter(ec1, target, enemyNameLabel1, lblLifeEnemy1, null, null, null, enemyManaLabel, currentLabel1, null, null, null, null, null, null, null, null, null, null, enemyActionLabel1, PanelBuffEnemy1, buttonTargetEnemy1, Color.DarkRed, pbEnemyTargetTarget1, bmpEnemy1, null, null, labelEnemyDamage1, labelEnemyCritical1, pbBuffEnemy1, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
                }

                if (ec2 != null)
                {
                    target = ec2.Targetting(mc, sc, tc);

                    Label currentLabel2 = null;
                    if ((ec2.Rare == TruthEnemyCharacter.RareString.Gold) ||
                        (ec2.Rare == TruthEnemyCharacter.RareString.Red && ec2.Name == Database.ENEMY_SEA_STAR_KNIGHT_AEGIRU) ||
                        (ec2.Rare == TruthEnemyCharacter.RareString.Red && ec2.Name == Database.ENEMY_SEA_STAR_KNIGHT_AMARA))
                    {
                        currentLabel2 = currentEnemyInstantPoint2;
                    }
                    ActivateSomeCharacter(ec2, target, enemyNameLabel2, lblLifeEnemy2, null, null, null, null, currentLabel2, null, null, null, null, null, null, null, null, null, null, enemyActionLabel2, PanelBuffEnemy2, buttonTargetEnemy2, Color.DarkGoldenrod, pbEnemyTargetTarget2, bmpEnemy2, null, null, labelEnemyDamage2, labelEnemyCritical2, pbBuffEnemy2, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
                }

                if (ec3 != null)
                {
                    target = ec3.Targetting(mc, sc, tc);

                    Label currentLabel3 = null;
                    if ((ec3.Rare == TruthEnemyCharacter.RareString.Gold) ||
                        (ec3.Rare == TruthEnemyCharacter.RareString.Red && ec3.Name == Database.ENEMY_SEA_STAR_KNIGHT_AEGIRU) ||
                        (ec3.Rare == TruthEnemyCharacter.RareString.Red && ec3.Name == Database.ENEMY_SEA_STAR_KNIGHT_AMARA))
                    {
                        currentLabel3 = currentEnemyInstantPoint3;
                    }
                    ActivateSomeCharacter(ec3, target, enemyNameLabel3, lblLifeEnemy3, null, null, null, null, currentLabel3, null, null, null, null, null, null, null, null, null, null, enemyActionLabel3, PanelBuffEnemy3, buttonTargetEnemy3, Color.DarkOliveGreen, pbEnemyTargetTarget3, bmpEnemy3, null, null, labelEnemyDamage3, labelEnemyCritical3, pbBuffEnemy3, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null);
                }
            }

            // �p�[�e�B�Ґ��͈ȉ��A�����ꂩ�ɊY�����邱�ƂƂȂ�B�����ɉ����āA��ʃ��C�A�E�g��K�X�ύX����B
            // DuelMode (mc vs ec1)
            // [mc] vs [ec1]�@�@�@�@�@     (�ʏ�퓬)

            // [mc] vs [ec1 ec2] �@�@�@    (�ʏ�퓬�j�i���^�p�͂Ȃ��j
            // [mc] vs [ec1 ec2 ec3] �@    (�ʏ�퓬�j�i���^�p�͂Ȃ��j

            // [mc sc] vs [ec1]  �@�@�@    (�{�X��)
            // [mc sc] vs [ec1 ec2]        (�ʏ�퓬�j
            // [mc sc] vs [ec1 ec2 ec3]    (�ʏ�퓬�j�i���^�p�͂Ȃ��j

            // [mc sc tc] vs [ec1]         (�{�X��)
            // [mc sc tc] vs [ec1 ec2]     (�ʏ�퓬�j
            // [mc sc tc] vs [ec1 ec2 ec3] (�ʏ�퓬�j�i���^�p�͂Ȃ��j

            // �����p�[�e�B�̃��C�A�E�g�z�u
            List<MainCharacter> group = new List<MainCharacter>();
            if (true && we.AvailableFirstCharacter && mc != null) group.Add(mc); // Duel���[�h�Ɉˑ������P�l�ڂ͔z�u����B
            if (!this.DuelMode && we.AvailableSecondCharacter && sc != null) group.Add(sc);
            if (!this.DuelMode && we.AvailableThirdCharacter && tc != null) group.Add(tc);

            if (group.Count == 1)
            {
                SetupPartyLayout(group, PARTY_POSITION_1, 0, 0);
            }
            else if (group.Count == 2)
            {
                SetupPartyLayout(group, PARTY_POSITION_21, PARTY_POSITION_22, 0);
            }
            else
            {
                SetupPartyLayout(group, 0, 0, 0); // �p�[�e�B�R�l�̏ꍇ�A�f�U�C�i�f�t�H���g��OK
            }

            // �G�p�[�e�B�̃��C�A�E�g�z�u
            List<MainCharacter> groupE = new List<MainCharacter>();
            if (true && we.AvailableFirstCharacter && ec1 != null) groupE.Add(ec1); // Duel���[�h�Ɉˑ������P�l�ڂ͔z�u����B
            if (!this.DuelMode && we.AvailableSecondCharacter && ec2 != null) groupE.Add(ec2);
            if (!this.DuelMode && we.AvailableThirdCharacter && ec3 != null) groupE.Add(ec3);

            if (groupE.Count == 1)
            {
                if (ec1.Rare != TruthEnemyCharacter.RareString.Gold)
                {
                    SetupPartyLayout(groupE, 85, 0, 0);
                }
            }
            else if (groupE.Count == 2)
            {
                if (ec1.Rare != TruthEnemyCharacter.RareString.Gold)
                {
                    SetupPartyLayout(groupE, 30, 50, 0);
                }
            }
            else
            {
                SetupPartyLayout(groupE, 0, 0, 0); // �p�[�e�B�R�l�̏ꍇ�A�f�U�C�i�f�t�H���g��OK
            }

            // DUEL���[�h���A���C�t�E�X�L���E�}�i�|�C���g��S���ɂ��܂��B
            if (this.DuelMode)
            {
                for (int ii = 0; ii < ActiveList.Count; ii++)
                {
                    ActiveList[ii].CurrentLife = ActiveList[ii].MaxLife;
                    UpdateLife(ActiveList[ii], 0, false, false, 0, false);
                    ActiveList[ii].CurrentSkillPoint = ActiveList[ii].MaxSkillPoint;
                    UpdateSkillPoint(ActiveList[ii]);
                    ActiveList[ii].CurrentMana = ActiveList[ii].MaxMana;
                    UpdateMana(ActiveList[ii], 0, false, false, 0);
                }
            }

            // �A�C�e���E�o�g���ݒ�E������A�C�R�����Z�b�g
            UseItemButton.Image = Image.FromFile(Database.BaseResourceFolder + "UseItemButton.bmp");
            BattleSettingButton.Image = Image.FromFile(Database.BaseResourceFolder + "BattleSettingButton.bmp");
            RunAwayButton.Image = Image.FromFile(Database.BaseResourceFolder + "RunAwayButton.bmp");

            BattleSettingButton.Visible = we.AvailableBattleSettingMenu;

            // �o�g���J�n�����X�^�[�g�^�C�}�[���ғ�
            if (this.DuelMode)
            {
                this.timerBattleStart.Start();
            }
        }

        private void SetupPartyLayout(List<MainCharacter> group, int moveY1, int moveY2, int moveY3)
        {
            for (int ii = 0; ii < group.Count; ii++)
            {
                int adjust = 0;
                if (ii == 0) adjust = moveY1;
                if (ii == 1) adjust = moveY2;
                if (ii == 2) adjust = moveY3;

                if (group[ii].ActionKeyNum1 != null) group[ii].ActionKeyNum1.Location = new Point(group[ii].ActionKeyNum1.Location.X, group[ii].ActionKeyNum1.Location.Y + adjust);
                if (group[ii].ActionKeyNum2 != null) group[ii].ActionKeyNum2.Location = new Point(group[ii].ActionKeyNum2.Location.X, group[ii].ActionKeyNum2.Location.Y + adjust);
                if (group[ii].ActionKeyNum3 != null) group[ii].ActionKeyNum3.Location = new Point(group[ii].ActionKeyNum3.Location.X, group[ii].ActionKeyNum3.Location.Y + adjust);
                if (group[ii].ActionKeyNum4 != null) group[ii].ActionKeyNum4.Location = new Point(group[ii].ActionKeyNum4.Location.X, group[ii].ActionKeyNum4.Location.Y + adjust);
                if (group[ii].ActionKeyNum5 != null) group[ii].ActionKeyNum5.Location = new Point(group[ii].ActionKeyNum5.Location.X, group[ii].ActionKeyNum5.Location.Y + adjust);
                if (group[ii].ActionKeyNum6 != null) group[ii].ActionKeyNum6.Location = new Point(group[ii].ActionKeyNum6.Location.X, group[ii].ActionKeyNum6.Location.Y + adjust);
                if (group[ii].ActionKeyNum7 != null) group[ii].ActionKeyNum7.Location = new Point(group[ii].ActionKeyNum7.Location.X, group[ii].ActionKeyNum7.Location.Y + adjust);
                if (group[ii].ActionKeyNum8 != null) group[ii].ActionKeyNum8.Location = new Point(group[ii].ActionKeyNum8.Location.X, group[ii].ActionKeyNum8.Location.Y + adjust);
                if (group[ii].ActionKeyNum9 != null) group[ii].ActionKeyNum9.Location = new Point(group[ii].ActionKeyNum9.Location.X, group[ii].ActionKeyNum9.Location.Y + adjust);

                if (group[ii].IsSorceryMark1 != null) group[ii].IsSorceryMark1.Location = new Point(group[ii].IsSorceryMark1.Location.X, group[ii].IsSorceryMark1.Location.Y + adjust);
                if (group[ii].IsSorceryMark2 != null) group[ii].IsSorceryMark2.Location = new Point(group[ii].IsSorceryMark2.Location.X, group[ii].IsSorceryMark2.Location.Y + adjust);
                if (group[ii].IsSorceryMark3 != null) group[ii].IsSorceryMark3.Location = new Point(group[ii].IsSorceryMark3.Location.X, group[ii].IsSorceryMark3.Location.Y + adjust);
                if (group[ii].IsSorceryMark4 != null) group[ii].IsSorceryMark4.Location = new Point(group[ii].IsSorceryMark4.Location.X, group[ii].IsSorceryMark4.Location.Y + adjust);
                if (group[ii].IsSorceryMark5 != null) group[ii].IsSorceryMark5.Location = new Point(group[ii].IsSorceryMark5.Location.X, group[ii].IsSorceryMark5.Location.Y + adjust);
                if (group[ii].IsSorceryMark6 != null) group[ii].IsSorceryMark6.Location = new Point(group[ii].IsSorceryMark6.Location.X, group[ii].IsSorceryMark6.Location.Y + adjust);
                if (group[ii].IsSorceryMark7 != null) group[ii].IsSorceryMark7.Location = new Point(group[ii].IsSorceryMark7.Location.X, group[ii].IsSorceryMark7.Location.Y + adjust);
                if (group[ii].IsSorceryMark8 != null) group[ii].IsSorceryMark8.Location = new Point(group[ii].IsSorceryMark8.Location.X, group[ii].IsSorceryMark8.Location.Y + adjust);
                if (group[ii].IsSorceryMark9 != null) group[ii].IsSorceryMark9.Location = new Point(group[ii].IsSorceryMark9.Location.X, group[ii].IsSorceryMark9.Location.Y + adjust);

                if (group[ii].ActionButton1 != null) group[ii].ActionButton1.Location = new Point(group[ii].ActionButton1.Location.X, group[ii].ActionButton1.Location.Y + adjust);
                if (group[ii].ActionButton2 != null) group[ii].ActionButton2.Location = new Point(group[ii].ActionButton2.Location.X, group[ii].ActionButton2.Location.Y + adjust);
                if (group[ii].ActionButton3 != null) group[ii].ActionButton3.Location = new Point(group[ii].ActionButton3.Location.X, group[ii].ActionButton3.Location.Y + adjust);
                if (group[ii].ActionButton4 != null) group[ii].ActionButton4.Location = new Point(group[ii].ActionButton4.Location.X, group[ii].ActionButton4.Location.Y + adjust);
                if (group[ii].ActionButton5 != null) group[ii].ActionButton5.Location = new Point(group[ii].ActionButton5.Location.X, group[ii].ActionButton5.Location.Y + adjust);
                if (group[ii].ActionButton6 != null) group[ii].ActionButton6.Location = new Point(group[ii].ActionButton6.Location.X, group[ii].ActionButton6.Location.Y + adjust);
                if (group[ii].ActionButton7 != null) group[ii].ActionButton7.Location = new Point(group[ii].ActionButton7.Location.X, group[ii].ActionButton7.Location.Y + adjust);
                if (group[ii].ActionButton8 != null) group[ii].ActionButton8.Location = new Point(group[ii].ActionButton8.Location.X, group[ii].ActionButton8.Location.Y + adjust);
                if (group[ii].ActionButton9 != null) group[ii].ActionButton9.Location = new Point(group[ii].ActionButton9.Location.X, group[ii].ActionButton9.Location.Y + adjust);

                if (group[ii].labelCurrentManaPoint != null) group[ii].labelCurrentManaPoint.Location = new Point(group[ii].labelCurrentManaPoint.Location.X, group[ii].labelCurrentManaPoint.Location.Y + adjust);
                if (group[ii].labelCurrentSkillPoint != null) group[ii].labelCurrentSkillPoint.Location = new Point(group[ii].labelCurrentSkillPoint.Location.X, group[ii].labelCurrentSkillPoint.Location.Y + adjust);
                if (group[ii].labelCurrentInstantPoint != null) group[ii].labelCurrentInstantPoint.Location = new Point(group[ii].labelCurrentInstantPoint.Location.X, group[ii].labelCurrentInstantPoint.Location.Y + adjust);

                if (group[ii].labelName != null) group[ii].labelName.Location = new Point(group[ii].labelName.Location.X, group[ii].labelName.Location.Y + adjust);
                if (group[ii].labelLife != null) group[ii].labelLife.Location = new Point(group[ii].labelLife.Location.X, group[ii].labelLife.Location.Y + adjust);
                if (group[ii].ActionLabel != null) group[ii].ActionLabel.Location = new Point(group[ii].ActionLabel.Location.X, group[ii].ActionLabel.Location.Y + adjust);
                if (group[ii].MainObjectButton != null) group[ii].MainObjectButton.Location = new Point(group[ii].MainObjectButton.Location.X, group[ii].MainObjectButton.Location.Y + adjust);
                if (group[ii].BuffPanel != null) group[ii].BuffPanel.Location = new Point(group[ii].BuffPanel.Location.X, group[ii].BuffPanel.Location.Y + adjust);

                if (group[ii].DamageLabel != null) group[ii].DamageLabel.Location = new Point(group[ii].DamageLabel.Location.X, group[ii].DamageLabel.Location.Y + adjust);
                if (group[ii].CriticalLabel != null) group[ii].CriticalLabel.Location = new Point(group[ii].CriticalLabel.Location.X, group[ii].CriticalLabel.Location.Y + adjust);
            }
        }
    
        private void BattleStart_Click(object sender, EventArgs e)
        {
            if (th == null)
            {
                timerBattleStart.Stop();
                if (this.DuelMode)
                {
                    BattleStart.Text = "DUEL���E�E�E";
                    BattleStart.Enabled = false;
                }
                else
                {
                    BattleStart.Text = "�퓬���E�E�E";
                }
                this.BattleMenuPanel.Visible = false;
                th = new Thread(new System.Threading.ThreadStart(BattleLoop));
                th.IsBackground = true;
                th.Start();
            }
            else
            {
                if ((BattleStart.Text == "�퓬���E�E�E") ||
                    (BattleStart.Text == "DUEL���E�E�E"))
                {
                    if (this.DuelMode)
                    {
                        // DUEL�ł͓r����U��~�͏o���Ȃ����Ƃ���B
                    }
                    else
                    {
                        BattleStart.Text = "�퓬��~";
                        this.BattleMenuPanel.Visible = true;
                    }
                    tempStopFlag = true;
                }
                else
                {
                    timerBattleStart.Stop();
                    BattleStart.Text = "�퓬���E�E�E";
                    tempStopFlag = false;
                    this.BattleMenuPanel.Visible = false;
                }
            }
        }

        int TIMER_SPEED = 10;
        int BattleTimeCounter = Database.BASE_TIMER_BAR_LENGTH / 2;
        int BattleTurnCount = 0;

        bool BreakOn_StanceOfFlow = false;
        bool StayOn_StanceOfFlow = false;

        bool endBattleForMatrixDragonEnd = false; // �x�z����b�I�����ɐ퓬�I��������t���O

        private void GoToTimeStopColor(MainCharacter player)
        {
            if (player.CurrentLife >= player.MaxLife)
            {
                player.labelLife.ForeColor = Color.LightGreen;
            }
            else
            {
                player.labelLife.ForeColor = Color.White;
            }
        }
        private void BackToNormalColor(MainCharacter player)
        {
            if (IsPlayerEnemy(player))
            {
                if (((TruthEnemyCharacter)player).Rare == TruthEnemyCharacter.RareString.Gold)
                {
                    player.labelName.ForeColor = Color.DarkOrange;
                    player.labelCurrentInstantPoint.BackColor = Color.Gold;
                }
                else
                {
                    player.labelName.ForeColor = Color.Black;
                }
            }
            else
            {
                player.labelName.ForeColor = Color.Black;
            }

            player.ActionLabel.ForeColor = Color.Black;
            player.CriticalLabel.ForeColor = Color.Black;
            player.DamageLabel.ForeColor = Color.Black;
            player.BuffPanel.BackColor = Color.GhostWhite;

            if (player.CurrentLife >= player.MaxLife)
            {
                player.labelLife.ForeColor = Color.Green;
            }
            else
            {
                player.labelLife.ForeColor = Color.Black;
            }
        }

        bool isEscDown = false;
        private void DetectKeyPressed()
        {
            try
            {
                //����������͂̌��m���J�n
                while (!endFlag)
                {
                    //���݂̓��͏󋵂��擾����
                    KeyboardState state = this.device.GetCurrentState();
                    List<Key> keys = state.PressedKeys;
                    if (keys.Count <= 0)
                    {
                        isEscDown = false;
                    }
                    else if (keys.Count > 0)
                    {
                        bool detectShift = false;
                        if (keys.Contains(Key.LeftShift) || keys.Contains(Key.RightShift)) { detectShift = true; }
                        if (keys.Contains(Key.Escape) == false) { isEscDown = false; }

                        switch (keys[0])
                        {
                            case Key.D1:
                                if (this.ActionButton11.Enabled && this.ActionButton11.Visible)
                                {
                                    ActionCommand(null, detectShift, mc, mc.BattleActionCommand1, 0);
                                }
                                break;
                            case Key.D2:
                                if (this.ActionButton12.Enabled && this.ActionButton12.Visible)
                                {
                                    ActionCommand(null, detectShift, mc, mc.BattleActionCommand2, 1);
                                }
                                break;
                            case Key.D3:
                                if (this.ActionButton13.Enabled && this.ActionButton13.Visible)
                                {
                                    ActionCommand(null, detectShift, mc, mc.BattleActionCommand3, 2);
                                }
                                break;
                            case Key.D4:
                                if (this.ActionButton14.Enabled && this.ActionButton14.Visible)
                                {
                                    ActionCommand(null, detectShift, mc, mc.BattleActionCommand4, 3);
                                }
                                break;
                            case Key.D5:
                                if (this.ActionButton15.Enabled && this.ActionButton15.Visible)
                                {
                                    ActionCommand(null, detectShift, mc, mc.BattleActionCommand5, 4);
                                }
                                break;
                            case Key.D6:
                                if (this.ActionButton16.Enabled && this.ActionButton16.Visible)
                                {
                                    ActionCommand(null, detectShift, mc, mc.BattleActionCommand6, 5);
                                }
                                break;
                            case Key.D7:
                                if (this.ActionButton17.Enabled && this.ActionButton17.Visible)
                                {
                                    ActionCommand(null, detectShift, mc, mc.BattleActionCommand7, 6);
                                }
                                break;
                            case Key.D8:
                                if (this.ActionButton18.Enabled && this.ActionButton18.Visible)
                                {
                                    ActionCommand(null, detectShift, mc, mc.BattleActionCommand8, 7);
                                }
                                break;
                            case Key.D9:
                                if (this.ActionButton19.Enabled && this.ActionButton19.Visible)
                                {
                                    ActionCommand(null, detectShift, mc, mc.BattleActionCommand9, 8);
                                }
                                break;

                            case Key.Q:
                                if (this.ActionButton21.Enabled && this.ActionButton21.Visible)
                                {
                                    ActionCommand(null, detectShift, sc, sc.BattleActionCommand1, 0);
                                }
                                break;
                            case Key.W:
                                if (this.ActionButton22.Enabled && this.ActionButton22.Visible)
                                {
                                    ActionCommand(null, detectShift, sc, sc.BattleActionCommand2, 1);
                                }
                                break;
                            case Key.E:
                                if (this.ActionButton23.Enabled && this.ActionButton23.Visible)
                                {
                                    ActionCommand(null, detectShift, sc, sc.BattleActionCommand3, 2);
                                }
                                break;
                            case Key.R:
                                if (this.ActionButton24.Enabled && this.ActionButton24.Visible)
                                {
                                    ActionCommand(null, detectShift, sc, sc.BattleActionCommand4, 3);
                                }
                                break;
                            case Key.T:
                                if (this.ActionButton25.Enabled && this.ActionButton25.Visible)
                                {
                                    ActionCommand(null, detectShift, sc, sc.BattleActionCommand5, 4);
                                }
                                break;
                            case Key.Y:
                                if (this.ActionButton26.Enabled && this.ActionButton26.Visible)
                                {
                                    ActionCommand(null, detectShift, sc, sc.BattleActionCommand6, 5);
                                }
                                break;
                            case Key.U:
                                if (this.ActionButton27.Enabled && this.ActionButton27.Visible)
                                {
                                    ActionCommand(null, detectShift, sc, sc.BattleActionCommand7, 6);
                                }
                                break;
                            case Key.I:
                                if (this.ActionButton28.Enabled && this.ActionButton28.Visible)
                                {
                                    ActionCommand(null, detectShift, sc, sc.BattleActionCommand8, 7);
                                }
                                break;
                            case Key.O:
                                if (this.ActionButton29.Enabled && this.ActionButton29.Visible)
                                {
                                    ActionCommand(null, detectShift, sc, sc.BattleActionCommand9, 8);
                                }
                                break;

                            case Key.A:
                                if (this.ActionButton31.Enabled && this.ActionButton31.Visible)
                                {
                                    ActionCommand(null, detectShift, tc, tc.BattleActionCommand1, 0);
                                }
                                break;
                            case Key.S:
                                if (this.ActionButton32.Enabled && this.ActionButton32.Visible)
                                {
                                    ActionCommand(null, detectShift, tc, tc.BattleActionCommand2, 1);
                                }
                                break;
                            case Key.D:
                                if (this.ActionButton33.Enabled && this.ActionButton33.Visible)
                                {
                                    ActionCommand(null, detectShift, tc, tc.BattleActionCommand3, 2);
                                }
                                break;
                            case Key.F:
                                if (this.ActionButton34.Enabled && this.ActionButton34.Visible)
                                {
                                    ActionCommand(null, detectShift, tc, tc.BattleActionCommand4, 3);
                                }
                                break;
                            case Key.G:
                                if (this.ActionButton35.Enabled && this.ActionButton35.Visible)
                                {
                                    ActionCommand(null, detectShift, tc, tc.BattleActionCommand5, 4);
                                }
                                break;
                            case Key.H:
                                if (this.ActionButton36.Enabled && this.ActionButton36.Visible)
                                {
                                    ActionCommand(null, detectShift, tc, tc.BattleActionCommand6, 5);
                                }
                                break;
                            case Key.J:
                                if (this.ActionButton37.Enabled && this.ActionButton37.Visible)
                                {
                                    ActionCommand(null, detectShift, tc, tc.BattleActionCommand7, 6);
                                }
                                break;
                            case Key.K:
                                if (this.ActionButton38.Enabled && this.ActionButton38.Visible)
                                {
                                    ActionCommand(null, detectShift, tc, tc.BattleActionCommand8, 7);
                                }
                                break;
                            case Key.L:
                                if (this.ActionButton39.Enabled && this.ActionButton39.Visible)
                                {
                                    ActionCommand(null, detectShift, tc, tc.BattleActionCommand9, 8);
                                }
                                break;

                            case Key.Escape:
                                if (this.isEscDown == false)
                                {
                                    this.isEscDown = true;
                                    if (th == null)
                                    {
                                        if (this.NowSelectingTarget)
                                        {
                                            CompleteInstantAction();
                                        }
                                    }
                                    else
                                    {
                                        if (this.NowSelectingTarget)
                                        {
                                            CompleteInstantAction();
                                        }
                                        else
                                        {
                                            if (this.DuelMode == false)
                                            {
                                                if (BattleStart.Text == "�퓬���E�E�E")
                                                {
                                                    BattleStart.Text = "�퓬��~";
                                                    tempStopFlag = true;
                                                    this.BattleMenuPanel.Visible = true;
                                                }
                                                else
                                                {
                                                    BattleStart.Text = "�퓬���E�E�E";
                                                    tempStopFlag = false;
                                                    this.BattleMenuPanel.Visible = false;
                                                }
                                            }
                                            else
                                            {
                                                if (this.NowStackInTheCommand == false)
                                                {
                                                    this.BattleMenuPanel.Visible = !this.BattleMenuPanel.Visible;
                                                }
                                            }
                                        }
                                    }
                                }
                                break;
                        }
                    }
                    System.Threading.Thread.Sleep(1);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Database.BattleRoutineError + "\r\n" + ex.ToString());
            }
        }

        /// <summary>
        /// �퓬���C�����[�v�����B�퓬�V�X�e���̊g���͂����ōs���Ă��������B
        /// </summary>
        private void BattleLoop()
        {
            try
            {
                while (!endFlag)
                {
                    System.Threading.Thread.Sleep(TIMER_SPEED);
                    CheckStackInTheCommand();
                    if (UpdatePlayerDeadFlag()) break;

                    #region "�^�C���X�g�b�v�`�F�b�N"
                    bool tempTimeStop = false;
                    for (int ii = 0; ii < ActiveList.Count; ii++)
                    {
                        if ((ActiveList[ii].CurrentTimeStop > 0))
                        {
                            this.NowTimeStop = true;
                            tempTimeStop = true;
                            break;
                        }
                    }

                    if (tempTimeStop == false)
                    {
                        this.NowTimeStop = false;
                    }
                    if ((this.NowTimeStop == true) && (this.BackColor == Color.GhostWhite))
                    {
                        this.BackColor = Color.Black;
                        this.labelBattleTurn.ForeColor = Color.White;
                        this.TimeSpeedLabel.ForeColor = Color.White;
                        this.lblTimerCount.ForeColor = Color.White;
                        for (int ii = 0; ii < ActiveList.Count; ii++)
                        {
                            ActiveList[ii].labelName.ForeColor = Color.White;
                            ActiveList[ii].ActionLabel.ForeColor = Color.White;
                            ActiveList[ii].CriticalLabel.ForeColor = Color.White;
                            ActiveList[ii].DamageLabel.ForeColor = Color.White;
                            GoToTimeStopColor(ActiveList[ii]);
                            ActiveList[ii].BuffPanel.BackColor = Color.Black;
                        }
                    }
                    if ((this.NowTimeStop == false) && (this.BackColor == Color.Black))
                    {
                        ExecPhaseElement(MethodType.TimeStopEnd, null);
                    }
                    #endregion

                    #region "�퓬��U��~�t���O"
                    if (this.tempStopFlag) continue; // �u�퓬��~�v�{�^����ESC�L�[�ŁA��U��~������B
                    if (this.DuelMode == false) // DUEL���[�h�̎��A�I�����̑I�𒆂͈�U��~���Ȃ��B
                    {
                        if (this.NowSelectingTarget) continue; // �C���X�^���g�s���ΏۑI�����A��U��~������B
                    }
                    if (this.NowStackInTheCommand) continue; // �X�^�b�N�C���U�R�}���h�������͒�~������B
                    #endregion

                    this.BattleTimeCounter++; // ���C���퓬�^�C�}�[�J�E���g�X�V
                    #region "Bystander��p"
                    int currentTimerCount = this.BattleTimeCounter;
                    if (BattleTurnCount != 0) 
                    {
                        double currentTime = (Database.BASE_TIMER_BAR_LENGTH / 2.0f - (double)currentTimerCount) / (Database.BASE_TIMER_BAR_LENGTH / 2.0f) * 300.0f / 100.0f;
                        lblTimerCount.Text = currentTime.ToString("0.00"); 
                    }
                    const int DivNum = 32;
                    for (int ii = 0; ii < 8; ii++)
                    {
                        if (DivNum * ii <= this.BattleTimeCounter && this.BattleTimeCounter < DivNum * (ii+1)) 
                        {
                            pbSandglass.Image = this.imageSandglass[ii];
                            break;
                        }
                    }
                    //else if (32 <= this.BattleTimeCounter && this.BattleTimeCounter < 64) { pbSandglass.Image = this.imageSandglass[1]; }
                    //else if (64 <= this.BattleTimeCounter && this.BattleTimeCounter < 96) { pbSandglass.Image = this.imageSandglass[2]; }
                    //else if (96 <= this.BattleTimeCounter && this.BattleTimeCounter < 128) { pbSandglass.Image = this.imageSandglass[3]; }
                    //else if (128 <= this.BattleTimeCounter && this.BattleTimeCounter < 160) { pbSandglass.Image = this.imageSandglass[4]; }
                    //else if (160 <= this.BattleTimeCounter && this.BattleTimeCounter < 192) { pbSandglass.Image = this.imageSandglass[5]; }
                    //else if (192 <= this.BattleTimeCounter && this.BattleTimeCounter < 224) { pbSandglass.Image = this.imageSandglass[6]; }
                    //else if (224 <= this.BattleTimeCounter && this.BattleTimeCounter <= 250) { pbSandglass.Image = this.imageSandglass[7]; }
                    #endregion

                    if (BattleTimeCounter >= Database.BASE_TIMER_BAR_LENGTH / 2)
                    {
                        if (BattleTurnCount == 0)
                        {
                            // �^�[���J�n���i�퓬�J�n����j
                            ExecPhaseElement(MethodType.Beginning, null);
                            // �^�[�����X�V�i�P�^�[���n�܂�j
                            UpdateTurnEnd();
                        }
                        else
                        {
                            // �^�[���X�V���O�ɂāA�퓬��̒ǉ����ʃt�F�[�Y
                            ExecPhaseElement(MethodType.AfterBattleEffect, null);

                            // �^�[�����X�V
                            UpdateTurnEnd();

                            // �^�[���X�V����̃N���[���i�b�v
                            ExecPhaseElement(MethodType.CleanUpStep, null);

                            // �^�[���X�V��̃A�b�v�L�[�v
                            ExecPhaseElement(MethodType.UpKeepStep, null);
                        }
                    }
                    else
                    {
                        ExecPhaseElement(MethodType.CleanUpForBoss, null);
                    }

                    UpdateUseItemGauge();

                    #region "�e�v���C���[�̐퓬�t�F�[�Y"
                    for (int ii = 0; ii < ActiveList.Count; ii++)
                    {
                        if (this.NowTimeStop && ActiveList[ii].CurrentTimeStop <= 0 && ActiveList[ii].Name != Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
                        {
                            // ���Ԃ͔�΂����
                        }
                        else if (!ActiveList[ii].Dead)
                        {
                            if (ActiveList[ii].BattleBarPos > Database.BASE_TIMER_BAR_LENGTH ||
                                ActiveList[ii].BattleBarPos2 > Database.BASE_TIMER_BAR_LENGTH ||
                                ActiveList[ii].BattleBarPos3 > Database.BASE_TIMER_BAR_LENGTH)
                            {
                                // �퓬�s�������s�O�Ƀ|�W�V�����ƈӎv����t���O�ƃJ�E���^�[�A�^�b�N������
                                int arrowType = 0;
                                if (ActiveList[ii].BattleBarPos2 > Database.BASE_TIMER_BAR_LENGTH) { arrowType = 1; }
                                else if (ActiveList[ii].BattleBarPos3 > Database.BASE_TIMER_BAR_LENGTH) { arrowType = 2; }
                                UpdatePlayerPreCondition(ActiveList[ii], arrowType);

                                // �퓬�s�������s
                                if (ExecPhaseElement(MethodType.PlayerAttackPhase, ActiveList[ii]) == false) break;

                                if (ActiveList[ii].CurrentSkillName == Database.STANCE_OF_FLOW && ActiveList[ii].PA == PlayerAction.UseSkill)
                                {
                                    ActiveList[ii].BattleBarPos = Database.BASE_TIMER_BAR_LENGTH;
                                }

                                // �Ώۂ��s���s�\�ȏꍇ�A�^�[�Q�b�g��؂�ւ���B
                                UpdatePlayerTarget(ActiveList[ii]);
                            }
                            else
                            {
                                // �C���X�^���g�s���̃^�C�}�[�X�V
                                UpdatePlayerInstantPoint(ActiveList[ii]);

                                // �퓬�ҋ@�|�W�V�����X�V
                                UpdatePlayerGaugePosition(ActiveList[ii]);

                                // �퓬���s���e�̌���t�F�[�Y�i�G��p)
                                UpdatePlayerNextDecision(ActiveList[ii]);

                                // �X�^�b�N�C���U�R�}���h�̔�������t�F�[�Y�i�G��p�j
                                UpdatePlayerDoStackInTheCommand(ActiveList[ii]);
                            }
                        }
                    }
                    #endregion

                    CheckStackInTheCommand();
                    if (UpdatePlayerDeadFlag()) break; // �p�[�e�B���S�m�F�Ő퓬�𔲂���B
                    if (this.endBattleForMatrixDragonEnd) break; // �퓬�I���T�C���ɂ��A�퓬�𔲂���B

                    pbPlayer1.Invalidate();
                }

                // �퓬�I����
                pbPlayer1.Invalidate();
                this.Invoke(new _BattleEndPhase(BattleEndPhase));
            }
            catch (Exception ex)
            {
                MessageBox.Show(Database.BattleRoutineError + "\r\n" + ex.ToString());
            }
        }

        private void UpdatePlayerDoStackInTheCommand(MainCharacter player)
        {
            if (IsPlayerAlly(player)) { return; } // �����v���C���[�͎����I�ɉ��炩�̍s���͍s��Ȃ��B
            if (this.NowStackInTheCommand) { return; } // �X�^�b�N�E�C���E�U�E�R�}���h���̓X���[

            #region "�Z�����C�E���E"
            if (player.Name == Database.DUEL_SELMOI_RO)
            {
                bool existItem = false;
                ItemBackPack[] tempItem = player.GetBackPackInfo();
                foreach (ItemBackPack value in tempItem)
                {
                    if (value != null)
                    {
                        if (value.Name == Database.COMMON_FROZEN_BALL)
                        {
                            existItem = true;
                        }
                    }
                }

                if (player.CurrentInstantPoint >= player.MaxInstantPoint && 50 < player.BattleBarPos && player.BattleBarPos < 100)
                {
                    if (existItem)
                    {
                        UseInstantPoint(player);
                        player.StackActivePlayer = player;
                        player.StackTarget = mc;
                        player.StackPlayerAction = PlayerAction.UseItem;
                        player.StackCommandString = Database.COMMON_FROZEN_BALL;
                        player.StackActivation = true;
                        this.NowStackInTheCommand = true;
                    }
                    else if (player.CurrentAbsorbWater <= 0)
                    {
                        if (player.CurrentAbsorbWater <= 0)
                        {
                            UseInstantPoint(player);
                            player.StackActivePlayer = player;
                            player.StackTarget = player;
                            player.StackPlayerAction = PlayerAction.UseSpell;
                            player.StackCommandString = Database.ABSORB_WATER;
                            player.StackActivation = true;
                            this.NowStackInTheCommand = true;
                        }
                        else
                        {
                            UseInstantPoint(player);
                            player.StackActivePlayer = player;
                            player.StackTarget = mc;
                            player.StackPlayerAction = PlayerAction.UseSpell;
                            player.StackCommandString = Database.WORD_OF_POWER;
                            player.StackActivation = true;
                            this.NowStackInTheCommand = true;
                        }
                    }
                    else
                    {
                        UseInstantPoint(player);
                        player.StackActivePlayer = player;
                        player.StackTarget = mc;
                        player.StackPlayerAction = PlayerAction.UseSpell;
                        player.StackCommandString = Database.WORD_OF_POWER;
                        player.StackActivation = true;
                        this.NowStackInTheCommand = true;
                    }
                }
            }
            #endregion
            #region "�J�[�e�B���E�}�C"
            else if (player.Name == Database.DUEL_KARTIN_MAI)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if (player.CurrentHeatBoost <= 0)
                    {
                        UseInstantPoint(player);
                        player.StackActivePlayer = player;
                        player.StackTarget = player;
                        player.StackPlayerAction = PlayerAction.UseSpell;
                        player.StackCommandString = Database.HEAT_BOOST;
                        player.StackActivation = true;
                        this.NowStackInTheCommand = true;
                    }
                    else if (player.CurrentShadowPact <= 0)
                    {
                        UseInstantPoint(player);
                        player.StackActivePlayer = player;
                        player.StackTarget = player;
                        player.StackPlayerAction = PlayerAction.UseSpell;
                        player.StackCommandString = Database.SHADOW_PACT;
                        player.StackActivation = true;
                        this.NowStackInTheCommand = true;
                    }
                    else
                    {
                        UseInstantPoint(player);
                        player.StackActivePlayer = player;
                        player.StackTarget = mc;
                        player.StackPlayerAction = PlayerAction.UseSpell;
                        player.StackCommandString = Database.FIRE_BALL;
                        player.StackActivation = true;
                        this.NowStackInTheCommand = true;
                    }
                }
            }
            #endregion
            #region "�W�F�_�E�A���X"
            else if (player.Name == Database.DUEL_JEDA_ARUS)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint && 150 < player.BattleBarPos && player.BattleBarPos < 200)
                {
                    UseInstantPoint(player);
                    player.StackActivePlayer = player;
                    player.StackTarget = mc;
                    player.StackPlayerAction = PlayerAction.UseItem;
                    player.StackCommandString = Database.RARE_AERO_BLADE;
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "�V�j�L�A�E���F�C���n���c"
            else if (player.Name == Database.DUEL_SINIKIA_VEILHANTU)
            {
                if (player.CurrentLife < player.MaxLife / 3)
                {
                    if (player.CurrentInstantPoint >= player.MaxInstantPoint && 400 < player.BattleBarPos && player.BattleBarPos < 450)
                    {
                        UseInstantPoint(player);
                        player.StackActivePlayer = ec1;
                        player.StackTarget = ec1;
                        player.StackPlayerAction = PlayerAction.UseSpell;
                        player.StackCommandString = Database.LIFE_TAP;
                        player.StackActivation = true;
                        this.NowStackInTheCommand = true;
                    }
                }
            }
            #endregion
            #region "�y�P�K�z���݂��t�����V�X"
            else if (player.Name == Database.ENEMY_BOSS_KARAMITUKU_FLANSIS)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint && 150 < player.BattleBarPos && player.BattleBarPos < 200)
                {
                    UseInstantPoint(player);
                    player.StackActivePlayer = ec1;
                    if (mc != null && !mc.Dead) player.StackTarget = mc;
                    else if (sc != null && !sc.Dead) player.StackTarget = sc;
                    //player.StackTarget = null; // �u�x���vnull�w�肪�X�^�b�N�C���U�R�}���h�Łu�S�́v��\�����悤�Ƃ��Ă��邪�A�v�����̐V�d�l�ł���B�S�̎d�l�̂ǂ��Ɋւ���Ă��邩�l�@���Ă��������B
                    player.StackPlayerAction = PlayerAction.SpecialSkill;
                    player.StackCommandString = "�L���E�X�s�j���O�����T�[";
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "�I���E�����f�B�X�i��DUEL)
            else if (player.Name == Database.DUEL_OL_LANDIS)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if ((mc.CurrentLife < 500) && (mc.CurrentParalyze <= 0) && (ec1.CurrentSkillPoint >= Database.SURPRISE_ATTACK_COST))
                    {
                        UseInstantPoint(player);
                        player.StackActivePlayer = ec1;
                        player.StackTarget = mc;
                        player.StackPlayerAction = PlayerAction.UseSkill;
                        player.StackCommandString = Database.SURPRISE_ATTACK;
                        player.StackActivation = true;
                        this.NowStackInTheCommand = true;
                    }
                    else
                    {
                        if ((mc.CurrentBlackFire <= 0) && (ec1.CurrentMana >= Database.BLACK_FIRE_COST))
                        {
                            UseInstantPoint(player);
                            player.StackActivePlayer = ec1;
                            player.StackTarget = mc;
                            player.StackPlayerAction = PlayerAction.UseSpell;
                            player.StackCommandString = Database.BLACK_FIRE;
                            player.StackActivation = true;
                            this.NowStackInTheCommand = true;
                        }
                        else if ((mc.CurrentImmolate <= 0) && (ec1.CurrentMana >= Database.IMMOLATE_COST))
                        {
                            UseInstantPoint(player);
                            player.StackActivePlayer = ec1;
                            player.StackTarget = mc;
                            player.StackPlayerAction = PlayerAction.UseSpell;
                            player.StackCommandString = Database.IMMOLATE;
                            player.StackActivation = true;
                            this.NowStackInTheCommand = true;
                        }
                    }
                }
                // ������Ƌ�������̂ŁA���󂩁B
                //    if ((mc.CurrentLife < mc.MaxLife / 2) && (ec1.CurrentSkillPoint >= Database.CRUSHING_BLOW_COST))
                //    {
                //        UseInstantPoint(player);
                //        player.StackActivePlayer = ec1;
                //        player.StackTarget = mc;
                //        player.StackPlayerAction = PlayerAction.UseSkill;
                //        player.StackCommandString = Database.CRUSHING_BLOW;
                //        player.StackActivation = true;
                //        this.NowStackInTheCommand = true;
                //    }
                //    else if (ec1.CurrentSkillPoint >= Database.STRAIGHT_SMASH_COST)
                //    {
                //        UseInstantPoint(player);
                //        player.StackActivePlayer = ec1;
                //        player.StackTarget = mc;
                //        player.StackPlayerAction = PlayerAction.UseSkill;
                //        player.StackCommandString = Database.STRAIGHT_SMASH;
                //        player.StackActivation = true;
                //        this.NowStackInTheCommand = true;
                //    }
                //    else 
                //    {
                //        UseInstantPoint(player);
                //        player.StackActivePlayer = ec1;
                //        player.StackTarget = ec1;
                //        player.StackPlayerAction = PlayerAction.UseSkill;
                //        player.StackCommandString = Database.INNER_INSPIRATION;
                //        player.StackActivation = true;
                //        this.NowStackInTheCommand = true;
                //    }                    
                //}
            }
            #endregion
            #region "�P����C�̉��q"
            else if (player.Name == Database.ENEMY_BRILLIANT_SEA_PRINCE)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint && 150 < player.BattleBarPos && player.BattleBarPos < 200)
                {
                    UseInstantPoint(player);
                    player.StackActivePlayer = ec1;
                    player.StackTarget = null; // �u�x���vnull�w�肪�X�^�b�N�C���U�R�}���h�Łu�S�́v��\�����悤�Ƃ��Ă��邪�A�v�����̐V�d�l�ł���B�S�̎d�l�̂ǂ��Ɋւ���Ă��邩�l�@���Ă��������B
                    player.StackPlayerAction = PlayerAction.SpecialSkill;
                    player.StackCommandString = "�A�C�\�j�b�N�E�E�F�C��";
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "�����E�X��̏���"
            else if (player.Name == Database.ENEMY_ORIGIN_STAR_CORAL_QUEEN)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    UseInstantPoint(player);
                    player.StackActivePlayer = ec1;
                    if (player.CurrentPhysicalDefenseUp <= 0)
                    {
                        player.StackTarget = ec1;
                        player.StackPlayerAction = PlayerAction.SpecialSkill;
                        player.StackCommandString = "�A���_�[�g�̉r��";
                    }
                    else if (player.CurrentMirrorImage <= 0)
                    {
                        player.StackTarget = ec1;
                        player.StackPlayerAction = PlayerAction.SpecialSkill;
                        player.StackCommandString = "�T���}���̉r��";
                    }
                    else
                    {
                        if (AP.Math.RandomInteger(2) == 0)
                        {
                            player.StackTarget = null;
                            player.StackPlayerAction = PlayerAction.SpecialSkill;
                            player.StackCommandString = "�G�������^���E�X�v���b�V��";

                        }
                        else
                        {
                            player.StackTarget = ec1;
                            player.StackPlayerAction = PlayerAction.SpecialSkill;
                            player.StackCommandString = "�����̗���";
                        }
                    }
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "�W���G���E�i�C�g"
            else if (player.Name == Database.ENEMY_SHELL_SWORD_KNIGHT)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    UseInstantPoint(player);
                    player.StackActivePlayer = ec1;
                    if (mc != null && !mc.Dead) { player.StackTarget = mc; }
                    else if (sc != null && !sc.Dead) { player.StackTarget = sc; }
                    else if (tc != null && !tc.Dead) { player.StackTarget = tc; }
                    player.StackPlayerAction = PlayerAction.SpecialSkill;
                    player.StackCommandString = "�W���G���E�u���C�N";
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "�W�F���[�A�C�E��"
            else if (player.Name == Database.ENEMY_JELLY_EYE_BRIGHT_RED)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    UseInstantPoint(player);
                    player.StackActivePlayer = ec1;
                    if (mc != null && !mc.Dead) { player.StackTarget = mc; }
                    else if (sc != null && !sc.Dead) { player.StackTarget = sc; }
                    else if (tc != null && !tc.Dead) { player.StackTarget = tc; }
                    player.StackPlayerAction = PlayerAction.SpecialSkill;
                    player.StackCommandString = "�n��̈ꌂ";
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "�W�F���[�A�C�E��"
            else if (player.Name == Database.ENEMY_JELLY_EYE_DEEP_BLUE)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    UseInstantPoint(player);
                    player.StackActivePlayer = ec2;
                    if (mc != null && !mc.Dead) { player.StackTarget = mc; }
                    else if (sc != null && !sc.Dead) { player.StackTarget = sc; }
                    else if (tc != null && !tc.Dead) { player.StackTarget = tc; }
                    player.StackPlayerAction = PlayerAction.SpecialSkill;
                    player.StackCommandString = "��蹂̈ꌂ";
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "�C���R�m�G�[�M��"
            else if (player.Name == Database.ENEMY_SEA_STAR_KNIGHT_AEGIRU)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    UseInstantPoint(player);
                    player.StackActivePlayer = ec2;
                   
                    if (sc != null && !sc.Dead) { player.StackTarget = sc; }
                    else if (tc != null && !tc.Dead) { player.StackTarget = tc; }
                    else if (mc != null && !mc.Dead) { player.StackTarget = mc; }
                    player.StackPlayerAction = PlayerAction.SpecialSkill;
                    player.StackCommandString = "�X�^�[�E�_�X�g";
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "�C���R�m�A�}��"
            else if (player.Name == Database.ENEMY_SEA_STAR_KNIGHT_AMARA)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    UseInstantPoint(player);
                    player.StackActivePlayer = ec3;

                    if (tc != null && !tc.Dead) { player.StackTarget = tc; }
                    else if (sc != null && !sc.Dead) { player.StackTarget = sc; }
                    else if (mc != null && !mc.Dead) { player.StackTarget = mc; }
                    player.StackPlayerAction = PlayerAction.SpecialSkill;
                    player.StackCommandString = "�X�^�[�E�t�H�[��";
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "�C�����̉�"
            else if (player.Name == Database.ENEMY_SEA_STAR_ORIGIN_KING)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    UseInstantPoint(player);
                    player.StackActivePlayer = ec1;
                    player.StackTarget = null;
                    player.StackPlayerAction = PlayerAction.SpecialSkill;
                    player.StackCommandString = "�C�����̎���";
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "�A�f���E�u���K���f�B"
            else if (player.Name == Database.DUEL_ADEL_BRIGANDY)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    UseInstantPoint(player);
                    player.StackActivePlayer = ec1;
                    player.StackTarget = mc;
                    player.StackPlayerAction = PlayerAction.UseSpell;
                    player.StackCommandString = Database.WORD_OF_POWER;
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "���l�E�R���g�X"
            else if (player.Name == Database.DUEL_LENE_COLTOS)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    UseInstantPoint(player);
                    if (((TruthEnemyCharacter)player).AI_TacticsNumber == 1)
                    {
                        player.StackActivePlayer = ec1;
                        player.StackTarget = ec1;
                        player.StackPlayerAction = PlayerAction.UseSpell;
                        player.StackCommandString = Database.FRESH_HEAL;
                    }
                    else
                    {
                        player.StackActivePlayer = ec1;
                        player.StackTarget = mc;
                        player.StackPlayerAction = PlayerAction.UseItem;
                        player.StackCommandString = Database.RARE_BLUE_LIGHTNING;
                    }
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "�X�R�[�e�B�E�U���Q"
            else if (player.Name == Database.DUEL_SCOTY_ZALGE)
            {
            }
            #endregion
            #region "�y���}�E�����~�B"
            else if (player.Name == Database.DUEL_PERMA_WARAMY)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint && player.BattleBarPos < 50)
                {
                    UseInstantPoint(player);
                    if (player.CurrentLife <= player.MaxLife * 2 / 3)
                    {
                        player.StackActivePlayer = ec1;
                        player.StackTarget = ec1;
                        player.StackPlayerAction = PlayerAction.UseSpell;
                        player.StackCommandString = Database.FRESH_HEAL;
                    }
                    else if (player.CurrentWordOfLife <= 0)
                    {
                        player.StackActivePlayer = ec1;
                        player.StackTarget = ec1;
                        player.StackPlayerAction = PlayerAction.UseSpell;
                        player.StackCommandString = Database.WORD_OF_LIFE;
                    }
                    else if (mc.CurrentEnrageBlast <= 0)
                    {
                        player.StackActivePlayer = ec1;
                        player.StackTarget = mc;
                        player.StackPlayerAction = PlayerAction.UseSpell;
                        player.StackCommandString = Database.ENRAGE_BLAST;
                    }
                    else if (player.CurrentFlameAura <= 0)
                    {
                        player.StackActivePlayer = ec1;
                        player.StackTarget = ec1;
                        player.StackPlayerAction = PlayerAction.UseSpell;
                        player.StackCommandString = Database.FLAME_AURA;
                    }
                    else if (player.CurrentStrengthUp <= 0)
                    {
                        player.StackActivePlayer = ec1;
                        player.StackTarget = ec1;
                        player.StackPlayerAction = PlayerAction.UseItem;
                        player.StackCommandString = Database.RARE_BURNING_CLAYMORE;
                    }
                    else
                    {
                        player.StackActivePlayer = ec1;
                        player.StackTarget = mc;
                        player.StackPlayerAction = PlayerAction.UseSpell;
                        player.StackCommandString = Database.WORD_OF_POWER;
                    }
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "�L���g�E�W�����W��"
            else if (player.Name == Database.DUEL_KILT_JORJU)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if (mc.CurrentLife <= 1200)
                    {
                        if ((player.CurrentWordOfFortune <= 0))
                        {
                            if (player.BattleBarPos > Database.BASE_TIMER_BAR_LENGTH - 50)
                            {
                                UseInstantPoint(player);
                                player.StackActivePlayer = player;
                                player.StackTarget = player;
                                player.StackPlayerAction = PlayerAction.UseSpell;
                                player.StackCommandString = Database.WORD_OF_FORTUNE;
                                player.StackActivation = true;
                                this.NowStackInTheCommand = true;
                            }
                        }
                        else
                        {
                            if (player.CurrentSkillPoint > Database.STRAIGHT_SMASH_COST && ((TruthEnemyCharacter)player).AI_TacticsNumber == 0)
                            {
                                UseInstantPoint(player);
                                player.StackActivePlayer = ec1;
                                player.StackTarget = mc;
                                player.StackPlayerAction = PlayerAction.UseSkill;
                                player.StackCommandString = Database.STRAIGHT_SMASH;
                                player.StackActivation = true;
                                this.NowStackInTheCommand = true;
                            }
                            else if (player.CurrentMana > Database.BLUE_BULLET_COST && ((TruthEnemyCharacter)player).AI_TacticsNumber == 1)
                            {
                                UseInstantPoint(player);
                                player.StackActivePlayer = ec1;
                                player.StackTarget = mc;
                                player.StackPlayerAction = PlayerAction.UseSpell;
                                player.StackCommandString = Database.BLUE_BULLET;
                                player.StackActivation = true;
                                this.NowStackInTheCommand = true;
                            }
                        }
                    }
                    else
                    {
                        if (player.CurrentSkillPoint > Database.STRAIGHT_SMASH_COST && ((TruthEnemyCharacter)player).AI_TacticsNumber == 0)
                        {
                            UseInstantPoint(player);
                            player.StackActivePlayer = ec1;
                            player.StackTarget = mc;
                            player.StackPlayerAction = PlayerAction.UseSkill;
                            player.StackCommandString = Database.STRAIGHT_SMASH;
                            player.StackActivation = true;
                            this.NowStackInTheCommand = true;
                        }
                        else if (player.CurrentMana > Database.BLUE_BULLET_COST && ((TruthEnemyCharacter)player).AI_TacticsNumber == 1)
                        {
                            UseInstantPoint(player);
                            player.StackActivePlayer = ec1;
                            player.StackTarget = mc;
                            player.StackPlayerAction = PlayerAction.UseSpell;
                            player.StackCommandString = Database.BLUE_BULLET;
                            player.StackActivation = true;
                            this.NowStackInTheCommand = true;
                        }
                    }
                }
            }
            #endregion
            #region "�y�Q�K�z��C�փ����B�A�T��"
            else if (player.Name == Database.ENEMY_BOSS_LEVIATHAN)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    UseInstantPoint(player);
                    player.StackActivePlayer = ec1;
                    player.StackTarget = null;
                    player.StackPlayerAction = PlayerAction.SpecialSkill;
                    player.StackCommandString = "�^�C�_���E�E�F�C�u";
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "���F���[�E�A�[�e�B(��DUEL�j"
            else if (player.Name == Database.VERZE_ARTIE)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if ((player.CurrentStanceOfMystic <= 0) && (player.CurrentSkillPoint >= Database.STANCE_OF_MYSTIC_COST))
                    {
                        ExecActionMethod(player, player, PlayerAction.UseSkill, Database.STANCE_OF_MYSTIC);
                    }
                    else if (mc.CurrentHolyBreaker > 0)
                    {
                        if (player.CurrentSkillPoint > Database.PSYCHIC_WAVE_COST)
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSkill, Database.PSYCHIC_WAVE);
                        }
                        else
                        {
                            ExecActionMethod(player, player, PlayerAction.UseSkill, Database.INNER_INSPIRATION);
                        }
                    }
                    else if (player.CurrentGaleWind > 0)
                    {
                        if (player.CurrentSkillPoint >= Database.STRAIGHT_SMASH_COST)
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSkill, Database.STRAIGHT_SMASH);
                        }
                        else
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSkill, Database.NEUTRAL_SMASH);
                        }
                    }
                    else if (player.CurrentCounterAttack <= 0 && player.CurrentDeflection <= 0)
                    {
                        ExecActionMethod(player, mc, PlayerAction.UseSkill, Database.COUNTER_ATTACK);
                    }
                    else if (player.CurrentSkyShieldValue <= 0 && player.CurrentMirrorImage <= 0)
                    {
                        ExecActionMethod(player, player, PlayerAction.UseSpell, Database.MIRROR_IMAGE);
                    }
                    else if (player.CurrentSkillPoint < player.MaxSkillPoint)
                    {
                        ExecActionMethod(player, player, PlayerAction.UseSkill, Database.INNER_INSPIRATION);
                    }
                }
            }
            #endregion
            #region "�r���[�E���L"
            else if (player.Name == Database.DUEL_BILLY_RAKI)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if ((player.CurrentSkillPoint > Database.DOUBLE_SLASH_COST) && (player.CurrentSkillPoint < Database.CARNAGE_RUSH_COST))
                    {
                        UseInstantPoint(player);
                        player.StackActivePlayer = ec1;
                        player.StackTarget = mc;
                        player.StackPlayerAction = PlayerAction.UseSkill;
                        player.StackCommandString = Database.DOUBLE_SLASH;
                        player.StackActivation = true;
                        this.NowStackInTheCommand = true;
                    }
                }                
            }
            #endregion
            #region "�A���i�E�n�~���g��"
            else if (player.Name == Database.DUEL_ANNA_HAMILTON)
            {
                // �Ƃ��ɂȂ�
            }
            #endregion
            #region "�J���}���Y�E�I�[��"
            else if (player.Name == Database.DUEL_CALMANS_OHN)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if (player.CurrentWordOfLife <= 0)
                    {
                        UseInstantPoint(player);
                        player.StackActivePlayer = ec1;
                        player.StackTarget = ec1;
                        player.StackPlayerAction = PlayerAction.UseSpell;
                        player.StackCommandString = Database.WORD_OF_LIFE;
                        player.StackActivation = true;
                        this.NowStackInTheCommand = true;
                    }
                    else
                    {
                        if (((TruthEnemyCharacter)player).AI_TacticsNumber == 0)
                        {
                            if (player.CurrentGaleWind > 0)
                            {
                                UseInstantPoint(player);
                                player.StackActivePlayer = ec1;
                                player.StackTarget = mc;
                                player.StackPlayerAction = PlayerAction.UseSpell;
                                player.StackCommandString = Database.BLUE_BULLET;
                                player.StackActivation = true;
                                this.NowStackInTheCommand = true;
                            }
                            else
                            {
                                UseInstantPoint(player);
                                player.StackActivePlayer = ec1;
                                player.StackTarget = mc;
                                player.StackPlayerAction = PlayerAction.UseSpell;
                                player.StackCommandString = Database.DEVOURING_PLAGUE;
                                player.StackActivation = true;
                                this.NowStackInTheCommand = true;
                            }

                            ((TruthEnemyCharacter)player).AI_TacticsNumber = 1;
                        }
                        else if (((TruthEnemyCharacter)player).AI_TacticsNumber == 1)
                        {
                            if (player.CurrentPromisedKnowledge <= 0)
                            {
                                UseInstantPoint(player);
                                player.StackActivePlayer = ec1;
                                player.StackTarget = ec1;
                                player.StackPlayerAction = PlayerAction.UseSpell;
                                player.StackCommandString = Database.PROMISED_KNOWLEDGE;
                                player.StackActivation = true;
                                this.NowStackInTheCommand = true;
                            }
                            else
                            {
                                UseInstantPoint(player);
                                player.StackActivePlayer = ec1;
                                player.StackTarget = mc;
                                player.StackPlayerAction = PlayerAction.UseSpell;
                                player.StackCommandString = Database.BLUE_BULLET;
                                player.StackActivation = true;
                                this.NowStackInTheCommand = true;
                            }

                            ((TruthEnemyCharacter)player).AI_TacticsNumber = 2;
                        }
                        else
                        {
                            if (mc.CurrentLife <= mc.MaxLife / 2)
                            {
                                UseInstantPoint(player);
                                player.StackActivePlayer = ec1;
                                player.StackTarget = ec1;
                                player.StackPlayerAction = PlayerAction.UseSpell;
                                player.StackCommandString = Database.GALE_WIND;
                                player.StackActivation = true;
                                this.NowStackInTheCommand = true;
                            }
                            else
                            {
                                if (player.CurrentGaleWind > 0)
                                {
                                    UseInstantPoint(player);
                                    player.StackActivePlayer = ec1;
                                    player.StackTarget = mc;
                                    player.StackPlayerAction = PlayerAction.UseSpell;
                                    player.StackCommandString = Database.BLUE_BULLET;
                                    player.StackActivation = true;
                                    this.NowStackInTheCommand = true;
                                }
                                else
                                {
                                    UseInstantPoint(player);
                                    player.StackActivePlayer = ec1;
                                    player.StackTarget = mc;
                                    player.StackPlayerAction = PlayerAction.UseSpell;
                                    player.StackCommandString = Database.DEVOURING_PLAGUE;
                                    player.StackActivation = true;
                                    this.NowStackInTheCommand = true;
                                }
                            }
                            ((TruthEnemyCharacter)player).AI_TacticsNumber = 0;
                        }
                    }
                }
            }
            #endregion
            #region "�T���E���E"
            else if (player.Name == Database.DUEL_SUN_YU)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if (mc.CurrentAetherDrive > 0)
                    {
                        ExecActionMethod(player, mc, PlayerAction.UseSpell, Database.TRANQUILITY);
                    }
                    else if (mc.CurrentSaintPower > 0)
                    {
                        ExecActionMethod(player, mc, PlayerAction.UseSpell, Database.DISPEL_MAGIC);
                    }
                }
            }
            #endregion
            #region "�V�����@���c�F�E�t���[��"
            else if (player.Name == Database.DUEL_SHUVALTZ_FLORE)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if (((TruthEnemyCharacter)player).AI_TacticsNumber == 0)
                    {
                        if (player.CurrentProtection <= 0)
                        {
                            ExecActionMethod(player, player, PlayerAction.UseSpell, Database.PROTECTION);
                        }
                        else if (player.CurrentAbsorbWater <= 0)
                        {
                            ExecActionMethod(player, player, PlayerAction.UseSpell, Database.ABSORB_WATER);
                        }
                        else if (player.CurrentSaintPower <= 0)
                        {
                            ExecActionMethod(player, player, PlayerAction.UseSpell, Database.SAINT_POWER);
                        }
                        else if (player.CurrentAbsorbWater <= 0)
                        {
                            ExecActionMethod(player, player, PlayerAction.UseSpell, Database.ABSORB_WATER);
                        }
                        else if (player.CurrentAetherDrive <= 0)
                        {
                            ExecActionMethod(player, player, PlayerAction.UseSpell, Database.AETHER_DRIVE);
                        }
                        ((TruthEnemyCharacter)player).AI_TacticsNumber = 1;
                    }
                    else if (((TruthEnemyCharacter)player).AI_TacticsNumber == 1)
                    {
                        ExecActionMethod(player, mc, PlayerAction.UseSkill, Database.NEUTRAL_SMASH);
                        ((TruthEnemyCharacter)player).AI_TacticsNumber = 2;
                    }
                    else
                    {
                        if (50 < mc.BattleBarPos && mc.BattleBarPos < 75)
                        {
                            ((TruthEnemyCharacter)player).AI_TacticsNumber = 0;
                        }
                    }
                }
            }
            #endregion
            #region "�V�j�L�A�E�J�[���n���c(DUEL)"
            else if (player.Name == Database.DUEL_SINIKIA_KAHLHANZ)
            {
                if ((player.CurrentInstantPoint >= player.MaxInstantPoint) &&
                    (mc.CurrentInstantPoint < mc.MaxInstantPoint))
                {
                    if (((TruthEnemyCharacter)player).AI_TacticsNumber == 0)
                    {
                        if ((mc.CurrentFrozen <= 0) &&
                            (((TruthEnemyCharacter)player).DetectCannotBeFrozen == false))
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSpell, Database.CHILL_BURN);
                        }
                        else if (player.CurrentHeatBoost <= 0)
                        {
                            ExecActionMethod(player, player, PlayerAction.UseSpell, Database.HEAT_BOOST);
                        }
                        else if (player.CurrentPsychicTrance <= 0)
                        {
                            ExecActionMethod(player, player, PlayerAction.UseSpell, Database.PSYCHIC_TRANCE);
                        }
                        else if (mc.CurrentWordOfMalice <= 0)
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSpell, Database.WORD_OF_MALICE);
                        }
                        else if (mc.CurrentBlackFire <= 0)
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSpell, Database.BLACK_FIRE);
                        }
                        else
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSpell, Database.PIERCING_FLAME);
                        }
                        //((TruthEnemyCharacter)player).AI_TacticsNumber = 1;
                    }
                    //else if (((TruthEnemyCharacter)player).AI_TacticsNumber == 1)
                    //{
                    //    ExecActionMethod(player, mc, PlayerAction.UseSkill, Database.NEUTRAL_SMASH);
                    //    ((TruthEnemyCharacter)player).AI_TacticsNumber = 2;
                    //}
                    //else
                    //{
                    //    if (50 < mc.BattleBarPos && mc.BattleBarPos < 75)
                    //    {
                    //        ((TruthEnemyCharacter)player).AI_TacticsNumber = 0;
                    //    }
                    //}
                }
            }
            #endregion
            #region "�y�R�K�z�n�E�����O�E�V�[�U�["
            else if (player.Name == Database.ENEMY_BOSS_HOWLING_SEIZER)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    ExecActionMethod(player, null, PlayerAction.SpecialSkill, "�A�[�X�E�R�[���h�E�V�F�C�N");
                }
            }
            #endregion
            #region "���x���E�[���L�X"
            else if (player.Name == Database.DUEL_RVEL_ZELKIS)
            {
                if ((player.CurrentInstantPoint >= player.MaxInstantPoint))
                {
                    if (mc.CurrentFrozen > 0)
                    {
                        ((TruthEnemyCharacter)player).AI_TacticsNumber = 1;
                    }
                    else if (player.CurrentPromisedKnowledge > 0)
                    {
                        ((TruthEnemyCharacter)player).AI_TacticsNumber = 2;
                    }

                    if (((TruthEnemyCharacter)player).AI_TacticsNumber == 0)
                    {
                        if ((mc.CurrentFrozen <= 0) &&
                            (player.CurrentMana >= Database.CHILL_BURN_COST) &&
                            (((TruthEnemyCharacter)player).DetectCannotBeFrozen == false))
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSpell, Database.CHILL_BURN);
                        }
                    }
                    else if (((TruthEnemyCharacter)player).AI_TacticsNumber == 1)
                    {
                        if ((player.CurrentPromisedKnowledge <= 0) &&
                                 (player.CurrentMana >= Database.PROMISED_KNOWLEDGE_COST))
                        {
                            ExecActionMethod(player, player, PlayerAction.UseSpell, Database.PROMISED_KNOWLEDGE);
                        }
                    }
                    else if (((TruthEnemyCharacter)player).AI_TacticsNumber == 2)
                    {
                        if ((mc.CurrentFrozen <= 0) &&
                            (player.CurrentMana >= Database.CHILL_BURN_COST) &&
                            (((TruthEnemyCharacter)player).DetectCannotBeFrozen == false))
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSpell, Database.CHILL_BURN);
                        }
                        else if (player.CurrentSkillPoint >= Database.CARNAGE_RUSH_COST)
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSkill, Database.CARNAGE_RUSH);
                        }
                        else if (player.CurrentMana >= Database.BLUE_BULLET_COST)
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSpell, Database.BLUE_BULLET);
                        }
                    }
                }
            }
            #endregion
            #region "���@���E�w�[�O�X�e��"
            else if (player.Name == Database.DUEL_VAN_HEHGUSTEL)
            {
                if ((player.CurrentInstantPoint >= player.MaxInstantPoint))
                {
                    // ��p�̔��f
                    if (player.CurrentLightServant > 0 && player.CurrentLightServantValue >= 3)
                    {
                        ((TruthEnemyCharacter)player).AI_TacticsNumber = 1;
                    }
                    if (player.CurrentFlameAura > 0 && player.CurrentFrozenAura > 0)
                    {
                        ((TruthEnemyCharacter)player).AI_TacticsNumber = 2;
                    }

                    if (player.CurrentLightServant > 0 && player.CurrentLightServantValue >= 3 && player.CurrentLife < player.MaxLife / 2)
                    {
                        ExecActionMethod(player, player, PlayerAction.UseItem, Database.COMMON_LIGHT_SERVANT);
                    }
                    else if (((TruthEnemyCharacter)player).AI_TacticsNumber == 0)
                    {
                        ExecActionMethod(player, mc, PlayerAction.UseSkill, Database.NEUTRAL_SMASH);
                    }
                    else if (((TruthEnemyCharacter)player).AI_TacticsNumber == 1)
                    {
                        if ((player.CurrentFlameAura <= 0) &&
                            (player.CurrentMana >= Database.FLAME_AURA_COST))
                        {
                            ExecActionMethod(player, player, PlayerAction.UseSpell, Database.FLAME_AURA);
                        }
                        else if ((player.CurrentFrozenAura <= 0) &&
                                 (player.CurrentMana >= Database.FROZEN_AURA_COST))
                        {
                            ExecActionMethod(player, player, PlayerAction.UseSpell, Database.FROZEN_AURA);
                        }
                    }
                    else
                    {
                        if ((mc.CurrentLife < mc.MaxLife * 2 / 3) &&
                            (player.CurrentSkillPoint >= Database.SURPRISE_ATTACK_COST) &&
                            (mc.CurrentParalyze <= 0) &&
                            (((TruthEnemyCharacter)player).DetectCannotBeParalyze == false))
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSkill, Database.SURPRISE_ATTACK);
                        }
                        else
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSkill, Database.NEUTRAL_SMASH);
                        }
                    }
                }
            }
            #endregion
            #region "�I�E�����E�E�Q���}"
            else if (player.Name == Database.DUEL_OHRYU_GENMA)
            {
                if ((player.CurrentInstantPoint >= player.MaxInstantPoint))
                {
                    if (player.CurrentSkillPoint < Database.CARNAGE_RUSH_COST)
                    {
                        ExecActionMethod(player, player, PlayerAction.UseSkill, Database.INNER_INSPIRATION);
                    }
                    else if ((player.CurrentSwiftStep <= 0) &&
                        (player.CurrentSkillPoint >= Database.SWIFT_STEP_COST))
                    {
                        ExecActionMethod(player, player, PlayerAction.UseSkill, Database.SWIFT_STEP);
                    }
                    else if ((player.CurrentVoidExtraction <= 0) &&
                             (player.CurrentSkillPoint >= Database.VOID_EXTRACTION_COST))
                    {
                        ExecActionMethod(player, player, PlayerAction.UseSkill, Database.VOID_EXTRACTION);
                    }
                    else if ((player.CurrentSkillPoint >= Database.WORD_OF_POWER_COST))
                    {
                        ExecActionMethod(player, mc, PlayerAction.UseSpell, Database.WORD_OF_POWER);
                    }
                }
            }
            #endregion
            #region "���_�E�~�X�g�D���X"
            else if (player.Name == Database.DUEL_LADA_MYSTORUS)
            {
            }
            #endregion
            #region "�V���E�I�X�L�����[�e"
            else if (player.Name == Database.DUEL_SIN_OSCURETE)
            {
                if ((player.CurrentInstantPoint >= player.MaxInstantPoint) &&
                    (mc.CurrentInstantPoint < 500) &&
                    (mc.CurrentMana > 0))
                {
                    if ((player.CurrentAntiStun <= 0) &&
                        (player.CurrentSkillPoint >= Database.ANTI_STUN_COST))
                    {
                        ExecActionMethod(player, player, PlayerAction.UseSkill, Database.ANTI_STUN);
                    }
                    else if ((player.CurrentOneImmunity <= 0) &&
                             (player.CurrentMana >= Database.ONE_IMMUNITY_COST))
                    {
                        ExecActionMethod(player, player, PlayerAction.UseSpell, Database.ONE_IMMUNITY);
                    }
                    else if (player.CurrentSkillPoint <= 30)
                    {
                        ExecActionMethod(player, player, PlayerAction.UseSkill, Database.INNER_INSPIRATION);
                    }
                    // ���̎��_�ł͋������邽�߁A�R�����g�A�E�g
                    //if ((player.CurrentTimeStop <= 0) &&
                    //    (player.CurrentMana >= Database.TIME_STOP_COST) &&
                    //    (player.CurrentWordOfLife <= 0))
                    //{
                    //    ExecActionMethod(player, player, PlayerAction.UseSpell, Database.TIME_STOP);
                    //}
                }
            }
            #endregion
            #region "�y�S�K�z���M�B���A�[�["
            else if (player.Name == Database.ENEMY_BOSS_LEGIN_ARZE ||
                     player.Name == Database.ENEMY_BOSS_LEGIN_ARZE_1 ||
                     player.Name == Database.ENEMY_BOSS_LEGIN_ARZE_2 ||
                     player.Name == Database.ENEMY_BOSS_LEGIN_ARZE_3)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    ExecActionMethod(player, null, PlayerAction.SpecialSkill, "�����̌ۓ�");
                }
            }
            #endregion
            #region "���i�E�A�~���A(DUEL)"
            else if (player.Name == Database.ENEMY_LAST_RANA_AMILIA)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if (!((TruthEnemyCharacter)player).StillNotAction1 &&
                        player.CurrentMana >= Database.TIME_STOP_COST)
                    {
                        ((TruthEnemyCharacter)player).StillNotAction1 = true;
                        ExecActionMethod(player, player, PlayerAction.UseSpell, Database.TIME_STOP);
                    }
                    else
                    {
                        if ((player.CurrentOneImmunity <= 0) &&
                            (player.CurrentMana >= Database.ONE_IMMUNITY_COST))
                        {
                            ExecActionMethod(player, player, PlayerAction.UseSpell, Database.ONE_IMMUNITY);
                        }
                        else if ((player.CurrentVoidExtraction <= 0) && (player.CurrentSkillPoint >= Database.VOID_EXTRACTION_COST))
                        {
                            ExecActionMethod(player, player, PlayerAction.UseSkill, Database.VOID_EXTRACTION);
                        }
                        else if ((player.CurrentPromisedKnowledge <= 0) && (player.CurrentMana >= Database.PROMISED_KNOWLEDGE_COST))
                        {
                            ExecActionMethod(player, player, PlayerAction.UseSpell, Database.PROMISED_KNOWLEDGE);
                        }
                        else if ((mc.CurrentImpulseHitValue < 3) && (player.CurrentSkillPoint >= Database.IMPULSE_HIT_COST))
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSkill, Database.IMPULSE_HIT);
                        }
                        else if ((mc.CurrentMana >= mc.MaxMana / 5) && (player.CurrentMana >= Database.DOOM_BLADE_COST))
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSpell, Database.DOOM_BLADE);
                        }
                        else
                        {
                            switch (AP.Math.RandomInteger(2))
                            {
                                case 0:
                                    if (player.CurrentMana >= Database.WHITE_OUT_COST)
                                    {
                                        ExecActionMethod(player, mc, PlayerAction.UseSpell, Database.WHITE_OUT);
                                    }
                                    break;
                                case 1:
                                    if (player.CurrentSkillPoint >= Database.ENIGMA_SENSE_COST)
                                    {
                                        ExecActionMethod(player, mc, PlayerAction.UseSkill, Database.ENIGMA_SENSE);
                                    }
                                    break;
                            }
                        }
                    }
                }
            }
            #endregion
            #region "�V�j�L�A�E�J�[���n���c(DUEL2)"
            else if (player.Name == Database.ENEMY_LAST_SINIKIA_KAHLHANZ)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if (player.BattleBarPos <= 250)
                    {
                        if ((player.CurrentOneImmunity <= 0) &&
                            (player.CurrentMana >= Database.ONE_IMMUNITY_COST))
                        {
                            ExecActionMethod(player, player, PlayerAction.UseSpell, Database.ONE_IMMUNITY);
                        }
                        //else if ((player.CurrentLife <= player.MaxLife / 2) && (player.CurrentMana >= Database.LIFE_TAP_COST))
                        //{
                        //    ExecActionMethod(player, player, PlayerAction.UseSpell, Database.LIFE_TAP);
                        //}
                        //else if ((player.CurrentPhantasmalWind <= 0) && (player.CurrentMana >= Database.PHANTASMAL_WIND_COST))
                        //{
                        //    ExecActionMethod(player, player, PlayerAction.UseSpell, Database.PHANTASMAL_WIND);
                        //}
                        //else if ((player.CurrentMana >= Database.WARP_GATE_COST) && (player.BattleBarPos > Database.BASE_TIMER_BAR_LENGTH / 2))
                        //{
                        //    ExecActionMethod(player, player, PlayerAction.UseSpell, Database.WARP_GATE);
                        //}   
                        else if (player.MaxLife * 0.0f <= player.CurrentLife && player.CurrentLife < player.MaxLife * 0.8f && player.CurrentMana >= Database.LIFE_TAP_COST)
                        {
                            ExecActionMethod(player, player, PlayerAction.UseSpell, Database.LIFE_TAP);
                        }
                        else if ((mc.CurrentSigilOfHomura <= 0) && (player.CurrentMana >= Database.SIGIL_OF_HOMURA_COST))
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSpell, Database.SIGIL_OF_HOMURA);
                        }
                        //else if ((player.CurrentRedDragonWill <= 0) && (player.CurrentMana >= Database.RED_DRAGON_WILL_COST))
                        //{
                        //    ExecActionMethod(player, player, PlayerAction.UseSpell, Database.RED_DRAGON_WILL);
                        //}
                        //else if ((mc.CurrentEnrageBlast <= 0) && (player.CurrentMana >= Database.ENRAGE_BLAST_COST))
                        //{
                        //    ExecActionMethod(player, mc, PlayerAction.UseSpell, Database.ENRAGE_BLAST);
                        //}

                        //else if ((player.CurrentPromisedKnowledge <= 0) && (player.CurrentMana >= Database.PROMISED_KNOWLEDGE_COST))
                        //{
                        //    ExecActionMethod(player, player, PlayerAction.UseSpell, Database.PROMISED_KNOWLEDGE);
                        //}
                        //else if ((mc.CurrentImpulseHitValue < 3) && (player.CurrentSkillPoint >= Database.IMPULSE_HIT_COST))
                        //{
                        //    ExecActionMethod(player, mc, PlayerAction.UseSkill, Database.IMPULSE_HIT);
                        //}
                        //else if ((mc.CurrentMana >= mc.MaxMana / 5) && (player.CurrentMana >= Database.DOOM_BLADE_COST))
                        //{
                        //    ExecActionMethod(player, mc, PlayerAction.UseSpell, Database.DOOM_BLADE);
                        //}
                        //else
                        //{
                        //    switch (AP.Math.RandomInteger(2))
                        //    {
                        //        case 0:
                        //            if (player.CurrentMana >= Database.WHITE_OUT_COST)
                        //            {
                        //                ExecActionMethod(player, mc, PlayerAction.UseSpell, Database.WHITE_OUT);
                        //            }
                        //            break;
                        //        case 1:
                        //            if (player.CurrentSkillPoint >= Database.ENIGMA_SENSE_COST)
                        //            {
                        //                ExecActionMethod(player, mc, PlayerAction.UseSkill, Database.ENIGMA_SENSE);
                        //            }
                        //            break;
                        //    }
                        //}
                    }
                }
            }
            #endregion
            #region "�I���E�����f�B�X(DUEL2)"
            else if (player.Name == Database.ENEMY_LAST_OL_LANDIS)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if (!((TruthEnemyCharacter)player).OpponentUseInstantPoint)
                    {
                        // �ΐ푊�肪�C���X�^���g����ĂȂ��ꍇ�A�������Ȃ�
                    }
                    else
                    {
                        if (player.BeforeSkillName == Database.SOUL_EXECUTION)
                        {
                            ExecActionMethod(player, player, PlayerAction.UseSpell, Database.GENESIS);
                        }
                        else if ((player.CurrentLife < player.MaxLife / 2) && (mc.CurrentParalyze <= 0) && ((TruthEnemyCharacter)player).DetectCannotBeParalyze == false && player.CurrentSkillPoint >= Database.SURPRISE_ATTACK_COST)
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSkill, Database.SURPRISE_ATTACK);
                        }
                        else if ((player.CurrentLife < player.MaxLife / 2) && (player.CurrentMana >= Database.CELESTIAL_NOVA_COST))
                        {
                            ExecActionMethod(player, player, PlayerAction.UseSpell, Database.CELESTIAL_NOVA);
                        }
                        else if ((player.CurrentMana >= Database.DISPEL_MAGIC_COST) &&
                                 ((mc.CurrentSaintPower > 0) || (mc.CurrentHeatBoost > 0) || (mc.CurrentFlameAura > 0) || (mc.CurrentHolyBreaker > 0)))
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSpell, Database.DISPEL_MAGIC);
                        }
                        else if ((player.CurrentMana >= Database.TRANQUILITY_COST) &&
                                ((mc.CurrentGaleWind > 0) || (mc.CurrentWordOfFortune > 0)))
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSpell, Database.TRANQUILITY);
                        }
                        else if ((player.CurrentBlackContract <= 0) && (player.CurrentMana >= Database.BLACK_CONTRACT_COST))
                        {
                            ExecActionMethod(player, player, PlayerAction.UseSpell, Database.BLACK_CONTRACT);
                        }
                        else if ((player.CurrentFlameAura <= 0) && ((player.CurrentBlackContract > 0) || (player.CurrentMana >= Database.FLAME_AURA_COST)))
                        {
                            ExecActionMethod(player, player, PlayerAction.UseSpell, Database.FLAME_AURA);
                        }
                        else if ((player.CurrentGaleWind <= 0) && ((player.CurrentBlackContract > 0) || (player.CurrentMana >= Database.GALE_WIND_COST)))
                        {
                            ExecActionMethod(player, player, PlayerAction.UseSpell, Database.GALE_WIND);
                        }
                        else if ((mc.CurrentImpulseHitValue < 2) && ((player.CurrentBlackContract > 0) || (player.CurrentSkillPoint >= Database.IMPULSE_HIT_COST)))
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSkill, Database.IMPULSE_HIT);
                        }
                        else if ((mc.CurrentOnslaughtHitValue < 2) && ((player.CurrentBlackContract > 0) || (player.CurrentSkillPoint >= Database.ONSLAUGHT_HIT_COST)))
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSkill, Database.ONSLAUGHT_HIT);
                        }
                        else if ((mc.CurrentConcussiveHitValue < 2) && ((player.CurrentBlackContract > 0) || (player.CurrentSkillPoint >= Database.ONSLAUGHT_HIT_COST)))
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSkill, Database.CONCUSSIVE_HIT);
                        }
                    }
                }
            }
            #endregion
            #region "���F���[�E�A�[�e�B�ŏI��(DUEL)"
            else if (player.Name == Database.ENEMY_LAST_VERZE_ARTIE)
            {
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if (((TruthEnemyCharacter)player).AI_TacticsNumber == 1)
                    {
                        if (player.CurrentFlameAura > 0 && player.CurrentFrozenAura > 0 && player.CurrentGaleWind > 0)
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSkill, Database.NEUTRAL_SMASH);
                        }
                    }
                    else if (((TruthEnemyCharacter)player).AI_TacticsNumber == 9)
                    {
                        if (player.BeforeSpellName == Database.ZETA_EXPLOSION)
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSpell, Database.GENESIS);
                        }
                    }
                }
            }
            #endregion
            #region "�y���߁z���F���[�E�A�[�e�B�ŏI��Q(DUEL)"
            else if (player.Name == Database.ENEMY_LAST_SIN_VERZE_ARTIE)
            {
                if (player.CurrentSpecialInstant >= player.MaxSpecialInstant)
                {
                    UseSpecialInstant(player);
                    if (player.CurrentLifeCountValue == 3)
                    {
                        if (player.CurrentEclipseEnd > 0)
                        {
                            ExecActionMethod(player, player, PlayerAction.SpecialSkill, Database.FINAL_LADARYNTE_CHAOTIC_SCHEMA);
                        }
                        else
                        {
                            ExecActionMethod(player, mc, PlayerAction.SpecialSkill, Database.FINAL_INVISIBLE_HUNDRED_CUTTER);
                        }
                    }
                    else if (player.CurrentLifeCountValue == 2)
                    {
                        ExecActionMethod(player, player, PlayerAction.SpecialSkill, Database.FINAL_LADARYNTE_CHAOTIC_SCHEMA);
                    }
                    else if (player.CurrentLifeCountValue == 1)
                    {
                        ExecActionMethod(player, mc, PlayerAction.SpecialSkill, Database.FINAL_ZERO_INNOCENT_SIN);
                    }
                    else if (player.CurrentLifeCountValue <= 0)
                    {
                        if (player.CurrentLife <= player.MaxLife * 0.5f)
                        {
                            ExecActionMethod(player, mc, PlayerAction.SpecialSkill, Database.FINAL_SEFINE_PAINFUL_HYMNUS);
                        }
                        else
                        {
                            ExecActionMethod(player, player, PlayerAction.SpecialSkill, Database.FINAL_LADARYNTE_CHAOTIC_SCHEMA);
                            // Database.FINAL_ADEST_ESPELANTIE;
                        }
                    }
                }
                else if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    if (player.BattleBarPos <= 200)
                    {
                        if (player.CurrentGaleWind <= 0 && player.CurrentMana >= Database.GALE_WIND_COST)
                        {
                            ExecActionMethod(player, player, PlayerAction.UseSpell, Database.GALE_WIND);
                        }
                        else if (player.MaxLife * 0.4f <= player.CurrentLife && player.CurrentLife < player.MaxLife * 0.5f && player.CurrentMana >= Database.CELESTIAL_NOVA_COST)
                        {
                            ExecActionMethod(player, player, PlayerAction.UseSpell, Database.CELESTIAL_NOVA);
                        }
                        else
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSkill, Database.NEUTRAL_SMASH);
                        }
                    }
                    else if (320 < player.BattleBarPos && player.BattleBarPos <= 350 && AP.Math.RandomInteger(40) == 0)
                    {
                        if (player.CurrentMana >= Database.WARP_GATE_COST)
                        {
                            ExecActionMethod(player, mc, PlayerAction.UseSpell, Database.WARP_GATE);
                        }
                    }
                }
                else if (player.CurrentFrozen > 0 || player.CurrentParalyze > 0 || player.CurrentStunning > 0)
                {
                    ExecActionMethod(player, player, PlayerAction.UseSkill, Database.RECOVER);
                }
            }
            #endregion
            #region "�x�z��"
            else if (player.Name == Database.ENEMY_DRAGON_SOKUBAKU_BRIYARD ||
                     player.Name == Database.ENEMY_DRAGON_TINKOU_DEEPSEA ||
                     player.Name == Database.ENEMY_DRAGON_DESOLATOR_AZOLD ||
                     player.Name == Database.ENEMY_DRAGON_IDEA_CAGE_ZEED ||
                     player.Name == Database.ENEMY_DRAGON_ALAKH_VES_T_ETULA)
            {
                if (((TruthEnemyCharacter)player).AI_TacticsNumber == 5)
                {
                    ExecActionMethod(player, mc, PlayerAction.SpecialSkill, "�`������");
                }
            }
            #endregion
            #region "Bystander Emptiness"
            else if (player.Name == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
            {
                TruthEnemyCharacter current = (TruthEnemyCharacter)player;
                if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                {
                    UseInstantPoint(player);
                    player.StackActivePlayer = ec1;
                    player.StackTarget = null;
                    player.StackPlayerAction = PlayerAction.SpecialSkill;
                    int rand = AP.Math.RandomInteger(4);
                    if (rand == 0) { player.StackTarget = current.Targetting(mc, sc, tc); player.StackCommandString = "�L���E�X�s�j���O�����T�["; }
                    else if (rand == 1) { player.StackCommandString = "�A�[�X�E�R�[���h�E�V�F�C�N"; }
                    else if (rand == 2) { player.StackCommandString = "�^�C�_���E�E�F�C�u"; }
                    else if (rand == 3) { player.StackCommandString = "�����̌ۓ�"; }
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
            }
            #endregion
            #region "�_�~�[�f�U��N"
            else if (player.Name == Database.DUEL_DUMMY_SUBURI)
            {
                //if (player.CurrentInstantPoint >= player.MaxInstantPoint)
                //{
                //    //if (player.CurrentTimeStop > 0)
                //    {
                //        UseInstantPoint(player);
                //        player.StackActivePlayer = ec1;
                //        player.StackTarget = mc;
                //        player.StackPlayerAction = PlayerAction.UseSpell;
                //        player.StackCommandString = Database.IMMOLATE;
                //        player.StackActivation = true;
                //        this.NowStackInTheCommand = true;
                //    }
                //}
            }
            #endregion

        }
        
        private void UpdateUseItemGauge()
        {
            if (UseItemGauge.Width < 600) UseItemGauge.Width += 1;
        }

        public enum MethodType
        {
            Beginning,
            AfterBattleEffect,
            // UpdateTurnEnd,
            CleanUpStep,
            UpKeepStep,
            //UpdateUseItemGauge,
            PlayerAttackPhase,
            // UpdatePlayerTarget,
            // UpdatePlayerInstantPoint,
            // UpdatePlayerGaugePosition,
            // UpdatePlayerNextDecision,
            // UpdatePlayerDoStackInTheCommand,
            // UpdatePlayerDeadFlag,
            CleanUpForBoss,
            TimeStopEnd,
        }

        private bool ExecPhaseElement(MethodType method, MainCharacter player)
        {
            switch (method)
            {
                case MethodType.Beginning:
                    Beginning();
                    break;
                case MethodType.AfterBattleEffect:
                    AfterBattleEffect();
                    break;
                case MethodType.CleanUpStep:
                    CleanUpStep();
                    break;
                case MethodType.UpKeepStep:
                    UpkeepStep();
                    break;
                case MethodType.PlayerAttackPhase:
                    PlayerAttackPhase(player, false, false, true);
                    break;
                case MethodType.CleanUpForBoss:
                    CleanUpForBoss();
                    break;
                case MethodType.TimeStopEnd:
                    TimeStopEnd();
                    break;
            }
            if (UpdatePlayerDeadFlag()) return false;

            CheckStackInTheCommand();
            if (UpdatePlayerDeadFlag()) return false;
            return true;
        }

        private void TimeStopEnd()
        {
            bool tempStop = false;
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                for (int jj = 0; jj < ActiveList[ii].ActionCommandStackList.Count; jj++)
                {
                    if (ActiveList[ii].Name == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
                    {
                        if (tempStop == false)
                        {
                            tempStop = true;
                            this.labelBattleTurn.BackColor = Color.White;
                            this.labelBattleTurn.ForeColor = Color.Black;
                            System.Threading.Thread.Sleep(1000);
                            this.labelBattleTurn.BackColor = Color.GhostWhite;
                            this.labelBattleTurn.ForeColor = Color.Black;
                        }
                        PlayerAttackPhase(ActiveList[ii], ActiveList[ii].ActionCommandStackTarget[jj], TruthActionCommand.CheckPlayerActionFromString(ActiveList[ii].ActionCommandStackList[jj]), ActiveList[ii].ActionCommandStackList[jj], true, false, false);
                    }
                    else
                    {
                        ExecActionMethod(ActiveList[ii], ActiveList[ii].ActionCommandStackTarget[jj], TruthActionCommand.CheckPlayerActionFromString(ActiveList[ii].ActionCommandStackList[jj]), ActiveList[ii].ActionCommandStackList[jj]);
                    }
                }
                ActiveList[ii].ActionCommandStackList.Clear();
                ActiveList[ii].ActionCommandStackTarget.Clear();
            }

            this.BackColor = Color.GhostWhite;
            this.labelBattleTurn.ForeColor = Color.Black;
            this.TimeSpeedLabel.ForeColor = Color.Black;
            this.lblTimerCount.ForeColor = Color.Black;
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                BackToNormalColor(ActiveList[ii]);

                //Type type = ActiveList[ii].GetType();
                //FieldInfo[] fields = type.GetFields();
                //foreach (FieldInfo info in fields)
                //{
                //    if (info.FieldType == typeof(System.Windows.Forms.Label))
                //    {
                //        Label obj = (Label)type.GetField(info.Name).GetValue(ActiveList[ii]);
                //        if (obj != null)
                //        {
                //            obj.ForeColor = Color.FromArgb(System.Math.Abs(255 - obj.ForeColor.R), Math.Abs(255 - obj.ForeColor.G), Math.Abs(255 - obj.ForeColor.B));
                //        }
                //    }
                //}
            }

        }

        private void CheckStackInTheCommand()
        {
            if (this.NowStackInTheCommand)
            {
                this.BattleMenuPanel.Visible = false;
                StackThread = new Thread(new System.Threading.ThreadStart(StackInTheCommand));
                StackThread.Priority = ThreadPriority.Highest;
                StackThread.Start();
                StackThread.Join();
                StackThread = null;

                this.NowStackInTheCommand = false;

                CompleteInstantAction();
            }
        }

        private void UpdateTurnEnd(bool cancelCounterClear = false)
        {
            this.BattleTurnCount++;
            this.labelBattleTurn.Text = "�^�[�� " + BattleTurnCount.ToString();
            this.labelBattleTurn.Update();
            if (cancelCounterClear == false) { this.BattleTimeCounter = 0; }
        }

        private void UpdatePlayerPreCondition(MainCharacter player, int arrowType)
        {
            // StanceOfFlow���L�L�q
            if (this.StayOn_StanceOfFlow)
            {
                this.BreakOn_StanceOfFlow = true;
            }

            if (arrowType == 0) { player.BattleBarPos = 0; }
            else if (arrowType == 1) { player.BattleBarPos2 = 0; }
            else if (arrowType == 2) { player.BattleBarPos3 = 0; }
            player.ActionDecision = false;
            // player.CurrentCounterAttack = false; // ���̃R�}���h�����s������J�E���^�[�����ł��Ă��܂��̂̓Q�[��������A�������낭�Ȃ��B
        }

        private void UpdatePlayerNextDecision(MainCharacter player)
        {
            if (player == mc || player == sc || player == tc) return; // �R���s���[�^�[��p���[�`���̂��߁A�v���C���[���͉������Ȃ��B
           
            if (player.Name == Database.DUEL_OL_LANDIS) // �I���E�����f�B�X�͏�ɐ�p��ύX�\�Ƃ���B���F���[�Ȃǎ�v�l���͑S�ĊY���B
            {
                ((TruthEnemyCharacter)player).NextAttackDecision(mc, mc, sc, tc, ec1, ec2, ec3);
            }
            else if (player.Name == Database.VERZE_ARTIE_FULL || player.Name == Database.VERZE_ARTIE
                  || player.Name == Database.ENEMY_LAST_RANA_AMILIA
                  || player.Name == Database.ENEMY_LAST_SINIKIA_KAHLHANZ
                  || player.Name == Database.ENEMY_LAST_OL_LANDIS
                  || player.Name == Database.ENEMY_LAST_VERZE_ARTIE
                  || player.Name == Database.ENEMY_LAST_SIN_VERZE_ARTIE)
            {
                ((TruthEnemyCharacter)player).NextAttackDecision(mc, mc, sc, tc, ec1, ec2, ec3);
            }
            else if ((player.Name == Database.DUEL_SHUVALTZ_FLORE) ||
                     (player.Name == Database.DUEL_SIN_OSCURETE))
            {
                ((TruthEnemyCharacter)player).NextAttackDecision(mc, mc, sc, tc, ec1, ec2, ec3);
            }
            else
            {
                if ((!player.ActionDecision && player.BattleBarPos > player.DecisionTiming) ||
                    ((TruthEnemyCharacter)player).Name == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
                {
                    player.ActionDecision = true;

                    if (((TruthEnemyCharacter)player).InitialTarget == TruthEnemyCharacter.TargetLogic.Front)
                    {
                        if (mc != null && !mc.Dead)
                        {
                            ((TruthEnemyCharacter)player).NextAttackDecision(mc, mc, sc, tc, ec1, ec2, ec3);
                        }
                        else if (sc != null && !sc.Dead)
                        {
                            ((TruthEnemyCharacter)player).NextAttackDecision(sc, mc, sc, tc, ec1, ec2, ec3);
                        }
                        else if (tc != null && !tc.Dead)
                        {
                            ((TruthEnemyCharacter)player).NextAttackDecision(tc, mc, sc, tc, ec1, ec2, ec3);
                        }
                    }
                    else if (((TruthEnemyCharacter)player).InitialTarget == TruthEnemyCharacter.TargetLogic.Back)
                    {
                        if (tc != null && !tc.Dead)
                        {
                            ((TruthEnemyCharacter)player).NextAttackDecision(tc, mc, sc, tc, ec1, ec2, ec3);
                        }
                        else if (sc != null && !sc.Dead)
                        {
                            ((TruthEnemyCharacter)player).NextAttackDecision(sc, mc, sc, tc, ec1, ec2, ec3);
                        }
                        else if (mc != null && !mc.Dead)
                        {
                            ((TruthEnemyCharacter)player).NextAttackDecision(mc, mc, sc, tc, ec1, ec2, ec3);
                        }
                    }
                    player.ActionLabel.Update();
                }
            }
        }

        private void UpdatePlayerInstantPoint(MainCharacter player)
        {
            if (player.CurrentFrozen > 0)
            {
                return;
            }
            if (player.CurrentStunning > 0)
            {
                return;
            }
            if (player.CurrentParalyze > 0)
            {
                return;
            }
            if (player.CurrentStarLightning > 0)
            {
                return;
            }

            if (player.labelCurrentInstantPoint != null)
            {
                if (player.CurrentInstantPoint < player.MaxInstantPoint)
                {
                    player.CurrentInstantPoint += PrimaryLogic.BattleResponseValue(player, this.DuelMode);
                }
                player.labelCurrentInstantPoint.Text = ((int)player.CurrentInstantPoint).ToString() + " / " + player.MaxInstantPoint;
            }

            if (player.labelCurrentSpecialInstant != null)
            {
                if (player.CurrentSpecialInstant < player.MaxSpecialInstant)
                {
                    player.CurrentSpecialInstant += PrimaryLogic.BattleResponseValue(player, this.DuelMode);
                }
                player.labelCurrentSpecialInstant.Text = ((int)player.CurrentSpecialInstant).ToString() + " / " + player.MaxSpecialInstant;
            }
        }

        private void UpdatePlayerGaugePosition(MainCharacter player)
        {
            if (player.CurrentFrozen > 0)
            {
                return;
            }
            if (player.CurrentStunning > 0)
            {
                return;
            }
            if (player.CurrentParalyze > 0)
            {
                return;
            }
            if (player.CurrentStarLightning > 0)
            {
                return;
            }
            double movement = PrimaryLogic.BattleSpeedValue(player, this.DuelMode);
            if (player.Name == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
            {
                TruthEnemyCharacter player2 = ((TruthEnemyCharacter)player);
                movement = movement + Math.Log((double)(1 + this.BattleTurnCount)) / 3;
            }
            if (player.CurrentSlow > 0)
            {
                movement = movement * 2.0f / 3.0f;
            }
            if (player.CurrentSpeedBoost > 0)
            {
                player.CurrentSpeedBoost--;
                movement = movement + 2;
            }
            if (player.CurrentSwiftStep > 0)
            {
                movement = movement * 1.3f;
            }
            if (player.CurrentSmoothingMove > 0)
            {
                movement = movement * 2.0f;
            }
            if (player.CurrentJuzaPhantasmal > 0)
            {
                movement = movement * PrimaryLogic.JuzaPhantasmalValue(player);
            }
            player.BattleBarPos += movement;
            if (player.BattleBarPos > Database.BASE_TIMER_BAR_LENGTH)
            {
                player.BattleBarPos = Database.BASE_TIMER_BAR_LENGTH + 1;
            }
            // �ŏI��J�I�e�B�b�N�X�L�[�}
            if (player.CurrentChaoticSchema > 0)
            {
                player.BattleBarPos2 += movement;
                if (player.BattleBarPos2 > Database.BASE_TIMER_BAR_LENGTH)
                {
                    player.BattleBarPos2 = Database.BASE_TIMER_BAR_LENGTH + 1;
                }

                if (player.CurrentLifeCountValue <= 1)
                {
                    player.BattleBarPos3 += movement;
                    if (player.BattleBarPos3 > Database.BASE_TIMER_BAR_LENGTH)
                    {
                        player.BattleBarPos3 = Database.BASE_TIMER_BAR_LENGTH + 1;
                    }
                }
            }

            // StanceOfFlow���L�̃|�W�V�����X�V
            if ((player.CurrentStanceOfFlow > 0) && (player.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH))
            {
                if (this.StayOn_StanceOfFlow == false && this.BreakOn_StanceOfFlow == false)
                {
                    this.StayOn_StanceOfFlow = true;
                    player.BattleBarPos = Database.BASE_TIMER_BAR_LENGTH;
                }
                else
                {
                    if (this.BreakOn_StanceOfFlow == false)
                    {
                        player.BattleBarPos = Database.BASE_TIMER_BAR_LENGTH;
                    }
                    else
                    {
                        this.StayOn_StanceOfFlow = false;
                        this.BreakOn_StanceOfFlow = false;
                    }
                }
            }
        }

        /// <summary>
        /// "[��ҕK�{]�������A�p�[�e�B�Ґ����\�ɂ��邱�Ƃ�z�肷��ƁA���̂܂܂ł͂����Ȃ��͂��B"
        /// </summary>
        /// <param name="mainCharacter"></param>
        private void UpdatePlayerTarget(MainCharacter mainCharacter)
        {
            if (mainCharacter == ec1 || mainCharacter == ec2 || mainCharacter == ec3)
            {
                if (this.DuelMode)
                {
                    // Duel���[�h�̏ꍇ�A�Ȃɂ����Ȃ��B
                }
                else if (we.AvailableSecondCharacter == false && we.AvailableThirdCharacter == false)
                {
                    // ������l�̏ꍇ�A�Ȃɂ����Ȃ��B
                }
                else if (we.AvailableSecondCharacter == true && we.AvailableThirdCharacter == false)
                {
                    // ������l�̏ꍇ�A����łȂ��ق��֐؂�ւ���B
                    if (mc != null && mc.Dead) mainCharacter.Target = sc;
                    else if(sc != null & sc.Dead) mainCharacter.Target = mc;
                }
                else
                {
                    List<MainCharacter> group = new List<MainCharacter>();
                    if (mc != null && !mc.Dead) { group.Add(mc); }
                    if (sc != null && !sc.Dead) { group.Add(sc); }
                    if (tc != null && !tc.Dead) { group.Add(tc); }
                    if (((TruthEnemyCharacter)mainCharacter).InitialTarget == TruthEnemyCharacter.TargetLogic.Front)
                    {
                        mainCharacter.Target = group[0];
                    }
                    else
                    {
                        mainCharacter.Target = group[group.Count - 1];
                    }
                }

                // �G���̏ꍇ�A�v���C���[���֍s��������̍s���w�j��ҋ@�ɂ������Ƃ�`���邽�߁B
                if (mainCharacter.FullName == Database.DUEL_CALMANS_OHN)
                {
                    mainCharacter.PA = PlayerAction.Defense;
                    mainCharacter.ActionLabel.Text = Database.DEFENSE_JP;
                }
                else
                {
                    mainCharacter.ActionLabel.Text = Database.STAY_JP;
                }
            }
            else
            {
                if (ec2 == null && ec3 == null)
                {
                    // �G��l�̏ꍇ�A�Ȃɂ����Ȃ��B
                }
                else if (ec2 != null && ec3 == null)
                {
                    // �G��l�̏ꍇ�A����łȂ��ق��֐؂�ւ���B
                    if (ec1 != null && ec1.Dead) mainCharacter.Target = ec2;
                    else if (ec2 != null && ec2.Dead) mainCharacter.Target = ec1;
                }
                else
                {
                    List<MainCharacter> group = new List<MainCharacter>();
                    if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                    if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                    if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
                    mainCharacter.Target = group[0];
                }
            }
        }

        /// <summary>
        /// �����FOK
        /// �����FIgnore
        /// ������FAbort
        /// </summary>
        delegate void _BattleEndPhase();
        private void BattleEndPhase()
        {
            this.Update(); // ��������Ȃ��ƃX���b�h�����ł̉�ʕ`�悪�ǂ����Ă��Ȃ��B
            
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                string brokenName = String.Empty;
                ActiveList[ii].CleanUpBattleEnd(ref brokenName);
                if (brokenName != String.Empty)
                {
                    // �j�������A�C�e�������o���Ă��ǂ����A���O����������ꍇ�A�ǂ߂Ȃ��̂ŁA�A�C�e�����\���͕s�v�Ɣ��f�B
                    this.Invoke(new _AnimationDamage(AnimationDamage), 0, ActiveList[ii], 200, Color.Red, false, false, Database.BROKEN_ITEM);
                }
            }
            
            // �x�z����b�I�����A�ʏ�I���Ƃ݂Ȃ��B
            if (this.endBattleForMatrixDragonEnd)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            // [�x��]������A���ł��̏ꍇ�A�v���C���[�̕����Ƃ݂Ȃ�
            else if (EnemyPartyDeathCheck())
            {
                if (this.DuelMode)
                {
                    txtBattleMessage.Text = txtBattleMessage.Text.Insert(0, "�A�C����DUEL�ɔs�ꂽ�I\r\n");
                    txtBattleMessage.Update();
                    System.Threading.Thread.Sleep(1000);
                    this.DialogResult = System.Windows.Forms.DialogResult.Ignore;
                }
                else
                {
                    txtBattleMessage.Text = txtBattleMessage.Text.Insert(0, "�S�ł��܂����E�E�E������x���̐퓬����蒼���܂����H\r\n");
                    txtBattleMessage.Update();
                    yesno.ShowDialog();
                    if (yesno.DialogResult == DialogResult.Yes)
                    {
                        this.DialogResult = DialogResult.Retry;
                    }
                    else
                    {
                        this.DialogResult = DialogResult.Ignore;
                    }
                }
            }
            else if (endFlag)
            {
                if (!we.AvailableSecondCharacter)
                {
                    if (this.DuelMode)
                    {
                        txtBattleMessage.Text = txtBattleMessage.Text.Insert(0, "�A�C���͍~�Q��錾�����B\r\n");
                    }
                    else
                    {
                        txtBattleMessage.Text = txtBattleMessage.Text.Insert(0, "�A�C���͓����o�����B\r\n");
                    }
                }
                else
                {
                    if (this.DuelMode)
                    {
                        txtBattleMessage.Text = txtBattleMessage.Text.Insert(0, "�A�C���͍~�Q��錾�����B\r\n");
                    }
                    else
                    {
                        txtBattleMessage.Text = txtBattleMessage.Text.Insert(0, "�A�C���B�͓����o�����B\r\n");
                    }
                }
                txtBattleMessage.Update();
                System.Threading.Thread.Sleep(1000);
                this.DialogResult = System.Windows.Forms.DialogResult.Abort;
            }
            else
            {
                txtBattleMessage.Text = txtBattleMessage.Text.Insert(0, "�G��|�����B\r\n");
                txtBattleMessage.Update();
                System.Threading.Thread.Sleep(1000);

                // �G���ăJ�E���g�𐔂���B
                GroundOne.WE2.KillingEnemy++;

                // ���K�p�̌��J�E���g�𐔂���B
                if (mc != null)
                {
                    if ((mc.MainWeapon != null) && (mc.MainWeapon.Name == Database.POOR_PRACTICE_SWORD_ZERO) ||
                        (mc.SubWeapon != null) && (mc.SubWeapon.Name == Database.POOR_PRACTICE_SWORD_ZERO))
                    {
                        GroundOne.WE2.PracticeSwordCount++;
                    }
                }

                if (this.DuelMode)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    return;
                }

                string targetItemName = Method.GetNewItem(Method.NewItemCategory.Battle, mc, ec1, we.DungeonArea);

                using (MessageDisplayWithIcon mdwi = new MessageDisplayWithIcon())
                {
                    if (targetItemName != string.Empty)
                    {
                        ItemBackPack item = new ItemBackPack(targetItemName);
                        mdwi.Message = item.Name + "����肵���I";
                        mdwi.Item = item;
                        if (mdwi.Item.Rare == ItemBackPack.RareLevel.Epic)
                        {
                            GroundOne.PlaySoundEffect(Database.SOUND_GET_EPIC_ITEM);
                        }
                        else if (mdwi.Item.Rare == ItemBackPack.RareLevel.Rare)
                        {
                            GroundOne.PlaySoundEffect(Database.SOUND_GET_RARE_ITEM);
                        }
                        mdwi.StartPosition = FormStartPosition.CenterParent;
                        mdwi.ShowDialog();

                        if (GetNewItem(item))
                        {
                            // �o�b�N�p�b�N���󂢂Ăē���\�ȏꍇ�A�����ł͉������Ȃ��B
                        }
                        else
                        {
                            // �o�b�N�p�b�N�������ς��̏ꍇ�X�e�[�^�X��ʂŕs�v�A�C�e�����̂Ă����܂��B
                            mdwi.Message = "�o�b�N�p�b�N�������ς��̂��߁A�X�e�[�^�X��ʂ��J���܂��B";
                            mdwi.Item = new ItemBackPack("");
                            mdwi.ShowDialog();
                            using (TruthStatusPlayer sp = new TruthStatusPlayer())
                            {
                                sp.MC = mc;
                                if (we.AvailableSecondCharacter)
                                {
                                    sp.SC = sc;
                                }
                                if (we.AvailableThirdCharacter)
                                {
                                    sp.TC = tc;
                                }
                                sp.WE = we;
                                sp.StartPosition = FormStartPosition.CenterParent;
                                sp.OnlySelectTrash = true;
                                sp.ShowDialog();
                                mc = sp.MC;
                                if (we.AvailableSecondCharacter)
                                {
                                    sc = sp.SC;
                                }
                                if (we.AvailableThirdCharacter)
                                {
                                    tc = sp.TC;
                                }
                                if (sp.DialogResult == DialogResult.Abort)
                                {
                                    this.DialogResult = DialogResult.Abort;
                                }
                            }
                            GetNewItem(item);
                        }
                    }
                }

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private bool GetNewItem(ItemBackPack newItem)
        {
            bool getOK = false;
            if (mc.AddBackPack(newItem) && getOK == false)
            {
                getOK = true;
            }
            if (we.AvailableSecondCharacter && getOK == false)
            {
                if (sc.AddBackPack(newItem))
                {
                    getOK = true;
                }
            }
            if (we.AvailableThirdCharacter && getOK == false)
            {
                if (tc.AddBackPack(newItem))
                {
                    getOK = true;
                }
            }

            return getOK;
        }

        /// <summary>
        /// �S�ł��Ă��邩�ǂ����𔻒肷��B�i�G�E�����܂ށj
        /// </summary>
        /// <returns>true:�S�ł��Ă���
        ///          false:�S�ł��Ă��Ȃ�</returns>
        private bool UpdatePlayerDeadFlag()
        {
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (ActiveList[ii].CurrentLife <= 0)
                {
                    if ((ActiveList[ii].Name == Database.ENEMY_BOSS_LEGIN_ARZE_3) &&
                        (!((TruthEnemyCharacter)ActiveList[ii]).DetectDeath))
                    {
                            ((TruthEnemyCharacter)ActiveList[ii]).DetectDeath = true;
                            UpdateBattleText(ActiveList[ii].Name + "�͎��̎��陋�߁A�[���̖h�ǂ���肾�����I�I\r\n");
                            this.Invoke(new _AnimationDamage(AnimationDamage), 0, ActiveList[ii], 0, Color.Black, true, false, "�[���̖h��");
                            ActiveList[ii].CurrentLife = 1;
                            UpdateLife(ActiveList[ii]);
                            ActiveList[ii].CurrentTheAbyssWall = Database.INFINITY;
                            ActiveList[ii].ActivateBuff(ActiveList[ii].pbTheAbyssWall, Database.BaseResourceFolder + Database.THE_ABYSS_WALL + ".bmp", Database.INFINITY);
                    }
                    else if (ActiveList[ii].CurrentGenseiTaima > 0)
                    {
                        ActiveList[ii].RemoveGenseiTaima();
                        UpdateBattleText(ActiveList[ii].Name + "�ɑ΂��đޖ��̌��ʂ��������A�v���̋��ԂŐ����c�����I�I\r\n");
                        this.Invoke(new _AnimationDamage(AnimationDamage), 0, ActiveList[ii], 0, Color.Black, true, false, "����");
                        ActiveList[ii].CurrentLife = ActiveList[ii].MaxLife / 2;
                        UpdateLife(ActiveList[ii]);
                    }
                    else if (ActiveList[ii].CurrentStanceOfDeath > 0)
                    {
                        ActiveList[ii].RemoveStanceOfDeath();
                        UpdateBattleText(ActiveList[ii].Name + "�͒v���̋��ԂŐ����c�����I�I\r\n");
                        this.Invoke(new _AnimationDamage(AnimationDamage), 0, ActiveList[ii], 0, Color.Black, true, false, "����");
                        ActiveList[ii].CurrentLife = 1;
                        UpdateLife(ActiveList[ii]);
                    }
                    else if (ActiveList[ii].CurrentShadowBible > 0)
                    {
                        ActiveList[ii].RemoveShadowBible();
                        UpdateBattleText(ActiveList[ii].Name + "�͒v���̋��Ԃł݂Ȃ��鐶���͂�����������I�I\r\n");
                        this.Invoke(new _AnimationDamage(AnimationDamage), 0, ActiveList[ii], 0, Color.Black, true, false, "����");
                        ActiveList[ii].CurrentLife = ActiveList[ii].MaxLife;
                        UpdateLife(ActiveList[ii]);
                        NowNoResurrection(ActiveList[ii], ActiveList[ii], 999);
                    }
                    else if (ActiveList[ii].CurrentAfterReviveHalf > 0)
                    {
                        ActiveList[ii].RemoveAfterReviveHalf();
                        UpdateBattleText(ActiveList[ii].Name + "�͒v���̋��ԂŐ����c�����I�I\r\n");
                        this.Invoke(new _AnimationDamage(AnimationDamage), 0, ActiveList[ii], 0, Color.Black, true, false, "����");
                        ActiveList[ii].CurrentLife = (int)(ActiveList[ii].MaxLife / 2.0f);
                        UpdateLife(ActiveList[ii]);
                    }
                    else if (ActiveList[ii].CurrentLifeCount > 0)
                    {
                        ActiveList[ii].CurrentLifeCountValue--;
                        UpdateBattleText(ActiveList[ii].Name + "�̐����͂��P���ꂽ�I�I\r\n");
                        if (ActiveList[ii].CurrentLifeCountValue <= 0)
                        {
                            UpdateBattleText(ActiveList[ii].GetCharacterSentence(217));
                            ActiveList[ii].RemoveLifeCount();
                            ActiveList[ii].DeadPlayer();
                        }
                        else
                        {
                            UpdateBattleText(ActiveList[ii].GetCharacterSentence(216));
                            System.Threading.Thread.Sleep(1000);
                            this.Invoke(new _AnimationDamage(AnimationDamage), 0, ActiveList[ii], 0, Color.Black, true, false, "��������");
                            ActiveList[ii].RemoveDebuffEffect();
                            ActiveList[ii].RemoveDebuffParam();
                            ActiveList[ii].RemoveDebuffSkill();
                            ActiveList[ii].RemoveDebuffSpell();
                            ActiveList[ii].ChangeLifeCountStatus(ActiveList[ii].CurrentLifeCountValue);
                            ActiveList[ii].CurrentLife = (int)(ActiveList[ii].MaxLife);
                            UpdateLife(ActiveList[ii]);
                            ActiveList[ii].labelLife.Update();
                            System.Threading.Thread.Sleep(1000);
                        }
                    }
                    else if (CheckResurrectWithItem(ActiveList[ii], Database.RARE_TAMATEBAKO_AKIDAMA))
                    {
                        UpdateBattleText(Database.RARE_TAMATEBAKO_AKIDAMA + "���W������n�߂��I\r\n", 500);
                        this.Invoke(new _AnimationDamage(AnimationDamage), 0, ActiveList[ii], 0, Color.Black, true, false, "����");

                        UpdateBattleText(ActiveList[ii].Name + "�͒v���̋��ԂŐ����c�����I�I\r\n");
                        ActiveList[ii].CurrentLife = (int)(ActiveList[ii].MaxLife * 0.1f);
                        UpdateLife(ActiveList[ii]);
                    }
                    else
                    {
                        ActiveList[ii].DeadPlayer();
                    }
                }

                // TranscendentWish�̌��ʂ��������ꂽ���������������ǉ��B
                if (ActiveList[ii].DeadSignForTranscendentWish)
                {
                    UpdateBattleText(ActiveList[ii].Name + "��TranscendentWish�̌��ʂ��؂ꂽ�I�����̌��������Ă����E�E�E\r\n");
                    UpdateLife(ActiveList[ii], ActiveList[ii].CurrentLife, false, true, 0, false);
                    ActiveList[ii].CurrentLife = 0;
                    UpdateLife(ActiveList[ii], 0, false, false, 0, false);
                    ActiveList[ii].DeadPlayer();
                    System.Threading.Thread.Sleep(1000);
                }

                CheckChaosDesperate(ActiveList[ii]);
            }

            if (PlayerPartyDeathCheck() || EnemyPartyDeathCheck())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckResurrectWithItem(MainCharacter target, string itemName)
        {
            if ((target.MainWeapon != null) && (target.MainWeapon.Name == itemName) && (target.MainWeapon.EffectStatus == false))
            {
                target.MainWeapon.EffectStatus = true;
                return true;
            }
            else if ((target.SubWeapon != null) && (target.SubWeapon.Name == itemName) && (target.SubWeapon.EffectStatus == false))
            {
                target.SubWeapon.EffectStatus = true;
                return true;
            }
            else if ((target.MainArmor != null) && (target.MainArmor.Name == itemName) && (target.MainArmor.EffectStatus == false))
            {
                target.MainArmor.EffectStatus = true;
                return true;
            }
            else if ((target.Accessory != null) && (target.Accessory.Name == itemName) && (target.Accessory.EffectStatus == false))
            {
                target.Accessory.EffectStatus = true;
                return true;
            }
            else if ((target.Accessory2 != null) && (target.Accessory2.Name == itemName) && (target.Accessory2.EffectStatus == false))
            {
                target.Accessory2.EffectStatus = true;
                return true;
            }

            return false;
        }


        private bool PlayerPartyDeathCheck()
        {
            // ���̃��W�b�N�A�C�}�C�`�����ǂ��Ƃ���B
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (!ActiveList[ii].Dead)
                {
                    if (ActiveList[ii] == ec1 ||
                        ActiveList[ii] == ec2 ||
                        ActiveList[ii] == ec3)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
        private bool EnemyPartyDeathCheck()
        {
            // ���̃��W�b�N�A�C�}�C�`�����ǂ��Ƃ���B
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (!ActiveList[ii].Dead)
                {
                    if (ActiveList[ii] == mc ||
                        ActiveList[ii] == sc ||
                        ActiveList[ii] == tc)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        private void Beginning()
        {
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (this.LifeCountBattle)
                {
                    PlayerBuffAbstract(ActiveList[ii], ActiveList[ii], Database.INFINITY, Database.LIFE_COUNT);
                }
                // �����i�������
                ItemEffect(ActiveList[ii], ActiveList[ii].MainWeapon);
                ItemEffect(ActiveList[ii], ActiveList[ii].MainArmor);
                ItemEffect(ActiveList[ii], ActiveList[ii].SubWeapon);
                ItemEffect(ActiveList[ii], ActiveList[ii].Accessory);
                ItemEffect(ActiveList[ii], ActiveList[ii].Accessory2);
                if (ActiveList[ii].MainWeapon != null)
                {
                    if (ActiveList[ii].MainWeapon.Name == Database.RARE_DOOMBRINGER)
                    {
                        if (ActiveList[ii].CurrentGaleWind <= 0)
                        {
                            PlayerSpellGaleWind(ActiveList[ii]);
                        }
                    }
                    if (ActiveList[ii].MainWeapon.Name == Database.EPIC_MEIKOU_DOOMBRINGER)
                    {
                        if (ActiveList[ii].CurrentGaleWind <= 0)
                        {
                            PlayerSpellGaleWind(ActiveList[ii]);
                        }
                        ActiveList[ii].BeforePA = PlayerAction.UseSpell;
                        ActiveList[ii].BeforeUsingItem = String.Empty;
                        ActiveList[ii].BeforeSkillName = String.Empty;
                        ActiveList[ii].BeforeSpellName = Database.GALE_WIND;
                        ActiveList[ii].BeforeTarget = ActiveList[ii];
                        //ActiveList[ii].BeforeTarget2 = null; // �L���v�f�͂Ȃ�
                    }
                }

                if (ActiveList[ii].Accessory != null)
                {
                    if (ActiveList[ii].Accessory.Name == Database.EPIC_SHUVALTZ_FLORE_ACCESSORY1)
                    {
                        if (ActiveList[ii].CurrentRiseOfImage <= 0)
                        {
                            PlayerSpellRiseOfImage(ActiveList[ii], ActiveList[ii]);
                        }
                    }
                    if (ActiveList[ii].Accessory.Name == Database.EPIC_SHUVALTZ_FLORE_ACCESSORY2)
                    {
                        if (ActiveList[ii].CurrentWordOfLife <= 0)
                        {
                            PlayerSpellWordOfLife(ActiveList[ii], ActiveList[ii]);
                        }
                    }
                    if (ActiveList[ii].Accessory.Name == Database.EPIC_FLOW_FUNNEL_OF_THE_ZVELDOZE)
                    {
                        if (ActiveList[ii].CurrentWordOfLife <= 0)
                        {
                            PlayerSpellWordOfLife(ActiveList[ii], ActiveList[ii]);
                        }
                        if (ActiveList[ii].CurrentRiseOfImage <= 0)
                        {
                            PlayerSpellRiseOfImage(ActiveList[ii], ActiveList[ii]);
                        }
                    }
                    if (ActiveList[ii].Accessory.Name == Database.COMMON_DEVIL_SEALED_VASE)
                    {
                        string chooseCommand = String.Empty;
                        using (TruthChooseCommand tcc = new TruthChooseCommand())
                        {
                            tcc.StartPosition = FormStartPosition.CenterParent;
                            tcc.Owner = this;
                            tcc.ShowDialog();
                            chooseCommand = tcc.ChooseCommand;
                        }
                        ActiveList[ii].Accessory.ImprintCommand = chooseCommand;
                    }
                    if (ActiveList[ii].Accessory.Name == Database.RARE_VOID_HYMNSONIA)
                    {
                        PlayerBuffAbstract(ActiveList[ii], ActiveList[ii], 999, Database.ITEMCOMMAND_VOID_HYMNSONIA);
                    }
                }
                if (ActiveList[ii].Accessory2 != null)
                {
                    if (ActiveList[ii].Accessory2.Name == Database.EPIC_SHUVALTZ_FLORE_ACCESSORY1)
                    {
                        if (ActiveList[ii].CurrentRiseOfImage <= 0)
                        {
                            PlayerSpellRiseOfImage(ActiveList[ii], ActiveList[ii]);
                        }
                    }
                    if (ActiveList[ii].Accessory2.Name == Database.EPIC_SHUVALTZ_FLORE_ACCESSORY2)
                    {
                        if (ActiveList[ii].CurrentWordOfLife <= 0)
                        {
                            PlayerSpellWordOfLife(ActiveList[ii], ActiveList[ii]);
                        }
                    }
                    if (ActiveList[ii].Accessory2.Name == Database.EPIC_FLOW_FUNNEL_OF_THE_ZVELDOZE)
                    {
                        if (ActiveList[ii].CurrentWordOfLife <= 0)
                        {
                            PlayerSpellWordOfLife(ActiveList[ii], ActiveList[ii]);
                        }
                        if (ActiveList[ii].CurrentRiseOfImage <= 0)
                        {
                            PlayerSpellRiseOfImage(ActiveList[ii], ActiveList[ii]);
                        }
                    }
                    if (ActiveList[ii].Accessory2.Name == Database.COMMON_DEVIL_SEALED_VASE)
                    {
                        string chooseCommand = String.Empty;
                        using (TruthChooseCommand tcc = new TruthChooseCommand())
                        {
                            tcc.StartPosition = FormStartPosition.CenterParent;
                            tcc.Owner = this;
                            tcc.ShowDialog();
                            chooseCommand = tcc.ChooseCommand;
                        }
                        ActiveList[ii].Accessory2.ImprintCommand = chooseCommand;
                    }
                    if (ActiveList[ii].Accessory2.Name == Database.RARE_VOID_HYMNSONIA)
                    {
                        PlayerBuffAbstract(ActiveList[ii], ActiveList[ii], 999, Database.ITEMCOMMAND_VOID_HYMNSONIA);
                    }
                }
            }
        }

        private void UpkeepStep()
        {
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                // �����i�������
                ItemEffect(ActiveList[ii], ActiveList[ii].MainWeapon);
                ItemEffect(ActiveList[ii], ActiveList[ii].SubWeapon);
                ItemEffect(ActiveList[ii], ActiveList[ii].MainArmor);
                ItemEffect(ActiveList[ii], ActiveList[ii].Accessory);
                ItemEffect(ActiveList[ii], ActiveList[ii].Accessory2);

                // OneAuthority�̌���
                if (ActiveList[ii].CurrentAbsoluteZero > 0)
                {
                }
                else
                {
                    if (ActiveList[ii].CurrentOneAuthority > 0)
                    {
                        ActiveList[ii].CurrentSkillPoint += (int)PrimaryLogic.OneAuthorityValue(ActiveList[ii], this.DuelMode);
                    }
                    // �e�v���C���[�̃X�L���l����
                    ActiveList[ii].CurrentSkillPoint++;
                    UpdateSkillPoint(ActiveList[ii]);
                }

                // �y�C���t���E�C���T�j�e�B����
                if (ActiveList[ii].CurrentPainfulInsanity > 0 && ActiveList[ii].Dead == false)
                {
                    List<MainCharacter> group = new List<MainCharacter>();
                    if (IsPlayerAlly(ActiveList[ii]))
                    {
                        if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                        if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                        if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
                    }
                    else
                    {
                        if (mc != null && !mc.Dead) { group.Add(mc); }
                        if (sc != null && !sc.Dead) { group.Add(sc); }
                        if (tc != null && !tc.Dead) { group.Add(tc); }
                    }

                    for (int jj = 0; jj < group.Count; jj++)
                    {
                        double effectValue = PrimaryLogic.PainfulInsanityValue(ActiveList[ii], this.DuelMode);
                        effectValue = DamageIsZero(effectValue, group[jj]);
                        UpdateBattleText(ActiveList[ii].Name + "��" + group[jj].Name + "�̐S�֒��ړI�ȃ_���[�W�𔭐������Ă���B" +((int)effectValue).ToString() + "�̃_���[�W\r\n");
                        LifeDamage(effectValue, group[jj]);
                    }
                }

                // �Ō���
                if (ActiveList[ii].CurrentPoison > 0 && ActiveList[ii].Dead == false)
                {
                    double effectValue = PrimaryLogic.PoisonValue(ActiveList[ii]);
                    effectValue = DamageIsZero(effectValue, ActiveList[ii]);
                    UpdateBattleText(ActiveList[ii].Name + "�͖ғł̌��ʂɂ��A���C�t������Ă����B\r\n");
                    LifeDamage(effectValue, ActiveList[ii]);
                    UpdateBattleText(ActiveList[ii].Name + "��" + ((int)effectValue).ToString() + "�̃_���[�W\r\n");
                }
                // �X���b�v����
                if (ActiveList[ii].CurrentSlip > 0 && ActiveList[ii].Dead == false)
                {
                    double effectValue = PrimaryLogic.SlipValue(ActiveList[ii]);
                    effectValue = DamageIsZero(effectValue, ActiveList[ii]);
                    UpdateBattleText(ActiveList[ii].Name + "�̏����͂Ђǂ��A���C�t������Ă����B\r\n");
                    LifeDamage(effectValue, ActiveList[ii]);
                    UpdateBattleText(ActiveList[ii].Name + "��" + ((int)effectValue).ToString() + "�̃_���[�W\r\n");
                }
                // �t���b�V���E�u���C�Y����
                if (ActiveList[ii].CurrentFlashBlazeCount > 0 && ActiveList[ii].Dead == false)
                {
                    double effectValue = PrimaryLogic.FlashBlaze_A_Value(ActiveList[ii], this.DuelMode);
                    effectValue = DamageIsZero(effectValue, ActiveList[ii]);
                    UpdateBattleText(ActiveList[ii].Name + "�ɑM���̉����~�蒍���B\r\n");
                    LifeDamage(effectValue, ActiveList[ii]);
                    UpdateBattleText(ActiveList[ii].Name + "��" + ((int)effectValue).ToString() + "�̃_���[�W\r\n");
                    ActiveList[ii].RemoveFlashBlaze();
                }
                // �G�����C�W�E�u���X�g����
                if (ActiveList[ii].CurrentEnrageBlast > 0 && ActiveList[ii].Dead == false)
                {
                    double effectValue = PrimaryLogic.EnrageBlast_A_Value(ActiveList[ii], this.DuelMode);
                    effectValue = DamageIsZero(effectValue, ActiveList[ii]);
                    UpdateBattleText(ActiveList[ii].Name + "�։΂̕����~�蒍���B\r\n");
                    LifeDamage(effectValue, ActiveList[ii]);
                    UpdateBattleText(ActiveList[ii].Name + "��" + ((int)effectValue).ToString() + "�̃_���[�W\r\n");
                }
                // �u���C�W���O�E�t�B�[���h����
                if (ActiveList[ii].CurrentBlazingField > 0 && ActiveList[ii].Dead == false)
                {
                    double effectValue = PrimaryLogic.BlazingField_A_Value(ActiveList[ii], this.DuelMode);
                    effectValue = DamageIsZero(effectValue, ActiveList[ii]);
                    UpdateBattleText(ActiveList[ii].Name + "�֖҉΂��~�蒍���B\r\n");
                    LifeDamage(effectValue, ActiveList[ii]);
                    UpdateBattleText(ActiveList[ii].Name + "��" + ((int)effectValue).ToString() + "�̃_���[�W\r\n");
                }
                // ���_���[�W�Q����
                if (ActiveList[ii].CurrentFireDamage2 > 0 && ActiveList[ii].Dead == false)
                {
                    double effectValue = PrimaryLogic.FireDamage2Value(ActiveList[ii]);
                    effectValue = DamageIsZero(effectValue, ActiveList[ii]);
                    UpdateBattleText(ActiveList[ii].Name + "�֖҉΂��~�蒍���B\r\n");
                    LifeDamage(effectValue, ActiveList[ii]);
                    UpdateBattleText(ActiveList[ii].Name + "��" + ((int)effectValue).ToString() + "�̃_���[�W\r\n");
                }
                // ��Ȃ鉋����
                if (ActiveList[ii].CurrentIchinaruHomura > 0 && ActiveList[ii].Dead == false)
                {
                    double effectValue = PrimaryLogic.IchinaruHomuraValue(ec1); // �_���[�W�������̓��M�B���A�[�[
                    effectValue = DamageIsZero(effectValue, ActiveList[ii]);
                    UpdateBattleText(ActiveList[ii].Name + "�ɉ��΂��~�蒍���B\r\n");
                    LifeDamage(effectValue, ActiveList[ii]);
                    UpdateBattleText(ActiveList[ii].Name + "��" + ((int)effectValue).ToString() + "�̃_���[�W\r\n");
                }
            }

            // Bystander�̓A�b�v�L�[�v�����C���s���̎厲�Ƃ���B
            if ((ec1 != null) && (ec1.Name == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS))
            {
                int totalNum = 0;
                if (FieldBuff1.Count <= 0) totalNum++;
                if (FieldBuff2.Count <= 0) totalNum++;
                if (FieldBuff3.Count <= 0) totalNum++;
                if (FieldBuff4.Count <= 0) totalNum++;
                if (FieldBuff5.Count <= 0) totalNum++;
                //if (FieldBuff6.Count <= 0) totalNum++;
                int choice = AP.Math.RandomInteger(totalNum);
                if (FieldBuff1.Count > 0 && choice >= 0) { choice++; }
                if (FieldBuff2.Count > 0 && choice >= 1) { choice++; }
                if (FieldBuff3.Count > 0 && choice >= 2) { choice++; }
                if (FieldBuff4.Count > 0 && choice >= 3) { choice++; }
                if (FieldBuff5.Count > 0 && choice >= 4) { choice++; }
                if (FieldBuff6.Count > 0 && choice >= 5) { choice++; }

                TruthImage[] list = { FieldBuff1, FieldBuff2, FieldBuff3, FieldBuff4, FieldBuff5, FieldBuff6 };
                switch (choice)
                {
                    case 0:
                        ec1.ChoiceTimeSequenceBuff(0, list, this.BattleTurnCount);
                        FieldBuff1.ImageName = Database.BUFF_TIME_SEQUENCE_1;
                        FieldBuff1.Image = Image.FromFile(Database.BaseResourceFolder + "bys_zougou.bmp");
                        break;
                    case 1:
                        ec1.ChoiceTimeSequenceBuff(1, list, this.BattleTurnCount);
                        FieldBuff2.ImageName = Database.BUFF_TIME_SEQUENCE_2;
                        FieldBuff2.Image = Image.FromFile(Database.BaseResourceFolder + "bys_reikuu.bmp");
                        break;
                    case 2:
                        ec1.ChoiceTimeSequenceBuff(2, list, this.BattleTurnCount);
                        FieldBuff3.ImageName = Database.BUFF_TIME_SEQUENCE_3;
                        FieldBuff3.Image = Image.FromFile(Database.BaseResourceFolder + "bys_seiei.bmp");
                        break;
                    case 3:
                        ec1.ChoiceTimeSequenceBuff(3, list, this.BattleTurnCount);
                        FieldBuff4.ImageName = Database.BUFF_TIME_SEQUENCE_4;
                        FieldBuff4.Image = Image.FromFile(Database.BaseResourceFolder + "bys_zekken.bmp");
                        break;
                    case 4:
                        ec1.ChoiceTimeSequenceBuff(4, list, this.BattleTurnCount);
                        FieldBuff5.ImageName = Database.BUFF_TIME_SEQUENCE_5;
                        FieldBuff5.Image = Image.FromFile(Database.BaseResourceFolder + "bys_ryokuei.bmp");
                        break;
                    case 5:
                        FieldBuff6.Count = 10;
                        FieldBuff6.ImageName = Database.BUFF_TIME_SEQUENCE_6;
                        FieldBuff6.Image = Image.FromFile(Database.BaseResourceFolder + "bys_syuen.bmp");
                        break;
                    case 6:
                        //FieldBuff1.AbstractCountDownBuff();
                        break;
                }
            }

        }

        private void ItemEffect(MainCharacter player, ItemBackPack item)
        {
            if (item != null)
            {
                if (item.Name == Database.EPIC_ORB_GROW_GREEN)
                {
                    PlayerAbstractSkillGain(player, player, 0, PrimaryLogic.EverGrowGreenValue(player), 0, String.Empty, 5009);
                }
                if (item.Name == Database.EPIC_SHUVALTZ_FLORE_ACCESSORY1)
                {
                    if (player.CurrentRiseOfImage <= 0)
                    {
                        PlayerSpellRiseOfImage(player, player);
                    }
                }
                if (item.Name == Database.EPIC_SHUVALTZ_FLORE_ACCESSORY2)
                {
                    if (player.CurrentWordOfLife <= 0)
                    {
                        PlayerSpellWordOfLife(player, player);
                    }
                }
                if (item.Name == Database.EPIC_SHUVALTZ_FLORE_SWORD)
                {
                    PlayerAbstractSkillGain(player, player, 0, PrimaryLogic.ShuvalzFloreSwordValue(player), 0, String.Empty, 5009);
                }
                if (item.Name == Database.EPIC_SHUVALTZ_FLORE_SHIELD)
                {
                    PlayerAbstractLifeGain(player, player, 0, PrimaryLogic.ShuvalzFloreShieldValue(player), 0, String.Empty, 5002);
                }
                if (item.Name == Database.EPIC_SHUVALTZ_FLORE_ARMOR)
                {
                    PlayerAbstractManaGain(player, player, 0, PrimaryLogic.ShuvalzFloreArmorValue(player), 0, String.Empty, 5003);
                }
                if (item.Name == Database.EPIC_SHEZL_MYSTIC_FORTUNE)
                {
                    PlayerAbstractManaGain(player, player, 0, PrimaryLogic.ShezlMysticFortuneValue(player), 0, String.Empty, 5003);
                }
                if (item.Name == Database.EPIC_EZEKRIEL_ARMOR_SIGIL)
                {
                    PlayerAbstractLifeGain(player, player, 0, PrimaryLogic.EzekrielArmorSigilValue_A(player), 0, String.Empty, 5002);
                    PlayerAbstractManaGain(player, player, 0, PrimaryLogic.EzekrielArmorSigilValue_B(player), 0, String.Empty, 5003);
                    PlayerAbstractSkillGain(player, player, 0, PrimaryLogic.EzekrielArmorSigilValue_C(player), 0, String.Empty, 5009);
                }
                if (item.Name == Database.RARE_ANGEL_CONTRACT)
                {
                    if (player.CurrentPreStunning > 0 || player.CurrentStunning > 0 || player.CurrentSilence > 0 ||
                        player.CurrentPoison > 0 || player.CurrentTemptation > 0 || player.CurrentFrozen > 0 ||
                        player.CurrentParalyze > 0 || player.CurrentSlow > 0 || player.CurrentBlind > 0)
                    {
                        UpdateBattleText(player.Name + "���������Ă���V�g�̌_�񏑂�����P�����I\r\n", 1000);
                        player.RemovePreStunning();
                        player.RemoveStun();
                        player.RemoveSilence();
                        player.RemovePoison();
                        player.RemoveTemptation();
                        player.RemoveFrozen();
                        player.RemoveParalyze();
                        player.RemoveSlow();
                        player.RemoveBlind();
                        UpdateBattleText(player.Name + "�ɂ������Ă��镉�̉e�����S�ĉ������ꂽ�B\r\n");
                    }
                }
                if (item.Name == Database.RARE_ARCHANGEL_CONTRACT)
                {
                    if (player.CurrentPhysicalAttackDown > 0 || player.CurrentPhysicalDefenseDown > 0 ||
                        player.CurrentMagicAttackDown > 0 || player.CurrentMagicDefenseDown > 0 ||
                        player.CurrentSpeedDown > 0 || player.CurrentReactionDown > 0 || player.CurrentPotentialDown > 0)
                    {
                        UpdateBattleText(player.Name + "���������Ă����V�g�̌_�񏑂�����P�����I\r\n", 1000);
                        player.RemovePhysicalAttackDown();
                        player.RemovePhysicalDefenseDown();
                        player.RemoveMagicAttackDown();
                        player.RemoveMagicDefenseDown();
                        player.RemoveSpeedDown();
                        player.RemoveReactionDown();
                        player.RemovePotentialDown();
                        UpdateBattleText(player.Name + "�̔\�͒ቺ��Ԃ��������ꂽ�I");
                    }
                }

                if (item.Name == Database.COMMON_ELDER_PERSPECTIVE_GRASS)
                {
                    if (player.Target != null)
                    {
                        BuffDownBattleSpeed(player.Target, 1000.0F);
                        BuffDownBattleReaction(player.Target, 1000.0F);
                    }
                }

                if (item.Name == Database.RARE_DEVIL_SUMMONER_TOME)
                {
                    if (player.Target != null)
                    {
                        double effectValue = PrimaryLogic.DevilSummonerTomeValue(player, this.DuelMode);
                        AbstractMagicDamage(player, player.Target, 0, effectValue, 0, Database.SOUND_FIREBALL, 120, TruthActionCommand.MagicType.Shadow_Fire, false, CriticalType.Random);
                    }
                }

                if (item.Name == Database.EPIC_ETERNAL_HOMURA_RING)
                {
                    PlayerAbstractManaGain(player, player, 0, PrimaryLogic.EternalHomuraRingValue_A(player), 0, String.Empty, 5003);
                }
            }
        }

        private void AfterBattleEffect()
        {
            //if (this.ec1 != null)
            //{
            //    if (!ec1.Dead)
            //    {
            //        if (ec1.CurrentWordOfLife > 0)
            //        {
            //            if (ec1.CurrentAbsoluteZero > 0)
            //            {
            //                UpdateBattleText(ec1.GetCharacterSentence(119));
            //            }
            //            else
            //            {
            //                int effectValue = ec1.TotalMind + ec1.TotalIntelligence;
            //                UpdateBattleText("�厩�R����" + ec1.Name + "�֗͋����������s���n��B" + effectValue.ToString() + "���C�t��\r\n");
            //                ec1.CurrentLife += effectValue;
            //                UpdateLife(ec1);
            //            }
            //        }
            //    }
            //}

            //if (this.ec1 != null)
            //{
            //    if (!ec1.Dead)
            //    {
            //        if (ec1.CurrentBlackContract > 0)
            //        {
            //            UpdateBattleText(ec1.Name + "�͈����ւ̑㏞���x�����B" + Convert.ToString((int)((float)ec1.MaxLife / 10.0F)) + "���C�t���������B\r\n");
            //            ec1.CurrentLife -= (int)((float)ec1.MaxLife / 10.0F);
            //            UpdateLife(ec1);
            //        }
            //    }
            //}

            //if (ec1 != null)
            //{
            //    if (!ec1.Dead)
            //    {
            //        if (ec1.CurrentDamnation > 0)
            //        {
            //            Decimal effectValue = Convert.ToDecimal(Math.Ceiling((float)ec1.MaxLife / ec1.TotalMind));
            //            UpdateBattleText("����" + this.ec1.Name + "�̑��݂��Ă����Ԃ�c�܂��Ă���B" + effectValue.ToString() + "�̃_���[�W\r\n");
            //            ec1.CurrentLife -= Convert.ToInt32(effectValue);
            //            UpdateLife(ec1);
            //        }

            //        if (ec1.CurrentPoison > 0)
            //        {
            //            UpdateBattleText(ec1.Name + "�͖ғłɂ�胉�C�t������Ă䂭�E�E�E" + Convert.ToInt32((float)ec1.MaxLife / 10.0F).ToString() + "�̃_���[�W\r\n");
            //            ec1.CurrentLife -= Convert.ToInt32((float)ec1.MaxLife / 10.0F);
            //            UpdateLife(ec1);
            //        }
            //    }
            //}


            //if (this.ActivateTimeStop > 0 || this.ActivateTrcky > 0 || this.ActivateGrowUp > 0 || this.ActivateSword > 0 || this.ActivateEverGreen > 0 || this.ActivateTotalEndTime > 0)
            //{
            //    return; // �v���C���[�G�t�F�N�g�͎��Ԓ�~�ɂ�肩����Ȃ��Ȃ�B
            //}



            // �v���C���[�ւ̃G�t�F�N�g
            //for (int ii = 0; ii < playerList.Length; ii++)
            //{
            //    if (!playerList[ii].Dead)
            //    {
            //        if ((playerList[ii].Accessory != null) && (playerList[ii].Accessory.Name == "�Đ��̖��"))
            //        {
            //            if (playerList[ii].CurrentAbsoluteZero > 0)
            //            {
            //                UpdateBattleText("������" + playerList[ii].Name + "�͐�Η�x���ʂɂ�莩�R�̉��b���󂯂Ƃ�Ȃ��I\r\n");
            //            }
            //            else
            //            {
            //                int effectValue = (playerList[ii].MaxLife * playerList[ii].Accessory.MaxValue / 100);
            //                UpdateBattleText(playerList[ii].Accessory.Name + "���琶���̗͂�����Ă���B" + effectValue.ToString() + "���C�t��\r\n");
            //                playerList[ii].CurrentLife += effectValue;
            //                UpdateLife(playerList[ii]);
            //            }
            //        }

            //        if (playerList[ii].CurrentWordOfLife > 0 && !playerList[ii].Dead)
            //        {
            //            if (playerList[ii].CurrentAbsoluteZero > 0)
            //            {
            //                UpdateBattleText("������" + playerList[ii].Name + "�͐�Η�x���ʂɂ�莩�R�̉��b���󂯂Ƃ�Ȃ��I\r\n");
            //            }
            //            else
            //            {
            //                int effectValue = playerList[ii].TotalMind + playerList[ii].TotalIntelligence;
            //                UpdateBattleText("�厩�R����" + playerList[ii].Name + "�֗͋����������s���n��B" + effectValue.ToString() + "���C�t��\r\n");
            //                playerList[ii].CurrentLife += effectValue;
            //                UpdateLife(playerList[ii]);
            //            }
            //        }

            //        if (playerList[ii].CurrentPoison > 0)
            //        {
            //            UpdateBattleText(playerList[ii].Name + "�͖ғłɂ�胉�C�t������Ă䂭�E�E�E" + Convert.ToInt32((float)playerList[ii].MaxLife / 10.0F).ToString() + "�̃_���[�W\r\n");
            //            playerList[ii].CurrentLife -= Convert.ToInt32((float)playerList[ii].MaxLife / 10.0F);
            //            UpdateLife(playerList[ii]);
            //        }
            //    }
            //}

            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (!ActiveList[ii].Dead)
                {
                    if (ActiveList[ii].CurrentEternalDroplet > 0)
                    {
                        double effectValue = PrimaryLogic.EternalDropletValue_A(ActiveList[ii]);
                        if (ActiveList[ii].CurrentNourishSense > 0)
                        {
                            effectValue = effectValue * 1.3f;
                        } 
                        effectValue = GainIsZero(effectValue, ActiveList[ii]);
                        UpdateBattleText("�i�������������A" + ActiveList[ii].Name + "�֐����͂𒍂�����ł���B" + ((int)effectValue).ToString() + "���C�t��\r\n");
                        ActiveList[ii].CurrentLife += (int)(effectValue);
                        UpdateLife(ActiveList[ii], effectValue, true, true, 0, false);

                        double effectValue2 = PrimaryLogic.EternalDropletValue_B(ActiveList[ii]);
                        effectValue2 = GainIsZero(effectValue2, ActiveList[ii]);
                        UpdateBattleText(((int)effectValue2).ToString() + "�}�i��\r\n");
                        ActiveList[ii].CurrentMana += (int)effectValue;
                        UpdateMana(ActiveList[ii], (double)effectValue, true, true, 0);
                    }

                    if (ActiveList[ii].CurrentWordOfLife > 0)
                    {
                        double effectValue = PrimaryLogic.WordOfLifeValue(ActiveList[ii], this.DuelMode);
                        if (ActiveList[ii].CurrentNourishSense > 0)
                        {
                            effectValue = effectValue * 1.3f;
                        }
                        effectValue = GainIsZero(effectValue, ActiveList[ii]);
                        UpdateBattleText("�厩�R����" + ActiveList[ii].Name + "�֗͋����������s���n��B" + ((int)effectValue).ToString() + "���C�t��\r\n");
                        ActiveList[ii].CurrentLife += (int)(effectValue);
                        UpdateLife(ActiveList[ii], effectValue, true, true, 0, false);
                    }

                    if (ActiveList[ii].CurrentEverDroplet > 0 && ActiveList[ii].Dead == false)
                    {
                        double effectValue = PrimaryLogic.EverDropletValue(ActiveList[ii]);
                        effectValue = GainIsZero(effectValue, ActiveList[ii]);
                        UpdateBattleText("�����̍�������" + ActiveList[ii].Name + "�֖����̃C���[�W���s���n��B" + ((int)effectValue).ToString() + "�}�i��\r\n");
                        ActiveList[ii].CurrentMana += (int)effectValue;
                        UpdateMana(ActiveList[ii], (double)effectValue, true, true, 0);
                    }

                    if (ActiveList[ii].CurrentBlackContract > 0 && !ActiveList[ii].Dead)
                    {
                        double effectValue = Math.Ceiling((float)ActiveList[ii].MaxLife / 10.0F);//playerList[ii].TotalMind));
                        effectValue = DamageIsZero(effectValue, ActiveList[ii]);
                        UpdateBattleText(ActiveList[ii].Name + "�͈����ւ̑㏞���x�����B" + ((int)effectValue).ToString() + "���C�t���������B\r\n");
                        LifeDamage(effectValue, ActiveList[ii]);
                    }

                    if (ActiveList[ii].CurrentHymnContract > 0 && !ActiveList[ii].Dead)
                    {
                        double effectValue = Math.Ceiling((float)ActiveList[ii].MaxLife / 10.0F);//playerList[ii].TotalMind));
                        effectValue = DamageIsZero(effectValue, ActiveList[ii]);
                        UpdateBattleText(ActiveList[ii].Name + "�͓V�g�Ƃ̒����ɂ��A���̑�����x�����B" + ((int)effectValue).ToString() + "���C�t���������B\r\n");
                        LifeDamage(effectValue, ActiveList[ii]);
                    }

                    if (ActiveList[ii].CurrentDamnation > 0 && !ActiveList[ii].Dead)
                    {
                        double effectValue = PrimaryLogic.DamnationValue(ActiveList[ii]);
                        effectValue = DamageIsZero(effectValue, ActiveList[ii]);
                        UpdateBattleText("����" + ActiveList[ii].Name + "�̑��݂��Ă����Ԃ�c�܂��Ă���B" + ((int)effectValue).ToString() + "�̃_���[�W\r\n");
                        LifeDamage(effectValue, ActiveList[ii]);
                    }

                    if ((ActiveList[ii].Accessory != null) && (ActiveList[ii].Accessory.Name == Database.COMMON_MUKEI_SAKAZUKI))
                    {
                        if (ActiveList[ii].PoolLifeConsumption > 0)
                        {
                            double effectValue = (double)(ActiveList[ii].PoolLifeConsumption) / 2.0F;
                            double effectValue2 = (double)(ActiveList[ii].PoolManaConsumption) / 2.0F;
                            double effectValue3 = (double)(ActiveList[ii].PoolSkillConsumption) / 2.0F;
                            effectValue = GainIsZero(effectValue, ActiveList[ii]);
                            effectValue2 = GainIsZero(effectValue2, ActiveList[ii]);
                            effectValue3 = GainIsZero(effectValue3, ActiveList[ii]);
                            UpdateBattleText(Database.COMMON_MUKEI_SAKAZUKI + "����" + ActiveList[ii].Name + "�֐����̐����N���o�Ă���B\r\n");
                            UpdateBattleText(ActiveList[ii].Name + "�̃��C�t��" + ((int)effectValue).ToString() + "�񕜁A�}�i��" + ((int)effectValue2).ToString() + "�񕜁A�X�L���|�C���g��" + ((int)effectValue3).ToString() + "��\r\n");
                            ActiveList[ii].CurrentLife += (int)effectValue;
                            ActiveList[ii].CurrentMana += (int)effectValue2;
                            ActiveList[ii].CurrentSkillPoint += (int)effectValue3;
                            UpdateLife(ActiveList[ii], (double)effectValue, true, true, 0, false);
                            UpdateMana(ActiveList[ii], (double)effectValue2, true, true, 0);
                            UpdateSkillPoint(ActiveList[ii], (double)effectValue3, true, true, 0);
                        }
                    }
                }
            }


            // �G�l�~�[�ւ̃G�t�F�N�g
            //for (int ii = 0; ii < playerList.Length; ii++)
            //{
            //    if (playerList[ii].CurrentPainfulInsanity > 0 && !playerList[ii].Dead)
            //    {
            //        int effectValue = playerList[ii].TotalMind * 3;
            //        UpdateBattleText(playerList[ii].Name + "��" + this.ec1.Name + "�̐S�֒��ړI�ȃ_���[�W�𔭐������Ă���B" + effectValue.ToString() + "�̃_���[�W\r\n");
            //        ec1.CurrentLife -= effectValue;
            //        UpdateLife(ec1);
            //    }
            //}


            //for (int ii = 0; ii < playerList.Length; ii++)
            //{
            //    if (playerList[ii].CurrentDamnation > 0 && !playerList[ii].Dead)
            //    {
            //        UpdateBattleText("����" + playerList[ii].Name + "�̑��݂��Ă����Ԃ�c�܂��Ă���B" + Convert.ToInt32((float)playerList[ii].MaxLife / 10.0F).ToString() + "�̃_���[�W\r\n");
            //        playerList[ii].CurrentLife -= Convert.ToInt32((float)playerList[ii].MaxLife / 10.0F);
            //        UpdateLife(playerList[ii]);
            //    }
            //}
        }

        protected void TimeStopAlly(MainCharacter player)
        {
            List<MainCharacter> group = new List<MainCharacter>();
            if (mc != null && !mc.Dead) { group.Add(mc); }
            if (sc != null && !sc.Dead) { group.Add(sc); }
            if (tc != null && !tc.Dead) { group.Add(tc); }

            foreach (MainCharacter current in group)
            {
                PlayerSpellTimeStop(player, current, true);
            }
        }

        private int CurrentTimeStop = 0; // [��ҕK�{]�^�C���X�g�b�v����Ґ�p�ŏ��������Ă��������B�{�t���O�͕s�v�ł��B
        private void CleanUpStep()
        {
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                ActiveList[ii].CleanUpEffect(false, false);
                if (ActiveList[ii].Name == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
                {
                    // ���Ɓu�U���P�v
                    if (FieldBuff1.AbstractCountDownBuff())
                    {
                        TruthEnemyCharacter player = ((TruthEnemyCharacter)ActiveList[ii]);
                        
                        player.Pattern1++;

                        if (player.Pattern1 > 0) { player.ActionCommandStackList.Add(Database.BLAZING_FIELD); player.ActionCommandStackTarget.Add(player.Targetting(mc, sc, tc)); }
                        if (player.Pattern1 > 1) { player.ActionCommandStackList.Add(Database.SIGIL_OF_HOMURA); player.ActionCommandStackTarget.Add(player.Targetting(mc, sc, tc)); }
                        if (player.Pattern1 > 2) { player.ActionCommandStackList.Add(Database.IMMOLATE); player.ActionCommandStackTarget.Add(player.Targetting(mc, sc, tc)); }
                        if (player.Pattern1 > 3) { player.ActionCommandStackList.Add(Database.WORD_OF_MALICE); player.ActionCommandStackTarget.Add(player.Targetting(mc, sc, tc)); }
                        if (player.Pattern1 > 4) { player.ActionCommandStackList.Add(Database.PIERCING_FLAME); player.ActionCommandStackTarget.Add(player.Targetting(mc, sc, tc)); }
                        if (player.Pattern1 > 5) { player.ActionCommandStackList.Add(Database.DEMONIC_IGNITE); player.ActionCommandStackTarget.Add(player.Targetting(mc, sc, tc)); }
                        if (player.Pattern1 > 6) { player.ActionCommandStackList.Add(Database.DOOM_BLADE); player.ActionCommandStackTarget.Add(player.Targetting(mc, sc, tc)); }
                        if (player.Pattern1 > 7) { player.ActionCommandStackList.Add(Database.LAVA_ANNIHILATION); player.ActionCommandStackTarget.Add(player.Targetting(mc, sc, tc)); }

                        TimeStopAlly(ActiveList[ii]); 
                    }
                    // ���u�f�B�X�y���v
                    if (FieldBuff2.AbstractCountDownBuff()) 
                    {
                        TruthEnemyCharacter player = ((TruthEnemyCharacter)ActiveList[ii]);
                        player.Pattern2++;

                        if (player.Pattern2 > 0) { player.ActionCommandStackList.Add(Database.ABSOLUTE_ZERO); player.ActionCommandStackTarget.Add(player.Targetting(tc, sc, mc)); }
                        if (player.Pattern2 > 0) { player.ActionCommandStackList.Add(Database.DAMNATION); player.ActionCommandStackTarget.Add(player.Targetting(mc, sc, tc)); }
                        if (player.Pattern2 > 1) { player.ActionCommandStackList.Add(Database.AUSTERITY_MATRIX); player.ActionCommandStackTarget.Add(player.Targetting(mc, sc, tc)); }
                        if (player.Pattern2 > 2) { player.ActionCommandStackList.Add(Database.BLACK_CONTRACT); player.ActionCommandStackTarget.Add(player.Targetting(mc, sc, tc)); }
                        if (player.Pattern2 > 3) { player.ActionCommandStackList.Add(Database.TRANQUILITY); player.ActionCommandStackTarget.Add(player.Targetting(mc, sc, tc)); }
                        if (player.Pattern2 > 4) { player.ActionCommandStackList.Add(Database.HYMN_CONTRACT); player.ActionCommandStackTarget.Add(player.Targetting(mc, sc, tc)); }
                        if (player.Pattern2 > 5) { player.ActionCommandStackList.Add(Database.DISPEL_MAGIC); player.ActionCommandStackTarget.Add(player.Targetting(mc, sc, tc)); }
                        if (player.Pattern2 > 6) { player.ActionCommandStackList.Add(Database.TRANSCENDENT_WISH); player.ActionCommandStackTarget.Add(player.Targetting(mc, sc, tc)); }

                        TimeStopAlly(ActiveList[ii]);
                    }
                    // ���h�u�h��v
                    if (FieldBuff3.AbstractCountDownBuff())
                    {
                        TruthEnemyCharacter player = ((TruthEnemyCharacter)ActiveList[ii]);
                        player.Pattern3++;

                        if (player.Pattern3 > 0) { player.ActionCommandStackList.Add(Database.PROTECTION); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern3 > 1) { player.ActionCommandStackList.Add(Database.MIRROR_IMAGE); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern3 > 2) { player.ActionCommandStackList.Add(Database.DEFLECTION); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern3 > 3) { player.ActionCommandStackList.Add(Database.SKY_SHIELD); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern3 > 4) { player.ActionCommandStackList.Add(Database.STATIC_BARRIER); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern3 > 5) { player.ActionCommandStackList.Add(Database.SKY_SHIELD); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern3 > 6) { player.ActionCommandStackList.Add(Database.STATIC_BARRIER); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern3 > 7) { player.ActionCommandStackList.Add(Database.SKY_SHIELD); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern3 > 8) { player.ActionCommandStackList.Add(Database.STATIC_BARRIER); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern3 > 9) { player.ActionCommandStackList.Add(Database.HOLY_BREAKER); player.ActionCommandStackTarget.Add(player); }

                        TimeStopAlly(ActiveList[ii]);
                    }
                    // �⌕�u�U���Q�v
                    if (FieldBuff4.AbstractCountDownBuff())
                    {
                        TruthEnemyCharacter player = ((TruthEnemyCharacter)ActiveList[ii]);
                        player.Pattern4++;

                        if (player.Pattern4 <= 2) { player.ActionCommandStackList.Add(Database.STRAIGHT_SMASH); player.ActionCommandStackTarget.Add(player.Targetting(mc, sc, tc)); }
                        else if (player.Pattern4 <= 4) { player.ActionCommandStackList.Add(Database.DOUBLE_SLASH); player.ActionCommandStackTarget.Add(player.Targetting(mc, sc, tc)); }
                        else if (player.Pattern4 <= 8) { player.ActionCommandStackList.Add(Database.SILENT_RUSH); player.ActionCommandStackTarget.Add(player.Targetting(mc, sc, tc)); }
                        else if (player.Pattern4 <= 16) { player.ActionCommandStackList.Add(Database.CARNAGE_RUSH); player.ActionCommandStackTarget.Add(player.Targetting(mc, sc, tc)); }
                        else if (player.Pattern4 <= 32) { player.ActionCommandStackList.Add(Database.SOUL_EXECUTION); player.ActionCommandStackTarget.Add(player.Targetting(mc, sc, tc)); }

                        if (player.Pattern4 > 0) { player.ActionCommandStackList.Add(Database.SAINT_POWER); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern4 > 1) { player.ActionCommandStackList.Add(Database.FLAME_AURA); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern4 > 2) { player.ActionCommandStackList.Add(Database.FROZEN_AURA); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern4 > 3) { player.ActionCommandStackList.Add(Database.GALE_WIND); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern4 > 4) { player.ActionCommandStackList.Add(Database.WORD_OF_FORTUNE); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern4 > 5) { player.ActionCommandStackList.Add(Database.SIN_FORTUNE); player.ActionCommandStackTarget.Add(player); }

                        TimeStopAlly(ActiveList[ii]);
                    }
                    // �Ήi�u�񕜁v
                    if (FieldBuff5.AbstractCountDownBuff())
                    {
                        TruthEnemyCharacter player = ((TruthEnemyCharacter)ActiveList[ii]);
                        player.Pattern5++;

                        if (player.Pattern5 > 0) { player.ActionCommandStackList.Add(Database.NOURISH_SENSE); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern5 > 0) { player.ActionCommandStackList.Add(Database.FRESH_HEAL); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern5 > 1) { player.ActionCommandStackList.Add(Database.WORD_OF_LIFE); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern5 > 2) { player.ActionCommandStackList.Add(Database.SACRED_HEAL); player.ActionCommandStackTarget.Add(player); }
                        if (player.Pattern5 > 3) { player.ActionCommandStackList.Add(Database.CELESTIAL_NOVA); player.ActionCommandStackTarget.Add(player); }
                        
                        TimeStopAlly(ActiveList[ii]);
                    }
                    // �I��
                    if (FieldBuff6.AbstractCountDownBuff())
                    {
                        TruthEnemyCharacter player = ((TruthEnemyCharacter)ActiveList[ii]);
                        player.Pattern6++;
                        if (player.Pattern6 > 0) { player.ActionCommandStackList.Add(Database.ZETA_EXPLOSION); player.ActionCommandStackTarget.Add(player.Targetting(mc, sc, tc)); }

                        TimeStopAlly(ActiveList[ii]);
                        //if (this.actionCommandStackList.Count == 0)
                        //{
                        //    SetupActionCommand(this, this, PlayerAction.UseSpell, Database.SHADOW_PACT);
                        //}
                        //else if (this.actionCommandStackList.Count == 1)
                        //{
                        //    SetupActionCommand(this, this, PlayerAction.UseSpell, Database.PROMISED_KNOWLEDGE);
                        //}
                        //else if (this.actionCommandStackList.Count == 2)
                        //{
                        //    SetupActionCommand(this, this, PlayerAction.UseSpell, Database.ETERNAL_PRESENCE);
                        //}
                        //else if (this.actionCommandStackList.Count == 3)
                        //{
                        //    SetupActionCommand(this, this, PlayerAction.UseSpell, Database.PSYCHIC_TRANCE);
                        //}
                        //else if (this.actionCommandStackList.Count == 4)
                        //{
                        //    SetupActionCommand(this, this, PlayerAction.UseSpell, Database.RED_DRAGON_WILL);
                        //}
                        //else if (this.actionCommandStackList.Count == 5)
                        //{
                        //    SetupActionCommand(this, this, PlayerAction.UseSpell, Database.BLUE_DRAGON_WILL);
                        //}
                        //else if (this.actionCommandStackList.Count == 6)
                        //{
                        //    SetupActionCommand(this, target, PlayerAction.UseSpell, Database.DISPEL_MAGIC);
                        //}
                        //else if (this.actionCommandStackList.Count == 7)
                        //{
                        //    SetupActionCommand(this, target, PlayerAction.UseSpell, Database.TRANQUILITY);
                        //}
                        //else if (this.actionCommandStackList.Count == 8)
                        //{
                        //    SetupActionCommand(this, target, PlayerAction.UseSpell, Database.AUSTERITY_MATRIX);
                        //}
                        //else if (this.actionCommandStackList.Count == 9)
                        //{
                        //    SetupActionCommand(this, target, PlayerAction.UseSpell, Database.ABSOLUTE_ZERO);
                        //}
                        //else if (this.actionCommandStackList.Count == 10)
                        //{
                        //    SetupActionCommand(this, target, PlayerAction.UseSpell, Database.SIGIL_OF_HOMURA);
                        //}
                        //else if (this.actionCommandStackList.Count == 11)
                        //{
                        //    SetupActionCommand(this, target, PlayerAction.UseSpell, Database.ZETA_EXPLOSION);
                        //}

                        TimeStopAlly(ActiveList[ii]);
                    }
                }
            }
            
            // �O�҂���̈��p���ɂ͕s�����Ă�R�[�f�B���O�B���������K�v�B
            if (CurrentTimeStop > 0)
            {
                CurrentTimeStop--;
            }            
        }
        private void CleanUpForBoss()
        {
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (IsPlayerEnemy(ActiveList[ii]))
                {
                    if ((((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss1) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss2) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss21) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss22) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss23) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss24) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss25) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss3) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss4) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.Boss5) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.LastBoss) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.TruthBoss1) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.TruthBoss2) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.TruthBoss3) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.TruthBoss4) ||
                        (((TruthEnemyCharacter)(ActiveList[ii])).Area == TruthEnemyCharacter.MonsterArea.TruthBoss5))
                    {
                        ((TruthEnemyCharacter)ActiveList[ii]).CleanUpEffectForBoss();
                    }
                }
            }
        }


        private void pbPlayer1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (!ActiveList[ii].Dead)
                {
                    if (ActiveList[ii].Equals(ec1) || ActiveList[ii].Equals(ec2) || ActiveList[ii].Equals(ec3))
                    {
                        g.DrawImage(ActiveList[ii].MainFaceArrow, (float)ActiveList[ii].BattleBarPos, 30);
                        if (ActiveList[ii].ShadowFaceArrow2 != null && ActiveList[ii].CurrentChaoticSchema > 0)
                        {
                            g.DrawImage(ActiveList[ii].ShadowFaceArrow2, (float)ActiveList[ii].BattleBarPos2, 30);
                        }
                        if (ActiveList[ii].ShadowFaceArrow3 != null && ActiveList[ii].CurrentChaoticSchema > 0 && ActiveList[ii].CurrentLifeCountValue <= 1)
                        {
                            g.DrawImage(ActiveList[ii].ShadowFaceArrow3, (float)ActiveList[ii].BattleBarPos3, 30);
                        }
                    }
                    else
                    {
                        g.DrawImage(ActiveList[ii].MainFaceArrow, (float)ActiveList[ii].BattleBarPos, 0);
                    }
                }
            }
        }

        private void battleSpeedBar_Scroll(object sender, EventArgs e)
        {
            switch (battleSpeedBar.Value)
            {
                case 1:
                    TIMER_SPEED = 40;
                    TimeSpeedLabel.Text = "���ԑ��x x0.25";
                    break;
                case 2:
                    TIMER_SPEED = 30;
                    TimeSpeedLabel.Text = "���ԑ��x x0.37";
                    break;
                case 3:
                    TIMER_SPEED = 20;
                    TimeSpeedLabel.Text = "���ԑ��x x0.50";
                    break;
                case 4:
                    TIMER_SPEED = 15;
                    TimeSpeedLabel.Text = "���ԑ��x x0.75";
                    break;
                case 5:
                    TIMER_SPEED = 10;
                    TimeSpeedLabel.Text = "���ԑ��x x1.00";
                    break;
                case 6:
                    TIMER_SPEED = 8;
                    TimeSpeedLabel.Text = "���ԑ��x x1.50";
                    break;
                case 7:
                    TIMER_SPEED = 5;
                    TimeSpeedLabel.Text = "���ԑ��x x2.00";
                    break;
                case 8:
                    TIMER_SPEED = 3;
                    TimeSpeedLabel.Text = "���ԑ��x x3.00";
                    break;
                case 9:
                    TIMER_SPEED = 2;
                    TimeSpeedLabel.Text = "���ԑ��x x4.00";
                    break;
                default:
                    TIMER_SPEED = 10;
                    TimeSpeedLabel.Text = "���ԑ��x x1.00";
                    break;
            }
        }

        private void RefreshActionIcon(MainCharacter player)
        {
            //string SelectOn = "_On";
            string fileExt = ".bmp";

            if (player.BattleActionCommand1 != "") player.ActionButton1.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand1 + fileExt);
            if (player.BattleActionCommand2 != "") player.ActionButton2.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand2 + fileExt);
            if (player.BattleActionCommand3 != "") player.ActionButton3.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand3 + fileExt);
            if (player.BattleActionCommand4 != "") player.ActionButton4.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand4 + fileExt);
            if (player.BattleActionCommand5 != "") player.ActionButton5.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand5 + fileExt);
            if (player.BattleActionCommand6 != "") player.ActionButton6.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand6 + fileExt);
            if (player.BattleActionCommand7 != "") player.ActionButton7.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand7 + fileExt);
            if (player.BattleActionCommand8 != "") player.ActionButton8.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand8 + fileExt);
            if (player.BattleActionCommand9 != "") player.ActionButton9.Image = new Bitmap(Database.BaseResourceFolder + player.BattleActionCommand9 + fileExt);
        }
        
        #region "���ڍU��"

        private void PlayerSkillDoubleSlash(MainCharacter player, MainCharacter target, double magnification, bool ignoreDefense)
        {
            // ����F�J�E���^�[�A�^�b�N�������Ă���ꍇ
            if (target.CurrentCounterAttack > 0)
            {
                // �����F����̖h��̐��𖳎��ł���ꍇ
                if (ignoreDefense)
                {
                    UpdateBattleText(player.GetCharacterSentence(110));
                }
                else
                {
                    // �����FNothingOfNothingness�ɂ�閳�����������Ă���ꍇ
                    if (player.CurrentNothingOfNothingness > 0)
                    {
                        UpdateBattleText(player.Name + "��" + target.Name + "��" + Database.ATTACK_JP + "���d�|���鏊�ŁE�E�E\r\n", 500);
                        UpdateBattleText(target.GetCharacterSentence(114), 1000);
                        UpdateBattleText(player.Name + "�͖������I�[���ɂ���Č���Ă���I\r\n");
                        target.RemoveStanceOfEyes();
                        target.RemoveNegate();
                        target.RemoveCounterAttack();
                    }
                    else
                    {
                        UpdateBattleText(player.Name + "��" + target.Name + "��" + Database.ATTACK_JP + "���d�|���鏊�ŁE�E�E\r\n", 500);
                        UpdateBattleText(target.GetCharacterSentence(113));
                        PlayerNormalAttack(target, player, 0, false, false);
                        return;
                    }
                }
            }

            UpdateBattleText(player.GetCharacterSentence(2));
            PlayerNormalAttack(player, target, 0, false, false);
            UpdateBattleText(player.GetCharacterSentence(3), 100);
            PlayerNormalAttack(player, target, 0, false, false);
        }

        private bool PlayerNormalAttack(MainCharacter player, MainCharacter target, double magnification, bool ignoreDefense, bool ignoreDoubleAttack)
        {
            return PlayerNormalAttack(player, target, magnification, 0, ignoreDefense, false, 0, 0, string.Empty, -1, ignoreDoubleAttack, CriticalType.Random);
        }
        /// <summary>
        /// �ʏ�U���̃��\�b�h
        /// </summary>
        /// <param name="player">�Ώی�</param>
        /// <param name="target">�Ώۑ���</param>
        /// <param name="magnification">�����{���A�O�̏ꍇ�͑������Ȃ�</param>
        /// <param name="crushingBlow">�X�^�����ʎ���</param>
        /// <param name="skipCounterPhase">�J�E���^�[����̕������X�L�b�v���� </param>
        /// <param name="ignoreDefense"> �h��p���𖳎�����> </param>
        /// <param name="atkBase">���O�ɍU���_���[�W�l�����܂��Ă���A�w�肵�Ȃ��ꍇ��0</param>
        /// <param name="interval">�ʏ�U���̃_���[�W�A�j���[�V�����̕\�����ԁB�w�肵�Ȃ��ꍇ��0</param>
        /// <param name="soundName">���ʉ��t�@�C�����́B�w�肵�Ȃ��ꍇ��string.Empty</param>
        /// <param name="textNumber">�s�����̃��b�Z�[�W�ԍ�GetCharacterSentence()�A�w�肵�Ȃ��ꍇ��-1</param>
        /// <param name="ignoreDoubleAttack">2��U�������{���Ȃ��t���O</param>
        /// <param name="critical">�N���e�B�J�������t���O</param>
        private bool PlayerNormalAttack(MainCharacter player, MainCharacter target, double magnification, int crushingBlow, bool ignoreDefense, bool skipCounterPhase, double atkBase, int interval, string soundName, int textNumber, bool ignoreDoubleAttack, CriticalType critical)
        {
            //if (skipCounterPhase == false)
            //{
            //    if (CheckCounterAttack(player))
            //    {
            //        PlayerNormalAttack(target, player, 0, false, false);
            //        return;
            //    }
            //}

            for (int ii = 0; ii < 2; ii++) // �T�u�E�F�|���ɂ��2��U�����l��
            {
                // �U���~�X���肷��O��Glory���ʁiGlory�͎��g�ΏۂȂ̂ŁA�K�p�Ώۂn�j�d�l�͑O�Ҏ���Ɠ����j
                // Glory�ɂ�����
                if (player.CurrentGlory > 0)
                {
                    MainCharacter memoTarget = player.Target;
                    player.Target = player;
                    PlayerSpellFreshHeal(player, player);
                    player.Target = memoTarget;
                }

                // �~�X����
                if (CheckDodge(player, target, false))
                {
                    // ������ꂽ�ꍇ�A�_���[�W�͔������Ȃ��B�f�t���N�V��������Ȃǂ����l�B
                    // ��ԉ��ɃT�u�E�F�|���ɂ����U�����肪����̂ŁA����͕ʂƂ���B
                    this.Invoke(new _AnimationDamage(AnimationDamage), 0, target, 0, Color.Black, true, false, String.Empty);
                }
                else if (CheckBlindMiss(player, target))
                {
                    // �Èłɂ��U�����O�����ꍇ�_���[�W�͔������Ȃ��B�f�t���N�V��������Ȃǂ����l�B
                    // ��ԉ��ɃT�u�E�F�|���ɂ����U�����肪����̂ŁA����͕ʂƂ���B
                    this.Invoke(new _AnimationDamage(AnimationDamage), 0, target, 0, Color.Black, true, false, String.Empty);
                }
                else
                {
                    double damage = 0;
                    // �_���[�W���Z
                    if (atkBase == 0)
                    {
                        if (ii == 0)
                        {
                            damage = PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Random, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PlayerStance.FrontOffence, PrimaryLogic.SpellSkillType.Standard, this.DuelMode);
                        }
                        else
                        {
                            damage = PrimaryLogic.SubAttackValue(player, PrimaryLogic.NeedType.Random, 1.0F, 0, 0, 0, 1.0F, PlayerStance.FrontOffence, this.DuelMode);
                        }
                    }
                    else
                    {
                        damage = atkBase;
                    }
                    if (magnification > 0)
                    {
                        damage = damage * magnification;
                    }
                    if (player.CurrentSaintPower > 0)
                    {
                        damage = damage * 1.5F;
                    }
                    if (player.CurrentEternalPresence > 0)
                    {
                        damage = damage * 1.3F;
                    }
                    if (player.CurrentAetherDrive > 0)
                    {
                        damage = damage * 2.0F;
                    }
                    if (player.CurrentBlindJustice > 0)
                    {
                        damage = damage * 1.7F;
                    }
                    if (player.CurrentRisingAura > 0)
                    {
                        damage = damage * 1.4F;
                    }
                    if (player.CurrentMazeCube > 0)
                    {
                        damage = damage * PrimaryLogic.MazeCubeValue(player);
                    }
                    if (player.CurrentEternalFateRing > 0)
                    {
                        damage = damage * PrimaryLogic.EternalFateRingValue(player);
                    }

                    // �_���[�W�y��
                    damage -= PrimaryLogic.PhysicalDefenseValue(target, PrimaryLogic.NeedType.Random, this.DuelMode);
                    if (damage <= 0.0f) damage = 0.0f;

                    if (target.CurrentProtection > 0 && player.CurrentTruthVision <= 0)
                    {
                        damage = (int)((float)damage / 1.2F);
                    }
                    if (target.CurrentEternalPresence > 0)
                    {
                        damage = damage * 0.8F;
                    }
                    if (target.CurrentExaltedField > 0 && player.CurrentTruthVision <= 0)
                    {
                        damage = damage / 1.4F;
                    }
                    if (target.CurrentAetherDrive > 0 && player.CurrentTruthVision <= 0)
                    {
                        damage = damage * 0.5f;
                    }

                    if (ignoreDefense == false)
                    {
                        if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                        {
                            if ((target.CurrentAbsoluteZero > 0) ||
                                (target.CurrentFrozen > 0) ||
                                (target.CurrentParalyze > 0) ||
                                (target.CurrentStunning > 0))
                            {
                                UpdateBattleText(target.GetCharacterSentence(88));
                            }
                            else
                            {
                                if (target.SubWeapon != null)
                                {
                                    if (target.SubWeapon.Type == ItemBackPack.ItemType.Shield)
                                    {
                                        damage = damage / 4.0f;
                                    }
                                    else
                                    {
                                        damage = damage / 3.0f;
                                    }
                                }
                                else
                                {
                                    damage = damage / 3.0f;
                                }
                            }
                        }
                        // �����E�C���[�j�e�B�ɂ��y��
                        if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                        {
                            if (target.CurrentAbsoluteZero > 0)
                            {
                                UpdateBattleText(target.GetCharacterSentence(88));
                            }
                            else
                            {
                                damage = 0;
                            }
                        }
                    }

                    // �N���e�B�J������
                    bool detectCritical = false;
                    if (critical == CriticalType.Random) detectCritical = PrimaryLogic.CriticalDetect(player);
                    if (critical == CriticalType.None) detectCritical = false;
                    if (critical == CriticalType.Absolute) detectCritical = true;
                    if (crushingBlow > 0) detectCritical = false;
                    if (IsPlayerEnemy(player))
                    {
                        if (((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area11 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area12 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area13 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area14 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area21 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area22 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area23 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area24 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area31 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area32 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area33 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area34 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area41 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area42 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area43 ||
                            ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area44)
                            //((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area51) �Ō�̎G�����炢�̓N���e�B�J���ʏ픻��ŁB
                        {
                            if (AP.Math.RandomInteger(2) != 0) // �G���N���e�B�J���͓񕪂̈�ɋ@������炷
                            {
                                detectCritical = false;
                            }
                        }
                    }
                    if (detectCritical)
                    {
                        damage = damage * PrimaryLogic.CriticalDamageValue(player, this.DuelMode);
                        if (player.CurrentSinFortune > 0)
                        {
                            damage = damage * PrimaryLogic.SinFortuneValue(player);
                            player.RemoveSinFortune();
                        }
                    }


                    // ���ʉ��̍Đ�
                    if (soundName != string.Empty)
                    {
                        GroundOne.PlaySoundEffect(soundName);
                    }
                    else
                    {
                        if (player == ec1 || player == ec2 || player == ec3)
                        {
                            GroundOne.PlaySoundEffect(Database.SOUND_ENEMY_ATTACK1);
                        }
                        else
                        {
                            GroundOne.PlaySoundEffect(Database.SOUND_SWORD_SLASH1);
                        }
                    }
                    
                    // �f�t���N�V�������ʂ̓N���e�B�J���l�����f������
                    // �f�t���N�V�����ɂ�镨���U������
                    if (skipCounterPhase)
                    {
                        if (target.CurrentDeflection > 0)
                        {
                            UpdateBattleText(target.GetCharacterSentence(62));
                            this.Invoke(new _AnimationDamage(AnimationDamage), 0, target, 0, Color.Black, true, false, Database.FAIL_DEFLECTION);
                            target.CurrentDeflection = 0;
                            target.pbDeflection.Image = null;
                            target.pbDeflection.Update();
                        }
                    }
                    else
                    {
                        if (target.CurrentDeflection > 0)
                        {
                            damage = DamageIsZero(damage, player);
                            LifeDamage(damage, player);
                            target.CurrentDeflection = 0;
                            target.pbDeflection.Image = null;
                            target.pbDeflection.Update();
                            return true;
                        }
                    }

                    // StaticBarrier�ɂ�����
                    if (target.CurrentStaticBarrier > 0)
                    {
                        target.CurrentStaticBarrierValue--;
                        target.ChangeStaticBarrierStatus(target.CurrentStaticBarrierValue);
                        damage = damage * 0.5f;
                    }

                    // StanceOfMystic�ɂ�����
                    if (target.CurrentStanceOfMysticValue > 0)
                    {
                        target.CurrentStanceOfMysticValue--;
                        target.ChangeStanceOfMysticStatus(target.CurrentStanceOfMysticValue);
                        damage = 0;
                        LifeDamage(damage, target, interval, false);
                        return false; // �Ăяo�����Œǉ����ʂ��X�L�b�v�����邽�߂�false�Ԃ�
                    }
                    // HardestParry�ɂ�����
                    if (target.CurrentHardestParry)
                    {
                        target.CurrentHardestParry = false;
                        damage = 0;
                        LifeDamage(damage, target, interval, false);
                        return false; // �Ăяo�����Œǉ����ʂ��X�L�b�v�����邽�߂�false�Ԃ�
                    }
                    // �_���[�W�O�ϊ�
                    damage = DamageIsZero(damage, target);

                    // �X�P�[���E�I�u�E�u���[���C�W�ɂ�����
                    if ((target.MainArmor != null) && (target.MainArmor.Name == Database.RARE_SCALE_BLUERAGE))
                    {
                        if (AP.Math.RandomInteger(100) < PrimaryLogic.ScaleOfBlueRageValue(player))
                        {
                            this.Invoke(new _AnimationDamage(AnimationDamage), 0, target, 0, Color.Black, false, false, Database.IMMUNE_DAMAGE);
                            damage = 0;
                        }
                    }
                    // �X���C�h�E�X���[�E�V�[���h�ɂ�����
                    if ((target.SubWeapon != null) && (target.SubWeapon.Name == Database.RARE_SLIDE_THROUGH_SHIELD))
                    {
                        if (AP.Math.RandomInteger(100) < PrimaryLogic.SlideThroughShieldValue(player))
                        {
                            this.Invoke(new _AnimationDamage(AnimationDamage), 0, target, 0, Color.Black, false, false, Database.IMMUNE_DAMAGE);
                            damage = 0;
                        }
                    }

                    // ���b�Z�[�W�X�V
                    if (detectCritical)
                    {
                        UpdateBattleText(player.GetCharacterSentence(117));
                    }
                    if (soundName == Database.SOUND_STRAIGHT_SMASH)
                    {
                        UpdateBattleText(String.Format(player.GetCharacterSentence(124), target.Name, (int)damage), interval);
                    }
                    else if (textNumber != -1)
                    {
                        UpdateBattleText(String.Format(player.GetCharacterSentence(textNumber), target.Name, (int)damage), interval);
                    }
                    else
                    {
                        UpdateBattleText(String.Format(player.GetCharacterSentence(115), target.Name, (int)damage), interval);
                    }

                    // ���C�t���X�V
                    LifeDamage(damage, target, interval, detectCritical);

                    // �A�r�X�E�t�@�C�A�ɂ�����
                    if (player.CurrentAbyssFire > 0)
                    {
                        double effectValue = PrimaryLogic.AbyssFireValue(target); // �_���[�W�������̓��M�B���A�[�[
                        LifeDamage(effectValue, player, interval, detectCritical);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(120), player.Name, ((int)effectValue).ToString()), interval);
                    }

                    // �V�F�Y���E�~���[�W���E�����T�[�̏ꍇ�A�_�u���q�b�g�����Ƃ���B
                    if (((ii == 0) && (player.MainWeapon != null) && (player.MainWeapon.Name == Database.EPIC_SHEZL_THE_MIRAGE_LANCER)) ||
                        ((ii == 1) && (player.SubWeapon != null) && (player.SubWeapon.Name == Database.EPIC_SHEZL_THE_MIRAGE_LANCER)))
                    {
                        LifeDamage(damage, target, interval, detectCritical);
                    }

                    // �Ώێ҂̃V�[���E�I�u�E�o�����X�ɂ�����
                    if ((target.Accessory != null) && (target.Accessory.Name == Database.RARE_SEAL_OF_BALANCE))
                    {
                        PlayerAbstractManaGain(target, target, 0, PrimaryLogic.SealOfBalanceValue_A(target), 0, Database.SOUND_FRESH_HEAL, 5003);
                    }
                    if ((target.Accessory2 != null) && (target.Accessory2.Name == Database.RARE_SEAL_OF_BALANCE))
                    {
                        PlayerAbstractManaGain(target, target, 0, PrimaryLogic.SealOfBalanceValue_A(target), 0, Database.SOUND_FRESH_HEAL, 5003);
                    }

                    // �W���ƒf����ʂ�����ꍇ�A�r�؂ꂳ��
                    if (player.CurrentSyutyu_Danzetsu > 0)
                    {
                        player.CurrentSyutyu_Danzetsu = 0;
                        player.DeBuff(player.pbSyutyuDanzetsu);
                    }

                    // HolyBreaker�ɂ��_���[�W����
                    if (target.CurrentHolyBreaker > 0)
                    {
                        LifeDamage(damage, player);
                    }

                    // ���X���ɂ�����
                    if ((ii == 0) && (player.MainWeapon != null) && (player.MainWeapon.Name == Database.RARE_BLACK_ICE_SWORD) ||
                        (ii == 1) && (player.SubWeapon != null) && (player.SubWeapon.Name == Database.RARE_BLACK_ICE_SWORD))
                    {
                        double effectValue = PrimaryLogic.BlackIceSwordValue(player);
                        effectValue = GainIsZero(effectValue, player);
                        player.CurrentMana += (int)effectValue;
                        UpdateMana(player, (int)effectValue, true, true, 0);
                    }
                    // �����^���C�Y�h�E�t�H�[�X�E�N���[�ɂ�����
                    if ((ii == 0) && (player.MainWeapon != null) && (player.MainWeapon.Name == Database.RARE_MENTALIZED_FORCE_CLAW) ||
                        (ii == 1) && (player.SubWeapon != null) && (player.SubWeapon.Name == Database.RARE_MENTALIZED_FORCE_CLAW))
                    {
                        double effectValue = PrimaryLogic.MentalizedForceClawValue(player);
                        effectValue = GainIsZero(effectValue, player);
                        player.CurrentSkillPoint += (int)effectValue;
                        UpdateSkillPoint(player, (int)effectValue, true, true, 0);
                    }
                    // �N���C���A�E�I�u�E�U�b�N�X�ɂ�����
                    if ((ii == 0) && (player.MainWeapon != null) && (player.MainWeapon.Name == Database.RARE_CLAYMORE_ZUKS))
                    {
                        double effectValue = PrimaryLogic.ClaymoreZuksValue(player);
                        effectValue = GainIsZero(effectValue, player);
                        player.CurrentLife += (int)effectValue;
                        UpdateLife(player, (int)effectValue, true, true, 0, false);
                    }
                    // �\�[�h�E�I�u�E�f�B�o�C�h�ɂ�����
                    if ((ii == 0) && (player.MainWeapon != null) && (player.MainWeapon.Name == Database.RARE_SWORD_OF_DIVIDE) ||
                        (ii == 1) && (player.SubWeapon != null) && (player.SubWeapon.Name == Database.RARE_SWORD_OF_DIVIDE))
                    {
                        if (AP.Math.RandomInteger(100) < PrimaryLogic.SwordOfDivideValue_A(player))
                        {
                            double effectValue = PrimaryLogic.SwordOfDivideValue(target);
                            effectValue = DamageIsZero(effectValue, target);
                            LifeDamage(effectValue, target);
                        }
                    }
                    // �^�g���E�}�X�^�[�u���C�h�ɂ�����
                    if ((ii == 0) && (player.MainWeapon != null) && (player.MainWeapon.Name == Database.RARE_TRUERED_MASTER_BLADE) ||
                        (ii == 1) && (player.SubWeapon != null) && (player.SubWeapon.Name == Database.RARE_TRUERED_MASTER_BLADE))
                    {
                        if (AP.Math.RandomInteger(100) < PrimaryLogic.SinkouenMasterBladeValue_A(player))
                        {
                            // ���[�h�E�I�u�E�p���[�𔭓�
                            PlayerSpellWordOfPower(player, target, 0, 0);
                        }
                    }
                    // �f�r���L���[�ɂ�����
                    if ((ii == 0) && (player.MainWeapon != null) && (player.MainWeapon.Name == Database.RARE_DEVIL_KILLER) ||
                        (ii == 1) && (player.SubWeapon != null) && (player.SubWeapon.Name == Database.RARE_DEVIL_KILLER))
                    {
                        if (AP.Math.RandomInteger(100) < PrimaryLogic.DevilKillerValue(player))
                        {
                            PlayerDeath(player, target);
                        }
                    }

                    // �W���U�E�t�@���^�Y�}���E�N���[�ɂ�����
                    if ((ii == 0) && (player.MainWeapon != null) && (player.MainWeapon.Name == Database.EPIC_JUZA_THE_PHANTASMAL_CLAW) ||
                        (ii == 1) && (player.SubWeapon != null) && (player.SubWeapon.Name == Database.EPIC_JUZA_THE_PHANTASMAL_CLAW))
                    {
                        PlayerBuffAbstract(player, player, 999, Database.ITEMCOMMAND_JUZA_PHANTASMAL);
                    }

                    // �G�^�[�i���E�t�F�C�g�����O�ɂ�����
                    if ((player.Accessory != null) && (player.Accessory.Name == Database.EPIC_FATE_RING_OMEGA))
                    {
                        PlayerBuffAbstract(player, player, 999, Database.ITEMCOMMAND_ETERNAL_FATE);
                    }
                    if ((player.Accessory2 != null) && (player.Accessory2.Name == Database.EPIC_FATE_RING_OMEGA))
                    {
                        PlayerBuffAbstract(player, player, 999, Database.ITEMCOMMAND_ETERNAL_FATE);
                    }

                    // �G�^�[�i���E���C���������O�ɂ�����
                    if ((player.Accessory != null) && (player.Accessory.Name == Database.EPIC_FATE_RING_OMEGA))
                    {
                        double effectValue = PrimaryLogic.EternalLoyalRingValue(player);
                        effectValue = GainIsZero(effectValue, player);
                        player.CurrentSkillPoint += (int)effectValue;
                        UpdateSkillPoint(player, (int)effectValue, true, true, 0);
                    }
                    if ((player.Accessory2 != null) && (player.Accessory2.Name == Database.EPIC_FATE_RING_OMEGA))
                    {
                        double effectValue = PrimaryLogic.EternalLoyalRingValue(player);
                        effectValue = GainIsZero(effectValue, player);
                        player.CurrentSkillPoint += (int)effectValue;
                        UpdateSkillPoint(player, (int)effectValue, true, true, 0);
                    }

                    // ���C�g�E�T�[���@���g�ɂ�����
                    if ((player.Accessory != null) && (player.Accessory.Name == Database.COMMON_LIGHT_SERVANT))
                    {
                        PlayerBuffAbstract(player, player, 999, Database.ITEMCOMMAND_LIGHT_SERVANT);
                    }
                    if ((player.Accessory2 != null) && (player.Accessory2.Name == Database.COMMON_LIGHT_SERVANT))
                    {
                        PlayerBuffAbstract(player, player, 999, Database.ITEMCOMMAND_LIGHT_SERVANT);
                    }

                    // �V���h�E�E�T�[���@���g�ɂ�����
                    if ((player.Accessory != null) && (player.Accessory.Name == Database.COMMON_SHADOW_SERVANT))
                    {
                        PlayerBuffAbstract(player, player, 999, Database.ITEMCOMMAND_SHADOW_SERVANT);
                    }
                    if ((player.Accessory2 != null) && (player.Accessory2.Name == Database.COMMON_SHADOW_SERVANT))
                    {
                        PlayerBuffAbstract(player, player, 999, Database.ITEMCOMMAND_SHADOW_SERVANT);
                    }

                    // ���C�Y�E�L���[�u�ɂ�����
                    if ((player.Accessory != null) && (player.Accessory.Name == Database.COMMON_MAZE_CUBE) && (player.Accessory.SwitchStatus1 == false))
                    {
                        player.Accessory.SwitchStatus1 = true;
                        PlayerBuffAbstract(player, player, 999, Database.ITEMCOMMAND_MAZE_CUBE);
                    }
                    if ((player.Accessory2 != null) && (player.Accessory2.Name == Database.COMMON_MAZE_CUBE) && (player.Accessory2.SwitchStatus1 == false))
                    {
                        player.Accessory2.SwitchStatus1 = true;
                        PlayerBuffAbstract(player, player, 999, Database.ITEMCOMMAND_MAZE_CUBE);
                    }

                    // �G���u�����E�I�u�E���@���L���[�ɂ�����
                    if ((player.Accessory != null) && (player.Accessory.Name == Database.RARE_EMBLEM_OF_VALKYRIE) ||
                        (player.Accessory2 != null) && (player.Accessory2.Name == Database.RARE_EMBLEM_OF_VALKYRIE))
                    {
                        if (AP.Math.RandomInteger(100) < PrimaryLogic.EmblemOfValkyrieValue(player))
                        {
                            NowStunning(player, target, (int)PrimaryLogic.EmblemOfValkyrieValue_A(player));
                        }
                    }
                    // �G���u�����E�I�u�E�n�f�X�ɂ�����
                    if ((player.Accessory != null) && (player.Accessory.Name == Database.RARE_EMBLEM_OF_HADES) ||
                        (player.Accessory2 != null) && (player.Accessory2.Name == Database.RARE_EMBLEM_OF_HADES))
                    {
                        if (AP.Math.RandomInteger(100) < PrimaryLogic.EmblemOfHades(player))
                        {
                            PlayerDeath(player, target);
                        }
                    }
                    // �X���̕��ɂ�����
                    if ((player.Accessory != null) && (player.Accessory.Name == Database.EPIC_ORB_SILENT_COLD_ICE) ||
                        (player.Accessory2 != null) && (player.Accessory2.Name == Database.EPIC_ORB_SILENT_COLD_ICE))
                    {
                        if (AP.Math.RandomInteger(100) < PrimaryLogic.SilentColdIceValue(player))
                        {
                            NowFrozen(player, target, (int)PrimaryLogic.SilentColdIceValue_A(player));
                            target.RemoveBuffSpell();
                        }
                    }

                    // CrushingBlow�ɂ��C��
                    if (crushingBlow > 0)
                    {
                        UpdateBattleText(String.Format(player.GetCharacterSentence(70), target.Name, (int)damage));
                        if (target.CurrentAntiStun > 0)
                        {
                            target.RemoveAntiStun();
                            UpdateBattleText(target.GetCharacterSentence(94));
                        }
                        else
                        {
                            if ((target.Accessory != null) && (target.Accessory.Name == "�|�S�̐Α�"))
                            {
                                Random rd3 = new Random(DateTime.Now.Millisecond * Environment.TickCount);
                                if (rd3.Next(1, 101) <= target.Accessory.MinValue)
                                {
                                    UpdateBattleText(target.Name + "���������Ă���|�S�̐Α�������P�����I\r\n", 1000);
                                    UpdateBattleText(target.Name + "�̓X�^����ԂɊׂ�Ȃ������B\r\n");
                                }
                                else
                                {
                                    NowStunning(player, target, crushingBlow);                                  
                                }
                            }
                            else
                            {
                                NowStunning(player, target, crushingBlow);
                            }
                        }
                    }

                    // FlameAura�ɂ��ǉ��U��
                    if (player.CurrentFlameAura > 0)
                    {
                        double additional = PrimaryLogic.FlameAuraValue(player, this.DuelMode);
                        if (ignoreDefense == false)
                        {
                            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    additional = (int)((float)additional / 3.0F);
                                }
                            }
                            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    additional = 0;
                                }
                            }
                        }

                        additional = DamageIsZero(additional, target);
                        LifeDamage(additional, target, interval);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(14), additional.ToString()));
                    }
                    // FrozenAura�ɂ��ǉ��U��
                    if (player.CurrentFrozenAura > 0)
                    {
                        double additional = PrimaryLogic.FrozenAuraValue(player, this.DuelMode);
                        if (ignoreDefense == false)
                        {
                            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    additional = (int)((float)additional / 3.0F);
                                }
                            }
                            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    additional = 0;
                                }
                            }
                        }
                        additional = DamageIsZero(additional, target);
                        LifeDamage(additional, target, interval);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(140), additional.ToString()));
                    }

                    // ImmortalRave�ɂ��ǉ��U��
                    if (player.CurrentImmortalRave == 3)
                    {
                        PlayerSpellFireBall(player, target, 0, 0);
                    }
                    else if (player.CurrentImmortalRave == 2)
                    {
                        PlayerSpellFlameStrike(player, target, 0, 0);
                    }
                    else if (player.CurrentImmortalRave == 1)
                    {
                        PlayerSpellVolcanicWave(player, target, 0, 0);
                    }
                }

                // �T�u�E�F�|��������ꍇ�A���U���ƂȂ�B
                if (player.SubWeapon == null)
                {
                    return true;
                }
                if (player.SubWeapon.Name == "")
                {
                    return true;
                }
                if ((player.SubWeapon.Type == ItemBackPack.ItemType.Weapon_Rod || player.SubWeapon.Type == ItemBackPack.ItemType.Weapon_TwoHand || player.SubWeapon.Type == ItemBackPack.ItemType.Shield))
                {
                    return true;
                }
                // �X�L�������U������̏ꍇ�A�T�u�E�F�|��2��U���������I�Ɏ��{���Ȃ��ꍇ
                if (ignoreDoubleAttack)
                {
                    return true;
                }
            }
            return true;
        }

        /// <summary>
        /// ���@�_���[�W�̃��W�b�N
        /// </summary>
        /// <param name="player">�Ώی�</param>
        /// <param name="target">�Ώۑ���</param>
        /// <param name="interval">������̃C���^�[�o��</param>
        /// <param name="damage">�_���[�W</param> // ref�Q�Ɓ@DevouringPlague�̎Q�ƌ��ŉ񕜗ʂɋt�Z�������̂��g�p���邽��
        /// <param name="magnification">�����{���A�O�̏ꍇ�͑������Ȃ�</param>
        /// <param name="soundName">���ʉ��t�@�C����</param>
        /// <param name="messageNumber">���@�_���[�W���b�Z�[�W</param>
        /// <param name="magicType">���@����</param>
        /// <param name="ignoreTargetDefense">�Ώۂ̖h��𖳎�����ꍇ�ATrue</param>
        /// <param name="critical">�N���e�B�J���L���t���O</param>
        private bool AbstractMagicDamage(MainCharacter player, MainCharacter target,
            int interval, double damage, double magnification, string soundName, int messageNumber, TruthActionCommand.MagicType magicType, bool ignoreTargetDefense, CriticalType critical)
        {
            return AbstractMagicDamage(player, target, interval, ref damage, magnification, soundName, messageNumber, magicType, ignoreTargetDefense, critical);
        }
        private bool AbstractMagicDamage(MainCharacter player, MainCharacter target,
            int interval, ref double damage, double magnification, string soundName, int messageNumber, TruthActionCommand.MagicType magicType, bool ignoreTargetDefense, CriticalType critical)
        {
            if (CheckDodge(player, target))
            {
                this.Invoke(new _AnimationDamage(AnimationDamage), 0, target, 0, Color.Black, true, false, String.Empty);
                return false; 
            }
            if (CheckBlindMiss(player, target))
            {
                this.Invoke(new _AnimationDamage(AnimationDamage), 0, target, 0, Color.Black, true, false, String.Empty);
                return false;
            }

            if (CheckSilence(player))
            {
                this.Invoke(new _AnimationDamage(AnimationDamage), 0, player, 0, Color.Black, false, false, Database.MISS_SPELL);
                return false;
            }


            // �_���[�W���Z
            if (damage == 0)
            {
                damage = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, this.DuelMode);
            }
            // �_���[�W�u�~�v����
            if (magnification > 0)
            {
                damage = damage * magnification;
            }
            if (player.CurrentShadowPact > 0)
            {
                damage = damage * 1.5F;
            }
            if (player.CurrentEternalPresence > 0)
            {
                damage = damage * 1.3F;
            }
            if (player.CurrentPsychicTrance > 0)
            {
                damage = damage * 1.7F;
            }
            if (player.CurrentAscensionAura > 0)
            {
                damage = damage * 1.4F;
            }
            if (player.CurrentMazeCube > 0)
            {
                damage = damage * PrimaryLogic.MazeCubeValue(player);
            }

            damage = player.AmplifyMagicByEquipment(damage, magicType);

            if (player.CurrentRedDragonWill > 0)
            {
                if ((magicType == TruthActionCommand.MagicType.Fire) ||
                    (magicType == TruthActionCommand.MagicType.Fire_Force) ||
                    (magicType == TruthActionCommand.MagicType.Fire_Ice) ||
                    (magicType == TruthActionCommand.MagicType.Fire_Will) ||
                    (magicType == TruthActionCommand.MagicType.Light_Fire) ||
                    (magicType == TruthActionCommand.MagicType.Shadow_Fire))
                {
                    damage = damage * 1.5F;
                }
            }
            if (player.CurrentBlueDragonWill > 0)
            {
                if ((magicType == TruthActionCommand.MagicType.Ice) ||
                    (magicType == TruthActionCommand.MagicType.Fire_Ice) ||
                    (magicType == TruthActionCommand.MagicType.Ice_Force) ||
                    (magicType == TruthActionCommand.MagicType.Ice_Will) ||
                    (magicType == TruthActionCommand.MagicType.Light_Ice) ||
                    (magicType == TruthActionCommand.MagicType.Shadow_Ice))
                {
                    damage = damage * 1.5F;
                }
            }

            // �_���[�W�u�{�v�ǉ�
            if ((magicType == TruthActionCommand.MagicType.Light) ||
                (magicType == TruthActionCommand.MagicType.Light_Fire) ||
                (magicType == TruthActionCommand.MagicType.Light_Force) ||
                (magicType == TruthActionCommand.MagicType.Light_Ice) ||
                (magicType == TruthActionCommand.MagicType.Light_Shadow) ||
                (magicType == TruthActionCommand.MagicType.Light_Will))
            {
                damage += player.CurrentLightUpValue;
                damage -= player.CurrentLightDownValue;
            }
            if ((magicType == TruthActionCommand.MagicType.Shadow) ||
                (magicType == TruthActionCommand.MagicType.Shadow_Fire) ||
                (magicType == TruthActionCommand.MagicType.Shadow_Force) ||
                (magicType == TruthActionCommand.MagicType.Shadow_Ice) ||
                (magicType == TruthActionCommand.MagicType.Shadow_Will) ||
                (magicType == TruthActionCommand.MagicType.Light_Shadow))
            {
                damage += player.CurrentShadowUpValue;
                damage -= player.CurrentShadowDownValue;
            }
            if ((magicType == TruthActionCommand.MagicType.Fire) ||
                (magicType == TruthActionCommand.MagicType.Fire_Force) ||
                (magicType == TruthActionCommand.MagicType.Fire_Ice) ||
                (magicType == TruthActionCommand.MagicType.Fire_Will) ||
                (magicType == TruthActionCommand.MagicType.Light_Fire) ||
                (magicType == TruthActionCommand.MagicType.Shadow_Fire))
            {
                damage += player.CurrentFireUpValue;
                damage -= player.CurrentFireDownValue;
            }
            if ((magicType == TruthActionCommand.MagicType.Ice) ||
                (magicType == TruthActionCommand.MagicType.Ice_Force) ||
                (magicType == TruthActionCommand.MagicType.Ice_Will) ||
                (magicType == TruthActionCommand.MagicType.Light_Ice) ||
                (magicType == TruthActionCommand.MagicType.Shadow_Ice) ||
                (magicType == TruthActionCommand.MagicType.Fire_Ice))
            {
                damage += player.CurrentIceUpValue;
                damage -= player.CurrentIceDownValue;
            }
            if ((magicType == TruthActionCommand.MagicType.Force) ||
                (magicType == TruthActionCommand.MagicType.Force_Will) ||
                (magicType == TruthActionCommand.MagicType.Light_Force) ||
                (magicType == TruthActionCommand.MagicType.Shadow_Force) ||
                (magicType == TruthActionCommand.MagicType.Fire_Force) ||
                (magicType == TruthActionCommand.MagicType.Ice_Force))
            {
                damage += player.CurrentForceUpValue;
                damage -= player.CurrentForceDownValue;
            }
            if ((magicType == TruthActionCommand.MagicType.Will) ||
                (magicType == TruthActionCommand.MagicType.Light_Will) ||
                (magicType == TruthActionCommand.MagicType.Shadow_Will) ||
                (magicType == TruthActionCommand.MagicType.Fire_Will) ||
                (magicType == TruthActionCommand.MagicType.Ice_Will) ||
                (magicType == TruthActionCommand.MagicType.Force_Will))
            {
                damage += player.CurrentWillUpValue;
                damage -= player.CurrentWillDownValue;
            }

            // �_���[�W�y��
            if (magicType == TruthActionCommand.MagicType.Force || ignoreTargetDefense)
            {
                // ���[�h���@�̓_���[�W�z���ł��Ȃ��B
            }
            else
            {
                damage -= PrimaryLogic.MagicDefenseValue(target, PrimaryLogic.NeedType.Random, this.DuelMode);
                if (damage <= 0.0f) damage = 0.0f;

                if (target.CurrentAbsorbWater > 0 && player.CurrentTruthVision <= 0)
                {
                    damage = damage / 1.3F;
                }
                if (target.CurrentEternalPresence > 0)
                {
                    damage = damage * 0.8F;
                }
                if (target.CurrentExaltedField > 0 && player.CurrentTruthVision <= 0)
                {
                    damage = damage / 1.4F;
                }

                if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                {
                    if ((target.CurrentAbsoluteZero > 0) ||
                        (target.CurrentFrozen > 0) ||
                        (target.CurrentParalyze > 0) ||
                        (target.CurrentStunning > 0))
                    {
                        UpdateBattleText(target.GetCharacterSentence(88));
                    }
                    else
                    {
                        if (target.SubWeapon != null)
                        {
                            if (target.SubWeapon.Type == ItemBackPack.ItemType.Shield)
                            {
                                damage = damage / 4.0f;
                            }
                            else
                            {
                                damage = damage / 3.0f;
                            }
                        }
                        else
                        {
                            damage = damage / 3.0f;
                        }
                    }
                }
                if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                {
                    if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(88));
                    }
                    else
                    {
                        damage = 0;
                    }
                }

                if (magicType == TruthActionCommand.MagicType.Fire)
                {
                    if (target.CurrentSigilOfHomura > 0)
                    {
                        // SigilOfHomura���L���̏ꍇ�A�΂̃_���[�W�y���͍s���Ȃ��B
                    }
                    else
                    {
                        if (target.ResistFire > 0)
                        {
                            damage -= target.ResistFire;
                        }
                        if (target.CurrentResistFireUp > 0)
                        {
                            damage -= target.CurrentResistFireUpValue;
                        }

                        if (target.MainWeapon != null)
                        {
                            damage -= target.MainWeapon.ResistFire;
                        }
                        if (target.SubWeapon != null)
                        {
                            damage -= target.SubWeapon.ResistFire;
                        }
                        if (target.MainArmor != null)
                        {
                            damage -= target.MainArmor.ResistFire;
                        }
                        if (target.Accessory != null)
                        {
                            // ���d�l�̂��߁A���̒l�őϐ��l�������B
                            if (target.Accessory.Name == Database.COMMON_CHARM_OF_FIRE_ANGEL)
                            {
                                damage -= target.Accessory.MinValue;
                            }
                            else
                            {
                                damage -= target.Accessory.ResistFire;
                            }
                            if (target.Accessory.Name == Database.RARE_SEAL_AQUA_FIRE)
                            {
                                damage = damage * (100.0F - (float)target.Accessory.MinValue) / 100.0F;
                            }
                        }
                        if (target.Accessory2 != null)
                        {
                            // ���d�l�̂��߁A���̒l�őϐ��l�������B
                            if (target.Accessory2.Name == Database.COMMON_CHARM_OF_FIRE_ANGEL)
                            {
                                damage -= target.Accessory2.MinValue;
                            }
                            else
                            {
                                damage -= target.Accessory2.ResistFire;
                            }

                            if (target.Accessory2.Name == Database.RARE_SEAL_AQUA_FIRE)
                            {
                                damage = damage * (100.0F - (float)target.Accessory2.MinValue) / 100.0F;
                            }

                        }
                    }
                    if (damage <= 0) damage = 0;
                }
                if (magicType == TruthActionCommand.MagicType.Ice)
                {
                    if (target.ResistIce > 0)
                    {
                        damage -= target.ResistIce;
                    }
                    if (target.CurrentResistIceUp > 0)
                    {
                        damage -= target.CurrentResistIceUpValue;
                    }

                    if (target.MainWeapon != null)
                    {
                        damage -= target.MainWeapon.ResistIce;
                    }
                    if (target.SubWeapon != null)
                    {
                        damage -= target.SubWeapon.ResistIce;
                    }
                    if (target.MainArmor != null)
                    {
                        damage -= target.MainArmor.ResistIce;
                    }
                    if (target.Accessory != null)
                    {
                        // ���d�l�̂��߁A���̒l�őϐ��l�������B
                        if (target.Accessory.Name == Database.RARE_SEAL_AQUA_FIRE)
                        {
                            damage = damage * (100.0F - (float)target.Accessory.MinValue) / 100.0F;
                        }
                        else
                        {
                            damage -= target.Accessory.ResistIce;
                        }
                    }
                    if (target.Accessory2 != null)
                    {
                        // ���d�l�̂��߁A���̒l�őϐ��l�������B
                        if (target.Accessory2.Name == Database.RARE_SEAL_AQUA_FIRE)
                        {
                            damage = damage * (100.0F - (float)target.Accessory2.MinValue) / 100.0F;
                        }
                        else
                        {
                            damage -= target.Accessory2.ResistIce;
                        }
                    }
                    if (damage <= 0) damage = 0;
                }
                if (magicType == TruthActionCommand.MagicType.Light)
                {
                    if (target.ResistLight > 0)
                    {
                        damage -= target.ResistLight;
                    }
                    if (target.CurrentResistLightUp > 0)
                    {
                        damage -= target.CurrentResistLightUpValue;
                    }

                    if (target.MainWeapon != null)
                    {
                        damage -= target.MainWeapon.ResistLight;
                    }
                    if (target.SubWeapon != null)
                    {
                        damage -= target.SubWeapon.ResistLight;
                    }
                    if (target.MainArmor != null)
                    {
                        damage -= target.MainArmor.ResistLight;
                    }
                    if (target.Accessory != null)
                    {
                        damage -= target.Accessory.ResistLight;
                    }
                    if (target.Accessory2 != null)
                    {
                        damage -= target.Accessory2.ResistLight;
                    }
                    if (damage <= 0) damage = 0;
                }
                if (magicType == TruthActionCommand.MagicType.Shadow)
                {
                    if (target.ResistShadow > 0)
                    {
                        damage -= target.ResistShadow;
                    }
                    if (target.CurrentResistShadowUp > 0)
                    {
                        damage -= target.CurrentResistShadowUpValue;
                    }

                    if (target.MainWeapon != null)
                    {
                        damage -= target.MainWeapon.ResistShadow;
                    }
                    if (target.SubWeapon != null)
                    {
                        damage -= target.SubWeapon.ResistShadow;
                    }
                    if (target.MainArmor != null)
                    {
                        damage -= target.MainArmor.ResistShadow;
                    } 
                    if (target.Accessory != null)
                    {
                        damage -= target.Accessory.ResistShadow;
                    }
                    if (target.Accessory2 != null)
                    {
                        damage -= target.Accessory2.ResistShadow;
                    } 
                    if (damage <= 0) damage = 0;
                }
                if (magicType == TruthActionCommand.MagicType.Force)
                {
                    if (target.ResistForce > 0)
                    {
                        damage -= target.ResistForce;
                    }
                    if (target.CurrentResistForceUp > 0)
                    {
                        damage -= target.CurrentResistForceUpValue;
                    }

                    if (target.MainWeapon != null)
                    {
                        damage -= target.MainWeapon.ResistForce;
                    }
                    if (target.SubWeapon != null)
                    {
                        damage -= target.SubWeapon.ResistForce;
                    }
                    if (target.MainArmor != null)
                    {
                        damage -= target.MainArmor.ResistForce;
                    }
                    if (target.Accessory != null)
                    {
                        damage -= target.Accessory.ResistForce;
                    }
                    if (target.Accessory2 != null)
                    {
                        damage -= target.Accessory2.ResistForce;
                    }
                    if (damage <= 0) damage = 0;
                }
                if (magicType == TruthActionCommand.MagicType.Will)
                {
                    if (target.ResistWill > 0)
                    {
                        damage -= target.ResistWill;
                    }
                    if (target.CurrentResistWillUp > 0)
                    {
                        damage -= target.CurrentResistWillUpValue;
                    }

                    if (target.MainWeapon != null)
                    {
                        damage -= target.MainWeapon.ResistWill;
                    }
                    if (target.SubWeapon != null)
                    {
                        damage -= target.SubWeapon.ResistWill;
                    }
                    if (target.MainArmor != null)
                    {
                        damage -= target.MainArmor.ResistWill;
                    }
                    if (target.Accessory != null)
                    {
                        damage -= target.Accessory.ResistWill;
                    }
                    if (target.Accessory2 != null)
                    {
                        damage -= target.Accessory2.ResistWill;
                    }
                    if (damage <= 0) damage = 0;
                }
                // [�x��]�������@�͂ǂ������Ă����̂��H
            }


            // ��҂���X�y���N���e�B�J�����̗p
            bool detectCritical = false;
            if (critical == CriticalType.Random) detectCritical = PrimaryLogic.CriticalDetect(player);
            if (critical == CriticalType.None) detectCritical = false;
            if (critical == CriticalType.Absolute) detectCritical = true;
            if (IsPlayerEnemy(player))
            {
                if (((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area11 ||
                    ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area12 ||
                    ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area13 ||
                    ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area14 ||
                    ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area21 ||
                    ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area22 ||
                    ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area23 ||
                    ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area24 ||
                    ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area31 ||
                    ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area32 ||
                    ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area33 ||
                    ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area34 ||
                    ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area41 ||
                    ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area42 ||
                    ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area43 ||
                    ((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area44)
                //((TruthEnemyCharacter)player).Area == TruthEnemyCharacter.MonsterArea.Area51) �Ō�̎G�����炢�̓N���e�B�J���ʏ픻��ŁB
                {
                    if (AP.Math.RandomInteger(2) != 0) // �G���N���e�B�J���͓񕪂̈�ɋ@������炷
                    {
                        detectCritical = false;
                    }
                }
            }

            if (detectCritical)
            {
                damage = damage * PrimaryLogic.CriticalDamageValue(player, this.DuelMode);
                if (player.CurrentSinFortune > 0)
                {
                    damage = damage * PrimaryLogic.SinFortuneValue(player);
                    player.RemoveSinFortune();
                }
            }

            // ���ʉ��̍Đ�
            if (soundName != String.Empty)
            {
                GroundOne.PlaySoundEffect(soundName);
            }

            // MirrorImage�ɂ�����
            if (magicType == TruthActionCommand.MagicType.Force)
            {
                // ���[�h���@�͔��˂ł��Ȃ��B
            }
            else
            {
                if (target.CurrentMirrorImage > 0)
                {
                    this.Invoke(new _AnimationDamage(AnimationDamage), 0, target, 0, Color.Black, false, false, "����");

                    damage = DamageIsZero(damage, player);
                    LifeDamage(damage, player);
                    UpdateBattleText(String.Format(target.GetCharacterSentence(58), ((int)damage).ToString(), player.Name), 1000);

                    target.CurrentMirrorImage = 0;
                    target.DeBuff(target.pbMirrorImage);
                    return true;
                }
            }
            // SkyShield�ɂ�����
            if (target.CurrentSkyShieldValue > 0)
            {
                target.CurrentSkyShieldValue--;
                target.ChangeSkyShieldStatus(target.CurrentSkyShieldValue);
                damage = 0;
            }
            // StaticBarrier�̌���
            if (target.CurrentStaticBarrier > 0)
            {
                target.CurrentStaticBarrierValue--;
                target.ChangeStaticBarrierStatus(target.CurrentStaticBarrierValue);
                damage = damage * 0.5f;
            }
            // StanceOfMystic�ɂ�����
            if (target.CurrentStanceOfMysticValue > 0)
            {
                target.CurrentStanceOfMysticValue--;
                target.ChangeStanceOfMysticStatus(target.CurrentStanceOfMysticValue);
                damage = 0;
                LifeDamage(damage, target, interval, false);
                return false; // �Ăяo�����Œǉ����ʂ��X�L�b�v�����邽�߂�false�Ԃ�
            }
            // HardestParry�ɂ�����
            if (target.CurrentHardestParry)
            {
                target.CurrentHardestParry = false;
                damage = 0;
                LifeDamage(damage, target, interval, false);
                return false; // �Ăяo�����Œǉ����ʂ��X�L�b�v�����邽�߂�false�Ԃ�
            }
            // �_���[�W�O�ϊ�
            damage = DamageIsZero(damage, target);

            // �u���[�E���t���N�g�E���[�u�ɂ�����
            if ((target.MainArmor != null) && (target.MainArmor.Name == Database.RARE_BLUE_REFLECT_ROBE))
            {
                if (AP.Math.RandomInteger(100) < PrimaryLogic.BlueReflectRobeValue(player))
                {
                    this.Invoke(new _AnimationDamage(AnimationDamage), 0, target, 0, Color.Black, false, false, Database.IMMUNE_DAMAGE);
                    damage = 0;
                }
            }
            // �X���C�h�E�X���[�E�V�[���h�ɂ�����
            if ((target.SubWeapon != null) && (target.SubWeapon.Name == Database.RARE_SLIDE_THROUGH_SHIELD))
            {
                if (AP.Math.RandomInteger(100) < PrimaryLogic.SlideThroughShieldValue(player))
                {
                    this.Invoke(new _AnimationDamage(AnimationDamage), 0, target, 0, Color.Black, false, false, Database.IMMUNE_DAMAGE);
                    damage = 0;
                }
            }

            // ���b�Z�[�W�X�V
            if (detectCritical)
            {
                UpdateBattleText(player.GetCharacterSentence(117));
            }

            // ���C�t���X�V
            LifeDamage(damage, target, interval, detectCritical);
            if (soundName == "DevouringPlague.mp3")
            {
                UpdateBattleText(String.Format(player.GetCharacterSentence(messageNumber), ((int)damage).ToString()), interval);
            }
            else
            {
                UpdateBattleText(String.Format(player.GetCharacterSentence(120), target.Name, ((int)damage).ToString()), interval);
            }

            // �A�r�X�E�t�@�C�A�ɂ�����
            if (player.CurrentAbyssFire > 0)
            {
                double effectValue = PrimaryLogic.AbyssFireValue(ec1); // �_���[�W�������̓��M�B���A�[�[
                LifeDamage(effectValue, player, interval);
                UpdateBattleText(String.Format(player.GetCharacterSentence(120), player.Name, ((int)effectValue).ToString()), interval);
            }

            // �Ώێ҂̃V�[���E�I�u�E�o�����X�ɂ�����
            if ((target.Accessory != null) && (target.Accessory.Name == Database.RARE_SEAL_OF_BALANCE))
            {
                PlayerAbstractSkillGain(target, target, 0, PrimaryLogic.RainbowTubeValue_B(target, this.DuelMode), 0, Database.SOUND_FRESH_HEAL, 5009);
            }
            if ((target.Accessory2 != null) && (target.Accessory2.Name == Database.RARE_SEAL_OF_BALANCE))
            {
                PlayerAbstractSkillGain(target, target, 0, PrimaryLogic.RainbowTubeValue_B(target, this.DuelMode), 0, Database.SOUND_FRESH_HEAL, 5009);
            }

            // �W���ƒf����ʂ�����ꍇ�A�r�؂ꂳ��
            if (player.CurrentSyutyu_Danzetsu > 0)
            {
                player.CurrentSyutyu_Danzetsu = 0;
                player.DeBuff(player.pbSyutyuDanzetsu);
            }

            // SigilOfHomura�ɂ��ǉ�����
            if (target.CurrentSigilOfHomura > 0)
            {
                UpdateBattleText("���̈󂪐Ԃ��P���I\r\n");
                LifeDamage(damage, target, interval, detectCritical);
                UpdateBattleText(String.Format(player.GetCharacterSentence(120), target.Name, ((int)damage).ToString()), interval);
            }

            // �A�_�[�J�[�E�t�H���X�E���b�h�ɂ�����
            if ((player.MainWeapon != null) && (player.MainWeapon.Name == Database.RARE_ADERKER_FALSE_ROD))
            {
                double effectValue = PrimaryLogic.AderkerFalseRodValue(player);
                player.CurrentInstantPoint += (int)effectValue;
                UpdateInstantPoint(player);
            }
            // �^�g���E�}�X�^�[�u���C�h�ɂ�����
            if ((player.MainWeapon != null) && (player.MainWeapon.Name == Database.RARE_TRUERED_MASTER_BLADE) ||
                (player.SubWeapon != null) && (player.SubWeapon.Name == Database.RARE_TRUERED_MASTER_BLADE))
            {
                if (AP.Math.RandomInteger(100) < PrimaryLogic.SinkouenMasterBladeValue_A(player))
                {
                    // �T�C�L�b�N�E�E�F�C�u�𔭓�
                    PlayerSkillPsychicWave(player, target);
                }
            }

            // ���C�g�E�T�[���@���g�ɂ�����
            if ((player.Accessory != null) && (player.Accessory.Name == Database.COMMON_LIGHT_SERVANT))
            {
                PlayerBuffAbstract(player, player, 999, Database.ITEMCOMMAND_LIGHT_SERVANT);
            }
            if ((player.Accessory2 != null) && (player.Accessory2.Name == Database.COMMON_LIGHT_SERVANT))
            {
                PlayerBuffAbstract(player, player, 999, Database.ITEMCOMMAND_LIGHT_SERVANT);
            }

            // �V���h�E�E�T�[���@���g�ɂ�����
            if ((player.Accessory != null) && (player.Accessory.Name == Database.BUFF_SHADOW_SERVANT))
            {
                PlayerBuffAbstract(player, player, 999, Database.ITEMCOMMAND_SHADOW_SERVANT);
            }
            if ((player.Accessory2 != null) && (player.Accessory2.Name == Database.BUFF_SHADOW_SERVANT))
            {
                PlayerBuffAbstract(player, player, 999, Database.ITEMCOMMAND_SHADOW_SERVANT);
            }

            // ���C�Y�E�L���[�u�ɂ�����
            if ((player.Accessory != null) && (player.Accessory.Name == Database.COMMON_MAZE_CUBE) && (player.Accessory.SwitchStatus1 == true))
            {
                player.Accessory.SwitchStatus1 = false;
                PlayerBuffAbstract(player, player, 999, Database.ITEMCOMMAND_MAZE_CUBE);
            }
            if ((player.Accessory2 != null) && (player.Accessory2.Name == Database.COMMON_MAZE_CUBE) && (player.Accessory2.SwitchStatus1 == true))
            {
                player.Accessory2.SwitchStatus1 = false;
                PlayerBuffAbstract(player, player, 999, Database.ITEMCOMMAND_MAZE_CUBE);
            }

            // �G���u�����E�I�u�E���@���L���[�ɂ�����
            if ((player.Accessory != null) && (player.Accessory.Name == Database.RARE_EMBLEM_OF_VALKYRIE) ||
                (player.Accessory2 != null) && (player.Accessory2.Name == Database.RARE_EMBLEM_OF_VALKYRIE))
            {
                if (AP.Math.RandomInteger(100) < PrimaryLogic.EmblemOfValkyrieValue(player))
                {
                    NowStunning(player, target, (int)PrimaryLogic.EmblemOfValkyrieValue_A(player));
                }
            }
            // �G���u�����E�I�u�E�n�f�X�ɂ�����
            if ((player.Accessory != null) && (player.Accessory.Name == Database.RARE_EMBLEM_OF_HADES) ||
                (player.Accessory2 != null) && (player.Accessory2.Name == Database.RARE_EMBLEM_OF_HADES))
            {
                if (AP.Math.RandomInteger(100) < PrimaryLogic.EmblemOfHades(player))
                {
                    PlayerDeath(player, target);
                }
            }
            return true;
        }




        /// <summary>
        /// ���@�U���̃��\�b�h
        /// </summary>
        private void PlayerMagicAttack(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            double damage = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Random, 1.0f, 0.0f, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, this.DuelMode);
            AbstractMagicDamage(player, target, interval, ref damage, magnification, Database.SOUND_MAGIC_ATTACK, 120, TruthActionCommand.MagicType.None, false, CriticalType.Random);
        }

        // �G�Ώ�
        /// <summary>
        /// �_�[�N�E�u���X�g�̃��\�b�h
        /// </summary>
        private void PlayerSpellDarkBlast(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            double damage = PrimaryLogic.DarkBlastValue(player, this.DuelMode);
            AbstractMagicDamage(player, target, interval, ref damage, magnification, "DarkBlast.mp3", 27, TruthActionCommand.MagicType.Shadow, false, CriticalType.Random);
        }

        /// <summary>
        /// �f���H�[�����O�E�v���O�[�̃��\�b�h
        /// </summary>
        /// <param name="player"></param>
        /// <param name="mainCharacter"></param>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        private void PlayerSpellDevouringPlague(MainCharacter player, MainCharacter target, int interval, int magnification)
        {
            double damage = PrimaryLogic.DevouringPlagueValue(player, this.DuelMode);
            if (AbstractMagicDamage(player, target, interval, ref damage, magnification, "DevouringPlague.mp3", 29, TruthActionCommand.MagicType.Shadow, false, CriticalType.Random))
            {
                if (player.CurrentNourishSense > 0)
                {
                    damage = damage * 1.3f;
                }
                damage = GainIsZero(damage, player);
                player.CurrentLife += (int)damage;
                UpdateLife(player, damage, true, true, 0, false);
            }
        }

        /// <summary>
        /// �t�@�C�A�E�{�[���̃��\�b�h
        /// </summary>
        private void PlayerSpellFireBall(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            double damage = PrimaryLogic.FireBallValue(player, this.DuelMode);
            AbstractMagicDamage(player, target, interval, ref damage, magnification, "FireBall.mp3", 10, TruthActionCommand.MagicType.Fire, false, CriticalType.Random);
        }

        /// <summary>
        /// �A�C�X�E�j�[�h���̃��\�b�h
        /// </summary>
        private void PlayerSpellIceNeedle(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            double damage = PrimaryLogic.IceNeedleValue(player, this.DuelMode);
            AbstractMagicDamage(player, target, interval, ref damage, magnification, "IceNeedle.mp3", 30, TruthActionCommand.MagicType.Ice, false, CriticalType.Random);
        }

        /// <summary>
        /// �t���[�Y���E�����X�̃��\�b�h
        /// </summary>
        private void PlayerSpellFrozenLance(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            double damage = PrimaryLogic.FrozenLanceValue(player, this.DuelMode);
            AbstractMagicDamage(player, target, interval, ref damage, magnification, "FrozenLance.mp3", 31, TruthActionCommand.MagicType.Ice, false, CriticalType.Random);
        }

        /// <summary>
        /// ���[�h�E�I�u�E�p���[�̃��\�b�h
        /// </summary>
        private void PlayerSpellWordOfPower(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            double damage = PrimaryLogic.WordOfPowerValue(player, this.DuelMode);
            AbstractMagicDamage(player, target, interval, ref damage, magnification, "WordOfPower.mp3", 33, TruthActionCommand.MagicType.Force, true, CriticalType.Random);
        }

        /// <summary>
        /// �z�[���[�E�V���b�N�[�̃��\�b�h
        /// </summary>
        private void PlayerSpellHolyShock(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            double damage = PrimaryLogic.HolyShockValue(player, this.DuelMode);
            AbstractMagicDamage(player, target, interval, ref damage, magnification, "HolyShock.mp3", 23, TruthActionCommand.MagicType.Light, false, CriticalType.Random);
        }

        /// <summary>
        /// �t���C���E�X�g���C�N�̃��\�b�h
        /// </summary>
        private void PlayerSpellFlameStrike(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            double damage = PrimaryLogic.FlameStrikeValue(player, this.DuelMode);
            AbstractMagicDamage(player, target, interval, ref damage, magnification, "FlameStrike.mp3", 11, TruthActionCommand.MagicType.Fire, false, CriticalType.Random);
        }

        /// <summary>
        /// ���H���J�j�b�N�E�E�F�C���̃��\�b�h
        /// </summary>
        private void PlayerSpellVolcanicWave(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            double damage = PrimaryLogic.VolcanicWaveValue(player, this.DuelMode);
            AbstractMagicDamage(player, target, interval, ref damage, magnification, "VolcanicWave.mp3", 12, TruthActionCommand.MagicType.Fire, false, CriticalType.Random);
        }

        /// <summary>
        /// �z���C�g�E�A�E�g�̃��\�b�h
        /// </summary>
        private void PlayerSpellWhiteOut(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            double damage = PrimaryLogic.WhiteOutValue(player, this.DuelMode);
            AbstractMagicDamage(player, target, interval, ref damage, magnification, "WhiteOut.mp3", 34, TruthActionCommand.MagicType.Will, false, CriticalType.Random);
        }

        /// <summary>
        /// �t���b�V���E�u���C�Y�̃��\�b�h
        /// </summary>
        private void PlayerSpellFlashBlaze(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            double damage = PrimaryLogic.FlashBlazeValue(player, this.DuelMode);
            if (AbstractMagicDamage(player, target, interval, ref damage, magnification, "HolyShock.mp3", 120, TruthActionCommand.MagicType.Light, false, CriticalType.Random))
            {
                PlayerBuffAbstract(player, target, 999, "FlashBlaze_Buff.bmp");
            }
        }

        /// <summary>
        /// �Z���X�e�B�A���E�m���@�̃��\�b�h
        /// </summary>
        private void PlayerSpellCelestialNova(MainCharacter player, MainCharacter target)
        {
            if (DetectOpponentParty(player, target))
            {
                AbstractMagicDamage(player, target, 0, PrimaryLogic.CelestialNovaValue_A(player, this.DuelMode), 0, "CelestialNova.mp3", 26, TruthActionCommand.MagicType.Light, false, CriticalType.Random);
            }
            else
            {
                double lifeGain = PrimaryLogic.CelestialNovaValue_B(player, this.DuelMode);
                if (player.CurrentNourishSense > 0)
                {
                    lifeGain = lifeGain * 1.3f;
                }
                PlayerAbstractLifeGain(player, target, 0, lifeGain, 0, Database.SOUND_CELESTIAL_NOVA, 25);
            }
        }

        /// <summary>
        /// ���C�g�E�f�g�l�C�^�[�̃��\�b�h
        /// </summary>
        private void PlayerSpellLightDetonator(MainCharacter player, MainCharacter target, int p, int p_2)
        {
            List<MainCharacter> group = new List<MainCharacter>();
            if (player == mc || player == sc || player == tc)
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else if (player == ec1 || player == ec2 || player == ec3)
            {
                if (mc != null && !mc.Dead) { group.Add(mc); }
                if (sc != null && !sc.Dead) { group.Add(sc); }
                if (tc != null && !tc.Dead) { group.Add(tc); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                AbstractMagicDamage(player, group[ii], 0, PrimaryLogic.LightDetonatorValue(player, this.DuelMode), 0, "FlameStrike.mp3", 132, TruthActionCommand.MagicType.Light_Fire, false, CriticalType.Random);
            }
        }

        /// <summary>
        /// �[�[�^�E�G�N�X�v���[�W�����̃��\�b�h
        /// </summary>
        private void PlayerSpellZetaExplosion(MainCharacter player, MainCharacter target)
        {
            AbstractMagicDamage(player, target, 0, PrimaryLogic.ZetaExplosionValue(player, this.DuelMode), 0, Database.SOUND_ZETA_EXPLOSION, 139, TruthActionCommand.MagicType.Fire_Ice, true, CriticalType.Random);
        }

        /// <summary>
        /// �`���E�o�[���̃��\�b�h
        /// </summary>
        private void PlayerSpellChillBurn(MainCharacter player, MainCharacter target)
        {
            if (AbstractMagicDamage(player, target, 0, PrimaryLogic.ChillBurnValue(player, this.DuelMode), 0, "IceNeedle.mp3", 138, TruthActionCommand.MagicType.Fire_Ice, false, CriticalType.Random))
            {
                NowFrozen(player, target, 2);
            }
        }

        /// <summary>
        /// �s�A�b�V���O�E�t���C���̃��\�b�h
        /// </summary>
        private void PlayerSpellPiercingFlame(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(182));
            AbstractMagicDamage(player, target, 0, PrimaryLogic.PiercingFlameValue(player, this.DuelMode), 0, Database.SOUND_PIERCING_FLAME, 138, TruthActionCommand.MagicType.Fire_Force, true, CriticalType.Random);
        }

        /// <summary>
        /// �u���[�E�o���b�g�̃��\�b�h
        /// </summary>
        private void PlayerSpellBlueBullet(MainCharacter player, MainCharacter target, int interval, double magnification)
        {
            UpdateBattleText(player.GetCharacterSentence(151));
            for (int ii = 0; ii < 3; ii++)
            {
                AbstractMagicDamage(player, target, interval, PrimaryLogic.BlueBulletValue(player, this.DuelMode), magnification, "FrozenLance.mp3", 120, TruthActionCommand.MagicType.Shadow_Ice, false, CriticalType.Random);
            }
        }

        /// <summary>
        /// �A�Z���_���g�E���e�I�̃��\�b�h
        /// </summary>
        private void PlayerSpellAscendantMeteor(MainCharacter player, MainCharacter target, int p, int p_2)
        {
            UpdateBattleText(player.GetCharacterSentence(133));
            for (int ii = 0; ii < 10; ii++)
            {
                AbstractMagicDamage(player, target, 15, PrimaryLogic.AscendantMeteorValue(player, this.DuelMode), 0, "FireBall.mp3", 120, TruthActionCommand.MagicType.Light_Fire, false, CriticalType.Random);
            }
        }

        /// <summary>
        /// �X�^�[�E���C�g�j���O�̃��\�b�h
        /// </summary>
        private void PlayerSpellStarLightning(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(141));
            if (AbstractMagicDamage(player, target, 0, PrimaryLogic.StarLightningValue(player, this.DuelMode), 0, "FlameStrike.mp3", 120, TruthActionCommand.MagicType.Light_Will, false, CriticalType.Random))
            {
                PlayerBuffAbstract(player, target, 1, "StarLightning.bmp");
            }
        }

        /// <summary>
        /// �u���b�N�E�t�@�C�A�̃��\�b�h
        /// </summary>
        private void PlayerSpellBlackFire(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(143));
            if (AbstractMagicDamage(player, target, 0, PrimaryLogic.BlackFireValue(player, this.DuelMode), 0, "DarkBlast.mp3", 120, TruthActionCommand.MagicType.Shadow_Fire, false, CriticalType.Random))
            {
                PlayerBuffAbstract(player, target, 999, "BlackFire.bmp");
            }
        }

        /// <summary>
        /// ���[�h�E�I�u�E�}���X�̃��\�b�h
        /// </summary>
        private void PlayerSpellWordOfMalice(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(142));
            if (AbstractMagicDamage(player, target, 0, PrimaryLogic.WordOfMaliceValue(player, this.DuelMode), 0, "WhiteOut.mp3", 120, TruthActionCommand.MagicType.Shadow_Force, false, CriticalType.Random))
            {
                PlayerBuffAbstract(player, target, 999, "WordOfMalice.bmp");
            }
        }

        /// <summary>
        /// �G�����C�W�E�u���X�g�̃��\�b�h
        /// </summary>
        private void PlayerSpellEnrageBlast(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(144));
            List<MainCharacter> group = new List<MainCharacter>();
            if (player == mc || player == sc || player == tc)
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else if (player == ec1 || player == ec2 || player == ec3)
            {
                if (mc != null && !mc.Dead) { group.Add(mc); }
                if (sc != null && !sc.Dead) { group.Add(sc); }
                if (tc != null && !tc.Dead) { group.Add(tc); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                if (AbstractMagicDamage(player, group[ii], 0, PrimaryLogic.EnrageBlastValue(player, this.DuelMode), 0, "FlameStrike.mp3", 120, TruthActionCommand.MagicType.Fire_Force, false, CriticalType.Random))
                {
                    PlayerBuffAbstract(player, group[ii], 999, "EnrageBlast.bmp");
                }
            }
        }

        /// <summary>
        /// �C�����C�g�̃��\�b�h
        /// </summary>
        private void PlayerSpellImmolate(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(145));
            if (AbstractMagicDamage(player, target, 0, PrimaryLogic.ImmolateValue(player, this.DuelMode), 0, "FireBall.mp3", 120, TruthActionCommand.MagicType.Fire_Will, false, CriticalType.Random))
            {
                PlayerBuffAbstract(player, target, 999, "Immolate.bmp");
            }
        }

        /// <summary>
        /// ���@�j�b�V���E�E�F�C���̃��\�b�h
        /// </summary>
        private void PlayerSpellVanishWave(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(146));
            if (AbstractMagicDamage(player, target, 0, PrimaryLogic.VanishWaveValue(player, this.DuelMode), 0, "WhiteOut.mp3", 120, TruthActionCommand.MagicType.Ice_Will, false, CriticalType.Random))
            {
                //PlayerBuffAbstract(player, target, 999, "VanishWave.bmp");
                NowSilence(player, target, 3);
            }
        }

        /// <summary>
        /// �A�r�X�E�A�C�̃��\�b�h
        /// </summary>
        private void PlayerSpellAbyssEye(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(180));
            int random = AP.Math.RandomInteger(100);
            if (target.GetType() == typeof(TruthEnemyCharacter))
            {
                TruthEnemyCharacter tTarget = (TruthEnemyCharacter)target;
                if ((random <= 70) &&
                    (tTarget.Area != TruthEnemyCharacter.MonsterArea.Duel) &&
                    ((tTarget.Rare == TruthEnemyCharacter.RareString.Black) ||
                     (tTarget.Rare == TruthEnemyCharacter.RareString.Blue) ||
                     (tTarget.Rare == TruthEnemyCharacter.RareString.Red)
                    )
                   )
                {
                    GroundOne.PlaySoundEffect(Database.SOUND_ABYSS_EYE);
                    PlayerDeath(player, target);
                }
                else
                {
                    AbstractMagicDamage(player, target, 0, PrimaryLogic.AbyssEyeValue(player, this.DuelMode), 0, Database.SOUND_ABYSS_EYE, 120, TruthActionCommand.MagicType.Shadow_Force, false, CriticalType.Random);
                }
            }
            else
            {
                AbstractMagicDamage(player, target, 0, PrimaryLogic.AbyssEyeValue(player, this.DuelMode), 0, Database.SOUND_ABYSS_EYE, 120, TruthActionCommand.MagicType.Shadow_Force, false, CriticalType.Random);
            }
        }

        /// <summary>
        /// �h�D�[���E�u���C�h�̃��\�b�h
        /// </summary>
        private void PlayerSpellDoomBlade(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(181));
            if (AbstractMagicDamage(player, target, 0, PrimaryLogic.DoomBladeValue(player, this.DuelMode), 0, Database.SOUND_DOOM_BLADE, 120, TruthActionCommand.MagicType.Shadow_Will, false, CriticalType.Random))
            {
                double damage = PrimaryLogic.DoomBlade_A_Value(player, this.DuelMode);
                target.CurrentMana -= (int)damage;
                UpdateMana(target, damage, false, true, 0);
            }
        }

        /// <summary>
        /// �}�C���h�E�L�����O�̃��\�b�h
        /// </summary>
        private void PlayerSkillMindKilling(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(200));
            double effectValue = PrimaryLogic.MindKillingValue(player, this.DuelMode);
            target.CurrentMana -= (int)effectValue;
            UpdateMana(target, effectValue, false, true, 0);
        }

        // �O��Ώۂ�Ώ�
        private void PlayerSpellGenesis(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_GENESIS);
            UpdateBattleText(player.GetCharacterSentence(108));
            ExecBeforeAttackPhase(player, false);
        }

        private void ExecBeforeAttackPhase(MainCharacter player, bool skipStanceDouble)
        {
            if (player.BeforePA == PlayerAction.None)
            {
                PlayerNormalAttack(player, player.Target, 0.0f, false, false);
                return;
            }

            PlayerAction shadowPA = player.PA;
            string shadowItem = player.CurrentUsingItem;
            string shadowSpell = player.CurrentSpellName;
            string shadowSkill = player.CurrentSkillName;
            string shadowArche = player.CurrentArchetypeName;
            MainCharacter shadowTarget = player.Target;
            MainCharacter shadowTarget2 = player.Target2;
            PlayerAction shadowBeforePA = player.BeforePA;
            string shadowBeforeItem = player.BeforeUsingItem;
            string shadowBeforeSpell = player.BeforeSpellName;
            string shadowBeforeskill = player.BeforeSkillName;
            string shadowBeforeArche = player.BeforeArchetypeName;
            MainCharacter shadowBeforeTarget = player.BeforeTarget;
            MainCharacter shadowBeforeTarget2 = player.BeforeTarget2;

            player.PA = player.BeforePA;
            player.CurrentUsingItem = player.BeforeUsingItem;
            player.CurrentSkillName = player.BeforeSkillName;
            player.CurrentSpellName = player.BeforeSpellName;
            player.CurrentArchetypeName = player.BeforeArchetypeName;
            player.Target = player.BeforeTarget;
            player.Target2 = player.BeforeTarget2;

            PlayerAttackPhase(player, true, skipStanceDouble, false);

            player.PA = shadowPA;
            player.CurrentUsingItem = shadowItem;
            player.CurrentSkillName = shadowSkill;
            player.CurrentSpellName = shadowSpell;
            player.CurrentArchetypeName = shadowArche;
            player.Target = shadowTarget;
            player.Target2 = shadowTarget2;
            player.BeforePA = shadowBeforePA;
            player.BeforeUsingItem = shadowBeforeItem;
            player.BeforeSkillName = shadowBeforeskill;
            player.BeforeSpellName = shadowBeforeSpell;
            player.BeforeTarget = shadowBeforeTarget;
            player.BeforeTarget2 = shadowBeforeTarget2;
        }

        /// <summary>
        /// �f�[���j�b�N�E�C�O�i�C�g�̃��\�b�h
        /// </summary>
        private void PlayerSpellDemonicIgnite(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(213));
            if (AbstractMagicDamage(player, target, 0, PrimaryLogic.DemonicIgniteValue(player, this.DuelMode), 0, Database.SOUND_DEMONIC_IGNITE, 120, TruthActionCommand.MagicType.Shadow_Fire, false, CriticalType.Random))
            {
                NowNoGainLife(target, 1);
            }
        }
        
        /// <summary>
        /// �G���h���X�E�A���Z���̃��\�b�h
        /// </summary>
        private void PlayerSpellEndlessAnthem(MainCharacter player, MainCharacter target)
        {
            List<MainCharacter> group = new List<MainCharacter>();
            if (IsPlayerAlly(player))
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else
            {
                if (mc != null && !mc.Dead) { group.Add(mc); }
                if (sc != null && !sc.Dead) { group.Add(sc); }
                if (tc != null && !tc.Dead) { group.Add(tc); }
            }
            for (int ii = 0; ii < group.Count; ii++)
            {
                group[ii].BuffCountUp();
            }
            GroundOne.PlaySoundEffect(Database.SOUND_ENDLESS_ANTHEM);
        }

        /// <summary>
        /// ���[�v�E�Q�[�g�̃��\�b�h
        /// </summary>
        private void PlayerSpellWarpGate(MainCharacter player, MainCharacter target)
        {
            // �Q�[�W��i�߂�B�i�߂����ʁA�s���t�F�[�Y�𒴂����ꍇ�A�R�X�g�����ōs�����s���A����������������ɃQ�[�W��i�߂�B
            GroundOne.PlaySoundEffect(Database.SOUND_WARP_GATE);
            for (int ii = 0; ii < (int)PrimaryLogic.WarpGateValue(player); ii++)
            {
                player.BattleBarPos++;
                if (player.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH)
                {
                    PlayerAttackPhase(player, true, false, false);
                    player.BattleBarPos = 0;
                }
                pbPlayer1.Invalidate();
                this.Update();
                System.Threading.Thread.Sleep(1);
            }
        }


        // �����Ώ�
        /// <summary>
        /// �񕜃R�}���h�̒��ۉ�
        /// </summary>
        /// <param name="player">�Ώی�</param>
        /// <param name="target">�Ώۑ���</param>
        /// <param name="interval">������̃C���^�[�o��</param>
        /// <param name="damage">�_���[�W</param>
        /// <param name="magnification">�����{���A�O�̏ꍇ�͑������Ȃ�</param>
        /// <param name="soundName">���ʉ��t�@�C����</param>
        /// <param name="messageNumber">���@�_���[�W���b�Z�[�W</param>
        private void PlayerAbstractLifeGain(MainCharacter player, MainCharacter target, int interval, double effectValue, double magnification, string soundName, int messageNumber)
        {
            if (target != null)
            {
                if ((target != ec1) ||
                     (player == ec1 && target == ec1))
                {
                    if (target.Dead)
                    {
                        UpdateBattleText("������" + target.Name + "�͎���ł��܂��Ă��邽�ߌ��ʂ����������I\r\n");
                    }
                    else if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(119));
                    }
                    else
                    {
                        if (soundName != String.Empty)
                        {
                            GroundOne.PlaySoundEffect(soundName);
                        }
                        effectValue = GainIsZero(effectValue, target);
                        target.CurrentLife += (int)effectValue;
                        UpdateLife(target, effectValue, true, true, 0, false);
                        if (messageNumber == 0)
                        {
                            UpdateBattleText(target.Name + "��" + ((int)effectValue).ToString() + "�񕜂����B\r\n");
                        }
                        else
                        {
                            UpdateBattleText(String.Format(player.GetCharacterSentence(messageNumber), ((int)effectValue).ToString()));
                        }
                    }
                }
                else
                {
                    UpdateBattleText(player.GetCharacterSentence(53));
                }
            }
            else
            {
                if (player.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(player.GetCharacterSentence(119));
                }
                else
                {
                    if (soundName != String.Empty)
                    {
                        GroundOne.PlaySoundEffect(soundName);
                    }
                    effectValue = GainIsZero(effectValue, player);
                    player.CurrentLife += (int)effectValue;
                    UpdateLife(player, effectValue, true, true, 0, false);
                    if (messageNumber == 0)
                    {
                        UpdateBattleText(player.Name + "��" + ((int)effectValue).ToString() + "�񕜂����B\r\n");
                    }
                    else
                    {
                        UpdateBattleText(String.Format(player.GetCharacterSentence(messageNumber), effectValue.ToString()));
                    }
                }
            }  
        }

        /// <summary>
        /// �}�i�񕜃R�}���h�̒��ۉ�
        /// </summary>
        /// <param name="player">�Ώی�</param>
        /// <param name="target">�Ώۑ���</param>
        /// <param name="interval">������̃C���^�[�o��</param>
        /// <param name="damage">�_���[�W</param>
        /// <param name="magnification">�����{���A�O�̏ꍇ�͑������Ȃ�</param>
        /// <param name="soundName">���ʉ��t�@�C����</param>
        /// <param name="messageNumber">���@�_���[�W���b�Z�[�W</param>
        private void PlayerAbstractManaGain(MainCharacter player, MainCharacter target, int interval, double effectValue, double magnification, string soundName, int messageNumber)
        {
            if (target != null)
            {
                if ((target != ec1) ||
                     (player == ec1 && target == ec1))
                {
                    if (target.Dead)
                    {
                        UpdateBattleText("������" + target.Name + "�͎���ł��܂��Ă��邽�ߌ��ʂ����������I\r\n");
                    }
                    else if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(121));
                    }
                    else
                    {
                        if (soundName != String.Empty)
                        {
                            GroundOne.PlaySoundEffect(soundName);
                        }
                        target.CurrentMana += (int)effectValue;
                        UpdateMana(target, effectValue, true, true, 0);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(messageNumber), ((int)effectValue).ToString()));
                    }
                }
                else
                {
                    UpdateBattleText(player.GetCharacterSentence(53));
                }
            }
            else
            {
                if (player.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(player.GetCharacterSentence(121));
                }
                else
                {
                    if (soundName != String.Empty)
                    {
                        GroundOne.PlaySoundEffect(soundName);
                    }
                    player.CurrentMana += (int)effectValue;
                    UpdateMana(player, effectValue, true, true, 0);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(messageNumber), effectValue.ToString()));
                }
            }
        }

        /// <summary>
        /// �X�L���񕜃R�}���h�̒��ۉ�
        /// </summary>
        /// <param name="player">�Ώی�</param>
        /// <param name="target">�Ώۑ���</param>
        /// <param name="interval">������̃C���^�[�o��</param>
        /// <param name="damage">�_���[�W</param>
        /// <param name="magnification">�����{���A�O�̏ꍇ�͑������Ȃ�</param>
        /// <param name="soundName">���ʉ��t�@�C����</param>
        /// <param name="messageNumber">���@�_���[�W���b�Z�[�W</param>
        private void PlayerAbstractSkillGain(MainCharacter player, MainCharacter target, int interval, double effectValue, double magnification, string soundName, int messageNumber)
        {
            if (target != null)
            {
                if ((target != ec1) ||
                     (player == ec1 && target == ec1))
                {
                    if (target.Dead)
                    {
                        UpdateBattleText("������" + target.Name + "�͎���ł��܂��Ă��邽�ߌ��ʂ����������I\r\n");
                    }
                    else if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(121));
                    }
                    else
                    {
                        if (soundName != String.Empty)
                        {
                            GroundOne.PlaySoundEffect(soundName);
                        }
                        target.CurrentSkillPoint += (int)effectValue;
                        UpdateSkillPoint(target, effectValue, true, true, 0);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(messageNumber), ((int)effectValue).ToString()));
                    }
                }
                else
                {
                    UpdateBattleText(player.GetCharacterSentence(53));
                }
            }
            else
            {
                if (player.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(player.GetCharacterSentence(121));
                }
                else
                {
                    if (soundName != String.Empty)
                    {
                        GroundOne.PlaySoundEffect(soundName);
                    }
                    player.CurrentSkillPoint += (int)effectValue;
                    UpdateSkillPoint(player, effectValue, true, true, 0);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(messageNumber), effectValue.ToString()));
                }
            }
        }

        /// <summary>
        /// ���j�F�W���ƒf��̃��\�b�h
        /// </summary>
        private void PlayerArchetypeSyutyuDanzetsu(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_SYUTYU_DANZETSU);
            PlayerBuffAbstract(player, player, 999, "SYUTYU-DANZETSU.bmp");
            player.AlreadyPlayArchetype = true;
        }
        /// <summary>
        /// ���j�F�z�Ɛ���̃��\�b�h
        /// </summary>
        private void PlayerArchetypeJunkanSeiyaku(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_JUNKAN_SEIYAKU);
            List<MainCharacter> group = new List<MainCharacter>();
            if (IsPlayerEnemy(player))
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else
            {
                if (mc != null && !mc.Dead) { group.Add(mc); }
                if (sc != null && !sc.Dead) { group.Add(sc); }
                if (tc != null && !tc.Dead) { group.Add(tc); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                PlayerBuffAbstract(player, group[ii], (int)PrimaryLogic.JunkanSeiyakuValue(player), "JUNKAN-SEIYAKU.bmp");
            }
            player.AlreadyPlayArchetype = true;
        }

        private void PlayerArchetypeOraOraOraaa(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_ORA_ORA_ORAAA);
            int num = (int)PrimaryLogic.OraOraOraaaValue(player);
            List<MainCharacter> group = new List<MainCharacter>();
            if (IsPlayerAlly(player))
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else
            {
                if (mc != null && !mc.Dead) { group.Add(mc); }
                if (sc != null && !sc.Dead) { group.Add(sc); }
                if (tc != null && !tc.Dead) { group.Add(tc); }
            }           
            for (int ii = 0; ii < num; ii++)
            {
                string soundName = "Hit01.mp3"; if (ii == num-1) soundName = "KineticSmash.mp3";
                int interval = 7; if (ii == num - 1) { interval = 60; }

                PlayerNormalAttack(player, group[AP.Math.RandomInteger(group.Count)], 0, 0, false, false, 0, interval, soundName, 115, false, CriticalType.Random);
            }
        }

        /// <summary>
        /// �t���b�V���E�q�[���̃��\�b�h
        /// </summary>
        /// <param name="player"></param>
        private void PlayerSpellFreshHeal(MainCharacter player, MainCharacter target)
        {
            double lifeGain = PrimaryLogic.FreshHealValue(player, this.DuelMode);
            if (player.CurrentNourishSense > 0)
            {
                lifeGain = lifeGain * 1.3f;
            }
            PlayerAbstractLifeGain(player, player, 0, lifeGain, 0, Database.SOUND_FRESH_HEAL, 9);
        }

        /// <summary>
        /// ���C�t�E�^�b�v�̃��\�b�h
        /// </summary>
        /// <param name="player"></param>
        private void PlayerSpellLifeTap(MainCharacter player, MainCharacter target)
        {
            double lifeGain = PrimaryLogic.LifeTapValue(player, this.DuelMode);
            if (player.CurrentNourishSense > 0)
            {
                lifeGain = lifeGain * 1.3f;
            }
            PlayerAbstractLifeGain(player, target, 0, lifeGain, 0, Database.SOUND_LIFE_TAP, 9);
        }

        /// <summary>
        /// �T�[�N���b�h�E�q�[���̃��\�b�h
        /// </summary>
        private void PlayerSpellSacredHeal(MainCharacter player, MainCharacter target)
        {
            List<MainCharacter> group = new List<MainCharacter>();
            if (IsPlayerEnemy(player))
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else
            {
                if (mc != null && !mc.Dead) { group.Add(mc); }
                if (sc != null && !sc.Dead) { group.Add(sc); }
                if (tc != null && !tc.Dead) { group.Add(tc); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                PlayerAbstractLifeGain(player, group[ii], 0, PrimaryLogic.SacredHealValue(player, this.DuelMode), 0, "CelestialNova.mp3", 135);
            }
        }

        /// <summary>
        /// �N���[���W���O�̃��\�b�h
        /// </summary>
        /// <param name="player"></param>
        private void PlayerSpellCleansing(MainCharacter player, MainCharacter target)
        {
            if (player.CurrentStunning > 0 || player.CurrentSilence > 0 || player.CurrentPoison > 0 || player.CurrentParalyze > 0 || player.CurrentTemptation > 0 || player.CurrentFrozen > 0)
            {
                UpdateBattleText(player.GetCharacterSentence(109));
                return;
            }

            if (target != ec1 || target != ec2 || target != ec3)
            {
                UpdateBattleText(player.GetCharacterSentence(77));
                target.CurrentPreStunning = 0;
                target.DeBuff(target.pbPreStunning);
                target.CurrentStunning = 0;
                target.DeBuff(target.pbStun);
                target.CurrentSilence = 0;
                target.DeBuff(target.pbSilence);
                target.CurrentPoison = 0;
                target.CurrentPoisonValue = 0;
                target.DeBuff(target.pbPoison);
                target.CurrentTemptation = 0;
                target.DeBuff(target.pbTemptation);
                target.CurrentFrozen = 0;
                target.DeBuff(target.pbFrozen);
                target.CurrentParalyze = 0;
                target.DeBuff(target.pbParalyze);
                target.CurrentSlow = 0;
                target.DeBuff(target.pbSlow);
                target.CurrentBlind = 0;
                target.DeBuff(target.pbBlind);
                target.CurrentSlip = 0;
                target.DeBuff(target.pbSlip);
                //target.CurrentNoResurrection = 0; // �����s�͕��̉e���Ƃ�����`�ɂ͓��Ă͂܂�Ȃ��B
                //target.DeBuff(target.pbNoResurrection);
                //target.CurrentNoGainLife = 0; // ���C�t�񕜕s�͕��̉e���Ƃ�����`�ɂ͓��Ă͂܂�Ȃ��B
                //target.DeBuff(target.pbNoGainLife);
                GroundOne.PlaySoundEffect("Cleansing.mp3");
                UpdateBattleText(target.Name + "�ɂ������Ă��镉�̉e�����S�Ď�蕥��ꂽ�B\r\n");
            }
            else
            {
                UpdateBattleText(player.GetCharacterSentence(53));
            }
        }

        /// <summary>
        /// �G���W�F���E�u���X�̃��\�b�h
        /// </summary>
        private void PlayerSpellAngelBreath(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(177));
            List<MainCharacter> group = new List<MainCharacter>();
            if (IsPlayerEnemy(player))
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else
            {
                if (mc != null && !mc.Dead) { group.Add(mc); }
                if (sc != null && !sc.Dead) { group.Add(sc); }
                if (tc != null && !tc.Dead) { group.Add(tc); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                group[ii].CurrentPreStunning = 0;
                group[ii].DeBuff(group[ii].pbPreStunning);
                group[ii].CurrentStunning = 0;
                group[ii].DeBuff(group[ii].pbStun);
                group[ii].CurrentSilence = 0;
                group[ii].DeBuff(group[ii].pbSilence);
                group[ii].CurrentPoison = 0;
                group[ii].CurrentPoisonValue = 0;
                group[ii].DeBuff(group[ii].pbPoison);
                group[ii].CurrentTemptation = 0;
                group[ii].DeBuff(group[ii].pbTemptation);
                group[ii].CurrentFrozen = 0;
                group[ii].DeBuff(group[ii].pbFrozen);
                group[ii].CurrentParalyze = 0;
                group[ii].DeBuff(group[ii].pbParalyze);
                group[ii].CurrentSlow = 0;
                group[ii].DeBuff(group[ii].pbSlow);
                group[ii].CurrentBlind = 0;
                group[ii].DeBuff(group[ii].pbBlind);
                group[ii].CurrentSlip = 0;
                group[ii].DeBuff(group[ii].pbSlip);
                //group[ii].CurrentNoResurrection = 0; // �����s�͕��̉e���Ƃ�����`�ɂ͓��Ă͂܂�Ȃ��B
                //group[ii].DeBuff(group[ii].pbNoResurrection);
                GroundOne.PlaySoundEffect("Cleansing.mp3");
                UpdateBattleText(group[ii].Name + "�ɂ������Ă��镉�̉e�����S�Ď�蕥��ꂽ�B\r\n");
            }
        }

        /// <summary>
        /// �s���A�E�v���t�@�C�P�[�V�����̃��\�b�h
        /// </summary>
        /// <param name="player"></param>
        private void PlayerSkillPurePurification(MainCharacter player)
        {
            UpdateBattleText(player.GetCharacterSentence(78));
            player.CurrentPreStunning = 0;
            player.DeBuff(player.pbPreStunning);
            player.CurrentStunning = 0;
            player.DeBuff(player.pbStun);
            player.CurrentSilence = 0;
            player.DeBuff(player.pbSilence);
            player.CurrentPoison = 0;
            player.CurrentPoisonValue = 0;
            player.DeBuff(player.pbPoison);
            player.CurrentTemptation = 0;
            player.DeBuff(player.pbTemptation);
            player.CurrentFrozen = 0;
            player.DeBuff(player.pbFrozen);
            player.CurrentParalyze = 0;
            player.DeBuff(player.pbParalyze);
            player.CurrentSlow = 0;
            player.DeBuff(player.pbSlow);
            player.CurrentBlind = 0;
            player.DeBuff(player.pbBlind);
            player.CurrentSlip = 0;
            player.DeBuff(player.pbSlip);
            //player.CurrentNoResurrection = 0; // �����s�͕��̉e���Ƃ�����`�ɂ͓��Ă͂܂�Ȃ��B
            //player.DeBuff(player.pbNoResurrection);
            //player.CurrentNoGainLife = 0; // �����s�͕��̉e���Ƃ�����`�ɂ͓��Ă͂܂�Ȃ��B
            //player.DeBuff(player.pbNoGainLife);
            GroundOne.PlaySoundEffect("Cleansing.mp3");
            UpdateBattleText(player.Name + "�ɂ������Ă��镉�̉e�����S�Ď�蕥��ꂽ�B\r\n");
        }

        /// <summary>
        /// �f�B�X�y���E�}�W�b�N�̃��\�b�h
        /// </summary>
        private void PlayerSpellDispelMagic(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(48), 1000);

            if (target.CurrentNothingOfNothingness > 0)
            {
                UpdateBattleText("�������A" + target.Name + "�͖������𖳌��ɂ���I�[���ɂ���Č���Ă���I\r\n");
                return;
            }

            target.RemoveBuffSpell();
            GroundOne.PlaySoundEffect("DispelMagic.mp3");
            UpdateBattleText(target.Name + "�̔\�͂t�o�^���ʂ�S�đł��������I\r\n");
        }

        private void RemoveBuffAll(MainCharacter target)
        {
            RemoveBuffSpell(target);
            RemoveBuffSkill(target);
            RemoveEffect(target);
        }

        private void RemoveEffect(MainCharacter target)
        {
            target.RemoveDebuffEffect();
            target.RemoveBuffEffect();

            target.RemoveBuffParam();
            target.RemoveDebuffParam();

            target.RemoveStrengthUp();
            target.RemoveAgilityUp();
            target.RemoveIntelligenceUp();
            target.RemoveStaminaUp();
            target.RemoveMindUp();

            target.RemoveResistLightUp();
            target.RemoveResistShadowUp();
            target.RemoveResistFireUp();
            target.RemoveResistIceUp();
            target.RemoveResistForceUp();
            target.RemoveResistWillUp();
        }

        private void RemoveBuffSpell(MainCharacter target)
        {
            target.RemoveBuffSpell();
            RemoveTemporaryUpSpell(target);
            RemoveBuffDownSpell(target);
        }

        private void RemoveBuffSkill(MainCharacter target)
        {
            RemoveBuffUpSkill(target);
            RemoveTemporaryUpSkill(target);
            RemoveBuffDownSkill(target);           
        }

        private void RemoveTemporaryUpSpell(MainCharacter target)
        {
            // ��{�X�y��
            target.RemoveGlory();
            target.RemoveBlackContract();
            target.RemoveImmortalRave();
            target.RemoveMirrorImage();
            target.RemoveGaleWind();
            target.RemoveWordOfFortune();
            target.RemoveAetherDrive();
            target.RemoveDeflection();
            target.RemoveOneImmunity();
            target.RemoveTimeStop();
            // �����X�y��
            //target.RemoveTranscendentWish();
            target.RemoveHymnContract();
            target.RemoveEndlessAnthem();
            target.RemoveSinFortune();
            //target.RemoveEclipseEnd();
        }
        private void RemoveBuffDownSpell(MainCharacter target)
        {
            target.RemoveDebuffSpell();
        }
        private void RemoveBuffUpSkill(MainCharacter target)
        {
            target.RemoveBuffSkill();
        }
        private void RemoveTemporaryUpSkill(MainCharacter target)
        {
            // ��{�X�L��
            target.RemoveStanceOfFlow();
            target.RemoveHighEmotionality();
            // �����X�L��
            target.RemoveStanceOfDouble();
            target.RemoveSwiftStep();
            target.RemoveVigorSense();
            target.RemoveSmoothingMove();
            target.RemoveFutureVision();
            target.RemoveOneAuthority();
        }
        private void RemoveBuffDownSkill(MainCharacter target)
        {
            target.RemoveDebuffSkill();
        }

        private void PlayerSpellTranquility(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(84));

            if (target.CurrentNothingOfNothingness > 0)
            {
                UpdateBattleText("�������A" + target.Name + "�͖������𖳌��ɂ���I�[���ɂ���Č���Ă���I\r\n");
                return;
            }

            target.RemoveGlory();
            // GaleWind��Fortune��Dispel�s�\�Ȃ͎̂��H�㋭���������߁ADispel�ΏۂƂ���B
            target.RemoveGaleWind();
            target.RemoveWordOfFortune();
            target.RemoveBlackContract();
            target.RemoveHymnContract();
            target.RemoveImmortalRave();
            //target.RemoveAbsoluteZero(); // Tranquility�͕��̉e�����ʂ���������X�y���ł͂Ȃ��B
            target.RemoveAetherDrive();
            target.RemoveOneImmunity();
            target.RemoveHighEmotionality();
            target.RemoveStanceOfFlow();
            // �����X�L��
            target.RemoveStanceOfDouble();
            target.RemoveSwiftStep();
            target.RemoveVigorSense();
            target.RemoveSmoothingMove();
            target.RemoveFutureVision();
            target.RemoveOneAuthority();

            GroundOne.PlaySoundEffect("Tranquility.mp3");
            UpdateBattleText(target.Name + "�̈�莞�ԕt�^���ʂ�S�đł��������I\r\n");
        }

        /// <summary>
        /// ���[�h�E�I�u�E�A�e�B�`���[�h�̃��\�b�h
        /// </summary>
        private void PlayerSpellWordOfAttitude(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(147));
            target.CurrentInstantPoint = target.MaxInstantPoint;
            UpdateInstantPoint(target);
        }

        // [���]�F�S�Ă̂a�t�e�e�Q�t�o���@�́A�����֏W�񂳂��悤�ɂ��Ă��������B
        // [�x��]�F�����ɏW�񂳂�Ă�����͖����v���C���[�݂̂�ΏۂƂ��Ă��܂��B�G������ʖ���������悤�ɂ��Ă��������B
        private void PlayerBuffAbstract(MainCharacter player, MainCharacter target, int effectTime, string spellName)
        {
            string fileExt = ".bmp";
            if (target != null)
            {
                int effectValue = 0;
                if (target.CurrentAusterityMatrix > 0 || target.CurrentAusterityMatrixOmega > 0)
                {
                    string spellNameWithoutExt = spellName.Substring(0, spellName.Length - 4);
                    if (TruthActionCommand.GetBuffType(spellNameWithoutExt) == TruthActionCommand.BuffType.Up)
                    {
                        UpdateBattleText(target.Name + "��AusterityMatrix�Ɏx�z����Ă���ABUFF��t�^�ł��Ȃ������I�I\r\n");
                        this.Invoke(new _AnimationDamage(AnimationDamage), 0, target, 0, Color.Black, true, false, Database.EFFECT_CANNOT_BUFF);
                        return;
                    }
                }

                switch (spellName)
                {
                    case "Damnation.bmp":
                        target.CurrentDamnation = effectTime;
                        target.ActivateBuff(target.pbDamnation, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(37));
                        break;
                    case "AbsoluteZero.bmp":
                        target.CurrentAbsoluteZero = effectTime;
                        target.ActivateBuff(target.pbAbsoluteZero, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(81));
                        break;
                    case "StanceOfFlow.bmp":
                        target.CurrentStanceOfFlow = effectTime;
                        target.ActivateBuff(target.pbStanceOfFlow, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(64));
                        break;
                    case "OneImmunity.bmp":
                        target.CurrentOneImmunity = effectTime;
                        target.ActivateBuff(target.pbOneImmunity, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(46));
                        break;
                    case "GaleWind.bmp":
                        target.CurrentGaleWind = effectTime;
                        target.ActivateBuff(target.pbGaleWind, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(40));
                        break;
                    case "AetherDrive.bmp":
                        target.CurrentAetherDrive = effectTime;
                        target.ActivateBuff(target.pbAetherDrive, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(43));
                        break;
                    case "ImmortalRave.bmp":
                        target.CurrentImmortalRave = effectTime;
                        target.ActivateBuff(target.pbImmortalRave, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(39));
                        break;
                    case "BlackContract.bmp":
                        target.CurrentBlackContract = effectTime;
                        target.ActivateBuff(target.pbBlackContract, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(35));
                        break;
                    case "Glory.bmp":
                        target.CurrentGlory = effectTime;
                        target.ActivateBuff(target.pbGlory, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(24));
                        break;
                    case "Protection.bmp":
                        target.CurrentProtection = effectTime;
                        target.ActivateBuff(target.pbProtection, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(18));
                        break;
                    case "AbsorbWater.bmp":
                        target.CurrentAbsorbWater = effectTime;
                        target.ActivateBuff(target.pbAbsorbWater, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(19));
                        break;
                    case "SaintPower.bmp":
                        target.CurrentSaintPower = effectTime;
                        target.ActivateBuff(target.pbSaintPower, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(20));
                        break;
                    case "ShadowPact.bmp":
                        target.CurrentShadowPact = effectTime;
                        target.ActivateBuff(target.pbShadowPact, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(21));
                        break;
                    case "BloodyVengeance.bmp":
                        effectValue = player.StandardIntelligence / 2;
                        if ((effectValue - target.BuffStrength_BloodyVengeance) > 0)
                        {
                            target.CurrentBloodyVengeance = effectTime;
                            target.ActivateBuff(target.pbBloodyVengeance, Database.BaseResourceFolder + spellName, effectTime);
                            UpdateBattleText(String.Format(player.GetCharacterSentence(22), Convert.ToString(effectValue - target.BuffStrength_BloodyVengeance)));
                            target.BuffStrength_BloodyVengeance += effectValue;
                        }
                        else
                        {
                            UpdateBattleText(player.GetCharacterSentence(82));
                        }
                        break;

                    case "HeatBoost.bmp":
                        effectValue = player.StandardIntelligence / 2;
                        if ((effectValue - target.BuffAgility_HeatBoost) > 0)
                        {
                            target.CurrentHeatBoost = effectTime;
                            target.ActivateBuff(target.pbHeatBoost, Database.BaseResourceFolder + spellName, effectTime);
                            UpdateBattleText(String.Format(player.GetCharacterSentence(38), Convert.ToString(effectValue - target.BuffAgility_HeatBoost)));
                            target.BuffAgility_HeatBoost += effectValue;
                        }
                        else
                        {
                            UpdateBattleText(player.GetCharacterSentence(82));
                        }
                        break;

                    case "FlameAura.bmp":
                        target.CurrentFlameAura = effectTime;
                        target.ActivateBuff(target.pbFlameAura, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(36));
                        break;

                    case "PromisedKnowledge.bmp":
                        effectValue = player.StandardIntelligence / 2;
                        if ((effectValue - target.BuffIntelligence_PromisedKnowledge) > 0)
                        {
                            target.CurrentPromisedKnowledge = effectTime;
                            target.ActivateBuff(target.pbPromisedKnowledge, Database.BaseResourceFolder + spellName, effectTime);
                            UpdateBattleText(String.Format(player.GetCharacterSentence(83), Convert.ToString(effectValue - target.BuffIntelligence_PromisedKnowledge)));
                            target.BuffIntelligence_PromisedKnowledge += effectValue;
                        }
                        else
                        {
                            UpdateBattleText(player.GetCharacterSentence(82));
                        }
                        break;

                    case "RiseOfImage.bmp":
                        effectValue = player.StandardIntelligence / 2;
                        if ((effectValue - target.BuffMind_RiseOfImage) > 0)
                        {
                            target.CurrentRiseOfImage = effectTime;
                            target.ActivateBuff(target.pbRiseOfImage, Database.BaseResourceFolder + spellName, effectTime);
                            UpdateBattleText(String.Format(player.GetCharacterSentence(49), Convert.ToString(effectValue - target.BuffMind_RiseOfImage)));
                            target.BuffMind_RiseOfImage += effectValue;
                        }
                        else
                        {
                            UpdateBattleText(player.GetCharacterSentence(82));
                        }
                        break;

                    case "WordOfLife.bmp":
                        target.CurrentWordOfLife = effectTime;
                        target.ActivateBuff(target.pbWordOfLife, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(41));
                        break;

                    case "WordOfFortune.bmp":
                        target.CurrentWordOfFortune = effectTime;
                        target.ActivateBuff(target.pbWordOfFortune, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(42));
                        break;

                    case "EternalPresence.bmp":
                        target.CurrentEternalPresence = effectTime;
                        target.ActivateBuff(target.pbEternalPresence, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(44), 1000);
                        UpdateBattleText(player.GetCharacterSentence(45));
                        break;

                    case "MirrorImage.bmp":
                        target.CurrentMirrorImage = effectTime;
                        target.ActivateBuff(target.pbMirrorImage, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(57), player.Target.Name));
                        break;

                    case "Deflection.bmp":
                        target.CurrentDeflection = effectTime;
                        target.ActivateBuff(target.pbDeflection, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(60), target.Name));
                        break;

                    case "PsychicTrance.bmp":
                        target.CurrentPsychicTrance = effectTime;
                        target.ActivateBuff(target.pbPsychicTrance, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(129), target.Name));
                        break;
                    case "BlindJustice.bmp":
                        target.CurrentBlindJustice = effectTime;
                        target.ActivateBuff(target.pbBlindJustice, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(130), target.Name));
                        break;
                    case "TranscendentWish.bmp":
                        if (target.CurrentTranscendentWish <= 0)
                        {
                            target.CurrentTranscendentWish = effectTime;
                            target.ActivateBuff(target.pbTranscendentWish, Database.BaseResourceFolder + spellName, effectTime);
                            UpdateBattleText(String.Format(player.GetCharacterSentence(131), target.Name));

                            target.BuffStrength_TranscendentWish = (int)(PrimaryLogic.TranscendentWishValue(target, PrimaryLogic.ParameterType.Strength));
                            target.BuffAgility_TranscendentWish = (int)(PrimaryLogic.TranscendentWishValue(target, PrimaryLogic.ParameterType.Agility));
                            target.BuffIntelligence_TranscendentWish = (int)(PrimaryLogic.TranscendentWishValue(target, PrimaryLogic.ParameterType.Intelligence));
                            target.BuffStamina_TranscendentWish = (int)(PrimaryLogic.TranscendentWishValue(target, PrimaryLogic.ParameterType.Stamina));
                            target.BuffMind_TranscendentWish = (int)(PrimaryLogic.TranscendentWishValue(target, PrimaryLogic.ParameterType.Mind));
                        }
                        else
                        {
                            UpdateBattleText(player.GetCharacterSentence(82));
                        }
                        break;
                    case "SkyShield.bmp":
                        target.CurrentSkyShield = effectTime;
                        target.CurrentSkyShieldValue++;
                        target.ChangeSkyShieldStatus(target.CurrentSkyShieldValue);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(134), target.Name));
                        break;
                    case "StaticBarrier.bmp":
                        target.CurrentStaticBarrier = effectTime;
                        target.CurrentStaticBarrierValue++;
                        target.ChangeStaticBarrierStatus(target.CurrentStaticBarrierValue);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(186), target.Name));
                        break;
                    case "EverDroplet.bmp":
                        target.CurrentEverDroplet = effectTime;
                        target.ActivateBuff(target.pbEverDroplet, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(136), target.Name));
                        break;
                    case "FrozenAura.bmp":
                        target.CurrentFrozenAura = effectTime;
                        target.ActivateBuff(target.pbFrozenAura, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(137), target.Name));
                        break;

                    //case "Damnation.bmp":
                    //    player.Target.CurrentDamnation = effectTime;
                    //    player.Target.pbDamnation.Image = Image.FromFile(Database.BaseResourceFolder + spellName);
                    //    player.Target.pbDamnation.Update();
                    //    UpdateBattleText(player.GetCharacterSentence(37));
                    //    break;

                    case "PainfulInsanity.bmp":
                        player.CurrentPainfulInsanity = effectTime;
                        player.ActivateBuff(player.pbPainfulInsanity, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(4));
                        break;

                    case "FlashBlaze_Buff.bmp":
                        target.CurrentFlashBlazeCount = effectTime;
                        target.ActivateBuff(target.pbFlashBlaze, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "StarLightning.bmp":
                        if (target.CurrentStarLightning <= 0) // �X�^�����ʂȂ̂ŁA�ݐς����Ȃ��B
                        {
                            target.CurrentStarLightning = effectTime;
                        }
                        target.ActivateBuff(target.pbStarLightning, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(target.Name + "�͋C�₵���B\r\n");
                        break;
                    case "WordOfMalice.bmp":
                        target.CurrentWordOfMalice = effectTime;
                        target.ActivateBuff(target.pbWordOfMalice, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "SinFortune.bmp":
                        target.CurrentSinFortune = effectTime;
                        target.ActivateBuff(target.pbSinFortune, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(211), target.Name));
                        break;
                    case "BlackFire.bmp":
                        target.CurrentBlackFire = effectTime;
                        target.ActivateBuff(target.pbBlackFire, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "EnrageBlast.bmp":
                        target.CurrentEnrageBlast = effectTime;
                        target.ActivateBuff(target.pbEnrageBlast, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "SigilOfHomura.bmp":
                        target.CurrentSigilOfHomura = effectTime;
                        target.ActivateBuff(target.pbSigilOfHomura, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(206)));
                        break;
                    case "Immolate.bmp":
                        target.CurrentImmolate = effectTime;
                        target.ActivateBuff(target.pbImmolate, Database.BaseResourceFolder + spellName, effectTime);
                        break;

                    case "HolyBreaker.bmp":
                        target.CurrentHolyBreaker = effectTime;
                        target.ActivateBuff(target.pbHolyBreaker, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(148));
                        break;
                    case "HymnContract.bmp":
                        target.CurrentHymnContract = effectTime;
                        target.ActivateBuff(target.pbHymnContract, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "DarkenField.bmp":
                        target.CurrentDarkenField = effectTime;
                        target.ActivateBuff(target.pbDarkenField, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "EclipseEnd.bmp":
                        target.CurrentEclipseEnd = effectTime;
                        target.ActivateBuff(target.pbEclipseEnd, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "BlazingField.bmp":
                        target.CurrentBlazingField = effectTime;
                        target.CurrentBlazingFieldFactor = player.TotalIntelligence;
                        target.ActivateBuff(target.pbBlazingField, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "ExaltedField.bmp":
                        target.CurrentExaltedField = effectTime;
                        target.ActivateBuff(target.pbExaltedField, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "ONEAuthority.bmp":
                        target.CurrentOneAuthority = effectTime;
                        target.ActivateBuff(target.pbOneAuthority, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "RisingAura.bmp":
                        target.CurrentRisingAura = effectTime;
                        target.ActivateBuff(target.pbRisingAura, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "AscensionAura.bmp":
                        target.CurrentAscensionAura = effectTime;
                        target.ActivateBuff(target.pbAscensionAura, Database.BaseResourceFolder + spellName, effectTime);
                        break;

                    case "SeventhMagic.bmp":
                        target.CurrentSeventhMagic = effectTime;
                        target.ActivateBuff(target.pbSeventhMagic, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(150));
                        break;

                    case "PhantasmalWind.bmp":
                        target.CurrentPhantasmalWind = effectTime;
                        target.ActivateBuff(target.pbPhantasmalWind, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(183));
                        break;
                    case "ParadoxImage.bmp":
                        target.CurrentParadoxImage = effectTime;
                        target.ActivateBuff(target.pbParadoxImage, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(184));
                        break;

                    case "AusterityMatrix.bmp":
                        target.CurrentAusterityMatrix = effectTime;
                        target.ActivateBuff(target.pbAusterityMatrix, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(207));
                        break;

                    case "RedDragonWill.bmp":
                        target.CurrentRedDragonWill = effectTime;
                        target.ActivateBuff(target.pbRedDragonWill, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(208));
                        break;

                    case "BlueDragonWill.bmp":
                        target.CurrentBlueDragonWill = effectTime;
                        target.ActivateBuff(target.pbBlueDragonWill, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(209));
                        break;

                    case "NourishSense.bmp":
                        target.CurrentNourishSense = effectTime;
                        target.ActivateBuff(target.pbNourishSense, Database.BaseResourceFolder + spellName, effectTime);
                        break;

                    case "StanceOfDouble.bmp":
                        target.CurrentStanceOfDouble = effectTime;
                        target.ActivateBuff(target.pbStanceOfDouble, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "SwiftStep.bmp":
                        target.CurrentSwiftStep = effectTime;
                        target.ActivateBuff(target.pbSwiftStep, Database.BaseResourceFolder + spellName, effectTime);
                        break;                      
                    case "VigorSense.bmp":
                        target.CurrentVigorSense = effectTime;
                        target.ActivateBuff(target.pbVigorSense, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "SmoothingMove.bmp":
                        target.CurrentSmoothingMove = effectTime;
                        target.ActivateBuff(target.pbSmoothingMove, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "FutureVision.bmp":
                        target.CurrentFutureVision = effectTime;
                        target.ActivateBuff(target.pbFutureVision, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "ReflexSpirit.bmp":
                        target.CurrentReflexSpirit = effectTime;
                        target.ActivateBuff(target.pbReflexSpirit, Database.BaseResourceFolder + spellName, effectTime);
                        break;
                    case "TrustSilence.bmp":
                        target.CurrentTrustSilence = effectTime;
                        target.ActivateBuff(target.pbTrustSilence, Database.BaseResourceFolder + spellName, effectTime);
                        break;

                    case "ConcussiveHit.bmp":
                        target.CurrentConcussiveHit = effectTime;
                        target.CurrentConcussiveHitValue++;
                        target.ChangeConcussiveHitStatus(target.CurrentConcussiveHitValue);
                        break;
                    case "OnslaughtHit.bmp":
                        target.CurrentOnslaughtHit = effectTime;
                        target.CurrentOnslaughtHitValue++;
                        target.ChangeOnslaughtHitStatus(target.CurrentOnslaughtHitValue);
                        break;
                    case "ImpulseHit.bmp":
                        target.CurrentImpulseHit = effectTime;
                        target.CurrentImpulseHitValue++;
                        target.ChangeImpulseHitStatus(target.CurrentImpulseHitValue);
                        break;
                    case "JUNKAN-SEIYAKU.bmp":
                        if (target.CurrentJunkan_Seiyaku <= 0)
                        {
                            target.CurrentJunkan_Seiyaku = effectTime;
                            target.ActivateBuff(target.pbJunkanSeiyaku, Database.BaseResourceFolder + spellName, effectTime);
                        }
                        else
                        {
                            UpdateBattleText(target.GetCharacterSentence(92));
                        }
                        break;

                    // �������g���Ώ�
                    case "StanceOfMystic.bmp":
                        target.CurrentStanceOfMystic = effectTime;
                        target.CurrentStanceOfMysticValue++;
                        target.ChangeStanceOfMysticStatus(target.CurrentStanceOfMysticValue);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(168), target.Name));
                        break;
                    case "TruthVision.bmp":
                        player.CurrentTruthVision = effectTime;
                        target.ActivateBuff(target.pbTruthVision, Database.BaseResourceFolder + spellName, effectTime);
                        UpdateBattleText(player.GetCharacterSentence(63));
                        break;

                    case "HighEmotionality.bmp":
                        if (player.CurrentHighEmotionality <= 0)
                        {
                            player.BuffStrength_HighEmotionality = player.Strength / 3;
                            player.BuffAgility_HighEmotionality = player.Agility / 3;
                            player.BuffIntelligence_HighEmotionality = player.Intelligence / 3;
                            //player.BuffStamina_HighEmotionality = player.Stamina / 3;
                            player.BuffMind_HighEmotionality = player.Mind / 3;

                            player.CurrentHighEmotionality = effectTime;
                            target.ActivateBuff(target.pbHighEmotionality, Database.BaseResourceFolder + spellName, effectTime);
                            UpdateBattleText(player.GetCharacterSentence(85), 1000);
                            UpdateBattleText(player.GetCharacterSentence(86));
                        }
                        else
                        {
                            UpdateBattleText(player.GetCharacterSentence(82));
                        }
                        break;

                    case "Negate.bmp":
                        player.CurrentNegate = effectTime;
                        player.ActivateBuff(player.pbNegate, Database.BaseResourceFolder + spellName, effectTime);
                        break;

                    case "StanceOfEyes.bmp":
                        player.CurrentStanceOfEyes = effectTime;
                        player.ActivateBuff(player.pbStanceOfEyes, Database.BaseResourceFolder + spellName, effectTime);
                        break;

                    case "CounterAttack.bmp":
                        player.CurrentCounterAttack = effectTime;
                        player.ActivateBuff(player.pbCounterAttack, Database.BaseResourceFolder + spellName, effectTime);
                        break;

                    case "StanceOfStanding.bmp":
                        player.CurrentStanceOfStanding = effectTime;
                        player.ActivateBuff(player.pbStanceOfStanding, Database.BaseResourceFolder + spellName, effectTime);
                        break;

                    case "AntiStun.bmp":
                        if (player.CurrentAntiStun <= 0)
                        {
                            player.CurrentAntiStun = effectTime;
                            target.ActivateBuff(target.pbAntiStun, Database.BaseResourceFolder + spellName, effectTime);
                            UpdateBattleText(player.GetCharacterSentence(93));
                        }
                        else
                        {
                            UpdateBattleText(player.GetCharacterSentence(92));
                        }
                        break;

                    case "StanceOfDeath.bmp":
                        if (player.CurrentStanceOfDeath <= 0)
                        {
                            player.CurrentStanceOfDeath = effectTime;
                            target.ActivateBuff(target.pbStanceOfDeath, Database.BaseResourceFolder + spellName, effectTime);
                            UpdateBattleText(player.GetCharacterSentence(95));
                        }
                        else
                        {
                            UpdateBattleText(player.GetCharacterSentence(92));
                        }
                        break;

                    case "NothingOfNothingness.bmp":
                        if (player.CurrentNothingOfNothingness <= 0)
                        {
                            player.CurrentNothingOfNothingness = effectTime;
                            player.ActivateBuff(player.pbNothingOfNothingness, Database.BaseResourceFolder + spellName, effectTime);
                            UpdateBattleText(player.GetCharacterSentence(106), 1000);
                            UpdateBattleText(player.GetCharacterSentence(107));
                        }
                        else
                        {
                            UpdateBattleText(player.GetCharacterSentence(92));
                        }
                        break;

                    case "SYUTYU-DANZETSU.bmp":
                        if (player.CurrentSyutyu_Danzetsu <= 0)
                        {
                            player.CurrentSyutyu_Danzetsu = effectTime;
                            player.ActivateBuff(player.pbSyutyuDanzetsu, Database.BaseResourceFolder + spellName, effectTime);
                            UpdateBattleText(player.GetCharacterSentence(203));
                        }
                        else
                        {
                            UpdateBattleText(player.GetCharacterSentence(92));
                        }
                        break;

                    case "TimeStop.bmp":
                        if (player.CurrentTimeStop <= 0)
                        {
                            player.CurrentTimeStop = effectTime;
                            player.ActivateBuff(player.pbTimeStop, Database.BaseResourceFolder + spellName, effectTime);
                            UpdateBattleText(player.GetCharacterSentence(47));
                        }
                        else
                        {
                            UpdateBattleText(player.GetCharacterSentence(92));
                        }
                        break;

                    case "AfterReviveHalf.bmp":
                        if (player.CurrentAfterReviveHalf <= 0)
                        {
                            player.CurrentAfterReviveHalf = effectTime;
                            player.ActivateBuff(player.pbAfterReviveHalf, Database.BaseResourceFolder + spellName, effectTime);
                            UpdateBattleText(player.GetCharacterSentence(212));
                        }
                        break;

                    // �A�C�e������
                    case Database.ITEMCOMMAND_FELTUS:
                        player.CurrentFeltus = effectTime;
                        player.CurrentFeltusValue++;
                        player.ChangeFeltusStatus(player.CurrentFeltusValue);
                        UpdateBattleText(target.Name + "�ɁA�y�_�z�̒~�σJ�E���^�[��������I\r\n");
                        break;

                    case Database.ITEMCOMMAND_JUZA_PHANTASMAL:
                        player.CurrentJuzaPhantasmal = effectTime;
                        player.CurrentJuzaPhantasmalValue++;
                        player.ChangeJuzaPhantasmalStatus(player.CurrentJuzaPhantasmalValue);
                        UpdateBattleText(target.Name + "�ɁA�y�D�z�̒~�σJ�E���^�[��������I\r\n");
                        break;

                    case Database.ITEMCOMMAND_ETERNAL_FATE:
                        player.CurrentEternalFateRing = effectTime;
                        player.CurrentEternalFateRingValue++;
                        player.ChangeEternalFateRingStatus(player.CurrentEternalFateRingValue);
                        UpdateBattleText(target.Name + "�ɁA�y���z�̒~�σJ�E���^�[��������I\r\n");
                        break;

                    case Database.ITEMCOMMAND_LIGHT_SERVANT:
                        player.CurrentLightServant = effectTime;
                        player.CurrentLightServantValue++;
                        player.ChangeLightServantStatus(player.CurrentLightServantValue);
                        UpdateBattleText(player.Name + "�ɁA�y���z�̒~�σJ�E���^�[��������I\r\n");
                        break;

                    case Database.ITEMCOMMAND_SHADOW_SERVANT:
                        player.CurrentShadowServant = effectTime;
                        player.CurrentShadowServantValue++;
                        player.ChangeShadowServantStatus(player.CurrentShadowServantValue);
                        UpdateBattleText(player.Name + "�ɁA�y�Łz�̒~�σJ�E���^�[��������I\r\n");
                        break;

                    case Database.ITEMCOMMAND_ADIL_RING_BLUE_BURN:
                        player.CurrentAdilBlueBurn = effectTime;
                        player.CurrentAdilBlueBurnValue++;
                        player.ChangeAdilBlueBurnStatus(player.CurrentAdilBlueBurnValue);
                        UpdateBattleText(target.Name + "�ɁA�y���z�̒~�σJ�E���^�[��������I\r\n");
                        break;

                    case Database.ITEMCOMMAND_MAZE_CUBE:
                        player.CurrentMazeCube = effectTime;
                        player.CurrentMazeCubeValue++;
                        player.ChangeMazeCubeStatus(player.CurrentMazeCubeValue);
                        UpdateBattleText(player.Name + "�ɁA�y���z�̒~�σJ�E���^�[��������I\r\n");
                        break;

                    case Database.ITEMCOMMAND_DETACHMENT_ORB:
                        player.CurrentDetachmentOrb = effectTime;
                        player.ActivateBuff(player.pbDetachmentOrb, Database.BaseResourceFolder + spellName + fileExt, effectTime);
                        UpdateBattleText(player.Name + "�ɁA�S�_���[�W�����̘����t�B�[���h���W�J���ꂽ�I\r\n");
                        break;

                    case Database.ITEMCOMMAND_DEVIL_SUMMONER_TOME:
                        player.CurrentDevilSummonerTome = effectTime;
                        player.ActivateBuff(player.pbDevilSummonerTome, Database.BaseResourceFolder + spellName + fileExt, effectTime);
                        UpdateBattleText(player.Name + "�̓A�[�N�f�[���������������I\r\n");
                        break;

                    case Database.ITEMCOMMAND_VOID_HYMNSONIA:
                        player.CurrentVoidHymnsonia = effectTime;
                        player.ActivateBuff(player.pbVoidHymnsonia, Database.BaseResourceFolder + spellName + fileExt, effectTime);
                        UpdateBattleText(player.Name + "�͋󋕂ȉ̐��ɐS��D��ꂽ��ԂƂȂ����I\r\n");
                        break;

                    case Database.ITEMCOMMAND_GENSEI_TAIMA:
                        player.CurrentGenseiTaima = effectTime;
                        player.ActivateBuff(player.pbGenseiTaima, Database.BaseResourceFolder + spellName + fileExt, effectTime);
                        UpdateBattleText(player.Name + "�͑ޖ��̖���g�p���鎖�ŁA�����ɑ΂��鋰�|����U�蕥�����I\r\n");
                        break;

                    case Database.LIFE_COUNT:
                        player.CurrentLifeCount = effectTime;
                        player.CurrentLifeCountValue = 1;
                        player.pbLifeCount.CumulativeAlign = TruthImage.CumulativeTextAlign.Center;
                        player.ActivateBuff(player.pbLifeCount, Database.BaseResourceFolder + spellName + fileExt, effectTime);
                        player.CurrentLifeCountValue = 3;
                        player.ChangeLifeCountStatus(player.CurrentLifeCountValue);
                        UpdateBattleText(player.Name + "�ɐ����̓J�E���^�[���R���������I\r\n");
                        break;

                    case Database.CHAOTIC_SCHEMA:
                        player.CurrentChaoticSchema = effectTime;
                        player.ActivateBuff(player.pbChaoticSchema, Database.BaseResourceFolder + spellName + fileExt, effectTime);
                        break;

                    default:
                        break;
                }
            }
            else
            {
                UpdateBattleText(player.GetCharacterSentence(53));
            }
        }

        // �����Ώۂ̖��@
        // �O���[���[�̃��\�b�h
        private void PlayerSpellGlory(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("Glory.mp3");
            PlayerBuffAbstract(player, player, 4, "Glory.bmp");
        }

        // �u���b�N�E�R���g���N�g�̃��\�b�h
        private void PlayerSpellBlackContract(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("BlackContract.mp3");
            PlayerBuffAbstract(player, target, 4, "BlackContract.bmp");
        }

        // �C���[�^���E���C�u�̃��\�b�h
        private void PlayerSpellImmortalRave(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("ImmortalRave.mp3");
            PlayerBuffAbstract(player, player, 4, "ImmortalRave.bmp");
        }

        // �Q�C���E�E�B���h�̃��\�b�h
        private void PlayerSpellGaleWind(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("GaleWind.mp3");
            if (player.CurrentGaleWind <= 0)
            {
                PlayerBuffAbstract(player, player, 2, "GaleWind.bmp");
            }
            else
            {
                // ��ҁA���F���[���w��s������邽�߂̎d�g�݁B
                PlayerBuffAbstract(player, player, player.CurrentGaleWind + 1, "GaleWind.bmp");
            }
        }

        // �G�[�e���E�h���C�u�̃��\�b�h
        private void PlayerSpellAetherDrive(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("AetherDrive.mp3");
            PlayerBuffAbstract(player, player, 4, "AetherDrive.bmp");
        }

        // �����E�C���[�j�e�B�̃��\�b�h
        private void PlayerSpellOneImmunity(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("OneImmunity.mp3");
            PlayerBuffAbstract(player, player, 4, "OneImmunity.bmp");
        }

        // �X�^���X�E�I�u�E�t���[�̃��\�b�h
        private void PlayerSkillStanceOfFlow(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("StanceOfFlow.mp3");
            PlayerBuffAbstract(player, player, 3, "StanceOfFlow.bmp");
        }

        //�@�g���D�X�E���B�W�����̃��\�b�h
        private void PlayerSkillTruthVision(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("TruthVision.mp3");
            PlayerBuffAbstract(player, target, 999, "TruthVision.bmp");
        }

        // �v���e�N�V�����̃��\�b�h
        private void PlayerSpellProtection(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("Protection.mp3");
            PlayerBuffAbstract(player, target, 999, "Protection.bmp");
        }

        /// <summary>
        /// �A�u�\�[�u�E�E�H�[�^�[�̃��\�b�h
        /// </summary>
        private void PlayerSpellAbsorbWater(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("AbsorbWater.mp3");
            PlayerBuffAbstract(player, target, 999, "AbsorbWater.bmp");
        }

        private void PlayerSpellSaintPower(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("SaintPower.mp3");
            PlayerBuffAbstract(player, target, 999, "SaintPower.bmp");
        }

        private void PlayerSpellShadowPact(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("ShadowPact.mp3");
            PlayerBuffAbstract(player, target, 999, "ShadowPact.bmp");
        }

        private void PlayerSpellBloodyVengeance(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("BloodyVengeance.mp3");
            PlayerBuffAbstract(player, target, 999, "BloodyVengeance.bmp");
        }

        private void PlayerSpellHeatBoost(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("HeatBoost.mp3");
            PlayerBuffAbstract(player, target, 999, "HeatBoost.bmp");
        }

        private void PlayerSpellRiseOfImage(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("RiseOfImage.mp3");
            PlayerBuffAbstract(player, target, 999, "RiseOfImage.bmp");
        }

        protected void PlayerSpellPromisedKnowledge(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("PromisedKnowledge.mp3");
            PlayerBuffAbstract(player, target, 999, "PromisedKnowledge.bmp");
        }

        private void PlayerSpellFlameAura(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("FlameAura.mp3");
            PlayerBuffAbstract(player, target, 999, "FlameAura.bmp");
        }

        private void PlayerSpellWordOfLife(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("WordOfLife.mp3");
            PlayerBuffAbstract(player, target, 999, "WordOfLife.bmp");
        }

        private void PlayerSpellWordOfFortune(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("WordOfFortune.mp3");
            PlayerBuffAbstract(player, target, 2, "WordOfFortune.bmp"); // �P�^�[���p���̂��߂ɂ́A�����l�͂P�{�P
        }

        private void PlayerSpellMirrorImage(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("MirrorImage.mp3");
            PlayerBuffAbstract(player, target, 999, "MirrorImage.bmp");
        }

        private void PlayerSpellDeflection(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("Deflection.mp3");
            PlayerBuffAbstract(player, target, 999, "Deflection.bmp");
        }

        private void PlayerSpellSigilOfHomura(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_SIGIL_OF_HOMURA);
            PlayerBuffAbstract(player, target, 999, "SigilOfHomura.bmp");
        }

        /// <summary>
        /// �t�@���^�Y�}���E�E�B���h�̃��\�b�h
        /// </summary>
        private void PlayerSpellPhantasmalWind(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_PHANTASMAL_WIND);
            PlayerBuffAbstract(player, target, 999, "PhantasmalWind.bmp");
        }

        /// <summary>
        /// �p���h�b�N�X�E�C���[�W�̃��\�b�h
        /// </summary>
        private void PlayerSpellParadoxImage(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_PARADOX_IMAGE);
            PlayerBuffAbstract(player, target, 999, "ParadoxImage.bmp");
        }

        private void PlayerSpellHymnContract(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_HYMN_CONTRACT);
            PlayerBuffAbstract(player, target, 4, "HymnContract.bmp");
        }

        /// <summary>
        /// �T�C�L�b�N�E�g�����X�̃��\�b�h
        /// </summary>
        private void PlayerSpellPsychicTrance(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("RiseOfImage.mp3");
            PlayerBuffAbstract(player, target, 999, "PsychicTrance.bmp");
        }

        /// <summary>
        /// �u���C���h�E�W���X�e�B�X�̃��\�b�h
        /// </summary>
        private void PlayerSpellBlindJustice(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("RiseOfImage.mp3");
            PlayerBuffAbstract(player, target, 999, "BlindJustice.bmp");
        }

        /// <summary>
        /// �g�����b�Z���f���g�E�E�B�b�V���̃��\�b�h
        /// </summary>
        private void PlayerSpellTranscendentWish(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("RiseOfImage.mp3");
            PlayerBuffAbstract(player, target, 4, "TranscendentWish.bmp"); // �R�^�[���p���̂��߂ɂ́A�����l�͂R�{�P
        }

        /// <summary>
        /// �t���[�Y���E�I�[���̃��\�b�h
        /// </summary>
        private void PlayerSpellFrozenAura(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("IceNeedle.mp3");
            PlayerBuffAbstract(player, target, 999, "FrozenAura.bmp");
        }

        /// <summary>
        /// �X�J�C�E�V�[���h�̃��\�b�h
        /// </summary>
        private void PlayerSpellSkyShield(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("Glory.mp3");
            PlayerBuffAbstract(player, target, 999, "SkyShield.bmp");
        }

        /// <summary>
        /// �X�^�e�B�b�N�E�o���A�̃��\�b�h1
        /// </summary>
        private void PlayerSpellStaticBarrier(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_STATIC_BARRIER);
            PlayerBuffAbstract(player, target, 999, "StaticBarrier.bmp");
        }

        /// <summary>
        /// �G���@�[�E�h���b�v���b�g�̃��\�b�h
        /// </summary>
        private void PlayerSpellEverDroplet(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("Glory.mp3");
            PlayerBuffAbstract(player, target, 999, "EverDroplet.bmp");
        }

        /// <summary>
        /// �z�[���[�E�u���C�J�[�̃��\�b�h
        /// </summary>
        private void PlayerSpellHolyBreaker(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("Glory.mp3");
            PlayerBuffAbstract(player, target, 999, "HolyBreaker.bmp");
        }

        /// <summary>
        /// �A�E�X�e���e�B�E�}�g���N�X�̃��\�b�h
        /// </summary>
        private void PlayerSpellAusterityMatrix(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_AUSTERITY_MATRIX);
            target.RemoveBuffSpell();
            PlayerBuffAbstract(player, target, 999, "AusterityMatrix.bmp");
        }

        /// <summary>
        /// ���b�h�E�h���S���E�E�B���̃��\�b�h
        /// </summary>
        private void PlayerSpellRedDragonWill(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_RED_DRAGON_WILL);
            PlayerBuffAbstract(player, target, 999, "RedDragonWill.bmp");
        }

        /// <summary>
        /// �u���[�E�h���S���E�E�B���̃��\�b�h
        /// </summary>
        private void PlayerSpellBlueDragonWill(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_BLUE_DRAGON_WILL);
            PlayerBuffAbstract(player, target, 999, "BlueDragonWill.bmp");
        }

        /// <summary>
        /// �V���E�t�H�[�`�����̃��\�b�h
        /// </summary>
        private void PlayerSpellSinFortune(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_SIN_FORTUNE);
            PlayerBuffAbstract(player, target, 999, "SinFortune.bmp");
        }

        /// <summary>
        /// �m���b�V���E�Z���X�̃��\�b�h
        /// </summary>
        private void PlayerSkillNourishSense(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(199));
            GroundOne.PlaySoundEffect(Database.SOUND_NOURISH_SENSE);
            PlayerBuffAbstract(player, target, 999, "NourishSense.bmp");
        }

        /// <summary>
        /// �G�^�[�i���E�v���[���X�̃��\�b�h
        /// </summary>
        private void PlayerSpellEternalPresence(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("EternalPresence.mp3");
            PlayerBuffAbstract(player, target, 999, "EternalPresence.bmp");
        }

        /// <summary>
        /// �_���l�[�V�����̃��\�b�h
        /// </summary>
        private void PlayerSpellDamnation(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("Damnation.mp3");
            PlayerBuffAbstract(player, target, 999, "Damnation.bmp");
        }

        /// <summary>
        /// �A�u�\�����[�g�E�[���̃��\�b�h
        /// </summary>
        private void PlayerSpellAbsoluteZero(MainCharacter player, MainCharacter target)
        {
            int effectTurn = 4;
            if (player.Name == Database.ENEMY_LAST_SIN_VERZE_ARTIE) { effectTurn = 1; }
            GroundOne.PlaySoundEffect("AbsoluteZero.mp3");
            if (player.Target.CurrentAbsoluteZero <= 0) // ���͖���Ȗ��@�̂��߁A�p���^�[���̘A���X�V�͏o���Ȃ����Ă���B
            {
                PlayerBuffAbstract(player, target, effectTurn, "AbsoluteZero.bmp");
            }
        }

        /// <summary>
        /// �X�^���X�E�I�u�E�_�u���̃��\�b�h
        /// </summary>
        private void PlayerSkillStanceOfDouble(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(215));
            GroundOne.PlaySoundEffect(Database.SOUND_STANCE_OF_DOUBLE);
            PlayerBuffAbstract(player, target, 1, "StanceOfDouble.bmp");
        }

        /// <summary>
        /// �^�C���X�g�b�v�̃��\�b�h
        /// </summary>
        private void PlayerSpellTimeStop(MainCharacter player, MainCharacter target, bool immidiateEnd = false)
        {
            GroundOne.PlaySoundEffect("TimeStop.mp3");
            if (player.CurrentTimeStop <= 0) // ���͖���Ȗ��@�̂��߁A�p���^�[���̘A���X�V�͏o���Ȃ����Ă���B
            {
                if (immidiateEnd)
                {
                    player.CurrentTimeStopImmediate = true;
                }
                PlayerBuffAbstract(player, player, 1, "TimeStop.bmp"); // �P�^�[���p��
            }
        }

        /// <summary>
        /// �_�[�P���E�t�B�[���h�̃��\�b�h
        /// </summary>
        private void PlayerSpellDarkenField(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(149));
            List<MainCharacter> group = new List<MainCharacter>();
            if (player == mc || player == sc || player == tc)
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else if (player == ec1 || player == ec2 || player == ec3)
            {
                if (mc != null && !mc.Dead) { group.Add(mc); }
                if (sc != null && !sc.Dead) { group.Add(sc); }
                if (tc != null && !tc.Dead) { group.Add(tc); }
            }
            
            for (int ii = 0; ii < group.Count; ii++)
            {
                GroundOne.PlaySoundEffect("DarkBlast.mp3");
                PlayerBuffAbstract(player, group[ii], 999, "DarkenField.bmp");
            }
        }

        /// <summary>
        /// �u���C�W���O�E�t�B�[���h�̃��\�b�h
        /// </summary>
        private void PlayerSpellBlazingField(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(178));
            List<MainCharacter> group = new List<MainCharacter>();
            if (player == mc || player == sc || player == tc)
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else if (player == ec1 || player == ec2 || player == ec3)
            {
                if (mc != null && !mc.Dead) { group.Add(mc); }
                if (sc != null && !sc.Dead) { group.Add(sc); }
                if (tc != null && !tc.Dead) { group.Add(tc); }
            }
            
            for (int ii = 0; ii < group.Count; ii++)
            {
                GroundOne.PlaySoundEffect("FlameStrike.mp3");
                PlayerBuffAbstract(player, group[ii], 999, "BlazingField.bmp");
            }
        }

        /// <summary>
        /// �C�O�U���e�B�b�h�E�t�B�[���h
        /// </summary>
        private void PlayerSpellExaltedField(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(174));
            List<MainCharacter> group = new List<MainCharacter>();
            if (IsPlayerEnemy(player))
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else
            {
                if (mc != null && !mc.Dead) { group.Add(mc); }
                if (sc != null && !sc.Dead) { group.Add(sc); }
                if (tc != null && !tc.Dead) { group.Add(tc); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                GroundOne.PlaySoundEffect("Protection.mp3");
                PlayerBuffAbstract(player, group[ii], 999, "ExaltedField.bmp");
            }
        }

        /// <summary>
        /// �����E�I�[�\���e�B�̃��\�b�h
        /// </summary>
        private void PlayerSkillOneAuthority(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(202));
            List<MainCharacter> group = new List<MainCharacter>();
            if (IsPlayerEnemy(player))
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else
            {
                if (mc != null && !mc.Dead) { group.Add(mc); }
                if (sc != null && !sc.Dead) { group.Add(sc); }
                if (tc != null && !tc.Dead) { group.Add(tc); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                GroundOne.PlaySoundEffect("SaintPower.mp3");
                PlayerBuffAbstract(player, group[ii], 4, "ONEAuthority.bmp");
            }
        }

        /// <summary>
        /// ���H���e�b�N�X�E�t�B�[���h
        /// </summary>
        private void PlayerSpellVortexField(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(185));
            List<MainCharacter> group = new List<MainCharacter>();
            if (IsPlayerAlly(player))
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else
            {
                if (mc != null && !mc.Dead) { group.Add(mc); }
                if (sc != null && !sc.Dead) { group.Add(sc); }
                if (tc != null && !tc.Dead) { group.Add(tc); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                GroundOne.PlaySoundEffect(Database.SOUND_VORTEX_FIELD);
                NowSlow(player, group[ii], 4);
            }
        }

        /// <summary>
        /// ���C�W���O�E�I�[���̃��\�b�h
        /// </summary>
        private void PlayerSkillRisingAura(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(175));
            List<MainCharacter> group = new List<MainCharacter>();
            if (IsPlayerEnemy(player))
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else
            {
                if (mc != null && !mc.Dead) { group.Add(mc); }
                if (sc != null && !sc.Dead) { group.Add(sc); }
                if (tc != null && !tc.Dead) { group.Add(tc); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                GroundOne.PlaySoundEffect("HighEmotionality.mp3");
                PlayerBuffAbstract(player, group[ii], 999, "RisingAura.bmp");
            }
        }

        /// <summary>
        /// �A�Z���V�����E�I�[���̃��\�b�h
        /// </summary>
        private void PlayerSkillAscensionAura(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(176));
            List<MainCharacter> group = new List<MainCharacter>();
            if (IsPlayerEnemy(player))
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else
            {
                if (mc != null && !mc.Dead) { group.Add(mc); }
                if (sc != null && !sc.Dead) { group.Add(sc); }
                if (tc != null && !tc.Dead) { group.Add(tc); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                GroundOne.PlaySoundEffect("HighEmotionality.mp3");
                PlayerBuffAbstract(player, group[ii], 999, "AscensionAura.bmp");
            }
        }

        /// <summary>
        /// �G�N���v�X�E�G���h�̃��\�b�h
        /// </summary>
        private void PlayerSpellEclipseEnd(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(210));
            GroundOne.PlaySoundEffect(Database.SOUND_ECLIPSE_END);
            List<MainCharacter> group = new List<MainCharacter>();
            if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
            if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
            if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            if (mc != null && !mc.Dead) { group.Add(mc); }
            if (sc != null && !sc.Dead) { group.Add(sc); }
            if (tc != null && !tc.Dead) { group.Add(tc); }

            for (int ii = 0; ii < group.Count; ii++)
            {
                RemoveBuffAll(group[ii]);
                PlayerBuffAbstract(player, group[ii], 2, "EclipseEnd.bmp");
            }
        }

        /// <summary>
        /// �����@�E�A�j�q���[�V�����̃��\�b�h
        /// </summary>
        private void PlayerSpellLavaAnnihilation(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(10028));
            GroundOne.PlaySoundEffect("LavaAnnihilation.mp3");
            List<MainCharacter> group = new List<MainCharacter>();
            if (IsPlayerAlly(player))
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else
            {
                if (mc != null && !mc.Dead) { group.Add(mc); }
                if (sc != null && !sc.Dead) { group.Add(sc); }
                if (tc != null && !tc.Dead) { group.Add(tc); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                AbstractMagicDamage(player, group[ii], 0, PrimaryLogic.LavaAnnihilationValue(player, this.DuelMode), 0.0f, String.Empty, 0, TruthActionCommand.MagicType.Fire, false, CriticalType.Random);
            }
        }
        
        private void PlayerSkillFatalBlow(MainCharacter player, MainCharacter target)
        {
            bool notOneKill = false;
            if (target.GetType() == typeof(TruthEnemyCharacter))
            {
                if (((TruthEnemyCharacter)target).Area == TruthEnemyCharacter.MonsterArea.Duel) { notOneKill = true; }
                if (((TruthEnemyCharacter)target).Rare == TruthEnemyCharacter.RareString.Purple) { notOneKill = true; }
                if (((TruthEnemyCharacter)target).Rare == TruthEnemyCharacter.RareString.Gold) { notOneKill = true; }
            }

            if (notOneKill)
            {
                // 100%�N���e�B�J���q�b�g
                PlayerNormalAttack(player, target, 0, 0, false, false, 0, 0, Database.SOUND_KINETIC_SMASH, 173, true, CriticalType.Absolute);
            }
            else
            {
                int rand = AP.Math.RandomInteger(3);
                if (rand <= 0)
                {
                    PlayerDeath(player, target);
                }
                else
                {
                    // 100%�N���e�B�J���q�b�g
                    PlayerNormalAttack(player, target, 0, 0, false, false, 0, 0, Database.SOUND_KINETIC_SMASH, 173, true, CriticalType.Absolute);
                }
            }
        }

        /// <summary>
        /// �Z�u���X�E�}�W�b�N�̃��\�b�h
        /// </summary>
        private void PlayerSpellSeventhMagic(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("TruthVision.mp3");
            PlayerBuffAbstract(player, target, 999, "SeventhMagic.bmp");
        }

        /// <summary>
        /// �X�^���X�E�I�u�E�~�X�e�B�b�N�̃��\�b�h
        /// </summary>
        private void PlayerSkillStanceOfMystic(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("GaleWind.mp3");
            PlayerBuffAbstract(player, target, 999, "StanceOfMystic.bmp");
        }

        bool CannotResurrect = false;
        /// <summary>
        /// ���U���N�V�����̃��\�b�h
        /// </summary>
        private void PlayerSpellResurrection(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("Resurrection.mp3");
            UpdateBattleText(player.GetCharacterSentence(52), 3000);

            ResurrectionLogic(player, target, Database.RESURRECTION);

        }

        /// <summary>
        /// �f�X�E�f�B�i�C�̃��\�b�h
        /// </summary>
        private void PlayerSpellDeathDeny(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect(Database.SOUND_DEATH_DENY);
            UpdateBattleText(player.GetCharacterSentence(214), 3000);

            ResurrectionLogic(player, target, Database.DEATH_DENY);
            NowNoResurrection(player, target, 999);
        }

        private void ResurrectionLogic(MainCharacter player, MainCharacter target, string command)
        {
            if (target != null)
            {
                if (DetectOpponentParty(player, target))
                {
                    UpdateBattleText(player.GetCharacterSentence(53));
                }
                else
                {
                    if (target == player)
                    {
                        UpdateBattleText(player.GetCharacterSentence(55));
                    }
                    else if (!target.Dead)
                    {
                        UpdateBattleText(player.GetCharacterSentence(54));
                    }
                    else if (this.CannotResurrect)
                    {
                        UpdateBattleText("�������A���S��Ύ��ԗ��y�I���z�̌��ʂɂ�蕜�����ł��Ȃ��I\r\n");
                    }
                    else if (target.CurrentNoResurrection > 0)
                    {
                        UpdateBattleText("�������A" + target.Name + "�͕����ł��Ȃ������I");
                    }
                    else
                    {
                        if (command == Database.DEATH_DENY)
                        {
                            target.ResurrectPlayer((int)PrimaryLogic.DeathDenyValue(target));
                            target.CurrentMana = target.MaxMana;
                            UpdateMana(target, target.MaxMana, true, false, 0);
                            target.CurrentSkillPoint = target.MaxSkillPoint;
                            UpdateSkillPoint(target, target.MaxMana, true, false, 0);
                        }
                        else
                        {
                            target.ResurrectPlayer((int)PrimaryLogic.ResurrectionValue(target));
                        }
                        this.Update();
                        UpdateBattleText(target.Name + "�͕��������I\r\n");
                    }
                }
            }
            else
            {
                UpdateBattleText(player.GetCharacterSentence(53));
            }
        }

        /// <summary>
        /// �X�g���[�g�X�}�b�V���̃��\�b�h
        /// </summary>
        private void PlayerSkillStraightSmash(MainCharacter player, MainCharacter target, int interval, bool ignoreDefense)
        {
            UpdateBattleText(player.GetCharacterSentence(1));
            double damage = PrimaryLogic.StraightSmashValue(player, this.DuelMode);
            PlayerNormalAttack(player, target, 0, 0, ignoreDefense, false, damage, interval, Database.SOUND_STRAIGHT_SMASH, -1, true, CriticalType.Random);
        }

        /// <summary>
        /// �T�C�����g�E���b�V���̃��\�b�h
        /// </summary>
        /// <param name="player"></param>
        /// <param name="target"></param>
        /// <param name="p"></param>
        /// <param name="p_2"></param>
        private void PlayerSkillSilentRush(MainCharacter player, MainCharacter target)
        {
            for (int ii = 0; ii < 3; ii++)
            {
                string soundName = "Hit01.mp3";
                int interval = 30;
                int sentence = 89; if (ii == 1) sentence = 90; if (ii == 2) sentence = 91;

                PlayerNormalAttack(player, target, 0, 0, false, false, 0, interval, soundName, sentence, false, CriticalType.Random);
            }
        }

        /// <summary>
        /// �J���l�[�W���b�V���̃��\�b�h
        /// </summary>
        private void PlayerSkillCarnageRush(MainCharacter player, MainCharacter target)
        {
            for (int ii = 0; ii < 5; ii++)
            {
                string soundName = "Hit01.mp3"; if (ii == 4) soundName = "KineticSmash.mp3";
                int interval = 30; if (ii == 1 || ii == 2 || ii == 3) { interval = 8; } if (ii == 4) { interval = 30; }
                int sentence = 65; if (ii == 1) sentence = 66; if (ii == 2) sentence = 67; if (ii == 3) sentence = 68; if (ii == 4) sentence = 69;

                PlayerNormalAttack(player, target, 0, 0, false, false, 0, interval, soundName, sentence, false, CriticalType.Random);
            }
        }

        /// <summary>
        /// �\�E���E�G�O�[�L���[�V����
        /// </summary>
        /// <param name="player"></param>
        /// <param name="target"></param>
        private void PlayerSkillSoulExecution(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(188), 1000);
            bool alreadyTruthVision = false;
            if (player.CurrentTruthVision <= 0)
            {
                PlayerSkillTruthVision(player, player);
            }
            else
            {
                alreadyTruthVision = true;
            }
            
            for (int ii = 0; ii < 10; ii++)
            {
                string soundName = "Hit01.mp3"; if (ii == 9) soundName = "Catastrophe.mp3";
                int[] interval = { 10, 9, 8, 7, 6, 5, 4, 3, 50, 0 };
                int[] sentence = { 189, 190, 191, 192, 193, 194, 195, 196, 197, 198 };
                double[] damageMag = { 1.0f, 1.1f, 1.2f, 1.3f, 1.5f, 1.7f, 1.9f, 2.2f, 2.5f, 3.0f }; // ; damageMag += ii * 0.2f; if (ii == 9) damageMag += 3.0f;
                PlayerNormalAttack(player, target, damageMag[ii], 0, false, false, 0, interval[ii], soundName, sentence[ii], false, CriticalType.Random);
            }

            if (alreadyTruthVision == false)
            {
                player.CurrentTruthVision = 0;
                player.DeBuff(player.pbTruthVision);
            }
        }


        /// <summary>
        /// �G�j�O�}�E�Z���X�̃��\�b�h
        /// </summary>
        private void PlayerSkillEnigmaSense(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(72));
            double atkBase = PrimaryLogic.EnigmaSenseValue(player, this.DuelMode);
            PlayerNormalAttack(player, target, 0, 0, false, false, atkBase, 0, string.Empty, -1, true, CriticalType.Random);
        }

        /// <summary>
        /// �N���b�V���O�E�u���[�̃��\�b�h
        /// </summary>
        private void PlayerSkillCrushingBlow(MainCharacter player)
        {
            PlayerNormalAttack(player, player.Target, 0, 2, false, false, 0, 0, Database.SOUND_CRUSHING_BLOW, -1, true, CriticalType.Random);
        }

        /// <summary>
        /// �L�l�e�B�b�N�E�X�}�b�V���̃��\�b�h
        /// </summary>
        private void PlayerSkillKineticSmash(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(74));
            double damage = PrimaryLogic.KineticSmashValue(player, this.DuelMode);
            PlayerNormalAttack(player, target, 0, 0, false, false, damage, 0, Database.SOUND_KINETIC_SMASH, -1, true, CriticalType.Random);
        }

        /// <summary>
        /// �\�E���E�C���t�B�j�e�B�̃��\�b�h
        /// </summary>
        private void PlayerSkillSoulInfinity(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(73));
            double damage = PrimaryLogic.SoulInfinityValue(player, this.DuelMode);
            PlayerNormalAttack(player, target, 0, 0, false, false, damage, 0, Database.SOUND_SOUL_INFINITY, -1, true, CriticalType.Random);
        }

        /// <summary>
        /// �J�^�X�g���t�B�̃��\�b�h
        /// </summary>
        private void PlayerSkillCatastrophe(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(98), 1000);
            double damage = PrimaryLogic.CatastropheValue(player, this.DuelMode);
            PlayerNormalAttack(player, target, 0, 0, false, false, damage, 0, Database.SOUND_CATASTROPHE, 99, true, CriticalType.Random);
        }

        /// <summary>
        /// �O�E�C���p�N�g�̃��\�b�h
        /// </summary>
        private void PlayerSkillOboroImpact(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(96), 1000);
            double damage = PrimaryLogic.OboroImpactValue(player, this.DuelMode);
            PlayerNormalAttack(player, target, 0, 0, false, false, damage, 0, Database.SOUND_OBORO_IMPACT, 97, true, CriticalType.Random);
        }


        /// <summary>
        /// �C���i�[�E�C���X�s���[�V�����̃��\�b�h
        /// </summary>
        private void PlayerSkillInnerInspiration(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("InnerInspiration.mp3");
            double effectValue = PrimaryLogic.InnerInspirationValue(player);
            effectValue = GainIsZero(effectValue, player);
            UpdateBattleText(String.Format(player.GetCharacterSentence(51), Convert.ToString((int)effectValue)));
            player.CurrentSkillPoint += (int)effectValue;
            UpdateSkillPoint(player, effectValue, true, true, 0);
        }

        /// <summary>
        /// �X�^���X�E�I�u�E�X�^���f�B���O�̃��\�b�h
        /// </summary>
        private void PlayerSkillStanceOfStanding(MainCharacter player, MainCharacter target)
        {
            if (PlayerNormalAttack(player, target, 0, false, false))
            {
                PlayerBuffAbstract(player, player, 1, Database.STANCE_OF_STANDING + ".bmp");
            }
            UpdateBattleText(player.GetCharacterSentence(56));
        }
        /// <summary>
        /// �j���[�g�����E�X�}�b�V���̃��\�b�h
        /// </summary>
        private void PlayerSkillNeutralSmash(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(152));
            PlayerNormalAttack(player, target, 0, false, false);
        }
        /// <summary>
        /// �X�E�B�t�g�E�X�e�b�v�̃��\�b�h
        /// </summary>
        private void PlayerSkillSwiftStep(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(153));
            GroundOne.PlaySoundEffect("StanceOfFlow.mp3");
            PlayerBuffAbstract(player, target, 3, "SwiftStep.bmp");
        }
        /// <summary>
        /// ���B�S�[�E�Z���X�̃��\�b�h
        /// </summary>
        private void PlayerSkillVigorSense(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(201));
            GroundOne.PlaySoundEffect("StanceOfFlow.mp3");
            PlayerBuffAbstract(player, target, 3, "VigorSense.bmp");
        }

        /// <summary>
        /// �T�[�N���E�X���b�V���̃��\�b�h
        /// </summary>
        private void PlayerSkillCircleSlash(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(154));
            List<MainCharacter> group = new List<MainCharacter>();
            if (player == mc || player == sc || player == tc)
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else if (player == ec1 || player == ec2 || player == ec3)
            {
                if (mc != null && !mc.Dead) { group.Add(mc); }
                if (sc != null && !sc.Dead) { group.Add(sc); }
                if (tc != null && !tc.Dead) { group.Add(tc); }
            }
            for (int ii = 0; ii < group.Count; ii++)
            {
                PlayerNormalAttack(player, group[ii], 0, false, false);
            }
        }
        /// <summary>
        /// �����u���E�V���E�g�̃��\�b�h
        /// </summary>
        private void PlayerSkillRumbleShout(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(155));
            target.Target = player;
            target.StackTarget = player;
        }
        /// <summary>
        /// �X���[�W���O�E���[���̃��\�b�h
        /// </summary>
        private void PlayerSkillSmoothingMove(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(156));
            if (PlayerNormalAttack(player, target, 0, false, false))
            {
                GroundOne.PlaySoundEffect("AeroBlade.mp3");
                PlayerBuffAbstract(player, player, 1, "SmoothingMove.bmp");
            }
        }
        /// <summary>
        /// �t���[�`���[�E���B�W�����̃��\�b�h
        /// </summary>
        private void PlayerSkillFutureVision(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(157));
            GroundOne.PlaySoundEffect("Tranquility.mp3");
            PlayerBuffAbstract(player, player, 2, "FutureVision.bmp");
        }
        /// <summary>
        /// ���t���b�N�X�E�X�s���b�g�̃��\�b�h
        /// </summary>
        private void PlayerSkillReflexSpirit(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(158));
            GroundOne.PlaySoundEffect("Tranquility.mp3");
            PlayerBuffAbstract(player, player, 999, "ReflexSpirit.bmp");
        }
        /// <summary>
        /// �V���[�v�E�O���A�̃��\�b�h
        /// </summary>
        private void PlayerSkillSharpGlare(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(159));
            if (PlayerNormalAttack(player, target, 0, false, false))
            {
                GroundOne.PlaySoundEffect("RisingKnuckle.mp3");
                NowSilence(player, target, 3);
            }
        }
        /// <summary>
        /// �A���m�E���E�V���b�N�̃��\�b�h
        /// </summary>
        private void PlayerSkillUnknownShock(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(187));

            List<MainCharacter> group = new List<MainCharacter>();
            if (IsPlayerAlly(player))
            {
                if (ec1 != null && !ec1.Dead) { group.Add(ec1); }
                if (ec2 != null && !ec2.Dead) { group.Add(ec2); }
                if (ec3 != null && !ec3.Dead) { group.Add(ec3); }
            }
            else
            {
                if (mc != null && !mc.Dead) { group.Add(mc); }
                if (sc != null && !sc.Dead) { group.Add(sc); }
                if (tc != null && !tc.Dead) { group.Add(tc); }
            }

            for (int ii = 0; ii < group.Count; ii++)
            {
                if (PlayerNormalAttack(player, group[ii], 0, false, false))
                {
                    GroundOne.PlaySoundEffect("RisingKnuckle.mp3");
                    NowBlind(player, group[ii], 3);
                }
            }
        }

        /// <summary>
        /// �g���X�g�E�T�C�����X�̃��\�b�h
        /// </summary>
        private void PlayerSkillTrustSilence(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(160));
            GroundOne.PlaySoundEffect("Tranquility.mp3");
            PlayerBuffAbstract(player, player, 999, "TrustSilence.bmp");
        }
        /// <summary>
        /// �T�v���C�Y�E�A�^�b�N�̃��\�b�h
        /// </summary>
        private void PlayerSkillSurpriseAttack(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(161));
            if (PlayerNormalAttack(player, target, 0, false, false))
            {
                bool result = NowParalyze(player, target, 1);
                if (result == false)
                {
                    ((TruthEnemyCharacter)player).DetectCannotBeParalyze = true;
                }
            }
        }
        /// <summary>
        /// �T�C�L�b�N�E�E�F�C���̃��\�b�h
        /// </summary>
        private void PlayerSkillPsychicWave(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(162));

            double damage = PrimaryLogic.PsychicWaveValue(player, this.DuelMode);
            AbstractMagicDamage(player, target, 0, damage, 0, "WordOfPower.mp3", -1, TruthActionCommand.MagicType.None, true, CriticalType.Random);
        }
        /// <summary>
        /// ���J�o�[�̃��\�b�h
        /// </summary>
        private void PlayerSkillRecover(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(163));
            target.RecoverStunning();
            target.RecoverParalyze();
            target.RecoverFrozen();
        }
        /// <summary>
        /// �o�C�I�����g�E�X���b�V���̃��\�b�h
        /// </summary>
        private void PlayerSkillViolentSlash(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(164));
            double damage = PrimaryLogic.ViolentSlashValue(player, this.DuelMode);
            PlayerNormalAttack(player, target, 0, 0, true, false, damage, 0, Database.SOUND_KINETIC_SMASH, -1, false, CriticalType.Random);
        }
        /// <summary>
        /// �A�E�^�[�E�C���X�s���[�V�����̃��\�b�h
        /// </summary>
        private void PlayerSkillOuterInspiration(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("WordOfLife.mp3");
            UpdateBattleText(player.GetCharacterSentence(165));
            target.RemovePhysicalAttackDown();
            target.RemovePhysicalDefenseDown();
            target.RemoveMagicAttackDown();
            target.RemoveMagicDefenseDown();
            target.RemoveSpeedDown();
            target.RemoveReactionDown();
            target.RemovePotentialDown();
            UpdateBattleText(target.Name + "�̔\�͒ቺ��Ԃ��������ꂽ�I");
        }

        /// <summary>
        /// �f�B�[�v�E�~���[�̃��\�b�h
        /// </summary>
        private void PlayerSpellDeepMirror(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(179));
            player.CurrentDeepMirror = true;
        }

        /// <summary>
        /// �X�^���X�E�I�u�E�T�b�h�l�X�̃��\�b�h
        /// </summary>
        private void PlayerSkillStanceOfSuddenness(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(166));
            player.CurrentStanceOfSuddenness = true;
        }

        /// <summary>
        /// �n�[�f�X�g�E�p���B�̃��\�b�h
        /// </summary>
        private void PlayerSkillHardestParry(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(169));
            player.CurrentHardestParry = true;
        }

        /// <summary>
        /// �R���J�V�b���E�q�b�g�̃��\�b�h
        /// </summary>
        private void PlayerSkillConcussiveHit(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(170));
            if (PlayerNormalAttack(player, target, 0, false, false))
            {
                PlayerBuffAbstract(player, target, 999, "ConcussiveHit.bmp");
            }
        }

        /// <summary>
        /// �I���X���[�g�E�q�b�g�̃��\�b�h
        /// </summary>
        private void PlayerSkillOnslaughtHit(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(171));
            if (PlayerNormalAttack(player, target, 0, false, false))
            {
                PlayerBuffAbstract(player, target, 999, "OnslaughtHit.bmp");
            }
        }

        /// <summary>
        /// �C���p���X�E�q�b�g�̃��\�b�h
        /// </summary>
        private void PlayerSkillImpulseHit(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(172));
            if (PlayerNormalAttack(player, target, 0, false, false))
            {
                PlayerBuffAbstract(player, target, 999, "ImpulseHit.bmp");
            }
        }

        /// <summary>
        /// �n�C�E�G���[�V���i���e�B�̃��\�b�h
        /// </summary>
        private void PlayerSkillHighEmotionality(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("HighEmotionality.mp3");
            player.Target = player;
            PlayerBuffAbstract(player, player, 4, "HighEmotionality.bmp");
        }

        /// <summary>
        /// �X�^���X�E�I�u�E�f�X�̃��\�b�h
        /// </summary>
        private void PlayerSkillStanceOfDeath(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("StanceOfDeath.mp3");
            player.Target = player;
            PlayerBuffAbstract(player, player, 999, "StanceOfDeath.bmp");
        }

        /// <summary>
        /// �A���`�E�X�^���̃��\�b�h
        /// </summary>
        private void PlayerSkillAntiStun(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("AntiStun.mp3");
            player.Target = player;
            PlayerBuffAbstract(player, player, 999, "AntiStun.bmp");
        }

        /// <summary>
        /// �y�C���t���E�C���T�j�e�B
        /// </summary>
        private void PlayerSkillPainfulInsanity(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("PainfulInsanity.mp3");
            player.Target = player;
            PlayerBuffAbstract(player, player, 999, "PainfulInsanity.bmp"); 
        }

        /// <summary>
        /// �i�b�V���O�E�I�u�E�i�b�V���O�l�X�̃��\�b�h
        /// </summary>
        private void PlayerSkillNothingOfNothingness(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("NothingOfNothingness.mp3");
            player.Target = player;
            PlayerBuffAbstract(player, player, 999, "NothingOfNothingness.bmp");
        }


        #endregion


        private bool CheckDodge(MainCharacter player, MainCharacter target)
        {
            return CheckDodge(player, target, false);
        }

        private bool CheckDodge(MainCharacter player, MainCharacter target, bool ignoreDodge)
        {
            if ((target.MainArmor != null) && (target.MainArmor.Name == Database.RARE_ONEHUNDRED_BUTOUGI) &&
                (target.CurrentStunning <= 0) &&
                (target.CurrentFrozen <= 0) &&
                (target.CurrentParalyze <= 0) &&
                (target.CurrentTemptation <= 0))
            {
                Random rd = new Random(DateTime.Now.Millisecond * Environment.TickCount);
                if (rd.Next(1, 101) <= PrimaryLogic.OneHundredButougiValue(target))
                {
                    UpdateBattleText(target.Name + "��" + target.MainArmor.Name + "�̌��ʂőf�����g�����킵���I\r\n");
                    return true;
                }
            }

            if ((target.Accessory != null) && (target.Accessory.Name == "�g���킵�̃}���g") &&
                (target.CurrentStunning <= 0) &&
                (target.CurrentFrozen <= 0) &&
                (target.CurrentParalyze <= 0) &&
                (target.CurrentTemptation <= 0))
            {
                Random rd = new Random(DateTime.Now.Millisecond * Environment.TickCount);
                if (rd.Next(1, 101) <= target.Accessory.MinValue)
                {
                    UpdateBattleText(target.Name + "��" + target.Accessory.Name + "�̌��ʂőf�����g�����킵���I\r\n");
                    return true;
                }
            }

            if ((target.Accessory2 != null) && (target.Accessory2.Name == "�g���킵�̃}���g") &&
                    (target.CurrentStunning <= 0) &&
                    (target.CurrentFrozen <= 0) &&
                    (target.CurrentParalyze <= 0) &&
                    (target.CurrentTemptation <= 0))
            {
                Random rd = new Random(DateTime.Now.Millisecond * Environment.TickCount);
                if (rd.Next(1, 101) <= target.Accessory2.MinValue)
                {
                    UpdateBattleText(target.Name + "��" + target.Accessory2.Name + "�̌��ʂőf�����g�����킵���I\r\n");
                    return true;
                }
            }

            if (target.CurrentBlinded > 0)
            {
                UpdateBattleText(target.Name + "�͑ޔ���Ԃɂ��A��Ȃ��g�����킵���I\r\n");
                return true;
            }

            return false;
        }

        private bool CheckBlindMiss(MainCharacter player, MainCharacter target)
        {
            if (player.CurrentBlind > 0)
            {
                int randomValue = AP.Math.RandomInteger(1000);
                if (randomValue <= 500)
                {
                    UpdateBattleText(player.Name + "�͍U�����O���Ă��܂����I\r\n");
                    return true;
                }
            }
            return false;
        }

        private void UpdateBattleText(string text)
        {
            txtBattleMessage.Text = txtBattleMessage.Text.Insert(0, text);
            txtBattleMessage.Update();
        }
        private void UpdateBattleText(string text, int sleepTime)
        {
            txtBattleMessage.Text = txtBattleMessage.Text.Insert(0, text);
            txtBattleMessage.Update();
            this.Update();

            if (sleepTime > 0)
            {
                System.Threading.Thread.Sleep(sleepTime);
                Application.DoEvents();
            }
        }

        private void TruthBattleEnemy_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (GroundOne.sound != null)
            {
                GroundOne.sound.StopMusic();
                //GroundOne.sound.Disactive();
            }
        }

        int battleStartCounter = 4;
        private void timerBattleStart_Tick(object sender, EventArgs e)
        {
            this.battleStartCounter--;
            if (this.battleStartCounter <= 0)
            {
                if (this.DuelMode)
                {
                    UpdateBattleText("DUEL�J�n�I  \r\n");
                }
                else
                {
                    UpdateBattleText("�퓬�J�n�I  \r\n");
                }
                timerBattleStart.Stop();
                timerBattleStart.Enabled = false;
                BattleStart_Click(null, null);
            }
            else
            {
                UpdateBattleText(battleStartCounter.ToString() + "�E�E�E\r\n");
            }
        }

        /// <summary>
        /// ���C�t�������̕\��
        /// </summary>
        /// <param name="player">�Ώی��v���C���[</param>
        /// <param name="damage">�_���[�W��</param>
        /// <param name="plusValue">�_���[�W���v���X�̏ꍇ</param>
        /// <param name="animationDamage">�A�j���[�V��������ꍇ�F�s�q�t�d</param>
        /// <param name="interval">�A�j���[�V��������ꍇ�̃C���^�[�o��</param>
        /// <param name="critical">�N���e�B�J���̏ꍇ�F�s�q�t�d</param>
        private void UpdateLife(MainCharacter player)
        {
            UpdateLife(player, 0, false, false, 0, false);
        }
        private void UpdateLife(MainCharacter player, double damage, bool plusValue, bool animationDamage, int interval, bool critical)
        {
            if (player.labelLife != null)
            {
                player.labelLife.Text = player.CurrentLife.ToString();
                if (player.CurrentLife >= player.MaxLife)
                {
                    player.labelLife.ForeColor = Color.Green;
                    if (this.NowTimeStop)
                    {
                        player.labelLife.ForeColor = Color.LightGreen;
                    }
                }
                else
                {
                    player.labelLife.ForeColor = Color.Black;
                    if (this.NowTimeStop)
                    {
                        player.labelLife.ForeColor = Color.White;
                    }
                }
                player.labelLife.Update();
            }
            if (animationDamage)
            {
                Color color = Color.Black;
                if (plusValue)
                {
                    color = Color.Green;
                    if (this.NowTimeStop)
                    {
                        color = Color.LightGreen;
                    }
                }
                else if (this.NowTimeStop)
                {
                    color = Color.White;
                }
                this.Invoke(new _AnimationDamage(AnimationDamage), damage, player, interval, color, false, critical, String.Empty);
            }
        }

        private void UpdateInstantPoint(MainCharacter player)
        {
            if (player.labelCurrentInstantPoint != null)
            {
                player.labelCurrentInstantPoint.Text = player.CurrentInstantPoint.ToString() + " / " + player.MaxInstantPoint.ToString();
                player.labelCurrentInstantPoint.Update();
            }
        }

        private void UpdateMana(MainCharacter player)
        {
            UpdateMana(player, 0, false, false, 0);
        }
        private void UpdateMana(MainCharacter player, double effectValue, bool plusValue, bool animationDamage, int interval)
        {
            if (player.labelCurrentManaPoint != null)
            {
                player.labelCurrentManaPoint.Text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();
                player.labelCurrentManaPoint.Update();
            }
            if (animationDamage)
            {
                this.Invoke(new _AnimationDamage(AnimationDamage), effectValue, player, interval, Color.Blue, false, false, String.Empty);
            }
        }

        private void UpdateSkillPoint(MainCharacter player)
        {
            UpdateSkillPoint(player, 0, false, false, 0);
        }
        private void UpdateSkillPoint(MainCharacter player, double effectValue, bool plusValue, bool animationDamage, int interval)
        {
            if (player.labelCurrentSkillPoint != null)
            {
                player.labelCurrentSkillPoint.Text = player.CurrentSkillPoint.ToString() + " / " + player.MaxSkillPoint.ToString();
                player.labelCurrentSkillPoint.Update();
            }
            if (animationDamage)
            {
                this.Invoke(new _AnimationDamage(AnimationDamage), effectValue, player, interval, Color.DarkGreen, false, false, String.Empty);
            }

        }


        string instantActionCommandString = String.Empty;
        MainCharacter tempTargetForInstant = null;
        MainCharacter tempTargetForTarget2 = null;
        MainCharacter tempTargetForTarget = null;

        private void TruthBattleEnemy_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private bool CheckBattlePlaying()
        {
            if (th == null)
            {
                return true;
            }
            else
            {
                if (tempStopFlag)
                {
                    return true;
                }
            }
            return false;
        }

        bool NowSelectingTarget = false;
        MainCharacter currentTargetedPlayer = null;
        private void ActionButton11_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, mc, mc.BattleActionCommand1, 0);
        }
        private void ActionButton12_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, mc, mc.BattleActionCommand2, 1);
        }
        private void ActionButton13_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, mc, mc.BattleActionCommand3, 2);
        }
        private void ActionButton14_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, mc, mc.BattleActionCommand4, 3);
        }
        private void ActionButton15_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, mc, mc.BattleActionCommand5, 4);
        }
        private void ActionButton16_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, mc, mc.BattleActionCommand6, 5);
        }
        private void ActionButton17_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, mc, mc.BattleActionCommand7, 6);
        }
        private void ActionButton18_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, mc, mc.BattleActionCommand8, 7);
        }
        private void ActionButton19_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, mc, mc.BattleActionCommand9, 8);
        }

        private void ActionButton21_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, sc, sc.BattleActionCommand1, 0);
        }
        private void ActionButton22_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, sc, sc.BattleActionCommand2, 1);
        }
        private void ActionButton23_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, sc, sc.BattleActionCommand3, 2);
        }
        private void ActionButton24_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, sc, sc.BattleActionCommand4, 3);
        }
        private void ActionButton25_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, sc, sc.BattleActionCommand5, 4);
        }
        private void ActionButton26_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, sc, sc.BattleActionCommand6, 5);
        }
        private void ActionButton27_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, sc, sc.BattleActionCommand7, 6);
        }
        private void ActionButton28_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, sc, sc.BattleActionCommand8, 7);
        }
        private void ActionButton29_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, sc, sc.BattleActionCommand9, 8);
        }

        private void ActionButton31_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, tc, tc.BattleActionCommand1, 0);
        }
        private void ActionButton32_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, tc, tc.BattleActionCommand2, 1);
        }
        private void ActionButton33_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, tc, tc.BattleActionCommand3, 2);
        }
        private void ActionButton34_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, tc, tc.BattleActionCommand4, 3);
        }
        private void ActionButton35_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, tc, tc.BattleActionCommand5, 4);
        }
        private void ActionButton36_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, tc, tc.BattleActionCommand6, 5);
        }
        private void ActionButton37_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, tc, tc.BattleActionCommand7, 6);
        }
        private void ActionButton38_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, tc, tc.BattleActionCommand8, 7);
        }
        private void ActionButton39_MouseClick(object sender, MouseEventArgs e)
        {
            ActionCommand(e, false, tc, tc.BattleActionCommand9, 8);
        }

        private void ActionCommand(MouseEventArgs e, bool detectShift, MainCharacter player, string BattleActionCommand, int number)
        {
            // �����ꂩ�̃v���C���[���s�����s���ł���ԁA���荞�݂͂ł��Ȃ��B
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (ActiveList[ii].NowExecActionFlag)
                {
                    if (BattleActionCommand == Database.DEFENSE_EN)
                    {
                        // �h�䂾���͑����K�p���\�Ƃ���B
                    }
                    else
                    {
                        return;
                    }
                }

                if (IsPlayerEnemy(ActiveList[ii]))
                {
                    if (ActiveList[ii].CurrentTimeStop > 0)
                    {
                        UpdateBattleText("���Ԓ�~���̂��߁A�s���ł��Ȃ��I�I\r\n");
                        return;
                    }
                }
            }

            if (player != null)
            {
                UpdatePlayerDeadFlag();
                if (player.Dead)
                {
                    return;
                }
            }
            if (player.CurrentFrozen > 0 && BattleActionCommand != Database.RECOVER)
            {
                return;
            }
            if (player.CurrentStunning > 0 && BattleActionCommand != Database.RECOVER)
            {
                return;
            }
            if (player.CurrentParalyze > 0 && BattleActionCommand != Database.RECOVER)
            {
                return;
            }

            if (((e != null) && (e.Button == System.Windows.Forms.MouseButtons.Right)) ||
                 (detectShift) //(e2 != null) && (e2.Shift))
                )
            {
                if (CheckBattlePlaying()) return;
                if ((player.CurrentInstantPoint < player.MaxInstantPoint) &&
                    (BattleActionCommand != Database.ARCHETYPE_EIN) &&
                    (BattleActionCommand != Database.ARCHETYPE_RANA) &&
                    (BattleActionCommand != Database.ARCHETYPE_OL) &&
                    (BattleActionCommand != Database.ARCHETYPE_VERZE))
                {
                    return;
                }
                if (this.we.AvailableInstantCommand == false) return;
                if ((BattleActionCommand == Database.ARCHETYPE_EIN) || // ���j�͈����x�����ł���
                    (BattleActionCommand == Database.ARCHETYPE_RANA) ||
                    (BattleActionCommand == Database.ARCHETYPE_OL) ||
                    (BattleActionCommand == Database.ARCHETYPE_VERZE))
                {
                    // �V���C�j���O�E�G�[�e�����ʂ����鎞�́A��񂾂��ǉ������\�ł���B
                    if (player.CurrentShiningAether > 0)
                    {
                        // �ǉ������\�̂��߁A�X���[
                    }
                    // ���j�͈����x�����ł���
                    else if (player.AlreadyPlayArchetype)
                    {
                        UpdateBattleText(player.GetCharacterSentence(204));
                        return;
                    }
                }
                if (CheckNotInstant(BattleActionCommand)) // �C���X�^���g�ł͂Ȃ��ꍇ�A�����ł��Ȃ��B
                {
                    UpdateBattleText(player.GetCharacterSentence(128));
                    return;
                }
                if (CheckInstantTarget(BattleActionCommand)) // �C���X�^���g�Ώۂ̏ꍇ
                {
                    if (this.NowStackInTheCommand)
                    {
                        // �X�^�b�N�E�C���E�U�E�R�}���h���̓C���X�^���g�ΏۂƂ��Ĕ������邽�߁A�����ł̓X���[
                        // �������A���O�ɃR�X�g����`�F�b�N������B
                        if (player.CurrentSkillPoint < TruthActionCommand.Cost(BattleActionCommand, player) &&
                            TruthActionCommand.GetAttribute(BattleActionCommand) == TruthActionCommand.Attribute.Skill)
                        {
                            if (EffectCheckDarknessCoin(player))
                            {
                                // �㏞���x���������߁A�X���[
                            }
                            else if (player.CurrentBlackContract > 0)
                            {
                                // �u���b�N�E�R���g���N�g���̓X���[
                            }
                            else
                            {
                                UpdateBattleText(player.GetCharacterSentence(0));
                                return;
                            }
                        }
                        else
                        {
                            player.CurrentSkillPoint -= TruthActionCommand.Cost(BattleActionCommand, player);
                            UpdateSkillPoint(player);
                        }
                    }
                    else
                    {
                        UpdateBattleText(player.GetCharacterSentence(167)); // �C���X�^���g�Ώۂ̏ꍇ�A�����ł��Ȃ��B
                        return;
                    }
                }

                this.currentTargetedPlayer = player;
                InstantAttackPhase(BattleActionCommand);
            }
            else if (((e != null) && (e.Button == System.Windows.Forms.MouseButtons.Left)) ||
                      (!detectShift)/*(e2 != null) && (!e2.Shift))*/
                    )
            {
                if (CheckInstantTarget(BattleActionCommand)) // �C���X�^���g�Ώۂ̏ꍇ�A�����ł��Ȃ��B
                {
                    UpdateBattleText(player.GetCharacterSentence(167));
                    return;
                }
                if ((BattleActionCommand == Database.ARCHETYPE_EIN) || // ���j�̓C���X�^���g������p�ł���B
                    (BattleActionCommand == Database.ARCHETYPE_RANA) ||
                    (BattleActionCommand == Database.ARCHETYPE_OL) ||
                    (BattleActionCommand == Database.ARCHETYPE_VERZE))
                {
                    UpdateBattleText(player.GetCharacterSentence(205));
                    return;
                }

                this.currentTargetedPlayer = player;
                this.currentTargetedPlayer.ReserveBattleCommand = BattleActionCommand;

                //if (ActionCommandAttribute.IsOwnTarget(BattleActionCommand))
                if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.Own)
                {
                    PlayerActionSet(this.currentTargetedPlayer);
                    RefreshActionIcon(this.currentTargetedPlayer);
                    // �������g���Ώۂ̏ꍇ�A�w��ΏۑI���͕s�v
                    this.currentTargetedPlayer.Target2 = this.currentTargetedPlayer;
                    this.currentTargetedPlayer.ReserveBattleCommand = String.Empty;
                }
                else if ((TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.EnemyGroup) ||
                         (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllyGroup) ||
                         (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllMember))
                {
                    PlayerActionSet(this.currentTargetedPlayer);
                    RefreshActionIcon(this.currentTargetedPlayer);
                    // �G�S���A�����S���A�G�����S�����Ώۂ̏ꍇ�A�������^�[�Q�b�g���Ȃ������͂��Ȃ��B
                    this.currentTargetedPlayer.ReserveBattleCommand = String.Empty;
                }
                else
                {
                    this.NowSelectingTarget = true;
                    this.Invalidate();
                }
            }
        }

        private bool CheckInstantTarget(string BattleActionCommand)
        {
            if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.InstantTarget)
            {
                return true;
            }
            return false;
        }

        private bool CheckNotInstant(string BattleActionCommand)
        {
            // [�x��] ����A�A�N�Z�T���͏�ɃC���X�^���g�ŗǂ��̂��H
            if (BattleActionCommand == Database.ACCESSORY_SPECIAL_EN) return false;
            if (BattleActionCommand == Database.ACCESSORY_SPECIAL2_EN) return false;
            if (BattleActionCommand == Database.WEAPON_SPECIAL_EN) return false;
            if (BattleActionCommand == Database.WEAPON_SPECIAL_LEFT_EN) return false;

            if (TruthActionCommand.GetTimingType(BattleActionCommand) != TruthActionCommand.TimingType.Instant)
            {
                return true;
            }
            return false;
        }
        private void InstantAttackPhase(string BattleActionCommand)
        {
            // �G�ΏہE�����ΏہE�����ΏہA�P��G�A�����G�A�P�ꖡ���A���������A�󋵂ɂ���Ăh�e�����g��������B
            if (this.NowSelectingTarget == false)
            {
                // ���@�E�X�L���͌Ăяo�����̖��̂����̂܂܎g���邪�A����\�͕͂��햼�ɂ���ĈقȂ邽�߁A�ȉ��̕���B
                if (BattleActionCommand == Database.WEAPON_SPECIAL_EN)
                {
                    InstantAttackSelect(this.currentTargetedPlayer.MainWeapon.Name);
                }
                else if (BattleActionCommand == Database.WEAPON_SPECIAL_LEFT_EN)
                {
                    InstantAttackSelect(this.currentTargetedPlayer.SubWeapon.Name);
                }
                else if (BattleActionCommand == Database.ACCESSORY_SPECIAL_EN)
                {
                    InstantAttackSelect(this.currentTargetedPlayer.Accessory.Name);
                }
                else if (BattleActionCommand == Database.ACCESSORY_SPECIAL2_EN)
                {
                    InstantAttackSelect(this.currentTargetedPlayer.Accessory2.Name);
                }
                else
                {
                    InstantAttackSelect(BattleActionCommand);
                }
            }
            else
            {
                MainCharacter memoTarget = null;
                if (TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.AllyOrEnemy)
                {
                    return; // �G�����I�𒆂Ɏ����I���͍s���Ȃ����߁A�������Ȃ��B
                }
                else if (TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Ally)
                {
                    memoTarget = this.currentTargetedPlayer;
                }
                else if (TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Enemy)
                {
                    memoTarget = this.currentTargetedPlayer.Target;
                }
                // �ȉ�����^�[�Q�b�g�͖������߁A�����s�v�B
                //else if (ActionCommandAttribute.IsOwnTarget(this.tempActionLabel))
                //{
                //}
                //else if (ActionCommandAttribute.IsAll(this.tempActionLabel))
                //{
                //}

                ExecActionMethod(this.currentTargetedPlayer, memoTarget, TruthActionCommand.CheckPlayerActionFromString(this.instantActionCommandString), this.instantActionCommandString);
            }
        }

        private bool UseInstantPoint(MainCharacter player)
        {
            if (player.CurrentInstantPoint <= 0)
            {
                // �C���X�^���g�|�C���g�����ɂO�̏ꍇ�A�������Ȃ�
                return false;
            }

            player.CurrentInstantPoint = 0;
            if (player.labelCurrentInstantPoint != null)
            {
                player.labelCurrentInstantPoint.Text = player.CurrentInstantPoint.ToString() + " / " + player.MaxInstantPoint;
            }
            return true;
        }

        private void UseSpecialInstant(MainCharacter player)
        {
            player.CurrentSpecialInstant = 0;
            if (player.labelCurrentSpecialInstant != null)
            {
                player.labelCurrentSpecialInstant.Text = player.CurrentSpecialInstant.ToString() + " / " + player.MaxSpecialInstant.ToString();
            }
        }

        private void CompleteInstantAction()
        {
            //this.currentTargetedPlayer.CurrentInstantPoint = 0;
            this.instantActionCommandString = String.Empty;
            this.tempTargetForInstant = null;
            this.tempTargetForTarget = null;
            this.tempTargetForTarget2 = null;
            this.NowSelectingTarget = false;
            this.Invalidate();
        }

        private void InstantAttackSelect(string BattleActionCommand)
        {
            // �������g���Ώۂ̏ꍇ�A�p�[�e�B�\���Ɋ֌W�Ȃ��A���ڎ������g��
            if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.Own)
            {
                ExecActionMethod(this.currentTargetedPlayer, this.currentTargetedPlayer, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
            }
            else
            {
                // �����P�l�̏ꍇ
                if ((we.AvailableSecondCharacter == false) && (we.AvailableThirdCharacter == false) ||
                    (this.DuelMode))
                {
                    // �G���P�l�̏ꍇ
                    if ((ec2 == null) && (ec3 == null))
                    {
                        if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.Ally)
                        {
                            ExecActionMethod(this.currentTargetedPlayer, this.currentTargetedPlayer, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                        else if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.Enemy)
                        {
                            ExecActionMethod(this.currentTargetedPlayer, ec1, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                        else if ((TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.EnemyGroup) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllyGroup) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllMember) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.InstantTarget))
                        {
                            ExecActionMethod(this.currentTargetedPlayer, null, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                        else if ((TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllyOrEnemy))
                        {
                            this.instantActionCommandString = BattleActionCommand;
                            this.NowSelectingTarget = true;
                            this.Invalidate();
                        }
                        else // ���������ĂȂ����A�A�C�e���g�p��O��Ƃ��Đ݌v����Ă���
                        {
                            ExecActionMethod(this.currentTargetedPlayer, this.currentTargetedPlayer, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                    }
                    // �G���Q�l�ȏ�i�����j�̏ꍇ
                    else
                    {
                        if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.Ally)
                        {
                            ExecActionMethod(this.currentTargetedPlayer, this.currentTargetedPlayer, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                        else if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.Enemy)
                        {
                            this.instantActionCommandString = BattleActionCommand;
                            this.NowSelectingTarget = true;
                            this.Invalidate();
                        }
                        else if ((TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.EnemyGroup) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllyGroup) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllMember) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.InstantTarget))
                        {
                            ExecActionMethod(this.currentTargetedPlayer, null, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                        else // ���������ĂȂ����A�A�C�e���g�p��O��Ƃ��Đ݌v����Ă���
                        {
                            ExecActionMethod(this.currentTargetedPlayer, null, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                    }
                }
                // �����Q�l�ȏ�i�����j�̏ꍇ
                else
                {
                    // �G���P�l�̏ꍇ
                    if ((ec2 == null) && (ec3 == null))
                    {
                        if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.Ally)
                        {
                            this.instantActionCommandString = BattleActionCommand;
                            this.NowSelectingTarget = true;
                            this.Invalidate();
                        }
                        else if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.Enemy)
                        {
                            ExecActionMethod(this.currentTargetedPlayer, ec1, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                        else if ((TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.EnemyGroup) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllyGroup) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllMember) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.InstantTarget))
                        {
                            ExecActionMethod(this.currentTargetedPlayer, null, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                        else // ���������ĂȂ����A�A�C�e���g�p��O��Ƃ��Đ݌v����Ă���
                        {
                            ExecActionMethod(this.currentTargetedPlayer, null, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                    }
                    // �G���Q�l�ȏ�i�����j�̏ꍇ
                    else
                    {
                        if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.Ally)
                        {
                            this.instantActionCommandString = BattleActionCommand;
                            this.NowSelectingTarget = true;
                            this.Invalidate();
                        }
                        else if (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.Enemy)
                        {
                            this.instantActionCommandString = BattleActionCommand;
                            this.NowSelectingTarget = true;
                            this.Invalidate();
                        }
                        else if ((TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.EnemyGroup) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllyGroup) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.AllMember) ||
                                 (TruthActionCommand.GetTargetType(BattleActionCommand) == TruthActionCommand.TargetType.InstantTarget))
                        {
                            ExecActionMethod(this.currentTargetedPlayer, null, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                        else // ���������ĂȂ����A�A�C�e���g�p��O��Ƃ��Đ݌v����Ă���
                        {
                            ExecActionMethod(this.currentTargetedPlayer, null, TruthActionCommand.CheckPlayerActionFromString(BattleActionCommand), BattleActionCommand);
                        }
                    }
                }
            }
        }

        private void ExecActionMethod(MainCharacter player, MainCharacter target, PlayerAction PA, String CommandName)
        {
            // 1. ���j�̏ꍇ�A�C���X�^���g����͂����A�X�^�b�N�����p�����ɃX�^�b�N���ڂ���B
            if ((CommandName == Database.ARCHETYPE_EIN) ||
                (CommandName == Database.ARCHETYPE_RANA) ||
                (CommandName == Database.ARCHETYPE_OL) ||
                (CommandName == Database.ARCHETYPE_VERZE)                
                )
            {
                player.StackActivePlayer = player;
                player.StackTarget = target;
                player.StackPlayerAction = PA;
                player.StackCommandString = CommandName;
                player.StackActivation = true;
                this.NowStackInTheCommand = true;
            }
            else if (IsPlayerEnemy(player) && (((TruthEnemyCharacter)player).UseStackCommand))
            {
                if (UseInstantPoint(player) == false) { return; }
                player.StackActivePlayer = player;
                player.StackTarget = target;
                player.StackPlayerAction = PA;
                player.StackCommandString = CommandName;
                player.StackActivation = true;
                this.NowStackInTheCommand = true;
            }
            else
            {
                if ((this.DuelMode) ||
                    (this.NowStackInTheCommand))
                {
                    if (UseInstantPoint(player) == false) { return; }
                    player.StackActivePlayer = player;
                    player.StackTarget = target;
                    player.StackPlayerAction = PA;
                    player.StackCommandString = CommandName;
                    player.StackActivation = true;
                    this.NowStackInTheCommand = true;
                }
                else
                {
                    if (UseInstantPoint(player) == false) { return; }
                    PlayerAttackPhase(player, target, PA, CommandName, false, false, false);
                    CompleteInstantAction();
                }
            }
        }
        
        private void TruthBattleEnemy_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int BluePenWidth = 4;
            int SkyBluePenWidth = 2;
            Pen BluePen = new Pen(mc.PlayerBattleTargetColor1, BluePenWidth);
            Pen SkyBluePen = new Pen(mc.PlayerBattleTargetColor2, SkyBluePenWidth);
            if (this.NowSelectingTarget)
            {
                if (this.currentTargetedPlayer == sc)
                {
                    BluePen = new Pen(sc.PlayerBattleTargetColor1, BluePenWidth);
                    SkyBluePen = new Pen(sc.PlayerBattleTargetColor2, SkyBluePenWidth);
                }
                if (this.currentTargetedPlayer == tc)
                {
                    BluePen = new Pen(tc.PlayerBattleTargetColor1, BluePenWidth);
                    SkyBluePen = new Pen(tc.PlayerBattleTargetColor2, SkyBluePenWidth);
                }
                int basePosX = 570; // �������̂w���C��
                int basePosX2 = 686; // �G����X���C��

                int basePosY = 165; // �����F�P�l�ڂ�Y���C��(
                int basePosY2 = 323; // �����F�Q�l�ڂ�Y���C��
                int basePosY3 = 481; // �����F�R�l�ڂ�Y���C��

                int basePosEY = 165; // �G���F�P�l�ڂ�Y���C��(
                int basePosEY2 = 323; // �G���F�Q�l�ڂ�Y���C��
                int basePosEY3 = 481; // �G���F�R�l�ڂ�Y���C��

                int len = 68; // �l�p�g�̉�����
                int len2 = 64; // �l�p�g�̏c����

                MainCharacter JudgeSourceTarget = null;
                if (this.instantActionCommandString != String.Empty)
                {
                    JudgeSourceTarget = this.tempTargetForInstant;
                }
                else
                {
                    if (TruthActionCommand.GetTargetType(this.currentTargetedPlayer.ReserveBattleCommand) == TruthActionCommand.TargetType.Ally)                        
                    {
                        JudgeSourceTarget = this.tempTargetForTarget2;
                    }
                    else if (TruthActionCommand.GetTargetType(this.currentTargetedPlayer.ReserveBattleCommand) == TruthActionCommand.TargetType.Enemy)
                    {
                        JudgeSourceTarget = this.tempTargetForTarget;
                    }
                    else
                    {
                        JudgeSourceTarget = this.tempTargetForTarget;
                    }
                }

                // �p�[�e�B�P�l�A�܂���Duel���[�h�퓬�̏ꍇ�A�����z�u�Ƃ���B
                int partyGroup = 0;
                int enemyGroup = 0;
                for (int ii = 0; ii < ActiveList.Count; ii++)
                {
                    if (ActiveList[ii].Equals(mc)) partyGroup++;
                    if (ActiveList[ii].Equals(sc) && !this.DuelMode) partyGroup++;
                    if (ActiveList[ii].Equals(tc) && !this.DuelMode) partyGroup++;

                    if (ActiveList[ii].Equals(ec1)) enemyGroup++;
                    if (ActiveList[ii].Equals(ec2) && !this.DuelMode) enemyGroup++;
                    if (ActiveList[ii].Equals(ec3) && !this.DuelMode) enemyGroup++;
                }
                if (partyGroup == 1)
                {
                    basePosY += PARTY_POSITION_1; // �P�lVS�P�l�̒ʏ�퓬���̃��C�A�E�g�ύX�����l
                }
                else if (partyGroup == 2)
                {
                    basePosY += PARTY_POSITION_21;
                    basePosY2 += PARTY_POSITION_22;
                }
                else
                {
                    // �R�l�̏ꍇ�A�f�t�H���g�f�U�C����OK
                }

                if (enemyGroup == 1)
                {
                    basePosEY += PARTY_POSITION_1;
                }
                else if (enemyGroup == 2)
                {
                    basePosEY += PARTY_POSITION_21;
                    basePosEY2 += PARTY_POSITION_22;
                }
                else
                {
                    // �R�l�̏ꍇ�A�f�t�H���g�f�U�C����OK
                }

                if (JudgeSourceTarget == ec1 && ec1 != null && ec1.Name == Database.ENEMY_JELLY_EYE_BRIGHT_RED)
                {
                    basePosEY = 89; // �{�X��A�Q�K�W�F���[�A�C�Ԃ̃��C�A�E�g�ύX�����l
                }
                if (JudgeSourceTarget == ec2 && ec2 != null && ec2.Name == Database.ENEMY_JELLY_EYE_DEEP_BLUE)
                {
                    basePosEY2 = 230; // �{�X��A�Q�K�W�F���[�A�C�̃��C�A�E�g�ύX�����l
                }


                if (JudgeSourceTarget == null)
                {
                    g.DrawRectangle(BluePen, new Rectangle(basePosX - 4, basePosY - 4, len, len));
                    g.DrawRectangle(SkyBluePen, new Rectangle(basePosX - 2, basePosY - 2, len2, len2));
                }
                else if (JudgeSourceTarget == mc)
                {
                    g.DrawRectangle(BluePen, new Rectangle(basePosX - 4, basePosY - 4, len, len));
                    g.DrawRectangle(SkyBluePen, new Rectangle(basePosX - 2, basePosY - 2, len2, len2));
                }
                else if (JudgeSourceTarget == sc)
                {
                    g.DrawRectangle(BluePen, new Rectangle(basePosX - 4, basePosY2 - 4, len, len));
                    g.DrawRectangle(SkyBluePen, new Rectangle(basePosX - 2, basePosY2 - 2, len2, len2));
                }
                else if (JudgeSourceTarget == tc)
                {
                    g.DrawRectangle(BluePen, new Rectangle(basePosX - 4, basePosY3 - 4, len, len));
                    g.DrawRectangle(SkyBluePen, new Rectangle(basePosX - 2, basePosY3 - 2, len2, len2));
                }
                else if (JudgeSourceTarget == ec1)
                {
                    g.DrawRectangle(BluePen, new Rectangle(basePosX2 - 4, basePosEY - 4, len, len));
                    g.DrawRectangle(SkyBluePen, new Rectangle(basePosX2 - 2, basePosEY - 2, len2, len2));
                }
                else if (JudgeSourceTarget == ec2)
                {
                    g.DrawRectangle(BluePen, new Rectangle(basePosX2 - 4, basePosEY2 - 4, len, len));
                    g.DrawRectangle(SkyBluePen, new Rectangle(basePosX2 - 2, basePosEY2 - 2, len2, len2));
                }
                else if (JudgeSourceTarget == ec3)
                {
                    g.DrawRectangle(BluePen, new Rectangle(basePosX2 - 4, basePosEY3 - 4, len, len));
                    g.DrawRectangle(SkyBluePen, new Rectangle(basePosX2 - 2, basePosEY3 - 2, len2, len2));
                }
            }

            if (this.DuelMode == false)
            {
                MainCharacter target = null;
                for (int ii = 0; ii < ActiveList.Count; ii++)
                {
                    if (ActiveList[ii] != null && ActiveList[ii].Target != null)
                    {
                        target = ActiveList[ii].Target;
                        Pen currentPen1 = null;
                        Pen currentPen2 = null;
                        Point srcOffset = Point.Empty;
                        Point dstOffset = Point.Empty;
                        int adjustY = 15;
                        if (ActiveList[ii] == mc)
                        {
                            currentPen1 = new Pen(Brushes.DarkBlue, 2);
                            currentPen2 = new Pen(Brushes.DarkBlue, 1);
                            srcOffset = new Point(ActiveList[ii].MainObjectButton.Location.X + ActiveList[ii].MainObjectButton.Width, ActiveList[ii].MainObjectButton.Location.Y + ActiveList[ii].MainObjectButton.Height / 2 - adjustY);
                            dstOffset = new Point(target.MainObjectButton.Location.X, target.MainObjectButton.Location.Y + target.MainObjectButton.Height / 2 - adjustY);
                        }
                        else if (ActiveList[ii] == sc)
                        {
                            currentPen1 = new Pen(Brushes.Pink, 2);
                            currentPen2 = new Pen(Brushes.Pink, 1);
                            srcOffset = new Point(ActiveList[ii].MainObjectButton.Location.X + ActiveList[ii].MainObjectButton.Width, ActiveList[ii].MainObjectButton.Location.Y + ActiveList[ii].MainObjectButton.Height / 2 - adjustY);
                            dstOffset = new Point(target.MainObjectButton.Location.X, target.MainObjectButton.Location.Y + target.MainObjectButton.Height / 2 - adjustY);
                        }
                        else if (ActiveList[ii] == tc)
                        {
                            currentPen1 = new Pen(Brushes.Gold, 2);
                            currentPen2 = new Pen(Brushes.Gold, 1);
                            srcOffset = new Point(ActiveList[ii].MainObjectButton.Location.X + ActiveList[ii].MainObjectButton.Width, ActiveList[ii].MainObjectButton.Location.Y + ActiveList[ii].MainObjectButton.Height / 2 - adjustY);
                            dstOffset = new Point(target.MainObjectButton.Location.X, target.MainObjectButton.Location.Y + target.MainObjectButton.Height / 2 - adjustY);
                        }
                        else if (ActiveList[ii] == ec1)
                        {
                            currentPen1 = new Pen(Brushes.DarkRed, 2);
                            currentPen2 = new Pen(Brushes.DarkRed, 1);
                            srcOffset = new Point(ActiveList[ii].MainObjectButton.Location.X, ActiveList[ii].MainObjectButton.Location.Y + ActiveList[ii].MainObjectButton.Height / 2 + adjustY);
                            dstOffset = new Point(target.MainObjectButton.Location.X + target.MainObjectButton.Width, target.MainObjectButton.Location.Y + target.MainObjectButton.Height / 2 + adjustY);
                        }
                        else if (ActiveList[ii] == ec2)
                        {
                            currentPen1 = new Pen(Brushes.DarkGoldenrod, 2);
                            currentPen2 = new Pen(Brushes.DarkGoldenrod, 1);
                            srcOffset = new Point(ActiveList[ii].MainObjectButton.Location.X, ActiveList[ii].MainObjectButton.Location.Y + ActiveList[ii].MainObjectButton.Height / 2 + adjustY);
                            dstOffset = new Point(target.MainObjectButton.Location.X + target.MainObjectButton.Width, target.MainObjectButton.Location.Y + target.MainObjectButton.Height / 2 + adjustY);
                        }
                        else if (ActiveList[ii] == ec3)
                        {
                            currentPen1 = new Pen(Brushes.DarkOliveGreen, 2);
                            currentPen2 = new Pen(Brushes.DarkOliveGreen, 1);
                            srcOffset = new Point(ActiveList[ii].MainObjectButton.Location.X, ActiveList[ii].MainObjectButton.Location.Y + ActiveList[ii].MainObjectButton.Height / 2 + adjustY);
                            dstOffset = new Point(target.MainObjectButton.Location.X + target.MainObjectButton.Width, target.MainObjectButton.Location.Y + target.MainObjectButton.Height / 2 + adjustY);
                        }

                        g.DrawLine(currentPen1, new Point(srcOffset.X, srcOffset.Y), new Point(dstOffset.X, dstOffset.Y));

                        //for (int ii = 0; ii < 5; ii++)
                        //{
                        //    g.DrawLine(BluePen2, new Point(target.MainObjectButton.Location.X - ii, target.MainObjectButton.Location.Y + target.MainObjectButton.Height / 2), new Point(target.MainObjectButton.Location.X - 6 + ii, target.MainObjectButton.Location.Y + target.MainObjectButton.Height / 2 - 6 + ii));
                        //    g.DrawLine(BluePen2, new Point(target.MainObjectButton.Location.X - ii, target.MainObjectButton.Location.Y + target.MainObjectButton.Height / 2), new Point(target.MainObjectButton.Location.X - 6 + ii, target.MainObjectButton.Location.Y + target.MainObjectButton.Height / 2 + 6 - ii));
                        //    g.DrawLine(BluePen2, new Point(target.MainObjectButton.Location.X - 6 + ii, target.MainObjectButton.Location.Y + target.MainObjectButton.Height / 2 - 6 + ii), new Point(target.MainObjectButton.Location.X - 6 + ii, target.MainObjectButton.Location.Y + target.MainObjectButton.Height / 2 + 6 - ii));
                        //}
                    }
                }
            }
        }

        private void buttonTargetPlayer_Click(object sender, EventArgs e)
        { 
            if (this.NowSelectingTarget)
            {
                if ((this.instantActionCommandString != String.Empty))// && (this.currentTargetedPlayer.StackActivePlayer == null))
                {
                    for (int ii = 0; ii < ActiveList.Count; ii++)
                    {
                        if (((PictureBox)sender).Equals(ActiveList[ii].MainObjectButton)) { this.tempTargetForInstant = ActiveList[ii]; }
                    }

                    if ((TruthActionCommand.CheckPlayerActionFromString(this.instantActionCommandString) == PlayerAction.UseSpell) && TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Enemy)
                    {
                        UpdateBattleText(this.currentTargetedPlayer.GetCharacterSentence(4074));
                        return;
                    }
                    else if ((TruthActionCommand.CheckPlayerActionFromString(this.instantActionCommandString) == PlayerAction.UseSkill) && TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Enemy)
                    {
                        UpdateBattleText(this.currentTargetedPlayer.GetCharacterSentence(4074));
                        return;
                    }
                    else if ((TruthActionCommand.CheckPlayerActionFromString(this.instantActionCommandString) == PlayerAction.Archetype) && TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Enemy)
                    {
                        UpdateBattleText(this.currentTargetedPlayer.GetCharacterSentence(4074));
                        return;
                    }
                    else if ((TruthActionCommand.CheckPlayerActionFromString(this.instantActionCommandString) == PlayerAction.NormalAttack))
                    {
                        UpdateBattleText(this.currentTargetedPlayer.GetCharacterSentence(4076));
                        return;
                    }                   
                    ExecActionMethod(this.currentTargetedPlayer, this.tempTargetForInstant, TruthActionCommand.CheckPlayerActionFromString(this.instantActionCommandString), this.instantActionCommandString);
                }
                else
                {
                    MainCharacter memoTarget = null;
                    for (int ii = 0; ii < ActiveList.Count; ii++)
                    {
                        if (((PictureBox)sender).Equals(ActiveList[ii].MainObjectButton)) { memoTarget = ActiveList[ii]; }
                    }

                    if (TruthActionCommand.GetTargetType(this.currentTargetedPlayer.ReserveBattleCommand) == TruthActionCommand.TargetType.Enemy)
                    {
                        UpdateBattleText(this.currentTargetedPlayer.GetCharacterSentence(4074));
                        return;
                    }

                    if ((TruthActionCommand.GetTargetType(this.currentTargetedPlayer.ReserveBattleCommand) == TruthActionCommand.TargetType.Ally) ||
                        (TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Ally))
                    {
                        this.currentTargetedPlayer.Target2 = memoTarget;
                    }
                    else
                    {
                        this.currentTargetedPlayer.Target = memoTarget;
                    }
                    PlayerActionSet(this.currentTargetedPlayer);
                    RefreshActionIcon(this.currentTargetedPlayer);
                }
                this.currentTargetedPlayer.ReserveBattleCommand = String.Empty;
                this.NowSelectingTarget = false;
                this.Invalidate();
            }
        }

        private void buttonTargetPlayer1_Click(object sender, EventArgs e)
        {
            buttonTargetPlayer_Click(sender, e);
        }

        private void buttonTargetPlayer2_Click(object sender, EventArgs e)
        {
            buttonTargetPlayer_Click(sender, e);
        }

        private void buttonTargetPlayer3_Click(object sender, EventArgs e)
        {
            buttonTargetPlayer_Click(sender, e);
        }

        private void buttonTargetEnemy_Click(object sender, EventArgs e)
        {
            if (this.NowSelectingTarget)
            {
                if ((this.instantActionCommandString != String.Empty))// && (this.currentTargetedPlayer.StackActivePlayer == null))
                {
                    for (int ii = 0; ii < ActiveList.Count; ii++)
                    {
                        if (((PictureBox)sender).Equals(ActiveList[ii].MainObjectButton)) { this.tempTargetForInstant = ActiveList[ii]; }
                    }

                    if ((TruthActionCommand.CheckPlayerActionFromString(this.instantActionCommandString) == PlayerAction.UseSpell) && TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Ally)
                    {
                        UpdateBattleText(this.currentTargetedPlayer.GetCharacterSentence(4072));
                        return;
                    }
                    else if ((TruthActionCommand.CheckPlayerActionFromString(this.instantActionCommandString) == PlayerAction.UseSkill) && TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Ally)
                    {
                        UpdateBattleText(this.currentTargetedPlayer.GetCharacterSentence(4072));
                        return;
                    }
                    else if ((TruthActionCommand.CheckPlayerActionFromString(this.instantActionCommandString) == PlayerAction.Archetype) && TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Ally)
                    {
                        UpdateBattleText(this.currentTargetedPlayer.GetCharacterSentence(4072));
                        return;
                    }
                    ExecActionMethod(this.currentTargetedPlayer, this.tempTargetForInstant, TruthActionCommand.CheckPlayerActionFromString(this.instantActionCommandString), this.instantActionCommandString);
                }
                else
                {
                    MainCharacter memoTarget = null;
                    for (int ii = 0; ii < ActiveList.Count; ii++)
                    {
                        if (((PictureBox)sender).Equals(ActiveList[ii].MainObjectButton)) { memoTarget = ActiveList[ii]; }
                    }

                    if (TruthActionCommand.GetTargetType(this.currentTargetedPlayer.ReserveBattleCommand) == TruthActionCommand.TargetType.Ally)
                    {
                        UpdateBattleText(this.currentTargetedPlayer.GetCharacterSentence(4072));
                        return;
                    }

                    if ((TruthActionCommand.GetTargetType(this.currentTargetedPlayer.ReserveBattleCommand) == TruthActionCommand.TargetType.Ally) ||
                        (TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Ally))
                    {
                        this.currentTargetedPlayer.Target2 = memoTarget;
                    }
                    else
                    {
                        this.currentTargetedPlayer.Target = memoTarget;
                    }
                    PlayerActionSet(this.currentTargetedPlayer);
                    RefreshActionIcon(this.currentTargetedPlayer);
                }
                this.currentTargetedPlayer.ReserveBattleCommand = String.Empty;
                this.NowSelectingTarget = false;
                this.Invalidate();
            }
        }

        private void buttonTargetEnemy1_Click(object sender, EventArgs e)
        {
            buttonTargetEnemy_Click(sender, e);
        }

        private void buttonTargetEnemy2_Click(object sender, EventArgs e)
        {
            buttonTargetEnemy_Click(sender, e);
        }

        private void buttonTargetEnemy3_Click(object sender, EventArgs e)
        {
            buttonTargetEnemy_Click(sender, e);
        }

        private void buttonTargetPlayer1_MouseEnter(object sender, EventArgs e)
        {
            if (this.NowSelectingTarget)
            {
                if (this.instantActionCommandString != string.Empty)
                {
                    this.tempTargetForInstant = mc;
                }
                else
                {
                    if ((TruthActionCommand.GetTargetType(this.currentTargetedPlayer.ReserveBattleCommand) == TruthActionCommand.TargetType.Ally) ||
                        (TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Ally))
                    {
                        this.tempTargetForTarget2 = mc;
                    }
                    else
                    {
                        this.tempTargetForTarget = mc;
                    }
                }

                this.Invalidate();
            }
        }

        private void buttonTargetPlayer2_MouseEnter(object sender, EventArgs e)
        {
            if (this.NowSelectingTarget)
            {
                if (this.instantActionCommandString != string.Empty)
                {
                    this.tempTargetForInstant = sc;
                }
                else
                {
                    if ((TruthActionCommand.GetTargetType(this.currentTargetedPlayer.ReserveBattleCommand) == TruthActionCommand.TargetType.Ally) ||
                        (TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Ally))
                    {
                        this.tempTargetForTarget2 = sc;
                    }
                    else
                    {
                        this.tempTargetForTarget = sc;
                    }
                }
                this.Invalidate();
            }
        }

        private void buttonTargetPlayer3_MouseEnter(object sender, EventArgs e)
        {
            if (this.NowSelectingTarget)
            {
                if (this.instantActionCommandString != string.Empty)
                {
                    this.tempTargetForInstant = tc;
                }
                else
                {
                    if ((TruthActionCommand.GetTargetType(this.currentTargetedPlayer.ReserveBattleCommand) == TruthActionCommand.TargetType.Ally) ||
                        (TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Ally))
                    {
                        this.tempTargetForTarget2 = tc;
                    }
                    else
                    {
                        this.tempTargetForTarget = tc;
                    }
                }
                this.Invalidate();
            }
        }

        private void buttonTargetEnemy1_MouseEnter(object sender, EventArgs e)
        {
            if (this.NowSelectingTarget)
            {
                if (this.instantActionCommandString != string.Empty)
                {
                    this.tempTargetForInstant = ec1;
                }
                else
                {
                    if ((TruthActionCommand.GetTargetType(this.currentTargetedPlayer.ReserveBattleCommand) == TruthActionCommand.TargetType.Ally) ||
                        (TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Ally))
                    {
                        this.tempTargetForTarget2 = ec1;
                    }
                    else
                    {
                        this.tempTargetForTarget = ec1;
                    }
                }
                this.Invalidate();
            }
        }

        private void buttonTargetEnemy2_MouseEnter(object sender, EventArgs e)
        {
            if (this.NowSelectingTarget)
            {
                if (this.instantActionCommandString != string.Empty)
                {
                    this.tempTargetForInstant = ec2;
                }
                else
                {
                    if ((TruthActionCommand.GetTargetType(this.currentTargetedPlayer.ReserveBattleCommand) == TruthActionCommand.TargetType.Ally) ||
                        (TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Ally))
                    {
                        this.tempTargetForTarget2 = ec2;
                    }
                    else
                    {
                        this.tempTargetForTarget = ec2;
                    }
                }
                this.Invalidate();
            }
        }

        private void buttonTargetEnemy3_MouseEnter(object sender, EventArgs e)
        {
            if (this.NowSelectingTarget)
            {
                if (this.instantActionCommandString != string.Empty)
                {
                    this.tempTargetForInstant = ec3;
                }
                else
                {
                    if ((TruthActionCommand.GetTargetType(this.currentTargetedPlayer.ReserveBattleCommand) == TruthActionCommand.TargetType.Ally) ||
                        (TruthActionCommand.GetTargetType(this.instantActionCommandString) == TruthActionCommand.TargetType.Ally))
                    {
                        this.tempTargetForTarget2 = ec3;
                    }
                    else
                    {
                        this.tempTargetForTarget = ec3;
                    }
                }
                this.Invalidate();
            }
        }

        private void TruthBattleEnemy_Shown(object sender, EventArgs e)
        {
            if (this.ec1 != null)
            {
                // ���F���[�ŏI�퓬�Q
                if (this.ec1.Name == Database.ENEMY_LAST_SIN_VERZE_ARTIE)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM22, Database.BGM23, Database.BGM23LoopBegin);
                }
                // ���F���[�ŏI�퓬
                else if (this.ec1.Name == Database.ENEMY_LAST_VERZE_ARTIE)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM22, Database.BGM23, Database.BGM23LoopBegin);
                }
                // DUEL�ŏI��
                else if (this.ec1.Name == Database.ENEMY_LAST_RANA_AMILIA ||
                         this.ec1.Name == Database.ENEMY_LAST_SINIKIA_KAHLHANZ ||
                         this.ec1.Name == Database.ENEMY_LAST_OL_LANDIS)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM21, Database.BGM21LoopBegin);
                }
                // �{�X
                else if ((this.ec1.Name == Database.ENEMY_BOSS_KARAMITUKU_FLANSIS) ||
                    (this.ec1.Name == Database.ENEMY_BRILLIANT_SEA_PRINCE) ||
                    (this.ec1.Name == Database.ENEMY_ORIGIN_STAR_CORAL_QUEEN) ||
                    (this.ec1.Name == Database.ENEMY_JELLY_EYE_BRIGHT_RED) ||
                    (this.ec1.Name == Database.ENEMY_JELLY_EYE_DEEP_BLUE) ||
                    (this.ec1.Name == Database.ENEMY_SEA_STAR_KNIGHT_AEGIRU) ||
                    (this.ec1.Name == Database.ENEMY_SEA_STAR_KNIGHT_AMARA) ||
                    (this.ec1.Name == Database.ENEMY_SEA_STAR_ORIGIN_KING) ||
                    (this.ec1.Name == Database.ENEMY_BOSS_HOWLING_SEIZER) ||
                    (this.ec1.Name == Database.ENEMY_BOSS_LEGIN_ARZE_1) ||
                    (this.ec1.Name == Database.ENEMY_BOSS_LEGIN_ARZE_2) ||
                    (this.ec1.Name == Database.ENEMY_BOSS_LEGIN_ARZE_3)
                    )
                {
                    GroundOne.PlayDungeonMusic(Database.BGM04, Database.BGM04LoopBegin);
                }
                // ����DUEL�I���E�����f�B�X
                else if (this.ec1.Name == Database.DUEL_OL_LANDIS)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM17, Database.BGM17LoopBegin);
                }
                // �x�z���B�̌Ăѐ�
                else if (this.ec1.Rare == TruthEnemyCharacter.RareString.Purple)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM18, Database.BGM18LoopBegin);
                }
                // �ŏI�{�X�F�x�z��
                else if (this.ec1.Name == Database.ENEMY_BOSS_BYSTANDER_EMPTINESS)
                {
                    GroundOne.PlayDungeonMusic(Database.BGM05, Database.BGM05LoopBegin);
                }
                // �ʏ�o�g��
                else
                {
                    GroundOne.PlayDungeonMusic(Database.BGM03, Database.BGM03LoopBegin);
                }
            }
            // ������A�G�̏�񂪎擾�ł��Ȃ��ꍇ�A�ʏ�o�g��
            else
            {
                GroundOne.PlayDungeonMusic(Database.BGM03, Database.BGM03LoopBegin);
            }

            this.ActionButton11.Select();
            keyTh = new Thread(new System.Threading.ThreadStart(DetectKeyPressed));
            keyTh.IsBackground = true;
            keyTh.Start();
        }

        /// <summary>
        /// ���ُ�Ԃŉr���s�\���ǂ����𔻒肷��`�F�b�N���\�b�h
        /// </summary>
        /// <returns>False�F�J�E���^�[�����ŃX���[�@true�F�J�E���^�[���肠��</returns>
        private bool CheckSilence(MainCharacter player)
        {
            if (player.CurrentSilence > 0)
            {
                this.Invoke(new _AnimationDamage(AnimationDamage), 0, player, 0, Color.Black, false, false, Database.MISS_SPELL);
                return true;
            }
            return false;
        }

        private bool CheckCancelSpell(MainCharacter player, string currentSpellName)
        {
            List<MainCharacter> group = new List<MainCharacter>();
            if ((mc != null) && (!mc.Dead)) { group.Add(mc); }
            if ((sc != null) && (!sc.Dead)) { group.Add(sc); }
            if ((tc != null) && (!tc.Dead)) { group.Add(tc); }
            if ((ec1 != null) && (!ec1.Dead)) { group.Add(ec1); }
            if ((ec2 != null) && (!ec2.Dead)) { group.Add(ec2); }
            if ((ec3 != null) && (!ec3.Dead)) { group.Add(ec3); }

            for (int ii = 0; ii < group.Count; ii++)
            {
                if ((group[ii].Accessory != null) && (group[ii].Accessory.Name == Database.COMMON_DEVIL_SEALED_VASE) && (group[ii].Accessory.ImprintCommand == currentSpellName) ||
                    (group[ii].Accessory2 != null) && (group[ii].Accessory2.Name == Database.COMMON_DEVIL_SEALED_VASE) && (group[ii].Accessory2.ImprintCommand == currentSpellName))
                {
                    this.Invoke(new _AnimationDamage(AnimationDamage), 0, player, 0, Color.Black, false, false, Database.MISS_SPELL);
                    return true;
                }
            }
            return false;
        }


        /// <summary>�C���X�^���g�s���J�E���^�[FutureVision�̃`�F�b�N���\�b�h</summary> 
        /// <returns>False�F�J�E���^�[�����ŃX���[�@true�F�J�E���^�[���肠��</returns>
        private bool CheckFutureVision(MainCharacter player)
        {
            return false;
        }
        /// <summary>
        /// �C���X�^���g�s���J�E���^�[StanceOfSuddenness�̃`�F�b�N���\�b�h</summary>
        /// <returns>False�F�J�E���^�[�����ŃX���[�@true�F�J�E���^�[���肠��</returns>
        private bool CheckStanceOfSuddenness(MainCharacter player)
        {
            return false;
        }

        /// <summary>
        /// ��_���[�W�n�C���X�^���g�s���J�E���^�[DeepMirror�̃`�F�b�N���\�b�h</summary>
        /// <returns>False�F�J�E���^�[�����ŃX���[�@true�F�J�E���^�[���肠��</returns>
        private bool CheckDeepMirror(MainCharacter player)
        {
            return false;
        }

        /// <summary>�_���[�W�n�C���X�^���g�J�E���^�[StanceOfMystic�̃`�F�b�N���\�b�h</summary>
        /// <returns>False�F�J�E���^�[�����ŃX���[�@true�F�J�E���^�[���肠��</returns>
        private bool CheckStanceOfMystic(MainCharacter player)
        {
            return false;
        }

        /// <summary>���@�E�X�L���J�E���^�[HymnContract�̃`�F�b�N���\�b�h</summary>
        /// <returns>False�F�J�E���^�[�����ŃX���[�@true�F�J�E���^�[���肠��</returns>
        private bool CheckHymnContract(MainCharacter player)
        {
            return false;
        }

        /// <summary>
        /// ���@�E�X�L���J�E���^�[StanceOfEyes�̃`�F�b�N���\�b�h</summary>
        /// <returns>False�F�J�E���^�[�����ŃX���[�@true�F�J�E���^�[���肠��</returns>
        private bool CheckStanceOfEyes(MainCharacter player)
        {
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (DetectOpponentParty(player, ActiveList[ii]))
                {
                    if (ActiveList[ii].CurrentStanceOfEyes > 0)
                    {
                        ActiveList[ii].RemoveStanceOfEyes(); // �J�E���^�[�����^���s�Ɍ��炸�A��x�`�F�b�N��������������������̂Ƃ���i�P�^�[���ŉ��x����������̂͋����������߁j
                        if (JudgeSuccessOfCounter(player, ActiveList[ii], 101))
                        {
                            return true; // �J�E���^�[����
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// �X�y���J�E���^�[Negate�̃`�F�b�N���\�b�h</summary>
        /// <returns>False�F�J�E���^�[�����ŃX���[�@true�F�J�E���^�[���肠��</returns>
        private bool CheckNegateCounter(MainCharacter player)
        {
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (DetectOpponentParty(player, ActiveList[ii]))
                {
                    if (ActiveList[ii].CurrentNegate > 0)
                    {
                        ActiveList[ii].RemoveNegate(); // �J�E���^�[�����^���s�Ɍ��炸�A��x�`�F�b�N��������������������̂Ƃ���i�P�^�[���ŉ��x����������̂͋����������߁j
                        if (JudgeSuccessOfCounter(player, ActiveList[ii], 104))
                        {
                            return true; // �J�E���^�[����
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// �����U���J�E���^�[CounterAttack�̃`�F�b�N���\�b�h</summary>
        /// <returns>False�F�J�E���^�[�����ŃX���[�@true�F�J�E���^�[���肠��</returns>
        private bool CheckCounterAttack(MainCharacter player, string CurrentSkillName)
        {
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (DetectOpponentParty(player, ActiveList[ii]))
                {
                    if (ActiveList[ii].CurrentCounterAttack > 0)
                    {
                        if (TruthActionCommand.IsDamage(CurrentSkillName))
                        {
                            ActiveList[ii].RemoveCounterAttack();
                            if (JudgeSuccessOfCounter(player, ActiveList[ii], 113))
                            {
                                // [�x��] �J�E���^�[������Ń_���[�W��^����̂͂������Ȃ��̂��E�E�E
                                // �������A�����ɓ���Ă����B
                                PlayerNormalAttack(ActiveList[ii], player, 0, false, false);
                                return true; // �J�E���^�[����
                            }
                        }
                    }
                }
            }
            return false;
        }
        


        /// <summary>
        /// �J�E���^�[�s�ׂ������������邩�ǂ����𔻒肷�郁�\�b�h
        /// </summary>
        /// <param name="player">�Ώی��v���C���[</param>
        /// <param name="target">�Ώې�^�[�Q�b�g</param>
        /// <param name="messageNumber">�L�����N�^�[�Z���t�ԍ�</param>
        /// <returns>�J�E���^�[�����Ȃ�True�A���s�Ȃ�False</returns>
        private bool JudgeSuccessOfCounter(MainCharacter player, MainCharacter target, int messageNumber)
        {
            if (target.CurrentNothingOfNothingness > 0)
            {
                UpdateBattleText("�������A" + player.Name + "�͖������𖳌��ɂ���I�[���ɂ���Č���Ă���I\r\n");
                this.Invoke(new _AnimationDamage(AnimationDamage), 0, target, 0, Color.Black, true, false, Database.FAIL_COUNTER);
                return false;
            }
            else if (player.CurrentHymnContract > 0)
            {
                UpdateBattleText(player.Name + "�͓V�g�̌_��ɂ��ی삳��Ă���A�J�E���^�[�𖳎������I\r\n");
                this.Invoke(new _AnimationDamage(AnimationDamage), 0, target, 0, Color.Black, true, false, Database.FAIL_COUNTER);
                return false;
            }
            else if (player.PA == PlayerAction.UseSpell && (TruthActionCommand.CantBeCountered(player.CurrentSpellName)) ||
                     player.StackPlayerAction == PlayerAction.UseSpell && (TruthActionCommand.CantBeCountered(player.StackCommandString)))
            {
                UpdateBattleText(player.CurrentSpellName + "�̓J�E���^�[�o���Ȃ��I�I�I\r\n");
                this.Invoke(new _AnimationDamage(AnimationDamage), 0, target, 0, Color.Black, true, false, Database.FAIL_COUNTER);
                return false;
            }
            else if (player.PA == PlayerAction.UseSkill && (TruthActionCommand.CantBeCountered(player.CurrentSkillName)) ||
                     player.StackPlayerAction == PlayerAction.UseSkill && (TruthActionCommand.CantBeCountered(player.StackCommandString)))
            {
                UpdateBattleText(player.CurrentSkillName + "�̓J�E���^�[�o���Ȃ��I�I�I\r\n");
                this.Invoke(new _AnimationDamage(AnimationDamage), 0, target, 0, Color.Black, true, false, Database.FAIL_COUNTER);
                return false;
            }
            else if (player.PA == PlayerAction.Archetype && (TruthActionCommand.CantBeCountered(player.CurrentArchetypeName)) ||
                player.StackPlayerAction == PlayerAction.Archetype && (TruthActionCommand.CantBeCountered(player.StackCommandString)))
            {
                UpdateBattleText(player.CurrentArchetypeName + "�̓J�E���^�[�o���Ȃ��I�I�I\r\n");
                this.Invoke(new _AnimationDamage(AnimationDamage), 0, target, 0, Color.Black, true, false, Database.FAIL_COUNTER);
                return false;
            }
            else
            {
                UpdateBattleText(target.GetCharacterSentence(messageNumber));
                this.Invoke(new _AnimationDamage(AnimationDamage), 0, target, 0, Color.Black, true, false, Database.SUCCESS_COUNTER);
                return true;
            }
        }

        private bool IsPlayerEnemy(MainCharacter player)
        {
            if (player == null) { return false; }
            if ((player == ec1) || (player == ec2) || (player == ec3))
            {
                return true;
            }
            return false;
        }

        private bool IsPlayerAlly(MainCharacter player)
        {
            if (player == null) { return false; }
            if ((player == mc) || (player == sc) || (player == tc))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// �v���C���[�ƃ^�[�Q�b�g�̓p�[�e�B�����o�[���G�����o�[���𔻕ʂ��郁�\�b�h
        /// </summary>
        /// <param name="player">�Ώی��v���C���[</param>
        /// <param name="target">�Ώې�^�[�Q�b�g</param>
        /// <returns>�G�����o�[�Ȃ�True�A���������o�[�Ȃ�False</returns>
        private bool DetectOpponentParty(MainCharacter player, MainCharacter target)
        {
            // �ǐ����A���Z�_���W����d�������L�q
            if (((player == ec1 || player == ec2 || player == ec3) &&
                 (target == mc  || target == sc  || target == tc )) 
                 ||
                 ((player == mc  || player == sc  || player == tc) &&
                  (target == ec1 || target == ec2 || target == ec3)))
            {
                return true;
            }
            return false;
        }

        private void UseItemButton_Click(object sender, EventArgs e)
        {
            if (UseItemGauge.Width < 600)
            {
                if (mc.Dead == false)
                {
                    UpdateBattleText(mc.GetCharacterSentence(125));
                }
                else if (we.AvailableSecondCharacter && sc.Dead == false)
                {
                    UpdateBattleText(sc.GetCharacterSentence(125));
                }
                else if (we.AvailableThirdCharacter && tc.Dead == false)
                {
                    UpdateBattleText(tc.GetCharacterSentence(125));
                }
                return;
            }

            using (TruthStatusPlayer TSP = new TruthStatusPlayer())
            {
                TSP.WE = we;
                TSP.MC = mc;
                TSP.SC = sc;
                TSP.TC = tc;
                TSP.StartPosition = FormStartPosition.CenterParent;
                TSP.OnlyUseItem = true;
                TSP.DuelMode = this.DuelMode;
                if (mc.Dead == false)
                {
                    TSP.CurrentStatusView = Color.LightSkyBlue;
                }
                else if (we.AvailableSecondCharacter && sc.Dead == false)
                {
                    TSP.CurrentStatusView = Color.Pink;
                }
                else if (we.AvailableThirdCharacter && tc.Dead == false)
                {
                    TSP.CurrentStatusView = Color.Gold;
                }
                TSP.ShowDialog();

                if (TSP.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    if (we.AvailableFirstCharacter)
                    {
                        mc = TSP.MC;
                        UpdateLife(mc, 0, false, false, 0, false);
                        UpdateSkillPoint(mc, 0, false, false, 0);
                        UpdateMana(mc, 0, false, false, 0);
                    }
                    if (we.AvailableSecondCharacter && this.DuelMode == false)
                    {
                        sc = TSP.SC;
                        UpdateLife(sc, 0, false, false, 0, false);
                        UpdateSkillPoint(sc, 0, false, false, 0);
                        UpdateMana(sc, 0, false, false, 0);
                    }
                    if (we.AvailableThirdCharacter && this.DuelMode == false)
                    {
                        tc = TSP.TC;
                        UpdateLife(tc, 0, false, false, 0, false);
                        UpdateSkillPoint(tc, 0, false, false, 0);
                        UpdateMana(tc, 0, false, false, 0);
                    }

                    UseItemGauge.Width = 0;
                }
            }
        }

        private void BattleSettingButton_Click(object sender, EventArgs e)
        {
            using (TruthBattleSetting TBS = new TruthBattleSetting())
            {
                TBS.Opacity = 0.95f;
                TBS.StartPosition = FormStartPosition.CenterParent;
                TBS.MC = this.mc;
                TBS.SC = this.sc;
                TBS.TC = this.tc;
                TBS.WE = this.we;
                TBS.DuelMode = this.DuelMode;
                TBS.ShowDialog();
                if (TBS.DialogResult == System.Windows.Forms.DialogResult.OK)
                {
                    this.mc = TBS.MC;
                    this.sc = TBS.SC;
                    this.tc = TBS.TC;
                    this.we = TBS.WE;
                }
            }
            UpdateBattleCommandSetting(mc, mc.ActionButton1, mc.ActionButton2, mc.ActionButton3, mc.ActionButton4, mc.ActionButton5, mc.ActionButton6, mc.ActionButton7, mc.ActionButton8, mc.ActionButton9,
                                           mc.IsSorceryMark1, mc.IsSorceryMark2, mc.IsSorceryMark3, mc.IsSorceryMark4, mc.IsSorceryMark5, mc.IsSorceryMark6, mc.IsSorceryMark7, mc.IsSorceryMark8, mc.IsSorceryMark9);
            if (we.AvailableSecondCharacter && this.DuelMode == false)
            {
                UpdateBattleCommandSetting(sc, sc.ActionButton1, sc.ActionButton2, sc.ActionButton3, sc.ActionButton4, sc.ActionButton5, sc.ActionButton6, sc.ActionButton7, sc.ActionButton8, sc.ActionButton9,
                                               sc.IsSorceryMark1, sc.IsSorceryMark2, sc.IsSorceryMark3, sc.IsSorceryMark4, sc.IsSorceryMark5, sc.IsSorceryMark6, sc.IsSorceryMark7, sc.IsSorceryMark8, sc.IsSorceryMark9);
            }
            if (we.AvailableThirdCharacter && this.DuelMode == false)
            {
                UpdateBattleCommandSetting(tc, tc.ActionButton1, tc.ActionButton2, tc.ActionButton3, tc.ActionButton4, tc.ActionButton5, tc.ActionButton6, tc.ActionButton7, tc.ActionButton8, tc.ActionButton9,
                                               tc.IsSorceryMark1, tc.IsSorceryMark2, tc.IsSorceryMark3, tc.IsSorceryMark4, tc.IsSorceryMark5, tc.IsSorceryMark6, tc.IsSorceryMark7, tc.IsSorceryMark8, tc.IsSorceryMark9);
            }
        }

        private void RunAwayButton_Click(object sender, EventArgs e)
        {
            if (this.cannotRunAway)
            {
                txtBattleMessage.Text = txtBattleMessage.Text.Insert(0, "�A�C���͍��������Ȃ���Ԃɋ���B\r\n");
                return;
            }

            if (th != null)
            {
                tempStopFlag = true;
                endFlag = true;
            }
            else
            {
                if (this.DuelMode)
                {
                    txtBattleMessage.Text = txtBattleMessage.Text.Insert(0, "�A�C���͍~�Q��錾�����B\r\n");
                }
                else
                {
                    txtBattleMessage.Text = txtBattleMessage.Text.Insert(0, "�A�C���͓����o�����B\r\n");
                }

                txtBattleMessage.Update();
                System.Threading.Thread.Sleep(1000);
                DialogResult = DialogResult.Abort;
            }
        }
        
        protected PopUpMini popupInfo = null;

        void TruthBattleEnemy_MouseEnter(object sender, EventArgs e)
        {
            Panel currentPanel = (Panel)(((PictureBox)sender).Parent);
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (currentPanel.Equals(ActiveList[ii].BuffPanel))
                {
                    //((Panel)(((PictureBox)sender).Parent)).Size = new Size(26 * ActiveList[ii].BuffNumber, 26);
                    ((Panel)(((PictureBox)sender).Parent)).BringToFront();
                    break;
                }
            }
        }

        void TruthBattleEnemy_MouseMove(object sender, MouseEventArgs e)
        {
            if (popupInfo == null)
            {
                popupInfo = new PopUpMini();
            }

            popupInfo.StartPosition = FormStartPosition.Manual;

            popupInfo.Location = new Point(this.Location.X + ((PictureBox)sender).Location.X + ((Panel)((TruthImage)sender).Parent).Location.X + e.X + 5,
                this.Location.Y + ((PictureBox)sender).Location.Y + ((Panel)((TruthImage)sender).Parent).Location.Y + e.Y - 18);
            popupInfo.PopupColor = Color.Black;
            System.OperatingSystem os = System.Environment.OSVersion;
            int osNumber = os.Version.Major;
            if (osNumber != 5)
            {
                popupInfo.Opacity = 0.7f;
            }

            if (((TruthImage)sender).Image != null)
            {
                TruthImage currentImage = ((TruthImage)sender);
                popupInfo.CurrentInfo = currentImage.ImageName;
                popupInfo.CurrentInfo += "\r\n" + TruthActionCommand.GetDescription(currentImage.ImageName);
                popupInfo.Show();
            }
        }

        void TruthBattleEnemy_MouseLeave(object sender, EventArgs e)
        {
            //((Panel)(((PictureBox)sender).Parent)).Size = new Size(80, 26);

            if (popupInfo != null)
            {
                popupInfo.Close();
                popupInfo = null;
            }
        }

        void TruthBattleEnemy_Action_Leave(object sender, EventArgs e)
        {
            if (popupInfo != null)
            {
                popupInfo.Close();
                popupInfo = null;
            }
        }
        const int CURRENT_ACTION_NUM = 9;
        const int BASIC_ACTION_NUM = 8; // ��{�s��
        const int MIX_ACTION_NUM = 45; // [�x��] �b��A�{��Database�ɋL�ڂ���ׂ�
        const int MIX_ACTION_NUM_2 = 30; // [�x��]�b��A�{��Database�ɋL�ڂ���ׂ�
        const int ARCHETYPE_NUM = 1; // �A�[�L�^�C�v

        void TruthBattleEnemy_Action_MouseMove(object sender, MouseEventArgs e)
        {
            if (popupInfo == null)
            {
                popupInfo = new PopUpMini();
            }

            popupInfo.StartPosition = FormStartPosition.Manual;
            popupInfo.Location = new Point(this.Location.X + ((PictureBox)sender).Location.X + e.X + 5, this.Location.Y + ((PictureBox)sender).Location.Y + e.Y - 18);
            popupInfo.PopupColor = Color.Black;
            System.OperatingSystem os = System.Environment.OSVersion;
            int osNumber = os.Version.Major;
            if (osNumber != 5)
            {
                popupInfo.Opacity = 0.7f;
            }

            // [�x��] for�����O���O���Ƃ��������Ȃ��B���[�X�y�b�N��������x����������Ȃ��B
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (IsPlayerAlly(ActiveList[ii]))
                {
                    for (int jj = 0; jj < CURRENT_ACTION_NUM; jj++)
                    {
                        if (((PictureBox)sender).Equals(ActiveList[ii].ActionButtonList[jj]))
                        {
                            if (((PictureBox)sender).Image != null)
                            {
                                string imageName = ActiveList[ii].BattleActionCommandList[jj];
                                popupInfo.CurrentInfo = imageName;
                                popupInfo.CurrentInfo += "\r\n" + TruthActionCommand.GetDescription(imageName);
                                popupInfo.Show();
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void FieldBuff_MouseEnter(object sender, EventArgs e)
        {
        }

        private void FieldBuff_MouseLeave(object sender, EventArgs e)
        {
            if (popupInfo != null)
            {
                popupInfo.Close();
                popupInfo = null;
            }
        }

        private void FieldBuff_MouseMove(object sender, MouseEventArgs e)
        {
            if (popupInfo == null)
            {
                popupInfo = new PopUpMini();
            }

            popupInfo.StartPosition = FormStartPosition.Manual;
            popupInfo.Location = new Point(this.Location.X + ((PictureBox)sender).Location.X + e.X + 5, this.Location.Y + ((PictureBox)sender).Location.Y + e.Y - 18);
            popupInfo.PopupColor = Color.Black;
            System.OperatingSystem os = System.Environment.OSVersion;
            int osNumber = os.Version.Major;
            if (osNumber != 5)
            {
                popupInfo.Opacity = 0.7f;
            }

            if (((TruthImage)sender).Image != null)
            {
                popupInfo.CurrentInfo = ((TruthImage)sender).ImageName;
                popupInfo.CurrentInfo += "\r\n" + TruthActionCommand.GetDescription(((TruthImage)sender).ImageName);
                popupInfo.Show();
                return;
            }
        }

    }
}