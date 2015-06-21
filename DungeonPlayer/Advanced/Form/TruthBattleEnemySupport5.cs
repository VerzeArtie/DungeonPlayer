using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DungeonPlayer
{
    public partial class TruthBattleEnemy : MotherForm
    {
        public bool JudgeSelectDefense(MainCharacter activePlayer, string actionCommand)
        {
            if (activePlayer != ec1 && ec1.PA != PlayerAction.Defense && ec1.StackActivation == false && (TruthActionCommand.IsDamage(actionCommand)))
            {
                return true;
            }
            return false;
        }

        public bool CanAction(MainCharacter activePlayer)
        {
            if (activePlayer.CurrentStunning > 0 || activePlayer.CurrentFrozen > 0 || activePlayer.CurrentParalyze > 0)
            {
                return false;
            }
            return true;
        }

        public void ExecStanceOfSuddenness(SortedList<int, MainCharacter> player, string message, int ii)
        {
            //player[ii].CurrentStanceOfSuddenness = false;
            //activePlayer.StackCommandString = String.Empty;
            //activePlayer.StackPlayerAction = PlayerAction.None;
            //activePlayer.StackTarget = null;
            //activePlayer.StackActivePlayer = null;
            //activePlayer.StackActivation = false;
            //StackInTheCommandLabel.ForeColor = System.Drawing.Color.White;
            //StackInTheCommandLabel.BackColor = System.Drawing.Color.Black;
            //StackInTheCommandLabel.Width = Database.TIMEUP_FIRST_RESPONSE;
            //StackInTheCommandLabel.Text = "失敗！(要因：" + player[ii].Name + "のStanceOfSuddenness)";
            //StackInTheCommandLabel.Update();
            //System.Threading.Thread.Sleep(1000);
            //StackNameLabel.Width = 0;
            //StackNameLabel.Update();
            //StackInTheCommandLabel.Width = 0;
            //StackInTheCommandLabel.Update();
            //return;
        }

        public void ExecDefense(SortedList<int, MainCharacter> player, string message, int ii)
        {
            UpdateBattleText(message);
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
            //player[ii].StackActivation = true; // 防御姿勢はスタックアクティベーションに入らず、即時適用となる。
        }
    }
}
