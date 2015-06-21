using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using SlimDX.DirectInput;

namespace DungeonPlayer
{
    public partial class TruthStatusPlayer : MotherForm
    {
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

        // s 後編追加
        private bool initializeLevelUp;
        public bool InitializeLevelUp
        {
            get { return initializeLevelUp; }
            set { initializeLevelUp = value; }
        }

        private bool levelUp;
        public bool LevelUp
        {
            get { return levelUp; }
            set { levelUp = value; }
        }
        private int upPoint = 4;
        public int UpPoint
        {
            get { return UpPoint; }
            set { upPoint = value; }
        }
        public int CumultiveLvUpValue { get; set; }

        private Color currentStatusView = Database.COLOR_EIN;
        public Color CurrentStatusView
        {
            get { return currentStatusView; }
            set { currentStatusView = value; }
        }

        private bool onlySelectTrash = false;
        public bool OnlySelectTrash
        {
            get { return onlySelectTrash; }
            set { onlySelectTrash = value; }
        }

        private string cannotSelectTrash = String.Empty; // 対象アイテムが重要品で捨てられない場合。
        public string CannotSelectTrash
        {
            get { return cannotSelectTrash; }
            set { cannotSelectTrash = value; }
        }

        private bool onlyUseItem = false;
        public bool OnlyUseItem
        {
            get { return onlyUseItem; }
            set { onlyUseItem = value; }
        }

        private bool duelMode = false;
        public bool DuelMode
        {
            get { return duelMode; }
            set { duelMode = value; }
        }


        private System.Windows.Forms.Label[] backpack;
        private System.Windows.Forms.Label[] backpackStack;
        private System.Windows.Forms.PictureBox[] backpackIcon;

        private bool useOverShifting = false;

        private System.Windows.Forms.Label[] SpellSkill;

        private System.Windows.Forms.Label[] ResistLabel;
        private System.Windows.Forms.Label[] ResistLabelValue;
        static int MAX_RESIST_NUM = 6; // レジスト値の最大数

        private System.Windows.Forms.Label[] ResistAbnormalStatus;
        private System.Windows.Forms.Label[] ResistAbnormalStatusValue;
        static int MAX_ABNORMALSTATUS_NUM = 10; // 状態異常の最大数

        public TruthStatusPlayer()
        {
            InitializeComponent();
            SetupBackpackData();
        }

        static int FIXED_ROW_NUM = 10; // バックパックや魔法・スキルのリスト欄の最大列数
        private void SetupBackpackData()
        {
            backpack = new Label[Database.MAX_BACKPACK_SIZE];
            backpackStack = new Label[Database.MAX_BACKPACK_SIZE];
            backpackIcon = new PictureBox[Database.MAX_BACKPACK_SIZE];

            for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
            {
                backpackIcon[ii] = new PictureBox();
                backpackIcon[ii].SizeMode = PictureBoxSizeMode.StretchImage;
                backpackIcon[ii].BackColor = System.Drawing.Color.Transparent;
                backpackIcon[ii].Location = new System.Drawing.Point(5 + 320 * (ii / FIXED_ROW_NUM), 13 + 31 * (ii % FIXED_ROW_NUM));
                backpackIcon[ii].Name = "backpackIcon" + ii.ToString();
                backpackIcon[ii].Size = new System.Drawing.Size(20, 20);
                backpackIcon[ii].TabStop = false;
                this.grpBackPack.Controls.Add(backpackIcon[ii]);

                backpackStack[ii] = new Label();
                backpackStack[ii].Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                backpackStack[ii].Location = new System.Drawing.Point(24 + 320 * (ii / FIXED_ROW_NUM), 15 + 31 * (ii % FIXED_ROW_NUM));
                backpackStack[ii].Name = "backpackStack" + ii.ToString();
                backpackStack[ii].AutoSize = true;
                backpackStack[ii].TabIndex = 0;
                this.grpBackPack.Controls.Add(backpackStack[ii]);

                backpack[ii] = new Label();
                backpack[ii].Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Underline | System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                backpack[ii].Location = new System.Drawing.Point(56 + 320 * (ii / FIXED_ROW_NUM), 15 + 31 * (ii % FIXED_ROW_NUM));
                backpack[ii].Name = "backpack" + ii.ToString();
                backpack[ii].AutoSize = true;
                backpack[ii].TabIndex = 0;
                backpack[ii].MouseEnter += new EventHandler(StatusPlayer_MouseEnter);
                backpack[ii].MouseDown += new MouseEventHandler(StatusPlayer_MouseDown);
                backpack[ii].MouseLeave += new EventHandler(StatusPlayer_MouseLeave);
                backpack[ii].Click += new EventHandler(StatusPlayer_Click);
                this.grpBackPack.Controls.Add(backpack[ii]);
            }

            SpellSkill = new Label[FIXED_ROW_NUM];
            for (int ii = 0; ii < FIXED_ROW_NUM; ii++)
            {
                SpellSkill[ii] = new Label();
                SpellSkill[ii].Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Underline | System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                SpellSkill[ii].Location = new System.Drawing.Point(50 + 320 * (ii / FIXED_ROW_NUM), 15 + 31 * (ii % FIXED_ROW_NUM));
                SpellSkill[ii].Name = "SpellSkill" + ii.ToString();
                SpellSkill[ii].AutoSize = true;
                SpellSkill[ii].TabIndex = 0;
                //SpellSkill[ii].MouseEnter += new EventHandler(StatusPlayer_MouseEnter);
                SpellSkill[ii].MouseDown += new MouseEventHandler(StatusPlayer_MouseDown);
                //SpellSkill[ii].MouseLeave += new EventHandler(StatusPlayer_MouseLeave);
                SpellSkill[ii].Click += new EventHandler(btnSomeSpellSkill_Click);
                this.grpSpellSkill.Controls.Add(SpellSkill[ii]);
            }

            ResistLabel = new Label[MAX_RESIST_NUM];
            ResistLabelValue = new Label[MAX_RESIST_NUM];
            for (int ii = 0; ii < MAX_RESIST_NUM; ii++)
            {
                ResistLabel[ii] = new Label();
                ResistLabel[ii].Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                ResistLabel[ii].Location = new System.Drawing.Point(20, 30 + 50 * (ii % MAX_RESIST_NUM));
                ResistLabel[ii].Name = "ResistLabel" + ii.ToString();
                ResistLabel[ii].AutoSize = true;
                ResistLabel[ii].TabIndex = 0;
                this.grpResistStatus.Controls.Add(ResistLabel[ii]);

                ResistLabelValue[ii] = new Label();
                ResistLabelValue[ii].Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold | FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                ResistLabelValue[ii].Location = new System.Drawing.Point(130, 30 + 50 * (ii % MAX_RESIST_NUM));
                ResistLabelValue[ii].Name = "ResistLabelValue" + ii.ToString();
                ResistLabelValue[ii].AutoSize = true;
                ResistLabelValue[ii].TabIndex = 0;
                this.grpResistStatus.Controls.Add(ResistLabelValue[ii]);
            }

            ResistAbnormalStatus = new Label[MAX_ABNORMALSTATUS_NUM];
            ResistAbnormalStatusValue = new Label[MAX_ABNORMALSTATUS_NUM];
            for (int ii = 0; ii < MAX_ABNORMALSTATUS_NUM; ii++)
            {
                ResistAbnormalStatus[ii] = new Label();
                ResistAbnormalStatus[ii].Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                ResistAbnormalStatus[ii].Location = new System.Drawing.Point(370, 20 + 33 * (ii % MAX_ABNORMALSTATUS_NUM));
                ResistAbnormalStatus[ii].Name = "ResistAbnormalStatus" + ii.ToString();
                ResistAbnormalStatus[ii].AutoSize = true;
                ResistAbnormalStatus[ii].TabIndex = 0;
                this.grpResistStatus.Controls.Add(ResistAbnormalStatus[ii]);

                ResistAbnormalStatusValue[ii] = new Label();
                ResistAbnormalStatusValue[ii].Font = new System.Drawing.Font("MS UI Gothic", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                ResistAbnormalStatusValue[ii].Location = new System.Drawing.Point(470, 20 + 33 * (ii % MAX_ABNORMALSTATUS_NUM));
                ResistAbnormalStatusValue[ii].Name = "ResistAbnormalStatusValue" + ii.ToString();
                ResistAbnormalStatusValue[ii].AutoSize = true;
                ResistAbnormalStatusValue[ii].TabIndex = 0;
                this.grpResistStatus.Controls.Add(ResistAbnormalStatusValue[ii]);
            }
        }

        MainCharacter GetCurrentPlayer()
        {
            MainCharacter player = null;
            if (mc != null && mc.PlayerStatusColor == this.BackColor)
            {
                player = this.mc;
            }
            else if (sc != null && sc.PlayerStatusColor == this.BackColor)
            {
                player = this.sc;
            }
            else if (tc != null && tc.PlayerStatusColor == this.BackColor)
            {
                player = this.tc;
            }
            return player;
        }

        void StatusPlayer_Click(object sender, EventArgs e)
        {
            string fileExt = ".bmp";
            if (((MouseEventArgs)e).Button == System.Windows.Forms.MouseButtons.Right) return;

            MainCharacter player = GetCurrentPlayer();

            for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
            {
                if (((Label)sender).Name == "backpack" + ii.ToString())
                {
                    if (this.levelUp)
                    {
                        mainMessage.Text = player.GetCharacterSentence(2002);
                        return;
                    }

                    if (this.useOverShifting)
                    {
                        mainMessage.Text = player.GetCharacterSentence(2023);
                        return;
                    }

                    ItemBackPack backpackData = new ItemBackPack(((Label)sender).Text);

                    if (this.onlySelectTrash)
                    {
                        mainMessage.Text = string.Format(player.GetCharacterSentence(2030), ((Label)sender).Text); // mc.Name + "：" + ((Label)sender).Text + "を捨てて新しいアイテムを入手するか？";
                        using (YesNoRequest yesno = new YesNoRequest())
                        {
                            yesno.StartPosition = FormStartPosition.CenterParent;
                            yesno.ShowDialog();
                            if (yesno.DialogResult == DialogResult.Yes)
                            {
                                if (TruthItemAttribute.CheckImportantItem(backpackData.Name) == TruthItemAttribute.Transfer.Any)
                                {
                                    player.DeleteBackPack(backpackData, player.CheckBackPackExist(backpackData, ii), ii);
                                    ((Label)sender).Text = "";
                                    ((Label)sender).Cursor = System.Windows.Forms.Cursors.Default;
                                    this.DialogResult = DialogResult.OK;
                                }
                                else
                                {
                                    mainMessage.Text = player.GetCharacterSentence(2013);
                                    this.DialogResult = DialogResult.None;
                                }
                                return;
                            }
                            else
                            {
                                return;
                            }
                        }
                    }

                    if (backpackData.Name == "") return;

                    using (SelectAction sa = new SelectAction())
                    {
                        if (this.onlyUseItem) // 戦闘中は「つかう」しかさせない仕様で動作させる。「わたす」戦闘の最中でとっさの渡しは出来ない。「すてる」戦闘後に捨てるのが普通。
                        {
                            if ((backpackData.Type == ItemBackPack.ItemType.Use_Potion) ||
                                (backpackData.Type == ItemBackPack.ItemType.Use_Any))
                            {
                                // 使用品限り、使用可能とする。装備品は選択不可
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2032);
                                return;
                            }

                            sa.TargetNum = 0;
                        }
                        else
                        {
                            sa.StartPosition = FormStartPosition.Manual;
                            if ((this.Location.X + this.Size.Width - this.mousePosX) <= sa.Width) this.mousePosX = this.Location.X + this.Size.Width - sa.Width;
                            if ((this.Location.Y + this.Size.Height - this.mousePosY) <= sa.Height) this.mousePosY = this.Location.Y + this.Size.Height - sa.Height;
                            sa.Location = new Point(this.mousePosX, this.mousePosY);

                            if (backpackData.Type == ItemBackPack.ItemType.Armor_Middle
                                || backpackData.Type == ItemBackPack.ItemType.Armor_Light
                                || backpackData.Type == ItemBackPack.ItemType.Armor_Heavy
                                || backpackData.Type == ItemBackPack.ItemType.Accessory
                                || backpackData.Type == ItemBackPack.ItemType.Weapon_Heavy
                                || backpackData.Type == ItemBackPack.ItemType.Weapon_Light
                                || backpackData.Type == ItemBackPack.ItemType.Weapon_Middle)
                            {
                                sa.ElementA = "そうび";
                            }
                            else
                            {
                                sa.ElementA = "つかう";
                            }

                            if (!we.AvailableSecondCharacter && !we.AvailableThirdCharacter) // 1人しかいない場合、「わたす」コマンドではなく、「すてる」である。
                            {
                                sa.ElementB = "すてる";
                            }
                            else
                            {
                                sa.ElementB = "わたす";
                                sa.ElementC = "すてる";
                            }
                            sa.IsShift = this.IsShift;
                            sa.ShowDialog();
                            this.IsShift = sa.IsShift;
                        }

                        if (sa.TargetNum == 0) // つかう / そうび
                        {
                            // switch-case に進みます。
                        }
                        else if (sa.TargetNum == 1) // わたす
                        {
                            if (!we.AvailableSecondCharacter && !we.AvailableThirdCharacter) // 1人しかいない場合、「わたす」コマンドではなく、「すてる」である。
                            {
                                if (TruthItemAttribute.CheckImportantItem(backpackData.Name) == TruthItemAttribute.Transfer.Any)
                                {
                                    player.DeleteBackPack(backpackData, 1, ii);
                                    UsingItemUpdateBackPackLabel(player, backpackData, (Label)sender, ii);
                                }
                                else
                                {
                                    mainMessage.Text = player.GetCharacterSentence(2013);
                                }
                                return;
                            }
                            else // ここからが「わたす」コマンドである
                            {
                                int exchangeValue = CallBackPackExchangeValue(player, backpackData, ii);
                                if (exchangeValue <= -1) return;

                                MainCharacter target = null;
                                using (SelectTarget st = new SelectTarget())
                                {
                                    st.StartPosition = FormStartPosition.Manual;
                                    st.Location = new Point(this.mousePosX, this.mousePosY);
                                    if (we.AvailableThirdCharacter)
                                    {
                                        st.MaxSelectable = 3;
                                        st.FirstName = mc.Name;
                                        st.SecondName = sc.Name;
                                        st.ThirdName = tc.Name;
                                    }
                                    else
                                    {
                                        st.MaxSelectable = 2;
                                        st.FirstName = mc.Name;
                                        st.SecondName = sc.Name;
                                    }
                                    st.ShowDialog();
                                    if (st.TargetNum == 1)
                                    {
                                        if (mc != null && mc.PlayerStatusColor == this.BackColor)
                                        {
                                            return;
                                        }
                                        else
                                        {
                                            target = mc;
                                        }
                                    }
                                    else if (st.TargetNum == 2)
                                    {
                                        if (sc != null && sc.PlayerStatusColor == this.BackColor)
                                        {
                                            return;
                                        }
                                        else
                                        {
                                            target = sc;
                                        }
                                    }
                                    else if (st.TargetNum == 3)
                                    {
                                        if (tc != null && tc.PlayerStatusColor == this.BackColor)
                                        {
                                            return;
                                        }
                                        else
                                        {
                                            target = tc;
                                        }
                                    }
                                    else
                                    {
                                        // ESCキーの場合、なにもしません。
                                        return;
                                    }
                                }
                                if (backpackData.Name == Database.RARE_EARRING_OF_LANA && (target == sc || target == tc))
                                {
                                    mainMessage.Text = "アイン：（いや・・・これは渡さないでおこう。)";
                                    return;
                                }
                                if ((backpackData.Name == Database.POOR_PRACTICE_SWORD_ZERO && (target == sc || target == tc)) ||
                                    (backpackData.Name == Database.POOR_PRACTICE_SWORD_1 && (target == sc || target == tc)) ||
                                    (backpackData.Name == Database.POOR_PRACTICE_SWORD_2 && (target == sc || target == tc)) ||
                                    (backpackData.Name == Database.COMMON_PRACTICE_SWORD_3 && (target == sc || target == tc)) ||
                                    (backpackData.Name == Database.COMMON_PRACTICE_SWORD_4 && (target == sc || target == tc)) ||
                                    (backpackData.Name == Database.RARE_PRACTICE_SWORD_5 && (target == sc || target == tc)) ||
                                    (backpackData.Name == Database.RARE_PRACTICE_SWORD_6 && (target == sc || target == tc)) ||
                                    (backpackData.Name == Database.EPIC_PRACTICE_SWORD_7 && (target == sc || target == tc)) ||
                                    (backpackData.Name == Database.LEGENDARY_FELTUS && (target == sc || target == tc)))
                                {
                                    mainMessage.Text = "アイン：（いや・・・これは渡さないでおこう。)";
                                    return;
                                }

                                bool success = target.AddBackPack(backpackData, exchangeValue);
                                if (success)
                                {
                                    player.DeleteBackPack(backpackData, exchangeValue, ii);
                                    UsingItemUpdateBackPackLabel(player, backpackData, (Label)sender, ii);
                                }
                                else
                                {
                                    mainMessage.Text = String.Format(player.GetCharacterSentence(2003), target.Name);
                                }
                                return;
                            }
                        }
                        else if (sa.TargetNum == 2) // すてる
                        {
                            if (TruthItemAttribute.CheckImportantItem(backpackData.Name) == TruthItemAttribute.Transfer.Any)
                            {
                                int exchangeValue = CallBackPackExchangeValue(player, backpackData, ii);
                                if (exchangeValue <= -1) return;

                                player.DeleteBackPack(backpackData, exchangeValue, ii);
                                UsingItemUpdateBackPackLabel(player, backpackData, (Label)sender, ii);
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2013);
                            }
                            return;
                        }
                        else
                        {
                            // ESCキーキャンセルは何もしません。
                            return;
                        }
                    }

                    if (player.Dead)
                    {
                        mainMessage.Text = "【" + player.Name + "は死んでしまっているため、アイテムが使えない。】";
                        return;
                    }

                    switch (backpackData.Name)
                    {
                        case Database.POOR_SMALL_RED_POTION:
                        case Database.COMMON_NORMAL_RED_POTION:
                        case Database.COMMON_LARGE_RED_POTION:
                        case Database.COMMON_HUGE_RED_POTION:
                        case Database.COMMON_GORGEOUS_RED_POTION:
                        case Database.RARE_PERFECT_RED_POTION:
                        case "名前がとても長いわりにはまったく役に立たず、何の効果も発揮しない役立たずであるにもかかわらずデコレーションが長い超豪華なスーパーミラクルポーション":
                            int effect = backpackData.UseIt();
                            if (player.CurrentNourishSense > 0)
                            {
                                effect = (int)((double)effect * 1.3f);
                            }
                            player.CurrentLife += effect;
                            player.DeleteBackPack(backpackData, 1, ii);
                            this.life.Text = player.CurrentLife.ToString() + " / " + player.MaxLife.ToString();
                            mainMessage.Text = String.Format(player.GetCharacterSentence(2001), effect);
                            UsingItemUpdateBackPackLabel(player, backpackData, (Label)sender, ii);
                            RefreshPartyMembersLife();
                            break;

                        case Database.POOR_SMALL_BLUE_POTION:
                        case Database.COMMON_NORMAL_BLUE_POTION:
                        case Database.COMMON_LARGE_BLUE_POTION:
                        case Database.COMMON_HUGE_BLUE_POTION:
                        case Database.COMMON_GORGEOUS_BLUE_POTION:
                        case Database.RARE_PERFECT_BLUE_POTION:
                            effect = backpackData.UseIt();
                            player.CurrentMana += effect;
                            player.DeleteBackPack(backpackData, 1, ii);
                            this.mana.Text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();
                            mainMessage.Text = String.Format(player.GetCharacterSentence(2001), effect);
                            UsingItemUpdateBackPackLabel(player, backpackData, (Label)sender, ii);
                            RefreshPartyMembersLife();
                            break;

                        case Database.POOR_SMALL_GREEN_POTION:
                        case Database.COMMON_NORMAL_GREEN_POTION:
                        case Database.COMMON_LARGE_GREEN_POTION:
                        case Database.COMMON_HUGE_GREEN_POTION:
                        case Database.COMMON_GORGEOUS_GREEN_POTION:
                        case Database.RARE_PERFECT_GREEN_POTION:
                            effect = backpackData.UseIt();
                            player.CurrentSkillPoint += effect;
                            player.DeleteBackPack(backpackData, 1, ii);
                            this.skill.Text = player.CurrentSkillPoint.ToString() + " / " + player.MaxSkillPoint.ToString();
                            mainMessage.Text = String.Format(player.GetCharacterSentence(2001), effect);
                            UsingItemUpdateBackPackLabel(player, backpackData, (Label)sender, ii);
                            RefreshPartyMembersLife();
                            break;

                        case Database.COMMON_REVIVE_POTION_MINI:
                            MainCharacter target = null;
                            using (SelectTarget st = new SelectTarget())
                            {
                                st.StartPosition = FormStartPosition.Manual;
                                st.Location = new Point(this.mousePosX, this.mousePosY);
                                if (we.AvailableThirdCharacter)
                                {
                                    st.MaxSelectable = 3;
                                    st.FirstName = mc.Name;
                                    st.SecondName = sc.Name;
                                    st.ThirdName = tc.Name;
                                    st.ShowDialog();
                                }
                                else if (we.AvailableSecondCharacter)
                                {
                                    st.MaxSelectable = 2;
                                    st.FirstName = mc.Name;
                                    st.SecondName = sc.Name;
                                    st.ShowDialog();
                                }
                                else
                                {
                                    st.TargetNum = 1;
                                }

                                if (st.TargetNum == 1)
                                {
                                    target = mc;
                                }
                                else if (st.TargetNum == 2)
                                {
                                    target = sc;
                                }
                                else if (st.TargetNum == 3)
                                {
                                    target = tc;
                                }
                            }
                            if (target.Dead)
                            {
                                player.DeleteBackPack(backpackData, 1, ii);
                                target.ResurrectPlayer(1);
                                this.life.Text = target.CurrentLife.ToString() + " / " + target.MaxLife.ToString();
                                mainMessage.Text = target.GetCharacterSentence(2016);
                            }
                            else if (target == player)
                            {
                                mainMessage.Text = player.GetCharacterSentence(2018);
                            }
                            else
                            {
                                mainMessage.Text = String.Format(player.GetCharacterSentence(2017), target.Name);
                            }
                            break;

                        case Database.COMMON_POTION_MAGIC_SEAL:
                            if (this.onlyUseItem)
                            {
                                player.DeleteBackPack(backpackData, 1, ii);
                                player.AmplifyMagicAttack = 1.05f;
                                player.ActivateBuff(player.pbMagicAttackUp, Database.BaseResourceFolder + "BuffMagicAttackUp.bmp", Database.INFINITY);
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2011);
                            }
                            break;

                        case Database.COMMON_POTION_ATTACK_SEAL:
                            if (this.onlyUseItem)
                            {
                                player.DeleteBackPack(backpackData, 1, ii);
                                player.AmplifyPhysicalAttack = 1.05f;
                                player.ActivateBuff(player.pbPhysicalAttackUp, Database.BaseResourceFolder + "BuffPhysicalAttackUp.bmp", Database.INFINITY);
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2011);
                            }
                            break;


                        case Database.POOR_POTION_CURE_POISON:
                            if (this.onlyUseItem)
                            {
                                player.DeleteBackPack(backpackData, 1, ii);
                                player.CurrentPoison = 0;
                                player.CurrentPoisonValue = 0;
                                player.DeBuff(player.pbPoison);
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2011);
                            }
                            break;

                        case Database.COMMON_POTION_NATURALIZE:
                            if (this.onlyUseItem)
                            {
                                player.DeleteBackPack(backpackData, 1, ii);
                                player.CurrentPoison = 0;
                                player.CurrentPoisonValue = 0;
                                player.DeBuff(player.pbPoison);
                                player.CurrentSlow = 0;
                                player.DeBuff(player.pbSlow);
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2011);
                            }
                            break;

                        case Database.COMMON_POTION_CURE_BLIND:
                            if (this.onlyUseItem)
                            {
                                player.DeleteBackPack(backpackData, 1, ii);
                                player.CurrentBlind = 0;
                                player.DeBuff(player.pbBlind);
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2011);
                            }
                            break;

                        case Database.RARE_POTION_MOSSGREEN_DREAM:
                            if (this.onlyUseItem)
                            {
                                player.DeleteBackPack(backpackData, 1, ii);
                                player.CurrentSlow = 0;
                                player.DeBuff(player.pbSlow);
                                player.CurrentPoison = 0;
                                player.CurrentPoisonValue = 0;
                                player.DeBuff(player.pbPoison);
                                player.CurrentBlind = 0;
                                player.DeBuff(player.pbBlind);
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2011);
                            }
                            break;

                        case Database.COMMON_RESIST_POISON:
                            if (this.onlyUseItem)
                            {
                                player.DeleteBackPack(backpackData, 1, ii);
                                player.CurrentPoison = 0;
                                player.CurrentPoisonValue = 0;
                                player.DeBuff(player.pbPoison);
                                player.ResistPoison = true;
                                player.ActivateBuff(player.pbResistPoison, Database.BaseResourceFolder + "ResistPoison.bmp", Database.INFINITY);
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2011);
                            }
                            break;

                        case Database.COMMON_POTION_OVER_GROWTH:
                            if (this.onlyUseItem)
                            {
                                player.DeleteBackPack(backpackData, 1, ii);
                                player.CurrentStaminaUp = Database.INFINITY;
                                player.CurrentStaminaUpValue = 100; // スタミナUPは内部処理で10倍されてるため、ここでは1000/10で100
                                player.ActivateBuff(player.pbStaminaUp, Database.BaseResourceFolder + "BuffStaminaUp.bmp", Database.INFINITY);
                                player.labelLife.Text = player.CurrentLife.ToString();
                                if (player.CurrentLife >= player.MaxLife)
                                {
                                    player.labelLife.ForeColor = Color.Green;
                                }
                                else
                                {
                                    player.labelLife.ForeColor = Color.Black;
                                }
                                player.labelLife.Update();
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2011);
                            }
                            break;

                        case Database.COMMON_POTION_RAINBOW_IMPACT:
                            if (this.onlyUseItem)
                            {
                                player.DeleteBackPack(backpackData, 1, ii);
                                player.RemovePhysicalAttackDown();
                                player.RemoveMagicAttackDown();
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2011);
                            }
                            break;
                            
                        case Database.COMMON_POTION_BLACK_GAST:
                            if (this.onlyUseItem)
                            {
                                player.DeleteBackPack(backpackData, 1, ii);
                                player.AmplifyMagicAttack = 1.07f;
                                player.AmplifyPhysicalAttack = 1.07f;
                                player.ActivateBuff(player.pbMagicAttackUp, Database.BaseResourceFolder + "BuffMagicAttackUp.bmp", Database.INFINITY);
                                player.ActivateBuff(player.pbPhysicalAttackUp, Database.BaseResourceFolder + "BuffPhysicalAttackUp.bmp", Database.INFINITY);
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2011);
                            }
                            break;

                        case Database.COMMON_FAIRY_BREATH:
                            if (this.onlyUseItem)
                            {
                                player.DeleteBackPack(backpackData, 1, ii);
                                player.RemoveSilence();
                                player.ResistSilence = true;
                                player.ActivateBuff(player.pbResistSilence, Database.BaseResourceFolder + "ResistSilence.bmp", Database.INFINITY);
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2011);
                            }
                            break;

                        case Database.COMMON_HEART_ACCELERATION:
                            if (this.onlyUseItem)
                            {
                                player.DeleteBackPack(backpackData, 1, ii);
                                player.RemoveParalyze();
                                player.ResistParalyze = true;
                                player.ActivateBuff(player.pbResistParalyze, Database.BaseResourceFolder + "ResistParalyze.bmp", Database.INFINITY);
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2011);
                            }
                            break;

                        case Database.RARE_SAGE_POTION_MINI:
                            if (this.onlyUseItem)
                            {
                                player.DeleteBackPack(backpackData, 1, ii);
                                player.RemoveDebuffEffect();
                                player.RemoveDebuffParam();
                                player.RemoveDebuffSpell();
                                player.RemoveDebuffSkill();
                                player.CurrentSagePotionMini = Database.INFINITY;
                                player.CurrentNoResurrection = Database.INFINITY;
                                player.ActivateBuff(player.pbNoResurrection, Database.BaseResourceFolder + "NoResurrection.bmp", Database.INFINITY);
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2011);
                            }
                            break;

                        case Database.RARE_POWER_SURGE:
                            if (this.onlyUseItem)
                            {
                                player.DeleteBackPack(backpackData, 1, ii);
                                player.BuffUpStrength(600);
                                player.BuffUpStamina(400);
                                player.BuffUpAmplifyPhysicalAttack(1.20f);
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2011);
                            }
                            break;

                        case Database.RARE_ZEPHER_BREATH:
                            if (this.onlyUseItem)
                            {
                                player.DeleteBackPack(backpackData, 1, ii);
                                player.BuffUpAgility(600);
                                player.BuffUpIntelligence(400);
                                player.BuffUpAmplifyBattleSpeed(1.20f);
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2011);
                            }
                            break;

                        case Database.RARE_GENSEI_MAGIC_BOTTLE:
                            if (this.onlyUseItem)
                            {
                                player.DeleteBackPack(backpackData, 1, ii);
                                player.BuffUpIntelligence(600);
                                player.BuffUpMind(400);
                                player.BuffUpAmplifyMagicAttack(1.20f);
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2011);
                            }
                            break;

                        case Database.RARE_ZETTAI_STAMINAUP:
                            if (this.onlyUseItem)
                            {
                                player.DeleteBackPack(backpackData, 1, ii);
                                player.BuffUpStrength(200);
                                player.BuffUpIntelligence(200);
                                player.BuffUpStamina(600);
                                player.BuffUpAmplifyPhysicalDefence(1.10f);
                                player.BuffUpAmplifyMagicDefense(1.10f);
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2011);
                            }
                            break;

                        case Database.RARE_MIND_ILLUSION:
                            if (this.onlyUseItem)
                            {
                                player.DeleteBackPack(backpackData, 1, ii);
                                player.BuffUpStrength(100);
                                player.BuffUpAgility(100);
                                player.BuffUpIntelligence(100);
                                player.BuffUpStamina(100);
                                player.BuffUpMind(600);
                                player.BuffUpAmplifyPotential(1.20f);
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2011);
                            }                           
                            break;

                        case Database.RARE_GENSEI_TAIMA_KUSURI:
                            if (this.onlyUseItem)
                            {
                                player.DeleteBackPack(backpackData, 1, ii);
                                player.CurrentGenseiTaima = Database.INFINITY;
                                player.ActivateBuff(player.pbGenseiTaima, Database.BaseResourceFolder + Database.ITEMCOMMAND_GENSEI_TAIMA + fileExt, Database.INFINITY);
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2011);
                            }                           
                            break;

                        case Database.RARE_SHINING_AETHER:
                            if (this.onlyUseItem)
                            {
                                player.DeleteBackPack(backpackData, 1, ii);
                                player.CurrentShiningAether = 2; // 次のターンまで有効
                                player.ActivateBuff(player.pbShiningAether, Database.BaseResourceFolder + Database.ITEMCOMMAND_SHINING_AETHER + fileExt, 2);
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2011);
                            }                           
                            break;

                        case Database.RARE_BLACK_ELIXIR:
                            if (this.onlyUseItem)
                            {
                                player.DeleteBackPack(backpackData, 1, ii);
                                player.CurrentBlackElixir = Database.INFINITY;
                                player.CurrentBlackElixirValue = player.MaxLife / 2;
                                player.CurrentLife += player.CurrentBlackElixirValue;
                                player.ActivateBuff(player.pbBlackElixir, Database.BaseResourceFolder + Database.ITEMCOMMAND_BLACK_ELIXIR + fileExt, Database.INFINITY);
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2011);
                            }                           
                            break;

                        case Database.RARE_ELEMENTAL_SEAL:
                            if (this.onlyUseItem)
                            {
                                player.DeleteBackPack(backpackData, 1, ii);
                                player.RemoveDebuffEffect();
                                player.CurrentElementalSeal = Database.INFINITY;
                                player.ActivateBuff(player.pbElementalSeal, Database.BaseResourceFolder + Database.ITEMCOMMAND_ELEMENTAL_SEAL + fileExt, Database.INFINITY);
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2011);
                            } 
                            break;

                        case Database.RARE_COLORESS_ANTIDOTE:
                            if (this.onlyUseItem)
                            {
                                player.DeleteBackPack(backpackData, 1, ii);
                                player.RemoveDebuffParam();
                                player.CurrentColoressAntidote = Database.INFINITY;
                                player.ActivateBuff(player.pbColoressAntidote, Database.BaseResourceFolder + Database.ITEMCOMMAND_COLORESS_ANTIDOTE + fileExt, Database.INFINITY);
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2011);
                            } 
                            break;

                        case Database.RARE_TOTAL_HIYAKU_KASSEI:
                            if (this.onlyUseItem)
                            {
                                int maxValue = Math.Max(player.Strength,
                                               Math.Max(player.Agility,
                                                        player.Intelligence));
                                if (maxValue == player.Strength)
                                {
                                    player.BuffStrength_Hiyaku_Kassei = maxValue;
                                }
                                else if (maxValue == player.Agility)
                                {
                                    player.BuffAgility_Hiyaku_Kassei = maxValue;
                                }
                                else if (maxValue == player.Intelligence)
                                {
                                    player.BuffIntelligence_Hiyaku_Kassei = maxValue;
                                }
                                player.DeleteBackPack(backpackData, 1, ii);
                                this.skill.Text = player.CurrentSkillPoint.ToString() + " / " + player.MaxSkillPoint.ToString();
                                UsingItemUpdateBackPackLabel(player, backpackData, (Label)sender, ii);
                                RefreshPartyMembersLife();
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2011);
                            }
                            break;

                        case "神聖水": // ２階アイテム
                            if (!we.AlreadyUseSyperSaintWater)
                            {
                                we.AlreadyUseSyperSaintWater = true;
                                player.CurrentLife += (int)((double)player.MaxLife * 0.3F);
                                player.CurrentMana += (int)((double)player.MaxMana * 0.3F);
                                player.CurrentSkillPoint += (int)((double)player.MaxSkillPoint * 0.3F);
                                this.life.Text = player.CurrentLife.ToString() + " / " + player.MaxLife.ToString();
                                this.mana.Text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();
                                this.skill.Text = player.CurrentSkillPoint.ToString() + " / " + player.MaxSkillPoint.ToString();
                                RefreshPartyMembersLife();
                                mainMessage.Text = player.GetCharacterSentence(2009);
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2010);
                            }
                            break;

                        case Database.RARE_PURE_WATER:
                            if (!we.AlreadyUsePureWater)
                            {
                                we.AlreadyUsePureWater = true;
                                player.CurrentLife = (int)((double)player.MaxLife);
                                this.life.Text = player.CurrentLife.ToString() + " / " + player.MaxLife.ToString();
                                mainMessage.Text = player.GetCharacterSentence(2027);
                                RefreshPartyMembersLife();
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2028);
                            }
                            break;

                        case "リヴァイヴポーション":
                            if (!we.AlreadyUseRevivePotion)
                            {
                                target = null;
                                using (SelectTarget st = new SelectTarget())
                                {
                                    st.StartPosition = FormStartPosition.Manual;
                                    st.Location = new Point(this.mousePosX, this.mousePosY);
                                    if (we.AvailableThirdCharacter)
                                    {
                                        st.MaxSelectable = 3;
                                        st.FirstName = mc.Name;
                                        st.SecondName = sc.Name;
                                        st.ThirdName = tc.Name;
                                        st.ShowDialog();
                                    }
                                    else if (we.AvailableSecondCharacter)
                                    {
                                        st.MaxSelectable = 2;
                                        st.FirstName = mc.Name;
                                        st.SecondName = sc.Name;
                                        st.ShowDialog();
                                    }
                                    else
                                    {
                                        st.TargetNum = 1;
                                    }

                                    if (st.TargetNum == 1)
                                    {
                                        target = mc;
                                    }
                                    else if (st.TargetNum == 2)
                                    {
                                        target = sc;
                                    }
                                    else if (st.TargetNum == 3)
                                    {
                                        target = tc;
                                    }
                                }
                                if (target.Dead)
                                {
                                    we.AlreadyUseRevivePotion = true;
                                    target.Dead = false;
                                    target.CurrentLife = target.MaxLife / 2;
                                    this.life.Text = target.CurrentLife.ToString() + " / " + target.MaxLife.ToString();
                                    mainMessage.Text = target.GetCharacterSentence(2016);
                                }
                                else if (target == player)
                                {
                                    mainMessage.Text = player.GetCharacterSentence(2018);
                                }
                                else
                                {
                                    mainMessage.Text = String.Format(player.GetCharacterSentence(2017), target.Name);
                                }
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2010);
                            }
                            break;

                        case Database.EPIC_OVER_SHIFTING:
                            StatusButton_Click(StatusButton, null);
                            this.Update();

                            this.useOverShifting = true;
                            button1.Visible = false;
                            mainMessage.Text = player.GetCharacterSentence(2022);
                            this.Update();
                            System.Threading.Thread.Sleep(500);
                            int firstStrength = 1;
                            int firstAgility = 1;
                            int firstIntelligence = 1;
                            int firstStamina = 1;
                            int firstMind = 1;
                            if (player.Equals(mc))
                            {
                                firstStrength = Database.MAINPLAYER_FIRST_STRENGTH;
                                firstAgility = Database.MAINPLAYER_FIRST_AGILITY;
                                firstIntelligence = Database.MAINPLAYER_FIRST_INTELLIGENCE;
                                firstStamina = Database.MAINPLAYER_FIRST_STAMINA;
                                firstMind = Database.MAINPLAYER_FIRST_MIND;
                            }
                            else if (player.Equals(sc))
                            {
                                firstStrength = Database.SECONDPLAYER_FIRST_STRENGTH;
                                firstAgility = Database.SECONDPLAYER_FIRST_AGILITY;
                                firstIntelligence = Database.SECONDPLAYER_FIRST_INTELLIGENCE;
                                firstStamina = Database.SECONDPLAYER_FIRST_STAMINA;
                                firstMind = Database.SECONDPLAYER_FIRST_MIND;
                            }
                            else if (player.Equals(tc))
                            {
                                firstStrength = Database.THIRDPLAYER_FIRST_STRENGTH;
                                firstAgility = Database.THIRDPLAYER_FIRST_AGILITY;
                                firstIntelligence = Database.THIRDPLAYER_FIRST_INTELLIGENCE;
                                firstStamina = Database.THIRDPLAYER_FIRST_STAMINA;
                                firstMind = Database.THIRDPLAYER_FIRST_MIND;
                            }
                            while (true)
                            {
                                if (player.Strength <= firstStrength)
                                {
                                    if (player.Agility <= firstAgility)
                                    {
                                        if (player.Intelligence <= firstIntelligence)
                                        {
                                            if (player.Stamina <= firstStamina)
                                            {
                                                if (player.Mind <= firstMind)
                                                {
                                                    break;
                                                }
                                                else
                                                {
                                                    player.Mind--;
                                                    this.mindLabel.Text = player.Mind.ToString();
                                                    this.mindLabel.Update();
                                                }
                                            }
                                            else
                                            {
                                                player.Stamina--;
                                                this.stamina.Text = player.Stamina.ToString();
                                                this.stamina.Update();
                                            }
                                        }
                                        else
                                        {
                                            player.Intelligence--;
                                            this.intelligence.Text = player.Intelligence.ToString();
                                            this.intelligence.Update();
                                        }
                                    }
                                    else
                                    {
                                        player.Agility--;
                                        this.agility.Text = player.Agility.ToString();
                                        this.agility.Update();
                                    }
                                }
                                else
                                {
                                    player.Strength--;
                                    this.strength.Text = player.Strength.ToString();
                                    this.strength.Update();
                                }
                                this.upPoint++;
                                System.Threading.Thread.Sleep(1);
                                Application.DoEvents();
                            }
                            buttonStrength.Enabled = true;
                            buttonAgility.Enabled = true;
                            buttonIntelligence.Enabled = true;
                            buttonStamina.Enabled = true;
                            buttonMind.Enabled = true;
                            player.DeleteBackPack(backpackData, 1, ii);
                            UsingItemUpdateBackPackLabel(player, backpackData, (Label)sender, ii);
                            SettingCharacterData(player);
                            RefreshPartyMembersBattleStatus(player);
                            break;

                        case Database.GROWTH_LIQUID_STRENGTH:
                        case Database.GROWTH_LIQUID2_STRENGTH:
                        case Database.GROWTH_LIQUID3_STRENGTH:
                        case Database.GROWTH_LIQUID4_STRENGTH:
                        case Database.GROWTH_LIQUID5_STRENGTH:
                            int effectValue = backpackData.MinValue + AP.Math.RandomInteger(backpackData.MaxValue - backpackData.MinValue + 1);
                            player.Strength += effectValue;
                            player.DeleteBackPack(backpackData, 1, ii);
                            SettingCharacterData(player);
                            RefreshPartyMembersBattleStatus(player);
                            mainMessage.Text = String.Format(player.GetCharacterSentence(2024), "力", effectValue.ToString());
                            break;
                        case Database.GROWTH_LIQUID_AGILITY:
                        case Database.GROWTH_LIQUID2_AGILITY:
                        case Database.GROWTH_LIQUID3_AGILITY:
                        case Database.GROWTH_LIQUID4_AGILITY:
                        case Database.GROWTH_LIQUID5_AGILITY:
                            effectValue = backpackData.MinValue + AP.Math.RandomInteger(backpackData.MaxValue - backpackData.MinValue + 1);
                            player.Agility += effectValue;
                            player.DeleteBackPack(backpackData, 1, ii);
                            SettingCharacterData(player);
                            RefreshPartyMembersBattleStatus(player);
                            mainMessage.Text = String.Format(player.GetCharacterSentence(2024), "技", effectValue.ToString());
                            break;
                        case Database.GROWTH_LIQUID_INTELLIGENCE:
                        case Database.GROWTH_LIQUID2_INTELLIGENCE:
                        case Database.GROWTH_LIQUID3_INTELLIGENCE:
                        case Database.GROWTH_LIQUID4_INTELLIGENCE:
                        case Database.GROWTH_LIQUID5_INTELLIGENCE:
                            effectValue = backpackData.MinValue + AP.Math.RandomInteger(backpackData.MaxValue - backpackData.MinValue + 1);
                            player.Intelligence += effectValue;
                            player.DeleteBackPack(backpackData, 1, ii);
                            SettingCharacterData(player);
                            RefreshPartyMembersBattleStatus(player);
                            mainMessage.Text = String.Format(player.GetCharacterSentence(2024), "知", effectValue.ToString());
                            break;
                        case Database.GROWTH_LIQUID_STAMINA:
                        case Database.GROWTH_LIQUID2_STAMINA:
                        case Database.GROWTH_LIQUID3_STAMINA:
                        case Database.GROWTH_LIQUID4_STAMINA:
                        case Database.GROWTH_LIQUID5_STAMINA:
                            effectValue = backpackData.MinValue + AP.Math.RandomInteger(backpackData.MaxValue - backpackData.MinValue + 1);
                            player.Stamina += effectValue;
                            player.DeleteBackPack(backpackData, 1, ii);
                            SettingCharacterData(player);
                            RefreshPartyMembersBattleStatus(player);
                            mainMessage.Text = String.Format(player.GetCharacterSentence(2024), "体", effectValue.ToString());
                            break;
                        case Database.GROWTH_LIQUID_MIND:
                        case Database.GROWTH_LIQUID2_MIND:
                        case Database.GROWTH_LIQUID3_MIND:
                        case Database.GROWTH_LIQUID4_MIND:
                        case Database.GROWTH_LIQUID5_MIND:
                            effectValue = backpackData.MinValue + AP.Math.RandomInteger(backpackData.MaxValue - backpackData.MinValue + 1);
                            player.Mind += effectValue;
                            player.DeleteBackPack(backpackData, 1, ii);
                            SettingCharacterData(player);
                            RefreshPartyMembersBattleStatus(player);
                            mainMessage.Text = String.Format(player.GetCharacterSentence(2024), "心", effectValue.ToString());
                            break;

                        // 装備品：武器
                        case "練習用の剣": // アイン初期装備
                        case "ナックル": // ラナ初期装備
                        case "白銀の剣（レプリカ）": // ヴェルゼ初期装備
                        case "ショートソード": // ガンツの武具屋販売（ダンジョン１階）
                        case "洗練されたロングソード": // ガンツの武具屋販売（ダンジョン１階）
                        case "青銅の剣": // ガンツの武具屋販売（ダンジョン２階）
                        case "メタルフィスト": // ガンツの武具屋販売（ダンジョン２階）
                        case "プラチナソード": // ガンツの武具屋販売（ダンジョン３階）
                        case "ファルシオン": // ガンツの武具屋販売（ダンジョン３階）
                        case "アイアンクロー": // ガンツの武具屋販売（ダンジョン３階）
                        case "シャムシール": // ３階アイテム
                        case "ライトプラズマブレード": // ガンツの武具屋販売（ダンジョン４階）
                        case "イスリアルフィスト": // ガンツの武具屋販売（ダンジョン４階）
                        case "エスパダス": // ダンジョン４階のアイテム
                        case "ソード・オブ・ブルールージュ": // ダンジョン４階のアイテム
                        case "ルナ・エグゼキュージョナー": // ダンジョン５階
                        case "蒼黒・氷大蛇の爪": // ダンジョン５階
                        case "ファージル・ジ・エスペランザ": // ダンジョン５階
                        case "神剣  フェルトゥーシュ":
                        case "双剣  ジュノセレステ":
                        case "極剣  ゼムルギアス":
                        case "クロノス・ロマティッド・ソード":
                        // 装備品：防具
                        case "黒真空の鎧（レプリカ）": // ヴェルゼ初期装備
                        case "コート・オブ・プレート": // アイン初期装備
                        case "ライト・クロス": // ラナ初期装備
                        case "冒険者用の鎖かたびら": // ガンツの武具屋販売（ダンジョン１階）
                        case "青銅の鎧": // ガンツの武具屋販売（ダンジョン１階）
                        case "真鍮の鎧": // ２階アイテム
                        case "光沢のある鉄のプレート": // ガンツの武具屋販売（ダンジョン２階）
                        case "シルクの武道衣": // ガンツの武具屋販売（ダンジョン２階）
                        case "シルバーアーマー": // ガンツの武具屋販売（ダンジョン３階）
                        case "獣皮製の舞踏衣": // ガンツの武具屋販売（ダンジョン３階）
                        case "フィスト・クロス": // ガンツの武具屋販売（ダンジョン３階）
                        case "プレート・アーマー": // ３階アイテム
                        case "ラメラ・アーマー": // ３階アイテム
                        case "プリズマティックアーマー": // ガンツの武具屋販売（ダンジョン４階）
                        case "極薄合金製の羽衣": // ガンツの武具屋販売（ダンジョン４階）
                        case "アヴォイド・クロス": // ダンジョン４階のアイテム
                        case "ブリガンダィン": // ダンジョン４階のアイテム
                        case "ロリカ・セグメンタータ": // ダンジョン４階のアイテム
                        case "ヘパイストス・パナッサロイニ":
                        // 装備品：アクセサリ
                        case "珊瑚のブレスレット": // ラナ初期装備
                        case "天空の翼（レプリカ）": // ヴェルゼ初期装備
                        case Database.COMMON_CHARM_OF_FIRE_ANGEL: // １階アイテム e 後編編集
                        case "チャクラオーブ": // １階アイテム
                        case "些細なパワーリング": // ガンツの武具屋販売（ダンジョン１階）
                        case "紺碧のスターエムブレム": // ガンツの武具屋販売（ダンジョン２階）
                        case "闘魂バンド": // ガンツの武具屋販売（ダンジョン２階）
                        case "鷹の刻印": // ２階アイテム
                        case "身かわしのマント": // ２階アイテム
                        case "ライオンハート": // ３階アイテム
                        case "オーガの腕章": // ３階アイテム
                        case "鋼鉄の石像": // ３階アイテム
                        case "ファラ様信仰のシール": // ３階アイテム
                        case "ウェルニッケの腕輪": // ガンツの武具屋販売（ダンジョン３階）
                        case "賢者の眼鏡": // ガンツの武具屋販売（ダンジョン３階）
                        case "七色プリズムバンド": // ガンツの武具屋販売（ダンジョン４階）
                        case "再生の紋章": // ガンツの武具屋販売（ダンジョン４階）
                        case "シールオブアクア＆ファイア": // ガンツの武具屋販売（ダンジョン４階）
                        case "ドラゴンのベルト": // ガンツの武具屋販売（ダンジョン４階）
                        case "剣紋章ペンダント": // ラナレベルアップ時でもらえるアイテム
                        case "夢見の印章": // ダンジョン４階のアイテム
                        case "天使の契約書": // ダンジョン４階のアイテム
                        case "ラナのイヤリング": // ダンジョン５階（ラナのイベント） // ラナ専用
                        case "レジェンド・レッドホース": // ダンジョン５階のアイテム
                        case "エルミ・ジョルジュ　ファージル王家の刻印":
                        case "ファラ・フローレ　天使のペンダント":
                        case "シニキア・カールハンツ　魔道デビルアイ":
                        case "オル・ランディス　炎神グローブ":
                        case "ヴェルゼ・アーティ　天空の翼":
                        // s 後編追加
                        case Database.POOR_PRACTICE_SHILED:

                        case Database.POOR_HINJAKU_ARMRING:
                        case Database.POOR_USUYOGORETA_FEATHER:
                        case Database.POOR_NON_BRIGHT_ORB:
                        case Database.POOR_KUKEI_BANGLE:
                        case Database.POOR_SUTERARESHI_EMBLEM:
                        case Database.POOR_ARIFURETA_STATUE:
                        case Database.POOR_NON_ADJUST_BELT:
                        case Database.POOR_SIMPLE_EARRING:
                        case Database.POOR_KATAKUZURESHITA_FINGERRING:
                        case Database.POOR_IROASETA_CHOKER:
                        case Database.POOR_YOREYORE_MANTLE:
                        case Database.POOR_NON_HINSEI_CROWN:
                        case Database.POOR_TUKAIFURUSARETA_SWORD:
                        case Database.POOR_TUKAINIKUI_LONGSWORD:
                        case Database.POOR_GATAGAKITERU_ARMOR:
                        case Database.POOR_FESTERING_ARMOR:
                        case Database.POOR_HINSO_SHIELD:
                        case Database.POOR_MUDANIOOKII_SHIELD:

                        case Database.COMMON_RED_PENDANT:
                        case Database.COMMON_BLUE_PENDANT:
                        case Database.COMMON_PURPLE_PENDANT:
                        case Database.COMMON_GREEN_PENDANT:
                        case Database.COMMON_YELLOW_PENDANT:
                        case Database.COMMON_SISSO_ARMRING:
                        case Database.COMMON_FINE_FEATHER:
                        case Database.COMMON_KIREINA_ORB:
                        case Database.COMMON_FIT_BANGLE:
                        case Database.COMMON_PRISM_EMBLEM:
                        case Database.COMMON_FINE_SWORD:
                        case Database.COMMON_TWEI_SWORD:
                        case Database.COMMON_FINE_ARMOR:
                        case Database.COMMON_GOTHIC_PLATE:
                        case Database.COMMON_FINE_SHIELD:
                        case Database.COMMON_GRIPPING_SHIELD:

                        case Database.RARE_JOUSITU_BLUE_POWERRING:
                        case Database.RARE_KOUJOUSINYADORU_RED_ORB:
                        case Database.RARE_MAGICIANS_MANTLE:
                        case Database.RARE_BEATRUSH_BANGLE:
                        case Database.RARE_AERO_BLADE:
                        case Database.RARE_SUN_BRAVE_ARMOR:
                        case Database.RARE_ESMERALDA_SHIELD:

                        case Database.EPIC_RING_OF_OSCURETE:
                        case Database.EPIC_MERGIZD_SOL_BLADE:

                        case Database.COMMON_SIMPLE_BRACELET:
                        case Database.POOR_HARD_SHOES:
                        case Database.COMMON_SEAL_OF_POSION:
                            // e 後編追加
                            if (((backpackData.Type == ItemBackPack.ItemType.Weapon_Heavy) && (player == sc || player == tc)) // アイン専用
                                || ((backpackData.Type == ItemBackPack.ItemType.Weapon_Light) && (player == mc || player == tc)) // ラナ専用
                                || ((backpackData.Type == ItemBackPack.ItemType.Weapon_Middle) && (player == mc || player == sc)) // ヴェルゼ専用
                                || ((backpackData.Type == ItemBackPack.ItemType.Armor_Heavy) && (player == sc || player == tc)) // アイン専用
                                || ((backpackData.Type == ItemBackPack.ItemType.Armor_Light) && (player == mc || player == tc)) // ラナ専用
                                || ((backpackData.Type == ItemBackPack.ItemType.Armor_Middle) && (player == mc || player == sc)) // ヴェルゼ専用
                                || ((backpackData.EquipablePerson == ItemBackPack.Equipable.Ein) && (player == sc || player == tc)) // アイン専用
                                || ((backpackData.EquipablePerson == ItemBackPack.Equipable.Lana) && (player == mc || player == tc)) // ラナ専用
                                || ((backpackData.EquipablePerson == ItemBackPack.Equipable.Verze) && (player == mc || player == sc))) // ヴェルゼ専用
                            {
                                mainMessage.Text = player.GetCharacterSentence(2019);
                                return;
                            }
                            EquipDecision(player, backpackData, sender, ii);
                            break;

                        // その他
                        case "ブラックマテリアル": // １階ドロップアイテム
                        case "ブルーマテリアル": // １階アイテム
                        case "レッドマテリアル": // ３階アイテム
                        case "グリーンマテリアル": // ダンジョン４階のアイテム
                        case "タイム・オブ・ルーセ": // ダンジョン５階の隠しアイテム
                            mainMessage.Text = player.GetCharacterSentence(2007);
                            break;

                        case "リーベストランクポーション":
                        case "アカシジアの実":
                            mainMessage.Text = player.GetCharacterSentence(2011);
                            break;

                        case Database.RARE_TOOMI_BLUE_SUISYOU: // 初期ラナ会話イベントで入手アイテム
                            if (we.dungeonEvent4_SlayBoss3)
                            {
                                mainMessage.Text = "アイン：ダメだ。ラナが囚われたままだ。助けるまではもう街へは帰らねえ。";
                                return; 
                            }
                            if (we.CompleteSlayBoss5)
                            {
                                mainMessage.Text = player.GetCharacterSentence(2020);
                                return;
                            }
                            if ((we.dungeonEvent329 && !we.dungeonEvent328) ||
                                (we.dungeonEvent318 && !we.dungeonEvent312))
                            {
                                mainMessage.Text = player.GetCharacterSentence(2020);
                                return;
                            }

                            mainMessage.Text = player.GetCharacterSentence(2006);
                            using (YesNoRequest yesno = new YesNoRequest())
                            {
                                yesno.StartPosition = FormStartPosition.CenterParent;
                                yesno.ShowDialog();
                                if (yesno.DialogResult == DialogResult.Yes)
                                {
                                    if (we.SaveByDungeon)
                                    {
                                        this.DialogResult = DialogResult.Abort;
                                        return;
                                    }
                                    else
                                    {
                                        mainMessage.Text = player.GetCharacterSentence(2012);
                                        return;
                                    }
                                }
                                else
                                {
                                    mainMessage.Text = "";
                                }
                            }
                            break;


                    }
                    break;
                }
            }
            if (this.onlyUseItem)
            {
                if (player.labelLife != null)
                {
                    player.labelLife.Text = player.CurrentLife.ToString();
                    if (player.CurrentLife >= player.MaxLife)
                    {
                        player.labelLife.ForeColor = Color.Green;
                    }
                    else
                    {
                        player.labelLife.ForeColor = Color.Black;
                    }
                    player.labelLife.Update();
                }

                this.DialogResult = DialogResult.OK;
            }
        }

        private int CallBackPackExchangeValue(MainCharacter player, ItemBackPack backpack, int ii)
        {
            int exchangeValue = player.CheckBackPackExist(backpack, ii); // [警告] backpackData.StackValueでは無い事は分かりにくい。
            if (IsShift)
            {
                using (SelectValue sv = new SelectValue())
                {
                    sv.StartPosition = FormStartPosition.Manual;
                    sv.Location = new Point(this.mousePosX, this.mousePosY);
                    sv.MaxValue = exchangeValue;
                    sv.ShowDialog();
                    IsShift = false; // ShowDialog表示先で、Shiftキーは外された場合検知できないため、ココでリセット。
                    if (sv.DialogResult == DialogResult.Cancel) return -1; // ESCキャンセルは中断とみなす。
                    exchangeValue = sv.CurrentValue;
                }
            }
            return exchangeValue;
        }

        private void EquipDecision(MainCharacter player, ItemBackPack backpackData, Object sender, int ii)
        {
            player.GetCharacterSentence(2004);
            using (YesNoRequest yesno = new YesNoRequest())
            {
                yesno.StartPosition = FormStartPosition.CenterParent;
                yesno.ShowDialog();
                if (yesno.DialogResult == DialogResult.Yes)
                {
                    // [警告]：武器・防具・アクセサリが違うだけなので、関数統一化を実施してください。
                    ItemBackPack tempItem = null;
                    if (backpackData.Type == ItemBackPack.ItemType.Weapon_Heavy
                        || backpackData.Type == ItemBackPack.ItemType.Weapon_Light
                        || backpackData.Type == ItemBackPack.ItemType.Weapon_Middle)
                    {
                        if (player.MainWeapon != null)
                        {
                            tempItem = new ItemBackPack(player.MainWeapon.Name);
                        }
                        player.MainWeapon = backpackData;
                        weapon.Text = player.MainWeapon.Name;
                        UpdateLabelColorForRare(ref this.weapon, player.MainWeapon.Rare);
                    }
                    else if (backpackData.Type == ItemBackPack.ItemType.Armor_Heavy
                        || backpackData.Type == ItemBackPack.ItemType.Armor_Light
                        || backpackData.Type == ItemBackPack.ItemType.Armor_Middle)
                    {
                        if (player.MainArmor != null)
                        {
                            tempItem = new ItemBackPack(player.MainArmor.Name);
                        }
                        player.MainArmor = backpackData;
                        armor.Text = player.MainArmor.Name;
                        UpdateLabelColorForRare(ref this.armor, player.MainArmor.Rare);
                    }
                    else if (backpackData.Type == ItemBackPack.ItemType.Shield)
                    {
                        if (player.SubWeapon != null)
                        {
                            tempItem = new ItemBackPack(player.SubWeapon.Name);
                        }
                        player.SubWeapon = backpackData;
                        subWeapon.Text = player.SubWeapon.Name;
                        UpdateLabelColorForRare(ref this.subWeapon, player.SubWeapon.Rare);
                    }
                    else if (backpackData.Type == ItemBackPack.ItemType.Accessory)
                    {
                        if (player.Accessory == null)
                        {
                            if (player.Accessory2 == null)
                            {
                                // 両方とも空の場合は、Accessoryを対象とする。
                                player.Accessory = backpackData;
                                accessory.Text = player.Accessory.Name;
                            }
                            else
                            {
                                // Accessory2があってもAccessoryが空なら、Accessoryを対象とする。
                                player.Accessory = backpackData;
                                accessory.Text = player.Accessory.Name;
                            }
                        }
                        else
                        {
                            if (player.Accessory2 == null)
                            {
                                // Accessoryが埋まっていてAccessory2が空なら、Accessory2を対象とする。
                                player.Accessory2 = backpackData;
                                accessory2.Text = player.Accessory2.Name;
                            }
                            else
                            {
                                // 両方とも埋まっている場合は、Accessoryを対象とする。
                                tempItem = new ItemBackPack(player.Accessory.Name);
                                player.Accessory = backpackData;
                                accessory.Text = player.Accessory.Name;
                            }
                        }

                        // UpdateLabelColorForRare(ref accessory, player.Accessory.Rare); // 後編削除                   
                    }
                    else
                    {
                        // [警告] 将来装備する箇所が増えた場合、随時対応してください。
                        mainMessage.Text = "";
                        return;
                    }

                    player.DeleteBackPack(backpackData, 1, ii);
                    // [警告]：nullオブジェクトなのか、Name空文字なのかハッキリ修正してください。
                    if (tempItem != null)
                    {
                        if (tempItem.Name != String.Empty)
                        {
                            player.AddBackPack(tempItem);
                        }
                        else
                        {
                            ((Label)sender).Text = "";
                            ((Label)sender).Cursor = System.Windows.Forms.Cursors.Default;
                            backpackStack[ii].Text = "";
                            backpackIcon[ii].Image = null;
                            backpackIcon[ii].Update();
                        }
                    }
                    else
                    {
                        ((Label)sender).Text = "";
                        ((Label)sender).Cursor = System.Windows.Forms.Cursors.Default;
                        backpackStack[ii].Text = "";
                        backpackIcon[ii].Image = null;
                        backpackIcon[ii].Update();
                    }
                    UpdateBackPackLabel(player);
                    SettingCharacterData(player);
                    RefreshPartyMembersBattleStatus(player);
                    mainMessage.Text = player.GetCharacterSentence(2005);
                }
                else
                {
                    mainMessage.Text = "";
                }
            }
        }

        private void UpdateLabelColorForRare(ref Label label, ItemBackPack.RareLevel rareLevel)
        {
            switch (rareLevel)
            {
                case ItemBackPack.RareLevel.Poor:
                    label.BackColor = Color.Gray;
                    label.ForeColor = Color.White;
                    break;
                case ItemBackPack.RareLevel.Common:
                    label.BackColor = Color.Green;
                    label.ForeColor = Color.White;
                    break;
                case ItemBackPack.RareLevel.Rare:
                    label.BackColor = Color.DarkBlue;
                    label.ForeColor = Color.White;
                    break;
                case ItemBackPack.RareLevel.Epic:
                    label.BackColor = Color.Purple;
                    label.ForeColor = Color.White;
                    break;
                case ItemBackPack.RareLevel.Legendary:
                    label.BackColor = Color.OrangeRed;
                    label.ForeColor = Color.White;
                    break;
            }
        }

        private void UsingItemUpdateBackPackLabel(MainCharacter player, ItemBackPack backpackData, Label sender, int ii)
        {
            int stackValue = player.CheckBackPackExist(backpackData, ii);
            if (stackValue <= 0)
            {
                ((Label)sender).Text = "";
                ((Label)sender).Cursor = System.Windows.Forms.Cursors.Default;
                ((Label)backpackStack[ii]).Text = "";
                backpackIcon[ii].Image = null;
                backpackIcon[ii].Update();
            }
            else
            {
                ((Label)backpackStack[ii]).Text = "x" + stackValue.ToString();
            }
        }

        void StatusPlayer_MouseLeave(object sender, EventArgs e)
        {
            if (levelUp)
            {
                if (initializeLevelUp)
                {
                    mainMessage.Text = upPoint.ToString() + "ポイントを割り振ってください。";
                }
                else
                {
                    mainMessage.Text = "レベルアップ！！" + upPoint.ToString() + "ポイントを割り振ってください。";
                }
            }
            else if (this.useOverShifting)
            {
                mainMessage.Text = "オーバーシフティング使用中、" + upPoint.ToString() + "ポイントを割り振ってください。";
            }
            else
            {
                //mainMessage.Text = "";
            }
        }

        void StatusPlayer_MouseEnter(object sender, EventArgs e)
        {
            if (((Label)sender).Name == "weapon")
            {
                ItemBackPack temp = new ItemBackPack(weapon.Text);
                if (temp.Description != "")
                {
                    mainMessage.Text = temp.Description;
                    return;
                }
            }

            // s 後編追加
            if (((Label)sender).Name == "subWeapon")
            {
                ItemBackPack temp = new ItemBackPack(subWeapon.Text);
                if (temp.Description != "")
                {
                    mainMessage.Text = temp.Description;
                    return;
                }
            }
            // e 後編追加

            if (((Label)sender).Name == "armor")
            {
                ItemBackPack temp = new ItemBackPack(armor.Text);
                if (temp.Description != "")
                {
                    mainMessage.Text = temp.Description;
                    return;
                }
            }

            if (((Label)sender).Name == "accessory")
            {
                ItemBackPack temp = new ItemBackPack(accessory.Text);
                if (temp.Description != "")
                {
                    mainMessage.Text = temp.Description;
                    return;
                }
            }

            // s 後編追加
            if (((Label)sender).Name == "accessory2")
            {
                ItemBackPack temp = new ItemBackPack(accessory2.Text);
                if (temp.Description != "")
                {
                    mainMessage.Text = temp.Description;
                    return;
                }
            }
            // e 後編追加

            for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
            {
                if (((Label)sender).Name == "backpack" + ii.ToString())
                {
                    ItemBackPack temp = new ItemBackPack(backpack[ii].Text);
                    if (temp.Description != "")
                    {
                        mainMessage.Text = temp.Description;
                    }
                    return;
                }
            }

        }

        private int mousePosX = 0;
        private int mousePosY = 0;
        void StatusPlayer_MouseDown(object sender, MouseEventArgs e)
        {
            this.mousePosX = this.Location.X + ((Label)sender).Location.X + e.X;
            this.mousePosY = this.Location.Y + ((Label)sender).Location.Y + e.Y;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (this.OnlySelectTrash)
            {
                if (this.cannotSelectTrash != String.Empty)
                {
                    mainMessage.Text = "アイン：いや【" + this.cannotSelectTrash + "】の入手を諦めるわけにはいかねえ。";
                    this.DialogResult = System.Windows.Forms.DialogResult.None;
                }
                else
                {
                    this.DialogResult = DialogResult.Cancel;
                }
            }
            else if (this.onlyUseItem)
            {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }

        private void TruthStatusPlayer_Load(object sender, EventArgs e)
        {
            this.basePhysicalLocation = this.PhysicalAttck.Location;

            this.buttonStrength.BackgroundImageLayout = ImageLayout.Stretch;
            this.buttonStrength.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "StrengthMark.bmp");
            this.buttonAgility.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "AgilityMark.bmp");
            this.buttonIntelligence.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "IntelligenceMark.bmp");
            this.buttonStamina.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "StaminaMark.bmp");
            this.buttonMind.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "MindMark.bmp");

            // this.gold.Text = mc.Gold.ToString() + "[G]"; // [警告]：ゴールドの所持は別クラスにするべきです。

            this.BackColor = currentStatusView;
            if (mc != null && mc.PlayerStatusColor == this.BackColor)
            {
                SettingCharacterData(mc);
                RefreshPartyMembersBattleStatus(mc);
            }
            else if (sc != null && sc.PlayerStatusColor == this.BackColor)
            {
                SettingCharacterData(sc);
                RefreshPartyMembersBattleStatus(sc);
            }
            else if (tc != null && sc.PlayerStatusColor == this.BackColor)
            {
                SettingCharacterData(tc);
                RefreshPartyMembersBattleStatus(tc);
            }
            if (mc != null) { button2.BackColor = mc.PlayerColor; }
            if (sc != null) { button3.BackColor = sc.PlayerColor; }
            if (tc != null) { button4.BackColor = tc.PlayerColor; }

            RefreshPartyMembersLife();

            if (!we.AvailableSecondCharacter && !we.AvailableThirdCharacter)
            {
                button2.Visible = false;
                labelFirstPlayerLife.Visible = false;
            }
            if (this.duelMode == true)
            {
                button2.Visible = false;
                labelFirstPlayerLife.Visible = false;
            }

            if (we.AvailableSecondCharacter && this.duelMode == false)
            {
                button3.Visible = true;
                labelSecondPlayerLife.Visible = true;
            }
            else
            {
                button3.Visible = false;
                labelSecondPlayerLife.Visible = false;
            }
            if (we.AvailableThirdCharacter && this.duelMode == false)
            {
                button4.Visible = true;
                labelThirdPlayerLife.Visible = true;
            }
            else
            {
                button4.Visible = false;
                labelThirdPlayerLife.Visible = false;
            }

            if (!levelUp)
            {
                //buttonStrength.Enabled = false;
                //buttonAgility.Enabled = false;
                //buttonIntelligence.Enabled = false;
                //buttonStamina.Enabled = false;
                //buttonMind.Enabled = false;

                mainMessage.Text = "";
            }
            else
            {
                button1.Visible = false;
                if (CumultiveLvUpValue >= 2)
                {
                    mainMessage.Text = CumultiveLvUpValue.ToString() + "レベルアップ！！" + upPoint.ToString() + "ポイントを割り振ってください。";
                }
                else
                {
                    mainMessage.Text = "レベルアップ！！" + upPoint.ToString() + "ポイントを割り振ってください。";
                }
            }

            if (this.OnlySelectTrash)
            {
                button1.Text = "諦める";
                mainMessage.Text = "アイン：バックパックがいっぱいみたいだ。何か捨てないとな・・・";
                FirstViewToSecondView = true;
                grpBattleStatus.Width = 0;
                grpEquipment.Width = 0;
                grpParameter.Width = 0;
                grpBackPack.Width = BACKPACK_WIDTH;
                grpBackPack.Location = new Point(BACKPACK_BASE_POSITION, grpBackPack.Location.Y);
            }

            if (this.onlyUseItem)
            {
                FirstViewToSecondView = true;
                grpBattleStatus.Width = 0;
                grpEquipment.Width = 0;
                grpParameter.Width = 0;
                grpBackPack.Width = BACKPACK_WIDTH;
                grpBackPack.Location = new Point(BACKPACK_BASE_POSITION, grpBackPack.Location.Y);
            }
        }


        private void CheckUpPoint()
        {
            upPoint--;
            if (upPoint <= 0)
            {
                mainMessage.Text = "ポイント割り振り完了！";
                button1.Text = "完了";
                button1.Visible = true;
                buttonStrength.Enabled = false;
                buttonAgility.Enabled = false;
                buttonIntelligence.Enabled = false;
                buttonStamina.Enabled = false;
                buttonMind.Enabled = false;
                this.useOverShifting = false;
            }
            else
            {
                mainMessage.Text = "あと" + upPoint.ToString() + "ポイントを割り振ってください。";
            }
        }



        private void SettingCharacterData(MainCharacter chara)
        {
            this.playerName.Text = chara.FullName;
            this.level.Text = chara.Level.ToString();
            if (chara.Level < Database.CHARACTER_MAX_LEVEL5)
            {
                this.experience.Text = chara.Exp.ToString() + " / " + chara.NextLevelBorder.ToString();
            }
            else
            {
                this.experience.Text = "-----" + " / " + "-----";
            }

            this.strength.Text = chara.Strength.ToString();
            this.addStrength.Text = " + " + chara.BuffStrength_Accessory.ToString();
            if (chara.BuffStrength_Food == 0) this.addStrength2.Text = "";
            else this.addStrength2.Text = " + " + chara.BuffStrength_Food.ToString();

            this.agility.Text = chara.Agility.ToString();
            this.addAgility.Text = " + " + chara.BuffAgility_Accessory.ToString();
            if (chara.BuffAgility_Food == 0) this.addAgility2.Text = "";
            else this.addAgility2.Text = " + " + chara.BuffAgility_Food.ToString();

            this.intelligence.Text = chara.Intelligence.ToString();
            this.addIntelligence.Text = " + " + chara.BuffIntelligence_Accessory.ToString();
            if (chara.BuffIntelligence_Food == 0) this.addIntelligence2.Text = "";
            else this.addIntelligence2.Text = " + " + chara.BuffIntelligence_Food.ToString();

            this.stamina.Text = chara.Stamina.ToString();
            this.addStamina.Text = " + " + chara.BuffStamina_Accessory.ToString();
            if (chara.BuffStamina_Food == 0) this.addStamina2.Text = "";
            else this.addStamina2.Text = " + " + chara.BuffStamina_Food.ToString();

            this.mindLabel.Text = chara.Mind.ToString();
            this.addMind.Text = " + " + chara.BuffMind_Accessory.ToString();
            if (chara.BuffMind_Food == 0) this.addMind2.Text = "";
            else this.addMind2.Text = " + " + chara.BuffMind_Food.ToString();

            // over shifting
            if (this.useOverShifting)
            {
                plus1.Visible = true;
                plus10.Visible = true;
                plus100.Visible = true;
                plus1000.Visible = true;
                btnUpReset.Visible = true;
                lblRemain.Visible = true;
                lblRemain.Text = "残り　" + this.upPoint.ToString();
            }
            else
            {
                plus1.Visible = false;
                plus10.Visible = false;
                plus100.Visible = false;
                plus1000.Visible = false;
                lblRemain.Visible = false;
                btnUpReset.Visible = false;
            }
            //

            RefreshPartyMembersLife();
            this.life.Text = chara.CurrentLife.ToString() + " / " + chara.MaxLife.ToString();

            if (chara.AvailableSkill)
            {
                label24.Visible = true;
                skill.Visible = true;
                if (chara.CurrentSkillPoint > chara.MaxSkillPoint)
                {
                    chara.CurrentSkillPoint = chara.MaxSkillPoint;
                }
                skill.Text = chara.CurrentSkillPoint.ToString() + " / " + chara.MaxSkillPoint.ToString();
            }
            else
            {
                label24.Visible = false;
                skill.Visible = false;
            }

            if (chara.AvailableMana)
            {
                label25.Visible = true;
                mana.Visible = true;
                mana.Text = chara.CurrentMana.ToString() + " / " + chara.MaxMana.ToString();
            }
            else
            {
                label25.Visible = false;
                mana.Visible = false;
            }

            this.weapon.Text = "";
            this.subWeapon.Text = "";
            this.armor.Text = "";
            this.accessory.Text = "";
            this.accessory2.Text = "";
            if (chara.MainWeapon != null)
            {
                if (chara.MainWeapon.Name == "")
                {
                    this.weapon.AutoSize = false;
                    this.weapon.BackColor = Color.Transparent;
                }
                else
                {
                    this.weapon.AutoSize = true;
                    this.weapon.Text = chara.MainWeapon.Name;
                    UpdateLabelColorForRare(ref this.weapon, chara.MainWeapon.Rare);
                }
            }
            else
            {
                this.weapon.AutoSize = false;
                this.weapon.BackColor = Color.Transparent;
            }

            if (chara.SubWeapon != null)
            {
                if (chara.SubWeapon.Name == "")
                {
                    this.subWeapon.AutoSize = false;
                    this.subWeapon.BackColor = Color.Transparent;
                }
                else
                {
                    this.subWeapon.AutoSize = true;
                    this.subWeapon.Text = chara.SubWeapon.Name;
                    UpdateLabelColorForRare(ref this.subWeapon, chara.SubWeapon.Rare);
                }
            }
            else
            {
                this.subWeapon.AutoSize = false;
                this.subWeapon.BackColor = Color.Transparent;
            }

            if (chara.MainArmor != null)
            {
                if (chara.MainArmor.Name == "")
                {
                    this.armor.AutoSize = false;
                    this.armor.BackColor = Color.Transparent;
                }
                else
                {
                    this.armor.AutoSize = true;
                    this.armor.Text = chara.MainArmor.Name;
                    UpdateLabelColorForRare(ref this.armor, chara.MainArmor.Rare);
                }
            }
            else
            {
                this.armor.AutoSize = false;
                this.armor.BackColor = Color.Transparent;
            }

            if (chara.Accessory != null)
            {
                if (chara.Accessory.Name == "")
                {
                    this.accessory.AutoSize = false;
                    this.accessory.BackColor = Color.Transparent;
                }
                else
                {
                    this.accessory.AutoSize = true;
                    this.accessory.Text = chara.Accessory.Name;
                    UpdateLabelColorForRare(ref this.accessory, chara.Accessory.Rare);
                }
            }
            else
            {
                this.accessory.AutoSize = false;
                this.accessory.BackColor = Color.Transparent;
            }

            if (chara.Accessory2 != null)
            {
                if (chara.Accessory2.Name == "")
                {
                    this.accessory2.AutoSize = false;
                    this.accessory2.BackColor = Color.Transparent;
                }
                else
                {
                    this.accessory2.AutoSize = true;
                    this.accessory2.Text = chara.Accessory2.Name;
                    UpdateLabelColorForRare(ref this.accessory2, chara.Accessory2.Rare);
                }
            }
            else
            {
                this.accessory2.AutoSize = false;
                this.accessory2.BackColor = Color.Transparent;
            }
            UpdateBackPackLabel(chara);
            UpdateSpellSkillLabel(chara);
            UpdateResistStatus(chara);
            //this.btnFreshHeal.Visible = chara.FreshHeal;
            //this.btnLifeTap.Visible = chara.LifeTap;
            //this.btnResurrection.Visible = chara.Resurrection;
            //this.btnCelestialNova.Visible = chara.CelestialNova;
        }

        private void UpdateSpellSkillLabel(MainCharacter target)
        {
            if (target.FreshHeal == false)
            {
                SpellSkill[0].Text = "";
                SpellSkill[0].Cursor = System.Windows.Forms.Cursors.Default;
            }
            else
            {
                SpellSkill[0].Text = Database.FRESH_HEAL_JP;
                SpellSkill[0].Cursor = System.Windows.Forms.Cursors.Hand;
            }

            if (target.LifeTap == false)
            {
                SpellSkill[1].Text = "";
                SpellSkill[1].Cursor = System.Windows.Forms.Cursors.Default;
            }
            else
            {
                SpellSkill[1].Text = Database.LIFE_TAP_JP;
                SpellSkill[1].Cursor = System.Windows.Forms.Cursors.Hand;
            }


            if (target.Resurrection == false)
            {
                SpellSkill[2].Text = "";
                SpellSkill[2].Cursor = System.Windows.Forms.Cursors.Default;
            }
            else
            {
                SpellSkill[2].Text = Database.RESURRECTION_JP;
                SpellSkill[2].Cursor = System.Windows.Forms.Cursors.Hand;
            }

            if (target.SacredHeal == false)
            {
                SpellSkill[3].Text = "";
                SpellSkill[3].Cursor = System.Windows.Forms.Cursors.Default;
            }
            else
            {
                SpellSkill[3].Text = Database.SACRED_HEAL_JP;
                SpellSkill[3].Cursor = System.Windows.Forms.Cursors.Hand;
            }
        }

        private void UpdateBackPackLabel(MainCharacter target)
        {
            ItemBackPack[] backpackData = target.GetBackPackInfo();
            for (int ii = 0; ii < backpackData.Length; ii++)
            {
                if (backpackData[ii] == null)
                {
                    backpack[ii].Text = "";
                    backpack[ii].Cursor = System.Windows.Forms.Cursors.Default;
                    backpackStack[ii].Text = "";
                    backpackIcon[ii].Image = null;
                    backpackIcon[ii].Update();
                }
                else
                {
                    backpack[ii].Text = backpackData[ii].Name;
                    backpack[ii].Cursor = System.Windows.Forms.Cursors.Hand;
                    UpdateLabelColorForRare(ref backpack[ii], backpackData[ii].Rare);
                    backpackStack[ii].Text = "x" + backpackData[ii].StackValue.ToString();
                    if ((backpackData[ii].Type == ItemBackPack.ItemType.Weapon_Heavy) ||
                        (backpackData[ii].Type == ItemBackPack.ItemType.Weapon_Middle))
                    {
                        backpackIcon[ii].Image = Image.FromFile(Database.BaseResourceFolder + "Weapon.bmp");
                    }
                    else if (backpackData[ii].Type == ItemBackPack.ItemType.Weapon_TwoHand)
                    {
                        backpackIcon[ii].Image = Image.FromFile(Database.BaseResourceFolder + "TwoHand.bmp");
                    }
                    else if (backpackData[ii].Type == ItemBackPack.ItemType.Weapon_Light)
                    {
                        backpackIcon[ii].Image = Image.FromFile(Database.BaseResourceFolder + "Knuckle.bmp");
                    }
                    else if (backpackData[ii].Type == ItemBackPack.ItemType.Weapon_Rod)
                    {
                        backpackIcon[ii].Image = Image.FromFile(Database.BaseResourceFolder + "Rod.bmp");
                    }
                    else if (backpackData[ii].Type == ItemBackPack.ItemType.Shield)
                    {
                        backpackIcon[ii].Image = Image.FromFile(Database.BaseResourceFolder + "Shield.bmp");
                    }
                    else if ((backpackData[ii].Type == ItemBackPack.ItemType.Armor_Heavy) ||
                             (backpackData[ii].Type == ItemBackPack.ItemType.Armor_Middle))
                    {
                        backpackIcon[ii].Image = Image.FromFile(Database.BaseResourceFolder + "Armor.bmp");
                    }
                    else if ((backpackData[ii].Type == ItemBackPack.ItemType.Armor_Light))
                    {
                        backpackIcon[ii].Image = Image.FromFile(Database.BaseResourceFolder + "LightArmor.bmp");
                    }
                    else if (backpackData[ii].Type == ItemBackPack.ItemType.Accessory)
                    {
                        backpackIcon[ii].Image = Image.FromFile(Database.BaseResourceFolder + "Accessory.bmp");
                    }
                    else if ((backpackData[ii].Type == ItemBackPack.ItemType.Material_Equip) ||
                             (backpackData[ii].Type == ItemBackPack.ItemType.Material_Food) ||
                             (backpackData[ii].Type == ItemBackPack.ItemType.Material_Potion))
                    {
                        backpackIcon[ii].Image = Image.FromFile(Database.BaseResourceFolder + "Material1.bmp");
                    }
                    else if (backpackData[ii].Type == ItemBackPack.ItemType.Use_Potion)
                    {
                        backpackIcon[ii].Image = Image.FromFile(Database.BaseResourceFolder + "Potion.bmp");
                    }
                    else if (backpackData[ii].Type == ItemBackPack.ItemType.Use_Any)
                    {
                        backpackIcon[ii].Image = Image.FromFile(Database.BaseResourceFolder + "UseItem.bmp");
                    }
                    else
                    {
                        backpackIcon[ii].Image = Image.FromFile(Database.BaseResourceFolder + "Useless.bmp");
                    }
                    backpackIcon[ii].Update();
                }
            }
        }

        private void UpdateResistStatus(MainCharacter player)
        {
            // 【後編必須】％レジスト増強も実装してください。
            ResistLabel[0].Text = Database.STRING_LIGHT;
            ResistLabelValue[0].Text = "+" + player.TotalResistLight.ToString() + " (0%)";
            ResistLabel[1].Text = Database.STRING_SHADOW;
            ResistLabelValue[1].Text = "+" + player.TotalResistShadow.ToString() + " (0%)";
            ResistLabel[2].Text = Database.STRING_FIRE;
            ResistLabelValue[2].Text = "+" + player.TotalResistFire.ToString() + " (0%)";
            ResistLabel[3].Text = Database.STRING_ICE;
            ResistLabelValue[3].Text = "+" + player.TotalResistIce.ToString() + " (0%)";
            ResistLabel[4].Text = Database.STRING_FORCE;
            ResistLabelValue[4].Text = "+" + player.TotalResistForce.ToString() + " (0%)";
            ResistLabel[5].Text = Database.STRING_WILL;
            ResistLabelValue[5].Text = "+" + player.TotalResistWill.ToString() + " (0%)";

            ResistAbnormalStatus[0].Text = Database.STRING_STUNNING;
            if (player.CheckResistStun) ResistAbnormalStatusValue[0].Text = "○";
            else ResistAbnormalStatusValue[0].Text = "";
            ResistAbnormalStatus[1].Text = Database.STRING_SILENCE;
            if (player.CheckResistSilence) ResistAbnormalStatusValue[1].Text = "○";
            else ResistAbnormalStatusValue[1].Text = "";
            ResistAbnormalStatus[2].Text = Database.STRING_POISON;
            if (player.CheckResistPoison) ResistAbnormalStatusValue[2].Text = "○";
            else ResistAbnormalStatusValue[2].Text = "";
            ResistAbnormalStatus[3].Text = Database.STRING_TEMPTATION;
            if (player.CheckResistTemptation) ResistAbnormalStatusValue[3].Text = "○";
            else ResistAbnormalStatusValue[3].Text = "";
            ResistAbnormalStatus[4].Text = Database.STRING_FROZEN;
            if (player.CheckResistFrozen) ResistAbnormalStatusValue[4].Text = "○";
            else ResistAbnormalStatusValue[4].Text = "";
            ResistAbnormalStatus[5].Text = Database.STRING_PARALYZE;
            if (player.CheckResistParalyze) ResistAbnormalStatusValue[5].Text = "○";
            else ResistAbnormalStatusValue[5].Text = "";
            ResistAbnormalStatus[6].Text = Database.STRING_SLOW;
            if (player.CheckResistSlow) ResistAbnormalStatusValue[6].Text = "○";
            else ResistAbnormalStatusValue[6].Text = "";
            ResistAbnormalStatus[7].Text = Database.STRING_BLIND;
            if (player.CheckResistBlind) ResistAbnormalStatusValue[7].Text = "○";
            else ResistAbnormalStatusValue[7].Text = "";
            ResistAbnormalStatus[8].Text = Database.STRING_SLIP;
            if (player.CheckResistSlip) ResistAbnormalStatusValue[8].Text = "○";
            else ResistAbnormalStatusValue[8].Text = "";
            // [コメント] 復活不可は特殊なので、ステータスとして見せたくはない。
            //ResistAbnormalStatus[9].Text = Database.STRING_NORESURRECTION;
            //if (player.CheckResistNoResurrection) ResistAbnormalStatusValue[9].Text += "　○";
        }

        Point basePhysicalLocation;
        private void RefreshPartyMembersBattleStatus(MainCharacter player)
        {
            double temp1 = 0;
            double temp2 = 0;
            temp1 = PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Min, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false);
            temp2 = PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Max, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false);
            PhysicalAttck.Text = temp1.ToString("F2");
            PhysicalAttck.Text += " - " + temp2.ToString("F2");

            temp1 = PrimaryLogic.SubAttackValue(player, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, PlayerStance.None, false);
            temp2 = PrimaryLogic.SubAttackValue(player, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, PlayerStance.None, false);
            if (temp1 > 0)
            {
                PhysicalAttck.Location = new Point(this.basePhysicalLocation.X, this.basePhysicalLocation.Y - 10);
                PhysicalAttck.Text += "\r\n" + temp1.ToString("F2");
                PhysicalAttck.Text += " - " + temp2.ToString("F2");
            }
            else
            {
                PhysicalAttck.Location = new Point(this.basePhysicalLocation.X, this.basePhysicalLocation.Y);
            }

            temp1 = PrimaryLogic.PhysicalDefenseValue(player, PrimaryLogic.NeedType.Min, false);
            temp2 = PrimaryLogic.PhysicalDefenseValue(player, PrimaryLogic.NeedType.Max, false);
            PhysicalDefense.Text = temp1.ToString("F2");
            PhysicalDefense.Text += " - " + temp2.ToString("F2");

            temp1 = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Min, 1.0f, 0.0f, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false);
            temp2 = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Max, 1.0f, 0.0f, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false);
            MagicAttack.Text = temp1.ToString("F2");
            MagicAttack.Text += " - " + temp2.ToString("F2");

            temp1 = PrimaryLogic.MagicDefenseValue(player, PrimaryLogic.NeedType.Min, false);
            temp2 = PrimaryLogic.MagicDefenseValue(player, PrimaryLogic.NeedType.Max, false);
            MagicDefense.Text = temp1.ToString("F2");
            MagicDefense.Text += " - " + temp2.ToString("F2");

            temp1 = PrimaryLogic.BattleSpeedValue(player, false);
            BattleSpeed.Text = temp1.ToString("F2");

            temp1 = PrimaryLogic.BattleResponseValue(player, false);
            BattleResponse.Text = temp1.ToString("F2");

            temp1 = PrimaryLogic.PotentialValue(player, false);
            Potential.Text = temp1.ToString("F2");
        }

        private void RefreshPartyMembersLife()
        {
            if (we.AvailableFirstCharacter)
            {
                labelFirstPlayerLife.Text = mc.CurrentLife.ToString() + "/" + mc.MaxLife.ToString();
            }
            if (we.AvailableSecondCharacter && this.duelMode == false)
            {
                labelSecondPlayerLife.Text = sc.CurrentLife.ToString() + "/" + sc.MaxLife.ToString();
            }
            if (we.AvailableThirdCharacter && this.duelMode == false)
            {
                labelThirdPlayerLife.Text = tc.CurrentLife.ToString() + "/" + tc.MaxLife.ToString();
            }
        }

        const string ToRight = ">>";
        const string ToLeft = "<<";
        private void buttonViewChange_Click(object sender, EventArgs e)
        {
            MainCharacter targetPlayer = null;
            if (mc != null && mc.PlayerStatusColor == this.BackColor)
            {
                targetPlayer = mc;
            }
            else if (sc != null && sc.PlayerStatusColor == this.BackColor)
            {
                targetPlayer = sc;
            }
            else if (tc != null && tc.PlayerStatusColor == this.BackColor)
            {
                targetPlayer = tc;
            }

            if (this.onlySelectTrash)
            {
                mainMessage.Text = targetPlayer.GetCharacterSentence(2021);
                return;
            }
            if (this.onlyUseItem)
            {
                mainMessage.Text = targetPlayer.GetCharacterSentence(2031);
                return;
            }

            if (buttonViewChange.Text == ToRight)
            {
                if (!FirstViewToSecondView)
                {
                    FirstViewToSecondView = true;
                }
                else if (!SecondViewToThirdView)
                {
                    SecondViewToThirdView = true;
                }
                else
                {
                    ThirdViewToFourthView = true;
                }
            }
            else
            {
                if (ThirdViewToFourthView)
                {
                    ThirdViewToFourthView = false;
                }
                else if (SecondViewToThirdView)
                {
                    SecondViewToThirdView = false;
                }
                else
                {
                    FirstViewToSecondView = false;
                }
            }
            timerGroupMoveAnimation.Start();
        }


        //const int BACKPACK_WIDTH = 616; // 12
        //const int SPELLSKILL_WIDTH = 616;
        //const int BACKPACK_BASE_POSITION = 12;
        //const int SPELLSKILL_BASE_POSITION = 12;
        //const int PARAMETER_WIDTH = 170;
        //const int EQUIPMENT_WIDTH = 167;
        //const int BATTLEPARAMETER_WIDTH = 268;
        const int BACKPACK_WIDTH = 1000; // 12
        const int SPELLSKILL_WIDTH = 1000;
        const int BACKPACK_BASE_POSITION = 12;
        const int SPELLSKILL_BASE_POSITION = 12;
        const int PARAMETER_WIDTH = 250;
        const int EQUIPMENT_WIDTH = 317;
        const int BATTLEPARAMETER_WIDTH = 421;

        const int ANIMATION_WIDTH = 3;
        bool FirstViewToSecondView = false;
        bool SecondViewToThirdView = false;
        bool ThirdViewToFourthView = false;

        private void timerGroupMoveAnimation_Tick(object sender, EventArgs e)
        {
            timerGroupMoveAnimation.Stop();

            if (FirstViewToSecondView && !SecondViewToThirdView && !ThirdViewToFourthView)
            {
                if (buttonViewChange.Text == ToRight)
                {
                    // パラメタ・装備・戦闘値　⇒　バックパック
                    if ((grpBattleStatus.Width > ANIMATION_WIDTH) || (grpEquipment.Width > ANIMATION_WIDTH) || (grpParameter.Width > ANIMATION_WIDTH))
                    {
                        grpBattleStatus.Width -= grpBattleStatus.Width / ANIMATION_WIDTH;
                        grpEquipment.Width -= grpEquipment.Width / ANIMATION_WIDTH;
                        grpParameter.Width -= grpParameter.Width / ANIMATION_WIDTH;

                        int moveValue = (BACKPACK_WIDTH - grpBackPack.Width) / ANIMATION_WIDTH;
                        grpBackPack.Width += (BACKPACK_WIDTH - grpBackPack.Width) / ANIMATION_WIDTH;
                        grpBackPack.Location = new Point(grpBackPack.Location.X - moveValue, grpBackPack.Location.Y);
                        timerGroupMoveAnimation.Start();
                    }
                    else if ((grpBattleStatus.Width > 0) || (grpEquipment.Width > 0) || (grpParameter.Width > 0))
                    {
                        grpBattleStatus.Width -= 1;
                        grpEquipment.Width -= 1;
                        grpParameter.Width -= 1;
                        grpBackPack.Width += 1;
                        grpBackPack.Location = new Point(grpBackPack.Location.X - 1, grpBackPack.Location.Y);
                        timerGroupMoveAnimation.Start();
                    }
                    else
                    {
                        grpBattleStatus.Width = 0;
                        grpEquipment.Width = 0;
                        grpParameter.Width = 0;
                        grpBackPack.Width = BACKPACK_WIDTH;
                        grpBackPack.Location = new Point(BACKPACK_BASE_POSITION, grpBackPack.Location.Y);
                        //buttonViewChange.Text = ToLeft;
                    }
                }
                else
                {
                    // 魔法・スキル　⇒　バックパック
                    grpResistStatus.Width = 0;
                    grpResistStatus.Location = new Point(SPELLSKILL_BASE_POSITION + SPELLSKILL_WIDTH, grpResistStatus.Location.Y);

                    if (grpSpellSkill.Width > ANIMATION_WIDTH)
                    {
                        int moveValue = grpSpellSkill.Width / ANIMATION_WIDTH;
                        grpSpellSkill.Width -= grpSpellSkill.Width / ANIMATION_WIDTH;
                        grpSpellSkill.Location = new Point(grpSpellSkill.Location.X + moveValue, grpSpellSkill.Location.Y);

                        timerGroupMoveAnimation.Start();
                    }
                    else if (grpSpellSkill.Width > 0)
                    {
                        grpSpellSkill.Width -= 1;
                        timerGroupMoveAnimation.Start();
                    }
                    else
                    {
                        grpSpellSkill.Width = 0;
                        //buttonViewChange.Text = ToRight;
                    }
                }
            }
            else if (FirstViewToSecondView && SecondViewToThirdView && !ThirdViewToFourthView)
            {
                if (buttonViewChange.Text == ToRight)
                {
                    // バックパック　⇒　魔法・スキル
                    grpBackPack.Width = BACKPACK_WIDTH;
                    grpBackPack.Location = new Point(BACKPACK_BASE_POSITION, grpBackPack.Location.Y);

                    if ((this.grpSpellSkill.Width < SPELLSKILL_WIDTH - ANIMATION_WIDTH))
                    {
                        int moveValue = (SPELLSKILL_WIDTH - grpSpellSkill.Width) / ANIMATION_WIDTH;
                        grpSpellSkill.Width += (SPELLSKILL_WIDTH - grpSpellSkill.Width) / ANIMATION_WIDTH;
                        grpSpellSkill.Location = new Point(grpSpellSkill.Location.X - moveValue, grpSpellSkill.Location.Y);
                        timerGroupMoveAnimation.Start();
                    }
                    else if ((SPELLSKILL_WIDTH - ANIMATION_WIDTH) < this.grpSpellSkill.Width && this.grpSpellSkill.Width < (SPELLSKILL_WIDTH))
                    {
                        grpBackPack.Width -= 1;
                        grpSpellSkill.Width += 1;
                        grpSpellSkill.Location = new Point(grpSpellSkill.Location.X - 1, grpSpellSkill.Location.Y);
                        timerGroupMoveAnimation.Start();
                    }
                    else
                    {
                        grpBattleStatus.Width = 0;
                        grpEquipment.Width = 0;
                        grpParameter.Width = 0;

                        grpSpellSkill.Width = BACKPACK_WIDTH;
                        grpSpellSkill.Location = new Point(SPELLSKILL_BASE_POSITION, grpSpellSkill.Location.Y);
                        //buttonViewChange.Text = ToLeft;
                    }
                }
                else
                {
                    // レジスト・耐性　⇒　魔法・スキル
                    if (grpResistStatus.Width > ANIMATION_WIDTH)
                    {
                        int moveValue = grpResistStatus.Width / ANIMATION_WIDTH;
                        grpResistStatus.Width -= grpResistStatus.Width / ANIMATION_WIDTH;
                        grpResistStatus.Location = new Point(grpResistStatus.Location.X + moveValue, grpResistStatus.Location.Y);

                        timerGroupMoveAnimation.Start();
                    }
                    else if (grpResistStatus.Width > 0)
                    {
                        grpResistStatus.Width -= 1;
                        timerGroupMoveAnimation.Start();
                    }
                    else
                    {
                        grpResistStatus.Width = 0;
                        //buttonViewChange.Text = ToRight;
                    }
                }
            }
            else if (FirstViewToSecondView && SecondViewToThirdView && ThirdViewToFourthView)
            {
                // 魔法・スキル  ⇒  レジスト・耐性
                grpSpellSkill.Width = BACKPACK_WIDTH;
                grpSpellSkill.Location = new Point(BACKPACK_BASE_POSITION, grpSpellSkill.Location.Y);

                if ((this.grpResistStatus.Width < SPELLSKILL_WIDTH - ANIMATION_WIDTH))
                {
                    int moveValue = (SPELLSKILL_WIDTH - grpResistStatus.Width) / ANIMATION_WIDTH;
                    grpResistStatus.Width += (SPELLSKILL_WIDTH - grpResistStatus.Width) / ANIMATION_WIDTH;
                    grpResistStatus.Location = new Point(grpResistStatus.Location.X - moveValue, grpResistStatus.Location.Y);
                    timerGroupMoveAnimation.Start();
                }
                else if ((SPELLSKILL_WIDTH - ANIMATION_WIDTH) < this.grpResistStatus.Width && this.grpResistStatus.Width < (SPELLSKILL_WIDTH))
                {
                    grpSpellSkill.Width -= 1;
                    grpResistStatus.Width += 1;
                    grpResistStatus.Location = new Point(grpResistStatus.Location.X - 1, grpResistStatus.Location.Y);
                    timerGroupMoveAnimation.Start();
                }
                else
                {
                    grpBattleStatus.Width = 0;
                    grpEquipment.Width = 0;
                    grpParameter.Width = 0;

                    grpResistStatus.Width = BACKPACK_WIDTH;
                    grpResistStatus.Location = new Point(SPELLSKILL_BASE_POSITION, grpResistStatus.Location.Y);
                    buttonViewChange.Text = ToLeft;
                }
            }
            else
            {
                // バックパック　⇒　パラメタ・装備・戦闘値
                grpSpellSkill.Width = 0;
                grpSpellSkill.Location = new Point(SPELLSKILL_BASE_POSITION + SPELLSKILL_WIDTH, grpSpellSkill.Location.Y);

                if (grpBackPack.Width > ANIMATION_WIDTH)
                {
                    int moveValue = grpBackPack.Width / ANIMATION_WIDTH;
                    grpBackPack.Width -= grpBackPack.Width / ANIMATION_WIDTH;
                    grpBackPack.Location = new Point(grpBackPack.Location.X + moveValue, grpBackPack.Location.Y);

                    grpParameter.Width += (PARAMETER_WIDTH - grpParameter.Width) / ANIMATION_WIDTH;
                    grpEquipment.Width += (EQUIPMENT_WIDTH - grpEquipment.Width) / ANIMATION_WIDTH;
                    grpBattleStatus.Width += (BATTLEPARAMETER_WIDTH - grpBattleStatus.Width) / ANIMATION_WIDTH;
                    timerGroupMoveAnimation.Start();
                }
                else if (grpBackPack.Width > 0)
                {
                    grpBattleStatus.Width += 1;
                    grpEquipment.Width += 1;
                    grpParameter.Width += 1;
                    grpBackPack.Width -= 1;
                    timerGroupMoveAnimation.Start();
                }
                else
                {
                    grpBattleStatus.Width = BATTLEPARAMETER_WIDTH;
                    grpEquipment.Width = EQUIPMENT_WIDTH;
                    grpParameter.Width = PARAMETER_WIDTH;
                    grpBackPack.Width = 0;
                    buttonViewChange.Text = ToRight;
                }
            }
        }

        bool IsShift = false;
        private void TruthStatusPlayer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey)
            {
                IsShift = true;
            }
        }

        private void TruthStatusPlayer_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey)
            {
                IsShift = false;
            }
        }


        private void btnSomeSpellSkill_Click(object sender, EventArgs e)
        {
            MainCharacter player = null;
            if (mc != null && mc.PlayerStatusColor == this.BackColor)
            {
                player = this.mc;
            }
            else if (sc != null && sc.PlayerStatusColor == this.BackColor)
            {
                player = this.sc;
            }
            else if (tc != null && tc.PlayerStatusColor == this.BackColor)
            {
                player = this.tc;
            }

            if (player.Dead)
            {
                mainMessage.Text = "【" + player.Name + "は死んでしまっているため、魔法詠唱ができない。】";
                return;
            }

            if (this.levelUp)
            {
                mainMessage.Text = player.GetCharacterSentence(2002);
                return;
            }

            if (this.useOverShifting)
            {
                mainMessage.Text = player.GetCharacterSentence(2023);
                return;
            }

            if (this.onlySelectTrash)
            {
                mainMessage.Text = player.GetCharacterSentence(2021);
                return;
            }

            if (((Label)sender).Text == Database.FRESH_HEAL_JP)
            {
                if (player.CurrentMana < Database.FRESH_HEAL_COST)
                {
                    mainMessage.Text = player.GetCharacterSentence(2008);
                    return;
                }
            }
            else if (((Label)sender).Text == Database.LIFE_TAP_JP)
            {
                if (player.CurrentMana < Database.LIFE_TAP_COST)
                {
                    mainMessage.Text = player.GetCharacterSentence(2008);
                    return;
                }
            }
            else if (((Label)sender).Text == Database.RESURRECTION_JP)
            {
                if (player.CurrentMana < Database.RESURRECTION_COST)
                {
                    mainMessage.Text = player.GetCharacterSentence(2008);
                    return;
                }
            }
            else if (((Label)sender).Text == Database.SACRED_HEAL_JP)
            {
                if (player.CurrentMana < Database.SACRED_HEAL_COST)
                {
                    mainMessage.Text = player.GetCharacterSentence(2008);
                    return;
                }
            }

            // 単体対象の場合
            if ((((Label)sender).Text == Database.FRESH_HEAL_JP) ||
                (((Label)sender).Text == Database.LIFE_TAP_JP) ||
                (((Label)sender).Text == Database.RESURRECTION_JP))
            {
                MainCharacter target = null;
                if (!we.AvailableSecondCharacter && !we.AvailableThirdCharacter)
                {
                    target = this.mc;
                }
                else if (we.AvailableSecondCharacter || we.AvailableThirdCharacter)
                {
                    using (SelectDungeon sa = new SelectDungeon())
                    {
                        sa.StartPosition = FormStartPosition.Manual;
                        if ((this.Location.X + this.Size.Width - this.mousePosX) <= sa.Width) this.mousePosX = this.Location.X + this.Size.Width - sa.Width;
                        if ((this.Location.Y + this.Size.Height - this.mousePosY) <= sa.Height) this.mousePosY = this.Location.Y + this.Size.Height - sa.Height;
                        sa.Location = new Point(this.mousePosX, this.mousePosY + this.grpSpellSkill.Location.Y);
                        if (we.AvailableSecondCharacter && we.AvailableThirdCharacter)
                        {
                            sa.MaxSelectable = 3;
                            sa.FirstName = mc.Name;
                            sa.SecondName = sc.Name;
                            sa.ThirdName = tc.Name;
                        }
                        else if (we.AvailableSecondCharacter && !we.AvailableThirdCharacter)
                        {
                            sa.MaxSelectable = 2;
                            sa.FirstName = mc.Name;
                            sa.SecondName = sc.Name;
                        }
                        //else if (!we.AvailableSecondCharacter && we.AvailableThirdCharacter)
                        //{
                        //    sa.MaxSelectable = 2;
                        //    sa.FirstName = mc.Name;
                        //    sa.SecondName = tc.Name;
                        //}
                        sa.EnablePopUpInfo = true;
                        sa.MC = this.mc;
                        sa.SC = this.sc;
                        sa.TC = this.tc;
                        sa.ShowDialog();
                        if (sa.TargetDungeon == 1)
                        {
                            target = this.mc;
                        }
                        else if (sa.TargetDungeon == 2)
                        {
                            target = this.sc;
                        }
                        else if (sa.TargetDungeon == 3)
                        {
                            target = this.tc;
                        }
                        else
                        {
                            // ESCキーキャンセルは何もしません。
                            return;
                        }
                    }
                }

                if ((((Label)sender).Text == Database.FRESH_HEAL_JP) ||
                    (((Label)sender).Text == Database.LIFE_TAP_JP))
                {
                    if (target.Dead)
                    {
                        mainMessage.Text = "【" + target.Name + "は死んでしまっているため、効果がない。】";
                        return;
                    }

                    int lifeGain = 0;
                    if (((Label)sender).Text == Database.FRESH_HEAL_JP)
                    {
                        player.CurrentMana -= Database.FRESH_HEAL_COST;
                        lifeGain = (int)PrimaryLogic.FreshHealValue(player, false);
                    }
                    else if (((Label)sender).Text == Database.LIFE_TAP_JP)
                    {
                        player.CurrentMana -= Database.LIFE_TAP_COST;
                        lifeGain = (int)PrimaryLogic.LifeTapValue(player, false);
                    }

                    target.CurrentLife += lifeGain;
                    mainMessage.Text = String.Format(player.GetCharacterSentence(2001), lifeGain.ToString());
                }
                else
                {
                    if (target.Dead)
                    {
                        player.CurrentMana -= Database.RESURRECTION_COST;

                        target.Dead = false;
                        target.CurrentLife = (int)PrimaryLogic.ResurrectionValue(target);
                        mainMessage.Text = String.Format(target.GetCharacterSentence(2016));
                    }
                    else if (target == player)
                    {
                        mainMessage.Text = String.Format(player.GetCharacterSentence(2018));
                    }
                    else if (!target.Dead)
                    {
                        mainMessage.Text = String.Format(player.GetCharacterSentence(2017), target.Name);
                    }
                }
                this.life.Text = player.CurrentLife.ToString() + " / " + player.MaxLife.ToString();
                this.mana.Text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();
            }
            // 味方全体の場合
            else
            {
                int lifeGain = 0;
                if (((Label)sender).Text == Database.SACRED_HEAL_JP)
                {
                    player.CurrentMana -= Database.SACRED_HEAL_COST;
                    lifeGain = (int)PrimaryLogic.SacredHealValue(player, false);
                }

                List<MainCharacter> group = new List<MainCharacter>();
                if (mc != null && !mc.Dead) { group.Add(mc); }
                if (sc != null && !sc.Dead) { group.Add(sc); }
                if (tc != null && !tc.Dead) { group.Add(tc); }
                for (int ii = 0; ii < group.Count; ii++)
                {
                    group[ii].CurrentLife += lifeGain;
                    mainMessage.Text = String.Format(player.GetCharacterSentence(2035), lifeGain.ToString());
                }
            }

            this.life.Text = player.CurrentLife.ToString() + " / " + player.MaxLife.ToString();
            this.mana.Text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();
            RefreshPartyMembersLife();
        }

        private void btnResurrection_Click(object sender, EventArgs e)
        {
            MainCharacter player = null;
            if (mc != null && mc.PlayerStatusColor == this.BackColor)
            {
                player = this.mc;
            }
            else if (sc != null && sc.PlayerStatusColor == this.BackColor)
            {
                player = this.sc;
            }
            else if (tc != null && tc.PlayerStatusColor == this.BackColor)
            {
                player = this.tc;
            }

            if (player.Dead)
            {
                mainMessage.Text = "【" + player.Name + "は死んでしまっているため、魔法詠唱ができない。】";
                return;
            }

            if (this.levelUp)
            {
                mainMessage.Text = player.GetCharacterSentence(2002);
                return;
            }

            if (this.useOverShifting)
            {
                mainMessage.Text = player.GetCharacterSentence(2023);
                return;
            }

            if (this.onlySelectTrash)
            {
                mainMessage.Text = player.GetCharacterSentence(2021);
                return;
            }

            if (player.CurrentMana < Database.RESURRECTION_COST)
            {
                mainMessage.Text = player.GetCharacterSentence(2008);
                return;
            }

            MainCharacter target = null;
            if (!we.AvailableSecondCharacter && !we.AvailableThirdCharacter)
            {
                target = this.mc;
            }
            else if (we.AvailableSecondCharacter || we.AvailableThirdCharacter)
            {
                using (SelectDungeon sa = new SelectDungeon())
                {
                    sa.StartPosition = FormStartPosition.Manual;
                    if ((this.Location.X + this.Size.Width - this.mousePosX) <= sa.Width) this.mousePosX = this.Location.X + this.Size.Width - sa.Width;
                    if ((this.Location.Y + this.Size.Height - this.mousePosY) <= sa.Height) this.mousePosY = this.Location.Y + this.Size.Height - sa.Height;
                    sa.Location = new Point(this.mousePosX, this.mousePosY);
                    if (we.AvailableSecondCharacter && we.AvailableThirdCharacter)
                    {
                        sa.MaxSelectable = 3;
                        sa.FirstName = mc.Name;
                        sa.SecondName = sc.Name;
                        sa.ThirdName = tc.Name;
                    }
                    else if (we.AvailableSecondCharacter && !we.AvailableThirdCharacter)
                    {
                        sa.MaxSelectable = 2;
                        sa.FirstName = mc.Name;
                        sa.SecondName = sc.Name;
                    }
                    //else if (!we.AvailableSecondCharacter && we.AvailableThirdCharacter)
                    //{
                    //    sa.MaxSelectable = 2;
                    //    sa.FirstName = mc.Name;
                    //    sa.SecondName = tc.Name;
                    //}
                    sa.ShowDialog();
                    if (sa.TargetDungeon == 1)
                    {
                        target = this.mc;
                    }
                    else if (sa.TargetDungeon == 2)
                    {
                        target = this.sc;
                    }
                    else if (sa.TargetDungeon == 3)
                    {
                        target = this.tc;
                    }
                    else
                    {
                        // ESCキーキャンセルは何もしません。
                        return;
                    }
                }
            }

            if (target.Dead)
            {
                player.CurrentMana -= Database.RESURRECTION_COST;
                this.mana.Text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();

                target.Dead = false;
                target.CurrentLife = target.MaxLife / 2;
                mainMessage.Text = String.Format(target.GetCharacterSentence(2016));
            }
            else if (target == player)
            {
                mainMessage.Text = String.Format(player.GetCharacterSentence(2018));
            }
            else if (!target.Dead)
            {
                mainMessage.Text = String.Format(player.GetCharacterSentence(2017), target.Name);
            }
            RefreshPartyMembersLife();
        }

        private void btnCelestialNova_Click(object sender, EventArgs e)
        {
            MainCharacter player = null;
            if (mc != null && mc.PlayerStatusColor == this.BackColor)
            {
                player = this.mc;
            }
            else if (sc != null && sc.PlayerStatusColor == this.BackColor)
            {
                player = this.sc;
            }
            else if (tc != null && tc.PlayerStatusColor == this.BackColor)
            {
                player = this.tc;
            }

            if (player.Dead)
            {
                mainMessage.Text = "【" + player.Name + "は死んでしまっているため、魔法詠唱ができない。】";
                return;
            }

            if (this.levelUp)
            {
                mainMessage.Text = player.GetCharacterSentence(2002);
                return;
            }

            if (this.useOverShifting)
            {
                mainMessage.Text = player.GetCharacterSentence(2023);
                return;
            }

            if (this.onlySelectTrash)
            {
                mainMessage.Text = player.GetCharacterSentence(2021);
                return;
            }

            if (player.CurrentMana < Database.CELESTIAL_NOVA_COST)
            {
                mainMessage.Text = player.GetCharacterSentence(2008);
                return;
            }

            MainCharacter target = null;
            if (!we.AvailableSecondCharacter && !we.AvailableThirdCharacter)
            {
                target = this.mc;
            }
            else if (we.AvailableSecondCharacter || we.AvailableThirdCharacter)
            {
                using (SelectDungeon sa = new SelectDungeon())
                {
                    sa.StartPosition = FormStartPosition.Manual;
                    if ((this.Location.X + this.Size.Width - this.mousePosX) <= sa.Width) this.mousePosX = this.Location.X + this.Size.Width - sa.Width;
                    if ((this.Location.Y + this.Size.Height - this.mousePosY) <= sa.Height) this.mousePosY = this.Location.Y + this.Size.Height - sa.Height;
                    sa.Location = new Point(this.mousePosX, this.mousePosY);

                    if (we.AvailableSecondCharacter && we.AvailableThirdCharacter)
                    {
                        sa.MaxSelectable = 3;
                        sa.FirstName = mc.Name;
                        sa.SecondName = sc.Name;
                        sa.ThirdName = tc.Name;
                    }
                    else if (we.AvailableSecondCharacter && !we.AvailableThirdCharacter)
                    {
                        sa.MaxSelectable = 2;
                        sa.FirstName = mc.Name;
                        sa.SecondName = sc.Name;
                    }
                    //else if (!we.AvailableSecondCharacter && we.AvailableThirdCharacter)
                    //{
                    //    sa.MaxSelectable = 2;
                    //    sa.FirstName = mc.Name;
                    //    sa.SecondName = tc.Name;
                    //}
                    sa.EnablePopUpInfo = true;
                    sa.MC = this.mc;
                    sa.SC = this.sc;
                    sa.TC = this.tc;
                    sa.ShowDialog();
                    if (sa.TargetDungeon == 1)
                    {
                        target = this.mc;
                    }
                    else if (sa.TargetDungeon == 2)
                    {
                        target = this.sc;
                    }
                    else if (sa.TargetDungeon == 3)
                    {
                        target = this.tc;
                    }
                    else
                    {
                        // ESCキーキャンセルは何もしません。
                        return;
                    }
                }
            }

            if (target.Dead)
            {
                mainMessage.Text = "【" + target.Name + "は死んでしまっているため、効果がない。】";
                return;
            }

            player.CurrentMana -= Database.CELESTIAL_NOVA_COST;
            Random rd = new Random(DateTime.Now.Millisecond);
            int effectValue = 400 + player.Intelligence * 5 + rd.Next(player.Mind, player.Mind * 2);

            target.CurrentLife += effectValue;
            mainMessage.Text = String.Format(player.GetCharacterSentence(2001), effectValue.ToString());
            this.life.Text = player.CurrentLife.ToString() + " / " + player.MaxLife.ToString();
            this.mana.Text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();
            RefreshPartyMembersLife();
        }

        private void weapon_Click(object sender, EventArgs e)
        {
            ChangeEquipment(0);
        }
        private void subWeapon_Click(object sender, System.EventArgs e)
        {
            MainCharacter targetPlayer = null;
            if (mc != null && mc.PlayerStatusColor == this.BackColor)
            {
                targetPlayer = mc;
            }
            else if (sc != null && sc.PlayerStatusColor == this.BackColor)
            {
                targetPlayer = sc;
            }
            else if (tc != null && tc.PlayerStatusColor == this.BackColor)
            {
                targetPlayer = tc;
            }
            if (targetPlayer.MainWeapon != null)
            {
                if ((targetPlayer.MainWeapon.Type == ItemBackPack.ItemType.Weapon_Rod) ||
                    (targetPlayer.MainWeapon.Type == ItemBackPack.ItemType.Weapon_TwoHand))
                {
                    mainMessage.Text = targetPlayer.GetCharacterSentence(2025);
                    return;
                }
                if (targetPlayer.MainWeapon.Name == "")
                {
                    mainMessage.Text = targetPlayer.GetCharacterSentence(2026);
                    return;
                }
            }
            if (targetPlayer.MainWeapon == null)
            {
                mainMessage.Text = targetPlayer.GetCharacterSentence(2026);
                return;
            }
            ChangeEquipment(1);
        }
        private void armor_Click(object sender, EventArgs e)
        {
            ChangeEquipment(2);
        }
        private void accessory_Click(object sender, EventArgs e)
        {
            ChangeEquipment(3);
        }
        private void accessory2_Click(object sender, EventArgs e)
        {
            ChangeEquipment(4);
        }

        // equipType: 0:Weapon  1:SubWeapon  2:Armor  3:Accessory  4:Accessory2
        private void ChangeEquipment(int equipType)
        {
            MainCharacter targetPlayer = null;
            if (mc != null && mc.PlayerStatusColor == this.BackColor)
            {
                targetPlayer = mc;
            }
            else if (sc != null && sc.PlayerStatusColor == this.BackColor)
            {
                targetPlayer = sc;
            }
            else if (tc != null && tc.PlayerStatusColor == this.BackColor)
            {
                targetPlayer = tc;
            }

            TruthSelectEquipment tse = new TruthSelectEquipment();
            ItemBackPack[] temp = targetPlayer.GetBackPackInfo();
            int counter = 0;
            for (int ii = 0; ii < temp.Length; ii++)
            {
                if (temp[ii] == null)
                    continue;

                if (CheckEquipmentType(targetPlayer, temp[ii], equipType))
                {
                    tse.btn[counter].Text = temp[ii].Name;
                    counter++;
                }
            }
            tse.StartPosition = FormStartPosition.CenterParent;
            tse.Player = targetPlayer;
            tse.EquipType = equipType;
            tse.ShowDialog();
            if (tse.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                ItemBackPack exchangeItem = new ItemBackPack(tse.SelectValue);
                ItemBackPack tempItem = null;
                if (equipType == 0)
                {
                    tempItem = targetPlayer.MainWeapon;
                    targetPlayer.MainWeapon = exchangeItem;
                    if ((exchangeItem.Type == ItemBackPack.ItemType.Weapon_Rod) ||
                        (exchangeItem.Type == ItemBackPack.ItemType.Weapon_TwoHand))
                    {
                        if (targetPlayer.SubWeapon != null)
                        {
                            if (targetPlayer.SubWeapon.Name != "")
                            {
                                targetPlayer.AddBackPack(targetPlayer.SubWeapon);
                            }
                            targetPlayer.SubWeapon = null;
                        }
                    }
                }
                else if (equipType == 1)
                {
                    tempItem = targetPlayer.SubWeapon;
                    targetPlayer.SubWeapon = exchangeItem;
                    if (targetPlayer.MainWeapon != null)
                    {
                        if (targetPlayer.MainWeapon.Name != "")
                        {
                            if ((targetPlayer.MainWeapon.Type == ItemBackPack.ItemType.Weapon_Rod) ||
                                (targetPlayer.MainWeapon.Type == ItemBackPack.ItemType.Weapon_TwoHand))
                            {
                                targetPlayer.AddBackPack(targetPlayer.MainWeapon);
                                targetPlayer.MainWeapon = null;
                            }
                        }
                    }
                }
                else if (equipType == 2)
                {
                    tempItem = targetPlayer.MainArmor;
                    targetPlayer.MainArmor = exchangeItem;
                }
                else if (equipType == 3)
                {
                    tempItem = targetPlayer.Accessory;
                    targetPlayer.Accessory = exchangeItem;
                }
                else if (equipType == 4)
                {
                    tempItem = targetPlayer.Accessory2;
                    targetPlayer.Accessory2 = exchangeItem;
                }
                if (exchangeItem != null)
                {
                    if (exchangeItem.Name != "")
                    {
                        targetPlayer.DeleteBackPack(exchangeItem);
                    }
                }
                if (tempItem != null)
                {
                    if (tempItem.Name != "")
                    {
                        targetPlayer.AddBackPack(tempItem);
                    }
                }
                SettingCharacterData(targetPlayer);
                RefreshPartyMembersBattleStatus(targetPlayer);
            }
        }

        // equipType: 0:Weapon  1:SubWeapon  2:Armor  3:Accessory  4:Accessory2
        private bool CheckEquipmentType(MainCharacter player, ItemBackPack item, int equipType)
        {
            if (equipType == 0)
            {
                if ((player == mc) && (item.Type == ItemBackPack.ItemType.Weapon_Heavy) ||
                    (player == mc) && (item.Type == ItemBackPack.ItemType.Weapon_TwoHand))
                {
                    return true;
                }
                else if ((player == sc) && (item.Type == ItemBackPack.ItemType.Weapon_Light) ||
                         (player == sc) && (item.Type == ItemBackPack.ItemType.Weapon_Rod))
                {
                    return true;
                }
                else if ((player == tc) && (item.Type == ItemBackPack.ItemType.Weapon_TwoHand) ||
                         (player == tc) && (item.Type == ItemBackPack.ItemType.Weapon_Heavy) ||
                         (player == tc) && (item.Type == ItemBackPack.ItemType.Weapon_Middle) ||
                         (player == tc) && (item.Type == ItemBackPack.ItemType.Weapon_Light))
                {
                    return true;
                }
            }
            else if (equipType == 1)
            {
                if ((player == mc) && (item.Type == ItemBackPack.ItemType.Weapon_Heavy) ||
                    (player == mc) && (item.Type == ItemBackPack.ItemType.Shield))
                {
                    return true;
                }
                else if ((player == sc) && (item.Type == ItemBackPack.ItemType.Weapon_Light))
                {
                    return true;
                }
                else if ((player == tc) && (item.Type == ItemBackPack.ItemType.Weapon_Heavy) ||
                         (player == tc) && (item.Type == ItemBackPack.ItemType.Weapon_Middle) ||
                         (player == tc) && (item.Type == ItemBackPack.ItemType.Weapon_Light) ||
                         (player == tc) && (item.Type == ItemBackPack.ItemType.Shield))
                {
                    return true;
                }
            }
            else if (equipType == 2)
            {
                if ((player == mc) && (item.Type == ItemBackPack.ItemType.Armor_Heavy) ||
                    (player == mc) && (item.Type == ItemBackPack.ItemType.Armor_Middle))
                {
                    return true;
                }
                else if ((player == sc) && (item.Type == ItemBackPack.ItemType.Armor_Light))
                {
                    return true;
                }
                else if ((player == tc) && (item.Type == ItemBackPack.ItemType.Armor_Heavy) ||
                         (player == tc) && (item.Type == ItemBackPack.ItemType.Armor_Light) ||
                         (player == tc) && (item.Type == ItemBackPack.ItemType.Armor_Middle))
                {
                    return true;
                }
            }
            else if (equipType == 3)
            {
                if (item.Type == ItemBackPack.ItemType.Accessory)
                {
                    if ((player == mc) && (item.EquipablePerson == ItemBackPack.Equipable.All) ||
                        (player == mc) && (item.EquipablePerson == ItemBackPack.Equipable.Ein))
                    {
                        return true;
                    }
                    if ((player == sc) && (item.EquipablePerson == ItemBackPack.Equipable.All) ||
                        (player == sc) && (item.EquipablePerson == ItemBackPack.Equipable.Lana))
                    {
                        return true;
                    }
                    if ((player == tc) && (item.EquipablePerson == ItemBackPack.Equipable.All) ||
                        (player == tc) && (item.EquipablePerson == ItemBackPack.Equipable.Verze) ||
                        (player == tc) && (item.EquipablePerson == ItemBackPack.Equipable.Ol))
                    {
                        return true;
                    }
                }
            }
            else if (equipType == 4)
            {
                if (item.Type == ItemBackPack.ItemType.Accessory)
                {
                    if ((player == mc) && (item.EquipablePerson == ItemBackPack.Equipable.All) ||
                        (player == mc) && (item.EquipablePerson == ItemBackPack.Equipable.Ein))
                    {
                        return true;
                    }
                    if ((player == sc) && (item.EquipablePerson == ItemBackPack.Equipable.All) ||
                        (player == sc) && (item.EquipablePerson == ItemBackPack.Equipable.Lana))
                    {
                        return true;
                    }
                    if ((player == tc) && (item.EquipablePerson == ItemBackPack.Equipable.All) ||
                        (player == tc) && (item.EquipablePerson == ItemBackPack.Equipable.Verze) ||
                        (player == tc) && (item.EquipablePerson == ItemBackPack.Equipable.Ol))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.levelUp)
            {
                mainMessage.Text = mc.GetCharacterSentence(2002);
                return;
            }
            else if (this.useOverShifting)
            {
                mainMessage.Text = mc.GetCharacterSentence(2023);
                return;
            }
            else if (this.onlySelectTrash)
            {
                mainMessage.Text = mc.GetCharacterSentence(2021);
            }
            else
            {
                this.BackColor = mc.PlayerStatusColor;
                this.currentStatusView = mc.PlayerStatusColor;
                SettingCharacterData(mc);
                RefreshPartyMembersBattleStatus(mc);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.levelUp)
            {
                mainMessage.Text = sc.GetCharacterSentence(2002);
                return;
            }
            else if (this.useOverShifting)
            {
                mainMessage.Text = sc.GetCharacterSentence(2023);
                return;
            }
            else if (this.onlySelectTrash)
            {
                mainMessage.Text = sc.GetCharacterSentence(2021);
            }
            else
            {
                this.BackColor = sc.PlayerStatusColor;
                this.currentStatusView = sc.PlayerStatusColor;
                SettingCharacterData(sc);
                RefreshPartyMembersBattleStatus(sc);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.levelUp)
            {
                mainMessage.Text = tc.GetCharacterSentence(2002);
                return;
            }
            else if (this.useOverShifting)
            {
                mainMessage.Text = tc.GetCharacterSentence(2023);
                return;
            }
            else if (this.onlySelectTrash)
            {
                mainMessage.Text = tc.GetCharacterSentence(2021);
            }
            else
            {
                this.BackColor = tc.PlayerStatusColor;
                this.currentStatusView = tc.PlayerStatusColor;
                SettingCharacterData(tc);
                RefreshPartyMembersBattleStatus(tc);
            }
        }

        private void ChangeViewButton_Click(int viewNumber)
        {
            MainCharacter targetPlayer = null;
            if (mc != null && mc.PlayerStatusColor == this.BackColor)
            {
                targetPlayer = mc;
            }
            else if (sc != null && sc.PlayerStatusColor == this.BackColor)
            {
                targetPlayer = sc;
            }
            else if (tc != null && tc.PlayerStatusColor == this.BackColor)
            {
                targetPlayer = tc;
            }

            if (this.onlySelectTrash)
            {
                mainMessage.Text = targetPlayer.GetCharacterSentence(2021);
                return;
            }
            if (this.onlyUseItem)
            {
                mainMessage.Text = targetPlayer.GetCharacterSentence(2031);
                return;
            }

            if (viewNumber == 0)
            {
                FirstViewToSecondView = false;
                SecondViewToThirdView = false;
                ThirdViewToFourthView = false;
                grpBattleStatus.Width = BATTLEPARAMETER_WIDTH;
                grpEquipment.Width = EQUIPMENT_WIDTH;
                grpParameter.Width = PARAMETER_WIDTH;
                grpBackPack.Width = 0;
                grpSpellSkill.Width = 0;
                grpResistStatus.Width = 0;
                buttonViewChange.Text = ToRight;

            }
            else if (viewNumber == 1)
            {
                FirstViewToSecondView = true;
                SecondViewToThirdView = false;
                ThirdViewToFourthView = false;
                grpBattleStatus.Width = 0;
                grpEquipment.Width = 0;
                grpParameter.Width = 0;
                grpBackPack.Width = BACKPACK_WIDTH;
                grpBackPack.Location = new Point(BACKPACK_BASE_POSITION, grpBackPack.Location.Y);
                grpSpellSkill.Width = 0;
                grpResistStatus.Width = 0;
                //buttonViewChange.Text = ToLeft;
            }
            else if (viewNumber == 2)
            {
                FirstViewToSecondView = true;
                SecondViewToThirdView = true;
                ThirdViewToFourthView = false;
                grpBattleStatus.Width = 0;
                grpEquipment.Width = 0;
                grpParameter.Width = 0;
                grpBackPack.Width = 0;
                grpSpellSkill.Width = BACKPACK_WIDTH;
                grpSpellSkill.Location = new Point(SPELLSKILL_BASE_POSITION, grpSpellSkill.Location.Y);
                grpResistStatus.Width = 0;
                //buttonViewChange.Text = ToLeft;
            }
            else if (viewNumber == 3)
            {
                FirstViewToSecondView = true;
                SecondViewToThirdView = true;
                ThirdViewToFourthView = true;
                grpBattleStatus.Width = 0;
                grpEquipment.Width = 0;
                grpParameter.Width = 0;
                grpBackPack.Width = 0;
                grpSpellSkill.Width = 0;
                grpResistStatus.Width = BACKPACK_WIDTH;
                grpResistStatus.Location = new Point(SPELLSKILL_BASE_POSITION, grpResistStatus.Location.Y);
                buttonViewChange.Text = ToLeft;
            }           
        }

        private void StatusButton_Click(object sender, EventArgs e)
        {
            if (sender.Equals(StatusButton))
            {
                ChangeViewButton_Click(0);
            }
            else if (sender.Equals(itemButton))
            {
                ChangeViewButton_Click(1);
            }
            else if (sender.Equals(spellButton))
            {
                ChangeViewButton_Click(2);
            }
            else if (sender.Equals(resistButton))
            {
                ChangeViewButton_Click(3);
            }
        }

        // [警告] 以下、TruthSelectCharacterと重複する記述です。統一化を行ってください。
        private int addStrSC = 0;
        private int addAglSC = 0;
        private int addIntSC = 0;
        private int addStmSC = 0;
        private int addMndSC = 0;
        upType number = upType.Strength;

        enum upType
        {
            Strength,
            Agility,
            Intelligence,
            Stamina,
            Mind
        }

        private void grpParameter_Paint(object sender, PaintEventArgs e)
        {
            if (this.useOverShifting)
            {
                Graphics g = e.Graphics;
                int BluePenWidth = 4;
                int SkyBluePenWidth = 2;
                Pen pen1 = new Pen(Brushes.DarkOrange, BluePenWidth);
                Pen pen2 = new Pen(Brushes.Orange, SkyBluePenWidth);
                int basePosX = 0; // 味方側のＸライン
                int basePosY = 0; // 味方：１人目のYライン(
                int len = 58; // 四角枠の横長さ
                int len2 = 54; // 四角枠の縦長さ

                if (this.number == upType.Strength) { basePosX = buttonStrength.Location.X; basePosY = buttonStrength.Location.Y; }
                else if (this.number == upType.Agility) { basePosX = buttonAgility.Location.X; basePosY = buttonAgility.Location.Y; }
                else if (this.number == upType.Intelligence) { basePosX = buttonIntelligence.Location.X; basePosY = buttonIntelligence.Location.Y; }
                else if (this.number == upType.Stamina) { basePosX = buttonStamina.Location.X; basePosY = buttonStamina.Location.Y; }
                else if (this.number == upType.Mind) { basePosX = buttonMind.Location.X; basePosY = buttonMind.Location.Y; }

                g.DrawRectangle(pen1, new Rectangle(basePosX - 4, basePosY - 4, len, len));
                g.DrawRectangle(pen2, new Rectangle(basePosX - 2, basePosY - 2, len2, len2));
            }
            else
            {
                base.OnPaint(e);
            }
        }

        private void buttonStrength_Click(object sender, EventArgs e)
        {
            // 通常レベルアップ＋１のロジック
            if (this.levelUp)
            {
                if (mc != null && mc.PlayerStatusColor == this.BackColor)
                {
                    mc.Strength++;
                    strength.Text = mc.Strength.ToString();
                    RefreshPartyMembersBattleStatus(mc);
                }
                else if (sc != null && sc.PlayerStatusColor == this.BackColor)
                {
                    sc.Strength++;
                    strength.Text = sc.Strength.ToString();
                    RefreshPartyMembersBattleStatus(sc);
                }
                else if (tc != null && tc.PlayerStatusColor == this.BackColor)
                {
                    tc.Strength++;
                    strength.Text = tc.Strength.ToString();
                    RefreshPartyMembersBattleStatus(tc);
                }
                CheckUpPoint();
            }
            // オーバーシフティング
            else if (this.useOverShifting)
            {
                this.number = upType.Strength;
                grpParameter.Invalidate();
            }            
        }


        private void buttonAgility_Click(object sender, EventArgs e)
        {
            // 通常レベルアップ＋１のロジック
            if (this.levelUp)
            {
                if (mc != null && mc.PlayerStatusColor == this.BackColor)
                {
                    mc.Agility++;
                    agility.Text = mc.Agility.ToString();
                    RefreshPartyMembersBattleStatus(mc);
                }
                else if (sc != null && sc.PlayerStatusColor == this.BackColor)
                {
                    sc.Agility++;
                    agility.Text = sc.Agility.ToString();
                    RefreshPartyMembersBattleStatus(sc);
                }
                else if (tc != null && tc.PlayerStatusColor == this.BackColor)
                {
                    tc.Agility++;
                    agility.Text = tc.Agility.ToString();
                    RefreshPartyMembersBattleStatus(tc);
                }
                CheckUpPoint();
            }
            // オーバーシフティング
            else if (this.useOverShifting)
            {
                this.number = upType.Agility;
                grpParameter.Invalidate();
            }
        }

        private void buttonIntelligence_Click(object sender, EventArgs e)
        {
            // 通常レベルアップ＋１のロジック
            if (this.levelUp)
            {
                if (mc != null && mc.PlayerStatusColor == this.BackColor)
                {
                    mc.Intelligence++;
                    intelligence.Text = mc.Intelligence.ToString();
                    if (mc.AvailableMana)
                    {
                        this.mana.Text = mc.CurrentMana.ToString() + " / " + mc.MaxMana.ToString();
                    }
                    RefreshPartyMembersBattleStatus(mc);
                }
                else if (sc != null && sc.PlayerStatusColor == this.BackColor)
                {
                    sc.Intelligence++;
                    intelligence.Text = sc.Intelligence.ToString();
                    if (sc.AvailableMana)
                    {
                        this.mana.Text = sc.CurrentMana.ToString() + " / " + sc.MaxMana.ToString();
                    }
                    RefreshPartyMembersBattleStatus(sc);
                }
                else if (tc != null && tc.PlayerStatusColor == this.BackColor)
                {
                    tc.Intelligence++;
                    intelligence.Text = tc.Intelligence.ToString();
                    if (tc.AvailableMana)
                    {
                        this.mana.Text = tc.CurrentMana.ToString() + " / " + tc.MaxMana.ToString();
                    }
                    RefreshPartyMembersBattleStatus(tc);
                }
                CheckUpPoint();
            }
            // オーバーシフティング
            else if (this.useOverShifting)
            {
                this.number = upType.Intelligence;
                grpParameter.Invalidate();
            }
        }


        private void buttonStamina_Click(object sender, EventArgs e)
        {
            // 通常レベルアップ＋１のロジック
            if (this.levelUp)
            {
                if (mc != null && mc.PlayerStatusColor == this.BackColor)
                {
                    mc.Stamina++;
                    stamina.Text = mc.Stamina.ToString();
                    this.life.Text = mc.CurrentLife.ToString() + " / " + mc.MaxLife.ToString();
                    RefreshPartyMembersBattleStatus(mc);
                }
                else if (sc != null && sc.PlayerStatusColor == this.BackColor)
                {
                    sc.Stamina++;
                    stamina.Text = sc.Stamina.ToString();
                    this.life.Text = sc.CurrentLife.ToString() + " / " + sc.MaxLife.ToString();
                    RefreshPartyMembersBattleStatus(sc);
                }
                else if (tc != null && tc.PlayerStatusColor == this.BackColor)
                {
                    tc.Stamina++;
                    stamina.Text = tc.Stamina.ToString();
                    this.life.Text = tc.CurrentLife.ToString() + " / " + tc.MaxLife.ToString();
                    RefreshPartyMembersBattleStatus(tc);
                }
                RefreshPartyMembersLife();
                CheckUpPoint();
            }
            // オーバーシフティング
            else if (this.useOverShifting)
            {
                this.number = upType.Stamina;
                grpParameter.Invalidate();
            }
        }

        private void buttonMind_Click(object sender, EventArgs e)
        {
            // 通常レベルアップ＋１のロジック
            if (this.levelUp)
            {
                if (mc != null && mc.PlayerStatusColor == this.BackColor)
                {
                    mc.Mind++;
                    mindLabel.Text = mc.Mind.ToString();
                    RefreshPartyMembersBattleStatus(mc);
                }
                else if (sc != null && sc.PlayerStatusColor == this.BackColor)
                {
                    sc.Mind++;
                    mindLabel.Text = sc.Mind.ToString();
                    RefreshPartyMembersBattleStatus(sc);
                }
                else if (tc != null && tc.PlayerStatusColor == this.BackColor)
                {
                    tc.Mind++;
                    mindLabel.Text = tc.Mind.ToString();
                    RefreshPartyMembersBattleStatus(tc);
                }
                CheckUpPoint();
            }
            // オーバーシフティング
            else if (this.useOverShifting)
            {
                this.number = upType.Mind;
                grpParameter.Invalidate();
            }
        }

        private void GoLevelUpPoint(upType type, int plus, ref MainCharacter player, ref int remain, ref int addStr, ref int addAgl, ref int addInt, ref int addStm, ref int addMnd)
        {
            if (remain > 0 && remain >= plus)
            {
                remain -= plus;
                if (type == upType.Strength)
                {
                    addStr += plus;
                    player.Strength += plus;
                    strength.Text = player.Strength.ToString();
                }
                else if (type == upType.Agility)
                {
                    addAgl += plus;
                    player.Agility += plus;
                    agility.Text = player.Agility.ToString();
                }
                else if (type == upType.Intelligence)
                {
                    addInt += plus;
                    player.Intelligence += plus;
                    intelligence.Text = player.Intelligence.ToString();
                }
                else if (type == upType.Stamina)
                {
                    addStm += plus;
                    player.Stamina += plus;
                    stamina.Text = player.Stamina.ToString();
                }
                else if (type == upType.Mind)
                {
                    addMnd += plus;
                    player.Mind += plus;
                    mindLabel.Text = player.Mind.ToString();
                }
                RefreshPartyMembersBattleStatus(player);
                RefreshPartyMembersLife();
                lblRemain.Text = "残り " + remain.ToString();
            }
        }
        private void plus1_Click(object sender, EventArgs e)
        {
            MainCharacter player = GetCurrentPlayer();
            int plus = 0;
            if ((sender.Equals(plus1))) { plus = 1; }
            else if ((sender.Equals(plus10))) { plus = 10; }
            else if ((sender.Equals(plus100))) { plus = 100; }
            else if ((sender.Equals(plus1000))) { plus = 1000; }

            GoLevelUpPoint(this.number, plus, ref player, ref this.upPoint, ref this.addStrSC, ref this.addAglSC, ref this.addIntSC, ref this.addStmSC, ref this.addMndSC);
            if (this.upPoint <= 0)
            {
                this.plus1.Visible = false;
                this.plus10.Visible = false;
                this.plus100.Visible = false;
                this.plus1000.Visible = false;
                this.btnUpReset.Visible = false;
                this.lblRemain.Visible = false;
                this.useOverShifting = false;
                this.buttonStrength.Enabled = false;
                this.buttonAgility.Enabled = false;
                this.buttonIntelligence.Enabled = false;
                this.buttonStamina.Enabled = false;
                this.buttonMind.Enabled = false;
                this.grpParameter.Invalidate();
                button1.Visible = true;
                this.mainMessage.Text = "アイン：っしゃ、再割り振り完了！";
                player.CurrentLife = player.MaxLife;
                player.CurrentSkillPoint = player.MaxSkillPoint;
                player.CurrentMana = player.MaxMana;
                SettingCharacterData(player);
                this.Update();
            }
        }

        private void ResetParameter(ref MainCharacter player, ref int remain, ref int addStr, ref int addAgl, ref int addInt, ref int addStm, ref int addMnd)
        {
            remain += addStr; player.Strength -= addStr; addStr = 0;
            remain += addAgl; player.Agility -= addAgl; addAgl = 0;
            remain += addInt; player.Intelligence -= addInt; addInt = 0;
            remain += addStm; player.Stamina -= addStm; addStm = 0;
            remain += addMnd; player.Mind -= addMnd; addMnd = 0;
            lblRemain.Text = "残り　" + remain.ToString();
        }

        private void btnUpReset_Click(object sender, EventArgs e)
        {
            MainCharacter player = GetCurrentPlayer();
            ResetParameter(ref player, ref this.upPoint, ref this.addStrSC, ref this.addAglSC, ref this.addIntSC, ref this.addStmSC, ref this.addMndSC);
            SettingCharacterData(player);
            RefreshPartyMembersBattleStatus(player);
            RefreshPartyMembersLife();
        }

        PopUpMini popupInfo = null;
        private void buttonStrength_MouseEnter(object sender, EventArgs e)
        {
        }

        private void buttonStrength_MouseLeave(object sender, EventArgs e)
        {
            if (popupInfo != null)
            {
                popupInfo.Close();
                popupInfo = null;
            }
        }
        private void buttonStrength_MouseMove(object sender, MouseEventArgs e)
        {
            if (popupInfo == null)
            {
                popupInfo = new PopUpMini();
            }

            popupInfo.StartPosition = FormStartPosition.Manual;

            Button currentBtn = (Button)sender;
            popupInfo.Location = new Point(this.Location.X + currentBtn.Location.X + ((GroupBox)currentBtn.Parent).Location.X + e.X + 5,
                this.Location.Y + currentBtn.Location.Y + ((GroupBox)currentBtn.Parent).Location.Y + e.Y - 18);
            popupInfo.PopupColor = Color.Black;
            System.OperatingSystem os = System.Environment.OSVersion;
            int osNumber = os.Version.Major;
            if (osNumber != 5)
            {
                popupInfo.Opacity = 0.7f;
            }
            if (currentBtn.Equals(buttonStrength)) { popupInfo.CurrentInfo = "【力】パラメタ\r\n物理攻撃／物理防御に影響する。"; }
            else if (currentBtn.Equals(buttonAgility)) { popupInfo.CurrentInfo = "【技】パラメタ\r\n戦闘速度／戦闘反応に影響する。"; }
            else if (currentBtn.Equals(buttonIntelligence)) { popupInfo.CurrentInfo = "【知】パラメタ\r\n魔法攻撃／魔法防御に影響する。"; }
            else if (currentBtn.Equals(buttonStamina)) { popupInfo.CurrentInfo = "【体】パラメタ\r\n最大ライフに影響する。"; }
            else if (currentBtn.Equals(buttonMind)) { popupInfo.CurrentInfo = "【心】パラメタ\r\n潜在能力に影響する。\r\n物理攻撃／物理防御／魔法攻撃／魔法防御／戦闘速度／戦闘反応のベース値に影響する。"; }
            popupInfo.Show();
        }
    }
}
