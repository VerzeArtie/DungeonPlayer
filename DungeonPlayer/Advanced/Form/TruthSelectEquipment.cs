using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace DungeonPlayer
{
    public partial class TruthSelectEquipment : MotherForm
    {
        public int EquipType { get; set; } // equipType: 0:Weapon  1:SubWeapon  2:Armor  3:Accessory  4:Accessory2
        public MainCharacter Player { get; set; }
        private MainCharacter shadow = new MainCharacter();
        public string SelectValue { get; set; }
        public Button[] btn = new Button[36];
        public TruthSelectEquipment()
        {
            InitializeComponent();
            for (int ii = 0; ii < 36; ii++)
            {
                btn[ii] = new Button();
            }

            //btn[0] = target1;
            //btn[1] = target2;
            //btn[2] = target3;
            //btn[3] = target4;
            //btn[4] = target5;
            //btn[5] = target6;
            //btn[6] = target7;
            //btn[7] = target8;
            //btn[8] = target9;
            //btn[9] = target10;
        }

        private void target1_Click(object sender, EventArgs e)
        {
            string target = ((Button)sender).Text;
            if (target != null)
            {
                if ((target != string.Empty) ||
                    (target != ""))
                {
                    this.SelectValue = ((Button)sender).Text;
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
            }
        }

        private void TruthSelectEquipment_Load(object sender, EventArgs e)
        {
            this.basePhysicalLocation = this.PhysicalAttack.Location;
            this.basePhysicalLocationBar = this.PhysicalAttackBar.Location;
            this.basePhysicalLocationMax = this.PhysicalAttackMax.Location;
            this.basePhysicalLocation2 = this.PhysicalAttack2.Location;
            this.basePhysicalLocation2Bar = this.PhysicalAttack2Bar.Location;
            this.basePhysicalLocation2Max = this.PhysicalAttack2Max.Location;

            shadow.MainWeapon = this.Player.MainWeapon;
            shadow.SubWeapon = this.Player.SubWeapon;
            shadow.MainArmor = this.Player.MainArmor;
            shadow.Accessory = this.Player.Accessory;
            shadow.Accessory2 = this.Player.Accessory2;

            // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
            Type type = this.Player.GetType();
            foreach (PropertyInfo pi in type.GetProperties())
            {
                if (pi.PropertyType == typeof(System.Int32))
                {
                    try
                    {
                        pi.SetValue(shadow, (System.Int32)(type.GetProperty(pi.Name).GetValue(this.Player, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.String))
                {
                    try
                    {
                        pi.SetValue(shadow, (string)(type.GetProperty(pi.Name).GetValue(this.Player, null)), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.Boolean))
                {
                    try
                    {
                        pi.SetValue(shadow, (System.Boolean)(type.GetProperty(pi.Name).GetValue(this.Player, null)), null);
                    }
                    catch { }
                }
                // s 後編追加
                else if (pi.PropertyType == typeof(PlayerStance))
                {
                    try
                    {
                        pi.SetValue(shadow, (PlayerStance)(Enum.Parse(typeof(PlayerStance), type.GetProperty(pi.Name).GetValue(this.Player, null).ToString())), null);
                    }
                    catch { }
                }
                // e 後編追加
                // s 後編追加
                else if (pi.PropertyType == typeof(AdditionalSpellType))
                {
                    try
                    {
                        pi.SetValue(shadow, (AdditionalSpellType)(Enum.Parse(typeof(AdditionalSpellType), type.GetProperty(pi.Name).GetValue(this.Player, null).ToString())), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(AdditionalSkillType))
                {
                    try
                    {
                        pi.SetValue(shadow, (AdditionalSkillType)(Enum.Parse(typeof(AdditionalSkillType), type.GetProperty(pi.Name).GetValue(this.Player, null).ToString())), null);
                    }
                    catch { }
                }
                // e 後編追加
            }

            //this.BackColor = currentStatusView;
            //if (this.BackColor == Color.LightSkyBlue)
            //{
            //    if (mc != null)
            //    {
            //        //SettingCharacterData(mc);
            //        RefreshPartyMembersBattleStatus(mc);
            //    }
            //}
            //else if (this.BackColor == Color.Pink)
            //{
            //    if (sc != null)
            //    {
            //        //SettingCharacterData(sc);
            //        RefreshPartyMembersBattleStatus(sc);
            //    }
            //}
            //else if (this.BackColor == Color.Gold)
            //{
            //    if (tc != null)
            //    {
            //        //SettingCharacterData(tc);
            //        RefreshPartyMembersBattleStatus(tc);
            //    }
            //}
            RefreshPartyMembersBattleStatus(Player);

            RefreshPartyMemberBaseParameter(Player);

            // RefreshPartyMembersLife();
            //this.life.Text = player.CurrentLife.ToString() + " / " + player.MaxLife.ToString();

            //if (player.AvailableSkill)
            //{
            //    label24.Visible = true;
            //    skill.Visible = true;
            //    skill.Text = player.CurrentSkillPoint.ToString() + " / " + player.MaxSkillPoint.ToString();
            //}
            //else
            //{
            //    label24.Visible = false;
            //    skill.Visible = false;
            //}


            //if (chara.AvailableMana)
            //{
            //    label25.Visible = true;
            //    mana.Visible = true;
            //    mana.Text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();
            //}
            //else
            //{
            //    label25.Visible = false;
            //    mana.Visible = false;
            //}
            //spell1.Visible = chara.FreshHeal;

            //this.weapon.Text = "";
            //this.subWeapon.Text = "";
            //this.armor.Text = "";
            //this.accessory.Text = "";
            //if (player.MainWeapon != null)
            //{
            //    this.weapon.Text = chara.MainWeapon.Name;
            //    UpdateLabelColorForRare(ref this.weapon, player.MainWeapon.Rare);
            //}
            //if (player.SubWeapon != null)
            //{
            //    this.subWeapon.Text = chara.SubWeapon.Name;
            //    UpdateLabelColorForRare(ref this.subWeapon, player.SubWeapon.Rare);
            //}
            //if (player.MainArmor != null)
            //{
            //    this.armor.Text = chara.MainArmor.Name;
            //    UpdateLabelColorForRare(ref this.armor, player.MainArmor.Rare);
            //}
            //if (player.Accessory != null)
            //{
            //    this.accessory.Text = chara.Accessory.Name;
            //    UpdateLabelColorForRare(ref this.accessory, player.Accessory.Rare);
            //}
            //if (player.Accessory2 != null)
            //{
            //    this.accessory2.Text = chara.Accessory2.Name;
            //    UpdateLabelColorForRare(ref this.accessory2, player.Accessory2.Rare);
            //}
            //UpdateBackPackLabel(chara);
            //UpdateSpellSkillLabel(chara);


            PageNumber1_Click(PageNumber1, null);

            if (this.btn[9].Text == String.Empty)
            {
                PageNumber2.Visible = false;
                PageNumber3.Visible = false;
                PageNumber4.Visible = false;
            }
            if (this.btn[18].Text == String.Empty)
            {
                PageNumber3.Visible = false;
                PageNumber4.Visible = false;
            }
            if (this.btn[27].Text == String.Empty)
            {
                PageNumber4.Visible = false;
            }

        }

        private void RefreshPartyMemberBaseParameter(MainCharacter player)
        {
            this.strength.Text = player.Strength.ToString();
            this.addStrength.Text = " + " + player.BuffStrength_Accessory.ToString();

            this.agility.Text = player.Agility.ToString();
            this.addAgility.Text = " + " + player.BuffAgility_Accessory.ToString();

            this.intelligence.Text = player.Intelligence.ToString();
            this.addIntelligence.Text = " + " + player.BuffIntelligence_Accessory.ToString();

            this.stamina.Text = player.Stamina.ToString();
            this.addStamina.Text = " + " + player.BuffStamina_Accessory.ToString();

            this.mindLabel.Text = player.Mind.ToString();
            this.addMind.Text = " + " + player.BuffMind_Accessory.ToString();

            Color downColor = Color.Red;
            Color upColor = Color.Blue;
            Color normalColor = Color.Black;
            if (shadow.BuffStrength_Accessory > this.Player.BuffStrength_Accessory) this.addStrength.ForeColor = upColor;
            else if (shadow.BuffStrength_Accessory < this.Player.BuffStrength_Accessory) this.addStrength.ForeColor = downColor;
            else this.addStrength.ForeColor = normalColor;

            if (shadow.BuffAgility_Accessory > this.Player.BuffAgility_Accessory) this.addAgility.ForeColor = upColor;
            else if (shadow.BuffAgility_Accessory < this.Player.BuffAgility_Accessory) this.addAgility.ForeColor = downColor;
            else this.addAgility.ForeColor = normalColor;

            if (shadow.BuffIntelligence_Accessory > this.Player.BuffIntelligence_Accessory) this.addIntelligence.ForeColor = upColor;
            else if (shadow.BuffIntelligence_Accessory < this.Player.BuffIntelligence_Accessory) this.addIntelligence.ForeColor = downColor;
            else this.addIntelligence.ForeColor = normalColor;

            if (shadow.BuffStamina_Accessory > this.Player.BuffStamina_Accessory) this.addStamina.ForeColor = upColor;
            else if (shadow.BuffStamina_Accessory < this.Player.BuffStamina_Accessory) this.addStamina.ForeColor = downColor;
            else this.addStamina.ForeColor = normalColor;

            if (shadow.BuffMind_Accessory > this.Player.BuffMind_Accessory) this.addMind.ForeColor = upColor;
            else if (shadow.BuffMind_Accessory < this.Player.BuffMind_Accessory) this.addMind.ForeColor = downColor;
            else this.addMind.ForeColor = normalColor;
        }

        Point basePhysicalLocation;
        Point basePhysicalLocationBar;
        Point basePhysicalLocationMax;
        Point basePhysicalLocation2;
        Point basePhysicalLocation2Bar;
        Point basePhysicalLocation2Max;
        private void RefreshPartyMembersBattleStatus(MainCharacter player)
        {
            bool MainBlade = false;
            bool SubBlade = false;
            bool DoubleBlade = false;
            double temp1 = 0;
            double temp2 = 0;

            if (shadow.MainWeapon != null)
            {
                if ((shadow.MainWeapon.Type == ItemBackPack.ItemType.Weapon_Heavy) ||
                    (shadow.MainWeapon.Type == ItemBackPack.ItemType.Weapon_Light) ||
                    (shadow.MainWeapon.Type == ItemBackPack.ItemType.Weapon_Middle))
                {
                    MainBlade = true;
                    if (shadow.SubWeapon != null)
                    {
                        if ((shadow.SubWeapon.Type == ItemBackPack.ItemType.Weapon_Heavy) ||
                            (shadow.SubWeapon.Type == ItemBackPack.ItemType.Weapon_Light) ||
                            (shadow.SubWeapon.Type == ItemBackPack.ItemType.Weapon_Middle))
                        {
                            SubBlade = true;
                            DoubleBlade = true;
                        }
                    }

                }
                if ((shadow.MainWeapon.Type == ItemBackPack.ItemType.Weapon_Rod) ||
                    (shadow.MainWeapon.Type == ItemBackPack.ItemType.Weapon_TwoHand))
                {
                    MainBlade = true;
                    SubBlade = false;
                }
                if (shadow.MainWeapon.Name == "") // メイン武器が無い場合も含む。
                {
                    MainBlade = true;
                    SubBlade = false;
                }
            }
            else
            {
                if (shadow.SubWeapon != null)
                {
                    if ((shadow.SubWeapon.Type == ItemBackPack.ItemType.Weapon_Heavy) ||
                        (shadow.SubWeapon.Type == ItemBackPack.ItemType.Weapon_Light) ||
                        (shadow.SubWeapon.Type == ItemBackPack.ItemType.Weapon_Middle))
                    {
                        SubBlade = true;
                    }
                }
            }

            temp1 = PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Min, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false);
            temp2 = PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Max, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false);
            PhysicalAttack.Text = temp1.ToString("F2");
            PhysicalAttackMax.Text = temp2.ToString("F2");

            if (MainBlade == false && SubBlade == true)
            {
                temp1 = PrimaryLogic.SubAttackValue(player, PrimaryLogic.NeedType.Min, 1.0f, 0, 0, 0, 1.0f, PlayerStance.None, false);
                temp2 = PrimaryLogic.SubAttackValue(player, PrimaryLogic.NeedType.Max, 1.0f, 0, 0, 0, 1.0f, PlayerStance.None, false);
                PhysicalAttack.Text = temp1.ToString("F2");
                PhysicalAttackMax.Text = temp2.ToString("F2");
            }

            if (DoubleBlade)
            {
                // ２刀流の場合
                temp1 = PrimaryLogic.SubAttackValue(player, PrimaryLogic.NeedType.Min, 1.0f, 0, 0, 0, 1.0f, PlayerStance.None, false);
                temp2 = PrimaryLogic.SubAttackValue(player, PrimaryLogic.NeedType.Max, 1.0f, 0, 0, 0, 1.0f, PlayerStance.None, false);
                PhysicalAttack2.Text = temp1.ToString("F2");
                PhysicalAttack2Max.Text = temp2.ToString("F2");

                PhysicalAttack.Location = new Point(this.basePhysicalLocation.X, this.basePhysicalLocation.Y);
                PhysicalAttackBar.Location = new Point(this.basePhysicalLocationBar.X, this.basePhysicalLocationBar.Y);
                PhysicalAttackMax.Location = new Point(this.basePhysicalLocationMax.X, this.basePhysicalLocationMax.Y);
                PhysicalAttack2.Visible = true;
                PhysicalAttack2Bar.Visible = true;
                PhysicalAttack2Max.Visible = true;
            }
            else
            {
                // １刀流の場合
                PhysicalAttack.Location = new Point(this.basePhysicalLocation.X, this.basePhysicalLocation.Y + 10);
                PhysicalAttackBar.Location = new Point(this.basePhysicalLocationBar.X, this.basePhysicalLocationBar.Y + 10);
                PhysicalAttackMax.Location = new Point(this.basePhysicalLocationMax.X, this.basePhysicalLocationMax.Y + 10);
                PhysicalAttack2.Visible = false;
                PhysicalAttack2Bar.Visible = false;
                PhysicalAttack2Max.Visible = false;
            }

            temp1 = PrimaryLogic.PhysicalDefenseValue(player, PrimaryLogic.NeedType.Min, false);
            temp2 = PrimaryLogic.PhysicalDefenseValue(player, PrimaryLogic.NeedType.Max, false);
            PhysicalDefense.Text = temp1.ToString("F2");
            PhysicalDefenseMax.Text = temp2.ToString("F2");

            temp1 = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Min, 1.0f, 0.0f, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false);
            temp2 = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Max, 1.0f, 0.0f, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false);
            MagicAttack.Text = temp1.ToString("F2");
            MagicAttackMax.Text = temp2.ToString("F2");

            temp1 = PrimaryLogic.MagicDefenseValue(player, PrimaryLogic.NeedType.Min, false);
            temp2 = PrimaryLogic.MagicDefenseValue(player, PrimaryLogic.NeedType.Max, false);
            MagicDefense.Text = temp1.ToString("F2");
            MagicDefenseMax.Text = temp2.ToString("F2");

            temp1 = PrimaryLogic.BattleSpeedValue(player, false);
            BattleSpeed.Text = temp1.ToString("F2");

            temp1 = PrimaryLogic.BattleResponseValue(player, false);
            BattleResponse.Text = temp1.ToString("F2");

            temp1 = PrimaryLogic.PotentialValue(player, false);
            Potential.Text = temp1.ToString("F2");

            Color downColor = Color.Red;
            Color upColor = Color.Blue;
            Color normalColor = Color.Black;

            if (MainBlade)
            {
                //MessageBox.Show("mainblade only");
                // 物理攻撃（最小）
                if (PrimaryLogic.PhysicalAttackValue(shadow, PrimaryLogic.NeedType.Min, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false) >
                    PrimaryLogic.PhysicalAttackValue(this.Player, PrimaryLogic.NeedType.Min, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false)) this.PhysicalAttack.ForeColor = upColor;
                else if (PrimaryLogic.PhysicalAttackValue(shadow, PrimaryLogic.NeedType.Min, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false) <
                    PrimaryLogic.PhysicalAttackValue(this.Player, PrimaryLogic.NeedType.Min, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false)) this.PhysicalAttack.ForeColor = downColor;
                else this.PhysicalAttack.ForeColor = normalColor;
                // 物理攻撃（最大）
                if (PrimaryLogic.PhysicalAttackValue(shadow, PrimaryLogic.NeedType.Max, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false) >
                    PrimaryLogic.PhysicalAttackValue(this.Player, PrimaryLogic.NeedType.Max, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false)) this.PhysicalAttackMax.ForeColor = upColor;
                else if (PrimaryLogic.PhysicalAttackValue(shadow, PrimaryLogic.NeedType.Max, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false) <
                    PrimaryLogic.PhysicalAttackValue(this.Player, PrimaryLogic.NeedType.Max, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false)) this.PhysicalAttackMax.ForeColor = downColor;
                else this.PhysicalAttackMax.ForeColor = normalColor;
            }

            if (MainBlade == false && SubBlade == true)
            {
                // 物理攻撃（最小）
                if (PrimaryLogic.SubAttackValue(shadow, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, PlayerStance.None, false) >
                    PrimaryLogic.SubAttackValue(this.Player, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, PlayerStance.None, false)) this.PhysicalAttack.ForeColor = upColor;
                else if (PrimaryLogic.SubAttackValue(shadow, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, PlayerStance.None, false) <
                    PrimaryLogic.SubAttackValue(this.Player, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, PlayerStance.None, false)) this.PhysicalAttack.ForeColor = downColor;
                else this.PhysicalAttack.ForeColor = normalColor;
                // 物理攻撃（最大）
                if (PrimaryLogic.SubAttackValue(shadow, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, PlayerStance.None, false) >
                    PrimaryLogic.SubAttackValue(this.Player, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, PlayerStance.None, false)) this.PhysicalAttackMax.ForeColor = upColor;
                else if (PrimaryLogic.SubAttackValue(shadow, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, PlayerStance.None, false) <
                    PrimaryLogic.SubAttackValue(this.Player, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, PlayerStance.None, false)) this.PhysicalAttackMax.ForeColor = downColor;
                else this.PhysicalAttackMax.ForeColor = normalColor;
            }

            if (DoubleBlade)
            {
                // 物理攻撃２（最小）
                if (PrimaryLogic.SubAttackValue(shadow, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, PlayerStance.None, false) >
                    PrimaryLogic.SubAttackValue(this.Player, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, PlayerStance.None, false)) this.PhysicalAttack2.ForeColor = upColor;
                else if (PrimaryLogic.SubAttackValue(shadow, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, PlayerStance.None, false) <
                    PrimaryLogic.SubAttackValue(this.Player, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, PlayerStance.None, false)) this.PhysicalAttack2.ForeColor = downColor;
                else this.PhysicalAttack2.ForeColor = normalColor;
                // 物理攻撃２（最大）
                if (PrimaryLogic.SubAttackValue(shadow, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, PlayerStance.None, false) >
                    PrimaryLogic.SubAttackValue(this.Player, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, PlayerStance.None, false)) this.PhysicalAttack2Max.ForeColor = upColor;
                else if (PrimaryLogic.SubAttackValue(shadow, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, PlayerStance.None, false) <
                    PrimaryLogic.SubAttackValue(this.Player, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, PlayerStance.None, false)) this.PhysicalAttack2Max.ForeColor = downColor;
                else this.PhysicalAttack2Max.ForeColor = normalColor;
            }

            // 魔法攻撃（最小）
            if (PrimaryLogic.MagicAttackValue(shadow, PrimaryLogic.NeedType.Min, 1.0f, 0.0f, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false) >
                PrimaryLogic.MagicAttackValue(this.Player, PrimaryLogic.NeedType.Min, 1.0f, 0.0f, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false)) this.MagicAttack.ForeColor = upColor;
            else if (PrimaryLogic.MagicAttackValue(shadow, PrimaryLogic.NeedType.Min, 1.0f, 0.0f, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false) <
                PrimaryLogic.MagicAttackValue(this.Player, PrimaryLogic.NeedType.Min, 1.0f, 0.0f, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false)) this.MagicAttack.ForeColor = downColor;
            else this.MagicAttack.ForeColor = normalColor;
            // 魔法攻撃（最大）
            if (PrimaryLogic.MagicAttackValue(shadow, PrimaryLogic.NeedType.Max, 1.0f, 0.0f, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false) >
                PrimaryLogic.MagicAttackValue(this.Player, PrimaryLogic.NeedType.Max, 1.0f, 0.0f, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false)) this.MagicAttackMax.ForeColor = upColor;
            else if (PrimaryLogic.MagicAttackValue(shadow, PrimaryLogic.NeedType.Max, 1.0f, 0.0f, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false) <
                PrimaryLogic.MagicAttackValue(this.Player, PrimaryLogic.NeedType.Max, 1.0f, 0.0f, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false)) this.MagicAttackMax.ForeColor = downColor;
            else this.MagicAttackMax.ForeColor = normalColor;

            // 物理防御（最小）
            if (PrimaryLogic.PhysicalDefenseValue(shadow, PrimaryLogic.NeedType.Min, false) > PrimaryLogic.PhysicalDefenseValue(this.Player, PrimaryLogic.NeedType.Min, false)) this.PhysicalDefense.ForeColor = upColor;
            else if (PrimaryLogic.PhysicalDefenseValue(shadow, PrimaryLogic.NeedType.Min, false) < PrimaryLogic.PhysicalDefenseValue(this.Player, PrimaryLogic.NeedType.Min, false)) this.PhysicalDefense.ForeColor = downColor;
            else this.PhysicalDefense.ForeColor = normalColor;
            // 物理防御（最大）
            if (PrimaryLogic.PhysicalDefenseValue(shadow, PrimaryLogic.NeedType.Max, false) > PrimaryLogic.PhysicalDefenseValue(this.Player, PrimaryLogic.NeedType.Max, false)) this.PhysicalDefenseMax.ForeColor = upColor;
            else if (PrimaryLogic.PhysicalDefenseValue(shadow, PrimaryLogic.NeedType.Max, false) < PrimaryLogic.PhysicalDefenseValue(this.Player, PrimaryLogic.NeedType.Max, false)) this.PhysicalDefenseMax.ForeColor = downColor;
            else this.PhysicalDefenseMax.ForeColor = normalColor;

            // 魔法防御（最小）
            if (PrimaryLogic.MagicDefenseValue(shadow, PrimaryLogic.NeedType.Min, false) > PrimaryLogic.MagicDefenseValue(this.Player, PrimaryLogic.NeedType.Min, false)) this.MagicDefense.ForeColor = upColor;
            else if (PrimaryLogic.MagicDefenseValue(shadow, PrimaryLogic.NeedType.Min, false) < PrimaryLogic.MagicDefenseValue(this.Player, PrimaryLogic.NeedType.Min, false)) this.MagicDefense.ForeColor = downColor;
            else this.MagicDefense.ForeColor = normalColor;
            // 魔法防御（最大）
            if (PrimaryLogic.MagicDefenseValue(shadow, PrimaryLogic.NeedType.Max, false) > PrimaryLogic.MagicDefenseValue(this.Player, PrimaryLogic.NeedType.Max, false)) this.MagicDefenseMax.ForeColor = upColor;
            else if (PrimaryLogic.MagicDefenseValue(shadow, PrimaryLogic.NeedType.Max, false) < PrimaryLogic.MagicDefenseValue(this.Player, PrimaryLogic.NeedType.Max, false)) this.MagicDefenseMax.ForeColor = downColor;
            else this.MagicDefenseMax.ForeColor = normalColor;
            // 戦闘速度
            if (PrimaryLogic.BattleSpeedValue(shadow, false) > PrimaryLogic.BattleSpeedValue(this.Player, false)) this.BattleSpeed.ForeColor = upColor;
            else if (PrimaryLogic.BattleSpeedValue(shadow, false) < PrimaryLogic.BattleSpeedValue(this.Player, false)) this.BattleSpeed.ForeColor = downColor;
            else this.BattleSpeed.ForeColor = normalColor;
            // 戦闘反応
            if (PrimaryLogic.BattleResponseValue(shadow, false) > PrimaryLogic.BattleResponseValue(this.Player, false)) this.BattleResponse.ForeColor = upColor;
            else if (PrimaryLogic.BattleResponseValue(shadow, false) < PrimaryLogic.BattleResponseValue(this.Player, false)) this.BattleResponse.ForeColor = downColor;
            else this.BattleResponse.ForeColor = normalColor;
            // 潜在能力
            if (PrimaryLogic.PotentialValue(shadow, false) > PrimaryLogic.PotentialValue(this.Player, false)) this.Potential.ForeColor = upColor;
            else if (PrimaryLogic.PotentialValue(shadow, false) < PrimaryLogic.PotentialValue(this.Player, false)) this.Potential.ForeColor = downColor;
            else this.Potential.ForeColor = normalColor;
        }

        private void target10_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void target1_MouseEnter(object sender, EventArgs e)
        {
            string target = ((Button)sender).Text;
            if (target == null)
            {
                return;
            }
            if ((target == string.Empty) ||
                (target == ""))
            {
                return;
            }

            ItemBackPack temp = new ItemBackPack(((Button)sender).Text);
            mainMessage.Text = temp.Description;

            if (this.EquipType == 0)
            {
                shadow.MainWeapon = temp;
            }
            else if (this.EquipType == 1)
            {
                shadow.SubWeapon = temp;
            }
            else if (this.EquipType == 2)
            {
                shadow.MainArmor = temp;
            }
            else if (this.EquipType == 3)
            {
                shadow.Accessory = temp;
            }
            else if (this.EquipType == 4)
            {
                shadow.Accessory2 = temp;
            }

            RefreshPartyMemberBaseParameter(shadow);
            RefreshPartyMembersBattleStatus(shadow);
        }

        private void target1_MouseLeave(object sender, EventArgs e)
        {
            if (this.EquipType == 0)
            {
                shadow.MainWeapon = this.Player.MainWeapon;
            }
            else if (this.EquipType == 1)
            {
                shadow.SubWeapon = this.Player.SubWeapon;
            }
            else if (this.EquipType == 2)
            {
                shadow.MainArmor = this.Player.MainArmor;
            }
            else if (this.EquipType == 3)
            {
                shadow.Accessory = this.Player.Accessory;
            }
            else if (this.EquipType == 4)
            {
                shadow.Accessory2 = this.Player.Accessory2;
            }

            mainMessage.Text = "";
            RefreshPartyMemberBaseParameter(this.Player);
            RefreshPartyMembersBattleStatus(this.Player);
        }

        const int MAX_NUM = 9;
        private void PageNumber1_Click(object sender, EventArgs e)
        {
            int pageNumber = Convert.ToInt32(((Button)sender).Text) - 1;

            if (btn[pageNumber * MAX_NUM + 0] != null) { target1.Text = btn[pageNumber * MAX_NUM + 0].Text; }
            if (btn[pageNumber * MAX_NUM + 1] != null) { target2.Text = btn[pageNumber * MAX_NUM + 1].Text; }
            if (btn[pageNumber * MAX_NUM + 2] != null) { target3.Text = btn[pageNumber * MAX_NUM + 2].Text; }
            if (btn[pageNumber * MAX_NUM + 3] != null) { target4.Text = btn[pageNumber * MAX_NUM + 3].Text; }
            if (btn[pageNumber * MAX_NUM + 4] != null) { target5.Text = btn[pageNumber * MAX_NUM + 4].Text; }
            if (btn[pageNumber * MAX_NUM + 5] != null) { target6.Text = btn[pageNumber * MAX_NUM + 5].Text; }
            if (btn[pageNumber * MAX_NUM + 6] != null) { target7.Text = btn[pageNumber * MAX_NUM + 6].Text; }
            if (btn[pageNumber * MAX_NUM + 7] != null) { target8.Text = btn[pageNumber * MAX_NUM + 7].Text; }
            if (btn[pageNumber * MAX_NUM + 8] != null) { target9.Text = btn[pageNumber * MAX_NUM + 8].Text; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ItemBackPack[] tempBackPack = this.Player.GetBackPackInfo();
            int count = 0;
            if (tempBackPack != null)
            {
                for (int ii = 0; ii < tempBackPack.Length; ii++)
                {
                    if (tempBackPack[ii] != null)
                    {
                        if ((tempBackPack[ii].Name != String.Empty) && (tempBackPack[ii].Name != ""))
                        {
                            count++;
                        }
                    }
                }
                if (count >= Database.MAX_BACKPACK_SIZE)
                {
                    mainMessage.Text = Player.GetCharacterSentence(2029);
                    return;
                }
            }
            this.SelectValue = "";
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

    }
}
