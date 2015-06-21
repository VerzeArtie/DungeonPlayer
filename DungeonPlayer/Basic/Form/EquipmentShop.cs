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
        public bool LayoutLarge { get; set; } // 後編追加

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
        protected int MAX_EQUIPLIST = 25; // 後編編集

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
            ganz.FullName = "ガンツ・ギメルガ";
            ganz.Name = "ガンツ";

            equipList = new Label[MAX_EQUIPLIST];
            costList = new Label[MAX_EQUIPLIST];
            backpackList = new Label[Database.MAX_BACKPACK_SIZE];
            
            // move-out
            OnInitializeLayout();
        }

        // s 後編追加
        protected virtual void OnInitializeLayout()
        {
            // インスタンス生成部で記述していた処理をこちらへ移行し、後編画面レイアウトは別途できるようにした。
            for (int ii = 0; ii < MAX_EQUIPLIST; ii++)
            {
                equipList[ii] = new Label();
                equipList[ii].Font = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                equipList[ii].Location = new System.Drawing.Point(50, 100 + 26 * ii); // 後編編集
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
                costList[ii].Location = new System.Drawing.Point(50 + 200, 100 + 26 * ii); // 後編編集
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
                equipList[ii].Location = new System.Drawing.Point(50, 100 + 26 * ii); // 後編編集
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
                costList[ii].Location = new System.Drawing.Point(50 + 200, 100 + 26 * ii); // 後編編集
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
        // e 後編追加

        protected void EquipmentShop_Load(object sender, EventArgs e)
        {
            this.currentPlayer = this.mc;

            if (!we.AvailableSecondCharacter && !we.AvailableThirdCharacter)
            {
                btnBackpack1.Visible = false; // [コメント]：最初はキャラクター増加を見せない演出のため、VisibleはFalse
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
                btnBackpack3.Visible = false; // [コメント]：ストーリーの演出上、ヴェルゼはガンツの武具屋へ訪れる事はないため。
            }

            UpdateBackPackLabel(this.currentPlayer);

            if (we.AvailableEquipShop && !we.AvailableEquipShop2)
            {
                SetupAvailableList(1);

                btnLevel1.Visible = false; // [コメント]：最初は武具種類が増える傾向を見せない演出のため、VisibleはFalse
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
            OnLoadMessage(); // 後編編集
            label2.Text = mc.Gold.ToString() + "[G]"; // [警告]：ゴールドの所持は別クラスにするべきです。
        }

        // s 後編追加
        protected virtual void OnLoadSetupFloorButton()
        {
            // 前編では実装していない
        }
        // e 後編追加

        // s 後編追加
        protected virtual void OnLoadMessage()
        {
            SetupMessageText(3000);
        }
        // e 後編追加

        // [コメント]：引数が無限に増える可能性がある場合、記述方法が何かありそうです。時間があれば探してください。
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
            mainMessage.Update(); // 後編追加
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
            mainMessage.Update(); // 後編追加
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
            mainMessage.Update(); // 後編追加
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
        // s 後編編集
        protected virtual void ConstructPopupInfo(PopUpMini popupInfo)
        {
            popupInfo.CurrentInfo = currentPlayer.FullName + "\r\n";

            if (currentPlayer.MainWeapon == null)
            {
                popupInfo.CurrentInfo += "武器 " + "（なし）" + "\r\n";
            }
            else
            {
                popupInfo.CurrentInfo += "武器 " + currentPlayer.MainWeapon.Name + "\r\n";
            }
            if (currentPlayer.MainArmor == null)
            {
                popupInfo.CurrentInfo += "防具 " + "（なし）" + "\r\n";
            }
            else
            {
                popupInfo.CurrentInfo += "防具 " + currentPlayer.MainArmor.Name + "\r\n";
            }
            if (currentPlayer.Accessory == null)
            {
                popupInfo.CurrentInfo += "装飾品  " + "（なし）" + "\r\n";
            }
            else
            {
                popupInfo.CurrentInfo += "装飾品  " + currentPlayer.Accessory.Name + "\r\n";
            }
            popupInfo.CurrentInfo += "\r\n";
        }
        // e 後編編集

        protected void EquipmentShop_MouseMove(object sender, MouseEventArgs e)
        {

            if (popupInfo == null)
            {
                popupInfo = new PopUpMini();
            }

            popupInfo.StartPosition = FormStartPosition.Manual;
            popupInfo.Location = new Point(this.Location.X + ((Label)sender).Location.X + e.X + 10, this.Location.Y + ((Label)sender).Location.Y + e.Y + -100);
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

            popupInfo.FontFamilyName = new Font("ＭＳ ゴシック", 14.0F, FontStyle.Regular, GraphicsUnit.Pixel, 128, true);

            ConstructPopupInfo(popupInfo);

            // [警告] 同じ扱いで良しとするが、今後アクセサリで攻撃力上昇したり、武具でパラメタUPがあるものがあれば、対応必須。
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
                        popupInfo.CurrentInfo += "最小防御力  " + currentPlayer.MainArmor.MinValue.ToString() + " ==> " + itemInfo.MinValue.ToString() + "\r\n";
                        popupInfo.CurrentInfo += "最大防御力  " + currentPlayer.MainArmor.MaxValue.ToString() + " ==> " + itemInfo.MaxValue.ToString() + "\r\n";
                    }
                    else
                    {
                        popupInfo.CurrentInfo += "（装備不可）" + "\r\n";
                    }
                    break;
                case ItemBackPack.ItemType.Weapon_Heavy:
                case ItemBackPack.ItemType.Weapon_Light:
                case ItemBackPack.ItemType.Weapon_Middle:
                    if (((currentPlayer == mc) && (itemInfo.Type == ItemBackPack.ItemType.Weapon_Heavy))
                        || ((currentPlayer == sc) && (itemInfo.Type == ItemBackPack.ItemType.Weapon_Light))
                        || ((currentPlayer == tc) && (itemInfo.Type == ItemBackPack.ItemType.Weapon_Middle)))
                    {
                        popupInfo.CurrentInfo += "最小攻撃力  " + currentPlayer.MainWeapon.MinValue.ToString() + " ==> " + itemInfo.MinValue.ToString() + "\r\n";
                        popupInfo.CurrentInfo += "最大攻撃力  " + currentPlayer.MainWeapon.MaxValue.ToString() + " ==> " + itemInfo.MaxValue.ToString() + "\r\n";
                    }
                    else
                    {
                        popupInfo.CurrentInfo += "（装備不可）" + "\r\n";
                    }
                    break;
                // s 後編追加
                case ItemBackPack.ItemType.Weapon_Rod:
                    if (((currentPlayer == sc))
                        || ((currentPlayer == tc)))
                    {
                        popupInfo.CurrentInfo += "最小攻撃力  " + currentPlayer.MainWeapon.MinValue.ToString() + " ==> " + itemInfo.MinValue.ToString() + "\r\n";
                        popupInfo.CurrentInfo += "最大攻撃力  " + currentPlayer.MainWeapon.MaxValue.ToString() + " ==> " + itemInfo.MaxValue.ToString() + "\r\n";
                        popupInfo.CurrentInfo += "最小魔法力  " + currentPlayer.MainWeapon.MagicMinValue.ToString() + " ==> " + itemInfo.MagicMinValue.ToString() + "\r\n";
                        popupInfo.CurrentInfo += "最大魔法力  " + currentPlayer.MainWeapon.MagicMaxValue.ToString() + " ==> " + itemInfo.MagicMaxValue.ToString() + "\r\n";
                    }
                    else
                    {
                        popupInfo.CurrentInfo += "（装備不可）" + "\r\n";
                    }
                    break;

                case ItemBackPack.ItemType.Shield:
                    if (currentPlayer.SubWeapon != null)
                    {
                        if (currentPlayer.SubWeapon.Type == ItemBackPack.ItemType.Shield)
                        {
                            popupInfo.CurrentInfo += "最小防御力  " + currentPlayer.SubWeapon.MinValue.ToString() + " ==> " + itemInfo.MinValue.ToString() + "\r\n";
                            popupInfo.CurrentInfo += "最大防御力  " + currentPlayer.SubWeapon.MaxValue.ToString() + " ==> " + itemInfo.MaxValue.ToString() + "\r\n";
                        }
                        else
                        {
                            popupInfo.CurrentInfo += "最小防御力  ---" + " ==> " + itemInfo.MinValue.ToString() + "\r\n";
                            popupInfo.CurrentInfo += "最大防御力  ---" + " ==> " + itemInfo.MaxValue.ToString() + "\r\n";
                        }
                    }
                    else
                    {
                        popupInfo.CurrentInfo += "最小防御力  ---" + " ==> " + itemInfo.MinValue.ToString() + "\r\n";
                        popupInfo.CurrentInfo += "最大防御力  ---" + " ==> " + itemInfo.MaxValue.ToString() + "\r\n";
                    }
                    break;

                case ItemBackPack.ItemType.Weapon_TwoHand:
                    if (currentPlayer == mc)
                    {
                        popupInfo.CurrentInfo += "最小攻撃力  " + currentPlayer.MainWeapon.MinValue.ToString() + " ==> " + itemInfo.MinValue.ToString() + "\r\n";
                        popupInfo.CurrentInfo += "最大攻撃力  " + currentPlayer.MainWeapon.MaxValue.ToString() + " ==> " + itemInfo.MaxValue.ToString() + "\r\n";
                    }
                    else
                    {
                        popupInfo.CurrentInfo += "（装備不可）" + "\r\n";
                    }
                    break;

                // e 後編追加
                case ItemBackPack.ItemType.Accessory:
                    int fixedSize = 11;
                    // 力
                    string strengthInfo = String.Empty;
                    strengthInfo += "力  " + currentPlayer.Strength.ToString();
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

                    // 技
                    string agilityInfo = String.Empty;
                    agilityInfo += "技  " + currentPlayer.Agility.ToString();
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

                    // 知
                    string intelligenceInfo = String.Empty;
                    intelligenceInfo += "知  " + currentPlayer.Intelligence.ToString();
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

                    // 体
                    string staminaInfo = String.Empty;
                    staminaInfo += "体  " + currentPlayer.Stamina.ToString();
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

                    // 心
                    string mindInfo = String.Empty;
                    mindInfo += "心  " + currentPlayer.Mind.ToString();
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
                            case "ショートソード": // ガンツの武具屋販売（ダンジョン１階）
                                mainMessage.Text = "ガンツ：そいつは標準的なショートソードだね。買うかね？";
                                break;
                            case "洗練されたロングソード": // ガンツの武具屋販売（ダンジョン１階）
                                mainMessage.Text = "ガンツ：普通のロングソードだがヴァスタ爺が少し鍛えてある。買うかね？";
                                break;
                            case "冒険者用の鎖かたびら": // ガンツの武具屋販売（ダンジョン１階）
                                mainMessage.Text = "ガンツ：冒険者なら必需品といえる防御を誇る。買うかね？";
                                break;
                            case "青銅の鎧": // ガンツの武具屋販売（ダンジョン１階）
                                mainMessage.Text = "ガンツ：文句なしの良品質な一品だ。買うかね？";
                                break;
                            case "神剣  フェルトゥーシュ":
                                mainMessage.Text = "ガンツ：ヴァスタ爺の最高傑作だが、先客が買占めてしまったようだ。すまない。";
                                return;
                            case "些細なパワーリング": // ガンツの武具屋販売（ダンジョン１階）
                                mainMessage.Text = "ガンツ：目の付け所が良いな。買うかね？";
                                break;
                            case "紺碧のスターエムブレム": // ガンツの武具屋販売（ダンジョン２階）
                                mainMessage.Text = "ガンツ：ハンナの思い付きを採用した一品だ。買うかね？";
                                break;
                            case "闘魂バンド": // ガンツの武具屋販売（ダンジョン２階）
                                mainMessage.Text = "ガンツ：やる気を出すにはこのバンドが最適だ。買うかね？";
                                break;
                            case "ウェルニッケの腕輪": // ガンツの武具屋販売（ダンジョン３階）
                                mainMessage.Text = "ウェルニッケ素材を使う事で体力の源を宿らせた腕輪だ。買うかね？";
                                break;
                            case "賢者の眼鏡": // ガンツの武具屋販売（ダンジョン３階）
                                mainMessage.Text = "ヴァスタ爺が１日の思いつきで作ったユニークな眼鏡だ。買うかね？";
                                break;
                            case "ファルシオン": // ガンツの武具屋販売（ダンジョン３階）
                                mainMessage.Text = "過去の文献を参考にして作り上げた剣だ。買うかね？";
                                break;
                            case "フィスト・クロス": // ガンツの武具屋販売（ダンジョン３階）
                                mainMessage.Text = "打撃系同士の打ち合いに特化させた衣だ。買うかね？";
                                break;

                            case "青銅の剣": // ガンツの武具屋販売（ダンジョン２階）
                                mainMessage.Text = "青銅の剣は重さと威力が良いバランスじゃよ。買うかね？";
                                break;
                            case "メタルフィスト": // ガンツの武具屋販売（ダンジョン２階）
                                mainMessage.Text = "メタル製は若干重いものの、慣れれば扱いは良いはず。買うかね？";
                                break;
                            case "光沢のある鉄のプレート": // ガンツの武具屋販売（ダンジョン２階）
                                mainMessage.Text = "鉄製のプレートにイエローマテリアルを幾つか埋め込んだ。買うかね？";
                                break;
                            case "シルクの武道衣": // ガンツの武具屋販売（ダンジョン２階）
                                mainMessage.Text = "シルク製だが、縫い目をキメ細かくしてあり頑丈なものになっておる。買うかね？";
                                break;

                            case "プラチナソード": // ガンツの武具屋販売（ダンジョン３階）
                                mainMessage.Text = "プラチナ素材で精製した剣だ。シンプルじゃろ。買うかね？";
                                break;
                            case "アイアンクロー": // ガンツの武具屋販売（ダンジョン３階）
                                mainMessage.Text = "鉄製の爪だ。シンプルじゃろ。買うかね？";
                                break;
                            case "シルバーアーマー": // ガンツの武具屋販売（ダンジョン３階）
                                mainMessage.Text = "銀素材を少しずつ埋め込む事で耐久性をあげた鎧だ。買うかね？";
                                break;
                            case "獣皮製の舞踏衣": // ガンツの武具屋販売（ダンジョン３階）
                                mainMessage.Text = "ギルブロンド種族の皮を使って生成したものだ。買うかね？";
                                break;

                            case "ライトプラズマブレード": // ガンツの武具屋販売（ダンジョン４階）
                                mainMessage.Text = "イエローとブルーマテリアルをふんだんに使ったブレードだ。買うかね？";
                                break;
                            case "イスリアルフィスト": // ガンツの武具屋販売（ダンジョン４階）
                                mainMessage.Text = "ほぼ透明で、重さを感じさせないが威力は確かなものとした。買うかね？";
                                break;
                            case "プリズマティックアーマー": // ガンツの武具屋販売（ダンジョン４階）
                                mainMessage.Text = "カラーマテリアルを幾つか合成して作成したものだ。買うかね？";
                                break;
                            case "極薄合金製の羽衣": // ガンツの武具屋販売（ダンジョン４階）
                                mainMessage.Text = "この薄さの合金に仕立てるのは苦労させられた。買うかね？";
                                break;

                            case "七色プリズムバンド": // ガンツの武具屋販売（ダンジョン４階）
                                mainMessage.Text = "カラーマテリアルを上手く組み合わせて作ったアクセサリだ。買うかね？";
                                break;
                            case "再生の紋章": // ガンツの武具屋販売（ダンジョン４階）
                                mainMessage.Text = "ウェルニッケの素材を極小にして、埋め込んだものだ。買うかね？";
                                break;
                            case "シールオブアクア＆ファイア": // ガンツの武具屋販売（ダンジョン４階）
                                mainMessage.Text = "少し一風変わっておるだろ。買うかね？";
                                break;
                            case "ドラゴンのベルト": // ガンツの武具屋販売（ダンジョン４階）
                                mainMessage.Text = "希少価値のあるドラゴン素材を使ったものだ。買うかね？";
                                break;


                            // 武具屋で以下のものは販売予定ありません。
                            case "小さい赤ポーション":
                            case "普通の赤ポーション":
                            case "大きな赤ポーション":
                            case "特大赤ポーション":
                            case "豪華な赤ポーション":
                            case "名前がとても長いわりにはまったく役に立たず、何の効果も発揮しない役立たずであるにもかかわらずデコレーションが長い超豪華なスーパーミラクルポーション":
                            case "神聖水": // ２階アイテム

                            case "練習用の剣": // アイン初期装備
                            case "ナックル": // ラナ初期装備
                            case "白銀の剣（レプリカ）": // ヴェルゼ初期装備
                            case "シャムシール": // ３階アイテム
                            case "エスパダス": // ダンジョン４階のアイテム
                            case "ルナ・エグゼキュージョナー": // ダンジョン５階
                            case "蒼黒・氷大蛇の爪": // ダンジョン５階
                            case "ファージル・ジ・エスペランザ": // ダンジョン５階
                            case "双剣  ジュノセレステ":
                            case "極剣  ゼムルギアス":
                            case "クロノス・ロマティッド・ソード":

                            case "コート・オブ・プレート": // アイン初期装備
                            case "ライト・クロス": // ラナ初期装備
                            case "黒真空の鎧（レプリカ）": // ヴェルゼ初期装備
                            case "真鍮の鎧": // ２階アイテム
                            case "プレート・アーマー": // ３階アイテム
                            case "ラメラ・アーマー": // ３階アイテム
                            case "ブリガンダィン": // ダンジョン４階のアイテム
                            case "ロリカ・セグメンタータ": // ダンジョン４階のアイテム
                            case "アヴォイド・クロス": // ダンジョン４階のアイテム
                            case "ソード・オブ・ブルールージュ": // ダンジョン４階のアイテム
                            case "ヘパイストス・パナッサロイニ":

                            case "珊瑚のブレスレット": // ラナ初期装備
                            case "天空の翼（レプリカ）": // ヴェルゼ初期装備
                            case "炎授天使の護符": // １階アイテム
                            case "チャクラオーブ": // １階アイテム
                            case "鷹の刻印": // ２階アイテム
                            case "身かわしのマント": // ２階アイテム
                            case "ライオンハート": // ３階アイテム
                            case "オーガの腕章": // ３階アイテム
                            case "鋼鉄の石像": // ３階アイテム
                            case "ファラ様信仰のシール": // ３階アイテム
                            case "剣紋章ペンダント": // ラナレベルアップ時でもらえるアイテム
                            case "夢見の印章": // ダンジョン４階のアイテム
                            case "天使の契約書": // ダンジョン４階のアイテム
                            case "エルミ・ジョルジュ　ファージル王家の刻印":
                            case "ファラ・フローレ　天使のペンダント":
                            case "シニキア・カールハンツ　魔道デビルアイ":
                            case "オル・ランディス　炎神グローブ":
                            case "ヴェルゼ・アーティ　天空の翼":

                            case "ブルーマテリアル": // １階アイテム
                            case "レッドマテリアル": // ３階アイテム
                            case "グリーンマテリアル": // ダンジョン４階のアイテム
                            case "リーベストランクポーション":
                            case "リヴァイヴポーション":
                            case "アカシジアの実":
                            case "遠見の青水晶": // 初期ラナ会話イベントで入手アイテム
                            case "オーバーシフティング": // ダンジョン５階
                            case "レジェンド・レッドホース": // ダンジョン５階
                            case "ラナのイヤリング": // ダンジョン５階（ラナのイベント）
                            case "タイム・オブ・ルーセ": // ダンジョン５階の隠しアイテム
                            default:
                                VendorBuyMessage(backpackData); // 後編編集
                                break; // 後編編集
                        }
                    }
                    else
                    {
                        if (backpackData.Name == "神剣  フェルトゥーシュ")
                        {
                            mainMessage.Text = this.currentPlayer.GetCharacterSentence(3010);
                            return;
                        }

                        mainMessage.Text = String.Format(this.currentPlayer.GetCharacterSentence(3001), backpackData.Name, backpackData.Cost.ToString());
                    }

                    // [警告] 購入手続きのロジックが綺麗ではありません。ベストコーディングを狙ってください。
                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                    {
                        yesno.Large = this.LayoutLarge; // 後編追加
                        yesno.Location = new Point(this.Location.X + YESNO_LOCATION_X, this.Location.Y + YESNO_LOCATION_Y); // 後編編集
                        yesno.ShowDialog();
                        if (yesno.DialogResult == DialogResult.Yes)
                        {
                            if (mc.Gold < backpackData.Cost)
                            {
                                MessageExchange1(backpackData, mc); // 後編編集
                            }
                            else
                            {
                                if (((currentPlayer == mc) && (backpackData.Type == ItemBackPack.ItemType.Armor_Heavy))
                                   || ((currentPlayer == mc) && (backpackData.Type == ItemBackPack.ItemType.Weapon_TwoHand)) // 後編追加
                                   || ((currentPlayer == mc) && (backpackData.Type == ItemBackPack.ItemType.Weapon_Heavy))
                                   || ((currentPlayer == sc) && (backpackData.Type == ItemBackPack.ItemType.Armor_Light))
                                   || ((currentPlayer == sc) && (backpackData.Type == ItemBackPack.ItemType.Weapon_Light))
                                   || ((currentPlayer == sc) && (backpackData.Type == ItemBackPack.ItemType.Weapon_Rod)) // 後編追加
                                   || ((currentPlayer == tc) && (backpackData.Type == ItemBackPack.ItemType.Armor_Middle))
                                   || ((currentPlayer == tc) && (backpackData.Type == ItemBackPack.ItemType.Weapon_Middle))
                                   || ((currentPlayer == tc) && (backpackData.Type == ItemBackPack.ItemType.Weapon_Rod)) // 後編追加
                                   || (backpackData.Type == ItemBackPack.ItemType.Accessory))
                                {
                                    // 装備可能なため装備するかどうか、問い合わせ。
                                    SetupMessageText(3011);
                                    using (YesNoRequestMini yesno2 = new YesNoRequestMini())
                                    {
                                        yesno2.Large = this.LayoutLarge; // 後編追加
                                        yesno2.Location = new Point(this.Location.X + YESNO_LOCATION_X, this.Location.Y + YESNO_LOCATION_Y); // 後編編集
                                        yesno2.ShowDialog();
                                        if (yesno2.DialogResult == DialogResult.Yes)
                                        {
                                            // s 後編追加
                                            // 現在装備が売却可能かどうかを確認
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
                                                    // アイテムが一杯の時、取引不成立。
                                                    MessageExchange2(); // 後編編集
                                                    return;
                                                }
                                                else
                                                {
                                                    // 新しいアイテムを追加して、支払い。取引成立。
                                                    mc.Gold -= backpackData.Cost;
                                                    label2.Text = mc.Gold.ToString() + "[G]"; // [警告]：ゴールドの所持は別クラスにするべきです。
                                                    UpdateBackPackLabel(this.currentPlayer);
                                                    MessageExchange3(); // 後編編集
                                                    return;
                                                }
                                            }
                                            // e 後編追加
                                                 
                                            // 現在装備と取替えで現在装備売却するかどうか、問い合わせ。
                                            int cost = 0;
                                            if ((backpackData.Type == ItemBackPack.ItemType.Armor_Middle) || (backpackData.Type == ItemBackPack.ItemType.Armor_Light) || (backpackData.Type == ItemBackPack.ItemType.Armor_Heavy))
                                            {
                                                cost = currentPlayer.MainArmor.Cost / 2;
                                                SetupMessageText(3012, currentPlayer.MainArmor.Name, cost.ToString());
                                            }
                                            else if ((backpackData.Type == ItemBackPack.ItemType.Weapon_Rod) || (backpackData.Type == ItemBackPack.ItemType.Weapon_TwoHand) || (backpackData.Type == ItemBackPack.ItemType.Weapon_Heavy) || (backpackData.Type == ItemBackPack.ItemType.Weapon_Light) || (backpackData.Type == ItemBackPack.ItemType.Weapon_Middle)) // 後編編集
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
                                                yesno3.Large = this.LayoutLarge; // 後編追加
                                                yesno3.Location = new Point(this.Location.X + YESNO_LOCATION_X, this.Location.Y + YESNO_LOCATION_Y); // 後編編集
                                                yesno3.ShowDialog();
                                                if (yesno3.DialogResult == DialogResult.Yes)
                                                {
                                                    // 現在装備と取替え成立。買い取り額をプラスする。
                                                    mc.Gold += cost;
                                                    label2.Text = mc.Gold.ToString() + "[G]"; // [警告]：ゴールドの所持は別クラスにするべきです。
                                                }
                                                else
                                                {
                                                    // 現在装備と取替えしないため、荷物がいっぱいの場合、取引不成立とする。
                                                    if ((backpackData.Type == ItemBackPack.ItemType.Armor_Middle) || (backpackData.Type == ItemBackPack.ItemType.Armor_Light) || (backpackData.Type == ItemBackPack.ItemType.Armor_Heavy))
                                                    {
                                                        if (!currentPlayer.AddBackPack(currentPlayer.MainArmor))
                                                        {
                                                            MessageExchange2(); // 後編編集
                                                            return;
                                                        }
                                                    }
                                                    else if ((backpackData.Type == ItemBackPack.ItemType.Weapon_Rod) || (backpackData.Type == ItemBackPack.ItemType.Weapon_TwoHand) || (backpackData.Type == ItemBackPack.ItemType.Weapon_Heavy) || (backpackData.Type == ItemBackPack.ItemType.Weapon_Light) || (backpackData.Type == ItemBackPack.ItemType.Weapon_Middle)) // 後編編集
                                                    {
                                                        if (!currentPlayer.AddBackPack(currentPlayer.MainWeapon))
                                                        {
                                                            MessageExchange2(); // 後編編集
                                                            return;
                                                        }
                                                    }
                                                    else if (backpackData.Type == ItemBackPack.ItemType.Accessory)
                                                    {
                                                        if (!currentPlayer.AddBackPack(currentPlayer.Accessory))
                                                        {
                                                            MessageExchange2(); // 後編編集
                                                            return;
                                                        }
                                                    }
                                                    UpdateBackPackLabel(this.currentPlayer);
                                                }

                                                // s 後編編集
                                                if ((backpackData.Type == ItemBackPack.ItemType.Weapon_Rod) || (backpackData.Type == ItemBackPack.ItemType.Weapon_TwoHand))
                                                {
                                                    cost = currentPlayer.SubWeapon.Cost / 2;
                                                    SetupMessageText(3012, currentPlayer.SubWeapon.Name, cost.ToString());

                                                    using (YesNoRequestMini yesno4 = new YesNoRequestMini())
                                                    {
 							yesno4.Large = this.LayoutLarge; // 後編追加
                                                        yesno4.Location = new Point(this.Location.X + YESNO_LOCATION_X, this.Location.Y + YESNO_LOCATION_Y);
                                                        yesno4.ShowDialog();
                                                        if (yesno4.DialogResult == DialogResult.Yes)
                                                        {
                                                            // 現在装備と取替え成立。買い取り額をプラスする。
                                                            mc.Gold += cost;
                                                            label2.Text = mc.Gold.ToString() + "[G]"; // [警告]：ゴールドの所持は別クラスにするべきです。
                                                        }
                                                        else
                                                        {
                                                            MessageExchange7(backpackData.Name);
                                                            Method.AddItemBank(we, backpackData.Name);
                                                            //MessageExchange2(); // 後編編集
                                                            //return;
                                                            UpdateBackPackLabel(this.currentPlayer);
                                                            OKRequest ok = new OKRequest();
                                                            ok.StartPosition = FormStartPosition.Manual;
                                                            ok.Location = new Point(this.Location.X + OK_LOCATION_X, this.Location.Y + OK_LOCATION_Y); // 後編編集
                                                            ok.ShowDialog();
                                                        }
                                                    }
                                                }
                                                // e 後編編集

                                                // 新しいアイテムを装備させて、支払いを行い、取引完了。
                                                if ((backpackData.Type == ItemBackPack.ItemType.Armor_Middle) || (backpackData.Type == ItemBackPack.ItemType.Armor_Light) || (backpackData.Type == ItemBackPack.ItemType.Armor_Heavy))
                                                {
                                                    currentPlayer.MainArmor = backpackData;
                                                }
                                                else if ((backpackData.Type == ItemBackPack.ItemType.Weapon_Rod) || (backpackData.Type == ItemBackPack.ItemType.Weapon_TwoHand) || (backpackData.Type == ItemBackPack.ItemType.Weapon_Heavy) || (backpackData.Type == ItemBackPack.ItemType.Weapon_Light) || (backpackData.Type == ItemBackPack.ItemType.Weapon_Middle)) // 後編編集
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
                                                MessageExchange3(); // 後編編集
                                            }
                                        }
                                        else
                                        {
                                            // 装備させず、新しいアイテムを購入。
                                            bool success = this.currentPlayer.AddBackPack(backpackData);
                                            if (!success)
                                            {
                                                // アイテムが一杯の時、取引不成立。
                                                MessageExchange2(); // 後編編集
                                            }
                                            else
                                            {
                                                // 新しいアイテムを追加して、支払い。取引成立。
                                                mc.Gold -= backpackData.Cost;
                                                label2.Text = mc.Gold.ToString() + "[G]"; // [警告]：ゴールドの所持は別クラスにするべきです。
                                                UpdateBackPackLabel(this.currentPlayer);
                                                MessageExchange3(); // 後編編集
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
                                            MessageExchange2(); // 後編編集
                                        }
                                        else
                                        {
                                            mc.Gold -= backpackData.Cost;
                                            label2.Text = mc.Gold.ToString() + "[G]"; // [警告]：ゴールドの所持は別クラスにするべきです。
                                            UpdateBackPackLabel(this.currentPlayer);
                                            MessageExchange3(); // 後編編集
                                        }
                                    }
                                    else
                                    {
                                        MessageExchange1(backpackData, mc); // 後編編集
                                    }
                                }
                            }
                        }
                        else
                        {
                            MessageExchange4(); // 後編編集
                        }
                    }
                    return;
                }
            }

            for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
            {
                int stack = 1; // 後編追加

                if (((Label)sender).Name == "backpackList" + ii.ToString())
                {
                    ItemBackPack backpackData = new ItemBackPack(((Label)sender).Text);
                    switch (backpackData.Name)
                    {
                        // [コメント]：特別なアイテムの場合別会話を繰り広げてください。
                        // s 後編
                        case Database.EPIC_OLD_TREE_MIKI_DANPEN:
                            if (!we.GanzGift1)
                            {
                                UpdateMainMessage("ガンツ：・・・アインよ、良い物を見つけてきたな。");

                                UpdateMainMessage("アイン：おじさん、これは一体？");

                                UpdateMainMessage("ガンツ：この大陸の遥か北にある山脈ウェクスラーには、かつて古代栄樹が生えておったのだ。");

                                UpdateMainMessage("アイン：古代栄樹？？　伝説上のおとぎ話じゃないんですか？");

                                UpdateMainMessage("ガンツ：今ではおとぎ話として伝えられておるのは、事実だ。");

                                UpdateMainMessage("ガンツ：古代栄樹は一旦その姿を消滅した後、全く新しい場所で再生が行われる。");

                                UpdateMainMessage("ガンツ：その話自体は真実ではあるが、それを信じる者はこの今の時代では数少なかろう。");

                                UpdateMainMessage("アイン：っで、このゴツゴツした木の幹の一部みたいなのが・・・？");

                                UpdateMainMessage("ガンツ：そう、これこそまさしく古代栄樹木の幹の断片。よくぞ手に入れた。");

                                UpdateMainMessage("ガンツ：アインよ、すまんがこれをワシに託してもらえんかね？");

                                UpdateMainMessage("アイン：っえ！？");

                                UpdateMainMessage("ラナ：っちょっと、そこのバカアイン。何考えてるのよ？");

                                UpdateMainMessage("アイン：っい、いやいやいや。別に何も考えてねえさ、ッハッハッハ！");

                                UpdateMainMessage("ラナ：ふ～ん、ならいいんだけど♪");

                                UpdateMainMessage("アイン：と、当然だろ！？　何一つやましい事は考えてねえさ！");

                                UpdateMainMessage("ラナ：ッフフ、自爆しなくても良いのに♪");

                                UpdateMainMessage("アイン：っあ！ったく・・・まあ良いか。");

                                UpdateMainMessage("アイン：いや、何でもねえんだ。おじさん、受け取ってくれ。");

                                UpdateMainMessage("ガンツ：心遣い、感謝する。");

                                UpdateMainMessage("ガンツ：この素材を使って、一つワシなりの最高傑作を作ってみせよう。");

                                UpdateMainMessage("アイン：ッマジで！！！");

                                UpdateMainMessage("ガンツ：二言はない。");

                                UpdateMainMessage("ガンツ：出来上がったら、こちらから連絡する。楽しみにしておれ。");

                                UpdateMainMessage("アイン：やった！　すげぇ、楽しみだぜ！！");

                                we.GanzGift1 = true;
                                SellBackPackItem(backpackData, ((Label)sender), stack, ii);
                                return;
                            }
                            break;
                        // e 後編
                        case "タイム・オブ・ルーセ":
                            if (!we.AvailableEquipShop5)
                            {
                                OKRequest ok = new OKRequest();
                                ok.StartPosition = FormStartPosition.Manual;
                                ok.Location = new Point(this.Location.X + OK_LOCATION_X, this.Location.Y + OK_LOCATION_Y); // 後編編集
                                mainMessage.Text = "アイン：ガンツ叔父さん、これはいくらぐらいだ？";
                                mainMessage.Update();
                                ok.ShowDialog();
                                mainMessage.Text = "ガンツ：ん？おお、どれ見せてみなさい。";
                                mainMessage.Update();
                                ok.ShowDialog();
                                mainMessage.Text = "ガンツ：なんと！！　アイン、これはどこで見つけた？";
                                mainMessage.Update();
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：最下層の一直線の所の手前で妙に色が違う壁があったんだ。";
                                mainMessage.Update();
                                ok.ShowDialog();
                                mainMessage.Text = "ガンツ：そうか・・・大したものだ。でかしたぞアイン。";
                                mainMessage.Update();
                                ok.ShowDialog();
                                mainMessage.Text = "ガンツ：すまんが、これをワシに託してもらえないかね？";
                                mainMessage.Update();
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：アイン、叔父さんがせっかくこう言ってるんだから、渡しておけば？";
                                mainMessage.Update();
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：そうだなあ・・・渡してしまうか？";
                                mainMessage.Update();
                                using (YesNoRequestMini ynr = new YesNoRequestMini())
                                {
                                    ynr.Large = this.LayoutLarge; // 後編追加
                                    ynr.Location = new Point(this.Location.X + YESNO_LOCATION_X, this.Location.Y + YESNO_LOCATION_X); // 後編編集
                                    ynr.ShowDialog();
                                    if (ynr.DialogResult == DialogResult.Yes)
                                    {
                                        mainMessage.Text = "アイン：まあ正直俺が持っていても何に使えるか全然分からねえしな。";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "アイン：叔父さん、受け取ってくれ！";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "ガンツ：アインよ、恩にきる。";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "ラナ：それ何に使うものなんですか？";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "ガンツ：ある伝説の武具を作成するためのモノと言われておる。";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "ラナ：神の七遺産とは違うんですか？";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "ガンツ：神の七遺産とは違うものだとヴァスタ爺は言っておった。";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "ガンツ：ワシはこの『タイム・オブ・ルーセ』を持ってヴァスタ爺に会いに行こうと思う。";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "アイン：ヴァスタ爺さんが何か知ってるっていうのか？";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "ガンツ：ああ、そうだ。ワシらの間での昔からの約束だ。";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "アイン：そうか、良かったじゃねえか！約束果たせそうで！";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "ガンツ：ああ、本当に感謝するぞ、アインよ。";
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
                                        mainMessage.Text = "アイン：叔父さん・・・これっていくらぐらいだ？";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "ラナ：アイン！　ちょっとそれは無いんじゃない！？";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "ガンツ：値段は付けようが無い、すまないが買い取りというのは出来ん。";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        mainMessage.Text = "ラナ：ちょっとアイン、素直に渡してあげてよ？";
                                        mainMessage.Update();
                                        ok.ShowDialog();
                                        while (true)
                                        {
                                            using (YesNoRequestMini ynr2 = new YesNoRequestMini())
                                            {
                                                ynr2.Large = this.LayoutLarge; // 後編追加
                                                ynr2.Location = new Point(this.Location.X + YESNO_LOCATION_X, this.Location.Y + YESNO_LOCATION_Y); // 後編編集
                                                ynr2.ShowDialog();
                                                if (ynr2.DialogResult == DialogResult.Yes)
                                                {
                                                    mainMessage.Text = "アイン：まあ正直俺が持っていても何に使えるか全然分からねえしな。";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：叔父さん、受け取ってくれ！";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ガンツ：アインよ、恩にきる。";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：それ何に使うものなんですか？";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ガンツ：ある伝説の武具を作成するためのモノと言われておる。";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：神の七遺産とは違うんですか？";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ガンツ：神の七遺産とは違うものだとヴァスタ爺は言っておった。";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ガンツ：ワシはこの『タイム・オブ・ルーセ』を持ってヴァスタ爺に会いに行こうと思う。";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：ヴァスタ爺さんが何か知ってるっていうのか？";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ガンツ：ああ、そうだ。ワシらの間での昔からの約束だ。";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：そうか、良かったじゃねえか！約束果たせそうで！";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ガンツ：ああ、本当に感謝するぞ、アインよ。";
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
                                                    mainMessage.Text = "アイン：叔父さん・・・これって・・・・いくら・・・";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：ジィーーー・・・（白い目）";
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
                                MessageExchange5(); // 後編編集
                                return;
                            }

                        // [コメント]：特別なアイテムの場合別会話を繰り広げてください。
                        case "剣紋章ペンダント": // ラナレベルアップ時でもらえるアイテム
                            SetupMessageText(3008, (backpackData.Cost / 2).ToString());
                            break;


                        default:
                            if (backpackData.Cost <= 0)
                            {
                                MessageExchange5(); // 後編編集
                                return;
                            }
                            // s 後編追加
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
                            // e 後編追加
                            else
                            {
                                // s 後編編集
                                stack = SelectSellStackValue(sender, e, backpackData, ii);
                                if (stack == -1) return; // 複数量指定の時、ESCキャンセルはｰ1で抜けてくるので、即時Return

                                MessageExchange6(backpackData, stack, ii);
                            }
                            break;
                    }
                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                    {
                        yesno.Large = this.LayoutLarge; // 後編追加
                        yesno.Location = new Point(this.Location.X + YESNO_LOCATION_X, this.Location.Y + YESNO_LOCATION_Y); // 後編編集
                        yesno.ShowDialog();
                        if (yesno.DialogResult == DialogResult.Yes)
                        {
                            mc.Gold += stack * backpackData.Cost / 2; // 後編編集
                            label2.Text = mc.Gold.ToString() + "[G]"; // [警告]：ゴールドの所持は別クラスにするべきです。
                            // this.currentPlayer.DeleteBackPack(backpackData);
                            SellBackPackItem(backpackData, ((Label)sender), stack, ii);
                            MessageExchange3(); // 後編編集
                        }
                        else
                        {
                            MessageExchange4(); // 後編編集
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
                messageOK.Location = new Point(this.Location.X + OK_LOCATION_X, this.Location.Y + OK_LOCATION_Y); // 後編編集
            }
            mainMessage.Text = message;
            mainMessage.Update();
            messageOK.ShowDialog();
        }

        // s 後編編集
        protected virtual int SelectSellStackValue(object sender, EventArgs e, ItemBackPack backpackData, int ii)
        {
            // 前編では何もしないが、後編では数量指定の売却があるため、空のインタフェースを構築しておく。
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
            mainMessage.Text = String.Format(ganz.GetCharacterSentence(3001), backpackData.Name, backpackData.Cost.ToString()); // 後編編集
        }
        // e 後編編集

        protected virtual void SellBackPackItem(ItemBackPack backpackData, Label sender, int stack, int ii)
        {
            this.currentPlayer.DeleteBackPack(backpackData, stack);
            ((Label)sender).Text = "";
            ((Label)sender).Cursor = System.Windows.Forms.Cursors.Default;
        }

        // s 後編編集
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
        // e 後編編集

        protected virtual void UpdateBackPackLabel(MainCharacter target) // 後編編集
        {
            label11.Text = "バックパック（" + target.Name + ")";

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
                            case ItemBackPack.RareLevel.Legendary: // 後編追加
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
                case ItemBackPack.RareLevel.Legendary: // 後編追加
                    target.BackColor = Color.OrangeRed;
                    target.ForeColor = Color.White;
                    break;
            }
        }

        protected virtual void SetupAvailableList(int level) // c 後編編集
        {
            ItemBackPack item = null;
            switch (level)
            {
                case 1:
                    item = new ItemBackPack("ショートソード");
                    equipList[0].Text = item.Name;
                    UpdateRareColor(item, equipList[0]);

                    item = new ItemBackPack("洗練されたロングソード");
                    equipList[1].Text = item.Name;
                    UpdateRareColor(item, equipList[1]);

                    item = new ItemBackPack("冒険者用の鎖かたびら");
                    equipList[2].Text = item.Name;
                    UpdateRareColor(item, equipList[2]);

                    item = new ItemBackPack("青銅の鎧");
                    equipList[3].Text = item.Name;
                    UpdateRareColor(item, equipList[3]);

                    item = new ItemBackPack("些細なパワーリング");
                    equipList[4].Text = item.Name;
                    UpdateRareColor(item, equipList[4]);

                    item = new ItemBackPack("神剣  フェルトゥーシュ");
                    equipList[5].Text = item.Name;
                    equipList[5].Font = new Font("MS UI Gothic", 10F, FontStyle.Strikeout);
                    costList[5].Font = new Font("MS UI Gothic", 10F, FontStyle.Strikeout);
                    UpdateRareColor(item, equipList[5]);

                    equipList[6].Text = "";

                    equipList[7].Text = "";
                    break;

                case 2:
                    item = new ItemBackPack("青銅の剣");
                    equipList[0].Text = item.Name;
                    UpdateRareColor(item, equipList[0]);

                    item = new ItemBackPack("メタルフィスト");
                    equipList[1].Text = item.Name;
                    UpdateRareColor(item, equipList[1]);

                    item = new ItemBackPack("光沢のある鉄のプレート");
                    equipList[2].Text = item.Name;
                    UpdateRareColor(item, equipList[2]);

                    item = new ItemBackPack("シルクの武道衣");
                    equipList[3].Text = item.Name;
                    UpdateRareColor(item, equipList[3]);

                    item = new ItemBackPack("紺碧のスターエムブレム");
                    equipList[4].Text = item.Name;
                    UpdateRareColor(item, equipList[4]);

                    item = new ItemBackPack("闘魂バンド");
                    equipList[5].Text = item.Name;
                    equipList[5].Font = new Font("MS UI Gothic", 10F, FontStyle.Underline);
                    costList[5].Font = new Font("MS UI Gothic", 10F, FontStyle.Bold);
                    UpdateRareColor(item, equipList[5]);

                    equipList[6].Text = "";

                    equipList[7].Text = "";
                    break;

                case 3:
                    item = new ItemBackPack("プラチナソード");
                    equipList[0].Text = item.Name;
                    UpdateRareColor(item, equipList[0]);

                    item = new ItemBackPack("アイアンクロー");
                    equipList[1].Text = item.Name;
                    UpdateRareColor(item, equipList[1]);

                    item = new ItemBackPack("シルバーアーマー");
                    equipList[2].Text = item.Name;
                    UpdateRareColor(item, equipList[2]);

                    item = new ItemBackPack("獣皮製の舞踏衣");
                    equipList[3].Text = item.Name;
                    UpdateRareColor(item, equipList[3]);

                    item = new ItemBackPack("ウェルニッケの腕輪");
                    equipList[4].Text = item.Name;
                    UpdateRareColor(item, equipList[4]);

                    item = new ItemBackPack("賢者の眼鏡");
                    equipList[5].Text = item.Name;
                    equipList[5].Font = new Font("MS UI Gothic", 10F, FontStyle.Underline);
                    costList[5].Font = new Font("MS UI Gothic", 10F, FontStyle.Bold);
                    UpdateRareColor(item, equipList[5]);

                    item = new ItemBackPack("ファルシオン");
                    equipList[6].Text = item.Name;
                    UpdateRareColor(item, equipList[6]);

                    item = new ItemBackPack("フィスト・クロス");
                    equipList[7].Text = item.Name;
                    UpdateRareColor(item, equipList[7]);
                    break;

                case 4:
                    item = new ItemBackPack("プリズマティックアーマー");
                    equipList[0].Text = item.Name;
                    UpdateRareColor(item, equipList[0]);

                    item = new ItemBackPack("極薄合金製の羽衣");
                    equipList[1].Text = item.Name;
                    UpdateRareColor(item, equipList[1]);

                    item = new ItemBackPack("ライトプラズマブレード");
                    equipList[2].Text = item.Name;
                    UpdateRareColor(item, equipList[2]);

                    item = new ItemBackPack("イスリアルフィスト");
                    equipList[3].Text = item.Name;
                    UpdateRareColor(item, equipList[3]);

                    item = new ItemBackPack("七色プリズムバンド");
                    equipList[4].Text = item.Name;
                    UpdateRareColor(item, equipList[4]);

                    item = new ItemBackPack("再生の紋章");
                    equipList[5].Text = item.Name;
                    equipList[5].Font = new Font("MS UI Gothic", 10F, FontStyle.Underline);
                    costList[5].Font = new Font("MS UI Gothic", 10F, FontStyle.Bold);
                    UpdateRareColor(item, equipList[5]);

                    item = new ItemBackPack("シールオブアクア＆ファイア");
                    equipList[6].Text = item.Name;
                    UpdateRareColor(item, equipList[6]);

                    item = new ItemBackPack("ドラゴンのベルト");
                    equipList[7].Text = item.Name;
                    UpdateRareColor(item, equipList[7]);
                    break;

                case 5:
                    item = new ItemBackPack("プライド・オブ・シーカー");
                    equipList[0].Text = item.Name;
                    UpdateRareColor(item, equipList[0]);

                    item = new ItemBackPack("詩聖水宝の勾玉");
                    equipList[1].Text = item.Name;
                    UpdateRareColor(item, equipList[1]);

                    item = new ItemBackPack("ディセンションブーツ");
                    equipList[2].Text = item.Name;
                    UpdateRareColor(item, equipList[2]);

                    item = new ItemBackPack("ハートブレーカー");
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

        // s 後編追加
        protected virtual void EquipmentShop_Shown(object sender, EventArgs e)
        {
            // 前編では何もしないが、後編で新規アイテム追加があるため、空のインタフェースを構築しておく。
        }
        // e 後編追加
    }
}