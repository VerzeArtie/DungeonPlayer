using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;


namespace DungeonPlayer
{
    public partial class BattleEnemy : MotherForm
    {
        // [警告最大]：戦闘フェーズは複数の味方、複数の敵を想定したスケルトンが必要です。かならず推敲してください。
        // 味方合計3人まで、敵合計4人までとは限りません。拡張性を最初から組んでください。
        private void BattleStart()
        {
            // 戦闘前、アップキープ
            UpkeepStep();

            // 戦闘前、逃げ判定
            if (RunAwayStep(this.mc.PA))
            {
                this.DialogResult = DialogResult.Abort;
                if (this.mc != null)
                {
                    this.mc.CleanUpBattleEnd();
                }
                if (this.sc != null)
                {
                    this.sc.CleanUpBattleEnd();
                }
                if (this.tc != null)
                {
                    this.tc.CleanUpBattleEnd();
                }
                return; // 逃げた場合、強制中断
            }

            // [警告]：この時点で敵の行動が決まってないと、StanceOfStaindingやCounterAttackといったスキルが敵側では効果を発揮しません。
            int enemyActionNum = SetupEnemyAction();

            // [警告]：隊列モードで指定がある場合、味方プレイヤーは順序が固定になるようにしてください。
            // 戦闘開始前、順序の決定
            SortedList<int, MainCharacter> ActiveList = new SortedList<int, MainCharacter>();
            AddActivePlayer(ActiveList, this.mc, this.ec1.Agility, this.ec1.Mind);
            AddActivePlayer(ActiveList, this.sc, this.ec1.Agility, this.ec1.Mind);
            AddActivePlayer(ActiveList, this.tc, this.ec1.Agility, this.ec1.Mind);
            AddActivePlayer(ActiveList, this.ec1);
            // 敵の数が増える場合、随時追加してください。
            // 味方の数が増える場合、随時追加してください。

            //this.textBox1.Text += (ActiveList.Keys[0].ToString() +"   " + ActiveList.Keys[1].ToString() + "   " + ActiveList.Keys[2].ToString() + "   " + ActiveList.Keys[3].ToString()) + "\r\n";


            // 戦闘開始
            for (int ii = 0; ii < ActiveList.Count; ii++)
            {
                if (ActiveList.Values[ii].GetType() == typeof(MainCharacter))
                {
                    if (!ActiveList.Values[ii].Dead)
                    {
                        PlayerAttackPhase(ActiveList.Values[ii]); if (ActiveList.Values[ii].CurrentGaleWind == 1) PlayerAttackPhase(ActiveList.Values[ii], true);
                    }
                }
                else if (ActiveList.Values[ii].GetType() == typeof(EnemyCharacter1))
                {
                    if (!ActiveList.Values[ii].Dead)
                    {
                        if (enemyActionNum != 0)
                        {
                            EnemyAttackPhase(ActiveList.Values[ii].Target, enemyActionNum); if (ActiveList.Values[ii].CurrentGaleWind == 1) EnemyAttackPhase(ActiveList.Values[ii].Target, this.bossAlreadyActionNum);
                        }
                        else
                        {
                            EnemyAttackPhase(); if (ActiveList.Values[ii].CurrentGaleWind == 1) EnemyAttackPhase(ActiveList.Values[ii].Target, this.bossAlreadyActionNum);
                        }
                    }
                }

                if (PlayerDeathCheck()) // 相打ちが発生した場合、プレイヤーが負けるものとする。
                {
                    for (int jj = 0; jj < ActiveList.Count; jj++)
                    {
                        ActiveList.Values[jj].CleanUpBattleEnd();
                    }
                    return; // 何らか影響で自分自身が死んだ場合、強制中断
                }
                if (EnemyDeathCheck())
                {
                    for (int jj = 0; jj < ActiveList.Count; jj++)
                    {
                        ActiveList.Values[jj].CleanUpBattleEnd();
                    }
                    return; // 敵を倒した場合、強制中断
                }

            }

            // 戦闘後の追加効果フェーズ
            AfterBattleEffect();
            if (PlayerDeathCheck())
            {
                for (int jj = 0; jj < ActiveList.Count; jj++)
                {
                    ActiveList.Values[jj].CleanUpBattleEnd();
                }
                return; // 何らか影響で自分自身が死んだ場合、強制中断
            }
            if (EnemyDeathCheck())
            {
                for (int jj = 0; jj < ActiveList.Count; jj++)
                {
                    ActiveList.Values[jj].CleanUpBattleEnd();
                }
                return; // 何らかの影響で敵自身が死んだ場合、強制中断
            }

            // 戦闘終了後、クリーンナップ
            CleanUpStep();
        }


    }
}
