using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class MessageDisplayWithIcon : MessageDisplay
    {
        private ItemBackPack item = null;
        public ItemBackPack Item
        {
            get { return item; }
            set { item = value; }
        }

        public MessageDisplayWithIcon()
        {
            InitializeComponent();
        }

        private void MessageDisplayWithIcon_Load(object sender, EventArgs e)
        {
            if (item != null)
            {
                switch (item.Rare)
                {
                    case ItemBackPack.RareLevel.Poor:
                        BackColor = Color.Gray;
                        label1.BackColor = Color.Gray;
                        label1.ForeColor = Color.Black;
                        break;
                    case ItemBackPack.RareLevel.Common:
                        BackColor = Color.LightGreen;
                        label1.BackColor = Color.LightGreen;
                        label1.ForeColor = Color.Black;
                        break;
                    case ItemBackPack.RareLevel.Rare:
                        BackColor = Color.Blue;
                        label1.BackColor = Color.Blue;
                        label1.ForeColor = Color.White;
                        break;
                    case ItemBackPack.RareLevel.Epic:
                        BackColor = Color.Purple;
                        label1.BackColor = Color.Purple;
                        label1.ForeColor = Color.White;
                        break;
                    case ItemBackPack.RareLevel.Legendary: // Œã•Ò’Ç‰Á
                        BackColor = Color.OrangeRed;
                        label1.BackColor = Color.OrangeRed;
                        label1.ForeColor = Color.White;
                        break;
                    default:
                        BackColor = Color.Gray;
                        label1.BackColor = Color.Gray;
                        label1.ForeColor = Color.Black;
                        break;
                }

                switch (item.Type)
                {
                    case ItemBackPack.ItemType.Weapon_TwoHand:
                        pictureBox1.Image = Image.FromFile(Database.BaseResourceFolder + "TwoHand.bmp");
                        break;
                    case ItemBackPack.ItemType.Weapon_Light:
                        pictureBox1.Image = Image.FromFile(Database.BaseResourceFolder + "Knuckle.bmp");
                        break;
                    case ItemBackPack.ItemType.Weapon_Heavy:
                    case ItemBackPack.ItemType.Weapon_Middle:
                        pictureBox1.Image = Image.FromFile(Database.BaseResourceFolder + "Weapon.bmp");
                        break;
                    case ItemBackPack.ItemType.Weapon_Rod:
                        pictureBox1.Image = Image.FromFile(Database.BaseResourceFolder + "Rod.bmp");
                        break;
                    case ItemBackPack.ItemType.Accessory:
                        pictureBox1.Image = Image.FromFile(Database.BaseResourceFolder + "Accessory.bmp");
                        break;
                    case ItemBackPack.ItemType.Armor_Heavy:
                    case ItemBackPack.ItemType.Armor_Middle:
                        pictureBox1.Image = Image.FromFile(Database.BaseResourceFolder + "Armor.bmp");
                        break;
                    case ItemBackPack.ItemType.Armor_Light:
                        pictureBox1.Image = Image.FromFile(Database.BaseResourceFolder + "LightArmor.bmp");
                        break;
                    case ItemBackPack.ItemType.Material_Equip:
                    case ItemBackPack.ItemType.Material_Food:
                    case ItemBackPack.ItemType.Material_Potion:
                        pictureBox1.Image = Image.FromFile(Database.BaseResourceFolder + "Material1.bmp");
                        break;
                    case ItemBackPack.ItemType.Shield:
                        pictureBox1.Image = Image.FromFile(Database.BaseResourceFolder + "Shield.bmp");
                        break;
                    case ItemBackPack.ItemType.Use_Any:
                        pictureBox1.Image = Image.FromFile(Database.BaseResourceFolder + "UseItem.bmp");
                        break;
                    case ItemBackPack.ItemType.Use_Potion:
                        pictureBox1.Image = Image.FromFile(Database.BaseResourceFolder + "Potion.bmp");
                        break;
                    case ItemBackPack.ItemType.Useless:
                        pictureBox1.Image = Image.FromFile(Database.BaseResourceFolder + "Useless.bmp");
                        break;
                    case ItemBackPack.ItemType.None:
                        pictureBox1.Visible = false;
                        break;
                    default:
                        pictureBox1.Image = Image.FromFile(Database.BaseResourceFolder + "Useless.bmp");
                        break;
                }
            }
        }
    }
}