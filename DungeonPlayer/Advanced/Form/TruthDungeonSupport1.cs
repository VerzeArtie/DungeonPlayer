using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class TruthDungeon : MotherForm
    {
        public void MirrorLastWay()
        {
            AutoMove(0);
            AutoMove(0);
            AutoMove(0);
            JumpByMirror_TruthWay5E();
            AutoMove(2);
            System.Threading.Thread.Sleep(1000);
            AutoMove(2);
            AutoMove(2);
            AutoMove(2);
            AutoMove(2);
            AutoMove(3);
            AutoMove(2);
            AutoMove(2);
            AutoMove(2);
            AutoMove(2);
            AutoMove(3);
            AutoMove(2);
            AutoMove(2);
            AutoMove(2);
            AutoMove(3);
            AutoMove(2);
            AutoMove(3);
            AutoMove(2);
            AutoMove(3);
            AutoMove(3);
            AutoMove(3);
            AutoMove(2);
            AutoMove(3);
            AutoMove(3);
            AutoMove(3);
            AutoMove(3);
            AutoMove(3);
            AutoMove(2);
            AutoMove(3);
            AutoMove(3);
            AutoMove(3);
            AutoMove(3);
            AutoMove(3);
            AutoMove(3);
            AutoMove(3);
            AutoMove(3);
            AutoMove(3);
            AutoMove(3);
            AutoMove(3);
            AutoMove(3);
            AutoMove(2);
            AutoMove(3);
            AutoMove(3);
            AutoMove(3);
            AutoMove(3);
            AutoMove(2);
            AutoMove(3);
            AutoMove(3);
            AutoMove(3);
            AutoMove(2);
            AutoMove(3);
            AutoMove(3);
            AutoMove(3);
            AutoMove(2);
            AutoMove(3);
            AutoMove(2);
            AutoMove(3);
            AutoMove(2);
            AutoMove(2);
            AutoMove(3);
            AutoMove(2);
            AutoMove(2);
            AutoMove(2);
            AutoMove(2);
            AutoMove(3);
            AutoMove(2);
            AutoMove(2);
            AutoMove(2);
            AutoMove(2);
            AutoMove(2);
            AutoMove(2);
        }

        public void MirrorTruthWay(int wayPoint)
        {
            #region "鏡X1"
            if (wayPoint == 0)
            {
                JumpByMirror_TruthWay1A();
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(0);
                AutoMove(0);
                AutoMove(0);
                AutoMove(0);
                AutoMove(0);
                JumpByMirror_TruthWay1B();
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                JumpByMirror_TruthWay1C();
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                JumpByMirror_TruthWay1D();
                AutoMove(0);
                System.Threading.Thread.Sleep(1000);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                JumpByMirror_TurnBack();
                System.Threading.Thread.Sleep(500);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(0);
                AutoMove(0);
                AutoMove(0);
            }
            #endregion
            #region "鏡X2"
            else if (wayPoint == 1)
            {
                JumpByMirror_TruthWay2A();
                AutoMove(1);
                AutoMove(0);
                AutoMove(0);
                AutoMove(0);
                AutoMove(1);
                AutoMove(1);
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                AutoMove(1);
                AutoMove(1);
                AutoMove(0);
                AutoMove(0);
                AutoMove(0);
                AutoMove(0);
                AutoMove(1);
                AutoMove(1);
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                JumpByMirror_TruthWay2B();
                AutoMove(2);
                AutoMove(0);
                AutoMove(0);
                AutoMove(1);
                AutoMove(1);
                AutoMove(0);
                AutoMove(0);
                AutoMove(0);
                AutoMove(0);
                AutoMove(0);
                AutoMove(0);
                AutoMove(0);
                AutoMove(2);
                JumpByMirror_TruthWay2C();
                AutoMove(2);
                AutoMove(3);
                AutoMove(2);
                JumpByMirror_TruthWay2D();
                AutoMove(1);
                AutoMove(1);
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(0);
                AutoMove(0);
                System.Threading.Thread.Sleep(1000);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                JumpByMirror_TurnBack();
                System.Threading.Thread.Sleep(500);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(0);
                AutoMove(0);
                AutoMove(0);
            }
            #endregion
            #region "鏡X3"
            else if (wayPoint == 2)
            {
                JumpByMirror_TruthWay3A();
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                JumpByMirror_TruthWay3B();
                AutoMove(3);
                AutoMove(3);
                AutoMove(2);
                AutoMove(2);
                AutoMove(0);
                AutoMove(0);
                AutoMove(0);
                AutoMove(0);
                AutoMove(1);
                AutoMove(0);
                AutoMove(0);
                JumpByMirror_TruthWay3C();
                AutoMove(2);
                AutoMove(0);
                AutoMove(0);
                AutoMove(1);
                AutoMove(1);
                AutoMove(0);
                AutoMove(0);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                JumpByMirror_TruthWay3D();
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                AutoMove(0);
                AutoMove(0);
                System.Threading.Thread.Sleep(1000);
                AutoMove(0);
                AutoMove(2);
                AutoMove(0);
                JumpByMirror_TurnBack();
                System.Threading.Thread.Sleep(500);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(0);
                AutoMove(0);
                AutoMove(0);
            }
            #endregion
            #region "鏡X4"
            else if (wayPoint == 3)
            {
                JumpByMirror_TruthWay4A();
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(3);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                JumpByMirror_TruthWay4B();
                AutoMove(2);
                AutoMove(2);
                JumpByMirror_TruthWay4C();
                AutoMove(0);
                AutoMove(0);
                AutoMove(0);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                JumpByMirror_TruthWay4D();
                AutoMove(0);
                AutoMove(0);
                AutoMove(1);
                System.Threading.Thread.Sleep(1000);
                AutoMove(0);
                AutoMove(0);
                AutoMove(0);
                AutoMove(0);
                AutoMove(2);
                AutoMove(0);
                AutoMove(0);
                AutoMove(0);
                AutoMove(0);
                AutoMove(0);
                AutoMove(0);
                AutoMove(2);
                AutoMove(2);
                JumpByMirror_TurnBack();
                System.Threading.Thread.Sleep(500);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(0);
                AutoMove(0);
                AutoMove(0);
            }
            #endregion
            #region "鏡X5"
            else if (wayPoint == 4)
            {
                JumpByMirror_TruthWay5A();
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                JumpByMirror_TruthWay5B();
                AutoMove(0);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                JumpByMirror_TruthWay5C();
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                JumpByMirror_TruthWay5D();
                AutoMove(0);
                System.Threading.Thread.Sleep(500);
                AutoMove(0);
                AutoMove(0);
                AutoMove(0);
            }
            #endregion
        }

        public void MirrorWay(int wayLine, int anotherWayLine)
        {
            // 万が一、設定が無い場合は150, 138, 96, 130, 124で進める事にする。
            if (wayLine < 95 || wayLine > 150)
            {
                if (anotherWayLine == 1) { wayLine = 150; }
                else if (anotherWayLine == 2) { wayLine = 138; }
                else if (anotherWayLine == 3) { wayLine = 96; }
                else if (anotherWayLine == 4) { wayLine = 130; }
                else if (anotherWayLine == 5) { wayLine = 124; } // 正解
                else if (anotherWayLine == 6) { wayLine = 144; } // 原点解
                else { wayLine = 124; } // それ以外の万が一が来た場合はanotherwayline5と同じにする。
            }

            #region "鏡38"
            if (95 <= wayLine && wayLine <= 116)
            {
                AutoMove(1);
                AutoMove(1);
                AutoMove(0);
                AutoMove(0);
                JumpByMirror_2_38();
                AutoMove(3);
                AutoMove(3);
                AutoMove(2);
                if (95 <= wayLine && wayLine <= 104)
                {
                    AutoMove(3);
                    AutoMove(3);
                    AutoMove(3);
                    JumpByMirror_2_41();
                    AutoMove(3);
                    AutoMove(3);
                    AutoMove(3);
                    if (wayLine == 95 || wayLine == 96 || wayLine == 97)
                    {
                        AutoMove(3);
                        JumpByMirror_2_47();
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(3);
                        if (wayLine == 95)
                        {
                            AutoMove(1);
                            AutoMove(1);
                            AutoMove(3);
                            AutoMove(3);
                            JumpByMirror_2_59();
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(3);
                        }
                        else if (wayLine == 96)
                        {
                            AutoMove(3);
                            AutoMove(3);
                            JumpByMirror_2_60();
                            AutoMove(1);
                            AutoMove(1);
                            AutoMove(1);
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(2);
                        }
                        else if (wayLine == 97)
                        {
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(3);
                            AutoMove(3);
                            JumpByMirror_2_61();
                            AutoMove(1);
                            AutoMove(1);
                            AutoMove(1);
                            AutoMove(1);
                            AutoMove(1);
                            AutoMove(1);
                            AutoMove(1);
                            AutoMove(3);
                            AutoMove(3);
                        }
                    }
                    else if (wayLine == 98 || wayLine == 99 || wayLine == 100
                          || wayLine == 101 || wayLine == 102 || wayLine == 103 || wayLine == 104)
                    {
                        AutoMove(2);
                        AutoMove(2);
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        JumpByMirror_2_48();
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(2);
                        if (wayLine == 98 || wayLine == 99 || wayLine == 100)
                        {
                            AutoMove(0);
                            AutoMove(0);
                            JumpByMirror_2_62();
                            AutoMove(0);
                            AutoMove(0);
                            AutoMove(0);
                            AutoMove(0);
                            if (wayLine == 98) { AutoMove(1); AutoMove(1); }
                            else if (wayLine == 99) { /* なにもなし */ }
                            else if (wayLine == 100) { AutoMove(2); AutoMove(2); }
                            AutoMove(0);
                            AutoMove(0);
                        }
                        else if (wayLine == 101 || wayLine == 102 || wayLine == 103 || wayLine == 104)
                        {
                            AutoMove(2);
                            AutoMove(0);
                            AutoMove(0);
                            JumpByMirror_2_63();
                            AutoMove(0);
                            AutoMove(0);
                            AutoMove(1);
                            AutoMove(1);
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(1);
                            AutoMove(1);
                            if (wayLine == 101)
                            {
                                AutoMove(1);
                                AutoMove(1);
                                AutoMove(0);
                                AutoMove(0);
                            }
                            else if (wayLine == 102 || wayLine == 103 || wayLine == 104)
                            {
                                AutoMove(0);
                                AutoMove(0);
                                AutoMove(0);
                                AutoMove(0);
                                AutoMove(0);
                                if (wayLine == 102)
                                {
                                    AutoMove(2);
                                    AutoMove(2);
                                    AutoMove(2);
                                    AutoMove(0);
                                }
                                else if (wayLine == 103 || wayLine == 104)
                                {
                                    AutoMove(0);
                                    AutoMove(0);
                                    AutoMove(0);
                                    AutoMove(0);
                                    AutoMove(0);
                                    AutoMove(1);
                                    AutoMove(0);
                                    AutoMove(1);
                                    AutoMove(1);
                                    AutoMove(1);
                                    AutoMove(1);
                                    AutoMove(1);
                                    AutoMove(1);
                                    if (wayLine == 103)
                                    {
                                        AutoMove(3);
                                        AutoMove(3);
                                        AutoMove(3);
                                        AutoMove(3);
                                        AutoMove(3);
                                    }
                                    else// if (truthWay1 == 104) // 万が一を考えた場合の緊急回避として、ポイントミスはelseで始末するべきである
                                    {
                                        AutoMove(0);
                                        AutoMove(0);
                                        AutoMove(0);
                                    }
                                }
                            }
                        }
                    }
                }
                else if (105 <= wayLine && wayLine <= 108)
                {
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(3);
                    AutoMove(3);
                    AutoMove(3);
                    JumpByMirror_2_42();
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(0);
                    AutoMove(0);
                    AutoMove(0);
                    AutoMove(0);
                    AutoMove(0);
                    AutoMove(0);
                    AutoMove(1);
                    AutoMove(1);
                    AutoMove(1);
                    AutoMove(1);
                    AutoMove(1);
                    AutoMove(0);
                    AutoMove(0);
                    AutoMove(0);
                    AutoMove(0);
                    JumpByMirror_2_49();
                    AutoMove(1);
                    AutoMove(0);
                    if (wayLine == 105)
                    {
                        AutoMove(0);
                        AutoMove(2);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                        JumpByMirror_2_64();
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(3);
                    }
                    else if (wayLine == 106 || wayLine == 107 || wayLine == 108)
                    {
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        JumpByMirror_2_65();
                        AutoMove(1);
                        AutoMove(1);
                        if (wayLine == 106) { AutoMove(0); AutoMove(0); AutoMove(0); AutoMove(0); }
                        else if (wayLine == 107) { AutoMove(0); AutoMove(0); }
                        else if (wayLine == 108) { /*何もしない*/ }
                        AutoMove(1);
                        AutoMove(1);
                    }
                }
                else if (109 <= wayLine && wayLine <= 116)
                {
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(3);
                    AutoMove(3);
                    AutoMove(3);
                    JumpByMirror_2_43();
                    AutoMove(2);
                    AutoMove(2);
                    if (109 <= wayLine && wayLine <= 111)
                    {
                        AutoMove(2);
                        AutoMove(2);
                        JumpByMirror_2_50();
                        AutoMove(1);
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(3);
                        JumpByMirror_2_66();
                        AutoMove(2);
                        AutoMove(2);
                        AutoMove(2);
                        AutoMove(2);
                        AutoMove(2);
                        if (wayLine == 109) { AutoMove(0); AutoMove(0); }
                        else if (wayLine == 110) { /*何もしない*/ }
                        else if (wayLine == 111) { AutoMove(3); AutoMove(3); }
                        AutoMove(2);
                        AutoMove(2);
                    }
                    else if (112 <= wayLine && wayLine <= 116)
                    {
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(3);
                        JumpByMirror_2_51();
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                        if (wayLine == 112)
                        {
                            AutoMove(1);
                            AutoMove(1);
                            AutoMove(1);
                            AutoMove(0);
                            AutoMove(0);
                            AutoMove(0);
                            JumpByMirror_2_67();
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(1);
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                        }
                        else if (wayLine == 113)
                        {
                            AutoMove(1);
                            AutoMove(0);
                            AutoMove(0);
                            AutoMove(0);
                            JumpByMirror_2_68();
                            AutoMove(1);
                            AutoMove(1);
                        }
                        else if (wayLine == 114 || wayLine == 115 || wayLine == 116)
                        {
                            AutoMove(2);
                            AutoMove(0);
                            AutoMove(0);
                            AutoMove(0);
                            JumpByMirror_2_69();
                            AutoMove(0);
                            AutoMove(0);
                            AutoMove(0);
                            AutoMove(0);
                            if (wayLine == 114) { AutoMove(1); }
                            else if (wayLine == 115) { AutoMove(2); }
                            else if (wayLine == 116) { AutoMove(2); AutoMove(2); AutoMove(2); }
                            AutoMove(0);
                            AutoMove(0);
                        }
                    }
                }
            }
            #endregion
            #region "鏡39"
            else if (117 <= wayLine && wayLine <= 133)
            {
                AutoMove(0);
                AutoMove(0);
                JumpByMirror_2_39();
                AutoMove(2);
                AutoMove(2);
                AutoMove(3);
                AutoMove(3);
                AutoMove(3);
                if (117 <= wayLine && wayLine <= 123)
                {
                    AutoMove(1);
                    AutoMove(1);
                    AutoMove(3);
                    AutoMove(3);
                    JumpByMirror_2_44();
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(2);
                    if (117 <= wayLine && wayLine <= 121)
                    {
                        AutoMove(0);
                        AutoMove(0);
                        JumpByMirror_2_52();
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        if (wayLine == 117 || wayLine == 118 || wayLine == 119 || wayLine == 120)
                        {
                            AutoMove(0);
                            AutoMove(0);
                            AutoMove(2);
                            JumpByMirror_2_70();
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            if (wayLine == 117) { AutoMove(0); AutoMove(0); AutoMove(0); }
                            else if (wayLine == 118) { AutoMove(0); }
                            else if (wayLine == 119) { AutoMove(3); }
                            else if (wayLine == 120) { AutoMove(3); AutoMove(3); AutoMove(3); }
                            AutoMove(2);
                        }
                        else if (wayLine == 121)
                        {
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            JumpByMirror_2_71();
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(3);
                            AutoMove(2);
                            AutoMove(3);
                            AutoMove(2);
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(2);
                        }
                    }
                    else if (wayLine == 122 || wayLine == 123)
                    {
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(3);
                        JumpByMirror_2_53();
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(1);
                        JumpByMirror_2_72();
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(3);
                        if (wayLine == 122) { AutoMove(1); }
                        else if (wayLine == 123) { AutoMove(2); }
                        AutoMove(3);
                        AutoMove(3);
                    }
                }
                else if (124 <= wayLine && wayLine <= 133)
                {
                    AutoMove(3);
                    AutoMove(3);
                    JumpByMirror_2_45();
                    AutoMove(1);
                    AutoMove(1);
                    AutoMove(1);
                    if (124 <= wayLine && wayLine <= 129)
                    {
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(0);
                        AutoMove(0);
                        JumpByMirror_2_54();
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(1);
                        AutoMove(0);
                        AutoMove(1);
                        AutoMove(1);
                        if (wayLine == 124)
                        {
                            AutoMove(1);
                            AutoMove(1);
                            AutoMove(1);
                            AutoMove(1);
                            AutoMove(0);
                            AutoMove(0);
                            JumpByMirror_2_73();
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(1);
                            AutoMove(1);
                            AutoMove(1);
                            AutoMove(1);
                        }
                        else if (wayLine == 125)
                        {
                            AutoMove(1);
                            AutoMove(1);
                            AutoMove(0);
                            AutoMove(0);
                            JumpByMirror_2_74();
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(3);
                            AutoMove(2);
                            AutoMove(3);
                            AutoMove(2);
                            AutoMove(3);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(3);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                        }
                        else if (wayLine == 126 || wayLine == 127 || wayLine == 128 || wayLine == 129)
                        {
                            AutoMove(0);
                            AutoMove(0);
                            JumpByMirror_2_75();
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(3);
                            AutoMove(3);
                            if (wayLine == 126)
                            {
                                AutoMove(3);
                                AutoMove(3);
                                AutoMove(3);
                                AutoMove(3);
                                AutoMove(3);
                                AutoMove(3);
                                AutoMove(3);
                                AutoMove(3);
                            }
                            else
                            {
                                AutoMove(1);
                                AutoMove(1);
                                AutoMove(1);
                                AutoMove(1);
                                AutoMove(1);
                                AutoMove(1);
                                AutoMove(1);
                                AutoMove(1);
                                AutoMove(1);
                                AutoMove(1);
                                AutoMove(1);
                                if (wayLine == 127)
                                {
                                    AutoMove(1);
                                    AutoMove(1);
                                    AutoMove(3);
                                    AutoMove(3);
                                    AutoMove(3);
                                    AutoMove(3);
                                    AutoMove(3);
                                    AutoMove(3);
                                }
                                else
                                {
                                    AutoMove(3);
                                    AutoMove(3);
                                    AutoMove(3);
                                    if (wayLine == 128)
                                    {
                                        AutoMove(2);
                                        AutoMove(2);
                                        AutoMove(2);
                                    }
                                    else
                                    {
                                        AutoMove(3);
                                        AutoMove(3);
                                        AutoMove(3);
                                        AutoMove(3);
                                        AutoMove(3);
                                        AutoMove(3);
                                        AutoMove(1);
                                        AutoMove(3);
                                        AutoMove(3);
                                        AutoMove(3);
                                        AutoMove(3);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        AutoMove(0);
                        AutoMove(0);
                        JumpByMirror_2_55();
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(3);
                        if (130 <= wayLine && wayLine <= 133)
                        {
                            if (wayLine == 133)
                            {
                                AutoMove(2);
                                AutoMove(3);
                                AutoMove(3);
                                JumpByMirror_2_77();
                                AutoMove(3);
                                AutoMove(3);
                                AutoMove(3);
                                AutoMove(3);
                                AutoMove(1);
                                AutoMove(1);
                                AutoMove(0);
                                AutoMove(0);
                                AutoMove(0);
                                AutoMove(0);
                                AutoMove(0);
                                AutoMove(1);
                            }
                            else if (130 <= wayLine && wayLine <= 132)
                            {
                                AutoMove(1);
                                AutoMove(3);
                                AutoMove(3);
                                JumpByMirror_2_76();
                                AutoMove(3);
                                AutoMove(3);
                                AutoMove(3);
                                AutoMove(3);
                                AutoMove(3);
                                AutoMove(3);
                                AutoMove(3);
                                AutoMove(3);
                                AutoMove(3);
                                AutoMove(3);
                                AutoMove(3);
                                AutoMove(3);
                                AutoMove(3);
                                AutoMove(3);
                                AutoMove(1);
                                AutoMove(1);
                                if (wayLine == 130)
                                {
                                    AutoMove(1);
                                    AutoMove(1);
                                    AutoMove(1);
                                    AutoMove(1);
                                    AutoMove(0);
                                    AutoMove(0);
                                    AutoMove(0);
                                }
                                else if (wayLine == 131)
                                {
                                    AutoMove(1);
                                    AutoMove(1);
                                    AutoMove(0);
                                    AutoMove(0);
                                    AutoMove(0);
                                }
                                else if (wayLine == 132)
                                {
                                    AutoMove(0);
                                    AutoMove(0);
                                    AutoMove(0);
                                }
                            }
                        }
                    }
                }
            }
            #endregion
            #region "鏡40"
            else if (134 <= wayLine && wayLine <= 150)
            {
                AutoMove(2);
                AutoMove(2);
                AutoMove(0);
                AutoMove(0);
                JumpByMirror_2_40();
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(2);
                AutoMove(3);
                AutoMove(3);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                AutoMove(1);
                JumpByMirror_2_46();
                AutoMove(3);
                if (134 <= wayLine && wayLine <= 138)
                {
                    AutoMove(1);
                    AutoMove(1);
                    AutoMove(1);
                    AutoMove(1);
                    AutoMove(1);
                    AutoMove(1);
                    AutoMove(1);
                    AutoMove(1);
                    AutoMove(1);
                    AutoMove(1);
                    AutoMove(1);
                    AutoMove(1);
                    AutoMove(1);
                    JumpByMirror_2_56();
                    AutoMove(0);
                    AutoMove(0);
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(3);
                    AutoMove(3);
                    AutoMove(3);
                    AutoMove(3);
                    AutoMove(1);
                    AutoMove(1);
                    if (wayLine == 134 || wayLine == 135 || wayLine == 136 || wayLine == 137)
                    {
                        AutoMove(0);
                        AutoMove(0);
                        JumpByMirror_2_78();
                        AutoMove(2);
                        AutoMove(2);
                        AutoMove(2);
                        AutoMove(2);
                        AutoMove(2);
                        AutoMove(2);
                        if (wayLine == 134) { AutoMove(0); AutoMove(0); AutoMove(0); }
                        else if (wayLine == 135) { AutoMove(0); AutoMove(0); AutoMove(2); }
                        else if (wayLine == 136) { AutoMove(2); AutoMove(2); AutoMove(0); }
                        else if (wayLine == 137) { AutoMove(2); AutoMove(2); AutoMove(2); }
                    }
                    else if (wayLine == 138)
                    {
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(2);
                        AutoMove(2);
                        AutoMove(3);
                        AutoMove(3);
                        JumpByMirror_2_79();
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                    }
                }
                else if (wayLine == 139 || wayLine == 140 || wayLine == 141)
                {
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(2);
                    JumpByMirror_2_57();
                    AutoMove(0);
                    AutoMove(0);
                    JumpByMirror_2_80();
                    AutoMove(0);
                    if (wayLine == 139) { AutoMove(2); }
                    else if (wayLine == 140) { AutoMove(0); AutoMove(0); AutoMove(2); }
                    else if (wayLine == 141) { AutoMove(0); AutoMove(0); AutoMove(0); }
                }
                else if (142 <= wayLine && wayLine <= 150)
                {
                    AutoMove(3);
                    AutoMove(3);
                    AutoMove(1);
                    AutoMove(1);
                    AutoMove(1);
                    AutoMove(1);
                    AutoMove(1);
                    AutoMove(1);
                    JumpByMirror_2_58();
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(2);
                    AutoMove(2);
                    if (wayLine == 142 || wayLine == 143)
                    {
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(2);
                        AutoMove(2);
                        AutoMove(2);
                        AutoMove(2);
                        AutoMove(2);
                        AutoMove(2);
                        JumpByMirror_2_81();
                        AutoMove(1);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(2);
                        AutoMove(2);
                        AutoMove(2);
                        AutoMove(2);
                        if (wayLine == 142) { AutoMove(0); }
                        else if (wayLine == 143) { AutoMove(3); }
                        AutoMove(2);
                    }
                    else if (wayLine == 144)
                    {
                        AutoMove(0);
                        AutoMove(2);
                        AutoMove(2);
                        AutoMove(2);
                        AutoMove(2);
                        AutoMove(2);
                        JumpByMirror_2_82();
                        AutoMove(3);
                        AutoMove(3);
                    }
                    else if (wayLine == 145 || wayLine == 146)
                    {
                        AutoMove(3);
                        AutoMove(2);
                        AutoMove(2);
                        AutoMove(2);
                        AutoMove(2);
                        JumpByMirror_2_83();
                        AutoMove(0);
                        AutoMove(0);
                        if (wayLine == 145) { AutoMove(1); }
                        else if (wayLine == 146) { /* 何もしない */ }
                        AutoMove(0);
                        AutoMove(0);
                    }
                    else if (wayLine == 147 || wayLine == 148 || wayLine == 149 || wayLine == 150)
                    {
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(3);
                        AutoMove(2);
                        AutoMove(2);
                        AutoMove(2);
                        JumpByMirror_2_84();
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(1);
                        AutoMove(0);
                        AutoMove(0);
                        AutoMove(0);
                        if (wayLine == 147)
                        {
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                            AutoMove(2);
                        }
                        else
                        {
                            AutoMove(0);
                            AutoMove(0);
                            AutoMove(0);
                            if (wayLine == 148)
                            {
                                AutoMove(2);
                                AutoMove(2);
                                AutoMove(0);
                                AutoMove(0);
                            }
                            else
                            {
                                AutoMove(0);
                                AutoMove(0);
                                AutoMove(0);
                                AutoMove(0);
                                AutoMove(0);
                                if (wayLine == 149)
                                {
                                    AutoMove(2);
                                    AutoMove(2);
                                    AutoMove(3);
                                    AutoMove(3);
                                }
                                else if (wayLine == 150)
                                {
                                    AutoMove(0);
                                    AutoMove(0);
                                    AutoMove(2);
                                    AutoMove(2);
                                    AutoMove(2);
                                    AutoMove(2);
                                }
                            }
                        }

                    }
                }
            }
            #endregion
        }

        #region "３F鏡リスト"
        public void JumpByMirror_Recollection4() { JumpByMirror(1, 1); }
        public void JumpByMirror_ZeroWay() { JumpByMirror(12, 37); }
        public void JumpByMirror_Recollection3() { JumpByMirror(10, 1); }
        public void JumpByMirror_TurnBack() { JumpByMirror(19, 20); }
        public void JumpByMirror_2_38() { JumpByMirror(29, 40, 28, 18); UpdateUnknownTileArea3_Area2(); }
        public void JumpByMirror_2_39() { JumpByMirror(4, 45); UpdateUnknownTileArea3_Area22(); }
        public void JumpByMirror_2_40() { JumpByMirror(30, 21); UpdateUnknownTileArea3_Area37(); }
        public void JumpByMirror_2_41() { JumpByMirror(7, 33, 21, 0); UpdateUnknownTileArea3_Area3(); }
        public void JumpByMirror_2_42() { JumpByMirror(28, 43); UpdateUnknownTileArea3_Area11(); }
        public void JumpByMirror_2_43() { JumpByMirror(22, 21); UpdateUnknownTileArea3_Area15(); }
        public void JumpByMirror_2_44() { JumpByMirror(11, 45); UpdateUnknownTileArea3_Area23(); }
        public void JumpByMirror_2_45() { JumpByMirror(36, 36); UpdateUnknownTileArea3_Area29(); }
        public void JumpByMirror_2_46() { JumpByMirror(0, 40); UpdateUnknownTileArea3_Area38(); }
        public void JumpByMirror_2_47() { JumpByMirror(23, 29); UpdateUnknownTileArea3_Area4(); }
        public void JumpByMirror_2_48() { JumpByMirror(24, 42); UpdateUnknownTileArea3_Area8(); }
        public void JumpByMirror_2_49() { JumpByMirror(38, 38); UpdateUnknownTileArea3_Area12(); }
        public void JumpByMirror_2_50() { JumpByMirror(16, 40); UpdateUnknownTileArea3_Area16(); }
        public void JumpByMirror_2_51() { JumpByMirror(36, 53); UpdateUnknownTileArea3_Area18(); }
        public void JumpByMirror_2_52() { JumpByMirror(13, 30); UpdateUnknownTileArea3_Area24(); }
        public void JumpByMirror_2_53() { JumpByMirror(30, 41); UpdateUnknownTileArea3_Area27(); }
        public void JumpByMirror_2_54() { JumpByMirror(11, 32); UpdateUnknownTileArea3_Area30(); }
        public void JumpByMirror_2_55() { JumpByMirror(4, 40); UpdateUnknownTileArea3_Area34(); }
        public void JumpByMirror_2_56() { JumpByMirror(16, 32); UpdateUnknownTileArea3_Area39(); }
        public void JumpByMirror_2_57() { JumpByMirror(20, 48); UpdateUnknownTileArea3_Area42(); }
        public void JumpByMirror_2_58() { JumpByMirror(22, 26); UpdateUnknownTileArea3_Area44(); }
        public void JumpByMirror_2_59() { JumpByMirror(20, 31); UpdateUnknownTileArea3_Area5(); }
        public void JumpByMirror_2_60() { JumpByMirror(4, 52); UpdateUnknownTileArea3_Area6(); }
        public void JumpByMirror_2_61() { JumpByMirror(12, 52); UpdateUnknownTileArea3_Area7(); }
        public void JumpByMirror_2_62() { JumpByMirror(36, 29); UpdateUnknownTileArea3_Area9(); }
        public void JumpByMirror_2_63() { JumpByMirror(17, 35); UpdateUnknownTileArea3_Area10(); }
        public void JumpByMirror_2_64() { JumpByMirror(3, 42); UpdateUnknownTileArea3_Area13(); }
        public void JumpByMirror_2_65() { JumpByMirror(38, 53); UpdateUnknownTileArea3_Area14(); }
        public void JumpByMirror_2_66() { JumpByMirror(33, 30); UpdateUnknownTileArea3_Area17(); }
        public void JumpByMirror_2_67() { JumpByMirror(4, 35); UpdateUnknownTileArea3_Area19(); }
        public void JumpByMirror_2_68() { JumpByMirror(17, 23); UpdateUnknownTileArea3_Area20(); }
        public void JumpByMirror_2_69() { JumpByMirror(29, 25); UpdateUnknownTileArea3_Area21(); }
        public void JumpByMirror_2_70() { JumpByMirror(29, 26); UpdateUnknownTileArea3_Area25(); }
        public void JumpByMirror_2_71() { JumpByMirror(4, 30); UpdateUnknownTileArea3_Area26(); }
        public void JumpByMirror_2_72() { JumpByMirror(15, 51); UpdateUnknownTileArea3_Area28(); }
        public void JumpByMirror_2_73() { JumpByMirror(22, 31); UpdateUnknownTileArea3_Area31(); }
        public void JumpByMirror_2_74() { JumpByMirror(33, 39); UpdateUnknownTileArea3_Area32(); }
        public void JumpByMirror_2_75() { JumpByMirror(0, 41); UpdateUnknownTileArea3_Area33(); }
        public void JumpByMirror_2_76() { JumpByMirror(11, 54); UpdateUnknownTileArea3_Area35(); }
        public void JumpByMirror_2_77() { JumpByMirror(26, 37); UpdateUnknownTileArea3_Area36(); }
        public void JumpByMirror_2_78() { JumpByMirror(20, 20); UpdateUnknownTileArea3_Area40(); }
        public void JumpByMirror_2_79() { JumpByMirror(20, 45); UpdateUnknownTileArea3_Area41(); }
        public void JumpByMirror_2_80() { JumpByMirror(9, 21); UpdateUnknownTileArea3_Area43(); }
        public void JumpByMirror_2_81() { JumpByMirror(28, 40); UpdateUnknownTileArea3_Area45(); }
        public void JumpByMirror_2_82() { JumpByMirror(22, 53); UpdateUnknownTileArea3_Area46(); }
        public void JumpByMirror_2_83() { JumpByMirror(10, 52); UpdateUnknownTileArea3_Area47(); }
        public void JumpByMirror_2_84() { JumpByMirror(34, 27); UpdateUnknownTileArea3_Area48(); }
        public void JumpByMirror_TruthWay1A() { JumpByMirror(36, 43); UpdateUnknownTileArea3_Area49(); }
        public void JumpByMirror_TruthWay1B() { JumpByMirror(2, 31); UpdateUnknownTileArea3_Area50(); }
        public void JumpByMirror_TruthWay1C() { JumpByMirror(32, 54); UpdateUnknownTileArea3_Area51(); }
        public void JumpByMirror_TruthWay1D() { JumpByMirror(22, 47); UpdateUnknownTileArea3_Area52(); }
        public void JumpByMirror_TruthWay2A() { JumpByMirror(17, 53); UpdateUnknownTileArea3_Area53(); }
        public void JumpByMirror_TruthWay2B() { JumpByMirror(12, 21); UpdateUnknownTileArea3_Area54(); }
        public void JumpByMirror_TruthWay2C() { JumpByMirror(37, 38); UpdateUnknownTileArea3_Area55(); }
        public void JumpByMirror_TruthWay2D() { JumpByMirror(2, 25); UpdateUnknownTileArea3_Area56(); }
        public void JumpByMirror_TruthWay3A() { JumpByMirror(26, 24); UpdateUnknownTileArea3_Area57(); }
        public void JumpByMirror_TruthWay3B() { JumpByMirror(17, 43); UpdateUnknownTileArea3_Area58(); }
        public void JumpByMirror_TruthWay3C() { JumpByMirror(4, 21); UpdateUnknownTileArea3_Area59(); }
        public void JumpByMirror_TruthWay3D() { JumpByMirror(39, 35); UpdateUnknownTileArea3_Area60(); }
        public void JumpByMirror_TruthWay4A() { JumpByMirror(38, 21); UpdateUnknownTileArea3_Area61(); }
        public void JumpByMirror_TruthWay4B() { JumpByMirror(33, 49); UpdateUnknownTileArea3_Area62(); }
        public void JumpByMirror_TruthWay4C() { JumpByMirror(16, 49); UpdateUnknownTileArea3_Area63(); }
        public void JumpByMirror_TruthWay4D() { JumpByMirror(18, 44); UpdateUnknownTileArea3_Area64(); }
        public void JumpByMirror_TruthWay5A() { JumpByMirror(24, 21); UpdateUnknownTileArea3_Area65(); }
        public void JumpByMirror_TruthWay5B() { JumpByMirror(27, 54); UpdateUnknownTileArea3_Area66(); }
        public void JumpByMirror_TruthWay5C() { JumpByMirror(31, 48); UpdateUnknownTileArea3_Area67(); }
        public void JumpByMirror_TruthWay5D() { JumpByMirror(29, 1); }
        public void JumpByMirror_TruthWay5E() { JumpByMirror(1, 21); }
        public void JumpByMirror_1_End()
        {
            JumpByMirror(19, 18, 6, 8);
        }
        #endregion

        public void Story_TruthRecollection4_5()
        {
            this.BackColor = Color.Black;
            mainMessage.BackColor = Color.White;
            this.backgroundData = null;
            this.Invalidate();

            UpdateMainMessageWithBlack("　　　【その瞬間、アインの脳裏に激しい激痛が襲った！周囲の感覚が麻痺する！！】");

            UpdateMainMessageWithBlack("　　アイン：・・・代償？");

            UpdateMainMessageWithBlack("　　ファラ：ええ、あのダンジョンにて願いをかなえるためには");

            UpdateMainMessageWithBlack("　　ファラ：代償が必要不可欠なの。");

            UpdateMainMessageWithBlack("　　アイン：・・・　ッハハハ　・・・");

            UpdateMainMessageWithBlack("　　アイン：悪い冗談はやめてくれよ、ファラ様。");

            UpdateMainMessageWithBlack("　　アイン：まさか、神様がいるとでも言うつもりなのか？");

            UpdateMainMessageWithBlack("　　ファラ：・・・　・・・　・・・");

            UpdateMainMessageWithBlack("　　ファラ：そうね、そんな所よ。");

            UpdateMainMessageWithBlack("　　ファラ：こう答えちゃったら、アインさんはお怒りになるかも知れないわね。");

            UpdateMainMessageWithBlack("　　アイン：当たり前だろ。");

            UpdateMainMessageWithBlack("    アイン：居るわけねえんだって、神様なんていう類のもんは。");

            UpdateMainMessageWithBlack("　　アイン：もう少しまともに解説してくれ。");

            UpdateMainMessageWithBlack("　　ファラ：・・・　・・・　・・・");

            UpdateMainMessageWithBlack("　　ファラ：ごめんなさい。");

            UpdateMainMessageWithBlack("　　ファラ：どう表現していいのか分からないの。");
            
            UpdateMainMessageWithBlack("　　アイン：代償ってのは、どういう類の事を指してるんだ？");

            UpdateMainMessageWithBlack("　　アイン：まさか、人の死とかいうんじゃないだろうな。");

            UpdateMainMessageWithBlack("　　ファラ：・・・　・・・　・・・");

            UpdateMainMessageWithBlack("　　アイン：い、いやいやいや。待ってくれよ。");

            UpdateMainMessageWithBlack("　　アイン：エレマさんが死んだのは事故なんだろ？");

            UpdateMainMessageWithBlack("　　アイン：FiveSeeker達がダンジョンに挑んだのとは関係の無い話だ。");

            UpdateMainMessageWithBlack("　　アイン：不幸な事故だった、そう考えるのが妥当な線だろ。");

            UpdateMainMessageWithBlack("　　アイン：らしくないぜ、聡明なファラ様がそんなオカルト的な考え方するなんてさ。");

            UpdateMainMessageWithBlack("　　ファラ：ウフフ、やっぱりおかしいわよね、こんな言い方じゃ");

            UpdateMainMessageWithBlack("　　ファラ：アインさん。");

            UpdateMainMessageWithBlack("　　ファラ：エレマはね、アーティの事を好いていたの。");

            UpdateMainMessageWithBlack("　　ファラ：私がジョルジュを好いている以上に・・・");

            UpdateMainMessageWithBlack("　　ファラ：アーティはもともと親が居なくて育ったの。");

            UpdateMainMessageWithBlack("　　ファラ：そんな中で、アーティが奇跡的に普通の人として育ったのは");

            UpdateMainMessageWithBlack("　　ファラ：彼女がいたからなのよ。");

            UpdateMainMessageWithBlack("　　ファラ：アーティは彼女を");

            UpdateMainMessageWithBlack("　　ファラ：彼女はアーティを");

            UpdateMainMessageWithBlack("　　ファラ：きっと二人は出会ったころからずっと・・・");

            UpdateMainMessageWithBlack("　　アイン：ちょ、ちょっと待ってくれ、いきなりワケわかんない話になったぞ。");

            UpdateMainMessageWithBlack("　　アイン：それが今の話とどう関係が？");

            UpdateMainMessageWithBlack("　　ファラ：くじ引きが終わった後、私は一旦撤回したのよ！！");

            UpdateMainMessageWithBlack("　　アイン：なっ・・・　・・・　・・・");

            UpdateMainMessageWithBlack("　　ファラ：こんな決め方じゃダメ、エレマじゃなくて私が残ると言ったのよ。");

            UpdateMainMessageWithBlack("　　ファラ：それでも、エレマはきかなくて・・・");

            UpdateMainMessageWithBlack("　　ファラ：私は直観的にあの時わかったの。");

            UpdateMainMessageWithBlack("　　ファラ：絶対に良くない事が起こる。");

            UpdateMainMessageWithBlack("　　ファラ：止めないと・・・止めないと駄目だって！！！");

            UpdateMainMessageWithBlack("　　アイン：わ、わかった。分かった、良いから落ち着いてくれ。");

            UpdateMainMessageWithBlack("　　ファラ：・・・　・・・　・・・");

            UpdateMainMessageWithBlack("　　ファラ：どうしても");

            UpdateMainMessageWithBlack("　　ファラ：どう説得しても");

            UpdateMainMessageWithBlack("　　ファラ：どうやり直しても");

            UpdateMainMessageWithBlack("　　ファラ：止められなかったの");

            UpdateMainMessageWithBlack("　　ファラ：ダダをこねる私に対してエレマがね、変な事言い出したの。");

            UpdateMainMessageWithBlack("　　（　エレマ：『　ファラ、もしも私が行く事になったら　』　）");

            UpdateMainMessageWithBlack("　　（　エレマ：『　きっと・・・結果が反対になっちゃうの　』　）");

            UpdateMainMessageWithBlack("　　（　エレマ：『　わたし・・・それだけは絶対に嫌なの　』　）");

            UpdateMainMessageWithBlack("　　（　エレマ：『　だったら、最初に決定されたこの世界　』　）");

            UpdateMainMessageWithBlack("　　（　エレマ：『　私は受け止めるわ　』　）");

            UpdateMainMessageWithBlack("　　（　エレマ：『　ゴメンね、ファラ　』　）");

            UpdateMainMessageWithBlack("　　アイン：結果が・・・反対？");

            UpdateMainMessageWithBlack("　　ファラ：あの時は、みんなポカンとしちゃって");

            UpdateMainMessageWithBlack("　　ファラ：何か少し笑っちゃって・・・");

            UpdateMainMessageWithBlack("　　ファラ：それから同時に、私泣いちゃって・・・");

            UpdateMainMessageWithBlack("　　ファラ：無意味に心がざわついて・・・止まらなかったの");

            UpdateMainMessageWithBlack("　　ファラ：意味も分からず、痛くて・・・苦しくて・・・");

            UpdateMainMessageWithBlack("　　ファラ：・・・　・・・　・・・");

            UpdateMainMessageWithBlack("　　アイン：ダンジョンが何か明確に代償を要求してくるわけじゃないんだろ？");

            UpdateMainMessageWithBlack("　　アイン：気のせいさ、そんなのは。");

            UpdateMainMessageWithBlack("　　アイン：な、なあ元気出してくれよ、ファラ様。");

            UpdateMainMessageWithBlack("　　アイン：エレマさんは事故だったんだ。");

            UpdateMainMessageWithBlack("　　アイン：あまりそんな風に結び付けない方が良い。");

            UpdateMainMessageWithBlack("　　アイン：今はエレマさんへのお参りをしっかりやろう。");

            UpdateMainMessageWithBlack("　　ファラ：ええ・・・そうね。");

            UpdateMainMessageWithBlack("　　ファラ：アインさん、ダンジョンへ行くのであれば");

            UpdateMainMessageWithBlack("　　ファラ：必ず引き返す事を忘れずに");

            UpdateMainMessageWithBlack("　　アイン：ああ、分かった！");

            this.BackColor = Color.RoyalBlue;
            mainMessage.BackColor = Color.Transparent;
            this.backgroundData = Image.FromFile(Database.BaseResourceFolder + Database.DUNGEON_BACKGROUND);
            this.Invalidate();
        }
        public void Story_TruthRecollection3_4()
        {
            this.BackColor = Color.Black;
            mainMessage.BackColor = Color.White;
            this.backgroundData = null;
            this.Invalidate();

            UpdateMainMessageWithBlack("　　　【その瞬間、アインの脳裏に激しい激痛が襲った！周囲の感覚が麻痺する！！】");

            UpdateMainMessageWithBlack("　　＜＜＜　終わりへと足を運ぶな。　始まりへと足を進めろ。　＞＞＞");

            GroundOne.PlayDungeonMusic(Database.BGM15, Database.BGM15LoopBegin);

            UpdateMainMessageWithBlack("　　アイン：終わりへと・・・足を運ばないように？？");

            UpdateMainMessageWithBlack("　　ファラ：はい、ダンジョンへ向かうのであれば、そうしてください。");

            UpdateMainMessageWithBlack("　　アイン：えっと、大変すいませんが、もう一度教えてもらえないでしょうか？");

            UpdateMainMessageWithBlack("　　ファラ：終わりへと、足を運ぶなかれ。　始まりへと足を進めよ。");

            UpdateMainMessageWithBlack("　　ファラ：国王エルミからの教えです、ちゃんと守ってくださいね（＾＾");

            UpdateMainMessageWithBlack("　　アイン：えっと・・・？");

            UpdateMainMessageWithBlack("　　ファラ：アインさんの今のお気持ちは察しします、よく分かりませんよね。");

            UpdateMainMessageWithBlack("　　アイン：えっ、ええ。正直なトコ、言語系は結構苦手なもんでして・・・");

            UpdateMainMessageWithBlack("　　アイン：始まりへと足を進めるってどういう行いを指してるんでしょうか？？");

            UpdateMainMessageWithBlack("　　ファラ：アインさんの赴くままに（＾＾");

            UpdateMainMessageWithBlack("　　アイン：足を進めている以上、物事は進むワケだし、嫌でも終わりに近づくのでは？");

            UpdateMainMessageWithBlack("　　ファラ：アインさんの思うがままに（＾＾");

            UpdateMainMessageWithBlack("　　アイン：っぐ・・・了解、了解っす。　ハハハ・・・");

            UpdateMainMessageWithBlack("　　アイン：そうれはそうと、さっきの話ですが・・・");

            UpdateMainMessageWithBlack("　　ファラ：何故6人目のセフィだけがダンジョンに行かずに残ったのか？");

            UpdateMainMessageWithBlack("　　アイン：うわぁ、何でもお見通しかよ。　ええ、それに関してです。");

            UpdateMainMessageWithBlack("　　アイン：何故、彼女だけがダンジョンに赴かなかったんでしょう？");

            UpdateMainMessageWithBlack("　　ファラ：ファージル区域は当時、それほど治安が良いとは言えない状態。ここまでは話しましたわね？");

            UpdateMainMessageWithBlack("　　アイン：ええ、それは聞きました。");

            UpdateMainMessageWithBlack("　　アイン：あっ、ひょっとして！");

            UpdateMainMessageWithBlack("　　ファラ：ウフフ、噂通り読みは鋭いみたいね、アインさんは（＾＾");

            UpdateMainMessageWithBlack("　　アイン：いやいや・・・外れてるかも・・・");

            UpdateMainMessageWithBlack("　　ファラ：先ほどのご無礼のかわりに、まずはそちらからどうぞ（＾＾");

            UpdateMainMessageWithBlack("　　アイン：いやいやいや・・・無礼なんてとんでもない、ダンジョン目指す事自体は言い当てられてたし・・・");

            UpdateMainMessageWithBlack("　　ファラ：遠慮せず、どうぞ（＾＾");

            UpdateMainMessageWithBlack("　　アイン：かなわねえな・・・ハハ・・・じゃあ・・・");

            UpdateMainMessageWithBlack("　　アイン：ファージル区域はいつも６人体制で、自警団のような活動をしていた。");

            UpdateMainMessageWithBlack("　　アイン：それでファージル区域には、ある程度の治安が保たれていたわけだ。");

            UpdateMainMessageWithBlack("　　アイン：ユング区域のダンジョンへ全員で赴けば、どうしてもファージル区域の治安はまた悪くなる。");

            UpdateMainMessageWithBlack("　　アイン：ファージル区域の継続的な自衛活動として、最低限一人は残る必要があった。");

            UpdateMainMessageWithBlack("　　ファラ：ピンポン、大正解、さすがはアインさんです（＾＾");

            UpdateMainMessageWithBlack("　　アイン：いやいやいや・・・すみません、ホントこの辺で勘弁してください・・・");

            UpdateMainMessageWithBlack("　　ファラ：いえいえ、当たっているもの、大したものですわ（＾＾");

            UpdateMainMessageWithBlack("　　ファラ：そう、当時ダンジョンへ行こうと決めたのはエルミなんだけど。");

            UpdateMainMessageWithBlack("　　ファラ：そこで、一つ声が上がったの。");

            UpdateMainMessageWithBlack("　　ファラ：誰か一人は最低でも残るべきだと。");

            UpdateMainMessageWithBlack("　　ファラ：そう提案した声の主は、カールだったわ。");

            UpdateMainMessageWithBlack("　　アイン：シニキア・カールハンツ爵ですか？");

            UpdateMainMessageWithBlack("　　ファラ：ええ。");

            UpdateMainMessageWithBlack("　　ファラ：でも、その提案を聞き、残ると言い出したのは、アーティだった。");

            UpdateMainMessageWithBlack("　　アイン：ヴェルゼさんが・・・");

            UpdateMainMessageWithBlack("　　ファラ：その後、続けてオルがこう付け足したのよ。");

            UpdateMainMessageWithBlack("　　ファラ（声マネ）『ランディス：アーティ。てめえは絶対にダンジョンに来い。』");

            UpdateMainMessageWithBlack("　　ファラ（声マネ）『ランディス：じゃねえとダンジョン制覇は無理だ。』");

            UpdateMainMessageWithBlack("　　アイン：ヴェルゼさんがいないとダンジョンが解けない・・・師匠はそう読み切ったのか？");

            UpdateMainMessageWithBlack("　　ファラ：そうね、今となっては私も理解できます。");

            UpdateMainMessageWithBlack("　　ファラ：彼は戦闘面においては天才的でした。");

            UpdateMainMessageWithBlack("　　ファラ：おそらく力量そのものは、オルよりも遥か上。");

            UpdateMainMessageWithBlack("　　アイン：マジか・・・師匠より上だなんて・・・");

            UpdateMainMessageWithBlack("　　ファラ：いえ、正確には当時はまだオルの方が上だったのは事実です。");

            UpdateMainMessageWithBlack("　　ファラ：ただし、早期段階でオルはアーティが自分に追いついてくるのを見越していた。");

            UpdateMainMessageWithBlack("　　ファラ：そんな所だと思います。");

            UpdateMainMessageWithBlack("　　アイン：そうだったのか・・・俺の知らない事ばかりだな。");

            UpdateMainMessageWithBlack("　　ファラ：ウフフ、昔話だから知らなくて当たり前ですよ（＾＾");

            UpdateMainMessageWithBlack("　　アイン：でも、今の流れで、誰が残る事になるか、どういう風に決まったんだ？");

            UpdateMainMessageWithBlack("　　アイン：そういう場合は、大概は話が平行線になるだろうし。");

            UpdateMainMessageWithBlack("　　ファラ：アーティが「くじ引きにしよう」と言い出したんです。");

            UpdateMainMessageWithBlack("　　ファラ：それなら公平に決定できると。");

            UpdateMainMessageWithBlack("　　アイン：なるほど。");

            UpdateMainMessageWithBlack("　　ファラ：ウフフ、でも彼は当時からトリックが大好きだったのよ、つまり（＾＾");

            UpdateMainMessageWithBlack("　　アイン：え？い、いや・・・意味が・・・");

            UpdateMainMessageWithBlack("　　ファラ：彼が最後に引いて、貧乏クジの護衛をやるつもりだったんです。");

            UpdateMainMessageWithBlack("　　アイン：そんな事が可能なんですか？？");

            UpdateMainMessageWithBlack("　　ファラ：私には詳しく分かりませんが、できるそうです。");

            UpdateMainMessageWithBlack("　　アイン：じ、じゃあ結局ヴェルゼさんが？？");

            UpdateMainMessageWithBlack("　　ファラ：ウフフ、当時のアーティの性格や行動パターンは、セフィにとってお手玉のようなモノだったんですよ（＾＾");

            UpdateMainMessageWithBlack("　　アイン：あっ！　じゃあひょっとして、貧乏くじをトリックで自分が取るはずだったモノが！？");

            UpdateMainMessageWithBlack("　　ファラ：そうです、アーティの仕掛けたタネはセフィにバレてしまい、セフィが最後から２番目にくじを引き当てたのです。");

            UpdateMainMessageWithBlack("　　アイン：そうか、それでエレマさんに護衛役が回っちまったって事か・・・");

            UpdateMainMessageWithBlack("　　アイン：じゃあ、結局ダンジョンへ行かなければ、何とかなったんだろうか・・・？");

            UpdateMainMessageWithBlack("　　ファラ：いいえ、当時ファージル区域はあのままでは犯罪は増加の一方でしたし。");

            UpdateMainMessageWithBlack("　　ファラ：例えその時ダンジョンを諦めても、別の何らかの犯罪が起これば起こるほど・・・");

            UpdateMainMessageWithBlack("　　アイン：・・・そうだとしても、ダンジョンへ何故そうまでして赴く必要があったんだ？");

            UpdateMainMessageWithBlack("　　ファラ：そのダンジョンでは、実は言い伝えがあるのですよ。");

            UpdateMainMessageWithBlack("　　ファラ：『ダンジョンへ赴きし者達』");

            UpdateMainMessageWithBlack("　　ファラ：『汝らは、何を願うか？』");

            UpdateMainMessageWithBlack("　　ファラ：『攻略した暁にて』");

            UpdateMainMessageWithBlack("    ファラ：『その者の願い』");

            UpdateMainMessageWithBlack("    ファラ：『叶えてしんぜよう』");

            UpdateMainMessageWithBlack("　　アイン：願い事を・・・叶える？？");

            UpdateMainMessageWithBlack("    アイン：そんな神様みたいな話が本当に言い伝えとしてあったというんですか？？");

            UpdateMainMessageWithBlack("　　ファラ：ええ、本当よ。");

            UpdateMainMessageWithBlack("　　ファラ：当時の私達はそれが唯一の頼りだったの。");

            UpdateMainMessageWithBlack("　　アイン：そっか・・・治安が悪けりゃ、そういう頼みごともしてみたくなるよな・・・");

            UpdateMainMessageWithBlack("　　ファラ：ええ・・・自分達がまだ若かったという事もあったと思うわ。");

            UpdateMainMessageWithBlack("    ファラ：でもこの言い伝え、実は続きがあったのよ。");

            UpdateMainMessageWithBlack("　　アイン：続き・・・ですか？");

            UpdateMainMessageWithBlack("    ファラ：ええ、願い事が叶うと同時に・・・");

            GroundOne.StopDungeonMusic();

            UpdateMainMessageWithBlack("　　　【アインに対する激しい激痛は少しずつ引いていった。】");

            this.BackColor = Color.RoyalBlue;
            mainMessage.BackColor = Color.Transparent;
            this.backgroundData = Image.FromFile(Database.BaseResourceFolder + Database.DUNGEON_BACKGROUND);
            this.Invalidate();
        }
        public void Story_TruthRecollection3_3()
        {
            this.BackColor = Color.Black;
            mainMessage.BackColor = Color.White;
            this.backgroundData = null;
            this.Invalidate();

            UpdateMainMessageWithBlack("　　　【その瞬間、アインの脳裏に激しい激痛が襲った！周囲の感覚が麻痺する！！】");

            UpdateMainMessageWithBlack("　　＜＜＜　初めから全てが間違っているのだとしたら？　＞＞＞");

            //UpdateMainMessageWithBlack("　　＜＜＜　終わりへと足を運ぶな。　始まりへと足を進めろ。　＞＞＞");

            GroundOne.PlayDungeonMusic(Database.BGM15, Database.BGM15LoopBegin);

            UpdateMainMessageWithBlack("　　アイン：墓標参り・・・ですか？");

            UpdateMainMessageWithBlack("　　ファラ：ええ、もしよければアインさんも見ていただければと。");

            UpdateMainMessageWithBlack("　　ファラ：国王エルミからのたっての希望です。");

            UpdateMainMessageWithBlack("　　アイン：いえいえいえ、そんなとんでもないお言葉、巡礼させていただきます。");

            UpdateMainMessageWithBlack("　　ファラ：ウフフ、巡礼じゃないんだけどね。行きましょうか（＾＾");

            UpdateMainMessageWithBlack("　　アイン：ハハハ・・・");

            UpdateMainMessageWithBlack("　　アイン：えっと、じゃあこちらがその・・・？");

            UpdateMainMessageWithBlack("　　ファラ：ええ、そうです。");

            UpdateMainMessageWithBlack("　　アイン：・・・（黙祷を捧げるか・・・）");

            UpdateMainMessageWithBlack("　　アイン：・・・");

            UpdateMainMessageWithBlack("　　ファラ：私達は最初の頃、６人グループをよく旅をしていました。");

            UpdateMainMessageWithBlack("　　ファラ：私と、「カール」、「エルミ」、「オル」、「アーティ」");

            UpdateMainMessageWithBlack("　　ファラ：そしてもう１人が「エレマ・セフィーネ」");

            UpdateMainMessageWithBlack("　　アイン：エレマ・・・セフィーネ・・・");

            UpdateMainMessageWithBlack("　　アイン：俺達一般市民は、伝説のFiveSeekerって言われてるぐらいだから、メンバーは常々5人なんだと思ってましたよ。");

            UpdateMainMessageWithBlack("　　ファラ：そうね。国王エルミとしては、FiveSeekerじゃなくて本当はSixSeekerにして欲しいのよ、きっと（＾＾");

            UpdateMainMessageWithBlack("    アイン：そうか・・・俺達は始めから全て間違った認識のまま信じてたって事か・・・");

            UpdateMainMessageWithBlack("　　ファラ：知らなければ、知らないままですので、しょうがない事です（＾＾");

            UpdateMainMessageWithBlack("    アイン：す、すいません・・・ハハ・・・");

            UpdateMainMessageWithBlack("　　アイン：えっと・・・死因は何だったんでしょう？");

            UpdateMainMessageWithBlack("　　ファラ：事故だったと聞いています。");

            UpdateMainMessageWithBlack("　　ファラ：ヴェネステリア村まで向かう途中崖を通らなくては行けないのですが");

            UpdateMainMessageWithBlack("　　ファラ：当日は激しい雨天で、足元もぬかるみやすく、崖の地層もずいぶんと傷んでいたと聞いています。");

            UpdateMainMessageWithBlack("　　アイン：そこで・・・落ちたってワケか・・・");

            UpdateMainMessageWithBlack("　　アイン：誰か引き止めはしなかったんでしょうか？");

            UpdateMainMessageWithBlack("　　ファラ：この辺りのエリアは当時、それほど治安や整備が整っていない時代です。");

            UpdateMainMessageWithBlack("　　ファラ：ヴェネステリア村は自給自足が難しい土地であり、食料確保が厳しい地帯でした。");

            UpdateMainMessageWithBlack("　　ファラ：そしてファージル区域は食料を定期的に送り届ける援助活動を行っていました。");

            UpdateMainMessageWithBlack("　　ファラ：それもあって、エレマであれば、どうあっても行くと言ってきかなかったんでしょう。");

            UpdateMainMessageWithBlack("　　ファラ：ある程度警告を促す住人はいたのでしょうが、明確な引き止め行為は特になかったんだと思います。");

            UpdateMainMessageWithBlack("　　アイン：ファラ様や他のメンバーは引き止めはしなかったんでしょうか？");

            UpdateMainMessageWithBlack("　　ファラ：当時、未開拓エリアと呼ばれていたウェクスラー州のユング区域の裏");

            UpdateMainMessageWithBlack("　　ファラ：とあるダンジョン");

            UpdateMainMessageWithBlack("    ファラ：私達は当時、そのダンジョンの踏破を目指していました。");

            UpdateMainMessageWithBlack("　　アイン：ダンジョン？");

            UpdateMainMessageWithBlack("　　ファラ：ええ、『神の遺産』が宿ると言われているダンジョンです。");

            UpdateMainMessageWithBlack("　　ファラ：そのダンジョンに入っている間でしたので、いつもなら私やカールが引き止めるのですが・・・");

            UpdateMainMessageWithBlack("　　アイン：・・・す、すみません。立ち入った事をお聞きしてしまって・・・");

            UpdateMainMessageWithBlack("　　ファラ：良いんですよ、国王エルミも今の話を聞いてもらいたかったんだと思いますから（＾＾");

            UpdateMainMessageWithBlack("　　アイン：そ、そうですか・・・");

            UpdateMainMessageWithBlack("　　アイン：でも、何で俺なんかにこの話を？");

            UpdateMainMessageWithBlack("　　ファラ：アインさんは、ユング街のダンジョンへ向かうおつもりでしたよね？");

            UpdateMainMessageWithBlack("　　アイン：ッゲ！！！　何でそれを！！");

            UpdateMainMessageWithBlack("　　アイン：俺はまだその件を誰にも喋ってないはずですが！？");

            UpdateMainMessageWithBlack("　　ファラ：ウフフ、どうやら当たりの様でしたわ。国王エルミに報告しておきますね（＾＾");

            UpdateMainMessageWithBlack("　　アイン：・・・っだああぁぁぁ！！　ヒッカケ誘導かよ！！");

            UpdateMainMessageWithBlack("　　アイン：って、エルミ様に知れたら、ボケ師匠にも自動的に伝わるんじゃねえかあああぁぁ！！");

            UpdateMainMessageWithBlack("　　アイン：あぁ・・・またダンジョン開始に向けて、ボコられんのかな・・・");

            UpdateMainMessageWithBlack("　　ファラ：ウフフフ、ごめんなさいね（＾＾");

            UpdateMainMessageWithBlack("　　アイン：いやいや・・・（でも最初からピッタリ言い当てられたし、誘導じゃねえよなコレ・・・）");

            UpdateMainMessageWithBlack("　　ファラ：アインさん、お願いがあります。");

            UpdateMainMessageWithBlack("　　アイン：ッハ！ハイハイ、なんでしょうか！？");

            UpdateMainMessageWithBlack("　　ファラ：これは国王エルミのお願いでもあるのですが");

            UpdateMainMessageWithBlack("　　ファラ：ユング街のダンジョンへ向かわれるのであれば・・・");

            GroundOne.StopDungeonMusic();

            UpdateMainMessageWithBlack("　　　【アインに対する激しい激痛は少しずつ引いていった。】");

            this.BackColor = Color.RoyalBlue;
            mainMessage.BackColor = Color.Transparent;
            this.backgroundData = Image.FromFile(Database.BaseResourceFolder + Database.DUNGEON_BACKGROUND);
            this.Invalidate();
        }
        public void Story_TruthRecollection3_2()
        {
            this.BackColor = Color.Black;
            mainMessage.BackColor = Color.White;
            this.backgroundData = null;
            this.Invalidate();

            UpdateMainMessageWithBlack("　　　【その瞬間、アインの脳裏に激しい激痛が襲った！周囲の感覚が麻痺する！！】");

            UpdateMainMessageWithBlack("　　＜＜＜　この光景が全て幻想だとしたら？　＞＞＞");

            //UpdateMainMessageWithBlack("　　＜＜＜　初めから全てが間違っているのだとしたら？　＞＞＞");

            //UpdateMainMessageWithBlack("　　＜＜＜　終わりへと足を運ぶな。　始まりへと足を進めろ。　＞＞＞");

            GroundOne.PlayDungeonMusic(Database.BGM15, Database.BGM15LoopBegin);

            UpdateMainMessageWithBlack("　　ラナ：ランディスさんに聞いてみたのよ。");

            UpdateMainMessageWithBlack("　　アイン：何を？");

            UpdateMainMessageWithBlack("　　ラナ：ヴェルゼさんの素性の事よ。");

            UpdateMainMessageWithBlack("　　アイン：・・・そしたら、師匠は何て言ったんだ？");

            UpdateMainMessageWithBlack("　　ラナ：【全ては裏返し】って・・・");

            UpdateMainMessageWithBlack("　　アイン：・・・端的だなあ・・・");

            UpdateMainMessageWithBlack("　　ラナ：あ、そうそう。こうも言ってたわ。");

            UpdateMainMessageWithBlack("　　ラナ：（声マネ）『ランディス：アイツにとっちゃ、この光景が全て幻想なんだろ』");

            UpdateMainMessageWithBlack("　　ラナ：こんな感じだったと思うわ。");

            UpdateMainMessageWithBlack("　　アイン：全て幻想・・・");

            UpdateMainMessageWithBlack("　　アイン：ああ、ダメだ。全然意味がわからねえ・・・");

            UpdateMainMessageWithBlack("　　ラナ：そうかしら。");

            UpdateMainMessageWithBlack("　　ラナ：私、なんとなく分かっちゃったけど。");

            UpdateMainMessageWithBlack("　　アイン：っな！！　マジかよ！？　教えてくれよ！！");

            UpdateMainMessageWithBlack("　　ラナ：う～ん、教えてって言われても、合ってるかどうかもわかんないわよ。");

            UpdateMainMessageWithBlack("　　ラナ：それでもいい？");

            UpdateMainMessageWithBlack("　　アイン：ああ、ああ。　全然オッケー。");

            UpdateMainMessageWithBlack("　　ラナ：じゃあ、言うわね。");

            UpdateMainMessageWithBlack("　　ラナ：ヴェルゼさんには想い人が居たのよ、きっと。");

            UpdateMainMessageWithBlack("　　アイン：・・・え？");

            UpdateMainMessageWithBlack("　　ラナ：でも、不慮の事故か何かで、その想い人は死んでしまった。");

            UpdateMainMessageWithBlack("　　ラナ：想い人が死んでしまった世界の中で生きなくちゃいけない。");

            UpdateMainMessageWithBlack("　　ラナ：変えようと思っても変えられない。");

            UpdateMainMessageWithBlack("　　ラナ：だから、想い人の居ない現実の世界を、幻想の世界にしてしまいたいんじゃないかしら。");

            UpdateMainMessageWithBlack("　　ラナ：幻想と現実を入れ替える、言ってみれば現実世界を『全て裏返し』にしてしまう。");

            UpdateMainMessageWithBlack("　　ラナ：そういう考え方もあるわよね。実際無理なのは頭で分かってても、心がそう動くんじゃないかしら。");

            UpdateMainMessageWithBlack("　　アイン：・・・");

            UpdateMainMessageWithBlack("　　アイン：ラナ、お前のその想像力はすげえよな。");

            UpdateMainMessageWithBlack("　　ラナ：言っとくけど、当てずっぽだから、アテにならないわよ。");

            UpdateMainMessageWithBlack("　　アイン：いや、師匠がラナにかけた言葉は、ラナが推測しうる内容に誘導させているのは間違いねえんだ。");

            UpdateMainMessageWithBlack("　　アイン：多分合ってるぜ。それ。");

            UpdateMainMessageWithBlack("　　ラナ：そうかしら・・・今思いついたのを適当に言ってるだけだから、あんまり信頼しないでよね。");

            UpdateMainMessageWithBlack("　　アイン：ああ、了解了解。");

            GroundOne.StopDungeonMusic();

            UpdateMainMessageWithBlack("　　　【アインに対する激しい激痛は少しずつ引いていった。】");

            this.BackColor = Color.RoyalBlue;
            mainMessage.BackColor = Color.Transparent;
            this.backgroundData = Image.FromFile(Database.BaseResourceFolder + Database.DUNGEON_BACKGROUND);
            this.Invalidate();
        }
        public void Story_TruthRecollection3_1()
        {
            this.BackColor = Color.Black;
            mainMessage.BackColor = Color.White;
            this.backgroundData = null;
            this.Invalidate();

            UpdateMainMessageWithBlack("　　　【その瞬間、アインの脳裏に激しい激痛が襲った！周囲の感覚が麻痺する！！】");

            UpdateMainMessageWithBlack("　　＜＜＜　全てが裏返しだとしたら？ ＞＞＞");

            //UpdateMainMessageWithBlack("　　＜＜＜　この光景が全て幻想だとしたら？　＞＞＞");

            //UpdateMainMessageWithBlack("　　＜＜＜　初めから全てが間違っているのだとしたら？　＞＞＞");

            GroundOne.PlayDungeonMusic(Database.BGM15, Database.BGM15LoopBegin);

            UpdateMainMessageWithBlack("　　アイン：ヴェルゼ・アーティ？");

            UpdateMainMessageWithBlack("　　ラナ：アイン、あんたホントに知らないの？？");

            UpdateMainMessageWithBlack("　　アイン：いや、マジで知らねえな。");

            UpdateMainMessageWithBlack("　　ラナ：ふうん、変ね。");

            UpdateMainMessageWithBlack("　　アイン：何が変なんだよ。");

            UpdateMainMessageWithBlack("　　ラナ：もう何度か言ってると思うけど、もう一度言うわね。");

            UpdateMainMessageWithBlack("　　アイン：ああ、何度聞いても初めてにしか聞こえねえが、よろしく頼むぜ。");

            UpdateMainMessageWithBlack("　　ラナ：全てが裏返しな人なのよ。");

            UpdateMainMessageWithBlack("　　アイン：ラナ。そういう言い方するから覚えられねえんだって。");

            UpdateMainMessageWithBlack("　　アイン：頼む、もう少しだけわかりやすく教えてくれ。このとおりだ。");

            UpdateMainMessageWithBlack("　　ラナ：FiveSeekerはその名の通り、パーティは合わせて５人。ここまでは良いわね？");

            UpdateMainMessageWithBlack("　　アイン：ああ。");

            UpdateMainMessageWithBlack("　　ラナ：その中の５人を今から言うわね。");

            UpdateMainMessageWithBlack("　　アイン：おぉ、望むトコロだ！");

            UpdateMainMessageWithBlack("　　ラナ：望まれても困るんだけど・・・、じゃ行くわね。");

            UpdateMainMessageWithBlack("　　ラナ：エルミ・ジョルジュ。現国王様よ。");

            UpdateMainMessageWithBlack("　　アイン：ああ。");

            UpdateMainMessageWithBlack("　　ラナ：ファラ・フローレ。現王妃様ね。");

            UpdateMainMessageWithBlack("　　アイン：ああ。");

            UpdateMainMessageWithBlack("　　ラナ：オル・ランディス。今は闘技場の覇者で名が通ってるわね。");

            UpdateMainMessageWithBlack("　　アイン：まあそんなトコだな、次。");

            UpdateMainMessageWithBlack("　　ラナ：シニキア・カールハンツ。聖フローラ女学院の独立執行機関の長よ。");

            UpdateMainMessageWithBlack("　　アイン：ああ。");

            UpdateMainMessageWithBlack("　　ラナ：そして、ヴェルゼ・アーティ。");

            UpdateMainMessageWithBlack("　　ラナ：技の達人よ。");

            UpdateMainMessageWithBlack("　　アイン：技の達人・・・");

            UpdateMainMessageWithBlack("　　アイン：・・・　・・・　・・・");

            UpdateMainMessageWithBlack("　　アイン：そう、そうそう。");

            UpdateMainMessageWithBlack("　　アイン：ソコだよ。");

            UpdateMainMessageWithBlack("　　アイン：それだけなのかよ？他にもっとこう・・・ねえのかよ？");

            UpdateMainMessageWithBlack("　　ラナ：無いわ。");

            UpdateMainMessageWithBlack("    ラナ：現所在地、職業、生活、交友関係、一切が不明よ。");

            UpdateMainMessageWithBlack("　　アイン：・・・う～ん、どうもその特徴が無くて覚えにくいよなあ・・・実際。");

            UpdateMainMessageWithBlack("　　アイン：でだ。【全てが裏返し】ってのはどういう意味なんだ結局？");

            UpdateMainMessageWithBlack("    ラナ：私もよくわからないんだけどね・・・ゴメンナサイ。");

            UpdateMainMessageWithBlack("　　アイン：い、いやいやいや、変なトコで謝るなよ。いいっていって！ッハッハッハ！");

            UpdateMainMessageWithBlack("    ラナ：この言葉はね、ランディスさんから聞いたのよ。");

            UpdateMainMessageWithBlack("　　アイン：っな！？　師匠からかよ！？");

            UpdateMainMessageWithBlack("    ラナ：うん、そうなのよ。だからホンットちょっとだけ触れて終わりって感じだったの。");

            UpdateMainMessageWithBlack("　　アイン：ってか、何でラナはいつそんな会話を師匠としてたんだよ！？");

            UpdateMainMessageWithBlack("    ラナ：エスリミア草原区域で薬草の素材を探している時に偶然すれ違ったのよ。");

            UpdateMainMessageWithBlack("　　アイン：そこで何でそういう会話になったんだよ！？");

            UpdateMainMessageWithBlack("    ラナ：何妙な所で突っかかってるのよ、ホンットにもう。");

            UpdateMainMessageWithBlack("　　アイン：いやいや、悪い悪ぃ・・・で、そん時に教えてもらったのか？");

            UpdateMainMessageWithBlack("    ラナ：ええ、ちょっとその・・・");

            UpdateMainMessageWithBlack("　　アイン：・・・うん？");

            GroundOne.StopDungeonMusic();

            UpdateMainMessageWithBlack("　　　【アインに対する激しい激痛は少しずつ引いていった。】");

            this.BackColor = Color.RoyalBlue;
            mainMessage.BackColor = Color.Transparent;
            this.backgroundData = Image.FromFile(Database.BaseResourceFolder + Database.DUNGEON_BACKGROUND);
            this.Invalidate();
        }

        public void JumpByMirror_1_37()
        {
            JumpByMirror(38, 1, 0, 20);
        }
        public void JumpByMirror_1_33()
        {
            JumpByMirror(28, 8, 0, 17);
            UpdateUnknownTileArea3_0_16();
        }
        public void JumpByMirror_1_29()
        {
            JumpByMirror(37, 14, 0, 20);
            UpdateUnknownTileArea3_0_6();
        }
        public void JumpByMirror_1_21()
        {
            JumpByMirror(12, 12, 0, 0);
            UpdateUnknownTileArea3_0_14();
        }
        public void JumpByMirror_1_17()
        {
            JumpByMirror(28, 15, 3, 17);
            UpdateUnknownTileArea3_0_11();
        }
        public void JumpByMirror_1_11()
        {
            JumpByMirror(20, 12, 0, 10);
            UpdateUnknownTileArea3_0_7();
        }
        public void JumpByMirror_1_10()
        {
            JumpByMirror(36, 14, 0, 20);
            UpdateUnknownTileArea3_0_6();
        }
        public void JumpByMirror_1_3()
        {
            JumpByMirror(2, 16, 5, 0);
            UpdateUnknownTileArea3_0_2();
        }

        public void Story_TruthRecollection2_1()
        {
            this.BackColor = Color.Black;

            UpdateMainMessage("　　　【その瞬間、アインの脳裏に激しい激痛が襲った！周囲の感覚が麻痺する！！】");

            UpdateMainMessage("　　＜＜＜　どこで落としたんだ？ ＞＞＞");

            UpdateMainMessage("　　＜＜＜　盗まれたのだとしたら、どこだ？　＞＞＞");

            GroundOne.PlayDungeonMusic(Database.BGM15, Database.BGM15LoopBegin);

            UpdateMainMessage("　　アイン：しまった！！無くしちまった！！！");

            UpdateMainMessage("　　ラナ：いつ頃無くしたのよ？");

            UpdateMainMessage("　　アイン：いや、それが・・・２階を進めてる時は確か持ってたんだよ。");

            UpdateMainMessage("　　ラナ：いつ頃無くしたのかを聞いてるのよ。");

            UpdateMainMessage("　　アイン：いつ頃・・・いつ頃・・・。");

            UpdateMainMessage("　　アイン：２階の時は持ってた。");

            UpdateMainMessage("　　　『ッドグシィィァ！！！』（ラナのアルバトロスキックがアインに炸裂）　　");

            UpdateMainMessage("　　アイン：い、いやいや・・・本当思いだせないんだって。");

            UpdateMainMessage("　　アイン：何せあの剣はさ、持った感じがしないというかなんというか・・・");

            UpdateMainMessage("　　アイン：いつもと持った時の感触が何か違うなって気はしてたんだが・・・");

            UpdateMainMessage("　　ラナ：それって、無くしたんじゃなくて、すり替えられたって事じゃないの？");

            UpdateMainMessage("　　アイン：おお！　その通り！！");

            UpdateMainMessage("　　　『ズゴゴゴオオォォン！！！』（ラナの千手観音ブローがアインに炸裂）　　");

            UpdateMainMessage("　　アイン：グ・・グオオォォォ・・・わ、悪かったって。");

            UpdateMainMessage("　　ラナ：じゃあ、盗まれたあるいはすり替えられたとしたら、どこで？");

            UpdateMainMessage("　　アイン：それもわかれば苦労しねえんだけど・・・");

            UpdateMainMessage("　　ラナ：でも、おかしいわよね。");

            UpdateMainMessage("　　アイン：何がだ？");

            UpdateMainMessage("　　ラナ：それ、相当おかしいわよ、だって・・・");

            UpdateMainMessage("　　ラナ：アインって、剣は常に手元に置いておく方でしょ？");

            UpdateMainMessage("　　アイン：ああ、まあそうだな。");

            UpdateMainMessage("　　ラナ：褒めるのはシャクに障るけど・・・");

            UpdateMainMessage("　　ラナ：アインが持っている剣をすり替えるなんて、そんな簡単な事じゃないと思うんだけど。");

            UpdateMainMessage("　　アイン：そうか？");

            UpdateMainMessage("　　ラナ：そうよ。バカアインは、何だかんだ言っても気配察知は得意でしょ？");

            UpdateMainMessage("　　ラナ：バカそうに見えて、油断らしい油断も見当たらないわけだし。");

            UpdateMainMessage("　　アイン：まあ、そこら辺のゴロツキに取られるほど遅れは取るつもりはねえな。");

            UpdateMainMessage("　　アイン：でも、確かにすり替えられたんだって。");

            UpdateMainMessage("　　ラナ：ひょっとしたら顔見知りにやられたんじゃないの？");

            UpdateMainMessage("　　ラナ：顔見知りだったら少し気を許した状態になってるでしょ。");

            UpdateMainMessage("　　アイン：まさか顔見知りがそんな・・・");

            UpdateMainMessage("　　ラナ：イイから、少しだけ思い返してみなさいよ。");

            UpdateMainMessage("　　ラナ：知り合いの名前を言ってみなさいよ、ッホラホラ。");

            UpdateMainMessage("　　アイン：ラナだろ、ボケ師匠に、ガンツ伯父さん、ハンナおばさん・・・");

            UpdateMainMessage("　　アイン：DUEL闘技場の受付さん、カールハンツ先生・・・");

            UpdateMainMessage("　　アイン：それに街の皆・・・");

            UpdateMainMessage("　　アイン：それから・・・");

            UpdateMainMessage("　　ラナ：・・・");

            GroundOne.StopDungeonMusic();

            UpdateMainMessage("　　アイン：ヴェルゼ");

            UpdateMainMessage("　　アイン：【天空の翼】を保持する者");

            UpdateMainMessage("　　アイン：ヴェルゼ・アーティ");

            UpdateMainMessage("　　　【アインに対する激しい激痛は少しずつ引いていった。】");

            this.BackColor = Color.RoyalBlue;
        }

        public void Story_SeekerEvent907()
        {
            this.BackColor = Color.White;
            this.backgroundData = null;
            this.Invalidate();

            UpdateMainMessage("アイン：(俺は聖者のルートを潜った）");

            UpdateMainMessage("アイン：(その結果として、このダンジョンの構想を全て知る事になる）");

            UpdateMainMessage("アイン：(これが・・・究極解・・・）");

            UpdateMainMessage("アイン：(このダンジョンは一体いつから存在しているのか）");

            UpdateMainMessage("アイン：(ダンジョンは神の摂理そのもの）");

            UpdateMainMessage("アイン：(突如、でかい竜が目の前に現れた）");

            UpdateMainMessage("アイン：(そうか、理解した）");

            UpdateMainMessage("アイン：(この竜こそが唯一無二の存在）");

            UpdateMainMessage("アイン：(支配竜）");

            UpdateMainMessage("アイン：(ダンジョンの存在　＝　支配竜)");

            UpdateMainMessage("アイン：(ダンジョンは人の想いを媒介としているらしい）");

            UpdateMainMessage("アイン：(そしてその想いを解析し）");

            UpdateMainMessage("アイン：(願いを叶える。そういった所だ）");

            UpdateMainMessage("アイン：(だが、代わりに対象の人において最も致命的な犠牲を要求する仕組みになっている）");

            UpdateMainMessage("アイン：(犠牲を支払って願いを叶えるか）");

            UpdateMainMessage("アイン：(あるいは犠牲を払わず、現状のままで生かすか）");

            UpdateMainMessage("アイン：(人の想いは他の人の想いへと)");

            UpdateMainMessage("アイン：(それは連鎖的な反応であり）");

            UpdateMainMessage("アイン：(いわゆる『連鎖の終わり』と呼ばれる所はないそうだ）");

            UpdateMainMessage("アイン：(支配竜、いわゆるこのダンジョンは）");

            UpdateMainMessage("アイン：(それを明確に紡ぎ取り）");

            UpdateMainMessage("アイン：(ダンジョンを形成する支配竜はそれを問いかけとして提示し）");

            UpdateMainMessage("アイン：(ダンジョンを訪れる人へと、それを直視させる）");

            UpdateMainMessage("アイン：(問いかけへの直視は、通常の人間には耐え難いものであり");

            UpdateMainMessage("アイン：(大概は苦しみから逃げ出す者がほとんどらしい）");

            UpdateMainMessage("アイン：(なるほど、どうやら俺は）");

            UpdateMainMessage("アイン：(このダンジョンに対して）");

            UpdateMainMessage("アイン：(逃げずに、逃げずに）");

            UpdateMainMessage("アイン：(何度も諦めずに挑戦していたようだ）");

            UpdateMainMessage("アイン：(回数を教えてもらった）");

            UpdateMainMessage("アイン：(このダンジョンに来てから、俺は）");

            UpdateMainMessage("アイン：(" + Database.MUGEN_LOOP + "回）");

            UpdateMainMessage("アイン：(同じことを繰り返しているらしい）");

            UpdateMainMessage("アイン：(最下層は終着点ではなく）");

            UpdateMainMessage("アイン：(最下層は願いを満たせない者がたどり着く地点）");

            UpdateMainMessage("アイン：(終着点ではなく）");

            UpdateMainMessage("アイン：(完全なる終わり）");

            UpdateMainMessage("アイン：(完全なる終わりから、支配竜は）");

            UpdateMainMessage("アイン：(こう、問いかけるそうだ）");

            UpdateMainMessage("アイン：(次は何を願う？）");

            UpdateMainMessage("アイン：(もう一度、望みを繋げるか？）");

            UpdateMainMessage("アイン：(応えない者は、願いの叶わなかった現実世界に引き戻され）");

            UpdateMainMessage("アイン：(そこで一生を暮らす事になるらしい）");

            UpdateMainMessage("アイン：(願いは叶わなかった世界というと、聞こえは悪いが)");

            UpdateMainMessage("アイン：(ダンジョンに挑んだ事後として、通常の生活に戻るだけの話だ）");

            UpdateMainMessage("アイン：(願いは叶わなかった）");

            UpdateMainMessage("アイン：(それを噛みしめて生きていくそうだ）");

            UpdateMainMessage("アイン：(支配竜の問いかけに、応えた者は)");

            UpdateMainMessage("アイン：(一切の記憶を消去され）");

            UpdateMainMessage("アイン：(もう一度初めからやり直しができる）");

            UpdateMainMessage("アイン：(どこで選択が誤っていたか、思い出す事は叶わない）");

            UpdateMainMessage("アイン：(完全にリセットされた記憶から始めるわけだから)");

            UpdateMainMessage("アイン：(普通は何度やっても同じ結果になるらしい)");

            UpdateMainMessage("アイン：(俺もその中の１人だ)");

            UpdateMainMessage("アイン：(支配竜は最下層で、この事を人に伝えた上で)");

            UpdateMainMessage("アイン：(問いただしているそうだ。その上で)");

            UpdateMainMessage("アイン：(もう一度、望みを繋げるか？)");

            UpdateMainMessage("アイン：(通常なら１０，２０回、どれだけ心の強い者でも、１０００前後であきらめるそうだ）");

            UpdateMainMessage("アイン：(俺の" + Database.MUGEN_LOOP + "回は過去に例が無いらしい）");

            UpdateMainMessage("アイン：(俺の願い・・・願い・・・）");

            if (GroundOne.WE2.StartSeeker == false)
            {
                UpdateMainMessage("アイン：(気がつけば、ラナやヴェルゼは消えていた）");

                UpdateMainMessage("アイン：(いや、そもそもいつから消えていたのか）");

                UpdateMainMessage("アイン：(あるいは、存在すらしていたのかどうか）");
            }
            else
            {
                UpdateMainMessage("アイン：(願いを・・・純粋に考えているうちに・・・）");
            }

            UpdateMainMessage("アイン：(時間軸は遥かに超えた地点にどうやら来てしまっているようだ）");

            UpdateMainMessage("アイン：(周囲はすっかり光に包まれている）");

            UpdateMainMessage("アイン：(今の俺の願い・・・願い・・・）");

            UpdateMainMessage("アイン：(そう、俺はこのまま、支配竜の意志を受け継ぎ）");

            UpdateMainMessage("アイン：(俺が支配竜そのものになるんだ）");

            UpdateMainMessage("アイン：(ラナ・・・皆・・・）");

            UpdateMainMessage("アイン：(皆の願いが叶う世界）");

            UpdateMainMessage("アイン：(俺が今から）");

            UpdateMainMessage("アイン：(支配竜となりて）");

            UpdateMainMessage("アイン：(皆の願いが叶う世界を作ろう）");

            UpdateMainMessage("アイン：(それが俺の究極の願いだ）");

            UpdateMainMessage("アイン：・・・　・・・　・・・");

            UpdateMainMessage("アイン：・・・　・・・");

            UpdateMainMessage("アイン：・・・");

            UpdateMainMessage(" ～　THE　END　～　（聖者　『究極解』を会得した者）");
        }

        public void Story_SeekerEvent908()
        {
            this.BackColor = Color.Black;
            this.mainMessage.ForeColor = Color.White;
            this.backgroundData = null;
            this.Invalidate();

            UpdateMainMessage("アイン：(俺はこの愚者のルートを潜った）");

            UpdateMainMessage("アイン：(その後、俺は何度も何度も鏡を潜った）");

            UpdateMainMessage("アイン：(ようやく得られたと思った究極解）");

            UpdateMainMessage("アイン：(そこに到達すると、それ自体は究極解でも何でもなく）");

            UpdateMainMessage("アイン：(やはりそれは単なる通過点の一つでしかない）");

            UpdateMainMessage("アイン：(そうやって何度も何度も次こそは得られると信じて鏡を潜り続ける）");

            UpdateMainMessage("アイン：(最初の頃は、いろいろな記憶を持って挑んだ）");

            UpdateMainMessage("アイン：(ラナを助ける事、師匠に教えられた事）");

            UpdateMainMessage("アイン：(ファージル宮殿での出来事、エスミリア草原にある緑小屋、神剣フェルトゥーシュ）");

            UpdateMainMessage("アイン：(街のみんな、ハンナおばさん、ガンツおじさん）");

            UpdateMainMessage("アイン：(でも、どうしても鏡を潜るたびに・・・）");

            UpdateMainMessage("アイン：(その記憶がどんどん薄れていくのを感じた）");

            UpdateMainMessage("アイン：(鏡を潜り続け・・・）");
            
            UpdateMainMessage("アイン：(俺は当初の目的や想いを全く思い出せなくなってしまった）");

            UpdateMainMessage("アイン：(今では、鏡を潜る事しか考えられない状態だ）");

            UpdateMainMessage("アイン：(そう、それ自体が行動原理であり、それ自体が目的そのもの）");

            UpdateMainMessage("アイン：(それ以外に意識したり、考えたりする事は無くなった）");

            UpdateMainMessage("アイン：(そして、明確な何かが訪れる事もなく）");

            UpdateMainMessage("アイン：(永遠に究極解を求め続けて・・・）");

            if (GroundOne.WE2.StartSeeker == false)
            {
                UpdateMainMessage("アイン：(気がつけば、ラナやヴェルゼは消えていた）");

                UpdateMainMessage("アイン：(いや、そもそもいつから消えていたのか）");

                UpdateMainMessage("アイン：(あるいは、存在すらしていたのかどうか）");

                UpdateMainMessage("アイン：(駄目だ・・・それすらも思い出せない・・・）");
            }

            UpdateMainMessage("アイン：(周囲はすっかり暗闇になってしまった。）");

            UpdateMainMessage("アイン：(俺は今でも、鏡を求めて彷徨う)");

            UpdateMainMessage("アイン：(鏡を見つけては潜り、そしてまた潜り続ける）");

            UpdateMainMessage("アイン：(究極解など存在したのだろうか）");

            UpdateMainMessage("アイン：(それすらも思い出せなくなり）");

            UpdateMainMessage("アイン：(究極解と呼ばれていた解は得られたのか、通り過ぎたのか）");

            UpdateMainMessage("アイン：(認識が徐々に並行化し始めてきている)");

            UpdateMainMessage("アイン：(永遠にとある鏡だけを求め続け）");

            UpdateMainMessage("アイン：(そして、このまま果てるのだろう）");

            UpdateMainMessage("アイン：・・・　・・・　・・・");

            UpdateMainMessage("アイン：・・・　・・・");

            UpdateMainMessage("アイン：・・・");

            UpdateMainMessage(" ～　THE　END　～　（愚者　『究極解』に溺れた者）");
        }
    }
}
