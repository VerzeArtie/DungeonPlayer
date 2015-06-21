using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class TruthRequestFood : MotherForm // 後編編集
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
        public string CurrentSelect { get; set; }

        public TruthRequestFood()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.CurrentSelect == Database.FOOD_BIFUKATU)
            {
                EatFood(5, 0, 0, 5, 0);
            }
            else if (this.CurrentSelect == Database.FOOD_INAGO)
            {
                EatFood(0, 0, 5, 0, 5);
            }
            else if (this.CurrentSelect == Database.FOOD_USAGI)
            {
                EatFood(5, 5, 0, 0, 0);
            }
            else if (this.CurrentSelect == Database.FOOD_GEKIKARA_CURRY)
            {
                EatFood(0, 0, 0, 5, 5);
            }
            else if (this.CurrentSelect == Database.FOOD_SANMA)
            {
                EatFood(0, 5, 5, 0, 0);
            }
            // ２階
            else if (this.CurrentSelect == Database.FOOD_FISH_GURATAN)
            {
                EatFood(0, 30, 0, 20, 0);
            }
            else if (this.CurrentSelect == Database.FOOD_SEA_TENPURA)
            {
                EatFood(20, 0, 0, 30, 0);
            }
            else if (this.CurrentSelect == Database.FOOD_TRUTH_YAMINABE_1)
            {
                EatFood(0, 0, 20, 0, 30);
            }
            else if (this.CurrentSelect == Database.FOOD_OSAKANA_ZINGISKAN)
            {
                EatFood(30, 0, 0, 0, 20);
            }
            else if (this.CurrentSelect == Database.FOOD_RED_HOT_SPAGHETTI)
            {
                EatFood(0, 0, 30, 20, 0);
            }
            // ３階
            else if (this.CurrentSelect == Database.FOOD_HINYARI_YASAI)
            {
                EatFood(0, 0, 80, 0, 60);
            }
            else if (this.CurrentSelect == Database.FOOD_AZARASI_SHIOYAKI)
            {
                EatFood(0, 80, 60, 60, 0);
            }
            else if (this.CurrentSelect == Database.FOOD_WINTER_BEEF_CURRY)
            {
                EatFood(80, 50, 0, 50, 50);
            }
            else if (this.CurrentSelect == Database.FOOD_GATTURI_GOZEN)
            {
                EatFood(60, 60, 60, 60, 60);
            }
            else if (this.CurrentSelect == Database.FOOD_KOGOERU_DESSERT)
            {
                EatFood(70, 0, 100, 0, 120);
            }
            // ４階
            else if (this.CurrentSelect == Database.FOOD_BLACK_BUTTER_SPAGHETTI)
            {
                EatFood( 0 , 250, 250,  0 , 250);
            }
            else if (this.CurrentSelect == Database.FOOD_KOROKORO_PIENUS_HAMBURG)
            {
                EatFood(250,  0 ,  0 , 250, 250);
            }
            else if (this.CurrentSelect == Database.FOOD_PIRIKARA_HATIMITSU_STEAK)
            {
                EatFood(250,  0 , 250, 250,  0 );
            }
            else if (this.CurrentSelect == Database.FOOD_HUNWARI_ORANGE_TOAST)
            {
                EatFood( 0 , 250, 250, 250,  0 );
            }
            else if (this.CurrentSelect == Database.FOOD_TRUTH_YAMINABE_2)
            {
                EatFood(250, 250,  0 ,  0 , 250);
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void EatFood(int strUp, int aglUp, int intUp, int stmUp, int mindUp)
        {
            List<MainCharacter> group = new List<MainCharacter>();
            if (mc != null && we.AvailableFirstCharacter) group.Add(mc);
            if (sc != null && we.AvailableSecondCharacter) group.Add(sc);
            if (tc != null && we.AvailableThirdCharacter) group.Add(tc);
            for (int ii = 0; ii < group.Count; ii++)
            {
                group[ii].BuffStrength_Food = strUp;
                group[ii].BuffAgility_Food = aglUp;
                group[ii].BuffIntelligence_Food = intUp;
                group[ii].BuffStamina_Food = stmUp;
                group[ii].BuffMind_Food = mindUp;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (buttonList1.Text == Database.FOOD_BIFUKATU)
            {
                description.Text = "　『" + Database.FOOD_BIFUKATU + "』\r\n\r\nボリュームたっぷりで味も申し分が無いと好評。\r\n\r\n食べた次の日は、以下の効果。\r\n 力＋５\r\n 体＋５";
                this.CurrentSelect = Database.FOOD_BIFUKATU;
            }
            else if (buttonList1.Text == Database.FOOD_FISH_GURATAN)
            {
                description.Text = "　『" + Database.FOOD_FISH_GURATAN + "』\r\n\r\n新鮮な魚介類の素材を細切りにして散りばめてあるグラタン。\r\n\r\n食べた次の日は、以下の効果。\r\n 技＋３０\r\n 体＋２０";
                this.CurrentSelect = Database.FOOD_FISH_GURATAN;
            }
            else if (buttonList1.Text == Database.FOOD_HINYARI_YASAI)
            {
                description.Text = "　『" + Database.FOOD_HINYARI_YASAI + "』\r\n\r\nカリっと天ぷら粉で焼き上げた野菜天ぷら。\r\n野菜であることを忘れてしまうぐらい、非常に香ばしい食感が残る。\r\n\r\n食べた次の日は、以下の効果。\r\n 知＋８０\r\n 心＋６０";
                this.CurrentSelect = Database.FOOD_HINYARI_YASAI;
            }
            else if (buttonList1.Text == Database.FOOD_BLACK_BUTTER_SPAGHETTI)
            {
                description.Text = "　『" + Database.FOOD_BLACK_BUTTER_SPAGHETTI + "』\r\n\r\n真っ黒な色のスパゲッティ\r\n見た目がかなり不気味だが・・・スパイスの効いた香りがする。\r\n\r\n食べた次の日は、以下の効果。\r\n 技＋２５０\r\n 知＋２５０\r\n 心＋２５０";
                this.CurrentSelect = Database.FOOD_BLACK_BUTTER_SPAGHETTI;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (buttonList2.Text == Database.FOOD_GEKIKARA_CURRY)
            {
                description.Text = "　『" + Database.FOOD_GEKIKARA_CURRY + "』\r\n\r\nか・・・辛い！！でもウマイ！！\r\n　実はハンナが客に応じて辛い配分を全調整してるとの事。\r\n\r\n食べた次の日は、以下の効果。\r\n 体＋５\r\n 心＋５";
                this.CurrentSelect = Database.FOOD_GEKIKARA_CURRY;
            }
            else if (buttonList2.Text == Database.FOOD_SEA_TENPURA)
            {
                description.Text = "　『" + Database.FOOD_SEA_TENPURA + "』\r\n\r\nクチバシや舌の独特さを完全に除去し、質の高いテンプラに仕立ててある。\r\n\r\n食べた次の日は、以下の効果。\r\n 力＋２０\r\n 体＋３０";
                this.CurrentSelect = Database.FOOD_SEA_TENPURA;
            }
            else if (buttonList2.Text == Database.FOOD_AZARASI_SHIOYAKI)
            {
                description.Text = "　『" + Database.FOOD_AZARASI_SHIOYAKI + "』\r\n\r\n固くて歯ごたえの悪いアザラシ肉を十分にほぐし、凍らせた後、焼き、塩をまぶした究極の一品\r\n\r\n食べた次の日は、以下の効果。\r\n 技＋８０\r\n 知＋６０\r\n 体＋６０";
                this.CurrentSelect = Database.FOOD_AZARASI_SHIOYAKI;
            }
            else if (buttonList2.Text == Database.FOOD_KOROKORO_PIENUS_HAMBURG)
            {
                description.Text = "　『" + Database.FOOD_KOROKORO_PIENUS_HAMBURG + "』\r\n\r\nハンバーグの中に小さめに切り刻んだピーナッツが入っている\r\nフワフワとしたジューシーな肉とカリっとしたピーナッツが食欲をそそる。\r\n\r\n食べた次の日は、以下の効果。\r\n 力＋２５０\r\n 体＋２５０\r\n 心＋２５０";
                this.CurrentSelect = Database.FOOD_KOROKORO_PIENUS_HAMBURG;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (buttonList3.Text == Database.FOOD_INAGO)
            {
                description.Text = "　『" + Database.FOOD_INAGO + "』\r\n\r\n味自体が非常に絶妙で、歯ごたえも非常に良い。問題はその見た目だが・・・。\r\n\r\n食べた次の日は、以下の効果。\r\n 知＋５\r\n 心＋５";
                this.CurrentSelect = Database.FOOD_INAGO;
            }
            else if (buttonList3.Text == Database.FOOD_TRUTH_YAMINABE_1)
            {
                description.Text = "　『" + Database.FOOD_TRUTH_YAMINABE_1 + "』\r\n\r\n真実は闇の中にこそ潜む。味だけは保証されてるらしい・・・。\r\n\r\n食べた次の日は、以下の効果。\r\n 知＋２０\r\n 心＋３０";
                this.CurrentSelect = Database.FOOD_TRUTH_YAMINABE_1;
            }
            else if (buttonList3.Text == Database.FOOD_WINTER_BEEF_CURRY)
            {
                description.Text = "　『" + Database.FOOD_WINTER_BEEF_CURRY + "』\r\n\r\n冬の季節、急激な温度変化により身が引き締まったビーフを使用したカレーライス。臭みは一切感じない。\r\n\r\n食べた次の日は、以下の効果。\r\n 力＋８０\r\n 技＋５０\r\n 体＋５０\r\n 心＋５０";
                this.CurrentSelect = Database.FOOD_WINTER_BEEF_CURRY;
            }
            else if (buttonList3.Text == Database.FOOD_PIRIKARA_HATIMITSU_STEAK)
            {
                description.Text = "　『" + Database.FOOD_PIRIKARA_HATIMITSU_STEAK + "』\r\n\r\n表面に真っ赤なトウガラシがかけられているヒレステーキ。\r\nその裏には実はほんのりとハチミツが隠し味として入っており、食べた者には辛さと甘さが同時に響き渡る\r\n\r\n食べた次の日は、以下の効果。\r\n 力＋２５０\r\n 知＋２５０\r\n 体＋２５０";
                this.CurrentSelect = Database.FOOD_PIRIKARA_HATIMITSU_STEAK;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (buttonList4.Text == Database.FOOD_USAGI)
            {
                description.Text = "　『" + Database.FOOD_USAGI + "』\r\n\r\nウサギ独特の臭みを無くし、肉の旨みは残してある。\r\n\r\n食べた次の日は、以下の効果。\r\n 力＋５\r\n 技＋５";
                this.CurrentSelect = Database.FOOD_USAGI;
            }
            else if (buttonList4.Text == Database.FOOD_OSAKANA_ZINGISKAN)
            {
                description.Text = "　『" + Database.FOOD_OSAKANA_ZINGISKAN + "』\r\n\r\n魚とは思えないような歯ごたえのあるジンギスカン。\r\n\r\n食べた次の日は、以下の効果。\r\n 力＋３０\r\n 心＋２０";
                this.CurrentSelect = Database.FOOD_OSAKANA_ZINGISKAN;
            }
            else if (buttonList4.Text == Database.FOOD_GATTURI_GOZEN)
            {
                description.Text = "　『" + Database.FOOD_GATTURI_GOZEN + "』\r\n\r\n肉、魚、豆、味噌汁、ご飯、煎茶。全てが揃ったバランスの良い定食。\r\nハンナおばさん自慢の定食。\r\n\r\n食べた次の日は、以下の効果。\r\n 力＋６０\r\n 技＋６０\r\n 知＋６０\r\n 体＋６０\r\n 心＋６０";
                this.CurrentSelect = Database.FOOD_GATTURI_GOZEN;
            }
            else if (buttonList4.Text == Database.FOOD_HUNWARI_ORANGE_TOAST)
            {
                description.Text = "　『" + Database.FOOD_HUNWARI_ORANGE_TOAST + "』\r\n\r\n朝１番のトースト定食といえば、このオレンジトースト。\r\nふんだんに塗られたオレンジジャムとホワイトクリームを乗せたバカでかいトーストは男女問わず人気の一品である。\r\n\r\n食べた次の日は、以下の効果。\r\n 技＋２５０\r\n 知＋２５０\r\n 体＋２５０";
                this.CurrentSelect = Database.FOOD_HUNWARI_ORANGE_TOAST;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (buttonList5.Text == Database.FOOD_SANMA)
            {
                description.Text = "  『" + Database.FOOD_SANMA + "』\r\n\r\n魚本来の味を引き出しており、かつ、煮物と非常にマッチしてる。\r\n\r\n食べた次の日は、以下の効果。\r\n 技＋５\r\n 知＋５";
                this.CurrentSelect = Database.FOOD_SANMA;
            }
            else if (buttonList5.Text == Database.FOOD_RED_HOT_SPAGHETTI)
            {
                description.Text = "  『" + Database.FOOD_RED_HOT_SPAGHETTI + "』\r\n真っ赤なスパゲッティだが、実は全然辛く無いらしい。\r\n　素材の原色を駆使し、着色は一切行ってないそうだ。\r\n\r\n食べた次の日は、以下の効果。\r\n 知＋３０\r\n 体＋２０";
                this.CurrentSelect = Database.FOOD_RED_HOT_SPAGHETTI;
            }
            else if (buttonList5.Text == Database.FOOD_KOGOERU_DESSERT)
            {
                description.Text = "  『" + Database.FOOD_KOGOERU_DESSERT + "』\r\n何という青さ・・・見ただけで凍えてしまいそうだ。\r\n　食べた時の口いっぱいに広がる感触は一級品のデザートそのものである。\r\n\r\n食べた次の日は、以下の効果。\r\n 力＋７０\r\n 知＋１００\r\n 心＋１２０";
                this.CurrentSelect = Database.FOOD_KOGOERU_DESSERT;
            }
            else if (buttonList5.Text == Database.FOOD_TRUTH_YAMINABE_2)
            {
                description.Text = "  『" + Database.FOOD_TRUTH_YAMINABE_2 + "』\r\n食物の匂いが全くしない闇の鍋\r\n　ハンナ叔母さん曰く、美味しいモノはちゃんと入っているとの事。それを信じて食べるしか選択肢は無い。\r\n\r\n食べた次の日は、以下の効果。\r\n 力＋２５０\r\n 技＋２５０\r\n 心＋２５０";
                this.CurrentSelect = Database.FOOD_TRUTH_YAMINABE_2;
            }
        }

        private void TruthRequestFood_Shown(object sender, EventArgs e)
        {
            #region "１階"
            if (!GroundOne.WE2.FoodAvailable_11 && (GroundOne.WE2.FoodMixtureDay_11 != 0) && (we.GameDay > GroundOne.WE2.FoodMixtureDay_11))
            {
                GroundOne.WE2.FoodAvailable_11 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.FOOD_INAGO;
                    TID.ItemNameTitle = Database.FOOD_INAGO;
                    TID.ItemNameButtonSentence = "が、追加されました！";
                    TID.Description = "味自体が非常に絶妙で、歯ごたえも非常に良い。問題はその見た目だが・・・。食べた次の日は、知＋５、心＋５の効果が与えられる。";
                    TID.ShowDialog();
                }
                buttonList3.Visible = true;
            }
            if (!GroundOne.WE2.FoodAvailable_12 && (GroundOne.WE2.FoodMixtureDay_12 != 0) && (we.GameDay > GroundOne.WE2.FoodMixtureDay_12))
            {
                GroundOne.WE2.FoodAvailable_12 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.FOOD_USAGI;
                    TID.ItemNameTitle = Database.FOOD_USAGI;
                    TID.ItemNameButtonSentence = "が、追加されました！";
                    TID.Description = "ウサギ独特の臭みを無くし、肉の旨みは残してある。食べた次の日は、力＋５、技＋５の効果が与えられる。";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.FoodAvailable_13 && (GroundOne.WE2.FoodMixtureDay_13 != 0) && (we.GameDay > GroundOne.WE2.FoodMixtureDay_13))
            {
                GroundOne.WE2.FoodAvailable_13 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.FOOD_SANMA;
                    TID.ItemNameTitle = Database.FOOD_SANMA;
                    TID.ItemNameButtonSentence = "が、追加されました！";
                    TID.Description = "魚本来の味を引き出しており、かつ、煮物と非常にマッチしてる。食べた次の日は、技＋５、知＋５の効果が与えられる。";
                    TID.ShowDialog();
                }
            }
            #endregion
            #region "２階"
            if (!GroundOne.WE2.FoodAvailable_21 && (GroundOne.WE2.FoodMixtureDay_21 != 0) && (we.GameDay > GroundOne.WE2.FoodMixtureDay_21))
            {
                GroundOne.WE2.FoodAvailable_21 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.FOOD_SEA_TENPURA;
                    TID.ItemNameTitle = Database.FOOD_SEA_TENPURA;
                    TID.ItemNameButtonSentence = "が、追加されました！";
                    TID.Description = "クチバシや舌の独特さを完全に除去し、質の高いテンプラに仕立ててある。食べた次の日は、技＋３０、知＋２０の効果が与えられる。";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.FoodAvailable_22 && (GroundOne.WE2.FoodMixtureDay_22 != 0) && (we.GameDay > GroundOne.WE2.FoodMixtureDay_22))
            {
                GroundOne.WE2.FoodAvailable_22 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.FOOD_TRUTH_YAMINABE_1;
                    TID.ItemNameTitle = Database.FOOD_TRUTH_YAMINABE_1;
                    TID.ItemNameButtonSentence = "が、追加されました！";
                    TID.Description = "真実は闇の中にこそ潜む。味だけは保証されてるらしい・・・。食べた次の日は、知＋２０、心＋３０の効果が与えられる。";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.FoodAvailable_23 && (GroundOne.WE2.FoodMixtureDay_23 != 0) && (we.GameDay > GroundOne.WE2.FoodMixtureDay_23))
            {
                GroundOne.WE2.FoodAvailable_23 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.FOOD_OSAKANA_ZINGISKAN;
                    TID.ItemNameTitle = Database.FOOD_OSAKANA_ZINGISKAN;
                    TID.ItemNameButtonSentence = "が、追加されました！";
                    TID.Description = "魚とは思えないような歯ごたえのあるジンギスカン。食べた次の日は、力＋３０、知＋２０の効果が与えられる。";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.FoodAvailable_24 && (GroundOne.WE2.FoodMixtureDay_24 != 0) && (we.GameDay > GroundOne.WE2.FoodMixtureDay_24))
            {
                GroundOne.WE2.FoodAvailable_24 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.FOOD_RED_HOT_SPAGHETTI;
                    TID.ItemNameTitle = Database.FOOD_RED_HOT_SPAGHETTI;
                    TID.ItemNameButtonSentence = "が、追加されました！";
                    TID.Description = "真っ赤なスパゲッティだが、実は全然辛く無いらしい。素材の原色を駆使し、着色は一切行ってないそうだ。食べた次の日は、知＋３０、体＋２０の効果が与えられる。";
                    TID.ShowDialog();
                }
            }
            #endregion
            #region "３階"
            if (!GroundOne.WE2.FoodAvailable_31 && (GroundOne.WE2.FoodMixtureDay_31 != 0) && (we.GameDay > GroundOne.WE2.FoodMixtureDay_31))
            {
                GroundOne.WE2.FoodAvailable_31 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.FOOD_AZARASI_SHIOYAKI;
                    TID.ItemNameTitle = Database.FOOD_AZARASI_SHIOYAKI;
                    TID.ItemNameButtonSentence = "が、追加されました！";
                    TID.Description = "固くて歯ごたえの悪いアザラシ肉を十分にほぐし、凍らせた後、焼き、塩をまぶした究極の一品。食べた次の日は、技＋５０、知＋４０、体＋６０の効果が与えられる。";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.FoodAvailable_32 && (GroundOne.WE2.FoodMixtureDay_32 != 0) && (we.GameDay > GroundOne.WE2.FoodMixtureDay_32))
            {
                GroundOne.WE2.FoodAvailable_32 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.FOOD_WINTER_BEEF_CURRY;
                    TID.ItemNameTitle = Database.FOOD_WINTER_BEEF_CURRY;
                    TID.ItemNameButtonSentence = "が、追加されました！";
                    TID.Description = "冬の季節、急激な温度変化により身が引き締まったビーフを使用したカレーライス。臭みは一切感じない。食べた次の日は、力＋６０、技＋４０、体＋２５、心＋２５の効果が与えられる。";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.FoodAvailable_33 && (GroundOne.WE2.FoodMixtureDay_33 != 0) && (we.GameDay > GroundOne.WE2.FoodMixtureDay_33))
            {
                GroundOne.WE2.FoodAvailable_33 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.FOOD_GATTURI_GOZEN;
                    TID.ItemNameTitle = Database.FOOD_GATTURI_GOZEN;
                    TID.ItemNameButtonSentence = "が、追加されました！";
                    TID.Description = "肉、魚、豆、味噌汁、ご飯、煎茶。全てが揃ったバランスの良い定食。ハンナおばさん自慢の定食。食べた次の日は、力＋４０、技＋４０、知＋４０、体＋４０、心＋４０の効果が与えられる。";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.FoodAvailable_34 && (GroundOne.WE2.FoodMixtureDay_34 != 0) && (we.GameDay > GroundOne.WE2.FoodMixtureDay_34))
            {
                GroundOne.WE2.FoodAvailable_34 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.FOOD_KOGOERU_DESSERT;
                    TID.ItemNameTitle = Database.FOOD_KOGOERU_DESSERT;
                    TID.ItemNameButtonSentence = "が、追加されました！";
                    TID.Description = "何という青さ・・・見ただけで凍えてしまいそうだ。食べた時の口いっぱいに広がる感触は一級品のデザートそのものである。食べた次の日は、力＋３０、技＋３０、知＋３０、心＋１００";
                    TID.ShowDialog();
                }
            }
            #region "４階"
            if (!GroundOne.WE2.FoodAvailable_41 && (GroundOne.WE2.FoodMixtureDay_41 != 0) && (we.GameDay > GroundOne.WE2.FoodMixtureDay_41))
            {
                GroundOne.WE2.FoodAvailable_41 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.FOOD_KOROKORO_PIENUS_HAMBURG;
                    TID.ItemNameTitle = Database.FOOD_KOROKORO_PIENUS_HAMBURG;
                    TID.ItemNameButtonSentence = "が、追加されました！";
                    TID.Description = "ハンバーグの中に小さめに切り刻んだピーナッツが入っている。フワフワとしたジューシーな肉とカリっとしたピーナッツが食欲をそそる。食べた次の日は、力＋２５０、体＋２５０、心＋２５０";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.FoodAvailable_42 && (GroundOne.WE2.FoodMixtureDay_42 != 0) && (we.GameDay > GroundOne.WE2.FoodMixtureDay_42))
            {
                GroundOne.WE2.FoodAvailable_42 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.FOOD_PIRIKARA_HATIMITSU_STEAK;
                    TID.ItemNameTitle = Database.FOOD_PIRIKARA_HATIMITSU_STEAK;
                    TID.ItemNameButtonSentence = "が、追加されました！";
                    TID.Description = "表面に真っ赤なトウガラシがかけられているヒレステーキ。その裏には実はほんのりとハチミツが隠し味として入っており、食べた者には辛さと甘さが同時に響き渡る。食べた次の日は、力＋２５０、知＋２５０、体＋２５０";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.FoodAvailable_43 && (GroundOne.WE2.FoodMixtureDay_43 != 0) && (we.GameDay > GroundOne.WE2.FoodMixtureDay_43))
            {
                GroundOne.WE2.FoodAvailable_43 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.FOOD_HUNWARI_ORANGE_TOAST;
                    TID.ItemNameTitle = Database.FOOD_HUNWARI_ORANGE_TOAST;
                    TID.ItemNameButtonSentence = "が、追加されました！";
                    TID.Description = "朝１番のトースト定食といえば、このオレンジトースト。ふんだんに塗られたオレンジジャムとホワイトクリームを乗せたバカでかいトーストは男女問わず人気の一品である。食べた次の日は、技＋２５０、知＋２５０、体＋２５０";
                    TID.ShowDialog();
                }
            }
            if (!GroundOne.WE2.FoodAvailable_44 && (GroundOne.WE2.FoodMixtureDay_44 != 0) && (we.GameDay > GroundOne.WE2.FoodMixtureDay_44))
            {
                GroundOne.WE2.FoodAvailable_44 = true;
                using (TruthItemDesc TID = new TruthItemDesc())
                {
                    TID.StartPosition = FormStartPosition.CenterParent;
                    TID.ItemNameButton = Database.FOOD_TRUTH_YAMINABE_2;
                    TID.ItemNameTitle = Database.FOOD_TRUTH_YAMINABE_2;
                    TID.ItemNameButtonSentence = "が、追加されました！";
                    TID.Description = "食物の匂いが全くしない闇の鍋。ハンナ叔母さん曰く、美味しいモノはちゃんと入っているとの事。それを信じて食べるしか選択肢は無い。食べた次の日は、力＋２５０、技＋２５０、心＋２５０";
                    TID.ShowDialog();
                }
            }
            #endregion
            #endregion

            if (we.AvailableFood5) SetupAvailableList(5);
            else if (we.AvailableFood4) SetupAvailableList(4);
            else if (we.AvailableFood3) SetupAvailableList(3);
            else if (we.AvailableFood2) SetupAvailableList(2);
            else SetupAvailableList(1);

            button2_Click(null, null);
        }

        private void btnLevel1_Click(object sender, EventArgs e)
        {
            SetupAvailableList(1);
        }

        private void btnLevel2_Click(object sender, EventArgs e)
        {

            SetupAvailableList(2);
        }

        private void btnLevel3_Click(object sender, EventArgs e)
        {
            SetupAvailableList(3);
        }

        private void btnLevel4_Click(object sender, EventArgs e)
        {
            SetupAvailableList(4);
        }

        private void btnLevel5_Click(object sender, EventArgs e)
        {
            SetupAvailableList(5);
        }

        private void TruthRequestFood_Load(object sender, EventArgs e)
        {
            if (we.TruthCompleteArea1) we.AvailableFood2 = true;
            if (we.TruthCompleteArea2) we.AvailableFood3 = true;
            if (we.TruthCompleteArea3) we.AvailableFood4 = true;
            if (we.TruthCompleteArea4) we.AvailableFood5 = true;

            if (!we.AvailableFood2)
            {
                SetupAvailableList(1);

                btnLevel1.Visible = false; // [コメント]：最初は武具種類が増える傾向を見せない演出のため、VisibleはFalse
                btnLevel2.Visible = false;
                btnLevel3.Visible = false;
                btnLevel4.Visible = false;
                btnLevel5.Visible = false;
            }
            else if (we.AvailableFood2 && !we.AvailableFood3)
            {
                SetupAvailableList(2);
                btnLevel1.Visible = true;
                btnLevel2.Visible = true;
                btnLevel3.Visible = false;
                btnLevel4.Visible = false;
                btnLevel5.Visible = false;
            }
            else if (we.AvailableFood2 && we.AvailableFood3 && !we.AvailableFood4)
            {
                SetupAvailableList(3);
                btnLevel1.Visible = true;
                btnLevel2.Visible = true;
                btnLevel3.Visible = true;
                btnLevel4.Visible = false;
                btnLevel5.Visible = false;
            }
            else if (we.AvailableFood2 && we.AvailableFood3 && we.AvailableFood4 && !we.AvailableEquipShop5)
            {
                SetupAvailableList(4);
                btnLevel1.Visible = true;
                btnLevel2.Visible = true;
                btnLevel3.Visible = true;
                btnLevel4.Visible = true;
                btnLevel5.Visible = false;
            }
            else if (we.AvailableFood2 && we.AvailableFood3 && we.AvailableFood4 && we.AvailableEquipShop5)
            {
                SetupAvailableList(5);
                btnLevel1.Visible = true;
                btnLevel2.Visible = true;
                btnLevel3.Visible = true;
                btnLevel4.Visible = true;
                btnLevel5.Visible = true;
            }
            else
            {

            }
        }
            
        private void SetupAvailableList(int level)
        {
            if (level == 1)
            {
                buttonList1.Text = Database.FOOD_BIFUKATU;
                buttonList2.Text = Database.FOOD_GEKIKARA_CURRY;
                buttonList3.Text = Database.FOOD_INAGO;
                buttonList4.Text = Database.FOOD_USAGI;
                buttonList5.Text = Database.FOOD_SANMA;

                buttonList1.Visible = true;
                buttonList2.Visible = true;
                if (GroundOne.WE2.FoodAvailable_11) buttonList3.Visible = true; else buttonList3.Visible = false;
                if (GroundOne.WE2.FoodAvailable_12) buttonList4.Visible = true; else buttonList4.Visible = false;
                if (GroundOne.WE2.FoodAvailable_13) buttonList5.Visible = true; else buttonList5.Visible = false;
            }
            else if (level == 2)
            {
                buttonList1.Text = Database.FOOD_FISH_GURATAN;
                buttonList2.Text = Database.FOOD_SEA_TENPURA;
                buttonList3.Text = Database.FOOD_TRUTH_YAMINABE_1;
                buttonList4.Text = Database.FOOD_OSAKANA_ZINGISKAN;
                buttonList5.Text = Database.FOOD_RED_HOT_SPAGHETTI;

                buttonList1.Visible = true;
                if (GroundOne.WE2.FoodAvailable_21) buttonList2.Visible = true; else buttonList2.Visible = false;
                if (GroundOne.WE2.FoodAvailable_22) buttonList3.Visible = true; else buttonList3.Visible = false;
                if (GroundOne.WE2.FoodAvailable_23) buttonList4.Visible = true; else buttonList4.Visible = false;
                if (GroundOne.WE2.FoodAvailable_24) buttonList5.Visible = true; else buttonList5.Visible = false;
            }
            else if (level == 3)
            {
                buttonList1.Text = Database.FOOD_HINYARI_YASAI;
                buttonList2.Text = Database.FOOD_AZARASI_SHIOYAKI;
                buttonList3.Text = Database.FOOD_WINTER_BEEF_CURRY;
                buttonList4.Text = Database.FOOD_GATTURI_GOZEN;
                buttonList5.Text = Database.FOOD_KOGOERU_DESSERT;

                buttonList1.Visible = true;
                if (GroundOne.WE2.FoodAvailable_31) buttonList2.Visible = true; else buttonList2.Visible = false;
                if (GroundOne.WE2.FoodAvailable_32) buttonList3.Visible = true; else buttonList3.Visible = false;
                if (GroundOne.WE2.FoodAvailable_33) buttonList4.Visible = true; else buttonList4.Visible = false;
                if (GroundOne.WE2.FoodAvailable_34) buttonList5.Visible = true; else buttonList5.Visible = false;
            }
            else if (level == 4)
            {
                buttonList1.Text = Database.FOOD_BLACK_BUTTER_SPAGHETTI;
                buttonList2.Text = Database.FOOD_KOROKORO_PIENUS_HAMBURG;
                buttonList3.Text = Database.FOOD_PIRIKARA_HATIMITSU_STEAK;
                buttonList4.Text = Database.FOOD_HUNWARI_ORANGE_TOAST;
                buttonList5.Text = Database.FOOD_TRUTH_YAMINABE_2;

                if (GroundOne.WE2.FoodAvailable_41) buttonList2.Visible = true; else buttonList2.Visible = false;
                if (GroundOne.WE2.FoodAvailable_42) buttonList3.Visible = true; else buttonList3.Visible = false;
                if (GroundOne.WE2.FoodAvailable_43) buttonList4.Visible = true; else buttonList4.Visible = false;
                if (GroundOne.WE2.FoodAvailable_44) buttonList5.Visible = true; else buttonList5.Visible = false;
            }
        }
    }
}
