using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonPlayer
{
    public class EnemyCharacter1 : MainCharacter
    {
        public EnemyCharacter1(string createName, int difficulty)
        {
            this.name = createName;

            if (difficulty == 1)
            {
                switch (createName)
                {
                    // ダンジョン１階
                    case "ヤング・ゴブリン":
                        this.baseStrength = 4; // 6;
                        this.baseAgility = 3;
                        this.baseIntelligence = 2;
                        this.baseStamina = 2;
                        this.baseMind = 1;
                        this.baseLife = 25;
                        this.experience = 57;
                        this.level = 1;
                        this.gold = 5;
                        break;
                    case "薄汚れた盗賊":
                        this.baseStrength = 3; // 5;
                        this.baseAgility = 5;
                        this.baseIntelligence = 2;
                        this.baseStamina = 1;
                        this.baseMind = 1;
                        this.baseLife = 20;
                        this.experience = 53;
                        this.level = 1;
                        this.gold = 8;
                        break;
                    case "ひ弱なビートル":
                        this.baseStrength = 2; // 4;
                        this.baseAgility = 1;
                        this.baseIntelligence = 1;
                        this.baseStamina = 3;
                        this.baseMind = 1;
                        this.baseLife = 25;
                        this.experience = 61;
                        this.level = 1;
                        this.gold = 7;
                        break;
                    case "幼いエルフ":
                        this.baseStrength = 3;
                        this.baseAgility = 4;
                        this.baseIntelligence = 3; // 5;
                        this.baseStamina = 1;
                        this.baseMind = 1;
                        this.baseLife = 20;
                        this.experience = 59;
                        this.level = 1;
                        this.gold = 6;
                        break;


                    case "落ちぶれた騎士":
                        this.baseStrength = 7; // 11;
                        this.baseAgility = 4; // 6;
                        this.baseIntelligence = 3;
                        this.baseStamina = 1; // 4;
                        this.baseMind = 1;
                        this.experience = 85;
                        this.level = 2;
                        this.gold = 25;
                        break;
                    case "小さなイノシシ":
                        this.baseStrength = 4; // 6;
                        this.baseAgility = 3; // 5;
                        this.baseIntelligence = 2;
                        this.baseStamina = 2; // 5;
                        this.baseMind = 1;
                        this.experience = 88;
                        this.level = 2;
                        this.gold = 20;
                        break;
                    case "睨む岩石":
                        this.baseStrength = 6; // 10;
                        this.baseAgility = 1;
                        this.baseIntelligence = 1;
                        this.baseStamina = 3; // 6;
                        this.baseMind = 1;
                        this.experience = 95;
                        this.level = 2;
                        this.gold = 19;
                        break;
                    case "ブルースライム":
                        this.baseStrength = 3; // 5;
                        this.baseAgility = 3; // 5;
                        this.baseIntelligence = 4;
                        this.baseStamina = 2; // 4;
                        this.baseMind = 1;
                        this.experience = 92;
                        this.level = 2;
                        this.gold = 21;
                        break;

                    case "ウェアウルフ":
                        this.baseStrength = 12; // 19;
                        this.baseAgility = 8; // 12;
                        this.baseIntelligence = 2;
                        this.baseStamina = 4; // 9;
                        this.experience = 123;
                        this.level = 3;
                        this.gold = 40;
                        break;
                    case "俊敏な鷹":
                        this.baseStrength = 8; // 17;
                        this.baseAgility = 10; // 17;
                        this.baseIntelligence = 2;
                        this.baseStamina = 3; // 6;
                        this.experience = 131;
                        this.level = 3;
                        this.gold = 38;
                        break;
                    case "シャドウハンター":
                        this.baseStrength = 8; // 15;
                        this.baseAgility = 10; // 12;
                        this.baseIntelligence = 2;
                        this.baseStamina = 5; // 9;
                        this.experience = 125;
                        this.level = 3;
                        this.gold = 44;
                        break;
                    case "卑屈なオーク":
                        this.baseStrength = 7; // 14;
                        this.baseAgility = 7; // 11;
                        this.baseIntelligence = 5; // 8;
                        this.baseStamina = 6; // 9;
                        this.experience = 127;
                        this.level = 3;
                        this.gold = 42;
                        break;


                    case "ブラックナイト":
                        this.baseStrength = 13; // 20;
                        this.baseAgility = 4;
                        this.baseIntelligence = 4;
                        this.baseStamina = 6; // 8;
                        this.experience = 150;
                        this.level = 4;
                        this.gold = 65;
                        break;
                    case "ホワイトナイト":
                        this.baseStrength = 13; // 20;
                        this.baseAgility = 4;
                        this.baseIntelligence = 6; // 10;
                        this.baseStamina = 6; // 8;
                        this.experience = 151;
                        this.level = 4;
                        this.gold = 65;
                        break;
                    case "番狼":
                        this.baseStrength = 15; // 23;
                        this.baseAgility = 11;
                        this.baseIntelligence = 1;
                        this.baseStamina = 3;
                        this.experience = 159;
                        this.level = 4;
                        this.gold = 60;
                        break;
                    case "着こなしの良いエルフ":
                        this.baseStrength = 2;
                        this.baseAgility = 9;
                        this.baseIntelligence = 10; // 15;
                        this.baseStamina = 5; // 6;
                        this.experience = 153;
                        this.level = 4;
                        this.gold = 72;
                        break;

                    case "一階の守護者：GiezBurn":
                        this.baseStrength = 16;
                        this.baseAgility = 12;
                        this.baseIntelligence = 30;
                        this.baseStamina = 20;
                        this.experience = 2500;
                        this.baseLife = 590;//1243; // 590
                        this.level = 17;
                        this.gold = 1500;
                        break;

                    case "一階の守護者：絡みつくフランシス":
                        this.baseStrength = 14; // 16;
                        this.baseAgility = 12;
                        this.baseIntelligence = 20; // 30;
                        this.baseStamina = 12; // 20;
                        this.experience = 2500;
                        this.baseLife = 450; // 590;
                        this.level = 17;
                        this.gold = 1500;
                        break;


                    // ダンジョン２階 // ステータス大幅改版します。
                    case "狂戦士バーサーカー":
                        this.baseStrength = 22; // 32;
                        this.baseAgility = 8;
                        this.baseIntelligence = 2;
                        this.baseStamina = 16; // 18;
                        this.experience = 201;
                        this.level = 5;
                        this.gold = 120;
                        break;
                    case "青隼":
                        this.baseStrength = 18; // 25;
                        this.baseAgility = 9; // 11;
                        this.baseIntelligence = 3;
                        this.baseStamina = 11;
                        this.experience = 195;
                        this.level = 5;
                        this.gold = 125;
                        break;
                    case "黒ビートル":
                        this.baseStrength = 19; // 25;
                        this.baseAgility = 5;
                        this.baseIntelligence = 10;
                        this.baseStamina = 15; // 20;
                        this.experience = 208;
                        this.level = 5;
                        this.gold = 132;
                        break;
                    case "悪意を向ける人間":
                        this.baseStrength = 21; // 29;
                        this.baseAgility = 6;
                        this.baseIntelligence = 6;
                        this.baseStamina = 12; // 19;
                        this.experience = 204;
                        this.level = 5;
                        this.gold = 134;
                        break;

                    case "オールドツリー":
                        this.baseStrength = 8;
                        this.baseAgility = 9;
                        this.baseIntelligence = 21; // 29;
                        this.baseStamina = 19; // 28;
                        this.experience = 281;
                        this.level = 6;
                        this.gold = 225;
                        break;
                    case "小さなオーガ":
                        this.baseStrength = 26; // 33;
                        this.baseAgility = 4;
                        this.baseIntelligence = 6;
                        this.baseStamina = 22; // 31;
                        this.experience = 282;
                        this.level = 6;
                        this.gold = 231;
                        break;
                    case "エルヴィッシュ・シャーマン":
                        this.baseStrength = 1;
                        this.baseAgility = 3;
                        this.baseIntelligence = 22; // 31;
                        this.baseStamina = 20; // 32;
                        this.experience = 292;
                        this.level = 6;
                        this.gold = 247;
                        break;
                    case "正装をした神官":
                        this.baseStrength = 2;
                        this.baseAgility = 25;
                        this.baseIntelligence = 18; // 22;
                        this.baseStamina = 17; // 28;
                        this.experience = 297;
                        this.level = 6;
                        this.gold = 239;
                        break;

                    case "サバンナ・ライオン":
                        this.baseStrength = 31; // 45;
                        this.baseAgility = 32; // 48;
                        this.baseIntelligence = 1;
                        this.baseStamina = 26; // 35;
                        this.experience = 370;
                        this.level = 7;
                        this.gold = 331;
                        break;
                    case "獰猛なハゲタカ":
                        this.baseStrength = 29; // 40;
                        this.baseAgility = 30; // 45;
                        this.baseIntelligence = 2;
                        this.baseStamina = 23; // 36;
                        this.experience = 395;
                        this.level = 7;
                        this.gold = 326;
                        break;
                    case "ゴブリン・チーフ":
                        this.baseStrength = 30; // 49;
                        this.baseAgility = 2;
                        this.baseIntelligence = 19; // 35;
                        this.baseStamina = 26; // 33;
                        this.experience = 387;
                        this.level = 7;
                        this.gold = 345;
                        break;
                    case "荒れ狂ったドワーフ":
                        this.baseStrength = 37; // 52;
                        this.baseAgility = 3;
                        this.baseIntelligence = 1;
                        this.baseStamina = 28; // 39;
                        this.experience = 401;
                        this.level = 7;
                        this.gold = 349;
                        break;

                    case "異形の信奉者":
                        this.baseStrength = 12;
                        this.baseAgility = 31;
                        this.baseIntelligence = 31; // 48;
                        this.baseStamina = 30; // 45;
                        this.experience = 511;
                        this.level = 8;
                        this.gold = 450;
                        break;
                    case "マンイーター":
                        this.baseStrength = 40; // 59;
                        this.baseAgility = 12;
                        this.baseIntelligence = 10;
                        this.baseStamina = 33; // 49;
                        this.experience = 525;
                        this.level = 8;
                        this.gold = 461;
                        break;
                    case "ヴァンパイア":
                        this.baseStrength = 40; // 50;
                        this.baseAgility = 40; // 50;
                        this.baseIntelligence = 40; // 50;
                        this.baseStamina = 35; // 50;
                        this.experience = 541;
                        this.level = 8;
                        this.gold = 467;
                        break;
                    case "赤いフードをかぶった人間":
                        this.baseStrength = 41; // 51;
                        this.baseAgility = 30; // 40;
                        this.baseIntelligence = 35; // 45;
                        this.baseStamina = 37; // 53;
                        this.experience = 536;
                        this.level = 8;
                        this.gold = 488;
                        break;

                    case "二階の守護者：Lizenos":
                        this.baseStrength = 45; // 55;
                        this.baseAgility = 29; // 39;
                        this.baseIntelligence = 30; // 42;
                        this.baseStamina = 33; // 41;
                        this.experience = 5000; // 7500; //30000;
                        this.baseLife = 1483; // 2239; //4511;
                        this.level = 25;
                        this.gold = 9000; // 6500;
                        break;


                    // ダンジョン３階 // ステータス大幅改版します。更に改版
                    case "イビルメージ":
                        this.baseStrength = 5;
                        this.baseAgility = 8;
                        this.baseIntelligence = 40; // 51;
                        this.baseStamina = 43; // 56;
                        this.experience = 610; // 451;
                        this.level = 9;
                        this.gold = 622; // 450;
                        break;
                    case "ダークシーフ":
                        this.baseStrength = 41; // 46;
                        this.baseAgility = 41; // 51;
                        this.baseIntelligence = 3;
                        this.baseStamina = 42; // 54;
                        this.experience = 623; // 466;
                        this.level = 9;
                        this.gold = 635; // 452;
                        break;
                    case "アークドルイド":
                        this.baseStrength = 35; // 42;
                        this.baseAgility = 4;
                        this.baseIntelligence = 45; // 52;
                        this.baseStamina = 46; // 67;
                        this.experience = 639; // 442;
                        this.level = 9;
                        this.gold = 640; // 463;
                        break;
                    case "シャドウソーサラー":
                        this.baseStrength = 1;
                        this.baseAgility = 2;
                        this.baseIntelligence = 50; // 62;
                        this.baseStamina = 37; // 50;
                        this.experience = 621; // 449;
                        this.level = 9;
                        this.gold = 639; // 467;
                        break;

                    case "忍者":
                        this.baseStrength = 50; // 70;
                        this.baseAgility = 55; // 75;
                        this.baseIntelligence = 2;
                        this.baseStamina = 45; // 65;
                        this.experience = 750; // 522;
                        this.level = 10;
                        this.gold = 788; // 497;
                        break;
                    case "エグゼキュージョナー":
                        this.baseStrength = 55; // 75;
                        this.baseAgility = 16;
                        this.baseIntelligence = 2;
                        this.baseStamina = 50; // 60;
                        this.experience = 766; // 531;
                        this.level = 10;
                        this.gold = 792; // 511;
                        break;
                    case "パワー":
                        this.baseStrength = 60; // 80;
                        this.baseAgility = 1;
                        this.baseIntelligence = 1;
                        this.baseStamina = 53; // 64;
                        this.experience = 781; // 545;
                        this.level = 10;
                        this.gold = 781; // 516;
                        break;
                    case "ブラックアイ":
                        this.baseStrength = 12;
                        this.baseAgility = 12;
                        this.baseIntelligence = 61; // 72;
                        this.baseStamina = 50; // 60;
                        this.experience = 800; // 538;
                        this.level = 10;
                        this.gold = 795; // 528;
                        break;

                    case "エルヴィッシュ神官":
                        this.baseStrength = 1;
                        this.baseAgility = 3;
                        this.baseIntelligence = 70; // 88;
                        this.baseStamina = 60; // 78;
                        this.experience = 935; // 608;
                        this.level = 11;
                        this.gold = 921; // 538;
                        break;
                    case "アプレンティス・ロード":
                        this.baseStrength = 70; // 90;
                        this.baseAgility = 45; // 55;
                        this.baseIntelligence = 65; // 75;
                        this.baseStamina = 55; // 70;
                        this.experience = 944; // 620;
                        this.level = 11;
                        this.gold = 900; // 532;
                        break;
                    case "悪魔崇拝者":
                        this.baseStrength = 5;
                        this.baseAgility = 16;
                        this.baseIntelligence = 80; // 94;
                        this.baseStamina = 52; // 75;
                        this.experience = 951; // 598;
                        this.level = 11;
                        this.gold = 911; // 540;
                        break;
                    case "デビルメージ":
                        this.baseStrength = 1;
                        this.baseAgility = 10;
                        this.baseIntelligence = 85; // 100;
                        this.baseStamina = 58; // 70;
                        this.experience = 961; // 627;
                        this.level = 11;
                        this.gold = 929; // 546;
                        break;

                    case "聖騎士":
                        this.baseStrength = 90; // 104;
                        this.baseAgility = 6;
                        this.baseIntelligence = 60; // 75;
                        this.baseStamina = 60; // 80;
                        this.experience = 1122; // 710;
                        this.level = 12;
                        this.gold = 1102; // 590;
                        break;
                    case "フォールンシーカー":
                        this.baseStrength = 92; // 110;
                        this.baseAgility = 48; // 58;
                        this.baseIntelligence = 80; // 98;
                        this.baseStamina = 68; // 88;
                        this.experience = 1251; // 711;
                        this.level = 12;
                        this.gold = 1219; // 597;
                        break;
                    case "アイオブザドラゴン":
                        this.baseStrength = 100; // 115;
                        this.baseAgility = 24;
                        this.baseIntelligence = 90; // 110;
                        this.baseStamina = 70; // 90;
                        this.experience = 1261; // 725;
                        this.level = 12;
                        this.gold = 1053; // 590;
                        break;
                    case "生まれたての悪魔":
                        this.baseStrength = 90; // 100;
                        this.baseAgility = 90; // 100;
                        this.baseIntelligence = 90; // 100;
                        this.baseStamina = 85; // 100;
                        this.experience = 1311; // 744;
                        this.level = 12;
                        this.gold = 1219; // 594;
                        break;


                    case "三階の守護者：Minflore":
                        this.baseStrength = 125; // 165;
                        this.baseAgility = 106; // 146;
                        this.baseIntelligence = 55; // 151;
                        this.baseStamina = 100; // 110;
                        this.baseMind = 39;
                        this.experience = 12000; // 15000;
                        this.baseLife = 4982; // 6627
                        this.level = 47;
                        this.gold = 20000;
                        break;




                    case "ヴェルゼ・アーティ":
                        this.baseStrength = 118;
                        this.baseAgility = 197;
                        this.baseIntelligence = 48;
                        this.baseStamina = 64;
                        this.baseMind = 1;
                        this.experience = 0;
                        this.baseLife = 830; // (Lv40 - 1 ) * 20 + 50
                        this.baseMana = 1250; // (Lv40 - 1 ) * 30 + 80
                        this.level = 40;
                        this.gold = 0;
                        break;




                    case "ゴルゴン":
                        this.baseStrength = 110; // 145;
                        this.baseAgility = 25;
                        this.baseIntelligence = 120; // 160;
                        this.baseStamina = 90; // 120;
                        this.experience = 1455; // 861;
                        this.level = 13;
                        this.gold = 1364; // 568;
                        break;
                    case "ビーストマスター":
                        this.baseStrength = 115; // 152;
                        this.baseAgility = 95; // 146;
                        this.baseIntelligence = 40; // 55;
                        this.baseStamina = 105; // 123;
                        this.experience = 1491; // 876;
                        this.level = 13;
                        this.gold = 1397; // 586;
                        break;
                    case "ヒュージスパイダー":
                        this.baseStrength = 112; // 155;
                        this.baseAgility = 85; // 111;
                        this.baseIntelligence = 100; // 130;
                        this.baseStamina = 110; // 130;
                        this.experience = 1438; // 912;
                        this.level = 13;
                        this.gold = 1351; // 593;
                        break;
                    case "エルダーアサシン":
                        this.baseStrength = 80;
                        this.baseAgility = 90; // 145;
                        this.baseIntelligence = 95; // 112;
                        this.baseStamina = 97; // 120;
                        this.experience = 1501; // 923;
                        this.level = 13;
                        this.gold = 1442; // 602;
                        break;

                    case "マスターロード":
                        this.baseStrength = 120; // 150;
                        this.baseAgility = 120; // 150;
                        this.baseIntelligence = 120; // 150;
                        this.baseStamina = 120; // 150;
                        this.experience = 1752; // 1355;
                        this.level = 14;
                        this.gold = 1697; // 722;
                        break;
                    case "ブルータルオーガ":
                        this.baseStrength = 150; // 190;
                        this.baseAgility = 55;
                        this.baseIntelligence = 70;
                        this.baseStamina = 132; // 162;
                        this.experience = 1772; // 1366;
                        this.level = 14;
                        this.gold = 1618; // 739;
                        break;
                    case "ウィンドブレイカー":
                        this.baseStrength = 105; // 130;
                        this.baseAgility = 141; // 171;
                        this.baseIntelligence = 122; // 150;
                        this.baseStamina = 110; // 149;
                        this.experience = 1788; // 1397;
                        this.level = 14;
                        this.gold = 1642; // 753;
                        break;
                    case "シン・ザ・ダークエルフ":
                        this.baseStrength = 75;
                        this.baseAgility = 85;
                        this.baseIntelligence = 149; // 189;
                        this.baseStamina = 117; // 137;
                        this.experience = 1805; // 1428;
                        this.level = 14;
                        this.gold = 1722; // 769;
                        break;

                    case "アークデーモン":
                        this.baseStrength = 155; // 185;
                        this.baseAgility = 100; // 130;
                        this.baseIntelligence = 136; // 166;
                        this.baseStamina = 125; // 165;
                        this.experience = 2128; // 1752;
                        this.level = 15;
                        this.gold = 2122; // 822;
                        break;
                    case "サン・ストライダー":
                        this.baseStrength = 165; // 195;
                        this.baseAgility = 155; // 185;
                        this.baseIntelligence = 45;
                        this.baseStamina = 130; // 170;
                        this.experience = 2278; // 1799;
                        this.level = 15;
                        this.gold = 2183; // 851;
                        break;
                    case "天秤を司る者":
                        this.baseStrength = 140; // 170;
                        this.baseAgility = 140; // 170;
                        this.baseIntelligence = 140; // 170;
                        this.baseStamina = 140; // 170;
                        this.experience = 2235; // 1835;
                        this.level = 15;
                        this.gold = 2201; // 877;
                        break;
                    case "レイジ・イフリート":
                        this.baseStrength = 170; // 200;
                        this.baseAgility = 165; // 195;
                        this.baseIntelligence = 77;
                        this.baseStamina = 144; // 174;
                        this.experience = 2309; // 1895;
                        this.level = 15;
                        this.gold = 2455; // 902;
                        break;

                    case "ペインエンジェル":
                        this.baseStrength = 180; // 210;
                        this.baseAgility = 176; // 196;
                        this.baseIntelligence = 165; // 187;
                        this.baseStamina = 162; // 192;
                        this.experience = 2892; // 2252;
                        this.level = 15;
                        this.gold = 3121; // 1123;
                        break;
                    case "ドゥームブリンガー":
                        this.baseStrength = 200; // 230;
                        this.baseAgility = 30;
                        this.baseIntelligence = 160; // 180;
                        this.baseStamina = 150; // 180;
                        this.baseMind = 20;
                        this.experience = 2805; // 2299;
                        this.level = 15;
                        this.gold = 3280; // 1178;
                        break;
                    case "ハウリングホラー":
                        this.baseStrength = 50;
                        this.baseAgility = 50;
                        this.baseIntelligence = 170; // 200;
                        this.baseStamina = 160; // 200;
                        this.experience = 2901; // 2335;
                        this.level = 15;
                        this.gold = 3311; // 1192;
                        break;
                    case "カオス・ワーデン":
                        this.baseStrength = 210; // 240;
                        this.baseAgility = 192; // 222;
                        this.baseIntelligence = 180; // 210;
                        this.baseStamina = 175; // 205;
                        this.experience = 2920; // 2395;
                        this.level = 15;
                        this.gold = 3217; // 1225;
                        break;

                    case "四階の守護者：Altomo":
                        this.baseStrength = 176; // 196; // 246;
                        this.baseAgility = 249; // 269; // 329;
                        this.baseIntelligence = 188; // 268;
                        this.baseStamina = 310; // 394;
                        this.baseMind = 450; // 556;
                        this.experience = 23000; // 27000;
                        this.baseLife = 11786; // 13695; // 20695;
                        this.level = 80;
                        this.gold = 100000; // 40000
                        break;


                    //case "Astarte":
                    //    break;
                    case "Phoenix":
                        this.baseStrength = 250; // 320;
                        this.baseAgility = 200; // 250;
                        this.baseIntelligence = 330; // 440;
                        this.baseStamina = 220; // 320;
                        this.experience = 5600;
                        this.level = 30;
                        this.gold = 4250; // 3555;
                        break;
                    //case "Brunhilde":
                    //    break;

                    //case "Asmodeus":
                    //    break;
                    //case "Salasvati":
                    //    break;
                    case "Emerard Dragon":
                        this.baseStrength = 300; // 360;
                        this.baseAgility = 270; // 320;
                        this.baseIntelligence = 170; // 180;
                        this.baseStamina = 245; // 345;
                        this.experience = 5900;
                        this.level = 30;
                        this.gold = 4491; // 3222;
                        break;

                    //case "Galuda":
                    //    break;
                    //case "Amaterasu":
                    //    break;
                    //case "Hekate":
                    //    break;

                    //case "Bahamut":
                    //    break;
                    case "Nine Tail":
                        this.baseStrength = 180;
                        this.baseAgility = 220; // 270;
                        this.baseIntelligence = 290; // 390;
                        this.baseStamina = 280; // 357;
                        this.experience = 6000;
                        this.baseMind = 3;
                        this.level = 30;
                        this.gold = 4641; // 3345;
                        break;
                    //case "Lilith":
                    //    break;

                    case "Judgement":
                        this.baseStrength = 355; // 425;
                        this.baseAgility = 290; // 370;
                        this.baseIntelligence = 90;
                        this.baseStamina = 300; // 390;
                        this.experience = 6200;
                        this.level = 30;
                        this.gold = 4798; // 3400;
                        break;

                    case "五階の守護者：Bystander":
                        this.baseStrength = 500; // 999;
                        this.baseAgility = 500; // 999;
                        this.baseIntelligence = 500; // 999;
                        this.baseStamina = 500; // 999;
                        this.baseMind = 500; // 999;
                        this.experience = 0;
                        this.baseLife = 45000; // 90009;
                        this.level = 99;
                        this.gold = 0;
                        break;

                    case "原罪：Verze Artie":
                        this.baseStrength = 5321;
                        this.baseAgility = 7568;
                        this.baseIntelligence = 6750;
                        this.baseStamina = 3532;
                        this.baseMind = 1;
                        this.experience = 0;
                        this.baseLife = 3652973;
                        this.level = 261;
                        this.gold = 0;
                        break;

                    case "ダミー素振り君":
                        this.baseStrength = 1;
                        this.baseAgility = 1;
                        this.baseIntelligence = 1;
                        this.baseStamina = 9999;
                        this.baseMind = 1;
                        this.experience = 0;
                        this.baseLife = 9990009;
                        this.level = 1;
                        this.gold = 0;
                        break;
                }
            }
            else
            {
                switch (createName)
                {
                    // ダンジョン１階
                    case "ヤング・ゴブリン":
                        this.baseStrength = 6;
                        this.baseAgility = 3;
                        this.baseIntelligence = 2;
                        this.baseStamina = 2;
                        this.baseMind = 1;
                        this.baseLife = 25;
                        this.experience = 57;
                        this.level = 1;
                        this.gold = 5;
                        break;
                    case "薄汚れた盗賊":
                        this.baseStrength = 5;
                        this.baseAgility = 5;
                        this.baseIntelligence = 2;
                        this.baseStamina = 1;
                        this.baseMind = 1;
                        this.baseLife = 20;
                        this.experience = 53;
                        this.level = 1;
                        this.gold = 8;
                        break;
                    case "ひ弱なビートル":
                        this.baseStrength = 4;
                        this.baseAgility = 1;
                        this.baseIntelligence = 1;
                        this.baseStamina = 3;
                        this.baseMind = 1;
                        this.baseLife = 25;
                        this.experience = 61;
                        this.level = 1;
                        this.gold = 7;
                        break;
                    case "幼いエルフ":
                        this.baseStrength = 3;
                        this.baseAgility = 4;
                        this.baseIntelligence = 5;
                        this.baseStamina = 1;
                        this.baseMind = 1;
                        this.baseLife = 20;
                        this.experience = 59;
                        this.level = 1;
                        this.gold = 6;
                        break;


                    case "落ちぶれた騎士":
                        this.baseStrength = 11;
                        this.baseAgility = 6;
                        this.baseIntelligence = 3;
                        this.baseStamina = 4;
                        this.baseMind = 1;
                        this.experience = 85;
                        this.level = 2;
                        this.gold = 25;
                        break;
                    case "小さなイノシシ":
                        this.baseStrength = 6;
                        this.baseAgility = 5;
                        this.baseIntelligence = 2;
                        this.baseStamina = 5;
                        this.baseMind = 1;
                        this.experience = 88;
                        this.level = 2;
                        this.gold = 20;
                        break;
                    case "睨む岩石":
                        this.baseStrength = 10;
                        this.baseAgility = 1;
                        this.baseIntelligence = 1;
                        this.baseStamina = 6;
                        this.baseMind = 1;
                        this.experience = 95;
                        this.level = 2;
                        this.gold = 19;
                        break;
                    case "ブルースライム":
                        this.baseStrength = 5;
                        this.baseAgility = 5;
                        this.baseIntelligence = 4;
                        this.baseStamina = 4;
                        this.baseMind = 1;
                        this.experience = 92;
                        this.level = 2;
                        this.gold = 21;
                        break;

                    case "ウェアウルフ":
                        this.baseStrength = 19;
                        this.baseAgility = 12;
                        this.baseIntelligence = 2;
                        this.baseStamina = 9;
                        this.experience = 123;
                        this.level = 3;
                        this.gold = 40;
                        break;
                    case "俊敏な鷹":
                        this.baseStrength = 17;
                        this.baseAgility = 17;
                        this.baseIntelligence = 2;
                        this.baseStamina = 6;
                        this.experience = 131;
                        this.level = 3;
                        this.gold = 38;
                        break;
                    case "シャドウハンター":
                        this.baseStrength = 15;
                        this.baseAgility = 12;
                        this.baseIntelligence = 2;
                        this.baseStamina = 9;
                        this.experience = 125;
                        this.level = 3;
                        this.gold = 44;
                        break;
                    case "卑屈なオーク":
                        this.baseStrength = 14;
                        this.baseAgility = 11;
                        this.baseIntelligence = 8;
                        this.baseStamina = 9;
                        this.experience = 127;
                        this.level = 3;
                        this.gold = 42;
                        break;


                    case "ブラックナイト":
                        this.baseStrength = 20;
                        this.baseAgility = 4;
                        this.baseIntelligence = 4;
                        this.baseStamina = 8;
                        this.experience = 150;
                        this.level = 4;
                        this.gold = 65;
                        break;
                    case "ホワイトナイト":
                        this.baseStrength = 20;
                        this.baseAgility = 4;
                        this.baseIntelligence = 10;
                        this.baseStamina = 8;
                        this.experience = 151;
                        this.level = 4;
                        this.gold = 65;
                        break;
                    case "番狼":
                        this.baseStrength = 23;
                        this.baseAgility = 11;
                        this.baseIntelligence = 1;
                        this.baseStamina = 3;
                        this.experience = 159;
                        this.level = 4;
                        this.gold = 60;
                        break;
                    case "着こなしの良いエルフ":
                        this.baseStrength = 2;
                        this.baseAgility = 9;
                        this.baseIntelligence = 15;
                        this.baseStamina = 6;
                        this.experience = 153;
                        this.level = 4;
                        this.gold = 72;
                        break;

                    case "一階の守護者：GiezBurn":
                        this.baseStrength = 16;
                        this.baseAgility = 12;
                        this.baseIntelligence = 30;
                        this.baseStamina = 20;
                        this.experience = 2500;
                        this.baseLife = 590;//1243; // 590
                        this.level = 17;
                        this.gold = 1500;
                        break;

                    case "一階の守護者：絡みつくフランシス":
                        this.baseStrength = 16;
                        this.baseAgility = 12;
                        this.baseIntelligence = 30;
                        this.baseStamina = 20;
                        this.experience = 2500;
                        this.baseLife = 590;
                        this.level = 17;
                        this.gold = 1500;
                        break;


                    // ダンジョン２階 // ステータス大幅改版します。
                    case "狂戦士バーサーカー":
                        this.baseStrength = 32;
                        this.baseAgility = 8;
                        this.baseIntelligence = 2;
                        this.baseStamina = 18;
                        this.experience = 201;
                        this.level = 5;
                        this.gold = 120;
                        break;
                    case "青隼":
                        this.baseStrength = 25;
                        this.baseAgility = 11;
                        this.baseIntelligence = 3;
                        this.baseStamina = 11;
                        this.experience = 195;
                        this.level = 5;
                        this.gold = 125;
                        break;
                    case "黒ビートル":
                        this.baseStrength = 25;
                        this.baseAgility = 5;
                        this.baseIntelligence = 10;
                        this.baseStamina = 20;
                        this.experience = 208;
                        this.level = 5;
                        this.gold = 132;
                        break;
                    case "悪意を向ける人間":
                        this.baseStrength = 29;
                        this.baseAgility = 6;
                        this.baseIntelligence = 6;
                        this.baseStamina = 19;
                        this.experience = 204;
                        this.level = 5;
                        this.gold = 134;
                        break;

                    case "オールドツリー":
                        this.baseStrength = 8;
                        this.baseAgility = 9;
                        this.baseIntelligence = 29;
                        this.baseStamina = 28;
                        this.experience = 255;
                        this.level = 6;
                        this.gold = 172;
                        break;
                    case "小さなオーガ":
                        this.baseStrength = 33;
                        this.baseAgility = 4;
                        this.baseIntelligence = 6;
                        this.baseStamina = 31;
                        this.experience = 251;
                        this.level = 6;
                        this.gold = 180;
                        break;
                    case "エルヴィッシュ・シャーマン":
                        this.baseStrength = 1;
                        this.baseAgility = 3;
                        this.baseIntelligence = 31;
                        this.baseStamina = 32;
                        this.experience = 266;
                        this.level = 6;
                        this.gold = 177;
                        break;
                    case "正装をした神官":
                        this.baseStrength = 2;
                        this.baseAgility = 25;
                        this.baseIntelligence = 22;
                        this.baseStamina = 28;
                        this.experience = 259;
                        this.level = 6;
                        this.gold = 182;
                        break;

                    case "サバンナ・ライオン":
                        this.baseStrength = 45;
                        this.baseAgility = 48;
                        this.baseIntelligence = 1;
                        this.baseStamina = 35;
                        this.experience = 290;
                        this.level = 7;
                        this.gold = 237;
                        break;
                    case "獰猛なハゲタカ":
                        this.baseStrength = 40;
                        this.baseAgility = 45;
                        this.baseIntelligence = 2;
                        this.baseStamina = 36;
                        this.experience = 300;
                        this.level = 7;
                        this.gold = 242;
                        break;
                    case "ゴブリン・チーフ":
                        this.baseStrength = 49;
                        this.baseAgility = 2;
                        this.baseIntelligence = 35;
                        this.baseStamina = 33;
                        this.experience = 305;
                        this.level = 7;
                        this.gold = 239;
                        break;
                    case "荒れ狂ったドワーフ":
                        this.baseStrength = 52;
                        this.baseAgility = 3;
                        this.baseIntelligence = 1;
                        this.baseStamina = 39;
                        this.experience = 297;
                        this.level = 7;
                        this.gold = 248;
                        break;

                    case "異形の信奉者":
                        this.baseStrength = 12;
                        this.baseAgility = 31;
                        this.baseIntelligence = 48;
                        this.baseStamina = 45;
                        this.experience = 380;
                        this.level = 8;
                        this.gold = 252;
                        break;
                    case "マンイーター":
                        this.baseStrength = 59;
                        this.baseAgility = 12;
                        this.baseIntelligence = 10;
                        this.baseStamina = 49;
                        this.experience = 366;
                        this.level = 8;
                        this.gold = 250;
                        break;
                    case "ヴァンパイア":
                        this.baseStrength = 50;
                        this.baseAgility = 50;
                        this.baseIntelligence = 50;
                        this.baseStamina = 50;
                        this.experience = 391;
                        this.level = 8;
                        this.gold = 253;
                        break;
                    case "赤いフードをかぶった人間":
                        this.baseStrength = 51;
                        this.baseAgility = 40;
                        this.baseIntelligence = 45;
                        this.baseStamina = 53;
                        this.experience = 382;
                        this.level = 8;
                        this.gold = 260;
                        break;

                    case "二階の守護者：Lizenos":
                        this.baseStrength = 55;
                        this.baseAgility = 39;
                        this.baseIntelligence = 42;
                        this.baseStamina = 41;
                        this.experience = 7500; //30000;
                        this.baseLife = 2239; //4511;
                        this.level = 25;
                        this.gold = 6500;
                        break;


                    // ダンジョン３階 // ステータス大幅改版します。更に改版
                    case "イビルメージ":
                        this.baseStrength = 5;
                        this.baseAgility = 8;
                        this.baseIntelligence = 51;
                        this.baseStamina = 56;
                        this.experience = 451;
                        this.level = 9;
                        this.gold = 450;
                        break;
                    case "ダークシーフ":
                        this.baseStrength = 46;
                        this.baseAgility = 51;
                        this.baseIntelligence = 3;
                        this.baseStamina = 54;
                        this.experience = 466;
                        this.level = 9;
                        this.gold = 452;
                        break;
                    case "アークドルイド":
                        this.baseStrength = 42;
                        this.baseAgility = 4;
                        this.baseIntelligence = 52;
                        this.baseStamina = 67;
                        this.experience = 442;
                        this.level = 9;
                        this.gold = 463;
                        break;
                    case "シャドウソーサラー":
                        this.baseStrength = 1;
                        this.baseAgility = 2;
                        this.baseIntelligence = 62;
                        this.baseStamina = 50;
                        this.experience = 449;
                        this.level = 9;
                        this.gold = 467;
                        break;

                    case "忍者":
                        this.baseStrength = 70;
                        this.baseAgility = 75;
                        this.baseIntelligence = 2;
                        this.baseStamina = 65;
                        this.experience = 522;
                        this.level = 10;
                        this.gold = 497;
                        break;
                    case "エグゼキュージョナー":
                        this.baseStrength = 75;
                        this.baseAgility = 16;
                        this.baseIntelligence = 2;
                        this.baseStamina = 60;
                        this.experience = 531;
                        this.level = 10;
                        this.gold = 511;
                        break;
                    case "パワー":
                        this.baseStrength = 80;
                        this.baseAgility = 1;
                        this.baseIntelligence = 1;
                        this.baseStamina = 64;
                        this.experience = 545;
                        this.level = 10;
                        this.gold = 516;
                        break;
                    case "ブラックアイ":
                        this.baseStrength = 12;
                        this.baseAgility = 12;
                        this.baseIntelligence = 72;
                        this.baseStamina = 60;
                        this.experience = 538;
                        this.level = 10;
                        this.gold = 528;
                        break;

                    case "エルヴィッシュ神官":
                        this.baseStrength = 1;
                        this.baseAgility = 3;
                        this.baseIntelligence = 88;
                        this.baseStamina = 78;
                        this.experience = 608;
                        this.level = 11;
                        this.gold = 538;
                        break;
                    case "アプレンティス・ロード":
                        this.baseStrength = 90;
                        this.baseAgility = 55;
                        this.baseIntelligence = 75;
                        this.baseStamina = 70;
                        this.experience = 620;
                        this.level = 11;
                        this.gold = 532;
                        break;
                    case "悪魔崇拝者":
                        this.baseStrength = 5;
                        this.baseAgility = 16;
                        this.baseIntelligence = 94;
                        this.baseStamina = 75;
                        this.experience = 598;
                        this.level = 11;
                        this.gold = 540;
                        break;
                    case "デビルメージ":
                        this.baseStrength = 1;
                        this.baseAgility = 10;
                        this.baseIntelligence = 100;
                        this.baseStamina = 70;
                        this.experience = 627;
                        this.level = 11;
                        this.gold = 546;
                        break;

                    case "聖騎士":
                        this.baseStrength = 104;
                        this.baseAgility = 6;
                        this.baseIntelligence = 75;
                        this.baseStamina = 80;
                        this.experience = 710;
                        this.level = 12;
                        this.gold = 590;
                        break;
                    case "フォールンシーカー":
                        this.baseStrength = 110;
                        this.baseAgility = 58;
                        this.baseIntelligence = 98;
                        this.baseStamina = 88;
                        this.experience = 711;
                        this.level = 12;
                        this.gold = 597;
                        break;
                    case "アイオブザドラゴン":
                        this.baseStrength = 115;
                        this.baseAgility = 24;
                        this.baseIntelligence = 110;
                        this.baseStamina = 90;
                        this.experience = 725;
                        this.level = 12;
                        this.gold = 590;
                        break;
                    case "生まれたての悪魔":
                        this.baseStrength = 100;
                        this.baseAgility = 100;
                        this.baseIntelligence = 100;
                        this.baseStamina = 100;
                        this.experience = 744;
                        this.level = 12;
                        this.gold = 594;
                        break;


                    case "三階の守護者：Minflore":
                        this.baseStrength = 165;
                        this.baseAgility = 146;
                        this.baseIntelligence = 151;
                        this.baseStamina = 110;
                        this.baseMind = 39;
                        this.experience = 15000;
                        this.baseLife = 6627;
                        this.level = 47;
                        this.gold = 15000;
                        break;




                    case "ヴェルゼ・アーティ":
                        this.baseStrength = 118;
                        this.baseAgility = 197;
                        this.baseIntelligence = 48;
                        this.baseStamina = 64;
                        this.baseMind = 1;
                        this.experience = 0;
                        this.baseLife = 830; // (Lv40 - 1 ) * 20 + 50
                        this.baseMana = 1250; // (Lv40 - 1 ) * 30 + 80
                        this.level = 40;
                        this.gold = 0;
                        break;




                    case "ゴルゴン":
                        this.baseStrength = 145;
                        this.baseAgility = 25;
                        this.baseIntelligence = 160;
                        this.baseStamina = 120;
                        this.experience = 861;
                        this.level = 13;
                        this.gold = 568;
                        break;
                    case "ビーストマスター":
                        this.baseStrength = 152;
                        this.baseAgility = 146;
                        this.baseIntelligence = 55;
                        this.baseStamina = 123;
                        this.experience = 876;
                        this.level = 13;
                        this.gold = 586;
                        break;
                    case "ヒュージスパイダー":
                        this.baseStrength = 155;
                        this.baseAgility = 111;
                        this.baseIntelligence = 130;
                        this.baseStamina = 130;
                        this.experience = 912;
                        this.level = 13;
                        this.gold = 593;
                        break;
                    case "エルダーアサシン":
                        this.baseStrength = 80;
                        this.baseAgility = 145;
                        this.baseIntelligence = 112;
                        this.baseStamina = 120;
                        this.experience = 923;
                        this.level = 13;
                        this.gold = 602;
                        break;

                    case "マスターロード":
                        this.baseStrength = 150;
                        this.baseAgility = 150;
                        this.baseIntelligence = 150;
                        this.baseStamina = 150;
                        this.experience = 1355;
                        this.level = 14;
                        this.gold = 722;
                        break;
                    case "ブルータルオーガ":
                        this.baseStrength = 190;
                        this.baseAgility = 55;
                        this.baseIntelligence = 70;
                        this.baseStamina = 162;
                        this.experience = 1366;
                        this.level = 14;
                        this.gold = 739;
                        break;
                    case "ウィンドブレイカー":
                        this.baseStrength = 130;
                        this.baseAgility = 171;
                        this.baseIntelligence = 150;
                        this.baseStamina = 149;
                        this.experience = 1397;
                        this.level = 14;
                        this.gold = 753;
                        break;
                    case "シン・ザ・ダークエルフ":
                        this.baseStrength = 75;
                        this.baseAgility = 85;
                        this.baseIntelligence = 189;
                        this.baseStamina = 137;
                        this.experience = 1428;
                        this.level = 14;
                        this.gold = 769;
                        break;

                    case "アークデーモン":
                        this.baseStrength = 185;
                        this.baseAgility = 130;
                        this.baseIntelligence = 166;
                        this.baseStamina = 165;
                        this.experience = 1752;
                        this.level = 15;
                        this.gold = 822;
                        break;
                    case "サン・ストライダー":
                        this.baseStrength = 195;
                        this.baseAgility = 185;
                        this.baseIntelligence = 45;
                        this.baseStamina = 170;
                        this.experience = 1799;
                        this.level = 15;
                        this.gold = 851;
                        break;
                    case "天秤を司る者":
                        this.baseStrength = 170;
                        this.baseAgility = 170;
                        this.baseIntelligence = 170;
                        this.baseStamina = 170;
                        this.experience = 1835;
                        this.level = 15;
                        this.gold = 877;
                        break;
                    case "レイジ・イフリート":
                        this.baseStrength = 200;
                        this.baseAgility = 195;
                        this.baseIntelligence = 77;
                        this.baseStamina = 174;
                        this.experience = 1895;
                        this.level = 15;
                        this.gold = 902;
                        break;

                    case "ペインエンジェル":
                        this.baseStrength = 210;
                        this.baseAgility = 196;
                        this.baseIntelligence = 187;
                        this.baseStamina = 192;
                        this.experience = 2252;
                        this.level = 15;
                        this.gold = 1123;
                        break;
                    case "ドゥームブリンガー":
                        this.baseStrength = 230;
                        this.baseAgility = 30;
                        this.baseIntelligence = 180;
                        this.baseStamina = 180;
                        this.baseMind = 20;
                        this.experience = 2299;
                        this.level = 15;
                        this.gold = 1178;
                        break;
                    case "ハウリングホラー":
                        this.baseStrength = 50;
                        this.baseAgility = 50;
                        this.baseIntelligence = 200;
                        this.baseStamina = 200;
                        this.experience = 2335;
                        this.level = 15;
                        this.gold = 1192;
                        break;
                    case "カオス・ワーデン":
                        this.baseStrength = 240;
                        this.baseAgility = 222;
                        this.baseIntelligence = 210;
                        this.baseStamina = 205;
                        this.experience = 2395;
                        this.level = 15;
                        this.gold = 1225;
                        break;

                    case "四階の守護者：Altomo":
                        this.baseStrength = 246;
                        this.baseAgility = 329;
                        this.baseIntelligence = 268;
                        this.baseStamina = 394;
                        this.baseMind = 556;
                        this.experience = 27000;
                        this.baseLife = 20695;
                        this.level = 80;
                        this.gold = 40000;
                        break;


                    //case "Astarte":
                    //    break;
                    case "Phoenix":
                        this.baseStrength = 320;
                        this.baseAgility = 250;
                        this.baseIntelligence = 440;
                        this.baseStamina = 320;
                        this.experience = 5600;
                        this.level = 30;
                        this.gold = 3555;
                        break;
                    //case "Brunhilde":
                    //    break;

                    //case "Asmodeus":
                    //    break;
                    //case "Salasvati":
                    //    break;
                    case "Emerard Dragon":
                        this.baseStrength = 360;
                        this.baseAgility = 320;
                        this.baseIntelligence = 180;
                        this.baseStamina = 345;
                        this.experience = 5900;
                        this.level = 30;
                        this.gold = 3222;
                        break;

                    //case "Galuda":
                    //    break;
                    //case "Amaterasu":
                    //    break;
                    //case "Hekate":
                    //    break;

                    //case "Bahamut":
                    //    break;
                    case "Nine Tail":
                        this.baseStrength = 180;
                        this.baseAgility = 270;
                        this.baseIntelligence = 390;
                        this.baseStamina = 357;
                        this.experience = 6000;
                        this.baseMind = 3;
                        this.level = 30;
                        this.gold = 3345;
                        break;
                    //case "Lilith":
                    //    break;

                    case "Judgement":
                        this.baseStrength = 425;
                        this.baseAgility = 370;
                        this.baseIntelligence = 90;
                        this.baseStamina = 390;
                        this.experience = 6200;
                        this.level = 30;
                        this.gold = 3400;
                        break;

                    case "五階の守護者：Bystander":
                        this.baseStrength = 999;
                        this.baseAgility = 999;
                        this.baseIntelligence = 999;
                        this.baseStamina = 999;
                        this.baseMind = 999;
                        this.experience = 0;
                        this.baseLife = 90009;
                        this.level = 99;
                        this.gold = 0;
                        break;

                    case "原罪：Verze Artie":
                        this.baseStrength = 5321;
                        this.baseAgility = 7568;
                        this.baseIntelligence = 6750;
                        this.baseStamina = 3532;
                        this.baseMind = 1;
                        this.experience = 0;
                        this.baseLife = 3652973;
                        this.level = 261;
                        this.gold = 0;
                        break;

                    case "ダミー素振り君":
                        this.baseStrength = 1;
                        this.baseAgility = 1;
                        this.baseIntelligence = 1;
                        this.baseStamina = 9999;
                        this.baseMind = 1;
                        this.experience = 0;
                        this.baseLife = 9990009;
                        this.level = 1;
                        this.gold = 0;
                        break;
                }
            }

            if (this.baseLife == 0)
            {
                this.baseLife = 40;
            }
            this.currentLife = this.baseLife + this.baseStamina * 10;
            this.currentMana = this.baseMana + this.baseIntelligence * 10;

            this.MainWeapon = new ItemBackPack("");
            this.SubWeapon = new ItemBackPack(""); // 後編追加
            this.MainArmor = new ItemBackPack("");
            this.Accessory = new ItemBackPack("");
            this.Accessory2 = new ItemBackPack(""); // 後編追加
        }
    }
}
