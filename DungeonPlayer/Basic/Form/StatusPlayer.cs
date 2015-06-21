using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace DungeonPlayer
{
    public partial class StatusPlayer : MotherForm
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

        private Color currentStatusView = Color.LightSkyBlue;
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

        private System.Windows.Forms.Label[] backpack;

        private bool useOverShifting = false;

        public StatusPlayer()
        {
            InitializeComponent();
            backpack = new Label[Database.MAX_BACKPACK_SIZE];
            for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
            {
                backpack[ii] = new Label();
                backpack[ii].Font = new System.Drawing.Font("MS UI Gothic", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                backpack[ii].Location = new System.Drawing.Point(407, 116 + 31 * ii);
                backpack[ii].Name = "backpack" + ii.ToString();
                backpack[ii].AutoSize = true;
                backpack[ii].TabIndex = 0;
                backpack[ii].MouseEnter += new EventHandler(StatusPlayer_MouseEnter);
                backpack[ii].MouseDown += new MouseEventHandler(StatusPlayer_MouseDown);
                backpack[ii].MouseLeave += new EventHandler(StatusPlayer_MouseLeave);
                backpack[ii].Click += new EventHandler(StatusPlayer_Click);
                this.Controls.Add(backpack[ii]);
            }

            this.pbSortByUsed.Image = Image.FromFile(Database.BaseResourceFolder + "SortByUsed.bmp");
            this.pbSortByAccessory.Image = Image.FromFile(Database.BaseResourceFolder + "SortByAccessory.bmp");
            this.pbSortByWeapon.Image = Image.FromFile(Database.BaseResourceFolder + "SortByWeapon.bmp");
            this.pbSortByArmor.Image = Image.FromFile(Database.BaseResourceFolder + "SortByArmor.bmp");
            this.pbSortByName.Image = Image.FromFile(Database.BaseResourceFolder + "SortByName.bmp");
            this.pbSortByRare.Image = Image.FromFile(Database.BaseResourceFolder + "SortByRare.bmp");
        }

        void StatusPlayer_MouseLeave(object sender, EventArgs e)
        {
            if (levelUp)
            {
                mainMessage.Text = "���x���A�b�v�I�I" + upPoint.ToString() + "�|�C���g������U���Ă��������B";
            }
            else if (this.useOverShifting)
            {
                mainMessage.Text = "�I�[�o�[�V�t�e�B���O�g�p���A" + upPoint.ToString() + "�|�C���g������U���Ă��������B";
            }
            else
            {
                //mainMessage.Text = "";
            }
        }
        
        private void StatusPlayer_Load(object sender, EventArgs e)
        {
            if (we.AvailableItemSort)
            {
                pbSortByAccessory.Visible = true;
                pbSortByArmor.Visible = true;
                pbSortByName.Visible = true;
                pbSortByRare.Visible = true;
                pbSortByUsed.Visible = true;
                pbSortByWeapon.Visible = true;
            }
            this.gold.Text = mc.Gold.ToString() + "[G]"; // [�x��]�F�S�[���h�̏����͕ʃN���X�ɂ���ׂ��ł��B

            this.BackColor = currentStatusView;
            if (this.BackColor == Color.LightSkyBlue)
            {
                if (mc != null)
                {
                    SettingCharacterData(mc);
                }
            }
            else if (this.BackColor == Color.Pink)
            {
                if (sc != null)
                {
                    SettingCharacterData(sc);
                }
            }
            else if (this.BackColor == Color.Silver)
            {
                if (tc != null)
                {
                    SettingCharacterData(tc);
                }
            }

            RefreshPartyMembersLife();

            if (!we.AvailableSecondCharacter && !we.AvailableThirdCharacter)
            {
                button2.Visible = false;
                labelFirstPlayerLife.Visible = false;
            }
            if (we.AvailableSecondCharacter)
            {
                button3.Visible = true;
                labelSecondPlayerLife.Visible = true;
            }
            else
            {
                button3.Visible = false;
                labelSecondPlayerLife.Visible = false;
            }
            if (we.AvailableThirdCharacter)
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
                strUp.Visible = false;
                aglUp.Visible = false;
                intUp.Visible = false;
                stmUp.Visible = false;
                mindUp.Visible = false;
                mainMessage.Text = "";
            }
            else
            {
                button1.Visible = false;
                if (CumultiveLvUpValue >= 2)
                {
                    mainMessage.Text = CumultiveLvUpValue.ToString() + "���x���A�b�v�I�I" + upPoint.ToString() + "�|�C���g������U���Ă��������B";
                }
                else
                {
                    mainMessage.Text = "���x���A�b�v�I�I" + upPoint.ToString() + "�|�C���g������U���Ă��������B";
                }
            }

            if (onlySelectTrash)
            {
                button1.Text = "���߂�";
            }
        }

        private void RefreshPartyMembersLife()
        {
            if (we.AvailableFirstCharacter)
            {
                labelFirstPlayerLife.Text = mc.CurrentLife.ToString() + "/" + mc.MaxLife.ToString();
            }
            if (we.AvailableSecondCharacter)
            {
                labelSecondPlayerLife.Text = sc.CurrentLife.ToString() + "/" + sc.MaxLife.ToString();
            }
            if (we.AvailableThirdCharacter)
            {
                labelThirdPlayerLife.Text = tc.CurrentLife.ToString() + "/" + tc.MaxLife.ToString();
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

        void StatusPlayer_Click(object sender, EventArgs e)
        {
            MainCharacter player = null;
            if (this.BackColor == Color.LightSkyBlue)
            {
                player = this.mc;
            }
            else if (this.BackColor == Color.Pink)
            {
                player = this.sc;
            }
            else if (this.BackColor == Color.Silver)
            {
                player = this.tc;
            }

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
                        mainMessage.Text = mc.Name + "�F" + ((Label)sender).Text + "���̂ĂĐV�����A�C�e������肷�邩�H";
                        using (YesNoRequestMini yesno = new YesNoRequestMini())
                        {
                            yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                            yesno.ShowDialog();
                            if (yesno.DialogResult == DialogResult.Yes)
                            {
                                bool important = CheckImportantItem(player, backpackData);
                                if (!important)
                                {
                                    player.DeleteBackPack(backpackData);
                                    ((Label)sender).Text = "";
                                    ((Label)sender).Cursor = System.Windows.Forms.Cursors.Default;
                                    this.DialogResult = DialogResult.OK;
                                }
                                else
                                {
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
                    if (!we.AvailableSecondCharacter && !we.AvailableThirdCharacter)
                    {
                        using (SelectAction sa = new SelectAction())
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
                                sa.ElementA = "������";
                            }
                            else
                            {
                                sa.ElementA = "����";
                            }
                            sa.ElementB = "���Ă�";
                            sa.ShowDialog();
                            if (sa.TargetNum == 0)
                            {
                                // switch-case �ɐi�݂܂��B
                            }
                            else if (sa.TargetNum == 1)
                            {
                                bool important = CheckImportantItem(player, backpackData);
                                if (!important)
                                {
                                    player.DeleteBackPack(backpackData);
                                    ((Label)sender).Text = "";
                                    ((Label)sender).Cursor = System.Windows.Forms.Cursors.Default;
                                }
                                return;
                            }
                            else
                            {
                                // ESC�L�[�L�����Z���͉������܂���B
                                return;
                            }
                        }
                    }
                    else
                    {
                        using (SelectAction sa = new SelectAction())
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
                                sa.ElementA = "������";
                            }
                            else
                            {
                                sa.ElementA = "����";
                            }
                            sa.ElementB = "�킽��";
                            sa.ElementC = "���Ă�";
                            sa.ShowDialog();
                            if (sa.TargetNum == 0)
                            {
                                // switch-case �ɐi�݂܂��B
                            }
                            else if (sa.TargetNum == 1)
                            {
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
                                        if (this.BackColor == Color.LightSkyBlue)
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
                                        if (this.BackColor == Color.Pink)
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
                                        if (this.BackColor == Color.Silver)
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
                                        // ESC�L�[�̏ꍇ�A�Ȃɂ����܂���B
                                        return;
                                    }
                                }


                                bool success = target.AddBackPack(backpackData);
                                if (success)
                                {
                                    player.DeleteBackPack(backpackData);
                                    ((Label)sender).Text = "";
                                    ((Label)sender).Cursor = System.Windows.Forms.Cursors.Default;
                                }
                                else
                                {
                                    mainMessage.Text = String.Format(player.GetCharacterSentence(2003), target.Name);
                                }
                                return;
                            }
                            else if (sa.TargetNum == 2)
                            {
                                bool important = CheckImportantItem(player, backpackData);
                                if (!important)
                                {
                                    player.DeleteBackPack(backpackData);
                                    ((Label)sender).Text = "";
                                    ((Label)sender).Cursor = System.Windows.Forms.Cursors.Default;
                                }
                                return;
                            }
                            else
                            {
                                // ESC�L�[�L�����Z���͉������܂���B
                                return;
                            }
                        }
                    }

                    if (player.Dead)
                    {
                        mainMessage.Text = "�y" + player.Name + "�͎���ł��܂��Ă��邽�߁A�A�C�e�����g���Ȃ��B�z";
                        return;
                    }

                    switch (backpackData.Name)
                    {
                        case "�������ԃ|�[�V����":
                        case "���ʂ̐ԃ|�[�V����":
                        case "�傫�Ȑԃ|�[�V����":
                        case "����ԃ|�[�V����":
                        case "���؂Ȑԃ|�[�V����":
                        case "���O���ƂĂ��������ɂ͂܂��������ɗ������A���̌��ʂ��������Ȃ��𗧂����ł���ɂ�������炸�f�R���[�V���������������؂ȃX�[�p�[�~���N���|�[�V����":
                            int effect = backpackData.UseIt();
                            player.CurrentLife += effect;
                            player.DeleteBackPack(backpackData);
                            RefreshPartyMembersLife();
                            this.life.Text = player.CurrentLife.ToString() + " / " + player.MaxLife.ToString();
                            mainMessage.Text = String.Format(player.GetCharacterSentence(2001), effect);
                            ((Label)sender).Text = "";
                            ((Label)sender).Cursor = System.Windows.Forms.Cursors.Default;
                            break;

                        case "�������|�[�V����":
                        case "���ʂ̐|�[�V����":
                        case "�傫�Ȑ|�[�V����":
                        case "����|�[�V����":
                        case "���؂Ȑ|�[�V����":
                            effect = backpackData.UseIt();
                            player.CurrentMana += effect;
                            player.DeleteBackPack(backpackData);
                            this.mana.Text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();
                            mainMessage.Text = String.Format(player.GetCharacterSentence(2001), effect);
                            ((Label)sender).Text = "";
                            ((Label)sender).Cursor = System.Windows.Forms.Cursors.Default;
                            break;

                        case "�_����": // �Q�K�A�C�e��
                            if (!we.AlreadyUseSyperSaintWater)
                            {
                                we.AlreadyUseSyperSaintWater = true;
                                player.CurrentLife += (int)((double)player.MaxLife * 0.3F);
                                player.CurrentMana += (int)((double)player.MaxMana * 0.3F);
                                player.CurrentSkillPoint += (int)((double)player.MaxSkillPoint * 0.3F);
                                RefreshPartyMembersLife();
                                this.life.Text = player.CurrentLife.ToString() + " / " + player.MaxLife.ToString();
                                this.mana.Text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();
                                this.skill.Text = player.CurrentSkillPoint.ToString() + " / " + player.MaxSkillPoint.ToString();
                                mainMessage.Text = player.GetCharacterSentence(2009);
                            }
                            else
                            {
                                mainMessage.Text = player.GetCharacterSentence(2010);
                            }
                            break;

                        case "�����@�C���|�[�V����":
                            if (!we.AlreadyUseRevivePotion)
                            {
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
                                    we.AlreadyUseRevivePotion = true;
                                    target.Dead = false;
                                    target.CurrentLife = target.MaxLife / 2;
                                    RefreshPartyMembersLife();
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

                        case "�I�[�o�[�V�t�e�B���O": // �_���W�����T�K
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
                                System.Threading.Thread.Sleep(30);
                            }
                            strUp.Visible = true;
                            aglUp.Visible = true;
                            intUp.Visible = true;
                            stmUp.Visible = true;
                            mindUp.Visible = true;
                            player.DeleteBackPack(backpackData);
                            ((Label)sender).Text = "";
                            ((Label)sender).Cursor = System.Windows.Forms.Cursors.Default;
                            SettingCharacterData(player);
                            RefreshPartyMembersLife();
                            break;

                        // �����i�F����
                        case "���K�p�̌�": // �A�C����������
                        case "�i�b�N��": // ���i��������
                        case "����̌��i���v���J�j": // ���F���[��������
                        case "�V���[�g�\�[�h": // �K���c�̕���̔��i�_���W�����P�K�j
                        case "�������ꂽ�����O�\�[�h": // �K���c�̕���̔��i�_���W�����P�K�j
                        case "���̌�": // �K���c�̕���̔��i�_���W�����Q�K�j
                        case "���^���t�B�X�g": // �K���c�̕���̔��i�_���W�����Q�K�j
                        case "�v���`�i�\�[�h": // �K���c�̕���̔��i�_���W�����R�K�j
                        case "�t�@���V�I��": // �K���c�̕���̔��i�_���W�����R�K�j
                        case "�A�C�A���N���[": // �K���c�̕���̔��i�_���W�����R�K�j
                        case "�V�����V�[��": // �R�K�A�C�e��
                        case "���C�g�v���Y�}�u���[�h": // �K���c�̕���̔��i�_���W�����S�K�j
                        case "�C�X���A���t�B�X�g": // �K���c�̕���̔��i�_���W�����S�K�j
                        case "�G�X�p�_�X": // �_���W�����S�K�̃A�C�e��
                        case "�\�[�h�E�I�u�E�u���[���[�W��": // �_���W�����S�K�̃A�C�e��
                        case "���i�E�G�O�[�L���[�W���i�[": // �_���W�����T�K
                        case "�����E�X��ւ̒�": // �_���W�����T�K
                        case "�t�@�[�W���E�W�E�G�X�y�����U": // �_���W�����T�K
                        case "�_��  �t�F���g�D�[�V��":
                        case "�o��  �W���m�Z���X�e":
                        case "�Ɍ�  �[�����M�A�X":
                        case "�N���m�X�E���}�e�B�b�h�E�\�[�h":
                            if (((backpackData.Type == ItemBackPack.ItemType.Weapon_Heavy) && (player == sc || player == tc)) // �A�C����p
                                || ((backpackData.Type == ItemBackPack.ItemType.Weapon_Light) && (player == mc || player == tc)) // ���i��p
                                || ((backpackData.Type == ItemBackPack.ItemType.Weapon_Middle) && (player == mc || player == sc))) // ���F���[��p
                            {
                                mainMessage.Text = player.GetCharacterSentence(2019);
                                return;
                            }

                            mainMessage.Text = player.GetCharacterSentence(2004);


                            // [�x��]�F����E�h��E�A�N�Z�T�����Ⴄ�����Ȃ̂ŁA�֐����ꉻ�����{���Ă��������B
                            ItemBackPack tempItem = null;
                            if (player.MainWeapon != null)
                            {
                                tempItem = new ItemBackPack(player.MainWeapon.Name);
                            }
                            player.MainWeapon = backpackData;
                            weapon.Text = player.MainWeapon.Name;
                            switch (player.MainWeapon.Rare)
                            {
                                case ItemBackPack.RareLevel.Poor:
                                    this.weapon.BackColor = Color.Gray;
                                    this.weapon.ForeColor = Color.White;
                                    break;
                                case ItemBackPack.RareLevel.Common:
                                    this.weapon.BackColor = Color.Green;
                                    this.weapon.ForeColor = Color.White;
                                    break;
                                case ItemBackPack.RareLevel.Rare:
                                    this.weapon.BackColor = Color.DarkBlue;
                                    this.weapon.ForeColor = Color.White;
                                    break;
                                case ItemBackPack.RareLevel.Epic:
                                    this.weapon.BackColor = Color.Purple;
                                    this.weapon.ForeColor = Color.White;
                                    break;
                                case ItemBackPack.RareLevel.Legendary: // ��Ғǉ�
                                    this.weapon.BackColor = Color.OrangeRed;
                                    this.weapon.ForeColor = Color.White;
                                    break;
                            }

                            player.DeleteBackPack(backpackData);
                            // [�x��]�Fnull�I�u�W�F�N�g�Ȃ̂��AName�󕶎��Ȃ̂��n�b�L���C�����Ă��������B
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
                                }
                            }
                            else
                            {
                                ((Label)sender).Text = "";
                                ((Label)sender).Cursor = System.Windows.Forms.Cursors.Default;
                            }
                            UpdateBackPackLabel(player);
                            mainMessage.Text = player.GetCharacterSentence(2005);
                            break;

                        // �����i�F�h��
                        case "���^��̊Z�i���v���J�j": // ���F���[��������
                        case "�R�[�g�E�I�u�E�v���[�g": // �A�C����������
                        case "���C�g�E�N���X": // ���i��������
                        case "�`���җp�̍������т�": // �K���c�̕���̔��i�_���W�����P�K�j
                        case "���̊Z": // �K���c�̕���̔��i�_���W�����P�K�j
                        case "�^�J�̊Z": // �Q�K�A�C�e��
                        case "����̂���S�̃v���[�g": // �K���c�̕���̔��i�_���W�����Q�K�j
                        case "�V���N�̕�����": // �K���c�̕���̔��i�_���W�����Q�K�j
                        case "�V���o�[�A�[�}�[": // �K���c�̕���̔��i�_���W�����R�K�j
                        case "�b�琻�̕�����": // �K���c�̕���̔��i�_���W�����R�K�j
                        case "�t�B�X�g�E�N���X": // �K���c�̕���̔��i�_���W�����R�K�j
                        case "�v���[�g�E�A�[�}�[": // �R�K�A�C�e��
                        case "�������E�A�[�}�[": // �R�K�A�C�e��
                        case "�v���Y�}�e�B�b�N�A�[�}�[": // �K���c�̕���̔��i�_���W�����S�K�j
                        case "�ɔ��������̉H��": // �K���c�̕���̔��i�_���W�����S�K�j
                        case "�A���H�C�h�E�N���X": // �_���W�����S�K�̃A�C�e��
                        case "�u���K���_�B��": // �_���W�����S�K�̃A�C�e��
                        case "�����J�E�Z�O�����^�[�^": // �_���W�����S�K�̃A�C�e��
                        case "�w�p�C�X�g�X�E�p�i�b�T���C�j":
                            if (   ((backpackData.Type == ItemBackPack.ItemType.Armor_Heavy) && (player == sc || player == tc)) // �A�C����p
                                || ((backpackData.Type == ItemBackPack.ItemType.Armor_Light) && (player == mc || player == tc)) // ���i��p
                                || ((backpackData.Type == ItemBackPack.ItemType.Armor_Middle) && (player == mc || player == sc))) // ���F���[��p
                            {
                                mainMessage.Text = player.GetCharacterSentence(2019);
                                return;
                            }
                            EquipDecision(player, backpackData, sender);
                            break;

                        // �����i�F�A�N�Z�T��
                        case "�X��̃u���X���b�g": // ���i��������
                        case "�V��̗��i���v���J�j": // ���F���[��������
                        case "�����V�g�̌아": // �P�K�A�C�e��
                        case "�`���N���I�[�u": // �P�K�A�C�e��
                        case "���ׂȃp���[�����O": // �K���c�̕���̔��i�_���W�����P�K�j
                        case "���ɂ̃X�^�[�G���u����": // �K���c�̕���̔��i�_���W�����Q�K�j
                        case "�����o���h": // �K���c�̕���̔��i�_���W�����Q�K�j
                        case "��̍���": // �Q�K�A�C�e��
                        case "�g���킵�̃}���g": // �Q�K�A�C�e��
                        case "���C�I���n�[�g": // �R�K�A�C�e��
                        case "�I�[�K�̘r��": // �R�K�A�C�e��
                        case "�|�S�̐Α�": // �R�K�A�C�e��
                        case "�t�@���l�M�̃V�[��": // �R�K�A�C�e��
                        case "�E�F���j�b�P�̘r��": // �K���c�̕���̔��i�_���W�����R�K�j
                        case "���҂̊ዾ": // �K���c�̕���̔��i�_���W�����R�K�j
                        case "���F�v���Y���o���h": // �K���c�̕���̔��i�_���W�����S�K�j
                        case "�Đ��̖��": // �K���c�̕���̔��i�_���W�����S�K�j
                        case "�V�[���I�u�A�N�A���t�@�C�A": // �K���c�̕���̔��i�_���W�����S�K�j
                        case "�h���S���̃x���g": // �K���c�̕���̔��i�_���W�����S�K�j
                        case "����̓y���_���g": // ���i���x���A�b�v���ł��炦��A�C�e��
                        case "�����̈��": // �_���W�����S�K�̃A�C�e��
                        case "�V�g�̌_��": // �_���W�����S�K�̃A�C�e��
                        case "���i�̃C�������O": // �_���W�����T�K�i���i�̃C�x���g�j // ���i��p
                        case "���W�F���h�E���b�h�z�[�X": // �_���W�����T�K�̃A�C�e��
                        case "�v���C�h�E�I�u�E�V�[�J�[": // �K���c�̕���̔��i�_���W�����T�K�j
                        case "��������̌���": // �K���c�̕���̔��i�_���W�����T�K�j
                        case "�f�B�Z���V�����u�[�c": // �K���c�̕���̔��i�_���W�����T�K�j
                        case "�n�[�g�u���[�J�[": // �K���c�̕���̔��i�_���W�����T�K�j
                        case "�G���~�E�W�����W���@�t�@�[�W�����Ƃ̍���":
                        case "�t�@���E�t���[���@�V�g�̃y���_���g":
                        case "�V�j�L�A�E�J�[���n���c�@�����f�r���A�C":
                        case "�I���E�����f�B�X�@���_�O���[�u":
                        case "���F���[�E�A�[�e�B�@�V��̗�":
                            if (   ((backpackData.EquipablePerson == ItemBackPack.Equipable.Ein) && (player == sc || player == tc)) // �A�C����p
                                || ((backpackData.EquipablePerson == ItemBackPack.Equipable.Lana) && (player == mc || player == tc)) // ���i��p
                                || ((backpackData.EquipablePerson == ItemBackPack.Equipable.Verze) && (player == mc || player == sc))) // ���F���[��p
                            {
                                mainMessage.Text = player.GetCharacterSentence(2019);
                                return;
                            }
                            mainMessage.Text = player.GetCharacterSentence(2004);

                            // [�x��]�F����E�h��E�A�N�Z�T�����Ⴄ�����Ȃ̂ŁA�֐����ꉻ�����{���Ă��������B
                            ItemBackPack tempItem2 = null;
                            if (player.Accessory != null)
                            {
                                tempItem2 = new ItemBackPack(player.Accessory.Name);
                            }
                            player.Accessory = backpackData;
                            accessory.Text = player.Accessory.Name;
                            switch (player.Accessory.Rare)
                            {
                                case ItemBackPack.RareLevel.Poor:
                                    this.accessory.BackColor = Color.Gray;
                                    this.accessory.ForeColor = Color.White;
                                    break;
                                case ItemBackPack.RareLevel.Common:
                                    this.accessory.BackColor = Color.Green;
                                    this.accessory.ForeColor = Color.White;
                                    break;
                                case ItemBackPack.RareLevel.Rare:
                                    this.accessory.BackColor = Color.DarkBlue;
                                    this.accessory.ForeColor = Color.White;
                                    break;
                                case ItemBackPack.RareLevel.Epic:
                                    this.accessory.BackColor = Color.Purple;
                                    this.accessory.ForeColor = Color.White;
                                    break;
                                case ItemBackPack.RareLevel.Legendary: // ��Ғǉ�
                                    this.accessory.BackColor = Color.OrangeRed;
                                    this.accessory.ForeColor = Color.White;
                                    break;
                            }

                            player.DeleteBackPack(backpackData);
                            // [�x��]�Fnull�I�u�W�F�N�g�Ȃ̂��AName�󕶎��Ȃ̂��n�b�L���C�����Ă��������B
                            if (tempItem2 != null)
                            {
                                if (tempItem2.Name != String.Empty)
                                {
                                    player.AddBackPack(tempItem2);
                                }
                                else
                                {
                                    ((Label)sender).Text = "";
                                    ((Label)sender).Cursor = System.Windows.Forms.Cursors.Default;
                                }
                            }
                            else
                            {
                                ((Label)sender).Text = "";
                                ((Label)sender).Cursor = System.Windows.Forms.Cursors.Default;
                            }

                            UpdateBackPackLabel(player);
                            SettingCharacterData(player);
                            RefreshPartyMembersLife();
                            mainMessage.Text = player.GetCharacterSentence(2005);
                            break;
                        
                        // ���̑�
                        case "�u���[�}�e���A��": // �P�K�A�C�e��
                        case "���b�h�}�e���A��": // �R�K�A�C�e��
                        case "�O���[���}�e���A��": // �_���W�����S�K�̃A�C�e��
                        case "�^�C���E�I�u�E���[�Z": // �_���W�����T�K�̉B���A�C�e��
                            mainMessage.Text = player.GetCharacterSentence(2007);
                            break;

                        case "���[�x�X�g�����N�|�[�V����":
                        case "�A�J�V�W�A�̎�":
                            mainMessage.Text = player.GetCharacterSentence(2011);
                            break;

                        case "�����̐���": // �������i��b�C�x���g�œ���A�C�e��
                            if (we.CompleteSlayBoss5)
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
        }

        private bool CheckImportantItem(MainCharacter player, ItemBackPack backpackData)
        {
            if (backpackData.Name == "�����̐���")
            {
                mainMessage.Text = player.GetCharacterSentence(2013);
                return true;
            }
            if (backpackData.Name == "�V��̗��i���v���J�j" || backpackData.Name == "���^��̊Z�i���v���J�j" || backpackData.Name == "����̌��i���v���J�j")
            {
                if (tc != null)
                {
                    if (player.Name == tc.Name)
                    {
                        mainMessage.Text = player.GetCharacterSentence(2013);
                    }
                    else
                    {
                        mainMessage.Text = tc.GetCharacterSentence(2015);
                    }
                }
                else
                {
                    mainMessage.Text = player.GetCharacterSentence(2013);
                }
                return true;
            }
            if (backpackData.Name == "���i�̃C�������O")
            {
                if (sc != null)
                {
                    if (player.Name == sc.Name)
                    {
                        mainMessage.Text = player.GetCharacterSentence(2013);
                    }
                    else
                    {
                        mainMessage.Text = sc.GetCharacterSentence(2015);
                    }
                }
                else
                {
                    mainMessage.Text = player.GetCharacterSentence(2013);
                }
                return true;
            }
            return false;
        }

        private void EquipDecision(MainCharacter player, ItemBackPack backpackData, Object sender)
        {
            player.GetCharacterSentence(2004);

            // [�x��]�F����E�h��E�A�N�Z�T�����Ⴄ�����Ȃ̂ŁA�֐����ꉻ�����{���Ă��������B
            ItemBackPack tempItem = null;
            if (player.MainArmor != null)
            {
                tempItem = new ItemBackPack(player.MainArmor.Name);
            }
            player.MainArmor = backpackData;
            armor.Text = player.MainArmor.Name;
            switch (player.MainArmor.Rare)
            {
                case ItemBackPack.RareLevel.Poor:
                    this.armor.BackColor = Color.Gray;
                    this.armor.ForeColor = Color.White;
                    break;
                case ItemBackPack.RareLevel.Common:
                    this.armor.BackColor = Color.Green;
                    this.armor.ForeColor = Color.White;
                    break;
                case ItemBackPack.RareLevel.Rare:
                    this.armor.BackColor = Color.DarkBlue;
                    this.armor.ForeColor = Color.White;
                    break;
                case ItemBackPack.RareLevel.Epic:
                    this.armor.BackColor = Color.Purple;
                    this.armor.ForeColor = Color.White;
                    break;
                case ItemBackPack.RareLevel.Legendary: // ��Ғǉ�
                    this.armor.BackColor = Color.OrangeRed;
                    this.armor.ForeColor = Color.White;
                    break;
            }

            player.DeleteBackPack(backpackData);
            // [�x��]�Fnull�I�u�W�F�N�g�Ȃ̂��AName�󕶎��Ȃ̂��n�b�L���C�����Ă��������B
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
                }
            }
            else
            {
                ((Label)sender).Text = "";
                ((Label)sender).Cursor = System.Windows.Forms.Cursors.Default;
            }
            UpdateBackPackLabel(player);
            mainMessage.Text = player.GetCharacterSentence(2005);
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
                    switch (backpackData[ii].Rare)
                    {
                        case ItemBackPack.RareLevel.Poor:
                            backpack[ii].BackColor = Color.Gray;
                            backpack[ii].ForeColor = Color.White;                             
                            break;
                        case ItemBackPack.RareLevel.Common:
                            backpack[ii].BackColor = Color.Green;
                            backpack[ii].ForeColor = Color.White;
                            break;
                        case ItemBackPack.RareLevel.Rare:
                            backpack[ii].BackColor = Color.DarkBlue;
                            backpack[ii].ForeColor = Color.White;
                            break;
                        case ItemBackPack.RareLevel.Epic:
                            backpack[ii].BackColor = Color.Purple;
                            backpack[ii].ForeColor = Color.White;
                            break;
                        case ItemBackPack.RareLevel.Legendary: // ��Ғǉ�
                            backpack[ii].BackColor = Color.OrangeRed;
                            backpack[ii].ForeColor = Color.White;
                            break;
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.onlySelectTrash)
            {
                this.DialogResult = DialogResult.Cancel;
            }
            else
            {
                this.Close();
            }
        }

        private void SettingCharacterData(MainCharacter chara)
        {
            this.playerName.Text = chara.FullName;
            this.level.Text = chara.Level.ToString();
            if (chara.Level < 40) // [�x��]�F�O�҂͂k�u�S�O���l�`�w�Ƃ���B
            {
                this.experience.Text = chara.Exp.ToString() + " / " + chara.NextLevelBorder.ToString();
            }
            else
            {
                this.experience.Text = "-----" + " / " + "-----";
            }

            this.strength.Text = chara.Strength.ToString();
            if (chara.BuffStrength_Accessory != 0)
            {
                this.addStrength.Text = "+" + chara.BuffStrength_Accessory.ToString();
            }
            else
            {
                this.addStrength.Text = "";
            }

            this.agility.Text = chara.Agility.ToString();
            if (chara.BuffAgility_Accessory != 0)
            {
                this.addAgility.Text = "+" + chara.BuffAgility_Accessory.ToString();
            }
            else
            {
                this.addAgility.Text = "";
            }

            this.intelligence.Text = chara.Intelligence.ToString();
            if (chara.BuffIntelligence_Accessory != 0)
            {
                this.addIntelligence.Text = "+" + chara.BuffIntelligence_Accessory.ToString();
            }
            else
            {
                this.addIntelligence.Text = "";
            }

            this.stamina.Text = chara.Stamina.ToString();
            if (chara.BuffStamina_Accessory != 0)
            {
                this.addStamina.Text = "+" + chara.BuffStamina_Accessory.ToString();
            }
            else
            {
                this.addStamina.Text = "";
            }

            this.mindLabel.Text = chara.Mind.ToString();
            if (chara.BuffMind_Accessory != 0)
            {
                this.addMind.Text = "+" + chara.BuffMind_Accessory.ToString();
            }
            else
            {
                this.addMind.Text = "";
            }

            RefreshPartyMembersLife();
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
            this.armor.Text = "";
            this.accessory.Text = "";
            if (chara.MainWeapon != null)
            {
                this.weapon.Text = chara.MainWeapon.Name;
                switch (chara.MainWeapon.Rare)
                {
                    case ItemBackPack.RareLevel.Poor:
                        this.weapon.BackColor = Color.Gray;
                        this.weapon.ForeColor = Color.White;
                        break;
                    case ItemBackPack.RareLevel.Common:
                        this.weapon.BackColor = Color.Green;
                        this.weapon.ForeColor = Color.White;
                        break;
                    case ItemBackPack.RareLevel.Rare:
                        this.weapon.BackColor = Color.DarkBlue;
                        this.weapon.ForeColor = Color.White;
                        break;
                    case ItemBackPack.RareLevel.Epic:
                        this.weapon.BackColor = Color.Purple;
                        this.weapon.ForeColor = Color.White;
                        break;
                    case ItemBackPack.RareLevel.Legendary: // ��Ғǉ�
                        this.weapon.BackColor = Color.OrangeRed;
                        this.weapon.ForeColor = Color.White;
                        break;
                }
            }
            if (chara.MainArmor != null)
            {
                this.armor.Text = chara.MainArmor.Name;
                switch (chara.MainArmor.Rare)
                {
                    case ItemBackPack.RareLevel.Poor:
                        this.armor.BackColor = Color.Gray;
                        this.armor.ForeColor = Color.White;
                        break;
                    case ItemBackPack.RareLevel.Common:
                        this.armor.BackColor = Color.Green;
                        this.armor.ForeColor = Color.White;
                        break;
                    case ItemBackPack.RareLevel.Rare:
                        this.armor.BackColor = Color.DarkBlue;
                        this.armor.ForeColor = Color.White;
                        break;
                    case ItemBackPack.RareLevel.Epic:
                        this.armor.BackColor = Color.Purple;
                        this.armor.ForeColor = Color.White;
                        break;
                    case ItemBackPack.RareLevel.Legendary: // ��Ғǉ�
                        this.armor.BackColor = Color.OrangeRed;
                        this.armor.ForeColor = Color.White;
                        break;
                }
            }
            if (chara.Accessory != null)
            {
                this.accessory.Text = chara.Accessory.Name;
                switch (chara.Accessory.Rare)
                {
                    case ItemBackPack.RareLevel.Poor:
                        this.accessory.BackColor = Color.Gray;
                        this.accessory.ForeColor = Color.White;
                        break;
                    case ItemBackPack.RareLevel.Common:
                        this.accessory.BackColor = Color.Green;
                        this.accessory.ForeColor = Color.White;
                        break;
                    case ItemBackPack.RareLevel.Rare:
                        this.accessory.BackColor = Color.DarkBlue;
                        this.accessory.ForeColor = Color.White;
                        break;
                    case ItemBackPack.RareLevel.Epic:
                        this.accessory.BackColor = Color.Purple;
                        this.accessory.ForeColor = Color.White;
                        break;
                    case ItemBackPack.RareLevel.Legendary: // ��Ғǉ�
                        this.accessory.BackColor = Color.OrangeRed;
                        this.accessory.ForeColor = Color.White;
                        break;
                }
            }

            UpdateBackPackLabel(chara);

            this.btnFreshHeal.Visible = chara.FreshHeal;
            this.btnLifeTap.Visible = chara.LifeTap;
            this.btnResurrection.Visible = chara.Resurrection;
            this.btnCelestialNova.Visible = chara.CelestialNova;
        }

        private void strUp_Click(object sender, EventArgs e)
        {
            if (this.BackColor == Color.LightSkyBlue)
            {
                mc.Strength++;
                strength.Text = mc.Strength.ToString();
            }
            else if (this.BackColor == Color.Pink)
            {
                sc.Strength++;
                strength.Text = sc.Strength.ToString();
            }
            else if (this.BackColor == Color.Silver)
            {
                tc.Strength++;
                strength.Text = tc.Strength.ToString();
            }
            CheckUpPoint();
        }

        private void aglUp_Click(object sender, EventArgs e)
        {
            if (this.BackColor == Color.LightSkyBlue)
            {
                mc.Agility++;
                agility.Text = mc.Agility.ToString();
            }
            else if (this.BackColor == Color.Pink)
            {
                sc.Agility++;
                agility.Text = sc.Agility.ToString();
            }
            else if (this.BackColor == Color.Silver)
            {
                tc.Agility++;
                agility.Text = tc.Agility.ToString();
            }
            CheckUpPoint();
        }

        private void intUp_Click(object sender, EventArgs e)
        {
            if (this.BackColor == Color.LightSkyBlue)
            {
                mc.Intelligence++;
                intelligence.Text = mc.Intelligence.ToString();
                if (mc.AvailableMana)
                {
                    this.mana.Text = mc.CurrentMana.ToString() + " / " + mc.MaxMana.ToString();
                }
            }
            else if (this.BackColor == Color.Pink)
            {
                sc.Intelligence++;
                intelligence.Text = sc.Intelligence.ToString();
                if (sc.AvailableMana)
                {
                    this.mana.Text = sc.CurrentMana.ToString() + " / " + sc.MaxMana.ToString();
                }
            }
            else if (this.BackColor == Color.Silver)
            {
                tc.Intelligence++;
                intelligence.Text = tc.Intelligence.ToString();
                if (tc.AvailableMana)
                {
                    this.mana.Text = tc.CurrentMana.ToString() + " / " + tc.MaxMana.ToString();
                }
            }
            CheckUpPoint();
        }

        private void stmUp_Click(object sender, EventArgs e)
        {
            if (this.BackColor == Color.LightSkyBlue)
            {
                mc.Stamina++;
                stamina.Text = mc.Stamina.ToString();
                this.life.Text = mc.CurrentLife.ToString() + " / " + mc.MaxLife.ToString();
            }
            else if (this.BackColor == Color.Pink)
            {
                sc.Stamina++;
                stamina.Text = sc.Stamina.ToString();
                this.life.Text = sc.CurrentLife.ToString() + " / " + sc.MaxLife.ToString();
            }
            else if (this.BackColor == Color.Silver)
            {
                tc.Stamina++;
                stamina.Text = tc.Stamina.ToString();
                this.life.Text = tc.CurrentLife.ToString() + " / " + tc.MaxLife.ToString();
            }
            RefreshPartyMembersLife();
            CheckUpPoint();
        }

        private void mindUp_Click(object sender, EventArgs e)
        {
            if (this.BackColor == Color.LightSkyBlue)
            {
                mc.Mind++;
                mindLabel.Text = mc.Mind.ToString();
            }
            else if (this.BackColor == Color.Pink)
            {
                sc.Mind++;
                mindLabel.Text = sc.Mind.ToString();
            }
            else if (this.BackColor == Color.Silver)
            {
                tc.Mind++;
                mindLabel.Text = tc.Mind.ToString();
            }
            CheckUpPoint();
        }

        private void CheckUpPoint()
        {
            upPoint--;
            if (upPoint <= 0)
            {
                mainMessage.Text = "�|�C���g����U�芮���I";
                button1.Text = "����";
                button1.Visible = true;
                strUp.Visible = false;
                aglUp.Visible = false;
                intUp.Visible = false;
                stmUp.Visible = false;
                mindUp.Visible = false;
                this.useOverShifting = false;
            }
            else
            {
                mainMessage.Text = "����" + upPoint.ToString() + "�|�C���g������U���Ă��������B";
            }
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
                this.BackColor = Color.LightSkyBlue;
                this.currentStatusView = Color.LightSkyBlue;
                SettingCharacterData(mc);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (this.levelUp)
            {
                mainMessage.Text = sc.GetCharacterSentence(2002);
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
                this.BackColor = Color.Pink;
                this.currentStatusView = Color.Pink;
                SettingCharacterData(sc);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (this.levelUp)
            {
                mainMessage.Text = tc.GetCharacterSentence(2002);
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
                this.BackColor = Color.Silver;
                this.currentStatusView = Color.Silver;
                SettingCharacterData(tc);
            }
        }

        private void btnFreshHeal_MouseDown(object sender, MouseEventArgs e)
        {
            this.mousePosX = this.Location.X + ((Button)sender).Location.X + e.X;
            this.mousePosY = this.Location.Y + ((Button)sender).Location.Y + e.Y;
        }


        private void btnResurrection_MouseDown(object sender, MouseEventArgs e)
        {
            this.mousePosX = this.Location.X + ((Button)sender).Location.X + e.X;
            this.mousePosY = this.Location.Y + ((Button)sender).Location.Y + e.Y;
        }

        private void btnCelestialNova_MouseDown(object sender, MouseEventArgs e)
        {
            this.mousePosX = this.Location.X + ((Button)sender).Location.X + e.X;
            this.mousePosY = this.Location.Y + ((Button)sender).Location.Y + e.Y;
        }

        private void btnFreshHeal_Click(object sender, EventArgs e)
        {
            MainCharacter player = null;
            if (this.BackColor == Color.LightSkyBlue)
            {
                player = this.mc;
            }
            else if (this.BackColor == Color.Pink)
            {
                player = this.sc;
            }
            else if (this.BackColor == Color.Silver)
            {
                player = this.tc;
            }

            if (player.Dead)
            {
                mainMessage.Text = "�y" + player.Name + "�͎���ł��܂��Ă��邽�߁A���@�r�����ł��Ȃ��B�z";
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

            if (player.CurrentMana < Database.FRESH_HEAL_COST)
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
                        // ESC�L�[�L�����Z���͉������܂���B
                        return;
                    }
                }
            }

            if (target.Dead)
            {
                mainMessage.Text = "�y" + target.Name + "�͎���ł��܂��Ă��邽�߁A���ʂ��Ȃ��B�z";
                return;
            }

            player.CurrentMana -= Database.FRESH_HEAL_COST;
            Random rd = new Random(DateTime.Now.Millisecond);
            int lifeGain = player.TotalIntelligence * 4 + rd.Next(1, 20);

            target.CurrentLife += lifeGain;
            mainMessage.Text = String.Format(player.GetCharacterSentence(2001), lifeGain.ToString());
            this.life.Text = player.CurrentLife.ToString() + " / " + player.MaxLife.ToString();
            this.mana.Text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();
            RefreshPartyMembersLife();
        }

        private void btnLifeTap_Click(object sender, EventArgs e)
        {
            MainCharacter player = null;
            if (this.BackColor == Color.LightSkyBlue)
            {
                player = this.mc;
            }
            else if (this.BackColor == Color.Pink)
            {
                player = this.sc;
            }
            else if (this.BackColor == Color.Silver)
            {
                player = this.tc;
            }

            if (player.Dead)
            {
                mainMessage.Text = "�y" + player.Name + "�͎���ł��܂��Ă��邽�߁A���@�r�����ł��Ȃ��B�z";
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

            if (player.CurrentMana < Database.LIFE_TAP_COST)
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
                        // ESC�L�[�L�����Z���͉������܂���B
                        return;
                    }
                }
            }

            if (target.Dead)
            {
                mainMessage.Text = "�y" + target.Name + "�͎���ł��܂��Ă��邽�߁A���ʂ��Ȃ��B�z";
                return;
            }

            player.CurrentMana -= Database.LIFE_TAP_COST;
            Random rd = new Random(DateTime.Now.Millisecond);
            int lifeGain = player.TotalIntelligence * 4 + rd.Next(1, 30);

            target.CurrentLife += lifeGain;
            mainMessage.Text = String.Format(player.GetCharacterSentence(2001), lifeGain.ToString());
            this.life.Text = player.CurrentLife.ToString() + " / " + player.MaxLife.ToString();
            this.mana.Text = player.CurrentMana.ToString() + " / " + player.MaxMana.ToString();
            this.skill.Text = player.CurrentSkillPoint.ToString() + " / " + player.MaxSkillPoint.ToString();
            RefreshPartyMembersLife();
        }

        private void btnResurrection_Click(object sender, EventArgs e)
        {
            MainCharacter player = null;
            if (this.BackColor == Color.LightSkyBlue)
            {
                player = this.mc;
            }
            else if (this.BackColor == Color.Pink)
            {
                player = this.sc;
            }
            else if (this.BackColor == Color.Silver)
            {
                player = this.tc;
            }

            if (player.Dead)
            {
                mainMessage.Text = "�y" + player.Name + "�͎���ł��܂��Ă��邽�߁A���@�r�����ł��Ȃ��B�z";
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
                        // ESC�L�[�L�����Z���͉������܂���B
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
            if (this.BackColor == Color.LightSkyBlue)
            {
                player = this.mc;
            }
            else if (this.BackColor == Color.Pink)
            {
                player = this.sc;
            }
            else if (this.BackColor == Color.Silver)
            {
                player = this.tc;
            }

            if (player.Dead)
            {
                mainMessage.Text = "�y" + player.Name + "�͎���ł��܂��Ă��邽�߁A���@�r�����ł��Ȃ��B�z";
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
                        // ESC�L�[�L�����Z���͉������܂���B
                        return;
                    }
                }
            }

            if (target.Dead)
            {
                mainMessage.Text = "�y" + target.Name + "�͎���ł��܂��Ă��邽�߁A���ʂ��Ȃ��B�z";
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

        private void pbSortByUsed_Click(object sender, EventArgs e)
        {
            ExecItemBackPackSort(0);
        }

        private void pbSortByAccessory_Click(object sender, EventArgs e)
        {
            ExecItemBackPackSort(1);
        }

        private void pbSortByWeapon_Click(object sender, EventArgs e)
        {
            ExecItemBackPackSort(2);
        }

        private void pbSortByArmor_Click(object sender, EventArgs e)
        {
            ExecItemBackPackSort(3);
        }

        private void pbSortByName_Click(object sender, EventArgs e)
        {
            ExecItemBackPackSort(4);
        }

        private void pbSortByRare_Click(object sender, EventArgs e)
        {
            ExecItemBackPackSort(5);
        }

        private void ExecItemBackPackSort(int type)
        {
            MainCharacter player = null;
            if (this.BackColor == Color.LightSkyBlue)
            {
                player = this.mc;
            }
            else if (this.BackColor == Color.Pink)
            {
                player = this.sc;
            }
            else if (this.BackColor == Color.Silver)
            {
                player = this.tc;
            }

            if (type == 0)
            {
                player.ReplaceBackPack(player.SortByUsed());
            }
            else if (type == 1)
            {
                player.ReplaceBackPack(player.SortByAccessory());
            }
            else if (type == 2)
            {
                player.ReplaceBackPack(player.SortByWeapon());
            }
            else if (type == 3)
            {
                player.ReplaceBackPack(player.SortByArmor());
            }
            else if (type == 4)
            {
                player.ReplaceBackPack(player.SortByName());
            }
            else if (type == 5)
            {
                player.ReplaceBackPack(player.SortByRare());
            }

            UpdateBackPackLabel(player);

        }
    }
}