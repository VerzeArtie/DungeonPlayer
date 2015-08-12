using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace DungeonPlayer
{
    public partial class TruthHomeTown : MotherForm
    {
        #region "プロパティ"
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
        public bool[] Truth_KnownTileInfo { get; set; } // 後編追加
        public bool[] Truth_KnownTileInfo2 { get; set; } // 後編追加
        public bool[] Truth_KnownTileInfo3 { get; set; } // 後編追加
        public bool[] Truth_KnownTileInfo4 { get; set; } // 後編追加
        public bool[] Truth_KnownTileInfo5 { get; set; } // 後編追加
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
        #endregion

        public TruthHomeTown()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
        }

        private void TruthHomeTown_Load(object sender, EventArgs e)
        {
            //ReConstractWorldEnvironment(); // バッドエンドからの再スタートチェック

            if (we.AvailablePotionshop && !GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEvent511)
            {
                this.buttonPotion.Visible = true;
            }

            if (we.AvailableBackGate && !GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEvent511)
            {
                this.buttonShinikia.Visible = true;
            }

            if (we.AvailableDuelColosseum && !GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEvent511)
            {
                this.buttonDuel.Visible = true;
            }

            //if (we.Truth_CommunicationFirstHomeTown)
            {
                if (!we.AlreadyRest)
                {
                    ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_EVENING);
                }
                else
                {
                    ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                }
            }
            this.dayLabel.Text = we.GameDay.ToString() + "日目";
            if (we.AlreadyRest)
            {
                this.firstDay = we.GameDay - 1; // 休息したかどうかのフラグに関わらず町に訪れた最初の日を記憶します。
                if (this.firstDay <= 0) this.firstDay = 1; // [警告] 後編初日のロジック崩れによる回避手段。あまり良い直し方ではありません。
            }
            else
            {
                this.firstDay = we.GameDay; // 休息したかどうかのフラグに関わらず町に訪れた最初の日を記憶します。
            }
            this.we.SaveByDungeon = false; // 町に戻っていることを示すためのものです。
        }
        private void TruthHomeTown_Shown(object sender, EventArgs e)
        {
            GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

            ok = new OKRequest();
            ok.StartPosition = FormStartPosition.Manual;
            ok.Location = new Point(this.Location.X + 904, this.Location.Y + 708);

            #region "死亡しているものは自動的に復活させます。"
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
            #endregion

            if (!we.AlreadyShownEvent)
            {
                we.AlreadyShownEvent = true;
                // ダンジョンから帰還後、必須イベントとして優先
                #region "エンディング"
                if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd && GroundOne.WE2.SeekerEvent1103)
                {
                    ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);

                    buttonDungeon.Visible = false;
                    buttonGanz.Visible = false;
                    buttonShinikia.Visible = false;
                    buttonHanna.Visible = false;
                    buttonRana.Visible = false;
                    buttonPotion.Visible = false;
                    buttonDuel.Visible = false;
                    dayLabel.Visible = false;

                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    UpdateMainMessage("　　　＜＜＜　号外！！！　＞＞＞　");

                    UpdateMainMessage("　　　【野蛮なオル・ランディス、DUEL闘技場にて、屈辱の敗北！！】　");

                    UpdateMainMessage("　　　【DUELタイムは史上最高】　）　");

                    UpdateMainMessage("　　　【１６分４２秒】　");

                    UpdateMainMessage("　　　【決め手は、ゼータ・エクスプロージョン】");

                    UpdateMainMessage("　　　【オル・ランディス、必死にワープゲートを駆使して反撃を繰り広げていたが】");

                    UpdateMainMessage("　　　【相手のニゲイト、スタンス・オブ・サッドネッスに幾度となく阻止され】");

                    UpdateMainMessage("　　　【オル・ランディスが、タイムストップを発動する瞬間】");

                    UpdateMainMessage("　　　【相手に先手のタイムストップを発動されたのが致命的となった】");

                    UpdateMainMessage("アイン：マジか・・・師匠負けちまったのかよ。");

                    UpdateMainMessage("ラナ：珍しいわよね、あのランディスさんが負けるなんて。");

                    UpdateMainMessage("アイン：おっ、まだ続きがあるな。読んでみるか。");

                    UpdateMainMessage("　　　【なお、本DUELに関して、記者は自分の生命を賭して、オル・ランディス選手へ突撃取材を行った。】");

                    UpdateMainMessage("　　　記者：「ランディス選手！あれは敗北だったんでしょうか！？」");

                    UpdateMainMessage("　　　ランディス：「うっせぇな！」");

                    UpdateMainMessage("　　　記者：「戦略的には勝っていたかどうかだけでも、お聞かせください！」");

                    UpdateMainMessage("　　　ランディス：「うっせぇ、知るかボケ！」");

                    UpdateMainMessage("　　　記者：「体調は万全だったんでしょうか？どこか痛めていたなどは！？」");

                    UpdateMainMessage("　　　ランディス：「うっせぇ、んなんあるか、ボケが！」");

                    UpdateMainMessage("　　　記者：「最後に、ＤＵＥＬ闘技場の覇者として、今回の負けに関して一言お願いします！！！」");

                    UpdateMainMessage("　　　ランディス：「・・・　・・・　・・・」");

                    UpdateMainMessage("　　　ランディス：「小細工は一切無し、体調も互いに万全、試合前の不正取引一切無し」");

                    UpdateMainMessage("　　　ランディス：「俺の負けだ」");

                    UpdateMainMessage("　　　ランディス：「・・・　・・・　・・・」");

                    UpdateMainMessage("　　　記者：「・・・　・・・　・・・」");

                    UpdateMainMessage("　　　ランディス：「ドケやクソボケ記者がぁ！！食い殺すぞッラァ！！」");

                    UpdateMainMessage("　　　記者：「ッヒ、ヒエエェェ！！！わたたた、たりがとうございました！！！」");

                    UpdateMainMessage("アイン：・・・こぇ・・・よく聞きにいったなこの記者・・・");

                    UpdateMainMessage("ラナ：この映像、ものすごい勢いで逃げてるわね、記者さん・・・");

                    UpdateMainMessage("アイン：ああ・・・多分捕まったらマジで病院送りだったろうな・・・");

                    UpdateMainMessage("ラナ：あ、見てみて最後のコメント欄があるわよ、ホラ♪");

                    UpdateMainMessage("アイン：ほんとだ、どれどれ・・・");

                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("　　　【なお、記者は猛ダッシュで逃げる際、オル・ランディス選手の最後に振り返る時の顔を目撃した】");

                    UpdateMainMessage("　　　【その時の、オル・ランディス選手の顔は】");

                    UpdateMainMessage("　　　【どことなく、優しく笑っているように見えた】");

                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    UpdateMainMessage("ラナ：笑っているように・・・");

                    UpdateMainMessage("アイン：殺されかけた直後だろ？　悪意ある笑顔と勘違いしたんじゃないのか？");

                    UpdateMainMessage("ラナ：ッフフ、そうなのかもね。");

                    UpdateMainMessage("アイン：っさてと、今年もファージル宮殿生誕祭への招待状が届いてたっけ。");

                    UpdateMainMessage("ラナ：ウソ、ちゃんと覚えてたわけ？");

                    UpdateMainMessage("アイン：ああ、もちろん覚えてるさ。ちゃんと招待状もココに・・・");

                    UpdateMainMessage("アイン：・・・　・・・　・・・");

                    UpdateMainMessage("アイン：ッグ・・・");

                    UpdateMainMessage("ラナ：（ジィ～）");

                    UpdateMainMessage("アイン：いやいやいや、宿屋のバックパック管理倉庫に入れたんだった、ッハッハッハ！");

                    UpdateMainMessage("ラナ：へ～、じゃあ後でちゃんと見に行きましょ♪");

                    UpdateMainMessage("アイン：うっ・・・ッハッハハ・・・");

                    UpdateMainMessage("ラナ：ねえ、ところでアイン。");

                    UpdateMainMessage("アイン：ん？　なんだ？");

                    UpdateMainMessage("ラナ：私達ってダンジョンの最後でどうなったのか、覚えてる？");

                    UpdateMainMessage("アイン：ダンジョンの最後？");

                    UpdateMainMessage("アイン：・・・　・・・　・・・");

                    UpdateMainMessage("アイン：何言ってんだよ。");

                    UpdateMainMessage("アイン：制覇したに決まってるじゃないか！ッハッハッハ！");

                    UpdateMainMessage("ラナ：ちょっと、まともに答えなさいよね。");

                    UpdateMainMessage("ラナ：少なくとも私は覚えてないわよ、最後の方は。");

                    UpdateMainMessage("ラナ：最後はどんな感じだったの？教えてよ。");

                    UpdateMainMessage("アイン：そりゃお前、最下層と言えば・・・");

                    UpdateMainMessage("アイン：デカイ竜がドカーンと構えててさ。");

                    UpdateMainMessage("アイン：それを俺がズバズバっと撃破したのさ！");

                    UpdateMainMessage("アイン：ッハッハッハ！");

                    UpdateMainMessage("ラナ：・・・ふーん・・・");

                    UpdateMainMessage("ラナ：全然答えになってないわね。");

                    UpdateMainMessage("アイン：（ッギク）");

                    UpdateMainMessage("ラナ：私の事助けてくれたのはどの辺りだったのよ？");

                    UpdateMainMessage("ラナ：黒いもやもやした煙に囲まれて、真っ暗になって気絶した瞬間は覚えてるんだけど・・・");

                    UpdateMainMessage("ラナ：私、その後どうなったのかが全然思い出せないのよ。スッポ抜けた感じで。");

                    UpdateMainMessage("ラナ：気が付いたら、ダンジョンの外で介抱されてたのよね。");

                    UpdateMainMessage("ラナ：こんなの納得が行かないわ。ちゃんと教えてちょうだい。");

                    UpdateMainMessage("アイン：そ、それはだなあ・・・");

                    UpdateMainMessage("アイン：４階でお前が倒れてたんだよ。");

                    UpdateMainMessage("アイン：でだ、それを発見した俺は慌ててお前を抱き起こして・・・");

                    UpdateMainMessage("『ッドスウウゥゥン！！！』（ラナのファイナリティ・キックがアインに炸裂）　　");

                    UpdateMainMessage("ラナ：「抱き起こして」とかいう表現は止めてちょうだい。");

                    UpdateMainMessage("アイン：グフォオォ・・・ど、どう言えば良いんだよ・・・ったく・・・");

                    UpdateMainMessage("アイン：慌ててお前をそっと起こしてだな・・・");

                    UpdateMainMessage("アイン：（ハラハラ・・・）");

                    UpdateMainMessage("ラナ：その次は？");

                    UpdateMainMessage("アイン：で、後はそのままダンジョンから帰還したって所さ。");

                    UpdateMainMessage("ラナ：・・・　・・・　・・・");

                    UpdateMainMessage("ラナ：そんな普通の内容だったら、私だって覚えてると思うんだけど");

                    UpdateMainMessage("ラナ：ねえ、その時に何かあったんじゃないの？どうだったのよ？");

                    UpdateMainMessage("アイン：ま・・・");

                    UpdateMainMessage("アイン：ままま、良いじゃねえかそんな所は！");

                    UpdateMainMessage("アイン：ダンジョン制覇！無事、完結！！！");

                    UpdateMainMessage("アイン：ハーッハッハッハ！！");

                    UpdateMainMessage("ラナ：絶対何か隠してるわよね・・・");

                    UpdateMainMessage("アイン：まあ、ラナ、あれだ。");

                    UpdateMainMessage("アイン：良かったよ、お前と一緒に無事にここまで戻れて・・・本当に。");

                    UpdateMainMessage("ラナ：えっ・・・とっとと・・");

                    UpdateMainMessage("ラナ：ちょっと、何しんみりしちゃってるのよ、こんな所でもう。");

                    UpdateMainMessage("アイン：・・・悪い悪い、なんとなくな・・・");

                    UpdateMainMessage("アイン：おっ、そういえばラナ、頼み事があるんだが");

                    UpdateMainMessage("ラナ：なに？");

                    UpdateMainMessage("アイン：明日さ。ファージル宮殿の生誕祭２１年があるだろ？");

                    UpdateMainMessage("ラナ：ええ。");

                    UpdateMainMessage("アイン：その時に・・・必ず、寄っておきたい場所があるんだ。");

                    UpdateMainMessage("アイン：一緒に、来てくれるか？");

                    UpdateMainMessage("ラナ：べつに良いけど、どこに行くつもりなのよ？");

                    UpdateMainMessage("アイン：大丈夫、そんな遠い所へ行くわけじゃねえんだ。");

                    UpdateMainMessage("アイン：頼む。");

                    UpdateMainMessage("ラナ：うん、良いわよ。");

                    UpdateMainMessage("アイン：サンキュー。");

                    UpdateMainMessage("ラナ：じゃあ、明日またね♪");

                    UpdateMainMessage("アイン：おお、また明日な。");

                    UpdateMainMessage("ラナ：・・・　・・・　・・・");

                    UpdateMainMessage("ラナ：寝坊してたら、叩き殺すからね。");

                    UpdateMainMessage("アイン：・・・いやいや、殺さないようにしてくれ。");

                    UpdateMainMessage("ラナ：じゃあね。");

                    UpdateMainMessage("アイン：ああ。");

                    ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "次の日の朝・・・";
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：ゴフォオォォォ・・・ッゴホッゴホ・・・");

                    UpdateMainMessage("ラナ：１分遅れてたわよ。さ、起きなさい。");

                    UpdateMainMessage("アイン：イッツツツ・・・容赦ねえな・・・");

                    UpdateMainMessage("ラナ：ファージル宮殿前は朝一で行っても混雑するんだから、早めに行きましょうよ♪");

                    UpdateMainMessage("アイン：ああ、分かってるって。");

                    UpdateMainMessage("アイン：じゃあ、出発だ！！");

                    GroundOne.StopDungeonMusic();
                    GroundOne.PlayDungeonMusic(Database.BGM13, Database.BGM13LoopBegin);
                    ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_FAZIL_CASTLE);
                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "---- ファージル宮殿にて ----";
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：っうおぉぉ！　すげえ人の数だな！！");

                    UpdateMainMessage("ラナ：生誕祭だもの、当たり前じゃない♪");

                    UpdateMainMessage("アイン：エルミ国王もよくこれだけの人を毎年毎年、宮殿に招き入れるよな・・・");

                    UpdateMainMessage("アイン：宮殿がそろそろパンクするんじゃねえのか？");

                    UpdateMainMessage("ラナ：宮殿外のガーデン広場もあるんだし、大丈夫らしいわよ。");

                    UpdateMainMessage("アイン：ッハハ・・・その辺はさすがと行った所だな。");

                    UpdateMainMessage("アイン：さてと、じゃあ列に並んでエルミ国王とファラ王妃にご対面と行きますか！");

                    UpdateMainMessage("ラナ：うん♪");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "---- ２時間半経過後 ----";
                        md.ShowDialog();
                    }
                    UpdateMainMessage("アイン：・・・　もう少しか　・・・");

                    UpdateMainMessage("ラナ：うん、もうすぐのはずよ。");

                    UpdateMainMessage("ラナ：あっ、ホラ次が私達よ♪");

                    UpdateMainMessage("アイン：本当だ、もう少しだな・・・");

                    UpdateMainMessage("アイン：なあ、所でラナ。");

                    UpdateMainMessage("ラナ：何よ？");

                    UpdateMainMessage("アイン：宮殿内、どこら辺を見てみたいんだ？");

                    UpdateMainMessage("アイン：行きたい所を言ってくれ。優先させるからさ。");

                    UpdateMainMessage("ラナ：・・・え・・・");

                    UpdateMainMessage("ラナ：ええええええぇぇぇーーーー！！？？");

                    UpdateMainMessage("アイン：わーー、いきなりうるっさい声出すなって、ったく。");

                    UpdateMainMessage("ラナ：何ひょっとして、気遣ったとでも言うわけ？今のは？？");

                    UpdateMainMessage("アイン：・・・　・・・　・・・");

                    UpdateMainMessage("アイン：ああ、そうだよ。悪いかよ、まったく・・・");

                    UpdateMainMessage("ラナ：・・・　・・・　・・・");

                    UpdateMainMessage("ラナ：信じられないわね、バカアインがそういう事言うの。");

                    UpdateMainMessage("アイン：良いからほら、どこに行ってみるんだ？");

                    UpdateMainMessage("ラナ：んーじゃあ・・・");

                    UpdateMainMessage("ラナ：ファラ王妃様の謁見が終わってから言うわね♪");

                    UpdateMainMessage("アイン：ああ、了解。");

                    UpdateMainMessage("アイン：おっ、ようやく前のヤツが終わったみたいだぜ！");

                    UpdateMainMessage("ラナ：じゃあ、進めましょ♪");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "---- 国王／王妃　謁見の間にて ----";
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：っとと・・・ここで整列だよな・・・");

                    UpdateMainMessage("エルミ：よく来たね。アイン君とラナさん。");

                    UpdateMainMessage("ラナ：エルミ様、ファラ様、生誕祭おめでとうございます。");

                    UpdateMainMessage("ファラ：アインさんもラナさんも、リラックスしてくださいね（＾＾");

                    UpdateMainMessage("アイン：あっ！ありがとうございます！！");

                    UpdateMainMessage("ファラ：（ちょっと、あんまり形式を崩さないでよね）");

                    UpdateMainMessage("アイン：（いいじゃねえか・・・ああ言ってるんだしさ）");

                    UpdateMainMessage("エルミ：いいですよ、いつものアイン君らしく喋ってください。");

                    UpdateMainMessage("アイン：ハイ、どうもっす！！");

                    UpdateMainMessage("ラナ：えっと、そこのバカはちょっと置いといて・・・");

                    UpdateMainMessage("ラナ：エルミ様、本日折り入っての頼みがあってこの謁見の間に参りました。");

                    UpdateMainMessage("エルミ：どうしたんだい？");

                    UpdateMainMessage("ラナ：最近ファージル領域へ武力による接触を計る" + Database.VINSGALDE + "王国ですが");

                    UpdateMainMessage("ラナ：" + Database.VINSGALDE + "は元々私の母が育った国");

                    UpdateMainMessage("ラナ：母からは穀物が育ちにくいエリアが多い国だと聞かされておりました。");

                    UpdateMainMessage("ラナ：そこでお願いがあります。");

                    UpdateMainMessage("ラナ：今から２年の間、私とそこのバカアインに対して");

                    UpdateMainMessage("ラナ：国外遠征許可証を発行していただけないでしょうか？");

                    UpdateMainMessage("アイン：えっ、俺も！？");

                    UpdateMainMessage("エルミ：なるほど、" + Database.VINSGALDE + "に行って穀物以外の生産を教えてくるつもりなんだね。");

                    UpdateMainMessage("エルミ：しかし国外遠征許可証を要請してくるとは・・・");

                    UpdateMainMessage("エルミ：どうしようか？ファラ。");

                    UpdateMainMessage("ファラ：エルミったら、ひどい男（＾＾");

                    UpdateMainMessage("エルミ：っつ・・・どうしてお前は・・・");

                    UpdateMainMessage("ファラ：何よ臆病者、ちゃんと貴方が考えてきた事を言いなさいよね（＾＾");

                    UpdateMainMessage("エルミ：わかってるって、ああやはりお前は少し引っ込んでなさい。");

                    UpdateMainMessage("ファラ：・・・（＾＾＃");

                    UpdateMainMessage("エルミ：ッゴホン、では。えー・・・");

                    UpdateMainMessage("ファラ：ラナさん、国外遠征許可証は既に用意してあります（＾＾");

                    UpdateMainMessage("ラナ：えっ！？　本当ですか！？");

                    UpdateMainMessage("ファラ：アインさんの分もありますよ（＾＾");

                    UpdateMainMessage("アイン：マジかよ！！いつのまにそんなモノが！？");

                    UpdateMainMessage("ファラ：はい、エルミ解説（＾＾");

                    UpdateMainMessage("エルミ：ッ・・・ック・・・");

                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("エルミ：実はね、２か月前あたりから考えてたんだよ、" + Database.VINSGALDE + "の件は。");

                    UpdateMainMessage("エルミ：適任役は誰かなと考えた所");

                    UpdateMainMessage("エルミ：君達２人にこの生誕祭が終わった後、正式に依頼しに行こうと思ってたんだよ。");

                    UpdateMainMessage("（ラナ：えっ！？）　　（アイン：えっ！！）");

                    UpdateMainMessage("エルミ：あのダンジョンを突破してきた君達だ。");

                    UpdateMainMessage("エルミ：きみ達なら、きっとこの件を解決してくれる。そう信じたんだ。");

                    UpdateMainMessage("エルミ：というわけで・・・ファラ");

                    UpdateMainMessage("ファラ：はい、何でしょう？（＾＾");

                    UpdateMainMessage("エルミ：順番が逆になってしまったが・・・例のモノをここへ");

                    UpdateMainMessage("ファラ：フフフ、もう後ろの手に隠し持ってましたわ、ジャーン（＾＾");

                    UpdateMainMessage("エルミ：だあぁ、お前はもっと王妃らしくしなさい。本当に・・・");

                    UpdateMainMessage("ファラ：このぐらい良いじゃない、ッネ、アインさん（＾＾");

                    UpdateMainMessage("アイン：タ・・・タハハ・・・");

                    UpdateMainMessage("アイン：相変わらず、先回りされてしまいますね。凄いですよ、国王も王妃も。");

                    UpdateMainMessage("ラナ：本当、驚かされるわ。いくらなんでもここまで私の行動が読み切られるなんて・・・");

                    UpdateMainMessage("アイン：あ、ちょっと質問。ラナの生真面目さと正義行動は何となく読めるとしても");

                    UpdateMainMessage("アイン：何で俺の分まで？？");

                    UpdateMainMessage("ファラ：・・・ッキャ、野暮だわ（＾＾");

                    UpdateMainMessage("アイン：・・・ハイ？？");

                    UpdateMainMessage("エルミ：こら、ファラ。話をややこしくしないように。");

                    UpdateMainMessage("エルミ：アインくん、君に遠征してもらうのは、別の意図があるんだ。");

                    UpdateMainMessage("アイン：は、はい。");

                    UpdateMainMessage("エルミ：アインくん・・・実はね・・・");

                    GroundOne.PlayDungeonMusic(Database.BGM15, Database.BGM15LoopBegin);

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "---- ファージル宮殿、園芸の広場にて ----";
                        md.ShowDialog();
                    }

                    UpdateMainMessage("ラナ：あっ、ホラホラ見てアイン。これアカシジアの木よ♪");

                    UpdateMainMessage("アイン：へえ・・・こんな形してんだな。");

                    UpdateMainMessage("アイン：おっ、こっちのも奇妙な形をしてるぞ。ラナわかるか？");

                    UpdateMainMessage("ラナ：それはヒメリギツユクサよ。成熟するまでは、渦巻き状に葉を広げるの。");

                    UpdateMainMessage("アイン：へえ・・・へええぇぇ・・・");

                    UpdateMainMessage("ラナ：アインって本当に知らないのね、こういう事は。");

                    UpdateMainMessage("アイン：ああ、まったく知らない。");

                    UpdateMainMessage("ラナ：こっちの、ゴリュウモクレンは？");

                    UpdateMainMessage("アイン：見たことも聞いた事もないな。");

                    UpdateMainMessage("ラナ：この、マラ・ハクジュは？");

                    UpdateMainMessage("アイン：どっかの本でかろうじて・・・");

                    UpdateMainMessage("ラナ：えっと、ファージル宮殿で今年新作の薬草よ・・・本にまだ載ってないわよ・・・");

                    UpdateMainMessage("アイン：ッゲ、まじかよ？");

                    UpdateMainMessage("ラナ：フ・・・ッフフフ♪　あーオカシイー♪");

                    UpdateMainMessage("アイン：くそう、今に見てろ・・・全部暗記してやるからな。");

                    UpdateMainMessage("ラナ：バカアインには一生無理な課題じゃないかしら♪");

                    UpdateMainMessage("アイン：なあラナ。お前、やっぱり園芸とか好きなんだな？");

                    UpdateMainMessage("ラナ：うん、そうね。");

                    UpdateMainMessage("アイン：そうやって、薬草とか見て笑ってるお前って・・・");

                    UpdateMainMessage("ラナ：うん？");

                    UpdateMainMessage("アイン：・・・　・・・　・・・");

                    UpdateMainMessage("アイン：おっ！　あっちにも面白い形の木があるぞ、行ってみようぜ！");

                    UpdateMainMessage("ラナ：えーーー、何よ今の。");

                    UpdateMainMessage("アイン：行ってみようぜ！　っな！");

                    UpdateMainMessage("ラナ：・・・ハイハイ、分かりました♪");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "---- ファージル宮殿、食事の間にて ----";
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：マジかよ・・・この美味さ・・・");

                    UpdateMainMessage("アイン：俺は幸せ者だ！　まだまだ食わせてもらうぜ！！");

                    UpdateMainMessage("ラナ：すみません、カールハンツ爵。このバカが・・・");

                    UpdateMainMessage("カール：よい、気の済むまで食されよ。");

                    UpdateMainMessage("カール：それよりも、貴女は数日後、" + Database.VINSGALDE + "へ向かうとの事。");

                    UpdateMainMessage("カール：準備を怠るでないぞ、よいな。");

                    UpdateMainMessage("ラナ：ハイ、ありがとうございます。");

                    UpdateMainMessage("カール：それと貴君。");

                    UpdateMainMessage("アイン：ッモゴ・・・ハイ！");

                    UpdateMainMessage("カール：ランディスとはここで会ったかね。");

                    UpdateMainMessage("アイン：いえ、まだですが・・・");

                    UpdateMainMessage("アイン：えっ、やっぱりどこかに潜んでるんですか？生誕祭だし・・・");

                    UpdateMainMessage("カール：当然の事。");

                    UpdateMainMessage("アイン：トホホ・・・殴られたくないんだが・・・");

                    UpdateMainMessage("カール：ッフ、さすがに生誕祭のど真ん中でバトルを仕掛ける事はなかろう。");

                    UpdateMainMessage("カール：ランディスは貴君に何か話があるようだったぞ。");

                    UpdateMainMessage("カール：この食事が終わったら、会ってみるがよい。");

                    UpdateMainMessage("アイン：了解です。しかし、この宮殿広いしな・・・どこに行けば・・・");

                    UpdateMainMessage("　　【【【　その瞬間。　アインは遥か遠くから伝わってくる殺気を感じ取った。　】】】");

                    UpdateMainMessage("アイン：ハッ！　まさかこの悪寒と恐怖！！");

                    UpdateMainMessage("カール：この喧騒の中で互いに感知出来るとは、貴君も相当腕が上がったようだな。");

                    UpdateMainMessage("アイン：いや・・・これは単にボケ師匠の殺気が威圧的すぎるだけです・・・");

                    UpdateMainMessage("ラナ：でも、不思議よね。アインが自分から察知しようとしたら、感知できたんでしょ？");

                    UpdateMainMessage("アイン：ああ、まあ・・・・");

                    UpdateMainMessage("ラナ：話が一区切りするまでランディスさんは待っててくれた。って事じゃないかしら♪");

                    UpdateMainMessage("アイン：いや、絶対にありえないから。あの師匠に限ってないない。");

                    UpdateMainMessage("ラナ：ッフフ、そんな事言ってると・・・知らないわよー♪");

                    UpdateMainMessage("アイン：え？？？");

                    UpdateMainMessage("　　【【【　ヴオオォォォン！！！　】】】");

                    UpdateMainMessage("アイン：っおわあぁぁぁ！！！");

                    UpdateMainMessage("アイン：ちょちょちょ、止めてくれよボケ師匠・・・いつの間に後方に忍び寄ったんだ、ったく・・・");

                    UpdateMainMessage("ランディス：生誕祭のど真ん中でバトルを仕掛ける事がねぇと思ってんじゃねえぞ、ボケが。");

                    UpdateMainMessage("アイン：いやいやいや・・・");

                    UpdateMainMessage("ランディス：ダンジョン、どうだった。");

                    UpdateMainMessage("アイン：・・・まあ・・・");

                    UpdateMainMessage("アイン：予想を超えてた。");

                    UpdateMainMessage("アイン：自分の身の程が分かった、そんな気がした。");

                    UpdateMainMessage("アイン：俺、もうちょっといろいろ回ってみようと思うんだ。");

                    UpdateMainMessage("ランディス：ほぉ");

                    UpdateMainMessage("アイン：知らない事が多すぎる。");

                    UpdateMainMessage("アイン：カール先生にも教えてもらいたい事が山ほどあるんだが。");

                    UpdateMainMessage("アイン：教えてもらう以前に、自分の手足を使って、まずいろいろ拾ってみたいんだ。");

                    UpdateMainMessage("アイン：じゃないといつまでたっても、師匠やカール先生には勝てる気がしない。");

                    UpdateMainMessage("ランディス：ちったぁ、成長したって所か。");

                    UpdateMainMessage("ランディス：" + Database.VINSGALDE + "遠征、頑張ってこいや。");

                    UpdateMainMessage("アイン：げっ、なんでもう知ってるんだ・・・");

                    UpdateMainMessage("ランディス：俺様は国王直属だぞ、てめぇに話が行く前から聞かされてんだよ。");

                    UpdateMainMessage("アイン：ああ・・・そういや直属だったっけ・・・");

                    UpdateMainMessage("アイン：・・・　・・・　・・・");

                    UpdateMainMessage("アイン：なあ師匠");

                    UpdateMainMessage("アイン：俺、" + Database.VINSGALDE + "に行って、今度こそ。");

                    UpdateMainMessage("アイン：腕を磨いて磨いて、師匠に勝ってみせる！");

                    UpdateMainMessage("ランディス：・・・　ケッ・・・");

                    UpdateMainMessage("ランディス：せいぜい頑張れや、ッカッカッカ。");

                    UpdateMainMessage("アイン：笑ってられんのも今のうちだ、絶対だからな。");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "---- ファージル宮殿、休息の間にて ----";
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：いやあ、しかし宮殿中を歩き回ったな。");

                    UpdateMainMessage("ラナ：まだ一周出来てないわよね。本当凄いわね、エルミ様とファラ様は。");

                    UpdateMainMessage("アイン：ああ、こんだけの広さ。２人自らで設計したらしいからな。");

                    UpdateMainMessage("ラナ：・・・ねえアイン");

                    UpdateMainMessage("アイン：ん？");

                    UpdateMainMessage("ラナ：アンタどこか一緒に来て欲しい所があるとか言ってなかったっけ？");

                    UpdateMainMessage("アイン：あ、ああ、そうだそうだ。忘れる所だった。");

                    UpdateMainMessage("ラナ：ちょっと・・・そんな忘れるような内容なの？");

                    UpdateMainMessage("アイン：い、いやいや悪い・・・");

                    UpdateMainMessage("アイン：忘れてはいないさ。");

                    UpdateMainMessage("アイン：ただ、ちょっとな。");

                    UpdateMainMessage("ラナ：ん～、ハッキリしないわね。結局そこには行くの？行かないの？");

                    UpdateMainMessage("アイン：・・・　まあ　・・・");

                    UpdateMainMessage("ランディス：行ってやれ");

                    UpdateMainMessage("アイン：師匠、いつのまに・・・");

                    UpdateMainMessage("ランディス：てめぇが今回宮殿に来た一番の目的はそれだろ。");

                    UpdateMainMessage("ラナ：えっ、そうなの？アイン");

                    UpdateMainMessage("アイン：・・・ああ");

                    UpdateMainMessage("アイン：いや、特に変な内容ってわけじゃないんだ。");

                    UpdateMainMessage("アイン：ただ俺がそこに行くってのは変かなって思ってる面も・・・");

                    UpdateMainMessage("ランディス：変でもなんでもねえ、行ってやれ。");

                    UpdateMainMessage("ランディス：何だったら俺も一緒に行ってやる。挨拶がまだだしな。");

                    UpdateMainMessage("アイン：そ、そか。ハハハ・・・助かる。");

                    UpdateMainMessage("ラナ：どこに行くの？");

                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("アイン：・・・　・・・　・・・");

                    UpdateMainMessage("アイン：エレマ・セフィーネさんの墓場だ。");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "---- ファージル宮殿、墓地にて ----";
                        md.ShowDialog();
                    }

                    UpdateMainMessage("ラナ：こんな所があったのね・・・知らなかったわ。");

                    UpdateMainMessage("ランディス：隠されるよう設計されてんだ。小娘には見つけられなくて当然だ。");

                    UpdateMainMessage("ラナ：そうなんですか・・・");

                    UpdateMainMessage("アイン：確かエレマさんの墓標は・・・あの辺りだったかな・・・");

                    UpdateMainMessage("アイン：・・・あっ！");

                    UpdateMainMessage("アイン：誰か・・・いる・・・");
                    mainMessage.Visible = false;

                    for (int ii = 0; ii < 10; ii++)
                    {
                        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_FAZIL_CASTLE, (ii + 1) * 0.1f);
                        System.Threading.Thread.Sleep(400);
                        Application.DoEvents();
                    }

                    labelEnding.Visible = false;
                    labelEnding2.Visible = false;
                    this.BackColor = Color.White;
                    UpdateEndingMessage2("");
                    GroundOne.WE2.SeekerEndingRoll = true;

                    GroundOne.PlayDungeonMusic(Database.BGM09, Database.BGM09LoopBegin);
                    
                    UpdateEndingMessage2("Dungeon Player\r\n ～ The Liberty Seeker ～");

                    UpdateEndingMessage2("ストーリー　　【　湯淺　與範　】\r\n　　　　　　　【　辻谷　友紀　】");

                    UpdateEndingMessage2("音楽　　【　湯淺　晋太郎　】");

                    UpdateEndingMessage2("バトルシステム　　【　湯淺　與範　】");

                    UpdateEndingMessage2("マップ制作　【　湯淺　與範　】");

                    UpdateEndingMessage2("モンスター制作　　【　辻谷　友紀　】\r\n　　　　　　　　　【　湯淺　與範　】");

                    UpdateEndingMessage2("魔法／スキル　　　【　湯淺　與範　】");

                    UpdateEndingMessage2("サウンドエフェクト　【　湯淺　與範　】");

                    UpdateEndingMessage2("アイテム制作　　【　石高　裕介　】\r\n　　　　　　　　【　湯淺　與範　】");

                    UpdateEndingMessage2("グラフィック　　　【　辻谷　友紀　】");

                    UpdateEndingMessage2("プログラマー　【　湯淺　與範　】");

                    UpdateEndingMessage2("スペシャルサンクス　　【　KANAKO　】");

                    UpdateEndingMessage2("プロデューサー　　【　湯淺　與範　】");


                    UpdateEndingMessage("　ラナ：あれ・・・ヴェルゼさんじゃない？");

                    UpdateEndingMessage("　アイン：ああ・・・");

                    UpdateEndingMessage("　アイン：なぜ、こんな所に・・・");

                    UpdateEndingMessage("　ランディス：・・・");

                    UpdateEndingMessage("　アイン：（　ダンジョンから出る最後　）");

                    UpdateEndingMessage("　アイン：（　あの真っ白な空間の中で、支配竜が最後、俺に告げたこと　）");

                    UpdateEndingMessage("　アイン：（　唯一の例外として　）");

                    UpdateEndingMessage("　アイン：（　ヴェルゼ・アーティを現世に戻してくれると伝えてくれた　）");

                    UpdateEndingMessage("　アイン：（　ラナが助かったと分かった直後　）");

                    UpdateEndingMessage("　アイン：（　俺の勝手なわがままだったかもしれないが　）");

                    UpdateEndingMessage("　アイン：（　俺はあの真っ白な空間に引き込まれる中で必至に願った　）");

                    UpdateEndingMessage("　アイン：（　それを支配竜は例外的に聞き入れてくれたようだ　）");

                    UpdateEndingMessage("　アイン：（　ただし、絶対の条件があった　）");

                    UpdateEndingMessage("　アイン：（　生命としての活動を復活させるだけでしか原理的には行えない　）");

                    UpdateEndingMessage("　アイン：（　復活したとしても、本人の今までの記憶は一切保持されない　）");

                    UpdateEndingMessage("　アイン：（　記憶は完全に消去される　）");

                    UpdateEndingMessage("　アイン：（　最低限の生命活動を行うコア部分のみが魂として吹き込まれる）");

                    UpdateEndingMessage("　アイン：（　この事象は支配竜を通じて俺にだけ伝えられている　）");

                    UpdateEndingMessage("　アイン：・・・　・・・　・・・");

                    UpdateEndingMessage("　アイン：師匠やっぱり・・・ヴェルゼは記憶喪失なのか？");

                    UpdateEndingMessage("　ランディス：・・・");

                    UpdateEndingMessage("　ランディス：通常の会話でも大体分かるんだが");

                    UpdateEndingMessage("　ランディス：エルミの事やファラ、カールの事、もちろん俺も含めて");

                    UpdateEndingMessage("　ランディス：アーティの野郎は、完全に覚えてねぇ。");

                    UpdateEndingMessage("　ランディス：曖昧な雑談の中では読み切れない面もあるが");

                    UpdateEndingMessage("　ランディス：あのDUEL闘技場で、俺が負けた時の試合");

                    UpdateEndingMessage("　ランディス：あの時の戦闘の動きで、きっちり実感させてもらった。");

                    UpdateEndingMessage("　ランディス：今のアーティは完全に記憶喪失だ。");

                    UpdateEndingMessage("　ラナ：エレマさんとの記憶も・・・全部残ってないのよね・・・");

                    UpdateEndingMessage("　ランディス：だろうな。");

                    UpdateEndingMessage("　ラナ：そんな・・・じゃあ、どうして・・・");

                    UpdateEndingMessage("　アイン：・・・　・・・　・・・");

                    UpdateEndingMessage("　アイン：（　記憶が一切消去された状態で　）");

                    UpdateEndingMessage("　アイン：（　ヴェルゼは・・・無言で佇んでいる・・・　）");

                    UpdateEndingMessage("　アイン：（　最愛の人が眠る墓所の前で・・・　）");

                    UpdateEndingMessage("　アイン：（　長い事・・・ずっと・・・）");

                    UpdateEndingMessage("　ラナ：ねえ、見てあれ。");

                    UpdateEndingMessage("　アイン：ん？");

                    UpdateEndingMessage("　ラナ：ヴェルゼさんの足元、花が咲いてるわ。");

                    UpdateEndingMessage("　アイン：・・・ホントだ・・・何ていう花なんだ？");

                    UpdateEndingMessage("　ラナ：アルヴィアナの花");

                    UpdateEndingMessage("　アイン：アルヴィアナの・・・花？");

                    UpdateEndingMessage("　ラナ：この時期には、滅多に咲かない花よ。");

                    UpdateEndingMessage("　アイン：そうなんだ・・・");

                    UpdateEndingMessage("　アイン：花言葉とか、あったりするのか？");

                    UpdateEndingMessage("　ラナ：うん、花言葉はね・・・");

                    UpdateEndingMessage("");

                    UpdateEndingMessage("");

                    for (int ii = 0; ii < 3000; ii++)
                    {
                        point = new PointF(point.X, point.Y - 1);
                        this.Invalidate();

                        int sleep = 70;
                        if (ii > 2000) { sleep = 50; }
                        if (ii > 2500) { sleep = 30; }
                        if (ii > 2700) { sleep = 15; }
                        System.Threading.Thread.Sleep(sleep);
                        Application.DoEvents();
                    }

                    this.endingText3.Add("＜　奇跡の再会　＞　って言うのよ。"); 
                    for (int ii = 0; ii < 800; ii++)
                    {
                        this.Invalidate();
                        System.Threading.Thread.Sleep(20);
                        Application.DoEvents();
                    }

                    GroundOne.StopDungeonMusic();
                    GroundOne.WE2.SeekerEnd = true;
                    this.we.TruthCompleteArea5 = true;
                    this.we.TruthCompleteArea5Day = this.we.GameDay;
                    using (SaveLoad sl = new SaveLoad())
                    {
                        sl.MC = this.MC;
                        sl.SC = this.SC;
                        sl.TC = this.TC;
                        sl.WE = this.WE;
                        sl.Truth_KnownTileInfo = this.Truth_KnownTileInfo; // 後編追加
                        sl.Truth_KnownTileInfo2 = this.Truth_KnownTileInfo2; // 後編追加
                        sl.Truth_KnownTileInfo3 = this.Truth_KnownTileInfo3; // 後編追加
                        sl.Truth_KnownTileInfo4 = this.Truth_KnownTileInfo4; // 後編追加
                        sl.Truth_KnownTileInfo5 = this.Truth_KnownTileInfo5; // 後編追加
                        sl.SaveMode = true;
                        sl.StartPosition = FormStartPosition.CenterParent;
                        sl.ShowDialog();
                        sl.RealWorldSave();
                    }

                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    return;
                }
                #endregion
                #region "現実世界"
                else if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd && GroundOne.WE2.SeekerEvent511 && !GroundOne.WE2.SeekerEvent601)
                {
                    GroundOne.StopDungeonMusic();

                    dayLabel.Visible = false;
                    buttonHanna.Visible = false;
                    buttonDungeon.Visible = false;
                    buttonRana.Visible = false;
                    buttonGanz.Visible = false;
                    this.backgroundData = null;
                    this.BackColor = Color.Black;

                    UpdateMainMessage("アイン：っ・・・いつつ・・・");

                    UpdateMainMessage("アイン：今・・・何時だ？");

                    UpdateMainMessage("        『アインは宿屋の寝床から起き上がった。』");

                    UpdateMainMessage("アイン：朝の６時か・・・起きるには少し早いぐらいだな。");

                    UpdateMainMessage("アイン：・・・ん？何か床に落ちてるな。");
                    
                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = "【ラナのイヤリング】を手に入れました。";
                        GroundOne.playbackMessage.Insert(0, md.Message);
                        GroundOne.playbackInfoStyle.Insert(0, TruthPlaybackMessage.infoStyle.notify);
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    GetItemFullCheck(mc, Database.RARE_EARRING_OF_LANA);

                    UpdateMainMessage("アイン：ラナのイヤリングじゃねえか・・・何でこんな物が・・・");
                    
                    UpdateMainMessage("アイン：・・・　何であるんだっけ　・・・　ラナが落としたのか？");
                    
                    UpdateMainMessage("アイン：いいや、そんなワケ無えよな・・・じゃあ何でだ・・・");
                    
                    UpdateMainMessage("アイン：まあいいか。とりあえず、目覚めたわけだし、町にでも出てみるとするか。");
                    
                    ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                    this.BackColor = Color.WhiteSmoke;
                    buttonHanna.Visible = true;
                    buttonDungeon.Visible = true;
                    buttonRana.Visible = true;
                    buttonGanz.Visible = true;
                    dayLabel.Visible = true;
                    
                    UpdateMainMessage("アイン：さて、何すっかな。", true);

                    we.AlreadyRest = true; // 朝起きたときからスタートとする。

                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    GroundOne.WE2.SeekerEvent601 = true;
                    Method.AutoSaveTruthWorldEnvironment();
                    Method.AutoSaveRealWorld(this.MC, this.SC, this.TC, this.WE, null, null, null, null, null, this.Truth_KnownTileInfo, this.Truth_KnownTileInfo2, this.Truth_KnownTileInfo3, this.Truth_KnownTileInfo4, this.Truth_KnownTileInfo5);
                }
                #endregion
                #region "３階制覇"
                else if (this.we.TruthCompleteArea3 && !this.we.TruthCommunicationCompArea3)
                {
                    UpdateMainMessage("アイン：よし、３階も無事クリアしたぜ！！");

                    UpdateMainMessage("ラナ：ッフフ、じゃあ皆に知らせに行きましょうか♪");

                    UpdateMainMessage("アイン：ああ、そうだな！");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "アインは一通り、町の住人達に声をかけ、時間が刻々と過ぎていった。";
                        md.ShowDialog();

                        md.Message = "その日の夜、ハンナの宿屋亭にて";
                        md.ShowDialog();
                    }

                    GroundOne.StopDungeonMusic();
                    GroundOne.PlayDungeonMusic(Database.BGM15, Database.BGM15LoopBegin);
                     
                    UpdateMainMessage("アイン：いやあ・・・しかしすごかったよな、あの鏡の数");

                    UpdateMainMessage("ラナ：本当よね、もうどれぐらい通ったか覚えていないぐらいだわ。");

                    UpdateMainMessage("アイン：ラナのマップは本当に助かったぜ。サンキューサンキューな。");

                    UpdateMainMessage("ラナ：今回はワープがあったからあまり役にたってなかったけどね・・・");

                    UpdateMainMessage("アイン：そんな事ねえって！　行った所がちゃんと把握できる時点で大助かりさ！ッハッハッハ！");

                    UpdateMainMessage("ラナ：ッフフ、それなら良いんだけど♪");

                    UpdateMainMessage("アイン：ああ、これからも頼むぜ！ッハッハッハ！  お～い、叔母ちゃん、もう一杯追加できるか？");

                    UpdateMainMessage("ハンナ：ハイよ、いくらでも飲みな。");

                    UpdateMainMessage("アイン：おお、コレコレ！サンキュー！");

                    // 原点解を見つけていない（328)
                    // バッドエンド扱い
                    if (!we.dungeonEvent328)
                    {
                        UpdateMainMessage("ラナ：でも、アインはあれで良かったの？");

                        UpdateMainMessage("アイン：？　何の話だ？");

                        UpdateMainMessage("ラナ：最後の鏡の所よ。何か私に気にかけちゃってたみたいだけど。");

                        UpdateMainMessage("アイン：ああ、アレか。");

                        UpdateMainMessage("アイン：鏡を通り過ぎてたせいもあるしな。まあ、気にするなって。");

                        UpdateMainMessage("アイン：第一、ヒントらしいヒントがないんだ。どうしようもなかっただろ。");

                        UpdateMainMessage("アイン：こういう場合は、素直に目の前を進めるに限る。　それが１番さ。");

                        UpdateMainMessage("ラナ：ん～、それなら良いんだけど・・・");

                        UpdateMainMessage("ハンナ：何か最後にあったのかい？");

                        UpdateMainMessage("ラナ：う～ん、ボスを倒した後にね。　降り階段の前に看板があったのよ。");

                        UpdateMainMessage("ラナ：たしかね・・・え～と・・・");

                        UpdateMainMessage("　　　　『　正解を導きし者、無限解の探求にて永遠に彷徨い、原点を知ること無く、回り続けるがよい　』");

                        UpdateMainMessage("ハンナ：ありゃ、これはややこしい言い回し方だねぇ。");

                        UpdateMainMessage("ラナ：そうなんですよ、おばさんはこれ何か分かります？");

                        UpdateMainMessage("ハンナ：いや、アタシじゃ無理だね。ダンジョンに行ってる本人にしか分からないよ、こういうのは。");

                        UpdateMainMessage("アイン：だよなあ・・・いくら叔母さんでもこういうのは・・・");

                        UpdateMainMessage("ハンナ：原点っていうのは、探したのかい？");

                        UpdateMainMessage("アイン：う～ん、それなんですけどね、実は最初から次の段階のエリアに入る前に別の看板があってですね・・・");
                        
                        GroundOne.StopDungeonMusic();

                        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_NIGHT);

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "アインが予約していた部屋にて";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("アイン：ふう・・・バックパック整理っと・・・");

                        UpdateMainMessage("アイン：いよいよ３階も制覇、次から４階か・・・");

                        UpdateMainMessage("アイン：・・・　・・・");

                        UpdateMainMessage("アイン：・・・");

                        UpdateMainMessage("アイン：ラナは・・・大丈夫なんだろうか。");

                        UpdateMainMessage("アイン：鏡をくぐる時に、あんな風に眼の着色が変わって・・・");

                        UpdateMainMessage("アイン：でもまあ、ラナにしか聞こえなかった呼び声で、正解がわかったわけだ。");

                        UpdateMainMessage("アイン：実際今もラナの体調も悪いわけじゃねえ。");

                        UpdateMainMessage("アイン：大丈夫なハズ・・・行けるはずだ。");

                        this.backgroundData = null;
                        this.BackColor = Color.Black;

                        UpdateMainMessage("アイン：・・・　しかし、何なんだろうこの不安は　・・・");

                        UpdateMainMessage("アイン：あの正解ルートの脅し看板。");

                        UpdateMainMessage("アイン：あれは本当に単なる脅しなんだろうか。");

                        UpdateMainMessage("アイン：それとも・・・。");

                        UpdateMainMessage("アイン：とはいえ、ラナと一緒にダンジョン最下層には近づいていることは確かだ・・・");

                        UpdateMainMessage("アイン：FiveSeekerのヴェルゼさんも今じゃ一緒だ。");

                        UpdateMainMessage("アイン：このメンバーなら最下層へ必ず行ける");
                        
                        UpdateMainMessage("アイン：俺は信じてる。");

                        UpdateMainMessage("アイン：・・・　・・・");

                        UpdateMainMessage("アイン：・・・");

                        UpdateMainMessage("アイン：なにか重大な事を見落としている気がする・・・");

                        UpdateMainMessage("アイン：それも取り返しの付かない何か、あるいは、もう引き戻せない何か。");

                        UpdateMainMessage("アイン：いや、それ以前にだ。");

                        UpdateMainMessage("アイン：俺はもう何度も・・・");

                        UpdateMainMessage("アイン：記憶はねえが、この感覚");

                        UpdateMainMessage("アイン：俺は以前、どこかで【強くなろう】と誓った");

                        UpdateMainMessage("アイン：だがそれがどこから湧いてきた記憶なのか");
                        
                        UpdateMainMessage("アイン：把握できないでいる");

                        UpdateMainMessage("アイン：４階へ向かう俺の足は");

                        UpdateMainMessage("アイン：どんどん深い泥濘にハマっていくようだ");

                        UpdateMainMessage("アイン：底の知れない闇へと・・・");

                        UpdateMainMessage("アイン：・・・　・・・　・・・");

                        UpdateMainMessage("アイン：・・・　・・・");

                        UpdateMainMessage("アイン：・・・");

                        GroundOne.WE2.TruthBadEnd3 = true;
                    }
                    else if (!we.dungeonEvent332)
                    {
                        UpdateMainMessage("ラナ：でも、アインはあれで良かったの？");

                        UpdateMainMessage("アイン：？　何の話だ？");

                        UpdateMainMessage("ラナ：最後の原点解よ。アレを見つけたのに５つ鏡に挑戦しないなんて。");

                        UpdateMainMessage("アイン：ああ・・・");

                        UpdateMainMessage("アイン：・・・");

                        UpdateMainMessage("アイン：いや、良いんだ。");

                        UpdateMainMessage("アイン：俺にとって、どうもあの５つ鏡は進んじゃ行けねえ気がした。");

                        UpdateMainMessage("アイン：それだけの事さ。");

                        UpdateMainMessage("ラナ：ん～、それなら良いんだけど・・・");

                        UpdateMainMessage("ハンナ：何か最後にあったのかい？");

                        UpdateMainMessage("ラナ：う～ん、ボスを倒した後にね。　降り階段の前に看板があったのよ。");

                        UpdateMainMessage("ラナ：たしかね・・・え～と・・・");

                        UpdateMainMessage("　　　　『　正解を導きし者、無限解の探求にて永遠に彷徨い、原点を知ること無く、回り続けるがよい　』");

                        UpdateMainMessage("ハンナ：ありゃ、これはややこしい言い回し方だねぇ。");

                        UpdateMainMessage("ラナ：そうなんですよ、おばさん。でも、実はですね！　聞いてくださいよ！？");

                        UpdateMainMessage("ラナ：何とそこの超バカアインが【原点を知ること無く】の意味に相当する原点解を見つけちゃったんですよ！？");

                        UpdateMainMessage("ハンナ：そうなのかい？たまにはやるじゃないか、アッハハハ。");

                        UpdateMainMessage("アイン：まあ、正直な所半信半疑だったけどな、ハハハ・・・");

                        UpdateMainMessage("ラナ：で、そこまで見つけといて何で５つ鏡には向かわなかったのか、私にはちょっと理解不可能だけど。");

                        UpdateMainMessage("アイン：う～ん、まあまあ、それは良いじゃねえか！");

                        UpdateMainMessage("アイン：おばちゃん、アカシジアのスパゲッティ一つ追加！");

                        UpdateMainMessage("ハンナ：はいよ、待ってな。");

                        GroundOne.StopDungeonMusic();

                        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_NIGHT);

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "アインが予約していた部屋にて";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("アイン：ふう・・・バックパック整理っと・・・");

                        UpdateMainMessage("アイン：いよいよ３階も制覇、次から４階か・・・");

                        UpdateMainMessage("アイン：・・・　・・・");

                        UpdateMainMessage("アイン：・・・");

                        UpdateMainMessage("アイン：ラナは・・・大丈夫なんだろうか。");

                        UpdateMainMessage("アイン：鏡をくぐる時に、あんな風に眼の着色が変わって・・・");

                        UpdateMainMessage("アイン：でもまあ、ラナにしか聞こえなかった呼び声で、正解がわかったわけだ。");

                        UpdateMainMessage("アイン：実際今もラナの体調も悪いわけじゃねえ。");

                        UpdateMainMessage("アイン：大丈夫なハズ・・・行けるはずだ。");

                        this.backgroundData = null;
                        this.BackColor = Color.Black;

                        UpdateMainMessage("アイン：・・・　しかし、何なんだろうこの不安は　・・・");

                        UpdateMainMessage("アイン：原点解を見つけたのは、OKなんだろう");

                        UpdateMainMessage("アイン：しかし、俺にとってはあの５つ鏡がどうしても");

                        UpdateMainMessage("アイン：・・・　・・・");

                        UpdateMainMessage("アイン：怖くて先に進めなかった。");

                        UpdateMainMessage("アイン：この恐怖の根幹が俺自身分からないでいる。");

                        UpdateMainMessage("アイン：記憶の回想を幾つかは見ているが");

                        UpdateMainMessage("アイン：それが何を思い起こさせてくれてるのか、今の俺じゃほとんど掴めないでいる。");

                        UpdateMainMessage("アイン：このメンバーなら最下層へ必ず行ける、その確信はある。");

                        UpdateMainMessage("アイン：・・・");
                        
                        UpdateMainMessage("アイン：だが、それだけだ");

                        UpdateMainMessage("アイン：・・・　・・・");

                        UpdateMainMessage("アイン：・・・");

                        UpdateMainMessage("アイン：なにか重大な事を見落としている気がする・・・");

                        UpdateMainMessage("アイン：それも取り返しの付かない何か、あるいは、もう引き戻せない何か。");

                        UpdateMainMessage("アイン：いや、それ以前にだ。");

                        UpdateMainMessage("アイン：俺はもう何度も・・・");

                        UpdateMainMessage("アイン：記憶はねえが、この感覚");

                        UpdateMainMessage("アイン：俺は以前、どこかで【強くなろう】と誓った");

                        UpdateMainMessage("アイン：だがそれがどこから湧いてきた記憶なのか");

                        UpdateMainMessage("アイン：把握できないでいる");

                        UpdateMainMessage("アイン：４階へ向かう俺の足は");

                        UpdateMainMessage("アイン：どんどん深い泥濘にハマっていくようだ");

                        UpdateMainMessage("アイン：底の知れない闇へと・・・");

                        UpdateMainMessage("アイン：・・・　・・・　・・・");

                        UpdateMainMessage("アイン：・・・　・・・");

                        UpdateMainMessage("アイン：・・・");

                        GroundOne.WE2.TruthBadEnd3 = true;
                    }
                    else
                    {
                        UpdateMainMessage("アイン：ふぅっと・・・");

                        UpdateMainMessage("アイン：しかし、最後の看板・・・気になるんだよな・・・");

                        UpdateMainMessage("アイン：【生】【死】って言われてもなあ・・・");

                        UpdateMainMessage("ラナ：次の階層の何かヒントになってるんじゃないの？");

                        UpdateMainMessage("アイン：まあ、それならそれで、分かりやすいけどな。");

                        UpdateMainMessage("ラナ：別の何か隠された意味とか？原点解みたいに。");

                        UpdateMainMessage("アイン：いや、そういった類じゃなさそうだ。");

                        UpdateMainMessage("アイン：ああいうのは、解答を求める問いかけじゃねえしな。");

                        UpdateMainMessage("ハンナ：最後に何て書いてあったのか、正確に思い出せるかい？");

                        UpdateMainMessage("アイン：あ、ああ。ええと・・・");

                        UpdateMainMessage("アイン：　　　　『　原点を知りし者、　　向かうは　【生】【死】　』");

                        UpdateMainMessage("ハンナ：ふうん、そういう内容だったんだね。");

                        UpdateMainMessage("アイン：おばちゃんは何か分かるか？");

                        UpdateMainMessage("ハンナ：いや、皆目検討も付かないね。");

                        UpdateMainMessage("アイン：そうか・・・");

                        UpdateMainMessage("ハンナ：あんまり気にしちゃダメだよ。");

                        UpdateMainMessage("ハンナ：読み切れない時は、素直にそのまま心に留めておくんだね。");

                        UpdateMainMessage("アイン：ああ、ありがとうな、おばちゃん。");

                        UpdateMainMessage("ラナ：一応書き留めておいてあるから、気になった時は言ってちょうだいね。");

                        UpdateMainMessage("アイン：おお、メモ書き助かるぜ、サンキューな。");

                        UpdateMainMessage("ラナ：ねえ、アイン。");

                        UpdateMainMessage("アイン：ん？何だ？");

                        UpdateMainMessage("ラナ：う～ん、何でもない♪");

                        UpdateMainMessage("アイン：っだ、またお前それかよ。何だったのか、言ってみてくれよ？");

                        UpdateMainMessage("ラナ：そう？じゃあ・・・");

                        UpdateMainMessage("ラナ：アイン、ヴェルゼさんの事どう思う？");

                        UpdateMainMessage("アイン：・・・え？");

                        UpdateMainMessage("ラナ：ヴェルゼさんの事をどう思うのかって聞いてるのよ。");

                        UpdateMainMessage("アイン：ヴェルゼは・・・");

                        UpdateMainMessage("アイン：優しいトコがあって、頼りのある人で、何よりスキルコンビネーションが強え。");

                        UpdateMainMessage("ラナ：う～ん、そうじゃなくて。");

                        UpdateMainMessage("ラナ：どう思うのかって所を聞いてるんだけど。");

                        UpdateMainMessage("アイン：ど、どうって言われてもな・・・");

                        UpdateMainMessage("アイン：正直よく掴めねえって感じではある。");

                        UpdateMainMessage("ラナ：掴めないってどういう意味？");

                        UpdateMainMessage("アイン：DUEL戦でもそうだったんだが");

                        UpdateMainMessage("アイン：基本的に、動作そのもの自体は読めない。");

                        UpdateMainMessage("アイン：思惑、思慮、方向性・・・なんて言ったら良いんだろうな。");

                        UpdateMainMessage("アイン：そういうのも読めない。ようは考えてる事自体が読めないって話だ。");

                        UpdateMainMessage("ラナ：それは、付き合いが短いだけじゃないかしら。");

                        UpdateMainMessage("アイン：まあ、そうとも考えられるけどな。");

                        UpdateMainMessage("ラナ：でも・・・確かにアインの言うとおり・・・");

                        UpdateMainMessage("アイン：ん？");

                        UpdateMainMessage("ハンナ：あの子は、昔からそうだよ。");

                        UpdateMainMessage("ラナ：そうなんですか？");

                        UpdateMainMessage("アイン：な、何の話だよ？");

                        UpdateMainMessage("ラナ：アインが多分ヴェルゼさんの思考を読めないのは、期間的なものをもあるけど");

                        UpdateMainMessage("ラナ：ヴェルゼさん、自分の事を一切喋らないのよ。");

                        UpdateMainMessage("アイン：そうだっけ・・・");

                        UpdateMainMessage("ハンナ：昔から自分に関する会話はしない子だったからね。");

                        UpdateMainMessage("ハンナ：今でもその辺りは変わってないよ。");

                        UpdateMainMessage("ラナ：だからアインや私にとって、ヴェルゼさんがどういう人なのか印象付かないわけよ。");

                        UpdateMainMessage("アイン：う～ん、言われてみりゃそうなのかも・・・");

                        UpdateMainMessage("アイン：あ、そうだ。俺、ヴェルゼとDUEL対決したんだけど");

                        UpdateMainMessage("アイン：ヴェルゼのDUEL戦歴、知らないか？おばちゃん。");

                        UpdateMainMessage("ハンナ：知りたいかい？");

                        UpdateMainMessage("アイン：あ、ああ・・・");

                        UpdateMainMessage("ハンナ：戦歴はね");

                        UpdateMainMessage("アイン：・・・（ゴクリ）");

                        UpdateMainMessage("ハンナ：０勝４２３敗だよ。");

                        UpdateMainMessage("【【【　アインは、戦慄を覚えた　】】】");

                        UpdateMainMessage("アイン：ば・・・バカな！！！");

                        UpdateMainMessage("アイン：あの内容で全敗ってウソだろ！？");

                        UpdateMainMessage("ハンナ：戦歴は絶対に詐称出来ないからね、間違いはないと思うわよ。");

                        UpdateMainMessage("アイン：・・・どういう事だ・・・信じられねえ・・・");

                        UpdateMainMessage("ラナ：アインにとって、ヴェルゼさんのDUEL戦術はどう感じられたの？");

                        UpdateMainMessage("アイン：何ていうか・・・あれで実際勝ち続けてきたんだろうな、って印象だったぜ。");

                        UpdateMainMessage("ハンナ：あの子は、必ず負ける。");

                        UpdateMainMessage("ハンナ：決して勝利を求めたりしない子だったわ。");

                        UpdateMainMessage("アイン：そういうもんか・・・でも・・・");

                        UpdateMainMessage("アイン：・・・　う～ん　・・・");

                        UpdateMainMessage("ラナ：どうかしたの？");

                        UpdateMainMessage("アイン：いや・・・");

                        UpdateMainMessage("アイン：どうだろうな、わかんねえ。");

                        UpdateMainMessage("アイン：まあ・・・いいか！");

                        UpdateMainMessage("アイン：おばちゃん、アカシジアのスパゲッティもう一つ！");

                        UpdateMainMessage("ハンナ：はいよ、少し待ってな。");

                        UpdateMainMessage("　  ＜＜＜　ハンナは厨房へと戻っていった　＞＞＞　");

                        UpdateMainMessage("アイン（小声）：ラナ、あんまヴェルゼに首は突っ込むな。");

                        UpdateMainMessage("ラナ：え！？");

                        UpdateMainMessage("アイン（小声）：この話はちょっと長くなりそうだが、強いて言えば");

                        UpdateMainMessage("アイン（小声）：ヴェルゼは底が深い。");

                        UpdateMainMessage("アイン（小声）：深淵を覗き込む様な感覚だ。");

                        UpdateMainMessage("アイン（小声）：だからやめておけ、良いな？");

                        UpdateMainMessage("ラナ：ええ、別にそういうワケじゃないんだけど。");

                        UpdateMainMessage("アイン（小声）：イイから、普通の会話振りだけにしておけよ、良いな？");

                        UpdateMainMessage("ラナ：わーかった、分かったわよ。");

                        UpdateMainMessage("ハンナ：アカシジアスパゲッティ、お待ちどうさま。");

                        UpdateMainMessage("アイン：おっしゃ、待ってました！");

                        UpdateMainMessage("ラナ：ったく、ゲンキンよねホンット・・・ハアアァァァ");

                        UpdateMainMessage("アイン：ラナ、４階に行ったらさ、俺からヴェルゼに幾つか聞いてみるぜ。");

                        UpdateMainMessage("ラナ：えっ・・・");

                        UpdateMainMessage("ラナ：っえええええええぇ！！？！？！？");

                        UpdateMainMessage("アイン：うわっ、お前声がデケェっつうの。");

                        UpdateMainMessage("ラナ（小声）：ちちちちちょっと、さっきと全然違うじゃないの。");

                        UpdateMainMessage("アイン：良いんだって、こういう場合はこれが１番さ。");

                        UpdateMainMessage("ラナ：何が１番なのよ、まったくもう・・・");

                        UpdateMainMessage("アイン：おっしゃ、待ってろよヴェルゼ、ッハッハッハ！");

                        UpdateMainMessage("アイン：じゃ、ごちそうさま！");

                        UpdateMainMessage("ハンナ：はいよ、寝る前は、ちゃんとお腹を休めるんだよ。");

                        UpdateMainMessage("アイン：ああ、分かった。ありがとうなおばちゃん。");

                        UpdateMainMessage("アイン：よし、じゃあ寝床に戻って荷物整理でもすっかな。");

                        UpdateMainMessage("ラナ：じゃあ、また明日ね。");

                        UpdateMainMessage("アイン：ああ。");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "アインが予約していた部屋にて";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("アイン：ふう・・・バックパック整理っと・・・");

                        UpdateMainMessage("アイン：いよいよ次から４階か・・・");

                        UpdateMainMessage("アイン：・・・　・・・");

                        UpdateMainMessage("アイン：・・・");

                        UpdateMainMessage("アイン：ラナが導き出した正解、そして原点解への到達。");

                        UpdateMainMessage("アイン：そのおかげで、無限解も通過した。");

                        UpdateMainMessage("アイン：大丈夫なハズ・・・行けるはずだ。");

                        this.backgroundData = null;
                        this.BackColor = Color.Black;

                        UpdateMainMessage("アイン：・・・　・・・");

                        this.backgroundData = null;
                        this.BackColor = Color.Black;

                        if (GroundOne.WE2.TruthRecollection3_4)
                        {
                            UpdateMainMessage("アイン：記憶をたどると、次々思い出せることがある。");

                            UpdateMainMessage("アイン：だが、今俺が遭遇している事象と食い違いが幾つも存在している。");

                            UpdateMainMessage("アイン：整理して考えたい。");

                            UpdateMainMessage("アイン：だが、そんな気持ちとは裏腹に");

                            UpdateMainMessage("アイン：整理した結果は見たくも無いという気持ちも混在している。");

                            UpdateMainMessage("アイン：どうするか・・・");

                            UpdateMainMessage("アイン：・・・　・・・");

                            UpdateMainMessage("アイン：いや、考えるべきだ");

                            UpdateMainMessage("アイン：俺は今、そうしなくちゃならない");

                            UpdateMainMessage("アイン：・・・");

                            UpdateMainMessage("アイン：最後の看板には【生】【死】が書かれていた。");

                            UpdateMainMessage("アイン：だが、俺の記憶している限り、あそこの看板は・・・");

                            UpdateMainMessage("アイン：【死】");

                            UpdateMainMessage("アイン：それだけのはず");

                            UpdateMainMessage("アイン：何故、今回は【生】【死】なのか。");

                            UpdateMainMessage("アイン：これは人間の生死に関連している。");

                            UpdateMainMessage("アイン：記憶が【死】だけで、今みた看板は【生】【死】であるとすれば、");

                            UpdateMainMessage("アイン：悪い方じゃねえ。良い方に考えていいはずだ。");

                            UpdateMainMessage("アイン：まあ、こんな考え方はまた師匠にどやされるのがオチだけどな、ここでは良しとしよう。");

                            UpdateMainMessage("アイン：次に、ヴェルゼ・アーティという存在。");

                            UpdateMainMessage("アイン：俺の記憶では、ヴェルゼ・アーティは過去出会った事がある。");

                            UpdateMainMessage("アイン：そして今現在の俺としては、ヴェルゼ・アーティを見るのは初めてだ。");

                            UpdateMainMessage("アイン：常識的に考えてこの感覚は明らかにおかしい。");

                            UpdateMainMessage("アイン：何せ記憶上では会っているのだから");

                            UpdateMainMessage("アイン：今現在、俺は初めてヴェルゼの顔を見たというのは");

                            UpdateMainMessage("アイン：記憶が飛んでいるか、もしくは");

                            UpdateMainMessage("アイン：消されているか");

                            UpdateMainMessage("アイン：・・・");

                            UpdateMainMessage("アイン：ダメだ・・・ここら辺は追いかけても無駄だ。");

                            UpdateMainMessage("アイン：・・・");

                            UpdateMainMessage("アイン：ヴェルゼ・アーティに関する過去は断片的な情報の蓄積しか残されていない。");

                            UpdateMainMessage("アイン：技の達人。");

                            UpdateMainMessage("アイン：伝説のFiveSeekerの一人");

                            UpdateMainMessage("アイン：ファージル宮殿に仕えし者");

                            UpdateMainMessage("アイン：そして今は、俺と共にパーティを組んでくれている");

                            UpdateMainMessage("アイン：だが、ヴェルゼ・アーティの事を俺は何も知らないままだ");

                            UpdateMainMessage("アイン：そんな中でもかすかなヒントはあった");

                            UpdateMainMessage("アイン：記憶では、ラナが示す可能性。いわゆる女のカンってヤツだが");

                            UpdateMainMessage("アイン：ヴェルゼ・アーティには恋人が存在していた時期があり");

                            UpdateMainMessage("アイン：そしてその人は不慮の事故で亡くなってしまった。");

                            UpdateMainMessage("アイン：ヴェルゼ・アーティにとっては恋人を失った世界。");

                            UpdateMainMessage("アイン：幻想を望んだとしてもおかしくはない。良識の範囲内だ。");

                            UpdateMainMessage("アイン：このラナの推察はおそらく正しい。なぜなら");

                            UpdateMainMessage("アイン：記憶を辿り、ファラ様から教えてもらった内容と照らし合わせると");

                            UpdateMainMessage("アイン：ピタリと照合するからだ。");

                            UpdateMainMessage("アイン：元国王エルミやファラ王妃、カール、ボケ師匠。");

                            UpdateMainMessage("アイン：そしてヴェルゼ・アーティ");

                            UpdateMainMessage("アイン：6人目、エレマ・セフィーネ");

                            UpdateMainMessage("アイン：エレマさんはヴェルゼ・アーティの特性を知り尽くしていた。");

                            UpdateMainMessage("アイン：それがどういう事なのか、いくらなんでも俺にだって分かる。");

                            UpdateMainMessage("アイン：だが、今現在エレマさんは俺の前には存在しない。");

                            UpdateMainMessage("アイン：存在しているのは、ヴェルゼ・アーティだ。");

                            UpdateMainMessage("アイン：そもそも、何故いま俺の目の前にいるのか。");

                            UpdateMainMessage("アイン：・・・　・・・");

                            UpdateMainMessage("アイン：・・・");

                            UpdateMainMessage("アイン：ダメだ・・・ここも結局分からねえ・・・");

                            UpdateMainMessage("アイン：・・・");

                            UpdateMainMessage("アイン：・・・落ち着け・・・");

                            UpdateMainMessage("アイン：・・・");

                            UpdateMainMessage("アイン：ダンジョン");

                            UpdateMainMessage("アイン：最後にファラ様に言われた事");

                            UpdateMainMessage("アイン：終わりへと足を運ぶな");

                            UpdateMainMessage("アイン：始まりへと足を進めろ");

                            UpdateMainMessage("アイン：俺はなんとなくだが、この言葉に今、恐怖を覚えている");

                            UpdateMainMessage("アイン：何故なら、今現在");

                            UpdateMainMessage("アイン：もう４階に向けて足を運んでしまっている");

                            UpdateMainMessage("アイン：帰るべきなのか？");

                            UpdateMainMessage("アイン：・・・　・・・");

                            UpdateMainMessage("アイン：・・・");

                            UpdateMainMessage("アイン：いや、ダメだ。");

                            UpdateMainMessage("アイン：俺はダンジョンへ行く。そう心に誓った");

                            UpdateMainMessage("アイン：何度も何度もそう心に誓っている気がするが・・・");

                            UpdateMainMessage("アイン：その理由はシンプルだ");

                            UpdateMainMessage("アイン：資金源、戦闘スキル上達、度胸試し、人によって理由は様々だと思うが");

                            UpdateMainMessage("アイン：俺がダンジョンへ行こうとした理由はただ一つ");

                            UpdateMainMessage("アイン：ラナを守るため");

                            UpdateMainMessage("アイン：その証明の一つとして");

                            UpdateMainMessage("アイン：ダンジョンは俺一人で完全に制覇するつもりだ");

                            UpdateMainMessage("アイン：今は、ラナやヴェルゼと共に行動しているが");

                            UpdateMainMessage("アイン：それはそれで成り行きだ、無理に断ったりはしない");

                            UpdateMainMessage("アイン：どうあれ、ダンジョン制覇は必ずやってみせる");

                            UpdateMainMessage("アイン：難攻不落のダンジョン");

                            UpdateMainMessage("アイン：神々が住まうダンジョン");

                            UpdateMainMessage("アイン：神の遺産が得られるダンジョン");

                            UpdateMainMessage("アイン：人の心を喰らうダンジョン");

                            UpdateMainMessage("アイン：いろいろと噂はある");

                            UpdateMainMessage("アイン：挑戦した者は数知れないが、制覇した者は極々わずか");

                            UpdateMainMessage("アイン：噂だけじゃFiveSeekerだけじゃねえのかって話もあるぐらいだ");

                            UpdateMainMessage("アイン：つまり、このダンジョンが俺にも制覇できれば");

                            UpdateMainMessage("アイン：もう俺が何かに負けたりする事もない");

                            UpdateMainMessage("アイン：どんな事柄が起きようと");

                            UpdateMainMessage("アイン：ラナを必ず守れるようになる");

                            UpdateMainMessage("アイン：決めた事なんだ");

                            UpdateMainMessage("アイン：・・・");

                            UpdateMainMessage("アイン：退くわけには行かない。");

                            UpdateMainMessage("アイン：必ず・・・解くんだ。");

                            UpdateMainMessage("アイン：・・・");

                            GroundOne.WE2.TruthKey3 = true; // これを真実世界へのキーその３とする。
                        }

                        UpdateMainMessage("　　　【記憶と事実の矛盾】");

                        UpdateMainMessage("　　　【ヴェルゼとエレマ】");

                        UpdateMainMessage("　　　【にじり寄ってくる恐怖感】");

                        if (we.dungeonEvent328)
                        {
                            UpdateMainMessage("　　　【生と死を示した看板】");
                        }
                        else
                        {
                            UpdateMainMessage("　　　【無限解を示した看板】");
                        }

                        UpdateMainMessage("　　　【分からない事だらけのまま】");

                        UpdateMainMessage("　　　【俺は４階へと足を進める】");

                        UpdateMainMessage("　　　【答えは多分】");

                        UpdateMainMessage("　　　【この先にある】");
                    }

                    we.TruthCommunicationCompArea3 = true;

                    CallRestInn(true);

                    using (ESCMenu esc = new ESCMenu())
                    {
                        esc.MC = this.MC;
                        esc.SC = this.SC;
                        esc.TC = this.TC;
                        esc.WE = this.we;
                        esc.KnownTileInfo = null;
                        esc.KnownTileInfo2 = null;
                        esc.KnownTileInfo3 = null;
                        esc.KnownTileInfo4 = null;
                        esc.KnownTileInfo5 = null;
                        esc.Truth_KnownTileInfo = this.Truth_KnownTileInfo;
                        esc.Truth_KnownTileInfo2 = this.Truth_KnownTileInfo2;
                        esc.Truth_KnownTileInfo3 = this.Truth_KnownTileInfo3;
                        esc.Truth_KnownTileInfo4 = this.Truth_KnownTileInfo4;
                        esc.Truth_KnownTileInfo5 = this.Truth_KnownTileInfo5;
                        esc.StartPosition = FormStartPosition.CenterParent;
                        esc.TruthStory = true;
                        esc.OnlySave = true;
                        esc.ShowDialog();
                    }

                    //this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                    this.BackColor = Color.WhiteSmoke;
                    this.Update();
                    UpdateMainMessage("", true);
                    FourthCommunicationStart();
                }
                #endregion
                #region "３階、鏡エリア２－５をクリアした時"
                 else if (we.dungeonEvent312 && !we.dungeonEvent312_2)
                 {
                     we.dungeonEvent312_2 = true;

                     UpdateMainMessage("アイン：っしゃ、戻ってきたぜ。");

                     UpdateMainMessage("ヴェルゼ：アイン君、ボクは少しエルミ王、ファラ王妃へ現状の報告を行ってきます。");

                     UpdateMainMessage("アイン：あ、ああ、そうか。って現状報告？");

                     UpdateMainMessage("ヴェルゼ：はい。一応ファージル宮殿で勤めている以上、報告は必須事項ですから。");

                     UpdateMainMessage("アイン：そうなのか。じゃあ、分かったまた明日な。");

                     UpdateMainMessage("ヴェルゼ：ええ、では。");

                     UpdateMainMessage("　　　＜＜＜　ヴェルゼはその場から去っていった ＞＞＞");

                     UpdateMainMessage("アイン：さてと。何か奇妙な台座試練も抜けて来た事だし、明日はいよいよボスって所だな！");

                     UpdateMainMessage("ラナ：ようやくって感じよね。");

                     UpdateMainMessage("アイン：何か結構グルグルと回された感じだったよな、覚えきれねえぜ。");

                     UpdateMainMessage("ラナ：私もよ、ちょっと今回のはどこがどう繋がってるのか、全然把握できないわ。");

                     UpdateMainMessage("アイン：はあぁ・・・明日また地道に探すのか・・・キッチリ覚えておきゃ良かったぜ、とほほ。");

                     UpdateMainMessage("ラナ：まあ、明日また行ってみましょ、それしかないんだから♪");

                     UpdateMainMessage("アイン：ああ、じゃあまた明日な！");

                     UpdateMainMessage("ラナ：うん、またね♪");
                 }
                 #endregion
                #region "３階、鏡エリア２－４をクリアした時"
                 else if (we.dungeonEvent317 && !we.dungeonEvent317_2)
                 {
                     we.dungeonEvent317_2 = true;

                     UpdateMainMessage("ヴェルゼ：アイン君、すみませんがファージル宮殿へ行ってます。");

                     UpdateMainMessage("アイン：ああ、頼んだ。");

                     UpdateMainMessage("　　　＜＜＜　ヴェルゼはその場から去っていった ＞＞＞");

                     UpdateMainMessage("アイン：ラナ、おい。起きれるか？");

                     UpdateMainMessage("ラナ：えっ？ここどこよ？");

                     UpdateMainMessage("アイン：ダンジョンゲートの入り口だ。ほら、応急セットだ。");

                     UpdateMainMessage("ラナ：あっ、ありがと♪");

                     UpdateMainMessage("ラナ：で、何でここに戻ってきてるのよ、まさか私気絶しちゃってたワケ？");

                     UpdateMainMessage("アイン：いや、何か気絶というよりは、スヤスヤと寝てる感じだったぞ。");

                     UpdateMainMessage("ラナ：う～ん、度々ゴメンね。");

                     UpdateMainMessage("アイン：いいって、何か気持ちよさそうね寝てたし。");

                     UpdateMainMessage("ラナ：・・・");

                     UpdateMainMessage("ラナ：ちょっと・・・見ないでよね、人の寝顔。");

                     UpdateMainMessage("アイン：ッグア・・・分かった分かった・・・そんだけ元気がありゃオーケーだ。");

                     UpdateMainMessage("アイン：宿屋まで戻れそうか？");

                     UpdateMainMessage("ラナ：ええ、大丈夫よ。");

                     UpdateMainMessage("アイン：ゆっくり休むんだぞ。");

                     UpdateMainMessage("ラナ：ええ、また明日よろしくね♪");

                     UpdateMainMessage("アイン：ああ、またな。");
                 }
                 #endregion
                #region "３階、鏡エリア２－３をクリアした時"
                 else if (we.dungeonEvent316 && !we.dungeonEvent316_2)
                 {
                     we.dungeonEvent316_2 = true;

                     UpdateMainMessage("アイン：ラナ・・・ほら応急セットだ。");

                     UpdateMainMessage("ラナ：っふう・・・ありがと。");

                     UpdateMainMessage("アイン：大丈夫か？");

                     UpdateMainMessage("ラナ：ええ、もう何とも無いみたい。");

                     UpdateMainMessage("ヴェルゼ：応急セットはファージル宮殿倉庫には山のようにありますから、心配せず使ってください。");

                     UpdateMainMessage("ラナ：うん、ごめんなさい、ありがとうね。");

                     UpdateMainMessage("ヴェルゼ：それでは、すみませんがファージル宮殿へ次の分を取りに行きます。");

                     UpdateMainMessage("　　　＜＜＜　ヴェルゼはその場から去っていった ＞＞＞");

                     UpdateMainMessage("アイン：ラナ、宿屋まで送っていこうか？");

                     UpdateMainMessage("ラナ：イイって♪　ホンットに大丈夫だから♪");

                     UpdateMainMessage("アイン：ったく、頑固な奴だな本当に、ッハハハ・・・");

                     UpdateMainMessage("アイン：いいか、ヴェルゼも言ってたが辛い時はちゃんと言うんだぞ、いいな？");

                     UpdateMainMessage("ラナ：ええ、分かったわ。ちゃんと言うようにするから心配しないで♪");

                     UpdateMainMessage("アイン：了解了解、じゃあ、また明日な。");

                     UpdateMainMessage("ラナ：うん、アインもゆっくり休んでね。");

                     UpdateMainMessage("アイン：ああ。");
                 }
                 #endregion
                #region "３階、鏡エリア２－２をクリアした時"
                 else if (we.dungeonEvent315 && !we.dungeonEvent315_2)
                 {
                     we.dungeonEvent315_2 = true;

                     UpdateMainMessage("アイン：ラナ、宿屋まで歩いていけるか？");

                     UpdateMainMessage("ラナ：ええ、そのぐらいは大丈夫よ。");

                     UpdateMainMessage("ヴェルゼ：ラナさん、応急セットをどうぞ。");

                     UpdateMainMessage("ラナ：ありがとう♪");

                     UpdateMainMessage("ヴェルゼ：次の予備の分が必要ですね、応急セットを取ってきておきましょう。");

                     UpdateMainMessage("アイン：ああ、すまねえ。");

                     UpdateMainMessage("　　　＜＜＜　ヴェルゼはその場から去っていった ＞＞＞");

                     UpdateMainMessage("ラナ：・・・この応急セット凄いわね。何だか精神的な疲れが一瞬で飛んでいく感じだわ。");

                     UpdateMainMessage("アイン：そうなのか？");

                     UpdateMainMessage("ラナ：ええ、さすがファージル宮殿御用達って所なのかしら。");

                     UpdateMainMessage("アイン：でも今日は一旦休もうぜ。それは一時のもんだろうしな。");

                     UpdateMainMessage("ラナ：ええ、分かったわ。じゃあ、またね。");

                     UpdateMainMessage("アイン：ああ。");
                 }
                 #endregion
                #region "３階、鏡エリア２－１をクリアした時"
                 else if (we.dungeonEvent314 && !we.dungeonEvent314_2)
                 {
                     we.dungeonEvent314_2 = true;

                     UpdateMainMessage("アイン：ラナ、大丈夫か？");

                     UpdateMainMessage("ラナ：う～ん、大丈夫だとは思う。");

                     UpdateMainMessage("ヴェルゼ：アイン君、ボクはファージル宮殿から応急セットを持ってきます。");

                     UpdateMainMessage("アイン：ああ、すまねえな頼むぜ。");

                     UpdateMainMessage("　　　＜＜＜　ヴェルゼはその場から去っていった ＞＞＞");

                     UpdateMainMessage("アイン：ラナ、今日は宿屋へ戻って休むんだ。");

                     UpdateMainMessage("ラナ：何かゴメンね、変なトコになっちゃって・・・");

                     UpdateMainMessage("アイン：いいって、進めたんだし全然OKだろ。今はとにかく休むんだ。いいな？");

                     UpdateMainMessage("ラナ：ええ、わかったわ。じゃあ、また明日ね♪");

                     UpdateMainMessage("アイン：ああ、また明日な。");
                 }
                 #endregion
                #region "３階、エリア１の鏡をクリア時"
                 else if (we.TruthCompleteArea1 && we.TruthCompleteArea2 && !we.TruthCompleteArea3 && we.dungeonEvent305 && !we.dungeonEvent306)
                 {
                     we.dungeonEvent306 = true;

                     UpdateMainMessage("アイン：ふう、戻ったな。");

                     UpdateMainMessage("ラナ：でも、本当に珍しいわね、バカアインが賢い選択をするなんて。");

                     UpdateMainMessage("アイン：・・・いや、正直な所・・・");

                     UpdateMainMessage("ラナ：え？");

                     UpdateMainMessage("アイン：いや、何でもねえ。悪い悪い。");

                     UpdateMainMessage("ラナ：え、ちょっと！？　何その言いかけて止めるのナシにしてよね！？");

                     UpdateMainMessage("アイン：いやいや・・・まあ・・・");

                     UpdateMainMessage("ヴェルゼ：アイン君、すみませんがボクは少しファージル宮殿に用事があるので、宮殿へ戻っていますね。");

                     UpdateMainMessage("アイン：ん？あ、あぁ。　了解了解！");

                     UpdateMainMessage("　　　＜＜＜　ヴェルゼはその場から去っていった ＞＞＞");

                     UpdateMainMessage("アイン：ええとだな・・・じゃあラナ。");

                     UpdateMainMessage("アイン：ラナは鏡を潜ってみてその・・・");

                     UpdateMainMessage("アイン：どうだ。　体調に変化はないか？");

                     UpdateMainMessage("ラナ：・・・え？");

                     UpdateMainMessage("ラナ：えええええぇぇぇぇ！！？？");

                     UpdateMainMessage("アイン：うわっ、いきなり大声出すなっつうの。");

                     UpdateMainMessage("ラナ：え、ちょっと何、ひょっとして・・・");

                     UpdateMainMessage("ラナ：私の体調でも気遣ったって言ってるワケ！？");

                     UpdateMainMessage("アイン：いやいや、何となく気になっただけだ！　そんな御大層な心配じゃねえって！");

                     UpdateMainMessage("ラナ：・・・ップ");

                     UpdateMainMessage("ラナ：ップハハハハ、何いってんのこのバカアイン、アンタほんとオカシイんじゃないの♪");

                     UpdateMainMessage("ラナ：体調？？　だーいじょうぶに決まってるじゃないの♪　変な所でカンチガイ気遣いしすぎよホント♪");

                     UpdateMainMessage("アイン：な、なら良いんだがな。ッハハハ・・・");

                     UpdateMainMessage("アイン：まあ、たまにはこんな風に切り上げるのも悪くはないだろ。");

                     UpdateMainMessage("ラナ：ッフフ、そうね。たまには許してあげるとするわ♪");

                     UpdateMainMessage("アイン：じゃあ、今日はここまでだ。");

                     UpdateMainMessage("アイン：っお、どうだ。　飯でも一緒に食べていくか？");

                     UpdateMainMessage("ラナ：ううん、それは結構よ、一人で食べるから♪");

                     UpdateMainMessage("アイン：そか・・・まあ、それだけ元気なら良いんだ。");

                     UpdateMainMessage("アイン：（・・・気のせいだな、きっと）");

                     UpdateMainMessage("アイン：じゃあまた明日もよろしく頼むぜ！");

                     UpdateMainMessage("ラナ：エエ、おつかれさま♪");
                 }
                 #endregion
                #region "後編初日"
                 else if (this.firstDay >= 1 && !we.Truth_CommunicationFirstHomeTown)
                 {
                     GroundOne.StopDungeonMusic();

                     dayLabel.Visible = false;
                     buttonHanna.Visible = false;
                     buttonDungeon.Visible = false;
                     buttonRana.Visible = false;
                     buttonGanz.Visible = false;
                     this.backgroundData = null;
                     this.BackColor = Color.Black;

                     if (GroundOne.WE2.TruthBadEnd1) UpdateMainMessage("アイン：・・・ここは・・・っ・・・いつつ・・・");
                     else UpdateMainMessage("アイン：っ・・・いつつ・・・");

                     if (GroundOne.WE2.TruthBadEnd1) UpdateMainMessage("アイン：今、何時ぐらいだ？");
                     else UpdateMainMessage("アイン：今・・・何時だ？");

                     UpdateMainMessage("        『アインは宿屋の寝床から起き上がった。』");

                     if (GroundOne.WE2.TruthBadEnd1) UpdateMainMessage("アイン：朝７時ぐらいか。まあ、ちょうど良いぐらいの時間だな。");
                     else UpdateMainMessage("アイン：朝の６時か・・・起きるには少し早いぐらいだな。");

                     UpdateMainMessage("アイン：・・・ん？何か床に落ちてるな。");

                     using (MessageDisplay md = new MessageDisplay())
                     {
                         md.Message = "【ラナのイヤリング】を手に入れました。";
                         GroundOne.playbackMessage.Insert(0, md.Message);
                         GroundOne.playbackInfoStyle.Insert(0, TruthPlaybackMessage.infoStyle.notify);
                         md.StartPosition = FormStartPosition.CenterParent;
                         md.ShowDialog();
                     }

                     GetItemFullCheck(mc, Database.RARE_EARRING_OF_LANA);

                     if (GroundOne.WE2.TruthBadEnd1) UpdateMainMessage("アイン：ラナのイヤリングじゃねえか・・・何でこんな物が・・・");
                     else UpdateMainMessage("アイン：何だ、ラナのやつ。何でまたこんな所に落としてるんだ。");

                     if (GroundOne.WE2.TruthBadEnd1) UpdateMainMessage("アイン：・・・　何であるんだっけ　・・・　ラナが落としたのか？");
                     else UpdateMainMessage("アイン：・・・　・・・");

                     if (GroundOne.WE2.TruthBadEnd1) UpdateMainMessage("アイン：いいや、そんなワケ無えよな・・・じゃあ何でだ・・・");
                     else UpdateMainMessage("アイン：しょうがねえ。後で渡しに行ってやるか。");

                     if (GroundOne.WE2.TruthBadEnd1) UpdateMainMessage("アイン：まあいいか。とりあえず、目覚めたわけだし、町にでも出てみるか！");
                     else UpdateMainMessage("アイン：おっしゃ、せっかく目覚めたわけだし、町にでも出てみるか。");

                     ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                     this.BackColor = Color.WhiteSmoke;
                     buttonHanna.Visible = true;
                     buttonDungeon.Visible = true;
                     buttonRana.Visible = true;
                     buttonGanz.Visible = true;
                     dayLabel.Visible = true;


                     UpdateMainMessage("アイン：さて、何すっかな。", true);

                     we.Truth_CommunicationFirstHomeTown = true;
                     we.AlreadyRest = true; // 朝起きたときからスタートとする。

                     GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
                 }
                 #endregion
                #region "１階看板最後の情報を入手したとき"
                 else if (this.firstDay >= 1 && !we.Truth_Communication_Dungeon11 && we.dungeonEvent27)
                 {
                     UpdateMainMessage("アイン：いっつつつ・・・すまねえな。");

                     UpdateMainMessage("ラナ：別に良いわよ。でも、本当に大丈夫？");

                     UpdateMainMessage("アイン：ああ、何とかな。大丈夫だ、少しひいてきた。");

                     UpdateMainMessage("ラナ：汗も少しひいたみたいね。");

                     UpdateMainMessage("アイン：一体何なんだ、あの看板は。");

                     UpdateMainMessage("ラナ：座標地点を指し示していたみたいだけど？");

                     UpdateMainMessage("アイン：＜始まりの地にて＞　・・・か。");

                     UpdateMainMessage("アイン：そうか。『始まりの地、見落とすべからず。』って看板もあったよな。");

                     UpdateMainMessage("ラナ：きっとコレの事を示していたのね。");

                     UpdateMainMessage("アイン：ラナ、『４７　２９』と言ったらどの辺になるんだ？");

                     UpdateMainMessage("ラナ：おそらくだけどこの数字は座標ポイントＸとＹを示してるのよ。");

                     UpdateMainMessage("ラナ：Ｘは左右、Ｙを上下とすると、Ｘ方向へ４７、Ｙ方向へ２７。");

                     UpdateMainMessage("ラナ：つまり、右下のこの辺りを指し示してる事になるわ。");

                     UpdateMainMessage("ラナ：ちゃんと印付けておいたから。忘れることは無いと思うわ。");

                     UpdateMainMessage("アイン：明日になったら、行ってみるとするか。");

                     UpdateMainMessage("ラナ：うん、そうね。今日はもう休みましょう。");

                     UpdateMainMessage("アイン：ああ。それじゃ、ハンナ叔母さんの宿屋へ行こうぜ。");

                     UpdateMainMessage("ラナ：了解よ。");

                     we.Truth_Communication_Dungeon11 = true;
                 }
                 #endregion
                #region "１階制覇"
                 else if (this.we.TruthCompleteArea1 && !this.we.TruthCommunicationCompArea1)
                 {
                     if (we.AvailableSecondCharacter)
                     {
                         UpdateMainMessage("アイン：っしゃ！やったぜ！ラナ！");

                         UpdateMainMessage("ラナ：上出来なんじゃない♪");

                         UpdateMainMessage("アイン：ラナ！お前はやっぱ最高のパートナーだぜ！ッハッハッハ！");

                         UpdateMainMessage("ラナ：また、そんな浮かれてちゃって。ホラホラ、町の住人達に報告しにいきましょ。");

                         UpdateMainMessage("アイン：あ、ああ、そうだな！ッハッハッハ！");

                         using (MessageDisplay md = new MessageDisplay())
                         {
                             md.StartPosition = FormStartPosition.CenterParent;
                             md.Message = "アインは一通り、町の住人達に声をかけ、時間が刻々と過ぎていった。";
                             md.ShowDialog();

                             md.Message = "その日の夜、ハンナの宿屋亭にて";
                             md.ShowDialog();
                         }

                         GroundOne.StopDungeonMusic();
                         GroundOne.PlayDungeonMusic(Database.BGM15, Database.BGM15LoopBegin);

                         UpdateMainMessage("アイン：ここで、ボスが例の必殺技を出してくる瞬間にだな、ズバズバっと！");

                         UpdateMainMessage("ラナ：何言ってんのよ。誰が隙を作ってあげたと思ってるのよ？");

                         UpdateMainMessage("アイン：いやいや、そうだったな。サンキューサンキュー！");

                         // ラナのイヤリングを渡してしまっていた場合、かつ
                         // 真実解の部屋へ到達していない場合、BADEND
                         if ((we.Truth_GiveLanaEarring) &&
                             (!GroundOne.WE2.TruthRecollection1))
                         {
                             UpdateMainMessage("ハンナ：おやおや、アイン。バカに騒いでるようだね。");

                             UpdateMainMessage("アイン：聞いてくれよ、おばちゃん。来たんだよな、ココで・・・");

                             UpdateMainMessage("アイン：典型のヒラメキがな！！");

                             UpdateMainMessage("アイン：ッハッハッハッハッハ！！");

                             UpdateMainMessage("ラナ：まあ典型的なバカよね・・・ハアアァァァ・・・");

                             UpdateMainMessage("ラナ：アイン、そろそろ私は部屋に戻って一旦休息するわね。");

                             UpdateMainMessage("アイン：ん？あぁ！了解了解！");

                             UpdateMainMessage("ラナ：ハンナ叔母さん。どうもごちそう様でした♪");

                             UpdateMainMessage("ハンナ：あいよ、また明日も頑張るんだね。");

                             UpdateMainMessage("ラナ：ハイ、それでは失礼します。");

                             using (MessageDisplay md = new MessageDisplay())
                             {
                                 md.StartPosition = FormStartPosition.CenterParent;
                                 md.Message = "ラナは自分が予約していた部屋へ歩いていった。";
                                 md.ShowDialog();
                             }

                             UpdateMainMessage("アイン：はあ～食った食った。満足だ。おばちゃん、ありがと！");

                             UpdateMainMessage("ハンナ：よく食べたね。明日に差し支えないようにするんだよ。");

                             UpdateMainMessage("アイン：はい！！ありがとっした！！");

                             UpdateMainMessage("ハンナ：アイン。そういえば、部屋に何か落ちていなかったかい？");

                             UpdateMainMessage("アイン：・・・っはい？");

                             UpdateMainMessage("ハンナ：イヤリング、部屋に何か落ちていなかったかい？");

                             UpdateMainMessage("アイン：・・・っあ、ああ。あれなら・・・");

                             UpdateMainMessage("アイン：ラナに返しておきましたよ。");

                             UpdateMainMessage("アイン：確かアイツがいつも付けてたイヤリングですから。");

                             UpdateMainMessage("ハンナ：そうかい。まあ、無くさないように伝えておくんだよ。");

                             UpdateMainMessage("アイン：え～っと、分かりました！じゃ、ごちそう様でした！");

                             UpdateMainMessage("ハンナ：はいよ、明日もあるだろう。ゆっくりと休みな。");

                             UpdateMainMessage("アイン：ありがとうございます、失礼します！");

                             GroundOne.StopDungeonMusic();

                             ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_NIGHT);

                             using (MessageDisplay md = new MessageDisplay())
                             {
                                 md.StartPosition = FormStartPosition.CenterParent;
                                 md.Message = "アインが予約していた部屋にて";
                                 md.ShowDialog();
                             }

                             UpdateMainMessage("アイン：ふう・・・バックパック整理っと・・・");

                             UpdateMainMessage("アイン：明日から２階か・・・っしゃ！気合入れるぜ！！");

                             UpdateMainMessage("アイン：・・・　・・・");

                             UpdateMainMessage("アイン：・・・");

                             UpdateMainMessage("アイン：・・・　なんだろう　・・・");

                             UpdateMainMessage("アイン：ラナと一緒にダンジョンへ進んで・・・それから・・・");

                             this.backgroundData = null;
                             this.BackColor = Color.Black;

                             UpdateMainMessage("アイン：・・・　何か忘れてる気がする　・・・");

                             UpdateMainMessage("アイン：俺、何やってたんだっけ・・・");

                             UpdateMainMessage("アイン：まあいいや、ひとまずダンジョンの最下層へ・・・");

                             UpdateMainMessage("アイン：・・・　・・・");

                             UpdateMainMessage("アイン：・・・");

                             UpdateMainMessage(" ～　THE　END　～　（虚構へ）");

                             GroundOne.WE2.TruthBadEnd1 = true;
                         }
                         // それ以外はGOOD
                         else
                         {
                             UpdateMainMessage("ハンナ：おやおや、アイン。バカに騒いでるようだね。");

                             UpdateMainMessage("アイン：聞いてくれよ、おばちゃん。来たんだよな、ココで・・・");

                             UpdateMainMessage("アイン：天啓のヒラメキがな！！");

                             UpdateMainMessage("アイン：ッハッハッハッハッハ！！");

                             UpdateMainMessage("ラナ：たまたま突き刺した部分がボスの急所だっただけでしょ？");

                             UpdateMainMessage("アイン：狙ってやったに決まってるだろ？");

                             UpdateMainMessage("ラナ：ホンットてきとーなんだから・・・");

                             UpdateMainMessage("ハンナ：まあまあ、良いじゃないかラナちゃん。上手く進めたようだしね。");

                             UpdateMainMessage("ラナ：ええ、そうですね。たまにはバカも本来のバカに戻って嬉しいでしょうし♪");

                             UpdateMainMessage("アイン：ああ、バカで結構！");

                             UpdateMainMessage("アイン：ッハッハッハッハッハ！！");

                             UpdateMainMessage("ラナ：ハアアァァァ・・・・大丈夫なのかしら、こんな感じで・・・");

                             if (!we.Truth_GiveLanaEarring)
                             {
                                 UpdateMainMessage("アイン：っと、そうだ！忘れてたぜ！");

                                 UpdateMainMessage("ラナ：っな、何よ突然どうしたのよ？");

                                 UpdateMainMessage("アイン：ラナ、悪ぃ。一つ謝らせてくれ。");

                                 UpdateMainMessage("ラナ：何がよ？");

                                 UpdateMainMessage("アイン：実は、コレなんだが・・・");

                                 using (MessageDisplay md = new MessageDisplay())
                                 {
                                     md.StartPosition = FormStartPosition.CenterParent;
                                     md.Message = "アインは『ラナのイヤリング』をポケットから取り出した。";
                                     md.ShowDialog();
                                 }
                                 mc.DeleteBackPack(new ItemBackPack("ラナのイヤリング"));

                                 UpdateMainMessage("ラナ：っそ！ソレって！！");

                                 UpdateMainMessage("ハンナ：おやおや・・・ひょっとしてラナちゃんのアクセサリかい？");

                                 UpdateMainMessage("アイン：いや、いやいやいやいや！");

                                 UpdateMainMessage("アイン：った、たまたま俺の部屋に何故か転がってたんだって！");

                                 UpdateMainMessage("アイン：悪かった悪かった悪かった！っな！？");

                                 using (MessageDisplay md = new MessageDisplay())
                                 {
                                     md.StartPosition = FormStartPosition.CenterParent;
                                     md.Message = "ラナは驚きと戸惑いの表情を隠せないでいる・・・";
                                     md.ShowDialog();

                                     md.Message = "・・・　数秒後　・・・";
                                     md.ShowDialog();
                                 }

                                 UpdateMainMessage("ラナ：・・・　・・・　・・・っど・・・");

                                 UpdateMainMessage("アイン：怒髪天アルティメットブローとか勘弁な！？っな！？");

                                 UpdateMainMessage("ラナ：どっ・・・どうも、ありがと。");

                                 UpdateMainMessage("アイン：・・・ッハ？");

                                 UpdateMainMessage("ハンナ：アッハハハハハ。よかったじゃないか。");

                                 UpdateMainMessage("ハンナ：ホラ！こうなったら、アインも存分に謝るんだね。");

                                 UpdateMainMessage("アイン：っお、おお、悪かったな！いや、ホント悪かった！");

                                 UpdateMainMessage("ラナ：別に良いわよ。気にしてないから♪");

                                 UpdateMainMessage("アイン：っそ、そうか。なら良いんだが・・・とにかく悪かったな。");

                                 UpdateMainMessage("ラナ：良いって言ってるじゃない♪　済んだ事だし。");

                                 UpdateMainMessage("ラナ：ところで、コレどこに落ちてたのよ？");

                                 UpdateMainMessage("アイン：さっきも言ったと思うが、俺の部屋だ。");

                                 UpdateMainMessage("アイン：" + we.GameDay.ToString() + "日ぐらい前だったかな。");

                                 UpdateMainMessage("アイン：朝ふと起きるとさ、ベッドの横に転がってたんだよ。");

                                 UpdateMainMessage("ラナ：へえ、そんな所に落ちてたんだ。");

                                 UpdateMainMessage("アイン：そんな所って、じゃあどこに落ちてたと思ったんだよ？");

                                 UpdateMainMessage("ラナ：っべべべ、別に知らないわよ！！そんなの！！");

                                 UpdateMainMessage("アイン：うわわ、っ分かったって。そんなビビんなくて良いだろうが。");

                                 UpdateMainMessage("ラナ：っまったく・・・っあ、そうだ。もう一個聞いて良い？");

                                 UpdateMainMessage("アイン：ん？ああ、何個でも聞いてくれ。");

                                 UpdateMainMessage("ラナ：何で最初見つけたとき、渡してくれなかったの？");

                                 UpdateMainMessage("アイン：何て言ったら良いんだろうな。");

                                 UpdateMainMessage("アイン：よく考えてみたかったんだ。");

                                 UpdateMainMessage("ハンナ：・・・");

                                 UpdateMainMessage("アイン：ラナのイヤリング、最初見た時さ。");

                                 UpdateMainMessage("アイン：どことなくだが、よく理解できなかったんだよ。");

                                 UpdateMainMessage("アイン：部屋に落ちてたとか、そういう表面的な事じゃなく。");

                                 UpdateMainMessage("アイン：何でコレがあるんだっけ・・・");

                                 UpdateMainMessage("アイン：どうして落ちてるんだっけ・・・とか");

                                 UpdateMainMessage("アイン：そもそも何時からあったんだ・・・？とかな");

                                 UpdateMainMessage("アイン：何となくそういう所が、どうしても思い出せねえんだよ。");

                                 UpdateMainMessage("アイン：ラナに渡すと、何かこのもやもやした感じがすぐ消えてしまいそうでさ。");

                                 UpdateMainMessage("アイン：それでついつい、渡すのが遅れてしまった、ってワケさ。");

                                 UpdateMainMessage("ラナ：・・・何か・・・思い出せたの？");

                                 UpdateMainMessage("アイン：・・・　・・・　");

                                 UpdateMainMessage("アイン：ああ、思い出せたぜ。");
                             }

                             UpdateMainMessage("ハンナ：アイン、ラナちゃん。そろそろ店じまいだよ。");

                             UpdateMainMessage("ラナ：あ！そ、そうね。もうこんな時間じゃない。");

                             UpdateMainMessage("ラナ：ハンナおばさん、ごちそうさまでした♪");

                             UpdateMainMessage("ハンナ：アインもゆっくりと休むんだね。");

                             UpdateMainMessage("アイン：え？あ、あぁ、ありがとうございます。ごちそうさまでした！");

                             UpdateMainMessage("アイン：んじゃあ、また明日な。ラナ。");

                             UpdateMainMessage("ラナ：ええ、明日から２階ね。この調子で進みましょう。");

                             UpdateMainMessage("アイン：そうだな！じゃあ、またな！");

                             GroundOne.StopDungeonMusic();

                             ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_NIGHT);
                             using (MessageDisplay md = new MessageDisplay())
                             {
                                 md.StartPosition = FormStartPosition.CenterParent;
                                 md.Message = "アインが予約していた部屋にて";
                                 md.ShowDialog();
                             }

                             UpdateMainMessage("アイン：ふう・・・バックパック整理っと・・・");

                             UpdateMainMessage("アイン：明日から２階か・・・っしゃ！気合入れるぜ！！");

                             UpdateMainMessage("アイン：・・・　・・・");

                             UpdateMainMessage("アイン：・・・");

                             this.backgroundData = null;
                             this.BackColor = Color.Black;

                             if (GroundOne.WE2.TruthRecollection1)
                             {
                                 UpdateMainMessage("アイン：俺はダンジョンへ行こうとしていた。");

                                 UpdateMainMessage("アイン：師匠は無理だって言ってたが、俺には出来る気がしていた。");

                                 UpdateMainMessage("アイン：魔物討伐は楽々こなせていたし");

                                 UpdateMainMessage("アイン：DUELに関しても、かなり上位にランクイン出来ていた。");

                                 UpdateMainMessage("アイン：何より、自分自身がようやく強いと感じられるようになっていた。");

                                 UpdateMainMessage("アイン：ユング街のダンジョン。");

                                 UpdateMainMessage("アイン：いわゆる「神の試練」ってのが待ち構えているらしい");

                                 UpdateMainMessage("アイン：冗談じゃない。俺は「神」とかいう類が大嫌いだ。");

                                 UpdateMainMessage("アイン：神とか名の付く物には、決まってウラがある。");

                                 UpdateMainMessage("アイン：絶対に正体を暴いてやる。そして、");

                                 UpdateMainMessage("アイン：このダンジョンの最下層まで絶対に辿りついてみせる。");

                                 UpdateMainMessage("アイン：最初はそう考えていた。");

                                 UpdateMainMessage("アイン：１階までは何の苦労も無くクリアすることが出来ていた。");

                                 UpdateMainMessage("アイン：不安要素なんてのは一つも無かった。");

                                 UpdateMainMessage("アイン：強いて言えば、ラナと一緒にダンジョンへ向かう事になったぐらいだ。");

                                 UpdateMainMessage("アイン：ラナは普段の動き自体は良い。");

                                 UpdateMainMessage("アイン：ただ、ラナはいざと言う時に硬直してしまう場合がある。");

                                 UpdateMainMessage("アイン：まあそん時は、俺がとっさにカバーに入れば良いだけの話。大丈夫だ。");

                                 UpdateMainMessage("アイン：不安要素と呼ぶにはあまりにも小さすぎる不安だ。");

                                 UpdateMainMessage("アイン：ラナと俺は、今までも良く連携を組んでやってきている。");

                                 UpdateMainMessage("アイン：お互いの事は知り尽くしている。");

                                 UpdateMainMessage("アイン：１階ボスには若干手間取ったものの");

                                 UpdateMainMessage("アイン：ボスの威勢の良さが無くなるまではそんなに時間はかからなかった。");

                                 if (we.Truth_GiveLanaEarring)
                                 {
                                     UpdateMainMessage("アイン：・・・　確かそうだ。　思い出した。");

                                     UpdateMainMessage("アイン：ラナと一緒にダンジョンへ進んで・・・それから・・・");

                                     UpdateMainMessage("アイン：そうだ、ラナのイヤリングだ。");

                                     UpdateMainMessage("アイン：俺達はまだダンジョンの中にいる。");

                                     UpdateMainMessage("アイン：ダンジョン内で、ボスを倒した後、俺は確かに見てる。");

                                     UpdateMainMessage("アイン：ラナはあのイヤリングはあの時、まだ付けていた。");

                                     UpdateMainMessage("アイン：そもそも俺の部屋に放ってある代物じゃねえ。");

                                     UpdateMainMessage("アイン：ダンジョン内のどこかで無くした物だ。");

                                     UpdateMainMessage("アイン：それが俺の部屋にあるってのが考えられない。");

                                     UpdateMainMessage("アイン：自分で突っ込んでおいて、意味わかんねえけど・・・");

                                     UpdateMainMessage("アイン：俺とラナはダンジョンを進める途中で・・・");

                                     UpdateMainMessage("アイン：何か重大な失敗をおかした。");

                                     UpdateMainMessage("アイン：それも、取り返しのつかない事だ・・・");

                                     UpdateMainMessage("アイン：ッグ・・・駄目だ。ここがどうしても思い出せねえ・・・");
                                 }
                                 else
                                 {
                                     GroundOne.WE2.TruthKey1 = true; // これを真実世界へのキーその１とする。
                                 }
                             }

                             UpdateMainMessage("　　　【俺たちはその後、２階への階段を発見し】");

                             UpdateMainMessage("　　　【そのまま、２階へと足を運んだ】");

                             we.TruthCommunicationCompArea1 = true;
                         }

                         we.TruthCommunicationCompArea1 = true;
                         we.AlreadyRest = true;

                         using (ESCMenu esc = new ESCMenu())
                         {
                             esc.MC = this.MC;
                             esc.SC = this.SC;
                             esc.TC = this.TC;
                             esc.WE = this.we;
                             esc.KnownTileInfo = null;
                             esc.KnownTileInfo2 = null;
                             esc.KnownTileInfo3 = null;
                             esc.KnownTileInfo4 = null;
                             esc.KnownTileInfo5 = null;
                             esc.Truth_KnownTileInfo = this.Truth_KnownTileInfo;
                             esc.Truth_KnownTileInfo2 = this.Truth_KnownTileInfo2;
                             esc.Truth_KnownTileInfo3 = this.Truth_KnownTileInfo3;
                             esc.Truth_KnownTileInfo4 = this.Truth_KnownTileInfo4;
                             esc.Truth_KnownTileInfo5 = this.Truth_KnownTileInfo5;
                             esc.StartPosition = FormStartPosition.CenterParent;
                             esc.TruthStory = true;
                             esc.OnlySave = true;
                             esc.ShowDialog();
                         }

                         //this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                         ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                         this.BackColor = Color.WhiteSmoke;
                         this.Update();
                         UpdateMainMessage("", true);

                         SecondCommunicationStart();
                     }
                 }
                 #endregion
                #region "２階初日"
                 else if (this.we.TruthCompleteArea1 && this.we.TruthCommunicationCompArea1 && !we.Truth_CommunicationSecondHomeTown)
                 {
                     SecondCommunicationStart();
                 }
                #endregion
                #region "２階、地の部屋、選択失敗"
                if (we.dungeonEvent206 && !we.dungeonEvent207 && we.dungeonEvent207FailEvent2)
                {
                    we.dungeonEvent207FailEvent2 = false;
                    if (!we.dungeonEvent207FailEvent)
                    {
                        we.dungeonEvent207FailEvent = true;

                        UpdateMainMessage("アイン：ッゲ！町に戻ったじゃねえか！！");

                        UpdateMainMessage("ラナ：何かの強制転移装置みたいなものかしら。");

                        UpdateMainMessage("アイン：っわ、悪ぃな。しくじっちまったみたいだ。");

                        UpdateMainMessage("ラナ：ダンジョンゲート・・・入れないみたいよ。");

                        UpdateMainMessage("ラナ：今日は手仕舞いにするしかないみたいね。ハアァァァァ・・・");

                        UpdateMainMessage("アイン：くそっ・・・次はミスらないようにするぜ。");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：ッゲ！またやっちまった！！");

                        UpdateMainMessage("ラナ：ちょっと・・・「上」の意味分かってるわけ？");

                        UpdateMainMessage("アイン：あ、ああ。もちろんだ。悪ぃ悪い。");

                        UpdateMainMessage("ラナ：しっかりしてよね、バカアイン。ハアァァァァ・・・");

                        UpdateMainMessage("アイン：くそっ・・・今度こそ・・・");
                    }
                }
                else if (we.dungeonEvent208 && !we.dungeonEvent209 && we.dungeonEvent209FailEvent2)
                {
                    we.dungeonEvent209FailEvent2 = false;
                    if (!we.dungeonEvent209FailEvent)
                    {
                        we.dungeonEvent209FailEvent = true;

                        UpdateMainMessage("アイン：ッゲ！町に戻ったじゃねえか！！");

                        UpdateMainMessage("ラナ：何かの強制転移装置みたいなものかしら。");

                        UpdateMainMessage("アイン：っわ、悪ぃな。しくじっちまったみたいだ。");

                        UpdateMainMessage("ラナ：ダンジョンゲート・・・入れないみたいよ。");

                        UpdateMainMessage("ラナ：今日は手仕舞いにするしかないみたいね。ハアァァァァ・・・");

                        UpdateMainMessage("アイン：くそっ・・・次はミスらないようにするぜ。");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：ッゲ！またやっちまった！！");

                        UpdateMainMessage("ラナ：ちょっと・・・「下」の意味分かってるわけ？");

                        UpdateMainMessage("アイン：あ、ああ。もちろんだ。悪ぃ悪い。");

                        UpdateMainMessage("ラナ：しっかりしてよね、バカアイン。ハアァァァァ・・・");

                        UpdateMainMessage("アイン：くそっ・・・今度こそ・・・");
                    }
                }
                else if (we.dungeonEvent210 && !we.dungeonEvent211 && we.dungeonEvent211FailEvent2)
                {
                    we.dungeonEvent211FailEvent2 = false;
                    if (!we.dungeonEvent211FailEvent)
                    {
                        we.dungeonEvent211FailEvent = true;

                        UpdateMainMessage("アイン：ッゲ！町に戻ったじゃねえか！！");

                        UpdateMainMessage("ラナ：何かの強制転移装置みたいなものかしら。");

                        UpdateMainMessage("アイン：っわ、悪ぃな。しくじっちまったみたいだ。");

                        UpdateMainMessage("ラナ：ダンジョンゲート・・・入れないみたいよ。");

                        UpdateMainMessage("ラナ：今日は手仕舞いにするしかないみたいね。ハアァァァァ・・・");

                        UpdateMainMessage("アイン：くそっ・・・次はミスらないようにするぜ。");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：ッゲ！またやっちまった！！");

                        UpdateMainMessage("ラナ：ちょっと・・・「左」の意味分かってるわけ？");

                        UpdateMainMessage("アイン：あ、ああ。もちろんだ。悪ぃ悪い。");

                        UpdateMainMessage("ラナ：しっかりしてよね、バカアイン。ハアァァァァ・・・");

                        UpdateMainMessage("アイン：くそっ・・・今度こそ・・・");
                    }
                }
                #endregion
                #region "２階、神の試練クリア後"
                else if (GroundOne.WE2.TruthAnswerSuccess && we.dungeonEvent224 && !we.dungeonEvent225)
                {
                    we.dungeonEvent225 = true;

                    UpdateMainMessage("アイン：っしゃ、戻ってきたな。");

                    UpdateMainMessage("アイン：なあラナ。少しそこら辺、寄っていかないか？");

                    UpdateMainMessage("ラナ：え？うん。でもどこに行くのよ？");

                    UpdateMainMessage("アイン：『味商売　天地』でどうだ？");

                    UpdateMainMessage("ラナ：あの店味が濃くないかしら？　まあいいけど。");

                    UpdateMainMessage("アイン：じゃ、さっそく行くとするか！");

                    UpdateMainMessage("ラナ：まったく、あんな味のどこが良いのかしら・・・");

                    CallSomeMessageWithAnimation("    『味商売　天地』にて・・・");

                    UpdateMainMessage("アイン：ふ～、やっぱここの味は最高だぜ！ッハッハッハ！");

                    UpdateMainMessage("ラナ：私、あんまり好きじゃないんだけど・・・って、ちょっと聞いてる？");

                    UpdateMainMessage("アイン：ところで、あの詩だけどさ。ラナの母さんが作ったのか？");

                    UpdateMainMessage("ラナ：違うわよ。母さんもその先代から伝承されてきた詩だそうよ。");

                    UpdateMainMessage("アイン：先代？");

                    UpdateMainMessage("ラナ：そうよ、紫聡千律道場に代々伝えられてきている詩よ。");

                    UpdateMainMessage("ラナ：アインは小さい頃、たまたま私と一緒に稽古練習してたから。");

                    UpdateMainMessage("ラナ：それで偶然聞いてたのよね。小さい頃だから、覚えてないのが普通よ。");

                    UpdateMainMessage("アイン：なるほど・・・");

                    UpdateMainMessage("アイン：どおりで思い出せないわけだ。ッハハハ・・・");

                    UpdateMainMessage("アイン：でも、変だよな。");

                    UpdateMainMessage("ラナ：何が？");

                    UpdateMainMessage("アイン：このダンジョンでさ。");

                    UpdateMainMessage("アイン：なんでそんな事が起こりうるんだ？");

                    UpdateMainMessage("ラナ：・・・っえ・・・っと・・・");

                    using (TruthDecision td = new TruthDecision())
                    {
                        td.MainMessage = "　【　ラナにこのまま問い詰めてみますか？　】";
                        td.FirstMessage = "問い詰める。";
                        td.SecondMessage = "問い詰めず、話題を変える。";
                        td.StartPosition = FormStartPosition.CenterParent;
                        td.ShowDialog();
                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            UpdateMainMessage("アイン：ラナはこのダンジョンのからくり、どこまで把握してる？");

                            UpdateMainMessage("ラナ：っか、からくりって何の事よ？");

                            UpdateMainMessage("ラナ：知らないわよ、そんなの。");

                            UpdateMainMessage("アイン：何でも良い。知ってる事があれば教えてくれ。");

                            UpdateMainMessage("ラナ：・・・");

                            UpdateMainMessage("ラナ：知らないわ。本当よ。");

                            UpdateMainMessage("アイン：っそっか。。。");

                            UpdateMainMessage("アイン：悪いな。何か無理やり問い詰めちまって。");

                            UpdateMainMessage("ラナ：・・・ねえ、アイン。");

                            UpdateMainMessage("アイン：ん、何だ？");

                            UpdateMainMessage("ラナ：う～ん、何でもないわ。");

                            UpdateMainMessage("アイン：いや、良いんだ。悪かったな。");

                            UpdateMainMessage("ラナ：っあ、そうじゃないの。");

                            UpdateMainMessage("ラナ：ランディスのお師匠さんに、聞きにいってみない？");

                            UpdateMainMessage("アイン：あ？　あのボケ師匠にか？");

                            UpdateMainMessage("ラナ：アインが本当に困ってる時、私あんまり力になれてないみたいだし。");

                            UpdateMainMessage("ラナ：ランディスのお師匠さんなら、何か教えてくれそうじゃない？");

                            UpdateMainMessage("アイン：どうだろうなあ・・・");

                            UpdateMainMessage("アイン：ちょっとでも設問を間違えりゃ昇天だからな・・・ック・・・");

                            UpdateMainMessage("ラナ：まあ、無理にとは言わないけど。");

                            UpdateMainMessage("アイン：いいや、聞いてみるぜ。ビビってたってしょうがねえからな。");

                            UpdateMainMessage("アイン：いろいろとサンキューな、ラナ。");

                            UpdateMainMessage("ラナ：っふふ、別に良いわよ。大した事はやってないわ。");

                            UpdateMainMessage("ラナ：じゃあ、ちょっとランディスのお師匠さんの所に行ってみましょ♪");

                            UpdateMainMessage("アイン：ああ！");

                            we.dungeonEvent226 = true;
                        }
                        else
                        {
                            UpdateMainMessage("アイン：詩自体は、ラナも知ってたんだよな？");

                            UpdateMainMessage("ラナ：ええ、そうね。私も何度も聞いてたし、よく覚えてるわよ。");

                            UpdateMainMessage("アイン：あの詩、このダンジョンを解くためのヒントになるかも知れねえ。");

                            UpdateMainMessage("アイン：一応メモっといてくれるか？");

                            UpdateMainMessage("ラナ：うん、分かったわ♪");

                        }
                    }
                }
                #endregion
                #region "２階制覇"
                else if (this.we.TruthCompleteArea2 && !this.we.TruthCommunicationCompArea2)
                {
                    UpdateMainMessage("アイン：よおおぉぉし、到達到達！！");

                    UpdateMainMessage("ラナ：ッフフフ、クリアした後だし皆に知らせに行きましょ♪");

                    UpdateMainMessage("アイン：ああ、そうだな！");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "アインは一通り、町の住人達に声をかけ、時間が刻々と過ぎていった。";
                        md.ShowDialog();

                        md.Message = "その日の夜、ハンナの宿屋亭にて";
                        md.ShowDialog();
                    }

                    GroundOne.StopDungeonMusic();
                    GroundOne.PlayDungeonMusic(Database.BGM15, Database.BGM15LoopBegin);

                    // ８レバーが全てFalse、かつ
                    // 真実解の部屋へ到達していない場合、BADEND
                    if ((!GroundOne.WE2.TruthAnswer2_1) &&
                        (!GroundOne.WE2.TruthAnswer2_2) &&
                        (!GroundOne.WE2.TruthAnswer2_3) &&
                        (!GroundOne.WE2.TruthAnswer2_4) &&
                        (!GroundOne.WE2.TruthAnswer2_5) &&
                        (!GroundOne.WE2.TruthAnswer2_6) &&
                        (!GroundOne.WE2.TruthAnswer2_7) &&
                        (!GroundOne.WE2.TruthAnswer2_8) &&
                        (!GroundOne.WE2.TruthRecollection2))
                    {
                        if (!GroundOne.WE2.TruthAnswer2_OK)
                        {
                            UpdateMainMessage("アイン：しかし、妙なんだよな。");

                            UpdateMainMessage("ラナ：何がよ？");

                            UpdateMainMessage("アイン：結局さ。あのよく分からないレバーは何だったんだろうな。");

                            UpdateMainMessage("ラナ：触っても特に何も反応が無かったわよね。");

                            UpdateMainMessage("アイン：空中文字とか出ててさ。演出がすごかったわりに当たりが無かったよな。");

                            UpdateMainMessage("ラナ：次の階で何か影響が出るとかじゃないかしら？");

                            UpdateMainMessage("アイン：どうだろうな。そうだと良いんだが。");
                        }

                        UpdateMainMessage("ラナ：ハンナ叔母さん。豪華なごちそうありがとうございます♪");

                        UpdateMainMessage("ハンナ：はいよ、今日の夕飯はいつもより豪華にしておいたからね。");

                        UpdateMainMessage("ラナ：あ、ありがとうございます♪");

                        UpdateMainMessage("アイン：おぉぉ！これはスゲェ！いただきます！！");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "アインとラナが夕飯を食べた後・・・";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("アイン：ふう、もう食えないぜ。おばちゃん、ありがと！");

                        UpdateMainMessage("ハンナ：礼を言うなら、アンタのお師匠さんに言っておくんだね。");

                        UpdateMainMessage("アイン：っへ！？");

                        UpdateMainMessage("ハンナ：ああ見えて裏からコッソリ差し入れしてんだよ。");

                        UpdateMainMessage("アイン：ッマジかよ！？");

                        UpdateMainMessage("ハンナ：（声マネ）『ランディス：お祝いだぁ？クソくだらねぇ、勝手にやってろ』");

                        UpdateMainMessage("ハンナ：とか何とか行って即行でどっかに行っちまったよ。");

                        UpdateMainMessage("ラナ：フフフ、ランディスさんってやっぱり良い人じゃない。");

                        UpdateMainMessage("アイン：あの自分絶対史上主義のボケ師匠に限ってか・・・ハハハ");

                        UpdateMainMessage("ハンナ：礼とかはいいから、また殴られてくるんだね、アッハハハ");

                        UpdateMainMessage("アイン：ハ、ッハハハハ・・・");

                        UpdateMainMessage("ハンナ：アイン。ところで１階で思い出していた話について少し聞かせておくれ。");

                        UpdateMainMessage("アイン：っえ・・っと、１階ですか？");

                        GroundOne.StopDungeonMusic();

                        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_NIGHT);

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "アインが予約していた部屋にて";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("アイン：ふう・・・バックパック整理っと・・・");

                        UpdateMainMessage("アイン：３階か・・・ここからいよいよ難しくなるんだろうな。");

                        UpdateMainMessage("アイン：・・・　・・・");

                        UpdateMainMessage("アイン：・・・");

                        UpdateMainMessage("アイン：・・・　ハンナのおばさん、いきなり何だってんだろう・・・");

                        UpdateMainMessage("アイン：１階で思い出した話？");

                        UpdateMainMessage("アイン：２階を解いたこの時になって、いきなり何を・・・");

                        this.backgroundData = null;
                        this.BackColor = Color.Black;

                        UpdateMainMessage("アイン：・・・　何か忘れてる気がする　・・・");

                        UpdateMainMessage("アイン：俺、何やってたんだっけ・・・");

                        UpdateMainMessage("アイン：まあいいや、ひとまずダンジョンの最下層へ・・・");

                        UpdateMainMessage("アイン：・・・　・・・");

                        UpdateMainMessage("アイン：・・・");

                        UpdateMainMessage(" ～　THE　END　～　（虚構へ）");

                        GroundOne.WE2.TruthBadEnd2 = true;
                    }
                    // それ以外はGOOD
                    else
                    {
                        if (!GroundOne.WE2.TruthAnswer2_OK)
                        {
                            UpdateMainMessage("アイン：しかし、妙なんだよな。");

                            UpdateMainMessage("ラナ：何がよ？");

                            UpdateMainMessage("アイン：結局さ。あのよく分からないレバーは何だったんだろうな。");

                            UpdateMainMessage("ラナ：触っても特に何も反応が無かったわよね。");

                            UpdateMainMessage("アイン：空中文字とか出ててさ。演出がすごかったわりに当たりが無かったよな。");

                            UpdateMainMessage("ラナ：次の階で何か影響が出るとかじゃないかしら？");

                            UpdateMainMessage("アイン：どうだろうな。そうだと良いんだが。");
                        }

                        UpdateMainMessage("ラナ：ハンナ叔母さん。豪華なごちそうありがとうございます♪");

                        UpdateMainMessage("ハンナ：はいよ、今日の夕飯はいつもより豪華にしておいたからね。");

                        UpdateMainMessage("ラナ：あ、ありがとうございます♪");

                        UpdateMainMessage("アイン：おぉぉ！これはスゲェ！いただきます！！");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "アインとラナが夕飯を食べた後・・・";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("アイン：ふう、もう食えないぜ。おばちゃん、ありがと！");

                        UpdateMainMessage("ハンナ：礼を言うなら、アンタのお師匠さんに言っておくんだね。");

                        UpdateMainMessage("アイン：っへ！？");

                        UpdateMainMessage("ハンナ：ああ見えて裏からコッソリ差し入れしてんだよ。");

                        UpdateMainMessage("アイン：ッマジかよ！？");

                        UpdateMainMessage("ラナ：フフフ、ランディスさんってやっぱり良い人じゃない。");

                        UpdateMainMessage("アイン：あの自分絶対史上主義のボケ師匠に限ってか・・・ハハハ");

                        UpdateMainMessage("ハンナ：礼とかはいいから、また殴られてくるんだね、アッハハハ");

                        UpdateMainMessage("アイン：ハ、ッハハハハ・・・");

                        UpdateMainMessage("ハンナ：アイン。ところで１階で思い出していた話について少し聞かせておくれ。");

                        UpdateMainMessage("アイン：っえ・・っと、１階ですか？");

                        UpdateMainMessage("アイン：ええっと、あれ？　何だったっけ、ラナ！？");

                        UpdateMainMessage("ラナ：ちょっと何でソコで私に振ってんのよ。　自分で思い出しなさいよね。");

                        UpdateMainMessage("アイン：あれ・・・えっと、何だっけ？　アレ？");

                        UpdateMainMessage("アイン：・・・　・・・");

                        UpdateMainMessage("アイン：おぉ！そうだ！ラナのイヤリングだ！！");

                        UpdateMainMessage("アイン：悪い！あれは本当に悪かった！！");

                        UpdateMainMessage("ラナ：ッフフフ、それはもう良いって言ったじゃない♪");

                        UpdateMainMessage("ラナ：その後で思い出した事よ。何を思い出せたのか話してくれない？");

                        UpdateMainMessage("アイン：その後・・・？");

                        UpdateMainMessage("アイン：ああ、オーケーオーケー");

                        UpdateMainMessage("アイン：・・・　・・・");

                        UpdateMainMessage("アイン：ラナのイヤリングだが");

                        UpdateMainMessage("アイン：あれはラナと俺が２階へ降りる所で、ラナが落としたモノだ。");

                        UpdateMainMessage("アイン：それを俺が拾って");

                        UpdateMainMessage("アイン：ラナ、お前に渡した。");

                        UpdateMainMessage("アイン：自分で言ってて頭がおかしくなりそうだが");

                        UpdateMainMessage("アイン：これは確かな記憶だ。");

                        UpdateMainMessage("アイン：間違いはねえ。");

                        UpdateMainMessage("アイン：そうだ、だからこそ");

                        UpdateMainMessage("アイン：１階を制覇した時点でラナに渡そうと思ったんだ。");

                        UpdateMainMessage("ハンナ：よくやったね。アイン。");

                        UpdateMainMessage("ハンナ：正解だよ。");

                        UpdateMainMessage("アイン：おばちゃんはやっぱり知ってるんだな、全てを。");

                        UpdateMainMessage("ハンナ：わたしゃ本当に何も知らないよ。");

                        UpdateMainMessage("ハンナ：私はねアイン、あんたの手助けをしてるだけだよ。");

                        UpdateMainMessage("アイン：ん、まあそうなんだろうけどさ。");

                        UpdateMainMessage("アイン：ラナ、悪かったな本当に。");

                        UpdateMainMessage("ラナ：何で謝ってんのよ、別に悪い事してるわけじゃないんだし♪");

                        UpdateMainMessage("アイン：いや、いやいや。こういうことはもっと早めに・・・");

                        UpdateMainMessage("ラナ：っさて、私はもう部屋に戻ろうかな♪");

                        UpdateMainMessage("ラナ：おばさん、おやすみなさい♪");

                        UpdateMainMessage("ハンナ：あいよ、おやすみ。　ゆっくり休むんだよ。");

                        UpdateMainMessage("アイン：ふう・・・お祝いのハズだったのに、悪い事しちまったかな。");

                        UpdateMainMessage("ハンナ：アッハハハ、何言ってんだい。アンタは本当にバカだね。");

                        UpdateMainMessage("ハンナ：ッホラ、今日はもうゆっくり休むんだね、明日に備えて。");

                        UpdateMainMessage("アイン：あ、ああぁ。");

                        UpdateMainMessage("アイン：じゃあ今日はおばちゃん、いろいろサンキューな！");

                        UpdateMainMessage("ハンナ：あんまり考えこむんじゃないよ、寝れなくなるからね？");

                        UpdateMainMessage("アイン：ああ、分かってるって。じゃあおやすみなさい！");

                        GroundOne.StopDungeonMusic();

                        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_NIGHT);

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "アインが予約していた部屋にて";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("アイン：ふう・・・バックパック整理っと・・・");

                        UpdateMainMessage("アイン：３階か・・・ここからいよいよ難しくなるんだろうな。");

                        UpdateMainMessage("アイン：・・・　・・・");

                        this.backgroundData = null;
                        this.BackColor = Color.Black;

                        if (GroundOne.WE2.TruthRecollection1)
                        {
                            UpdateMainMessage("アイン：ラナにイヤリングを渡したのは良いが。");

                            UpdateMainMessage("アイン：あれだけなら、俺は致命的な現象に遭遇することは無かった。");

                            UpdateMainMessage("アイン：神の試練とやらは、簡単だった。");

                            UpdateMainMessage("アイン：『　神々の詩「海と大地、そして天空」　』");

                            UpdateMainMessage("アイン：ラナの母さんから良く聞かされていたヤツだ。");

                            UpdateMainMessage("アイン：２階を解いている間はハッキリ思い出せなかったが確かそうだ。");

                            UpdateMainMessage("アイン：ただ単にそれを声にして発するだけ。");

                            UpdateMainMessage("アイン：難しくも何とも無かった。これの何処が試練なのか俺には理解が及ばなかった。");

                            UpdateMainMessage("アイン：そして、どうしても思い出せないのが・・・");

                            UpdateMainMessage("アイン：『ヴェルゼ・アーティ』の存在。");

                            UpdateMainMessage("アイン：伝説のFiveSeeker、技の達人ってのは知ってる。");

                            UpdateMainMessage("アイン：俺はダンジョンゲート裏で確か遭遇してる。");

                            UpdateMainMessage("アイン：彼はこう言っていた。");

                            UpdateMainMessage("　　　『ヴェルゼ：アイン君、はじめまして。』");

                            UpdateMainMessage("　　　『本名はVerze Artieって言うんだ。よろしくね。』　　");

                            UpdateMainMessage("アイン：ッハハハ・・・そういえばそうだ。");

                            UpdateMainMessage("アイン：今思い出した事がある。");

                            UpdateMainMessage("アイン：そもそもだ。");

                            UpdateMainMessage("アイン：「はじめまして」　どころの騒ぎじゃねえ。");

                            UpdateMainMessage("アイン：はじめまして、なワケがねえんだ。");

                            UpdateMainMessage("アイン：俺は・・・この人の事を・・・");

                            UpdateMainMessage("アイン：もっとずっと前から知ってる。");

                            GroundOne.WE2.TruthKey2 = true; // これを真実世界へのキーその２とする。
                        }

                        UpdateMainMessage("　　　【俺はこの物語の真相を知ってるのかも知れない。】");

                        UpdateMainMessage("　　　【そんな奇妙な錯覚を覚えつつ、３階へと足を進めた。】");

                        we.TruthCommunicationCompArea2 = true;
                    }

                    we.TruthCommunicationCompArea2 = true;
                    CallRestInn(true);
                    
                    using (ESCMenu esc = new ESCMenu())
                    {
                        esc.MC = this.MC;
                        esc.SC = this.SC;
                        esc.TC = this.TC;
                        esc.WE = this.we;
                        esc.KnownTileInfo = null;
                        esc.KnownTileInfo2 = null;
                        esc.KnownTileInfo3 = null;
                        esc.KnownTileInfo4 = null;
                        esc.KnownTileInfo5 = null;
                        esc.Truth_KnownTileInfo = this.Truth_KnownTileInfo;
                        esc.Truth_KnownTileInfo2 = this.Truth_KnownTileInfo2;
                        esc.Truth_KnownTileInfo3 = this.Truth_KnownTileInfo3;
                        esc.Truth_KnownTileInfo4 = this.Truth_KnownTileInfo4;
                        esc.Truth_KnownTileInfo5 = this.Truth_KnownTileInfo5;
                        esc.StartPosition = FormStartPosition.CenterParent;
                        esc.TruthStory = true;
                        esc.OnlySave = true;
                        esc.ShowDialog();
                    }

                    //this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                    this.BackColor = Color.WhiteSmoke;
                    this.Update();
                    UpdateMainMessage("", true);
                    ThirdCommunicationStart();
                }
                #endregion
                // ダンジョンから帰還後、必須イベント以外のオプションイベントとして優先
                #region "看板「くまなく探せ」を見たとき"
                else if (this.firstDay >= 1 && we.BoardInfo14 &&
                         we.Truth_CommunicationJoinPartyLana == false && we.AvailableSecondCharacter == false)
                {
                    UpdateMainMessage("アイン：ラナ、｛くまなく｝って意味を教えてくれ。");

                    UpdateMainMessage("ラナ：アインはくまなくバカ♪");

                    UpdateMainMessage("アイン：待て待て。意味を教えてくれって言ってるだろ？");

                    UpdateMainMessage("ラナ：言葉どおりよ、アインは隅々まで余すとこなくバカって意味よ♪");

                    UpdateMainMessage("アイン：なるほどな・・・なるほど・・・");

                    UpdateMainMessage("ラナ：なるほどって・・・納得されても困るんだけど。");

                    UpdateMainMessage("アイン：また、看板があったんだ。内容はこうだ。");

                    UpdateMainMessage("『くまなく、探したか？』　っと。");

                    UpdateMainMessage("ラナ：なるほど。隅々まで探してみたか？と言われてるみたいね。");

                    UpdateMainMessage("ラナ：っで、隅々まで探してみたわけ？");

                    if (!we.BeforeSpecialInfo1)
                    {
                        UpdateMainMessage("アイン：ああ！もちろん全部探したぜ！");

                        UpdateMainMessage("アイン：それも、くまなく隅々まで全部な！ッハッハッハ！！");

                        UpdateMainMessage("ラナ：言葉ダブってるわよ。ホンットバカよね。ハアアァァァ・・・");

                        UpdateMainMessage("ラナ：ねえ、こう何か見落としとかあるんじゃないの？ちゃんと探してみた？");

                        if (!we.TruthSpecialInfo1)
                        {
                            UpdateMainMessage("アイン：ん？まあ隅々まで全部って言われてもな。");

                            UpdateMainMessage("アイン：そもそも、最下層制覇が目的だろ？");

                            UpdateMainMessage("ラナ：でも最下層に行くための必要な情報があるかも知れないわよ？");

                            UpdateMainMessage("アイン：ラナ、何でまたお前は、そう分かったような言い方をしてるんだ。");

                            UpdateMainMessage("アイン：おまえ・・・ひょっとして何か知ってるのか？");

                            UpdateMainMessage("ラナ：っべ、っべべべ別に知らないわよ！！");

                            UpdateMainMessage("アイン：分かった、分かったって。そんな動揺すんなって。");

                            UpdateMainMessage("アイン：まあ、気が向いたらいろいろ探索してみるとするさ。");

                            UpdateMainMessage("ラナ：看板にも書いてある事だし、探索して損はしないはずよ。");

                            UpdateMainMessage("アイン：オーケー、気になる所はまた探索するさ。サンキューな。", true);

                            // [真エンディング分岐]

                            UpdateMainMessage("アイン：・・・なあ、ラナ。");

                            UpdateMainMessage("ラナ：なによ？");

                            UpdateMainMessage("アイン：俺だけじゃ、看板の意図が掴めねえ時もある、そこでだ。");

                            UpdateMainMessage("アイン：ダンジョン、一緒に来ないか？");

                            UpdateMainMessage("ラナ：う～ん、どうしようかな。");

                            UpdateMainMessage("アイン：お前が居ると頼りになるからな。っな、頼むぜ。");

                            //UpdateMainMessage("　　　【ラナは一瞬、アインには見えないように、遠くを見るような笑顔をした後・・・】");
                            
                            UpdateMainMessage("　　　【　ラナはちょっと考え込むそぶりで、遠くを見るような笑顔をした　】");

                            UpdateMainMessage("　　　【　それは一瞬のことであり、アインにとってその表情は分からなかった　】");

                            UpdateMainMessage("ラナ：条件があるわね。聞いてもらえるかしら♪");

                            UpdateMainMessage("アイン：っお、何だよ？言ってみろよ。");

                            UpdateMainMessage("ラナ：【　真実の答え　】　　探してよね？");

                            UpdateMainMessage("アイン：っな。何だって？");

                            UpdateMainMessage("ラナ：ッフフ、冗談よ。冗談♪　じゃあ、明日からは私も行くわよ♪");

                            UpdateMainMessage("アイン：サンキュー！恩にきるぜ！！　ッハッハッハ！");

                            if (we.AvailablePotionshop)
                            {
                                UpdateMainMessage("アイン：っと、そういえばラナ。お前のお店はどうするんだよ？");

                                UpdateMainMessage("ラナ：ご心配なく♪　ちゃんと雇っておいたから♪");

                                UpdateMainMessage("アイン：ッマジかよ！？何でそんな用意周到なんだよ！？");

                                UpdateMainMessage("ラナ：まあ、私、接客はあんまり向いてないのよね。何か疲れちゃうし。");

                                UpdateMainMessage("ラナ：そんなわけだから、心配ご無用よ♪");
                            }

                            UpdateMainMessage("アイン：じゃあ、明日からよろしく頼むぜ！！");

                            UpdateMainMessage("ラナ：ハイハイ♪　じゃあまた明日ね。");

                            CallSomeMessageWithAnimation("【ラナがパーティに加わりました。】");

                            we.AvailableSecondCharacter = true;
                            we.Truth_CommunicationJoinPartyLana = true;
                        }
                        else
                        {
                            UpdateMainMessage("アイン：【力は力にあらず、力は全てである。】");

                            UpdateMainMessage("ラナ：っえ！？");

                            UpdateMainMessage("アイン：それから・・・【負けられない勝負。　しかし心は満たず。】");

                            UpdateMainMessage("アイン：最後は　【力のみに依存するな。心を対にせよ。】　だったかな。");

                            UpdateMainMessage("ラナ：ウソ！？そんなのちゃんと覚えてるの！？");

                            UpdateMainMessage("アイン：覚えてるというか、思い出した。何かダンジョン内でそんな言葉が出てきたな。");

                            UpdateMainMessage("アイン：ラナの母ちゃんがやってた紫聡千律道場。あそこの十訓の一つだろ。");

                            UpdateMainMessage("アイン：俺はとくにあの７番目が好きだったしな。");

                            UpdateMainMessage("ラナ：私はよく分かんないけどね、ああいう類のだけは。");

                            UpdateMainMessage("ラナ：アインを殴ると、アインが吹っ飛ぶ。これで十分よね♪");

                            UpdateMainMessage("アイン：お前のそういう所は何とか治らねえのかよ・・・でもまあ");

                            UpdateMainMessage("　　　（アインはいつになく、真剣な眼差しを見せ始め・・・）");

                            UpdateMainMessage("アイン：このダンジョン。少し読めたぜ。");

                            UpdateMainMessage("ラナ：え？");

                            UpdateMainMessage("アイン：下手に進んだら駄目なんだ。このダンジョン。");

                            UpdateMainMessage("ラナ：どういう意味よ？");

                            UpdateMainMessage("アイン：この言葉で思い出したことがある。");

                            UpdateMainMessage("ラナ：何を思い出したの？");

                            UpdateMainMessage("アイン：神剣フェルトゥーシュに関してだ。");

                            UpdateMainMessage("　　　（ラナはほんの一瞬だけ、顔を横に逸らしてから・・・）");

                            UpdateMainMessage("ラナ：フェルトゥーシュがどうしたのよ？");

                            UpdateMainMessage("アイン：突き刺された者、純粋な力による死を迎える");

                            UpdateMainMessage("アイン：ヒーリング効果が適用されず、蘇生魔法も効かない。");

                            UpdateMainMessage("アイン：まさに純粋な力そのものだ。");

                            UpdateMainMessage("アイン：だが、俺が思い出したのはそんな事じゃねえ。");

                            UpdateMainMessage("アイン：ラナ、お前が俺に最初にくれた剣。あれが、フェルトゥーシュだろ？。");

                            UpdateMainMessage("ラナ：・・・いつ頃から気づいてたのよ？");

                            UpdateMainMessage("アイン：ボケ師匠ランディスに出くわした時だ。");

                            UpdateMainMessage("ラナ：そうだったの。それからは、気づかない振りしてたの？");

                            UpdateMainMessage("アイン：いや、そういうわけじゃねえ。半信半疑だったってのが正直な所だ。");

                            UpdateMainMessage("アイン：あの剣は、どうみても単なるナマクラだ。実際使ってみても全然威力が出ないしな。");

                            UpdateMainMessage("ラナ：ふうん。それでお師匠さんに会ってからどう変わったのよ？");

                            UpdateMainMessage("アイン：師匠はどうもあの剣の特性に関して、もう一つ何か知ってるみたいなんだ。");

                            UpdateMainMessage("アイン：いや、あの剣に関わらず、全般的な話みたいだった。それを教えてくれた。");

                            UpdateMainMessage("アイン：心を燈して放たないと、攻撃力は出ない。何かそんな話だった。");

                            UpdateMainMessage("ラナ：心を燈して・・・って事は。");

                            UpdateMainMessage("アイン：あの剣、最高攻撃力が異常に高い。そして、最低攻撃力も異常に低い。");

                            UpdateMainMessage("アイン：心を燈さない限り、最高攻撃力は出ない。つまり、ナマクラなままってわけだ。");

                            UpdateMainMessage("アイン：それが分かった時点で、俺の力に対する考えは変わった。");

                            UpdateMainMessage("アイン：あの十訓の７番目。あの言葉通り、力は必要だが、力だけじゃ駄目だって事さ。");

                            UpdateMainMessage("ラナ：ねえ、アイン");

                            UpdateMainMessage("アイン：ん？");

                            UpdateMainMessage("ラナ：ダンジョン、このまま進められる？");

                            UpdateMainMessage("アイン：・・・ああ。俺はこのまま進める。");

                            UpdateMainMessage("アイン：俺はどうやら、いろいろと忘れてしまってるようだ。");

                            UpdateMainMessage("アイン：それを思い出さなきゃならねえ。");

                            UpdateMainMessage("アイン：ダンジョンをくまなく探索すれば、思い出すべき事が見つかる。");

                            UpdateMainMessage("アイン：このダンジョン、どうやら何か他の解き方があるみたいだ。");

                            UpdateMainMessage("アイン：俺はそれを見つけてみせる。必ずな。");

                            UpdateMainMessage("ラナ：っそ。何か安心しちゃったわ。");

                            UpdateMainMessage("ラナ：アイン、１階制覇のほう、頑張って来てよね♪");

                            UpdateMainMessage("アイン：おお、任せておけ。１階制覇できたら、連絡するからな。");

                            UpdateMainMessage("ラナ：頼んだわよ。１階制覇の時は、お宝どっさり持ってきてもらうから♪");

                            UpdateMainMessage("アイン：マジかよ。お宝没収かよ・・・、ラナ様にお貢物ってワケかよ。");

                            UpdateMainMessage("ラナ：っふふふ、ウソよウソ。何まじめに受けちゃってるのよ♪");

                            UpdateMainMessage("アイン：まあ、何か良いものあったら持ってくるよ。");

                            UpdateMainMessage("アイン：じゃあ、１階制覇！やってくるとするか！");

                            UpdateMainMessage("ラナ：楽しみにしてるわよ。じゃあ、また明日ね。");

                            UpdateMainMessage("アイン：ああ、またな。");

                            we.CompleteTruth1 = true;
                        }
                    }
                    else
                    {
                        UpdateMainMessage("アイン：【力は力にあらず、力は全てである。】");

                        UpdateMainMessage("ラナ：っえ！？");

                        UpdateMainMessage("アイン：それから・・・【負けられない勝負。　しかし心は満たず。】");

                        UpdateMainMessage("アイン：最後は　【力のみに依存するな。心を対にせよ。】　だったかな。");

                        UpdateMainMessage("ラナ：ウソ！？そんなのちゃんと覚えてるの！？");

                        UpdateMainMessage("アイン：覚えてるというか、思い出した。何かダンジョン内でそんな言葉が出てきたな。");

                        UpdateMainMessage("アイン：ラナの母ちゃんがやってた紫聡千律道場。あそこの十訓の一つだろ。");

                        UpdateMainMessage("アイン：俺はとくにあの７番目が好きだったしな。");

                        UpdateMainMessage("ラナ：私はよく分かんないけどね、ああいう類のだけは。");

                        UpdateMainMessage("ラナ：アインを殴ると、アインが吹っ飛ぶ。これで十分よね♪");

                        UpdateMainMessage("アイン：お前のそういう所は何とか治らねえのかよ・・・でもまあ");

                        UpdateMainMessage("　　　（アインはいつになく、真剣な眼差しを見せ始め・・・）");

                        UpdateMainMessage("アイン：このダンジョン。少し読めたぜ。");

                        UpdateMainMessage("ラナ：え？");

                        UpdateMainMessage("アイン：下手に進んだら駄目なんだ。このダンジョン。");

                        UpdateMainMessage("ラナ：どういう意味よ？");

                        UpdateMainMessage("アイン：この言葉で思い出したことがある。");

                        UpdateMainMessage("ラナ：何を思い出したの？");

                        UpdateMainMessage("アイン：神剣フェルトゥーシュに関してだ。");

                        UpdateMainMessage("　　　（ラナはほんの一瞬だけ、顔を横に逸らしてから・・・）");

                        UpdateMainMessage("ラナ：フェルトゥーシュがどうしたのよ？");

                        UpdateMainMessage("アイン：突き刺された者、純粋な力による死を迎える");

                        UpdateMainMessage("アイン：ヒーリング効果が適用されず、蘇生魔法も効かない。");

                        UpdateMainMessage("アイン：まさに純粋な力そのものだ。");

                        UpdateMainMessage("アイン：だが、俺が思い出したのはそんな事じゃねえ。");

                        UpdateMainMessage("アイン：ラナ、お前が俺に最初にくれた剣。あれが、フェルトゥーシュだろ？。");

                        UpdateMainMessage("ラナ：・・・いつ頃から気づいてたのよ？");

                        UpdateMainMessage("アイン：ボケ師匠ランディスに出くわした時だ。");

                        UpdateMainMessage("ラナ：そうだったの。それからは、気づかない振りしてたの？");

                        UpdateMainMessage("アイン：いや、そういうわけじゃねえ。半信半疑だったってのが正直な所だ。");

                        UpdateMainMessage("アイン：あの剣は、どうみても単なるナマクラだ。実際使ってみても全然威力が出ないしな。");

                        UpdateMainMessage("ラナ：ふうん。それでお師匠さんに会ってからどう変わったのよ？");

                        UpdateMainMessage("アイン：師匠はどうもあの剣の特性に関して、もう一つ何かを知ってるみたいなんだ。");

                        UpdateMainMessage("アイン：いや、あの剣に関わらず、全般的な話みたいだった。それを教えてくれた。");

                        UpdateMainMessage("アイン：心を燈して放たないと、攻撃力は出ない。何かそんな話だった。");

                        UpdateMainMessage("ラナ：心を燈して・・・って事は。");

                        UpdateMainMessage("アイン：あの剣、最高攻撃力が異常に高い。そして、最低攻撃力も異常に低い。");

                        UpdateMainMessage("アイン：心を燈さない限り、最高攻撃力は出ない。つまり、ナマクラなままってわけだ。");

                        UpdateMainMessage("アイン：それが分かった時点で、俺の力に対する考えは変わった。");

                        UpdateMainMessage("アイン：あの十訓の７番目。あの言葉通り、力は必要だが、力だけじゃ駄目だって事さ。");

                        UpdateMainMessage("ラナ：ねえ、アイン");

                        UpdateMainMessage("アイン：ん？");

                        UpdateMainMessage("ラナ：ダンジョン、このまま進められる？");

                        UpdateMainMessage("アイン：・・・ああ。俺はこのまま進める。");

                        UpdateMainMessage("アイン：俺はどうやら、いろいろと忘れてしまってるようだ。");

                        UpdateMainMessage("アイン：それを思い出さなきゃならねえ。");

                        UpdateMainMessage("アイン：ダンジョンをくまなく探索すれば、思い出すべき事が見つかる。");

                        UpdateMainMessage("アイン：このダンジョン、どうやら何か他の解き方があるみたいだ。");

                        UpdateMainMessage("アイン：俺はそれを見つけてみせる。必ずな。");

                        UpdateMainMessage("ラナ：っそ。何か安心しちゃったわ。");

                        UpdateMainMessage("ラナ：アイン、１階制覇のほう、頑張って来てよね♪");

                        UpdateMainMessage("アイン：おお、任せておけ。１階制覇できたら、連絡するからな。");

                        UpdateMainMessage("ラナ：頼んだわよ。１階制覇の時は、お宝どっさり持ってきてもらうから♪");

                        UpdateMainMessage("アイン：マジかよ。お宝没収かよ・・・、ラナ様にお貢物ってワケかよ。");

                        UpdateMainMessage("ラナ：っふふふ、ウソよウソ。何まじめに受けちゃってるのよ♪");

                        UpdateMainMessage("アイン：まあ、何か良いものあったら持ってくるよ。");

                        UpdateMainMessage("アイン：じゃあ、１階制覇！やってくるとするか！");

                        UpdateMainMessage("ラナ：楽しみにしてるわよ。じゃあ、また明日ね。");

                        UpdateMainMessage("アイン：ああ、またな。");

                        we.CompleteTruth1 = true;
                        // 固定メンバーでストーリ１本かどうか・・・どうする！？
                    }

                    we.AlreadyCommunicate = true;
                    return;
                }
                #endregion
                #region "看板「始まりの地」を見たとき"
                else if (this.firstDay >= 1 &&
                    we.BoardInfo10 &&
                    we.Truth_CommunicationJoinPartyLana == false &&
                    we.Truth_CommunicationNotJoinLana == false &&
                    we.AvailableSecondCharacter == false)
                {
                    UpdateMainMessage("アイン：ラナ、お前｛べからず｝って意味知ってるか？");

                    UpdateMainMessage("ラナ：何よ、突然そんなこと聞いて。");

                    UpdateMainMessage("ラナ：「～してはいけない。」って事。つまり、やっちゃいけないって事じゃないの？");

                    UpdateMainMessage("アイン：そうか・・・となると・・・");

                    UpdateMainMessage("アイン：いやしかし・・・どういう意味だ・・・");

                    UpdateMainMessage("ラナ：っちょ、なに考えちゃってるのよ。ちゃんと言いなさいよね？");

                    UpdateMainMessage("アイン：っあ、ああ、看板があったんだよ。");

                    UpdateMainMessage("『始まりの地、見落とすべからず。』　ってな。");

                    UpdateMainMessage("ラナ：言葉通りじゃない。「始まりの場所を見落とさないようにしなさい」って意味よ。");

                    UpdateMainMessage("アイン：よくわかんねえんだよな。これが。");

                    UpdateMainMessage("ラナ：何かひっかかりでもあるわけ？");

                    UpdateMainMessage("アイン：いや、特にねえけどさ。");

                    UpdateMainMessage("ラナ：じゃあ何よ？");

                    UpdateMainMessage("アイン：うーん、なんて言うんだ。捉え所が掴みにくいと思ってさ。");

                    UpdateMainMessage("ラナ：まあ１階の始めなんだし、冒険者への最初の警告って所じゃないの？");

                    UpdateMainMessage("アイン：うーん・・・なんて言うんだ・・・");

                    UpdateMainMessage("ラナ：・・・ッハイ、ポーションでもどうぞ♪");

                    GetGreenPotionForLana();

                    UpdateMainMessage("アイン：っお！？悪いな、わざわざ。代金はいくらだ？");

                    UpdateMainMessage("ラナ：良いわよ、そんなの。とっときなさいよ。");

                    UpdateMainMessage("アイン：いやいや、すまねえな。サンキュー！");

                    if (GroundOne.WE2.TruthBadEnd1)
                    {
                        UpdateMainMessage("アイン：・・・なあ、ラナ。ところで話は変わるんだが。");

                        UpdateMainMessage("ラナ：なによ？");

                        using (TruthDecision td = new TruthDecision())
                        {
                            td.MainMessage = "　【　ラナをパーティに誘いますか？　】";
                            td.FirstMessage = "ラナをパーティに誘う。";
                            td.SecondMessage = "ラナをパーティに誘わない。";
                            td.StartPosition = FormStartPosition.CenterParent;
                            td.ShowDialog();
                            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                            {
                                UpdateMainMessage("アイン：ダンジョン、一緒に来ないか？");

                                UpdateMainMessage("ラナ：う～ん、どうしようかな。");

                                UpdateMainMessage("アイン：お前が居るとポーションとかダンジョンマップが頼りになるからな。っな、頼むぜ。");

                                UpdateMainMessage("　　　【ラナは一瞬、アインには見えないように、遠くを見るような笑顔をした後・・・】");

                                UpdateMainMessage("ラナ：条件があるわね。聞いてもらえるかしら♪");

                                UpdateMainMessage("アイン：っお、何だよ？言ってみろよ。");

                                UpdateMainMessage("ラナ：【　真実の答え　】　　探してよね？");

                                UpdateMainMessage("アイン：っな。何だって？");

                                UpdateMainMessage("ラナ：ッフフ、冗談よ。冗談♪　じゃあ、明日からは私も行くわよ♪");

                                UpdateMainMessage("アイン：サンキュー！恩にきるぜ！！　ッハッハッハ！");

                                if (we.AvailablePotionshop)
                                {
                                    UpdateMainMessage("アイン：っと、そういえばラナ。お前のお店はどうするんだよ？");

                                    UpdateMainMessage("ラナ：ご心配なく♪　ちゃんと雇っておいたから♪");

                                    UpdateMainMessage("アイン：ッマジかよ！？何でそんな用意周到なんだよ！？");

                                    UpdateMainMessage("ラナ：まあ、私、接客はあんまり向いてないのよね。何か疲れちゃうし。");

                                    UpdateMainMessage("ラナ：そんなわけだから、心配ご無用よ♪");
                                }

                                UpdateMainMessage("アイン：じゃあ、明日からよろしく頼むぜ！！");

                                UpdateMainMessage("ラナ：ハイハイ♪　じゃあまた明日ね。");

                                CallSomeMessageWithAnimation("【ラナがパーティに加わりました。】");

                                we.AvailableSecondCharacter = true;
                                we.Truth_CommunicationJoinPartyLana = true;
                            }
                            else
                            {
                                UpdateMainMessage("アイン：い、いやいや、何でもねえ。");

                                UpdateMainMessage("ラナ：アイン、らしくないわね。言いたい事は正直に言ってよね？");

                                UpdateMainMessage("アイン：・・・ああ、正直言うとだな。");

                                UpdateMainMessage("アイン：ラナ、お前をパーティに誘おうと思ったんだが。");

                                UpdateMainMessage("アイン：やめた。俺一人で行ってみせるぜ。");

                                UpdateMainMessage("アイン：すまねえな。ラナ。");

                                UpdateMainMessage("ラナ：う～ん、別に良いわよ。ちゃんとそう言ってくれれば。");

                                UpdateMainMessage("アイン：じゃあ、また明日からダンジョンいって来るぜ。");

                                UpdateMainMessage("ラナ：ハイハイ、頑張ってきてよね♪");

                                CallSomeMessageWithAnimation("【ラナをパーティに加えませんでした。】");

                                we.Truth_CommunicationNotJoinLana = true;
                            }
                        }
                    }
                    else
                    {
                        UpdateMainMessage("アイン：・・・なあ、ラナ。ポーションもらったトコ悪いんだが。");

                        UpdateMainMessage("ラナ：なによ？");

                        UpdateMainMessage("アイン：ダンジョン、一緒に来ないか？");

                        UpdateMainMessage("ラナ：う～ん、どうしようかな。");

                        UpdateMainMessage("アイン：お前が居るとポーションとかダンジョンマップが頼りになるからな。っな、頼むぜ。");

                        UpdateMainMessage("　　　【ラナは一瞬、アインには見えないように、遠くを見るような笑顔をした後・・・】");

                        UpdateMainMessage("ラナ：条件があるわね。聞いてもらえるかしら♪");

                        UpdateMainMessage("アイン：っお、何だよ？言ってみろよ。");

                        UpdateMainMessage("ラナ：【　真実の答え　】　　探してよね？");

                        UpdateMainMessage("アイン：っな。何だって？");

                        UpdateMainMessage("ラナ：ッフフ、冗談よ。冗談♪　じゃあ、明日からは私も行くわよ♪");

                        UpdateMainMessage("アイン：サンキュー！恩にきるぜ！！　ッハッハッハ！");

                        if (we.AvailablePotionshop)
                        {
                            UpdateMainMessage("アイン：っと、そういえばラナ。お前のお店はどうするんだよ？");

                            UpdateMainMessage("ラナ：ご心配なく♪　ちゃんと雇っておいたから♪");

                            UpdateMainMessage("アイン：ッマジかよ！？何でそんな用意周到なんだよ！？");

                            UpdateMainMessage("ラナ：まあ、私、接客はあんまり向いてないのよね。何か疲れちゃうし。");

                            UpdateMainMessage("ラナ：そんなわけだから、心配ご無用よ♪");
                        }

                        UpdateMainMessage("アイン：じゃあ、明日からよろしく頼むぜ！！");

                        UpdateMainMessage("ラナ：ハイハイ♪　じゃあまた明日ね。");

                        CallSomeMessageWithAnimation("【ラナがパーティに加わりました。】");

                        we.AvailableSecondCharacter = true;
                        we.Truth_CommunicationJoinPartyLana = true;
                    }
                    return;
                }
                #endregion
                #region "看板「近道危険」を見たとき"
                //else if (this.firstDay >= 1 && we.BoardInfo11 &&
                //        we.Truth_CommunicationJoinPartyLana == false && we.AvailableSecondCharacter == false)
                //{
                //    UpdateMainMessage("アイン：近道には危険が潜む・・・そりゃそうだろうな・・・");

                //    UpdateMainMessage("ラナ：どうしたのよ？難しい顔して一人でブツブツ");

                //    UpdateMainMessage("アイン：おお、ラナか。ちょうど良かった。");

                //    UpdateMainMessage("アイン：ラナ、一緒にダンジョンこねえか？");

                //    UpdateMainMessage("ラナ：・・・え？");

                //    UpdateMainMessage("アイン：どうやら、あのダンジョンは近道が存在するらしいんだ。");

                //    UpdateMainMessage("ラナ：っちょ、ちょっと待ってよ。");

                //    UpdateMainMessage("アイン：・・・なんだ？");

                //    UpdateMainMessage("ラナ：唐突よね。いきなりどうしちゃったわけ？");

                //    UpdateMainMessage("アイン：いや、別に深い経緯はねえが・・・");

                //    UpdateMainMessage("アイン：なんだ、駄目なのか？");

                //    UpdateMainMessage("ラナ：う～ん、そういうわけじゃないけど・・・");

                //    UpdateMainMessage("アイン：行こうぜ、ラナ。２人で行くのにデメリットは無いだろ。");

                //    UpdateMainMessage("ラナ：まあそうだけどね。私、本当に行っても良いの？");

                //    // [真エンディング分岐]

                //    UpdateMainMessage("アイン：ああ、当然さ。お前が居ると頼りになるからな。っな、頼むぜ。");

                //    UpdateMainMessage("　　　【ラナは一瞬、アインには見えないように、遠くを見るような笑顔をした後・・・】");

                //    UpdateMainMessage("ラナ：条件があるわね。聞いてもらえるかしら♪");

                //    UpdateMainMessage("アイン：っお、何だよ？言ってみろよ。");

                //    UpdateMainMessage("ラナ：【　真実の答え　】　　探してよね？");

                //    UpdateMainMessage("アイン：っな。何だって？");

                //    UpdateMainMessage("ラナ：ッフフ、冗談よ。冗談♪　じゃあ、明日からは私も行くわよ♪");

                //    UpdateMainMessage("アイン：サンキュー！恩にきるぜ！！　ッハッハッハ！");

                //    if (we.AvailablePotionshop)
                //    {
                //        UpdateMainMessage("アイン：っと、そういえばラナ。お前のお店はどうするんだよ？");

                //        UpdateMainMessage("ラナ：ご心配なく♪　ちゃんと雇っておいたから♪");

                //        UpdateMainMessage("アイン：ッマジかよ！？何でそんな用意周到なんだよ！？");

                //        UpdateMainMessage("ラナ：まあ、私、接客はあんまり向いてないのよね。何か疲れちゃうし。");

                //        UpdateMainMessage("ラナ：そんなわけだから、心配ご無用よ♪");
                //    }

                //    UpdateMainMessage("アイン：じゃあ、明日からよろしく頼むぜ！！");

                //    UpdateMainMessage("ラナ：ハイハイ♪　じゃあまた明日ね。");

                //    CallSomeMessageWithAnimation("【ラナがパーティに加わりました。】");

                //    we.AvailableSecondCharacter = true;
                //    we.Truth_CommunicationJoinPartyLana = true;
                //}
                #endregion
                #region "看板３を見る前でも、大広間に到達した時"
                if ((we.dungeonEvent11KeyOpen || we.dungeonEvent12KeyOpen || we.dungeonEvent13KeyOpen || we.dungeonEvent14KeyOpen) &&
                    we.Truth_CommunicationJoinPartyLana == false && we.AvailableSecondCharacter == false)
                {
                    UpdateMainMessage("アイン：ふう、戻ってきたのは良いが・・・");

                    UpdateMainMessage("アイン：あの大広間、扉だらけで分かったもんじゃねえな。");

                    UpdateMainMessage("アイン：っくそ・・・こんな時にラナのダンジョンマップがあればな・・・");

                    UpdateMainMessage("ラナ：私のダンジョンマップがどうかしたの？");

                    UpdateMainMessage("アイン：うお！？　ビックリするじゃねえか！");

                    UpdateMainMessage("ラナ：何そんなビビってるわけ？まさか、また隠し事じゃないでしょうね♪");

                    UpdateMainMessage("アイン：い、いやいや。隠してるわけじゃねえ。");

                    UpdateMainMessage("アイン：でもまあ、そんなわけだ。気にするな！ッハッハッハ！");

                    UpdateMainMessage("ラナ：ふうん・・・っじゃ、こっちも内緒で♪");

                    UpdateMainMessage("アイン：っな！何が内緒なんだよ！？");

                    UpdateMainMessage("ラナ：ダンジョンマップがどうとか、言ってたじゃない？ちゃんと言いなさいよね。");

                    UpdateMainMessage("アイン：・・・　・・・");

                    UpdateMainMessage("アイン：・・・　・・・　まあ、アレだ。");

                    UpdateMainMessage("　　　『ッシュゴオオォォ！！』（ラナのエレメンタルキックがアインの胴体に炸裂）　　");

                    UpdateMainMessage("アイン：わわ、分かったって。待て待て。");

                    UpdateMainMessage("アイン：ダンジョンを進めてる途中、大きな大広間に出くわしたんだ。");

                    UpdateMainMessage("ラナ：へえ、そんな所があるんだ。っで、どうだったわけ？");

                    UpdateMainMessage("アイン：大広間には幾つもの扉があるんだが、これがほとんど鍵付きばかり。");

                    UpdateMainMessage("アイン：おそらく、違う道筋をたどってくれば、開けるとは思うんだが・・・");

                    UpdateMainMessage("アイン：何せ、マップがよくわかんねえ。");

                    UpdateMainMessage("アイン：簡単に言えば、マップがよくわかんねえ。");

                    UpdateMainMessage("アイン：結論として、マップがよくわかんねえ。");

                    UpdateMainMessage("ラナ：なるほど♪　私、良い事思いついちゃった♪");

                    UpdateMainMessage("アイン：ラナ、別にお前に来てくれとは言ってねえ。");

                    UpdateMainMessage("アイン：マップを書いてくれさえすれば良いんだ。");

                    UpdateMainMessage("ラナ：でも、ダンジョンに一緒に行かないとマップは書けないでしょ？");

                    UpdateMainMessage("アイン：いいや、来なくても良い。俺がトランシーバーでラナと通信を行う。");

                    UpdateMainMessage("アイン：『こちらアイン。ただいま座標ポイント『２２，３３』");

                    UpdateMainMessage("アイン：（声マネ）『こちらラナ。了解よ♪　マップ更新しといたわ』");

                    UpdateMainMessage("アイン：『こちらアイン。ただいま座標ポイント『３４，２２』　宝箱を発見！");

                    UpdateMainMessage("アイン：（声マネ）『こちらラナ。了解よ♪　マップ更新しといたわ』");

                    UpdateMainMessage("アイン：『こちらアイン・・・ただいま・・・』");

                    UpdateMainMessage("ラナ：・・・変な裏声出さないでよね。それ、全然似てないから。");

                    UpdateMainMessage("ラナ：そもそも何でそんな面倒な通信作業しなきゃ行けないのよ。");

                    UpdateMainMessage("アイン：っほら、こう何かいかにもダンジョン探索を進めてるって感じがするだろ？");

                    UpdateMainMessage("ラナ：そもそもトランシーバーなんてもの、ダンジョンで使えるわけ無いでしょ？");

                    UpdateMainMessage("アイン：ッグ！　っば、そんな馬鹿な！！");

                    UpdateMainMessage("ラナ：はあぁぁ・・・・何でこんなバカ話してるのかしら・・・私、行くわね。");

                    // [真エンディング分岐]

                    UpdateMainMessage("アイン：ま、待て待て！相談だ、ラナ！");

                    UpdateMainMessage("アイン：一緒にダンジョン行かねえか？");

                    UpdateMainMessage("　　　【ラナは一瞬、アインには見えないように、遠くを見るような笑顔をした後・・・】");

                    UpdateMainMessage("ラナ：条件があるわね。聞いてもらえるかしら♪");

                    UpdateMainMessage("アイン：っお、何だよ？言ってみろよ。");

                    UpdateMainMessage("ラナ：【　真実の答え　】　　探してよね？");

                    UpdateMainMessage("アイン：っな。何だって？");

                    UpdateMainMessage("ラナ：ッフフ、冗談よ。冗談♪　じゃあ、明日からは私も行くわよ♪");

                    UpdateMainMessage("アイン：サンキュー！恩にきるぜ！！　ッハッハッハ！");

                    if (we.AvailablePotionshop)
                    {
                        UpdateMainMessage("アイン：っと、そういえばラナ。お前のお店はどうするんだよ？");

                        UpdateMainMessage("ラナ：ご心配なく♪　ちゃんと雇っておいたから♪");

                        UpdateMainMessage("アイン：ッマジかよ！？何でそんな用意周到なんだよ！？");

                        UpdateMainMessage("ラナ：まあ、私、接客はあんまり向いてないのよね。何か疲れちゃうし。");

                        UpdateMainMessage("ラナ：そんなわけだから、心配ご無用よ♪");
                    }

                    UpdateMainMessage("アイン：じゃあ、明日からよろしく頼むぜ！！");

                    UpdateMainMessage("ラナ：ハイハイ♪　じゃあまた明日ね。");

                    CallSomeMessageWithAnimation("【ラナがパーティに加わりました。】");

                    we.AvailableSecondCharacter = true;
                    we.Truth_CommunicationJoinPartyLana = true;
                }
                #endregion
                #region "看板「メンバー構成で変化」を見たとき"
                else if ((we.BoardInfo13) && we.Truth_CommunicationJoinPartyLana == false && we.AvailableSecondCharacter == false)
                {
                    UpdateMainMessage("アイン：パーティによってメンバー構成は・・・変化する・・・");

                    UpdateMainMessage("ラナ：どうしたのよ？難しい顔して一人でブツブツ");

                    UpdateMainMessage("アイン：おお、ラナか。ちょうど良かった。");

                    UpdateMainMessage("アイン：ラナ、一緒にダンジョンこねえか？");

                    UpdateMainMessage("ラナ：・・・え？");

                    UpdateMainMessage("アイン：どうやら、あのダンジョンはメンバー構成によって変化するらしいんだ。");

                    UpdateMainMessage("ラナ：っちょ、ちょっと待ってよ。");

                    UpdateMainMessage("アイン：・・・なんだ？");

                    UpdateMainMessage("ラナ：唐突よね。いきなりどうしちゃったわけ？");

                    UpdateMainMessage("アイン：いや、別に深い経緯はねえが・・・");

                    UpdateMainMessage("アイン：なんだ、駄目なのか？");

                    UpdateMainMessage("ラナ：う～ん、そういうわけじゃないけど・・・");

                    UpdateMainMessage("アイン：行こうぜ、ラナ。２人で行くのにデメリットは無いだろ。");

                    UpdateMainMessage("ラナ：まあそうだけどね。私、本当に行っても良いの？");

                    // [真エンディング分岐]

                    UpdateMainMessage("アイン：ああ、当然さ。お前が居ると頼りになるからな。っな、頼むぜ。");

                    UpdateMainMessage("　　　【ラナは一瞬、アインには見えないように、遠くを見るような笑顔をした後・・・】");

                    UpdateMainMessage("ラナ：条件があるわね。聞いてもらえるかしら♪");

                    UpdateMainMessage("アイン：っお、何だよ？言ってみろよ。");

                    UpdateMainMessage("ラナ：【　真実の答え　】　　探してよね？");

                    UpdateMainMessage("アイン：っな。何だって？");

                    UpdateMainMessage("ラナ：ッフフ、冗談よ。冗談♪　じゃあ、明日からは私も行くわよ♪");

                    UpdateMainMessage("アイン：サンキュー！恩にきるぜ！！　ッハッハッハ！");

                    if (we.AvailablePotionshop)
                    {
                        UpdateMainMessage("アイン：っと、そういえばラナ。お前のお店はどうするんだよ？");

                        UpdateMainMessage("ラナ：ご心配なく♪　ちゃんと雇っておいたから♪");

                        UpdateMainMessage("アイン：ッマジかよ！？何でそんな用意周到なんだよ！？");

                        UpdateMainMessage("ラナ：まあ、私、接客はあんまり向いてないのよね。何か疲れちゃうし。");

                        UpdateMainMessage("ラナ：そんなわけだから、心配ご無用よ♪");
                    }

                    UpdateMainMessage("アイン：じゃあ、明日からよろしく頼むぜ！！");

                    UpdateMainMessage("ラナ：ハイハイ♪　じゃあまた明日ね。");

                    CallSomeMessageWithAnimation("【ラナがパーティに加わりました。】");

                    we.AvailableSecondCharacter = true;
                    we.Truth_CommunicationJoinPartyLana = true;

                }
                #endregion
                #region "DUEL闘技場開催"
                else if (this.firstDay >= 3 && !we.AvailableDuelColosseum)
                {
                    UpdateMainMessage("ラナ：っあ、アイン。こんな所に居たのね。");

                    UpdateMainMessage("アイン：ようラナ、何のようだ？");

                    UpdateMainMessage("ラナ：アインはDUEL闘技場には参加しないの？");

                    UpdateMainMessage("アイン：DUEL闘技か、あんま参加しようって思った事はねえな。");

                    UpdateMainMessage("ラナ：ふ～ん、そうなの？");

                    UpdateMainMessage("アイン：アレはなんっつうんだ。DUELだろ？");

                    UpdateMainMessage("ラナ：そうよ。");

                    UpdateMainMessage("アイン：DUELっつったら、DUELだろ？");

                    UpdateMainMessage("　　　『ッバグシ！』（ラナのエレメンタルキックが炸裂）　　");

                    UpdateMainMessage("アイン：おぉぉぉぉ・・・ッグ、分かった分かった！");

                    UpdateMainMessage("アイン：っで、俺に出ろとでも言いたいのか？");

                    UpdateMainMessage("ラナ：何で積極的に出たがらないのかを聞いてるのよ。");

                    UpdateMainMessage("アイン：そうだな。DUELってのはいわゆる真剣勝負だ。");

                    UpdateMainMessage("ラナ：ダンジョン行ってる時は真剣勝負じゃないワケ？");

                    UpdateMainMessage("アイン：別にそうは言ってねえ。だが、DUELとはまた少し別だ。");

                    UpdateMainMessage("アイン：ダンジョンのモンスターは適当にぶっ潰せば良いだけだろ？");

                    UpdateMainMessage("アイン：だが、DUELは明らかに相手はモンスターじゃねえ。人間だ。");

                    UpdateMainMessage("アイン：適当にあしらうのもなんだし、マジでぶっ潰すのもなんだ。");

                    UpdateMainMessage("アイン：真剣に面と向かってやってやんなきゃ申し訳が立たねえだろ。");

                    UpdateMainMessage("ラナ：う～ん・・・何だかよく分かんないわね。");

                    UpdateMainMessage("ラナ：やっぱり、一度ちゃんと参加してみれば？");

                    UpdateMainMessage("アイン：まいったな・・・どうすっかな・・・");

                    UpdateMainMessage("アイン：一つ、条件がある。飲んでくれるか？");

                    UpdateMainMessage("ラナ：っえ？ソレ、私に対して言ってるの？");

                    UpdateMainMessage("アイン：ああ、そうだ。");

                    UpdateMainMessage("ラナ：っそ、そんなの内容次第よ。じゃあ、言ってみなさいよ。");

                    UpdateMainMessage("アイン：俺が勝った直後とか、DUEL前後では出来る限り俺の周囲から離れてくれ。良いな？");

                    UpdateMainMessage("ラナ：っえ？　何よそれ？");

                    UpdateMainMessage("アイン：この条件、飲んでくれればDUELに参加してみるぜ。どうだ？");

                    UpdateMainMessage("ラナ：う～ん、アインってさ。たまに良く分からない事言うわね・・・");

                    UpdateMainMessage("ラナ：まあ、でもそんな内容だったら。了解よ♪");

                    UpdateMainMessage("アイン：っしゃ！決まりだ！！腕が鳴るぜ！！！");

                    UpdateMainMessage("アイン：申し込みとかの登録申請、早速やってくるとするか！ッハッハッハ！！");

                    CallSomeMessageWithAnimation("アインはDUEL闘技場へと向かっていった。");

                    UpdateMainMessage("ラナ：（アイン・・・あんな嬉しそうに、はしゃいで・・・）");

                    UpdateMainMessage("ラナ：（・・・　・・・）");

                    buttonDuel.Visible = true;

                    CallSomeMessageWithAnimation("【DUEL闘技場へ行く事が出来るようになりました。】");

                    GroundOne.StopDungeonMusic();
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    CallSomeMessageWithAnimation("－－－　DUEL闘技場にて　－－－");

                    UpdateMainMessage("アイン：うお！すげえ歓声だな！！");

                    UpdateMainMessage("ラナ：ちょうど対戦が始まった所なんじゃない？");

                    UpdateMainMessage("アイン：ああ、そうみたいだな。ちょっと見ていくか？");

                    UpdateMainMessage("ラナ：う～ん、私は良いわ、遠慮しとく。アイン登録申請に来たんじゃないの？");

                    UpdateMainMessage("アイン：うぉっと！そうだった、忘れてたぜ！！");

                    UpdateMainMessage("アイン：っしゃ、早速受け付けにでも行ってみるとするか。");

                    UpdateMainMessage("　　【受付嬢：ようこそ、DUEL闘技場へ。】");

                    UpdateMainMessage("アイン：っとだな、DUEL参加申し込みをしたいんだが。");

                    UpdateMainMessage("　　【受付嬢：DUEL申請でしたら、こちら登録シートに記入をお願いします。】");

                    UpdateMainMessage("アイン：『名前』っと。っよし・・・Ein・・・Wolence・・っと。");

                    UpdateMainMessage("アイン：『現在までのDUEL申し込み回数』・・・確か、３シーズンっと。");

                    UpdateMainMessage("アイン：『主戦術』？何だこりゃ。。。");

                    UpdateMainMessage("アイン：「アタック！！」");

                    UpdateMainMessage("ラナ：ちょっと、アイン。「アタック」なんて戦術でも何でもないわよ？");

                    UpdateMainMessage("アイン：良いじゃねえか。テキトーで良いんだよ、こんなもんは。");

                    UpdateMainMessage("アイン：『魔法習得度』？・・・そうだな、「１００％」っと。。。");

                    UpdateMainMessage("ラナ：ジィ～～・・・");

                    UpdateMainMessage("アイン：わかった、分かったって。「３０％」っと。。。");

                    UpdateMainMessage("アイン：『二刀流可否』？・・・あんまり得意じゃねえが、一応『可』っと。。。");

                    UpdateMainMessage("アイン：『スタックキャンセル可否』？・・・まあ『可』っかな。");

                    UpdateMainMessage("ラナ：何よそれ？");

                    UpdateMainMessage("アイン：ん？ああ、今度また教えてやるよ。次々っと・・・『ライバル』");

                    UpdateMainMessage("アイン：・・・そうだな『オル・ランディス』っと・・・");

                    UpdateMainMessage("ラナ：ランディスお師匠さんの名前じゃない。書いてもいいわけ？");

                    UpdateMainMessage("アイン：大丈夫だろ。単なるアンケートみたいなもんだろうし。っふう、最後か。");

                    UpdateMainMessage("アイン：『優勝したら？』。。。そうだなあ・・・");

                    UpdateMainMessage("「ッハッハッハ！！！」っとこんなもんか");

                    UpdateMainMessage("ラナ：ホンット、あきれるぐらいテキトーよね。");

                    UpdateMainMessage("アイン：まあまあ、良いじゃねえか。よし、ホラよ。これで全部記入したぜ。");

                    UpdateMainMessage("　　【受付嬢：登録シートを受け付けました。】");

                    UpdateMainMessage("　　【受付嬢：データベースに照合・適用を実施します。】");

                    UpdateMainMessage("　　【受付嬢：照合判定結果は明日となりますので、明日から対戦登録表に正式エントリーされます。】");

                    UpdateMainMessage("　　【受付嬢：対戦相手は対象の腕や力量に応じて本闘技場より自動的にピックアップいたします。】");

                    UpdateMainMessage("　　【受付嬢：ピックアップされたリスト内の相手と対戦を行ってください。】");

                    UpdateMainMessage("　　【受付嬢：対戦は原則として、キャンセル・拒否は行えません。必ず本闘技場で競っていただきます。】");

                    UpdateMainMessage("アイン：それでも相手や俺が無理やり拒否ったらどうなるんだ？");

                    UpdateMainMessage("　　【受付嬢：必ず対戦相手とＤＵＥＬされるよう手筈を整えます。】");

                    UpdateMainMessage("アイン：それでも相手が断ったらどうなるんだ？");

                    UpdateMainMessage("　　【受付嬢：必ず対戦相手とＤＵＥＬされるよう手筈を整えます。】");

                    UpdateMainMessage("アイン：マジかよ・・・まあいいか。他に詳細ルールはあるのか？");

                    UpdateMainMessage("　　【受付嬢：詳しいＤＵＥＬルールに関しては、データベース適用が終わり次第お伝えいたします。】");

                    UpdateMainMessage("　　【受付嬢：以上となります。明日の連絡をお待ちください。】");

                    UpdateMainMessage("アイン：ああ、いろいろとありがとな。サンキュー！");

                    UpdateMainMessage("アイン：今日は登録までか。まあ続きは明日って事で。そうだ、ラナ。");

                    UpdateMainMessage("ラナ：何よ？");

                    UpdateMainMessage("アイン：ラナ、お前も参加してみないか？");

                    UpdateMainMessage("ラナ：ええ！？私！？　イイわよそんなの。どうせすぐ負けちゃうし。");

                    UpdateMainMessage("アイン：何言ってんだ。あの無慈悲なライトニングキックなら大概の相手はその場で果てるぞ？");

                    UpdateMainMessage("ラナ：い、いい、いきなり知らない人に対して、あんなキックかませられないわよ。");

                    UpdateMainMessage("アイン：んまあ、いいか。っじゃ！明日からはDUELも頑張ってくるとするか！！");

                    UpdateMainMessage("ラナ：頑張って来てよね。期待してるわよ♪");

                    UpdateMainMessage("アイン：ああ、あのクソ師匠にもいつか勝利してみせるぜ！任せておけって！ッハッハッハ！！");

                    UpdateMainMessage("ラナ：私、薬素材の集めとかあるから、じゃあまた後で♪");

                    UpdateMainMessage("アイン：ああ、またな。");

                    CallSomeMessageWithAnimation("ラナは町の中へと歩いていった・・・");

                    UpdateMainMessage("アイン：（ダンジョンともう一つ、ＤＵＥＬか。。。）");

                    UpdateMainMessage("アイン：（ＤＵＥＬ・・・懐かしい感じがするな。。。）");

                    UpdateMainMessage("アイン：（っしゃ、明日からも頑張って行くとするか！）", true);

                    GroundOne.StopDungeonMusic();
                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
                    we.AvailableDuelColosseum = true;
                }
                #endregion
                #region "DUEL闘技場、DUEL開始"
                else if (this.firstDay >= 4 && !we.AvailableDuelMatch)
                {
                    UpdateMainMessage("　　【受付嬢：アイン様、お待ちしておりました。】");

                    UpdateMainMessage("アイン：よお、あの時の受付さんじゃないか！登録申請はどうなった？");

                    UpdateMainMessage("　　【受付嬢：アイン様の登録申請はデータベースへと照合され、正式に承諾されました。】");

                    UpdateMainMessage("アイン：おっしゃ！わざわざ教えに来てくれてサンキュー！");

                    UpdateMainMessage("　　【受付嬢：本日から、アイン様はＤＵＥＬ闘技場での対戦者リストに登録された事となります。】");

                    UpdateMainMessage("　　【受付嬢：近々予定されている対戦相手リストを確認したい場合は、ＤＵＥＬ闘技場までお越しください。】");

                    UpdateMainMessage("アイン：ああ、後で行ってみるとするわ。ありがとな！");

                    UpdateMainMessage("　　【受付嬢：なお、アイン様が「ライバル」欄にオル・ランディスを記載されていたため】");

                    UpdateMainMessage("アイン：あ？あぁ・・・確かに書いたが・・・");

                    UpdateMainMessage("　　【受付嬢：本闘技場のトップランカー、オル様より一言お伝えしておきたい内容があるとの事です】");

                    UpdateMainMessage("アイン：ッゲ！！！　マジかよ！？！？");

                    UpdateMainMessage("　　【受付嬢：それでは、本闘技場へとぜひともお越しください。私はこれにて。】");

                    CallSomeMessageWithAnimation("受付係員は闘技場へと戻っていった・・・");

                    UpdateMainMessage("アイン：ッグ・・・ヤ、ヤベェ・・・");

                    UpdateMainMessage("アイン：ックソ、何だっていきなり来てんだよ。。。");

                    UpdateMainMessage("アイン：逃げても・・・おそらく無駄だろうな。");

                    UpdateMainMessage("アイン：ここは闘技場へ行くしかないか。", true);

                    we.AvailableDuelMatch = true;
                }
                #endregion
                // ダンジョンから帰還後、必須イベントが無ければ、以下任意イベント
                #region "ESCメニュー：バトル設定"
                else if (!we.AvailableBattleSettingMenu && this.mc.Level >= 4)
                {
                    UpdateMainMessage("アイン：ストレートスマッシュに・・・それから・・・フレッシュヒール・・・");

                    UpdateMainMessage("ラナ：何そんな所で練習してるのよ？");

                    UpdateMainMessage("アイン：ああ、何となく思い出したのを体に慣れさせようと思ってだな。");

                    UpdateMainMessage("アイン：しかし、どうすっかな。");

                    UpdateMainMessage("ラナ：やけに考え込んでるわね。相談ならいつでも乗るわよ。");

                    UpdateMainMessage("アイン：おお、悪いな。ちょっとこういう話なんだが・・・");

                    UpdateMainMessage("　　　【アインの下手な説明が、ラナへ展開中・・・】");

                    UpdateMainMessage("ラナ：ッダメ！っぜんっっっぜん分かんない！！");

                    UpdateMainMessage("ラナ：バカアインの話って全然脈略が無いし、どこがポイントなのよ！？");

                    UpdateMainMessage("アイン：だからさっきから言ってるじゃねえか、この連続性が大事なんだって。");

                    UpdateMainMessage("ラナ：っちょ、もうそういう抽象的な話は結構よ。");

                    UpdateMainMessage("ラナ：アインの話、かいつまんで話すとこういうことよね？");

                    UpdateMainMessage("ラナ：『１．ＥＳＣメニューを開く』");

                    UpdateMainMessage("ラナ：『２．新しく追加されている【バトル設定】を選択する』");

                    UpdateMainMessage("ラナ：『３．現在習得してる魔法・スキル構成をバトルコマンドに設定する』");

                    UpdateMainMessage("ラナ：っでしょ？");

                    UpdateMainMessage("アイン：いや、それはそうなんだが、そういう話をしてんじゃねえ。");

                    UpdateMainMessage("アイン：コマンドの順序、そもそもバトルに関する根本的な理解がいまひとつだな。");

                    UpdateMainMessage("ラナ：今は良いでしょ。そんな話は後でいくらでも出てくるわよ。");

                    UpdateMainMessage("ラナ：とりあえず覚えた魔法・スキルをパパっと設定しちゃいなさいよ。");

                    UpdateMainMessage("ラナ：ホンット、どーでもいい部分でバカアインは凝り出すわね。");

                    UpdateMainMessage("アイン：まあいいじゃねえか。最初の内にやっておくに越した事はねえ。");

                    UpdateMainMessage("アイン：っしゃ、さっそくやってみるぜ！");

                    CallSomeMessageWithAnimation("【ESCメニューより「バトル設定」が選択できるようになりました。】");

                    CallSomeMessageWithAnimation("【習得した魔法・スキルをバトルコマンドに設定できるようになります。】");

                    we.AvailableBattleSettingMenu = true;
                }
                #endregion
                #region "戦闘：インスタントアクション"
                else if (!we.AvailableInstantCommand && this.mc.Level >= 6)
                {
                    UpdateMainMessage("アイン：この前は、確かこんな感じでやってた気がするんだが・・・");

                    UpdateMainMessage("ラナ：何か難しそうな顔してるわね。何か思いついたわけ？");

                    UpdateMainMessage("アイン：ん～いや、以前師匠に教わったヤツなんだけどな。");

                    UpdateMainMessage("ラナ：ランディスのお師匠さん？");

                    UpdateMainMessage("アイン：ああ、そうだ。");

                    UpdateMainMessage("アイン：インスタントアクションっていう行動らしいが。");

                    UpdateMainMessage("アイン：簡単に言うと・・・");

                    UpdateMainMessage("アイン：インスタントアクションだ！！");

                    UpdateMainMessage("ラナ：言い換えも出来てないじゃない・・・");

                    UpdateMainMessage("ラナ：まあそれは良いとして、出来そうなの？");

                    UpdateMainMessage("アイン：ああ、もうちょいのハズだ。まあ見ててくれよ。");

                    UpdateMainMessage("　【　アインはストレート・スマッシュの体勢に入った　】");

                    UpdateMainMessage("アイン：ッファイア！！");

                    UpdateMainMessage("ラナ：っえ！？");

                    UpdateMainMessage("　【　アインはダミー素振り君にファイア・ボールを放った！】");

                    UpdateMainMessage("アイン：よっしゃ！完璧だろ？ッハッハッハ！！");

                    UpdateMainMessage("ラナ：っお・・・驚いたわ。良くこんなの出来るわね？");

                    UpdateMainMessage("アイン：理屈は簡単だ。ラナ、お前にもたぶん出来る内容だぜ。");

                    UpdateMainMessage("アイン：要は、最初っからファイア・ボールを放つようにしとけばいいのさ。");

                    UpdateMainMessage("ラナ：見た目の素振りだけをストレート・スマッシュにしてたって事？");

                    UpdateMainMessage("アイン：いや、ストレート・スマッシュの体勢からは、ストレート・スマッシュは可能だ。");

                    UpdateMainMessage("ラナ：・・・私にも出来るのかしら・・・");

                    UpdateMainMessage("アイン：大丈夫だって。やってみろって。");

                    UpdateMainMessage("　【　ラナは通常攻撃の体勢に入った　】");

                    UpdateMainMessage("ラナ：う～ん・・・っと、こうかしら。ッハイ！");

                    UpdateMainMessage("　【　ラナはアイスニードルをダミー素振り君に放った！】");

                    UpdateMainMessage("アイン：・・・そんな感じだな！出来たじゃねえか！　ッハッハッハ！！");

                    UpdateMainMessage("ラナ：う～ん、アインのとは少し違う気がしたんだけど。");

                    UpdateMainMessage("アイン：このやり方さえ出来てれば、戦闘スタイルもかなり幅が拡がるぜ。");

                    UpdateMainMessage("ラナ：まあ確かに通常の戦闘コマンドに加えて、この行動が出来るのは嬉しいわね♪");

                    UpdateMainMessage("アイン：楽しみになってきたな！っしゃ、もういっちょ練習しておくぜ！ッハッハッハ！");

                    CallSomeMessageWithAnimation("【戦闘中にインスタントアクションが出来るようになりました。】");

                    CallSomeMessageWithAnimation("【戦闘中、アクションコマンドを右クリックする事で使用可能になります。】");

                    we.AvailableInstantCommand = true;
                }
                #endregion
             }
        }
        private void CallSomeMessageWithAnimation(string message)
        {
            using (MessageDisplay md = new MessageDisplay())
            {
                md.StartPosition = FormStartPosition.Manual;
                md.Location = new Point(this.Location.X, this.Location.Y + this.Size.Height);
                md.NeedAnimation = true;
                md.Message = message;
                md.ShowDialog();
            }
        }
        private void TruthHomeTown_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                using (ESCMenu esc = new ESCMenu())
                {
                    esc.MC = this.MC;
                    esc.SC = this.SC;
                    esc.TC = this.TC;
                    esc.WE = this.we;
                    esc.KnownTileInfo = null;
                    esc.KnownTileInfo2 = null;
                    esc.KnownTileInfo3 = null;
                    esc.KnownTileInfo4 = null;
                    esc.KnownTileInfo5 = null;
                    esc.Truth_KnownTileInfo = this.Truth_KnownTileInfo;
                    esc.Truth_KnownTileInfo2 = this.Truth_KnownTileInfo2;
                    esc.Truth_KnownTileInfo3 = this.Truth_KnownTileInfo3;
                    esc.Truth_KnownTileInfo4 = this.Truth_KnownTileInfo4;
                    esc.Truth_KnownTileInfo5 = this.Truth_KnownTileInfo5;
                    esc.StartPosition = FormStartPosition.CenterParent;
                    esc.TruthStory = true;
                    esc.ShowDialog();
                    if (esc.DialogResult == DialogResult.Retry)
                    {
                        this.mc = esc.MC;
                        this.sc = esc.SC;
                        this.tc = esc.TC;
                        this.we = esc.WE;
                        this.Truth_KnownTileInfo = esc.Truth_KnownTileInfo;
                        this.Truth_KnownTileInfo2 = esc.Truth_KnownTileInfo2;
                        this.Truth_KnownTileInfo3 = esc.Truth_KnownTileInfo3;
                        this.Truth_KnownTileInfo4 = esc.Truth_KnownTileInfo4;
                        this.Truth_KnownTileInfo5 = esc.Truth_KnownTileInfo5;
                        this.DialogResult = DialogResult.Retry;
                    }
                    else if (esc.DialogResult == DialogResult.Cancel)
                    {
                        this.DialogResult = DialogResult.Cancel;
                    }
                }
            }
        }

        private void UpdateEndingMessage2(string message)
        {
            this.endingText2.Add(message + "\r\n");
        }
        private void UpdateEndingMessage(string message)
        {
            this.endingText.Add(message + "\r\n");
        }
        private void UpdateMainMessage(string message)
        {
            UpdateMainMessage(message, false);
        }
        private void UpdateMainMessage(string message, bool ignoreOK)
        {
            GroundOne.playbackMessage.Insert(0, message);
            GroundOne.playbackInfoStyle.Insert(0, TruthPlaybackMessage.infoStyle.normal);
            mainMessage.Text = message;
            mainMessage.Update();
            if (!ignoreOK)
            {
                ok.ShowDialog();
            }
        }

        // ダンジョンシーカー！
        private void button2_Click(object sender, EventArgs e)
        {
            if (!GroundOne.WE2.RealWorld && we.GameDay <= 1 && (!we.AlreadyCommunicate || !we.Truth_CommunicationGanz1 || !we.Truth_CommunicationHanna1 || !we.Truth_CommunicationLana1))
            {
                mainMessage.Text = "アイン：ダンジョンはもう少し待ってくれ。町の皆に挨拶をしなくちゃな。";
            }
            else if (!GroundOne.WE2.RealWorld && we.TruthCompleteArea1 && (!we.Truth_CommunicationLana21 || !we.Truth_CommunicationGanz21 || !we.Truth_CommunicationHanna21 || !we.Truth_CommunicationOl21))
            {
                mainMessage.Text = "アイン：ダンジョンはもう少し待ってくれ。町の皆に挨拶をしなくちゃな。";
            }
            else if (!GroundOne.WE2.RealWorld && we.TruthCompleteArea2 && (!we.Truth_CommunicationLana31 || !we.Truth_CommunicationGanz31 || !we.Truth_CommunicationHanna31 || !we.Truth_CommunicationOl31 || !we.Truth_CommunicationSinikia31))
            {
                mainMessage.Text = "アイン：ダンジョンはもう少し待ってくれ。町の皆に挨拶をしなくちゃな。";
            }
            else if (!GroundOne.WE2.RealWorld && we.TruthCompleteArea3 && (!we.Truth_CommunicationLana41 || !we.Truth_CommunicationGanz41 || !we.Truth_CommunicationHanna41 || !we.Truth_CommunicationOl41 || !we.Truth_CommunicationSinikia41))
            {
                mainMessage.Text = "アイン：ダンジョンはもう少し待ってくれ。町の皆に挨拶をしなくちゃな。";
            }
            else if (GroundOne.WE2.RealWorld && (!GroundOne.WE2.SeekerEvent602 || !GroundOne.WE2.SeekerEvent603 || !GroundOne.WE2.SeekerEvent604))
            {
                mainMessage.Text = "アイン：ダンジョンはもう少し待ってくれ。町の皆に挨拶をしなくちゃな。";
            }
            else if (!we.AlreadyRest)
            {
                mainMessage.Text = "アイン：今出てきたばかりだぜ？一休憩させてくれ。";
            }
            else
            {
                if (GroundOne.WE2.RealWorld && GroundOne.WE2.SeekerEvent602 && GroundOne.WE2.SeekerEvent603 && GroundOne.WE2.SeekerEvent604 && !GroundOne.WE2.SeekerEvent605)
                {
                    UpdateMainMessage("アイン：（・・・よし・・・行くか！）");

                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("ラナ：ちょっと、待ちなさいよ。");

                    GroundOne.PlayDungeonMusic(Database.BGM19, Database.BGM19LoopBegin);

                    UpdateMainMessage("アイン：・・・ラナか・・・");

                    UpdateMainMessage("ラナ：今からダンジョンに向かう気よね？");

                    UpdateMainMessage("アイン：ああ、そのつもりだ。");

                    UpdateMainMessage("ラナ：・・・");

                    UpdateMainMessage("アイン：・・・");

                    UpdateMainMessage("ラナ：【　真実の答え　】・・・見つかった？");

                    UpdateMainMessage("アイン：・・・");

                    UpdateMainMessage("アイン：ああ、見つかってる。");

                    UpdateMainMessage("ラナ：今言ってみて、聞いてあげるから。");

                    UpdateMainMessage("アイン：わかった。");

                    UpdateMainMessage("アイン：【力は力にあらず、力は全てである。】");

                    UpdateMainMessage("アイン：【負けられない勝負。　しかし心は満たず。】");

                    UpdateMainMessage("アイン：【力のみに依存するな。心を対にせよ。】");

                    UpdateMainMessage("アイン：ラナの母ちゃんがやってた紫聡千律道場。あそこの十訓の一つだ。");

                    UpdateMainMessage("ラナ：そっか・・・ちゃんと覚えてたのね。");

                    UpdateMainMessage("アイン：ああ。あの時は分からなかったが、今、ようやく分かり始めたんだ。");

                    UpdateMainMessage("アイン：力だけじゃ限界がある、それだけじゃダメなんだ。");

                    UpdateMainMessage("アイン：でもだからと言って、信念や想いだけを持ってても駄目だ。");

                    UpdateMainMessage("アイン：両方とも併せもって初めて意味が出てくる。");

                    UpdateMainMessage("アイン：そんな感じだ。");

                    UpdateMainMessage("ラナ：そう・・・私にはよく分からないけど");

                    UpdateMainMessage("ラナ：アインが感じた今の答えが真実なのね、きっと。");

                    UpdateMainMessage("アイン：それを教えてくれたのが、この剣だ。");

                    UpdateMainMessage("ラナ：その練習用の剣？　小さい頃母さんからもらったやつよね。");

                    UpdateMainMessage("アイン：ああ、そうだ。");

                    UpdateMainMessage("アイン：これが神剣フェルトゥーシュだと知るまでにはずいぶんと時間がかかった。");

                    UpdateMainMessage("アイン：あの頃は、どうみても単なるナマクラの剣にしか見えなかったからな。");

                    UpdateMainMessage("ラナ：・・・いつ頃から気づいてたのよ？");

                    UpdateMainMessage("アイン：ボケ師匠ランディスに出くわした時だ。");

                    UpdateMainMessage("ラナ：そうだったの。それからは、気づかない振りしてたの？");

                    UpdateMainMessage("アイン：いや、そういうわけじゃねえ。半信半疑だったってのが正直な所だ。");

                    UpdateMainMessage("アイン：あの剣は、どうみても単なるナマクラだ。実際使ってみても全然威力が出ないしな。");

                    UpdateMainMessage("ラナ：ふうん。それでお師匠さんに会ってからどう変わったのよ？");

                    UpdateMainMessage("アイン：師匠はどうもあの剣の特性に関して、もう一つ何か知ってるみたいだったんだ。");

                    UpdateMainMessage("アイン：いや、あの剣に関わらず、全般的な話みたいだった。それを教えてくれた。");

                    UpdateMainMessage("アイン：心を燈して放たないと、威力は発揮されない。何かそんな話だった。");

                    UpdateMainMessage("ラナ：心を燈して・・・って事は。");

                    UpdateMainMessage("アイン：あの剣、最高攻撃力が異常に高い。そして、最低攻撃力も異常に低い。");

                    UpdateMainMessage("アイン：心を燈さない限り、最高攻撃力は出ない。つまり、ナマクラなままってわけだ。");

                    UpdateMainMessage("アイン：それが分かった時点で、俺の力に対する考えは変わった。");

                    UpdateMainMessage("アイン：あの十訓の７番目。あの言葉通り、力は必要だが、力だけじゃ駄目だって事さ。");

                    UpdateMainMessage("ラナ：うん・・・");

                    UpdateMainMessage("ラナ：アインって・・・凄いわね。");

                    UpdateMainMessage("アイン：な、いやいや、凄くなんかねえって。");

                    UpdateMainMessage("ラナ：ううん、そういう風に考えが行き届くのは凄いわよ。私じゃ考えもつかないもの。");

                    UpdateMainMessage("アイン：いや、俺の勝手な解釈だからな。間違ってる可能性の方が高いぞ。");

                    UpdateMainMessage("ラナ：ううん、解釈が間違ってるとかそういう話じゃないの。");

                    UpdateMainMessage("ラナ：アインの雰囲気そのものが、凄く変わるのよ。");

                    UpdateMainMessage("ラナ：凄く冷静で・・・的を得ていて・・・");

                    UpdateMainMessage("ラナ：いつものアインじゃないみたい。");

                    UpdateMainMessage("アイン：ま・・・");
                    
                    UpdateMainMessage("アイン：まあ、そういう側面もあるさ！　ッハッハッハ！");

                    UpdateMainMessage("ラナ：良いのよ、無理して雰囲気変えなくて、ッフフ♪");

                    UpdateMainMessage("アイン：わ、悪いな・・・");

                    UpdateMainMessage("ラナ：ッフフ、良いって言ってるじゃないの♪");

                    UpdateMainMessage("ラナ：でも、ついでに言わせてもらうわね。");

                    UpdateMainMessage("アイン：な、なんだ？");

                    UpdateMainMessage("ラナ：アイン、あんた私に手加減してるでしょ？");

                    UpdateMainMessage("アイン：手加減？？　一体何の話だ。");

                    UpdateMainMessage("ラナ：戦闘スタイルの事よ。");

                    UpdateMainMessage("アイン：戦闘・・・スタイル？");

                    UpdateMainMessage("ラナ：そうよ。私レベルが相手ならバレないとでも思ってたのかしら。");

                    UpdateMainMessage("アイン：いや、俺は手加減なんてしてないぞ。気のせいじゃないのか？");

                    UpdateMainMessage("ラナ：見たわよ、アンタが傭兵訓練所を卒業した後、コッソリ独自で練習している所。");

                    UpdateMainMessage("ラナ：あんな動き・・・見た事もないスピードだったわ。");

                    UpdateMainMessage("アイン：ま、待て。あれはだな・・・");

                    UpdateMainMessage("ラナ：いいのよ。私じゃ正直、追いつけないレベルだった。");

                    UpdateMainMessage("ラナ：動作切替タイミング、詠唱速度、剣を振るう速度。");

                    UpdateMainMessage("ラナ：全てが別次元だったわ。");

                    UpdateMainMessage("ラナ：どうして・・・私に見せてくれないのかしら？");

                    UpdateMainMessage("アイン：・・・　・・・");

                    UpdateMainMessage("アイン：すまねえ。");

                    UpdateMainMessage("ラナ：謝らないでよ・・・どうなの？本当の所を教えてちょうだいよ。");

                    UpdateMainMessage("アイン：・・・　・・・　・・・");

                    UpdateMainMessage("ラナ：やっぱり・・・そういう事よね。");

                    UpdateMainMessage("アイン：まて、そうじゃねえんだ！");

                    UpdateMainMessage("アイン：俺が悪いのは本当だ。");

                    UpdateMainMessage("アイン：ラナ、お前にだけはそういうとこを見せたくなかったんだ。");

                    UpdateMainMessage("アイン：知られたく・・・なかったんだ。");

                    UpdateMainMessage("アイン：お前がもし、俺のそういう側面を知ってしまえば・・・");

                    UpdateMainMessage("アイン：俺の前から・・・居なくなるんじゃないかって・・・");

                    UpdateMainMessage("ラナ：力量に差が出てきたら、私がアインから離れていく。そう考えたって事？");

                    UpdateMainMessage("アイン：・・・ああ・・・");

                    UpdateMainMessage("ラナ：・・・　・・・");

                    UpdateMainMessage("ラナ：ッフ、ッフフ♪　なーに言ってんのかしら、バッカじゃないのアンタ！？");

                    UpdateMainMessage("ラナ：力量なんて・・・そもそもアンタに私が追いつけるワケないでしょ！？");

                    UpdateMainMessage("アイン：ラ、ラナ・・・");

                    UpdateMainMessage("ラナ：何よそれ・・・失礼しちゃうわよホント。アンタの実力ってどんだけなのよ本当。");

                    UpdateMainMessage("ラナ：隠すとか隠さないとか・・・くだらない事ばっかり考えて・・・");
                    
                    UpdateMainMessage("ラナ：隠さなきゃいけないレベルになっちゃってる、そう言いたいわけ！？");

                    UpdateMainMessage("アイン：うっ・・・");

                    UpdateMainMessage("ラナ：あの練習内容の異次元みたいなスピードから察するに、そういう事よね！？");

                    UpdateMainMessage("ラナ：私なんかじゃ。。。絶対にあんなの出来っこないもん。。。");

                    UpdateMainMessage("アイン：いや、今は出来なくとも・・・");

                    UpdateMainMessage("ラナ：そんな風に気を使わないで。　私、自分の事は分かってるつもりだから。");

                    UpdateMainMessage("ラナ：・・・　・・・");

                    UpdateMainMessage("アイン：・・・　・・・");

                    UpdateMainMessage("ラナ：ッフフ・・・おかしいわね。昔の小さい頃のアインってさ、すごく弱かったし。");

                    UpdateMainMessage("ラナ：いっつも泣いてばっかり。で、私がいっつも守ってあげてたのに・・・");

                    UpdateMainMessage("ラナ：いつの間にそんなに腕を上げちゃってたのかしら、信じられないわ本当。");

                    UpdateMainMessage("アイン：ッハハ・・・あったな、そういやそんな事も・・・");

                    UpdateMainMessage("ラナ：・・・　・・・");

                    UpdateMainMessage("アイン：・・・　・・・");

                    UpdateMainMessage("ラナ：いいわ、アイン。");

                    UpdateMainMessage("ラナ：アインが私に対して、変に気を使ってた事は許してあげる。");

                    UpdateMainMessage("アイン：わ・・・悪かったな、マジで。");

                    UpdateMainMessage("アイン：これからは・・・そうだな、あまり気を使わずに・・・。");

                    UpdateMainMessage("ラナ：あっ、そぉーーーだ！！！");

                    UpdateMainMessage("アイン：うお！？なんだいきなり！？");

                    UpdateMainMessage("ラナ：今、良い事思いついちゃった♪");

                    UpdateMainMessage("ラナ：バカアイン、今から言うのは命令よ。ちゃんと聞きなさいよね。");

                    UpdateMainMessage("アイン：な、何だ？");

                    UpdateMainMessage("ラナ：私、今ここでアインにDUEL決闘を申し込むわ。");

                    UpdateMainMessage("アイン：な！！！");

                    UpdateMainMessage("ラナ：で、条件を一つ付け加えるわ。聞きなさい。");

                    UpdateMainMessage("アイン：な、何だその条件ってのは？");

                    UpdateMainMessage("ラナ：あんた今度こそ本当に、今この場で手加減せずに私に挑んでもらうわよ。");

                    UpdateMainMessage("ラナ：それが絶対の条件よ。どう？");

                    UpdateMainMessage("アイン：っぐ・・・");

                    UpdateMainMessage("アイン：もし万が一手加減してたら・・・どうなる？");

                    UpdateMainMessage("ラナ：その時は、私はアンタともうコンビは組まないわ。");
                    
                    UpdateMainMessage("ラナ：手加減されてまで一緒に居たくないから。");

                    UpdateMainMessage("アイン：・・・分かった。");
                    
                    UpdateMainMessage("アイン：この一戦、絶対に手加減はしねえ。約束だ！");

                    UpdateMainMessage("アイン：・・・あっ！ま、まてよ！？");
                    
                    UpdateMainMessage("アイン：万が一それで、俺が勝ってしまったらどうなるんだ？");

                    UpdateMainMessage("アイン：やっぱり・・・その時も・・・");

                    UpdateMainMessage("ラナ：・・・ップ");
                    
                    UpdateMainMessage("ラナ：ッフフフ、アーッハハハハ♪");

                    UpdateMainMessage("ラナ：何そんな心配してんのよ、大丈夫よ♪");

                    UpdateMainMessage("ラナ：手加減してない本気のアンタを見たいだけよ。");

                    UpdateMainMessage("ラナ：アンタ基本的に勝って当然なんだから、またクダラナイ事考えないでよねホント♪");

                    UpdateMainMessage("ラナ：（　どっちにしろ・・・本当に離れたりするわけ・・・　）");

                    UpdateMainMessage("アイン：えっ・・・？");

                    UpdateMainMessage("ラナ：ホーラホラホラホラ、じゃあ行くわよ。ちゃんと構えなさいよね♪");

                    UpdateMainMessage("アイン：あ、ああ。ちょっと待ってくれな。");

                    UpdateMainMessage("アイン：・・・よし、ＯＫだ。");

                    UpdateMainMessage("ラナ：私も良いわよ♪");

                    UpdateMainMessage("アイン：じゃあ、正真正銘の本気だ。手加減抜きで行くぜ！");

                    UpdateMainMessage("ラナ：始めるわよ、３");

                    UpdateMainMessage("アイン：２");

                    UpdateMainMessage("ラナ：１");

                    UpdateMainMessage("アイン：０！！");

                    bool failCount1 = false;
                    bool failCount2 = false;
                    while (true)
                    {
                        bool result = BattleStart(Database.ENEMY_LAST_RANA_AMILIA, true);

                        if (failCount1 && failCount2)
                        {
                            using (YesNoReqWithMessage ynrw = new YesNoReqWithMessage())
                            {
                                ynrw.StartPosition = FormStartPosition.CenterParent;
                                ynrw.MainMessage = "戦闘をスキップし、勝利した状態からストーリーを進めますか？\r\n戦闘スキップによるペナルティはありません。";
                                ynrw.ShowDialog();
                                if (ynrw.DialogResult == DialogResult.Yes)
                                {
                                    result = true;
                                }
                            }
                        }

                        if (result)
                        {
                            // 勝ち
                            UpdateMainMessage("ラナ：ッキャ！！");

                            UpdateMainMessage("アイン：しまっっ！！　大丈夫か、ラナ！？");

                            UpdateMainMessage("ラナ：いっつつつ・・・大丈夫よ、少し打っただけだから。");

                            UpdateMainMessage("アイン：け、怪我とかしてねえか？大丈夫なのか？痛い所はないか！？");

                            UpdateMainMessage("ラナ：だーいじょーぶだって言ってるでしょーが。ホラホラ元気よ♪");

                            UpdateMainMessage("アイン：よ・・・良かった。本当に大丈夫だな？");

                            UpdateMainMessage("ラナ：しつこいわね。蹴りかえすわよ。");

                            UpdateMainMessage("アイン：わわわ、わかった。");

                            UpdateMainMessage("ラナ：で・・・手加減はしてないわよね？");

                            UpdateMainMessage("アイン：もちろんさ！　俺の得意戦術をそのまま使ったからな！");

                            UpdateMainMessage("ラナ：でも、まさかあんなタイミングから入れてくるとは思わなかったわ。");

                            UpdateMainMessage("ラナ：アインってさ、どこでそういうの覚えてきてるの？");

                            UpdateMainMessage("アイン：どこって言われてもな・・・師匠とやってるうちに自然と・・・かな。");

                            UpdateMainMessage("ラナ：ふ～ん・・・やっぱりランディスのお師匠さんが影響してるわけね。");

                            UpdateMainMessage("アイン：あとは・・・自分なりに、コソコソっとだな・・・");

                            UpdateMainMessage("アイン：他にはDUEL闘技場を観察してて、自分にはないトコを観察かな。");

                            UpdateMainMessage("アイン：傭兵訓練時代の基礎訓練項目もたまに読み返して反復練習はしてる。");

                            UpdateMainMessage("アイン：モンスター狩りの時も、普段使わない新戦術を取り入れてみたり。");

                            UpdateMainMessage("アイン：あとは・・・");

                            UpdateMainMessage("ラナ：あ～あ、もうイイ！　私の負け！！");

                            UpdateMainMessage("アイン：うわっ、すまねえ、悪かったって。");

                            UpdateMainMessage("ラナ：ううん、良いの。本気を見せてくれたんだし、スッキリしたわ♪");

                            UpdateMainMessage("アイン：ハッ・・・ハハハ・・・");

                            UpdateMainMessage("ラナ：ダンジョン、私を誘わないで一人でいくつもりなんでしょ？");

                            UpdateMainMessage("アイン：うっ・・・");

                            UpdateMainMessage("ラナ：バカアインは嘘作りが下手くそすぎなのよ。そんなのお見通しよ。");

                            UpdateMainMessage("アイン：ハハハ・・・まあ・・・");

                            UpdateMainMessage("アイン：嘘というか、正直パーティに誘うつもりはあった。");

                            UpdateMainMessage("アイン：これは本当だ。");

                            UpdateMainMessage("ラナ：・・・　・・・");

                            UpdateMainMessage("アイン：でも、それじゃ・・・駄目みたいなんだ。");

                            UpdateMainMessage("アイン：俺は・・・");

                            UpdateMainMessage("アイン：ここを抜けださなきゃならないんだ。");

                            UpdateMainMessage("ラナ：何とか・・・辿りつけそうなの？");

                            UpdateMainMessage("アイン：ああ、お前のイヤリングもホラ。");

                            UpdateMainMessage("ラナ：あっ・・・");

                            UpdateMainMessage("アイン：今は、俺が手にしたままの状態だ。");

                            UpdateMainMessage("アイン：・・・分かったんだ、どうしなければいけないか。");

                            UpdateMainMessage("ラナ：・・・");

                            UpdateMainMessage("ラナ：ありがと。こんな所まで頑張ってくれて。");

                            UpdateMainMessage("アイン：バカ言うな。俺自身の問題だ。");

                            UpdateMainMessage("アイン：絶対に何とかしてやる。任せろ。");

                            UpdateMainMessage("ラナ：うん、お願い。期待してるから♪");

                            UpdateMainMessage("アイン：じゃあな、行ってくるぜ！！");

                            break;
                        }
                        else
                        {
                            using (YesNoReqWithMessage yerw = new YesNoReqWithMessage())
                            {
                                UpdateMainMessage("アイン：ッグ・・！！");
                                if (!failCount1)
                                {
                                    failCount1 = true;

                                    UpdateMainMessage("ラナ：今ので当たるなんて、アインらしくないわね。");

                                    UpdateMainMessage("アイン：ックソ・・・避けたつもりだったんだがな。");

                                    UpdateMainMessage("ラナ：今のアイン・・・やっぱり動きが鈍ってるわよ。");

                                    UpdateMainMessage("ラナ：見せてちょうだいよ、本当の動きを。");

                                    UpdateMainMessage("アイン：あ、ああ。今度こそ！");
                                }
                                else if (!failCount2)
                                {
                                    failCount2 = true;
                                    UpdateMainMessage("ラナ：手加減が身体に染み込んでいるみたいね。動きが遅かったわよ。");

                                    UpdateMainMessage("アイン：ラナ相手だと・・・動きが縮こまってるのか・・・");

                                    UpdateMainMessage("ラナ：今のじゃ納得いかないわ、アイン本気をだしてちょうだい。");

                                    UpdateMainMessage("アイン：ああ、今度こそ！");
                                }
                                else
                                {
                                    UpdateMainMessage("ラナ：今のじゃ納得いかないわ、アイン本気をだしてちょうだい。");

                                    UpdateMainMessage("アイン：っく、今度こそ！");
                                }
                            }
                        }
                    }

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "ダンジョンゲートの入り口にて";
                        md.ShowDialog();
                    }

                    GroundOne.StopDungeonMusic();

                    UpdateMainMessage("アイン：（・・・ダンジョンへ・・・俺は向かう・・・）");

                    UpdateMainMessage("アイン：（ラナのイヤリングは手にしたままだ）");

                    UpdateMainMessage("アイン：（俺はこれの意味を知っている）");

                    UpdateMainMessage("アイン：（・・・　・・・　・・・）");

                    UpdateMainMessage("アイン：（行こう、ダンジョンへ）");

                    this.targetDungeon = 1;
                    GroundOne.WE2.RealDungeonArea = 1;
                    GroundOne.WE2.SeekerEvent605 = true;
                    Method.AutoSaveTruthWorldEnvironment();
                    Method.AutoSaveRealWorld(this.MC, this.SC, this.TC, this.WE, null, null, null, null, null, this.Truth_KnownTileInfo, this.Truth_KnownTileInfo2, this.Truth_KnownTileInfo3, this.Truth_KnownTileInfo4, this.Truth_KnownTileInfo5);
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                }
                else
                {
                    string Opponent = WhoisDuelPlayer();
                    if (Opponent != String.Empty)
                    {
                        DuelSupportMessage(SupportType.FromDungeonGate, Opponent);

                        CallDuel(Opponent, true);
                    }
                    else
                    {
                        #region "ダンジョン階層を選択"
                        if (we.TruthCompleteArea1)
                        {
                            mainMessage.Text = "アイン：さて、何階から始めるかな。";
                            mainMessage.Update();
                            using (SelectDungeon sd = new SelectDungeon())
                            {
                                sd.StartPosition = FormStartPosition.Manual;
                                sd.Location = new Point(this.Location.X + 50, this.Location.Y + 50);
                                //if (we.CompleteArea5) sd.MaxSelectable = 5;
                                if (we.TruthCompleteArea4) sd.MaxSelectable = 5;
                                else if (we.TruthCompleteArea3) sd.MaxSelectable = 4;
                                else if (we.TruthCompleteArea2) sd.MaxSelectable = 3;
                                else if (we.TruthCompleteArea1) sd.MaxSelectable = 2;
                                sd.ShowDialog();
                                this.targetDungeon = sd.TargetDungeon;
                            }
                        }
                        if (this.targetDungeon == 1)
                        {
                            if (!we.TruthCompleteArea1)
                            {
                                mainMessage.Text = "アイン：さて、１階を突破するぜ！";
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
                        #endregion

                        #region "ラナ、ガンツ、ハンナの一般会話完了はここで反映します。"
                        if (this.firstDay >= 1 && !we.Truth_CommunicationLana1 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationLana1 = true;
                        else if (this.firstDay >= 2 && !we.Truth_CommunicationLana2 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationLana2 = true;
                        else if (this.firstDay >= 3 && !we.Truth_CommunicationLana3 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationLana3 = true;
                        else if (this.firstDay >= 4 && !we.Truth_CommunicationLana4 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationLana4 = true;
                        else if (this.firstDay >= 5 && !we.Truth_CommunicationLana5 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationLana5 = true;
                        else if (this.firstDay >= 6 && !we.Truth_CommunicationLana6 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationLana6 = true;
                        else if (this.firstDay >= 7 && !we.Truth_CommunicationLana7 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationLana7 = true;
                        else if (this.firstDay >= 8 && !we.Truth_CommunicationLana8 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationLana8 = true;
                        else if (this.firstDay >= 9 && !we.Truth_CommunicationLana9 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationLana9 = true;
                        else if (this.firstDay >= 10 && !we.Truth_CommunicationLana10 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationLana10 = true;

                        if (this.firstDay >= 1 && !we.Truth_CommunicationHanna1 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationHanna1 = true;
                        else if (this.firstDay >= 2 && !we.Truth_CommunicationHanna2 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationHanna2 = true;
                        else if (this.firstDay >= 3 && !we.Truth_CommunicationHanna3 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationHanna3 = true;
                        else if (this.firstDay >= 4 && !we.Truth_CommunicationHanna4 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationHanna4 = true;
                        else if (this.firstDay >= 5 && !we.Truth_CommunicationHanna5 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationHanna5 = true;
                        else if (this.firstDay >= 6 && !we.Truth_CommunicationHanna6 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationHanna6 = true;
                        else if (this.firstDay >= 7 && !we.Truth_CommunicationHanna7 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationHanna7 = true;
                        else if (this.firstDay >= 8 && !we.Truth_CommunicationHanna8 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationHanna8 = true;
                        else if (this.firstDay >= 9 && !we.Truth_CommunicationHanna9 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationHanna9 = true;
                        else if (this.firstDay >= 10 && !we.Truth_CommunicationHanna10 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationHanna10 = true;

                        if (this.firstDay >= 1 && !we.Truth_CommunicationGanz1 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationGanz1 = true;
                        else if (this.firstDay >= 2 && !we.Truth_CommunicationGanz2 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationGanz2 = true;
                        else if (this.firstDay >= 3 && !we.Truth_CommunicationGanz3 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationGanz3 = true;
                        else if (this.firstDay >= 4 && !we.Truth_CommunicationGanz4 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationGanz4 = true;
                        else if (this.firstDay >= 5 && !we.Truth_CommunicationGanz5 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationGanz5 = true;
                        else if (this.firstDay >= 6 && !we.Truth_CommunicationGanz6 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationGanz6 = true;
                        else if (this.firstDay >= 7 && !we.Truth_CommunicationGanz7 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationGanz7 = true;
                        else if (this.firstDay >= 8 && !we.Truth_CommunicationGanz8 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationGanz8 = true;
                        else if (this.firstDay >= 9 && !we.Truth_CommunicationGanz9 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationGanz9 = true;
                        else if (this.firstDay >= 10 && !we.Truth_CommunicationGanz10 && mc.Level >= 1 && we.AlreadyCommunicate) we.Truth_CommunicationGanz10 = true;
                        #endregion

                        we.AlreadyShownEvent = false;
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    }
                }
            }
        }

        private void CallDuel(string OpponentDuelist, bool fromGoDungeon)
        {
            UpdateMainMessage("　　【受付嬢：３】");

            UpdateMainMessage("　　【受付嬢：２】");

            UpdateMainMessage("　　【受付嬢：１】");

            UpdateMainMessage("　　【受付嬢：０】");


            bool result = BattleStart(OpponentDuelist, true);
            if (result)
            {
                GroundOne.WE2.DuelWin += 1;

                UpdateMainMessage("　　【受付嬢：勝者 アイン・ウォーレンス！】");

                UpdateMainMessage("アイン：っしゃ！俺の勝ちだぜ！！");
            }
            else
            {
                GroundOne.WE2.DuelLose += 1;
                UpdateMainMessage("　　【受付嬢：勝者 " + OpponentDuelist + "！】");

                UpdateMainMessage("アイン：ックソ・・・負けちまったか・・・");
            }

            #region "ザルゲのDUEL戦闘後のセリフ"
            if (OpponentDuelist == Database.DUEL_SCOTY_ZALGE)
            {
                if (result)
                {
                    we.DuelWinZalge = true;

                    UpdateMainMessage("ザルゲ：ッゲホオオォ・・・ッグ・・・こんな雑魚に俺様が・・・");

                    UpdateMainMessage("アイン：雑魚で悪かったな。");

                    UpdateMainMessage("ザルゲ：ックソォォォ・・・っおい、キーナ嬢！！");

                    UpdateMainMessage("ザルゲ：今から言う話をよぉぉぉく聞いておけよ！");

                    UpdateMainMessage("ザルゲ：３０連勝がストップしたからと言って・・・");

                    UpdateMainMessage("　　　（アインは突然大声で・・・）");

                    UpdateMainMessage("アイン：ＤＵＥＬ大会運営の会長さんよ！");

                    UpdateMainMessage("アイン：なあ、どっかに居るんだろ！？");

                    UpdateMainMessage("　　　（・・・観客が少しだけ、どよめき始めた・・・）");

                    UpdateMainMessage("アイン：受付嬢って確か、ＤＵＥＬ運営サポートが役割なんだろ？");

                    UpdateMainMessage("アイン：このままじゃ、ＤＵＥＬの運営全体に支障をきたすんじゃないのか？");

                    UpdateMainMessage("アイン：そうなる前にさ。サポートに徹する事が出来る環境にしてやってくれ。");

                    UpdateMainMessage("アイン：頼んだぜ！？");

                    UpdateMainMessage("　　　（・・・ざわ・・・ザワザワザワ・・・）");

                    UpdateMainMessage("ザルゲ：お、おい、何くだらねえこと提言してんだよ！？あぁぁ！？");

                    UpdateMainMessage("ザルゲ：んな会長なんてココら辺にいるわけねえだろうが！？");

                    UpdateMainMessage("アイン：俺たちが認識できないほど気配を隠してるだけだ。必ずどこかで見てるはずさ。");

                    UpdateMainMessage("ザルゲ：って事は何か？俺に鉄槌でも下るってかぁ！？");

                    UpdateMainMessage("ザルゲ：・・・・・・");

                    UpdateMainMessage("ザルゲ：って何も起きねぇじゃねえか！！　ゲハハハハハハ！");

                    UpdateMainMessage("ザルゲ：さぁて、先程の続きだ、良いかぁキーナ嬢ちゃん！？実はなあ！！");

                    UpdateMainMessage("　　　（突然、ザルゲに閃光が放たれた！！！）");

                    UpdateMainMessage("　　　（ッピッッシイイイイイィィィィィ！！！）");

                    UpdateMainMessage("　　　（・・・　・・・　・・・）");

                    UpdateMainMessage("アイン：き、消えた・・・");

                    UpdateMainMessage("アイン：ッサ・・・サンキューサンキュー！　ッハッハッハ！");

                    UpdateMainMessage("　　　（しばらくの間、闘技場では、アインへ賞賛の拍手が送られた）");

                    UpdateMainMessage("アイン：（キーナ嬢さん、これから上手く行くと良いな・・・)");

                    UpdateMainMessage("　　【受付嬢：・・・（ッゴホン）】");
                }
                else
                {
                    UpdateMainMessage("ザルゲ：アイン・ウォーレンス様、本日はありがっとォございました。");

                    UpdateMainMessage("ザルゲ：これを持ちましてぇ・・・ええ僭越ながらキーナ嬢との婚約を行うが我が運命。");

                    UpdateMainMessage("ザルゲ：その時はどうか婚儀の場へ・・・ック、ックククハハハ");

                    UpdateMainMessage("ザルゲ：お越しくださいませってかぁ！？　ゲハハハハハハ！！");

                    UpdateMainMessage("アイン：っく・・・");

                    UpdateMainMessage("ザルゲ：おい、キーナ嬢！　分かってんだろうなぁ！？");

                    UpdateMainMessage("ザルゲ：逃げんじゃねぇぞ！？　逃げたらご両親様がどうなるか分かってんだろうなぁ！？");

                    UpdateMainMessage("アイン：おい、もうＤＵＥＬは終了してるんだ。関係の無い話はするな。");

                    UpdateMainMessage("ザルゲ：はあぁぁ！？負けた奴が何をほざいんてんだぁ！？");

                    UpdateMainMessage("アイン：ッグ・・・");

                    UpdateMainMessage("ザルゲ：負け犬はとっとと退場するんだな！ホレ、そこの受付嬢さん、転送を早く。");

                    UpdateMainMessage("　　　（・・・受付嬢は少し小刻みに震えている・・・）");

                    UpdateMainMessage("ザルゲ：おっとっと、ここの受付嬢はルール通りに動けずに立ち往生ってかぁ！？");

                    UpdateMainMessage("ザルゲ：ゲハハハハハハ！！");

                    UpdateMainMessage("アイン：ッチ・・・早く転送させて終わるんだ、キーナさん。");

                    UpdateMainMessage("　　【受付嬢：それではＤＵＥＬは終了となります。両者とも転送させていただきます。】");

                    UpdateMainMessage("　　　（ッバシュウウン・・・）");

                    UpdateMainMessage("アイン：（ここは・・・そうか、終わったのか）");

                    UpdateMainMessage("　　【受付嬢：アイン様、現時点で" + GroundOne.WE2.DuelWin.ToString() + " 勝 " + GroundOne.WE2.DuelLose.ToString() + " 敗となります。】");

                    UpdateMainMessage("アイン：す、すまねえな・・・その・・・");

                    UpdateMainMessage("　　【受付嬢：今後のご活躍、期待しております。それでは。】");

                    UpdateMainMessage("アイン：あ、あぁ・・・");

                    UpdateMainMessage("アイン：（後味の悪い内容だな・・・ックソ・・・）");

                    if (mc != null)
                    {
                        mc.CurrentLife = mc.MaxLife;
                        mc.CurrentSkillPoint = mc.MaxSkillPoint;
                        mc.CurrentMana = mc.MaxMana;
                    }
                    return;
                }
            }
            #endregion

            UpdateMainMessage("　　【受付嬢：アイン様、現時点で" + GroundOne.WE2.DuelWin.ToString() + " 勝 " + GroundOne.WE2.DuelLose.ToString() + " 敗となります。】");

            UpdateMainMessage("　　【受付嬢：今後のご活躍、期待しております。それでは。】");

            if (fromGoDungeon)
            {
                CallSomeMessageWithAnimation("ダンジョンゲートの入り口へと強制送還されました");
            }
            else
            {
                CallSomeMessageWithAnimation("DUEL闘技場の入り口ゲートへと送還されました");
            }

            if (OpponentDuelist == Database.DUEL_EONE_FULNEA)
            {
                if (fromGoDungeon)
                {
                    UpdateMainMessage("アイン：っと、終わったら即送還かよ。本当に有無を言わさずだな。");
                }
                else
                {
                    UpdateMainMessage("アイン：っと、終わったら闘技場入り口へと送還されるのか。便利なシステムだな。");
                }
            }

            if (fromGoDungeon)
            {
                UpdateMainMessage("アイン：さて、ダンジョン行くとするか！", true);
            }
            else
            {
                UpdateMainMessage("アイン：さて、DUELも終えた事だし、ダンジョンにでも行くとするか！", true);
            }

            // [警告]：オブジェクトの参照が全ての場合、クラスにメソッドを用意してそれをコールした方がいい。
            if (mc != null)
            {
                mc.CurrentLife = mc.MaxLife;
                mc.CurrentSkillPoint = mc.MaxSkillPoint;
                mc.CurrentMana = mc.MaxMana;
            }
        }

        string PracticeSwordLevel(MainCharacter player)
        {
            string[] targetName = { Database.POOR_PRACTICE_SWORD_ZERO, Database.POOR_PRACTICE_SWORD_1, Database.POOR_PRACTICE_SWORD_2, Database.COMMON_PRACTICE_SWORD_3, Database.COMMON_PRACTICE_SWORD_4, Database.RARE_PRACTICE_SWORD_5, Database.RARE_PRACTICE_SWORD_6, Database.EPIC_PRACTICE_SWORD_7, Database.LEGENDARY_FELTUS };
            string detectName = String.Empty;

            for (int ii = 0; ii < targetName.Length; ii++)
            {
                if ((player != null) && (player.MainWeapon != null) && (player.MainWeapon.Name == targetName[ii]))
                {
                    detectName = targetName[ii];
                    break;
                }
                if ((player != null) && (player.SubWeapon != null) && (player.SubWeapon.Name == targetName[ii]))
                {
                    detectName = targetName[ii];
                    break;
                }
                if ((player != null) && (player.MainArmor != null) && (player.MainArmor.Name == targetName[ii]))
                {
                    detectName = targetName[ii];
                    break;
                }
                if ((player != null) && (player.Accessory != null) && (player.Accessory.Name == targetName[ii]))
                {
                    detectName = targetName[ii];
                    break;
                }
                if ((player != null) && (player.Accessory2 != null) && (player.Accessory2.Name == targetName[ii]))
                {
                    detectName = targetName[ii];
                    break;
                }
                ItemBackPack[] backpack = player.GetBackPackInfo();
                for (int kk = 0; kk < backpack.Length; kk++)
                {
                    if ((backpack[kk] != null) && (backpack[kk].Name == targetName[ii]))
                    {
                        detectName = targetName[ii];
                        break;
                    }
                }

                if (detectName != string.Empty)
                {
                    // 検知したため、検索不要
                    break;
                }
            }
            return detectName;
        }

        // ガンツ武具屋
        private void button4_Click(object sender, EventArgs e)
        {
            if (we.TruthCompleteArea1) we.AvailableEquipShop2 = true; // 前編で既に周知のため、解説は不要。
            if (we.TruthCompleteArea2) we.AvailableEquipShop3 = true; // 前編で既に周知のため、解説は不要。
            if (we.TruthCompleteArea3) we.AvailableEquipShop4 = true; // 前編で既に周知のため、解説は不要。
            if (we.TruthCompleteArea4) we.AvailableEquipShop5 = true; // 前編で既に周知のため、解説は不要。

            #region "現実世界"
            if (GroundOne.WE2.RealWorld && GroundOne.WE2.SeekerEvent601 && !GroundOne.WE2.SeekerEvent604)
            {
                UpdateMainMessage("アイン：ガンツ叔父さん、いますかー？");

                UpdateMainMessage("ガンツ：アインか。よく来てくれた。");

                UpdateMainMessage("アイン：武具店、開いてますか？");

                UpdateMainMessage("ガンツ：ああ、開店しておるので見ていくと良い。");

                UpdateMainMessage("アイン：っしゃ！やったぜ！じゃあ早速見せてもらうとするぜ！！");

                UpdateMainMessage("ガンツ：好きなだけ見ていくと良い。");

                UpdateMainMessage("アイン：・・・　・・・　・・・");

                UpdateMainMessage("ガンツ：アインよ。これからダンジョンへ向かうのだな？");

                UpdateMainMessage("アイン：はい。");

                UpdateMainMessage("ガンツ：アインよ、では心構えを一つ教えて進ぜよう。");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "ガンツは両眼を閉じた状態で、誰へともなく、空中へ語り始めた。";
                    md.ShowDialog();
                }

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

                UpdateMainMessage("アイン：ッハイ！！");

                UpdateMainMessage("ガンツ：心を決めたようだな。良い雰囲気だ。");

                UpdateMainMessage("ガンツ：アインよ、精進しなさい。");

                UpdateMainMessage("アイン：ッハイ！！！");
                
                GroundOne.WE2.SeekerEvent604 = true;
                Method.AutoSaveTruthWorldEnvironment();
                Method.AutoSaveRealWorld(this.MC, this.SC, this.TC, this.WE, null, null, null, null, null, this.Truth_KnownTileInfo, this.Truth_KnownTileInfo2, this.Truth_KnownTileInfo3, this.Truth_KnownTileInfo4, this.Truth_KnownTileInfo5);
            }
            else if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd && GroundOne.WE2.SeekerEvent604)
            {
                UpdateMainMessage("ガンツ：アインよ、精進しなさい。", true);
            }
            #endregion
            #region "オル・ランディス遭遇前後"
            else if (we.AvailableDuelMatch && !we.MeetOlLandis)
            {
                if (!we.MeetOlLandisBeforeGanz)
                {
                    UpdateMainMessage("アイン：こんちわー。");

                    UpdateMainMessage("ガンツ：アインよ。");

                    UpdateMainMessage("アイン：は、はい。なんでしょう？");

                    UpdateMainMessage("ガンツ：オルが挨拶に来ておったぞ。");

                    UpdateMainMessage("アイン：は、ハハハ・・・そうでしたか。そいつは良かったですね。");

                    UpdateMainMessage("ガンツ：アインよ。");

                    UpdateMainMessage("アイン：は、はいハイ！");

                    UpdateMainMessage("ガンツ：精進しなさい。");

                    UpdateMainMessage("アイン：ハイ！！！じゃあ、これで失礼いたします。");

                    UpdateMainMessage("　　（ッバタン・・・）");

                    UpdateMainMessage("アイン：（はあ・・・先回りされてるじゃねえか・・・）");

                    UpdateMainMessage("アイン：（しょうがねえ、もう闘技場へ行くしかねえみてえだな。）", true);
                    we.MeetOlLandisBeforeGanz = true;
                }
                else
                {
                    UpdateMainMessage("アイン：（しょうがねえ、もう闘技場へ行くしかねえみてえだな。）", true);
                }
                return;
            }
            #endregion
            #region "複合魔法・スキルを教えてもらうイベント"
            else if (we.TruthCompleteArea1 && !we.AvailableMixSpellSkill && mc.Level >= 21)
            {
                if (!we.AlreadyEquipShop)
                {
                    we.AlreadyEquipShop = true;

                    UpdateMainMessage("アイン：どうも、こんちわー");

                    UpdateMainMessage("ガンツ：アインよ、ちょっとこちらへ来なさい。");

                    UpdateMainMessage("アイン：ッゲ、何でしょう？");

                    UpdateMainMessage("ガンツ：心配はいらん。少しの間だけだ。");

                    UpdateMainMessage("アイン：はい。それじゃ・・・");

                    UpdateMainMessage("アイン：（あれ、この道って、ダンジョンゲートへ行くつもりか？）");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "ダンジョンゲート裏の広場にて";
                        md.ShowDialog();
                    }

                    UpdateMainMessage("ガンツ：着いたな。");

                    UpdateMainMessage("アイン：えっと、すいません質問なんですけど？");

                    UpdateMainMessage("ガンツ：なんだね。");

                    UpdateMainMessage("アイン：こんな裏広場まで来て一体何を？");

                    UpdateMainMessage("ガンツ：・・・アインよ、こちらへ来てみなさい。");

                    UpdateMainMessage("アイン：ん？これは・・・変な円紋が・・・");

                    UpdateMainMessage("ガンツ：空間転移装置、聞いた事ぐらいはあるだろう。");

                    UpdateMainMessage("アイン：マジっすか！？これが・・・へえええぇぇぇ！！");

                    UpdateMainMessage("アイン：え？　ってかどこかに行くつもりなんですか？");

                    UpdateMainMessage("　　ドン！！ （アインは突然突き飛ばされた）");

                    UpdateMainMessage("アイン：あ！っちょ！！　っちょちょ！！");

                    UpdateMainMessage("ガンツ：アインよ、精進なさい。");

                    UpdateMainMessage("　　ッバシュウウゥゥゥゥン");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "アインは別の場所へと飛ばされてしまった。";
                        md.ShowDialog();
                    }

                    this.buttonHanna.Visible = false;
                    this.buttonDungeon.Visible = false;
                    this.buttonRana.Visible = false;
                    this.buttonGanz.Visible = false;
                    this.buttonPotion.Visible = false;
                    this.buttonDuel.Visible = false;
                    ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_SECRETFIELD_OF_FAZIL);
                    this.Invalidate();

                    UpdateMainMessage("アイン：っいで！");

                    UpdateMainMessage("アイン：ッテテテ・・・体勢が悪かったせいか、放り出されちまった。");

                    UpdateMainMessage("アイン：ったく・・・行き先ぐらい教えてくれてもいいのに。");

                    UpdateMainMessage("？？？：貴君が、アイン・ウォーレンスか？");

                    UpdateMainMessage("アイン：え？あ、あぁそうだが・・・");

                    UpdateMainMessage("アイン：って、おわぁぁ！！");

                    UpdateMainMessage("　　　『相手の顔の右目がギョロっと動いている』");

                    UpdateMainMessage("アイン：ビックリしたなあ・・・擬眼かよ");

                    UpdateMainMessage("アイン：えっと・・・");

                    UpdateMainMessage("？？？：どれ、少し見せてもらうぞ。");

                    UpdateMainMessage("　　　『擬眼がギョロリと動きはじめた！』");

                    UpdateMainMessage("アイン：何かこええなあ・・・");

                    UpdateMainMessage("？？？：・・・　・・・　・・・");

                    UpdateMainMessage("？？？：スペル属性『聖  火  理』　それに");

                    UpdateMainMessage("？？？：スキル属性『動  剛  心眼』か。　なるほど。");

                    UpdateMainMessage("アイン：なっ！！");

                    UpdateMainMessage("？？？：大胆な攻撃スタイル、それに繊細な戦術をいくつか。");

                    UpdateMainMessage("？？？：挑発には意図的に挑む方だが、肝心な面はいつも冷静");

                    UpdateMainMessage("？？？：物理攻撃だけでなく、魔法にも長ける。全体的なオールラウンダー");

                    UpdateMainMessage("？？？：直感で『決まり』と判断すれば、一気に仕掛けるタイプ。");

                    UpdateMainMessage("？？？：ッフハハ、面白い。　ランディスもああ見えて教え好きだ。");

                    UpdateMainMessage("アイン：し、師匠を知ってるのか？");

                    UpdateMainMessage("　　　『擬眼が更にギョロリと動いた！』");

                    UpdateMainMessage("？？？：しかし、敵を全力で潰しにいかず、様子見の面が強い。");

                    UpdateMainMessage("？？？：一人で次々と倒そうとするより、チーム連携を考慮して動くタイプ。");

                    UpdateMainMessage("？？？：本来なら一人で出来る素質もあるが、表には決して見せない。");

                    UpdateMainMessage("？？？：なるほど、何か特定の事柄を意識しているな。");

                    UpdateMainMessage("？？？：この手抜き加減・・・驕りではなく、無意識的にかあるいは。");

                    UpdateMainMessage("？？？：たしかに、このままでは致命的な敗北は間逃れんな。");

                    UpdateMainMessage("アイン：い、いやいやいや・・・");

                    UpdateMainMessage("アイン：（ってか、さっきから妙にこの感覚・・・）");

                    UpdateMainMessage("　　【【【　アインは背筋に異常な恐怖感を覚えている。　】】】");

                    UpdateMainMessage("アイン：（あのボケ師匠じゃねえけど、ちょっと違った怖さがある・・・）");

                    UpdateMainMessage("アイン：（支配、制圧・・・そんな感じか）");

                    UpdateMainMessage("アイン：あんた、一体誰なんだよ？");

                    UpdateMainMessage("？？？：この距離感を一定に保つあたり、警戒心は貴君なりに最大というわけだな。");

                    UpdateMainMessage("？？？：だが我の射程、貴君の想像を遥かに超えている。");

                    UpdateMainMessage("アイン：っな！！！");

                    UpdateMainMessage("　　【【【　アインは膨大な汗を体中に感じた。　】】】");

                    UpdateMainMessage("アイン：（ッヤ、ヤベェ・・・何かヤベェ・・・！！！）");

                    UpdateMainMessage("アイン：って、ってか、そろそろ正体を教えろよ！？");

                    UpdateMainMessage("アイン：アンタ、敵じゃないんだろ！？");

                    UpdateMainMessage("？？？：この辺が頃合いか。逃げられても困る。");

                    UpdateMainMessage("？？？：我の名は『シニキア・カールハンツ』である。");

                    UpdateMainMessage("　　『アインは汗がスゥっと引いていくのを感じた。』");

                    UpdateMainMessage("アイン：（ッホ・・・なんだったんだ今のは・・・）");

                    UpdateMainMessage("アイン：・・・はじめまして、アインと言います。");

                    UpdateMainMessage("アイン：って、シニキアってまさか、伝説のFiveSeeker！！！");

                    UpdateMainMessage("カール：伝説のFiveSeekerなどという恥ずかしい通り名は止めてもらおう。");

                    UpdateMainMessage("カール：我はカールとでも呼べばよい。");

                    UpdateMainMessage("アイン：はい・・・えっと、じゃあのカールさん。");

                    UpdateMainMessage("アイン：ガンツ叔父さんは何でここへ俺を？");

                    UpdateMainMessage("カール：貴君を鍛えるよう言われている。");

                    UpdateMainMessage("アイン：俺をですか？");

                    UpdateMainMessage("カール：そうだ。");

                    UpdateMainMessage("カール：我の場合、鍛える方法は戦闘訓練ではない。");

                    UpdateMainMessage("カール：行動よりもまず理論。");

                    UpdateMainMessage("カール：貴君にはそれが欠けている。");

                    UpdateMainMessage("アイン：えっと・・・具体的には何を？");

                    UpdateMainMessage("カール：我の言う事、すべてを記憶せよ。");

                    UpdateMainMessage("アイン：記憶！？　暗記しろって事ですか！？");

                    UpdateMainMessage("カール：そうだ。では行くぞ。");

                    UpdateMainMessage("　　　『カールの講義が延々と小一時間続いたのち・・・』");

                    UpdateMainMessage("カール：複合魔法の基礎に関しては、以上だ。");

                    UpdateMainMessage("アイン：・・・");

                    UpdateMainMessage("　　　ッバタ・・・（アインはその場で静かに落ちた）");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "アインは複合魔法・スキルの基礎を習得した！";
                        md.ShowDialog();
                    }
                    we.AvailableMixSpellSkill = true;
                    GroundOne.WE2.AvailableMixSpellSkill = true;

                    UpdateMainMessage("カール：どうした。まだまだ先は長いぞ。");

                    UpdateMainMessage("アイン：無理・・・こういうのは駄目だ・・・");

                    UpdateMainMessage("アイン：なあ、ちょっとでも良いからよ。実践で教えてくれよ？");

                    UpdateMainMessage("カール：駄目だ。");

                    UpdateMainMessage("アイン：要は、聖と火を組み合わせるって事なんだろ？");

                    UpdateMainMessage("カール：違うな。");

                    UpdateMainMessage("アイン：具体的に一回だけ見せてくれよ・・・");

                    UpdateMainMessage("カール：駄目だ。");

                    UpdateMainMessage("アイン：火と聖って相性が良いって事なんだろ？");

                    UpdateMainMessage("カール：違うな。");

                    UpdateMainMessage("アイン：聖と闇は反対・・・みたいな？");

                    UpdateMainMessage("カール：違うな。");

                    UpdateMainMessage("アイン：カール先生、一回だけ頼むぜ！");

                    UpdateMainMessage("カール：駄目だ。");

                    UpdateMainMessage("アイン：トホホ・・・");

                    UpdateMainMessage("カール：心配か？");

                    UpdateMainMessage("アイン：え？そりゃまあ、やってみた方が覚えるのも早いし");

                    UpdateMainMessage("カール：イメージの基本は、習得した知識から来る。");

                    UpdateMainMessage("カール：具現化の展開は、それぞれの知恵から派生して成る。");

                    UpdateMainMessage("アイン：ん、ま、まあなんとなくその辺は・・・");

                    UpdateMainMessage("カール：心配するな。貴君はすでに習得したも同然だ。");

                    UpdateMainMessage("アイン：え！？　そんな、１回も確認してないですけど？");

                    UpdateMainMessage("カール：誰に教えを乞うたと思っておる。我を愚弄するか？");

                    UpdateMainMessage("アイン：い、いやいや！そんなつもりじゃございません！！");

                    UpdateMainMessage("カール：まあよい。空間転移装置を復活させておいた。帰るが良い。");

                    UpdateMainMessage("アイン：ハイ・・・どうもありがとうございました。");

                    UpdateMainMessage("　　ッバシュウウゥゥゥゥン");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "アインはダンジョンゲートの裏広場に戻ってきた";
                        md.ShowDialog();
                    }

                    if (!we.AlreadyRest)
                    {
                        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_EVENING);
                    }
                    else
                    {
                        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                    }
                    this.buttonHanna.Visible = true;
                    this.buttonDungeon.Visible = true;
                    this.buttonRana.Visible = true;
                    this.buttonGanz.Visible = true;
                    this.buttonPotion.Visible = true;
                    this.buttonDuel.Visible = true;


                    UpdateMainMessage("ガンツ：どうだったかね。");

                    UpdateMainMessage("アイン：どうも何も・・・すげえ疲れました。");

                    UpdateMainMessage("ガンツ：そうかね。では戻るとしよう。");

                    UpdateMainMessage("アイン：はい・・・");

                    UpdateMainMessage("アイン：（ガンツ叔父さんもこう見えて、強引だよな・・・）");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "アイン達はガンツの武具屋まで戻ってきた";
                        md.ShowDialog();
                    }

                    UpdateMainMessage("ガンツ：では、ワシはこれで。");

                    UpdateMainMessage("アイン：おじさん、ちょっと質問が");

                    UpdateMainMessage("ガンツ：何だね。");

                    UpdateMainMessage("アイン：あの転移された場所ってどこら辺なんですか？");

                    UpdateMainMessage("ガンツ：それはカール爵の希望により答えられん。");

                    UpdateMainMessage("アイン：そうなのか・・・いや、何か見たことある場所な気もしたんで");

                    UpdateMainMessage("ガンツ：なに、お主も良く知っておる場所よ。");

                    UpdateMainMessage("アイン：そうなんですか？　う～ん・・・");

                    UpdateMainMessage("アイン：まあ、良いや。おじさん、ありがとうございました！");

                    UpdateMainMessage("ガンツ：うむ、精進せいよ。");

                    UpdateMainMessage("　　　『ガンツは店の中へと戻っていった・・・』");

                    UpdateMainMessage("アイン：何かグダグダに疲れた気もするが・・・");

                    UpdateMainMessage("アイン：複合か・・・俺にも出来るようになるといいな");


                }
                else
                {
                    CallEquipmentShop();
                    mainMessage.Text = "";
                }
            }
            else if (we.TruthCompleteArea1 && we.AvailableMixSpellSkill && !buttonShinikia.Visible)
            {
                if (!we.AlreadyEquipShop)
                {
                    we.AlreadyEquipShop = true;

                    if ((mc.Level >= 21) && (!mc.FlashBlaze))
                    {
                        UpdateMainMessage("アイン：どうも、こんちわー");

                        UpdateMainMessage("ガンツ：アインよ、ちょっとこちらへ。");

                        UpdateMainMessage("アイン：あ、はい何でしょう？");

                        UpdateMainMessage("ガンツ：以前から見て、また少し強くなったと見えるな。");

                        UpdateMainMessage("アイン：いやいや、それほどでもありませんが・・・");

                        UpdateMainMessage("ガンツ：カール爵の所へ行って来なさい。");

                        UpdateMainMessage("アイン：ッゲ、またですか！？");

                        UpdateMainMessage("ガンツ：何を言っておるアイン、お主なら複合系など容易いだろう。");

                        UpdateMainMessage("アイン：う～ん・・・あの人苦手なんだよなあ・・・");

                        UpdateMainMessage("ガンツ：アインよ、精進しに行ってきなさい。");

                        UpdateMainMessage("アイン：ハイ・・・");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "ダンジョンゲート裏の広場にて";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("アイン：えっと、確かこの辺だったな・・・");

                        UpdateMainMessage("アイン：オッケー、発見発見っと！");

                        UpdateMainMessage("アイン：っしゃ、早速転送してもらおうか！");

                        UpdateMainMessage("　　ッバシュウウゥゥゥゥン");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "アインは別の場所へと飛ばされてしまった。";
                            md.ShowDialog();
                        }

                        this.buttonHanna.Visible = false;
                        this.buttonDungeon.Visible = false;
                        this.buttonRana.Visible = false;
                        this.buttonGanz.Visible = false;
                        this.buttonPotion.Visible = false;
                        this.buttonDuel.Visible = false;
                        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_SECRETFIELD_OF_FAZIL);

                        UpdateMainMessage("アイン：っとと・・・着いたみたいだな");

                        UpdateMainMessage("アイン：えっと・・・カールハンツ爵はどこに・・・");

                        UpdateMainMessage("カール：ココだ。");

                        UpdateMainMessage("アイン：って、おわぁぁ！！");

                        UpdateMainMessage("　　　『カール爵は突然見たこともないファイアボールを放ってきた！』");

                        UpdateMainMessage("アイン：ッゲ！！！");

                        UpdateMainMessage("　　　『アインはとっさの判断で身をかわした』");

                        UpdateMainMessage("カール：ホラホラホラ！");

                        UpdateMainMessage("　　　『カール爵は次々と魔法の弾丸を撃ち込んできている！！』");

                        UpdateMainMessage("アイン：っおわ！！っちょっちょ！！");

                        UpdateMainMessage("　　　『アインは５発のファイアボールらしき弾丸を何とか回避しきった』");

                        UpdateMainMessage("アイン：ッタタ、タンマタンマ！！");

                        UpdateMainMessage("アイン：あのボケ師匠も大概だけど、あんたも無茶苦茶だないきなり・・・");

                        UpdateMainMessage("カール：ッフハハ、そうとは言えしっかりと回避してるようだが。");

                        UpdateMainMessage("アイン：そりゃ、こんなもん一回一回食らってたらキリが無いだろ。");

                        UpdateMainMessage("カール：転送装置先では、敵が待ち構えてる場合も多い。気を付けるのだな。");

                        UpdateMainMessage("アイン：（ググ・・・この人やっぱり敵なんじゃ・・・）");

                        UpdateMainMessage("アイン：というか、見たこと無い魔法だったが・・・");

                        UpdateMainMessage("アイン：ひょっとして今のが！？");

                        UpdateMainMessage("カール：聖と火の複合魔法「フラッシュ・ブレイズ」だ。");

                        UpdateMainMessage("カール：やってみるが良い。");

                        UpdateMainMessage("アイン：い、いきなりですか！？");

                        UpdateMainMessage("カール：先の教え、覚えておるだろう。");

                        UpdateMainMessage("カール：教えの通りにやると良い、貴君は習得済みであると言ったハズだ。");

                        UpdateMainMessage("アイン：そ・・・そうかなあ・・・じゃあ・・・");

                        UpdateMainMessage("　　　『アインは魔法詠唱の構えを始めた』");

                        UpdateMainMessage("アイン：（こう・・・火に明かりを添えるようにして・・・）");

                        UpdateMainMessage("　　　ッバシュ！！　　　");

                        UpdateMainMessage("アイン：ッゲ！！！");

                        UpdateMainMessage("カール：まだまだだが、ひとまず出せたようだな。");

                        UpdateMainMessage("アイン：っそ、そんな本当に１回目で・・・");

                        UpdateMainMessage("カール：驚いたか。");

                        UpdateMainMessage("アイン：す・・・スゲェぜ！！　これ！！！");

                        UpdateMainMessage("カール：直感と感性で習得してきた貴君にとっては、新鮮な感覚であろう。");

                        UpdateMainMessage("アイン：・・・あの講義のおかげですかね？");

                        UpdateMainMessage("カール：当然だ。ずいぶんと無礼な質問だな。");

                        UpdateMainMessage("アイン：いやいやいや！スンマセンでした！！");

                        UpdateMainMessage("カール：今回はここまでだな、また来ると良い。");

                        UpdateMainMessage("アイン：ホントどうもありがとうございました！");

                        mc.FlashBlaze = true;
                        ShowActiveSkillSpell(mc, Database.FLASH_BLAZE);

                        if (!we.AlreadyRest)
                        {
                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_EVENING);
                        }
                        else
                        {
                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                        }
                        this.buttonHanna.Visible = true;
                        this.buttonDungeon.Visible = true;
                        this.buttonRana.Visible = true;
                        this.buttonGanz.Visible = true;
                        this.buttonPotion.Visible = true;
                        this.buttonDuel.Visible = true;


                        UpdateMainMessage("ガンツ：どうだったかね。");

                        UpdateMainMessage("アイン：・・・驚きました！！");

                        UpdateMainMessage("ガンツ：その様子、どうやら身に付けたようだね。");

                        UpdateMainMessage("アイン：これが驚きなんですよ！！");

                        UpdateMainMessage("アイン：始めから、クリーンに詠唱成功したんですよ！！");

                        UpdateMainMessage("ガンツ：よほど嬉しかったと見える。それほどかね？");

                        UpdateMainMessage("アイン：あんな体験は初めてでしたよ！！");

                        UpdateMainMessage("アイン：何せ、はじめっからですよ・・・はじめっから・・・");

                        UpdateMainMessage("ガンツ：アインよ、次からは好きなタイミングで彼の元へ訪れるがよい。");

                        UpdateMainMessage("アイン：あ、はい。また行ってみます！");

                        UpdateMainMessage("ガンツ：うむ、精進せいよ。");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "アインは「ゲート裏　転送装置」へ行けるようになりました。";
                            md.ShowDialog();
                        }
                        buttonShinikia.Visible = true;
                        we.AvailableBackGate = true;
                        this.we.alreadyCommunicateCahlhanz = true; // カール爵に教えてもらったばかりのため、Trueを指定しておく。
                    }


                }
                else
                {
                    CallEquipmentShop();
                    mainMessage.Text = "";
                }
            }
            #endregion
            #region "４階開始時"
            else if (we.TruthCompleteArea3 && !we.Truth_CommunicationGanz41)
            {
                we.Truth_CommunicationGanz41 = true;

                UpdateMainMessage("アイン：こんちわー。");

                UpdateMainMessage("ガンツ：アインよ、相変わらず元気そうじゃの。");

                UpdateMainMessage("アイン：ッハハ・・・");

                UpdateMainMessage("ガンツ：４階へは、やはり進むつもりか。");

                UpdateMainMessage("アイン：・・・　・・・");

                UpdateMainMessage("アイン：はい。");

                UpdateMainMessage("ガンツ：止めるつもりはなさそうだな。");

                UpdateMainMessage("アイン：ええ、なんとか制覇してみるつもりです。");

                UpdateMainMessage("ガンツ：ふむ、精進しなさい。");

                UpdateMainMessage("アイン：ハイ、それでは・・・");

                UpdateMainMessage("ガンツ：待ちなさい。");

                UpdateMainMessage("ガンツ：アインよ、剣を見せてくれんかね。");

                UpdateMainMessage("アイン：剣・・・？");

                UpdateMainMessage("ガンツ：練習用の剣を以前渡したであろう。");

                UpdateMainMessage("アイン：あ、ああ！　ちょっと待ってください。");

                UpdateMainMessage("アイン：ええと・・・これだ。ハイ、どうぞ");

                UpdateMainMessage("ガンツ：ふむ");

                UpdateMainMessage("ガンツ：・・・");

                string detectName = PracticeSwordLevel(mc);

                if (detectName == Database.POOR_PRACTICE_SWORD_ZERO)
                {
                    UpdateMainMessage("ガンツ：＜　" + detectName + "　＞か。");

                    UpdateMainMessage("ガンツ：剣が成長しておらんようだな。");

                    UpdateMainMessage("アイン：え・・・？");

                    UpdateMainMessage("アイン：今、成長って言いました？");

                    UpdateMainMessage("ガンツ：この剣は使い手の心の在り方を読み解き、そして共に成長してゆく。");

                    UpdateMainMessage("ガンツ：剣の主は、己の心を成長させれば、剣と共に飛躍的な進化が遂げられる。");

                    UpdateMainMessage("ガンツ：そう伝えられておる。");

                    UpdateMainMessage("アイン：そ、そうだったんですか・・・");

                    UpdateMainMessage("ガンツ：焦る事はない。興味があれば、また使ってみなさい。");

                    UpdateMainMessage("アイン：ハ、ハイ！どうもすみませんでした！");

                    UpdateMainMessage("ガンツ：謝る必要はない。");

                    UpdateMainMessage("ガンツ：アインよ、精進しなさい。");

                    UpdateMainMessage("アイン：ハイ、それでは失礼します。");

                    UpdateMainMessage("   ＜＜＜　ッバタン　（アインは武具屋の外へと出た）  ＞＞＞");

                    UpdateMainMessage("アイン：（この剣・・・そうだったのか・・・）");

                    UpdateMainMessage("アイン：（４階層の敵相手にこの状態じゃ使いもんにならねえが・・・）");

                    UpdateMainMessage("アイン：（まあ、気が向いたら使ってみるか）");
                }
                else if ((detectName == Database.POOR_PRACTICE_SWORD_1) ||
                         (detectName == Database.POOR_PRACTICE_SWORD_2) ||
                         (detectName == Database.COMMON_PRACTICE_SWORD_3))
                {
                    UpdateMainMessage("ガンツ：＜　" + detectName + "　＞か。");

                    UpdateMainMessage("ガンツ：ほんの少しだけ、成長しておるようだな。");

                    UpdateMainMessage("アイン：ええ・・・何となくだけど、少しだけマシに振る舞えるようにはなりました。");

                    UpdateMainMessage("アイン：それより今、成長って言いました？");

                    UpdateMainMessage("ガンツ：この剣は使い手の心の在り方を読み解き、そして共に成長してゆく。");

                    UpdateMainMessage("ガンツ：剣の主は、己の心を成長させれば、剣と共に飛躍的な進化が遂げられる。");

                    UpdateMainMessage("ガンツ：そう伝えられておる。");

                    UpdateMainMessage("アイン：そ、そうだったんですか・・・");

                    UpdateMainMessage("ガンツ：焦る事はない。興味があれば、また使ってみなさい。");

                    UpdateMainMessage("アイン：ハ、ハイ！どうもすみませんでした！");

                    UpdateMainMessage("ガンツ：謝る必要はない。");

                    UpdateMainMessage("ガンツ：アインよ、精進しなさい。");

                    UpdateMainMessage("アイン：ハイ、それでは失礼します。");

                    UpdateMainMessage("   ＜＜＜　ッバタン　（アインは武具屋の外へと出た）  ＞＞＞");

                    UpdateMainMessage("アイン：（この剣・・・そうだったのか・・・）");

                    UpdateMainMessage("アイン：（４階層の敵相手にこの状態じゃ使いもんにならねえが・・・）");

                    UpdateMainMessage("アイン：（まあ、気が向いたら使ってみるか）");
                }
                else if ((detectName == Database.COMMON_PRACTICE_SWORD_4) ||
                         (detectName == Database.RARE_PRACTICE_SWORD_5) ||
                         (detectName == Database.RARE_PRACTICE_SWORD_6))
                {
                    UpdateMainMessage("ガンツ：＜　" + detectName + "　＞か。");

                    UpdateMainMessage("ガンツ：なかなか、成長してきておるようだな。");

                    UpdateMainMessage("アイン：ええ・・・しかし、この剣、不思議ですよね。");

                    UpdateMainMessage("アイン：使えば使うほど熟練度が上がるっていうか・・・");

                    UpdateMainMessage("アイン：使いようによって、どんどん攻撃ダメージが上がってきてる感じがするんですよ。");

                    UpdateMainMessage("ガンツ：お主の言うとおり。");

                    UpdateMainMessage("アイン：え？");

                    UpdateMainMessage("ガンツ：この剣は使い手の心の在り方を読み解き、そして共に成長してゆく。");

                    UpdateMainMessage("ガンツ：剣の主は、己の心を成長させれば、剣と共に飛躍的な進化が遂げられる。");

                    UpdateMainMessage("ガンツ：そう伝えられておる。");

                    UpdateMainMessage("アイン：そ、そうか。どうりで・・・");

                    UpdateMainMessage("ガンツ：この調子で、その剣を使いこなしてみなさい。");

                    UpdateMainMessage("ガンツ：アイン、お主はきっと強くなれる。");

                    UpdateMainMessage("アイン：ハ、ハイ！どうもありがとうございます！");

                    UpdateMainMessage("ガンツ：謝る必要はない、精進しなさい。");

                    UpdateMainMessage("アイン：ハイ、それでは失礼します。");

                    UpdateMainMessage("   ＜＜＜　ッバタン　（アインは武具屋の外へと出た）  ＞＞＞");

                    UpdateMainMessage("アイン：（この剣・・・確かに威力がどんどん上がってきている・・・）");

                    UpdateMainMessage("アイン：（４階層、一気に使いこなせるように振舞ってみるか）");
                }
                else if (detectName == Database.EPIC_PRACTICE_SWORD_7)
                {
                    UpdateMainMessage("ガンツ：＜　" + detectName + "　＞か。");

                    UpdateMainMessage("ガンツ：アインよ。お主はこの剣が、何であるかは理解しているか？");

                    UpdateMainMessage("アイン：理解・・・ですか？");

                    UpdateMainMessage("アイン：・・・　・・・");

                    UpdateMainMessage("アイン：いえ、多分理解までは・・・");

                    UpdateMainMessage("ガンツ：ふむ、良い心構えだ。");

                    UpdateMainMessage("ガンツ：アインよ、答えはもう目の前である感覚はあるかね？");

                    UpdateMainMessage("アイン：ええ・・・正直な所、もう何となくは・・・");

                    UpdateMainMessage("ガンツ：アインよ、お主はもう十分に強くなった。");

                    UpdateMainMessage("ガンツ：アインよ、常々、精進しなさい。");

                    UpdateMainMessage("アイン：ハイ、どうもありがとうございます。");

                    UpdateMainMessage("   ＜＜＜　ッバタン　（アインは武具屋の外へと出た）  ＞＞＞");

                    UpdateMainMessage("アイン：（この剣への・・・理解・・・）");

                    UpdateMainMessage("アイン：（あと一息な感じはしている・・・）");

                    UpdateMainMessage("アイン：（もう一超え頑張るとするか！）");
                }
                else if (detectName == Database.LEGENDARY_FELTUS)
                {
                    UpdateMainMessage("ガンツ：＜　" + detectName + "　＞か。");

                    UpdateMainMessage("ガンツ：よくぞここまで。見事だ。");

                    UpdateMainMessage("アイン：いえ、これは俺が単に逃げ続けていただけですから。");

                    UpdateMainMessage("ガンツ：そうではない。向かい続けてきた結果だ。卑下をする事はない。");

                    UpdateMainMessage("アイン：はい。");

                    UpdateMainMessage("ガンツ：フェルトゥーシュ、今お主は、その手に所持しておる。");

                    UpdateMainMessage("アイン：ええ、確かにこの手に。");

                    UpdateMainMessage("ガンツ：恐る事なく、進めるが良い。");

                    UpdateMainMessage("アイン：はい。");

                    UpdateMainMessage("ガンツ：決して");

                    UpdateMainMessage("ガンツ：決して、負けるな、アインよ。");

                    UpdateMainMessage("ガンツ：精進を怠らず、進めよ。アイン・ウォーレンス。");

                    UpdateMainMessage("アイン：分かりました！");

                    UpdateMainMessage("   ＜＜＜　ッバタン　（アインは武具屋の外へと出た）  ＞＞＞");

                    UpdateMainMessage("アイン：（フェルトゥーシュにより俺は・・・）");

                    UpdateMainMessage("アイン：（分かってる、進むんだ）");

                    UpdateMainMessage("アイン：（俺は必ず、この手で）");

                    UpdateMainMessage("アイン：（決着を付けてみせる。）");
                }
            }
            #endregion
            #region "３階開始時"
            else if (we.TruthCompleteArea2 && !we.Truth_CommunicationGanz31)
            {
                we.Truth_CommunicationGanz31 = true;

                if (!we.AlreadyEquipShop)
                {
                    UpdateMainMessage("アイン：こんちわー。");

                    UpdateMainMessage("ガンツ：アインよ。いよいよ３階へと進むつもりか。");

                    UpdateMainMessage("アイン：はい、今日からスタートさせるつもりです。");

                    UpdateMainMessage("ガンツ：ふむ、ワシから言う事は特にない。");

                    UpdateMainMessage("ガンツ：アインよ、精進しなさい。");

                    UpdateMainMessage("アイン：あっと、ハイ！ありがとうございました！！");

                    UpdateMainMessage("ガンツ：だが、一つ言っておかねばならん事がある。");

                    UpdateMainMessage("アイン：（ッゲ、特に無いと言ったのに・・・この展開は・・・）");

                    UpdateMainMessage("ガンツ：アインよ、どこへ向かう？");

                    UpdateMainMessage("アイン：どこって、ダンジョン３階です。");

                    UpdateMainMessage("ガンツ：バカの振りは不要。しっかりと答えなさい。");

                    UpdateMainMessage("アイン：う～ん、そう言われても・・・");

                    if (GroundOne.WE2.TruthRecollection1 && GroundOne.WE2.TruthRecollection2)
                    {
                        UpdateMainMessage("アイン：始まりの地へ。");

                        UpdateMainMessage("アイン：広大な草原と無限に拡がる大空。");

                        UpdateMainMessage("アイン：そこで俺は、ケリをつける。");

                        UpdateMainMessage("ガンツ：・・・・・・ふむ・・・・・・");

                        UpdateMainMessage("ガンツ：精進しなさい、アインよ。");

                        UpdateMainMessage("ガンツ：決して負けてはならん。よいな？");

                        UpdateMainMessage("アイン：ああ、任せてくれ。");

                        UpdateMainMessage("アイン：絶対に今度こそ。");

                        UpdateMainMessage("ガンツ：うむ、行ってきなさい。気をつけてな。");

                        UpdateMainMessage("アイン：ああ、了解！");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：ダンジョン最下層だ。");

                        UpdateMainMessage("アイン：俺は絶対にこのダンジョンを制覇してみせる！");

                        UpdateMainMessage("ガンツ：ふむ、その勢い、忘れぬようにな。");

                        UpdateMainMessage("アイン：ガンツ叔父さんと話していると元気が出るよ、サンキュー。");

                        UpdateMainMessage("ガンツ：無理だけはせぬようにな、毎日をしっかり生きなさい。");

                        UpdateMainMessage("アイン：ああ、じゃあ行ってくるぜ！");
                    }
                    we.AlreadyEquipShop = true;
                }
                else
                {
                    CallEquipmentShop();
                    mainMessage.Text = "";
                }
            }
            #endregion
            #region "２階開始時"
            else if (we.TruthCompleteArea1 && !we.Truth_CommunicationGanz21)
            {
                if (!we.AlreadyEquipShop)
                {
                    UpdateMainMessage("アイン：こんちわー。");

                    UpdateMainMessage("ガンツ：アイン。２階へ向かうようだな。");

                    UpdateMainMessage("アイン：あ、ええ。今日からそのつもりです。");

                    UpdateMainMessage("ガンツ：ならば、これでも持って行くと良いだろう。");

                    CallSomeMessageWithAnimation("アインは" + Database.POOR_PRACTICE_SWORD_ZERO + "を手に入れた。");

                    UpdateMainMessage("アイン：これは・・・練習用の剣？");

                    UpdateMainMessage("ガンツ：その武器には特殊な効果が封じ込められておる。");

                    UpdateMainMessage("アイン：そうなんですか？");

                    UpdateMainMessage("ガンツ：ワシなりに考えてみたが、アインよ。");

                    UpdateMainMessage("アイン：ハイ。");

                    UpdateMainMessage("ガンツ：・・・いや、お前なりに使ってみると良い。");

                    UpdateMainMessage("アイン：えっと、どういう事でしょうか？");

                    UpdateMainMessage("ガンツ：アインよ、精進しなさい。");

                    UpdateMainMessage("アイン：あっと、ハイ！ありがとうございました！！");

                    UpdateMainMessage("   ＜＜＜　ッバタン　（アインは武具屋の外へと出た）  ＞＞＞");

                    UpdateMainMessage("アイン：（どうみてもこれは単なる練習用の剣だが・・・）");

                    UpdateMainMessage("アイン：（・・・いや、そんなわけがねえよな）");

                    UpdateMainMessage("アイン：（っしゃ、せっかくなんだし、使ってみるとするか！）");

                    GetItemFullCheck(mc, Database.POOR_PRACTICE_SWORD_ZERO);

                    we.Truth_CommunicationGanz21 = true;
                    we.AlreadyEquipShop = true;
                }
                else
                {
                    CallEquipmentShop();
                    mainMessage.Text = "";
                }
            }
            #endregion
            #region "１日目"
            else if (this.firstDay >= 1 && !we.Truth_CommunicationGanz1)
            {
                // if (!we.AlreadyRest) //  1日目はアインが起きたばかりなので、本フラグを未使用とします。
                if (!we.AlreadyEquipShop)
                {
                    UpdateMainMessage("アイン：ガンツ叔父さん、いますかー？");

                    UpdateMainMessage("ガンツ：アインか。よく来てくれた。");

                    UpdateMainMessage("アイン：武具店、開いてますか？");

                    UpdateMainMessage("ガンツ：ああ、今月はヴァスタ爺からの物資配給が良くてな。開店しておるので見ていくと良い。");

                    UpdateMainMessage("アイン：っしゃ！やったぜ！じゃあ早速見せてもらうとするぜ！！");

                    we.AlreadyEquipShop = true;
                    we.AvailableEquipShop = true;
                    we.Truth_CommunicationGanz1 = true; // 初回一日目のみ、ラナ、ガンツ、ハンナの会話を聞いたかどうか判定するため、ここでTRUEとします。

                    CallEquipmentShop();
                    mainMessage.Text = "";

                    UpdateMainMessage("アイン：叔父さん、また来るぜ。");

                    UpdateMainMessage("ガンツ：アインよ。これからダンジョンへ向かうのだな？");

                    UpdateMainMessage("アイン：はい。");

                    UpdateMainMessage("ガンツ：アインよ、では心構えを一つ教えて進ぜよう。");

                    UpdateMainMessage("アイン：ッマジっすか！？ハハ、やったぜ！ありがとうございます！");

                    UpdateMainMessage("ガンツ：ダンジョンで殺したモンスターからは、役に立つ材料がいくつも採れる。");

                    UpdateMainMessage("アイン：ッハイ！");

                    UpdateMainMessage("ガンツ：モンスターより得られる部材、素材をワシの所へ持ってくると良い。");

                    UpdateMainMessage("アイン：ッハイ！！");

                    UpdateMainMessage("ガンツ：それら部材、素材を組み合わせ、ワシが腕によりをかけて新しい武具を作ろう。");

                    UpdateMainMessage("アイン：ッハイ！！！");

                    UpdateMainMessage("ガンツ：アインよ、精進しなさい。");

                    UpdateMainMessage("ガンツ：では頼んだぞ。");

                    UpdateMainMessage("アイン：ッハイ！　ありがとうございました！！");

                    UpdateMainMessage("   ＜＜＜　ッバタン　（アインは武具屋の外へと出た）  ＞＞＞");

                    UpdateMainMessage("アイン：（待てよ・・・これはひょっとして・・・）");

                    UpdateMainMessage("アイン：（最後結局「精進しなさい」しか言ってねえよな・・・）");

                    UpdateMainMessage("アイン：（でもまあ、ガンツ叔父さんの新しい武具生成か。楽しみだな。）");

                    UpdateMainMessage("アイン：（モンスターから得られた部材・素材はガンツ叔父さんのトコへ持っていくとするか。）");
                }
                else
                {
                    CallEquipmentShop();
                    mainMessage.Text = "";
                }
            }
            #endregion
            #region "その他"
            else
            {
                CallEquipmentShop();
                mainMessage.Text = "";
            }
            #endregion
        }

        private void GoToKahlhanz()
        {
            this.buttonHanna.Visible = false;
            this.buttonDungeon.Visible = false;
            this.buttonRana.Visible = false;
            this.buttonGanz.Visible = false;
            this.buttonPotion.Visible = false;
            this.buttonDuel.Visible = false;
            this.buttonShinikia.Visible = false;
            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_SECRETFIELD_OF_FAZIL);
        }
        private void BackToTown()
        {
            if (!we.AlreadyRest)
            {
                ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_EVENING);
            }
            else
            {
                ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
            }
            this.buttonHanna.Visible = true;
            this.buttonDungeon.Visible = true;
            this.buttonRana.Visible = true;
            this.buttonGanz.Visible = true;
            this.buttonPotion.Visible = true;
            this.buttonDuel.Visible = true;
            this.buttonShinikia.Visible = true;
        }

        // カール爵の講義 / ファージル宮殿
        private void button8_Click(object sender, EventArgs e)
        {
            #region "ファージル宮殿 or カールハンツ爵の訓練場を選択"
            if (we.AvailableFazilCastle)
            {
                using (SelectDungeon sd = new SelectDungeon())
                {
                    sd.StartPosition = FormStartPosition.Manual;
                    sd.Location = new Point(this.Location.X + 20, this.Location.Y + 525);
                    sd.MaxSelectable = 2;
                    sd.FirstName = "カール爵の訓練場";
                    sd.SecondName = "ファージル宮殿";
                    sd.AdjustWidth = 200;
                    sd.ShowDialog();
                    if (sd.TargetDungeon == -1)
                    {
                        return;
                    }
                    else if (sd.TargetDungeon == 1)
                    {
                        if (we.alreadyCommunicateCahlhanz)
                        {
                            UpdateMainMessage("アイン：カールハンツ爵にはまた今度教えてもらうとしよう。", true);
                            return;
                        }
                        else
                        {
                            UpdateMainMessage("アイン：カール爵の訓練場へ赴くとするか。", true);
                            System.Threading.Thread.Sleep(1000);
                        }
                    }
                    else
                    {
                        if (!we.AvailableOneDayItem && we.AlreadyCommunicateFazilCastle)
                        {
                            UpdateMainMessage("アイン：ファージル宮殿は、また今度行ってみよう。", true);
                            return;
                        }
                        if (we.AvailableOneDayItem && we.AlreadyCommunicateFazilCastle && we.AlreadyGetOneDayItem && we.AlreadyGetMonsterHunt)
                        {
                            UpdateMainMessage("アイン：ファージル宮殿は、また今度行ってみよう。", true);
                            return;
                        }

                        #region "初めてのファージル宮殿"
                        if (!we.Truth_Communication_FC31)
                        {
                            UpdateMainMessage("ラナ：あっ♪　ファージル宮殿に行ってみるの？");

                            UpdateMainMessage("アイン：ああ、そのつもりだ。なんだヤケに楽しそうだな。");

                            UpdateMainMessage("ラナ：だって、憧れのエルミ様に会える可能性があるんだもの、楽しくなるわよ♪");

                            UpdateMainMessage("アイン：なに・・・そんなものなのか？");

                            UpdateMainMessage("ラナ：そりゃそうよ。女性なら誰でも憧れるわ。バカアインが知らなすぎなだけよ。");

                            UpdateMainMessage("アイン：いやまあ・・・そういう事は俺には確かに分からん。");

                            UpdateMainMessage("ラナ：まあ、イイわよ分からなくても。っささ、行きましょ♪　");

                            UpdateMainMessage("アイン：ったく、めずらしく機嫌が良いな。まあ行くとするか！");

                            UpdateMainMessage("ラナ：”めずらしく”ないからね、いつも機嫌良いでしょ♪　");

                            UpdateMainMessage("アイン：わ、分かった分かった。ッハハハ・・・行こうぜ。");

                            System.Threading.Thread.Sleep(1000);
                            CallFazilCastle();

                            UpdateMainMessage("ラナ：じゃあ、明日の朝だからね。忘れないでよねホント。");

                            UpdateMainMessage("アイン：ああ、了解了解！");
                        }
                        else if (!we.Truth_Communication_FC32)
                        {
                            UpdateMainMessage("ラナ：じゃあ、ファージル宮殿に行きましょ♪");

                            UpdateMainMessage("アイン：っしゃ、国王様、王妃様とご対面だな。");

                            UpdateMainMessage("アイン：じゃあ、転送するぜ！");

                            System.Threading.Thread.Sleep(1000);
                            CallFazilCastle();

                            UpdateMainMessage("アイン：ふう、戻りっと。");

                            UpdateMainMessage("ラナ：うーん・・・");

                            UpdateMainMessage("アイン：なんだ、どうかしたか？");

                            UpdateMainMessage("ラナ：あれのどこが礼儀に当たるワケなのか、ちょっと教えてもらえないかしら？");

                            UpdateMainMessage("アイン：ああ、その件か。");

                            UpdateMainMessage("アイン：なんて言うんだろうな。");

                            UpdateMainMessage("アイン：要件だけを言ったとする。");

                            UpdateMainMessage("ラナ：うん。");

                            UpdateMainMessage("アイン：その要件に相手が応えてくれたとする。");

                            UpdateMainMessage("ラナ：うんうん。");

                            UpdateMainMessage("アイン：っで、要件は満たされるワケだ。");

                            UpdateMainMessage("ラナ：そうよね、それが目的なんだから。");

                            UpdateMainMessage("アイン：それじゃつまんねえだろ？");

                            UpdateMainMessage("アイン：だから一旦要件は置いといて、次の機会にするのさ。");

                            UpdateMainMessage("ラナ：なんで、そーなるのよ？　意味がわからないわ。");

                            UpdateMainMessage("アイン：なんでって言われてもな・・・何となくとしか・・・");

                            UpdateMainMessage("ラナ：う～ん・・・");

                            UpdateMainMessage("ラナ：ダメ、全っ然わからないわ。");

                            UpdateMainMessage("アイン：ハハハ・・・悪い悪い。");

                            UpdateMainMessage("ラナ：まあ、良いわ。あのサンディって人も堅苦しい感じだったし、これでちょうど良いのかも知れないわね。");

                            UpdateMainMessage("アイン：まあまあ、やり方なんて人それぞれさ。楽しく行こうぜ！");

                            UpdateMainMessage("ラナ：はああぁ・・・別にいいけど、次からは頼むわねホント。");

                            UpdateMainMessage("アイン：了解了解！　任せておけ！");
                        }
                        #endregion
                        #region "２度目以降の通常入城"
                        else
                        {
                            UpdateMainMessage("アイン：おし、ファージル宮殿にでも行ってみるか。");

                            System.Threading.Thread.Sleep(1000);
                            CallFazilCastle();
                        }
                        #endregion
                        return;
                    }
                }
            }
            #endregion
            #region "四階開始時"
            if (we.TruthCompleteArea3 && !we.Truth_CommunicationSinikia41 && !we.alreadyCommunicateCahlhanz)
            {
                we.Truth_CommunicationSinikia41 = true;
                we.alreadyCommunicateCahlhanz = true;
                GoToKahlhanz();

                UpdateMainMessage("アイン：っとと・・・着いたみたいだな");

                UpdateMainMessage("アイン：あのすいません？");

                UpdateMainMessage("アイン：・・・カール先生？");

                UpdateMainMessage("アイン：・・・");

                UpdateMainMessage("アイン：（・・・居るのはわかるんだが・・・）");

                UpdateMainMessage("アイン：・・・");

                UpdateMainMessage("アイン：カール先生、出てくてきれ。");

                UpdateMainMessage("アイン：ちょっと話があるんだ、頼む。");

                UpdateMainMessage("アイン：・・・");

                UpdateMainMessage("　　　『ッバシュ！！』（カールは一瞬でその場に姿を現した！");

                UpdateMainMessage("カール：貴君か。");

                UpdateMainMessage("アイン：頼む、一生のお願いだ。教えてくれ。");

                UpdateMainMessage("カール：言ってみるが良い。");

                UpdateMainMessage("アイン：FiveSeekerに【強さ】についてだ。");

                UpdateMainMessage("カール：【強さ】への問いかけか。申してみよ。");

                UpdateMainMessage("アイン：FiveSeeker達はどうしてそこまでの強さを手に入れたんだ？");

                UpdateMainMessage("アイン：頼む、教えてくれ。");

                UpdateMainMessage("カール：明確な解は無い。");

                UpdateMainMessage("カール：全ては日々の積み重ね。");

                UpdateMainMessage("アイン：それは俺もラナもやってるつもりだ。どこが違う？");

                UpdateMainMessage("カール：練習量はおそらく同程度。その質もおそらく同じであろう。");

                UpdateMainMessage("アイン：・・・");

                UpdateMainMessage("アイン：論点を変えさせてくれ。");

                UpdateMainMessage("アイン：カール先生は魔導部門の専門職だよな？");

                UpdateMainMessage("カール：いかにも。");

                UpdateMainMessage("アイン：じゃあどうして、そこまでのスピードが出せるんだ？");

                UpdateMainMessage("アイン：技に長けているヴェルゼとほぼ同クラスのスピードな気がするんだが・・・");

                UpdateMainMessage("カール：アーティの事か。");

                UpdateMainMessage("カール：我自身が、奴ほどのスピードを引き出す事はない。");

                UpdateMainMessage("アイン：いや、ヴェルゼほどじゃないにしてもですよ。");

                UpdateMainMessage("アイン：それにしたって、早すぎだ。");

                UpdateMainMessage("アイン：何か強さの秘密があるって事なんじゃないですか？");

                UpdateMainMessage("カール：基礎的な鍛練は怠る事は決してない。");

                UpdateMainMessage("カール：スピードを上げる魔法も多種多様。それに加え基本的な速度を引きだす訓練は日々の積み重ね。");

                UpdateMainMessage("カール：そのことは貴君とて重々承知のはず。違うかね。");

                UpdateMainMessage("アイン：ま、まあそりゃそうだが・・・");

                UpdateMainMessage("カール：貴君の言う【強さ】という定義は、何を問おうとしておる？");

                UpdateMainMessage("アイン：う～ん・・・そう言われるとな・・・");

                UpdateMainMessage("カール：完全無欠な強さなど、この世には存在しない。");

                UpdateMainMessage("カール：日々の鍛練、そして、幅広い知識の習得、加え・・・");

                UpdateMainMessage("アイン：い、いやいやいや。ちょっとタイム！");

                UpdateMainMessage("アイン：そういう話は嫌と言うほど聞いてるんだ。そういう話じゃねえんだ。頼む！");

                UpdateMainMessage("カール：では、今一度申してみよ。");

                UpdateMainMessage("カール：貴君の知りたい【強さ】とは何か？");

                UpdateMainMessage("アイン：・・・　・・・　・・・");

                UpdateMainMessage("アイン：・・・　・・・");

                UpdateMainMessage("アイン：・・・");
                
                UpdateMainMessage("アイン：カール先生、ファイア・ボール２連発を一度見せてくれないか？");

                UpdateMainMessage("カール：容易きこと、では行くぞ。");

                UpdateMainMessage("　＜＜　カールは、その場で体位をほんの少しだけ変化させ・・・　＞＞");

                UpdateMainMessage("カール：ッファイア・ボール");

                UpdateMainMessage("　＜＜　ッボ、ッボシュウゥゥン・・・　＞＞");

                UpdateMainMessage("アイン：・・・　・・・");

                UpdateMainMessage("アイン：頼む！　もう一回だけ！");

                UpdateMainMessage("カール：ッフ。");

                UpdateMainMessage("カール：ッフハハハハ、貴君は本当に面白い。");

                UpdateMainMessage("カール：良いだろう、では行くぞ。");

                UpdateMainMessage("　＜＜　カールは、左肩の部分をほんの少しだけ揺らし始め・・・　＞＞");

                UpdateMainMessage("アイン：ッタ、タイム！そこのそれ！！");

                UpdateMainMessage("カール：っむ。");

                UpdateMainMessage("アイン：その時点で、詠唱は始まっているのか？");

                UpdateMainMessage("カール：まだだ。");

                UpdateMainMessage("アイン：っす、すまねえ・・・止めちまって。今度は止めねえから。");

                UpdateMainMessage("カール：良い、ではもう一度行くぞ。");

                UpdateMainMessage("　＜＜　カールは、右上の裾を微かに動作させ・・・　＞＞");

                UpdateMainMessage("アイン：（・・・毎回モーションが微妙に違う・・・）");

                UpdateMainMessage("カール：ッファイア・ボール");

                UpdateMainMessage("アイン：（やっぱり・・・詠唱タイミングで既に炎の塊が2つ出ている。）");

                UpdateMainMessage("　＜＜　ッボ　＞＞");

                UpdateMainMessage("アイン：（その瞬間に２つ同時ってワケでもねえから・・・）");

                UpdateMainMessage("　＜＜　ッボシュウウウゥゥゥン・・・　＞＞");

                UpdateMainMessage("アイン：（ゲイル・ウィンドでもねえよな・・・）");
                
                UpdateMainMessage("アイン：（ってことは、変なカラクリや小細工はねえな・・・）");
                
                UpdateMainMessage("アイン：（どうなってんだ・・・魔道士のくせに、このスピード・・・）");

                UpdateMainMessage("カール：以上");

                UpdateMainMessage("アイン：いやぁ・・・");

                UpdateMainMessage("アイン：やっぱ、スゲェよ・・・信じられねえぜ。");

                UpdateMainMessage("アイン：カール先生、やっぱ強すぎだぜ。");

                UpdateMainMessage("カール：貴君の問いに対する解答は見つけられたか？");

                UpdateMainMessage("アイン：いやっ・・・");
                
                UpdateMainMessage("アイン：ぜんっぜん分かんねぇ！！");
                
                UpdateMainMessage("アイン：アーッハッハッハ！");

                UpdateMainMessage("カール：ッフハハ、おかしな奴だ。");

                UpdateMainMessage("アイン：何かほんの少しでも掴めるかと思ったんですが、俺もまだまだです。");
                
                UpdateMainMessage("アイン：いつか絶対に、ボケ師匠やカール先生に追いついて見せます！");

                UpdateMainMessage("カール：知識は全ての源、忘れるな。");

                UpdateMainMessage("アイン：はい、どうもありがとうございました！");

                BackToTown();
            }
            #endregion
            #region "三階開始時"
            else if (we.TruthCompleteArea2 && !we.Truth_CommunicationSinikia31 && !we.alreadyCommunicateCahlhanz)
            {
                if (!we.Truth_CommunicationLana31)
                {
                    UpdateMainMessage("アイン：いや・・・その前に、ラナにひとまず挨拶しておくか。", true);
                    return;
                }
                if (!we.Truth_CommunicationOl31)
                {
                    UpdateMainMessage("アイン：いや・・・その前に、師匠にひとまず挨拶しておくか。", true);
                    return;
                }
                we.Truth_CommunicationSinikia31 = true;
                we.alreadyCommunicateCahlhanz = true;
                we.AlreadyCommunicateFazilCastle = true;

                UpdateMainMessage("アイン：よし、転送装置ゲートに着いたぜ。");

                UpdateMainMessage("ラナ：確か、この辺りの木の枝よ・・・えっとね。");

                UpdateMainMessage("ラナ：っあ、あったわ、コレね♪");

                UpdateMainMessage("アイン：どうみても普通の枝だけどな。");

                UpdateMainMessage("ラナ：単なる枝だったら少し曲がるぐらいでしょ？");

                UpdateMainMessage("アイン：まあ、そうだけどな。無理にヘシ折るなよ？");

                UpdateMainMessage("ラナ：ちょっと、失礼ね。植物系はバカアインみたいに頑丈じゃないんだから。");

                UpdateMainMessage("アイン：（俺だったらヘシ折るつもりなのか・・・）");

                UpdateMainMessage("ラナ：っよっと♪");

                UpdateMainMessage("　　　【転送装置ゲートが青白く光り始めた！】");

                UpdateMainMessage("　　　【・・・ゥゥゥブゥヴウウゥゥゥン・・・】");

                UpdateMainMessage("ラナ：ッホラ、見て見て！　あたりでしょ♪");

                UpdateMainMessage("アイン：へえ、何か紋様が少しだけ変わってるな。すげぇじゃねえか！");

                UpdateMainMessage("ラナ：これで多分ファージル宮殿へ通じるゲートになった筈よ、行ってみましょ♪");

                UpdateMainMessage("アイン：おっしゃ、それじゃ早速行くか！");

                UpdateMainMessage("　　　【アインとラナは転送装置ゲートへと足を運んだ・・・】");

                UpdateMainMessage("　　　【その瞬間だった】");

                Blackout();

                UpdateMainMessage("　　　【・・・ッパキイィィィンン・・・】");

                UpdateMainMessage("アイン：（ッゲ、なんだ今の音！！）");

                UpdateMainMessage("　　　【イィィィンン・・・】");

                UpdateMainMessage("アイン：（な！？　何だ大丈夫なのかよ、この転送！？）");

                UpdateMainMessage("　　　【アインは突如、転送ゲートから放り出された！！】");

                ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_FIELD_OF_FIRSTPLACE);
                this.BackColor = Color.WhiteSmoke;
                this.Update();

                UpdateMainMessage("アイン：ッイデ！！！");

                UpdateMainMessage("アイン：ッツツツ・・・何かムチャクチャな転送だったな・・・");

                UpdateMainMessage("アイン：って、どこなんだよ、ココは・・・");

                UpdateMainMessage("　　　＜＜＜　その時、一つの風がアインの全体へ触れた。そんな気がした。　＞＞＞　　");

                UpdateMainMessage("アイン：・・・！？");

                UpdateMainMessage("アイン：・・・");

                UpdateMainMessage("アイン：・・・");

                UpdateMainMessage("アイン：気のせいか");

                UpdateMainMessage("　　　『アイン君。　　始めまして。　　だね。』　　");

                UpdateMainMessage("　　【【【　その時、アインは自分が死体となるのを直感で感じた。　】】】");

                UpdateMainMessage("アイン：ッな！！");

                UpdateMainMessage("　　＜＜＜　アインは【直感】し、即座に後ろへ振り向き、剣を突っ立てた。　＞＞＞　　");

                UpdateMainMessage("？？？：ようこそ、ファージル宮殿周辺のエスリミア草原区域へ。");

                UpdateMainMessage("　　＜＜＜　しかし、後ろには既に人の気配のみが存在するだけだった。　＞＞＞　　");

                UpdateMainMessage("アイン：（ッバ・・・馬鹿な！！！）");

                UpdateMainMessage("　　【【【　アインは冷え切った汗を拭えないまま、死に対する激しい葛藤を続けている。　】】】");

                UpdateMainMessage("アイン：（・・・追従しきれねえ・・・ウソだろ！？）");

                UpdateMainMessage("？？？：アイン君、すみません、そんなに警戒しなくても良いですよ。");

                UpdateMainMessage("アイン：！！！");

                UpdateMainMessage("　　　＜＜＜　一つの優しい風がまた、アインの全体へ触れた。　＞＞＞　　");

                UpdateMainMessage("？？？：ボクの名はVerze Artie。");

                UpdateMainMessage("　　　＜＜＜　声が先に届き ＞＞＞    ");

                UpdateMainMessage("？？？：よろしくね、アイン君。");

                UpdateMainMessage("　　　＜＜＜　そしてようやく、アインに彼を目視する権利が与えられた。　＞＞＞　　");

                UpdateMainMessage("アイン：で・・・伝説のFiveSeeker、ヴェルゼ・アーティ！？");

                UpdateMainMessage("ヴェルゼ：その呼び名は止めてください。単純にヴェルゼで構いませんよ。");

                UpdateMainMessage("アイン：ど、どこなんだココは！？");

                UpdateMainMessage("ヴェルゼ：先ほども述べましたが、ファージル宮殿周辺のエスリミア草原区域です。");

                UpdateMainMessage("　　＜＜＜　アインは少しずつ、死の直感が無くなっていくのを感じた。　＞＞＞");

                UpdateMainMessage("アイン：エスリミア草原区域・・・");

                UpdateMainMessage("アイン：ああ、ファージル南街道の少し右に行ったあの辺りか。");

                UpdateMainMessage("アイン：・・・あれ！？　ラナはどこに行ったんだ？");

                UpdateMainMessage("ヴェルゼ：彼女でしたら、あちらで休息の寝息を立てていますよ。");

                UpdateMainMessage("ヴェルゼ：転送装置からの脱出時、少し強い圧力が加わったみたいですね。");

                UpdateMainMessage("アイン：だ、大丈夫なのかよ！？");

                UpdateMainMessage("ヴェルゼ：ええ、命に問題はありません。軽い気絶をしただけの様です。");

                UpdateMainMessage("アイン：ハアアァァァ・・・・まいったぜ、ホント。焦らせるなよ・・・");

                UpdateMainMessage("アイン：まったく、ラナの奴はたまに変な所に首を突っ込もうとするから・・・");

                UpdateMainMessage("ヴェルゼ：ところで、どうしてココへ来ようと考えたのですか？");

                UpdateMainMessage("アイン：ああ、師匠が転送装置の前で、奇妙な枝を触ってるのをラナが目撃してだな・・・");

                UpdateMainMessage("アイン：それでココに来ちまったってわけだ。。。");

                UpdateMainMessage("アイン：っと、あ！！");

                UpdateMainMessage("アイン：っす、スミマセン！　何か軽い口調で喋ってしまって！　申し訳ないです！");

                UpdateMainMessage("ヴェルゼ：いや、気にしないでください。アイン君の事はオル・ランディスから聞いていますから。");

                UpdateMainMessage("アイン：あ、ッハハハ・・・そうなんだ。いやいや、でも本当にすみません。");

                UpdateMainMessage("ヴェルゼ：ところで、アイン君。");

                UpdateMainMessage("アイン：はい、なんでしょう？");

                UpdateMainMessage("　　【【【　その時、アインは　】】】");

                UpdateMainMessage("　　【【【　再び、得たいの知れない死の直感を全体で感じ始めた！　】】】");

                UpdateMainMessage("アイン：（ックソ・・・何だこの感触は！？）");

                UpdateMainMessage("アイン：（この人から敵意は全くと言っていいほど感じ取る事ができない。これは確かだ。）");

                UpdateMainMessage("アイン：（なのに、何故か死の感触が強く迫ってくる・・・）");

                UpdateMainMessage("アイン：（いつ俺の後ろを取ってくるか分からねえ・・・そんな恐怖だ。）");

                UpdateMainMessage("ヴェルゼ：実は、オル・ランディスから依頼されている事があります。");

                UpdateMainMessage("アイン：ッゲ・・・あの師匠からですか！？");

                UpdateMainMessage("アイン：まさか、地獄のトレーニングとか・・・");

                UpdateMainMessage("ヴェルゼ：はい、その通りです。");

                UpdateMainMessage("アイン：ゲゲェ！　マジかよ！？");

                UpdateMainMessage("アイン：っとと、言葉がつい・・・すいません。");

                UpdateMainMessage("ヴェルゼ：本当に気にしなくて良いですよ、いつも通りの感覚で喋ってください。");

                UpdateMainMessage("ヴェルゼ：そうでないと、本当のアイン君を確認出来ませんからね。");

                UpdateMainMessage("アイン：あ、ああぁ・・・了解了解。");

                UpdateMainMessage("アイン：じゃあ、お言葉に甘えて。");

                UpdateMainMessage("アイン：で、トレーニングってのはどういうのをやるつもりなんだ？");

                UpdateMainMessage("ヴェルゼ：簡単ですよ。");

                UpdateMainMessage("ヴェルゼ：ボクとDUEL勝負と行きませんか？");

                UpdateMainMessage("アイン：え！いきなりDUELですか！？");

                UpdateMainMessage("ヴェルゼ：はい、よろしくお願いしたいと思います。");

                UpdateMainMessage("アイン：ウ・・・ヴ～ン・・・");

                UpdateMainMessage("アイン：い、いやいや。了解です！よろしくお願いします！");

                UpdateMainMessage("ヴェルゼ：ありがとうございます。");

                UpdateMainMessage("ヴェルゼ：それでは早速。");

                UpdateMainMessage("アイン：あ、っちょっとタンマ！！");

                UpdateMainMessage("ヴェルゼ：はい、なんでしょう？");

                UpdateMainMessage("アイン：（ッホ・・・ヴェルゼさんはちゃんと待ってくれるんだな・・・）");

                UpdateMainMessage("アイン：この勝負、勝ち負けに応じて何か発生するのか？");

                UpdateMainMessage("ヴェルゼ：いいえ、特に何もありませんよ。");

                UpdateMainMessage("ヴェルゼ：純粋な腕試し、それだけです。");

                UpdateMainMessage("アイン：そうか、まあDUELは元々そういうもんだしな。");

                UpdateMainMessage("アイン：っしゃ、オーケーオーケー！　いつでも良いぜ！");

                UpdateMainMessage("ヴェルゼ：では、始めるとしましょう。");

                UpdateMainMessage("ヴェルゼ：３");

                UpdateMainMessage("アイン：２");

                UpdateMainMessage("ヴェルゼ：１");

                bool result = BattleStart(Database.VERZE_ARTIE, true);
                if (result)
                {
                    UpdateMainMessage("アイン：っしゃ！このタイミングだ！！");

                    UpdateMainMessage("ヴェルゼ：さて、どうでしょう。");

                    UpdateMainMessage("　　＜＜＜　アインが止めの一撃を繰り出したその瞬間！　＞＞＞");

                    UpdateMainMessage("　　＜＜＜　ッバシュ！！　＞＞＞");

                    UpdateMainMessage("アイン：っき！　消えただと！？");

                    UpdateMainMessage("　　【【【　アインは、異常なまでの死の直感と旋律を感じた！！　】】】");

                    UpdateMainMessage("アイン：ヤッ、ヤベェ！！！！！");

                    UpdateMainMessage("ヴェルゼ：全ては一つとなりて");

                    UpdateMainMessage("　　＜＜＜　・・・声のみが響き渡り・・・　＞＞＞");

                    UpdateMainMessage("ヴェルゼ：その一つは全てへと拡散する。");

                    UpdateMainMessage("　　＜＜＜　彼の存在がアインの視界に入った瞬間　＞＞＞");

                    UpdateMainMessage("ヴェルゼ：【叡技】Ladarynte・Caotic・Schema！");

                    UpdateMainMessage("　　＜＜＜　ファシュゥン・・・　＞＞＞");

                    UpdateMainMessage("アイン：・・・？");

                    UpdateMainMessage("　　＜＜＜　刹那の静寂　＞＞＞");

                    UpdateMainMessage("アイン：・・");

                    UpdateMainMessage("　　＜＜＜　一瞬だった　＞＞＞");

                    UpdateMainMessage("アイン：！！　ッしまっ！！！");

                    UpdateMainMessage("　　＜＜＜　声を発する間もなく　＞＞＞");

                    UpdateMainMessage("　　＜＜＜　ッドシュッ　＞＞＞");

                    UpdateMainMessage("アイン：ッグ！！");

                    UpdateMainMessage("　　＜＜＜　ッドシュ、ッドシュ！　＞＞＞");

                    UpdateMainMessage("アイン：ッグハ！　よ、避け！！");

                    UpdateMainMessage("　　＜＜＜　ッドッドドドシュ！　＞＞＞");

                    UpdateMainMessage("アイン：ッグ、ッゲホ！！ウグッ！");

                    UpdateMainMessage("　　＜＜＜　その攻撃数、方角、タイミング　＞＞＞");

                    UpdateMainMessage("　　＜＜＜　無限連鎖　＞＞＞");

                    UpdateMainMessage("　　＜＜＜　ッドシュドシュドシュッドシュドシュドシュドドドシュ！　＞＞＞");

                    UpdateMainMessage("アイン：ッグァ！！・・・ァ・・・");

                    UpdateMainMessage("ヴェルゼ：とどめです、ハアァァァァ！！！");

                    UpdateMainMessage("　　＜＜＜　ガシュッッ！　＞＞＞");

                    UpdateMainMessage("アイン：ッガハ！！");

                    UpdateMainMessage("　　【【【アインの死がより確実なものとなっていく】】】");

                    UpdateMainMessage("ヴェルゼ：この辺で十分でしょうか。");

                    UpdateMainMessage("ヴェルゼ：セレスティアル・ノヴァ。");

                    UpdateMainMessage("　　＜＜＜アインの傷が癒えてゆく・・・＞＞＞");

                    UpdateMainMessage("アイン：ッハァ・・ッハァ・・・");

                    UpdateMainMessage("ヴェルゼ：すみません、どうやら勝負ありのようですね。");

                    UpdateMainMessage("アイン：くそ・・・勝ったと思ったんだけどな・・・ッハァ・・・ッハァ・・・");
                }
                else
                {
                    UpdateMainMessage("アイン：ッグハ・・・ック・・・");

                    UpdateMainMessage("ヴェルゼ：すみません、どうやら勝負ありのようですね。");

                    UpdateMainMessage("アイン：くそ・・・負けちまったか・・・");
                }

                UpdateMainMessage("アイン：て言うか、早すぎる・・・全く追いつける気がしねえ・・・");

                UpdateMainMessage("ヴェルゼ：いえ、すみませんが、それには理由があります。");

                UpdateMainMessage("ヴェルゼ：ボクのアクセサリをお見せしましょう。コレです。");

                UpdateMainMessage("アイン：この光り方・・・ひょっとして！！");

                UpdateMainMessage("ヴェルゼ：【天空の翼】　神々の遺産の一つです。");

                UpdateMainMessage("アイン：ボケ師匠の極悪グローブと同じ類のヤツだよな！？");

                UpdateMainMessage("ヴェルゼ：残念ながらそういう事になります。");

                UpdateMainMessage("ヴェルゼ：この翼を付けている場合、【技】のパラメタが異常なまでに増幅されます。");

                UpdateMainMessage("ヴェルゼ：そういう事ですから、公平さは欠けている事になります。");

                UpdateMainMessage("アイン：・・・いや");

                UpdateMainMessage("アイン：そういうのは関係ねえ、俺の負けだ。");

                UpdateMainMessage("ヴェルゼ：良い心構えです。");

                UpdateMainMessage("アイン：いやいや、次までには絶対少しぐらいは追いつけるようになってやるぜ。");

                UpdateMainMessage("ヴェルゼ：ハハハ、アイン君はきっと強くなりますよ。");

                UpdateMainMessage("ヴェルゼ：さて・・・それはさておき。");

                UpdateMainMessage("ヴェルゼ：少し以前から起きているのではないでしょうか？");

                UpdateMainMessage("アイン：？　なんの話だ？");

                UpdateMainMessage("ヴェルゼ：ラナさんは、すでに起きていますよね？");

                UpdateMainMessage("ラナ：ウソ！？　なんでバレちゃってるのよ！");

                UpdateMainMessage("アイン：おぉ、ラナ！　無事だったんだな！");

                UpdateMainMessage("アイン：いやあよかった良かった！　ッハッハッハ！！");

                UpdateMainMessage("アイン：一体いつ頃から起きていたんだ？");

                UpdateMainMessage("ラナ：バカアインが空中散歩してる所からよ♪");

                UpdateMainMessage("アイン：ハイハイ、俺が倒された瞬間は見られたって事ね・・・");

                UpdateMainMessage("ラナ：でもアインも結構何ていうか・・・");

                UpdateMainMessage("アイン：ん？なんだよ。");

                UpdateMainMessage("ラナ：ん～、何でも無い。気のせいね♪");

                UpdateMainMessage("アイン：なんだ、またソレかよ？　ハッキリ教えてくれよ。");

                UpdateMainMessage("ラナ：大した事じゃないわよ、何でも無いわ♪");

                UpdateMainMessage("ヴェルゼ：ラナさんは、アイン君と同じ転送装置で来たんですよね？");

                UpdateMainMessage("ラナ：え！？え、えぇ・・・でもどうしてですか？");

                UpdateMainMessage("ヴェルゼ：アイン君とは少し離れた所で倒れていたので、少々気になっただけです。");

                UpdateMainMessage("ヴェルゼ：アイン君と行き先が同じのため、同時に入ったのではありませんか？");

                UpdateMainMessage("ラナ：えぇ・・・アインとは同じタイミングで転送装置に入ったのは確かです。");

                UpdateMainMessage("アイン：同時に入るのは、あまり良くないのか？");

                UpdateMainMessage("ヴェルゼ：一般的には良いとはされていませんね。");

                UpdateMainMessage("ヴェルゼ：転送装置は１人専用のため、２人同時の場合、到達地点予測は不可能です。");

                UpdateMainMessage("アイン：ヤベ・・・次から１人ずつ入るか・・・");

                UpdateMainMessage("ラナ：ゴメンなさい、私もちょっと浮かれてたかも知れないわ。");

                UpdateMainMessage("ヴェルゼ：しかし、今回のようなケース自体、非常に稀だと思います。");

                UpdateMainMessage("ヴェルゼ：誰かが妙なトラップでも仕込まない限り、滅多なアクシデントはありません。");

                UpdateMainMessage("ヴェルゼ：ほんの少しタイミングをズラす程度で大丈夫だと思いますよ。");

                UpdateMainMessage("ラナ：分かりました、気をつけます。");

                UpdateMainMessage("アイン：さてと・・・この草原区域からファージル宮殿って・・・");

                UpdateMainMessage("ヴェルゼ：少し距離がありますね。");

                UpdateMainMessage("ヴェルゼ：転送装置は私が少しメンテナンスをしておきますので");

                UpdateMainMessage("ヴェルゼ：今回は一旦戻ってはいかがでしょうか？");

                UpdateMainMessage("アイン：ん・・・まあ、そうか。　どうする、ラナ？");

                UpdateMainMessage("ラナ：うん、一旦戻りましょ。");

                UpdateMainMessage("アイン：そうか、じゃあ戻るとするか。");

                UpdateMainMessage("アイン：ヴェルゼさん、いろいろありがとうな。");

                UpdateMainMessage("ヴェルゼ：いえ、こちらこそ。");

                UpdateMainMessage("ヴェルゼ：それと気兼ねなく話すためにも、ヴェルゼと呼び捨てで構いませんよ。");

                UpdateMainMessage("アイン：何かFiveSeeker様相手に呼び捨ても気が引けるが・・・");

                UpdateMainMessage("ヴェルゼ：始めに説明したはずです。オル・ランディスに報告しましょうか？");

                UpdateMainMessage("アイン：いやあ、今後ともよろしくな！ヴェルゼ！");

                UpdateMainMessage("ヴェルゼ：ハハハ、アイン君は本当に面白いですね。");

                UpdateMainMessage("アイン：じゃあ、本当に危ない所をありがとな、またどこかで会おう。");

                UpdateMainMessage("ラナ：私から先に行ってるわね。");

                UpdateMainMessage("アイン：ああ。");

                UpdateMainMessage("　　＜＜＜　ラナは転送装置で元の場所へと戻っていった　＞＞＞");

                UpdateMainMessage("アイン：っそれじゃ！");

                UpdateMainMessage("ヴェルゼ：はい。");

                ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                ButtonVisibleControl(true);

                UpdateMainMessage("　　＜＜＜　アインは転送装置で元の場所へと帰ってきた　＞＞＞");

                UpdateMainMessage("アイン：っふぅ・・・");

                UpdateMainMessage("　　　【・・・ゥゥゥブゥヴウウゥゥゥン・・・】");

                UpdateMainMessage("アイン：ん？");

                UpdateMainMessage("　　＜＜＜　転送装置が再び光り出した　＞＞＞");

                UpdateMainMessage("アイン：・・・マジかよ！？");

                UpdateMainMessage("ヴェルゼ：たびたび驚かせてすみません。");

                UpdateMainMessage("ヴェルゼ：そういえば、言い忘れていた事があります。");

                UpdateMainMessage("アイン：お、おお。えっと、なんでしょう？");

                UpdateMainMessage("ヴェルゼ：いや、これはお願いなのですが。");

                UpdateMainMessage("ヴェルゼ：この私を、アイン君のパーティに加えてもらえませんか？");

                UpdateMainMessage("アイン：なっ！！　マジで！？");

                UpdateMainMessage("ヴェルゼ：はい。");

                UpdateMainMessage("アイン：・・・・・・");

                UpdateMainMessage("ラナ：ちょっと、どうするのよ。アイン？");

                UpdateMainMessage("アイン：・・・・・・");

                UpdateMainMessage("ラナ：何、ボケっとしてんのよ。答えなさいよ？");

                UpdateMainMessage("アイン：や・・・");

                UpdateMainMessage("アイン：やった！！！");

                UpdateMainMessage("アイン：こちらこそ、お願いします！！！！！");

                UpdateMainMessage("ヴェルゼ：どうやらOKのようですね、ありがとうございます。");

                CallSomeMessageWithAnimation("ヴェルゼ・アーティがパーティに加わりました。");

                UpdateMainMessage("アイン：いやあ、でも本当に良いんですか？");

                UpdateMainMessage("ヴェルゼ：どういう意味でしょう？");

                UpdateMainMessage("アイン：俺達とダンジョンに向かってもなんの得にもならないですよ？");

                UpdateMainMessage("ヴェルゼ：・・・");

                UpdateMainMessage("ヴェルゼ：ハハハハ、これはまた面白い事を言いますね。");

                UpdateMainMessage("アイン：アハハ・・・（面白いのか？）");

                UpdateMainMessage("ヴェルゼ：得するために、ダンジョンへ向かっているワケではありませんよ。");

                UpdateMainMessage("ラナ：バカアインみたいに皆そーいう事考えてると思ったら大間違いよ。");

                UpdateMainMessage("アイン：ハイハイ・・・どうせ俺は生活資金源が目的ですよ・・・");

                UpdateMainMessage("ヴェルゼ：ハハハ、そういう意味ではボクも似たようなものですよ。");

                UpdateMainMessage("アイン：ホラ見ろ。ヴェルゼだって同じようなもんだって言ってるじゃねえか。");

                UpdateMainMessage("ラナ：ホンット筋金入りバカね、アンタに合わせてるだけでしょうが。");

                UpdateMainMessage("ヴェルゼ：目的は人によって違います。機会があればお話しますよ。");

                UpdateMainMessage("ヴェルゼ：さて、それではダンジョンへ向いましょう。");

                UpdateMainMessage("アイン：おっしゃ、よろしく頼むぜ！");

                we.AvailableThirdCharacter = true;
                tc = null;
                tc = new MainCharacter();
                tc.FullName = "ヴェルゼ・アーティ";
                tc.Name = "ヴェルゼ";
                tc.Strength = Database.VERZE_ARTIE_SECOND_STRENGTH;
                tc.Agility = Database.VERZE_ARTIE_SECOND_AGILITY;
                tc.Intelligence = Database.VERZE_ARTIE_SECOND_INTELLIGENCE;
                tc.Stamina = Database.VERZE_ARTIE_SECOND_STAMINA;
                tc.Mind = Database.VERZE_ARTIE_SECOND_MIND;
                tc.Level = 0;
                tc.Exp = 0;
                for (int ii = 0; ii < 35; ii++)
                {
                    tc.BaseLife += tc.LevelUpLifeTruth;
                    tc.BaseMana += tc.LevelUpManaTruth;
                    tc.Level++;
                }
                tc.CurrentLife = tc.MaxLife;
                tc.BaseSkillPoint = 100;
                tc.CurrentSkillPoint = 100;
                tc.CurrentMana = tc.MaxMana;
                tc.MainWeapon = new ItemBackPack(Database.RARE_WHITE_SILVER_SWORD_REPLICA);
                tc.MainArmor = new ItemBackPack(Database.RARE_BLACK_AERIAL_ARMOR_REPLICA);
                tc.Accessory = new ItemBackPack(Database.RARE_HEAVENLY_SKY_WING_REPLICA);
                tc.BattleActionCommand1 = Database.NEUTRAL_SMASH;
                tc.BattleActionCommand2 = Database.INNER_INSPIRATION;
                tc.BattleActionCommand3 = Database.MIRROR_IMAGE;
                tc.BattleActionCommand4 = Database.DEFLECTION;
                tc.BattleActionCommand5 = Database.STANCE_OF_FLOW;
                tc.BattleActionCommand6 = Database.GALE_WIND;
                tc.BattleActionCommand7 = Database.STRAIGHT_SMASH;
                tc.BattleActionCommand8 = Database.SURPRISE_ATTACK;
                tc.BattleActionCommand9 = Database.NEGATE;
                tc.AvailableMana = true;
                tc.AvailableSkill = true;

                tc.FireBall = true;
                tc.StraightSmash = true;
                tc.CounterAttack = true;
                tc.FreshHeal = true;
                tc.StanceOfFlow = true;
                tc.DispelMagic = true;
                tc.WordOfPower = true;
                tc.EnigmaSence = true;
                tc.BlackContract = true;
                tc.Cleansing = true;
                tc.GaleWind = true;
                tc.Deflection = true;
                tc.Negate = true;
                tc.InnerInspiration = true;
                tc.FrozenLance = true;
                tc.Tranquility = true;
                tc.WordOfFortune = true;
                tc.SkyShield = true;
                tc.NeutralSmash = true;
                tc.Glory = true;
                tc.BlackFire = true;
                tc.SurpriseAttack = true;
                tc.MirrorImage = true;
                tc.WordOfMalice = true;
                tc.StanceOfSuddenness = true;
                tc.CrushingBlow = true;
                tc.Immolate = true;
                tc.AetherDrive = true;
                tc.TrustSilence = true;
                tc.WordOfAttitude = true;
                tc.OneImmunity = true;
                tc.AntiStun = true;
                tc.FutureVision = true;

                we.AvailableFazilCastle = true;
            }
            #endregion
            #region "カールハンツ爵の訓練場"
            else if (!we.alreadyCommunicateCahlhanz)
            {
                we.alreadyCommunicateCahlhanz = true;

                GoToKahlhanz();

                #region "エンレイジ・ブラスト"
                if ((mc.Level >= 22) && (!mc.EnrageBlast))
                {
                    UpdateMainMessage("アイン：っとと・・・着いたみたいだな");

                    UpdateMainMessage("カール：来たか、それでは講義を始めるとしよう。");

                    UpdateMainMessage("アイン：はい、お願いします！");

                    UpdateMainMessage("　　　『アインは集中して講義の内容を聞いた！』");

                    UpdateMainMessage("アイン：先生、質問。");

                    UpdateMainMessage("カール：言ってみるが良い。");

                    UpdateMainMessage("　　【【【　アインは背筋に異常な威圧感を感じている。　】】】");

                    UpdateMainMessage("アイン：（何だこの異常な威圧感は・・・ぐぬぬ・・・）");

                    UpdateMainMessage("カール：どうした。");

                    UpdateMainMessage("アイン：ああっとですね。火と理を融合させる所はなんとなく分かるんですが");

                    UpdateMainMessage("カール：何となくという理解そのものが危うい。");

                    UpdateMainMessage("アイン：あっと、ハイ・・・");

                    UpdateMainMessage("カール：理とは、この世の【自然】、【原理】、【原則】そのものを指す。");

                    UpdateMainMessage("カール：そして火とは、【浄化】、【エネルギー】、【進行】そのものを指す。");

                    UpdateMainMessage("カール：原則を発展させるイメージを伴わせるには、その普遍的な概念を構築するが良い。");

                    UpdateMainMessage("アイン：あ、ハイ。そうなんですけど・・・");

                    UpdateMainMessage("カール：言ってみるが良い。");

                    UpdateMainMessage("　　【【【　アインは背筋に異常な威圧感を感じている。　】】】");

                    UpdateMainMessage("アイン：（この威圧感を何とかしてくれ・・・ッグググ・・・）");

                    UpdateMainMessage("カール：先と同じ展開。どうした。");

                    UpdateMainMessage("アイン：『火』って何かこう・・・まとまりが無くて、危なっかしいイメージじゃないですか。");

                    UpdateMainMessage("アイン：でも『理』ってのは何だろ・・・全てが一貫してビシーっと筋が通ってると言うか・・・");

                    UpdateMainMessage("アイン：それを融合させるって所が何となく・・・");

                    UpdateMainMessage("カール：『火』の動作は決してランダムではない。");

                    UpdateMainMessage("カール：『火』の移りゆく現象、それは予め定められた軌跡を辿る現象である。");

                    UpdateMainMessage("カール：『理』とて同義。全ては決定づけられた事象を指す場合もあるが、");

                    UpdateMainMessage("カール：始まりの条件付けで結果は千差万別。それは『火』の動作そのものでもあるのだ。");

                    UpdateMainMessage("カール：その始まりとなるのは己自身、つまり貴君のイメージが始まりだと考えれば良い。");

                    UpdateMainMessage("アイン：・・・　す・・・");

                    UpdateMainMessage("アイン：すげぇ！！！");

                    UpdateMainMessage("アイン：カール先生、やっぱアンタ天才ですよ！！！");

                    UpdateMainMessage("アイン：俺は講義でこんなにもイメージが行き届くのは今まで味わったことが無いもんで・・・");

                    UpdateMainMessage("アイン：いや、いやいやいや、ホンットどうもです！");

                    UpdateMainMessage("カール：転送装置の時間だ、そろそろ帰るが良い。");

                    UpdateMainMessage("カール：知識は全ての源、忘れるな。");

                    UpdateMainMessage("アイン：ありがとうございました！");

                    UpdateMainMessage("カール：（・・・　・・・）");

                    UpdateMainMessage("カール：（一度でここまで習得してくるとは。ランディスもさぞ楽しい事だろうな）");

                    mc.EnrageBlast = true;
                    ShowActiveSkillSpell(mc, Database.ENRAGE_BLAST);
                    UpdateMainMessage("", true);
                }
                #endregion
                #region "ホーリー・ブレイカー"
                else if ((mc.Level >= 23) && (!mc.HolyBreaker))
                {
                    UpdateMainMessage("アイン：っとと・・・着いたみたいだな");

                    UpdateMainMessage("カール：来たか、それでは講義を始めるとしよう。");

                    UpdateMainMessage("アイン：はい、お願いします！");

                    UpdateMainMessage("　　　『アインは集中して講義の内容を聞いた！』");

                    UpdateMainMessage("アイン：詠唱スタイルなんですけど、どうもしっくり来ないんですよ。");

                    UpdateMainMessage("カール：違和感を感じるのは、『聖』『理』の相性が良く、厳格さのイメージが強すぎるが故。");

                    UpdateMainMessage("カール：貴君は本来、その気質を有しているはず、しかし普段は出していない。違うかな？");

                    UpdateMainMessage("アイン：・・・いや");

                    UpdateMainMessage("アイン：いや、いやいや。そんなんじゃないです、結構俺って適当派なんで");

                    UpdateMainMessage("カール：ランディスの言ってた貴君の病気。無意識にまで入り込んでるようだな。");

                    UpdateMainMessage("アイン：いや、あのボケ師匠にも言われた事はあるけど。");

                    UpdateMainMessage("アイン：いやいや、そもそも今回のホーリー・ブレイカーは攻撃を攻撃として跳ね返すって事ですよね？");

                    UpdateMainMessage("アイン：それだけの事だと思うし、俺自身がしっくり来てないだけです。");

                    UpdateMainMessage("　　　『擬眼がギョロリと動きはじめた！』");

                    UpdateMainMessage("カール：我の前でそのような態度、取らぬ方が得策と心得よ。");

                    UpdateMainMessage("　　【【【　アインは背筋に更に尋常ではない威圧感を感じた。　】】】");

                    UpdateMainMessage("アイン：しっくり来ないってんじゃなくて・・・");

                    UpdateMainMessage("アイン：これは俺自身の問題。そう考えます。");

                    UpdateMainMessage("カール：己自身が一番把握しているのだろう。己自身に対して向きあうと良い。");

                    UpdateMainMessage("カール：ホーリー・ブレイカーは攻撃ダメージの分をそのまま相手に跳ね返す。");

                    UpdateMainMessage("カール：その分、自分自身もライフを消費する事には代わりはない。");

                    UpdateMainMessage("カール：もし、貴君が真の連携を求めているのであれば、今のスタイルは一旦捨てる事まで考え抜く事だ。");

                    UpdateMainMessage("アイン：そ・・・それは・・・");

                    UpdateMainMessage("カール：知識は全ての源、忘れるな。");

                    UpdateMainMessage("アイン：あ、はい。ありがとうございました！");

                    mc.HolyBreaker = true;
                    ShowActiveSkillSpell(mc, Database.HOLY_BREAKER);
                    UpdateMainMessage("", true);
                }
                #endregion
                #region "サークル・スラッシュ"
                else if ((mc.Level >= 27) && (!mc.CircleSlash))
                {
                    UpdateMainMessage("アイン：っとと・・・着いたみたいだな");

                    UpdateMainMessage("アイン：あの・・・最初いつも姿が見えないんですけど・・・");

                    UpdateMainMessage("　　【【【　アインは背筋に異常な威圧感を感じている。　】】】");

                    UpdateMainMessage("アイン：（何だこの威圧感は・・・ッグ・・・）");

                    UpdateMainMessage("カール：どうした。");

                    UpdateMainMessage("アイン：うお！！　おぉぉっと！！");

                    UpdateMainMessage("アイン：ほんっと驚かせないでくださいよ・・・");

                    UpdateMainMessage("カール：敵の気配ぐらい、事前に察知せよ。");

                    UpdateMainMessage("アイン：でも、今みたいな雰囲気だと、どうしようもないですよ。");

                    UpdateMainMessage("カール：貴君は『動』と『剛』を兼ね備えておる。");

                    UpdateMainMessage("カール：周囲一体をひとまず切り払ってみたらどうだ？");

                    UpdateMainMessage("アイン：急にそんな事、できるわけが・・・");

                    UpdateMainMessage("アイン：・・・　・・・");

                    UpdateMainMessage("カール：その辺の素質、ランディスが買うだけの事はあるようだな");

                    UpdateMainMessage("　　　『アインはいつものストレートスマッシュの構えを始めた』");

                    UpdateMainMessage("アイン：（こっから・・・体の軸をブレさせず、意図的に力をこめれば・・・");

                    UpdateMainMessage("アイン：おらよっと！");

                    UpdateMainMessage("　　　ヴオオォォン！！　　　");

                    UpdateMainMessage("カール：『サークル・スラッシュ』とでも名づけておくがいい。");

                    UpdateMainMessage("アイン：（何だこれ・・・次々と・・・バカな・・・）");

                    UpdateMainMessage("アイン：カール師匠");

                    UpdateMainMessage("カール：我は師匠ではない。　なんだね。");

                    UpdateMainMessage("アイン：今の俺のサークル・スラッシュって、まだまだですよね？");

                    UpdateMainMessage("カール：当然だ。");

                    UpdateMainMessage("アイン：スンマセン、良かったらもっと講義をお願いします。");

                    UpdateMainMessage("カール：ずいぶんと殊勝な心がけ、よほど気に入ったようだな。");

                    UpdateMainMessage("アイン：知識の集約だけでここまで来るとは思ってませんでした。");

                    UpdateMainMessage("カール：知識の無い側からすれば、そう感じられる。当然の反応だ。");

                    UpdateMainMessage("アイン：これって知らない人は一生気付けないんじゃないですか？");

                    UpdateMainMessage("カール：個人の態度次第だ。");

                    UpdateMainMessage("アイン：・・・そうか・・・");

                    UpdateMainMessage("カール：・・・転送装置の時間切れが近い、今日は戻ると良いだろう。");

                    UpdateMainMessage("アイン：あ！どうもありがとうございました！");

                    mc.CircleSlash = true;
                    ShowActiveSkillSpell(mc, Database.CIRCLE_SLASH);
                    UpdateMainMessage("", true);
                }
                #endregion
                #region "バイオレント・スラッシュ"
                else if ((mc.Level >= 28) && (!mc.ViolentSlash))
                {
                    UpdateMainMessage("アイン：っとと・・・着いたみたいだな");

                    UpdateMainMessage("カール：来たか、それでは講義を始めるとしよう。");

                    UpdateMainMessage("アイン：はい、お願いします！");

                    UpdateMainMessage("　　　『アインは集中して講義の内容を聞いた！』");

                    UpdateMainMessage("カール：聞きたい事はあるか。");

                    UpdateMainMessage("アイン：あ、じゃあ１個だけ。ええとですね・・・");

                    UpdateMainMessage("アイン：そもそもどう考えれば良いんですかね？");

                    UpdateMainMessage("カール：その言い方からして、カウンターの概念を聞きたいのだろう。");

                    UpdateMainMessage("アイン：あっとそうです、スイマセン・・・そうそう、カウンターの概念です。");

                    UpdateMainMessage("カール：このバイオレント・スラッシュはカウンターされないスキルである。");

                    UpdateMainMessage("カール：ただ貴君は既に察したようだが、それは相手にヒントも与える。");

                    UpdateMainMessage("アイン：そうなんですよ。カウンターされないからそうする場合はケースが特定される。");

                    UpdateMainMessage("アイン：致死ダメージに至るか、またはどうしても物理ダメージに付随する何かを通したい時。でも・・・");

                    UpdateMainMessage("カール：その通り、それこそカウンターの格好の的。");

                    UpdateMainMessage("カール：カウンターはされないが、ヒール魔法のスタックを積む事ぐらい容易であろう。");

                    UpdateMainMessage("カール：また致死ダメージに至らないのであれば、相手は別の大事な手をそこで発動するだろう。");

                    UpdateMainMessage("カール：で、あるとすればカウンターされない事自体に大した効果は望めない。");

                    UpdateMainMessage("カール：カウンターとはどう考えれば良いか、と言うことになる。これで良いかね。");

                    UpdateMainMessage("アイン：そ、そうそうそう！！！");

                    UpdateMainMessage("アイン：マジそうなんですよ！　ソコを教えてください！！");

                    UpdateMainMessage("カール：貴君なりの解釈は持っているかね、あれば言ってみるが良い。");

                    UpdateMainMessage("アイン：そう・・・ですねえ・・・");

                    UpdateMainMessage("アイン：どうせこういった場合、相手もインスタント値を蓄えている。だとすれば");

                    UpdateMainMessage("アイン：ヒントらしいヒントを与えない行動。そのタイミングで放てばいい・・・かな？");

                    UpdateMainMessage("カール：筋は良い。");

                    UpdateMainMessage("アイン：やった！　ッハッハッハ！");

                    UpdateMainMessage("カール：だが、及第点ではない。");

                    UpdateMainMessage("アイン：（ッガク・・・）");

                    UpdateMainMessage("カール：圧力をかける行為、それがこのバイオレント・スラッシュの一番の使い道。");

                    UpdateMainMessage("アイン：圧力？");

                    UpdateMainMessage("カール：致死に至らない状態から、このスキルを食らう側だとする。だとすれば");

                    UpdateMainMessage("アイン：まあ、食らうしか無いって思うぐらいか・・・ダメージは食らわざるをえないとして・・・");

                    UpdateMainMessage("アイン：そおおぉぉかあぁぁ、そうか！！！");

                    UpdateMainMessage("カール：言ってみるが良い。");

                    UpdateMainMessage("アイン：威力倍増！クリティカル！ゲイル・ウィンド！なんとでもやり方はあるじゃねぇか！！");

                    UpdateMainMessage("アイン：つまりライフ満タンでも、それが激減すりゃそれ自体が脅威そのものって事だ！！");

                    UpdateMainMessage("アイン：致死に至るケースじゃなくて、むしろ始めっから窮地に追い込む事をすり込ませる。");

                    UpdateMainMessage("アイン：要は、相手に圧力を事前に仕込める最大の手法ってワケだ！！");

                    UpdateMainMessage("カール：及第点だ。");

                    UpdateMainMessage("カール：本来、講義でこういった内容までは踏み込まないが、貴君のみ特別である。");

                    UpdateMainMessage("アイン：あ、そうなんすね・・・ホントありがとうございます。");

                    UpdateMainMessage("カール：まだまだ奥は深い。自分なりのスキル使用構築のスタイルを築くと良いだろう。");

                    UpdateMainMessage("カール：そろそろ転送装置の時間だ、帰るが良い。");

                    UpdateMainMessage("アイン：ありがとうございました！");

                    mc.ViolentSlash = true;
                    ShowActiveSkillSpell(mc, Database.VIOLENT_SLASH);
                    UpdateMainMessage("", true);
                }
                #endregion
                #region "ランブル・シャウト"
                else if ((mc.Level >= 29) && (!mc.RumbleShout))
                {
                    UpdateMainMessage("アイン：っとと・・・着いたみたいだな");

                    UpdateMainMessage("カール：来たか、それでは講義を始めるとしよう。");

                    UpdateMainMessage("アイン：はい、お願いします！");

                    UpdateMainMessage("　　　『アインは集中して講義の内容を聞いた！』");

                    UpdateMainMessage("カール：敵の注意を引くのには、個が全体を意識して初めて可能である。");

                    UpdateMainMessage("カール：本スキル使用時、必ず自分にダメージもしくは負のＢＵＦＦ効果がかかるため、失敗は許されぬ。");

                    UpdateMainMessage("アイン：確かに、こういうスキルは使い所を間違えたくは無いな。");

                    UpdateMainMessage("アイン：・・・");

                    UpdateMainMessage("カール：どうした、言ってみるが良い。");

                    UpdateMainMessage("アイン：敵が敵パーティにライフ回復させようとしてた場合はどうなる？");

                    UpdateMainMessage("カール：対象外だ。");

                    UpdateMainMessage("アイン：自分対象の時、これを使うとどうなる？");

                    UpdateMainMessage("カール：対象は変わらぬ。");

                    UpdateMainMessage("アイン：対象を取らない全体系は自分だけを対象に変更することは？");

                    UpdateMainMessage("カール：不可能だ。");

                    UpdateMainMessage("アイン：対象をこちらに向けた直後、敵が対象を選びなおす事は？");

                    UpdateMainMessage("カール：よほどの特例が無い限り、不可能だ。そのためのスキルでもある。");

                    UpdateMainMessage("アイン：サンキュー。すげぇ助かるぜ。");

                    UpdateMainMessage("カール：その抜け目の無さ。若い頃の我と類似する点がある。");

                    UpdateMainMessage("アイン：マ、マジっすか！？");

                    UpdateMainMessage("カール：しかし、発想の原点がまだまだ稚拙。");

                    UpdateMainMessage("アイン：とほほ・・・");

                    UpdateMainMessage("カール：知識は全ての源、忘れるな。");

                    UpdateMainMessage("アイン：ありがとうございました！");

                    mc.RumbleShout = true;
                    ShowActiveSkillSpell(mc, Database.RUMBLE_SHOUT);
                    UpdateMainMessage("", true);
                }
                #endregion
                #region "ワード・オブ・アティチュード"
                else if ((mc.Level >= 30) && (!mc.WordOfAttitude))
                {
                    UpdateMainMessage("アイン：っとと・・・着いたみたいだな");

                    UpdateMainMessage("カール：来たか、それでは講義を始めるとしよう。");

                    UpdateMainMessage("アイン：はい、お願いします！");

                    UpdateMainMessage("カール：ただし、今回の講義は少々特殊なものとなる。");

                    UpdateMainMessage("アイン：どういう意味ですか？");

                    UpdateMainMessage("カール：貴君に逆属性に関する原論を今から徹底的に叩きこむ。");

                    UpdateMainMessage("アイン：逆属性？");

                    UpdateMainMessage("カール：では、その全てを今から教える。心して記憶せよ。");

                    UpdateMainMessage("　　　『アインは集中して講義の内容を聞いた！』");

                    UpdateMainMessage("カール：本来、インスタント値の回復は生物の自然回復をベースとしている。");

                    UpdateMainMessage("カール：『ワード・オブ・アティチュード』この『水』と『理』による複合魔法はそれを可能とするもの。");

                    UpdateMainMessage("カール：貴君の場合、『火』ベースであるため、『水』『理』は論理矛盾をきたす。");

                    UpdateMainMessage("カール：しかし、『火』の逆となる『水』をもイメージの源泉とすれば可能となる。後に実行してみるが良かろう。");

                    UpdateMainMessage("アイン：論理矛盾・・・か・・・");

                    UpdateMainMessage("カール：どうした。");

                    UpdateMainMessage("アイン：論理ってのはイマイチ掴めない、そんな感じがしてさ。");

                    UpdateMainMessage("アイン：どこまでが論理で、どこからが論理じゃないのか・・・");

                    UpdateMainMessage("アイン：カール先生の言ってる事は受け入れられない内容じゃない。");

                    UpdateMainMessage("アイン：むしろ、話自体は筋が根幹から通っていて、聞いててスっと入ってくるし、スゲェ分かる。");

                    UpdateMainMessage("アイン：だからこそ、論理矛盾って言われると、聞きたくなるんですよ。良いですか？");

                    UpdateMainMessage("カール：言ってみるが良い。");

                    UpdateMainMessage("アイン：矛盾したらソコで終わりじゃないのか？");

                    UpdateMainMessage("カール：間違いなく終わりだ。");

                    UpdateMainMessage("アイン：だったら逆属性そのものに無理があるんじゃ？");

                    UpdateMainMessage("カール：無理が生じる、至極当然。");

                    UpdateMainMessage("アイン：ッゲ、マジかよ・・・じゃあ無理でしょう？");

                    UpdateMainMessage("カール：人間は論理的矛盾に陥ったと実感した時、心的ダメージは非常に大きい。");

                    UpdateMainMessage("カール：誰にでも出来るモノではない。そういう事だ。");

                    UpdateMainMessage("カール：貴君は我の今までの講義を聞き、そして今もここにいる。");

                    UpdateMainMessage("アイン：え、まあ・・・");

                    UpdateMainMessage("カール：であれば、貴君に資質はある。それを踏まえるがよい。");

                    UpdateMainMessage("　　　『アインはいつもの表情を崩し、今までにない真剣な表情でこう告げた』");

                    UpdateMainMessage("アイン：正直、出来ないヤツも居るって事ですか？");

                    UpdateMainMessage("カール：そういう事だ。不服か？");

                    UpdateMainMessage("アイン：不服とかじゃなくて、出来ないヤツはどうすればいいんですか？");

                    UpdateMainMessage("カール：出来ない者は、そもそも我の前に現れる事なく、自然と流れ行く。");

                    UpdateMainMessage("カール：・・・ふむ、なるほど。その動揺、自分以外の誰かを察しての事と見える。");

                    UpdateMainMessage("アイン：ッグ・・・");

                    UpdateMainMessage("カール：貴君がココに初めて来た時もそうであったな。");

                    UpdateMainMessage("カール：ちょうど良い、我の前にその者をココに連れて来ると良い。");

                    UpdateMainMessage("アイン：っえ、良いんですか？");

                    UpdateMainMessage("カール：貴君のその異常なまでの心の気配り、それを直さねばなるまい。");

                    UpdateMainMessage("アイン：いや、気配りなんてしてないですよ。");

                    UpdateMainMessage("カール：よい、今日はここまでだ。　知識は全ての源、忘れるな。");

                    UpdateMainMessage("アイン：あ、はい。いろいろ突っ込んだ所までスンマセン、ありがとうございました！");

                    mc.WordOfAttitude = true;
                    ShowActiveSkillSpell(mc, Database.WORD_OF_ATTITUDE);
                    UpdateMainMessage("", true);
                }
                #endregion
                #region "スカイ・シールド"
                else if ((mc.Level >= 31) && (!mc.SkyShield))
                {
                    UpdateMainMessage("アイン：っとと・・・着いたみたいだな");

                    UpdateMainMessage("アイン：あら？・・・居ない・・・");

                    UpdateMainMessage("アイン：いやいや・・・また変な所から仕掛けてくる可能性も・・・");

                    UpdateMainMessage("カール：貴君にとっての第一逆属性『水』。では早速実践にうつるとしよう。");

                    UpdateMainMessage("アイン：うおおぉぉ！！　ビビびっくりするじゃないですか！！");

                    UpdateMainMessage("アイン：っと・・・実践？");

                    UpdateMainMessage("カール：素質は十分に感じられる。");

                    UpdateMainMessage("アイン：実践って実践ですよね！？　っしゃ、待ってました！　じゃあ早速！");

                    UpdateMainMessage("カール：少し距離を取るとしよう。");

                    UpdateMainMessage("　　　『ッバシュ！！』（カールは一瞬で向こう側へ姿を移動させた！");

                    UpdateMainMessage("アイン：（何だ今の・・・テレポートみたいな現象だったぞ・・・）");

                    UpdateMainMessage("カール：今から貴君にフレイム・ストライクを乱射するとしよう。");

                    UpdateMainMessage("カール：実践の中で、我の攻撃を受け止める魔法、新しく発見してみせよ。");

                    UpdateMainMessage("アイン：っはい？");

                    UpdateMainMessage("カール：行くぞ。");

                    UpdateMainMessage("　　　『ッボシュ！ッボボボシュ！！』");

                    UpdateMainMessage("アイン：ッゲ！？　っちょっちょちょッタンマ！！！");

                    UpdateMainMessage("　　　『ッドシュ！』（アインに一撃が入った！）");

                    UpdateMainMessage("アイン：ッグハァ！！　ッグ、くそ・・・シャレになってねぇダメージだ。");

                    UpdateMainMessage("カール：どうした。実践では誰も貴君のペース配分など待ってくれはせんぞ。");

                    UpdateMainMessage("　　　『ッボシュ！ッボシュ！ッボボボシュ！！』");

                    UpdateMainMessage("アイン：ックソ・・・回避しきれねぇ、早すぎる！！");

                    UpdateMainMessage("　　　『ッドシュ！』（アインにもう一撃が入った！）");

                    UpdateMainMessage("アイン：ッグ！！　ッグ・・・");

                    UpdateMainMessage("カール：ホラホラホラ！！！");

                    UpdateMainMessage("　　　『ッドシュ！』（アインにもう一撃が入った！）");

                    UpdateMainMessage("アイン：ッグ、ゲホ・・・ボケ師匠と同じノリじゃねぇか、クソ・・・");

                    UpdateMainMessage("アイン：こんな中で・・・イメージなんか出来るかっつうの。");

                    UpdateMainMessage("カール：言っておくが");

                    UpdateMainMessage("　　　『ッボシュ！』");

                    UpdateMainMessage("カール：貴君が死ぬまでこれは続く。");

                    UpdateMainMessage("　　　『ッボボボシュ！』");

                    UpdateMainMessage("　　　『ッドドドシュ！』（アインに追加で３撃が入った！）");

                    UpdateMainMessage("アイン：ッグ！！ッグアアァァァ！！！");

                    UpdateMainMessage("アイン：（駄目だ・・・避けようなんてのは無理がある・・・）");

                    UpdateMainMessage("アイン：ホーリー・ブレイカー！");

                    UpdateMainMessage("　　　『ッドシュ！』（アインにもう一撃が入った！）");

                    UpdateMainMessage("カール：残念だが、それでは魔法ダメージは防げぬ。");

                    UpdateMainMessage("　　　『ッドシュ！』（アインにもう一撃が入った！）");

                    UpdateMainMessage("カール：イメージを飛躍させよ。貴君なら出来るはず。");

                    UpdateMainMessage("　　　『ッドシュ！』（アインにもう一撃が入った！）");

                    UpdateMainMessage("アイン：ッグ！！・・・たく、ボケ師匠もそうだが、どうしてこう無茶苦茶な・・・");

                    UpdateMainMessage("アイン：（・・・イメージの飛躍ってどういう事だよ、逆属性の水で・・・？");

                    UpdateMainMessage("　　　『ッドドドドシュ！』（アインにもう４撃入り、致命的なダメージとなった！）");

                    UpdateMainMessage("アイン：ッグアアァァァ！！！");

                    UpdateMainMessage("　　　『アインは意識を失う寸前で、あるイメージが浮かばせた！』");

                    UpdateMainMessage("カール：ッム！");

                    UpdateMainMessage("カール：では、トドメだ。　ラヴァ・アニヒレーション！");

                    UpdateMainMessage("　　　『ッズゴゴオォォォン・・・』");

                    UpdateMainMessage("　　　『・・・　・・・　・・・』");

                    UpdateMainMessage("アイン：ッハァ・・・・ッハァ・・・");

                    UpdateMainMessage("カール：それでこそランディスの弟子と言えよう。");

                    UpdateMainMessage("アイン：魔法ダメージを０にする・・・スカイ・シールド・・・");

                    UpdateMainMessage("アイン：ってか・・・もうダメ・・・");

                    UpdateMainMessage("カール：貴君はこれで、火の逆属性となる『水』との複合をまた一つ習得した事となる。");

                    UpdateMainMessage("カール：また、本魔法は３回まで蓄積可能な魔法である。後で知識習得の時間を与えよう。");

                    UpdateMainMessage("アイン：っちょ・・・倒れさせてください・・・");

                    UpdateMainMessage("カール：知識は全ての源、忘れるな。");

                    UpdateMainMessage("アイン：（・・・ッバタ）");

                    mc.SkyShield = true;
                    ShowActiveSkillSpell(mc, Database.SKY_SHIELD);
                    UpdateMainMessage("", true);
                }
                #endregion
                #region "フローズン・オーラ"
                else if ((mc.Level >= 32) && (!mc.FrozenAura))
                {
                    UpdateMainMessage("アイン：っとと・・・着いたみたいだな");

                    UpdateMainMessage("カール：来たか。");

                    UpdateMainMessage("アイン：あの、ちょっとタンマ！！");

                    UpdateMainMessage("カール：どうした。言ってみるが良い。");

                    UpdateMainMessage("アイン：今日はちょっと実践は良いんで講義でお願いします！");

                    UpdateMainMessage("カール：よほど前回のが堪えたと見える。何ならいつでも実践相手になろう。");

                    UpdateMainMessage("アイン：いやいやいや、ちょっホント勘弁・・・！");

                    UpdateMainMessage("カール：ッフハハハ、楽しみにしているぞ。");

                    UpdateMainMessage("アイン：ハハハ・・・（やっぱこの人敵なんじゃ・・・）");

                    UpdateMainMessage("　　　『アインは集中して講義の内容を聞いた！』");

                    UpdateMainMessage("アイン：・・・つまりこれって、『火』と『水』って事ですよね？");

                    UpdateMainMessage("カール：その通りだ。");

                    UpdateMainMessage("カール：完全なる逆属性同士の複合魔法となるため、詠唱形態は極めて特殊。");

                    UpdateMainMessage("カール：加えて、イメージの源泉も始めから相反するモノをイメージする必要がある。");

                    UpdateMainMessage("アイン：本当にこんなのが可能なのかよ・・・");

                    UpdateMainMessage("カール：魔法効果自体は、剣に氷を付与するのみ。");

                    UpdateMainMessage("カール：逆属性の基礎を習得した貴君なら造作も無きこと。");

                    UpdateMainMessage("アイン：そりゃ、そうかも知れませんけど・・・");

                    UpdateMainMessage("アイン：フレイム・オーラで剣に火属性を付与しておくじゃないですか。");

                    UpdateMainMessage("アイン：で、後付けでこのフローズン・オーラも付与可能だって言ってるんですよね？");

                    UpdateMainMessage("カール：可能かどうかは貴君次第。");

                    UpdateMainMessage("アイン：とほほ・・・ボケ師匠とノリが一緒だよなこういうトコ・・・");

                    UpdateMainMessage("アイン：あれ、まてよ！？");

                    UpdateMainMessage("カール：どうした。");

                    UpdateMainMessage("アイン：ボケ師匠もこういうの出来るって事ですよね？？");

                    UpdateMainMessage("カール：当然。");

                    UpdateMainMessage("アイン：・・・待てよ待てよ・・・");

                    UpdateMainMessage("カール：ッフハハハハ、ランディスに対する戦術構築か。");

                    UpdateMainMessage("アイン：そりゃ、そうですよ！　あのボケ師匠は何か反則っぽい事してる気がしてたんだよ。");

                    UpdateMainMessage("アイン：っしゃ、フローズン・オーラ絶対に使いこなしてやるぜ。");

                    UpdateMainMessage("アイン：で、フレイム・オーラも付けて、今度こそボコボコにしてやる。");

                    UpdateMainMessage("カール：一つ、忠告しておこう。");

                    UpdateMainMessage("カール：完全なる逆属性の融合のため、他の複合と比べて、詠唱コストは極めて高い。");

                    UpdateMainMessage("カール：マナの枯渇には気をつける事だ。");

                    UpdateMainMessage("アイン：なるほど・・・了解！！");

                    UpdateMainMessage("カール：知識は全ての源、忘れるな。");

                    UpdateMainMessage("アイン：ありがとうございました！");

                    mc.FrozenAura = true;
                    ShowActiveSkillSpell(mc, Database.FROZEN_AURA);
                    UpdateMainMessage("", true);
                }
                #endregion
                #region "シャープ・グレア"
                else if ((mc.Level >= 33) && (!mc.SharpGlare))
                {
                    UpdateMainMessage("アイン：っとと・・・着いたみたいだな");

                    UpdateMainMessage("カール：来たか、それでは講義を始めるとしよう。");

                    UpdateMainMessage("カール：本日からは、体術の方に専念する。心せよ。");

                    UpdateMainMessage("アイン：はい、お願いします！");

                    UpdateMainMessage("　　　『アインは集中して講義の内容を聞いた！』");

                    UpdateMainMessage("カール：体術に関しては、ランディスから何度も訓練は受けているだろう。");

                    UpdateMainMessage("アイン：嫌というほど・・・");

                    UpdateMainMessage("カール：『静』と『心眼』による複合スキル『シャープ・グレア』");

                    UpdateMainMessage("カール：貴君の場合、『動』が基本性質であるため、『静』は逆の性質となる。");

                    UpdateMainMessage("カール：しかし、ランディスの実践訓練を積んでいる故、貴君にその懸念は不要。");

                    UpdateMainMessage("アイン：そんなものなのか・・・嬉んでいいんだろうか・・・");

                    UpdateMainMessage("カール：この『シャープ・グレア』は身体への打撃に際し、魔法詠唱を失敗させる効果を持つ。");

                    UpdateMainMessage("アイン：相手のインスタント行動時に魔法詠唱だった場合も、これでカウンターは可能？");

                    UpdateMainMessage("カール：そういう事だ。");

                    UpdateMainMessage("カール：加えて、沈黙効果が一定期間続く。魔法詠唱メインの者にとっては警戒すべきスキルとなろう。");

                    UpdateMainMessage("アイン：沈黙効果がある程度続くって事は・・・アンチ系のスキルって事になるな。");

                    UpdateMainMessage("カール：その通りだ。");

                    UpdateMainMessage("カール：ただし、ニゲイトに比べ消費コストは多い。スキルポイントのペース配分にも気を配るが良かろう。");

                    UpdateMainMessage("カール：知識は全ての源、忘れるな。");

                    UpdateMainMessage("アイン：ありがとうございました！");

                    mc.SharpGlare = true;
                    ShowActiveSkillSpell(mc, Database.SHARP_GLARE);
                }
                #endregion
                #region "リフレックス・スピリット"
                else if ((mc.Level >= 34) && (!mc.ReflexSpirit))
                {
                    UpdateMainMessage("アイン：っとと・・・着いたみたいだな");

                    UpdateMainMessage("カール：来たか、それでは講義を始めるとしよう。");

                    UpdateMainMessage("アイン：はい、お願いします！");

                    UpdateMainMessage("　　　『アインは集中して講義の内容を聞いた！』");

                    UpdateMainMessage("アイン：『静』の要素ってほんとこういうのが多いですよね。");

                    UpdateMainMessage("アイン：効果が見えないつうかなんつうか・・・");

                    UpdateMainMessage("カール：スタン、麻痺、凍結への耐性を持つのは戦術理論上では、極めて重要。");

                    UpdateMainMessage("アイン：まあそうですよね。こんなのが発動出来るとすれば・・・");

                    UpdateMainMessage("アイン：今から攻撃に転じるぜ、って言ってるようなモンだな。");

                    UpdateMainMessage("カール：・・・フム。");

                    UpdateMainMessage("カール：貴君のその基本センス、高い先天性を有しているようだが。");

                    UpdateMainMessage("カール：それがランディスにとっては、格好の的とも言える。");

                    UpdateMainMessage("アイン：ッゲ・・・");

                    UpdateMainMessage("カール：貴君は考え方が非常に一貫しており、かつ、洗練されている故");

                    UpdateMainMessage("カール：貴君の行動には乱れや揺らぎが発生しにくいため、我にとっては非常に掴みやすい。");

                    UpdateMainMessage("アイン：マジか・・・");

                    UpdateMainMessage("カール：本スキルは完全なる防御に徹するための戦術。そう捉えても良いだろう。");

                    UpdateMainMessage("アイン：守ってても勝てなくないですか？");

                    UpdateMainMessage("カール：ガードスキルが何か別の主戦術を補うものであるとすれば。");

                    UpdateMainMessage("カール：あるいは、多段戦術の一角を匂わせるためのダミー行動。");

                    UpdateMainMessage("カール：更にあるとすれば、２ラインの戦術を交互に行うための布石であるとも考えられる。");

                    UpdateMainMessage("アイン：確かにボケ師匠はいつも最初同じ格好のクセに、大概やり方が無茶苦茶だよな・・・");

                    UpdateMainMessage("アイン：そうか・・・一つの行動に付き、より多くの選択肢を考慮しろって事ですよね？");

                    UpdateMainMessage("カール：その通りだ。");

                    UpdateMainMessage("アイン：『静』か・・・防衛的戦闘スタイルとかもありそうだな・・・確かに・・・");

                    UpdateMainMessage("カール：知識は全ての源、忘れるな。");

                    UpdateMainMessage("アイン：はい、ありがとうございました！");

                    mc.ReflexSpirit = true;
                    ShowActiveSkillSpell(mc, Database.REFLEX_SPIRIT);
                    UpdateMainMessage("", true);
                }
                #endregion
                #region "ニュートラル・スマッシュ"
                else if ((mc.Level >= 35) && (!mc.NeutralSmash))
                {
                    UpdateMainMessage("アイン：っとと・・・着いたみたいだな");

                    UpdateMainMessage("カール：来たか、それでは講義を始めるとしよう。");

                    UpdateMainMessage("アイン：はい、お願いします！");

                    UpdateMainMessage("　　　『アインは集中して講義の内容を聞いた！』");

                    UpdateMainMessage("カール：完全逆性質となる『動』と『静』、この複合においても極めて特殊。");

                    UpdateMainMessage("カール：動作の一貫として、動へと転じる体型に加え、静の体型も乱してはならない。");

                    UpdateMainMessage("カール：体型のイメージは総じて動と静が相殺され、通常行動スタイルと何ら変わりは無くなる。");

                    UpdateMainMessage("カール：スキル消費を完全になくし、完全なる通常攻撃。インスタント行動も可能。");

                    UpdateMainMessage("カール：『ニュートラル・スマッシュ』、使いこなしてみると良い。");

                    UpdateMainMessage("アイン：完全なる・・・通常攻撃・・・");

                    UpdateMainMessage("アイン：・・・しかし、これはどうなってるんだ？");

                    UpdateMainMessage("アイン：え・・・っと・・・いや、待てよ・・・");

                    UpdateMainMessage("アイン：・・・　・・・　・・・");

                    UpdateMainMessage("　　　『アインはいつもの表情を崩し、今までにない真剣な表情となった。』");

                    UpdateMainMessage("アイン：・・・おいおい、っちょ、待ってくれよコレって・・・");

                    UpdateMainMessage("アイン：スキルポイントはペース配分が肝だ。それなのに、このスキルにはそれが無い。");

                    UpdateMainMessage("アイン：そういやFiveSeekerには、技の達人が居ましたよね？");

                    UpdateMainMessage("カール：気づきが良いな。そう、彼「ヴェルゼ・アーティ」は好んでそれを多用していた。");

                    UpdateMainMessage("アイン：技が上がれば、インスタント値の回復は早いって事は・・・");

                    UpdateMainMessage("　　　『アインは異常なまでに冷や汗をかき始めた！』");

                    UpdateMainMessage("アイン：オイオイ・・・おいおいおい！　冗談じゃねえぞコレ！！");

                    UpdateMainMessage("カール：気づいた様だな。その通りだ。");

                    UpdateMainMessage("アイン：１ターンにおける直接攻撃回数が膨れ上がるじゃねえか！！！");

                    UpdateMainMessage("アイン：そ、そりゃ確かにインスタント行動中に、隙を見て何か入れられたら嫌だけどさ。");

                    UpdateMainMessage("アイン：ってか、スキル消費ねえし、ほとんど任意のタイミングじゃねえか！？");

                    UpdateMainMessage("カール：その使用方法はほぼ無限。");

                    UpdateMainMessage("カール：貴君も今、それを身につけた事となる。");

                    UpdateMainMessage("カール：思う存分に使用すると良いだろう。");

                    UpdateMainMessage("アイン：カ、カール先生！");

                    UpdateMainMessage("アイン：そのなんて言って良いか・・・ありがとうございました！");

                    UpdateMainMessage("カール：貴君のポテンシャルは非常に高い。");

                    UpdateMainMessage("カール：我の教えを必ず活かせるようになる事を期待する。");

                    UpdateMainMessage("カール：後は、ランディスとの実践訓練でもすると良いだろう。");

                    UpdateMainMessage("アイン：ハ、ハイ！");

                    UpdateMainMessage("カール：知識は全ての源、忘れるな。");

                    UpdateMainMessage("アイン：ありがとうございました！");

                    mc.NeutralSmash = true;
                    ShowActiveSkillSpell(mc, Database.NEUTRAL_SMASH);
                }
                #endregion
                #region "【元核】習得"
                else if ((mc.Level >= 40) && (!we.availableArchetypeCommand))
                {
                    if (we.Truth_CommunicationSinikia30DuelFail == false)
                    {
                        UpdateMainMessage("アイン：っとと・・・着いたみたいだな。");

                        UpdateMainMessage("アイン：カール先生、居るか？");

                        UpdateMainMessage("アイン：・・・いねえかな・・・");

                        UpdateMainMessage("アイン：（しかし、何となくだが・・・)");

                        UpdateMainMessage("アイン：（気配はねえが、妙な威圧感が空気に漂ってやがる）");

                        UpdateMainMessage("　　【【【　アインは威圧感の源泉を探り始めた。　】】】");

                        UpdateMainMessage("アイン：カール先生、居るんだろ？");

                        UpdateMainMessage("アイン：・・・　・・・");

                        UpdateMainMessage("アイン：（絶対にどこかにいる。この感覚、間違いねえ。）");

                        UpdateMainMessage("　　　『その瞬間、アインの目の前に３本のツララが突如発生した！！』");

                        UpdateMainMessage("アイン：っげぇ！！");

                        UpdateMainMessage("　　　『アインはとっさに避けようとし・・・』");

                        UpdateMainMessage("カール：ブルーバレットに続けて、ワード・オブ・アティチュード発動。");

                        UpdateMainMessage("カール：続けて即座に、ブラック・ファイアだ。");

                        UpdateMainMessage("　　　『ッボゥ！！！』");

                        UpdateMainMessage("アイン：ッグハ！");

                        UpdateMainMessage("　　　『アインの魔法防御力が下げられた！』");

                        UpdateMainMessage("カール：食らった所、大変貴君には申し訳ないが、今から実践講義を行う。");

                        UpdateMainMessage("アイン：え、えええ！？　マジかよ！？　今既に戦闘中なんじゃねぇのかよ！？");

                        UpdateMainMessage("カール：貴君に");

                        UpdateMainMessage("アイン：え？");

                        UpdateMainMessage("カール：奥義『元核』の基礎を授けよう。");

                        UpdateMainMessage("アイン：奥義？");

                        UpdateMainMessage("カール：ッフ、ダーケン・フィールド！");

                        UpdateMainMessage("アイン：ッゲ！シマッた！");

                        UpdateMainMessage("カール：ッフハハ、集中が切れておるようだな。");

                        UpdateMainMessage("アイン：オイオイオイ、どっちなんだよ、ックソ！");

                        UpdateMainMessage("カール：奥義『元核』は一朝一夕でどうにかなるものではない。");

                        UpdateMainMessage("カール：アウステリティ・マトリクス、発動。");

                        UpdateMainMessage("アイン：っとぉ、そいつはスタンス・オブ・アイズでカウンターだ！");

                        UpdateMainMessage("カール：インスタントで、レッド・ドラゴン・ウィル。");

                        UpdateMainMessage("　　　『カール爵の【火】属性の魔法攻撃力が格段に上昇した！』");

                        UpdateMainMessage("アイン：っげぇ！！！");

                        UpdateMainMessage("カール：ッフハハ、このまま蹴散らさせてもらおう。");

                        UpdateMainMessage("アイン：っくそ、奥義の話はどうなったんだよ・・・");

                        UpdateMainMessage("カール：では、このままDUELを執り行う。");

                        UpdateMainMessage("アイン：おいおいおい、こんな所からかよ！？");

                        UpdateMainMessage("カール：ッフハハハ、冗談だ。BUFF効果やライフ、マナなどは全て全快が基本ルールであるからな。");

                        UpdateMainMessage("アイン：ッホ・・・（でもやっぱ、この人ムチャクチャだ・・・）");

                        UpdateMainMessage("カール：ところで、ランディスは貴君に対し、かなり指導的な行動を取っているようだが、");

                        UpdateMainMessage("アイン：ッゲェ・・・あれのどこが指導的なんだよ・・・");

                        UpdateMainMessage("カール：ヤツは貴君に対して、甘すぎる。");

                        UpdateMainMessage("カール：そのような事では、到底、奥義【元核】は習得できないものと思え。");

                        UpdateMainMessage("アイン：実際、どうすりゃいいんだ？");

                        UpdateMainMessage("カール：ふむ。");

                        UpdateMainMessage("カール：奥義【元核】とはその個々の本質そのものを指す。");

                        UpdateMainMessage("カール：その個々の本質とは、本人にのみ知りうるものであって、他者が貴君に教えたり授けたりするものではない。");

                        UpdateMainMessage("アイン：ってことは・・・カール先生から教えてもらうってわけには行かないのか？");

                        UpdateMainMessage("カール：そのとおりだ。");

                        UpdateMainMessage("アイン：うーん・・・");

                        UpdateMainMessage("カール：だが、引き出すための指南、ある程度であれば可能である。");

                        UpdateMainMessage("カール：やってみるかね、アイン・ウォーレンス。");

                        UpdateMainMessage("アイン：おお！もちろんですよ、是非！！");

                        UpdateMainMessage("アイン：で、どうすれば良いんですか？");

                        UpdateMainMessage("カール：この我自ら、真剣勝負のDUELを貴君に申し込む。");

                        UpdateMainMessage("アイン：っな！！！");

                        UpdateMainMessage("　　【【【　アインは突如、背筋に異常な威圧感を感じ始めた　】】】");

                        UpdateMainMessage("アイン：DUELっつっても、さっき冗談って");

                        UpdateMainMessage("カール：貴君に今一度、問おう。");

                        UpdateMainMessage("カール：本気で示す戦闘術とは何かを。");

                        UpdateMainMessage("アイン：それってどういう意味だ？");

                        UpdateMainMessage("カール：ランディスに聞く限り、貴君はあらゆる局面において");

                        UpdateMainMessage("カール：手加減、いわゆる手抜きを行っておる。");

                        UpdateMainMessage("アイン：いやいや、してねえって。DUELでは特にそのつもりだ。");

                        UpdateMainMessage("カール：その心構え、我には筒抜けであることを知れ。");

                        UpdateMainMessage("カール：我の問いの意図は、理解はしておるだろう。");

                        UpdateMainMessage("カール：奥義を会得するかどうかは、あくまで貴君次第。");

                        UpdateMainMessage("　　【【【　異常な威圧感は殺気へと変わり始める　】】】");

                        UpdateMainMessage("カール：我は貴君を殺すつもりで行く。");

                        UpdateMainMessage("カール：貴君も我を殺すつもりで挑むと良いだろう。");

                        UpdateMainMessage("カール：さもなくば、貴君はこの場で果てる。死あるのみだ。");

                        UpdateMainMessage("アイン：（やっべぇ・・・マジで勝てそうにねえ・・・）");

                        UpdateMainMessage("アイン：（でも・・・やるしか・・・ねえ！！）");

                        UpdateMainMessage("アイン：すうぅぅぅ・・・");

                        UpdateMainMessage("アイン：・・・ふうぅぅぅ");

                        UpdateMainMessage("アイン：DUELで、手加減はしねえ。相手に対して失礼だからな。");

                        UpdateMainMessage("　　『アインはカールハンツに対して、無表情の顔付きでッスっと剣を構え始めた』");

                        UpdateMainMessage("アイン：・・・行くぜ。");

                        UpdateMainMessage("カール：来るがいい。");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：っとと・・・着いたみたいだな。");

                        UpdateMainMessage("アイン：カール先生。");

                        UpdateMainMessage("カール：どうした。");

                        UpdateMainMessage("アイン：挑戦させてくれ、奥義『元核』の習得。");

                        UpdateMainMessage("カール：挑む姿勢は認めよう。");

                        UpdateMainMessage("カール：だが、貴君が我を殺すつもりで来なければ、奥義習得の道は無いと思え。");

                        UpdateMainMessage("アイン：ああ、分かった。");

                        UpdateMainMessage("　　『アインはカールハンツに対して、無表情の顔付きでッスっと剣を構え始めた』");

                        UpdateMainMessage("アイン：DUEL・・・勝負だ。");

                        UpdateMainMessage("カール：来るがいい");
                    }
                    bool result = BattleStart(Database.DUEL_SINIKIA_KAHLHANZ, true);
                    if ((result) ||
                        ((result == false) && (we.Truth_CommunicationSinikia30DuelFailCount >= 3)))
                    {
                        // 勝った場合、次の会話へ
                        GroundOne.WE2.WinOnceSinikiaKahlHanz = true;
                        GroundOne.WE2.AvailableArcheTypeCommand = true;
                        mc.Syutyu_Danzetsu = true;
                        we.availableArchetypeCommand = true;

                        if ((result == false) && (we.Truth_CommunicationSinikia30DuelFailCount >= 3))
                        {
                            UpdateMainMessage("アイン：ッグアァ！！　・・・ッ・・・");

                            UpdateMainMessage("　　【【【　アインは壊滅的なダメージを喰らい、大量の血を吐きだした　】】】");

                            UpdateMainMessage("カール：ここまでのようだな。");

                            UpdateMainMessage("アイン：ッゥ・・・ック！");

                            UpdateMainMessage("アイン：コ・・・ココだ！！！");

                            UpdateMainMessage("カール：ッ！");

                            UpdateMainMessage("　　『　　　それは　　　　　』");

                            UpdateMainMessage("　　『　　　一瞬の出来事　　　　　』");

                            UpdateMainMessage("カール：ッム！");

                            UpdateMainMessage("　　『　　　カールハンツ爵の瞬速の詠唱開始タイミング　　』");

                            UpdateMainMessage("カール：ワン・イムー・・・");

                            UpdateMainMessage("　　『　　　アイン・ウォーレンスは　　』");

                            UpdateMainMessage("アイン：（見つけた・・・詠唱タイミング！！！）");

                            UpdateMainMessage("　　『　　　極限の状況の中　　』");

                            UpdateMainMessage("カール：ッ！！");

                            UpdateMainMessage("　　『　　瞬間的なる時間停止にも等しい刹那　　』　　");

                            UpdateMainMessage("アイン：ッラアアアァ！");

                            UpdateMainMessage("　　『　　ッドシュ・・・！！！　　』");

                            UpdateMainMessage("カール：ッグ・・・ハ・・・");

                            UpdateMainMessage("アイン：（だ、だめか・・・意識が・・・）");
                        }
                        else
                        {
                            UpdateMainMessage("カール：ッ！");

                            UpdateMainMessage("　　『　　　それは　　　　　』");

                            UpdateMainMessage("アイン：っちぃ！！次がヤベェ！");

                            UpdateMainMessage("　　『　　　一瞬の出来事　　　　　』");

                            UpdateMainMessage("カール：ッム！");

                            UpdateMainMessage("　　『　　　カールハンツ爵の瞬速の詠唱開始タイミング　　』");

                            UpdateMainMessage("カール：ワン・イムー・・・");

                            UpdateMainMessage("　　『　　　アイン・ウォーレンスは　　』");

                            UpdateMainMessage("アイン：（スタティック・バリアからワン・イムーニティに見せかけ・・・ココ！！！）");

                            UpdateMainMessage("　　『　　　極限の状況の中　　』");

                            UpdateMainMessage("カール：ッ！！");

                            UpdateMainMessage("　　『　　瞬間的なる時間停止にも等しい刹那　　』　　");

                            UpdateMainMessage("アイン：ッラアアアァ！");

                            UpdateMainMessage("　　『　　ッドシュ・・・！！！　　』");

                            UpdateMainMessage("カール：ッグ・・・ハ・・・");

                            UpdateMainMessage("アイン：ックソ、ハズれたか！　しまった！！！");

                            UpdateMainMessage("アイン：ヤベェ！！イムーニティからヴォルカニック・ウェイヴ連発が！！！");
                        }

                        UpdateMainMessage("　　『　　・・・（ドサッ・・・）　　』");

                        UpdateMainMessage("　　『　　カールハンツの胴体はわずかな音と共に、その場に伏せた。　　』");

                        UpdateMainMessage("アイン：っえ！？");

                        UpdateMainMessage("アイン：カ、カール先生大丈夫ですか！！！");

                        UpdateMainMessage("カール：ッフ・・・");

                        UpdateMainMessage("カール：ッフハハ、ッフハハハハハハ！");

                        UpdateMainMessage("アイン：え、えーと・・・");

                        UpdateMainMessage("カール：我の負けだ。");

                        if ((result == false) && (we.Truth_CommunicationSinikia30DuelFailCount >= 3))
                        {
                            UpdateMainMessage("カール：DUELは終了だ、ひとまず回復呪文をかけておいてやろう。");

                            UpdateMainMessage("カール：ゲイルウィンド、そしてサークレッドヒール。");

                            UpdateMainMessage("　　『アインはほんのり回復した気がした』");
                        }

                        UpdateMainMessage("カール：今の一撃、見事なり。");

                        UpdateMainMessage("アイン：今の一撃？");

                        UpdateMainMessage("　　『　　カールハンツの胴体がそのまま浮き上がる様にしてもとの立ち姿勢に戻った。　　』");

                        UpdateMainMessage("カール：その様子では、自分自身で掴みきれておらぬ感じだな。");

                        UpdateMainMessage("アイン：俺、カール先生にそんな致命的な一撃を与えていましたか？？");

                        UpdateMainMessage("カール：紛れもなく。");

                        UpdateMainMessage("アイン：いつ？");

                        UpdateMainMessage("カール：我が伏する直前に。");

                        UpdateMainMessage("アイン：どんな風に？");

                        UpdateMainMessage("カール：剣による斬り込み。");

                        UpdateMainMessage("アイン：・・・俺自身が？？");

                        UpdateMainMessage("カール：その通りだ。");

                        UpdateMainMessage("アイン：・・・　・・・");

                        UpdateMainMessage("カール：お主が名付けると良い。");

                        UpdateMainMessage("アイン：え？");

                        UpdateMainMessage("カール：奥義『元核』は人により千差万別。");

                        UpdateMainMessage("カール：貴君自身が納得の行く名称をつけると良いだろう。");

                        UpdateMainMessage("アイン：えーと・・・名称・・・名称・・・");

                        UpdateMainMessage("アイン：何だろう・・・つっても、全然どうやったか思い出せないんだが");

                        UpdateMainMessage("アイン：・・・　・・・");

                        UpdateMainMessage("アイン：集中・・・");

                        UpdateMainMessage("アイン：・・・　集中と　・・・");

                        UpdateMainMessage("アイン：断絶");

                        UpdateMainMessage("カール：フム");

                        UpdateMainMessage("アイン：『集中と断絶』で、どうかな？カール先生。");

                        UpdateMainMessage("カール：貴君の気のゆくままで良かろう。");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "アインは奥義【元核】『集中と断絶』を習得した！";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("アイン：なんかさ・・・");

                        UpdateMainMessage("カール：どうした。");

                        UpdateMainMessage("アイン：もっとこう、「やったぜ！！」とか「ついにきた！！！」って感触があると思ったんだが");

                        UpdateMainMessage("アイン：今は、全然と言っていいほど実感が無い・・・");

                        UpdateMainMessage("アイン：・・・そんな実感だな。");

                        UpdateMainMessage("カール：本人にのみ、知覚可能な領域であり、本人にとっての唯一無二。");

                        UpdateMainMessage("カール：本人にとって、五感では認識し得ない領域であり、本人の深淵に眠る心にのみ認識しうる。");

                        UpdateMainMessage("カール：その本人の心にのみコンタクトが取れた瞬間から発動が可能となる。");

                        UpdateMainMessage("カール：探り、導き出すのではなく、元から存在している心。それを貴君自身が体現させる。");

                        UpdateMainMessage("カール：奥義『元核』は、そういうものである。");

                        UpdateMainMessage("アイン：・・・ああ・・・確かに。");

                        UpdateMainMessage("アイン：何か、すげえ自然だ。理論としても、感情面からも自然だった。");

                        UpdateMainMessage("アイン：しかし、カール先生を倒したってのがありえないぜ・・・当たった気がしなかったもんな。");

                        UpdateMainMessage("アイン：カール先生さ、結構オーバーアクションで倒れてくれたんだろ？");

                        UpdateMainMessage("カール：その通りだ。");

                        UpdateMainMessage("アイン：げ、マジかよ！　やっぱり、かすってた程度だったんだろ。っくそぉ・・・");

                        UpdateMainMessage("カール：もう教える事はない。そのまま帰ると良い。");

                        UpdateMainMessage("アイン：そっか・・・何か名残惜しいけど・・・");

                        UpdateMainMessage("アイン：今日は本当、ありがとうございました！！");

                        UpdateMainMessage("カール：もうよい、行くがよい。");

                        UpdateMainMessage("アイン：はい、どうもでした！！！");

                        UpdateMainMessage("　　『アインは転送装置により町へと戻っていった。　』");

                        UpdateMainMessage("カール：・・・");

                        UpdateMainMessage("カール：ッグ・・・ッグホォ！！！");

                        UpdateMainMessage("　　『カールハンツはその場で大量の吐血をし、胴体から赤い線を大量に流し始めた！！　』");

                        UpdateMainMessage("カール：ッグ・・・ッムゥ・・・グ、ッグホ！ッゴホ！！");

                        UpdateMainMessage("？？？：大丈夫ですか？カールハンツ。");

                        UpdateMainMessage("カール：ッグ・・・貴様");

                        UpdateMainMessage("カール：フ・・・ファラか。");

                        UpdateMainMessage("カール：ッグ・・・ッゲホ、ッゴホ！！！");

                        UpdateMainMessage("ファラ：ウフフ、どうやら、かなり食い込まれたみたいですね（＾＾");

                        UpdateMainMessage("ファラ：セレスティアル・ノヴァ・エグゼ");

                        UpdateMainMessage("　　『カールハンツの致命傷がみるみる回復し始めた』　");

                        UpdateMainMessage("カール：ッグ・・・ッフゥ・・・");

                        UpdateMainMessage("ファラ：はい、もう大丈夫だと思いますよ（＾＾");

                        UpdateMainMessage("カール：世話をかけた。");

                        UpdateMainMessage("ファラ：ひょっとして、私の回復呪文以外だったら、【死】だったのではありませんか？");

                        UpdateMainMessage("カール：笑止");

                        UpdateMainMessage("ファラ：どうやら当たりのようですね（＾＾");

                        UpdateMainMessage("ファラ：私も見ていましたけど、彼の斬り込み、相当なものでしたわ。");

                        UpdateMainMessage("カール：ランディスが目を付ける理由。分からなくもない。");

                        UpdateMainMessage("カール：今の時点で【元核】によるダメージがあの威力となれば、おそらく。");

                        UpdateMainMessage("ファラ：そうね、内容は直接攻撃を一撃だけだから。");

                        UpdateMainMessage("ファラ：ウフフ、将来はきっと限りなく無限に近いダメージなりそうね（＾＾");

                        UpdateMainMessage("ファラ：よかったわね、本当に死ななくて（＾＾");

                        UpdateMainMessage("カール：ッフハハハ、冗談が過ぎるのではないか、ファラ王妃。");

                        UpdateMainMessage("ファラ：ウフフ、無理しないでくださいね、こちらも回復作業が大変ですから（＾＾");

                        UpdateMainMessage("カール：フム、肝に命じる。");

                        if (!we.AlreadyRest)
                        {
                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_EVENING);
                        }
                        else
                        {
                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                        }
                        this.buttonHanna.Visible = true;
                        this.buttonDungeon.Visible = true;
                        this.buttonRana.Visible = true;
                        this.buttonGanz.Visible = true;
                        this.buttonPotion.Visible = true;
                        this.buttonDuel.Visible = true;
                        this.buttonShinikia.Visible = true;

                        UpdateMainMessage("　　『　アインは転送装置から町へと戻ってきて・・・　』");

                        UpdateMainMessage("アイン：っふうぅ・・・戻りっと・・・");

                        UpdateMainMessage("アイン：っと、うわっとと！！");

                        UpdateMainMessage("　　『　アインは突如、足を崩してしまった。　』");

                        UpdateMainMessage("アイン：っと、クソ・・・なんでもねえ所で、変に足にきたな。");

                        UpdateMainMessage("アイン：・・・心なしか・・・");

                        UpdateMainMessage("アイン：（足に妙に力が入らねえ。）");

                        UpdateMainMessage("アイン：（さっき発動した奥義は思ったより身体に負担が大きいみたいだな・・・）");

                        UpdateMainMessage("アイン：（こりゃあ、一日に出来て１回だな。連発はできそうもねえ。）");

                        UpdateMainMessage("アイン：（使いどころは難しそうだ、気を付けないとな。）");

                        UpdateMainMessage("アイン：それはそうと・・・後でラナにも、奥義の話を伝えてやるとするか！");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：ッグアァ！！　・・・ッ・・・");

                        UpdateMainMessage("　　【【【　アインは壊滅的なダメージを喰らい、大量の血を吐きだした　】】】");

                        UpdateMainMessage("カール：ここまでのようだな。");

                        UpdateMainMessage("アイン：ッゥ・・・");

                        UpdateMainMessage("カール：今の攻撃で、なお生き存えているのは、賞賛に値する。");

                        UpdateMainMessage("アイン：・・・ッ・・・");

                        UpdateMainMessage("カール：このまま殺すのは惜しい、回復呪文をかけておいてやろう。");

                        UpdateMainMessage("カール：ゲイルウィンド、そしてサークレッドヒール。");

                        UpdateMainMessage("　　『アインはほんのり回復した気がした』");

                        UpdateMainMessage("アイン：ッグ・・・ッツ・・・");

                        UpdateMainMessage("カール：貴君には素質が無かったと見える。そのまま帰るがよい。");

                        we.Truth_CommunicationSinikia30DuelFail = true;
                        we.Truth_CommunicationSinikia30DuelFailCount++;

                        if (!we.AlreadyRest)
                        {
                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_EVENING);
                        }
                        else
                        {
                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                        }
                        this.buttonHanna.Visible = true;
                        this.buttonDungeon.Visible = true;
                        this.buttonRana.Visible = true;
                        this.buttonGanz.Visible = true;
                        this.buttonPotion.Visible = true;
                        this.buttonDuel.Visible = true;
                        this.buttonShinikia.Visible = true;

                    }
                }
                #endregion
                else
                {
                    UpdateMainMessage("アイン：っとと・・・着いたみたいだな");

                    UpdateMainMessage("アイン：あのすいません？");

                    UpdateMainMessage("カール：どうした。");

                    UpdateMainMessage("アイン：すいません、ちょっと講義でもお願いしたいのですが、");

                    UpdateMainMessage("カール：今、教える事はない。　帰るがよい。");

                    UpdateMainMessage("アイン：ハイ・・・");
                }

                BackToTown();
            }
            else
            {
                UpdateMainMessage("アイン：カールハンツ爵にはまた今度教えてもらうとしよう。", true);
            }
            #endregion
        }

        private void ButtonVisibleControl(bool visible)
        {
            this.buttonHanna.Visible = visible;
            this.buttonDungeon.Visible = visible;
            this.buttonRana.Visible = visible;
            this.buttonGanz.Visible = visible;
            if (we.AvailablePotionshop)
            {
                this.buttonPotion.Visible = visible;
            }
            if (we.AvailableDuelColosseum)
            {
                this.buttonDuel.Visible = visible;
            }
            if (we.AvailableBackGate)
            {
                this.buttonShinikia.Visible = visible;
            }
        }

        private void Blackout()
        {
            this.buttonHanna.Visible = false;
            this.buttonDungeon.Visible = false;
            this.buttonRana.Visible = false;
            this.buttonGanz.Visible = false;
            this.buttonPotion.Visible = false;
            this.buttonDuel.Visible = false;
            this.buttonShinikia.Visible = false;
            this.BackColor = Color.Black;
            ChangeBackgroundData(null);
            this.Invalidate();
        }
        
        private void ShowActiveSkillSpell(MainCharacter player, string skillSpellName)
        {
            using (TruthSkillSpellDesc skillSpell = new TruthSkillSpellDesc())
            {
                skillSpell.StartPosition = FormStartPosition.CenterParent;
                skillSpell.SkillSpellName = skillSpellName;
                skillSpell.Player = player;
                skillSpell.ShowDialog();
            }
        }

        // 幼馴染ラナ
        private void button3_Click(object sender, EventArgs e)
        {
            #region "現実世界"
            if (GroundOne.WE2.RealWorld && GroundOne.WE2.SeekerEvent601 && !GroundOne.WE2.SeekerEvent602)
            {
                UpdateMainMessage("ラナ：っあら、意外と早いじゃない。");

                UpdateMainMessage("アイン：ああ、何だか寝覚めが良いんだ。今日も調子全快だぜ！");

                UpdateMainMessage("ラナ：バカな事言ってないで、ホラホラ、朝ごはんでも食べましょ。");

                UpdateMainMessage("アイン：ああ、そうだな！じゃあ、ハンナ叔母さんとこで食べようぜ。");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "ハンナの宿屋（料理亭）にて・・・";
                    md.ShowDialog();
                }

                UpdateMainMessage("アイン：っさっすが、叔母さん！今日の飯もすげえ旨いよな！");

                UpdateMainMessage("ハンナ：アッハッハ、よく元気に食べるね。まだ沢山あるからね、どんどん食べな。");

                UpdateMainMessage("ラナ：アイン、少しは控えなさいよね。恥ずかしいったら。");

                UpdateMainMessage("アイン：ああ、控えるぜ。次からな！ッハッハッハ！！！");

                UpdateMainMessage("　　　『ッドス！』（ラナのサイレントブローがアインの横腹に炸裂）　　");

                UpdateMainMessage("アイン：うおおぉぉ・・・だから食ってる時にそれをやるなって・・・");

                UpdateMainMessage("アイン：・・・ッムグ・・・ごっそうさん！っでだ、ラナ。");

                UpdateMainMessage("ラナ：え？");

                UpdateMainMessage("アイン：オレはダンジョンへ向かうぜ。");

                UpdateMainMessage("アイン：そして、その最下層へオレは辿り付いてみせる！");

                UpdateMainMessage("ラナ：っちょ、何よいきなり唐突に。");

                UpdateMainMessage("ラナ：全然脈略が無いじゃない。何よ、本当にそんなトコ行きたいわけ？");

                UpdateMainMessage("アイン：ああ、本当だ。");

                UpdateMainMessage("アイン：金を稼いで収支を成り立たせるってのも当然なんだが、");

                UpdateMainMessage("アイン：伝説のFiveSeekerに追いつきたい気持ちもあるし、それに何より。");

                UpdateMainMessage("アイン：・・・　・・・　・・・");

                UpdateMainMessage("アイン：行かなくちゃ、ならないんだ。");

                UpdateMainMessage("ラナ：そ、そう・・・");

                UpdateMainMessage("アイン：っと、そういえばそうだ。忘れないうちに・・・");

                UpdateMainMessage("ラナ：何探してるのよ？");

                UpdateMainMessage("アイン：確かポケットに入れたはず・・・");

                while(true)
                {
                    using (TruthDecision td = new TruthDecision())
                    {
                        td.MainMessage = "　【　ラナにイヤリングを渡しますか？　】";
                        td.FirstMessage = "ラナにイヤリングを渡す。";
                        td.SecondMessage = "ラナにイヤリングを渡さず、ポケットにしまっておく。";
                        td.StartPosition = FormStartPosition.CenterParent;
                        td.ShowDialog();
                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            UpdateMainMessage("アイン：（・・・いや・・・）");

                        }
                        else
                        {
                            UpdateMainMessage("アイン：（・・・このイヤリング・・・）");

                            UpdateMainMessage("アイン：（これをもってると、何か思い出せそうだ・・・）");

                            UpdateMainMessage("アイン：（ラナには悪いが、もう少し持っておこう・・・）");

                            UpdateMainMessage("アイン：いや、何でもねえんだ。");

                            UpdateMainMessage("ラナ：今、ポケットをゴソゴソしてたじゃないの？");

                            UpdateMainMessage("アイン：い、いやいや。何でもねえ、ッハッハッハ！");

                            UpdateMainMessage("ラナ：何よ、あからさまに怪しかったわよ？今のは・・・");

                            UpdateMainMessage("アイン：いざ、ダンジョン！ッハッハッハ！");
                            break;
                        }
                    }
                    we.AlreadyCommunicate = true;
                }
                GroundOne.WE2.SeekerEvent602 = true;
                Method.AutoSaveTruthWorldEnvironment();
                Method.AutoSaveRealWorld(this.MC, this.SC, this.TC, this.WE, null, null, null, null, null, this.Truth_KnownTileInfo, this.Truth_KnownTileInfo2, this.Truth_KnownTileInfo3, this.Truth_KnownTileInfo4, this.Truth_KnownTileInfo5);
            }
            else if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd && GroundOne.WE2.SeekerEvent602)
            {
                UpdateMainMessage(MessageFormatForLana(1002), true);
            }
            #endregion
            #region "四階開始時"
            else if (we.TruthCompleteArea3 && !we.Truth_CommunicationLana41)
            {
                we.Truth_CommunicationLana41 = true;
                UpdateMainMessage("アイン：よおラナ、４階へ行く準備は出来たか？");

                UpdateMainMessage("ラナ：私は当然OKよ。アインの方は？");

                UpdateMainMessage("アイン：俺はまあ大体オッケーだな。");

                UpdateMainMessage("アイン：それよりだ、ラナ聞いてくれ。一つ気になってる事があるんだ。");

                UpdateMainMessage("ラナ：え、なによいきなり？");

                UpdateMainMessage("アイン：クヴェルタ街ってあるだろ？ほら、ガンツおじさんの知り合いの・・・");

                UpdateMainMessage("ラナ：ヴァスタおじさまの事？");

                UpdateMainMessage("アイン：そうそう！そのヴァスタじいさん！");

                UpdateMainMessage("ラナ：じいさんって言ったら、怒られるわよ・・・");

                UpdateMainMessage("アイン：いやいや、悪い悪い。でだ、そのヴァスタおじさんなんだが");

                UpdateMainMessage("アイン：極剣ゼムルギアスって確か・・・");

                UpdateMainMessage("ラナ：そうね、確かヴァスタおじさまが人生の全てをかけて、５０年越しで完成させた剣だそうよ♪");

                UpdateMainMessage("アイン：おお、確かそうだったよな。");

                UpdateMainMessage("ラナ：それがどうかしたの？？");

                UpdateMainMessage("アイン：・・・あと、もう一つなんかあったよな。");

                UpdateMainMessage("ラナ：双剣ジュノ・セレステ。ファージル宮殿に飾ってある至宝の一つよ。");

                UpdateMainMessage("アイン：ああ、それだそれそれ。");

                UpdateMainMessage("ラナ：それそれって・・・自分で思い出しなさいよね。まったく・・・");

                bool detectFeltus = false;
                if ((mc != null) && (mc.MainWeapon != null) && (mc.MainWeapon.Name == Database.LEGENDARY_FELTUS))
                {
                    detectFeltus = true;
                }
                if ((mc != null) && (mc.SubWeapon != null) && (mc.SubWeapon.Name == Database.LEGENDARY_FELTUS))
                {
                    detectFeltus = true;
                }
                if ((mc != null) && (mc.MainArmor != null) && (mc.MainArmor.Name == Database.LEGENDARY_FELTUS))
                {
                    detectFeltus = true;
                }
                if ((mc != null) && (mc.Accessory != null) && (mc.Accessory.Name == Database.LEGENDARY_FELTUS))
                {
                    detectFeltus = true;
                }
                if ((mc != null) && (mc.Accessory2 != null) && (mc.Accessory2.Name == Database.LEGENDARY_FELTUS))
                {
                    detectFeltus = true;
                }
                ItemBackPack[] backpack = mc.GetBackPackInfo();
                for (int ii = 0; ii < backpack.Length; ii++)
                {
                    if (backpack[ii] != null)
                    {
                        if (backpack[ii].Name == Database.LEGENDARY_FELTUS)
                        {
                            detectFeltus = true;
                            break;
                        }
                    }
                }

                UpdateMainMessage("アイン：で、最後の一つが・・・");

                UpdateMainMessage("ラナ：神剣フェルトゥーシュ。");

                UpdateMainMessage("ラナ：私の母さんが形見としてくれたモノね。");

                if (detectFeltus)
                {
                    UpdateMainMessage("アイン：今は俺が・・・こうして所持している。");
                }

                UpdateMainMessage("アイン：そのフェルトゥーシュはラナの母さんが作った物でも無いんだろ？");

                UpdateMainMessage("ラナ：ええ、そうよ。母さんが、若い頃にヴァスタおじさまからお預かり頂いた物らしいの。");

                UpdateMainMessage("アイン：じゃあ、ヴァスタじいさんが作ったってのか？？");

                UpdateMainMessage("ラナ：それは違うんじゃないかしら。だって極剣を作り続けるのに必死だったはずだし。");

                UpdateMainMessage("アイン：一本の剣に５０年費やしてる間に、実はもう一本作ってました・・・");

                UpdateMainMessage("アイン：そんなわけねえよな。さすがに。");

                UpdateMainMessage("ラナ：うん。");

                if (detectFeltus)
                {
                    UpdateMainMessage("アイン：じゃあ、この神剣に関しては、誰が作ったのかはハッキリしてない？");
                }
                else
                {
                    UpdateMainMessage("アイン：じゃあ、その神剣ってのは誰が作ったのかはハッキリしてない？");
                }

                UpdateMainMessage("ラナ：そういう事になるわね。");

                UpdateMainMessage("アイン：・・・　・・・");

                UpdateMainMessage("アイン：誰なんだ？？");

                UpdateMainMessage("ラナ：知らないわよ。");

                UpdateMainMessage("アイン：秘伝の書みたいなのは無いのか？こう、作成者が載ってる巻物みたいな・・・");

                UpdateMainMessage("ラナ：無いわよ。");

                if (detectFeltus)
                {
                    UpdateMainMessage("アイン：剣の柄か鞘に・・・どっか彫ってねえかな・・・");
                }
                else
                {
                    UpdateMainMessage("アイン：剣の柄か、鞘か何かに小さく彫ってあるとか・・・");
                }

                UpdateMainMessage("ラナ：無いって言ってるじゃない。シツコイわね。");

                UpdateMainMessage("アイン：神剣って言うぐらいだから、神様が作ったとか？");

                UpdateMainMessage("ラナ：あら、神様嫌いのアンタがそれで良いんなら、そういう事で良いんじゃないかしら♪");

                UpdateMainMessage("アイン：ハハッ、言ってくれるぜ。まあ、神様は置いといてだな。");

                UpdateMainMessage("アイン：結局、わからずじまいって事になるのか。");

                UpdateMainMessage("ラナ：う～ん・・・");

                UpdateMainMessage("アイン：ん？");

                UpdateMainMessage("ラナ：ねえ、アインって結局何が知りたかったの？");

                UpdateMainMessage("アイン：何がって言われてもな。");
                
                UpdateMainMessage("アイン：単に神剣フェルトゥーシュの作り手を知りたいだけだ。純粋な興味ってやつかな。");

                UpdateMainMessage("ラナ：・・・う～ん・・・");

                UpdateMainMessage("アイン：な、何だよ。なんかオカシイか？？");

                UpdateMainMessage("ラナ：いや、別にオカシイわけじゃないんだけどね。");

                UpdateMainMessage("アイン：何だ何だ、ハッキリ言ってくれよ？");

                UpdateMainMessage("ラナ：う～ん・・・");

                UpdateMainMessage("ラナ：・・・ねえ");

                UpdateMainMessage("ラナ：アインはその作り手の人をもしも知る事ができたら・・・");

                UpdateMainMessage("ラナ：その時、アインはどうしたいわけ？");

                UpdateMainMessage("アイン：どう・・・って言われてもな。");

                UpdateMainMessage("アイン：特別な目的は無いな。");

                UpdateMainMessage("ラナ：ふ～ん、そうなの？");

                UpdateMainMessage("アイン：ああ。");

                UpdateMainMessage("ラナ：じゃあ・・・ちょっとだけなら。");

                UpdateMainMessage("アイン：ちょっとだけ？？");

                UpdateMainMessage("ラナ：うん。");

                UpdateMainMessage("アイン：何だ、マジで実は知ってるって話か！？教えてくれよ！？");

                UpdateMainMessage("ラナ：ちょっちょっと、いきなり盛り上がらないでよ。大した情報じゃないんだから。");

                UpdateMainMessage("アイン：いやいやいや、どんな些細な事でも良いぜ、教えてくれ！頼む！！");

                UpdateMainMessage("ラナ：う～ん、期待させちゃうとアレなんだけど、まあ良いわ。言うわね。");

                UpdateMainMessage("アイン：ああ、頼むぜ。");

                UpdateMainMessage("ラナ：私の母さんに手渡される前はヴァスタおじさまが所持していた事はもう知ってるよね？");

                UpdateMainMessage("アイン：ああ、そうだな。");

                UpdateMainMessage("ラナ：そのヴァスタおじさまが、誰から譲り受けたのかを私こっそり聞いちゃった事があるのよ。");

                UpdateMainMessage("アイン：ラナ、お前は本当こっそり聞くの大好きだよな。");

                UpdateMainMessage("ラナ：ッフフ、ゴメンね♪　で、ヴァスタおじさまから帰ってきた答えがね。");

                UpdateMainMessage("ラナ：（声マネ）『ヴァスタ：誰から譲り受けたったぁ、ラナちゃん、そらもう答えは目の前にあるってもんよ！』");

                UpdateMainMessage("アイン：答えは・・・目の前？？");

                UpdateMainMessage("ラナ：でね、それじゃちょっと分からないから、もう少し詳しく聞いてみたのよ。");

                UpdateMainMessage("ラナ：そしたら、こう言ってきたの。");

                UpdateMainMessage("ラナ：（声マネ）『ヴァスタ：い～やいやいや、マイッタ！　そりゃ、俺の口からぁ直接は言えねえ、口止めされてんだ。』");

                UpdateMainMessage("アイン：口止め・・・どういう事だ、全然わからねえ。");

                UpdateMainMessage("アイン：知られちゃ困るって話なのか？？");

                UpdateMainMessage("ラナ：どうもそうらしいわよ。");

                UpdateMainMessage("ラナ：でも何か納得行かないなと思って、食い下がってみたの。");

                UpdateMainMessage("アイン：お前よくやるよなあ・・・そういう秘密暴き。");

                UpdateMainMessage("ラナ：まあ、良いじゃない、でね、そしたらね。");

                UpdateMainMessage("ラナ：（声マネ）『ヴァスタ：あああぁ・・・・・・まあ強いて言やぁ・・・・・・』");

                UpdateMainMessage("ラナ：（声マネ）『ヴァスタ：ウォーレンス！！！』");

                UpdateMainMessage("ラナ：（声マネ）『ヴァスタ：ッガーッハハハハハハ！』");

                UpdateMainMessage("アイン：（ッブフゥ！！！）");

                UpdateMainMessage("ラナ：っちょ、吹き出さないでよ、もう。");

                UpdateMainMessage("アイン：いやいやいや、実名の半分言っちゃってるじゃねえか！！口止めされてんじゃねえのかよ！！");

                UpdateMainMessage("アイン：って、俺の姓と同じじゃねえか！！！");

                UpdateMainMessage("ラナ：っでしょ？私もうビックリしちゃって。");

                UpdateMainMessage("アイン：名の方は、言ってくれなかったのか？");

                UpdateMainMessage("ラナ：うん、さすがにそれ以上は何度食い下がってもダメだったわ。");

                UpdateMainMessage("アイン：そっか・・・まあ、そりゃそうだろうな。");

                UpdateMainMessage("ラナ：話はこれでオシマイよ。参考になった？");

                UpdateMainMessage("アイン：ああ、十分だぜ。サンキューな。");

                UpdateMainMessage("アイン：しかし・・・ウォーレンスって姓の人は結構いるしな。");

                UpdateMainMessage("ラナ：そうね、アインに直接関係あるかどうかまでは分からないわね。");

                UpdateMainMessage("アイン：また機会があれば、ヴァスタじいさんに粘り腰で聞いてみるとするか。");

                UpdateMainMessage("ラナ：ッフフ、そうね。また今度一緒にクヴェルタ街に行ってみましょ♪");

                UpdateMainMessage("アイン：ああ、また行こうぜ。");
            }
            #endregion
            #region "三階開始時"
            else if (we.TruthCompleteArea2 && !we.Truth_CommunicationLana31)
            {
                we.Truth_CommunicationLana31 = true;

                UpdateMainMessage("アイン：よお、元気でやってるかラナ。");

                UpdateMainMessage("ラナ：それはこっちのセリフよ。３階向かう準備は万全なの？");

                UpdateMainMessage("アイン：ああ、バックパックは整理済みだ。任せておけって！");

                UpdateMainMessage("ラナ：そういう意味じゃないわよ。心構えの話よ。");

                UpdateMainMessage("アイン：心構えか？そうだなあ・・・");

                UpdateMainMessage("アイン：途中で止めたりはしねえさ。");
                
                UpdateMainMessage("アイン：キッチリ解いてみせる、任せておけ。");

                UpdateMainMessage("ラナ：フフフ、言ってくれるじゃない。安心したわ♪");

                UpdateMainMessage("ラナ：っあ、そういえば、アイン。");

                UpdateMainMessage("アイン：ん？何だよ。");

                UpdateMainMessage("ラナ：う～ん、どうしよっかな。やっぱり、教えない♪");

                UpdateMainMessage("アイン：ッゲ、いきなりまたそのパターンかよ？　教えてくれよ。");

                UpdateMainMessage("ラナ：う～ん、まあ良いわ。今回だけ特別♪");

                UpdateMainMessage("ラナ：カール爵の所へ行く転送装置なんだけど。");

                UpdateMainMessage("アイン：おお、あれがどうかしたか？");

                UpdateMainMessage("ラナ：実はあれ、ファージル宮殿にも繋がってるわよ。");

                UpdateMainMessage("アイン：ッマジかよ！？");

                UpdateMainMessage("アイン：ていうか、なんでそんな事が分かるんだよ！？");

                UpdateMainMessage("ラナ：正直なトコ、きっかけは偶然だったわ。");

                UpdateMainMessage("ラナ：ランディスのお師匠さんがあの転送装置の少し右側の樹木の枝をいじってたのよね。");

                UpdateMainMessage("ラナ：それでなんかあるんじゃないかなって思って草かげから見てたら・・・");

                UpdateMainMessage("ラナ：（声マネ）『ランディス：おい、そこの娘。何か用か。』");

                UpdateMainMessage("アイン：ッゲ、バレたのかよ・・・蜂の巣にされなかったか！？");

                UpdateMainMessage("ラナ：ボコボコにされるのってバカアインが対象の時だけよ♪");

                UpdateMainMessage("アイン：やっぱ俺だけヒデェ扱いなのか・・・まあ女性相手に手をあげる師匠じゃねえけどさ。");
                
                UpdateMainMessage("アイン：で、どういう話だったんだ？");

                UpdateMainMessage("ラナ：【何してるんですか？】って普通に聞いてみたわ。");

                UpdateMainMessage("ラナ：（声マネ）『ランディス：アーティとエルに連絡しに行くトコだ。じゃあな。』");

                UpdateMainMessage("ラナ：って、それだけ言って転送していったのよ。");

                UpdateMainMessage("アイン：アーティ・・・エル・・・");

                UpdateMainMessage("アイン：あぁ、なるほど！　それでファージル宮殿に繋がってるって事か！");

                UpdateMainMessage("ラナ：そういうこと♪　っね、今度行ってみましょ♪");

                UpdateMainMessage("アイン：ああそうだな！　せっかくなんだし行ってみるとするか！　ッハッハッハ！");
            }
            #endregion
            #region "ラナ・複合魔法・スキルの基礎習得"
            else if (we.AvailableMixSpellSkill)
            {
                if (!we.AlreadyCommunicate)
                {
                    we.AlreadyCommunicate = true;

                    #region "ラナ習得済み"
                    if (sc.Level >= 20 && mc.FlashBlaze && !we.Truth_CommunicationLana22)
                    {
                        we.Truth_CommunicationLana22 = true;

                        UpdateMainMessage("アイン：っお、こんなトコに居たのかよ！");

                        UpdateMainMessage("ラナ：アインじゃない。どうかしたの？　やけに嬉しそうだけど");

                        UpdateMainMessage("アイン：聞いてくれ、すげぇんだよこれが！！");

                        UpdateMainMessage("ラナ：ハイハイ、またいつものバカ騒ぎね・・・");

                        UpdateMainMessage("アイン：まあそう言わず、モノは一見にしかず！！！");

                        UpdateMainMessage("ラナ：モノじゃなくて百聞よ。まあそれ以前に１つも聞いてないんだけど。");

                        UpdateMainMessage("アイン：堅い事言うなって。っじゃ、そこで見てろよ？");

                        UpdateMainMessage("ラナ：ハイハイ・・・");

                        UpdateMainMessage("アイン：・・・行くぜ、究極奥義！！");

                        UpdateMainMessage("アイン：スーパー・ハイパー・アルティメット・ゴッド・サンダー！！！");

                        UpdateMainMessage("　　　ッバシュ！！（アインはフラッシュ・ブレイズを放った）　　　");

                        UpdateMainMessage("アイン：っしゃ！決まった！！");

                        UpdateMainMessage("アイン：ッハッハッハッハッハ！！");

                        UpdateMainMessage("ラナ：・・・　・・・");

                        UpdateMainMessage("アイン：どうだ、ラナ。どうだどうだどうだ！？");
                        
                        UpdateMainMessage("アイン：口が開いたまま塞がってねえようだな！　ッハッハッハ！！");

                        UpdateMainMessage("ラナ：何かと思えば、今のフラッシュ・ブレイズでしょ？");

                        UpdateMainMessage("ラナ：基となる火属性のファイア・ボールに聖属性のダメージを追加効果で付けるやつよね。");

                        UpdateMainMessage("ラナ：詠唱方法は、火属性の詠唱中に聖イメージを融合させるから、少し訓練が必要ね。");

                        UpdateMainMessage("ラナ：アイン、直感バカだから無理だと思ってたんだけど、どこで習得したのよ？");

                        UpdateMainMessage("アイン：・・・ッグ・・・");

                        UpdateMainMessage("ラナ：ひょっとして独学で編み出したわけじゃないわよね？");

                        UpdateMainMessage("アイン：ラナ！　お前が何で知ってるんだよ！？");

                        UpdateMainMessage("ラナ：知ってるも何も、知ってるに決まってるじゃない♪");

                        UpdateMainMessage("アイン：解せぬ！！　なぜだ！！！");

                        UpdateMainMessage("ラナ：「解せぬ」ってどこの台詞まわし使ってるのよ・・・");

                        UpdateMainMessage("ラナ：ホラ、私って聖フローラ女学院に飛び級で通ってたでしょ♪");

                        UpdateMainMessage("アイン：ファラ様が設立した学院ぐらい知ってるぜ、それがどうしたってんだ？");

                        UpdateMainMessage("ラナ：そこで複合魔法および複合スキルの基礎を学んだのよ。");

                        UpdateMainMessage("ラナ：ただし、飛び級やエリートクラスの生徒限定の話なんだけどね♪");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "ラナは複合魔法・スキルの基礎を習得済みだった！";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("アイン：・・・ック・・・そういや昔から頭は良かったよな・・・");
                        
                        UpdateMainMessage("アイン：じゃあ、既に習得してたって事か？");

                        UpdateMainMessage("ラナ：う～ん、それがね。そうでもないわ。");
                        
                        UpdateMainMessage("ラナ：学院内じゃ、実践行為は禁止されてたのよ。");

                        UpdateMainMessage("ラナ：一般生徒には教えてない内容だし、大っぴらな公開は禁止だったってトコかしら。");

                        UpdateMainMessage("アイン：じゃあさ、今はどうなんだよ？複合魔法は詠唱できるのか？");

                        UpdateMainMessage("ラナ：う～ん・・・昔１回だけ学院内で試した事はあるぐらいね。");

                        UpdateMainMessage("アイン：禁止じゃなかったのかよ・・・");

                        UpdateMainMessage("アイン：まあ、昔は良いって。今はどうなんだ？");

                        UpdateMainMessage("ラナ：コツを思い出すまでに少し時間はかかると思うけど。");

                        UpdateMainMessage("ラナ：たぶん大丈夫よ♪");

                        UpdateMainMessage("アイン：ラナ・・・お前はやっぱすげえぜ・・・");

                        UpdateMainMessage("ラナ：でもホント驚いたわ。さっきも聞いたけど、アインはどうやって習得したのよ？");

                        UpdateMainMessage("アイン：悪いが秘密にしておきたい。　また今度話すぜ。");

                        UpdateMainMessage("ラナ：そう、分かったわ。");

                        UpdateMainMessage("ラナ：じゃあ、今度からは機会があれば少し訓練しておくわね。");

                        UpdateMainMessage("アイン：ああ、話が早くて助かるぜ。じゃあ、次からは頼んだぜ！");

                        UpdateMainMessage("ラナ：フフフ、任せておいて。こういうのは得意だから♪", true);
                    }
                    #endregion
                    #region "ブルー・バレット"
                    else if ((sc.Level >= 21) && (!sc.BlueBullet))
                    {
                        UpdateMainMessage("ラナ：う～ん・・・このタイミングから・・・");

                        UpdateMainMessage("アイン：お、どうしたラナ？　タイミングがどうしたんだ？");

                        UpdateMainMessage("ラナ：アイン、ちょっとそこに居てちょうだい。");

                        UpdateMainMessage("アイン：ああ。");

                        UpdateMainMessage("ラナ：撃ち抜きなさい、氷の弾丸。　ブルーバレット！");

                        UpdateMainMessage("　　　ッドドド！！　　");

                        UpdateMainMessage("アイン：うおおぉぉぉ、待て待て待て！！");

                        UpdateMainMessage("アイン：んだ今のは。　アイス・ニードルって連射不可能じゃねえのかよ？");

                        UpdateMainMessage("ラナ：大きな氷の形状に対して、少しずつ闇魔法で分断する感じで詠唱するの。");

                        UpdateMainMessage("ラナ：そうすると、小さい弾丸上の氷が出来上がるわけよ♪");

                        UpdateMainMessage("アイン：どうでもいいが、俺を実験台にするんじゃない。");

                        UpdateMainMessage("ラナ：そんな事言っても、どうせ避けちゃうでしょ。");

                        UpdateMainMessage("アイン：今のはたまたま避けられただけだ。");

                        UpdateMainMessage("ラナ：避けないでちゃんと食らってよね、そうじゃないと威力が確認できないんだから♪");

                        UpdateMainMessage("アイン：まてまて、そういうのはもっと他のターゲットで・・・");

                        UpdateMainMessage("　　　ッドドドドドドドドド！！　　");

                        sc.BlueBullet = true;
                        ShowActiveSkillSpell(sc, Database.BLUE_BULLET);
                        UpdateMainMessage("", true);
                    }
                    #endregion
                    #region "ヴァニッシュ・ウェイヴ"
                    else if ((sc.Level >= 22) && (!sc.VanishWave))
                    {
                        UpdateMainMessage("ラナ：確かこんな感じだったかしら。");

                        UpdateMainMessage("アイン：っお、やってるなラナ。新技お披露か？");

                        UpdateMainMessage("ラナ：うん、ちょっと昨日ね、昔読んでた魔法辞典をまた読み返してたの。");

                        UpdateMainMessage("アイン：辞典・・・ッグ、寒気が・・・");

                        UpdateMainMessage("ラナ：ッフフ、まあちょっとやってみるわね。");

                        UpdateMainMessage("ラナ：じゃあ行くわよ、ヴァニッシュ・ウェイヴ！");

                        UpdateMainMessage("アイン：ブ、ッフォオォォ・・・ミゾにもろに・・・");

                        UpdateMainMessage("アイン：今の・・・一体何したんだよ？");

                        UpdateMainMessage("ラナ：さて、バカアインは今の状態で魔法が撃てるかしら♪");

                        UpdateMainMessage("アイン：ッゲ、まさか！");

                        UpdateMainMessage("アイン：（・・・ッファイア！）");

                        UpdateMainMessage("アイン：ックソ、やっぱり！？");

                        UpdateMainMessage("アイン：（・・・ッファイア、ファイア！）");

                        UpdateMainMessage("ラナ：その通り♪　魔法の詠唱部分だけが抜け落ちる沈黙効果よ♪");

                        UpdateMainMessage("アイン：（・・・ッファイア、ファイア、ファイア！）");

                        UpdateMainMessage("ラナ：ちょっとその辺でやめときなさいよ、すごく滑稽な姿よ、ッフフフ♪");

                        UpdateMainMessage("アイン：ちくしょう・・・これいつまで続くんだよ？");

                        UpdateMainMessage("ラナ：そんなに長くは無いけどある程度は続くわよ。");

                        UpdateMainMessage("アイン：いやいや、何ターンなんだよ！？");

                        UpdateMainMessage("ラナ：３ターンじゃなかったかしら。");

                        UpdateMainMessage("アイン：そうなのか・・・って、結構長いな。");

                        UpdateMainMessage("ラナ：まあ、しばらくは諦めなさい♪");

                        UpdateMainMessage("アイン：（・・・ファイア！）");

                        sc.VanishWave = true;
                        ShowActiveSkillSpell(sc, Database.VANISH_WAVE);
                        UpdateMainMessage("", true);
                    }
                    #endregion
                    #region "ダーケン・フィールド"
                    else if ((sc.Level >= 23) && (!sc.DarkenField))
                    {
                        UpdateMainMessage("ラナ：フィールド展開型の詠唱形態は特定のターゲット方向へ向けて放出するのではなく・・・");

                        UpdateMainMessage("アイン：相変わらず、その難しそうな辞典を読んでるんだな。");

                        UpdateMainMessage("ラナ：私の場合、バカアインと違って知識をまず押さえるトコから始めるのよ。");

                        UpdateMainMessage("アイン：いや、悪い意味じゃねえって。良くそういうトコから出してこれるよな。");

                        UpdateMainMessage("ラナ：うーん、だって理屈が無いとどうにもならないと思わない？");

                        UpdateMainMessage("アイン：まあ・・・最近はそう思える節も・・・。");

                        UpdateMainMessage("ラナ：ま、良いわ。とりあえずやってみるわね。");

                        UpdateMainMessage("ラナ：大地の恩恵を遮断せし闇、現れよダーケン・フィールド！");

                        UpdateMainMessage("　　　（アインの周辺一体が薄暗い闇が覆い始めた！）");

                        UpdateMainMessage("アイン：うわ・・・なんだこれ！？急に何か、ダウンする感じが・・・");

                        UpdateMainMessage("ラナ：そのフィールドにいる間は、物理防御と魔法防御がダウンする状態となるわ。");

                        UpdateMainMessage("アイン：マジかよ、本当面倒くせえのが多いな、複合魔法ってのは。");

                        UpdateMainMessage("アイン：じゃあ、・・・ダッシュでこのフィールドから！");

                        UpdateMainMessage("　　　（アインがダッシュすると、フィールド全体がアイン近辺を追従している！）");

                        UpdateMainMessage("アイン：何ぃ！？逃げられないのかよ！？");

                        UpdateMainMessage("ラナ：そうね、フィールド展開は特定のターゲットを示すわけじゃないんだけど");

                        UpdateMainMessage("ラナ：発生時からフィールド内に居たものは全てターゲットに出来るみたい。全体魔法ね。");

                        UpdateMainMessage("アイン：２人いたとして、２人別々に逃げたらどうなるんだよ？");

                        UpdateMainMessage("ラナ：２人分にちゃんと分割された状態でそのフィールドが追従するのよ。");

                        UpdateMainMessage("アイン：っな、なんてご都合主義な・・・");

                        UpdateMainMessage("ラナ：あ、そうそう。まだ試し撃ちしてなかったわね、本当に防御ダウンしてるのかしら♪");

                        UpdateMainMessage("アイン：っさて、俺はそろそろこの辺で・・・");

                        UpdateMainMessage("ラナ：無駄よ、もうさっきダミー素振り君をアインにセットしておいたから♪");

                        UpdateMainMessage("　　　（ダミー素振り君は【潜在奥義：集中と断絶】を放ってきた！）");

                        UpdateMainMessage("アイン：（・・・ッバタ）");

                        sc.DarkenField = true;
                        ShowActiveSkillSpell(sc, Database.DARKEN_FIELD);
                        UpdateMainMessage("", true);
                    }
                    #endregion
                    #region "フューチャー・ヴィジョン"
                    else if ((sc.Level >= 27) && (!sc.FutureVision))
                    {
                        UpdateMainMessage("ラナ：う～ん、面倒くさいわね、ホント・・・");

                        UpdateMainMessage("アイン：珍しいな、ラナでも面倒くさがるのか？");

                        UpdateMainMessage("ラナ：ダミー素振り君のセッティングよ。入力項目が多すぎて面倒なのよ。");

                        UpdateMainMessage("アイン：何だ、何か新しい技でも習得しようってのか？　何なら俺も手を貸すぜ。");

                        UpdateMainMessage("ラナ：そうなんだけど・・・");

                        UpdateMainMessage("ラナ：バカアインだと・・・");

                        UpdateMainMessage("アイン：何か否定的なためらい方だな。　俺じゃ助けにならないとでも？");

                        UpdateMainMessage("ラナ：う～ん、そういうわけじゃないわ。");

                        UpdateMainMessage("ラナ：じゃいいわ。ちょっとソコに立って。");

                        UpdateMainMessage("アイン：ダメージ系は避けるぞ、良いよな？");

                        UpdateMainMessage("ラナ：駄目よ。ダメージ系はちゃんと食らってよね。");

                        UpdateMainMessage("アイン：ぐぬぬ・・・");

                        UpdateMainMessage("ラナ：まあ今回はダメージじゃないわ。安心して♪");

                        UpdateMainMessage("アイン：ふう、そいつは助かるぜ。　で、どうだ？");

                        UpdateMainMessage("ラナ：ちょっと待っててね・・・ええと・・・");

                        UpdateMainMessage("　　　（ラナは大きく深呼吸を一度行った。）");

                        UpdateMainMessage("ラナ：うん、かかったわ。");

                        UpdateMainMessage("アイン：はい？");

                        UpdateMainMessage("ラナ：かかったって言ってるのよ。発動成功よ。");

                        UpdateMainMessage("アイン：何が？");

                        UpdateMainMessage("ラナ：後、効果持続時間もあるから、早くやってよね。");

                        UpdateMainMessage("アイン：だから、何がだよ？　全然わかんねえぞ。");

                        UpdateMainMessage("ラナ：何かインスタントで行動して。　きっと分かるから。");

                        UpdateMainMessage("アイン：っお、良いのかよ。　じゃあ・・・");

                        UpdateMainMessage("アイン：ファイア！");

                        UpdateMainMessage("アイン：っと！？　っげ！！！");

                        UpdateMainMessage("　　　（アインはたちまち詠唱体制を崩し、ファイア・ボール詠唱に失敗した。）");

                        UpdateMainMessage("アイン：わ、悪い悪い。ちょっと待ってくれ。もう１回発動させるから。");

                        UpdateMainMessage("ラナ：フフ、もう良いわよ。アリガト♪");

                        UpdateMainMessage("アイン：いやいや、単に失敗しただけだからさ。");

                        UpdateMainMessage("ラナ：そうじゃないのよ。トリガードイベント型スキルなのよ。");

                        UpdateMainMessage("アイン：っは？鳥が土居とベント？");

                        UpdateMainMessage("ラナ：ト・リ・ガ・－・ド・イ・ベ・ン・ト");

                        UpdateMainMessage("ラナ：アンタのインスタント行動を検知してそれをカウンターするスキルよ。");

                        UpdateMainMessage("ラナ：だから、今の詠唱失敗は私が仕掛けたトリガーが発動したってわけ。");

                        UpdateMainMessage("アイン：まあ分かったって。御託は良いからさ、もう１回発動させるから待ってろ。");

                        UpdateMainMessage("ラナ：もう～～・・・だからバカアインとはやりたくなかったのよ・・・");

                        UpdateMainMessage("ラナ：じゃ良いわ、もう１回ファイア・ボールお願い。");

                        UpdateMainMessage("アイン：っしゃ！任せろ！");

                        UpdateMainMessage("アイン：ファイア！");

                        UpdateMainMessage("　　　（ボシュウゥゥ）");

                        UpdateMainMessage("アイン：・・・で？");

                        UpdateMainMessage("ラナ：うん、稽古は終わりよ♪");

                        UpdateMainMessage("アイン：何かネタがよくわかんねえが・・・");

                        UpdateMainMessage("ラナ：ッホラホラ、じゃあまたね。今回はアリガト♪");

                        UpdateMainMessage("アイン：あ、ああぁ・・・");

                        sc.FutureVision = true;
                        ShowActiveSkillSpell(sc, Database.FUTURE_VISION);
                        UpdateMainMessage("", true);
                    }
                    #endregion
                    #region "リカバー"
                    else if ((sc.Level >= 28) && (!sc.Recover))
                    {
                        UpdateMainMessage("ラナ：（これは相手が必要よね・・・）");

                        UpdateMainMessage("アイン：っしゃ、じゃあ俺が相手になってやるぜ！");

                        UpdateMainMessage("ラナ：ッワ！アイン居たの？ビックリするじゃない。");

                        UpdateMainMessage("アイン：ラナが珍しく考え込んでたからな。気づかなかっただろ？");

                        UpdateMainMessage("ラナ：そうね、いつも気配ムンムンって感じだし・・・");

                        UpdateMainMessage("ラナ：あ、そうそう。良いかしら♪");

                        UpdateMainMessage("アイン：ああ、いつでもいいぜ！");

                        UpdateMainMessage("ラナ：じゃあ、まずはそのダミー素振り君の攻撃を食らってちょうだい♪");

                        UpdateMainMessage("アイン：マジかよ・・・じゃあ・・・");

                        UpdateMainMessage("　　　（ボグッシャアァァァ・・・（アインは気絶した））");

                        UpdateMainMessage("ラナ：気絶したみたいね・・・じゃあ、ッハイ！");

                        UpdateMainMessage("　　　（アインは気絶から回復した）");

                        UpdateMainMessage("アイン：ッツ、ツツツ・・・痛えな・・・");

                        UpdateMainMessage("アイン：おいおい、ちょっとダミー君の攻撃が強すぎねえか？素振りレベルじゃねえだろ、今のは");

                        UpdateMainMessage("ラナ：良いじゃない、バカアインだし大丈夫♪");

                        UpdateMainMessage("ラナ：じゃあ、もう１回♪");

                        UpdateMainMessage("　　　（ボグッシャアァァァ・・・（アインは気絶した））");

                        UpdateMainMessage("ラナ：気絶したみたいね・・・じゃあ、ッハイ！");

                        UpdateMainMessage("　　　（アインは気絶から回復した）");

                        UpdateMainMessage("アイン：ッ痛・・・お、おいおい待て待て、効果はなんとなくわかった！気絶解除のスキルだろ！？");

                        UpdateMainMessage("ラナ：じゃあ、もう１回♪");

                        UpdateMainMessage("アイン：待て、待て待て待て！！");

                        UpdateMainMessage("　　　（ボグッシャアァァァ・・・（アインは気絶した））");

                        sc.Recover = true;
                        ShowActiveSkillSpell(sc, Database.RECOVER);
                        UpdateMainMessage("", true);
                    }
                    #endregion
                    #region "トラスト・サイレンス"
                    else if ((sc.Level >= 29) && (!sc.TrustSilence))
                    {
                        UpdateMainMessage("ラナ：んっと、これはこうよね・・・で、それから・・・");

                        UpdateMainMessage("アイン：・・・（コッソリ）");

                        UpdateMainMessage("ラナ：ところで、バカアインは実験台にでもなってくれるのかしら♪");

                        UpdateMainMessage("アイン：うおぉ！なんで分かったんだよ！？");

                        UpdateMainMessage("ラナ：アンタその（コッソリ）感が出まくりでしょ。あんなの誰だって察知出来るわよ。");

                        UpdateMainMessage("アイン：（前のあの時は、上手く行ったのに・・・ナゼ・・・）");

                        UpdateMainMessage("ラナ：まあでも・・・今回のも少しややこしいのよね・・・");

                        UpdateMainMessage("アイン：大丈夫だ。キッチリ解説を付けてくれれば大丈夫！");

                        UpdateMainMessage("ラナ：アンタのそういうトコが余計ややこしいんじゃない・・・まあ良いわ。");

                        UpdateMainMessage("ラナ：今回のは先に説明しておくわね。");

                        UpdateMainMessage("ラナ：詠唱名「トラスト・サイレンス」");

                        UpdateMainMessage("ラナ：効果の対象は自分自身限定よ。");

                        UpdateMainMessage("ラナ：効果の内容は、対象へ沈黙/暗闇/誘惑に対する耐性（効果を防ぐ）を付与する。");

                        UpdateMainMessage("ラナ：効果をかけられた時にそれがかかるのではなく、かかった瞬間に効果が取り除かれる。");

                        UpdateMainMessage("ラナ：言ってみれば、予め準備しておいて、体制を整えておくような感じになるわね。");

                        UpdateMainMessage("ラナ：ちなみに、この効果は一度キリ。一旦沈黙や暗闇効果がかけられた場合、即座に解消となるわ。");

                        UpdateMainMessage("ラナ：重ねがけも出来ないみたいだから、過度な期待は出来ないかも知れないわね。");

                        UpdateMainMessage("ラナ：インスタントタイミングでも出来るからとっさにかけるのがベストなタイミングになりそうよ。");

                        UpdateMainMessage("ラナ：・・・って、ちょっと・・・そこ聞いてる？");

                        UpdateMainMessage("アイン：・・・　・・・");

                        UpdateMainMessage("アイン：・・・ッハ！！");

                        UpdateMainMessage("アイン：ッハッハッハッハ！　どうしたラナ！　もちろん、大体オッケー！！");

                        UpdateMainMessage("　　　『ッドゴォォォォン！！！』（ラナのサイクロイド・ブローがアインに炸裂）　　");

                        sc.TrustSilence = true;
                        ShowActiveSkillSpell(sc, Database.TRUST_SILENCE);
                        UpdateMainMessage("", true);
                    }
                    #endregion
                    #region "スカイ・シールド"
                    else if ((sc.Level >= 30) && (!sc.SkyShield))
                    {
                        UpdateMainMessage("ラナ：ッフフ、これが出来れば相当よね♪");

                        UpdateMainMessage("アイン：なんだずいぶんと楽しそうだな。");

                        UpdateMainMessage("ラナ：『聖』魔法との複合魔法の詠唱基礎をようやく習得できた所よ♪");

                        UpdateMainMessage("アイン：ッバ、バカな！可能なのかよ！？");

                        UpdateMainMessage("ラナ：可能ね、基礎さえ分かっていれば♪");

                        UpdateMainMessage("アイン：マジかよ・・・ッハハハ、さすがだぜ、ラナ。");

                        UpdateMainMessage("ラナ：うん、『聖』『水』による強力な複合魔法よ。良い？　見ててよね？");

                        UpdateMainMessage("アイン：あ、あぁ。お前さえ良ければ、いつまでも見ててやるぜ。");

                        UpdateMainMessage("　　　『ッシャゴオォォオォォ！！！』（ラナのライトニングキックがアインに炸裂）　　");

                        UpdateMainMessage("アイン：な・・・ナゼ・・・褒めたつもりが・・・（ッグホ）");

                        UpdateMainMessage("ラナ：何かイライラさせる言い方だったからよ。　少しは考えて言葉を選びさいよ。");

                        UpdateMainMessage("ラナ：まあ良いわ、見てて。");

                        UpdateMainMessage("　　　（ラナは歌に似たような感じで、綺麗な詠唱をし始めた・・・）");

                        UpdateMainMessage("アイン：（・・・へぇ・・・あのラナがね・・・）");

                        UpdateMainMessage("ラナ：紺碧の天空より加護を授かる、スカイ・シールド！");

                        UpdateMainMessage("　　　（ッパキィィン・・・）");

                        UpdateMainMessage("ラナ：まずまずの出来栄えね♪");

                        UpdateMainMessage("アイン：っお、何か防御壁みたいなのが出来たな。");

                        UpdateMainMessage("ラナ：アインのバカファイアでも撃ってみて♪");

                        UpdateMainMessage("アイン：バカファイアではない、ファイアボールだ。　じゃあ行くぜ！");

                        UpdateMainMessage("アイン：ファイア！");

                        UpdateMainMessage("　　　（ッパシイィィ！！）");

                        UpdateMainMessage("ラナ：ッフフフ、無傷よ♪");

                        UpdateMainMessage("アイン：何ぃ！？マジかよそれ！？");

                        UpdateMainMessage("ラナ：しかも、これはね。重ねがけが可能なの。見てて♪");

                        UpdateMainMessage("　　　（ラナは３回スカイ・シールドを詠唱した）");

                        UpdateMainMessage("ラナ：合計３回まで魔法ダメージを無効化出来るのよ♪");

                        UpdateMainMessage("アイン：オイオイオイ、反則じゃねえのかよ？");

                        UpdateMainMessage("ラナ：戦闘では３回詠唱するタイミングは無いかも知れないし、そこまで反則じゃないわよ。");

                        UpdateMainMessage("アイン：まあ、そうかも知れねえけどさ・・・無効化って相変わらず嫌な感じだよな。");

                        UpdateMainMessage("アイン：ところで、どんな魔法ダメージでも対象になるのか？");

                        UpdateMainMessage("ラナ：そうね、属性区別とかは特に無いわ。");

                        UpdateMainMessage("アイン：フレイム・オーラみたいな追加効果の魔法ダメージは？");

                        UpdateMainMessage("ラナ：良い所に気づくわね、そういうのも対象よ。");

                        UpdateMainMessage("アイン：ラナお得意のブルー・バレットは３回って事になるのか？");

                        UpdateMainMessage("ラナ：そうね、ブルーバレットが来た場合、３回分消費してしまう事になるわ。");

                        UpdateMainMessage("アイン：なるほど、解説いつもサンキューな。");

                        UpdateMainMessage("ラナ：ヤケに素直よね、何か気味が悪いわよ。悪いものでも食べたの？");

                        UpdateMainMessage("アイン：いや、無効化系のヤツだからな。入念に知っておきたかったのさ。");

                        UpdateMainMessage("ラナ：ふ～ん、そうなんだ。まあ、後は戦術次第って感じね。");

                        UpdateMainMessage("ラナ：ちなみに、バカアインには絶対かけないから♪");

                        UpdateMainMessage("アイン：マジかよ・・・ピンチな時は頼むぜ、ホント。");

                        UpdateMainMessage("ラナ：考えておいてあげる♪");

                        sc.SkyShield = true;
                        ShowActiveSkillSpell(sc, Database.SKY_SHIELD);
                        UpdateMainMessage("", true);
                    }
                    #endregion
                    #region "スター・ライトニング"
                    else if ((sc.Level >= 31) && (!sc.StarLightning))
                    {
                        UpdateMainMessage("アイン：ラナ、たまには飯でも食べに行くか？");

                        UpdateMainMessage("ラナ：うーん、行きたい所なんだけど、この魔法だけやっておきたいから。");

                        UpdateMainMessage("アイン：おお、やるじゃねえか。っしゃ、トコトン付き合うぜ！");

                        UpdateMainMessage("ラナ：あ・・・じゃあ、終わってからでもいいなら、食べに行きましょ♪");

                        UpdateMainMessage("アイン：いやいや、そんな飯ぐらいどうでもいいって、っさ！やろうぜ！");

                        UpdateMainMessage("ラナ：え、終わってから行くって言ってるじゃない！？　ソッチから誘ったくせに。");

                        UpdateMainMessage("アイン：い、いやいや。じゃあ終わってから行くか！　っな！");

                        UpdateMainMessage("ラナ：もーイイわよ、面倒くさいし次で良いんじゃないかしら。");

                        UpdateMainMessage("アイン：いやいやいや・・・");

                        UpdateMainMessage("ラナ：ああぁ！イライラしてきたわ！！　行くわよ！！！");

                        UpdateMainMessage("アイン：（ヒエェェ・・・）");

                        UpdateMainMessage("ラナ：雷鳴鬼神、天空から大地へ無限なる雷光をそこのバカに放たん！！　スターライトニング！！！");

                        UpdateMainMessage("ラナ：喰らいなさい！！　永遠に気絶してるが良いわ！！！");

                        UpdateMainMessage("　　　（ッバババババリバリバリバリ！！！（スーパークリティカルヒットが炸裂）");

                        UpdateMainMessage("　　　（アインは倒れる瞬間、【話の流れには気を配ろう】と心底から誓った。）");

                        sc.StarLightning = true;
                        ShowActiveSkillSpell(sc, Database.STAR_LIGHTNING);
                        UpdateMainMessage("", true);
                    }
                    #endregion
                    #region "サイキック・トランス"
                    else if ((sc.Level >= 32) && (!sc.PsychicTrance))
                    {
                        UpdateMainMessage("ラナ：っふぅ・・・これは本当に出来ないわね・・・");

                        UpdateMainMessage("アイン：珍しいな、ラナが習得に苦戦するなんて。");

                        UpdateMainMessage("ラナ：アインはどうなの？逆属性に関しては。");

                        UpdateMainMessage("アイン：そうだな、俺も正直得意げにお披露目というわけにはいかねえ。");

                        UpdateMainMessage("アイン：カール爵に講義はしてもらってたんだが、あれだけがどうしても今ひとつだな。");

                        UpdateMainMessage("ラナ：ちょっと待ってよ！");

                        UpdateMainMessage("アイン：ん？何だよ。");

                        UpdateMainMessage("アイン：・・・ッハ！　俺、今なんて言った！？");

                        UpdateMainMessage("ラナ：カール爵ってどういう事よ、まさかシニキア・カールハンツ公爵じゃないでしょうね！？");

                        UpdateMainMessage("アイン：・・・ハイ、その通りですが・・・");

                        UpdateMainMessage("ラナ：・・・本当に？");

                        UpdateMainMessage("アイン：な、何だよ。その不可思議なオーラは・・・ちょっと待った、殴るなよ？");

                        UpdateMainMessage("ラナ：本当かどうかを聞いてるだけよ、本当なの？");

                        UpdateMainMessage("アイン：あ、ああ・・・本当だ。あの威圧感、間違いなく本物だ。");

                        UpdateMainMessage("ラナ：ウソでしょ・・・何でバカアインがそんな所に通ってるのよ？");

                        UpdateMainMessage("アイン：そりゃ、色々とワケがあってだな。");

                        if (mc.Level >= 30)
                        {
                            UpdateMainMessage("アイン：（待てよ、そういや一度連れて来いって言われてたな・・・）");
                        }

                        UpdateMainMessage("アイン：なんなら、ラナも一緒に来てみるか？");

                        UpdateMainMessage("ラナ：ええ、出来る事ならお願いしたいぐらいよ。");

                        UpdateMainMessage("アイン：よし、分かった。じゃあこっちへ来てくれ。");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "アインとラナは転送装置でカール爵の元へと向かった。";
                            md.ShowDialog();
                        }

                        this.buttonHanna.Visible = false;
                        this.buttonDungeon.Visible = false;
                        this.buttonRana.Visible = false;
                        this.buttonGanz.Visible = false;
                        this.buttonPotion.Visible = false;
                        this.buttonDuel.Visible = false;
                        this.buttonShinikia.Visible = false;
                        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_SECRETFIELD_OF_FAZIL);

                        UpdateMainMessage("アイン：おし、着いたぜ。");

                        UpdateMainMessage("ラナ：え？　本当にこんな所に居るわけ？");

                        UpdateMainMessage("カール：貴女はラナ・アミリアか？");

                        UpdateMainMessage("ラナ：カールハンツ公爵、本当だわ！　お初にお目にかかります。");

                        UpdateMainMessage("アイン：何だラナ、ヤケに仰々しいな。");

                        UpdateMainMessage("ラナ：（っちょっと、アンタどれだけバカ過ぎるのよ、控えてよね、ホンット）");

                        UpdateMainMessage("アイン：いやいや、ていうか威圧感があり過ぎて、そんな控える暇なんか無いんだってマジで。");

                        UpdateMainMessage("カール：貴女、失礼だが、少々拝見させてもらう。");

                        UpdateMainMessage("　　　『擬眼がギョロリと動きはじめた！』");

                        UpdateMainMessage("ラナ：至極光栄の極み、心して拝受仕ります。");

                        UpdateMainMessage("カール：・・・　・・・　・・・");

                        UpdateMainMessage("カール：スペル属性『闇  水  空』　典型的女性像");

                        UpdateMainMessage("アイン：典型的！？　どこが！？");

                        UpdateMainMessage("ラナ：（良いから黙ってなさいよ・・・ホンットもう・・・）");

                        UpdateMainMessage("カール：スキル属性『静  柔  無心』　理路整然派");

                        UpdateMainMessage("カール：構えて攻撃するスタイルとは違い、敵の急所にターゲットを絞るのに専念。");

                        UpdateMainMessage("カール：駆け引きや心理戦には持ち込まず、あくまで実践・実利を重視する傾向。");

                        UpdateMainMessage("カール：魔法攻撃もしくは体術のダメージを主軸とし、前衛・後衛のいずれも可能。");

                        UpdateMainMessage("カール：特定の波や流れに依存せず、キッチリとした理論で相手を仕留める。");

                        UpdateMainMessage("カール：なるほど、確かにそこの貴君の相棒として、ふさわしいタイプではある。");

                        UpdateMainMessage("　　　『擬眼が更にギョロリと動いた！』");

                        UpdateMainMessage("カール：ただし、瞬間的局面において、最適戦術の選択に乏しい。");

                        UpdateMainMessage("アイン：！！");

                        UpdateMainMessage("カール：パーティ連携や１人技巧はそこそこ出来るものの、柔軟な取り合わせではなくパターン動作に陥る傾向。");

                        UpdateMainMessage("カール：普段出来るはずの行動が、その局面にして発動せず、致命傷に至るケースが存在。");

                        UpdateMainMessage("カール：しかし、それほどの局面はそれほどなかろう。側に居るものがカバーしていれば尚更の事。");

                        UpdateMainMessage("アイン：（・・・冗談だろ・・・当たりすぎだ・・・何だこの人・・・）");

                        UpdateMainMessage("カール：ふむ、その事に対しては、既に鍛錬する日々を積んでいるようだな。");

                        UpdateMainMessage("カール：良き心構えだ。その鍛錬、忘れずに続けると良い。");

                        UpdateMainMessage("ラナ：至らぬ我が身に対し、最善なる深慮。謹んで弛まぬ努力をさせていただきます。");

                        UpdateMainMessage("アイン：えっと・・・　・・・おい、ラナ。どうなってんだこりゃ。");

                        UpdateMainMessage("ラナ：シニキア・カールハンツ公爵は、聖フローラ女学院の独立執行機関の長よ。");

                        UpdateMainMessage("ラナ：魔法の使用に関しては一般的な公の場において、今でこそ認められてるけど");

                        UpdateMainMessage("ラナ：本来の使い方から誤った手法で、人民に被害を加えたりする輩が居た時代もあるのよ。");

                        UpdateMainMessage("ラナ：でも、そういった彼らは現在ここにはいない。なぜだか分かる？");

                        UpdateMainMessage("アイン：マジか・・・そんな凄え人なのかよ。伝説のFiveSeekerだけじゃなかったのか。");

                        UpdateMainMessage("カール：そのような話はどうでも良い事。忘れなさい。");

                        UpdateMainMessage("アイン：失礼いたしました。え、ええっと・・・では、カ、カ、カールハン・・・");

                        UpdateMainMessage("カール：今まで通りで良い。それから貴女も、もっと自然体で接していただこう。");

                        UpdateMainMessage("ラナ：・・・ハイ。");

                        UpdateMainMessage("カール：要件はさしずめ、逆属性複合の習得といった所か。");

                        UpdateMainMessage("ラナ：ハイ。");

                        UpdateMainMessage("アイン：おいおい、マジかよ・・・どんだけお見通しなんだよ。");

                        UpdateMainMessage("カール：貴女は理論の矛盾の故、見出せないようだな。それはそれで正しい。");

                        UpdateMainMessage("カール：ただ、この逆属性複合の場合に限り【理外の理】をイメージしなければならない。");

                        UpdateMainMessage("カール：精神の限界とは精神そのものに原因がある。それをイメージしてそれを超えるようにしなさい。");

                        UpdateMainMessage("カール：一度だけ見せよう。　見届けなさい。");

                        UpdateMainMessage("カール：我が精神へ永遠なる飛躍をもたらせ、【サイキック・トランス】");

                        UpdateMainMessage("　　　（カールは目にも止まらぬ速さで詠唱を行った！）");

                        UpdateMainMessage("アイン：（っな！　は、早ええぇぇ！！！）");

                        UpdateMainMessage("カール：完成だ。これで我が魔法攻撃力は更に増強された事となる。");

                        UpdateMainMessage("カール：ただし代償として魔法防御力は格段に落ちる。使い所が肝心なので覚えておきなさい。");

                        UpdateMainMessage("ラナ：ハイ、ありがとうございました。");

                        UpdateMainMessage("アイン：えっと・・・今の早すぎて全然見えなかったんだが・・・");

                        UpdateMainMessage("カール：速さは重要ではない。イメージがもたらした原理を把握すると良い。");

                        UpdateMainMessage("アイン：あ、はい。どうもです。");

                        UpdateMainMessage("カール：他に聞きたい事はあるかね。");

                        UpdateMainMessage("アイン：えっ、じゃあ良いですか。あのさっきの詠唱タイミングのトコですけど");

                        UpdateMainMessage("ラナ：（ちょっと！　バカアイン、本当に無礼よねアンタ）");

                        UpdateMainMessage("カール：よい、続けなさい。");

                        UpdateMainMessage("アイン：一瞬だけ両手の合わせ方向が逆に重ね合わせてたように見えたんですけど？");

                        UpdateMainMessage("カール：逆方向と順方向をすり合わせる方向性を示したモノ、それ容として融合させたもの。");

                        UpdateMainMessage("カール：貴君にもいずれ教えよう。");

                        UpdateMainMessage("アイン：あ、ありがとうござます！");

                        UpdateMainMessage("カール：転送装置を復活させておいた。早々に戻ると良い。");

                        UpdateMainMessage("ラナ：この度、ありがとうございました。また、お教えください。");

                        sc.PsychicTrance = true;
                        ShowActiveSkillSpell(sc, Database.PSYCHIC_TRANCE);

                        if (!we.AlreadyRest)
                        {
                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_EVENING);
                        }
                        else
                        {
                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                        }
                        this.buttonHanna.Visible = true;
                        this.buttonDungeon.Visible = true;
                        this.buttonRana.Visible = true;
                        this.buttonGanz.Visible = true;
                        this.buttonPotion.Visible = true;
                        this.buttonDuel.Visible = true;
                        this.buttonShinikia.Visible = true;

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "アインとラナは転送装置で街へ戻ってきた。";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("アイン：いや、しかし・・・あの速度はスゲェ・・・");

                        UpdateMainMessage("ラナ：何言ってるのよ、あれで私やアンタに見える形にしてくれてたんじゃない。");

                        UpdateMainMessage("アイン：ボケ師匠の場合もそうだけどさ。FiveSeekerって何か段違いのレベルだよな。");

                        UpdateMainMessage("アイン：あの妙な威圧感とかも特別だが、基本的な全体能力が別次元すぎる。");

                        UpdateMainMessage("アイン：FiveSeeker内で速度に特化する技系は確かヴェルゼ・アーティのはずだ。");

                        UpdateMainMessage("ラナ：「姿捕らえたもの存在せず」だから、たぶんそういう事になるわね。");

                        UpdateMainMessage("アイン：だとすると、カール爵の手抜き動作でさえ、あのレベルって事は・・・");

                        UpdateMainMessage("アイン：正直ソッチの方を考えると、堪えるよな。");

                        UpdateMainMessage("ラナ：私達なんて、まだまだこれからなんだし、そんなに焦る事ないわよ。");

                        UpdateMainMessage("アイン：まあ頑張り次第なんだろうけど。");

                        UpdateMainMessage("ラナ：ッフフ、さっき公爵が見せてくれた手順の中の一つが見えたんでしょ？");

                        UpdateMainMessage("アイン：あ、ああ・・・なんとなくな。");

                        UpdateMainMessage("ラナ：１回で見切れてたら凄いわよ、その感覚を伸ばすしかないんじゃないかしら♪");

                        UpdateMainMessage("アイン：そうだな、サンキュー。");

                        UpdateMainMessage("ラナ：私が習得しに行ったのに、何で私がアンタを励ましてんのよ。");

                        UpdateMainMessage("アイン：おお、そうだったな悪い悪い。で、出来そうなのか？");

                        UpdateMainMessage("ラナ：ええ、一度教えてもらったし、たぶん出来るようになってるわ。アリガト♪");

                        UpdateMainMessage("アイン：そか、そいつは良かった！　今後も頼むぜ、ラナ様！");

                        UpdateMainMessage("ラナ：ええ、バカアインがピンチな時に魔法防御を落としてあげるから♪");

                        UpdateMainMessage("アイン：ッハッハッハ！　そのぐらいでちょうど良い、頼んだぜ！");

                        UpdateMainMessage("ラナ：バカはバカね・・・ハアァァァ・・・");
                    }
                    #endregion
                    #region "サイキック・ウェイブ"
                    else if ((sc.Level >= 33) && (!sc.PsychicWave))
                    {
                        UpdateMainMessage("ラナ：ふう・・・難しいわね、こういうのは・・・");

                        UpdateMainMessage("アイン：よお、ラナ。どうした苦戦してるみたいじゃねえか。");

                        UpdateMainMessage("ラナ：どうもこうも無いわよ、体術としてのイメージが凄く掴みにくいわ。");

                        UpdateMainMessage("ラナ：『柔』と『心眼』の合わせ技なんだけど、『心眼』は私にとって逆性質、だから余計大変なのよ。");

                        UpdateMainMessage("アイン：どういう内容なんだ？");

                        UpdateMainMessage("ラナ：えっとね・・・なんて言うのかしら・・・");

                        UpdateMainMessage("ラナ：魔法詠唱するのよ。体術を使ってね。");

                        UpdateMainMessage("アイン：・・・マジで？");

                        UpdateMainMessage("ラナ：普通、体術を使う時はある特定のモーションからのクダリが必要でしょ？");

                        UpdateMainMessage("ラナ：でも、この体術だけは特別。魔法詠唱のモーションから入るの。");

                        UpdateMainMessage("ラナ：体術による魔法の創出、ダメージ発生源は【力】ではなく【知】って事になるわ。");

                        UpdateMainMessage("アイン：・・・へえ・・・なるほど・・・");

                        UpdateMainMessage("ラナ：ッホラ、こうやって・・・ここから・・・");

                        UpdateMainMessage("ラナ：行くわよ、サイキック・ウェイブ！");

                        UpdateMainMessage("　　　（ッヴァシュウゥゥゥン！）");

                        UpdateMainMessage("アイン：っぬお！防御！");

                        UpdateMainMessage("　　　（しかし、アインが防御体制を取ったにも関わらず、アインに激痛が走った！）");

                        UpdateMainMessage("アイン：っグア！痛っつつ・・・防御不可かよ。チクショウ。");

                        UpdateMainMessage("ラナ：ふう、一応出来たわね。でも、これ凄く難しいのよ。");

                        UpdateMainMessage("アイン：何かコレ・・・ワード・オブ・パワーみてえだな。");

                        UpdateMainMessage("アイン：あれのちょうど反対と言うか・・・双子みたいなもんだな。");

                        UpdateMainMessage("ラナ：よくそういうトコにすぐ気づくわね。センスだけは一人前って事かしら。");

                        UpdateMainMessage("アイン：天才アイン様だからな。ッハッハッハ！");

                        UpdateMainMessage("ラナ：じゃあ、問題よ。このスキルはミラー・イメージとデフレクションのどちらを貫通するかしら♪");

                        using (SelectAction sa = new SelectAction())
                        {
                            sa.StartPosition = FormStartPosition.CenterParent;
                            sa.ForceChangeWidth = 300;
                            sa.ElementA = "ミラー・イメージだろ！";
                            sa.ElementB = "当然、デフレクションだな！";
                            sa.ElementC = "実は、ワン・イムーニティ！";
                            sa.ShowDialog();
                            if (sa.TargetNum == 1)
                            {
                                UpdateMainMessage("アイン：当然、デフレクションだな！");

                                UpdateMainMessage("ラナ：そう・・・正解ね・・・");

                                UpdateMainMessage("アイン：お・・・おいおい、何だよ？そのテンションの下がり具合は。");

                                UpdateMainMessage("ラナ：ん、何でもないわよ♪　まあ、バカなりに考えたって事ね。");

                                UpdateMainMessage("アイン：俺だってヤマを張って、偶然当てる事もある。");

                                UpdateMainMessage("ラナ：やっぱりテキトーだったの！？　ちゃんと考えて答えなさいよね！？");

                                UpdateMainMessage("アイン：わわ、分かったって・・・次からちゃんと考えます・・・");
                            }
                            else
                            {
                                UpdateMainMessage("ラナ：ハ・ズ・レ♪　やっぱり、バカアインはバカで決定♪");

                                UpdateMainMessage("アイン：ぐぬぬぬ・・・");
                            }
                        }

                        sc.PsychicWave = true;
                        ShowActiveSkillSpell(sc, Database.PSYCHIC_WAVE);
                        UpdateMainMessage("", true);
                    }
                    #endregion
                    #region "シャープ・グレア"
                    else if ((sc.Level >= 34) && (!sc.SharpGlare))
                    {
                        UpdateMainMessage("ラナ：・・・を狙うが如く・・・");

                        UpdateMainMessage("アイン：（ットト・・・ラナだ。何かブツブツ言ってるな・・・）");

                        UpdateMainMessage("ラナ：ミゾオチを狙うが如く、敵の懐に入り込み、一気に突き上げる。");

                        UpdateMainMessage("ラナ：対象者へ一定のダメージを与え、かつ、一定期間の沈黙を付与する。");

                        UpdateMainMessage("ラナ：なんだ、『心眼』絡みだから難しいかと思ったら、普段バカアインにやってるヤツじゃない♪");

                        UpdateMainMessage("アイン：（ッソォット・・・今回だけはサラバ・・・）");

                        UpdateMainMessage("ラナ：バカアイン、ハ・ッ・ケ・ン♪");

                        UpdateMainMessage("アイン：ックソ、見つかった！　ホーリー・ブレイカー！");

                        UpdateMainMessage("ラナ：ダメージ反転しても、効果は適用されるわよ。残念ね♪");
                        
                        UpdateMainMessage("ラナ：じゃあ行くわよ、ッセイ！！");

                        UpdateMainMessage("　　　『ッズッドォォン！！』（ラナのシャープ・グレアがアインに炸裂）　　");

                        UpdateMainMessage("ラナ：決まったわ♪　じゃあ、さっそく魔法詠唱してみて♪");

                        UpdateMainMessage("アイン：無理です・・・勘弁してください・・・");

                        sc.SharpGlare = true;
                        ShowActiveSkillSpell(sc, Database.SHARP_GLARE);
                        UpdateMainMessage("", true);
                    }
                    #endregion
                    #region "スタンス・オブ・サッドネス"
                    else if ((sc.Level >= 35) && (!sc.StanceOfSuddenness))
                    {
                        UpdateMainMessage("ラナ：フゥ・・・疲れるわね。");

                        UpdateMainMessage("アイン：ラナ、あまりキリキリに詰めての練習は良くないぞ。");

                        UpdateMainMessage("ラナ：でも、まだ１回も成功してないから、出来るまでやってみるわ。");

                        UpdateMainMessage("アイン：どんな内容なんだよ？");

                        UpdateMainMessage("ラナ：『スタンス・オブ・サッドネス』、これも完全逆性質の類ね。");

                        UpdateMainMessage("ラナ：『心眼』と『無心』のコンビネーションだから、難しいのよホント。");

                        UpdateMainMessage("アイン：効果は？");

                        UpdateMainMessage("ラナ：対象のインスタント行動をキャンセルさせる事が可能になるわ。");

                        UpdateMainMessage("ラナ：さらに、本インスタントに対して、追加でスタックは乗せられないって所よ。");

                        UpdateMainMessage("アイン：マジで！？　何だその問答無用なアンチ活動は！？");

                        UpdateMainMessage("ラナ：アンチ活動って語句がオカシイわよ・・・それは良いとして");

                        UpdateMainMessage("ラナ：タイミングの掴み方が何か分からないのよね・・・");

                        UpdateMainMessage("アイン：そこでやはり、俺の協力が必要ってワケだな、任せろ。");

                        UpdateMainMessage("ラナ：じゃあ、一つお願い出来るかしら。");

                        UpdateMainMessage("アイン：っしゃ、来た！　任せておけ！！");

                        UpdateMainMessage("ラナ：自分で自分にファイア・ボールを撃ち込んでみて。");

                        UpdateMainMessage("アイン：ッブフゥ！");

                        UpdateMainMessage("アイン：ラナに対して撃ったら駄目なのかよ！？");

                        UpdateMainMessage("ラナ：嫌よそんなの、許せるはずないじゃない。");

                        UpdateMainMessage("アイン：じゃ、じゃあフレッシュ・ヒールをお前に対して詠唱しよう、どうだ？");

                        UpdateMainMessage("ラナ：何でライフ満タンの私にヒールかける必要があるのよ。止める必要も無いじゃない。");

                        UpdateMainMessage("アイン：いや・・・とにかく、自爆ファイアは勘弁だ。");

                        UpdateMainMessage("ラナ：う～ん、どうしようかしら・・・");

                        UpdateMainMessage("アイン：そうだ、またカール爵の所にでも行くか？");

                        UpdateMainMessage("ラナ：良いの？何度も行ったら失礼に当たらないかしら。");

                        UpdateMainMessage("アイン：大丈夫だって、ああ見えて寛大な人だ。");

                        UpdateMainMessage("ラナ：アンタのそういう言い方、ものすごーく失礼だから気をつけてよね・・・ホント。");

                        UpdateMainMessage("アイン：あっと、悪い悪い。じゃ早速行こうぜ！");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "アインとラナは転送装置でカール爵の元へと向かった。";
                            md.ShowDialog();
                        }

                        this.buttonHanna.Visible = false;
                        this.buttonDungeon.Visible = false;
                        this.buttonRana.Visible = false;
                        this.buttonGanz.Visible = false;
                        this.buttonPotion.Visible = false;
                        this.buttonDuel.Visible = false;
                        this.buttonShinikia.Visible = false;
                        ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_SECRETFIELD_OF_FAZIL);

                        UpdateMainMessage("アイン：おし、着いたぜ。");

                        UpdateMainMessage("ラナ：っとと・・・この転送装置、何か慣れないわね。");

                        UpdateMainMessage("カール：その転送装置は本来、貴女のために用意されたものではない。");

                        UpdateMainMessage("アイン：え、どういう意味ですか？");

                        UpdateMainMessage("カール：転送装置は個々の性質に順応したモノに調整が必要だ。");

                        UpdateMainMessage("カール：不適合であり、資質が低いモノが使えば、精神に支障をきたす。");

                        UpdateMainMessage("アイン：ッゲゲ・・・");

                        UpdateMainMessage("カール：案ずるな、貴女が来る事を想定し、我が既に調整しておいた。");

                        UpdateMainMessage("カール：多少のフラつきはあるだろうが、気に留める程ではない。安心して使うと良い。");

                        UpdateMainMessage("ラナ：数多き思慮、控えを持って、以後謹むように致します。");

                        UpdateMainMessage("カール：良い。以前にも言ったが自然体で接するようにせよ。");

                        UpdateMainMessage("ラナ：・・・ハイ。");

                        UpdateMainMessage("カール：逆性質の『心眼』と『無心』による複合スキル『スタンス・オブ・サッドネス』に関してだが");

                        UpdateMainMessage("ラナ：ハイ。");

                        UpdateMainMessage("アイン：っちょ待ってくれよ。だから、カール先生は何で要件が事前に分かってるんだよ？");

                        UpdateMainMessage("ラナ：（っちょっと！　控えてよね、バカアイン！）");

                        UpdateMainMessage("アイン：（い、いいじゃねえか別に。気になるんだし・・・）");

                        UpdateMainMessage("カール：良い、少しだけ教えよう。");

                        UpdateMainMessage("カール：我の見切りでは、貴女ラナ・アミリアは完全逆属性または逆性質の扱いに苦しんでおる。");

                        UpdateMainMessage("アイン：それって今来て分かったって事ですよね？");

                        UpdateMainMessage("カール：貴君が初めに連れて来た時からである。");

                        UpdateMainMessage("アイン：ウソ、マジか・・・");

                        UpdateMainMessage("カール：論理展開力のある女性にとって、この概念は非常に苦痛となるであろう。");

                        UpdateMainMessage("アイン：ラナ、お前確かサイキック・トランス出来るようになってたよな？");

                        UpdateMainMessage("ラナ：ん、あれは出来るようになったわね♪");

                        UpdateMainMessage("アイン：さすがラナだぜ、じゃあこのスキルの方も同じだろ？");

                        UpdateMainMessage("カール：控えよ、アイン・ウォーレンス。");

                        UpdateMainMessage("　　【【【　アインは突然、背筋に異常な威圧感を感じた！　】】】");

                        UpdateMainMessage("アイン：・・・はい。");

                        UpdateMainMessage("カール：ラナ・アミリアはお主とは性質が違う。");

                        UpdateMainMessage("カール：アイン・ウォーレンス、貴君は先に帰るがよい。");

                        UpdateMainMessage("アイン：わっ・・・わかりました。");

                        UpdateMainMessage("カール：非礼に対する意味ではない。場のケースを我がコントロールするため、悪く思うな。");

                        UpdateMainMessage("アイン：ああ、いえいえ。本当にスイマセンでした、今回はこれで失礼します。");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "アインは転送装置で街へと戻っていった。";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("カール：では、貴女ラナ・アミリアに問おう。");

                        UpdateMainMessage("ラナ：ハイ。");

                        UpdateMainMessage("カール：あえて意図して『心眼』を選択した根拠を述べたまえ。");

                        UpdateMainMessage("カール：『心眼』と『無心』の選択は、他の『静』『動』、または『柔』『剛』と比べても極めて困難。");

                        UpdateMainMessage("ラナ：昔から、アインよりも私の方が優れていたんです。");

                        UpdateMainMessage("ラナ：でも今は、そうじゃない。");
                        
                        UpdateMainMessage("ラナ：私、分かってるです、資質はアインの方が高いこと。");

                        UpdateMainMessage("ラナ：でも、こんな所で置いてけぼりにはなれない。私の方が難易度の高いのをやってのけなければ");

                        UpdateMainMessage("ラナ：でなければ、アインが私に対して、今以上に手加減をしてしまう。");

                        UpdateMainMessage("ラナ：それが嫌なんです。まるで私が単なる足枷みたいな感じがして・・・");

                        UpdateMainMessage("カール：貴女の場合、未だサイキック・トランスの詠唱は苦しいと見受けられる、間違いはないな？");

                        UpdateMainMessage("ラナ：ハイ。");

                        UpdateMainMessage("カール：『心眼』と『無心』への挑戦は、貴女の精神崩壊にも直結する内容である。");

                        UpdateMainMessage("カール：ゆえに、辛いと感じた場合、必ずあきらめの心得を持ち、時間を置くようにする事だ。");

                        UpdateMainMessage("ラナ：ハイ。");

                        UpdateMainMessage("カール：貴女に不足しているのは、純粋な休息の時間。");

                        UpdateMainMessage("カール：今までの鍛錬、申し分無く賞賛される内容ではある。");

                        UpdateMainMessage("カール：ただし、それだけでは貴女の身が危険であることにも変わりはない。");

                        UpdateMainMessage("カール：知識とは即座に習得されるものではない。");

                        UpdateMainMessage("カール：一刻の集中的な導入から、幾ばくかの流れ行く時間が必要となる。");

                        UpdateMainMessage("ラナ：何か・・・苦しくなってしまって・・・");

                        UpdateMainMessage("ラナ：私・・・アインに追いつけそうになかったし・・・");

                        UpdateMainMessage("ラナ：す、すみません、何かこんな所で私・・・");

                        UpdateMainMessage("　　（　ラナはその場で顔を伏せ、わずかな水滴を地面に落とした ）");

                        UpdateMainMessage("カール：今までの無理な鍛錬とその成果、相当の苦労を重ねて来た事であろう。");

                        UpdateMainMessage("カール：だが、貴女が死と向き合うレベルの鍛錬は少々早すぎる。");

                        UpdateMainMessage("カール：日々のスタイルを崩すこと無く、鍛錬するが良い。");

                        UpdateMainMessage("ラナ：すみません、こんな所で泣いてしまって・・・");

                        UpdateMainMessage("カール：よい。気に病む必要はない。");

                        UpdateMainMessage("カール：アイン・ウォーレンスは逸材。近くでその成長を見届けるぐらいの心構えとせよ。");

                        UpdateMainMessage("カール：同等や肩並べのつもりで励まぬように。");

                        UpdateMainMessage("ラナ：ハイ、わかりました。");

                        UpdateMainMessage("カール：では、『心眼』と『無心』による『スタンス・オブ・サッドネス』、教えて進ぜよう。");

                        UpdateMainMessage("ラナ：ハイ、お願いします。");

                        UpdateMainMessage("　　　『ラナは集中して講義の内容を聞いた！』");

                        UpdateMainMessage("カール：これで講義は終わりである。");

                        UpdateMainMessage("ラナ：ありがとうございました。");

                        UpdateMainMessage("カール：貴女もよくぞここまで辿り着いた。その類稀なる努力を賞賛と敬意を評し、");

                        UpdateMainMessage("カール：一度『スタンス・オブ・サッドネス』の極意、見せるとしよう。");

                        UpdateMainMessage("ラナ：っえ、本当ですか！？");

                        UpdateMainMessage("カール：構えたまえ。");

                        UpdateMainMessage("ラナ：アッ、ハイ！ではお願いいたします！");

                        UpdateMainMessage("カール：ゆくぞ。");

                        UpdateMainMessage("ラナ：放て氷の弾丸、ブルー・バレット！");

                        if (!we.AlreadyRest)
                        {
                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_EVENING);
                        }
                        else
                        {
                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
                        }
                        this.buttonHanna.Visible = true;
                        this.buttonDungeon.Visible = true;
                        this.buttonRana.Visible = true;
                        this.buttonGanz.Visible = true;
                        this.buttonPotion.Visible = true;
                        this.buttonDuel.Visible = true;
                        this.buttonShinikia.Visible = true;

                        UpdateMainMessage("アイン：・・・おせえなあ・・・");

                        UpdateMainMessage("　　　（ッバシュ！）");

                        UpdateMainMessage("ラナ：ふう、ただいま。");

                        UpdateMainMessage("アイン：おおぉ、やっと帰ってきたか！　どうだったよ？");

                        UpdateMainMessage("ラナ：ッフフフ、一回だけ、私にファイア・ボールをはなっても良いわよ♪");

                        UpdateMainMessage("アイン：やっぱ出来たのかよ！　さすがだぜ、ラナ、ッハッハッハ！！");

                        UpdateMainMessage("アイン：いやいや、本当ラナは最高だぜ！俺も正直付いていけねえぜホント！");

                        UpdateMainMessage("ラナ：分かった分かったってホント暑苦しいわね・・・良いから、早く撃ってみなさいよ。");

                        UpdateMainMessage("アイン：ああ、そうだな・・・じゃあ行くぜ！");

                        UpdateMainMessage("アイン：ファイア！");

                        UpdateMainMessage("ラナ：見切ったわ、ソレね！");

                        UpdateMainMessage("アイン：！！？");

                        UpdateMainMessage("　　　（ッシュッパァァァン・・・（ファイア・ボールが瞬時に空中ではじけ飛んだ）");

                        UpdateMainMessage("アイン：す・・・スゲェスゲェ！　ッハッハッハッハ！！");

                        UpdateMainMessage("アイン：おいおいおい、何だよコレ！　マジかよラナ！！");

                        UpdateMainMessage("ラナ：消費コストは結構多いみたいだから多用はできないけどね♪");

                        UpdateMainMessage("アイン：いやいやそれでもこの問答無用の一発キャンセルはすげえ！");

                        UpdateMainMessage("アイン：ラナ、期待してるぜ。");

                        UpdateMainMessage("アイン：お前に出会えて俺は最高に幸せだ！　ッハッハッハ！");

                        UpdateMainMessage("　　　『ッシャゴオォォオォォ！！！』（ラナのデバステイトブローがアインに炸裂した）　　");

                        UpdateMainMessage("ラナ：言葉選びなさい・・・バカアイン");

                        UpdateMainMessage("アイン：せ・・・せっかく褒めたのに・・・");
                        
                        UpdateMainMessage("アイン：（・・・バタ）");

                        sc.StanceOfSuddenness = true;
                        ShowActiveSkillSpell(sc, Database.STANCE_OF_SUDDENNESS);
                        UpdateMainMessage("", true);
                    }
                    #endregion
                    else if (!we.AlreadyRest)
                    {
                        mainMessage.Text = MessageFormatForLana(1001);
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
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
            #region "オル・ランディス遭遇前後"
            else if (we.AvailableDuelMatch && !we.MeetOlLandis)
            {
                if (!we.MeetOlLandisBeforeLana)
                {
                    UpdateMainMessage("ラナ：アイン、どうしたの？何か顔が冴えないみたいだけど。");

                    UpdateMainMessage("アイン：・・・ボケ師匠のランディスが闘技場に来てるみたいだ。");

                    UpdateMainMessage("ラナ：えっ、そうなの！？　良かったじゃない♪　早速、会いに行ってきたら♪");

                    UpdateMainMessage("アイン：出会ったら、俺が死んじまうだろ？");

                    UpdateMainMessage("ラナ：なにそんなビビってんのよ。だったら、私が先に顔会わせして来ようかな♪");

                    UpdateMainMessage("アイン：待て！　待て待て！！　それだけは駄目だ！！！");

                    UpdateMainMessage("ラナ：何でよ？");

                    UpdateMainMessage("アイン：こういった場合、俺が先に顔会わせに行かなきゃならねえんだ。");

                    UpdateMainMessage("アイン：じゃねえと、マジで殺されちまう。　俺が先に会ってくる。");

                    UpdateMainMessage("ラナ：そうなの。まあ良いわ、じゃあ早いとこ会って来ればいいじゃない♪");

                    UpdateMainMessage("アイン：クッソ、何でお前そんな楽しそうなんだよ。。。まあいいか。");

                    UpdateMainMessage("アイン：おっしゃ、踏ん切りが付いたぜ。早速行ってくるぜ。");

                    UpdateMainMessage("ラナ：あ、アインのためにこんなもの用意しておいたわよ。ハイ、ポーション♪");

                    CallSomeMessageWithAnimation("アインは" + Database.COMMON_REVIVE_POTION_MINI + "を手に入れた。");

                    UpdateMainMessage("アイン：要るかぁぁぁぁ、こんなのおおぉぉぉ！！！");

                    UpdateMainMessage("ラナ：フフ、じゃあ行ってらっしゃい♪", true);

                    GetItemFullCheck(mc, Database.COMMON_REVIVE_POTION_MINI);

                    we.MeetOlLandisBeforeLana = true;
                }
                else
                {
                    UpdateMainMessage("ラナ：行ってらっしゃい♪", true);
                }
                return;
            }
            #endregion
            #region "Ｌｖ４以降、スタンスの習得会話"
            else if (this.firstDay >= 5 && mc.Level >= 4 && we.Truth_CommunicationLana1_1 == false && we.AvailableSecondCharacter)
            {
                if (!we.AlreadyCommunicate)
                {
                    UpdateMainMessage("ラナ：バカアイン、ちょっといい？");

                    UpdateMainMessage("アイン：俺はバカじゃねえって。何だ？");

                    UpdateMainMessage("ラナ：フレッシュ・ヒール。覚えたわよね。");

                    UpdateMainMessage("アイン：ん？ああ、そうだな。なんとなく思い出したぜ。");

                    UpdateMainMessage("アイン：ヒール ＆ アタック！ 基本だからな！");

                    UpdateMainMessage("ラナ：昔っからそうよね・・・その単調なノリ・・・");

                    UpdateMainMessage("アイン：別にいいじゃねえか。ダメージレースの基本だろ。");

                    UpdateMainMessage("ラナ：そのスタンス、変えようと思った事は一度もないわけ？");

                    using (TruthDecision td = new TruthDecision())
                    {
                        td.MainMessage = "　【　スタンスを変えようと思いますか？　】";
                        td.FirstMessage = "思わないな。今までどおり行くぜ。";
                        td.SecondMessage = "正直、一度ぐらいは考えたことがある。";
                        td.StartPosition = FormStartPosition.CenterParent;
                        td.ShowDialog();
                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            UpdateMainMessage("アイン：思わないな。今までどおり行くぜ。");

                            UpdateMainMessage("ラナ：そう。まあ、今更変えるとは思ってなかったけど。");

                            UpdateMainMessage("アイン：やっぱダメージをガツガツ相手に与えないとな！ッハッハッハ！");

                            UpdateMainMessage("ラナ：じゃあ、アインは前衛攻撃型って事になるわね。");

                            UpdateMainMessage("アイン：前衛攻撃型？");

                            UpdateMainMessage("ラナ：前に出て、ダメージをガツガツやる役の事よ。");

                            UpdateMainMessage("アイン：あ、ああ。何だそういう意味かよ。");

                            UpdateMainMessage("ラナ：じゃあ、今までどおり、しっかり前に出て、攻撃よろしく♪");

                            UpdateMainMessage("アイン：ああ、任せておけ！ッハッハッハ！");

                            mc.Stance = PlayerStance.FrontOffence;

                            CallSomeMessageWithAnimation("【アインは前衛攻撃型になりました！】");

                            CallSomeMessageWithAnimation("【物理攻撃５％ＵＰ、魔法攻撃５％ＵＰ】");
                        }
                        else
                        {
                            UpdateMainMessage("アイン：正直、一度ぐらいは考えたことがある。");

                            UpdateMainMessage("ラナ：そう。まあ、今更変えるとは思ってなかったけど。");

                            UpdateMainMessage("アイン：・・・おい、考えた事はあると言ってるだろ。");

                            UpdateMainMessage("ラナ：・・・えええええぇぇぇええ！！？？");

                            UpdateMainMessage("アイン：うお！ビックリさせんなって。お前の驚き方はオーバーすぎるぞ。");

                            UpdateMainMessage("ラナ：ちゃんと理解して言ってるわけ？");

                            UpdateMainMessage("アイン：相手の方がダメージが強い場合は守るのも大事だしな。");

                            UpdateMainMessage("アイン：こちらの体制が崩れないようにして、少しずつ攻撃を織り交ぜる。");

                            UpdateMainMessage("アイン：ディフェンス ＆ アタックって所だな。");

                            UpdateMainMessage("ラナ：た・・・確かにそんな所ね・・・ちょっと違うけど。");

                            UpdateMainMessage("ラナ：アインからそんな言葉が出るとは意外だわ。");

                            UpdateMainMessage("アイン：ラナとテンポよく攻撃するのも良いけどな。");

                            UpdateMainMessage("アイン：ほら、たまにピンチな時あるだろ。守ってりゃ良かったって思う時が。");

                            UpdateMainMessage("ラナ：え、ええ、まあそうね。。。");

                            UpdateMainMessage("ラナ：じゃあ、前衛防衛型って事になるわね。");

                            UpdateMainMessage("アイン：前衛防衛型？何だそりゃ。");

                            UpdateMainMessage("ラナ：前でしっかりと相手のダメージを受け止める役の事よ。");

                            UpdateMainMessage("ラナ：それ以外にも、タイミングよく相手をスタンさせたりしてよね。");

                            UpdateMainMessage("ラナ：それから敵モンスターの注意もちゃんと引いてよね。");

                            UpdateMainMessage("ラナ：あんまり防御一辺倒にならないでよね。タイミングよく攻撃も混ぜてね。");

                            UpdateMainMessage("ラナ：どうしてもって時はライフ回復もちゃんとやってよね。");

                            UpdateMainMessage("アイン：・・・面倒くせえぇぇ！！！");

                            mc.Stance = PlayerStance.FrontDefense;

                            CallSomeMessageWithAnimation("【アインは前衛防衛型になりました！】");

                            CallSomeMessageWithAnimation("【物理防御５％ＵＰ、魔法防御５％ＵＰ】");

                        }
                    }

                    we.AlreadyCommunicate = true;
                }
                else
                {
                    mainMessage.Text = MessageFormatForLana(1001);
                }

                we.Truth_CommunicationLana1_1 = true;
            }
            #endregion
            #region "１人で絡みつくフランシスに遭遇済みの場合"
            else if (this.firstDay >= 1 &&
                !we.Truth_CommunicationLana1_2 &&
                Truth_KnownTileInfo[252] == true &&
                !we.TruthCompleteSlayBoss1
                )
            {
                if (!we.AlreadyCommunicate)
                {
                    UpdateMainMessage("アイン：っくそ・・・");

                    UpdateMainMessage("ラナ：どうしたのよ？");

                    UpdateMainMessage("アイン：ああ、１階のボスなんだがな。");

                    UpdateMainMessage("アイン：これがもう強すぎてだな、全然歯がたたねえ。");

                    UpdateMainMessage("ラナ：無理に突っ込みすぎなんじゃないの？");

                    if (mc.Level < 15)
                    {
                        UpdateMainMessage("ラナ：って、バカアイン・・・レベル" + mc.Level.ToString() + "じゃないのよ！？");

                        UpdateMainMessage("ラナ：そんなんで勝てるわけないでしょ・・・はあああぁぁぁ・・・");

                        UpdateMainMessage("アイン：いや、狙ってやったわけじゃねえ。");

                        UpdateMainMessage("アイン：単に突っ込んでたら、辿り付いちまったからな！ッハッハッハ！");

                        UpdateMainMessage("ラナ：それをバカって言うのよもう・・・はあああぁあぁぁ・・・");
                    }
                    else
                    {
                        UpdateMainMessage("ラナ：レベルは・・・" + mc.Level.ToString() + "ね。まあそれなりにやってるみたいだけど。");
                    }

                    UpdateMainMessage("ラナ：まあ良いわ。ちょっと、よく聞きなさいよね。");

                    UpdateMainMessage("ラナ：第一、ちゃんとレベルを上げる事。良いわね？");

                    if (mc.Level < 15)
                    {
                    }
                    else
                    {
                        UpdateMainMessage("ラナ：LV" + mc.Level.ToString() + "だし、それなりにやってるみたいだけど。");

                        UpdateMainMessage("ラナ：パラメタの割り振りは慎重にね。無茶な上げ方してると後々苦しくなるわよ。");
                    }

                    UpdateMainMessage("ラナ：第二、何も考えずにズンズカ進まないこと。");

                    if (we.dungeonEvent21KeyOpen || we.dungeonEvent22KeyOpen)
                    {
                        UpdateMainMessage("ラナ：警告を示すような看板とか、途中に何も無かったわけ？");

                        UpdateMainMessage("アイン：いや、あったけどな。でもそれなりにダンジョン探索しながら進めてきたつもりだ。");

                        UpdateMainMessage("ラナ：ふーん、ならいいんだけど・・・");
                    }
                    else
                    {
                        UpdateMainMessage("ラナ：警告を示すような看板とか、途中に何も無かったわけ？");

                        UpdateMainMessage("アイン：ッゲ・・・");

                        UpdateMainMessage("ラナ：やっぱりあったんだ。少しはそういう所にも注意してよね、ホント。");
                    }

                    UpdateMainMessage("ラナ：第三、これが結構大事なんだけど、良い装備を揃えるようにすること。");
                    if (mc.MainWeapon != null)
                    {
                        if (mc.MainWeapon.Rare == ItemBackPack.RareLevel.Rare || mc.MainWeapon.Rare == ItemBackPack.RareLevel.Epic)
                        {
                            UpdateMainMessage("アイン：今装備してるのは【" + mc.MainWeapon.Name + "】で、それなりに良い装備だぜ。");

                            UpdateMainMessage("ラナ：へえ、結構いい物持ってるじゃない♪じゃあ言う事無いわね。");
                        }
                        else if (mc.MainWeapon.Rare == ItemBackPack.RareLevel.Common)
                        {
                            UpdateMainMessage("ラナ：アインが今持ってるのって【" + mc.MainWeapon.Name + "】よね。");

                            UpdateMainMessage("ラナ：普通に使える装備みたいだけど、もう少し強い武器を探すといいわよ。");
                        }
                        else
                        {
                            UpdateMainMessage("ラナ：っちょっと・・・【" + mc.MainWeapon.Name + "】って使えない装備してるわね。");

                            if (mc.MainWeapon.Name == Database.POOR_PRACTICE_SWORD)
                            {
                                UpdateMainMessage("ラナ：まあその装備が何なのか、分かってるかはどうかは別としても・・・");
                            }
                            UpdateMainMessage("ラナ：ダンジョンちゃんと探索してれば、もう少しマシな武器が見つかるはずよ。");
                        }
                    }

                    UpdateMainMessage("ラナ：とにかく、レベルアップ。ダンジョン探索。装備の充実よ。");

                    UpdateMainMessage("ラナ：これさえやれば、それなりに解けるようにはなってるから、頑張ってよね。ホント。");

                    UpdateMainMessage("アイン：・・・ハイ。");

                    we.AlreadyCommunicate = true;
                }
                else
                {
                    mainMessage.Text = MessageFormatForLana(1001);
                }
                we.Truth_CommunicationLana1_2 = true;
            }
            #endregion
            #region "２階開始時"
            else if (we.TruthCompleteArea1 && !we.Truth_CommunicationLana21)
            {
                UpdateMainMessage("アイン：っお、こんな所に居たのか。ラナ。");

                UpdateMainMessage("ラナ：バカアイン、ちょうど良かったわ♪");

                UpdateMainMessage("アイン：ッゲ、そのあからさまな挨拶。。。");

                UpdateMainMessage("ラナ：実はね、とってもイイ事を思いついたの。ちょっとコッチコッチ♪");

                UpdateMainMessage("アイン：お、おぉ、分かったって。どこ行くんだよ？");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "ランラン薬品店の前にて・・・";
                    md.ShowDialog();
                }

                UpdateMainMessage("アイン：何だ、薬品店の前じゃないか。");

                UpdateMainMessage("ラナ：アイン、ちょっとコレ飲んでみてよ♪");

                UpdateMainMessage("アイン：紫色のポーションだな。一体何が起こるんだ？");

                UpdateMainMessage("ラナ：ちょっとコレ飲んでみてよ♪");

                UpdateMainMessage("アイン：永続型なのか？非戦闘時でも使えるのか？");

                UpdateMainMessage("ラナ：飲んでみてよ♪");

                UpdateMainMessage("アイン：ッグ・・・最初から嫌な予感はしてたが・・・");

                UpdateMainMessage("アイン：致死薬じゃねえよな？");

                UpdateMainMessage("ラナ：そんなワケないでしょ。ッホラ、とっとと飲め♪");

                UpdateMainMessage("アイン：っしゃ、行くぜ！！");

                UpdateMainMessage("　　　『ッゴクリ・・・』");

                UpdateMainMessage("アイン：特に変化は感じられないようだが・・・");

                UpdateMainMessage("アイン：あっ！！！");

                UpdateMainMessage("ラナ：どう？効いてきた？");

                UpdateMainMessage("アイン：何だかすげえ体が引き締まった感じがするぜ。");

                UpdateMainMessage("アイン：一体どんな効果を狙ったんだよ？");

                UpdateMainMessage("ラナ：身体の耐性能力を増強させる薬よ。今のは耐解毒効果なんだけど");

                UpdateMainMessage("ラナ：通常の解毒作用はもちろん効果はあるんだけど");

                UpdateMainMessage("ラナ：それに加えて、戦闘中であれば、その後ずっと毒耐性が付くってわけ♪");

                UpdateMainMessage("アイン：へぇ！？すげえじゃねえか、それ！！");

                UpdateMainMessage("ラナ：まあ、今のところ解毒作用のカテゴリーしか作れないんだけどね。");

                UpdateMainMessage("アイン：いやいや、それだけでも大したもんだぜ！");

                UpdateMainMessage("アイン：ラナ、お前はやっぱすげえぜ！ッハッハッハ！！");

                UpdateMainMessage("ラナ：うん、ありがと♪");

                UpdateMainMessage("ラナ：いろいろレパートリー増やしていくわね、ご購入お願いします♪");

                UpdateMainMessage("アイン：ああ、頼んだぜ！また買いに来るからな！");

                CallSomeMessageWithAnimation("アインは" + Database.COMMON_RESIST_POISON + "を手に入れた。");
                GetItemFullCheck(mc, Database.COMMON_RESIST_POISON);

                we.Truth_CommunicationLana21 = true;

            }
            #endregion

            #region "１日目"
            else if (this.firstDay >= 1 && !we.Truth_CommunicationLana1)
            {
                // if (!we.AlreadyRest) // 1日目はアインが起きたばかりなので、本フラグを未使用とします。
                if (!we.AlreadyCommunicate)
                {
                    UpdateMainMessage("ラナ：っあら、意外と早いじゃない。");

                    UpdateMainMessage("アイン：ああ、何だか寝覚めが良いんだ。今日も調子全快だぜ！");

                    UpdateMainMessage("ラナ：バカな事言ってないで、ホラホラ、朝ごはんでも食べましょ。");

                    UpdateMainMessage("アイン：ああ、そうだな！じゃあ、ハンナ叔母さんとこで食べようぜ。");

                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "ハンナの宿屋（料理亭）にて・・・";
                        md.ShowDialog();
                    }

                    UpdateMainMessage("アイン：っさっすが、叔母さん！今日の飯もすげえ旨いよな！");

                    UpdateMainMessage("ハンナ：アッハッハ、よく元気に食べるね。まだ沢山あるからね、どんどん食べな。");

                    UpdateMainMessage("ラナ：アイン、少しは控えなさいよね。恥ずかしいったら。");

                    UpdateMainMessage("アイン：ああ、控えるぜ。次からな！ッハッハッハ！！！");

                    UpdateMainMessage("　　　『ッドス！』（ラナのサイレントブローがアインの横腹に炸裂）　　");

                    UpdateMainMessage("アイン：うおおぉぉ・・・だから食ってる時にそれをやるなって・・・");

                    UpdateMainMessage("アイン：・・・ッムグ・・・ごっそうさん！っでだ、ラナ。");

                    UpdateMainMessage("ラナ：え？");

                    UpdateMainMessage("アイン：オレはダンジョンへ向かうぜ。");

                    UpdateMainMessage("アイン：そして、その最下層へオレは辿り付いてみせる！");

                    UpdateMainMessage("ラナ：っちょ、何よいきなり唐突に。");

                    UpdateMainMessage("ラナ：全然脈略が無いじゃない。何よ、本当にそんなトコ行きたいわけ？");

                    if (GroundOne.WE2.TruthBadEnd1)
                    {
                        UpdateMainMessage("アイン：まあ本当に行きたいとか言われてもなあ・・・");

                        UpdateMainMessage("アイン：金を稼いで収支を成り立たせるってのも当然なんだが、");

                        UpdateMainMessage("アイン：伝説のFiveSeekerに追いつきたい気持ちもあるが・・・");

                        UpdateMainMessage("アイン：それは別として、とにかく行かなくちゃならねえ。そんな気がするんだ。");

                        UpdateMainMessage("ラナ：ふーん、何か曖昧な答えね。");

                        UpdateMainMessage("ラナ：まあ、分かったわよ。っじゃあ、はいコレ♪");
                    }
                    else
                    {
                        UpdateMainMessage("アイン：何言ってるんだ、ラナ。俺たちの稼ぎが何なのか忘れたのか？");

                        UpdateMainMessage("アイン：俺達の収支はダンジョンで成り立ってるだろ。金を稼がないとな。");

                        UpdateMainMessage("ラナ：うん、まあそれは分かってるつもりよ。でも何で最下層に行きたがるの？");

                        UpdateMainMessage("アイン：何でかって？そりゃあ決まってるだろ！");

                        UpdateMainMessage("アイン：伝説のFiveSeeker様達に追いつくためさ！！");

                        UpdateMainMessage("ラナ：アインって昔っからFiveSeeker様の事、大好きよね。はしゃいじゃって、ッフフフ。");

                        UpdateMainMessage("アイン：何がおかしい？FiveSeekerはすべての冒険者にとっての憧れの的だろう？目標にして当然だろ。");

                        UpdateMainMessage("ラナ：分かったわよ。っじゃあ、はいコレ♪");
                    }


                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.Message = "【遠見の青水晶】を手に入れました。";
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.ShowDialog();
                    }

                    GetItemFullCheck(mc, Database.RARE_TOOMI_BLUE_SUISYOU);

                    UpdateMainMessage("アイン：お、【遠見の青水晶】じゃねえか。助かるぜ！");

                    UpdateMainMessage("ラナ：無くさないでよ？それ結構レア物で値段張るものなんだから。");

                    UpdateMainMessage("アイン：ん？おう、任せておけって！ッハッハッハ！！");

                    UpdateMainMessage("アイン：っと、そうだ。忘れないうちに・・・");

                    UpdateMainMessage("アイン：・・・（ごそごそ）・・・");

                    UpdateMainMessage("ラナ：何探してるのよ？");

                    UpdateMainMessage("アイン：確かポケットに入れたはず・・・");

                    using (TruthDecision td = new TruthDecision())
                    {
                        td.MainMessage = "　【　ラナにイヤリングを渡しますか？　】";
                        td.FirstMessage = "ラナにイヤリングを渡す。";
                        td.SecondMessage = "ラナにイヤリングを渡さず、ポケットにしまっておく。";
                        td.StartPosition = FormStartPosition.CenterParent;
                        td.ShowDialog();
                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            UpdateMainMessage("アイン：あったあった。ラナ、こいつを渡しておくぜ。");

                            UpdateMainMessage("ラナ：これ、私のイヤリングじゃない。どこで拾ったのよ？");

                            UpdateMainMessage("アイン：どこって、俺の部屋に落ちてたぞ。ラナが落としていったんだろ？");

                            UpdateMainMessage("ラナ：・・・っええ！？そそそ、そんなワケ無いじゃない！！");

                            UpdateMainMessage("アイン：なんでそんな慌ててんだよ。まあ返しておくぜ。ッホラ！");

                            UpdateMainMessage("ラナ：っとと、・・・アリガト♪");

                            UpdateMainMessage("アイン：お前は変な所で抜けてるからな、しっかり持ってろよな。");

                            UpdateMainMessage("アイン：じゃ、行ってくるかな！いざ、ダンジョン！ッハッハッハ！");

                            mc.DeleteBackPack(new ItemBackPack("ラナのイヤリング"));
                            we.Truth_GiveLanaEarring = true;
                        }
                        else
                        {
                            if (GroundOne.WE2.TruthBadEnd1)
                            {
                                UpdateMainMessage("アイン：（・・・このイヤリング・・・）");

                                UpdateMainMessage("アイン：（これをもってると、何か思い出せそうなんだが・・・）");

                                UpdateMainMessage("アイン：（ラナには悪いが、もう少し持っておこう・・・）");

                                UpdateMainMessage("アイン：いや、何でもねえんだ。");

                                UpdateMainMessage("ラナ：今、ポケットをゴソゴソしてたじゃないの？");

                                UpdateMainMessage("アイン：い、いやいや。何でもねえ、ッハッハッハ！");

                                UpdateMainMessage("ラナ：何よ、あからさまに怪しかったわよ？今のは・・・");

                                UpdateMainMessage("アイン：いざ、ダンジョン！ッハッハッハ！");
                            }
                            else
                            {
                                UpdateMainMessage("アイン：おっかしいな・・・確かにポケットに入れたはずだが・・・");

                                UpdateMainMessage("ラナ：何か探し物でもしてるの？");

                                UpdateMainMessage("アイン：い、いやいや。何でもねえ、ッハッハッハ！");

                                UpdateMainMessage("ラナ：何よ、怪しいわね・・・");

                                UpdateMainMessage("アイン：じゃ、行ってくるかな！いざ、ダンジョン！ッハッハッハ！");
                            }
                        }
                    }
                    we.AlreadyCommunicate = true;
                }
                else
                {
                    UpdateMainMessage(MessageFormatForLana(1002));
                }
                we.Truth_CommunicationLana1 = true; // 初回一日目のみ、ラナ、ガンツ、ハンナの会話を聞いたかどうか判定するため、ここでTRUEとします。
            }
            #endregion
            #region "２日目"
            else if (this.firstDay >= 2 && !we.Truth_CommunicationLana2)
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        UpdateMainMessage("アイン：よおラナ！ダンジョンから戻ってきて気づいたんだが。");

                        UpdateMainMessage("ラナ：赤ポーションでも欲しいわけ？");

                        UpdateMainMessage("アイン：っな！？　何で分かった！！？");

                        UpdateMainMessage("ラナ：大体ポーションひとつも持たずにダンジョンに突っ込むヤツなんてどこに居るのよ。");

                        UpdateMainMessage("アイン：悪ぃ・・・悪かったって、なあココはひとつ頼むぜ！");

                        UpdateMainMessage("ラナ：しょうがないわね、今回だけよ。ッホラ♪");
                        GetPotionForLana();

                        UpdateMainMessage("アイン：っしゃ！サンキューサンキュー！");

                        UpdateMainMessage("ラナ：っあ、っちょっとちょっと。");

                        UpdateMainMessage("アイン：ん？何だよ？");

                        UpdateMainMessage("ラナ：ッフフ、やっぱり秘密かな♪");

                        UpdateMainMessage("アイン：おい、待て待て。すげえ気になるじゃねえか？何だよ？");

                        UpdateMainMessage("ラナ：贅沢言わないの。ホラ、とっとと明日のダンジョンに備えなさいよ。");

                        UpdateMainMessage("アイン：贅沢言ってるつもりはねえが・・・オーケー。明日に備えるとするか。");

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
                        UpdateMainMessage("アイン：よおラナ！昨日のダンジョン探索中に思った事なんだが。");

                        UpdateMainMessage("ラナ：赤ポーションでも欲しいわけ？");

                        UpdateMainMessage("アイン：っな！？　何で分かった！！？");

                        UpdateMainMessage("ラナ：大体ポーションひとつも持たずにダンジョンに突っ込むヤツなんてどこに居るのよ。");

                        UpdateMainMessage("アイン：悪ぃ・・・悪かったって、なあココはひとつ頼むぜ！");

                        UpdateMainMessage("ラナ：しょうがないわね、今回だけよ。ッホラ♪");
                        GetPotionForLana();

                        UpdateMainMessage("アイン：っしゃ！サンキューサンキュー！");

                        UpdateMainMessage("ラナ：っあ、っちょっとちょっと。");

                        UpdateMainMessage("アイン：ん？何だよ？");

                        UpdateMainMessage("ラナ：ッフフ、やっぱり秘密かな♪");

                        UpdateMainMessage("アイン：おい、待て待て。すげえ気になるじゃねえか？何だよ？");

                        UpdateMainMessage("ラナ：贅沢言わないの。ホラ、とっととダンジョン行って来なさいよ。");

                        UpdateMainMessage("アイン：贅沢言ってるつもりはねえが・・・まあ行って来るさ！任せておけ！");

                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion
            #region "３日目"
            else if (this.firstDay >= 2 && !we.Truth_CommunicationLana3)
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        UpdateMainMessage("ラナ：っよし、完成完成♪");

                        UpdateMainMessage("アイン：っふう・・・やっぱこのままだと辛いな。");

                        UpdateMainMessage("ラナ：あっ、バカアインじゃない。ちょうど良かったわ。コッチに来てみて♪");

                        UpdateMainMessage("アイン：オレはバカじゃないと言ってるだろ。何だよ？");

                        UpdateMainMessage("ラナ：まあ、良いから良いから、早く来なさいよ。ホラホラホラ！");

                        UpdateMainMessage("アイン：おわっとっと！？　分かった、分かったって。そんなひっぱるなって。");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "ガンツの武具店がある横街道にて・・・";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("アイン：ん？何だ、ガンツ叔父さんの武具店の前じゃねえか。");

                        UpdateMainMessage("ラナ：っさてと・・・じゃ、私についてきて。");

                        UpdateMainMessage("アイン：おお、おいおいどこ行くんだよ！？そっちの角には何にも・・・");

                        UpdateMainMessage("アイン：・・・おおぉぉお！？んだこれは！！！");

                        this.we.AvailablePotionshop = true;
                        this.buttonPotion.Visible = true;
                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "『ラナのランラン薬品店♪』という看板がアインの目に入った。";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("アイン：っな・・・っどうなってやがる！？・・・いつの間にこんな小屋を！？");

                        UpdateMainMessage("アイン：ん！？ラナのヤツ、いつの間に居なくなって！？");

                        UpdateMainMessage("アイン：待て、待て待て。落ち着け・・・");

                        UpdateMainMessage("アイン：・・・");

                        UpdateMainMessage("アイン：・・・");

                        UpdateMainMessage("アイン：・・・ふぅ");

                        UpdateMainMessage("アイン：あの異常なまでに派手な装飾が施されてる小屋に入ってみるしかないか。");

                        UpdateMainMessage("アイン：こんちわーっす。　（って、何言ってんだ俺）");

                        UpdateMainMessage("ラナ：いらっしゃいませ♪　好きな薬品をお買い求・・・めくだ・・さ・・。");

                        UpdateMainMessage("ラナ：お買い求めくださいませよ♪");

                        UpdateMainMessage("アイン：お前、語尾がムチャクチャじゃねえか。ちゃんと練習したのかよ？");

                        UpdateMainMessage("ラナ：いいじゃない、別に。大して差は無いわよ。");

                        UpdateMainMessage("アイン：せっかく客が来たんだ。笑顔で出迎えろよな？");

                        UpdateMainMessage("ラナ：最初笑顔を提供したんだから良いじゃない。");

                        UpdateMainMessage("アイン：いや、いやいや、そういう意味じゃなくて・・・まあ良いか。");

                        UpdateMainMessage("アイン：へえぇ～。結構揃ってんじゃねえか！");

                        UpdateMainMessage("アイン：いつの間にこんなに揃えたんだよ！？");

                        UpdateMainMessage("ラナ：うん、今までの間に結構毎日、調合をして来ていたのよ。");

                        UpdateMainMessage("ラナ：一応数はそろえたから、思い切って始めようと思ったのよ♪");

                        UpdateMainMessage("アイン：うん。良いんじゃねえか？　コイツは気に入ったぜ、ラナ！");

                        UpdateMainMessage("ラナ：ッフフ、ありがとね♪");

                        UpdateMainMessage("アイン：早速だ。何か買っていっても良いのか？");

                        UpdateMainMessage("ラナ：うん。値段はおさえめにしといたから。どうぞ見て行ってね。");

                        CallPotionShop();
                        mainMessage.Text = "";

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
                        UpdateMainMessage("ラナ：っよし、完成完成♪");

                        UpdateMainMessage("アイン：っふう・・・やっぱこのままだと辛いな。");

                        UpdateMainMessage("ラナ：あっ、バカアインじゃない。ちょうど良かったわ。コッチに来てみて♪");

                        UpdateMainMessage("アイン：オレはバカじゃないと言ってるだろ。何だよ？");

                        UpdateMainMessage("ラナ：まあ、良いから良いから、早く来なさいよ。ホラホラホラ！");

                        UpdateMainMessage("アイン：おわっとっと！？　分かった、分かったって。そんなひっぱるなって。");

                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "ガンツの武具店がある横街道にて・・・";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("アイン：ん？何だ、ガンツ叔父さんの武具店の前じゃねえか。");

                        UpdateMainMessage("ラナ：っさてと・・・じゃ、私についてきて。");

                        UpdateMainMessage("アイン：おお、おいおいどこ行くんだよ！？そっちの角には何にも・・・");

                        UpdateMainMessage("アイン：・・・おおぉぉお！？んだこれは！！！");

                        this.we.AvailablePotionshop = true;
                        this.buttonPotion.Visible = true;
                        using (MessageDisplay md = new MessageDisplay())
                        {
                            md.StartPosition = FormStartPosition.CenterParent;
                            md.Message = "『ラナのランラン薬品店♪』という看板がアインの目に入った。";
                            md.ShowDialog();
                        }

                        UpdateMainMessage("アイン：っな・・・っどうなってやがる！？・・・いつの間にこんな小屋を！？");

                        UpdateMainMessage("アイン：ん！？ラナのヤツ、いつの間に居なくなって！？");

                        UpdateMainMessage("アイン：待て、待て待て。落ち着け・・・");

                        UpdateMainMessage("アイン：・・・");

                        UpdateMainMessage("アイン：・・・");

                        UpdateMainMessage("アイン：・・・ふぅ");

                        UpdateMainMessage("アイン：あの異常なまでに派手な装飾が施されてる小屋に入ってみるしかないか。");

                        UpdateMainMessage("アイン：こんちわーっす。　（って、何言ってんだ俺）");

                        UpdateMainMessage("ラナ：いらっしゃいませ♪　好きな薬品をお買い求・・・めくだ・・さ・・。");

                        UpdateMainMessage("ラナ：お買い求めくださいませよ♪");

                        UpdateMainMessage("アイン：お前、語尾がムチャクチャじゃねえか。ちゃんと練習したのかよ？");

                        UpdateMainMessage("ラナ：いいじゃない、別に。大して差は無いわよ。");

                        UpdateMainMessage("アイン：せっかく客が来たんだ。笑顔で出迎えろよな？");

                        UpdateMainMessage("ラナ：最初笑顔を提供したんだから良いじゃない。");

                        UpdateMainMessage("アイン：いや、いやいや、そういう意味じゃなくて・・・まあ良いか。");

                        UpdateMainMessage("アイン：へえぇ～。結構揃ってんじゃねえか！");

                        UpdateMainMessage("アイン：いつの間にこんなに揃えたんだよ！？");

                        UpdateMainMessage("ラナ：うん、今までの間に結構毎日、調合をして来ていたのよ。");

                        UpdateMainMessage("ラナ：一応数はそろえたから、思い切って始めようと思ったのよ♪");

                        UpdateMainMessage("アイン：うん。良いんじゃねえか？　コイツは気に入ったぜ、ラナ！");

                        UpdateMainMessage("ラナ：ッフフ、ありがとね♪");

                        UpdateMainMessage("アイン：早速だ。何か買っていっても良いのか？");

                        UpdateMainMessage("ラナ：うん。値段はおさえめにしといたから。どうぞ見て行ってね。");

                        CallPotionShop();
                        mainMessage.Text = "";

                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion

            //else if (!we.Truth_CommunicationLana4)
            //{
            //    if (!we.AlreadyRest)
            //    {
            //        if (!we.AlreadyCommunicate)
            //        {
            //            we.AlreadyCommunicate = true;
            //        }
            //        else
            //        {
            //            mainMessage.Text = MessageFormatForLana(1001);
            //        }
            //    }
            //    else
            //    {
            //        if (!we.AlreadyCommunicate)
            //        {


            //            we.AlreadyCommunicate = true;
            //        }
            //        else
            //        {
            //            mainMessage.Text = MessageFormatForLana(1002);
            //        }
            //    }
            //}

            #region "イベント無しの場合"
            else
            {
                if (!we.AlreadyRest)
                {
                    if (!we.AlreadyCommunicate)
                    {
                        UpdateMainMessage("ラナ：どう？調子のほうは？");

                        UpdateMainMessage("アイン：当然当然。任せとけって！ッハッハッハ！", true);

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
                        UpdateMainMessage("ラナ：死なないようにがんばる事ね。", true);
                        we.AlreadyCommunicate = true;
                    }
                    else
                    {
                        mainMessage.Text = MessageFormatForLana(1002);
                    }
                }
            }
            #endregion

        }

        // ハンナ宿屋
        private void button1_Click(object sender, EventArgs e)
        {
            #region "現実世界"
            if (GroundOne.WE2.RealWorld && GroundOne.WE2.SeekerEvent601 && !GroundOne.WE2.SeekerEvent603)
            {
                UpdateMainMessage("アイン：叔母さん、いますか？");

                UpdateMainMessage("ハンナ：アインじゃないか。何の用だい？");

                UpdateMainMessage("アイン：いや、特に用ってわけじゃないんだが・・・");

                UpdateMainMessage("ハンナ：どうしたんだい、何か気になる事でもあるのかい。");

                UpdateMainMessage("アイン：叔母さんの作る飯ってさ。もの凄く美味いじゃないですか？");

                UpdateMainMessage("ハンナ：アッハハハ、ありがとうね。何か聞きたい事でもあるのかい？");

                UpdateMainMessage("アイン：どうやって、そんな美味い飯を作れるようになったんですか。");

                UpdateMainMessage("ハンナ：う～ん、どうと言われてもねえ。慣れみたいなもんさ。アッハハハ");

                UpdateMainMessage("アイン：ハハハ・・・");

                UpdateMainMessage("アイン：・・・　・・・　・・・");

                UpdateMainMessage("ハンナ：どうしたんだい、今からダンジョンへ向かうんじゃないのかい？");

                UpdateMainMessage("アイン：えっ。");

                UpdateMainMessage("ハンナ：悩んでるようだね。言ってみな。");

                UpdateMainMessage("アイン：・・・　・・・　・・・");

                UpdateMainMessage("アイン：いや、もう行かなくちゃ。");

                UpdateMainMessage("アイン：叔母さん、本当にどうもありがとう。");

                UpdateMainMessage("ハンナ：アッハハハ、変な子だね。あたしゃ何もしてないよ。");

                UpdateMainMessage("アイン：・・・いや、ありがとう。");

                UpdateMainMessage("アイン：じゃ、行ってくる。");

                UpdateMainMessage("ハンナ：ああ、行ってきなさい。体に気をつけるんだよ。");

                UpdateMainMessage("アイン：ああ");

                GroundOne.WE2.SeekerEvent603 = true;
                Method.AutoSaveTruthWorldEnvironment();
                Method.AutoSaveRealWorld(this.MC, this.SC, this.TC, this.WE, null, null, null, null, null, this.Truth_KnownTileInfo, this.Truth_KnownTileInfo2, this.Truth_KnownTileInfo3, this.Truth_KnownTileInfo4, this.Truth_KnownTileInfo5);
            }
            #endregion
            #region "オル・ランディス遭遇前後"
            else if (we.AvailableDuelMatch && !we.MeetOlLandis)
            {
                if (!we.MeetOlLandisBeforeHanna)
                {
                    UpdateMainMessage("アイン：ふううぅぅ・・・こんちわーっす・・・");

                    UpdateMainMessage("ハンナ：あれま、どうしたんだい。らしくないため息なんか付いて。");

                    UpdateMainMessage("アイン：いや、実はですね・・・");

                    UpdateMainMessage("アイン：あのボケ師匠がＤＵＥＬ闘技場へ来てるみたいなんですよ・・・");

                    UpdateMainMessage("ハンナ：そりゃ本当かい？良かったじゃないか。");

                    UpdateMainMessage("アイン：はあああぁぁ・・・");

                    UpdateMainMessage("ハンナ：ちょっとそこで待ってなさいな。");

                    UpdateMainMessage("アイン：え？あ、はい。");

                    CallSomeMessageWithAnimation("ハンナは厨房から何かを持ってきた");

                    UpdateMainMessage("アイン：これは一体・・・なんですか？");

                    UpdateMainMessage("ハンナ：キツ～い辛味スパイスを加えた、激辛カレーだよ、たんと食べな。");

                    UpdateMainMessage("アイン：マジかよ・・・ッハッハッハ、悪いなおばちゃん。");

                    UpdateMainMessage("アイン：そうだな、考えててもしょうがねえよな。じゃいただきますっと！");

                    UpdateMainMessage("アイン：グオォォ！！！か、辛ええええぇ！！！");

                    UpdateMainMessage("ハンナ：今のうちにキツいパンチをもらっておくんだね。アッハハハ。", true);
                    we.MeetOlLandisBeforeHanna = true;
                }
                else
                {
                    UpdateMainMessage("ハンナ：っさあ、おとなしくＤＵＥＬ闘技場へ行ってくるんだね。", true);
                }
                return;
            }
            #endregion
            #region "４階開始時"
            else if (we.TruthCompleteArea3 && !we.Truth_CommunicationHanna41)
            {
                we.Truth_CommunicationHanna41 = true;

                UpdateMainMessage("ハンナ：あら、どうしたんだい。");

                UpdateMainMessage("アイン：すまねえ、爽快ドリンクを一本もらえるかな。");

                UpdateMainMessage("ハンナ：はいよ。");

                UpdateMainMessage("アイン：おっ、サンキュー。");

                UpdateMainMessage("ハンナ：いよいよ、４階に進むのかい。");

                UpdateMainMessage("アイン：ええ、まあ・・・");

                UpdateMainMessage("ハンナ：アッハッハ、何をそんなに怖気づいてるんだい。");

                UpdateMainMessage("アイン：いや、怖気づいてるわけじゃないんだが・・・");

                UpdateMainMessage("アイン：何となくかな・・・ッハハ");

                UpdateMainMessage("ハンナ：そんな所、ラナちゃんには見せられないね。台無しだよ。");

                UpdateMainMessage("アイン：いやいやいや、なんでアイツが出てくるんですか。");

                UpdateMainMessage("ハンナ：おや、出てきちゃ悪いのかい？");

                UpdateMainMessage("アイン：いや、関係ねえ話かなと思って・・・");

                UpdateMainMessage("ハンナ：で、どうしたんだい？怖気づいたんじゃないとしたら");

                UpdateMainMessage("アイン：多分、迷ってるんですよ、俺。");

                UpdateMainMessage("アイン：ヒトゴトみたいに言ってるのもオカシイんですけど。");

                UpdateMainMessage("アイン：【迷いが拭えない】って言ったらいいのか・・・なんだろ。");

                UpdateMainMessage("アイン：今までのが水の泡になったら、って考えると、先に進めなくなるんですよ。");

                UpdateMainMessage("ハンナ：４階に今行きたくないんなら、１日伸ばしたらどうだい。");

                UpdateMainMessage("アイン：いや、行きたくないわけじゃないんですよ。");

                UpdateMainMessage("ハンナ：行くのが、怖いのかい？");

                UpdateMainMessage("アイン：いや、怖いわけでもなく・・・");

                UpdateMainMessage("アイン：・・・なんとなくですが・・・");

                UpdateMainMessage("アイン：【誤った】っていう感触が襲ってくる感じなんですよ。");

                UpdateMainMessage("アイン：進めば進むほど、その感覚が強くなる感じがして・・・");

                UpdateMainMessage("ハンナ：【誤った】というのは感覚の問題だよ。");

                UpdateMainMessage("ハンナ：世界から見れば、【誤った】【正しかった】は存在しない。");

                UpdateMainMessage("アイン：それに関しては、ボケ師匠から嫌というほど知らされてます、分かってるんです。");

                UpdateMainMessage("アイン：だからこれも理由としては違う気がしてて・・・");

                UpdateMainMessage("ハンナ：アイン、深く掘りすぎない事が肝心だよ。");

                UpdateMainMessage("ハンナ：あんたは昔からその独特なクセがあるみたいだからね。");

                UpdateMainMessage("アイン：クセか、ッハハハ・・・確かにそうかも。");

                UpdateMainMessage("ハンナ：【誤った】ことを感じたままの状態で、進めなさい。");

                UpdateMainMessage("ハンナ：【正しかった】で前提で進む心意気が把握できてるのなら");

                UpdateMainMessage("ハンナ：今の状態は【誤った】感を察知した上で進めるのも心構えは同じだとは思わないかい？");

                UpdateMainMessage("アイン：誤った感を察知した上で・・・");

                UpdateMainMessage("アイン：なるほど・・・なるほど、そうかもな！");

                UpdateMainMessage("アイン：そうだな！そうだ、そうだ！そうだよ！サンキュー！");

                UpdateMainMessage("アイン：いやあ、おばちゃんのトコ来ると本っっ当に助かるぜ！ッハッハッハ！");

                UpdateMainMessage("ハンナ：アッハハハ、そうかい。元気になれたんなら良いよ。");

                UpdateMainMessage("ハンナ：アンタが冷えると、隣のラナちゃんも冷え込んでくるからね。まあ、気をつけな。");

                UpdateMainMessage("アイン：いやいやいや、だからアイツは本っっ当関係ないでしょうが、まったく・・・");

                UpdateMainMessage("ハンナ：アッハハハハ、そういう事にしておくわ。");

                UpdateMainMessage("ハンナ：ッホラ、じゃあ頑張って行ってきなさい。");

                UpdateMainMessage("アイン：ああ、ありがと！　じゃな！");
            }
            #endregion
            #region "３階開始時"
            else if (we.TruthCompleteArea2 && !we.Truth_CommunicationHanna31)
            {
                we.Truth_CommunicationHanna31 = true;

                UpdateMainMessage("ハンナ：あら、どうしたんだい。");

                UpdateMainMessage("アイン：いや、紅茶一杯もらえるかな。");

                UpdateMainMessage("ハンナ：はいよ。");

                UpdateMainMessage("アイン：さあて、どうすっかな・・・ホント。");

                UpdateMainMessage("ハンナ：なんの話だい？");

                UpdateMainMessage("アイン：スキルアップの話さ。");

                UpdateMainMessage("アイン：俺はもう十分強くなった、そう思うか？叔母ちゃん。");

                UpdateMainMessage("ハンナ：アッハハハハ、もう十分強いんじゃないのかい？");

                UpdateMainMessage("アイン：んなわけねえよな・・・分かってて聞いてんだけどさ、ハハハ。");

                UpdateMainMessage("ハンナ：何迷ってるかは分からないけど、コレだけは言えるよ。");

                UpdateMainMessage("ハンナ：アイン、あんたは強い部類に入るわよ。");

                UpdateMainMessage("アイン：ええぇ・・・お世辞なんか良いですよ。");

                UpdateMainMessage("アイン：自分のウィークポイントなんか山ほどあるし、全然強くならないんですよ。");

                UpdateMainMessage("ハンナ：いいや、数多くの旅の人を見てきたアタシが言うんだから、間違いないよ。");

                UpdateMainMessage("アイン：い、いやいや、本当・・・");

                UpdateMainMessage("ハンナ：いやいや、あんたは本当に強いよ。");

                UpdateMainMessage("アイン：う～ん、本当ですか？");

                UpdateMainMessage("ハンナ：本当の本当ってもんさ、アッハハハハ。");

                UpdateMainMessage("アイン：ハハハ・・・ありがとな。叔母ちゃん。");

                UpdateMainMessage("アイン：もし、３階が解けたらさ。");

                UpdateMainMessage("ハンナ：なんだい。");

                UpdateMainMessage("アイン：またいろいろと教えてくれ。");

                UpdateMainMessage("ハンナ：何言ってんだい、アタシから教えられる事なんて無いよ。");

                UpdateMainMessage("ハンナ：まったく。　ッホラホラ、行く前から落ち着いてんじゃないわよ。");

                UpdateMainMessage("ハンナ：キッチリ３階をクリアしてくるんだね、行ってきな。");

                UpdateMainMessage("アイン：あ、ああ！　オーケー！");

                if (we.Truth_CommunicationOl31)
                {
                    UpdateMainMessage("ハンナ：あらやだ、そう言えば忘れていたわ、アイン。");

                    UpdateMainMessage("アイン：ん？　何かあるのか？");

                    UpdateMainMessage("ハンナ：アンタの師匠から預かってるわよ。荷物。");

                    UpdateMainMessage("アイン：あ、ああ。そういや別れ際そんな事言ってたな。");

                    UpdateMainMessage("アイン：オバチャン、荷物管理とかもやってるのか？");

                    UpdateMainMessage("ハンナ：アッハハハハ、やってないわね。");

                    UpdateMainMessage("アイン：っえ、でも師匠の荷物を預かってくれてるんだろ？");

                    UpdateMainMessage("ハンナ：ハイハイ、いいからちょっと待ってな、一旦外に出ておくれ。");

                    UpdateMainMessage("アイン：っえ？あ、ああ・・・");
                    we.Truth_CommunicationHanna31_2 = true;
                }
            }
            #endregion
            #region "荷物預け追加"
            else if (we.TruthCompleteArea2 && !we.AvailableItemBank && we.Truth_CommunicationOl31)
            {
                if (we.Truth_CommunicationHanna31_2 == false)
                {
                    UpdateMainMessage("ハンナ：あら、そう言えば、忘れてたわ。");

                    UpdateMainMessage("アイン：ん？");

                    UpdateMainMessage("ハンナ：アンタの師匠から預かってるわよ。荷物。");

                    UpdateMainMessage("アイン：あ、ああ。そういや別れ際そんな事言ってたな。");

                    UpdateMainMessage("アイン：オバチャン、荷物管理とかもやってるのか？");

                    UpdateMainMessage("ハンナ：アッハハハハ、やってないわね。");

                    UpdateMainMessage("アイン：っえ、でも師匠の荷物を預かってくれてるんだろ？");

                    UpdateMainMessage("ハンナ：ハイハイ、いいからちょっと待ってな、一旦外に出ておくれ。");

                    UpdateMainMessage("アイン：っえ？あ、ああ・・・");
                }

                UpdateMainMessage("ハンナ：アイン、ほらこっちだよ。");

                UpdateMainMessage("アイン：あ、ああ。。。");

                UpdateMainMessage("アイン：（ホントだ。ちゃんと置いてってくれてたんだな・・・）");

                UpdateMainMessage("ハンナ：ああ見えて、照れ屋だからね。アンタの師匠は。");

                UpdateMainMessage("ハンナ：アンタに期待してるみたいだったよ。感謝しなさい、ッホラ！");

                UpdateMainMessage("アイン：あ、ああ、ああ・・・サンキューな、オバチャン。");

                UpdateMainMessage("ハンナ：アッハハハハ、アタシじゃなくて、お師匠さんに感謝しなさい。");

                UpdateMainMessage("アイン：ハハ・・・確かに。");

                UpdateMainMessage("アイン：しかし突然渡されてもな・・・");

                UpdateMainMessage("アイン：オバチャン、少しだけの間、保管しておいてもらえるか？");

                UpdateMainMessage("ハンナ：ああ、モチロンだよ。少しと言わずしばらくはずっと保管しといてあげるよ。");

                UpdateMainMessage("ハンナ：好きな時に持って行くんだね。");

                UpdateMainMessage("アイン：あと、俺のアイテムも出来れば・・・");

                UpdateMainMessage("ハンナ：モチロン構わないよ。預けたいモノは預けていきな。");

                UpdateMainMessage("アイン：いやあ、ホンット助かるぜ、サンキュー！");

                we.AvailableItemBank = true;
                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "ハンナの宿屋で「荷物の預け・受け取り」が可能になりました！";
                    md.ShowDialog();
                }

                UpdateMainMessage("ハンナ：ただ、無限には受け取れないよ。こちらも倉庫は限られてるからね。");

                UpdateMainMessage("アイン：いやいや、少しだけでも。本当助かります。ありがとうございます！");

                UpdateMainMessage("ハンナ：後は、アンタの好きなように整備しな。任せたわよ。");

                UpdateMainMessage("アイン：ありがとうございました！使わせてもらいます！どうもです！！");
            }
            #endregion
            #region "２階開始時"
            else if (we.TruthCompleteArea1 && !we.Truth_CommunicationHanna21)
            {
                UpdateMainMessage("ハンナ：おや、アインじゃないか。どうしたんだい？");

                UpdateMainMessage("アイン：叔母ちゃん、エルモラの紅茶一杯ください。");

                UpdateMainMessage("ハンナ：あいよ。少し待ってるんだね。");

                UpdateMainMessage("ハンナ：はい、どうぞ召し上がりな。");

                UpdateMainMessage("アイン：サンキュー、叔母ちゃん。");

                UpdateMainMessage("アイン：ふう・・・");

                UpdateMainMessage("ハンナ：どうしたんだい。言ってごらん。");

                UpdateMainMessage("アイン：２階行ってくるぜ。");

                UpdateMainMessage("ハンナ：そうかい、頑張って来な。");

                UpdateMainMessage("アイン：ただ・・・");

                UpdateMainMessage("アイン：っつ・・・上手く言えないんだが・・・");

                UpdateMainMessage("ハンナ：上手く行ってる証拠と考えたらどうだい？");

                UpdateMainMessage("アイン：・・・っはい？");

                UpdateMainMessage("ハンナ：何も無い状態なら、そんな風にはならないよ。");

                UpdateMainMessage("ハンナ：何か想う所がある。違うかい？");

                UpdateMainMessage("アイン：っえ、ええ・・・まあそうです。");

                UpdateMainMessage("ハンナ：だったら、その通りに進んでみたらどうだい。");

                UpdateMainMessage("ハンナ：進まない限り、答えなんて見つかりっこないからね。");

                UpdateMainMessage("アイン：・・・そうか、なるほど・・・");

                UpdateMainMessage("アイン：叔母ちゃん、ありがとな。今度こそ、２階行ってくるぜ！");

                UpdateMainMessage("ハンナ：あいよ、行ってらっしゃい。");

                we.Truth_CommunicationHanna21 = true;
            }
            #endregion
            #region "一日目"
            else if (this.firstDay >= 1 && !we.Truth_CommunicationHanna1 && mc.Level >= 1)
            {
                UpdateMainMessage("アイン：叔母さん、いますか？");

                UpdateMainMessage("ハンナ：アインじゃないか。何の用だい？");

                UpdateMainMessage("アイン：いや、特に用ってわけじゃないんだが・・・");

                UpdateMainMessage("ハンナ：どうしたんだい、何か気になる事でもあるのかい。");

                UpdateMainMessage("アイン：叔母さんの作る飯ってさ。もの凄く美味いじゃないですか？");

                UpdateMainMessage("ハンナ：アッハハハ、ありがとうね。何か聞きたい事でもあるのかい？");

                UpdateMainMessage("アイン：どうやって、そんな美味い飯を作れるようになったんですか。");

                UpdateMainMessage("ハンナ：う～ん、どうと言われてもねえ。こればかりは経験を積むしかないよ。");

                UpdateMainMessage("アイン：経験・・・か。");

                UpdateMainMessage("ハンナ：アイン。ひとつ頼まれてくれないかい？");

                UpdateMainMessage("ハンナ：アインは今からダンジョンへ向かうんだね？");

                UpdateMainMessage("アイン：はい。");

                UpdateMainMessage("ハンナ：ダンジョンで得たアイテムで、食材になる物をワタシの所へ持ってきてくれないかね？");

                UpdateMainMessage("ハンナ：そうしたら、これまでよりもっと良い夕飯を出せるようになるからね。");

                UpdateMainMessage("アイン：マジっすか！？なら、喜んで持ってきますよ！任せておいてください！");

                UpdateMainMessage("ハンナ：アッハハハ、期待して待ってるよ。さあ、いってらっしゃいな。", true);

                we.Truth_CommunicationHanna1 = true; // 初回一日目のみ、ラナ、ガンツ、ハンナの会話を聞いたかどうか判定するため、ここでTRUEとします。
                return;
            }
            #endregion
            #region "その他"
            else
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
                        yesno.Location = new Point(this.Location.X + 784, this.Location.Y + 708);
                        yesno.Large = true;
                        yesno.ShowDialog();
                        if (yesno.DialogResult == DialogResult.Yes)
                        {
                            mainMessage.Text = "ハンナ：はいよ、部屋は空いてるよ。ゆっくりと休みな。";
                            ok.ShowDialog();
                            mainMessage.Text = "アイン：サンキュー、おばちゃん。";
                            ok.ShowDialog();

                            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_NIGHT);

                            mainMessage.Text = "ハンナ：今日は何か食べていくかい？";
                            ok.ShowDialog();
                            using (TruthRequestFood trf = new TruthRequestFood())
                            {
                                trf.StartPosition = FormStartPosition.CenterParent;
                                trf.MC = this.mc;
                                trf.SC = this.sc;
                                trf.TC = this.tc;
                                trf.WE = this.we;
                                trf.ShowDialog();
                                this.mc = trf.MC;
                                this.sc = trf.SC;
                                this.tc = trf.TC;
                                this.we = trf.WE;
                                if (trf.DialogResult == System.Windows.Forms.DialogResult.OK)
                                {
                                    UpdateMainMessage("アイン：おばちゃん、『" + trf.CurrentSelect + "』を頼むぜ。");

                                    UpdateMainMessage("ハンナ：『" + trf.CurrentSelect + "』だね。少し待ってな。");

                                    UpdateMainMessage("ハンナ：はいよ、お待たせ。たんと召し上がれ。");

                                    UpdateMainMessage("　　【アインは十分な食事を取りました。】");

                                    UpdateMainMessage("アイン：ふう～、食った食った・・・");

                                    UpdateMainMessage("アイン：おばちゃん、ごちそうさま！");

                                    UpdateMainMessage("ハンナ：あいよ、後は明日に備えてゆっくり休みな。");

                                }
                            }

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
                    if (we.AvailableItemBank)
                    {
                        using (SelectDungeon sd = new SelectDungeon())
                        {
                            sd.StartPosition = FormStartPosition.Manual;
                            sd.Location = new Point(this.Location.X + 350, this.Location.Y + 550);
                            sd.MaxSelectable = 2;
                            sd.FirstName = "会話";
                            sd.SecondName = "倉庫";
                            sd.ShowDialog();
                            if (sd.TargetDungeon == -1)
                            {
                                return;
                            }
                            else if (sd.TargetDungeon == 1)
                            {
                                mainMessage.Text = "ハンナ：もう朝だよ。今日も頑張ってらっしゃい。";
                            }
                            else
                            {
                                UpdateMainMessage("ハンナ：荷物倉庫かい？ホラ、コッチだよ。", true);
                                mainMessage.Update();
                                System.Threading.Thread.Sleep(1000);
                                CallItemBank();
                                UpdateMainMessage("ハンナ：また用があったら寄るんだね。", true);
                            }
                        }
                    }
                    else
                    {
                        mainMessage.Text = "ハンナ：もう朝だよ。今日も頑張ってらっしゃい。";
                    }
                }
            }
            #endregion
        }

        // DUEL闘技場
        private void button7_Click(object sender, EventArgs e)
        {
            #region "４階開始時"
            if (we.TruthCompleteArea3 && !we.Truth_CommunicationOl41)
            {
                we.Truth_CommunicationOl41 = true;

                string detectSword = PracticeSwordLevel(mc);

                UpdateMainMessage("アイン：ふぅ・・・対戦相手の戦歴チェックっと・・・");

                UpdateMainMessage("アイン：・・・師匠、今頃何してるかな。");

                UpdateMainMessage("ラナ：あっ、アイン。こんな所に居たのね。");

                UpdateMainMessage("アイン：おお、ラナか。どうしたんだ？");

                UpdateMainMessage("ラナ：なんかね、闘技場の受付の人が、アインを探してたみたいよ。");

                UpdateMainMessage("アイン：そうか。じゃあちょっと受付まで行ってくる。サンキュー。");

                CallSomeMessageWithAnimation("－－－　アインは受付まで出向いた　－－－");

                UpdateMainMessage("　　【受付嬢：ＤＵＥＬ闘技場へようこそ。】");

                UpdateMainMessage("アイン：よう、受付さん。俺に何か用でもあったのか？");

                UpdateMainMessage("　　【受付嬢：はい、新着の伝言が入っております。】");

                UpdateMainMessage("アイン：伝言！？そんな制度があるのか？");

                UpdateMainMessage("　　【受付嬢：はい、あります。】");

                UpdateMainMessage("アイン：ま、まあいいか。。。で、どんな内容なんだ？");

                UpdateMainMessage("　　【受付嬢：こちらに、識別ID<3-297761 Ol_Landis>の音声データが入ったチップがあります。】");

                UpdateMainMessage("アイン：へえ、小っさいチップだな。");

                UpdateMainMessage("アイン：って、識別IDが・・・ッグ・・・");

                UpdateMainMessage("　　【受付嬢：音声チップは手前から向かいにあります、あちらのデンデン君にセットしてお使いください。】");

                UpdateMainMessage("アイン：デンデン君？？");

                UpdateMainMessage("　　【受付嬢：詳しくは端末にある操作説明を読んでご利用ください。】");

                UpdateMainMessage("アイン：あ、ああ・・・");

                UpdateMainMessage("アイン：(しかし、こんなものがあるとは・・・）");

                UpdateMainMessage("アイン：確かコッチだな。");

                UpdateMainMessage("アイン：おし、これだな。えーとどれどれ・・・");

                UpdateMainMessage("アイン（音読）：「チップを装置横にある差し込み口に挿入し、PUSHスタートを押してください。」");

                UpdateMainMessage("アイン：これか・・・よし。");

                UpdateMainMessage("　　【【【　その瞬間。　アインの脳内に直接オル・ランディスの音声が伝わってきた！！　】】】");

                UpdateMainMessage("ランディス：《よぉ、ザコアイン》");

                UpdateMainMessage("アイン：うお！！びっくりするな。直接聞こえるのかよ、これ。");

                UpdateMainMessage("ランディス：《これを聞いてるって事は、ひとまず、四階へと進め始めたって事だな》");

                UpdateMainMessage("アイン：ま、まあ聞いてみるか・・・");

                UpdateMainMessage("ランディス：《いいか、よく聞け》");

                UpdateMainMessage("ランディス：《今から俺が言う事は、全て事実だ》");

                UpdateMainMessage("アイン：っえ？");

                UpdateMainMessage("ランディス：《てめぇが受け止めるかどうかに関しては、てめぇで決めろ》");

                UpdateMainMessage("　　【【【　アインはほんの少しだけ呼吸が止まった　】】】");

                UpdateMainMessage("ランディス：《今からてめぇに起こりうる事象を全て伝える》");

                UpdateMainMessage("ランディス：《まず、てめぇの横にいるアーティだが》");

                UpdateMainMessage("ランディス：《四階開始と同時に姿を消す》");

                UpdateMainMessage("　　【【【　それは　】】】");

                UpdateMainMessage("ランディス：《次に四階の内容だが》");

                UpdateMainMessage("ランディス：《あっさりと道筋通り進めるはずだ》");

                UpdateMainMessage("ランディス：《迷うポイントはほとんどねえ、手筋どおりだ》");

                UpdateMainMessage("　　【【【　心のどこか奥底で　】】】");

                if (detectSword == Database.LEGENDARY_FELTUS)
                {
                    UpdateMainMessage("ランディス：《だがてめぇは、自分の知らない間に》");

                    UpdateMainMessage("ランディス：《今てめぇが手にしているその神剣フェルトゥーシュを、いつの間にか見失う》");
                }
                else
                {
                    UpdateMainMessage("ランディス：《だがてめぇは、そのまま進み続け》");

                    UpdateMainMessage("ランディス：《神剣フェルトゥーシュを永遠に手にする機会を失う》");
                }

                UpdateMainMessage("　　【【【　既に認識していたかの様な冷たい感触　】】】");

                if (detectSword == Database.LEGENDARY_FELTUS)
                {
                    UpdateMainMessage("ランディス：《神剣フェルトゥーシュを見失った状態で、ダンジョンを進み続け》");
                }
                else
                {
                    UpdateMainMessage("ランディス：《神剣フェルトゥーシュを入手出来ていないままの状態で、ダンジョンを進み続け》");
                }

                UpdateMainMessage("ランディス：《四階、最後の試練【神の選択肢】に遭遇》");

                UpdateMainMessage("ランディス：《てめぇは、そこで・・・》");

                UpdateMainMessage("　　【【【　心の隅々にまで、真っ黒なインクが染み込むように　】】】");

                UpdateMainMessage("ランディス：《誤りを選択する》");

                UpdateMainMessage("ランディス：《【神の選択肢】ってのは、そういうもんだ》");

                UpdateMainMessage("ランディス：《その後てめぇは》");

                UpdateMainMessage("　　【【【　絶望という色彩が体中を覆った　】】】");

                UpdateMainMessage("ランディス：《絶望する》");

                UpdateMainMessage("ランディス：《最下層への到達は、終着点なんかじぇねえ》");

                UpdateMainMessage("ランディス：《終わりの始まり》");

                UpdateMainMessage("ランディス：《絶対に、最下層への道を見誤んじゃねえぞ》");

                UpdateMainMessage("ランディス：《いいか、わかったな》");

                UpdateMainMessage("アイン：・・・");

                UpdateMainMessage("アイン：（なんだ・・・これ・・・）");

                UpdateMainMessage("アイン：（師匠が、何故これから先に起こりうる事を知ってるんだ）");

                UpdateMainMessage("アイン：（い、いやいやいや。そもそも未来なんて分かるはずがねえんだが）");

                UpdateMainMessage("アイン：（でもあの言い方は・・・）");

                UpdateMainMessage("アイン：（真に迫る言い方、つまりブラフや威嚇じゃねえ）");

                UpdateMainMessage("アイン：（本当の事をキッチリ言う時に使う声だ）");

                UpdateMainMessage("アイン：（だとしたら・・・）");

                UpdateMainMessage("ラナ：ねえ、どうしたのよ？さっきから止まってるみたいだけど");

                UpdateMainMessage("アイン：どぅわあぁ！！！");

                UpdateMainMessage("アイン：ラナか。何だ脅かすなよ。");

                UpdateMainMessage("ラナ：普通に声をかけただけなのに、過剰にビビってんのはそっちじゃない。");

                UpdateMainMessage("アイン：そ、そうか・・・ッハハハ・・・");

                UpdateMainMessage("ラナ：デンデン君からは、良い情報は得られたの？");

                UpdateMainMessage("アイン：ああ、まあな。それなりに。");

                UpdateMainMessage("ラナ：ふうん、まあ詮索はしないけど。");

                UpdateMainMessage("アイン：おし、じゃあ頑張って行くとするか！");

                UpdateMainMessage("ラナ：まったく相変わらずゲンキンよね。闘技場で忘れ物とかしないでよ？");

                UpdateMainMessage("アイン：ああ、分かってるって。っさ、行くぜ！");
            }
            #endregion
            #region "３階開始時"
            else if (we.TruthCompleteArea2 && !we.Truth_CommunicationOl31)
            {
                we.Truth_CommunicationOl31 = true;

                UpdateMainMessage("アイン：師匠、いるか？");

                UpdateMainMessage("ランディス：なんだ、ザコアイン。");

                UpdateMainMessage("アイン：３階に向けて、今から少し作戦タイムを・・・");

                UpdateMainMessage("ランディス：そぉだ、言い忘れてた事がある。");

                UpdateMainMessage("アイン：何だよ？");

                UpdateMainMessage("ランディス：オレは抜ける。");

                UpdateMainMessage("アイン：え？");

                UpdateMainMessage("ランディス：以上だ。");

                UpdateMainMessage("アイン：・・・えええぇぇぇ！？何でだよ！？");

                UpdateMainMessage("ランディス：急な用事だ。　テメェのお守りはココまでだ。");

                UpdateMainMessage("アイン：な、何だよ突然！？　用事って何だよ！");

                UpdateMainMessage("ランディス：っせぇ、黙れザコ。てめぇには関係ねえ。");

                UpdateMainMessage("アイン：くっそおぉぉ・・・マジかよ・・・");

                UpdateMainMessage("ランディス：荷物の件だが、【ハンナゆったり宿屋】に預けておいた。");

                UpdateMainMessage("ランディス：好きな時に荷物整備しとけ。");

                UpdateMainMessage("ランディス：以上だ。");

                CallSomeMessageWithAnimation("オル・ランディスはその場から立ち去っていった・・・");

                Method.RemoveParty(we, tc);

                UpdateMainMessage("アイン：・・・くそぉ、なんの前触れも無しかよ。");

                UpdateMainMessage("ラナ：でもランディスさんは、アインが来るのを一応待っていたワケよね。");

                UpdateMainMessage("アイン：ん？んん・・・まあ確かにそうなのかも。");

                UpdateMainMessage("ラナ：フフフ、何かオカシイわね。アイン結構気に入られてるんじゃないの♪");

                UpdateMainMessage("アイン：ウソつけ、あんなのテキトー快楽主義者だろ。。。");

                UpdateMainMessage("ラナ：で、ダンジョンはどうするわけ？");

                UpdateMainMessage("アイン：１人減る事で、ダンジョンのモンスター難易度も調整されるだろ。");

                UpdateMainMessage("アイン：いまさら止めてもどうかなるわけじゃないしな。続行だ。");

                UpdateMainMessage("ラナ：アインが続行なら、私も引き続きついて行くわよ♪");

                UpdateMainMessage("アイン：ああ、そうしてくれ。助かるぜ！");

                if (we.Truth_CommunicationLana31)
                {
                    UpdateMainMessage("ラナ：ところで、転送装置からファージル宮殿に行ってみない？");

                    UpdateMainMessage("アイン：おお、そうだったな！　じゃ、行くとするか！");
                }

                return;
            }
            #endregion

            #region "Duel申請中"
            if (!we.AvailableDuelMatch && !we.MeetOlLandis)
            {
                if (!we.AlreadyRest)
                {
                    UpdateMainMessage("アイン：まだ、登録申請中みたいだ。明日まで待つとするか。", true);
                }
                else
                {
                    UpdateMainMessage("アイン：受付さんよ。俺の登録申請はまだか？");

                    UpdateMainMessage("　　【受付嬢：もうしばらくお待ちください。】");

                    UpdateMainMessage("アイン：そっか、じゃあまたな。", true);
                }
                return;
            }
            #endregion

            #region "オル・ランディス遭遇"
            if (we.AvailableDuelMatch && !we.MeetOlLandis)
            {
                GroundOne.StopDungeonMusic();
                GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                UpdateMainMessage("　　【受付嬢：ＤＵＥＬ闘技場へようこそ。】");

                UpdateMainMessage("アイン：よお受付さん。今日はちょっと顔を出しただけなんだ。");

                UpdateMainMessage("アイン：いやいや、ッホント。特に用事はねえんだ。");

                UpdateMainMessage("アイン：邪魔したな、ッハッハッハ！");

                UpdateMainMessage("アイン：っじゃ、また今度な！");

                UpdateMainMessage("　　【【【　その瞬間。　アインは背筋の感触が無くなるほど凍りついた。　】】】");

                UpdateMainMessage("ランディス：よぉ、ザコアイン。");

                UpdateMainMessage("アイン：・・・人違いだ。俺はザコアインじゃねえ。");

                UpdateMainMessage("ランディス：ほぉ、じゃあ誰なんだ？");

                UpdateMainMessage("アイン：いや・・・");

                UpdateMainMessage("ランディス：『いや、いやいやいや。』　か。");

                UpdateMainMessage("ランディス：てめぇ。全っっっっっっ然成長してねえようだな。");

                UpdateMainMessage("アイン：いや・・・っちょ、待っ、っちょ、ッタンマ！");

                UpdateMainMessage("ランディス：はぁ？どうタンマなんだ？");

                UpdateMainMessage("アイン：え？え、いや、っか、かかってくるんじゃ");

                UpdateMainMessage("ランディス：前祝したいってワケか。よおおおおぉぉぉぉぉし！良い心構えだ。");

                UpdateMainMessage("アイン：いや、違っ、っちょっちょちょ！タンマタンマタンマ！！");

                GroundOne.StopDungeonMusic();

                UpdateMainMessage("ランディス：死んでこいやああぁぁぁぁぁ！！！");

                UpdateMainMessage("　　【　ドドドスドスドスドスドドドドドスドスドスドスドス　】");

                UpdateMainMessage("　　【　ドガガガガガガドガガガガドドガガガガガガガガ　】");

                UpdateMainMessage("　　【　ボボボボボボグッシャアアァァァァ・・・　】");

                CallSomeMessageWithAnimation("－－－　アイン気絶から、１時間が経過して　－－－");

                GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                UpdateMainMessage("アイン：ったく、ムチャクチャだぜ、ホント。。。いっつつつ・・・");

                UpdateMainMessage("ランディス：てめぇ、ほんっとに成長してねえな。");

                UpdateMainMessage("アイン：いきなり突っかかってくるのが悪いんだろうが。");

                UpdateMainMessage("ランディス：いきなり突っかかってねえだろぉが。");

                UpdateMainMessage("アイン：いや、何かそのこう・・・グォワアアアアって来るなっつうの。");

                UpdateMainMessage("ランディス：シラネエな、んなの。");

                UpdateMainMessage("ランディス：てめぇが弱すぎる。それだけだ。");

                UpdateMainMessage("アイン：いや、まだＤＵＥＬする場所でも無い所で突っかかってくるなつうの。");

                UpdateMainMessage("ランディス：いつだったら良いんだ？");

                UpdateMainMessage("アイン：いや・・・いやいや、そうじゃなくて。");

                UpdateMainMessage("ランディス：またか。その　『いや、いやいやいや。』");

                UpdateMainMessage("アイン：いや、違う。そうじゃな・・");

                UpdateMainMessage("ランディス：てめぇ、ＤＵＥＬに参戦するそうだな。");

                UpdateMainMessage("アイン：え？ああ、参戦するさ。");

                UpdateMainMessage("ランディス：俺様からザコアインへ、一言送ってやろうと思ってな。");

                UpdateMainMessage("アイン：っな、何だよ？");

                UpdateMainMessage("　　＜オル・ランディスは自分の足元へ指先を向け・・・＞");

                UpdateMainMessage("ランディス：　　俺んトコまで、来てみせろ。　");

                UpdateMainMessage("アイン：・・・ああ・・・当然さ！");

                UpdateMainMessage("アイン：当然行ってやるさ！見てろよな！！");

                UpdateMainMessage("　　＜オル・ランディスは少し微笑むと・・・＞");

                UpdateMainMessage("ランディス：ッフ、まぁガンバレや。ザコアイン。");

                CallSomeMessageWithAnimation("オル・ランディスはその場から立ち去っていった・・・");

                UpdateMainMessage("アイン：っくそう。結局、殴られ損かよ・・・");

                UpdateMainMessage("ラナ：っあ、アイン。いたいた♪");

                UpdateMainMessage("アイン：ラナ。いつのまに来てたんだ？");

                UpdateMainMessage("ラナ：アインが気絶してた場面ぐらいからよ♪");

                UpdateMainMessage("アイン：俺が気絶してるトコ見られてたって事かよ。");

                UpdateMainMessage("ラナ：でも本当、あんなに食らってるのに、意外とアイン元気よね。");

                UpdateMainMessage("アイン：師匠は生命に危険を及ぼす急所攻撃はしねえタイプなんだ。");

                UpdateMainMessage("アイン：だから、大概が気絶、もしくは病院送りが関の山ってワケさ。");

                UpdateMainMessage("ラナ：病院送りになっちゃう人もいるのね。まあＤＵＥＬって言う以上しょうがないんだろうけど。");

                UpdateMainMessage("ラナ：あ、そうそう。今日から参戦可能になったんでしょ？");

                UpdateMainMessage("アイン：まあ、そうだな。せっかくだし、今日から対戦してみる所なんだが");

                UpdateMainMessage("ラナ：ＤＵＥＬにおける詳細ルールは、見てみた？");

                UpdateMainMessage("アイン：いや、まだだな。ラナは知ってるのか？");

                UpdateMainMessage("ラナ：ううん、知らないわよ。");

                UpdateMainMessage("ラナ：ＤＵＥＬ参戦者のみに通達されるみたいだし。私は登録してないからね。");

                UpdateMainMessage("アイン：まあ、受付に聞いてみるとするか。おーい、受付さん。");

                UpdateMainMessage("　　【受付嬢：ＤＵＥＬ闘技場へようこそ。】");

                UpdateMainMessage("アイン：すまねえ、さっきは用事ねえって言ったんだが、ＤＵＥＬ詳細ルールっての見せてもらえるか？");

                UpdateMainMessage("　　【受付嬢：アイン様ですね、了解いたしました。】");

                UpdateMainMessage("　　＜受付係員は何か書かれている紙切れを１枚持ってきた。＞");

                UpdateMainMessage("　　【受付嬢：アイン様に関するＤＵＥＬ詳細ルールはこの通りです。ご参照ください。】");

                UpdateMainMessage("アイン：サンキュー！　この紙は、他の奴にも見せていいのか？");

                UpdateMainMessage("　　【受付嬢：構いません。】");

                UpdateMainMessage("アイン：そっか、わざわざありがとな。");

                UpdateMainMessage("アイン：ラナ、もらってきたぞ。じゃあ、見てみるか。");

                UpdateMainMessage("ラナ：うん、何て書いてある？");

                UpdateMainMessage("アイン：どれどれ・・・");

                using (TruthDuelRule tdr = new TruthDuelRule())
                {
                    tdr.StartPosition = FormStartPosition.CenterParent;
                    tdr.ShowDialog();
                }

                UpdateMainMessage("アイン：なるほどな。大体分かったぜ。");

                UpdateMainMessage("ラナ：一応装備やステータス値なんかは見せてもらえるわけね。");

                UpdateMainMessage("アイン：そうみたいだな。");

                UpdateMainMessage("ラナ：そのかわり、こっちも一緒だから、お互い手の内が少し分かっちゃうわね。");

                UpdateMainMessage("アイン：そうみたいだな。");

                UpdateMainMessage("ラナ：ライフ０になった時点で勝敗が決まる。ってことは単純に相手を倒せば良いのよね。");

                UpdateMainMessage("アイン：そうみたいだな。");

                UpdateMainMessage("ラナ：今日は何か一緒に食べてく？");

                UpdateMainMessage("アイン：そうみたいだな。");

                UpdateMainMessage("　　　『シャゴオォォン！！』（ラナのドラスティックキックがアインのミゾオチに炸裂）　　");

                UpdateMainMessage("アイン：っつうう・・・分かった、分かったって。");

                UpdateMainMessage("アイン：っまあ、ちょっと１回だけ対戦させてくれ。その後で、飯食べにいこうぜ。");

                UpdateMainMessage("ラナ：ん、じゃあ待ってるわね。やるからには、ちゃんと勝ってよね♪");

                UpdateMainMessage("アイン：ああ、任せとけって！ッハッハッハ！！");

                we.MeetOlLandis = true;
                return;
            }
            #endregion

            #region "２階開始時"
            else if (we.TruthCompleteArea1 && !we.Truth_CommunicationOl21)
            {
                GroundOne.StopDungeonMusic();
                GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                UpdateMainMessage("　　【受付嬢：ＤＵＥＬ闘技場へようこそ。】");

                UpdateMainMessage("アイン：よお受付さん。今日もちょっと顔を出しただけだ。");

                UpdateMainMessage("アイン：っじゃな！ッハッハッハ！");

                UpdateMainMessage("　　【【【　その瞬間。　アインは背筋の感触が無くなるほど凍りついた。　】】】");

                UpdateMainMessage("ランディス：よぉ。わざわざご苦労なこった。");

                UpdateMainMessage("アイン：きょ、今日は用事があって来た。");

                UpdateMainMessage("ランディス：ほぉ？");

                UpdateMainMessage("アイン：解いたぜ、１階。");

                UpdateMainMessage("ランディス：やるじゃねえか。大したもんだ。");

                UpdateMainMessage("アイン：このまま、進むぜ。");

                UpdateMainMessage("アイン：２階制覇も楽勝さ！ッハッハッハ！");

                UpdateMainMessage("ランディス：ッフ、まあがんばれや。ザコアイン。");

                UpdateMainMessage("アイン：待て、今日はそういう話をしに来たんじゃねえ。");

                UpdateMainMessage("アイン：師匠、お願いがあるんだ。聞いてくれるか？");

                UpdateMainMessage("ランディス：言ってみろ。");

                UpdateMainMessage("アイン：師匠もダンジョンへ一緒に来てくれないか？");

                UpdateMainMessage("ランディス：・・・");

                UpdateMainMessage("ランディス：・・・");

                UpdateMainMessage("アイン：・・・");

                UpdateMainMessage("ランディス：・・・");

                UpdateMainMessage("ランディス：・・・");

                UpdateMainMessage("ランディス：・・・");

                UpdateMainMessage("アイン：・・・");

                UpdateMainMessage("ランディス：駄目だな。");

                UpdateMainMessage("アイン：・・・そうか。");

                UpdateMainMessage("ランディス：少しは成長してるみてえじゃねえか。");

                UpdateMainMessage("アイン：・・・え？");

                UpdateMainMessage("ランディス：何でもねえ。オラ！！とっとと２階制覇してきやがれ！！！");

                UpdateMainMessage("アイン：え、っちょっオワワワワ！っちょちょちょ！！タンマタンマタンマ！！！");

                UpdateMainMessage("　　【　ズドッドドドドドッドドォォドドドド　】");

                UpdateMainMessage("　　【　メキボグッシャアァァァ・・・　】");

                CallSomeMessageWithAnimation("－－－　アイン気絶から、１時間が経過して　－－－");

                GroundOne.StopDungeonMusic();
                GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                UpdateMainMessage("ラナ：っで、断られちゃったわけ？");

                UpdateMainMessage("アイン：そうみたいだな。イッツツツ・・・");

                UpdateMainMessage("ラナ：でも良く誘う気になれたわね？自殺行為じゃない？？");

                UpdateMainMessage("アイン：でもさ。師匠が入れば、神がかり的にパワーアップするだろ？");

                UpdateMainMessage("ラナ：うーん、まあランディスのお師匠さんが居てくれたら心強いわね。");

                UpdateMainMessage("アイン：でもこんな理由じゃ仲間に入ってくれないよな。");

                UpdateMainMessage("ラナ：そうよね・・・う～ん・・・");

                UpdateMainMessage("アイン：いやいや、良いんだ。行こうぜ２階。");

                UpdateMainMessage("ラナ：良いの？");

                UpdateMainMessage("アイン：ああ、今はこのまま進むしかねえ。");

                UpdateMainMessage("アイン：いずれ入ってくれるキッカケのようなモノを作ってみせるさ。");

                UpdateMainMessage("ラナ：っそうね。じゃ、２階制覇に向けて頑張りましょ♪");

                UpdateMainMessage("アイン：ああ！", true);

                we.Truth_CommunicationOl21 = true;
                return;
            }
            #endregion

            #region "オル・ランディスを仲間にするところ"
            else if (we.dungeonEvent226 && !we.Truth_CommunicationOl22)
            {
                //we.Truth_CommunicationOl22 = true;
                if (!we.Truth_CommunicationOl22Fail)
                {
                    GroundOne.StopDungeonMusic();
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    UpdateMainMessage("　　【受付嬢：ＤＵＥＬ闘技場へようこそ。】");

                    UpdateMainMessage("アイン：よお受付さん！");

                    UpdateMainMessage("アイン：いや～～、いやいやいや！ッハッハッハ！");

                    UpdateMainMessage("　　【【【　その瞬間。　アインは背筋の感触が無くなるほど凍りついた。　】】】");

                    UpdateMainMessage("ランディス：おい、受付相手に何いきなり笑ってやがる。");

                    UpdateMainMessage("アイン：師匠、教えてくれ。");

                    UpdateMainMessage("　　『ランディスは一瞬ラナへ視線を移し・・・』");

                    UpdateMainMessage("ランディス：何が聞きたい？");

                    UpdateMainMessage("アイン：このダンジョン。どうなってる？");

                    UpdateMainMessage("ランディス：どうもこうもねえ。単なるダンジョンだ。");

                    UpdateMainMessage("アイン：台座の試練、クリアしたぜ。");

                    UpdateMainMessage("ランディス：やるじゃねえか。ザコアインにしちゃ大したもんだ。");

                    UpdateMainMessage("アイン：知の部屋もクリアまであと一歩だ。");

                    UpdateMainMessage("ランディス：てめぇ、何の話をしにきた？");

                    UpdateMainMessage("　　【【【　アインはさらに背筋に戦慄を感じた。　】】】");

                    UpdateMainMessage("アイン：た！　ッタイム！！");

                    UpdateMainMessage("アイン：教えてくれ。師匠。");

                    UpdateMainMessage("ランディス：何を知りてぇんだ？");
                }
                else
                {
                    if (!we.Truth_CommunicationOl22Progress1)
                    {
                        UpdateMainMessage("アイン：師匠、教えてくれ！　頼むぜ！");

                        UpdateMainMessage("ランディス：何を知りてぇんだ？");
                    }
                    else if (!we.Truth_CommunicationOl22Progress2)
                    {
                        UpdateMainMessage("アイン：師匠・・・頼む、もう１回だけチャンスを！");

                        UpdateMainMessage("ランディス：っち・・・しょうがねえ。");
                    }
                    else
                    {

                        UpdateMainMessage("ランディス：どぉした。");

                        UpdateMainMessage("アイン：っまだまだ！　もう一回DUELだ！！");

                        UpdateMainMessage("ランディス：何度でもかかってこいや、ザコアイン。");
                    }
                }

                using (TruthDecision td = new TruthDecision())
                {
                    td.StartPosition = FormStartPosition.CenterParent;

                    bool firstQuestion = we.Truth_CommunicationOl22Progress1;
                    if (!firstQuestion)
                    {
                        GroundOne.StopDungeonMusic();
                        GroundOne.PlayDungeonMusic(Database.BGM16, Database.BGM16LoopBegin);

                        td.MainMessage = "　【　オル・ランディスへの質問を選択してください。　】";
                        td.FirstMessage = "このダンジョン、どう解いていけば良い？";
                        td.SecondMessage = "このダンジョン、どうすれば解けるんだ？";
                        td.ShowDialog();
                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            UpdateMainMessage("アイン：このダンジョン、どう解いていけば良い？");

                            UpdateMainMessage("ランディス：どうもこうもねえ。自分なりに解いてみろ。");

                            UpdateMainMessage("アイン：台座の試練ってのがあったんだ。");

                            UpdateMainMessage("ランディス：ほぉ。");

                            UpdateMainMessage("アイン：そこでは、『神々の詩』を回答することになっていた。");

                            UpdateMainMessage("ランディス：回答は？");

                            UpdateMainMessage("アイン：出来たさ。");

                            UpdateMainMessage("ランディス：で、それがどうした？");

                            UpdateMainMessage("アイン：あれをどう捉えて良いのかが、わからねえ。");

                            td.MainMessage = "　【　オル・ランディスへの質問を選択してください。　】";
                            td.FirstMessage = "師匠は『神々の詩』に関して、何か知らないか？";
                            td.SecondMessage = "師匠の時も、ダンジョン攻略時、あんな台座が？";
                            td.StartPosition = FormStartPosition.CenterParent;
                            td.ShowDialog();
                            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                            {
                                UpdateMainMessage("アイン：師匠は『神々の詩』に関して、何か知らないか？");

                                UpdateMainMessage("ランディス：知らねぇな。");

                                UpdateMainMessage("アイン：頼む。何でも良いから知ってる事を");

                                UpdateMainMessage("ランディス：ゴチャゴチャとうるせぇ。帰れ。");
                                we.Truth_CommunicationOl22Fail = true;
                            }
                            else
                            {
                                UpdateMainMessage("アイン：師匠の時も、ダンジョン攻略時、あんな台座が？");

                                UpdateMainMessage("ランディス：ああ。");

                                UpdateMainMessage("アイン：どんな内容だったんだ？");

                                UpdateMainMessage("ランディス：てめぇには関係ねえ。");

                                UpdateMainMessage("アイン：教えてくれても良いだろ？");

                                UpdateMainMessage("ランディス：言っても意味がねえ。");

                                td.MainMessage = "　【　オル・ランディスへの質問を選択してください。　】";
                                td.FirstMessage = "意味がねえって・・・どういう意味だ？";
                                td.SecondMessage = "意味がねえかどうかは、聞かなきゃ分からないだろ？";
                                td.StartPosition = FormStartPosition.CenterParent;
                                td.ShowDialog();
                                if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                {
                                    UpdateMainMessage("アイン：意味がねえって・・・どういう意味だ？");

                                    UpdateMainMessage("ランディス：言葉通りだ。言った所で意味はねえ。");

                                    UpdateMainMessage("アイン：俺には当てはまらない・・・って事か？");

                                    UpdateMainMessage("ランディス：良く分かってるじゃねえか。");

                                    td.MainMessage = "　【　オル・ランディスへの質問を選択してください。　】";
                                    td.FirstMessage = "つまり、台座は俺の【未来】に関係してるって事か？";
                                    td.SecondMessage = "つまり、台座は俺の【過去】に関係してるって事か？";
                                    td.ShowDialog();
                                    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        UpdateMainMessage("アイン：つまり、台座は俺の【未来】に関係してるって事か？");

                                        UpdateMainMessage("ランディス：だからてめぇはザコアインだって言ってんだ。");

                                        UpdateMainMessage("アイン：違うのかよ？頼むから、教えてくれよ？");

                                        UpdateMainMessage("ランディス：ゴチャゴチャとうるせぇ。帰れ。");
                                        we.Truth_CommunicationOl22Fail = true;
                                    }
                                    else
                                    {
                                        UpdateMainMessage("アイン：つまり、台座は俺の【過去】に関係してるって事か？");

                                        UpdateMainMessage("ランディス：だとしたら、どうする。");

                                        td.MainMessage = "　【　オル・ランディスへの質問を選択してください。　】";
                                        td.FirstMessage = "過去を基として、解を導き出せって事か？";
                                        td.SecondMessage = "過去を紐解いて、正解を見つけろって事か？";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            GroundOne.StopDungeonMusic();

                                            UpdateMainMessage("アイン：過去を基として、解を導き出せって事か？");

                                            UpdateMainMessage("ランディス：さあな。");

                                            UpdateMainMessage("アイン：どうなんだよ？");

                                            UpdateMainMessage("ランディス：自分で考えろ。");

                                            UpdateMainMessage("アイン：ああ・・・");

                                            UpdateMainMessage("ランディス：ちったぁ、まともになってきたじゃねえか。");

                                            UpdateMainMessage("アイン：・・・え？");

                                            UpdateMainMessage("ランディス：今度は、俺から幾つか問う。");

                                            UpdateMainMessage("ランディス：答えろ。");

                                            UpdateMainMessage("アイン：あ、ああ！");

                                            UpdateMainMessage("", true);

                                            we.Truth_CommunicationOl22Progress1 = true;
                                            firstQuestion = true;
                                            // 正解
                                        }
                                        else
                                        {
                                            UpdateMainMessage("アイン：過去を紐解いて、正解を見つけろって事か？");

                                            UpdateMainMessage("ランディス：正解なんてもんはねえ。");

                                            UpdateMainMessage("アイン：じゃあ、過去が今回の台座の件とどう関係してるんだよ？");

                                            UpdateMainMessage("ランディス：てめぇ、何を聞きにきた？");

                                            UpdateMainMessage("アイン：っぐ・・・");

                                            UpdateMainMessage("ランディス：話にならねえな。");

                                            UpdateMainMessage("ランディス：帰れ、てめぇに教えることはねえ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                }
                                else
                                {
                                    UpdateMainMessage("アイン：意味がねえかどうかは、聞かなきゃ分からないだろ？");

                                    UpdateMainMessage("ランディス：ッチ・・・話にならねぇ。");

                                    UpdateMainMessage("アイン：っま、待ってくれ！！");

                                    UpdateMainMessage("ランディス：意味がねぇもの、無理に聞いてどうなる？");

                                    UpdateMainMessage("アイン：っぐ・・・");

                                    UpdateMainMessage("ランディス：帰れ、てめぇに教えることはねえ。");
                                    we.Truth_CommunicationOl22Fail = true;
                                }
                            }
                        }
                        else
                        {
                            UpdateMainMessage("アイン：このダンジョン、どうすれば解けるんだ？");

                            UpdateMainMessage("ランディス：知らねぇな。自分で探せ。");
                            we.Truth_CommunicationOl22Fail = true;
                        }
                    }

                    if (!we.Truth_CommunicationOl22Progress1)
                    {
                        GroundOne.StopDungeonMusic(); 
                        GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
                        return;
                    } // 正解してない場合、この時点で一旦設問終了


                    bool secondQuestion = we.Truth_CommunicationOl22Progress2;

                    if (!secondQuestion)
                    {
                        GroundOne.StopDungeonMusic();
                        GroundOne.PlayDungeonMusic(Database.BGM16, Database.BGM16LoopBegin);

                        td.MainMessage = "　【　てめぇ、何で俺様の所に来る気になった？　】";
                        td.FirstMessage = "ラナと相談した結果、師匠に聞こうって事で。";
                        td.SecondMessage = "台座の回答をした後、不思議とそう感じたからだ。";
                        td.ShowDialog();
                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            // Fail
                            td.MainMessage = "　【　事実を聞いてんじゃねえ。てめぇはどうなんだ？　】";
                            td.FirstMessage = "台座はクリアした。だが、妙なひっかかりを覚えた。";
                            td.SecondMessage = "どうって・・・特にどうってわけじゃないが・・・";
                            td.ShowDialog();
                            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                            {
                                // Fail
                                td.MainMessage = "　【　何が引っかかったのか、把握はしてんのか？　】";
                                td.FirstMessage = "把握はできてねえが、それなりの違和感は・・・";
                                td.SecondMessage = "いや・・・それが何なのかはわからねえ・・・";
                                td.ShowDialog();
                                if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                {
                                    // Fail
                                    td.MainMessage = "　【　はっきりしねえな。ドッチなんだ？　】";
                                    td.FirstMessage = "す、すまねえ・・・";
                                    td.SecondMessage = "台座は解けた。でもまだ奥底が見えないんだ。";
                                    td.ShowDialog();
                                    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        // Fail
                                        td.MainMessage = "　【　最後だ。何で、ダンジョン挑んでる？　】";
                                        td.FirstMessage = "っそ、それは・・・";
                                        td.SecondMessage = "ダンジョンで稼がなくちゃならねえ。それだけさ";
                                        td.ShowDialog();
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                    else
                                    {
                                        // Fail
                                        td.MainMessage = "　【　奥底ってのは何を指して言ってる？　】";
                                        td.FirstMessage = "奥底ってのは、つまり・・・";
                                        td.SecondMessage = "そんなの、俺にだってわからねえよ。";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                }
                                else
                                {
                                    // Fail
                                    td.MainMessage = "　【　過去と台座の関係ぐらいは分かってんだろうな？　】";
                                    td.FirstMessage = "もちろん、わかってるさ！";
                                    td.SecondMessage = "ッグ・・・";
                                    td.ShowDialog();
                                    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        // Fail
                                        td.MainMessage = "　【　じゃあ、言ってみろ。　】";
                                        td.FirstMessage = "そ、それは・・・";
                                        td.SecondMessage = "過去の出来事が台座での設問になる。そうだろ？";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                    else
                                    {
                                        // Fail
                                        td.MainMessage = "　【　ダンジョンの攻略方法、把握度合いはどぉなんだ？　】";
                                        td.FirstMessage = "それなりに、探索してるし分かってるつもりだぜ。";
                                        td.SecondMessage = "す・・・少しぐらいなら・・・";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // Fail
                                td.MainMessage = "　【　ダンジョンの攻略具合はどぉなんだ？　】";
                                td.FirstMessage = "順調だ。滞りなく進んでる。";
                                td.SecondMessage = "順調ってワケじゃねえ・・・";
                                td.ShowDialog();
                                if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                {
                                    // Fail
                                    td.MainMessage = "　【　攻略の意味は分かってんだろうな？　】";
                                    td.FirstMessage = "攻略の・・・意味？";
                                    td.SecondMessage = "最下層へ進めるための謎を解く。そうだろ？";
                                    td.ShowDialog();
                                    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        // Fail
                                        td.MainMessage = "　【　ダンジョンの攻略度合いさ。決まってるだろ？　】";
                                        td.FirstMessage = "ああ、これなら楽勝だぜ。";
                                        td.SecondMessage = "台座をクリアした所だが・・・";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                    else
                                    {
                                        // Fail
                                        td.MainMessage = "　【　最下層？　謎？　何言ってんだテメェは。　】";
                                        td.FirstMessage = "えっ？ち、違うのかよ？";
                                        td.SecondMessage = "え？　っと・・・";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                }
                                else
                                {
                                    // Fail
                                    td.MainMessage = "　【　台座のどこが気になった？言ってみろ。　】";
                                    td.FirstMessage = "詩の内容は過去に聞いた事がある。しかし・・・";
                                    td.SecondMessage = "看板の前に、突然出てきたって事ぐらいかな。";
                                    td.ShowDialog();
                                    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        // Fail
                                        td.MainMessage = "　【　ハッキリしねえな。わからねぇのか？　】";
                                        td.FirstMessage = "あ、ああ・・・";
                                        td.SecondMessage = "過去との決別をするために！って事だろ？";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                    else
                                    {
                                        // Fail
                                        td.MainMessage = "　【　それがどぉした？　】";
                                        td.FirstMessage = "あ、っいや・・・";
                                        td.SecondMessage = "・・・　・・・";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            //Sucess
                            td.MainMessage = "　【　台座をクリアした意味はわかってるか？　】";
                            td.FirstMessage = "いや、まだ断片的な事しか、わからねえ。";
                            td.SecondMessage = "ああ、当然分かっているさ！";
                            td.ShowDialog();
                            if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                            {
                                // Success
                                td.MainMessage = "　【　今後どうやって進めてくつもりだ？　】";
                                td.FirstMessage = "次への階段を探し出し、最下層を目指すまでさ。";
                                td.SecondMessage = "ダンジョン内をくまなく探索しながら進めるさ。";
                                td.ShowDialog();
                                if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                {
                                    // Fail
                                    td.MainMessage = "　【　最下層まで行けたとして、どうするつもりだ？　】";
                                    td.FirstMessage = "どうって・・・もっと強くなってやるさ。";
                                    td.SecondMessage = "どうって・・・";
                                    td.ShowDialog();
                                    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        // Fail
                                        td.MainMessage = "　【　強くなった後はどうするかを聞いてんだ。　】";
                                        td.FirstMessage = "その後は・・・その・・・";
                                        td.SecondMessage = "強ければ良いんだろ？それが師匠の教えじゃねえか。";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                    else
                                    {
                                        // Fail
                                        td.MainMessage = "　【　最後だ。何で、ダンジョン挑んでる？　】";
                                        td.FirstMessage = "っそ、それは・・・";
                                        td.SecondMessage = "ダンジョン制覇そのものが目的さ、それ以上の意味は無い。";
                                        td.ShowDialog();
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                }
                                else
                                {
                                    // Success
                                    td.MainMessage = "　【　探索して、ダンジョンの仕掛けは把握したのか？　】";
                                    td.FirstMessage = "台座なら、ちゃんと見つけたぜ。クリアもした。";
                                    td.SecondMessage = "台座の一部ぐらいしか・・・全体像はまだ何とも・・・";
                                    td.ShowDialog();
                                    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        // Fail
                                        td.MainMessage = "　【　てめぇのお望みってのは、台座だったのかよ？　】";
                                        td.FirstMessage = "メインの仕掛けを解いた。探索としては成功だろ？";
                                        td.SecondMessage = "いや・・・そういうわけじゃ・・・";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }

                                    }
                                    else
                                    {
                                        // Success
                                        td.MainMessage = "　【　最後だ。どうしてダンジョンへ挑む気になった？　】";
                                        td.FirstMessage = "腕を試したかった。それだけだ。";
                                        td.SecondMessage = "・・・　・・・　";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            // Fail
                                            UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            GroundOne.StopDungeonMusic();

                                            // Success
                                            UpdateMainMessage("ランディス：・・・ほぉ。");
                                            we.Truth_CommunicationOl22Progress2 = true;
                                            secondQuestion = true;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                // Fail
                                td.MainMessage = "　【　じゃあ、言ってみろ。　】";
                                td.FirstMessage = "台座は過去に関連してる。過去を基に解を導き出せばいい。";
                                td.SecondMessage = "あの詩がこのダンジョンにおける最大の鍵だ。";
                                td.ShowDialog();
                                if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                {
                                    // Fail
                                    td.MainMessage = "　【　どぉ導き出されるってんだ？言ってみろ。　】";
                                    td.FirstMessage = "過去の出来事を思い出しつつ、ダンジョン正解ルートを導き出す。";
                                    td.SecondMessage = "ど、どうって・・・";
                                    td.ShowDialog();
                                    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        // Fail
                                        td.MainMessage = "　【　正解だと？　てめぇ、俺から一体何を学んだ？　】";
                                        td.FirstMessage = "っあ！い、いやいやいや！！";
                                        td.SecondMessage = "隠さないでくれよ。正解ルートあるんだろ？";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                    else
                                    {
                                        // Fail
                                        td.MainMessage = "　【　答えるか、答えないか。ハッキリしろ。　】";
                                        td.FirstMessage = "す、すまねぇ・・・";
                                        td.SecondMessage = "未来へとつながるキーワードを探せば良いんだ！";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                }
                                else
                                {
                                    // Fail
                                    td.MainMessage = "　【　最大の鍵。どこで使うんだ？　】";
                                    td.FirstMessage = "ど、どこって・・・";
                                    td.SecondMessage = "最下層だ。最後で使うんだろ、こういうのは。";
                                    td.ShowDialog();
                                    if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                    {
                                        // Fail
                                        td.MainMessage = "　【　ハッキリしねえな。わからねぇのか？　】";
                                        td.FirstMessage = "最下層だ！";
                                        td.SecondMessage = "一番最初だ！";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ランディス：駄目だ。話にならねぇ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                    else
                                    {
                                        // Fail
                                        td.MainMessage = "　【　最後だ。どうしてダンジョンへ挑む気になった？　】";
                                        td.FirstMessage = "師匠の【炎神グローブ】みたいなヤツを俺も欲しいからさ。";
                                        td.SecondMessage = "もちろん、最下層到達で師匠に並ぶためさ。";
                                        td.ShowDialog();
                                        if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                        {
                                            UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ランディス：ッチ、だからテメェは駄目だっつってんだ、帰れ。");
                                            we.Truth_CommunicationOl22Fail = true;
                                        }
                                    }
                                }

                            }
                        }
                    }
                    if (!we.Truth_CommunicationOl22Progress2)
                    {
                        GroundOne.StopDungeonMusic();
                        GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
                        return;
                    } // 正解してない場合、この時点で一旦設問終了

                    GroundOne.StopDungeonMusic();
                    GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);

                    if (!we.Truth_CommunicationOl22DuelFail)
                    {
                        UpdateMainMessage("アイン：師匠、おりいって頼みがある。");

                        UpdateMainMessage("ランディス：何だ、言ってみろ。");

                        UpdateMainMessage("アイン：師匠、このダンジョン一緒に来てくれ。頼むぜ！");

                        UpdateMainMessage("ランディス：・・・");

                        UpdateMainMessage("ランディス：ちっと腕見せてみろ。");

                        UpdateMainMessage("アイン：え？");

                        UpdateMainMessage("ランディス：３");

                        UpdateMainMessage("ランディス：２");

                        UpdateMainMessage("アイン：おわっ！マジかよ！？");

                        UpdateMainMessage("ランディス：１");

                        UpdateMainMessage("アイン：ック・・・来い！！");
                    }

                    bool result = BattleStart(Database.DUEL_OL_LANDIS, true);
                    if (result)
                    {
                        // 勝った場合、次の会話へ
                        GroundOne.WE2.WinOnceOlLandis = true;
                    }
                    else
                    {
                        if (we.Truth_CommunicationOl22DuelFailCount >= 3)
                        {
                            // 負けすぎなので、そのまま通す。ただし、WinOnceOlLandisはつけない。
                        }
                        else
                        {
                            // 負けた場合、強制リトライ
                            UpdateMainMessage("ランディス：帰れ、てめぇに教えることはねえ。");

                            UpdateMainMessage("アイン：ッグ・・・");

                            we.Truth_CommunicationOl22Fail = true;
                            we.Truth_CommunicationOl22DuelFail = true;
                            we.Truth_CommunicationOl22DuelFailCount++;
                            return;
                        }
                    }

                    GroundOne.StopDungeonMusic();
                    GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                    UpdateMainMessage("　　【受付嬢：そちらの方々！！今すぐ対戦を中止してください！！　】");

                    UpdateMainMessage("アイン：ッウワ・・・ヤベ・・・");

                    UpdateMainMessage("　　【受付嬢：闘技場内での勝手な対戦は、ルール厳禁となっております。　】");

                    UpdateMainMessage("ランディス：ッチ、分かった分かったって、嬢ちゃん。");

                    UpdateMainMessage("　　【受付嬢：今から、名前を読み上げます。　】");

                    UpdateMainMessage("　　【受付嬢：オル・ランディス様　】");

                    UpdateMainMessage("　　【受付嬢：アイン・ウォーレンス様　】");

                    UpdateMainMessage("　　【受付嬢：読み上げられた者は、罰としてDUEL戦歴に１敗が加えられます。】");

                    UpdateMainMessage("アイン：ッゲ！！マジかよ！？");

                    UpdateMainMessage("ランディス：くだらんルールだな。");

                    UpdateMainMessage("　　【受付嬢：ただし、アイン・ウォーレンス様はDUEL参加より" + we.GameDay.ToString() + "日以内のため、特例除外とします。】");

                    UpdateMainMessage("アイン：助かったぜ・・・ッホ・・・");

                    UpdateMainMessage("　　【受付嬢：合わせて、オル・ランディス様には累積罰として更に２敗が加えられます。】");

                    UpdateMainMessage("ランディス：勝手に付けとけ。");

                    UpdateMainMessage("　　【受付嬢：なお、今後も続けて行った場合、DUEL戦歴に累積的な敗北数が加算されます。】");

                    UpdateMainMessage("　　【受付嬢：くれぐれも闘技場内での勝手なDUELはしないよう、お願いいたします。】");

                    UpdateMainMessage("アイン：ああ、悪かったな、受付さん。次からは気をつけるよ。");

                    UpdateMainMessage("ランディス：こいつらに気を使う必要はねぇ。ザコアイン。");

                    UpdateMainMessage("アイン：何でだよ。運営側の受付さんだろ？別に良いじゃねえか。");

                    UpdateMainMessage("ランディス：てめぇのそういうトコ・・・");

                    UpdateMainMessage("アイン：受付さんだって人間だ。良いだろ？");

                    UpdateMainMessage("ランディス：つくづく甘ちゃんだなテメェは・・・好きにしろ。");

                    UpdateMainMessage("アイン：っふぅ・・・マジで疲れたぜ。");

                    UpdateMainMessage("アイン：師匠とやるといつも全力だ・・・もう動けねえ・・・");

                    UpdateMainMessage("ランディス：ダンジョン。");

                    UpdateMainMessage("アイン：っえ？");

                    UpdateMainMessage("ランディス：行ってやっても良い。");

                    UpdateMainMessage("アイン：おおおお！！！　マジで！？　やった！！！");

                    UpdateMainMessage("ランディス：条件がある。");

                    UpdateMainMessage("アイン：あ、あぁ。教えてくれ。");

                    UpdateMainMessage("ランディス：ラナを外せ。");

                    UpdateMainMessage("アイン：・・・　えっ　・・・");

                    UpdateMainMessage("ランディス：冗談だ。真に受けんなボケ。");

                    UpdateMainMessage("アイン：っな・・・何だよ。つい考えちまったじゃねえか・・・");

                    UpdateMainMessage("ランディス：ラナと少し距離を置け。");

                    UpdateMainMessage("アイン：い、いやいや、何言ってるんだよ。");

                    UpdateMainMessage("アイン：距離感とかいう話にならない程度の距離でしか・・・");

                    UpdateMainMessage("ランディス：聞けっつってんだ、ボケ。");

                    UpdateMainMessage("ランディス：このダンジョン、解きてぇんだろ？");

                    UpdateMainMessage("アイン：あ、あぁ当然！　目指すは最下層到達だ！！");

                    UpdateMainMessage("ランディス：だったら距離を置け。");

                    UpdateMainMessage("アイン：それのどこがダンジョン攻略に・・・");

                    UpdateMainMessage("ランディス：以上だ。");

                    UpdateMainMessage("ランディス：パーティに入った以上、死ぬまで鍛えてやる。");

                    UpdateMainMessage("ランディス：覚悟しとけや、ザコアイン。");

                    UpdateMainMessage("アイン：あ、ああ！！");

                    UpdateMainMessage("アイン：サンキューな！　師匠！！　");
                    CallSomeMessageWithAnimation("【オル・ランディスがパーティに加わりました。】");

                    we.AvailableThirdCharacter = true;
                    we.Truth_CommunicationOl22 = true;

                    // 「コメント」初回設計で後編３人目をヴェルゼアーティでセーブしてしまっているため、
                    // ここで再設定しなければならなくなった。
                    tc.FullName = "オル・ランディス";
                    tc.Name = "ランディス";
                    tc.Strength = Database.OL_LANDIS_FIRST_STRENGTH;
                    tc.Agility = Database.OL_LANDIS_FIRST_AGILITY;
                    tc.Intelligence = Database.OL_LANDIS_FIRST_INTELLIGENCE;
                    tc.Stamina = Database.OL_LANDIS_FIRST_STAMINA;
                    tc.Mind = Database.OL_LANDIS_FIRST_MIND;
                    tc.Level = 35;
                    tc.Exp = 0;
                    tc.BaseLife = 2080;
                    tc.CurrentLife = tc.MaxLife;
                    tc.BaseSkillPoint = 100;
                    tc.CurrentSkillPoint = 100;
                    //td.TC.Gold = 10; // [警告]：ゴールドの所持は別クラスにするべきです。
                    tc.BaseMana = 1290;
                    tc.CurrentMana = tc.MaxMana;
                    tc.MainWeapon = new ItemBackPack(Database.POOR_GOD_FIRE_GLOVE_REPLICA);
                    tc.MainArmor = new ItemBackPack(Database.COMMON_AURA_ARMOR);
                    tc.Accessory = new ItemBackPack(Database.COMMON_FATE_RING);
                    tc.Accessory2 = new ItemBackPack(Database.COMMON_LOYAL_RING);
                    tc.BattleActionCommand1 = Database.ATTACK_EN;
                    tc.BattleActionCommand2 = Database.DEFENSE_EN;
                    tc.BattleActionCommand3 = Database.STRAIGHT_SMASH;
                    tc.BattleActionCommand4 = Database.VOLCANIC_WAVE;
                    tc.BattleActionCommand5 = Database.LIFE_TAP;
                    tc.BattleActionCommand6 = Database.SEVENTH_MAGIC;
                    tc.BattleActionCommand7 = Database.ONE_IMMUNITY;

                    tc.AvailableMana = true;
                    tc.AvailableSkill = true;
                    tc.StraightSmash = true;
                    tc.FireBall = true;
                    tc.DarkBlast = true;
                    tc.DoubleSlash = true;
                    tc.ShadowPact = true;
                    tc.FlameAura = true;
                    tc.StanceOfStanding = true;
                    tc.DispelMagic = true;
                    tc.LifeTap = true;
                    tc.HeatBoost = true;
                    tc.Negate = true;
                    tc.BlackContract = true;
                    tc.InnerInspiration = true;
                    tc.RiseOfImage = true;
                    tc.Deflection = true;
                    tc.FlameStrike = true;
                    tc.Tranquility = true;
                    tc.VoidExtraction = true;
                    tc.BlackFire = true;
                    tc.Immolate = true;
                    tc.DarkenField = true;
                    tc.DevouringPlague = true;
                    tc.VolcanicWave = true;
                    tc.OneImmunity = true;
                    tc.CircleSlash = true;
                    tc.OuterInspiration = true;
                    tc.SmoothingMove = true;
                    tc.WordOfMalice = true;
                    tc.EnrageBlast = true;
                    tc.SwiftStep = true;
                    tc.Recover = true;
                    tc.SurpriseAttack = true;
                    tc.SeventhMagic = true;
                }

                GroundOne.StopDungeonMusic();
                GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);

                return;

            }
            #endregion

            #region "条件に応じて、Duelを実施します。"
            else
            {
                string Opponent = WhoisDuelPlayer();
                if (Opponent != String.Empty && we.AlreadyRest)
                {
                    we.AlreadyDuelComplete = true;

                    DuelSupportMessage(SupportType.FromDuelGate, Opponent);

                    CallDuel(Opponent, false);
                    return;
                }
                else
                {
                    // 対戦相手がいない場合、何もおきないまま下へ行く。
                }
            }
            #endregion

            GroundOne.PlayDungeonMusic(Database.BGM11, Database.BGM11LoopBegin);
            UpdateMainMessage("アイン：っしゃ、対戦相手でも確認しておこうか。", true);
            using (TruthDuelSelect tds = new TruthDuelSelect())
            {
                tds.StartPosition = FormStartPosition.Manual;
                tds.Location = new Point(this.Location.X + 330, this.Location.Y + 30);
                tds.MC = this.mc;
                tds.SC = this.sc;
                tds.TC = this.tc;
                tds.WE = this.we;
                tds.ShowDialog();
            }
            GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
        }


        private void CallRestInn()
        {
            CallRestInn(false);
        }
        private void CallRestInn(bool noAction)
        {
            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);

            if (noAction == false)
            {
                GroundOne.PlaySoundEffect("RestInn.mp3");
                using (MessageDisplay md = new MessageDisplay())
                {
                    md.Message = "休息をとりました";
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.ShowDialog();
                }
            }

            we.AlreadyRest = true;
            // [警告]：オブジェクトの参照が全ての場合、クラスにメソッドを用意してそれをコールした方がいい。
            if (mc != null)
            {
                mc.CurrentLife = mc.MaxLife;
                mc.CurrentSkillPoint = mc.MaxSkillPoint;
                mc.CurrentMana = mc.MaxMana;
                mc.AlreadyPlayArchetype = false;
            }
            if (sc != null)
            {
                sc.CurrentLife = sc.MaxLife;
                sc.CurrentSkillPoint = sc.MaxSkillPoint;
                sc.CurrentMana = sc.MaxMana;
                sc.AlreadyPlayArchetype = false;
            }
            if (tc != null)
            {
                tc.CurrentLife = tc.MaxLife;
                tc.CurrentSkillPoint = tc.MaxSkillPoint;
                tc.CurrentMana = tc.MaxMana;
                tc.AlreadyPlayArchetype = false;
            }
            we.AlreadyUseSyperSaintWater = false;
            we.AlreadyUseRevivePotion = false;
            we.AlreadyUsePureWater = false;
            we.AlreadyGetOneDayItem = false;
            we.AlreadyGetMonsterHunt = false;
            we.AlreadyDuelComplete = false;

            this.we.GameDay += 1;
            dayLabel.Text = we.GameDay.ToString() + "日目";

            we.AlreadyCommunicateFazilCastle = false;

            if (noAction == false)
            {
                if (WhoisDuelPlayer() != String.Empty)
                {
                    DuelSupportMessage(SupportType.Begin, WhoisDuelPlayer());
                }
            }
        }

        private void CallEquipmentShop()
        {
            using (TruthEquipmentShop ES = new TruthEquipmentShop())
            {
                ES.StartPosition = FormStartPosition.CenterParent;
                ES.MC = this.mc;
                ES.SC = this.sc;
                ES.TC = this.tc;
                ES.WE = this.we;
                ES.ShowDialog();
            }
        }

        private void CallPotionShop()
        {
            if (we.TruthCompleteArea1) we.AvailablePotion2 = true;
            if (we.TruthCompleteArea2) we.AvailablePotion3 = true;
            if (we.TruthCompleteArea3) we.AvailablePotion4 = true;
            if (we.TruthCompleteArea4) we.AvailablePotion5 = true;

            using (TruthPotionShop PS = new TruthPotionShop())
            {
                PS.StartPosition = FormStartPosition.CenterParent;
                PS.MC = this.mc;
                PS.SC = this.sc;
                PS.TC = this.tc;
                PS.WE = this.we;
                PS.ShowDialog();
            }
        }

        private void CallItemBank()
        {
            using (TruthItemBank tib = new TruthItemBank())
            {
                tib.StartPosition = FormStartPosition.CenterParent;
                tib.MC = this.mc;
                tib.SC = this.sc;
                tib.TC = this.tc;
                tib.WE = this.we;
                tib.ShowDialog();
            }
        }

        private void CallFazilCastle()
        {
            we.AlreadyCommunicateFazilCastle = true;

            this.buttonHanna.Visible = false;
            this.buttonDungeon.Visible = false;
            this.buttonRana.Visible = false;
            this.buttonGanz.Visible = false;
            this.buttonPotion.Visible = false;
            this.buttonDuel.Visible = false;
            this.buttonShinikia.Visible = false;
            ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_FAZIL_CASTLE);

            GroundOne.StopDungeonMusic();
            GroundOne.PlayDungeonMusic(Database.BGM13, Database.BGM13LoopBegin);

            #region "初めての訪問"
            if (!we.Truth_Communication_FC31)
            {
                we.Truth_Communication_FC31 = true;
                UpdateMainMessage("アイン：っとぉ、ファージル宮殿到着っと。");

                UpdateMainMessage("ラナ：アイン、あれを見て！　凄いわ♪");

                UpdateMainMessage("アイン：ん？どれどれ？");

                UpdateMainMessage("アイン：お、おおぉわ！　んだありゃ！！");

                UpdateMainMessage("『　　宮殿前の城門ゲートには、一般市民が行列を生成している　　　』");

                UpdateMainMessage("アイン：おいおい、こんな並んで、一体なにがあるんだよ！？");

                UpdateMainMessage("ラナ：ファージル宮殿名物のリアル相談行列じゃない、知らないの？");

                UpdateMainMessage("アイン：なんだそれ、知るわけが無いだろう。");

                UpdateMainMessage("アイン：で、結局何で並んでるんだ？　教えてくれよ。");

                UpdateMainMessage("ラナ：え、ちょっとホント知らないわけ？ハアァァァ・・・まあ良いけど");

                UpdateMainMessage("ラナ：ファージル宮殿ではエルミ国王およびファラ王妃が民の声に直接耳を傾けるようにしているのよ。");

                UpdateMainMessage("アイン：それで、この行列だってのか！？　一体どんだけ聞いてんだよ！？");
            
                UpdateMainMessage("ラナ：朝方の7:00～12:00。そして12:30～18:00、最後に18:30～22:00までの三部構成ね。");

                UpdateMainMessage("アイン：オイオイオイ、ちょっと待てよ！！　ほとんど休みねえじゃねえか！！");

                UpdateMainMessage("ラナ：それだけ、民の事を念頭に置いているって事よね。正直コレは真似できないわ。");

                UpdateMainMessage("アイン：はあぁ・・・マジかよ・・・ただただ感心するばかりだな。");

                UpdateMainMessage("アイン：ってどうすんだよ？こんなの並んでいたらキリが無いぜ。");

                UpdateMainMessage("ラナ：大丈夫よ。順番に関しては完全に予約制なの。ホラそこに記入リストがあるでしょ♪");

                UpdateMainMessage("アイン：ん？何だそういうのがあるのか。早く言ってくれよ。");

                UpdateMainMessage("アイン：おしっと・・・記入したぜ。");

                UpdateMainMessage("ラナ：この量だとそうね・・・明日の朝方に行くといいわね。");

                UpdateMainMessage("アイン：へぇ、よくそんな正確に分かるな？");

                UpdateMainMessage("ラナ：当たり前じゃない。私結構昔の頃、ここに通うぐらい行ってたんだから♪");

                UpdateMainMessage("アイン：ッゲ、マジかよ！？");
                
                UpdateMainMessage("アイン：ったく、相当気に入ってんだな、エルミ王の事・・・");

                UpdateMainMessage("ラナ：ップ・・・ッププ");
                
                UpdateMainMessage("ラナ：あ～オカシイ、フフフ♪");

                UpdateMainMessage("アイン：っな、何がおかしい？");

                UpdateMainMessage("ラナ：フフフ、なんでも無いわよ♪　っさ、今回はここまでね、一旦帰りましょ♪");

                UpdateMainMessage("アイン：そんなオカシい内容だったか・・・");

                UpdateMainMessage("アイン：まあいい、確かにこれ以上やることもねえ。戻るとするか。");
            }
            #endregion
            #region "謁見開始"
            else if (!we.Truth_Communication_FC32)
            {
                we.Truth_Communication_FC32 = true;

                UpdateMainMessage("アイン：さて、着いたぜ。");

                UpdateMainMessage("アイン：ええと予約順はどれどれ・・・");

                UpdateMainMessage("アイン：っお、本当だ。後少しで俺達の番だな。");

                UpdateMainMessage("ラナ：っでしょ♪");

                UpdateMainMessage("アイン：でも、完全予約制ならここまでして並ぶ必要はねえんじゃねえのか？");

                UpdateMainMessage("ラナ：予約順序で自分の順番が来た時、該当の人が居なかった場合は、今並んでる人が割り込みで謁見する事が許可されてるのよ。");

                UpdateMainMessage("アイン：なるほど、じゃあ重要な要件を抱え込んでる人は、並んでいる方が謁見までの時間が短縮される場合があるって事か。");

                UpdateMainMessage("ラナ：そうね。あと、割り込みが１グループ入るから、その分だけ予約時間帯が大幅にズレる事もなくなるわけよ。");

                UpdateMainMessage("アイン：そこまで計算してのルールってわけか・・・ホントスゴすぎだな・・・");

                UpdateMainMessage("　　【近衛兵：アイン・ウォーレンス！　アイン・ウォーレンスはこの場に居るか！！】");

                UpdateMainMessage("アイン：おっと、呼ばれたみたいだ。行かなくちゃな！");

                UpdateMainMessage("アイン：衛兵のオッサン！俺だオレ！　今そっちに行くぜ！");

                UpdateMainMessage("　　【近衛兵：国王、王妃に対し、失礼の無きよう最善の心得を持って謁見に望まれたし！！】");

                UpdateMainMessage("アイン：っしゃ、了解了解！！");

                UpdateMainMessage("アイン：じゃあ行こうぜ、ラナ。");

                UpdateMainMessage("ラナ：ええ、楽しみよね♪");

                UpdateMainMessage("　　　『　謁見の間にて・・・　』");

                UpdateMainMessage("アイン：へえ・・・意外と普通の部屋だな。もっと豪華絢爛なトコかと思ったが。");

                UpdateMainMessage("ラナ：民との親近感を得るため、意図的にこの部屋の雰囲気を作ってるのよ。");

                UpdateMainMessage("アイン：マジかよ・・・そこまでするのか。");

                UpdateMainMessage("ラナ：っあ、ホラ来たわ！　っ静かに！");

                UpdateMainMessage("アイン：・・・（ドキドキ・・・）");

                UpdateMainMessage("国王エルミ：アイン・ウォーレンスとラナさんだね。よろしく。");

                UpdateMainMessage("王妃ファラ：エルモアの紅茶を煎じておいたわ。良ければどうぞ。");

                UpdateMainMessage("ラナ：あ、ありがとうございます♪　遠慮なく♪");

                UpdateMainMessage("ラナ：エルミ様は、今日も一段とカッコイイですね♪　元気でやってますか？");

                UpdateMainMessage("国王エルミ：ハハハ、ラナさんはいつもそんな調子だな、このとおり元気でやってるよ。");

                UpdateMainMessage("ラナ：ファラ様も、もーホント可愛すぎです。私いつもファラ様を参考にしてるんですよ♪");

                UpdateMainMessage("王妃ファラ：ウフフ、ありがとう。");

                UpdateMainMessage("ラナ：あっ、要件はですね。ソコにボーっと突っ立っているバカアインが言いますので聞いてください♪");

                UpdateMainMessage("アイン：・・・っな・・・");

                UpdateMainMessage("アイン：なんでそんな日常会話っぽいんだよ！？");

                UpdateMainMessage("国王エルミ：民と会話する時は、この調子で喋る方が一番意見を引き出しやすいからね。");

                UpdateMainMessage("ラナ：エルミ様は、一般日常会話に関しては上級クラスの資格を習得してるのよ。ホント凄いわよね。");

                UpdateMainMessage("アイン：そっ・・・そんなのがあるのか・・・");

                UpdateMainMessage("アイン：ってか、やっぱりあれか。お硬いセリフも喋れる上であえて日常会話っぽくしてると・・・？");

                UpdateMainMessage("国王エルミ：まあ、そんな所だね。気にしないで良いよ本当に。");

                UpdateMainMessage("王妃ファラ：ウフフ、では要件をどうぞ、アインさん（＾＾）");

                UpdateMainMessage("アイン：あ、あぁ・・・じ、じゃあええと・・・");

                UpdateMainMessage("ラナ：ッコラ、ちょっと！？　何どぎまぎしてるのよ、もう。");

                UpdateMainMessage("ラナ：ッビシっと要件を言いなさいよね。スパスパっと。");

                UpdateMainMessage("アイン：お、おう。じゃあ、改めて。");

                UpdateMainMessage("アイン：要件は簡単だ。");

                UpdateMainMessage("アイン：討伐の依頼は入ってないか？");

                UpdateMainMessage("国王エルミ：あるよ。それがどうしたんだい？");

                UpdateMainMessage("アイン：出来ればそれを俺達に任せて欲しい。詳細を教えてくれないか？");

                UpdateMainMessage("国王エルミ：構わないよ。やってくれるんなら、大歓迎だ。");

                UpdateMainMessage("国王エルミ：報酬は何にしようか。直接的な収入でいいかい？");

                UpdateMainMessage("アイン：ああ、それが一番助かる。");

                UpdateMainMessage("国王エルミ：それでは、近衛兵に対して、アイン・ウォーレンスの討伐依頼申請受諾権利を認める事を伝えておこう。");

                UpdateMainMessage("王妃ファラ：エルミ。この件なら既に、謁見前に近衛兵サンディに伝えておきましたよ。");

                UpdateMainMessage("国王エルミ：おっと、そういえばそうだったな。助かるよファラ。");

                UpdateMainMessage("アイン：っな！！？　なんで分かってたんだよ！？");

                UpdateMainMessage("ラナ：ッフフ、さすがよね。　だからエルミ様はカッコイイんじゃない♪");

                UpdateMainMessage("アイン：っいやいやいや！　そういう意味で言うトコかよ！？");

                UpdateMainMessage("国王エルミ：謁見の間まで来るという事で、答えはほぼ限られてくる。");

                UpdateMainMessage("国王エルミ：予約キャンセル待ちの列にも並んでないようだし切羽詰まった内容ではないとすると");

                UpdateMainMessage("国王エルミ：雑談か、生活資金源か、または雑多関連という事だし、だいたい目安は付くものなんだよ。");

                UpdateMainMessage("国王エルミ：アイン君は勇猛果敢な性質。　これ自体は前々から耳に届いているよ。");

                UpdateMainMessage("国王エルミ：となると。　答えは分かるよね。");

                UpdateMainMessage("アイン：・・・いやいやいや・・・恐れ入ります・・・");

                UpdateMainMessage("王妃ファラ：でもね。アインさんとラナさんに来ていただいて純粋に嬉しいんですよ、私もエルミも（＾＾）");

                UpdateMainMessage("アイン：いやあ・・・いやいやいや、もったいない言葉だ。ありがとうございます。");

                UpdateMainMessage("ラナ：エルミ様、また遊びに来てもいいですか♪");

                UpdateMainMessage("国王エルミ：もちろんだよ。こんなところで良ければ、何度でも来てくれて構わないよ。");

                UpdateMainMessage("王妃ファラ：お待ちしてますね（＾＾/）");

                UpdateMainMessage("アイン：あぁ・・・また来ます！！！");

                UpdateMainMessage("ラナ：ホーラ、そこで浮かれないの！　ホンットにもう・・・");

                UpdateMainMessage("アイン：じゃあ、本当にありがとうございました。失礼します。");

                UpdateMainMessage("国王エルミ：ああ、またね。");

                UpdateMainMessage("　　　『　城門ゲート前にて・・・　』");

                UpdateMainMessage("アイン：ええと、近衛兵サンディさんは・・・と・・・");

                UpdateMainMessage("　　【近衛兵：アイン・ウォーレンス！　アイン・ウォーレンスはこの場に居るか！！】");

                UpdateMainMessage("アイン：おわっ！！ああっと、ハイハイ。今そっちに行くぜ。");

                UpdateMainMessage("　　【近衛兵：アイン・ウォーレンスに通達する！】");

                UpdateMainMessage("　　【近衛兵：今この時より、アイン・ウォーレンスに討伐依頼申請の受理を行う権利を与える事とする！】");

                UpdateMainMessage("　　【近衛兵：討伐依頼のリストは、この私エガルト・サンディが所持している！！】");

                UpdateMainMessage("　　【近衛兵：リスト内容を見たければ、この私エガルト・サンディを尋ねるとよい！！】");

                UpdateMainMessage("アイン：あっ、あ、ああぁ・・・了解了解！");

                UpdateMainMessage("　　【近衛兵：アイン・ウォーレンスよ！　申したい事があれば何なりと聞くがよい！！】");

                UpdateMainMessage("アイン：あぁ・・・じゃあとりあえず、一つだけ。");

                UpdateMainMessage("アイン：えっと、次からはサンディって呼んでも良いか？");

                UpdateMainMessage("アイン：おーい近衛兵って呼ぶのも何となく変だしな。構わないか？");

                UpdateMainMessage("　　【近衛兵：承知いたした！】");

                UpdateMainMessage("　　【近衛兵：それでは以降、私の事はサンディと呼ぶが良い！！】");

                UpdateMainMessage("アイン：おーし、サンキューサンキュー。じゃあよろしくな！");

                UpdateMainMessage("ラナ：ちょっと、良い感じのトコ悪いんだけど、肝心の討伐依頼リストは見ておかないの？");

                UpdateMainMessage("アイン：ん？ああ、それも大事なんだけどな。今回はひとまずココまでって事にさせてくれ。悪いな。");

                UpdateMainMessage("ラナ：ふうん、そうなんだ。何か、バカアインって本当に変な時があるわね。");

                UpdateMainMessage("アイン：まあまあ、いいじゃねえか。これもちょっとした礼儀の一つさ。");

                UpdateMainMessage("ラナ：・・・それって礼儀になってるわけ？");

                UpdateMainMessage("アイン：じゃあ、ありがとな、サンディ。次また会いに来るから、そん時に討伐リスト見せてくれ！");

                UpdateMainMessage("サンディ：【承知いたした！】");
            }
            #endregion
            else if (!we.AvailableOneDayItem)
            {
                we.AvailableOneDayItem = true;

                UpdateMainMessage("アイン：さて、着いたぜ。");

                UpdateMainMessage("ラナ：あっ、アイン見て見てあっちの方で何か人だかりが出来てるわよ。");

                UpdateMainMessage("アイン：おっ、本当だ。なんかあったのかな？");

                UpdateMainMessage("ラナ：ちょっと行ってみましょ♪");

                UpdateMainMessage("サンディ：【皆の者に伝令事項がある！】");

                UpdateMainMessage("アイン：おっ、サンディだ。今日も元気にやってるな。");

                UpdateMainMessage("ラナ：ッフフ、声が大きいわよね、サンディさん。");

                UpdateMainMessage("アイン：ああ、遠くからでもすげえ耳に残る感じだよな。");

                UpdateMainMessage("サンディ：【本日より！】");

                UpdateMainMessage("サンディ：【ファージル宮殿に赴いた際！】");

                UpdateMainMessage("サンディ：【宮殿正面ゲート前の横通りにて！】");

                UpdateMainMessage("サンディ：【ファージル国王から、全ての民に対して！】");

                UpdateMainMessage("サンディ：【感謝と敬意の念を込め！】");

                UpdateMainMessage("サンディ：【毎日１回ずつ、お楽しみ抽選券を発行する！！！】");

                UpdateMainMessage("アイン：おっ、お楽しみ抽選券！？");

                UpdateMainMessage("ラナ：なんだか、面白そうね♪");

                UpdateMainMessage("サンディ：【抽選で当たるアイテムは粗品から豪華賞品まで様々！】");

                UpdateMainMessage("サンディ：【是非ともご利用されよ！！】");

                UpdateMainMessage("アイン：マジかよ。そいつは嬉しい内容だな。");

                UpdateMainMessage("アイン：実際にはどんな商品が当たるんだ？一覧リストとかあるのかな？");

                UpdateMainMessage("サンディ：【なお、全ての民に応じて、対象賞品は逐一更新され、かつ、その数は膨大！】");

                UpdateMainMessage("サンディ：【ゆえに、賞品リストを公開することは出来ない！】");

                UpdateMainMessage("サンディ：【なにとぞ、ご理解をいただきたい！】");

                UpdateMainMessage("アイン：全ての民に応じて、逐一って・・・すげえな・・・");

                UpdateMainMessage("ラナ：どういう仕組みなのかしら、想像もつかないわね。");

                UpdateMainMessage("アイン：まあ、やってみてからのお楽しみって所か。");

                UpdateMainMessage("ラナ：アイン、超豪華賞品が当たったら、ちゃんと私に頂戴よね♪");

                UpdateMainMessage("アイン：ゲッ・・・そ、それだけは・・・");

                UpdateMainMessage("ラナ：当たるまで、毎日バシバシやって頂戴♪");

                UpdateMainMessage("アイン：いやいやいや・・・");

                UpdateMainMessage("ラナ：決まり♪");

                UpdateMainMessage("アイン：ハ、ハハハ・・・");

                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.Message = "ファージル宮殿で「お楽しみ抽選券」を受け取る事が可能になりました！";
                    md.ShowDialog();
                }
                UpdateMainMessage("", true); 
            }
            #region "何もイベントが無い場合"
            else
            {
                UpdateMainMessage("サンディ：【よくぞ参られた！】", true);

                using (SelectDungeon sd = new SelectDungeon())
                {
                    sd.StartPosition = FormStartPosition.Manual;
                    sd.Location = new Point(this.Location.X + 50, this.Location.Y + 550);
                    sd.MaxSelectable = 3;
                    sd.FirstName = "抽選券";
                    sd.SecondName = "討伐";
                    sd.ThirdName = "あいさつ";
                    if (we.AvailableOneDayItem)
                    {
                        sd.ShowDialog();
                    }
                    else
                    {
                        sd.TargetDungeon = 2;
                    }
                    if (sd.TargetDungeon == 1)
                    {
                        if (!we.AlreadyGetOneDayItem)
                        {
                            UpdateMainMessage("サンディ：【お楽しみ抽選券は正面ゲート向かって右側である！】");

                            UpdateMainMessage("アイン：サンキュー。じゃ行ってくるぜ。");

                            UpdateMainMessage("　・・・　しばらく歩いた後　・・・");

                            if (!we.Truth_FirstOneDayItem)
                            {
                                UpdateMainMessage("アイン：あった、この箱みたいなやつか。");

                                UpdateMainMessage("ラナ：あ、あれじゃないの？");

                                UpdateMainMessage("アイン：お、本当だ！　どれどれ・・・");
                            }
                            else
                            {
                                UpdateMainMessage("アイン：よし、確かこの箱だったな。");
                            }

                            UpdateMainMessage("　【　お楽しみ抽選券をお求めの方は、『発行』ボタンを押してください　】");

                            UpdateMainMessage("アイン：じゃあピっと・・・");

                            UpdateMainMessage("　【　ッガガガガ・・・　】");

                            UpdateMainMessage("　【　ありがとうございます。無事に発行されました　】");

                            if (!we.Truth_FirstOneDayItem)
                            {
                                UpdateMainMessage("アイン：お、おぉ！やったぜ！");
                            }

                            UpdateMainMessage("　【　抽選券を持って、そのまま右へお進みください　】");

                            if (!we.Truth_FirstOneDayItem)
                            {
                                UpdateMainMessage("アイン：っしゃ、次だな！");

                                UpdateMainMessage("ラナ：きっとあれよ。何人か並んでるわ。");

                                UpdateMainMessage("アイン：よし、さっそく並んでみようぜ。");

                                UpdateMainMessage("アイン：・・・なげえな・・・");

                                UpdateMainMessage("ラナ：少し待つしかないわね。");

                                UpdateMainMessage("アイン：ふう・・・");

                                UpdateMainMessage("ラナ：ところで、どっちが券を使うの？");

                                UpdateMainMessage("アイン：いや、それはどっちでも良いだろう。");

                                UpdateMainMessage("ラナ：えー、何言ってんのよバカアイン？　大事なトコじゃないの。");

                                UpdateMainMessage("アイン：いやいやいや、抽選なんだから、誰がやっても同じだろ？");

                                UpdateMainMessage("ラナ：でも、強運の人がやると、立て続けに引き当てる人っているじゃない？");

                                UpdateMainMessage("アイン：確かにたまに居るような、そういう奴は。");

                                UpdateMainMessage("ラナ：でしょ？だから、私かアインのどっちかで、結果が変わるわけよ♪");

                                UpdateMainMessage("アイン：マジか・・・関係ねえ気もするけどなあ・・・");

                                UpdateMainMessage("ラナ：そういうワケだから、どっちが券を使うか決めてちょうだい♪");

                                UpdateMainMessage("アイン：いやいやいや・・・そうだなあ・・・");

                                UpdateMainMessage("アイン：・・・");

                                UpdateMainMessage("アイン：ダメだ、わかんねえ！");

                                UpdateMainMessage("アイン：券を使用する直前で決めよう！！！");

                                UpdateMainMessage("ラナ：えっ、何よそれ。　ちゃんと決めてよね。");

                                UpdateMainMessage("アイン：いやいや、何て言うんだ。決めようが無いぜ。");

                                UpdateMainMessage("アイン：その時その時の直観に頼ろう。っな！？");

                                UpdateMainMessage("ラナ：うーん、何か釈然としないけど・・・");

                                UpdateMainMessage("アイン：おっ、前が開いたぜ！俺たちの番じゃないか？");

                                UpdateMainMessage("ラナ：あ、本当ね。じゃあさっそくやってみましょ♪");

                                UpdateMainMessage("　【　抽選券をシート挿入口に差し込んでください　】");

                                UpdateMainMessage("アイン：よし、じゃあさっそくだが・・・");

                                UpdateMainMessage("ラナ：どっちがやってみる？");
                            }
                            else
                            {
                                UpdateMainMessage("ラナ：ねえ、どっちがやってみる？");
                            }

                            UpdateMainMessage("アイン：そうだなあ、ここは・・・");

                            string newItem = String.Empty;
                            using (TruthDecision td = new TruthDecision())
                            {
                                td.MainMessage = "どちらが抽選券を使いますか？";
                                td.FirstMessage = "アイン";
                                td.SecondMessage = "ラナ";
                                td.StartPosition = FormStartPosition.CenterParent;
                                td.ShowDialog();
                                if (td.DialogResult == System.Windows.Forms.DialogResult.Yes)
                                {
                                    UpdateMainMessage("アイン：おし、俺がやろう");

                                    UpdateMainMessage("ラナ：頑張ってね♪");

                                    UpdateMainMessage("アイン：任せておけ！");
                                    
                                    UpdateMainMessage("　【　抽選券を認識いたしました。　しばらくお待ちください。　】");

                                    UpdateMainMessage("アイン：おし・・・来い！！");
                                }
                                else
                                {
                                    UpdateMainMessage("アイン：ラナ、任せた。");

                                    UpdateMainMessage("ラナ：じゃあ、入れてみるわね。");

                                    UpdateMainMessage("　【　抽選券を認識いたしました。　しばらくお待ちください。　】");

                                    UpdateMainMessage("ラナ：まあ、そんなに期待はしないけど・・・");
                                }
                            }

                            GroundOne.StopDungeonMusic();

                            UpdateMainMessage("　【　結果を発表します　】");

                            UpdateMainMessage("　【　賞品は・・・　】");

                            UpdateMainMessage("　【　・・・　】");

                            UpdateMainMessage("　【　・・・　】");

                            UpdateMainMessage("　【　・・・　】");

                            newItem = Method.GetNewItem(Method.NewItemCategory.Lottery, mc, null, 4);

                            GroundOne.PlaySoundEffect("LvUp.mp3");
                            UpdateMainMessage("　【　＜" + newItem + "＞が当たりました！】");

                            GroundOne.PlayDungeonMusic(Database.BGM13, Database.BGM13LoopBegin);

                            UpdateMainMessage("　【　賞品を転送いたしますので、ボックスから受け取ってください　】");

                            UpdateMainMessage("　【　ッガコン！！！　】");

                            UpdateMainMessage("　【　またご利用ください　】");

                            if (!we.Truth_FirstOneDayItem)
                            {
                                UpdateMainMessage("アイン：すげえ・・・このデッパリ穴から即出てくるのかよ。");

                                UpdateMainMessage("ラナ：どういう仕掛けなのかしら。全アイテムが入ってるようにも思えないし・・・");

                                UpdateMainMessage("アイン：まあ、細かい仕掛けは気にしないでおこう。とにかく貰っておこうぜ！");
                            }
                            else
                            {
                                UpdateMainMessage("アイン：っしゃ、貰っておくぜ！");
                            }

                            CallSomeMessageWithAnimation(newItem + "を手に入れた。");

                            GetItemFullCheck(mc, newItem);

                            if (!we.Truth_FirstOneDayItem)
                            {
                                we.Truth_FirstOneDayItem = true;
                                UpdateMainMessage("アイン：また今度やってみようぜ。");

                                UpdateMainMessage("ラナ：ええ、そうね。");
                            }
                            we.AlreadyGetOneDayItem = true;
                        }
                        else
                        {
                            UpdateMainMessage("サンディ：【お楽しみ抽選券は本日既に発行済となった！】");

                            UpdateMainMessage("アイン：そっか、じゃあまた今度だな。");

                            UpdateMainMessage("サンディ：【また、参られよ！】");
                        }
                    }
                    else if (sd.TargetDungeon == 2)
                    {
                        UpdateMainMessage("アイン：よお、サンディ。良かったら討伐リストを見せてくれないか？");

                        UpdateMainMessage("サンディ：【すまぬが、討伐リストは未だ作られておらぬ！】");

                        UpdateMainMessage("サンディ：【今しばらく待たれよ！】");

                        UpdateMainMessage("アイン：ウゲ・・・じゃあ、しょうがねえ、戻るか・・・");
                        we.AlreadyGetMonsterHunt = true;
                    }
                    else
                    {
                        UpdateMainMessage("サンディ：【また、参られよ！】", true);
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            }
            #endregion

            if (!we.AlreadyRest)
            {
                ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_EVENING);
            }
            else
            {
                ChangeBackgroundData(Database.BaseResourceFolder + Database.BACKGROUND_MORNING);
            }
            this.buttonHanna.Visible = true;
            this.buttonDungeon.Visible = true;
            this.buttonRana.Visible = true;
            this.buttonGanz.Visible = true;
            this.buttonPotion.Visible = true;
            this.buttonDuel.Visible = true;
            this.buttonShinikia.Visible = true;

            GroundOne.StopDungeonMusic();
            GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
        }

        private void GetGreenPotionForLana()
        {
            string potionName = Database.POOR_SMALL_GREEN_POTION;

            if (!we.CompleteArea1 && !we.CompleteArea2 && !we.CompleteArea3 && !we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = Database.POOR_SMALL_GREEN_POTION;
            }
            else if (we.CompleteArea1 && !we.CompleteArea2 && !we.CompleteArea3 && !we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = Database.COMMON_NORMAL_GREEN_POTION;
            }
            else if (we.CompleteArea1 && we.CompleteArea2 && !we.CompleteArea3 && !we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = Database.COMMON_LARGE_GREEN_POTION;
            }
            else if (we.CompleteArea1 && we.CompleteArea2 && we.CompleteArea3 && !we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = Database.COMMON_HUGE_GREEN_POTION; ;
            }
            else if (we.CompleteArea1 && we.CompleteArea2 && we.CompleteArea3 && we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = Database.COMMON_GORGEOUS_GREEN_POTION;
            }
            else
            {
                potionName = Database.POOR_SMALL_GREEN_POTION;
            }
            bool result = mc.AddBackPack(new ItemBackPack(potionName));
            if (!result)
            {
                UpdateMainMessage("アイン：しまった、バックパックがいっぱいだ。何か捨てないとな・・・");
                using (TruthStatusPlayer sp = new TruthStatusPlayer())
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

        private void GetPotionForLana()
        {
            string potionName = Database.POOR_SMALL_RED_POTION;

            if (!we.CompleteArea1 && !we.CompleteArea2 && !we.CompleteArea3 && !we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = Database.POOR_SMALL_RED_POTION;
            }
            else if (we.CompleteArea1 && !we.CompleteArea2 && !we.CompleteArea3 && !we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = Database.COMMON_NORMAL_RED_POTION;
            }
            else if (we.CompleteArea1 && we.CompleteArea2 && !we.CompleteArea3 && !we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = Database.COMMON_LARGE_RED_POTION;
            }
            else if (we.CompleteArea1 && we.CompleteArea2 && we.CompleteArea3 && !we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = Database.COMMON_HUGE_RED_POTION;
            }
            else if (we.CompleteArea1 && we.CompleteArea2 && we.CompleteArea3 && we.CompleteArea4 && !we.CompleteArea5)
            {
                potionName = Database.COMMON_GORGEOUS_RED_POTION;
            }
            else
            {
                potionName = Database.POOR_SMALL_RED_POTION;
            }
            bool result = mc.AddBackPack(new ItemBackPack(potionName));
            if (!result)
            {
                UpdateMainMessage("アイン：しまった、バックパックがいっぱいだ。何か捨てないとな・・・");
                using (TruthStatusPlayer sp = new TruthStatusPlayer())
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
                    sp.WE = this.we;
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

        private string MessageFormatForLana(int num)
        {
            MainCharacter currentPlayer = new MainCharacter();
            currentPlayer.Name = "ラナ";
            switch (num)
            {
                case 1001:
                    if (!we.AvailableSecondCharacter)
                    {
                        return currentPlayer.GetCharacterSentence(num);
                    }
                    else
                    {
                        return currentPlayer.GetCharacterSentence(1003);
                    }

                case 1002:
                    if (!we.AvailableSecondCharacter)
                    {
                        return currentPlayer.GetCharacterSentence(num);
                    }
                    else
                    {
                        return currentPlayer.GetCharacterSentence(1004);
                    }
                default:
                    return currentPlayer.GetCharacterSentence(num);
            }
        }
        
        // ラナのランラン薬品店
        private void button6_Click(object sender, EventArgs e)
        {
            CallPotionShop();
            mainMessage.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {
//            BattleStart(Database.DUEL_SHUVALTZ_FLORE, true);
//            BattleStart(Database.DUEL_KILT_JORJU, false);
            //BattleStart(Database.DUEL_DUMMY_SUBURI, true);
            //BattleStart(Database.DUEL_SINIKIA_KAHLHANZ, true);
            // BattleStart(Database.ENEMY_LAST_RANA_AMILIA, true);
           // BattleStart(Database.ENEMY_LAST_SINIKIA_KAHLHANZ, true);
             //BattleStart(Database.ENEMY_LAST_OL_LANDIS, true);
            BattleStart(Database.DUEL_DUMMY_SUBURI, true);
            //3vs3はこちら
            //BattleStart(Database.DUEL_DUMMY_SUBURI, Database.DUEL_DUMMY_SUBURI, Database.DUEL_DUMMY_SUBURI, Database.RANA_AMILIA, Database.VERZE_ARTIE, false);
//            BattleStart(Database.ENEMY_GENAN_HUNTER, "", "", Database.RANA_AMILIA, "", false);
        }

        private bool BattleStart(string targetName, bool duel)
        {
            return BattleStart(targetName, null, null, null, null, duel);
        }
        private bool BattleStart(string targetName, string targetName2, string targetName3, string allyName2, string allyName3, bool duel)
        {
            GroundOne.StopDungeonMusic();

            bool result = EncountBattle(targetName, targetName2, targetName3, allyName2, allyName3, duel);
            GroundOne.PlayDungeonMusic(Database.BGM01, Database.BGM01LoopBegin);
            return result;
        }

        // DUEL専用のバトルセッティングです。通常戦闘の呼び出しとは随所に差異があります。
        private bool EncountBattle(string enemyName, string enemyName2, string enemyName3, string allyName2, string allyName3, bool duel)
        {
            GroundOne.StopDungeonMusic();

            bool endFlag = false;
            while (!endFlag)
            {
                System.Threading.Thread.Sleep(100);
                using (TruthBattleEnemy be = new TruthBattleEnemy())
                {
                    MainCharacter tempMC = new MainCharacter();
                    MainCharacter tempSC = new MainCharacter();
                    MainCharacter tempTC = new MainCharacter();
                    WorldEnvironment tempWE = new WorldEnvironment();
                    TruthWorldEnvironment tempWE2 = new TruthWorldEnvironment(); // 後編追加

                    tempMC.MainArmor = this.MC.MainArmor;
                    tempMC.SubWeapon = this.MC.SubWeapon;
                    tempMC.MainWeapon = this.MC.MainWeapon;
                    tempMC.Accessory = this.MC.Accessory;
                    tempMC.Accessory2 = this.MC.Accessory2;

                    tempSC.MainArmor = this.SC.MainArmor;
                    tempSC.SubWeapon = this.SC.SubWeapon;
                    tempSC.MainWeapon = this.SC.MainWeapon;
                    tempSC.Accessory = this.SC.Accessory;
                    tempSC.Accessory2 = this.SC.Accessory2;

                    tempTC.MainArmor = this.TC.MainArmor;
                    tempTC.SubWeapon = this.TC.SubWeapon;
                    tempTC.MainWeapon = this.TC.MainWeapon;
                    tempTC.Accessory = this.TC.Accessory;
                    tempTC.Accessory2 = this.TC.Accessory2;

                    ItemBackPack[] tempBackPack = new ItemBackPack[this.MC.GetBackPackInfo().Length];
                    tempBackPack = mc.GetBackPackInfo();
                    be.MC = tempMC;
                    for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
                    {
                        if (tempBackPack[ii] != null)
                        {
                            int stack = tempBackPack[ii].StackValue;
                            for (int jj = 0; jj < stack; jj++)
                            {
                                be.MC.AddBackPack(tempBackPack[ii]);
                            }
                        }
                    }


                    if (allyName2 != null && allyName2 != string.Empty)
                    {
                        ItemBackPack[] tempBackPack2 = new ItemBackPack[this.SC.GetBackPackInfo().Length];
                        tempBackPack2 = sc.GetBackPackInfo();
                        be.SC = tempSC;
                        for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
                        {
                            if (tempBackPack2[ii] != null)
                            {
                                int stack = tempBackPack2[ii].StackValue;
                                for (int jj = 0; jj < stack; jj++)
                                {
                                    be.SC.AddBackPack(tempBackPack2[ii]);
                                }
                            }
                        }
                    }
                    else
                    {
                        be.SC = null;
                    }

                    if (allyName3 != null && allyName3 != string.Empty)
                    {
                        ItemBackPack[] tempBackPack3 = new ItemBackPack[this.TC.GetBackPackInfo().Length];
                        tempBackPack3 = tc.GetBackPackInfo();
                        be.TC = tempTC;
                        for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
                        {
                            if (tempBackPack3[ii] != null)
                            {
                                int stack = tempBackPack3[ii].StackValue;
                                for (int jj = 0; jj < stack; jj++)
                                {
                                    be.TC.AddBackPack(tempBackPack3[ii]);
                                }
                            }
                        }
                    }
                    else
                    {
                        be.TC = null;
                    }


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
                        // s 後編追加
                        else if (pi.PropertyType == typeof(PlayerStance))
                        {
                            try
                            {
                                pi.SetValue(tempMC, (PlayerStance)(Enum.Parse(typeof(PlayerStance), type.GetProperty(pi.Name).GetValue(this.MC, null).ToString())), null);
                                pi.SetValue(tempSC, (PlayerStance)(Enum.Parse(typeof(PlayerStance), type.GetProperty(pi.Name).GetValue(this.SC, null).ToString())), null);
                                pi.SetValue(tempTC, (PlayerStance)(Enum.Parse(typeof(PlayerStance), type.GetProperty(pi.Name).GetValue(this.TC, null).ToString())), null);
                            }
                            catch { }
                        }
                        // e 後編追加
                        // s 後編追加
                        else if (pi.PropertyType == typeof(AdditionalSpellType))
                        {
                            try
                            {
                                pi.SetValue(tempMC, (AdditionalSpellType)(Enum.Parse(typeof(AdditionalSpellType), type.GetProperty(pi.Name).GetValue(this.MC, null).ToString())), null);
                                pi.SetValue(tempSC, (AdditionalSpellType)(Enum.Parse(typeof(AdditionalSpellType), type.GetProperty(pi.Name).GetValue(this.SC, null).ToString())), null);
                                pi.SetValue(tempTC, (AdditionalSpellType)(Enum.Parse(typeof(AdditionalSpellType), type.GetProperty(pi.Name).GetValue(this.TC, null).ToString())), null);
                            }
                            catch { }
                        }
                        else if (pi.PropertyType == typeof(AdditionalSkillType))
                        {
                            try
                            {
                                pi.SetValue(tempMC, (AdditionalSkillType)(Enum.Parse(typeof(AdditionalSkillType), type.GetProperty(pi.Name).GetValue(this.MC, null).ToString())), null);
                                pi.SetValue(tempSC, (AdditionalSkillType)(Enum.Parse(typeof(AdditionalSkillType), type.GetProperty(pi.Name).GetValue(this.SC, null).ToString())), null);
                                pi.SetValue(tempTC, (AdditionalSkillType)(Enum.Parse(typeof(AdditionalSkillType), type.GetProperty(pi.Name).GetValue(this.TC, null).ToString())), null);
                            }
                            catch { }
                        }
                        // e 後編追加
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


                    Type type3 = tempWE2.GetType();
                    foreach (PropertyInfo pi in type3.GetProperties())
                    {
                        // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                        if (pi.PropertyType == typeof(System.Int32))
                        {
                            try
                            {
                                pi.SetValue(tempWE2, (System.Int32)(type3.GetProperty(pi.Name).GetValue(GroundOne.WE2, null)), null);
                            }
                            catch { }
                        }
                        else if (pi.PropertyType == typeof(System.String))
                        {
                            try
                            {
                                pi.SetValue(tempWE2, (string)(type3.GetProperty(pi.Name).GetValue(GroundOne.WE2, null)), null);
                            }
                            catch { }
                        }
                        else if (pi.PropertyType == typeof(System.Boolean))
                        {
                            try
                            {
                                pi.SetValue(tempWE2, (System.Boolean)(type3.GetProperty(pi.Name).GetValue(GroundOne.WE2, null)), null);
                            }
                            catch { }
                        }
                    }

                    TruthEnemyCharacter ec1 = null;
                    TruthEnemyCharacter ec2 = null;
                    TruthEnemyCharacter ec3 = null;
                    if (enemyName != null  && enemyName  != string.Empty) ec1 = new TruthEnemyCharacter(enemyName);
                    if (enemyName2 != null && enemyName2 != string.Empty) ec2 = new TruthEnemyCharacter(enemyName2);
                    if (enemyName3 != null && enemyName3 != string.Empty) ec3 = new TruthEnemyCharacter(enemyName3);
                    be.EC1 = ec1;
                    be.EC2 = ec2;
                    be.EC3 = ec3;
                    be.WE = tempWE;
                    be.StartPosition = FormStartPosition.CenterParent;
                    //be.IgnoreApplicationDoEvent = true;
                    be.DuelMode = duel;
                    be.ShowDialog();
                    if (be.DialogResult == DialogResult.Retry)
                    {
                        // 死亡時、再挑戦する場合、はじめから呼びなおす。
                        this.Update();
                        continue;
                    }
                    if (be.DialogResult == DialogResult.Abort)
                    {
                        // 逃げた時、経験値とゴールドは入らない。
                        this.MC = tempMC;
                        this.MC.ReplaceBackPack(tempMC.GetBackPackInfo());
                        return false;
                    }
                    else if (be.DialogResult == DialogResult.Ignore)
                    {
                        endFlag = true;
                        return false;
                    }
                    else
                    {
                        if (we.AvailableFirstCharacter)
                        {
                            this.MC = tempMC;
                            this.MC.ReplaceBackPack(tempMC.GetBackPackInfo());
                        }
                        return true;
                    }
                }
            }

            return false;
        }


        private void ReConstractWorldEnvironment()
        {
            if (GroundOne.WE2.TruthBadEnd1 && we.TruthCommunicationCompArea1)
            {
                we.TruthCompleteSlayBoss1 = false;
                we.TruthCompleteArea1 = false;
                we.TruthCompleteArea1Day = 0;
                we.TruthCommunicationCompArea1 = false;

                we.dungeonEvent11KeyOpen = false;
                we.dungeonEvent11NotOpen = false;
                we.dungeonEvent12KeyOpen = false;
                we.dungeonEvent12NotOpen = false;
                we.dungeonEvent13KeyOpen = false;
                we.dungeonEvent13NotOpen = false;
                we.dungeonEvent14KeyOpen = false;
                we.dungeonEvent14NotOpen = false;
                we.dungeonEvent15 = false;
                we.dungeonEvent16 = false;
                we.dungeonEvent17 = false;
                we.dungeonEvent18 = false;
                we.dungeonEvent19 = false;
                we.dungeonEvent20 = false;
                we.dungeonEvent21KeyOpen = false;
                we.dungeonEvent21NotOpen = false;
                we.dungeonEvent22KeyOpen = false;
                we.dungeonEvent22NotOpen = false;
                we.dungeonEvent23KeyOpen = false;
                we.dungeonEvent23NotOpen = false;
                we.dungeonEvent24KeyOpen = false;
                we.dungeonEvent24NotOpen = false;
                we.dungeonEvent25 = false;
                we.dungeonEvent26 = false;
                we.dungeonEvent27 = false;
                we.dungeonEvent28KeyOpen = false;
                we.dungeonEvent29 = false;
                we.dungeonEvent30 = false;
                we.dungeonEvent31 = false;

                we.BoardInfo10 = false;
                we.BoardInfo11 = false;
                we.BoardInfo12 = false;
                we.BoardInfo13 = false;
                we.BoardInfo14 = false;

                we.MeetOlLandis = false;
                we.MeetOlLandisBeforeGanz = false;
                we.MeetOlLandisBeforeHanna = false;
                we.MeetOlLandisBeforeLana = false;

                we.AvailableDuelColosseum = false;
                we.AvailableDuelMatch = false;
                we.AvailablePotionshop = false;

                we.Truth_Communication_Dungeon11 = false;
                we.Truth_CommunicationJoinPartyLana = false;
                we.Truth_CommunicationNotJoinLana = false;
                we.Truth_CommunicationFirstHomeTown = false;

                we.Truth_CommunicationLana1_1 = false;

                we.Truth_CommunicationLana1 = false;
                we.Truth_CommunicationLana2 = false;
                we.Truth_CommunicationLana3 = false;
                we.Truth_CommunicationLana4 = false;
                we.Truth_CommunicationLana5 = false;
                we.Truth_CommunicationLana6 = false;
                we.Truth_CommunicationLana7 = false;
                we.Truth_CommunicationLana8 = false;
                we.Truth_CommunicationLana9 = false;
                we.Truth_CommunicationLana10 = false;
                
                we.Truth_CommunicationGanz1 = false;
                we.Truth_CommunicationGanz2 = false;
                we.Truth_CommunicationGanz3 = false;
                we.Truth_CommunicationGanz4 = false;
                we.Truth_CommunicationGanz5 = false;
                we.Truth_CommunicationGanz6 = false;
                we.Truth_CommunicationGanz7 = false;
                we.Truth_CommunicationGanz8 = false;
                we.Truth_CommunicationGanz9 = false;
                we.Truth_CommunicationGanz10 = false;

                we.Truth_CommunicationHanna1 = false;
                we.Truth_CommunicationHanna10 = false;
                we.Truth_CommunicationHanna2 = false;
                we.Truth_CommunicationHanna3 = false;
                we.Truth_CommunicationHanna4 = false;
                we.Truth_CommunicationHanna5 = false;
                we.Truth_CommunicationHanna6 = false;
                we.Truth_CommunicationHanna7 = false;
                we.Truth_CommunicationHanna8 = false;
                we.Truth_CommunicationHanna9 = false;

                we.Truth_GiveLanaEarring = false;

                we.AvailableSecondCharacter = false;

                we.GameDay = 1; // 初めての場合１日目とする。
                we.SaveByDungeon = false; // 初めての場合、町からスタートする。
                we.DungeonPosX = 1 + Database.DUNGEON_BASE_X + (Database.FIRST_POS % Database.TRUTH_DUNGEON_COLUMN) * Database.DUNGEON_MOVE_LEN;
                we.DungeonPosY = 1 + Database.DUNGEON_BASE_Y + (Database.FIRST_POS / Database.TRUTH_DUNGEON_COLUMN) * Database.DUNGEON_MOVE_LEN;
                we.DungeonArea = 1; // 初めての場合ダンジョンからスタートしているため、１とする。
                we.AvailableFirstCharacter = true; // 初めての場合、主人公アインを登録しておく。

                we.AlreadyCommunicate = false;
                we.AlreadyRest = true; // [警告] 後編のストーリー設定上、意図的なTrueだが設計思想としては危ない。

                we.AlreadyUseSyperSaintWater = false;
                we.AlreadyUseRevivePotion = false;
                we.AlreadyUsePureWater = false;
                we.AlreadyGetOneDayItem = false;
                we.AlreadyGetMonsterHunt = false;
                we.AlreadyDuelComplete = false;               

                if (mc != null)
                {
                    mc.CurrentLife = mc.MaxLife;
                    mc.CurrentSkillPoint = mc.MaxSkillPoint;
                    mc.CurrentMana = mc.MaxMana;
                    mc.AlreadyPlayArchetype = false;
                }
                if (sc != null)
                {
                    sc.CurrentLife = sc.MaxLife;
                    sc.CurrentSkillPoint = sc.MaxSkillPoint;
                    sc.CurrentMana = sc.MaxMana;
                    sc.AlreadyPlayArchetype = false;
                }
                if (tc != null)
                {
                    tc.CurrentLife = tc.MaxLife;
                    tc.CurrentSkillPoint = tc.MaxSkillPoint;
                    tc.CurrentMana = tc.MaxMana;
                    tc.AlreadyPlayArchetype = false;
                }

                this.firstDay = 1;
                this.dayLabel.Text = we.GameDay.ToString() + "日目";
            }
        }

        private void GetItemFullCheck(MainCharacter player, string itemName)
        {
            bool result = player.AddBackPack(new ItemBackPack(itemName));
            if (result) return;

            UpdateMainMessage("アイン：しまった、バックパックがいっぱいだ。何か捨てないとな・・・");

            using (TruthStatusPlayer sp = new TruthStatusPlayer())
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
                sp.WE = this.we;
                sp.OnlySelectTrash = true;
                if ((itemName == Database.RARE_EARRING_OF_LANA) ||
                    (itemName == Database.RARE_TOOMI_BLUE_SUISYOU))
                {
                    sp.CannotSelectTrash = itemName;
                }
                sp.StartPosition = FormStartPosition.CenterParent;
                sp.ShowDialog();
                if (this.DialogResult == DialogResult.Cancel)
                {
                }
                else
                {
                    mc = sp.MC;
                    mc.AddBackPack(new ItemBackPack(itemName));
                }
            }
            UpdateMainMessage("", true);
        }

        private void TruthHomeTown_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (GroundOne.sound != null)
            {
                GroundOne.sound.StopMusic();
                //GroundOne.sound.Disactive();
            }
        }

        Image backgroundData = null;
        public static Image AdjustBrightness(Image img, float b)
        {
            //明るさを変更した画像の描画先となるImageオブジェクトを作成
            Bitmap newImg = new Bitmap(img.Width, img.Height);
            //newImgのGraphicsオブジェクトを取得
            Graphics g = Graphics.FromImage(newImg);
            
            float[][] colorMatrixElements = { 
                new float[] {1,    0,    0,    0, 0},
                new float[] {0,    1,    0,    0, 0},
                new float[] {0,    0,    1,    0, 0},
                new float[] {0,    0,    0,    1, 0},
                new float[] {b,    b,    b,    0, 1}};

            //ColorMatrixオブジェクトの作成
            System.Drawing.Imaging.ColorMatrix cm = new System.Drawing.Imaging.ColorMatrix(colorMatrixElements);

            //ImageAttributesオブジェクトの作成
            System.Drawing.Imaging.ImageAttributes ia =
                new System.Drawing.Imaging.ImageAttributes();
            //ColorMatrixを設定する
            ia.SetColorMatrix(cm);

            //ImageAttributesを使用して描画
            g.DrawImage(img,
                new Rectangle(0, 0, img.Width, img.Height),
                0, 0, img.Width, img.Height, GraphicsUnit.Pixel, ia);

            //リソースを解放する
            g.Dispose();

            return newImg;
        }
        private void ChangeBackgroundData(string filename, float darkValue = 0)
        {
            if (filename == null || filename == String.Empty || filename == "")
            {
                this.backgroundData = null;
            }
            else
            {
                Image current = Image.FromFile(filename);
                if (darkValue > 0)
                {
                    System.Drawing.Imaging.ImageAttributes imageAttributes = new System.Drawing.Imaging.ImageAttributes();

                    Image newImg = AdjustBrightness(current, darkValue);
                    this.backgroundData = newImg;
                }
                else
                {
                    this.backgroundData = current;
                }
            }
            this.Invalidate();
        }

        private float GetOpacity(float y)
        {
            float opacity = 0;
            float border_top = 100.0F;
            float border_top2 = 400.0F;
            float border_bottom = 700.0F;
            float border_bottom2 = 768.0F;
            if (y <= border_top)
            {
                opacity = 255;
            }
            else if (border_top < y && y <= border_top2)
            {
                opacity = (border_top2 - y) / (border_top2 - border_top) * 255.0F;
            }
            else if (border_top2 < y && y <= border_bottom)
            {
                opacity = 0;
            }
            else if (border_bottom < y && y <= border_bottom2)
            {
                opacity = (y - border_bottom) / (border_bottom2 - border_bottom) * 255.0F;
            }
            else
            {
                opacity = 255;
            }
            return opacity;
        }
        Font fontA = new System.Drawing.Font("MS UI Gothic", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
        PointF point = new PointF(50, 768);
        List<string> endingText = new List<string>();
        List<string> endingText2 = new List<string>();
        List<string> endingText3 = new List<string>();
        int endingText3Count = 0;
        StringFormat format = new StringFormat();
        SolidBrush b = new SolidBrush(Color.Black);

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            if (GroundOne.WE2.SeekerEnd)
            {
                if (this.backgroundData != null)
                {
                    e.Graphics.DrawImage(this.backgroundData, new Rectangle(0, 0, 1024, 768));
                }
            }
            else if (this.backgroundData != null && GroundOne.WE2.SeekerEndingRoll == false)
            {
                e.Graphics.DrawImage(this.backgroundData, new Rectangle(0, 0, 1024, 768));
            }

            if (!GroundOne.WE2.SeekerEnd && GroundOne.WE2.SeekerEvent1103)
            {
                Graphics g = e.Graphics;
                for (int ii = 0; ii < this.endingText.Count; ii++)
                {
                    float y = point.Y + ii * 50;
                    float opacity = GetOpacity(y);
                    if (opacity < 255)
                    {
                        b.Color = Color.FromArgb(255, (int)opacity, (int)opacity, (int)opacity);
                        g.DrawString(endingText[ii], this.fontA, b, new PointF(point.X, y));
                    }

                    if (ii < endingText2.Count)
                    {
                        float y2 = point.Y + ii * 160;
                        float opacity2 = GetOpacity(y2);
                        if (opacity2 < 255)
                        {
                            b.Color = Color.FromArgb(255, (int)opacity2, (int)opacity2, (int)opacity2);
                            format.Alignment = StringAlignment.Center;
                            g.DrawString(endingText2[ii], this.fontA, b, new RectangleF(512, y2, 512, 1000), format);
                        }
                    }
                }

                for (int ii = 0; ii < this.endingText3.Count; ii++)
                {
                    float opacity3 = 0;
                    if (0 <= this.endingText3Count && this.endingText3Count < 200)
                    {
                        opacity3 = (float)((200 - this.endingText3Count) / 200.0F * 255.0F);
                    }
                    else
                    {
                        opacity3 = (float)((this.endingText3Count - 200) / 400.0F * 255.0F);
                        if (opacity3 > 255) { opacity3 = 255; }
                    }
                    if (opacity3 < 255)
                    {
                        b.Color = Color.FromArgb(255, (int)opacity3, (int)opacity3, (int)opacity3);
                        format.Alignment = StringAlignment.Near;
                        g.DrawString(endingText3[ii], this.fontA, b, new RectangleF(0 + this.endingText3Count / 5, 409, 1024 - this.endingText3Count / 5, 1000), format);
                    }
                    this.endingText3Count++;
                }
            }
        }

        private void buttonLevelup_Click(object sender, EventArgs e)
        {
            mc.Exp += 70000;
            Method.LevelUpCharacter(we, mc, sc, tc, false, System.Drawing.Color.LightSkyBlue);
        }

    }
}