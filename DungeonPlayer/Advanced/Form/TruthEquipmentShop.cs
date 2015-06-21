using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class TruthEquipmentShop : EquipmentShop
    {
        protected string titleName = String.Empty;
        public string TitleName
        {
            get { return titleName; }
            set
            {
                titleName = value;
                if (this.label1 != null) { this.label1.Text = titleName; }
            }
        }
        protected Label[] backpackStack;

        public TruthEquipmentShop()
        {
            this.KeyDown += new KeyEventHandler(EquipmentShop_KeyDown);
            this.KeyUp += new KeyEventHandler(TruthEquipmentShop_KeyUp);

            base.Width = Database.WIDTH_1024;
            base.Height = Database.HEIGHT_768;
            base.mainMessage.Width = Database.WIDTH_1024;
            base.mainMessage.Height = Database.HEIGHT_MAIN_MESSAGE;
            base.mainMessage.Location = new Point(0, Database.HEIGHT_768 - Database.HEIGHT_MAIN_MESSAGE);
            base.button1.Location = new Point(Database.WIDTH_1024 - Database.WIDTH_OK_BUTTON, Database.HEIGHT_768 - Database.HEIGHT_MAIN_MESSAGE);
            base.label2.Location = new Point(base.label2.Location.X, Database.HEIGHT_768 - Database.HEIGHT_MAIN_MESSAGE - 30);

            YESNO_LOCATION_X = 784;
            YESNO_LOCATION_Y = 708;

            base.LayoutLarge = true;
        }

        protected override void OnInitializeLayout()
        {
            base.OnInitializeLayout();

            OK_LOCATION_X = 904;
            OK_LOCATION_Y = 708;
            OK_SIZE_X = 120;
            OK_SIZE_Y = 60;
            button1.Size = new Size(OK_SIZE_X, OK_SIZE_Y);
            

            for (int ii = 0; ii < MAX_EQUIPLIST; ii++)
            {
                equipList[ii].Font = new System.Drawing.Font("MS UI Gothic", 13f, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                equipList[ii].Location = new System.Drawing.Point(50, 100 + 24 * ii); // 後編編集

                costList[ii].Font = new System.Drawing.Font("MS UI Gothic", 13f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                costList[ii].Location = new System.Drawing.Point(50 + 200, 100 + 24 * ii); // 後編編集
            }

            backpackStack = new Label[Database.MAX_BACKPACK_SIZE];
            for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
            {
                backpackList[ii].Font = new System.Drawing.Font("MS UI Gothic", 13f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                backpackList[ii].Location = new System.Drawing.Point(50 + 400, 116 + 29 * ii);

                backpackStack[ii] = new Label();
                backpackStack[ii].Font = new System.Drawing.Font("MS UI Gothic", 13f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                backpackStack[ii].Location = new System.Drawing.Point(420, 116 + 29 * ii);
                backpackStack[ii].Name = "backpackList" + ii.ToString();
                backpackStack[ii].Size = new Size(200, 12);
                backpackStack[ii].TabIndex = 0;
                backpackStack[ii].AutoSize = true;
                this.Controls.Add(backpackStack[ii]);
            }


            if (titleName != String.Empty)
            {
                this.label1.Text = titleName;
            }
        }

        bool IsShift = false;
        protected void TruthEquipmentShop_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                this.IsShift = false;
            }
        }
        protected override void EquipmentShop_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                button1_Click(null, null);
            }
            else if (e.KeyCode == Keys.ShiftKey)
            {
                this.IsShift = true;
            }
        }

       
        protected override void UpdateBackPackLabelInterface(MainCharacter target)
        {
            //base.UpdateBackPackLabel(target);

            ItemBackPack[] temp = target.GetBackPackInfo();

            for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
            {
                int baseNumber = 0;

                if (temp[ii + baseNumber] != null)
                {
                    backpackList[ii].Text = temp[ii + baseNumber].Name;
                    backpackList[ii].Cursor = System.Windows.Forms.Cursors.Hand;
                    switch (temp[ii + baseNumber].Rare)
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
                        case ItemBackPack.RareLevel.Legendary:
                            backpackList[ii].BackColor = Color.OrangeRed;
                            backpackList[ii].ForeColor = Color.White;
                            break;
                    }

                    backpackStack[ii].Text = "x" + temp[ii + baseNumber].StackValue.ToString();

                }
                else
                {
                    backpackList[ii].Text = "";
                    backpackList[ii].Cursor = System.Windows.Forms.Cursors.Default;

                    backpackStack[ii].Text = "";
                    backpackStack[ii].Cursor = System.Windows.Forms.Cursors.Default;
                }
            }

            //for (int jj = 10; jj < Database.MAX_BACKPACK_SIZE; jj++)
            //{
            //    backpackList[jj].Visible = false;
            //    backpackStack[jj].Visible = false;
            //}
        }

        protected override int SelectSellStackValue(object sender, EventArgs e, ItemBackPack backpackData, int ii)
        {
            int exchangeValue = Convert.ToInt32(backpackStack[ii].Text.Remove(0, 1), 10);
            if (this.IsShift)
            {
                using (SelectValue sv = new SelectValue())
                {
                    sv.StartPosition = FormStartPosition.Manual;
                    sv.Location = new Point(this.Location.X + backpackStack[ii].Location.X, this.Location.Y + backpackStack[ii].Location.Y + 15);
                    sv.MaxValue = exchangeValue;
                    sv.ShowDialog();
                    IsShift = false; // ShowDialog表示先で、Shiftキーは外された場合検知できないため、ココでリセット。
                    if (sv.DialogResult == DialogResult.Cancel) return -1; // ESCキャンセルは中断とみなす。
                    return sv.CurrentValue;
                }
            }
            else
            {
                return exchangeValue;
            }
        }

        protected override void SellBackPackItem(ItemBackPack backpackData, Label sender, int stack, int ii)
        {
            int MaxStack = Convert.ToInt32(backpackStack[ii].Text.Remove(0, 1), 10);
            int updateWE2Value = 0;

            if (stack >= MaxStack)
            {
                this.currentPlayer.DeleteBackPack(backpackData);
                sender.Text = "";
                sender.Cursor = System.Windows.Forms.Cursors.Default;
                backpackStack[ii].Text = "";
                updateWE2Value = MaxStack;
            }
            else
            {
                this.currentPlayer.DeleteBackPack(backpackData, stack);
                backpackStack[ii].Text = "x" + Convert.ToString(MaxStack - stack);
                updateWE2Value = MaxStack - stack;
            }

            // 素材売却情報を記憶する。
            #region "武具"
            #region "１階"
            if (backpackData.Name == Database.COMMON_WARM_NO_KOUKAKU)
            {
                GroundOne.WE2.EquipMaterial_11 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_BEATLE_TOGATTA_TUNO)
            {
                GroundOne.WE2.EquipMaterial_12 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_TAKA_FETHER)
            {
                GroundOne.WE2.EquipMaterial_13 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_SUN_LEAF)
            {
                GroundOne.WE2.EquipMaterial_14 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_OOKAMI_FANG)
            {
                GroundOne.WE2.EquipMaterial_15 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_TOGE_HAETA_SYOKUSYU)
            {
                GroundOne.WE2.EquipMaterial_16 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_ORANGE_MATERIAL)
            {
                GroundOne.WE2.EquipMaterial_17 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_YELLOW_MATERIAL)
            {
                GroundOne.WE2.EquipMaterial_18 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_BLUE_COPPER)
            {
                GroundOne.WE2.EquipMaterial_19 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_RABBIT_KEGAWA)
            {
                GroundOne.WE2.EquipMaterial_110 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_SPIDER_SILK)
            {
                GroundOne.WE2.EquipMaterial_111 += updateWE2Value;
            }
            #endregion
            #region "２階"
            else if (backpackData.Name == Database.COMMON_WHITE_MAGATAMA)
            {
                GroundOne.WE2.EquipMaterial_21 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_BLUE_MAGATAMA)
            {
                GroundOne.WE2.EquipMaterial_22 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_WASI_BLUE_FEATHER)
            {
                GroundOne.WE2.EquipMaterial_23 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_BLUEWHITE_SHARP_TOGE)
            {
                GroundOne.WE2.EquipMaterial_24 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_GOTUGOTU_KARA)
            {
                GroundOne.WE2.EquipMaterial_25 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_SEKIKASSYOKU_HASAMI)
            {
                GroundOne.WE2.EquipMaterial_26 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_JOE_ARM)
            {
                GroundOne.WE2.EquipMaterial_27 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_AOSAME_KENSHI)
            {
                GroundOne.WE2.EquipMaterial_28 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_KOUSITUKA_MATERIAL)
            {
                GroundOne.WE2.EquipMaterial_29 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_AOSAME_UROKO)
            {
                GroundOne.WE2.EquipMaterial_210 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_HALF_TRANSPARENT_ROCK_ASH)
            {
                GroundOne.WE2.EquipMaterial_211 += updateWE2Value;
            }
            #endregion
            #region "３階"
            else if (backpackData.Name == Database.COMMON_SNOW_CAT_KEGAWA)
            {
                GroundOne.WE2.EquipMaterial_31 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_LIZARD_UROKO)
            {
                GroundOne.WE2.EquipMaterial_32 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_GOTUGOTU_KONBOU)
            {
                GroundOne.WE2.EquipMaterial_33 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_EMBLEM_OF_PENGUIN)
            {
                GroundOne.WE2.EquipMaterial_34 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_ARGONIAN_PURPLE_UROKO)
            {
                GroundOne.WE2.EquipMaterial_35 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_BEAR_CLAW_KAKERA)
            {
                GroundOne.WE2.EquipMaterial_36 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_ESSENCE_OF_EARTH)
            {
                GroundOne.WE2.EquipMaterial_37 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_WOLF_KEGAWA)
            {
                GroundOne.WE2.EquipMaterial_38 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_TUNDRA_DEER_HORN)
            {
                GroundOne.WE2.EquipMaterial_39 += updateWE2Value;
            }
            else if (backpackData.Name == Database.EPIC_OLD_TREE_MIKI_DANPEN)
            {
                GroundOne.WE2.EquipMaterial_310 += updateWE2Value;
            }
            #endregion
            #region "４階"
            else if (backpackData.Name == Database.COMMON_HUNTER_SEVEN_TOOL)
            {
                GroundOne.WE2.EquipMaterial_41 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_BEAST_KEGAWA)
            {
                GroundOne.WE2.EquipMaterial_42 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_EXECUTIONER_ROBE)
            {
                GroundOne.WE2.EquipMaterial_43 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_ANGEL_SILK)
            {
                GroundOne.WE2.EquipMaterial_44 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_SABI_BUGU)
            {
                GroundOne.WE2.EquipMaterial_45 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_ESSENCE_OF_DARK)
            {
                GroundOne.WE2.EquipMaterial_46 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_SEEKER_HEAD)
            {
                GroundOne.WE2.EquipMaterial_47 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_MASTERBLADE_KAKERA)
            {
                GroundOne.WE2.EquipMaterial_48 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_ESSENCE_OF_FLAME)
            {
                GroundOne.WE2.EquipMaterial_49 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_GREAT_JEWELCROWN)
            {
                GroundOne.WE2.EquipMaterial_410 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_ONRYOU_HAKO)
            {
                GroundOne.WE2.EquipMaterial_411 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_KUMITATE_TENBIN)
            {
                GroundOne.WE2.EquipMaterial_412 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_KUMITATE_TENBIN_DOU)
            {
                GroundOne.WE2.EquipMaterial_413 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_KUMITATE_TENBIN_BOU)
            {
                GroundOne.WE2.EquipMaterial_414 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_DOOMBRINGER_TUKA)
            {
                GroundOne.WE2.EquipMaterial_415 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_DOOMBRINGER_KAKERA)
            {
                GroundOne.WE2.EquipMaterial_416 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_DOOMBRINGER)
            {
                GroundOne.WE2.EquipMaterial_417 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_JOUKA_TANZOU)
            {
                GroundOne.WE2.EquipMaterial_418 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_ESSENCE_OF_ADAMANTINE)
            {
                GroundOne.WE2.EquipMaterial_419 += updateWE2Value;
            }

            #endregion
            #endregion
            #region "ポーション/強化薬"
            #region "１階"
            else if (backpackData.Name == Database.COMMON_GREEN_SIKISO)
            {
                GroundOne.WE2.PotionMaterial_11 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_POISON_EKISU)
            {
                GroundOne.WE2.PotionMaterial_12 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_RED_HOUSI)
            {
                GroundOne.WE2.PotionMaterial_13 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_MARY_KISS)
            {
                GroundOne.WE2.PotionMaterial_14 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_ALRAUNE_KAHUN)
            {
                GroundOne.WE2.PotionMaterial_15 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_HYUI_SEED)
            {
                GroundOne.WE2.PotionMaterial_16 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_NEBARIITO_KUMO)
            {
                GroundOne.WE2.PotionMaterial_17 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_MANDORAGORA_ROOT)
            {
                GroundOne.WE2.PotionMaterial_18 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_BRILLIANT_RINPUN)
            {
                GroundOne.WE2.PotionMaterial_19 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_MOSSGREEN_EKISU)
            {
                GroundOne.WE2.PotionMaterial_110 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_DREAM_POWDER)
            {
                GroundOne.WE2.PotionMaterial_111 += updateWE2Value;
            }
            #endregion
            #region "２階"
            else if (backpackData.Name == Database.COMMON_GANGAME_EGG)
            {
                GroundOne.WE2.PotionMaterial_21 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_NANAIRO_SYOKUSYU)
            {
                GroundOne.WE2.PotionMaterial_22 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_EIGHTEIGHT_KUROSUMI)
            {
                GroundOne.WE2.PotionMaterial_23 += updateWE2Value;
            }
            #endregion
            #region "３階"
            else if (backpackData.Name == Database.COMMON_FAIRY_POWDER)
            {
                GroundOne.WE2.PotionMaterial_31 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_ESSENCE_OF_WIND)
            {
                GroundOne.WE2.PotionMaterial_32 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_FROZEN_HEART)
            {
                GroundOne.WE2.PotionMaterial_33 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_SHARPNESS_TIGER_TOOTH)
            {
                GroundOne.WE2.PotionMaterial_34 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_PURE_CRYSTAL)
            {
                GroundOne.WE2.PotionMaterial_35 += updateWE2Value;
            }
            #endregion
            #region "４階"
            else if (backpackData.Name == Database.RARE_BLOOD_DAGGER_KAKERA)
            {
                GroundOne.WE2.PotionMaterial_41 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_DEMON_HORN)
            {
                GroundOne.WE2.PotionMaterial_42 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_ESSENCE_OF_SHINE)
            {
                GroundOne.WE2.PotionMaterial_43 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_BLACK_SEAL_IMPRESSION)
            {
                GroundOne.WE2.PotionMaterial_44 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_CHAOS_SIZUKU)
            {
                GroundOne.WE2.PotionMaterial_45 += updateWE2Value;
            }
            #endregion
            #endregion
            #region "料理素材"
            #region "１階"
            else if (backpackData.Name == Database.COMMON_INAGO)
            {
                GroundOne.WE2.FoodMaterial_11 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_RABBIT_MEAT)
            {
                GroundOne.WE2.FoodMaterial_12 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_PLANTNOID_SEED)
            {
                GroundOne.WE2.FoodMaterial_13 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_GREEN_EGG_KAIGARA)
            {
                GroundOne.WE2.FoodMaterial_14 += updateWE2Value;
            }
            #endregion
            #region "２階"
            else if (backpackData.Name == Database.COMMON_SEA_WASI_KUTIBASI)
            {
                GroundOne.WE2.FoodMaterial_21 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_JOE_TONGUE)
            {
                GroundOne.WE2.FoodMaterial_22 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_JOE_LEG)
            {
                GroundOne.WE2.FoodMaterial_23 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_DAGGERFISH_UROKO)
            {
                GroundOne.WE2.FoodMaterial_24 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_PURE_WHITE_BIGEYE)
            {
                GroundOne.WE2.FoodMaterial_25 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_SIPPUU_HIRE)
            {
                GroundOne.WE2.FoodMaterial_26 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_SOFT_BIG_HIRE)
            {
                GroundOne.WE2.FoodMaterial_27 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_KURIONE_ZOUMOTU)
            {
                GroundOne.WE2.FoodMaterial_28 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_EIGHTEIGHT_KYUUBAN)
            {
                GroundOne.WE2.FoodMaterial_29 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_RENEW_AKAMI)
            {
                GroundOne.WE2.FoodMaterial_210 += updateWE2Value;
            }
            #endregion
            #region "３階"
            else if (backpackData.Name == Database.COMMON_WHITE_AZARASHI_MEAT)
            {
                GroundOne.WE2.FoodMaterial_31 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_KESSYOU_SEA_WATER_SALT)
            {
                GroundOne.WE2.FoodMaterial_32 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_ORC_MOMONIKU)
            {
                GroundOne.WE2.FoodMaterial_33 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_RED_ONION)
            {
                GroundOne.WE2.FoodMaterial_34 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_BIG_HIZUME)
            {
                GroundOne.WE2.FoodMaterial_35 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_WHITE_POWDER)
            {
                GroundOne.WE2.FoodMaterial_36 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_BLUE_DANGAN_KAKERA)
            {
                GroundOne.WE2.FoodMaterial_37 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_TOUMEI_SNOW_CRYSTAL)
            {
                GroundOne.WE2.FoodMaterial_38 += updateWE2Value;
            }
            #endregion
            #region "４階"
            else if (backpackData.Name == Database.COMMON_BLACK_SALT)
            {
                GroundOne.WE2.FoodMaterial_41 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_FEBL_ANIS)
            {
                GroundOne.WE2.FoodMaterial_42 += updateWE2Value;
            }
            else if (backpackData.Name == Database.RARE_MASTERBLADE_FIRE)
            {
                GroundOne.WE2.FoodMaterial_43 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_SMORKY_HUNNY)
            {
                GroundOne.WE2.FoodMaterial_44 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_ANGEL_DUST)
            {
                GroundOne.WE2.FoodMaterial_45 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_SUN_TARAGON)
            {
                GroundOne.WE2.FoodMaterial_46 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_ECHO_BEAST_MEAT)
            {
                GroundOne.WE2.FoodMaterial_47 += updateWE2Value;
            }
            else if (backpackData.Name == Database.COMMON_CHAOS_TONGUE)
            {
                GroundOne.WE2.FoodMaterial_48 += updateWE2Value;
            }            
            #endregion
            #endregion

            // 獲得した素材が調合の条件を満たしている場合、調合日を記憶する。
            #region "武具"
            #region "１階"
            if (GroundOne.WE2.EquipMaterial_11 >= 1 && GroundOne.WE2.EquipMaterial_12 >= 1 && GroundOne.WE2.EquipMixtureDay_11 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_11 = WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_13 >= 1 && GroundOne.WE2.EquipMaterial_14 >= 1 && GroundOne.WE2.EquipMixtureDay_12 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_12 = WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_15 >= 1 && GroundOne.WE2.EquipMaterial_16 >= 1 && GroundOne.WE2.EquipMaterial_17 >= 1 && GroundOne.WE2.EquipMixtureDay_13 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_13 = WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_18 >= 1 && GroundOne.WE2.EquipMaterial_19 >= 1 && GroundOne.WE2.EquipMixtureDay_14 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_14 = WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_110 >= 1 && GroundOne.WE2.EquipMaterial_111 >= 1 && GroundOne.WE2.EquipMixtureDay_15 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_15 = WE.GameDay;
            }
            #endregion
            #region "２階"
            if (GroundOne.WE2.EquipMaterial_21 >= 1 && GroundOne.WE2.EquipMaterial_22 >= 1 && GroundOne.WE2.EquipMixtureDay_21 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_21 = WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_23 >= 1 && GroundOne.WE2.EquipMaterial_24 >= 1 && GroundOne.WE2.EquipMixtureDay_22 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_22 = WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_25 >= 1 && GroundOne.WE2.EquipMixtureDay_23 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_23 = WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_26 >= 1 && GroundOne.WE2.EquipMaterial_27 >= 1 && GroundOne.WE2.EquipMixtureDay_24 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_24 = WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_28 >= 1 && GroundOne.WE2.EquipMaterial_29 >= 1 && GroundOne.WE2.EquipMixtureDay_25 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_25 = WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_210 >= 1 && GroundOne.WE2.EquipMaterial_211 >= 1 && GroundOne.WE2.EquipMixtureDay_26 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_26 = WE.GameDay;
            }
            #endregion
            #region "３階"
            if (GroundOne.WE2.EquipMaterial_31 >= 1 && GroundOne.WE2.EquipMixtureDay_31 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_31 = WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_32 >= 1 && GroundOne.WE2.EquipMixtureDay_32 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_32 = WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_33 >= 1 && GroundOne.WE2.EquipMixtureDay_33 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_33 = WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_34 >= 1 && GroundOne.WE2.EquipMixtureDay_34 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_34 = WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_35 >= 1 && GroundOne.WE2.EquipMixtureDay_35 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_35 = WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_36 >= 1 &&
                GroundOne.WE2.EquipMaterial_37 >= 1 && GroundOne.WE2.EquipMixtureDay_36 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_36 = WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_38 >= 1 && GroundOne.WE2.EquipMixtureDay_37 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_37 = WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_39 >= 1 && 
                GroundOne.WE2.EquipMaterial_310 >= 1 && GroundOne.WE2.EquipMixtureDay_38 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_38 = WE.GameDay;
            }
            #endregion
            #region "４階"
            if (GroundOne.WE2.EquipMaterial_41 >= 1 && GroundOne.WE2.EquipMixtureDay_41 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_41 = WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_42 >= 1 && GroundOne.WE2.EquipMixtureDay_42 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_42 = WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_43 >= 1 &&
                GroundOne.WE2.EquipMaterial_44 >= 1 && GroundOne.WE2.EquipMixtureDay_43 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_43 = WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_45 >= 1 &&
                GroundOne.WE2.EquipMaterial_46 >= 1 && GroundOne.WE2.EquipMixtureDay_44 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_44 = WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_47 >= 1 &&
                GroundOne.WE2.EquipMaterial_48 >= 1 &&
                GroundOne.WE2.EquipMaterial_49 >= 1 && GroundOne.WE2.EquipMixtureDay_45 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_45 = WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_410 >= 1 &&
                GroundOne.WE2.EquipMaterial_411 >= 1 && GroundOne.WE2.EquipMixtureDay_46 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_46 = WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_412 >= 1 &&
                GroundOne.WE2.EquipMaterial_413 >= 1 &&
                GroundOne.WE2.EquipMaterial_414 >= 1 && GroundOne.WE2.EquipMixtureDay_47 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_47 = WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_415 >= 1 &&
                GroundOne.WE2.EquipMaterial_416 >= 1 && GroundOne.WE2.EquipMixtureDay_48 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_48 = WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_417 >= 1 &&
                GroundOne.WE2.EquipMaterial_418 >= 1 && GroundOne.WE2.EquipMixtureDay_49 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_49 = WE.GameDay;
            }
            if (GroundOne.WE2.EquipMaterial_418 >= 1 &&
                GroundOne.WE2.EquipMaterial_419 >= 1 && GroundOne.WE2.EquipMixtureDay_410 <= 0)
            {
                GroundOne.WE2.EquipMixtureDay_410 = WE.GameDay;
            }
            #endregion
            #endregion


            #region "ポーション/強化薬"
            #region "１階"
            if (GroundOne.WE2.PotionMaterial_11 >= 1 && GroundOne.WE2.PotionMaterial_12 >= 1 && GroundOne.WE2.PotionMixtureDay_11 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_11 = WE.GameDay;
            }
            if (GroundOne.WE2.PotionMaterial_13 >= 1 && GroundOne.WE2.PotionMaterial_14 >= 1 && GroundOne.WE2.PotionMixtureDay_12 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_12 = WE.GameDay;
            }
            if (GroundOne.WE2.PotionMaterial_15 >= 1 && GroundOne.WE2.PotionMaterial_16 >= 1 && GroundOne.WE2.PotionMaterial_17 >= 1 && GroundOne.WE2.PotionMixtureDay_13 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_13 = WE.GameDay;
            }
            if (GroundOne.WE2.PotionMaterial_18 >= 1 && GroundOne.WE2.PotionMaterial_19 >= 1 && GroundOne.WE2.PotionMixtureDay_14 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_14 = WE.GameDay;
            }
            if (GroundOne.WE2.PotionMaterial_110 >= 1 && GroundOne.WE2.PotionMaterial_111 >= 1 && GroundOne.WE2.PotionMixtureDay_15 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_15 = WE.GameDay;
            }
            #endregion
            #region "２階"
            if (GroundOne.WE2.PotionMaterial_21 >= 1 && GroundOne.WE2.PotionMixtureDay_21 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_21 = WE.GameDay;
            }
            if (GroundOne.WE2.PotionMaterial_22 >= 1 && GroundOne.WE2.PotionMixtureDay_22 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_22 = WE.GameDay;
            }
            if (GroundOne.WE2.PotionMaterial_23 >= 1 && GroundOne.WE2.PotionMixtureDay_23 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_23 = WE.GameDay;
            }
            #endregion
            #region "３階"
            if (GroundOne.WE2.PotionMaterial_31 >= 1 &&
                GroundOne.WE2.PotionMaterial_32 >= 1 && GroundOne.WE2.PotionMixtureDay_31 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_31 = WE.GameDay;
            }
            if (GroundOne.WE2.PotionMaterial_33 >= 1 && 
                GroundOne.WE2.PotionMaterial_34 >= 1 && GroundOne.WE2.PotionMixtureDay_32 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_32 = WE.GameDay;
            }
            if (GroundOne.WE2.PotionMaterial_35 >= 1 && GroundOne.WE2.PotionMixtureDay_33 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_33 = WE.GameDay;
            }
            #endregion
            #region "４階"
            if (GroundOne.WE2.PotionMaterial_41 >= 1 && GroundOne.WE2.PotionMixtureDay_41 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_41 = WE.GameDay;
                GroundOne.WE2.PotionMixtureDay_42 = WE.GameDay;
            }
            if (GroundOne.WE2.PotionMaterial_42 >= 1 && GroundOne.WE2.PotionMixtureDay_43 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_43 = WE.GameDay;
                GroundOne.WE2.PotionMixtureDay_44 = WE.GameDay;
            }
            if (GroundOne.WE2.PotionMaterial_43 >= 1 && GroundOne.WE2.PotionMixtureDay_45 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_45 = WE.GameDay;
                GroundOne.WE2.PotionMixtureDay_46 = WE.GameDay;
            }
            if (GroundOne.WE2.PotionMaterial_44 >= 1 && GroundOne.WE2.PotionMixtureDay_47 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_47 = WE.GameDay;
                GroundOne.WE2.PotionMixtureDay_48 = WE.GameDay;
            }
            if (GroundOne.WE2.PotionMaterial_45 >= 1 && GroundOne.WE2.PotionMixtureDay_49 <= 0)
            {
                GroundOne.WE2.PotionMixtureDay_49 = WE.GameDay;
                GroundOne.WE2.PotionMixtureDay_410 = WE.GameDay;
            }
            #endregion
            #endregion

            #region "食品"
            #region "１階"
            if (GroundOne.WE2.FoodMaterial_11 >= 1 && GroundOne.WE2.FoodMixtureDay_11 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_11 = WE.GameDay;
            }
            if (GroundOne.WE2.FoodMaterial_12 >= 1 && GroundOne.WE2.FoodMixtureDay_12 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_12 = WE.GameDay;
            }
            if (GroundOne.WE2.FoodMaterial_13 >= 1 && GroundOne.WE2.FoodMaterial_14 >= 1 && GroundOne.WE2.FoodMixtureDay_13 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_13 = WE.GameDay;
            }
            #endregion
            #region "２階"
            if (GroundOne.WE2.FoodMaterial_21 >= 1 && GroundOne.WE2.FoodMaterial_22 >= 1 && GroundOne.WE2.FoodMixtureDay_21 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_21 = WE.GameDay;
            }
            if (GroundOne.WE2.FoodMaterial_23 >= 1 && GroundOne.WE2.FoodMaterial_24 >= 1 && GroundOne.WE2.FoodMaterial_25 >= 1 && GroundOne.WE2.FoodMixtureDay_22 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_22 = WE.GameDay;
            }
            if (GroundOne.WE2.FoodMaterial_26 >= 1 && GroundOne.WE2.FoodMaterial_27 >= 1 && GroundOne.WE2.FoodMixtureDay_23 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_23 = WE.GameDay;
            }
            if (GroundOne.WE2.FoodMaterial_28 >= 1 && GroundOne.WE2.FoodMaterial_29 >= 1 && GroundOne.WE2.FoodMaterial_210 >= 1 && GroundOne.WE2.FoodMixtureDay_24 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_24 = WE.GameDay;
            }
            #endregion
            #region "３階"
            if (GroundOne.WE2.FoodMaterial_31 >= 1 && 
                GroundOne.WE2.FoodMaterial_32 >= 1 && GroundOne.WE2.FoodMixtureDay_31 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_31 = WE.GameDay;
            }
            if (GroundOne.WE2.FoodMaterial_33 >= 1 &&
                GroundOne.WE2.FoodMaterial_34 >= 1 && GroundOne.WE2.FoodMixtureDay_32 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_32 = WE.GameDay;
            }
            if (GroundOne.WE2.FoodMaterial_35 >= 1 &&
                GroundOne.WE2.FoodMaterial_36 >= 1 && GroundOne.WE2.FoodMixtureDay_33 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_33 = WE.GameDay;
            }
            if (GroundOne.WE2.FoodMaterial_37 >= 1 &&
                GroundOne.WE2.FoodMaterial_38 >= 1 && GroundOne.WE2.FoodMixtureDay_34 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_34 = WE.GameDay;
            }
            #endregion
            #region "４階"
            if (GroundOne.WE2.FoodMaterial_41 >= 1 &&
                GroundOne.WE2.FoodMaterial_42 >= 1 && GroundOne.WE2.FoodMixtureDay_41 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_41 = WE.GameDay;
            }
            if (GroundOne.WE2.FoodMaterial_43 >= 1 &&
                GroundOne.WE2.FoodMaterial_44 >= 1 && GroundOne.WE2.FoodMixtureDay_42 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_42 = WE.GameDay;
            }
            if (GroundOne.WE2.FoodMaterial_45 >= 1 &&
                GroundOne.WE2.FoodMaterial_46 >= 1 && GroundOne.WE2.FoodMixtureDay_43 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_43 = WE.GameDay;
            }
            if (GroundOne.WE2.FoodMaterial_47 >= 1 &&
                GroundOne.WE2.FoodMaterial_48 >= 1 && GroundOne.WE2.FoodMixtureDay_44 <= 0)
            {
                GroundOne.WE2.FoodMixtureDay_44 = WE.GameDay;
            }
            #endregion
            #endregion
        }
        protected override void SetupAvailableList(int level)
        {
            for (int ii = 0; ii < MAX_EQUIPLIST; ii++)
            {
                equipList[ii].Text = "";
                costList[ii].Text = "";
            }

            ItemBackPack item = null;
            switch (level)
            {
                case 1:
                    int ii = 0;
                    item = new ItemBackPack(Database.COMMON_BRONZE_SWORD);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_FIT_ARMOR);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_LIGHT_SHIELD);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_FINE_SWORD_1);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;
                    
                    item = new ItemBackPack(Database.COMMON_BASTARD_SWORD_1);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_FINE_ARMOR_1);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_FINE_SHIELD_1);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_LIGHT_CLAW_1);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_KASHI_ROD_1);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_LETHER_CLOTHING_1);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_IRON_SWORD);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_KUSARI_KATABIRA);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.RARE_FLOWER_WAND);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_SURVIVAL_CLAW);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_SUPERIOR_CROSS);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;
                    
                    item = new ItemBackPack(Database.COMMON_BLACER_OF_SYOJIN);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_ZIAI_PENDANT);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    if (GroundOne.WE2.EquipAvailable_11)
                    {
                        item = new ItemBackPack(Database.COMMON_KOUKAKU_ARMOR);
                        equipList[ii].Text = item.Name;
                        //equipList[5].Font = new Font("MS UI Gothic", 10F, FontStyle.Strikeout);
                        //costList[5].Font = new Font("MS UI Gothic", 10F, FontStyle.Strikeout);
                        UpdateRareColor(item, equipList[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.EquipAvailable_12)
                    {
                        item = new ItemBackPack(Database.COMMON_SISSO_TUKEHANE);
                        equipList[ii].Text = item.Name;
                        UpdateRareColor(item, equipList[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.EquipAvailable_13)
                    {
                        item = new ItemBackPack(Database.RARE_WAR_WOLF_BLADE);
                        equipList[ii].Text = item.Name;
                        UpdateRareColor(item, equipList[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.EquipAvailable_14)
                    {
                        item = new ItemBackPack(Database.COMMON_BLUE_COPPER_ARMOR_KAI);
                        equipList[ii].Text = item.Name;
                        UpdateRareColor(item, equipList[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.EquipAvailable_15)
                    {
                        item = new ItemBackPack(Database.COMMON_RABBIT_SHOES);
                        equipList[ii].Text = item.Name;
                        UpdateRareColor(item, equipList[ii]);
                    }
                    ii++;
                    break;

                case 2:
                    ii = 0;
                    item = new ItemBackPack(Database.COMMON_SMART_SWORD_2);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_SMART_CLAW_2);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_RAUGE_SWORD_2);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_SMART_ROD_2);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_SMART_SHIELD_2);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_SMART_PLATE_2);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_SMART_CLOTHING_2);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_SMART_ROBE_2);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_STEEL_SWORD);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_BERSERKER_PLATE);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_MIX_HINOKI_ROD);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_FACILITY_CLAW);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.COMMON_BRIGHTNESS_ROBE);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    item = new ItemBackPack(Database.RARE_WILD_HEART_SPADE);
                    equipList[ii].Text = item.Name;
                    UpdateRareColor(item, equipList[ii]);
                    ii++;

                    if (GroundOne.WE2.EquipAvailable_21)
                    {
                        item = new ItemBackPack(Database.COMMON_WHITE_WAVE_RING);
                        equipList[ii].Text = item.Name;
                        UpdateRareColor(item, equipList[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.EquipAvailable_22)
                    {
                        item = new ItemBackPack(Database.COMMON_NEEDLE_FEATHER);
                        equipList[ii].Text = item.Name;
                        UpdateRareColor(item, equipList[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.EquipAvailable_23)
                    {
                        item = new ItemBackPack(Database.COMMON_KOUSHITU_ORB);
                        equipList[ii].Text = item.Name;
                        UpdateRareColor(item, equipList[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.EquipAvailable_24)
                    {
                        item = new ItemBackPack(Database.RARE_RED_ARM_BLADE);
                        equipList[ii].Text = item.Name;
                        UpdateRareColor(item, equipList[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.EquipAvailable_25)
                    {
                        item = new ItemBackPack(Database.RARE_STRONG_SERPENT_CLAW);
                        equipList[ii].Text = item.Name;
                        UpdateRareColor(item, equipList[ii]);
                    }
                    ii++;

                    if (GroundOne.WE2.EquipAvailable_26)
                    {
                        item = new ItemBackPack(Database.RARE_STRONG_SERPENT_SHIELD);
                        equipList[ii].Text = item.Name;
                        UpdateRareColor(item, equipList[ii]);
                    }
                    ii++;

                    break;

                case 3:
                    item = new ItemBackPack(Database.COMMON_WINTERS_HORN);
                    equipList[0].Text = item.Name;
                    UpdateRareColor(item, equipList[0]);

                    item = new ItemBackPack(Database.RARE_CHILL_BONE_SHIELD);
                    equipList[1].Text = item.Name;
                    UpdateRareColor(item, equipList[1]);

                    if (GroundOne.WE2.EquipAvailable_31)
                    {
                        item = new ItemBackPack(Database.COMMON_SNOW_GUARD);
                        equipList[2].Text = item.Name;
                        UpdateRareColor(item, equipList[2]);
                    }

                    if (GroundOne.WE2.EquipAvailable_32)
                    {
                        item = new ItemBackPack(Database.COMMON_LIZARDSCALE_ARMOR);
                        equipList[3].Text = item.Name;
                        UpdateRareColor(item, equipList[3]);
                    }

                    if (GroundOne.WE2.EquipAvailable_33)
                    {
                        item = new ItemBackPack(Database.COMMON_STEEL_BLADE);
                        equipList[4].Text = item.Name;
                        UpdateRareColor(item, equipList[4]);
                    }

                    if (GroundOne.WE2.EquipAvailable_34)
                    {
                        item = new ItemBackPack(Database.COMMON_PENGUIN_OF_PENGUIN);
                        equipList[5].Text = item.Name;
                        UpdateRareColor(item, equipList[5]);
                    }

                    if (GroundOne.WE2.EquipAvailable_35)
                    {
                        item = new ItemBackPack(Database.COMMON_ARGNIAN_TUNIC);
                        equipList[6].Text = item.Name;
                        UpdateRareColor(item, equipList[6]);
                    }

                    if (GroundOne.WE2.EquipAvailable_36)
                    {
                        item = new ItemBackPack(Database.RARE_SPLASH_BARE_CLAW);
                        equipList[7].Text = item.Name;
                        UpdateRareColor(item, equipList[7]);
                    }

                    if (GroundOne.WE2.EquipAvailable_37)
                    {
                        item = new ItemBackPack(Database.COMMON_WOLF_BATTLE_CLOTH);
                        equipList[8].Text = item.Name;
                        UpdateRareColor(item, equipList[8]);
                    }

                    if (GroundOne.WE2.EquipAvailable_38)
                    {
                        item = new ItemBackPack(Database.EPIC_GATO_HAWL_OF_GREAT);
                        equipList[9].Text = item.Name;
                        UpdateRareColor(item, equipList[9]);
                    }
                    break;

                case 4:
                    item = new ItemBackPack(Database.RARE_SUPERIOR_CHOSEN_ROD);
                    equipList[0].Text = item.Name;
                    UpdateRareColor(item, equipList[0]);

                    item = new ItemBackPack(Database.RARE_TYOU_KOU_SWORD);
                    equipList[1].Text = item.Name;
                    UpdateRareColor(item, equipList[1]);

                    item = new ItemBackPack(Database.RARE_TYOU_KOU_ARMOR);
                    equipList[2].Text = item.Name;
                    UpdateRareColor(item, equipList[2]);

                    item = new ItemBackPack(Database.RARE_TYOU_KOU_SHIELD);
                    equipList[3].Text = item.Name;
                    UpdateRareColor(item, equipList[3]);

                    item = new ItemBackPack(Database.RARE_WHITE_GOLD_CROSS);
                    equipList[4].Text = item.Name;
                    UpdateRareColor(item, equipList[4]);

                    if (GroundOne.WE2.EquipAvailable_41)
                    {
                        item = new ItemBackPack(Database.RARE_HUNTERS_EYE);
                        equipList[5].Text = item.Name;
                        UpdateRareColor(item, equipList[5]);
                    }
                    if (GroundOne.WE2.EquipAvailable_42)
                    {
                        item = new ItemBackPack(Database.RARE_ONEHUNDRED_BUTOUGI);
                        equipList[6].Text = item.Name;
                        UpdateRareColor(item, equipList[6]);
                    }
                    if (GroundOne.WE2.EquipAvailable_43)
                    {
                        item = new ItemBackPack(Database.RARE_DARKANGEL_CROSS);
                        equipList[7].Text = item.Name;
                        UpdateRareColor(item, equipList[7]);
                    }
                    if (GroundOne.WE2.EquipAvailable_44)
                    {
                        item = new ItemBackPack(Database.RARE_DEVIL_KILLER);
                        equipList[8].Text = item.Name;
                        UpdateRareColor(item, equipList[8]);
                    }
                    if (GroundOne.WE2.EquipAvailable_45)
                    {
                        item = new ItemBackPack(Database.RARE_TRUERED_MASTER_BLADE);
                        equipList[9].Text = item.Name;
                        UpdateRareColor(item, equipList[9]);
                    }
                    if (GroundOne.WE2.EquipAvailable_46)
                    {
                        item = new ItemBackPack(Database.RARE_VOID_HYMNSONIA);
                        equipList[10].Text = item.Name;
                        UpdateRareColor(item, equipList[10]);
                    }
                    if (GroundOne.WE2.EquipAvailable_47)
                    {
                        item = new ItemBackPack(Database.RARE_SEAL_OF_BALANCE);
                        equipList[11].Text = item.Name;
                        UpdateRareColor(item, equipList[11]);
                    }
                    if (GroundOne.WE2.EquipAvailable_48)
                    {
                        item = new ItemBackPack(Database.RARE_DOOMBRINGER);
                        equipList[12].Text = item.Name;
                        UpdateRareColor(item, equipList[12]);
                    }
                    if (GroundOne.WE2.EquipAvailable_49)
                    {
                        item = new ItemBackPack(Database.EPIC_MEIKOU_DOOMBRINGER);
                        equipList[13].Text = item.Name;
                        UpdateRareColor(item, equipList[13]);
                    }
                    
                    break;

                case 5:
                    item = new ItemBackPack(Database.COMMON_GORGEOUS_RED_POTION);
                    equipList[0].Text = item.Name;
                    UpdateRareColor(item, equipList[0]);

                    item = new ItemBackPack(Database.COMMON_GORGEOUS_BLUE_POTION);
                    equipList[1].Text = item.Name;
                    UpdateRareColor(item, equipList[1]);

                    item = new ItemBackPack(Database.COMMON_GORGEOUS_GREEN_POTION);
                    equipList[2].Text = item.Name;
                    UpdateRareColor(item, equipList[2]);

                    item = new ItemBackPack(Database.COMMON_PLATINUM_RING_1);
                    equipList[3].Text = item.Name;
                    UpdateRareColor(item, equipList[3]);

                    item = new ItemBackPack(Database.COMMON_PLATINUM_RING_2);
                    equipList[4].Text = item.Name;
                    UpdateRareColor(item, equipList[4]);

                    item = new ItemBackPack(Database.COMMON_PLATINUM_RING_3);
                    equipList[5].Text = item.Name;
                    UpdateRareColor(item, equipList[5]);

                    item = new ItemBackPack(Database.COMMON_PLATINUM_RING_4);
                    equipList[6].Text = item.Name;
                    UpdateRareColor(item, equipList[6]);

                    item = new ItemBackPack(Database.COMMON_PLATINUM_RING_5);
                    equipList[7].Text = item.Name;
                    UpdateRareColor(item, equipList[7]);

                    item = new ItemBackPack(Database.COMMON_PLATINUM_RING_6);
                    equipList[8].Text = item.Name;
                    UpdateRareColor(item, equipList[8]);

                    item = new ItemBackPack(Database.COMMON_PLATINUM_RING_7);
                    equipList[9].Text = item.Name;
                    UpdateRareColor(item, equipList[9]);

                    item = new ItemBackPack(Database.COMMON_PLATINUM_RING_8);
                    equipList[10].Text = item.Name;
                    UpdateRareColor(item, equipList[10]);

                    item = new ItemBackPack(Database.COMMON_PLATINUM_RING_9);
                    equipList[11].Text = item.Name;
                    UpdateRareColor(item, equipList[11]);

                    item = new ItemBackPack(Database.COMMON_PLATINUM_RING_10);
                    equipList[12].Text = item.Name;
                    UpdateRareColor(item, equipList[12]);

                    item = new ItemBackPack(Database.RARE_ETHREAL_EDGE_SABRE);
                    equipList[13].Text = item.Name;
                    UpdateRareColor(item, equipList[13]);

                    item = new ItemBackPack(Database.RARE_BLOODY_DIRTY_SCYTHE);
                    equipList[14].Text = item.Name;
                    UpdateRareColor(item, equipList[14]);

                    item = new ItemBackPack(Database.EPIC_MEIKOU_DOOMBRINGER);
                    equipList[15].Text = item.Name;
                    UpdateRareColor(item, equipList[15]);

                    item = new ItemBackPack(Database.RARE_WHITE_DIAMOND_SHIELD);
                    equipList[16].Text = item.Name;
                    UpdateRareColor(item, equipList[16]);

                    break;
            }

            for (int ii = 0; ii < MAX_EQUIPLIST; ii++)
            {
                if (equipList[ii].Text != "")
                {
                    ItemBackPack temp4 = new ItemBackPack(equipList[ii].Text);
                    costList[ii].Text = temp4.Cost.ToString();
                }
                else
                {
                    costList[ii].Text = "";
                }
            }
        }


        protected virtual void OnEquipmentShop_Shown()
        {
            #region "１階"
            if (!GroundOne.WE2.EquipAvailable_11 && (GroundOne.WE2.EquipMixtureDay_11 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_11))
            {
                GroundOne.WE2.EquipAvailable_11 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.COMMON_KOUKAKU_ARMOR;
                    TID.ItemNameTitle = Database.COMMON_KOUKAKU_ARMOR;
                    TID.Description = "甲殻部を繋ぎ合わせた鎧に、魔法耐性を若干付与させた一品。防御力１１～１５。火耐性＋３０";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.EquipAvailable_12 && (GroundOne.WE2.EquipMixtureDay_12 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_12))
            {
                GroundOne.WE2.EquipAvailable_12 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.COMMON_SISSO_TUKEHANE;
                    TID.ItemNameTitle = Database.COMMON_SISSO_TUKEHANE;
                    TID.Description = "毛皮に幾つかの白羽を埋め込んだアクセサリ。力＋３、技＋３、心＋３"; ;
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.EquipAvailable_13 && (GroundOne.WE2.EquipMixtureDay_13 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_13))
            {
                GroundOne.WE2.EquipAvailable_13 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.RARE_WAR_WOLF_BLADE;
                    TID.ItemNameTitle = Database.RARE_WAR_WOLF_BLADE;
                    TID.Description = "狼の牙を基素材とし、刺付き触手を加工した武器。攻撃力３２～４４"; ;
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.EquipAvailable_14 && (GroundOne.WE2.EquipMixtureDay_14 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_14))
            {
                GroundOne.WE2.EquipAvailable_14 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.COMMON_BLUE_COPPER_ARMOR_KAI;
                    TID.ItemNameTitle = Database.COMMON_BLUE_COPPER_ARMOR_KAI;
                    TID.Description = "青銅の材質強度を落とさずに仕上げられた鎧。防御力１８～２５。";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.EquipAvailable_15 && (GroundOne.WE2.EquipMixtureDay_15 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_15))
            {
                GroundOne.WE2.EquipAvailable_15 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.COMMON_RABBIT_SHOES;
                    TID.ItemNameTitle = Database.COMMON_RABBIT_SHOES;
                    TID.Description = "ウサギの毛皮と質の良いスパイダーシルクを合成した出来たシューズ。技＋１２、体力＋１０";
                    TID.ShowDialog();
                }
            }
            #endregion
            #region "２階"
            if (!GroundOne.WE2.EquipAvailable_21 && (GroundOne.WE2.EquipMixtureDay_21 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_21))
            {
                GroundOne.WE2.EquipAvailable_21 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.COMMON_WHITE_WAVE_RING;
                    TID.ItemNameTitle = Database.COMMON_WHITE_WAVE_RING;
                    TID.Description = "勾玉からエッセンスを引き出し、リング化に成功。知＋８、体力＋７、心＋１０";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.EquipAvailable_22 && (GroundOne.WE2.EquipMixtureDay_22 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_22))
            {
                GroundOne.WE2.EquipAvailable_22 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.COMMON_NEEDLE_FEATHER;
                    TID.ItemNameTitle = Database.COMMON_NEEDLE_FEATHER;
                    TID.Description = "鋭いトゲと幸運を呼ぶ青羽をうまく融合させたアクセサリ。力＋１０、技＋８、心＋９";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.EquipAvailable_23 && (GroundOne.WE2.EquipMixtureDay_23 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_23))
            {
                GroundOne.WE2.EquipAvailable_23 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.COMMON_KOUSHITU_ORB;
                    TID.ItemNameTitle = Database.COMMON_KOUSHITU_ORB;
                    TID.Description = "幾つもの殻を溶解し、一つの胸当ての形状にした。防御力８～２５";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.EquipAvailable_24 && (GroundOne.WE2.EquipMixtureDay_24 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_24))
            {
                GroundOne.WE2.EquipAvailable_24 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.RARE_RED_ARM_BLADE;
                    TID.ItemNameTitle = Database.RARE_RED_ARM_BLADE;
                    TID.Description = "豪腕なジョーの腕を加工し、赤褐色でコーティングを施してある。攻撃力２７～９５";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.EquipAvailable_25 && (GroundOne.WE2.EquipMixtureDay_25 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_25))
            {
                GroundOne.WE2.EquipAvailable_25 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.RARE_STRONG_SERPENT_CLAW;
                    TID.ItemNameTitle = Database.RARE_STRONG_SERPENT_CLAW;
                    TID.Description = "強固な青鮫の剣歯を更に高質化させ、高熱で磨いだ爪。攻撃力１５～４６";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.EquipAvailable_26 && (GroundOne.WE2.EquipMixtureDay_26 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_26))
            {
                GroundOne.WE2.EquipAvailable_26 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.RARE_STRONG_SERPENT_SHIELD;
                    TID.ItemNameTitle = Database.RARE_STRONG_SERPENT_SHIELD;
                    TID.Description = "強固な青鮫の鱗を更に高質化させ、低温度化で固めた防具。防御力６～３５";
                    TID.ShowDialog();
                }
            }
            #endregion
            #region "３階"
            if (!GroundOne.WE2.EquipAvailable_31 && (GroundOne.WE2.EquipMixtureDay_31 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_31))
            {
                GroundOne.WE2.EquipAvailable_31 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.COMMON_SNOW_GUARD;
                    TID.ItemNameTitle = Database.COMMON_SNOW_GUARD;
                    TID.Description = "吹雪対策用に見えるが、アクセサリとしての上質さは装着した者のみが知る。体＋５０、心＋５０、水耐性１０００";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.EquipAvailable_32 && (GroundOne.WE2.EquipMixtureDay_32 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_32))
            {
                GroundOne.WE2.EquipAvailable_32 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.COMMON_LIZARDSCALE_ARMOR;
                    TID.ItemNameTitle = Database.COMMON_LIZARDSCALE_ARMOR;
                    TID.Description = "リザードの鱗を細かく細分化し、鎧形状に仕立てなおしたもの。防御力１０５～１３０";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.EquipAvailable_33 && (GroundOne.WE2.EquipMixtureDay_33 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_33))
            {
                GroundOne.WE2.EquipAvailable_33 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.COMMON_STEEL_BLADE;
                    TID.ItemNameTitle = Database.COMMON_STEEL_BLADE;
                    TID.Description = "強靭な素材のみ使用した鋼にガンツ直々の技が宿った剣！攻撃力２２５(+25）～２５５(+25)";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.EquipAvailable_34 && (GroundOne.WE2.EquipMixtureDay_34 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_34))
            {
                GroundOne.WE2.EquipAvailable_34 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.COMMON_PENGUIN_OF_PENGUIN;
                    TID.ItemNameTitle = Database.COMMON_PENGUIN_OF_PENGUIN;
                    TID.Description = "ペンギンの気持ちが心なしか伝わってくる。力＋３０、技＋３０、知＋３０、体＋３０、心＋３０";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.EquipAvailable_35 && (GroundOne.WE2.EquipMixtureDay_35 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_35))
            {
                GroundOne.WE2.EquipAvailable_35 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.COMMON_ARGNIAN_TUNIC;
                    TID.ItemNameTitle = Database.COMMON_ARGNIAN_TUNIC;
                    TID.Description = "アルゴニアンの素材は紫色のコーティングがあり安定した防御性が出やすい。防御力５５～６７";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.EquipAvailable_36 && (GroundOne.WE2.EquipMixtureDay_36 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_36))
            {
                GroundOne.WE2.EquipAvailable_36 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.RARE_SPLASH_BARE_CLAW;
                    TID.ItemNameTitle = Database.RARE_SPLASH_BARE_CLAW;
                    TID.Description = "ゴツゴツし砕け散ったクマの手素材をガンツが見事に武器化に成功！　攻撃力２６２～２７７";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.EquipAvailable_37 && (GroundOne.WE2.EquipMixtureDay_37 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_37))
            {
                GroundOne.WE2.EquipAvailable_37 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.COMMON_WOLF_BATTLE_CLOTH;
                    TID.ItemNameTitle = Database.COMMON_WOLF_BATTLE_CLOTH;
                    TID.Description = "野生ウルフのごわごわした質感を落とすことなく衣に仕立ててある。防御力７２～７９";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.EquipAvailable_38 && (GroundOne.WE2.EquipMixtureDay_38 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_38))
            {
                GroundOne.WE2.EquipAvailable_38 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.EPIC_GATO_HAWL_OF_GREAT;
                    TID.ItemNameTitle = Database.EPIC_GATO_HAWL_OF_GREAT;
                    TID.Description = "古代賢者ガトゥに仕えていた神鹿の紋章。沈黙・スタン・麻痺耐性。技＋８５、知＋３２５、魔力６６６～７７７、魔攻率＋２０％、潜力率＋２０％、闇耐性1500、火耐性1500";
                    TID.ShowDialog();
                }
            }
            #endregion
            #region "４階"
            if (!GroundOne.WE2.EquipAvailable_41 && (GroundOne.WE2.EquipMixtureDay_41 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_41))
            {
                GroundOne.WE2.EquipAvailable_41 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.RARE_HUNTERS_EYE;
                    TID.ItemNameTitle = Database.RARE_HUNTERS_EYE;
                    TID.Description = "ハンター七つ道具を組み合わせて作成された擬眼。眼の動向の開き方に応じて様々なギミックが発動する。技＋３００、体＋３００、沈黙耐性、麻痺耐性、鈍化耐性、暗闇耐性";
                    TID.Description += "\r\n【特殊能力】　以下のいずれかがランダムに発動する。敵全体に対して【鈍化】効果を与える / 味方全体のいずれかがトゥルス・ヴィジョンがかかっていない場合、トゥルス・ヴィジョンを発動する / 自分自身の物理攻撃力と戦闘速度をUPする / 敵単体の物理攻撃力と戦闘速度をDOWNさせる";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.EquipAvailable_42 && (GroundOne.WE2.EquipMixtureDay_42 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_42))
            {
                GroundOne.WE2.EquipAvailable_42 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.RARE_ONEHUNDRED_BUTOUGI;
                    TID.ItemNameTitle = Database.RARE_ONEHUNDRED_BUTOUGI;
                    TID.Description = "選りすぐりの獣皮を集約し、動きやすさ・重量感を重視したもの。防御力１６４～１７８、聖耐性15000、闇耐性15000、火耐性15000、水耐性15000";
                    TID.Description += "\r\n【常備能力】　まれに物理/魔法による攻撃を回避する。";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.EquipAvailable_43 && (GroundOne.WE2.EquipMixtureDay_43 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_43))
            {
                GroundOne.WE2.EquipAvailable_43 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.RARE_DARKANGEL_CROSS;
                    TID.ItemNameTitle = Database.RARE_DARKANGEL_CROSS;
                    TID.Description = "執行人のローブから高級なシルク素材を摘出し、天使のシルクと融合させて新たに創生した衣。防御力１９０～２３４、魔法防御４９２～６５２、聖耐性22000、闇耐性22000、毒耐性、誘惑耐性、鈍化耐性、暗闇耐性";
                    TID.Description += "\r\n【常備能力】　聖魔法１０％強化、闇魔法１０％強化";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.EquipAvailable_44 && (GroundOne.WE2.EquipMixtureDay_44 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_44))
            {
                GroundOne.WE2.EquipAvailable_44 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.RARE_DEVIL_KILLER;
                    TID.ItemNameTitle = Database.RARE_DEVIL_KILLER;
                    TID.Description = "悪しき者を断つ剣。ガラクタから生成したとは思えないガンツ渾身の力作。攻撃力３６０～１８８５";
                    TID.Description += "\r\n【常備能力】　稀に即死させる。";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.EquipAvailable_45 && (GroundOne.WE2.EquipMixtureDay_45 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_45))
            {
                GroundOne.WE2.EquipAvailable_45 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.RARE_TRUERED_MASTER_BLADE;
                    TID.ItemNameTitle = Database.RARE_TRUERED_MASTER_BLADE;
                    TID.Description = "頭蓋骨を破砕した素材を柄に付け、剣の切っ先は常に火が宿る。攻撃力８００～８５０、魔力６５０～７００";
                    TID.Description += "\r\n【常備能力】　物理攻撃がヒットする度に、稀にワード・オブ・パワーが追加効果で発動する。魔法攻撃がヒットする度に、稀にサイキック・ウェイブが追加効果で発動する。";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.EquipAvailable_46 && (GroundOne.WE2.EquipMixtureDay_46 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_46))
            {
                GroundOne.WE2.EquipAvailable_46 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.RARE_VOID_HYMNSONIA;
                    TID.ItemNameTitle = Database.RARE_VOID_HYMNSONIA;
                    TID.Description = "豪華な財宝を与える事で怨霊を全て除去した箱。力＋５００、技＋５００、知＋５００、心－１０００、毒耐性、沈黙耐性、スタン耐性、麻痺耐性、凍結耐性、誘惑耐性、鈍化耐性、暗闇耐性、スリップ耐性";
                    TID.Description += "\r\n【特殊能力】　本装備品：ヴォイド・ヒムソニアの【心】パラメタ-1000の特性を無効にする。戦闘終了までこの効果は継続する。";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.EquipAvailable_47 && (GroundOne.WE2.EquipMixtureDay_47 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_47))
            {
                GroundOne.WE2.EquipAvailable_47 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.RARE_VOID_HYMNSONIA;
                    TID.ItemNameTitle = Database.RARE_VOID_HYMNSONIA;
                    TID.Description = "天秤の形状を再構築し、紋章の形状に変換することに成功。体＋５００、心＋５００、水耐性5000、空耐性5000";
                    TID.Description += "\r\n【常備能力】　物理攻撃を受けた場合、マナが回復する。魔法攻撃を受けた場合、スキルポイントが回復する。DEBUFF属性が付与された場合、次のターンそのBUFFを解除する。";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.EquipAvailable_48 && (GroundOne.WE2.EquipMixtureDay_48 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_48))
            {
                GroundOne.WE2.EquipAvailable_48 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.RARE_DOOMBRINGER;
                    TID.ItemNameTitle = Database.RARE_DOOMBRINGER;
                    TID.Description = "破滅した者へ永遠の安らぎをもたらすために作られた剣。攻撃力４７３～１４６９";
                    TID.Description += "\r\n【常備能力】　理魔法＋１０％強化。戦闘開始時、ゲイル・ウィンドが自分自身にかかる。";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.EquipAvailable_49 && (GroundOne.WE2.EquipMixtureDay_49 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_49))
            {
                GroundOne.WE2.EquipAvailable_49 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.EPIC_MEIKOU_DOOMBRINGER;
                    TID.ItemNameTitle = Database.EPIC_MEIKOU_DOOMBRINGER;
                    TID.Description = "穢れを取り払われた闇の剣。持ち主の意図に関わらず、剣が宿主を選ぶ。攻撃力１２００～２４００、魔力１２００～２４００、物攻率＋２５％、魔攻率＋２０％、戦速率＋１５％";
                    TID.Description += "\r\n【常備能力】　理魔法＋１６％強化、聖魔法１６％強化。戦闘開始時、ゲイル・ウィンドが自分自身にかかる。戦闘開始時、ジェネシスの行動記憶に【ゲイル・ウィンド】がセットされる。";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.EquipAvailable_410 && (GroundOne.WE2.EquipMixtureDay_410 != 0) && (we.GameDay > GroundOne.WE2.EquipMixtureDay_410))
            {
                GroundOne.WE2.EquipAvailable_410 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.EPIC_ETERNAL_HOMURA_RING;
                    TID.ItemNameTitle = Database.EPIC_ETERNAL_HOMURA_RING;
                    TID.Description = "その焔火が潰える事は未だかつて起きた事がない。魔法攻撃１８５０～２０５０、知＋１２５０、スタン耐性、麻痺耐性、凍結耐性";
                    TID.Description += "\r\n魔攻率＋３５％、魔防率３０％、聖耐性10000、闇耐性10000、火耐性75000、水耐性75000、理耐性10000、空耐性10000";
                    TID.Description += "\r\n【常備能力】毎ターン、MPを回復する。";
                    TID.Description += "\r\n【特殊能力】全MPを消費して、消費したMPの分だけ、無属性魔法ダメージを与える。";
                    TID.ShowDialog();
                }
            }

            #endregion
        }

        protected override void EquipmentShop_Shown(object sender, System.EventArgs e)
        {
            OnEquipmentShop_Shown();

            // ２重記述だが、ベストコードは後で良しとする。
            if (we.AvailableEquipShop && !we.AvailableEquipShop2)
            {
                SetupAvailableList(1);
            }
            else if (we.AvailableEquipShop && we.AvailableEquipShop2 && !we.AvailableEquipShop3)
            {
                SetupAvailableList(2);
            }
            else if (we.AvailableEquipShop && we.AvailableEquipShop2 && we.AvailableEquipShop3 && !we.AvailableEquipShop4)
            {
                SetupAvailableList(3);
            }
            else if (we.AvailableEquipShop && we.AvailableEquipShop2 && we.AvailableEquipShop3 && we.AvailableEquipShop4 && !we.AvailableEquipShop5)
            {
                SetupAvailableList(4);
            }
            else if (we.AvailableEquipShop && we.AvailableEquipShop2 && we.AvailableEquipShop3 && we.AvailableEquipShop4 && we.AvailableEquipShop5)
            {
                SetupAvailableList(5);
            }
            else
            {

            }
        }

        protected override void MessageExchange6(ItemBackPack backpackData, int stack, int ii)
        {
            SetupMessageText(3007, backpackData.Name, ((backpackData.Cost / 2) * stack).ToString());
        }

        protected override void ConstructPopupInfo(PopUpMini popupInfo)
        {
            popupInfo.CurrentInfo = currentPlayer.FullName + "\r\n";

            if (currentPlayer.MainWeapon == null)
            {
                popupInfo.CurrentInfo += "武器(メイン) " + "（なし）" + "\r\n";
            }
            else
            {
                popupInfo.CurrentInfo += "武器(メイン) " + currentPlayer.MainWeapon.Name + "\r\n";
            }
            if (currentPlayer.SubWeapon == null)
            {
                popupInfo.CurrentInfo += "武器(サブ) 　" + "（なし）" + "\r\n";
            }
            else
            {
                popupInfo.CurrentInfo += "武器(サブ)　 " + currentPlayer.SubWeapon.Name + "\r\n";
            }
            if (currentPlayer.MainArmor == null)
            {
                popupInfo.CurrentInfo += "防具　　　　 " + "（なし）" + "\r\n";
            }
            else
            {
                popupInfo.CurrentInfo += "防具　　　　 " + currentPlayer.MainArmor.Name + "\r\n";
            }
            if (currentPlayer.Accessory == null)
            {
                popupInfo.CurrentInfo += "装飾品１　　 " + "（なし）" + "\r\n";
            }
            else
            {
                popupInfo.CurrentInfo += "装飾品１　　 " + currentPlayer.Accessory.Name + "\r\n";
            }
            if (currentPlayer.Accessory2 == null)
            {
                popupInfo.CurrentInfo += "装飾品２　　 " + "（なし）" + "\r\n";
            }
            else
            {
                popupInfo.CurrentInfo += "装飾品２ 　　" + currentPlayer.Accessory2.Name + "\r\n";
            }
            popupInfo.CurrentInfo += "\r\n";
        }

    }
}
