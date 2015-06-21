using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class TruthPotionShop : TruthEquipmentShop
    {
        public TruthPotionShop()
        {
            base.BackColor = System.Drawing.Color.MistyRose;
            base.Name = "PotionShop";
            base.Text = "PotionShop";
            base.label1.Text = "ラナのランラン薬品店♪";
            ganz.FullName = "ラナ・アミリア";
            ganz.Name = "ラナ";
        }

        protected override void OnLoadSetupFloorButton()
        {
            if (we.AvailablePotionshop && !we.AvailablePotion2)
            {
                SetupAvailableList(1);

                btnLevel1.Visible = false; // [コメント]：最初は増える傾向を見せない演出のため、VisibleはFalse
                btnLevel2.Visible = false;
                btnLevel3.Visible = false;
                btnLevel4.Visible = false;
                btnLevel5.Visible = false;
            }
            else if (we.AvailablePotionshop && we.AvailablePotion2 && !we.AvailablePotion3)
            {
                SetupAvailableList(2);
                btnLevel1.Visible = true;
                btnLevel2.Visible = true;
                btnLevel3.Visible = false;
                btnLevel4.Visible = false;
                btnLevel5.Visible = false;
            }
            else if (we.AvailablePotionshop && we.AvailablePotion2 && we.AvailablePotion3 && !we.AvailablePotion4)
            {
                SetupAvailableList(3);
                btnLevel1.Visible = true;
                btnLevel2.Visible = true;
                btnLevel3.Visible = true;
                btnLevel4.Visible = false;
                btnLevel5.Visible = false;
            }
            else if (we.AvailablePotionshop && we.AvailablePotion2 && we.AvailablePotion3 && we.AvailablePotion4 && !we.AvailablePotion5)
            {
                SetupAvailableList(4);
                btnLevel1.Visible = true;
                btnLevel2.Visible = true;
                btnLevel3.Visible = true;
                btnLevel4.Visible = true;
                btnLevel5.Visible = false;
            }
            else if (we.AvailablePotionshop && we.AvailablePotion2 && we.AvailablePotion3 && we.AvailablePotion4 && we.AvailablePotion5)
            {
                SetupAvailableList(5);
                btnLevel1.Visible = true;
                btnLevel2.Visible = true;
                btnLevel3.Visible = true;
                btnLevel4.Visible = true;
                btnLevel5.Visible = true;
            }
        }
        protected override void OnLoadMessage()
        {
            mainMessage.Text = ganz.GetCharacterSentence(3013);
        }

        protected override void OnButton1_ClickMessage()
        {
            mainMessage.Text = ganz.GetCharacterSentence(3014);
        }

        protected override void VendorBuyMessage(ItemBackPack backpackData)
        {
            mainMessage.Text = String.Format(ganz.GetCharacterSentence(3015), backpackData.Name, backpackData.Cost.ToString());
        }

        protected override void MessageExchange1(ItemBackPack backpackData, MainCharacter player)
        {
            SetupMessageText(3016, Convert.ToString((backpackData.Cost - player.Gold)));

        }

        protected override void MessageExchange2()
        {
            SetupMessageText(3018);
        }

        protected override void MessageExchange3()
        {
            SetupMessageText(3017);
        }

        protected override void MessageExchange4()
        {
            SetupMessageText(3019);
        }

        protected override void MessageExchange5()
        {
            SetupMessageText(3020);
        }

        protected override void MessageExchange6(ItemBackPack backpackData, int stack, int ii)
        {
            SetupMessageText(3021, backpackData.Name, ((backpackData.Cost / 2) * stack).ToString());
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
                    item = new ItemBackPack(Database.POOR_SMALL_RED_POTION);
                    equipList[0].Text = item.Name;
                    UpdateRareColor(item, equipList[0]);

                    item = new ItemBackPack(Database.POOR_SMALL_BLUE_POTION);
                    equipList[1].Text = item.Name;
                    UpdateRareColor(item, equipList[1]);

                    item = new ItemBackPack(Database.POOR_SMALL_GREEN_POTION);
                    equipList[2].Text = item.Name;
                    UpdateRareColor(item, equipList[2]);

                    item = new ItemBackPack(Database.POOR_POTION_CURE_POISON);
                    equipList[3].Text = item.Name;
                    UpdateRareColor(item, equipList[3]);

                    if (GroundOne.WE2.PotionAvailable_11)
                    {
                        item = new ItemBackPack(Database.COMMON_POTION_NATURALIZE);
                        equipList[4].Text = item.Name;
                        UpdateRareColor(item, equipList[4]);
                    }
                    if (GroundOne.WE2.PotionAvailable_12)
                    {
                        item = new ItemBackPack(Database.COMMON_POTION_MAGIC_SEAL);
                        equipList[5].Text = item.Name;
                        UpdateRareColor(item, equipList[5]);
                    }
                    if (GroundOne.WE2.PotionAvailable_13)
                    {
                        item = new ItemBackPack(Database.COMMON_POTION_ATTACK_SEAL);
                        equipList[6].Text = item.Name;
                        UpdateRareColor(item, equipList[6]);
                    }
                    if (GroundOne.WE2.PotionAvailable_14)
                    {
                        item = new ItemBackPack(Database.COMMON_POTION_CURE_BLIND);
                        equipList[7].Text = item.Name;
                        UpdateRareColor(item, equipList[7]);
                    }
                    if (GroundOne.WE2.PotionAvailable_15)
                    {
                        item = new ItemBackPack(Database.RARE_POTION_MOSSGREEN_DREAM);
                        equipList[8].Text = item.Name;
                        UpdateRareColor(item, equipList[8]);
                    }
                    break;
                case 2:
                    item = new ItemBackPack(Database.COMMON_NORMAL_RED_POTION);
                    equipList[0].Text = item.Name;
                    UpdateRareColor(item, equipList[0]);

                    item = new ItemBackPack(Database.COMMON_NORMAL_BLUE_POTION);
                    equipList[1].Text = item.Name;
                    UpdateRareColor(item, equipList[1]);

                    item = new ItemBackPack(Database.COMMON_NORMAL_GREEN_POTION);
                    equipList[2].Text = item.Name;
                    UpdateRareColor(item, equipList[2]);

                    item = new ItemBackPack(Database.COMMON_RESIST_POISON);
                    equipList[3].Text = item.Name;
                    UpdateRareColor(item, equipList[3]);

                    if (GroundOne.WE2.PotionAvailable_21)
                    {
                        item = new ItemBackPack(Database.COMMON_POTION_OVER_GROWTH);
                        equipList[4].Text = item.Name;
                        UpdateRareColor(item, equipList[4]);
                    }
                    if (GroundOne.WE2.PotionAvailable_22)
                    {
                        item = new ItemBackPack(Database.COMMON_POTION_RAINBOW_IMPACT);
                        equipList[5].Text = item.Name;
                        UpdateRareColor(item, equipList[5]);
                    }
                    if (GroundOne.WE2.PotionAvailable_23)
                    {
                        item = new ItemBackPack(Database.COMMON_POTION_BLACK_GAST);
                        equipList[6].Text = item.Name;
                        UpdateRareColor(item, equipList[6]);
                    }
                    break;
                case 3:
                    item = new ItemBackPack(Database.COMMON_LARGE_RED_POTION);
                    equipList[0].Text = item.Name;
                    UpdateRareColor(item, equipList[0]);

                    item = new ItemBackPack(Database.COMMON_LARGE_BLUE_POTION);
                    equipList[1].Text = item.Name;
                    UpdateRareColor(item, equipList[1]);

                    item = new ItemBackPack(Database.COMMON_LARGE_GREEN_POTION);
                    equipList[2].Text = item.Name;
                    UpdateRareColor(item, equipList[2]);

                    if (GroundOne.WE2.PotionAvailable_31)
                    {
                        item = new ItemBackPack(Database.COMMON_FAIRY_BREATH);
                        equipList[3].Text = item.Name;
                        UpdateRareColor(item, equipList[3]);
                    }

                    if (GroundOne.WE2.PotionAvailable_32)
                    {
                        item = new ItemBackPack(Database.COMMON_HEART_ACCELERATION);
                        equipList[4].Text = item.Name;
                        UpdateRareColor(item, equipList[4]);
                    }

                    if (GroundOne.WE2.PotionAvailable_33)
                    {
                        item = new ItemBackPack(Database.RARE_SAGE_POTION_MINI);
                        equipList[5].Text = item.Name;
                        UpdateRareColor(item, equipList[5]);
                    }
                    break;

                case 4:
                    item = new ItemBackPack(Database.COMMON_HUGE_RED_POTION);
                    equipList[0].Text = item.Name;
                    UpdateRareColor(item, equipList[0]);

                    item = new ItemBackPack(Database.COMMON_HUGE_BLUE_POTION);
                    equipList[1].Text = item.Name;
                    UpdateRareColor(item, equipList[1]);

                    item = new ItemBackPack(Database.COMMON_HUGE_GREEN_POTION);
                    equipList[2].Text = item.Name;
                    UpdateRareColor(item, equipList[2]);

                    if (GroundOne.WE2.PotionAvailable_41)
                    {
                        item = new ItemBackPack(Database.RARE_POWER_SURGE);
                        equipList[3].Text = item.Name;
                        UpdateRareColor(item, equipList[3]);
                    }

                    if (GroundOne.WE2.PotionAvailable_42)
                    {
                        item = new ItemBackPack(Database.RARE_ELEMENTAL_SEAL);
                        equipList[4].Text = item.Name;
                        UpdateRareColor(item, equipList[4]);
                    }

                    if (GroundOne.WE2.PotionAvailable_43)
                    {
                        item = new ItemBackPack(Database.RARE_GENSEI_MAGIC_BOTTLE);
                        equipList[5].Text = item.Name;
                        UpdateRareColor(item, equipList[5]);
                    }

                    if (GroundOne.WE2.PotionAvailable_44)
                    {
                        item = new ItemBackPack(Database.RARE_GENSEI_TAIMA_KUSURI);
                        equipList[6].Text = item.Name;
                        UpdateRareColor(item, equipList[6]);
                    }

                    if (GroundOne.WE2.PotionAvailable_45)
                    {
                        item = new ItemBackPack(Database.RARE_MIND_ILLUSION);
                        equipList[7].Text = item.Name;
                        UpdateRareColor(item, equipList[7]);
                    }

                    if (GroundOne.WE2.PotionAvailable_46)
                    {
                        item = new ItemBackPack(Database.RARE_SHINING_AETHER);
                        equipList[8].Text = item.Name;
                        UpdateRareColor(item, equipList[8]);
                    }

                    if (GroundOne.WE2.PotionAvailable_47)
                    {
                        item = new ItemBackPack(Database.RARE_ZETTAI_STAMINAUP);
                        equipList[9].Text = item.Name;
                        UpdateRareColor(item, equipList[9]);
                    }

                    if (GroundOne.WE2.PotionAvailable_48)
                    {
                        item = new ItemBackPack(Database.RARE_BLACK_ELIXIR);
                        equipList[10].Text = item.Name;
                        UpdateRareColor(item, equipList[10]);
                    }

                    if (GroundOne.WE2.PotionAvailable_49)
                    {
                        item = new ItemBackPack(Database.RARE_ZEPHER_BREATH);
                        equipList[11].Text = item.Name;
                        UpdateRareColor(item, equipList[11]);
                    }

                    if (GroundOne.WE2.PotionAvailable_410)
                    {
                        item = new ItemBackPack(Database.RARE_COLORESS_ANTIDOTE);
                        equipList[12].Text = item.Name;
                        UpdateRareColor(item, equipList[12]);
                    }
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

        protected override void EquipmentShop_Shown(object sender, EventArgs e)
        {
            #region "１階"
            if (!GroundOne.WE2.PotionAvailable_11 && (GroundOne.WE2.PotionMixtureDay_11 != 0) && (we.GameDay > GroundOne.WE2.PotionMixtureDay_11))
            {
                GroundOne.WE2.PotionAvailable_11 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.COMMON_POTION_NATURALIZE;
                    TID.ItemNameTitle = Database.COMMON_POTION_NATURALIZE;
                    TID.Description = "自然素材の緑色素を調合した浄化薬。味方一体を対象とし、【猛毒】【鈍化】の効果を解除する。戦闘時のみ使用可能。";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.PotionAvailable_12 && (GroundOne.WE2.PotionMixtureDay_12 != 0) && (we.GameDay > GroundOne.WE2.PotionMixtureDay_12))
            {
                GroundOne.WE2.PotionAvailable_12 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.COMMON_POTION_MAGIC_SEAL;
                    TID.ItemNameTitle = Database.COMMON_POTION_MAGIC_SEAL;
                    TID.Description = "赤い胞子内から魔法成分を摘出し、マリーの素材との統合に成功。味方一体を対象とし、魔法攻撃力を５％ＵＰする。戦闘時のみ使用可能。";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.PotionAvailable_13 && (GroundOne.WE2.PotionMixtureDay_13 != 0) && (we.GameDay > GroundOne.WE2.PotionMixtureDay_13))
            {
                GroundOne.WE2.PotionAvailable_13 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.COMMON_POTION_ATTACK_SEAL;
                    TID.ItemNameTitle = Database.COMMON_POTION_ATTACK_SEAL;
                    TID.Description = "アルラウネの花粉から筋力を一時増強させる薬。味方一体を対象とし、物理攻撃力を５％ＵＰする。戦闘時のみ使用可能。";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.PotionAvailable_14 && (GroundOne.WE2.PotionMixtureDay_14 != 0) && (we.GameDay > GroundOne.WE2.PotionMixtureDay_14))
            {
                GroundOne.WE2.PotionAvailable_14 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.COMMON_POTION_CURE_BLIND;
                    TID.ItemNameTitle = Database.COMMON_POTION_CURE_BLIND;
                    TID.Description = "魔属性のマンドラゴラに輝く燐粉を織り交ぜた薬。味方一体を対象とし、【暗闇】の効果を解除する。戦闘時のみ使用可能。";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.PotionAvailable_15 && (GroundOne.WE2.PotionMixtureDay_15 != 0) && (we.GameDay > GroundOne.WE2.PotionMixtureDay_15))
            {
                GroundOne.WE2.PotionAvailable_15 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.RARE_POTION_MOSSGREEN_DREAM;
                    TID.ItemNameTitle = Database.RARE_POTION_MOSSGREEN_DREAM;
                    TID.Description = "モスの有効なエキスを摘出し、ドリームパウダーで清浄化した薬。味方一体を対象とし、【猛毒】【鈍化】【暗闇】の効果を解除する。戦闘時のみ使用可能。";
                    TID.ShowDialog();
                }
            }
            #endregion
            #region "２階"
            if (!GroundOne.WE2.PotionAvailable_21 && (GroundOne.WE2.PotionMixtureDay_21 != 0) && (we.GameDay > GroundOne.WE2.PotionMixtureDay_21))
            {
                GroundOne.WE2.PotionAvailable_21 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.COMMON_POTION_OVER_GROWTH;
                    TID.ItemNameTitle = Database.COMMON_POTION_OVER_GROWTH;
                    TID.Description = "異常成長した卵から、成長促進エキスを摘出し、調合した薬。最大ライフ1000ＵＰ【戦闘中専用】";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.PotionAvailable_22 && (GroundOne.WE2.PotionMixtureDay_22 != 0) && (we.GameDay > GroundOne.WE2.PotionMixtureDay_22))
            {
                GroundOne.WE2.PotionAvailable_22 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.COMMON_POTION_RAINBOW_IMPACT;
                    TID.ItemNameTitle = Database.COMMON_POTION_RAINBOW_IMPACT;
                    TID.Description = "あらゆる効果が期待される七色薬品。効果【スタン】【気絶】【凍結】【沈黙】を解除【戦闘中専用】";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.PotionAvailable_23 && (GroundOne.WE2.PotionMixtureDay_23 != 0) && (we.GameDay > GroundOne.WE2.PotionMixtureDay_23))
            {
                GroundOne.WE2.PotionAvailable_23 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.COMMON_POTION_BLACK_GAST;
                    TID.ItemNameTitle = Database.COMMON_POTION_BLACK_GAST;
                    TID.Description = "黒墨から強力な身体活性と魔力活性エキスを検出し、調合に成功。魔法攻撃/物理攻撃７％ＵＰ【戦闘中専用】";
                    TID.ShowDialog();
                }
            }
            #endregion
            #region "３階"
            if (!GroundOne.WE2.PotionAvailable_31 && (GroundOne.WE2.PotionMixtureDay_31 != 0) && (we.GameDay > GroundOne.WE2.PotionMixtureDay_31))
            {
                GroundOne.WE2.PotionAvailable_31 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.COMMON_FAIRY_BREATH;
                    TID.ItemNameTitle = Database.COMMON_FAIRY_BREATH;
                    TID.Description = "フェアリーの息吹には、精神を収める成分が含まれている。【沈黙】を解除し【沈黙】耐性を付与。【戦闘中専用】";
                    TID.ShowDialog();
                }
            }

            if (!GroundOne.WE2.PotionAvailable_32 && (GroundOne.WE2.PotionMixtureDay_32 != 0) && (we.GameDay > GroundOne.WE2.PotionMixtureDay_32))
            {
                GroundOne.WE2.PotionAvailable_32 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.COMMON_HEART_ACCELERATION;
                    TID.ItemNameTitle = Database.COMMON_HEART_ACCELERATION;
                    TID.Description = "心臓の状態を最高のコンディションにし、身体の躍動を生み出す。【麻痺】を解除し【麻痺】耐性を付与。【戦闘中専用】";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.PotionAvailable_33 && (GroundOne.WE2.PotionMixtureDay_33 != 0) && (we.GameDay > GroundOne.WE2.PotionMixtureDay_33))
            {
                GroundOne.WE2.PotionAvailable_33 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.RARE_SAGE_POTION_MINI;
                    TID.ItemNameTitle = Database.RARE_SAGE_POTION_MINI;
                    TID.Description = "賢者達の研究結果の一部を拝借した秘薬。全特殊効果を解除し、全耐性を付与。対象者は死亡時、復活できなくなる。【戦闘中専用】";
                    TID.ShowDialog();
                }
            }
            #endregion
            #region "４階"
            if (!GroundOne.WE2.PotionAvailable_41 && (GroundOne.WE2.PotionMixtureDay_41 != 0) && (we.GameDay > GroundOne.WE2.PotionMixtureDay_41))
            {
                GroundOne.WE2.PotionAvailable_41 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.RARE_POWER_SURGE;
                    TID.ItemNameTitle = Database.RARE_POWER_SURGE;
                    TID.Description = "生命の源からパワーの根源を引き出す薬。力＋６００、体＋４００、物攻率＋２０％を付与する。【戦闘中専用】";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.PotionAvailable_42 && (GroundOne.WE2.PotionMixtureDay_42 != 0) && (we.GameDay > GroundOne.WE2.PotionMixtureDay_42))
            {
                GroundOne.WE2.PotionAvailable_42 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.RARE_ELEMENTAL_SEAL;
                    TID.ItemNameTitle = Database.RARE_ELEMENTAL_SEAL;
                    TID.Description = "様々なマテリアルを分析し、耐性創生色素を抽出し、一つのシールに仕立てた薬品。";
                    TID.Description += "\r\n対象者の毒、沈黙、スタン、麻痺、凍結、誘惑、鈍化、暗闇、スリップを解除する。【戦闘中専用】";
                    TID.Description += "\r\n毒耐性、沈黙耐性、スタン耐性、麻痺耐性、凍結耐性、誘惑耐性、鈍化耐性、暗闇耐性、スリップ耐性【戦闘中専用】";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.PotionAvailable_43 && (GroundOne.WE2.PotionMixtureDay_43 != 0) && (we.GameDay > GroundOne.WE2.PotionMixtureDay_43))
            {
                GroundOne.WE2.PotionAvailable_43 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.RARE_GENSEI_MAGIC_BOTTLE;
                    TID.ItemNameTitle = Database.RARE_GENSEI_MAGIC_BOTTLE;
                    TID.Description = "精神の源から知恵の源流を引き出す薬。知＋６００、心＋４００、魔攻率＋２０％を付与する。【戦闘中専用】";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.PotionAvailable_44 && (GroundOne.WE2.PotionMixtureDay_44 != 0) && (we.GameDay > GroundOne.WE2.PotionMixtureDay_44))
            {
                GroundOne.WE2.PotionAvailable_44 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.RARE_GENSEI_TAIMA_KUSURI;
                    TID.ItemNameTitle = Database.RARE_GENSEI_TAIMA_KUSURI;
                    TID.Description = "『源正酒造』と連携し、酒と薬をうまく調合した退魔の秘薬。";
                    TID.Description += "\r\n即死を伴うアクションが行われた場合、即死を回避する。この効果は一度だけ適用される。【戦闘中専用】";
                    TID.Description += "\r\nライフが０になった場合、ライフを半分にまで回復する。この効果は一度だけ適用される。【戦闘中専用】";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.PotionAvailable_45 && (GroundOne.WE2.PotionMixtureDay_45 != 0) && (we.GameDay > GroundOne.WE2.PotionMixtureDay_45))
            {
                GroundOne.WE2.PotionAvailable_45 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.RARE_MIND_ILLUSION;
                    TID.ItemNameTitle = Database.RARE_MIND_ILLUSION;
                    TID.Description = "第六感の源からイメージの増幅を引き出す薬。力＋１００、技＋１００、知＋１００、体＋１００、心＋６００、潜力率＋２０％を付与する。【戦闘中専用】";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.PotionAvailable_46 && (GroundOne.WE2.PotionMixtureDay_46 != 0) && (we.GameDay > GroundOne.WE2.PotionMixtureDay_46))
            {
                GroundOne.WE2.PotionAvailable_46 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.RARE_SHINING_AETHER;
                    TID.ItemNameTitle = Database.RARE_SHINING_AETHER;
                    TID.Description = "神々しく光輝くエーテル剤。見ているだけでも、勇気が湧いてくる。";
                    TID.Description += "\r\n【元核】スキルを一度だけ発動可能となる。この効果は本戦闘中に一度発動した後でも、適用される。【戦闘中専用】";
                    TID.Description += "\r\nまた、次のターンまで、魔法およびスキルが、カウンターされなくなる。【戦闘中専用】";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.PotionAvailable_47 && (GroundOne.WE2.PotionMixtureDay_47 != 0) && (we.GameDay > GroundOne.WE2.PotionMixtureDay_47))
            {
                GroundOne.WE2.PotionAvailable_47 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.RARE_ZETTAI_STAMINAUP;
                    TID.ItemNameTitle = Database.RARE_ZETTAI_STAMINAUP;
                    TID.Description = "魂の源からオーラの存在を引き出す薬。力＋２００、知＋２００、体＋６００、物防率＋１０％、魔防率＋１０％を付与する。【戦闘中専用】";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.PotionAvailable_48 && (GroundOne.WE2.PotionMixtureDay_48 != 0) && (we.GameDay > GroundOne.WE2.PotionMixtureDay_48))
            {
                GroundOne.WE2.PotionAvailable_48 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.RARE_BLACK_ELIXIR;
                    TID.ItemNameTitle = Database.RARE_BLACK_ELIXIR;
                    TID.Description = "体内に宿る悪しき力を純粋な力へと変換する薬。";
                    TID.Description += "\r\n最大ライフを５０％増加させる。その増加した分だけ、ライフ回復する。【戦闘中専用】";
                    TID.Description += "\r\nライフを減少させる効果（ライフ％減少、ライフ半分、ライフ１変換、ライフ０変換）が来た場合、それを回避する。【戦闘中専用】";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.PotionAvailable_49 && (GroundOne.WE2.PotionMixtureDay_49 != 0) && (we.GameDay > GroundOne.WE2.PotionMixtureDay_49))
            {
                GroundOne.WE2.PotionAvailable_49 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.RARE_ZEPHER_BREATH;
                    TID.ItemNameTitle = Database.RARE_ZEPHER_BREATH;
                    TID.Description = "天性の源から躍動の心を引き出す薬。技＋６００、知＋４００、戦速率＋２０％を付与する。【戦闘中専用】";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.PotionAvailable_410 && (GroundOne.WE2.PotionMixtureDay_410 != 0) && (we.GameDay > GroundOne.WE2.PotionMixtureDay_410))
            {
                GroundOne.WE2.PotionAvailable_410 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.RARE_COLORESS_ANTIDOTE;
                    TID.ItemNameTitle = Database.RARE_COLORESS_ANTIDOTE;
                    TID.Description = "物理的、または精神的な悪循環を払拭させるために開発された特効薬。";
                    TID.Description += "\r\n全てのデバフ効果を解除する。【戦闘中専用】";
                    TID.Description += "\r\n全てのデバフ効果に対する耐性を得る。【戦闘中専用】";
                    TID.ShowDialog();
                }
            }
            #endregion

            // ２重記述だが、ベストコードは後で良しとする。
            if (we.AvailablePotionshop && !we.AvailablePotion2)
            {
                SetupAvailableList(1);
            }
            else if (we.AvailablePotionshop && we.AvailablePotion2 && !we.AvailablePotion3)
            {
                SetupAvailableList(2);
            }
            else if (we.AvailablePotionshop && we.AvailablePotion2 && we.AvailablePotion3 && !we.AvailablePotion4)
            {
                SetupAvailableList(3);
            }
            else if (we.AvailablePotionshop && we.AvailablePotion2 && we.AvailablePotion3 && we.AvailablePotion4 && !we.AvailablePotion5)
            {
                SetupAvailableList(4);
            }
            else if (we.AvailablePotionshop && we.AvailablePotion2 && we.AvailablePotion3 && we.AvailablePotion4 && we.AvailablePotion5)
            {
                SetupAvailableList(5);
            }
            else
            {

            }
        }
    }
}
