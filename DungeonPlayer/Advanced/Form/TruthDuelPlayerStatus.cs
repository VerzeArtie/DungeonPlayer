using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class TruthDuelPlayerStatus : MotherForm
    {
        public TruthEnemyCharacter tec { get; set; }

        private System.Windows.Forms.Label[] backpack;

        public TruthDuelPlayerStatus()
        {
            InitializeComponent();
            SetupBackpackData();    
        }

        private void TruthDuelPlayerStatus_Load(object sender, EventArgs e)
        {
            this.buttonStrength.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "StrengthMark.bmp");
            this.buttonAgility.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "AgilityMark.bmp");
            this.buttonIntelligence.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "IntelligenceMark.bmp");
            this.buttonStamina.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "StaminaMark.bmp");
            this.buttonMind.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "MindMark.bmp");

            if (tec != null)
            {
                SettingCharacterData(tec);
                RefreshPartyMembersBattleStatus(tec);
            }
        }

        private void SettingCharacterData(MainCharacter chara)
        {
            this.playerName.Text = chara.FullName;
            this.level.Text = chara.Level.ToString();

            this.strength.Text = chara.Strength.ToString();
            if (chara.Strength >= 100) this.strength.Font = new Font(this.strength.Font.FontFamily, 16, (FontStyle)((FontStyle.Bold | FontStyle.Italic)));
            else this.strength.Font = new Font(this.strength.Font.FontFamily, 20.25F, FontStyle.Bold | FontStyle.Italic);
            this.addStrength.Text = " + " + chara.BuffStrength_Accessory.ToString();

            this.agility.Text = chara.Agility.ToString();
            if (chara.Agility >= 100) this.agility.Font = new Font(this.agility.Font.FontFamily, 16, FontStyle.Bold | FontStyle.Italic);
            else this.agility.Font = new Font(this.agility.Font.FontFamily, 20.25F, FontStyle.Bold | FontStyle.Italic);
            this.addAgility.Text = " + " + chara.BuffAgility_Accessory.ToString();

            this.intelligence.Text = chara.Intelligence.ToString();
            if (chara.Intelligence >= 100) this.intelligence.Font = new Font(this.intelligence.Font.FontFamily, 16, FontStyle.Bold | FontStyle.Italic);
            else this.intelligence.Font = new Font(this.intelligence.Font.FontFamily, 20.25F, FontStyle.Bold | FontStyle.Italic);
            this.addIntelligence.Text = " + " + chara.BuffIntelligence_Accessory.ToString();

            this.stamina.Text = chara.Stamina.ToString();
            if (chara.Stamina >= 100) this.stamina.Font = new Font(this.stamina.Font.FontFamily, 16, FontStyle.Bold | FontStyle.Italic);
            else this.stamina.Font = new Font(this.stamina.Font.FontFamily, 20.25F, FontStyle.Bold | FontStyle.Italic);
            this.addStamina.Text = " + " + chara.BuffStamina_Accessory.ToString();

            this.mindLabel.Text = chara.Mind.ToString();
            if (chara.Mind >= 100) this.mindLabel.Font = new Font(this.mindLabel.Font.FontFamily, 16, FontStyle.Bold | FontStyle.Italic);
            else this.mindLabel.Font = new Font(this.mindLabel.Font.FontFamily, 20.25F, FontStyle.Bold | FontStyle.Italic);
            this.addMind.Text = " + " + chara.BuffMind_Accessory.ToString();

            this.life.Text = chara.CurrentLife.ToString() + " / " + chara.MaxLife.ToString();

            if (chara.AvailableSkill)
            {
                label24.Visible = true;
                skill.Visible = true;
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
            //spell1.Visible = chara.FreshHeal;

            this.weapon.Text = "";
            this.subWeapon.Text = "";
            this.armor.Text = "";
            this.accessory.Text = "";
            if (chara.MainWeapon != null)
            {
                this.weapon.Text = chara.MainWeapon.Name;
                UpdateLabelColorForRare(ref this.weapon, chara.MainWeapon.Rare);
            }
            if (chara.SubWeapon != null)
            {
                this.subWeapon.Text = chara.SubWeapon.Name;
                UpdateLabelColorForRare(ref this.subWeapon, chara.SubWeapon.Rare);
            }
            if (chara.MainArmor != null)
            {
                this.armor.Text = chara.MainArmor.Name;
                UpdateLabelColorForRare(ref this.armor, chara.MainArmor.Rare);
            }
            if (chara.Accessory != null)
            {
                this.accessory.Text = chara.Accessory.Name;
                UpdateLabelColorForRare(ref this.accessory, chara.Accessory.Rare);
            }
            if (chara.Accessory2 != null)
            {
                this.accessory2.Text = chara.Accessory2.Name;
                UpdateLabelColorForRare(ref this.accessory2, chara.Accessory2.Rare);
            }
            UpdateBackPackLabel(chara);
            UpdateSpellSkillLabel(chara);
        }

        Point basePhysicalLocation;
        private void RefreshPartyMembersBattleStatus(MainCharacter player)
        {
        }

        private void UpdateSpellSkillLabel(MainCharacter target)
        {
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        static int FIXED_ROW_NUM = 10; // バックパックや魔法・スキルのリスト欄の最大列数
        private void SetupBackpackData()
        {
            backpack = new Label[Database.MAX_BACKPACK_SIZE];

            for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
            {
                backpack[ii] = new Label();
                backpack[ii].Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Underline | System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                backpack[ii].Location = new System.Drawing.Point(50 + 320 * (ii / FIXED_ROW_NUM), 15 + 31 * (ii % FIXED_ROW_NUM));
                backpack[ii].Name = "backpack" + ii.ToString();
                backpack[ii].AutoSize = true;
                backpack[ii].TabIndex = 0;
                backpack[ii].MouseEnter += new EventHandler(StatusPlayer_MouseEnter);
                backpack[ii].MouseDown += new MouseEventHandler(StatusPlayer_MouseDown);
                backpack[ii].MouseLeave += new EventHandler(StatusPlayer_MouseLeave);
                this.grpBackPack.Controls.Add(backpack[ii]);
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
                }
                else
                {
                    backpack[ii].Text = backpackData[ii].Name;
                    backpack[ii].Cursor = System.Windows.Forms.Cursors.Hand;
                    UpdateLabelColorForRare(ref backpack[ii], backpackData[ii].Rare);
                }
            }
        }
        void StatusPlayer_MouseLeave(object sender, EventArgs e)
        {
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

    }
}
