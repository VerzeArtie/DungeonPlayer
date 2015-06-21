using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonPlayer
{
    public partial class TruthEnemyCharacter : MainCharacter
    {
        private void SetupActionWisely(MainCharacter player, MainCharacter target, string commandString)
        {
            PlayerAction pa = TruthActionCommand.CheckPlayerActionFromString(commandString);
            if (player.CurrentBlackContract <= 0)
            {
                if (pa == PlayerAction.UseSkill && player.CurrentSkillPoint < TruthActionCommand.Cost(commandString, player))
                {
                    if (player.CurrentGaleWind <= 0 && player.CurrentMana >= Database.GALE_WIND_COST)
                    {
                        target = player;
                        pa = PlayerAction.UseSpell;
                        commandString = Database.GALE_WIND;
                    }
                    else
                    {
                        target = player;
                        pa = PlayerAction.UseSkill;
                        commandString = Database.INNER_INSPIRATION;
                    }
                }
                else if (pa == PlayerAction.UseSpell && player.CurrentMana < TruthActionCommand.Cost(commandString, player))
                {
                    if (player.CurrentMana >= Database.BLACK_CONTRACT_COST)
                    {
                        target = player;
                        pa = PlayerAction.UseSpell;
                        commandString = Database.BLACK_CONTRACT;
                    }
                    else
                    {
                        target = player;
                        pa = PlayerAction.Defense;
                        commandString = Database.DEFENSE_EN;
                    }
                }
            }

            SetupActionCommand(player, target, pa, commandString);
        }

        private void SetupActionCommand(MainCharacter player, MainCharacter target, PlayerAction pa, string commandString)
        {
            player.PA = pa;
            player.Target = target;
            if (TruthActionCommand.CheckPlayerActionFromString(commandString) == PlayerAction.UseSpell)
            {
                player.CurrentSpellName = commandString;
            }
            else if (TruthActionCommand.CheckPlayerActionFromString(commandString) == PlayerAction.UseSkill)
            {
                player.CurrentSkillName = commandString;
            }
            else if (TruthActionCommand.CheckPlayerActionFromString(commandString) == PlayerAction.Archetype)
            {
                player.CurrentArchetypeName = commandString;
            }
            player.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(commandString);
        }
    }
}
