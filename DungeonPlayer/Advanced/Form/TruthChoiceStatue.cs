using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class TruthChoiceStatue : MotherForm
    {
        private MainCharacter mc;
        private MainCharacter sc;
        private MainCharacter tc;
        private WorldEnvironment we;
        private bool[] knownTileInfo;
        private bool[] knownTileInfo2;
        private bool[] knownTileInfo3;
        private bool[] knownTileInfo4;
        private bool[] knownTileInfo5;

        public MainCharacter MC
        {
            get { return mc; }
            set { mc = value; }
        }
        public MainCharacter SC
        {
            get { return sc; }
            set { sc = value; }
        }
        public MainCharacter TC
        {
            get { return tc; }
            set { tc = value; }
        }

        public WorldEnvironment WE
        {
            get { return we; }
            set { we = value; }
        }

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

        protected bool onlySave = false;
        public bool OnlySave
        {
            get { return onlySave; }
            set { onlySave = value; }
        }

        public bool[] Truth_KnownTileInfo { get; set; } // 後編追加
        public bool[] Truth_KnownTileInfo2 { get; set; } // 後編追加
        public bool[] Truth_KnownTileInfo3 { get; set; } // 後編追加
        public bool[] Truth_KnownTileInfo4 { get; set; } // 後編追加
        public bool[] Truth_KnownTileInfo5 { get; set; } // 後編追加
        
        private OKRequest ok;
        private bool flag0 = false;       
        private bool flag1 = false;
        private bool flag2 = false;
        private bool flag3 = false;       
        private bool flagA = false; // ファースト調査中、３か所とも調査済みでTrueになる
        private bool flag4 = false;
        private bool flag4_2 = false;
        private bool flag5 = false;
        private bool flag6 = false;
        private bool flag6_2 = false;
        private bool flag6_3 = false;
        private bool flagB = false; // ファースト選定、ラナの像で決定すればTrue、それ以外はBadEnd
        List<int> factOrder = new List<int>();
        private bool flagC = false; // 事実の順序を選定中、全選択完了でTrueになる
        private bool flagD = false; // 事実選定後、ラナの像で決定すればTrue、それ以外はBadEnd
        private bool flagE = false; // 真実選定前の調査、人間の像を選べばTrue、ラナの像で決定はBadEnd
        private bool flagF = false; // 真実の順序を選定中、全選択完了でTrueになる
        private bool flag7 = false; // 事実選定後、ラナの像で決定すればTrue、それ以外はBadEnd
        List<int> truthOrder = new List<int>();
        private bool flagG = false;
        List<int> elementalOrder = new List<int>();
        private bool flagH = false; // 神剣フェルトゥーシュ８つの光を選定中
        private bool flagI = false; // 神剣選定後、ラナの像を選べばTrue、それ以外はBadEnd
        List<int> songOrder = new List<int>();
        private bool flagJ = false; // 壁画の詩を選定中、全選択完了でTrueになる
        private bool flagK = false; // 壁画選定後、ラナの像を選べばTrue、それ以外はBadEnd

        public TruthChoiceStatue()
        {
            InitializeComponent();

            buttonChoice1.Visible = false;
            buttonChoice2.Visible = false;
            buttonChoice3.Visible = false;
            buttonFact1.Visible = false;
            buttonFact2.Visible = false;
            buttonFact3.Visible = false;
            buttonFact4.Visible = false;
            buttonFact5.Visible = false;
            buttonFact6.Visible = false;
            buttonFact7.Visible = false;
            buttonFact8.Visible = false;
            buttonFact9.Visible = false;
            buttonFact10.Visible = false;
            buttonTruth1.Visible = false;
            buttonTruth2.Visible = false;
            buttonTruth3.Visible = false;
            buttonTruth4.Visible = false;
            buttonTruth5.Visible = false;
            buttonTruth6.Visible = false;
            buttonTruth7.Visible = false;
            buttonTruth8.Visible = false;
            buttonTruth9.Visible = false;
            buttonTruth10.Visible = false;
            buttonElemental1.Visible = false;
            buttonElemental2.Visible = false;
            buttonElemental3.Visible = false;
            buttonElemental4.Visible = false;
            buttonElemental5.Visible = false;
            buttonElemental6.Visible = false;
            buttonElemental7.Visible = false;
            buttonElemental8.Visible = false;
            buttonSong1.Visible = false;
            buttonSong2.Visible = false;
            buttonSong3.Visible = false;
            buttonSong4.Visible = false;
        }

        private void TruthChoiceStatue_Load(object sender, EventArgs e)
        {
            ok = new OKRequest();
            ok.StartPosition = FormStartPosition.Manual;
            ok.Location = new Point(this.Location.X + 904, this.Location.Y + 708);
        }

        private void UpdateMainMessage(string message)
        {
            UpdateMainMessage(message, false);
        }
        private void UpdateMainMessage(string message, bool ignoreOK)
        {
            mainMessage.Text = message;
            mainMessage.Update();
            if (!ignoreOK)
            {
                ok.ShowDialog();
            }
        }

        private void ThisFormAutoSave()
        {
            // 11番のセーブデータを生成し、タイトルへ戻り、「Seeker」モードへ突入させる。
            GroundOne.WE2.SelectFalseStatue = true;
            Method.AutoSaveTruthWorldEnvironment();

            using (SaveLoad sl = new SaveLoad())
            {
                sl.MC = this.MC;
                sl.SC = this.SC;
                sl.TC = this.TC;
                sl.WE = this.WE;
                sl.KnownTileInfo = this.knownTileInfo;
                sl.KnownTileInfo2 = this.knownTileInfo2;
                sl.KnownTileInfo3 = this.knownTileInfo3;
                sl.KnownTileInfo4 = this.knownTileInfo4;
                sl.KnownTileInfo5 = this.knownTileInfo5;
                sl.Truth_KnownTileInfo = this.Truth_KnownTileInfo;
                sl.Truth_KnownTileInfo2 = this.Truth_KnownTileInfo2;
                sl.Truth_KnownTileInfo3 = this.Truth_KnownTileInfo3;
                sl.Truth_KnownTileInfo4 = this.Truth_KnownTileInfo4;
                sl.Truth_KnownTileInfo5 = this.Truth_KnownTileInfo5;
                sl.SaveMode = true;
                sl.RealWorldSave();
            }
        }

        private void NormalEnd_Correct()
        {
            GroundOne.StopDungeonMusic();

            UpdateMainMessage("アイン：（合っているはず、間違いない）");

            UpdateMainMessage("【ッドクン】");

            UpdateMainMessage("＜＜＜　アインの胸の鼓動が急速に高まる ＞＞＞");

            UpdateMainMessage("【ッドクン、ッドクン、ッドクン】");

            UpdateMainMessage("アイン：（行くぜ・・・！）");

            UpdateMainMessage("【　ッズシュ！！！　】");

            UpdateMainMessage("　　（　その瞬間　）");

            UpdateMainMessage("　　（　ラナを示す像は破壊された　）");

            UpdateMainMessage("　　（　その瞬間、光が溢れ出した　）");

            UpdateMainMessage("　　（　光は部屋全体に広がり、視界は全てが白の景色に変換された　）");

            UpdateMainMessage("　　（　ここから先、ダンジョンで何が起きたのか、未だに思い出せないでいる　）");

            UpdateMainMessage("　　（　気がつけば、元の世界に居た　）");

            UpdateMainMessage("　　（　ラナが居る世界　）");

            UpdateMainMessage("　　（　俺はラナと現在一緒に生活している　）");

            UpdateMainMessage("　　（　ラナは早朝、薬草の採取に向かう　）");

            UpdateMainMessage("　　（　午前中はパン屋で手伝いを行い、午後からは薬品店を手伝う　）");

            UpdateMainMessage("　　（　夕方になれば、武術稽古を一通りこなし、母と一緒に夕飯の準備　）");

            UpdateMainMessage("　　（　夜は薬の調合に関する勉強を励んでいる　）");

            UpdateMainMessage("　　（　将来は、自分で薬品店を開きたいそうだ　）");

            UpdateMainMessage("　　（　なんとも忙しい内容だが、ラナのやつは大いに充実してるみたいだ　）");

            UpdateMainMessage("　　（　一方俺は、ダンジョンに明け暮れる毎日　）");

            UpdateMainMessage("　　（　ダンジョンへ赴く理由　）");

            UpdateMainMessage("　　（　それは、稼ぎを得るためだ　）");

            UpdateMainMessage("　　（　ダンジョン内で拾ったアイテムは、街のショップ持っていき換金　）");

            UpdateMainMessage("　　（　使える装備は、チーム分配に則って、頂ける時にもらう　）");

            UpdateMainMessage("　　（　そうやって毎日、資金源を稼ぐ感じだ　）");

            UpdateMainMessage("　　（　ダンジョンの最下層への追求は止める事にした　）");

            UpdateMainMessage("　　（　そのきっかけはもう、思い出せない　）");

            UpdateMainMessage("　　（　ただ強いて言えば　）");

            UpdateMainMessage("　　（　意味が無いんじゃないかという諦めに似た感情が襲ったぐらいだ　）");

            UpdateMainMessage("　　（　最下層を目指さなくても、こうして資金源は得られる　）");

            UpdateMainMessage("　　（　それ以上、無理して進める事はないんじゃないのか　）");

            UpdateMainMessage("　　（　これでいいんだと自分に言い聞かせる　）");

            UpdateMainMessage("　　（　胸の奥底で何かが騒ぐが　）");

            UpdateMainMessage("　　（　それにフタを閉める　）");

            UpdateMainMessage("　　（　ラナがちゃんと一緒に生活してくれている　）");

            UpdateMainMessage("　　（　ボケ師匠もたまに相手してくれる　）");

            UpdateMainMessage("　　（　国王エルミ様や、王妃ファラ様も話を聞いてくれる　）");

            UpdateMainMessage("　　（　魔法に困ったら、カールハンツ先生のとこに行くのも良い　）");

            UpdateMainMessage("　　（　あと、FiveSeekerと言えば誰だっけ・・・　）");

            UpdateMainMessage("　　（　・・・　ズキリと胸が痛む　・・・　）");

            UpdateMainMessage("　　（　気にしちゃ駄目だ。おさえるんだ。　）");

            UpdateMainMessage("　　（　とにかく楽しい良き日々が過ごせる、もうそれだけで十分だ　）");

            UpdateMainMessage("　　（　今日という幸せな日々、そして、明日からもそれが続く　）");

            UpdateMainMessage("　　（　ダンジョンに通うが、ダンジョンの本質は忘れよう　）");

            UpdateMainMessage("　　（　俺はこれからも　）");

            UpdateMainMessage("　　（　ここで、永遠に日常生活を送ろうと思う　）");

            UpdateMainMessage("　　（　・・・　・・・　・・・　）");

            UpdateMainMessage("　　（　・・・　・・・　）");

            UpdateMainMessage("　　（　・・・　）");
            ThisFormAutoSave();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void BadEnd_LanaStatue()
        {
            GroundOne.StopDungeonMusic();

            UpdateMainMessage("アイン：（頼む・・・合っていてくれ・・・）");

            UpdateMainMessage("【ッドクン】");

            UpdateMainMessage("＜＜＜　アインの胸の鼓動が急速に高まる ＞＞＞");

            UpdateMainMessage("【ッドクン、ッドクン、ッドクン】");

            UpdateMainMessage("アイン：（行くぜ・・・！）");

            UpdateMainMessage("【　ッズシュ！！！　】");

            UpdateMainMessage("　　（　その瞬間　）");

            UpdateMainMessage("　　（　ラナを示す像は破壊された　）");

            UpdateMainMessage("　　（　それだけだった　）");

            UpdateMainMessage("　　（　像の破片がこぼれ落ちる音しか聞こえず　）");

            UpdateMainMessage("　　（　ダンジョンの扉は開かなかった　）");

            UpdateMainMessage("　　（　俺はこのダンジョンの中で、取り残された状態になった　）");

            UpdateMainMessage("　　（　出口は一切ない　）");

            UpdateMainMessage("　　（　あるとすれば、唯一　）");

            UpdateMainMessage("　　（　人間を示す像を破壊する事だ　）");

            UpdateMainMessage("　　（　だが、一方が空だったという事はつまり　）");

            UpdateMainMessage("　　（　もう一方には、ラナが閉じ込められているという事になる　）");

            UpdateMainMessage("　　（　その逃げられない事実を前にして　）");

            UpdateMainMessage("　　（　やがて俺は、純粋な願いだけをイメージする様になった　）");

            UpdateMainMessage("　　（　俺はもうここで永遠に閉じ込められてもいい　）");

            UpdateMainMessage("　　（　ただ、ラナだけは助けてやってくれないか　）");

            UpdateMainMessage("　　（　・・・　・・・　・・・　）");

            UpdateMainMessage("　　（　ラナを助けてやってくれ　）");

            UpdateMainMessage("　　（　・・・　・・・　）");

            UpdateMainMessage("　　（　・・・ラナ・・・　）");

            // 現在世界のスタート地点に戻るようセーブする。
            // 次の開始で、ストーリーを示すようにする。
            ThisFormAutoSave();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void BadEnd_HumanStatue()
        {
            GroundOne.StopDungeonMusic();

            UpdateMainMessage("アイン：（頼む・・・合っていてくれ・・・）");

            UpdateMainMessage("【ッドクン】");

            UpdateMainMessage("＜＜＜　アインの胸の鼓動が急速に高まる ＞＞＞");

            UpdateMainMessage("【ッドクン、ッドクン、ッドクン】");

            UpdateMainMessage("アイン：（行くぜ・・・！）");

            UpdateMainMessage("【　ッズシュ！！！　】");

            UpdateMainMessage("　　（　その瞬間　）");

            UpdateMainMessage("　　（　頬に無数の赤い水玉模様が付着した　）");

            UpdateMainMessage("　　（　手には、二つのカーネーションが添えられるような形で血液の線が垂れ　）");

            UpdateMainMessage("　　（　そして足元には、真紅の曼珠沙華が拡がり続け　）");

            UpdateMainMessage("　　（　ラナは絶命した　）");

            UpdateMainMessage("　　（　俺はラナの傍で、あらゆる動作を停止し　）");

            UpdateMainMessage("　　（　ダンジョンの扉が開放の地響きを鳴らし始めた　）");

            UpdateMainMessage("　　（　だが、俺にはどうでもいい事象だった　）");

            UpdateMainMessage("　　（　扉とか最下層なんかどうでもいい、ラナを返してくれ　）");

            UpdateMainMessage("　　（　そのことばかりを考えるようになった　）");

            UpdateMainMessage("　　（　神剣フェルトゥーシュの特性により　）");

            UpdateMainMessage("　　（　ラナの蘇生は不可能　）");

            UpdateMainMessage("　　（　その逃げられない事実を前にして　）");

            UpdateMainMessage("　　（　やがて俺は、泣くことも止め、考えるのも止め　）");

            UpdateMainMessage("　　（　呼吸すらしているかどうかも定かではなくなり　）");

            UpdateMainMessage("　　（　純粋な願いだけをイメージする様になった　）");

            UpdateMainMessage("　　（　俺はもういいから　）");

            UpdateMainMessage("　　（　ラナを助けてくれないか　）");

            UpdateMainMessage("　　（　・・・　・・・　・・・　）");

            UpdateMainMessage("　　（　ラナを助けてやってくれ　）");

            UpdateMainMessage("　　（　・・・　・・・　）");

            UpdateMainMessage("　　（　・・・ラナ・・・　）");

            ThisFormAutoSave();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void BadEnd_Surrender()
        {
            GroundOne.StopDungeonMusic();

            UpdateMainMessage("アイン：（・・・）");

            UpdateMainMessage("アイン：（どうして、こんな事になってしまったんだろう）");

            UpdateMainMessage("アイン：（レギィンアーゼの言葉が頭から離れない）");

            UpdateMainMessage("アイン：（ラナを助ける術はもう残されていない、そんな気がする）");

            UpdateMainMessage("アイン：（像が二つ用意されているが、どちらを選んでも駄目なんじゃないか）");

            UpdateMainMessage("アイン：（そういう事を奴は俺に告げたんだと思う）");

            UpdateMainMessage("アイン：（だとすれば・・・俺が・・・歩むべき道は一つしかない・・・）");

            UpdateMainMessage("アイン：（この神剣フェルトゥーシュを破壊して・・・）");

            UpdateMainMessage("アイン：（もう、このダンジョンへの挑戦は終わりにしよう・・・）");

            UpdateMainMessage("アイン：（ラナが死んだ世界）");

            UpdateMainMessage("アイン：（そして、最下層への到達も出来ぬまま・・・）");

            UpdateMainMessage("アイン：（どこか人里離れた山奥でひっそりと暮らそう）");

            UpdateMainMessage("アイン：（ラナが死んだとしても、俺が忘れなければ良いんだ）");

            UpdateMainMessage("アイン：（俺はラナを絶対に忘れない）");

            UpdateMainMessage("アイン：（俺自身が死ぬまで・・・）");

            UpdateMainMessage("アイン：（絶対に・・・忘れない・・・）");

            UpdateMainMessage(" ～　THE　END　～　（【夢幻】　永遠の停止）");

            // 現在世界のスタート地点に戻るようセーブする。
            ThisFormAutoSave();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void ChangeChoice()
        {
            if (this.flag1 && this.flag2 && this.flag3)
            {
                this.buttonChoice1.Visible = false;
                this.buttonChoice1.Update();
                this.buttonChoice2.Visible = false;
                this.buttonChoice2.Update();
                this.buttonChoice3.Visible = false;
                this.buttonChoice3.Update();
                this.flagA = true;

                UpdateMainMessage("アイン：（選択肢は限られている）");

                this.buttonChoice1.Text = "【人間の像】へ剣を差し込む";
                this.buttonChoice1.Visible = true;
                this.buttonChoice1.Update();
                UpdateMainMessage("アイン：（【人間の像】に剣を差し込むか）");

                this.buttonChoice2.Text = "【ラナの像】へ剣を差し込む";
                this.buttonChoice2.Visible = true;
                this.buttonChoice2.Update();
                UpdateMainMessage("アイン：（【ラナの像】に剣を差し込むか）");

                this.buttonChoice3.Text = "剣を破壊して、引き返す";
                this.buttonChoice3.Visible = true;
                this.buttonChoice3.Update();
                UpdateMainMessage("アイン：（あるいは、剣自体を破壊して引き返すか・・・）");
            }
        }



        // 人間の像
        private void buttonChoice1_Click(object sender, EventArgs e)
        {
            if (!this.flagA)
            {
                if (!this.flag1)
                {
                    this.flag1 = true;
                    UpdateMainMessage("アイン：（人間の容をした像が立っている）");

                    UpdateMainMessage("アイン：（これに剣を差し込めという事なんだろうか・・・）");

                    UpdateMainMessage("アイン：（だが、これに差し込んでもしも・・・）");

                    UpdateMainMessage("アイン：（・・・ッグ・・・）");

                    UpdateMainMessage("アイン：（どうする・・・）");

                    ChangeChoice();
                }
                else
                {
                    UpdateMainMessage("アイン：（万が一を考えると、迂闊に差し込めねえ・・・）");

                    UpdateMainMessage("アイン：（どうする・・・）", true);
                }
            }
            else if (!this.flagB)
            {
                if (!this.flag5)
                {
                    this.flag5 = true;
                    UpdateMainMessage("アイン：（人間を示す像だ・・・）");

                    UpdateMainMessage("アイン：（ラナを示す像なんかに、剣を差し込めるわけがない）");

                    UpdateMainMessage("アイン：（引き返す道も閉ざされている）");

                    UpdateMainMessage("アイン：（次に繋がる道を拓くためには、この像に剣を差し込むしかない）");

                    UpdateMainMessage("アイン：（それしか、手は残されていない、そんな気がする）");
                }
                else
                {
                    BadEnd_HumanStatue();
                }
            }
            else if (!this.flagC)
            {
                UpdateMainMessage("アイン：（今は、台座にあるボタンを順番通りに押そう）", true);
            }
            else if (!this.flagD)
            {
                // とくになし
            }
            else if (!this.flagE)
            {
                this.flagE = true;
                UpdateMainMessage("アイン：（人間の像だ・・・）");

                UpdateMainMessage("アイン：（こっちに差し込むつもりはねえが、ひとまず調べてみるか・・・）");

                UpdateMainMessage("アイン：（・・・　・・・　・・・）");

                UpdateMainMessage("アイン：（あった。やっぱりな・・・）");

                UpdateMainMessage("アイン：（像の背面側の台座だ。フタの形状部分がある）");

                UpdateMainMessage("アイン：（開いてみよう）");

                UpdateMainMessage("アイン：（・・・　・・・　・・・）");

                UpdateMainMessage("アイン：（同じようにボタンがある）");

                UpdateMainMessage("アイン：（合計１０個）");

                buttonTruth1.Visible = true;
                buttonTruth2.Visible = true;
                buttonTruth3.Visible = true;
                buttonTruth4.Visible = true;
                buttonTruth5.Visible = true;
                buttonTruth6.Visible = true;
                buttonTruth7.Visible = true;
                buttonTruth8.Visible = true;
                buttonTruth9.Visible = true;
                buttonTruth10.Visible = true;
                buttonTruth1.Update();
                buttonTruth2.Update();
                buttonTruth3.Update();
                buttonTruth4.Update();
                buttonTruth5.Update();
                buttonTruth6.Update();
                buttonTruth7.Update();
                buttonTruth8.Update();
                buttonTruth9.Update();
                buttonTruth10.Update();

                UpdateMainMessage("アイン：（よし・・・こっちも順番通りに合わせこめばいいはずだ）");

                UpdateMainMessage("アイン：（待ってろラナ・・・）", true);
            }
            else if (!this.flagF)
            {
                UpdateMainMessage("アイン：（今は、台座にあるボタンを順番通りに押そう）", true);
            }
            else if (!this.flagG)
            {
                BadEnd_HumanStatue();
            }
            else if (!this.flagH)
            {
                UpdateMainMessage("アイン：（今は、神剣フェルトゥーシュに集中しよう）", true);
            }
            else if (!this.flagI)
            {
                BadEnd_HumanStatue();
            }
            else if (!this.flagJ)
            {
                UpdateMainMessage("アイン：（今は、壁画の文字列を順番通り手をかざそう）", true);
            }
            else if (!this.flagK)
            {
                BadEnd_HumanStatue();
            }
        }



        // ラナ・アミリアの像
        private void buttonChoice2_Click(object sender, EventArgs e)
        {
            if (!this.flagA)
            {
                if (!this.flag2)
                {
                    this.flag2 = true;

                    UpdateMainMessage("アイン：（これは・・・ラナそっくりじゃねえか）");

                    UpdateMainMessage("アイン：（これに剣を差し込めという事なんだろうか・・・）");

                    UpdateMainMessage("アイン：（無理だ。ただでさえ今までフラッシュバックで嫌なもんを見てる）");

                    UpdateMainMessage("アイン：（この像に剣を突き立てる・・・できるはずがない）");

                    UpdateMainMessage("アイン：（表とか裏とかそんな話じゃなく）");

                    UpdateMainMessage("アイン：（出来ないモノは出来ない）");

                    UpdateMainMessage("アイン：（生理的に無理だ）");

                    UpdateMainMessage("アイン：（どうする・・・）");

                    ChangeChoice();
                }
                else
                {
                    UpdateMainMessage("アイン：（ラナそっくりの像に剣を突き立てるつもりはねえ）");

                    UpdateMainMessage("アイン：（どうする・・・）", true);
                }
            }
            else if (!this.flagB)
            {
                if (!this.flag4)
                {
                    this.flag4 = true;
                    UpdateMainMessage("アイン：（ラナそっくりの像に・・・剣を・・・）");

                    UpdateMainMessage("アイン：（駄目だ。どうしても生理的な拒絶を感じる）");

                    UpdateMainMessage("アイン：（手が・・・震えるんだ）");

                    UpdateMainMessage("アイン：（自分の意志とは関係なく、無意識的に手が震える）");

                    UpdateMainMessage("アイン：（例え、この像に剣を差し込むのが１００％唯一の道だと思っても）");

                    UpdateMainMessage("アイン：（心の底から、恐怖を感じる）");

                    UpdateMainMessage("アイン：（刃を突き立てる姿勢だけでも、震えてしまう）");

                    UpdateMainMessage("アイン：（絶対に俺には無理だ）");

                    UpdateMainMessage("アイン：（どうする・・・）");
                }
                else if (!this.flag4_2)
                {
                    this.flag4_2 = true;
                    UpdateMainMessage("アイン：（【人間の像】に差し込むのが正解なんじゃないか・・・）");

                    UpdateMainMessage("アイン：（どうする・・・）", true);
                }
                else
                {
                    this.flagB = true;
                    UpdateMainMessage("アイン：（・・・いや、違う・・・）");

                    UpdateMainMessage("アイン：（【人間の像】なんて曖昧なものが合っているハズがねえんだ）");

                    UpdateMainMessage("アイン：（そこは直感で何となく分かる。だが）");

                    UpdateMainMessage("アイン：（だからと言って、ラナそっくりの像に・・・）");

                    UpdateMainMessage("アイン：（無理だ、突き刺せない）");

                    UpdateMainMessage("アイン：（・・・おちつけ・・・）");

                    UpdateMainMessage("アイン：（おちつけ・・・）");

                    UpdateMainMessage("アイン：（おちつくんだ）");

                    UpdateMainMessage("アイン：（・・・　・・・　・・・）");

                    UpdateMainMessage("アイン：（レギィンのヤツは、『貴公は既に解を得ている』と言っていた）");

                    UpdateMainMessage("アイン：（言葉通りなんじゃないか）");

                    UpdateMainMessage("アイン：（絶対に何かあるはずだ）");

                    UpdateMainMessage("アイン：（もっと、よく調べるんだ）");

                    UpdateMainMessage("アイン：（・・・　・・・　・・・）");

                    UpdateMainMessage("アイン：（あった・・・像の台座部分にうっすらと）");

                    UpdateMainMessage("アイン：（フタの形状をした部分がある）");

                    UpdateMainMessage("アイン：（開いてみよう）");

                    UpdateMainMessage("アイン：（・・・　・・・　・・・）");

                    UpdateMainMessage("アイン：（ボタンがある）");

                    UpdateMainMessage("アイン：（合計１０個）");

                    UpdateMainMessage("アイン：（それぞれに何か書いてある）");

                    buttonFact1.Visible = true;
                    buttonFact2.Visible = true;
                    buttonFact3.Visible = true;
                    buttonFact4.Visible = true;
                    buttonFact5.Visible = true;
                    buttonFact6.Visible = true;
                    buttonFact7.Visible = true;
                    buttonFact8.Visible = true;
                    buttonFact9.Visible = true;
                    buttonFact10.Visible = true;
                    buttonFact1.Update();
                    buttonFact2.Update();
                    buttonFact3.Update();
                    buttonFact4.Update();
                    buttonFact5.Update();
                    buttonFact6.Update();
                    buttonFact7.Update();
                    buttonFact8.Update();
                    buttonFact9.Update();
                    buttonFact10.Update();

                    UpdateMainMessage("アイン：（これは・・・）");

                    UpdateMainMessage("アイン：（・・・よし・・・多分これを順番通りに合わせこめばいいんだ）");

                    UpdateMainMessage("アイン：（待ってろラナ・・・今、助ける）");
                }
            }
            else if (!this.flagC)
            {
                UpdateMainMessage("アイン：（今は、台座にあるボタンを順番通りに押そう）", true);
            }
            else if (!this.flagD)
            {
                UpdateMainMessage("アイン：（これで良いんだろうか・・・）");

                UpdateMainMessage("アイン：（ボタンは確かに順番通りに押したはずだ）");

                UpdateMainMessage("アイン：（だが、その行為に意味はあったんだろうか）");

                UpdateMainMessage("アイン：（レギィンのヤツが言った事を、勝手に解釈しているだけだ）");

                UpdateMainMessage("アイン：（ラナが助かる保証は・・・どこにもない・・・）");

                if (!CheckFactOrder())
                {
                    BadEnd_LanaStatue();
                }
                else
                {
                    this.flagD = true;
                    UpdateMainMessage("アイン：（頼む・・・合っていてくれ・・・）");

                    UpdateMainMessage("【ッドクン】");

                    UpdateMainMessage("＜＜＜　アインの胸の鼓動が急速に高まる ＞＞＞");

                    UpdateMainMessage("【ッドクン、ッドクン、ッドクン】");

                    UpdateMainMessage("アイン：（あっ！！！）");

                    UpdateMainMessage("アイン：（ま、まて！！　落ち着け・・・）");

                    UpdateMainMessage("アイン：（４階を進める際・・・）");

                    UpdateMainMessage("アイン：（事実を示す事と、もう一つ看板から示されていた内容がある）");

                    UpdateMainMessage("アイン：（まだ・・・何かあるはずだ）");

                    UpdateMainMessage("アイン：（早とちりせず、探すんだ）");
                }
            }
            else if (!this.flagE)
            {
                if (!this.flag7)
                {
                    this.flag7 = true;
                    UpdateMainMessage("アイン：（・・・特にもう何も無さそうだな）");

                    UpdateMainMessage("アイン：（何かまだあると思ったが・・・気のせいだったのか）");
                    
                    UpdateMainMessage("アイン：（このまま、突き刺せば良いんだろうか・・・）");

                    UpdateMainMessage("アイン：（どうする・・・）");
                }
                else
                {
                    BadEnd_LanaStatue();
                }
            }
            else if (!this.flagF)
            {
                UpdateMainMessage("アイン：（今は、台座にあるボタンを順番通りに押そう）", true);
            }
            else if (!this.flagG)
            {
                UpdateMainMessage("アイン：（事実ではなく、真実を示す部分・・・）");

                UpdateMainMessage("アイン：（４階で見てきた看板の通り、ボタンを設定したはずだ）");

                if (!CheckTruthOrder())
                {
                    BadEnd_LanaStatue();
                }
                else
                {
                    this.flagG = true;
                    UpdateMainMessage("アイン：（頼む・・・合っていてくれ・・・）");

                    UpdateMainMessage("【ッドクン】");

                    UpdateMainMessage("＜＜＜　アインの胸の鼓動が急速に高まる ＞＞＞");

                    UpdateMainMessage("【ッドクン、ッドクン、ッドクン】");

                    UpdateMainMessage("　　（　その時、意識的ではなかったが　）");

                    UpdateMainMessage("　　（　極度の集中により、全神経が剣の切っ先にいった時　）");

                    UpdateMainMessage("　　（　視界にある文字列が、とっさに飛び込んできた　）");

                    UpdateMainMessage("アイン：（切っ先に・・・何か見える！？）");

                    UpdateMainMessage("アイン：（なんだこれ・・・何かの文字か？）");

                    UpdateMainMessage("アイン：（今まで全然知らなかったぜ・・・）");

                    UpdateMainMessage("アイン：（・・・よく見ると文字の部分がうっすらと光を放っている）");

                    UpdateMainMessage("アイン：（この暗い通路の中でようやく見えるって感じだ・・・それで見えたのか）");

                    UpdateMainMessage("アイン：（よし、剣の表面を入念に調べてみよう）");

                    UpdateMainMessage("アイン：（・・・　・・・　・・・）");

                    UpdateMainMessage("アイン：（全部で合計８つ）");

                    buttonElemental1.Visible = true;
                    buttonElemental2.Visible = true;
                    buttonElemental3.Visible = true;
                    buttonElemental4.Visible = true;
                    buttonElemental5.Visible = true;
                    buttonElemental6.Visible = true;
                    buttonElemental7.Visible = true;
                    buttonElemental8.Visible = true;
                    buttonElemental1.Update();
                    buttonElemental2.Update();
                    buttonElemental3.Update();
                    buttonElemental4.Update();
                    buttonElemental5.Update();
                    buttonElemental6.Update();
                    buttonElemental7.Update();
                    buttonElemental8.Update();

                    UpdateMainMessage("アイン：（４階の看板に、この手の繋がる内容は無かったと思うが・・・）");

                    UpdateMainMessage("アイン：（・・・いや、当てて見せる）");

                    UpdateMainMessage("アイン：（今ここで、当てなきゃダメなんだ）");

                    UpdateMainMessage("アイン：（待ってろラナ）");
                }
            }
            else if (!this.flagH)
            {
                UpdateMainMessage("アイン：（今は、神剣フェルトゥーシュに集中しよう）", true);
            }
            else if (!this.flagI)
            {
                this.flagI = true;
                UpdateMainMessage("アイン：（剣全体が光っている・・・）");

                UpdateMainMessage("アイン：（よし、これで突き刺せば・・・）");

                UpdateMainMessage("　　（　アインは、ゆっくりと剣を振りかざし始めた　）");

                UpdateMainMessage("　　（　その時だった　）");
                
                UpdateMainMessage("アイン：（ん・・・）");
                
                UpdateMainMessage("アイン：（壁に・・・何か見える）");

                UpdateMainMessage("アイン：（剣の光のおかげでやっと見える感じだ）");
                
                UpdateMainMessage("アイン：（何で今まで気づかなかったんだ・・・）");

                UpdateMainMessage("アイン：（２つの像と神剣の存在が強烈すぎて壁を見る余裕がなかったせいもあるが・・・）");

                UpdateMainMessage("アイン：（ともかく、壁に近づいてみよう）");

                UpdateMainMessage("アイン：（・・・　・・・　・・・）");

                UpdateMainMessage("アイン：（これは・・・抽象画か）");

                UpdateMainMessage("アイン：（・・・あっ）");

                UpdateMainMessage("アイン：（文字列だ。小さい文字で何かが連なっている）");

                UpdateMainMessage("アイン：（４方向の壁全体に書いてありそうだ。くまなく調べてみよう。）");

                UpdateMainMessage("アイン：（・・・　・・・　・・・）");

                UpdateMainMessage("アイン：（それぞれの壁に一つずつ、合計４つか）");

                buttonSong1.Visible = true;
                buttonSong2.Visible = true;
                buttonSong3.Visible = true;
                buttonSong4.Visible = true;
                buttonSong1.Update();
                buttonSong2.Update();
                buttonSong3.Update();
                buttonSong4.Update();

                UpdateMainMessage("アイン：（いよいよ・・・最終局面の様相だな）");

                UpdateMainMessage("アイン：（これはさすがに本当の最後だと直感できる）");

                UpdateMainMessage("アイン：（ラナ・・・もうすぐだ、待ってろ）");
            }
            else if (!this.flagJ)
            {
                UpdateMainMessage("アイン：（今は、壁画の文字列を順番通り手をかざそう）", true);
            }
            else if (!this.flagK)
            {
                NormalEnd_Correct();
            }
        }



        // フェルトゥーシュの剣
        private void buttonChoice3_Click(object sender, EventArgs e)
        {
            if (!this.flagA)
            {
                if (!this.flag3)
                {
                    this.flag3 = true;

                    UpdateMainMessage("アイン：（間違いねえ・・・フェルトゥーシュだ）");

                    UpdateMainMessage("アイン：（何だってこんな所で出てくるんだ）");

                    UpdateMainMessage("アイン：（・・・これを使って、どちらかの像へ・・・）");

                    UpdateMainMessage("アイン：（どうする・・・）");

                    ChangeChoice();
                }
                else
                {
                    UpdateMainMessage("アイン：（・・・これを使って、どちらかの像へ・・・）");

                    UpdateMainMessage("アイン：（どうする・・・）", true);
                }
            }
            else if (!this.flagB)
            {
                if (!this.flag6)
                {
                    this.flag6 = true;
                    UpdateMainMessage("アイン：（引き返す・・・どこへ？）");

                    UpdateMainMessage("アイン：（ラナはこのダンジョンに囚われたままなんだ）");

                    UpdateMainMessage("アイン：（囚われているラナを放ってはおけない）");

                    UpdateMainMessage("アイン：（必ず、救ってみせる）");
                }
                else if (!this.flag6_2)
                {
                    this.flag6_2 = true;
                    UpdateMainMessage("アイン：（引き返した所で意味はねえ・・・）");

                    UpdateMainMessage("アイン：（絶対に救ってみせる）", true);
                }
                else if (!this.flag6_3)
                {
                    this.flag6_3 = true;
                    UpdateMainMessage("アイン：（絶対に・・・救うんだ）", true);
                }
                else
                {
                    BadEnd_Surrender();
                }
            }
            else if (!this.flagC)
            {
                UpdateMainMessage("アイン：（今は、台座にあるボタンを順番通りに押そう）", true);
            }
            else if (!this.flagD)
            {
                BadEnd_Surrender();
            }
            else if (!this.flagE)
            {
                UpdateMainMessage("アイン：（囚われているラナを放ってはおけない）");

                UpdateMainMessage("アイン：（必ず、救ってみせる）", true);
            }
            else if (!this.flagF)
            {
                UpdateMainMessage("アイン：（今は、台座にあるボタンを順番通りに押そう）", true);
            }
            else if (!this.flagG)
            {
                BadEnd_Surrender();
            }
            else if (!this.flagH)
            {
                UpdateMainMessage("アイン：（剣の一部が光を放っている・・・順序は・・・）", true);
            }
            else if (!this.flagI)
            {
                BadEnd_Surrender();
            }
            else if (!this.flagJ)
            {
                UpdateMainMessage("アイン：（今は、壁画の文字列を順番通り手をかざそう）", true);
            }
            else if (!this.flagK)
            {
                BadEnd_Surrender();
            }
        }


        private void buttonChoice4_Click(object sender, EventArgs e)
        {
            if (!this.flag0)
            {
                this.flag0 = true;
                buttonChoice4.Visible = false;
                buttonChoice4.Update();

                UpdateMainMessage("アイン：（・・・ッグ！！）");

                UpdateMainMessage("アイン：（何か聞こえる・・・頭に・・・直接・・・だ、誰だ！？）");

                UpdateMainMessage("レギィン：我が名は闇と焔を司りし者、レギィン・アーゼ。");

                UpdateMainMessage("レギィン：このダンジョンにて理を守りし者。");

                UpdateMainMessage("アイン：ラナをどこへやった。");

                UpdateMainMessage("レギィン：貴公は何ゆえ、歩を進める。答えよ。");

                UpdateMainMessage("アイン：問答に付き合う暇はねえんだ。ラナをどこへやったんだ。");

                UpdateMainMessage("レギィン：貴公のいう、ラナ・アミリアという存在は");

                UpdateMainMessage("レギィン：既に死を遂げたも同然である。");

                UpdateMainMessage("アイン：まだ生存してるって事なんだな？");

                UpdateMainMessage("レギィン：些末なこと。事象の覆りは起こらない。");

                UpdateMainMessage("アイン：そんな事は、やってみなくちゃわからないだろ？");

                UpdateMainMessage("レギィン：貴公はこれまで何度も実施を試みている、すべては同一の事象となる。");

                UpdateMainMessage("アイン：・・・何の話だ？");

                UpdateMainMessage("レギィン：貴公の眼前にある、一つの剣を見よ。");

                UpdateMainMessage("アイン：・・・剣？");

                buttonChoice3.Visible = true;
                buttonChoice3.Update();

                UpdateMainMessage("アイン：こ・・・これは");

                UpdateMainMessage("アイン：神剣フェルトゥーシュか！？");

                UpdateMainMessage("レギィン：貴公自身が自ら手放した剣である。");

                UpdateMainMessage("アイン：俺自身が・・・自ら？");

                UpdateMainMessage("レギィン：このダンジョンは、対象者の願望によりその性質が形成される。");

                UpdateMainMessage("レギィン：そしてその願望は、最終的には叶えられる。");

                UpdateMainMessage("レギィン：だが、それには犠牲が必要である。");

                UpdateMainMessage("レギィン：犠牲の対象となるは、いずれか二つ。");

                buttonChoice1.Visible = true;
                buttonChoice1.Update();
                UpdateMainMessage("レギィン：一つは【人間を示す像】");

                buttonChoice2.Visible = true;
                buttonChoice2.Update();
                UpdateMainMessage("レギィン：そしてもう一つは、【ラナ・アミリアを示す像】");

                UpdateMainMessage("アイン：ラナ！！！");

                UpdateMainMessage("レギィン：貴公はいずれかに神剣フェルトゥーシュを差し込むのが務め。");

                UpdateMainMessage("レギィン：正しき方へ剣を突き刺すがよい。アイン・ウォーレンスよ。");

                UpdateMainMessage("レギィン：正しき方を選択すれば、道は拓かれる。");

                UpdateMainMessage("レギィン：この【理】を認識せよ、アイン・ウォーレンスよ。");

                UpdateMainMessage("アイン：ッグ・・・");

                UpdateMainMessage("レギィン：己の心そのものが映し出されるこの場において");

                UpdateMainMessage("レギィン：アイン・ウォーレンス、貴公は既に解を得ているはず。");

                UpdateMainMessage("レギィン：されど、これまで何度も実施を試みた貴公の選択意志。");
                
                UpdateMainMessage("レギィン：結果はすべて等しく同じ。");

                UpdateMainMessage("アイン：毎回間違った方を選んでいるとでも言うつもりかよ？");

                UpdateMainMessage("レギィン：愚かなり、アイン・ウォーレンス。");

                UpdateMainMessage("レギィン：全ては貴公次第。");

                UpdateMainMessage("レギィン：何度も言おう。貴公は既に解を得ている。");

                UpdateMainMessage("レギィン：だが、それを選択せず、何故過ちへと歩を進めるか。");

                UpdateMainMessage("レギィン：愚かなり");

                UpdateMainMessage("レギィン：・・・愚かなり、アイン・ウォーレンスよ・・・");

                UpdateMainMessage("　　【　レギィン・アーゼは再び空中へ飛散していった　】");

                UpdateMainMessage("アイン：（・・・っくそ・・・惑わされるな・・・）");

                UpdateMainMessage("アイン：（まずは、差し込む前に調べるんだ・・・）");
            }

        }

        private bool CheckFactOrder()
        {
            if (this.factOrder[0] != 1) { return false; }
            if (this.factOrder[1] != 2) { return false; }
            if (this.factOrder[2] != 3) { return false; }
            if (this.factOrder[3] != 4) { return false; }
            if (this.factOrder[4] != 5) { return false; }
            if (this.factOrder[5] != 6) { return false; }
            if (this.factOrder[6] != 7) { return false; }
            if (this.factOrder[7] != 8) { return false; }
            if (this.factOrder[8] != 9) { return false; }
            if (this.factOrder[9] != 10) { return false; }

            return true;
        }

        private bool CheckTruthOrder()
        {
            if (this.truthOrder[0] != 1) { return false; }
            if (this.truthOrder[1] != 2) { return false; }
            if (this.truthOrder[2] != 3) { return false; }
            if (this.truthOrder[3] != 4) { return false; }
            if (this.truthOrder[4] != 5) { return false; }
            if (this.truthOrder[5] != 6) { return false; }
            if (this.truthOrder[6] != 7) { return false; }
            if (this.truthOrder[7] != 8) { return false; }
            if (this.truthOrder[8] != 9) { return false; }
            if (this.truthOrder[9] != 10) { return false; }

            return true;
        }

        private bool CheckElementalOrder()
        {
            if (this.elementalOrder[0] != 1) { return false; }
            if (this.elementalOrder[1] != 2) { return false; }
            if (this.elementalOrder[2] != 3) { return false; }
            if (this.elementalOrder[3] != 4) { return false; }
            if (this.elementalOrder[4] != 5) { return false; }
            if (this.elementalOrder[5] != 6) { return false; }
            if (this.elementalOrder[6] != 7) { return false; }
            if (this.elementalOrder[7] != 8) { return false; }

            return true;
        }

        private bool CheckSongOrder()
        {
            if (this.songOrder[0] != 1) { return false; }
            if (this.songOrder[1] != 2) { return false; }
            if (this.songOrder[2] != 3) { return false; }
            if (this.songOrder[3] != 4) { return false; }

            return true;
        }

        private void buttonFact_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Enabled = false;
            btn.Update();
            if (btn.Equals(buttonFact1)) { this.factOrder.Add(1); }
            if (btn.Equals(buttonFact2)) { this.factOrder.Add(2); }
            if (btn.Equals(buttonFact3)) { this.factOrder.Add(3); }
            if (btn.Equals(buttonFact4)) { this.factOrder.Add(4); }
            if (btn.Equals(buttonFact5)) { this.factOrder.Add(5); }
            if (btn.Equals(buttonFact6)) { this.factOrder.Add(6); }
            if (btn.Equals(buttonFact7)) { this.factOrder.Add(7); }
            if (btn.Equals(buttonFact8)) { this.factOrder.Add(8); }
            if (btn.Equals(buttonFact9)) { this.factOrder.Add(9); }
            if (btn.Equals(buttonFact10)) { this.factOrder.Add(10); }

            if (this.factOrder.Count >= 10)
            {
                this.flagC = true;
                UpdateMainMessage("アイン：（どうだ・・・これで順番に押せたはずだが）");

                UpdateMainMessage("アイン：（特に物音はしないが・・・）");

                UpdateMainMessage("アイン：（やるべき事はやったはずだ）");

                UpdateMainMessage("アイン：（どうする・・・）");
            }
        }

        private void buttonTruth_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Enabled = false;
            btn.Update();
            if (btn.Equals(buttonTruth1)) { this.truthOrder.Add(1); }
            if (btn.Equals(buttonTruth2)) { this.truthOrder.Add(2); }
            if (btn.Equals(buttonTruth3)) { this.truthOrder.Add(3); }
            if (btn.Equals(buttonTruth4)) { this.truthOrder.Add(4); }
            if (btn.Equals(buttonTruth5)) { this.truthOrder.Add(5); }
            if (btn.Equals(buttonTruth6)) { this.truthOrder.Add(6); }
            if (btn.Equals(buttonTruth7)) { this.truthOrder.Add(7); }
            if (btn.Equals(buttonTruth8)) { this.truthOrder.Add(8); }
            if (btn.Equals(buttonTruth9)) { this.truthOrder.Add(9); }
            if (btn.Equals(buttonTruth10)) { this.truthOrder.Add(10); }

            if (this.truthOrder.Count >= 10)
            {
                this.flagF = true;
                UpdateMainMessage("アイン：（これで・・・全部揃えたはずだ）");

                UpdateMainMessage("アイン：（物音は特にしないか・・・）");

                UpdateMainMessage("アイン：（どうする・・・）");
            }
        }

        private void buttonElemental_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Enabled = false;
            btn.Update();
            if (btn.Equals(buttonElemental1)) { this.elementalOrder.Add(1); }
            if (btn.Equals(buttonElemental2)) { this.elementalOrder.Add(2); }
            if (btn.Equals(buttonElemental3)) { this.elementalOrder.Add(3); }
            if (btn.Equals(buttonElemental4)) { this.elementalOrder.Add(4); }
            if (btn.Equals(buttonElemental5)) { this.elementalOrder.Add(5); }
            if (btn.Equals(buttonElemental6)) { this.elementalOrder.Add(6); }
            if (btn.Equals(buttonElemental7)) { this.elementalOrder.Add(7); }
            if (btn.Equals(buttonElemental8)) { this.elementalOrder.Add(8); }

            if (this.elementalOrder.Count >= 8)
            {
                this.flagH = true;
                if (CheckElementalOrder())
                {
                    UpdateMainMessage("アイン：（剣全体が光り始めた！！！）");

                    UpdateMainMessage("アイン：（すげえ眩しい・・・）");

                    UpdateMainMessage("アイン：（よし、っこれなら行けるかもしれねえ！）");
                }
                else
                {
                    UpdateMainMessage("アイン：（・・・特に変化はないか・・・）");

                    UpdateMainMessage("アイン：（どうする・・・）");
                }
            }
        }

        private void buttonSong_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.Enabled = false;
            btn.Update();
            if (btn.Equals(buttonSong1)) { this.songOrder.Add(1); }
            if (btn.Equals(buttonSong2)) { this.songOrder.Add(2); }
            if (btn.Equals(buttonSong3)) { this.songOrder.Add(3); }
            if (btn.Equals(buttonSong4)) { this.songOrder.Add(4); }

            if (this.songOrder.Count >= 4)
            {
                this.flagJ = true;
                if (CheckSongOrder())
                {
                    UpdateMainMessage("　　（　アインが手をかざし終えた瞬間、【光の線】が発生し始めた！　）");

                    UpdateMainMessage("アイン：（すげえな・・・仕掛けだらけだったんだこの部屋・・・）");

                    UpdateMainMessage("アイン：（左右の壁にある文字が繋がる様に一本）");

                    UpdateMainMessage("アイン：（あと、前方後方の壁文字からも同様に連結する形で一本発生している）");

                    UpdateMainMessage("アイン：（で、綺麗に一直線のラインが２本発生している）");

                    UpdateMainMessage("アイン：（って事は、２本がクロスするポイントがあるはずだ）");

                    UpdateMainMessage("アイン：（・・・クロスポイントは・・・）");

                    UpdateMainMessage("アイン：（やっぱり・・・【ラナの像】だ）");

                    UpdateMainMessage("アイン：（間違いない）");

                    UpdateMainMessage("アイン：（キッチリ調べて正解だったんだ）");

                    UpdateMainMessage("アイン：（何も考えず、動揺と不安で闇雲に選んでたらきっと失敗してた）");

                    UpdateMainMessage("アイン：（というより）");
                    
                    UpdateMainMessage("アイン：（何も解放させずに単に差し込んだら駄目なんだろう、きっと）");

                    UpdateMainMessage("アイン：（これで終わりだ）");

                    UpdateMainMessage("アイン：（今度こそ大丈夫だ）");
                }
                else
                {
                    UpdateMainMessage("アイン：（・・・特に変化はないか・・・）");

                    UpdateMainMessage("アイン：（どうする・・・）");
                }
            }
        }
    }
}
