using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class BattleSkillRequest : MotherForm
    {
        protected string currentSkillName;
        public string CurrentSkillName
        {
            get { return currentSkillName; }
            set { currentSkillName = value; }
        }
        private MainCharacter mc;
        public MainCharacter MC
        {
            get { return mc; }
            set { mc = value; }
        }

        private System.Windows.Forms.Button[] skill;
        private System.Windows.Forms.Label[] skillpt;
        private int MAX_SKILL_LIST = 4;
        private int MAX_SKILL_TYPE = 6;
        private int currentView = 0;

        private List<int> availableList = null;

        public BattleSkillRequest()
        {
            InitializeComponent();
            availableList = new List<int>();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void BattleSkillRequest_Load(object sender, EventArgs e)
        {
            skill = new Button[MAX_SKILL_LIST];
            skillpt = new Label[MAX_SKILL_LIST];

            for (int ii = 0; ii < MAX_SKILL_LIST; ii++)
            {
                skill[ii] = new Button();
                skill[ii].BackColor = Color.White;
                skill[ii].ForeColor = Color.Black;
                skill[ii].Font = new System.Drawing.Font("Lucida Sans Unicode", 14F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                skill[ii].Location = new System.Drawing.Point(90, 80 + 50 * (ii % 21));
                skill[ii].Name = "skill" + ii.ToString();
                skill[ii].Size = new Size(420, 40);
                skill[ii].Text = "";
                skill[ii].TabIndex = 0;
                skill[ii].Click += new EventHandler(skill_Click);
                skill[ii].MouseEnter += new EventHandler(BattleSkillRequest_MouseEnter);
                skill[ii].MouseMove += new MouseEventHandler(BattleSkillRequest_MouseMove);
                skill[ii].MouseLeave += new EventHandler(BattleSkillRequest_MouseLeave);
                skill[ii].Enter += new EventHandler(BattleSkillRequest_Enter);
                skill[ii].Leave += new EventHandler(BattleSkillRequest_Leave);
                this.Controls.Add(skill[ii]);

                skillpt[ii] = new Label();
                skillpt[ii].BackColor = Color.Transparent;
                skillpt[ii].ForeColor = Color.Black;
                skillpt[ii].Font = new System.Drawing.Font("Lucida Sans Unicode", 14F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                skillpt[ii].Location = new System.Drawing.Point(520, 80 + 50 * (ii % 21));
                skillpt[ii].Name = "skill" + ii.ToString();
                skillpt[ii].Size = new Size(100, 40);
                skillpt[ii].Text = "";
                skillpt[ii].TextAlign = ContentAlignment.MiddleLeft;
                skillpt[ii].TabIndex = 0;
                this.Controls.Add(skillpt[ii]);
            }

            if (mc.StraightSmash || mc.DoubleSlash || mc.CrushingBlow || mc.SoulInfinity)
            {
                availableList.Add(0);
            }
            if (mc.CounterAttack || mc.PurePurification || mc.AntiStun || mc.StanceOfDeath)
            {
                availableList.Add(1);
            }
            if (mc.StanceOfFlow || mc.EnigmaSence || mc.SilentRush || mc.OboroImpact)
            {
                availableList.Add(2);
            }
            if (mc.StanceOfStanding || mc.InnerInspiration || mc.KineticSmash || mc.Catastrophe)
            {
                availableList.Add(3);
            }
            if (mc.TruthVision || mc.HighEmotionality || mc.StanceOfEyes || mc.PainfulInsanity)
            {
                availableList.Add(4);
            }
            if (mc.Negate || mc.VoidExtraction || mc.CarnageRush || mc.NothingOfNothingness)
            {
                availableList.Add(5);
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

            if (mc.BeforePA == PlayerAction.UseSkill)
            {
                int targetCurrentView = 0;
                // ページ位置
                if ((mc.BeforeSkillName == Database.STRAIGHT_SMASH) ||
                     (mc.BeforeSkillName == Database.DOUBLE_SLASH) ||
                     (mc.BeforeSkillName == Database.CRUSHING_BLOW) ||
                     (mc.BeforeSkillName == Database.SOUL_INFINITY))
                {
                    targetCurrentView = 0;
                }
                else if ((mc.BeforeSkillName == Database.COUNTER_ATTACK) ||
                         (mc.BeforeSkillName == Database.PURE_PURIFICATION) ||
                         (mc.BeforeSkillName == Database.ANTI_STUN) ||
                         (mc.BeforeSkillName == Database.STANCE_OF_DEATH))
                {
                    targetCurrentView = 1;
                }
                else if ((mc.BeforeSkillName == Database.STANCE_OF_FLOW) ||
                         (mc.BeforeSkillName == Database.ENIGMA_SENSE) ||
                         (mc.BeforeSkillName == Database.SILENT_RUSH) ||
                         (mc.BeforeSkillName == Database.OBORO_IMPACT))
                {
                    targetCurrentView = 2;
                }
                else if ((mc.BeforeSkillName == Database.STANCE_OF_STANDING) ||
                         (mc.BeforeSkillName == Database.INNER_INSPIRATION) ||
                         (mc.BeforeSkillName == Database.KINETIC_SMASH) ||
                         (mc.BeforeSkillName == Database.CATASTROPHE))
                {
                    targetCurrentView = 3;
                }
                else if ((mc.BeforeSkillName == Database.TRUTH_VISION) ||
                         (mc.BeforeSkillName == Database.HIGH_EMOTIONALITY) ||
                         (mc.BeforeSkillName == Database.STANCE_OF_EYES) ||
                         (mc.BeforeSkillName == Database.PAINFUL_INSANITY))
                {
                    targetCurrentView = 4;
                }
                else if ((mc.BeforeSkillName == Database.NEGATE) ||
                         (mc.BeforeSkillName == Database.VOID_EXTRACTION) ||
                         (mc.BeforeSkillName == Database.CARNAGE_RUSH) ||
                         (mc.BeforeSkillName == Database.NOTHING_OF_NOTHINGNESS))
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
                if ((mc.BeforeSkillName == Database.STRAIGHT_SMASH) ||
                    (mc.BeforeSkillName == Database.COUNTER_ATTACK) ||
                    (mc.BeforeSkillName == Database.STANCE_OF_FLOW) ||
                    (mc.BeforeSkillName == Database.STANCE_OF_STANDING) ||
                    (mc.BeforeSkillName == Database.TRUTH_VISION) ||
                    (mc.BeforeSkillName == Database.NEGATE))
                {
                    skill[0].Focus();
                    skill[0].Select();
                }
                else if ((mc.BeforeSkillName == Database.DOUBLE_SLASH) ||
                         (mc.BeforeSkillName == Database.PURE_PURIFICATION) ||
                         (mc.BeforeSkillName == Database.ENIGMA_SENSE) ||
                         (mc.BeforeSkillName == Database.INNER_INSPIRATION) ||
                         (mc.BeforeSkillName == Database.HIGH_EMOTIONALITY) ||
                         (mc.BeforeSkillName == Database.VOID_EXTRACTION))
                {
                    skill[1].Focus();
                    skill[1].Select();
                }
                else if ((mc.BeforeSkillName == Database.CRUSHING_BLOW) ||
                         (mc.BeforeSkillName == Database.ANTI_STUN) ||
                         (mc.BeforeSkillName == Database.SILENT_RUSH) ||
                         (mc.BeforeSkillName == Database.KINETIC_SMASH) ||
                         (mc.BeforeSkillName == Database.STANCE_OF_EYES) ||
                         (mc.BeforeSkillName == Database.CARNAGE_RUSH))
                {
                    skill[2].Focus();
                    skill[2].Select();
                }
                else if ((mc.BeforeSkillName == Database.SOUL_INFINITY) ||
                         (mc.BeforeSkillName == Database.STANCE_OF_DEATH) ||
                         (mc.BeforeSkillName == Database.OBORO_IMPACT) ||
                         (mc.BeforeSkillName == Database.CATASTROPHE) ||
                         (mc.BeforeSkillName == Database.PAINFUL_INSANITY) ||
                         (mc.BeforeSkillName == Database.NOTHING_OF_NOTHINGNESS))
                {
                    skill[3].Focus();
                    skill[3].Select();
                }
            }
        }

        void BattleSkillRequest_MouseEnter(object sender, EventArgs e)
        {
            UpdateCurrentTarget(sender);
        }

        void BattleSkillRequest_Leave(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.White;
        }

        void BattleSkillRequest_Enter(object sender, EventArgs e)
        {
            UpdateCurrentTarget(sender);
        }

        private void UpdateCurrentTarget(object sender)
        {
            for (int ii = 0; ii < MAX_SKILL_LIST; ii++)
            {
                if (sender.Equals(skill[ii]))
                {
                    ((Button)skill[ii]).BackColor = Color.Khaki;
                }
                else
                {
                    ((Button)skill[ii]).BackColor = Color.White;
                }
            }
            if (sender.Equals(button1)) { button1.BackColor = Color.Khaki; } else { button1.BackColor = Color.White; }
            if (sender.Equals(button2)) { button2.BackColor = Color.Khaki; } else { button2.BackColor = Color.White; }
            if (sender.Equals(button3)) { button3.BackColor = Color.Khaki; } else { button3.BackColor = Color.White; }
        }

        private PopUpMini popupInfo = null;
        void BattleSkillRequest_MouseLeave(object sender, EventArgs e)
        {
            if (popupInfo != null)
            {
                popupInfo.Close();
                popupInfo = null;
            }
            ((Button)sender).BackColor = Color.White;
        }

        void BattleSkillRequest_MouseMove(object sender, MouseEventArgs e)
        {
            if (popupInfo == null)
            {
                popupInfo = new PopUpMini();
            }

            // 動
            if (sender.Equals(skill[0]) && skill[0].Text == Database.STRAIGHT_SMASH) popupInfo.CurrentInfo = "敵対象：ダメージ";
            if (sender.Equals(skill[1]) && skill[1].Text == Database.DOUBLE_SLASH) popupInfo.CurrentInfo = "敵対象：２回攻撃";
            if (sender.Equals(skill[2]) && skill[2].Text == Database.CRUSHING_BLOW) popupInfo.CurrentInfo = "敵対象：スタン攻撃";
            if (sender.Equals(skill[3]) && skill[3].Text == Database.SOUL_INFINITY) popupInfo.CurrentInfo = "敵対象：ダメージ";
            // 静
            if (sender.Equals(skill[0]) && skill[0].Text == Database.COUNTER_ATTACK) popupInfo.CurrentInfo = "敵対象：敵の直接攻撃をカウンター";
            if (sender.Equals(skill[1]) && skill[1].Text == Database.PURE_PURIFICATION) popupInfo.CurrentInfo = "自分対象：スタン・沈黙・猛毒・誘惑・凍結・麻痺・鈍化を解除"; // 後編編集（鈍化を追記）
            if (sender.Equals(skill[2]) && skill[2].Text == Database.ANTI_STUN) popupInfo.CurrentInfo = "自分対象：スタン攻撃に対する耐性付与";
            if (sender.Equals(skill[3]) && skill[3].Text == Database.STANCE_OF_DEATH) popupInfo.CurrentInfo = "自分対象：致死ダメージで死亡回避";
            // 柔
            if (sender.Equals(skill[0]) && skill[0].Text == Database.STANCE_OF_FLOW) popupInfo.CurrentInfo = "自分対象：次の３ターン、必ず後攻";
            if (sender.Equals(skill[1]) && skill[1].Text == Database.ENIGMA_SENSE) popupInfo.CurrentInfo = "敵対象：ダメージ";
            if (sender.Equals(skill[2]) && skill[2].Text == Database.SILENT_RUSH) popupInfo.CurrentInfo = "敵対象：３回攻撃";
            if (sender.Equals(skill[3]) && skill[3].Text == Database.OBORO_IMPACT) popupInfo.CurrentInfo = "敵対象：ダメージ";
            // 剛
            if (sender.Equals(skill[0]) && skill[0].Text == Database.STANCE_OF_STANDING) popupInfo.CurrentInfo = "敵対象：防御体制のまま直接攻撃";
            if (sender.Equals(skill[1]) && skill[1].Text == Database.INNER_INSPIRATION) popupInfo.CurrentInfo = "自分対象：スキルポイントを回復";
            if (sender.Equals(skill[2]) && skill[2].Text == Database.KINETIC_SMASH) popupInfo.CurrentInfo = "敵対象：ダメージ";
            if (sender.Equals(skill[3]) && skill[3].Text == Database.CATASTROPHE) popupInfo.CurrentInfo = "敵対象：ダメージ";
            // 心眼
            if (sender.Equals(skill[0]) && skill[0].Text == Database.TRUTH_VISION) popupInfo.CurrentInfo = "自分対象：敵パラメタＵＰを無視";
            if (sender.Equals(skill[1]) && skill[1].Text == Database.HIGH_EMOTIONALITY) popupInfo.CurrentInfo = "自分対象：次の３ターン、力・技・知・心パラメタＵＰ";
            if (sender.Equals(skill[2]) && skill[2].Text == Database.STANCE_OF_EYES) popupInfo.CurrentInfo = "敵対象：本ターン、相手の行動をキャンセル";
            if (sender.Equals(skill[3]) && skill[3].Text == Database.PAINFUL_INSANITY) popupInfo.CurrentInfo = "自分対象：毎ターン、心値による永続ダメージ";
            // 無心
            if (sender.Equals(skill[0]) && skill[0].Text == Database.NEGATE) popupInfo.CurrentInfo = "敵対象：本ターン、相手のスペル詠唱をキャンセル";
            if (sender.Equals(skill[1]) && skill[1].Text == Database.VOID_EXTRACTION) popupInfo.CurrentInfo = "自分対象：力、技、知、心のうち、最も高いパラメタを２倍";
            if (sender.Equals(skill[2]) && skill[2].Text == Database.CARNAGE_RUSH) popupInfo.CurrentInfo = "敵対象：５回攻撃";
            if (sender.Equals(skill[3]) && skill[3].Text == Database.NOTHING_OF_NOTHINGNESS) popupInfo.CurrentInfo = "自分対象：" + Database.DISPEL_MAGIC + "、" + Database.TRANQUILITY + "を無効化。\n" + Database.NEGATE + "、" + Database.COUNTER_ATTACK + "、" + Database.STANCE_OF_EYES + "を無効化";

            popupInfo.StartPosition = FormStartPosition.Manual;
            popupInfo.Location = new Point(this.Location.X + ((Button)sender).Location.X + e.X + 10, this.Location.Y + ((Button)sender).Location.Y + e.Y + 0);
            popupInfo.PopupColor = Color.Black;
            //popupInfo.PopupTextColor = Brushes.White; // 後編削除
            popupInfo.Show();
        }

        void skill_Click(object sender, EventArgs e)
        {
            this.currentSkillName = ((Button)sender).Text;
            DialogResult = DialogResult.OK;
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
            for (int ii = 0; ii < MAX_SKILL_LIST; ii++)
            {
                label1.Text = "";
                skill[ii].Text = "";
                skill[ii].Visible = true;
                skillpt[ii].Text = "";
                skillpt[ii].Visible = true;
            }

            string pt = "pt";
            switch (this.availableList[currentNumber])
            {
                case 0:
                    label1.Text = "動 (Active)";
                    if (mc.StraightSmash) { skill[0].Text = Database.STRAIGHT_SMASH; skillpt[0].Text = Database.STRAIGHT_SMASH_COST.ToString() + pt; } else { skill[0].Visible = false; skillpt[0].Visible = false; }
                    if (mc.DoubleSlash)   { skill[1].Text = Database.DOUBLE_SLASH;   skillpt[1].Text = Database.DOUBLE_SLASH_COST.ToString()   + pt; } else { skill[1].Visible = false; skillpt[1].Visible = false; }
                    if (mc.CrushingBlow)  { skill[2].Text = Database.CRUSHING_BLOW;  skillpt[2].Text = Database.CRUSHING_BLOW_COST.ToString()  + pt; } else { skill[2].Visible = false; skillpt[2].Visible = false; }
                    if (mc.SoulInfinity)  { skill[3].Text = Database.SOUL_INFINITY;  skillpt[3].Text = Database.SOUL_INFINITY_COST.ToString()  + pt; } else { skill[3].Visible = false; skillpt[3].Visible = false; }
                    break;

                case 1:
                    label1.Text = "静 (Passive)";
                    if (mc.CounterAttack)    { skill[0].Text = Database.COUNTER_ATTACK;    skillpt[0].Text = Database.COUNTER_ATTACK_COST.ToString()    + pt; } else { skill[0].Visible = false; skillpt[0].Visible = false; }
                    if (mc.PurePurification) { skill[1].Text = Database.PURE_PURIFICATION; skillpt[1].Text = Database.PURE_PURIFICATION_COST.ToString() + pt; } else { skill[1].Visible = false; skillpt[1].Visible = false; }
                    if (mc.AntiStun)         { skill[2].Text = Database.ANTI_STUN;         skillpt[2].Text = Database.ANTI_STUN_COST.ToString()         + pt; } else { skill[2].Visible = false; skillpt[2].Visible = false; }
                    if (mc.StanceOfDeath)    { skill[3].Text = Database.STANCE_OF_DEATH;   skillpt[3].Text = Database.STANCE_OF_DEATH_COST.ToString()   + pt; } else { skill[3].Visible = false; skillpt[3].Visible = false; }
                    break;

                case 2:
                    label1.Text = "柔 (Soft)";
                    if (mc.StanceOfFlow) { skill[0].Text = Database.STANCE_OF_FLOW; skillpt[0].Text = Database.STANCE_OF_FLOW_COST.ToString() + pt; } else { skill[0].Visible = false; skillpt[0].Visible = false; }
                    if (mc.EnigmaSence)  { skill[1].Text = Database.ENIGMA_SENSE;   skillpt[1].Text = Database.ENIGMA_SENSE_COST.ToString()   + pt; } else { skill[1].Visible = false; skillpt[1].Visible = false; }
                    if (mc.SilentRush)   { skill[2].Text = Database.SILENT_RUSH;    skillpt[2].Text = Database.SILENT_RUSH_COST.ToString()    + pt; } else { skill[2].Visible = false; skillpt[2].Visible = false; }
                    if (mc.OboroImpact)  { skill[3].Text = Database.OBORO_IMPACT;   skillpt[3].Text = Database.OBORO_IMPACT_COST.ToString()   + pt; } else { skill[3].Visible = false; skillpt[3].Visible = false; }
                    break;
                
                case 3:
                    label1.Text = "剛 (Hard)";
                    if (mc.StanceOfStanding) { skill[0].Text = Database.STANCE_OF_STANDING; skillpt[0].Text = Database.STANCE_OF_STANDING_COST.ToString() + pt; } else { skill[0].Visible = false; skillpt[0].Visible = false; }
                    if (mc.InnerInspiration) { skill[1].Text = Database.INNER_INSPIRATION;  skillpt[1].Text = Database.INNER_INSPIRATION_COST.ToString()  + pt; } else { skill[1].Visible = false; skillpt[1].Visible = false; }
                    if (mc.KineticSmash)     { skill[2].Text = Database.KINETIC_SMASH;      skillpt[2].Text = Database.KINETIC_SMASH_COST.ToString()      + pt; } else { skill[2].Visible = false; skillpt[2].Visible = false; }
                    if (mc.Catastrophe)      { skill[3].Text = Database.CATASTROPHE;        skillpt[3].Text = "XXX" /*Database.CATASTROPHE_COST.ToString()*/        + pt; } else { skill[3].Visible = false; skillpt[3].Visible = false; }
                    break;
                
                case 4:
                    label1.Text = "心眼 (Truth)";
                    if (mc.TruthVision)      { skill[0].Text = Database.TRUTH_VISION;      skillpt[0].Text = Database.TRUTH_VISION_COST.ToString()      + pt; } else { skill[0].Visible = false; skillpt[0].Visible = false; }
                    if (mc.HighEmotionality) { skill[1].Text = Database.HIGH_EMOTIONALITY; skillpt[1].Text = Database.HIGH_EMOTIONALITY_COST.ToString() + pt; } else { skill[1].Visible = false; skillpt[1].Visible = false; }
                    if (mc.StanceOfEyes)     { skill[2].Text = Database.STANCE_OF_EYES;    skillpt[2].Text = Database.STANCE_OF_EYES_COST.ToString()    + pt; } else { skill[2].Visible = false; skillpt[2].Visible = false; }
                    if (mc.PainfulInsanity)  { skill[3].Text = Database.PAINFUL_INSANITY;  skillpt[3].Text = Database.PAINFUL_INSANITY_COST.ToString()  + pt; } else { skill[3].Visible = false; skillpt[3].Visible = false; }
                    break;
                
                case 5:
                    label1.Text = "無心 (Void)";
                    if (mc.Negate)               { skill[0].Text = Database.NEGATE;                 skillpt[0].Text = Database.NEGATE_COST.ToString()                 + pt; } else { skill[0].Visible = false; skillpt[0].Visible = false; }
                    if (mc.VoidExtraction)       { skill[1].Text = Database.VOID_EXTRACTION;        skillpt[1].Text = Database.VOID_EXTRACTION_COST.ToString()        + pt; } else { skill[1].Visible = false; skillpt[1].Visible = false; }
                    if (mc.CarnageRush)          { skill[2].Text = Database.CARNAGE_RUSH;           skillpt[2].Text = Database.CARNAGE_RUSH_COST.ToString()           + pt; } else { skill[2].Visible = false; skillpt[2].Visible = false; }
                    if (mc.NothingOfNothingness) { skill[3].Text = Database.NOTHING_OF_NOTHINGNESS; skillpt[3].Text = Database.NOTHING_OF_NOTHINGNESS_COST.ToString() + pt; } else { skill[3].Visible = false; skillpt[3].Visible = false; }
                    break;

            }
        }
    }
}