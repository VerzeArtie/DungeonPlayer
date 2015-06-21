using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Collections;
//using Microsoft.DirectX.DirectSound;
using System.Threading;
using System.Runtime.InteropServices;

namespace DungeonPlayer
{
    public partial class HomeTown : MotherForm
    {
        private MainCharacter mc = null;
        public MainCharacter MC
        {
            get { return mc; }
            set { mc = value; }
        }
        private MainCharacter sc = null;
        public MainCharacter SC
        {
            get { return sc; }
            set { sc = value; }
        }
        private MainCharacter tc = null;
        public MainCharacter TC
        {
            get { return tc; }
            set { tc = value; }
        }
        private WorldEnvironment we;
        public WorldEnvironment WE
        {
            get { return we; }
            set { we = value; }
        }
        private bool[] knownTileInfo;
        private bool[] knownTileInfo2;
        private bool[] knownTileInfo3;
        private bool[] knownTileInfo4;
        private bool[] knownTileInfo5;
        public bool[] KnownTileInfo
        {
            get { return knownTileInfo; }
            set { knownTileInfo = value; }
        }
        public bool[] KnownTileInfo2
        {
            get { return knownTileInfo2; }
            set { knownTileInfo2 = value; }
        }
        public bool[] KnownTileInfo3
        {
            get { return knownTileInfo3; }
            set { knownTileInfo3 = value; }
        }
        public bool[] KnownTileInfo4
        {
            get { return knownTileInfo4; }
            set { knownTileInfo4 = value; }
        }
        public bool[] KnownTileInfo5
        {
            get { return knownTileInfo5; }
            set { knownTileInfo5 = value; }
        }

        private OKRequest ok;

        private int firstDay = 1;
        //private int day = 1;
        //public int Day
        //{
        //    get { return day; }
        //    set { day = value; }
        //}

        private int targetDungeon = 1;
        public int TargetDungeon
        {
            get { return targetDungeon; }
        }

        private bool noFirstMusic = false;
        public bool NoFirstMusic
        {
            get { return noFirstMusic; }
            set { noFirstMusic = value; }
        }

        private void UpdateMainMessage(string message)
        {
            UpdateMainMessage(message, false);
        }
        private void UpdateMainMessage(string message, bool ignoreOK)
        {
            mainMessage.Text = message;
            if (!ignoreOK)
            {
                ok.ShowDialog();
            }
        }

        System.Threading.Thread th;
        bool endSign;
        private int battleSpeed;
        public int BattleSpeed
        {
            get { return battleSpeed; }
            set { battleSpeed = value; }
        }
        private int difficulty;
        public int Difficulty
        {
            get { return difficulty; }
            set { difficulty = value; }
        }


        public HomeTown()
        {
            InitializeComponent();
            //th = new Thread(new System.Threading.ThreadStart(UpdateXAudio));
            //th.IsBackground = true;
            //th.Start();
        }

        private void HomeTown_FormClosing(object sender, FormClosingEventArgs e)
        {
            endSign = true;
            GroundOne.StopDungeonMusic();
            if (GroundOne.sound != null) // 後編編集
            {
                GroundOne.sound.StopMusic(); // 後編編集
                // this.sound.Disactive(); // 後編削除
            }

            // 「警告」この処理は何に対して緊急回避した修正かわからないままです。
            if (this.firstDay >= 7)
            {
                we.AvailableEquipShop = true;
            }
        }

        private void HomeTown_Load(object sender, EventArgs e)
        {
            this.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "hometown.jpg");
            this.dayLabel.Text = we.GameDay.ToString() + "日目";
            if (we.AlreadyRest)
            {
                this.firstDay = we.GameDay - 1; // 休息したかどうかのフラグに関わらず町に訪れた最初の日を記憶します。
            }
            else
            {
                this.firstDay = we.GameDay; // 休息したかどうかのフラグに関わらず町に訪れた最初の日を記憶します。
            }
            this.we.SaveByDungeon = false; // 町に戻っていることを示すためのものです。

        }

        private void HomeTown_Shown(object sender, EventArgs e)
        {
            if (!noFirstMusic)
            {
                GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
            }

            ok = new OKRequest();
            ok.StartPosition = FormStartPosition.Manual;
            ok.Location = new Point(this.Location.X + 540, this.Location.Y + 440);

            // 死亡しているものは自動的に復活させます。
            if (mc != null)
            {
                if (mc.Dead)
                {
                    mainMessage.Text = "ダンジョンゲートから不思議な光が" + mc.Name + "へと流れ込む。";
                    ok.ShowDialog();
                    mainMessage.Text = mc.Name + "は命を吹き返した。";
                    mc.Dead = false;
                    mc.CurrentLife = mc.MaxLife / 2;
                    ok.ShowDialog();
                }
            }
            if (sc != null)
            {
                if (sc.Dead)
                {
                    mainMessage.Text = "ダンジョンゲートから不思議な光が" + sc.Name + "へと流れ込む。";
                    ok.ShowDialog();
                    mainMessage.Text = sc.Name + "は命を吹き返した。";
                    sc.Dead = false;
                    sc.CurrentLife = sc.MaxLife / 2;
                    ok.ShowDialog();
                }
            }
            if (tc != null)
            {
                if (tc.Dead)
                {
                    mainMessage.Text = "ダンジョンゲートから不思議な光が" + tc.Name + "へと流れ込む。";
                    ok.ShowDialog();
                    mainMessage.Text = tc.Name + "は命を吹き返した。";
                    tc.Dead = false;
                    tc.CurrentLife = tc.MaxLife / 2;
                    ok.ShowDialog();
                }
            }

            // 各ダンジョンクリア毎に特別イベントを発生させます。十分盛り上げてください。
            #region "初めて帰還"
            if (!this.we.CommunicationFirstHomeTown)
            {
                mainMessage.Text = "アイン：ふう、ユングの町に戻ってきたぜ。さて、どうすっかな。";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：っお、バカがちゃんと戻ってきたようね。";
                ok.ShowDialog();
                mainMessage.Text = "アイン：ラナ、人の名前をちゃんと呼べ。アイン『様』と呼べ。";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：自分で『様』付けてる時点でバカよ。カギカッコまで付けちゃって・・・自覚しなさい。";
                ok.ShowDialog();
                mainMessage.Text = "アイン：心配するな。自覚はしてるつもりだ。";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：どんだけバカよ・・・あ、そうだわ。コレ渡しとくね。";
                ok.ShowDialog();
                mainMessage.Text = "アイン：ん？なんだそれは。丸っこいな。水晶か？";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：そうよ。アイン、入り口に戻るまでにモンスターに出くわすのは辛いと思わない？";
                ok.ShowDialog();
                mainMessage.Text = "アイン：まあな、戻ろうって時に出てくると非常にウザい事この上ない。っお！まさか！？";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：【遠見の青水晶】  ダンジョンから一気にこのユングの町に戻ることができるの。";
                ok.ShowDialog();
                mainMessage.Text = "アイン：すげえじゃねえか。これなら後戻り気にせずガンガン進めるって事だな。";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：アインにあげるわ。大事に使ってよね。"; 
                ok.ShowDialog();
                mainMessage.Text = "アイン：マジかよ！いやあ助かるぜ。サンキュー。";
                ok.ShowDialog();
                mc.AddBackPack(new ItemBackPack("遠見の青水晶"));
                using (MessageDisplay md = new MessageDisplay())
                {
                    md.Message = "【遠見の青水晶】を手に入れました。";
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.ShowDialog();
                }
                mainMessage.Text = "ラナ：良いわよこれぐらい。さ、ハンナおばさん、ガンツおじさんに会ってきたら？";
                ok.ShowDialog();
                mainMessage.Text = "アイン：おお、そうだな。さーて、じゃ挨拶いってくるか！";
                ok.ShowDialog();
                we.CommunicationFirstHomeTown = true;
            }
            #endregion
            #region "１階制覇"
            else if (this.we.CompleteArea1 && !this.we.CommunicationCompArea1)
            {
                mainMessage.Text = "アイン：おーい、ラナ！ラナ！";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：なによ、ウッサイわね。";
                ok.ShowDialog();
                mainMessage.Text = "アイン：ついに１階制覇したぜ。さすが俺だな！";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：っげ、アインまさかひょっとして。。。";
                ok.ShowDialog();
                mainMessage.Text = "アイン：ん？何だよ・・・ん、おわ！";
                ok.ShowDialog();
                mainMessage.Text = "アイン：イデデデデ！！！イッテェェェエエエェェェ！！";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：ふーん・・・夢オチってわけじゃなさそうね。";
                ok.ShowDialog();
                mainMessage.Text = "アイン：何言ってんだ、当たり前だろ。あ～イッテェなあ、もう。";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：やるじゃない。ちょっとだけ驚いちゃった。";
                ok.ShowDialog();
                mainMessage.Text = "アイン：おお、もっと存分に驚いてくれ！ッハッハッハ！！";
                ok.ShowDialog();
                if (this.we.GameDay >= 7)
                {
                    mainMessage.Text = "ラナ：経過日数は" + this.we.GameDay.ToString() + "日ね。まっ、上出来なんじゃない？";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：何だよその“経過日数”ってのは。";
                    ok.ShowDialog();
                    mainMessage.Text = "ラナ：ダンジョン１階制覇まで費やした時間に決まってるじゃない。";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：何か引っかかる言い方だな。もっと素直に俺を尊べ！";
                    ok.ShowDialog();
                    mainMessage.Text = "ラナ：ホンット、バカよね。尊ぶって意味は分かってるわけ？";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：敬う。";
                    ok.ShowDialog();
                    mainMessage.Text = "ラナ：敬うってどういう意味？";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：尊敬する。";
                    ok.ShowDialog();
                    mainMessage.Text = "ラナ：尊敬するってどういう意味？";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：尊ぶ。";
                    ok.ShowDialog();
                    mainMessage.Text = "ラナ：ハアァ～～～ァァ～～・・・ま、いいわ。とりあえずおめでとう。";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：サンキュー！！お前からおめでとうが出るなんて嬉しいね。";
                    ok.ShowDialog();
                    mainMessage.Text = "ラナ：何言ってんのよ。気休めよ、き、や、す、め。";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：いや、気休めでも嬉しいぜ。サンキュー。";
                    ok.ShowDialog();
                    mainMessage.Text = "ラナ：もう、調子狂うわね。そうそう、ガンツさん、ハンナ叔母さんにも報告したら？";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：ああ、そのつもりだぜ。あ、そうだ。今日は一緒に飯でも食うか。";
                    ok.ShowDialog();
                    mainMessage.Text = "ラナ：そうね。お祝いはお祝いだし、一緒しちゃおうかな。";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：おっしゃ！明日から２階目指すぜ。まずはちょっとガンツさんとこ行ってくるぜ。";
                    ok.ShowDialog();
                    mainMessage.Text = "ラナ：フフフ、とっとと行ってこい。";
                    ok.ShowDialog();
                    we.CommunicationCompArea1 = true;
                }
                else
                {
                    mainMessage.Text = "ラナ：経過日数は・・・" + this.we.GameDay.ToString() + "日？ウソ、冗談でしょ。";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：冗談だ。";
                    ok.ShowDialog();
                    mainMessage.Text = "ラナ：何バカ言ってんのよ。ボケ自爆も大概に・・・あ、ねえねえ。";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：何だよ？";
                    ok.ShowDialog();
                    mainMessage.Text = "ラナ：覚えてる？";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：１階制覇のことか？そりゃ今やった事ぐらい覚えてるさ。";
                    ok.ShowDialog();
                    mainMessage.Text = "ラナ：そうじゃなくて、その前よ。";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：ラナ、何言ってんだお前？";
                    ok.ShowDialog();
                    // １度ゲームクリアしてるかどうかで分岐してください。
                    mainMessage.Text = "ラナ：お願い、真面目に答えて。　　　　【覚えてる？】";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：・・・";
                    ok.ShowDialog();
                    if (we.TrueEnding1)
                    {
                        using (YesNoRequest yesno = new YesNoRequest())
                        {
                            yesno.StartPosition = FormStartPosition.CenterParent;
                            yesno.ShowDialog();
                            if (yesno.DialogResult == DialogResult.Yes)
                            {
                                mainMessage.Text = "アイン：・・・ああ、不思議とな。【ダンジョンに入った事、覚えているぞ】";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：そう、じゃあ次も真面目に答えて。　　　　【答えは分かった？】";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：ラナ・・・";
                                ok.ShowDialog();
                                // 真実の言葉：１を見ていたかどうかで分岐してください。
                                yesno.StartPosition = FormStartPosition.CenterParent;
                                yesno.ShowDialog();
                                if (yesno.DialogResult == DialogResult.Yes)
                                {
                                    mainMessage.Text = "アイン：・・・【俺達はまだダンジョンの中にいる】";
                                    ok.ShowDialog();
                                    mainMessage.Text = "ラナ：そうね。正解よ。じゃあこれはどう？　　　　【出る方法は？】";
                                    ok.ShowDialog();
                                    mainMessage.Text = "アイン：もう止めてくれないか。俺はこのままお前と";
                                    ok.ShowDialog();
                                    mainMessage.Text = "ラナ：駄目よ。アインお願い、答えて。";
                                    ok.ShowDialog();
                                    mainMessage.Text = "アイン：・・・";
                                    ok.ShowDialog();
                                    // 真実の言葉：２を見ていたかどうかで分岐してください。
                                    yesno.StartPosition = FormStartPosition.CenterParent;
                                    yesno.ShowDialog();
                                    if (yesno.DialogResult == DialogResult.Yes)
                                    {
                                        mainMessage.Text = "アイン：【願いがかなう場所へ、願いが終わる場所へ】";
                                        ok.ShowDialog();
                                        mainMessage.Text = "ラナ：そうね。いよいよ本題ってとこかしら。　　　　【願いとは？】";
                                        ok.ShowDialog();
                                        mainMessage.Text = "アイン：いい加減止めろっつってんだろうが！！";
                                        ok.ShowDialog();
                                        mainMessage.Text = "ラナ：アイン、ごめんなさいね。でも、もう知ってる筈よ。";
                                        ok.ShowDialog();
                                        mainMessage.Text = "アイン：・・・";
                                        ok.ShowDialog();
                                        // 真実の言葉：３を見ていたかどうかで分岐してください。
                                        yesno.StartPosition = FormStartPosition.CenterParent;
                                        yesno.ShowDialog();
                                        if (yesno.DialogResult == DialogResult.Yes)
                                        {
                                            mainMessage.Text = "アイン：【このままダンジョン２階へ行ける事】";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：もう嘘は良いの。ありがとうね。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：お前がどうしてそんな事言うんだ。良いからもう止めろ！！";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：ホントの事言って、ね。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：・・・【ラナが救われる事】";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：ピンポーン♪　ようやくバカも卒業ってとこかしら♪";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：っさ、ワケわかんねえ話は置いといて。もう２階へ行くぞ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：最後の質問よ。　　　　【真実とは？】";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：っせぇぇぇなあああ！！！どうでも良いんだよそんな事は！！！";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：フフ、アインってさ、優しすぎだよね。ねえ、答えてよ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：・・・";
                                            ok.ShowDialog();
                                            // 真実の言葉：４を見ていたかどうかで分岐してください。
                                            yesno.StartPosition = FormStartPosition.CenterParent;
                                            yesno.ShowDialog();
                                            if (yesno.DialogResult == DialogResult.Yes)
                                            {
                                                mainMessage.Text = "アイン：・・・・・・";
                                                ok.ShowDialog();
                                                mainMessage.Text = "ラナ：もう良いのよ。ホントごめんなさいね。ありがとうね。";
                                                ok.ShowDialog();
                                                mainMessage.Text = "アイン：・・・・っ・・ぁ・・・ぇ";
                                                ok.ShowDialog();
                                                mainMessage.Text = "ラナ：ん？ホラホラ、聞こえないぞー♪";
                                                ok.ShowDialog();
                                                mainMessage.Text = "アイン：・・・ずっと好きだぞ、ラナ。";
                                                ok.ShowDialog();
                                                mainMessage.Text = "ラナ：・・・うん。";
                                                ok.ShowDialog();
                                                mainMessage.Text = "アイン：俺は死ぬまでずっとお前が好きだ、ラナ。";
                                                ok.ShowDialog();
                                                mainMessage.Text = "ラナ：・・・・・・うん。";
                                                ok.ShowDialog();
                                                mainMessage.Text = "アイン：言うぞ。";
                                                ok.ShowDialog();
                                                mainMessage.Text = "ラナ：・・・・・・・・・うん。";
                                                ok.ShowDialog();
                                                mainMessage.Text = "アイン：　　　　【ラナは死んだ】　　　　";
                                                ok.ShowDialog();
                                                mainMessage.Text = "ラナ：アインの事、大好きだよ。ずっと愛してる。忘れないでね。";
                                                ok.ShowDialog();
                                                mainMessage.Text = "アイン：ボケが！テメェなんぞ、くたばって清々するさ！！！";
                                                ok.ShowDialog();
                                                mainMessage.Text = "ラナ：あっ、そーいう事言うのね。分かった分かった、くたばってやるわよ。";
                                                ok.ShowDialog();
                                                mainMessage.Text = "アイン：くたばるタマかよ。殺しても死なねぇヤツじゃねえか。ッハッハッハ！";
                                                ok.ShowDialog();
                                                mainMessage.Text = "ラナ：アイン、もう時間がすぐソコまで来てるの。";
                                                ok.ShowDialog();
                                                mainMessage.Text = "アイン：お、おい、明日の毒盛赤ポーション、あるんだろ？";
                                                ok.ShowDialog();
                                                mainMessage.Text = "ラナ：いっつもそうやってバカ台詞言ってるから笑っちゃうじゃない。";
                                                ok.ShowDialog();
                                                mainMessage.Text = "アイン：じゃあ素直に笑っえっての。なあ。何で泣いてるんだよラナ。";
                                                ok.ShowDialog();
                                                mainMessage.Text = "ラナ：永遠に愛してるよ。生まれ変わったらまた会おう。";
                                                ok.ShowDialog();
                                                mainMessage.Text = "アイン：バカ。そんな泣いてるお前なんかに会いたくなんかねぇっての。";
                                                ok.ShowDialog();
                                                mainMessage.Text = "ラナ：フフ・・・ウフフ。最後までそんなバカしてくれて。ねえアイン。";
                                                ok.ShowDialog();
                                                mainMessage.Text = "アイン：ラナ・・・ラナ、おい。";
                                                ok.ShowDialog();
                                                mainMessage.Text = "ラナ：ねえ、アイン・・・アイン。";
                                                ok.ShowDialog();
                                                mainMessage.Text = "アイン：ラナ・・・うっ、頭が・・・";
                                                ok.ShowDialog();
                                                mainMessage.Text = "アイン：ラナ、ラナ！グアァァァァァ！！！！！！";
                                                ok.ShowDialog();
                                                we.CommunicationCompArea1 = true;
                                                // ＴＲＵＥエンドを分岐用に用意してください。
                                                // 真実の言葉：５
                                                // 真実の欠片：ラナのイヤリング
                                                // 真実の鏡：偽りの万華鏡
                                                // 真実の扉を開いていたかどうか
                                                // 真実の声を聞いていたかどうか
                                                return;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    mainMessage.Text = "アイン：・・・いや、何言ってるんだラナ。そもそも質問の意味が全然わからねぇぞ。";
                    ok.ShowDialog();
                    mainMessage.Text = "ラナ：そっ、そう。フフフ、ごめんね。変な事聞いちゃって。";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：ところで何だよ、その“経過日数”ってのは。";
                    ok.ShowDialog();
                    mainMessage.Text = "ラナ：ダンジョン１階制覇まで費やした時間に決まってるじゃない。";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：何か引っかかる言い方だな。もっと素直に俺を尊べ！";
                    ok.ShowDialog();
                    mainMessage.Text = "ラナ：ホンット、バカよね。尊ぶって意味は分かってるわけ？";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：敬う。";
                    ok.ShowDialog();
                    mainMessage.Text = "ラナ：敬うってどういう意味？";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：尊敬する。";
                    ok.ShowDialog();
                    mainMessage.Text = "ラナ：尊敬するってどういう意味？";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：尊ぶ。";
                    ok.ShowDialog();
                    mainMessage.Text = "ラナ：ハアァ～～～ァァ～～・・・ま、いいわ。とりあえずおめでとう。";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：サンキュー！！お前からおめでとうが出るなんて嬉しいね。";
                    ok.ShowDialog();
                    mainMessage.Text = "ラナ：何言ってんのよ。気休めよ、き、や、す、め。";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：いや、気休めでも嬉しいぜ。サンキュー。";
                    ok.ShowDialog();
                    mainMessage.Text = "ラナ：もう、調子狂うわね。そうそう、ガンツさん、ハンナ叔母さんにも報告したら？";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：ああ、そのつもりだぜ。あ、そうだ。今日は一緒に飯でも食うか。";
                    ok.ShowDialog();
                    mainMessage.Text = "ラナ：そうね。お祝いはお祝いだし、一緒しちゃおうかな。";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：おっしゃ！明日から２階目指すぜ。じゃあ、ちょっと皆のとこに回ってくるぜ。";
                    ok.ShowDialog();
                    mainMessage.Text = "ラナ：フフフ、とっとと行ってこい。";
                    ok.ShowDialog();
                }

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "アインは一通り、町の住人達に声をかけ、時間が刻々と過ぎていった。";
                    md.ShowDialog();

                    md.Message = "その日の夜、ハンナの宿屋亭にて";
                    md.ShowDialog();
                }

                mainMessage.Text = "アイン：さてと、いよいよ明日から２階か。バックパックの確認でもしておくか・・・";
                ok.ShowDialog();
                mainMessage.Text = "　　　『ドン！ドン！ドン！　・・・　バコン！！！』　　";
                ok.ShowDialog();
                mainMessage.Text = "　　　【アイン？居ないの？居るんなら返事しなさいよ。】　　";
                ok.ShowDialog();
                mainMessage.Text = "　　　『ッゴスン！！　ドガン！！！　ボグッシャアアァァ！！！』　　";
                ok.ShowDialog();
                mainMessage.Text = "アイン：待て！居るって！開けるから待て、壊すな！";
                ok.ShowDialog();
                mainMessage.Text = "　　　『・・・ッガチャ』";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：あっ、なな、何だ、いるじゃない。返事ぐらいしなさいよ。";
                ok.ShowDialog();
                mainMessage.Text = "アイン：お前、俺が声返す前に、１回蹴飛ばしてなかったか？";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：返事が無かったし居ないと思ったんで♪";
                ok.ShowDialog();
                mainMessage.Text = "アイン：ったく、どこに返事する間があったってんだよ。ええと、何の用だ？";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：ダンジョン２階に行くにあたって、心構えを教えておこうと思ってね。";
                ok.ShowDialog();
                mainMessage.Text = "アイン：何だ、１階と２階じゃそんなにも違うのか？";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：そうよ、まず第一にチームワークを大切にする事。";
                ok.ShowDialog();
                mainMessage.Text = "アイン：何言ってんだ。【１人でもチーム】という表現ほど寂しいモノはねえだろ。";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：第二、これが結構肝心なんだけど、相手の状態やステータスにも気を配る事。";
                ok.ShowDialog();
                mainMessage.Text = "アイン：まあモンスターの状態を把握する方が、戦闘を進める上では有利ではあるな。";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：そして第三、装備品や消耗品など、アイテムの管理はより一層重要になってくるわ。";
                ok.ShowDialog();
                mainMessage.Text = "アイン：まあな、今ちょうどバックパックの整理をしてた所だ。";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：じゃあ第四はね。連携モードを上手く活用するために必須なのが戦闘順番になるんだけど";
                ok.ShowDialog();
                mainMessage.Text = "アイン：待て、待て待て。話が見えないぞ、一体いくつあるんだよそれ？";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：百よ。";
                ok.ShowDialog();
                mainMessage.Text = "アイン：マジかよ。どんだけ心構えるんだよ。しかも連携モードとかも興味ねえし。";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：何言ってるのよ、連携していかないと勝てないわよ。ちゃんと連携しなさいよね。";
                ok.ShowDialog();
                mainMessage.Text = "アイン：誰とやるんだよ？";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：私に決まってるじゃない。あんた、バカじゃないの？";
                ok.ShowDialog();
                mainMessage.Text = "アイン：ッハアァァ！？お前と連携？？";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：まあアインは連携とか下手そうだしね。ちゃんとサポートしてあげるから。";
                ok.ShowDialog();
                mainMessage.Text = "アイン：ラナ、ひょっとしてお前ダンジョンに来るのか？";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：連携って言うのはね、戦闘順番に応じて、連続ヒットさせた場合の総合計値を上げる・・・";
                ok.ShowDialog();
                mainMessage.Text = "アイン：ダンジョンに来るのか？って聞いてんだよ、ちゃんと答えてくれ。";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：・・・そ、そうよ。駄目？";
                ok.ShowDialog();
                mainMessage.Text = "アイン：・・・・・・";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：どうなのよ？";
                ok.ShowDialog();
                mainMessage.Text = "アイン：いいや、大助かりだぜ、サンキュー。ただ・・・な。";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：ただ、何？何か文句でもあるわけ？";
                ok.ShowDialog();
                mainMessage.Text = "アイン：いや、そんなんじゃねえんだ。ただ、何となく引っかかると言うか。";
                ok.ShowDialog();
                mainMessage.Text = "アイン：大事なことを忘れてるような感じがあったんで、ちょっとな。";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：ラナ様の百の心構えを忘れてるだけでしょ。しっかり全部覚えてよね。";
                ok.ShowDialog();
                mainMessage.Text = "アイン：いや、それは今日始めて聞くぞ。お前が突然機関銃の如く・・・";
                ok.ShowDialog();
                mainMessage.Text = "ラナ：ハイハイ、じゃあ成立♪　明日からはビシビシ鍛えるから覚悟！じゃあ後ヨロシクね♪";
                ok.ShowDialog();
                mainMessage.Text = "　　　『ッバタン・・・』";
                ok.ShowDialog();
                mainMessage.Text = "アイン：何がどう“後ヨロシク”なんだよ。無茶苦茶強引だな、ラナのやつ。";
                ok.ShowDialog();
                mainMessage.Text = "アイン：しかし、何だったんだろうな。さっきのもやもやした感触は・・・";
                ok.ShowDialog();
                mainMessage.Text = "アイン：まあ、気にしてもしょうがねえか！";
                ok.ShowDialog();
                mainMessage.Text = "アイン：っしゃ、ラナの分もバックパック整備しておくとするか。";
                ok.ShowDialog();
                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "ラナがパーティに加わりました。";
                    md.ShowDialog();
                }

                CallRestInn();
                we.AvailableSecondCharacter = true;
                we.CommunicationCompArea1 = true;
            }
            #endregion
            #region "２階区画１イベント"
            else if (this.we.InfoArea21 && !this.we.SolveArea21)
            {
                UpdateMainMessage("アイン：なあ、ラナ。あの２階、すでに行き止まりだぞ。");

                UpdateMainMessage("ラナ：おかしいわね。こんな早く最下層とは思えないわ。");

                UpdateMainMessage("アイン：そう言ってもな。どうしようもねえぞ。");

                UpdateMainMessage("ラナ：ちょっと待って、メモしてあるから、もう一度読んでみるわ。");

                UpdateMainMessage("　　　　『この先、行き止まり。　一旦、立ち去るがよい。』");

                UpdateMainMessage("アイン：メモ！？・・・さすがラナ！ッハッハッハ！！");

                UpdateMainMessage("ラナ：あんた、バカじゃないの？それぐらい普通やるでしょ。");

                UpdateMainMessage("アイン：しかし特に何の変哲もない文章だな。");

                UpdateMainMessage("ラナ：待って。ココ見てよ。　【一旦】って書いてあるわ。");

                UpdateMainMessage("アイン：んん？・・・確かに書いてあるな。それがどうした。");

                UpdateMainMessage("ラナ：一旦・・・一旦、立ち去る・・・そうよ、一旦立ち去れば良いのよ。");

                UpdateMainMessage("アイン：なあ、最下層にたどり着いたことだし、夕飯でも食べねえか？");

                UpdateMainMessage("　　　『ッシャゴオォォオォォ！！！』（ラナのライトニングキックがアインに炸裂）　　");

                UpdateMainMessage("アイン：っが！！！ぐ、ぐおおぉぉぉ・・・お前そのツッコミ、リアル過ぎるぞ。");

                UpdateMainMessage("ラナ：私達、一旦ユングの町に戻ってきたのよね。");

                UpdateMainMessage("アイン：ああ、確かに今戻ってきてるな。");

                UpdateMainMessage("ラナ：ダンジョンから【一旦、立ち去った】って事になるわ。");

                UpdateMainMessage("アイン：なるほど、そういう事か。");

                UpdateMainMessage("ラナ：決まりね。明日また行ってみましょう。きっと何か変わってるわ♪");

                UpdateMainMessage("アイン：なるほど、そういう事か。");

                UpdateMainMessage("ラナ：アイン・・・あんたひょっとして・・・");

                UpdateMainMessage("アイン：なるほど、そういう事か。");

                UpdateMainMessage("ラナ：ハアアァァァ～～～～ァァ・・・まあ良いわ。とにかく、明日ね。");
                this.we.SolveArea21 = true;
            }
            #endregion
            #region "２階区画６イベント"
            else if (this.we.InfoArea26 && !this.we.SolveArea26)
            {
                UpdateMainMessage("アイン：いやー疲れた疲れた！！っさ、どっか飯でも行くか！？");

                UpdateMainMessage("ラナ：・・・");

                UpdateMainMessage("アイン：おいおい、何暗い顔してんだよ？");

                UpdateMainMessage("ラナ：だって・・・あんなの解けっこないでしょ！？");

                UpdateMainMessage("アイン：おぁ、いきなりそんなツンツンするなよ。");

                UpdateMainMessage("ラナ：アインは解けたって言うの！？答えてよ！");

                UpdateMainMessage("アイン：いや、そりゃまあ無理だけどさ。");

                UpdateMainMessage("ラナ：だったら何でそんなテキトーなのよ。休んでたって解けやしないのよ？");

                UpdateMainMessage("アイン：いや、そりゃまあそうかも知れないが。でもな・・・");

                UpdateMainMessage("ラナ：あんたがそんなテキトーだから、最後になって２階が解けないんじゃない。");

                UpdateMainMessage("アイン：いや、わりいなホント。何かとまた思い付くようにするからさ・・・");

                UpdateMainMessage("ラナ：ホンット！どうすんのよ！！　ノーヒントで誰が解けるって言うのよ！！！");

                UpdateMainMessage("アイン：おい、おいラナ。落ち着けって、まいったなどうすりゃ・・・");
                
                UpdateMainMessage("　　　【おや、誰かと思えば、アインとラナちゃんじゃないか。こんな所で、どうしたんだい？】");

                UpdateMainMessage("アイン：あっ、おーハンナ叔母さんじゃないですか！叔母さん、どうしたんですか？");

                UpdateMainMessage("ハンナ：ガンツの旦那に飯と酒を持っていく所だよ。そっちはダンジョン順調に進んでるかい？");

                UpdateMainMessage("アイン：いやあ・・・それが見事にピンチでさ・・・");

                UpdateMainMessage("ハンナ：ラナちゃんも一緒みたいだね。ラナちゃん、どうだい、アタシと一緒に夕飯でも？");

                UpdateMainMessage("ラナ：・・・え？あ・・・");

                UpdateMainMessage("ハンナ：ありゃりゃ、こりゃ重症だね。ちょっとだけ待ってな。旦那に渡してきたらすぐ戻るよ。");

                UpdateMainMessage("　　　【ハンナは、ガンツの武具屋へと去っていった・・・】");

                UpdateMainMessage("アイン：おい、ラナ。ハンナ叔母さんとこで飯食わねえか？");

                UpdateMainMessage("ラナ：・・・そうね。");

                UpdateMainMessage("アイン：おっしゃ！決まり決まりっと！！");

                UpdateMainMessage("　　　【ハンナが、アインとラナの所へ戻ってきた・・・】");

                UpdateMainMessage("ハンナ：ハイハイ、じゃあウチへ来て沢山食べていきな。今日はアタシも一緒に食べるよ。");

                UpdateMainMessage("アイン：本当にありがとうございます！よし、じゃあ行こうぜ、ラナ！");

                UpdateMainMessage("ラナ：え、ええ・・・");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "アインとラナはハンナ叔母さんと共にハンナの宿屋へ向かった。";
                    md.ShowDialog();
                }

                UpdateMainMessage("アイン：ムグ・・・上手い、上手いな・・・");

                UpdateMainMessage("ラナ：アイン、ちょっともう少し品のある食べ方してよね。");

                UpdateMainMessage("アイン：ムグ・・・グ・・・ん？何ぐぁ？");

                UpdateMainMessage("ハンナ：アッハッハ、良いよ良いよ。ウチじゃ食べ方のルールなんて無いからね。");

                UpdateMainMessage("ハンナ：ところで、さっきダンジョンでピンチとか言ってたね。行き詰ったのかい？");

                UpdateMainMessage("アイン：あ、あぁ、それがな。");

                UpdateMainMessage("ラナ：アイン、私が説明するわ。");

                UpdateMainMessage("ハンナ：まあまあラナちゃん、アインに説明させてやったらどうだい？");

                UpdateMainMessage("ラナ：え？ええ、叔母さんがそう言うのなら、アインに任せようかしら。");

                UpdateMainMessage("アイン：あのダンジョン、２階が妙に区画分けされててな。");

                UpdateMainMessage("アイン：それで１区画毎の入り口に看板が立ってるんだよ。それを読むと・・・");

                UpdateMainMessage("アイン：『この先、行き止まり。　一旦、立ち去るがよい。』");

                UpdateMainMessage("アイン：『この先、行き止まり。　各々の強さを示せ。』");

                UpdateMainMessage("アイン：『この先、行き止まり。　汝自身に問いかけよ。』");

                UpdateMainMessage("アイン：『この先、行き止まり。　正しき順序、正しき道筋を示せ。』");

                UpdateMainMessage("アイン：『この先、行き止まり。　最後の区画へ進むべし。』");

                UpdateMainMessage("アイン：ここまでは何とか順調だったわけさ。それらしいヒントが後ろにくっついてるからな。");

                UpdateMainMessage("アイン：ところが、最後の区画がこういう看板だったんだ。");

                UpdateMainMessage("アイン：『この先、行き止まり。』");

                UpdateMainMessage("アイン：見事に何も書いてねえ。それで周りを探索したんだが・・・");

                UpdateMainMessage("アイン：何一つヒントが無い状態だってわけさ。・・・ここで降参ってワケだ。参ったぜホント。");

                UpdateMainMessage("ハンナ：最後はヒント無しってかい？そりゃ確かに困った事だね、アッハハハハ。");

                UpdateMainMessage("ラナ：叔母さんはどう？何か分かりますか？");

                UpdateMainMessage("ハンナ：いいや、ヒントが無いものは分からないね。");

                UpdateMainMessage("アイン：ほら！叔母さんだって分からないんだぜ。俺に分からなくて当たり前だろ！？ッハッハッハ！");

                UpdateMainMessage("ラナ：変なトコで自慢しないでくれる！？あんたってホンットバカよね！？");

                UpdateMainMessage("アイン：い、いやいや、悪かったって言ってるだろ・・・");

                UpdateMainMessage("ハンナ：アインはそうやってたまにラナちゃんに気遣ってるんだよ。許してやんな。");

                UpdateMainMessage("ラナ：叔母さん！？アイツは正真正銘ですよ！？");

                UpdateMainMessage("ハンナ：アイン、６つの看板を全て記憶してきたんだね？たいしたモノだ。");

                UpdateMainMessage("ラナ：っな！？そ、そういえば、さっきスラスラ言ってたわね。");

                UpdateMainMessage("アイン：ああ、実はラナのメモをたまにコッソリ・・・");

                UpdateMainMessage("　　　『ッドグシ！』（ラナのサイレントブローがアインの横腹に炸裂）　　");

                UpdateMainMessage("アイン：（ッグ、ヴヴオォ・・・おま、飯食ってる時にやるなよ・・・）");

                UpdateMainMessage("ハンナ：アッハハハ、アイン、女の子の持ち物覗き見しちゃ駄目だよ。");

                UpdateMainMessage("ハンナ：どうするんだい？もうこの辺で止めておくかい？");

                UpdateMainMessage("ラナ：・・・・・・");

                UpdateMainMessage("アイン：・・・・・・");

                UpdateMainMessage("ハンナ：しょーがない子達だね。そんなんじゃ最下層行けないよ？");

                UpdateMainMessage("ハンナ：じゃあ、叔母さんからとっておきのヒントを一つだけあげよう。");

                UpdateMainMessage("アインとラナ：本当ですか！！？");

                UpdateMainMessage("ハンナ：ああ、よく聞くんだね。");

                UpdateMainMessage("ハンナ：【ヒントは無い】　それが自体がヒントだよ。");

                UpdateMainMessage("ラナ：ちょっと、どういう意味ですかソレ？");

                UpdateMainMessage("ハンナ：ラナちゃん、現実ってのは解けるか解けないかなんて誰にも分からないのさ。");

                UpdateMainMessage("ハンナ：ヒントどころか、現実ってのは問いかけ自体もしてくれない。通り過ぎても返事もしない。");

                UpdateMainMessage("ハンナ：でもね、そのダンジョンにはちゃーんと、看板が立ってたんでしょう？");

                UpdateMainMessage("ラナ：ええ、確かに看板自体はあったわ。");

                UpdateMainMessage("ハンナ：まあアタシゃ行ってないから分からないけど、聞く限りじゃ同じような看板があったんだね？");

                UpdateMainMessage("ラナ：ええ、１区画毎の入り口に貼り付けてあったわ。");

                UpdateMainMessage("ハンナ：だったら、もう答えは目の前だよ。それは【在る】って事じゃないかしらね。");

                UpdateMainMessage("アイン：・・・オーケーオーケー。何となく読めたぜ！");

                UpdateMainMessage("ラナ：え、何が読めたって言うの？");

                UpdateMainMessage("アイン：え？いや、いやいや・・・答えはわかんねえけどさ。何となく・・・");

                UpdateMainMessage("ハンナ：アイン、誤魔化さないの。ちゃんと言いな。");

                UpdateMainMessage("アイン：あ、ああ。分かったよ叔母さん。ラナ、言葉通りだ【ヒントは無い】ってことは");

                UpdateMainMessage("ラナ：うん。");

                UpdateMainMessage("アイン：ヒントが無くても、答え自体は存在すると言ってるようなものだ。");

                UpdateMainMessage("ラナ：・・・うん。");

                UpdateMainMessage("アイン：つまりこう言う事さ。ヒントが無い以上、自分達の力で探し当てるしかないってな。");

                UpdateMainMessage("ラナ：・・・うん。");

                UpdateMainMessage("アイン：探せば在る。　「看板が在る」 ＝　「答えも在る」って事だ！立派なヒントじゃねえかコレも！");

                UpdateMainMessage("ラナ：・・・うん。そうだね。");

                UpdateMainMessage("アイン：っしゃ！そうと決まれば、明日のバックパック整備でもしとくか！叔母ちゃん、ごちそうさん！");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "アインは自分の部屋へと大急ぎで走っていった。";
                    md.ShowDialog();
                }

                UpdateMainMessage("ラナ：・・・アイン、本当に分かったのかしら。");

                UpdateMainMessage("ハンナ：ああ見えて、筋が良いのよあの子は。");
                
                UpdateMainMessage("ハンナ：それと、たまにラナちゃんに気を使ってるのよ、余計なお世話だよねえ？");

                UpdateMainMessage("ラナ：っふん、アイツはやっぱりバカよ。ワケ分かんないトコで分かったような顔して・・・");

                UpdateMainMessage("ハンナ：アッハハハ、もしも見つけられないようなら容赦なく引っぱたいてやんな。");

                UpdateMainMessage("ラナ：見つけられないに決まってるわ。その時は100ビンタね♪");

                UpdateMainMessage("ハンナ：ああ、その意気だよ。頑張んな！あんた達なら行けるかも知れないわ。");

                UpdateMainMessage("ラナ：叔母さん、ありがとうございました。ダンジョン進められる道をまた探してみます。");

                UpdateMainMessage("ハンナ：ああ、沢山食べて、沢山寝る。それで思いっきり活動してきな。");

                UpdateMainMessage("ラナ：今日は美味しいご飯ありがとうございました。また頑張ってきます。");

                UpdateMainMessage("ハンナ：あいよ。");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "ラナは自分の部屋へ戻っていった。";
                }

                UpdateMainMessage("ハンナ：さてと、片付けてしまうかね。");

                UpdateMainMessage("　　　【おい、ハンナ。あんまり甘やかすな。】");

                UpdateMainMessage("ハンナ：アララ、アンタいつの間にそこに居たの？");

                UpdateMainMessage("ガンツ：お前の飯、届ける時間がいつもより２５分早かった。何かあったのかと思ってな。");

                UpdateMainMessage("ハンナ：なあに、ちょっとした気まぐれだよ、気にしなさんな。");

                UpdateMainMessage("ガンツ：ラナとアイン。あのまま放っておけば止めてただろう。何故手をいれた？");

                UpdateMainMessage("ハンナ：手を入れちゃ駄目なのかい？あんたも固い人だねえ。");

                UpdateMainMessage("ガンツ：２階の最後と言えば、容こそそれぞれ違えど、立派な試練。ヒントなど提示するべきではない。");

                UpdateMainMessage("ハンナ：でもアタシゃ感じたよ。あの二人、何かあるわ。アタシにそう思わせたの。");

                UpdateMainMessage("ガンツ：なるほどな。ならば仕方あるまい。だが、あまり過度な肩入れはするな。");

                UpdateMainMessage("ハンナ：ああ、これっきりだよ。");

                we.SolveArea26 = true;

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "そして一日が過ぎた。";
                }
                CallRestInn();
            }
            #endregion
            #region "２階制覇"
            else if (this.we.CompleteArea2 && !this.we.CommunicationCompArea2)
            {
                UpdateMainMessage("アイン：２階・・・突破だぜ！！");

                UpdateMainMessage("ラナ：いろいろあったね、ホント２階は苦労したわ。");
                
                UpdateMainMessage("アイン：俺としては、最初の看板の時点でマジで【最下層】だと思ったぜ？");
                
                UpdateMainMessage("ラナ：んなワケないでしょ。それだったら１００万人居たら９０万人は解けてるわよ。");
                
                UpdateMainMessage("アイン：なあ、次から・・・３階だぜ。");
                
                UpdateMainMessage("ラナ：何よ？急にかしこまっちゃって。");
                
                UpdateMainMessage("アイン：俺、絶対に制覇してみせるからな。");
                
                UpdateMainMessage("ラナ：ッフフ、期待してるわよ。アインならやれると思うよ。");
                
                UpdateMainMessage("アイン：いや、次のダンジョン３階。このままじゃ制覇できねえ気がする。");
                
                UpdateMainMessage("ラナ：どう言う事よ？");
                
                UpdateMainMessage("アイン：まあ話は後だ。ガンツさんやハンナ叔母さんに連絡してくる。それからだな！");
                
                UpdateMainMessage("ラナ：フフ、そうね。まずは皆に連絡してきましょ♪");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "アインは一通り、町の住人達に声をかけ、時間が刻々と過ぎていった。";
                    md.ShowDialog();

                    md.Message = "その日の夜、ハンナの宿屋亭にて";
                    md.ShowDialog();
                }

                UpdateMainMessage("アイン：カンパーイ！カンパーイ！カンパーイ！ッハッハッハッハ！！");
                
                UpdateMainMessage("ハンナ：アッハハハハ、カンパイだね！");
                
                UpdateMainMessage("ラナ：もう、何回カンパイしてんのよ。これで５回目じゃない全く・・・");
                
                UpdateMainMessage("アイン：いやあ、特に看板３つ目がさ。これがワケわかんねえ質問が多くてよ！！");
                
                UpdateMainMessage("ハンナ：どんなのだったんだい？");
                
                UpdateMainMessage("アイン：得意な分野は何だとか、師匠は誰だとか・・・特に最後の１０問目なんかアレだぜ！？");
                
                UpdateMainMessage("ラナ：あ、アイン！っちょっと！！");
                
                UpdateMainMessage("アイン：ッモ、モグァ！！オマ、飯をムリに・・・モガガガガ！！");
                
                UpdateMainMessage("ハンナ：アッハハハハ、良いよ良いよ。ムリして聞かないから。");

                UpdateMainMessage("ハンナ：アイン、２階の最後はどうやって解いたんだい？言ってごらん。");

                UpdateMainMessage("アイン：ッゲホ、ッゲホ・・・あ、ああ、最後の台座だよな。");

                UpdateMainMessage("アイン：歌・・・いや、あれは【詩】って言うのか？聞こえたんだよ【詩】が。");

                UpdateMainMessage("ラナ：私は全然聞こえなかったのにね。何だかアイン、うわの空って感じだったわよ。");

                UpdateMainMessage("アイン：【詩】が聞こえてきて、それから膨大な【数式の羅列】、後はそうだな、いろんな【色彩】が見えた。");

                UpdateMainMessage("アイン：で、台座の前に立った時、不思議と【詩】【数式の羅列】【色彩】が見事に当てはまった。");

                UpdateMainMessage("アイン：俺はその言葉をありのまま口に出していた。そんな感じだったな。");

                UpdateMainMessage("ハンナ：・・・・・・なるほどねえ。そうきたかい。");

                UpdateMainMessage("ラナ：コイツ、ワケわかんない台詞連発だったわよ。");

                UpdateMainMessage("ラナ：ねえ、ハンナ叔母さんは何か分かる？");

                UpdateMainMessage("ハンナ：いいや、あたしゃ行った事無いからね。でもこれだけは言えるよ。");

                UpdateMainMessage("　　　【ハンナ！酒だ！　酒！】");
                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "武具屋のガンツがフラついた足でこちらへ寄ってきた。";
                    md.ShowDialog();
                }
                UpdateMainMessage("ハンナ：あらま！アンタ今日は武具屋いいのかい？");

                UpdateMainMessage("ガンツ：今日は店じまいだ。アイン、ラナ、おめでとう。");

                UpdateMainMessage("アイン・ラナ：ありがとうございます！");

                UpdateMainMessage("ガンツ：アイン、よくやった。日々武具を使ってもらってるワシも誇りに思える。");

                UpdateMainMessage("ガンツ：ラナ、よくやった。お前無しでアインはここまで辿りつけないだろう。");

                UpdateMainMessage("ガンツ：では、カンパイ！");

                UpdateMainMessage("アイン：カンパーイ！カンパーイ！カンパーイ！");

                UpdateMainMessage("ラナ：っもう・・・これで６回目ね。カンパイ♪");

                UpdateMainMessage("ガンツ：ところでだ、アイン。　お前は修行が足りん。");

                UpdateMainMessage("ハンナ：やだよ、この人。こんな日に説教かい？");

                UpdateMainMessage("ガンツ：ハンナ、お前は少し黙っていなさい。");

                UpdateMainMessage("ハンナ：はいよ。");

                UpdateMainMessage("ガンツ：良いか、アイン。");

                UpdateMainMessage("アイン：はい、何ですか？");

                UpdateMainMessage("ガンツ：アイン。今のままでは３階は解けないと思いなさい。");

                UpdateMainMessage("ラナ：あっ・・・そういえば、さっき。");

                UpdateMainMessage("ガンツ：うむ？");

                UpdateMainMessage("アイン：ああ、今の俺たちじゃ解けない。何となくそんな気がしてるんだ。");

                UpdateMainMessage("ガンツ：ふむ、随分と成長してるようだな。それが分かるなら話は早い。");

                UpdateMainMessage("ラナ：何で一回も行った事がないのに、そんなことが言えるのよ？アイン。");

                UpdateMainMessage("アイン：叔母さん、２階の制覇してる人はどのぐらい居るんだ？");

                UpdateMainMessage("ハンナ：そうだね。ざっと数えて、１５０人ぐらいだよ。");

                UpdateMainMessage("ラナ：ウソ・・・ほとんど残ってないじゃない。");

                UpdateMainMessage("ガンツ：２階の時点で、ほとんどのメンバーは振り落とされる。");

                UpdateMainMessage("ガンツ：３階では限られたメンバーしか生き残る事は許されていない。");

                UpdateMainMessage("ガンツ：アイン、次の３階では、より一層の鍛錬をしなさい。");

                UpdateMainMessage("アイン：はい。");

                UpdateMainMessage("ガンツ：それから、ラナ。");

                UpdateMainMessage("ラナ：あ、はい。");

                UpdateMainMessage("ガンツ：３階はラナ君の番であるが、準備はできているかね？");

                UpdateMainMessage("ラナ：・・・え？");

                UpdateMainMessage("ガンツ：ふむ・・・ふむ。どうしたものか。");
                
                UpdateMainMessage("ガンツ：ラナ君の属性は【闇】【水】【空】だね・・・ふむ。");

                UpdateMainMessage("ラナ：えっ、え、え、何で分かるんですか？？？");

                UpdateMainMessage("ガンツ：ラナ君、拳武術も少し携わってたね？");

                UpdateMainMessage("ラナ：えっ、はい。ライトニングキックなどを日々練習中です。");

                UpdateMainMessage("アイン：あれ・・・練習なのか？");

                UpdateMainMessage("ラナ：練習よ。ちゃんと手加減して打ち込んでるじゃない♪");

                UpdateMainMessage("アイン：・・・手加減・・・恐るべし。");

                UpdateMainMessage("ガンツ：ラナ君。ワシが今度StanceOfFlowの極意を教えてあげよう。");

                UpdateMainMessage("アインとラナ：っえ！？　（マジかよ！？）　（本当ですか！？）");

                UpdateMainMessage("ガンツ：後手必勝の極意。会得は難しいが、掌握すれば飛躍的な成長を遂げられる。");

                UpdateMainMessage("アイン：・・・ば、バカな。ガンツ叔父さん、あれって後手ですよ？？");

                UpdateMainMessage("ガンツ：アイン、今はラナ君に話しかけておる。");

                UpdateMainMessage("アイン：あ、はい。");

                UpdateMainMessage("ラナ：ホラ、ホラホラホラ！やっぱりあの閃きは間違いないのよ♪");

                UpdateMainMessage("ラナ：ガンツ叔父さん、よろしくお願いします。私毎日励みますから♪♪");

                UpdateMainMessage("アイン：っく・・・マジかよ。");

                UpdateMainMessage("ガンツ：ラナ君、３階では君次第だ。心して取り掛かりなさい。");

                UpdateMainMessage("ラナ：あ、ハイ！ありがとうございます！頑張ります！");

                UpdateMainMessage("ハンナ：さあさあ、酒を持ってきたよ。飯も追加しといたからドンドン食べな！");

                UpdateMainMessage("アインとラナ：ありがとうございます！！いただきまーす！");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "アインとラナはそれから団欒のひとときを過ごし、部屋へと戻っていった。";
                    md.ShowDialog();
                }

                UpdateMainMessage("ハンナ：あんたも随分とお人好しじゃないか。");

                UpdateMainMessage("ガンツ：あのままでは、３階制覇確率は０％のままだろう。");

                UpdateMainMessage("ハンナ：おやおや、確率計算までしてたのかい？あんたの計算、正確だからねえ。");

                UpdateMainMessage("ガンツ：特にラナ君の方は、かげりが出てきておる。あまり成長してないようだ。");

                UpdateMainMessage("ハンナ：ラナちゃん、相当喜んでたわよ。鍛えてどうするつもりなんだい？");

                UpdateMainMessage("ガンツ：０％ではなく１％だ。ワシが出来るのはそこまでだ。");

                UpdateMainMessage("ハンナ：１％かい？アッハハハハ。３階なのに珍しく大きい数字にしてるじゃないか。");

                UpdateMainMessage("ガンツ：ワシも少し見たくなった。お前じゃないが、確かに手を貸したくなった。");

                UpdateMainMessage("ハンナ：これで、おあいこだね。さあ、アンタも片付け手伝いなさい！");

                UpdateMainMessage("ガンツ：っふん、おあいこではない。ワシは武術を鍛えてやるだけだ。");

                UpdateMainMessage("ハンナ：アッハハハ、確かにそうだね。アタシはもう見守るのに撤するよ。");

                UpdateMainMessage("ガンツ：ワシも今回限りだ。鍛えおわった後は見守るとしよう。");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "・・・・・・その頃、アインの部屋にて・・・・・・";
                    md.ShowDialog();
                }

                UpdateMainMessage("アイン：ラナのやつにガンツ叔父が師匠として付くのか。こりゃやべえな・・・");

                UpdateMainMessage("アイン：・・・俺も３階に向けて少し訓練しておくか・・・");

                UpdateMainMessage("アイン：よし！外で練習だ！");

                GroundOne.StopDungeonMusic();

                UpdateMainMessage("　　　【ドアを開けた瞬間、目に見えない速さで何かが通り過ぎた。】　　");

                UpdateMainMessage("アイン：ん？何か突風みたいなものが・・・あれ、何か落ちてるぞ。");

                UpdateMainMessage("アイン：手紙・・・か？ラナ、ひょっとして俺に");

                UpdateMainMessage("　　　【ダンジョンゲート入り口の裏で待つ。　～ Ｖ・Ａ ～　】　　");

                UpdateMainMessage("アイン：何だこりゃ。Ｖ．Ａ・・・誰だ？");

                UpdateMainMessage("アイン：まあ丁度練習する所だったしな。行ってみるとするか！");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "その夜、アインはダンジョンゲート入り口の裏にやってきた。";
                    md.ShowDialog();
                }

                GroundOne.PlayDungeonMusic(Database.BGM10, Database.BGM10LoopBegin);
                this.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "hometown2.jpg");

                UpdateMainMessage("アイン：へえ、ダンジョンゲートの裏側ってこんな場所なんだ・・・いい広場だな。");

                UpdateMainMessage("　　　＜＜＜　一つの風がアインの全体へ触れた。そんな気がした。　＞＞＞　　");

                UpdateMainMessage("アイン：・・・ん？");

                UpdateMainMessage("　　　『アイン君。　　始めまして。　　だね。』　　");

                UpdateMainMessage("アイン：っな！？っぜ、全然見えなかったぞ！？いつ来たんだ！！？");

                UpdateMainMessage("アイン：お前が・・・　～Ｖ．Ａ～　か！？");

                UpdateMainMessage("　　　『そうだよ。本名はVerze Artie。よろしくね。』　　");

                UpdateMainMessage("アイン：・・・えぇ！？ヴェ、ヴェルゼ・アーティだって！？");

                UpdateMainMessage("ヴェルゼ：アイン君、噂通りの成長をしてるね。影ながらボクも見ていたよ。");

                UpdateMainMessage("アイン：いや、俺なんか全然まだまだです。成長なんていう言葉は程遠いですよ。");

                UpdateMainMessage("ヴェルゼ：謙遜しなくて良い。見れば分かるよ。１階突入時とは別人になってるね。");

                UpdateMainMessage("アイン：こ、こんな事言うのも失礼ですが、ヴェルゼ様。");

                UpdateMainMessage("アイン：何故俺なんかを呼び出したんですか？");

                UpdateMainMessage("ヴェルゼ：ヴェルゼで良いよ。ボクは様って付けられるのが嫌いだから。");

                UpdateMainMessage("ヴェルゼ：あと、もっと気楽に話して良いですよ。ボクも気軽に喋りたいですから。");

                UpdateMainMessage("ヴェルゼ：さて、ボクから折り入っての頼みがあるんです。聞いてもらえますか？");

                UpdateMainMessage("アイン：ヴェルゼ様・・・ヴェルゼの言う事なら何でも、何でしょうか？");

                UpdateMainMessage("ヴェルゼ：アイン君に力を貸してあげたい。ボクをパーティに入れてもらえませんか？");

                UpdateMainMessage("アイン：本当ですか！？あの伝説のFiveSeekerが！？");

                UpdateMainMessage("ヴェルゼ：伝説のFiveSeekerなんて言われると大げさだな。たいした事じゃないよ。");

                UpdateMainMessage("アイン：ええっと・・・そりゃあもう");

                UpdateMainMessage("　　　『あれ？ひょっとしてアインじゃない？オーイ、そこのバカー！！』　");

                UpdateMainMessage("アイン：ん？おお、ラナー！一体どうしたんだ？こんな所に来て？");

                UpdateMainMessage("ラナ：明日からガンツ叔父さんに鍛えてもらうために、ちょっとね。");

                UpdateMainMessage("ラナ：って、アレ？アイン・・・二人？？？");

                UpdateMainMessage("　　　＜＜＜　ラナには二人のアインが居るように見えた。そんな気がした。　＞＞＞　　");

                UpdateMainMessage("ラナ：っと、そんなわけ無いか。夜だし見間違えただけか♪　こちらの方はどなた？");

                UpdateMainMessage("アイン：ラナ！聞いて驚け！　実はこの人は！！");

                UpdateMainMessage("ヴェルゼ：ヴェルゼ・アーティと言います。よろしくね。");

                UpdateMainMessage("ラナ：・・・・・・伝説のFiveSeeker？");

                UpdateMainMessage("アイン：そう！あの伝説のFiveSeekerだ！！マジすげえ！！！");

                UpdateMainMessage("ヴェルゼ：君がラナさんですね。始めまして、実は今、アイン君にお願いをしていた所です。");

                UpdateMainMessage("ラナ：え、どどど、どうも始めまして。・・・このバカに頼みごとですか？");

                UpdateMainMessage("ヴェルゼ：アイン君とラナさん、二人のパーティにボクも加えてもらいたいのです。");

                UpdateMainMessage("アイン：おい、ラナ！入ってもらおうぜ！！");

                UpdateMainMessage("ラナ：え？え、ええ。そ、そりゃもちろん、こちらからお願いしたいぐらいです。");

                UpdateMainMessage("アイン：っな！そうだよな！？おおっしゃああああああ！！！");

                UpdateMainMessage("ヴェルゼ：どうやらパーティに入れてもらえるようですね。");

                UpdateMainMessage("アイン：もっちろんですよ！何言ってるんですか！！ホントこちらこそよろしくお願いします！");

                UpdateMainMessage("ラナ：・・・・・・　・・・・・・　・・・・・・");

                UpdateMainMessage("アイン：おい、どうしたんだよラナ！ポカーンとしてさ、もっと喜べよ！？");

                UpdateMainMessage("ラナ：あ、ヴェルゼ様。よろしくお願いします♪");

                UpdateMainMessage("ヴェルゼ：驚かせちゃったみたいだね。ごめんね、ラナさん。");
                
                UpdateMainMessage("ヴェルゼ：じゃあ早速明日から同行させてもらうけど良いかな？");

                UpdateMainMessage("アイン：もっちもちもちです！百人力期待してます！独壇場、期待してます！");

                UpdateMainMessage("ヴェルゼ：そうだ、アイン君。申し訳ないがボクは君が思ってるほど強くないんだよ。");

                UpdateMainMessage("アイン：何言ってるんですか。伝説のFiveSeekerが言う『強くない』の意味ぐらい分かりますって");

                UpdateMainMessage("ヴェルゼ：いや、そう言うわけではないんだよ。明日、また説明するよ。");

                UpdateMainMessage("アイン：はい、はいはい。分かりました！おおっしゃあ！！今日は徹夜で訓練だ！！！");

                UpdateMainMessage("ヴェルゼ：明日からよろしくね。じゃあ、ボクはこれで。");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "ヴェルゼがパーティに加わりました。";
                    md.ShowDialog();
                }
                we.AvailableThirdCharacter = true;

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "ヴェルゼはその場から去っていった";
                    md.ShowDialog();
                }

                GroundOne.StopDungeonMusic();

                UpdateMainMessage("ラナ：アイン、ちょっと良い？");

                UpdateMainMessage("アイン：ん？何だよ？");

                UpdateMainMessage("ラナ：その・・・私が言うのもなんだけど、ヴェルゼさんってアインに似てない？");

                UpdateMainMessage("アイン：はぁ？んなワケないだろ。どっからどう見ても別人だろ。");

                UpdateMainMessage("ラナ：うん、まあそうなんだけどね。でもどこか似てると思わなかった？");

                UpdateMainMessage("アイン：雰囲気も、衣装も、実力も、口調も、顔つきも、品格も、年齢も全てが別人だと思うが。");

                UpdateMainMessage("ラナ：うん・・・うん、まあそうなんだけどね。私がオカシイのかしら。");

                UpdateMainMessage("アイン：ラナ、今日は酒も沢山飲んでるし、この真夜中だ。酔ってんだろ？");

                UpdateMainMessage("ラナ：ううん、そうよね。ゴメンゴメン、何か変な事言ったね忘れて♪");

                UpdateMainMessage("アイン：ラナ、ちょっとDuelでもやっていくか？３階に向けて特訓しようぜ。");

                UpdateMainMessage("ラナ：良いわよ。アイン、言っとくけど手加減無しだからね♪");

                UpdateMainMessage("アイン：ッハッハッハ！俺が手加減なんかするわけねえだろ。ラナ、お前も手抜きするなよ？");

                UpdateMainMessage("ラナ：ええ、良いわよ。死んでも知らないからね♪");

                UpdateMainMessage("ラナ：３・・・２・・・１・・・");

                UpdateMainMessage("アイン：ＧＯ！！");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "その後、アインとラナは宿屋へ戻り、そして一日が過ぎた。";
                    md.ShowDialog();
                }

                we.CommunicationCompArea2 = true;
                GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
                this.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "hometown.jpg");
                CallRestInn();

            }
            #endregion
            #region "アイテムソート取得イベント"
            else if (this.we.CommunicationCompArea2 && this.we.InfoArea31 && !this.we.AvailableItemSort)
            {
                UpdateMainMessage("アイン：っと・・・何だっけこのアイテムは・・・");

                UpdateMainMessage("アイン：とりあえず俺の武器じゃねえな。");

                UpdateMainMessage("　　『ッポイ』");

                UpdateMainMessage("ラナ：何やってるのよ？バカアイン。");

                UpdateMainMessage("アイン：見てわかるだろ。アイテムの整理さ。");

                UpdateMainMessage("アイン：それとだな。俺は断じてバカではない。");

                UpdateMainMessage("アイン：うーん、これも俺の武器じゃねえな。。。");

                UpdateMainMessage("　　『ッポイ』");

                UpdateMainMessage("ラナ：アンタの武器以外をそこら辺に投げつけてるだけじゃない。それのどこが整理なのよ。");

                UpdateMainMessage("アイン：ちゃんと後で整理するって、心配すんな！ッハッハッハ！");

                UpdateMainMessage("ラナ：まったく。ソートってものぐらいちゃんと覚えなさいよね。");

                UpdateMainMessage("アイン：何だ？そのソートってのは？");

                UpdateMainMessage("　　『ッポイ』");

                UpdateMainMessage("ラナ：種類って意味よ。");

                UpdateMainMessage("アイン：種類が、一体何だってんだ？");

                UpdateMainMessage("　　『ッポイ』");

                UpdateMainMessage("ラナ：アイテムを整理する時の並び替えのルールにするものよ。");

                UpdateMainMessage("アイン：そんなルール、どう役に立つんだ？");

                UpdateMainMessage("　　『ッポイ』");

                UpdateMainMessage("ラナ：ちょっと！それ私がいつも渡してる赤ポーションじゃないのよ！！！");

                UpdateMainMessage("　　　『ッシャゴオォォオォォ！！！』（ラナのライトニングキックがアインに炸裂）　　");

                UpdateMainMessage("アイン：ゲフォオオオォォォ・・・す、すまねえ・・・途中からあんまり使ってねえんだ。");

                UpdateMainMessage("ラナ：まあ今じゃもうヒールやってるんだし、しょうがないんだけど。");
                
                UpdateMainMessage("ラナ：それはそれとして、ちゃんと聞きなさいよね。");

                UpdateMainMessage("アイン：・・・ハイ・・・");

                UpdateMainMessage("ラナ：幾つかの並び替える方法があるわ。");

                UpdateMainMessage("ラナ：まず最初は使用品ね。私が渡してる【赤ポーション】や【遠見の青水晶】が該当するわ。");

                UpdateMainMessage("ラナ：それが一番バックパックの上に来るようにすることができるわ。カンタンでしょ♪");

                UpdateMainMessage("ラナ：それから次がアクセサリね。");

                UpdateMainMessage("ラナ：今アインが装備してる【" + mc.Accessory.Name + "】などが該当するわ。");

                UpdateMainMessage("ラナ：さっきと同様、一番バックパックの上に来るようにすることができるのよ♪");

                UpdateMainMessage("ラナ：後は順番に武器・防具・名前、そして、レア。要は種類によって並び替えが可能になるワケよ。");

                UpdateMainMessage("ラナ：それから対象ソート以外のアイテムに関しては、以下の順序が優先されるわ。");

                UpdateMainMessage("ラナ：使用品　＞　アクセサリ　＞　武器　＞　防具");

                UpdateMainMessage("ラナ：同じ種類だった場合は名前順序で並び替えられるわよ。");

                UpdateMainMessage("ラナ：ただし、レアソートの場合は同じレア率だった場合は常に名前順になるわ。注意してね。");

                UpdateMainMessage("ラナ：っささ、カンタンでしょ。早速やってみなさいよね♪");

                UpdateMainMessage("アイン：・・・なんか、面倒くせえええ！！");

                UpdateMainMessage("ラナ：ハァァ～～～～、何でこんなカンタンなの面倒くさがるのよ・・・ホンットバカよね・・・");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "アイン達はアイテムを整理できるようになった";
                    md.ShowDialog();
                }
                we.AvailableItemSort = true;
            }
            #endregion
            #region "３階第四関門突破後"
            else if (this.we.SolveArea34 && !this.we.CompleteArea34)
            {
                UpdateMainMessage("ヴェルゼ：アイン君、すいません。ボクはちょっと救急用具でも取ってきますから・・・");

                UpdateMainMessage("アイン：ああ、任せたぜ！");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "アインは急いでハンナの宿屋へラナを運んだ・・・";
                    md.ShowDialog();
                }

                GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                UpdateMainMessage("アイン：・・・・・・・");

                UpdateMainMessage("ハンナ：・・・大丈夫みたいだよ、疲れてるだけだね。一日寝ればスッカリ良くなるよ。");

                UpdateMainMessage("アイン：・・・俺のせいだ。");

                UpdateMainMessage("ハンナ：何言ってんだい。無事に帰ってきたんだ良かったじゃないか。");

                UpdateMainMessage("アイン：俺のせいだってんだ！！");

                UpdateMainMessage("ハンナ：アイン、止めなさい。");

                UpdateMainMessage("アイン：・・・・・・・");

                UpdateMainMessage("アイン：・・・なあ、叔母さん教えてくれよ。");

                UpdateMainMessage("ハンナ：なんだい？");

                UpdateMainMessage("アイン：あのダンジョン、一体何なんだ？");

                UpdateMainMessage("ハンナ：・・・あたしゃよく知らない。答えられないね。");

                UpdateMainMessage("アイン：ウソだ！本当は何か知ってるんだろ！？今までも沢山の探索者が来てんだろ！？");

                UpdateMainMessage("ハンナ：本当だよ。中身に関しては一切知らないんだよ。");

                UpdateMainMessage("アイン：ラナのやつ、変な鏡のせいで妙にテンションハイになるし。");

                UpdateMainMessage("アイン：オマケに最後の台座のアレ。何なんだよアレは！教えてくれよ！？");

                UpdateMainMessage("ガンツ：アイン、落ち着きなさい。心を乱すな。");

                UpdateMainMessage("アイン：あの台座の時、ラナが・・・ラナが分けわかんねえ台詞を連発してさ。");

                UpdateMainMessage("ガンツ：アイン、お前とて２階で一度体験しているはず。なんとも無かろう？");

                UpdateMainMessage("アイン：・・・ああ。");

                UpdateMainMessage("ガンツ：いいか、あの台座はな。");

                UpdateMainMessage("ハンナ：ちょいとアンタ。");

                UpdateMainMessage("ガンツ：いいんだ。後はワシの方で何とかする。");

                UpdateMainMessage("ハンナ：まったく、１回だけじゃなかったのかい。");

                UpdateMainMessage("ガンツ：【神の七遺産】　聞いた事はあるだろう？");

                UpdateMainMessage("アイン：神の七遺産ってあの・・・ランディのボケが付けてるアレか？");

                UpdateMainMessage("ガンツ：アイン、そしてラナ君がその台座で受けたのは【神の試練】と呼ばれておる。");

                UpdateMainMessage("アイン：神の試練？");

                UpdateMainMessage("ガンツ：そう、あれは人が本来持ちうる能力を全解放させるための呼び声が設定されている。");

                UpdateMainMessage("ガンツ：かたち・形式などは入った者によって違うが、本人にとって一番馴染み深い内容が投影される。");

                UpdateMainMessage("ガンツ：それが本人へ直接呼び声として伝わり、実際に台座において機能を果たす。それが神の試練だ。");

                UpdateMainMessage("ガンツ：事後で申し訳ないが、これで何人もの人間が命を落としている。");

                UpdateMainMessage("アイン：！！！っふざっけ！！");

                UpdateMainMessage("ハンナ：もし明らかに素質が無く、駄目そうならアタシが直々に蹴飛ばしてでも帰すようにしてるのさ。");

                UpdateMainMessage("アイン：ラナが・・・もしラナがこれで死んでいたら！！！");

                UpdateMainMessage("ガンツ：大丈夫だ。ワシが鍛えておる。");

                UpdateMainMessage("アイン：万が一ってあるだろうが！？");

                UpdateMainMessage("ハンナ：アイン、およし！");

                UpdateMainMessage("        『ラナ：（んん・・・アイン・・・ぁんた・・・バカじゃない？』（寝言のようだ）");

                UpdateMainMessage("アイン：ったく・・・人の気も知らずに・・・夢ん中まで俺がバカってか・・・ハハ");

                UpdateMainMessage("ガンツ：万が一は在り得る。だが、ハンナとワシが共に力を貸す事となった。");

                UpdateMainMessage("ハンナ：そうだよ。めったに無い事だからね。");

                UpdateMainMessage("ガンツ：ゆえにワシはラナ君を鍛える事とした。これで失敗は無い。そう踏んだのだ。");

                UpdateMainMessage("アイン：ハンナ叔母さん、ガンツ叔父さん、そうか・・・すまねえ。");

                UpdateMainMessage("ハンナ：やだね、あたしゃ何もしてないよ。この旦那がサポートしてくれたのさ。");

                UpdateMainMessage("ガンツ：ハンナ、お前とて随分と宿屋で介抱したもんだ。");

                UpdateMainMessage("アイン：その神の試練とやら、ラナも超えたって事ですよね？");

                UpdateMainMessage("ガンツ：そうだ。今日はゆっくりと休ませると良い。アイン、お前もよく頑張った。");

                UpdateMainMessage("ハンナ：ラナちゃんはアタシが見ておくから。アイン、あんたも十分休んできな。顔に出てるよ。");

                UpdateMainMessage("アイン：はい。じゃあ俺も一足先に休ませてもらいます。");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "アインは自分が取っている部屋へと戻った・・・";
                    md.ShowDialog();
                }

                UpdateMainMessage("アイン：ふう、そういや３階の守護者がまだだったな・・・");

                UpdateMainMessage("アイン：っいや、明日はダンジョン探索は休みにしておくかな。");

                UpdateMainMessage("アイン：ラナのやつ倒れるぐらいだったんだ。少し休ませないとな。");

                UpdateMainMessage("アイン：・・・ん？そういや・・・ヴェルゼのやつ。");

                UpdateMainMessage("アイン：あいつ救急用具取りに行くって、全然姿見せないな。");

                UpdateMainMessage("アイン：まあ良いか。明日聞いてみる事にするか。正直疲れたぜ今日は・・・。");

                UpdateMainMessage("アイン：・・・　・・・");

                UpdateMainMessage("アイン：・・・・・");

                UpdateMainMessage("アイン：・・");

                // [警告]：幻想世界（真実世界）の描写はリアルに描いてください。これは真実系イベントからラナイベントにしてください。
                //UpdateMainMessage("        『・・・おーい・・・そこのバカー！』");

                //UpdateMainMessage("アイン：・・・ん？何だラナか？");

                //UpdateMainMessage("アイン：ん？んん、誰もいねえな。おっかしいな。");

                //UpdateMainMessage("アイン：・・・");
                we.CompleteArea34 = true;
                CallRestInn();

                UpdateMainMessage("アイン：・・・Ｚｚｚ");

                UpdateMainMessage("        『アイン撲滅奥義：　インフィニティ・ブロー！！』");

                UpdateMainMessage("        『ボボ・ボボボボボ・ッボ！ドガッシャアアアアァァァン！！！』（部屋のドアが破壊された）");

                UpdateMainMessage("アイン：ん？うお！？うおお！！！うおおおおおおぉぉ！待て待て待て待て待て！！！");

                UpdateMainMessage("ラナ：真奥義：　インフィニティ・キック！！！　ハアアアァァァ！！！");

                UpdateMainMessage("アイン：・・・ヴォアアアアァァァァ・・・（パリーン）");
                
                UpdateMainMessage("        『アインは部屋の窓から落ちました』");

                UpdateMainMessage("ラナ：うーっん、絶好調だわ！！　アイン、今日も良い朝ね♪");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "ダンジョンゲート裏の広場にて。";
                    md.ShowDialog();
                }

                UpdateMainMessage("アイン：おまえさ。少し手加減っての覚えたら？・・・いっつつつ・・・");

                UpdateMainMessage("ラナ：ランディスお師匠さんは手加減しないんでしょ？");

                UpdateMainMessage("アイン：限度がある。ハンナ叔母さんの宿屋、壊すつもりかよ。");

                UpdateMainMessage("ラナ：大丈夫、ちゃんと叔母さんの許可は取ってあるの、ご心配なく♪");

                UpdateMainMessage("アイン：マジかよ。何で許可が降りてんだよ。ったくハンナ叔母さんも何考えてんだ。");

                UpdateMainMessage("ヴェルゼ：ラナさん、元気そうですね。");

                UpdateMainMessage("ラナ：ヴェルゼさん、おはよう。ええすっかり良好よ。");

                UpdateMainMessage("アイン：しかし、寝起きとは言え、受け止められなかったぜ。");

                UpdateMainMessage("アイン：ラナ、ヴェルゼにもいっぺん仕掛けてみろよ？");

                UpdateMainMessage("ラナ：え？ヴェルゼさんに？うーん・・・");

                UpdateMainMessage("ヴェルゼ：大丈夫ですよ。ラナさん、おもいきりやってみてください。");

                UpdateMainMessage("ラナ：えっとじゃ、行くわよ。構えて。");

                UpdateMainMessage("ヴェルゼ：はい、いつでもどうぞ。");

                UpdateMainMessage("ラナ：フウゥゥゥ・・・行くわよ。インフィニティ・ブロー！");

                UpdateMainMessage("        『ッボボ！』");

                UpdateMainMessage("ヴェルゼ：！？　ック！");

                UpdateMainMessage("        『ッボ・ッボボボボボ！ドオオオォォン！！！』（空中で轟音が響き渡った）");

                UpdateMainMessage("ラナ：え、っちょ、っちょっと！当たってないじゃない！");

                UpdateMainMessage("ヴェルゼ：さて、これはなかなかですね。驚きました。");

                UpdateMainMessage("ヴェルゼ：３階始めの頃とはまるで別人ですね。アイン君、これは痛かったでしょう？");

                UpdateMainMessage("アイン：やっぱそうだろ。ラナ、お前の拳武術、すげえ成長しているぞ。");

                UpdateMainMessage("ラナ：待ってよ、何で当たってないわけ？？");

                UpdateMainMessage("ヴェルゼ：ラナさん、すいません。ちょっとボクの方で卑怯な手を使いました。");

                UpdateMainMessage("ラナ：卑怯？どういう事なの？");

                UpdateMainMessage("ヴェルゼ：OneImmunityとGenesisの組み合わせで、局所物理ダメージを連続無効化しました。");

                UpdateMainMessage("ラナ：・・・え？っええ？");

                UpdateMainMessage("ヴェルゼ：簡単に言うと、物理ダメージ無効化の状態です。");

                UpdateMainMessage("ラナ：そ、そうなんだ。やっぱりヴェルゼさん凄いですね。そんなモーション全然見えないんだもの。");

                UpdateMainMessage("ヴェルゼ：いえ、初回モーションの間に、一撃だけ実は入ってますよ。ホラ、見てください。");

                UpdateMainMessage("        『ヴェルゼの左腕の後ろ側に少しだけアザが残っている。』");

                UpdateMainMessage("アイン：FiveSeekerヴェルゼ様に一撃入れてやがる・・・ほんと危ないヤツだな・・・");

                UpdateMainMessage("ラナ：ヴェルゼさんありがとう。また鍛錬を重ねていくわ。");

                UpdateMainMessage("ヴェルゼ：ええ、その調子でアイン君を追い抜いてしまって構いませんよ。");

                UpdateMainMessage("アイン：おっしゃ、俺も負けてられねえ。さあ、３階の守護者！倒すとしようぜ！！");

                UpdateMainMessage("ラナ：残るは守護者ね。張り切っていきましょう♪");

                UpdateMainMessage("ヴェルゼ：行きましょう。４階到達は、すぐそこです。");

            }
            #endregion
            #region "３階制覇"
            else if (this.we.CompleteArea3 && !this.we.CommunicationCompArea3)
            {
                UpdateMainMessage("アイン：・・・これで３階突破だ・・・");

                UpdateMainMessage("ラナ：何よ、しんみりしちゃって。もっと派手に喜ばないの？");

                UpdateMainMessage("アイン：ラナ、ちょっと話があるんだ。ヴェルゼにも後で言おうと思う。");

                UpdateMainMessage("ヴェルゼ：アイン君、ボクはちょっと４階探索用のアイテムを一通り整備してきます。");

                UpdateMainMessage("アイン：そうか。じゃあまた後でな。");

                UpdateMainMessage("ラナ：・・・ホラホラ、ガンツ叔父さんやハンナ叔母さんに報告しにいこう♪");

                UpdateMainMessage("アイン：ああ！今日も一杯飲んで食うとするか！！ッハッハッハ！！");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "アインは一通り、町の住人達に声をかけ、時間が刻々と過ぎていった。";
                    md.ShowDialog();

                    md.Message = "その日の夜、ハンナの宿屋亭にて";
                    md.ShowDialog();
                }

                UpdateMainMessage("アイン：いや、そこでラナのやつがさ。おかしいんだってコレが！ッハッハッハ！");

                UpdateMainMessage("ラナ：なによ、光が見えたおかげで解けたんじゃないの、少しは認めなさいよね？");

                UpdateMainMessage("アイン：あげくの果ては『いいから良いから、ッネ♪』っとかありえねえ語尾でさ。ッハッハッハ！");

                UpdateMainMessage("ラナ：っう、うそ！？私そんな変な言い方してたわけ？アイン誇張して言ってない？？");

                UpdateMainMessage("アイン：ああ、あぁ？お前、覚えてないのかよ？");

                UpdateMainMessage("ラナ：覚えてないわよ。アインあんた嘘付いてるんじゃないの？");

                UpdateMainMessage("アイン：ん？ああ・・・");

                UpdateMainMessage("アイン：嘘だ！！！");

                UpdateMainMessage("アイン：ッハッハッハッハ！");

                UpdateMainMessage("        『ドバッキャアァァァァァ！！！』（ラナの轟・ライトニングキックが炸裂）");

                UpdateMainMessage("ハンナ：アッハッハッハ。ラナちゃん、こりゃまた随分と腕を上げたねえ。");

                UpdateMainMessage("アイン：いっつつつ、本当に威力が上がってるし、マジで痛いつうの。");

                UpdateMainMessage("ラナ：アイン、話って何よ。言ってみなさい。");

                UpdateMainMessage("アイン：ん？あ、ああ・・・俺はな。正直、３階制覇はもっと嬉しいもんだと思ってた。");

                UpdateMainMessage("アイン：だが、実際は違った。制覇した事で今までに経験した事のない不安が広がったんだ。");

                UpdateMainMessage("ラナ：怖気づいたのね。何だったらアインはおとなしくしてて、私が制覇するとか♪");

                UpdateMainMessage("アイン：いや、話はソコなんだ、ラナ。");

                UpdateMainMessage("アイン：ラナ、このダンジョン制覇、ここら辺で止めておかねえか？");

                UpdateMainMessage("ラナ：・・・え？");

                UpdateMainMessage("アイン：叔母さん、３階層の到達者はざっと何人だ？");

                UpdateMainMessage("ハンナ：１２人だよ。");

                UpdateMainMessage("ラナ：１２人って・・・FiveSeeker様が５人だから");

                UpdateMainMessage("ラナ：残る７人って事は、後２～３パーティぐらいしか到達してないじゃない。");

                UpdateMainMessage("ハンナ：そうだよ。ラナちゃんとアインはもう指折りに数えられる所にいるって事だよ。");

                UpdateMainMessage("アイン：ラナ、このダンジョンには名だたるパーティーが深奥に挑んでいるんだ。");

                UpdateMainMessage("アイン：ある者は傷つき、ある者は命を落とし、ある者は心を病んだ。");

                UpdateMainMessage("アイン：この謳い文句。ジョークでも誇張でも何でもねえ。言葉通りマジだ。");

                UpdateMainMessage("ラナ：アイン、見損なったわ。");

                UpdateMainMessage("アイン：何？");

                UpdateMainMessage("ラナ：アンタがそんな事言うとは思わなかったって話よ。");

                UpdateMainMessage("アイン：俺はな、慎重に考えた上で言ってるんだ。");

                UpdateMainMessage("ラナ：アンタの慎重な考えなんてアテにならないわよ、何の話かと思えば。");

                UpdateMainMessage("ラナ：じゃあ私から質問してあげるわ、アイン、もうダンジョン探索は止めておく？");

                UpdateMainMessage("アイン：・・・・・・");

                UpdateMainMessage("ラナ：ッハイ、決まり♪");

                UpdateMainMessage("アイン：っくそう、どこでそんな方法を覚えたんだよ、お前。");

                UpdateMainMessage("ラナ：どこだって良いじゃない、そんな事は。ッササ食べて飲んで明日に備えましょ♪");

                UpdateMainMessage("アイン：ああ、そうだな！考えててもしょうがねえ！とことん今日は食べて飲むぜ！！");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "アインとラナはその後、ひとときの団欒を楽しみ、部屋へと戻っていった。";
                    md.ShowDialog();
                }

                UpdateMainMessage("アイン：３階で無事だったとは言え、ラナのやつ・・・");

                UpdateMainMessage("アイン：４階か・・・どんな困難があろうとも俺は辿り着いて見せるぜ・・・！");

                UpdateMainMessage("アイン：っと、バックパック整備だな・・・ラナの分と、ヴェルゼのやつと・・・");

                UpdateMainMessage("アイン：そういや、まだヴェルゼに一発も当て切れていねえ・・・さすがFiveSeekerだな。");

                UpdateMainMessage("アイン：・・・っくそ、落ちつかねえな。");

                UpdateMainMessage("アイン：やっぱダンジョンゲート裏広場で腕ならしをしておくか。");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "ダンジョンゲート裏の広場にて";
                    md.ShowDialog();
                }

                GroundOne.StopDungeonMusic();
                this.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "HomeTown2.jpg");

                UpdateMainMessage("アイン：GaleWind！・・・っこっから・・・FireBall！");

                UpdateMainMessage("アイン：っち・・・よく考えたら２回同じ事やってるだけか・・・");

                GroundOne.PlayDungeonMusic(Database.BGM10, Database.BGM10LoopBegin);

                UpdateMainMessage("　　　『アイン君。　　良い線、　　行ってますよ。』　　");

                UpdateMainMessage("アイン：！！　ヴェルゼか！？");

                UpdateMainMessage("　　　＜＜＜　一つの風がアインの全体へ触れた。そんな気がした。　＞＞＞　　");

                UpdateMainMessage("ヴェルゼ：アイン君、GaleWindは使い方によって非常に強力です。");

                UpdateMainMessage("アイン：・・・いったい何時からそこに居たんだ？");

                UpdateMainMessage("ヴェルゼ：ついさっきです。ボクも何となく寝付けなくて。");

                UpdateMainMessage("アイン：教えてくれ。４階はどんな場所なんだ？");

                UpdateMainMessage("ヴェルゼ：アイン君、ボクと一度勝負しませんか？");

                UpdateMainMessage("アイン：質問に答えてくれ。FiverSeekerなら一度４階を制覇してるんだろ？");

                UpdateMainMessage("ヴェルゼ：それは答えられません。パーティによってダンジョンの構成は違います。");

                UpdateMainMessage("アイン：じゃあヴェルゼの時はどんなのだったんだ？教えてくれよ？");

                UpdateMainMessage("ヴェルゼ：アイン君、４階に踏み込む前から随分と不安がってますね。");

                UpdateMainMessage("アイン：だったら何だよ？");

                UpdateMainMessage("ヴェルゼ：アイン君、ひょっとして怖いんじゃないですか？");

                UpdateMainMessage("アイン：何がだよ？");

                UpdateMainMessage("ヴェルゼ：真実を知る事が・・・ね。");

                UpdateMainMessage("アイン：ヴェルゼ、勝負だ。");

                UpdateMainMessage("ヴェルゼ：ええ、良いですよ。いつでもかかってきてください。");

                GroundOne.StopDungeonMusic();

                mc.CurrentLife = mc.MaxLife;
                mc.CurrentMana = mc.MaxMana;
                mc.CurrentSkillPoint = mc.MaxSkillPoint;

                bool result = EncountBattle("ヴェルゼ・アーティ");

                if (!result)
                {
                    UpdateMainMessage("ヴェルゼ：話にならないですね、アイン君。動きが鈍っていますよ。");

                    UpdateMainMessage("アイン：ッゲハァ！！・・・ッガ・・・んだよ、全然勝てねえ・・・ッゲ・ゲホ・・・ウゴォ");

                    UpdateMainMessage("        『アインは致命傷とも思える大量の血を吐いた！』");

                    UpdateMainMessage("ヴェルゼ：CelestialNovaです。　ライフを元に戻しておきましょう。");

                    mc.CurrentLife = mc.MaxLife;
                }
                else
                {
                    // [警告]：話を盛り上げるため、万が一勝利した場合は何かフラグを作って盛り上げてください。
                    we.DefeatVerze = true;
                    UpdateMainMessage("アイン：どうだ！俺の方が強いだろうが！！");

                    UpdateMainMessage("ヴェルゼ：さて、確かにアイン君。強くなりました。ですが果たしてどうでしょうか？");

                    UpdateMainMessage("アイン：どういう意味だ？　っうお！？");

                    UpdateMainMessage("　　　＜＜＜　切り裂く風がアインの全体へ直接触れ始めた！　＞＞＞　　");

                    UpdateMainMessage("        『ッヒュ・・・ッヒュヒュン・・・ズガガガ！』");

                    UpdateMainMessage("ヴェルゼ：GaleWind, CarnageRush, WordOfPower,そしてGenesis＋２回行動による究極コンボ");

                    UpdateMainMessage("        『ッガガ！　ガガッガガガ！！　ッヒュヒュン』");

                    UpdateMainMessage("ヴェルゼ：ボクのお気に入り、受け取ってください。");
                    
                    UpdateMainMessage("        『ッヒュ、ッヒュヒュン、ッガ、ッガガ、ッガガガガガ！！』");

                    UpdateMainMessage("ヴェルゼ：瘴技　インヴィジブル・ハンドレッド・カッター！");

                    UpdateMainMessage("        『ッガ、ガガガガガ、ズガガガガガガガァァ！！！ズゴゴゴォオオオォン・・・』");

                    UpdateMainMessage("アイン：ッゲハァ！！・・・ッガ・・・んだ今の全然見えねえ・・・ッゲ・ゲホ・・・ウゴォ");

                    UpdateMainMessage("        『アインは致命傷とも思える大量の血を吐いた！』");

                    UpdateMainMessage("ヴェルゼ：CelestialNovaです。　ライフを元に戻しておきましょう。");

                    mc.CurrentLife = mc.MaxLife;
                }

                UpdateMainMessage("アイン：・・・４階は・・・何がある？");

                UpdateMainMessage("　　　『　　　　　　　絶望です。　　　　　』　　");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "アインはその場で気を失った・・・";
                    md.ShowDialog();
                }

                GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                UpdateMainMessage("・・・・・・");

                UpdateMainMessage("・・・・");

                UpdateMainMessage("・・");

                UpdateMainMessage("　　　『っもう、アインったら変に弱音なんか吐いちゃって、らしくないったら・・・あら。』　");

                UpdateMainMessage("ラナ：っちょっと、そこのバカ。起きなさいよ？");

                UpdateMainMessage("アイン：・・・ああ。");

                UpdateMainMessage("ラナ：なんだ、起きてたのね。何こんなとこで居眠りしてんのよ？");

                UpdateMainMessage("アイン：ヴェルゼにDuelで負けちまった。");

                UpdateMainMessage("ラナ：ヴェルゼさんとやってたの？もう居ないみたいだけど。");

                UpdateMainMessage("アイン：FiveSeekerの中でヴェルゼは確か、技の達人だったよな？");

                UpdateMainMessage("ラナ：そうね、確か【天空の翼】の保持者で、「姿捕らえたもの存在せず」でしょ。");

                UpdateMainMessage("アイン：ヴェルゼのやつ、単に神の七遺産に頼った早さだけじゃねえ。力も俺以上だ。");

                UpdateMainMessage("ラナ：そりゃあヴェルゼさん、FiveSeekerだし。元々のベース能力が違うんじゃない？");

                UpdateMainMessage("アイン：あれで、全盛期じゃねえっつってんだ。信じられねえ・・・心が折れちまいそうだ。");

                UpdateMainMessage("アイン：ランディのボケはどうやってヴェルゼと戦ってたんだろうな・・・");

                UpdateMainMessage("ラナ：今のアインに言うと悪いんだけど。");

                UpdateMainMessage("アイン：ん？気にするな。言ってみろ。");

                UpdateMainMessage("ラナ：ヴェルゼさんって、私達と出会った当初から、ずっと手加減してるような雰囲気ない？");

                UpdateMainMessage("アイン：ああ、俺もそんな気がするぜ。");

                UpdateMainMessage("ラナ：私達がまだ習得できそうもない内容を軽くやっちゃう時あるし。");

                UpdateMainMessage("ラナ：それに各魔法・スキルのコンビネーションが逸脱していると思わない？");

                UpdateMainMessage("アイン：加えて、初期モーションもほとんど見えねえしな。");

                UpdateMainMessage("アイン：何となくだが、そういうのを見てると勝てる気がしなくなってくるんだよな。");

                if (result)
                {
                    UpdateMainMessage("アイン：特に最後のは防ぎようがなかったぜ。");

                    UpdateMainMessage("ラナ：どんなのよ？");

                    UpdateMainMessage("アイン：正確には覚えてねえが・・・まずGaleWindだ。");

                    UpdateMainMessage("アイン：それを同時に２回やっていた。つまり・・・３回行動になる。");

                    UpdateMainMessage("アイン：そこから更に同時に２回であるにも関わらず、CarnageRushとWordOfPowerを繰り出していた。");

                    UpdateMainMessage("ラナ：っちょ・・・何それ・・・");

                    UpdateMainMessage("アイン：CarnageRushを防御しても、WordOfPowerが同時に来るんだ。しかも３体が２回だ。防げねえ。");

                    UpdateMainMessage("アイン：その合計６回を同時にもう２回やるよう、Genesisとかいうスペルをぶっ放してた。");

                    UpdateMainMessage("ラナ：６回を２回って・・・で、CarnageRushというのは何回出てたのよ？");

                    UpdateMainMessage("アイン：５回ぐらいだ。");

                    UpdateMainMessage("ラナ：６，２，５・・・って合計６０回じゃない！！　ええーーーーー！？");

                    UpdateMainMessage("アイン：Genesis自体も２回詠唱だ。ヴェルゼはインビジブル・ハンドレッド・カッターと名付けてた。");

                    UpdateMainMessage("ラナ：じゃあ１２０回・・・・・・・・・１００回超えてる・・・・ウソ・・・");

                    UpdateMainMessage("アイン：多分、あれでやっと手加減抜きなんだ。初めて見たぜ。");
                }

                UpdateMainMessage("ラナ：私のインフィニティ・ブローなんて赤子同然なのかも知れないわね。");

                UpdateMainMessage("アイン：とにかくFiveSeekerはすげえ。ダンジョン最下層制覇はそれだけすげえんだよ、やっぱり。");

                UpdateMainMessage("アイン：おっしゃ、何かヴェルゼにDuel負けしてから、やる気出てきたぜ！！");

                UpdateMainMessage("ラナ：ッフフ、アイン昔から負けず嫌いね。FiveSeeker目指すんなら頑張んないとね♪");

                UpdateMainMessage("アイン：俺は４階制覇するぜ。ラナ、お前も協力してくれ！！");

                UpdateMainMessage("ラナ：ええ、もちろんよ。まあアインが足手まといになるようなら、私が先頭になるわ。");

                UpdateMainMessage("アイン：ッハッハッハ！言ってくれるぜ。なあ今度さ、ヴェルゼに俺達２人で挑んでみようぜ？");

                UpdateMainMessage("ラナ：そうね、２人なら何とか倒せるかもしれないわね。Duel名義ならヴェルゼさん受けてくれそうだし。");

                UpdateMainMessage("アイン：４階さ、絶対突破しような！");

                UpdateMainMessage("ラナ：ええ、頑張りましょ♪");

                this.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "HomeTown.jpg");
                we.CommunicationCompArea3 = true;
                CallRestInn();
            }
            #endregion
            //#region "４階初回突入時"
            //else if (!this.we.CommunicationEnterFourArea && this.we.CompleteArea3 && this.we.CommunicationCompArea3)
            //{
            //    UpdateMainMessage("ハンナ：・・・いよいよだねえ。");

            //    UpdateMainMessage("ガンツ：ふむ、ここからだ。いよいよ辛くなるぞ。");

            //    UpdateMainMessage("ハンナ：手助け無しだよ、あんた。");

            //    UpdateMainMessage("ガンツ：おまえこそ、あまり知恵を貸すでないぞ。");

            //    UpdateMainMessage("ハンナ：アッハハハ。　あたしゃ、この辺からはまったく助けにならないよ。");

            //    UpdateMainMessage("ハンナ：でも最初は半信半疑だったけど、ラナちゃんが意外と助け舟になってるねえ。");

            //    UpdateMainMessage("ガンツ：そうだな、あの二人はひょっとしたら、何かあるのかも知れんな。");

            //    UpdateMainMessage("ガンツ：アイン・・・ラナ・・・");
                
            //    UpdateMainMessage("ガンツ：ワシの期待を大きく裏切ってみせよ。頼んだぞ。");

            //    this.we.CommunicationEnterFourArea = true;
            //}
            //#endregion
            #region "４階初回戻り"
            else if (this.we.InfoArea46 && !this.we.InfoArea47)
            {
                UpdateMainMessage("アイン：おっしゃ、戻ってきたぜ。いっぺん宿屋に戻るとするか。");

                UpdateMainMessage("ヴェルゼ：アイン君、すいませんがボクは少しファージル宮殿に行ってきます。");

                UpdateMainMessage("アイン：ここから近くは無いはずだが、すぐに行き来できるのか？");

                UpdateMainMessage("ヴェルゼ：ええ、とっておきの転送詠唱ポイントがありますから、大丈夫です。");

                UpdateMainMessage("アイン：そうか。じゃあまた明日よろしくな。");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "ヴェルゼはダンジョンゲート裏の広場の先へ去っていった。";
                    md.ShowDialog();
                }

                UpdateMainMessage("アイン：さてと。ラナ、お前も宿屋で一緒に飯でも食べるか？");

                UpdateMainMessage("ラナ：え？う、うん。");

                UpdateMainMessage("アイン：おいおい、大丈夫かよ。やけに素直だな・・・");

                UpdateMainMessage("ラナ：気にしないで、っささ行きましょ♪");

                UpdateMainMessage("アイン：あ、ああ。");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "アインとラナはハンナの宿屋へ向かった。";
                    md.ShowDialog();
                }

                UpdateMainMessage("アイン：ふうっ、上手かったぜ。ハンナ叔母さん、ごちそうさま！");

                UpdateMainMessage("ハンナ：はいよ、お粗末さま。");

                UpdateMainMessage("ラナ：叔母様、ごちそうさま。");

                UpdateMainMessage("ハンナ：はいよ、ラナちゃんもお粗末さま。");

                UpdateMainMessage("アイン：４階、あの回廊。物凄く長くかんじないか？");

                UpdateMainMessage("ラナ：そうね。");

                UpdateMainMessage("アイン：今の所、妙な仕掛けや変な謎かけもねえ。このまま突っ切ろうぜ！");

                UpdateMainMessage("ラナ：ええ、頑張りましょ。");

                UpdateMainMessage("アイン：じゃ、俺は部屋に戻ってるぜ。ラナ、また明日な。");

                UpdateMainMessage("ラナ：うん、オヤスミ。");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "アインとラナは各自、自分の予約した部屋へ行った。";
                    md.ShowDialog();
                }

                UpdateMainMessage("アイン：・・・ッチ。どうなってんだこりゃ。");

                UpdateMainMessage("アイン：４階は・・・順調のはずだ。気味悪いぐらい何もねえしな。");

                UpdateMainMessage("アイン：いける・・・行けるはずだ。");

                UpdateMainMessage("アイン：変に考えるな。落ち着け俺。");

                UpdateMainMessage("アイン：バックパック整理でもして、ゆっくり寝れば良いさ。");

                UpdateMainMessage("アイン：・・・　・・・");

                UpdateMainMessage("アイン：・・・");

                UpdateMainMessage("アイン：・・");

                // [警告]：真実世界の描写は何度も何度も何度も何度も何度も推敲してください。
                UpdateMainMessage("　　　　『アインはその日、夢を見た』");

                UpdateMainMessage("　（（　・・・　何か見える　・・・　））");

                UpdateMainMessage("　（（　・・・　何だ？　・・・　家と一面に草原　・・・　？　））");

                UpdateMainMessage("　（（　・・・　それとイヤリング　・・・　誰のものだ　・・・　？　））");

                UpdateMainMessage("　（（　・・・　それと　・・・　空が　・・・　快晴　・・・　））");

                UpdateMainMessage("　（（　・・・　（ッハハハ）　・・・　誰か　・・・　笑ってやがる　・・・　））");

                UpdateMainMessage("　（（　・・・　無数の鳥　・・・　何か聞こえ　・・・　詩？　・・・　））");

                UpdateMainMessage("　（（　・・・　・・・　・・・　・・・　））");

                UpdateMainMessage("アイン：うおおぉおぉ！？！？　ハァッハァッ・・・");

                UpdateMainMessage("アイン：何だ、夢か・・・ックソ、何だってんだ一体。");

                UpdateMainMessage("アイン：・・・ラナのやつ・・・何か俺に聞こうとしてたな。");

                UpdateMainMessage("        ＜＜＜　ラナ：あんた、何か隠してるでしょ？　＞＞＞");

                UpdateMainMessage("アイン：隠してるって何のことだ。");

                UpdateMainMessage("アイン：駄目だ。全然身に覚えがねえ。");

                UpdateMainMessage("アイン：・・・俺が何か重大な事を忘れているのか？落ち着け、思い出せ・・・");

                UpdateMainMessage("アイン：・・・");

                UpdateMainMessage("アイン：・・");

                UpdateMainMessage("アイン：・");

                UpdateMainMessage("アイン：ックソ、何も思い出せねえ。元々何もねえっての。");

                UpdateMainMessage("アイン：明日ダンジョンでまた、ラナのやつにそれとなく話してみるか。");

                UpdateMainMessage("アイン：しかしヴェルゼのやつ、ファージル宮殿に戻るとか言ってたな。一体なんのために？");

                UpdateMainMessage("アイン：ああ、駄目だ、駄目だ！止めだ！！考えててもしょうがねえ、寝るぞ！！");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "その日、アインは幾つか考え事をしたまま再び眠りについた。";
                    md.ShowDialog();
                }

                this.we.InfoArea47 = true;
                CallRestInn();
            }
            #endregion
            #region "４階中間戻り"
            else if (this.we.InfoArea410 && !this.we.InfoArea411)
            {
                UpdateMainMessage("アイン：おし、町に戻ったぜ。ラナ！宿屋へ行くぞ！");

                UpdateMainMessage("ヴェルゼ：アイン君、本当にすいません。今日もファージル宮殿へ一旦寄ります。");

                UpdateMainMessage("アイン：ああ、了解。じゃあまた明日な。");

                UpdateMainMessage("ヴェルゼ：はい、それでは。");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "ヴェルゼはダンジョンゲート裏の広場の先へ去っていった。";
                    md.ShowDialog();
                }

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "アインとラナはハンナの宿屋へ向かった。";
                    md.ShowDialog();
                }

                UpdateMainMessage("アイン：・・・");

                UpdateMainMessage("ラナ：・・・");

                UpdateMainMessage("ラナ：・・・・・・ねえアイン。");

                UpdateMainMessage("アイン：ん？何だ？");

                UpdateMainMessage("ラナ：私ね・・・っう・・・っう、・・・ごめん、ごめんなさい。");

                UpdateMainMessage("        『ラナは顔に両手をあてて伏せてしまった。』");

                UpdateMainMessage("アイン：ダンジョン・・・やめとくか？");

                UpdateMainMessage("ラナ：違うの！！アイン、ダンジョンは進んでちょうだい！");

                UpdateMainMessage("アイン：あ、ああ・・・オーケーオーケー！");
                
                UpdateMainMessage("アイン：お前がそこまで言うんだ、いくらでも進んでやるぜ！ッハッハッハ！");

                UpdateMainMessage("ラナ：・・・・・・");

                UpdateMainMessage("アイン：なあ、さすがに・・・ごめんなさいだけじゃ分からないぞ。");

                UpdateMainMessage("アイン：一体何を謝ってるんだ？もし苦しくなければ言ってみろ。");

                UpdateMainMessage("ラナ：・・・・・・");

                UpdateMainMessage("ラナ：・・・・・・");

                UpdateMainMessage("ラナ：・・・・・・");

                UpdateMainMessage("ラナ：・・・・・・");

                UpdateMainMessage("ラナ：・・・・・・");

                UpdateMainMessage("アイン：・・・・・・");

                UpdateMainMessage("ラナ：・・・・・・");

                UpdateMainMessage("アイン：・・・・・・");

                UpdateMainMessage("ラナ：・・・・・・");

                UpdateMainMessage("アイン：・・・・・・");

                UpdateMainMessage("        『ラナは顔から両手を離した。いつものトパーズ色の目ではなく、酷い赤になっている。』");

                UpdateMainMessage("ラナ：・・・・・・事よ。");

                UpdateMainMessage("アイン：ん？");

                UpdateMainMessage("ラナ：私の隠し事よ。");

                UpdateMainMessage("アイン：隠し事・・・か？");

                UpdateMainMessage("ラナ：・・・うん。");

                UpdateMainMessage("アイン：お、おお。隠し事な。誰だって1個や100個ぐらいあるさ。");

                UpdateMainMessage("ラナ：・・・うん。ごめんなさい・・・うん。ゴメンね・・・");

                UpdateMainMessage("アイン：わ、分かった分かったからそんな謝るな。っな？");
                
                UpdateMainMessage("アイン：ラナ、お前が謝るとどうして良いか、わかんねえんだよ。だから笑え。");

                UpdateMainMessage("ラナ：うん・・・ッフフ、ゴメンね本当に。");

                UpdateMainMessage("アイン：隠し事、話せるか？");

                UpdateMainMessage("ラナ：ううん、それは話せないわ。でも、もうじきね。もうじき話せると思うの。");

                UpdateMainMessage("アイン：そうか？っじゃ今は無理して話さなくて良いぞ。明日か明後日かその次か、またな。");

                UpdateMainMessage("ラナ：うん、うん。また次ね。ゴメンねホント、ダンジョン中断ばっかりさせちゃって。");

                UpdateMainMessage("アイン：気にするな！っしゃ、また明日から頼むぜ！！");

                UpdateMainMessage("ラナ：ええ、そっちこそ途中で根を上げないでよ♪");

                UpdateMainMessage("アイン：よし、部屋で休むとするか。じゃな！");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "アインとラナは各自、自分の予約した部屋へ行った。";
                    md.ShowDialog();
                }

                UpdateMainMessage("アイン：ラナのやつ、少しは機嫌が元通りになったんなら良いが。");

                UpdateMainMessage("アイン：さてと、バックパック整備っと・・・");

                UpdateMainMessage("アイン：・・・ッフワァ、俺も意外と疲れてるな。寝るとするか。");

                UpdateMainMessage("アイン：・・・・・・");

                UpdateMainMessage("　　　　『アインはその日、夢を見た』");

                UpdateMainMessage("　（（　・・・　それ　・・・　なんていう歌だ？　・・・　・・・　））");

                UpdateMainMessage("　（（　・・・　海と　・・・　大地　・・・　そして天空　・・・　））");

                UpdateMainMessage("　（（　・・・　聞かねえ名前だな　・・・　どこで　・・・　覚えた　・・・　））");

                UpdateMainMessage("　（（　・・・　・・・　・・・　・・・　））");

                UpdateMainMessage("　（（　・・・　（ザザアァァン・・・）　・・・　海　・・・　良い音だ　・・・　））");

                UpdateMainMessage("　（（　・・・　鳥が沢山　・・・　・・・　ん？何だ？・・・　））");

                UpdateMainMessage("　（（　・・・　・・・　（（（私も行ってみたい。良いかしら？）））・・・　・・・　））");

                UpdateMainMessage("アイン：お、おわ！！！わああぁぁぁあ！！！　ハァッハァッハァッ・・・");

                UpdateMainMessage("アイン：ひょっとして昨日の夢の続きか・・・ハァッハァッハァッ・・・");

                UpdateMainMessage("アイン：・・・ハァッハァッハァッ・・・落ち着け、落ち着け俺。");

                UpdateMainMessage("アイン：・・・ハァッハァッハァッ・・・ッフウゥ・・・ッフウウウゥゥ・・・");

                UpdateMainMessage("アイン：っくそ、４階自体はそんな難しくねえ。");

                UpdateMainMessage("アイン：それで、何でこんな状態になるんだ、ックソ！！");

                UpdateMainMessage("アイン：・・・駄目だ、落ち着いてもすぐ考えが巡っちまう。");
                
                UpdateMainMessage("アイン：何か、飲み物でも探してくるか。");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "アインは宿屋の１階カウンターまで降りてきた。";
                    md.ShowDialog();
                }

                UpdateMainMessage("アイン：あった、あった。【爽快！アクア＆ファイア】っと。");

                UpdateMainMessage("アイン：＜＜ッゴク、ッゴク・・・＞＞");

                UpdateMainMessage("アイン：・・・っふぅ・・・");

                UpdateMainMessage("アイン：・・・");

                UpdateMainMessage("アイン：・・・ラナのやつ、何をあんな謝ってるんだ。");

                UpdateMainMessage("アイン：そりゃ、隠し事をするのは悪いのかもしれない。");

                UpdateMainMessage("アイン：しかし、あの謝り方は異常だ。何回も何回も。");
                
                UpdateMainMessage("アイン：目が腫れ上がるまで泣いているじゃねえか。見てられねえ。");

                UpdateMainMessage("アイン：隠し事自体が謝ってる原因じゃねえかも知れないな。");

                UpdateMainMessage("アイン：だが、まずはそれを聞いてやらない事には・・・");

                UpdateMainMessage("        ＜＜＜　ラナ：でも、もうじきね。もうじき話せると思うの。　＞＞＞");

                UpdateMainMessage("アイン：本当に何を話すつもりだ。ラナ・・・");

                UpdateMainMessage("アイン：・・・いや、４階だ。");

                UpdateMainMessage("アイン：こんな所で、思考ばかりしては駄目だ。４階と言ったら４階だ。");

                UpdateMainMessage("アイン：４階・・・突破するぞ！！");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "アインはその後、部屋に戻って再び眠りについた。";
                    md.ShowDialog();
                }

                we.InfoArea411 = true;
                CallRestInn();
            }
            #endregion
            #region "４階終盤戻り"
            else if (this.we.InfoArea416 && !this.we.InfoArea417)
            {
                UpdateMainMessage("ラナ：戻ったわよ、アイン。っさ、一旦宿屋に行きましょ。");

                UpdateMainMessage("ヴェルゼ：ラナさん、ボクはファージル宮殿に戻って、気付け薬を探してきます。");

                UpdateMainMessage("ラナ：え、ええ。お願いするわ。");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "ヴェルゼはダンジョンゲート裏の広場の先へ去っていった。";
                    md.ShowDialog();
                }

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "ハンナの宿屋にて・・・";
                    md.ShowDialog();
                }

                UpdateMainMessage("　　　　『アインは目を閉じたまま横になっている』");

                UpdateMainMessage("アイン：・・・　・・・　どうだ　・・・　いい場所　・・・　だろ？");

                UpdateMainMessage("ラナ：・・・うん・・・");
                
                UpdateMainMessage("ハンナ：ちょいと、失礼するよ。");

                UpdateMainMessage("ラナ：あ、ハイ。");

                UpdateMainMessage("ハンナ：・・・アイン、相当しんどいだろうね。");

                UpdateMainMessage("アイン：・・・　・・・　ほら　・・・　気をつけろって　・・・");

                UpdateMainMessage("ハンナ：ものすごい脂汗だね。水とヒンヤリサワーを持ってきたよ。");

                UpdateMainMessage("ラナ：・・・私が・・・");

                UpdateMainMessage("ハンナ：およし。");

                UpdateMainMessage("ラナ：どうして・・・こうなっちゃったんだろうね、アイン。");

                UpdateMainMessage("ハンナ：どのぐらい進んだのさ？");

                UpdateMainMessage("ラナ：マップを見る限りだと、もうすぐマップ上が埋め尽くされる所。");

                UpdateMainMessage("ハンナ：アイン・・・ほら、水だよ。飲みな。");

                UpdateMainMessage("アイン：・・・ッグ、ゴク・・・ッガハァ！！　ッガハ！　グウゥゥ・・・");

                UpdateMainMessage("アイン：ガアアアァァァァァ！！！！！！");

                UpdateMainMessage("ハンナ：アイン！落ち着きなさい！ホラ大丈夫だから！");

                UpdateMainMessage("アイン：グガアアァアァアアァァ！！！！アアアァアァアアァァ！！！！");

                UpdateMainMessage("ラナ：ごめんね、アイン。ゴメンね。ごめんね。やっぱり私が");

                UpdateMainMessage("ハンナ：およし！");

                UpdateMainMessage("ハンナ：アイン！ほら、ヒンヤリサワーだよ、これも飲みな。");

                UpdateMainMessage("アイン：ガ、ッガアアァアア！！グアアアアァァァ！！！・・・・・ッハァッハァ");

                UpdateMainMessage("アイン：ッハアアァ・・・ハァ・・・ッゲホ！　ッゲホ！・・・ハアァ・・・");

                UpdateMainMessage("ハンナ：ほら、また水だよ。もう一息だよ、ほら。");

                UpdateMainMessage("アイン：・・・ハァ・・・ハァ・・・フウ・・・");

                UpdateMainMessage("アイン：・・・　・・・　ヴェ　・・・　ヴェルゼ");

                UpdateMainMessage("ハンナ：！！！！！");

                UpdateMainMessage("　　　　『ハンナの顔色は異常なまでに青ざめている』");

                UpdateMainMessage("ラナ：アイン、ヴェルゼさんはここには居ないわ。今気付け薬持ってくる所よ。");

                UpdateMainMessage("ハンナ：ラナちゃん、今何人で行動してるんだい？");

                UpdateMainMessage("ラナ：え？３人です。アインと、そしてヴェルゼさん。");

                UpdateMainMessage("ガンツ：・・・馬鹿な・・・");

                UpdateMainMessage("　　　　『駆けつけてきたガンツの顔色は執拗なまでに深刻な顔をしている。』");

                UpdateMainMessage("ガンツ：ラナ君、話がある。");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "アインを部屋で休ませて部屋を出た。　食堂にて・・・";
                    md.ShowDialog();
                }

                UpdateMainMessage("ラナ：３階を出発する時にアインが誰かと話してたんです。");

                UpdateMainMessage("ラナ：それで伝説のFiveSeekerが参加してくれるって大はしゃぎで。");

                UpdateMainMessage("ガンツ：ラナ君、きみもこの世界のからくりは知っているだろう。");

                UpdateMainMessage("ラナ：・・・ええ。");

                UpdateMainMessage("ガンツ：アインには当然だが全てがタブーだ。４階制覇するまで、まだ教えてはおらんな？");

                UpdateMainMessage("ラナ：はい。");

                UpdateMainMessage("ハンナ：ラナちゃん。アインはあのヴェルゼに出会った事はあるのかい？");

                UpdateMainMessage("ラナ：分からないわ・・・。");

                UpdateMainMessage("ラナ：でも、アインったら大はしゃぎするから、良いかなって思ったの。");

                UpdateMainMessage("ガンツ：中止しなさい。");

                UpdateMainMessage("ハンナ：アンタ、口出しは。");

                UpdateMainMessage("ガンツ：黙っておれ。");

                UpdateMainMessage("ラナ：中止って・・・ダンジョン制覇をですか？");

                UpdateMainMessage("ガンツ：うむ、ワシの見込み違いだ。");

                UpdateMainMessage("ハンナ：およし。今さら中止して何になるって言うのさ。コントロールできやしないよ。");

                UpdateMainMessage("ガンツ：黙っておれと言っておるだろう！");

                UpdateMainMessage("ラナ：どうすれば・・・良いんでしょうね・・・アイン、ごめんなさい・・・");

                UpdateMainMessage("　　　　『ラナは両手で顔を塞いだ。　肩が少し震えているようだ』");

                UpdateMainMessage("ハンナ：アンタが今中止を言った所で無駄だよ。");

                UpdateMainMessage("ガンツ：だが・・・まさか・・・あのヴェルゼとは。");

                UpdateMainMessage("ガンツ：あやつはまぎれもなく天才。");

                UpdateMainMessage("ガンツ：右に出るものはおらん。最強だ。");

                UpdateMainMessage("　　　　『ラナが再び両手を解き、ハッと顔を上げた』");

                UpdateMainMessage("ラナ：最強って、ランディスのお師匠さんじゃないんですか？");

                UpdateMainMessage("ガンツ：ヴェルゼはランディスとのＤＵＥＬでは必ず一撃でやられておる。");

                UpdateMainMessage("ラナ：それじゃやっぱり。");

                UpdateMainMessage("ガンツ：オルはよく言っておったよ。");

                UpdateMainMessage("ランディス：『ッケ。　俺の負けだ。』");

                UpdateMainMessage("ラナ：一撃で勝っているのに？");

                UpdateMainMessage("ガンツ：ヴェルゼ。あやつが本気で戦っている所は一度して見た事はない。");

                UpdateMainMessage("ガンツ：オルはそれを察した上で、負けを意識せざるを得ないと言うことだ。");

                UpdateMainMessage("ハンナ：あの子の動き、不気味なぐらい綺麗だったからね。");

                UpdateMainMessage("ガンツ：ラナ君、ヴェルゼと対峙した事は？");

                UpdateMainMessage("ラナ：練習ならありますよ。ダンジョンゲート裏で。");

                UpdateMainMessage("ガンツ：どんな感じであった？");

                UpdateMainMessage("ラナ：私の拳武術をぶつけてみたんだけど、軽く受け止められてしまいました。");

                UpdateMainMessage("ガンツ：アインの方はどうだ？");

                // ２周目、ヴェルゼＤＵＥＬで勝ち負けに応じて会話を変化させてください。
                UpdateMainMessage("ラナ：ダンジョンゲート裏でＤＵＥＬしたそうです。負けちゃったみたいですけど。");

                UpdateMainMessage("ガンツ：ヴェルゼが・・・勝ったと！？");

                UpdateMainMessage("ラナ：ええ、あのバカアインが勝てるわけ無いですし。");

                UpdateMainMessage("ハンナ：おかしいわね。");

                UpdateMainMessage("ラナ：え？");

                UpdateMainMessage("ハンナ：あの子のＤＵＥＬ戦歴は・・・");
                
                UpdateMainMessage("ハンナ：０勝４２３敗だよ。");

                UpdateMainMessage("ラナ：・・・っちょっと・・・どういう事ですか？");

                UpdateMainMessage("ハンナ：【天空の翼】の保持者ヴェルゼ、あの子は必ず負ける。そういう子なのさ。");

                UpdateMainMessage("ハンナ：どんな相手にも本気を出さない。決して勝利を求めたりしない子だったわ。");

                UpdateMainMessage("ガンツ：ワシの時も初見がそう。最初の一振り後、終わってしまったかのような雰囲気。");

                UpdateMainMessage("ガンツ：百発百中、勝てん。あの時ワシは、心底から戦慄の汗が出た。");

                UpdateMainMessage("ガンツ：だがその後、必ずカウンターを食らってヴェルゼは負ける。");

                UpdateMainMessage("ラナ：アインの時は・・・間違って加減を忘れて、勝っちゃって事？");

                UpdateMainMessage("ハンナ：これは・・・どうなってんだい。分からなくなってきたね。");

                UpdateMainMessage("ガンツ：ラナ君、ダンジョン探索を続けなさい。");

                UpdateMainMessage("ラナ：・・・え？ええっと・・・はい。");

                UpdateMainMessage("ハンナ：今回はちょっと雰囲気が違うね。ラナちゃん。");

                UpdateMainMessage("ラナ：はい。こんなのは初めてです。");

                UpdateMainMessage("ガンツ：だが・・・・・・・・・・・・　　　　いや。　　　　うむ。");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "ガンツは両眼を閉じた状態で、誰へともなく、空中へ語り始めた。";
                    md.ShowDialog();
                }

                UpdateMainMessage("ガンツ：聞こえるかね。");
                
                UpdateMainMessage("ガンツ：精進しなさい。");
                
                UpdateMainMessage("ガンツ：お前はものすごいモノを秘めている。");

                UpdateMainMessage("ガンツ：精進しなさい。");

                UpdateMainMessage("ガンツ：お前は間違いなく打ちのめされる。");

                UpdateMainMessage("ガンツ：精進しなさい。");

                UpdateMainMessage("ガンツ：途中、決してくじけてはならん。");

                UpdateMainMessage("ガンツ：精進しなさい。");

                UpdateMainMessage("ガンツ：どうしてもしんどい時は一旦休みなさい。");

                UpdateMainMessage("ガンツ：精進しなさい。");

                UpdateMainMessage("ガンツ：お前ならきっと叶えられるはずだ。");

                UpdateMainMessage("ガンツ：精進しなさい。アイン。");

                UpdateMainMessage("　　　　『ガンツは両目を開き、テーブルへ眼を戻した』");

                UpdateMainMessage("ガンツ：ラナ君、やはり君がキーになっている。間違いないじゃろう。");

                UpdateMainMessage("ラナ：はい。");

                UpdateMainMessage("ハンナ：ラナちゃん。アインを導いてやっておくれ。");

                UpdateMainMessage("ラナ：はい。わかりました。");

                UpdateMainMessage("ラナ：私も一旦部屋へ戻ります。");

                UpdateMainMessage("ハンナ：ああ、今日はゆっくり休むんだね。");

                UpdateMainMessage("ラナ：はい、ありがとうございました。");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "一方、アインの部屋にて・・・";
                    md.ShowDialog();
                }

                // [警告]：真実世界の描写は何度も何度も何度も何度も何度も推敲してください。
                UpdateMainMessage("　　　　『アインは呼吸が整った状態で夢を見ている』");

                UpdateMainMessage("　（（　・・・　だーめだ。　・・・　駄目に決まってるだろ　・・・　・・・））");

                UpdateMainMessage("　（（　・・・　ここ　・・・　家小屋と草原ばっかり　・・・　ずるい　・・・　））");

                UpdateMainMessage("　（（　・・・　・・・　だーめだ　・・・　空でも見てろ　・・・　））");

                UpdateMainMessage("　（（　・・・　・・・　・・・　・・・））");

                UpdateMainMessage("　（（　・・・　風　・・・　・・・　気持ち良いな　・・・　））");

                UpdateMainMessage("　（（　・・・　・・・　そうだ　・・・　歌ってくれ　・・・　あの詩・・・））");

                UpdateMainMessage("　（（　・・・　・・・　（（（　ラナ　）））・・・　・・・　））");

                UpdateMainMessage("アイン：・・・・・・");

                UpdateMainMessage("アイン：・・・");

                UpdateMainMessage("アイン：・・っう・・・");

                UpdateMainMessage("アイン：っつ・・・イテテテ・・・");

                UpdateMainMessage("アイン：ここは・・・叔母さんの・・・");

                UpdateMainMessage("アイン：・・・・・・ッハ！！！しまっ！！！");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "アインは慌てて扉を開けて通路へ出ようとした・・・";
                    md.ShowDialog();
                }

                UpdateMainMessage("　　　　【バチイイィィィン！！！】");

                UpdateMainMessage("ラナ：イッタァ・・・いわね！！！何すんのよ！このバカアイン！！");

                UpdateMainMessage("アイン：いや・・・まさかそこに居るとは・・・ハ・・・ハハハ・・・");

                UpdateMainMessage("ラナ：せっかく心配してやって来たのに・・・最低ね！！");

                UpdateMainMessage("アイン：何か眼が覚めちまってさ。ダンジョンにお前ら置いて来ちまったと思って。");

                UpdateMainMessage("ラナ：ア・タ・シ・がココに連れて来たんじゃない。何言ってるのよ？");

                UpdateMainMessage("アイン：マジかよ！？");

                UpdateMainMessage("ラナ：全く、どの辺から覚えてないのよ。足元フラフラしちゃってたわよ。");

                UpdateMainMessage("アイン：マジかよ！？");

                UpdateMainMessage("ラナ：変な事ブツブツ言ってたわよ、起きてる時に夢遊病なんて聞いてあきれるわ。");

                UpdateMainMessage("アイン：マジかよ！？");

                UpdateMainMessage("ラナ：次その“マジかよ！？”って言ったら、ファイナルライトニング食らわせるからね♪");

                UpdateMainMessage("アイン：・・・悪い、それは勘弁だ。");

                UpdateMainMessage("ラナ：大丈夫なの？体の方は。");

                UpdateMainMessage("アイン：ああ、大丈夫だ。そのかわり、また夢を見ていた。");

                UpdateMainMessage("ラナ：そ、そそ、そう・・・どんなのよ？");

                UpdateMainMessage("アイン：何かを喋ってる。家と草原ばかり。風も涼しかった。");

                UpdateMainMessage("ラナ：まさに夢ね。脈略０じゃない。");

                UpdateMainMessage("アイン：いや、そうなんだけどな・・・");

                UpdateMainMessage("ラナ：アイン、明日はどうするの？");

                UpdateMainMessage("アイン：行くぜ、４階制覇。");

                UpdateMainMessage("ラナ：・・・いよいよね。");

                UpdateMainMessage("アイン：俺はな。今までの夢、脈略０じゃない気がしてるんだ。");

                UpdateMainMessage("ラナ：うん。");

                UpdateMainMessage("アイン：ラナ、お前ひょっとして・・・いや。");

                UpdateMainMessage("アイン：行こう。４階制覇に向けて。");

                UpdateMainMessage("ラナ：うん。明日頑張りましょ♪");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "アインは再び自分の部屋へと戻った。そして夜が更けていった。";
                    md.ShowDialog();
                }

                WE.InfoArea417 = true;
                CallRestInn();
            }
            #endregion
            #region "４階制覇"
            else if (this.we.CompleteArea4 && !this.we.CommunicationCompArea4)
            {
                this.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "hometown2.jpg");
                GroundOne.PlayDungeonMusic(Database.BGM10, Database.BGM10LoopBegin);
                
                UpdateMainMessage("アイン：４階制覇・・・本当はもっと喜ぶ所なんだろうけどな。");

                UpdateMainMessage("アイン：さてと、宿屋に行こうぜ。そこで話を聞かせてくれ。");

                UpdateMainMessage("ヴェルゼ：いえ、アイン君。このダンジョンのゲート裏で話しましょう。");

                UpdateMainMessage("アイン：ヴェルゼ、そういやお前、宿屋や武具屋には顔を出さないんだな。");

                UpdateMainMessage("ヴェルゼ：はい、少し人々の賑わいは苦手でしてね。ここは幸い人は集まりにくいですし。");

                UpdateMainMessage("アイン：じゃあ俺さ、【爽快！アクア＆ファイア】でも買ってくるよ。少しここで待っててくれ。");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "アインは宿屋へ一旦ジュースを買いに行き、そして再び戻ってきた。";
                    md.ShowDialog();
                }

                UpdateMainMessage("アイン：おし、買ってきたぜ。ほれ、ラナの分だ！");

                UpdateMainMessage("ラナ：うん、ありがと。");

                UpdateMainMessage("アイン：こっちはヴェルゼの分だ。受け取ってくれ。");

                UpdateMainMessage("ヴェルゼ：ええ、お気遣いありがとうございます。");

                UpdateMainMessage("アイン：なあ・・・どこから話そうか？");

                UpdateMainMessage("ヴェルゼ：ボクから話しましょう。良いですか？ラナさん。");

                UpdateMainMessage("ラナ：ええ・・・いいわよ。");

                UpdateMainMessage("アイン：話す前に俺から一つ質問させてくれ。");

                UpdateMainMessage("ヴェルゼ：良いですよ。何でしょう？");

                UpdateMainMessage("アイン：俺への手紙、あれはどうやって置いたんだ？");

                UpdateMainMessage("ヴェルゼ：・・・さすがアイン君です。そうですね、そこから話を始めましょう。");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "ヴェルゼはその場に座ったまま、目を閉じた状態で喋り始めた。";
                    md.ShowDialog();
                }

                UpdateMainMessage("ヴェルゼ：厳密に正確に表現しましょう。");

                UpdateMainMessage("ヴェルゼ：ボク自身はその手紙というものをアイン君に届けてはいません。");

                UpdateMainMessage("アイン：・・・どういう意味だ？");

                UpdateMainMessage("ヴェルゼ：アイン君、３階に行こうとした時、【練習に励もう】と思いませんでしたか？");

                UpdateMainMessage("アイン：ああ、そうだ。ラナにガンツの叔父が付いたからな。追い越されると思ったぜ。");

                UpdateMainMessage("アイン：それで、どうしても猛特訓がしたかった。そんなとこだな。");

                UpdateMainMessage("ヴェルゼ：アイン君、それが【答え】です。");

                UpdateMainMessage("アイン：・・・わかんねえ。何がどういう意味での答えなんだ？");

                UpdateMainMessage("ヴェルゼ：もう一度言いましょう。ボク自身はアイン君へ手紙を届けていません。");

                UpdateMainMessage("アイン：じゃあ誰が届けたって言うんだよ？");

                UpdateMainMessage("ヴェルゼ：・・・アイン君自身です。");

                UpdateMainMessage("アイン：・・・俺自身？・・・ッハ！ヴェルゼも面白い事言うよな！ッハッハッハ！");

                UpdateMainMessage("ヴェルゼ：アイン君、この世界ではそれが事実です。");

                UpdateMainMessage("アイン：俺はあんな手紙を書いた記憶はねえ。何かの間違いだろ。");

                UpdateMainMessage("ヴェルゼ：いえ、事実です。");

                UpdateMainMessage("ヴェルゼ：アイン君、君は確かに正に紛れもなくあの瞬間、書いたんです。");

                UpdateMainMessage("アイン：いいや、全然わかんねえぞ。ヴェルゼ、何を言ってるのか説明してくれ。");

                UpdateMainMessage("ヴェルゼ：そうですね。これでは分からないかもしれません。");

                UpdateMainMessage("ヴェルゼ：じゃあ、これはいかがでしょう？　【秤の三面鏡】覚えていますか？");

                UpdateMainMessage("アイン：ん？ああ、３階で出てきたあのワープ装置だよな。");

                UpdateMainMessage("ヴェルゼ：アイン君、このダンジョンに来る前、ファージル宮殿に訪れた事はありませんか？");

                UpdateMainMessage("アイン：何だ？唐突に・・・そうだなあ・・・");

                UpdateMainMessage("アイン：・・・っお、そうだそうだ。");

                UpdateMainMessage("アイン：ラナと一緒に訪れた事がある。っな！？");

                UpdateMainMessage("ラナ：そうね、あの時はファラ王妃、エルミ国王に一度会いたいってアインが言い出して。");

                UpdateMainMessage("アイン：何言ってる？ラナがエルミ国王を見たいって、無理やり誘ったんだろ？");

                UpdateMainMessage("ラナ：そ、そうじゃないでしょ！？アインがファラ王妃をいっぺん見るんだって言うからじゃない。");

                UpdateMainMessage("アイン：いやいや、違うぜ。確かにあの時はラナ、お前が");

                UpdateMainMessage("ヴェルゼ：ハハハ、アイン君がファラ王妃を見たかったと言うことにしておきましょう。");

                UpdateMainMessage("アイン：ッケ、あ～そうですよ。　ったく、何だってこんな話してんだ。");

                UpdateMainMessage("ヴェルゼ：どうです。ファージル宮殿で【秤の三面鏡】見てませんでしたか？");

                UpdateMainMessage("アイン：俺は知らねえ。見たことはねえな。");

                UpdateMainMessage("ラナ：私、ファラ王妃へ顔合わせしてる時に、ファージル宮殿内を紹介してもらったわ。");

                UpdateMainMessage("ラナ：その時にね、その【秤の三面鏡】を見せてもらったの。");

                UpdateMainMessage("アイン：お、そういや何かそんな事言ってたなお前、鏡が凄くきれいだったとか。");

                UpdateMainMessage("ヴェルゼ：アイン君は、過去に一度【秤の三面鏡】の情報を得ています。");

                UpdateMainMessage("アイン：ああ。");

                UpdateMainMessage("ヴェルゼ：でも、その【秤の三面鏡】が何なのかはよく知らない。");

                UpdateMainMessage("アイン：ああ、そうだな。きれいな鏡ぐらいしか知らなかったぜ。");

                UpdateMainMessage("ヴェルゼ：しかしそれでも、【ラナさんは鏡に関する知識を得ていた】という事実は知っていた。");

                UpdateMainMessage("アイン：まあ、そんな所だな。それがどうしたっていうんだ？");

                UpdateMainMessage("ヴェルゼ：そしてダンジョン３階の構想は、まさに【秤の三面鏡】だった。");

                UpdateMainMessage("ヴェルゼ：どうしてだと思います？");

                UpdateMainMessage("アイン：・・・わかんねえ。全然わかんねえぞ。");

                UpdateMainMessage("アイン：まるで俺がその事実を知っていたから、３階ダンジョンの構成がそうなった。");

                UpdateMainMessage("アイン：とでも、言いたげだな。");

                UpdateMainMessage("ヴェルゼ：それが【答え】です。");

                UpdateMainMessage("　　　　『アインはその時、異常なほどの冷や汗を背中に感じた。』");

                UpdateMainMessage("アイン：・・・・・・わかんねえ。");

                UpdateMainMessage("アイン：ヴェルゼ、お前何言ってるんだ？俺には、全然わかんねえ。");

                UpdateMainMessage("ヴェルゼ：アイン君。逃げないでください。それが【答え】なんです。");

                UpdateMainMessage("アイン：俺は逃げてなんかいねえ！俺はいつも真正面からぶつかるほうだ！");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "アインは空っぽになった【爽快！アクア＆ファイア】を投げつけた。";
                    md.ShowDialog();
                }

                UpdateMainMessage("ヴェルゼ：アイン君、続けますよ。では４階へ踏み込む時、ひどく不安がっていましたね。");

                UpdateMainMessage("ヴェルゼ：それは何故です？");

                UpdateMainMessage("アイン：そりゃあ、初めて踏み込む領域だし、ラナも一回倒れちまったしな。少しぐらい不安にはなるさ。");

                UpdateMainMessage("ヴェルゼ：それだけですか？");

                UpdateMainMessage("アイン：ああ！！それだけだよ！！");

                UpdateMainMessage("ヴェルゼ：どうして、そんな態度で声を張り上げてるんですか？");

                UpdateMainMessage("アイン：いや、すまねえ・・・何となく。");

                UpdateMainMessage("ヴェルゼ：いえ、こちらも問い詰め過ぎでした。すいません。");

                UpdateMainMessage("アイン：・・・・・・");

                UpdateMainMessage("アイン：・・・何か、あの看板。");

                UpdateMainMessage("アイン：４階の最後の、あの看板を見たくなかった。");

                UpdateMainMessage("アイン：俺はあの看板を見て、少なくとも絶望した。この世の終わりみたいな。");

                UpdateMainMessage("アイン：教えてくれよ。あの看板は何なんだ？");

                UpdateMainMessage("ヴェルゼ：真実です。");

                UpdateMainMessage("アイン：真実？");

                UpdateMainMessage("ヴェルゼ：はい、あの看板には真実が描かれています。");

                UpdateMainMessage("アイン：何だよ、その真実ってのは？");

                UpdateMainMessage("ヴェルゼ：・・・・・・");

                UpdateMainMessage("アイン：おい・・・おいおい・・・何だってんだよ。答えてくれよ。");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "ヴェルゼは目を開き、アインに気付かれないように、ラナの方へと無言で目を向けた。";
                    md.ShowDialog();
                }

                UpdateMainMessage("ラナ：アイン、私が隠している事・・・話すわね。");

                UpdateMainMessage("ラナ：アイン、ダンジョンに行こうとしたきっかけは覚えてる？");

                UpdateMainMessage("アイン：俺達の収支はダンジョンで成り立ってるだろ。金を稼がないとな。");

                UpdateMainMessage("ラナ：そうね、でも思い出してみて。なんでダンジョンで金稼ぎにしたんだっけ？");

                UpdateMainMessage("アイン：俺は昔から、力と剣バカだからな。一番性に合っていたって事さ。");

                UpdateMainMessage("ラナ：そうね。バカアインは剣一本で頑張ってきたわ。");

                UpdateMainMessage("ラナ：でも、もっと思い出してみて、何で剣の訓練をするようになったの？");

                UpdateMainMessage("アイン：ラナ、お前の隠している事とどう関係があるんだ？");

                UpdateMainMessage("ラナ：関係大アリなの、お願い、思い出してみて。");

                UpdateMainMessage("アイン：・・・そうだなあ・・・");

                UpdateMainMessage("アイン：・・・・・・ああ、そうだ。");

                UpdateMainMessage("アイン：小さい頃だ。ラナ、お前が俺に練習用の剣。　持ってきたんだったな。");

                UpdateMainMessage("ラナ：ビンゴよ♪　なんだ意外と覚えてるのね。ッフフフ");

                UpdateMainMessage("アイン：お前が思い出せって言うからだろ。ったく、そんなのが一体なんだってんだ？");

                UpdateMainMessage("ラナ：アイン、正確にはあれは練習用の剣じゃないの。");

                UpdateMainMessage("アイン：何だ、違ってたのか？まあガキの頃だからな。間違えただけだろ。");

                UpdateMainMessage("ラナ：あれはね、【神剣  フェルトゥーシュ】なの。");

                UpdateMainMessage("アイン：はあぁ！？ッハハ、ラナ。冗談も大概にしろ。あんなものが神剣なワケねえだろ。");

                UpdateMainMessage("ラナ：アイン、紛れもなく事実なの。今まで隠していて本当にごめんね。");

                UpdateMainMessage("アイン：いや、いやいや待てよ。何でラナがそんなもの持ってたんだよ？");

                UpdateMainMessage("アイン：いや、いやいや、いやいやいや。それが何だってんだよ？");

                UpdateMainMessage("ラナ：アイン、っちょ落ち着いてよ。今からちゃんと話すわ。");

                UpdateMainMessage("ラナ：まず、何で私が持ってたか、だけど。");

                UpdateMainMessage("ラナ：死んだお母さんの形見なの。お母さん、昔は凄い剣術の使い手でね。");

                UpdateMainMessage("ラナ：そのお母さんが昔、ヴァスタ爺さんという人から受け継いだ剣がそれだったの。");

                UpdateMainMessage("ラナ：アインってさ。小さい頃、すっごく弱かったじゃない。");

                UpdateMainMessage("アイン：ああ、思い出したくもねえぐらい弱かったな。");

                UpdateMainMessage("ラナ：アイン、バカみたいに泣きじゃくるから、何か無いのかなって思ったの。");

                UpdateMainMessage("アイン：それで渡してくれたのがあの剣なのか？");

                UpdateMainMessage("ラナ：そうね、お母さんが言ってくれたの。");

                UpdateMainMessage("ラナの母：『ラナ、剣はね、誰かに使ってもらうのが一番良いの。』");

                UpdateMainMessage("ラナの母：『ラナ、お前は剣術は少し向かないかもしれないわね。』");

                UpdateMainMessage("ラナの母：『その時はね。持ってるだけじゃダメ。誰かに使ってもらいなさい。』");

                UpdateMainMessage("ラナ：当時の私はね、剣術を少しやってたんだけど、これがまるでダメだったわけ。");

                UpdateMainMessage("アイン：まあ、ラナはどう考えても拳武術だよな。");

                UpdateMainMessage("アイン：じゃあ、あれか？隠し事って言うのは、神剣を渡していたって事か？");

                UpdateMainMessage("ラナ：・・・うん、そういう事になるね。");

                UpdateMainMessage("アイン：い、いやいや。待てよ。別にそんなのは隠し事だったとしても問題はねえだろ。");

                UpdateMainMessage("アイン：ダンジョン４階でのお前、ひどくテンパってたじゃねえか。");

                UpdateMainMessage("アイン：しかもその後、何度も何度も謝るし、明らかに動揺してただろ。");

                UpdateMainMessage("ラナ：アイン、このダンジョンに挑む時、【神剣  フェルトゥーシュ】持ってないよね？");

                UpdateMainMessage("アイン：ああ、そういやガンツ叔父の武具屋では売り切れたって・・・");

                UpdateMainMessage("ラナ：私がアインに渡した筈の剣よ。武具屋に売っているワケが無いのよ。");

                UpdateMainMessage("アイン：・・・・・・");

                GroundOne.StopDungeonMusic();

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "ラナの表情は少し微笑んだように見えた。そして一呼吸置いて続けた。";
                    md.ShowDialog();
                }

                UpdateMainMessage("ラナ：ねえアイン。　何でだろうね？");

                UpdateMainMessage("アイン：・・・何でだよ？　何でなんだよ？教えてくれよ。ラナ。");

                UpdateMainMessage("ラナ：何でだと思う？　アイン。");

                UpdateMainMessage("アイン：・・・わかんねえ・・・全然わかんねえ。");

                UpdateMainMessage("アイン：ヴェルゼにしても、ラナにしてもそうだ。");

                UpdateMainMessage("アイン：お前らさ、何でソコで話を止めるんだ？");

                UpdateMainMessage("ラナ：・・・・・・。");

                UpdateMainMessage("ヴェルゼ：・・・・・・。");

                UpdateMainMessage("アイン：おいおい・・・何なんだよ！ックソ！！");

                UpdateMainMessage("ラナ：アイン、頑張って。怒ったまま逃げないで。");

                UpdateMainMessage("アイン：だから逃げてねえって！　俺は知らねえって言ってるだろ！？");

                UpdateMainMessage("ラナ：・・・ごめん。何か無理やり追い詰めちゃって。");

                UpdateMainMessage("アイン：・・・分かった。お前ら、結局俺に真実とやらを話すつもりが無いんだろ？");

                UpdateMainMessage("ヴェルゼ：アイン君、それは違います！");

                UpdateMainMessage("アイン：ラナ、お前もヴェルゼと一緒に隠し続けるつもりだろ？");

                UpdateMainMessage("ラナ：アイン、それは違うわ！お願い信じて！");

                UpdateMainMessage("アイン：だったら何でちゃんと答えないんだ！");

                UpdateMainMessage("ラナ：アインが自分で思い出さないと駄目なの。");

                UpdateMainMessage("アイン：俺が自分で？");

                UpdateMainMessage("ラナ：そうよ。私やヴェルゼさんから教えるんじゃなくて、アイン自身が思い出す必要があるのよ。");

                UpdateMainMessage("アイン：どういう事なんだ。本当に何も知らねえ。思い出すも何も・・・");

                UpdateMainMessage("アイン：・・・　・・・");

                UpdateMainMessage("アイン：・・・夢か。そうだ、夢だ。");

                UpdateMainMessage("アイン：そういや、ダンジョン４階から見始めた変な夢の話、してなかったな。");

                UpdateMainMessage("アイン：聞いてくれるか？ラナ、ヴェルゼ。");

                UpdateMainMessage("ラナ：もちろんよ。");

                UpdateMainMessage("ヴェルゼ：ええ、どうぞ。");

                UpdateMainMessage("アイン：どこに居るのかも全然ハッキリしないが。");

                UpdateMainMessage("アイン：何もねえところだな。家がポツンポツンとあって、");

                UpdateMainMessage("アイン：あとは、果てしなく広がる青空と草原だ。");

                UpdateMainMessage("アイン：青空には数え切れないほどの鳥が飛んでたぜ。");

                UpdateMainMessage("アイン：俺はそこで、一つ手に拾ったんだ。");

                UpdateMainMessage("アイン：確かそう・・・あれは・・・");

                UpdateMainMessage("アイン：イヤリングだ。");

                UpdateMainMessage("アイン：・・・ッグ・・・頭が・・・");

                UpdateMainMessage("ラナ：アイン・・・脂汗が凄いわ・・・無理しないで・・・");

                UpdateMainMessage("アイン：それと・・・何か聞いたことのある・・・詩が・・・");

                UpdateMainMessage("アイン：ウッ・・・グアアアァァアアァァアァァァ！！！");

                UpdateMainMessage("アイン：アアアアアァァァ！！！");

                UpdateMainMessage("ラナ：アイン！駄目！しっかりして！！");

                UpdateMainMessage("ヴェルゼ：アイン君！しっかり！大丈夫ですか！？");

                UpdateMainMessage("ラナ：アイン！！　駄目！！　しっかり！！！　アイン！！！");

                UpdateMainMessage("ラナ：アイン！！！！");

                UpdateMainMessage("");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "アインが気を失ってから、５時間が過ぎた";
                    md.ShowDialog();
                }

                GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "ハンナの宿屋にて・・・";
                    md.ShowDialog();
                }

                UpdateMainMessage("ハンナ：アインは休ませておいたよ。ゆっくり寝かせるといい。");

                UpdateMainMessage("ラナ：・・・アイン・・・");

                UpdateMainMessage("ハンナ：アインはどこまで喋ったんだい？");

                UpdateMainMessage("ラナ：夢の話は最初の一節だけ。");

                UpdateMainMessage("ラナ：でも途中から、ひどく苦しみ始めて・・・");

                UpdateMainMessage("ガンツ：・・・やはり、無理じゃろう。");

                UpdateMainMessage("ハンナ：自分から夢の話を始めたんだね？");

                UpdateMainMessage("ラナ：ええ、幾つか紐解きのキッカケになるような話題で誘導はしたけど。");

                UpdateMainMessage("ラナ：一呼吸したあと、確かに自分から喋り始めていたわ。");

                UpdateMainMessage("ガンツ：それでも、急激な苦痛と失神。");

                UpdateMainMessage("ガンツ：おそらく、次起きた時はもう喋らんだろう。");

                UpdateMainMessage("ラナ：・・・私じゃやっぱり駄目ね・・・");

                UpdateMainMessage("ハンナ：およし。ラナちゃんのせいじゃないからね。");

                UpdateMainMessage("ラナ：でも・・・でも叔母さん、結局は私から");

                UpdateMainMessage("ハンナ：大丈夫、大丈夫だよ。");

                UpdateMainMessage("ハンナ：ラナちゃんは、よくやったよ。大したものさ。");

                UpdateMainMessage("ラナ：ううん・・・ごめんなさい・・・");

                UpdateMainMessage("ガンツ：ラナよ、アインはヴェルゼとは話をするのか？");

                UpdateMainMessage("ラナ：ヴェルゼさんとアインですか？ええ、話していますよ");

                UpdateMainMessage("ガンツ：ヴェルゼは・・・何と？");

                UpdateMainMessage("ラナ：アインが猛特訓を始めた時の話とか。");

                UpdateMainMessage("ラナ：それから、【秤の三面鏡】の話も。");

                UpdateMainMessage("ラナ：それから・・・");

                UpdateMainMessage("ガンツ：ふむ。");

                UpdateMainMessage("ラナ：「真実です」・・・とも言ってたわ。");

                UpdateMainMessage("ガンツ：なんと！　それは本当かね？");

                UpdateMainMessage("ラナ：ええ、確かに。でもそのあと、アイン物凄く動揺しちゃって。");

                UpdateMainMessage("ガンツ：・・・・・・あの・・・・・・ヴェルゼが・・・");

                UpdateMainMessage("ハンナ：アインも少しずつ夢を語り始めたんだ。あと少しのはずだよ。");

                UpdateMainMessage("ガンツ：・・・・・・うむ・・・・・・いや・・・・・・");
                
                UpdateMainMessage("ガンツ：・・・・・・・・・ふむ・・・・・・・・・確かに進んでる。");
                
                UpdateMainMessage("ガンツ：・・・だが・・・・・・ゆえに危険すぎる。");

                UpdateMainMessage("ハンナ：アンタもおよし。もうここまで来ているんだ。進めるしかないんだよ。");

                UpdateMainMessage("ハンナ：ラナちゃん、最下層へ行ってきな。");

                UpdateMainMessage("ラナ：ええ・・・でも、アインちゃんと起きてくれるかしら・・・");

                UpdateMainMessage("ハンナ：大丈夫だよ、寝息はしっかり整っている。明日になればちゃんと起きるよ。");

                UpdateMainMessage("ラナ：私、最下層でアインと何を話せばいいのかしら。");

                UpdateMainMessage("ハンナ：アッハハハハ、何言ってんだい。いつも通りバシーンっとやってやんな。");

                UpdateMainMessage("ラナ：ッフフ、そうですね。分かりました。");

                UpdateMainMessage("ガンツ：ラナ君。");

                UpdateMainMessage("ラナ：はい。");

                UpdateMainMessage("ガンツ：アインの夢の内容はどんなものであった？");

                UpdateMainMessage("ラナ：見つけたそうです・・・イヤリングを。");

                UpdateMainMessage("ガンツ：そうか、ついにそこまで。");

                UpdateMainMessage("ガンツ：・・・・・・ふむ、十分だ。　次にまた会おう。");

                UpdateMainMessage("ガンツ：我々は君達が最下層で【真実の看板】を見る頃から居なくなる。分かっているね？");

                UpdateMainMessage("ラナ：はい。");

                UpdateMainMessage("ガンツ：アインには精進しなさい、と。");

                UpdateMainMessage("ラナ：はい。");

                UpdateMainMessage("ガンツ：ラナ、お前には本当に辛い想いばかりを。すまない。");

                UpdateMainMessage("ラナ：そんな事ありません。私も何とかしたい一心でココに居るんです。");

                UpdateMainMessage("ガンツ：ふむ、十分休んでそれから最下層へ。その時、ワシらへの挨拶は不要だ。");

                UpdateMainMessage("ラナ：はい、本当にありがとうございます。");

                UpdateMainMessage("ハンナ：ラナちゃん、さあ休むんだ。最下層へ向けて。");

                UpdateMainMessage("ラナ：ええ、ありがとうございます。それではおやすみなさい。");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "ラナは自分の部屋へと戻っていった";
                    md.ShowDialog();
                }

                UpdateMainMessage("ガンツ：さて、ワシは武具屋へ戻るとするか。");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "その時、ガンツの手がほんの少し透明になり揺れ始めていた。";
                    md.ShowDialog();
                }

                UpdateMainMessage("ハンナ：アンタ、次こそは叶う気がする。そんな気がしないかい？");

                UpdateMainMessage("ガンツ：お前のように楽観主義者ではない。");

                UpdateMainMessage("ハンナ：アッハハハ、あんたも本当に頑固だね。");

                UpdateMainMessage("ガンツ：武具屋でいつも通りの生活をするまでよ。そして・・・");

                GroundOne.StopDungeonMusic();

                UpdateMainMessage("　　　　『ガンツ：この世界で』");
                
                UpdateMainMessage("　　　　『ガンツ：約束された終わりを静かに迎えよう。』");
                
                UpdateMainMessage("　　　　『ガンツ：頼んだぞ、アイン、ラナ。』");


                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "アインの部屋にて・・・";
                    md.ShowDialog();
                }

                // [警告]：真実世界の描写は何度も何度も何度も何度も何度も推敲してください。
                UpdateMainMessage("　　　　『アインは呼吸が整った状態で夢を見ている』");

                UpdateMainMessage("　（（　・・・　（青く照らし）　・・・　（地は新緑を）　・・・　・・・））");

                UpdateMainMessage("　（（　・・・　（偉大なる海）　・・・　・・・　（天へと還り）　・・・　））");

                UpdateMainMessage("　（（　・・・　良い声だ　・・・　・・・　ラナお前か？　・・・　））");

                UpdateMainMessage("　（（　・・・　・・・　歌いきったら　・・・　連れてってよね　・・・））");

                UpdateMainMessage("　（（　・・・　ダンジョンか？　・・・　・・・　そうだなあ　・・・　））");

                UpdateMainMessage("　（（　・・・　・・・　・・・　いいぜ　・・・　・・・））");

                UpdateMainMessage("　（（　・・・　・・・　フフフ　・・・　・・・　ありがと、アイン　・・・　））");

                UpdateMainMessage("　（（　・・・　（偉大なる母）　・・・　・・・　（厳格なる父）　・・・））");

                UpdateMainMessage("　（（　・・・　（永久）・・・　・・・　（完全調和への導き）　・・・　・・・））");

                UpdateMainMessage("　（（　・・・　（終わらない所へ）　・・・　・・・　（終わりと始まり）　・・・　・・・））");

                UpdateMainMessage("　（（　・・・　・・・　ずっとさ　・・・　・・・　これ　・・・））");

                UpdateMainMessage("　（（　・・・　（あなた）　・・・　・・・　（わたしが居た場所へ）　・・・　・・・））");

                UpdateMainMessage("　（（　・・・　・・・　＜＜＜　続くと　いいな　＞＞＞　・・・　・・・　））");

                UpdateMainMessage("アイン：・・・・・・");

                UpdateMainMessage("アイン：・・・");

                UpdateMainMessage("アイン：・・夢か。");

                UpdateMainMessage("アイン：・・・いや・・・おそらく・・・");

                UpdateMainMessage("アイン：・・・次で最下層か。");

                UpdateMainMessage("アイン：俺が思い出したくないのは・・・");

                UpdateMainMessage("アイン：っつ！痛ぅ・・・テテテ・・・駄目だ。思い出せねえ。");

                UpdateMainMessage("アイン：ハァ・・・ハァ・・・うう・・・ああ、ちくしょう。");

                UpdateMainMessage("アイン：ラナのやつ、何だってあんな練習用の剣の話なんかを。");

                UpdateMainMessage("アイン：ヴェルゼにしても随分と厄介な質問ばかりしやがって。");

                UpdateMainMessage("アイン：・・・この頭の激痛は・・・やっぱり、俺が逃げてるのか。");

                UpdateMainMessage("アイン：っくそ、しかし痛すぎて思い出そうにも思い出せねえ。");

                UpdateMainMessage("アイン：最下層で何かが分かるのか？");

                UpdateMainMessage("アイン：・・・痛ぅ・・・ッグ・・・駄目だ。寝るしかねえな。");

                UpdateMainMessage("アイン：・・・・・・");

                UpdateMainMessage("アイン：・・・俺は・・・");

                UpdateMainMessage("アイン：・・・・・・ラナ・・・");

                UpdateMainMessage("アイン：・・・・・・");

                UpdateMainMessage("アイン：・・・");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "その後、アインは深い眠りについた。";
                    md.ShowDialog();
                }

                GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
                this.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "hometown.jpg");
                this.we.CommunicationCompArea4 = true;
                CallRestInn();
            }
            #endregion
            #region "５階制覇、前編終了"
            else if (WE.CompleteSlayBoss5 && WE.CompleteArea5 && WE.TruthEventForLana)
            {
                UpdateMainMessage("・・・　・・・　・・・　・・・　・・・");

                button4.Visible = false;

                UpdateMainMessage("・・・　・・・　・・・　・・・");

                button1.Visible = false;

                UpdateMainMessage("・・・　・・・　・・・");

                button2.Visible = false;

                UpdateMainMessage("・・・　・・・");

                dayLabel.Visible = false;

                UpdateMainMessage("・・・");

                this.BackColor = Color.Black;
                this.BackgroundImage = null;
                this.Update();

                UpdateMainMessage("プロデューサ：　湯淺　與範");

                UpdateMainMessage("シナリオ：　辻谷　友紀");

                UpdateMainMessage("作曲：　湯淺　晋太郎");

                UpdateMainMessage("編曲：　湯淺　晋太郎");

                UpdateMainMessage("サウンド：　湯淺　與範");

                UpdateMainMessage("バトルシステム：　湯淺　與範");

                UpdateMainMessage("キャラクター設定：　辻谷　友紀 / 湯淺　與範");

                UpdateMainMessage("ダンジョンデザイン：　湯淺　與範");

                UpdateMainMessage("アイテム製作：　石高　裕介");

                UpdateMainMessage("プログラム：　湯淺　與範");

                UpdateMainMessage("エグゼグティブ・プロデューサ：　湯淺　與範");

                button3.Visible = false;

                UpdateMainMessage("         ～～～　Ｄｕｎｇｅｏｎ　Ｓｅｅｋｅｒ　前編　（完） ～～～　　");

                mainMessage.Visible = false;
                this.Update();

                System.Threading.Thread.Sleep(3000);

                mainMessage.Visible = true;
                this.Update();

                Application.DoEvents();

                UpdateMainMessage("アイン：　・・・　ッグ　・・・");

                UpdateMainMessage("アイン：そうだ、確かラナは死んだんだ。");

                UpdateMainMessage("アイン：神剣フェルトゥーシュがラナに刺さって・・・");

                UpdateMainMessage("アイン：俺が・・・弱かったからだ。");

                UpdateMainMessage("アイン：・・・強くなってやる。");

                UpdateMainMessage("アイン：今度こそ・・・俺は・・・誓う");

                using (YesNoRequest yesno = new YesNoRequest())
                {
                    yesno.StartPosition = FormStartPosition.CenterParent;
                    yesno.ShowDialog();
                    if (yesno.DialogResult == DialogResult.Yes)
                    {
                        UpdateMainMessage("アイン：俺は願いを叶える。");

                        UpdateMainMessage("アイン：ラナを死なせはしねえ。");

                        UpdateMainMessage("アイン：必ず食い止めて見せる。");

                        UpdateMainMessage("　　　＜＜＜　アインは心に強く誓った。ラナの死を止めると。　＞＞＞");

                        this.BackColor = Color.White;
                        this.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "hometown.jpg");

                        GroundOne.PlayDungeonMusic(Database.BGM04, Database.BGM04LoopBegin);

                        UpdateMainMessage("　　　＜＜＜　Ｄｕｎｇｅｏｎ　Ｓｅｅｋｅｒ（後編予告） ＞＞＞");
                        
                        UpdateMainMessage("      ＜＜＜　アインとラナに起きた出来事が次々と明らかになる。　＞＞＞");

                        UpdateMainMessage("　　　『アイン：何だって？お前今なんつった？』");

                        UpdateMainMessage("　　　『ラナ：だからさ、アイン、いつヴェルゼさんと知り合ったのよ？』");

                        UpdateMainMessage("　　　『アイン：ダンジョン３階のはずだ・・・いや、もっと前って事か？』");

                        UpdateMainMessage("　　　『ラナ：始めて会った時の事、思い出してよ。ちゃんと。』");

                        UpdateMainMessage("　　　『アイン：手紙を確かに受けとったんだよ、こんな感じのを』");
                        
                        UpdateMainMessage("      『　【ダンジョンゲート入り口の裏で待つ。　～ Ｖ・Ａ ～　】　　");

                        UpdateMainMessage("      『ラナ：それってアインが宿屋で拾った手紙だっけ？？");

                        UpdateMainMessage("　　　『アイン：いや・・違う。確かこの手紙は");

                        UpdateMainMessage("　　　＜＜＜　真実を受諾し始めたアインは、激痛に耐えながら次々と思い出す　＞＞＞");

                        UpdateMainMessage("　　　『アイン：そうだ、そもそも俺は神剣フェルトゥーシュを持ってたはずだ。』");

                        UpdateMainMessage("　　　『アイン：いつかどこかで、無くしちまってる・・・』");

                        UpdateMainMessage("　　　『ヴェルゼ：アイン君、よく思い出してください。重要な手がかりのはずです。』");

                        UpdateMainMessage("　　　『アイン：・・・いや、違う。どこかで・・・』");

                        UpdateMainMessage("　　　『アイン：・・・盗まれたんだ。』");

                        UpdateMainMessage("　　　『ヴェルゼ：それは、誰に盗まれたんですか？』");

                        UpdateMainMessage("　　　『アイン：分かってれば苦労はしねえんだが・・・寝てたわけじゃないんだ。』");

                        UpdateMainMessage("　　　『アイン：何か一つの風が通り過ぎたような・・・そんな気がした時は既に");

                        UpdateMainMessage("　　　＜＜＜　各ポイントでの伏線発言が次々と暴かれ始める！　＞＞＞");

                        UpdateMainMessage("      『アイン：全然聞いた事無いけどな。不思議と昔から知ってるような感じだったぜ。すげえ綺麗だった。");

                        UpdateMainMessage("　　　『ラナ：う～ん・・・多分私もその詩、知ってるわよ。』");

                        UpdateMainMessage("      『アイン：マジかよ！？』");

                        UpdateMainMessage("　　　『ラナ：私の母さんからよく聞かされていたわ。確かタイトルが・・・』");

                        UpdateMainMessage("　　　＜＜＜　伝説のFiveSeekerヴェルゼとアインの関係が明らかになり始める！　＞＞＞");

                        UpdateMainMessage("      『ラナ：ヴェルゼさん、またファラ様とかのお話、宿屋で聞かせてくださいね♪』");

                        UpdateMainMessage("      『ヴェルゼ：・・・ラナさん、鏡をお願いします。』");

                        UpdateMainMessage("      『ラナ：え？え、ええ・・・それじゃ』");

                        UpdateMainMessage("      『アイン：ヴェルゼ、待てよ。宿屋・・・来てくれるよな？』");

                        UpdateMainMessage("      『ヴェルゼ：・・・それは出来ませんね。』");

                        UpdateMainMessage("      『アイン：どういうことだよ？』");

                        UpdateMainMessage("      『ヴェルゼ：それはアイン君、あなたとボクの関係に起因しています。』");

                        UpdateMainMessage("      『アイン：俺とヴェルゼ？』");

                        UpdateMainMessage("      『ヴェルゼ：アイン君、どうしても知りたければ、今日の夜ダンジョンゲート裏まで来てください。』");

                        UpdateMainMessage("      『ヴェルゼ：ラナさんは来てはいけません。アイン君一人で来てください。』");

                        UpdateMainMessage("　　　＜＜＜　ラナと秤の三面鏡に関する事実が浮き彫りになる！　＞＞＞");

                        UpdateMainMessage("      『ラナ：っあ、アレの事？あれはね。』");

                        UpdateMainMessage("　　　『ヴェルゼ：秤の三面鏡ですね。』");

                        UpdateMainMessage("　　　『ラナ：そう、あの鏡に何度も触れているウチに聞こえてきてたの。』");

                        UpdateMainMessage("　　　『ラナ：誰かと誰かが喋っているのよ。私はそれを横から見てるの。』");

                        UpdateMainMessage("　　　『アイン：誰かと誰かって誰だよ？』");

                        UpdateMainMessage("　　　『ラナ：う～ん、ちょっと待ってよね。』");

                        UpdateMainMessage("　　　『ラナ：あ、思い出したわ・・・けど、これって・・・？？？』");

                        UpdateMainMessage("　　　＜＜＜　伝説のFiveSeekerであり、師匠でもあるオル・ランディスが参戦する！　＞＞＞");

                        UpdateMainMessage("      『アイン：っけ・・・こんなん勝てるわけねえだろ。どうすりゃ・・・』");

                        UpdateMainMessage("      『ランディス：オラオラ、弱すぎんだよ、ザコアイン！オラオラアアァァア！！！！』");

                        UpdateMainMessage("      『ラナ：アインまたDuelで負けてるわね・・・こりゃ勝てそうにないわ。ッフフ、ガンバレ♪』");

                        UpdateMainMessage("      『ランディス：魂がねえ！オーラがねえ！覇気がねえ！弱すぎんだよテメェ！！』");

                        UpdateMainMessage("      『アイン：っせぇ！真面目にやってるだろうが！！』");

                        UpdateMainMessage("      『　ガキィィィ！！！（剣が交錯する！）』");

                        UpdateMainMessage("      『ランディス：真面目ってか！ッハッハッハッハァ！そんなんで勝てると思ってんのかぁ！オラアァァ！！！』");

                        UpdateMainMessage("      『アイン：ゲホ・・・！ッグ、っくそが・・・！てめえそのグローブ、外せよ。卑怯だぞ。』");

                        UpdateMainMessage("      『ランディス：武器のせいかよ！だからテメェは弱いってんだ！！もっかい昇天してこいよオラァ！！！』");

                        UpdateMainMessage("      『ラナ：良いぞ、バカアイン。　ガンバレ♪　ガンバレ♪』");

                        UpdateMainMessage("      『アイン：ああ・・・あああぁぁぁ！　もういっちょ！！！』");

                        UpdateMainMessage("　　　＜＜＜　ダンジョンの構成に変化と拡張が起こる！　＞＞＞");

                        UpdateMainMessage("　　　＜＜＜　新戦闘システム『スタック・イン・ザ・コマンド』を導入　！！　＞＞＞");

                        UpdateMainMessage("　　　＜＜＜　幻想世界と真実世界のミッシングリンク関係が明らかに！　＞＞＞");

                        UpdateMainMessage("　　　＜＜＜　Ｄｕｎｇｅｏｎ　Ｓｅｅｋｅｒ（後編）　へ続く＞＞＞");

                        we.EnterSecondGame = true;

                        using (ESCMenu esc = new ESCMenu())
                        {
                            esc.MC = this.MC;
                            esc.SC = this.SC;
                            esc.TC = this.TC;
                            esc.WE = this.WE;
                            esc.KnownTileInfo = this.knownTileInfo;
                            esc.KnownTileInfo2 = this.knownTileInfo2;
                            esc.KnownTileInfo3 = this.knownTileInfo3;
                            esc.KnownTileInfo4 = this.knownTileInfo4;
                            esc.KnownTileInfo5 = this.knownTileInfo5;
                            esc.StartPosition = FormStartPosition.CenterParent;
                            esc.OnlySave = true;
                            esc.ShowDialog();
                        }
                        //button1.Visible = true;
                        //button2.Visible = true;
                        //button3.Visible = true;
                        //button4.Visible = true;
                        //dayLabel.Visible = true;
                        Application.Exit();

                    }
                    else
                    {
                        Application.Exit();
                    }
                }

            }
            #endregion


        }

        /// <summary>
        /// if-else文とフラグを複雑化させてハンナ叔母ちゃんとの会話を盛り上げてください。
        /// </summary>
        private void button1_Click(object sender, EventArgs e)
        {
            #region "１日目"
            if (this.firstDay >= 1 && !we.CommunicationHanna1 && mc.Level >= 1 && knownTileInfo[2])
            {
                if (!we.AlreadyRest)
                {
                    mainMessage.Text = "アイン：ふう、疲れた・・・ハンナおばちゃん、今日は空いてる？";
                    ok.ShowDialog();
                    mainMessage.Text = "ハンナ：ああ、空いてるよ。泊まっていきなさい。";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：サンキュー！じゃあ上がらせてもらうよ。";
                    ok.ShowDialog();
                    mainMessage.Text = "ハンナ：そうそう、聞いたよ。アンタこれからダンジョン探索やるんだって？";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：最深部まで絶対に辿りついてやるぜ！ッハッハッハ！";
                    ok.ShowDialog();
                    mainMessage.Text = "ハンナ：若いってのは良いねぇ。今の所、最深部到達者は数えるぐらいね。";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：俺も挑戦する。そしてその最深部到達者の中に含めてもらうさ。";
                    ok.ShowDialog();
                    mainMessage.Text = "ハンナ：ダンジョンを上手く進めるためには、まず自分の体調を整えること。";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：了解。今日は思いっきり休むとするさ。";
                    ok.ShowDialog();
                    mainMessage.Text = "ハンナ：ああ、ゆっくりと休んでいきなさい。";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：サンキュー。";
                    ok.ShowDialog();
                    CallRestInn();
                }
                else
                {
                    mainMessage.Text = "ハンナ：もう朝だよ。さあ、始めが肝心だからね。いってらっしゃい。";
                }
            }
            #endregion
            #region "２日目"
            else if (this.firstDay == 2)
            {
                if (!we.AlreadyRest)
                {
                    mainMessage.Text = "アイン：おばちゃん。空いてる？";
                    ok.ShowDialog();
                    mainMessage.Text = "ハンナ：空いてるよ。泊まってくかい？";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：どうすっかな・・・もう今日は泊まるか？";
                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                    {
                        yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                        yesno.ShowDialog();
                        if (yesno.DialogResult == DialogResult.Yes)
                        {
                            mainMessage.Text = "ハンナ：はいよ、一名様ご案内だね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：サンキュー、おばちゃん。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：おばちゃん、そういや最深部到達者ってどのぐらい居るんだい？";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：５人だね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ッゲ、５人も居るのかよ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：そう感じるかい？でも驚いちゃいけないよ。今までの挑戦した者の数は";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：どのぐらい居るんだ？";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：私が知る限りでも、１００万人程度だね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：マジかよ・・・ほとんど全滅じゃねえか。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：５人については知ってるかい？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：いいや、知らねえな。スゲェのか？";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：ああ、今度また聞かせてあげるよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：おう、楽しみにしてるぜ！";
                            ok.ShowDialog();
                            CallRestInn();
                        }
                        else
                        {
                            mainMessage.Text = "アイン：ごめん。まだ用があるんだ、後でくるよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：いつでも寄ってらっしゃい。部屋は空けておくからね。";
                        }
                    }
                }
                else
                {
                    mainMessage.Text = "ハンナ：もう朝だよ。さあ、始めが肝心だからね。いってらっしゃい。";
                }
            }
            #endregion
            #region "３日目"
            else if (this.firstDay == 3)
            {
                if (!we.AlreadyRest)
                {
                    mainMessage.Text = "アイン：おばちゃん。空いてる？";
                    ok.ShowDialog();
                    mainMessage.Text = "ハンナ：空いてるよ。泊まってくかい？";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：どうすっかな・・・もう今日は泊まるか？";
                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                    {
                        yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                        yesno.ShowDialog();
                        if (yesno.DialogResult == DialogResult.Yes)
                        {
                            mainMessage.Text = "ハンナ：はいよ、一名様ご案内だね。";
                            ok.ShowDialog();
                            // [警告]：２日目会話してるかどうかで分岐させてください。
                            mainMessage.Text = "アイン：サンキュー、おばちゃん。到達者５人についてさっそく教えてくれよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：ここら辺では、彼らに敬意を表して、【-- FiveSeeker --】と呼ばれているよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：まず一人目は【エルミ・ジョルジュ】";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：っな・・・マジかよ！！・・・マジかよ！！！";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：アッハハハ、驚いたかい？何せ、我々の国を治めてる国王様だからね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ショック隠す方が無理だな。いやあ、しかし誇りに思えるな！";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：ジョルジュ様は常に全能力が上がる【ファージル王家の刻印】を装備していたらしいね。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：「秩序・フェア・対等」を重んじて戦闘する様は誰が見ても惚れ惚れしたそうだよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：秩序とフェア・・・か。俺には出来ない芸当かもな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：何言ってんだい。アンタだって素質はあるよ。ほら明日からも頑張りな！";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：おう、おばちゃんに励まされると何となく元気になるぜ。サンキュー";
                            ok.ShowDialog();
                            CallRestInn();
                        }
                        else
                        {
                            mainMessage.Text = "アイン：ごめん。まだ用があるんだ、後でくるよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：いつでも寄ってらっしゃい。部屋は空けておくからね。";
                        }
                    }
                }
                else
                {
                    mainMessage.Text = "ハンナ：もう朝だよ。今日も頑張ってらっしゃい。";
                }
            }
            #endregion
            #region "４日目"
            else if (this.firstDay == 4)
            {
                if (!we.AlreadyRest)
                {
                    mainMessage.Text = "アイン：おばちゃん。空いてる？";
                    ok.ShowDialog();
                    mainMessage.Text = "ハンナ：空いてるよ。泊まってくかい？";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：どうすっかな・・・もう今日は泊まるか？";
                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                    {
                        yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                        yesno.ShowDialog();
                        if (yesno.DialogResult == DialogResult.Yes)
                        {
                            mainMessage.Text = "ハンナ：はいよ、一名様ご案内だね。";
                            ok.ShowDialog();
                            // [警告]：２日目、３日目会話してるかどうかで分岐させてください。
                            mainMessage.Text = "アイン：サンキュー、おばちゃん。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：まさか５人中の１人が国王とはな。じゃあ到達者５人の２人目は誰なんだ？";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：やだよ、またビックリさせちゃ悪いんだけどね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：おいおい、まさか・・・";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：二人目は【ファラ・フローレ】　現国王の良き妻ね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ショックを通り越した気分だよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：ファラ様はそれはもう天使の様な方でねぇ・・・女性のアタシらでもうっとりするぐらいだったよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：純白半透明である【天使のペンダント】をいつも胸に着けていたね。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：あれは【神の七遺産】と呼ばれている代物らしいね。通常の人では効果が発揮しないそうだ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：通常って・・・どうやったら効果が発揮してたんだ？";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：　＜＜＜純粋な祈りを常に心に秘めた者にのみ許される、神の慈悲＞＞＞";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：どうやら腕力じゃなさそうだな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：そもそもファラ様は女性、アンタは男性、根本的に違うって事だね。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：さあ、今日はここまでだよ。明日も頑張ってきな。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：到達者ってありえねえなマジ・・・今日は休ませてもらうよ。サンキュー。";
                            ok.ShowDialog();
                            CallRestInn();
                        }
                        else
                        {
                            mainMessage.Text = "アイン：ごめん。まだ用があるんだ、後でくるよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：いつでも寄ってらっしゃい。部屋は空けておくからね。";
                        }
                    }
                }
                else
                {
                    mainMessage.Text = "ハンナ：もう朝だよ。今日も頑張ってらっしゃい。";
                }
            }  
            #endregion
            #region "５日目"
            else if (this.firstDay == 5)
            {
                if (!we.AlreadyRest)
                {
                    mainMessage.Text = "アイン：おばちゃん。空いてる？";
                    ok.ShowDialog();
                    mainMessage.Text = "ハンナ：空いてるよ。泊まってくかい？";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：どうすっかな・・・もう今日は泊まるか？";
                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                    {
                        yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                        yesno.ShowDialog();
                        if (yesno.DialogResult == DialogResult.Yes)
                        {
                            mainMessage.Text = "ハンナ：はいよ、一名様ご案内だね。";
                            ok.ShowDialog();
                            // [警告]：２日目、３日目、４日目会話してるかどうかで分岐させてください。
                            mainMessage.Text = "アイン：サンキュー、おばちゃん。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：なあ３人目についてだが、ちょっと待ってくれ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：おやおや、怖気づいたかい？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：そうじゃねえ、ただ驚かされっぱなしだからな。よし、教えてくれ。３人目は？";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：三人目は【シニキア・カールハンツ】";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：魔道最高クラス、伝説のカール爵じゃねえか。そんな事だろうと思ったぜ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：カール爵は魔道特性を司る光・闇・火・水・理・空をマスター。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：眼球装着式【魔道デビルアイ】で長点・弱点・気質・力量・采配・才覚・天運を見通す。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：類まれなる「洞察・知恵・判断」で百発百中。睨まれた者はひれ伏してたもんだわ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：俺も噂だけは聞いたことある。人間の領域を超えてるぜ全く。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：今は隠れ家で研究だけど、国王ジョルジュ様の良きライバルだったって所だね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ジョルジュ様とカール爵、どっちが強かったんだい？";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：常に互角だったと、されてるねぇ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：国王ジョルジュ様は正々堂々としすぎてる。『弱点は見抜かれてこそ弱点』とか何とかで。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：一方、カール爵は洞察しすぎで『弱点だらけ。底が知れぬ』とか何とかで。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：お互い競り合うけどいつも決着付かず仕舞いってわけさ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：よくわかんねえな。言ってる意味も不明だな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：アイン、あんたにも分かる日がきっと来るよ。さあ、今日はもう休むんだね。";
                            ok.ShowDialog();
                            CallRestInn();
                        }
                        else
                        {
                            mainMessage.Text = "アイン：ごめん。まだ用があるんだ、後でくるよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：いつでも寄ってらっしゃい。部屋は空けておくからね。";
                        }
                    }
                }
                else
                {
                    mainMessage.Text = "ハンナ：もう朝だよ。今日も頑張ってらっしゃい。";
                }
            }
            #endregion
            #region "６日目"
            else if (this.firstDay == 6)
            {
                if (!we.AlreadyRest)
                {
                    mainMessage.Text = "アイン：おばちゃん。空いてる？";
                    ok.ShowDialog();
                    mainMessage.Text = "ハンナ：空いてるよ。泊まってくかい？";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：どうすっかな・・・もう今日は泊まるか？";
                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                    {
                        yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                        yesno.ShowDialog();
                        if (yesno.DialogResult == DialogResult.Yes)
                        {
                            mainMessage.Text = "ハンナ：はいよ、一名様ご案内だね。";
                            ok.ShowDialog();
                            // [警告]：２日目、３日目、４日目、５日目会話してるかどうかで分岐させてください。
                            mainMessage.Text = "アイン：サンキュー、おばちゃん。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：いよいよ４人目か。もう何でもかかって来い。ッハッハッハ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：かかってきやしないよ、ええと４人目だったね。じゃいくよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ああ来い！ッハッハッハッハ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：四人目は【オル・ランディス】";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ハーーーーッハッハッハッハ！！！俺の師匠じゃねぇか！！！！";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：通称オールラウンドデス。全てを焼き殺せる者として名が通った最強人物。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：全てを強引に焼き殺す拳【炎神グローブ】。ありゃま、アインにとっては。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：史上最低のグローブだな。思い起こすだけでも・・・";
                            ok.ShowDialog();
                            mainMessage.Text = "ランディス：『いっぺん死んでこいやザコがぁぁぁぁー！！オラァァァァァァァァァ！！』";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：・・・理由なき暴挙。あれで何度灰にさせられた事やら。あれは完全に悪の化身だろ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：アッハハハ、悪かったねぇ、でもこんな言い方もなんだけど。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：あれも【神の七遺産】のうちの一つだよ。悪とはちょっと違うと思うねえ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：何だと？だとしたら通常じゃやはり単なるグローブなのか？";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：そう言う事になるねえ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：　＜＜＜無双信念を心に携えし者にのみ許される、神の聖炎＞＞＞";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：間違いなく腕力だな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：そうだねぇ、でも私の推測だけど、信念が宿ってないと駄目だろうね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：っけ、俺を殴りたいだけじゃねえか。んなもん、どこが信念なんだ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：アンタほんとに良い師匠に会ったじゃないか。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：殴られっぱなしじゃ、俺自身が許せないね。いつかアイツは俺自らの手でリベンジだ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：ホント良い信念持ってるじゃない。あぁ、いつかギッタギタに倒してやんな。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：おっしゃ、そうと決まればまずは休息だ。おばちゃんありがとう！";
                            ok.ShowDialog();
                            CallRestInn();
                        }
                        else
                        {
                            mainMessage.Text = "アイン：ごめん。まだ用があるんだ、後でくるよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：いつでも寄ってらっしゃい。部屋は空けておくからね。";
                        }
                    }
                }
                else
                {
                    mainMessage.Text = "ハンナ：もう朝だよ。今日も頑張ってらっしゃい。";
                }
            }  
            #endregion
            #region "７日目"
            else if (this.firstDay == 7)
            {
                if (!we.AlreadyRest)
                {
                    mainMessage.Text = "アイン：おばちゃん。空いてる？";
                    ok.ShowDialog();
                    mainMessage.Text = "ハンナ：空いてるよ。泊まってくかい？";
                    ok.ShowDialog();
                    mainMessage.Text = "アイン：どうすっかな・・・もう今日は泊まるか？";
                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                    {
                        yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                        yesno.ShowDialog();
                        if (yesno.DialogResult == DialogResult.Yes)
                        {
                            mainMessage.Text = "ハンナ：はいよ、一名様ご案内だね。";
                            ok.ShowDialog();
                            // [警告]：２日目、３日目、４日目、５日目会話してるかどうかで分岐させてください。
                            mainMessage.Text = "アイン：サンキュー、おばちゃん。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：これで最後の５人目って事になるな。もう有名な人物は居ないと思うが。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：そうだろうね。何せ５人目に関しては。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：なんだ、どう言う意味だ？そりゃ";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：まあ先に名前を言うからね。よく覚えておくんだよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：五人目は【ヴェルゼ・アーティ】";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：誰なんだソイツは？全然聞いた事がないぜ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：姿見たもの存在せず、姿捕らえたもの存在せず、姿感じたもの存在せず。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：【天空の翼】をまとった者の証として、神の速度、縦横無尽に天地を駆け巡る。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：どう言う事だ、本当に存在するのか？そんなヤツ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：居るらしいね。悪いけど私自身もちゃんと姿を見たわけじゃないんだよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：今も実は国王様の重臣として健在との事。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：冗談だろ！？聞いた事ねえぞ、そんなヤツ。って、またジョルジュ様の縁の人なんだな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：実際に国王様もしくは国王の秘宝を狙った者は";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：確か・・・『その日に王室の外で白眼で気絶。本人も経緯を全く覚えてない。』だったな・・・";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ところでその【天空の翼】。それも【神の七遺産】ってワケか？";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：そうだね。こう言い伝えられているよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：　＜＜＜思考・論理の驕りを捨て、自由心を保つ者にのみ許される、神の躍動＞＞＞";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：思考と論理を捨てる・・・腕力だろ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：ラナちゃんから聞いてるけど・・・アイン、あんたは本当にバカだねぇ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ッハッハッハッハ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：まあこれで５人全員だよ。どうだい？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ランディのボケは置いといてだな。アイツ以外は全員すげぇヤツらばかりだな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：アイン、さあアンタも最深層まで到達するよう頑張ってきな。応援してるよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：正直な所、俺はそんなに伝説じみた能力も装備もねえ。だがやってやるぜ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：その息だ。じゃあ今日はゆっくり休みなさい。決して無理はするんじゃないよ。";
                            ok.ShowDialog();
                            CallRestInn();
                        }
                        else
                        {
                            mainMessage.Text = "アイン：ごめん。まだ用があるんだ、後でくるよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ハンナ：いつでも寄ってらっしゃい。部屋は空けておくからね。";
                        }
                    }
                }
                else
                {
                    mainMessage.Text = "ハンナ：もう朝だよ。今日も頑張ってらっしゃい。";
                }
            }  
            #endregion
            #region "４階制覇後、５階看板到達後、ハンナのゆったり宿屋閉店"
            else if (we.CommunicationCompArea4 & we.TruthWord5 && !we.CommunicationHanna100)
            {
                we.CommunicationHanna100 = true;
                if (!we.AlreadyRest)
                {
                    UpdateMainMessage("アイン：おばちゃん、空いてる？");

                    UpdateMainMessage("アイン：あれ、何かガラーンとしてるな。おばちゃん居ないのかよ？");

                    UpdateMainMessage("ラナ：アイン、カウンター席に何か書置きがあるわよ。読むわね。");

                    UpdateMainMessage("　　　『ガンツの武具屋閉店により、ハンナのゆったり宿屋も一時閉店とする。』");

                    UpdateMainMessage("アイン：おいおい、どうなってんだよ！？宿屋が閉店って事は休めねえじゃないか！");

                    if (!we.CommunicationGanz100)
                    {
                        UpdateMainMessage("アイン：いやいや、ガンツ叔父さんの武具屋も閉店してるのかよ！？どうなってんだ！");
                    }

                    UpdateMainMessage("ラナ：待ってアイン、その下に続きが書いてあるわ。");

                    UpdateMainMessage("　　　『片付け、整理整頓、壊れ物直して、ちゃんと次の人が使えるようにしとくんだよ。【Hanna.Gimerga】");

                    UpdateMainMessage("アイン：片付けとか自分でやれば使っても良いって事か？");

                    UpdateMainMessage("ラナ：ッフフ、おばさんらしい書き方ね。");

                    UpdateMainMessage("アイン：まあ何かとおばちゃんには世話になりっぱなしだからな。自分達でそのぐらいはやるか！");

                    UpdateMainMessage("ラナ：そうね、少し手間はかかるけど、おばさんがいつもやってた事、理解するには丁度良いわ。");

                    UpdateMainMessage("アイン：ああ、そうだな。ガンツ叔父さん、ハンナおばちゃんが帰ってくるまで丁寧にしとかないとな。");

                    UpdateMainMessage("ラナ：キッチン・食堂・寝床・ゆったりルーム、いろんな所に書置きがあるわ・・・");

                    UpdateMainMessage("　　　『ホラそこ、食べ物こぼすんじゃないよ』");

                    UpdateMainMessage("　　　『ジュース散らかしっぱなしで返ったらダメだよ』");

                    UpdateMainMessage("　　　『寝る前は、ちゃんと忘れ物しないように荷物整理しときな。』");

                    UpdateMainMessage("　　　『ケンカした後は椅子とテーブルの並びぐらい直しときな。』");

                    UpdateMainMessage("アイン：こりゃ、すげえ量だな。アチコチに貼ってある・・・");

                    UpdateMainMessage("ラナ：おばさんが居ると思って使えば良いのよ。");

                    UpdateMainMessage("アイン：それもそうだな。何かおばちゃんが居るみたいな感じがするな、ッハッハッハ！");

                    UpdateMainMessage("ラナ：大事に使わせてもらいましょ。じゃ、アインおやすみなさい。");

                    UpdateMainMessage("アイン：ああ、おやすみだ。");

                    CallRestInn();
                }
                else
                {
                    mainMessage.Text = "アイン：じゃあ、行ってくるぜ。ハンナおばちゃん。";
                } 
            }
            #endregion
            #region "Ｘ日目"
            else
            {
                if (!we.CommunicationHanna100)
                {
                    if (!we.AlreadyRest)
                    {
                        mainMessage.Text = "アイン：おばちゃん。空いてる？";
                        ok.ShowDialog();
                        mainMessage.Text = "ハンナ：空いてるよ。泊まってくかい？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：どうすっかな・・・泊まるか？";
                        using (YesNoRequestMini yesno = new YesNoRequestMini())
                        {
                            yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                            yesno.ShowDialog();
                            if (yesno.DialogResult == DialogResult.Yes)
                            {
                                mainMessage.Text = "ハンナ：はいよ、部屋は空いてるよ。ゆっくりと休みな。";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：サンキュー、おばちゃん。";
                                ok.ShowDialog();
                                CallRestInn();
                            }
                            else
                            {
                                mainMessage.Text = "アイン：ごめん。まだ用があるんだ、後でくるよ。";
                                ok.ShowDialog();
                                mainMessage.Text = "ハンナ：いつでも寄ってらっしゃい。部屋は空けておくからね。";
                            }
                        }
                    }
                    else
                    {
                        mainMessage.Text = "ハンナ：もう朝だよ。今日も頑張ってらっしゃい。";
                    }
                }
                else
                {
                    if (!we.AlreadyRest)
                    {
                        UpdateMainMessage("アイン：宿屋、使わせてもらうとするか？", true);

                        using (YesNoRequestMini yesno = new YesNoRequestMini())
                        {
                            yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                            yesno.ShowDialog();
                            if (yesno.DialogResult == System.Windows.Forms.DialogResult.Yes)
                            {
                                UpdateMainMessage("ラナ：おばさんが帰ってくるまで、ちゃんと綺麗にしておきましょ。");

                                UpdateMainMessage("アイン：ああ、そうだな。じゃあおやすみだ。ラナ。");

                                UpdateMainMessage("ラナ：ええ、おやすみなさい。");
                                CallRestInn();
                            }
                            else
                            {
                                UpdateMainMessage("", true);
                            }
                        }
                    }
                    else
                    {
                        mainMessage.Text = "アイン：じゃあ、行ってくるぜ。ハンナおばちゃん。";
                    }
                }
            }
            #endregion
        }

        private void CallRestInn()
        {
            GroundOne.PlaySoundEffect("RestInn.mp3");
            using (MessageDisplay md = new MessageDisplay())
            {
                md.Message = "休息をとりました";
                md.StartPosition = FormStartPosition.CenterParent;
                md.ShowDialog();
            }

            we.AlreadyRest = true;
            // [警告]：オブジェクトの参照が全ての場合、クラスにメソッドを用意してそれをコールした方がいい。
            if (mc != null)
            {
                mc.CurrentLife = mc.MaxLife;
                mc.CurrentSkillPoint = mc.MaxSkillPoint;
                mc.CurrentMana = mc.MaxMana;
            }
            if (sc != null)
            {
                sc.CurrentLife = sc.MaxLife;
                sc.CurrentSkillPoint = sc.MaxSkillPoint;
                sc.CurrentMana = sc.MaxMana;
            }
            if (tc != null)
            {
                tc.CurrentLife = tc.MaxLife;
                tc.CurrentSkillPoint = tc.MaxSkillPoint;
                tc.CurrentMana = tc.MaxMana;
            }
            we.AlreadyUseSyperSaintWater = false;
            we.AlreadyUseRevivePotion = false;
            we.AlreadyUsePureWater = false; // 後編追加
            this.we.GameDay += 1;
            dayLabel.Text = we.GameDay.ToString() + "日目";
        }

        /// <summary>
        /// ダンジョンから出たばかりの場合、一休憩、休んだ後はダンジョンへ移動します。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //bool result = EncountBattle("ヴェルゼ・アーティ");
            //return;

            if (!we.AlreadyRest)
            {
                mainMessage.Text = "アイン：今出てきたばかりだぜ？一休憩させてくれ。";
            }
            // [警告]：ラナとの会話が強制なのか任意なのかを決定してください。
            //else if (!we.AlreadyCommunicate)
            //{
            //    mainMessage.Text = "アイン：ラナのやつ、何してるかな。";
            //}
            else
            {
                // レベルアップしている場合は新能力付与の会話を盛り上げてください。
                #region "アインのレベルアップ"
                if (mc != null)
                {
                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.Manual;
                        md.Location = new Point(this.Location.X, this.Location.Y + this.Size.Height);
                        md.NeedAnimation = true;

                        if (mc.Level >= 3 && !mc.StraightSmash)
                        {
                            mainMessage.Text = "アイン：ん？何だこの感触は。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：どうしたのよ？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：いや・・・ちょいと、やってみるか。離れててくれ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：オラァ！ストレート・スマッシュ！！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：へぇ、やるじゃない♪";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：おぉ、一旦構えてから突撃する感じだぜ。なかなかイケてるだろ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：アイン、それにはスキルポイントと言うものが必要になってくるのよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：なんだその面倒くさそうなのは。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：簡単よ。各キャラには１００のスキルポイントがあらかじめ与えられているわ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ちなみにストレート・スマッシュはいくつ消費するんだ？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：１５ね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：だとしたら６回しかできねえな。あとの１０はムダだな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：戦闘中は１ターン毎に１回復するとしたら？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：なるほど、すると多少時間が経てば何度も打てるってことか。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：肝心の攻撃力は・・・すばやく踏み込める方が威力が大きそうだな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：まあ、直接攻撃でも技値は上げておくことね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：オーケー、次から使っていくとするか！";
                            ok.ShowDialog();
                            md.Message = "＜アインはストレート・スマッシュを習得しました＞";
                            md.ShowDialog();
                            using (MessageDisplay md2 = new MessageDisplay())
                            {
                                md2.StartPosition = FormStartPosition.Manual;
                                md2.Location = new Point(this.Location.X, this.Location.Y + this.Size.Height);
                                md2.NeedAnimation = true;
                                md2.Message = "＜アインのスキル使用が解放されました＞";
                                md2.ShowDialog();
                            }
                            mc.AvailableSkill = true;
                            mc.StraightSmash = true;
                        }
                        if (mc.Level >= 4 && !mc.FreshHeal)
                        {
                            mainMessage.Text = "アイン：本日から、俺はパラディンだ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：え？っちょ何言って・・・";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ライフ回復の呪文。フレッシュ・ヒール！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：・・・ップ、フフッ、フフフフフ、あぁお腹痛い、止めてよねホント、アハハハハ";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：何がおかしい？パラディンならライフ回復ぐらいできて当然だろ！画期的な発想だなこれは。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：いや、別にその辺のバカは良いけどね。問題はね、アイン。あなた知力はいくつなの？";
                            ok.ShowDialog();
                            if (this.mc.Intelligence < 6)
                            {
                                mainMessage.Text = "アイン：" + this.mc.Intelligence.ToString() + "だな・・・って事はまさかぁ！！";
                                ok.ShowDialog();
                                int effectValue = this.mc.Intelligence * 4 + 20;
                                mainMessage.Text = "ラナ：そうよ。その程度の知力じゃ" + effectValue.ToString() + "程度しか回復しないわよ。ホントバカなんだから、フフフ";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：次からのレベルアップ時に考えるようにするさ。出来ないよりマシだろ。";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：まあそうね。確かにそれも一理あるわ。でも凄いじゃない、いきなり回復呪文なんて。";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：出来て当然だ。元々の俺はだな。";
                                ok.ShowDialog();
                            }
                            else
                            {
                                mainMessage.Text = "アイン：ッフ・・・ラナ、今の俺の知力は" + this.mc.Intelligence.ToString() + "だ。";
                                ok.ShowDialog();
                                int effectValue = this.mc.Intelligence * 4 + 20;
                                mainMessage.Text = "ラナ：え、ウソ！？じゃあ" + effectValue.ToString() + "ぐらいは回復出来るわけね、結構頭使ってるじゃない。";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：次からのレベルアップ時から更にバランスよく鍛えるつもりさ。";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：ホントよくやってるじゃない。たいしたものね。回復呪文の事まで考えていたなんて。";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：そりゃ考えていて当然さ。元々の俺はだな。";
                                ok.ShowDialog();
                            }
                            mainMessage.Text = "ラナ：あ、ああそうだったわね。失礼したわね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ところで、これって魔法系だよな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：当たり前じゃない。スキルじゃないからね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：どうやって唱えるんだ？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：え、ひょっとしてだけど、唱え方自体分からないの？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ッハッハッハ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：どうやってその呪文やろうとしてたのよ。あんた、バカじゃない？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ッハッハッハッハ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：マナポイントというのがあるわ。それを幾つか消費して発動するの、今回は消費量２０って所ね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ッハッハッハッハッハ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：こりゃ駄目だわ・・・ハアァァ～～～～・・・";
                            ok.ShowDialog();
                            md.Message = "＜アインはフレッシュ・ヒールを習得しました＞";
                            md.ShowDialog();
                            using (MessageDisplay md2 = new MessageDisplay())
                            {
                                md2.StartPosition = FormStartPosition.Manual;
                                md2.Location = new Point(this.Location.X, this.Location.Y + this.Size.Height);
                                md2.NeedAnimation = true;
                                md2.Message = "＜アインの魔法使用が解放されました＞";
                                md2.ShowDialog();
                            }
                            mc.FreshHeal = true;
                            mc.AvailableMana = true;
                        }
                        if (mc.Level >= 5 && !mc.FireBall)
                        {
                            mainMessage.Text = "アイン：ＬＶ５、来たぜ来たぜ！良い目覚めの瞬間だ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：ＬＶ５じゃせいぜいFireballって所でしょ？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：この炎の玉を食らいやがれ！FireBall！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：炎の玉はアインのバカ知力でもそこそこのダメージが期待できるわね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：見てみろよ。あんなデカイ木も丸焦げにしてやったぜ・・・";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：デカイ木ぐらいでも、知力を上げていれば、その分だけ";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：その先飛ばし解説は止めてくれよな・・・";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：ッフフ、冗談よ♪　知力を上げれば威力も少しずつ上がっていくわ、覚えておくことね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ああ、魔法攻撃も捨てたもんじゃないからな。";
                            ok.ShowDialog();
                            md.Message = "＜アインはファイア・ボールを習得しました＞";
                            md.ShowDialog();
                            mc.FireBall = true;
                        }
                        if (mc.Level >= 6 && !mc.Protection)
                        {
                            mainMessage.Text = "アイン：ＬＶ３：スキル、４：魔法、５：魔法、そしてＬＶ６と言えば。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：どっちなのよ？ハッキリしなさいよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ックソ・・・この流れは俺は認めねえぞ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：誰に対して言ってるのよ、自分で閃いてるんじゃないの？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：知るかよ。ただ、何となく思い出す感じだ・・・Protectionだな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：まあ、思い出すと閃くってのは似たようなものよ。加護系の一種ね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：物理防御力を上げられるようだな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：聖属性の魔法シンプルなだけに強力よね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ああ、上手く使っていくことにするさ。";
                            ok.ShowDialog();
                            md.Message = "＜アインはプロテクションを習得しました＞";
                            md.ShowDialog();
                            mc.Protection = true;
                        }
                        if (mc.Level >= 7 && !mc.DoubleSlash)
                        {
                            mainMessage.Text = "アイン：よっしゃ、レベル７になったぜ。見ろラナ、この鍛えられた体を。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：誰がそんな暑苦しい体なんて見るのよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：お、何かこう閃いたぜ・・・ダブルスラッシュ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：いかにも２回攻撃しそうなネーミングよね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：よく当てたな、ッハッハッハッハ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：一回のターンで、通常攻撃を２回行うみたいね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：１度に２回攻撃すりゃあ敵も早めに倒せる。シンプルながら強いぜ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：２回目の攻撃は技術ポイントも加算対象ね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：マジかよ！？っしゃ、ガンガン使っていくぜ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：肝心な時のためにスキルポイント貯めておいてよね？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：オーケー、了解了解！";
                            ok.ShowDialog();
                            md.Message = "＜アインはダブル・スラッシュを習得しました＞";
                            md.ShowDialog();
                            mc.DoubleSlash = true;
                        }
                        if (mc.Level >= 8 && !mc.FlameAura)
                        {
                            mainMessage.Text = "アイン：ＬＶ１０まで、あと２つと迫ったぜ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：やるじゃないの。さてさて、何か思いついたかしら？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：お、そうだな、以前ラナが言ってたヤツ、あれやってみっか！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：魔法剣の事？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：そうさ、名づけて・・・FlameAuraだ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：ホント安直なネーミングね、いつもながら。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：炎を剣に宿したとする。するとどうなる？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：アンタが決めなさいよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：じゃ、ダメージ追加効果で！！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：また、ダメージなの？炎で敵を包み込んで、視界を遮るとかじゃないワケ？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：大概そういうのは、効かない敵とか居るだろ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：そりゃまあ居ないわけじゃないけどね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：だったらやはり追加ダメージで確定だ。サンキュー！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：どうサンキューなのよ。まったく・・・少しは違うの思いつかないのかしら。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：サンキュー！！";
                            ok.ShowDialog();
                            md.Message = "＜アインはフレイム・オーラを習得しました＞";
                            md.ShowDialog();
                            mc.FlameAura = true;
                        }
                        if (mc.Level >= 9 && !mc.StanceOfStanding)
                        {
                            mainMessage.Text = "アイン：いよいよ、ＬＶ１０だな。その前にもう一つぐらい良いだろ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：もう一つぐらいってどういう内容にするつもりよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：まあ、任せておけって・・・そうだな、StanceOfStandingなんてどうだ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：だから、どういう内容にするつもりよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：何だろうな・・・そうだな、こんな風に防衛の構えをしたままでいるのさ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：どういう内容なのよ、ソレ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：その姿勢を崩さず、そのままの体制で攻撃に移る。大振りはしねえ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：ふーん、３連続ツッコミした割に、まともなのね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：要は防御と攻撃の両方を兼ね備える感じだな";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：うん、バランスが良い感じなんじゃない？気に入ったみたいね♪";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：あぁ、ダメージレースの場合は使っていくとするさ！";
                            ok.ShowDialog();
                            md.Message = "＜アインはスタンス・オブ・スタンディングを習得しました＞";
                            md.ShowDialog();
                            mc.StanceOfStanding = true;
                        }
                        if (mc.Level >= 10 && !mc.WordOfPower)
                        {
                            mainMessage.Text = "アイン：ついにレベル１０到達だ。記念に一つ閃くぜ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：記念に閃くってどういう勘違いなのよ。で、何を閃くのよ？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：何となくだが昔からこの『理』、知っている気がしてんだ。 Word Of Powerさ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：ワード・オブ・・・え？何それ、全然知らないわよ、そんなの。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：そうだな、まあラナにとっては。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：んん？どういう意味よ？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：い、いや何でもねえ。とにかくPOWER = 力こそが全てさ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：何かゴマかしてない？アイン、怒らせないでよ♪";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：力こそが全てさ！ッハッハッハ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：まあ良いわ。で、どんな魔法なわけよ、たまにはアインが解説してみて。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：魔法攻撃ってのは本来任意の属性に応じた攻撃だ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：そうよね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：だが、これは違う。物理攻撃に属する魔法攻撃ってワケさ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：何それ、反則っぽいわね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：魔法としての特性を使って物理攻撃を行うと言う事はだな・・・";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：何よ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：・・・えーとだな、力こそが全てさ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：・・・やっぱ、バカね。";
                            ok.ShowDialog();
                            md.Message = "＜アインはワード・オブ・パワーを習得しました＞";
                            md.ShowDialog();
                            mc.WordOfPower = true;
                        }
                        if (mc.Level >= 11 && !we.AlreadyLvUpEmpty11)
                        {
                            mainMessage.Text = "アイン：ッフ、２桁のＬＶ１１ともなると、どうすっかな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：甘いわねアイン。毎回あると思ったら大間違いよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：どういう意味だよ、説明してみろよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：アイン、今回あなた何も習得できないわよ♪";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：なんだと！？バカ言え、待ってろ今すぐ思いついてやる。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：・・・　・・・　・・・ッソ";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：・・・　・elag・A・";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：Saira Rol・・・";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ぬおおおぉぉおぉぉ・・・";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：っさ、今日は戻ったら夕飯一緒に食べてあげるから、行こ行こ♪";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：な、何故だああああぁぁぁ！！";
                            ok.ShowDialog();
                            md.Message = "＜アインは何も習得できませんでした＞";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty11 = true;
                        }
                        if (mc.Level >= 12 && !mc.HolyShock)
                        {
                            mainMessage.Text = "アイン：前回のレベルアップはしくじったが、その分取り返すぜ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：まあ、さすがに２回続けてっていうのは無さそうね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：イメージは既に在る。聖なる鉄槌、HolyShockさ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：ま、まさか・・・それってダメージ系？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：当たり前だろ。他に何がある？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：ホントダメージ系が多いわね。聖属性なんだから他のイメージは浮かばないワケ？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：聖なる鉄槌さ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：問い正した私が悪かったわ・・・でもまあ火と聖じゃ属性が違うわね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：そうだな、とにかく属性が違っててもダメージは必要だ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：相手によって使い分けも重要だから良く考えて使ってよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：オーケー！";
                            ok.ShowDialog();
                            md.Message = "＜アインはホーリー・ショックを習得しました＞";
                            md.ShowDialog();
                            mc.HolyShock = true;
                        }
                        if (mc.Level >= 13 && !mc.TruthVision)
                        {
                            mainMessage.Text = "アイン：レベルアップするたびに、１つ習得ってのは気分が良いな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：たまに、習得できない場合もあるけどね♪";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：あれは勘弁して欲しいぜ全く・・・いや、マズイ。何とか閃いて見せるぜ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：・・・　・・・　・・・そもそもだ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：そもそも？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：防御力ＵＰや攻撃力ＵＰとかあるだろ。アレが気にいらねえ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：気に入らないって言われてもね。どうするのよ？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：心の本質さえ見抜けば、色付けや飾り付けに惑わされねえハズだ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：心の本質？たまにヘンな事言うわね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：心から物事を見るスキルさ。TruthVisionと名づけるぜ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：真実の視覚、要するに相手の防御力ＵＰや攻撃力ＵＰを";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：そう、無視するって事さ。消すわけじゃねえからな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：ワード・オブなんとかもそうだけど、たまに反則ね、アイン。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：最初から何も付けてなけりゃ関係ねえだろ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：まあそうね、使いどころが難しそうだけど、頑張ってね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ああ、ボス戦などで使いそうだな。任せとけよ。";
                            ok.ShowDialog();
                            md.Message = "＜アインはトゥルス・ヴィジョンを習得しました＞";
                            md.ShowDialog();
                            mc.TruthVision = true;
                        }
                        if (mc.Level >= 14 && !mc.HeatBoost)
                        {
                            mainMessage.Text = "アイン：レベルアップといえば、火属性だ。おっしゃ、Burn！！！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：そろそろ火属性＝ダメージの方程式から卒業したら？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：そうか？まあさすがにBurn！だけじゃ物足りねえ気もするしな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：「侵略する事、火の如し」みたいなのはどう♪";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：確かに何となく早そうだな、それで行ってみるか。HeatBoostでどうだ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：技値のアップね。やっとダメージバカから一歩前進したのね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：何言ってる、技値も立派なダメージ源さ。大して変わりゃしねえよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：フフ、そう言えるようになってきたって事じゃない。その調子で♪";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：まあどっちでも構わないぜ。サクサクっと習得しとくか！";
                            ok.ShowDialog();
                            md.Message = "＜アインはヒート・ブーストを習得しました＞";
                            md.ShowDialog();
                            mc.HeatBoost = true;
                        }
                        if (mc.Level >= 15 && !mc.SaintPower)
                        {
                            mainMessage.Text = "アイン：このレベルアップローテーション、読めて来たぜ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：じゃあ今回は何だと思うわけ？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：スキルだな！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：じゃ、早く思いついて見せてよ♪";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：聖なる力だ、そしてやはり、力こそ全てだ！SaintPower！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：聖属性の魔法ね。残念でした♪";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：しまったぁぁぁああぁぁ！間違えたじゃねえか！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：聖属性のもう一つのパラメタＵＰ魔法ね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ああ、当然だが物理攻撃がＵＰする。俺にとって最強スペルの一つだな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：言っておくけど、特定の箇所を上げるのはスキルには存在しないわよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ラナ、前にも聞いたが、お前たまに何でそう言う事を知ってるんだ？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：知ってるわけじゃないんだけどね、何となく思い出す感じよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：思い出すって言う割に随分と知った風な口調じゃないか。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：まあまあ良いじゃないそんな事は♪";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：まあな、っさ、ガンガン使っていくとするぜ！";
                            ok.ShowDialog();
                            md.Message = "＜アインはセイント・パワー習得しました＞";
                            md.ShowDialog();
                            mc.SaintPower = true;
                        }
                        if (mc.Level >= 16 && !mc.GaleWind)
                        {
                            UpdateMainMessage("アイン：ダブル・スラッシュの時からあった閃きだが。");

                            UpdateMainMessage("ラナ：例の２回攻撃よね？");

                            UpdateMainMessage("アイン：そうだ、これにも『理』の要素がある気がしてたんだよ。");

                            UpdateMainMessage("ラナ：アイン、その『理』ってどっから持ってきたのよ？");

                            UpdateMainMessage("アイン：知るかよ。");

                            UpdateMainMessage("ラナ：何も知らないくせに閃くってこと？");

                            UpdateMainMessage("アイン：知るかって、何となくだよ。２回・・・つまり２人居るようなものさ。");

                            UpdateMainMessage("ラナ：２人居るわけないじゃない。何言ってるのよアイン。");

                            UpdateMainMessage("アイン：同時に像が重なるイメージから呼び起こすんだよ。GaleWindと呼ぶことにするぜ！");

                            UpdateMainMessage("ラナ：ネーミングもかなりオカシイわよ。アイン・・・大丈夫？");

                            UpdateMainMessage("　　　【アインの姿が重複しているように見え始めた】");

                            UpdateMainMessage("ラナ：っちょっと、変な幽霊が付いてるわよ。");

                            UpdateMainMessage("アイン：多分それは俺だ。気にするな。まず試しに一つやるぜ！");

                            UpdateMainMessage("【アイン：食らえ！ファイア・ボール！】　　　　【？？？：食らえ！ファイア・ボール！】");

                            UpdateMainMessage("ラナ：・・・・・・凄いわね・・・・・・驚いたわ。");

                            UpdateMainMessage("アイン：これは使えるぜ。ダブル・スラッシュなら４回攻撃だ。最強だろ！ッハッハッハ！！");

                            UpdateMainMessage("ラナ：アイン、貴方どこでそれを学んだのよ？私、知らないわよその『理』とか言うの。");

                            UpdateMainMessage("アイン：学んだわけじゃねえ。最初っから身に付いているような感じだ。");

                            UpdateMainMessage("ラナ：最初っからって、じゃあ最初っからやってなさいよ。");

                            UpdateMainMessage("アイン：いや、最初っからできる訳じゃねえって・・・");

                            UpdateMainMessage("ラナ：じゃあ、何でそういうのができるのよ！？私もマスターするから、答えてよ！！");

                            UpdateMainMessage("アイン：ラナ、落ち着け。コレは、そういうもんじゃねえんだ。今度また教えてやるさ。");

                            UpdateMainMessage("ラナ：ホント？じゃあ後で良いから教えてよね。ホントにだよ？");

                            UpdateMainMessage("アイン：・・・あぁ、ホントだ。");

                            md.Message = "＜アインはゲイル・ウィンドを習得しました＞";
                            md.ShowDialog();
                            mc.GaleWind = true;
                        }
                        if (mc.Level >= 17 && !we.AlreadyLvUpEmpty12)
                        {
                            UpdateMainMessage("アイン：ＬＶ１０：理１　ＬＶ１１：なし　ＬＶ１６：理２　ＬＶ１７：・・・【在る】！！");

                            UpdateMainMessage("ラナ：アイン、今日は戻ってきたら夕飯一緒にどう♪");

                            UpdateMainMessage("アイン：その夕飯の誘い方は止めてくれええぇぇぇ！！！");

                            UpdateMainMessage("ラナ：ハンナ叔母さんにはもう言っておいたから、心配しなくて良いわよ♪");

                            UpdateMainMessage("アイン：くっそおおぉぉお・・・見てろ・・・");

                            UpdateMainMessage("アイン：ウォオオォォォ・・・　・・・　・・・");

                            UpdateMainMessage("アイン：　出でよ！　　出でよ！！　　出でよ！！！");

                            UpdateMainMessage("アイン：　ハイパー・ジェネティック・エレクトロ・ボンバー！！");

                            UpdateMainMessage("ラナ：ハイハイ、そんなの無いからね。");

                            UpdateMainMessage("アイン：ココでネーミングしたら習得できるんじゃねえのかよ！");

                            UpdateMainMessage("ラナ：そんな勝手なルールも無いからね。諦めてご飯食べよ♪");

                            UpdateMainMessage("アイン：・・・・・・超絶志向究極インセンティブアトラクタ！！！");

                            UpdateMainMessage("　　　『ドグシャアアァァ！！！』（ラナのハイキックが飛んだ）");

                            UpdateMainMessage("アイン：さっ、ダンジョンの後は、夕飯でも行くとするか、ラナ・・・");

                            md.Message = "＜アインは何も習得できませんでした＞";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty12 = true;
                        }
                        if (mc.Level >= 18 && !mc.InnerInspiration)
                        {
                            UpdateMainMessage("アイン：前回のレベルアップ時は確か“戻ってきたら夕飯”だったな。");

                            UpdateMainMessage("ラナ：さすがに２回は無いって言ってるじゃない。どう閃きそうなの？");

                            UpdateMainMessage("アイン：あぁ、戦闘中どうしても連続的な行動のせいで集中できねえ場合がある。");

                            UpdateMainMessage("ラナ：確かに、戦闘中はどうしても気が抜けないわね。");

                            UpdateMainMessage("アイン：そこでだ、気を抜くわけじゃねえが、攻撃や防御もしねえかわりに。");

                            UpdateMainMessage("ラナ：どうするつもりよ？");

                            UpdateMainMessage("アイン：集中すりゃなんとでもなるって事で・・・Inner Inspirationでどうだ。");

                            UpdateMainMessage("ラナ：内側に集中・・・スキルに関連してそうね。");

                            UpdateMainMessage("アイン：呼吸を整えて、体制を整えるのさ。そうすりゃスキルポイントに専念できる。");

                            UpdateMainMessage("ラナ：ライフじゃなく、スキルポイントの回復とは考えたわね♪");

                            UpdateMainMessage("アイン：スキルは何かと使う場面が多いからな。これでもう少しスキルが使えるってわけだ。");

                            UpdateMainMessage("ラナ：スキルポイントの回復は、心値も影響するみたいだから上げておく事ね。");

                            UpdateMainMessage("アイン：了解了解！");

                            md.Message = "＜アインはインナ・インスピレーションを習得しました＞";
                            md.ShowDialog();
                            mc.InnerInspiration = true;
                        }
                        if (mc.Level >= 19 && !mc.WordOfLife)
                        {
                            UpdateMainMessage("アイン：来た来たぁ！ＬＶ１９だぜ！");

                            UpdateMainMessage("ラナ：ＬＶ２０でもないのに何喜んでるのよ？");

                            UpdateMainMessage("アイン：ラナ、ＬＶ２０で“戻ってきたら夕飯”は無いはずだよな。");

                            UpdateMainMessage("ラナ：まあ、区切りの良いポイントでそれは無いわね。");

                            UpdateMainMessage("アイン：その直前で『理』を習得してみせる！");

                            UpdateMainMessage("ラナ：変な所にイチイチ頭使ってないで、ホラホラ、一体どんなのよ？");

                            UpdateMainMessage("アイン：自然の広大な草原、樹齢１千年、源流の滝、火山のマグマなどにも『理』が在る。");

                            UpdateMainMessage("アイン：あれらは在るべくして在り、成るべくして成っている存在だろ。");

                            UpdateMainMessage("ラナ：・・・ま、まあそうね。");

                            UpdateMainMessage("アイン：あれらにはパワーがみなぎってるはずだ。そこからエネルギーを借りるぜ。Word Of Life。");

                            UpdateMainMessage("ラナ：そんなもの感じ取れるの？本当に。");

                            UpdateMainMessage("アイン：ああ、俺たちの祖先は元来からそうしてきてた筈だ。");

                            UpdateMainMessage("ラナ：まあ言われてみればそうかもしれないわね。");

                            UpdateMainMessage("アイン：１ターンに付き、幾ばくかライフが回復する、自発で行動して回復するわけじゃねえ。");

                            UpdateMainMessage("ラナ：回復しながら行動出来るわけね。なかなか強そうじゃない。");

                            UpdateMainMessage("アイン：まあ、ライフが足りてる間は不要だし、欲しい時にガッツリ回復ってわけでもねえ。");

                            UpdateMainMessage("ラナ：そういう意味じゃそれほど強くないかもね。");

                            UpdateMainMessage("アイン：ダメージレースには、うってつけだぜ。任せときな！");

                            md.Message = "＜アインはワード・オブ・ライフを習得しました＞";
                            md.ShowDialog();
                            mc.WordOfLife = true;
                        }
                        if (mc.Level >= 20 && !mc.FlameStrike)
                        {
                            UpdateMainMessage("アイン：おっしゃ！ＬＶ２０到達！長かったぜここまで。");

                            UpdateMainMessage("ラナ：ハンナ叔母さんに聞いても今日はちゃんと作ってくれるらしいわ。良かったわね♪");

                            UpdateMainMessage("アイン：ああ、それじゃいくぜ・・・");

                            UpdateMainMessage("アイン：当然だが、火属性だな。");

                            UpdateMainMessage("アイン：真紅の炎をイメージ。");

                            UpdateMainMessage("アイン：焼け焦がすが如く、直接ダメージだ。");

                            UpdateMainMessage("アイン：FlameStrike！！！");

                            UpdateMainMessage("　　　『ッシュウウゥゥ・・・シュゴオオオォオォオォォォォォ！！！』　　");

                            UpdateMainMessage("ラナ：アンタの攻撃魔法はホント危なっかしいわね。見ていて少し怖いぐらいよ。");

                            UpdateMainMessage("アイン：久しぶりのダメージ系魔法だ。腕がなったぜ。");

                            UpdateMainMessage("ラナ：ファイア・ボールより色が濃いし、凝縮された塊になってたわね");

                            UpdateMainMessage("アイン：ああ、広がった球体だと分散しがちだからな、凝縮させてある。コイツは強力だぜ。");

                            UpdateMainMessage("ラナ：アインはホント火属性がピッタリね。うらやましいぐらいだわ。");

                            UpdateMainMessage("アイン：ラナ、お前も水属性は相性ピッタリだろ。どっちが強いのを生み出すか、競争だな！");

                            UpdateMainMessage("ラナ：フフ、ええ望むところよ、アインなんかに負けないんだから♪");

                            md.Message = "＜アインはフレイム・ストライクを習得しました＞";
                            md.ShowDialog();
                            mc.FlameStrike = true;
                        }
                        if (mc.Level >= 21 && !mc.HighEmotionality)
                        {
                            UpdateMainMessage("アイン：ＬＶ２１到達だぜ！ここで、こうさ。ッガツンと来るモノが欲しいよな。");

                            UpdateMainMessage("ラナ：アインの思いつくのは、ッガツンとしたものばかりじゃない？");

                            UpdateMainMessage("アイン：いや、いやいや違うぜ。こう何て言うんだ・・・");

                            UpdateMainMessage("アイン：全体が湧き上がるかのような・・・");

                            UpdateMainMessage("ラナ：それ以上バカになってどうするつもりよ。");

                            UpdateMainMessage("アイン：バカって言うな・・・良いか行くぜ！　HighEmotionality！");

                            UpdateMainMessage("      『アインの周りに強烈なオーラが表れ始めた』");

                            UpdateMainMessage("ラナ：うわっ、何よそれ？");

                            UpdateMainMessage("アイン：ッハハ、調子良い感じだぜ。　ラナ！っちょっと受け止めてみてくれ！");

                            UpdateMainMessage("アイン：オラァ！　ストレート・スマッシュ！！");

                            UpdateMainMessage("      『ガキイィィ！！』");

                            UpdateMainMessage("ラナ：っちょ、いつもより攻撃が・・・！？　危ないじゃないの！");

                            UpdateMainMessage("アイン：悪いな、今のでも手加減したつもりだ。どうやら全体的に能力がＵＰしてるようだ。");

                            UpdateMainMessage("ラナ：まったくそのバカ力を更に上げてくるなんて、イカサマよね。");

                            UpdateMainMessage("      『アインをとりまくオーラが薄れ始めてきた』");

                            UpdateMainMessage("アイン：っふうううぅ・・・どうやらこれが限界みたいだ。");

                            UpdateMainMessage("ラナ：当たり前よ、あんな危なっかしい状態続くハズないわよ。");

                            UpdateMainMessage("アイン：でもまあ、肝心な局面で使えばこれほどッガツンとしたものは無いぜ。任せておきな！");

                            md.Message = "＜アインはハイ・エモーショナリティを習得しました＞";
                            md.ShowDialog();
                            mc.HighEmotionality = true;
                        }
                        if (mc.Level >= 22 && !mc.WordOfFortune)
                        {
                            UpdateMainMessage("アイン：ＬＶ２２・・・見えるぜ。");

                            UpdateMainMessage("ラナ：何がよ？");

                            UpdateMainMessage("アイン：未来だ。");

                            UpdateMainMessage("ラナ：アンタが一人ボケ笑い死にする所かしら？");

                            UpdateMainMessage("アイン：笑ったまま死ぬわけねえだろうが。正確に言えば、精神集中そのものだ。");

                            UpdateMainMessage("ラナ：こんどは一体どういう内容よ？");

                            UpdateMainMessage("アイン：この１ターンは何もせず、剣の切っ先一点に全神経を集中させる魔法だ。");

                            UpdateMainMessage("ラナ：なるほど、何となく読めたわ。出すつもりね・・・クリティカル");

                            UpdateMainMessage("アイン：ご名答だ。命名はWord Of Fortuneとするぜ！");

                            UpdateMainMessage("アイン：全神経の集中がキマったら、後は攻撃あるのみだ！　オラァ！");

                            UpdateMainMessage("ラナ：凄いわね、本当にクリティカルが出るなんて。");

                            UpdateMainMessage("アイン：しかし、この集中方法は本当に短い間しか維持できねえ。１ターンが限度だな。");

                            UpdateMainMessage("ラナ：あらかじめクリティカルを出すために１ターン犠牲にするのよね。");

                            UpdateMainMessage("アイン：そうだな、結構使い道は限られるかもしれない。だが、使い切ってみせるぜ。");

                            md.Message = "＜アインはワード・オブ・フォーチュン習得しました＞";
                            md.ShowDialog();
                            mc.WordOfFortune = true;
                        }
                        if (mc.Level >= 23 && !mc.Glory)
                        {
                            UpdateMainMessage("アイン：ＬＶ２３っと！　ラナ、今日は“戻ってきたら夕飯”の誘いは無いんだよな？");

                            UpdateMainMessage("ラナ：ええ、今日は特に無いわよ。");

                            UpdateMainMessage("アイン：おっしゃ！じゃあ、今日もバンバン閃いてみせる！");

                            UpdateMainMessage("ラナ：っちょ、そんな喜ばなくたって良いじゃない。失礼ね。");

                            UpdateMainMessage("アイン：ッハッハッハ！悪い悪い！　ってか、よく考えたらさ。");

                            UpdateMainMessage("アイン：ダメージレースってあるだろ？100ダメージ与えて、40ダメージ食らうみたいな。");

                            UpdateMainMessage("ラナ：戦闘中の基本中の基本ね。それがどうかしたの？");

                            UpdateMainMessage("アイン：最近気付いたが、FreshHealは詠唱後の効果と詠唱前モーションに若干時間差が発生してる。");

                            UpdateMainMessage("アイン：つまり、この時間差を上手く繋げばヒール＆アタックの完成だ。");

                            UpdateMainMessage("アイン：この流れは負ける気がしねえぜ！ッハッハッハ！　Gloryと名付けるぜ！");

                            UpdateMainMessage("ラナ：ヒール＆アタック・・・シンプルなだけに、バカね。");

                            UpdateMainMessage("アイン：バカって言うな、結構強力だぜ！？");

                            UpdateMainMessage("アイン：ヒール＆アタック！　ヒール＆アタック！　ノリが良いだろ！　ッハッハッハ！");

                            UpdateMainMessage("ラナ：ハアアァァ・・・・正真正銘のバカね。");

                            md.Message = "＜アインはグローリーを習得しました＞";
                            md.ShowDialog();
                            mc.Glory = true;
                        }
                        if (mc.Level >= 24 && !mc.VolcanicWave)
                        {
                            UpdateMainMessage("アイン：おおおぉ・・・ＬＶ２４！！");

                            UpdateMainMessage("ラナ：中途半端な数字なのに、無駄に盛り上げてるわね。");

                            UpdateMainMessage("アイン：火属性に決定だ！！どうだ！？");

                            UpdateMainMessage("ラナ：確かこの前、火属性のFlameStrikeを習得してなかった？");

                            UpdateMainMessage("アイン：良いじゃねえか、たまには連発でもな。");

                            UpdateMainMessage("ラナ：まさか・・・ダメージじゃないでしょうね。");

                            UpdateMainMessage("アイン：っそのまさかだ！ッハッハッハ！");

                            UpdateMainMessage("ラナ：っちょっと、本当にダメージバカね・・・何か他のが良いんじゃない？");

                            UpdateMainMessage("アイン：良いんだって、このぐらいでも丁度良いぐらいだろ。行くぜ！ Volcanic Wave！");

                            UpdateMainMessage("　　　『シュゴオオォォォォ・・・ブワアアァァア！！！』　　");

                            UpdateMainMessage("アイン：ッハッハッハ！燃え上がれ！燃え盛れ！そして、燃え尽きちまいな！");

                            UpdateMainMessage("ラナ：最後微妙に単語間違えてるわよ、アイン。");

                            UpdateMainMessage("アイン：オーケーオーケー多少の単語ミスは気にしねえ！");

                            UpdateMainMessage("ラナ：ホンット無茶苦茶やって・・・でも、さすが火属性よね。威力は相当ありそうだわ。");

                            UpdateMainMessage("アイン：ああ、FlameStrikeの時点でイメージがあった。凝縮されたものを連続で波のように出す感じだな。");

                            UpdateMainMessage("ラナ：マナ消費もかなりデカそうね。マナ切れに気をつけて使っていってよね。");

                            UpdateMainMessage("アイン：了解了解！");

                            md.Message = "＜アインはヴォルカニック・ウェイブを習得しました＞";
                            md.ShowDialog();

                            mc.VolcanicWave = true;
                        }
                        if (mc.Level >= 25 && !mc.CrushingBlow)
                        {
                            UpdateMainMessage("アイン：ＬＶ２５か。ちょっとした中間ポイントみたいなもんだな。");

                            UpdateMainMessage("ラナ：どう、何か思いつきそう？");

                            UpdateMainMessage("アイン：いや、どうだろうな。やんわりとした閃きはあるんだが・・・");

                            UpdateMainMessage("ラナ：そうなの？何だかハッキリしない言い方ね。");

                            UpdateMainMessage("アイン：さて・・・どんなのが良いか・・・");

                            UpdateMainMessage("ラナ：私がアインへ脳天直撃貫通ブローをやれば閃くとか？");

                            UpdateMainMessage("アイン：おお！　それだ！");

                            UpdateMainMessage("ラナ：っえ、本気なの？アイン・・・あんたひょっとして真性バカ？　じゃあ遠慮なく♪");

                            UpdateMainMessage("アイン：あ、いやいや！　待て待て待て！！俺にそのブローは止めてくれ！閃きが無くなっちまう！！");

                            UpdateMainMessage("ラナ：う～ん。なんだ、つまんないわね♪　まあいいわ、何を閃いたのよ？");

                            UpdateMainMessage("アイン：モンスターの脳天へ直接打撃を加える事で一瞬気絶させるってのはどうだ。");

                            UpdateMainMessage("ラナ：なるほど、相手を一瞬でも気絶させれば、行動を一回止める事が出来るわね。良いんじゃない？");

                            UpdateMainMessage("アイン：ネーミングはそうだな・・・いつもラナの極悪技の名前を使って・・・。");

                            UpdateMainMessage("アイン：CrushingBlowってのはどうだ！　っな！　ラナ！？");

                            UpdateMainMessage("ラナ：だ・れ・が・極悪よ！？食らいなさい！　ッハアアァァ！！");

                            UpdateMainMessage("　　　『ズドオオォォォン！！！』（ラナのクリーンクリティカルクラッシングがアインに炸裂した）");

                            md.Message = "＜アインはクラッシング・ブローを習得しました＞";
                            md.ShowDialog();

                            mc.CrushingBlow = true;
                        }
                        if (mc.Level >= 26 && !we.AlreadyLvUpEmpty13)
                        {
                            UpdateMainMessage("アイン：まあＬＶ２６って所だな。っさてと！");

                            UpdateMainMessage("ラナ：っさて、今日は戻ったら夕飯ご一緒させてもらうわ♪");

                            UpdateMainMessage("アイン：・・・");

                            UpdateMainMessage("アイン：・・・っさてと！どうすっかな！");

                            UpdateMainMessage("ラナ：たまには新しい外食系のお店でも探してみない？");

                            UpdateMainMessage("アイン：・・・さてと！さてと！さてと！！！");

                            UpdateMainMessage("アイン：ウオオオォォォ！！　ありえねえぇだろおおぉぉおぉ！！");

                            UpdateMainMessage("アイン：必殺！　オレンジハーブティ！");

                            UpdateMainMessage("アイン：奥義！　グリルチキン！");

                            UpdateMainMessage("アイン：必殺コンボ！　こんがりビーフグラタン！");

                            UpdateMainMessage("アイン：神技！　季節風・秋の春雨！");

                            UpdateMainMessage("アイン：なんだ、この閃きはああぁぁぁ！！");

                            UpdateMainMessage("ラナ：美味しい店なら、そうね『満万兆幸笑』がオススメよ。");

                            UpdateMainMessage("アイン：おい、ラナ！この設定はどう考えてもおかしいだろ！？");

                            UpdateMainMessage("ラナ：う～ん、戻ってきたら一緒にご飯食べるんだからもっと喜んだら？");

                            UpdateMainMessage("アイン：・・・　そうだなあ、本来は喜ぶ所なのかもな・・・（ガク）");

                            UpdateMainMessage("ラナ：っささ、気にせず『満万兆幸笑』へ行きましょ、ッホラホラ♪");

                            UpdateMainMessage("アイン：神よ、これは喜ぶべきなのか。悲しむべきなのか・・・・・");

                            md.Message = "＜アインは何も習得できませんでした＞";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty13 = true;
                        }
                        if (mc.Level >= 27 && !mc.AetherDrive)
                        {
                            UpdateMainMessage("アイン：ＬＶ２７だぜ！連続休みはねえんだったよな！？よし来たぜ！");

                            UpdateMainMessage("ラナ：何よ、そんなに私と夕飯食べるのが嫌なのかしら。");

                            UpdateMainMessage("アイン：い、いやいやいや、そういうわけじゃねえ。");

                            UpdateMainMessage("ラナ：ッフフ、まあ良いわよ。で、どうなの閃き具合は？");

                            UpdateMainMessage("アイン：前回のHighEmotionalityは精神的な高揚から思いついているんだが。");

                            UpdateMainMessage("アイン：今回はもっと自然法則に沿ったものを生み出してみたいんだよ。");

                            UpdateMainMessage("ラナ：何だかまたヘンテコな発想を持ち出してきたわね。");

                            UpdateMainMessage("アイン：ヘンテコって何だよ、これでもちゃんと考えてるんだぜ。");

                            UpdateMainMessage("アイン：過去の遺産とも呼べるエーテルの力。これをあえて魔法として乗せてはどうだ？");

                            UpdateMainMessage("ラナ：どうだ？って私に言われても解答のしようがないわ。どうなの、出来そうなの？");

                            UpdateMainMessage("アイン：最初のWordOfPowerがあったろ。アレを攻撃ではなく、周囲に一定維持させるような感じだ。");

                            UpdateMainMessage("アイン：こうやって・・・それに乗っける形として周囲に取り巻く状況を生成だ！　AetherDrive！");

                            UpdateMainMessage("      『ヴヴウウゥゥゥン！！』（アインの周囲に新しい空想物理法則が生成された！）");

                            UpdateMainMessage("ラナ：ッワ！何よ、それ！？WordOfPowerの塊みたいね・・・");

                            UpdateMainMessage("アイン：ああ、ちょっと素振りしてみるか・・・オラァ！");

                            UpdateMainMessage("ラナ：・・・凄いじゃない！？　いつもより遥かに振りが速いわ！！");

                            UpdateMainMessage("アイン：それだけじゃねえ・・・見ろ、これを俺の素振り側じゃなくて、ここの『ダミー素振り君』に当てておけば。");

                            UpdateMainMessage("ラナ：っあ！『ダミー素振り君』の振りが遅くなってるわね。");

                            UpdateMainMessage("アイン：そうだ、これは空想物理の応用次第で、攻撃・防御ともに優れた効果を発揮できる。");

                            UpdateMainMessage("ラナ：WordOfPowerからつくづく思うんだけど、アインのそういうとこ真似できないわね。");

                            UpdateMainMessage("アイン：そうか？ラナもちょっとやれば習得できそうだけどな。");

                            UpdateMainMessage("ラナ：う～ん・・・やめとくわ。私は私のやり方で習得していくから。");

                            UpdateMainMessage("アイン：そうか。まあそれが良いかもな。じゃ遠慮なく、この魔法はガンガン使わせてもらうぜ！");

                            md.Message = "＜アインはエーテル・ドライブを習得しました＞";
                            md.ShowDialog();
                            mc.AetherDrive = true;
                        }
                        if (mc.Level >= 28 && !mc.KineticSmash)
                        {
                            UpdateMainMessage("アイン：ようやくＬＶ２８だな、ＬＶ３０まであと少しだぜ。");

                            UpdateMainMessage("ラナ：結構強力なのが揃ってきてるわよね。");

                            UpdateMainMessage("アイン：ああ、だがオレはここでもう一つ一線を越えたスキルを習得するつもりだ。");

                            UpdateMainMessage("ラナ：また何かダメージ系なワケ？");

                            UpdateMainMessage("アイン：ああ、そうさ！！");

                            UpdateMainMessage("ラナ：何か他の類は閃かないの？");

                            UpdateMainMessage("アイン：ダメージ系さ！任せとけって！");

                            UpdateMainMessage("ラナ：まあ良いけどね、一体どういうものに仕上げるつもりよ。");

                            UpdateMainMessage("アイン：剣技を一つ編み出そうと思ってな！まあ、基本はコレだ。");

                            UpdateMainMessage("　　　『ッブン、ッブブン！』（アインは２，３回適度に素振りした）");

                            UpdateMainMessage("アイン：っさてっと。ココからだ・・・行くぜ。");

                            UpdateMainMessage("ラナ：へえ・・・何だか随分と自然な構えね。いつもの突進型じゃないわね。");

                            UpdateMainMessage("アイン：俺は武器の威力の高さってのは原型追求だと思ってる。");

                            UpdateMainMessage("ラナ：っえ？");

                            UpdateMainMessage("アイン：構えすぎても駄目だ。力が分散するのも駄目だ。");

                            UpdateMainMessage("アイン：どの構えが一番力が凝縮されるか、考えた末にこのスタイルにたどり着いた。");

                            UpdateMainMessage("ラナ：構えって言うより、何も考えずそこに突っ立っているだけにしか見えないわよ。");

                            UpdateMainMessage("アイン：そうだ、この突っ立っている状態からが一番力を出せる。そんな気がするんだ。");

                            UpdateMainMessage("アイン：剣の最高速度へ到達するのと同時に力を一気に凝縮させる、いくぜKineticSmash！！");

                            UpdateMainMessage("      『ッガ！ッシイイィィ・・・』");

                            UpdateMainMessage("ラナ：音が何か凄く奇妙ね。派手さはないけど、ほとんど何でもなぎ倒せそうね。");

                            UpdateMainMessage("アイン：っしゃ！これは行けるぜ、力と武器攻撃、加えて心の集中も必要不可欠だな。");

                            UpdateMainMessage("アイン：武器が強ければ強いほど威力も増すことが出来そうだ。これは使えるぜ！");

                            md.Message = "＜アインはキネティック・スマッシュを習得しました＞";
                            md.ShowDialog();
                            mc.KineticSmash = true;
                        }
                        if (mc.Level >= 29 && !mc.StanceOfEyes)
                        {
                            UpdateMainMessage("アイン：ＬＶ２９だぜ！あと一つと迫ったな！");

                            UpdateMainMessage("ラナ：ＬＶ３０記念があるんだから、あんまり派手なのは避けておいたら？");

                            UpdateMainMessage("アイン：お、良いこと言うな、ラナ。そうだな、ここは一つ地味に行くとするか。");

                            UpdateMainMessage("アイン：確かにダメージ系ばかりが増えがちだからな、何にするか・・・");

                            UpdateMainMessage("　　　『アインは珍しく真面目な顔をして真っ直ぐに空中を凝視し始めた』");

                            UpdateMainMessage("ラナ：あっ、それ良いんじゃない？アインらしさが無くて丁度良さそうよ。");

                            UpdateMainMessage("アイン：ん？何がだ？");

                            UpdateMainMessage("　　　『アインの顔は元の会話モードに戻った。』");

                            UpdateMainMessage("ラナ：今、何か物凄い勢いで睨み付けてたじゃない。何でも見切れそうな顔してたわよ。");

                            UpdateMainMessage("アイン：ん？ん、ああ、ちょっといろいろ考えてたのさ。");

                            UpdateMainMessage("アイン：待てよ・・・そうか。そういう事か！　決めた、StanceOfEyesと名づけるぜ！");

                            UpdateMainMessage("アイン：ラナ、丁度良い。少し練習だ。");

                            UpdateMainMessage("ラナ：何を思いついたって言うのよ？変なネーミングしちゃって、あんまりジロジロ見ないでよ。");

                            UpdateMainMessage("アイン：い、いや、そういう意味じゃねえ・・・まあいい、ちょっとだけ頼むぜ。");

                            UpdateMainMessage("ラナ：分かったわ、行くわよ・・・");

                            UpdateMainMessage("ラナ：ハアァァ！アイスニードル！！");

                            UpdateMainMessage("アイン：っしゃ、ソコだ！！");

                            UpdateMainMessage("　　　『アインはとっさにラナの詠唱したアイスニードルの生成瞬間に剣を振りかざした！』");

                            UpdateMainMessage("ラナ：・・・ッウソ！？私の魔法がかき消された！？");

                            UpdateMainMessage("アイン：ッハッハッハ！どうだ驚いたか！");

                            UpdateMainMessage("ラナ：地味なわりに物凄く派手な効果ね・・・相変わらずと行った所かしら。");

                            UpdateMainMessage("ラナ：でもまあ、それをやってる間攻撃はさすがに出来ないわけね。");

                            UpdateMainMessage("アイン：まあな、相手の動作を集中して見てないと出来ねえ。");

                            UpdateMainMessage("ラナ：相手が何か狙っている場合はかなり使えそうじゃない？");

                            UpdateMainMessage("アイン：ああ、相手の大技とかはコレで封じてやるぜ、任せておきな！");

                            md.Message = "＜アインはスタンス・オブ・アイズを習得しました＞";
                            md.ShowDialog();
                            mc.StanceOfEyes = true;
                        }
                        if (mc.Level >= 30 && !mc.Resurrection)
                        {
                            UpdateMainMessage("アイン：おっしゃ！ついにＬＶ３０達成だぜ！");

                            UpdateMainMessage("ラナ：なかなかやるわね、とりあえずおめでとう、アイン。");

                            UpdateMainMessage("アイン：ああ、サンキュー。ＬＶ３０ともなると成長した実感が沸くな。");

                            UpdateMainMessage("ラナ：アインってホントバカなわりに、結構まともに成長するのね。");

                            UpdateMainMessage("アイン：お前な。バカだったら成長しねえって事はないだろ。");

                            UpdateMainMessage("ラナ：バカだから成長率が高いのかも知れないわね。");

                            UpdateMainMessage("アイン：ったく、少し褒められたと思えばすぐコレだな。まあ良い、閃いた内容はだな。");

                            UpdateMainMessage("アイン：とりあえずダメージ系では・・・ない！");

                            UpdateMainMessage("　　　『天空より激しい稲光が数十箇所へ降り注いだ！！！』");

                            UpdateMainMessage("ラナ：ッウ・・・ウソ・・・ッバ・・・バカアインに限って・・・");

                            UpdateMainMessage("アイン：記念はいつもダメージ系だと思っていたのか？甘いなラナ。");

                            UpdateMainMessage("ラナ：じゃあ何だって言うのよ？");

                            UpdateMainMessage("アイン：ダメージ系じゃなくとも、特別強烈な魔法だ。");

                            UpdateMainMessage("アイン：そうだな、そこで今、ラナ様に踏まれてしまったゲンゴロー虫を対象にしてみるか。");

                            UpdateMainMessage("ラナ：っえ！ウソ踏んじゃってた！？");

                            UpdateMainMessage("アイン：ああ、ちょっとどいてな。やってみるぜ・・・");

                            UpdateMainMessage("アイン：死亡っていう状態は、肉体的な崩壊とそれを実感した精神が共にやられた状態だ。");

                            UpdateMainMessage("アイン：活動停止から極端に長い期間が空いてしまったらどうしようもねえが");

                            UpdateMainMessage("アイン：たった今踏まれてしまった可哀想なゲンゴロー虫君なら・・・");

                            UpdateMainMessage("アイン：精神を統合した状態で、深層世界へ潜り込むイメージで・・・Resurrection！");

                            UpdateMainMessage("　　　『ゲンゴロー虫君は、淡い光に包まれたと同時に再び動き始めた！』");

                            UpdateMainMessage("ラナ：っす・・・凄いじゃない！？死者蘇生でしょ、コレ！？");

                            UpdateMainMessage("アイン：随分と大げさな表現だな。もう少し正確な表現をするとすれば、復活だな。");

                            UpdateMainMessage("ラナ：十分過ぎるわよ、こんなの普通やろうと思ってやるもんじゃないわよ。");

                            UpdateMainMessage("アイン：しかし、マナの消費量が半端じゃねえ。何度も連発できるような代物じゃねえな。");

                            UpdateMainMessage("ラナ：そうよね、他の魔法のためにもマナは節約するものだし、過度な期待は出来ないわね。");

                            UpdateMainMessage("アイン：ま、というわけだ。どうだ最高の魔法だろ？");

                            UpdateMainMessage("アイン：ラナ、万が一お前がやられた時は、お前の深層世界へ潜り込むイメージで・・・");

                            UpdateMainMessage("　　　『ドバキグッシャアアアァァ！！！』（ラナのデストラクション・キックがアインに炸裂した）");

                            md.Message = "＜アインはリザレクションを習得しました＞";
                            md.ShowDialog();
                            mc.Resurrection = true;
                        }
                        if (mc.Level >= 31 && !mc.Catastrophe)
                        {
                            UpdateMainMessage("アイン：ようやくＬＶ３０から３１だな、ここまで来ると中々上がりにくいな。");

                            UpdateMainMessage("ラナ：そうね、ある程度の成長もしたわけだし、伸びもそこそこになりそうね。");

                            UpdateMainMessage("アイン：まあその分だけ閃きの内容も強力なモノになるはずだ。");

                            UpdateMainMessage("アイン：そういや、リザレクションの前ぐらいにKineticSmashをやってたよな。");

                            UpdateMainMessage("ラナ：StanceOfEyesの前じゃない？まあいいけど、どの辺から閃いたわけ？");

                            UpdateMainMessage("アイン：んそうだったっけか？まあ良いか、KineticSmashが原点だが。");

                            UpdateMainMessage("アイン：まあ、簡単に言えば、あれを更に洗練化したものだ。");

                            UpdateMainMessage("アイン：武器に対する自然の構えから、集中力も勿論必要なわけだ。");

                            UpdateMainMessage("アイン：そこに更に加えて、初期モーション、最高到達点の導出を加える。");

                            UpdateMainMessage("ラナ：へえ・・・何となくStanceOfStandingの構えに似てるわね。");

                            UpdateMainMessage("アイン：そうか？そんなつもりは無いんだがな、確かに似てるかもしれねえな。");

                            UpdateMainMessage("アイン：さてと・・・いくぜ！【究極奥義】Catastrophe！！");

                            UpdateMainMessage("　　　『ッドスウウゥゥゥン！！！』　　");

                            UpdateMainMessage("ラナ：物凄い轟音ね・・・破壊的って言うよりは全身全霊と言った感じかしら？");

                            UpdateMainMessage("アイン：ああ・・・そう・・・だな・・・っふううう、マジで疲れた！！");

                            UpdateMainMessage("アイン：駄目だ、コイツは全スキルポイントを使っちまうみたいだ。１回しか出来ねえ。");

                            UpdateMainMessage("ラナ：全スキルポイントって言うと、１とか２でも一応可能なわけ？");

                            UpdateMainMessage("アイン：ああ、やって出来ない事はねえ。だが威力は多分そんなに出せねえな。");

                            UpdateMainMessage("ラナ：なるべくスキルポイントを溜めておいた方が良いって事ね。");

                            UpdateMainMessage("アイン：まあな、最後の一撃必殺に近い感じで使えば良いって所だ。");

                            md.Message = "＜アインはカタストロフィー習得しました＞";
                            md.ShowDialog();
                            mc.Catastrophe = true;
                        }
                        if (mc.Level >= 32 && !we.AlreadyLvUpEmpty14)
                        {
                            UpdateMainMessage("アイン：おし、ＬＶ３２か。Resurrection、Catastropheと来て次はだな・・・");

                            UpdateMainMessage("ラナ：アイン、ちょっと話があるんだけど♪");

                            UpdateMainMessage("アイン：飯の話ならお断りだ。");

                            UpdateMainMessage("ラナ：ちょっと話するだけよ♪");

                            UpdateMainMessage("アイン：お前なんで♪付けてんだよ？");

                            UpdateMainMessage("ラナ：良いからイイから、そんな細かい事気にしないでよ♪");

                            UpdateMainMessage("アイン：っく・・・くおおおおぉぉぉ！！！");

                            UpdateMainMessage("アイン：俺よ！目覚めよ！　ハアアァァァァァア！！");

                            UpdateMainMessage("アイン：・・・う、うおおおぉぉおおぉぉ！！");

                            UpdateMainMessage("アイン：おおおぉぉおおぉぉおおおおおおおぉぉぉぉぉ！！！");

                            UpdateMainMessage("ラナ：うわっ・・・結構頑張るわね・・・");

                            UpdateMainMessage("アイン：ウオラアアアアアアアアアアアアアァァァ！！！");

                            UpdateMainMessage("アイン：　　　　　　【パンチ！！！】");

                            UpdateMainMessage("アイン：・・・・・・");

                            UpdateMainMessage("ラナ：ップ・・・ッププ、アインごめんなさい、笑っちゃ駄目なんだけど・・ッフフ");

                            UpdateMainMessage("アイン：・・・そうだな・・・さすがに無理っぽいな。戻ったら喜んで食べるとするか！");

                            UpdateMainMessage("ラナ：ついに諦めたワケね。そうよ、たまには諦めも肝心よ。");

                            UpdateMainMessage("アイン：ああ、また上手い外食屋探して歩こうぜ！ッハッハッハ！");

                            UpdateMainMessage("ラナ：ッフフ、何よそれ。うん、じゃダンジョンの後、歩いて回って見ましょ♪");

                            md.Message = "＜アインは何も習得できませんでした＞";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty14 = true;
                        }
                        if (mc.Level >= 33 && !mc.Genesis)
                        {
                            UpdateMainMessage("アイン：ＬＶ３３到達だぜ！");

                            UpdateMainMessage("アイン：ラナ、この前は一緒に飯くれてサンキューな。");

                            UpdateMainMessage("ラナ：急に感謝するなんて気持ち悪いわね。");

                            UpdateMainMessage("アイン：いや、確かに無理して何かを会得しようとするのは駄目なのかもな。");

                            UpdateMainMessage("ラナ：そうね、閃きや思いつきなんていうのは気まぐれが一番よ。");

                            UpdateMainMessage("アイン：今回思いついたのは、まさに気まぐれだ。");

                            UpdateMainMessage("アイン：ある程度気まぐれでも出来る行為は、あんまり意識しなくても出来てるだろ？");

                            UpdateMainMessage("ラナ：まあそうかもね。");

                            UpdateMainMessage("アイン：これさ、上手く回せねえかと考えたんだ。たとえば前回やった行為だが。");

                            UpdateMainMessage("アイン：一番初めにやった行為が全て原点であると仮定してだな。");

                            UpdateMainMessage("アイン：次からの行為は全てがその原点に対する模倣・変化・発展・一新・普遍だったりするわけだ。");

                            UpdateMainMessage("ラナ：え？え、えぇ・・・");

                            UpdateMainMessage("アイン：初回アクションの原点さえガッチリ掴んでおけば、次全く同じ行為は意識しないで済む。");

                            UpdateMainMessage("アイン：まず初回アクションの一例だ。ダブル・スラッシュ！オラァ！");

                            UpdateMainMessage("アイン：そして全ては原点にして在る。原点に立ち返る事で編み出される魔法、Genesis！");

                            UpdateMainMessage("アイン：もういっちょ行くぜ、ダブル・スラッシュ！");

                            UpdateMainMessage("ラナ：っあ、さっきと全く同じ技ね。その魔法で同じ行為が繰り出せるわけ？");

                            UpdateMainMessage("アイン：そうだな。しかもこの魔法は全然意識しなくても良いからコスト０ってわけだ。");

                            UpdateMainMessage("アイン：更に前回行為は掌握済みだから、前回支払った魔法コスト・スキル消費も０ってわけだ。");

                            UpdateMainMessage("ラナ：何それ、微妙に反則っぽいわね。");

                            UpdateMainMessage("アイン：２回目以降で同じ行為をしたい時だけだ、気楽な魔法だが意外と使い所は難しいかもな。");

                            UpdateMainMessage("ラナ：それでも、反則魔法っぽいわ。私はそういうの苦手だな。");

                            UpdateMainMessage("アイン：まあ、この魔法は自分自身にしかかけられねえから。俺なりに何かコンボを作ってくぜ。");

                            md.Message = "＜アインはジェネシスを習得しました＞";
                            md.ShowDialog();
                            mc.Genesis = true;
                        }
                        if (mc.Level >= 34 && !mc.SoulInfinity)
                        {
                            UpdateMainMessage("アイン：ＬＶ３４・・・結構あがったよな、本当に。");

                            UpdateMainMessage("ラナ：ここまで来て何感傷に浸ってんのよ。ホラホラ、っささっと思い付いてよ。");

                            UpdateMainMessage("アイン：何だその無理やりな注文は？ったく、しょうがねえなあ・・・");

                            UpdateMainMessage("アイン：さっさと思いつくと言えば、やっぱアレだな。ダメージ系だろ！！");

                            UpdateMainMessage("ラナ：うわっ・・・しまった。バカアインがバカだって事うっかり忘れてたわ。");

                            UpdateMainMessage("アイン：ああ、何とでも言え！ダメージバカさ！ッハッハッハ！");

                            UpdateMainMessage("アイン：おっしゃ、何だか絶好調だぜ！うおおおお、【究極奥義】SoulInfinity！！");

                            UpdateMainMessage("ラナ：っわ！っ凄い突風だったわ。あんなの当たったら正直まともに立ってられないわよ。");

                            UpdateMainMessage("アイン：この高揚はすげえ・・・力・技・知・心の全パラメタが重要になってきそうだ。");

                            UpdateMainMessage("アイン：どっか一つに偏らせたら駄目だ。全てをまんべんなく上げた方が値は高いな。");

                            UpdateMainMessage("ラナ：何かとっとと思いついただけあって、とりあえず何でも付けてみたような感じね。");

                            UpdateMainMessage("アイン：ああ、確かにそうだな。だが、ダメージＭＡＸ自体はパラメタの割り振り次第では一番かもな。");

                            UpdateMainMessage("ラナ：でも、ある程度偏っていてもソコソコのダメージは期待できるんじゃない？");

                            UpdateMainMessage("アイン：そうだな、スキル消費もそれほど多いわけじゃねえ。気分が整い次第バンバン使っていくぜ！");

                            md.Message = "＜アインはソウル・インフィニティを習得しました＞";
                            md.ShowDialog();
                            mc.SoulInfinity = true;
                        }
                        if (mc.Level >= 35 && !mc.ImmortalRave)
                        {
                            UpdateMainMessage("アイン：ＬＶ３５は嬉しいんだが・・・");

                            UpdateMainMessage("ラナ：何戸惑ってんのよ、ハッキリ言いなさいよ。");

                            UpdateMainMessage("アイン：火魔法で思いついたんだが、こういうのはアリなのかどうか。");

                            UpdateMainMessage("ラナ：どんな効果よ？って、バカアインの場合はダメージ系よね？");

                            UpdateMainMessage("アイン：火の魔法と言えば当然ダメージ系だ。");

                            UpdateMainMessage("ラナ：身も蓋も無いわね、アイン。");

                            UpdateMainMessage("アイン：ただ、直接的なダメージばかりじゃ面白くはねえ。");

                            UpdateMainMessage("ラナ：やっと直接以外の事も考えるようになったのね。");

                            UpdateMainMessage("アイン：そこでだ、火の魔法を俺の周囲である程度蓄えるってのはどうだ？");

                            UpdateMainMessage("ラナ：蓄える？どんな風によ。");

                            UpdateMainMessage("アイン：俺はこれをImmortalRaveと命名するぜ、オラァ！ファイアボール！");

                            UpdateMainMessage("　　　『アインの周囲に小さい炎が一つ漂い始めた』");

                            UpdateMainMessage("ラナ：へえなるほど、面白いわね。");

                            UpdateMainMessage("アイン：っしゃ、フレイムストライク！");

                            UpdateMainMessage("　　　『アインの周囲に大きめの炎が一つ漂い始めた』");

                            UpdateMainMessage("アイン：これでラストだ！ヴォルカニックウェイブ！");

                            UpdateMainMessage("　　　『アインの周囲にバカデカイ炎が一つ漂い始めた』");

                            UpdateMainMessage("ラナ：同時に３つも蓄えるなんて・・・アインたまに凄いわね。");

                            UpdateMainMessage("アイン：いくぜ、ストレートスマッシュオラァ！");

                            UpdateMainMessage("アイン：そして同時に、ファイアボールだ！");

                            UpdateMainMessage("アイン：次々いくぜ、ダブル・スラッシュ！");

                            UpdateMainMessage("アイン：同時に、フレイムストライク！");

                            UpdateMainMessage("アイン：オラオラオラァ！ラスト、キネテッィクスマッシュ！");

                            UpdateMainMessage("アイン：こいつも同時に食らえ、ヴォルカニックウェイブ！");

                            UpdateMainMessage("ラナ：え、何それ・・・反則じゃない？");

                            UpdateMainMessage("アイン：ああ、反則かも知れないな。だが俺はやってみせる！");

                            UpdateMainMessage("ラナ：せっかく凄い魔法なのに、最後の言葉使いが間違ってるわよ。");

                            UpdateMainMessage("アイン：何！？しまった、かなりカッコよく決めたつもりが・・・");

                            UpdateMainMessage("ラナ：まあ、今の３連続はかなりダメージがでかそうね。期待してるわよ。");

                            UpdateMainMessage("アイン：あ、ああ、任せておけって！ッハッハッハ！");

                            md.Message = "＜アインはイモータル・レイブを習得しました＞";
                            md.ShowDialog();
                            mc.ImmortalRave = true;
                        }
                        if (mc.Level >= 36 && !mc.PainfulInsanity)
                        {
                            UpdateMainMessage("アイン：ＬＶ３６って所だな。ＭＡＸ４０まであと４つと迫ったぜ！");

                            UpdateMainMessage("ラナ：ここまで来ると閃く内容もかなり強烈なんじゃない？");

                            UpdateMainMessage("アイン：ああ、俺は「心眼」系のスキルでどうしても一つやってみたいのがあるんだ。");

                            UpdateMainMessage("ラナ：一体どんなのよ？");

                            UpdateMainMessage("アイン：勿論、ダメージ系さ！！！");

                            UpdateMainMessage("ラナ：・・・え、ウソ、本気なの？");

                            UpdateMainMessage("アイン：本気に決まってるだろ！　ッハッハッハ！！");

                            UpdateMainMessage("ラナ：ホントダメージ系ばっかりなのね。でも「心眼」系なんかでどうするつもりよ？");

                            UpdateMainMessage("アイン：相手の心の中を探るんじゃなくてだな。直接圧迫する感じだ。");

                            UpdateMainMessage("アイン：ちょっとそこのミューミューフラワーで実験してみるぜ。");

                            UpdateMainMessage("ラナ：あ、止めてよ。ミューミューフラワーが可哀想じゃないのよ。");

                            UpdateMainMessage("アイン：じゃあ、どれでやれっていうんだ。");

                            UpdateMainMessage("ラナ：そこのゲバゲバ君で良いんじゃない？");

                            UpdateMainMessage("アイン：オーケーオーケー。じゃあやってみるぜ。いくぜ！【究極奥義】PainfulInsanity！");

                            UpdateMainMessage("　　　『アインは集中した眼差しを一度ゲバゲバ君へと向けた！』");

                            UpdateMainMessage("アイン：っしゃ、これで完成だ。");

                            UpdateMainMessage("ラナ：え？一体どういう事よ。");

                            UpdateMainMessage("アイン：今、そこのゲバゲバ君は心の中で格闘中だ。");

                            UpdateMainMessage("ラナ：誰とよ？");

                            UpdateMainMessage("アイン：俺の眼差しと格闘中だ！");

                            UpdateMainMessage("ラナ：その間、アイン自身は自由に動けるの？");

                            UpdateMainMessage("アイン：ああ、あくまでもゲバゲバ君の心の中に眼差しを宿らせた。俺は動けるぜ。");

                            UpdateMainMessage("　　　『ゲバゲバ君の動きが鈍ってきた。ダメージが入っているようだ。』");

                            UpdateMainMessage("アイン：ちなみにこの眼差しは俺が死亡するかゲバゲバ君が死ぬまでは絶対に解除できねえ。");

                            UpdateMainMessage("ラナ：かなりネチっこいスキルね。でも強力だわ。");

                            UpdateMainMessage("アイン：発動のためのスキル量は結構食うが、一度決まれば毎ターンダメージだ。");

                            UpdateMainMessage("　　　『ゲバゲバ君の動きが止まった。どうやら天に召されたようだ。』");

                            UpdateMainMessage("アイン：ゲバゲバ君、すまねえな。リザレクション！");

                            UpdateMainMessage("ラナ：うわ！何復活させてんのよ！？良いじゃないのよ、そんなの。");

                            UpdateMainMessage("アイン：何言ってる、見た目は気持ち悪いがコイツは無害だ。");

                            UpdateMainMessage("ラナ：んまあしょうがないか・・・アイン、心眼系スキルでダメージは珍しいと思うわ。");

                            UpdateMainMessage("アイン：ああ、しかも永続ダメージだからな、ボス戦なんかでは結構使えるぜ！任せておきな！");

                            md.Message = "＜アインはペインフル・インサニティを習得しました＞";
                            md.ShowDialog();
                            mc.PainfulInsanity = true;
                        }
                        if (mc.Level >= 37 && !mc.CelestialNova)
                        {
                            UpdateMainMessage("アイン：やっとＬＶ３７だ。ここまで来ればＭＡＸ４０まであとわずかだ。");

                            UpdateMainMessage("ラナ：アインってさ、ホンットダメージ系が多いわよね。");

                            UpdateMainMessage("アイン：ああ、そうだな。");

                            UpdateMainMessage("ラナ：何でダメージ系にこだわるのよ？");

                            UpdateMainMessage("アイン：そりゃあ、ダメージが当たる方が強いだろ。");

                            UpdateMainMessage("ラナ：何でダメージが当たる方が強いのよ？");

                            UpdateMainMessage("アイン：ダメージが当たればライフが減る。当然その方が強いだろ。");

                            UpdateMainMessage("ラナ：ダメージ以外で強いのもあるとは思わない？");

                            UpdateMainMessage("アイン：最終的にはダメージ系だろ。というわけでだ、思いついたのがコレだ。");

                            UpdateMainMessage("ラナ：無理やり終了させたわね・・・どんなのよ？");

                            UpdateMainMessage("アイン：ダメージってのは攻撃だと思われがちだが、ライフ回復もある意味ダメージだ。");

                            UpdateMainMessage("アイン：ライフ回復はダメージ攻撃の反対みたいなものだからな。");

                            UpdateMainMessage("アイン：戦況によっては、攻撃で潰した方が良い場合もある。");

                            UpdateMainMessage("アイン：また他の戦況では、ダメージレースの中で回復に徹した方が良い時もある。");

                            UpdateMainMessage("ラナ：まあ場合によって回復したい時と攻撃したい時は違うわね。");

                            UpdateMainMessage("アイン：と、いうわけだ。この魔法はCelestialNovaと名付けるぜ！");

                            UpdateMainMessage("アイン：ラナ、っほらよ！");

                            UpdateMainMessage("ラナ：あ、ライフ回復ね。ありがと♪");

                            UpdateMainMessage("アイン：同じ魔法をそこの『ダミー素振り君』にぶつけてみるぜ！");

                            UpdateMainMessage("ラナ：あ、ダメージが入ったわ！　ちょっと何よそれ！？");

                            UpdateMainMessage("アイン：手首で空中を切る時の向きが肝心なんだ。横へスライドさせる方が回復。");

                            UpdateMainMessage("アイン：空中を切る際、上空へ突き上げる方向へスライドさせるのがダメージってわけだ。");

                            UpdateMainMessage("アイン：ついでにだな。この魔法を打つ瞬間、体の重心を下にしておけばダメージ体制にしやすい。");

                            UpdateMainMessage("アイン：逆に両足のうち、逆利きの足を前衛に出しておけば、回復側へシフトしやすい。");

                            UpdateMainMessage("アイン：ってなわけだ。こういう構えからこの魔法を繰り出せば、いつでもどっち側も発動させられるぜ！");

                            UpdateMainMessage("アイン：ッハッハッハッハ！");

                            UpdateMainMessage("ラナ：・・・アイン。");

                            UpdateMainMessage("アイン：どうした！ビビって声も出ねえか！ッハッハッハッハッハ！");

                            UpdateMainMessage("ラナ：アイン・・・今のヴェルゼさんにそっくりよ。");

                            UpdateMainMessage("アイン：ッハッハッハッハ！買いかぶりだ！気にするなって！");

                            UpdateMainMessage("ラナ：そ、そう？そうよね。こんなバカアインに限ってね・・・");

                            md.Message = "＜アインはセレスティアル・ノヴァを習得しました＞";
                            md.ShowDialog();
                            mc.CelestialNova = true;
                        }
                        if (mc.Level >= 38 && !mc.LavaAnnihilation)
                        {
                            UpdateMainMessage("アイン：ＬＶ・・・３８だ！！！");

                            UpdateMainMessage("ラナ：やけに気合入ってるわね。既に何か閃いているわけ？");

                            UpdateMainMessage("アイン：ああ、当然閃いている。");

                            UpdateMainMessage("ラナ：最初から閃いているって事は・・・ダメージ系っぽいわね。");

                            UpdateMainMessage("アイン：ああ！しかし今回のはただのダメージ系じゃねえんだ！");

                            UpdateMainMessage("アイン：おおおおぉぉぉ！！！いくぜ！！！　LavaAnnihilation！！！");

                            UpdateMainMessage("　　　『ゴゴッゴォォオォ！！シュヴヮアアアァァァン！！！』　　");

                            UpdateMainMessage("アイン：どうだ、見ろ！ラナ！焼け野原状態の完成だ！！！");

                            UpdateMainMessage("ラナ：ダンジョンゲートの裏に野原は無いわよ。しかし無茶苦茶ね。");

                            UpdateMainMessage("アイン：ターゲットとか対象とか相手とか関係ねえ、全部焼き尽くすぜ！");

                            UpdateMainMessage("ラナ：ダンジョン内では一体づつ相手にするのに・・・ホンット派手好きね。");

                            UpdateMainMessage("アイン：・・・しまったああぁ！そうだった！！一体づつしか出てこねえじゃん！？");

                            UpdateMainMessage("ラナ：何だ、忘れてたのね。やっぱバカアインはバカでしかないわね♪");

                            UpdateMainMessage("アイン：やっぱ魔法コスト削減で対象とって良いか？MiniAnnihilationとか駄目か？");

                            UpdateMainMessage("ラナ：駄目なんじゃない？もう命名しちゃったワケだし。");

                            UpdateMainMessage("アイン：くっそおおぉぉ・・・コストがかかり過ぎなんだつうの。ええい・・・");

                            UpdateMainMessage("アイン：ウオォラァァァ！！燃えろ燃えろ！！ッハッハッハッハッハ！！！");

                            UpdateMainMessage("ラナ：どっかの三流ボスみたいな事してないで、残りマナには注意してよね。");

                            UpdateMainMessage("アイン：っく、了解了解・・・");

                            md.Message = "＜アインはラヴァ・アニヒレーションを習得しました＞";
                            md.ShowDialog();
                            mc.LavaAnnihilation = true;
                        }
                        if (mc.Level >= 39 && !we.AlreadyLvUpEmpty15)
                        {
                            UpdateMainMessage("アイン：ＬＶ３９・・・ＭＡＸまであと一つだ。");

                            UpdateMainMessage("アイン：ラナ、今日さ、戻ったら飯を一緒に食べようぜ。");

                            UpdateMainMessage("ラナ：何食べようか？");

                            UpdateMainMessage("アイン：【極麺・犀龍寺】はどうだ？あそこの麺は上手いぜ。");

                            UpdateMainMessage("ラナ：私麺系はあんまり好みじゃないのよ。他にない？");

                            UpdateMainMessage("アイン：【エステ・メルンザ・アスペランテ】はどうだ？");

                            UpdateMainMessage("ラナ：あそこのニコニコパフェは美味しいわ♪ 行きましょ♪");

                            UpdateMainMessage("アイン：・・・ハハ。　悪くねえかもな。こういうのも");

                            UpdateMainMessage("ラナ：やけに諦めが早かったじゃない。");

                            UpdateMainMessage("アイン：まあな。前回でさすがに無理だってのも理解したし、切り替えないとな。");

                            UpdateMainMessage("ラナ：アインは普段は外で食べないの？");

                            UpdateMainMessage("アイン：いや、たまに外で食べるけどな。それがどうした？");

                            UpdateMainMessage("ラナ：ううん、別に何でも。");

                            UpdateMainMessage("アイン：相変わらず引っかかる言い方だな。まあ良いけどな。");

                            UpdateMainMessage("ラナ：もし、私が行きたい所。");

                            UpdateMainMessage("アイン：ん？");

                            UpdateMainMessage("ラナ：私が行きたい所、言ったら、アインも来る？");

                            UpdateMainMessage("アイン：ん？おお、ウマイ所ならどこでも良いぜ！");

                            UpdateMainMessage("ラナ：ッフフ、変なアインね。");

                            UpdateMainMessage("アイン：っな！どこが変なんだよ！？");

                            UpdateMainMessage("ラナ：良いからいいから、っささ、じゃあ行きましょ♪");

                            md.Message = "＜アインは何も習得できませんでした＞";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty15 = true;
                        }
                        if (mc.Level >= 40 && !mc.EternalPresence)
                        {
                            UpdateMainMessage("アイン：おおおおぉっしゃ！ＭＡＸ４０だぜ！！");

                            UpdateMainMessage("ラナ：アイン、ＭＡＸＬＶ制覇おめでとう。");

                            UpdateMainMessage("アイン：ああ、ここまで来れたのは純粋に嬉しく思うぜ。");

                            UpdateMainMessage("アイン：俺なりに、いろいろ閃いてきたが、ラストは俺らしく行くぜ。");

                            UpdateMainMessage("ラナ：フィニッシュブローとかじゃないでしょうね？");

                            UpdateMainMessage("アイン：いいや、最後はダメージじゃねえ。パラメタＵＰだ。");

                            UpdateMainMessage("ラナ：どういう上げ方をするつもりよ？");

                            UpdateMainMessage("アイン：ラナ、俺たちは普段、手加減して戦っている。と言ったら理解できるか？");

                            UpdateMainMessage("ラナ：え？そんな事は無いでしょ。私のライトニングキックは本気よ♪");

                            UpdateMainMessage("アイン：まあ・・・ソコは手加減してくれよ。っな、マジで。");

                            UpdateMainMessage("アイン：対象が何であれ、人が行動を起こす場合、ある程度周囲に対する気遣いってのがある。");

                            UpdateMainMessage("アイン：だが、俺たちはそれを認識はしてねえ。あくまでも勝手にそうしてるだけだ。");

                            UpdateMainMessage("ラナ：そういう気遣いを無くすっていうの？");

                            UpdateMainMessage("アイン：そうじゃねえ。俺たちが行動している法則の基点を変えるって話だ。気遣いは当然あるが。");

                            UpdateMainMessage("アイン：『誠意』のような行動原理も含める。と言えば通じるか？");

                            UpdateMainMessage("ラナ：んまあ、良くワカンナイけどさ。やってみせてよ。");

                            UpdateMainMessage("アイン：じゃあ、ちょっとやってみるぜ・・・命名はそうだな。　EternalPresenceだ！！");

                            UpdateMainMessage("　　　『アインの周りに新しい法則と原理が発生し始めた！！！』　　");

                            UpdateMainMessage("アイン：っしゃ、コイツはすげえぜ。ランディのボケもこれを使ってたんだろうな。");

                            UpdateMainMessage("アイン：攻撃力、防御力、魔法攻撃力、魔法防御力ＵＰだ。");

                            UpdateMainMessage("ラナ：凄いわね・・・何だか少しだけランディスお師匠さんに似てるわ、確かに。");

                            UpdateMainMessage("ラナ：そこに立ってるだけで、『気持ちよくぶっ潰す♪』感が伝わってくるわね。");

                            UpdateMainMessage("アイン：い、いやいや。別にそういうわけじゃねえけどな。");

                            UpdateMainMessage("アイン：これなら、初めてあのボケ師匠に五分で渡り合えそうな気もするぜ。");

                            UpdateMainMessage("ラナ：うん、イケるわよきっと。このダンジョン制覇したらお師匠さんに会ってみたら？");

                            UpdateMainMessage("アイン：ああ、そうだな。待ってろろ、クソボケランディ。次こそてめえは潰すぜ！");

                            md.Message = "＜アインはエターナル・プリゼンスを習得しました＞";
                            md.ShowDialog();
                            mc.EternalPresence = true;
                        }
                    }
                }
                #endregion
                #region "ラナのレベルアップ"
                if (sc != null)
                {
                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.Manual;
                        md.Location = new Point(this.Location.X, this.Location.Y + this.Size.Height);
                        md.NeedAnimation = true;

                        if (sc.Level >= 3 && !sc.IceNeedle)
                        {
                            UpdateMainMessage("ラナ：やったわよ、アイン。ＬＶ３になったわ。");

                            UpdateMainMessage("アイン：おお、やるじゃねえか。グラッツ！");

                            UpdateMainMessage("ラナ：私がとりあえず閃くのは魔法からにするわ。");

                            UpdateMainMessage("アイン：ああ、期待してるぜ。見せてくれよ。");

                            UpdateMainMessage("ラナ：本来は水属性だけど、氷から来るイメージよ。“串刺し”にしてあげるわ。");

                            UpdateMainMessage("アイン：あ・・・あぁ・・・");

                            UpdateMainMessage("ラナ：この氷の刃で“激痛”を味わうが良いわ、 Ice Needle！！");

                            UpdateMainMessage("　　　『ガカカカカカ！　シュドドド！！』　　");

                            UpdateMainMessage("アイン：ラ、ラナ。あんまり過激な発言は控えろよな、オマエ一応女性だぞ・・・");

                            UpdateMainMessage("ラナ：良いじゃない♪　アインなんかぶっ潰してやるわ！！！　そうイメージしたの♪");

                            UpdateMainMessage("アイン：何かすげぇ楽しげに言うよな。リアルで怖いじゃないか。ッハッハッハ・・・");

                            UpdateMainMessage("ラナ：大丈夫よ、リアルだから心配しないでね♪");

                            md.Message = "＜ラナはアイス・ニードルを習得しました＞";
                            md.ShowDialog();
                            using (MessageDisplay md2 = new MessageDisplay())
                            {
                                md2.StartPosition = FormStartPosition.Manual;
                                md2.Location = new Point(this.Location.X, this.Location.Y + this.Size.Height);
                                md2.NeedAnimation = true;
                                md2.Message = "＜ラナの魔法使用が解放されました＞";
                                md2.ShowDialog();
                            }
                            sc.AvailableMana = true;
                            sc.IceNeedle = true;
                        }
                        if (sc.Level >= 4 && !sc.CounterAttack)
                        {
                            UpdateMainMessage("ラナ：レベルアップよ。さて、今回は何にしようかな。");

                            UpdateMainMessage("アイン：防御魔法なんてのはどうだ？");

                            UpdateMainMessage("ラナ：直接魔法攻撃も良いけど、スキル攻撃も必要よね。");

                            UpdateMainMessage("アイン：防御の構えをするスキルなんてのはどうだ？");

                            UpdateMainMessage("ラナ：それ！もらったわ♪");

                            UpdateMainMessage("アイン：ッフゥ・・・で、どんな防御なんだ、実際は？");

                            UpdateMainMessage("ラナ：試しにかかってきなさいよ、アイン。");

                            UpdateMainMessage("アイン：良いのか？じゃあいくぜ・・・オラァ！");

                            UpdateMainMessage("ラナ：防御なんてするはず無いじゃない、　Counter Attackよ。　ッハアァ！！");

                            UpdateMainMessage("　　　『ッバコン！！！』（ラナのカウンターがアインのみぞおちに炸裂した）　　");

                            UpdateMainMessage("アイン：ッグ！ッゲエェェェ・・・オ、オマエそれのどこが防衛なんだよ・・・");

                            UpdateMainMessage("ラナ：やったわ！クリーンヒットね♪　さすがアイン、弱い弱い♪♪");

                            UpdateMainMessage("アイン：・・・次ぐらいは防衛的なものにしろよ、っな・・・ッハッハッハ・・・");

                            UpdateMainMessage("　　　【・・・パタッ・・・】（アインは気を失った）　　");

                            md.Message = "＜ラナはカウンター・アタックを習得しました＞";
                            md.ShowDialog();
                            using (MessageDisplay md2 = new MessageDisplay())
                            {
                                md2.StartPosition = FormStartPosition.Manual;
                                md2.Location = new Point(this.Location.X, this.Location.Y + this.Size.Height);
                                md2.NeedAnimation = true;
                                md2.Message = "＜ラナのスキル使用が解放されました＞";
                                md2.ShowDialog();
                            }
                            sc.AvailableSkill = true;
                            sc.CounterAttack = true;
                        }
                        if (sc.Level >= 5 && !sc.DarkBlast)
                        {
                            UpdateMainMessage("ラナ：氷の針、みぞおちカウンター。いろいろ考えられるわね・・・");

                            UpdateMainMessage("アイン：ラナ、よく聞け。落ち着いて聞くんだ。");

                            UpdateMainMessage("ラナ：何よ、今もうスグ閃く所なんだから、邪魔しないでよね。");

                            UpdateMainMessage("アイン：オマエ女性だからな。たまには防御とか考えろよ、っな！？");

                            UpdateMainMessage("ラナ：女性だと防御しなきゃいけないワケ？？");

                            UpdateMainMessage("アイン：い、いやまあそういうワケじゃねえけどさ。何て言うんだ。");

                            UpdateMainMessage("ラナ：あ、決まりね。そうだわ、“漆黒の闇”からの攻撃なんてどう。");

                            UpdateMainMessage("アイン：ま、待て待て！止めろ！俺にそれを向けるな！！");

                            UpdateMainMessage("ラナ：この真っ黒な波動に耐えられるかしら・・・DarkBlastと命名するわ。食らいなさい！");

                            UpdateMainMessage("アイン：う・・・ぬぐああぁぁぁああぁぁ・・・");

                            UpdateMainMessage("ラナ：フフフ、アイン苦しそうねえ。あ！これは快感だわ♪♪♪");

                            UpdateMainMessage("アイン：神よ、俺に安らぎを与えたまえ・・・");
                            md.Message = "＜ラナはダーク・ブラストを習得しました＞";
                            md.ShowDialog();
                            sc.DarkBlast = true;
                        }
                        if (sc.Level >= 6 && !sc.AbsorbWater)
                        {
                            UpdateMainMessage("ラナ：アインはＬＶ６の時は何を閃いたの？");

                            UpdateMainMessage("アイン：俺か？ああ、確かプロテクションだな。物理防御ＵＰするやつさ。");

                            UpdateMainMessage("ラナ：そっか、じゃあ私もたまにはやってみるとするわ。");

                            UpdateMainMessage("アイン：相手がラナに対してウヘヘ（笑）と襲ってくるのをイメージすると良いぞ。");

                            UpdateMainMessage("　　　『ッバキ！！！』（ラナがアインにミドルキックをかました）　　");

                            UpdateMainMessage("アイン：ってぇな！何で俺が蹴られてんだよ。");

                            UpdateMainMessage("ラナ：うるさい、黙れ、バカアイン。　イメージすると怖すぎじゃないホント・・・");

                            UpdateMainMessage("アイン：どんなイメージしたんだよ・・・まあそれは置いといて、どうだ？");

                            UpdateMainMessage("ラナ：水の女神が大きな球体で優しく包み込んでくれるのはどう？　AbsorbWaterよ。");

                            UpdateMainMessage("アイン：予想以上に随分ときれいなイメージだな。どんな効果だ？");

                            UpdateMainMessage("ラナ：遠距離から来るエネルギーを吸収してくれる。つまり魔法防御ね。");

                            UpdateMainMessage("アイン：やれば出来るじゃないか。大したもんだな。");

                            UpdateMainMessage("ラナ：と、当然じゃない、このぐらい。大して凄くも無いわよ。");

                            UpdateMainMessage("アイン：いや、その調子で頑張れよ。期待してるぜ！");

                            UpdateMainMessage("ラナ：え、ええ、たまには防護してあげるからね。");

                            md.Message = "＜ラナはアブソーブ・ウォータを習得しました＞";
                            md.ShowDialog();
                            sc.AbsorbWater = true;
                        }
                        if (sc.Level >= 7 && !sc.StanceOfFlow)
                        {
                            UpdateMainMessage("ラナ：レベルアップ、レベルアップっと♪");

                            UpdateMainMessage("アイン：ナイスだ、ラナ。次は何を閃くつもりだ？");

                            UpdateMainMessage("ラナ：もっと戦術に幅が欲しいのよね。こう『柔』のイメージよ。");

                            UpdateMainMessage("アイン：柔らかい戦術か？よくわかんねえけどな。");

                            UpdateMainMessage("ラナ：負けるが勝ちってあるじゃない。StanceOfFlowと呼ぶ事にするわ。");

                            UpdateMainMessage("アイン：負けたら、負けだろ？");

                            UpdateMainMessage("ラナ：必ず後攻を取るようにするわ。そうすれば、相手の出方次第になる。");

                            UpdateMainMessage("アイン：後攻なんか取ってたら、負けだろ？");

                            UpdateMainMessage("ラナ：必ずしもそうじゃないって話よ、後手必勝とかあるでしょ？");

                            UpdateMainMessage("アイン：いや・・・まあ、負けだろ？");

                            UpdateMainMessage("ラナ：・・・アインなんか・・・嫌い");

                            UpdateMainMessage("アイン：うぉ！！　い、いや、いやいやいやいや！　悪かった！！！");

                            UpdateMainMessage("ラナ：後手必勝の策、絶対に構築して見せるから、見てなさいよホント。");

                            UpdateMainMessage("アイン：わ、分かった！楽しみにしてるぜ！ッハッハッハッハ！！！");

                            md.Message = "＜ラナはスタンス・オブ・フローを習得しました＞";
                            md.ShowDialog();
                            sc.StanceOfFlow = true;
                        }
                        if (sc.Level >= 8 && !we.AlreadyLvUpEmpty21)
                        {
                            UpdateMainMessage("ラナ：・・・そうね、この方が良いかしら。・・・う～んやっぱダメか・・・");

                            UpdateMainMessage("アイン：何やってんだ、ラナ？");

                            UpdateMainMessage("ラナ：何って後手必勝作戦よ。あ、言っておくけどアインには関係ないからね。");

                            UpdateMainMessage("アイン：い、いやいや、レベルアップ時の閃きはどうすんだ？");

                            UpdateMainMessage("ラナ：パスよ。");

                            UpdateMainMessage("アイン：い、いやいや、パスって・・・");

                            UpdateMainMessage("ラナ：パスって言ってるじゃないアッチ行っててよ！！バカじゃないの！？");

                            UpdateMainMessage("アイン：あ、あぁ・・・わりぃな");

                            UpdateMainMessage("アイン：しまった、後攻取りの件で怒らせちまった・・・何とかしねぇと。");

                            md.Message = "＜ラナは何も習得できませんでした＞";
                            md.ShowDialog();
                            sc.EmotionAngry = true;
                            we.AlreadyLvUpEmpty21 = true;
                        }
                        if (sc.Level >= 9 && !sc.ShadowPact)
                        {
                            UpdateMainMessage("ラナ：レベルアップね。ＬＶ９では何にしようかしら。");

                            UpdateMainMessage("アイン：そういや、闇属性っての意外だよな。");

                            UpdateMainMessage("ラナ：あらそう？意外って事も無いでしょ。");

                            UpdateMainMessage("アイン：いや、女といや、聖なる女神とかそういうイメージがあるしな。");

                            UpdateMainMessage("ラナ：何それ・・・アインってヘンタイなの？女の子は意外と闇を抱える生物なのよ♪");

                            UpdateMainMessage("アイン：そんなものなのか？すまねえ、俺にはサッパリだが。");

                            UpdateMainMessage("ラナ：ま、アインには分からなくていいわよ。そうね、今回は闇の力を借りるとするわ。");

                            UpdateMainMessage("ラナ：暗闇の中に魔法力は宿るものよ。ShadowPact、決まりね。");

                            UpdateMainMessage("アイン：何だそのあからさまに怪しいネームは・・・");

                            UpdateMainMessage("ラナ：魔法攻撃力を上げるものね。闇属性としてはおそらく一番使い勝手が良いわ。");

                            UpdateMainMessage("アイン：おまえのIceNeedleやDarkBlastの威力が上がるって事か？");

                            UpdateMainMessage("ラナ：正解よ♪");

                            UpdateMainMessage("アイン：・・・おっと、用事を思い出したぜ。じゃ、じゃあな！ッハッハッハ！");

                            UpdateMainMessage("ラナ：アイン、っちょ、試し打ちさせなさいよ。ッハァァァァ！！IceNeedle！！！");

                            md.Message = "＜ラナはシャドウ・パクトを習得しました＞";
                            md.ShowDialog();
                            sc.ShadowPact = true;
                        }
                        if (sc.Level >= 10 && !sc.DispelMagic)
                        {
                            UpdateMainMessage("ラナ：よし、ついにＬＶ１０よ。長かったわ♪");

                            UpdateMainMessage("アイン：やったじゃねえか！さあ、記念に一つ閃いてみせろよ。");

                            UpdateMainMessage("ラナ：ええ、昔から考えてた【空（くう）】の概念よ。今それを実現させてみせるわ。");

                            UpdateMainMessage("アイン：【空（くう）】？？？カラッポ・・・何も無いって意味か？");

                            UpdateMainMessage("ラナ：そうよ、元々は何も無いの。全て無いのが一番の始まりだと思わない？");

                            UpdateMainMessage("アイン：何言ってんだラナ、何かオカシイぞ？今日のセリフ。");

                            UpdateMainMessage("ラナ：アインには・・・分かりっこないわよ。じゃあ、行くわよ。Dispel Magic！");

                            UpdateMainMessage("アイン：特に、何も変化は無いが？");

                            UpdateMainMessage("ラナ：あんたバカじゃない？わかるわけないでしょ。そうねじゃプロテクションやっておけば？");

                            UpdateMainMessage("アイン：ま・・・まさか！！！");

                            UpdateMainMessage("ラナ：ハイハイ、良いからやってみなさいよ♪");

                            if (mc.Protection)
                            {
                                UpdateMainMessage("アイン：プロテクション！");
                            }
                            if (mc.FlameAura)
                            {
                                UpdateMainMessage("アイン：フレイム・オーラ！");
                            }
                            if (mc.HeatBoost)
                            {
                                UpdateMainMessage("アイン：ヒート・ブースト！");
                            }
                            if (mc.SaintPower)
                            {
                                UpdateMainMessage("アイン：セイント・パワー！");
                            }
                            if (mc.WordOfLife)
                            {
                                UpdateMainMessage("アイン：ワード・オブ・ライフ！");
                            }
                            if (mc.HighEmotionality)
                            {
                                UpdateMainMessage("アイン：ハイ・エモーショナリティ！");
                            }
                            if (mc.WordOfFortune)
                            {
                                UpdateMainMessage("アイン：ワード・オブ・フォーチュン！");
                            }
                            if (mc.Glory)
                            {
                                UpdateMainMessage("アイン：グローリー！");
                            }
                            if (mc.AetherDrive)
                            {
                                UpdateMainMessage("アイン：エーテル・ドライブ！");
                            }
                            if (mc.EternalPresence)
                            {
                                UpdateMainMessage("アイン：エターナル・プリゼンス！");
                            }

                            UpdateMainMessage("ラナ：無にかえりなさい・・・それ、Dispel Magicよ。");

                            UpdateMainMessage("　　　『ッビ、ッパシュ！！・・・』（アインを包み込んでいた効果が全て消えた）");

                            UpdateMainMessage("アイン：見事に全部消えたじゃねえか！何だそれ！！");

                            UpdateMainMessage("ラナ：フフ、どーだ、まいったか♪");

                            UpdateMainMessage("アイン：ラナ！消すとか無とか、そういうのは止めておけ！！");

                            UpdateMainMessage("ラナ：っな、何よ。怒っちゃってさ。せっかく編み出したんだから良いじゃない。");

                            UpdateMainMessage("アイン：ま、まあ・・・そうだけどな・・・何だろうな。");

                            UpdateMainMessage("ラナ：まあ普段はあまり役に立たないのは確かね。気をつけて使うとするわ。");

                            UpdateMainMessage("アイン：ああ、どうしても相手が強い効果がかかり過ぎてる時だけにしとけよ。");

                            UpdateMainMessage("ラナ：ええ、分かったわ。");

                            md.Message = "＜ラナはディスペル・マジックを習得しました＞";
                            md.ShowDialog();
                            sc.DispelMagic = true;
                        }
                        if (sc.Level >= 11 && !sc.LifeTap)
                        {
                            UpdateMainMessage("ラナ：私もようやくＬＶが２桁よ。ッフフ、どこから上げようかしら♪");

                            UpdateMainMessage("アイン：おいラナ。お前はこのタイミングで『習得せず』は無いのか？");

                            UpdateMainMessage("ラナ：無いでしょ。アインじゃあるまいし。");

                            UpdateMainMessage("アイン：っち・・・俺とはタイミングが違うって事か。");

                            UpdateMainMessage("ラナ：まあまあ気にしないの。・・・そうね、今回も闇のイメージがするわ。");

                            UpdateMainMessage("アイン：またかよ！？お得意の水属性じゃねえのか？");

                            UpdateMainMessage("ラナ：そうね、変換、吸収、取引が闇だから・・・LifeTapなんてどう？");

                            UpdateMainMessage("アイン：取引だと？まあ確かに、ラナはよくそういうの好きだよな。");

                            UpdateMainMessage("ラナ：マナと引き換えにライフ回復するの♪");

                            UpdateMainMessage("アイン：ッゲ、お前までライフ回復できるようになるのかよ。");

                            UpdateMainMessage("ラナ：あら、闇でもライフ回復ぐらいよくある事よ。");

                            UpdateMainMessage("アイン：まあそうだけどな。");

                            UpdateMainMessage("ラナ：これでアインが私に気遣わなくても思う存分攻撃できるわよ。");

                            UpdateMainMessage("アイン：いや、でも危ない時は言えよな、ちゃんとヒールしてやっからさ。");

                            UpdateMainMessage("ラナ：ええ、分かったわ。");

                            md.Message = "＜ラナはライフ・タップを習得しました＞";
                            md.ShowDialog();
                            sc.LifeTap = true;
                        }
                        if (sc.Level >= 12 && !sc.PurePurification)
                        {
                            UpdateMainMessage("ラナ：ＬＶ１２ってとこね。っさて、何にしようかしら。");

                            UpdateMainMessage("アイン：そこのラナさん、そろそろ癒し系では？");

                            UpdateMainMessage("ラナ：何よ、その癒し系って？");

                            UpdateMainMessage("アイン：いや・・・癒し系ってのはつまり・・・");

                            UpdateMainMessage("ラナ：ワケ分かんない単語使わないでよ。う～ん、そうね。");

                            UpdateMainMessage("ラナ：純粋なる清浄とかはどう？ 内なる自己治癒を高めるの。");

                            UpdateMainMessage("      『ラナの体がほのかに薄青白く綺麗に光り始めた。』");

                            UpdateMainMessage("アイン：お・・・おおおぉぉ・・・");

                            UpdateMainMessage("ラナ：ふう、っよしこんな感じね♪　PurePurificationでどう？");

                            UpdateMainMessage("アイン：どんな効果なんだ？");

                            UpdateMainMessage("ラナ：今とても気分が良いわ。負の影響がかかってるのが全部取り払われる感じね。");

                            UpdateMainMessage("アイン：・・・しかし、180度変わって、物凄く癒し系だったな。");

                            UpdateMainMessage("ラナ：何かその単語止めてくれない？せっかく良い気分なのに、妙にイライラするわね♪");

                            UpdateMainMessage("アイン：わ、わかったわかった。じゃあ、その・・・綺麗だぞ、ラナ。");

                            UpdateMainMessage("ラナ：っな・・・何で褒め殺しなのよ！　死ね！バカアイン！！");

                            UpdateMainMessage("　　　『ッバゴオオォォン！！！』（ラナのエレクトロキックがアインに炸裂した）　　");

                            md.Message = "＜ラナはピュア・ピュリファイケーションを習得しました＞";
                            md.ShowDialog();

                            sc.PurePurification = true;
                        }
                        if (sc.Level >= 13 && !sc.EnigmaSence)
                        {
                            UpdateMainMessage("ラナ：ようやくＬＶ１３よ。ここは何か一つ奇抜なのを考えてみるわ。");

                            UpdateMainMessage("アイン：奇抜って言っても、あんまり変なのは使わずじまいになるぜ。");

                            UpdateMainMessage("ラナ：そうよね・・・でも、前々から感じていた感覚だけど。");

                            UpdateMainMessage("アイン：何だ？言ってみろよ。");

                            UpdateMainMessage("ラナ：攻撃って力が全てじゃない。そう思わない？");

                            UpdateMainMessage("アイン：いいや、力こそ全てだろ。");

                            UpdateMainMessage("ラナ：うーん・・・今の私の力・技・知のうち、パラメタで最大の値は装備品効果合わせて");

                            int maxValue = Math.Max(sc.StandardStrength, Math.Max(sc.StandardAgility, sc.StandardIntelligence));
                            if (maxValue == sc.StandardStrength)
                            {
                                UpdateMainMessage("ラナ：力、" + maxValue.ToString() + "が一番高いわけね。");
                            }
                            else if (maxValue == sc.StandardAgility)
                            {
                                UpdateMainMessage("ラナ：技、" + maxValue.ToString() + "が一番高いわけね。");
                            }
                            else
                            {
                                UpdateMainMessage("ラナ：知、" + maxValue.ToString() + "が一番高いわけね。");
                            }

                            UpdateMainMessage("アイン：なるほどな。それがどうかしたのか？");

                            UpdateMainMessage("ラナ：一番高い値で攻撃できるって凄いと思わない？");

                            UpdateMainMessage("アイン：いや、そんなのはダメだろ。あまりにも発想が謎すぎるぞ。");

                            UpdateMainMessage("ラナ：ッフフ、何か出来そうなのよね、良く分かんないからEnigmaSenceとでも名付けるわ。");

                            UpdateMainMessage("アイン：マジかよ！？アリなのかよ、そんなの！！力こそが全てだろ！？");

                            UpdateMainMessage("ラナ：良いじゃない、魔法力ＵＰの知が攻撃にも出来る。これって最高のセンスよ♪");

                            UpdateMainMessage("アイン：おいおい、冗談だろ・・・これじゃ力を上げる立場が危ういじゃねえか。");

                            UpdateMainMessage("ラナ：でもスキルポイントを消費するからね。それでおあいこでしょ。");

                            UpdateMainMessage("アイン：おあいこかよ、何か違う気もするけどな。まあ良しとするか。");

                            UpdateMainMessage("ラナ：ッフフ、私もたまにはこれで高いダメージをはじき出して見るわ♪");

                            md.Message = "＜ラナはエニグマ・センスを習得しました＞";
                            md.ShowDialog();

                            sc.EnigmaSence = true;
                        }
                        if (sc.Level >= 14 && !sc.BlackContract)
                        {
                            UpdateMainMessage("ラナ：ＬＶ１４ね。うーん、私もそろそろ考え時かな。");

                            UpdateMainMessage("アイン：年頃ってやつか？全く合わない事するな、ッハッハッハ！");

                            UpdateMainMessage("　　　『ヴォグッシャアアァァ！！！』（ラナのエレメンタルブローがアインに炸裂した）　　");

                            UpdateMainMessage("ラナ：イメージは闇ね。それから何かを犠牲にしながら・・・");

                            UpdateMainMessage("ラナ：物凄く強力な効果を持つのよ。ねぇ、良いと思わない？アイン。");

                            UpdateMainMessage("アイン：・・・・・・");

                            UpdateMainMessage("ラナ：いざと言うとき、ライフを犠牲にしてでも、スキル・魔法を打ちたい放題ってのはどうかしら。");

                            UpdateMainMessage("ラナ：悪魔的な力を感じるわ。ッフフ♪　決まったわ、BlackContractよ！");

                            UpdateMainMessage("ラナ：ッフフ、打ち込み放題って最高だと思わない？ねえ、アイン！　今からぶっ潰すわよ！");

                            UpdateMainMessage("ラナ：DarkBlast！　IceNeedle！　EnigmaSence攻撃！　");

                            UpdateMainMessage("      『ッドス！　ッドス！　ボゴォ！！（アインに3種類ともクリーンヒットした！）』");

                            UpdateMainMessage("アイン：・・・・・・・・・　グォ・・・");

                            UpdateMainMessage("ラナ：ッフフ♪　どうやらこれで終わりみたい。うーん、スッキリしたわ！");

                            UpdateMainMessage("ラナ：あらら、アイン。大丈夫？ゴメンナサイね、付き合ってもらっちゃって。");

                            UpdateMainMessage("アイン：・・・・・・");

                            UpdateMainMessage("ラナ：うーん、寝たきりか・・・何だかつまらないわね・・・っよし、じゃあ");

                            UpdateMainMessage("ラナ：LifeTapで回復。　もう一回BlackContractね。　行くわよアイン♪");

                            UpdateMainMessage("アイン：待て待て待て待て待て！　待った待った待った待った！！");

                            UpdateMainMessage("ラナ：DarkBlast！　IceNeedle！　EnigmaSence攻撃！　");

                            md.Message = "＜ラナはブラック・コントラクトを習得しました＞";
                            md.ShowDialog();

                            sc.BlackContract = true;
                        }
                        if (sc.Level >= 15 && !sc.Cleansing)
                        {
                            UpdateMainMessage("ラナ：ＬＶ１５到達したわ。今回も何か閃くと良いんだけど。");

                            UpdateMainMessage("アイン：お前、この前見せたピュア・・・何だっけ。");

                            UpdateMainMessage("ラナ：PurePurificationよ。ちゃんと覚えてよね。");

                            UpdateMainMessage("アイン：おお、ソレソレ。それは自分自身にしか対象にできないだろ。");

                            UpdateMainMessage("ラナ：そうね、自分自身に対する自己治癒だし。");

                            UpdateMainMessage("アイン：ラナ自身が正常な時に、それを更に他のやつに分け与えられないか？");

                            UpdateMainMessage("ラナ：う～ん、難しそうね。でも、やってみるわ。ちょっと離れてみて。");

                            UpdateMainMessage("ラナ：・・・スウウゥゥ・・・");

                            UpdateMainMessage("ラナ：・・・うん、Cleansingと名づけるわ。ッハイ！");

                            UpdateMainMessage("アイン：うお！・・・おおお！！　何だか気分爽快だぜ！");

                            UpdateMainMessage("ラナ：うん、私の体調が良いときは出来そうよ。グッドアイデアね。");

                            UpdateMainMessage("アイン：これなら多少の負の影響が来ても何とか持ち応えられそうだな。");

                            UpdateMainMessage("ラナ：私もアインも調子が悪くなったら、まず私が治らないと駄目だから注意してよね。");

                            md.Message = "＜ラナはクリーンジングを習得しました＞";
                            md.ShowDialog();

                            sc.Cleansing = true;
                        }
                        if (sc.Level >= 16 && !sc.Negate)
                        {
                            UpdateMainMessage("ラナ：順調にレベルアップよ。");

                            UpdateMainMessage("アイン：そういや、俺はスキル：「心眼」系を一つ習得したが、お前はまだだよな？");

                            UpdateMainMessage("ラナ：イイとこに気付いたわね。私は今回あらかじめイメージしていたのがあるの。");

                            UpdateMainMessage("アイン：どんなんだ？");

                            UpdateMainMessage("ラナ：水には火、光には闇、理には空と、大体反対側に位置する要素ってあるでしょ？");

                            UpdateMainMessage("アイン：っげ・・・まさかラナ。思いついたのってDispelMagicみたいなやつか！？");

                            UpdateMainMessage("ラナ：う～ん少し似てるかもね。でも私って無とか虚構みたいなのに惹かれるのよね。");

                            UpdateMainMessage("アイン：ったく、少しは違う印象のものにしろよな。あんまり良いイメージはねえけどな。");

                            UpdateMainMessage("ラナ：まあ良いじゃない。好みなんて人によって違うんだから、ッホラホラ練習させてよ♪");

                            UpdateMainMessage("アイン：どうすんだよ？");

                            UpdateMainMessage("ラナ：適当に魔法を打ち込んでみてよ。");

                            UpdateMainMessage("アイン：っしゃ、じゃいくぜ・・・オラァ！フレイム・ストライク！！");

                            UpdateMainMessage("      『シュゴオオォォ！！！』　　　");

                            UpdateMainMessage("ラナ：その詠唱スペルへ命令するわ。Negate！");

                            UpdateMainMessage("      『ッバシュウウウゥゥゥン・・・』");

                            UpdateMainMessage("ラナ：やった、消えたわ！　どうやら成功のようね♪");

                            UpdateMainMessage("アイン：マジか・・・まさかとは思うが魔法なら全て消せるって事か？");

                            UpdateMainMessage("ラナ：う～ん、とりあえず全部いけそうだけど、やってみない事には分からないわ。");

                            UpdateMainMessage("アイン：DispelMagicは付与効果を消す。Negateは詠唱タイミングスペルを消す。とんでもねえな。");

                            UpdateMainMessage("ラナ：でも相手が使ってこない限り、使えないわ。後、そのタイミングじゃないと意味が無いわね。");

                            UpdateMainMessage("ラナ：結構使いどころが難しそうよ。そんな過度な期待はできないわね。");

                            UpdateMainMessage("アイン：しかし、消せるもんは消せるんだよな。デカイ魔法は是非とも止めてくれよ。");

                            UpdateMainMessage("ラナ：ええ、上手く当ててみせるわ♪");

                            md.Message = "＜ラナはニゲイト習得しました＞";
                            md.ShowDialog();
                            sc.Negate = true;
                        }
                        if (sc.Level >= 17 && !sc.FrozenLance)
                        {
                            mainMessage.Text = "ラナ：レベル１７って所ね♪";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ラナ、お前の体・・・";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：バッ、ッバカ！見るな！！アッチ行ってよ！！";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：い、いやいや、そういう意味じゃねえが。で、何か閃きそうか？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：そうね、水属性で鋭い刃でアインを心臓を突き刺す、FrozenLanceってどう♪";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：お前、水属性で何でまた攻撃魔法なんだよ？防衛的なものにしろ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：良いじゃないの、火属性のアインばっかり攻撃じゃツマンナイし。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：まあどっちでも良いけどな。っで、どんな能力なんだ？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：もちろん、直接攻撃に決まってるじゃない。さーて、試してみようかしら♪";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：分かった。分かったからその辺にしておこうな。っな。っな。";
                            ok.ShowDialog();
                            mainMessage.Text = "　　　『ッピシ、ピキキキキィ！ピシャアアーーーーーー！！ドドドドドド！！！』　　";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ッグ、ッグアアアアァァァァ・・・・・・";
                            ok.ShowDialog();
                            md.Message = "＜ラナはFrozenLanceを習得しました＞";
                            md.ShowDialog();
                            sc.FrozenLance = true;
                        }
                        if (sc.Level >= 18 && !sc.RiseOfImage)
                        {
                            UpdateMainMessage("ラナ：私もようやく２０まで後少しって所ね。");

                            UpdateMainMessage("アイン：ラナ、お前も何のかんの言って結構ダメージ系入れてんだな。");

                            UpdateMainMessage("ラナ：そう？バカアインほどじゃないけどね。でも今回は違うわ。");

                            UpdateMainMessage("アイン：どういうのを思いついたんだよ。");

                            UpdateMainMessage("ラナ：心って不思議じゃない？");

                            UpdateMainMessage("アイン：まあな。見えない部分だし、不思議というより当たり前と言う感じだな。");

                            UpdateMainMessage("ラナ：戦闘してる時、いつも集中したいんだけど中々一点に収まらない場合があるのよ。");

                            UpdateMainMessage("ラナ：どうしても集中しないと駄目って時はコレね。RiseOfImageよ。");

                            UpdateMainMessage("アイン：どうなるんだ？集中できるようになるのか？");

                            UpdateMainMessage("ラナ：心のパラメタＵＰよ。へえ・・・結構爽快な気分ね、コレ♪");

                            UpdateMainMessage("アイン：心パラメタＵＰか。何となくお前の顔つきも随分良くなったな。");

                            UpdateMainMessage("ラナ：いつも悪い顔つきで悪かったわね！");

                            UpdateMainMessage("アイン：べ、別にそんなんじゃねえって。何でそんな裏読みしてんだよ。");

                            UpdateMainMessage("ラナ：関係ないじゃないの。まったく少しは考えなさいよ。");

                            UpdateMainMessage("アイン：わ、わかった。まあ、その魔法・・・気分良くなってよかったじゃねえか。");

                            UpdateMainMessage("ラナ：知らないわよ。もう、とにかく心パラメタＵＰは重要だからね。");

                            UpdateMainMessage("アイン：わ、わかった。わかったって・・・ワカリマシタ。");

                            md.Message = "＜ラナはライズ・オブ・イメージを習得しました＞";
                            md.ShowDialog();
                            sc.RiseOfImage = true;
                        }
                        if (sc.Level >= 19 && !we.AlreadyLvUpEmpty22)
                        {
                            UpdateMainMessage("ラナ：まったく、人の気も知らないで・・・ホンットに・・・もう一度");

                            UpdateMainMessage("ラナ：ライズオブイメージ！・・・っとこの後でエニグマセンス！");

                            UpdateMainMessage("アイン：っお、やってるな。ラナ、元気か？");

                            UpdateMainMessage("　　　『ヴォゲッシャアァ！！』（ラナのライズオブライトニングがアインに炸裂）　　");

                            UpdateMainMessage("アイン：っぐ・・・ぐおおおぉぉ・・・何故・・・");

                            UpdateMainMessage("ラナ：知らないわよ！アンタが悪いんでしょ！頭冷やして考えなさいよ！！");

                            UpdateMainMessage("アイン：この理由無き暴挙・・・神よ・・・彼女に清楚な心を・・・");

                            UpdateMainMessage("アイン：　　『ッパタ』");

                            UpdateMainMessage("ラナ：口で『ッパタ』って言っても駄目よ♪　くらいなさい！");

                            UpdateMainMessage("アイン：待て待て待て！何なんだっつうの！？");

                            UpdateMainMessage("ラナ：理由？アインをぶっ潰したいだけよ！行くわよ！！ハアアァァァ・・・");

                            UpdateMainMessage("アイン：待て！あり得ないだろ！？せめて理由ぐらい付けろ！");

                            UpdateMainMessage("　　　『ッド・・・！ッグシャアァ！！』（ラナのエニグマブローがアインに炸裂）　　");

                            UpdateMainMessage("　　　『アインはその場で果てた・・・』");

                            UpdateMainMessage("ラナ：っふー、スッキリした！あー気持ち良いわね、ライズオブイメージ♪");

                            UpdateMainMessage("ラナ：エニグマセンスも普段使ってる能力で攻撃だから余計気持ちいいわ♪");

                            UpdateMainMessage("ラナ：って、あれ？アイン・・・本当に気絶しちゃったかしら。");

                            UpdateMainMessage("ラナ：まいいわ、っささ、爽快になった所でダンジョン行きましょ♪");

                            md.Message = "＜ラナは何も習得できませんでした＞";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty22 = true;
                        }
                        if (sc.Level >= 20 && !sc.Deflection)
                        {
                            UpdateMainMessage("ラナ：やったわ！ＬＶ２０に到達よ♪");

                            UpdateMainMessage("アイン：やるじゃねえか！ラナ。ＬＶ２０はもう決めてあるのか？");

                            UpdateMainMessage("ラナ：ええ、前々からやろうと思ってたのがあるのよ。");

                            UpdateMainMessage("ラナ：アイン、ちょっとそっち側に立ってみて。");

                            UpdateMainMessage("アイン：こっちか？オーケーオーケー。");

                            UpdateMainMessage("ラナ：っささ、かかってきなさい♪");

                            UpdateMainMessage("アイン：お前またこのパターンか・・・カウンターアタックの時とそっくりだな。");

                            UpdateMainMessage("ラナ：そう言ってる間に・・・っそれ、Deflectionよ！");

                            UpdateMainMessage("　　　『ラナの周囲に白い防壁空間が発生した！』");

                            UpdateMainMessage("アイン：ッゲ！何だそれ！？");

                            UpdateMainMessage("ラナ：ッフフ、秘密よ♪");

                            UpdateMainMessage("アイン：っくそう、何なんだ・・・怪しいな。っしゃ！ファイアボールだ！");

                            UpdateMainMessage("ラナ：ッソレね、ニゲイト！");

                            UpdateMainMessage("アイン：ああぁ！っくそ面倒だ。ええい、ストレートスマッシュ食らえ！");

                            UpdateMainMessage("　　　『ッパキーン（白い防壁がアインのストレートスマッシュを返した！』");

                            UpdateMainMessage("アイン：だああ！痛ってええぇぇぇ！！");

                            UpdateMainMessage("ラナ：っそれ、ダークブラスト！");

                            UpdateMainMessage("アイン：うお！？グアアアァ・・・イツツツ");

                            UpdateMainMessage("アイン：何だそれ、物理攻撃反射するのか？");

                            UpdateMainMessage("ラナ：そうよ。バカアインにとっては天敵みたいなものね。");

                            UpdateMainMessage("アイン：っけ、って事は魔法からやらせるように誘導させてるようなモノか。");

                            UpdateMainMessage("ラナ：そうよ。だからニゲイトがタイミングよく当たったって事よ。");

                            UpdateMainMessage("アイン：・・・あーーーー面倒くせええ！　だが、俺はやってみせる！");

                            UpdateMainMessage("ラナ：何言ってるのよもう。反射は一度だけだからね。そこさえ注意すれば良いワケよ。");

                            UpdateMainMessage("アイン：ワザと直接攻撃を一度食らえってか？");

                            UpdateMainMessage("ラナ：嫌なら、何か考えておきなさいよ。");

                            UpdateMainMessage("アイン：・・・面倒くせええぇ！！");

                            md.Message = "＜ラナはデフレクションを習得しました＞";
                            md.ShowDialog();
                            sc.Deflection = true;
                        }
                        if (sc.Level >= 21 && !sc.AntiStun)
                        {
                            UpdateMainMessage("ラナ：ＬＶ２１になった所で、今回は何にしようかな。");

                            UpdateMainMessage("アイン：どうした、閃かないのか？");

                            UpdateMainMessage("ラナ：う～ん、そういうわけじゃないんだけどね。");

                            UpdateMainMessage("アイン：だったら、見せてくれよ。");

                            UpdateMainMessage("ラナ：う～ん、ちょっとアインのは痛そうだし・・・あ、そうだわ。");

                            UpdateMainMessage("ラナ：これこれ。この『ダミー素振り君』を使う事にするわ♪");

                            UpdateMainMessage("アイン：うお！？何だよ。そんなのあったのか！？");

                            UpdateMainMessage("ラナ：アイン知らないの？DUEL向けで良く使われるトレーニングマシンよ。");

                            UpdateMainMessage("アイン：マシン？この何か・・・一撃で壊れそうなのがか？");

                            UpdateMainMessage("ラナ：でも壊された事は無いみたいよ。ええっとスタン効果でダメージ１っと・・・。");

                            UpdateMainMessage("ラナ：あと、攻撃方法・・・直接ね。それから、タイマー５秒後っと。入力完了♪");

                            UpdateMainMessage("ラナ：ええっとそうね、AntiStunとでも名付けるわ。文字通りスタン効果に耐性が付くわ。");
                            
                            UpdateMainMessage("ラナ：５，４，３，２，１・・・");

                            UpdateMainMessage("　　　『　ッポコン☆　』　（ラナの頭にダミー素振り君のスタン攻撃が炸裂した）");

                            UpdateMainMessage("ラナ：っう～ん、大丈夫よ。スタン効果の耐性付与、上出来ね♪");

                            UpdateMainMessage("アイン：なあ・・・何かこう実感が伴わなくねえか？");

                            UpdateMainMessage("ラナ：スタン効果はダメージ量とは直接関係はないからコレで良いのよ。");

                            UpdateMainMessage("アイン：よくわかんねえけどな・・・そのダミー素振り君、何でもできるのか？");

                            UpdateMainMessage("ラナ：いろんなボタンが沢山あるみたい。注意して使ってよね。");

                            UpdateMainMessage("アイン：ちょっと俺もやってみるか・・・何々？・・・ダメージベース３５００。");

                            UpdateMainMessage("アイン：『真ボス【瘴技　インヴィジブル・ハンドレッド・カッター】面白そうだな。オラ！");

                            UpdateMainMessage("　　　『シュガガガ、ッガガガッガゴオオオォォォン！！！』（アインは来世へ飛び立った）");
                            
                            md.Message = "＜ラナはアンチ・スタンを習得しました＞";
                            md.ShowDialog();
                            sc.AntiStun = true;
                        }
                        if (sc.Level >= 22 && !sc.DevouringPlague)
                        {
                            UpdateMainMessage("ラナ：ＬＶ２２よ、順調な伸びね。");

                            UpdateMainMessage("アイン：前のアンチスタンと言い、その前のデフレクションと言い。");

                            UpdateMainMessage("ラナ：そうね、そろそろ前衛的な魔法が欲しいわね。");

                            UpdateMainMessage("アイン：い、いやいや、そういう意味で言ったんじゃねえ。そのままで良いって意味だ。");

                            UpdateMainMessage("ラナ：ほめ言葉、ありがと。そのままの状態でさっそく闇魔法を思いついたわ♪");

                            UpdateMainMessage("アイン：いや、別にそんな流れで闇魔法なんてやらなくて良いぞ、っな？");

                            UpdateMainMessage("ラナ：単なるダメージ魔法だけじゃイマイチよね・・・でもライフ回復はもうあるわけだし。");

                            UpdateMainMessage("ラナ：そうね、ココは闇魔法らしく、ダメージ＋回復でやってみるわ、DevouringPlagueよ。");

                            UpdateMainMessage("アイン：ラナ、ちゃんと用意しておいてやったぞ。『ダミー素振り君』だ。");

                            UpdateMainMessage("アイン：なんと、この『ダミー素振り君』、魔法防御率とかも設定できるみたいぜ。");

                            UpdateMainMessage("アイン：また、一ターンの間は必ず直接攻撃のみを実施すると入力してだな・・・");

                            UpdateMainMessage("アイン：更にライフや直接攻撃量も設定可能だから、ダメージレースなんかやりたければ");

                            UpdateMainMessage("ラナ：いいからいいから、っさ、行くわよ♪♪");

                            UpdateMainMessage("アイン：ッグ・・・ウグオオオォォォ・・・・");

                            md.Message = "＜ラナはデヴォリング・プラグーを習得しました＞";
                            md.ShowDialog();
                            sc.DevouringPlague = true;
                        }
                        if (sc.Level >= 23 && !sc.Tranquility)
                        {
                            UpdateMainMessage("ラナ：ようやくＬＶ２３ね。それは良いんだけど・・・");

                            UpdateMainMessage("アイン：何だ。閃きにくいのか？");

                            UpdateMainMessage("ラナ：う、ううんそうじゃないけど。やっぱり閃く内容って変えられないみたいね。");

                            UpdateMainMessage("アイン：閃きなんてそうそう変えれるもんじゃないだろ、気にするな。");

                            UpdateMainMessage("ラナ：うん、で何を思いついた内容なんだけど。");

                            UpdateMainMessage("アイン：おお、どんなのだよ？");

                            UpdateMainMessage("ラナ：生物って元の状態というのが必ずあるでしょ。");

                            UpdateMainMessage("ラナ：その元の状態に戻す感じよ。Tranquilityと命名するわ。");

                            UpdateMainMessage("　　　『ラナの手元に薄明るい緑色の発光体が発生し始めた』");

                            UpdateMainMessage("アイン：・・・へえ・・・何か随分と・・・きれいな色だな。");

                            UpdateMainMessage("ラナ：行くわよ、アイン。ッハイ！");

                            UpdateMainMessage("アイン：おっおおおぉ・・・？　何ともねえが。");

                            UpdateMainMessage("ラナ：キメ技の時に、一時的に効果を付与するのがあるでしょ？");

                            UpdateMainMessage("アイン：ああ、たまに出てくるよな。そういうの。");

                            UpdateMainMessage("ラナ：そういうのを一旦解除させるような内容なの。ちょっと今の私達じゃわからないかも。");

                            UpdateMainMessage("アイン：まあ、敵がさ。バンバン何か使ってくりゃ、試しに打ってみりゃ良いだろ。");

                            UpdateMainMessage("ラナ：それもそうね、相手が強力な一時強化を使ってきたらやってみるわ。");

                            md.Message = "＜ラナはトランキィリティを習得しました＞";
                            md.ShowDialog();
                            sc.Tranquility = true;
                        }
                        if (sc.Level >= 24 && !sc.MirrorImage)
                        {
                            UpdateMainMessage("ラナ：ＬＶ２４よ、トントン拍子にＵＰできるのって良いわね。");

                            UpdateMainMessage("アイン：なるべくトントンと清楚なイメージでも思い付いてくれ。");

                            UpdateMainMessage("ラナ：何よその無理やりな繋げ方・・・まるで私が清楚じゃないみたいじゃない。");

                            UpdateMainMessage("アイン：い・・・いや・・・");

                            UpdateMainMessage("　　　『シュゴオオオォォン！！』（ラナのファイナリティブローがアインに炸裂）　　");

                            UpdateMainMessage("アイン：おまえ・・・すでに・・・");

                            UpdateMainMessage("ラナ：ほんと失礼よね、いいから見てなさいよ、今から立証してみせるから。");

                            UpdateMainMessage("ラナ：純蒼の女神よ、授けたまえ。蒼の誇り、深蒼の鏡を授けたまえ。");

                            UpdateMainMessage("　　　『ラナの体の周囲に濃い青色の空間壁が現れ始めた。』");

                            UpdateMainMessage("アイン：おお・・・何かすげえ濃い色だな。");

                            UpdateMainMessage("ラナ：いくわよ、MirrorImage！");

                            UpdateMainMessage("アイン：へえ、随分とハッキリした濃い青色の空間壁だな。何に使えるんだ？");

                            UpdateMainMessage("ラナ：ダメージ系に当たる魔法は反射するわ。");

                            UpdateMainMessage("ラナ：でもパラメタＵＰ系やライフ回復などは反射しないみたいよ。");

                            UpdateMainMessage("アイン：なるほどな、そいつはかなり便利だな・・・いやあしかし・・・");

                            UpdateMainMessage("ラナ：しかし・・・何よ？何か実験してみたいわけ？");

                            UpdateMainMessage("アイン：蒼の中に居るラナ・・・見直したぜ。清楚感が出てて綺麗だ。");

                            UpdateMainMessage("ラナ：っえ！？・・・えええぇええぇぇ！？");

                            UpdateMainMessage("ラナ：死ね！バカアイン！！");

                            UpdateMainMessage("　　　『シュゴオオオォォン！！』（ラナのイーグルキックがアインに炸裂）　　");

                            UpdateMainMessage("アイン：・・・な・・・何故・・・");

                            md.Message = "＜ラナはミラー・イメージを習得しました＞";
                            md.ShowDialog();
                            sc.MirrorImage = true;
                        }
                        if (sc.Level >= 25 && !sc.VoidExtraction)
                        {
                            UpdateMainMessage("ラナ：ＬＶ２５、丁度中間ポイントね。");

                            UpdateMainMessage("アイン：ラナ、調子はどうだ。順調か？");

                            UpdateMainMessage("ラナ：うん、良い感じよ。今回はそうね・・・っあ、こんなのどう？");

                            UpdateMainMessage("ラナ：普通は能力を高める際、何らかのイメージを伴う。そう思わない？");

                            UpdateMainMessage("アイン：まあ、ありていに言えばそうだな。");

                            UpdateMainMessage("ラナ：その時ってただ単に集中するのとは違ってて、引き出すような感覚でやるの。");

                            UpdateMainMessage("ラナ：自分が今最も出来そうな事は何か、今最も引き出せそうなのは何か。");

                            UpdateMainMessage("アイン：なるほどな、何となく分かる気がするぜ。");

                            UpdateMainMessage("ラナ：今もてる最高のポテンシャル・・・更に引き出してみるわ・・・VoidExtraction！");

                            UpdateMainMessage("アイン：ぉお！何か、洗練された雰囲気になったな。");

                            int maxValue = Math.Max(sc.StandardStrength, Math.Max(sc.StandardAgility, Math.Max(sc.StandardIntelligence, sc.StandardMind)));
                            if (maxValue == sc.StandardStrength)
                            {
                                UpdateMainMessage("ラナ：今の私の場合だと力、" + maxValue.ToString() + "が引き出された事になるわ。");
                            }
                            else if (maxValue == sc.StandardAgility)
                            {
                                UpdateMainMessage("ラナ：今の私の場合だと技、" + maxValue.ToString() + "が引き出された事になるわ。");
                            }
                            else if (maxValue == sc.StandardIntelligence)
                            {
                                UpdateMainMessage("ラナ：今の私の場合だと知、" + maxValue.ToString() + "が引き出された事になるわ。");
                            }
                            else
                            {
                                UpdateMainMessage("ラナ：今の私の場合だと心、" + maxValue.ToString() + "が引き出された事になるわ。");
                            }


                            UpdateMainMessage("アイン：なるほど、最も高いパラメタが更に上昇するのか・・・");

                            UpdateMainMessage("アイン：って、おいおい！それ十分強すぎじゃねえか！？");

                            UpdateMainMessage("ラナ：そうかしら？こんなもんだと思うけど。");

                            UpdateMainMessage("アイン：やべえ・・・何か・・・俺の方が弱くならねえか？");

                            UpdateMainMessage("ラナ：あら、確かに私の拳武術がこれ以上強くなったら危ないかもね♪");

                            UpdateMainMessage("アイン：くっそう、俺はまだまだやれる！");

                            UpdateMainMessage("ラナ：っふふ、まあ私がたまに手加減してあげるわよ♪");

                            UpdateMainMessage("アイン：本気で来な、ラナ。そんなボンド・エキストラショット怖くも何ともねえからな。");

                            UpdateMainMessage("ラナ：アイン・・・さすがに間違えすぎよ・・・");

                            md.Message = "＜ラナはヴォイド・エクストラクションを習得しました＞";
                            md.ShowDialog();
                            sc.VoidExtraction = true;
                        }
                        if (sc.Level >= 26 && !sc.OneImmunity)
                        {
                            UpdateMainMessage("ラナ：レベルアップレベルアップっと♪");

                            UpdateMainMessage("アイン：何だかヤケに楽しそうじゃねえか。");

                            UpdateMainMessage("ラナ：今日はね、とっておきの秘策。編み出せる気がするのよ♪");

                            UpdateMainMessage("アイン：へえ、自信ありそうな雰囲気だな。おーし、俺が実験台になってやるよ。");

                            UpdateMainMessage("ラナ：ガンツ叔父さんにね、ヒントを教えてもらったのよ。");

                            UpdateMainMessage("アイン：何？くっそ・・・ガンツ叔父さんか、確かな入れ知恵をもらってそうだな。");

                            UpdateMainMessage("アイン：まあいい。　っしゃ！いつでもいいぜ。");

                            UpdateMainMessage("ラナ：じゃあ、やってみるわよ・・・MirrorImageとDeflectionのイメージが基本で・・・");

                            UpdateMainMessage("ラナ：ッハ！　OneImmunity！");

                            UpdateMainMessage("アイン：！？何だ、また何かの防御系か？");

                            UpdateMainMessage("ラナ：ッフウゥ・・・よし、これは凄そうだわ。アイン、防御系なんてものじゃないわよ。");

                            UpdateMainMessage("ラナ：何でも良いわ、好きな攻撃方法をやってみてちょうだい。");

                            UpdateMainMessage("アイン：マジかよ、随分な言いっぷりだな。じゃ遠慮なくやらせてもらうぜ。");

                            UpdateMainMessage("アイン：オラァ！ヴォルカニックウェイブだ！！");

                            UpdateMainMessage("        『シュゴオオオォォォ！ッドオオオォォン！！！』");

                            UpdateMainMessage("アイン：・・・！！！　まさか！！！");

                            UpdateMainMessage("ラナ：どうやら成功のようね。");

                            UpdateMainMessage("ラナ：そうよ、ノーダメージよ。私にはまるで当たってないわ。");

                            UpdateMainMessage("アイン：おいおい、本気かよ。いざ、キメ打ちって時にコレだと、たまったもんじゃないぜ。");

                            UpdateMainMessage("ラナ：でも弱点も沢山あるみたいね。");

                            UpdateMainMessage("ラナ：まず第一。詠唱した時は効果がないの。");

                            UpdateMainMessage("アイン：そうか、まず俺が待ち構える形だったからか。");

                            UpdateMainMessage("ラナ：そして第二。この防御体制を維持しておかないと発揮しないわね。");

                            UpdateMainMessage("ラナ：そして第三。一時的魔法だから、数ターン経つと消えちゃうわ。");

                            UpdateMainMessage("アイン：まあそんな魔法が永続魔法だったら、降参レベルだぜ・・・");

                            UpdateMainMessage("ラナ：物理・魔法ともに完全防御だからこのぐらいの制限はあってもおかしくないって事ね。");

                            UpdateMainMessage("アイン：相手があからさまな攻撃態勢を示したら、やればよさそうだな。");

                            UpdateMainMessage("ラナ：ボス戦ぐらいしか使い道がないかも知れないわね、イザという時使っていくとするわ。");

                            md.Message = "＜ラナはワン・イムーニティを習得しました＞";
                            md.ShowDialog();
                            sc.OneImmunity = true;
                        }
                        if (sc.Level >= 27 && !sc.WhiteOut)
                        {
                            UpdateMainMessage("ラナ：ＬＶ２７だけど・・・何だか調子がイマイチよね。");

                            UpdateMainMessage("アイン：何だどっか悪いとこでもあるのか？");

                            UpdateMainMessage("ラナ：ううん、何でも無いわ。でも閃き感覚もイマイチなのよ。");

                            UpdateMainMessage("アイン：でも癇癪は起こしてねえしな。閃くのは間違いなさそうだが。");

                            UpdateMainMessage("ラナ：な・ん・で・す・っ・て！？");

                            UpdateMainMessage("アイン：はい、はいはい、気のせいでした。ッハハハ・・・");

                            UpdateMainMessage("ラナ：どうもいつもの五感が冴えないのよ。気のせいかしらね・・・");

                            UpdateMainMessage("ラナ：・・・っあ、コレ使えそうね。っよし、バカアインで試して見るわ。");

                            UpdateMainMessage("アイン：ダメージ系は他の対象物にしような？ラナ先生。");

                            UpdateMainMessage("ラナ：私、ホンットこういうのに惹かれるのかも知れないわ。いくわよ、WhiteOut！");

                            UpdateMainMessage("アイン：っう！・・・ウオオォォォ、体が何か・・・いってえええぇぇぇ！！");

                            UpdateMainMessage("ラナ：アインの体に通っている全五感神経にダメージを与えたわ、快感よね♪");

                            UpdateMainMessage("アイン：いっってててててってえぇぇ！！止め止め！タンマ！！アダダダダ！！");

                            UpdateMainMessage("アイン：ったく、耳から頭から目、手足、体中がヒデェ痛みだ。勘弁してくれよ。");

                            UpdateMainMessage("ラナ：ううん・・・何か足りないわね。");
                            
                            UpdateMainMessage("ラナ：よし、もう一回♪");

                            UpdateMainMessage("アイン：イデデデデデデデデデデデ！！！");

                            md.Message = "＜ラナはホワイト・アウトを習得しました＞";
                            md.ShowDialog();
                            sc.WhiteOut = true;
                        }
                        if (sc.Level >= 28 && !sc.BloodyVengeance)
                        {
                            UpdateMainMessage("ラナ：順調にＬＶＵＰって所ね♪");

                            UpdateMainMessage("アイン：ラナさ、おまえ閃く時ってＬＶＵＰ前か？ＬＶＵＰ時か？");

                            UpdateMainMessage("ラナ：う～ん、別にその時の気分次第よ。");

                            UpdateMainMessage("アイン：そうか、じゃあ提案だ。ラナ、今回は心が静まる何かにしろ。");

                            UpdateMainMessage("ラナ：ちょっ、何よそれ。まるで私がいつも荒れるみたいじゃない？");

                            UpdateMainMessage("アイン：ラナ、女らしくなるために、ライトニングキックは撤廃しろ。");

                            UpdateMainMessage("ラナ：へ～、ケンカ腰じゃない♪　よし決めたわ。");

                            UpdateMainMessage("ラナ：いつもより更にも増して最高の力でぶっ潰して上げるわ♪");

                            UpdateMainMessage("ラナ：さあ、行くわよ、覚悟しなさいよね・・・BloodyVengeance！");

                            UpdateMainMessage("        『ラナの拳に今まで見た事も無い力のオーラが凝縮される！』");

                            UpdateMainMessage("アイン：こりゃまた・・・スゲエのが来たな・・・");

                            UpdateMainMessage("ラナ：VoidExtractionとEnigmaSenceのダブル連携よ、ッハアアァァァ！");

                            UpdateMainMessage("アイン：う！うおおぉぉぉお！！！");

                            UpdateMainMessage("ラナ：・・・ふん、結局止めちゃうのね。");

                            UpdateMainMessage("アイン：ラナ、お前どんだけ潜在性を秘めてんだ。マジでやべえって。");

                            UpdateMainMessage("ラナ：でも、アインは今のでさえも止められたのよね。");

                            UpdateMainMessage("ラナ：アイン、私に対していつも手を抜いてない？");

                            UpdateMainMessage("アイン：バカ言え、手は抜いてねえって。");

                            UpdateMainMessage("ラナ：だとしたら、自覚が無いワケね。あーあ、何だか損しちゃった気分ね。");

                            UpdateMainMessage("アイン：いや、いやいや、手抜きしてねえって言ってるだろ？今のだってスレスレだ。");

                            UpdateMainMessage("ラナ：ッフフ、良いわよ。まあ、今度の機会じゃ私も本気だすからね♪");

                            UpdateMainMessage("アイン：ああ、お互い手抜き無しだ！ガンガン来いって。　ッハッハッハ！！");

                            md.Message = "＜ラナはブラッディ・ヴェンジェンスを習得しました＞";
                            md.ShowDialog();
                            sc.BloodyVengeance = true;
                        }
                        if (sc.Level >= 29 && !we.AlreadyLvUpEmpty23)
                        {
                            UpdateMainMessage("ラナ：どうやったら真っ向から打ち崩せるのかしら・・・");

                            UpdateMainMessage("アイン：ラナ、ＬＶＵＰおめでとう。");

                            UpdateMainMessage("ラナ：う～ん・・・コレも駄目そうね・・・っえ？何よアイン。");

                            UpdateMainMessage("アイン：いや、だからおめでとうっと。マズイまさかこの展開は");

                            UpdateMainMessage("ラナ：こう来て・・・ああ、もう！こんなんじゃカウンターで駄目じゃないの！");

                            UpdateMainMessage("アイン：さて、さてさて・・・");

                            UpdateMainMessage("ラナ：ちょっと！！そこのアイン！");

                            UpdateMainMessage("アイン：はい！はいはいはい、何だ何だ！？");

                            UpdateMainMessage("ラナ：カウンターに備えてる相手のカウンターを見破った上で");

                            UpdateMainMessage("ラナ：カウンター同士になったとき、先に先行打撃入れるには");

                            UpdateMainMessage("ラナ：どうすれば良いのよ！？ちゃんと答えなさいよね！！");

                            UpdateMainMessage("アイン：ええっとだな。それはだな。まあ、待て待て待てとりあえず暴力反対。");

                            UpdateMainMessage("アイン：カウンター同士のにらみ合いだろ？よくある事だ。ええっと待て待て待て。");

                            UpdateMainMessage("アイン：そうだな・・・『試しに打ち込んでみる』。どうだ！？ッハッハッハ！");

                            UpdateMainMessage("　　　『ドブッシイィィ！！』（ラナのカウンターライトニングがアインに炸裂）　　");

                            UpdateMainMessage("ラナ：うん、なるほどね♪　理解理解。");

                            UpdateMainMessage("アイン：っく・・・建設的に言っても無駄なのかよ・・・神よ・・・");

                            UpdateMainMessage("ラナ：アイン、ところで一つ聞きたいんだけど。");

                            UpdateMainMessage("アイン：駄目！だめだめだ！　今日はもうここまで！！");

                            UpdateMainMessage("ラナ：何よ。　付き合い悪いわね。");

                            UpdateMainMessage("アイン：俺はな、お前に十分付き合ってると思うぞ？");

                            UpdateMainMessage("ラナ：ホントあと一つだけよ。");

                            UpdateMainMessage("ラナ：ダメージレースの最後、決め手となるスキルは何が良いかしら？");

                            UpdateMainMessage("アイン：ハーイハイハイハイ終了タイム！　また来週！！");

                            UpdateMainMessage("ラナ：何で無理やり終わらせようとしてんのよ！イライラするわね♪♪");

                            UpdateMainMessage("　　　『ゲヴォッシャアアァアァ！！』（ラナのイノセントブローがアインに炸裂）　　");

                            md.Message = "＜ラナは何も習得できませんでした＞";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty23 = true;
                        }
                        if (sc.Level >= 30 && !sc.PromisedKnowledge)
                        {
                            UpdateMainMessage("ラナ：やったわ！ついにＬＶ３０到達よ♪");

                            UpdateMainMessage("アイン：やったじゃねえか！ラナ、おめでとう！");

                            UpdateMainMessage("ラナ：ッフフフ、ありがとアイン♪");

                            UpdateMainMessage("アイン：（まったく普段からこうしてりゃ・・・）　今回は何を閃くつもりだ？");

                            UpdateMainMessage("ラナ：何にするかは大体決めてあるの。水魔法よ。");

                            UpdateMainMessage("アイン：水魔法か、加護・防衛魔法は結構揃って来ているしな、どんなのにするんだ？");

                            UpdateMainMessage("ラナ：魔力の源泉は知力よね。");

                            UpdateMainMessage("ラナ：全ての源、それは水よね。");

                            UpdateMainMessage("ラナ：蒼授天使との契約で最も強力なものになる気がするわ・・・いくわよ。");

                            UpdateMainMessage("ラナ：蒼授天使よ、我に古来より卓越された知識を授けたまえ、PromisedKnowledgeよ。");

                            UpdateMainMessage("アイン：まさか、知力そのものを上げる魔法か？");

                            UpdateMainMessage("ラナ：ご名答♪　これは凄いわね。魔法力自体は当然上がるワケだけど。");

                            UpdateMainMessage("アイン：魔法関連全般が全面的に強化される感じだな。かなり強くねえか？それ。");

                            UpdateMainMessage("ラナ：そうね、これ以外のＢＵＦＦ系ＵＰも強化されると思ってくれれば良いわ。");

                            UpdateMainMessage("アイン：いや、ホントやるじゃねえか。何かラナの方が閃きセンスあるって事か？");

                            UpdateMainMessage("ラナ：でもバカアインだってたまに反則っぽいの思いつくじゃない。今回のは順当よね。");

                            UpdateMainMessage("ラナ：っささ、魔法を使う者にとってはおそらく頻繁に駆使するからね。期待しててちょうだい♪");

                            md.Message = "＜ラナはプロミスド・ナレッジを習得しました＞";
                            md.ShowDialog();
                            sc.PromisedKnowledge = true;
                        }
                        if (sc.Level >= 31 && !sc.SilentRush)
                        {
                            UpdateMainMessage("ラナ：ついに私もＬＶ３１ね。アインはＬＶ３１では、何を閃いてたの？");

                            UpdateMainMessage("アイン：俺か？俺はその時は・・・確か何かスキル系だったな。");

                            UpdateMainMessage("ラナ：スキル系って何となくだけど、一回目の感触が大事だと思わない。");

                            UpdateMainMessage("アイン：まあそうかも知れないな。何だ、スキル系を閃いたのか？");

                            UpdateMainMessage("ラナ：私なりにちょっとしたコンボを思いついたのよ、最初の一発はキメておきたいの。");

                            UpdateMainMessage("アイン：そうか、よしじゃあ俺が初見相手になってやる。かかって来い！");

                            UpdateMainMessage("ラナ：私がイイって言うまで、真剣に構えててよね。っじゃ行くわよ・・・");

                            UpdateMainMessage("アイン：っしゃ！いつでも良いぜ！");

                            UpdateMainMessage("ラナ：スウウゥゥゥ・・・");

                            UpdateMainMessage("アイン：・・・ん？");

                            UpdateMainMessage("　　　『アインはその一瞬だけ、ラナの姿を見失った！』");

                            UpdateMainMessage("アイン：・・・！？っしまっ！！！");

                            UpdateMainMessage("ラナ：食らいなさい、SilentRush！　ヤアァ！");

                            UpdateMainMessage("　　　『ッド！ッズドドン！！』（アインに３発のダメージが入った！）");

                            UpdateMainMessage("アイン：ッチ、っくそが！！　今のどうやったんだ？目の前にいた筈だが。");

                            UpdateMainMessage("ラナ：初期モーションのステップ応用よ。動く前に幻影が残るようにステップを踏むの。");

                            UpdateMainMessage("ラナ：それで、あたかもソコにいたかのような錯覚を与えたまま、懐へ飛び込めるわ。");

                            UpdateMainMessage("アイン：何か結構テクニック使ってそうだな。ラナ、見事に一杯食わされたぜ。");

                            UpdateMainMessage("ラナ：でも準備段階も含めてスキル量は結構使いそうだわ。慎重に使わないと駄目ね。");

                            UpdateMainMessage("アイン：いやしかし気配だけ残して次の行動に移るなんてホントやるじゃねえか。");

                            UpdateMainMessage("ラナ：うん、こういうのはどんどん応用が利くわ。期待していいからね♪");

                            md.Message = "＜ラナはサイレント・ラッシュを習得しました＞";
                            md.ShowDialog();
                            sc.SilentRush = true;
                        }
                        if (sc.Level >= 32 && !sc.CarnageRush)
                        {
                            UpdateMainMessage("ラナ：ＬＶ３２にもなってくると、より一層の閃きが欲しいところね。");

                            UpdateMainMessage("アイン：この前見せてくれた、サイレントラッシュ。中々使い勝手が良さそうだな。");

                            UpdateMainMessage("ラナ：そうね、でもあれはもっと応用が利きそうなんだけどね。");

                            UpdateMainMessage("ラナ：っあ、そうだわ。アインがよくやってるダブル・スラッシュを見せてよ。");

                            UpdateMainMessage("アイン：ん？おお、勿論オーケーだ。じゃ、行くぜ！");

                            UpdateMainMessage("ラナ：っそれそれ、どうやって剣の振り筋を途中で変えているのよ？");

                            UpdateMainMessage("アイン：どう？　どうと言われてもな・・・");

                            UpdateMainMessage("アイン：最初っから途中で変えるつもりでやるって感じだな。気まぐれで変えてるわけじゃねえ。");

                            UpdateMainMessage("アイン：要するに２回当てる事を前提に最初っから必ず変えるつもりって事だ。");

                            UpdateMainMessage("ラナ：なるほどね。わかったわ。ちょっとやってみるから、少し離れて。");

                            UpdateMainMessage("アイン：オーケー。　このぐらいで良いか？");

                            UpdateMainMessage("ラナ：うん、十分よ。じゃあ行くわよ。");

                            UpdateMainMessage("ラナ：・・・　・・・");

                            UpdateMainMessage("アイン：・・・　・・・");

                            UpdateMainMessage("ラナ：・・・　一つ目よ、ッハイ！");

                            UpdateMainMessage("　　　『ッド！』　");

                            UpdateMainMessage("ラナ：二つ目からの連撃、ッセイ！");

                            UpdateMainMessage("ラナ：ヤアァ！");

                            UpdateMainMessage("ラナ：ッフ！");

                            UpdateMainMessage("　　　『ッドドド！』　");

                            UpdateMainMessage("ラナ：最後にトドメ、ッハアアァァ！");

                            UpdateMainMessage("　　　『ズッドオオオォォン！！』　");

                            UpdateMainMessage("　　　『ッバタ・・・』　（アインはその場で倒れた）");

                            UpdateMainMessage("ラナ：やったわ！！綺麗にクリーンヒットしたでしょ？どうだった♪");

                            UpdateMainMessage("アイン：・・・　・・・　・・・");

                            UpdateMainMessage("　　　『・・・ッゲシ』　（ラナの息の根確認キックがアインにヒット）");

                            UpdateMainMessage("アイン：・・・　・・・　・・・");

                            UpdateMainMessage("ラナ：やった・・・これならアインに順調に勝てそうね♪　練習に励もうっと♪");

                            UpdateMainMessage("ラナ：もう１回、一つ目から行くわよ、ッハイ！");

                            md.Message = "＜ラナはカルネージ・ラッシュを習得しました＞";
                            md.ShowDialog();
                            sc.CarnageRush = true;
                        }
                        if (sc.Level >= 33 && !we.AlreadyLvUpEmpty24)
                        {
                            UpdateMainMessage("ラナ：・・・う～ん・・・どうなのかしらねホントのとこ。");

                            UpdateMainMessage("アイン：何がだ？");

                            UpdateMainMessage("ラナ：アイン、ちゃんと答えなさいよ。");

                            UpdateMainMessage("アイン：何がだ？");

                            UpdateMainMessage("ラナ：カルネージラッシュのクリーンヒットよ。");

                            UpdateMainMessage("アイン：いや、すまねえ。マジでK.Oくらって覚えてねえ。");

                            UpdateMainMessage("ラナ：いくらなんでも綺麗に決まりすぎよ。正直に言いなさい。");

                            UpdateMainMessage("ラナ：アイン、私に手加減してるでしょ？");

                            UpdateMainMessage("アイン：手加減って言うと御幣があるな・・・何て言うんだ。");

                            UpdateMainMessage("アイン：ラナに致命傷を与える事はできねえ。たとえDUELであってもだ。");

                            UpdateMainMessage("アイン：だから・・・そのなんだ。手加減はしてねえって。");

                            UpdateMainMessage("ラナ：う～ん、もう分かったわよ。");

                            UpdateMainMessage("ラナ：要するに、アインは私相手だと本気が出てないって事よ。");

                            UpdateMainMessage("アイン：すまねえな。何か悪い気にさせてたか。");

                            UpdateMainMessage("ラナ：良いのよ、私が本気にさせられるレベルじゃない事ぐらい分かってるつもりよ。");

                            UpdateMainMessage("アイン：いやいや、そういう意味じゃねえ。");

                            UpdateMainMessage("ラナ：その「いやいや」って言うの止めなさいよ。");

                            UpdateMainMessage("アイン：まあ・・・分かった。そうだな、確かに本気に撤しきれてねえ。");

                            UpdateMainMessage("アイン：ランディ師匠からすりゃ、俺は誰が相手でも本気になれてねえみたいだ。");

                            UpdateMainMessage("アイン：ラナ、次からはちゃんと本気で相手するぜ。");

                            UpdateMainMessage("ラナ：うん、その調子で来なさいよ。楽しみにしてるわよ♪");

                            md.Message = "＜ラナは何も習得できませんでした＞";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty24 = true;
                        }
                        if (sc.Level >= 34 && !sc.Damnation)
                        {
                            UpdateMainMessage("ラナ：ＬＶ３４ともなると一つ一つ上げるのが大変になってきたわ。");

                            UpdateMainMessage("アイン：この辺からはどんどん厳しくなるからな。で、どうだ？");

                            UpdateMainMessage("ラナ：拮抗状態が生まれた時の秘策を持っておきたいと思わない？");

                            UpdateMainMessage("アイン：拮抗状態か？まあ、にらみ合いになってくると確かに欲しいな。");

                            UpdateMainMessage("ラナ：何もしないままなら、どんどん厳しい状態に陥れたいの。");

                            UpdateMainMessage("ラナ：と言うことで、ココは闇魔法で決まりね♪");

                            UpdateMainMessage("ラナ：そうだわ、なおかつ永続的なものが良いわね。どんどん押し潰されるような");

                            UpdateMainMessage("アイン：おまえはまた、どうしてそんな過激な・・・");

                            UpdateMainMessage("ラナ：良いじゃないの、別に。ッフフ、滅びを迎えると良いわバカアイン、Damnation！");

                            UpdateMainMessage("　　　『アインの周囲に黒い虚空間が発生し始めた！！』");

                            UpdateMainMessage("アイン：うお！？何だよコレ・・・ッグ、グアアァ！");

                            UpdateMainMessage("ラナ：その黒い虚空間はアイン、あなたの体力を徐々に蝕んでいくわ。っさ、逃げられないわよ。");

                            UpdateMainMessage("アイン：確かにこのまとわり憑きでライフが奪われるんじゃ動かざるを得ないな。っい、イッツツツ！");

                            UpdateMainMessage("ラナ：アイン、ごめんなさい。ソレ解除できないみたい。");

                            UpdateMainMessage("アイン：イッツツツ・・・あぁ！？何だと！？");

                            UpdateMainMessage("ラナ：この永続魔法はDispelMagicやCleansingじゃ除去できないようなの。ごめんね♪");

                            UpdateMainMessage("アイン：じゃあ、とっととケリをつけねえと１００％俺が負けちまうじゃねえかよ！");

                            UpdateMainMessage("ラナ：喋っている間に、結構減ったみたいね♪　じゃ、私の決め技でも食らってちょうだい。");

                            UpdateMainMessage("アイン：てめえこんなのアリかよ！？覚えてやがれよ！　イッツツツ！グアアァ！");

                            md.Message = "＜ラナはダムネーションを習得しました＞";
                            md.ShowDialog();
                            sc.Damnation = true;
                        }
                        if (sc.Level >= 35 && !sc.StanceOfDeath)
                        {
                            UpdateMainMessage("ラナ：ＬＶ３５到達っと♪　私もいよいよあと５つと迫ったわ。");

                            UpdateMainMessage("アイン：ラナ、お前の言う『静』のスキルって俺にとっちゃ面倒なのばかりだな。");

                            UpdateMainMessage("ラナ：アインが習得している『動』と私の『静』って『双極』と呼ばれる関係にあるらしいわよ。");

                            UpdateMainMessage("ラナ：だからアインにとっては厄介なのばかりになるのよ。");

                            UpdateMainMessage("アイン：双極？何だそれは、聞いた事がねえな。");

                            UpdateMainMessage("ラナ：まあいっぺんガンツ叔父さんにでも聞いてみると良いわよ。っあ、これもらいね。");

                            UpdateMainMessage("アイン：何がだ？");

                            UpdateMainMessage("ラナ：アインが面倒くさがりそうなスキルっと・・・うん、決定決定♪");

                            UpdateMainMessage("アイン：おい、待て待て。そんな閃き方はねえだろ。");

                            UpdateMainMessage("ラナ：ダメージ系にとって一番厄介なのは、耐性・軽減・吸収・防壁みたいなものよね？");

                            UpdateMainMessage("アイン：まあそうだな、ライフ０になってくれねえとかだったらホント面倒くせえな。");

                            UpdateMainMessage("ラナ：アイン、今日は凄い冴えてるわね。このアイデアは画期的よ、StanceOfDeathと命名するわ。");

                            UpdateMainMessage("アイン：ま、まさか！？そのネーミング、そのまんまじゃねえだろうな！？");

                            UpdateMainMessage("ラナ：そのまさかよ。ライフが０になるダメージの際、瀕死状態で生き残れるようになるの。");

                            UpdateMainMessage("アイン：ダブル・スラッシュで１回目で０、２回目で撃沈するのか？");

                            UpdateMainMessage("ラナ：撃沈しないわよ♪　連続ダメージ系ののスキルは一つと見なされるの。");

                            UpdateMainMessage("アイン：フレイムオーラみたいな追加効果系は？");

                            UpdateMainMessage("ラナ：同じく生き残るわよ♪　一つの攻撃として見なされるわね。");

                            UpdateMainMessage("アイン：ゲイルウィンドで２回行動での連続攻撃は？");

                            UpdateMainMessage("ラナ：生き残るわよ♪　２回行動も２回攻撃も１ターン内では同じ事よ。");

                            UpdateMainMessage("アイン：・・・面倒くせえええええぇぇぇぇ！！！！！");

                            md.Message = "＜ラナはスタンス・オブ・デスを習得しました＞";
                            md.ShowDialog();
                            sc.StanceOfDeath = true;
                        }
                        if (sc.Level >= 36 && !sc.OboroImpact)
                        {
                            UpdateMainMessage("ラナ：やっとＬＶ３６よ。ＬＶ３５から随分と上がり辛くなってるわね。");

                            UpdateMainMessage("アイン：そうだな、こっからはしんどくなるぜ。");

                            UpdateMainMessage("ラナ：でも経験値を稼いでる間に、いろいろと考えたわ。");

                            UpdateMainMessage("ラナ：私、ガンツ叔父さんにいろいろ教えてもらって本当に良かったと思ってるわ。");

                            UpdateMainMessage("アイン：ああ、相当鍛えられたよな。もう以前のお前とは比べものにならねえしな。");

                            UpdateMainMessage("ラナ：教えてもらってる事でどうしても上手く分からない所があるのよ。");

                            UpdateMainMessage("ラナ：（声マネ）『ガンツ：ラナ君、力、技、知、心も全て等しく必要と覚えておきなさい。』");

                            UpdateMainMessage("ラナ：（声マネ）『ガンツ：体系とイメージは同一と覚えておきなさい。』");

                            UpdateMainMessage("アイン：ッハッハッハ！　ラナが声マネするのって凄ぇ面白いな、ッハッハッハ！");

                            UpdateMainMessage("ラナ：っちょっと、そこはどうでも良いじゃないの、中身を聞いてよね。");

                            UpdateMainMessage("ラナ：今言った内容、ようやく分かった気がするのよ。やってみるわ。");

                            UpdateMainMessage("　　　『ラナは戦闘の構えではなく、一つの奇妙な体系を描き始めた。』");

                            UpdateMainMessage("　　　『ラナの両手は龍のような円を描き、足元は自然と流れ始めるような位置に取った。』");

                            UpdateMainMessage("　　　『ラナの姿がその場で２重３重と幻影が出始めた！』");

                            UpdateMainMessage("アイン：！！？　これは！！");

                            UpdateMainMessage("ラナ：行けるわ・・・フウウゥゥ・・・いくわよ、　【究極奥義】OboroImpact！");

                            UpdateMainMessage("　　　『ラナの一閃がダミー素振り君にクリーンヒットした！！』");

                            UpdateMainMessage("アイン：お・・・おおおぉぉ！！！！！");

                            UpdateMainMessage("ラナ：ッハアァアアァァァ・・・駄目ね。凄い疲れるわコレ。");

                            UpdateMainMessage("アイン：お、おま、今何か動きがヤバかったぞ。何なんだ今のは！！！");

                            UpdateMainMessage("ラナ：ガンツ叔父さんが伝授してくれた言葉通りよ。力、技、知、心の全てを注いでみたの。");

                            UpdateMainMessage("ラナ：あとはイメージよね、龍のような流線型で流れ出るようなイメージ。");

                            UpdateMainMessage("　　　『アインの顔からは珍しくいつもの笑顔が消えている。』");

                            UpdateMainMessage("アイン：お・・・おまえさ。");

                            UpdateMainMessage("ラナ：でも、今のままじゃ不完全だわ。多分もっとエッセンスが必要なのよ・・・っえ？何よ？");

                            UpdateMainMessage("アイン：おまえ、何か本当に拳武術ってピッタリだな。");

                            UpdateMainMessage("ラナ：え？・・・えええぇぇえぇ？？っちょ、何マジ顔になってるのよ！？");

                            UpdateMainMessage("アイン：いや、今のスキル。俺には到底できそうにねえと思ったのさ。");

                            UpdateMainMessage("ラナ：ま、まあ、ア、アインには違う性質のスキルがあるじゃない？");

                            UpdateMainMessage("アイン：ああ、そうだけどさ・・・");

                            UpdateMainMessage("ラナ：えっと何でヘコんでるのよ？ッササ、次はアインの番よ♪");

                            UpdateMainMessage("アイン：・・・ああ");

                            UpdateMainMessage("ラナ：変な所で落ち込むわねホント。シャンっとしなさいよ！");

                            UpdateMainMessage("アイン：あ、ああ、ああ、オーケーオーケー。");

                            UpdateMainMessage("ラナ：腑抜けアインじゃなくて、いつものバカアインに戻りなさいよね。調子が狂うわ。");

                            UpdateMainMessage("アイン：いやいや、かなりすげえスキルだったからな。正直ビビったぜ。");

                            UpdateMainMessage("アイン：まあ俺も負けちゃいられねえ。精進あるのみだな。");

                            UpdateMainMessage("ラナ：（声マネ）『ガンツ：アインよ、精進しなさい』");

                            UpdateMainMessage("アイン：ッハッハッハ！お前のそれすげえ似てるな。ああ、精進するぜ、任せておきな！");

                            UpdateMainMessage("ラナ：ッフフ、ノーテンキね。っささ、私も精進していくわよ、見てなさいよね♪");

                            md.Message = "＜ラナは朧・インパクトを習得しました＞";
                            md.ShowDialog();
                            sc.OboroImpact = true;
                        }
                        if (sc.Level >= 37 && !sc.AbsoluteZero)
                        {
                            UpdateMainMessage("ラナ：やったわ♪　ＬＶ３７に到達ね♪");

                            UpdateMainMessage("アイン：ラナも、もうすぐＭＡＸ４０だな。");

                            UpdateMainMessage("ラナ：私ね、水魔法で最高に良いものは何かってずっと考えていたの。");

                            UpdateMainMessage("アイン：どんなのだ？");

                            UpdateMainMessage("ラナ：「凍結状態」っていうのに凄く憧れるのよね。全部凍結しちゃえばいいのよ。");

                            UpdateMainMessage("アイン：お前はたまにそういう過激な発言をするよな・・・");

                            UpdateMainMessage("アイン：って、まさか！？");

                            UpdateMainMessage("ラナ：もう準備は万全よ、食らいなさい。　AbsoluteZero！");

                            UpdateMainMessage("　　　『ッピキーーーーーン！』");

                            UpdateMainMessage("アイン：っが！・・・こ、これは・・・駄目だ、何か全然動きが・・・");

                            UpdateMainMessage("ラナ：やったわ、完璧ね。今から解説してあげるわ、よーく聞きなさいよね♪");

                            UpdateMainMessage("アイン：っくそが、何しやがった？　オラァ！");

                            UpdateMainMessage("ラナ：そそ、攻撃だけはできるのよ。安心した？♪");

                            UpdateMainMessage("アイン：疑問系に♪くっつけやがって・・・安心できるかよ、余計不安が広がったぜ。");

                            UpdateMainMessage("ラナ：【その１】バカアインはスキルが使えなくなった♪");

                            UpdateMainMessage("アイン：何だと！？・・・キ・・・キネティッ・・・");

                            UpdateMainMessage("アイン：駄目だ、全然構えが取れねえ・・・");

                            UpdateMainMessage("ラナ：【その２】バカアインは魔法が使えなくなった♪");

                            UpdateMainMessage("アイン：マジかよ！ええいくそ・・・ゲイル・・・ウィ・・・");

                            UpdateMainMessage("アイン：ックソ！全然、詠唱形態が・・・");

                            UpdateMainMessage("ラナ：【その３】バカアインはライフ回復が出来なくなった♪");

                            UpdateMainMessage("アイン：おいおい、そんなのまでアリかよ！？ポーションはさすがに・・・");

                            UpdateMainMessage("アイン：（ッゴクッゴク）・・・！まるで効いてねえ！！");

                            UpdateMainMessage("ラナ：【その４】バカアインは防御の構えが取れなくなった。じゃ、行くわね♪");

                            UpdateMainMessage("アイン：ッバ！嘘だろ！？さすがに構えぐらい・・・あ・・・ックオオオォォ！！");

                            UpdateMainMessage("　　　『ッボッゴオオォオオォォン！！ （ラナのアイン撲滅ブローが炸裂！）』");

                            UpdateMainMessage("アイン：こ・・・こんなのアリか・・・　　　（ッパタ）");

                            UpdateMainMessage("ラナ：よく考えたらOboroImpactとの強烈なコンボになりそうね。");

                            UpdateMainMessage("ラナ：ッフフ、今日の所はこの辺にしといてあげるわ♪");

                            UpdateMainMessage("アイン：頼むからその辺にしておいてくれ・・・");

                            md.Message = "＜ラナはアブソリュート・ゼロを習得しました＞";
                            md.ShowDialog();
                            sc.AbsoluteZero = true;
                        }
                        if (sc.Level >= 38 && !we.AlreadyLvUpEmpty25)
                        {
                            UpdateMainMessage("ラナ：ＬＶ３８だけど・・・ハァ・・・どっちかしらね・・・");

                            UpdateMainMessage("アイン：っお、ラナ、やけに元気が無いな。");

                            UpdateMainMessage("ラナ：アインはいつもノーテンキよね。");

                            UpdateMainMessage("アイン：何言ってるんだ、俺だってたまにあるぞ。");

                            UpdateMainMessage("ラナ：何よ、言ってみなさいよ。");

                            UpdateMainMessage("アイン：・・・っくそう！ってな。");

                            UpdateMainMessage("ラナ：何よそれ。全然違うじゃない・・・ハアアァァ～～～～ァァ・・・");

                            UpdateMainMessage("アイン：相談ならいつでも乗ってやれるぜ？");

                            UpdateMainMessage("ラナ：バカアインに聞いても駄目だろうけど、一応じゃあ聞いてくれる？");

                            UpdateMainMessage("アイン：っお、おお！任せておけって！");

                            UpdateMainMessage("ラナ：私ね、薬草術は出来るとしても、魔法と拳武術どっちだと思う？");

                            UpdateMainMessage("アイン：両方だろ。");

                            UpdateMainMessage("ラナ：どっち？って聞いてるのよ、取捨選択よ、左 or 右よ、両方なんて駄目よ。");

                            UpdateMainMessage("アイン：何で駄目なんだ？良いだろ、両方あれば。");

                            UpdateMainMessage("ラナ：ック・・・聞いた私がバカだったわ！");

                            UpdateMainMessage("アイン：ま、まあ待て待て落ち着けって。何でどっちか選ぼうとするんだよ？");

                            UpdateMainMessage("ラナ：だって・・・このままだと、どっちつかずじゃない。");

                            UpdateMainMessage("アイン：お前も変な所でそうやって決定したがるクセがあるよな。");

                            UpdateMainMessage("アイン：いいか、ラナ。じゃ、薬草術は何で大丈夫なんだよ？");

                            UpdateMainMessage("ラナ：薬草術は戦闘系じゃないでしょ？だから区別してれば大丈夫よ。");

                            UpdateMainMessage("アイン：じゃ、こんなのはどうだ。魔法は戦闘以外じゃ使えないのか？");

                            UpdateMainMessage("アイン：ダメージ系魔法にしたって制御が出来てりゃ日常でも何かと使い道はあるだろ。");

                            UpdateMainMessage("ラナ：じゃあ、魔法は戦闘以外って事にしたとしたら");

                            UpdateMainMessage("ラナ：拳武術は残せそうだけど、今度は薬草術と魔法のどっちかを選択しなきゃ駄目よね。");

                            UpdateMainMessage("アイン：っば、別にそういう意味で言ったんじゃねえって。何でそうなるんだ・・・");

                            UpdateMainMessage("アイン：いいか、ラナ。じゃ、もう一回だ。お前、区別してれば大丈夫なんだな？");

                            UpdateMainMessage("ラナ：まあそうね、同じジャンルとしてかぶらなければ大丈夫だとは思えるわ。");

                            UpdateMainMessage("アイン：じゃあ、俺が今から区分けしてやる。良いか、よく聞けよ。");

                            UpdateMainMessage("アイン：【薬草術】　生産系");

                            UpdateMainMessage("アイン：【拳武術】　戦闘系");

                            UpdateMainMessage("アイン：【魔法術】　エレメンタル系");

                            UpdateMainMessage("ラナ：何よそのエレメンタル系って。明らかに今作ったでしょ？");

                            UpdateMainMessage("アイン：駄目なのか？じゃあ、ハイブリッド系。");

                            UpdateMainMessage("ラナ：そんなどっちつかずな単語駄目に決まってるじゃない。駄目よ。");

                            UpdateMainMessage("アイン：っくそう、じゃあとっておきの・・・オールラウンダー系！");

                            UpdateMainMessage("ラナ：そんな包括的な単語も駄目よ。何よ、全然納得できないじゃないの！");

                            UpdateMainMessage("アイン：クッキング系！　サンダー系！　詠唱系！　イリュージョン系！　スペシャル系！");

                            UpdateMainMessage("ラナ：・・・　・・・");

                            UpdateMainMessage("アイン：あ、いやすまねえ・・・駄目か。あんまり相談に乗ってやれてないか。");

                            UpdateMainMessage("ラナ：詠唱系・・・とかいけそうね。");

                            UpdateMainMessage("ラナ：薬草は実際に作らないと駄目、拳武術は自分の拳で直接的なものだし。");

                            UpdateMainMessage("ラナ：魔法は詠唱することで効果を発動するから確かに区分け出来そうね。ホントだわ！");

                            UpdateMainMessage("ラナ：うん、今のままで行ってみるわ、ありがと♪");

                            UpdateMainMessage("アイン：・・・ああ！よかったじゃねえか！ッハッハッハ！");

                            UpdateMainMessage("ラナ：アインはこういうの良く迷ったりはしないの？");

                            UpdateMainMessage("アイン：別になんとも思わねえな、こなせるもんは適当にやっときゃ良いだろ。");

                            UpdateMainMessage("アイン：スラッシュ！　ヒール！　ファイア！　オラァ！　食らえ！");

                            UpdateMainMessage("ラナ：・・・武具の手入れぐらいはキチンとしなさいよね。細工術とかやったらどうよ？");

                            UpdateMainMessage("アイン：ムリ！！");

                            UpdateMainMessage("ラナ：やっぱバカね・・・ある意味迷いが無いから、無駄なくバカね。");

                            UpdateMainMessage("アイン：無駄なくバカとは何だ。まあ、また迷ったら相談してくれよ！っな！？");

                            UpdateMainMessage("ラナ：そうね、また今度機会があればね♪");

                            UpdateMainMessage("アイン：ところでお前さ、今回閃きは無しかよ？");

                            UpdateMainMessage("ラナ：ッヴ・・・　・・・　・・・　ッササ、練習練習♪");

                            md.Message = "＜ラナは何も習得できませんでした＞";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty25 = true;
                        }
                        if (sc.Level >= 39 && !sc.TimeStop)
                        {
                            UpdateMainMessage("ラナ：ＬＶ３９よ、最大４０まであと一歩と迫ったわ♪");

                            UpdateMainMessage("アイン：ラナの終盤習得する内容はすげえのばかりだな。");

                            UpdateMainMessage("ラナ：何言ってるのよ、アインだって大したモノばかりじゃないの。");

                            UpdateMainMessage("アイン：いや、俺のはなんて言うんだ。当たり前な強さばかりだが");

                            UpdateMainMessage("アイン：ラナが習得してきているものは異例な強さが多い。そんな気がするぜ。");

                            UpdateMainMessage("ラナ：私の場合ダメージ系ばっかりは好きじゃないから、そうなるのかもね。");

                            UpdateMainMessage("ラナ：そうそう、今回のも多分凄いと思うわ。ね、聞いてよ。");

                            UpdateMainMessage("アイン：ん？今度はどんなのだ？");

                            UpdateMainMessage("ラナ：以前の水魔法は「凍結」がコンセプトだったけど、今回は違うの。");

                            UpdateMainMessage("ラナ：空魔法でいろいろ考えたのよ、何が最大級なのかって。");

                            UpdateMainMessage("ラナ：アイン、時間ってどう感じる？");

                            UpdateMainMessage("アイン：時間？感じると言うよりは、普通に経過していくって感じだな。");

                            UpdateMainMessage("ラナ：ご飯食べてる時や、練習稽古に励んでる時は？");

                            UpdateMainMessage("アイン：特に意識はしてねえ。");

                            UpdateMainMessage("ラナ：寝る前は？");

                            UpdateMainMessage("アイン：何時間寝るかって少しは考えがよぎるな。");

                            UpdateMainMessage("ラナ：でも時間は等しく流れているわ。意識できるか出来ないかの差なのよ。");

                            UpdateMainMessage("ラナ：この魔法は、アインの時間に対する認識を狂わせる魔法よ。");

                            UpdateMainMessage("アイン：一体どうなるんだ？");

                            UpdateMainMessage("ラナ：今からやってみせるわ。アイン自身が体験してみてちょうだい。いくわよ、TimeStop！");

                            UpdateMainMessage("ラナ：どうだった？アイン。");

                            UpdateMainMessage("アイン：は？いいから早くかけろって。");

                            UpdateMainMessage("ラナ：もう、終わってるわよ、アインの自分のライフ値を見てみなさい。");

                            UpdateMainMessage("アイン：ッバ！・・・バカな！！！？");

                            UpdateMainMessage("　　　『アインのライフは既に半分以下にまで減っていた！』");

                            UpdateMainMessage("アイン：・・・こ・・・これは・・・");

                            UpdateMainMessage("アイン：ダメージ系魔法だな！！！");

                            UpdateMainMessage("ラナ：違うわよ、ダメージ系は実際に当てたのは事実だけどね。");

                            UpdateMainMessage("アイン：実際当てたんだろ、じゃあダメージ系じゃないか。");

                            UpdateMainMessage("ラナ：私が仕掛けたOboroImpactを、アインが認識していなかったって事よ。");

                            UpdateMainMessage("ラナ：ううん、実際はダメージヒット直前で認識出来てるはずよ。");

                            UpdateMainMessage("アイン：まあ確かにラナがTimeStopと言った直後に体に衝撃が走ってるからな。");

                            UpdateMainMessage("アイン：その魔法自体がダメージ系魔法なんじゃないかと思っちまうぜ。");

                            UpdateMainMessage("ラナ：アイン、私この魔法は凄く危険だと思うわ。");

                            UpdateMainMessage("アイン：どういう意味だ？");

                            UpdateMainMessage("ラナ：時間の認識ができないのよ。今のは単なるダメージ系だけど");

                            UpdateMainMessage("ラナ：もし、複雑で連続性の高いコンボが一瞬の間で完成してしまったとしたら");

                            UpdateMainMessage("ラナ：おそらく、避ける術はどんどん無くなっていくわ。");

                            UpdateMainMessage("アイン：コンボなんざ、俺にとってはそれほど怖くはねえ。攻略方法は必ずあるはずだ。");

                            UpdateMainMessage("アイン：ラナ、その魔法どんどん使ってきて良いぜ。絶対に弱点はあるはずだからな。");

                            UpdateMainMessage("ラナ：そうね、アインなら何でも打ち破りそうだし、DUELではどんどん使うわよ♪");

                            md.Message = "＜ラナはタイムストップを習得しました＞";
                            md.ShowDialog();
                            sc.TimeStop = true;
                        }
                        if (sc.Level >= 40 && !sc.NothingOfNothingness)
                        {
                            UpdateMainMessage("ラナ：きたわ・・・ついに私も最高レベルよ。");

                            UpdateMainMessage("アイン：やったじゃねえか、ラナ！　やっぱお前はすげえよ。");

                            UpdateMainMessage("ラナ：アインとずっとやってきてるワケだし、このぐらいはこなさないとね♪");

                            UpdateMainMessage("ラナ：それで、今回私が習得する内容は実は一番最初の頃からある構想なの。");

                            UpdateMainMessage("アイン：一番最初から？");

                            UpdateMainMessage("ラナ：無くしたくないものって凄く大事よね。");

                            UpdateMainMessage("アイン：まあ、そうだな。");

                            UpdateMainMessage("ラナ：でも必ず無くなっていくの、まるで最初から無かったかのよう。");

                            UpdateMainMessage("アイン：・・・まあ・・・そうなのかも知れねえな。");

                            UpdateMainMessage("ラナ：無くそうとしても無くせないようにすれば良いと思わない？");

                            UpdateMainMessage("ラナ：強制的に無くそうと試みても、絶対に無くなりはしないの。");

                            UpdateMainMessage("アイン：どんなのに・・・するつもりなんだよ・・・？");

                            UpdateMainMessage("ラナ：今集中して、発動させてみるわ・・・");

                            UpdateMainMessage("ラナ：・・・　・・・　・・・　");

                            UpdateMainMessage("ラナ：・・・　・・・");

                            UpdateMainMessage("ラナ：・・・　来たわ、いける。　命名はNothingOfNothingnessよ。");

                            UpdateMainMessage("　　　『ラナの周囲に無形フィールドが色とりどりに拡がった！』");

                            UpdateMainMessage("アイン：な・・・　・・・");

                            UpdateMainMessage("ラナ：完成よ。");

                            UpdateMainMessage("アイン：ちょっと待てよ。何が完成なんだ。");

                            UpdateMainMessage("ラナ：このスキルは相手が無効化してくる行動・スキル・魔法を全て無効にするの。");

                            UpdateMainMessage("ラナ：これから私が得る能力ＵＰ系のものは全て永続的な効果が絶対的なものとなるわ。");

                            UpdateMainMessage("ラナ：私が繰り出すスキルは、全て発動の邪魔はされなくなるわ。これも絶対よ。");

                            UpdateMainMessage("ラナ：私が詠唱する魔法は、全て発動割り込み・打ち消しは通用しなくなるわ。絶対にね。");

                            UpdateMainMessage("アイン：完全に・・・逆だな。今までと。");

                            UpdateMainMessage("アイン：どういうルートで思いついたんだ？");

                            UpdateMainMessage("ラナ：そんなの秘密に決まってるじゃない。");

                            UpdateMainMessage("アイン：っくそ、ここに来て秘密かよ。まあルートはこの際良いとしてもだ。");

                            UpdateMainMessage("アイン：お前のそのスキル、消費量はどのぐらいなんだ？");

                            UpdateMainMessage("ラナ：キッチリ１００よ。弱点はこの消費量ぐらいね。");

                            UpdateMainMessage("アイン：ラナ、お前本当に・・・すげえよ、やっぱり。");

                            UpdateMainMessage("ラナ：何そんなマジ顔になってんのよ、ッフフ。アインにはあんまり関係ないかもね♪");

                            UpdateMainMessage("ラナ：私、このスキルを駆使して必ずＤＵＥＬ大会で優勝してみせるわ。");

                            UpdateMainMessage("アイン：ッハハ、言ってくれるじゃねえか。ＤＵＥＬ大会はライバルがひしめきあう場所だ。");

                            UpdateMainMessage("アイン：ラナ、お前すげえからな、できるかもしれねえぜ。");

                            UpdateMainMessage("ラナ：ありがと、っさてとこのスキルでどの程度まで適用されるのかをやってみるとするわ。");

                            UpdateMainMessage("ラナ：アイン、ちょっと付き合いなさい♪");

                            UpdateMainMessage("アイン：ああ、良いぜ！");

                            md.Message = "＜ラナはナッシング・オブ・ナッシングネスを習得しました＞";
                            md.ShowDialog();
                            sc.NothingOfNothingness = true;
                        }
                    }
                }
                #endregion
                #region "ヴェルゼのレベルアップ"
                if (tc != null)
                {
                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.Manual;
                        md.Location = new Point(this.Location.X, this.Location.Y + this.Size.Height);
                        md.NeedAnimation = true;

                        if (tc.Level >= 3 && !tc.FireBall)
                        {
                            UpdateMainMessage("ヴェルゼ：やりました、レベルが上がりましたよ。");

                            UpdateMainMessage("アイン：ヴェルゼ、最初はどういうものを閃くつもりだ？");

                            UpdateMainMessage("ラナ：ヴェルゼさんって、属性は何になるの？");

                            UpdateMainMessage("ヴェルゼ：ハハハ、いっぺんにきましたね。ひとつずつ答えますよ。");

                            UpdateMainMessage("ヴェルゼ：まず、最初は何と言ってもダメージ源になる火属性ですね。");

                            UpdateMainMessage("アイン：っお、俺と同じじゃないか。気が合うな。");

                            UpdateMainMessage("ヴェルゼ：ええ、アインさんと同じ火です。でも属性についてですが。");

                            UpdateMainMessage("ヴェルゼ：ボクの場合、特別な属性は無いんですよ。");

                            UpdateMainMessage("ラナ：無い・・・？");

                            UpdateMainMessage("ヴェルゼ：そうです。全属性、行けるんです。カール爵ほどではありませんけどね。");

                            UpdateMainMessage("アイン：マジかよ・・・やっぱ伝説のFiveSeekerはダテじゃねえな・・・");

                            UpdateMainMessage("ヴェルゼ：ハハ、アイン君。買いかぶりです、中途半端なだけですよ。");

                            UpdateMainMessage("ヴェルゼ：それにしても久しぶりです。ちょっとやってみましょう・・・ハイ。");

                            UpdateMainMessage("ラナ：か、軽く出したわ。Fireballよね今の。ほとんど構えてなくない？？");

                            UpdateMainMessage("ヴェルゼ：出し方はコツがあります。構え無しのテクは今度教えますよ。");

                            UpdateMainMessage("ラナ：え、ホントですか？是非是非♪");

                            md.Message = "＜ヴェルゼはファイア・ボールを習得しました＞";
                            md.ShowDialog();
                            using (MessageDisplay md2 = new MessageDisplay())
                            {
                                md2.StartPosition = FormStartPosition.Manual;
                                md2.Location = new Point(this.Location.X, this.Location.Y + this.Size.Height);
                                md2.NeedAnimation = true;
                                md2.Message = "＜ヴェルゼの魔法使用が解放されました＞";
                                md2.ShowDialog();
                            }
                            tc.AvailableMana = true;
                            tc.FireBall = true;
                        }
                        if (tc.Level >= 4 && !tc.DoubleSlash)
                        {
                            UpdateMainMessage("ヴェルゼ：さてさて、レベルアップですね。");

                            UpdateMainMessage("ラナ：伝説のFiveSeeker時代のスキルはどんなのがあったんですか？");

                            UpdateMainMessage("ヴェルゼ：CarnageRushというのがありましたよ。");

                            UpdateMainMessage("アイン：それってどんなのだ？");

                            UpdateMainMessage("ヴェルゼ：５回です。");

                            UpdateMainMessage("アイン：・・・は？？");

                            UpdateMainMessage("ヴェルゼ：今のボクはまだ思い出せませんが、純粋な５回攻撃の事です。");

                            UpdateMainMessage("ラナ：ジョーダンでしょ・・・");

                            UpdateMainMessage("ヴェルゼ：足元の構え、独特のステップ。加えて武器の振り具合で５回当てるんです。");

                            UpdateMainMessage("ヴェルゼ：ちょっと昔の事なんですいません。今はこのぐらいが良いリハビリです。");

                            UpdateMainMessage("　　　『ッッブブン！！！』　　");

                            UpdateMainMessage("アイン：うお！まさか今のダブル・スラッシュか！？はええ！！！");

                            UpdateMainMessage("ヴェルゼ：はい、これ懐かしいですね。昔から随分訓練したものです。");

                            UpdateMainMessage("アイン：こ、今度さ。俺に今のスピードでのやり方を教えてくれないか？");

                            UpdateMainMessage("ヴェルゼ：はい、喜んで。");

                            md.Message = "＜ヴェルゼはダブル・スラッシュを習得しました＞";
                            md.ShowDialog();
                            using (MessageDisplay md2 = new MessageDisplay())
                            {
                                md2.StartPosition = FormStartPosition.Manual;
                                md2.Location = new Point(this.Location.X, this.Location.Y + this.Size.Height);
                                md2.NeedAnimation = true;
                                md2.Message = "＜ヴェルゼのスキル使用が解放されました＞";
                                md2.ShowDialog();
                            }
                            tc.AvailableSkill = true;
                            tc.DoubleSlash = true;
                        }
                        if (tc.Level >= 5 && !tc.DarkBlast)
                        {
                            UpdateMainMessage("ヴェルゼ：レベル・・・アップですね。っ痛ぅ・・・");

                            UpdateMainMessage("ラナ：ど、どうしたんですか？");

                            UpdateMainMessage("ヴェルゼ：いや、何でもないです。ちょっと闇属性はクセがありましてね。");

                            UpdateMainMessage("アイン：何だ、意外と得手不得手があるんだな。");

                            UpdateMainMessage("ヴェルゼ：え、ええぇ・・・最初は簡単なヤツで肩慣らし・・・です・・・");

                            UpdateMainMessage("ヴェルゼ：ック・・・クク、DarkBlast！！！");

                            UpdateMainMessage("アイン：ヴェ、ヴェルゼ大丈夫なのか？お前。");

                            UpdateMainMessage("ヴェルゼ：ハァハァ・・・だ、大丈夫です。心配しないで。");

                            UpdateMainMessage("ラナ：しんどかったら休んでくださいね。");

                            UpdateMainMessage("ヴェルゼ：ええ、ありがとうございます。もう大丈夫ですよ。");

                            md.Message = "＜ヴェルゼはダーク・ブラストを習得しました＞";
                            md.ShowDialog();
                            tc.DarkBlast = true;
                        }
                        if (tc.Level >= 6 && !tc.CounterAttack)
                        {
                            UpdateMainMessage("ヴェルゼ：レベルアップするたびに、思い出す。どういう構造なんでしょうね。");

                            UpdateMainMessage("アイン：ヴェルゼ、知ってるか？たまに閃かないんだぜ。");

                            UpdateMainMessage("ラナ：それはバカアインだけの特権でしょ。ヴェルゼさんに限って無いわよ。");

                            UpdateMainMessage("ヴェルゼ：いや、ボクもいずれ１回休みはあると思います。でも今回は行けます。");

                            UpdateMainMessage("ヴェルゼ：体術は好み次第ですが、やはりCounterAttackが無難でしょう。");

                            UpdateMainMessage("ラナ：無難な技なんですか？");

                            UpdateMainMessage("ヴェルゼ：ええ、相手がどういった行動をしてくるか分からない場合。");

                            UpdateMainMessage("ヴェルゼ：または、相手が物理攻撃が強いかどうかを見極める場合。");

                            UpdateMainMessage("ヴェルゼ：更に、これをやった時にマイナスになる事は一つもありませんしね。");

                            UpdateMainMessage("アイン：相手が能力ＵＰ系だったら損じゃねえのか？");

                            UpdateMainMessage("ヴェルゼ：最初の１回ぐらいは差し上げますよ。一撃死のリスク回避が最優先です。");

                            UpdateMainMessage("アイン：そうか・・・言われてみればそうかもな。");

                            UpdateMainMessage("ラナ：そういえば、ガンツ叔父さんもそんな事言ってたわ。");

                            UpdateMainMessage("アイン：結構深いな、参考になるぜ。サンキュー。");

                            md.Message = "＜ヴェルゼはカウンター・アタックを習得しました＞";
                            md.ShowDialog();
                            tc.CounterAttack = true;
                        }
                        if (tc.Level >= 7 && !tc.FreshHeal)
                        {
                            UpdateMainMessage("ヴェルゼ：さて、今回のレベルアップは既にイメージがあります。");

                            UpdateMainMessage("アイン：どれにするんだ？まさかFreshHealとかか？");

                            UpdateMainMessage("ヴェルゼ：はい、そのまさかですよ。アイン君。");

                            UpdateMainMessage("アイン：マジかよ！？　絶対当たると思ってないから言ったのに。");

                            UpdateMainMessage("ラナ：聖と闇の両立なんて可能なんですか？");

                            UpdateMainMessage("ヴェルゼ：カール爵に一度教わると良いです。本質が同じだという理解が必要です。");

                            UpdateMainMessage("アイン：本質が同じ？　バカな。まったくの逆じゃねえか。");

                            UpdateMainMessage("ヴェルゼ：ええ、真逆ですから、故に同じ本質を持っているんです。");

                            UpdateMainMessage("ラナ：何となくだけど、分かる気がするわね。");

                            UpdateMainMessage("アイン：お、俺も何となくだが、分かるぜ。ッハッハッハ！");

                            UpdateMainMessage("ラナ：じゃあおさらい、言って見てよ、アイン。");

                            UpdateMainMessage("アイン：反対なんだ。だから同じなんだよ！ッハッハッハ！");

                            UpdateMainMessage("ラナ：・・・この馬鹿に両立は絶対に無理そうね。");

                            UpdateMainMessage("ヴェルゼ：アイン君はそのままで良いと思いますけどね、気が向けばいつでもどうぞ。");

                            UpdateMainMessage("アイン：ッハッハッハ！");

                            md.Message = "＜ヴェルゼはフレッシュ・ヒール習得しました＞";
                            md.ShowDialog();
                            tc.FreshHeal = true;
                        }
                        if (tc.Level >= 8 && !tc.StanceOfFlow)
                        {
                            UpdateMainMessage("ヴェルゼ：レベルアップです。どんどん行きましょう。");

                            UpdateMainMessage("ラナ：でもヴェルゼさん、体術も物凄くサマになってますよね。");

                            UpdateMainMessage("ヴェルゼ：ハハ、アイン君にも言いましたが、買いかぶりです。");

                            UpdateMainMessage("アイン：ヴェルゼって妙にベースの動きが早えよな。");

                            UpdateMainMessage("ヴェルゼ：構えるクセを付けておくと、“早いという錯覚”を相手に印象付けられます。");

                            UpdateMainMessage("ヴェルゼ：あ、試しにStanceOfFlowで今回は行きましょう。");

                            UpdateMainMessage("アイン：出た！あのワザと後手になるやつ！！俺には理解できねえが。");

                            UpdateMainMessage("ラナ：何言ってんのよ。ガンツ叔父さん直伝で私もかなり良くなったんだから。");

                            UpdateMainMessage("ヴェルゼ：ラナさん、ちょっと見せてください。ボクが診断してみます。");

                            UpdateMainMessage("ラナ：よし、じゃあ行くわよ・・・・・・");

                            UpdateMainMessage("ヴェルゼ：・・・さて、なかなか上手ですよ。隙が無くバランスが良い、お見事ですね。");

                            UpdateMainMessage("ラナ：ホラホラ、どうだバカアイン、あなたには一生無理よね♪");

                            UpdateMainMessage("アイン：ハナっから興味がねえんだよ。知るかっつうの。");

                            UpdateMainMessage("ヴェルゼ：さて、ちょっと離れててください。次はボクが構えてみますから。");

                            UpdateMainMessage("ヴェルゼ：アイン君、では行きますよ・・・");

                            UpdateMainMessage("　　　『ヴェルゼに睨まれた瞬間、アインは異様な冷や汗をかいた』　　");

                            UpdateMainMessage("アイン：・・・すげえ。全部読まれてるみたいで勝てる気がしなかったぞ、今。");

                            UpdateMainMessage("ラナ：私のとは、何となく雰囲気が違うわね。");

                            UpdateMainMessage("ヴェルゼ：後手必勝とは相手の手口を封殺する事にあります。");

                            UpdateMainMessage("アイン：くそ・・・俺も練習してみるかな・・・ラナ、練習だ。見つめていいか！？");

                            UpdateMainMessage("ラナ：気持ち悪いから、他でやってちょうだい・・・");

                            md.Message = "＜ヴェルゼはスタンス・オブ・フローを習得しました＞";
                            md.ShowDialog();
                            tc.StanceOfFlow = true;
                        }
                        if (tc.Level >= 9 && !tc.StanceOfStanding)
                        {
                            UpdateMainMessage("ヴェルゼ：レベルアップ毎に思い出す感じです。懐かしいですね、本当に。");

                            UpdateMainMessage("アイン：ヴェルゼは魔法・スキルどっちが得意なんだ？");

                            UpdateMainMessage("ヴェルゼ：得意・・・得意ですか？強いて言えば両方です。");

                            UpdateMainMessage("アイン：何となくそんな気がしたぜ・・・");

                            UpdateMainMessage("ラナ：ヴェルゼさん、今回はどっちを思い出したんですか？");

                            UpdateMainMessage("アイン：２連続体術だったりしてな。ッハッハッハ！");

                            UpdateMainMessage("ヴェルゼ：アイン君、結構カンが良いですね。StanceOfStandingで当たりです。");

                            UpdateMainMessage("アイン：StanceOfFlowと両方使えるのか。本当に何でもアリなんだな。");

                            UpdateMainMessage("ヴェルゼ：この構え、ボクのお気に入りです、何と言っても防御兼攻撃ですからね。");

                            UpdateMainMessage("ヴェルゼ：では、やってみましょう。アイン君、構えて。");

                            UpdateMainMessage("アイン：ああ、いつでも来い。基本的には普通攻撃だからな。");

                            UpdateMainMessage("ヴェルゼ：ええと、確かこんな感じでしたね。ッハイ！！！");

                            UpdateMainMessage("　　　『ッドスウウゥゥゥン！！！』　　");

                            UpdateMainMessage("ラナ：え・・・何か今、凄い轟音しなかった？");

                            UpdateMainMessage("アイン：ッゲ・・・ガハアァ！！ッゲホッゲホ！んだ今の・・・ッグッガハァ！！");

                            UpdateMainMessage("ヴェルゼ：！！？　しまった！！！　すいません、ボクとしたことが！！！");

                            UpdateMainMessage("アイン：今の・・・何なんだ？体中に・・・衝撃が");

                            UpdateMainMessage("ヴェルゼ：何でも無いんです、忘れてください。本当にすいませんでした。");

                            UpdateMainMessage("ラナ：アイン、大丈夫？顔が真っ青ね、しっかりしなさいよ♪");

                            UpdateMainMessage("ヴェルゼ：Catastropheという究極奥義です。StandOfStandingに似てるので、つい。");

                            UpdateMainMessage("アイン：ふう大丈夫みたいだ。まあ、StandOfStandingは知ってるからな。次また見せてくれ。");

                            UpdateMainMessage("ヴェルゼ：ええ、本当に次からは気をつけます。");

                            md.Message = "＜ヴェルゼはスタンス・オブ・スタンディングを習得しました＞";
                            md.ShowDialog();
                            tc.StanceOfStanding = true;
                        }
                        if (tc.Level >= 10 && !tc.DispelMagic)
                        {
                            UpdateMainMessage("ヴェルゼ：さて、このＬＶ１０・・・・・・っう");

                            UpdateMainMessage("アイン：おい、ヴェルゼ顔色が悪いぞ。大丈夫か、お前？");

                            UpdateMainMessage("ヴェルゼ：っう、ガアアァァァァ！！！！！");

                            UpdateMainMessage("ヴェルゼ：ハハハハハハ！！消えて無くなれ！！！　DispelMagic！！！");

                            UpdateMainMessage("ラナ：っひ・・・やだ・・・な、何か変だよアイン。");

                            UpdateMainMessage("アイン：おい！オイ！！！ヴェルゼしっかりしろ！！！");

                            UpdateMainMessage("ヴェルゼ：アアァァァァ・・・ハァハァ・・・ア、アイン君ですか？");

                            UpdateMainMessage("アイン：ああ、俺だ。大丈夫か！？");

                            UpdateMainMessage("ヴェルゼ：・・・・・・DispelMagicはボクにとってトラウマみたいな魔法なんです。");

                            UpdateMainMessage("ラナ：やだ・・・しんどいなら思い出さなくて良いよ。私だって出来るから。");

                            UpdateMainMessage("ヴェルゼ：ラナさんも、あまり濫用しない方が良いです。この魔法副作用が大きいですから。");

                            UpdateMainMessage("アイン：どういう事なんだ？説明・・・っと、止めておくか。");

                            UpdateMainMessage("ヴェルゼ：いえ、お気にせずに。少し説明しましょうか。");

                            UpdateMainMessage("ヴェルゼ：元々このDispelMagicというのはこの世には存在しない魔法です。");

                            UpdateMainMessage("アイン：この世に？あの世なら在るってのか？");

                            UpdateMainMessage("ヴェルゼ：違います。正確にはこのダンジョン限定で使える魔法という意味です。");

                            UpdateMainMessage("ラナ：え、そうだったの？全然知らなかったわね。");

                            UpdateMainMessage("ヴェルゼ：DispelMagicとは純粋にこのダンジョンへ願い事をするようなものです。");

                            UpdateMainMessage("ヴェルゼ：このダンジョンは挑戦してくる者の精神へ大きく干渉しています。");

                            UpdateMainMessage("アイン：そうか・・・何となく分かったぜ。確かに止めといた方が良いぞラナ。");

                            UpdateMainMessage("ラナ：何でよ？ダンジョン限定なんでしょ？だったら大丈夫なんじゃない？");

                            UpdateMainMessage("アイン：いいや、ダンジョン限定だ。だからこそだぜ。");

                            UpdateMainMessage("アイン：いいか、もしもダンジョン限定で何でも願い事が叶ってみろ。");

                            UpdateMainMessage("アイン：お前一生死ぬまで、このダンジョンから出なくなっちまうぞ。");

                            UpdateMainMessage("ラナ：何言ってるのよ。目的が達成されればさすがにオサラバでしょ♪");

                            UpdateMainMessage("ヴェルゼ：アイン君、ボクが説明します。ラナさん、良いですか？");

                            UpdateMainMessage("ヴェルゼ：ダンジョンはそれを利用して、ラナさんの人格を大きく狂わせる場合があります。");

                            UpdateMainMessage("ヴェルゼ：だからDispelMagicは極力使わないでください。真実の方を大事にしてください。");

                            UpdateMainMessage("ラナ：まあヴェルゼさんがそう言うんなら、分かったわ。あんまり使わないようにするわね♪");

                            UpdateMainMessage("ヴェルゼ：ええ、ラナさんは頭が良いですからね。理解が早くて助かります。");

                            UpdateMainMessage("ヴェルゼ：さて、滅多に使わないようにボクも自己規制を厳しくしますよ。");

                            md.Message = "＜ヴェルゼはディスペル・マジックを習得しました＞";
                            md.ShowDialog();
                            tc.DispelMagic = true;
                        }
                        if (tc.Level >= 11 && !tc.WordOfPower)
                        {
                            UpdateMainMessage("ヴェルゼ：ＬＶがついに２桁に乗ってきました。ボクとしては嬉しいかぎりですね。");

                            UpdateMainMessage("アイン：ヴェルゼ、一つ良い事思いついたぜ。");

                            UpdateMainMessage("ヴェルゼ：はい、なんでしょう？");

                            UpdateMainMessage("アイン：俺が今からやる魔法と同じものを俺に向けてみてくれ。比べてみたいんだ。");

                            UpdateMainMessage("ヴェルゼ：あ、はい、良いですよ。やってみましょうか。");

                            UpdateMainMessage("ラナ：アインったら、何の魔法にするつもりなのよ？");

                            UpdateMainMessage("アイン：魔法名は・・・WordOfPowerだ！オラァ！！");

                            UpdateMainMessage("ヴェルゼ：さて、アイン君のWordOfPower・・・止めれますかね。WordOfPowerです、ッハァァ！");

                            UpdateMainMessage("　　　『ッズズウウン！！』　　");

                            UpdateMainMessage("ヴェルゼ：さて・・・ほぼ互角ですね。いや驚きました。");

                            UpdateMainMessage("アイン：冗談だろ。攻撃魔法の中じゃ、俺の一番得意なヤツだぜ？");

                            UpdateMainMessage("ヴェルゼ：威力自体はおそらくアイン君の方が上ですよ。ご心配なく。");

                            UpdateMainMessage("アイン：っな！・・・じゃどうしてだ！？");

                            UpdateMainMessage("ヴェルゼ：自分が劣勢な側と判断したら、足の付けねと手首を下から上へスナップさせる向きにするんです。");

                            UpdateMainMessage("ヴェルゼ：そうする事で、相手への攻撃は弱くなりますが、衝撃緩和要素を強化できるんですよ。");

                            UpdateMainMessage("ラナ：ヴェルゼさんって、よくそういうのをあたかも当たり前みたいにやりますよね？");

                            UpdateMainMessage("ヴェルゼ：ハハハ、さすがに年季の差といった所でしょう。でもちょっと卑怯ですね。");

                            UpdateMainMessage("ヴェルゼ：エルミだったら、おそらく自分が劣勢でも正面からぶつかるやり方を選びます。");

                            UpdateMainMessage("アイン：ったく、WordOfPowerならイケると思ったんだがな。叶わねえぜホント。");

                            UpdateMainMessage("ヴェルゼ：今度またやってみましょう。ボクも次までに鍛えておきますよ。");

                            md.Message = "＜ヴェルゼはワード・オブ・パワーを習得しました＞";
                            md.ShowDialog();
                            tc.WordOfPower = true;
                        }
                        if (tc.Level >= 12 && !we.AlreadyLvUpEmpty31)
                        {
                            UpdateMainMessage("ヴェルゼ：美しい・・・このバランス。");

                            UpdateMainMessage("アイン：ヴェルゼ、どうしたんだ？変な一人事なんか言って。");

                            UpdateMainMessage("ヴェルゼ：アイン君、見てください。このバランス。");

                            UpdateMainMessage("ヴェルゼ：四葉のクローバー。幸せの象徴でありなおかつ、相手へは究極の不幸の訪れ。");

                            UpdateMainMessage("ヴェルゼ：綺麗に開けた葉の間の究極的なバランス。命名規則も意味づけも完璧・・・あぁ罪だ・・・");

                            UpdateMainMessage("ラナ：アイン、アイン。これってひょっとして？");

                            UpdateMainMessage("アイン：ああ、例のアレだ。閃き０ってやつだな・・・");

                            UpdateMainMessage("ヴェルゼ：アイン君もラナさんもそう思いませんか？これこそが愛、そして罪だと。");

                            UpdateMainMessage("アインとラナ：え？え・・・えぇ、そ、そうですね・・・");

                            UpdateMainMessage("ヴェルゼ：この稀少さ・・・なんて罪なんだ・・・あぁ、まるでボクのようだ。");

                            UpdateMainMessage("アイン：放っておくか。全然上の空だしな。");

                            UpdateMainMessage("ラナ：ヴェルゼさん、次は閃きますように。");

                            md.Message = "＜ヴェルゼは何も習得できませんでした＞";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty31 = true;
                        }
                        if (tc.Level >= 13 && !tc.EnigmaSence)
                        {
                            UpdateMainMessage("ヴェルゼ：ＬＶ１３・・・いい数字ですね。");

                            UpdateMainMessage("ラナ：ヴェルゼさんって本当は何でも覚えているんじゃないんですか？");

                            UpdateMainMessage("ヴェルゼ：何でもとはどういう意味でしょう？");

                            UpdateMainMessage("ラナ：伝説のFiveSeeker全盛期時代の魔法・スキル全部ですよ。");

                            UpdateMainMessage("ヴェルゼ：少し誤解があったかも知れませんね。補足しておきましょう。");

                            UpdateMainMessage("アイン：誤解？");

                            UpdateMainMessage("ヴェルゼ：実際に体で経験してきたモノが次々と思い出してくれる感じですね。");

                            UpdateMainMessage("ヴェルゼ：頭では忘れている。でも体は覚えている。そんな所です。");

                            UpdateMainMessage("ラナ：やっぱりそうなんですね。何だか、習得する時の動作が良すぎるんで。");

                            UpdateMainMessage("アイン：確かにヴェルゼの習得時の動きはハンパじゃねえしな。");

                            UpdateMainMessage("ヴェルゼ：今回はEnigmaSenceですね、このパワー源はいまだに解明されていませんが、");

                            int maxValue = Math.Max(tc.StandardStrength, Math.Max(tc.StandardAgility, tc.StandardIntelligence));
                            if (maxValue == tc.StandardStrength)
                            {
                                UpdateMainMessage("ヴェルゼ：今のボクでは力、" + maxValue.ToString() + "が一番高いですね。");
                            }
                            else if (maxValue == tc.StandardAgility)
                            {
                                UpdateMainMessage("ヴェルゼ：今のボクでは技、" + maxValue.ToString() + "が一番高いですね。");
                            }
                            else
                            {
                                UpdateMainMessage("ヴェルゼ：今のボクでは知、" + maxValue.ToString() + "が一番高いですね。");
                            }

                            UpdateMainMessage("ヴェルゼ：では、行きますよ。ハァ！");

                            UpdateMainMessage("アイン：相変わらず早えよな・・・一瞬バックステップしたかと思ったら前進してる。");

                            UpdateMainMessage("ラナ：私のエニグマ・センスとちょっと構えが違うわね。");

                            UpdateMainMessage("ヴェルゼ：ボクにとっては、バックステップが一番良い感じですからね。");

                            UpdateMainMessage("ヴェルゼ：少し攻撃力に不満を感じた時はこれを使って行きますよ。");

                            md.Message = "＜ヴェルゼはエニグマ・センスを習得しました＞";
                            md.ShowDialog();
                            tc.EnigmaSence = true;
                        }
                        if (tc.Level >= 14 && !tc.BlackContract)
                        {
                            UpdateMainMessage("ヴェルゼ：ＬＶ１４・・・そろそろこの辺で・・・。");

                            UpdateMainMessage("アイン：ヴェルゼ、その雰囲気・・・闇魔法か？");

                            UpdateMainMessage("ヴェルゼ：心配しないでください。こういうモノなんですよボクの場合は・・・。");

                            UpdateMainMessage("ヴェルゼ：悪魔契約です、ボクにとってはある意味快感ですからね。");

                            UpdateMainMessage("ヴェルゼ：悪魔、ッハハハ、良い響きです。BlackContract！");

                            UpdateMainMessage("ラナ：ヴェルゼさん・・・雰囲気が変わりますよね。");

                            UpdateMainMessage("ヴェルゼ：先ほども言いましたが、心配無用です。こういう雰囲気の方が出しやすいんです。");

                            UpdateMainMessage("ヴェルゼ：確か、魔法とスキルのコストが０でしたね。少しやってみますか。");

                            UpdateMainMessage("アイン：おっと、俺が実験台でやるぜ。");

                            UpdateMainMessage("ヴェルゼ：ええ、受けてください。ダブル・スラッシュ！");

                            UpdateMainMessage("アイン：おっと、ッグ！　イツツ・・・");

                            UpdateMainMessage("ヴェルゼ：　っと、　　ここで、　　ダブル・スラッシュ！");

                            UpdateMainMessage("アイン：うお！？　またかよ、ってえええええ・・・");

                            UpdateMainMessage("ヴェルゼ：まだ行けます、ダブル・スラッシュ！");

                            UpdateMainMessage("アイン：ウグッ！　グアアァ！");

                            UpdateMainMessage("ラナ：何か今の・・・ダブル・スラッシュ＊３、かなり手厳しいわね。");

                            UpdateMainMessage("ヴェルゼ：コストを気にしなくて良いですからね。たたみかけるには最適でしょう。");

                            UpdateMainMessage("アイン：くっそ！それにしたって、全然スキがねえ。");

                            UpdateMainMessage("ヴェルゼ：ラナさんもこの魔法を持ってますね、自分なりのキメ打ち持っておくと良いですよ。");

                            UpdateMainMessage("ラナ：ええ、一点集中が良さそうね。考えておくわ。");

                            md.Message = "＜ヴェルゼはブラック・コントラクトを習得しました＞";
                            md.ShowDialog();
                            tc.BlackContract = true;
                        }
                        if (tc.Level >= 15 && !tc.Cleansing)
                        {
                            UpdateMainMessage("ヴェルゼ：ボクもようやくＬＶ１５ですね。");

                            UpdateMainMessage("アイン：ヴェルゼは攻撃・回復・特殊系とバランスが良いよな。");

                            UpdateMainMessage("ヴェルゼ：良いトコ取りに見えますか？それでも、結構不自由なものですよ。");

                            UpdateMainMessage("ラナ：治癒系もひょっとして出来ちゃったりするんですか？");

                            UpdateMainMessage("ヴェルゼ：ハハハ、ラナさんに一本取られてしまいましたね。");

                            UpdateMainMessage("ヴェルゼ：今回このCleansingを思い出していた所ですよ。");

                            UpdateMainMessage("ラナ：良かったわ。これで私が調子悪い時でもヴェルゼさんが居ればＯＫね♪");

                            UpdateMainMessage("アイン：くそ、俺は水魔法なんて出来ねえからな。うらやましいな、何か。");

                            UpdateMainMessage("ヴェルゼ：いえ、アイン君は攻め続ける方が良いと思いますよ。");

                            UpdateMainMessage("ヴェルゼ：３人ともが防衛的な魔法ばかりでは、戦闘の軸が出来ませんからね。");

                            UpdateMainMessage("アイン：ああ、まあ俺が調子悪い時は、スマンが頼りにしてるぜ！");

                            UpdateMainMessage("ヴェルゼ：ええ、任せておいてください。");

                            md.Message = "＜ヴェルゼはクリーンジングを習得しました＞";
                            md.ShowDialog();
                            tc.Cleansing = true;
                        }
                        if (tc.Level >= 16 && !tc.GaleWind)
                        {
                            UpdateMainMessage("ヴェルゼ：１６という数字もなかなか良いと思いませんか？");

                            UpdateMainMessage("アイン：良いと言うと、どういう意味だ？");

                            UpdateMainMessage("ヴェルゼ：ハハハ、まあ個人的な価値観です。気にしないでください。");

                            UpdateMainMessage("ヴェルゼ：さて、ここは一つボクの基本中の基本を習得するとしましょう。");

                            UpdateMainMessage("ヴェルゼ：ボクが全盛期時代は、よくこれをやっていましたね。GaleWindです。");

                            UpdateMainMessage("ラナ：わあ・・・ほとんど一瞬で出たわ・・・凄いわね。");

                            UpdateMainMessage("【ヴェルゼ：ワード・オブ・パワー！】　　　　【？？？：ワード・オブ・パワー！】");

                            UpdateMainMessage("アイン：なるほど、力押しでなおかつ、防御不可のスペルが２回か。");

                            UpdateMainMessage("ヴェルゼ：今のはほんの一旦に過ぎませんが、このスペルは最強の部類に入ると思います。");

                            UpdateMainMessage("アイン：そうか？確かに強力そうだが、ただの２回行動みたいなもんだからな。");

                            UpdateMainMessage("ヴェルゼ：ちょっと、今のアイン君では無理かもしれませんが。");

                            UpdateMainMessage("アイン：ん？");

                            UpdateMainMessage("ヴェルゼ：まあ種明かしは出来ませんが、見ててください。");

                            UpdateMainMessage("　　　『ヴェルゼがふと間を置いたかと思わせた・・・その瞬間！』");

                            UpdateMainMessage("【ヴェルゼ：ダブル・スラッシュ！】　　　　【？？？：ワード・オブ・パワー！】");

                            UpdateMainMessage("アイン：っな！！！　何だと！！？");

                            UpdateMainMessage("ラナ：違うモーションだったわ。　アイン見極められた？");

                            UpdateMainMessage("アイン：っば・・・バカな！！　ありえねえだろ今のは！？");

                            UpdateMainMessage("ヴェルゼ：詠唱のタイミングと初期モーションに関するテクニック連携です。");

                            UpdateMainMessage("アイン：どうやったんだよ。そんなのが可能なのか！？");

                            UpdateMainMessage("ヴェルゼ：ちょっと高度な技術を要するかもしれませんが、可能です。");

                            UpdateMainMessage("アイン：くそ・・・こんなの見せられちゃ黙ってられねえ。意地でも食らいついてやるぜ。");

                            UpdateMainMessage("ヴェルゼ：また見たい時は言ってください。いつでもお見せします。");

                            md.Message = "＜ヴェルゼはゲイル・ウィンドを習得しました＞";
                            md.ShowDialog();
                            tc.GaleWind = true;
                        }
                        if (tc.Level >= 17 && !tc.FrozenLance)
                        {
                            UpdateMainMessage("ヴェルゼ：ＬＶ１７ですね。さて、今回はどれでいきましょうか。");

                            UpdateMainMessage("ラナ：ヴェルゼさん、水魔法も使えるんですよね？");

                            UpdateMainMessage("ヴェルゼ：ええ、行けますよ。そうですね、今回は水魔法の主力をやってみましょう。");

                            UpdateMainMessage("ヴェルゼ：確かこの紋でしたね。ッハイ。");

                            UpdateMainMessage("　　　『ッピキキ！シャアアァァーーー！！』　　");

                            UpdateMainMessage("ラナ：うわあぁ・・・何か綺麗な螺旋状でしたね。");

                            UpdateMainMessage("ヴェルゼ：すいません、どうやら真っ直ぐに飛ばせてないようですね。");

                            UpdateMainMessage("アイン：螺旋状ってむしろどうやって飛ばすんだ？");

                            UpdateMainMessage("ヴェルゼ：螺旋状に飛ばす際は、手を突き出す時に、肘の部分を少し回転させるんですよ。");

                            UpdateMainMessage("アイン：なるほど、そうやると螺旋状になるのか・・・おっしゃ、俺もやってみっか！");

                            UpdateMainMessage("アイン：ええっとだな・・・っしゃ！ファイア・ボール！");

                            UpdateMainMessage("ラナ：うわあぁ・・・何か綺麗な直線だったわね・・・アイン。");

                            UpdateMainMessage("アイン：ッハッハッハ！・・・っくそう！");

                            UpdateMainMessage("ヴェルゼ：螺旋状は特に威力が上がったりはしません、すこし豪華に見せるくらいです。");

                            UpdateMainMessage("アイン：しかし見た目的な印象も大事だからな。");

                            UpdateMainMessage("ラナ：えっと・・・あれ？");

                            UpdateMainMessage("ヴェルゼ：　　？　　　何でしょう？");

                            UpdateMainMessage("ラナ：えっと、ヴェルゼさん、真っ直ぐ飛ばせるんですか？");

                            UpdateMainMessage("ヴェルゼ：ええ、真っ直ぐはこうですね。ッハイ。");

                            UpdateMainMessage("ラナ：・・・う～ん、何だろう。");

                            UpdateMainMessage("ヴェルゼ：どうかしましたか？");

                            UpdateMainMessage("ラナ：アイン、もう一回ファイア・ボール撃ってみて。");

                            UpdateMainMessage("アイン：ん？おお任せておけ・・・ファイア・ボール！");

                            UpdateMainMessage("ラナ：・・・やっぱり・・・");

                            UpdateMainMessage("アイン：何だよ？もったいぶるな。");

                            UpdateMainMessage("ラナ：同じ直線だけど、構え方、初期モーション、飛び方、速度、何もかもが全然違うわね。");

                            UpdateMainMessage("アイン：そりゃあ、そうだろ。何だ今更？");

                            UpdateMainMessage("ラナ：う、ううん、何でもないわ。");

                            UpdateMainMessage("ヴェルゼ：人によって個人差はあります。多分それに気付いたんでしょう。");

                            UpdateMainMessage("ヴェルゼ：ラナさんとボクの打ち方も違いますからね、お互い練習に励みましょう。");

                            UpdateMainMessage("ラナ：う、うんうん。そうよ。個人差ね、見せてくれてありがと♪");

                            md.Message = "＜ヴェルゼはフローズン・ランスを習得しました＞";
                            md.ShowDialog();
                            tc.FrozenLance = true;
                        }
                        if (tc.Level >= 18 && !tc.InnerInspiration)
                        {
                            UpdateMainMessage("ヴェルゼ：ＬＶ１８になりましたね。");

                            UpdateMainMessage("アイン：ヴェルゼの全盛期時代ってさ。ランディのボケも一緒に居たんだろ？");

                            UpdateMainMessage("ヴェルゼ：ボケとは過激な表現ですね、ハハハ。確かにボケたところもありましたが。");

                            UpdateMainMessage("ヴェルゼ：でもスタイルや戦闘技術はどれをとってもストレートなものばかりでしたよ。");

                            UpdateMainMessage("ヴェルゼ：思いだしました。戦闘中にボクがこれをやると彼はよく怒ってました。");

                            UpdateMainMessage("『ランディス：アーティ！！てめぇ、それバッカだな！！！』");

                            UpdateMainMessage("ラナ：アーティ？");

                            UpdateMainMessage("ヴェルゼ：ボクの下の呼び名です。ヴェルゼは発音がし辛くて、嫌だったようです。");

                            UpdateMainMessage("アイン：結局どんなのをやっていたんだ？");

                            UpdateMainMessage("ヴェルゼ：体内に内在している精神力を引き出す事でスキルを回復するInnerInspirationです。");

                            UpdateMainMessage("ヴェルゼ：ボクの場合、結構スキルを多段活用するので、よくこれを使うんですよ。");

                            UpdateMainMessage("アイン：確かにヴェルゼの場合、スキル攻撃が結構豊富だよな。");

                            UpdateMainMessage("ラナ：ヴェルゼさんって、それでもスキルが枯渇する事ってあまりないですよね？");

                            UpdateMainMessage("ヴェルゼ：戦闘時はイザという時のためにある程度溜めておく方ですからね。");

                            UpdateMainMessage("ヴェルゼ：しかしこのスキルがあればその心配もなくなります。戦術の幅はぐんと拡がります。");

                            UpdateMainMessage("ラナ：アインも少しは見習いなさいよ？バカみたいに使ってすぐ０にしないように♪");

                            UpdateMainMessage("アイン：はいはい、わーっかりましたよ。っくそう、覚えてろよ。");

                            md.Message = "＜ヴェルゼはインナー・インスピレーションを習得しました＞";
                            md.ShowDialog();
                            tc.InnerInspiration = true;
                        }
                        if (tc.Level >= 19 && !we.AlreadyLvUpEmpty32)
                        {
                            UpdateMainMessage("ヴェルゼ：ＬＶ２０まであと一歩と迫りましたよ。");

                            UpdateMainMessage("ラナ：ヴェルゼさんは、お花とかは好きですか？");

                            UpdateMainMessage("ヴェルゼ：花は・・・正直な所、好きではありません。");

                            UpdateMainMessage("ラナ：あ、そうなんですか。まあ好みの問題だし、しょうがないわね。");

                            UpdateMainMessage("ヴェルゼ：永遠ってあると思いますか？");

                            UpdateMainMessage("ラナ：え？え、ええ・・・っと、無いかな♪");

                            UpdateMainMessage("ヴェルゼ：この世に生まれてきた生命は全て永遠ではありません。");

                            UpdateMainMessage("ヴェルゼ：ラナさん、花からは永遠を連想しませんか？");

                            UpdateMainMessage("ヴェルゼ：その美しさ、とても人の手で創れるものではありません。");

                            UpdateMainMessage("ヴェルゼ：しかし、この美しさも必ず終わりが訪れます。");

                            UpdateMainMessage("ヴェルゼ：永遠を感じさせる。だが永遠ではない。");

                            UpdateMainMessage("ヴェルゼ：ああ・・・花もまた、ボクのようだ・・・");
                            
                            UpdateMainMessage("ヴェルゼ：罪が・・・罪が隠せないでいる。");

                            UpdateMainMessage("ラナ：・・・　・・・");

                            UpdateMainMessage("アイン：（おい・・・おい、ラナ）");

                            UpdateMainMessage("ラナ：（何よ？）");

                            UpdateMainMessage("アイン：（お前、何の話をしたんだ？）");

                            UpdateMainMessage("ラナ：（お花よ・・・別に変な話をしたつもりはないんだけど）");

                            UpdateMainMessage("ヴェルゼ：ずっと咲き続けていられたら・・・いいや駄目だ。それじゃ駄目なんだ。");

                            UpdateMainMessage("ヴェルゼ：ボクは・・・ボクは最低だ！！！あああぁぁぁ！！！");

                            UpdateMainMessage("ラナ：（・・・行きましょ、今回は私が悪かったと思うわ。）");

                            UpdateMainMessage("アイン：（おまえも、ヴェルゼのあの状態知ってるだろうが、気をつけろよな）");

                            UpdateMainMessage("ラナ：（うん・・・お花は禁句ね・・・）");

                            UpdateMainMessage("ヴェルゼ：咲いていた時のあなた、とても綺麗でした。");

                            UpdateMainMessage("ヴェルゼ：ボクはずっとあなたを忘れません。「想い描けば永遠」でしたよね・・・");

                            md.Message = "＜ヴェルゼは何も習得できませんでした＞";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty32 = true;
                        }
                        if (tc.Level >= 20 && !tc.FlameStrike)
                        {
                            UpdateMainMessage("ヴェルゼ：やりました。ＬＶ２０到達です。");

                            UpdateMainMessage("アイン：やるじゃねえか、ヴェルゼ！");

                            UpdateMainMessage("ヴェルゼ：ハハハ、こう見えてなかなか嬉しいものなんですよ。");

                            UpdateMainMessage("アイン：ＬＶ２０到達記念では、何を習得するんだ？");

                            UpdateMainMessage("ヴェルゼ：習得するのは、アイン君と同じFlameStrikeにしようと前から決めていました。");

                            UpdateMainMessage("アイン：ッゲ！マジかよ！？");

                            UpdateMainMessage("ラナ：アインも習得したＬＶは２０だったわね。まさに同じタイミングね♪");

                            UpdateMainMessage("ヴェルゼ：ラナさんもアイン君も離れていてください。");

                            UpdateMainMessage("ヴェルゼ：では、行きます。一瞬で焼き尽くせ、FlameStrikeです。");

                            UpdateMainMessage("      『ッシュウウウゥゥ・・・ッゴゴオオオォォ！！！！』");

                            UpdateMainMessage("アイン：！！！！！");

                            UpdateMainMessage("ヴェルゼ：こんなものでしょう。久しぶりでした、やはり爽快ですね。");

                            UpdateMainMessage("アイン：い、今の確かに・・・");

                            UpdateMainMessage("ヴェルゼ：どうかしましたか？");

                            UpdateMainMessage("アイン：俺のとは全く違うな。");

                            UpdateMainMessage("ヴェルゼ：同じ魔法であったとしても、詠唱者が違えば差異は出てきますね。");

                            UpdateMainMessage("ヴェルゼ：ボクのは少しひねった軌道を描いて対象物に向かいますが");

                            UpdateMainMessage("ヴェルゼ：アイン君の場合は、対象物まで直線軌道で進んでいます。");

                            UpdateMainMessage("ヴェルゼ：カール爵のは面白いですよ？加速する形で綺麗な円を描きます。");

                            UpdateMainMessage("ラナ：それにしてもいつも思うんだけど、ホンット早いわ・・・");

                            UpdateMainMessage("ラナ：バカアインの２倍ぐらいのスピードじゃない？");

                            UpdateMainMessage("アイン：確かに俺よりは遥かに早ぇ・・・");

                            UpdateMainMessage("ヴェルゼ：前にも何度か言っていますが、初期モーションというのが重要です。");

                            UpdateMainMessage("ヴェルゼ：ボクの場合は、詠唱開始する時と手足の紋の記述をほぼ同時に始める事にしています。");

                            UpdateMainMessage("ヴェルゼ：詠唱時に手を動かしたりするのは気が乱れる人もいるそうですが");

                            UpdateMainMessage("ヴェルゼ：カール爵のレベルにでもならない限り、そうそうダメージ量が変わる事はありません。");

                            UpdateMainMessage("ヴェルゼ：アイン君も一度やってみませんか？");

                            UpdateMainMessage("ヴェルゼ：こうです。こうして・・・この時からもう詠唱ですね・・・この直前で");

                            UpdateMainMessage("アイン：・・・いや、後で教えてくれ・・・");

                            md.Message = "＜ヴェルゼはフレイム・ストライクを習得しました＞";
                            md.ShowDialog();
                            tc.FlameStrike = true;
                        }
                        if (tc.Level >= 21 && !tc.AntiStun)
                        {
                            UpdateMainMessage("ヴェルゼ：ＬＶ２１になりました。");

                            UpdateMainMessage("ラナ：ヴェルゼさん、前回はアインと同じ魔法を習得してたわね。");

                            UpdateMainMessage("ラナ：今度はちょっと私と同じモノを習得は出来ないかしら？");

                            UpdateMainMessage("アイン：お前さ、人の閃きや習得するものに対して、お願いなんかするなよな。");

                            UpdateMainMessage("ラナ：良いじゃない、別に。");

                            UpdateMainMessage("ヴェルゼ：ハハハ、ラナさん面白い事を言いますね。ええ、良いですよ。");

                            UpdateMainMessage("ヴェルゼ：そうですね、じゃあAntiStunにしてみましょう。");

                            UpdateMainMessage("ヴェルゼ：AntiStunはスタン効果に対しての耐性が付くのは知ってますね？");

                            UpdateMainMessage("ラナ：うん、ダミー素振り君で一度立証済みよ♪");

                            UpdateMainMessage("ヴェルゼ：しかしこのAntiStunは決定的な弱点があります。アイン君分かりますか？");

                            UpdateMainMessage("アイン：攻撃ダメージではない！");

                            UpdateMainMessage("ヴェルゼ：ハズレです。ラナさんは？");

                            UpdateMainMessage("ラナ：う～ん、何だろう・・・タイミング次第って所かしら？");

                            UpdateMainMessage("ヴェルゼ：半分正解ですね。じゃあボクから少し解説しましょう。");

                            UpdateMainMessage("ヴェルゼ：このスキル、先制を取れないと戦術としては十分な効果が発揮されません。");

                            UpdateMainMessage("ヴェルゼ：よく考えてみてください。最初の攻撃でスタン攻撃が出来るとしたら");

                            UpdateMainMessage("ヴェルゼ：それを実行して、相手がスタン攻撃への耐性を持ってるか知ろうと思いませんか？");

                            UpdateMainMessage("アイン：そうか、分かったぜ。");

                            UpdateMainMessage("アイン：先制を取ってないとAntiStunが出来ていない状態で相手からスタン攻撃がくる。");

                            UpdateMainMessage("アイン：スタン耐性を付けようとしてんのに、最初食らったら付けれねえからな。");

                            UpdateMainMessage("アイン：って事で、最初の一発が止めれないようじゃ意味がねえって話だろ。違うか？");

                            UpdateMainMessage("ラナ：・・・っえ？そうなの？");

                            UpdateMainMessage("ヴェルゼ：そのとおりです。アイン君、やはりセンスが良いですね。");

                            UpdateMainMessage("ヴェルゼ：一番初めにスタン耐性が付けられるようになれば戦術の組み合わせは拡がりますよ。");

                            UpdateMainMessage("ラナ：ヴェルゼさんって行動が早いから、大抵は先行よね。");

                            UpdateMainMessage("ヴェルゼ：ラナさんもこれを使う時はなるべく先行が取れるようにしといてください。");

                            UpdateMainMessage("ラナ：うん、分かったわ。ホンットためになったわ、ありがと♪");

                            UpdateMainMessage("ヴェルゼ：アイン君も将来ＤＵＥＬをする時は頭に叩き込んでおいてください。");

                            UpdateMainMessage("アイン：オーケーオーケー！");

                            md.Message = "＜ヴェルゼはアンチ・スタンを習得しました＞";
                            md.ShowDialog();
                            tc.AntiStun = true;
                        }
                        if (tc.Level >= 22 && !tc.WordOfFortune)
                        {
                            UpdateMainMessage("ヴェルゼ：ＬＶ２２と順調に上がっていますね。");

                            UpdateMainMessage("アイン：ヴェルゼってたまに一人客観セリフを言う時あるよな。");

                            UpdateMainMessage("ヴェルゼ：変な口癖ですから、気にしないでください。");

                            UpdateMainMessage("ヴェルゼ：さて、どうやら今回の思い出した内容は魔法のようですね。");

                            UpdateMainMessage("ヴェルゼ：これは・・・WordOfFortuneですね！やりましたよ！");

                            UpdateMainMessage("アイン：何だ、やけに嬉しそうだな？");

                            UpdateMainMessage("ヴェルゼ：それはそうですよ、何せボクの全盛期時代の常用スキルですから。");

                            UpdateMainMessage("ヴェルゼ：この100%クリティカルというのはボクにとっては欠かせない存在です。");

                            UpdateMainMessage("ラナ：珍しいわね、ヴェルゼさんがこんなに喜ぶなんて。");

                            UpdateMainMessage("ヴェルゼ：クリティカルと言うのは、基本ダメージの底が３倍になるものです。");

                            UpdateMainMessage("ヴェルゼ：当時のボクはこの魔法にとても惹かれましたね。");

                            UpdateMainMessage("ヴェルゼ：この魔法を使う頃から、ボクの戦術が確立し始めたといっても過言ではありません。");

                            UpdateMainMessage("アイン：クリティカルにも戦術なんてものがあるのか？");

                            UpdateMainMessage("ヴェルゼ：そうですね、例えばアイン君のライフが残り５００だとしましょう。");

                            UpdateMainMessage("ヴェルゼ：ボクの攻撃が基本値は５０だとした場合、アイン君は１０回まで受け止められます。");

                            UpdateMainMessage("アイン：いや、いやいや、そういうのは分かるけどな。戦術ってのがピンとこねえんだ。");

                            UpdateMainMessage("ヴェルゼ：実際やってみれば分かりますかね。ラナさんちょっと離れててください。");

                            UpdateMainMessage("ヴェルゼ：アイン君、では行きますよ。何でも使って結構です。");

                            UpdateMainMessage("アイン：体で分からせるってか、上等だ！行くぜ！！");

                            UpdateMainMessage("      『ッガキイ！　ッガッガガガガ！　パッシイイィィン！（戦闘が繰り広げられる）』");

                            UpdateMainMessage("アイン：ッチ・・・フレッシュヒール！");

                            UpdateMainMessage("ヴェルゼ：そして次が最後でしょう、ワード・オブ・フォーチュンから");

                            UpdateMainMessage("アイン：！！！っしまっ！");

                            UpdateMainMessage("ヴェルゼ：ダブル・スラッシュです。ッハァ！");

                            UpdateMainMessage("      『ッブヴゥン、ッガシイイィン！！！』");

                            UpdateMainMessage("アイン：ッグ、グハァ！！！・・・イテテ");

                            UpdateMainMessage("ラナ：ウソ、何よ今の・・・ヒールしてるタイミングとほぼ同時だわ。");

                            UpdateMainMessage("ヴェルゼ：アイン君、ヒールするタイミングを見誤りましたね。");

                            UpdateMainMessage("アイン：ったく、かなわねえぜ。あんなタイミングで出されたら。");

                            UpdateMainMessage("ヴェルゼ：この魔法は戦闘の波・バランスを突然崩す魔法です。");

                            UpdateMainMessage("ラナ：アイン・・・さすがにキツイわね。私も多分同じ目に会ってると思うわ。");

                            UpdateMainMessage("ヴェルゼ：普段どおりの戦闘をやっていると足元を掬われるので気をつけてください。");

                            md.Message = "＜ヴェルゼはワード・オブ・フォーチュンを習得しました＞";
                            md.ShowDialog();
                            tc.WordOfFortune = true;
                        }
                        if (tc.Level >= 23 && !tc.Glory)
                        {
                            UpdateMainMessage("ヴェルゼ：ＬＶ２３ですが、ここでひとつ、アイン君に質問です。");

                            UpdateMainMessage("アイン：うお！？俺かよ？何だ何だ？");

                            UpdateMainMessage("ヴェルゼ：FlameStrike、WordOfFortune、そして今回思い出したGlory。");

                            UpdateMainMessage("アイン：グローリーを思い出したってか。ホント俺の覚える順序のままだな。");

                            UpdateMainMessage("ヴェルゼ：ハハハ、確かにそうでした。偶然ですね。");

                            UpdateMainMessage("ヴェルゼ：さて、このGloryですが、気づいた事を全て言ってみてください。");

                            UpdateMainMessage("アイン：全部かよ。そうだなあ・・・");

                            UpdateMainMessage("アイン：回復　＋　アタック！！");

                            UpdateMainMessage("アイン：ダメージレースで有効だ。");

                            UpdateMainMessage("アイン：一時的効果なので３ターンぐらいで消えちまう。");

                            UpdateMainMessage("アイン：後はそうだなあ・・・　・・・");

                            UpdateMainMessage("ヴェルゼ：アイン君、それは気づいた事ではなく、魔法の特性を述べているだけです。");

                            UpdateMainMessage("ヴェルゼ：魔法・スキルがどう扱われるのが最適かを発見することが気づく事です。");

                            UpdateMainMessage("ラナ：ヴェルゼさん、そこのバカアインには不可能問題よ。");

                            UpdateMainMessage("ヴェルゼ：いいえ、そうとも限りません。ボクから見るとアイン君、センスは確かです。");

                            UpdateMainMessage("ラナ：そ、そう？う～ん、そんな事無いと思うけど・・・アイン、どう？");

                            UpdateMainMessage("アイン：・・・　そうだな。");

                            UpdateMainMessage("アイン：直接攻撃が避けられたりした場合でも、ライフゲインにはなる。");

                            UpdateMainMessage("アイン：だから俺の方は直接攻撃が当たらなかったとしても損はしねえ。");

                            UpdateMainMessage("アイン：むしろ、基本攻撃が低くチマチマ削ろうとするヤツにとっちゃ災難だろうな。");

                            UpdateMainMessage("ヴェルゼ：すばらしいですね、さすがアイン君です。");

                            UpdateMainMessage("ラナ：アインってたまに変な事言ってるけど、的を得ている時もあるのね。");

                            UpdateMainMessage("ヴェルゼ：そういった事を幾つも積み重ねてみてください。");

                            UpdateMainMessage("アイン：ああ、毎回アドバイスしてくれて助かるぜ、サンキュー。");

                            md.Message = "＜ヴェルゼはグローリーを習得しました＞";
                            md.ShowDialog();
                            tc.Glory = true;
                        }
                        if (tc.Level >= 24 && !tc.Tranquility)
                        {
                            UpdateMainMessage("ヴェルゼ：ボクもそこそこ上がってきました。ＬＶ２４になりました。");

                            UpdateMainMessage("ラナ：ヴェルゼさんって思い出す順番に法則性はあるんですか？");

                            UpdateMainMessage("ヴェルゼ：どうでしょうね、そもそも思い出すのに順番は決められないと思います。");

                            UpdateMainMessage("ラナ：ッフフ、そうもそうね♪　今回はどんなのを思い出せるの？");

                            UpdateMainMessage("ヴェルゼ：Tranquilityのようです。これも中々好きですね。");

                            UpdateMainMessage("ヴェルゼ：DispelMagicとは違う部分を打ち消してくれるのはありがたいです。");

                            UpdateMainMessage("ラナ：でもTranquilityで消せる部分を使ってくる相手って中々いないわね。");

                            UpdateMainMessage("ラナ：しかも一時的効果だから無理して打たなくても良いって気もするわ。");

                            UpdateMainMessage("ヴェルゼ：モンスターがこの手の戦術を使ってくるケースは極稀です。");

                            UpdateMainMessage("ヴェルゼ：Tranquilityで消せる魔法というのは大抵一定の戦術パターンがあります。");

                            UpdateMainMessage("ヴェルゼ：どれもこれも強力なパターンですから、分かっていても防げない場合");

                            UpdateMainMessage("ラナ：そうね、この魔法で打ち消してしまえば、リズムを狂わせられそうね。");

                            UpdateMainMessage("ヴェルゼ：リズムを狂わせる・・・良い響きです。");

                            UpdateMainMessage("ヴェルゼ：戦闘に慣れて来たものは一定の戦術パターン型にはまりがちですから");

                            UpdateMainMessage("ヴェルゼ：彼らが編み出す戦術が、この魔法で崩れ去る瞬間は・・・何とも罪です。");

                            UpdateMainMessage("ラナ：あの・・・ヴェルゼさん");

                            UpdateMainMessage("ヴェルゼ：罪だと思いませんか？この魔法そのものが・・・静穏とは名ばかり。");

                            UpdateMainMessage("ヴェルゼ：相手の心理状態を激しく揺さぶります。あ、ああぁぁ狂わせてしまった・・・");

                            UpdateMainMessage("ラナ：・・・しまったわ、「リズム」「狂う」も禁句みたい・・・");

                            md.Message = "＜ヴェルゼはトランキィリティを習得しました＞";
                            md.ShowDialog();
                            tc.Tranquility = true;
                        }
                        if (tc.Level >= 25 && !tc.MirrorImage)
                        {
                            UpdateMainMessage("ヴェルゼ：ＬＶ２５です。さて、今回はどれで行きましょう。");

                            UpdateMainMessage("アイン：ヴェルゼが特別得意な魔法属性って何になるんだ？");

                            UpdateMainMessage("ヴェルゼ：器用貧乏ですからね、特別得意というのは難しいですが・・・");

                            UpdateMainMessage("アインとラナ：（器用貧乏はねえよな・・・）（ないわね・・・）");

                            UpdateMainMessage("ヴェルゼ：ボクの場合、水魔法ですね。");

                            UpdateMainMessage("ラナ：っえ？そうなの！？");

                            UpdateMainMessage("ヴェルゼ：ハイ、おそらく。あの凍てつく氷のイメージが一番扱いやすいです。");

                            UpdateMainMessage("ヴェルゼ：そうですね、MirrorImageなんてどうでしょう。やってみましょうか。");

                            UpdateMainMessage("アイン：っへえ・・・ヴェルゼのMirrorImage、何かすげえ黒青氷だな？");

                            UpdateMainMessage("ヴェルゼ：この魔法は奥が深いです。そういうイメージが具現化しているんだと思います。");

                            UpdateMainMessage("ヴェルゼ：カウンターとは違い、この魔法はダメージ系魔法を跳ね返します。");

                            UpdateMainMessage("ヴェルゼ：また事前に宣言する魔法ですから、通常はダメージ系魔法はもう打たれないでしょう。");

                            UpdateMainMessage("ヴェルゼ：そう錯覚してしまいそうになりませんか？ラナさん。");

                            UpdateMainMessage("ラナ：私もそう思ったのよね最初・・・");

                            UpdateMainMessage("アイン：何だ、違うのかよ？");

                            UpdateMainMessage("ヴェルゼ：最初は必ずはね返せます。つまり相手は最初手ごろな魔法を打ってきて");

                            UpdateMainMessage("ヴェルゼ：その後で大きいダメージの魔法を打ち込めばいいわけです。");

                            UpdateMainMessage("ヴェルゼ：MirrorImageをかけている側は安心しているでしょうから、");

                            UpdateMainMessage("ヴェルゼ：こういった攻撃に対しては意外と無防備になりがちなんですよ。");

                            UpdateMainMessage("ラナ：そうなのよ。だから私もあんまり過度にこの魔法には期待できなくて");

                            UpdateMainMessage("ヴェルゼ：それで良いんですよラナさん。");

                            UpdateMainMessage("ヴェルゼ：過度な期待は出来ない。しかし相手も必ず一発は当てないといけない。");

                            UpdateMainMessage("ヴェルゼ：要するに相手が何かしらダメージ系魔法を打ってきたとしたら、");

                            UpdateMainMessage("ヴェルゼ：次は大きいダメージ魔法を構えている。それをこちらに伝えているようなものです。");

                            UpdateMainMessage("ヴェルゼ：ここでブラフを仕掛けている場合も想定されますが、普通そこまではしませんね。");

                            UpdateMainMessage("ヴェルゼ：MirrorImageは端的に言えば、戦術の幅を広げるのではなく、相手の戦術幅を狭める。");

                            UpdateMainMessage("ヴェルゼ：そういうものだと解釈して使えば、かなり使い勝手は上がりますよ。");

                            UpdateMainMessage("ラナ：・・・え？ええっと・・・ありがと♪");

                            UpdateMainMessage("アイン：駄目だ。俺には全然分からなかったぞ・・・");

                            UpdateMainMessage("ヴェルゼ：すいません、つまらない内容でした。気軽に使って行きましょう。");

                            md.Message = "＜ヴェルゼはミラー・イメージを習得しました＞";
                            md.ShowDialog();
                            tc.MirrorImage = true;
                        }
                        if (tc.Level >= 26 && !tc.CrushingBlow)
                        {
                            UpdateMainMessage("ヴェルゼ：ＬＶ２６ですね。そろそろ習得した数も増えてきた所です。");

                            UpdateMainMessage("ラナ：ヴェルゼさん、こんな事聞くのも変ですけど・・・");

                            UpdateMainMessage("ヴェルゼ：何でしょうか？");

                            UpdateMainMessage("ラナ：どうしてヴェルゼさんは全盛期時代の習得したものを忘れてしまったの？");

                            UpdateMainMessage("ヴェルゼ：忘れたわけではありませんよ。");

                            UpdateMainMessage("ラナ：でも、今は思い出している最中なのよね？");

                            UpdateMainMessage("ヴェルゼ：ええ、そうですね。");

                            UpdateMainMessage("ラナ：う～ん・・・ちょっと分かんないんだけど・・・");

                            UpdateMainMessage("ヴェルゼ：ハハハ、すいません少し意地悪をしてしまいました。");

                            UpdateMainMessage("ヴェルゼ：ちょうど良い例え話があります。今回ボクが思い出したのは。");

                            UpdateMainMessage("ヴェルゼ：いきます、CrushingBlowです。");

                            UpdateMainMessage("      『ッガコオオォォン！　（アインはヴェルゼのCrushingBlowを食らった）』");

                            UpdateMainMessage("アイン：・・・（パタ）");

                            UpdateMainMessage("アイン：・・・");

                            UpdateMainMessage("アイン：・・・ッテテテ・・・いきなり何すんだヴェルゼ！？");

                            UpdateMainMessage("ヴェルゼ：アイン君、さっきラナさんとボクは何を話していました？");

                            UpdateMainMessage("アイン：ん？・・・っと、ちょっと待ってくれ・・・");

                            UpdateMainMessage("アイン：・・・あああぁぁぁ・・・・っと・・・");

                            UpdateMainMessage("アイン：ヴェルゼは全盛期時代のスキルを忘れたのかどうか？だったな。");

                            UpdateMainMessage("ヴェルゼ：ラナさん、ちょうどこんな感じです。");

                            UpdateMainMessage("ラナ：なるほど、なるほど♪　了解よ♪");

                            UpdateMainMessage("アイン：お前ら、俺を例え話の的にするなよな・・・");

                            md.Message = "＜ヴェルゼはクラッシング・ブローを習得しました＞";
                            md.ShowDialog();
                            tc.CrushingBlow = true;
                        }
                        if (tc.Level >= 27 && !tc.OneImmunity)
                        {
                            UpdateMainMessage("ヴェルゼ：ＬＶ２７にもなると少しＬＶ３０を期待してしまいますね。");

                            UpdateMainMessage("アイン：ヴェルゼ、おまえの覚えるスキルは結構個人的なのが多いな。");

                            UpdateMainMessage("ヴェルゼ：そうかも知れませんね。未だにパーティプレイなど少々苦手です。");

                            UpdateMainMessage("アイン：いや、そういう意味じゃねえけどさ。今回思い出すのはどんなのだ？");

                            UpdateMainMessage("ヴェルゼ：OneImmunityです。これはなかなか使い辛いものです。");

                            UpdateMainMessage("ヴェルゼ：確かに完全防御は有無を言わさず強力ですが、");

                            UpdateMainMessage("ヴェルゼ：使い所を間違えれば、相手にアドバンテージを与えるだけですからね。");

                            UpdateMainMessage("ヴェルゼ：ところでアイン君は防御系にはあまり興味はありませんか？");

                            UpdateMainMessage("アイン：今のところはな、あまり興味はねえ。これでもクソランディのせいだな。");

                            UpdateMainMessage("ヴェルゼ：そういえば、彼が師匠でしたね。ハハハ、すいませんそれじゃ興味はありませんね。");

                            UpdateMainMessage("アイン：でもその絶対防御ってのは本当にムカつく魔法だな。反則じゃねえのか？");

                            UpdateMainMessage("ヴェルゼ：この魔法は防御の構えをしないと、効果を発揮しないんですよ。");

                            UpdateMainMessage("ヴェルゼ：防御をしていると、攻撃は当然できません、十分フェアな内容です。");

                            UpdateMainMessage("ヴェルゼ：防御している間に相手がライフ回復やパラメタＵＰ系をしてきたら無駄になります。");

                            UpdateMainMessage("ヴェルゼ：そういう意味では、この魔法はそれほど相手に圧力は与えてるとは言えません。");

                            UpdateMainMessage("ヴェルゼ：ですが");

                            UpdateMainMessage("アイン：ん？");

                            UpdateMainMessage("ヴェルゼ：・・・内緒ですね。");

                            UpdateMainMessage("アイン：何だよそれ！？教えろよヴェルゼ！");

                            UpdateMainMessage("ヴェルゼ：今度ＤＵＥＬの機会でもあれば、お見せします。楽しみにしててください。");

                            UpdateMainMessage("アイン：マジかよ！っしゃ、その時は絶対やってもらうからな！");

                            md.Message = "＜ヴェルゼはワン・イムーニティを習得しました＞";
                            md.ShowDialog();
                            tc.OneImmunity = true;
                        }
                        if (tc.Level >= 28 && !tc.AetherDrive)
                        {
                            UpdateMainMessage("ヴェルゼ：ＬＶ２８ですね。");

                            UpdateMainMessage("アイン：俺さ、ヴェルゼに見極めてほしい事があるんだ。聞いてくれるか？");

                            UpdateMainMessage("ヴェルゼ：ボクで答えられる事なら、何でもどうぞ。");

                            UpdateMainMessage("アイン：パラメタＵＰ系は強いと思うか？");

                            UpdateMainMessage("ヴェルゼ：ええ、強いですよ。今回はAetherDriveを習得できそうです。");

                            UpdateMainMessage("アイン：ヴェルゼってそのままで十分強いだろ？");

                            UpdateMainMessage("ヴェルゼ：それは買いかぶりです。何度も言ってますが、相手への錯覚要素が強いだけです。");

                            UpdateMainMessage("ヴェルゼ：ですがアイン君の指摘どおり、今回ボクの習得するのは");

                            UpdateMainMessage("ヴェルゼ：物理攻撃２倍、物理防御半減で間違いなく強化魔法です。");

                            UpdateMainMessage("アイン：ヴェルゼの２倍攻撃が３ターン・・・考えたくもねえ内容だな。");

                            UpdateMainMessage("ラナ：ヴェルゼさんってタイミングが絶妙に良いのよね。");

                            UpdateMainMessage("アイン：ひょっとしてヴェルゼだけ５ターンぐらい継続されるんじゃねえだろうな？");

                            UpdateMainMessage("ヴェルゼ：ハハハ、それはさすに無いですよ。間違いなく継続時間は３ターンまでです。");

                            UpdateMainMessage("ヴェルゼ：アイン君、ボクはそんなに凄くありません。少し誇張をやめた方がいいです。");

                            UpdateMainMessage("アイン：そうだと良いけどな、俺はどうもヴェルゼはまだまだ手加減してる感がしてる。");

                            UpdateMainMessage("ヴェルゼ：手加減とは少し違いますね、制御していると捉えてください。");

                            UpdateMainMessage("ヴェルゼ：どんな力や技もやみくもな振りかざし方では駄目です。");

                            UpdateMainMessage("ヴェルゼ：このAetherDriveにしても、ただ単に３ターン攻撃せずとも");

                            UpdateMainMessage("ヴェルゼ：かかっているだけで相手を防衛的な思考に陥れる事が可能ですからね。");

                            UpdateMainMessage("アイン：そう考えられる所がすげえよな。俺なんかまだまだって感じだぜ。");

                            UpdateMainMessage("ラナ：アインもたまに変な事を言いながら似たような思考してるわよ？");

                            UpdateMainMessage("アイン：俺が！？冗談だろラナ、ッハッハッハ！");

                            UpdateMainMessage("ラナ：う～ん、気のせいかしらね。");

                            UpdateMainMessage("ヴェルゼ：・・・さて、今度の機会にまた一緒に考えてみましょう。");

                            md.Message = "＜ヴェルゼはエーテル・ドライブを習得しました＞";
                            md.ShowDialog();
                            tc.AetherDrive = true;
                        }
                        if (tc.Level >= 29 && !we.AlreadyLvUpEmpty33)
                        {
                            UpdateMainMessage("ヴェルゼ：ＬＶ２９ということは、３０まで後一つと迫りましたね。");

                            UpdateMainMessage("ラナ：ヴェルゼさん、ほら見て見て♪");

                            UpdateMainMessage("ヴェルゼ：何でしょう？");

                            UpdateMainMessage("アイン：待て、ラナ。");

                            UpdateMainMessage("ラナ：っえ？何よ？");

                            UpdateMainMessage("アイン：その、何だ。それ見せてヴェルゼの調子がまた狂わないだろうな？");

                            UpdateMainMessage("ラナ：大丈夫よこのぐらい。っささ、見てちょうだいヴェルゼさん♪");

                            UpdateMainMessage("ヴェルゼ：これは、首飾りですね。ラナさんが作ったのですか？");

                            UpdateMainMessage("ラナ：う～ん、私じゃないけどね。どう？");

                            UpdateMainMessage("ヴェルゼ：とても綺麗だと思います。ラナさんに似合いそうですよ。");

                            UpdateMainMessage("ラナ：ハート型の首飾りだし、ちょっと好みじゃないわね、私は。");

                            UpdateMainMessage("ヴェルゼ：そういえば、良く見るとハート型ですね・・・");

                            UpdateMainMessage("ヴェルゼ：ハート型は人々の感情を大きく揺さぶります。何故でしょうね？");

                            UpdateMainMessage("ラナ：心臓の形みたいだからじゃないかしら？何となくそれっぽい形してるし。");

                            UpdateMainMessage("ヴェルゼ：この形、存在そのものが・・・　・・・　愛を連想させるからです。");

                            UpdateMainMessage("アイン：（ほらみろ、やっぱり駄目じゃねえか、ラナ！どうすんだよ？）");

                            UpdateMainMessage("ラナ：（まあ見てなさいって、少しは予測してたんだから。元に戻してみせるわ）");

                            UpdateMainMessage("アイン：（マジかよ！？お前何でそんなとこ首突っ込んでんだよ。）");

                            UpdateMainMessage("ラナ：ヴェルゼさん、愛ってあると思います？");

                            UpdateMainMessage("ヴェルゼ：無いんですよ・・・愛は、存在しません。");

                            UpdateMainMessage("ラナ：っえ？");

                            UpdateMainMessage("ヴェルゼ：存在していたらおかしいでしょう？それを立証すれば愛は、愛ではなくなる。");

                            UpdateMainMessage("ラナ：え、でもそのハート型は愛を連想させるんでしょう？");

                            UpdateMainMessage("ヴェルゼ：ハート型は愛を思い起こさせます。しかし、愛はハートではありません。");

                            UpdateMainMessage("ラナ：っな・・・でも、ヴェルゼさんは愛が存在しなくても良いの？");

                            UpdateMainMessage("ヴェルゼ：愛は形でもイメージでも、姿でも、何者にも置き換えられないんです。");

                            UpdateMainMessage("ヴェルゼ：置き換えられたら・・・愛は・・・そこで消滅します");

                            UpdateMainMessage("ラナ：（・・・ック・・・なかなか手強いわね。）");

                            UpdateMainMessage("アイン：（おい、どんどんエスカレートしてるじゃねえか。もうその辺にしとけって）");

                            UpdateMainMessage("ラナ：愛は・・・在るわよ！");

                            UpdateMainMessage("ヴェルゼ：いや、存在しません・・・無いんですよ・・・だからこそ愛になるんです。");

                            UpdateMainMessage("ラナ：私よ！　私がヴェルゼさんのそばに居て、愛を教えてあげるわ！");

                            UpdateMainMessage("アイン：ッブバァ！！（アインはジュースを吐いた）");

                            UpdateMainMessage("ヴェルゼ：・・・本当に居てくれますか？セフィーネ。");

                            UpdateMainMessage("ラナ：えぇ、ずっとそばに居てあげるわ！");

                            UpdateMainMessage("ラナ：って、あれ！！？？　セフィーネって誰よ！？");

                            UpdateMainMessage("ヴェルゼ：その日から・・・まるで最初から存在してたかのように居たアナタへ");

                            UpdateMainMessage("ヴェルゼ：その日から・・・いつもハーブティでもてなしてくれたアナタへ");

                            UpdateMainMessage("ヴェルゼ：その日から・・・いつも綺麗なハートの首飾りを付けていたアナタへ");

                            UpdateMainMessage("ヴェルゼ：その日から・・・ずっと続く日々を愛で埋めてくれたアナタへ");

                            UpdateMainMessage("ラナ：（しまったわ・・・ハートの首飾り、完全にビンゴね・・・）");

                            UpdateMainMessage("アイン：（だから止めとけって言ったじゃねえか）");

                            UpdateMainMessage("ヴェルゼ：もう、あなたはいない・・・全てが現実、そして幻想。ゆえに愛が生まれる。");

                            UpdateMainMessage("ヴェルゼ：セフィーネ・・・ボクはここで永遠を");

                            UpdateMainMessage("ラナ：だ・・・駄目みたいね。");

                            UpdateMainMessage("アイン：ヴェルゼのヤツ、空中に何かものすごい回想シーンを描いてたぞ。");

                            UpdateMainMessage("アイン：途中からは、ラナの方に全く目を向けて無かったじゃねえか。");

                            UpdateMainMessage("アイン：ラナ、とにかくだ。ヴェルゼに対しては最大限に喋りかけ方に気を配れ。");

                            UpdateMainMessage("アイン：『その手に関連する』言葉、物、雰囲気、イメージを連想させるものは控えろ、良いな？");

                            UpdateMainMessage("ラナ：もー分かったわよ。。。でも、何があったのかしらね。");

                            UpdateMainMessage("アイン：【控えろよ】　分かったな？");

                            UpdateMainMessage("ラナ：分かったわよ・・・");

                            md.Message = "＜ヴェルゼは何も習得できませんでした＞";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty33 = true;
                        }
                        if (tc.Level >= 30 && !tc.Resurrection)
                        {
                            UpdateMainMessage("ヴェルゼ：やりましたよ。ついにＬＶ３０に到達です。");

                            UpdateMainMessage("アイン：やったな、ヴェルゼ。ＬＶ３０は記念に一発特別なのを思い出せそうか？");

                            UpdateMainMessage("ヴェルゼ：そうですね、コレといって思い当たるものはありませんが。");

                            UpdateMainMessage("ヴェルゼ：ああ、とっておきの良いのがありますね、あれにしましょう。");

                            UpdateMainMessage("アイン：あれって何だ？");

                            UpdateMainMessage("ヴェルゼ：アイン君がＬＶ３０で習得したResurrectionです。");

                            UpdateMainMessage("アイン：マジかよ！？ヴェルゼ本当に何でもこなせるんだな。やっぱすげえぜ！");

                            UpdateMainMessage("ラナ：双極に位置する属性はどうやって両方とも習得してるんですか？");

                            UpdateMainMessage("ヴェルゼ：本質が同じだという解説は覚えていますか？");

                            UpdateMainMessage("ラナ：う～ん、何となくだけど。ごめんなさい、良いのに覚えていないわ。");

                            UpdateMainMessage("ヴェルゼ：そうですね、FrozenLanceとFlameStrikeを例にとってみましょう。");

                            UpdateMainMessage("ヴェルゼ：FlameStrikeを放つ時は火のイメージをしますよね？");

                            UpdateMainMessage("アイン：ああ、そうだな。");

                            UpdateMainMessage("ヴェルゼ：そのイメージのまま放てば当然それはFlameStrikeとなります。");

                            UpdateMainMessage("ヴェルゼ：ラナさん、FrozenLanceを放つ時は水もしくは氷のイメージですよね？");

                            UpdateMainMessage("ラナ：ええ、そうね。");

                            UpdateMainMessage("ヴェルゼ：これも同じ手順です。さて、要はこの手順内で一つきっかけを与えます。");

                            UpdateMainMessage("アイン：きっかけ？");

                            UpdateMainMessage("ヴェルゼ：はい、アイン君の場合は火、ラナさんの場合は水。途中までは全く同じで良いです。");

                            UpdateMainMessage("ヴェルゼ：詠唱時では、反対の属性を打ち消せられるよう両方イメージします。");

                            UpdateMainMessage("ヴェルゼ：そして効果発動時において、打ち消そうと思った側のほうを強くイメージしてください。");

                            UpdateMainMessage("ヴェルゼ：その瞬間、発動される魔法が双極に位置する魔法が放たれます。");

                            UpdateMainMessage("アイン：ん・・・・んんん・・・・んんんんんんんんんん・・・・");

                            UpdateMainMessage("アイン：駄目だ！んなの無理だろ！？");

                            UpdateMainMessage("ラナ：私も・・・そんな簡単には出来ないわね。");

                            UpdateMainMessage("ヴェルゼ：このやり方はカール爵から教えてもらったものです。彼ならもっと上手く教えてくれますよ。");

                            UpdateMainMessage("ヴェルゼ：リザレクションもどちらかといえば、闇魔法の反対をイメージしてボクは放っています。");

                            UpdateMainMessage("ヴェルゼ：効果は同じなので、アイン君と少し詠唱方法が違いますが気にしないでください。");

                            UpdateMainMessage("アイン：いやあ・・・俺も今は出来ねえがいずれ出来るようになってみせるぜ。");

                            UpdateMainMessage("ヴェルゼ：ええ、その意気です。がんばって何度か施行してみてください。");

                            md.Message = "＜ヴェルゼはリザレクションを習得しました＞";
                            md.ShowDialog();
                            tc.Resurrection = true;
                        }
                        if (tc.Level >= 31 && !tc.CarnageRush)
                        {
                            UpdateMainMessage("ヴェルゼ：ＬＶ３１まで来ましたね。");

                            UpdateMainMessage("ラナ：ヴェルゼさんもＬＶ３０ぐらいから結構昇り調子になりそうですか？");

                            UpdateMainMessage("ヴェルゼ：そうですね、いろいろと思い当たる節も出てきましたよ。");

                            UpdateMainMessage("ヴェルゼ：アイン君と、最初の頃話をしていたものをやってみましょう。");

                            UpdateMainMessage("アイン：まさかとは思うが・・・");

                            UpdateMainMessage("ヴェルゼ：ラナさん、OneImmunityを張っておいてください。");

                            UpdateMainMessage("ラナ：え？ええ、良いわよ。");

                            UpdateMainMessage("　　　『ラナはOneImmunityを発動させ、防御の構えを取った。』");

                            UpdateMainMessage("ラナ：ハイ、準備完了よ♪");

                            UpdateMainMessage("ヴェルゼ：CarnageRushです、　１つ目");

                            UpdateMainMessage("ヴェルゼ：２、３，４・・・そして５つ！ハアアァァァ！！！");

                            UpdateMainMessage("　　　『ガッ、ガガガガアアァァ！！！』");

                            UpdateMainMessage("ラナ：・・・す、凄いわ・・・");

                            UpdateMainMessage("ヴェルゼ：このスキルもボクのお気に入りの一つですね。");

                            UpdateMainMessage("ヴェルゼ：スキル量はバカになりませんが、５回連続攻撃のCarnageRush。");

                            UpdateMainMessage("ヴェルゼ：InnerInspirationをうまく活用していけば、最高のバランスです。");

                            UpdateMainMessage("ラナ：なんだか動きが全然見えなかったわ。OneImmunityじゃないと防ぎようがないわね。");

                            UpdateMainMessage("アイン：今のダブルスラッシュの応用みたいなものか？");

                            UpdateMainMessage("ヴェルゼ：そうですね、でも少し違います。");

                            UpdateMainMessage("ヴェルゼ：連続攻撃の要素としては大きく分けて３つあります。");

                            UpdateMainMessage("ヴェルゼ：その中でも最も大きい要素としては");

                            UpdateMainMessage("アイン：いや・・・いい。俺なりのものを編み出して見せる。");

                            UpdateMainMessage("ラナ：私もそういうのやってみたいのよね、ヴェルゼさん今度教えてください♪");

                            UpdateMainMessage("ヴェルゼ：ええ、良いですよ。いつでも聞いてください。");

                            md.Message = "＜ヴェルゼはカルネージ・ラッシュを習得しました＞";
                            md.ShowDialog();
                            tc.CarnageRush = true;
                        }
                        if (tc.Level >= 32 && !tc.Catastrophe)
                        {
                            UpdateMainMessage("ヴェルゼ：ＬＶ３２になりました。いよいよＬＶ４０が見えてきましたね。");

                            UpdateMainMessage("アイン：ヴェルゼはもうＬＶ３０ぐらいがＭＡＸって事にしねえか？");

                            UpdateMainMessage("ヴェルゼ：ハハハ、面白い冗談ですね。さすがにそれは出来ません。");

                            UpdateMainMessage("アイン：今回はどんなのを見せてくれるんだ？");

                            UpdateMainMessage("ヴェルゼ：アイン君に以前間違えてやってしまったあれをやります。");

                            UpdateMainMessage("アイン：間違ってやってしまった・・・？");

                            UpdateMainMessage("ヴェルゼ：ええ、Catastropheです。アイン君、いきますよ、受けてみてください。");

                            UpdateMainMessage("アイン：っな！？いきなりかよ！！");

                            UpdateMainMessage("　　　『ッドスウウゥゥゥン！！！』　　");

                            UpdateMainMessage("アイン：ッフウゥ～～～・・・間一髪だったぜ。");

                            UpdateMainMessage("ラナ：え、アインひょっとして今の受け止められたの？");

                            UpdateMainMessage("アイン：いや、ダメージ自体は食らってる、だが即死レベルのダメージは回避できた。");

                            UpdateMainMessage("ヴェルゼ：さて、やはりアイン君はセンスが良いですね。");

                            UpdateMainMessage("ヴェルゼ：思い出した始めとは言え、これだけ受け止められたのは意外です。");

                            UpdateMainMessage("アイン：ヴェルゼの動きはいつも早すぎだからな。それに合わせるようにしてみただけだ。");

                            UpdateMainMessage("ヴェルゼ：・・・ハハ、ハハハハハ！さすがアイン君です。そう来てくれると、とても嬉しいです。");

                            UpdateMainMessage("ヴェルゼ：アイン君、次はもっと違うカタストロフィを編み出せるよう練習しておきます。");

                            UpdateMainMessage("ヴェルゼ：その時までアイン君も鍛錬を怠らないようお願いします。");

                            UpdateMainMessage("アイン：お、おお！任せておけ！？");

                            md.Message = "＜ヴェルゼはカタストロフィを習得しました＞";
                            md.ShowDialog();
                            tc.Catastrophe = true;
                        }
                        if (tc.Level >= 33 && !tc.Genesis)
                        {
                            UpdateMainMessage("ヴェルゼ：ＬＶ３３ですね。この辺からは中々上がり辛い感じがしてきます。");

                            UpdateMainMessage("アイン：ヴェルゼちょっと良いか！　　　　　ラナ：ねえ、ヴェルゼさん？");

                            UpdateMainMessage("ヴェルゼ：はい、何でしょう？２人とも。");

                            UpdateMainMessage("アイン：ラナ、お前先で良いぞ。");

                            UpdateMainMessage("ラナ：そう？じゃあ遠慮なく♪　ヴェルゼさんに魔法詠唱方法を習いたいの。");

                            UpdateMainMessage("ラナ：ヴェルゼさん、今回だけまたゆっくりと詠唱してもらえないかしら？");

                            UpdateMainMessage("ヴェルゼ：ええ、もちろん良いですよ。今回のはGenesisですからゆっくりでも構いませんね。");

                            UpdateMainMessage("ヴェルゼ：では、解説付きで行きます。まず詠唱時の初期モーション。");

                            UpdateMainMessage("ヴェルゼ：ここでは詠唱と同時に両手の紋を描きます。");

                            UpdateMainMessage("ラナ：待って、そこが早すぎるのよ。ごめんなさい、もう一回お願い。");

                            UpdateMainMessage("ヴェルゼ：良いですよ。こうやって・・・こう・・・で・・・こうですね。");

                            UpdateMainMessage("アイン：そうか、そんな風にやってたのか。全然分からなかったぜ。");

                            UpdateMainMessage("ヴェルゼ：で、次がターゲット選定と発動モーションですが、");

                            UpdateMainMessage("ヴェルゼ：ここもターゲット選定で体を向ける際に、発動モーションを同時に繰り出します。");

                            UpdateMainMessage("ラナ：そこも待って。ごめんなさいね、毎回止めちゃって。");

                            UpdateMainMessage("ヴェルゼ：構いませんよ。この程度の動作なら支障はありません。");

                            UpdateMainMessage("ラナ：発動モーションって絶対両足を固定させるわよね？");

                            UpdateMainMessage("ヴェルゼ：そんな事はありませんよ。");

                            UpdateMainMessage("ラナ：え？？");

                            UpdateMainMessage("ヴェルゼ：それは思い込みです。確かに発動モーション時は両足を固定させがちですが");

                            UpdateMainMessage("ヴェルゼ：発動が開始される直前までは動かしても何の影響もありません。");

                            UpdateMainMessage("ヴェルゼ：なので、ターゲット選定時に発動モーションを繰り広げれば良いんですよ。");

                            UpdateMainMessage("ラナ：へえ・・・そうだったんだ。私全然知らなかったわそんなの。");

                            UpdateMainMessage("ヴェルゼ：最後の発動時ですが、ココもポイントがあります。");

                            UpdateMainMessage("ヴェルゼ：実際の発動までのタイムラグを縮めるためには手を少しでも前に出してください。");

                            UpdateMainMessage("ヴェルゼ：人によりますが、ボクの場合、大抵この発動モーションの最後ぐらいで出しておきます。");

                            UpdateMainMessage("ヴェルゼ：そして・・・こう・・・ですかね。いよいよ発動ですね、Genesisです。");

                            UpdateMainMessage("ヴェルゼ：そしてFireBallです。");

                            UpdateMainMessage("ヴェルゼ：これで何回でもFireBallを連続詠唱になります。楽で良いですよね、この魔法。");

                            UpdateMainMessage("アイン：・・・すげえな。");

                            UpdateMainMessage("ラナ：う～ん、教えてもらったのは良いんだけど、イマイチ分からないわよね。");

                            UpdateMainMessage("ラナ：今のを全部やったとしてもあんなスピードにはならないと思うんだけど。");

                            UpdateMainMessage("アイン：確かにそうだ。今のを全部やってもヴェルゼの通りにはならねえ。");

                            UpdateMainMessage("ヴェルゼ：人によります。いろいろ試してみて、一番タイミングが合うのを探ってみてください。");

                            UpdateMainMessage("ラナ：あ、ありがとヴェルゼさん♪　また今度教えてください。");

                            UpdateMainMessage("ヴェルゼ：ええ、いつでもどうぞ。　さて、アイン君の方は何だったんでしょう？");

                            UpdateMainMessage("アイン：いや、俺はいい。またの機会にでも聞いてみるぜ。");

                            UpdateMainMessage("ヴェルゼ：そうですか。ではまた次の機会で。");

                            md.Message = "＜ヴェルゼはジェネシスを習得しました＞";
                            md.ShowDialog();
                            tc.Genesis = true;
                        }
                        if (tc.Level >= 34 && !we.AlreadyLvUpEmpty34)
                        {
                            UpdateMainMessage("ヴェルゼ：ＬＶ３４ですね。３４といえばアイン君、こんなのを知ってますか？");

                            UpdateMainMessage("アイン：ん？何がだ？");

                            UpdateMainMessage("　　　『ヴェルゼはおもむろに棒のきれはしで地面に何か描き始めた』");

                            UpdateMainMessage("　　　『四角形を描いた後、一つ一つ数値を書いているようだ』");

                            UpdateMainMessage("ヴェルゼ：魔方陣というものです。タテとヨコの和が必ず一致していれば完成です。");

                            UpdateMainMessage("アイン：ヨコとタテが同じ和？");

                            UpdateMainMessage("ヴェルゼ：はい、これで完成です。");

                            UpdateMainMessage("ラナ：ワア、何か綺麗ねこれ。魔方陣て言うの？");

                            UpdateMainMessage("ヴェルゼ：お互いの数値がお互いの和を知っているからこの数値になります。");

                            UpdateMainMessage("ヴェルゼ：１から１６までを使うタイプでは、４ｘ４の四角形の場合、合計の和は３４です。");

                            UpdateMainMessage("アイン：それが一体何になるってんだ？");

                            UpdateMainMessage("ヴェルゼ：何もありません。目的も何も無く、ただ純粋に存在するんです。");

                            UpdateMainMessage("ヴェルゼ：「純粋な存在」・・・そこには存在だけがあり続けるんです・・・綺麗ですよね");

                            UpdateMainMessage("ヴェルゼ：ラナさんも感じた「綺麗さ」が今そこにあります。");

                            UpdateMainMessage("ヴェルゼ：アナタは、これを無邪気に描こうとして何度も失敗していましたね。");

                            UpdateMainMessage("ヴェルゼ：アナタは、失敗に気づくのがとても遅く、失敗してから笑ってましたね。");

                            UpdateMainMessage("ヴェルゼ：アナタは、全部を消さずに一つ一つ直していく。とても無駄な時間でした。");

                            UpdateMainMessage("ヴェルゼ：アナタは、それを無駄な時間とは呼びませんでしたね、そう永遠に楽しい時間だと・・・");

                            UpdateMainMessage("アイン：（マズイ。　アナタ連打が始まったぞ・・・どうするラナ？）");

                            UpdateMainMessage("ラナ：（どうもこうも無いわよ。　もうジ・エンドじゃないのよこれ・・・）");

                            UpdateMainMessage("ヴェルゼ：セフィーネ、ボクもアナタと一緒にいる無駄な時間が永遠の楽しさに変わりました。");

                            UpdateMainMessage("ヴェルゼ：セフィーネ、どうして笑っているのか、ボクには全然分かっていませんでしたね。");

                            UpdateMainMessage("ヴェルゼ：セフィーネ、純粋な存在そのものが罪だったんです・・・あぁ・・・");

                            UpdateMainMessage("ラナ：ヴェルゼさん、あの～何か思いつきます？");

                            UpdateMainMessage("アイン：（ラナ、もはや無理やりだ・・・）");

                            UpdateMainMessage("ヴェルゼ：ええ、思いつきましたよ。");

                            UpdateMainMessage("ラナ：やったわ♪どんなのを思いつきました？");

                            UpdateMainMessage("ヴェルゼ：そこの数字を２つ足してみて、差し引いてみてはどうでしょうか？");

                            UpdateMainMessage("ヴェルゼ：そうするとホラ、こちらとあちらが３４になりますよね。");

                            UpdateMainMessage("ヴェルゼ：楽しいですね・・・この間違え方は本当に罪です・・・");

                            UpdateMainMessage("ラナ：・・・　・・・　どーすんのよコレ！！！？？？");

                            UpdateMainMessage("アイン：「魔方陣」とか「綺麗」もタブーだな。");

                            UpdateMainMessage("ラナ：ヴェルゼさん！このまんまじゃ駄目よ！？");

                            UpdateMainMessage("ヴェルゼ：ハハハ、すいません。怒らせてしまいましたね。");

                            UpdateMainMessage("ヴェルゼ：怒ったセフィーネも綺麗です。");

                            UpdateMainMessage("ラナ：あ～もううぅ＠＄％＃　ラチが空かないわ！！");

                            UpdateMainMessage("アイン：お、おいラナ。。。別に良いじゃねえか。");

                            UpdateMainMessage("ラナ：イイワケ無いでしょ！？何も習得しないのに何なのよコレは！？");

                            UpdateMainMessage("ラナ：次にこの『完全究極アッチ側モード』が発動されたら私が止めてみせるわ！！！");

                            UpdateMainMessage("アイン：・・・オーケーオーケー・・・じゃ俺はコレで・・・");

                            UpdateMainMessage("ラナ：アインも協力してよね！？ゼッタイだから！！！");

                            UpdateMainMessage("アイン：マジかよ・・・了解了解・・・");

                            md.Message = "＜ヴェルゼはなにも習得できませんでした＞";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty34 = true;
                        }
                        if (tc.Level >= 35 && !tc.ImmortalRave)
                        {
                            UpdateMainMessage("ヴェルゼ：ＬＶ３５になりました。いよいよＬＶ４０が少しだけ見えてきました。");

                            UpdateMainMessage("アイン：ヴェルゼちょっと良いか！？");

                            UpdateMainMessage("ヴェルゼ：そういえば前回何か聞こうとしてましたね。何でしょう？");

                            UpdateMainMessage("アイン：実はラナと同じ質問だったんだ。詠唱をゆっくりと見せてほしくてな。");

                            UpdateMainMessage("ヴェルゼ：それで一旦やめて置いたんですね。良いですよ何度でもお見せします。");

                            UpdateMainMessage("アイン：ああ、よろしく頼むぜ。");

                            UpdateMainMessage("ヴェルゼ：今回思いついているのは、ImmortalRaveですね。ではやってみましょう。");

                            UpdateMainMessage("ヴェルゼ：まず初期モーションですが");

                            UpdateMainMessage("アイン：待ってくれ。その魔法、俺も同じタイミングから一緒にやってみる。");

                            UpdateMainMessage("アイン：ラナ、遠くから見ててくれ。");

                            UpdateMainMessage("ラナ：ええ、良いわよ♪");

                            UpdateMainMessage("ヴェルゼ：まず初期モーションから来て・・・");

                            UpdateMainMessage("　　　『ヴェルゼがゆっくりと解説する間、アインはそれに追いつく形で詠唱する』");

                            UpdateMainMessage("ヴェルゼ：ッハイ、ImmortailRaveです。　　　　アイン：ッラァ！イモータルレイブ！！");

                            UpdateMainMessage("ラナ：っへえ、アイン意外と追いついているじゃない。");

                            UpdateMainMessage("アイン：ヴェルゼが解説付きでガイドしてくれてるからな。それに追いつけばいいわけだ。");

                            UpdateMainMessage("ラナ：でも私、ちょっと気づいたんだけど。");

                            UpdateMainMessage("ラナ：バカアインの詠唱って本当にヴェルゼさんと動きが完全に違ってるわよ？");

                            UpdateMainMessage("ラナ：確かに最後のタイミングはバッチリだったけどね・・・どういう事なのかしら？");

                            UpdateMainMessage("ヴェルゼ：人によって細部は違ってきます、多分その影響でしょう。");

                            UpdateMainMessage("ラナ：それにしても何か異様なくらい真逆で違うのよね。");

                            UpdateMainMessage("アイン：気にしすぎじゃねえのか？ヴェルゼが解説抜きだとタイミングは合わないしな。");

                            UpdateMainMessage("ラナ：まあそうなんだけど・・・ヴェルゼさんは何か思うところあります？");

                            UpdateMainMessage("ヴェルゼ：・・・特に気にはなりませんね。個人差は誰にでもありますから。");

                            UpdateMainMessage("ラナ：ふ～ん、私の勘違いなのかな。");

                            UpdateMainMessage("ヴェルゼ：このImmortalRaveは３ターンの特性をうまく使い切るのがコツです。");

                            UpdateMainMessage("ヴェルゼ：アイン君、今度お互いに練習してみましょう。");

                            UpdateMainMessage("アイン：ああ、了解だぜ。今回も解説してくれてサンキュー！");

                            UpdateMainMessage("ヴェルゼ：ラナさんも、別の魔法でいろいろと試してみましょう。");

                            UpdateMainMessage("ラナ：う～ん、うんうん。わかったわ♪");

                            md.Message = "＜ヴェルゼはイモータル・レイブを習得しました＞";
                            md.ShowDialog();
                            tc.ImmortalRave = true;
                        }
                        if (tc.Level >= 36 && !we.AlreadyLvUpEmpty35)
                        {
                            UpdateMainMessage("ヴェルゼ：ＬＶ３６到達です。ここからはさすがに上がり辛いですね。");

                            UpdateMainMessage("アイン：ヴェルゼでも辛いと思ったりする事はあるのか？");

                            UpdateMainMessage("ヴェルゼ：ええ、それなりに辛い時はありますよ。");

                            UpdateMainMessage("ラナ：何でも完璧にこなしちゃうから、そうは見えないのよね。");

                            UpdateMainMessage("ヴェルゼ：完璧にこなしているわけではありませんよ。");

                            UpdateMainMessage("ラナ：え？そうなの？");

                            UpdateMainMessage("ヴェルゼ：はい、ボクなんかまだまだですよ。");

                            UpdateMainMessage("ラナ：一番辛いと思ったのはどんな事だった？");

                            UpdateMainMessage("アイン：（おい、ラナ！）");

                            UpdateMainMessage("ラナ：え？何よアイン。");

                            UpdateMainMessage("ヴェルゼ：良いんですよ、答えは一つしかありません。");

                            UpdateMainMessage("ヴェルゼ：セフィーネが消え去った日です。");

                            UpdateMainMessage("ラナ：ご・・・ごめんなさい・・・・・・");

                            UpdateMainMessage("ヴェルゼ：気にしないでください。過去の出来事です。");

                            UpdateMainMessage("ヴェルゼ：セフィーネ。ボクの罪は・・・");

                            UpdateMainMessage("ラナ：（・・・よし、来たわね・・・『完全究極アッチ側モード』）");

                            UpdateMainMessage("アイン：（まさか、お前ワザとかよ？）");

                            UpdateMainMessage("ラナ：（当たり前じゃない？アイン、あんたも協力しなさいよ）");

                            UpdateMainMessage("アイン：（ったく、お前もとんだ物好きだな・・・）");

                            UpdateMainMessage("ラナ：ヴェルゼさん、こっち向いてください。");

                            UpdateMainMessage("ヴェルゼ：ボクの罪は・・・ボクが完璧過ぎた事でしたね。");

                            UpdateMainMessage("アイン：おいヴェルゼ。セフィーネが呼んでるぞ。");

                            UpdateMainMessage("ヴェルゼ：？");

                            UpdateMainMessage("セフィーネ（ラナ）：ヴェルゼさん、こっち向いてください。");

                            UpdateMainMessage("ヴェルゼ：セフィ・・・セフィですか！？");

                            UpdateMainMessage("セフィーネ（ラナ）：ええ、久しぶりね、ヴェルゼさん。");

                            UpdateMainMessage("ヴェルゼ：セフィ・・・久しぶりです。元気でしたか？");

                            UpdateMainMessage("アイン：（お前さ、セフィーネって女性の事知ってんのかよ？）");

                            UpdateMainMessage("ラナ：（し、知ってるわけないじゃない。適当よこんなのは）");

                            UpdateMainMessage("セフィーネ（ラナ）：ヴェルゼさん、ワタシの事覚えていてくれたんですね。");

                            UpdateMainMessage("ヴェルゼ：当然じゃないですか。一日も忘れた事などありません。");

                            UpdateMainMessage("セフィーネ（ラナ）：ワタシもヴェルゼさんのこと、ずっと覚えてたわよ。");

                            UpdateMainMessage("ラナ：（しまったわ・・・微妙に語尾が・・・ムツカシイわね）");

                            UpdateMainMessage("アイン：（そんなんで良くやるよな・・・）");

                            UpdateMainMessage("ヴェルゼ：セフィがたとえボクを忘れても、ボクはセフィを忘れません。");

                            UpdateMainMessage("セフィーネ（ラナ）：ヴェルゼさん、またあの時みたいに魔方陣描いてもらえる？");

                            UpdateMainMessage("ヴェルゼ：ええ、もちろんですよ。大きさは４ｘ４にしましょう。");

                            UpdateMainMessage("　　　『ヴェルゼはおもむろにラナの隣に座り、綺麗な魔方陣を描き始めた』");

                            UpdateMainMessage("セフィーネ（ラナ）：もー、せっかくこれなら完成すると思ったのに・・・");

                            UpdateMainMessage("ヴェルゼ：ハハハ、セフィ。それでは完成しませんよ。");

                            UpdateMainMessage("ヴェルゼ：角の対極に位置する場所に出来る限りバランスの良い数値を配置するんです。");

                            UpdateMainMessage("ヴェルゼ：ほら、こうやって・・・");

                            UpdateMainMessage("セフィーネ（ラナ）：あ！ダメ！ワタシが完成させるの！！");

                            UpdateMainMessage("ヴェルゼ：アッハハハ、良いですよ。好きなだけ試してみてください。");

                            UpdateMainMessage("　　　『そして、ヴェルゼとラナ（セフィーネ役）の楽しい一時が過ぎた』");

                            UpdateMainMessage("アイン：ヴェルゼ、セフィーネに伝え忘れた事、何かあったんじゃねえのか？");

                            UpdateMainMessage("ヴェルゼ：・・・セフィ、聞いてくれますか？");

                            UpdateMainMessage("セフィーネ（ラナ）：何？");

                            UpdateMainMessage("ヴェルゼ：セフィーネ。ボクはユング町のダンジョンに挑もうと思います。");

                            UpdateMainMessage("アイン：っな！？");

                            UpdateMainMessage("ヴェルゼ：セフィーネ。その時、ボクはアナタとしばらく会えなくなります。");

                            UpdateMainMessage("ヴェルゼ：ダンジョンから帰って来るまでの間、待っていてもらえますか？");

                            UpdateMainMessage("セフィーネ（ラナ）：・・・　・・・　・・・");

                            UpdateMainMessage("アイン：（おいラナ！！ポカーンとしてんじゃねえよ！？）");

                            UpdateMainMessage("セフィーネ（ラナ）：うん、ありがとう。待っているわ。約束ね♪");

                            UpdateMainMessage("ヴェルゼ：セフィ、これはボクからの贈り物です。受け取ってください。");

                            UpdateMainMessage("　　　『ラナが以前渡したハート型のペンダントを手渡された。』");

                            UpdateMainMessage("セフィーネ（ラナ）：ありがとう、ハート型は大好きよ。本当にありがとう。");

                            UpdateMainMessage("ヴェルゼ：セフィ、じゃあボクは行ってきます。必ず帰ってきますからその時まで・・・");

                            UpdateMainMessage("セフィーネ（ラナ）：うん、必ず・・・");

                            UpdateMainMessage("　　　『ヴェルゼはその場で意識を失った。』");

                            UpdateMainMessage("ラナ：・・・　・・・");

                            UpdateMainMessage("アイン：・・・　・・・");

                            UpdateMainMessage("ラナ：・・・");

                            UpdateMainMessage("アイン：・・・");

                            md.Message = "＜ヴェルゼは何も習得できませんでした＞";
                            md.ShowDialog();
                            we.AlreadyLvUpEmpty35 = true;
                        }
                        if (tc.Level >= 37 && !tc.AbsoluteZero)
                        {
                            UpdateMainMessage("ヴェルゼ：ＬＶ３７ですね。ここまでくるとＬＶ４０が待ち遠しいです。");

                            UpdateMainMessage("ラナ：ッフフ、ヴェルゼさんでも『待ち遠しい』とかってあるんですね。");

                            UpdateMainMessage("ヴェルゼ：ハハハ、それはそうですよ。ラナさん、ボクには無いとでも思ってたんですか？");

                            UpdateMainMessage("ラナ：ええっと、違うの。ほらヴェルゼさんっていつも冷静だし。");

                            UpdateMainMessage("ヴェルゼ：冷静でありたいとは常々心がけています。そのせいかも知れません。");

                            UpdateMainMessage("ヴェルゼ：そういえば、冷静で思い出しました。この魔法もボクのお気に入りです。");

                            UpdateMainMessage("ヴェルゼ：冷厳なる断罪天使、AbsoluteZeroです。");

                            UpdateMainMessage("　　　『ッパキキキィィィ、ッシュウウウウゥゥゥ・・・』");

                            UpdateMainMessage("アイン：ッゲ！俺が対象かよ！？");

                            UpdateMainMessage("ヴェルゼ：この魔法の特徴は複数ありますが、ある一つの概念が用いられています、何だと思います？");

                            UpdateMainMessage("ラナ：バカアインを凍り付けにする事♪");

                            UpdateMainMessage("ヴェルゼ：ハハハ、ラナさんは本当に面白い事を言いますね。");

                            UpdateMainMessage("ヴェルゼ：この魔法は『対抗』の概念を極力省く事に集約されています。");

                            UpdateMainMessage("ラナ：『対抗』？");

                            UpdateMainMessage("ヴェルゼ：そうです。例えばですが、アイン君、ちょっとすいません。");

                            UpdateMainMessage("アイン：おい待て、例えなんか要らねえだろ。");

                            UpdateMainMessage("ヴェルゼ：アイン君の場合、『対抗』に該当するのは唯一StanceOfEyesのみです。");

                            UpdateMainMessage("ヴェルゼ：しかし、今の彼はその構えを取る事が出来ません。");

                            UpdateMainMessage("ヴェルゼ：つまり、今の彼は必ず任意の行動を受けざるを得ません。");

                            UpdateMainMessage("ヴェルゼ：しかも防御する事もままなりませんから、対抗のしようがないわけです。さて・・・");

                            UpdateMainMessage("アイン：待て待て待て・・・");

                            UpdateMainMessage("ヴェルゼ：ハハハ、冗談です。さすがに今回は不意打ちだったので、これ以上は何もしません。");

                            UpdateMainMessage("ヴェルゼ：対抗を意識するが故に、何もしなければ、アイン君自身に何も起こりませんね。");

                            UpdateMainMessage("アイン：ふう・・・ビビったぜ、ホント。この魔法だけは勘弁してほしいぜ。");

                            UpdateMainMessage("アイン：何もさせてもらえないからな。正直動揺は隠し切れねえってとこだな。");

                            UpdateMainMessage("ラナ：一度決まってしまえば、確かにほとんど無抵抗ってところね。");

                            UpdateMainMessage("ヴェルゼ：この魔法は出会い頭、拮抗状態中、トドメなど多数の局面で使用できます。");

                            UpdateMainMessage("ヴェルゼ：敵が防衛的な場合にボクの方から積極的に使っていくとしましょう。");

                            md.Message = "＜ヴェルゼはアブソリュート・ゼロを習得しました＞";
                            md.ShowDialog();
                            tc.AbsoluteZero = true;
                        }
                        if (tc.Level >= 38 && !tc.CelestialNova)
                        {
                            UpdateMainMessage("ヴェルゼ：ＬＶ３８になりましたね。いよいよ後２つと迫りました。");

                            UpdateMainMessage("アイン：ヴェルゼのＬＶ４０ってのは正直戦いたくはねえな・・・");

                            UpdateMainMessage("ヴェルゼ：しかし、ＤＵＥＬ戦では大抵ＭＡＸレベルまで皆上げてきますね。");

                            UpdateMainMessage("アイン：ま、確かにそうだけどな。で、どうだ？");

                            UpdateMainMessage("ヴェルゼ：今回は・・・どうやらCelestialNovaのようです。これは良い魔法ですね。");

                            UpdateMainMessage("アイン：あの、回復と攻撃両方できるやつだな。確かにソイツは便利だ。");

                            UpdateMainMessage("ヴェルゼ：この魔法は確か聖属性でしたね？");

                            UpdateMainMessage("アイン：ああ、そうだな。それが何だ？");

                            UpdateMainMessage("ヴェルゼ：この魔法は攻撃側にも使えるのでちょっと闇属性と間違えやすいとは思いませんでしたか？");

                            UpdateMainMessage("アイン：そうか？特に気にはならなかったが。");

                            UpdateMainMessage("ヴェルゼ：まあそれはさておき、ラナさん。");

                            UpdateMainMessage("ラナ：え？私？");

                            UpdateMainMessage("ヴェルゼ：ラナさんは連続的な回復をしてくる相手にはどうするべきだと思いますか？");

                            UpdateMainMessage("ラナ：当然、殴りまくるわよ♪");

                            UpdateMainMessage("アイン：マジかよ・・・怖えな");

                            UpdateMainMessage("ラナ：だって、それ以外無いじゃない。アインも同じ答えでしょ？");

                            UpdateMainMessage("アイン：まあそうだな。");

                            UpdateMainMessage("ヴェルゼ：逆に考えてみてください。相手が攻撃ばかりしてくる場合はどうしますか？");

                            UpdateMainMessage("アイン：まあ死なない程度に攻撃しつつライフ回復だな。ダメージ量にもよるが。");

                            UpdateMainMessage("ヴェルゼ：今のラナさん、アイン君では無理かも知れませんが、こういうのがあります。");

                            UpdateMainMessage("　　　『ヴェルゼはCelestialNovaの魔法の構えをした。』");

                            UpdateMainMessage("ヴェルゼ：アイン君、ちょっとボクに攻撃しようとしてみてください。");

                            UpdateMainMessage("アイン：良いのかよ？遠慮なく行くぜ。");

                            UpdateMainMessage("ヴェルゼ：ええ、どうぞお構いなく。");

                            UpdateMainMessage("アイン：オラァ！");

                            UpdateMainMessage("ヴェルゼ：CelestialNovaです、回復しましたよ。");

                            UpdateMainMessage("アイン：オラァ！");

                            UpdateMainMessage("ヴェルゼ：CelestialNovaです、回復しましたよ。");

                            UpdateMainMessage("アイン：でもそれは当然だよな。");

                            UpdateMainMessage("ヴェルゼ：さて、どうですかね。続けてどうぞ。");

                            UpdateMainMessage("アイン：オラァ！");

                            UpdateMainMessage("ヴェルゼ：CelestialNova、アイン君へ攻撃です。");

                            UpdateMainMessage("アイン：オラァ！");

                            UpdateMainMessage("ヴェルゼ：CelestialNova、アイン君へ攻撃です。");

                            UpdateMainMessage("アイン：オラァ！");

                            UpdateMainMessage("ヴェルゼ：CelestialNovaです、回復しましたよ。");

                            UpdateMainMessage("アイン：オラァ！");

                            UpdateMainMessage("ヴェルゼ：CelestialNova、アイン君へ攻撃です。");

                            UpdateMainMessage("アイン：っち、CelestialNova、ライフ回復したぜ。");

                            UpdateMainMessage("ヴェルゼ：CelestialNova、アイン君へ攻撃です。");

                            UpdateMainMessage("アイン：っち、CelestialNova、ライフ回復したぜ。");

                            UpdateMainMessage("ヴェルゼ：CelestialNovaです、回復しましたよ。");

                            UpdateMainMessage("アイン：オラァ！");

                            UpdateMainMessage("ヴェルゼ：CelestialNova、アイン君へ攻撃です。");

                            UpdateMainMessage("アイン：っタイム！");

                            UpdateMainMessage("アイン：っくっそ・・・何か全然動きが読み辛え・・・。");

                            UpdateMainMessage("ラナ：うーん、確かに何だかアインが押され気味だったわね。何でよ？");

                            UpdateMainMessage("アイン：何か迷うんだよ、途中まで動きが同じだからのせいか、わかんねえけどよ。");

                            UpdateMainMessage("アイン：発動までのモーションが同じだからよ。テンポが狂わされる感じだ。");

                            UpdateMainMessage("ヴェルゼ：この手の戦闘方法は良く使われるものです。覚えておいてください。");

                            UpdateMainMessage("アイン：っち、かなわねえな、マジで。");

                            md.Message = "＜ヴェルゼはセレスティアル・ノヴァを習得しました＞";
                            md.ShowDialog();
                            tc.CelestialNova = true;
                        }
                        if (tc.Level >= 39 && !tc.LavaAnnihilation)
                        {
                            UpdateMainMessage("ヴェルゼ：よし、ＬＶ３９です。あと一つですね。");

                            UpdateMainMessage("アイン：ヴェルゼが“よし”とか言うと、何か笑っちまうな。ッハッハハハ！");

                            UpdateMainMessage("ヴェルゼ：さて・・・アイン君には久しぶりに豪華絢爛な魔法を浴びせるとしましょう。");

                            UpdateMainMessage("アイン：待て、ヴェルゼってそんな短気じゃねえよな？");

                            UpdateMainMessage("ヴェルゼ：そうですね、今回はとっておきの火魔法、LavaAnnihilationです。");

                            UpdateMainMessage("　　　『ゴゴゴォォオォオオオオオ！！！！！』　　");

                            UpdateMainMessage("アイン：ッゲエェェ！！俺の周囲一体全部かよ！！");

                            UpdateMainMessage("ヴェルゼ：この魔法は明確な対象をとりません。周囲に居るものは全てが対象となりえます。");

                            UpdateMainMessage("　　　『ッゴ・・・ゴパアアァァァン！！！！！』　　");

                            UpdateMainMessage("アイン：ア！アッツツツ、グアアァァァアチチチチ！！！！");

                            UpdateMainMessage("ヴェルゼ：そしてこの溶岩は不規則な動きで対象を取らずとも対象を焼き尽くします。");

                            UpdateMainMessage("アイン：わかった！分かったから止めてくれって！！アチチチチ！！！！");

                            UpdateMainMessage("ヴェルゼ：すみませんが、実はこの魔法、一旦始まると止めようがありません。");

                            UpdateMainMessage("ラナ：うーん、アイン、これはどうやら逃げ道がなさそうね。諦めたら？");

                            UpdateMainMessage("アイン：っくっそお！次からは・・・");

                            UpdateMainMessage("アイン：ッア！！　アッチイイィィィ！！！！");

                            UpdateMainMessage("ヴェルゼ：ラナさん、後でかまいませんので水魔法で冷やしてあげてください。");

                            UpdateMainMessage("ラナ：あ、はい喜んで♪");

                            UpdateMainMessage("アイン：待て！ラナの水魔法も攻撃専用のを俺に打つつもりだろ！？");

                            UpdateMainMessage("ラナ：大丈夫よ、対象はちゃんと取ってあげるから♪");

                            UpdateMainMessage("アイン：アッツ！アツツツツ！！！あああ、くっそおおおおぉぉ！！！");

                            UpdateMainMessage("ラナ：アイン撲滅奥義。　ハイパーフローズンランス改よ！");

                            UpdateMainMessage("ヴェルゼ：面白そうですね、ボクからも援護射撃です。フローズンランス・エクストリームです。");

                            UpdateMainMessage("　　　『ビキキキイイイィィ！！！！』　　『ピッキイイィィン！！！』");

                            UpdateMainMessage("アイン：ガアアアァァ！もう次から絶対からかいません！！！");

                            md.Message = "＜ヴェルゼはラヴァ・アニヒレーション習得しました＞";
                            md.ShowDialog();
                            tc.LavaAnnihilation = true;
                        }
                        if (tc.Level >= 40 && !tc.TimeStop)
                        {
                            UpdateMainMessage("ヴェルゼ：ＬＶ４０・・・ＭＡＸ到達です。");

                            UpdateMainMessage("ラナ：ヴェルゼさん、おめでとうございます♪");

                            UpdateMainMessage("アイン：やったなヴェルゼ、さすがとしか良いようがないぜ。");

                            UpdateMainMessage("ヴェルゼ：いえいえ、これもアイン君やラナさんが一緒に居てくれたおかげです。");

                            UpdateMainMessage("アイン：最後は何を習得するんだ？");

                            UpdateMainMessage("ヴェルゼ：ありきたりですが、ラナさんと同様、TimeStopです。");

                            UpdateMainMessage("アイン：・・・やべえ・・・マジかよ。ヴェルゼがそれを使ったらいよいよ反則だろ。");

                            UpdateMainMessage("ヴェルゼ：そうでもありませんよ。使いどころが肝心なだけです。");

                            UpdateMainMessage("ラナ：あ、ちょっと良いかしら？");

                            UpdateMainMessage("ヴェルゼ：はい、何でしょう？");

                            UpdateMainMessage("ラナ：ヴェルゼさんってGaleWind使えるわよね？");

                            UpdateMainMessage("ヴェルゼ：ラナさんも本当にセンスが良いですね。ラナさんの推測は当たっています。");

                            UpdateMainMessage("ラナ：２ターン分時間停止できちゃうわけ？？");

                            UpdateMainMessage("ヴェルゼ：そうなりますが、GaleWind自体に１ターン使ってるのも事実です。");

                            UpdateMainMessage("ラナ：うーん、さすがに無限ターンにはならないわけね。");

                            UpdateMainMessage("ヴェルゼ：それが可能になってしまったら、ＤＵＥＬ大会は消滅するでしょう。");

                            UpdateMainMessage("アイン：でもな・・・そういう組み合わせが出てきている時点で天才の領域だぜ、マジで。");

                            UpdateMainMessage("ヴェルゼ：ハハハ、何度か言ってますが、買いかぶりです。魔法だけはカール爵には叶いませんからね。");

                            UpdateMainMessage("ヴェルゼ：後は、切り札や奥の手と言ったものがまるで通用しないエルミにも叶いそうにはありません。");

                            UpdateMainMessage("アイン：ランディのボケはどうだ？");

                            UpdateMainMessage("ヴェルゼ：彼とは何度か交えていますが、勝てた試しはありませんね。");

                            UpdateMainMessage("ラナ：え、そうなんですか？");

                            UpdateMainMessage("ヴェルゼ：ええ、不服ですか？");

                            UpdateMainMessage("ラナ：ええっと、そういうわけじゃないんだけど・・・TimeStopを使ってもですか？");

                            UpdateMainMessage("ヴェルゼ：ええ。彼はそういうタイミングを全て知り尽くしています。");

                            UpdateMainMessage("ヴェルゼ：なので、TimeStopをかけられても、対応策を仕掛けた状態で時間停止に入っています。");

                            UpdateMainMessage("ラナ：そんな事が可能なんだ・・・やっぱり凄いわね、FiveSeeker。");

                            UpdateMainMessage("ヴェルゼ：そういう意味では、このTimeStopは、狙ってやるのはあまり効果的ではありません。");

                            UpdateMainMessage("ヴェルゼ：意外性が高く、特に意識を必要としない間を創り上げて放つのが効果的ですね。");

                            UpdateMainMessage("アイン：んなの、どうやって見極めるんだよ。ダメだ、話の次元が少し違ってきてるぜ。");

                            UpdateMainMessage("ヴェルゼ：アイン君なら大丈夫ですよ、もっと磨く事でおそらく付いて来れるはずです。");

                            UpdateMainMessage("ラナ：私はどう？");

                            UpdateMainMessage("ヴェルゼ：もちろん、ラナさんも立派について来れます。安心してください。");

                            UpdateMainMessage("ヴェルゼ：さて、これでボクもようやく全盛期時代に一歩戻った感じがします。");

                            UpdateMainMessage("アイン：一歩戻ったとは？");

                            UpdateMainMessage("ヴェルゼ：その先は、また今度の機会に詳しく話します。");

                            UpdateMainMessage("ヴェルゼ：今はこの状態をキープして、全力でダンジョンへ挑みましょう。");

                            md.Message = "＜ヴェルゼはタイムストップを習得しました＞";
                            md.ShowDialog();
                            tc.TimeStop = true;
                        }
                    }
                }
                #endregion

                #region "StandOfFlowがバカにされて怒るラナ"
                if (sc != null && sc.EmotionAngry)
                {
                    UpdateMainMessage("アイン：いや・・・このまま怒らせたままじゃ駄目だ。ちょっとラナの所に寄るぜ。");

                    UpdateMainMessage("アイン：・・・っお、いたいた。ラナ！");

                    UpdateMainMessage("ラナ：・・・何よ。");

                    UpdateMainMessage("アイン：ダ・・・ダンジョン行くぜ！");

                    UpdateMainMessage("ラナ：行ってくれば。");

                    UpdateMainMessage("アイン：そ、そうだそうだ。良いこと思いついたぜ。");

                    UpdateMainMessage("ラナ：・・・何？");

                    UpdateMainMessage("アイン：い、いやあソレなんだけどな・・・何て言うんだ。");

                    UpdateMainMessage("ラナ：・・・");

                    UpdateMainMessage("アイン：あぁっと・・・ま、待ってろよ・・・");

                    UpdateMainMessage("ラナ：・・・・・・ップ　");

                    UpdateMainMessage("ラナ：アッハハハハハ。ッフフフフ、あーおかしい、アッハハハハ、何やってんのよアイン");

                    UpdateMainMessage("アイン：なっ、何がおかしい！？");

                    UpdateMainMessage("ラナ：だってさ、怒りを鎮めるための行動は何も決めてないみたいだし。");

                    UpdateMainMessage("アイン：それは今考えてるんだっつうの。");

                    UpdateMainMessage("ラナ：思いついてないし・・・やっぱ、バカね。");

                    UpdateMainMessage("アイン：ッグ・・・");

                    UpdateMainMessage("ラナ：筋金入りねホント。普通は相手に会う前に考えとくものよ、覚えときなさい。");

                    UpdateMainMessage("アイン：オーケー。了解了解！");

                    UpdateMainMessage("ラナ：大体、アンタが最初に謝りに来てんのに、何でアンタがオーケーセリフなのよ。");

                    UpdateMainMessage("アイン：いや、何とか元に戻ってくれたみたいでさ、ほんとに安心したよ。");

                    UpdateMainMessage("ラナ：イチイチ怒った後、引きずってても何もないでしょ。元々怒ってないわけだし。");

                    UpdateMainMessage("アイン：いつ頃からだ？");

                    UpdateMainMessage("ラナ：最初の【・・・何よ】からに決まってるじゃない。");

                    UpdateMainMessage("アイン：マジかよ！最初すげー目が釣り上がってたじゃねえか！");

                    UpdateMainMessage("ラナ：そんなもの演技でしょうが、あー、やっぱりアレでビビったわけね♪");

                    UpdateMainMessage("アイン：んな事ねえ！俺がビビるわけねえだろ！ただ、ちょっとな・・・。");

                    UpdateMainMessage("ラナ：アッハハハハ、ゴメンゴメン♪　あ、そうそう、実はね。。。");

                    UpdateMainMessage("　　　【・・・ッガサ・・・ッゴソ】　　ラナ：あったわ。コレね。");

                    UpdateMainMessage("ラナ：ハイ、これでも付けてみろ。");

                    UpdateMainMessage("アイン：ん？おぉ、剣の紋章が入ってるな！良いじゃねえかっコレ！！！");

                    UpdateMainMessage("ラナ：ま、まあ、バカアインは剣バカだし、バカにはバカ紋章が一番お似合いバカって所よ。");

                    UpdateMainMessage("アイン：お前、どれだけバカって入れてんだよ。最後のバカはオカシイだろ。");

                    UpdateMainMessage("ラナ：オカシくないわ。あんたバカだからね。このぐらいがちょうど良いの♪");

                    UpdateMainMessage("アイン：ありがたく頂くぜ。いやいや、サンキュー！やったぜ！！すげええぇぇ！！！");
                    using (MessageDisplay md = new MessageDisplay())
                    {
                        mc.AddBackPack(new ItemBackPack("剣紋章ペンダント"));
                        md.Message = "【剣紋章ペンダント】を手に入れました。";
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    UpdateMainMessage("ラナ：どんだけ喜んでるのよ。ホラホラ、とっととダンジョン行くわよ♪");

                    UpdateMainMessage("アイン：お、おぉそうだったな。じゃ、行くとするか！！");
                   
                    sc.EmotionAngry = false;
                }
                #endregion
                #region "ヴェルゼが始めて参加する日"
                if (WE.AvailableThirdCharacter && !WE.CommunicationThirdChara1)
                {
                    UpdateMainMessage("アイン：よし・・・ついに３階スタートか。やってやるぜ！");

                    UpdateMainMessage("ヴェルゼ：お待ちしてましたよ、アイン君。");

                    UpdateMainMessage("ラナ：遅いわね、アイン。");

                    UpdateMainMessage("アイン：おお、揃ってるじゃねえか。ラナ、ガンツさんに期待に応えようぜ！");

                    UpdateMainMessage("ラナ：ええ、せっかく特訓してもらえる事になったんだし、頑張るわよ。");

                    UpdateMainMessage("アイン：ヴェルゼ様・・・いや、ヴェルゼ。今日からよろしく頼むぜ！");

                    UpdateMainMessage("ヴェルゼ：ええ、しかしあまり期待しないでくださいね？ボクはホントに弱いですよ。");

                    UpdateMainMessage("アイン：昨日の去り際にも言ってたが、どういう意味なんだ？");

                    UpdateMainMessage("ヴェルゼ：実は私の装備品が誰かに盗まれてしまったのですよ。");

                    UpdateMainMessage("ヴェルゼ：白銀の剣、黒真空の鎧、そして天空の翼・・・３つとも全てね。");

                    UpdateMainMessage("アイン：え！？・・・一体誰が・・・");

                    UpdateMainMessage("ヴェルゼ：ボクも迂闊でした。丁度ファージル宮殿内でふっと散歩している間でした。");

                    UpdateMainMessage("アイン：体力的な身体能力は？");

                    UpdateMainMessage("ヴェルゼ：さて、ダンジョン制覇の時期から見て随分経ちますからね。かなり衰えていますよ。");

                    UpdateMainMessage("アイン：いや、分かった。事前に教えてくれて助かるぜ。");

                    UpdateMainMessage("アイン：伝説のFiveSeekerと言えど、同じ人間だ。対等にやっていくつもりだ。よろしくなヴェルゼ！");

                    UpdateMainMessage("ヴェルゼ：ッハハ、アイン君は少しエルミに似ていますね。こちらこそよろしく。");

                    UpdateMainMessage("ラナ：ヴェルゼさんは、国王エルミ様と同い年なんですよね？");

                    UpdateMainMessage("ヴェルゼ：ボクとエルミですか？そうですよ、それが何か？");

                    UpdateMainMessage("ラナ：いえ、良いんです。それじゃよろしくお願いします♪");

                    UpdateMainMessage("ヴェルゼ：ええ、こちらこそ。");

                    UpdateMainMessage("アイン：おっしゃ、いくぜ！");

                    we.CommunicationThirdChara1 = true;
                }
                #endregion
                if (we.CompleteArea1)
                {
                    mainMessage.Text = "アイン：さて、何階から始めるかな。";
                    mainMessage.Update();
                    using (SelectDungeon sd = new SelectDungeon())
                    {
                        sd.StartPosition = FormStartPosition.Manual;
                        sd.Location = new Point(this.Location.X + 50, this.Location.Y + 50);
                        //if (we.CompleteArea5) sd.MaxSelectable = 5;
                        if (we.CompleteArea4) sd.MaxSelectable = 5;
                        else if (we.CompleteArea3) sd.MaxSelectable = 4;
                        else if (we.CompleteArea2) sd.MaxSelectable = 3;
                        else if (we.CompleteArea1) sd.MaxSelectable = 2;
                        sd.ShowDialog();
                        this.targetDungeon = sd.TargetDungeon;
                    }
                }
                if (this.targetDungeon == 1)
                {
                    if (!we.CompleteArea1)
                    {
                        mainMessage.Text = "アイン：今日こそ１階を突破するぜ！";
                    }
                    else
                    {
                        mainMessage.Text = "アイン：もう１度、１階でも探索するか。";
                    }
                }
                else if (this.targetDungeon == 2)
                {
                    if (!we.CompleteArea2)
                    {
                        mainMessage.Text = "アイン：目指すは２階を制覇だな！";
                    }
                    else
                    {
                        mainMessage.Text = "アイン：もう１度、２階でも探索するか。";
                    }
                }
                else if (this.targetDungeon == 3)
                {
                    if (!we.CompleteArea3)
                    {
                        mainMessage.Text = "アイン：いよいよ３階、気を引き締めていくぜ！";
                    }
                    else
                    {
                        mainMessage.Text = "アイン：もう１度、３階でも探索するか。";
                    }
                }
                else if (this.targetDungeon == 4)
                {
                    if (!we.CompleteArea4)
                    {
                        mainMessage.Text = "アイン：４階制覇やってみせるぜ！";
                    }
                    else
                    {
                        mainMessage.Text = "アイン：もう１度、４階でも探索するか。";
                    }
                }
                else if (this.targetDungeon == 5)
                {
                    if (!we.CompleteArea5)
                    {
                        mainMessage.Text = "アイン：最下層制覇、やってみせる！";
                    }
                    else
                    {
                        mainMessage.Text = "アイン：もう１度、５階でも探索するか。";
                    }
                }
                else if (this.targetDungeon == -1)
                {
                    this.targetDungeon = 1;
                    return;
                }
                mainMessage.Update();
                System.Threading.Thread.Sleep(1000);

                // ラナ、ガンツ、ハンナの一般会話完了はここで反映します。
                if (this.firstDay >= 1 && !we.CommunicationLana1 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana1 = true;
                else if (this.firstDay >= 2 && !we.CommunicationLana2 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana2 = true;
                else if (this.firstDay >= 3 && !we.CommunicationLana3 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana3 = true;
                else if (this.firstDay >= 4 && !we.CommunicationLana4 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana4 = true;
                else if (this.firstDay >= 5 && !we.CommunicationLana5 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana5 = true;
                else if (this.firstDay >= 6 && !we.CommunicationLana6 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana6 = true;
                else if (this.firstDay >= 7 && !we.CommunicationLana7 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana7 = true;
                else if (this.firstDay >= 8 && !we.CommunicationLana8 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana8 = true;
                else if (this.firstDay >= 9 && !we.CommunicationLana9 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana9 = true;
                else if (this.firstDay >= 10 && !we.CommunicationLana10 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana10 = true;
                else if (this.firstDay >= 11 && !we.CommunicationLana11 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana11 = true;
                else if (this.firstDay >= 12 && !we.CommunicationLana12 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana12 = true;
                else if (this.firstDay >= 13 && !we.CommunicationLana13 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana13 = true;
                else if (this.firstDay >= 14 && !we.CommunicationLana14 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana14 = true;
                else if (this.firstDay >= 15 && !we.CommunicationLana15 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana15 = true;
                else if (this.firstDay >= 16 && !we.CommunicationLana16 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana16 = true;
                else if (this.firstDay >= 17 && !we.CommunicationLana17 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana17 = true;
                else if (this.firstDay >= 18 && !we.CommunicationLana18 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana18 = true;
                else if (this.firstDay >= 19 && !we.CommunicationLana19 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana19 = true;
                else if (this.firstDay >= 20 && !we.CommunicationLana20 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyCommunicate) we.CommunicationLana20 = true;

                if (this.firstDay >= 1 && !we.CommunicationHanna1 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna1 = true;
                else if (this.firstDay >= 2 && !we.CommunicationHanna2 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna2 = true;
                else if (this.firstDay >= 3 && !we.CommunicationHanna3 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna3 = true;
                else if (this.firstDay >= 4 && !we.CommunicationHanna4 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna4 = true;
                else if (this.firstDay >= 5 && !we.CommunicationHanna5 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna5 = true;
                else if (this.firstDay >= 6 && !we.CommunicationHanna6 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna6 = true;
                else if (this.firstDay >= 7 && !we.CommunicationHanna7 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna7 = true;
                else if (this.firstDay >= 8 && !we.CommunicationHanna8 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna8 = true;
                else if (this.firstDay >= 9 && !we.CommunicationHanna9 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna9 = true;
                else if (this.firstDay >= 10 && !we.CommunicationHanna10 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna10 = true;
                else if (this.firstDay >= 11 && !we.CommunicationHanna11 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna11 = true;
                else if (this.firstDay >= 12 && !we.CommunicationHanna12 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna12 = true;
                else if (this.firstDay >= 13 && !we.CommunicationHanna13 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna13 = true;
                else if (this.firstDay >= 14 && !we.CommunicationHanna14 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna14 = true;
                else if (this.firstDay >= 15 && !we.CommunicationHanna15 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna15 = true;
                else if (this.firstDay >= 16 && !we.CommunicationHanna16 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna16 = true;
                else if (this.firstDay >= 17 && !we.CommunicationHanna17 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna17 = true;
                else if (this.firstDay >= 18 && !we.CommunicationHanna18 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna18 = true;
                else if (this.firstDay >= 19 && !we.CommunicationHanna19 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna19 = true;
                else if (this.firstDay >= 20 && !we.CommunicationHanna20 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyRest) we.CommunicationHanna20 = true;

                if (this.firstDay >= 1 && this.firstDay <= 4 && (!we.CommunicationGanz1 || !we.CommunicationGanz2 || !we.CommunicationGanz3 || !we.CommunicationGanz4) &&
                    mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop)
                {
                    switch (this.firstDay)
                    {
                        case 1:
                            we.CommunicationGanz1 = true;
                            break;
                        case 2:
                            we.CommunicationGanz1 = true;
                            we.CommunicationGanz2 = true;
                            break;
                        case 3:
                            we.CommunicationGanz1 = true;
                            we.CommunicationGanz2 = true;
                            we.CommunicationGanz3 = true;
                            break;
                        case 4:
                            we.CommunicationGanz1 = true;
                            we.CommunicationGanz2 = true;
                            we.CommunicationGanz3 = true;
                            we.CommunicationGanz4 = true;
                            break;
                        default:
                            we.CommunicationGanz1 = true;
                            we.CommunicationGanz2 = true;
                            we.CommunicationGanz3 = true;
                            we.CommunicationGanz4 = true;
                            break;
                    }
                }
                else if (this.firstDay >= 7 && !we.CommunicationGanz7 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz7 = true;
                else if (this.firstDay >= 8 && !we.CommunicationGanz8 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz8 = true;
                else if (this.firstDay >= 9 && !we.CommunicationGanz9 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz9 = true;
                else if (this.firstDay >= 10 && !we.CommunicationGanz10 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz10 = true;
                else if (this.firstDay >= 11 && !we.CommunicationGanz11 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz11 = true;
                else if (this.firstDay >= 12 && !we.CommunicationGanz12 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz12 = true;
                else if (this.firstDay >= 13 && !we.CommunicationGanz13 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz13 = true;
                else if (this.firstDay >= 14 && !we.CommunicationGanz14 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz14 = true;
                else if (this.firstDay >= 15 && !we.CommunicationGanz15 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz15 = true;
                else if (this.firstDay >= 16 && !we.CommunicationGanz16 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz16 = true;
                else if (this.firstDay >= 17 && !we.CommunicationGanz17 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz17 = true;
                else if (this.firstDay >= 18 && !we.CommunicationGanz18 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz18 = true;
                else if (this.firstDay >= 19 && !we.CommunicationGanz19 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz19 = true;
                else if (this.firstDay >= 20 && !we.CommunicationGanz20 && mc.Level >= 1 && knownTileInfo[2] && we.AlreadyEquipShop) we.CommunicationGanz20 = true;

                this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
        }




        /// <summary>
        /// if-else文とフラグを複雑化させて幼馴染みラナとの会話を盛り上げてください。
        /// </summary>
        private void button3_Click(object sender, EventArgs e)
        {
            #region "１日目"
            if (this.firstDay >= 1 && !we.CommunicationLana1 && mc.Level >= 1 && knownTileInfo[2])
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "ラナ：アイン、【遠見の青水晶】は戦闘中には使えないから注意してね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ああ。水晶の中を集中して見続けないと駄目だからな。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：・・・ガンツおじさんの所で売らないでよ？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：あのな。売るわけないだろ、こんな便利なモノ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：・・・寝ボケた状態で、無くさないでよ？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：『無くしました』なんてイベントでもない限りそれは無いな。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：・・・食べたりしないでよ？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：さすがにそこまでバカじゃねえ。";
                        ok.ShowDialog();
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                }
                else
                {
                    if (!WE.AlreadyCommunicate)
                    {
                        mainMessage.Text = "ラナ：アイン、【遠見の青水晶】は戦闘中には使えないから注意してね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ああ。水晶の中を集中して見続けないと駄目だからな。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：・・・ガンツおじさんの所で売らないでよ？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：あのな。売るわけないだろ、こんな便利なモノ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：・・・寝ボケた状態で、無くさないでよ？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：『無くしました』なんてイベントでもない限りそれは無いな。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：・・・食べたりしないでよ？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：さすがにそこまでバカじゃねえ。";
                        ok.ShowDialog();
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "２日目"
            else if (this.firstDay >= 2 && !WE.CommunicationLana2 && mc.Level >= 1 && knownTileInfo[2])
            {
                we.CommunicationFirstContact2 = true;
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        if (!we.OneDeny)
                        {
                            mainMessage.Text = "アイン：っよ、ラナ。今戻ったぞ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：ちょっとアイン。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：なんだよ？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：アンタ、ちゃんと赤ポーション使ってる？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：持ってるわけないだろ。なんだ、くれるのか？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：タダで渡すわけないじゃない。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：貴様、薬屋でも無いのに金を要求するのか！？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：ビンゴ♪";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：昔のよしみ・・・";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：今現在の話、５Ｇよ♪";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：おいおい・・・冗談だろ。買えってのかよ？";
                            using (YesNoRequestMini yesno = new YesNoRequestMini())
                            {
                                yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                yesno.ShowDialog();
                                if (yesno.DialogResult == DialogResult.Yes)
                                {
                                    mc.Gold -= 5;
                                    GetPotionForLana();
                                    mainMessage.Text = "アイン：ック・・・ほらよ。";
                                    ok.ShowDialog();
                                    mainMessage.Text = "ラナ：おぉ、ナイストレード♪";
                                    ok.ShowDialog();
                                    mainMessage.Text = "アイン：今に見てろ。近い将来、正式な伝統ある由緒正しいハイパー薬屋が出来るはずだ。";
                                    ok.ShowDialog();
                                    mainMessage.Text = "ラナ：何の神頼みをしてるのやら♪　毎度有り！";
                                    ok.ShowDialog();
                                    mainMessage.Text = "アイン：神よ・・・まっとうな薬屋を・・・";
                                    we.AlreadyCommunicate = true;
                                    we.CommunicationSuccess2 = true;
                                }
                                else
                                {
                                    mainMessage.Text = "アイン：不要だ";
                                    ok.ShowDialog();
                                    mainMessage.Text = "ラナ：えぇー？バカじゃないの？";
                                    ok.ShowDialog();
                                    mainMessage.Text = "アイン：金は貯めるに限る。こんな所で使うのは頭の悪いヤツがする事だ。";
                                    ok.ShowDialog();
                                    mainMessage.Text = "ラナ：バカじゃないの？";
                                    ok.ShowDialog();
                                    mainMessage.Text = "アイン：・・・ッハッハッハッハッハ";
                                    ok.ShowDialog();
                                    mainMessage.Text = "ラナ：無理しちゃってさ。次頼んでも駄目だからね。";
                                    ok.ShowDialog();
                                    mainMessage.Text = "アイン：ッハッハッハッハッハ・・・";
                                    ok.ShowDialog();
                                    we.OneDeny = true;
                                }
                            }
                        }
                        else
                        {
                            mainMessage.Text = "アイン：おい、ラナ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：何よ？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：何でもねえよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：赤ポーション欲しいんでしょ？５Ｇよ♪";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：くっそぉ・・・。しかしここは買うしか。";
                            using (YesNoRequestMini yesno = new YesNoRequestMini())
                            {
                                yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                yesno.ShowDialog();
                                if (yesno.DialogResult == DialogResult.Yes)
                                {
                                    mc.Gold -= 5;
                                    GetPotionForLana();
                                    mainMessage.Text = "アイン：ック・・・ほらよ。";
                                    ok.ShowDialog();
                                    mainMessage.Text = "ラナ：おぉ、ナイストレード♪";
                                    ok.ShowDialog();
                                    mainMessage.Text = "アイン：今に見てろ。近い将来、正式な伝統ある由緒正しいハイパー薬屋が出来るはずだ。";
                                    ok.ShowDialog();
                                    mainMessage.Text = "ラナ：何の神頼みをしてるのやら♪　毎度有り！";
                                    ok.ShowDialog();
                                    mainMessage.Text = "アイン：神よ・・・まっとうな薬屋を・・・";
                                    we.AlreadyCommunicate = true;
                                    we.CommunicationSuccess2 = true;
                                }
                                else
                                {
                                    mainMessage.Text = "アイン：何でもねえよ！！";
                                    ok.ShowDialog();
                                    mainMessage.Text = "ラナ：何よもう。面白い企画だと思ったのに・・・";
                                }
                            }
                        }
                    }
                    else
                    {
                        mainMessage.Text = "ラナ：赤ポーション一つ持った事だし、明日からはもっと進めるはずよね。";
                    }
                }
                else
                {
                    if (!we.AlreadyCommunicate)
                    {
                        if (!we.OneDeny)
                        {
                            mainMessage.Text = "アイン：よーし、じゃ行ってくるぜ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：ちょっとアイン。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：なんだよ？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：アンタ、赤ポーションっていう物を知ってる？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：知ってるが持ってないな。なんだ、くれるのか？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：タダで渡すわけないじゃない。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：貴様、薬屋でも無いのに金を要求するのか！？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：ビンゴ♪";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：昔のよしみ・・・";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：今現在の話、５Ｇよ♪";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：おいおい・・・冗談だろ。買えってのかよ？";
                            using (YesNoRequestMini yesno = new YesNoRequestMini())
                            {
                                yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                yesno.ShowDialog();
                                if (yesno.DialogResult == DialogResult.Yes)
                                {
                                    mc.Gold -= 5;
                                    GetPotionForLana();
                                    mainMessage.Text = "アイン：ック・・・ほらよ。";
                                    ok.ShowDialog();
                                    mainMessage.Text = "ラナ：おぉ、ナイストレード♪";
                                    ok.ShowDialog();
                                    mainMessage.Text = "アイン：今に見てろ。近い将来、正式な伝統ある由緒正しいハイパー薬屋が出来るはずだ。";
                                    ok.ShowDialog();
                                    mainMessage.Text = "ラナ：何の神頼みをしてるのやら♪　毎度有り！";
                                    ok.ShowDialog();
                                    mainMessage.Text = "アイン：神よ・・・まっとうな薬屋を・・・";
                                    we.AlreadyCommunicate = true;
                                    we.CommunicationSuccess2 = true;
                                }
                                else
                                {
                                    mainMessage.Text = "アイン：不要だ";
                                    ok.ShowDialog();
                                    mainMessage.Text = "ラナ：えぇー？バカじゃないの？";
                                    ok.ShowDialog();
                                    mainMessage.Text = "アイン：金は貯めるに限る。こんな所で使うのは頭の悪いヤツがする事だ。";
                                    ok.ShowDialog();
                                    mainMessage.Text = "ラナ：バカじゃないの？";
                                    ok.ShowDialog();
                                    mainMessage.Text = "アイン：・・・ッハッハッハッハッハ";
                                    ok.ShowDialog();
                                    mainMessage.Text = "ラナ：無理しちゃってさ。次頼んでも駄目だからね。";
                                    ok.ShowDialog();
                                    mainMessage.Text = "アイン：ッハッハッハッハッハ・・・";
                                    ok.ShowDialog();
                                    we.OneDeny = true;
                                }
                            }
                        }
                        else
                        {
                            mainMessage.Text = "アイン：おい、ラナ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：何よ？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：何でもねえよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：赤ポーション欲しいんでしょ？５Ｇよ♪";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：くっそぉ・・・。しかしここは買うしか。";
                            using (YesNoRequestMini yesno = new YesNoRequestMini())
                            {
                                yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                yesno.ShowDialog();
                                if (yesno.DialogResult == DialogResult.Yes)
                                {
                                    mc.Gold -= 5;
                                    GetPotionForLana();
                                    mainMessage.Text = "アイン：ック・・・ほらよ。";
                                    ok.ShowDialog();
                                    mainMessage.Text = "ラナ：おぉ、ナイストレード♪";
                                    ok.ShowDialog();
                                    mainMessage.Text = "アイン：今に見てろ。近い将来、正式な伝統ある由緒正しいハイパー薬屋が出来るはずだ。";
                                    ok.ShowDialog();
                                    mainMessage.Text = "ラナ：何の神頼みをしてるのやら♪　毎度有り！";
                                    ok.ShowDialog();
                                    mainMessage.Text = "アイン：神よ・・・まっとうな薬屋を・・・";
                                    we.AlreadyCommunicate = true;
                                    we.CommunicationSuccess2 = true;
                                }
                                else
                                {
                                    mainMessage.Text = "アイン：何でもねえよ！！";
                                    ok.ShowDialog();
                                    mainMessage.Text = "ラナ：何よもう。面白い企画だと思ったのに・・・";
                                }
                            }
                        }
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "３日目"
            else if (this.firstDay >= 3 && !we.CommunicationLana3 && mc.Level >= 1 && knownTileInfo[2])
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        // ２日目の会話を実施していた場合
                        if (we.CommunicationFirstContact2)
                        {
                            // ２日目：赤ポーションを受け取っていた場合。
                            if (we.CommunicationSuccess2)
                            {
                                mainMessage.Text = "アイン：よぉ、裏口専門闇悪徳薬業者・・・";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：ジョーダンが通じないのねホント。ホラ代金返却よ。";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：ッバ！バカな！！！";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：赤ポーションで少しは進めるんじゃないかと思ったからよ。ジョーダンでしょうが。ホラホラ";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：誰だ貴様は";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：え？";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：貴様、ラナの名を語る偽者だな！";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：本物だったら代金返却はありえないの？";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：そうだ。良く分かってるじゃないか。";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：へー、じゃ今偽者だしね。本物になろうかなー♪";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：ちょっとソコ、ターーーーイム！！！";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：謝ってよねーホント";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：スイマセンでした・・・";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：じゃあこれ、昨日の分よ";
                                ok.ShowDialog();
                                mc.Gold += 5;
                                mainMessage.Text = "アイン：ホントスイマセンでした・・・（代金を受け取りました）";
                                ok.ShowDialog();
                                UpdateMainMessage("ラナ：さてさて、とにかく赤ポーションは一日１個渡すからさ。大事に使ってよね。");
                                GetPotionForLana();

                                mainMessage.Text = "アイン：了解！";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：一日１個でどこまで行けるやら、ホントに";
                                we.AlreadyCommunicate = true;
                            }
                            else
                            {
                                mainMessage.Text = "アイン：よっ！ものは相談なんだが";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：相談って何よ？";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：相談はアレについてだ。";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：アレって何よ？";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：なかなかどうして、アレが必要なんだよ。";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：聖なるハイパー無限ソードとか？";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：そういうのも欲しいんだが、いやアレだ";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：透明マントみたいなやつ？";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：違う、透き通るマントではなく、ビンに液体が入っているアレだ。";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：リポビタンＺのこと？";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：・・・いや、ほんと悪かった・・・";
                                ok.ShowDialog();
                                UpdateMainMessage("ラナ：まあ、謝ったという事で許しといてあげるわ。じゃあハイ♪");
                                GetPotionForLana();

                                mainMessage.Text = "アイン：あと、５Ｇだったな。ちょっと待ってろ";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：ウソよ、ウソウソ。そんなの取る訳ないでしょ";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：何いいいぃぃぃ！！";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：もともとちょっとした企画だったのよ。真に受けるからこっちがビックリしたじゃないの。";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：いや、ホントすまねえな。これ１個で頑張ってくるぜ！";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：赤ポーションは一日１個渡すからさ。失敗したら承知しないからね。";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：イエッサー！";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：まったくああいう馬鹿っぽい所さえ無ければねえ・・・";
                                we.AlreadyCommunicate = true;
                            }
                        }
                        else
                        {
                            mainMessage.Text = "アイン：なかなか進めねえな。途中までいくとライフが減っていて引き返すハメになる。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：それは回復薬が無いからでしょ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：でもハンナ叔母ちゃんは宿屋、ガンツのおっちゃんは武具屋だしな。";
                            ok.ShowDialog();
                            UpdateMainMessage("ラナ：あっ、それじゃ・・・はいコレ♪");
                            GetPotionForLana();

                            mainMessage.Text = "アイン：おお！！！透き通るビンに赤色の液体・・・まさか！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：回復薬よ。大事に使ってちょうだいね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：いや、ホントすまねえな。これ１個で頑張ってくるぜ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：赤ポーションは一日１個渡すからさ。失敗したら承知しないからね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：イエッサー！";
                            we.AlreadyCommunicate = true;
                        }
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                }
                else
                {
                    if (!we.AlreadyCommunicate)
                    {
                        // ２日目の会話を実施していた場合
                        if (we.CommunicationFirstContact2)
                        {
                            // ２日目：赤ポーションを受け取っていた場合。
                            if (we.CommunicationSuccess2)
                            {
                                mainMessage.Text = "アイン：よぉ、裏口専門闇悪徳薬業者・・・";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：ジョーダンが通じないのねホント。ホラ代金返却よ。";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：ッバ！バカな！！！";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：赤ポーションで少しは進めるんじゃないかと思ったからよ。ジョーダンでしょうが。ホラホラ";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：誰だ貴様は";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：え？";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：貴様、ラナの名を語る偽者だな！";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：本物だったら代金返却はありえないの？";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：そうだ。良く分かってるじゃないか。";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：へー、じゃ今偽者だしね。本物になろうかなー♪";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：ちょっとソコ、ターーーーイム！！！";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：謝ってよねーホント";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：スイマセンでした・・・";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：じゃあこれ、前回徴収した分よ";
                                ok.ShowDialog();
                                mc.Gold += 5;
                                mainMessage.Text = "アイン：ホントスイマセンでした・・・（代金を受け取りました）";
                                ok.ShowDialog();
                                UpdateMainMessage("ラナ：さてさて、とにかく赤ポーションは一日１個渡すからさ。大事に使ってよね。");
                                GetPotionForLana();

                                mainMessage.Text = "アイン：了解！";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：一日１個でどこまで行けるやら、ホントに";
                                we.AlreadyCommunicate = true;
                            }
                            else
                            {
                                mainMessage.Text = "アイン：よっ！ものは相談なんだが";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：相談って何よ？";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：相談はアレについてだ。";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：アレって何よ？";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：なかなかどうして、アレが必要なんだよ。";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：聖なるハイパー無限ソードとか？";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：そういうのも欲しいんだが、いやアレだ";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：透明マントみたいなやつ？";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：違う、透き通るマントではなく、ビンに液体が入っているアレだ。";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：リポビタンＺのこと？";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：・・・いや、ほんと悪かった・・・";
                                ok.ShowDialog();
                                UpdateMainMessage("ラナ：まあ、謝ったという事で許しといてあげるわ。じゃあハイ♪");
                                GetPotionForLana();

                                mainMessage.Text = "アイン：あと、５Ｇだったな。ちょっと待ってろ";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：ウソよ、ウソウソ。そんなの取る訳ないでしょ";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：何いいいぃぃぃ！！";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：もともとちょっとした企画だったのよ。真に受けるからこっちがビックリしたじゃないの。";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：いや、ホントすまねえな。これ１個で頑張ってくるぜ！";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：赤ポーションは一日１個渡すからさ。失敗したら承知しないからね。";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：イエッサー！";
                                ok.ShowDialog();
                                mainMessage.Text = "ラナ：まったくああいう馬鹿っぽい所さえ無ければねえ・・・";
                                we.AlreadyCommunicate = true;
                            }
                        }
                        else
                        {
                            mainMessage.Text = "アイン：なかなか進めねえな。途中までいくとライフが減っていて引き返すハメになる。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：それは回復薬が無いからでしょ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：でもハンナ叔母ちゃんは宿屋、ガンツのおっちゃんは武具屋だしな。";
                            ok.ShowDialog();
                            UpdateMainMessage("ラナ：あっ、それじゃ・・・はいコレ♪");
                            GetPotionForLana();

                            mainMessage.Text = "アイン：おお！！！透き通るビンに赤色の液体・・・まさか！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：回復薬よ。大事に使ってちょうだいね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：いや、ホントすまねえな。これ１個で頑張ってくるぜ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：赤ポーションは一日１個渡すからさ。失敗したら承知しないからね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：イエッサー！";
                            we.AlreadyCommunicate = true;
                        }
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "４日目"
            else if (this.firstDay >= 4 && !we.CommunicationLana4 && mc.Level >= 1 && knownTileInfo[2])
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "アイン：そもっそもだな。武具屋の品物を買占めするか？普通。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：ガンツさんの所、結構品揃え良かったからね。正直ビックリね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：何故、買占めしたんだ？まさか全部持ち歩くわけないだろ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：そうね。ダンジョンを進める上で、武具劣化を想定し、レベルに応じた武具の使い分けってとこでしょ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：何だその頭使ってそうな作戦は。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：実際、用意周到な作戦じゃない。ポーションすら無かったアンタよりはるかにマシね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ッケ、意外と有り余る金をムダに使ったかも知れねぇだろ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：まあまあ、しばらく待ってれば、また入荷するって話じゃない。気長に待ちましょ♪";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：そうだなグダグダ考えてもしょうがねえ！";
                        ok.ShowDialog();
                        UpdateMainMessage("ラナ：あ、でも頭は使いなさいよね。ほら明日のポーション。");
                        GetPotionForLana();

                        mainMessage.Text = "アイン：サンキュー！";
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                }
                else
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "アイン：そもっそもだな。武具屋の品物を買占めするか？普通。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：ガンツさんの所、結構品揃え良かったからね。正直ビックリね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：何故、買占めしたんだ？まさか全部持ち歩くわけないだろ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：そうね。ダンジョンを進める上で、武具劣化を想定し、レベルに応じた武具の使い分けってとこでしょ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：何だその頭使ってそうな作戦は。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：実際、用意周到な作戦じゃない。ポーションすら無かったアンタよりはるかにマシね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ッケ、意外と有り余る金をムダに使ったかも知れねぇだろ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：まあまあ、しばらく待ってれば、また入荷するって話じゃない。気長に待ちましょ♪";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：そうだなグダグダ考えてもしょうがねえ！";
                        ok.ShowDialog();
                        UpdateMainMessage("ラナ：あ、でも頭は使いなさいよね。ほら今日のポーション。");
                        GetPotionForLana();

                        mainMessage.Text = "アイン：サンキュー！";
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "５日目"
            else if (this.firstDay >= 5 && !we.CommunicationLana5 && mc.Level >= 1 && knownTileInfo[2])
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "アイン：ラナ、良いこと思いついたぜ！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：無い頭で何を思いついたのよ？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ダンジョンに行かず、ダンジョン最下層へ到着する方法だ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：言葉分かって使ってんのかしらホント・・・";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：そもそも目的地はダンジョンの最奥なんだろう？ならば、全てを掘り返してしまえば良い。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：じゃ頑張って掘り返せば？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：任せとけって！ッハッハッハッハ！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：・・・（遠い目）";
                        ok.ShowDialog();
                        mainMessage.Text = "・・・・・";
                        ok.ShowDialog();
                        mainMessage.Text = "・・・・";
                        ok.ShowDialog();
                        mainMessage.Text = "・・・";
                        ok.ShowDialog();
                        mainMessage.Text = "・・";
                        ok.ShowDialog();
                        mainMessage.Text = "・";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：・・・・・・ッハッハッハッハッハ！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：今日もお疲れ様、どう順調だった？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ッハッハッハ！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：大体入り口のドアからして壊せないのに何考えてたのよ。ダンジョン専念よ。このバカ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：そうする・・・悪ぃ。";
                        ok.ShowDialog();
                        UpdateMainMessage("ラナ：はい、明日のポーションね。ったく、いい加減、頭を先に直してチョーダイよ。");
                        GetPotionForLana();

                        mainMessage.Text = "アイン：そうするぜ！任せとけ！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：「そうするぜ」って自爆してるし・・・先が思いやられるわ。";
                        WE.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                }
                else
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "アイン：ラナ、寝てる間に良いこと思いついたぜ！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：無い頭で何を思いついたのよ？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ダンジョンに行かず、ダンジョン最下層へ到着する方法だ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：言葉分かって使ってんのかしらホント・・・";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：そもそも目的地はダンジョンの最奥なんだろう？ならば、全てを掘り返してしまえば良い。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：じゃ頑張って掘り返せば？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：マトックまで昨日用意したんだぜ。任せとけって！ッハッハッハッハ！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：・・・（遠い目）";
                        ok.ShowDialog();
                        mainMessage.Text = "・・・・・";
                        ok.ShowDialog();
                        mainMessage.Text = "・・・・";
                        ok.ShowDialog();
                        mainMessage.Text = "・・・";
                        ok.ShowDialog();
                        mainMessage.Text = "・・";
                        ok.ShowDialog();
                        mainMessage.Text = "ッバキ・・・（マトックが壊れました）";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：・・・・・・ッハッハッハッハッハ！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：今日もお疲れ様、どう順調だった？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：アーーーッハッハッハ！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：早く諦めてダンジョンに専念しなさいよ。このバカ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：そうする・・・悪ぃ。";
                        ok.ShowDialog();
                        UpdateMainMessage("ラナ：はい、今日のポーションね。ったく、いい加減、頭を先に直してチョーダイよ。");
                        GetPotionForLana();

                        mainMessage.Text = "アイン：そうするぜ！任せとけ！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：「そうするぜ」って完全に自爆してるし・・・今日も良い所までいけるのかしら。";
                        WE.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "６日目"
            else if (this.firstDay >= 6 && !we.CommunicationLana6 && mc.Level >= 1 && knownTileInfo[2])
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "ラナ：そういえば、もうすぐ一週間経過ね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：あのダンジョン、行けども行けどもラチがあかねえな。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：どうしてよ？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：さあな。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：『さあな』ってひょっとしてバカ？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：おまえその『バカ』ってよく使うな。俺は決してバカじゃない。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：へー・・・そぉなんだ。例えば？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ストレート・スマッシュ！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：へー・・・他には？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：戦況を見て、逃げる！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：最後はポーションでたまに回復？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ピンチの時こそ、ポーショ・・・何故分かる！？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：ワンパターンね";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：っな、じゃどうしろと言うんだ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：確かに、今はまだそうするしかないわね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：今はまだ？";
                        ok.ShowDialog();
                        UpdateMainMessage("ラナ：っそ。ハイ、ポーション♪");
                        GetPotionForLana();

                        mainMessage.Text = "アイン：なんか突っかかる言い方だな。まあポーションはありがたくもらうぜ！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：気にしない、気にしない。せいぜい頑張ってよね。";
                        WE.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                }
                else
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "ラナ：ついに一週間が経過したわね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：あのダンジョン、行けども行けどもラチがあかねえな。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：どうしてよ？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：さあな。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：『さあな』ってひょっとしてバカ？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：おまえその『バカ』ってよく使うな。俺は決してバカじゃない。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：へー・・・そぉなんだ。例えば？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ストレート・スマッシュ！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：へー・・・他には？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：戦況を見て、逃げる！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：最後はポーションでたまに回復？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ピンチの時こそ、ポーショ・・・何故分かる！？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：ワンパターンね";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：っな、じゃどうしろと言うんだ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：確かに、今はまだそうするしかないわね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：今はまだ？";
                        ok.ShowDialog();
                        UpdateMainMessage("ラナ：っそ。ハイ、今日のポーション♪");
                        GetPotionForLana();

                        mainMessage.Text = "アイン：なんか突っかかる言い方だな。まあポーションはありがたくもらうぜ！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：気にしない、気にしない。じゃ今日も頑張ってきてよね。";
                        WE.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "７日目"
            else if (this.firstDay >= 7 && !we.CommunicationLana7 && mc.Level >= 1 && knownTileInfo[2])
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "ラナ：あー！ちょっとちょっと！";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：なんだ？騒々しいな。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：アイン、ひょっとしてとんでもない勘違いしてない？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：だから一体何がどうしたってんだ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：そうね・・・無理もないわ。何せ、今まで教えてなかったワケだし。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：シツコイ。いい加減怒るぞ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：技と言えば？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：クリティカルヒット！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：他は？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：スーパークリティカルヒット！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：うゎ・・・実はね、技術にはまだ隠されたパラメタがあるのよ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ハイパークリティカルヒット！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：っちょ、良いから聞きなさいよね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：すまん。で、隠されたパラメタってのは何だ？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：それは秘密よ♪";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：超エレクトロ・ジェネティック・クリティカルヒット！！！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：分かった分かった、ハイハイ。ええとね、実は先制攻撃に関係するのよ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：おお、そういやたまに相手が先に仕掛けてくるな。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：次やられるライフになってから回復しようとしても";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ポーション手遅れの場合があるな。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：なので、やられる一歩手前で";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ポーション飲む必要があるな。";
                        ok.ShowDialog();
                        UpdateMainMessage("ラナ：そうならないためにも技術を上げれば効果テキメンよ。ハイ、ポーション。");
                        GetPotionForLana();

                        if (mc.Agility > 10)
                        {
                            mainMessage.Text = "アイン：確かに最近何となくだが先制攻撃が出やすいと思ってた所だ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：へー、ちゃんと鍛えてるんだ技の方も。" + mc.Agility.ToString() + "もあれば上等ね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：おお、任せとけって！！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：なんだ結構やるじゃないの。ホントに任せちゃおうかしら。";
                        }
                        else
                        {
                            mainMessage.Text = "アイン：・・・面倒くせぇ！！ハイパークリティカルヒット！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：ハァ～～～～～～ァ～～～～～ァ～～～～～～～・・・";
                        }
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                }
                else
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "ラナ：オハヨー。あ、ちょっとちょっと！";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：なんだ？朝から騒々しいな。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：アイン、ひょっとしてとんでもない勘違いしてない？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：だから一体何がどうしたってんだ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：そうね・・・無理もないわ。何せ、今まで教えてなかったワケだし。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：シツコイ。いい加減怒るぞ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：技と言えば？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：クリティカルヒット！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：他は？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：スーパークリティカルヒット！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：うゎ・・・実はね、技術にはまだ隠されたパラメタがあるのよ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ハイパークリティカルヒット！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：っちょ、良いから聞きなさいよね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：すまん。で、隠されたパラメタってのは何だ？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：それは秘密よ♪";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：超エレクトロ・ジェネティック・クリティカルヒット！！！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：分かった分かった、ハイハイ。ええとね、実は先制攻撃に関係するのよ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：おお、そういやたまに相手が先に仕掛けてくるな。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：次やられるライフになってから回復しようとしても";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ポーション手遅れの場合があるな。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：なので、やられる一歩手前で";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ポーション飲む必要があるな。";
                        ok.ShowDialog();
                        UpdateMainMessage("ラナ：そうならないためにも技術を上げれば効果テキメンよ。ハイ、ポーション。");
                        GetPotionForLana();

                        if (mc.Agility > 10)
                        {
                            mainMessage.Text = "アイン：確かに最近何となくだが先制攻撃が出やすいと思ってた所だ。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：へー、ちゃんと鍛えてるんだ技の方も。" + mc.Agility.ToString() + "もあれば上等ね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：おお、任せとけって！！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：なんだ結構やるじゃないの。ホントに任せちゃおうかしら。";
                        }
                        else
                        {
                            mainMessage.Text = "アイン：・・・面倒くせぇ！！ハイパークリティカルヒット！";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：ハァ～～～～～～ァ～～～～～ァ～～～～～～～・・・";
                        } 
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "８日目"
            else if (this.firstDay >= 8 && !we.CommunicationLana8 && mc.Level >= 1 && knownTileInfo[2])
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "アイン：体力を上げておくと、最大ライフが増えるんだよな？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：当たり前じゃない。そんな事。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ラナ、実は知ってるんだろ？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：何の事よ？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：体力に関する隠しパラメタさ！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：無いわよ、そんなの。体力は純粋に最大ライフの増加だけね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：『秘密よ♪』とか言えよ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：無いって言ってるじゃないの。気持ち悪い言い方しないでよ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：無いわけないだろ！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：だいたい体力って言ったら、それ以外何があるって言うのよ？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：そうだな。体力と言えば、力！！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：それは腕力。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：じゃあ他にそうだな。持久力！！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：純粋に最大ライフの事ね。他は？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ッハッハッハッハ！";
                        ok.ShowDialog();
                        UpdateMainMessage("ラナ：ホラ、赤ポーションよ。言っとくけど、体力バカにはならないでよね。");
                        GetPotionForLana();

                        mainMessage.Text = "アイン：心配するな、体力が全てじゃない事ぐらい分かってるさ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：ホントかしら・・・パラメタＵＰには最善を尽くしてね。";
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                }
                else
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "アイン：昨日ふと疑問に思ったが、体力を上げておくと、最大ライフが増えるんだよな？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：当たり前じゃない。そんな事。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ラナ、実は知ってるんだろ？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：何の事よ？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：体力に関する隠しパラメタさ！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：無いわよ、そんなの。体力は純粋に最大ライフの増加だけね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：『秘密よ♪』とか言えよ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：無いって言ってるじゃないの。気持ち悪い言い方しないでよ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：無いわけないだろ！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：だいたい体力って言ったら、それ以外何があるって言うのよ？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：そうだな。体力と言えば、力！！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：それは腕力。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：じゃあ他にそうだな。持久力！！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：純粋に最大ライフの事ね。他は？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ッハッハッハッハ！";
                        ok.ShowDialog();
                        UpdateMainMessage("ラナ：ホラ、赤ポーションよ。言っとくけど、体力バカにはならないでよね。");
                        GetPotionForLana();

                        mainMessage.Text = "アイン：心配するな、体力が全てじゃない事ぐらい分かってるさ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：ホントかしら・・・パラメタＵＰには最善を尽くしてね。";
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "９日目"
            else if (this.firstDay >= 9 && !we.CommunicationLana9 && mc.Level >= 1 && knownTileInfo[2])
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "アイン：力こそが全てさ。ラナもそう思うだろ？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：ぜーんぜん♪";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：通常攻撃がＵＰ。これさえあれば、どんな敵も一撃さ！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：ぜーんぜん♪♪";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：おまけにそこからクリティカルヒットが出れば更に確実！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：ぜーんぜん♪♪♪";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：何かお前のそのノリノリな姿がヤケに腹立つな。何なんだ？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：あらら、怒らせちゃったか。力って物理攻撃でしょ？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：そうだ。この世で一番絶対的なものだ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：物理攻撃が効かない相手も居るわよ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：それでも俺は当てる。当てて見せる。";
                        ok.ShowDialog();
                        if (WE.CompleteArea1)
                        {
                            mainMessage.Text = "ラナ：２階に行ってるなら言っておくけど・・・出るわよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：何が出るってんだ？ユーレイとかお化けか？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：そうよ、どれだけ剣をブンブンやっても無意味ね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：それでも俺は当てる。当てて見せる。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：おんなじ台詞言ってないで、少しは力以外も上げておきなさい。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：それでも俺は当てる。当てて見せる。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：ック・・・同じ台詞をいつまでも・・・ヤケにイライラさせるわね。殴ろうかしら♪";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：分かった！！力以外も上げる。マジ勘弁な。";
                            ok.ShowDialog();
                            UpdateMainMessage("ラナ：まあ、ともかく。物理攻撃が効かない時は魔法やスキルで対処することね。はい、ポーション。");
                            GetPotionForLana();

                            mainMessage.Text = "アイン：ところでお前さ、何でそんな事しってるわけ？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：っぇ？さ、さささ、さーてそれでは次回お楽しみに・・・アハハ♪";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ったく、何作り笑いしてんだか。まあ情報くれてサンキュー。";
                        }
                        else
                        {
                            mainMessage.Text = "ラナ：まあ今は大丈夫だけど、力以外でダメージが上がる方法もある事ぐらい覚えてね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：それでも俺は当てる。当てて見せる。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：力以外の、たとえば知力が上がれば威力が上がる魔法も多数存在するのよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：それでも俺は当てる。当てて見せる。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：スキルってあるでしょ？あれも基本は力じゃなくて技の方が高い効果を引き出すのよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：それでも俺は当てる。当てて見せる。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：ック・・・同じ顔で同じ台詞をいつまでも・・・ヤケにイライラさせるわね。殴ろうかしら♪";
                            ok.ShowDialog();
                            UpdateMainMessage("アイン：分かった！！力以外も上げる。マジ勘弁な。");
                            GetPotionForLana();

                            mainMessage.Text = "アイン：ところでお前さ、何でそんな事を知ってるわけ？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：っぇ？さ、さささ、さーてそれでは次回お楽しみに・・・アハハ♪";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ったく、何作り笑いしてんだか。まあ情報くれてサンキュー。";
                        }
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                }
                else
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "アイン：力こそが全てさ。ラナもそう思うだろ？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：ぜーんぜん♪";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：通常攻撃がＵＰ。これさえあれば、どんな敵も一撃さ！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：ぜーんぜん♪♪";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：おまけにそこからクリティカルヒットが出れば更に確実！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：ぜーんぜん♪♪♪";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：何かお前のそのノリノリな姿がヤケに腹立つな。何なんだ？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：あらら、怒らせちゃったか。力って物理攻撃でしょ？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：そうだ。この世で一番絶対的なものだ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：物理攻撃が効かない相手も居るわよ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：それでも俺は当てる。当てて見せる。";
                        ok.ShowDialog();
                        if (WE.CompleteArea1)
                        {
                            mainMessage.Text = "ラナ：２階に行ってるなら言っておくけど・・・出るわよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：何が出るってんだ？ユーレイとかお化けか？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：そうよ、どれだけ剣をブンブンやっても無意味ね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：それでも俺は当てる。当てて見せる。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：おんなじ台詞言ってないで、少しは力以外も上げておきなさい。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：それでも俺は当てる。当てて見せる。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：ック・・・同じ台詞をいつまでも・・・ヤケにイライラさせるわね。殴ろうかしら♪";
                            ok.ShowDialog();
                            UpdateMainMessage("アイン：分かった！！力以外も上げる。マジ勘弁な。");
                            GetPotionForLana();

                            mainMessage.Text = "アイン：ところでお前さ、何でそんな事を知ってるわけ？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：っぇ？さ、さささ、さーてそれでは次回お楽しみに・・・アハハ♪";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ったく、何作り笑いしてんだか。まあ情報くれてサンキュー。";
                        }
                        else
                        {
                            mainMessage.Text = "ラナ：まあ今は大丈夫だけど、力以外でダメージが上がる方法もある事ぐらい覚えてね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：それでも俺は当てる。当てて見せる。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：力以外の、たとえば知力が上がれば威力が上がる魔法も多数存在するのよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：それでも俺は当てる。当てて見せる。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：スキルってあるでしょ？あれも基本は力じゃなくて技の方が高い効果を引き出すのよ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：それでも俺は当てる。当てて見せる。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：ック・・・同じ顔で同じ台詞をいつまでも・・・ヤケにイライラさせるわね。殴ろうかしら♪";
                            ok.ShowDialog();
                            UpdateMainMessage("アイン：分かった！！力以外も上げる。マジ勘弁な。");
                            GetPotionForLana();

                            mainMessage.Text = "アイン：ところでお前さ、何でそんな事しってるわけ？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：っぇ？さ、さささ、さーてそれでは次回お楽しみに・・・アハハ♪";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ったく、何作り笑いしてんだか。まあ情報くれてサンキュー。";
                        }
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "１０日目"
            else if (this.firstDay >= 10 && !we.CommunicationLana10 && mc.Level >= 1 && knownTileInfo[2])
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "ラナ：おい、そこのバカ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：よう、何だよそこの・・・ラナ";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：アンタ、ホントにバカよね・・・何か思いつきなさいよ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：だから何だってんだよ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：アイン、今日はラナ先生が知力というものを親切丁寧に解説してあげるわ、覚悟なさい。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：あ～、ハンナおばさんとこ行って休んで良いか？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：まず、知力ＵＰ＝魔法力ＵＰ。これは基本中の基本ね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：あ～はいはい。ガンツおじさんとこで武器見てきて良いか？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：それから付随効果がかかる魔法もあるんだけど、この付随ポイントに加算値が付くの。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：う～ん、最近さ。どうも寝付けが悪くてな。睡眠薬とか無いか？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：後は他に防御系の魔法に対してもどれだけ防御するかのポイントが加算されるわね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：まあこの解説自体が睡眠薬って所か。フワァァ～～アァ。じゃ少し寝るわ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：極めつけは魔法剣ね。通常攻撃自体に魔法付与させ、攻撃力を増大させるの。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：なに！？俺の今持ってる武器も上がるのか！？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：そうよ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：どのぐらい上がるんだ！？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：知力の分だけ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：・・・グハァァ！！！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：アンタの負けよアイン。そんな３流のシカトスタンスなんて取ろうってのが間違いなのよ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ど、どうすればいい？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：知力を上げなさい♪";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：他に手はないのか！？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：知力について解説をしてるんじゃない。他にあるわけないでしょ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ソコを何とか授けたまえ。ラナ『様』！！";
                        ok.ShowDialog();
                        UpdateMainMessage("ラナ：はい、ポーション♪");
                        GetPotionForLana();

                        mainMessage.Text = "アイン：くそっ、ポーションで俺はごまかされねえ。力だ。力さえ上げれば。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：魔法剣は知力×３の威力が出るのよ♪";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ラナ。俺を惑わすんじゃない。そんなものに頼る俺ではない。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：意外と頑固ね。まあ、知力を捨てることだけは避けてよね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：オーケー。魔法剣もやってみたいしな。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：うん、頑張ってね。知力も上がればアイン、かなり良い線行くわよ。";
                        this.we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                }
                else
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "ラナ：おい、そこのバカ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：よう、何だよそこの・・・ラナ";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：アンタ、ホントにバカよね・・・何か思いつきなさいよ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：だから何だってんだよ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：アイン、今日はラナ先生が知力というものを親切丁寧に解説してあげるわ、覚悟なさい。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：あ～、もうダンジョン向かうとこなんだ。行って良いか？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：まず、知力ＵＰ＝魔法力ＵＰ。これは基本中の基本ね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：おっとそうだ。ガンツじいさん、何か良いもの仕入れてるかな。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：それから付随効果がかかる魔法もあるんだけど、この付随ポイントに加算値が付くの。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：う～ん。どうも昨日は寝付けが悪かったみたいだ。目覚まし薬とかは無いか？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：後は他に防御系の魔法に対してもどれだけ防御するかのポイントが加算されるわね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：まあこの解説だけじゃ目覚ましにもならねえな。フワァァ～～アァ。さてと、じゃ行ってくるか。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：極めつけは魔法剣ね。通常攻撃自体に魔法付与させ、攻撃力を増大させるの。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：なに！？俺の今持ってる武器も上がるのか！？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：そうよ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：どのぐらい上がるんだ！？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：知力の分だけ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：・・・グハァァ！！！";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：アンタの負けよアイン。そんな３流のシカトスタンスなんて取ろうってのが間違いなのよ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ど、どうすればいい？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：知力を上げなさい♪";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：他に手はないのか！？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：知力について解説をしてるんじゃない。他にあるわけないでしょ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ソコを何とか授けたまえ。ラナ『様』！！";
                        ok.ShowDialog();
                        UpdateMainMessage("ラナ：はい、ポーション♪");
                        GetPotionForLana();

                        mainMessage.Text = "アイン：くそっ、ポーションで俺はごまかされねえ。力だ。力さえ上げれば。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：魔法剣は知力×３の威力が出るのよ♪";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ラナ。俺を惑わすんじゃない。そんなものに頼る俺ではない。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：意外と頑固ね。まあ、知力を捨てることだけは避けてよね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：オーケー。魔法剣もやってみたいしな。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：うん、頑張ってね。知力も上がればアイン、かなり良い線行くわよ。";
                        this.we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "１１日目"
            else if (this.firstDay >= 11 && !we.CommunicationLana11 && mc.Level >= 1 && knownTileInfo[2])
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "アイン：ふう～、どうも調子が出ねえな。クリティカルもイマイチ出ねえ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：力・技・知・体と一通り揃ったとしても、一番重要な要素。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：最後に残った心ってか。結局どう影響してくるんだ？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：心そのものね。特別パラメタＵＰには関与しないの。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：心ひとつで世界が変わるってか。まあ上げてどうなるわけでもあるまい。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：そうね。コレといって上げる必要はないわね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：だったら実際問題、上げる必要は無えよな？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：そう思うんだったら、上げなくても良いんじゃない？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ふう～、どうも納得がいかねえな。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：ホントしょーがないわね、じゃあヒントよ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：おう、頼むぜ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：力１、技１、知１、体１、心１００。どう？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ヤング・ゴブリンすら倒せそうにねえな・・・";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：ようするに心だけ上げても意味がないってわけよ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：じゃあやっぱ不要ってことか？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：力４０、技２５、知１０、体２５、心１。どう？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：魔法が若干非力だが、大体やれそうだな。だが・・・";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：そうよ、コレといって上げる必要はないけど。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：何となく調子が出なそうだな。強い敵が来たらマズそうだな。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：ご明察。心は調子の良し悪しって所ね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：どう言う事だ？アバウトすぎてよく分かんねえ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：ちょっと話が長くなりそうね。続きは明日のダンジョンの後って事で♪";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ハァ！？おいっちょ待てって。今言えよ。";
                        ok.ShowDialog();
                        UpdateMainMessage("ラナ：今日はおとなしく調子悪いままで居てよ。ポーションで我慢我慢♪");
                        GetPotionForLana();

                        mainMessage.Text = "アイン：ポーションはライフ回復しかしねえぞ、ああぁぁぁあ何だこのわだかまりはぁぁあ！";
                        ok.ShowDialog();
                        mainMessage.Text = MessageFormatForLana(1001) + "フフフ♪";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：っくそ・・・お決まりの台詞を先に言いやがって・・・覚えてろよ、ラナ。";
                        this.we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1001) + "フフフ♪";
                    }
                }
                else
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "アイン：ふう～、昨日そうだったんだが、どうもクリティカルがイマイチ出ねえ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：力・技・知・体と一通り揃ったとしても、一番重要な要素。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：最後に残った心ってか。結局どう影響してくるんだ？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：心そのものね。特別パラメタＵＰには関与しないの。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：心ひとつで世界が変わるってか。まあ上げてどうなるわけでもあるまい。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：そうね。コレといって上げる必要はないわね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：だったら実際問題、上げる必要は無えよな？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：そう思うんだったら、上げなくても良いんじゃない？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ふう～、どうも納得がいかねえな。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：ホントしょーがないわね、じゃあヒントよ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：おう、頼むぜ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：力１、技１、知１、体１、心１００。どう？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ヤング・ゴブリンすら倒せそうにねえな・・・";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：ようするに心だけ上げても意味がないってわけよ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：じゃあやっぱ不要ってことか？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：力４０、技２５、知１０、体２５、心１。どう？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：魔法が若干非力だが、大体やれそうだな。だが・・・";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：そうよ、コレといって上げる必要はないけど。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：何となく調子が出なそうだな。強い敵が来たらマズそうだな。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：ご明察。心は調子の良し悪しって所ね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：どう言う事だ？アバウトすぎてよく分かんねえ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：ちょっと話が長くなりそうね。続きはダンジョンから帰ってきてからって事で♪";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ハァ！？おいっちょ待てって。今言えよ。";
                        ok.ShowDialog();
                        UpdateMainMessage("ラナ：調子悪いままダンジョンゴーゴー♪　まずはポーションよね。");
                        GetPotionForLana();

                        mainMessage.Text = "アイン：ポーションはライフ回復しかしねえぞ、ああぁぁぁあ何だこのわだかまりはぁぁあ！";
                        ok.ShowDialog();
                        mainMessage.Text = MessageFormatForLana(1002) + "フフフ♪";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：っくそ・・・お決まりの台詞を先に言いやがって・・・覚えてろよ、ラナ。";
                        this.we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002) + "フフフ♪";
                    }
                }
            }
            #endregion
            #region "１２日目"
            else if (this.firstDay >= 12 && !we.CommunicationLana12 && mc.Level >= 1 && knownTileInfo[2])
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "アイン：よう、悪徳精神専門業者。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：どういう専門業者よ。へんな言いがかり付けないでよ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：スマン、で、調子の良し悪しとはどういう意味だ？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：アイン、今攻撃力はいくつなの？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ん？え～とな。" + Convert.ToString(this.mc.Strength + mc.MainWeapon.MinValue) + " - " + Convert.ToString(this.mc.Strength + mc.MainWeapon.MaxValue) + "だな。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：その差が良い方向に縮まると思えば良いわ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：最大攻撃力の値になりやすいって事か？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：半分正解ね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：もう半分はどういう意味だ？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：アインの技値はいくつ？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：" + this.mc.Agility.ToString() + "だな。それがどう影響してくる？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：たとえばモンスターの技値が20だったとするわね。";
                        ok.ShowDialog();
                        int difference = this.mc.Agility - 20;
                        mainMessage.Text = "ラナ：そうすると差し引きが・・・" + difference.ToString();
                        ok.ShowDialog();
                        if (difference > 0)
                        {
                            mainMessage.Text = "ラナ：やるじゃない、アイン先制攻撃率は50%より上ね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：そういや何かそんな話だったな。それで心がどう影響するんだ？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：もともと50%より上だけど、その値そのものが増強されるの。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：よくわかんねえな。どっちにしろ“50%より上”なんだろ？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：50%より下でも先制攻撃はたまに出来るでしょ？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：そうだな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：50%より上でも先制攻撃される時があるでしょ？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：そうだな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：そのベース値自体がより確かな結果に繋がるって事よ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：だから調子の良し悪しってか・・・何かよくわかんねえけどな。";
                            ok.ShowDialog();
                        }
                        else if (difference == 0)
                        {
                            mainMessage.Text = "ラナ：ちょうどイーブン。アイン先制攻撃率は50%そのものね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：そういや何かそんな話だったな。それで心がどう影響するんだ？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：50%ジャストの場合、3回連続で先制攻撃されることが無くなるわね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：確かに平等なのに、連続して先制されるとイライラするな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：でも50%より下でも先制攻撃はたまに出来るでしょ？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：そうだな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：でも50%より上だったとしても先制攻撃される時があるでしょ？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：そうだな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：50%ジャスト以外でも、そのベース値自体がより確かな結果に繋がるって事よ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：だから調子の良し悪しってか・・・何かよくわかんねえけどな。";
                            ok.ShowDialog();
                        }
                        else
                        {
                            mainMessage.Text = "ラナ：アイン先制攻撃率は50%より下って事になるわね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：そういや何かそんな話だったな。それで心がどう影響するんだ？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：もともと50%より下の場合、その値そのものが吸収されるの。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：よくわかんねえな。どっちにしろ“50%より下”なんだろ？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：50%より上でも先制攻撃される時があるでしょ？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：そうだな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：50%より下でも先制攻撃はたまに出来るでしょ？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：そうだな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：そのベース値自体がより確かな結果に繋がるって事よ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：だから調子の良し悪しってか・・・何かよくわかんねえけどな。";
                            ok.ShowDialog();
                        }
                        mainMessage.Text = "ラナ：要するにそういうような所に影響が出てくるって事よ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：魔法やクリティカル、アイテム使用、敵からのダメージ、などなど全てか？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：そう、全てに通じてるわよ。結局、心が低い間は理不尽な戦闘ケースに苛まれる一方ね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：調子の良し悪しってか。何となくだが、分かったぜ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：そう言ってもらえると助かるわ。相手と拮抗する際に非常に重要だから。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ああ、心も鍛えていくとするか。";
                        ok.ShowDialog();
                        UpdateMainMessage("ラナ：さて、ポーション渡すから明日からまた頑張ってきてよね。");
                        GetPotionForLana();

                        mainMessage.Text = "アイン：おう、任せとけ！";
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                }
                else
                {
                    if (!we.AlreadyCommunicate)
                    {
                        mainMessage.Text = "アイン：よう、悪徳精神専門業者。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：どういう専門業者よ。へんな言いがかり付けないでよ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：スマン、で、調子の良し悪しとはどういう意味だ？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：アイン、今攻撃力はいくつなの？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ん？え～とな。" + Convert.ToString(this.mc.Strength + mc.MainWeapon.MinValue) + " - " + Convert.ToString(this.mc.Strength + mc.MainWeapon.MaxValue) + "だな。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：その差が良い方向に縮まると思えば良いわ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：最大攻撃力の値になりやすいって事か？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：半分正解ね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：もう半分はどういう意味だ？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：アインの技値はいくつ？";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：" + this.mc.Agility.ToString() + "だな。それがどう影響してくる？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：たとえばモンスターの技値が20だったとするわね。";
                        ok.ShowDialog();
                        int difference = this.mc.Agility - 20;
                        mainMessage.Text = "ラナ：そうすると差し引きが・・・" + difference.ToString();
                        ok.ShowDialog();
                        if (difference > 0)
                        {
                            mainMessage.Text = "ラナ：やるじゃない、アイン先制攻撃率は50%より上ね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：そういや何かそんな話だったな。それで心がどう影響するんだ？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：もともと50%より上だけど、その値そのものが増強されるの。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：よくわかんねえな。どっちにしろ“50%より上”なんだろ？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：50%より下でも先制攻撃はたまに出来るでしょ？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：そうだな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：50%より上でも先制攻撃される時があるでしょ？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：そうだな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：そのベース値自体がより確かな結果に繋がるって事よ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：だから調子の良し悪しってか・・・何かよくわかんねえけどな。";
                            ok.ShowDialog();
                        }
                        else if (difference == 0)
                        {
                            mainMessage.Text = "ラナ：ちょうどイーブン。アイン先制攻撃率は50%そのものね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：そういや何かそんな話だったな。それで心がどう影響するんだ？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：50%ジャストの場合、3回連続で先制攻撃されることが無くなるわね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：確かに平等なのに、連続して先制されるとイライラするな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：でも50%より下でも先制攻撃はたまに出来るでしょ？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：そうだな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：でも50%より上だったとしても先制攻撃される時があるでしょ？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：そうだな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：50%ジャスト以外でも、そのベース値自体がより確かな結果に繋がるって事よ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：だから調子の良し悪しってか・・・何かよくわかんねえけどな。";
                            ok.ShowDialog();
                        }
                        else
                        {
                            mainMessage.Text = "ラナ：アイン先制攻撃率は50%より下って事になるわね。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：そういや何かそんな話だったな。それで心がどう影響するんだ？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：もともと50%より下の場合、その値そのものが吸収されるの。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：よくわかんねえな。どっちにしろ“50%より下”なんだろ？";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：50%より上でも先制攻撃される時があるでしょ？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：そうだな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：50%より下でも先制攻撃はたまに出来るでしょ？";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：そうだな。";
                            ok.ShowDialog();
                            mainMessage.Text = "ラナ：そのベース値自体がより確かな結果に繋がるって事よ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：だから調子の良し悪しってか・・・何かよくわかんねえけどな。";
                            ok.ShowDialog();
                        }
                        mainMessage.Text = "ラナ：要するにそういうような所に影響が出てくるって事よ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：魔法やクリティカル、アイテム使用、敵からのダメージ、などなど全てか？";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：そう、全てに通じてるわよ。結局、心が低い間は理不尽な戦闘ケースに苛まれる一方ね。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：調子の良し悪しってか。何となくだが、分かったぜ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ラナ：そう言ってもらえると助かるわ。相手と拮抗する際に非常に重要だから。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ああ、心も鍛えていくとするか。";
                        ok.ShowDialog();
                        UpdateMainMessage("ラナ：さて、ポーション渡すからダンジョン頑張ってきてよね。");
                        GetPotionForLana();

                        mainMessage.Text = "アイン：おう、任せとけ！";
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "１３日目"
            else if (this.firstDay >= 13 && !we.CommunicationLana13 && mc.Level >= 5 && knownTileInfo[2])
            {
                if (!we.AlreadyCommunicate)
                {
                    UpdateMainMessage("アイン：力、技、知、体、心、これで５つ全て揃ったな。");

                    UpdateMainMessage("ラナ：アイン、結局どれを重点で上げていくのよ？");

                    UpdateMainMessage("アイン：結構悩まされるよな。少しおさらいしてみるか。");

                    UpdateMainMessage("アイン：力。　物理攻撃力ＵＰ。");

                    UpdateMainMessage("アイン：技。　クリティカルヒット率、先制攻撃率。");

                    UpdateMainMessage("ラナ：技パラメタで威力が上がるスキルもあるわ。忘れないように♪");

                    UpdateMainMessage("アイン：知。　魔法攻撃力ＵＰ。");

                    UpdateMainMessage("アイン：心。　微妙。");

                    UpdateMainMessage("ラナ：微妙って、おさらいになってないじゃない。");

                    UpdateMainMessage("アイン：心。　各種パラメタによって引き出される値のブレを無くす。");

                    UpdateMainMessage("ラナ：ありていに言ってしまえばそうね。");

                    UpdateMainMessage("アイン：結局どれが要るかって事だが・・・");

                    using (SelectTarget st = new SelectTarget())
                    {
                        st.FirstName = "力";
                        st.SecondName = "技";
                        st.ThirdName = "知";
                        st.FourthName = "体";
                        st.FifthName = "心";
                        st.MaxSelectable = 5;
                        st.StartPosition = FormStartPosition.CenterParent;
                        st.ShowDialog();
                        switch (st.TargetNum)
                        {
                            case 1:
                                UpdateMainMessage("アイン：やっぱりどう考えても力だろ。俺の考えは変わらないぜ。");

                                UpdateMainMessage("ラナ：そうね、例外はあるけど物理攻撃で押していくのは基本中の基本よ。");

                                UpdateMainMessage("アイン：なんだ、ヤケに素直だな？");

                                UpdateMainMessage("ラナ：ありのままの事実を言ってるだけよ。力無しでこの業界はやっていけないわ。");

                                UpdateMainMessage("アイン：業界ってか。まあ、力は任せておけってところだ！ッハッハッハ！");
                                break;

                            case 2:
                                UpdateMainMessage("アイン：意外と技が最強なんじゃねえのか？");

                                UpdateMainMessage("ラナ：何で疑問系にしてんのよ。技だけじゃ力にはならないわよ？");

                                UpdateMainMessage("アイン：まあそうだけどな。ストレート・スマッシュ使う場合などはある程度スピードが要る。");

                                UpdateMainMessage("アイン：他にもクリティカル３倍を狙う時は、力だけじゃダメだろ。");

                                UpdateMainMessage("ラナ：結構勉強してんのね。技が上がればそれだけ戦闘を優位に進められる事は確かよ。");

                                UpdateMainMessage("アイン：先手必勝、アドバンテージ、先付けプレッシャーなど揃えてみたいよな。");

                                UpdateMainMessage("ラナ：ッフフ、良いんじゃない？そういうのも一つのやりかただと思うわ。");
                                break;

                            case 3:
                                UpdateMainMessage("アイン：知力バンバン上げてさ、ヒールしまくる、とかどうだ？");

                                UpdateMainMessage("ラナ：そうね、そうしておけばそう簡単にやられたりはしなくなるわね。");

                                UpdateMainMessage("アイン：ファイア・ボールも威力が上がるしな。っお、これは一石二鳥じゃねえか！？");

                                UpdateMainMessage("ラナ：マナさえ潤沢にあれば、ダメージレースはコントロールしやすい、一理あるわ。");

                                UpdateMainMessage("アイン：ラナ、お前魔法術以外に拳武術も少し出来たよな。どうだ俺とチェンジしないか？");

                                UpdateMainMessage("ラナ：力こそが全てじゃなかったの？気持ち悪い提案ね・・・本気かしら。");

                                UpdateMainMessage("アイン：ダメージが当たるなら、それも力だ。どうだ？");

                                UpdateMainMessage("ラナ：まあ、考えといてあげる♪");

                                UpdateMainMessage("アイン：ああ、俺がヒール役に徹して、ラナがブンブン拳やるのも悪くねえかもな。");
                                break;

                            case 4:
                                UpdateMainMessage("アイン：・・・いや、体力はある意味最強だが、これは無いな。");

                                UpdateMainMessage("ラナ：ある意味ってどういう事よ？");

                                UpdateMainMessage("アイン：戦闘っていうのは、相手を片付けないと駄目なんだ。");

                                UpdateMainMessage("ラナ：片付けないと駄目なの？ファラ様の場合、非戦闘状態に持ち込むってよく聞く話よ。");

                                UpdateMainMessage("アイン：あれは・・・なんつうか別次元だ。ファラ王妃特有のもので、俺に出来る代物じゃないな。");

                                UpdateMainMessage("アイン：原則、ダンジョンのモンスターは倒さないと駄目だ。");

                                UpdateMainMessage("アイン：体力があれば、ライフが上がる分、そう簡単にやられなくなる。");

                                UpdateMainMessage("アイン：だが、それだけだ。それだけじゃ敵は倒せねえ。");

                                UpdateMainMessage("ラナ：負けはしないけど、勝ちもしないって所？");

                                UpdateMainMessage("ラナ：それもあるが、FiveSeeker達のそれぞれの特性、覚えてるか？");

                                UpdateMainMessage("ラナ：国王エルミ様は全能力。ファラ様は尊い心。");

                                UpdateMainMessage("ラナ：カール爵は卓越した知力、ヴェルゼさんは神業の技術。");

                                UpdateMainMessage("ラナ：ランディスお師匠さんは、言わずもながら力・・・っあ。");

                                UpdateMainMessage("アイン：そうだ、体力ってのは絶対必須だが、それまでって事だ。");

                                UpdateMainMessage("ラナ：でも、体力が無いとすぐやられちゃうからね。上げておくに越した事はないわ。");

                                UpdateMainMessage("アイン：そうだな。上げないわけにもいかねえから、適度に上げつつって所にしておくか。");
                                break;

                            case 5:
                                UpdateMainMessage("アイン：心を上げると、どういうわけか最強になったりしてな。");

                                UpdateMainMessage("ラナ：何で他人事台詞みたいな表現なのよ。言っとくけど");

                                UpdateMainMessage("アイン：心だけ上げても効果はねえ・・・だろ？わかってるって。");

                                UpdateMainMessage("アイン：ただ、ランディのボケから習ったことがあるが");

                                UpdateMainMessage("ランディス：『てめえ、剣の振り方、スッカスカだな！！　宿ってねえモン全然怖くねぇんだよ！！』");

                                UpdateMainMessage("ランディス：『というわけでだ。　もう１０ぺん死んでこいやあああぁぁぁぁ！！！！』");

                                UpdateMainMessage("ラナ：スッカスカって心のこと？");

                                UpdateMainMessage("アイン：ああ、多分そうだ。あのあとは酷かったぜ。マジで１０連敗だ。");

                                UpdateMainMessage("アイン：ランディのやつ、毎回本気なんだよ。手抜き・練習・お手本っていうのが全然ねえ。");

                                UpdateMainMessage("ラナ：心を上げておくと、常に本気・全力・運気全開って状態になるのかもね。");

                                UpdateMainMessage("アイン：そういう事だ。俺はこれに賭けてみるぜ。ラナ、楽しみにしててくれ。");
                                break;
                        }
                    }

                    UpdateMainMessage("ラナ：じゃあ、はいポーション。");
                    GetPotionForLana();

                    mainMessage.Text = "アイン：いつもサンキュー。";
                    we.AlreadyCommunicate = true;
                }
                else
                {
                    if (!we.AlreadyRest)
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "１４日目"
            else if (this.firstDay >= 14 && !we.CommunicationLana14 && mc.Level >= 5 && knownTileInfo[2])
            {
                if (!we.AlreadyCommunicate)
                {
                    UpdateMainMessage("アイン：久々に閃いたぜ！ダンジョン攻略法！！");

                    UpdateMainMessage("ラナ：え？　あのマトックで掘り返すので懲りたんじゃなかったの？");

                    UpdateMainMessage("アイン：そもそもだ。攻略なんて他にもあるはずだ。そうだろ？");

                    UpdateMainMessage("アイン：これを見ろ！パッパラパーン！！！");

                    UpdateMainMessage("ラナ：あんたの頭がパッパラパンでしょ・・・何それ？");

                    UpdateMainMessage("アイン：ダウジングだよ。何だラナ知らないのか？しょうがねえアイン先生が詳しく解説してやるよ。");

                    UpdateMainMessage("アイン：地下水や貴金属などの鉱脈、隠れたブツをこの棒や振り子などの装置の動きによって見つけるのさ！");

                    UpdateMainMessage("アイン：ッフ、どうした。突っ込みは無しか。どうやら返す言葉もねえようだな！ッハッハッハ！");

                    UpdateMainMessage("ラナ：「返す言葉が無い」って、そういう意味じゃないわよ・・・");

                    UpdateMainMessage("アイン：いよっしゃ！さあて、いっちょやってみるか！見てろよラナ。今最下層の場所を見つけるからな！");

                    UpdateMainMessage("ラナ：・・・（遠い目）");

                    UpdateMainMessage("・・・・・");

                    UpdateMainMessage("・・・・");

                    UpdateMainMessage("・・・");

                    UpdateMainMessage("・・");

                    UpdateMainMessage("アイン：見つけたぜ・・・ほら・・・");

                    UpdateMainMessage("        『アインはブラックマテリアルを発掘した。』");

                    UpdateMainMessage("ラナ：ブラックマテリアル、色付きマテリアルを加工した後で廃棄される・・・");

                    UpdateMainMessage("アイン：・・・・・・っく、それ以上は言うな！！");

                    UpdateMainMessage("ラナ：・・・はい、ポーション。");
                    GetPotionForLana();

                    we.AlreadyCommunicate = true;
                }
                else
                {
                    if (!we.AlreadyRest)
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "１５日目"
            else if (this.firstDay >= 15 && !we.CommunicationLana15 && mc.Level >= 5 && knownTileInfo[2])
            {
                if (!we.AlreadyCommunicate)
                {
                    UpdateMainMessage("ラナ：私思うのよね。このままじゃいけないって。");

                    UpdateMainMessage("アイン：何がだ。体重か？");

                    UpdateMainMessage("        『ドグッシャアアアァァ！！！（エターナルブローがアインに炸裂）』");

                    UpdateMainMessage("アイン：・・・・・・　　　（ッパタ）");

                    UpdateMainMessage("ラナ：薬学の知識上達と、武術・魔法術の両立ってなかなか難しいのよ。");

                    UpdateMainMessage("ラナ：薬草取りと調合ばっかりやってると、戦闘のカンが落ちるっていうのかしら。");

                    UpdateMainMessage("ラナ：普段からやっていないと、普段やってる方ばかりに偏っちゃってね。");

                    UpdateMainMessage("ラナ：こうしてたまにアインに拳武術でもやってないとなまっちゃうワケよ。ね、聞いてる？");

                    UpdateMainMessage("アイン：・・・・・・");

                    UpdateMainMessage("ラナ：おーい、今日はポーション要らないのかしら？");

                    UpdateMainMessage("アイン：要る。");

                    UpdateMainMessage("ラナ：起きてるんだったら、ちゃんと受け答えしなさいよ。");

                    UpdateMainMessage("アイン：お前のその武術、十分腕は落ちてねえと思うんだが・・・");

                    if (!we.CompleteArea1)
                    {
                        UpdateMainMessage("ラナ：　（ねえ、私もダンジョンに行くって言ったら・・・アイン良い？）");
                    }
                    else
                    {
                        UpdateMainMessage("ラナ：　（ねえ、私もダンジョンで拳戦闘術を・・・基本にして良い？）");
                    }

                    UpdateMainMessage("アイン：は？　なんか、聞こえなかった。なんだって？");

                    UpdateMainMessage("        『ボギャアアアァァァ！！！（ファントムキックがアインに炸裂）』");

                    UpdateMainMessage("アイン：ゲホオォォォ・・・いや、突然声が小さすぎて・・・ど、どうしろと・・・");

                    UpdateMainMessage("アイン：・・・・・・　　　（ッパタ）");
                    GetPotionForLana();
                    we.AlreadyCommunicate = true;
                }
                else
                {
                    if (!we.AlreadyRest)
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "１６日目"
            else if (this.firstDay >= 16 && !we.CommunicationLana16 && !we.CompleteSlayBoss1 && mc.Level >= 8 && knownTileInfo[386])
            {
                if (!we.AlreadyCommunicate)
                {
                    UpdateMainMessage("アイン：ッセイ！ッハ！");

                    UpdateMainMessage("ラナ：おっ、せいが出るじゃない。アイン。調子はどうなの？");

                    UpdateMainMessage("アイン：ダンジョン１階も結構進んでると思う。あと一歩だぜ！");

                    UpdateMainMessage("ラナ：アイン、【守護者】って聞いた事ある？");

                    UpdateMainMessage("アイン：ああ、一回手合わせした事があるぜ。アイツとんでもなく強いぞ。");

                    UpdateMainMessage("ラナ：【守護者】はある特定の戦術パターンを用いてくるそうだから、気をつけてね。");

                    UpdateMainMessage("アイン：ある特定の戦術パターン？何だそりゃ？");

                    UpdateMainMessage("ラナ：ライフが減ってきたら？");

                    UpdateMainMessage("アイン：フレッシュ・ヒール。");

                    UpdateMainMessage("ラナ：ライフが減ってなくて、スキルポイントもあるようなら？");

                    UpdateMainMessage("アイン：ストレート・スマッシュ。");

                    UpdateMainMessage("ラナ：大体そんな感じよ。");

                    UpdateMainMessage("アイン：なるほどな、いろいろ教えてくれてサンキュー！");

                    UpdateMainMessage("ラナ：【守護者】を倒す事で、次の階層へ進めると聞いたことがあるわ。もう少しね。");

                    UpdateMainMessage("アイン：ああ、今度こそアイツをぶっ倒してやるぜ！");

                    UpdateMainMessage("ラナ：ッフフ、そのいきよ。はい、ポーション♪");
                    GetPotionForLana();
                    we.AlreadyCommunicate = true;
                }
                else
                {
                    if (!we.AlreadyRest)
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "最後の日"
            else if (we.CommunicationCompArea4 && we.TruthWord5 && !we.CommunicationLana100)
            {
                we.CommunicationLana100 = true;
                if (!we.AlreadyCommunicate)
                {
                    UpdateMainMessage("ラナ：最下層の看板、【幻想世界】って意味、アインには分かる？");

                    UpdateMainMessage("アイン：ああ、何となくだがな。");

                    UpdateMainMessage("アイン：何となく俺が見た夢。あれの事だろ？");

                    UpdateMainMessage("ラナ：うん、アインが見た夢が関係してるのは間違いなさそうよ。");

                    UpdateMainMessage("アイン：あと、俺が何か探ろうとした時に来るひどい激痛もそうだ。");

                    UpdateMainMessage("アイン：それから、ラナ。お前が見せた『ごめんなさいＲＵＳＨ』も関係ありそうだな。");

                    UpdateMainMessage("ラナ：アイン、私ね。");

                    UpdateMainMessage("アイン：良いんだ、ラナ。最下層、制覇しようぜ。その後、ファージル宮殿行こうな。");

                    UpdateMainMessage("ラナ：ええ、そうね。ファージル宮殿、また行きましょ♪　はい、ポーション。");
                    GetPotionForLana();

                    UpdateMainMessage("アイン：サンキュー！");

                    we.AlreadyCommunicate = true;
                }
                else
                {
                    if (!we.AlreadyRest)
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "X日目"
            else
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        if (sc != null && sc.EmotionAngry)
                        {
                            UpdateMainMessage("ラナ：・・・赤ポーションよ。");
                            GetPotionForLana();

                            UpdateMainMessage("アイン：あ、ああ、行ってくるぜ。");

                            UpdateMainMessage("ラナ：・・・・・・");

                            UpdateMainMessage("アイン：・・・", true);
                        }
                        else
                        {
                            UpdateMainMessage("ラナ：どう？調子のほうは？");

                            UpdateMainMessage("アイン：当然当然。任せとけって！ッハッハッハ！");

                            UpdateMainMessage("ラナ：何をどう任せるのやら・・・ホラ、明日の赤ポーションよ。");
                            GetPotionForLana();

                            UpdateMainMessage("アイン：オッ、サンキュ！これでまた明日も頑張ってくるぜ。", true);
                        }
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        if (sc != null && sc.EmotionAngry)
                        {
                            mainMessage.Text = "ラナ：何よ・・・休めば？";
                        }
                        else
                        {
                            mainMessage.Text = MessageFormatForLana(1001);
                        }
                    }
                }
                else
                {
                    if (!we.AlreadyCommunicate)
                    {
                        if (sc != null && sc.EmotionAngry)
                        {
                            UpdateMainMessage("ラナ：・・・赤ポーションよ。");
                            GetPotionForLana();

                            UpdateMainMessage("アイン：あ、ああ、行ってくるぜ。");

                            UpdateMainMessage("ラナ：・・・・・・");

                            UpdateMainMessage("アイン：・・・", true);
                        }
                        else
                        {
                            UpdateMainMessage("ラナ：はい、今日の赤ポーション。");
                            GetPotionForLana();

                            UpdateMainMessage("アイン：サンキュ！じゃいってくら！");

                            UpdateMainMessage("ラナ：死なないようにがんばる事ね。", true);
                        }
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        if (sc != null && sc.EmotionAngry)
                        {
                            mainMessage.Text = "ラナ：・・・勝手に行ってくれば";
                        }
                        else
                        {
                            mainMessage.Text = MessageFormatForLana(1002);
                        }
                    }
                }
            }
            #endregion

        }

        /// <summary>
        /// if-else文とフラグを複雑化させてガンツ叔父さんとの会話を盛り上げてください。
        /// </summary>
        private void button4_Click(object sender, EventArgs e)
        {
            int firstOpenDay = 3;
            #region "１日目 - ２日目"
            if (this.firstDay >= 1 && this.firstDay <= (firstOpenDay-1) && mc.Level >= 1 && knownTileInfo[2] &&
                 (!we.CommunicationGanz1 ||
                  !we.CommunicationGanz2/* ||
                  !we.CommunicationGanz3 ||
                  !we.CommunicationGanz4*/))
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyEquipShop)
                    {
                        if (!we.CommunicationGanz1 && !we.CommunicationGanz2) // && !we.CommunicationGanz3 && !we.CommunicationGanz4)
                        {
                            mainMessage.Text = "アイン：ガンツ叔父さん、居ますか？";
                            ok.ShowDialog();
                            mainMessage.Text = "ガンツ：うむ、少しそこで待っておれ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ああ。";
                            ok.ShowDialog();
                            mainMessage.Text = "・・・・・";
                            ok.ShowDialog();
                            mainMessage.Text = "・・・・";
                            ok.ShowDialog();
                            mainMessage.Text = "・・・";
                            ok.ShowDialog();
                            mainMessage.Text = "ガンツ：待たせたな。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：良い品は揃ってるか？";
                            ok.ShowDialog();
                            mainMessage.Text = "ガンツ：実はさっき新しく来た冒険者が全て買占めてしまった所だ。すまんな。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：何ぃ！？！？武具の買占めってどういう事だよ！？";
                            ok.ShowDialog();
                            mainMessage.Text = "ガンツ：一般的な物から、超一級品の物まで全て持っていきおった。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：マジかよ・・・次の入荷は何時になるんだ？";
                            ok.ShowDialog();
                            if (firstOpenDay - we.GameDay <= 0)
                            {
                                UpdateMainMessage("ガンツ：心配せんでも、すでに入荷済み。今準備しておる所だ、ダンジョンでも行って来るが良い。");

                                UpdateMainMessage("アイン：本当か！？やったぜ！じゃあダンジョン行って来るから、帰ってきたら見せてくれよ！");

                                UpdateMainMessage("ガンツ：二言はない。はやく行って来なさい。");

                                UpdateMainMessage("アイン：了解！！");
                            }
                            else
                            {
                                mainMessage.Text = "ガンツ：あと" + Convert.ToString(firstOpenDay - we.GameDay) + "日は待っておれ。";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：ったく、買占めなんてするヤツの気が知れねえな。";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：まあしょうがねえ、また来るぜ！";
                                ok.ShowDialog();
                                mainMessage.Text = "ガンツ：入荷次第、教える。その時にまた来るがいい。";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：了解！！";
                            }
                            we.AlreadyEquipShop = true;
                        }
                        else
                        {
                            UpdateMainMessage("アイン：ガンツ叔父さん、準備まだ？");

                            if (firstOpenDay - we.GameDay <= 0)
                            {
                                UpdateMainMessage("ガンツ：入荷済みで、今準備しておる所だ、ダンジョンでも行って来るが良い。");

                                UpdateMainMessage("アイン：本当か！？やったぜ！じゃあダンジョン行って来るから、帰ってきたら見せてくれよ！");

                                UpdateMainMessage("ガンツ：二言はない。はやく行って来なさい。");

                                UpdateMainMessage("アイン：了解！！");
                            }
                            else
                            {
                                UpdateMainMessage("ガンツ：あと" + Convert.ToString(firstOpenDay - we.GameDay) + "日は待っておれ。");

                                UpdateMainMessage("アイン：オーケー、了解。");
                            }

                            we.AlreadyEquipShop = true;
                        }
                    }
                    else
                    {
                        if (firstOpenDay - we.GameDay <= 0)
                        {
                            UpdateMainMessage("ガンツ：入荷済みで、今準備しておる所だ、ダンジョンでも行って来るが良い。");
                        }
                        else
                        {
                            mainMessage.Text = "ガンツ：あと" + Convert.ToString(firstOpenDay - we.GameDay) + "日待っておれ。";
                        }
                    }
                }
                else
                {
                    if (!we.AlreadyEquipShop)
                    {
                        if (!we.CommunicationGanz1 && !we.CommunicationGanz2) // && !we.CommunicationGanz3 && !we.CommunicationGanz4)
                        {
                            mainMessage.Text = "アイン：ガンツ叔父さん、居ますか？";
                            ok.ShowDialog();
                            mainMessage.Text = "ガンツ：うむ、少しそこで待っておれ。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：ああ。";
                            ok.ShowDialog();
                            mainMessage.Text = "・・・・・";
                            ok.ShowDialog();
                            mainMessage.Text = "・・・・";
                            ok.ShowDialog();
                            mainMessage.Text = "・・・";
                            ok.ShowDialog();
                            mainMessage.Text = "ガンツ：待たせたな。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：良い品は揃ってるか？";
                            ok.ShowDialog();
                            mainMessage.Text = "ガンツ：実はさっき新しく来た冒険者が全て買占めてしまった所だ。すまんな。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：何ぃ！？！？武具の買占めってどういう事だよ！？";
                            ok.ShowDialog();
                            mainMessage.Text = "ガンツ：一般的な物から、超一級品の物まで全て持っていきおった。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：マジかよ・・・次の入荷は何時になるんだ？";
                            ok.ShowDialog();
                            if (firstOpenDay - we.GameDay <= 0)
                            {
                                UpdateMainMessage("ガンツ：心配せんでも、すでに入荷済み。今準備しておる所だ、ダンジョンでも行って来るが良い。");

                                UpdateMainMessage("アイン：本当か！？やったぜ！じゃあダンジョン行って来るから、帰ってきたら見せてくれよ！");

                                UpdateMainMessage("ガンツ：二言はない。はやく行って来なさい。");

                                UpdateMainMessage("アイン：了解！！");
                            }
                            else
                            {
                                mainMessage.Text = "ガンツ：あと" + Convert.ToString(firstOpenDay - we.GameDay) + "日は待っておれ。";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：ったく、買占めなんてするヤツの気が知れねえな。";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：まあしょうがねえ、また来るぜ！";
                                ok.ShowDialog();
                                mainMessage.Text = "ガンツ：入荷次第、教える。その時にまた来るがいい。";
                                ok.ShowDialog();
                                mainMessage.Text = "アイン：了解！！";
                            }
                            we.AlreadyEquipShop = true;
                        }
                        else
                        {
                            UpdateMainMessage("アイン：ガンツ叔父さん、準備まだ？");

                            if (firstOpenDay - we.GameDay <= 0)
                            {
                                UpdateMainMessage("ガンツ：入荷済みで、今準備しておる所だ、ダンジョンでも行って来るが良い。");

                                UpdateMainMessage("アイン：本当か！？やったぜ！じゃあダンジョン行って来るから、帰ってきたら見せてくれよ！");

                                UpdateMainMessage("ガンツ：二言はない。はやく行って来なさい。");

                                UpdateMainMessage("アイン：了解！！");
                            }
                            else
                            {
                                UpdateMainMessage("ガンツ：あと" + Convert.ToString(firstOpenDay - we.GameDay) + "日は待っておれ。");

                                UpdateMainMessage("アイン：オーケー、了解。");
                            }

                            we.AlreadyEquipShop = true;
                        }
                    }
                    else
                    {
                        if (firstOpenDay - we.GameDay <= 0)
                        {
                            UpdateMainMessage("ガンツ：入荷済みで、今準備しておる所だ、ダンジョンでも行って来るが良い。");
                        }
                        else
                        {
                            mainMessage.Text = "ガンツ：あと" + Convert.ToString(firstOpenDay - we.GameDay) + "日待っておれ。";
                        }
                    }
                }
            }
            #endregion
            #region "３日目"
            else if (this.firstDay >= firstOpenDay && !we.CommunicationGanz3/*5*/ && mc.Level >= 1 && knownTileInfo[2])
            {
                we.CommunicationGanz3 = true;
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyEquipShop)
                    {
                        mainMessage.Text = "アイン：ガンツ叔父さん、お店、空いてますか？";
                        ok.ShowDialog();
                        mainMessage.Text = "ガンツ：おお、よく来たなアイン。店の方は空いておるぞ。好きなだけ見ていくが良い。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：よっしゃ！早速拝見させてもらうぜ！";
                        ok.ShowDialog();
                        mainMessage.Text = "ガンツ：ただし、こう言ってはなんだが。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ん？";
                        ok.ShowDialog();
                        if (!we.CommunicationGanz1 && !we.CommunicationGanz2 && !we.CommunicationGanz3 && !we.CommunicationGanz4)
                        {
                            UpdateMainMessage("ガンツ：以前、武具屋の品物が全て売り切れてしまってな。");

                            UpdateMainMessage("ガンツ：新しく調達した品物しかまだ店には出せん。");
                        }
                        else
                        {
                            mainMessage.Text = "ガンツ：武具製作の都でもあるクヴェルタ街。そこの鍛冶屋ヴァスタ爺曰く・・・";
                            ok.ShowDialog();
                            mainMessage.Text = "ヴァスタ：『誰じゃ！武具の買占めなんぞするヤツは。金のムダ使いじゃな。』";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：俺と同じ事言ってるな。ッハッハッハ！";
                            ok.ShowDialog();
                            mainMessage.Text = "ヴァスタ：『本来３日間では到底無理だが、ガンツ。お前とは古い付き合いじゃからの。』";
                            ok.ShowDialog();
                            mainMessage.Text = "ガンツ：そういう訳で、本当に基本的な武具だけを揃えてもらった。";
                            ok.ShowDialog();
                        }

                        UpdateMainMessage("アイン：いいや、それだけでも十分だぜ。本当に助かるよ。");

                        UpdateMainMessage("ガンツ：品数は少ないが、使える代物ばかりだ。見ていってくれ。");

                        we.AvailableEquipShop = true;
                        CallEquipmentShop();

                        mainMessage.Text = "";
                        we.AlreadyEquipShop = true;
                    }
                    else
                    {
                        we.AvailableEquipShop = true;
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                }
                else
                {
                    if (!we.AlreadyEquipShop)
                    {
                        mainMessage.Text = "アイン：約束の３日間だぜ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ガンツ：おお、よく来たなアイン。お待たせしたな。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：よっしゃ！早速拝見させてもらうぜ！";
                        ok.ShowDialog();
                        mainMessage.Text = "ガンツ：ただし、こう言ってはなんだが。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：ん？何だ？";
                        ok.ShowDialog();
                        mainMessage.Text = "ガンツ：武具製作の都でもあるクヴェルタ街。そこの鍛冶屋ヴァスタ爺曰く・・・";
                        ok.ShowDialog();
                        mainMessage.Text = "ヴァスタ：『誰じゃ！武具の買占めなんぞするヤツは。金のムダ使いじゃな。』";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：俺と同じ事言ってるな。ッハッハッハ！";
                        ok.ShowDialog();
                        mainMessage.Text = "ヴァスタ：『本来３日間では到底無理だが、ガンツ。お前とは古い付き合いじゃからの。』";
                        ok.ShowDialog();
                        mainMessage.Text = "ガンツ：そういう訳で、本当に基本的な武具だけを揃えてもらった。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：いいや、それだけでも十分だぜ。本当に助かるよ。";
                        ok.ShowDialog();
                        mainMessage.Text = "ガンツ：品数は少ないが、使える代物ばかりだ。見ていってくれ。";
                        ok.ShowDialog();
                        mainMessage.Text = "アイン：おう、遠慮なく見させてもらうぜ。";
                        ok.ShowDialog();

                        we.AvailableEquipShop = true;
                        CallEquipmentShop();

                        mainMessage.Text = "";
                        we.AlreadyEquipShop = true;
                    }
                    else
                    {
                        we.AvailableEquipShop = true;
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                }
            }
            #endregion
            #region "１階制覇後、ＬＶ２武具販売"
            else if (we.CommunicationCompArea1 && !we.AvailableEquipShop2)
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyEquipShop)
                    {
                        UpdateMainMessage("アイン：こんちわー。");

                        UpdateMainMessage("ガンツ：うむ、アインか。すまんが、今は準備中だ。");
                        
                        UpdateMainMessage("アイン：ん？そうなのか？");
                        
                        UpdateMainMessage("ガンツ：明日になれば、すぐにまた開店出来る。その時に来てくれ。");
                        
                        UpdateMainMessage("アイン：オーケー、了解。");

                        we.AlreadyEquipShop = true;
                    }
                    else
                    {
                        UpdateMainMessage("ガンツ：明日になれば、すぐにまた開店出来る。その時に来てくれ。");
                    }
                }
                else
                {
                    if (!we.AlreadyEquipShop || !we.AvailableEquipShop2)
                    {
                        we.AvailableEquipShop2 = true;
                        UpdateMainMessage("アイン：こんちわー。っと、おぉ！？");

                        UpdateMainMessage("ガンツ：うむ、アインか。待っておったぞ。");

                        UpdateMainMessage("アイン：武具の種類が増加している！すげえな！！");

                        UpdateMainMessage("ガンツ：それと、ラナ君にも似合う武具を用意しておいた。見ていってくれ。");

                        UpdateMainMessage("ラナ：っえぇ？私にもですか！？  ありがとうございます、ガンツ叔父さん。");

                        UpdateMainMessage("アイン：ラナ、お前ダンジョンに行く事、ガンツさんに喋ってたのか？");

                        UpdateMainMessage("ラナ：そんなわけないでしょ。昨日アインに言ったのが始めてだし。");

                        UpdateMainMessage("ガンツ：ハンナは汲み取るのが性分でな。ラナ君が行こうとしているのをワシに伝えてきおった。");

                        UpdateMainMessage("ラナ：あ、ハンナ叔母さんが・・・？");

                        UpdateMainMessage("ガンツ：あやつは、要らん事を言いに来るからな、まったく叶わんよ。");

                        UpdateMainMessage("ラナ：すいません、何かわざわざ・・・");

                        UpdateMainMessage("ガンツ：何を言っておる、ワシとしては大歓迎だよ。さあ、見てきなさい。");

                        we.AlreadyEquipShop = true;
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                    else
                    {
                        we.AvailableEquipShop2 = true;
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                }
            }
            #endregion
            #region "２階制覇後、ＬＶ３武具販売"
            else if (we.CommunicationCompArea2 && !we.AvailableEquipShop3)
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyEquipShop)
                    {
                        UpdateMainMessage("アイン：こんちわー。");

                        UpdateMainMessage("ガンツ：うむ、アインか。先ほどヴァスタ爺がワシの所に来ておってな。今は準備中だ。");

                        UpdateMainMessage("アイン：ん？ヴァスタ爺さんってちょくちょくココに訪れるのか？");

                        UpdateMainMessage("ガンツ：年寄り共の戯れだ、気にするな。また開店する。その時に来るとよい。");

                        UpdateMainMessage("アイン：オーケー、了解。");

                        we.AlreadyEquipShop = true;
                    }
                    else
                    {
                        UpdateMainMessage("ガンツ：明日になれば、すぐにまた開店する。その時に来るとよい。");
                    }
                }
                else
                {
                    if (!we.AlreadyEquipShop || !we.AvailableEquipShop3)
                    {
                        we.AvailableEquipShop3 = true;
                        UpdateMainMessage("ガンツ：アインとラナ君か。お待たせしたな。");

                        UpdateMainMessage("アイン：おお！品揃えが強化されている！！見て回っていいですか！？");

                        UpdateMainMessage("ラナ：うわぁ、結構品揃えが増えてるわ。どれにしようか迷っちゃうわね♪");

                        UpdateMainMessage("ガンツ：３階は今まで以上に強烈なモンスターが出てくるだろう。準備は怠るな。");
                        
                        UpdateMainMessage("アインとラナ：ッハイ！");

                        we.AlreadyEquipShop = true;
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                    else
                    {
                        we.AvailableEquipShop3 = true;
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                }
            }
            #endregion
            #region "３階制覇後、ＬＶ４武具販売"
            else if (we.CommunicationCompArea3 && !we.AvailableEquipShop4)
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyEquipShop)
                    {
                        UpdateMainMessage("アイン：こんちわー。");

                        UpdateMainMessage("ガンツ：うむ、アインか。今しばらく待っておれ。自信作を準備している所だ。");

                        UpdateMainMessage("アイン：おお！マジですか！？ええ、いくらでも待ちますよ！");

                        UpdateMainMessage("ガンツ：アインがダンジョンに行っている間に整えておこう。また来るとよい。");

                        UpdateMainMessage("アイン：オーケー！");

                        we.AlreadyEquipShop = true;
                    }
                    else
                    {
                        UpdateMainMessage("ガンツ：明日になれば、すぐにまた開店する。その時に来るとよい。");
                    }
                }
                else
                {
                    if (!we.AlreadyEquipShop || !we.AvailableEquipShop4)
                    {
                        we.AvailableEquipShop4 = true;
                        UpdateMainMessage("ガンツ：お待たせしたな。４階用のアイテムはより強化したものに仕上げてある。");

                        UpdateMainMessage("アイン：すげえ・・・今まで見たことも無いぐらいの質の高さだ・・・");

                        UpdateMainMessage("ラナ：アクセサリ関連も随分と増えたわね。全部欲しいくらいね。");

                        UpdateMainMessage("ガンツ：残念だが、値段はそれ相応にしてある。");
                        
                        UpdateMainMessage("アイン：全然気にしませんよ！");

                        UpdateMainMessage("ガンツ：うむ、では見て行くが良い。");

                        we.AlreadyEquipShop = true;
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                    else
                    {
                        we.AvailableEquipShop4 = true;
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                }
            }
            #endregion
            #region "４階制覇後、５階看板到達後、ガンツ武具屋閉店"
            else if (we.CommunicationCompArea4 && we.TruthWord5 && !we.AvailableEquipShop5 && !we.CommunicationGanz100)
            {
                if (!we.AlreadyRest)
                {
                    we.AvailableEquipShop5 = true;
                    we.CommunicationGanz100 = true;
                    if (!we.AlreadyEquipShop)
                    {
                        UpdateMainMessage("アイン：こんちわー。");

                        UpdateMainMessage("アイン：あれ？誰も居ないな。");

                        UpdateMainMessage("ラナ：アイン、ねえ見てよ。ココに書置きがあるわよ。");

                        UpdateMainMessage("アイン：何て書いてあるんだ？");

                        UpdateMainMessage("ラナ：ええっとね、こう書いてあるわ・・・");

                        if (we.SpecialTreasure1)
                        {
                            UpdateMainMessage("　　　『時空のルーツを入手したため、天下一品　ガンツ武具店は閉店とする。』");

                            UpdateMainMessage("アイン：時空のルーツ？あのタイム・オブ・ルーセのことか？");

                            UpdateMainMessage("ラナ：多分そうね。でも、突然閉店するなんて相当な品物だったみたいね。");
                        }
                        else
                        {
                            UpdateMainMessage("　　　『時空のルーツを探すべく、天下一品　ガンツ武具店は閉店とする。』");

                            UpdateMainMessage("アイン：時空のルーツ？何だそりゃ。聞いた事がねえな。");

                            UpdateMainMessage("ラナ：私も初めて聞くわ。でも、たまに閉店するのもガンツ叔父さんらしいわね。");
                        }

                        UpdateMainMessage("アイン：しかし客商売なんだから、誰か代理の雇い人でも付けてやれば良いのにな。");

                        UpdateMainMessage("アイン：ってか、もう買えねえって事かよ！？おい、マジかよ！");

                        UpdateMainMessage("ラナ：ちゃんと注意書きまで書いてあるわよ。ええっとね・・・");

                        UpdateMainMessage("　　　『なお、現在売られている武具は、購入時に必ずGoldを支払うように　【Ganz.Gimerga】");

                        UpdateMainMessage("アイン：誰も居ないんだから、取り放題だろ。こんなので商売が成り立つのか？");

                        UpdateMainMessage("ラナ：でもガンツ叔父さん怒らせると怖いし、誰も盗みはやらないんじゃないの？");

                        UpdateMainMessage("アイン：・・・それもそうだな。");

                        UpdateMainMessage("ラナ：まあ、入ってみましょ。もし欲しい物があれば購入自体はして良いみたいだし。");

                        UpdateMainMessage("アイン：オーケー、じゃあ店主不在の武具店にお邪魔してみるか！");

                        we.AlreadyEquipShop = true;
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                    else
                    {
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                }
                else
                {
                    we.AvailableEquipShop5 = true;
                    we.CommunicationGanz100 = true;
                    if (!we.AlreadyEquipShop)
                    {
                        UpdateMainMessage("アイン：こんちわー。");

                        UpdateMainMessage("アイン：あれ？誰も居ないな。");

                        UpdateMainMessage("ラナ：アイン、ねえ見てよ。ココに書置きがあるわよ。");

                        UpdateMainMessage("アイン：何て書いてあるんだ？");

                        UpdateMainMessage("ラナ：ええっとね、こう書いてあるわ・・・");

                        if (we.SpecialTreasure1)
                        {
                            UpdateMainMessage("　　　『時空のルーツを入手したため、天下一品　ガンツ武具店は閉店とする。』");

                            UpdateMainMessage("アイン：時空のルーツ？あのタイム・オブ・ルーセのことか？");

                            UpdateMainMessage("ラナ：多分そうね。でも、突然閉店するなんて相当な品物だったみたいね。");
                        }
                        else
                        {
                            UpdateMainMessage("　　　『時空のルーツを探すべく、天下一品　ガンツ武具店は閉店とする。』");

                            UpdateMainMessage("アイン：時空のルーツ？何だそりゃ。聞いた事がねえな。");

                            UpdateMainMessage("ラナ：私も初めて聞くわ。でも、たまに閉店するのもガンツ叔父さんらしいわね。");
                        }

                        UpdateMainMessage("アイン：しかし客商売なんだから、誰か代理の雇い人でも付けてやれば良いのにな。");

                        UpdateMainMessage("アイン：ってか、もう買えねえって事かよ！？おい、マジかよ！");

                        UpdateMainMessage("ラナ：ちゃんと注意書きまで書いてあるわよ。ええっとね・・・");

                        UpdateMainMessage("　　　『なお、現在売られている武具は、購入時に必ずGoldを支払うように　【Ganz.Gimerga】");

                        UpdateMainMessage("アイン：誰も居ないんだから、取り放題だろ。こんなので商売が成り立つのか？");

                        UpdateMainMessage("ラナ：でもガンツ叔父さん怒らせると怖いし、誰も盗みはやらないんじゃないの？");

                        UpdateMainMessage("アイン：・・・それもそうだな。");

                        UpdateMainMessage("ラナ：まあ、入ってみましょ。もし欲しい物があれば購入自体はして良いみたいだし。");

                        UpdateMainMessage("アイン：オーケー、じゃあ店主不在の武具店にお邪魔してみるか！");

                        we.AlreadyEquipShop = true;
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                    else
                    {
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }

                }
                
            }
            #endregion
            #region "Ｘ日目"
            else
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AvailableEquipShop)
                    {
                        if (!we.AlreadyEquipShop)
                        {
                            int target = firstOpenDay - we.GameDay;// day;
                            mainMessage.Text = "ガンツ：あと" + target + "日待っておれ。";
                            we.AlreadyEquipShop = true;
                        }
                        else
                        {
                            int target = firstOpenDay - we.GameDay;// day;
                            mainMessage.Text = "ガンツ：あと" + target + "日待っておれ。";
                        }
                    }
                    else
                    {
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                }
                else
                {
                    if (!we.AvailableEquipShop)
                    {
                        if (!we.AlreadyCommunicate)
                        {
                            int target = firstOpenDay - we.GameDay;// day;
                            mainMessage.Text = "ガンツ：あと" + target + "日だ。今のままでダンジョンへ行って来い。";
                            we.AlreadyEquipShop = true;
                        }
                        else
                        {
                            int target = firstOpenDay - we.GameDay;// day;
                            mainMessage.Text = "ガンツ：あと" + target + "日だ。今のままでダンジョンへ行って来い。";
                        }
                    }
                    else
                    {
                        CallEquipmentShop();
                        mainMessage.Text = "";
                    }
                }
            }
            #endregion
        }

        private void CallEquipmentShop()
        {
            using (EquipmentShop ES = new EquipmentShop())
            {
                ES.StartPosition = FormStartPosition.CenterParent;
                ES.MC = this.mc;
                ES.SC = this.sc;
                ES.TC = this.tc;
                ES.WE = this.we;
                ES.ShowDialog();
            }
        }

        private void HomeTown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                using (ESCMenu esc = new ESCMenu())
                {
                    esc.MC = this.MC;
                    esc.SC = this.SC;
                    esc.TC = this.TC;
                    esc.WE = this.we;
                    esc.KnownTileInfo = this.knownTileInfo;
                    esc.KnownTileInfo2 = this.knownTileInfo2;
                    esc.KnownTileInfo3 = this.knownTileInfo3;
                    esc.KnownTileInfo4 = this.knownTileInfo4;
                    esc.KnownTileInfo5 = this.knownTileInfo5;
                    esc.StartPosition = FormStartPosition.CenterParent;
                    esc.ShowDialog();
                    if (esc.DialogResult == DialogResult.Retry)
                    {
                        this.mc = esc.MC;
                        this.sc = esc.SC;
                        this.tc = esc.TC;
                        this.we = esc.WE;
                        this.knownTileInfo = esc.KnownTileInfo;
                        this.knownTileInfo2 = esc.KnownTileInfo2;
                        this.knownTileInfo3 = esc.KnownTileInfo3;
                        this.knownTileInfo4 = esc.KnownTileInfo4;
                        this.knownTileInfo5 = esc.KnownTileInfo5;
                        this.DialogResult = DialogResult.Retry;
                    }
                    else if (esc.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    }
                }
            }
        }


        // [警告]：Form1.csからコピペしました。改版時はForm1.csもお忘れなく。
        private bool EncountBattle(string enemyName)
        {
            bool endFlag = false;
            while (!endFlag)
            {
                System.Threading.Thread.Sleep(1000);
                using (BattleEnemy be = new BattleEnemy())
                {
                    MainCharacter tempMC = new MainCharacter();
                    MainCharacter tempSC = new MainCharacter();
                    MainCharacter tempTC = new MainCharacter();
                    WorldEnvironment tempWE = new WorldEnvironment();

                    tempMC.MainArmor = this.MC.MainArmor;
                    tempMC.MainWeapon = this.MC.MainWeapon;
                    tempMC.Accessory = this.MC.Accessory;
                    tempSC.MainArmor = this.SC.MainArmor;
                    tempSC.MainWeapon = this.SC.MainWeapon;
                    tempSC.Accessory = this.SC.Accessory;
                    tempTC.MainArmor = this.TC.MainArmor;
                    tempTC.MainWeapon = this.TC.MainWeapon;
                    tempTC.Accessory = this.TC.Accessory;

                    ItemBackPack[] tempBackPack = new ItemBackPack[this.MC.GetBackPackInfo().Length];
                    tempBackPack = MC.GetBackPackInfo();
                    be.MC = tempMC;
                    for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
                    {
                        if (tempBackPack[ii] != null)
                        {
                            be.MC.AddBackPack(tempBackPack[ii]);
                        }
                    }

                    //if (WE.AvailableSecondCharacter)
                    //{
                    //    ItemBackPack[] tempBackPack2 = new ItemBackPack[this.SC.GetBackPackInfo().Length];
                    //    tempBackPack2 = SC.GetBackPackInfo();
                    //    be.SC = tempSC;
                    //    for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
                    //    {
                    //        if (tempBackPack2[ii] != null)
                    //        {
                    //            be.SC.AddBackPack(tempBackPack2[ii]);
                    //        }
                    //    }
                    //}
                    //else
                    //{
                        be.SC = null;
                    //}

                    //if (WE.AvailableThirdCharacter)
                    //{
                    //    ItemBackPack[] tempBackPack3 = new ItemBackPack[this.TC.GetBackPackInfo().Length];
                    //    tempBackPack3 = TC.GetBackPackInfo();
                    //    be.TC = tempTC;
                    //    for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
                    //    {
                    //        if (tempBackPack3[ii] != null)
                    //        {
                    //            be.TC.AddBackPack(tempBackPack3[ii]);
                    //        }
                    //    }
                    //}
                    //else
                    //{
                        be.TC = null;
                    //}

                    EnemyCharacter1 ec1 = new EnemyCharacter1(enemyName, this.difficulty);
                    if (enemyName == "ヴェルゼ・アーティ")
                    {
                        ec1.MainWeapon = new ItemBackPack("白銀の剣（レプリカ）");
                        ec1.MainArmor = new ItemBackPack("黒真空の鎧（レプリカ）");
                        ec1.Accessory = new ItemBackPack("天空の翼（レプリカ）");
                    }
                    be.EC1 = ec1;

                    Type type = tempMC.GetType();
                    foreach (PropertyInfo pi in type.GetProperties())
                    {
                        // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                        if (pi.PropertyType == typeof(System.Int32))
                        {
                            try
                            {
                                pi.SetValue(tempMC, (System.Int32)(type.GetProperty(pi.Name).GetValue(this.MC, null)), null);
                                pi.SetValue(tempSC, (System.Int32)(type.GetProperty(pi.Name).GetValue(this.SC, null)), null);
                                pi.SetValue(tempTC, (System.Int32)(type.GetProperty(pi.Name).GetValue(this.TC, null)), null);
                            }
                            catch { }
                        }
                        else if (pi.PropertyType == typeof(System.String))
                        {
                            try
                            {
                                pi.SetValue(tempMC, (string)(type.GetProperty(pi.Name).GetValue(this.MC, null)), null);
                                pi.SetValue(tempSC, (string)(type.GetProperty(pi.Name).GetValue(this.SC, null)), null);
                                pi.SetValue(tempTC, (string)(type.GetProperty(pi.Name).GetValue(this.TC, null)), null);
                            }
                            catch { }
                        }
                        else if (pi.PropertyType == typeof(System.Boolean))
                        {
                            try
                            {
                                pi.SetValue(tempMC, (System.Boolean)(type.GetProperty(pi.Name).GetValue(this.MC, null)), null);
                                pi.SetValue(tempSC, (System.Boolean)(type.GetProperty(pi.Name).GetValue(this.SC, null)), null);
                                pi.SetValue(tempTC, (System.Boolean)(type.GetProperty(pi.Name).GetValue(this.TC, null)), null);
                            }
                            catch { }
                        }
                    }

                    Type type2 = tempWE.GetType();
                    foreach (PropertyInfo pi in type2.GetProperties())
                    {
                        // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                        if (pi.PropertyType == typeof(System.Int32))
                        {
                            try
                            {
                                pi.SetValue(tempWE, (System.Int32)(type2.GetProperty(pi.Name).GetValue(this.WE, null)), null);
                            }
                            catch { }
                        }
                        else if (pi.PropertyType == typeof(System.String))
                        {
                            try
                            {
                                pi.SetValue(tempWE, (string)(type2.GetProperty(pi.Name).GetValue(this.WE, null)), null);
                            }
                            catch { }
                        }
                        else if (pi.PropertyType == typeof(System.Boolean))
                        {
                            try
                            {
                                pi.SetValue(tempWE, (System.Boolean)(type2.GetProperty(pi.Name).GetValue(this.WE, null)), null);
                            }
                            catch { }
                        }
                    }

                    be.WE = tempWE;
                    be.StartPosition = FormStartPosition.CenterParent;
                    be.BattleSpeed = this.battleSpeed;
                    be.Difficulty = this.difficulty;
                    be.ShowDialog();
                    if (be.DialogResult == DialogResult.Retry)
                    {
                        // 死亡時、再挑戦する場合、はじめから呼びなおす。
                        if (ec1.Name == "ヴェルゼ・アーティ")
                        {
                            this.mainMessage.Text = "ヴェルゼ：何度でもかかってきてください。";
                        }
                        else
                        {
                            this.mainMessage.Text = "";
                        }
                        this.Update();
                        continue;
                    }
                    if (be.DialogResult == DialogResult.Abort)
                    {
                        // 逃げた時、経験値とゴールドは入らない。
                        this.MC = tempMC;
                        this.MC.ReplaceBackPack(tempMC.GetBackPackInfo());
                        //if (WE.AvailableSecondCharacter)
                        //{
                        //    this.SC = tempSC;
                        //    this.SC.ReplaceBackPack(tempSC.GetBackPackInfo());
                        //}
                        //if (WE.AvailableThirdCharacter)

                        //    this.TC = tempTC;
                        //    this.TC.ReplaceBackPack(tempTC.GetBackPackInfo());
                        //}
                        //this.WE = tempWE; // WEはHomeTownで更新します。
                        return false;
                    }
                    else if (be.DialogResult == DialogResult.Ignore)
                    {
                        endFlag = true;
                        //this.WE = tempWE; // WEはHomeTownで更新します。
                    }
                    else
                    {
                        //if (WE.AvailableFirstCharacter)
                        //{
                        //this.MC = tempMC;
                        this.MC.ReplaceBackPack(tempMC.GetBackPackInfo());
                        //    MC.Exp += be.EC1.Exp;
                        //    MC.Gold += be.EC1.Gold;
                        //    if (MC.Exp >= MC.NextLevelBorder)
                        //    {
                        //        mainMessage.Text = "アイン：レベルアップだぜ！！";
                        //        using (StatusPlayer sp = new StatusPlayer())
                        //        {
                        //            // [警告]：レベルアップのMAXライフが常に２０、マナが３０で良いかどうか検討してください。
                        //            MC.BaseLife += 20;
                        //            MC.BaseMana += 30;
                        //            //MC.CurrentLife = MC.BaseLife;
                        //            MC.Exp = MC.Exp - MC.NextLevelBorder;
                        //            MC.Level += 1;
                        //            sp.WE = WE;
                        //            sp.MC = MC;
                        //            sp.SC = SC;
                        //            sp.TC = TC;
                        //            sp.CurrentStatusView = Color.LightSkyBlue;
                        //            sp.LevelUp = true;
                        //            sp.UpPoint = MC.LevelUpPoint;
                        //            sp.StartPosition = FormStartPosition.CenterParent;
                        //            sp.ShowDialog();
                        //        }
                        //    }
                        //}
                        //if (WE.AvailableSecondCharacter)
                        //{
                        //    this.SC = tempSC;
                        //    this.SC.ReplaceBackPack(tempSC.GetBackPackInfo());
                        //    SC.Exp += be.EC1.Exp;
                        //    //SC.Gold += be.EC1.Gold; // [警告]：ゴールドの所持は別クラスにするべきです。
                        //    if (SC.Exp >= SC.NextLevelBorder)
                        //    {
                        //        mainMessage.Text = "ラナ：来たわ、レベルアップ♪";
                        //        using (StatusPlayer sp = new StatusPlayer())
                        //        {
                        //            // [警告]：レベルアップのMAXライフが常に２０、マナが３０で良いかどうか検討してください。
                        //            SC.BaseLife += 20;
                        //            SC.BaseMana += 30;
                        //            //MC.CurrentLife = MC.BaseLife;
                        //            SC.Exp = SC.Exp - SC.NextLevelBorder;
                        //            SC.Level += 1;
                        //            sp.WE = WE;
                        //            sp.MC = MC;
                        //            sp.SC = SC;
                        //            sp.TC = TC;
                        //            sp.CurrentStatusView = Color.Pink;
                        //            sp.LevelUp = true;
                        //            sp.UpPoint = SC.LevelUpPoint;
                        //            sp.StartPosition = FormStartPosition.CenterParent;
                        //            sp.ShowDialog();
                        //        }
                        //    }
                        //}
                        //if (WE.AvailableThirdCharacter)
                        //{
                        //    this.TC = tempTC;
                        //    this.TC.ReplaceBackPack(tempTC.GetBackPackInfo());
                        //    TC.Exp += be.EC1.Exp;
                        //    //TC.Gold += be.EC1.Gold; // [警告]：ゴールドの所持は別クラスにするべきです。
                        //    if (TC.Exp >= TC.NextLevelBorder)
                        //    {
                        //        mainMessage.Text = "ヴェルゼ：レベルアップですね。";
                        //        using (StatusPlayer sp = new StatusPlayer())
                        //        {
                        //            // [警告]：レベルアップのMAXライフが常に２０、マナが３０で良いかどうか検討してください。
                        //            TC.BaseLife += 20;
                        //            TC.BaseMana += 30;
                        //            //TC.CurrentLife = MC.BaseLife;
                        //            TC.Exp = TC.Exp - TC.NextLevelBorder;
                        //            TC.Level += 1;
                        //            sp.WE = WE;
                        //            sp.MC = MC;
                        //            sp.SC = SC;
                        //            sp.TC = TC;
                        //            sp.CurrentStatusView = Color.Silver;
                        //            sp.LevelUp = true;
                        //            sp.UpPoint = TC.LevelUpPoint;
                        //            sp.StartPosition = FormStartPosition.CenterParent;
                        //            sp.ShowDialog();
                        //        }
                        //    }
                        //}
                        //this.WE = tempWE; // WEはHomeTownで更新します。
                        return true;
                    }
                }
            }

            return false;
        }

        private void GetPotionForLana()
        {
            string potionName = "小さい赤ポーション";

            if (!we.CompleteArea1 && !we.CompleteArea2 && !we.CompleteArea3 && !we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = "小さい赤ポーション";
            }
            else if (we.CompleteArea1 && !we.CompleteArea2 && !we.CompleteArea3 && !we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = "普通の赤ポーション"; 
            }
            else if (we.CompleteArea1 && we.CompleteArea2 && !we.CompleteArea3 && !we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = "大きな赤ポーション";
            }
            else if (we.CompleteArea1 && we.CompleteArea2 && we.CompleteArea3 && !we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = "特大赤ポーション";
            }
            else if (we.CompleteArea1 && we.CompleteArea2 && we.CompleteArea3 && we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = "豪華な赤ポーション";
            }
            else
            {
                potionName = "小さい赤ポーション";
            }
            bool result = mc.AddBackPack(new ItemBackPack(potionName));
            if (!result)
            {
                UpdateMainMessage("アイン：しまった、バックパックがいっぱいだ。何か捨てないとな・・・");
                using (StatusPlayer sp = new StatusPlayer())
                {
                    sp.MC = mc;
                    if (we.AvailableSecondCharacter)
                    {
                        sp.SC = sc;
                    }
                    if (we.AvailableThirdCharacter)
                    {
                        sp.TC = tc;
                    }
                    sp.WE = we;
                    sp.OnlySelectTrash = true;
                    sp.StartPosition = FormStartPosition.CenterParent;
                    sp.ShowDialog();
                    if (this.DialogResult == DialogResult.Cancel)
                    {
                    }
                    else
                    {
                        mc = sp.MC;
                        mc.AddBackPack(new ItemBackPack(potionName));
                    }
                }
                UpdateMainMessage("", true);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GroundOne.StopDungeonMusic();

            UpdateMainMessage("アイン：よし、練習でもするか", true);
            mainMessage.Update();
            using (RequestInput ri = new RequestInput())
            {
                ri.StartPosition = FormStartPosition.CenterParent;
                ri.InputData = "ダミー素振り君";
                ri.ShowDialog();

                string entryName = ri.InputData; // ダミー素振り君
                bool result = EncountBattle(entryName);
                if (result)
                {
                    UpdateMainMessage("アイン：俺の勝ちだな。", true);
                }
                else
                {
                    UpdateMainMessage("アイン：っち・・・負けちまったぜ。", true);
                }

            }
            GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
        }

    }
}