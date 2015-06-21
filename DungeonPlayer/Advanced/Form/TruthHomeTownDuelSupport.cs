using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    enum SupportType
    {
        Begin,
        FromDuelGate,
        FromDungeonGate,
    }

    public partial class TruthHomeTown
    {
        string KIINA = "受付嬢";

        string WhoisDuelPlayer()
        {
            string OpponentDuelist = String.Empty;

            if (we.AlreadyDuelComplete) return String.Empty;

            int[] levelBorder = new int[22];
            levelBorder[0] = 4;
            levelBorder[1] = 7;
            levelBorder[2] = 10;
            levelBorder[3] = 13;
            levelBorder[4] = 16;
            levelBorder[5] = 20;
            levelBorder[6] = 23;
            levelBorder[7] = 26;
            levelBorder[8] = 29;
            levelBorder[9] = 32;
            levelBorder[10] = 35;
            levelBorder[11] = 38;
            levelBorder[12] = 41;
            levelBorder[13] = 44;
            levelBorder[14] = 47;
            levelBorder[15] = 50;
            levelBorder[16] = 52;
            levelBorder[17] = 54;
            levelBorder[18] = 56;
            levelBorder[19] = 58;
            levelBorder[20] = 60;
            levelBorder[21] = 999;

            if (we.AvailableDuelMatch && we.MeetOlLandis)
            {
                // レベル上限に応じて対戦相手をスキップ移行するのを撤廃した。
                // mc.Level <= levelBorder[x] - 1
                // 階層毎にDUEL相手を制御する処理を追記したい。
                // !TruthCompleteAreaxx
                if (levelBorder[0] <= mc.Level&& !we.TruthDuelMatch1 && !we.TruthCompleteArea1)
                {
                    OpponentDuelist = Database.DUEL_EONE_FULNEA;
                }
                else if (levelBorder[1] <= mc.Level && !we.TruthDuelMatch2 && !we.TruthCompleteArea1)
                {
                    OpponentDuelist = Database.DUEL_MAGI_ZELKIS;
                }
                else if (levelBorder[2] <= mc.Level && !we.TruthDuelMatch3 && !we.TruthCompleteArea1)
                {
                    OpponentDuelist = Database.DUEL_SELMOI_RO;
                }
                else if (levelBorder[3] <= mc.Level && !we.TruthDuelMatch4 && !we.TruthCompleteArea1)
                {
                    OpponentDuelist = Database.DUEL_KARTIN_MAI;
                }
                else if (levelBorder[4] <= mc.Level && !we.TruthDuelMatch5 && !we.TruthCompleteArea1)
                {
                    OpponentDuelist = Database.DUEL_JEDA_ARUS;
                }
                else if (levelBorder[5] <= mc.Level && !we.TruthDuelMatch6 && !we.TruthCompleteArea1)
                {
                    OpponentDuelist = Database.DUEL_SINIKIA_VEILHANTU;
                }
                else if (levelBorder[6] <= mc.Level && !we.TruthDuelMatch7 && !we.TruthCompleteArea2)
                {
                    OpponentDuelist = Database.DUEL_ADEL_BRIGANDY;
                }
                else if (levelBorder[7] <= mc.Level && !we.TruthDuelMatch8 && !we.TruthCompleteArea2)
                {
                    OpponentDuelist = Database.DUEL_LENE_COLTOS;
                }
                else if (levelBorder[8] <= mc.Level && !we.TruthDuelMatch9 && !we.TruthCompleteArea2)
                {
                    OpponentDuelist = Database.DUEL_SCOTY_ZALGE;
                }
                else if (levelBorder[9] <= mc.Level && !we.TruthDuelMatch10 && !we.TruthCompleteArea2)
                {
                    OpponentDuelist = Database.DUEL_PERMA_WARAMY;
                }
                else if (levelBorder[10] <= mc.Level && !we.TruthDuelMatch11 && !we.TruthCompleteArea2)
                {
                    OpponentDuelist = Database.DUEL_KILT_JORJU;
                }
                else if (levelBorder[11] <= mc.Level && !we.TruthDuelMatch12 && !we.TruthCompleteArea3)
                {
                    OpponentDuelist = Database.DUEL_BILLY_RAKI;
                }
                else if (levelBorder[12] <= mc.Level && !we.TruthDuelMatch13 && !we.TruthCompleteArea3)
                {
                    OpponentDuelist = Database.DUEL_ANNA_HAMILTON;
                }
                else if (levelBorder[13] <= mc.Level && !we.TruthDuelMatch14 && !we.TruthCompleteArea3)
                {
                    OpponentDuelist = Database.DUEL_CALMANS_OHN;
                }
                else if (levelBorder[14] <= mc.Level && !we.TruthDuelMatch15 && !we.TruthCompleteArea3)
                {
                    OpponentDuelist = Database.DUEL_SUN_YU;
                }
                else if (levelBorder[15] <= mc.Level && !we.TruthDuelMatch16 && !we.TruthCompleteArea3)
                {
                    OpponentDuelist = Database.DUEL_SHUVALTZ_FLORE;
                }
                else if (levelBorder[16] <= mc.Level && !we.TruthDuelMatch17 && !we.TruthCompleteArea4)
                {
                    OpponentDuelist = Database.DUEL_RVEL_ZELKIS;
                }
                else if (levelBorder[17] <= mc.Level && !we.TruthDuelMatch18 && !we.TruthCompleteArea4)
                {
                    OpponentDuelist = Database.DUEL_VAN_HEHGUSTEL;
                }
                else if (levelBorder[18] <= mc.Level && !we.TruthDuelMatch19 && !we.TruthCompleteArea4)
                {
                    OpponentDuelist = Database.DUEL_OHRYU_GENMA;
                }
                else if (levelBorder[19] <= mc.Level && !we.TruthDuelMatch20 && !we.TruthCompleteArea4)
                {
                    OpponentDuelist = Database.DUEL_LADA_MYSTORUS;
                }
                else if (levelBorder[20] <= mc.Level && !we.TruthDuelMatch21 && !we.TruthCompleteArea4)
                {
                    OpponentDuelist = Database.DUEL_SIN_OSCURETE;
                }
            }
            else
            {
                OpponentDuelist = String.Empty;
            }

            return OpponentDuelist;
        }

        void DuelSupportMessage(SupportType type, string OpponentDuelist)
        {
            if (we.DuelWinZalge) KIINA = "キーナ";
            else KIINA = "受付嬢";

            int[] levelBorder = new int[22];
            levelBorder[0] = 4;
            levelBorder[1] = 7;
            levelBorder[2] = 10;
            levelBorder[3] = 13;
            levelBorder[4] = 16;
            levelBorder[5] = 20;
            levelBorder[6] = 23;
            levelBorder[7] = 26;
            levelBorder[8] = 29;
            levelBorder[9] = 32;
            levelBorder[10] = 35;
            levelBorder[11] = 38;
            levelBorder[12] = 41;
            levelBorder[13] = 44;
            levelBorder[14] = 47;
            levelBorder[15] = 50;
            levelBorder[16] = 52;
            levelBorder[17] = 54;
            levelBorder[18] = 56;
            levelBorder[19] = 58;
            levelBorder[20] = 60;
            levelBorder[21] = 999;

            #region "エオネ・フルネア"
            if (levelBorder[0] <= mc.Level && !we.TruthDuelMatch1 && !we.TruthCompleteArea1)
            {
                if (type == SupportType.Begin)
                {
                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("アイン：ん・・・もう６時か。そろそろ起きないとな。");

                    UpdateMainMessage("アイン：おばちゃん、おはようさん。");

                    UpdateMainMessage("ハンナ：ああ、アインかい。ようやく起きたね。");

                    UpdateMainMessage("ハンナ：そういえば、表に闘技場の受付が来てるよ。");

                    UpdateMainMessage("アイン：ッゲ、マジかよ！？");

                    UpdateMainMessage("ハンナ：連絡事項があるそうだよ。早く行ってやんな。");

                    UpdateMainMessage("アイン：わ、わかったぜ。サンキュー、おばちゃん。");

                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    UpdateMainMessage("受付嬢：アイン様、お待ちしておりました。");

                    UpdateMainMessage("アイン：よう、闘技場の受付さんじゃねえか！　おはようっす！");

                    UpdateMainMessage("受付嬢：おはようございます、アイン様。");

                    UpdateMainMessage("受付嬢：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。");

                    UpdateMainMessage("アイン：DUEL対戦シード登録・・・つまり、今日が対戦日って事か？");

                    UpdateMainMessage("受付嬢：はい");

                    UpdateMainMessage("アイン：マジかよ・・・って、対戦相手は誰なんだ？");

                    UpdateMainMessage("受付嬢：対戦相手は『" + OpponentDuelist + "』様となっております。");

                    UpdateMainMessage("受付嬢：なお、本対戦はダンジョンへ赴く前に開催とさせていただきます。");

                    UpdateMainMessage("受付嬢：本日は、必ずDUEL闘技場へとお越しくださいますよう、よろしくお願いします。");

                    UpdateMainMessage("アイン：えっと、ダンジョンへ直接向かったら駄目なのか？");

                    UpdateMainMessage("受付嬢：本日は、必ずDUEL闘技場へとお越しくださいますよう、よろしくお願いします。");

                    UpdateMainMessage("アイン：っとぉ・・・分かった・・・");

                    UpdateMainMessage("アイン：じゃあ、DUEL闘技場へと向かうぜ。了解了解！");

                    UpdateMainMessage("受付嬢：連絡事項は以上です。　それでは。");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = "受付嬢は立ち去っていった・・・";
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：ふう、今日がDUEL日ってワケか・・・");

                    UpdateMainMessage("アイン：っしゃ、DUEL向けに準備でも整えるとするか！", true);
                }
                else if ((type == SupportType.FromDungeonGate) ||
                            (type == SupportType.FromDuelGate))
                {
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);
 
                    UpdateMainMessage("　　【受付嬢：アイン様、お待ちしておりました。】");

                    if (type == SupportType.FromDungeonGate)
                    {
                        UpdateMainMessage("アイン：よお、Duelの受付さんじゃねえか。");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：よお、受付さん。約束どおり来たぜ。");
                    }

                    UpdateMainMessage("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】");

                    if (type == SupportType.FromDungeonGate)
                    {
                        UpdateMainMessage("アイン：ッゲ！いきなりかよ？");

                        UpdateMainMessage("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】");

                        UpdateMainMessage("アイン：っちょ、待ってくれよ。今からダンジョンなんだが。");

                        UpdateMainMessage("    【受付嬢：DUEL闘技場中央部へトランスポートさせていただきます。】");

                        UpdateMainMessage("アイン：マジかよ。少し戦いの準備支度をしたいんだが・・・");

                        UpdateMainMessage("    【受付嬢：DUEL闘技場中央部へトランスポートさせていただきます。】");

                        UpdateMainMessage("アイン：本当有無を言わさずだな・・・次からは気をつけねえと。");
                    }

                    UpdateMainMessage("アイン：ああ、じゃあ転送してくれ。頼んだぜ！");

                    CallSomeMessageWithAnimation("アインはDUEL闘技場へ転送されました");

                    UpdateMainMessage("アイン：っと・・・ここは、突然闘技場の中央に飛ばされたのか。");

                    UpdateMainMessage("エオネ：よろしくお願いしますね。アインさん。");

                    UpdateMainMessage("アイン：っあ、あぁ。　こちらこそ！");

                    UpdateMainMessage("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】");

                    UpdateMainMessage("エオネ：はい");

                    UpdateMainMessage("アイン：おう");

                    we.TruthDuelMatch1 = true;
                }
            }
            #endregion
            #region "マーギ・ゼルキス
            else if (levelBorder[1] <= mc.Level && !we.TruthDuelMatch2 && !we.TruthCompleteArea1)
            {
                if (type == SupportType.Begin)
                {
                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("アイン：ん・・・もう６時か。そろそろ起きないとな。");

                    UpdateMainMessage("アイン：おはよう、おばちゃん。");

                    UpdateMainMessage("ハンナ：ああ、おはよう。");

                    UpdateMainMessage("ハンナ：そうそう、また来てるわよ、闘技場の受付。");

                    UpdateMainMessage("アイン：グア・・・DUEL通達か。");

                    UpdateMainMessage("ハンナ：待たせちゃ悪いからね、早く行ってやんな。");

                    UpdateMainMessage("アイン：ああ、じゃちょっくら挨拶してくるぜ。");

                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    UpdateMainMessage("受付嬢：アイン様、お待ちしておりました。");

                    UpdateMainMessage("アイン：よう、受付さん、今日もいい天気だな！");

                    UpdateMainMessage("受付嬢：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。");

                    UpdateMainMessage("アイン：そっか、じゃあ今日がDUEL開始日ってワケだな。");

                    UpdateMainMessage("アイン：ところで、対戦相手は？");

                    UpdateMainMessage("受付嬢：対戦相手は『" + OpponentDuelist + "』様となっております。");

                    UpdateMainMessage("受付嬢：なお、本対戦はダンジョンへ赴く前に開催とさせていただきます。");

                    UpdateMainMessage("受付嬢：本日、DUEL闘技場へとお越しくださいますよう、よろしくお願いします。");

                    UpdateMainMessage("アイン：ああ、分かったぜ。連絡サンキューな！");

                    UpdateMainMessage("受付嬢：連絡は以上です。　それでは。");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = "受付嬢は立ち去っていった・・・";
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：ふう・・・さてと");

                    UpdateMainMessage("アイン：オーケー、DUEL準備と行くか！", true);
                }
                else if ((type == SupportType.FromDuelGate) ||
                            (type == SupportType.FromDungeonGate))
                {
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    UpdateMainMessage("　　【受付嬢：アイン様、お待ちしておりました。】");

                    if (type == SupportType.FromDungeonGate)
                    {
                        UpdateMainMessage("アイン：Duelタイムってわけか。");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：よお、受付さん。約束どおり来たぜ。");
                    }

                    UpdateMainMessage("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】");

                    UpdateMainMessage("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】");

                    UpdateMainMessage("アイン：ああ、準備は万端だ。転送してくれ。");

                    CallSomeMessageWithAnimation("アインはDUEL闘技場へ転送されました");

                    UpdateMainMessage("ゼルキス：貴様がアインとか言うヤツか。");

                    UpdateMainMessage("アイン：ああ、そうだ。");

                    UpdateMainMessage("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】");

                    UpdateMainMessage("ゼルキス：潰してやるぜ。アイン。");

                    UpdateMainMessage("アイン：おう、やれるもんならやってみろ。");

                    we.TruthDuelMatch2 = true;
                }
            }
            #endregion
            #region "セルモイ・ロウ"
            else if (levelBorder[2] <= mc.Level && !we.TruthDuelMatch3 && !we.TruthCompleteArea1)
            {
                if (type == SupportType.Begin)
                {
                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("アイン：ん・・・もう６時か。そろそろ起きないとな。");

                    UpdateMainMessage("アイン：おはよう、おばちゃん。");

                    UpdateMainMessage("ハンナ：ああ、おはよう。");

                    UpdateMainMessage("ハンナ：アイン、闘技場の受付だよ、行ってやんな");

                    UpdateMainMessage("アイン：DUEL通達か、了解了解。");

                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    UpdateMainMessage("受付嬢：アイン様、お待ちしておりました。");

                    UpdateMainMessage("アイン：いつも悪いな、朝早くから");

                    UpdateMainMessage("受付嬢：いえ");

                    UpdateMainMessage("受付嬢：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。");

                    UpdateMainMessage("アイン：連絡サンキュー。　対戦相手の名前は？");

                    UpdateMainMessage("受付嬢：対戦相手は『" + OpponentDuelist + "』様となっております。");

                    UpdateMainMessage("受付嬢：なお、本対戦はダンジョンへ赴く前に開催とさせていただきます。");

                    UpdateMainMessage("受付嬢：本日、DUEL闘技場へとお越しくださいますよう、よろしくお願いします。");

                    UpdateMainMessage("アイン：それはそうと、何でダンジョンへ赴く前じゃないと駄目なんだ？");

                    UpdateMainMessage("受付嬢：それは・・・");
                    
                    UpdateMainMessage("受付嬢：ともかく、DUEL闘技場へとお越しくださいますよう、よろしくお願いします。");

                    UpdateMainMessage("アイン：あ、ああ。　オーケー。");

                    UpdateMainMessage("受付嬢：以上です。　それでは。");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = "受付嬢は立ち去っていった・・・";
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：受付だから、余計な事までは喋れないって感じなのか・・・");

                    UpdateMainMessage("アイン：まあいいや、DUELの準備するか！", true);
                }
                else if ((type == SupportType.FromDuelGate) ||
                         (type == SupportType.FromDungeonGate))
                {
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    UpdateMainMessage("　　【受付嬢：アイン様、お待ちしておりました。】");

                    if (type == SupportType.FromDungeonGate)
                    {
                        UpdateMainMessage("アイン：Duelタイムってわけか。");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：よお、受付さん。約束どおり来たぜ。");
                    }

                    UpdateMainMessage("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】");

                    UpdateMainMessage("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】");

                    UpdateMainMessage("アイン：ああ、準備は万端だ。転送してくれ。");

                    CallSomeMessageWithAnimation("アインはDUEL闘技場へ転送されました");

                    UpdateMainMessage("セルモイ：よろしくお願いします。");

                    UpdateMainMessage("アイン：ああ、よろしくな。");

                    UpdateMainMessage("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】");

                    UpdateMainMessage("セルモイ：本気で行きます！");

                    UpdateMainMessage("アイン：っしゃ、かかってこい！");

                    we.TruthDuelMatch3 = true;
                }
            }
            #endregion
            #region "カーティン・マイ"
            else if (levelBorder[3] <= mc.Level && !we.TruthDuelMatch4 && !we.TruthCompleteArea1)
            {
                if (type == SupportType.Begin)
                {
                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("アイン：ん・・・もう６時か。そろそろ起きないとな。");

                    UpdateMainMessage("アイン：おはよう、おばちゃん。");

                    UpdateMainMessage("ハンナ：ああ、おはよう。");

                    UpdateMainMessage("ハンナ：アイン、闘技場の受付だよ、行ってやんな");

                    UpdateMainMessage("アイン：オーケー");

                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    UpdateMainMessage("受付嬢：アイン様、お待ちしておりました。");

                    UpdateMainMessage("アイン：いつも俺より早く起きてるのか？");

                    UpdateMainMessage("受付嬢：はい");

                    UpdateMainMessage("受付嬢：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。");

                    UpdateMainMessage("アイン：対戦相手の名前は？");

                    UpdateMainMessage("受付嬢：対戦相手は『" + OpponentDuelist + "』様となっております。");

                    UpdateMainMessage("受付嬢：なお、本対戦はダンジョンへ赴く前に開催とさせていただきます。");

                    UpdateMainMessage("アイン：前にも聞いたけどさ。　何でダンジョン行くのが禁止なんだ？");

                    UpdateMainMessage("受付嬢：禁止ではありません。ルールです。");

                    UpdateMainMessage("受付嬢：本日、DUEL闘技場へとお越しくださいますよう、よろしくお願いします。");

                    UpdateMainMessage("アイン：いや、ダンジョンで少し強くなってからと思うんだが、どうだろう？");

                    UpdateMainMessage("受付嬢：認められておりません。");

                    UpdateMainMessage("受付嬢：以上です。　それでは。");

                    UpdateMainMessage("アイン：っちょ、待ってくれ。");

                    UpdateMainMessage("受付嬢：なんでしょうか。");

                    UpdateMainMessage("アイン：対戦相手は男性なのか？　それとも女性なのか？");

                    UpdateMainMessage("受付嬢：お答えいたしかねます。　それでは。");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = "受付嬢は立ち去っていった・・・";
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：まあ、名前からして女性って気もするが・・・");

                    UpdateMainMessage("アイン：女性相手だとやりにくいんだよなぁ・・・");

                    UpdateMainMessage("アイン：でもまあ、DUELだしな、万全の体制でいくか！", true);
                }
                else if ((type == SupportType.FromDuelGate) ||
                         (type == SupportType.FromDungeonGate))
                {
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);
                    UpdateMainMessage("　　【受付嬢：アイン様、お待ちしておりました。】");

                    if (type == SupportType.FromDungeonGate)
                    {
                        UpdateMainMessage("アイン：Duelタイムってわけか。");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：よお、受付さん。約束どおり来たぜ。");
                    }

                    UpdateMainMessage("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】");

                    UpdateMainMessage("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】");

                    UpdateMainMessage("アイン：ああ、準備は万端だ。転送してくれ。");

                    CallSomeMessageWithAnimation("アインはDUEL闘技場へ転送されました");

                    UpdateMainMessage("カーティン：私、最近調子が良いのよ。さあ、私の糧になってちょうだい。");

                    UpdateMainMessage("アイン：どうかな。そう簡単に糧になりはしないぜ。");

                    UpdateMainMessage("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】");

                    UpdateMainMessage("カーティン：容赦しないわよ。");

                    UpdateMainMessage("アイン：こっちも本気で行くぜ！");

                    we.TruthDuelMatch4 = true;
                }
            }
            #endregion
            #region "ジェダ・アルス"
            else if (levelBorder[4] <= mc.Level && !we.TruthDuelMatch5 && !we.TruthCompleteArea1)
            {
                if (type == SupportType.Begin)
                {
                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("アイン：ん・・・５時４５分か。ちょっと早いけど起きるかな。");

                    UpdateMainMessage("アイン：おはよう、おばちゃん。");

                    UpdateMainMessage("ハンナ：ああ、おはよう。");

                    UpdateMainMessage("ハンナ：アイン、また来てるわよ、受付のお嬢ちゃん。");

                    UpdateMainMessage("アイン：ああ、ちょっと顔を出してくる。");

                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    UpdateMainMessage("受付嬢：アイン様、お待ちしておりました。");

                    UpdateMainMessage("アイン：もう来てるんだな。　いつも何時ぐらいに起きてるんだ？");

                    UpdateMainMessage("受付嬢：お答えいたしかねます。");

                    UpdateMainMessage("アイン：ぐぁ・・・");

                    UpdateMainMessage("受付嬢：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。");

                    UpdateMainMessage("受付嬢：対戦相手は『" + OpponentDuelist + "』様となっております。");

                    UpdateMainMessage("アイン：ジェダ！？あの、大金持ち暮らしのボンボンかよ！？");

                    UpdateMainMessage("受付嬢：はい");

                    UpdateMainMessage("受付嬢：なお、本対戦はダンジョンへ赴く前に開催とさせていただきます。");

                    UpdateMainMessage("アイン：不慮の事故で、開催されなかった事って今まであるのか？");

                    UpdateMainMessage("受付嬢：ありません");

                    UpdateMainMessage("受付嬢：本日、DUEL闘技場へとお越しくださいますよう、よろしくお願いします。");

                    UpdateMainMessage("受付嬢：以上です。　それでは。");

                    UpdateMainMessage("アイン：もう１つ聞きたい事があるんだが。");

                    UpdateMainMessage("受付嬢：なんでしょうか。　手短にお願いします。");

                    UpdateMainMessage("アイン：DUELで勝ち続けると、何か褒美みたいなのはあるのか？");

                    UpdateMainMessage("受付嬢：ありません");
                    
                    UpdateMainMessage("受付嬢：それでは");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = "受付嬢は立ち去っていった・・・";
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：しかし、スパっと言い放って帰っていくな・・・");

                    UpdateMainMessage("アイン：さてと、じゃあDUELの準備でもするかな！", true);
                }
                else if ((type == SupportType.FromDuelGate) ||
                         (type == SupportType.FromDungeonGate))
                {
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);
                    UpdateMainMessage("　　【受付嬢：アイン様、お待ちしておりました。】");

                    if (type == SupportType.FromDungeonGate)
                    {
                        UpdateMainMessage("アイン：Duelタイムってわけか。");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：よお、受付さん。約束どおり来たぜ。");
                    }

                    UpdateMainMessage("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】");

                    UpdateMainMessage("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】");

                    UpdateMainMessage("アイン：ああ、準備は万端だ。転送してくれ。");

                    CallSomeMessageWithAnimation("アインはDUEL闘技場へ転送されました");

                    UpdateMainMessage("ジェダ：また弱そうなヤツが来た（笑）");

                    UpdateMainMessage("アイン：なら、倒してみろよ。");

                    UpdateMainMessage("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】");

                    UpdateMainMessage("ジェダ：クスクス・・・じゃ、やろっか（笑）");

                    UpdateMainMessage("アイン：行くぜ！");

                    we.TruthDuelMatch5 = true;
                }
            }
            #endregion
            #region "シニキア・ヴェイルハンツ"
            else if (levelBorder[5] <= mc.Level && !we.TruthDuelMatch6 && !we.TruthCompleteArea1)
            {
                if (type == SupportType.Begin)
                {
                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("アイン：ん・・・６時か。そろそろ起きないとな。");

                    UpdateMainMessage("アイン：おはよう、おばちゃん。");

                    UpdateMainMessage("ハンナ：ああ、おはよう。");

                    UpdateMainMessage("ハンナ：アイン、DUEL受付の嬢ちゃんだよ。");

                    UpdateMainMessage("アイン：ああ、顔を出してくる。　了解。");

                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    UpdateMainMessage("受付嬢：アイン様、お待ちしておりました。");

                    UpdateMainMessage("アイン：よお、受付さん。おはよう！");

                    UpdateMainMessage("受付嬢：・・・");
                    
                    UpdateMainMessage("受付嬢：おはようございます。");                  
                    
                    UpdateMainMessage("受付嬢：さて、アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。");

                    UpdateMainMessage("受付嬢：対戦相手は『" + OpponentDuelist + "』様となっております。");

                    UpdateMainMessage("アイン：シニキア・・・マジかよ・・・");

                    UpdateMainMessage("受付嬢：なお、本対戦はダンジョンへ赴く前に開催とさせていただきます。");

                    UpdateMainMessage("アイン：DUEL闘技場もダンジョンも行かない場合は、どうなるんだ？");

                    UpdateMainMessage("受付嬢：必ずDUEL闘技場へお越しください。");

                    UpdateMainMessage("アイン：いや、いやいやちょっと立て込んでて行けない場合は？");

                    UpdateMainMessage("受付嬢：私のほうが、そちらへ赴きます。");

                    UpdateMainMessage("アイン：それで強制転送を使うのか？");

                    UpdateMainMessage("受付嬢：はい");

                    UpdateMainMessage("アイン：俺が逃げたらどうなるんだよ？");

                    UpdateMainMessage("受付嬢：お答えいたしかねます。");

                    UpdateMainMessage("アイン：ひょっとして、絶対逃げ切れないって言ってる？");

                    UpdateMainMessage("受付嬢：お答えいたしかねます。");

                    UpdateMainMessage("受付嬢：本日、DUEL闘技場へとお越しくださいますよう、よろしくお願いします。");

                    UpdateMainMessage("受付嬢：以上です。　それでは。");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = "受付嬢は立ち去っていった・・・";
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：・・・逃げないでおくか。逃げるだけ無駄って感じもするし。");

                    UpdateMainMessage("アイン：さてと、じゃあDUELの準備でもするかな！", true);
                }
                else if ((type == SupportType.FromDuelGate) ||
                         (type == SupportType.FromDungeonGate))
                {
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    if (type == SupportType.FromDungeonGate)
                    {
                        UpdateMainMessage("アイン：Duelタイムってわけか。");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：よお、受付さん。約束どおり来たぜ。");
                    }

                    UpdateMainMessage("　　【受付嬢：アイン様、お待ちしておりました。】");

                    UpdateMainMessage("アイン：Duelタイムってわけか。");

                    UpdateMainMessage("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】");

                    UpdateMainMessage("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】");

                    UpdateMainMessage("アイン：ああ、準備は万端だ。転送してくれ。");

                    CallSomeMessageWithAnimation("アインはDUEL闘技場へ転送されました");

                    UpdateMainMessage("ヴェイルハンツ：ランディスの弟子か。ちょうど良い。");

                    UpdateMainMessage("アイン：その名前！まさか、カール爵の！？");

                    UpdateMainMessage("ヴェイルハンツ：父の魔道こそ一番だと、ここで証明してみせよう。");

                    UpdateMainMessage("アイン：ッハッハッハ！あぁ、見せてみろよ！");

                    UpdateMainMessage("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】");

                    UpdateMainMessage("ヴェイルハンツ：では、勝負！");

                    UpdateMainMessage("アイン：しゃ、来い！");

                    we.TruthDuelMatch6 = true;
                }
            }
            #endregion
            #region "アデル・ブリガンディ"
            else if (levelBorder[6] <= mc.Level && !we.TruthDuelMatch7 && !we.TruthCompleteArea2)
            {
                if (type == SupportType.Begin)
                {
                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("アイン：ん・・・６時か。そろそろ起きないとな。");

                    UpdateMainMessage("アイン：おはよう、おばちゃん。");

                    UpdateMainMessage("ハンナ：ああ、おはよう。");

                    UpdateMainMessage("ハンナ：アイン、また来てるわよ、受付のお嬢ちゃん。");

                    UpdateMainMessage("アイン：ああ、ちょっと顔を出してくる。");

                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    UpdateMainMessage("受付嬢：アイン様、お待ちしておりました。");

                    UpdateMainMessage("アイン：おはよう！　受付さん。");

                    UpdateMainMessage("受付嬢：おはようございます。");

                    UpdateMainMessage("受付嬢：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。");

                    UpdateMainMessage("アイン：相手は？");

                    UpdateMainMessage("受付嬢：対戦相手は『" + OpponentDuelist + "』様となっております。");

                    UpdateMainMessage("アイン：聞いた事がねえな・・・誰なんだ？");

                    UpdateMainMessage("受付嬢：・・・お答え");

                    UpdateMainMessage("アイン：ああ、いやいやひとり言だ、悪い悪ぃ。");

                    UpdateMainMessage("受付嬢：・・・");
                    
                    UpdateMainMessage("受付嬢：そうですか");

                    UpdateMainMessage("アイン：そういえば、聞いておきたかったんだが・・・");

                    UpdateMainMessage("受付嬢：なお、本対戦はダンジョンへ赴く前に開催とさせていただきます。");

                    UpdateMainMessage("アイン：受付さん、って毎回呼びにくいんだが・・・");
                    
                    UpdateMainMessage("受付嬢：本日、DUEL闘技場へとお越しくださいますよう、よろしくお願いします。");

                    UpdateMainMessage("アイン：受付さん、名前は？");

                    UpdateMainMessage("受付嬢：連絡は以上です。　それでは。");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = "受付嬢は立ち去っていった・・・";
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：流れるような定型文と立ち去り方・・・ある意味すげぇ・・・");

                    UpdateMainMessage("アイン：っしゃ、DUELの準備と行くか！", true);
                }
                else if ((type == SupportType.FromDuelGate) ||
                         (type == SupportType.FromDungeonGate))
                {
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    UpdateMainMessage("　　【受付嬢：アイン様、お待ちしておりました。】");

                    if (type == SupportType.FromDungeonGate)
                    {
                        UpdateMainMessage("アイン：Duelタイムってわけか。");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：よお、受付さん。約束どおり来たぜ。");
                    }

                    UpdateMainMessage("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】");

                    UpdateMainMessage("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】");

                    UpdateMainMessage("アイン：ああ、準備は万端だ。転送してくれ。");

                    CallSomeMessageWithAnimation("アインはDUEL闘技場へ転送されました");

                    UpdateMainMessage("ブリガンディ：ッハン、こんな奴かよ・・・");

                    UpdateMainMessage("アイン：悪かったな、こんな奴で。");

                    UpdateMainMessage("ブリガンディ：言っておくが、お前みたいなタイプは何人も倒してきてる。");

                    UpdateMainMessage("ブリガンディ：今回も同じように勝たせてもらうぜ。");

                    UpdateMainMessage("アイン：だったら、同じパターンで倒してみろよ。");

                    UpdateMainMessage("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】");

                    UpdateMainMessage("ブリガンディ：一瞬で終わらせてやるぜ！");

                    UpdateMainMessage("アイン：負けてたまるか！！");

                    we.TruthDuelMatch7 = true;
                }
            }
            #endregion
            #region "レネ・コルトス"
            else if (levelBorder[7] <= mc.Level && !we.TruthDuelMatch8 && !we.TruthCompleteArea2)
            {
                if (type == SupportType.Begin)
                {
                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("アイン：ん・・・６時か。そろそろ起きないとな。");

                    UpdateMainMessage("アイン：おはよう、おばちゃん。");

                    UpdateMainMessage("ハンナ：ああ、おはよう。");

                    UpdateMainMessage("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。");

                    UpdateMainMessage("アイン：ああ、ちょっと顔を出してくる。");

                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    UpdateMainMessage("受付嬢：アイン様、お待ちしておりました。");

                    UpdateMainMessage("アイン：いやあ、おはようおはよう！");

                    UpdateMainMessage("受付嬢：・・・（ゴホン）");

                    UpdateMainMessage("受付嬢：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。");

                    UpdateMainMessage("アイン：相手は？");

                    UpdateMainMessage("受付嬢：対戦相手は『" + OpponentDuelist + "』様となっております。");

                    UpdateMainMessage("アイン：どんな奴なんだ？って聞いても駄目か・・・");

                    UpdateMainMessage("受付嬢：・・・気弱で、引っ込むタイプのようです。");

                    UpdateMainMessage("アイン：聞いた俺が悪かった、じゃな。");

                    UpdateMainMessage("受付嬢：・・・（ッゴホン）");
                    
                    UpdateMainMessage("アイン：え！？いや、聞いてなかった、もう１回頼む！");

                    UpdateMainMessage("受付嬢：対戦相手の特徴はお教えする事は出来ません。");

                    UpdateMainMessage("アイン：し、しまったあぁぁぁ・・・もう１回後生だぜ！！");
                    
                    UpdateMainMessage("受付嬢：気弱で引っ込むタイプです。そのせいか、一度劣勢になると防御に戦術を切り替えます。");

                    UpdateMainMessage("アイン：なるほど・・・気弱で引っ込むタイプ・・・劣勢時は防御・・・");

                    UpdateMainMessage("アイン：あれ？何でそんな気弱な奴がＤＵＥＬに参戦してるんだよ？");

                    UpdateMainMessage("受付嬢：それぞれの想いがあるのでしょう。我々はそこまで関与しておりません。");

                    UpdateMainMessage("受付嬢：連絡は以上です。　それでは。");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = "受付嬢は立ち去っていった・・・";
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：特徴を教えたら駄目だったんじゃねえのか？");

                    UpdateMainMessage("アイン：・・・まあいいや、DUELの準備と行くか！", true);
                }
                else if ((type == SupportType.FromDuelGate) ||
                         (type == SupportType.FromDungeonGate))
                {
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    UpdateMainMessage("　　【受付嬢：アイン様、お待ちしておりました。】");

                    if (type == SupportType.FromDungeonGate)
                    {
                        UpdateMainMessage("アイン：Duelタイムってわけか。");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：よお、受付さん。約束どおり来たぜ。");
                    }

                    UpdateMainMessage("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】");

                    UpdateMainMessage("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】");

                    UpdateMainMessage("アイン：ああ、準備は万端だ。転送してくれ。");

                    CallSomeMessageWithAnimation("アインはDUEL闘技場へ転送されました");

                    UpdateMainMessage("レネ：スミマセン・・・");

                    UpdateMainMessage("アイン：ん？何がだ？");

                    UpdateMainMessage("レネ：あの・・・わ、わたし、剣装備なんですけど・・・");

                    UpdateMainMessage("アイン：お、おぉ、そうみたいだな。（何か調子狂うな・・・）");

                    UpdateMainMessage("レネ：あ・・・あの、卑怯だと思ったら許してください、スミマセン！！");

                    UpdateMainMessage("アイン：わ、わかったわかった。卑怯もクソもねえから、心配するなって。");

                    UpdateMainMessage("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】");

                    UpdateMainMessage("レネ：ス、スミマセン！　い、い、いきますね！！");

                    UpdateMainMessage("アイン：っしゃ、かかってこい！");

                    we.TruthDuelMatch8 = true;
                }
            }
            #endregion
            #region "スコーティ・ザルゲ"
            else if (levelBorder[8] <= mc.Level && !we.TruthDuelMatch9 && !we.TruthCompleteArea2)
            {
                if (type == SupportType.Begin)
                {
                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("アイン：ん・・・６時か。そろそろ起きないとな。");

                    UpdateMainMessage("アイン：おはよう、おばちゃん。");

                    UpdateMainMessage("ハンナ：ああ、おはよう。");

                    UpdateMainMessage("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。");

                    UpdateMainMessage("アイン：ああ、ちょっと顔を出してくる。");

                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    UpdateMainMessage("受付嬢：アイン様、お待ちしておりました。");

                    UpdateMainMessage("アイン：おお、いつも早いな！");

                    UpdateMainMessage("受付嬢：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。");

                    UpdateMainMessage("受付嬢：対戦相手は『" + OpponentDuelist + "』様となっております。");

                    UpdateMainMessage("アイン：ザルゲ・・・聞いた事があるな。");

                    UpdateMainMessage("受付嬢：残虐非道な行為、狡猾な戦術。");

                    UpdateMainMessage("アイン：ああ、巷じゃ結構有名だしな。");

                    UpdateMainMessage("アイン：って受付嬢さんは、俺に情報提供しちゃ駄目なんじゃないのか？");
                    
                    UpdateMainMessage("アイン：というか、受付嬢さんって言い難いんだが・・・名前を教えてくれないか。");

                    UpdateMainMessage("受付嬢：・・・");
                    
                    UpdateMainMessage("受付嬢：ミシェル・キーナと言います。");

                    UpdateMainMessage("アイン：ミシェル・キーナさんっと・・・了解了解。じゃあ、キーナさんで良いか？");

                    UpdateMainMessage("受付嬢：お答えいたしかねます。");

                    UpdateMainMessage("アイン：マジかよ・・・そこでお答えできねえってか。まあ良いけど。");

                    UpdateMainMessage("受付嬢：アイン・ウォーレンス様。");

                    UpdateMainMessage("アイン：お、おお何だ？");

                    UpdateMainMessage("受付嬢：本日の対戦、勝っていただけますか？");

                    UpdateMainMessage("アイン：おお、いつもそのつもりだぜ。それがどうした？");

                    UpdateMainMessage("受付嬢：私、あのような非道な人間にいつまでも勝って欲しくはありません。");

                    UpdateMainMessage("受付嬢：アイン様、どうか・・・");

                    UpdateMainMessage("アイン：任せておけって、どんな奴でもＤＵＥＬでは公平だ。");

                    UpdateMainMessage("アイン：必ずぶちのめしてやるさ、ッハッハッハ！");

                    UpdateMainMessage("受付嬢：・・・お願いいたします、アイン様には期待しておりますので。");
                    
                    UpdateMainMessage("受付嬢：それでは。");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = "受付嬢は立ち去っていった・・・";
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：キーナさん、ああやってしゃべると、案外普通の人なんだよな・・・");

                    UpdateMainMessage("アイン：っさてと、DUELの準備と行くか！", true);
                }
                else if ((type == SupportType.FromDuelGate) ||
                         (type == SupportType.FromDungeonGate))
                {
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    UpdateMainMessage("　　【受付嬢：アイン様、お待ちしておりました。】");

                    if (type == SupportType.FromDungeonGate)
                    {
                        UpdateMainMessage("アイン：Duelタイムってわけか。");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：よお、受付さん。約束どおり来たぜ。");
                    }

                    UpdateMainMessage("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】");

                    UpdateMainMessage("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】");

                    UpdateMainMessage("アイン：ああ、準備は万端だ。転送してくれ。");

                    CallSomeMessageWithAnimation("アインはDUEL闘技場へ転送されました");

                    UpdateMainMessage("ザルゲ：ヒュゥ！　アイン・ウォーレンス様のご登場ってワケか！！");

                    UpdateMainMessage("アイン：ああ、悪かったか？");

                    UpdateMainMessage("ザルゲ：とんっでもゴザイマセン、アイン様。");

                    UpdateMainMessage("ザルゲ：誠心誠意持って、戦わせていただきたく存じます・・・");

                    UpdateMainMessage("ザルゲ：ってかぁ！？　ゲハハハハハハハ！！");

                    UpdateMainMessage("アイン：その手には乗らねえぜ。準備は万端だ。");

                    UpdateMainMessage("ザルゲ：っおうおうおう、キッチリしてやがんなぁ。ツマンねえ野郎だ。");

                    UpdateMainMessage("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】");

                    UpdateMainMessage("ザルゲ：おう、待て待てって、キーナ嬢ちゃん！！　そう焦るなよ！？");

                    UpdateMainMessage("ザルゲ：次に俺が勝つと、てめぇが俺の嫁さん決定って約束だったかなぁ！？");

                    UpdateMainMessage("　　【受付嬢：！！！】");

                    UpdateMainMessage("ザルゲ：ッヒャヒャヒャ、てめぇのご両親の借金チャラって事にしてやんだぞ！？");
                    
                    UpdateMainMessage("ザルゲ：悪いがこの婚約。ご両親からは快く承諾してもらってるって話だぁ！");

                    UpdateMainMessage("ザルゲ：しょうがねえから、勝ってやるんだぜ、ありがたく思えよ！？");

                    UpdateMainMessage("ザルゲ：ゲハハハハハハハハハ！！");

                    UpdateMainMessage("　　【受付嬢：デュ・・・DUEL開始となります！】");

                    UpdateMainMessage("アイン：（そういう事か・・・どうりで、朝のあの言い方・・・）");

                    UpdateMainMessage("ザルゲ：アイン・ウォーレンス様、お手柔らかに、ックッックククハハハ！！");

                    UpdateMainMessage("アイン：悪いが、俺には関係ない話だ。");

                    UpdateMainMessage("ザルゲ：・・・あぁ！？");

                    UpdateMainMessage("アイン：好きにするが良いさ。");

                    UpdateMainMessage("アイン：だが、ＤＵＥＬは真剣勝負。");

                    UpdateMainMessage("アイン：私情は関係ねえ、とにかく勝たせてもらう。");

                    UpdateMainMessage("ザルゲ：ッッッソッタレ、アイン・ウォーレンスよぉ！　ムカツくぜえぇぇ！！");

                    UpdateMainMessage("アイン：サッサと始めるぜ！　勝負だ！！");

                    we.TruthDuelMatch9 = true;
                }
            }
            #endregion
            #region "ペルマ・ワラミィ"
            else if (levelBorder[9] <= mc.Level && !we.TruthDuelMatch10 && !we.TruthCompleteArea2)
            {
                if (type == SupportType.Begin)
                {
                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("アイン：ん・・・６時か。そろそろ起きないとな。");

                    UpdateMainMessage("アイン：おはよう、おばちゃん。");

                    UpdateMainMessage("ハンナ：ああ、おはよう。");

                    UpdateMainMessage("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。");

                    UpdateMainMessage("アイン：ああ、ちょっと顔を出してくる。");

                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    UpdateMainMessage(KIINA + "：アイン様、お待ちしておりました。");

                    UpdateMainMessage("アイン：おはよう、キーナさん。");

                    if (we.DuelWinZalge)
                    {
                        UpdateMainMessage(KIINA + "：・・・おはようございます。");
                    }

                    UpdateMainMessage(KIINA + "：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。");

                    UpdateMainMessage(KIINA + "：対戦相手は『" + OpponentDuelist + "』様となっております。");

                    UpdateMainMessage("アイン：ワラミィさんって、クヴェルタ街の魔法武具専門ショップのオーナーだよな？");

                    if (we.DuelWinZalge)
                    {
                        UpdateMainMessage(KIINA + "：はい。魔法武器を駆使した戦術を編み出したそうで最近は非常に好調のようです。");
                    }

                    UpdateMainMessage("アイン：魔法武具のオーナー・・・どんなのが飛び出してくるやら・・・");

                    UpdateMainMessage("アイン：なあ、キーナさん。その辺の戦術をコッソリと・・・");

                    if (we.DuelWinZalge)
                    {
                        UpdateMainMessage(KIINA + "：ア、アイン様！");

                        UpdateMainMessage(KIINA + "：前回はありがとうございました！");

                        UpdateMainMessage("アイン：うお！？何だいきなり、どうしたんだ？");

                        UpdateMainMessage(KIINA + "：っその・・・アイン様が負けていたら、わたしは今頃・・・");

                        UpdateMainMessage(KIINA + "：っう・・・うう・・・ありがとう・・・ございました・・・");

                        UpdateMainMessage("　　　（" + KIINA + "はその場で泣き崩れてしまった！）");

                        UpdateMainMessage("アイン：っちょ！っちょちょちょ、タンマ！！");

                        UpdateMainMessage("アイン：分かった、気持ちは分かった！　だから泣き止んでくれ、っな！？");

                        UpdateMainMessage(KIINA + "：でも、もしザルゲが勝っていたらと思うと・・・うう・・・");

                        UpdateMainMessage(KIINA + "：あの非道なる男を前に、次々と対戦相手は倒れ・・・");

                        UpdateMainMessage(KIINA + "：相手を騙す戦術が得意で、生真面目タイプの対戦相手は大概が・・・");

                        UpdateMainMessage("アイン：まあ、確かにそれなりの戦術だったよな。");

                        UpdateMainMessage("アイン：口は悪いけど、単なる３流だと思って挑んだヤツは負けて当然だろう。");

                        UpdateMainMessage(KIINA + "：３０連戦、連勝したら・・・という約束でした。");

                        UpdateMainMessage(KIINA + "：あんな非道な男、勝てるはずが無い。そう思って約束して・・・");

                        UpdateMainMessage("アイン：バカ、気を付けろよ。ああいうヤツに限って、戦術に長けてるんだ。");

                        UpdateMainMessage(KIINA + "：っう・・・うう・・・すいません・・・");

                        UpdateMainMessage(KIINA + "：連戦３０戦目・・・アイン様で・・・わたし、本当によかった！！");

                        UpdateMainMessage(KIINA + "：っう・・・うう・・・");

                        UpdateMainMessage("アイン：い、いやいやいや、タンマ！！頼むから泣かないでくれ、っな！？");

                        UpdateMainMessage(KIINA + "：す、すいません・・・");

                        UpdateMainMessage("アイン：ふう・・・とにかく、くだらない約束はするなって事だ、気を付けろよな。");

                        UpdateMainMessage(KIINA + "：え、ええ・・・");
                    }
                    else
                    {
                        UpdateMainMessage(KIINA + "：お答えいたしかねます。");

                        UpdateMainMessage("アイン：ハハハ・・・まあ、そりゃそうだよな。");
                    }
                    
                    UpdateMainMessage("アイン：さて、じゃあ魔法対策でもやっておくかな。");

                    if (we.DuelWinZalge)
                    {
                        UpdateMainMessage(KIINA + "：アイン様、このまま勝ち続けてくださる事を祈ってます。");

                        UpdateMainMessage("アイン：嬉しい事言ってくれるじゃねえか、任せておけ！　ッハッハッハ！");

                        UpdateMainMessage(KIINA + "：ありがとうございました。　今後もがんばってください、それでは。");
                    }
                    else
                    {
                        UpdateMainMessage(KIINA + "：連絡は以上です。　それでは。");
                    }
                    
                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = KIINA + "は立ち去っていった・・・";
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：っさてと、DUELの準備と行くか！", true);
                }
                else if ((type == SupportType.FromDuelGate) ||
                         (type == SupportType.FromDungeonGate))
                {
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    UpdateMainMessage("　　【受付嬢：アイン様、お待ちしておりました。】");

                    if (type == SupportType.FromDungeonGate)
                    {
                        UpdateMainMessage("アイン：Duelタイムってわけか。");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：よお、受付さん。約束どおり来たぜ。");
                    }

                    UpdateMainMessage("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】");

                    UpdateMainMessage("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】");

                    UpdateMainMessage("アイン：ああ、準備は万端だ。転送してくれ。");

                    CallSomeMessageWithAnimation("アインはDUEL闘技場へ転送されました");

                    UpdateMainMessage("ワラミィ：お主がアイン・ウォーレンスじゃな？");

                    UpdateMainMessage("アイン：ああ、そうだ。");

                    UpdateMainMessage("ワラミィ：ふむ、ガンツめ、良いヤツを近くに置いとるな。");

                    UpdateMainMessage("アイン：ッハハ、ガンツ伯父さんと知り合いなんですね？");

                    UpdateMainMessage("ワラミィ：当たり前じゃ。ワシとガンツは互いにライバルのようなもの。");

                    UpdateMainMessage("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】");

                    UpdateMainMessage("アイン：よろしくお願いします！");

                    UpdateMainMessage("ワラミィ：良き心構えじゃな。　それでは、始めさせてもらおうかの。");

                    we.TruthDuelMatch10 = true;
                }
            }
            #endregion
            #region "キルト・ジョルジュ"
            else if (levelBorder[10] <= mc.Level && !we.TruthDuelMatch11 && !we.TruthCompleteArea2)
            {
                if (type == SupportType.Begin)
                {
                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("アイン：ん・・・６時か。そろそろ起きないとな。");

                    UpdateMainMessage("アイン：おはよう、おばちゃん。");

                    UpdateMainMessage("ハンナ：ああ、おはよう。");

                    UpdateMainMessage("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。");

                    UpdateMainMessage("アイン：ああ、ちょっと顔を出してくる。");

                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    UpdateMainMessage(KIINA + "：アイン様、お待ちしておりました。");

                    UpdateMainMessage("アイン：いつもわざわざ連絡ありがとうな。");

                    if (we.DuelWinZalge)
                    {
                        UpdateMainMessage(KIINA + "：・・・いえ、お構いなく。");
                    }

                    UpdateMainMessage(KIINA + "：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。");

                    UpdateMainMessage(KIINA + "：対戦相手は『" + OpponentDuelist + "』様となっております。");

                    UpdateMainMessage("アイン：っげげ！！　何で現国王の息子と戦わなくちゃならないんだよ！？");

                    UpdateMainMessage("アイン：って、何でエントリーされてんだよ！？");

                    UpdateMainMessage("アイン：って、何で俺が対戦相手になってんだよ！？");

                    if (we.DuelWinZalge)
                    {
                        UpdateMainMessage(KIINA + "：・・・弱りました。お答えいたしかねます。");
                    }
                    else
                    {
                        UpdateMainMessage(KIINA + "：お答えいたしかねます。");
                    }

                    UpdateMainMessage("アイン：い、いやいやビックリしただけだって。　別に答えなくていいけど。");

                    UpdateMainMessage("アイン：しかし、参ったなあぁ・・・国王の家訓ってアレだろ？");

                    UpdateMainMessage("アイン：聡明！");

                    UpdateMainMessage("アイン：湧源！！");

                    UpdateMainMessage("アイン：高潔！！！");

                    UpdateMainMessage("アイン：とか何とか、そういう家訓が山のようにあるって話じゃねえか。");

                    UpdateMainMessage(KIINA + "：その家訓は日々増え続けており、今では２５０種にも及ぶようです。");

                    UpdateMainMessage("アイン：うげ・・・あり得ねえ・・・");

                    UpdateMainMessage("アイン：そんなふうに鍛えられたヤツが相手かよ。");

                    UpdateMainMessage("アイン：そんなのに限って、結構チャランポランなヤツだったりして・・・");

                    if (we.DuelWinZalge)
                    {
                        UpdateMainMessage(KIINA + "：次期国王の筆頭であるキルト様は、毎晩３時まで武術稽古を欠かさないとの噂です。");

                        UpdateMainMessage(KIINA + "：早朝の起床５時。学を習得し、努めてから朝食に入るとの噂もあります。");

                        UpdateMainMessage(KIINA + "：その後も、実践経験を積むため、全方位的転送装置で各地方を巡回し、一日を終始されるそうです。");

                        UpdateMainMessage("アイン：ハハハ・・・笑えねえ・・・");

                        UpdateMainMessage(KIINA + "：でも・・・");
                        
                        UpdateMainMessage(KIINA + "：アイン様も日々ダンジョンで鍛錬されているそうですね。");

                        UpdateMainMessage("アイン：あ、ああ。鍛錬って言うのとは、少し違うけどな。");

                        UpdateMainMessage(KIINA + "：わたしはアイン様を応援してます。　がんばってくださいね。");

                        UpdateMainMessage("アイン：おお、任せておけ！");
                        
                        UpdateMainMessage("アイン：国王の息子だろうが関係ねえ、ぶち当たるまでさ！");

                        UpdateMainMessage(KIINA + "：クスクス、国王が今のを聞いたらきっと笑うでしょうね。");

                        UpdateMainMessage("アイン：ハハハ・・・ちょっと言い過ぎたかもな。");

                        UpdateMainMessage(KIINA + "：あっ、すみません。少し長くなったみたいですので・・・");
                        
                        UpdateMainMessage(KIINA + "：連絡は以上です。　それでは。");

                        UpdateMainMessage("アイン：ああ、またな。");
                    }
                    else
                    {
                        UpdateMainMessage(KIINA +"：それでは、連絡も済ませたので、私はこれで。");

                        UpdateMainMessage("アイン：あ、ああ。");

                    }

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = KIINA + "は立ち去っていった・・・";
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：っさてと、DUELの準備と行くか！", true);
                }
                else if ((type == SupportType.FromDuelGate) ||
                         (type == SupportType.FromDungeonGate))
                {
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    UpdateMainMessage("　　【受付嬢：アイン様、お待ちしておりました。】");

                    if (type == SupportType.FromDungeonGate)
                    {
                        UpdateMainMessage("アイン：Duelタイムってわけか。");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：よお、受付さん。約束どおり来たぜ。");
                    }

                    UpdateMainMessage("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】");

                    UpdateMainMessage("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】");

                    UpdateMainMessage("アイン：ああ、準備は万端だ。転送してくれ。");

                    CallSomeMessageWithAnimation("アインはDUEL闘技場へ転送されました");

                    UpdateMainMessage("アイン：（・・・出たあぁ・・・王家のオーラがプンプンするぜ）");

                    UpdateMainMessage("キルト：アイン・ウォーレンス、ようやく会う事ができたか。");

                    UpdateMainMessage("アイン：おう、初顔合わせだよな？");

                    UpdateMainMessage("キルト：オル・ランディスの弟子と聞いているが？");

                    UpdateMainMessage("アイン：お、おお、一応それで合ってるはずだ。それがどうかしたか？");

                    UpdateMainMessage("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】");

                    UpdateMainMessage("　　　（対戦相手キルトは剣の切っ先を丁寧にアインへ向け・・・）");

                    UpdateMainMessage("キルト：相手に不足は無い。");
                    
                    UpdateMainMessage("キルト：全身全霊を持って挑ませていただく。");

                    UpdateMainMessage("　　　（キルトは凛とした態度で剣を真っ直ぐに構えた！）");

                    UpdateMainMessage("アイン：（この年でこの雰囲気・・・やるじゃねえか）");

                    UpdateMainMessage("アイン：こちらも準備オッケー。　いつでも行けるぜ。");

                    UpdateMainMessage("キルト：では、勝負！");

                    we.TruthDuelMatch11 = true;
                }
            }
            #endregion
            #region "ビリー・ラキ"
            else if (levelBorder[11] <= mc.Level && !we.TruthDuelMatch12 && !we.TruthCompleteArea3)
            {
                if (type == SupportType.Begin)
                {
                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("アイン：ん・・・６時か。そろそろ起きないとな。");

                    UpdateMainMessage("アイン：おはよう、おばちゃん。");

                    UpdateMainMessage("ハンナ：ああ、おはよう。");

                    UpdateMainMessage("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。");

                    UpdateMainMessage("アイン：ああ、ちょっと顔を出してくる。");

                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    UpdateMainMessage(KIINA + "：アイン様、お待ちしておりました。");

                    UpdateMainMessage("アイン：おう、おはようさん。");

                    UpdateMainMessage(KIINA + "：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。");

                    UpdateMainMessage(KIINA + "：対戦相手は『" + OpponentDuelist + "』様となっております。");

                    UpdateMainMessage("アイン：ビリー・ラキ？？・・・誰だ？");

                    if (we.DuelWinZalge)
                    {
                        UpdateMainMessage(KIINA + "：ビリー・ラキ様は、アイン様の事を良くご存知でおられるようです。");

                        UpdateMainMessage("アイン：そうなのか？アレ、誰だったっけ・・・やべ・・・");

                        UpdateMainMessage(KIINA + "：アイン様は確か、オルガウェイン傭兵訓練施設の卒業者でしたよね？");

                        UpdateMainMessage("アイン：ああ、そうだな。よく知ってるじゃねえか。");

                        UpdateMainMessage("アイン：ははーん、そういう事か。そういや、あの頃はライバルがゴロゴロいたっけな。");

                        UpdateMainMessage(KIINA + "：ライバルはよく記憶している者も中には居ます。アイン様は知らない間に倒していたのかもしれませんね。");

                        UpdateMainMessage("アイン：ん？ああ・・・そうなのかもしれん・・・");

                        UpdateMainMessage(KIINA + "：ビリーは私の幼馴馴染みなのですが、どうぞ心ゆくまで戦ってやってください。");

                        UpdateMainMessage("アイン：おお、そうなのか！？そりゃ奇遇だな、ッハッハッハ！");

                        UpdateMainMessage(KIINA + "：ビリーときたら、私じゃなくてラナさんの方にばかり・・・だから私だって・・・");

                        UpdateMainMessage("アイン：え？");

                        UpdateMainMessage(KIINA + "：っな、何でもありません！！！");

                        UpdateMainMessage("アイン：うわっと、怒るな怒るな・・・ハハッ・・・");

                        UpdateMainMessage(KIINA + "：・・・ッコホん");
                    }
                    else
                    {
                        UpdateMainMessage(KIINA + "：お答えいたしかねます。");

                        UpdateMainMessage("アイン：そ、そうか・・・ハハハ・・・");
                    }

                    UpdateMainMessage(KIINA + "：ビリーの戦術について少し解説いたします。");

                    UpdateMainMessage(KIINA + "：彼の戦術は基本殴って殴って、殴りまくる。それだけがモットーです。");

                    UpdateMainMessage("アイン：それだけなのか？");

                    UpdateMainMessage(KIINA + "：そのようです。");

                    UpdateMainMessage("アイン：ほかには？");

                    UpdateMainMessage(KIINA + "：後はご自分でお確かめください。");

                    UpdateMainMessage(KIINA + "：それでは、連絡は以上です。私はこれで。");

                    UpdateMainMessage("アイン：あ、ああ。");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = KIINA + "は立ち去っていった・・・";
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：っさてと、DUELの準備と行くか！", true);
                }
                else if ((type == SupportType.FromDuelGate) ||
                         (type == SupportType.FromDungeonGate))
                {
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    UpdateMainMessage("　　【受付嬢：アイン様、お待ちしておりました。】");

                    if (type == SupportType.FromDungeonGate)
                    {
                        UpdateMainMessage("アイン：Duelタイムってわけか。");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：よお、受付さん。約束どおり来たぜ。");
                    }

                    UpdateMainMessage("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】");

                    UpdateMainMessage("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】");

                    UpdateMainMessage("アイン：ああ、準備は万端だ。転送してくれ。");

                    CallSomeMessageWithAnimation("アインはDUEL闘技場へ転送されました");

                    UpdateMainMessage("ビリー：来たか、アイン・ウォーレンス！！");

                    UpdateMainMessage("アイン：お前は確か・・・っと・・・");
                    
                    UpdateMainMessage("アイン：ビリーであってるよな？");

                    UpdateMainMessage("ビリー：・・・・・・");

                    UpdateMainMessage("アイン：ラキ！　そうそう！");
                    
                    UpdateMainMessage("アイン：ビリー・ラキ！　久しぶりだな、ッハッハッハ！");

                    UpdateMainMessage("ビリー：うおおおぉぉ、まさか忘れたとは言わせねえぞおおぉぉぉ！！！");

                    UpdateMainMessage("アイン：悪ぃ、すまねえが後で教えてくれよホント。");

                    UpdateMainMessage("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】");

                    UpdateMainMessage("ビリー：くそ・・・完全に忘れてんのかよ！");

                    UpdateMainMessage("ビリー：そんな忘れグセの激しい貴様なんかに・・・");

                    UpdateMainMessage("ビリー：何でラナちゃんがああぁぁぁぁ！！！");

                    UpdateMainMessage("アイン：ッゲ、なんでそこでラナが出てくるんだよ？　関係ねえだろ！？");

                    UpdateMainMessage("ビリー：うっせぇ、うっせぇ！　コッチの話だ！");
                    
                    UpdateMainMessage("ビリー：構えろ！　絶対に貴様は潰す！！");

                    UpdateMainMessage("アイン：あ、ああ・・・じゃあ行くぜ！");

                    we.TruthDuelMatch12 = true;
                }
            }
            #endregion
            #region "アンナ・ハミルトン"
            else if (levelBorder[12] <= mc.Level && !we.TruthDuelMatch13 && !we.TruthCompleteArea3)
            {
                if (type == SupportType.Begin)
                {
                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("アイン：ん・・・６時か。そろそろ起きないとな。");

                    UpdateMainMessage("アイン：おはよう、おばちゃん。");

                    UpdateMainMessage("ハンナ：ああ、おはよう。");

                    UpdateMainMessage("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。");

                    UpdateMainMessage("アイン：ああ、ちょっと顔を出してくる。");

                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    UpdateMainMessage(KIINA + "：アイン様、お待ちしておりました。");

                    UpdateMainMessage("アイン：おう、おはようさん。");

                    UpdateMainMessage(KIINA + "：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。");

                    UpdateMainMessage(KIINA + "：対戦相手は『" + OpponentDuelist + "』様となっております。");

                    UpdateMainMessage("アイン：ハミルトンってどっかで聞いた事あるんだけどな・・・");

                    UpdateMainMessage(KIINA + "：ハミルトン家は代々ファージル宮殿の遊撃騎士団長を務めていますね。");

                    UpdateMainMessage("アイン：おおぉぉ、ソレだ！何か聞いた事あるなと思ってたけど。");

                    UpdateMainMessage("アイン：でも、アンナって名前は聞いた事がねえけどな。");

                    UpdateMainMessage(KIINA + "：ハミルトン家の現当主クメル・ハミルトンの長女ですね。");

                    UpdateMainMessage("アイン：何でそんなトコまで分かるんだよ！？");

                    UpdateMainMessage("アイン：って、そうか。DUEL対戦の経歴とかは受付さんには全部丸わかりだって噂だしな・・・");

                    if (we.DuelWinZalge)
                    {
                        UpdateMainMessage(KIINA + "：ご存じ無いのも無理はありません。世間一般には知られていませんからね。");

                        UpdateMainMessage("アイン：何かワケありって事なのか？");

                        UpdateMainMessage(KIINA + "：ファージル宮殿の騎士団長である以上・・・");

                        UpdateMainMessage("アイン：ああ、分かった。そういう事か。");

                        UpdateMainMessage("アイン：余計な面倒事、危険な事柄に巻き込ませないためか。");

                        UpdateMainMessage(KIINA + "：はい、その通りです。");

                        UpdateMainMessage("アイン：っで、その長女さんがこんな大舞台に出てきても大丈夫なのかよ？");

                        UpdateMainMessage("アイン：DUEL闘技場に出てきた以上、それはもう世間一般に知られるって事だろ？");

                        UpdateMainMessage(KIINA + "：ハミルトン家は元来より『武』に長けた家系なので、それを示す意味で参戦されてるのだと思われます。");

                        UpdateMainMessage("アイン：なるほど、DUEL闘技場で戦績を上げていれば、そう簡単に拉致出来る相手じゃねえって事になるな。");

                        UpdateMainMessage("アイン：しかし・・・");

                        UpdateMainMessage("アイン：俺は負けられねえ。勝たせてもらうぜ！");

                        UpdateMainMessage(KIINA + "：アンナ・ハミルトンは杖を装備したマジシャンタイプでありながら");

                        UpdateMainMessage(KIINA + "：折々にスキを見て、スタン効果の攻撃も繰り出してくるため");

                        UpdateMainMessage(KIINA + "：単純なコンボではありますが、強敵なのは間違いありません。");

                        UpdateMainMessage(KIINA + "：え、えっと・・・頑張ってください！");

                        UpdateMainMessage("アイン：ん？あ、ああ頑張ってくるぜ！ッハッハッハ！");
                    }

                    UpdateMainMessage(KIINA + "：それでは、連絡は以上です。私はこれで。");

                    UpdateMainMessage("アイン：ああ。");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = KIINA + "は立ち去っていった・・・";
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：っさてと、DUELの準備と行くか！", true);
                }
                else if ((type == SupportType.FromDuelGate) ||
                         (type == SupportType.FromDungeonGate))
                {
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    UpdateMainMessage("　　【受付嬢：アイン様、お待ちしておりました。】");

                    if (type == SupportType.FromDungeonGate)
                    {
                        UpdateMainMessage("アイン：Duelタイムってわけか。");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：よお、受付さん。約束どおり来たぜ。");
                    }

                    UpdateMainMessage("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】");

                    UpdateMainMessage("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】");

                    UpdateMainMessage("アイン：ああ、準備は万端だ。転送してくれ。");

                    CallSomeMessageWithAnimation("アインはDUEL闘技場へ転送されました");

                    UpdateMainMessage("アイン：っよっと、到着到着っと・・・");

                    UpdateMainMessage("アイン：おっ、アンナ・ハミルトンさんだな。よろしく頼むぜ！");

                    UpdateMainMessage("アンナ：っゆ・・・");

                    UpdateMainMessage("アイン：ん？");

                    UpdateMainMessage("アンナ：っ許せないわ！！　アイン・ウォーレンス！！");

                    UpdateMainMessage("アイン：どわぁ！　な、なんだよ一体いきなり！？");

                    UpdateMainMessage("アンナ：あなたさえ、出場しなければ・・・っく・・・");

                    UpdateMainMessage("アイン：ま、まあ・・・落ち着け。何の言いがかりか全く分からねえが・・・");

                    UpdateMainMessage("アンナ：キーナは・・・");

                    UpdateMainMessage("アンナ：キーナは私だけのものよ！！　アンタなんかに絶対渡さない！！");

                    UpdateMainMessage("アイン：っな、どういう話の展開だよ・・・");

                    UpdateMainMessage("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】");

                    UpdateMainMessage("アンナ：ここであなたは負けるの！そしてキーナには一切近づかないでちょうだい！！");

                    UpdateMainMessage("アイン：い、いやいや・・・まいったな・・・ハハ・・・");

                    UpdateMainMessage("アイン：悪いが、負けるつもりはねえ。");

                    UpdateMainMessage("アイン：勝たせてもらうぜ。");

                    UpdateMainMessage("アンナ：キーナをさらうつもりね。そうはさせないわ！！");

                    UpdateMainMessage("アイン：い、いや・・・");

                    UpdateMainMessage("アンナ：勝負よ！　アイン・ウォーレンス！！");

                    UpdateMainMessage("アイン：ああ、勝負だ！（妙なノリだな・・・）");

                    we.TruthDuelMatch13 = true;
                }
            }
            #endregion
            #region "カルマンズ・オーン"
            else if (levelBorder[13] <= mc.Level && !we.TruthDuelMatch14 && !we.TruthCompleteArea3)
            {
                if (type == SupportType.Begin)
                {
                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("アイン：ん・・・６時か。そろそろ起きないとな。");

                    UpdateMainMessage("アイン：おはよう、おばちゃん。");

                    UpdateMainMessage("ハンナ：ああ、おはよう。");

                    UpdateMainMessage("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。");

                    UpdateMainMessage("アイン：ああ、ちょっと顔を出してくる。");

                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    UpdateMainMessage(KIINA + "：アイン様、お待ちしておりました。");

                    UpdateMainMessage("アイン：おう、おはようさん。");

                    UpdateMainMessage(KIINA + "：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。");

                    UpdateMainMessage(KIINA + "：対戦相手は『" + OpponentDuelist + "』様となっております。");

                    UpdateMainMessage("アイン：オーン先生！！　出ているのかよ！？");

                    UpdateMainMessage(KIINA + "：傭兵訓練施設の誇示が目的で、毎年必ず施設内で選別されて出場をされていますね。");

                    UpdateMainMessage("アイン：マジであの先生かよ。。。結構骨が折れそうだな。");

                    if (we.DuelWinZalge)
                    {
                        UpdateMainMessage(KIINA + "：あの、アイン様。");

                        UpdateMainMessage("アイン：ん？");

                        UpdateMainMessage(KIINA + "：・・・カルマンズ・オーンの裏戦術。");

                        UpdateMainMessage(KIINA + "：ご存知ですか？");

                        UpdateMainMessage("アイン：おっと、待ってくれよ。");

                        UpdateMainMessage("アイン：そいつはさすがに受付嬢としては、規則違反に当たるんじゃねえのか？");

                        UpdateMainMessage(KIINA + "：！！");

                        UpdateMainMessage("アイン：まあ、この会話が誰にも聞かれてなければ通るだろうが。");

                        UpdateMainMessage("アイン：そんな事言ってたんじゃ、試合前の暗殺と同じ類になっちまうな。");

                        UpdateMainMessage(KIINA + "：す、すみません！　すみませんでした！！");

                        UpdateMainMessage("アイン：い、いやいやいや。そんな謝らなくて良いって。ッハハハ・・・");

                        UpdateMainMessage("アイン：俺なら大丈夫だって。");

                        UpdateMainMessage("アイン：裏戦術、表戦術、あるいはそれ以外の要素。");

                        UpdateMainMessage("アイン：それら全部ひっくるめてDUELだ。正々堂々行くぜ！");

                        UpdateMainMessage(KIINA + "：私の考え方が卑小だったんです。ご、ごめんなさい・・・っう・・・");

                        UpdateMainMessage("アイン：だだだ、だから良いって言ってんじゃねえか、っな！？その泣くな、泣くなって！");

                        UpdateMainMessage("アイン：そそ、そうだ。アレだ、いつもの基本戦術を解説してくれ。");

                        UpdateMainMessage(KIINA + "：・・・すみません。。そうですね、公となっている戦術を今から説明いたしましょう。");

                    }

                    UpdateMainMessage(KIINA + "：カルマンズ・オーンの戦術は大きく分けて、２種類あります。");

                    UpdateMainMessage(KIINA + "：１つ。　ライフ・コントロール。");

                    UpdateMainMessage(KIINA + "相手を倒しに行くのではなく、自分の方がライフアドバテンテージが高くする方法です。");

                    UpdateMainMessage("アイン：ああ、それは知ってる。あれは嫌でも嫌になるぜ。");

                    UpdateMainMessage(KIINA + "：２つ。　五月雨系の連撃。");

                    UpdateMainMessage(KIINA + "：タイミングはケースに応じてですが、突如連撃を繰り返し行なってきます。");

                    UpdateMainMessage("アイン：あれも冗談キツイぜ。うっかり気が緩んでるヤツなら瞬殺だろうな。");

                    UpdateMainMessage("アイン：ってか、やっぱりそうなんだな。。。ッハハ、あの人とやるとはな。厄介だぜホント。");

                    if (we.DuelWinZalge)
                    {
                        UpdateMainMessage(KIINA + "：それでは、アイン様のご武運を祈っています。私はこれで");
                    }
                    else
                    {
                        UpdateMainMessage(KIINA + "：それ以外にもカルマンズ・オーンは秘策を持っているとの噂もあります。");

                        UpdateMainMessage(KIINA + "：それでは、連絡は以上です。私はこれで。");
                    }
                    
                    UpdateMainMessage("アイン：ああ。");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = KIINA + "は立ち去っていった・・・";
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：っさてと、DUELの準備と行くか！", true);
                }
                else if ((type == SupportType.FromDuelGate) ||
                         (type == SupportType.FromDungeonGate))
                {
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    UpdateMainMessage("　　【受付嬢：アイン様、お待ちしておりました。】");

                    if (type == SupportType.FromDungeonGate)
                    {
                        UpdateMainMessage("アイン：Duelタイムってわけか。");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：よお、受付さん。約束どおり来たぜ。");
                    }

                    UpdateMainMessage("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】");

                    UpdateMainMessage("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】");

                    UpdateMainMessage("アイン：ああ、準備は万端だ。転送してくれ。");

                    CallSomeMessageWithAnimation("アインはDUEL闘技場へ転送されました");

                    UpdateMainMessage("オーン：来たか、ガキアイン。");

                    UpdateMainMessage("アイン：俺はガキじゃねえ。久しぶりだな、オーン先生。");

                    UpdateMainMessage("オーン：悪いが喋る時間は不要だ、とっとと始めよう。");

                    UpdateMainMessage("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】");

                    UpdateMainMessage("アイン：即開始かよ。ッハハハ、オーン先生らしいぜ。");

                    UpdateMainMessage("オーン：構えろ、アイン。");

                    UpdateMainMessage("アイン：ああ、いつでもいいぜ！");

                    we.TruthDuelMatch14 = true;
                }
            }
            #endregion
            #region "サン・ユウ"
            else if (levelBorder[14] <= mc.Level && !we.TruthDuelMatch15 && !we.TruthCompleteArea3)
            {
                if (type == SupportType.Begin)
                {
                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("アイン：ん・・・６時か。そろそろ起きないとな。");

                    UpdateMainMessage("アイン：おはよう、おばちゃん。");

                    UpdateMainMessage("ハンナ：ああ、おはよう。");

                    UpdateMainMessage("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。");

                    UpdateMainMessage("アイン：ああ、ちょっと顔を出してくる。");

                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    UpdateMainMessage(KIINA + "：アイン様、お待ちしておりました。");

                    UpdateMainMessage("アイン：おう、おはようさん。");

                    UpdateMainMessage(KIINA + "：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。");

                    UpdateMainMessage(KIINA + "：対戦相手は『" + OpponentDuelist + "』様となっております。");

                    UpdateMainMessage("アイン：う～ん、聞いたことのねえ名前だな。");

                    UpdateMainMessage("アイン：戦績とかは聞けるんだったよな。どうだ、強いのか？");

                    UpdateMainMessage(KIINA + "：サン・ユウの戦績は・・・");

                    UpdateMainMessage(KIINA + "：26勝1敗です。");

                    UpdateMainMessage("アイン：ッゲ！！！　マジか！？");

                    UpdateMainMessage("アイン：ハッキリ言ってFiveSeekerレベルの勝率じゃねえか。冗談きついな。");

                    UpdateMainMessage(KIINA + "：必要に応じて、ライフを回復する。");

                    UpdateMainMessage(KIINA + "：BUFF構築を適度に行う。");

                    UpdateMainMessage(KIINA + "：コンスタントにダメージを与える。");

                    UpdateMainMessage(KIINA + "：サン・ユウの戦術は極めて明確です。");

                    UpdateMainMessage("アイン：まいったな・・・そういうのが強いって言うのはある意味相場だよな・・・");

                    if (we.DuelWinZalge)
                    {
                        UpdateMainMessage("アイン：でも26勝1敗って、1敗の相手は誰だったんだよ？");

                        UpdateMainMessage(KIINA + "：オル・ランディスです。");

                        UpdateMainMessage("アイン：・・・最悪だな・・・あのボケ師匠、新参モノにも容赦ねえしな。");

                        UpdateMainMessage("アイン：師匠はその時さ、勝ちセリフで何て言ってた？");

                        UpdateMainMessage(KIINA + "：勝ちセリフ・・・ですか？　何か意味があるんでしょうか？");

                        UpdateMainMessage("アイン：ああ、そっけねえがタメになる一言を伝えて終わるのが師匠のクセだ。");

                        UpdateMainMessage(KIINA + "：ええと、待って下さいね・・・");

                        UpdateMainMessage(KIINA + "：・・・　・・・　・・・　あ！　思い出しました。");

                        UpdateMainMessage(KIINA + "：（声マネ）『ランディス：前提ミエミエなんだよ、ザコが。』");

                        UpdateMainMessage("アイン：前提ミエミエか・・・なるほどな。");

                        UpdateMainMessage(KIINA + "：あの、今のセリフが重要なんでしょうか？");

                        UpdateMainMessage("アイン：ああ、そうだな。かなり重要だ。");

                        UpdateMainMessage("アイン：師匠はガサツで乱暴に見えるが、基本的な戦術論はかなり緻密だ。");

                        UpdateMainMessage("アイン：だから大概普通の戦術論はフツウに見抜いちまう。多分それを伝えたんだと思うぜ。");

                        UpdateMainMessage(KIINA + "：ええと・・・私には分かりませんが・・・");

                        UpdateMainMessage(KIINA + "：アイン様の役にたったのであれば・・・私嬉しく思います！！");

                        UpdateMainMessage("アイン：・・・ん？あ、ああ。");

                        UpdateMainMessage("アイン：サンキュー、サンキュー、キーナさんの解説はいつも助かるぜ！　ッハッハッハ！");

                        UpdateMainMessage(KIINA + "：・・・それでは、私はこれで。ご活躍期待しています。");

                        UpdateMainMessage("アイン：ああ！");
                    }
                    else
                    {
                        UpdateMainMessage(KIINA + "：それでは、連絡は以上です。私はこれで。");

                        UpdateMainMessage("アイン：ああ。");
                    }

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = KIINA + "は立ち去っていった・・・";
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：っさてと、DUELの準備と行くか！", true);
                }
                else if ((type == SupportType.FromDuelGate) ||
                         (type == SupportType.FromDungeonGate))
                {
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    UpdateMainMessage("　　【受付嬢：アイン様、お待ちしておりました。】");

                    if (type == SupportType.FromDungeonGate)
                    {
                        UpdateMainMessage("アイン：Duelタイムってわけか。");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：よお、受付さん。約束どおり来たぜ。");
                    }

                    UpdateMainMessage("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】");

                    UpdateMainMessage("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】");

                    UpdateMainMessage("アイン：ああ、準備は万端だ。転送してくれ。");

                    CallSomeMessageWithAnimation("アインはDUEL闘技場へ転送されました");

                    UpdateMainMessage("サン：来たきた、アインさんだ！");

                    UpdateMainMessage("アイン：っしゃ、サン・ユウだな。お手柔らかに頼むぜ。");

                    UpdateMainMessage("サン：・・・へえぇ・・・");

                    UpdateMainMessage("　　　『サン・ユウの目はスゥっと細くなった』　　");

                    UpdateMainMessage("サン：ホントだ。。。確かにアインさんは、アインさんだね。");

                    UpdateMainMessage("アイン：ん？どういう意味だ？");

                    UpdateMainMessage("　　　『サン・ユウの顔全体が少しホワっとした笑顔になり・・・』　　");

                    UpdateMainMessage("サン：ボクの知る限り");
                    
                    UpdateMainMessage("アインさんはオル・ランディスの愛弟子。");

                    UpdateMainMessage("　　　『サン・ユウの目は突如カっと目を見開いた！』　　");

                    UpdateMainMessage("サン：この天才のボクに泥を付けるなんてなぁ！ランディスめ！！");

                    UpdateMainMessage("サン：しかも説教付きだ！　絶対に許せない！！");

                    UpdateMainMessage("サン：ランディスの弟子、アイン・ウォーレンス！！貴様だけは絶対に潰すぜ！！");

                    UpdateMainMessage("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】");

                    UpdateMainMessage("アイン：・・・悪いが俺には関係ねえ話だ。");

                    UpdateMainMessage("アイン：だが、これだけは言える。");
                    
                    UpdateMainMessage("アイン：サン。そんなんじゃお前は、あのボケ師匠に一生勝てねえな。");

                    UpdateMainMessage("サン：ッグ！！！");

                    UpdateMainMessage("サン：っく、くそおおおぉぉぉ！！！！！！");

                    UpdateMainMessage("サン：ボクが２敗なんてあり得ないんだ！！　貴様だけは絶対に絶対に絶対に絶対に絶対に絶対に潰す！！！！！");

                    UpdateMainMessage("アイン：ああ、かかって来い。受けて立つぜ！");

                    we.TruthDuelMatch15 = true;
                }
            }
            #endregion
            #region "シュヴァルツェ・フローレ"
            else if (levelBorder[15] <= mc.Level && !we.TruthDuelMatch16 && !we.TruthCompleteArea3)
            {
                if (type == SupportType.Begin)
                {
                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("アイン：ん・・・６時か。そろそろ起きないとな。");

                    UpdateMainMessage("アイン：おはよう、おばちゃん。");

                    UpdateMainMessage("ハンナ：ああ、おはよう。");

                    UpdateMainMessage("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。");

                    UpdateMainMessage("アイン：ああ、ちょっと顔を出してくる。");

                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    UpdateMainMessage(KIINA + "：アイン様、お待ちしておりました。");

                    UpdateMainMessage("アイン：おう、おはようさん。");

                    UpdateMainMessage(KIINA + "：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。");

                    UpdateMainMessage(KIINA + "：対戦相手は『" + OpponentDuelist + "』様となっております。");

                    UpdateMainMessage("アイン：・・・フローレ！！？　まさか！！！");

                    UpdateMainMessage(KIINA + "：そうですね。彼は王妃様にとってのたった一人だけの義兄弟。");

                    if (we.DuelWinZalge)
                    {
                        UpdateMainMessage(KIINA + "：家族を全て亡くした王妃ファラ様・・・。");
                        
                        UpdateMainMessage(KIINA + "：そんなファラ様に残されたのは、たった一人の執事役シュヴァルツェ。");

                        UpdateMainMessage(KIINA + "：ファラ様の笑顔を、長年の歳月をかけて戻してくださったのが、");

                        UpdateMainMessage("アイン：そのシュヴァルツェってワケか。すげえよなコイツも・・・");

                        UpdateMainMessage("アイン：ファラ様の笑顔は、この上なく綺麗だからな。見てるコッチが恥ずかしくなるぐらいだ。");

                        UpdateMainMessage(KIINA + "：今でもファラ様を守るために、ほぼ寝ずの状態で鍛錬の日々を過ごされているそうです。");

                        UpdateMainMessage(KIINA + "：戦術は既にご存知でしょうが、お聞きになりますか？");

                        UpdateMainMessage("アイン：ああ、頼むぜ。");

                        UpdateMainMessage(KIINA + "：基本的には守りの戦術。");

                        UpdateMainMessage(KIINA + "：BUFF構築と要所のカウンターでアドバンテージを確立させ、");

                        UpdateMainMessage(KIINA + "：そのアドバンテージを拡げていき、敵に降伏させるのが基本戦術です。");
                        
                        UpdateMainMessage("アイン：何か戦術一つとっても、その人の人柄みたいなのが出るんだな。");

                        UpdateMainMessage("アイン：守りの戦術か・・・実際手強そうだぜ。気を引き締めないと勝てそうにないな。");
                    }

                    UpdateMainMessage(KIINA + "：ファラ王妃に対する忠誠心はおそらく彼が一番抱いている事でしょう。");

                    UpdateMainMessage("アイン：ふう、気合勝負になりそうだな。。。戦術は練りに練っておくとするか。");

                    UpdateMainMessage(KIINA + "：それでは、連絡は以上です。私はこれで。");

                    UpdateMainMessage("アイン：ああ。");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = KIINA + "は立ち去っていった・・・";
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：っさてと、DUELの準備と行くか！", true);
                }
                else if ((type == SupportType.FromDuelGate) ||
                         (type == SupportType.FromDungeonGate))
                {
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    UpdateMainMessage("　　【受付嬢：アイン様、お待ちしておりました。】");

                    if (type == SupportType.FromDungeonGate)
                    {
                        UpdateMainMessage("アイン：Duelタイムってわけか。");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：よお、受付さん。約束どおり来たぜ。");
                    }

                    UpdateMainMessage("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】");

                    UpdateMainMessage("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】");

                    UpdateMainMessage("アイン：ああ、準備は万端だ。転送してくれ。");

                    CallSomeMessageWithAnimation("アインはDUEL闘技場へ転送されました");

                    UpdateMainMessage("アイン：・・・よろしく頼むぜ、シュヴァルツェさんとやら。");

                    UpdateMainMessage("シュヴァルツェ：アイン君だね。ようやく会えた。");

                    UpdateMainMessage("アイン：っな、待ってたのかよ？");

                    UpdateMainMessage("シュヴァルツェ：どうしても一つ、聞いておきたい事があってね。");

                    UpdateMainMessage("シュヴァルツェ：君はどうしてこの闘技場へ参加しようと思ったんだい？");

                    UpdateMainMessage("アイン：ん？DUEL前にそんな話か・・・そうだなあ・・・");

                    UpdateMainMessage("アイン：参加理由は、師匠に勝つためだ。");

                    UpdateMainMessage("アイン：DUELは一戦一戦が勝負そのもの。師匠はそれにほぼ勝ち続けている。");

                    UpdateMainMessage("アイン：そのセンスは俺も同じように持っている。そのセンスを腐らせたくはねえ。");

                    UpdateMainMessage("アイン：まあこんな所だ！　満足のいく内容だったか？");

                    UpdateMainMessage("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】");

                    UpdateMainMessage("シュヴァルツェ：ああ、満足だよ。");
                    
                    UpdateMainMessage("シュヴァルツェ：君のような人がいて本当に良かった。");

                    UpdateMainMessage("アイン：っへ？？");

                    UpdateMainMessage("シュヴァルツェ：なんでも無い。こちらの話だ。");
                    
                    UpdateMainMessage("シュヴァルツェ：さて、はじめるとしよう。DUEL戦では本気で戦ってもらおうか！");

                    UpdateMainMessage("アイン：ああ、当然だ。DUELじゃいつも本気だぜ。");

                    UpdateMainMessage("シュヴァルツェ：では！");

                    UpdateMainMessage("アイン：っしゃ、来い！");

                    we.TruthDuelMatch16 = true;
                }
            }
            #endregion
            #region "ルベル・ゼルキス"
            else if (levelBorder[16] <= mc.Level && !we.TruthDuelMatch17 && !we.TruthCompleteArea4)
            {
                if (type == SupportType.Begin)
                {
                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("アイン：ん・・・６時か。そろそろ起きないとな。");

                    UpdateMainMessage("アイン：おはよう、おばちゃん。");

                    UpdateMainMessage("ハンナ：ああ、おはよう。");

                    UpdateMainMessage("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。");

                    UpdateMainMessage("アイン：ああ、ちょっと顔を出してくる。");

                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    UpdateMainMessage(KIINA + "：アイン様、お待ちしておりました。");

                    UpdateMainMessage("アイン：おう、おはようさん。");

                    UpdateMainMessage(KIINA + "：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。");

                    UpdateMainMessage(KIINA + "：対戦相手は『" + OpponentDuelist + "』様となっております。");

                    UpdateMainMessage("アイン：ゼルキス・・・どっかで聞いた気もするんだが・・・");

                    UpdateMainMessage(KIINA + "：ファージル宮殿にて、ここ最近仕え始めたゼルキス家の長男です。");

                    if (we.DuelWinZalge)
                    {
                        UpdateMainMessage("アイン：ああ、そういやそうだった。すっかり忘れてた。");

                        UpdateMainMessage("アイン：そういや最初の方で、マーギ・ゼルキスってヤツと戦った気がするが・・・");

                        UpdateMainMessage(KIINA + "：マーギ・ゼルキスは次男にあたります。");

                        UpdateMainMessage("アイン：あっ、やっぱりそうなんだ。");

                        UpdateMainMessage("アイン：って事はアイツと戦術は大体一緒なのか？");

                        UpdateMainMessage(KIINA + "：そうですね、ほとんど同じだと言われています。");

                        UpdateMainMessage(KIINA + "：次男マーギは、回復ポーションと直接攻撃、火魔法の基本的なスタイルに対し");

                        UpdateMainMessage(KIINA + "：長男ルベルは、回復ポーションと直接攻撃、それと水魔法を駆使してきます。");

                        UpdateMainMessage("アイン：・・・そうなのか。");

                        UpdateMainMessage(KIINA + "：どうかされましたか？");

                        UpdateMainMessage("アイン：いや、なんて言うのかな。");

                        UpdateMainMessage("アイン：全然違う気がするな、それ。");

                        UpdateMainMessage(KIINA + "：水魔法と言っても、基本的には攻撃魔法を主体とした戦術の様です。");

                        UpdateMainMessage("アイン：ああ。だが、そこの話じゃないんだ。");

                        UpdateMainMessage(KIINA + "：？");

                        UpdateMainMessage("アイン：キーナさん、ひょっとしてその、ルベル・ゼルキスの公の戦術ってのは");

                        UpdateMainMessage("アイン：プレッシャー・パーミッション戦術だろ、違うか？");

                        UpdateMainMessage(KIINA + "：え、えっと・・・");

                        UpdateMainMessage("アイン：あ、ああ、悪い。こっちの話なんだ。気にしないでくれ。");

                        UpdateMainMessage(KIINA + "：すみません・・・せっかく話をして下さったのに・・・");

                        UpdateMainMessage("アイン：い、いいっていいって。　気にするなって。");

                        UpdateMainMessage(KIINA + "：・・・あの！！！");

                        UpdateMainMessage("アイン：な、なんだ？");

                        UpdateMainMessage(KIINA + "：ももも、もしよかったらまた今度そのプレッス・パーミット戦術を教えてください！！！");

                        UpdateMainMessage("アイン：あ、ああ・・・");

                        UpdateMainMessage("アイン：いいぜ、もちろんだ！");

                        UpdateMainMessage(KIINA + "：あ、ありがとうございます！！！");
                    }
                    else
                    {
                        UpdateMainMessage(KIINA + "：長男ルベルは、回復ポーションと直接攻撃、それと水魔法を駆使してきます。");

                        UpdateMainMessage(KIINA + "：水魔法と言っても、基本的には攻撃魔法を主体とした戦術の様です。");

                        UpdateMainMessage("アイン：そうか、分かった。ありがとな。");
                    }

                    UpdateMainMessage(KIINA + "：それでは、連絡は以上です。私はこれで。");

                    UpdateMainMessage("アイン：ああ。");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = KIINA + "は立ち去っていった・・・";
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：っさてと、DUELの準備と行くか！", true);
                }
                else if ((type == SupportType.FromDuelGate) ||
                         (type == SupportType.FromDungeonGate))
                {
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    UpdateMainMessage("　　【受付嬢：アイン様、お待ちしておりました。】");

                    if (type == SupportType.FromDungeonGate)
                    {
                        UpdateMainMessage("アイン：Duelタイムってわけか。");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：よお、受付さん。約束どおり来たぜ。");
                    }

                    UpdateMainMessage("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】");

                    UpdateMainMessage("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】");

                    UpdateMainMessage("アイン：ああ、準備は万端だ。転送してくれ。");

                    CallSomeMessageWithAnimation("アインはDUEL闘技場へ転送されました");

                    UpdateMainMessage("ルベル：アイン・・・ウォーレンスか。");

                    UpdateMainMessage("アイン：・・・");

                    UpdateMainMessage("ルベル：強いな。");
                    
                    UpdateMainMessage("ルベル：直接見てる今、それがハッキリわかる。私の戦術では勝てそうにないな。");

                    UpdateMainMessage("アイン：俺は、強くなんかねえ。");

                    UpdateMainMessage("ルベル：そなたに問いたい、そなたの強さの根源はなんだ。");

                    UpdateMainMessage("アイン：そんなもんは持ってねえ。");

                    UpdateMainMessage("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】");

                    UpdateMainMessage("ルベル：この勝負、万が一にでも私が勝てたら、その時に教えてもらおう。");

                    UpdateMainMessage("アイン：・・・いや、悪いがごめんだ。");

                    UpdateMainMessage("アイン：ここで負けるわけにはいかないんだ、勝たせてもらう。");

                    UpdateMainMessage("ルベル：ック・・・ックハハハ！　面白い！！");

                    UpdateMainMessage("ルベル：勝負だ！　アイン・ウォーレンスよ！");

                    UpdateMainMessage("アイン：こい！");

                    we.TruthDuelMatch17 = true;
                }
            }
            #endregion
            #region "ヴァン・ヘーグステル"
            else if (levelBorder[17] <= mc.Level && !we.TruthDuelMatch18 && !we.TruthCompleteArea4)
            {
                if (type == SupportType.Begin)
                {
                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("アイン：ん・・・６時か。そろそろ起きないとな。");

                    UpdateMainMessage("アイン：おはよう、おばちゃん。");

                    UpdateMainMessage("ハンナ：ああ、おはよう。");

                    UpdateMainMessage("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。");

                    UpdateMainMessage("アイン：ああ、ちょっと顔を出してくる。");

                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    UpdateMainMessage(KIINA + "：アイン様、お待ちしておりました。");

                    UpdateMainMessage("アイン：おう、おはようさん。");

                    UpdateMainMessage(KIINA + "：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。");

                    UpdateMainMessage(KIINA + "：対戦相手は『" + OpponentDuelist + "』様となっております。");

                    UpdateMainMessage("アイン：聞いたことの無い名前だな・・・誰なんだ？");

                    UpdateMainMessage(KIINA + "：戦歴は非常に浅く、戦術もそれほど公にはなってない、無名の新人です。");

                    UpdateMainMessage(KIINA + "：勝敗は１６勝０敗。今の所全勝です。");

                    UpdateMainMessage("アイン：マジかよ・・・一番やりづらいタイプだな。");

                    if (we.DuelWinZalge)
                    {
                        UpdateMainMessage(KIINA + "：でも私・・・彼の訓練する所を見ました！！");

                        UpdateMainMessage("アイン：っちょ、待ってくれ。ひょっとして非公式な情報じゃ・・・");

                        UpdateMainMessage(KIINA + "：良いんです、大丈夫ですから。");

                        UpdateMainMessage("アイン：おいおい、よせって・・・");

                        UpdateMainMessage(KIINA + "：彼の基本戦術は、ベースは物理と魔法の両方からの攻撃です。");

                        UpdateMainMessage(KIINA + "：タイミングに隙があれば、ニュートラル・スラッシュをインスタントで発動。");

                        UpdateMainMessage(KIINA + "：魔法は火がベース、ピアッシング・フレイムをメインとしてました。");

                        UpdateMainMessage(KIINA + "：ダメージ軽減でハーデスト・パリィも練習していましたし、カウンターも揃ってます。");

                        UpdateMainMessage(KIINA + "：攻めて良し、守って良し、特に隙が無い感じの鍛錬をされてるようです。");

                        UpdateMainMessage("アイン：いいのかよ、そんなに言っちゃって・・・");

                        UpdateMainMessage("アイン：・・・あれ？");

                        UpdateMainMessage(KIINA + "：どうかされましたか？");

                        UpdateMainMessage("アイン：ニュートラル・スラッシュとピアッシング・フレイムって言ったよな？");

                        UpdateMainMessage(KIINA + "：ええ、はい。");

                        UpdateMainMessage("アイン：おかしいな、それ。");

                        UpdateMainMessage(KIINA + "：え、えっ、どうしてですか？");

                        UpdateMainMessage("アイン：いや、別に戦術にケチをつけるわけじゃねえんだけどな。");

                        UpdateMainMessage("アイン：俺の考えうる限り、魔法なら魔法、物理なら物理で固めてくるはずなんだ。");

                        UpdateMainMessage(KIINA + "：そ、そうなんですよね！やっぱり私もそう思ってました！！");

                        UpdateMainMessage("アイン：【多彩さ】をウリにする素人的発想で長生きはしないはずなんだが・・・");

                        UpdateMainMessage("アイン：勝率データからして、弱いとも到底思えない。");

                        UpdateMainMessage("アイン：・・・　・・・　・・・");

                        UpdateMainMessage(KIINA + "：あのっ、参考にしてもらえたでしょうか！！？？");

                        UpdateMainMessage("アイン：ん？あ、ああ、かなり参考になったぜ、サンキュー！");

                        UpdateMainMessage(KIINA + "：あっ、喜んでもらえて・・・うれしいです！！！");

                        UpdateMainMessage(KIINA + "：頑張ってください！！！");

                        UpdateMainMessage("アイン：ああ、ありがとな！　（ハハハ、やたらと元気だな。。。）");
                    }
                    else
                    {
                        UpdateMainMessage(KIINA + "：それでは、連絡は以上です。私はこれで。");

                        UpdateMainMessage("アイン：ああ。");
                    }

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = KIINA + "は立ち去っていった・・・";
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：っさてと、DUELの準備と行くか！", true);
                }
                else if ((type == SupportType.FromDuelGate) ||
                         (type == SupportType.FromDungeonGate))
                {
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    UpdateMainMessage("　　【受付嬢：アイン様、お待ちしておりました。】");

                    if (type == SupportType.FromDungeonGate)
                    {
                        UpdateMainMessage("アイン：Duelタイムってわけか。");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：よお、受付さん。約束どおり来たぜ。");
                    }

                    UpdateMainMessage("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】");

                    UpdateMainMessage("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】");

                    UpdateMainMessage("アイン：ああ、準備は万端だ。転送してくれ。");

                    CallSomeMessageWithAnimation("アインはDUEL闘技場へ転送されました");

                    UpdateMainMessage("ヴァン：・・・　・・・");

                    UpdateMainMessage("アイン：・・・　・・・");

                    UpdateMainMessage("ヴァン：戦術は、火と物理。");

                    UpdateMainMessage("ヴァン：ニュートラル・スラッシュとピアッシング・フレイム。");

                    UpdateMainMessage("アイン：なっ！！");

                    UpdateMainMessage("アイン：・・・惑わされないぜ。");

                    UpdateMainMessage("ヴァン：惑わしてなどいない、今から繰り出す技を教えているだけの事。");

                    UpdateMainMessage("アイン：自信があるって事か。");

                    UpdateMainMessage("ヴァン：自信ではない、予め敵に伝えておき、対策の手筋と思考形成を縛らせるのが目的。");

                    UpdateMainMessage("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】");

                    UpdateMainMessage("アイン：・・・この辺は師匠から嫌と言うほど教えられている。");

                    UpdateMainMessage("ヴァン：ほう・・・彼、オル・ランディスはなんと？");

                    UpdateMainMessage("アイン：『行為が思考を上回るレベルで戦え』って言われている。");

                    UpdateMainMessage("ヴァン：なるほど、通用せぬという事だな。");

                    UpdateMainMessage("ヴァン：では、そろそろ行為に入るとしよう。");

                    UpdateMainMessage("アイン：よし、こっちは準備いいぜ。");

                    UpdateMainMessage("ヴァン：では、参る。");
                    we.TruthDuelMatch18 = true;
                }
            }
            #endregion
            #region "オウリュウ・ゲンマ"
            else if (levelBorder[18] <= mc.Level && !we.TruthDuelMatch19 && !we.TruthCompleteArea4)
            {
                if (type == SupportType.Begin)
                {
                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("アイン：ん・・・６時か。そろそろ起きないとな。");

                    UpdateMainMessage("アイン：おはよう、おばちゃん。");

                    UpdateMainMessage("ハンナ：ああ、おはよう。");

                    UpdateMainMessage("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。");

                    UpdateMainMessage("アイン：ああ、ちょっと顔を出してくる。");

                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    UpdateMainMessage(KIINA + "：アイン様、お待ちしておりました。");

                    UpdateMainMessage("アイン：おう、おはようさん。");

                    UpdateMainMessage(KIINA + "：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。");

                    UpdateMainMessage(KIINA + "：対戦相手は『" + OpponentDuelist + "』様となっております。");

                    UpdateMainMessage("アイン：あっ、聞いたことあるぜ！　その名前！");

                    UpdateMainMessage(KIINA + "：闘技場において、最も長く戦歴を残している者です。");

                    UpdateMainMessage(KIINA + "：勝率は５６４２勝３９８７敗。");

                    UpdateMainMessage("アイン：あのオッサン、どれだけの数をこなしてんだよって感じだしな。");

                    UpdateMainMessage(KIINA + "：オウリュウ・ゲンマの戦術は固定枠にとらわれていません。");

                    UpdateMainMessage(KIINA + "：勝敗結果よりも、プロセスの最中で、常に新しい技を編み出そうとする事で有名です。");

                    UpdateMainMessage("アイン：まあ、確かに。何か毎回やらかしてるって感じだもんな。");

                    if (we.DuelWinZalge)
                    {
                        UpdateMainMessage(KIINA + "：でもわたし、昔から見てきていて思うんですけど。");

                        UpdateMainMessage(KIINA + "：彼はそれでもやっぱり、勝ちに対する意識は残ってるように思えるんですよ。");

                        UpdateMainMessage("アイン：そうだろうな。あれは何ていうか一種のメンタル・トレーニングなんだと思う。");

                        UpdateMainMessage("アイン：自分の戦術型が極地にハマりすぎてないか、常にチェックしてるんだと思うぜ。");

                        UpdateMainMessage("アイン：あの点は、師匠にホントよく似てる。");

                        UpdateMainMessage(KIINA + "：あっ、そういえば、彼はよくオル・ランディスと実践訓練をされてるそうです。");

                        UpdateMainMessage("アイン：やっぱり、そうなのか・・・");

                        UpdateMainMessage("アイン：しかし、何ていうのかな・・・");

                        UpdateMainMessage("アイン：ランク上位ってのは、似たような強さを持ってるよな、本当。");

                        UpdateMainMessage(KIINA + "：あっ、それわたしにも何となく分かります！！");

                        UpdateMainMessage(KIINA + "：トップランクの皆さんって優しいですよね！！");

                        UpdateMainMessage(KIINA + "：あと、とっても話が面白いです！！トゲが無いっていうか・・・その・・・");

                        UpdateMainMessage(KIINA + "：あとあと！　考え方もとっても柔らかい感じがします！！");

                        UpdateMainMessage("アイン：そうそう、何か捉えどころが無いんだよな。");

                        UpdateMainMessage("アイン：でもまあ、それに気圧されてちゃダメだからな。頑張って俺も追いつくぜ。");

                        UpdateMainMessage(KIINA + "：アインさんも優しくてとっても強い方ですから、きっと大丈夫です！！");

                        UpdateMainMessage("アイン：ハハハ、そう言ってもらえると気休めとしては有り難いな。");

                        UpdateMainMessage(KIINA + "：い、いいえいえいえ！！　本心に誓って、本当にそう思います！！！");

                        UpdateMainMessage("アイン：まあ、頑張ってくるさ。　じゃあ、少しアップに行くから。");

                        UpdateMainMessage(KIINA + "：あっ・・・ハイ！！頑張ってきてください！！");
                    }
                    else
                    {
                        UpdateMainMessage(KIINA + "：それでは、連絡は以上です。私はこれで。");

                        UpdateMainMessage("アイン：ああ。");
                    }  

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = KIINA + "は立ち去っていった・・・";
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：っさてと、DUELの準備と行くか！", true);
                }
                else if ((type == SupportType.FromDuelGate) ||
                         (type == SupportType.FromDungeonGate))
                {
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    UpdateMainMessage("　　【受付嬢：アイン様、お待ちしておりました。】");

                    if (type == SupportType.FromDungeonGate)
                    {
                        UpdateMainMessage("アイン：Duelタイムってわけか。");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：よお、受付さん。約束どおり来たぜ。");
                    }

                    UpdateMainMessage("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】");

                    UpdateMainMessage("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】");

                    UpdateMainMessage("アイン：ああ、準備は万端だ。転送してくれ。");

                    CallSomeMessageWithAnimation("アインはDUEL闘技場へ転送されました");

                    UpdateMainMessage("ゲンマ：おぬしが、ヴァインか！　ようきた！！");

                    UpdateMainMessage("アイン：ヴァインじゃない、アイン・ウォーレンスだ。");

                    UpdateMainMessage("ゲンマ：あああ、そりゃ失礼！！　ダーーッハッハッハ！！！");

                    UpdateMainMessage("ゲンマ：でだ。　おぬし、得意技は？");

                    UpdateMainMessage("アイン：いや、特にそういうのはねえ。");

                    UpdateMainMessage("ゲンマ：そうかそうか、特になしかと！！　ヴァーーッハッハッハ！！！");

                    UpdateMainMessage("ゲンマ：で、恋人は？");

                    UpdateMainMessage("アイン：・・・いや、関係ねえ話だ・・・");

                    UpdateMainMessage("ゲンマ：・・・");

                    UpdateMainMessage("ゲンマ：ヴゥワーーッハッハッハッハ！！！");

                    UpdateMainMessage("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】");

                    UpdateMainMessage("ゲンマ：あーりゃりゃりゃ、もう始まっちまうのかよ。");

                    UpdateMainMessage("ゲンマ：のお、ヴォーレンスよ。おぬし、もっと人生を、満喫せよ！！");

                    UpdateMainMessage("アイン：・・・ウォーレンスだ。今はDUELに集中するぜ。");

                    UpdateMainMessage("ゲンマ：カアァァ！つれないねえぇ～！！");

                    UpdateMainMessage("ゲンマ：ま、気持ちは分かる。今は、どんっどん、勝つ事！！");

                    UpdateMainMessage("ゲンマ：ええか、だが、よ～く聞け。");

                    UpdateMainMessage("ゲンマ：いずれ、勝っても勝っても、つまらなくなる、そんな時が・・・");

                    UpdateMainMessage("ゲンマ：くる！！！");

                    UpdateMainMessage("ゲンマ：そしたらなぁ、勝つか負けるかより！");

                    UpdateMainMessage("ゲンマ：技を、極める事！");

                    UpdateMainMessage("ゲンマ：技を、楽しむ事！");

                    UpdateMainMessage("ゲンマ：技を、あみ出す事！");

                    UpdateMainMessage("ゲンマ：これを、おぬしも、やってみることだ。");

                    UpdateMainMessage("アイン：・・・ああ、わかった。");

                    UpdateMainMessage("ゲンマ：技を進化させる際、負ける事も、よくよくある事。");

                    UpdateMainMessage("ゲンマ：勝ってばかりじゃあ、技は進化せん。");

                    UpdateMainMessage("ゲンマ：勝ちは停滞。勝ちは思考の収縮。");

                    UpdateMainMessage("ゲンマ：負けそうな時こそ！！");
                    
                    UpdateMainMessage("ゲンマ：自由に、楽しく！　ヴァーッハッハッハッハ！！！");

                    UpdateMainMessage("ゲンマ：あっ、それとだ。技うんぬんよりも前にだ！！");

                    UpdateMainMessage("ゲンマ：人生の広がり！！　男と女！　酒と旅！！　栄光と挫折！！！");

                    UpdateMainMessage("　　【受付嬢：DUEL開始となります。両者構えてください。】");

                    UpdateMainMessage("ゲンマ：あぁ～、嬢ちゃん、そりゃないぜえぇ！！良いとこなのによおぉ・・・");

                    UpdateMainMessage("　　【受付嬢：両者構えてください。】");

                    UpdateMainMessage("ゲンマ：けっ・・・じゃ、しょうがねえな。");

                    UpdateMainMessage("ゲンマ：おぅ、かかってこぉい！　ヴァイン・ヴォーレンス！！");

                    UpdateMainMessage("アイン：おし、行くぜ！！");
                    we.TruthDuelMatch19 = true;
                }
            }
            #endregion
            #region "ラダ・ミストゥルス"
            else if (levelBorder[19] <= mc.Level && !we.TruthDuelMatch20 && !we.TruthCompleteArea4)
            {
                if (type == SupportType.Begin)
                {
                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("アイン：ん・・・６時か。そろそろ起きないとな。");

                    UpdateMainMessage("アイン：おはよう、おばちゃん。");

                    UpdateMainMessage("ハンナ：ああ、おはよう。");

                    UpdateMainMessage("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。");

                    UpdateMainMessage("アイン：ああ、ちょっと顔を出してくる。");

                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    UpdateMainMessage(KIINA + "：アイン様、お待ちしておりました。");

                    UpdateMainMessage("アイン：おう、おはようさん。");

                    UpdateMainMessage(KIINA + "：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。");

                    UpdateMainMessage(KIINA + "：対戦相手は『" + OpponentDuelist + "』様となっております。");

                    UpdateMainMessage("アイン：何か微妙に雑誌で名前を見たような・・・");

                    UpdateMainMessage(KIINA + "：月刊ファージルハンターによく記載される方です。");

                    if (we.DuelWinZalge)
                    {
                        UpdateMainMessage("アイン：あっ、思い出した！");

                        UpdateMainMessage("アイン：大型モンスター討伐を全部一人でやってのけるアイツだろ！？");

                        UpdateMainMessage(KIINA + "：はい、まさにその彼です。");

                        UpdateMainMessage(KIINA + "：周囲の人達とは折り合いが付かず、行動は荒唐無稽で命知らず");

                        UpdateMainMessage(KIINA + "：彼はよく「相場を知らない者」として罵られる日々を送っていたようです・・・");

                        UpdateMainMessage(KIINA + "：でも彼は徐々に討伐範囲を広げ、今ではダブルＳランクのモンスターまで一人でこなします。");

                        UpdateMainMessage("アイン：・・・すげえな・・・");

                        UpdateMainMessage(KIINA + "：もう、彼を罵る人は居ないでしょう。");

                        UpdateMainMessage("アイン：で、今度はDUEL闘技場でも・・・というワケか。");

                        UpdateMainMessage(KIINA + "：はい、そう考えて間違いはないと思います。");

                        UpdateMainMessage("アイン：何でもかんでも全部一人でか・・・まあ、DUELはもともと一人が前提だけどな。");

                        UpdateMainMessage(KIINA + "：あの・・・わたし・・・");

                        UpdateMainMessage("アイン：ん？");

                        UpdateMainMessage(KIINA + "：あの人・・・何となくですが、見ていて怖いです。");

                        UpdateMainMessage(KIINA + "：殺気立ってるというか・・・人自体を嫌ってる感触が伝わってきます。");

                        UpdateMainMessage(KIINA + "：下手に近づくと平気で殺人でもしそうな雰囲気です・・・怖くて・・・");

                        UpdateMainMessage("アイン：・・・まあ");

                        UpdateMainMessage("アイン：周りのヤツが罵り続けたんだ。そうなるのが、当然じゃないかな。");

                        UpdateMainMessage(KIINA + "：え！？");

                        UpdateMainMessage("アイン：悪いのは、そのラダ・ミストゥルスってヤツじゃない。");

                        UpdateMainMessage("アイン：根本的に悪いのは、周囲の奴らだろ。");

                        UpdateMainMessage(KIINA + "：そ、そうなんですか！？");

                        UpdateMainMessage("アイン：罵れば人は普通に傷つく。");

                        UpdateMainMessage("アイン：表に見えるかどうかだけの違いだ。例外なんていやしないさ。");

                        UpdateMainMessage("アイン：そうやって憎悪の根底が生み出されてきたんだ、殺気の一つや二つ持っててもおかしくないさ。");

                        UpdateMainMessage(KIINA + "：そ、そんな・・・じゃあまさか、一人で大型モンスター討伐をしてきたのも・・・");

                        UpdateMainMessage("アイン：ああ、周囲の人達に対する正当な見せしめ・復讐と言った所だろうな。");

                        UpdateMainMessage("アイン：抱え込んでるものが深そうだ。かなり手強いな・・・おそらく。");

                        UpdateMainMessage(KIINA + "：あ・・・あのっ・・・死なないでください！！！");

                        UpdateMainMessage("アイン：ッハハハ、大丈夫だって、闘技場だろ？");

                        UpdateMainMessage(KIINA + "：でも、あるんです！！　ごくまれに！！！");

                        UpdateMainMessage(KIINA + "：おねがいっ・・・おねがいだから・・・うっ・・・");

                        UpdateMainMessage("アイン：あっ、っちょっタンマ！分かった！！　わかったからここで泣くなって、ホラ・・・");

                        UpdateMainMessage("アイン：分かった分かった、どうしてもヤバイと思ったら降伏宣言すらから・・・なっ？");

                        UpdateMainMessage(KIINA + "：うっ・・・うっ・・・お願いします。");

                        UpdateMainMessage("アイン：ああ。");

                        UpdateMainMessage(KIINA + "：約束、絶対ですからね。");

                        UpdateMainMessage("アイン：ああ！任せておけ！");

                        UpdateMainMessage(KIINA + "：良かった・・・それでは、失礼します。私はこれで。");

                        UpdateMainMessage("アイン：またな。");
                    }
                    else
                    {
                        UpdateMainMessage(KIINA + "：それでは、連絡は以上です。私はこれで。");

                        UpdateMainMessage("アイン：ああ。");
                    }

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = KIINA + "は立ち去っていった・・・";
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：っさてと、DUELの準備と行くか！", true);
                }
                else if ((type == SupportType.FromDuelGate) ||
                         (type == SupportType.FromDungeonGate))
                {
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    UpdateMainMessage("　　【受付嬢：アイン様、お待ちしておりました。】");

                    if (type == SupportType.FromDungeonGate)
                    {
                        UpdateMainMessage("アイン：Duelタイムってわけか。");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：よお、受付さん。約束どおり来たぜ。");
                    }

                    UpdateMainMessage("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】");

                    UpdateMainMessage("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】");

                    UpdateMainMessage("アイン：ああ、準備は万端だ。転送してくれ。");

                    CallSomeMessageWithAnimation("アインはDUEL闘技場へ転送されました");

                    UpdateMainMessage("ラダ：・・・来たか、待ちわびた。");

                    UpdateMainMessage("アイン：・・・");

                    UpdateMainMessage("ラダ：噂には聞いてる。だが、一つだけ確認させてもらおう。");

                    UpdateMainMessage("アイン：なんだ？");

                    UpdateMainMessage("ラダ：強くなった今の自分だからこそ分かる。");

                    UpdateMainMessage("ラダ：弱き者は、口を使い、自分のプライドを保持する事にのみ固執する。");

                    UpdateMainMessage("ラダ：弱者は弱者。抜けられない。そこまでは良い。");

                    UpdateMainMessage("アイン：・・・");

                    UpdateMainMessage("ラダ：だが何故！！　そんな彼らが我を嘲笑い、罵り、侮辱し続けたのか！！！");

                    UpdateMainMessage("アイン：（殺気が・・・ビリビリと伝わってくる・・・）");

                    UpdateMainMessage("ラダ：答えてもらおう、アイン・ウォーレンス。");

                    UpdateMainMessage("ラダ：弱き者に価値はない。　死すべき存在だ、そうは思わないか？");

                    UpdateMainMessage("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】");

                    UpdateMainMessage("アイン：・・・悪いが、強いとか弱いとかって話は、俺には興味がねえ。");

                    UpdateMainMessage("アイン：死する者は自然と死ぬ、生き残る者は自然と生き残る。");

                    UpdateMainMessage("アイン：そして、そんな中でお前は生き残った。");

                    UpdateMainMessage("ラダ：！！！");

                    UpdateMainMessage("アイン：お前は十分過ぎるほど強くなった。もうファージル全域の人々がそれを認めてる。");

                    UpdateMainMessage("アイン：弱き者は、もうお前の事を嘲笑せず賞賛を送っている。別に死すべき存在ではないんだ。");

                    UpdateMainMessage("ラダ：・・・　・・・");

                    UpdateMainMessage("アイン：ここからは真剣勝負だ。　言っておくが、俺は負けねえからな。");

                    UpdateMainMessage("ラダ：ッフフフ・・・くだらん戯言だったな。");

                    UpdateMainMessage("ラダ：勝負は我が勝つ！！来い、アイン・ウォーレンス！！");

                    we.TruthDuelMatch20 = true;
                }
            }
            #endregion
            #region "シン・オスキュレーテ"
            else if (levelBorder[20] <= mc.Level && !we.TruthDuelMatch21 && !we.TruthCompleteArea4)
            {
                if (type == SupportType.Begin)
                {
                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("アイン：ん・・・６時か。そろそろ起きないとな。");

                    UpdateMainMessage("アイン：おはよう、おばちゃん。");

                    UpdateMainMessage("ハンナ：ああ、おはよう。");

                    UpdateMainMessage("ハンナ：アイン、DUELの受付嬢ちゃんが来てるわよ。");

                    UpdateMainMessage("アイン：ああ、ちょっと顔を出してくる。");

                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    UpdateMainMessage(KIINA + "：アイン様、お待ちしておりました。");

                    UpdateMainMessage("アイン：おう、おはようさん。");

                    UpdateMainMessage(KIINA + "：アイン様は本日、DUEL対戦シードに登録されましたのでご連絡させていただきます。");

                    UpdateMainMessage(KIINA + "：対戦相手は『" + OpponentDuelist + "』様となっております。");

                    UpdateMainMessage("アイン：・・・何者なんだ？");

                    UpdateMainMessage(KIINA + "：古代賢者オスキュレーテの名を継ぐ者として知られております。");

                    UpdateMainMessage("アイン：古代・・・賢者？　何の話だ？");

                    UpdateMainMessage(KIINA + "：ファージル全土の黎明紀において、ヒトとしての文明を一から創られた最初の人類。");

                    UpdateMainMessage(KIINA + "：そう言い伝えられております。");

                    UpdateMainMessage("アイン：それが・・・古代賢者・・・");

                    if (we.DuelWinZalge)
                    {
                        UpdateMainMessage(KIINA + "：はい、そして本闘技場でのトップランカー内において、唯一の女性となります。");

                        UpdateMainMessage("アイン：マジかよ。まあ、いてもおかしくはないけどな。");

                        UpdateMainMessage("アイン：ってか、古代賢者っていうのがイマイチ分からないが・・・");

                        UpdateMainMessage("アイン：何か新しい秘術とかを持ってたりするのか？");

                        UpdateMainMessage(KIINA + "：いえ、そういった特別な能力を持ち合わせてはいないそうです。");

                        UpdateMainMessage(KIINA + "：ただ、公の戦術として、誰でもよく使うあの有名なスキルを駆使してきます。");

                        UpdateMainMessage("アイン：まさか・・・ストレート・スマッシュじゃないだろうな？");

                        UpdateMainMessage(KIINA + "：すごい！！　当たりです。");

                        UpdateMainMessage("アイン：で、ゲイル・ウィンドだろ。");

                        UpdateMainMessage(KIINA + "：はい！それも当たりです！！");

                        UpdateMainMessage("アイン：ワード・オブ・フォーチュンも絡む・・・っと・・・");

                        UpdateMainMessage(KIINA + "：何でそんなにお見通しなんですか！？出会った事は無いんですよね？？");

                        UpdateMainMessage("アイン：まあ、そうだけどさ。トップランカーって時点で何となくわかるもんさ。");

                        UpdateMainMessage("アイン：似通ってくるというか、そうならざるをえない。");

                        UpdateMainMessage("アイン：でも、似てるだけじゃ生き残れない。ソイツなりのオリジナリティがどこかにあるものさ。");

                        UpdateMainMessage("アイン：そこら辺になると、実際にＤＵＥＬをしてみない限りは分からないけどな。");

                        UpdateMainMessage(KIINA + "：アインさんは、今までよくＤＵＥＬをされてるのですか？");

                        UpdateMainMessage("アイン：いや、特に率先してやったりはしてないな。");

                        UpdateMainMessage("アイン：ボケ師匠がたまに構ってくれる程度だ。不特定多数を相手してるワケじゃねえ。");

                        UpdateMainMessage("アイン：ＤＵＥＬ数は多分少ない方だと自分では正直思う。");

                        UpdateMainMessage(KIINA + "：でも、さっきみたいな話は、歴戦の方々からよくお話を伺います。");

                        UpdateMainMessage(KIINA + "：アインさんって・・・ひょっとして天才なんじゃないですか！！？？");

                        UpdateMainMessage("アイン：いや、それは無いな。");

                        UpdateMainMessage(KIINA + "：そ、そうでしょうか・・・");

                        UpdateMainMessage("アイン：自分は何かに優れているという気はしない。");

                        UpdateMainMessage("アイン：成績もそれほど良くは無かったし、可もなく不可もなくって所だ。");

                        UpdateMainMessage("アイン：あと、肝心の師匠にはほとんど勝てないでいる。今も連敗中さ。");

                        UpdateMainMessage("アイン：俺は、キーナさんが考えてるような天才なんかじゃない、何処にでもいる普通の傭兵さ。");

                        UpdateMainMessage(KIINA + "：そ、そんな事ありません！　アインさんは絶対に凄い人なんです！！");

                        UpdateMainMessage(KIINA + "：わ、わたし、闘技場でアインさんみたいな優しい人に、今まで出会った事ありませんし！！");

                        UpdateMainMessage("アイン：わ、分かった。分かったから、お、落ち着けって・・・");

                        UpdateMainMessage(KIINA + "：お願いです。わたしと・・・約束してください！");

                        UpdateMainMessage("アイン：な、なんだ？");

                        UpdateMainMessage(KIINA + "：アインさん、ゼッタイに闘技場でトップレベルにランクインしてください！！！");

                        UpdateMainMessage(KIINA + "：お願いです！！");

                        UpdateMainMessage("アイン：・・・　・・・");

                        UpdateMainMessage("アイン：確約はできねえが");

                        UpdateMainMessage("アイン：出来る限りやってみるぜ。");

                        UpdateMainMessage(KIINA + "：あ・・・ありがとうございます！！！");

                        UpdateMainMessage("アイン：キーナさんのためだ、頑張ってみるぜ。");

                        UpdateMainMessage("アイン：約束だ。");

                        UpdateMainMessage(KIINA + "：は・・・はい！！！");

                        UpdateMainMessage(KIINA + "：そ、それでは連絡は以上です！　失礼します！");

                        UpdateMainMessage("アイン：ああ、またな！");
                    }
                    else
                    {
                        UpdateMainMessage(KIINA + "：それでは、連絡は以上です。私はこれで。");

                        UpdateMainMessage("アイン：ああ。");
                    }

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = KIINA + "は立ち去っていった・・・";
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：っさてと、DUELの準備と行くか！", true);
                }
                else if ((type == SupportType.FromDuelGate) ||
                         (type == SupportType.FromDungeonGate))
                {
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    UpdateMainMessage("　　【受付嬢：アイン様、お待ちしておりました。】");

                    if (type == SupportType.FromDungeonGate)
                    {
                        UpdateMainMessage("アイン：Duelタイムってわけか。");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：よお、受付さん。約束どおり来たぜ。");
                    }

                    UpdateMainMessage("　　【受付嬢：ただいまより、『アイン・ウォーレンス』 vs 『" + OpponentDuelist + "』のDUELを開催します。】");

                    UpdateMainMessage("    【受付嬢：アイン様、DUEL闘技場中央部へトランスポートさせていただきます。】");

                    UpdateMainMessage("アイン：ああ、準備は万端だ。転送してくれ。");

                    CallSomeMessageWithAnimation("アインはDUEL闘技場へ転送されました");

                    UpdateMainMessage("シン：アイン・ウォーレンス。");

                    UpdateMainMessage("シン：知恵を超越するもの。");

                    UpdateMainMessage("アイン：なに？");

                    UpdateMainMessage("シン：柔軟なスタイルと、異常とも思える潜在性を有する。");

                    UpdateMainMessage("シン：ただし、周囲とのバランス／兼ね合いまでを最適化の対象としてしまう。");

                    UpdateMainMessage("シン：環境さえ揃えば、その能力は計り知れない。だが、今は開花に至らず・・・か。");

                    UpdateMainMessage("シン：なるほど・・・確かに面白い逸材だね。");

                    UpdateMainMessage("アイン：なっ、何の話をしている！？");

                    UpdateMainMessage("シン：君は、このプロフェシー・サーガに描かれている通りの人物だ。実に興味深い。");

                    UpdateMainMessage("シン：君自身は気にしなくてもいい話です。");

                    UpdateMainMessage("　　【受付嬢：それでは、アイン・ウォーレンス様。" + OpponentDuelist + "様。DUEL開始となります。】");

                    UpdateMainMessage("シン：では、試させてもらいましょう。");

                    UpdateMainMessage("シン：君がこのファージル全域を導く覇者に成り得るかどうか。");

                    UpdateMainMessage("アイン：（ックソ、なんだこのオーラは・・・シャレになってねえな・・・）");

                    UpdateMainMessage("アイン：（いや、気圧されちゃ駄目だ。ココは・・・）");

                    UpdateMainMessage("アイン：言ってる意味が分からねえな。");

                    UpdateMainMessage("シン：・・・");

                    UpdateMainMessage("アイン：ファージル国はエルミ国王が治めてくれている。");
                    
                    UpdateMainMessage("アイン：あの人がやってくれてんなら、将来は当分安定だ。俺がどうのこうのと出る幕じゃない。");

                    UpdateMainMessage("アイン：だが、今そんな事は関係ない。");

                    UpdateMainMessage("シン：『そんな事は関係ない』・・・か");
                    
                    UpdateMainMessage("シン：ッフフフ、なかなか良い考え方ですね。");

                    UpdateMainMessage("アイン：（・・・つかめないやつだな・・・）");

                    UpdateMainMessage("アイン：とにかく、今ここで負けるわけにはいかねえ。");

                    UpdateMainMessage("アイン：勝負だ！　行くぜ！");

                    UpdateMainMessage("シン：はい、いつでも。");

                    we.TruthDuelMatch21 = true;
                }
            }
            #endregion
        }
    }
}