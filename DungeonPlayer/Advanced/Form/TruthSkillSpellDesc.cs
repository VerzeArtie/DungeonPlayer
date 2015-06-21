using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class TruthSkillSpellDesc : Form
    {
        public string SkillSpellName { get; set; }
        public MainCharacter Player { get; set; }

        public TruthSkillSpellDesc()
        {
            InitializeComponent();
            this.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "BackgroundActivateSkill.png");
            this.pbWeapon.Image = Image.FromFile(Database.BaseResourceFolder + "WeaponMark.bmp");
            this.pbStrength.Image = Image.FromFile(Database.BaseResourceFolder + "StrengthMark.bmp");
            this.pbAgility.Image = Image.FromFile(Database.BaseResourceFolder + "AgilityMark.bmp");
            this.pbIntelligence.Image = Image.FromFile(Database.BaseResourceFolder + "IntelligenceMark.bmp");
            this.pbStamina.Image = Image.FromFile(Database.BaseResourceFolder + "StaminaMark.bmp");
            this.pbMind.Image = Image.FromFile(Database.BaseResourceFolder + "MindMark.bmp");

        }

        private void TruthSkillSpellDesc_Load(object sender, EventArgs e)
        {
            this.labelTitle.Text = SkillSpellName;
            this.label5.Text = TruthActionCommand.ConvertToJapanese(SkillSpellName);
            string ext = ".bmp";
            pictureBox1.Image = Image.FromFile(Database.BaseResourceFolder + SkillSpellName + ext);
            label4.Text = TruthActionCommand.GetCost(SkillSpellName).ToString();
            switch (TruthActionCommand.GetTargetType(SkillSpellName))
            {
                case TruthActionCommand.TargetType.AllMember:
                    label8.Text = "敵味方\r\n全体";
                    break;
                case TruthActionCommand.TargetType.Ally:
                    label8.Text = "味方単体";
                    break;
                case TruthActionCommand.TargetType.AllyGroup:
                    label8.Text = "味方全体";
                    break;
                case TruthActionCommand.TargetType.AllyOrEnemy:
                    label8.Text = "敵単体\r\n味方単体";
                    break;
                case TruthActionCommand.TargetType.Enemy:
                    label8.Text = "敵単体";
                    break;
                case TruthActionCommand.TargetType.EnemyGroup:
                    label8.Text = "敵全体";
                    break;
                case TruthActionCommand.TargetType.InstantTarget:
                    label8.Text = "インスタント対象";
                    break;
                case TruthActionCommand.TargetType.NoTarget:
                    label8.Text = "なし";
                    break;
                case TruthActionCommand.TargetType.Own:
                    label8.Text = "自分";
                    break;
            }

            // 内在　Immanence Spirit
            // 外在  
            string ARCHTYPE_PHISICAL = "開放型";
            string ARCHTYPE_INNER_MENTAL = "内在型";

            string SKILL_ATTRIBUTE_TEXT = "スキル属性";
            string SPELL_ATTRIBUTE_TEXT = "スペル属性";

            string ATTRIBUTE_LIGHT = "--- 聖 ---";
            string ATTRIBUTE_SHADOW = "--- 闇 ---";
            string ATTRIBUTE_FIRE = "--- 火 ---";
            string ATTRIBUTE_ICE = "--- 水 ---";
            string ATTRIBUTE_FORCE = "--- 理 ---";
            string ATTRIBUTE_WILL = "--- 空 ---";

            string ATTRIBUTE_LIGHT_SHADOW = " --- 聖/闇 ---";
            string ATTRIBUTE_LIGHT_FIRE = " --- 聖/火 ---";
            string ATTRIBUTE_LIGHT_ICE = " --- 聖/水 ---";
            string ATTRIBUTE_LIGHT_FORCE = " --- 聖/理 ---";
            string ATTRIBUTE_LIGHT_WILL = " --- 聖/空 ---";
            string ATTRIBUTE_SHADOW_FIRE = " --- 闇/火 ---";
            string ATTRIBUTE_SHADOW_ICE = " --- 闇/水 ---";
            string ATTRIBUTE_SHADOW_FORCE = " --- 闇/理 ---";
            string ATTRIBUTE_SHADOW_WILL = " --- 闇/空 ---";
            string ATTRIBUTE_FIRE_ICE = " --- 火/水 ---";
            string ATTRIBUTE_FIRE_FORCE = " --- 火/理 ---";
            string ATTRIBUTE_FIRE_WILL = " --- 火/空 ---";
            string ATTRIBUTE_ICE_FORCE = " --- 水/理 ---";
            string ATTRIBUTE_ICE_WILL = " --- 水/空 ---";
            string ATTRIBUTE_FORCE_WILL = " --- 理/空 ---";

            string ATTRIBUTE_ACTIVE = "--- 動 ---";
            string ATTRIBUTE_PASSIVE = "--- 静 ---";
            string ATTRIBUTE_SOFT = "--- 柔 ---";
            string ATTRIBUTE_HARD = "--- 剛 ---";
            string ATTRIBUTE_TRUTH = "--- 心眼 ---";
            string ATTRIBUTE_VOID = "--- 無心 ---";

            string ATTRIBUTE_ACTIVE_PASSIVE = "--- 動/静 ---";
            string ATTRIBUTE_ACTIVE_SOFT = "--- 動/柔 ---";
            string ATTRIBUTE_ACTIVE_HARD = "--- 動/剛 ---";
            string ATTRIBUTE_ACTIVE_TRUTH = "--- 動/心眼 ---";
            string ATTRIBUTE_ACTIVE_VOID = "--- 動/無心 ---";
            string ATTRIBUTE_PASSIVE_SOFT = "--- 静/柔 ---";
            string ATTRIBUTE_PASSIVE_HARD = "--- 静/剛 ---";
            string ATTRIBUTE_PASSIVE_TRUTH = "--- 静/心眼 ---";
            string ATTRIBUTE_PASSIVE_VOID = "--- 静/無心 ---";
            string ATTRIBUTE_SOFT_HARD = "--- 柔/剛 ---";
            string ATTRIBUTE_SOFT_TRUTH = "--- 柔/心眼 ---";
            string ATTRIBUTE_SOFT_VOID = "--- 柔/無心 ---";
            string ATTRIBUTE_HARD_TRUTH = "--- 剛/心眼 ---";
            string ATTRIBUTE_HARD_VOID = "--- 剛/無心 ---";
            string ATTRIBUTE_TRUTH_VOID = "--- 心眼/無心 ---";

            string ATTRIBUTE_NONE = "--- 無属性 ---";

            labelTitle.Text = this.SkillSpellName;
            if (TruthActionCommand.GetTimingType(this.SkillSpellName) == TruthActionCommand.TimingType.Instant)
            {
                labelTiming.Text = "インスタント";
            }
            else if (TruthActionCommand.GetTimingType(this.SkillSpellName) == TruthActionCommand.TimingType.Sorcery)
            {
                labelTiming.Text = "ソーサリー";
            }
            labelDescription.Text = TruthActionCommand.GetDescription(this.SkillSpellName);
            if (TruthActionCommand.CheckPlayerActionFromString(this.SkillSpellName) == PlayerAction.UseSpell)
            {
                labelAttributeTitle.Text = SPELL_ATTRIBUTE_TEXT;
                switch (TruthActionCommand.GetMagicType(this.SkillSpellName))
                {
                    case TruthActionCommand.MagicType.Light:
                        labelAttribute.Text = ATTRIBUTE_LIGHT;
                        break;
                    case TruthActionCommand.MagicType.Shadow:
                        labelAttribute.Text = ATTRIBUTE_SHADOW;
                        break;
                    case TruthActionCommand.MagicType.Fire:
                        labelAttribute.Text = ATTRIBUTE_FIRE;
                        break;
                    case TruthActionCommand.MagicType.Ice:
                        labelAttribute.Text = ATTRIBUTE_ICE;
                        break;
                    case TruthActionCommand.MagicType.Force:
                        labelAttribute.Text = ATTRIBUTE_FORCE;
                        break;
                    case TruthActionCommand.MagicType.Will:
                        labelAttribute.Text = ATTRIBUTE_WILL;
                        break;
                    case TruthActionCommand.MagicType.Light_Shadow:
                        labelAttribute.Text = ATTRIBUTE_LIGHT_SHADOW;
                        break;
                    case TruthActionCommand.MagicType.Light_Fire:
                        labelAttribute.Text = ATTRIBUTE_LIGHT_FIRE;
                        break;
                    case TruthActionCommand.MagicType.Light_Ice:
                        labelAttribute.Text = ATTRIBUTE_LIGHT_ICE;
                        break;
                    case TruthActionCommand.MagicType.Light_Force:
                        labelAttribute.Text = ATTRIBUTE_LIGHT_FORCE;
                        break;
                    case TruthActionCommand.MagicType.Light_Will:
                        labelAttribute.Text = ATTRIBUTE_LIGHT_WILL;
                        break;
                    case TruthActionCommand.MagicType.Shadow_Fire:
                        labelAttribute.Text = ATTRIBUTE_SHADOW_FIRE;
                        break;
                    case TruthActionCommand.MagicType.Shadow_Ice:
                        labelAttribute.Text = ATTRIBUTE_SHADOW_ICE;
                        break;
                    case TruthActionCommand.MagicType.Shadow_Force:
                        labelAttribute.Text = ATTRIBUTE_SHADOW_FORCE;
                        break;
                    case TruthActionCommand.MagicType.Shadow_Will:
                        labelAttribute.Text = ATTRIBUTE_SHADOW_WILL;
                        break;
                    case TruthActionCommand.MagicType.Fire_Ice:
                        labelAttribute.Text = ATTRIBUTE_FIRE_ICE;
                        break;
                    case TruthActionCommand.MagicType.Fire_Force:
                        labelAttribute.Text = ATTRIBUTE_FIRE_FORCE;
                        break;
                    case TruthActionCommand.MagicType.Fire_Will:
                        labelAttribute.Text = ATTRIBUTE_FIRE_WILL;
                        break;
                    case TruthActionCommand.MagicType.Ice_Force:
                        labelAttribute.Text = ATTRIBUTE_ICE_FORCE;
                        break;
                    case TruthActionCommand.MagicType.Ice_Will:
                        labelAttribute.Text = ATTRIBUTE_ICE_WILL;
                        break;
                    case TruthActionCommand.MagicType.Force_Will:
                        labelAttribute.Text = ATTRIBUTE_FORCE_WILL;
                        break;
                    default:
                        labelAttribute.Text = ATTRIBUTE_NONE;
                        break;
                }
            }
            else
            {
                labelAttributeTitle.Text = SKILL_ATTRIBUTE_TEXT;
                switch (TruthActionCommand.GetSkillType(this.SkillSpellName))
                {
                    case TruthActionCommand.SkillType.Active:
                        labelAttribute.Text = ATTRIBUTE_ACTIVE;
                        break;
                    case TruthActionCommand.SkillType.Passive:
                        labelAttribute.Text = ATTRIBUTE_PASSIVE;
                        break;
                    case TruthActionCommand.SkillType.Soft:
                        labelAttribute.Text = ATTRIBUTE_SOFT;
                        break;
                    case TruthActionCommand.SkillType.Hard:
                        labelAttribute.Text = ATTRIBUTE_HARD;
                        break;
                    case TruthActionCommand.SkillType.Truth:
                        labelAttribute.Text = ATTRIBUTE_TRUTH;
                        break;
                    case TruthActionCommand.SkillType.Void:
                        labelAttribute.Text = ATTRIBUTE_VOID;
                        break;
                    case TruthActionCommand.SkillType.Active_Passive:
                        labelAttribute.Text = ATTRIBUTE_ACTIVE_PASSIVE;
                        break;
                    case TruthActionCommand.SkillType.Active_Soft:
                        labelAttribute.Text = ATTRIBUTE_ACTIVE_SOFT;
                        break;
                    case TruthActionCommand.SkillType.Active_Hard:
                        labelAttribute.Text = ATTRIBUTE_ACTIVE_HARD;
                        break;
                    case TruthActionCommand.SkillType.Active_Truth:
                        labelAttribute.Text = ATTRIBUTE_ACTIVE_TRUTH;
                        break;
                    case TruthActionCommand.SkillType.Active_Void:
                        labelAttribute.Text = ATTRIBUTE_ACTIVE_VOID;
                        break;
                    case TruthActionCommand.SkillType.Passive_Soft:
                        labelAttribute.Text = ATTRIBUTE_PASSIVE_SOFT;
                        break;
                    case TruthActionCommand.SkillType.Passive_Hard:
                        labelAttribute.Text = ATTRIBUTE_PASSIVE_HARD;
                        break;
                    case TruthActionCommand.SkillType.Passive_Truth:
                        labelAttribute.Text = ATTRIBUTE_PASSIVE_TRUTH;
                        break;
                    case TruthActionCommand.SkillType.Passive_Void:
                        labelAttribute.Text = ATTRIBUTE_PASSIVE_VOID;
                        break;
                    case TruthActionCommand.SkillType.Soft_Hard:
                        labelAttribute.Text = ATTRIBUTE_SOFT_HARD;
                        break;
                    case TruthActionCommand.SkillType.Soft_Truth:
                        labelAttribute.Text = ATTRIBUTE_SOFT_TRUTH;
                        break;
                    case TruthActionCommand.SkillType.Soft_Void:
                        labelAttribute.Text = ATTRIBUTE_SOFT_VOID;
                        break;
                    case TruthActionCommand.SkillType.Hard_Truth:
                        labelAttribute.Text = ATTRIBUTE_HARD_TRUTH;
                        break;
                    case TruthActionCommand.SkillType.Hard_Void:
                        labelAttribute.Text = ATTRIBUTE_HARD_VOID;
                        break;
                    case TruthActionCommand.SkillType.Truth_Void:
                        labelAttribute.Text = ATTRIBUTE_TRUTH_VOID;
                        break;
                    default:
                        labelAttribute.Text = ATTRIBUTE_NONE;
                        break;
                }
            }
            button2.Text = Player.Name + "は" + TruthActionCommand.ConvertToJapanese(this.SkillSpellName) + "を習得した";

            if (TruthActionCommand.IsDamage(this.SkillSpellName))
            {
                if (TruthActionCommand.CheckPlayerActionFromString(this.SkillSpellName) == PlayerAction.UseSpell)
                {
                    if (this.SkillSpellName == Database.WORD_OF_POWER)
                    {
                        pbStrength.Visible = true;
                    }
                    else
                    {
                        pbIntelligence.Visible = true;
                    }
                }
                else
                {
                    if (this.SkillSpellName == Database.PSYCHIC_WAVE)
                    {
                        pbIntelligence.Visible = true;
                    }
                    else
                    {
                        pbStrength.Visible = true;
                    }
                }
            }

            switch (this.SkillSpellName)
            {
                // アイン
                case Database.STRAIGHT_SMASH:
                    pbAgility.Visible = true;
                    break;
                case Database.INNER_INSPIRATION:
                    pbMind.Visible = true;
                    break;
                case Database.WORD_OF_LIFE:
                    pbMind.Visible = true;
                    break;

                // ラナ
                case Database.ENIGMA_SENSE:
                    pbAgility.Visible = true;
                    pbIntelligence.Visible = true;
                    break;
                case Database.BLACK_CONTRACT:
                    pbMind.Visible = true;
                    break;

                // ランディス
                case Database.SMOOTHING_MOVE:
                    //pbAgility.Visible = true;
                    break;

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

    }
}
