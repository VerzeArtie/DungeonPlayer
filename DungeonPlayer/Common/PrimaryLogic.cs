using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    // 武器・防具・プレイヤーパラメタ・スキル・魔法それらの計算結果値を示すクラス
    public static class PrimaryLogic
    {
        // [必須] 武器最小値と最大値を心パラメタに連動させたのを横展開すること。

        public enum NeedType
        {
            Random,
            Min,
            Max           
        }

        public enum DmgAttr
        {
            Physical,
            Magic,
        }

        public enum SpellSkillType
        {
            Standard,
            PsychicWave,
            WordOfPower,
        }

        /// <summary>
        /// メインウェポンの物理攻撃値の算出
        /// </summary>
        public static double PhysicalAttackValue(MainCharacter player, NeedType type, double pStr, double pAgl, double pInt, double pMind, double pWeapon, PlayerStance stance, SpellSkillType spellSkill, bool duelMode)
        {
            return PhysicalAttackValue(player, type, pStr, pAgl, pInt, pMind, pWeapon, stance, spellSkill, duelMode, false);
        }
        /// <summary>
        /// サブウェポンの物理攻撃値の算出(引数はPhysicalAttackValueと同じだが、PsychicWaveは関係がない)
        /// </summary>
        public static double SubAttackValue(MainCharacter player, NeedType type, double pStr, double pAgl, double pInt, double pMind, double pWeapon, PlayerStance stance, bool duelMode)
        {
            return PhysicalAttackValue(player, type, pStr, pAgl, pInt, pMind, pWeapon, stance, SpellSkillType.Standard, duelMode, true);
        }
        /// <summary>
        /// ウェポンの物理攻撃値の算出
        /// </summary>
        /// <param name="player">発動元プレイヤー</param>
        /// <param name="type">最小、最大、ランダム値を選択</param>
        /// <param name="pStr">力による増強倍率</param>
        /// <param name="pAgl">技による増強倍率</param>
        /// <param name="pInt">知による増強倍率</param>
        /// <param name="pMind">心による増強倍率</param>
        /// <param name="pWeapon">武器による増強倍率</param>
        /// <param name="stance">適用されるスタイルの選定（プレイヤーのスタンスを指すわけではない）</param>
        /// <param name="spellSkill">スペル・スキルの名前</param>
        /// <param name="duelMode">Duel戦闘を示すフラグ</param>
        /// <param name="subWeapon">サブウェポンかどうかを示すフラグ</param>
        /// <returns></returns>
        public static double PhysicalAttackValue(MainCharacter player, NeedType type, double pStr, double pAgl, double pInt, double pMind, double pWeapon, PlayerStance stance, SpellSkillType spellSkill, bool duelMode, bool subWeapon)
        {
            return AttackValue(player, type, DmgAttr.Physical, pStr, pAgl, pInt, pMind, pWeapon, stance, spellSkill, false, duelMode, subWeapon);
        }

        /// <summary>
        /// 魔法攻撃値の算出
        /// </summary>
        /// <param name="player">対象元プレイヤー</param>
        /// <param name="type">最小、最大、ランダム値を選択</param>
        /// <param name="pInt">知による増強倍率</param>
        /// <param name="PlayerStance">適用されるスタイルの選定（プレイヤーのスタンスを指すわけではない）</param>
        /// <param name="WordOfPower">ワード・オブ・パワーの場合True</param>
        /// <param name="ignoreChargeCount">ためるコマンドが対象外の場合True</param>
        /// <returns></returns>
        public static double MagicAttackValue(MainCharacter player, NeedType type, double pInt, double pMind, PlayerStance stance, SpellSkillType spellSkill, bool ignoreChargeCount, bool duelMode)
        {
            double pStr = 0;
            double pAgl = 0;
            double pWeapon = 0;
            bool subWeapon = false;    
            return AttackValue(player, type, DmgAttr.Magic, pStr, pAgl, pInt, pMind, pWeapon, stance, spellSkill, ignoreChargeCount, duelMode, subWeapon);
        }

        public static double AttackValue(MainCharacter player, NeedType type, DmgAttr attr, double pStr, double pAgl, double pInt, double pMind, double pWeapon, PlayerStance stance, SpellSkillType spellSkill, bool ignoreChargeCount, bool duelMode, bool subWeapon)
        {
            double min = 0;
            double max = 0;
            double minFactor = 0.0f;
            double maxFactor = 0.0f;

            // パラメタによるベース値
            if (player.CurrentSeventhMagic > 0)
            {
                if (attr == DmgAttr.Physical)
                {
                    if (spellSkill == SpellSkillType.PsychicWave)
                    {
                        // WordOfPowerで反転、SeventhMagicで更に反転のため、(力)
                        minFactor = maxFactor = player.TotalStrength * pStr + player.TotalAgility * pAgl + player.TotalIntelligence * pInt + player.TotalMind * pMind;
                    }
                    else
                    {
                        // SeventhMagicで反転。(知)
                        minFactor = maxFactor = player.TotalIntelligence * pStr + player.TotalAgility * pAgl + player.TotalStrength * pInt + player.TotalMind * pMind;
                    }
                }
                else // if (attr == DmgAttr.Magic)
                {
                    if (spellSkill == SpellSkillType.WordOfPower)
                    {
                        // WordOfPowerで反転、SeventhMagicで更に反転のため、(知)
                        minFactor = maxFactor = player.TotalIntelligence * pInt + player.TotalMind * pMind;
                    }
                    else
                    {
                        // SeventMagicで反転。(力)
                        minFactor = maxFactor = player.TotalStrength * pInt + player.TotalMind * pMind;
                    }
                }
            }
            else
            {
                if (attr == DmgAttr.Physical)
                {
                    if (spellSkill == SpellSkillType.PsychicWave)
                    {
                        // PsychicWaveは反転。(知)
                        minFactor = maxFactor = player.TotalIntelligence * pStr + player.TotalAgility * pAgl + player.TotalStrength * pInt + player.TotalMind * pMind;
                    }
                    else
                    {
                        // 何もかかってないため、(力)
                        minFactor = maxFactor = player.TotalStrength * pStr + player.TotalAgility * pAgl + player.TotalIntelligence * pInt + player.TotalMind * pMind;
                    }
                }
                else // if (attr == DmgAttr.Magic)
                {
                    if (spellSkill == SpellSkillType.WordOfPower)
                    {
                        // WordOfPowerは反転。(力)
                        minFactor = maxFactor = player.TotalStrength * pInt + player.TotalMind * pMind;
                    }
                    else
                    {
                        // 何もかかってないため、(知)
                        minFactor = maxFactor = player.TotalIntelligence * pInt + player.TotalMind * pMind;
                    }
                }
            }

            // 心係数はここで増幅させる。
            //min = minFactor * (Math.Log(Convert.ToInt32(player.TotalMind), Math.Exp(1)) + 1.00) * 1.00;
            //max = maxFactor * (Math.Log(Convert.ToInt32(player.TotalMind), Math.Exp(1)) + 1.00) * 1.00;
            min = minFactor * ((Math.Log(Database.MAX_PARAMETER, Math.Exp(1)) - Math.Log((Database.MAX_PARAMETER - Convert.ToInt32(player.TotalMind)), Math.Exp(1))) * 10 + 1.00) * 1.00;
            max = maxFactor * ((Math.Log(Database.MAX_PARAMETER, Math.Exp(1)) - Math.Log((Database.MAX_PARAMETER - Convert.ToInt32(player.TotalMind)), Math.Exp(1))) * 10 + 1.00) * 1.00;

            if (attr == DmgAttr.Physical)
            {
                // 武器による加算値（メイン）
                if (subWeapon == false)
                {
                    if (player.MainWeapon != null)
                    {
                        min += player.MainWeapon.MinValue * pWeapon;
                        max += player.MainWeapon.MaxValue * pWeapon;
                    }
                    // アクセサリの中に物理攻撃を増加させるものがある。それらはこちらに加算する。
                    if (player.Accessory != null)
                    {
                        min += player.Accessory.MinValue;
                        max += player.Accessory.MaxValue;
                    }
                    if (player.Accessory2 != null)
                    {
                        min += player.Accessory2.MinValue;
                        max += player.Accessory2.MaxValue;
                    }
                }
                // 武器による加算値（サブ）
                else
                {
                    if (player.SubWeapon != null)
                    {
                        if ((player.SubWeapon.Type == ItemBackPack.ItemType.Weapon_Heavy) ||
                            (player.SubWeapon.Type == ItemBackPack.ItemType.Weapon_Light) ||
                            (player.SubWeapon.Type == ItemBackPack.ItemType.Weapon_Middle))
                        {
                            min += player.SubWeapon.MinValue * pWeapon;
                            max += player.SubWeapon.MaxValue * pWeapon;
                        }
                        else
                        {
                            return 0; // サブ武器が武器でない場合は０とみなす。
                        }
                    }
                }
            }
            else // if (attr == DmgAttr.Magic)
            {
                // (武具)1.0
                if (player.MainWeapon != null)
                {
                    min += player.MainWeapon.MagicMinValue;
                    max += player.MainWeapon.MagicMaxValue;
                }
                if (player.Accessory != null)
                {
                    min += player.Accessory.MagicMinValue;
                    max += player.Accessory.MagicMaxValue;
                }
                if (player.Accessory2 != null)
                {
                    min += player.Accessory2.MagicMinValue;
                    max += player.Accessory2.MagicMaxValue;
                }
            }
            
            double result = 0.0f;
            switch (type)
            {
                case NeedType.Random:
                    double sigma = 0.0f;
                    double mu = 0.0f;
                    // sigma
                    if (player.TotalMind <= ((max + min) / 2.0F)) { sigma = (player.TotalMind / 3.5F); }
                    else { sigma = (((max + min) / 2.0F) / 3.5F); }
                    // mu
                    if (player.TotalMind <= min) mu = min;
                    else if (player.TotalMind >= max) mu = max;
                    else { mu = player.TotalMind; }
                    // 『標準正規累積分布に対する逆関数』により算出される
                    result = (Statistics.Distributions.NormInv(AP.Math.RandomReal(), mu, sigma));
                    if (result <= min) result = min;
                    if (result >= max) result = max;
                    break;
                case NeedType.Min:
                    result = min;
                    break;
                case NeedType.Max:
                    result = max;
                    break;
            }

            // 武器、防具、アクセサリからの増強
            if (attr == DmgAttr.Physical)
            {
                if ((player.MainWeapon != null) && (player.MainWeapon.AmplifyPhysicalAttack != 0.0f)) result = result * player.MainWeapon.AmplifyPhysicalAttack;
                if ((player.SubWeapon != null) && (player.SubWeapon.AmplifyPhysicalAttack != 0.0f)) result = result * player.SubWeapon.AmplifyPhysicalAttack;
                if ((player.MainArmor != null) && (player.MainArmor.AmplifyPhysicalAttack != 0.0f)) result = result * player.MainArmor.AmplifyPhysicalAttack;
                if ((player.Accessory != null) && (player.Accessory.AmplifyPhysicalAttack != 0.0f)) result = result * player.Accessory.AmplifyPhysicalAttack;
                if ((player.Accessory2 != null) && (player.Accessory2.AmplifyPhysicalAttack != 0.0f)) result = result * player.Accessory2.AmplifyPhysicalAttack;
            }
            else // if (attr == DmgAttr.Magic)
            {
                if ((player.MainWeapon != null) && (player.MainWeapon.AmplifyMagicAttack != 0.0f)) result = result * player.MainWeapon.AmplifyMagicAttack;
                if ((player.SubWeapon != null) && (player.SubWeapon.AmplifyMagicAttack != 0.0f)) result = result * player.SubWeapon.AmplifyMagicAttack;
                if ((player.MainArmor != null) && (player.MainArmor.AmplifyMagicAttack != 0.0f)) result = result * player.MainArmor.AmplifyMagicAttack;
                if ((player.Accessory != null) && (player.Accessory.AmplifyMagicAttack != 0.0f)) result = result * player.Accessory.AmplifyMagicAttack;
                if ((player.Accessory2 != null) && (player.Accessory2.AmplifyMagicAttack != 0.0f)) result = result * player.Accessory2.AmplifyMagicAttack;
            }

            if (attr == DmgAttr.Physical)
            {
                if (player.CurrentPhysicalChargeCount > 0)
                {
                    result = result * (double)(player.CurrentPhysicalChargeCount + 1.0F);
                }

                if (stance == PlayerStance.FrontOffence && player.Stance == PlayerStance.FrontOffence)
                {
                    result = result * 1.05f;
                }
                if (player.AmplifyPhysicalAttack > 0.0f)
                {
                    result = result * player.AmplifyPhysicalAttack;
                }
            }
            else // if (attr == DmgAttr.Magic)
            {
                if (!ignoreChargeCount && (player.CurrentChargeCount > 0))
                {
                    result = result * (double)(player.CurrentChargeCount + 1.0F);
                }
                if (stance == PlayerStance.BackSupport && player.Stance == PlayerStance.BackSupport)
                {
                    result = result * 1.05f;
                }
                if ((stance == PlayerStance.FrontOffence || stance == PlayerStance.BackOffence) &&
                    (player.Stance == PlayerStance.FrontOffence || player.Stance == PlayerStance.BackOffence))
                {
                    result = result * 1.05f;
                }
                if (player.AmplifyMagicAttack > 0.0f) // 「警告」魔法攻撃増強で回復も増強するのか？
                {
                    result = result * player.AmplifyMagicAttack;
                }
            }

            if (player.CurrentSyutyu_Danzetsu > 0)
            {
                result = result * PrimaryLogic.SyutyuDanzetsuValue(player);
            }
            if (player.CurrentOnslaughtHit > 0)
            {
                result = result * (1.00f - 0.15f * player.CurrentOnslaughtHitValue);
            }
            if (duelMode)
            {
                result = result * 0.5f;
            }

            // ＋ーバッファは潜在係数や増幅係数を考慮せず、単純増加とする。
            if (attr == DmgAttr.Physical)
            {
                result += player.CurrentPhysicalAttackUpValue;
                result -= player.CurrentPhysicalAttackDownValue;
            }
            else // if (attr == DmgAttr.Magic)
            {
                result += player.CurrentMagicAttackUpValue;
                result -= player.CurrentMagicAttackDownValue;
            }

            if (result <= 0) result = 0;
            return result;
        }

        /// <summary>
        /// 物理防御値の算出
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static double PhysicalDefenseValue(MainCharacter player, NeedType type, bool duelMode)
        {
            double min = 0;
            double max = 0;

            // 力0.20 + 技0.05 + 心0.10
            //min = (player.TotalStrength) * (Math.Log(Convert.ToInt32(player.TotalMind), Math.Exp(1)) + 1.00) * 0.20
            //     + (player.TotalAgility) * (Math.Log(Convert.ToInt32(player.TotalMind), Math.Exp(1)) + 1.00) * 0.05
            //     + (player.TotalMind) * 0.10; // 心パラメタは潜在能力以外はLog乗算しない

            //max = (player.TotalStrength) * (Math.Log(Convert.ToInt32(player.TotalMind), Math.Exp(1)) + 1.00) * 0.20
            //     + (player.TotalAgility) * (Math.Log(Convert.ToInt32(player.TotalMind), Math.Exp(1)) + 1.00) * 0.05
            //     + (player.TotalMind) * 0.10;
            min = (player.TotalStrength) * ((Math.Log(Database.MAX_PARAMETER, Math.Exp(1)) - Math.Log((Database.MAX_PARAMETER - Convert.ToInt32(player.TotalMind)), Math.Exp(1))) * 10 + 1.00) * 0.20
                 + (player.TotalAgility) * ((Math.Log(Database.MAX_PARAMETER, Math.Exp(1)) - Math.Log((Database.MAX_PARAMETER - Convert.ToInt32(player.TotalMind)), Math.Exp(1))) * 10 + 1.00) * 0.05
                 + (player.TotalMind) * 0.10; // 心パラメタは潜在能力以外はLog乗算しない

            max = (player.TotalStrength) * ((Math.Log(Database.MAX_PARAMETER, Math.Exp(1)) - Math.Log((Database.MAX_PARAMETER - Convert.ToInt32(player.TotalMind)), Math.Exp(1))) * 10 + 1.00) * 0.20
                 + (player.TotalAgility) * ((Math.Log(Database.MAX_PARAMETER, Math.Exp(1)) - Math.Log((Database.MAX_PARAMETER - Convert.ToInt32(player.TotalMind)), Math.Exp(1))) * 10 + 1.00) * 0.05
                 + (player.TotalMind) * 0.10;

            // 鎧0.2
            if (player.MainArmor != null)
            {
                min += player.MainArmor.MinValue; // * 0.20; // 防具の能力が低減されているのはプレイヤーにとって不満である。
                max += player.MainArmor.MaxValue; // * 0.20; // 防具の能力が低減されているのはプレイヤーにとって不満である。
            }


            // 盾0.2
            if (player.SubWeapon != null)
            {
                if (player.SubWeapon.Type == ItemBackPack.ItemType.Shield)
                {
                    min += player.SubWeapon.MinValue; // * 0.20; // 防具の能力が低減されているのはプレイヤーにとって不満である。
                    max += player.SubWeapon.MaxValue; // * 0.20; // 防具の能力が低減されているのはプレイヤーにとって不満である。
                }
            }

            double result = 0.0f;
            switch (type)
            {
                case NeedType.Random:
                    double sigma = 0.0f;
                    double mu = 0.0f;
                    // sigma
                    if (player.TotalMind <= ((max + min) / 2.0F)) { sigma = (player.TotalMind / 3.5F); }
                    else { sigma = (((max + min) / 2.0F) / 3.5F); }
                    // mu
                    if (player.TotalMind <= min) mu = min;
                    else if (player.TotalMind >= max) mu = max;
                    else { mu = player.TotalMind; }
                    // 『標準正規累積分布に対する逆関数』により算出される
                    result = (Statistics.Distributions.NormInv(AP.Math.RandomReal(), mu, sigma));
                    if (result <= min) result = min;
                    if (result >= max) result = max;
                    break;
                case NeedType.Min:
                    result = min;
                    break;
                case NeedType.Max:
                    result = max;
                    break;
            }

            // 武器、防具、アクセサリからの増強
            if ((player.MainWeapon != null) && (player.MainWeapon.AmplifyPhysicalDefense != 0.0f)) result = result * player.MainWeapon.AmplifyPhysicalDefense;
            if ((player.SubWeapon != null) && (player.SubWeapon.AmplifyPhysicalDefense != 0.0f)) result = result * player.SubWeapon.AmplifyPhysicalDefense;
            if ((player.MainArmor != null) && (player.MainArmor.AmplifyPhysicalDefense != 0.0f)) result = result * player.MainArmor.AmplifyPhysicalDefense;
            if ((player.Accessory != null) && (player.Accessory.AmplifyPhysicalDefense != 0.0f)) result = result * player.Accessory.AmplifyPhysicalDefense;
            if ((player.Accessory2 != null) && (player.Accessory2.AmplifyPhysicalDefense != 0.0f)) result = result * player.Accessory2.AmplifyPhysicalDefense;

            if (player.CurrentBlindJustice > 0)
            {
                result = result * 0.70f;
            }
            if (player.CurrentImmolate > 0)
            {
                result = result * 0.80f;
            }
            if (player.CurrentDarkenField > 0)
            {
                result = result * 0.80f;
            }
            if (player.CurrentConcussiveHit > 0)
            {
                result = result * (1.00f - 0.15f * player.CurrentConcussiveHitValue);
            }

            if (player.Stance == PlayerStance.FrontDefense)
            {
                result = result * 1.05f;
            }
            if (player.AmplifyPhysicalDefense > 0.0f)
            {
                result = result * player.AmplifyPhysicalDefense;
            }

            if (duelMode)
            {
                result = result * 0.5f;
            }

            // ＋ーバッファは潜在係数や増幅係数を考慮せず、単純増加とする。
            result += player.CurrentPhysicalDefenseUpValue;
            result -= player.CurrentPhysicalDefenseDownValue;

            if (result <= 0) result = 0;
            return result;
        }

        /// <summary>
        /// 魔法防御値の算出
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static double MagicDefenseValue(MainCharacter player, NeedType type, bool duelMode)
        {
            double min = 0;
            double max = 0;

            // 知0.20 + 力0.05 + 心0.10
            //min = (player.TotalIntelligence) * (Math.Log(Convert.ToInt32(player.TotalMind), Math.Exp(1)) + 1.00) * 0.20
            //    + (player.TotalStrength) * (Math.Log(Convert.ToInt32(player.TotalMind), Math.Exp(1)) + 1.00) * 0.05
            //    + (player.TotalMind) * 0.10;

            //max = (player.TotalIntelligence) * (Math.Log(Convert.ToInt32(player.TotalMind), Math.Exp(1)) + 1.00) * 0.20
            //    + (player.TotalStrength) * (Math.Log(Convert.ToInt32(player.TotalMind), Math.Exp(1)) + 1.00) * 0.05
            //    + (player.TotalMind) * 0.10;
            min = (player.TotalIntelligence) * ((Math.Log(Database.MAX_PARAMETER, Math.Exp(1)) - Math.Log((Database.MAX_PARAMETER - Convert.ToInt32(player.TotalMind)), Math.Exp(1))) * 10 + 1.00) * 0.20
                + (player.TotalStrength) * ((Math.Log(Database.MAX_PARAMETER, Math.Exp(1)) - Math.Log((Database.MAX_PARAMETER - Convert.ToInt32(player.TotalMind)), Math.Exp(1))) * 10 + 1.00) * 0.05
                + (player.TotalMind) * 0.10;

            max = (player.TotalIntelligence) * ((Math.Log(Database.MAX_PARAMETER, Math.Exp(1)) - Math.Log((Database.MAX_PARAMETER - Convert.ToInt32(player.TotalMind)), Math.Exp(1))) * 10 + 1.00) * 0.20
                + (player.TotalStrength) * ((Math.Log(Database.MAX_PARAMETER, Math.Exp(1)) - Math.Log((Database.MAX_PARAMETER - Convert.ToInt32(player.TotalMind)), Math.Exp(1))) * 10 + 1.00) * 0.05
                + (player.TotalMind) * 0.10;

            // 鎧0.2
            if (player.MainArmor != null)
            {
                min += player.MainArmor.MagicMinValue; // * 0.20; // 防具の能力が低減されているのはプレイヤーにとって不服である。
                max += player.MainArmor.MagicMaxValue; // * 0.20; // 防具の能力が低減されているのはプレイヤーにとって不服である。
            }

            // 盾0.2
            if (player.SubWeapon != null)
            {
                if (player.SubWeapon.Type == ItemBackPack.ItemType.Shield)
                {
                    min += player.SubWeapon.MagicMinValue; // * 0.20; // 防具の能力が低減されているのはプレイヤーにとって不服である。
                    max += player.SubWeapon.MagicMaxValue; // * 0.20; // 防具の能力が低減されているのはプレイヤーにとって不服である。
                }
            }

            double result = 0.0f;
            switch (type)
            {
                case NeedType.Random:
                    double sigma = 0.0f;
                    double mu = 0.0f;
                    // sigma
                    if (player.TotalMind <= ((max + min) / 2.0F)) { sigma = (player.TotalMind / 3.5F); }
                    else { sigma = (((max + min) / 2.0F) / 3.5F); }
                    // mu
                    if (player.TotalMind <= min) mu = min;
                    else if (player.TotalMind >= max) mu = max;
                    else { mu = player.TotalMind; }
                    // 『標準正規累積分布に対する逆関数』により算出される
                    result = (Statistics.Distributions.NormInv(AP.Math.RandomReal(), mu, sigma));
                    if (result <= min) result = min;
                    if (result >= max) result = max;
                    break;
                case NeedType.Min:
                    result = min;
                    break;
                case NeedType.Max:
                    result = max;
                    break;
            }

            // 武器、防具、アクセサリからの増強
            if ((player.MainWeapon != null) && (player.MainWeapon.AmplifyMagicDefense != 0.0f)) result = result * player.MainWeapon.AmplifyMagicDefense;
            if ((player.SubWeapon != null) && (player.SubWeapon.AmplifyMagicDefense != 0.0f)) result = result * player.SubWeapon.AmplifyMagicDefense;
            if ((player.MainArmor != null) && (player.MainArmor.AmplifyMagicDefense != 0.0f)) result = result * player.MainArmor.AmplifyMagicDefense;
            if ((player.Accessory != null) && (player.Accessory.AmplifyMagicDefense != 0.0f)) result = result * player.Accessory.AmplifyMagicDefense;
            if ((player.Accessory2 != null) && (player.Accessory2.AmplifyMagicDefense != 0.0f)) result = result * player.Accessory2.AmplifyMagicDefense;

            if (player.CurrentDarkenField > 0)
            {
                result = result * 0.80f;
            }
            if (player.CurrentBlackFire > 0)
            {
                result = result * 0.80f;
            }
            if (player.CurrentPsychicTrance > 0)
            {
                result = result * 0.70f;
            }
            if (player.CurrentConcussiveHit > 0)
            {
                result = result * (1.00f - 0.15f * player.CurrentConcussiveHitValue);
            }

            if (player.Stance == PlayerStance.FrontDefense || player.Stance == PlayerStance.BackSupport)
            {
                result = result * 1.05f;
            }
            if (player.AmplifyMagicDefense > 0.0f)
            {
                result = result * player.AmplifyMagicDefense;
            }

            if (duelMode)
            {
                result = result * 0.5f;
            }

            // ＋ーバッファは潜在係数や増幅係数を考慮せず、単純増加とする。
            result += player.CurrentMagicDefenseUpValue;
            result -= player.CurrentMagicDefenseDownValue;

            if (result <= 0) result = 0;
            return result;
        }

        /// <summary>
        /// 戦闘速度値の算出
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static double BattleSpeedValue(MainCharacter player, bool duelMode)
        {
            // 最大速度が速すぎるため以下のように調整。
            // 最小が1.00、最大が7.00となるようにする。

            // 技1.0
            //double result = (player.TotalAgility) * (Math.Log(Convert.ToInt32(player.TotalMind), Math.Exp(1)) + 1.00) * 1.00;
            // double result = (player.TotalAgility) * ((Math.Log(Database.MAX_PARAMETER, Math.Exp(1)) - Math.Log((Database.MAX_PARAMETER - Convert.ToInt32(player.TotalMind)), Math.Exp(1))) * 10 + 1.00) * 1.00;
            // 指数関数的増大は戦闘速度が無限に膨れ上がってしまっては困る原因の元を作ってしまった。ここだけは線形増加に留める。
            double result = (double)(player.TotalAgility);

            // 技      [ 1 - 100 ] -->   1 + 技 / 100 * 1.0
            //      [ 101 - 1000 ] -->   2 + 技 / 1000 * 2.0
            //    [ 1001 - 10000 ] -->   4 + 技 / 9999 * 3.0
            if (0 <= player.TotalAgility && player.TotalAgility <= 100)
            {
                //double MAX_VALUE = 110.050335853503;
                result = 1.0F + result / 100.0F * 1.0F;
            }
            else if (101 <= player.TotalAgility && player.TotalAgility <= 1000)
            {
                //double MAX_VALUE = 2053.60515657827000000;
                result = 2.0F + (result - 100) / 900.0F * 2.0F;
            }
            else if (1001 <= player.TotalAgility && player.TotalAgility <= 9999)
            {
                //double MAX_VALUE = 930940.933793899;
                result = 4.0F + (result - 1000) / 9000.0F * 3.0F;
            }

            // 心係数はここでは、線形増強
            result = result * (1.00f + (double)(player.TotalMind) / 9999.0f * 1.00f);

            // 武器、防具、アクセサリからの増強
            if ((player.MainWeapon != null) && (player.MainWeapon.AmplifyBattleSpeed != 0.0f)) result = result * player.MainWeapon.AmplifyBattleSpeed;
            if ((player.SubWeapon != null) && (player.SubWeapon.AmplifyBattleSpeed != 0.0f)) result = result * player.SubWeapon.AmplifyBattleSpeed;
            if ((player.MainArmor != null) && (player.MainArmor.AmplifyBattleSpeed != 0.0f)) result = result * player.MainArmor.AmplifyBattleSpeed;
            if ((player.Accessory != null) && (player.Accessory.AmplifyBattleSpeed != 0.0f)) result = result * player.Accessory.AmplifyBattleSpeed;
            if ((player.Accessory2 != null) && (player.Accessory2.AmplifyBattleSpeed != 0.0f)) result = result * player.Accessory2.AmplifyBattleSpeed;

            if (player.CurrentImpulseHit > 0)
            {
                result = result * (1.00f - 0.15f * player.CurrentImpulseHitValue);
            }

            if (player.AmplifyBattleSpeed > 0.0f)
            {
                result = result * player.AmplifyBattleSpeed;
            }
            
            if (duelMode)
            {
                result = result * 1.0f;
            }

　          // ＋ーバッファは潜在係数や増幅係数を考慮せず、単純増加とする。
            result += player.CurrentSpeedUpValue;
            result -= player.CurrentSpeedDownValue;

            if (result <= 0) result = 0;

            //result += 1.0f; // 万が一０の場合進まなくなるため、1.0を追加
            if (result < 1.0f) { result = 1.0f; } // 万が一、1よりも小さい場合、進みが遅すぎるため、1.0を追加
            return result;
        }

        /// <summary>
        /// 戦闘反応値の算出
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static double BattleResponseValue(MainCharacter player, bool duelMode)
        {
            // 最大速度が速すぎるため以下のように調整。
            // 最小が1.00、最大が31.00となるようにする。

            // 技0.20 + 知0.05
            //double result = (player.TotalAgility) * (Math.Log(Convert.ToInt32(player.TotalMind), Math.Exp(1)) + 1.00) * 0.20
            //         + (player.TotalIntelligence) * (Math.Log(Convert.ToInt32(player.TotalMind), Math.Exp(1)) + 1.00) * 0.05
            double result = (player.TotalAgility) * ((Math.Log(Database.MAX_PARAMETER, Math.Exp(1)) - Math.Log((Database.MAX_PARAMETER - Convert.ToInt32(player.TotalMind)), Math.Exp(1))) * 10 + 1.00) * 0.20
                     + (player.TotalIntelligence) * ((Math.Log(Database.MAX_PARAMETER, Math.Exp(1)) - Math.Log((Database.MAX_PARAMETER - Convert.ToInt32(player.TotalMind)), Math.Exp(1))) * 10 + 1.00) * 0.05;

            // 技      [ 1 - 100 ] -->   1 + 技 / 100 * 5.0
            //      [ 101 - 1000 ] -->   6 + 技 / 1000 * 10.0
            //    [ 1001 - 10000 ] -->   16 + 技 / 9999 * 15.0
            //技9999,知9999,心9999最大
            if (0 <= player.TotalAgility && player.TotalAgility <= 100)
            {
                double MAX_VALUE = 33.01510075605090000;
                result = 1.0F + result / MAX_VALUE * 5.0F;
            }
            else if (101 <= player.TotalAgility && player.TotalAgility <= 1000)
            {
                double MAX_VALUE = 616.08154697348000000;
                result = 6.0F + result / MAX_VALUE * 10.0F;
            }
            else if (1001 <= player.TotalAgility && player.TotalAgility <= 9999)
            {
                double MAX_VALUE = 279282.28013817000000000;
                result = 16.0F + result / MAX_VALUE * 15.0F;
            }

            // 武器、防具、アクセサリからの増強
            if ((player.MainWeapon != null) && (player.MainWeapon.AmplifyBattleResponse != 0.0f)) result = result * player.MainWeapon.AmplifyBattleResponse;
            if ((player.SubWeapon != null) && (player.SubWeapon.AmplifyBattleResponse != 0.0f)) result = result * player.SubWeapon.AmplifyBattleResponse;
            if ((player.MainArmor != null) && (player.MainArmor.AmplifyBattleResponse != 0.0f)) result = result * player.MainArmor.AmplifyBattleResponse;
            if ((player.Accessory != null) && (player.Accessory.AmplifyBattleResponse != 0.0f)) result = result * player.Accessory.AmplifyBattleResponse;
            if ((player.Accessory2 != null) && (player.Accessory2.AmplifyBattleResponse != 0.0f)) result = result * player.Accessory2.AmplifyBattleResponse;

            if (player.CurrentPhantasmalWind > 0)
            {
                result = result * 1.2f;
            }
            if (player.CurrentVigorSense > 0)
            {
                result = result * 1.4f;
            }
            if (player.CurrentWordOfMalice > 0)
            {
                result = result * 0.7f;
            }

            if (player.CurrentImpulseHit > 0)
            {
                result = result * (1.00f - 0.15f * player.CurrentImpulseHitValue);
            }

            if (player.AmplifyBattleResponse > 0.0f)
            {
                result = result * player.AmplifyBattleResponse;
            }

            if (duelMode)
            {
                result = result * 1.0f;
            }

            // ＋ーバッファは潜在係数や増幅係数を考慮せず、単純増加とする。
            result += player.CurrentReactionUpValue;
            result -= player.CurrentReactionDownValue;

            if (result <= 0) result = 0;
            return result;
        }

        /// <summary>
        /// 潜在能力値の算出
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static double PotentialValue(MainCharacter player, bool duelMode)
        {
            // 最大値が大きすぎるため以下の様に調整
            // 最小が1.00、最大が4.00となるようにする。

            // 心1.00
            //double result = (player.TotalMind) * (Math.Log(Convert.ToInt32(player.TotalMind), Math.Exp(1)) + 1.00) * 1.00;
            //double result = (player.TotalMind) * ((Math.Log(Database.MAX_PARAMETER, Math.Exp(1)) - Math.Log((Database.MAX_PARAMETER - Convert.ToInt32(player.TotalMind)), Math.Exp(1))) * 10 + 1.00) * 1.00;
            // 指数関数的増大は戦闘速度が無限に膨れ上がってしまっては困る原因の元を作ってしまった。ここだけは線形増加に留める。
            double result = (double)(player.TotalMind);

            // 心      [ 1 - 100 ] -->   1.0 + 心 /  100 * 0.5
            //      [ 101 - 1000 ] -->   1.5 + 心 / 1000 * 1.0
            //    [ 1001 - 10000 ] -->   2.5 + 心 / 9999 * 1.5
            if (0 <= player.TotalMind && player.TotalMind <= 100)
            {
                result = 1.0F + result / 100.0F * 0.5F;
            }
            else if (101 <= player.TotalMind && player.TotalMind <= 1000)
            {
                result = 1.5F + (result - 100) / 900.0F * 1.0F;
            }
            else if (1001 <= player.TotalMind && player.TotalMind <= 9999)
            {
                result = 2.5F + (result - 1000) / 9000.0F * 1.5F;
            }           

            // 武器、防具、アクセサリからの増強
            if ((player.MainWeapon != null) && (player.MainWeapon.AmplifyPotential != 0.0f)) result = result * player.MainWeapon.AmplifyPotential;
            if ((player.SubWeapon != null) && (player.SubWeapon.AmplifyPotential != 0.0f)) result = result * player.SubWeapon.AmplifyPotential;
            if ((player.MainArmor != null) && (player.MainArmor.AmplifyPotential != 0.0f)) result = result * player.MainArmor.AmplifyPotential;
            if ((player.Accessory != null) && (player.Accessory.AmplifyPotential != 0.0f)) result = result * player.Accessory.AmplifyPotential;
            if ((player.Accessory2 != null) && (player.Accessory2.AmplifyPotential != 0.0f)) result = result * player.Accessory2.AmplifyPotential;

            if (player.CurrentParadoxImage > 0)
            {
                result = result * 1.2f;
            }

            if (player.AmplifyPotential > 0.0f)
            {
                result = result * player.AmplifyPotential;
            }

            if (duelMode)
            {
                result = result * 1.0f;
            }

            // ＋ーバッファは潜在係数や増幅係数を考慮せず、単純増加とする。
            result += player.CurrentPotentialUpValue;
            result -= player.CurrentPotentialDownValue;

            if (result <= 0) result = 0;
            return result;
        }


        /// <summary>
        /// クリティカル発動率の算出（前編ではパラメタＭＡＸが１００前後、後編では９９９９ＭＡＸを意識した値に調整）
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static bool CriticalDetect(MainCharacter player)
        {
            double max = 0;

            // 技      [ 1 - 100 ] -->   0 + 技 / 10
            //      [ 101 - 1000 ] -->  10 + 技 / 45
            //    [ 1001 - 10000 ] -->  30 + 技 / 450
            if (0 <= player.TotalAgility && player.TotalAgility <= 100)
            {
                max = 0.0F + (double)player.TotalAgility / 10.0F;
            }
            else if (101 <= player.TotalAgility && player.TotalAgility <= 1000)
            {
                max = 10.0F + (double)(player.TotalAgility - 100.0F) / 45.0F;
            }
            else if (1001 <= player.TotalAgility && player.TotalAgility <= 9999)
            {
                max = 40.0F + (double)(player.TotalAgility - 1000.0F) / 450.0F;
            }

            Random rd = new Random(DateTime.Now.Millisecond);
            int result = AP.Math.RandomInteger(100);
            if (player.CurrentWordOfFortune >= 1) // 前編では==1だが、後編では違和感が出るので、>= 1
            {
                result = 0;
            }
            if (result < max)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static double CriticalDamageValue(MainCharacter player, bool duelMode)
        {
            if (duelMode)
            {
                return 1.5F;
            }
            else
            {
                return 2.5F;
            }
        }

        //   元核！！！  //////////////////////////////////////////////////////////////////////////////////////////////////////
        public static double SyutyuDanzetsuValue(MainCharacter player)
        {
            double result = 0;

            // 心      [ 1 -   100 ] --> 1.0 ～  1.6 --> 1.0 + 0.6 * 心 /  100
            //       [ 101 -   400 ] --> 1.6 ～  2.8 --> 1.6 + 1.2 * 心 /  300
            //       [ 401 -  1000 ] --> 2.8 ～  4.6 --> 2.8 + 1.8 * 心 /  600
            //       [1001 -  3500 ] --> 4.6 ～  7.0 --> 4.6 + 2.4 * 心 / 2500
            //      [ 1001 - 10000 ] --> 7.0 ～ 10.0 --> 7.0 + 3.0 * 心 / 6500
            if (0 <= player.TotalMind && player.TotalMind <= 100)
            {
                result = 1.0F + 0.6F * (double)(player.TotalMind - 0.0F) / 100.0F;
            }
            else if (101 <= player.TotalMind && player.TotalMind <= 400)
            {
                result = 1.6F + 1.2F * (double)(player.TotalMind - 100.0F) / 300.0F;
            }
            else if (401 <= player.TotalMind && player.TotalMind <= 1000)
            {
                result = 2.8F + 1.8F * (double)(player.TotalMind - 400.0F) / 600.0F;
            }
            else if (1001 <= player.TotalMind && player.TotalMind <= 3500)
            {
                result = 4.6F + 2.4F * (double)(player.TotalMind - 1000.0F) / 2500.0F;
            }
            else if (3501 <= player.TotalMind && player.TotalMind <= 9999)
            {
                result = 7.0F + 3.0F * (double)(player.TotalMind - 3500.0F) / 6500.0F; 
            }

            return result;
        }

        public static double JunkanSeiyakuValue(MainCharacter player)
        {
            double result = 0;

            // 心      [ 1 -   100 ] -->  1.0 ～  3.0 -->  1.0 +  2.0 * 心 /  100
            //       [ 101 -   400 ] -->  3.0 ～  7.0 -->  3.0 +  4.0 * 心 /  300
            //       [ 401 -  1000 ] -->  7.0 ～ 13.0 -->  7.0 +  6.0 * 心 /  600
            //       [1001 -  3500 ] --> 13.0 ～ 21.0 --> 13.0 +  8.0 * 心 / 2500
            //      [ 1001 - 10000 ] --> 21.0 ～ 31.0 --> 21.0 + 10.0 * 心 / 6500
            if (0 <= player.TotalMind && player.TotalMind <= 100)
            {
                result = 1.0F + 2.0F * (double)(player.TotalMind - 0.0F) / 100.0F;
            }
            else if (101 <= player.TotalMind && player.TotalMind <= 400)
            {
                result = 3.0F + 4.0F * (double)(player.TotalMind - 100.0F) / 300.0F;
            }
            else if (401 <= player.TotalMind && player.TotalMind <= 1000)
            {
                result = 7.0F + 6.0F * (double)(player.TotalMind - 400.0F) / 600.0F;
            }
            else if (1001 <= player.TotalMind && player.TotalMind <= 3500)
            {
                result = 13.0F + 8.0F * (double)(player.TotalMind - 1000.0F) / 2500.0F;
            }
            else if (3501 <= player.TotalMind && player.TotalMind <= 9999)
            {
                result = 21.0F + 10.0F * (double)(player.TotalMind - 3500.0F) / 6500.0F;
            }

            return result;
        }

        public static double OraOraOraaaValue(MainCharacter player)
        {
            double result = 0;

            // 心      [ 1 -   100 ] -->   1.0 ～  12.0 -->  1.0 + 11.0 * 心 /  100
            //       [ 101 -   400 ] -->  12.0 ～  34.0 --> 12.0 + 22.0 * 心 /  300
            //       [ 401 -  1000 ] -->  34.0 ～  67.0 --> 34.0 + 33.0 * 心 /  600
            //       [1001 -  3500 ] -->  67.0 ～ 111.0 -->  67.0 + 44.0 * 心 / 2500
            //      [ 1001 - 10000 ] --> 111.0 ～ 166.0 --> 111.0 + 55.0 * 心 / 6500
            if (0 <= player.TotalMind && player.TotalMind <= 100)
            {
                result = 1.0F + 11.0F * (double)(player.TotalMind - 0.0F) / 100.0F;
            }
            else if (101 <= player.TotalMind && player.TotalMind <= 400)
            {
                result = 12.0F + 22.0F * (double)(player.TotalMind - 100.0F) / 300.0F;
            }
            else if (401 <= player.TotalMind && player.TotalMind <= 1000)
            {
                result = 34.0F + 33.0F * (double)(player.TotalMind - 400.0F) / 600.0F;
            }
            else if (1001 <= player.TotalMind && player.TotalMind <= 3500)
            {
                result = 67.0F + 44.0F * (double)(player.TotalMind - 1000.0F) / 2500.0F;
            }
            else if (3501 <= player.TotalMind && player.TotalMind <= 9999)
            {
                result = 111.0F + 55.0F * (double)(player.TotalMind - 3500.0F) / 6500.0F;
            }

            return result;
        }

        //   スキル！！！  //////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// ストレート・スマッシュ値の算出
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static double StraightSmashValue(MainCharacter player, bool duelMode)
        {
            // 力1.0　技2.0-3.0　知0.0  心0.0  武器1.0-2.0
            double pAgl = (double)(2.0F + (AP.Math.RandomInteger(1000) + 1) / 1000.0F);
            double pWeapon = (double)(1.0F + (AP.Math.RandomInteger(1000) + 1) / 1000.0F);
            return ConstructPhysicalDamage(player, 1.0F, pAgl, 0.0F, 0.0F, pWeapon, 0, 1, PlayerStance.FrontOffence, SpellSkillType.Standard, duelMode);
        }

        // ヴァイオレント・スラッシュ値の算出
        public static double ViolentSlashValue(MainCharacter player, bool duelMode)
        {
            // 力2.5  技0.0  知0.0  心0.0  武器1.0
            return ConstructPhysicalDamage(player, 2.5F, 0.0F, 0.0F, 0.0F, 1.0, 0, 1, PlayerStance.FrontOffence, SpellSkillType.Standard, duelMode);
        }

        // サイキック・ウェイブ値の算出
        public static double PsychicWaveValue(MainCharacter player, bool duelMode)
        {
            return ConstructPhysicalDamage(player, 2.0F, 0, 0, 0, 1.0F, 0, 1, PlayerStance.BackOffence, SpellSkillType.PsychicWave, duelMode);
        }
        // マインド・キリング（マナダメージ）値の算出
        public static double MindKillingValue(MainCharacter player, bool duelMode)
        {
            return ConstructPhysicalDamage(player, 1.0f, 0.0f, 0.0f, 0.0f, 1.0f, 100, 200, PlayerStance.FrontOffence, SpellSkillType.Standard, duelMode);
        }

        // ワン・オーソリティ（スキル回復量）値の算出
        public static double OneAuthorityValue(MainCharacter player, bool duelMode)
        {
            double min = 0;
            double max = 0;

            // ベース値＋１５は固定
            // 心      [ 1 - 100 ] -->   0 + 心 / 5
            //      [ 101 - 1000 ] -->  25 + 心 / 25
            //    [ 1001 - 10000 ] -->  61 + 心 / 450
            if (0 <= player.TotalMind && player.TotalMind <= 100)
            {
                min = 0.0F;
                max = 0.0F + (double)player.TotalMind / 4.0F;
            }
            else if (101 <= player.TotalMind && player.TotalMind <= 1000)
            {
                min = 25.0F;
                max = 25.0F + (double)(player.TotalMind - 100.0F) / 30.0F;
            }
            else if (1001 <= player.TotalMind && player.TotalMind <= 9999)
            {
                min = 55.0F;
                max = 55.0F + (double)(player.TotalMind - 1000.0F) / 300.0F;
            }

            Random rd = new Random(DateTime.Now.Millisecond * Environment.TickCount);
            double result = rd.Next((int)min, (int)(max + 1.0F)) + 10.0F;

            return result;
        }            

        /// <summary>
        /// エニグマ・センス値の算出（弱小スキルなため、後編で増強）
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static double EnigmaSenseValue(MainCharacter player, bool duelMode)
        {
            // MAX(力、技、知）
            double atkBase = Math.Max(player.TotalStrength, Math.Max(player.TotalAgility, player.TotalIntelligence));
            double pStr = 0.0F;
            double pAgl = 0.0F;
            double pInt = 0.0F;
            if (atkBase == player.TotalIntelligence)
            {
                // [修正]SeventhMagicで弱体化するつもりはないため、ここで再反転させておく。
                if (player.CurrentSeventhMagic > 0)
                {
                    pStr = (double)((2.0F + (AP.Math.RandomInteger(3000) + 1) / 1000.0F));
                    pAgl = 0.0F;
                    pInt = 0.0F;
                }
                else
                {
                    pStr = 0.0F;
                    pAgl = 0.0F;
                    pInt = (double)((2.0F + (AP.Math.RandomInteger(3000) + 1) / 1000.0F));
                }
            }
            else if (atkBase == player.TotalAgility)
            {
                pStr = 0.0F;
                pAgl = (double)((2.0F + (AP.Math.RandomInteger(3000) + 1) / 1000.0F));
                pInt = 0.0F;
            }
            else  // でなければ「力」
            {
                // [修正]SeventhMagicで弱体化するつもりはないため、ここで再反転させておく。
                if (player.CurrentSeventhMagic > 0)
                {
                    pStr = 0.0F;
                    pAgl = 0.0F;
                    pInt = (double)((2.0F + (AP.Math.RandomInteger(3000) + 1) / 1000.0F));
                }
                else
                {
                    pStr = (double)((2.0F + (AP.Math.RandomInteger(3000) + 1) / 1000.0F));
                    pAgl = 0.0F;
                    pInt = 0.0F;
                }
            }
            return ConstructPhysicalDamage(player, pStr, pAgl, pInt, 0.0F, 1.0F, 30, 35, PlayerStance.FrontOffence, SpellSkillType.Standard, duelMode);
        }
        
        /// <summary>
        /// キネティック・スマッシュ値の算出
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static double KineticSmashValue(MainCharacter player, bool duelMode)
        {
            double result = ConstructPhysicalDamage(player, 1.0F, 0.0F, 0.0F, 1.0F, 3.0F, 2000, 3000, PlayerStance.FrontOffence, SpellSkillType.Standard, duelMode);
            result = result * PotentialValue(player, duelMode);
            return result;
        }

        /// <summary>
        /// ソウル・インフィニティ値の算出
        /// </summary>
        public static double SoulInfinityValue(MainCharacter player, bool duelMode)
        {
            // ( 力＋技＋知 ) ｘ 1.2 ｘ 潜在能力
            double pStr = 1.2F;
            double pAgl = 1.2F;
            double pInt = 1.2F;

            double result = ConstructPhysicalDamage(player, pStr, pAgl, pInt, 0.0F, 1.0F, 6000, 8000, PlayerStance.FrontOffence, SpellSkillType.Standard, duelMode);
            result = result * PotentialValue(player, duelMode);
            return result;
        }

        /// <summary>
        /// 朧・インパクト値の算出
        /// </summary>
        public static double OboroImpactValue(MainCharacter player, bool duelMode)
        {
            // ( max x 1.5 + mid x 1.0 + min x 0.5 ) x 潜在能力
            double pStr = 0.0F;
            double pAgl = 0.0F;
            double pInt = 0.0F;
            double max = Math.Max(Math.Max(player.TotalStrength, player.TotalAgility), player.TotalIntelligence);
            double min = Math.Min(Math.Min(player.TotalStrength, player.TotalAgility), player.TotalIntelligence);
            if (max == player.TotalStrength)
            {
                pStr = 1.5F;
                if (min == player.TotalAgility)
                {
                    pAgl = 0.5F;
                    pInt = 1.0F;
                }
                else
                {
                    pAgl = 1.0F;
                    pInt = 0.5F;
                }
            }
            else if (max == player.TotalAgility)
            {
                pAgl = 1.5F;
                if (min == player.TotalStrength)
                {
                    pStr = 0.5F;
                    pInt = 1.0F;
                }
                else
                {
                    pStr = 1.0F;
                    pInt = 0.5F;
                }
            }
            else if (max == player.TotalIntelligence)
            {
                pInt = 1.5F;
                if (min == player.TotalStrength)
                {
                    pStr = 0.5F;
                    pAgl = 1.0F;
                }
                else
                {
                    pStr = 1.0F;
                    pAgl = 0.5F;
                }
            }
            double result = ConstructPhysicalDamage(player, pStr, pAgl, pInt, 0.0F, 0.0F, 6000, 8000, PlayerStance.FrontOffence, SpellSkillType.Standard, duelMode);
            result = result * PotentialValue(player, duelMode);
            return result;
        }

        /// <summary>
        /// カタストロフィ値の算出
        /// </summary>
        public static double CatastropheValue(MainCharacter player, bool duelMode)
        {
            // (MAX x 0.0 + MID x 0.0 + MIN x 5.0 ) ｘ 潜在能力
            double pStr = 0.0F;
            double pAgl = 0.0F;
            double pInt = 0.0F;
            double min = Math.Min(Math.Min(player.TotalStrength, player.TotalAgility), player.TotalIntelligence);
            if (min == player.TotalStrength) { pStr = 5.0F; }
            else if (min == player.TotalAgility) { pAgl = 5.0F; }
            else if (min == player.TotalIntelligence) { pInt = 5.0F; }

            double result = ConstructPhysicalDamage(player, pStr, pAgl, pInt, 0.0F, 1.0F, 6000, 8000, PlayerStance.FrontOffence, SpellSkillType.Standard, duelMode);
            result = result * PotentialValue(player, duelMode);
            return result;
        }

        public static double PainfulInsanityValue(MainCharacter player, bool duelMode)
        {
            // 心ｘ３　（前編から後編にかけて、さらに潜在能力値増幅を追加）
            double result = ConstructMagicDamage(player, 0.0f, 3.0f, 2000, 3000, PlayerStance.FrontOffence, SpellSkillType.Standard, true, duelMode);
            result = result * PotentialValue(player, duelMode);
            return result;
        }

        /// <summary>
        /// インナー・インスピレーション値の算出（前編ではパラメタＭＡＸが１００前後、後編では９９９９ＭＡＸを意識した値に調整）
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static double InnerInspirationValue(MainCharacter player)
        {
            double min = 0;
            double max = 0;

            // ベース値＋１０は固定
            // 心      [ 1 - 100 ] -->   0 + 心 / 10
            //      [ 101 - 1000 ] -->  10 + 心 / 90
            //    [ 1001 - 10000 ] -->  20 + 心 / 900
            if (0 <= player.TotalMind && player.TotalMind <= 100)
            {
                min = 0.0F;
                max = 0.0F + (double)player.TotalMind / 10.0F;
            }
            else if (101 <= player.TotalMind && player.TotalMind <= 1000)
            {
                min = 10.0F;
                max = 10.0F + (double)(player.TotalMind - 100.0F) / 90.0F;
            }
            else if (1001 <= player.TotalMind && player.TotalMind <= 9999)
            {
                min = 20.0F;
                max = 20.0F + (double)(player.TotalMind - 1000.0F) / 900.0F;
            }

            Random rd = new Random(DateTime.Now.Millisecond * Environment.TickCount);
            double result = rd.Next((int)min, (int)(max + 1.0F)) + 10.0F;

            return result;
        }

        //   魔法！！！  //////////////////////////////////////////////////////////////////////////////////////////////////////

        // フレッシュ・ヒール値の算出
        public static double FreshHealValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 4.0F, 0.0F, 40, 50, PlayerStance.BackSupport, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }

        // ライフ・タップ値の算出
        public static double LifeTapValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 4.0F, 0.0F, 40, 50, PlayerStance.BackSupport, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }

        // ファイア・ボール値の算出
        public static double FireBallValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 3.0F, 0.0F, 30, 35, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }

        // アイス・ニードル値の算出
        public static double IceNeedleValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 2.8F, 0.0F, 30, 35, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }

        // ダークブラスト値の算出
        public static double DarkBlastValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 2.6F, 0.0F, 30, 35, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }

        // ワード・オブ・パワー値の算出
        public static double WordOfPowerValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 2.4F, 0.0F, 30, 35, PlayerStance.BackOffence, SpellSkillType.WordOfPower, false, duelMode);
        }

        // ワード・オブ・ライフ値の算出
        public static double WordOfLifeValue(MainCharacter player, bool duelMode)
        {
            // (知＋心)*潜在能力値
            double result = 0;
            result = ConstructMagicDamage(player, 1.0F, 1.0F, 30, 35, PlayerStance.BackSupport, SpellSkillType.Standard, false, duelMode);
            result += PotentialValue(player, duelMode);
            return result;
        }

        // フレイム・オーラ値の算出
        public static double FlameAuraValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 3.0F, 0.0F, 30, 35, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, true, duelMode);
        }

        // フローズン・オーラ値の算出
        public static double FrozenAuraValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 2.8F, 0.0F, 30, 35, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, true, duelMode);
        }

        // ホーリー・ショック値の算出
        public static double HolyShockValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 2.2F, 0.0F, 120, 135, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }

        // デヴォーリング・プラグー値の算出
        public static double DevouringPlagueValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 2.0F, 0.0F, 120, 135, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }

        // フラッシュ・ブレイズ値の算出
        public static double FlashBlazeValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 2.0F, 0.0F, 200, 300, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }

        // フラッシュ・ブレイズ（追加効果）値の算出
        public static double FlashBlaze_A_Value(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 2.0F, 0.0F, 200, 300, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }

        // エンレイジ・ブラスト値の算出
        public static double EnrageBlastValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 1.0F, 0.0F, 200, 300, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }
        // エンレイジ・ブラスト（追加効果）値の算出
        public static double EnrageBlast_A_Value(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 1.0F, 0.0F, 200, 300, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }

        // ブラック・ファイア値の算出
        public static double BlackFireValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 1.0F, 0.0F, 200, 300, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }

        // ワード・オブ・マリス値の算出
        public static double WordOfMaliceValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 1.0F, 0.0F, 200, 300, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }

        // イモレイト値の算出
        public static double ImmolateValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 1.0F, 0.0F, 200, 300, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }

        // ヴァニッシュ・ウェイヴ値の算出
        public static double VanishWaveValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 1.0F, 0.0F, 200, 300, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }

        // スター・ライトニング値の算出
        public static double StarLightningValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 1.0F, 0.0F, 200, 300, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }

        // ブルー・バレット値の算出
        public static double BlueBulletValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 0.9F, 0.0F, 200, 300, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }
        
        // フレイム・ストライク値の算出
        public static double FlameStrikeValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 3.5F, 0.0F, 750, 1000, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode); // 後編増強
        }

        // フローズン・ランス値の算出
        public static double FrozenLanceValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 3.3F, 0.0F, 750, 1000, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode); // 後編増強
        }

        // ライト・デトネイター値の算出
        public static double LightDetonatorValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 3.0f, 0.0F, 750, 1000, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }

        // ヴォルカニック・ウェイヴ値の算出
        public static double VolcanicWaveValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 4.0F, 0.0F, 1200, 1600, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode); // 後編増強
        }

        // ホワイト・アウト値の算出
        public static double WhiteOutValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 3.8F, 0.0F, 1200, 1600, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode); // 後編増強
        }


        // ブレイジング・フィールド（追加効果）値の算出
        public static double BlazingField_A_Value(MainCharacter player, bool duelMode)
        {
            //return ConstructMagicDamage(player, 2.5F, 1200, 1500, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);

            double result = player.CurrentBlazingFieldFactor * 2.5F + AP.Math.RandomInteger(1000);
           
            if (duelMode)
            {
                result = result * 0.5f;
            }
            return result;
        }

        // ピアッシング・フレイム値の算出
        public static double PiercingFlameValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 4.5F, 0.0F, 3500, 5000, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }

        // ドゥーム・ブレイド値の算出
        public static double DoomBladeValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 2.8F, 0.0F, 2000, 3500, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }
        // ドゥーム・ブレイド（マナダメージ）値の算出
        public static double DoomBlade_A_Value(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 1.5F, 0.0F, 500, 1000, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }
        
        // アビス・アイ値の算出
        public static double AbyssEyeValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 1.0F, 0.0F, 4500, 6000, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }

        // シン・フォーチュン値の算出
        public static double SinFortuneValue(MainCharacter player)
        {
            return 1.5F;
        }

        // サークレッド・ヒール値の算出
        public static double SacredHealValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 3.5F, 0.0F, 4000, 6000, PlayerStance.BackSupport, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }

        // アセンダント・メテオ値の算出
        public static double AscendantMeteorValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 1.5F, 0.0F, 2000, 3000, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }

        // セレスティアル・ノヴァ
        public static double CelestialNovaValue_A(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 4.5F, 0.0F, 4000, 5000, PlayerStance.BackOffence, SpellSkillType.Standard, false, duelMode);
        }
        public static double CelestialNovaValue_B(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 5.0F, 0.0F, 8000, 10000, PlayerStance.BackSupport, SpellSkillType.Standard, false, duelMode);
        }

        // デーモニック・イグニート
        public static double DemonicIgniteValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 4.5F, 0.0F, 6000, 7000, PlayerStance.BackOffence, SpellSkillType.Standard, false, duelMode);
        }
             
        // ラヴァ・アニヒレーション値の算出
        public static double LavaAnnihilationValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 5.0f, 0.0F, 7000, 8000, PlayerStance.BackOffence, SpellSkillType.Standard, false, duelMode);
        }

        // ゼータ・エクスプロージョン値の算出
        public static double ZetaExplosionValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 6.0F, 0.0F, 8000, 12000, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }

        // チル・バーン（直接）値の算出
        public static double ChillBurnValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 1.0f, 0.0F, 0, 0, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);// 5.0F, 3300, 7200, PlayerStance.BackOffence, false, false);
        }

        // リザレクション値の算出
        public static double ResurrectionValue(MainCharacter player)
        {
            return player.MaxLife / 2.0F;
        }
        // デス・ディナイ
        public static double DeathDenyValue(MainCharacter player)
        {
            return player.MaxLife;
        }
        // エヴァー・ドロップレット値の算出
        public static double EverDropletValue(MainCharacter player)
        {
            return player.MaxMana / 30.0F; // 「警告」要調整
        }


        // ダムネーションの算出
        public static double DamnationValue(MainCharacter player)
        {
            double result = 0;
            // 心   [    1 -   100 ] --> 10.0  ～  20.0 --> 10.0 + 10.0 * 心 /  100
            //      [  101 -   400 ] --> 20.0  ～  34.0 --> 20.0 + 14.0 * 心 /  300
            //      [  401 -  1000 ] --> 34.0  ～  52.0 --> 34.0 + 18.0 * 心 /  600
            //      [ 1001 -  3500 ] --> 52.0  ～  74.0 --> 52.0 + 22.0 * 心 / 2500
            //      [ 3501 - 10000 ] --> 74.0  ～ 100.0 --> 74.0 + 26.0 * 心 / 6500
            if (0 <= player.TotalMind && player.TotalMind <= 100)
            {
                result = 10.0F + 10.0F * (double)(player.TotalMind - 0.0F) / 100.0F;
            }
            else if (101 <= player.TotalMind && player.TotalMind <= 400)
            {
                result = 20.0F + 14.0F * (double)(player.TotalMind - 100.0F) / 300.0F;
            }
            else if (401 <= player.TotalMind && player.TotalMind <= 1000)
            {
                result = 34.0F + 18.0F * (double)(player.TotalMind - 400.0F) / 600.0F;
            }
            else if (1001 <= player.TotalMind && player.TotalMind <= 3500)
            {
                result = 52.0F + 22.0F * (double)(player.TotalMind - 1000.0F) / 2500.0F;
            }
            else if (3501 <= player.TotalMind && player.TotalMind <= 9999)
            {
                result = 74.0F + 26.0F * (double)(player.TotalMind - 3500.0F) / 6500.0F;
            }

            result = ((double)player.MaxLife / result);
            return result;
        }

        public enum ParameterType
        {
            Strength,
            Agility,
            Intelligence,
            Stamina,
            Mind,
        }

        /// <summary>
        /// トランッセンデント・ウィッシュ値の算出
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static double TranscendentWishValue(MainCharacter player, ParameterType type)
        {
            if (type == ParameterType.Strength)
            {
                return player.TotalStrength * 0.5f;
            }
            else if (type == ParameterType.Agility)
            {
                return player.TotalAgility * 0.5f;
            }
            else if (type == ParameterType.Intelligence)
            {
                return player.TotalIntelligence * 0.5f;
            }
            else if (type == ParameterType.Stamina)
            {
                return player.TotalStamina * 0.5f;
            }
            else if (type == ParameterType.Mind)
            {
                return player.TotalMind * 0.5f;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// ワープ・ゲート値の算出
        /// </summary>
        public static double WarpGateValue(MainCharacter player)
        {
            return 250; // ゲージバーを進める値
        }

        /// <summary>
        /// 物理系統のダメージの場合
        /// </summary>
        /// <param name="player">発動元のプレイヤー</param>
        /// <param name="pStr">力パラメタによる増強倍率</param>
        /// <param name="minBorder">ベース最小値</param>
        /// <param name="maxBorder">ベース最大値</param>
        /// <param name="stance">スタンスの対象、回復系はBackSupport、攻撃はFrontOffenseなど</param>
        /// <param name="spellSkill">スペル、スキルタイプを指定</param>
        /// <returns></returns>
        private static double ConstructPhysicalDamage(MainCharacter player, double pStr, double pAgl, double pInt, double pMind, double pWeapon, int minBorder, int maxBorder, PlayerStance stance, SpellSkillType spellSkill, bool duelMode)
        {
            // ベース値を算出し・・・
            double result = PhysicalAttackValue(player, NeedType.Random, pStr, pAgl, pInt, pMind, pWeapon, stance, spellSkill, duelMode);

            // 弱すぎる場合の最低値を付与した上で
            Random rd = new Random(DateTime.Now.Millisecond * Environment.TickCount);
            result += rd.Next(minBorder, maxBorder);

            // それを返す！
            return result;
        }

        /// <summary>
        /// 魔法系統のダメージの場合、
        /// </summary>
        /// <param name="player">発動元のプレイヤー</param>
        /// <param name="pInt">知力の倍率</param>
        /// <param name="minBorder">ベース最小値</param>
        /// <param name="maxBorder">ベース最大値</param>
        /// <param name="stance">スタンスの対象、回復系はBackSupport、攻撃はFrontOffenseなど</param>
        /// <param name="spellSkill">スペル・スキルの名前</param>
        /// <param name="ignoreChargeCount">ためるコマンドが対象外の場合True</param>
        /// <returns></returns>
        private static double ConstructMagicDamage(MainCharacter player, double pInt, double pMind, int minBorder, int maxBorder, PlayerStance stance, SpellSkillType spellSkill, bool ignoreChargeCount, bool duelMode)
        {
            // ベース値を算出し・・・
            double result = MagicAttackValue(player, NeedType.Random, pInt, pMind, stance, spellSkill, ignoreChargeCount, duelMode);

            // 弱すぎる場合の最低値を付与した上で
            Random rd = new Random(DateTime.Now.Millisecond * Environment.TickCount);
            result += rd.Next(minBorder, maxBorder);

            // それを返す！
            return result;
        }


        // その他
        public static double PoisonValue(MainCharacter player)
        {
            double maxLevel = 10.0F;
            double ratio = player.CurrentPoisonValue;
            if (ratio >= maxLevel) ratio = maxLevel;

            return Convert.ToInt32((double)player.MaxLife * ((double)player.CurrentPoisonValue / maxLevel));
        }

        public static double SlipValue(MainCharacter player)
        {
            return Convert.ToInt32((float)player.MaxLife / 10.0F);
        }




        ////　アイテム！！！ ////////////////////////////////////////////


        /// <summary>
        /// ライジング・ナックル値の算出
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static double RisingKnuckleValue(MainCharacter player, bool duelMode)
        {
            return ConstructPhysicalDamage(player, 1.0f, 1.0f, 0, 0, 0, 40, 50, PlayerStance.FrontOffence, SpellSkillType.Standard, duelMode);
        }

        /// <summary>
        /// アイス・ソード値の算出
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static double IceSwordValue(MainCharacter player, bool duelMode)
        {
            return ConstructPhysicalDamage(player, 1.0f, 0, 1.0f, 0, 0, 65, 90, PlayerStance.FrontOffence, SpellSkillType.Standard, duelMode);
        }

        /// <summary>
        /// エアロ・ブレード値の算出
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static double AeroBladeValue(MainCharacter player, bool duelMode)
        {
            return ConstructPhysicalDamage(player, 1.0f, 0, 1.0f, 0, 0, 30, 35, PlayerStance.FrontOffence, SpellSkillType.Standard, duelMode);
        }

        /// <summary>
        /// ライフ・ソード値の算出
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static double LifeSwordValue(MainCharacter player, bool duelMode)
        {
            return ConstructPhysicalDamage(player, 0.0f, 0, 1.0f, 0, 1.0f, 30, 35, PlayerStance.BackSupport, SpellSkillType.Standard, duelMode);
        }

        /// <summary>
        /// 疾風の宝珠値の算出
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static double HayateOrbValue(MainCharacter player)
        {
            return 30; // 戦闘速度一時UPタイム
        }

        /// <summary>
        /// オータムン・ロッド値の算出
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static double AutumnRodValue(MainCharacter player)
        {
            return 10; // Mana回復
        }
        /// <summary>
        /// フラワー・ワンド値の算出
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static double FlowerWandValue(MainCharacter player)
        {
            return 16; // Mana回復
        }
        /// <summary>
        /// ブルー・ライトニング値の算出
        /// </summary>
        public static double BlueLightningValue(MainCharacter player, bool duelMode)
        {
            return ConstructPhysicalDamage(player, 0.0f, 0, 1.5f, 0, 0, 200, 250, PlayerStance.FrontOffence, SpellSkillType.Standard, duelMode);
        }

        /// <summary>
        /// バーニング・クレイモア値の算出
        /// </summary>
        public static double BurningClaymoreValue(MainCharacter player)
        {
            return 150; // 力Up
        }

        /// <summary>
        /// 知恵の輪リング値の算出
        /// </summary>
        public static double ChienowaRingValue(MainCharacter player)
        {
            return 300; // レジスト水をＵＰ
        }

        /// <summary>
        /// ロケット・ダッシュ値の算出
        /// </summary>
        public static double RocketDashValue(MainCharacter player)
        {
            return 200; // ゲージバーを逆戻しする値
        }

        /// <summary>
        /// ウォードラム値の算出
        /// </summary>
        public static double WarDrumValue(MainCharacter player)
        {
            return 50; // 力パラメタを増強させる値
        }

        /// <summary>
        /// 力の杖値の算出
        /// </summary>
        public static double RodOfStrengthValue(MainCharacter player)
        {
            return 100; // 力を増強させる値
        }

        /// <summary>
        /// ラス・サーベル・クロー値の算出
        /// </summary>
        public static double WrathServelClawValue(MainCharacter player, bool duelMode)
        {
            return ConstructPhysicalDamage(player, 1.1f, 0, 1.1f, 0, 0, 400, 500, PlayerStance.FrontOffence, SpellSkillType.Standard, duelMode);
        }

        /// <summary>
        /// 赤蒼授の杖値の算出
        /// </summary>
        public static double BlueRedRodValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 1.5f, 0.0F, 500, 750, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }
        public static double BlueRedRodValue_A(MainCharacter player)
        {
            return 100; // マナ回復
        }

        /// <summary>
        /// 命運プリズムボックス値の算出
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public static double MeiunBoxValue_A(MainCharacter player)
        {
            return 1000; // ライフ回復
        }
        public static double MeiunBoxValue_B(MainCharacter player)
        {
            return 200; // マナ回復
        }
        public static double MeiunBoxValue_C(MainCharacter player)
        {
            return 20; // スキル回復
        }

        // 黒氷刀値の算出
        public static double BlackIceSwordValue(MainCharacter player)
        {
            return 150; // マナ回復
        }
        // メンタライズド・フォース・クロー値の算出
        public static double MentalizedForceClawValue(MainCharacter player)
        {
            return 2; // スキル回復
        }
        // エターナル・ロイヤルリング値の算出
        public static double EternalLoyalRingValue(MainCharacter player)
        {
            return 3; // スキル回復
        }

        // クレイモア・オブ・ザックス値の算出
        public static double ClaymoreZuksValue(MainCharacter player)
        {
            return 750; // ライフ回復
        }
        // アダーカー・フォルス・ロッド値の算出
        public static double AderkerFalseRodValue(MainCharacter player)
        {
            return 100; // インスタント・ポイント回復
        }
        // ソード・オブ・ディバイド値の算出
        public static double SwordOfDivideValue(MainCharacter player)
        {
            return (double)player.CurrentLife / 5.0f;
        }
        public static double SwordOfDivideValue_A(MainCharacter player)
        {
            return 5; // パーセンテージ ( X / 100)の値
        }
        // 真紅炎・マスターブレイド値の算出
        public static double SinkouenMasterBladeValue_A(MainCharacter player)
        {
            return 30; // パーセンテージ ( X / 100)の値
        }
        // デビルキラー値の算出
        public static double DevilKillerValue(MainCharacter player)
        {
            return 20; // パーセンテージ ( X / 100)の値
        }

        // スケール・オブ・ブルーレイジ値の算出
        public static double ScaleOfBlueRageValue(MainCharacter player)
        {
            return 5; // パーセンテージ ( X / 100)の値
        }
        // ブルー・リフレクト・ローブ値の算出
        public static double BlueReflectRobeValue(MainCharacter player)
        {
            return 5; // パーセンテージ ( X / 100)の値
        }
        // スライド・スルー・シールド値の算出
        public static double SlideThroughShieldValue(MainCharacter player)
        {
            return 5; // パーセンテージ ( X / 100)の値
        }
        // ウィンターズ・ホーン値の算出
        public static double WintersHornValue(MainCharacter player)
        {
            return 250; // 知パラメタを増強させる値
        }
        // 新成緑の宝珠値の算出
        public static double EverGrowGreenValue(MainCharacter player)
        {
            return 3; // スキル回復
        }
        // 海星源の宝珠値の算出
        public static double GroundSeaStarValue(MainCharacter player)
        {
            return 30; // パーセンテージ ( X / 100)の値
        }
        // シュヴァルツェ・ソード値の算出
        public static double ShuvalzFloreSwordValue(MainCharacter player)
        {
            return 5; // スキル回復
        }
        // シュヴァルツェ・シールド値の算出
        public static double ShuvalzFloreShieldValue(MainCharacter player)
        {
            return (double)player.MaxLife * 0.05f;
        }
        // シュヴァルツェ・アーマー値の算出
        public static double ShuvalzFloreArmorValue(MainCharacter player)
        {
            return (double)player.MaxMana * 0.05f;
        }
        // シェズル・ミスティック・フォーチュン値の算出
        public static double ShezlMysticFortuneValue(MainCharacter player)
        {
            return (double)player.MaxMana * 0.05f;
        }
        // 神剣フェルトゥーシュ値の算出
        public static double FeltusValue(MainCharacter player)
        {
            return 100.0f;
        }
        // ジュザ・ファンタズマル・クロー値の算出
        public static double JuzaPhantasmalValue(MainCharacter player)
        {
            return 1.00f + player.CurrentJuzaPhantasmalValue * 0.02f; // 蓄積カウンター１つに付き、上昇パーセンテージ（1.00 + X)
        }
        // エターナル・フェイトリング値の算出
        public static double EternalFateRingValue(MainCharacter player)
        {
            return 1.00f + player.CurrentEternalFateRingValue * 0.02f; // 蓄積カウンター１つに付き、上昇パーセンテージ（1.00 + X)
        }
        // ライト・サーヴァント値の算出
        public static double LightServantValue(MainCharacter player)
        {
            return 10000 + AP.Math.RandomInteger(2000);
        }
        // エディル・ブルー・バーン値の算出
        public static double AdilBlueBurnValue(MainCharacter player)
        {
            return 3000 * player.CurrentAdilBlueBurnValue + AP.Math.RandomInteger(2000 * player.CurrentAdilBlueBurnValue);
        }
        // 百獣皮の舞踏衣値の算出
        public static double OneHundredButougiValue(MainCharacter player)
        {
            return 20; // パーセンテージ ( X / 100)の値
        }
        // ソサエティ・シンボル値の算出
        public static double SocietySymbolValue(MainCharacter player)
        {
            return 1000 + AP.Math.RandomInteger(200); // 直接の値
        }
        // 銀羽飾りの腕章値の算出
        public static double SilverFeatherBraceletValue(MainCharacter player)
        {
            return 1000 + AP.Math.RandomInteger(200); // 直接の値
        }
        // バード・ソング・リュート値の算出
        public static double BirdSongLuteValue(MainCharacter player)
        {
            return 750;
        }
        // コア・エッセンス・チャネル値の算出
        public static double CoreEssenceChannelValue_A(MainCharacter player)
        {
            return 7500 + AP.Math.RandomInteger(2500); // ライフ回復
        }
        public static double CoreEssenceChannelValue_B(MainCharacter player)
        {
            return 550 + AP.Math.RandomInteger(450); // マナ回復
        }
        public static double CoreEssenceChannelValue_C(MainCharacter player)
        {
            return 20 + AP.Math.RandomInteger(10); // スキル回復
        }
        // ハンターズ・アイ値の算出
        public static double HuntersEyeValue_A(MainCharacter player)
        {
            return 300;
        }
        public static double HuntersEyeValue_B(MainCharacter player)
        {
            return 150;
        }
        public static double HuntersEyeValue_C(MainCharacter player)
        {
            return 400;
        }
        public static double HuntersEyeValue_D(MainCharacter player)
        {
            return 200;
        }
        // エムブレム・オブ・ヴァルキリー値の算出
        public static double EmblemOfValkyrieValue(MainCharacter player)
        {
            return 25; // パーセンテージ ( X / 100)の値
        }
        public static double EmblemOfValkyrieValue_A(MainCharacter player)
        {
            return 2; // 効果継続ターンの値
        }
        // エムブレム・オブ・ハデス値の算出
        public static double EmblemOfHades(MainCharacter player)
        {
            return 4; // パーセンテージ ( X / 100)の値
        }
        // 氷絶零の宝珠値の算出
        public static double SilentColdIceValue(MainCharacter player)
        {
            return 10; // パーセンテージ ( X / 100)の値
        }
        public static double SilentColdIceValue_A(MainCharacter player)
        {
            return 2; // 効果継続ターンの値
        }

        // エゼクリエル・アーマー・シギル値の算出
        public static double EzekrielArmorSigilValue_A(MainCharacter player)
        {
            return 2500 + AP.Math.RandomInteger(1000);
        }
        public static double EzekrielArmorSigilValue_B(MainCharacter player)
        {
            return 1500 + AP.Math.RandomInteger(750);
        }
        public static double EzekrielArmorSigilValue_C(MainCharacter player)
        {
            return 5 + AP.Math.RandomInteger(5);
        }
        // メイズ・キューブ値の算出
        public static double MazeCubeValue(MainCharacter player)
        {
            return 1.00f + player.CurrentMazeCubeValue * 0.02f; // 蓄積カウンター１つに付き、上昇パーセンテージ率（1.00 + X)
        }
        // レインボーチューブ値の算出
        public static double RainbowTubeValue_A(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 4.0F, 0.0F, 1200, 1600, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode); // ヴォルカニックウェイヴと同等
        }
        public static double RainbowTubeValue_B(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 5.0F, 0.0F, 5000, 7000, PlayerStance.BackSupport, PrimaryLogic.SpellSkillType.Standard, false, duelMode); // サークレッド・ヒールと同等
        }
        // デビル・サモナーズ・トーム値の算出
        public static double DevilSummonerTomeValue(MainCharacter player, bool duelMode)
        {
            return ConstructMagicDamage(player, 3.0F, 0.0F, 2500, 6000, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode);
        }
        // シール・オブ・バランス値の算出
        public static double SealOfBalanceValue_A(MainCharacter player)
        {
            return 1000 + AP.Math.RandomInteger(500);
        }
        public static double SealOfBalanceValue_B(MainCharacter player)
        {
            return 4 + AP.Math.RandomInteger(4);
        }
        // フローティング・ホワイト・ボール値の算出
        public static double FloatingWhiteBallValue(MainCharacter player)
        {
            return 15; // パーセンテージ ( X / 100)の値
        }
        // 暗黒の通貨値の算出
        public static double DarknessCoinValue(MainCharacter player)
        {
            return player.MaxLife * 0.2f; // 削り取りパーセンテージ(MaxValue * X)
        }
        // 不死なる焔火のリング値の算出
        public static double EternalHomuraRingValue(MainCharacter player, bool duelMode)
        {
            double factor = player.CurrentMana / player.MaxMana;
            double amp = 12.0F;
            double min = 90000;
            double max = 150000;

            return ConstructMagicDamage(player, amp, 0.0F, (int)min, (int)max, PlayerStance.BackOffence, PrimaryLogic.SpellSkillType.Standard, false, duelMode); // ゼータ・エクスプロージョンと同等
        }
        public static double EternalHomuraRingValue_A(MainCharacter player)
        {
            return 2000 + AP.Math.RandomInteger(1000);
        }

        ////　その他！！！ ////////////////////////////////////////////
        public static double FireDamage2Value(MainCharacter player)
        {
            return 3500 + AP.Math.RandomInteger(1000)*2.0f;
        }

        public static double VoiceOfNoMercy(MainCharacter player)
        {
            // 最大ライフ×(1.0F - 心)
            double factor = 1.0F - (double)((double)player.TotalMind / 1000.0F);
            if (factor <= 0.0F) factor = 0.0f;

            return player.MaxLife * factor;
        }
        
        ////  レギィンアーゼ固有 //////////////////////////////////////
        public static double IchinaruHomuraValue(MainCharacter player)
        {
            double pInt = 1.0F;
            if (player.CurrentAbyssWill > 0)
            {
                pInt += player.CurrentAbyssWillValue * 0.5F;
            }
            return ConstructMagicDamage(player, pInt, 0.0F, 0, 0, PlayerStance.BackOffence, SpellSkillType.Standard, false, false);
        }
        public static double AbyssFireValue(MainCharacter player)
        {
            double pInt = 1.0F;
            if (player.CurrentAbyssWill > 0)
            {
                pInt += player.CurrentAbyssWillValue * 0.5F;
            }
            return ConstructMagicDamage(player, pInt, 0.0F, 0, 0, PlayerStance.BackOffence, SpellSkillType.Standard, false, false);
        }
        public static double EternalDropletValue_A(MainCharacter player)
        {
            // ライフゲイン
            // 1/10 (0.10)削れてない場合、1/100回復
            // 3/4  (0.25)削れてない場合、1/50回復
            // 1/2  (0.50)削れてない場合、1/25回復
            // 1/5  (0.80)削れてない場合、1/16回復
            // 最後-------------------->、1/10回復
            if (player.CurrentLife >= player.MaxLife * 0.9f)
            {
                return (double)player.MaxLife / 100.0F;
            }
            else if (player.CurrentLife >= player.MaxLife * 0.75f)
            {
                return (double)player.MaxLife / 50.0F;
            }
            else if (player.CurrentLife >= player.CurrentLife * 0.5f)
            {
                return (double)player.MaxLife / 25.0F;
            }
            else if (player.CurrentLife >= player.CurrentLife * 0.2f)
            {
                return (double)player.MaxLife / 16.0F;
            }
            else
            {
                return (double)player.MaxLife / 10.0F;
            }
        }
        public static double EternalDropletValue_B(MainCharacter player)
        {
            // マナゲイン
            // 1/20回復
            return player.MaxLife / 20.0F;
        }

    }
}
