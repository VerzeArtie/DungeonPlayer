using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class TruthBattleEnemy : MotherForm
    {
        /// <summary>
        /// スレッド呼び出し元のメソッド
        /// </summary>
        public void StackInTheCommand()
        {
            for (int ii = 0; ii < this.ActiveList.Count; ii++)
            {
                if (this.ActiveList[ii].StackActivation)
                {
                    this.ActiveList[ii].StackActivation = false;
                    StackInTheCommand(this.ActiveList, this.ActiveList[ii].StackActivePlayer, this.ActiveList[ii].StackTarget, this.ActiveList[ii].StackPlayerAction, this.ActiveList[ii].StackCommandString);
                    break;
                }
            }
        }

        /// <summary>
        /// スタック・イン・ザ・コマンド。（再帰メソッド）
        /// </summary>
        /// <param name="player">プレイヤー全員</param>
        public void StackInTheCommand(SortedList<int, MainCharacter> player, MainCharacter activePlayer, MainCharacter target, PlayerAction pa, string actionCommand)
        {
            try
            {
                int TimeUp = 1200;
                int TimeUpFirstResponse = 600;
                int cumulativeCounter = 0;

                // 【警告】最終DUELフェーズのみ、進行速度を早くするが、本来は戦闘反応値に依存して早くするべきである。
                if (activePlayer.Name == Database.ENEMY_LAST_RANA_AMILIA ||
                    activePlayer.Name == Database.ENEMY_LAST_SINIKIA_KAHLHANZ ||
                    activePlayer.Name == Database.ENEMY_LAST_OL_LANDIS
                    )
                {
                    TimeUp = 800; TimeUpFirstResponse = 200; 
                }
                else if (activePlayer.Name == Database.ENEMY_LAST_VERZE_ARTIE ||
                    activePlayer.Name == Database.ENEMY_LAST_SIN_VERZE_ARTIE)
                {
                    TimeUp = 600; TimeUpFirstResponse = 0;
                }

                UpdateLabelInfo(activePlayer, target, Database.TIMEUP_FIRST_RESPONSE, actionCommand);

                while (true)
                {
                    System.Threading.Thread.Sleep(1);
                    for (int ii = 0; ii < player.Count; ii++)
                    {
                        if (this.NowTimeStop && player[ii].CurrentTimeStop <= 0)
                        {
                            // 時間は飛ばされる
                        }
                        else
                        {
                            // 「警告」敵側のスタックセットの理論を別ソースに移行して展開していってください。
                            #region "シニキア・ヴェイルハンツ"
                            if (player[ii].Name == Database.DUEL_SINIKIA_VEILHANTU)
                            {
                                if (cumulativeCounter > 900)
                                {
                                    if (player[ii].CurrentInstantPoint >= player[ii].MaxInstantPoint)
                                    {
                                        if (((pa == PlayerAction.UseSkill) && (actionCommand == Database.DOUBLE_SLASH)) ||
                                            ((pa == PlayerAction.UseSkill) && (actionCommand == Database.STRAIGHT_SMASH)) ||
                                            ((pa == PlayerAction.UseSkill) && (actionCommand == Database.CRUSHING_BLOW)) ||
                                            ((pa == PlayerAction.UseSkill) && (actionCommand == Database.ENIGMA_SENSE)) ||
                                            ((pa == PlayerAction.UseSkill) && (actionCommand == Database.SILENT_RUSH)) ||
                                            ((pa == PlayerAction.UseSkill) && (actionCommand == Database.OBORO_IMPACT)) ||
                                            ((pa == PlayerAction.UseSkill) && (actionCommand == Database.KINETIC_SMASH)) ||
                                            ((pa == PlayerAction.UseSkill) && (actionCommand == Database.CATASTROPHE)) ||
                                            ((pa == PlayerAction.UseSkill) && (actionCommand == Database.CARNAGE_RUSH))
                                            )
                                        {
                                            UpdateBattleText(ec1.Name + "：『カウンターアタック』を発動だ。\r\n");
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = mc;
                                            player[ii].StackPlayerAction = PlayerAction.UseSkill;
                                            player[ii].StackCommandString = Database.COUNTER_ATTACK;
                                            player[ii].StackActivation = true;
                                        }
                                        else if (((pa == PlayerAction.UseSpell) && (actionCommand == Database.FIRE_BALL)) ||
                                                 ((pa == PlayerAction.UseSpell) && (actionCommand == Database.FLAME_STRIKE)) ||
                                                 ((pa == PlayerAction.UseSpell) && (actionCommand == Database.FRESH_HEAL)) ||
                                                 ((pa == PlayerAction.UseSpell) && (actionCommand == Database.HOLY_SHOCK)) ||
                                                 ((pa == PlayerAction.UseSpell) && (actionCommand == Database.WORD_OF_POWER)) ||
                                                 ((pa == PlayerAction.UseSpell) && (actionCommand == Database.GALE_WIND))
                                            )
                                        {
                                            UpdateBattleText(ec1.Name + "：『ニゲイト』を発動だ。\r\n");
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = mc;
                                            player[ii].StackPlayerAction = PlayerAction.UseSkill;
                                            player[ii].StackCommandString = Database.NEGATE;
                                            player[ii].StackActivation = true;
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region "オル・ランディス"
                            else if (player[ii].Name == Database.DUEL_OL_LANDIS)
                            {
                                if (cumulativeCounter > 950)
                                {
                                    if (player[ii].CurrentInstantPoint >= player[ii].MaxInstantPoint)
                                    {
                                        if (((pa == PlayerAction.UseSkill) && TruthActionCommand.IsDamage(actionCommand)))
                                        {
                                            UpdateBattleText(ec1.Name + "：あめぇんだよ、ザコアイン！\r\n");
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = mc;
                                            player[ii].StackPlayerAction = PlayerAction.UseSkill;
                                            player[ii].StackCommandString = Database.COUNTER_ATTACK;
                                            player[ii].StackActivation = true;
                                        }
                                        //if (player[ii].CurrentSaintPower <= 0)
                                        //{
                                        //    UpdateBattleText(ec1.Name + "：パワーアップだ！！\r\n");
                                        //    player[ii].CurrentInstantPoint = 0;
                                        //    player[ii].StackActivePlayer = player[ii];
                                        //    player[ii].StackTarget = player[ii];
                                        //    player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                        //    player[ii].StackCommandString = Database.SAINT_POWER;
                                        //    player[ii].StackActivation = true;
                                        //}
                                        //else if (player[ii].CurrentGlory <= 0)
                                        //{
                                        //    if ((pa == PlayerAction.UseSkill) && (actionCommand == Database.STRAIGHT_SMASH) ||
                                        //        (pa == PlayerAction.UseSkill) && (actionCommand == Database.DOUBLE_SLASH) ||
                                        //        (pa == PlayerAction.UseSkill) && (actionCommand == Database.ENIGMA_SENSE))
                                        //    {
                                        //        UpdateBattleText(ec1.Name + "：あめぇんだよ、ザコアイン！\r\n");
                                        //        player[ii].CurrentInstantPoint = 0;
                                        //        player[ii].StackActivePlayer = player[ii];
                                        //        player[ii].StackTarget = player[ii];
                                        //        player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                        //        player[ii].StackCommandString = Database.GLORY;
                                        //        player[ii].StackActivation = true;
                                        //    }
                                        //}
                                        //else
                                        //{
                                        //    UpdateBattleText(ec1.Name + "：食らえや！！\r\n");
                                        //    player[ii].CurrentInstantPoint = 0;
                                        //    player[ii].StackActivePlayer = player[ii];
                                        //    player[ii].StackTarget = mc;
                                        //    player[ii].StackPlayerAction = PlayerAction.UseSkill;
                                        //    player[ii].StackCommandString = Database.STRAIGHT_SMASH;
                                        //    player[ii].StackActivation = true;
                                        //}
                                    }
                                }
                            }
                            #endregion
                            #region "スコーティ・ザルゲ"
                            else if (player[ii].Name == Database.DUEL_SCOTY_ZALGE)
                            {
                                if (cumulativeCounter > 900)
                                {
                                    if (player[ii].CurrentInstantPoint >= player[ii].MaxInstantPoint)
                                    {
                                        if ((pa == PlayerAction.UseSkill) && (TruthActionCommand.IsDamage(actionCommand)))
                                        {
                                            bool existItem = false;
                                            ItemBackPack[] tempItem = player[ii].GetBackPackInfo();
                                            foreach (ItemBackPack value in tempItem)
                                            {
                                                if (value != null)
                                                {
                                                    if (value.Name == Database.COMMON_NORMAL_RED_POTION)
                                                    {
                                                        existItem = true;
                                                    }
                                                }
                                            }

                                            if ((ec1.CurrentLife <= ec1.MaxLife / 2) &&
                                                (existItem))
                                            {
                                                UpdateBattleText(ec1.Name + "：ヒャハハハ、待ってましたぁ！ライフ回復ってかぁ！？\r\n");
                                                player[ii].CurrentInstantPoint = 0;
                                                player[ii].StackActivePlayer = player[ii];
                                                player[ii].StackTarget = player[ii];
                                                player[ii].StackPlayerAction = PlayerAction.UseItem;
                                                player[ii].StackCommandString = Database.COMMON_NORMAL_RED_POTION;
                                                player[ii].StackActivation = true;
                                            }
                                            else
                                            {
                                                UpdateBattleText(ec1.Name + "：『カウンターアタック』ってかぁ！？ッヒャハハハ！\r\n");
                                                player[ii].CurrentInstantPoint = 0;
                                                player[ii].StackActivePlayer = player[ii];
                                                player[ii].StackTarget = mc;
                                                player[ii].StackPlayerAction = PlayerAction.UseSkill;
                                                player[ii].StackCommandString = Database.COUNTER_ATTACK;
                                                player[ii].StackActivation = true;
                                            }
                                        }
                                        else
                                        {
                                            if (mc.CurrentDarkenField <= 0)
                                            {
                                                player[ii].CurrentInstantPoint = 0;
                                                player[ii].StackActivePlayer = player[ii];
                                                player[ii].StackTarget = mc;
                                                player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                                player[ii].StackCommandString = Database.WORD_OF_MALICE;
                                                player[ii].StackActivation = true;
                                            }
                                            else
                                            {
                                                player[ii].CurrentInstantPoint = 0;
                                                player[ii].StackActivePlayer = player[ii];
                                                player[ii].StackTarget = mc;
                                                player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                                player[ii].StackCommandString = Database.IMMOLATE;
                                                player[ii].StackActivation = true;
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region "キルト・ジョルジュ"
                            else if (player[ii].Name == Database.DUEL_KILT_JORJU)
                            {
                                if (cumulativeCounter > 950)
                                {
                                    if (JudgeSelectDefense(activePlayer, actionCommand))
                                    {
                                        ExecDefense(player, ec1.Name + "：防御させてもらう！\r\n", ii);
                                    }
                                }
                            }
                            #endregion
                            #region "ヴェルゼ・アーティ"
                            else if ((player[ii].Name == Database.VERZE_ARTIE) && IsPlayerEnemy(player[ii]))
                            {
                                if (cumulativeCounter > 1100)
                                {
                                    if (player[ii].CurrentInstantPoint >= player[ii].MaxInstantPoint)
                                    {
                                        if (((pa == PlayerAction.UseSkill) && TruthActionCommand.IsDamage(actionCommand)) && (player[ii].CurrentCounterAttack <= 0))
                                        {
                                            UpdateBattleText(ec1.Name + "：すみませんが、カウンターさせてもらいます。\r\n");
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = mc;
                                            player[ii].StackPlayerAction = PlayerAction.UseSkill;
                                            player[ii].StackCommandString = Database.COUNTER_ATTACK;
                                            player[ii].StackActivation = true;
                                        }
                                        else if (((pa == PlayerAction.UseSpell) && TruthActionCommand.IsDamage(actionCommand)) && ((player[ii].CurrentMirrorImage <= 0)))
                                        {
                                            if (actionCommand == Database.WORD_OF_POWER)
                                            {
                                            }
                                            else
                                            {
                                                UpdateBattleText(ec1.Name + "：それを喰らうわけには行きませんね、ミラーイメージです。\r\n");
                                                player[ii].CurrentInstantPoint = 0;
                                                player[ii].StackActivePlayer = player[ii];
                                                player[ii].StackTarget = player[ii];
                                                player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                                player[ii].StackCommandString = Database.MIRROR_IMAGE;
                                                player[ii].StackActivation = true;
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region "アンナ・ハミルトン"
                            else if (player[ii].Name == Database.DUEL_ANNA_HAMILTON)
                            {
                                if (cumulativeCounter > 700)
                                {
                                    if (TruthActionCommand.IsDamage(actionCommand))
                                    {
                                        if (activePlayer != ec1 && ec1.PA != PlayerAction.Defense && ec1.StackActivation == false)
                                        {
                                            // 防御はスタック積みではないので、インタラプトと同系列とする。
                                            UpdateBattleText(ec1.Name + "：いやあああぁぁぁ！！　インスタント攻撃なんてしないでよ！！！\r\n");
                                            player[ii].PA = PlayerAction.Defense;
                                            player[ii].ActionLabel.Text = Database.DEFENSE_JP;
                                            Color tempBack = StackInTheCommandLabel.BackColor;
                                            Color tempFore = StackInTheCommandLabel.ForeColor;
                                            StackInTheCommandLabel.ForeColor = System.Drawing.Color.White;
                                            StackInTheCommandLabel.BackColor = System.Drawing.Color.Black;
                                            StackInTheCommandLabel.Width = Database.TIMEUP_FIRST_RESPONSE;
                                            StackInTheCommandLabel.Text = player[ii].Name + "防御の姿勢";
                                            StackInTheCommandLabel.Update();
                                            System.Threading.Thread.Sleep(1000);
                                            StackInTheCommandLabel.Width = 0;
                                            StackInTheCommandLabel.Update();
                                            StackInTheCommandLabel.ForeColor = tempFore;
                                            StackInTheCommandLabel.BackColor = tempBack;
                                        }

                                        if (player[ii].CurrentInstantPoint >= player[ii].MaxInstantPoint)
                                        {
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = player[ii];
                                            player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                            player[ii].StackCommandString = Database.ONE_IMMUNITY;
                                            player[ii].StackActivation = true;
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region "サン・ユウ"
                            if (player[ii].Name == Database.DUEL_SUN_YU)
                            {
                                if (cumulativeCounter > 900)
                                {
                                    if (player[ii].CurrentInstantPoint >= player[ii].MaxInstantPoint)
                                    {
                                        if (((pa == PlayerAction.UseSkill) && (TruthActionCommand.IsDamage(actionCommand))))
                                        {
                                            UpdateBattleText(ec1.Name + "：ッフン、喰らってたまるか、そんなもの！！\r\n");
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = mc;
                                            player[ii].StackPlayerAction = PlayerAction.UseSkill;
                                            player[ii].StackCommandString = Database.COUNTER_ATTACK;
                                            player[ii].StackActivation = true;
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region "シニキア・カールハンツ"
                            else if (player[ii].Name == Database.DUEL_SINIKIA_KAHLHANZ)
                            {
                                if (cumulativeCounter > 1100)
                                {
                                    if (player[ii].CurrentInstantPoint >= player[ii].MaxInstantPoint)
                                    {
                                        if (((pa == PlayerAction.UseSkill) && TruthActionCommand.IsDamage(actionCommand)) && (player[ii].CurrentCounterAttack <= 0))
                                        {
                                            UpdateBattleText(ec1.Name + "：ッフ、カウンターアタック。\r\n");
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = mc;
                                            player[ii].StackPlayerAction = PlayerAction.UseSkill;
                                            player[ii].StackCommandString = Database.COUNTER_ATTACK;
                                            player[ii].StackActivation = true;
                                        }
                                        else if (((pa == PlayerAction.UseSpell) && TruthActionCommand.IsDamage(actionCommand)) && ((player[ii].CurrentMirrorImage <= 0)))
                                        {
                                            if (actionCommand == Database.WORD_OF_POWER)
                                            {
                                            }
                                            else
                                            {
                                                UpdateBattleText(ec1.Name + "：ッフ、ミラーイメージ。\r\n");
                                                player[ii].CurrentInstantPoint = 0;
                                                player[ii].StackActivePlayer = player[ii];
                                                player[ii].StackTarget = player[ii];
                                                player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                                player[ii].StackCommandString = Database.MIRROR_IMAGE;
                                                player[ii].StackActivation = true;
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region "ルベル・ゼルキス"
                            else if (player[ii].Name == Database.DUEL_RVEL_ZELKIS)
                            {
                                if (cumulativeCounter > 1100)
                                {
                                    if (player[ii].CurrentInstantPoint >= player[ii].MaxInstantPoint)
                                    {
                                        if (((pa == PlayerAction.UseSkill) && TruthActionCommand.IsDamage(actionCommand)) && (player[ii].CurrentCounterAttack <= 0))
                                        {
                                            UpdateBattleText(ec1.Name + "：食らってなるものか、カウンターをさせてもらおう。\r\n");
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = mc;
                                            player[ii].StackPlayerAction = PlayerAction.UseSkill;
                                            player[ii].StackCommandString = Database.COUNTER_ATTACK;
                                            player[ii].StackActivation = true;
                                        }
                                        else if (((pa == PlayerAction.UseSpell) && TruthActionCommand.IsDamage(actionCommand)))
                                        {
                                            if (actionCommand == Database.WORD_OF_POWER)
                                            {
                                            }
                                            else
                                            {
                                                UpdateBattleText(ec1.Name + "：食らってなるものか、ニゲイトをさせてもらおう。\r\n");
                                                player[ii].CurrentInstantPoint = 0;
                                                player[ii].StackActivePlayer = player[ii];
                                                player[ii].StackTarget = player[ii];
                                                player[ii].StackPlayerAction = PlayerAction.UseSkill;
                                                player[ii].StackCommandString = Database.NEGATE;
                                                player[ii].StackActivation = true;
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region "ヴァン・ヘーグステル"
                            else if (player[ii].Name == Database.DUEL_VAN_HEHGUSTEL)
                            {
                                if (JudgeSelectDefense(activePlayer, actionCommand))
                                {
                                    ExecDefense(player, ec1.Name + "：防御だな。\r\n", ii);
                                }
                            }
                            #endregion
                            #region "オウリュウ・ゲンマ"
                            else if (player[ii].Name == Database.DUEL_OHRYU_GENMA)
                            {
                                if (JudgeSelectDefense(activePlayer, actionCommand))
                                {
                                    ExecDefense(player, ec1.Name + "：防御じゃのお。\r\n", ii);
                                }
                            }
                            #endregion
                            #region "ラダ・ミストゥルス"
                            else if (player[ii].Name == Database.DUEL_LADA_MYSTORUS)
                            {
                                if (cumulativeCounter > 1100)
                                {
                                    if ((player[ii].CurrentInstantPoint >= player[ii].MaxInstantPoint))
                                    {
                                        if (JudgeSelectDefense(activePlayer, actionCommand))
                                        {
                                            ExecDefense(player, ec1.Name + "：フン、ここは防御だな。\r\n", ii);
                                        }
                                        else if (TruthActionCommand.IsDamage(actionCommand) == false)
                                        {
                                            UpdateBattleText(ec1.Name + "：では、こちらも動き出させてもらう。\r\n");
                                            if (player[ii].CurrentPromisedKnowledge <= 0)
                                            {
                                                player[ii].CurrentInstantPoint = 0;
                                                player[ii].StackActivePlayer = player[ii];
                                                player[ii].StackTarget = player[ii];
                                                player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                                player[ii].StackCommandString = Database.PROMISED_KNOWLEDGE;
                                            }
                                            else if (player[ii].CurrentWordOfLife <= 0)
                                            {
                                                player[ii].CurrentInstantPoint = 0;
                                                player[ii].StackActivePlayer = player[ii];
                                                player[ii].StackTarget = player[ii];
                                                player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                                player[ii].StackCommandString = Database.WORD_OF_LIFE;
                                            }
                                            else
                                            {
                                                player[ii].CurrentInstantPoint = 0;
                                                player[ii].StackActivePlayer = player[ii];
                                                player[ii].StackTarget = mc;
                                                player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                                player[ii].StackCommandString = Database.FLASH_BLAZE;
                                            }
                                            player[ii].StackActivation = true;
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region "シン・オスキュレーテ"
                            else if (player[ii].Name == Database.DUEL_SIN_OSCURETE)
                            {
                                if (cumulativeCounter > 1100)
                                {
                                    if ((player[ii].CurrentInstantPoint >= player[ii].MaxInstantPoint))
                                    {
                                        if (TruthActionCommand.IsDamage(actionCommand))
                                        {
                                            if (pa == PlayerAction.UseSpell && mc.CurrentSilence <= 0)
                                            {
                                                UpdateBattleText(ec1.Name + "：沈黙してもらいます。\r\n");
                                                player[ii].CurrentInstantPoint = 0;
                                                player[ii].StackActivePlayer = player[ii];
                                                player[ii].StackTarget = mc;
                                                player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                                player[ii].StackCommandString = Database.VANISH_WAVE;
                                                player[ii].StackActivation = true;
                                            }
                                            else
                                            {
                                                if (JudgeSelectDefense(activePlayer, actionCommand))
                                                {
                                                    ExecDefense(player, ec1.Name + "：防御ですかね。\r\n", ii);
                                                }
                                            }
                                        }
                                        else if (((pa == PlayerAction.UseSpell) && TruthActionCommand.IsHeal(actionCommand)))
                                        {
                                            UpdateBattleText(ec1.Name + "：カウンターしておきましょう。\r\n");
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = player[ii];
                                            player[ii].StackPlayerAction = PlayerAction.UseSkill;
                                            player[ii].StackCommandString = Database.NEGATE;
                                            player[ii].StackActivation = true;
                                        }
                                        else
                                        {
                                            UpdateBattleText(ec1.Name + "：レスポンスします。\r\n");
                                            if (((TruthEnemyCharacter)player[ii]).AI_TacticsNumber == 0)
                                            {
                                                if (player[ii].CurrentPromisedKnowledge <= 0)
                                                {
                                                    player[ii].CurrentInstantPoint = 0;
                                                    player[ii].StackActivePlayer = player[ii];
                                                    player[ii].StackTarget = player[ii];
                                                    player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                                    player[ii].StackCommandString = Database.PROMISED_KNOWLEDGE;
                                                }
                                                else if (player[ii].CurrentRiseOfImage <= 0)
                                                {
                                                    player[ii].CurrentInstantPoint = 0;
                                                    player[ii].StackActivePlayer = player[ii];
                                                    player[ii].StackTarget = player[ii];
                                                    player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                                    player[ii].StackCommandString = Database.RISE_OF_IMAGE;
                                                }
                                                else if (player[ii].CurrentBloodyVengeance <= 0)
                                                {
                                                    player[ii].CurrentInstantPoint = 0;
                                                    player[ii].StackActivePlayer = player[ii];
                                                    player[ii].StackTarget = player[ii];
                                                    player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                                    player[ii].StackCommandString = Database.BLOODY_VENGEANCE;
                                                }
                                                else if (player[ii].CurrentHeatBoost <= 0)
                                                {
                                                    player[ii].CurrentInstantPoint = 0;
                                                    player[ii].StackActivePlayer = player[ii];
                                                    player[ii].StackTarget = player[ii];
                                                    player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                                    player[ii].StackCommandString = Database.HEAT_BOOST;
                                                }
                                                else if (mc.CurrentMana > 0)
                                                {
                                                    player[ii].CurrentInstantPoint = 0;
                                                    player[ii].StackActivePlayer = player[ii];
                                                    player[ii].StackTarget = mc;
                                                    player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                                    player[ii].StackCommandString = Database.DOOM_BLADE;
                                                }
                                                else
                                                {
                                                    player[ii].CurrentInstantPoint = 0;
                                                    player[ii].StackActivePlayer = player[ii];
                                                    player[ii].StackTarget = mc;
                                                    player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                                    player[ii].StackCommandString = Database.WORD_OF_POWER;
                                                }
                                            }
                                            else
                                            {
                                                if (mc.CurrentWordOfMalice <= 0) // 戦闘反応
                                                {
                                                    player[ii].CurrentInstantPoint = 0;
                                                    player[ii].StackActivePlayer = player[ii];
                                                    player[ii].StackTarget = mc;
                                                    player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                                    player[ii].StackCommandString = Database.WORD_OF_MALICE;
                                                }
                                                else if (mc.CurrentImmolate <= 0) // 物理防御
                                                {
                                                    player[ii].CurrentInstantPoint = 0;
                                                    player[ii].StackActivePlayer = player[ii];
                                                    player[ii].StackTarget = mc;
                                                    player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                                    player[ii].StackCommandString = Database.IMMOLATE;
                                                }
                                                else if (mc.CurrentBlackFire <= 0) // 魔法防御
                                                {
                                                    player[ii].CurrentInstantPoint = 0;
                                                    player[ii].StackActivePlayer = player[ii];
                                                    player[ii].StackTarget = mc;
                                                    player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                                    player[ii].StackCommandString = Database.BLACK_FIRE;
                                                }
                                                else if (mc.CurrentMana > 0)
                                                {
                                                    player[ii].CurrentInstantPoint = 0;
                                                    player[ii].StackActivePlayer = player[ii];
                                                    player[ii].StackTarget = mc;
                                                    player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                                    player[ii].StackCommandString = Database.DOOM_BLADE;
                                                }
                                                else
                                                {
                                                    player[ii].CurrentInstantPoint = 0;
                                                    player[ii].StackActivePlayer = player[ii];
                                                    player[ii].StackTarget = mc;
                                                    player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                                    player[ii].StackCommandString = Database.WORD_OF_POWER;
                                                }
                                            }
                                            player[ii].StackActivation = true;
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region "ラナ・アミリア（DUEL)"
                            else if (player[ii].Name == Database.ENEMY_LAST_RANA_AMILIA)           
                            {
                                if (cumulativeCounter > 1100 && CanAction(player[ii]))
                                {
                                    if ((player[ii].CurrentInstantPoint >= player[ii].MaxInstantPoint))
                                    {
                                        if (actionCommand == Database.CHILL_BURN)
                                        {
                                            if (player[ii].CurrentSkillPoint >= Database.NEGATE_COST)
                                            {
                                                UpdateBattleText(ec1.Name + "：止められるわけにはいかないわ、Negate！\r\n");
                                                player[ii].CurrentInstantPoint = 0;
                                                player[ii].StackActivePlayer = player[ii];
                                                player[ii].StackTarget = mc;
                                                player[ii].StackPlayerAction = PlayerAction.UseSkill;
                                                player[ii].StackCommandString = Database.NEGATE;
                                                player[ii].StackActivation = true;
                                            }
                                        }
                                        else if ((player[ii].CurrentOneImmunity <= 0) && (player[ii].CurrentMana >= Database.ONE_IMMUNITY_COST))
                                        {
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = player[ii];
                                            player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                            player[ii].StackCommandString = Database.ONE_IMMUNITY;
                                            player[ii].StackActivation = true;
                                        }
                                        else if (JudgeSelectDefense(activePlayer, actionCommand))
                                        {
                                            ExecDefense(player, ec1.Name + "：防御の体制を取るわ。\r\n", ii);
                                        }
                                        else if (pa == PlayerAction.UseSpell && !TruthActionCommand.IsDamage(actionCommand))
                                        {
                                            UpdateBattleText(ec1.Name + "：それもカウンターさせてもらうわ、DeepMirrorよ！\r\n");
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = mc;
                                            player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                            player[ii].StackCommandString = Database.DEEP_MIRROR;
                                            player[ii].StackActivation = true;
                                        }
                                        else if (pa == PlayerAction.UseSpell)
                                        {
                                            UpdateBattleText(ec1.Name + "：沈黙させるわ、VanishWave！\r\n");
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = mc;
                                            player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                            player[ii].StackCommandString = Database.VANISH_WAVE;
                                            player[ii].StackActivation = true;
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region "シニキア・カールハンツ(DUEL2)"
                            else if (player[ii].Name == Database.ENEMY_LAST_SINIKIA_KAHLHANZ)
                            {
                                if (cumulativeCounter > 1100 && CanAction(player[ii]))
                                {
                                    if ((player[ii].CurrentInstantPoint >= player[ii].MaxInstantPoint))
                                    {
                                        if ((pa == PlayerAction.UseSpell) && (player[ii].CurrentSkillPoint >= Database.NEGATE_COST) && (actionCommand != Database.WORD_OF_POWER) )
                                        {
                                            UpdateBattleText(ec1.Name + "：それは止めさせてもらおう、Negateだ。\r\n");
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = mc;
                                            player[ii].StackPlayerAction = PlayerAction.UseSkill;
                                            player[ii].StackCommandString = Database.NEGATE;
                                            player[ii].StackActivation = true;
                                        }
                                        else if ((pa == PlayerAction.UseSpell) && (player[ii].CurrentMana >= Database.VANISH_WAVE_COST))
                                        {
                                            UpdateBattleText(ec1.Name + "：沈黙させてもらうぞ、VanishWaveだ。\r\n");
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = mc;
                                            player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                            player[ii].StackCommandString = Database.VANISH_WAVE;
                                            player[ii].StackActivation = true;
                                        }
                                        else if ((player[ii].CurrentOneImmunity <= 0) && (player[ii].CurrentMana >= Database.ONE_IMMUNITY_COST))
                                        {
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = player[ii];
                                            player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                            player[ii].StackCommandString = Database.ONE_IMMUNITY;
                                            player[ii].StackActivation = true;
                                        }
                                        else if (JudgeSelectDefense(activePlayer, actionCommand))
                                        {
                                            ExecDefense(player, ec1.Name + "：防御させてもらおう。\r\n", ii);
                                        }
                                        else if (pa == PlayerAction.UseSpell && !TruthActionCommand.IsDamage(actionCommand))
                                        {
                                            UpdateBattleText(ec1.Name + "：ッフ、DeepMirrorだ。\r\n");
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = mc;
                                            player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                            player[ii].StackCommandString = Database.DEEP_MIRROR;
                                            player[ii].StackActivation = true;
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region "オル・ランディス（DUEL2)"
                            else if (player[ii].Name == Database.ENEMY_LAST_OL_LANDIS)
                            {
                                if (cumulativeCounter > 1100 && CanAction(player[ii]))
                                {
                                    if (JudgeSelectDefense(activePlayer, actionCommand))
                                    {
                                        ExecDefense(player, ec1.Name + "：ッケ、防御だ。\r\n", ii);
                                    }

                                    if ((player[ii].CurrentInstantPoint >= player[ii].MaxInstantPoint))
                                    {
                                        if ((pa == PlayerAction.UseSpell) && (player[ii].CurrentSkillPoint >= Database.HARDEST_PARRY_COST) && (actionCommand == Database.WORD_OF_POWER))
                                        {
                                            UpdateBattleText(ec1.Name + "：ケッ、パリィ発動！\r\n");
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = mc;
                                            player[ii].StackPlayerAction = PlayerAction.UseSkill;
                                            player[ii].StackCommandString = Database.HARDEST_PARRY;
                                            player[ii].StackActivation = true;
                                        }
                                        else if (TruthActionCommand.IsHeal(actionCommand) && (player[ii].CurrentMana >= Database.DEMONIC_IGNITE_COST))
                                        {
                                            UpdateBattleText(ec1.Name + "：させっかよ、ディーモ！\r\n");
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = mc;
                                            player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                            player[ii].StackCommandString = Database.DEMONIC_IGNITE;
                                            player[ii].StackActivation = true;
                                        }
                                        else if (player[ii].CurrentBlackContract > 0)
                                        {
                                            // スタンス・オブ・サッドネスは即時発動なので、スタック乗せがどうなるか
                                        }
                                        else if ((pa == PlayerAction.UseSpell) && (player[ii].CurrentSkillPoint >= Database.NEGATE_COST))
                                        {
                                            UpdateBattleText(ec1.Name + "：あめぇ、ニゲイト！\r\n");
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = mc;
                                            player[ii].StackPlayerAction = PlayerAction.UseSkill;
                                            player[ii].StackCommandString = Database.NEGATE;
                                            player[ii].StackActivation = true;
                                        }
                                        else if ((pa == PlayerAction.UseSkill) && (TruthActionCommand.IsDamage(actionCommand)) && (player[ii].CurrentSkillPoint >= Database.COUNTER_ATTACK_COST))
                                        {
                                            UpdateBattleText(ec1.Name + "：あめぇ、カウンター！\r\n");
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = player[ii];
                                            player[ii].StackPlayerAction = PlayerAction.UseSkill;
                                            player[ii].StackCommandString = Database.COUNTER_ATTACK;
                                            player[ii].StackActivation = true;
                                        }
                                    }
                                }
                            }
                            #endregion
                            #region "ヴェルゼ・アーティ（最終戦）
                            else if (player[ii].Name == Database.ENEMY_LAST_VERZE_ARTIE)
                            {
                                if (cumulativeCounter > 1100 && CanAction(player[ii]))
                                {
                                    if (JudgeSelectDefense(activePlayer, actionCommand))
                                    {
                                        ExecDefense(player, ec1.Name + "：防御させてもらいましょう。\r\n", ii);
                                    }
                                }
                            }
                            #endregion
                            #region "ヴェルゼ・アーティ（最終戦２）
                            else if (player[ii].Name == Database.ENEMY_LAST_SIN_VERZE_ARTIE)
                            {
                                if (cumulativeCounter > 1100 && CanAction(player[ii]))
                                {
                                    if ((player[ii].CurrentInstantPoint >= player[ii].MaxInstantPoint))
                                    {
                                        if (pa == PlayerAction.UseSpell && !TruthActionCommand.IsDamage(actionCommand))
                                        {
                                            UpdateBattleText(ec1.Name + "：甘いですね、DeepMirror。\r\n");
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = mc;
                                            player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                            player[ii].StackCommandString = Database.DEEP_MIRROR;
                                            player[ii].StackActivation = true;
                                        }
                                        else if (actionCommand == Database.CHILL_BURN)
                                        {
                                            if (player[ii].CurrentSkillPoint >= Database.NEGATE_COST)
                                            {
                                                UpdateBattleText(ec1.Name + "：ックク、Negateです。\r\n");
                                                player[ii].CurrentInstantPoint = 0;
                                                player[ii].StackActivePlayer = player[ii];
                                                player[ii].StackTarget = mc;
                                                player[ii].StackPlayerAction = PlayerAction.UseSkill;
                                                player[ii].StackCommandString = Database.NEGATE;
                                                player[ii].StackActivation = true;
                                            }
                                        }
                                        else if (actionCommand == Database.PIERCING_FLAME && !ec1.DetectCannotBeSilence)
                                        {
                                            UpdateBattleText(ec1.Name + "：少し黙っていてもらいましょう、VanishWave。\r\n");
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = mc;
                                            player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                            player[ii].StackCommandString = Database.VANISH_WAVE;
                                            player[ii].StackActivation = true;
                                        }
                                        else if (actionCommand == Database.PIERCING_FLAME && player[ii].CurrentSkillPoint >= Database.NEGATE_COST)
                                        {
                                            UpdateBattleText(ec1.Name + "：ックク、Negateです。\r\n");
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = mc;
                                            player[ii].StackPlayerAction = PlayerAction.UseSkill;
                                            player[ii].StackCommandString = Database.NEGATE;
                                            player[ii].StackActivation = true;
                                        }
                                        else if ((player[ii].CurrentOneImmunity <= 0) && (player[ii].CurrentMana >= Database.ONE_IMMUNITY_COST))
                                        {
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = player[ii];
                                            player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                            player[ii].StackCommandString = Database.ONE_IMMUNITY;
                                            player[ii].StackActivation = true;
                                        }
                                        else if (JudgeSelectDefense(activePlayer, actionCommand))
                                        {
                                            ExecDefense(player, ec1.Name + "：利きませんよ、それは。\r\n", ii);
                                        }
                                        else if (pa == PlayerAction.UseSpell)
                                        {
                                            UpdateBattleText(ec1.Name + "：少し黙っていてもらいましょう、VanishWave。\r\n");
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = mc;
                                            player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                            player[ii].StackCommandString = Database.VANISH_WAVE;
                                            player[ii].StackActivation = true;
                                        }
                                    }
                                }
                                if (cumulativeCounter > 1100 && CanAction(player[ii]))
                                {
                                    if (JudgeSelectDefense(activePlayer, actionCommand))
                                    {
                                        ExecDefense(player, ec1.Name + "：防御させてもらいましょう。\r\n", ii);
                                    }
                                }
                            }
                            #endregion
                            #region "ダミー素振り君"
                            else if (player[ii].Name == Database.DUEL_DUMMY_SUBURI)
                            {
                                if (cumulativeCounter > 1100)
                                {
                                    if (player[ii].CurrentInstantPoint >= player[ii].MaxInstantPoint)
                                    {
                                        if (((pa == PlayerAction.UseSkill) && TruthActionCommand.IsDamage(actionCommand)) && (player[ii].CurrentCounterAttack <= 0))
                                        {
                                            UpdateBattleText(ec1.Name + "：フレッシュヒール。\r\n");
                                            player[ii].CurrentInstantPoint = 0;
                                            player[ii].StackActivePlayer = player[ii];
                                            player[ii].StackTarget = player[ii];
                                            player[ii].StackPlayerAction = PlayerAction.UseSpell;
                                            player[ii].StackCommandString = Database.FRESH_HEAL;
                                            player[ii].StackActivation = true;
                                        }
                                        //else if (((pa == PlayerAction.UseSpell) && TruthActionCommand.IsDamage(actionCommand)) && ((player[ii].CurrentNegate == false)))
                                        //{
                                        //    UpdateBattleText(ec1.Name + "：ニゲイト。\r\n");
                                        //    player[ii].CurrentInstantPoint = 0;
                                        //    player[ii].StackActivePlayer = player[ii];
                                        //    player[ii].StackTarget = player[ii];
                                        //    player[ii].StackPlayerAction = PlayerAction.UseSkill;
                                        //    player[ii].StackCommandString = Database.NEGATE;
                                        //    player[ii].StackActivation = true;
                                        //}
                                    }
                                }
                            }
                            #endregion
                        }

                        #region "対戦相手が特定のカウンター系をいれこんでいる場合、打ち消しが実施される。"
                        if (DetectOpponentParty(activePlayer, player[ii]))
                        {
                            int failActionTime = 601;
                            if (player[ii].CurrentFutureVision > 0 && cumulativeCounter > failActionTime)
                            {
                                activePlayer.StackCommandString = String.Empty;
                                activePlayer.StackPlayerAction = PlayerAction.None;
                                activePlayer.StackTarget = null;
                                activePlayer.StackActivePlayer = null;
                                activePlayer.StackActivation = false;
                                StackInTheCommandLabel.ForeColor = System.Drawing.Color.White;
                                StackInTheCommandLabel.BackColor = System.Drawing.Color.Black;
                                StackInTheCommandLabel.Width = Database.TIMEUP_FIRST_RESPONSE;
                                StackInTheCommandLabel.Text = "失敗！(要因：" + player[ii].Name + "のFutureVision)";
                                StackInTheCommandLabel.Update();
                                System.Threading.Thread.Sleep(1000);
                                StackNameLabel.Width = 0;
                                StackNameLabel.Update();
                                StackInTheCommandLabel.Width = 0;
                                StackInTheCommandLabel.Update(); 
                                return;
                            }

                            if (player[ii].CurrentDeepMirror == true && cumulativeCounter > failActionTime)
                            {
                                player[ii].CurrentDeepMirror = false;
                                if (TruthActionCommand.IsDamage(actionCommand) == false)
                                {
                                    // 非ダメージ系統の場合のみ発動する。
                                    activePlayer.StackCommandString = String.Empty;
                                    activePlayer.StackPlayerAction = PlayerAction.None;
                                    activePlayer.StackTarget = null;
                                    activePlayer.StackActivePlayer = null;
                                    activePlayer.StackActivation = false;
                                    StackInTheCommandLabel.ForeColor = System.Drawing.Color.White;
                                    StackInTheCommandLabel.BackColor = System.Drawing.Color.Black;
                                    StackInTheCommandLabel.Width = Database.TIMEUP_FIRST_RESPONSE;
                                    StackInTheCommandLabel.Text = "失敗！(要因：" + player[ii].Name + "のDeepMirror)";
                                    StackInTheCommandLabel.Update();
                                    System.Threading.Thread.Sleep(1000);
                                    StackNameLabel.Width = 0;
                                    StackNameLabel.Update();
                                    StackInTheCommandLabel.Width = 0;
                                    StackInTheCommandLabel.Update();
                                    return;
                                }
                            }
                        }
                        #endregion

                        #region "他のプレイヤーがコマンドスタックを載せてきた場合"
                        if ((player[ii].StackActivation))
                        {
                            player[ii].StackActivation = false;

                            #region "StanceOfSuddnessによるカウンター"
                            if (player[ii].StackCommandString == Database.STANCE_OF_SUDDENNESS)
                            {
                                player[ii].CurrentStanceOfSuddenness = false;
                                activePlayer.StackCommandString = String.Empty;
                                activePlayer.StackPlayerAction = PlayerAction.None;
                                activePlayer.StackTarget = null;
                                activePlayer.StackActivePlayer = null;
                                activePlayer.StackActivation = false;
                                StackInTheCommandLabel.ForeColor = System.Drawing.Color.White;
                                StackInTheCommandLabel.BackColor = System.Drawing.Color.Black;
                                StackInTheCommandLabel.Width = Database.TIMEUP_FIRST_RESPONSE;
                                StackInTheCommandLabel.Text = "失敗！(要因：" + player[ii].Name + "のStanceOfSuddenness)";
                                StackInTheCommandLabel.Update();
                                System.Threading.Thread.Sleep(1000);
                                StackNameLabel.Width = 0;
                                StackNameLabel.Update();
                                StackInTheCommandLabel.Width = 0;
                                StackInTheCommandLabel.Update();
                                return;
                            }
                            #endregion

                            // インスタント対象の場合、ここでターゲットを記載する（メインメソッドのターゲット指定では指定できない）
                            if (player[ii].StackCommandString == Database.DEEP_MIRROR)
                            {
                                player[ii].StackTarget = activePlayer;
                            }

                            StackInTheCommand(player, player[ii], player[ii].StackTarget, player[ii].StackPlayerAction, player[ii].StackCommandString);

                            int temp = 0;
                            if (cumulativeCounter > TimeUpFirstResponse)
                            {
                                temp = (cumulativeCounter - TimeUpFirstResponse) / (TimeUp / Database.TIMEUP_FIRST_RESPONSE);
                            }

                            // RumbleShoutにより、第ニパラメタをtargetからactivePlayer.StackTargetに変更。概念の見直しは必要と考えられる。
                            UpdateLabelInfo(activePlayer, activePlayer.StackTarget, Database.TIMEUP_FIRST_RESPONSE - temp, actionCommand);
                        }
                        #endregion
                    }

                    #region "通常 or 即時元核奥義 or 即時リカバー"
                    if ((cumulativeCounter >= TimeUp + TimeUpFirstResponse) ||
                        (TruthActionCommand.CheckPlayerActionFromString(actionCommand) == PlayerAction.Archetype) ||
                        ((TruthActionCommand.CheckPlayerActionFromString(actionCommand) == PlayerAction.UseSkill) && (actionCommand == Database.RECOVER)))
                    {
                        if (TruthActionCommand.CheckPlayerActionFromString(actionCommand) == PlayerAction.Archetype)
                        {
                            if (actionCommand == Database.ARCHETYPE_EIN)
                            {
                                StackInTheCommandLabel.Text = "【元核】　【集中と断絶】　【発動！】";
                            }
                            else if (actionCommand == Database.ARCHETYPE_RANA)
                            {
                                StackInTheCommandLabel.Text = "【元核】　【循環の誓約】　【発動！】";
                            }
                            else if (actionCommand == Database.ARCHETYPE_OL)
                            {
                                StackInTheCommandLabel.Text = "【元核】　【オラオラオラァ！】　【発動！】";
                            }
                            else if (actionCommand == Database.ARCHETYPE_VERZE)
                            {
                                StackInTheCommandLabel.Text = "【元核】　【真実の破壊】　【発動！】";
                            }
                            UpdateLabelInfo(activePlayer, target, Database.TIMEUP_FIRST_RESPONSE, actionCommand);
                            System.Threading.Thread.Sleep(2000);
                        }
                        else if ((TruthActionCommand.CheckPlayerActionFromString(actionCommand) == PlayerAction.UseSkill) && (actionCommand == Database.RECOVER))
                        {
                            StackInTheCommandLabel.Text = "【リカバー】発動！";
                            UpdateLabelInfo(activePlayer, target, Database.TIMEUP_FIRST_RESPONSE, actionCommand);
                            System.Threading.Thread.Sleep(1000);
                        }

                        if (activePlayer.Dead == false)
                        {
                            PlayerAttackPhase(activePlayer, target, pa, actionCommand, false, false, false);
                        }
                        UpdatePlayerDeadFlag(); // 死亡判定・全滅判定更新
                        break;
                    }
                    #endregion

                    #region "スタックコマンド待機時間累積更新"
                    // 【警告】最終DUELフェーズのみ、進行速度を早くするが、本来は戦闘反応値に依存して早くするべきである。
                    if (activePlayer.Name == Database.VERZE_ARTIE ||
                        activePlayer.Name == Database.ENEMY_LAST_VERZE_ARTIE ||
                        activePlayer.Name == Database.ENEMY_LAST_SIN_VERZE_ARTIE)
                    {
                        if (actionCommand == Database.NEUTRAL_SMASH)
                        {
                            cumulativeCounter += 5;
                        }
                        else
                        {
                            cumulativeCounter += 2;
                        }
                    }
                    else if (activePlayer.Name == Database.ENEMY_LAST_RANA_AMILIA ||
                             activePlayer.Name == Database.ENEMY_LAST_SINIKIA_KAHLHANZ ||
                             activePlayer.Name == Database.ENEMY_LAST_OL_LANDIS
                        )
                    {
                        cumulativeCounter += 2;
                    }
                    else if (this.FinalBattle)
                    {
                        cumulativeCounter += 3;
                    }
                    else if (this.HiSpeedAnimation)
                    {
                        cumulativeCounter += 2;
                    }
                    else
                    {
                        cumulativeCounter++;
                    }
                    #endregion

                    int temp2 = 0;
                    if (cumulativeCounter > TimeUpFirstResponse)
                    {
                        temp2 = (cumulativeCounter - TimeUpFirstResponse) / (TimeUp / Database.TIMEUP_FIRST_RESPONSE);
                    }
                    StackInTheCommandLabel.Width = Database.TIMEUP_FIRST_RESPONSE - temp2;
                    StackInTheCommandLabel.Text = Convert.ToString(Database.TIMEUP_FIRST_RESPONSE - temp2);
                    StackInTheCommandLabel.Update();
                }
                activePlayer.StackCommandString = String.Empty;
                activePlayer.StackPlayerAction = PlayerAction.None;
                activePlayer.StackTarget = null;
                activePlayer.StackActivePlayer = null;
                activePlayer.StackActivation = false;

                StackNameLabel.Width = 0;
                StackNameLabel.Update();
                StackInTheCommandLabel.Width = 0;
                StackInTheCommandLabel.Update();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(Database.BattleRoutineError + "\r\n exception: " + ex.ToString());
            }
        }

        private void UpdateLabelInfo(MainCharacter activePlayer, MainCharacter target, int baseWidth, string actionCommand)
        {
            StackNameLabel.Width = 600;
            StackNameLabel.Text = actionCommand;
            StackNameLabel.Text += "    " + activePlayer.Name + " --> ";
            if (target != null)
            {
                StackNameLabel.Text += "  " + target.Name;
            }
            else
            {
                StackNameLabel.Text += "  " + "全体"; // 「警告」絡みつくフランシスのファイアビューネが発端となっている。全体考察してください。
            }

            if (actionCommand == Database.ARCHETYPE_EIN)
            {
                StackNameLabel.BackColor = Color.Black;
            }
            else if (actionCommand == Database.RECOVER)
            {
                StackNameLabel.BackColor = Color.Black;
            }
            else if (activePlayer == mc || activePlayer == sc || activePlayer == tc)
            {
                StackNameLabel.BackColor = System.Drawing.Color.Blue;
            }
            else
            {
                StackNameLabel.BackColor = System.Drawing.Color.Red;
            }
            StackNameLabel.Update();

            StackInTheCommandLabel.Width = Database.TIMEUP_FIRST_RESPONSE;
            if (TruthActionCommand.CheckPlayerActionFromString(actionCommand) == PlayerAction.Archetype)
            {
                StackInTheCommandLabel.BackColor = Color.Black;
            }
            else if (activePlayer == mc || activePlayer == sc || activePlayer == tc)
            {
                StackInTheCommandLabel.BackColor = System.Drawing.Color.Blue;
            }
            else
            {
                StackInTheCommandLabel.BackColor = System.Drawing.Color.Red;
            }
            StackInTheCommandLabel.Update();
        }
    }
}
