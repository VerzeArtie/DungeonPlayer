using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class TruthBattleSetting : MotherForm
    {
        string[][] currentCommand = new string[Database.MAX_PARTY_MEMBER][];

        MainCharacter currentPlayer;
        int currentPlayerNumber = 0;

        private MainCharacter mc;
        public MainCharacter MC
        {
            get { return mc; }
            set { mc = value; }
        }

        private MainCharacter sc;
        public MainCharacter SC
        {
            get { return sc; }
            set { sc = value; }
        }
        private MainCharacter tc;
        public MainCharacter TC
        {
            get { return tc; }
            set { tc = value; }
        }

        private WorldEnvironment we;
        public WorldEnvironment WE
        {
            get { return we; }
            set { we = value; }
        }

        private bool duelMode = false;
        public bool DuelMode
        {
            get { return duelMode; }
            set { duelMode = value; }
        }

        PictureBox[] pbAction = null;
        PictureBox[] pbCurrentAction = null;

        PictureBox moveActionBox = null;

        const int CURRENT_ACTION_NUM = 9;
        const int BASIC_ACTION_NUM = 8; // 基本行動
        const int MIX_ACTION_NUM = 45; // [警告] 暫定、本来Databaseに記載するべき
        const int MIX_ACTION_NUM_2 = 30; // [警告]暫定、本来Databaseに記載するべき
        const int ARCHETYPE_NUM = 1; // アーキタイプ

        string[] battleCommandList = new string[BASIC_ACTION_NUM + Database.SPELL_MAX_NUM + Database.SKILL_MAX_NUM + MIX_ACTION_NUM + MIX_ACTION_NUM_2 + ARCHETYPE_NUM];
        string[] battleDescriptionList = new string[BASIC_ACTION_NUM + Database.SPELL_MAX_NUM + Database.SKILL_MAX_NUM + MIX_ACTION_NUM + MIX_ACTION_NUM_2 + ARCHETYPE_NUM];
        public TruthBattleSetting()
        {
            InitializeComponent();
        }

        void TruthBattleSetting_MouseMove(object sender, MouseEventArgs e)
        {
            moveActionBox.Location = new Point(((PictureBox)sender).Location.X + e.X - adjustX, ((PictureBox)sender).Location.Y + e.Y - adjustY);

            if (moveActionBox.Visible == false)
            {
                if (popupInfo == null)
                {
                    popupInfo = new PopUpMini();
                }

                popupInfo.StartPosition = FormStartPosition.Manual;
                popupInfo.Location = new Point(this.Location.X + ((PictureBox)sender).Location.X + e.X + 5, this.Location.Y + ((PictureBox)sender).Location.Y + e.Y - 18);
                popupInfo.PopupColor = Color.Black;
                // s 後編編集
                System.OperatingSystem os = System.Environment.OSVersion;
                int osNumber = os.Version.Major;
                if (osNumber != 5)
                {
                    popupInfo.Opacity = 0.7f;
                }
                //popupInfo.Opacity = 0.7f; // 後編削除
                //popupInfo.PopupTextColor = Brushes.White; // 後編削除
                // e 後編編集

                // temp del//popupInfo.FontFamilyName = new Font("ＭＳ ゴシック", 14.0F, FontStyle.Regular, GraphicsUnit.Pixel, 128, true);

                // [警告] for文がグルグルともったいない。ロースペックが来たら遅いかもしれない。
                for (int ii = 0; ii < CURRENT_ACTION_NUM; ii++)
                {
                    if (((PictureBox)sender).Equals(pbCurrentAction[ii]))
                    {
                        if (((PictureBox)sender).Image != null)
                        {
                            popupInfo.CurrentInfo = this.currentCommand[this.currentPlayerNumber][ii] + "\r\n";
                            for (int jj = 0; jj < BASIC_ACTION_NUM + Database.SPELL_MAX_NUM + Database.SKILL_MAX_NUM + MIX_ACTION_NUM + MIX_ACTION_NUM_2 + ARCHETYPE_NUM; jj++)
                            {
                                if (this.currentCommand[this.currentPlayerNumber][ii] == battleCommandList[jj])
                                {
                                    popupInfo.CurrentInfo += battleDescriptionList[jj];
                                }
                            }
                            popupInfo.Show();
                            return;
                        }
                    }
                }

                for (int ii = 0; ii < BASIC_ACTION_NUM + Database.SPELL_MAX_NUM + Database.SKILL_MAX_NUM + MIX_ACTION_NUM + MIX_ACTION_NUM_2 + ARCHETYPE_NUM; ii++)
                {
                    if (((PictureBox)sender).Equals(pbAction[ii]))
                    {
                        if (((PictureBox)sender).Image != null)
                        {
                            popupInfo.CurrentInfo = battleCommandList[ii] + "\r\n" + battleDescriptionList[ii];
                            popupInfo.Show();
                            return;
                        }
                    }
                }
            }

        }

        void TruthBattleSetting_MouseUp(object sender, MouseEventArgs e)
        {
            if (moveActionBox != null)
            {
                moveActionBox.Visible = false;
            }

            int positionX = e.X + ((PictureBox)sender).Location.X;
            int positionY = e.Y + ((PictureBox)sender).Location.Y;
            for (int ii = 0; ii < CURRENT_ACTION_NUM; ii++)
            {
                if (pbCurrentAction[ii].Location.X <= positionX && positionX <= pbCurrentAction[ii].Location.X + pbCurrentAction[ii].Width &&
                    pbCurrentAction[ii].Location.Y <= positionY && positionY <= pbCurrentAction[ii].Location.Y + pbCurrentAction[ii].Height)
                {
                    pbCurrentAction[ii].Image = ((PictureBox)sender).Image;

                    this.currentCommand[this.currentPlayerNumber][ii] = Database.STAY_EN;
                    for (int jj = 0; jj < BASIC_ACTION_NUM + Database.SPELL_MAX_NUM + Database.SKILL_MAX_NUM + MIX_ACTION_NUM + MIX_ACTION_NUM_2 + ARCHETYPE_NUM; jj++)
                    {
                        if (pbCurrentAction[ii].Image.Equals(pbAction[jj].Image))
                        {
                            this.currentCommand[this.currentPlayerNumber][ii] = battleCommandList[jj];
                            break;
                        }
                    }
                    break;
                }
            }
        }

        int adjustX = 0;
        int adjustY = 0;
        void TruthBattleSetting_MouseDown(object sender, MouseEventArgs e)
        {
            if (moveActionBox == null)
            {
                moveActionBox = new PictureBox();
            }
            moveActionBox.Image = ((PictureBox)sender).Image;
            moveActionBox.Visible = true;
            this.adjustX = e.X;
            this.adjustY = e.Y;
            moveActionBox.Location = new Point(((PictureBox)sender).Location.X + e.X, ((PictureBox)sender).Location.Y + e.Y);
        }

        protected PopUpMini popupInfo = null;
        void TruthBattleSetting_MouseLeave(object sender, EventArgs e)
        {
            if (popupInfo != null)
            {
                popupInfo.Close();
                popupInfo = null;
            }
        }

        void TruthBattleSetting_MouseEnter(object sender, EventArgs e)
        {


        }

        private void SetupbattleCommand()
        {
            // 基本行動・スペル・スキルの順序の配列を記憶しておく。
            string[] ssName = TruthActionCommand.GetActionList(currentPlayer);
            for (int ii = 0; ii < Database.TOTAL_COMMAND_NUM; ii++)
            {
                battleCommandList[ii] = ssName[ii];
                battleDescriptionList[ii] = TruthActionCommand.GetDescription(ssName[ii]);
            }
        }

        private void TruthBattleSetting_Load(object sender, EventArgs e)
        {
            if (mc != null) { button2.BackColor = mc.PlayerColor; }
            if (sc != null) { button3.BackColor = sc.PlayerColor; }
            if (tc != null) { button4.BackColor = tc.PlayerColor; }

            for (int ii = 0; ii < Database.MAX_PARTY_MEMBER; ii++)
            {
                this.currentCommand[ii] = new string[Database.BATTLE_COMMAND_MAX];
            }

            currentPlayer = this.mc;
            currentPlayerNumber = 0;
            SetupbattleCommand();

            // 基本行動・スペル・スキルの選択ボックスを準備する。
            pbCurrentAction = new PictureBox[CURRENT_ACTION_NUM];

            int sourceSize = 50;
            if (we.AvailableMixSpellSkill == false)
            {
                pbAction = new PictureBox[BASIC_ACTION_NUM + Database.SPELL_MAX_NUM + Database.SKILL_MAX_NUM + MIX_ACTION_NUM + MIX_ACTION_NUM_2 + ARCHETYPE_NUM];
                int currentSelectSize = 70;
                sourceSize = 50;
                int adjustX = 50;
                int adjustY = 90;
                int baseY = 120;

                label20.Location = new Point(10, baseY + adjustY * 0);
                label8.Location = new Point(10, baseY + adjustY * 1);
                label9.Location = new Point(10, baseY + adjustY * 2);
                label10.Location = new Point(10, baseY + adjustY * 3);
                label11.Location = new Point(10, baseY + adjustY * 4);
                label12.Location = new Point(10, baseY + adjustY * 5);
                label13.Location = new Point(10, baseY + adjustY * 6);
                label14.Location = new Point(500, baseY + adjustY * 1);
                label15.Location = new Point(500, baseY + adjustY * 2);
                label16.Location = new Point(500, baseY + adjustY * 3);
                label17.Location = new Point(500, baseY + adjustY * 4);
                label18.Location = new Point(500, baseY + adjustY * 5);
                label19.Location = new Point(500, baseY + adjustY * 6);
                label20.Size = new System.Drawing.Size(50, 40);
                label8.Size = new System.Drawing.Size(50, 40);
                label9.Size = new System.Drawing.Size(50, 40);
                label10.Size = new System.Drawing.Size(50, 40);
                label11.Size = new System.Drawing.Size(50, 40);
                label12.Size = new System.Drawing.Size(50, 40);
                label13.Size = new System.Drawing.Size(50, 40);
                label14.Size = new System.Drawing.Size(50, 40);
                label15.Size = new System.Drawing.Size(50, 40);
                label16.Size = new System.Drawing.Size(50, 40);
                label17.Size = new System.Drawing.Size(50, 40);
                label18.Size = new System.Drawing.Size(50, 40);
                label19.Size = new System.Drawing.Size(50, 40);

                // 現在選択
                for (int ii = 0; ii < CURRENT_ACTION_NUM; ii++)
                {
                    pbCurrentAction[ii] = new PictureBox();
                    pbCurrentAction[ii].BackColor = System.Drawing.Color.Transparent;
                    pbCurrentAction[ii].Location = new System.Drawing.Point(20 + (ii % CURRENT_ACTION_NUM) * 90, 20);
                    pbCurrentAction[ii].Name = "pbCurrentAction" + ii.ToString();
                    pbCurrentAction[ii].Size = new System.Drawing.Size(currentSelectSize, currentSelectSize);
                    pbCurrentAction[ii].SizeMode = PictureBoxSizeMode.StretchImage;
                    pbCurrentAction[ii].MouseEnter += new EventHandler(TruthBattleSetting_MouseEnter);
                    pbCurrentAction[ii].MouseDown += new MouseEventHandler(TruthBattleSetting_MouseDown);
                    pbCurrentAction[ii].MouseUp += new MouseEventHandler(TruthBattleSetting_MouseUp);
                    pbCurrentAction[ii].MouseLeave += new EventHandler(TruthBattleSetting_MouseLeave);
                    pbCurrentAction[ii].MouseMove += new MouseEventHandler(TruthBattleSetting_MouseMove);
                    this.Controls.Add(pbCurrentAction[ii]);
                }
                // 基本
                for (int ii = 0; ii < BASIC_ACTION_NUM; ii++)
                {
                    pbAction[ii] = new PictureBox();
                    pbAction[ii].BackColor = System.Drawing.Color.Transparent;
                    pbAction[ii].Location = new System.Drawing.Point(70 + (ii % BASIC_ACTION_NUM) * sourceSize, 110);
                    pbAction[ii].Name = "pbAction" + ii.ToString();
                    pbAction[ii].Size = new System.Drawing.Size(sourceSize, sourceSize);
                    pbAction[ii].SizeMode = PictureBoxSizeMode.StretchImage;
                    pbAction[ii].MouseEnter += new EventHandler(TruthBattleSetting_MouseEnter);
                    pbAction[ii].MouseDown += new MouseEventHandler(TruthBattleSetting_MouseDown);
                    pbAction[ii].MouseUp += new MouseEventHandler(TruthBattleSetting_MouseUp);
                    pbAction[ii].MouseLeave += new EventHandler(TruthBattleSetting_MouseLeave);
                    pbAction[ii].MouseMove += new MouseEventHandler(TruthBattleSetting_MouseMove);
                    this.Controls.Add(pbAction[ii]);
                }
                // スペル
                for (int ii = BASIC_ACTION_NUM; ii < BASIC_ACTION_NUM + Database.SPELL_MAX_NUM; ii++)
                {
                    pbAction[ii] = new PictureBox();
                    pbAction[ii].BackColor = System.Drawing.Color.Transparent;
                    pbAction[ii].Location = new System.Drawing.Point(70 + ((ii - BASIC_ACTION_NUM) % Database.SPELL_ONETYPE_MAX_NUM) * adjustX,
                                                                    205 + ((ii - BASIC_ACTION_NUM) / Database.SPELL_ONETYPE_MAX_NUM) * adjustY);
                    pbAction[ii].Name = "pbAction" + ii.ToString();
                    pbAction[ii].Size = new System.Drawing.Size(sourceSize, sourceSize);
                    pbAction[ii].SizeMode = PictureBoxSizeMode.StretchImage;
                    pbAction[ii].MouseEnter += new EventHandler(TruthBattleSetting_MouseEnter);
                    pbAction[ii].MouseDown += new MouseEventHandler(TruthBattleSetting_MouseDown);
                    pbAction[ii].MouseUp += new MouseEventHandler(TruthBattleSetting_MouseUp);
                    pbAction[ii].MouseLeave += new EventHandler(TruthBattleSetting_MouseLeave);
                    pbAction[ii].MouseMove += new MouseEventHandler(TruthBattleSetting_MouseMove);
                    this.Controls.Add(pbAction[ii]);
                }

                // スキル
                for (int ii = BASIC_ACTION_NUM + Database.SPELL_MAX_NUM; ii < BASIC_ACTION_NUM + Database.SPELL_MAX_NUM + Database.SKILL_MAX_NUM; ii++)
                {
                    pbAction[ii] = new PictureBox();
                    pbAction[ii].BackColor = System.Drawing.Color.Transparent;
                    pbAction[ii].Location = new System.Drawing.Point(560 + ((ii - BASIC_ACTION_NUM - Database.SPELL_MAX_NUM) % Database.SKILL_ONETYPE_MAX_NUM) * adjustX,
                                                                     205 + ((ii - BASIC_ACTION_NUM - Database.SPELL_MAX_NUM) / Database.SKILL_ONETYPE_MAX_NUM) * adjustY);
                    pbAction[ii].Name = "pbAction" + ii.ToString();
                    pbAction[ii].Size = new System.Drawing.Size(sourceSize, sourceSize);
                    pbAction[ii].SizeMode = PictureBoxSizeMode.StretchImage;
                    pbAction[ii].MouseEnter += new EventHandler(TruthBattleSetting_MouseEnter);
                    pbAction[ii].MouseDown += new MouseEventHandler(TruthBattleSetting_MouseDown);
                    pbAction[ii].MouseUp += new MouseEventHandler(TruthBattleSetting_MouseUp);
                    pbAction[ii].MouseLeave += new EventHandler(TruthBattleSetting_MouseLeave);
                    pbAction[ii].MouseMove += new MouseEventHandler(TruthBattleSetting_MouseMove);
                    this.Controls.Add(pbAction[ii]);
                }


                label21.Visible = false;
                label22.Visible = false;
                // 複合魔法
                for (int ii = BASIC_ACTION_NUM + Database.SPELL_MAX_NUM + Database.SKILL_MAX_NUM; ii < BASIC_ACTION_NUM + Database.SPELL_MAX_NUM + Database.SKILL_MAX_NUM + MIX_ACTION_NUM; ii++)
                {
                    pbAction[ii] = new PictureBox();
                    pbAction[ii].BackColor = System.Drawing.Color.Transparent;
                    pbAction[ii].Location = new System.Drawing.Point(-100, -100);
                    pbAction[ii].Name = "pbAction" + ii.ToString();
                    pbAction[ii].Size = new System.Drawing.Size(sourceSize, sourceSize);
                    pbAction[ii].SizeMode = PictureBoxSizeMode.StretchImage;
                }

                // 複合スキル
                for (int ii = BASIC_ACTION_NUM + Database.SPELL_MAX_NUM + Database.SKILL_MAX_NUM + MIX_ACTION_NUM; ii < BASIC_ACTION_NUM + Database.SPELL_MAX_NUM + Database.SKILL_MAX_NUM + MIX_ACTION_NUM + MIX_ACTION_NUM_2; ii++)
                {
                    pbAction[ii] = new PictureBox();
                    pbAction[ii].BackColor = System.Drawing.Color.Transparent;
                    pbAction[ii].Location = new System.Drawing.Point(-100, -100);
                    pbAction[ii].Name = "pbAction" + ii.ToString();
                    pbAction[ii].Size = new System.Drawing.Size(sourceSize, sourceSize);
                    pbAction[ii].SizeMode = PictureBoxSizeMode.StretchImage;
                }
            }
            else
            {
                pbAction = new PictureBox[BASIC_ACTION_NUM + Database.SPELL_MAX_NUM + Database.SKILL_MAX_NUM + MIX_ACTION_NUM + MIX_ACTION_NUM_2 + ARCHETYPE_NUM];
                int currentSelectSize = 70;
                sourceSize = 50;
                int pictureBoxWidth = 50;
                int marginY = 50;
                int marginY2 = 50;
                // 現在選択
                for (int ii = 0; ii < CURRENT_ACTION_NUM; ii++)
                {
                    pbCurrentAction[ii] = new PictureBox();
                    pbCurrentAction[ii].BackColor = System.Drawing.Color.Transparent;
                    pbCurrentAction[ii].Location = new System.Drawing.Point(20 + (ii % CURRENT_ACTION_NUM) * 90, 20);
                    pbCurrentAction[ii].Name = "pbCurrentAction" + ii.ToString();
                    pbCurrentAction[ii].Size = new System.Drawing.Size(currentSelectSize, currentSelectSize);
                    pbCurrentAction[ii].SizeMode = PictureBoxSizeMode.StretchImage;
                    pbCurrentAction[ii].MouseEnter += new EventHandler(TruthBattleSetting_MouseEnter);
                    pbCurrentAction[ii].MouseDown += new MouseEventHandler(TruthBattleSetting_MouseDown);
                    pbCurrentAction[ii].MouseUp += new MouseEventHandler(TruthBattleSetting_MouseUp);
                    pbCurrentAction[ii].MouseLeave += new EventHandler(TruthBattleSetting_MouseLeave);
                    pbCurrentAction[ii].MouseMove += new MouseEventHandler(TruthBattleSetting_MouseMove);
                    this.Controls.Add(pbCurrentAction[ii]);
                }
                // 基本
                for (int ii = 0; ii < BASIC_ACTION_NUM; ii++)
                {
                    pbAction[ii] = new PictureBox();
                    pbAction[ii].BackColor = System.Drawing.Color.Transparent;
                    pbAction[ii].Location = new System.Drawing.Point(70 + (ii % BASIC_ACTION_NUM) * pictureBoxWidth, 110);
                    pbAction[ii].Name = "pbAction" + ii.ToString();
                    pbAction[ii].Size = new System.Drawing.Size(sourceSize, sourceSize);
                    pbAction[ii].SizeMode = PictureBoxSizeMode.StretchImage;
                    pbAction[ii].MouseEnter += new EventHandler(TruthBattleSetting_MouseEnter);
                    pbAction[ii].MouseDown += new MouseEventHandler(TruthBattleSetting_MouseDown);
                    pbAction[ii].MouseUp += new MouseEventHandler(TruthBattleSetting_MouseUp);
                    pbAction[ii].MouseLeave += new EventHandler(TruthBattleSetting_MouseLeave);
                    pbAction[ii].MouseMove += new MouseEventHandler(TruthBattleSetting_MouseMove);
                    this.Controls.Add(pbAction[ii]);
                }
                // スペル
                for (int ii = BASIC_ACTION_NUM; ii < BASIC_ACTION_NUM + Database.SPELL_MAX_NUM; ii++)
                {
                    pbAction[ii] = new PictureBox();
                    pbAction[ii].BackColor = System.Drawing.Color.Transparent;
                    pbAction[ii].Location = new System.Drawing.Point(70 + ((ii - BASIC_ACTION_NUM) % Database.SPELL_ONETYPE_MAX_NUM) * pictureBoxWidth,
                                                                    180 + ((ii - BASIC_ACTION_NUM) / Database.SPELL_ONETYPE_MAX_NUM) * marginY);
                    pbAction[ii].Name = "pbAction" + ii.ToString();
                    pbAction[ii].Size = new System.Drawing.Size(sourceSize, sourceSize);
                    pbAction[ii].SizeMode = PictureBoxSizeMode.StretchImage;
                    pbAction[ii].MouseEnter += new EventHandler(TruthBattleSetting_MouseEnter);
                    pbAction[ii].MouseDown += new MouseEventHandler(TruthBattleSetting_MouseDown);
                    pbAction[ii].MouseUp += new MouseEventHandler(TruthBattleSetting_MouseUp);
                    pbAction[ii].MouseLeave += new EventHandler(TruthBattleSetting_MouseLeave);
                    pbAction[ii].MouseMove += new MouseEventHandler(TruthBattleSetting_MouseMove);
                    this.Controls.Add(pbAction[ii]);
                }

                // スキル
                for (int ii = BASIC_ACTION_NUM + Database.SPELL_MAX_NUM; ii < BASIC_ACTION_NUM + Database.SPELL_MAX_NUM + Database.SKILL_MAX_NUM; ii++)
                {
                    pbAction[ii] = new PictureBox();
                    pbAction[ii].BackColor = System.Drawing.Color.Transparent;
                    pbAction[ii].Location = new System.Drawing.Point(560 + ((ii - BASIC_ACTION_NUM - Database.SPELL_MAX_NUM) % Database.SKILL_ONETYPE_MAX_NUM) * pictureBoxWidth,
                                                                     180 + ((ii - BASIC_ACTION_NUM - Database.SPELL_MAX_NUM) / Database.SKILL_ONETYPE_MAX_NUM) * marginY);
                    pbAction[ii].Name = "pbAction" + ii.ToString();
                    pbAction[ii].Size = new System.Drawing.Size(sourceSize, sourceSize);
                    pbAction[ii].SizeMode = PictureBoxSizeMode.StretchImage;
                    pbAction[ii].MouseEnter += new EventHandler(TruthBattleSetting_MouseEnter);
                    pbAction[ii].MouseDown += new MouseEventHandler(TruthBattleSetting_MouseDown);
                    pbAction[ii].MouseUp += new MouseEventHandler(TruthBattleSetting_MouseUp);
                    pbAction[ii].MouseLeave += new EventHandler(TruthBattleSetting_MouseLeave);
                    pbAction[ii].MouseMove += new MouseEventHandler(TruthBattleSetting_MouseMove);
                    this.Controls.Add(pbAction[ii]);
                }

                // 複合魔法
                for (int ii = BASIC_ACTION_NUM + Database.SPELL_MAX_NUM + Database.SKILL_MAX_NUM; ii < BASIC_ACTION_NUM + Database.SPELL_MAX_NUM + Database.SKILL_MAX_NUM + MIX_ACTION_NUM; ii++)
                {
                    pbAction[ii] = new PictureBox();
                    pbAction[ii].BackColor = System.Drawing.Color.Transparent;
                    pbAction[ii].Location = new System.Drawing.Point(70 + ((ii - BASIC_ACTION_NUM - Database.SPELL_MAX_NUM - Database.SKILL_MAX_NUM) % 15) * pictureBoxWidth,
                                                                    500 + ((ii - BASIC_ACTION_NUM - Database.SPELL_MAX_NUM - Database.SKILL_MAX_NUM) / 15) * marginY2);
                    pbAction[ii].Name = "pbAction" + ii.ToString();
                    pbAction[ii].Size = new System.Drawing.Size(sourceSize, sourceSize);
                    pbAction[ii].SizeMode = PictureBoxSizeMode.StretchImage;
                    pbAction[ii].MouseEnter += new EventHandler(TruthBattleSetting_MouseEnter);
                    pbAction[ii].MouseDown += new MouseEventHandler(TruthBattleSetting_MouseDown);
                    pbAction[ii].MouseUp += new MouseEventHandler(TruthBattleSetting_MouseUp);
                    pbAction[ii].MouseLeave += new EventHandler(TruthBattleSetting_MouseLeave);
                    pbAction[ii].MouseMove += new MouseEventHandler(TruthBattleSetting_MouseMove);
                    this.Controls.Add(pbAction[ii]);
                }

                // 複合スキル
                for (int ii = BASIC_ACTION_NUM + Database.SPELL_MAX_NUM + Database.SKILL_MAX_NUM + MIX_ACTION_NUM; ii < BASIC_ACTION_NUM + Database.SPELL_MAX_NUM + Database.SKILL_MAX_NUM + MIX_ACTION_NUM + MIX_ACTION_NUM_2; ii++)
                {
                    pbAction[ii] = new PictureBox();
                    pbAction[ii].BackColor = System.Drawing.Color.Transparent;
                    pbAction[ii].Location = new System.Drawing.Point(70 + ((ii - BASIC_ACTION_NUM - Database.SPELL_MAX_NUM - Database.SKILL_MAX_NUM - MIX_ACTION_NUM) % 15) * pictureBoxWidth,
                                                                    660 + ((ii - BASIC_ACTION_NUM - Database.SPELL_MAX_NUM - Database.SKILL_MAX_NUM - MIX_ACTION_NUM) / 15) * marginY2);
                    pbAction[ii].Name = "pbAction" + ii.ToString();
                    pbAction[ii].Size = new System.Drawing.Size(sourceSize, sourceSize);
                    pbAction[ii].SizeMode = PictureBoxSizeMode.StretchImage;
                    pbAction[ii].MouseEnter += new EventHandler(TruthBattleSetting_MouseEnter);
                    pbAction[ii].MouseDown += new MouseEventHandler(TruthBattleSetting_MouseDown);
                    pbAction[ii].MouseUp += new MouseEventHandler(TruthBattleSetting_MouseUp);
                    pbAction[ii].MouseLeave += new EventHandler(TruthBattleSetting_MouseLeave);
                    pbAction[ii].MouseMove += new MouseEventHandler(TruthBattleSetting_MouseMove);
                    this.Controls.Add(pbAction[ii]);
                }
            }

            if (we.availableArchetypeCommand)
            {
                // 元核
                sourceSize = 50;
                for (int ii = BASIC_ACTION_NUM + Database.SPELL_MAX_NUM + Database.SKILL_MAX_NUM + MIX_ACTION_NUM + MIX_ACTION_NUM_2; ii < BASIC_ACTION_NUM + Database.SPELL_MAX_NUM + Database.SKILL_MAX_NUM + MIX_ACTION_NUM + MIX_ACTION_NUM_2 + ARCHETYPE_NUM; ii++)
                {
                    pbAction[ii] = new PictureBox();
                    pbAction[ii].BackColor = System.Drawing.Color.Transparent;
                    pbAction[ii].Location = new System.Drawing.Point(875, 180);
                    pbAction[ii].Name = "pbAction" + ii.ToString();
                    pbAction[ii].Size = new System.Drawing.Size(sourceSize, sourceSize);
                    pbAction[ii].SizeMode = PictureBoxSizeMode.StretchImage;
                    pbAction[ii].MouseEnter += new EventHandler(TruthBattleSetting_MouseEnter);
                    pbAction[ii].MouseDown += new MouseEventHandler(TruthBattleSetting_MouseDown);
                    pbAction[ii].MouseUp += new MouseEventHandler(TruthBattleSetting_MouseUp);
                    pbAction[ii].MouseLeave += new EventHandler(TruthBattleSetting_MouseLeave);
                    pbAction[ii].MouseMove += new MouseEventHandler(TruthBattleSetting_MouseMove);
                    this.Controls.Add(pbAction[ii]);
                }
            }
            else
            {
                label25.Visible = false;
            }

            moveActionBox = new PictureBox();
            moveActionBox.Visible = false;
            moveActionBox.BackColor = System.Drawing.Color.Transparent;
            moveActionBox.Location = new System.Drawing.Point(0, 0);
            moveActionBox.Name = "moveActionBox";
            moveActionBox.Size = new System.Drawing.Size(30, 30);
            moveActionBox.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(moveActionBox);
            moveActionBox.BringToFront();

            if (this.duelMode == false)
            {
                if (we.AvailableSecondCharacter)
                {
                    button2.Visible = true;
                    button3.Visible = true;
                }
                if (we.AvailableThirdCharacter)
                {
                    button4.Visible = true;
                }
            }

            SetupAllIcon();
        }

        private void SetupAllIcon()
        {
            string fileExt = ".bmp";           
            string[] ssName = TruthActionCommand.GetActionList(currentPlayer);
            bool[] ssAvailable = TruthActionCommand.GetAvailableActionList(currentPlayer);
             for (int ii = 0; ii < Database.TOTAL_COMMAND_NUM; ii++)
            {
                try { if (ssAvailable[ii]) { pbAction[ii].Image = Image.FromFile(Database.BaseResourceFolder + ssName[ii] + fileExt); } else { pbAction[ii].Image = null; } }
                catch { }
            }         

            // プレイヤーのバトルコマンドを反映する。
            if (this.currentCommand[this.currentPlayerNumber][0] == null) this.currentCommand[this.currentPlayerNumber][0] = currentPlayer.BattleActionCommand1;
            if (this.currentCommand[this.currentPlayerNumber][1] == null) this.currentCommand[this.currentPlayerNumber][1] = currentPlayer.BattleActionCommand2;
            if (this.currentCommand[this.currentPlayerNumber][2] == null) this.currentCommand[this.currentPlayerNumber][2] = currentPlayer.BattleActionCommand3;
            if (this.currentCommand[this.currentPlayerNumber][3] == null) this.currentCommand[this.currentPlayerNumber][3] = currentPlayer.BattleActionCommand4;
            if (this.currentCommand[this.currentPlayerNumber][4] == null) this.currentCommand[this.currentPlayerNumber][4] = currentPlayer.BattleActionCommand5;
            if (this.currentCommand[this.currentPlayerNumber][5] == null) this.currentCommand[this.currentPlayerNumber][5] = currentPlayer.BattleActionCommand6;
            if (this.currentCommand[this.currentPlayerNumber][6] == null) this.currentCommand[this.currentPlayerNumber][6] = currentPlayer.BattleActionCommand7;
            if (this.currentCommand[this.currentPlayerNumber][7] == null) this.currentCommand[this.currentPlayerNumber][7] = currentPlayer.BattleActionCommand8;
            if (this.currentCommand[this.currentPlayerNumber][8] == null) this.currentCommand[this.currentPlayerNumber][8] = currentPlayer.BattleActionCommand9;

            for (int ii = 0; ii < Database.BATTLE_COMMAND_MAX; ii++)
            {
                pbCurrentAction[ii].Image = pbAction[2].Image;
                for (int jj = 0; jj < BASIC_ACTION_NUM + Database.SPELL_MAX_NUM + Database.SKILL_MAX_NUM + MIX_ACTION_NUM + MIX_ACTION_NUM_2 + ARCHETYPE_NUM; jj++)
                {
                    if (currentCommand[this.currentPlayerNumber][ii] == battleCommandList[jj])
                    {
                        pbCurrentAction[ii].Image = pbAction[jj].Image;
                        break;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (CheckChangeCommand(this.currentPlayer))
            //{
            //    mainMessage.Text = "アイン：バトル設定を反映してないみたいだぜ。反映しておくか？";
            //    using (YesNoRequest yesno = new YesNoRequest())
            //    {
            //        yesno.StartPosition = FormStartPosition.CenterParent;
            //        yesno.ShowDialog();
            //        if (yesno.DialogResult == System.Windows.Forms.DialogResult.Yes)
            //        {
            //            UpdateCurrentPlayerCommand(this.currentPlayer);
            //        }

            //    }
            //}
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.currentPlayer = mc;
            this.currentPlayerNumber = 0;
            SetupbattleCommand();
            SetupAllIcon();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.currentPlayer = sc;
            this.currentPlayerNumber = 1;
            SetupbattleCommand();
            SetupAllIcon();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.currentPlayer = tc;
            this.currentPlayerNumber = 2;
            SetupbattleCommand();
            SetupAllIcon();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int ii = 0; ii < Database.MAX_PARTY_MEMBER; ii++)
            {
                this.currentPlayerNumber = 0;
                this.currentPlayer = mc;
                SetupAllIcon();
                UpdateCurrentPlayerCommand(mc);

                if (we.AvailableSecondCharacter && this.duelMode == false)
                {
                    this.currentPlayerNumber = 1;
                    this.currentPlayer = sc;
                    SetupAllIcon();
                    UpdateCurrentPlayerCommand(sc);
                }

                if (we.AvailableThirdCharacter && this.duelMode == false)
                {
                    this.currentPlayerNumber = 2;
                    this.currentPlayer = tc;
                    SetupAllIcon();
                    UpdateCurrentPlayerCommand(tc);
                }
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void UpdateCurrentPlayerCommand(MainCharacter player)
        {
            try
            {
                for (int ii = 0; ii < Database.BATTLE_COMMAND_MAX; ii++)
                {
                    this.currentCommand[this.currentPlayerNumber][ii] = Database.STAY_EN;
                    for (int jj = 0; jj < BASIC_ACTION_NUM + Database.SPELL_MAX_NUM + Database.SKILL_MAX_NUM + MIX_ACTION_NUM + MIX_ACTION_NUM_2 + ARCHETYPE_NUM; jj++)
                    {
                        if (pbCurrentAction[ii].Image.Equals(pbAction[jj].Image))
                        {
                            this.currentCommand[this.currentPlayerNumber][ii] = battleCommandList[jj];
                            break;
                        }
                    }
                }

                // キャラクターのバトルコマンド設定に反映
                player.BattleActionCommand1 = this.currentCommand[this.currentPlayerNumber][0];
                player.BattleActionCommand2 = this.currentCommand[this.currentPlayerNumber][1];
                player.BattleActionCommand3 = this.currentCommand[this.currentPlayerNumber][2];
                player.BattleActionCommand4 = this.currentCommand[this.currentPlayerNumber][3];
                player.BattleActionCommand5 = this.currentCommand[this.currentPlayerNumber][4];
                player.BattleActionCommand6 = this.currentCommand[this.currentPlayerNumber][5];
                player.BattleActionCommand7 = this.currentCommand[this.currentPlayerNumber][6];
                player.BattleActionCommand8 = this.currentCommand[this.currentPlayerNumber][7];
                player.BattleActionCommand9 = this.currentCommand[this.currentPlayerNumber][8];
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool CheckChangeCommand(MainCharacter player)
        {
            if (player.BattleActionCommand1 != this.currentCommand[this.currentPlayerNumber][0]) return true;
            if (player.BattleActionCommand2 != this.currentCommand[this.currentPlayerNumber][1]) return true;
            if (player.BattleActionCommand3 != this.currentCommand[this.currentPlayerNumber][2]) return true;
            if (player.BattleActionCommand4 != this.currentCommand[this.currentPlayerNumber][3]) return true;
            if (player.BattleActionCommand5 != this.currentCommand[this.currentPlayerNumber][4]) return true;
            if (player.BattleActionCommand6 != this.currentCommand[this.currentPlayerNumber][5]) return true;
            if (player.BattleActionCommand7 != this.currentCommand[this.currentPlayerNumber][6]) return true;
            if (player.BattleActionCommand8 != this.currentCommand[this.currentPlayerNumber][7]) return true;
            if (player.BattleActionCommand9 != this.currentCommand[this.currentPlayerNumber][8]) return true;

            return false;
            
        }
    }
}
