using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DungeonPlayer
{
    public partial class TruthBattleEnemy : MotherForm
    {

        /// <summary>
        /// 物理攻撃上昇BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffUpPhysicalAttack(MainCharacter player, double effectValue, int turn = 999)
        {
            UpdateBattleText(player.Name + "は【物理攻撃】が" + effectValue.ToString() + "上昇\r\n");
            this.Invoke(new _AnimationDamage(AnimationDamage), 0, player, 0, Color.Black, false, false, "物理攻撃UP");
            player.CurrentPhysicalAttackUp = turn;
            player.CurrentPhysicalAttackUpValue = (int)effectValue;
            player.ActivateBuff(player.pbPhysicalAttackUp, Database.BaseResourceFolder + Database.BUFF_PHYSICAL_ATTACK_UP, turn);
        }

        /// <summary>
        /// 物理攻撃減少BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffDownPhysicalAttack(MainCharacter player, double effectValue, int turn = 999)
        {
            if (player.CheckResistPhysicalAttackDown)
            {
                UpdateBattleText(player.Name + "は、物理攻撃DOWN効果を受けなかった！\r\n");
                return;
            }
            if (((player.Accessory != null) && (player.Accessory.Name == Database.COMMON_ROYAL_GUARD_RING)) ||
                ((player.Accessory2 != null) && (player.Accessory2.Name == Database.COMMON_ROYAL_GUARD_RING)))
            {
                UpdateBattleText(player.Name + "にかけられた物理攻撃DOWN効果は無効化された！\r\n");
                return;
            }
            
            UpdateBattleText(player.Name + "は【物理攻撃】が" + effectValue.ToString() + "減少\r\n");
            this.Invoke(new _AnimationDamage(AnimationDamage), 0, player, 0, Color.Black, false, false, "物理攻撃DOWN");
            player.CurrentPhysicalAttackDown = turn;
            player.CurrentPhysicalAttackDownValue = (int)effectValue;
            player.ActivateBuff(player.pbPhysicalAttackDown, Database.BaseResourceFolder + Database.BUFF_PHYSICAL_ATTACK_DOWN, turn);
        }

        /// <summary>
        /// 魔法攻撃上昇BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffUpMagicAttack(MainCharacter player, double effectValue, int turn = 999)
        {
            UpdateBattleText(player.Name + "は【魔法攻撃】が" + effectValue.ToString() + "上昇\r\n");
            this.Invoke(new _AnimationDamage(AnimationDamage), 0, player, 0, Color.Black, false, false, "魔法攻撃UP");
            player.CurrentMagicAttackUp = turn;
            player.CurrentMagicAttackUpValue = (int)effectValue;
            player.ActivateBuff(player.pbMagicAttackUp, Database.BaseResourceFolder + Database.BUFF_MAGIC_ATTACK_UP, turn);
        }

        /// <summary>
        /// 魔法攻撃減少BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffDownMagicAttack(MainCharacter player, double effectValue, int turn = 999)
        {
            if (player.CheckResistMagicAttackDown)
            {
                UpdateBattleText(player.Name + "は、魔法攻撃DOWN効果を受けなかった！\r\n");
                return;
            }
            if (((player.Accessory != null) && (player.Accessory.Name == Database.COMMON_ELEMENTAL_GUARD_RING)) ||
                ((player.Accessory2 != null) && (player.Accessory2.Name == Database.COMMON_ELEMENTAL_GUARD_RING)))
            {
                UpdateBattleText(player.Name + "にかけられた魔法攻撃DOWN効果は無効化された！\r\n");
                return;
            }

            UpdateBattleText(player.Name + "は【魔法攻撃】が" + effectValue.ToString() + "減少\r\n");
            this.Invoke(new _AnimationDamage(AnimationDamage), 0, player, 0, Color.Black, false, false, "魔法攻撃DOWN");
            player.CurrentMagicAttackDown = turn;
            player.CurrentMagicAttackDownValue = (int)effectValue;
            player.ActivateBuff(player.pbMagicAttackDown, Database.BaseResourceFolder + Database.BUFF_MAGIC_ATTACK_DOWN, turn);
        }

        /// <summary>
        /// 物理防御上昇BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffUpPhysicalDefense(MainCharacter player, double effectValue, int turn = 999)
        {
            UpdateBattleText(player.Name + "は【物理防御】が" + effectValue.ToString() + "上昇\r\n");
            this.Invoke(new _AnimationDamage(AnimationDamage), 0, player, 0, Color.Black, false, false, "物理防御UP");
            player.CurrentPhysicalDefenseUp = turn;
            player.CurrentPhysicalDefenseUpValue = (int)effectValue;
            player.ActivateBuff(player.pbPhysicalDefenseUp, Database.BaseResourceFolder + Database.BUFF_PHYSICAL_DEFENSE_UP, turn);
        }

        /// <summary>
        /// 物理防御減少BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffDownPhysicalDefense(MainCharacter player, double effectValue, int turn = 999)
        {
            if (player.CheckResistPhysicalDefenseDown)
            {
                UpdateBattleText(player.Name + "は、物理防御DOWN効果を受けなかった！\r\n");
                return;
            }
            if (((player.Accessory != null) && (player.Accessory.Name == Database.COMMON_ROYAL_GUARD_RING)) ||
                ((player.Accessory2 != null) && (player.Accessory2.Name == Database.COMMON_ROYAL_GUARD_RING)))
            {
                UpdateBattleText(player.Name + "にかけられた物理防御DOWN効果は無効化された！\r\n");
                return;
            }

            UpdateBattleText(player.Name + "は【物理防御】が" + effectValue.ToString() + "減少\r\n");
            this.Invoke(new _AnimationDamage(AnimationDamage), 0, player, 0, Color.Black, false, false, "物理防御DOWN");
            player.CurrentPhysicalDefenseDown = turn;
            player.CurrentPhysicalDefenseDownValue = (int)effectValue;
            player.ActivateBuff(player.pbPhysicalDefenseDown, Database.BaseResourceFolder + Database.BUFF_PHYSICAL_DEFENSE_DOWN, turn);
        }

        /// <summary>
        /// 魔法防御上昇BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffUpMagicDefense(MainCharacter player, double effectValue, int turn = 999)
        {
            UpdateBattleText(player.Name + "は【魔法防御】が" + effectValue.ToString() + "上昇\r\n");
            this.Invoke(new _AnimationDamage(AnimationDamage), 0, player, 0, Color.Black, false, false, "魔法防御UP");
            player.CurrentMagicDefenseUp = turn;
            player.CurrentMagicDefenseUpValue = (int)effectValue;
            player.ActivateBuff(player.pbMagicDefenseUp, Database.BaseResourceFolder + Database.BUFF_MAGIC_DEFENSE_UP, turn);
        }

        /// <summary>
        /// 魔法防御減少BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffDownMagicDefense(MainCharacter player, double effectValue, int turn = 999)
        {
            if (player.CheckResistMagicDefenseDown)
            {
                UpdateBattleText(player.Name + "は、魔法防御DOWN効果を受けなかった！\r\n");
                return;
            }
            if (((player.Accessory != null) && (player.Accessory.Name == Database.COMMON_ELEMENTAL_GUARD_RING)) ||
                ((player.Accessory2 != null) && (player.Accessory2.Name == Database.COMMON_ELEMENTAL_GUARD_RING)))
            {
                UpdateBattleText(player.Name + "にかけられた魔法防御DOWN効果は無効化された！\r\n");
                return;
            }

            UpdateBattleText(player.Name + "は【魔法防御】が" + effectValue.ToString() + "減少\r\n");
            this.Invoke(new _AnimationDamage(AnimationDamage), 0, player, 0, Color.Black, false, false, "魔法防御DOWN");
            player.CurrentMagicDefenseDown = turn;
            player.CurrentMagicDefenseDownValue = (int)effectValue;
            player.ActivateBuff(player.pbMagicDefenseDown, Database.BaseResourceFolder + Database.BUFF_MAGIC_DEFENSE_DOWN, turn);
        }

        /// <summary>
        /// 戦闘速度上昇BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffUpBattleSpeed(MainCharacter player, double effectValue, int turn = 999)
        {
            UpdateBattleText(player.Name + "は【戦闘速度】が" + effectValue.ToString() + "上昇\r\n");
            this.Invoke(new _AnimationDamage(AnimationDamage), 0, player, 0, Color.Black, false, false, "戦闘速度UP");
            player.CurrentSpeedUp = turn;
            player.CurrentSpeedUpValue = (int)effectValue;
            player.ActivateBuff(player.pbSpeedUp, Database.BaseResourceFolder + Database.BUFF_SPEED_UP, turn);
        }

        /// <summary>
        /// 戦闘速度減少BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffDownBattleSpeed(MainCharacter player, double effectValue, int turn = 999)
        {
            if (player.CheckResistBattleSpeedDown)
            {
                UpdateBattleText(player.Name + "は、戦闘防御DOWN効果を受けなかった！\r\n");
                return;
            }
            if (((player.Accessory != null) && (player.Accessory.Name == Database.COMMON_HAYATE_GUARD_RING)) ||
                ((player.Accessory2 != null) && (player.Accessory2.Name == Database.COMMON_HAYATE_GUARD_RING)))
            {
                UpdateBattleText(player.Name + "にかけられた戦闘速度DOWN効果は無効化された！\r\n");
                return;
            }

            UpdateBattleText(player.Name + "は【戦闘速度】が" + effectValue.ToString() + "減少\r\n");
            this.Invoke(new _AnimationDamage(AnimationDamage), 0, player, 0, Color.Black, false, false, "戦闘速度DOWN");
            player.CurrentSpeedDown = turn;
            player.CurrentSpeedDownValue = (int)effectValue;
            player.ActivateBuff(player.pbSpeedDown, Database.BaseResourceFolder + Database.BUFF_SPEED_DOWN, turn);
        }

        /// <summary>
        /// 戦闘反応上昇BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffUpBattleReaction(MainCharacter player, double effectValue, int turn = 999)
        {
            UpdateBattleText(player.Name + "は【戦闘反応】が" + effectValue.ToString() + "上昇\r\n");
            this.Invoke(new _AnimationDamage(AnimationDamage), 0, player, 0, Color.Black, false, false, "戦闘反応UP");
            player.CurrentReactionUp = turn;
            player.CurrentReactionUpValue = (int)effectValue;
            player.ActivateBuff(player.pbReactionUp, Database.BaseResourceFolder + Database.BUFF_REACTION_UP, turn);
        }

        /// <summary>
        /// 戦闘反応減少BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffDownBattleReaction(MainCharacter player, double effectValue, int turn = 999)
        {
            if (player.CheckResistBattleResponseDown)
            {
                UpdateBattleText(player.Name + "は、戦闘反応DOWN効果を受けなかった！\r\n");
                return;
            }
            if (((player.Accessory != null) && (player.Accessory.Name == Database.COMMON_HAYATE_GUARD_RING)) ||
                ((player.Accessory2 != null) && (player.Accessory2.Name == Database.COMMON_HAYATE_GUARD_RING)))
            {
                UpdateBattleText(player.Name + "にかけられた戦闘反応DOWN効果は無効化された！\r\n");
                return;
            }

            UpdateBattleText(player.Name + "は【戦闘反応】が" + effectValue.ToString() + "減少\r\n");
            this.Invoke(new _AnimationDamage(AnimationDamage), 0, player, 0, Color.Black, false, false, "戦闘反応DOWN");
            player.CurrentReactionDown = turn;
            player.CurrentReactionDownValue = (int)effectValue;
            player.ActivateBuff(player.pbReactionDown, Database.BaseResourceFolder + Database.BUFF_REACTION_DOWN, turn);
        }

        /// <summary>
        /// 潜在能力上昇BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffUpPotential(MainCharacter player, double effectValue, int turn = 999)
        {
            UpdateBattleText(player.Name + "は【潜在能力】が" + effectValue.ToString() + "上昇\r\n");
            this.Invoke(new _AnimationDamage(AnimationDamage), 0, player, 0, Color.Black, false, false, "潜在能力UP");
            player.CurrentPotentialUp = turn;
            player.CurrentPotentialUpValue = (int)effectValue;
            player.ActivateBuff(player.pbPotentialUp, Database.BaseResourceFolder + Database.BUFF_POTENTIAL_UP, turn);
        }

        /// <summary>
        /// 潜在能力減少BUFF
        /// </summary>
        /// <param name="player">プレイヤー</param>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        void BuffDownPotential(MainCharacter player, double effectValue, int turn = 999)
        {
            if (player.CheckResistPotentialDown)
            {
                UpdateBattleText(player.Name + "は、潜在能力DOWN効果を受けなかった！\r\n");
                return;
            }
            UpdateBattleText(player.Name + "は【潜在能力】が" + effectValue.ToString() + "減少\r\n");
            this.Invoke(new _AnimationDamage(AnimationDamage), 0, player, 0, Color.Black, false, false, "潜在能力DOWN");
            player.CurrentPotentialDown = turn;
            player.CurrentPotentialDownValue = (int)effectValue;
            player.ActivateBuff(player.pbPotentialDown, Database.BaseResourceFolder + Database.BUFF_POTENTIAL_DOWN, turn);
        }
    }
}
