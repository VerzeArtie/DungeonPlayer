using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonPlayer
{
    public partial class TruthEnemyCharacter : MainCharacter
    {
        public enum TargetLogic
        {
            Front,
            Back,
        }

        public enum RareString
        {
            Legendary,
            Purple,
            Gold,
            Red,
            Blue,
            Black
        }

        public enum ArmorType
        {
            Normal,
            Regist_Physical,
            Regist_Magic,
            Regist_Both
        }

        public enum MonsterArea
        {
            Area11,
            Area12,
            Area13,
            Area14,
            Boss1,
            TruthBoss1,
            Area21,
            Area22,
            Area23,
            Area24,
            Boss21,
            Boss22,
            Boss23,
            Boss24,
            Boss25,
            Boss2,
            TruthBoss2,
            Area31,
            Area32,
            Area33,
            Area34,
            Boss3,
            TruthBoss3,
            Area41,
            Area42,
            Area43,
            Area44,
            Boss4,
            TruthBoss4,
            Area51,
            Boss5,
            TruthBoss5,
            Area46,
            Duel,
            LastBoss,
        }

        protected TargetLogic _initialTarget = TargetLogic.Front;
        public TargetLogic InitialTarget
        {
            get { return _initialTarget; }
            set { _initialTarget = value; }
        } // 先頭開始時のターゲット選定

        public bool UseStackCommand { get; set; } // 敵がボスとしてスタックコマンドを使うかどうかのフラグ（Falseなら直接行動的）
        public bool DetectCannotBeStun { get; set; } // 敵がスタン耐性があるかどうかを知るフラグ
        public bool DetectCannotBeSilence { get; set; } // 敵が沈黙耐性があるかどうかを知るフラグ
        public bool DetectCannotBePoison { get; set; } // 敵が猛毒耐性があるかどうかを知るフラグ
        public bool DetectCannotBeTemptation { get; set; } // 敵が誘惑耐性があるかどうかを知るフラグ
        public bool DetectCannotBeFrozen { get; set; } // 敵が凍結耐性があるかどうかを知るフラグ
        public bool DetectCannotBeParalyze { get; set; } // 敵が麻痺耐性があるかどうかを知るフラグ
        public bool DetectCannotBeSlow { get; set; } // 敵が鈍化耐性があるかどうかを知るフラグ
        public bool DetectCannotBeBlind { get; set; } // 敵が暗闇耐性があるかどうかを知るフラグ
        public bool DetectCannotBeSlip { get; set; } // 敵がスリップ耐性があるかどうかを知るフラグ
        public bool DetectCannotBeNoResurrection { get; set; } // 敵が復活不可耐性があるかどうかを知るフラグ
        public bool DetectCannotBeNoGainLife { get; set; } // 敵がライフ回復不可耐性があるかどうかを知るフラグ
        public bool DetectDeath { get; set; } // 敵が自分が一旦死亡した事を知るフラグ（レギィンアーゼLv3専用）
                
        public int Pattern1 { get; set; } // Bystander戦術１
        public int Pattern2 { get; set; } // Bystander戦術２
        public int Pattern3 { get; set; } // Bystander戦術３
        public int Pattern4 { get; set; } // Bystander戦術４
        public int Pattern5 { get; set; } // Bystander戦術５
        public int Pattern6 { get; set; } // Bystander戦術６
        public int TimeCumulative { get; set; } // Bystander戦術時間累積カウント

        public bool StillNotAction1 { get; set; } // 一度も任意のコマンドを発動していない場合を示すフラグ、DUEL最終戦ラナとの戦闘でつけたフラグ
        public bool StillNotAction2 { get; set; } // 一度も任意のコマンドを発動していない場合を示すフラグ、DUEL最終戦ラナとの戦闘でつけたフラグ
        public bool OpponentUseInstantPoint { get; set; } // 対戦相手が一度も任意の【インスタントorソーサリー】行動をしてない事を示すフラグ、DUEL最終戦オルとの戦闘

        public int AI_TacticsNumber { get; set; } // 戦術を切り替えるために使用

        public bool BossBeforeStay { get; set; }
        public MonsterArea Area { get; set; }
        public string Description { get; set; }
        public RareString Rare { get; set; }
        public ArmorType Armor { get; set; }
        public string[] DropItem { get; set; }

        public static int MAX_DROPITEM_SIZE = 10;

        public bool AddDropItem(string dropitem)
        {
            for (int ii = 0; ii < MAX_DROPITEM_SIZE; ii++)
            {
                if (this.DropItem[ii] == string.Empty)
                {
                    this.DropItem[ii] = dropitem;
                    return true;
                }
            }
            return false;
        }

        public MainCharacter Targetting(MainCharacter mc, MainCharacter sc, MainCharacter tc)
        {
            if (this.InitialTarget == TruthEnemyCharacter.TargetLogic.Back)
            {
                if (tc != null && tc.Dead == false) { return tc; }
                else if (sc != null && sc.Dead == false) { return sc; }
                else if (mc != null && mc.Dead == false) { return mc; }
            }
            else if (this.InitialTarget == TargetLogic.Front)
            {
                if (mc != null && mc.Dead == false) { return mc; }
                else if (sc != null && sc.Dead == false) { return sc; }
                else if (tc != null && tc.Dead == false) { return tc; }
            }

            return mc;
        }

        public void NextAttackDecision(MainCharacter target,
                                        MainCharacter mc,
                                        MainCharacter sc,
                                        MainCharacter tc,
                                        TruthEnemyCharacter ec1,
                                        TruthEnemyCharacter ec2,
                                        TruthEnemyCharacter ec3)
        {
            Random rd = new Random(Environment.TickCount * DateTime.Now.Millisecond);
            switch (this.name)
            {
                case Database.ENEMY_KOUKAKU_WURM:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = Database.TOSSIN;
                            break;
                        case 1:
                            this.pa = PlayerAction.NormalAttack;
                            this.ActionLabel.Text = Database.ATTACK_JP;
                            break;
                    }
                    break;
                case Database.ENEMY_HIYOWA_BEATLE:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            if (this.currentStrengthUp <= 0)
                            {
                                this.pa = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = Database.BUFFUP_STRENGTH;
                            }
                            else
                            {
                                this.pa = PlayerAction.NormalAttack;
                                this.ActionLabel.Text = Database.ATTACK_JP;
                            }
                            break;
                        case 1:
                            this.pa = PlayerAction.NormalAttack;
                            this.ActionLabel.Text = Database.ATTACK_JP;
                            break;
                    }
                    break;
                case Database.ENEMY_GREEN_CHILD:
                    this.pa = PlayerAction.SpecialSkill;
                    this.ActionLabel.Text = Database.MAGIC_ATTACK;
                    break;
                case Database.ENEMY_MANDRAGORA:
                    this.pa = PlayerAction.SpecialSkill;
                    this.ActionLabel.Text = "超音波";
                    break;

                case Database.ENEMY_SUN_FLOWER:
                    this.PA = PlayerAction.UseSpell;
                    this.CurrentSpellName = Database.FIRE_BALL;
                    this.Target = target;
                    this.ActionLabel.Text = Database.FIRE_BALL_JP;
                    break;
                case Database.ENEMY_RED_HOPPER:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.Target = target;
                            this.ActionLabel.Text = "連続攻撃";
                            break;
                        case 1:
                            this.PA = PlayerAction.NormalAttack;
                            this.Target = target;
                            this.ActionLabel.Text = Database.ATTACK_JP;
                            break;
                    }
                    break;
                case Database.ENEMY_EARTH_SPIDER:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            this.PA = PlayerAction.NormalAttack;
                            this.Target = target;
                            this.ActionLabel.Text = Database.ATTACK_JP;
                            break;
                        case 1:
                            if (target.CurrentSlow <= 0)
                            {
                                this.pa = PlayerAction.SpecialSkill;
                                this.target = target;
                                this.ActionLabel.Text = "蜘蛛の糸";
                            }
                            else
                            {
                                this.PA = PlayerAction.NormalAttack;
                                this.Target = target;
                                this.ActionLabel.Text = Database.ATTACK_JP;
                            }
                            break;
                    }
                    break;
                case Database.ENEMY_ALRAUNE:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.target = target;
                            this.ActionLabel.Text = "怪しげな花弁";
                            break;
                        case 1:
                            this.PA = PlayerAction.UseSpell;
                            this.CurrentSpellName = Database.DARK_BLAST;
                            this.target = target;
                            this.ActionLabel.Text = Database.DARK_BLAST_JP;
                            break;
                        case 2:
                            this.PA = PlayerAction.SpecialSkill;
                            this.target = target;
                            this.ActionLabel.Text = "猛毒の花粉";
                            break;
                    }
                    break;

                case Database.ENEMY_POISON_MARY:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.target = target;
                            this.ActionLabel.Text = "毒胞子";
                            break;
                        case 1:
                            this.PA = PlayerAction.SpecialSkill;
                            this.target = target;
                            this.ActionLabel.Text = "幻覚胞子";
                            break;
                        case 2:
                            this.PA = PlayerAction.UseSpell;
                            this.target = target;
                            this.CurrentSpellName = Database.DEVOURING_PLAGUE;
                            this.ActionLabel.Text = Database.DEVOURING_PLAGUE_JP;
                            break;
                    }
                    break;

                case Database.ENEMY_SPEEDY_TAKA:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.Target = target;
                            this.ActionLabel.Text = "連続攻撃";
                            break;
                        case 1:
                            this.PA = PlayerAction.NormalAttack;
                            this.Target = target;
                            this.ActionLabel.Text = Database.ATTACK_JP;
                            break;
                    }
                    break;
                case Database.ENEMY_ZASSYOKU_RABBIT:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            if (this.currentStrengthUp <= 0)
                            {
                                this.pa = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = Database.BUFFUP_STRENGTH;
                            }
                            else
                            {
                                this.pa = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = Database.TOSSIN;
                            }
                            break;
                        case 1:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = Database.TOSSIN;
                            break;
                    }
                    break;
                case Database.ENEMY_WONDER_SEED:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "棘殻ローリング";
                            break;
                        case 1:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ニードル・スピア";
                            break;
                        case 2:
                            this.PA = PlayerAction.SpecialSkill;
                            this.Target = target;
                            this.ActionLabel.Text = "連続攻撃";
                            break;
                    }
                    break;
                case Database.ENEMY_FLANSIS_KNIGHT:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "なぎ払い";
                            break;
                        case 1:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ファイア・ランス";
                            break;
                        case 2:
                            this.pa = PlayerAction.NormalAttack;
                            this.ActionLabel.Text = Database.ATTACK_JP;
                            break;
                    }
                    break;
                case Database.ENEMY_SHOTGUN_HYUI:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ヒューイ弾丸";
                            break;
                        case 1:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ホウセンの種";
                            break;
                        case 2:
                            if (this.currentStrengthUp <= 0)
                            {
                                this.pa = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = Database.BUFFUP_STRENGTH;
                            }
                            else
                            {
                                this.pa = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "ヒューイ弾丸";
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_BRILLIANT_BUTTERFLY:
                    this.pa = PlayerAction.SpecialSkill;
                    this.ActionLabel.Text = Database.MAGIC_ATTACK;
                    break;
                case Database.ENEMY_WAR_WOLF:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            this.pa = PlayerAction.NormalAttack;
                            this.ActionLabel.Text = Database.ATTACK_JP;
                            break;
                        case 1:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = Database.TOSSIN;
                            break;
                    }
                    break;
                case Database.ENEMY_BLOOD_MOSS:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            this.pa = PlayerAction.NormalAttack;
                            this.ActionLabel.Text = Database.ATTACK_JP;
                            break;
                        case 1:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "赤い胞子";
                            break;
                    }
                    break;
                case Database.ENEMY_MOSSGREEN_DADDY:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            this.pa = PlayerAction.UseSpell;
                            this.currentSpellName = Database.FIRE_BALL;
                            this.ActionLabel.Text = Database.FIRE_BALL_JP;
                            break;
                        case 1:
                            this.pa = PlayerAction.UseSpell;
                            this.currentSpellName = Database.SHADOW_PACT;
                            this.ActionLabel.Text = Database.SHADOW_PACT_JP;
                            break;
                        case 2:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "エンタングル";
                            break;

                    }
                    break;

                case Database.ENEMY_BOSS_KARAMITUKU_FLANSIS:
                    int rand = AP.Math.RandomInteger(5);
                    if (this.BossBeforeStay)
                    {
                        rand = 3;
                    }
                    // Tac0：始まりは毒胞子による猛毒と暗闇から始める。Tac1へ移行。
                    // Tac1：レッドローズブラスト、または連槍突進、またはファイアビューネを行う。Tac2へ移行。
                    // Tac2：絡み蔦は相手がSlowではない場合、行う。そうでない場合、毒胞子を続けて放つ。Tac1へ移行。
                    // 　　　なお、毒カウンターは累積する。毒カウンターが増加するたび、猛毒効果ダメージは上昇する。
                    // キル・スピニングランサーはインスタント行動がたまったときにスタックコマンドで発動する。
                    if ((mc != null) && (mc.CurrentPoison <= 0) && (sc != null) && (sc.CurrentPoison <= 0))
                    {
                        this.PA = PlayerAction.SpecialSkill;
                        this.target = target;
                        this.ActionLabel.Text = "黒の毒胞子";
                    }
                    else
                    {
                        if (this.AI_TacticsNumber == 0)
                        {
                            switch (AP.Math.RandomInteger(3))
                            {
                                case 0:
                                    this.PA = PlayerAction.SpecialSkill;
                                    this.target = target;
                                    this.ActionLabel.Text = "レッドローズブラスト"; // ファイアビューネ
                                    break;
                                case 1:
                                    this.PA = PlayerAction.SpecialSkill;
                                    this.target = target;
                                    this.ActionLabel.Text = "連槍突進";
                                    break;
                                case 2:
                                    this.PA = PlayerAction.SpecialSkill;
                                    this.target = target;
                                    this.ActionLabel.Text = "ファイアビューネ";
                                    break;
                            }

                            this.AI_TacticsNumber = 1;
                        }
                        else
                        {
                            if ((mc != null) && (mc.CurrentSlow <= 0) && (sc != null) && (sc.CurrentSlow <= 0))
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.target = target;
                                this.ActionLabel.Text = "絡み蔦";
                            }
                            else
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.target = target;
                                this.ActionLabel.Text = "黒の毒胞子";
                            }

                            this.AI_TacticsNumber = 0;
                        }
                    }
                    //switch (rand)
                    //{
                    //    case 0:
                    //        this.PA = PlayerAction.SpecialSkill;
                    //        this.target = target;
                    //        this.ActionLabel.Text = "絡み蔦";
                    //        break;
                    //    case 1:
                    //        this.PA = PlayerAction.SpecialSkill;
                    //        this.target = target;
                    //        this.ActionLabel.Text = "黒の毒胞子";
                    //        break;
                    //    case 2:
                    //        this.PA = PlayerAction.SpecialSkill;
                    //        this.target = target;
                    //        this.ActionLabel.Text = "レッドローズブラスト";
                    //        break;
                    //    case 3:
                    //        this.PA = PlayerAction.SpecialSkill;
                    //        this.target = target;
                    //        this.ActionLabel.Text = "キル・スピニングランサー";
                    //        break;
                    //    case 4:
                    //        this.PA = PlayerAction.SpecialSkill;
                    //        this.target = target;
                    //        this.ActionLabel.Text = "連槍突進";
                    //        break;
                    //    default:
                    //        System.Windows.Forms.MessageBox.Show(rand.ToString());
                    //        break;
                    //}
                    break;






                // ２階






                case Database.ENEMY_DAGGER_FISH:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.Target = target;
                            this.ActionLabel.Text = "乱れ噛み付き";
                            break;
                        case 1:
                            this.PA = PlayerAction.NormalAttack;
                            this.Target = target;
                            this.ActionLabel.Text = Database.ATTACK_JP;
                            break;
                    }
                    break;

                case Database.ENEMY_SIPPU_FLYING_FISH:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.Target = target;
                            this.ActionLabel.Text = "連続攻撃";
                            break;
                        case 1:
                            this.PA = PlayerAction.NormalAttack;
                            this.Target = target;
                            this.ActionLabel.Text = Database.ATTACK_JP;
                            break;
                    }
                    break;

                case Database.ENEMY_ORB_SHELLFISH:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            this.PA = PlayerAction.UseSpell;
                            this.currentSpellName = Database.FROZEN_LANCE;
                            this.ActionLabel.Text = Database.FROZEN_LANCE_JP;
                            this.Target = target;
                            break;
                        case 1:
                            if (this.currentChargeCount <= 0)
                            {
                                this.PA = PlayerAction.Charge;
                                this.Target = this;
                                this.ActionLabel.Text = Database.TAMERU_JP;
                            }
                            else
                            {
                                this.PA = PlayerAction.UseSpell;
                                this.currentSpellName = Database.FROZEN_LANCE;
                                this.ActionLabel.Text = Database.FROZEN_LANCE_JP;
                                this.Target = target;
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_SPLASH_KURIONE:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "透明な光";
                            this.Target = target;
                            break;
                        case 1:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "共鳴波";
                            this.Target = target;
                            break;
                    }
                    break;

                case Database.ENEMY_ROLLING_MAGURO:
                    if (this.target == mc)
                    {
                        this.PA = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "捕獲選定";
                        if (sc != null && !sc.Dead)
                        {
                            // this.target = sc;
                        }
                        else if (tc != null && !tc.Dead)
                        {
                            // this.target = tc;
                        }
                        else
                        {
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ローリング突進";
                            // this.targetは記述しない。
                        }
                    }
                    else
                    {
                        this.PA = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "ローリング突進";
                        // this.targetは記述しない。
                    }
                    break;

                case Database.ENEMY_RANBOU_SEA_ARTINE:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "トゲの放射";
                            this.Target = target;
                            break;
                        case 1:
                            if (this.currentPhysicalAttackUp <= 0)
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "表面膨張";
                                this.Target = target;
                            }
                            else
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "トゲの放射";
                                this.Target = target;
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_BLUE_SEA_WASI:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "連続攻撃";
                            this.Target = target;
                            break;
                        case 1:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "金切り声";
                            this.Target = target;
                            break;
                    }
                    break;

                case Database.ENEMY_GANGAME:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "地響き";
                            this.Target = target;
                            break;
                        case 1:
                            if (this.currentPhysicalDefenseUp <= 0)
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "タートル・シェル";
                                this.Target = target;
                            }
                            else
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "がぶりつき";
                                this.Target = target;
                            }
                            break;
                        case 2:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "がぶりつき";
                            this.Target = target;
                            break;
                    }
                    break;

                case Database.ENEMY_BIGMOUSE_JOE:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            if (this.currentAgilityUp <= 0)
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "伸張する舌";
                                this.Target = target;
                            }
                            else
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "トリプル・パンチ";
                                this.Target = target;
                            }
                            break;
                        case 1:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "トリプル・パンチ";
                            this.Target = target;
                            break;
                        case 2:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "異常な奇声";
                            this.Target = target;
                            break;
                    }
                    break;


                case Database.ENEMY_MOGURU_MANTA:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "流水の渦巻き";
                            this.Target = target;
                            break;
                        case 1:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "流水の突壁";
                            this.Target = target;
                            break;
                    }
                    break;

                case Database.ENEMY_FLOATING_GOLD_FISH:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "鉄砲泡";
                            this.Target = target;
                            break;
                        case 1:
                            if (this.currentSpeedUp <= 0)
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "水面跳躍";
                                this.Target = target;
                            }
                            else
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "鉄砲泡";
                                this.Target = target;
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_GOEI_HERMIT_CLUB:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "豪腕ハサミ";
                            this.Target = target;
                            break;
                        case 1:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "突進バサミ";
                            this.Target = target;
                            break;
                        case 2:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ダブル・ハサミ";
                            this.Target = target;
                            break;
                    }
                    break;

                case Database.ENEMY_VANISHING_CORAL:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "コーラル・サウンド";
                            this.Target = target;
                            break;
                        case 1:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "バニッシング・エフェクト";
                            this.Target = target;
                            break;
                        case 2:
                            if (this.currentLife <= this.MaxLife / 5)
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "ラスト・バウンド";
                                this.Target = target;
                            }
                            else
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "コーラル・サウンド";
                                this.Target = target;
                            }
                            break;
                    }
                    break;

                case Database.ENEMY_CASSY_CANCER:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ベタつく緑泡";
                            this.Target = target;
                            break;
                        case 1:
                            if (this.currentPhysicalDefenseUp <= 0)
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "甲殻増強";
                                this.Target = target;
                            }
                            else
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "キャンサー・ブロー";
                                this.Target = target;
                            }
                            break;
                        case 2:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "キャンサー・ブロー";
                            this.Target = target;
                            break;
                    }
                    break;


                case Database.ENEMY_BLACK_STARFISH:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            this.PA = PlayerAction.UseSpell;
                            this.currentSpellName = Database.BLACK_FIRE;
                            this.ActionLabel.Text = Database.BLACK_FIRE_JP;
                            this.Target = target;
                            break;
                        case 1:
                            this.PA = PlayerAction.UseSpell;
                            this.currentSpellName = Database.BLUE_BULLET;
                            this.ActionLabel.Text = Database.BLUE_BULLET_JP;
                            this.Target = target;
                            break;
                    }
                    break;

                case Database.ENEMY_RAINBOW_ANEMONE:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            this.PA = PlayerAction.UseSpell;
                            this.currentSpellName = Database.VANISH_WAVE;
                            this.ActionLabel.Text = Database.VANISH_WAVE_JP;
                            this.Target = target;
                            break;
                        case 1:
                            this.PA = PlayerAction.UseSkill;
                            this.currentSkillName = Database.PSYCHIC_WAVE;
                            this.ActionLabel.Text = Database.PSYCHIC_WAVE_JP;
                            this.Target = target;
                            break;
                    }
                    break;

                case Database.ENEMY_EDGED_HIGH_SHARK:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "猛突撃";
                            this.Target = target;
                            break;
                        case 1:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "貪欲な咬みつき";
                            this.Target = target;
                            break;
                        case 2:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ヴァイオレンス・テール";
                            this.Target = target;
                            break;
                    }
                    break;

                case Database.ENEMY_EIGHT_EIGHT:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "【八】はがい絞め";
                            this.Target = target;
                            break;
                        case 1:
                            if (this.CurrentMagicAttackUp <= 0)
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "ブチ巻く黒墨";
                                this.Target = this;
                            }
                            else
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "黒墨ミサイル";
                                this.target = target;
                            }
                            break;
                        case 2:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "大渦巻き";
                            this.Target = target;
                            break;
                    }
                    break;


                // ２階、力の部屋ボス１
                case Database.ENEMY_BRILLIANT_SEA_PRINCE:
                    rand = AP.Math.RandomInteger(3);
                    switch (rand)
                    {
                        case 0:
                            if ((this.currentPhysicalDefenseUp <= 0 && this.currentMagicDefenseUp <= 0 && this.currentSpeedUp <= 0) &&
                                (this.currentStrengthUp <= 0 && this.currentIntelligenceUp <= 0 && this.currentMindUp <= 0))
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.target = this;
                                this.ActionLabel.Text = "シースライドウォータ";
                            }
                            else
                            {
                                this.PA = PlayerAction.NormalAttack;
                                this.target = target;
                                this.ActionLabel.Text = Database.ATTACK_JP;
                            }
                            break;
                        case 1:
                            if ((this.currentStrengthUp <= 0 && this.currentIntelligenceUp <= 0 && this.currentMindUp <= 0) &&
                                (this.currentPhysicalDefenseUp <= 0 && this.currentMagicDefenseUp <= 0 && this.currentSpeedUp <= 0))
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.target = this;
                                this.ActionLabel.Text = "勇敢な雄叫び";
                            }
                            else
                            {
                                this.PA = PlayerAction.NormalAttack;
                                this.target = target;
                                this.ActionLabel.Text = Database.ATTACK_JP;
                            }
                            break;
                        case 2:
                            if (this.currentStrengthUp > 0 || this.currentIntelligenceUp > 0 || this.currentMindUp > 0)
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.target = target;
                                this.ActionLabel.Text = "グングニル・スラッシュ";

                                RemoveStrengthUp();
                                RemoveMindUp();
                            }
                            else if (this.currentPhysicalDefenseUp > 0 || this.currentMagicDefenseUp > 0 || this.currentSpeedUp > 0)
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.target = target;
                                this.ActionLabel.Text = "グングニルの閃光";

                                RemoveMagicAttackUp();
                                RemoveSpeedUp();
                            }
                            else
                            {
                                this.PA = PlayerAction.NormalAttack;
                                this.target = target;
                                this.ActionLabel.Text = Database.ATTACK_JP;
                            }
                            break;
                        default:
                            System.Windows.Forms.MessageBox.Show(rand.ToString());
                            break;
                    }

                    break;

                // ２階、力の部屋ボス２
                case Database.ENEMY_ORIGIN_STAR_CORAL_QUEEN:
                    this.PA = PlayerAction.UseSpell;
                    this.currentSpellName = Database.FROZEN_LANCE;
                    this.target = target;
                    this.ActionLabel.Text = Database.FROZEN_LANCE_JP;
                    break;

                // ２階、力の部屋ボス３
                case Database.ENEMY_SHELL_SWORD_KNIGHT:
                    rand = AP.Math.RandomInteger(4);
                    if (this.currentWordOfFortune > 0)
                    {
                        rand = 0;
                    }
                    switch (rand)
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.target = target;
                            this.ActionLabel.Text = "シー・ストライプ";
                            break;
                        case 1:
                            if (this.currentWordOfFortune <= 0)
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.target = this;
                                this.ActionLabel.Text = "ブリンク・シェル";
                            }
                            else
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.target = target;
                                this.ActionLabel.Text = "シー・ストライプ";
                            }
                            break;
                        case 2:
                            this.PA = PlayerAction.SpecialSkill;
                            this.target = target;
                            this.ActionLabel.Text = "深海の渦";
                            break;
                        case 3:
                            if (this.currentPhysicalAttackUp <= 0)
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.target = this;
                                this.ActionLabel.Text = "海星源への忠誠";
                            }
                            else
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.target = target;
                                this.ActionLabel.Text = "シー・ストライプ";
                            }
                            break;
                        default:
                            System.Windows.Forms.MessageBox.Show(rand.ToString());
                            break;
                    }
                    break;

                // ２階、力の部屋ボス４－１
                case Database.ENEMY_JELLY_EYE_BRIGHT_RED:
                    rand = AP.Math.RandomInteger(4);
                    switch (rand)
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.target = target;
                            this.ActionLabel.Text = "燃え盛る炎弾丸";
                            break;
                        case 1:
                            this.PA = PlayerAction.SpecialSkill;
                            this.target = this;
                            this.ActionLabel.Text = "ファイア・ウォール";
                            break;
                        case 2:
                            this.PA = PlayerAction.SpecialSkill;
                            this.target = target;
                            this.ActionLabel.Text = "ブレイジング・ストーム";
                            break;
                        case 3:
                            this.PA = PlayerAction.SpecialSkill;
                            this.target = target;
                            this.ActionLabel.Text = "フラッシュ・バーン";
                            break;
                        default:
                            System.Windows.Forms.MessageBox.Show(rand.ToString());
                            break;
                    }
                    break;

                // ２階、力の部屋ボス４－２
                case Database.ENEMY_JELLY_EYE_DEEP_BLUE:
                    rand = AP.Math.RandomInteger(4);
                    switch (rand)
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.target = target;
                            this.ActionLabel.Text = "凍てつく氷弾丸";
                            break;
                        case 1:
                            this.PA = PlayerAction.SpecialSkill;
                            this.target = this;
                            this.ActionLabel.Text = "ウォータ・バブル";
                            break;
                        case 2:
                            this.PA = PlayerAction.SpecialSkill;
                            this.target = target;
                            this.ActionLabel.Text = "ウォーター・スラッシュ";
                            break;
                        case 3:
                            this.PA = PlayerAction.SpecialSkill;
                            this.target = target;
                            this.ActionLabel.Text = "ハルシネイト・アイ";
                            break;
                        default:
                            System.Windows.Forms.MessageBox.Show(rand.ToString());
                            break;
                    }
                    break;

                // ２階、力の部屋ボス５－１
                case Database.ENEMY_SEA_STAR_KNIGHT_AEGIRU:
                    rand = AP.Math.RandomInteger(2);
                    switch (rand)
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.target = target;
                            this.ActionLabel.Text = "スターソード『煌』";
                            break;

                        case 1:
                            if (this.target2 != null && !this.target2.Dead && this.target2.CurrentPhysicalDefenseUp <= 0)
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "エーギル・フィールド";
                            }
                            else
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.target = target;
                                this.ActionLabel.Text = "スターソード『煌』";
                            }
                            break;
                        default:
                            System.Windows.Forms.MessageBox.Show(rand.ToString());
                            break;
                    }
                    break;

                // ２階、力の部屋ボス５ー２
                case Database.ENEMY_SEA_STAR_KNIGHT_AMARA:
                    rand = AP.Math.RandomInteger(2);
                    switch (rand)
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.target = target;
                            this.ActionLabel.Text = "スターソード『艶』";
                            break;

                        case 1:
                            if (this.target2 != null && !this.target2.Dead && this.target2.CurrentPhysicalDefenseUp <= 0)
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "アマラ・フィールド";
                            }
                            else
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.target = target;
                                this.ActionLabel.Text = "スターソード『艶』";
                            }
                            break;
                        default:
                            System.Windows.Forms.MessageBox.Show(rand.ToString());
                            break;
                    }
                    break;

                // ２階、力の部屋ボス５－３
                case Database.ENEMY_SEA_STAR_ORIGIN_KING:
                    rand = AP.Math.RandomInteger(3);
                    switch (rand)
                    {
                        case 0:
                            if (ec2 != null && !ec2.Dead && ec2.currentProtection <= 0)
                            {
                                this.PA = PlayerAction.UseSpell;
                                this.currentSpellName = Database.PROTECTION;
                                this.ActionLabel.Text = Database.PROTECTION_JP;
                                this.target2 = ec2;
                            }
                            else if (ec3 != null && !ec3.Dead && ec3.currentProtection <= 0)
                            {
                                this.PA = PlayerAction.UseSpell;
                                this.currentSpellName = Database.PROTECTION;
                                this.ActionLabel.Text = Database.PROTECTION_JP;
                                this.target2 = ec3;
                            }
                            else if (this.currentProtection <= 0)
                            {
                                this.PA = PlayerAction.UseSpell;
                                this.currentSpellName = Database.PROTECTION;
                                this.ActionLabel.Text = Database.PROTECTION_JP;
                                this.target2 = this;
                            }
                            else
                            {
                                this.pa = PlayerAction.UseSpell;
                                this.currentSpellName = Database.HOLY_SHOCK;
                                this.ActionLabel.Text = Database.HOLY_SHOCK_JP;
                                this.target = target;
                            }
                            break;
                        case 1:
                            if (ec2 != null && !ec2.Dead && ec2.currentSaintPower <= 0)
                            {
                                this.PA = PlayerAction.UseSpell;
                                this.currentSpellName = Database.SAINT_POWER;
                                this.ActionLabel.Text = Database.SAINT_POWER_JP;
                                this.target2 = ec2;
                            }
                            else if (ec3 != null && !ec3.Dead && ec3.currentSaintPower <= 0)
                            {
                                this.PA = PlayerAction.UseSpell;
                                this.currentSpellName = Database.SAINT_POWER;
                                this.ActionLabel.Text = Database.SAINT_POWER_JP;
                                this.target2 = ec3;
                            }
                            else
                            {
                                this.PA = PlayerAction.UseSpell;
                                this.currentSpellName = Database.HOLY_SHOCK;
                                this.ActionLabel.Text = Database.HOLY_SHOCK_JP;
                                this.target = target;
                            }
                            break;

                        case 2:
                            this.PA = PlayerAction.UseSpell;
                            this.currentSpellName = Database.FRESH_HEAL;
                            this.ActionLabel.Text = Database.FRESH_HEAL_JP;
                            if (ec2 != null && !ec2.Dead && ec3 != null && !ec3.dead)
                            {
                                if ((ec2.currentLife >= ec2.MaxLife) && (ec3.currentLife >= ec3.MaxLife))
                                {
                                    this.target2 = this;
                                }
                                else if ((ec2.currentLife < ec2.MaxLife) && (ec3.currentLife >= ec3.MaxLife))
                                {
                                    this.target2 = ec2;
                                }
                                else if ((ec2.currentLife >= ec2.MaxLife) && (ec3.currentLife < ec3.MaxLife))
                                {
                                    this.target2 = ec3;
                                }
                                else if (ec2.currentLife < ec3.currentLife)
                                {
                                    this.target2 = ec2;
                                }
                                else
                                {
                                    this.target2 = ec3;
                                }
                            }
                            else if (ec2 != null && ec2.dead && ec3 != null && !ec3.dead)
                            {
                                this.target2 = ec3;
                            }
                            else if (ec2 != null && !ec2.dead && ec3 != null && ec3.dead)
                            {
                                this.target2 = ec2;
                            }
                            else
                            {
                                this.target2 = this;
                            }
                            break;
                        default:
                            System.Windows.Forms.MessageBox.Show(rand.ToString());
                            break;
                    }
                    break;

                // ２階、力の部屋ボス６
                case Database.ENEMY_BOSS_LEVIATHAN:
                    rand = AP.Math.RandomInteger(4);
                    switch (rand)
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "バースト・クラウド";
                            break;

                        case 1:
                            if ((this.currentLife <= this.MaxLife / 2) && (this.currentPhysicalAttackUp <= 0))
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.target2 = this;
                                this.ActionLabel.Text = "海王の咆哮";
                            }
                            else
                            {
                                this.pa = PlayerAction.SpecialSkill;
                                this.target = target;
                                this.ActionLabel.Text = "大激衝";
                            }
                            break;

                        case 2:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "サージェティック・バインド";
                            break;

                        case 3:
                            this.pa = PlayerAction.SpecialSkill;
                            this.target = target;
                            this.ActionLabel.Text = "大激衝";
                            break;
                    }
                    break;

                // １～５階、支配竜
                case Database.ENEMY_DRAGON_SOKUBAKU_BRIYARD:
                case Database.ENEMY_DRAGON_TINKOU_DEEPSEA:
                case Database.ENEMY_DRAGON_DESOLATOR_AZOLD:
                    if (this.AI_TacticsNumber < 5)
                    {
                        this.pa = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "無音の呼び声";
                    }
                    else
                    {
                        this.pa = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "形成消失";
                    }
                    break;

                // ３階
                case Database.ENEMY_TOSSIN_ORC:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "突貫";
                            this.target = target;
                            break;
                        case 1:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "暴走";
                            break;
                    }
                    break;
                case Database.ENEMY_SNOW_CAT:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "連続攻撃";
                            this.target = target;
                            break;
                        case 1:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "凍りつく吹雪";
                            this.target = target;
                            break;
                    }
                    break;
                case Database.ENEMY_WAR_MAMMOTH:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ためる";
                            this.target = this;
                            break;
                        case 1:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "蹂躙";
                            this.target = target;
                            break;
                    }
                    break;
                case Database.ENEMY_WINGED_COLD_FAIRY:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "プチ・ブリザード";
                            this.target = target;
                            break;
                        case 1:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "凍結玉";
                            this.target = target;
                            break;
                        case 2:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ウィンター・ソング";
                            this.target = target;
                            break;
                    }
                    break;

                case Database.ENEMY_BRUTAL_OGRE:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ぶん投げる";
                            this.target = target;
                            break;
                        case 1:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "氷の儀式";
                            this.target = this;
                            break;
                    }
                    break;
                case Database.ENEMY_HYDRO_LIZARD:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "リザード・スラッシュ";
                            this.target = target;
                            break;
                        case 1:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "アイシクル・ブレード";
                            break;
                    }
                    break;
                case Database.ENEMY_PENGUIN_STAR:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ペンギンの輝き！";
                            this.target = this;
                            break;
                        case 1:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ペンギンアタック！";
                            this.target = target;
                            break;
                    }
                    break;
                case Database.ENEMY_SWORD_TOOTH_TIGER:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "サーヴェルクロー";
                            this.target = target;
                            break;
                        case 1:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "目くらまし";
                            this.target = this;
                            break;
                        case 2:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "連速三段";
                            this.target = target;
                            break;
                    }
                    break;
                case Database.ENEMY_FEROCIOUS_RAGE_BEAR:
                    switch (AP.Math.RandomInteger(4))
                    {
                        case 0:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "四歯戦速";
                            this.target = target;
                            break;
                        case 1:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "自己増強";
                            this.target = this;
                            break;
                        case 2:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "漸波動";
                            this.target = target;
                            break;
                        case 3:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "食いちぎり";
                            this.target = target;
                            break;                            
                    }
                    break;

                case Database.ENEMY_WINTER_ORB:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "氷の結晶術";
                            this.target = target;
                            break;
                        case 1:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "冷気の射出";
                            this.target = target;
                            break;
                    }
                    break;
                case Database.ENEMY_PATHFINDING_LIGHTNING_AZARASI:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            if (this == ec2)
                            {
                                if (ec1.ActionLabel.Text == "津波の呼び声")
                                {
                                    this.pa = PlayerAction.SpecialSkill;
                                    this.ActionLabel.Text = "平穏の呼び声";
                                    this.target = target;
                                }
                                else
                                {
                                    this.pa = PlayerAction.SpecialSkill;
                                    this.ActionLabel.Text = "津波の呼び声";
                                    this.target = target;
                                }
                            }
                            else
                            {
                                this.pa = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "津波の呼び声";
                                this.target = target;
                            }
                            break;
                        case 1:
                            if (this == ec2)
                            {
                                if (ec1.ActionLabel.Text == "平穏の呼び声")
                                {
                                    this.pa = PlayerAction.SpecialSkill;
                                    this.ActionLabel.Text = "津波の呼び声";
                                    this.target = target;
                                }
                                else
                                {
                                    this.pa = PlayerAction.SpecialSkill;
                                    this.ActionLabel.Text = "平穏の呼び声";
                                    this.target = target;
                                }
                            }
                            else
                            {
                                this.pa = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "平穏の呼び声";
                                this.target = target;
                            }
                            break;
                    }
                    break;
                case Database.ENEMY_INTELLIGENCE_ARGONIAN:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            this.pa = PlayerAction.UseSpell;
                            this.currentSpellName = Database.FROZEN_AURA;
                            this.ActionLabel.Text = Database.FROZEN_AURA_JP;
                            this.target = this;
                            break;
                        case 1:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "打突";
                            this.target = target;
                            break;
                    }
                    break;
                case Database.ENEMY_MAGIC_HYOU_RIFLE:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "雹弾乱射";
                            this.target = target;
                            break;
                        case 1:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ＳＰＬＡＳＨ！";
                            this.target = target;
                            break;
                        case 2:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "マジックバリア";
                            this.target = this;
                            break;
                    }
                    break;
                case Database.ENEMY_PURE_BLIZZARD_CRYSTAL:
                    switch (AP.Math.RandomInteger(4))
                    {
                        case 0:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ブリザード";
                            this.target = target;
                            break;
                        case 1:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "零式";
                            this.target = target;
                            break;
                        case 2:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "蒼授の気配";
                            this.target = this;
                            break;
                        case 3:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "絶・スピニングランサー";
                            this.target = target;
                            break;
                    }
                    break;

                case Database.ENEMY_PURPLE_EYE_WARE_WOLF:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            if (this.CurrentStrengthUp <= 0)
                            {
                                this.pa = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "バトルクライ";
                                this.target = this;
                            }
                            else
                            {
                                this.pa = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "キリング・スラッシュ";
                                this.target = target;
                            }
                            break;
                        case 1:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "キリング・スラッシュ";
                            this.target = target;
                            break;
                    }
                    break;
                case Database.ENEMY_FROST_HEART:
                    if (this.currentMagicAttackUpValue <= 30000)
                    {
                        this.pa = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "冷気圧縮";
                        this.target = this;
                    }
                    else
                    {
                        this.pa = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "自爆";
                        List<MainCharacter> group = new List<MainCharacter>();
                        if (mc != null && !mc.Dead) { group.Add(mc); }
                        if (sc != null && !sc.Dead) { group.Add(sc); }
                        if (tc != null && !tc.Dead) { group.Add(tc); }
                        int randomValue = AP.Math.RandomInteger(group.Count);
                        this.target = group[randomValue];
                    }
                    break;
                case Database.ENEMY_WIND_BREAKER:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            if (this.currentGaleWind <= 0)
                            {
                                this.pa = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "ソード・オブ・ウィンド";
                                this.target = this;
                            }
                            else
                            {
                                this.pa = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "断空";
                                this.target = target;
                            }
                            break;
                        case 1:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "断空";
                            this.target = target;
                            break;
                        case 2:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "アイス・トルネード";
                            this.target = target;
                            break;
                    }
                    break;
                case Database.ENEMY_TUNDRA_LONGHORN_DEER:
                    List<MainCharacter> group_deer = new List<MainCharacter>();
                    if (mc != null && !mc.Dead) { group_deer.Add(mc); }
                    if (sc != null && !sc.Dead) { group_deer.Add(sc); }
                    if (tc != null && !tc.Dead) { group_deer.Add(tc); }
                    int randomValue_deer = AP.Math.RandomInteger(group_deer.Count);
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            if (group_deer[randomValue_deer].CurrentReactionDown <= 0)
                            {
                                this.pa = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "トキのコエ";
                                this.target = group_deer[randomValue_deer];
                            }
                            else
                            {
                                this.pa = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "レッドスノーホーン";
                                this.target = group_deer[randomValue_deer];
                            }
                            break;
                        case 1:
                            if (group_deer[randomValue_deer].CurrentPhysicalAttackDown <= 0)
                            {
                                this.pa = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "氷雪化現象";
                                this.target = group_deer[randomValue_deer];
                            }
                            else
                            {
                                this.pa = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "レッドスノーホーン";
                                this.target = group_deer[randomValue_deer];
                            }
                            break;
                        case 2:
                            if (group_deer[randomValue_deer].CurrentMagicDefenseDown <= 0)
                            {
                                this.pa = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "無音音響の和";
                                this.target = group_deer[randomValue_deer];
                            }
                            else
                            {
                                this.pa = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "レッドスノーホーン";
                                this.target = group_deer[randomValue_deer];
                            }
                            break;
                    }
                    break;


                // ３階、ボス
                case Database.ENEMY_BOSS_HOWLING_SEIZER:
                    rand = AP.Math.RandomInteger(5);
                    switch (rand)
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "もぎとり";
                            this.target = target;
                            break;

                        case 1:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ブンまわし";
                            break;

                        case 2:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "異常音響";
                            break;

                        case 3:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "凍らせる視線";
                            this.target = target;
                            break;

                        case 4:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "破裂する雄叫び";
                            break;
                    }
                    break;

                #region "４階"
                case Database.ENEMY_GENAN_HUNTER:
                    rand = AP.Math.RandomInteger(3);
                    switch (rand)
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "バインド・ウィップ";
                            break;
                        case 1:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "アジテイト・アロー";
                            break;
                        case 2:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "デッドリー・ショット";
                            break;
                    }
                    break;
                case Database.ENEMY_BEAST_MASTER:
                    rand = AP.Math.RandomInteger(3);
                    switch (rand)
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "タイガー・ブロウ";
                            break;
                        case 1:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "圧死の咆哮";
                            break;
                        case 2:
                            if (this.currentPhysicalAttackUp <= 0)
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "ピューマ・ライジング";
                            }
                            else
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "タイガー・ブロウ";
                            }
                            break;
                    }
                    break;
                case Database.ENEMY_ELDER_ASSASSIN:
                    rand = AP.Math.RandomInteger(3);
                    switch (rand)
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "フェイタル・ニードル";
                            break;
                        case 1:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "気配抹消";
                            break;
                        case 2:
                            if (target.CurrentPhysicalDefenseDown <= 0)
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "ウロボロスの一撃";
                            }
                            else
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "神速の連撃";
                            }
                            break;
                    }
                    break;
                case Database.ENEMY_FALLEN_SEEKER:
                    rand = AP.Math.RandomInteger(3);
                    switch (rand)
                    {
                        case 0:
                            if (this.currentShadowUp <= 0)
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "汚れし悪魔契約";
                            }
                            else if (this.currentLightUp <= 0)
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "純潔の聖天使契約";
                            }
                            else
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "ホーリー・バレット";
                            }
                            break;
                        case 1:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ホーリー・バレット";
                            break;
                        case 2:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "セイント・スラッシュ";
                            break;
                    }
                    break;

                case Database.ENEMY_MASTER_LOAD:
                    rand = AP.Math.RandomInteger(3);
                    switch (rand)
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "スペリオル・フィールド";
                            break;
                        case 1:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ダーク・エリミネイト";
                            break;
                        case 2:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "振動剣";
                            break;
                    }
                    break;
                case Database.ENEMY_EXECUTIONER:
                    rand = AP.Math.RandomInteger(3);
                    switch (rand)
                    {
                        case 0:
                            if (this.currentAfterReviveHalf <= 0)
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "断罪の加護";
                            }
                            else
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "デス・ストライク";
                            }
                            break;
                        case 1:
                            if (target.CurrentAbsoluteZero <= 0)
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "魂への凍結";
                            }
                            else
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "デス・ストライク";
                            }
                            break;
                        case 2:
                            if (target.CurrentDamnation <= 0)
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "死滅のひと振り";
                            }
                            else
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "デス・ストライク";
                            }
                            break;
                    }
                    break;
                case Database.ENEMY_DARK_MESSENGER:
                    if (this.AI_TacticsNumber == 0)
                    {
                        if ((mc != null && mc.CurrentNoResurrection <= 0) ||
                            (sc != null && sc.CurrentNoResurrection <= 0) ||
                            (tc != null && tc.CurrentNoResurrection <= 0))
                        {
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "黒龍のささやき";
                        }
                        else
                        {
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "チューズン・サクリファイ";
                        }
                    }
                    else if (this.AI_TacticsNumber == 1)
                    {
                        this.PA = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "チューズン・サクリファイ";
                    }
                    else if (this.AI_TacticsNumber == 2)
                    {
                        this.PA = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "死への背徳";
                    }
                    break;
                case Database.ENEMY_BLACKFIRE_MASTER_BLADE:
                    if (this.AI_TacticsNumber == 0)
                    {
                        this.PA = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "螺旋黒炎";
                    }
                    else
                    {
                        rand = AP.Math.RandomInteger(2);
                        switch (rand)
                        {
                            case 0:
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "乱奏連撃";
                                break;
                            case 1:
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "ブルー・エクスプロード";
                                break;
                        }
                    }
                    break;
                case Database.ENEMY_SIN_THE_DARKELF:
                    rand = AP.Math.RandomInteger(4);
                    switch (rand)
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ネイチャー・エンゼンブル";
                            break;
                        case 1:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "シャープネル・ニードル";
                            break;
                        case 2:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "メギド・ブレイズ";
                            break;
                        case 3:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "アーケン・デストラクション";
                            break;
                    }
                    break;

                case Database.ENEMY_SUN_STRIDER:
                    rand = AP.Math.RandomInteger(3);
                    switch (rand)
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "太陽の滅印";
                            break;
                        case 1:
                            if (target.CurrentDamnation <= 0)
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "ブラック・フレア";
                            }
                            else
                            {
                                if (this.currentSaintPower <= 0)
                                {
                                    this.PA = PlayerAction.SpecialSkill;
                                    this.ActionLabel.Text = "サテライト・エナジー";
                                }
                                else
                                {
                                    this.PA = PlayerAction.SpecialSkill;
                                    this.ActionLabel.Text = "サテライト・ソード";
                                }
                            }
                            break;
                        case 2:
                            if (this.currentSaintPower <= 0)
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "サテライト・エナジー";
                            }
                            else
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "サテライト・ソード";
                            }
                            break;
                    }
                    break;
                case Database.ENEMY_ARC_DEMON:
                    rand = AP.Math.RandomInteger(3);
                    switch (rand)
                    {
                        case 0:
                            if (this.currentPhysicalAttackUp <= 0)
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "デビル・プロミス";
                            }
                            else
                            {
                                this.pa = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "ギガント・スレイヤー";
                            }
                            break;
                        case 1:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "呪怨殺";
                            break;
                        case 2:
                            List<MainCharacter> group = new List<MainCharacter>();
                            if (mc != null && !mc.Dead) { group.Add(mc); }
                            if (sc != null && !sc.Dead) { group.Add(sc); }
                            if (tc != null && !tc.Dead) { group.Add(tc); }
                            MainCharacter current = group[AP.Math.RandomInteger(group.Count - 1)];
                            if (current.CurrentPhysicalAttackDown <= 0)
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "深淵の理";
                                this.target = target;
                            }
                            else
                            {
                                this.pa = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "ギガント・スレイヤー";
                            }
                            break;
                    }
                    break;
                case Database.ENEMY_BALANCE_IDLE:
                    rand = AP.Math.RandomInteger(3);
                    switch (rand)
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "全ては灰に";
                            break;
                        case 1:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "生命の輝き";
                            break;
                        case 2:
                            if (this.currentAfterReviveHalf <= 0)
                            {
                                this.PA = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "オーン・プリゼンス";
                            }
                            else
                            {
                                this.pa = PlayerAction.SpecialSkill;
                                this.ActionLabel.Text = "レヴェルの唄";
                            }
                            break;
                    }
                    break;
                case Database.ENEMY_GO_FLAME_SLASHER:
                    if (this.AI_TacticsNumber == 0)
                    {
                        this.PA = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "煉獄弾";
                    }
                    else if (this.AI_TacticsNumber == 1)
                    {
                        if ((mc != null && !mc.Dead && mc.CurrentFireDamage2 <= 0) ||
                            (sc != null && !sc.Dead && sc.CurrentFireDamage2 <= 0) ||
                            (tc != null && !tc.Dead && tc.CurrentFireDamage2 <= 0))
                        {
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "禍の炎";
                        }
                        else
                        {
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ジ・エンド";
                        }
                    }
                    break;
                case Database.ENEMY_DEVIL_CHILDREN:
                    if (AI_TacticsNumber == 0)
                    {
                        if (this.currentBlackMagic <= 0)
                        {
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "暗黒の詠唱術";
                        }
                        else
                        {
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "クロマティック・バレット";
                        }
                    }
                    else if (AI_TacticsNumber == 1)
                    {
                        this.PA = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "無音の真空波";
                    }
                    else if (AI_TacticsNumber == 2)
                    {
                        if (this.currentLife <= this.MaxLife/2)
                        {
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "異常再生";
                        }
                        else
                        {
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "超高温熱波動";
                        }
                    }
                    else if (AI_TacticsNumber == 3)
                    {
                        this.PA = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "クロマティック・バレット";
                    }
                    break;

                case Database.ENEMY_HOWLING_HORROR:
                    if (AI_TacticsNumber == 0)
                    {
                        this.PA = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "スペクター・ヴォイス";
                    }
                    else if (AI_TacticsNumber == 1)
                    {
                        this.PA = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "無慈悲な叫び声";
                    }
                    else
                    {
                        this.PA = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "ダーク・シミュラクラム";
                    }
                    break;
                case Database.ENEMY_PAIN_ANGEL:
                    rand = AP.Math.RandomInteger(3);
                    switch (rand)
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "フェイブリオル・ランス";
                            break;
                        case 1:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "安らかな死別";
                            break;
                        case 2:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ダンシング・ソード";
                            break;
                    }
                    break;
                case Database.ENEMY_CHAOS_WARDEN:
                    if (this.AI_TacticsNumber == 0)
                    {
                        this.PA = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "カオス・デスペラート";
                    }
                    else if (this.AI_TacticsNumber == 1)
                    {
                        this.PA = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "マリア・ダンセル";
                    }
                    else
                    {
                        this.PA = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "調律の破壊";
                    }
                    break;
                case Database.ENEMY_DOOM_BRINGER:
                    rand = AP.Math.RandomInteger(4);
                    switch (rand)
                    {
                        case 0:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ディレンジド・アート";
                            break;
                        case 1:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ヘル・サークル";
                            break;
                        case 2:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "無垢のひと振り";
                            break;
                        case 3:
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ハーシュ・カッター";
                            break;
                    }
                    break;

                // ４階、ボス
                case Database.ENEMY_BOSS_LEGIN_ARZE:
                case Database.ENEMY_BOSS_LEGIN_ARZE_1:
                case Database.ENEMY_BOSS_LEGIN_ARZE_2:
                case Database.ENEMY_BOSS_LEGIN_ARZE_3:
                    if (this.AI_TacticsNumber == 0)
                    {
                        if (target.CurrentAusterityMatrixOmega <= 0)
                        {
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "アウステリティ・マトリクス・Ω";
                            this.target = target;
                        }
                        else
                        {
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "アビスの意志";
                        }
                    }
                    else if (this.AI_TacticsNumber == 1)
                    {
                        if ((sc != null) && (!sc.Dead) && (sc.CurrentAbyssFire <= 0))
                        {
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "アビス・ファイア";
                            this.target = sc;
                        }
                        else if ((mc != null) && (!mc.Dead) && (mc.CurrentIchinaruHomura <= 0))
                        {
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "壱なる焔";
                            this.target = mc;
                        }
                        else if ((sc != null) && (!sc.Dead) && (sc.CurrentBlackFire <= 0) && (this.name != Database.ENEMY_BOSS_LEGIN_ARZE_1))
                        {
                            SetupActionCommand(this, sc, PlayerAction.UseSpell, Database.BLACK_FIRE);
                        }
                        else if ((mc != null) && (!mc.Dead) && (mc.CurrentBlackFire <= 0) && (this.name != Database.ENEMY_BOSS_LEGIN_ARZE_1))
                        {
                            SetupActionCommand(this, mc, PlayerAction.UseSpell, Database.BLACK_FIRE);
                        }
                        else
                        {
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "アビスの意志";
                        }
                    }
                    else if (this.AI_TacticsNumber == 2)
                    {
                        if ((sc != null) && (!sc.Dead) && (sc.CurrentIchinaruHomura <= 0))
                        {
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "壱なる焔";
                            this.target = sc;
                        }
                        else if ((mc != null) && (!mc.Dead) && (mc.CurrentAbyssFire <= 0))
                        {
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "アビス・ファイア";
                            this.target = mc;
                        }
                        else if ((sc != null) && (!sc.Dead) && (sc.CurrentBlackFire <= 0) && (this.name != Database.ENEMY_BOSS_LEGIN_ARZE_1))
                        {
                            SetupActionCommand(this, sc, PlayerAction.UseSpell, Database.BLACK_FIRE);
                        }
                        else if ((mc != null) && (!mc.Dead) && (mc.CurrentBlackFire <= 0) && (this.name != Database.ENEMY_BOSS_LEGIN_ARZE_1))
                        {
                            SetupActionCommand(this, mc, PlayerAction.UseSpell, Database.BLACK_FIRE);
                        }
                        else
                        {
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "アビスの意志";
                        }
                    }
                    else if (this.AI_TacticsNumber == 3)
                    {
                        //if ((this.CurrentLightAndShadow <= 0) && (this.name != Database.ENEMY_BOSS_LEGIN_ARZE_1))
                        //{
                        //    this.PA = PlayerAction.SpecialSkill;
                        //    this.ActionLabel.Text = "ライト・アンド・シャドウ";
                        //    this.target = this;
                        //}
                        if (this.currentEternalDroplet <= 0)
                        {
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "エターナル・ドロップレット";
                            this.target = this;
                        }
                        else
                        {
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "アビスの意志";
                        }
                    }
                    else if (this.AI_TacticsNumber == 4)
                    {
                        if ((this.CurrentVoiceOfAbyss <= 0) && (this.name != Database.ENEMY_BOSS_LEGIN_ARZE_1))
                        {
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ヴォイス・オブ・アビス";
                            this.target = target;
                        }
                        else
                        {
                            this.PA = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "アビスの意志";
                        }
                    }

                    this.AI_TacticsNumber++;
                    if (this.AI_TacticsNumber > 4) { this.AI_TacticsNumber = 0; }
                    break;
                #endregion
                #region "５階"
                case Database.ENEMY_BOSS_BYSTANDER_EMPTINESS:
                    this.PA = PlayerAction.SpecialSkill;
                    this.ActionLabel.Text = "時間律の支配";
                    this.target = this;
                    break;
                    //rand = AP.Math.RandomInteger(3);
                    //switch (rand)
                    //{
                    //    case 0:
                    //        this.PA = PlayerAction.SpecialSkill;
                    //        this.ActionLabel.Text = "時間律（憎業）";
                    //        break;
                    //    case 1:
                    //        this.PA = PlayerAction.SpecialSkill;
                    //        this.ActionLabel.Text = "時間律（零空）";
                    //        break;
                    //    case 2:
                    //        this.PA = PlayerAction.SpecialSkill;
                    //        this.ActionLabel.Text = "時間律（盛栄）";
                    //        break;
                    //    case 3:
                    //        this.PA = PlayerAction.SpecialSkill;
                    //        this.ActionLabel.Text = "時間律（絶剣）";
                    //        break;
                    //    case 4:
                    //        this.PA = PlayerAction.SpecialSkill;
                    //        this.ActionLabel.Text = "時間律（緑永）";
                    //        break;
                    //    case 5:
                    //        this.PA = PlayerAction.SpecialSkill;
                    //        this.ActionLabel.Text = "完全絶対時間律（終焉）";
                    //        break;
                    //}
                    //break;
                case Database.ENEMY_PHOENIX:
                    if (this.AI_TacticsNumber == 0)
                    {
                        this.PA = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "戦慄の金切り声";
                        this.AI_TacticsNumber++;
                    }
                    else if (this.AI_TacticsNumber == 1)
                    {
                        this.PA = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "焼き尽くす煉獄炎";
                        this.AI_TacticsNumber++;
                    }
                    else if (this.AI_TacticsNumber == 2)
                    {
                        this.PA = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "輝ける生命";
                        this.AI_TacticsNumber = 0;
                    }
                    break;
                case Database.ENEMY_JUDGEMENT:
                    if (this.AI_TacticsNumber == 0)
                    {
                        this.PA = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "聖者の裁き";
                        this.AI_TacticsNumber++;
                    }
                    else if (this.AI_TacticsNumber == 1)
                    {
                        this.PA = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "福音";
                        this.AI_TacticsNumber++;
                    }
                    else if (this.AI_TacticsNumber == 2)
                    {
                        this.PA = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "解放の賛歌";
                        this.AI_TacticsNumber = 0;
                    }
                    break;
                case Database.ENEMY_NINE_TAIL:
                    if (this.AI_TacticsNumber == 0)
                    {
                        this.pa = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "ベジェ・テイル・アタック";
                    }
                    else if (this.AI_TacticsNumber == 1)
                    {
                        this.pa = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "喰らいつき";
                    }
                    else
                    {
                        this.pa = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "隕石を呼ぶ声";
                    }
                    this.AI_TacticsNumber++;
                    break;
                case Database.ENEMY_EMERALD_DRAGON:
                    if (this.AI_TacticsNumber == 0)
                    {
                        this.pa = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "圧死の視線"; // ライフ１、蘇生不可
                        this.target = target;
                    }
                    else if (this.AI_TacticsNumber == 1)
                    {
                        this.pa = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "イル・メギド・ブレス"; // 全員、最大ライフ－１のダメージ
                        this.target = target;
                    }
                    else if (this.AI_TacticsNumber == 2)
                    {
                        this.pa = PlayerAction.SpecialSkill;
                        this.ActionLabel.Text = "炎と氷の爆発";
                        this.target = target;
                    }
                    this.AI_TacticsNumber++;
                    break;
                #endregion
                #region "真実世界"
                case Database.ENEMY_LAST_RANA_AMILIA:
                    // 相手が行動直前なら防御姿勢
                    if (target.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - 70)
                    {
                        this.pa = PlayerAction.Defense;
                        this.ActionLabel.Text = Database.DEFENSE_JP;
                    }
                    else if ((this.currentCounterAttack <= 0) && (this.currentSkillPoint >= Database.COUNTER_ATTACK_COST) && (target.CurrentSkillName == Database.CRUSHING_BLOW))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSkill, Database.COUNTER_ATTACK);
                    }
                    else if ((this.currentNegate <= 0) && (this.currentSkillPoint >= Database.NEGATE_COST) && (target.CurrentSpellName == Database.CHILL_BURN))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSkill, Database.NEGATE);
                    }
                    else if ((this.currentOneImmunity <= 0) && (this.currentMana >= Database.ONE_IMMUNITY_COST))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.ONE_IMMUNITY);
                    }
                    //else if ((this.currentNothingOfNothingness <= 0) && (this.currentSkillPoint >= Database.NOTHING_OF_NOTHINGNESS_COST) && (this.currentInstantPoint >= this.MaxInstantPoint))
                    //{
                    //    SetupActionCommand(this, this, PlayerAction.UseSkill, Database.NOTHING_OF_NOTHINGNESS);
                    //}
                    else if ((!this.StillNotAction2) && (this.currentEclipseEnd <= 0) && (this.currentMana >= Database.ECLIPSE_END_COST) && (this.currentInstantPoint >= this.MaxInstantPoint))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.ECLIPSE_END);
                        this.StillNotAction2 = true;
                    }
                    else if ((this.currentVoidExtraction <= 0) && (this.currentSkillPoint >= Database.VOID_EXTRACTION_COST))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSkill, Database.VOID_EXTRACTION);
                    }
                    else if ((this.currentPromisedKnowledge <= 0) && (this.currentMana >= Database.PROMISED_KNOWLEDGE_COST))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.PROMISED_KNOWLEDGE);
                    }
                    else if ((this.currentFutureVision <= 0) && (this.currentSkillPoint >= Database.FUTURE_VISION_COST) && (this.currentInstantPoint >= this.MaxInstantPoint))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSkill, Database.FUTURE_VISION);
                    }
                    else if ((target.CurrentAbsoluteZero <= 0) && (this.currentMana >= Database.ABSOLUTE_ZERO_COST) && (this.currentInstantPoint >= this.MaxInstantPoint))
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.ABSOLUTE_ZERO);
                    }
                    else if ((target.CurrentImpulseHitValue < 3) && (this.currentSkillPoint >= Database.IMPULSE_HIT_COST))
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSkill, Database.IMPULSE_HIT);
                    }
                    else if ((target.CurrentMana >= target.MaxMana / 5) && (this.currentMana >= Database.DOOM_BLADE_COST))
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.DOOM_BLADE);
                    }
                    else if ((this.currentMana >= Database.WHITE_OUT_COST))
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.WHITE_OUT);
                    }
                    else if ((this.currentSkillPoint >= Database.ENIGMA_SENSE_COST))
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSkill, Database.ENIGMA_SENSE);
                    }
                    else
                    {
                        this.pa = PlayerAction.Defense;
                        this.ActionLabel.Text = Database.DEFENSE_JP;
                    }
                    break;
                case Database.ENEMY_LAST_SINIKIA_KAHLHANZ:
                    if (this.currentEclipseEnd > 0) { StillNotAction1 = true; }

                    // 相手が行動直前なら防御姿勢
                    if (target.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - 50)
                    {
                        this.pa = PlayerAction.Defense;
                        this.ActionLabel.Text = Database.DEFENSE_JP;
                    }
                    else if ((this.currentEclipseEnd <= 0) && (this.currentInstantPoint >= this.MaxInstantPoint) && (this.currentMana >= Database.ECLIPSE_END_COST) && this.StillNotAction1 == false)
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.ECLIPSE_END);
                    }
                    else if ((this.currentCounterAttack <= 0) && (this.currentSkillPoint >= Database.COUNTER_ATTACK_COST) && (target.CurrentSkillName == Database.CRUSHING_BLOW))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSkill, Database.COUNTER_ATTACK);
                    }
                    else if ((this.currentNegate <= 0) && (this.currentSkillPoint >= Database.NEGATE_COST) && (target.CurrentSpellName == Database.CHILL_BURN))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSkill, Database.NEGATE);
                    }
                    else if ((target.CurrentAusterityMatrix <= 0) && (this.currentMana >= Database.AUSTERITY_MATRIX_COST))
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.AUSTERITY_MATRIX);
                    }
                    //else if ((this.currentOneImmunity <= 0) && (this.currentMana >= Database.ONE_IMMUNITY_COST))
                    //{
                    //    SetupActionCommand(this, this, PlayerAction.UseSpell, Database.ONE_IMMUNITY);
                    //}
                    else if ((target.CurrentNoGainLife <= 0) && (this.currentMana >= Database.DEMONIC_IGNITE_COST))
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.DEMONIC_IGNITE);
                    }
                    //else if ((this.currentPromisedKnowledge <= 0) && (this.currentMana >= Database.PROMISED_KNOWLEDGE_COST))
                    //{
                    //    SetupActionCommand(this, this, PlayerAction.UseSpell, Database.PROMISED_KNOWLEDGE);
                    //}
                    //else if ((target.CurrentSigilOfHomura <= 0) && (this.currentMana <= Database.SIGIL_OF_HOMURA_COST))
                    //{
                    //    SetupActionCommand(this, target, PlayerAction.UseSpell, Database.SIGIL_OF_HOMURA);
                    //}
                    //else if ((this.currentRedDragonWill <= 0) && (this.currentMana >= Database.RED_DRAGON_WILL_COST))
                    //{
                    //    SetupActionCommand(this, this, PlayerAction.UseSpell, Database.RED_DRAGON_WILL);
                    //}
                    //else if ((target.CurrentEnrageBlast <= 0) && (this.currentMana >= Database.ENRAGE_BLAST_COST))
                    //{
                    //    SetupActionCommand(this, target, PlayerAction.UseSpell, Database.ENRAGE_BLAST);
                    //}
                    //else if ((this.CurrentGaleWind <= 0) && (this.currentMana >= Database.GALE_WIND_COST))
                    //{
                    //    SetupActionCommand(this, this, PlayerAction.UseSpell, Database.GALE_WIND);
                    //}
                    else if (this.currentMana >= Database.PIERCING_FLAME_COST)
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.PIERCING_FLAME);
                    }
                    //else if ((this.currentMana >= this.MaxMana / 2) && (target.CurrentLife <= target.MaxLife) && (this.currentInstantPoint >= this.MaxInstantPoint))
                    //{
                    //    SetupActionCommand(this, target, PlayerAction.UseSpell, Database.ASCENDANT_METEOR);
                    //}
                    else
                    {
                        this.pa = PlayerAction.Defense;
                        this.ActionLabel.Text = Database.DEFENSE_JP;
                    }
                    break;
                case Database.ENEMY_LAST_OL_LANDIS:
                    // 相手が行動直前時は防御を選択
                    int RESPONSE_BORDER_OL = 20;
                    if (target.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - RESPONSE_BORDER_OL)
                    {
                        this.pa = PlayerAction.Defense;
                        this.ActionLabel.Text = Database.DEFENSE_JP;
                    }
                    else if (!this.OpponentUseInstantPoint)
                    {
                        if (this.currentSkillPoint <= 5)
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSkill, Database.INNER_INSPIRATION);
                        }
                        else if ((this.currentLife <= this.MaxLife * 3 / 5) && (this.currentMana >= Database.CELESTIAL_NOVA_COST))
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSpell, Database.CELESTIAL_NOVA);
                        }
                        else if ((this.currentCounterAttack <= 0) && (this.currentSkillPoint >= Database.COUNTER_ATTACK_COST))
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSkill, Database.COUNTER_ATTACK);
                        }
                        else if ((this.currentNegate <= 0) && (this.currentSkillPoint >= Database.NEGATE_COST))
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSkill, Database.NEGATE);
                        }
                        else if ((this.currentMana >= Database.DISPEL_MAGIC_COST) &&
                                 (target.CurrentSaintPower > 0 || target.CurrentFlameAura > 0 || target.CurrentHeatBoost > 0 || target.CurrentHolyBreaker > 0))
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.DISPEL_MAGIC);
                        }
                        else if ((this.currentMana >= Database.TRANQUILITY_COST) &&
                                 (target.CurrentGaleWind > 0 || target.CurrentWordOfFortune > 0))
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.TRANQUILITY);
                        }
                        else if ((this.currentBlackContract <= 0) && (this.currentMana >= Database.BLACK_CONTRACT_COST))
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSpell, Database.BLACK_CONTRACT);
                        }
                        else if ((this.currentFlameAura <= 0) && ((this.currentBlackContract > 0) || (this.currentMana >= Database.FLAME_AURA_COST)))
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSpell, Database.FLAME_AURA);
                        }
                        else if ((this.currentPhantasmalWind <= 0) && ((this.currentBlackContract > 0) || (this.currentMana >= Database.PHANTASMAL_WIND_COST)))
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSpell, Database.PHANTASMAL_WIND);
                        }
                        else if ((target.CurrentImpulseHitValue < 2) && ((this.currentBlackContract > 0) || (this.currentSkillPoint >= Database.IMPULSE_HIT_COST)))
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSkill, Database.IMPULSE_HIT);
                        }
                        else if ((target.CurrentOnslaughtHitValue < 2) && ((this.currentBlackContract > 0) || (this.currentSkillPoint >= Database.ONSLAUGHT_HIT_COST)))
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSkill, Database.ONSLAUGHT_HIT);
                        }
                        else if ((target.CurrentConcussiveHitValue < 2) && ((this.currentBlackContract > 0) || (this.currentSkillPoint >= Database.CONCUSSIVE_HIT_COST)))
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSkill, Database.CONCUSSIVE_HIT);
                        }
                        else if ((this.currentBlackContract > 0) || (this.currentSkillPoint >= Database.DOUBLE_SLASH_COST))
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSkill, Database.DOUBLE_SLASH);
                        }
                        else
                        {
                            this.pa = PlayerAction.Defense;
                            this.ActionLabel.Text = Database.DEFENSE_JP;
                        }
                    }
                    else
                    {
                        if (this.beforeSkillName != Database.CARNAGE_RUSH)
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSkill, Database.CARNAGE_RUSH);
                        }
                        else
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.GENESIS);
                        }
                    }
                    break;
                case Database.ENEMY_LAST_VERZE_ARTIE:
                    // pattern
                    if (this.currentFutureVision > 0)
                    {
                        this.AI_TacticsNumber = 9;
                    }
                    else if (target.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - 15 && TruthActionCommand.IsDamage(target.ActionLabel.Text))
                    {
                        this.AI_TacticsNumber = 10;
                    }
                    else if (((this.currentLife < this.MaxLife / 2) && (target.CurrentLife > target.MaxLife / 2)) ||
                             (this.AI_TacticsNumber == 4 && target.CurrentLife > target.MaxLife / 2))
                    {
                        this.AI_TacticsNumber = 4;
                    }
                    else if ((this.beforePA == PlayerAction.UseSpell && this.beforeSpellName != Database.ONE_IMMUNITY) ||
                             (this.beforePA == PlayerAction.UseSkill && (this.beforeSkillName != Database.STANCE_OF_MYSTIC && 
                                                                         this.beforeSkillName != Database.NEGATE &&
                                                                         this.beforeSkillName != Database.COUNTER_ATTACK &&
                                                                         this.beforeSkillName != Database.FUTURE_VISION)))
                    {
                        if (this.currentOneImmunity <= 0 || this.currentStanceOfMysticValue < 3 || this.currentNegate <= 0 || this.currentCounterAttack <= 0 || this.currentFutureVision <= 0)
                        {
                            this.AI_TacticsNumber = 0;
                        }
                        else
                        {
                            this.AI_TacticsNumber = 1;
                        }
                    }
                    else if (target.PA == PlayerAction.Defense)
                    {
                        this.AI_TacticsNumber = 8;
                    }
                    else if ((target.PA == PlayerAction.UseSpell && TruthActionCommand.IsDamage(target.CurrentSpellName)) ||
                             (target.PA == PlayerAction.UseSkill && TruthActionCommand.IsDamage(target.CurrentSkillName)) ||
                             (target.PA == PlayerAction.NormalAttack))
                    {
                        this.AI_TacticsNumber = 1;
                    }
                    else if ((target.PA == PlayerAction.UseSpell && TruthActionCommand.GetBuffType(target.CurrentSpellName) == TruthActionCommand.BuffType.Up) ||
                             (target.PA == PlayerAction.UseSkill && TruthActionCommand.GetBuffType(target.CurrentSkillName) == TruthActionCommand.BuffType.Up))
                    {
                        if (target.CurrentAusterityMatrix <= 0 || this.currentNothingOfNothingness <= 0)
                        {
                            this.AI_TacticsNumber = 3;
                        }
                        else
                        {
                            this.AI_TacticsNumber = 1;
                        }
                    }
                    else
                    {
                        if (this.currentEverDroplet <= 0 || this.currentWordOfLife <= 0)
                        {
                            this.AI_TacticsNumber = 7;
                        }
                        else if (this.DetectCannotBeFrozen == false || this.DetectCannotBeParalyze == false || this.DetectCannotBeStun == false)
                        {
                            this.AI_TacticsNumber = 2;
                        }
                        else if (target.CurrentEnrageBlast <= 0 || target.CurrentBlazingField <= 0 || target.CurrentDamnation <= 0 || this.currentPainfulInsanity <= 0)
                        {
                            this.AI_TacticsNumber = 6;
                        }
                        else
                        {
                            this.AI_TacticsNumber = 1;
                        }
                    }

                    // tac
                    if (this.AI_TacticsNumber == 0)
                    {
                        if (this.currentOneImmunity <= 0)
                        {
                            SetupActionWisely(this, this, Database.ONE_IMMUNITY);
                        }
                        else if (this.currentStanceOfDeath <= 0)
                        {
                            SetupActionWisely(this, this, Database.STANCE_OF_DEATH);
                        }
                        else if (this.currentStanceOfMysticValue < 3)
                        {
                            SetupActionWisely(this, this, Database.STANCE_OF_MYSTIC);
                        }
                        else if (this.currentCounterAttack <= 0)
                        {
                            SetupActionWisely(this, this, Database.COUNTER_ATTACK);
                        }
                        else if (this.currentNegate <= 0)
                        {
                            SetupActionWisely(this, this, Database.NEGATE);
                        }
                        else if ((this.currentFutureVision <= 0) && (this.currentInstantPoint >= this.MaxInstantPoint))
                        {
                            SetupActionWisely(this, this, Database.FUTURE_VISION);
                        }
                    }
                    else if (this.AI_TacticsNumber == 1)
                    {
                        if (this.currentFlameAura <= 0)
                        {
                            SetupActionWisely(this, this, Database.FLAME_AURA);
                        }
                        else if (this.currentFrozenAura <= 0)
                        {
                            SetupActionWisely(this, this, Database.FROZEN_AURA);
                        }
                        else if (this.currentGaleWind <= 0)
                        {
                            SetupActionWisely(this, this, Database.GALE_WIND);
                        }
                        else
                        {
                            SetupActionWisely(this, this, Database.NEUTRAL_SMASH);
                        }
                    }
                    else if (this.AI_TacticsNumber == 2)
                    {
                        if ((target.CurrentFrozen <= 0) && (this.DetectCannotBeFrozen == false))
                        {
                            SetupActionWisely(this, target, Database.CHILL_BURN);
                        }
                        else if ((target.CurrentParalyze <= 0) && (this.DetectCannotBeParalyze == false))
                        {
                            SetupActionWisely(this, target, Database.SURPRISE_ATTACK);
                        }
                        else if ((target.CurrentStunning <= 0) && (this.DetectCannotBeStun == false) && (this.currentInstantPoint >= this.MaxInstantPoint))
                        {
                            SetupActionWisely(this, target, Database.CRUSHING_BLOW);
                        }
                    }
                    else if (this.AI_TacticsNumber == 3)
                    {
                        if ((this.currentEclipseEnd <= 0) && (this.currentInstantPoint >= this.MaxInstantPoint))
                        {
                            SetupActionWisely(this, this, Database.ECLIPSE_END);
                        }
                        else if ((this.currentHymnContract <= 0) && (this.currentInstantPoint >= this.MaxInstantPoint))
                        {
                            SetupActionWisely(this, this, Database.HYMN_CONTRACT);
                        }
                        else if ((this.currentAusterityMatrix <= 0) && (this.currentInstantPoint >= this.MaxInstantPoint))
                        {
                            SetupActionWisely(this, target, Database.AUSTERITY_MATRIX);
                        }
                        else if ((this.currentNothingOfNothingness <= 0) && (this.currentInstantPoint >= this.MaxInstantPoint))
                        {
                            SetupActionWisely(this, this, Database.NOTHING_OF_NOTHINGNESS);
                        }
                    }
                    else if (this.AI_TacticsNumber == 4)
                    {
                        if (this.currentLife < this.MaxLife / 4)
                        {
                            SetupActionWisely(this, this, Database.CELESTIAL_NOVA);
                        }
                        else if (this.currentGaleWind <= 0)
                        {
                            SetupActionWisely(this, this, Database.GALE_WIND);
                        }
                        else if (this.currentWordOfFortune <= 0)
                        {
                            SetupActionWisely(this, this, Database.WORD_OF_FORTUNE);
                        }
                        else if (this.currentLife < this.MaxLife / 2)
                        {
                            SetupActionWisely(this, this, Database.CELESTIAL_NOVA);
                        }
                        else if (this.currentLife >= this.MaxLife / 2)
                        {
                            SetupActionWisely(this, target, Database.CELESTIAL_NOVA);
                        }
                    }
                    else if (this.AI_TacticsNumber == 5)
                    {
                        if (this.currentGaleWind <= 0)
                        {
                            SetupActionWisely(this, this, Database.GALE_WIND);
                        }
                        else
                        {
                            SetupActionWisely(this, this, Database.INNER_INSPIRATION);
                        }
                    }
                    else if (this.AI_TacticsNumber == 6)
                    {
                        if (this.currentEnrageBlast <= 0)
                        {
                            SetupActionWisely(this, this, Database.ENRAGE_BLAST);
                        }
                        else if (target.CurrentBlazingField <= 0)
                        {
                            SetupActionWisely(this, target, Database.BLAZING_FIELD);
                        }
                        else if (target.CurrentDamnation <= 0)
                        {
                            SetupActionWisely(this, target, Database.DAMNATION);
                        }
                        else if ((this.currentPainfulInsanity <= 0) && (this.currentInstantPoint >= this.MaxInstantPoint))
                        {
                            SetupActionWisely(this, this, Database.PAINFUL_INSANITY);
                        }
                    }
                    else if (this.AI_TacticsNumber == 7)
                    {
                        if (this.currentEverDroplet <= 0)
                        {
                            SetupActionWisely(this, this, Database.EVER_DROPLET);
                        }
                        else if (this.currentWordOfLife <= 0)
                        {
                            SetupActionWisely(this, this, Database.WORD_OF_LIFE);
                        }
                    }
                    else if (this.AI_TacticsNumber == 8)
                    {
                        if (target.PA == PlayerAction.Defense)
                        {
                            SetupActionWisely(this, target, Database.PSYCHIC_WAVE);
                        }
                        else if (target.PA == PlayerAction.Defense)
                        {
                            SetupActionWisely(this, target, Database.PIERCING_FLAME);
                        }
                        else if (target.PA == PlayerAction.Defense)
                        {
                            SetupActionWisely(this, target, Database.WORD_OF_POWER);
                        }
                    }
                    else if (this.AI_TacticsNumber == 9)
                    {
                        // action check
                        if (this.currentTimeStop > 0)
                        {
                            this.StillNotAction1 = true;
                        }
                        if (this.currentStanceOfDouble > 0)
                        {
                            this.StillNotAction2 = true;
                        }
                        
                        // tac
                        if (this.currentTimeStop <= 0 && this.StillNotAction1 == false)
                        {
                            SetupActionWisely(this, this, Database.TIME_STOP);
                        }
                        else if (this.currentBlackContract <= 0)
                        {
                            SetupActionWisely(this, this, Database.BLACK_CONTRACT);
                        }
                        else if (target.CurrentSigilOfHomura <= 0)
                        {
                            SetupActionWisely(this, target, Database.SIGIL_OF_HOMURA);
                        }
                        else if (this.currentGaleWind <= 0)
                        {
                            SetupActionWisely(this, this, Database.GALE_WIND);
                        }
                        else if (this.beforeSpellName != Database.ZETA_EXPLOSION)
                        {
                            SetupActionWisely(this, target, Database.ZETA_EXPLOSION);
                        }
                        else if (this.beforeSpellName == Database.ZETA_EXPLOSION && this.currentStanceOfDouble <= 0)
                        {
                            SetupActionWisely(this, this, Database.STANCE_OF_DOUBLE);
                        }
                        else
                        {
                            SetupActionWisely(this, target, Database.ZETA_EXPLOSION);
                        }
                    }
                    else if (this.AI_TacticsNumber == 10)
                    {
                        this.pa = PlayerAction.Defense;
                        this.ActionLabel.Text = Database.DEFENSE_JP;
                    }
                    break;
                case Database.ENEMY_LAST_SIN_VERZE_ARTIE:
                    int random1 = AP.Math.RandomInteger(4);
                    int RESPONSE_BORDER = 10;
                   
                    // 相手が行動直前時は防御を選択
                    if (target.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - RESPONSE_BORDER)
                    {
                        this.pa = PlayerAction.Defense;
                        this.ActionLabel.Text = Database.DEFENSE_JP;
                    }
                    // ライフが減ってきた場合は回復
                    else if (this.currentLife < this.MaxLife / 3)
                    {
                        SetupActionWisely(this, this, Database.CELESTIAL_NOVA);
                    }
                    // 特定のBUFFが付いている場合、それを強化するためのシーケンスを選択する。
                    else if (this.currentFlameAura > 0 && this.currentFrozenAura <= 0 && this.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - RESPONSE_BORDER)
                    {
                        SetupActionWisely(this, this, Database.FROZEN_AURA);
                    }
                    else if (this.currentFrozenAura > 0 && this.currentFlameAura <= 0 && this.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - RESPONSE_BORDER)
                    {
                        SetupActionWisely(this, this, Database.FLAME_AURA);
                    }
                    else if (this.currentWordOfLife > 0 && this.currentEverDroplet <= 0 && this.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - RESPONSE_BORDER)
                    {
                        SetupActionWisely(this, this, Database.EVER_DROPLET);
                    }
                    else if (this.currentEverDroplet > 0 && this.currentWordOfLife <= 0 && this.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - RESPONSE_BORDER)
                    {
                        SetupActionWisely(this, this, Database.WORD_OF_LIFE);
                    }
                    else if (this.currentDeflection > 0 && this.currentMirrorImage <= 0 && this.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - RESPONSE_BORDER)
                    {
                        SetupActionWisely(this, this, Database.MIRROR_IMAGE);
                    }
                    else if (this.currentMirrorImage > 0 && this.currentDeflection <= 0 && this.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - RESPONSE_BORDER)
                    {
                        SetupActionWisely(this, this, Database.DEFLECTION);
                    }
                    else if (target.CurrentBlueDragonWill > 0 && this.currentRedDragonWill <= 0 && this.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - RESPONSE_BORDER)
                    {
                        SetupActionWisely(this, this, Database.RED_DRAGON_WILL);
                    }
                    else if (this.currentRedDragonWill > 0 && target.CurrentBlueDragonWill <= 0 && this.BattleBarPos >= Database.BASE_TIMER_BAR_LENGTH - RESPONSE_BORDER)
                    {
                        SetupActionWisely(this, this, Database.BLUE_DRAGON_WILL);
                    }
                    else if (this.currentTruthVision > 0 && this.currentPainfulInsanity <= 0)
                    {
                        SetupActionWisely(this, this, Database.PAINFUL_INSANITY);
                    }
                    else if (this.currentPainfulInsanity > 0 && this.currentTruthVision <= 0)
                    {
                        SetupActionWisely(this, this, Database.TRUTH_VISION);
                    }
                    // 生命力カウンター（２）依存戦術
                    else if (this.currentFutureVision <= 0 && this.currentInstantPoint >= this.MaxInstantPoint && random1 == 0 && this.CurrentLifeCountValue == 2)
                    {
                        SetupActionWisely(this, target, Database.FUTURE_VISION);
                    }
                    // ワープゲート用に常にランダム
                    else
                    {
                        List<string> commands = new List<string>(TruthActionCommand.GetActionList(this));
                        // 基本動作または弱コマンドはリストから除外
                        commands.Remove(Database.ATTACK_EN);
                        commands.Remove(Database.DEFENSE_EN);
                        commands.Remove(Database.STAY_EN);
                        commands.Remove(Database.WEAPON_SPECIAL_EN);
                        commands.Remove(Database.WEAPON_SPECIAL_LEFT_EN);
                        commands.Remove(Database.TAMERU_EN);
                        commands.Remove(Database.ACCESSORY_SPECIAL_EN);
                        commands.Remove(Database.ACCESSORY_SPECIAL2_EN);
                        commands.Remove(Database.FRESH_HEAL);
                        commands.Remove(Database.RESURRECTION);
                        commands.Remove(Database.LIFE_TAP);
                        commands.Remove(Database.DARK_BLAST);
                        commands.Remove(Database.FIRE_BALL);
                        commands.Remove(Database.FLAME_STRIKE);
                        commands.Remove(Database.ICE_NEEDLE);
                        commands.Remove(Database.STANCE_OF_FLOW);
                        commands.Remove(Database.TRANSCENDENT_WISH); // 自分が死亡してしまうので除外。
                        commands.Remove(Database.DEATH_DENY);
                        commands.Remove(Database.WORD_OF_ATTITUDE);
                        commands.Remove(Database.SEVENTH_MAGIC); // ヴェルゼの場合、特に意味はないため除外。
                        commands.Remove(Database.WARP_GATE);
                        commands.Remove(Database.ANTI_STUN); // ヴェルゼは装備でスタン耐性があるため、除外。
                        commands.Remove(Database.NEUTRAL_SMASH);
                        commands.Remove(Database.CIRCLE_SLASH);
                        commands.Remove(Database.RUMBLE_SHOUT);
                        commands.Remove(Database.REFLEX_SPIRIT); // ヴェルゼは装備でスタン耐性があるため、除外。
                        commands.Remove(Database.TRUST_SILENCE); // ヴェルゼは装備でスタン耐性があるため、除外。
                        commands.Remove(Database.RECOVER);
                        commands.Remove(Database.HARDEST_PARRY);
                        commands.Remove(Database.STANCE_OF_SUDDENNESS);
                        commands.Remove(Database.ENDLESS_ANTHEM);
                        commands.Remove(Database.GENESIS);
                        // BUFFが既に付与されている場合は除外
                        if (this.currentBloodyVengeance > 0) { commands.Remove(Database.BLOODY_VENGEANCE); }
                        if (this.currentHeatBoost > 0) { commands.Remove(Database.HEAT_BOOST); }
                        if (this.currentPromisedKnowledge > 0) { commands.Remove(Database.PROMISED_KNOWLEDGE); }
                        if (this.currentRiseOfImage > 0) { commands.Remove(Database.RISE_OF_IMAGE); }
                        if (this.currentProtection > 0) { commands.Remove(Database.PROTECTION); }
                        if (this.currentSaintPower > 0) { commands.Remove(Database.SAINT_POWER); }
                        if (this.currentGlory > 0) { commands.Remove(Database.GLORY); }
                        if (this.currentShadowPact > 0) { commands.Remove(Database.SHADOW_PACT); }
                        if (this.currentBlackContract > 0) { commands.Remove(Database.BLACK_CONTRACT); }
                        if (this.currentFlameAura > 0) { commands.Remove(Database.FLAME_AURA); }
                        if (this.currentImmortalRave > 0) { commands.Remove(Database.IMMORTAL_RAVE); }
                        if (this.currentAbsorbWater > 0) { commands.Remove(Database.ABSORB_WATER); }
                        if (this.currentMirrorImage > 0) { commands.Remove(Database.MIRROR_IMAGE); }
                        if (this.currentGaleWind > 0) { commands.Remove(Database.GALE_WIND); }
                        if (this.currentWordOfLife > 0) { commands.Remove(Database.WORD_OF_LIFE); }
                        if (this.currentWordOfFortune > 0) { commands.Remove(Database.WORD_OF_FORTUNE); }
                        if (this.currentAetherDrive > 0) { commands.Remove(Database.AETHER_DRIVE); }
                        if (this.currentEternalPresence > 0) { commands.Remove(Database.ETERNAL_PRESENCE); }
                        if (this.currentDeflection > 0) { commands.Remove(Database.DEFLECTION); }
                        if (this.currentOneImmunity > 0) { commands.Remove(Database.ONE_IMMUNITY); }
                        if (this.currentPsychicTrance > 0) { commands.Remove(Database.PSYCHIC_TRANCE); }
                        if (this.currentBlindJustice > 0) { commands.Remove(Database.BLIND_JUSTICE); }
                        //if (this.currentTranscendentWish > 0) { commands.Remove(Database.TRANSCENDENT_WISH); } // 既に除外されている。
                        if (this.currentSkyShieldValue >= 3) { commands.Remove(Database.SKY_SHIELD); }
                        if (this.currentEverDroplet > 0) { commands.Remove(Database.EVER_DROPLET); }
                        if (this.currentHolyBreaker > 0) { commands.Remove(Database.HOLY_BREAKER); }
                        if (this.currentExaltedField > 0) { commands.Remove(Database.EXALTED_FIELD); }
                        if (this.currentHymnContract > 0) { commands.Remove(Database.HYMN_CONTRACT); }
                        if (this.currentSinFortune > 0) { commands.Remove(Database.SIN_FORTUNE); }
                        if (this.currentEclipseEnd > 0) { commands.Remove(Database.ECLIPSE_END); }
                        if (this.currentFrozenAura > 0) { commands.Remove(Database.FROZEN_AURA); }
                        if (this.currentPhantasmalWind > 0) { commands.Remove(Database.PHANTASMAL_WIND); }
                        if (this.currentRedDragonWill > 0) { commands.Remove(Database.RED_DRAGON_WILL); }
                        if (this.currentStaticBarrierValue >= 3) { commands.Remove(Database.STATIC_BARRIER); }
                        if (this.currentBlueDragonWill > 0) { commands.Remove(Database.BLUE_DRAGON_WILL); }
                        //if (this.currentSeventhMagic > 0) { commands.Remove(Database.SEVENTH_MAGIC); } // 既に除外されている。
                        if (this.currentParadoxImage > 0) { commands.Remove(Database.PARADOX_IMAGE); }
                        if (this.currentCounterAttack > 0) { commands.Remove(Database.COUNTER_ATTACK); }
                        //if (this.currentAntiStun > 0) { commands.Remove(Database.ANTI_STUN); } // 既に除外されている。
                        if (this.currentStanceOfDeath > 0) { commands.Remove(Database.STANCE_OF_DEATH); }
                        if (this.currentStanceOfFlow > 0) { commands.Remove(Database.STANCE_OF_FLOW); }
                        if (this.currentStanceOfStanding > 0) { commands.Remove(Database.STANCE_OF_STANDING); }
                        if (this.currentTruthVision > 0) { commands.Remove(Database.TRUTH_VISION); }
                        if (this.currentHighEmotionality > 0) { commands.Remove(Database.HIGH_EMOTIONALITY); }
                        if (this.currentStanceOfEyes > 0) { commands.Remove(Database.STANCE_OF_EYES); }
                        if (this.currentPainfulInsanity > 0) { commands.Remove(Database.PAINFUL_INSANITY); }
                        if (this.currentNegate > 0) { commands.Remove(Database.NEGATE); }
                        if (this.currentVoidExtraction > 0) { commands.Remove(Database.VOID_EXTRACTION); }
                        if (this.currentNothingOfNothingness > 0) { commands.Remove(Database.NOTHING_OF_NOTHINGNESS); }
                        if (this.currentStanceOfDouble > 0) { commands.Remove(Database.STANCE_OF_DOUBLE); }
                        if (this.currentSwiftStep > 0) { commands.Remove(Database.SWIFT_STEP); }
                        if (this.currentVigorSense > 0) { commands.Remove(Database.VIGOR_SENSE); }
                        if (this.currentRisingAura > 0) { commands.Remove(Database.RISING_AURA); }
                        if (this.currentSmoothingMove > 0) { commands.Remove(Database.SMOOTHING_MOVE); }
                        if (this.currentAscensionAura > 0) { commands.Remove(Database.ASCENSION_AURA); }
                        if (this.currentFutureVision > 0) { commands.Remove(Database.FUTURE_VISION); }
                        //if (this.currentReflexSpirit > 0) { commands.Remove(Database.REFLEX_SPIRIT); } // 既に除外されている。
                        //if (this.currentTrustSilence > 0) { commands.Remove(Database.TRUST_SILENCE); } // 既に除外されている。
                        if (this.currentStanceOfMysticValue >= 3) { commands.Remove(Database.STANCE_OF_MYSTIC); }
                        if (this.currentNourishSense > 0) { commands.Remove(Database.NOURISH_SENSE); }
                        if (this.currentOneAuthority > 0) { commands.Remove(Database.ONE_AUTHORITY); }
                        // 相手に負BUFFが既に付与されている場合は除外
                        if (target.CurrentDamnation > 0) { commands.Remove(Database.DAMNATION); }
                        if (target.CurrentAbsoluteZero > 0) { commands.Remove(Database.ABSOLUTE_ZERO); }
                        if (target.CurrentFlashBlazeCount > 0) { commands.Remove(Database.FLASH_BLAZE); }
                        if (target.CurrentStarLightning > 0) { commands.Remove(Database.STAR_LIGHTNING); }
                        if (target.CurrentBlackFire > 0) { commands.Remove(Database.BLACK_FIRE); }
                        if (target.CurrentBlazingField > 0) { commands.Remove(Database.BLAZING_FIELD); }
                        if (target.CurrentNoGainLife > 0) { commands.Remove(Database.DEMONIC_IGNITE); }
                        if (target.CurrentWordOfMalice > 0) { commands.Remove(Database.WORD_OF_MALICE); }
                        if (target.CurrentDarkenField > 0) { commands.Remove(Database.DARKEN_FIELD); }
                        if (target.CurrentFrozen > 0) { commands.Remove(Database.CHILL_BURN); }
                        if (target.CurrentEnrageBlast > 0) { commands.Remove(Database.ENRAGE_BLAST); }
                        if (target.CurrentSigilOfHomura > 0) { commands.Remove(Database.SIGIL_OF_HOMURA); }
                        if (target.CurrentImmolate > 0) { commands.Remove(Database.IMMOLATE); }
                        if (target.CurrentAusterityMatrix > 0) { commands.Remove(Database.AUSTERITY_MATRIX); }
                        if (target.CurrentSilence > 0) { commands.Remove(Database.VANISH_WAVE); }
                        if (target.CurrentStunning > 0) { commands.Remove(Database.CRUSHING_BLOW); }
                        if (target.CurrentOnslaughtHitValue >= 3) { commands.Remove(Database.ONSLAUGHT_HIT); }
                        if (target.CurrentBlind > 0) { commands.Remove(Database.UNKNOWN_SHOCK); }
                        if (target.CurrentConcussiveHitValue >= 3) { commands.Remove(Database.CONCUSSIVE_HIT); }
                        if (target.CurrentParalyze > 0) { commands.Remove(Database.SURPRISE_ATTACK); }
                        if (target.CurrentImpulseHitValue >= 3) { commands.Remove(Database.IMPULSE_HIT); }
                        // ディスペルマジック対象が相手にかかってない場合は除外
                        if (target.CurrentProtection <= 0 && target.CurrentSaintPower <= 0 && target.CurrentAbsorbWater <= 0 && target.CurrentShadowPact <= 0 
                            && target.CurrentEternalPresence <= 0 && target.CurrentBloodyVengeance <= 0 && target.CurrentHeatBoost <= 0 && target.CurrentPromisedKnowledge <= 0
                            && target.CurrentRiseOfImage <= 0 && target.CurrentWordOfLife <= 0 && target.CurrentFlameAura <= 0
                            && target.CurrentPsychicTrance <= 0 && target.CurrentBlindJustice <= 0 && target.CurrentSkyShield <= 0
                            && target.CurrentEverDroplet <= 0 && target.CurrentHolyBreaker <= 0 && target.CurrentExaltedField <= 0
                            && target.CurrentFrozenAura <= 0 && target.CurrentPhantasmalWind <= 0 && target.CurrentRedDragonWill <= 0
                            && target.CurrentStaticBarrier <= 0 && target.CurrentBlueDragonWill <= 0 && target.CurrentSeventhMagic <= 0
                            && target.CurrentParadoxImage <= 0)
                        {
                            commands.Remove(Database.DISPEL_MAGIC);
                        }
                        // トランキリティ対象が相手にかかってない場合は除外
                        if (target.CurrentGlory <= 0 && target.CurrentGaleWind <= 0 && target.CurrentWordOfFortune <= 0 && target.CurrentBlackContract <= 0
                            && target.CurrentHymnContract <= 0 && target.CurrentImmortalRave <= 0 && target.CurrentAbsoluteZero <= 0 && target.CurrentAetherDrive <= 0
                            && target.CurrentOneImmunity <= 0 && target.CurrentHighEmotionality <= 0)
                        {
                            commands.Remove(Database.TRANQUILITY);
                        }
                        // ダメージ系統が当たる場合の除外リスト(IsDamageでは自分回復可能なCelestialNova、FlameAuraによる追加ダメージの分も入ってしまうので個別記述とする）
                        if (this.currentEclipseEnd > 0)
                        {
                            commands.Remove(Database.HOLY_SHOCK);
                            commands.Remove(Database.DEVOURING_PLAGUE);
                            commands.Remove(Database.VOLCANIC_WAVE);
                            commands.Remove(Database.LAVA_ANNIHILATION);
                            commands.Remove(Database.FROZEN_LANCE);
                            commands.Remove(Database.WORD_OF_POWER);
                            commands.Remove(Database.FLASH_BLAZE);
                            commands.Remove(Database.LIGHT_DETONATOR);
                            commands.Remove(Database.ASCENDANT_METEOR);
                            commands.Remove(Database.STAR_LIGHTNING);
                            commands.Remove(Database.BLACK_FIRE);
                            commands.Remove(Database.DEMONIC_IGNITE);
                            commands.Remove(Database.BLUE_BULLET);
                            commands.Remove(Database.WORD_OF_MALICE);
                            commands.Remove(Database.ABYSS_EYE);
                            commands.Remove(Database.DOOM_BLADE);
                            commands.Remove(Database.CHILL_BURN);
                            commands.Remove(Database.ZETA_EXPLOSION);
                            commands.Remove(Database.ENRAGE_BLAST);
                            commands.Remove(Database.PIERCING_FLAME);
                            commands.Remove(Database.IMMOLATE);
                            commands.Remove(Database.VANISH_WAVE);
                            commands.Remove(Database.STRAIGHT_SMASH);
                            commands.Remove(Database.DOUBLE_SLASH);
                            commands.Remove(Database.CRUSHING_BLOW);
                            commands.Remove(Database.SOUL_INFINITY);
                            commands.Remove(Database.ENIGMA_SENSE);
                            commands.Remove(Database.SILENT_RUSH);
                            commands.Remove(Database.OBORO_IMPACT);
                            commands.Remove(Database.STANCE_OF_STANDING);
                            commands.Remove(Database.KINETIC_SMASH);
                            commands.Remove(Database.CATASTROPHE);
                            commands.Remove(Database.CARNAGE_RUSH);
                            commands.Remove(Database.STANCE_OF_DOUBLE); // もし、前回行動がダメージ系だったら、ここで発動するとカッコ悪いのでやらない。
                            commands.Remove(Database.UNKNOWN_SHOCK);
                            commands.Remove(Database.FATAL_BLOW);
                            commands.Remove(Database.SHARP_GLARE);
                            commands.Remove(Database.CONCUSSIVE_HIT);
                            commands.Remove(Database.MIND_KILLING);
                            commands.Remove(Database.SURPRISE_ATTACK);
                            commands.Remove(Database.PSYCHIC_WAVE);
                            commands.Remove(Database.IMPULSE_HIT);
                            commands.Remove(Database.VIOLENT_SLASH);
                            commands.Remove(Database.SOUL_EXECUTION);
                        }

                        int randomNumber2 = AP.Math.RandomInteger(commands.Count);
                        this.pa = TruthActionCommand.CheckPlayerActionFromString(commands[randomNumber2]);
                        SetupActionWisely(this, target, commands[randomNumber2]);
                        this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(commands[randomNumber2]);
                    }
//                    else if (this.currentInstantPoint >= this.MaxInstantPoint && random1 == 1)
//                    {
//                        SetupActionWisely(this, target, Database.STRAIGHT_SMASH, Database.STRAIGHT_SMASH_COST);
////                        SetupActionWisely(this, target, Database.ZETA_EXPLOSION, Database.ZETA_EXPLOSION_COST);
//                    }
//                    else if (this.currentInstantPoint >= this.MaxInstantPoint && random1 == 2)
//                    {
//                        SetupActionWisely(this, target, Database.ABSOLUTE_ZERO, Database.ABSOLUTE_ZERO_COST);
//                    }
//                    else if (this.currentInstantPoint >= this.MaxInstantPoint && random1 == 3)
//                    {
//                        SetupActionWisely(this, target, Database.NOTHING_OF_NOTHINGNESS, Database.NOTHING_OF_NOTHINGNESS_COST);
//                    }
//                    else
//                    {
//                        SetupActionCommand(this, target, PlayerAction.UseSkill, Database.STRAIGHT_SMASH);
//                    }
                    break;
                #endregion
                #region "Duel対戦相手（１階）"
                case Database.DUEL_EONE_FULNEA:
                    this.PA = PlayerAction.UseSpell;
                    this.CurrentSpellName = Database.ICE_NEEDLE;
                    this.Target = target;
                    this.ActionLabel.Text = Database.ICE_NEEDLE_JP;
                    break;

                case Database.DUEL_MAGI_ZELKIS:

                    bool existItem = false;
                    ItemBackPack[] tempItem = this.GetBackPackInfo();
                    foreach (ItemBackPack value in tempItem)
                    {
                        if (value != null)
                        {
                            if (value.Name == Database.COMMON_NORMAL_RED_POTION)
                            {
                                existItem = true;
                            }
                        }
                    }

                    if ((this.CurrentLife < this.MaxLife) &&
                        (existItem))
                    {
                        this.PA = PlayerAction.UseItem;
                        this.CurrentUsingItem = Database.COMMON_NORMAL_RED_POTION;
                        this.Target = this;
                        this.ActionLabel.Text = Database.COMMON_NORMAL_RED_POTION;
                    }
                    else if (this.CurrentFlameAura <= 0)
                    {
                        this.PA = PlayerAction.UseSpell;
                        this.CurrentSpellName = Database.FLAME_AURA;
                        this.Target = this;
                        this.ActionLabel.Text = Database.FLAME_AURA_JP;
                    }
                    else
                    {
                        if (AP.Math.RandomInteger(2) <= 0)
                        {
                            this.PA = PlayerAction.NormalAttack;
                            this.Target = target;
                            this.ActionLabel.Text = Database.ATTACK_JP;
                        }
                        else
                        {
                            this.PA = PlayerAction.UseSpell;
                            this.CurrentSpellName = Database.FIRE_BALL;
                            this.Target = target;
                            this.ActionLabel.Text = Database.FIRE_BALL_JP;
                        }
                    }
                    break;

                case Database.DUEL_SELMOI_RO:
                    switch (AP.Math.RandomInteger(2))
                    {
                        case 0:
                            this.PA = PlayerAction.UseSpell;
                            this.Target = target;
                            this.CurrentSpellName = Database.WORD_OF_POWER;
                            this.ActionLabel.Text = Database.WORD_OF_POWER_JP;
                            break;
                        case 1:
                            this.PA = PlayerAction.UseSkill;
                            this.Target = target;
                            this.currentSkillName = Database.STANCE_OF_STANDING;
                            this.ActionLabel.Text = Database.STANCE_OF_STANDING_JP;
                            break;
                    }
                    break;

                case Database.DUEL_KARTIN_MAI:
                    switch (AP.Math.RandomInteger(3))
                    {
                        case 0:
                            this.pa = PlayerAction.UseSkill;
                            this.currentSkillName = Database.ENIGMA_SENSE;
                            this.ActionLabel.Text = Database.ENIGMA_SENSE_JP;
                            this.target = target;
                            break;
                        case 1:
                            if (this.currentLife >= this.MaxLife)
                            {
                                this.pa = PlayerAction.UseSpell;
                                this.currentSpellName = Database.FIRE_BALL;
                                this.ActionLabel.Text = Database.FIRE_BALL_JP;
                                this.target = target;
                            }
                            else
                            {
                                this.pa = PlayerAction.UseSpell;
                                this.currentSpellName = Database.LIFE_TAP;
                                this.ActionLabel.Text = Database.LIFE_TAP_JP;
                                this.target = this;
                            }
                            break;
                        case 2:
                            this.pa = PlayerAction.UseSpell;
                            this.currentSpellName = Database.DEVOURING_PLAGUE;
                            this.ActionLabel.Text = Database.DEVOURING_PLAGUE_JP;
                            this.target = target;
                            break;
                    }
                    break;
                case Database.DUEL_JEDA_ARUS:
                    if (this.currentSkillPoint < 25)
                    {
                        existItem = false;
                        tempItem = this.GetBackPackInfo();
                        foreach (ItemBackPack value in tempItem)
                        {
                            if (value != null)
                            {
                                if (value.Name == Database.RARE_PURE_GREEN_WATER)
                                {
                                    existItem = true;
                                }
                            }
                        }
                        if (existItem)
                        {
                            this.pa = PlayerAction.UseItem;
                            this.currentUsingItem = Database.RARE_PURE_GREEN_WATER;
                            this.ActionLabel.Text = Database.RARE_PURE_GREEN_WATER;
                            this.target = this;
                        }
                        else
                        {
                            this.pa = PlayerAction.UseSkill;
                            this.currentSkillName = Database.DOUBLE_SLASH;
                            this.ActionLabel.Text = Database.DOUBLE_SLASH_JP;
                            this.target = target;
                        }
                    }
                    else
                    {
                        switch (AP.Math.RandomInteger(2))
                        {
                            case 0:
                                existItem = false;
                                tempItem = this.GetBackPackInfo();
                                foreach (ItemBackPack value in tempItem)
                                {
                                    if (value != null)
                                    {
                                        if (value.Name == Database.RARE_PURE_WATER)
                                        {
                                            existItem = true;
                                        }
                                    }
                                }

                                if (this.currentLife < this.MaxLife * 0.5F && existItem)
                                {
                                    this.pa = PlayerAction.UseItem;
                                    this.currentUsingItem = Database.RARE_PURE_WATER;
                                    this.ActionLabel.Text = Database.RARE_PURE_WATER;
                                    this.target = this;
                                }
                                else
                                {
                                    this.pa = PlayerAction.UseSkill;
                                    this.currentSkillName = Database.DOUBLE_SLASH;
                                    this.ActionLabel.Text = Database.DOUBLE_SLASH_JP;
                                    this.target = target;
                                }
                                break;
                            case 1:
                                if (this.currentSaintPower <= 0)
                                {
                                    this.pa = PlayerAction.UseSpell;
                                    this.currentSpellName = Database.SAINT_POWER;
                                    this.ActionLabel.Text = Database.SAINT_POWER_JP;
                                    this.target = this;
                                }
                                else
                                {
                                    this.pa = PlayerAction.UseSkill;
                                    this.currentSkillName = Database.DOUBLE_SLASH;
                                    this.ActionLabel.Text = Database.DOUBLE_SLASH_JP;
                                    this.target = target;
                                }
                                break;
                            case 2:
                                break;
                        }
                    }
                    break;

                case Database.DUEL_SINIKIA_VEILHANTU:
                    switch (AP.Math.RandomInteger(1))
                    {
                        case 0:
                            if (target.PA == PlayerAction.Defense)
                            {
                                if (this.currentShadowPact <= 0)
                                {
                                    this.pa = PlayerAction.UseSpell;
                                    this.currentSpellName = Database.SHADOW_PACT;
                                    this.ActionLabel.Text = Database.SHADOW_PACT_JP;
                                    this.target = this;
                                }
                                else if (this.currentChargeCount <= 0)
                                {
                                    this.pa = PlayerAction.Charge;
                                    this.ActionLabel.Text = Database.TAMERU_JP;
                                    this.target = this;
                                }
                                else
                                {
                                    this.pa = PlayerAction.UseSpell;
                                    this.currentSpellName = Database.HOLY_SHOCK;
                                    this.ActionLabel.Text = Database.HOLY_SHOCK_JP;
                                    this.target = target;
                                }
                            }
                            else
                            {
                                this.pa = PlayerAction.UseSpell;
                                this.currentSpellName = Database.HOLY_SHOCK;
                                this.ActionLabel.Text = Database.HOLY_SHOCK_JP;
                                this.target = target;
                            }
                            break;
                    }
                    break;

                case Database.DUEL_OL_LANDIS:
                    if ((this.target.CurrentParalyze > 0) && (this.currentSkillPoint >= Database.STRAIGHT_SMASH_COST))
                    {
                        this.pa = PlayerAction.UseSkill;
                        this.currentSkillName = Database.STRAIGHT_SMASH;
                        this.ActionLabel.Text = Database.STRAIGHT_SMASH_JP;
                        this.target = target;
                    }
                    else
                    {
                        if (this.currentMana >= Database.VOLCANIC_WAVE_COST)
                        {
                            this.pa = PlayerAction.UseSpell;
                            this.currentSpellName = Database.VOLCANIC_WAVE;
                            this.ActionLabel.Text = Database.VOLCANIC_WAVE_JP;
                            this.target = target;
                        }
                        else if (this.currentSkillPoint >= Database.STRAIGHT_SMASH_COST)
                        {
                            this.pa = PlayerAction.UseSkill;
                            this.currentSkillName = Database.STRAIGHT_SMASH;
                            this.ActionLabel.Text = Database.STRAIGHT_SMASH_JP;
                            this.target = this;
                        }
                    }
                    break;

                case Database.VERZE_ARTIE_FULL:
                case Database.VERZE_ARTIE:
                    if (this.currentStanceOfFlow <= 0)
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSkill, Database.STANCE_OF_FLOW);
                    }
                    else if (this.currentCounterAttack <= 0)
                    {
                        if (this.currentSkillPoint < Database.COUNTER_ATTACK_COST)
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSkill, Database.NEUTRAL_SMASH);
                        }
                        else
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSkill, Database.COUNTER_ATTACK);
                        }
                    }
                    else if (this.currentMirrorImage <= 0)
                    {
                        if (this.currentMana < Database.MIRROR_IMAGE_COST)
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSkill, Database.NEUTRAL_SMASH);
                        }
                        else
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSpell, Database.MIRROR_IMAGE);
                        }
                    }
                    else if (this.currentMana < Database.GALE_WIND_COST)
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSkill, Database.NEUTRAL_SMASH);
                    }
                    else if (this.currentGaleWind <= 0)
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.GALE_WIND);
                    }
                    else if (this.currentSkillPoint < Database.STRAIGHT_SMASH_COST)
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSkill, Database.INNER_INSPIRATION);
                    }
                    else
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSkill, Database.STRAIGHT_SMASH);
                    }
                    break;

                case Database.DUEL_LENE_COLTOS:
                    if (this.currentLife <= this.MaxLife / 2 && AI_TacticsNumber == 0)
                    {
                        AI_TacticsNumber = 1;
                    }
                    else if (this.currentLife >= this.MaxLife && AI_TacticsNumber == 1)
                    {
                        AI_TacticsNumber = 0;
                    }

                    switch (AP.Math.RandomInteger(1))
                    {
                        case 0:
                            if (AI_TacticsNumber == 1)
                            {
                                this.pa = PlayerAction.UseSpell;
                                this.currentSpellName = Database.FRESH_HEAL;
                                this.ActionLabel.Text = Database.FRESH_HEAL_JP;
                                this.target = this;
                            }
                            else
                            {
                                this.pa = PlayerAction.UseItem;
                                this.currentUsingItem = Database.RARE_BLUE_LIGHTNING;
                                this.ActionLabel.Text = Database.RARE_BLUE_LIGHTNING;
                                this.target = target;
                            }
                            break;
                    }
                    break;
                case Database.DUEL_SCOTY_ZALGE:
                    switch (AP.Math.RandomInteger(1))
                    {
                        case 0:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "ザルゲ・スラッシュ";
                            this.target = target;
                            break;
                    }
                    break;
                case Database.DUEL_PERMA_WARAMY:
                    if (this.currentHolyBreaker <= 0)
                    {
                        this.pa = PlayerAction.UseSpell;
                        this.currentSpellName = Database.HOLY_BREAKER;
                        this.ActionLabel.Text = Database.HOLY_BREAKER_JP;
                        this.target = this;
                    }
                    else
                    {
                        if (this.currentLife >= this.MaxLife)
                        {
                            this.pa = PlayerAction.UseSpell;
                            this.currentSpellName = Database.FLAME_STRIKE;
                            this.ActionLabel.Text = Database.FLAME_STRIKE_JP;
                            this.target = target;
                        }
                        else
                        {
                            this.pa = PlayerAction.Defense;
                            this.ActionLabel.Text = Database.DEFENSE_JP;
                        }
                    }
                    break;

                case Database.DUEL_KILT_JORJU:
                    if (AI_TacticsNumber == 0)
                    {
                        if (this.currentSkillPoint > Database.STRAIGHT_SMASH_COST)
                        {
                            this.pa = PlayerAction.UseSkill;
                            this.currentSkillName = Database.STRAIGHT_SMASH;
                            this.ActionLabel.Text = Database.STRAIGHT_SMASH_JP;
                            this.target = target;
                        }
                        else
                        {
                            this.pa = PlayerAction.NormalAttack;
                            this.ActionLabel.Text = Database.ATTACK_JP;
                            this.target = target;
                        }
                        AI_TacticsNumber = 1;
                    }
                    else if (AI_TacticsNumber == 1)
                    {
                        if (this.currentMana > Database.BLUE_BULLET_COST)
                        {
                            this.pa = PlayerAction.UseSpell;
                            this.currentSpellName = Database.BLUE_BULLET;
                            this.ActionLabel.Text = Database.BLUE_BULLET_JP;
                            this.target = target;
                        }
                        else
                        {
                            this.pa = PlayerAction.NormalAttack;
                            this.ActionLabel.Text = Database.ATTACK_JP;
                            this.target = target;
                        }
                        AI_TacticsNumber = 0;
                    }
                    break;
                case Database.DUEL_BILLY_RAKI:
                    if (this.currentSkillPoint > Database.CARNAGE_RUSH_COST)
                    {
                        this.pa = PlayerAction.UseSkill;
                        this.currentSkillName = Database.CARNAGE_RUSH;
                        this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSkillName);
                        this.target = target;
                    }
                    else if (this.currentMana > Database.WORD_OF_POWER_COST)
                    {
                        this.pa = PlayerAction.UseSpell;
                        this.currentSpellName = Database.WORD_OF_POWER;
                        this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSpellName);
                        this.target = target;
                    }
                    else
                    {
                        this.pa = PlayerAction.NormalAttack;
                        this.target = target;
                    }
                    break;
                case Database.DUEL_ANNA_HAMILTON:
                    if (this.currentWordOfLife <= 0)
                    {
                        this.pa = PlayerAction.UseSpell;
                        this.currentSpellName = Database.WORD_OF_LIFE;
                        this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSpellName);
                        this.target = this;
                    }
                    else
                    {
                        if (AI_TacticsNumber == 0)
                        {
                            this.pa = PlayerAction.UseSpell;
                            this.currentSpellName = Database.VOLCANIC_WAVE;
                            this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSpellName);
                            this.target = target;
                            AI_TacticsNumber = 1;
                        }
                        else if (AI_TacticsNumber == 1)
                        {
                            if (this.currentSkillPoint > Database.CRUSHING_BLOW_COST)
                            {
                                this.pa = PlayerAction.UseSkill;
                                this.currentSkillName = Database.CRUSHING_BLOW;
                                this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSkillName);
                                this.target = target;
                            }
                            else
                            {
                                this.pa = PlayerAction.UseSpell;
                                this.currentSpellName = Database.VOLCANIC_WAVE;
                                this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSpellName);
                                this.target = target;
                            }
                            AI_TacticsNumber = 0;
                        }
                    }
                    break;
                case Database.DUEL_CALMANS_OHN:
                    this.pa = PlayerAction.Defense;
                    this.ActionLabel.Text = Database.DEFENSE_JP;
                    this.target = this;
                    break;
                case Database.DUEL_SUN_YU:
                    if (AI_TacticsNumber == 0)
                    {
                        if (this.currentLife < this.MaxLife / 2)
                        {
                            this.pa = PlayerAction.UseSpell;
                            this.currentSpellName = Database.SACRED_HEAL;
                            this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSpellName);
                            this.target = this;
                        }
                        else
                        {
                            if (target.CurrentDarkenField <= 0)
                            {
                                this.pa = PlayerAction.UseSpell;
                                this.currentSpellName = Database.DARKEN_FIELD;
                                this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSpellName);
                                this.target = target;
                            }
                            else if (this.currentSaintPower <= 0)
                            {
                                this.pa = PlayerAction.UseSpell;
                                this.currentSpellName = Database.SAINT_POWER;
                                this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSpellName);
                                this.target = this;
                            }
                            else if (this.currentBloodyVengeance <= 0)
                            {
                                this.pa = PlayerAction.UseSpell;
                                this.currentSpellName = Database.BLOODY_VENGEANCE;
                                this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSpellName);
                                this.target = this;
                            }
                            else
                            {
                                this.pa = PlayerAction.NormalAttack;
                                this.ActionLabel.Text = Database.ATTACK_JP;
                                this.target = target;
                            }
                        }
                        AI_TacticsNumber = 1;
                    }
                    else if (AI_TacticsNumber == 1)
                    {
                        if (target.CurrentConcussiveHit <= 0 || target.CurrentConcussiveHitValue < 3)
                        {
                            this.pa = PlayerAction.UseSkill;
                            this.currentSkillName = Database.CONCUSSIVE_HIT;
                            this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSkillName);
                            this.target = target;
                        }
                        AI_TacticsNumber = 2;
                    }
                    else
                    {
                        if (this.currentSkillPoint > Database.SILENT_RUSH_COST)
                        {
                            this.pa = PlayerAction.UseSkill;
                            this.currentSkillName = Database.SILENT_RUSH;
                            this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSkillName);
                            this.target = this.target;
                        }
                        else
                        {
                            this.pa = PlayerAction.NormalAttack;
                            this.ActionLabel.Text = Database.ATTACK_JP;
                            this.target = target;
                        }

                        AI_TacticsNumber = 0;
                    }
                    break;
                case Database.DUEL_SHUVALTZ_FLORE:
                    if (this.currentAetherDrive > 0 && this.currentWordOfFortune <= 0)
                    {
                        this.pa = PlayerAction.UseSpell;
                        this.currentSpellName = Database.WORD_OF_FORTUNE;
                        this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSpellName);
                        this.target = this;
                    }
                    else if (this.currentInstantPoint >= this.MaxInstantPoint && this.currentExaltedField <= 0 && this.currentMana > Database.EXALTED_FIELD_COST)
                    {                        
                        this.pa = PlayerAction.UseSpell;
                        this.currentSpellName = Database.EXALTED_FIELD;
                        this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSpellName);
                    }
                    else if (this.currentInstantPoint >= this.MaxInstantPoint && this.currentRisingAura <= 0 && this.currentSkillPoint > Database.RISING_AURA_COST)
                    {
                        this.pa = PlayerAction.UseSkill;
                        this.currentSkillName = Database.RISING_AURA;
                        this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSkillName);
                    }
                    else if (this.currentAetherDrive > 0 && this.currentWordOfFortune > 0)
                    {
                        this.pa = PlayerAction.UseSkill;
                        this.currentSkillName = Database.NEUTRAL_SMASH;
                        this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSkillName);
                        this.target = target;
                    }
                    else
                    {
                        this.pa = PlayerAction.Defense;
                        this.ActionLabel.Text = Database.DEFENSE_JP;
                    }
                    break;

                case Database.DUEL_SINIKIA_KAHLHANZ:
                    this.pa = PlayerAction.UseSpell;
                    this.currentSpellName = Database.PIERCING_FLAME;
                    this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSpellName);
                    this.target = target;
                    break;

                case Database.DUEL_RVEL_ZELKIS:
                    bool existRedPotion = false;
                    bool existBluePotion = false;
                    bool existGreenPotion = false;
                    ItemBackPack[] tempItem_2 = this.GetBackPackInfo();
                    foreach (ItemBackPack value in tempItem_2)
                    {
                        if (value != null)
                        {
                            if (value.Name == Database.COMMON_HUGE_RED_POTION)
                            {
                                existRedPotion = true;
                            }
                            if (value.Name == Database.COMMON_HUGE_BLUE_POTION)
                            {
                                existBluePotion = true;
                            }
                            if (value.Name == Database.COMMON_HUGE_GREEN_POTION)
                            {
                                existGreenPotion = true;
                            }
                        }
                    }

                    // 判断の基準
                    if (mc.CurrentFrozen > 0)
                    {
                        this.AI_TacticsNumber = 1;
                    }
                    else if (this.CurrentPromisedKnowledge > 0)
                    {
                        this.AI_TacticsNumber = 2;
                    }

                    // アイテム使用の基準
                    if ((this.CurrentLife < this.MaxLife / 2) &&
                        (existRedPotion))
                    {
                        this.PA = PlayerAction.UseItem;
                        this.CurrentUsingItem = Database.COMMON_HUGE_RED_POTION;
                        this.Target = this;
                        this.ActionLabel.Text = Database.COMMON_HUGE_RED_POTION;
                    }
                    else if ((this.currentMana < Database.CHILL_BURN_COST) &&
                        (existBluePotion))
                    {
                        this.PA = PlayerAction.UseItem;
                        this.CurrentUsingItem = Database.COMMON_HUGE_BLUE_POTION;
                        this.Target = this;
                        this.ActionLabel.Text = Database.COMMON_HUGE_BLUE_POTION;
                    }
                    else if ((this.currentSkillPoint < Database.CARNAGE_RUSH_COST) &&
                        (existGreenPotion))
                    {
                        this.PA = PlayerAction.UseItem;
                        this.CurrentUsingItem = Database.COMMON_HUGE_GREEN_POTION;
                        this.Target = this;
                        this.ActionLabel.Text = Database.COMMON_HUGE_GREEN_POTION;
                    }                    
                    else if (this.AI_TacticsNumber == 0)
                    {
                        if ((this.currentMana >= Database.CHILL_BURN_COST) &&
                            (target.CurrentFrozen <= 0) &&
                            (this.DetectCannotBeFrozen == false))
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.CHILL_BURN);
                        }
                        else if (this.currentMana >= Database.PROMISED_KNOWLEDGE_COST &&
                                 this.currentPromisedKnowledge <= 0)
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSpell, Database.PROMISED_KNOWLEDGE);
                        }
                        else if (this.currentMana >= Database.BLUE_BULLET_COST)
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.BLUE_BULLET);
                        }
                    }
                    else if (this.AI_TacticsNumber == 1) 
                    {
                        if (this.currentMana >= Database.PROMISED_KNOWLEDGE_COST &&
                            this.currentPromisedKnowledge <= 0)
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSpell, Database.PROMISED_KNOWLEDGE);
                        }
                        else if (this.currentMana >= Database.BLUE_BULLET_COST)
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSpell, Database.BLUE_BULLET);
                        }
                    }
                    else if (this.AI_TacticsNumber == 2)
                    {
                        if (this.CurrentSeventhMagic <= 0)
                        {
                            SetupActionCommand(this, this, PlayerAction.UseSpell, Database.SEVENTH_MAGIC);
                        }
                        else if (this.CurrentSkillPoint >= Database.CARNAGE_RUSH_COST)
                        {
                            SetupActionCommand(this, target, PlayerAction.UseSkill, Database.CARNAGE_RUSH);
                        }
                        else if (this.CurrentMana >= Database.BLUE_BULLET_COST)
                        {
                            SetupActionCommand(this, mc, PlayerAction.UseSpell, Database.BLUE_BULLET);
                        }
                    }
                    break;
                    
                case Database.DUEL_VAN_HEHGUSTEL:
                    if (this.currentChargeCount <= 0)
                    {
                        this.pa = PlayerAction.Charge;
                        this.ActionLabel.Text = Database.TAMERU_JP;
                        this.target = this;
                    }
                    else if (this.currentMana >= Database.PIERCING_FLAME_COST)
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.PIERCING_FLAME);
                    }
                    else
                    {
                        this.pa = PlayerAction.Defense;
                        this.ActionLabel.Text = Database.DEFENSE_JP;
                        this.target = this;
                    }
                    break;
                case Database.DUEL_OHRYU_GENMA:
                    if ((this.currentOneAuthority <= 0) &&
                        (this.currentSkillPoint <= 30))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSkill, Database.ONE_AUTHORITY);
                    }
                    else
                    {
                        // 全てかかっている場合、次のAIへ
                        if ((target.CurrentImpulseHitValue >= 3) && (target.CurrentConcussiveHitValue >= 3) && (target.CurrentOnslaughtHitValue >= 3))
                        {
                            this.AI_TacticsNumber = 3;
                        }
                        // ２つ満たしている場合、残り１つのAIを選択
                        else if ((target.CurrentImpulseHitValue >= 3) && (target.CurrentConcussiveHitValue >= 3) && (target.CurrentOnslaughtHitValue < 3))
                        {
                            this.AI_TacticsNumber = 2;
                        }
                        else if ((target.CurrentImpulseHitValue >= 3) && (target.CurrentConcussiveHitValue < 3) && (target.CurrentOnslaughtHitValue >= 3))
                        {
                            this.AI_TacticsNumber = 1;
                        }
                        else if ((target.CurrentImpulseHitValue < 3) && (target.CurrentConcussiveHitValue >= 3) && (target.CurrentOnslaughtHitValue >= 3))
                        {
                            this.AI_TacticsNumber = 0;
                        }
                        // １つ満たしている場合、残り２つのAIをいずれか選択
                        else if ((target.CurrentImpulseHitValue < 3) && (target.CurrentConcussiveHitValue < 3) && (target.CurrentOnslaughtHitValue >= 3))
                        {
                            if (AP.Math.RandomInteger(2) == 0)
                            {
                                this.AI_TacticsNumber = 0;
                            }
                            else
                            {
                                this.AI_TacticsNumber = 1;
                            }
                        }
                        else if ((target.CurrentImpulseHitValue < 3) && (target.CurrentConcussiveHitValue >= 3) && (target.CurrentOnslaughtHitValue < 3))
                        {
                            if (AP.Math.RandomInteger(2) == 0)
                            {
                                this.AI_TacticsNumber = 0;
                            }
                            else
                            {
                                this.AI_TacticsNumber = 2;
                            }
                        }
                        else if ((target.CurrentImpulseHitValue >= 3) && (target.CurrentConcussiveHitValue < 3) && (target.CurrentOnslaughtHitValue < 3))
                        {
                            if (AP.Math.RandomInteger(2) == 0)
                            {
                                this.AI_TacticsNumber = 1;
                            }
                            else
                            {
                                this.AI_TacticsNumber = 2;
                            }
                        }
                        // 全て満たしてない場合、ランダム３
                        else if ((target.CurrentImpulseHitValue < 3) && (target.CurrentConcussiveHitValue < 3) && (target.CurrentOnslaughtHitValue < 3))
                        {
                            this.AI_TacticsNumber = AP.Math.RandomInteger(3);
                        }

                        // AIに応じて、スキル変更
                        if (this.AI_TacticsNumber == 0)
                        {
                            if ((this.currentSkillPoint >= Database.IMPULSE_HIT_COST) &&
                                (target.CurrentImpulseHitValue < 3))
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSkill, Database.IMPULSE_HIT);
                            }
                        }
                        else if (this.AI_TacticsNumber == 1)
                        {
                            if ((this.currentSkillPoint >= Database.CONCUSSIVE_HIT_COST) &&
                                (target.CurrentConcussiveHitValue < 3))
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSkill, Database.CONCUSSIVE_HIT);
                            }
                        }
                        else if (this.AI_TacticsNumber == 2)
                        {
                            if ((this.currentSkillPoint >= Database.ONSLAUGHT_HIT_COST) &&
                                (target.CurrentOnslaughtHitValue < 3))
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSkill, Database.ONSLAUGHT_HIT);
                            }
                        }
                        else if (this.AI_TacticsNumber == 3)
                        {
                            if (this.currentSkillPoint >= Database.CARNAGE_RUSH_COST)
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSkill, Database.CARNAGE_RUSH);
                            }
                            else if (this.currentSkillPoint >= Database.VIOLENT_SLASH_COST)
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSkill, Database.VIOLENT_SLASH);
                            }
                            else if (this.currentSkillPoint >= Database.DOUBLE_SLASH_COST)
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSkill, Database.DOUBLE_SLASH);
                            }
                            else if (this.currentSkillPoint >= Database.STRAIGHT_SMASH_COST)
                            {
                                SetupActionCommand(this, target, PlayerAction.UseSkill, Database.STRAIGHT_SMASH);
                            }
                        }
                    }
                    break;
                case Database.DUEL_LADA_MYSTORUS:
                    if ((target.CurrentDamnation <= 0) &&
                        (this.currentMana >= Database.DAMNATION_COST))
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.DAMNATION);
                    }
                    else if ((this.currentOneImmunity <= 0) &&
                             (this.currentMana >= Database.ONE_IMMUNITY_COST))
                    {
                        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.ONE_IMMUNITY);
                    }
                    else if ((target.CurrentEnrageBlast <= 0) &&
                             (this.currentMana >= Database.ENRAGE_BLAST_COST))
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.ENRAGE_BLAST);
                    }
                    else if ((target.CurrentBlazingField <= 0) &&
                             (this.currentMana >= Database.BLAZING_FIELD_COST))
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.BLAZING_FIELD);
                    }
                    else if ((this.currentSkyShieldValue < 3) &&
                             (target.CurrentMana >= Database.SKY_SHIELD_COST))
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.SKY_SHIELD);
                    }
                    else
                    {
                        this.pa = PlayerAction.Defense;
                        this.ActionLabel.Text = Database.DEFENSE_JP;
                        this.target = this;
                    }
                    break;
                case Database.DUEL_SIN_OSCURETE:
                    if ((target.CurrentMana <= 0) && (this.currentMana >= Database.ZETA_EXPLOSION_COST))
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.ZETA_EXPLOSION);                        
                    }
                    else if (this.BattleBarPos <= 400)
                    {
                        this.pa = PlayerAction.Defense;
                        this.ActionLabel.Text = Database.DEFENSE_JP;
                        this.target = this;
                    }
                    else
                    {
                        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.WORD_OF_POWER);
                    }
                    //if (this.currentMana >= Database.ONE_IMMUNITY_COST &&
                    //    this.currentOneImmunity <= 0)
                    //{
                    //    SetupActionCommand(this, this, PlayerAction.UseSpell, Database.ONE_IMMUNITY);
                    //}

                    //else if (this.AI_TacticsNumber == 0)
                    //{
                    //    if (this.currentMana >= Database.DOOM_BLADE_COST && target.CurrentMana > 0)
                    //    {
                    //        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.DOOM_BLADE);
                    //    }
                    //}
                    //else if (this.AI_TacticsNumber == 1)
                    //{
                    //    if ((this.currentHeatBoost <= 0) &&
                    //        (this.currentMana >= Database.HEAT_BOOST_COST))
                    //    {
                    //        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.HEAT_BOOST);
                    //    }
                    //    else if ((this.currentWordOfLife <= 0) &&
                    //             (this.currentMana >= Database.WORD_OF_LIFE_COST))
                    //    {
                    //        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.WORD_OF_LIFE);
                    //    }
                    //    else if ((this.currentSaintPower <= 0) &&
                    //             (this.currentMana >= Database.SAINT_POWER_COST))
                    //    {
                    //        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.SAINT_POWER);
                    //    }
                    //    else if ((this.currentProtection <= 0) &&
                    //             (this.currentMana >= Database.PROTECTION_COST))
                    //    {
                    //        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.PROTECTION);
                    //    }
                    //    else if ((this.currentPromisedKnowledge <= 0) &&
                    //             (this.currentMana >= Database.PROMISED_KNOWLEDGE_COST))
                    //    {
                    //        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.PROMISED_KNOWLEDGE);
                    //    }
                    //    else if ((this.currentBloodyVengeance <= 0) &&
                    //             (this.currentMana >= Database.BLOODY_VENGEANCE_COST))
                    //    {
                    //        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.BLOODY_VENGEANCE);
                    //    }
                    //    else if ((this.currentRiseOfImage <= 0) &&
                    //             (this.currentMana >= Database.RISE_OF_IMAGE_COST))
                    //    {
                    //        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.RISE_OF_IMAGE);
                    //    }
                    //    else if ((this.currentShadowPact <= 0) &&
                    //             (this.currentMana >= Database.SHADOW_PACT_COST))
                    //    {
                    //        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.SHADOW_PACT);
                    //    }
                    //    else if ((this.currentAbsorbWater <= 0) &&
                    //             (this.currentMana >= Database.ABSORB_WATER_COST))
                    //    {
                    //        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.ABSORB_WATER);
                    //    }
                    //    else if ((this.currentEternalPresence <= 0) &&
                    //             (this.currentMana >= Database.ETERNAL_PRESENCE_COST))
                    //    {
                    //        SetupActionCommand(this, this, PlayerAction.UseSpell, Database.ETERNAL_PRESENCE);
                    //    }
                    //}
                    //else
                    //{
                    //    if ((this.currentMana >= Database.ZETA_EXPLOSION_COST))
                    //    {
                    //        SetupActionCommand(this, target, PlayerAction.UseSpell, Database.ZETA_EXPLOSION);
                    //    }
                    //}
                    break;
                #endregion

                case Database.DUEL_DUMMY_SUBURI:
                    SetupActionWisely(this, this, Database.FRESH_HEAL);
//                    SetupActionWisely(this, this, Database.STANCE_OF_MYSTIC);
                    return;

                    this.pa = PlayerAction.UseSkill;
                    this.currentSkillName = Database.NEGATE;
                    this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSkillName);
                    this.target = target;
                    return;

                    this.pa = PlayerAction.UseSpell;
                    this.currentSpellName = Database.TIME_STOP;
                    this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSpellName);
                    this.target = this;
                    return;

                    this.pa = PlayerAction.SpecialSkill;
                    this.ActionLabel.Text = "BUFF!";
                    this.target = target;
                    return;

                    if (this.currentVigorSense <= 0)
                    {
                        this.pa = PlayerAction.UseSkill;
                        this.currentSkillName = Database.VIGOR_SENSE;
                        this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSkillName);
                        this.target = this;
                    }
                    else if (this.currentRisingAura <= 0)
                    {
                        this.pa = PlayerAction.UseSkill;
                        this.currentSkillName = Database.RISING_AURA;
                        this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSkillName);
                        this.target = this;
                    }
                    else if (this.currentAbsorbWater <= 0)
                    {
                        this.pa = PlayerAction.UseSpell;
                        this.currentSpellName = Database.ABSORB_WATER;
                        this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSpellName);
                        this.target = this;
                    }
                    else if (this.currentSaintPower <= 0)
                    {
                        this.pa = PlayerAction.UseSpell;
                        this.currentSpellName = Database.SAINT_POWER;
                        this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSpellName);
                        this.target = this;
                    }
                    else if (this.currentProtection <= 0)
                    {
                        this.pa = PlayerAction.UseSpell;
                        this.currentSpellName = Database.PROTECTION;
                        this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSpellName);
                        this.target = this;
                    }
                    else if (this.currentBloodyVengeance <= 0)
                    {
                        this.pa = PlayerAction.UseSpell;
                        this.currentSpellName = Database.BLOODY_VENGEANCE;
                        this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSpellName);
                        this.target = this;
                    }
                    else if (this.currentPromisedKnowledge <= 0)
                    {
                        this.pa = PlayerAction.UseSpell;
                        this.currentSpellName = Database.PROMISED_KNOWLEDGE;
                        this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSpellName);
                        this.target = this;
                    }
                    else if (this.currentHeatBoost <= 0)
                    {
                        this.pa = PlayerAction.UseSpell;
                        this.currentSpellName = Database.HEAT_BOOST;
                        this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSpellName);
                        this.target = this;
                    }
                    return;

                    if (target.CurrentNoResurrection <= 0)
                    {
                        this.pa = PlayerAction.SpecialSkill;
                        //this.ActionLabel.Text = "スタン";
                        //this.ActionLabel.Text = "沈黙";
                        //this.ActionLabel.Text = "猛毒";
                        //this.ActionLabel.Text = "誘惑";
                        //this.ActionLabel.Text = "凍結";
                        //this.ActionLabel.Text = "麻痺";
                        //this.ActionLabel.Text = "鈍化";
                        //this.ActionLabel.Text = "暗闇";
                        //this.ActionLabel.Text = "スリップ";
                        this.ActionLabel.Text = "復活不可";
                        this.target = target;
                    }
                    else if (this.currentAbsorbWater <= 0)
                    {
                        this.pa = PlayerAction.UseSpell;
                        this.currentSpellName = Database.ABSORB_WATER;
                        this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSpellName);
                        this.target = this;
                    }
                    else if (this.currentSaintPower <= 0)
                    {
                        this.pa = PlayerAction.UseSpell;
                        this.currentSpellName = Database.SAINT_POWER;
                        this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSpellName);
                        this.target = this;
                    }
                    else if (this.currentProtection <= 0)
                    {
                        this.pa = PlayerAction.UseSpell;
                        this.currentSpellName = Database.PROTECTION;
                        this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSpellName);
                        this.target = this;
                    }
                    else if (this.currentBloodyVengeance <= 0)
                    {
                        this.pa = PlayerAction.UseSpell;
                        this.currentSpellName = Database.BLOODY_VENGEANCE;
                        this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSpellName);
                        this.target = this;
                    }
                    else if (this.currentPromisedKnowledge <= 0)
                    {
                        this.pa = PlayerAction.UseSpell;
                        this.currentSpellName = Database.PROMISED_KNOWLEDGE;
                        this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSpellName);
                        this.target = this;
                    }
                    else if (this.currentHeatBoost <= 0)
                    {
                        this.pa = PlayerAction.UseSpell;
                        this.currentSpellName = Database.HEAT_BOOST;
                        this.ActionLabel.Text = TruthActionCommand.ConvertToJapanese(this.currentSpellName);
                        this.target = this;
                    }
                    else
                    {
                        this.pa = PlayerAction.None;
                        this.ActionLabel.Text = Database.STAY_JP;
                        this.target = this;
                    }
                    return;
                    switch (AP.Math.RandomInteger(1))
                    {
                        case 20:
                            this.pa = PlayerAction.UseSpell;
                            this.currentSpellName = Database.FLAME_STRIKE;
                            this.ActionLabel.Text = Database.FLAME_STRIKE_JP;
                            this.target = target;
                            break;
                        case 19:
                            this.pa = PlayerAction.UseSpell;
                            this.currentSpellName = Database.DEFLECTION;
                            this.ActionLabel.Text = Database.DEFLECTION_JP;
                            this.target = this;
                            break;
                        case 18:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "弱体化「魔法防御」";
                            this.target = target;
                            break;
                        case 17:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "弱体化「魔法攻撃」";
                            this.target = target;
                            break;
                        case 16:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "弱体化「物理攻撃」";
                            this.target = target;
                            break;
                        case 15:
                            this.pa = PlayerAction.UseSkill;
                            this.currentSkillName = Database.COUNTER_ATTACK;
                            this.ActionLabel.Text = Database.COUNTER_ATTACK_JP;
                            this.target = target;
                            break;
                        case 0:
                            this.pa = PlayerAction.SpecialSkill;
                            this.ActionLabel.Text = "スタン";
                            this.ActionLabel.Text = "沈黙";
                            this.ActionLabel.Text = "猛毒";
                            this.ActionLabel.Text = "誘惑";
                            this.ActionLabel.Text = "凍結";
                            this.ActionLabel.Text = "麻痺";
                            this.ActionLabel.Text = "鈍化";
                            this.ActionLabel.Text = "暗闇";
                            this.ActionLabel.Text = "スリップ";
                            this.ActionLabel.Text = "復活不可";
                            this.target = target;
                            break;
                        case 9:
                            this.pa = PlayerAction.UseSkill;
                            this.currentSkillName = Database.CRUSHING_BLOW;
                            this.ActionLabel.Text = Database.CRUSHING_BLOW_JP;
                            this.target = target;
                            break;
                        case 8:
                            this.pa = PlayerAction.None;
                            break;
                        case 7:
                            this.pa = PlayerAction.UseSpell;
                            this.currentSpellName = Database.WORD_OF_MALICE;
                            this.ActionLabel.Text = Database.WORD_OF_MALICE_JP;
                            this.target = target;
                            break;
                        case 6:
                            this.pa = PlayerAction.UseSkill;
                            this.currentSkillName = Database.STRAIGHT_SMASH;
                            this.ActionLabel.Text = Database.STRAIGHT_SMASH_JP;
                            this.target = target;
                            break;
                        case 5:
                            this.pa = PlayerAction.UseSkill;
                            this.currentSkillName = Database.STANCE_OF_EYES;
                            this.ActionLabel.Text = Database.STANCE_OF_EYES_JP;
                            this.target = target;
                            break;
                        case 4:
                            this.pa = PlayerAction.UseSkill;
                            this.currentSkillName = Database.NEGATE;
                            this.ActionLabel.Text = Database.NEGATE_JP;
                            this.target = target;
                            break;
                        case 3:
                            this.pa = PlayerAction.Defense;
                            break;
                        case 2:
                            this.pa = PlayerAction.NormalAttack;
                            this.ActionLabel.Text = Database.ATTACK_JP;
                            this.target = target;
                            break;
                    }

                    break;
            }
        }


        public TruthEnemyCharacter(string createName)
        {
            this.DropItem = new string[MAX_DROPITEM_SIZE];
            for (int ii = 0; ii < MAX_DROPITEM_SIZE; ii++)
            {
                this.DropItem[ii] = String.Empty;
            }

            // 敵はスキル・魔法が使えなくなっていても、DUELでは使える風に見せかけるため、常にTRUEとします。
            this.availableMana = true;
            this.availableSkill = true;

            this.name = createName;
            switch (createName)
            {
                #region "ダンジョン１階"
                #region "エリア１"
                case Database.ENEMY_KOUKAKU_WURM:
                    this.baseStrength = 7;
                    this.baseAgility = 2;
                    this.baseIntelligence = 1;
                    this.baseStamina = 1;
                    this.baseMind = 1;
                    this.baseLife = 45;
                    this.experience = 102;
                    this.level = 1;
                    this.gold = 44;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area11;
                    this.DropItem[0] = "ワームの甲殻";
                    break;
                case Database.ENEMY_HIYOWA_BEATLE:
                    this.baseStrength = 6;
                    this.baseAgility = 3;
                    this.baseIntelligence = 1;
                    this.baseStamina = 1;
                    this.baseMind = 1;
                    this.baseLife = 40;
                    this.experience = 109;
                    this.level = 1;
                    this.gold = 47;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area11;
                    this.DropItem[0] = "ビートルの尖った角";
                    break;
                case Database.ENEMY_GREEN_CHILD:
                    this.baseStrength = 5;
                    this.baseAgility = 5;
                    this.baseIntelligence = 12;
                    this.baseStamina = 1;
                    this.baseMind = 1;
                    this.baseLife = 30;
                    this.experience = 126;
                    this.level = 1;
                    this.gold = 54;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area11;
                    this.DropItem[0] = "緑化色素";
                    break;
                case Database.ENEMY_MANDRAGORA:
                    this.baseStrength = 16;
                    this.baseAgility = 7;
                    this.baseIntelligence = 21;
                    this.baseStamina = 14;
                    this.baseLife = 0;
                    this.experience = 215;
                    this.level = 4;
                    this.gold = 92;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area11;
                    this.DropItem[0] = "マンドラゴラの根";
                    break;
                #endregion

                #region "エリア２"
                case Database.ENEMY_SUN_FLOWER:
                    this.baseResistFire = 30;
                    this.baseStrength = 1;
                    this.baseAgility = 10;
                    this.baseIntelligence = 25;
                    this.baseStamina = 12;
                    this.baseMind = 14;
                    this.experience = 172;
                    this.level = 6;
                    this.gold = 64;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area12;
                    this.DropItem[0] = "太陽の葉";
                    break;
                case Database.ENEMY_RED_HOPPER:
                    this.baseStrength = 20;
                    this.baseAgility = 21;
                    this.baseIntelligence = 5;
                    this.baseStamina = 8;
                    this.baseMind = 3;
                    this.experience = 186;
                    this.level = 6;
                    this.gold = 70;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Regist_Physical;
                    this.Area = MonsterArea.Area12;
                    this.DropItem[0] = "蝗";
                    break;
                case Database.ENEMY_EARTH_SPIDER:
                    this.baseStrength = 56;
                    this.baseAgility = 11;
                    this.baseIntelligence = 11;
                    this.baseStamina = 15;
                    this.experience = 198;
                    this.level = 6;
                    this.gold = 75;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area12;
                    this.DropItem[0] = "スパイダーシルク";
                    break;
                case Database.ENEMY_ALRAUNE:
                    this.baseStrength = 5;
                    this.baseAgility = 35;
                    this.baseIntelligence = 38;
                    this.baseStamina = 25;
                    this.experience = 337;
                    this.level = 9;
                    this.gold = 116;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area12;
                    this.DropItem[0] = "アルラウネの花粉";
                    break;
                case Database.ENEMY_POISON_MARY:
                    this.baseStrength = 8;
                    this.baseAgility = 48;
                    this.baseIntelligence = 52;
                    this.baseStamina = 39;
                    this.baseMind = 40;
                    this.experience = 506;
                    this.level = 13;
                    this.gold = 209;
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area12;
                    this.DropItem[0] = "マリーキッス";
                    this.DropItem[1] = "ブルーマテリアル";
                    break;
                #endregion

                #region "エリア３"
                case Database.ENEMY_ZASSYOKU_RABBIT:
                    this.baseStrength = 72;
                    this.baseAgility = 35;
                    this.baseIntelligence = 2;
                    this.baseStamina = 30;
                    this.experience = 405;
                    this.level = 11;
                    this.gold = 146;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area13;
                    this.DropItem[0] = "ウサギの毛皮";
                    this.DropItem[1] = "ウサギの肉";
                    break;
                case Database.ENEMY_SPEEDY_TAKA:
                    this.baseStrength = 66;
                    this.baseAgility = 55;
                    this.baseIntelligence = 22;
                    this.baseStamina = 33;
                    this.experience = 437;
                    this.level = 11;
                    this.gold = 158;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area13;
                    this.DropItem[0] = "鷹の白羽";
                    break;
                case Database.ENEMY_WONDER_SEED:
                    this.baseStrength = 93;
                    this.baseAgility = 30;
                    this.baseIntelligence = 56;
                    this.baseStamina = 51;
                    this.experience = 743;
                    this.level = 14;
                    this.gold = 247;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area13;
                    this.DropItem[0] = "プラントノイドの種";
                    break;
                case Database.ENEMY_FLANSIS_KNIGHT:
                    this.baseStrength = 115;
                    this.baseAgility = 13;
                    this.baseIntelligence = 3;
                    this.baseStamina = 65;
                    this.baseMind = 5;
                    this.experience = 751;
                    this.level = 15;
                    this.gold = 249;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area13;
                    this.DropItem[0] = "刺の生えた触手";
                    break;
                case Database.ENEMY_SHOTGUN_HYUI:
                    this.baseStrength = 85;
                    this.baseAgility = 113;
                    this.baseIntelligence = 42;
                    this.baseStamina = 81;
                    this.baseMind = 53;
                    this.experience = 1126;
                    this.level = 18;
                    this.gold = 449;
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area13;
                    this.DropItem[0] = "ヒューイの種";
                    this.DropItem[1] = "ブルーマテリアル";
                    break;
                #endregion

                #region "エリア４"
                case Database.ENEMY_WAR_WOLF:
                    this.baseStrength = 85;
                    this.baseAgility = 62;
                    this.baseIntelligence = 10;
                    this.baseStamina = 62;
                    this.experience = 901;
                    this.level = 19;
                    this.gold = 314;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area14;
                    this.DropItem[0] = "狼の牙";
                    break;
                case Database.ENEMY_BRILLIANT_BUTTERFLY:
                    this.baseStrength = 2;
                    this.baseAgility = 9;
                    this.baseIntelligence = 135;
                    this.baseStamina = 48;
                    this.experience = 946;
                    this.level = 19;
                    this.gold = 330;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area14;
                    this.DropItem[0] = "輝きの燐粉";
                    break;
                case Database.ENEMY_BLOOD_MOSS:
                    this.baseStrength = 96;
                    this.baseAgility = 3;
                    this.baseIntelligence = 90;
                    this.baseStamina = 66;
                    this.baseMind = 20;
                    this.experience = 1608;
                    this.level = 21;
                    this.gold = 514;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area14;
                    this.DropItem[0] = "赤い胞子";
                    break;
                case Database.ENEMY_MOSSGREEN_DADDY:
                    this.baseStrength = 145;
                    this.baseAgility = 39;
                    this.baseIntelligence = 131;
                    this.baseStamina = 82;
                    this.baseMind = 30;
                    this.experience = 2413;
                    this.level = 24;
                    this.gold = 926;
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area14;
                    this.DropItem[0] = "モスグリーンのエキス";
                    this.DropItem[1] = "ブルーマテリアル";
                    this.DropItem[2] = "成長リキッド【知】";
                    break;
                #endregion

                #region "ボス"
                case Database.ENEMY_BOSS_KARAMITUKU_FLANSIS:
                    this.baseStrength = 195;
                    this.baseAgility = 100;
                    this.baseIntelligence = 150;
                    this.baseStamina = 210;
                    this.experience = 4825;
                    this.baseLife = 3500;
                    this.baseInstantPoint = 3000;
                    this.level = 30;
                    this.gold = 10000;
                    this.Rare = RareString.Gold;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss1;
                    this.DropItem[0] = Database.EPIC_ORB_GROW_GREEN;
                    this.UseStackCommand = true;
                    break;

                case Database.ENEMY_DRAGON_SOKUBAKU_BRIYARD:
                    this.baseStrength = 68590;
                    this.baseAgility = 1;//4241;会話専用のため、スピードを減らす。
                    this.baseIntelligence = 77610;
                    this.baseStamina = 40650;
                    this.baseMind = 2150;
                    this.experience = 0;//15000000;
                    this.baseLife = 34359122;
                    this.level = 251;
                    this.gold = 15000000;
                    this.Area = MonsterArea.TruthBoss1;
                    this.Rare = RareString.Purple;
                    this.Armor = ArmorType.Normal;
                    this.UseStackCommand = true;
                    break;
                #endregion
                #endregion
                #region "ダンジョン２階"
                #region "エリア１"
                case Database.ENEMY_DAGGER_FISH:
                    this.baseStrength = 220;
                    this.baseAgility = 133;
                    this.baseIntelligence = 10;
                    this.baseStamina = 85;
                    this.experience = 3475;
                    this.level = 25;
                    this.gold = 1488;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area21;
                    this.DropItem[0] = Database.COMMON_DAGGERFISH_UROKO;
                    break;

                case Database.ENEMY_SIPPU_FLYING_FISH:
                    this.baseStrength = 220;
                    this.baseAgility = 230;
                    this.baseIntelligence = 55;
                    this.baseStamina = 77;
                    this.experience = 3579;
                    this.level = 25;
                    this.gold = 1522;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area21;
                    this.DropItem[0] = Database.COMMON_SIPPUU_HIRE;
                    break;

                case Database.ENEMY_ORB_SHELLFISH:
                    this.baseStrength = 30;
                    this.baseAgility = 105;
                    this.baseIntelligence = 170;
                    this.baseStamina = 88;
                    this.experience = 3686;
                    this.level = 25;
                    this.gold = 1605;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area21;
                    this.DropItem[0] = Database.COMMON_WHITE_MAGATAMA;
                    this.DropItem[1] = Database.COMMON_BLUE_MAGATAMA;
                    break;

                case Database.ENEMY_SPLASH_KURIONE:
                    this.baseStrength = 120;
                    this.baseAgility = 155;
                    this.baseIntelligence = 309;
                    this.baseStamina = 112;
                    this.experience = 4976;
                    this.level = 30;
                    this.gold = 2029;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area21;
                    this.DropItem[0] = Database.COMMON_BLUEWHITE_SHARP_TOGE;
                    this.DropItem[1] = Database.COMMON_KURIONE_ZOUMOTU;
                    break;
                #endregion

                #region "エリア２"
                case Database.ENEMY_ROLLING_MAGURO:
                    this.baseStrength = 450;
                    this.baseAgility = 85;
                    this.baseIntelligence = 5;
                    this.baseStamina = 140;
                    this.experience = 3732;
                    this.level = 28;
                    this.gold = 1720;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area22;
                    this.DropItem[0] = Database.COMMON_RENEW_AKAMI;
                    break;

                case Database.ENEMY_RANBOU_SEA_ARTINE:
                    this.baseStrength = 360;
                    this.baseAgility = 156;
                    this.baseIntelligence = 5;
                    this.baseStamina = 120;
                    this.experience = 3844;
                    this.level = 28;
                    this.gold = 1778;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area22;
                    this.DropItem[0] = Database.COMMON_SEA_WASI_KUTIBASI;
                    break;

                case Database.ENEMY_BLUE_SEA_WASI:
                    this.baseStrength = 320;
                    this.baseAgility = 410;
                    this.baseIntelligence = 40;
                    this.baseStamina = 115;
                    this.experience = 3960;
                    this.level = 28;
                    this.gold = 1832;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area22;
                    this.DropItem[0] = Database.COMMON_WASI_BLUE_FEATHER;
                    break;

                case Database.ENEMY_GANGAME:
                    this.baseStrength = 165;
                    this.baseAgility = 50;
                    this.baseIntelligence = 168;
                    this.baseStamina = 240;
                    this.experience = 4752;
                    this.level = 31;
                    this.gold = 2298;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area22;
                    this.DropItem[0] = Database.COMMON_GANGAME_EGG;
                    break;

                case Database.ENEMY_BIGMOUSE_JOE:
                    this.baseStrength = 560;
                    this.baseAgility = 110;
                    this.baseIntelligence = 370;
                    this.baseStamina = 412;
                    this.experience = 6652;
                    this.level = 35;
                    this.gold = 3337;
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area22;
                    this.DropItem[0] = Database.RARE_JOE_ARM;
                    this.DropItem[1] = Database.RARE_JOE_LEG;
                    this.DropItem[2] = Database.RARE_JOE_TONGUE;
                    break;
                #endregion

                #region "エリア３"
                case Database.ENEMY_MOGURU_MANTA:
                    this.baseStrength = 120;
                    this.baseAgility = 40;
                    this.baseIntelligence = 460;
                    this.baseStamina = 350;
                    this.experience = 4989;
                    this.level = 33;
                    this.gold = 2636;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area23;
                    this.DropItem[0] = Database.COMMON_SOFT_BIG_HIRE;
                    break;

                case Database.ENEMY_FLOATING_GOLD_FISH:
                    this.baseStrength = 10;
                    this.baseAgility = 350;
                    this.baseIntelligence = 420;
                    this.baseStamina = 320;
                    this.experience = 5139;
                    this.level = 33;
                    this.gold = 2767;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area23;
                    this.DropItem[0] = Database.COMMON_PURE_WHITE_BIGEYE;
                    break;

                case Database.ENEMY_GOEI_HERMIT_CLUB:
                    this.baseStrength = 520;
                    this.baseAgility = 405;
                    this.baseIntelligence = 10;
                    this.baseStamina = 470;
                    this.experience = 6167;
                    this.level = 36;
                    this.gold = 3756;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area23;
                    this.DropItem[0] = Database.COMMON_GOTUGOTU_KARA;
                    break;

                case Database.ENEMY_VANISHING_CORAL:
                    this.baseStrength = 10;
                    this.baseAgility = 580;
                    this.baseIntelligence = 500;
                    this.baseStamina = 510;
                    this.experience = 6228;
                    this.level = 36;
                    this.gold = 3783;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area23;
                    this.DropItem[0] = Database.COMMON_HALF_TRANSPARENT_ROCK_ASH;
                    break;

                case Database.ENEMY_CASSY_CANCER:
                    this.baseStrength = 690;
                    this.baseAgility = 510;
                    this.baseIntelligence = 515;
                    this.baseStamina = 620;
                    this.experience = 8720;
                    this.level = 40;
                    this.gold = 6010;
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area23;
                    this.DropItem[0] = Database.RARE_SEKIKASSYOKU_HASAMI;
                    break;
                #endregion

                #region "エリア４"
                case Database.ENEMY_BLACK_STARFISH:
                    this.baseStrength = 10;
                    this.baseAgility = 10;
                    this.baseIntelligence = 352;
                    this.baseStamina = 166;
                    this.experience = 6104;
                    this.level = 38;
                    this.gold = 4507;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area24;
                    this.DropItem[0] = Database.COMMON_KOUSITUKA_MATERIAL;
                    break;

                case Database.ENEMY_RAINBOW_ANEMONE:
                    this.baseStrength = 10;
                    this.baseAgility = 288;
                    this.baseIntelligence = 271;
                    this.baseStamina = 115;
                    this.experience = 6287;
                    this.level = 38;
                    this.gold = 4683;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area24;
                    this.DropItem[0] = Database.COMMON_NANAIRO_SYOKUSYU;
                    break;

                case Database.ENEMY_EDGED_HIGH_SHARK:
                    this.baseStrength = 412;
                    this.baseAgility = 224;
                    this.baseIntelligence = 10;
                    this.baseStamina = 233;
                    this.experience = 7544;
                    this.level = 41;
                    this.gold = 6745;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area24;
                    this.DropItem[0] = Database.COMMON_AOSAME_KENSHI;
                    this.DropItem[1] = Database.COMMON_AOSAME_UROKO;
                    break;

                case Database.ENEMY_EIGHT_EIGHT:
                    this.baseStrength = 10;
                    this.baseAgility = 322;
                    this.baseIntelligence = 511;
                    this.baseStamina = 380;
                    this.experience = 10562;
                    this.level = 45;
                    this.gold = 11341;
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area24;
                    this.DropItem[0] = Database.COMMON_EIGHTEIGHT_KUROSUMI;
                    this.DropItem[1] = Database.COMMON_EIGHTEIGHT_KYUUBAN;
                    break;
                #endregion

                #region "力の部屋：ボス"
                case Database.ENEMY_BRILLIANT_SEA_PRINCE:
                    this.baseStrength = 360;
                    this.baseAgility = 280;
                    this.baseIntelligence = 400;
                    this.baseStamina = 200;
                    this.experience = 10984;
                    this.baseLife = 16000;
                    this.baseInstantPoint = 2400;
                    this.level = 50;
                    this.gold = 15000;
                    this.Rare = RareString.Gold;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss21;
                    break;

                case Database.ENEMY_ORIGIN_STAR_CORAL_QUEEN:
                    this.baseStrength = 250;
                    this.baseAgility = 140;
                    this.baseIntelligence = 650;
                    this.baseStamina = 200;
                    this.experience = 11424;
                    this.baseLife = 19500;
                    this.baseInstantPoint = 1800;
                    this.level = 51;
                    this.gold = 16000;
                    this.Rare = RareString.Gold;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss22;
                    break;

                case Database.ENEMY_SHELL_SWORD_KNIGHT:
                    this.baseStrength = 720;
                    this.baseAgility = 450;
                    this.baseIntelligence = 10;
                    this.baseStamina = 300;
                    this.experience = 11881;
                    this.baseLife = 22000;
                    this.baseInstantPoint = 3900;
                    this.level = 52;
                    this.gold = 17000;
                    this.Rare = RareString.Gold;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss23;
                    break;

                case Database.ENEMY_JELLY_EYE_BRIGHT_RED:
                    this.baseStrength = 150;
                    this.baseAgility = 250;
                    this.baseIntelligence = 900;
                    this.baseStamina = 280;
                    this.experience = 12356;
                    this.baseLife = 20000;
                    this.baseInstantPoint = 2200;
                    this.level = 53;
                    this.gold = 18000;
                    this.Rare = RareString.Gold;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss24;
                    //this.baseResistFire = 2000; // これだと画面上で分からないため、BattleEnemy側の初期化でサポート
                    break;

                case Database.ENEMY_JELLY_EYE_DEEP_BLUE:
                    this.baseStrength = 150;
                    this.baseAgility = 250;
                    this.baseIntelligence = 900;
                    this.baseStamina = 280;
                    this.experience = 12356;
                    this.baseLife = 20000;
                    this.baseInstantPoint = 2700;
                    this.level = 53;
                    this.gold = 18000;
                    this.Rare = RareString.Gold;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss24;
                    //this.baseResistIce = 2000; // これだと画面上で分からないため、BattleEnemy側の初期化でサポート
                    break;


                case Database.ENEMY_SEA_STAR_ORIGIN_KING:
                    this.baseStrength = 750;
                    this.baseAgility = 100;
                    this.baseIntelligence = 750;
                    this.baseStamina = 500;
                    this.experience = 12850;
                    this.baseLife = 28000;
                    this.baseInstantPoint = 15000;
                    this.level = 55;
                    this.gold = 19000;
                    this.Rare = RareString.Gold;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss25;
                    break;

                case Database.ENEMY_SEA_STAR_KNIGHT_AEGIRU:
                    this.baseStrength = 350;
                    this.baseAgility = 420;
                    this.baseIntelligence = 50;
                    this.baseStamina = 320;
                    this.experience = 12850;
                    this.baseLife = 15000;
                    this.baseInstantPoint = 5400;
                    this.level = 50;
                    this.gold = 19000;
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss25;
                    break;

                case Database.ENEMY_SEA_STAR_KNIGHT_AMARA:
                    this.baseStrength = 420;
                    this.baseAgility = 350;
                    this.baseIntelligence = 50;
                    this.baseStamina = 320;
                    this.experience = 12850;
                    this.baseLife = 15000;
                    this.baseInstantPoint = 3600;
                    this.level = 50;
                    this.gold = 19000;
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss25;
                    break;

                #endregion

                #region "ボス"

                case Database.ENEMY_BOSS_LEVIATHAN:
                    this.baseStrength = 850;
                    this.baseAgility = 700;
                    this.baseIntelligence = 1000;
                    this.baseStamina = 600;
                    this.baseMind = 250;
                    this.experience = 25700;
                    this.baseLife = 40000;
                    this.baseInstantPoint = 75000;
                    this.level = 57;
                    this.gold = 50000;
                    this.Rare = RareString.Gold;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss2;
                    this.DropItem[0] = Database.EPIC_ORB_GROUNDSEA_STAR;
                    this.UseStackCommand = true;
                    break;

                case Database.ENEMY_DRAGON_TINKOU_DEEPSEA:
                    this.baseStrength = 35510;
                    this.baseAgility = 100;//2566;会話専用のため、スピードを減らす。
                    this.baseIntelligence = 91210;
                    this.baseStamina = 61120;
                    this.baseMind = 2150;
                    this.experience = 0;//15000000;
                    this.baseLife = 41226289;
                    this.level = 252;
                    this.gold = 15000000;
                    this.Area = MonsterArea.TruthBoss2;
                    this.Rare = RareString.Purple;
                    this.Armor = ArmorType.Normal;
                    this.UseStackCommand = true;
                    break;
                #endregion
                #endregion
                #region "３階"
                #region "エリア１"
                case Database.ENEMY_TOSSIN_ORC:
                    this.baseStrength = 788;
                    this.baseAgility = 60;
                    this.baseIntelligence = 2;
                    this.baseStamina = 500;
                    this.baseLife = 26000;
                    this.baseMind = 0;
                    this.experience = 19032;
                    this.level = 50;
                    this.gold = 7123;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area31;
                    this.DropItem[0] = Database.COMMON_ORC_MOMONIKU;
                    break;

                case Database.ENEMY_SNOW_CAT:
                    this.baseStrength = 566;
                    this.baseAgility = 488;
                    this.baseIntelligence = 271;
                    this.baseStamina = 115;
                    this.baseLife = 22000;
                    this.baseMind = 0;
                    this.experience = 19603;
                    this.level = 50;
                    this.gold = 7271;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area31;
                    this.DropItem[0] = Database.COMMON_SNOW_CAT_KEGAWA;
                    break;

                case Database.ENEMY_WAR_MAMMOTH:
                    this.baseStrength = 720;
                    this.baseAgility = 50;
                    this.baseIntelligence = 1;
                    this.baseStamina = 550;
                    this.baseLife = 32000;
                    this.baseMind = 0;
                    this.experience = 20191;
                    this.level = 50;
                    this.gold = 7634;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area31;
                    this.DropItem[0] = Database.COMMON_BIG_HIZUME;
                    break;

                case Database.ENEMY_WINGED_COLD_FAIRY:
                    this.baseStrength = 5;
                    this.baseAgility = 252;
                    this.baseIntelligence = 1015;
                    this.baseStamina = 330;
                    this.baseLife = 25000;
                    this.baseMind = 0;
                    this.experience = 27257;
                    this.level = 54;
                    this.gold = 9479;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area31;
                    this.DropItem[0] = Database.COMMON_FAIRY_POWDER;
                    break;
                #endregion
                #region "エリア２"
                case Database.ENEMY_BRUTAL_OGRE:
                    this.baseStrength = 850;
                    this.baseAgility = 550;
                    this.baseIntelligence = 1;
                    this.baseStamina = 160;
                    this.baseLife = 36000;
                    this.baseMind = 0;
                    this.gold = 8135;
                    this.experience = 20443;
                    this.level = 55;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area32;
                    this.DropItem[0] = Database.COMMON_GOTUGOTU_KONBOU;
                    break;
                case Database.ENEMY_HYDRO_LIZARD:
                    this.baseStrength = 650;
                    this.baseAgility = 320;
                    this.baseIntelligence = 650;
                    this.baseStamina = 220;
                    this.baseLife = 41000;
                    this.baseMind = 0;
                    this.gold = 8386;
                    this.experience = 21056;
                    this.level = 55;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area32;
                    this.DropItem[0] = Database.COMMON_LIZARD_UROKO;
                    break;
                case Database.ENEMY_PENGUIN_STAR:
                    this.baseStrength = 380;
                    this.baseAgility = 380;
                    this.baseIntelligence = 380;
                    this.baseStamina = 380;
                    this.baseLife = 45000;
                    this.baseMind = 380;
                    this.gold = 8623;
                    this.experience = 21688;
                    this.level = 55;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area32;
                    this.DropItem[0] = Database.COMMON_EMBLEM_OF_PENGUIN;
                    break;
                case Database.ENEMY_SWORD_TOOTH_TIGER:
                    this.baseStrength = 1100;
                    this.baseAgility = 1;
                    this.baseIntelligence = 650;
                    this.baseStamina = 350;
                    this.baseLife = 60000;
                    this.baseMind = 0;
                    this.gold = 10652;
                    this.experience = 26026;
                    this.level = 58;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area32;
                    this.DropItem[0] = Database.COMMON_SHARPNESS_TIGER_TOOTH;
                    break;
                case Database.ENEMY_FEROCIOUS_RAGE_BEAR:
                    this.baseStrength = 1500;
                    this.baseAgility = 1;
                    this.baseIntelligence = 1;
                    this.baseStamina = 1000;
                    this.baseLife = 85000;
                    this.baseMind = 0;
                    this.gold = 15173;
                    this.experience = 36436;
                    this.level = 62;
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area32;
                    this.DropItem[0] = Database.RARE_BEAR_CLAW_KAKERA;
                    break;
                #endregion

                #region "エリア３"
                case Database.ENEMY_WINTER_ORB:
                    this.baseStrength = 1;
                    this.baseAgility = 200;
                    this.baseIntelligence = 1200;
                    this.baseStamina = 650;
                    this.baseLife = 50000;
                    this.baseMind = 0;
                    this.gold = 12121;
                    this.experience = 27327;
                    this.level = 60;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area33;
                    this.DropItem[0] = Database.COMMON_TOUMEI_SNOW_CRYSTAL;
                    break;
                case Database.ENEMY_PATHFINDING_LIGHTNING_AZARASI:
                    this.baseStrength = 1;
                    this.baseAgility = 300;
                    this.baseIntelligence = 700;
                    this.baseStamina = 600;
                    this.baseLife = 65000;
                    this.baseMind = 500;
                    this.gold = 12691;
                    this.experience = 28147;
                    this.level = 60;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area33;
                    this.DropItem[0] = Database.COMMON_WHITE_AZARASHI_MEAT;
                    break;
                case Database.ENEMY_INTELLIGENCE_ARGONIAN:
                    this.baseStrength = 1015;
                    this.baseAgility = 300;
                    this.baseIntelligence = 1020;
                    this.baseStamina = 700;
                    this.baseLife = 73000;
                    this.baseMind = 0;
                    this.gold = 16997;
                    this.experience = 33776;
                    this.level = 63;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area33;
                    this.DropItem[0] = Database.COMMON_ARGONIAN_PURPLE_UROKO;
                    break;
                case Database.ENEMY_MAGIC_HYOU_RIFLE:
                    this.baseStrength = 1;
                    this.baseAgility = 1000;
                    this.baseIntelligence = 1800;
                    this.baseStamina = 200;
                    this.baseLife = 86000;
                    this.baseMind = 0;
                    this.gold = 17117;
                    this.experience = 34114;
                    this.level = 63;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area33;
                    this.DropItem[0] = Database.COMMON_BLUE_DANGAN_KAKERA;
                    break;
                case Database.ENEMY_PURE_BLIZZARD_CRYSTAL:
                    this.baseStrength = 1;
                    this.baseAgility = 1;
                    this.baseIntelligence = 2600;
                    this.baseStamina = 1000;
                    this.baseLife = 110000;
                    this.baseMind = 0;
                    this.gold = 26811;
                    this.experience = 47759;
                    this.level = 67;
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area33;
                    this.DropItem[0] = Database.RARE_PURE_CRYSTAL;
                    break;
                #endregion

                #region "エリア４"
                case Database.ENEMY_PURPLE_EYE_WARE_WOLF:
                    this.baseStrength = 1400;
                    this.baseAgility = 800;
                    this.baseIntelligence = 1;
                    this.baseStamina = 700;
                    this.baseLife = 85000;
                    this.baseMind = 0;
                    this.gold = 20268;
                    this.experience = 33432;
                    this.level = 65;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area34;
                    this.DropItem[0] = Database.COMMON_WOLF_KEGAWA;
                    break;
                case Database.ENEMY_FROST_HEART:
                    this.baseStrength = 1;
                    this.baseAgility = 700;
                    this.baseIntelligence = 1900;
                    this.baseStamina = 300;
                    this.baseLife = 75000;
                    this.baseMind = 0;
                    this.gold = 21031;
                    this.experience = 34435;
                    this.level = 65;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area34;
                    this.DropItem[0] = Database.COMMON_FROZEN_HEART;
                    break;
                case Database.ENEMY_WIND_BREAKER:
                    this.baseStrength = 1800;
                    this.baseAgility = 1400;
                    this.baseIntelligence = 1200;
                    this.baseStamina = 800;
                    this.baseLife = 110000;
                    this.baseMind = 0;
                    this.gold = 30009;
                    this.experience = 41321;
                    this.level = 69;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area34;
                    this.DropItem[0] = Database.COMMON_ESSENCE_OF_WIND;
                    break;
                case Database.ENEMY_TUNDRA_LONGHORN_DEER:
                    this.baseStrength = 1;
                    this.baseAgility = 750;
                    this.baseIntelligence = 3500;
                    this.baseStamina = 1600;
                    this.baseLife = 150000;
                    this.baseMind = 1600;
                    this.gold = 50016;
                    this.experience = 57850;
                    this.level = 73;
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area34;
                    this.DropItem[0] = Database.RARE_TUNDRA_DEER_HORN;
                    break;
                #endregion

                #region "ボス"
                case Database.ENEMY_BOSS_HOWLING_SEIZER:
                    this.baseStrength = 4500;
                    this.baseAgility = 950;
                    this.baseIntelligence = 1;
                    this.baseStamina = 3400;
                    this.baseMind = 1200;
                    this.experience = 115700;
                    this.baseLife = 560000;
                    this.baseInstantPoint = 15000;
                    this.level = 85;
                    this.gold = 200000;
                    this.ResistStun = true;
                    this.ResistParalyze = true;
                    this.ResistFrozen = true;
                    this.Rare = RareString.Gold;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss3;
                    this.DropItem[0] = Database.EPIC_ORB_SILENT_COLD_ICE;
                    this.UseStackCommand = true;
                    break;

                case Database.ENEMY_DRAGON_DESOLATOR_AZOLD:
                    this.baseStrength = 99930;
                    this.baseAgility = 500;//4543;会話専用のため、スピードを減らす。
                    this.baseIntelligence = 11250;
                    this.baseStamina = 44510;
                    this.baseMind = 3190;
                    this.experience = 0;//15000000;
                    this.baseLife = 49938705;
                    this.baseInstantPoint = 100000;
                    this.level = 253;
                    this.gold = 15000000;
                    this.Rare = RareString.Purple;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.TruthBoss3;
                    this.UseStackCommand = true;
                    break;
                #endregion
                #endregion
                #region "４階"
                #region "エリア１"
                case Database.ENEMY_GENAN_HUNTER:
                    this.baseStrength = 1600;
                    this.baseAgility = 1200;
                    this.baseIntelligence = 1;
                    this.baseStamina = 800;
                    this.baseLife = 98000;
                    this.baseMind = 0;
                    this.gold = 45010;
                    this.experience = 42439;
                    this.level = 75;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area41;
                    this.DropItem[0] = Database.COMMON_HUNTER_SEVEN_TOOL;
                    this.InitialTarget = TargetLogic.Back;
                    break;
                case Database.ENEMY_BEAST_MASTER:
                    this.baseStrength = 1700;
                    this.baseAgility = 800;
                    this.baseIntelligence = 1;
                    this.baseStamina = 800;
                    this.baseLife = 105000;
                    this.baseMind = 0;
                    this.gold = 45361;
                    this.experience = 43712;
                    this.level = 75;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area41;
                    this.DropItem[0] = Database.COMMON_BEAST_KEGAWA;
                    break;
                case Database.ENEMY_ELDER_ASSASSIN:
                    this.baseStrength = 1;
                    this.baseAgility = 1800;
                    this.baseIntelligence = 1300;
                    this.baseStamina = 900;
                    this.baseLife = 86000;
                    this.baseMind = 0;
                    this.gold = 46219;
                    this.experience = 45023;
                    this.level = 75;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area41;
                    this.DropItem[0] = Database.RARE_BLOOD_DAGGER_KAKERA;
                    this.InitialTarget = TargetLogic.Back;
                    break;
                case Database.ENEMY_FALLEN_SEEKER:
                    this.baseStrength = 1500;
                    this.baseAgility = 1200;
                    this.baseIntelligence = 1200;
                    this.baseStamina = 1400;
                    this.baseLife = 140000;
                    this.baseMind = 0;
                    this.gold = 50572;
                    this.experience = 60781;
                    this.level = 78;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area41;
                    this.DropItem[0] = Database.COMMON_SABI_BUGU;
                    break;
                #endregion
                #region "エリア２"
                case Database.ENEMY_MASTER_LOAD:
                    this.baseStrength = 1;
                    this.baseAgility = 1400;
                    this.baseIntelligence = 1800;
                    this.baseStamina = 1700;
                    this.baseLife = 280000;
                    this.baseMind = 0;
                    this.gold = 47401;
                    this.experience = 45586;
                    this.level = 79;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area42;
                    this.DropItem[0] = Database.RARE_ESSENCE_OF_DARK;
                    break;
                case Database.ENEMY_EXECUTIONER:
                    this.baseStrength = 2500;
                    this.baseAgility = 700;
                    this.baseIntelligence = 1000;
                    this.baseStamina = 2000;
                    this.baseLife = 330000;
                    this.baseMind = 0;
                    this.gold = 47993;
                    this.experience = 46954;
                    this.level = 79;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area42;
                    this.DropItem[0] = Database.COMMON_EXECUTIONER_ROBE;
                    break;
                case Database.ENEMY_DARK_MESSENGER:
                    this.baseStrength = 1;
                    this.baseAgility = 1;
                    this.baseIntelligence = 4500;
                    this.baseStamina = 500;
                    this.baseLife = 180000;
                    this.baseMind = 0;
                    this.gold = 48552;
                    this.experience = 48362;
                    this.level = 79;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area42;
                    this.DropItem[0] = Database.COMMON_SEEKER_HEAD;
                    break;
                case Database.ENEMY_BLACKFIRE_MASTER_BLADE:
                    this.baseStrength = 3000;
                    this.baseAgility = 1400;
                    this.baseIntelligence = 2500;
                    this.baseStamina = 2200;
                    this.baseLife = 380000;
                    this.baseMind = 0;
                    this.gold = 53341;
                    this.experience = 58035;
                    this.level = 83;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area42;
                    this.DropItem[0] = Database.RARE_MASTERBLADE_KAKERA;
                    this.DropItem[1] = Database.RARE_MASTERBLADE_FIRE;
                    break;
                case Database.ENEMY_SIN_THE_DARKELF:
                    this.baseStrength = 1;
                    this.baseAgility = 1200;
                    this.baseIntelligence = 4500;
                    this.baseStamina = 2000;
                    this.baseLife = 460000;
                    this.baseMind = 0;
                    this.gold = 64015;
                    this.experience = 81249;
                    this.level = 87;
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area42;
                    this.DropItem[0] = Database.COMMON_GREAT_JEWELCROWN;
                    break;
                #endregion
                #region "エリア３"
                case Database.ENEMY_SUN_STRIDER:
                    this.baseStrength = 2800;
                    this.baseAgility = 1600;
                    this.baseIntelligence = 1;
                    this.baseStamina = 2500;
                    this.baseLife = 450000;
                    this.baseMind = 0;
                    this.gold = 56810;
                    this.experience = 60936;
                    this.level = 85;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area43;
                    this.DropItem[0] = Database.RARE_ESSENCE_OF_SHINE;
                    break;
                case Database.ENEMY_ARC_DEMON:
                    this.baseStrength = 2900;
                    this.baseAgility = 1100;
                    this.baseIntelligence = 2200;
                    this.baseStamina = 1000;
                    this.baseLife = 490000;
                    this.baseMind = 0;
                    this.gold = 58155;
                    this.experience = 62765;
                    this.level = 85;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area43;
                    this.DropItem[0] = Database.RARE_DEMON_HORN;
                    break;
                case Database.ENEMY_BALANCE_IDLE:
                    this.baseStrength = 2222;
                    this.baseAgility = 2222;
                    this.baseIntelligence = 2222;
                    this.baseStamina = 2222;
                    this.baseLife = 550000;
                    this.baseMind = 0;
                    this.gold = 68322;
                    this.experience = 75317;
                    this.level = 89;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area43;
                    this.DropItem[0] = Database.COMMON_KUMITATE_TENBIN;
                    this.DropItem[1] = Database.COMMON_KUMITATE_TENBIN_BOU;
                    this.DropItem[2] = Database.COMMON_KUMITATE_TENBIN_DOU;
                    break;
                case Database.ENEMY_GO_FLAME_SLASHER:
                    this.baseStrength = 3300;
                    this.baseAgility = 2200;
                    this.baseIntelligence = 3600;
                    this.baseStamina = 3000;
                    this.baseLife = 570000;
                    this.baseMind = 0;
                    this.gold = 68065;
                    this.experience = 76071;
                    this.level = 89;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area43;
                    this.DropItem[0] = Database.RARE_ESSENCE_OF_FLAME;
                    break;
                case Database.ENEMY_DEVIL_CHILDREN:
                    this.baseStrength = 3000;
                    this.baseAgility = 2500;
                    this.baseIntelligence = 4000;
                    this.baseStamina = 1000;
                    this.baseLife = 666666;
                    this.baseMind = 0;
                    this.gold = 91489;
                    this.experience = 106499;
                    this.level = 93;
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area43;
                    this.DropItem[0] = Database.RARE_BLACK_SEAL_IMPRESSION;
                    break;
                #endregion
                #region "エリア４"
                case Database.ENEMY_HOWLING_HORROR:
                    this.baseStrength = 1;
                    this.baseAgility = 2800;
                    this.baseIntelligence = 3300;
                    this.baseStamina = 2000;
                    this.baseLife = 590000;
                    this.baseMind = 0;
                    this.gold = 76042;
                    this.experience = 74549;
                    this.level = 91;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area44;
                    this.DropItem[0] = Database.COMMON_ONRYOU_HAKO;
                    break;
                case Database.ENEMY_PAIN_ANGEL:
                    this.baseStrength = 4600;
                    this.baseAgility = 2000;
                    this.baseIntelligence = 3500;
                    this.baseStamina = 2200;
                    this.baseLife = 630000;
                    this.baseMind = 0;
                    this.gold = 77844;
                    this.experience = 76786;
                    this.level = 91;
                    this.Rare = RareString.Black;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area44;
                    this.DropItem[0] = Database.RARE_ANGEL_SILK;
                    break;
                case Database.ENEMY_CHAOS_WARDEN:
                    this.baseStrength = 1;
                    this.baseAgility = 1000;
                    this.baseIntelligence = 5500;
                    this.baseStamina = 4000;
                    this.baseLife = 700000;
                    this.baseMind = 0;
                    this.gold = 99037;
                    this.experience = 92143;
                    this.level = 95;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area44;
                    this.DropItem[0] = Database.RARE_CHAOS_SIZUKU;
                    break;
                case Database.ENEMY_DOOM_BRINGER:
                    this.baseStrength = 6000;
                    this.baseAgility = 3000;
                    this.baseIntelligence = 1;
                    this.baseStamina = 2000;
                    this.baseLife = 800000;
                    this.baseMind = 0;
                    this.gold = 146267;
                    this.experience = 129000;
                    this.level = 99;
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area44;
                    this.DropItem[0] = Database.RARE_DOOMBRINGER_KAKERA;
                    this.DropItem[1] = Database.RARE_DOOMBRINGER_TUKA;
                    break;
                #endregion

                #region "ボス"
                case Database.ENEMY_BOSS_LEGIN_ARZE_1:
                case Database.ENEMY_BOSS_LEGIN_ARZE_2:
                case Database.ENEMY_BOSS_LEGIN_ARZE_3:
                    //this.name = Database.ENEMY_BOSS_LEGIN_ARZE;
                    this.baseStrength = 1;
                    this.baseAgility = 1500;
                    this.baseIntelligence = 1300; // ベース値が強すぎると、BUFFDOWN魔法の威力が強すぎなので、意図的に弱体化//13000;
                    this.baseStamina = 17000;
                    this.baseMind = 100;
                    this.experience = 125000;
                    this.baseLife = 2590000;
                    if (createName == Database.ENEMY_BOSS_LEGIN_ARZE_2) this.baseLife = 2800000;
                    if (createName == Database.ENEMY_BOSS_LEGIN_ARZE_3) this.baseLife = 3100000;
                    this.baseMana = 2720000;
                    this.baseInstantPoint = 25000;
                    this.level = 107;
                    this.gold = 500000;
                    this.ResistStun = true;
                    this.ResistParalyze = true;
                    this.ResistFrozen = true;
                    this.Rare = RareString.Gold;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss4;
                    //this.DropItem[0] = Database.EPIC_ORB_DESTRUCT_FIRE;
                    this.UseStackCommand = true;
                    break;

                case Database.ENEMY_DRAGON_IDEA_CAGE_ZEED:
                    this.baseStrength = 21260;
                    this.baseAgility = 1000; // 会話専用のため、スピードを減らす。
                    this.baseIntelligence = 92370;
                    this.baseStamina = 81100;
                    this.baseMind = 3950;
                    this.experience = 0;
                    this.baseLife = 53910687;
                    this.baseInstantPoint = 100000;
                    this.level = 254;
                    this.gold = 15000000;
                    this.Rare = RareString.Purple;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.TruthBoss4;
                    this.UseStackCommand = true;
                    break;
                #endregion
                #endregion
                #region "５階"
                case Database.ENEMY_PHOENIX:
                    this.baseStrength = 3400;
                    this.baseAgility = 2500;
                    this.baseIntelligence = 5100;
                    this.baseStamina = 5000;
                    this.baseLife = 500000;
                    this.baseMind = 0;
                    this.baseResistFire = 30000;
                    this.gold = 560000;
                    this.experience = 130000;
                    this.level = 110;
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area51;
                    break;

                case Database.ENEMY_NINE_TAIL:
                    this.baseStrength = 6100;
                    this.baseAgility = 2800;
                    this.baseIntelligence = 1600;
                    this.baseStamina = 5500;
                    this.baseLife = 550000;
                    this.baseMind = 0;
                    this.gold = 620000;
                    this.experience = 130000;
                    this.level = 111;
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area51;
                    break;
                
                case Database.ENEMY_JUDGEMENT:
                    this.baseStrength = 3800;
                    this.baseAgility = 2400;
                    this.baseIntelligence = 3800;
                    this.baseStamina = 6000;
                    this.baseLife = 650000;
                    this.baseMind = 0;
                    this.gold = 680000;
                    this.experience = 130000;
                    this.level = 112;
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area51;
                    break;
                
                case Database.ENEMY_EMERALD_DRAGON:
                    this.baseStrength = 2200;
                    this.baseAgility = 3000;
                    this.baseIntelligence = 6200;
                    this.baseStamina = 4600;
                    this.baseLife = 600000;
                    this.baseMind = 0;
                    this.gold = 730000;
                    this.experience = 130000;
                    this.level = 113;
                    this.Rare = RareString.Red;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Area51;
                    break;

                case Database.ENEMY_BOSS_BYSTANDER_EMPTINESS:
                    this.baseStrength = 3000;
                    this.baseAgility = 100;
                    this.baseIntelligence = 3000;
                    this.baseStamina = 9999;
                    this.baseLife = 9900009;
                    this.baseMind = 1;
                    this.baseInstantPoint = 8000;
                    this.ResistBlind = true;
                    this.ResistFrozen = true;
                    this.ResistNoResurrection = true;
                    this.ResistParalyze = true;
                    this.ResistPoison = true;
                    this.ResistSilence = true;
                    this.ResistSlip = true;
                    this.ResistSlow = true;
                    this.ResistStun = true;
                    this.ResistTemptation = true;
                    this.gold = 0;
                    this.experience = 0;
                    this.level = 200;
                    this.Rare = RareString.Gold;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.Boss5;
                    break;

                case Database.ENEMY_DRAGON_ALAKH_VES_T_ETULA:
                    this.baseStrength = 78772;
                    this.baseAgility = 1000; // 会話専用のため、スピードを減らす。
                    this.baseIntelligence = 56910;
                    this.baseStamina = 92500;
                    this.baseMind = 5850;
                    this.experience = 0;
                    this.baseLife = 68119820;
                    this.baseInstantPoint = 100000;
                    this.level = 255;
                    this.gold = 15000000;
                    this.Rare = RareString.Purple;
                    this.Armor = ArmorType.Normal;
                    this.Area = MonsterArea.TruthBoss5;
                    this.UseStackCommand = true;
                    break;
                #endregion

                #region "真実世界"
                case Database.ENEMY_LAST_RANA_AMILIA:
                    this.fullName = Database.ENEMY_LAST_RANA_AMILIA;
                    this.baseStrength = 1150;
                    this.baseAgility = 1400;
                    this.baseIntelligence = 1400;
                    this.baseStamina = 700;
                    this.baseMind = 400;
                    this.baseLife = 0;
                    this.baseMind = 0;
                    this.level = 0;
                    for (int ii = 0; ii < 70; ii++)
                    {
                        this.baseLife += this.LevelUpLifeTruth;
                        this.baseMana += this.LevelUpManaTruth;
                        this.level++;
                    }
                    this.Rare = RareString.Legendary;
                    this.gold = 0;
                    this.experience = 0;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.EPIC_JUZA_THE_PHANTASMAL_CLAW);
                    this.MainArmor = new ItemBackPack(Database.RARE_WHITE_GOLD_CROSS);
                    this.Accessory = new ItemBackPack(Database.COMMON_PLATINUM_RING_1);
                    this.Accessory2 = new ItemBackPack(Database.COMMON_PLATINUM_RING_10);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.ENEMY_LAST_SINIKIA_KAHLHANZ:
                    this.fullName = Database.ENEMY_LAST_SINIKIA_KAHLHANZ;
                    this.baseStrength = 1;
                    this.baseAgility = 2600;
                    this.baseIntelligence = 1549;
                    this.baseStamina = 600;
                    this.baseMind = 300;
                    this.baseLife = 0;
                    this.baseMind = 0;
                    this.level = 0;
                    for (int ii = 0; ii < 70; ii++)
                    {
                        this.baseLife += this.LevelUpLifeTruth;
                        this.baseMana += this.LevelUpManaTruth;
                        this.level++;
                    }
                    this.Rare = RareString.Legendary;
                    this.gold = 0;
                    this.experience = 0;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.POOR_PRACTICE_SWORD_1);
                    this.MainArmor = new ItemBackPack(Database.EPIC_YAMITUYUKUSA_MOON_ROBE_2);
                    this.Accessory = new ItemBackPack(Database.RARE_DANZAI_ANGEL_GOHU);
                    this.Accessory2 = new ItemBackPack(Database.LEGENDARY_ANASTELISA_INNOCENT_FIRE_RING_2);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.ENEMY_LAST_OL_LANDIS:
                    this.fullName = Database.ENEMY_LAST_OL_LANDIS;
                    this.baseStrength = 1800;
                    this.baseAgility = 2700;
                    this.baseIntelligence = 700;
                    this.baseStamina = 700;
                    this.baseMind = 600;
                    this.baseLife = 0;
                    this.baseMind = 0;
                    this.level = 0;
                    for (int ii = 0; ii < 70; ii++)
                    {
                        this.baseLife += this.LevelUpLifeTruth;
                        this.baseMana += this.LevelUpManaTruth;
                        this.level++;
                    }
                    this.Rare = RareString.Legendary;
                    this.gold = 0;
                    this.experience = 0;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.EPIC_JUZA_THE_PHANTASMAL_CLAW);
                    this.SubWeapon = new ItemBackPack(Database.RARE_TYOU_KOU_SHIELD);
                    this.MainArmor = new ItemBackPack(Database.EPIC_EZEKRIEL_ARMOR_SIGIL);
                    this.Accessory = new ItemBackPack(Database.RARE_DANZAI_ANGEL_GOHU);
                    this.Accessory2 = new ItemBackPack(Database.COMMON_GREEN_CRYSTAL);
                    this.Area = MonsterArea.Duel;
                    this.OpponentUseInstantPoint = true;
                    break;

                case Database.ENEMY_LAST_VERZE_ARTIE:
                    this.fullName = Database.ENEMY_LAST_VERZE_ARTIE;
                    this.baseStrength = 1500;
                    this.baseAgility = 100;
                    this.baseIntelligence = 1000;
                    this.baseStamina = 5650;//1047;
                    this.baseMind = 500;
                    this.baseLife = 0;
                    this.baseMind = 0;
                    this.baseSpecialInstant = 15000;
                    this.level = 0;
                    for (int ii = 0; ii < 70; ii++)
                    {
                        this.baseLife += this.LevelUpLifeTruth;
                        this.baseMana += this.LevelUpManaTruth;
                        this.level++;
                    }
                    this.Rare = RareString.Legendary;
                    this.gold = 0;
                    this.experience = 0;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.POOR_PRACTICE_SWORD);
                    this.MainArmor = new ItemBackPack(Database.LEGENDARY_LAMUDA_BLACK_AERIAL_ARMOR);
                    this.Accessory = new ItemBackPack(Database.LEGENDARY_EPSIRON_HEAVENLY_SKY_WING);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.ENEMY_LAST_SIN_VERZE_ARTIE:
                    this.fullName = Database.ENEMY_LAST_SIN_VERZE_ARTIE;
                    this.baseStrength = 3500;
                    this.baseAgility = 500;
                    this.baseIntelligence = 1900;
                    this.baseStamina = 8681;//1047;
                    this.baseMind = 2200;
                    this.baseLife = 0;
                    this.baseMind = 0;
                    this.baseSpecialInstant = 12000;
                    this.level = 0;
                    for (int ii = 0; ii < 70; ii++)
                    {
                        this.baseLife += this.LevelUpLifeTruth;
                        this.baseMana += this.LevelUpManaTruth;
                        this.level++;
                    }
                    this.Rare = RareString.Legendary;
                    this.gold = 0;
                    this.experience = 0;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.LEGENDARY_TAU_WHITE_SILVER_SWORD);
                    this.MainArmor = new ItemBackPack(Database.LEGENDARY_LAMUDA_BLACK_AERIAL_ARMOR);
                    this.Accessory = new ItemBackPack(Database.LEGENDARY_EPSIRON_HEAVENLY_SKY_WING);
                    this.Accessory2 = new ItemBackPack(Database.LEGENDARY_SEFINE_HYMNUS_RING);
                    this.Area = MonsterArea.LastBoss;
                    break;

                    
                #endregion

                #region "DUEL闘技場"
                case Database.DUEL_EONE_FULNEA:
                    this.fullName = Database.DUEL_EONE_FULNEA;
                    this.baseStrength = 2;
                    this.baseAgility = 3;
                    this.baseIntelligence = 23;
                    this.baseStamina = 8;
                    this.baseMind = 3;
                    this.experience = 0;
                    this.level = 4;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.COMMON_WHITE_ROD);
                    this.MainArmor = new ItemBackPack(Database.COMMON_BLUE_ROBE);
                    this.Accessory = new ItemBackPack(Database.COMMON_FINE_FEATHER);
                    this.Accessory2 = new ItemBackPack(Database.COMMON_KIREINA_ORB);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_MAGI_ZELKIS:
                    this.fullName = Database.DUEL_MAGI_ZELKIS;
                    this.baseStrength = 15;
                    this.baseAgility = 5;
                    this.baseIntelligence = 11;
                    this.baseStamina = 16;
                    this.baseMind = 4;
                    this.baseLife = 0;
                    this.baseMana = 0;
                    this.experience = 0;
                    this.level = 7;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.COMMON_ZELKIS_SWORD);
                    this.MainArmor = new ItemBackPack(Database.COMMON_ZELKIS_ARMOR);
                    this.Accessory = new ItemBackPack(Database.COMMON_RED_PENDANT);
                    this.Accessory2 = new ItemBackPack(Database.COMMON_RED_PENDANT);
                    this.backpack = new ItemBackPack[2];
                    this.backpack[0] = new ItemBackPack(Database.COMMON_NORMAL_RED_POTION);
                    this.backpack[1] = new ItemBackPack(Database.COMMON_NORMAL_RED_POTION);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_SELMOI_RO:
                    this.fullName = Database.DUEL_SELMOI_RO;
                    this.baseStrength = 25;
                    this.baseAgility = 15;
                    this.baseIntelligence = 2;
                    this.baseStamina = 20;
                    this.baseMind = 5;
                    this.baseLife = 0;
                    this.baseMana = 0;
                    this.experience = 0;
                    this.level = 10;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.COMMON_FINE_SWORD);
                    this.SubWeapon = new ItemBackPack(Database.COMMON_FINE_SWORD);
                    this.MainArmor = new ItemBackPack(Database.COMMON_FINE_ARMOR);
                    this.Accessory = new ItemBackPack(Database.COMMON_PRISM_EMBLEM);
                    this.Accessory2 = new ItemBackPack(Database.COMMON_PRISM_EMBLEM);
                    this.backpack = new ItemBackPack[1];
                    this.backpack[0] = new ItemBackPack(Database.COMMON_FROZEN_BALL);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_KARTIN_MAI:
                    this.fullName = Database.DUEL_KARTIN_MAI;
                    this.baseStrength = 1;
                    this.baseAgility = 10;
                    this.baseIntelligence = 60;
                    this.baseStamina = 15;
                    this.baseMind = 6;
                    this.baseLife = 0;
                    this.baseMana = 0;
                    this.experience = 0;
                    this.level = 13;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.COMMON_KASHI_ROD);
                    this.MainArmor = new ItemBackPack(Database.COMMON_COTTON_ROBE);
                    this.Accessory = new ItemBackPack(Database.COMMON_FINE_FEATHER);
                    this.Accessory2 = new ItemBackPack(Database.COMMON_FINE_FEATHER);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_JEDA_ARUS:
                    this.fullName = Database.DUEL_JEDA_ARUS;
                    this.baseStrength = 50;
                    this.baseAgility = 10;
                    this.baseIntelligence = 12;
                    this.baseStamina = 45;
                    this.baseMind = 1;
                    this.baseLife = 0;
                    this.baseMana = 0;
                    this.experience = 0;
                    this.level = 16;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.RARE_AERO_BLADE);
                    this.SubWeapon = new ItemBackPack(Database.RARE_AERO_BLADE);
                    this.MainArmor = new ItemBackPack(Database.RARE_SUN_BRAVE_ARMOR);
                    this.Accessory = new ItemBackPack(Database.RARE_SINTYUU_RING_KUROHEBI);
                    this.Accessory2 = new ItemBackPack(Database.RARE_SINTYUU_RING_AKAHYOU);
                    this.backpack = new ItemBackPack[2];
                    this.backpack[0] = new ItemBackPack(Database.RARE_PURE_WATER);
                    this.backpack[1] = new ItemBackPack(Database.RARE_PURE_GREEN_WATER);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_SINIKIA_VEILHANTU:
                    this.fullName = Database.DUEL_SINIKIA_VEILHANTU;
                    this.baseStrength = 2;
                    this.baseAgility = 2;
                    this.baseIntelligence = 80;
                    this.baseStamina = 50;
                    this.baseMind = 20;
                    this.baseLife = 0;
                    this.baseMana = 0;
                    this.experience = 0;
                    this.level = 20;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.EPIC_DEVIL_EYE_ROD);
                    this.MainArmor = new ItemBackPack(Database.RARE_MAGICIANS_MANTLE);
                    this.Accessory = new ItemBackPack(Database.COMMON_PURPLE_AMULET);
                    this.Accessory2 = new ItemBackPack(Database.COMMON_PURPLE_AMULET);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_ADEL_BRIGANDY:
                    this.fullName = Database.DUEL_ADEL_BRIGANDY;
                    this.baseStrength = 77;
                    this.baseAgility = 60;
                    this.baseIntelligence = 5;
                    this.baseStamina = 40;
                    this.baseMind = 8;
                    this.baseLife = 0;
                    this.baseMana = 0;
                    this.experience = 0;
                    this.level = 23;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.RARE_ICE_SWORD);
                    this.MainArmor = new ItemBackPack(Database.RARE_SUN_BRAVE_ARMOR);
                    this.Accessory = new ItemBackPack(Database.RARE_SINTYUU_RING_KUROHEBI);
                    this.Accessory2 = new ItemBackPack(Database.RARE_SINTYUU_RING_KUROHEBI);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_LENE_COLTOS:
                    this.fullName = Database.DUEL_LENE_COLTOS;
                    this.baseStrength = 5;
                    this.baseAgility = 5;
                    this.baseIntelligence = 150;
                    this.baseStamina = 78;
                    this.baseMind = 10;
                    this.baseLife = 0;
                    this.baseMana = 0;
                    this.experience = 0;
                    this.level = 26;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.RARE_BLUE_LIGHTNING);
                    this.MainArmor = new ItemBackPack(Database.COMMON_SMART_ROBE);
                    this.Accessory = new ItemBackPack(Database.COMMON_GREEN_CHARM);
                    this.Accessory2 = new ItemBackPack(Database.COMMON_GREEN_CHARM);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_SCOTY_ZALGE:
                    this.fullName = Database.DUEL_SCOTY_ZALGE;
                    this.baseStrength = 80;
                    this.baseAgility = 140;
                    this.baseIntelligence = 2;
                    this.baseStamina = 80;
                    this.baseMind = 3;
                    this.baseLife = 0;
                    this.baseMana = 0;
                    this.experience = 0;
                    this.level = 29;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.COMMON_ZALGE_CLAW);
                    this.SubWeapon = new ItemBackPack(Database.COMMON_ZALGE_CLAW);
                    this.MainArmor = new ItemBackPack(Database.COMMON_SERPENT_ARMOR);
                    this.Accessory = new ItemBackPack(Database.RARE_SEAL_OF_DEATH);
                    this.Accessory2 = new ItemBackPack(Database.RARE_EMBLEM_BLUESTAR);
                    this.backpack[0] = new ItemBackPack(Database.COMMON_NORMAL_RED_POTION);
                    this.backpack[1] = new ItemBackPack(Database.COMMON_NORMAL_RED_POTION);
                    this.backpack[2] = new ItemBackPack(Database.COMMON_NORMAL_RED_POTION);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_PERMA_WARAMY:
                    this.fullName = Database.DUEL_PERMA_WARAMY;
                    this.baseStrength = 19;
                    this.baseAgility = 5;
                    this.baseIntelligence = 125;
                    this.baseStamina = 100;
                    this.baseMind = 140;
                    this.baseLife = 0;
                    this.baseMana = 0;
                    this.experience = 0;
                    this.level = 32;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.RARE_BURNING_CLAYMORE);
                    this.MainArmor = new ItemBackPack(Database.RARE_RED_THUNDER_ROBE);
                    this.Accessory = new ItemBackPack(Database.COMMON_RED_KOKUIN);
                    this.Accessory2 = new ItemBackPack(Database.COMMON_RED_KOKUIN);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_KILT_JORJU:
                    this.fullName = Database.DUEL_KILT_JORJU;
                    this.baseStrength = 100;
                    this.baseAgility = 100;
                    this.baseIntelligence = 100;
                    this.baseStamina = 100;
                    this.baseMind = 100;
                    this.baseLife = 0;
                    this.baseMana = 0;
                    this.experience = 0;
                    this.level = 35;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.COMMON_SMASH_BLADE);
                    this.SubWeapon = new ItemBackPack(Database.COMMON_PURE_BRONZE_SHIELD);
                    this.MainArmor = new ItemBackPack(Database.COMMON_SERPENT_ARMOR);
                    this.Accessory = new ItemBackPack(Database.EPIC_FAZIL_ORB_1);
                    this.Accessory2 = new ItemBackPack(Database.EPIC_FAZIL_ORB_2);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_BILLY_RAKI:
                    this.fullName = Database.DUEL_BILLY_RAKI;
                    this.baseStrength = 301;
                    this.baseAgility = 40;
                    this.baseIntelligence = 1;
                    this.baseStamina = 260;
                    this.baseMind = 1;
                    this.baseLife = 50;
                    this.baseMana = 0;
                    this.experience = 0;
                    this.level = 0;
                    for (int ii = 0; ii < 38; ii++)
                    {
                        this.baseLife += this.LevelUpLifeTruth;
                        this.baseMana += this.LevelUpManaTruth;
                        this.level++;
                    }
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.RARE_MENTALIZED_FORCE_CLAW);
                    this.SubWeapon = new ItemBackPack(Database.RARE_MENTALIZED_FORCE_CLAW);
                    this.MainArmor = new ItemBackPack(Database.RARE_SCALE_BLUERAGE);
                    this.Accessory = new ItemBackPack(Database.RARE_EARTH_BREAKERS_SIGIL);
                    this.Accessory2 = new ItemBackPack(Database.RARE_LIVING_GROWTH_SEED);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_ANNA_HAMILTON:
                    this.fullName = Database.DUEL_ANNA_HAMILTON;
                    this.baseStrength = 10;
                    this.baseAgility = 120;
                    this.baseIntelligence = 420;
                    this.baseStamina = 202;
                    this.baseMind = 80;
                    this.baseLife = 50;
                    this.baseMana = 0;
                    this.experience = 0;
                    this.level = 0;
                    for (int ii = 0; ii < 41; ii++)
                    {
                        this.baseLife += this.LevelUpLifeTruth;
                        this.baseMana += this.LevelUpManaTruth;
                        this.level++;
                    }
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.COMMON_SEKIGAN_ROD);
                    this.MainArmor = new ItemBackPack(Database.COMMON_FLOATING_ROBE);
                    this.Accessory = new ItemBackPack(Database.RARE_TEARS_END);
                    this.Accessory2 = new ItemBackPack(Database.RARE_LIVING_GROWTH_SEED);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_CALMANS_OHN:
                    this.fullName = Database.DUEL_CALMANS_OHN;
                    this.baseStrength = 10;
                    this.baseAgility = 250;
                    this.baseIntelligence = 210;
                    this.baseStamina = 121;
                    this.baseMind = 450;
                    this.baseLife = 50;
                    this.baseMana = 0;
                    this.experience = 0;
                    this.level = 0;
                    for (int ii = 0; ii < 44; ii++)
                    {
                        this.baseLife += this.LevelUpLifeTruth;
                        this.baseMana += this.LevelUpManaTruth;
                        this.level++;
                    }
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.RARE_SHAERING_BONE_CRUSHER);
                    this.MainArmor = new ItemBackPack(Database.RARE_DRAGONSCALE_ARMOR);
                    this.Accessory = new ItemBackPack(Database.RARE_WHITE_TIGER_ANGEL_GOHU);
                    this.Accessory2 = new ItemBackPack(Database.RARE_AERIAL_VORTEX);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_SUN_YU:
                    this.fullName = Database.DUEL_SUN_YU;
                    this.baseStrength = 559;
                    this.baseAgility = 200;
                    this.baseIntelligence = 80;
                    this.baseStamina = 450;
                    this.baseMind = 10;
                    this.baseLife = 50;
                    this.baseMana = 0;
                    this.experience = 0;
                    this.level = 0;
                    for (int ii = 0; ii < 47; ii++)
                    {
                        this.baseLife += this.LevelUpLifeTruth;
                        this.baseMana += this.LevelUpManaTruth;
                        this.level++;
                    }
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.RARE_BLUE_LIGHT_MOON_CLAW);
                    this.SubWeapon = new ItemBackPack(Database.RARE_BLUE_LIGHT_MOON_CLAW);
                    this.MainArmor = new ItemBackPack(Database.RARE_SCALE_BLUERAGE);
                    this.Accessory = new ItemBackPack(Database.RARE_WHITE_TIGER_ANGEL_GOHU);
                    this.Accessory2 = new ItemBackPack(Database.RARE_SHIHANDAI_KUROOBI);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_SHUVALTZ_FLORE:
                    this.fullName = Database.DUEL_SHUVALTZ_FLORE;
                    this.baseStrength = 10;
                    this.baseAgility = 450;
                    this.baseIntelligence = 160;
                    this.baseStamina = 900;
                    this.baseMind = 85;
                    this.baseLife = 50;
                    this.baseMana = 0;
                    this.experience = 0;
                    this.level = 0;
                    for (int ii = 0; ii < 50; ii++)
                    {
                        this.baseLife += this.LevelUpLifeTruth;
                        this.baseMana += this.LevelUpManaTruth;
                        this.level++;
                    }
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.EPIC_SHUVALTZ_FLORE_SWORD);
                    this.SubWeapon = new ItemBackPack(Database.EPIC_SHUVALTZ_FLORE_SHIELD);
                    this.MainArmor = new ItemBackPack(Database.EPIC_SHUVALTZ_FLORE_ARMOR);
                    this.Accessory = new ItemBackPack(Database.EPIC_SHUVALTZ_FLORE_ACCESSORY1);
                    this.Accessory2 = new ItemBackPack(Database.EPIC_SHUVALTZ_FLORE_ACCESSORY2);
                    this.Area = MonsterArea.Duel;
                    break;

                case Database.DUEL_RVEL_ZELKIS:
                    this.fullName = Database.DUEL_RVEL_ZELKIS;
                    this.baseStrength = 30;
                    this.baseAgility = 400;
                    this.baseIntelligence = 700;
                    this.baseStamina = 311;
                    this.baseMind = 400;
                    this.baseLife = 0;
                    this.baseMana = 0;
                    this.experience = 0;
                    this.level = 0;
                    for (int ii = 0; ii < 52; ii++)
                    {
                        this.baseLife += this.LevelUpLifeTruth;
                        this.baseMana += this.LevelUpManaTruth;
                        this.level++;
                    }
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.COMMON_SWORD_OF_RVEL);
                    this.MainArmor = new ItemBackPack(Database.COMMON_ARMOR_OF_RVEL);
                    this.Accessory = new ItemBackPack(Database.COMMON_PURPLE_MEDALLION);
                    this.Accessory2 = new ItemBackPack(Database.COMMON_PURPLE_MEDALLION);
                    this.backpack = new ItemBackPack[3];
                    this.backpack[0] = new ItemBackPack(Database.COMMON_HUGE_RED_POTION);
                    this.backpack[1] = new ItemBackPack(Database.COMMON_HUGE_BLUE_POTION);
                    this.backpack[2] = new ItemBackPack(Database.COMMON_HUGE_GREEN_POTION);
                    this.Area = MonsterArea.Duel;
                    break;
                case Database.DUEL_VAN_HEHGUSTEL:
                    this.fullName = Database.DUEL_VAN_HEHGUSTEL;
                    this.baseStrength = 1;
                    this.baseAgility = 450;
                    this.baseIntelligence = 1253;
                    this.baseStamina = 396;
                    this.baseMind = 1;
                    this.baseLife = 0;
                    this.baseMana = 0;
                    this.experience = 0;
                    this.level = 0;
                    for (int ii = 0; ii < 54; ii++)
                    {
                        this.baseLife += this.LevelUpLifeTruth;
                        this.baseMana += this.LevelUpManaTruth;
                        this.level++;
                    }
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.RARE_INVISIBLE_STATE_ROD);
                    this.MainArmor = new ItemBackPack(Database.COMMON_MASTER_CROSS);
                    this.Accessory = new ItemBackPack(Database.COMMON_LIGHT_SERVANT);
                    this.Accessory2 = new ItemBackPack(Database.RARE_CORE_ESSENCE_CHANNEL);
                    this.Area = MonsterArea.Duel;
                    break;
                case Database.DUEL_OHRYU_GENMA:
                    this.fullName = Database.DUEL_OHRYU_GENMA;
                    this.baseStrength = 1385;
                    this.baseAgility = 1;
                    this.baseIntelligence = 1;
                    this.baseStamina = 1000;
                    this.baseMind = 1;
                    this.baseLife = 0;
                    this.baseMana = 0;
                    this.experience = 0;
                    this.level = 0;
                    for (int ii = 0; ii < 56; ii++)
                    {
                        this.baseLife += this.LevelUpLifeTruth;
                        this.baseMana += this.LevelUpManaTruth;
                        this.level++;
                    }
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.RARE_SHINGETUEN_CLAW);
                    this.SubWeapon = new ItemBackPack(Database.RARE_SHINGETUEN_CLAW);
                    this.MainArmor = new ItemBackPack(Database.RARE_BLOOD_BLAZER_CROSS);
                    this.Accessory = new ItemBackPack(Database.RARE_SOUSUI_HIDENSYO);
                    this.Accessory2 = new ItemBackPack(Database.COMMON_FLOATING_WHITE_BALL);
                    this.Area = MonsterArea.Duel;
                    break;
                case Database.DUEL_LADA_MYSTORUS:
                    this.fullName = Database.DUEL_LADA_MYSTORUS;
                    this.baseStrength = 1;
                    this.baseAgility = 500;
                    this.baseIntelligence = 1003;
                    this.baseStamina = 500;
                    this.baseMind = 700;
                    this.baseLife = 0;
                    this.baseMana = 0;
                    this.experience = 0;
                    this.level = 0;
                    for (int ii = 0; ii < 58; ii++)
                    {
                        this.baseLife += this.LevelUpLifeTruth;
                        this.baseMana += this.LevelUpManaTruth;
                        this.level++;
                    }
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.EPIC_LADA_ACHROMATIC_ORB);
                    this.SubWeapon = new ItemBackPack(Database.RARE_SLIDE_THROUGH_SHIELD);
                    this.MainArmor = new ItemBackPack(Database.RARE_WHITE_GOLD_CROSS);
                    this.Accessory = new ItemBackPack(Database.RARE_OLD_TREE_SINKI);
                    this.Accessory2 = new ItemBackPack(Database.RARE_OLD_TREE_SINKI);
                    this.Area = MonsterArea.Duel;
                    break;
                case Database.DUEL_SIN_OSCURETE:
                    this.fullName = Database.DUEL_SIN_OSCURETE;
                    this.baseStrength = 590;
                    this.baseAgility = 630;
                    this.baseIntelligence = 480;
                    this.baseStamina = 500;
                    this.baseMind = 854;
                    this.baseLife = 0;
                    this.baseMana = 0;
                    this.experience = 0;
                    this.level = 0;
                    for (int ii = 0; ii < 60; ii++)
                    {
                        this.baseLife += this.LevelUpLifeTruth;
                        this.baseMana += this.LevelUpManaTruth;
                        this.level++;
                    }
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.EPIC_MEIKOU_DOOMBRINGER);
                    this.MainArmor = new ItemBackPack(Database.RARE_WHITE_GOLD_CROSS);
                    this.Accessory = new ItemBackPack(Database.RARE_ANGEL_CONTRACT);
                    this.Accessory2 = new ItemBackPack(Database.RARE_SEAL_OF_BALANCE);
                    this.Area = MonsterArea.Duel;
                    break;

                #endregion

                #region "シニキア・カールハンツ、元核習得前"
                case Database.DUEL_SINIKIA_KAHLHANZ:
                    this.fullName = Database.DUEL_SINIKIA_KAHLHANZ;
                    this.baseStrength = Database.SINIKIA_KAHLHANTZ_FIRST_STRENGTH;
                    this.baseAgility = Database.SINIKIA_KAHLHANTZ_FIRST_AGILITY;
                    this.baseIntelligence = Database.SINIKIA_KAHLHANTZ_FIRST_INTELLIGENCE;
                    this.baseStamina = Database.SINIKIA_KAHLHANTZ_FIRST_STAMINA;
                    this.baseMind = Database.SINIKIA_KAHLHANTZ_FIRST_MIND;
                    this.baseLife = 8080;
                    this.baseMana = 6015;
                    this.experience = 0;
                    this.level = 50;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.LEGENDARY_DARKMAGIC_DEVIL_EYE_REPLICA);
                    this.MainArmor = new ItemBackPack(Database.EPIC_YAMITUYUKUSA_MOON_ROBE);
                    this.Accessory = new ItemBackPack(Database.LEGENDARY_ZVELDOSE_DEVIL_FIRE_RING);
                    this.Accessory2 = new ItemBackPack(Database.LEGENDARY_ANASTELISA_INNOCENT_FIRE_RING);
                    this.Area = MonsterArea.Duel;
                    break;
                #endregion

                #region "オル・ランディス仲間にする前"
                case Database.DUEL_OL_LANDIS:
                    this.fullName = Database.DUEL_OL_LANDIS;
                    this.baseStrength = Database.OL_LANDIS_FIRST_STRENGTH;//650;
                    this.baseAgility = Database.OL_LANDIS_FIRST_AGILITY; // 210;
                    this.baseIntelligence = Database.OL_LANDIS_FIRST_INTELLIGENCE;//5
                    this.baseStamina = Database.OL_LANDIS_FIRST_STAMINA; //205;
                    this.baseMind = Database.OL_LANDIS_FIRST_MIND;//266;
                    this.baseLife = 2080;
                    this.baseMana = 1290;
                    this.experience = 0;
                    this.level = 35;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.POOR_GOD_FIRE_GLOVE_REPLICA);
                    this.MainArmor = new ItemBackPack(Database.COMMON_AURA_ARMOR);
                    this.Accessory = new ItemBackPack(Database.COMMON_FATE_RING);
                    this.Accessory2 = new ItemBackPack(Database.COMMON_LOYAL_RING);
                    this.Area = MonsterArea.Duel;
                    break;
                #endregion

                #region "ヴェルゼ・アーティDUELその１"
                case Database.VERZE_ARTIE:
                    this.fullName = Database.VERZE_ARTIE_FULL;
                    this.baseStrength = 100;
                    this.baseAgility = 1205;
                    this.baseIntelligence = 100;
                    this.baseStamina = 100;
                    this.baseMind = 100;
                    for (int ii = 0; ii < 50; ii++)
                    {
                        this.baseLife += this.LevelUpLifeTruth;
                        this.baseMana += this.LevelUpManaTruth;
                        this.Level++;
                    }
                    this.experience = 0;
                    this.gold = 0;
                    this.Rare = RareString.Blue;
                    this.Armor = ArmorType.Normal;
                    this.MainWeapon = new ItemBackPack(Database.LEGENDARY_TAU_WHITE_SILVER_SWORD_0);
                    this.MainArmor = new ItemBackPack(Database.LEGENDARY_LAMUDA_BLACK_AERIAL_ARMOR_0);
                    this.Accessory = new ItemBackPack(Database.LEGENDARY_EPSIRON_HEAVENLY_SKY_WING_0);
                    this.Area = MonsterArea.Duel;
                    break;
                #endregion

                #region "ダミー素振り君"
                case Database.DUEL_DUMMY_SUBURI:
                    this.baseStrength = 1000;
                    this.baseAgility = 500;
                    this.baseIntelligence = 1000;
                    this.baseStamina = 9999;
                    this.baseMind = 700;
                    this.experience = 0;
                    this.baseLife = 9990009;
                    this.ResistFire = 99999999;
                    this.Rare = RareString.Black;
                    this.level = 1;
                    this.gold = 0;
                    this.Area = MonsterArea.Area31;
                    break;
                #endregion

            }
            if (this.baseLife == 0)
            {
                this.baseLife = 50 + (this.level - 1) * 20;
            }
            if (this.baseMana == 0)
            {
                this.baseMana = 80 + (this.level - 1) * 15;
            }

            float powerValue = 1.0f;
            if (GroundOne.Difficulty == 1)
            {
                powerValue = 0.75f;
            }
            this.baseStrength = ((int)((float)this.baseStrength * powerValue)); if (this.baseStrength <= 1) this.baseStrength = 1;
            this.baseAgility = ((int)((float)this.baseAgility * powerValue)); if (this.baseAgility <= 1) this.baseAgility = 1;
            this.baseIntelligence = ((int)((float)this.baseIntelligence * powerValue)); if (this.baseIntelligence <= 1) this.baseIntelligence = 1;
            this.baseStamina = ((int)((float)this.baseStamina * powerValue)); if (this.baseStamina <= 1) this.baseStamina = 1;
            this.baseMind = ((int)((float)this.baseMind * powerValue)); if (this.baseMind <= 1) this.baseMind = 1;
            this.baseLife = ((int)((float)this.baseLife * powerValue)); if (this.baseLife <= 1) this.baseLife = 1;
            this.baseMana = ((int)((float)this.baseMana * powerValue)); if (this.baseMana <= 1) this.baseMana = 1;
            //
            // c 後編編集
            // 後編からは敵もDUELで装備を持つようになります。
            if (this.MainWeapon == null)
            {
                this.MainWeapon = new ItemBackPack("");
            }
            if (this.SubWeapon == null)
            {
                this.SubWeapon = new ItemBackPack(""); // 後編追加
            }
            if (this.MainArmor == null)
            {
                this.MainArmor = new ItemBackPack("");
            }
            if (this.Accessory == null)
            {
                this.Accessory = new ItemBackPack("");
            }
            if (this.Accessory2 == null)
            {
                this.Accessory2 = new ItemBackPack(""); // 後編追加
            }
            // c 後編編集

            this.currentLife = this.MaxLife; // c 後編編集
            this.currentMana = this.MaxMana; // c 後編編集

        }

        public new void CleanUpEffectForBoss()
        {
            base.CleanUpEffectForBoss();
        }

        public void ChoiceTimeSequenceBuff(int number, TruthImage[] list, int currentTurn)
        {
            this.TimeCumulative++;
            double max = Math.Max(Math.Max(Math.Max(Math.Max(list[0].Count, list[1].Count), list[2].Count), list[3].Count), list[4].Count);
            // １つも無い場合、つまり一番初めは２からスタート
            if (max <= 0)
            {
                list[number].Count = 2;
            }
            // ２つ目以降
            else
            {
                int choice = 0;
                // 万が一最大より低い場合はそのまま埋め合わせる。
                if (this.TimeCumulative < max)
                {
                    choice = (int)max;
                }
                // 最大値と同じだった場合は、１大きい状態でセット
                else if (this.TimeCumulative == max)
                {
                    choice = (int)max + 1;
                }
                // 最大値より大きい場合、（普段はココ）
                else // this.TimeCumulative > max
                {
                    // 最大値＋１と同じなのであれば、そのままセット（最大値と同じだった場合は、１大きい状態でセットと同じ）
                    if (this.TimeCumulative <= (int)max + 1)
                    {
                        choice = this.TimeCumulative;
                    }
                    // でなければ、最大値より２以上大きい事となる。飛び飛び数値にせず最大より１大きい状態としてセットしたい
                    else
                    {
                        choice = (int)max + 1;
                    }
                }

                // １０ターン毎に最大値歯車の稼働数を上げたいため、最大値を大きくする
                if ((currentTurn >= 10 && choice == 3) ||
                    (currentTurn >= 20 && choice == 4) ||
                    (currentTurn >= 30 && choice == 5) ||
                    (currentTurn >= 40 && choice == 6))
                {
                    choice++;
                }

                list[number].Count = choice;
            }
        }
    }
}
