using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class BattleSpellRequest : MotherForm
    {
        protected string currentSpellName;
        public string CurrentSpellName
        {
            get { return currentSpellName; }
            set { currentSpellName = value; }
        }
        private MainCharacter mc;
        public MainCharacter MC
        {
            get { return mc; }
            set { mc = value; }
        }
        protected int targetNum = 0;
        public int TargetNum
        {
            get { return targetNum; }
            set { targetNum = value; }
        }

        private System.Windows.Forms.Button[] spell;
        private System.Windows.Forms.Label[] spellpt;
        private int MAX_SPELL_LIST = 7;
        private int MAX_SPELL_TYPE = 6;
        private int currentView = 0;

        private WorldEnvironment we;
        public WorldEnvironment WE
        {
            get { return we; }
            set { we = value; }
        }

        private bool ignoreSelectTarget = false;
        public bool IgnoreSelectTarget
        {
            get { return ignoreSelectTarget; }
            set { ignoreSelectTarget = value; }
        }

        private List<int> availableList = null;

        public BattleSpellRequest()
        {
            InitializeComponent();
            availableList = new List<int>();
        }

        private void BattleSpellRequest_Load(object sender, EventArgs e)
        {
            spell = new Button[MAX_SPELL_LIST];
            spellpt = new Label[MAX_SPELL_LIST];
            for (int ii = 0; ii < MAX_SPELL_LIST; ii++)
            {
                spell[ii] = new Button();
                spell[ii].BackColor = Color.White;
                spell[ii].ForeColor = Color.Black;
                spell[ii].Font = new System.Drawing.Font("Lucida Sans Unicode", 14F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                spell[ii].Location = new System.Drawing.Point(90, 80 + 50 * (ii%21));
                spell[ii].Name = "spell" + ii.ToString();
                spell[ii].Size = new Size(420, 40);
                spell[ii].Text = "";
                spell[ii].TabIndex = ii+1;
                spell[ii].Click += new EventHandler(spell_Click);
                spell[ii].MouseEnter += new EventHandler(BattleSpellRequest_MouseEnter);
                spell[ii].MouseMove += new MouseEventHandler(BattleSpellRequest_MouseMove);
                spell[ii].MouseLeave += new EventHandler(BattleSpellRequest_MouseLeave);
                spell[ii].Enter += new EventHandler(BattleSpellRequest_Enter);
                spell[ii].Leave += new EventHandler(BattleSpellRequest_Leave);
                this.Controls.Add(spell[ii]);

                spellpt[ii] = new Label();
                spellpt[ii].BackColor = Color.Transparent;
                spellpt[ii].ForeColor = Color.Black;
                spellpt[ii].Font = new System.Drawing.Font("Lucida Sans Unicode", 14F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                spellpt[ii].Location = new System.Drawing.Point(520, 80 + 50 * (ii % 21));
                spellpt[ii].Name = "skill" + ii.ToString();
                spellpt[ii].Size = new Size(100, 40);
                spellpt[ii].Text = "";
                spellpt[ii].TextAlign = ContentAlignment.MiddleLeft;
                spellpt[ii].TabIndex = 0;
                this.Controls.Add(spellpt[ii]);
            }

            if (mc.FreshHeal || mc.Protection || mc.HolyShock || mc.SaintPower || mc.Glory || mc.Resurrection || mc.CelestialNova)
            {
                this.availableList.Add(0);
            }
            if (mc.DarkBlast || mc.ShadowPact || mc.LifeTap || mc.BlackContract || mc.DevouringPlague || mc.BloodyVengeance || mc.Damnation)
            {
                this.availableList.Add(1);
            }
            if (mc.FireBall || mc.FlameAura || mc.HeatBoost || mc.FlameStrike || mc.VolcanicWave || mc.ImmortalRave || mc.LavaAnnihilation)
            {
                this.availableList.Add(2);
            }
            if (mc.IceNeedle || mc.AbsorbWater || mc.Cleansing || mc.FrozenLance || mc.MirrorImage || mc.PromisedKnowledge || mc.AbsoluteZero)
            {
                this.availableList.Add(3);
            }
            if (mc.WordOfPower || mc.GaleWind || mc.WordOfLife || mc.WordOfFortune || mc.AetherDrive || mc.Genesis || mc.EternalPresence)
            {
                this.availableList.Add(4);
            }
            if (mc.DispelMagic || mc.RiseOfImage || mc.Deflection || mc.Tranquility || mc.OneImmunity || mc.WhiteOut || mc.TimeStop)
            {
                this.availableList.Add(5);
            }

            if (this.availableList.Count <= 0)
            {
                labelNoSpell.Visible = true;
                button1.Enabled = false;
                button3.Enabled = false;
                return;
            }

            labelNoSpell.Visible = false;
            button1.Enabled = true;
            button3.Enabled = true;
            UpdateCurrentView();

            if (mc.BeforePA == PlayerAction.UseSpell)
            {
                int targetCurrentView = 0;
                // ページ位置
                if ((mc.BeforeSpellName == Database.FRESH_HEAL) ||
                     (mc.BeforeSpellName == Database.PROTECTION) ||
                     (mc.BeforeSpellName == Database.HOLY_SHOCK) ||
                     (mc.BeforeSpellName == Database.SAINT_POWER) ||
                     (mc.BeforeSpellName == Database.GLORY) ||
                     (mc.BeforeSpellName == Database.RESURRECTION) ||
                     (mc.BeforeSpellName == Database.CELESTIAL_NOVA))
                {
                    targetCurrentView = 0;
                }
                else if ((mc.BeforeSpellName == Database.DARK_BLAST) ||
                         (mc.BeforeSpellName == Database.SHADOW_PACT) ||
                         (mc.BeforeSpellName == Database.LIFE_TAP) ||
                         (mc.BeforeSpellName == Database.BLACK_CONTRACT) ||
                         (mc.BeforeSpellName == Database.DEVOURING_PLAGUE) ||
                         (mc.BeforeSpellName == Database.BLOODY_VENGEANCE) ||
                         (mc.BeforeSpellName == Database.DAMNATION))

                {
                    targetCurrentView = 1;
                }
                else if ((mc.BeforeSpellName == Database.FIRE_BALL) ||
                         (mc.BeforeSpellName == Database.FLAME_AURA) ||
                         (mc.BeforeSpellName == Database.HEAT_BOOST) ||
                         (mc.BeforeSpellName == Database.FLAME_STRIKE) ||
                         (mc.BeforeSpellName == Database.VOLCANIC_WAVE) ||
                         (mc.BeforeSpellName == Database.IMMORTAL_RAVE) ||
                         (mc.BeforeSpellName == Database.LAVA_ANNIHILATION))
                {
                    targetCurrentView = 2;
                }
                else if ((mc.BeforeSpellName == Database.ICE_NEEDLE) ||
                         (mc.BeforeSpellName == Database.ABSORB_WATER) ||
                         (mc.BeforeSpellName == Database.MIRROR_IMAGE) ||
                         (mc.BeforeSpellName == Database.CLEANSING) ||
                         (mc.BeforeSpellName == Database.FROZEN_LANCE) ||
                         (mc.BeforeSpellName == Database.PROMISED_KNOWLEDGE) ||
                         (mc.BeforeSpellName == Database.ABSOLUTE_ZERO))
                {
                    targetCurrentView = 3;
                }
                else if ((mc.BeforeSpellName == Database.WORD_OF_POWER) ||
                         (mc.BeforeSpellName == Database.GALE_WIND) ||
                         (mc.BeforeSpellName == Database.WORD_OF_LIFE) ||
                         (mc.BeforeSpellName == Database.WORD_OF_FORTUNE) ||
                         (mc.BeforeSpellName == Database.AETHER_DRIVE) ||
                         (mc.BeforeSpellName == Database.GENESIS) ||
                         (mc.BeforeSpellName == Database.ETERNAL_PRESENCE))
                {
                    targetCurrentView = 4;
                }
                else if ((mc.BeforeSpellName == Database.DISPEL_MAGIC) ||
                         (mc.BeforeSpellName == Database.RISE_OF_IMAGE) ||
                         (mc.BeforeSpellName == Database.DEFLECTION) ||
                         (mc.BeforeSpellName == Database.TRANQUILITY) ||
                         (mc.BeforeSpellName == Database.ONE_IMMUNITY) ||
                         (mc.BeforeSpellName == Database.WHITE_OUT) ||
                         (mc.BeforeSpellName == Database.TIME_STOP))
                {
                    targetCurrentView = 5;
                }

                for (int ii = 0; ii < availableList.Count; ii++)
                {
                    if (availableList[ii] == targetCurrentView)
                    {
                        this.currentView = ii;
                        UpdateCurrentView();
                    }
                }

                // フォーカス位置
                if ( (mc.BeforeSpellName == Database.FRESH_HEAL) ||
                     (mc.BeforeSpellName == Database.DARK_BLAST) ||
                     (mc.BeforeSpellName == Database.FIRE_BALL) ||
                     (mc.BeforeSpellName == Database.ICE_NEEDLE) ||
                     (mc.BeforeSpellName == Database.WORD_OF_POWER) ||
                     (mc.BeforeSpellName == Database.DISPEL_MAGIC))
                {
                    spell[0].Focus();
                    spell[0].Select();
                }
                else if ((mc.BeforeSpellName == Database.PROTECTION) ||
                         (mc.BeforeSpellName == Database.SHADOW_PACT) ||
                         (mc.BeforeSpellName == Database.FLAME_AURA) ||
                         (mc.BeforeSpellName == Database.ABSORB_WATER) ||
                         (mc.BeforeSpellName == Database.GALE_WIND) ||
                         (mc.BeforeSpellName == Database.RISE_OF_IMAGE))
                {
                    spell[1].Focus();
                    spell[1].Select();
                }
                else if ((mc.BeforeSpellName == Database.HOLY_SHOCK) ||
                         (mc.BeforeSpellName == Database.LIFE_TAP) ||
                         (mc.BeforeSpellName == Database.HEAT_BOOST) ||
                         (mc.BeforeSpellName == Database.CLEANSING) ||
                         (mc.BeforeSpellName == Database.WORD_OF_LIFE) ||
                         (mc.BeforeSpellName == Database.DEFLECTION))
                {
                    spell[2].Focus();
                    spell[2].Select();
                }
                else if ((mc.BeforeSpellName == Database.SAINT_POWER) ||
                         (mc.BeforeSpellName == Database.BLACK_CONTRACT) ||
                         (mc.BeforeSpellName == Database.FLAME_STRIKE) ||
                         (mc.BeforeSpellName == Database.FROZEN_LANCE) ||
                         (mc.BeforeSpellName == Database.WORD_OF_FORTUNE) ||
                         (mc.BeforeSpellName == Database.TRANQUILITY))
                {
                    spell[3].Focus();
                    spell[3].Select();
                }
                else if ((mc.BeforeSpellName == Database.GLORY) ||
                         (mc.BeforeSpellName == Database.DEVOURING_PLAGUE) ||
                         (mc.BeforeSpellName == Database.VOLCANIC_WAVE) ||
                         (mc.BeforeSpellName == Database.MIRROR_IMAGE) ||
                         (mc.BeforeSpellName == Database.AETHER_DRIVE) ||
                         (mc.BeforeSpellName == Database.ONE_IMMUNITY))
                {
                    spell[4].Focus();
                    spell[4].Select();
                }
                else if ((mc.BeforeSpellName == Database.RESURRECTION) ||
                         (mc.BeforeSpellName == Database.BLOODY_VENGEANCE) ||
                         (mc.BeforeSpellName == Database.IMMORTAL_RAVE) ||
                         (mc.BeforeSpellName == Database.PROMISED_KNOWLEDGE) ||
                         (mc.BeforeSpellName == Database.GENESIS) || 
                         (mc.BeforeSpellName == Database.WHITE_OUT))
                {
                    spell[5].Focus();
                    spell[5].Select();
                }
                else if ((mc.BeforeSpellName == Database.CELESTIAL_NOVA) ||
                         (mc.BeforeSpellName == Database.DAMNATION) ||
                         (mc.BeforeSpellName == Database.LAVA_ANNIHILATION) ||
                         (mc.BeforeSpellName == Database.ABSOLUTE_ZERO) ||
                         (mc.BeforeSpellName == Database.ETERNAL_PRESENCE) ||
                         (mc.BeforeSpellName == Database.TIME_STOP))
                {
                    spell[6].Focus();
                    spell[6].Select();
                }
            }
        }

        void BattleSpellRequest_MouseEnter(object sender, EventArgs e)
        {
            UpdateCurrentTarget(sender);
        }

        void BattleSpellRequest_Leave(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.White;            
        }

        void BattleSpellRequest_Enter(object sender, EventArgs e)
        {
            UpdateCurrentTarget(sender);
        }

        private void UpdateCurrentTarget(object sender)
        {
            for (int ii = 0; ii < MAX_SPELL_LIST; ii++)
            {
                if (sender.Equals(spell[ii]))
                {
                    ((Button)spell[ii]).BackColor = Color.Khaki;
                }
                else
                {
                    ((Button)spell[ii]).BackColor = Color.White;
                }
            }
            if (sender.Equals(button1)) { button1.BackColor = Color.Khaki; } else { button1.BackColor = Color.White; }
            if (sender.Equals(button2)) { button2.BackColor = Color.Khaki; } else { button2.BackColor = Color.White; }
            if (sender.Equals(button3)) { button3.BackColor = Color.Khaki; } else { button3.BackColor = Color.White; }
        }

        private PopUpMini popupInfo = null;
        void BattleSpellRequest_MouseLeave(object sender, EventArgs e)
        {
            if (popupInfo != null)
            {
                popupInfo.Close();
                popupInfo = null;
            }
            ((Button)sender).BackColor = Color.White;            

        }

        void BattleSpellRequest_MouseMove(object sender, MouseEventArgs e)
        {
            if (popupInfo == null)
            {
                popupInfo = new PopUpMini();
            }

            // 聖
            if (sender.Equals(spell[0]) && spell[0].Text == Database.FRESH_HEAL) popupInfo.CurrentInfo = "単一対象：ライフを回復";
            if (sender.Equals(spell[1]) && spell[1].Text == Database.PROTECTION) popupInfo.CurrentInfo = "単一対象：物理防御をＵＰ";
            if (sender.Equals(spell[2]) && spell[2].Text == Database.HOLY_SHOCK) popupInfo.CurrentInfo = "敵対象：ダメージ";
            if (sender.Equals(spell[3]) && spell[3].Text == Database.SAINT_POWER) popupInfo.CurrentInfo = "単一対象：物理攻撃をＵＰ";
            if (sender.Equals(spell[4]) && spell[4].Text == Database.GLORY) popupInfo.CurrentInfo = "自分対象：次の３ターン、直接攻撃＋FreshHeal(コスト０)";
            if (sender.Equals(spell[5]) && spell[5].Text == Database.RESURRECTION) popupInfo.CurrentInfo = "単一対象：蘇生する";
            if (sender.Equals(spell[6]) && spell[6].Text == Database.CELESTIAL_NOVA) popupInfo.CurrentInfo = "単一対象：敵にダメージ、または、味方のライフを回復";
            // 闇
            if (sender.Equals(spell[0]) && spell[0].Text == Database.DARK_BLAST) popupInfo.CurrentInfo = "敵対象：ダメージ";
            if (sender.Equals(spell[1]) && spell[1].Text == Database.SHADOW_PACT) popupInfo.CurrentInfo = "単一対象：魔法攻撃をＵＰ";
            if (sender.Equals(spell[2]) && spell[2].Text == Database.LIFE_TAP) popupInfo.CurrentInfo = "単一対象：ライフを回復";
            if (sender.Equals(spell[3]) && spell[3].Text == Database.BLACK_CONTRACT) popupInfo.CurrentInfo = "自分対象：次の３ターン、10%のライフを失うのと引き換え。\nスキル、魔法コストを０にする";
            if (sender.Equals(spell[4]) && spell[4].Text == Database.DEVOURING_PLAGUE) popupInfo.CurrentInfo = "敵対象：敵にダメージ＋自分のライフを回復";
            if (sender.Equals(spell[5]) && spell[5].Text == Database.BLOODY_VENGEANCE) popupInfo.CurrentInfo = "単一対象：力パラメタをＵＰ";
            if (sender.Equals(spell[6]) && spell[6].Text == Database.DAMNATION) popupInfo.CurrentInfo = "敵対象：毎ターン、闇からのダメージ";
            // 火
            if (sender.Equals(spell[0]) && spell[0].Text == Database.FIRE_BALL) popupInfo.CurrentInfo = "敵対象：ダメージ";
            if (sender.Equals(spell[1]) && spell[1].Text == Database.FLAME_AURA) popupInfo.CurrentInfo = "単一対象：直接攻撃に炎付随追加ダメージ";
            if (sender.Equals(spell[2]) && spell[2].Text == Database.HEAT_BOOST) popupInfo.CurrentInfo = "単一対象：技パラメタをＵＰ";
            if (sender.Equals(spell[3]) && spell[3].Text == Database.FLAME_STRIKE) popupInfo.CurrentInfo = "敵対象：ダメージ";
            if (sender.Equals(spell[4]) && spell[4].Text == Database.VOLCANIC_WAVE) popupInfo.CurrentInfo = "敵対象：ダメージ";
            if (sender.Equals(spell[5]) && spell[5].Text == Database.IMMORTAL_RAVE) popupInfo.CurrentInfo = "自分対象：次の３ターン、直接攻撃＋火攻撃スペル（コスト０）を行う";
            if (sender.Equals(spell[6]) && spell[6].Text == Database.LAVA_ANNIHILATION) popupInfo.CurrentInfo = "敵全対象：ダメージ";
            // 水
            if (sender.Equals(spell[0]) && spell[0].Text == Database.ICE_NEEDLE) popupInfo.CurrentInfo = "敵対象：ダメージ";
            if (sender.Equals(spell[1]) && spell[1].Text == Database.ABSORB_WATER) popupInfo.CurrentInfo = "単一対象：魔法防御をＵＰ";
            if (sender.Equals(spell[2]) && spell[2].Text == Database.CLEANSING) popupInfo.CurrentInfo = "単一対象：スタン・沈黙・猛毒・誘惑・凍結・麻痺・鈍化を解除"; // 後編編集（鈍化追記）
            if (sender.Equals(spell[3]) && spell[3].Text == Database.FROZEN_LANCE) popupInfo.CurrentInfo = "敵対象：ダメージ";
            if (sender.Equals(spell[4]) && spell[4].Text == Database.MIRROR_IMAGE) popupInfo.CurrentInfo = "単一対象：ダメージ系魔法を反射する。\n" + Database.WORD_OF_POWER + "は反射できない。";
            if (sender.Equals(spell[5]) && spell[5].Text == Database.PROMISED_KNOWLEDGE) popupInfo.CurrentInfo = "単一対象：知パラメタをＵＰ";
            if (sender.Equals(spell[6]) && spell[6].Text == Database.ABSOLUTE_ZERO) popupInfo.CurrentInfo = "敵対象：次の３ターン、敵はライフ回復不可、\nスペル詠唱不可、スキル使用不可、防御不可";
            // 理
            if (sender.Equals(spell[0]) && spell[0].Text == Database.WORD_OF_POWER) popupInfo.CurrentInfo = "敵対象：対象の防御体制を無視した上で、\n魔法による物理ダメージ。";
            if (sender.Equals(spell[1]) && spell[1].Text == Database.GALE_WIND) popupInfo.CurrentInfo = "自分対象：次のターン、同一コマンドを連続２回行動";
            if (sender.Equals(spell[2]) && spell[2].Text == Database.WORD_OF_LIFE) popupInfo.CurrentInfo = "単一対象：毎ターン、ライフを回復";
            if (sender.Equals(spell[3]) && spell[3].Text == Database.WORD_OF_FORTUNE) popupInfo.CurrentInfo = "単一対象：次のターン、必ずクリティカルヒット";
            if (sender.Equals(spell[4]) && spell[4].Text == Database.AETHER_DRIVE) popupInfo.CurrentInfo = "自分対象：次の３ターン、敵からの物理ダメージ半減\n自分からの物理ダメージ２倍";
            if (sender.Equals(spell[5]) && spell[5].Text == Database.GENESIS) popupInfo.CurrentInfo = "自分対象：前回とった行動と同じ行動を行う。\n前回消費したスペル、スキルコストは０";
            if (sender.Equals(spell[6]) && spell[6].Text == Database.ETERNAL_PRESENCE) popupInfo.CurrentInfo = "単一対象：物理攻撃、物理防御、魔法攻撃、魔法防御UP";
            // 空
            if (sender.Equals(spell[0]) && spell[0].Text == Database.DISPEL_MAGIC) popupInfo.CurrentInfo = "敵対象：Protection、SaintPower、ShadowPact \n BloodyVengeance、FlameAura, HeatBoost \n AbsorbWater、WordOfLife \n EternalPresence、RiseOfImageを解除する";
            if (sender.Equals(spell[1]) && spell[1].Text == Database.RISE_OF_IMAGE) popupInfo.CurrentInfo = "単一対象：心パラメタをＵＰ";
            if (sender.Equals(spell[2]) && spell[2].Text == Database.DEFLECTION) popupInfo.CurrentInfo = "単一対象：物理攻撃を反射する。\n" + Database.WORD_OF_POWER + "は反射できない。";
            if (sender.Equals(spell[3]) && spell[3].Text == Database.TRANQUILITY) popupInfo.CurrentInfo = "敵対象：Glory、BlackContract, ImmortalRave \n AbsoluteZero、AetherDrive \n OneImmunity、HighEmotionalityを解除する";
            if (sender.Equals(spell[4]) && spell[4].Text == Database.ONE_IMMUNITY) popupInfo.CurrentInfo = "自分対象：次の３ターン、防御している間、全ダメージを無効化。\n" + Database.WORD_OF_POWER + "は防御できない。";
            if (sender.Equals(spell[5]) && spell[5].Text == Database.WHITE_OUT) popupInfo.CurrentInfo = "敵対象：ダメージ";
            if (sender.Equals(spell[6]) && spell[6].Text == Database.TIME_STOP) popupInfo.CurrentInfo = "敵対象：相手の現在ターンと次のターンを飛ばす";
            
            popupInfo.StartPosition = FormStartPosition.Manual;
            popupInfo.Location = new Point(this.Location.X + ((Button)sender).Location.X + e.X + 10, this.Location.Y + ((Button)sender).Location.Y + e.Y + 0);
            popupInfo.PopupColor = Color.Black;
            //popupInfo.PopupTextColor = Brushes.White; // 後編削除
            popupInfo.Show();
        }

        void spell_Click(object sender, EventArgs e)
        {
            this.currentSpellName = ((Button)sender).Text;

            if (this.currentSpellName == Database.FRESH_HEAL ||
                this.currentSpellName == Database.PROTECTION ||
                this.currentSpellName == Database.SAINT_POWER ||
                this.currentSpellName == Database.RESURRECTION ||
                this.currentSpellName == Database.CELESTIAL_NOVA ||
                this.currentSpellName == Database.SHADOW_PACT ||
                this.currentSpellName == Database.LIFE_TAP ||
                this.currentSpellName == Database.BLOODY_VENGEANCE ||
                this.currentSpellName == Database.FLAME_AURA ||
                this.currentSpellName == Database.HEAT_BOOST ||
                this.currentSpellName == Database.ABSORB_WATER ||
                this.currentSpellName == Database.CLEANSING ||
                this.currentSpellName == Database.MIRROR_IMAGE ||
                this.CurrentSpellName == Database.PROMISED_KNOWLEDGE ||
                this.currentSpellName == Database.WORD_OF_LIFE ||
                this.currentSpellName == Database.WORD_OF_FORTUNE ||
                this.currentSpellName == Database.ETERNAL_PRESENCE ||
                this.currentSpellName == Database.RISE_OF_IMAGE ||
                this.currentSpellName == Database.DEFLECTION)
            {
                using (SelectTarget st = new SelectTarget())
                {
                    st.StartPosition = FormStartPosition.CenterParent;
                    if (!ignoreSelectTarget)
                    {
                        if (we.AvailableFirstCharacter && we.AvailableSecondCharacter && we.AvailableThirdCharacter)
                        {
                            st.FirstName = "アイン";
                            st.SecondName = "ラナ";
                            st.ThirdName = "ヴェルゼ";
                            st.FourthName = "敵";
                            st.MaxSelectable = 4;
                        }
                        else if (we.AvailableFirstCharacter && we.AvailableSecondCharacter)
                        {
                            st.FirstName = "アイン";
                            st.SecondName = "ラナ";
                            st.ThirdName = "敵";
                            st.MaxSelectable = 3;
                        }
                        else if (we.AvailableFirstCharacter)
                        {
                            st.FirstName = "アイン";
                            st.SecondName = "敵";
                            st.MaxSelectable = 2;
                        }
                    }
                    else
                    {
                        st.FirstName = "アイン";
                        st.SecondName = "敵";
                        st.MaxSelectable = 2;
                    }
                    st.ShowDialog();
                    if (!ignoreSelectTarget)
                    {
                        if (we.AvailableFirstCharacter && we.AvailableSecondCharacter && we.AvailableThirdCharacter)
                        {
                            if (st.TargetNum == 0) DialogResult = DialogResult.None;
                            else
                            {
                                this.targetNum = st.TargetNum; // 1:アイン、2:ラナ、3：ヴェルゼ、4：敵
                                DialogResult = DialogResult.OK;
                            }
                        }
                        else if (we.AvailableFirstCharacter && we.AvailableSecondCharacter)
                        {
                            if (st.TargetNum == 0) DialogResult = DialogResult.None;
                            else
                            {
                                if (st.TargetNum == 3)
                                {
                                    this.targetNum = 4; // 敵
                                }
                                else
                                {
                                    this.targetNum = st.TargetNum; // 1:アイン、2:ラナ
                                }
                                DialogResult = DialogResult.OK;
                            }
                        }
                        else if (we.AvailableFirstCharacter)
                        {
                            if (st.TargetNum == 0) DialogResult = DialogResult.None;
                            else
                            {
                                if (st.TargetNum == 2)
                                {
                                    this.targetNum = 4; // 敵
                                }
                                else
                                {
                                    this.targetNum = st.TargetNum; // 1:アイン
                                }
                                DialogResult = DialogResult.OK;
                            }
                        }
                        else
                        {
                            DialogResult = DialogResult.OK;
                        }
                    }
                    else
                    {
                        if (st.TargetNum == 0) DialogResult = DialogResult.None;
                        else
                        {
                            if (st.TargetNum == 2)
                            {
                                this.targetNum = 4; // 敵
                            }
                            else
                            {
                                this.targetNum = st.TargetNum; // 1:アイン
                            }
                            DialogResult = DialogResult.OK;
                        }
                    }
                }
            }
            else
            {
                DialogResult = DialogResult.OK;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (currentView <= 0) currentView = this.availableList.Count - 1;
            else currentView--;
            UpdateCurrentView();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (currentView >= this.availableList.Count - 1) currentView = 0;
            else currentView++;
            UpdateCurrentView();
        }

        private void UpdateCurrentView()
        {
            UpdateCurrentView(this.currentView);
        }
        private void UpdateCurrentView(int currentNumber)
        {
            for (int ii = 0; ii < MAX_SPELL_LIST; ii++)
            {
                label1.Text = "";
                spell[ii].Text = "";
                spellpt[ii].Text = "";
                spell[ii].Visible = true;
                spellpt[ii].Visible = true;
            }

            string pt = "pt";
            switch (this.availableList[currentNumber])
            {
                case 0:
                    label1.Text = "聖 (Light)";
                    if (mc.FreshHeal)     { spell[0].Text = Database.FRESH_HEAL;     spellpt[0].Text = Database.FRESH_HEAL_COST.ToString()     + pt; } else { spell[0].Visible = false; spellpt[0].Visible = false; }
                    if (mc.Protection)    { spell[1].Text = Database.PROTECTION;     spellpt[1].Text = Database.PROTECTION_COST.ToString()     + pt; } else { spell[1].Visible = false; spellpt[1].Visible = false; }
                    if (mc.HolyShock)     { spell[2].Text = Database.HOLY_SHOCK;     spellpt[2].Text = Database.HOLY_SHOCK_COST.ToString()     + pt; } else { spell[2].Visible = false; spellpt[2].Visible = false; }
                    if (mc.SaintPower)    { spell[3].Text = Database.SAINT_POWER;    spellpt[3].Text = Database.SAINT_POWER_COST.ToString()    + pt; } else { spell[3].Visible = false; spellpt[3].Visible = false; }
                    if (mc.Glory)         { spell[4].Text = Database.GLORY;          spellpt[4].Text = Database.GLORY_COST.ToString()          + pt; } else { spell[4].Visible = false; spellpt[4].Visible = false; }
                    if (mc.Resurrection)  { spell[5].Text = Database.RESURRECTION;   spellpt[5].Text = Database.RESURRECTION_COST.ToString()   + pt; } else { spell[5].Visible = false; spellpt[5].Visible = false; }
                    if (mc.CelestialNova) { spell[6].Text = Database.CELESTIAL_NOVA; spellpt[6].Text = Database.CELESTIAL_NOVA_COST.ToString() + pt; } else { spell[6].Visible = false; spellpt[6].Visible = false; }
                    break;

                case 1:
                    label1.Text = "闇 (Shadow)";
                    if (mc.DarkBlast)       { spell[0].Text = Database.DARK_BLAST;       spellpt[0].Text = Database.DARK_BLAST_COST.ToString()       + pt; } else { spell[0].Visible = false; spellpt[0].Visible = false; }
                    if (mc.ShadowPact)      { spell[1].Text = Database.SHADOW_PACT;      spellpt[1].Text = Database.SHADOW_PACT_COST.ToString()      + pt; } else { spell[1].Visible = false; spellpt[1].Visible = false; }
                    if (mc.LifeTap)         { spell[2].Text = Database.LIFE_TAP;         spellpt[2].Text = Database.LIFE_TAP_COST.ToString()    + pt; } else { spell[2].Visible = false; spellpt[2].Visible = false; }
                    if (mc.BlackContract)   { spell[3].Text = Database.BLACK_CONTRACT;   spellpt[3].Text = Database.BLACK_CONTRACT_COST.ToString()   + pt; } else { spell[3].Visible = false; spellpt[3].Visible = false; }
                    if (mc.DevouringPlague) { spell[4].Text = Database.DEVOURING_PLAGUE; spellpt[4].Text = Database.DEVOURING_PLAGUE_COST.ToString() + pt; } else { spell[4].Visible = false; spellpt[4].Visible = false; }
                    if (mc.BloodyVengeance) { spell[5].Text = Database.BLOODY_VENGEANCE; spellpt[5].Text = Database.BLOODY_VENGEANCE_COST.ToString() + pt; } else { spell[5].Visible = false; spellpt[5].Visible = false; }
                    if (mc.Damnation)       { spell[6].Text = Database.DAMNATION;        spellpt[6].Text = Database.DAMNATION_COST.ToString()        + pt; } else { spell[6].Visible = false; spellpt[6].Visible = false; }
                    break;

                case 2:
                    label1.Text = "火 (Fire)";
                    if (mc.FireBall)         { spell[0].Text = Database.FIRE_BALL;         spellpt[0].Text = Database.FIRE_BALL_COST.ToString()         + pt; } else { spell[0].Visible = false; spellpt[0].Visible = false; }
                    if (mc.FlameAura)        { spell[1].Text = Database.FLAME_AURA;        spellpt[1].Text = Database.FLAME_AURA_COST.ToString()        + pt; } else { spell[1].Visible = false; spellpt[1].Visible = false; }
                    if (mc.HeatBoost)        { spell[2].Text = Database.HEAT_BOOST;        spellpt[2].Text = Database.HEAT_BOOST_COST.ToString()        + pt; } else { spell[2].Visible = false; spellpt[2].Visible = false; }
                    if (mc.FlameStrike)      { spell[3].Text = Database.FLAME_STRIKE;      spellpt[3].Text = Database.FLAME_STRIKE_COST.ToString()      + pt; } else { spell[3].Visible = false; spellpt[3].Visible = false; }
                    if (mc.VolcanicWave)     { spell[4].Text = Database.VOLCANIC_WAVE;     spellpt[4].Text = Database.VOLCANIC_WAVE_COST.ToString()     + pt; } else { spell[4].Visible = false; spellpt[4].Visible = false; }
                    if (mc.ImmortalRave)     { spell[5].Text = Database.IMMORTAL_RAVE;     spellpt[5].Text = Database.IMMORTAL_RAVE_COST.ToString()     + pt; } else { spell[5].Visible = false; spellpt[5].Visible = false; }
                    if (mc.LavaAnnihilation) { spell[6].Text = Database.LAVA_ANNIHILATION; spellpt[6].Text = Database.LAVA_ANNIHILATION_COST.ToString() + pt; } else { spell[6].Visible = false; spellpt[6].Visible = false; }
                    break;

                case 3:
                    label1.Text = "水 (Ice)";
                    if (mc.IceNeedle)         { spell[0].Text = Database.ICE_NEEDLE;         spellpt[0].Text = Database.ICE_NEEDLE_COST.ToString()         + pt; } else { spell[0].Visible = false; spellpt[0].Visible = false; }
                    if (mc.AbsorbWater)       { spell[1].Text = Database.ABSORB_WATER;       spellpt[1].Text = Database.ABSORB_WATER_COST.ToString()       + pt; } else { spell[1].Visible = false; spellpt[1].Visible = false; }
                    if (mc.Cleansing)         { spell[2].Text = Database.CLEANSING;          spellpt[2].Text = Database.CLEANSING_COST.ToString()          + pt; } else { spell[2].Visible = false; spellpt[2].Visible = false; }
                    if (mc.FrozenLance)       { spell[3].Text = Database.FROZEN_LANCE;       spellpt[3].Text = Database.FROZEN_LANCE_COST.ToString()       + pt; } else { spell[3].Visible = false; spellpt[3].Visible = false; }
                    if (mc.MirrorImage)       { spell[4].Text = Database.MIRROR_IMAGE;       spellpt[4].Text = Database.MIRROR_IMAGE_COST.ToString()       + pt; } else { spell[4].Visible = false; spellpt[4].Visible = false; }
                    if (mc.PromisedKnowledge) { spell[5].Text = Database.PROMISED_KNOWLEDGE; spellpt[5].Text = Database.PROMISED_KNOWLEDGE_COST.ToString() + pt; } else { spell[5].Visible = false; spellpt[5].Visible = false; }
                    if (mc.AbsoluteZero)      { spell[6].Text = Database.ABSOLUTE_ZERO;      spellpt[6].Text = Database.ABSOLUTE_ZERO_COST.ToString()      + pt; } else { spell[6].Visible = false; spellpt[6].Visible = false; }
                    break;

                case 4:
                    label1.Text = "理 (Force)";
                    if (mc.WordOfPower)     { spell[0].Text = Database.WORD_OF_POWER;    spellpt[0].Text = Database.WORD_OF_POWER_COST.ToString()    + pt; } else { spell[0].Visible = false; spellpt[0].Visible = false; }
                    if (mc.GaleWind)        { spell[1].Text = Database.GALE_WIND;        spellpt[1].Text = Database.GALE_WIND_COST.ToString()        + pt; } else { spell[1].Visible = false; spellpt[1].Visible = false; }
                    if (mc.WordOfLife)      { spell[2].Text = Database.WORD_OF_LIFE;     spellpt[2].Text = Database.WORD_OF_LIFE_COST.ToString()     + pt; } else { spell[2].Visible = false; spellpt[2].Visible = false; }
                    if (mc.WordOfFortune)   { spell[3].Text = Database.WORD_OF_FORTUNE;  spellpt[3].Text = Database.WORD_OF_FORTUNE_COST.ToString()  + pt; } else { spell[3].Visible = false; spellpt[3].Visible = false; }
                    if (mc.AetherDrive)     { spell[4].Text = Database.AETHER_DRIVE;     spellpt[4].Text = Database.AETHER_DRIVE_COST.ToString()     + pt; } else { spell[4].Visible = false; spellpt[4].Visible = false; }
                    if (mc.Genesis)         { spell[5].Text = Database.GENESIS;          spellpt[5].Text = Database.GENESIS_COST.ToString()          + pt; } else { spell[5].Visible = false; spellpt[5].Visible = false; }
                    if (mc.EternalPresence) { spell[6].Text = Database.ETERNAL_PRESENCE; spellpt[6].Text = Database.ETERNAL_PRESENCE_COST.ToString() + pt; } else { spell[6].Visible = false; spellpt[6].Visible = false; }
                    break;

                case 5:
                    label1.Text = "空 (Will)";
                    if (mc.DispelMagic) { spell[0].Text = Database.DISPEL_MAGIC;  spellpt[0].Text = Database.DISPEL_MAGIC_COST.ToString()  + pt; } else { spell[0].Visible = false; spellpt[0].Visible = false; }
                    if (mc.RiseOfImage) { spell[1].Text = Database.RISE_OF_IMAGE; spellpt[1].Text = Database.RISE_OF_IMAGE_COST.ToString() + pt; } else { spell[1].Visible = false; spellpt[1].Visible = false; }
                    if (mc.Deflection)  { spell[2].Text = Database.DEFLECTION;    spellpt[2].Text = Database.DEFLECTION_COST.ToString()    + pt; } else { spell[2].Visible = false; spellpt[2].Visible = false; }
                    if (mc.Tranquility) { spell[3].Text = Database.TRANQUILITY;   spellpt[3].Text = Database.TRANQUILITY_COST.ToString()   + pt; } else { spell[3].Visible = false; spellpt[3].Visible = false; }
                    if (mc.OneImmunity) { spell[4].Text = Database.ONE_IMMUNITY;  spellpt[4].Text = Database.ONE_IMMUNITY_COST.ToString()  + pt; } else { spell[4].Visible = false; spellpt[4].Visible = false; }
                    if (mc.WhiteOut)    { spell[5].Text = Database.WHITE_OUT;     spellpt[5].Text = Database.WHITE_OUT_COST.ToString()     + pt; } else { spell[5].Visible = false; spellpt[5].Visible = false; }
                    if (mc.TimeStop)    { spell[6].Text = Database.TIME_STOP;     spellpt[6].Text = Database.TIME_STOP_COST.ToString()     + pt; } else { spell[6].Visible = false; spellpt[6].Visible = false; }
                    break;
            }
        }
    }
}