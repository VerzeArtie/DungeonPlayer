using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class BattleEnemy : MotherForm
    {
        private int FirstStrikeProb(int src)
        {
            if (src <= -100) src = -100;
            if (src >= 100) src = 100;

            int[] data = new int[201]; // -100から+100まで201の値を受け持つ
            data[0] = 5;
            data[1] = 5;
            data[2] = 6;
            data[3] = 6;
            data[4] = 7;
            data[5] = 7;
            data[6] = 8;
            data[7] = 8;
            data[8] = 9;
            data[9] = 9;
            data[10] = 10;
            data[11] = 11;
            data[12] = 12;
            data[13] = 13;
            data[14] = 14;
            data[15] = 15;
            data[16] = 16;
            data[17] = 17;
            data[18] = 18;
            data[19] = 19;
            data[20] = 20;
            data[21] = 21;
            data[22] = 23;
            data[23] = 25;
            data[24] = 27;
            data[25] = 29;
            data[26] = 31;
            data[27] = 33;
            data[28] = 35;
            data[29] = 37;
            data[30] = 39;
            data[31] = 43;
            data[32] = 47;
            data[33] = 51;
            data[34] = 55;
            data[35] = 59;
            data[36] = 63;
            data[37] = 66;
            data[38] = 70;
            data[39] = 74;
            data[40] = 78;
            data[41] = 86;
            data[42] = 94;
            data[43] = 102;
            data[44] = 109;
            data[45] = 117;
            data[46] = 125;
            data[47] = 133;
            data[48] = 141;
            data[49] = 148;
            data[50] = 156;
            data[51] = 172;
            data[52] = 188;
            data[53] = 203;
            data[54] = 219;
            data[55] = 234;
            data[56] = 250;
            data[57] = 266;
            data[58] = 281;
            data[59] = 297;
            data[60] = 313;
            data[61] = 344;
            data[62] = 375;
            data[63] = 406;
            data[64] = 438;
            data[65] = 469;
            data[66] = 500;
            data[67] = 531;
            data[68] = 563;
            data[69] = 594;
            data[70] = 625;
            data[71] = 688;
            data[72] = 750;
            data[73] = 813;
            data[74] = 875;
            data[75] = 938;
            data[76] = 1000;
            data[77] = 1063;
            data[78] = 1125;
            data[79] = 1188;
            data[80] = 1250;
            data[81] = 1375;
            data[82] = 1500;
            data[83] = 1625;
            data[84] = 1750;
            data[85] = 1875;
            data[86] = 2000;
            data[87] = 2125;
            data[88] = 2250;
            data[89] = 2375;
            data[90] = 2500;
            data[91] = 2750;
            data[92] = 3000;
            data[93] = 3250;
            data[94] = 3500;
            data[95] = 3750;
            data[96] = 4000;
            data[97] = 4250;
            data[98] = 4500;
            data[99] = 4750;
            data[100] = 5000;
            data[101] = 5250;
            data[102] = 5500;
            data[103] = 5750;
            data[104] = 6000;
            data[105] = 6250;
            data[106] = 6500;
            data[107] = 6750;
            data[108] = 7000;
            data[109] = 7250;
            data[110] = 7500;
            data[111] = 7625;
            data[112] = 7750;
            data[113] = 7875;
            data[114] = 8000;
            data[115] = 8125;
            data[116] = 8250;
            data[117] = 8375;
            data[118] = 8500;
            data[119] = 8625;
            data[120] = 8750;
            data[121] = 8813;
            data[122] = 8875;
            data[123] = 8938;
            data[124] = 9000;
            data[125] = 9063;
            data[126] = 9125;
            data[127] = 9188;
            data[128] = 9250;
            data[129] = 9313;
            data[130] = 9375;
            data[131] = 9406;
            data[132] = 9438;
            data[133] = 9469;
            data[134] = 9500;
            data[135] = 9531;
            data[136] = 9563;
            data[137] = 9594;
            data[138] = 9625;
            data[139] = 9656;
            data[140] = 9688;
            data[141] = 9703;
            data[142] = 9719;
            data[143] = 9734;
            data[144] = 9750;
            data[145] = 9766;
            data[146] = 9781;
            data[147] = 9797;
            data[148] = 9813;
            data[149] = 9828;
            data[150] = 9844;
            data[151] = 9852;
            data[152] = 9859;
            data[153] = 9867;
            data[154] = 9875;
            data[155] = 9883;
            data[156] = 9891;
            data[157] = 9898;
            data[158] = 9906;
            data[159] = 9914;
            data[160] = 9922;
            data[161] = 9926;
            data[162] = 9930;
            data[163] = 9934;
            data[164] = 9938;
            data[165] = 9941;
            data[166] = 9945;
            data[167] = 9949;
            data[168] = 9953;
            data[169] = 9957;
            data[170] = 9961;
            data[171] = 9963;
            data[172] = 9965;
            data[173] = 9967;
            data[174] = 9969;
            data[175] = 9971;
            data[176] = 9973;
            data[177] = 9975;
            data[178] = 9977;
            data[179] = 9979;
            data[180] = 9980;
            data[181] = 9981;
            data[182] = 9982;
            data[183] = 9983;
            data[184] = 9984;
            data[185] = 9985;
            data[186] = 9986;
            data[187] = 9987;
            data[188] = 9988;
            data[189] = 9989;
            data[190] = 9990;
            data[191] = 9991;
            data[192] = 9991;
            data[193] = 9992;
            data[194] = 9992;
            data[195] = 9993;
            data[196] = 9993;
            data[197] = 9994;
            data[198] = 9994;
            data[199] = 9995;
            data[200] = 9995;

            return data[src+100];
        }

        private void AddActivePlayer(SortedList<int, MainCharacter> list, MainCharacter chara)
        {
            if (chara != null)
            {
                while (true)
                {
                    try
                    {
                        Random rd = new Random((int)(Environment.TickCount * DateTime.Now.Millisecond) + list.Count + 1);
                        int randomSpeed = (int)(5000.0F + normaldistr.invnormaldistribution(AP.Math.RandomReal()) * 2500.0F);
                        if (randomSpeed <= 0) randomSpeed = 0; if (randomSpeed >= 10000) randomSpeed = 10000;
                        randomSpeed = 10000 - randomSpeed;
                        list.Add(randomSpeed, chara);
                        //this.textBox1.Text += chara.Name + ":" + randomSpeed.ToString() + "\r\n";
                        System.Threading.Thread.Sleep(1); // TickCountとMillisecondを狂わせるために入れてます。
                        break;
                    }
                    catch
                    {
                        System.Threading.Thread.Sleep(1); // 万が一、登録済みのプレイヤーと値がぶつかった時やりなおします。
                    }
                }
            }
        }

        private int SetupEnemyAction()
        {
            Random rd = new Random(DateTime.Now.Millisecond * Environment.TickCount);

            if (ec1.Name == "ヴェルゼ・アーティ")
            {
                // Logic A
                if (mc.CurrentLife < (mc.MaxLife / 5))
                {
                    switch(rd.Next(1, 3))
                    {
                        case 1:
                            if (ec1.CurrentSkillPoint >= Database.DOUBLE_SLASH_COST)
                            {
                                SelectDoubleSlash(ec1);
                                return 4;
                            }
                            else
                            {
                                SelectNormalAttack(ec1);
                                return 1;
                            }
                        case 2:
                            if (ec1.CurrentSkillPoint >= Database.CRUSHING_BLOW_COST && mc.CurrentStunning <= 0)
                            {
                                SelectCrushingBlow(ec1);
                                return 5;
                            }
                            else if (ec1.CurrentSkillPoint >= Database.DOUBLE_SLASH_COST)
                            {
                                SelectDoubleSlash(ec1);
                                return 4;
                            }
                            else
                            {
                                SelectNormalAttack(ec1);
                                return 1;
                            }
                    }
                }

                // Logic B
                if (ec1.CurrentOneImmunity > 0)
                {
                    if (ec1.CurrentSkillPoint >= Database.STANCE_OF_STANDING_COST)
                    {
                        SelectStanceOfStanding(ec1);
                        return 3;
                    }
                    else
                    {
                        SelectNormalAttack(ec1);
                        return 1;
                    }

                }

                // Logic C
                if (ec1.CurrentGaleWind > 0)
                {
                    switch (rd.Next(1, 3))
                    {
                        case 1:
                            if (ec1.CurrentSkillPoint >= Database.DOUBLE_SLASH_COST)
                            {
                                SelectDoubleSlash(ec1);
                                return 4;
                            }
                            else
                            {
                                SelectNormalAttack(ec1);
                                return 1;
                            }
                        case 2:
                            if (ec1.CurrentLife < (ec1.MaxLife / 2))
                            {
                                if (ec1.CurrentMana >= Database.FRESH_HEAL_COST)
                                {
                                    SelectFreshHeal(ec1);
                                    return 6;
                                }
                                else
                                {
                                    if (ec1.CurrentSkillPoint >= Database.DOUBLE_SLASH_COST)
                                    {
                                        SelectDoubleSlash(ec1);
                                        return 4;
                                    }
                                    else
                                    {
                                        SelectNormalAttack(ec1);
                                        return 1;
                                    }
                                }
                            }
                            else
                            {
                                if (ec1.CurrentSkillPoint >= Database.DOUBLE_SLASH_COST)
                                {
                                    SelectDoubleSlash(ec1);
                                    return 4;
                                }
                                else
                                {
                                    SelectNormalAttack(ec1);
                                    return 1;
                                }
                            }
                    }
                }

                // Logic D
                if (mc.CurrentStunning > 0)
                {
                    int resultA = rd.Next(1, 4);
                    switch (resultA)
                    {
                        case 1:
                            if (ec1.CurrentSkillPoint >= Database.DOUBLE_SLASH_COST)
                            {
                                SelectDoubleSlash(ec1);
                                return 4;
                            }
                            else
                            {
                                SelectNormalAttack(ec1);
                                return 1;
                            }
                        case 2:
                            if (ec1.CurrentLife < (ec1.MaxLife * 3 / 4))
                            {
                                if (ec1.CurrentMana >= Database.FRESH_HEAL_COST)
                                {
                                    SelectFreshHeal(ec1);
                                    return 6;
                                }
                                else
                                {
                                    if (ec1.CurrentSkillPoint >= Database.DOUBLE_SLASH_COST)
                                    {
                                        SelectDoubleSlash(ec1);
                                        return 4;
                                    }
                                    else
                                    {
                                        SelectNormalAttack(ec1);
                                        return 1;
                                    }
                                }
                            }
                            else
                            {
                                if (ec1.CurrentSkillPoint >= Database.DOUBLE_SLASH_COST)
                                {
                                    SelectDoubleSlash(ec1);
                                    return 4;
                                }
                                else
                                {
                                    SelectNormalAttack(ec1);
                                    return 1;
                                }
                            }

                        case 3:
                            if (ec1.CurrentMana >= Database.ONE_IMMUNITY_COST)
                            {
                                SelectOneImmunity(ec1);
                                return 7;
                            }
                            else
                            {
                                if (ec1.CurrentSkillPoint >= Database.DOUBLE_SLASH_COST)
                                {
                                    SelectDoubleSlash(ec1);
                                    return 4;
                                }
                                else
                                {
                                    SelectNormalAttack(ec1);
                                    return 1;
                                }
                            }
                    }
                }

                // Logic E
                if ((mc.CurrentProtection > 0)
                    || (mc.CurrentSaintPower > 0)
                    || (mc.CurrentShadowPact > 0)
                    || (mc.CurrentBloodyVengeance > 0)
                    || (mc.CurrentFlameAura > 0)
                    || (mc.CurrentHeatBoost > 0)
                    || (mc.CurrentAbsorbWater > 0)
                    || (mc.CurrentPromisedKnowledge > 0)
                    || (mc.CurrentWordOfLife > 0)
                    || (mc.CurrentEternalPresence > 0)
                    || (mc.CurrentRiseOfImage > 0))
                {
                    if (ec1.CurrentMana >= Database.DISPEL_MAGIC_COST)
                    {
                        SelectDispelMagic(ec1);
                        return 10;
                    }
                    else
                    {
                        if (ec1.CurrentSkillPoint >= Database.DOUBLE_SLASH_COST)
                        {
                            SelectDoubleSlash(ec1);
                            return 4;
                        }
                        else
                        {
                            SelectNormalAttack(ec1);
                            return 1;
                        }
                    }
                }

                // Logic F
                if (ec1.CurrentLife < (ec1.MaxLife / 2))
                {
                    if (ec1.CurrentMana >= Database.FRESH_HEAL_COST)
                    {
                        SelectFreshHeal(ec1);
                        return 6;
                    }
                    else
                    {
                        if (ec1.CurrentSkillPoint >= Database.DOUBLE_SLASH_COST)
                        {
                            SelectDoubleSlash(ec1);
                            return 4;
                        }
                        else
                        {
                            SelectNormalAttack(ec1);
                            return 1;
                        }
                    }
                }

                // Logic G
                // [警告]：本来ここでCrushingBlowを基本にすれば強烈な強さだが手加減をする事とする。
                if (ec1.CurrentLife == ec1.MaxLife)
                {
                    switch (rd.Next(1, 9))
                    {
                        case 1:
                            if (ec1.CurrentSkillPoint >= Database.COUNTER_ATTACK_COST)
                            {
                                SelectCounterAttack(ec1);
                                return 2;
                            }
                            else
                            {
                                SelectNormalAttack(ec1);
                                return 1;
                            }
                        case 2:
                            if (ec1.CurrentSkillPoint >= Database.STANCE_OF_STANDING_COST)
                            {
                                SelectStanceOfStanding(ec1);
                                return 3;
                            }
                            else
                            {
                                SelectNormalAttack(ec1);
                                return 1;
                            }
                        case 3:
                            if (ec1.CurrentSkillPoint >= Database.DOUBLE_SLASH_COST)
                            {
                                SelectDoubleSlash(ec1);
                                return 4;
                            }
                            else
                            {
                                SelectNormalAttack(ec1);
                                return 1;
                            }
                        case 4:
                            if (ec1.CurrentMana >= Database.GALE_WIND_COST)
                            {
                                SelectGaleWind(ec1);
                                return 9;
                            }
                            else
                            {
                                if (ec1.CurrentSkillPoint >= Database.DOUBLE_SLASH_COST)
                                {
                                    SelectDoubleSlash(ec1);
                                    return 4;
                                }
                                else
                                {
                                    SelectNormalAttack(ec1);
                                    return 1;
                                }
                            }
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                            if (ec1.CurrentSkillPoint >= Database.CRUSHING_BLOW_COST && mc.CurrentStunning <= 0)
                            {
                                SelectCrushingBlow(ec1);
                                return 5;
                            }
                            else if (ec1.CurrentSkillPoint >= Database.DOUBLE_SLASH_COST)
                            {
                                SelectDoubleSlash(ec1);
                                return 4;
                            }
                            else
                            {
                                SelectNormalAttack(ec1);
                                return 1;
                            }
                    }
                }

                // Other
                switch (rd.Next(1, 11))
                {
                    case 1:
                        SelectNormalAttack(ec1);
                        break;
                    case 2:
                        if (ec1.CurrentSkillPoint >= Database.COUNTER_ATTACK_COST)
                        {
                            SelectCounterAttack(ec1);
                            return 2;
                        }
                        else
                        {
                            SelectNormalAttack(ec1);
                            return 1;
                        }
                    case 3:
                        if (ec1.CurrentSkillPoint >= Database.STANCE_OF_STANDING_COST)
                        {
                            SelectStanceOfStanding(ec1);
                            return 3;
                        }
                        else
                        {
                            SelectNormalAttack(ec1);
                            return 1;
                        }
                    case 4:
                        if (ec1.CurrentSkillPoint >= Database.DOUBLE_SLASH_COST)
                        {
                            SelectDoubleSlash(ec1);
                            return 4;
                        }
                        else
                        {
                            SelectNormalAttack(ec1);
                            return 1;
                        }
                    case 5:
                        if (ec1.CurrentSkillPoint >= Database.DOUBLE_SLASH_COST)
                        {
                            SelectDoubleSlash(ec1);
                            return 4;
                        }
                        else
                        {
                            SelectNormalAttack(ec1);
                            return 1;
                        }
                    case 6:
                        if (ec1.CurrentLife < (ec1.MaxLife / 2))
                        {
                            if (ec1.CurrentMana >= Database.FRESH_HEAL_COST)
                            {
                                SelectFreshHeal(ec1);
                                return 6;
                            }
                            else
                            {
                                if (ec1.CurrentSkillPoint >= Database.DOUBLE_SLASH_COST)
                                {
                                    SelectDoubleSlash(ec1);
                                    return 4;
                                }
                                else
                                {
                                    SelectNormalAttack(ec1);
                                    return 1;
                                }
                            }
                        }
                        else
                        {
                            if (ec1.CurrentSkillPoint >= Database.DOUBLE_SLASH_COST)
                            {
                                SelectDoubleSlash(ec1);
                                return 4;
                            }
                            else
                            {
                                SelectNormalAttack(ec1);
                                return 1;
                            }
                        }
                    case 7:
                        if (ec1.CurrentMana >= Database.ONE_IMMUNITY_COST)
                        {
                            SelectOneImmunity(ec1);
                            return 7;
                        }
                        else
                        {
                            if (ec1.CurrentSkillPoint >= Database.DOUBLE_SLASH_COST)
                            {
                                SelectDoubleSlash(ec1);
                                return 4;
                            }
                            else
                            {
                                SelectNormalAttack(ec1);
                                return 1;
                            }
                        }
                    case 8:
                        if (ec1.CurrentSkillPoint >= Database.DOUBLE_SLASH_COST)
                        {
                            SelectDoubleSlash(ec1);
                            return 4;
                        }
                        else
                        {
                            SelectNormalAttack(ec1);
                            return 1;
                        }
                        //SelectCarnageRush(ec1);
                    case 9:
                        if (ec1.CurrentMana >= Database.GALE_WIND_COST)
                        {
                            SelectGaleWind(ec1);
                            return 9;
                        }
                        else
                        {
                            if (ec1.CurrentSkillPoint >= Database.DOUBLE_SLASH_COST)
                            {
                                SelectDoubleSlash(ec1);
                                return 4;
                            }
                            else
                            {
                                SelectNormalAttack(ec1);
                                return 1;
                            }
                        }
                    case 10:
                        if (ec1.CurrentSkillPoint >= Database.DOUBLE_SLASH_COST)
                        {
                            SelectDoubleSlash(ec1);
                            return 4;
                        }
                        else
                        {
                            SelectNormalAttack(ec1);
                            return 1;
                        }
                        //SelectDispelMagic(ec1);
                }
            }
            else if (ec1.Name == "ダミー素振り君")
            {
                SelectStraightSmash(ec1);
                return 1;
                //SelectWordOfLife(ec1);
                //return 6;
                //SelectFreshHeal(ec1);
                //return 5;
                //SelectGaleWind(ec1);
                //return 4;
                //SelectDoubleSlash(ec1);
                //return 3;
                //SelectCarnageRush(ec1);
                //return 2;
                //SelectDefense(ec1);
                //return 1;
            }
            else
            {
                SelectNormalAttack(ec1);
                return 0;
            }
            return 0;
        }

        /// <summary>
        /// ０～１００００の値を出し一番値が小さいものが先行となります。万が一登録済みのプレイヤーと値がぶつかった場合はやり直しとなります。
        /// StanceOfFlowを使っている場合は、＋１０００００の値が加算されます。
        /// </summary>
        /// <param name="list"></param>
        /// <param name="chara"></param>
        /// <param name="enemyAgl"></param>
        /// <param name="enemyMind"></param>
        private void AddActivePlayer(SortedList<int, MainCharacter> list, MainCharacter chara, int enemyAgl, int enemyMind)
        {
            if (chara != null)
            {
                while (true)
                {
                    try
                    {
                        Random rd = new Random((int)(Environment.TickCount * DateTime.Now.Millisecond) + list.Count + 1);
                        // [警告]：Mindパラメタからブレが減るかどうかの計算が未確定です。
                        int randomSpeed = (int)((double)FirstStrikeProb(chara.Agility - enemyAgl) + normaldistr.invnormaldistribution(AP.Math.RandomReal()) * 2500.0F);
                        if (randomSpeed <= 0) randomSpeed = 0; if (randomSpeed >= 10000) randomSpeed = 10000;
                        randomSpeed = 10000 - randomSpeed; // 10000から引いて、一番小さい値が先行になるようにしています。
                        if (chara.CurrentStanceOfFlow > 0)
                        {
                            randomSpeed += 100000 + AP.Math.RandomInteger(1000);
                        }
                        if (chara.PA == PlayerAction.UseSkill)
                        {
                            if (chara.CurrentSkillName == "StanceOfEyes" || chara.CurrentSkillName == "CounterAttack" || chara.CurrentSkillName == "Negate")
                            {
                                int firstValue = 0;
                                while (true)
                                {
                                    if (!list.ContainsKey(firstValue))
                                    {
                                        randomSpeed = firstValue; // [警告]：相手も０を出してきたら、五分五分で運任せになる。再検討してください。
                                        break;
                                    }
                                    else
                                    {
                                        firstValue--;
                                    }
                                }
                            }

                            if (chara.CurrentSkillName == "StanceOfStanding")
                            {
                                //chara.CurrentStanceOfStanding = true;
                                chara.CurrentStanceOfStanding = 1; // 後編編集
                            }
                            if (chara.CurrentSkillName == "CounterAttack")
                            {
                                //chara.CurrentCounterAttack = true;
                                chara.CurrentCounterAttack = Database.INFINITY; // 後編編集
                            }
                        }
                        if (chara.Accessory != null)
                        {
                            if (chara.Accessory.Name == "レジェンド・レッドホース")
                            {
                                randomSpeed = -100;
                            }
                        }
                        //else if (chara.PA == PlayerAction.Defense)
                        //{
                        //    randomSpeed = 0;
                        //}
                        list.Add(randomSpeed, chara);
                        //this.textBox1.Text += chara.Name + ":" + randomSpeed.ToString() + "\r\n";
                        System.Threading.Thread.Sleep(1); // TickCountとMillisecondを狂わせるために入れてます。
                        break;
                    }
                    catch
                    {
                        System.Threading.Thread.Sleep(1); // 万が一、登録済みのプレイヤーと値がぶつかった時やりなおします。
                    }
                }
            }
        }
    }
}
