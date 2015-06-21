using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class EquipmentShop : MotherForm
    {
        public bool LayoutLarge { get; set; } // ��Ғǉ�

        protected MainCharacter mc;
        public MainCharacter MC
        {
            get { return mc; }
            set { mc = value; }
        }
        protected MainCharacter sc;
        public MainCharacter SC
        {
            get { return sc; }
            set { sc = value; }
        }
        protected MainCharacter tc;
        public MainCharacter TC
        {
            get { return tc; }
            set { tc = value; }
        }

        protected WorldEnvironment we;
        public WorldEnvironment WE
        {
            get { return we; }
            set { we = value; }
        }

        protected MainCharacter ganz;

        protected System.Windows.Forms.Label[] equipList;
        protected System.Windows.Forms.Label[] costList;
        protected int MAX_EQUIPLIST = 25; // ��ҕҏW

        protected Label[] backpackList;
        protected MainCharacter currentPlayer;

        protected int YESNO_LOCATION_X = 440;
        protected int YESNO_LOCATION_Y = 440;
        protected int OK_LOCATION_X = 540;
        protected int OK_LOCATION_Y = 440;
        protected int OK_SIZE_X = 100;
        protected int OK_SIZE_Y = 40;

        public EquipmentShop()
        {
            InitializeComponent();
            ganz = new MainCharacter();
            ganz.FullName = "�K���c�E�M�����K";
            ganz.Name = "�K���c";

            equipList = new Label[MAX_EQUIPLIST];
            costList = new Label[MAX_EQUIPLIST];
            backpackList = new Label[Database.MAX_BACKPACK_SIZE];
            
            // move-out
            OnInitializeLayout();
        }

        // s ��Ғǉ�
        protected virtual void OnInitializeLayout()
        {
            // �C���X�^���X�������ŋL�q���Ă���������������ֈڍs���A��҉�ʃ��C�A�E�g�͕ʓr�ł���悤�ɂ����B
            for (int ii = 0; ii < MAX_EQUIPLIST; ii++)
            {
                equipList[ii] = new Label();
                equipList[ii].Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                equipList[ii].Location = new System.Drawing.Point(50, 100 + 26 * ii); // ��ҕҏW
                equipList[ii].Name = "equipList" + ii.ToString();
                equipList[ii].Size = new Size(180, 12);
                equipList[ii].AutoSize = true;
                equipList[ii].TabIndex = 0;
                equipList[ii].Cursor = System.Windows.Forms.Cursors.Hand;
                equipList[ii].MouseEnter += new EventHandler(EquipmentShop_MouseEnter);
                equipList[ii].MouseMove += new MouseEventHandler(EquipmentShop_MouseMove);
                equipList[ii].MouseLeave += new EventHandler(EquipmentShop_MouseLeave);
                equipList[ii].Click += new EventHandler(EquipmentShop_Click);
                this.Controls.Add(equipList[ii]);

                costList[ii] = new Label();
                costList[ii].Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                costList[ii].Location = new System.Drawing.Point(50 + 200, 100 + 26 * ii); // ��ҕҏW
                costList[ii].Name = "costList" + ii.ToString();
                costList[ii].Size = new Size(200, 12);
                costList[ii].TabIndex = 0;
                costList[ii].AutoSize = true;
                this.Controls.Add(costList[ii]);
            }

            for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
            {
                backpackList[ii] = new Label();
                backpackList[ii].Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                backpackList[ii].Location = new System.Drawing.Point(50 + 400, 116 + 31 * ii);
                backpackList[ii].Name = "backpackList" + ii.ToString();
                backpackList[ii].Size = new Size(200, 12);
                backpackList[ii].TabIndex = 0;
                backpackList[ii].AutoSize = true;
                backpackList[ii].MouseEnter += new EventHandler(EquipmentShop_MouseEnter);
                backpackList[ii].MouseLeave += new EventHandler(EquipmentShop_MouseLeave);
                backpackList[ii].Click += new EventHandler(EquipmentShop_Click);
                this.Controls.Add(backpackList[ii]);
            }

            for (int ii = 0; ii < MAX_EQUIPLIST; ii++)
            {
                equipList[ii] = new Label();
                equipList[ii].Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                equipList[ii].Location = new System.Drawing.Point(50, 100 + 26 * ii); // ��ҕҏW
                equipList[ii].Name = "equipList" + ii.ToString();
                equipList[ii].Size = new Size(180, 12);
                equipList[ii].AutoSize = true;
                equipList[ii].TabIndex = 0;
                equipList[ii].Cursor = System.Windows.Forms.Cursors.Hand;
                equipList[ii].MouseEnter += new EventHandler(EquipmentShop_MouseEnter);
                equipList[ii].MouseMove += new MouseEventHandler(EquipmentShop_MouseMove);
                equipList[ii].MouseLeave += new EventHandler(EquipmentShop_MouseLeave);
                equipList[ii].Click += new EventHandler(EquipmentShop_Click);
                this.Controls.Add(equipList[ii]);

                costList[ii] = new Label();
                costList[ii].Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                costList[ii].Location = new System.Drawing.Point(50 + 200, 100 + 26 * ii); // ��ҕҏW
                costList[ii].Name = "costList" + ii.ToString();
                costList[ii].Size = new Size(200, 12);
                costList[ii].TabIndex = 0;
                costList[ii].AutoSize = true;
                this.Controls.Add(costList[ii]);
            }

            for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
            {
                backpackList[ii] = new Label();
                backpackList[ii].Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                backpackList[ii].Location = new System.Drawing.Point(50 + 400, 116 + 31 * ii);
                backpackList[ii].Name = "backpackList" + ii.ToString();
                backpackList[ii].Size = new Size(200, 12);
                backpackList[ii].TabIndex = 0;
                backpackList[ii].AutoSize = true;
                backpackList[ii].MouseEnter += new EventHandler(EquipmentShop_MouseEnter);
                backpackList[ii].MouseLeave += new EventHandler(EquipmentShop_MouseLeave);
                backpackList[ii].Click += new EventHandler(EquipmentShop_Click);
                this.Controls.Add(backpackList[ii]);
            }
        }
        // e ��Ғǉ�

        protected void EquipmentShop_Load(object sender, EventArgs e)
        {
            this.currentPlayer = this.mc;

            if (!we.AvailableSecondCharacter && !we.AvailableThirdCharacter)
            {
                btnBackpack1.Visible = false; // [�R�����g]�F�ŏ��̓L�����N�^�[�����������Ȃ����o�̂��߁AVisible��False
                btnBackpack2.Visible = false;
                btnBackpack3.Visible = false;
            }
            else if (we.AvailableSecondCharacter && !we.AvailableThirdCharacter)
            {
                btnBackpack1.Visible = true;
                btnBackpack2.Visible = true;
                btnBackpack3.Visible = false;
            }
            else if (we.AvailableThirdCharacter)
            {
                btnBackpack1.Visible = true;
                btnBackpack2.Visible = true;
                btnBackpack3.Visible = false; // [�R�����g]�F�X�g�[���[�̉��o��A���F���[�̓K���c�̕���֖K��鎖�͂Ȃ����߁B
            }

            UpdateBackPackLabel(this.currentPlayer);

            if (we.AvailableEquipShop && !we.AvailableEquipShop2)
            {
                SetupAvailableList(1);

                btnLevel1.Visible = false; // [�R�����g]�F�ŏ��͕����ނ�������X���������Ȃ����o�̂��߁AVisible��False
                btnLevel2.Visible = false;
                btnLevel3.Visible = false;
                btnLevel4.Visible = false;
                btnLevel5.Visible = false;
            }
            else if (we.AvailableEquipShop && we.AvailableEquipShop2 && !we.AvailableEquipShop3)
            {
                SetupAvailableList(2);
                btnLevel1.Visible = true;
                btnLevel2.Visible = true;
                btnLevel3.Visible = false;
                btnLevel4.Visible = false;
                btnLevel5.Visible = false;
            }
            else if (we.AvailableEquipShop && we.AvailableEquipShop2 && we.AvailableEquipShop3 && !we.AvailableEquipShop4)
            {
                SetupAvailableList(3);
                btnLevel1.Visible = true;
                btnLevel2.Visible = true;
                btnLevel3.Visible = true;
                btnLevel4.Visible = false;
                btnLevel5.Visible = false;
            }
            else if (we.AvailableEquipShop && we.AvailableEquipShop2 && we.AvailableEquipShop3 && we.AvailableEquipShop4 && !we.AvailableEquipShop5)
            {
                SetupAvailableList(4);
                btnLevel1.Visible = true;
                btnLevel2.Visible = true;
                btnLevel3.Visible = true;
                btnLevel4.Visible = true;
                btnLevel5.Visible = false;
            }
            else if (we.AvailableEquipShop && we.AvailableEquipShop2 && we.AvailableEquipShop3 && we.AvailableEquipShop4 && we.AvailableEquipShop5)
            {
                //SetupAvailableList(5);
                //btnLevel1.Visible = true;
                //btnLevel2.Visible = true;
                //btnLevel3.Visible = true;
                //btnLevel4.Visible = true;
                //btnLevel5.Visible = true;
                SetupAvailableList(5);
                btnLevel1.Visible = true;
                btnLevel2.Visible = true;
                btnLevel3.Visible = true;
                btnLevel4.Visible = true;
                btnLevel5.Visible = true;
            }
            else
            {

            }
            OnLoadSetupFloorButton();
            OnLoadMessage(); // ��ҕҏW
            label2.Text = mc.Gold.ToString() + "[G]"; // [�x��]�F�S�[���h�̏����͕ʃN���X�ɂ���ׂ��ł��B
        }

        // s ��Ғǉ�
        protected virtual void OnLoadSetupFloorButton()
        {
            // �O�҂ł͎������Ă��Ȃ�
        }
        // e ��Ғǉ�

        // s ��Ғǉ�
        protected virtual void OnLoadMessage()
        {
            SetupMessageText(3000);
        }
        // e ��Ғǉ�

        // [�R�����g]�F�����������ɑ�����\��������ꍇ�A�L�q���@���������肻���ł��B���Ԃ�����ΒT���Ă��������B
        protected void SetupMessageText(int number)
        {
            if (!we.AvailableEquipShop5)
            {
                mainMessage.Text = ganz.GetCharacterSentence(number);
            }
            else
            {
                mainMessage.Text = this.currentPlayer.GetCharacterSentence(number);
            }
            mainMessage.Update(); // ��Ғǉ�
        }

        protected void SetupMessageText(int number, string arg1)
        {
            if (!we.AvailableEquipShop5)
            {
                mainMessage.Text = String.Format(ganz.GetCharacterSentence(number), arg1);
            }
            else
            {
                mainMessage.Text = String.Format(this.currentPlayer.GetCharacterSentence(number), arg1);
            }
            mainMessage.Update(); // ��Ғǉ�
        }

        protected void SetupMessageText(int number, string arg1, string arg2)
        {
            if (!we.AvailableEquipShop5)
            {
                mainMessage.Text = String.Format(ganz.GetCharacterSentence(number), arg1, arg2);
            }
            else
            {
                mainMessage.Text = String.Format(this.currentPlayer.GetCharacterSentence(number), arg1, arg2);
            }
            mainMessage.Update(); // ��Ғǉ�
        }

        protected void EquipmentShop_MouseEnter(object sender, EventArgs e)
        {
            for (int ii = 0; ii < MAX_EQUIPLIST; ii++)
            {
                if (((Label)sender).Name == "equipList" + ii.ToString())
                {
                    ItemBackPack temp = new ItemBackPack(equipList[ii].Text);
                    mainMessage.Text = temp.Description;
                    return;
                }
            }

            for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
            {
                if (((Label)sender).Name == "backpackList" + ii.ToString())
                {
                    ItemBackPack temp = new ItemBackPack(backpackList[ii].Text);
                    mainMessage.Text = temp.Description;
                    return;
                }
            }
        }

        protected PopUpMini popupInfo = null;
        protected void EquipmentShop_MouseLeave(object sender, EventArgs e)
        {
            if (popupInfo != null)
            {
                popupInfo.Close();
                popupInfo = null;
            }
        }
        // s ��ҕҏW
        protected virtual void ConstructPopupInfo(PopUpMini popupInfo)
        {
            popupInfo.CurrentInfo = currentPlayer.FullName + "\r\n";

            if (currentPlayer.MainWeapon == null)
            {
                popupInfo.CurrentInfo += "���� " + "�i�Ȃ��j" + "\r\n";
            }
            else
            {
                popupInfo.CurrentInfo += "���� " + currentPlayer.MainWeapon.Name + "\r\n";
            }
            if (currentPlayer.MainArmor == null)
            {
                popupInfo.CurrentInfo += "�h�� " + "�i�Ȃ��j" + "\r\n";
            }
            else
            {
                popupInfo.CurrentInfo += "�h�� " + currentPlayer.MainArmor.Name + "\r\n";
            }
            if (currentPlayer.Accessory == null)
            {
                popupInfo.CurrentInfo += "�����i  " + "�i�Ȃ��j" + "\r\n";
            }
            else
            {
                popupInfo.CurrentInfo += "�����i  " + currentPlayer.Accessory.Name + "\r\n";
            }
            popupInfo.CurrentInfo += "\r\n";
        }
        // e ��ҕҏW

        protected void EquipmentShop_MouseMove(object sender, MouseEventArgs e)
        {

            if (popupInfo == null)
            {
                popupInfo = new PopUpMini();
            }

            popupInfo.StartPosition = FormStartPosition.Manual;
            popupInfo.Location = new Point(this.Location.X + ((Label)sender).Location.X + e.X + 10, this.Location.Y + ((Label)sender).Location.Y + e.Y + -100);
            popupInfo.PopupColor = Color.Black;
            // s ��ҕҏW
            System.OperatingSystem os = System.Environment.OSVersion;
            int osNumber = os.Version.Major;
            if (osNumber != 5)
            {
                popupInfo.Opacity = 0.7f;
            }
            //popupInfo.Opacity = 0.7f; // ��ҍ폜
            //popupInfo.PopupTextColor = Brushes.White; // ��ҍ폜
            // e ��ҕҏW

            popupInfo.FontFamilyName = new Font("�l�r �S�V�b�N", 14.0F, FontStyle.Regular, GraphicsUnit.Pixel, 128, true);

            ConstructPopupInfo(popupInfo);

            // [�x��] ���������ŗǂ��Ƃ��邪�A����A�N�Z�T���ōU���͏㏸������A����Ńp�����^UP��������̂�����΁A�Ή��K�{�B
            ItemBackPack itemInfo = new ItemBackPack(((Label)sender).Text);
            switch (itemInfo.Type)
            {
                case ItemBackPack.ItemType.Armor_Heavy:
                case ItemBackPack.ItemType.Armor_Light:
                case ItemBackPack.ItemType.Armor_Middle:
                    if (((currentPlayer == mc) && (itemInfo.Type == ItemBackPack.ItemType.Armor_Heavy))
                        || ((currentPlayer == sc) && (itemInfo.Type == ItemBackPack.ItemType.Armor_Light || itemInfo.Type == ItemBackPack.ItemType.Armor_Middle))
                        || ((currentPlayer == tc) && (itemInfo.Type == ItemBackPack.ItemType.Armor_Middle)))
                    {
                        popupInfo.CurrentInfo += "�ŏ��h���  " + currentPlayer.MainArmor.MinValue.ToString() + " ==> " + itemInfo.MinValue.ToString() + "\r\n";
                        popupInfo.CurrentInfo += "�ő�h���  " + currentPlayer.MainArmor.MaxValue.ToString() + " ==> " + itemInfo.MaxValue.ToString() + "\r\n";
                    }
                    else
                    {
                        popupInfo.CurrentInfo += "�i�����s�j" + "\r\n";
                    }
                    break;
                case ItemBackPack.ItemType.Weapon_Heavy:
                case ItemBackPack.ItemType.Weapon_Light:
                case ItemBackPack.ItemType.Weapon_Middle:
                    if (((currentPlayer == mc) && (itemInfo.Type == ItemBackPack.ItemType.Weapon_Heavy))
                        || ((currentPlayer == sc) && (itemInfo.Type == ItemBackPack.ItemType.Weapon_Light))
                        || ((currentPlayer == tc) && (itemInfo.Type == ItemBackPack.ItemType.Weapon_Middle)))
                    {
                        popupInfo.CurrentInfo += "�ŏ��U����  " + currentPlayer.MainWeapon.MinValue.ToString() + " ==> " + itemInfo.MinValue.ToString() + "\r\n";
                        popupInfo.CurrentInfo += "�ő�U����  " + currentPlayer.MainWeapon.MaxValue.ToString() + " ==> " + itemInfo.MaxValue.ToString() + "\r\n";
                    }
                    else
                    {
                        popupInfo.CurrentInfo += "�i�����s�j" + "\r\n";
                    }
                    break;
                // s ��Ғǉ�
                case ItemBackPack.ItemType.Weapon_Rod:
                    if (((currentPlayer == sc))
                        || ((currentPlayer == tc)))
                    {
                        popupInfo.CurrentInfo += "�ŏ��U����  " + currentPlayer.MainWeapon.MinValue.ToString() + " ==> " + itemInfo.MinValue.ToString() + "\r\n";
                        popupInfo.CurrentInfo += "�ő�U����  " + currentPlayer.MainWeapon.MaxValue.ToString() + " ==> " + itemInfo.MaxValue.ToString() + "\r\n";
                        popupInfo.CurrentInfo += "�ŏ����@��  " + currentPlayer.MainWeapon.MagicMinValue.ToString() + " ==> " + itemInfo.MagicMinValue.ToString() + "\r\n";
                        popupInfo.CurrentInfo += "�ő喂�@��  " + currentPlayer.MainWeapon.MagicMaxValue.ToString() + " ==> " + itemInfo.MagicMaxValue.ToString() + "\r\n";
                    }
                    else
                    {
                        popupInfo.CurrentInfo += "�i�����s�j" + "\r\n";
                    }
                    break;

                case ItemBackPack.ItemType.Shield:
                    if (currentPlayer.SubWeapon != null)
                    {
                        if (currentPlayer.SubWeapon.Type == ItemBackPack.ItemType.Shield)
                        {
                            popupInfo.CurrentInfo += "�ŏ��h���  " + currentPlayer.SubWeapon.MinValue.ToString() + " ==> " + itemInfo.MinValue.ToString() + "\r\n";
                            popupInfo.CurrentInfo += "�ő�h���  " + currentPlayer.SubWeapon.MaxValue.ToString() + " ==> " + itemInfo.MaxValue.ToString() + "\r\n";
                        }
                        else
                        {
                            popupInfo.CurrentInfo += "�ŏ��h���  ---" + " ==> " + itemInfo.MinValue.ToString() + "\r\n";
                            popupInfo.CurrentInfo += "�ő�h���  ---" + " ==> " + itemInfo.MaxValue.ToString() + "\r\n";
                        }
                    }
                    else
                    {
                        popupInfo.CurrentInfo += "�ŏ��h���  ---" + " ==> " + itemInfo.MinValue.ToString() + "\r\n";
                        popupInfo.CurrentInfo += "�ő�h���  ---" + " ==> " + itemInfo.MaxValue.ToString() + "\r\n";
                    }
                    break;

                case ItemBackPack.ItemType.Weapon_TwoHand:
                    if (currentPlayer == mc)
                    {
                        popupInfo.CurrentInfo += "�ŏ��U����  " + currentPlayer.MainWeapon.MinValue.ToString() + " ==> " + itemInfo.MinValue.ToString() + "\r\n";
                        popupInfo.CurrentInfo += "�ő�U����  " + currentPlayer.MainWeapon.MaxValue.ToString() + " ==> " + itemInfo.MaxValue.ToString() + "\r\n";
                    }
                    else
                    {
                        popupInfo.CurrentInfo += "�i�����s�j" + "\r\n";
                    }
                    break;

                // e ��Ғǉ�
                case ItemBackPack.ItemType.Accessory:
                    int fixedSize = 11;
                    // ��
                    string strengthInfo = String.Empty;
                    strengthInfo += "��  " + currentPlayer.Strength.ToString();
                    if (currentPlayer.BuffStrength_Accessory != 0)
                    {
                        strengthInfo += "(+" + currentPlayer.BuffStrength_Accessory.ToString() + ")";
                    }
                    strengthInfo = strengthInfo.PadRight(fixedSize);
                    strengthInfo += " ==> " + currentPlayer.Strength.ToString();
                    if (itemInfo.BuffUpStrength != 0)
                    {
                        strengthInfo += "(+" + itemInfo.BuffUpStrength.ToString() + ")";
                    }
                    strengthInfo += "\r\n";
                    popupInfo.CurrentInfo += strengthInfo;

                    // �Z
                    string agilityInfo = String.Empty;
                    agilityInfo += "�Z  " + currentPlayer.Agility.ToString();
                    if (currentPlayer.BuffAgility_Accessory != 0)
                    {
                        agilityInfo += "(+" + currentPlayer.BuffAgility_Accessory.ToString() + ")";
                    }
                    agilityInfo = agilityInfo.PadRight(fixedSize);
                    agilityInfo += " ==> " + currentPlayer.Agility.ToString();
                    if (itemInfo.BuffUpAgility != 0)
                    {
                        agilityInfo += "(+" + itemInfo.BuffUpAgility.ToString() + ")";
                    }
                    agilityInfo += "\r\n";
                    popupInfo.CurrentInfo += agilityInfo;

                    // �m
                    string intelligenceInfo = String.Empty;
                    intelligenceInfo += "�m  " + currentPlayer.Intelligence.ToString();
                    if (currentPlayer.BuffIntelligence_Accessory != 0)
                    {
                        intelligenceInfo += "(+" + currentPlayer.BuffIntelligence_Accessory.ToString() + ")";
                    }
                    intelligenceInfo = intelligenceInfo.PadRight(fixedSize);
                    intelligenceInfo += " ==> " + currentPlayer.Intelligence.ToString();
                    if (itemInfo.BuffUpIntelligence != 0)
                    {
                        intelligenceInfo += "(+" + itemInfo.BuffUpIntelligence.ToString() + ")";
                    }
                    intelligenceInfo += "\r\n";
                    popupInfo.CurrentInfo += intelligenceInfo;

                    // ��
                    string staminaInfo = String.Empty;
                    staminaInfo += "��  " + currentPlayer.Stamina.ToString();
                    if (currentPlayer.BuffStamina_Accessory != 0)
                    {
                        staminaInfo += "(+" + currentPlayer.BuffStamina_Accessory.ToString() + ")";
                    }
                    staminaInfo = staminaInfo.PadRight(fixedSize);
                    staminaInfo += " ==> " + currentPlayer.Stamina.ToString();
                    if (itemInfo.BuffUpStamina != 0)
                    {
                        staminaInfo += "(+" + itemInfo.BuffUpStamina.ToString() + ")";
                    }
                    staminaInfo += "\r\n";
                    popupInfo.CurrentInfo += staminaInfo;

                    // �S
                    string mindInfo = String.Empty;
                    mindInfo += "�S  " + currentPlayer.Mind.ToString();
                    if (currentPlayer.BuffMind_Accessory != 0)
                    {
                        mindInfo += "(+" + currentPlayer.BuffMind_Accessory.ToString() + ")";
                    }
                    mindInfo = mindInfo.PadRight(fixedSize);
                    mindInfo += " ==> " + currentPlayer.Mind.ToString();
                    if (itemInfo.BuffUpMind != 0)
                    {
                        mindInfo += "(+" + itemInfo.BuffUpMind.ToString() + ")";
                    }
                    mindInfo += "\r\n";
                    popupInfo.CurrentInfo += mindInfo;

                    if (itemInfo.Information != string.Empty)
                    {
                        popupInfo.CurrentInfo += "\r\n";
                        popupInfo.CurrentInfo += itemInfo.Information;
                    }
                    break;
                default:
                    break;
            }
            popupInfo.Show();
        }

        protected void EquipmentShop_Click(object sender, EventArgs e)
        {
            for (int ii = 0; ii < MAX_EQUIPLIST; ii++)
            {
                if (((Label)sender).Name == "equipList" + ii.ToString())
                {
                    ItemBackPack backpackData = new ItemBackPack(((Label)sender).Text);
                    if (!we.AvailableEquipShop5)
                    {
                        switch (backpackData.Name)
                        {
                            case "�V���[�g�\�[�h": // �K���c�̕���̔��i�_���W�����P�K�j
                                mainMessage.Text = "�K���c�F�����͕W���I�ȃV���[�g�\�[�h���ˁB�������ˁH";
                                break;
                            case "�������ꂽ�����O�\�[�h": // �K���c�̕���̔��i�_���W�����P�K�j
                                mainMessage.Text = "�K���c�F���ʂ̃����O�\�[�h�������@�X�^�ꂪ�����b���Ă���B�������ˁH";
                                break;
                            case "�`���җp�̍������т�": // �K���c�̕���̔��i�_���W�����P�K�j
                                mainMessage.Text = "�K���c�F�`���҂Ȃ�K���i�Ƃ�����h����ւ�B�������ˁH";
                                break;
                            case "���̊Z": // �K���c�̕���̔��i�_���W�����P�K�j
                                mainMessage.Text = "�K���c�F����Ȃ��̗Ǖi���Ȉ�i���B�������ˁH";
                                break;
                            case "�_��  �t�F���g�D�[�V��":
                                mainMessage.Text = "�K���c�F���@�X�^��̍ō����삾���A��q������߂Ă��܂����悤���B���܂Ȃ��B";
                                return;
                            case "���ׂȃp���[�����O": // �K���c�̕���̔��i�_���W�����P�K�j
                                mainMessage.Text = "�K���c�F�ڂ̕t�������ǂ��ȁB�������ˁH";
                                break;
                            case "���ɂ̃X�^�[�G���u����": // �K���c�̕���̔��i�_���W�����Q�K�j
                                mainMessage.Text = "�K���c�F�n���i�̎v���t�����̗p������i���B�������ˁH";
                                break;
                            case "�����o���h": // �K���c�̕���̔��i�_���W�����Q�K�j
                                mainMessage.Text = "�K���c�F���C���o���ɂ͂��̃o���h���œK���B�������ˁH";
                                break;
                            case "�E�F���j�b�P�̘r��": // �K���c�̕���̔��i�_���W�����R�K�j
                                mainMessage.Text = "�E�F���j�b�P�f�ނ��g�����ő̗͂̌����h�点���r�ւ��B�������ˁH";
                                break;
                            case "���҂̊ዾ": // �K���c�̕���̔��i�_���W�����R�K�j
                                mainMessage.Text = "���@�X�^�ꂪ�P���̎v�����ō�������j�[�N�Ȋዾ���B�������ˁH";
                                break;
                            case "�t�@���V�I��": // �K���c�̕���̔��i�_���W�����R�K�j
                                mainMessage.Text = "�ߋ��̕������Q�l�ɂ��č��グ�������B�������ˁH";
                                break;
                            case "�t�B�X�g�E�N���X": // �K���c�̕���̔��i�_���W�����R�K�j
                                mainMessage.Text = "�Ō��n���m�̑ł������ɓ����������߂��B�������ˁH";
                                break;

                            case "���̌�": // �K���c�̕���̔��i�_���W�����Q�K�j
                                mainMessage.Text = "���̌��͏d���ƈЗ͂��ǂ��o�����X�����B�������ˁH";
                                break;
                            case "���^���t�B�X�g": // �K���c�̕���̔��i�_���W�����Q�K�j
                                mainMessage.Text = "���^�����͎኱�d�����̂́A�����Έ����͗ǂ��͂��B�������ˁH";
                                break;
                            case "����̂���S�̃v���[�g": // �K���c�̕���̔��i�_���W�����Q�K�j
                                mainMessage.Text = "�S���̃v���[�g�ɃC�G���[�}�e���A����������ߍ��񂾁B�������ˁH";
                                break;
                            case "�V���N�̕�����": // �K���c�̕���̔��i�_���W�����Q�K�j
                                mainMessage.Text = "�V���N�������A�D���ڂ��L���ׂ������Ă�����Ȃ��̂ɂȂ��Ă���B�������ˁH";
                                break;

                            case "�v���`�i�\�[�h": // �K���c�̕���̔��i�_���W�����R�K�j
                                mainMessage.Text = "�v���`�i�f�ނŐ������������B�V���v�������B�������ˁH";
                                break;
                            case "�A�C�A���N���[": // �K���c�̕���̔��i�_���W�����R�K�j
                                mainMessage.Text = "�S���̒܂��B�V���v�������B�������ˁH";
                                break;
                            case "�V���o�[�A�[�}�[": // �K���c�̕���̔��i�_���W�����R�K�j
                                mainMessage.Text = "��f�ނ����������ߍ��ގ��őϋv�����������Z���B�������ˁH";
                                break;
                            case "�b�琻�̕�����": // �K���c�̕���̔��i�_���W�����R�K�j
                                mainMessage.Text = "�M���u�����h�푰�̔���g���Đ����������̂��B�������ˁH";
                                break;

                            case "���C�g�v���Y�}�u���[�h": // �K���c�̕���̔��i�_���W�����S�K�j
                                mainMessage.Text = "�C�G���[�ƃu���[�}�e���A�����ӂ񂾂�Ɏg�����u���[�h���B�������ˁH";
                                break;
                            case "�C�X���A���t�B�X�g": // �K���c�̕���̔��i�_���W�����S�K�j
                                mainMessage.Text = "�قړ����ŁA�d�������������Ȃ����З͂͊m���Ȃ��̂Ƃ����B�������ˁH";
                                break;
                            case "�v���Y�}�e�B�b�N�A�[�}�[": // �K���c�̕���̔��i�_���W�����S�K�j
                                mainMessage.Text = "�J���[�}�e���A��������������č쐬�������̂��B�������ˁH";
                                break;
                            case "�ɔ��������̉H��": // �K���c�̕���̔��i�_���W�����S�K�j
                                mainMessage.Text = "���̔����̍����Ɏd���Ă�̂͋�J������ꂽ�B�������ˁH";
                                break;

                            case "���F�v���Y���o���h": // �K���c�̕���̔��i�_���W�����S�K�j
                                mainMessage.Text = "�J���[�}�e���A������肭�g�ݍ��킹�č�����A�N�Z�T�����B�������ˁH";
                                break;
                            case "�Đ��̖��": // �K���c�̕���̔��i�_���W�����S�K�j
                                mainMessage.Text = "�E�F���j�b�P�̑f�ނ��ɏ��ɂ��āA���ߍ��񂾂��̂��B�������ˁH";
                                break;
                            case "�V�[���I�u�A�N�A���t�@�C�A": // �K���c�̕���̔��i�_���W�����S�K�j
                                mainMessage.Text = "�����ꕗ�ς���Ă��邾��B�������ˁH";
                                break;
                            case "�h���S���̃x���g": // �K���c�̕���̔��i�_���W�����S�K�j
                                mainMessage.Text = "�󏭉��l�̂���h���S���f�ނ��g�������̂��B�������ˁH";
                                break;


                            // ����ňȉ��̂��͔̂̔��\�肠��܂���B
                            case "�������ԃ|�[�V����":
                            case "���ʂ̐ԃ|�[�V����":
                            case "�傫�Ȑԃ|�[�V����":
                            case "����ԃ|�[�V����":
                            case "���؂Ȑԃ|�[�V����":
                            case "���O���ƂĂ��������ɂ͂܂��������ɗ������A���̌��ʂ��������Ȃ��𗧂����ł���ɂ�������炸�f�R���[�V���������������؂ȃX�[�p�[�~���N���|�[�V����":
                            case "�_����": // �Q�K�A�C�e��

                            case "���K�p�̌�": // �A�C����������
                            case "�i�b�N��": // ���i��������
                            case "����̌��i���v���J�j": // ���F���[��������
                            case "�V�����V�[��": // �R�K�A�C�e��
                            case "�G�X�p�_�X": // �_���W�����S�K�̃A�C�e��
                            case "���i�E�G�O�[�L���[�W���i�[": // �_���W�����T�K
                            case "�����E�X��ւ̒�": // �_���W�����T�K
                            case "�t�@�[�W���E�W�E�G�X�y�����U": // �_���W�����T�K
                            case "�o��  �W���m�Z���X�e":
                            case "�Ɍ�  �[�����M�A�X":
                            case "�N���m�X�E���}�e�B�b�h�E�\�[�h":

                            case "�R�[�g�E�I�u�E�v���[�g": // �A�C����������
                            case "���C�g�E�N���X": // ���i��������
                            case "���^��̊Z�i���v���J�j": // ���F���[��������
                            case "�^�J�̊Z": // �Q�K�A�C�e��
                            case "�v���[�g�E�A�[�}�[": // �R�K�A�C�e��
                            case "�������E�A�[�}�[": // �R�K�A�C�e��
                            case "�u���K���_�B��": // �_���W�����S�K�̃A�C�e��
                            case "�����J�E�Z�O�����^�[�^": // �_���W�����S�K�̃A�C�e��
                            case "�A���H�C�h�E�N���X": // �_���W�����S�K�̃A�C�e��
                            case "�\�[�h�E�I�u�E�u���[���[�W��": // �_���W�����S�K�̃A�C�e��
                            case "�w�p�C�X�g�X�E�p�i�b�T���C�j":

                            case "�X��̃u���X���b�g": // ���i��������
                            case "�V��̗��i���v���J�j": // ���F���[��������
                            case "�����V�g�̌아": // �P�K�A�C�e��
                            case "�`���N���I�[�u": // �P�K�A�C�e��
                            case "��̍���": // �Q�K�A�C�e��
                            case "�g���킵�̃}���g": // �Q�K�A�C�e��
                            case "���C�I���n�[�g": // �R�K�A�C�e��
                            case "�I�[�K�̘r��": // �R�K�A�C�e��
                            case "�|�S�̐Α�": // �R�K�A�C�e��
                            case "�t�@���l�M�̃V�[��": // �R�K�A�C�e��
                            case "����̓y���_���g": // ���i���x���A�b�v���ł��炦��A�C�e��
                            case "�����̈��": // �_���W�����S�K�̃A�C�e��
                            case "�V�g�̌_��": // �_���W�����S�K�̃A�C�e��
                            case "�G���~�E�W�����W���@�t�@�[�W�����Ƃ̍���":
                            case "�t�@���E�t���[���@�V�g�̃y���_���g":
                            case "�V�j�L�A�E�J�[���n���c�@�����f�r���A�C":
                            case "�I���E�����f�B�X�@���_�O���[�u":
                            case "���F���[�E�A�[�e�B�@�V��̗�":

                            case "�u���[�}�e���A��": // �P�K�A�C�e��
                            case "���b�h�}�e���A��": // �R�K�A�C�e��
                            case "�O���[���}�e���A��": // �_���W�����S�K�̃A�C�e��
                            case "���[�x�X�g�����N�|�[�V����":
                            case "�����@�C���|�[�V����":
                            case "�A�J�V�W�A�̎�":
                            case "�����̐���": // �������i��b�C�x���g�œ���A�C�e��
                            case "�I�[�o�[�V�t�e�B���O": // �_���W�����T�K
                            case "���W�F���h�E���b�h�z�[�X": // �_���W�����T�K
                            case "���i�̃C�������O": // �_���W�����T�K�i���i�̃C�x���g�j
                            case "�^�C���E�I�u�E���[�Z": // �_���W�����T�K�̉B���A�C�e��
                            default:
                                VendorBuyMessage(backpackData); // ��ҕҏW
                                break; // ��ҕҏW
                        }
                    }
                    else
                    {
                        if (backpackData.Name == "�_��  �t�F���g�D�[�V��")
                        {
                            mainMessage.Text = this.currentPlayer.GetCharacterSentence(3010);
                            return;
                        }

                        mainMessage.Text = String.Format(this.currentPlayer.GetCharacterSentence(3001), backpackData.Name, backpackData.Cost.ToString());
                    }

                    // [�x��] �w���葱���̃��W�b�N���Y��ł͂���܂���B�x�X�g�R�[�f�B���O��_���Ă��������B
                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                    {
                        yesno.Large = this.LayoutLarge; // ��Ғǉ�
                        yesno.Location = new Point(this.Location.X + YESNO_LOCATION_X, this.Location.Y + YESNO_LOCATION_Y); // ��ҕҏW
                        yesno.ShowDialog();
                        if (yesno.DialogResult == DialogResult.Yes)
                        {
                            if (mc.Gold < backpackData.Cost)
                            {
                                MessageExchange1(backpackData, mc); // ��ҕҏW
                            }
                            else
                            {
                                if (((currentPlayer == mc) && (backpackData.Type == ItemBackPack.ItemType.Armor_Heavy))
                                   || ((currentPlayer == mc) && (backpackData.Type == ItemBackPack.ItemType.Weapon_TwoHand)) // ��Ғǉ�
                                   || ((currentPlayer == mc) && (backpackData.Type == ItemBackPack.ItemType.Weapon_Heavy))
                                   || ((currentPlayer == sc) && (backpackData.Type == ItemBackPack.ItemType.Armor_Light))
                                   || ((currentPlayer == sc) && (backpackData.Type == ItemBackPack.ItemType.Weapon_Light))
                                   || ((currentPlayer == sc) && (backpackData.Type == ItemBackPack.ItemType.Weapon_Rod)) // ��Ғǉ�
                                   || ((currentPlayer == tc) && (backpackData.Type == ItemBackPack.ItemType.Armor_Middle))
                                   || ((currentPlayer == tc) && (backpackData.Type == ItemBackPack.ItemType.Weapon_Middle))
                                   || ((currentPlayer == tc) && (backpackData.Type == ItemBackPack.ItemType.Weapon_Rod)) // ��Ғǉ�
                                   || (backpackData.Type == ItemBackPack.ItemType.Accessory))
                                {
                                    // �����\�Ȃ��ߑ������邩�ǂ����A�₢���킹�B
                                    SetupMessageText(3011);
                                    using (YesNoRequestMini yesno2 = new YesNoRequestMini())
                                    {
                                        yesno2.Large = this.LayoutLarge; // ��Ғǉ�
                                        yesno2.Location = new Point(this.Location.X + YESNO_LOCATION_X, this.Location.Y + YESNO_LOCATION_Y); // ��ҕҏW
                                        yesno2.ShowDialog();
                                        if (yesno2.DialogResult == DialogResult.Yes)
                                        {
                                            // s ��Ғǉ�
                                            // ���ݑ��������p�\���ǂ������m�F
                                            if ((currentPlayer.MainWeapon.Name == Database.LEGENDARY_FELTUS) ||
                                                (currentPlayer.MainWeapon.Name == Database.POOR_PRACTICE_SWORD_1) ||
                                                (currentPlayer.MainWeapon.Name == Database.POOR_PRACTICE_SWORD_2) ||
                                                (currentPlayer.MainWeapon.Name == Database.COMMON_PRACTICE_SWORD_3) ||
                                                (currentPlayer.MainWeapon.Name == Database.COMMON_PRACTICE_SWORD_4) ||
                                                (currentPlayer.MainWeapon.Name == Database.RARE_PRACTICE_SWORD_5) ||
                                                (currentPlayer.MainWeapon.Name == Database.RARE_PRACTICE_SWORD_6) ||
                                                (currentPlayer.MainWeapon.Name == Database.EPIC_PRACTICE_SWORD_7))
                                            {
                                                bool success = this.currentPlayer.AddBackPack(backpackData);
                                                if (!success)
                                                {
                                                    // �A�C�e������t�̎��A����s�����B
                                                    MessageExchange2(); // ��ҕҏW
                                                    return;
                                                }
                                                else
                                                {
                                                    // �V�����A�C�e����ǉ����āA�x�����B��������B
                                                    mc.Gold -= backpackData.Cost;
                                                    label2.Text = mc.Gold.ToString() + "[G]"; // [�x��]�F�S�[���h�̏����͕ʃN���X�ɂ���ׂ��ł��B
                                                    UpdateBackPackLabel(this.currentPlayer);
                                                    MessageExchange3(); // ��ҕҏW
                                                    return;
                                                }
                                            }
                                            // e ��Ғǉ�
                                                 
                                            // ���ݑ����Ǝ�ւ��Ō��ݑ������p���邩�ǂ����A�₢���킹�B
                                            int cost = 0;
                                            if ((backpackData.Type == ItemBackPack.ItemType.Armor_Middle) || (backpackData.Type == ItemBackPack.ItemType.Armor_Light) || (backpackData.Type == ItemBackPack.ItemType.Armor_Heavy))
                                            {
                                                cost = currentPlayer.MainArmor.Cost / 2;
                                                SetupMessageText(3012, currentPlayer.MainArmor.Name, cost.ToString());
                                            }
                                            else if ((backpackData.Type == ItemBackPack.ItemType.Weapon_Rod) || (backpackData.Type == ItemBackPack.ItemType.Weapon_TwoHand) || (backpackData.Type == ItemBackPack.ItemType.Weapon_Heavy) || (backpackData.Type == ItemBackPack.ItemType.Weapon_Light) || (backpackData.Type == ItemBackPack.ItemType.Weapon_Middle)) // ��ҕҏW
                                            {
                                                cost = currentPlayer.MainWeapon.Cost / 2;
                                                SetupMessageText(3012, currentPlayer.MainWeapon.Name, cost.ToString());
                                            }
                                            else if (backpackData.Type == ItemBackPack.ItemType.Accessory)
                                            {
                                                cost = currentPlayer.Accessory.Cost / 2;
                                                SetupMessageText(3012, currentPlayer.Accessory.Name, cost.ToString());
                                            }
                                            using (YesNoRequestMini yesno3 = new YesNoRequestMini())
                                            {
                                                yesno3.Large = this.LayoutLarge; // ��Ғǉ�
                                                yesno3.Location = new Point(this.Location.X + YESNO_LOCATION_X, this.Location.Y + YESNO_LOCATION_Y); // ��ҕҏW
                                                yesno3.ShowDialog();
                                                if (yesno3.DialogResult == DialogResult.Yes)
                                                {
                                                    // ���ݑ����Ǝ�ւ������B�������z���v���X����B
                                                    mc.Gold += cost;
                                                    label2.Text = mc.Gold.ToString() + "[G]"; // [�x��]�F�S�[���h�̏����͕ʃN���X�ɂ���ׂ��ł��B
                                                }
                                                else
                                                {
                                                    // ���ݑ����Ǝ�ւ����Ȃ����߁A�ו��������ς��̏ꍇ�A����s�����Ƃ���B
                                                    if ((backpackData.Type == ItemBackPack.ItemType.Armor_Middle) || (backpackData.Type == ItemBackPack.ItemType.Armor_Light) || (backpackData.Type == ItemBackPack.ItemType.Armor_Heavy))
                                                    {
                                                        if (!currentPlayer.AddBackPack(currentPlayer.MainArmor))
                                                        {
                                                            MessageExchange2(); // ��ҕҏW
                                                            return;
                                                        }
                                                    }
                                                    else if ((backpackData.Type == ItemBackPack.ItemType.Weapon_Rod) || (backpackData.Type == ItemBackPack.ItemType.Weapon_TwoHand) || (backpackData.Type == ItemBackPack.ItemType.Weapon_Heavy) || (backpackData.Type == ItemBackPack.ItemType.Weapon_Light) || (backpackData.Type == ItemBackPack.ItemType.Weapon_Middle)) // ��ҕҏW
                                                    {
                                                        if (!currentPlayer.AddBackPack(currentPlayer.MainWeapon))
                                                        {
                                                            MessageExchange2(); // ��ҕҏW
                                                            return;
                                                        }
                                                    }
                                                    else if (backpackData.Type == ItemBackPack.ItemType.Accessory)
                                                    {
                                                        if (!currentPlayer.AddBackPack(currentPlayer.Accessory))
                                                        {
                                                            MessageExchange2(); // ��ҕҏW
                                                            return;
                                                        }
                                                    }
                                                    UpdateBackPackLabel(this.currentPlayer);
                                                }

                                                // s ��ҕҏW
                                                if ((backpackData.Type == ItemBackPack.ItemType.Weapon_Rod) || (backpackData.Type == ItemBackPack.ItemType.Weapon_TwoHand))
                                                {
                                                    cost = currentPlayer.SubWeapon.Cost / 2;
                                                    SetupMessageText(3012, currentPlayer.SubWeapon.Name, cost.ToString());

                                                    using (YesNoRequestMini yesno4 = new YesNoRequestMini())
                                                    {
 							yesno4.Large = this.LayoutLarge; // ��Ғǉ�
                                                        yesno4.Location = new Point(this.Location.X + YESNO_LOCATION_X, this.Location.Y + YESNO_LOCATION_Y);
                                                        yesno4.ShowDialog();
                                                        if (yesno4.DialogResult == DialogResult.Yes)
                                                        {
                                                            // ���ݑ����Ǝ�ւ������B�������z���v���X����B
                                                            mc.Gold += cost;
                                                            label2.Text = mc.Gold.ToString() + "[G]"; // [�x��]�F�S�[���h�̏����͕ʃN���X�ɂ���ׂ��ł��B
                                                        }
                                                        else
                                                        {
                                                            MessageExchange7(backpackData.Name);
                                                            Method.AddItemBank(we, backpackData.Name);
                                                            //MessageExchange2(); // ��ҕҏW
                                                            //return;
                                                            UpdateBackPackLabel(this.currentPlayer);
                                                            OKRequest ok = new OKRequest();
                                                            ok.StartPosition = FormStartPosition.Manual;
                                                            ok.Location = new Point(this.Location.X + OK_LOCATION_X, this.Location.Y + OK_LOCATION_Y); // ��ҕҏW
                                                            ok.ShowDialog();
                                                        }
                                                    }
                                                }
                                                // e ��ҕҏW

                                                // �V�����A�C�e���𑕔������āA�x�������s���A��������B
                                                if ((backpackData.Type == ItemBackPack.ItemType.Armor_Middle) || (backpackData.Type == ItemBackPack.ItemType.Armor_Light) || (backpackData.Type == ItemBackPack.ItemType.Armor_Heavy))
                                                {
                                                    currentPlayer.MainArmor = backpackData;
                                                }
                                                else if ((backpackData.Type == ItemBackPack.ItemType.Weapon_Rod) || (backpackData.Type == ItemBackPack.ItemType.Weapon_TwoHand) || (backpackData.Type == ItemBackPack.ItemType.Weapon_Heavy) || (backpackData.Type == ItemBackPack.ItemType.Weapon_Light) || (backpackData.Type == ItemBackPack.ItemType.Weapon_Middle)) // ��ҕҏW
                                                {
                                                    currentPlayer.MainWeapon = backpackData;
                                                    if ((backpackData.Type == ItemBackPack.ItemType.Weapon_Rod) || (backpackData.Type == ItemBackPack.ItemType.Weapon_TwoHand))
                                                    {
                                                        currentPlayer.SubWeapon = new ItemBackPack("");
                                                    }
                                                }
                                                else if (backpackData.Type == ItemBackPack.ItemType.Accessory)
                                                {
                                                    currentPlayer.Accessory = backpackData;
                                                }
                                                mc.Gold -= backpackData.Cost;
                                                label2.Text = mc.Gold.ToString() + "[G]";
                                                MessageExchange3(); // ��ҕҏW
                                            }
                                        }
                                        else
                                        {
                                            // �����������A�V�����A�C�e�����w���B
                                            bool success = this.currentPlayer.AddBackPack(backpackData);
                                            if (!success)
                                            {
                                                // �A�C�e������t�̎��A����s�����B
                                                MessageExchange2(); // ��ҕҏW
                                            }
                                            else
                                            {
                                                // �V�����A�C�e����ǉ����āA�x�����B��������B
                                                mc.Gold -= backpackData.Cost;
                                                label2.Text = mc.Gold.ToString() + "[G]"; // [�x��]�F�S�[���h�̏����͕ʃN���X�ɂ���ׂ��ł��B
                                                UpdateBackPackLabel(this.currentPlayer);
                                                MessageExchange3(); // ��ҕҏW
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (mc.Gold >= backpackData.Cost)
                                    {
                                        bool success = this.currentPlayer.AddBackPack(backpackData);
                                        if (!success)
                                        {
                                            MessageExchange2(); // ��ҕҏW
                                        }
                                        else
                                        {
                                            mc.Gold -= backpackData.Cost;
                                            label2.Text = mc.Gold.ToString() + "[G]"; // [�x��]�F�S�[���h�̏����͕ʃN���X�ɂ���ׂ��ł��B
                                            UpdateBackPackLabel(this.currentPlayer);
                                            MessageExchange3(); // ��ҕҏW
                                        }
                                    }
                                    else
                                    {
                                        MessageExchange1(backpackData, mc); // ��ҕҏW
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageExchange4(); // ��ҕҏW
                        }
                    }
                    return;
                }
            }

            for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
            {
                int stack = 1; // ��Ғǉ�

                if (((Label)sender).Name == "backpackList" + ii.ToString())
                {
                    ItemBackPack backpackData = new ItemBackPack(((Label)sender).Text);
                    switch (backpackData.Name)
                    {
                        // [�R�����g]�F���ʂȃA�C�e���̏ꍇ�ʉ�b���J��L���Ă��������B
                        // s ���
                        case Database.EPIC_OLD_TREE_MIKI_DANPEN:
                            if (!we.GanzGift1)
                            {
                                UpdateMainMessage("�K���c�F�E�E�E�A�C����A�ǂ����������Ă����ȁB");

                                UpdateMainMessage("�A�C���F��������A����͈�́H");

                                UpdateMainMessage("�K���c�F���̑嗤�̗y���k�ɂ���R���E�F�N�X���[�ɂ́A���ČÑ�h���������Ă������̂��B");

                                UpdateMainMessage("�A�C���F�Ñ�h���H�H�@�`����̂��Ƃ��b����Ȃ���ł����H");

                                UpdateMainMessage("�K���c�F���ł͂��Ƃ��b�Ƃ��ē`�����Ă���̂́A�������B");

                                UpdateMainMessage("�K���c�F�Ñ�h���͈�U���̎p�����ł�����A�S���V�����ꏊ�ōĐ����s����B");

                                UpdateMainMessage("�K���c�F���̘b���̂͐^���ł͂��邪�A�����M����҂͂��̍��̎���ł͐����Ȃ��낤�B");

                                UpdateMainMessage("�A�C���F���ŁA���̃S�c�S�c�����؂̊��̈ꕔ�݂����Ȃ̂��E�E�E�H");

                                UpdateMainMessage("�K���c�F�����A���ꂱ���܂������Ñ�h���؂̊��̒f�ЁB�悭����ɓ��ꂽ�B");

                                UpdateMainMessage("�K���c�F�A�C����A���܂񂪂�������V�ɑ����Ă��炦�񂩂ˁH");

                                UpdateMainMessage("�A�C���F�����I�H");

                                UpdateMainMessage("���i�F��������ƁA�����̃o�J�A�C���B���l���Ă�̂�H");

                                UpdateMainMessage("�A�C���F�����A���₢�₢��B�ʂɉ����l���Ă˂����A�b�n�b�n�b�n�I");

                                UpdateMainMessage("���i�F�Ӂ`��A�Ȃ炢���񂾂��ǁ�");

                                UpdateMainMessage("�A�C���F�ƁA���R����I�H�@�����܂������͍l���Ă˂����I");

                                UpdateMainMessage("���i�F�b�t�t�A�������Ȃ��Ă��ǂ��̂Ɂ�");

                                UpdateMainMessage("�A�C���F�����I�������E�E�E�܂��ǂ����B");

                                UpdateMainMessage("�A�C���F����A���ł��˂��񂾁B��������A�󂯎���Ă���B");

                                UpdateMainMessage("�K���c�F�S�����A���ӂ���B");

                                UpdateMainMessage("�K���c�F���̑f�ނ��g���āA����V�Ȃ�̍ō����������Ă݂��悤�B");

                                UpdateMainMessage("�A�C���F�b�}�W�ŁI�I�I");

                                UpdateMainMessage("�K���c�F�񌾂͂Ȃ��B");

                                UpdateMainMessage("�K���c�F�o���オ������A�����炩��A������B�y���݂ɂ��Ă���B");

                                UpdateMainMessage("�A�C���F������I�@�������A�y���݂����I�I");

                                we.GanzGift1 = true;
                                SellBackPackItem(backpackData, ((Label)sender), stack, ii);
                                return;
                            }
                            break;
                        // e ���
                        case "�^�C���E�I�u�E���[�Z":
                            if (!we.AvailableEquipShop5)
                            {
                                OKRequest ok = new OKRequest();
                                ok.StartPosition = FormStartPosition.Manual;
                                ok.Location = new Point(this.Location.X + OK_LOCATION_X, this.Location.Y + OK_LOCATION_Y); // ��ҕҏW
                                mainMessage.Text = "�A�C���F�K���c�f������A����͂����炮�炢���H";
                                mainMessage.Update();
                                ok.ShowDialog();
                                mainMessage.Text = "�K���c�F��H�����A�ǂꌩ���Ă݂Ȃ����B";
                                mainMessage.Update();
                                ok.ShowDialog();
                                mainMessage.Text = "�K���c�F�Ȃ�ƁI�I�@�A�C���A����͂ǂ��Ō������H";
                                mainMessage.Update();
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�ŉ��w�̈꒼���̏��̎�O�Ŗ��ɐF���Ⴄ�ǂ��������񂾁B";
                                mainMessage.Update();
                                ok.ShowDialog();
                                mainMessage.Text = "�K���c�F�������E�E�E�債�����̂��B�ł��������A�C���B";
                                mainMessage.Update();
                                ok.ShowDialog();
                                mainMessage.Text = "�K���c�F���܂񂪁A��������V�ɑ����Ă��炦�Ȃ����ˁH";
                                mainMessage.Update();
                                ok.ShowDialog();
                                mainMessage.Text = "���i�F�A�C���A�f�����񂪂����������������Ă�񂾂���A�n���Ă����΁H";
                                mainMessage.Update();
                                ok.ShowDialog();
                                mainMessage.Text = "�A�C���F�������Ȃ��E�E�E�n���Ă��܂����H";
                                mainMessage.Update();
                                using (YesNoRequestMini ynr = new YesNoRequestMini())
                                {
                                    ynr.Large = this.LayoutLarge; // ��Ғǉ�
                                    ynr.Location = new Point(this.Location.X + YESNO_LOCATION_X, this.Location.Y + YESNO_LOCATION_X); // ��ҕҏW
                                    ynr.ShowDialog();
                                    if (ynr.DialogResult == DialogResult.Yes)
                                    {
                                        mainMessage.Text = "�A�C���F�܂��������������Ă��Ă����Ɏg���邩�S�R������˂����ȁB";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "�A�C���F�f������A�󂯎���Ă���I";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "�K���c�F�A�C����A���ɂ���B";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "���i�F���ꉽ�Ɏg�����̂Ȃ�ł����H";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "�K���c�F����`���̕�����쐬���邽�߂̃��m�ƌ����Ă���B";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "���i�F�_�̎���Y�Ƃ͈Ⴄ��ł����H";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "�K���c�F�_�̎���Y�Ƃ͈Ⴄ���̂��ƃ��@�X�^��͌����Ă������B";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "�K���c�F���V�͂��́w�^�C���E�I�u�E���[�Z�x�������ă��@�X�^��ɉ�ɍs�����Ǝv���B";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "�A�C���F���@�X�^�ꂳ�񂪉����m���Ă���Ă����̂��H";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "�K���c�F�����A�������B���V��̊Ԃł̐̂���̖񑩂��B";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "�A�C���F�������A�ǂ���������˂����I�񑩉ʂ��������ŁI";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "�K���c�F�����A�{���Ɋ��ӂ��邼�A�A�C����B";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        this.currentPlayer.DeleteBackPack(backpackData);
                                        ((Label)sender).Text = "";
                                        ((Label)sender).Cursor = System.Windows.Forms.Cursors.Default;
                                        WE.SpecialTreasure1 = true;
                                        return;
                                    }
                                    else
                                    {
                                        mainMessage.Text = "�A�C���F�f������E�E�E������Ă����炮�炢���H";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "���i�F�A�C���I�@������Ƃ���͖����񂶂�Ȃ��I�H";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "�K���c�F�l�i�͕t���悤�������A���܂Ȃ����������Ƃ����̂͏o����B";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "���i�F������ƃA�C���A�f���ɓn���Ă����Ă�H";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        while (true)
                                        {
                                            using (YesNoRequestMini ynr2 = new YesNoRequestMini())
                                            {
                                                ynr2.Large = this.LayoutLarge; // ��Ғǉ�
                                                ynr2.Location = new Point(this.Location.X + YESNO_LOCATION_X, this.Location.Y + YESNO_LOCATION_Y); // ��ҕҏW
                                                ynr2.ShowDialog();
                                                if (ynr2.DialogResult == DialogResult.Yes)
                                                {
                                                    mainMessage.Text = "�A�C���F�܂��������������Ă��Ă����Ɏg���邩�S�R������˂����ȁB";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F�f������A�󂯎���Ă���I";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�K���c�F�A�C����A���ɂ���B";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F���ꉽ�Ɏg�����̂Ȃ�ł����H";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�K���c�F����`���̕�����쐬���邽�߂̃��m�ƌ����Ă���B";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F�_�̎���Y�Ƃ͈Ⴄ��ł����H";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�K���c�F�_�̎���Y�Ƃ͈Ⴄ���̂��ƃ��@�X�^��͌����Ă������B";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�K���c�F���V�͂��́w�^�C���E�I�u�E���[�Z�x�������ă��@�X�^��ɉ�ɍs�����Ǝv���B";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F���@�X�^�ꂳ�񂪉����m���Ă���Ă����̂��H";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�K���c�F�����A�������B���V��̊Ԃł̐̂���̖񑩂��B";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F�������A�ǂ���������˂����I�񑩉ʂ��������ŁI";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�K���c�F�����A�{���Ɋ��ӂ��邼�A�A�C����B";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    this.currentPlayer.DeleteBackPack(backpackData);
                                                    ((Label)sender).Text = "";
                                                    ((Label)sender).Cursor = System.Windows.Forms.Cursors.Default;
                                                    WE.SpecialTreasure1 = true;
                                                    return;
                                                }
                                                else
                                                {
                                                    mainMessage.Text = "�A�C���F�f������E�E�E������āE�E�E�E������E�E�E";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F�W�B�[�[�[�E�E�E�i�����ځj";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    continue;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageExchange5(); // ��ҕҏW
                                return;
                            }

                        // [�R�����g]�F���ʂȃA�C�e���̏ꍇ�ʉ�b���J��L���Ă��������B
                        case "����̓y���_���g": // ���i���x���A�b�v���ł��炦��A�C�e��
                            SetupMessageText(3008, (backpackData.Cost / 2).ToString());
                            break;


                        default:
                            if (backpackData.Cost <= 0)
                            {
                                MessageExchange5(); // ��ҕҏW
                                return;
                            }
                            // s ��Ғǉ�
                            else if ((backpackData.Name == Database.LEGENDARY_FELTUS) ||
                                     (backpackData.Name == Database.POOR_PRACTICE_SWORD_1) ||
                                     (backpackData.Name == Database.POOR_PRACTICE_SWORD_2) ||
                                     (backpackData.Name == Database.COMMON_PRACTICE_SWORD_3) ||
                                     (backpackData.Name == Database.COMMON_PRACTICE_SWORD_4) ||
                                     (backpackData.Name == Database.RARE_PRACTICE_SWORD_5) ||
                                     (backpackData.Name == Database.RARE_PRACTICE_SWORD_6) ||
                                     (backpackData.Name == Database.EPIC_PRACTICE_SWORD_7))
                            {
                                MessageExchange5();
                                return;
                            }
                            // e ��Ғǉ�
                            else
                            {
                                // s ��ҕҏW
                                stack = SelectSellStackValue(sender, e, backpackData, ii);
                                if (stack == -1) return; // �����ʎw��̎��AESC�L�����Z���Ͱ1�Ŕ����Ă���̂ŁA����Return

                                MessageExchange6(backpackData, stack, ii);
                            }
                            break;
                    }
                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                    {
                        yesno.Large = this.LayoutLarge; // ��Ғǉ�
                        yesno.Location = new Point(this.Location.X + YESNO_LOCATION_X, this.Location.Y + YESNO_LOCATION_Y); // ��ҕҏW
                        yesno.ShowDialog();
                        if (yesno.DialogResult == DialogResult.Yes)
                        {
                            mc.Gold += stack * backpackData.Cost / 2; // ��ҕҏW
                            label2.Text = mc.Gold.ToString() + "[G]"; // [�x��]�F�S�[���h�̏����͕ʃN���X�ɂ���ׂ��ł��B
                            // this.currentPlayer.DeleteBackPack(backpackData);
                            SellBackPackItem(backpackData, ((Label)sender), stack, ii);
                            MessageExchange3(); // ��ҕҏW
                        }
                        else
                        {
                            MessageExchange4(); // ��ҕҏW
                        }
                    }
                    return;
                }
            }
        }

        OKRequest messageOK = null;
        protected void UpdateMainMessage(string message)
        {
            if (messageOK == null)
            {
                messageOK = new OKRequest();
                messageOK.StartPosition = FormStartPosition.Manual;
                messageOK.Location = new Point(this.Location.X + OK_LOCATION_X, this.Location.Y + OK_LOCATION_Y); // ��ҕҏW
            }
            mainMessage.Text = message;
            mainMessage.Update();
            messageOK.ShowDialog();
        }

        // s ��ҕҏW
        protected virtual int SelectSellStackValue(object sender, EventArgs e, ItemBackPack backpackData, int ii)
        {
            // �O�҂ł͉������Ȃ����A��҂ł͐��ʎw��̔��p�����邽�߁A��̃C���^�t�F�[�X���\�z���Ă����B
            return 1;
        }

        protected virtual void MessageExchange7(string itemName)
        {
            SetupMessageText(3013, itemName);
        }

        protected virtual void MessageExchange6(ItemBackPack backpackData, int stack, int ii)
        {
            SetupMessageText(3007, backpackData.Name, (backpackData.Cost / 2).ToString());
        }

        protected virtual void MessageExchange5()
        {
            SetupMessageText(3006);
        }

        protected virtual void MessageExchange4()
        {
            SetupMessageText(3005);
        }

        protected virtual void MessageExchange3()
        {
            SetupMessageText(3003);
        }

        protected virtual void MessageExchange2()
        {
            SetupMessageText(3002);
        }

        protected virtual void MessageExchange1(ItemBackPack backpackData, MainCharacter player)
        {
            SetupMessageText(3004, Convert.ToString((backpackData.Cost - player.Gold)));
        }

        protected virtual void VendorBuyMessage(ItemBackPack backpackData)
        {
            mainMessage.Text = String.Format(ganz.GetCharacterSentence(3001), backpackData.Name, backpackData.Cost.ToString()); // ��ҕҏW
        }
        // e ��ҕҏW

        protected virtual void SellBackPackItem(ItemBackPack backpackData, Label sender, int stack, int ii)
        {
            this.currentPlayer.DeleteBackPack(backpackData, stack);
            ((Label)sender).Text = "";
            ((Label)sender).Cursor = System.Windows.Forms.Cursors.Default;
        }

        // s ��ҕҏW
        protected virtual void button1_Click(object sender, EventArgs e)
        {
            OnButton1_ClickMessage();
            mainMessage.Update();
            System.Threading.Thread.Sleep(1000);
            this.Close();
        }
        protected virtual void OnButton1_ClickMessage()
        {
            SetupMessageText(3009);
        }
        // e ��ҕҏW

        protected virtual void UpdateBackPackLabel(MainCharacter target) // ��ҕҏW
        {
            label11.Text = "�o�b�N�p�b�N�i" + target.Name + ")";

            UpdateBackPackLabelInterface(target);
        }

        protected virtual void UpdateBackPackLabelInterface(MainCharacter target)
        {
            ItemBackPack[] temp = target.GetBackPackInfo();

            int ii = 0;
            try
            {
                for (ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
                {
                    if (temp[ii] != null)
                    {
                        backpackList[ii].Text = temp[ii].Name;
                        backpackList[ii].Cursor = System.Windows.Forms.Cursors.Hand;
                        switch (temp[ii].Rare)
                        {
                            case ItemBackPack.RareLevel.Poor:
                                backpackList[ii].BackColor = Color.Gray;
                                backpackList[ii].ForeColor = Color.White;
                                break;
                            case ItemBackPack.RareLevel.Common:
                                backpackList[ii].BackColor = Color.Green;
                                backpackList[ii].ForeColor = Color.White;
                                break;
                            case ItemBackPack.RareLevel.Rare:
                                backpackList[ii].BackColor = Color.DarkBlue;
                                backpackList[ii].ForeColor = Color.White;
                                break;
                            case ItemBackPack.RareLevel.Epic:
                                backpackList[ii].BackColor = Color.Purple;
                                backpackList[ii].ForeColor = Color.White;
                                break;
                            case ItemBackPack.RareLevel.Legendary: // ��Ғǉ�
                                backpackList[ii].BackColor = Color.OrangeRed;
                                backpackList[ii].ForeColor = Color.White;
                                break;
                        }

                    }
                    else
                    {
                        backpackList[ii].Text = "";
                        backpackList[ii].Cursor = System.Windows.Forms.Cursors.Default;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("ii: " + ii.ToString());
            }
        }

        protected void btnLevel1_Click(object sender, EventArgs e)
        {
            SetupAvailableList(1);
        }

        protected void btnLevel2_Click(object sender, EventArgs e)
        {
            SetupAvailableList(2);
        }

        protected void btnLevel3_Click(object sender, EventArgs e)
        {
            SetupAvailableList(3);
        }

        protected void btnLevel4_Click(object sender, EventArgs e)
        {
            SetupAvailableList(4);
        }

        protected void btnLevel5_Click(object sender, EventArgs e)
        {
            SetupAvailableList(5);
        }

        protected void UpdateRareColor(ItemBackPack item, Label target)
        {
            switch (item.Rare)
            {
                case ItemBackPack.RareLevel.Poor:
                    target.BackColor = Color.Gray;
                    target.ForeColor = Color.White;
                    break;
                case ItemBackPack.RareLevel.Common:
                    target.BackColor = Color.Green;
                    target.ForeColor = Color.White;
                    break;
                case ItemBackPack.RareLevel.Rare:
                    target.BackColor = Color.DarkBlue;
                    target.ForeColor = Color.White;
                    break;
                case ItemBackPack.RareLevel.Epic:
                    target.BackColor = Color.Purple;
                    target.ForeColor = Color.White;
                    break;
                case ItemBackPack.RareLevel.Legendary: // ��Ғǉ�
                    target.BackColor = Color.OrangeRed;
                    target.ForeColor = Color.White;
                    break;
            }
        }

        protected virtual void SetupAvailableList(int level) // c ��ҕҏW
        {
            ItemBackPack item = null;
            switch (level)
            {
                case 1:
                    item = new ItemBackPack("�V���[�g�\�[�h");
                    equipList[0].Text = item.Name;
                    UpdateRareColor(item, equipList[0]);

                    item = new ItemBackPack("�������ꂽ�����O�\�[�h");
                    equipList[1].Text = item.Name;
                    UpdateRareColor(item, equipList[1]);

                    item = new ItemBackPack("�`���җp�̍������т�");
                    equipList[2].Text = item.Name;
                    UpdateRareColor(item, equipList[2]);

                    item = new ItemBackPack("���̊Z");
                    equipList[3].Text = item.Name;
                    UpdateRareColor(item, equipList[3]);

                    item = new ItemBackPack("���ׂȃp���[�����O");
                    equipList[4].Text = item.Name;
                    UpdateRareColor(item, equipList[4]);

                    item = new ItemBackPack("�_��  �t�F���g�D�[�V��");
                    equipList[5].Text = item.Name;
                    equipList[5].Font = new Font("MS UI Gothic", 10F, FontStyle.Strikeout);
                    costList[5].Font = new Font("MS UI Gothic", 10F, FontStyle.Strikeout);
                    UpdateRareColor(item, equipList[5]);

                    equipList[6].Text = "";

                    equipList[7].Text = "";
                    break;

                case 2:
                    item = new ItemBackPack("���̌�");
                    equipList[0].Text = item.Name;
                    UpdateRareColor(item, equipList[0]);

                    item = new ItemBackPack("���^���t�B�X�g");
                    equipList[1].Text = item.Name;
                    UpdateRareColor(item, equipList[1]);

                    item = new ItemBackPack("����̂���S�̃v���[�g");
                    equipList[2].Text = item.Name;
                    UpdateRareColor(item, equipList[2]);

                    item = new ItemBackPack("�V���N�̕�����");
                    equipList[3].Text = item.Name;
                    UpdateRareColor(item, equipList[3]);

                    item = new ItemBackPack("���ɂ̃X�^�[�G���u����");
                    equipList[4].Text = item.Name;
                    UpdateRareColor(item, equipList[4]);

                    item = new ItemBackPack("�����o���h");
                    equipList[5].Text = item.Name;
                    equipList[5].Font = new Font("MS UI Gothic", 10F, FontStyle.Underline);
                    costList[5].Font = new Font("MS UI Gothic", 10F, FontStyle.Bold);
                    UpdateRareColor(item, equipList[5]);

                    equipList[6].Text = "";

                    equipList[7].Text = "";
                    break;

                case 3:
                    item = new ItemBackPack("�v���`�i�\�[�h");
                    equipList[0].Text = item.Name;
                    UpdateRareColor(item, equipList[0]);

                    item = new ItemBackPack("�A�C�A���N���[");
                    equipList[1].Text = item.Name;
                    UpdateRareColor(item, equipList[1]);

                    item = new ItemBackPack("�V���o�[�A�[�}�[");
                    equipList[2].Text = item.Name;
                    UpdateRareColor(item, equipList[2]);

                    item = new ItemBackPack("�b�琻�̕�����");
                    equipList[3].Text = item.Name;
                    UpdateRareColor(item, equipList[3]);

                    item = new ItemBackPack("�E�F���j�b�P�̘r��");
                    equipList[4].Text = item.Name;
                    UpdateRareColor(item, equipList[4]);

                    item = new ItemBackPack("���҂̊ዾ");
                    equipList[5].Text = item.Name;
                    equipList[5].Font = new Font("MS UI Gothic", 10F, FontStyle.Underline);
                    costList[5].Font = new Font("MS UI Gothic", 10F, FontStyle.Bold);
                    UpdateRareColor(item, equipList[5]);

                    item = new ItemBackPack("�t�@���V�I��");
                    equipList[6].Text = item.Name;
                    UpdateRareColor(item, equipList[6]);

                    item = new ItemBackPack("�t�B�X�g�E�N���X");
                    equipList[7].Text = item.Name;
                    UpdateRareColor(item, equipList[7]);
                    break;

                case 4:
                    item = new ItemBackPack("�v���Y�}�e�B�b�N�A�[�}�[");
                    equipList[0].Text = item.Name;
                    UpdateRareColor(item, equipList[0]);

                    item = new ItemBackPack("�ɔ��������̉H��");
                    equipList[1].Text = item.Name;
                    UpdateRareColor(item, equipList[1]);

                    item = new ItemBackPack("���C�g�v���Y�}�u���[�h");
                    equipList[2].Text = item.Name;
                    UpdateRareColor(item, equipList[2]);

                    item = new ItemBackPack("�C�X���A���t�B�X�g");
                    equipList[3].Text = item.Name;
                    UpdateRareColor(item, equipList[3]);

                    item = new ItemBackPack("���F�v���Y���o���h");
                    equipList[4].Text = item.Name;
                    UpdateRareColor(item, equipList[4]);

                    item = new ItemBackPack("�Đ��̖��");
                    equipList[5].Text = item.Name;
                    equipList[5].Font = new Font("MS UI Gothic", 10F, FontStyle.Underline);
                    costList[5].Font = new Font("MS UI Gothic", 10F, FontStyle.Bold);
                    UpdateRareColor(item, equipList[5]);

                    item = new ItemBackPack("�V�[���I�u�A�N�A���t�@�C�A");
                    equipList[6].Text = item.Name;
                    UpdateRareColor(item, equipList[6]);

                    item = new ItemBackPack("�h���S���̃x���g");
                    equipList[7].Text = item.Name;
                    UpdateRareColor(item, equipList[7]);
                    break;

                case 5:
                    item = new ItemBackPack("�v���C�h�E�I�u�E�V�[�J�[");
                    equipList[0].Text = item.Name;
                    UpdateRareColor(item, equipList[0]);

                    item = new ItemBackPack("��������̌���");
                    equipList[1].Text = item.Name;
                    UpdateRareColor(item, equipList[1]);

                    item = new ItemBackPack("�f�B�Z���V�����u�[�c");
                    equipList[2].Text = item.Name;
                    UpdateRareColor(item, equipList[2]);

                    item = new ItemBackPack("�n�[�g�u���[�J�[");
                    equipList[3].Text = item.Name;
                    UpdateRareColor(item, equipList[3]);

                    equipList[4].Text = "";

                    equipList[5].Text = "";

                    equipList[6].Text = "";

                    equipList[7].Text = "";
                    break;
            }
            ItemBackPack temp0 = new ItemBackPack(equipList[0].Text);
            costList[0].Text = temp0.Cost.ToString();
            ItemBackPack temp1 = new ItemBackPack(equipList[1].Text);
            costList[1].Text = temp1.Cost.ToString();
            ItemBackPack temp2 = new ItemBackPack(equipList[2].Text);
            costList[2].Text = temp2.Cost.ToString();
            ItemBackPack temp3 = new ItemBackPack(equipList[3].Text);
            costList[3].Text = temp3.Cost.ToString();
            if (equipList[4].Text != "")
            {
                ItemBackPack temp4 = new ItemBackPack(equipList[4].Text);
                costList[4].Text = temp4.Cost.ToString();
            }
            else
            {
                costList[4].Text = "";
            }
            if (equipList[5].Text != "")
            {
                ItemBackPack temp5 = new ItemBackPack(equipList[5].Text);
                costList[5].Text = temp5.Cost.ToString();
            }
            else
            {
                costList[5].Text = "";
            }
            if (equipList[6].Text != "")
            {
                ItemBackPack temp6 = new ItemBackPack(equipList[6].Text);
                costList[6].Text = temp6.Cost.ToString();
            }
            else
            {
                costList[6].Text = "";
            }
            if (equipList[7].Text != "")
            {
                ItemBackPack temp7 = new ItemBackPack(equipList[7].Text);
                costList[7].Text = temp7.Cost.ToString();
            }
            else
            {
                costList[7].Text = "";
            }

        }

        protected void btnBackpack1_Click(object sender, EventArgs e)
        {
            this.currentPlayer = this.mc;
            UpdateBackPackLabel(this.currentPlayer);
        }

        protected void btnBackpack2_Click(object sender, EventArgs e)
        {
            this.currentPlayer = this.sc;
            UpdateBackPackLabel(this.currentPlayer);
        }

        protected void btnBackpack3_Click(object sender, EventArgs e)
        {
            this.currentPlayer = this.tc;
            UpdateBackPackLabel(this.currentPlayer);
        }

        protected virtual void EquipmentShop_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                button1_Click(null, null);
            }
        }

        // s ��Ғǉ�
        protected virtual void EquipmentShop_Shown(object sender, EventArgs e)
        {
            // �O�҂ł͉������Ȃ����A��҂ŐV�K�A�C�e���ǉ������邽�߁A��̃C���^�t�F�[�X���\�z���Ă����B
        }
        // e ��Ғǉ�
    }
}