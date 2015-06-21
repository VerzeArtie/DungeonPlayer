using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
//using Microsoft.DirectX.DirectSound;
using System.Runtime.InteropServices;
using System.Threading;
using System.Media;

namespace DungeonPlayer
{
    // [警告]：全体的にmcが１人目、scが２人目、tcが３人目を想定した作りになっています。隊列編成を組む場合再構築してください。
    public partial class BattleEnemy : MotherForm
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

        private MainCharacter[] playerList = null;

        private EnemyCharacter1 ec1;
        public EnemyCharacter1 EC1
        {
            get { return ec1; }
            set { ec1 = value; }
        }

        private WorldEnvironment we;
        public WorldEnvironment WE
        {
            get { return we; }
            set { we = value; }
        }

        private int CurrentTimeStop = 0;

        // 敵側が使ってくる場合
        private int CurrentCrushingBlowEnemy = 0;


        private bool acceptLose = false;
        public bool AcceptLose
        {
            get { return acceptLose; }
            set { acceptLose = value; }
        }

        //private Microsoft.DirectX.DirectSound.Device soundDevice;
        //public Microsoft.DirectX.DirectSound.Device SoundDevice
        //{
        //    get { return soundDevice; }
        //    set { soundDevice = value; }
        //}
        //private SecondaryBuffer shotsound = null;

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


        public BattleEnemy()
        {
            InitializeComponent();
            //th = new Thread(new System.Threading.ThreadStart(UpdateXAudio));
            //th.IsBackground = true;
            //th.Start();
        }

        private void BattleEnemy_FormClosing(object sender, FormClosingEventArgs e)
        {
            endSign = true;
            GroundOne.StopDungeonMusic();
            if (GroundOne.sound != null) // 後編編集
            {
                GroundOne.sound.StopMusic(); // 後編編集
                //this.sound.Disactive(); // 後編削除
            }
        }

        private void BattleEnemy_Load(object sender, EventArgs e)
        {
            if (mc != null)
            {
                this.nameLabel1.Text = this.mc.Name;
                UpdateLife(mc);

                if (!mc.AvailableSkill)
                {
                    this.skillLabel1.Visible = false;
                    this.SkillBox.Visible = false;
                    this.label2.Visible = false;
                }
                else
                {
                    UpdateMCSkillPoint(mc);
                }
                
                if (!mc.AvailableMana)
                {
                    this.manaLabel1.Visible = false;
                    this.SpellBox.Visible = false;
                    this.label4.Visible = false;
                }
                else
                {
                    UpdateMCMana(mc);
                }

                // [警告]：BattleEnemy_Load、MainCharacter:CleanUpEffect, MainCharacter:CleanUpBattleEndの展開ミスが増え続けています。
                // s 後編編集
                mc.pbAbsorbWater = (TruthImage)this.pbAbsorbWater;
                mc.pbAetherDrive = (TruthImage)this.pbAetherDrive;
                mc.pbBlackContract = (TruthImage)this.pbBlackContract;
                mc.pbBloodyVengeance = (TruthImage)this.pbBloodyVengeance;
                mc.pbEternalPresence = (TruthImage)this.pbEternalPresence;
                mc.pbFlameAura = (TruthImage)this.pbFlameAura;
                mc.pbGaleWind = (TruthImage)this.pbGaleWind;
                mc.pbGlory = (TruthImage)this.pbGlory;
                mc.pbHeatBoost = (TruthImage)this.pbHeatBoost;
                mc.pbImmortalRave = (TruthImage)this.pbImmortalRave;
                mc.pbOneImmunity = (TruthImage)this.pbOneImmunity;
                mc.pbProtection = (TruthImage)this.pbProtection;
                mc.pbRiseOfImage = (TruthImage)this.pbRiseOfImage;
                mc.pbSaintPower = (TruthImage)this.pbSaintPower;
                mc.pbShadowPact = (TruthImage)this.pbShadowPact;
                mc.pbWordOfFortune = (TruthImage)this.pbWordOfFortune;
                mc.pbWordOfLife = (TruthImage)this.pbWordOfLife;
                mc.pbMirrorImage = (TruthImage)this.pbMirrorImage;
                mc.pbDeflection = (TruthImage)this.pbDeflection;
                mc.pbTruthVision = (TruthImage)this.pbTruthVision1;
                mc.pbPainfulInsanity = (TruthImage)this.pbPainfulInsanity1;
                mc.pbStanceOfFlow = (TruthImage)this.pbStanceOfFlow1;
                mc.pbDamnation = (TruthImage)this.pbDamnation1;
                mc.pbAbsoluteZero = (TruthImage)this.pbAbsoluteZero1;
                mc.pbPromisedKnowledge = (TruthImage)this.pbPromisedKnowledge1;
                mc.pbHighEmotionality = (TruthImage)this.pbHighEmotionality1;
                mc.pbVoidExtraction = (TruthImage)this.pbVoidExtraction1;
                mc.pbAntiStun = (TruthImage)this.pbAntiStun1;
                mc.pbStanceOfDeath = (TruthImage)this.pbStanceOfDeath1;
                mc.pbNothingOfNothingness = (TruthImage)this.pbNothingOfNothingness1;

                mc.pbStun = (TruthImage)this.pbStun1;
                mc.pbSilence = (TruthImage)this.pbSilence1;
                mc.pbPoison = (TruthImage)this.pbPoison1;
                mc.pbTemptation = (TruthImage)this.pbTemptation1;
                mc.pbFrozen = (TruthImage)this.pbFrozen1;
                mc.pbParalyze = (TruthImage)this.pbParalyze1;
                mc.pbNoResurrection = (TruthImage)this.pbNoResurrection1;
                // e 後編編集

                mc.labelName = this.nameLabel1;
                mc.labelLife = this.lifeLabel1;
                mc.labelSkill = this.skillLabel1;
                mc.labelMana = this.manaLabel1;

                // スキル・魔法を習得していない間はその隙間が見えないように配置替えする。
                // 関数化しても良いが、要素が少ないため芋プログラミングで対応。
                if (!mc.AvailableMana && !mc.AvailableSkill)
                {
                    this.ItemBox.Location = new Point(this.ItemBox.Location.X, 335);
                    this.Defense.Location = new Point(this.ItemBox.Location.X, 388);
                }
                else if (mc.AvailableMana && !mc.AvailableSkill)
                {
                    this.SpellBox.Location = new Point(this.SpellBox.Location.X, 322);
                    this.ItemBox.Location = new Point(this.ItemBox.Location.X, 362);
                    this.Defense.Location = new Point(this.Defense.Location.X, 402);
                }
                else if (!mc.AvailableMana && mc.AvailableSkill)
                {
                    this.SkillBox.Location = new Point(this.SpellBox.Location.X, 322);
                    this.ItemBox.Location = new Point(this.ItemBox.Location.X, 362);
                    this.Defense.Location = new Point(this.Defense.Location.X, 402);
                }
            }
            else
            {
                nameLabel1.Visible = false;
                lifeLabel1.Visible = false;
                skillLabel1.Visible = false;
                manaLabel1.Visible = false;
            }

            if (sc != null)
            {
                playerBack.Visible = true;
            }
            else
            {
                playerBack.Visible = false;
            }

            if (sc != null)
            {
                this.nameLabel2.Text = this.sc.Name;
                UpdateLife(sc);

                if (!sc.AvailableSkill)
                {
                    this.skillLabel2.Visible = false;
                }
                else
                {
                    UpdateMCSkillPoint(sc);
                }

                if (!sc.AvailableMana)
                {
                    this.manaLabel2.Visible = false;
                }
                else
                {
                    UpdateMCMana(sc);
                }

                // [警告]：BattleEnemy_Load、MainCharacter:CleanUpEffect, MainCharacter:CleanUpBattleEndの展開ミスが増え続けています。
                // s 後編編集
                sc.pbAbsorbWater = (TruthImage)this.pbAbsorbWater2;
                sc.pbAetherDrive = (TruthImage)this.pbAetherDrive2;
                sc.pbBlackContract = (TruthImage)this.pbBlackContract2;
                sc.pbBloodyVengeance = (TruthImage)this.pbBloodyVengeance2;
                sc.pbEternalPresence = (TruthImage)this.pbEternalPresence2;
                sc.pbFlameAura = (TruthImage)this.pbFlameAura2;
                sc.pbGaleWind = (TruthImage)this.pbGaleWind2;
                sc.pbGlory = (TruthImage)this.pbGlory2;
                sc.pbHeatBoost = (TruthImage)this.pbHeatBoost2;
                sc.pbImmortalRave = (TruthImage)this.pbImmortalRave2;
                sc.pbOneImmunity = (TruthImage)this.pbOneImmunity2;
                sc.pbProtection = (TruthImage)this.pbProtection2;
                sc.pbRiseOfImage = (TruthImage)this.pbRiseOfImage2;
                sc.pbSaintPower = (TruthImage)this.pbSaintPower2;
                sc.pbShadowPact = (TruthImage)this.pbShadowPact2;
                sc.pbWordOfFortune = (TruthImage)this.pbWordOfFortune2;
                sc.pbWordOfLife = (TruthImage)this.pbWordOfLife2;
                sc.pbMirrorImage = (TruthImage)this.pbMirrorImage2;
                sc.pbDeflection = (TruthImage)this.pbDeflection2;
                sc.pbTruthVision = (TruthImage)this.pbTruthVision2;
                sc.pbPainfulInsanity = (TruthImage)this.pbPainfulInsanity2;
                sc.pbStanceOfFlow = (TruthImage)this.pbStanceOfFlow2;
                sc.pbDamnation = (TruthImage)this.pbDamnation2;
                sc.pbAbsoluteZero = (TruthImage)this.pbAbsoluteZero2;
                sc.pbPromisedKnowledge = (TruthImage)this.pbPromisedKnowledge2;
                sc.pbHighEmotionality = (TruthImage)this.pbHighEmotionality2;
                sc.pbVoidExtraction = (TruthImage)this.pbVoidExtraction2;
                sc.pbAntiStun = (TruthImage)this.pbAntiStun2;
                sc.pbStanceOfDeath = (TruthImage)this.pbStanceOfDeath2;
                sc.pbNothingOfNothingness = (TruthImage)this.pbNothingOfNothingness2;

                sc.pbStun = (TruthImage)this.pbStun2;
                sc.pbSilence = (TruthImage)this.pbSilence2;
                sc.pbPoison = (TruthImage)this.pbPoison2;
                sc.pbTemptation = (TruthImage)this.pbTemptation2;
                sc.pbFrozen = (TruthImage)this.pbFrozen2;
                sc.pbParalyze = (TruthImage)this.pbParalyze2;
                sc.pbNoResurrection = (TruthImage)this.pbNoResurrection2;
                // e 後編編集

                sc.labelName = this.nameLabel2;
                sc.labelLife = this.lifeLabel2;
                sc.labelSkill = this.skillLabel2;
                sc.labelMana = this.manaLabel2;
            }
            else
            {
                nameLabel2.Visible = false;
                lifeLabel2.Visible = false;
                skillLabel2.Visible = false;
                manaLabel2.Visible = false;
            }

            if (tc != null)
            {
                this.nameLabel3.Text = this.tc.Name;
                UpdateLife(tc);

                if (!tc.AvailableSkill)
                {
                    this.skillLabel3.Visible = false;
                }
                else
                {
                    UpdateMCSkillPoint(tc);
                }

                if (!tc.AvailableMana)
                {
                    this.manaLabel3.Visible = false;
                }
                else
                {
                    UpdateMCMana(tc);
                }

                // [警告]：BattleEnemy_Load、MainCharacter:CleanUpEffect, MainCharacter:CleanUpBattleEndの展開ミスが増え続けています。
                // s 後編編集
                tc.pbAbsorbWater = (TruthImage)this.pbAbsorbWater3;
                tc.pbAetherDrive = (TruthImage)this.pbAetherDrive3;
                tc.pbBlackContract = (TruthImage)this.pbBlackContract3;
                tc.pbBloodyVengeance = (TruthImage)this.pbBloodyVengeance3;
                tc.pbEternalPresence = (TruthImage)this.pbEternalPresence3;
                tc.pbFlameAura = (TruthImage)this.pbFlameAura3;
                tc.pbGaleWind = (TruthImage)this.pbGaleWind3;
                tc.pbGlory = (TruthImage)this.pbGlory3;
                tc.pbHeatBoost = (TruthImage)this.pbHeatBoost3;
                tc.pbImmortalRave = (TruthImage)this.pbImmortalRave3;
                tc.pbOneImmunity = (TruthImage)this.pbOneImmunity3;
                tc.pbProtection = (TruthImage)this.pbProtection3;
                tc.pbRiseOfImage = (TruthImage)this.pbRiseOfImage3;
                tc.pbSaintPower = (TruthImage)this.pbSaintPower3;
                tc.pbShadowPact = (TruthImage)this.pbShadowPact3;
                tc.pbWordOfFortune = (TruthImage)this.pbWordOfFortune3;
                tc.pbWordOfLife = (TruthImage)this.pbWordOfLife3;
                tc.pbMirrorImage = (TruthImage)this.pbMirrorImage3;
                tc.pbDeflection = (TruthImage)this.pbDeflection3;
                tc.pbTruthVision = (TruthImage)this.pbTruthVision3;
                tc.pbPainfulInsanity = (TruthImage)this.pbPainfulInsanity3;
                tc.pbStanceOfFlow = (TruthImage)this.pbStanceOfFlow3;
                tc.pbDamnation = (TruthImage)this.pbDamnation3;
                tc.pbAbsoluteZero = (TruthImage)this.pbAbsoluteZero3;
                tc.pbPromisedKnowledge = (TruthImage)this.pbPromisedKnowledge3;
                tc.pbHighEmotionality = (TruthImage)this.pbHighEmotionality3;
                tc.pbVoidExtraction = (TruthImage)this.pbVoidExtraction3;
                tc.pbAntiStun = (TruthImage)this.pbAntiStun3;
                tc.pbStanceOfDeath = (TruthImage)this.pbStanceOfDeath3;
                tc.pbNothingOfNothingness = (TruthImage)this.pbNothingOfNothingness3;

                tc.pbStun = (TruthImage)this.pbStun3;
                tc.pbSilence = (TruthImage)this.pbSilence3;
                tc.pbPoison = (TruthImage)this.pbPoison3;
                tc.pbTemptation = (TruthImage)this.pbTemptation3;
                tc.pbFrozen = (TruthImage)this.pbFrozen3;
                tc.pbParalyze = (TruthImage)this.pbParalyze3;
                tc.pbNoResurrection = (TruthImage)this.pbNoResurrection3;
                // e 後編編集

                tc.labelName = this.nameLabel3;
                tc.labelLife = this.lifeLabel3;
                tc.labelSkill = this.skillLabel3;
                tc.labelMana = this.manaLabel3;
            }
            else
            {
                nameLabel3.Visible = false;
                lifeLabel3.Visible = false;
                skillLabel3.Visible = false;
                manaLabel3.Visible = false;
            }

            if (mc != null && sc != null && tc != null)
            {
                playerList = new MainCharacter[3];
                playerList[0] = mc;
                playerList[1] = sc;
                playerList[2] = tc;
            }
            else if (mc != null && sc != null && tc == null)
            {
                playerList = new MainCharacter[2];
                playerList[0] = mc;
                playerList[1] = sc;
            }
            else if (mc != null && sc == null && tc == null)
            {
                playerList = new MainCharacter[1];
                playerList[0] = mc;
            }

            if (ec1 != null)
            {
                enemyNameLabel1.Text = ec1.Name;
                UpdateLife(ec1);
                UpdateMCSkillPoint(ec1);
                UpdateMCMana(ec1);

                // [警告]：敵の方も一通り用意してください。
                // s 後編編集
                ec1.pbAbsorbWater = (TruthImage)this.pbAbsorbWaterEnemy;
                ec1.pbAetherDrive = (TruthImage)this.pbAetherDriveEnemy;
                ec1.pbBlackContract = (TruthImage)this.pbBlackContractEnemy;
                ec1.pbBloodyVengeance = (TruthImage)this.pbBloodyVengeanceEnemy;
                ec1.pbEternalPresence = (TruthImage)this.pbEternalPresenceEnemy;
                ec1.pbFlameAura = (TruthImage)this.pbFlameAuraEnemy;
                ec1.pbGaleWind = (TruthImage)this.pbGaleWindEnemy;
                ec1.pbGlory = (TruthImage)this.pbGloryEnemy;
                ec1.pbHeatBoost = (TruthImage)this.pbHeatBoostEnemy;
                ec1.pbImmortalRave = (TruthImage)this.pbImmortalRaveEnemy;
                ec1.pbOneImmunity = (TruthImage)this.pbOneImmunityEnemy;
                ec1.pbProtection = (TruthImage)this.pbProtectionEnemy;
                ec1.pbRiseOfImage = (TruthImage)this.pbRiseOfImageEnemy;
                ec1.pbSaintPower = (TruthImage)this.pbSaintPowerEnemy;
                ec1.pbShadowPact = (TruthImage)this.pbShadowPactEnemy;
                ec1.pbWordOfFortune = (TruthImage)this.pbWordOfFortuneEnemy;
                ec1.pbWordOfLife = (TruthImage)this.pbWordOfLifeEnemy;
                ec1.pbMirrorImage = (TruthImage)this.pbMirrorImageEnemy;
                ec1.pbDeflection = (TruthImage)this.pbDeflectionEnemy;
                ec1.pbTruthVision = (TruthImage)this.pbTruthVisionEnemy;
                ec1.pbPainfulInsanity = (TruthImage)this.pbPainfulInsanityEnemy;
                ec1.pbStanceOfFlow = (TruthImage)this.pbStanceOfFlowEnemy;
                ec1.pbDamnation = (TruthImage)this.pbDamnationEnemy;
                ec1.pbAbsoluteZero = (TruthImage)this.pbAbsoluteZeroEnemy;
                ec1.pbPromisedKnowledge = (TruthImage)this.pbPromisedKnowledgeEnemy;
                ec1.pbHighEmotionality = (TruthImage)this.pbHighEmotionalityEnemy;
                ec1.pbVoidExtraction = (TruthImage)this.pbVoidExtractionEnemy;
                ec1.pbAntiStun = (TruthImage)this.pbAntiStunEnemy;
                ec1.pbStanceOfDeath = (TruthImage)this.pbStanceOfDeathEnemy;
                ec1.pbNothingOfNothingness = (TruthImage)this.pbNothingOfNothingnessEnemy;

                ec1.pbStun = (TruthImage)this.pbStunEnemy;
                ec1.pbSilence = (TruthImage)this.pbSilenceEnemy;
                ec1.pbPoison = (TruthImage)this.pbPoisonEnemy;
                ec1.pbTemptation = (TruthImage)this.pbTemptationEnemy;
                ec1.pbFrozen = (TruthImage)this.pbFrozenEnemy;
                ec1.pbParalyze = (TruthImage)this.pbParalyzeEnemy;
                ec1.pbNoResurrection = (TruthImage)this.pbNoResurrectionEnemy;
                // e 後編編集

                ec1.labelName = this.enemyNameLabel1;
                ec1.labelLife = this.enemyLifeLabel1;
                ec1.labelMana = this.enemyManaLabel1;
                ec1.labelSkill = this.enemySkillLabel1;

                // 【後編】のアイテムとバッティングした事により削除。
                //if (ec1.Name == "ドゥームブリンガー")
                //{
                //    ec1.MainWeapon = new ItemBackPack("ドゥーム・ブリンガー");
                //}
                if (ec1.Name == "ヴェルゼ・アーティ")
                {
                    this.enemyManaLabel1.Visible = true;
                    this.enemySkillLabel1.Visible = true;
                }
                else
                {
                    this.enemyManaLabel1.Visible = false;
                    this.enemySkillLabel1.Visible = false;
                }
            }
            else
            {
                enemyNameLabel1.Visible = false;
                enemyLifeLabel1.Visible = false;
            }

            UpdateCurrentPlayerLabel();
            for (int ii = 0; ii < playerList.Length; ii++)
            {
                if (playerList[ii].Dead)
                {
                    playerList[ii].labelName.ForeColor = Color.Red;
                    playerList[ii].labelLife.ForeColor = Color.Red;
                    playerList[ii].labelSkill.ForeColor = Color.Red;
                    playerList[ii].labelMana.ForeColor = Color.Red;
                }
            }
            //if (mc != null)
            //{
            //    if (mc.Dead)
            //    {
            //        mc.labelName.ForeColor = Color.Red;
            //        mc.labelLife.ForeColor = Color.Red;
            //        mc.labelSkill.ForeColor = Color.Red;
            //        mc.labelMana.ForeColor = Color.Red;
            //    }
            //}

            //if (sc != null)
            //{
            //    if (sc.Dead)
            //    {
            //        sc.labelName.ForeColor = Color.Red;
            //        sc.labelLife.ForeColor = Color.Red;
            //        sc.labelSkill.ForeColor = Color.Red;
            //        sc.labelMana.ForeColor = Color.Red;
            //    }
            //}

            //if (tc != null)
            //{
            //    if (tc.Dead)
            //    {
            //        tc.labelName.ForeColor = Color.Red;
            //        tc.labelLife.ForeColor = Color.Red;
            //        tc.labelSkill.ForeColor = Color.Red;
            //        tc.labelMana.ForeColor = Color.Red;
            //    }
            //}

            if ((ec1.Name == "一階の守護者：絡みつくフランシス") ||
                (ec1.Name == "二階の守護者：Lizenos") ||
                (ec1.Name == "三階の守護者：Minflore") ||
                (ec1.Name == "四階の守護者：Altomo") )
            {
                this.enemyNameLabel1.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                this.enemyNameLabel1.ForeColor = Color.Firebrick;
                GroundOne.PlayDungeonMusic(Database.BGM04, Database.BGM04LoopBegin);
            }
            else if (ec1.Name == "五階の守護者：Bystander")
            {
                this.enemyNameLabel1.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
                this.enemyNameLabel1.ForeColor = Color.Firebrick;
                GroundOne.PlayDungeonMusic(Database.BGM05, Database.BGM05LoopBegin);
            }
            else
            {
                GroundOne.PlayDungeonMusic(Database.BGM03, Database.BGM03LoopBegin);
            }

        }

        private void UpdateCurrentFocus(PlayerAction pa)
        {
            switch (pa)
            {
                case PlayerAction.Defense:
                    Defense.Focus();
                    break;
                case PlayerAction.None:
                    Attack.Focus(); // 何も指定されていない場合はデフォルトの「こうげき」にフォーカス
                    break;
                case PlayerAction.NormalAttack:
                    Attack.Focus();
                    break;
                case PlayerAction.RunAway:
                    RunAway.Focus();
                    break;
                case PlayerAction.UseItem:
                    ItemBox.Focus();
                    break;
                case PlayerAction.UseSkill:
                    SkillBox.Focus();
                    break;
                case PlayerAction.UseSpell:
                    SpellBox.Focus();
                    break;
                default:
                    Attack.Focus();
                    break;
            }
        }

        private void Attack_Click(object sender, EventArgs e)
        {
            for (int ii = 0; ii < playerList.Length; ii++)
            {
                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].PA == PlayerAction.None)
                {
                    this.playerList[ii].PA = PlayerAction.NormalAttack;
                    if ((ii + 1) == playerList.Length)
                    {
                        break;
                    }
                    else
                    {
                        for (int jj = ii + 1; jj < playerList.Length; jj++)
                        {
                            if (!playerList[jj].Dead && playerList[jj].CurrentFrozen <= 0 && playerList[jj].CurrentStunning <= 0 && playerList[jj].CurrentParalyze <= 0)
                            {
                                currentPlayerLabel.Text = this.playerList[jj].Name;
                                UpdateCurrentFocus(this.playerList[jj].BeforePA);
                                return;
                            }
                        }
                    }
                }
            }
            BattleStart();
        }

        private void SkillBox_Click(object sender, EventArgs e)
        {
            for (int ii = 0; ii < playerList.Length; ii++)
            {
                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].PA == PlayerAction.None)
                {
                    using (BattleSkillRequest bsr = new BattleSkillRequest())
                    {
                        bsr.MC = playerList[ii];
                        bsr.StartPosition = FormStartPosition.CenterParent;
                        bsr.ShowDialog();
                        this.Update();
                        if (bsr.DialogResult == DialogResult.Cancel)
                        {
                            return;
                        }
                        else
                        {
                            this.playerList[ii].CurrentSkillName = bsr.CurrentSkillName;
                        }
                    }
                    playerList[ii].PA = PlayerAction.UseSkill;
                    if ((ii + 1) == playerList.Length)
                    {
                        break;
                    }
                    else
                    {
                        for (int jj = ii + 1; jj < playerList.Length; jj++)
                        {
                            if (!playerList[jj].Dead && playerList[jj].CurrentFrozen <= 0 && playerList[jj].CurrentStunning <= 0 && playerList[jj].CurrentParalyze <= 0)
                            {
                                currentPlayerLabel.Text = this.playerList[jj].Name;
                                UpdateCurrentFocus(this.playerList[jj].BeforePA);
                                return;
                            }
                        }
                    }
                }
            }
            BattleStart();
        }

        private void SpellBox_Click(object sender, EventArgs e)
        {
            for (int ii = 0; ii < playerList.Length; ii++)
            {
                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].PA == PlayerAction.None)
                {
                    using (BattleSpellRequest bsr = new BattleSpellRequest())
                    {
                        bsr.MC = playerList[ii];
                        bsr.WE = this.we;
                        bsr.StartPosition = FormStartPosition.CenterParent;
                        if ((this.sc == null) && (this.tc == null))
                        {
                            bsr.IgnoreSelectTarget = true;
                        }
                        bsr.ShowDialog();
                        this.Update();
                        if (bsr.DialogResult == DialogResult.Cancel)
                        {
                            return;
                        }
                        else
                        {
                            playerList[ii].CurrentSpellName = bsr.CurrentSpellName;
                            // [警告]：隊列が変わるとＮＧになる部分。
                            if (bsr.TargetNum == 1) // 1:アイン、2:ラナ、3：ヴェルゼ、4：敵
                            {
                                playerList[ii].Target = mc;
                            }
                            else if (bsr.TargetNum == 2)
                            {
                                playerList[ii].Target = sc;
                            }
                            else if (bsr.TargetNum == 3)
                            {
                                playerList[ii].Target = tc;
                            }
                            else if (bsr.TargetNum == 4)
                            {
                                playerList[ii].Target = ec1;
                            }
                            else
                            {
                                playerList[ii].Target = null;
                            }
                        }
                        this.playerList[ii].PA = PlayerAction.UseSpell;
                        if ((ii + 1) == playerList.Length)
                        {
                            break;
                        }
                        else
                        {
                            for (int jj = ii + 1; jj < playerList.Length; jj++)
                            {
                                if (!playerList[jj].Dead && playerList[jj].CurrentFrozen <= 0 && playerList[jj].CurrentStunning <= 0 && playerList[jj].CurrentParalyze <= 0)
                                {
                                    currentPlayerLabel.Text = this.playerList[jj].Name;
                                    UpdateCurrentFocus(this.playerList[jj].BeforePA);
                                    return;
                                }
                            }
                        }
                    }
                }
            }

            BattleStart();
        }

        private void Defense_Click(object sender, EventArgs e)
        {
            for (int ii = 0; ii < playerList.Length; ii++)
            {
                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].PA == PlayerAction.None)
                {
                    this.playerList[ii].PA = PlayerAction.Defense;
                    if ((ii + 1) == playerList.Length)
                    {
                        break;
                    }
                    else
                    {
                        for (int jj = ii + 1; jj < playerList.Length; jj++)
                        {
                            if (!playerList[jj].Dead && playerList[jj].CurrentFrozen <= 0 && playerList[jj].CurrentStunning <= 0 && playerList[jj].CurrentParalyze <= 0)
                            {
                                currentPlayerLabel.Text = this.playerList[jj].Name;
                                UpdateCurrentFocus(this.playerList[jj].BeforePA);
                                return;
                            }
                        }
                    }
                }
            }

            BattleStart();
        }

        private void ItemBox_Click(object sender, EventArgs e)
        {
            for (int ii = 0; ii < playerList.Length; ii++)
            {
                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].PA == PlayerAction.None)
                {
                    ItemBackPack[] backpackData = playerList[ii].GetBackPackInfo();
                    using (BattleItemRequest bir = new BattleItemRequest())
                    {
                        bir.BackPackData = backpackData;
                        bir.StartPosition = FormStartPosition.CenterParent;
                        bir.WE = this.we;
                        bir.ShowDialog();
                        if (bir.TryUseNum == -1) // [警告]：-1がキャンセルと同等とは、わかりにくいです。
                        {
                            return;
                        }
                        else
                        {
                            // [警告]：隊列が変わるとＮＧになる部分。
                            if (bir.TargetNum == 1) // 1:アイン、2:ラナ、3：ヴェルゼ、4：敵
                            {
                                playerList[ii].Target = mc;
                            }
                            else if (bir.TargetNum == 2)
                            {
                                playerList[ii].Target = sc;
                            }
                            else if (bir.TargetNum == 3)
                            {
                                playerList[ii].Target = tc;
                            }
                            else if (bir.TargetNum == 4)
                            {
                                playerList[ii].Target = ec1;
                            }
                            else
                            {
                                playerList[ii].Target = null;
                            }
                        }
                        playerList[ii].CurrentUsingItem = backpackData[bir.TryUseNum].Name;
                    }
                    this.playerList[ii].PA = PlayerAction.UseItem;
                    if ((ii + 1) == playerList.Length)
                    {
                        break;
                    }
                    else
                    {
                        for (int jj = ii + 1; jj < playerList.Length; jj++)
                        {
                            if (!playerList[jj].Dead && playerList[jj].CurrentFrozen <= 0 && playerList[jj].CurrentStunning <= 0 && playerList[jj].CurrentParalyze <= 0)
                            {
                                currentPlayerLabel.Text = this.playerList[jj].Name;
                                UpdateCurrentFocus(this.playerList[jj].BeforePA);
                                return;
                            }
                        }
                    }
                }
            }

            BattleStart();
        }

        private void RunAway_Click(object sender, EventArgs e)
        {
            // 逃げるコマンドは、１人でも生きていれば逃げられるため、デッドフラグや凍結フラグは見なくて良い。
            // 凍結してると逃げられないという意見もあるが、逃げて良いことにする。
            if (mc != null) this.mc.PA = PlayerAction.RunAway;
            if (sc != null) this.sc.PA = PlayerAction.RunAway;
            if (tc != null) this.tc.PA = PlayerAction.RunAway;
            UpdateCurrentFocus(PlayerAction.RunAway);
            BattleStart();
        }

        // [警告]：最後の一人が決定した後、ＧＯサインを出すかどうかを確認できる仕組みを構築しておいてください。
        private void playerBack_Click(object sender, EventArgs e)
        {
            for (int ii = playerList.Length; ii > 0; ii--)
            {
                if (playerList[ii - 1].PA != PlayerAction.None)
                {
                    playerList[ii - 1].PA = PlayerAction.None;
                    playerList[ii - 1].CurrentUsingItem = String.Empty;
                    playerList[ii - 1].CurrentSkillName = String.Empty;
                    playerList[ii - 1].CurrentSpellName = String.Empty;
                    currentPlayerLabel.Text = this.playerList[ii - 1].Name;
                    UpdateCurrentFocus(this.playerList[ii - 1].BeforePA);
                    break;
                }
            }
        }


        private bool bossSpecialActionFlag;
        private MainCharacter bossSpecialActionTarget = null;
        private int bossAlreadyActionNum = -1; // ヴェルゼのGaleWind用で作られた新しいフラグ、GaleWindの仕組みを再考してください。

        private void EnemyAttackPhase()
        {
            EnemyAttackPhase(null, -1);
        }
        private void EnemyAttackPhase(MainCharacter alreadyTargetting, int alreadyAction)
        {
            //UpdateBattleText(ec1.Name + "の攻撃フェイズ。\r\n"); // なんらかの戦闘中に攻撃フェイズが分からないため付けたが普段は不要な戦闘コマンドである

            // 自分の状態が行動可能かどうかを確認します。
            if (this.CurrentTimeStop > 0)
            {
                UpdateBattleText(ec1.Name + "の時間は停止している！時間経過を認識しないまま時間が過ぎ去る！！\r\n");
                bossSpecialActionFlag = false;
                bossSpecialActionTarget = null;
                return;
            }

            if (ec1.CurrentStunning > 0)
            {
                UpdateBattleText(ec1.Name + "は気絶している。\r\n");
                return;
            }

            if (ec1.CurrentParalyze > 0)
            {
                UpdateBattleText(ec1.Name + "は麻痺している。\r\n");
                return;
            }

            if (ec1.CurrentFrozen > 0)
            {
                UpdateBattleText(ec1.Name + "は凍結している。\r\n");
                return;
            }

            if (ec1.CurrentTemptation > 0)
            {
                UpdateBattleText(ec1.Name + "は誘惑にかられている。\r\n");
                return;
            }

            if (ec1.PA == PlayerAction.UseSkill && ec1.CurrentAbsoluteZero > 0)
            {
                UpdateBattleText(ec1.GetCharacterSentence(87));
                return;
            }

            if (ec1.PA == PlayerAction.UseSpell && ec1.CurrentAbsoluteZero > 0)
            {
                UpdateBattleText(ec1.GetCharacterSentence(76));
                return;
            }

            // 対象を選定します。
            Random rd2 = new Random(DateTime.Now.Millisecond);
            int randomData = rd2.Next(1, 11);

            MainCharacter target = null;
            // ラスボスBystanderは同一対象へ連続的にスタックを積み上げる。
            if (ec1.Name == "五階の守護者：Bystander" && ec1.Target != null) 
            {
                alreadyTargetting = ec1.Target;
            }

            if (alreadyTargetting == null)
            {
                // [警告]：隊列が変わるとＮＧになる部分。
                // 凍結している敵を攻撃対象外にするのは戦術の一つである。
                // 戦術パターンの確立を行うにはこの記述方法を昇華させてください。
                Random rd3 = new Random(DateTime.Now.Millisecond);
                int totalAliveNum = 0;

                string lastFoundTarget = string.Empty;
                bool activeTarget = false;
                for (int ii = 0; ii < playerList.Length; ii++)
                {
                    if (ec1.Name == "四階の守護者：Altomo")
                    {
                        if (!playerList[ii].Dead)
                        {
                            lastFoundTarget = playerList[ii].Name;
                        }
                        activeTarget = !playerList[ii].Dead & (playerList[ii].CurrentFrozen <= 0);
                    }
                    else
                    {
                        if (!playerList[ii].Dead)
                        {
                            lastFoundTarget = playerList[ii].Name;
                        }
                        activeTarget = !playerList[ii].Dead;
                    }

                    if (activeTarget)
                    {
                        totalAliveNum++;
                    }
                }

                int targetNum = rd3.Next(0, totalAliveNum);
                int throughNum = 0;
                for (int ii = 0; ii < playerList.Length; ii++)
                {
                    if (ec1.Name == "四階の守護者：Altomo")
                    {
                        activeTarget = !playerList[ii].Dead & (playerList[ii].CurrentFrozen <= 0);
                    }
                    else
                    {
                        activeTarget = !playerList[ii].Dead;
                    }
                    if (activeTarget)
                    {
                        if ((targetNum + throughNum) == ii)
                        {
                            target = playerList[ii];
                            break;
                        }
                    }
                    else
                    {
                        throughNum++;
                    }
                }

                if (bossSpecialActionTarget != null)
                {
                    if (!bossSpecialActionTarget.Dead)
                    {
                        target = bossSpecialActionTarget;
                    }
                }

                if (target == null)
                {
                    if (ec1.Name == "四階の守護者：Altomo")
                    {
                        UpdateBattleText(ec1.Name + "は凍結状態が解ける瞬間を待ち構えている。\r\n");
                    }
                    else
                    {
                        UpdateBattleText(ec1.Name + "は対象を見失っている。何もできない\r\n");
                    }
                    return;
                }

                ec1.Target = target;
            }
            else
            {
                target = alreadyTargetting;
                ec1.Target = alreadyTargetting;
            }

            // StanceOfEyesによる行動キャンセルがないかどうかを確認します。
            for (int ii = 0; ii < playerList.Length; ii++)
            {
                // [警告]：StanceOfEyesが使えないと思われる状態は増える可能性があります。随時確認してください。
                //if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentStanceOfEyes)
                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentStanceOfEyes > 0)
                {
                    if (ec1.Name == "五階の守護者：Bystander")
                    {
                        if (ec1.CurrentNothingOfNothingness > 0)
                        {
                            UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                            //playerList[ii].CurrentStanceOfEyes = false;
                            playerList[ii].RemoveStanceOfEyes(); // 後編編集
                            //playerList[ii].CurrentNegate = false;
                            playerList[ii].RemoveNegate(); // 後編編集
                            //playerList[ii].CurrentCounterAttack = false;
                            playerList[ii].RemoveCounterAttack(); // 後編編集
                        }
                        else
                        {
                            UpdateBattleText(playerList[ii].GetCharacterSentence(102));
                            //playerList[ii].CurrentStanceOfEyes = false;
                            playerList[ii].RemoveStanceOfEyes(); // 後編編集
                            //playerList[ii].CurrentNegate = false;
                            playerList[ii].RemoveNegate(); // 後編編集
                            //playerList[ii].CurrentCounterAttack = false;
                            playerList[ii].RemoveCounterAttack(); // 後編編集
                        }
                    }
                    else
                    {
                        if (ec1.CurrentNothingOfNothingness > 0)
                        {
                            UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                            //playerList[ii].CurrentStanceOfEyes = false;
                            playerList[ii].RemoveStanceOfEyes(); // 後編編集
                            //playerList[ii].CurrentNegate = false;
                            playerList[ii].RemoveNegate(); // 後編編集
                            //playerList[ii].CurrentCounterAttack = false;
                            playerList[ii].RemoveCounterAttack(); // 後編編集
                        }
                        else
                        {
                            UpdateBattleText(playerList[ii].GetCharacterSentence(101));
                            return;
                        }
                    }
                }
            }


            // ダイスを振り、行動内容を示す番号を選出します。
            Random rd = new Random(DateTime.Now.Millisecond*Environment.TickCount);
            Random rd4 = new Random(DateTime.Now.Millisecond*Environment.TickCount);
            if (bossSpecialActionFlag) randomData = 8;
            if (alreadyAction >= 0) randomData = alreadyAction;

            // 各キャラに応じたアクションを行います。
            #region "一階の雑魚キャラ"
            if (ec1.Name == "ヤング・ゴブリン")
            {
                PlayerNormalAttack(ec1, target, 0, 0, false);
            }
            else if (ec1.Name == "薄汚れた盗賊")
            {
                PlayerNormalAttack(ec1, target, 0, 0, false);
            }
            else if (ec1.Name == "ひ弱なビートル")
            {
                PlayerNormalAttack(ec1, target, 0, 0, false);
            }
            else if (ec1.Name == "幼いエルフ")
            {
                switch (rd4.Next(1, 3))
                {
                    case 1:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                    case 2:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        EnemySpellPutiFireBall(ec1, target);
                        break;
                }
            }
            else if (ec1.Name == "小さなイノシシ")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                    case 2:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                    case 3:
                        UpdateBattleText(ec1.Name + "は猛突進をしてきた！ \r\n");
                        EnemySkillStraightSmash(ec1, target, true, false);
                        break;
                }
            }
            else if (ec1.Name == "俊敏な鷹")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                    case 2:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                    case 3:
                        UpdateBattleText(ec1.Name + "は素早い動作で激しく攻撃してきた！ \r\n");
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                }
            }
            else if (ec1.Name == "シャドウハンター")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                    case 2:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                    case 3:
                        UpdateBattleText(ec1.Name + "は背後から突然攻撃を繰り出してきた！ \r\n");
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                }
            }
            else if (ec1.Name == "着こなしの良いエルフ")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                    case 2:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                    case 3:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        PlayerSpellIceNeedle(ec1, target, 500);
                        break;
                }
            }
            #endregion
            #region "一階の守護者：絡みつくフランシス"
            else if (ec1.Name == "一階の守護者：絡みつくフランシス")
            {
                switch (randomData)
                {
                    case 4:
                    case 5:
                    case 10:
                    case 6:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                    case 1:
                    case 7:
                    case 3:
                        UpdateBattleText(ec1.Name + "：スペル詠唱「ファイアボルト」！\r\n");
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }

                        int enemyAtk = rd.Next(ec1.Intelligence, ec1.Intelligence + 4);
                        if (target.CurrentAbsorbWater > 0)
                        {
                            enemyAtk = (int)((float)enemyAtk / 1.2F);
                        }
                        if ((target.Accessory != null && target.Accessory.Name == "炎授天使の護符"))
                        {
                            enemyAtk -= target.Accessory.MinValue;
                            if (enemyAtk <= 0) enemyAtk = 0;
                        }
                        if ((target.Accessory != null && target.Accessory.Name == "シールオブアクア＆ファイア"))
                        {
                            enemyAtk = (int)((float)enemyAtk * (100.0F - (float)target.Accessory.MinValue) / 100.0F);
                        }
                        if (target.CurrentEternalPresence > 0)
                        {
                            enemyAtk = (int)((float)enemyAtk / 1.3F);
                        }
                        if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                        {
                            if (target.CurrentAbsoluteZero > 0)
                            {
                                UpdateBattleText(target.GetCharacterSentence(88));
                            }
                            else
                            {
                                enemyAtk = (int)((float)enemyAtk / 3.0F);
                            }
                        }
                        if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                        {
                            if (target.CurrentAbsoluteZero > 0)
                            {
                                UpdateBattleText(target.GetCharacterSentence(88));
                            }
                            else
                            {
                                enemyAtk = 0;
                            }
                        }

                        // MirrorImageによる効果
                        if (target.CurrentMirrorImage > 0)
                        {
                            ec1.CurrentLife -= enemyAtk;
                            UpdateLife(ec1);
                            UpdateBattleText(String.Format(target.GetCharacterSentence(58), enemyAtk.ToString(), ec1.Name), 1000);

                            target.CurrentMirrorImage = 0;
                            target.pbMirrorImage.Image = null;
                            target.pbMirrorImage.Update();
                            return;
                        }

                        if (CheckDodge(target)) { return; }

                        target.CurrentLife -= enemyAtk;
                        UpdateLife(target);
                        UpdateBattleText(target.Name + "へ" + enemyAtk.ToString() + "のダメージ\r\n");
                        return;
                    case 2:
                    case 9:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }

                        UpdateBattleText(ec1.Name + "：スペル詠唱「ファイアビューネ」！無数の炎の触手が" + target.Name + "に向けられる！！\r\n");
                        for (int ii = 0; ii < 8; ii++)
                        {
                            enemyAtk = rd.Next(ec1.Intelligence / 6, ec1.Intelligence / 6 + 3);
                            if (target.CurrentAbsorbWater > 0)
                            {
                                enemyAtk = (int)((float)enemyAtk / 1.2F);
                            }
                            if ((target.Accessory != null && target.Accessory.Name == "炎授天使の護符"))
                            {
                                enemyAtk -= target.Accessory.MinValue;
                                if (enemyAtk <= 0) enemyAtk = 0;
                            }
                            if ((target.Accessory != null && target.Accessory.Name == "シールオブアクア＆ファイア"))
                            {
                                enemyAtk = (int)((float)enemyAtk * (100.0F - (float)target.Accessory.MinValue) / 100.0F);
                            }
                            if (target.CurrentEternalPresence > 0)
                            {
                                enemyAtk = (int)((float)enemyAtk / 1.3F);
                            }
                            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    enemyAtk = (int)((float)enemyAtk / 3.0F);
                                }
                            }
                            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    enemyAtk = 0;
                                }
                            }

                            // MirrorImageによる効果
                            if (target.CurrentMirrorImage > 0)
                            {
                                ec1.CurrentLife -= enemyAtk;
                                UpdateLife(ec1);
                                UpdateBattleText(String.Format(target.GetCharacterSentence(58), enemyAtk.ToString(), ec1.Name), 1000);

                                target.CurrentMirrorImage = 0;
                                target.pbMirrorImage.Image = null;
                                target.pbMirrorImage.Update();
                            }
                            else
                            {
                                if (CheckDodge(target)) { continue; }

                                target.CurrentLife -= enemyAtk;
                                UpdateLife(target);
                                UpdateBattleText((ii + 1) + "本目！　" + target.Name + "へ" + enemyAtk.ToString() + "のダメージ\r\n", 300);
                            }
                        }
                        break;
                    case 8:
                        if (!bossSpecialActionFlag)
                        {
                            bossSpecialActionFlag = true;
                            bossSpecialActionTarget = target;
                            UpdateBattleText(ec1.Name + "は" + target.Name + "に対して巨大な槍の形状をした蔦を向け始めた。\r\n");
                        }
                        else
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(105));
                                    }
                                }
                            }

                            bossSpecialActionFlag = false;
                            bossSpecialActionTarget = null;
                            UpdateBattleText(ec1.Name + "：フシュウウゥゥ・・・フシャアアアアァァ！！！\r\n");
                            enemyAtk = rd.Next((ec1.Intelligence + ec1.BuffIntelligence_Accessory) * 2 + (ec1.Strength + ec1.BuffStrength_Accessory) * 2,
                                               (ec1.Intelligence + ec1.BuffIntelligence_Accessory) * 2 + (ec1.Strength + ec1.BuffStrength_Accessory) * 2 + ec1.Agility * 2);
                            // MirrorImageによる効果（ボス必殺技により防げない）
                            if (target.CurrentMirrorImage > 0)
                            {
                                UpdateBattleText(target.GetCharacterSentence(59), 1000);
                                target.CurrentMirrorImage = 0;
                                target.pbMirrorImage.Image = null;
                                target.pbMirrorImage.Update();
                            }

                            if (target.CurrentAbsorbWater > 0)
                            {
                                enemyAtk = (int)((float)enemyAtk / 1.2F);
                            }
                            if (target.CurrentEternalPresence > 0)
                            {
                                enemyAtk = (int)((float)enemyAtk / 1.3F);
                            }
                            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    enemyAtk = (int)((float)enemyAtk / 3.0F);
                                }
                            }
                            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    enemyAtk = 0;
                                }
                            }
                            UpdateBattleText(ec1.Name + "は「奥義　キル・スピニングランサー」発動した！！\r\n");

                            if (CheckDodge(target, true)) { return; }

                            target.CurrentLife -= enemyAtk;
                            UpdateLife(target);
                            UpdateBattleText("巨大な槍が" + target.Name + "へ突き刺さる！！" + target.Name + "へ" + enemyAtk.ToString() + "のダメージ\r\n");
                            UpdateBattleText(target.GetCharacterSentence(6));
                        }
                        break;
                }
            }
            #endregion
            #region "二回の雑魚キャラ"
            else if (ec1.Name == "狂戦士バーサーカー")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                    case 2:
                        EnemyNormalAttack(target);
                        break;
                    case 3:
                        UpdateBattleText(ec1.Name + "は大きな雄たけびを上げて突っ込んできた！ \r\n");
                        EnemyNormalAttack(target, 1, true);
                        break;
                }
            }
            else if (ec1.Name == "青隼")
            {
                EnemyNormalAttack(target);
            }
            else if (ec1.Name == "黒ビートル")
            {
                EnemyNormalAttack(target);
            }
            else if (ec1.Name == "悪意を向ける人間")
            {
                EnemyNormalAttack(target);
            }
            else if (ec1.Name == "オールドツリー")
            {
                switch (rd4.Next(1, 3))
                {
                    case 1:
                        EnemyNormalAttack(target);
                        break;
                    case 2:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        PlayerSpellFireBall(ec1, target, 500);
                        break;
                }
            }
            else if (ec1.Name == "小さなオーガ")
            {
                switch (rd4.Next(1, 3))
                {
                    case 1:
                        EnemyNormalAttack(target);
                        break;
                    case 2:
                        UpdateBattleText(ec1.Name + "は力強く棍棒を振りかざしてきた！\r\n");
                        PlayerNormalAttack(ec1, target, 1.1F, 0, false);
                        break;
                }
            }
            else if (ec1.Name == "エルヴィッシュ・シャーマン")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                    case 2:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        PlayerSpellIceNeedle(ec1, target, 500);
                        break;
                    case 3:
                        EnemyNormalAttack(target);
                        break;
                }
            }
            else if (ec1.Name == "正装をした神官")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        EnemySpellFreshHeal(ec1, ec1);
                        break;
                    case 2:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        EnemySpellHolyShock(ec1, target);
                        break;
                    case 3:
                        EnemyNormalAttack(target);
                        break;
                }
            }
            else if (ec1.Name == "サバンナ・ライオン")
            {
                EnemyNormalAttack(target);
            }
            else if (ec1.Name == "獰猛なハゲタカ")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                    case 2:
                        EnemyNormalAttack(target);
                        break;
                    case 3:
                        UpdateBattleText(ec1.Name + "は素早い動作で激しく攻撃してきた！ \r\n");
                        EnemyNormalAttack(target);
                        EnemyNormalAttack(target);
                        break;
                }
            }
            else if (ec1.Name == "ゴブリン・チーフ")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                    case 2:
                        EnemyNormalAttack(target);
                        break;
                    case 3:
                        if (ec1.CurrentFlameAura <= 0)
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }

                            ec1.Target = ec1;
                            PlayerSpellFlameAura(ec1);
                        }
                        else
                        {
                            EnemyNormalAttack(target);
                        }
                        break;
                }
            }
            else if (ec1.Name == "荒れ狂ったドワーフ")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                    case 2:
                        EnemyNormalAttack(target);
                        break;
                    case 3:
                        UpdateBattleText(ec1.Name + "は大きな雄たけびを上げて突っ込んできた！ \r\n");
                        EnemyNormalAttack(target, 1, true);
                        break;
                }
            }
            else if (ec1.Name == "異形の信奉者")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                    case 2:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        PlayerSpellDarkBlast(ec1, target, 500);
                        break;
                    case 3:
                        EnemyNormalAttack(target);
                        break;
                }
            }
            else if (ec1.Name == "マンイーター")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                    case 2:
                        EnemyNormalAttack(target);
                        break;
                    case 3:
                        if (target.CurrentParalyze <= 0)
                        {
                            UpdateBattleText(ec1.Name + "は人食い触手をのばせてきた！\r\n");
                            target.CurrentParalyze = 3;
                            target.pbParalyze.Image = Image.FromFile(Database.BaseResourceFolder + "Paralyze.bmp");
                            target.pbParalyze.Update();
                            UpdateBattleText(target.Name + "は麻痺状態に陥って動けなくなった！\r\n");
                        }
                        else
                        {
                            EnemyNormalAttack(target);
                        }
                        break;
                }
            }
            else if (ec1.Name == "ヴァンパイア")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                    case 2:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        PlayerSpellDarkBlast(ec1, target, 500);
                        break;
                    case 3:
                        EnemyNormalAttack(target);
                        break;
                }
            }
            else if (ec1.Name == "赤いフードをかぶった人間")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                    case 2:
                        EnemyNormalAttack(target);
                        break;
                    case 3:
                        EnemySkillStraightSmash(ec1, target, false, false);
                        break;
                }
            }
            #endregion
            #region "二階の守護者：Lizenos"
            else if (ec1.Name == "二階の守護者：Lizenos")
            {
                switch (randomData)
                {
                    case 1:
                    case 7:
                    case 3:
                    case 10:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }

                        int enemyAtk = rd.Next(ec1.Intelligence, ec1.Intelligence + 4);
                        target.CurrentLife -= enemyAtk;
                        UpdateBattleText(ec1.Name + "連技の構え　全属詠唱！！\r\n");
                        for (int ii = 0; ii < 4; ii++)
                        {
                            enemyAtk = rd.Next(ec1.Intelligence / 2, ec1.Intelligence / 2 + ec1.Intelligence / 4);
                            // MirrorImageによる効果
                            if (target.CurrentMirrorImage > 0)
                            {
                                ec1.CurrentLife -= enemyAtk;
                                UpdateLife(ec1);
                                UpdateBattleText(String.Format(target.GetCharacterSentence(58), enemyAtk.ToString(), ec1.Name), 1000);

                                target.CurrentMirrorImage = 0;
                                target.pbMirrorImage.Image = null;
                                target.pbMirrorImage.Update();
                            }
                            else
                            {
                                if (target.CurrentAbsorbWater > 0)
                                {
                                    enemyAtk = (int)((float)enemyAtk / 1.2F);
                                }
                                if (target.CurrentEternalPresence > 0)
                                {
                                    enemyAtk = (int)((float)enemyAtk / 1.3F);
                                }
                                if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                                {
                                    if (target.CurrentAbsoluteZero > 0)
                                    {
                                        UpdateBattleText(target.GetCharacterSentence(88));
                                    }
                                    else
                                    {
                                        enemyAtk = (int)((float)enemyAtk / 3.0F);
                                    }
                                }
                                if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                                {
                                    if (target.CurrentAbsoluteZero > 0)
                                    {
                                        UpdateBattleText(target.GetCharacterSentence(88));
                                    }
                                    else
                                    {
                                        enemyAtk = 0;
                                    }
                                }

                                if (CheckDodge(target)) { continue; }

                                target.CurrentLife -= enemyAtk;
                                string elementString = "";
                                if (ii == 0) elementString = "電雷：";
                                else if (ii == 1) elementString = "業火：";
                                else if (ii == 2) elementString = "硬岩";
                                else if (ii == 3) elementString = "鋭氷";
                                UpdateLife(target);
                                UpdateBattleText(target.Name + "へ" + elementString + enemyAtk.ToString() + "のダメージ\r\n", 300);
                            }
                        }
                        break;
                    case 2:
                    case 4:
                    case 5:
                        UpdateBattleText(ec1.Name + "疾風の構え　連続攻撃！！\r\n");
                        for (int ii = 0; ii < 2; ii++)
                        {
                            enemyAtk = rd.Next(ec1.TotalStrength, ec1.TotalStrength + ec1.TotalAgility);
                            if (target.CurrentDeflection > 0)
                            {
                                ec1.CurrentLife -= enemyAtk;
                                UpdateLife(ec1);
                                UpdateBattleText(String.Format(target.GetCharacterSentence(61), enemyAtk.ToString(), ec1.Name), 1000);

                                target.CurrentDeflection = 0;
                                target.pbDeflection.Image = null;
                                target.pbDeflection.Update();
                            }
                            else
                            {
                                enemyAtk -= rd.Next(target.MainArmor.MinValue, target.MainArmor.MaxValue + 1);
                                if (enemyAtk <= 0) enemyAtk = 0;
                                if (target.CurrentProtection > 0)
                                {
                                    enemyAtk = (int)((float)enemyAtk / 1.2F);
                                }
                                if (target.CurrentAetherDrive > 0)
                                {
                                    enemyAtk = (int)((float)enemyAtk / 2.0F);
                                }
                                if (target.CurrentEternalPresence > 0)
                                {
                                    enemyAtk = (int)((float)enemyAtk / 1.3F);
                                }
                                if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                                {
                                    if (target.CurrentAbsoluteZero > 0)
                                    {
                                        UpdateBattleText(target.GetCharacterSentence(88));
                                    }
                                    else
                                    {
                                        enemyAtk = (int)((float)enemyAtk / 3.0F);
                                    }
                                }
                                if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                                {
                                    if (target.CurrentAbsoluteZero > 0)
                                    {
                                        UpdateBattleText(target.GetCharacterSentence(88));
                                    }
                                    else
                                    {
                                        enemyAtk = 0;
                                    }
                                }

                                if (CheckDodge(target)) { continue; }

                                target.CurrentLife -= enemyAtk;
                                UpdateLife(target);
                                UpdateBattleText(target.Name + "へ" + enemyAtk.ToString() + "のダメージ\r\n", 300);
                            }
                        }
                        break;
                    case 9:
                    case 6:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                    case 8:
                        if (!bossSpecialActionFlag)
                        {
                            bossSpecialActionFlag = true;
                            bossSpecialActionTarget = target;
                            UpdateBattleText(ec1.Name + "は" + target.Name + "への構えを解き、明鏡止水の境地に入った。\r\n");
                        }
                        else
                        {
                            bossSpecialActionFlag = false;
                            bossSpecialActionTarget = null;
                            UpdateBattleText(ec1.Name + "：見えまい・・・見る事無く死ね\r\n");
                            UpdateBattleText(ec1.Name + "：神速無形　無限斬衝波！！！\r\n" + target.Name + "に", 1000);
                            for (int ii = 0; ii < 50; ii++)
                            {
                                enemyAtk = rd.Next(ec1.Intelligence / 3, ec1.Intelligence / 3 + ec1.Agility / 3);
                                if (target.CurrentDeflection > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(62), 1000);
                                    target.CurrentDeflection = 0;
                                    target.pbDeflection.Image = null;
                                    target.pbDeflection.Update();
                                }
                                else
                                {
                                    enemyAtk -= rd.Next(target.MainArmor.MinValue, target.MainArmor.MaxValue + 1);
                                    if (enemyAtk <= 0) enemyAtk = 0;
                                    if (target.CurrentAbsorbWater > 0)
                                    {
                                        enemyAtk = (int)((float)enemyAtk / 1.2F);
                                    }
                                    if (target.CurrentEternalPresence > 0)
                                    {
                                        enemyAtk = (int)((float)enemyAtk / 1.3F);
                                    }
                                    if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                                    {
                                        if (target.CurrentAbsoluteZero > 0)
                                        {
                                            UpdateBattleText(target.GetCharacterSentence(88));
                                        }
                                        else
                                        {
                                            enemyAtk = (int)((float)enemyAtk / 3.0F);
                                        }
                                    }
                                    if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                                    {
                                        if (target.CurrentAbsoluteZero > 0)
                                        {
                                            UpdateBattleText(target.GetCharacterSentence(88));
                                        }
                                        else
                                        {
                                            enemyAtk = 0;
                                        }
                                    }

                                    if (CheckDodge(target, true)) { continue; }

                                    target.CurrentLife -= enemyAtk;
                                    UpdateLife(target);
                                    UpdateBattleText("  " + enemyAtk.ToString() + "ダメージ", 30);
                                }
                            }
                            UpdateBattleText("\r\n", 0);
                            UpdateLife(target);
                            UpdateBattleText(target.GetCharacterSentence(7));
                        }
                        break;
                }
            }
            #endregion
            #region "三階の雑魚キャラ"
            else if (ec1.Name == "イビルメージ")
            {
                switch (rd4.Next(1, 5))
                {
                    case 1:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                    case 2:
                    case 3:
                    case 4:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }

                        EnemySpellFlameStrike(ec1, target);
                        break;
                }
            }
            else if (ec1.Name == "ダークシーフ")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                        EnemySkillStraightSmash(ec1, target, false, false);
                        break;
                    case 2:
                        EnemySkillDoubleSlash(ec1, target, false);
                        break;
                    case 3:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                }
            }
            else if (ec1.Name == "アークドルイド")
            {
                // Negateによるスペル詠唱キャンセル
                for (int ii = 0; ii < playerList.Length; ii++)
                {
                    // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                    if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                    {
                        if (ec1.CurrentNothingOfNothingness > 0)
                        {
                            UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                            //playerList[ii].CurrentStanceOfEyes = false;
                            playerList[ii].RemoveStanceOfEyes(); // 後編編集
                            //playerList[ii].CurrentNegate = false;
                            playerList[ii].RemoveNegate(); // 後編編集
                            //playerList[ii].CurrentCounterAttack = false;
                            playerList[ii].RemoveCounterAttack(); // 後編編集
                        }
                        else
                        {
                            UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                            return;
                        }
                    }
                }

                EnemySpellWordOfPower(ec1, target);
            }
            else if (ec1.Name == "シャドウソーサラー")
            {
                // Negateによるスペル詠唱キャンセル
                for (int ii = 0; ii < playerList.Length; ii++)
                {
                    // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                    if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0 )
                    {
                        if (ec1.CurrentNothingOfNothingness > 0)
                        {
                            UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                            //playerList[ii].CurrentStanceOfEyes = false;
                            playerList[ii].RemoveStanceOfEyes(); // 後編編集
                            //playerList[ii].CurrentNegate = false;
                            playerList[ii].RemoveNegate(); // 後編編集
                            //playerList[ii].CurrentCounterAttack = false;
                            playerList[ii].RemoveCounterAttack(); // 後編編集
                        }
                        else
                        {
                            UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                            return;
                        }
                    }
                }

                EnemySpellFrozenLance(ec1, target);
            }
            else if (ec1.Name == "忍者")
            {
                switch (rd4.Next(1, 5))
                {
                    case 1:
                    case 2:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                    case 3:
                        EnemySkillStraightSmash(ec1, target, false, false);
                        break;
                    case 4:
                        UpdateBattleText(ec1.Name + "は素早い動作で激しく攻撃してきた！ \r\n");
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                }
            }
            else if (ec1.Name == "エグゼキュージョナー")
            {
                switch (rd4.Next(1, 5))
                {
                    case 1:
                    case 2:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                    case 3:
                        EnemySkillStraightSmash(ec1, target, false, false);
                        break;
                    case 4:
                        UpdateBattleText(ec1.Name + "は素早い動作で激しく攻撃してきた！ \r\n");
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                }
            }
            else if (ec1.Name == "パワー")
            {
                switch (rd4.Next(1, 3))
                {
                    case 1:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                    case 2:
                        UpdateBattleText(ec1.Name + "は力強く天使の槍を振りかざしてきた！\r\n");
                        PlayerNormalAttack(ec1, target, 1.1F, 0, false);
                        break;
                }
            }
            else if (ec1.Name == "ブラックアイ")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                    case 2:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        PlayerSpellDarkBlast(ec1, target, 500);
                        break;
                    case 3:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                }
            }
            else if (ec1.Name == "エルヴィッシュ神官")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        EnemySpellFreshHeal(ec1, ec1);
                        break;
                    case 2:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        EnemySpellHolyShock(ec1, target);
                        break;
                    case 3:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                }
            }
            else if (ec1.Name == "アプレンティス・ロード")
            {
                switch (rd4.Next(1, 7))
                {
                    case 1:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        MainCharacter temp = ec1.Target;
                        ec1.Target = ec1;
                        PlayerSpellProtection(ec1);
                        ec1.Target = temp;
                        break;
                    case 2:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        EnemySpellHolyShock(ec1, target);
                        break;
                    case 3:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        EnemySpellFreshHeal(ec1, ec1);
                        break;
                    case 4:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        EnemySpellFlameStrike(ec1, target);
                        break;
                    case 5:
                        if (target.CurrentStunning <= 0)
                        {
                            PlayerNormalAttack(ec1, target, 0, 2, false);
                        }
                        else
                        {
                            PlayerNormalAttack(ec1, target, 0, 0, false);
                        }
                        break;
                    case 6:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                }

            }
            else if (ec1.Name == "悪魔崇拝者")
            {
                switch (rd4.Next(1, 5))
                {
                    case 1:
                    case 2:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        PlayerSpellDarkBlast(ec1, target, 500);
                        break;
                    case 3:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                    case 4:
                        if (ec1.CurrentShadowPact <= 0)
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }

                            MainCharacter temp = ec1.Target;
                            ec1.Target = ec1;
                            PlayerSpellShadowPact(ec1);
                            ec1.Target = temp;
                        }
                        break;
                }
            }
            else if (ec1.Name == "デビルメージ")
            {
                switch (rd4.Next(1, 5))
                {
                    case 1:
                    case 2:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        EnemySpellFlameStrike(ec1, target);
                        break;
                    case 3:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                    case 4:
                        if (ec1.CurrentShadowPact <= 0)
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }

                            MainCharacter temp = ec1.Target;
                            ec1.Target = ec1;
                            PlayerSpellShadowPact(ec1);
                            ec1.Target = temp;
                        }
                        break;
                }
            }
            else if (ec1.Name == "聖騎士")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        EnemySpellFreshHeal(ec1, ec1);
                        break;
                    case 2:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        EnemySpellHolyShock(ec1, target);
                        break;
                    case 3:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                }
            }
            else if (ec1.Name == "フォールンシーカー")
            {
                switch (rd4.Next(1, 5))
                {
                    case 1:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        EnemySpellFreshHeal(ec1, ec1);
                        break;
                    case 2:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                    case 3:
                        EnemySkillStraightSmash(ec1, target, false, false);
                        break;
                    case 4:
                        UpdateBattleText(ec1.Name + "は素早い動作で激しく攻撃してきた！ \r\n");
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                }
            }
            else if (ec1.Name == "アイオブザドラゴン")
            {
                switch (rd4.Next(1, 5))
                {
                    case 1:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        EnemySpellFrozenLance(ec1, target);
                        break;
                    case 2:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                    case 3:
                        EnemySkillStraightSmash(ec1, target, false, false);
                        break;
                    case 4:
                        if (target.CurrentParalyze <= 0)
                        {
                            UpdateBattleText(ec1.Name + "は鋭い視線で睨みつけてきた！\r\n");
                            target.CurrentParalyze = 4;
                            target.pbParalyze.Image = Image.FromFile(Database.BaseResourceFolder + "Paralyze.bmp");
                            target.pbParalyze.Update();
                            UpdateBattleText(target.Name + "は麻痺状態に陥って動けなくなった！\r\n");
                        }
                        else
                        {
                            PlayerNormalAttack(ec1, target, 0, 0, false);
                        } 
                        break;
                }
            }
            else if (ec1.Name == "生まれたての悪魔")
            {
                switch (rd4.Next(1, 7))
                {
                    case 1:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }

                        MainCharacter temp = ec1.Target;
                        ec1.Target = ec1;
                        PlayerSpellShadowPact(ec1);
                        ec1.Target = temp;
                        break;
                    case 2:
                        UpdateBattleText(ec1.Name + "は不気味な笑みを浮かべて襲ってきた！\r\n ");
                        EnemySkillStraightSmash(ec1, target, true, false);
                        break;
                    case 3:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }

                        ec1.Target = ec1;
                        PlayerSpellLifeTap(ec1);
                        break;
                    case 4:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }

                        EnemySpellFlameStrike(ec1, target);
                        break;
                    case 5:
                        if (target.CurrentStunning <= 0)
                        {
                            PlayerNormalAttack(ec1, target, 0, 2, false);
                        }
                        else
                        {
                            PlayerNormalAttack(ec1, target, 0, 0, false);
                        }
                        break;
                    case 6:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                }
            }
            #endregion
            #region "三階の守護者：Minflore"
            else if (ec1.Name == "三階の守護者：Minflore")
            {
                switch (randomData)
                {
                    case 4:
                        if (ec1.CurrentLife < ec1.MaxLife * 3 / 5)
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }

                            EnemySpellFreshHeal(ec1, null);
                        }
                        else
                        {
                            PlayerNormalAttack(ec1, target, 0, 0, false);
                        }
                        break;

                    case 2:
                    case 5:
                        PlayerSkillDoubleSlash(ec1, target, false);
                        break;

                    case 1:
                        if (CurrentCrushingBlowEnemy <= 0)
                        {
                            CurrentCrushingBlowEnemy = 2; // １ターン継続のためには、初期値は１＋１
                            PlayerNormalAttack(ec1, target, 0, 2, false);
                        }
                        else
                        {
                            PlayerNormalAttack(ec1, target, 0, 0, false);
                        }
                        break;
                    case 7:
                        if (ec1.CurrentSaintPower <= 0)
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }

                            GroundOne.PlaySoundEffect("SaintPower.mp3");
                            ec1.CurrentSaintPower = 999;
                            ec1.pbSaintPower.Image = Image.FromFile(Database.BaseResourceFolder + "SaintPower.bmp");
                            ec1.pbSaintPower.Update();
                            UpdateBattleText(ec1.GetCharacterSentence(20));
                            //PlayerSpellSaintPower(ec1);
                        }
                        else
                        {
                            PlayerNormalAttack(ec1, target, 0, 0, false);
                        }
                        break;

                    case 3:
                        if (ec1.CurrentGlory <= 0)
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }
                            ec1.Target = ec1;
                            PlayerSpellGlory(ec1);
                        }
                        else
                        {
                            PlayerNormalAttack(ec1, target, 0, 0, false);
                        }
                        break;
                    case 10:
                        if (ec1.CurrentAbsorbWater <= 0)
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }

                            ec1.CurrentAbsorbWater = 999;
                            ec1.pbAbsorbWater.Image = Image.FromFile(Database.BaseResourceFolder + "AbsorbWater.bmp");
                            ec1.pbAbsorbWater.Update();
                            UpdateBattleText(ec1.GetCharacterSentence(19));
                            //PlayerSpellAbsorbWater(ec1);
                        }
                        else
                        {
                            PlayerNormalAttack(ec1, target, 0, 0, false);
                        }
                        break;
                    case 9:
                    case 6:
                        if (ec1.CurrentProtection <= 0)
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }

                            GroundOne.PlaySoundEffect("Protection.mp3");
                            ec1.CurrentProtection = 999;
                            ec1.pbProtection.Image = Image.FromFile(Database.BaseResourceFolder + "Protection.bmp");
                            ec1.pbProtection.Update();
                            UpdateBattleText(ec1.GetCharacterSentence(18));
                            //PlayerSpellProtection(ec1);
                        }
                        else
                        {
                            PlayerNormalAttack(ec1, target, 0, 0, false);
                        }
                        break;
                    case 8:
                        if (!bossSpecialActionFlag)
                        {
                            bossSpecialActionFlag = true;
                            bossSpecialActionTarget = target;
                            UpdateBattleText(ec1.Name + "は" + target.Name + "と距離を置き、一礼の合図を送った。" + target.Name + "に異様な戦慄が走る！！\r\n");
                        }
                        else
                        {
                            bossSpecialActionFlag = false;
                            bossSpecialActionTarget = null;
                            UpdateBattleText(ec1.Name + "    １０の色彩を送るわ。受け取ってちょうだい。カラーズ・ディヴァイン・ソード。\r\n");
                            UpdateBattleText("１０の剣が" + ec1.Name + "の頭上で螺旋上に浮かび上がった。\r\n" + target.Name + "へ", 1000);
                            int enemyAtk = 0;
                            enemyAtk = rd.Next((ec1.TotalStrength), (ec1.TotalStrength) + (ec1.TotalStrength));
                            if (target.CurrentDeflection > 0)
                            {
                                UpdateBattleText(target.GetCharacterSentence(62), 1000);
                                target.CurrentDeflection = 0;
                                target.pbDeflection.Image = null;
                                target.pbDeflection.Update();
                            }

                            enemyAtk -= rd.Next(target.MainArmor.MinValue, target.MainArmor.MaxValue + 1);
                            if (enemyAtk <= 0) enemyAtk = 0;
                            if (target.CurrentProtection > 0)
                            {
                                enemyAtk = (int)((float)enemyAtk / 1.2F);
                            }
                            if (target.CurrentEternalPresence > 0)
                            {
                                enemyAtk = (int)((float)enemyAtk / 1.3F);
                            }
                            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    enemyAtk = (int)((float)enemyAtk / 3.0F);
                                }
                            }
                            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    enemyAtk = 0;
                                }
                            }
                            UpdateBattleText("  瑠璃の色剣：" + enemyAtk.ToString() + "のダメージ", 100);
                            if (CheckDodge(target, true)) { }
                            target.CurrentLife -= enemyAtk;
                            UpdateLife(target);
                            enemyAtk = rd.Next((ec1.TotalStrength), (ec1.TotalStrength) + (ec1.TotalAgility));
                            enemyAtk -= rd.Next(target.MainArmor.MinValue, target.MainArmor.MaxValue + 1);
                            if (enemyAtk <= 0) enemyAtk = 0;
                            if (target.CurrentProtection > 0)
                            {
                                enemyAtk = (int)((float)enemyAtk / 1.2F);
                            }
                            if (target.CurrentEternalPresence > 0)
                            {
                                enemyAtk = (int)((float)enemyAtk / 1.3F);
                            }
                            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    enemyAtk = (int)((float)enemyAtk / 3.0F);
                                }
                            }
                            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    enemyAtk = 0;
                                }
                            }
                            UpdateBattleText("                    桔梗の色剣：" + enemyAtk.ToString() + "のダメージ\r\n\r\n", 100);
                            if (CheckDodge(target, true)) { }
                            target.CurrentLife -= enemyAtk;
                            UpdateLife(target);
                            enemyAtk = rd.Next((ec1.TotalStrength), (ec1.TotalStrength) + (ec1.TotalMind));
                            enemyAtk -= rd.Next(target.MainArmor.MinValue, target.MainArmor.MaxValue + 1);
                            if (enemyAtk <= 0) enemyAtk = 0;
                            if (target.CurrentProtection > 0)
                            {
                                enemyAtk = (int)((float)enemyAtk / 1.2F);
                            }
                            if (target.CurrentEternalPresence > 0)
                            {
                                enemyAtk = (int)((float)enemyAtk / 1.3F);
                            }
                            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    enemyAtk = (int)((float)enemyAtk / 3.0F);
                                }
                            }
                            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    enemyAtk = 0;
                                }
                            }
                            UpdateBattleText("    臙脂の色剣：" + enemyAtk.ToString() + "のダメージ", 100);
                            if (CheckDodge(target, true)) { }
                            target.CurrentLife -= enemyAtk;
                            UpdateLife(target);
                            enemyAtk = rd.Next((ec1.TotalStrength), (ec1.TotalStrength) + (ec1.TotalIntelligence));
                            enemyAtk -= rd.Next(target.MainArmor.MinValue, target.MainArmor.MaxValue + 1);
                            if (enemyAtk <= 0) enemyAtk = 0;
                            if (target.CurrentProtection > 0)
                            {
                                enemyAtk = (int)((float)enemyAtk / 1.2F);
                            }
                            if (target.CurrentEternalPresence > 0)
                            {
                                enemyAtk = (int)((float)enemyAtk / 1.3F);
                            }
                            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    enemyAtk = (int)((float)enemyAtk / 3.0F);
                                }
                            }
                            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    enemyAtk = 0;
                                }
                            }
                            UpdateBattleText("                紫苑の色剣：" + enemyAtk.ToString() + "のダメージ\r\n\r\n", 100);
                            if (CheckDodge(target, true)) { }
                            target.CurrentLife -= enemyAtk;
                            UpdateLife(target);
                            enemyAtk = rd.Next((ec1.TotalAgility), (ec1.TotalAgility) + (ec1.TotalAgility));
                            enemyAtk -= rd.Next(target.MainArmor.MinValue, target.MainArmor.MaxValue + 1);
                            if (enemyAtk <= 0) enemyAtk = 0;
                            if (target.CurrentProtection > 0)
                            {
                                enemyAtk = (int)((float)enemyAtk / 1.2F);
                            }
                            if (target.CurrentEternalPresence > 0)
                            {
                                enemyAtk = (int)((float)enemyAtk / 1.3F);
                            }
                            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    enemyAtk = (int)((float)enemyAtk / 3.0F);
                                }
                            }
                            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    enemyAtk = 0;
                                }
                            }
                            UpdateBattleText("      漆黒の色剣：" + enemyAtk.ToString() + "のダメージ", 100);
                            if (CheckDodge(target, true)) { }
                            target.CurrentLife -= enemyAtk;
                            UpdateLife(target);
                            enemyAtk = rd.Next((ec1.TotalAgility), (ec1.TotalAgility) + (ec1.TotalMind));
                            enemyAtk -= rd.Next(target.MainArmor.MinValue, target.MainArmor.MaxValue + 1);
                            if (enemyAtk <= 0) enemyAtk = 0;
                            if (target.CurrentProtection > 0)
                            {
                                enemyAtk = (int)((float)enemyAtk / 1.2F);
                            }
                            if (target.CurrentEternalPresence > 0)
                            {
                                enemyAtk = (int)((float)enemyAtk / 1.3F);
                            }
                            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    enemyAtk = (int)((float)enemyAtk / 3.0F);
                                }
                            }
                            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    enemyAtk = 0;
                                }
                            }
                            UpdateBattleText("            月草の色剣：" + enemyAtk.ToString() + "のダメージ\r\n\r\n", 100);
                            if (CheckDodge(target, true)) { }
                            target.CurrentLife -= enemyAtk;
                            UpdateLife(target);
                            enemyAtk = rd.Next((ec1.TotalAgility), (ec1.TotalAgility) + (ec1.TotalIntelligence));
                            enemyAtk -= rd.Next(target.MainArmor.MinValue, target.MainArmor.MaxValue + 1);
                            if (enemyAtk <= 0) enemyAtk = 0;
                            if (target.CurrentProtection > 0)
                            {
                                enemyAtk = (int)((float)enemyAtk / 1.2F);
                            }
                            if (target.CurrentEternalPresence > 0)
                            {
                                enemyAtk = (int)((float)enemyAtk / 1.3F);
                            }
                            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    enemyAtk = (int)((float)enemyAtk / 3.0F);
                                }
                            }
                            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    enemyAtk = 0;
                                }
                            }
                            UpdateBattleText("        桜蘭の色剣：" + enemyAtk.ToString() + "のダメージ", 100);
                            if (CheckDodge(target, true)) { }
                            target.CurrentLife -= enemyAtk;
                            UpdateLife(target);
                            enemyAtk = rd.Next((ec1.TotalMind), (ec1.TotalMind) + (ec1.TotalMind));
                            enemyAtk -= rd.Next(target.MainArmor.MinValue, target.MainArmor.MaxValue + 1);
                            if (enemyAtk <= 0) enemyAtk = 0;
                            if (target.CurrentProtection > 0)
                            {
                                enemyAtk = (int)((float)enemyAtk / 1.2F);
                            }
                            if (target.CurrentEternalPresence > 0)
                            {
                                enemyAtk = (int)((float)enemyAtk / 1.3F);
                            }
                            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    enemyAtk = (int)((float)enemyAtk / 3.0F);
                                }
                            }
                            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    enemyAtk = 0;
                                }
                            }
                            UpdateBattleText("        琥珀の色剣：" + enemyAtk.ToString() + "のダメージ\r\n\r\n", 100);
                            if (CheckDodge(target, true)) { }
                            target.CurrentLife -= enemyAtk;
                            UpdateLife(target);
                            enemyAtk = rd.Next((ec1.TotalMind), (ec1.TotalMind) + (ec1.TotalIntelligence));
                            enemyAtk -= rd.Next(target.MainArmor.MinValue, target.MainArmor.MaxValue + 1);
                            if (enemyAtk <= 0) enemyAtk = 0;
                            if (target.CurrentProtection > 0)
                            {
                                enemyAtk = (int)((float)enemyAtk / 1.2F);
                            }
                            if (target.CurrentEternalPresence > 0)
                            {
                                enemyAtk = (int)((float)enemyAtk / 1.3F);
                            }
                            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    enemyAtk = (int)((float)enemyAtk / 3.0F);
                                }
                            }
                            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    enemyAtk = 0;
                                }
                            }
                            UpdateBattleText("          真紅の色剣：" + enemyAtk.ToString() + "のダメージ", 100);
                            if (CheckDodge(target, true)) { }
                            target.CurrentLife -= enemyAtk;
                            UpdateLife(target);
                            enemyAtk = rd.Next((ec1.TotalIntelligence), (ec1.TotalIntelligence) + (ec1.TotalIntelligence));
                            enemyAtk -= rd.Next(target.MainArmor.MinValue, target.MainArmor.MaxValue + 1);
                            if (enemyAtk <= 0) enemyAtk = 0;
                            if (target.CurrentProtection > 0)
                            {
                                enemyAtk = (int)((float)enemyAtk / 1.2F);
                            }
                            if (target.CurrentEternalPresence > 0)
                            {
                                enemyAtk = (int)((float)enemyAtk / 1.3F);
                            }
                            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    enemyAtk = (int)((float)enemyAtk / 3.0F);
                                }
                            }
                            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    enemyAtk = 0;
                                }
                            }
                            UpdateBattleText("    翡翠の色剣：" + enemyAtk.ToString() + "のダメージ\r\n", 100);
                            if (CheckDodge(target, true)) { }
                            target.CurrentLife -= enemyAtk;
                            UpdateLife(target);
                            UpdateBattleText(target.GetCharacterSentence(8));
                        }
                        break;
                }
            }
            #endregion
            #region "四階の雑魚キャラ"
            else if (ec1.Name == "ゴルゴン")
            {
                switch(rd4.Next(1, 4))
                {
                    case 1:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集     
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        EnemySpellFrozenLance(ec1, target);
                        break;
                    case 2:
                        if (target.CurrentParalyze <= 0)
                        {
                            UpdateBattleText(ec1.Name + "は鋭い眼光を向けてきた！\r\n");
                            target.CurrentParalyze = 3;
                            target.pbParalyze.Image = Image.FromFile(Database.BaseResourceFolder + "Paralyze.bmp");
                            target.pbParalyze.Update();
                            UpdateBattleText(target.Name + "は麻痺状態に陥って動けなくなった！\r\n");
                        }
                        else
                        {
                            PlayerNormalAttack(ec1, target, 0, 0, false);
                        }
                        break;
                    case 3:
                        PlayerSkillSilentRush(ec1, target, false);
                        break;
                }
            }
            else if (ec1.Name == "ビーストマスター")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                        if (ec1.CurrentHeatBoost <= 0)
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }

                            ec1.Target = ec1;
                            PlayerSpellHeatBoost(ec1);
                        }
                        else
                        {
                            PlayerSkillStraightSmash(ec1, target, false);
                        }
                        break;
                    case 2:
                        PlayerSkillStraightSmash(ec1, target, false);
                        break;
                    case 3:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }

                        PlayerSpellVolcanicWave(ec1, target, 500);
                        break;
                }
            }
            else if (ec1.Name == "ヒュージスパイダー")
            {
                switch(rd4.Next(1, 4))
                {
                    case 1:
                        if (target.CurrentPoison <= 0)
                        {
                            UpdateBattleText(ec1.Name + "は猛毒の針を突きつけてきた！\r\n");
                            target.CurrentPoison = 999;
                            target.pbPoison.Image = Image.FromFile(Database.BaseResourceFolder + "Poison.bmp");
                            target.pbPoison.Update();
                            UpdateBattleText(target.Name + "は猛毒におかされた！\r\n");

                            UpdateBattleText(ec1.Name + "は蜘蛛の糸をからませてきた！\r\n");
                            target.CurrentParalyze = 3;
                            target.pbParalyze.Image = Image.FromFile(Database.BaseResourceFolder + "Paralyze.bmp");
                            target.pbParalyze.Update();
                            UpdateBattleText(target.Name + "は麻痺状態に陥って動けなくなった！\r\n");
                        }
                        else
                        {
                            PlayerNormalAttack(ec1, target, 0, 0, false);
                        }
                        break;
                    case 2:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }

                        PlayerSpellDevouringPlague(ec1, target);
                        break;
                    case 3:
                        PlayerNormalAttack(ec1, target, 0, 2, false);
                        break;
                }
            }
            else if (ec1.Name == "エルダーアサシン")
            {
                switch (rd4.Next(1, 5))
                {
                    case 1:
                        PlayerSkillSilentRush(ec1, target, false);
                        break;
                    case 2:
                    case 3:
                    case 4:
                        PlayerNormalAttack(ec1, target, 0, 0, false);                       
                        break;
                }
            }
            else if (ec1.Name == "マスターロード")
            {
                switch (rd4.Next(1, 7))
                {
                    case 1:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }

                        MainCharacter temp = ec1.Target;
                        ec1.Target = ec1;
                        PlayerSpellProtection(ec1);
                        ec1.Target = temp;
                        break;
                    case 2:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }

                        EnemySpellHolyShock(ec1, target);
                        break;
                    case 3:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }

                        EnemySpellFreshHeal(ec1, ec1);
                        break;
                    case 4:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }

                        EnemySpellFlameStrike(ec1, target);
                        break;
                    case 5:
                        if (target.CurrentStunning <= 0)
                        {
                            PlayerNormalAttack(ec1, target, 0, 2, false);
                        }
                        else
                        {
                            PlayerNormalAttack(ec1, target, 0, 0, false);
                        }
                        break;
                    case 6:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                }
            }
            else if (ec1.Name == "ブルータルオーガ")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                        if (ec1.CurrentBloodyVengeance <= 0)
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }

                            ec1.Target = ec1;
                            PlayerSpellBloodyVengeance(ec1);
                        }
                        else
                        {
                            UpdateBattleText(ec1.Name + "は大きな拳で猛威を振るってきた！ \r\n");
                            PlayerNormalAttack(ec1, target, 1.2F, 0, false);
                        }
                        break;
                    case 2:
                        UpdateBattleText(ec1.Name + "は大きな拳で猛威を振るってきた！ \r\n");
                        PlayerNormalAttack(ec1, target, 1.2F, 0, false);
                        break;
                    case 3:
                        UpdateBattleText(ec1.Name + "は大きな雄たけびを上げて突っ込んできた！ \r\n");
                        PlayerNormalAttack(ec1, target, 0, 2, true);
                        break;
                }
            }
            else if (ec1.Name == "ウィンドブレイカー")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                        if (ec1.CurrentAetherDrive > 0)
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }

                            ec1.Target = ec1;
                            PlayerSpellAetherDrive(ec1);
                        }
                        else
                        {
                            PlayerNormalAttack(ec1, target, 0, 0, false);
                        }
                        break;
                    case 2:
                        UpdateBattleText(ec1.Name + "は切り裂く刃を無数に発生させてきた！\r\n");
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            int effectValue = ec1.TotalIntelligence + rd.Next(150, 250);
                            if (!playerList[ii].Dead)
                            {
                                if (ec1.CurrentShadowPact > 0)
                                {
                                    effectValue = (int)((float)effectValue * 1.5F);
                                }
                                if (ec1.CurrentEternalPresence > 0)
                                {
                                    effectValue = (int)((float)effectValue * 1.3F);
                                }
                                if (playerList[ii].CurrentAbsorbWater > 0 && ec1.CurrentTruthVision <= 0)
                                {
                                    effectValue = (int)((float)effectValue / 1.2F);
                                }
                                if (playerList[ii].PA == PlayerAction.Defense || playerList[ii].CurrentStanceOfStanding > 0)
                                {
                                    if (playerList[ii].CurrentAbsoluteZero > 0)
                                    {
                                        UpdateBattleText(target.GetCharacterSentence(88));
                                    }
                                    else
                                    {
                                        effectValue = (int)((float)effectValue / 3.0F);
                                    }
                                }
                                if (playerList[ii].CurrentOneImmunity > 0 && (playerList[ii].PA == PlayerAction.Defense || playerList[ii].CurrentStanceOfStanding > 0))
                                {
                                    if (playerList[ii].CurrentAbsoluteZero > 0)
                                    {
                                        UpdateBattleText(target.GetCharacterSentence(88));
                                    }
                                    else
                                    {
                                        effectValue = 0;
                                    }
                                }

                                if (CheckDodge(playerList[ii])) { continue; }

                                GroundOne.PlaySoundEffect("EnemyAttack1.mp3");
                                playerList[ii].CurrentLife -= effectValue;
                                UpdateLife(playerList[ii]);
                                UpdateBattleText(playerList[ii].Name + "へ" + effectValue.ToString() + "のダメージ\r\n");
                            }
                        }
                        break;
                    case 3:
                        PlayerSkillDoubleSlash(ec1, target, false);
                        break;
                }
            }
            else if (ec1.Name == "シン・ザ・ダークエルフ")
            {
                switch (rd4.Next(1, 5))
                {
                    case 1:
                        if (ec1.CurrentPromisedKnowledge <= 0)
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }
                            ec1.Target = ec1;
                            PlayerSpellPromisedKnowledge(ec1);
                        }
                        else
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }
                            PlayerSpellFrozenLance(ec1, target, 500);
                        }
                        break;
                    case 2:
                        if (ec1.CurrentShadowPact <= 0)
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }
                            ec1.Target = ec1;
                            PlayerSpellShadowPact(ec1);
                        }
                        else
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }
                            PlayerSpellFrozenLance(ec1, target, 500);
                        }
                        break;
                    case 3:
                    case 4:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        PlayerSpellFrozenLance(ec1, target, 500);
                        break;
                }
            }
            else if (ec1.Name == "アークデーモン")
            {
                switch (rd4.Next(1, 5))
                {
                    case 1:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        PlayerSpellWordOfPower(ec1, target, 500);
                        break;
                    case 2:
                        if (ec1.CurrentBloodyVengeance <= 0)
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }
                            ec1.Target = ec1;
                            PlayerSpellBloodyVengeance(ec1);
                        }
                        else
                        {
                            UpdateBattleText(ec1.Name + "は激しく拳に力を込めて殴りかかってってきた！\r\n");
                            PlayerNormalAttack(ec1, target, 1.4F, 0, false);
                        }
                        break;
                    case 3:
                        UpdateBattleText(ec1.Name + "は激しく拳に力を込めて殴りかかってってきた！\r\n");
                        PlayerNormalAttack(ec1, target, 1.4F, 0, false);
                        break;
                    case 4:
                        if (ec1.CurrentGaleWind <= 0)
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }
                            PlayerSpellVolcanicWave(ec1, target, 500);
                        }
                        else
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }
                            PlayerSpellWordOfPower(ec1, target, 500);
                        }
                        break;
                }
            }
            else if (ec1.Name == "サン・ストライダー")
            {
                switch (rd4.Next(1, 3))
                {
                    case 1:
                        if (ec1.CurrentBloodyVengeance <= 0)
                        {
                            // 特殊能力とみなし、Negateの対象にはしない。
                            UpdateBattleText(ec1.Name + "は太陽の位置を示す方角へ剣を向けた・・・\r\n");
                            ec1.CurrentBloodyVengeance = 999;
                            ec1.pbBloodyVengeance.Image = Image.FromFile(Database.BaseResourceFolder + "BloodyVengeance.bmp");
                            ec1.pbBloodyVengeance.Update();
                            ec1.CurrentAetherDrive = 4;
                            ec1.pbAetherDrive.Image = Image.FromFile(Database.BaseResourceFolder + "AetherDrive.bmp");
                            ec1.pbAetherDrive.Update();
                            UpdateBattleText(ec1.Name + "に強力な力が宿った！\r\n");
                        }
                        else
                        {
                            PlayerSkillStraightSmash(ec1, target, false);
                        }
                        break;
                    case 2:
                        PlayerSkillStraightSmash(ec1, target, false);
                        break;
                }
            }
            else if (ec1.Name == "天秤を司る者")
            {
                switch(rd4.Next(1, 5))
                {
                    case 1:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        PlayerSpellWhiteOut(ec1, target, 500);
                        break;
                    case 2:
                        if (ec1.CurrentDeflection <= 0)
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }
                            ec1.Target = ec1;
                            PlayerSpellDeflection(ec1);
                        }
                        else
                        {
                            PlayerNormalAttack(ec1, target, 0, 0, false);
                        }
                        break;
                    case 3:
                        if (ec1.CurrentMirrorImage <= 0)
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }
                            ec1.Target = ec1;
                            PlayerSpellMirrorImage(ec1);
                        }
                        else
                        {
                            PlayerNormalAttack(ec1, target, 0, 0, false);
                        }
                        break;
                    case 4:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                }
            }
            else if (ec1.Name == "レイジ・イフリート")
            {
                switch (rd4.Next(1, 5))
                {
                    case 1:
                        UpdateBattleText(ec1.Name + "は業火の吐息をまきちらした！\r\n");
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            int effectValue = ec1.TotalIntelligence + rd.Next(200, 300);
                            if (!playerList[ii].Dead)
                            {
                                if (ec1.CurrentShadowPact > 0)
                                {
                                    effectValue = (int)((float)effectValue * 1.5F);
                                }
                                if (ec1.CurrentEternalPresence > 0)
                                {
                                    effectValue = (int)((float)effectValue * 1.3F);
                                }
                                if ((playerList[ii].Accessory != null && playerList[ii].Accessory.Name == "炎授天使の護符"))
                                {
                                    effectValue -= playerList[ii].Accessory.MinValue;
                                    if (effectValue <= 0) effectValue = 0;
                                }
                                if ((playerList[ii].Accessory != null && playerList[ii].Accessory.Name == "シールオブアクア＆ファイア"))
                                {
                                    effectValue = (int)((float)effectValue * (100.0F - (float)playerList[ii].Accessory.MinValue) / 100.0F);
                                }
                                if (playerList[ii].CurrentAbsorbWater > 0 && ec1.CurrentTruthVision <= 0)
                                {
                                    effectValue = (int)((float)effectValue / 1.2F);
                                }
                                if (playerList[ii].PA == PlayerAction.Defense || playerList[ii].CurrentStanceOfStanding > 0)
                                {
                                    if (playerList[ii].CurrentAbsoluteZero > 0)
                                    {
                                        UpdateBattleText(target.GetCharacterSentence(88));
                                    }
                                    else
                                    {
                                        effectValue = (int)((float)effectValue / 3.0F);
                                    }
                                }
                                if (playerList[ii].CurrentOneImmunity > 0 && (playerList[ii].PA == PlayerAction.Defense || playerList[ii].CurrentStanceOfStanding > 0))
                                {
                                    if (playerList[ii].CurrentAbsoluteZero > 0)
                                    {
                                        UpdateBattleText(target.GetCharacterSentence(88));
                                    }
                                    else
                                    {
                                        effectValue = 0;
                                    }
                                }

                                if (CheckDodge(playerList[ii])) { continue;}

                                GroundOne.PlaySoundEffect("EnemyAttack1.mp3");
                                playerList[ii].CurrentLife -= effectValue;
                                UpdateLife(playerList[ii]);
                                UpdateBattleText(playerList[ii].Name + "へ" + effectValue.ToString() + "のダメージ\r\n");
                            }
                        }
                        break;
                    case 2:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        PlayerSpellVolcanicWave(ec1, target, 500);
                        break;
                    case 3:
                        if (ec1.CurrentFlameAura <= 0)
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }

                            ec1.Target = ec1;
                            PlayerSpellFlameAura(ec1);
                        }
                        else
                        {
                            PlayerNormalAttack(ec1, target, 0, 0, false);
                        }
                        break;
                    case 4:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                }
            }
            else if (ec1.Name == "ペインエンジェル")
            {
                switch (rd4.Next(1, 5))
                {
                    case 1:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        EnemySpellHolyShock(ec1, target);
                        break;
                    case 2:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        EnemySpellFreshHeal(ec1, target);
                        break;
                    case 3:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;
                    case 4:
                        if (ec1.CurrentHighEmotionality <= 0)
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }
                            PlayerSkillHighEmotionality(ec1, ec1);
                        }
                        else
                        {
                            PlayerNormalAttack(ec1, target, 0, 0, false);
                        }
                        break;
                }
            }
            else if (ec1.Name == "ドゥームブリンガー")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                        PlayerSkillKineticSmash(ec1, target, false);
                        break;
                    case 2:
                        if (target.CurrentStunning <= 0)
                        {
                            PlayerNormalAttack(ec1, target, 0, 2, false);
                        }
                        else
                        {
                            PlayerSkillDoubleSlash(ec1, target, false);
                        }
                        break;
                    case 3:
                        PlayerSkillDoubleSlash(ec1, target, false);
                        break;
                }
            }
            else if (ec1.Name == "ハウリングホラー")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                        // 特殊能力とみなし、Negateの対象にはしない。
                        UpdateBattleText(ec1.Name + "は死霊と共に気味の悪い声で雄叫びをあげた！\r\n");
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            if (!playerList[ii].Dead)
                            {
                                playerList[ii].CurrentSilence = 999;
                                playerList[ii].pbSilence.Image = Image.FromFile(Database.BaseResourceFolder + "Silence.bmp");
                                playerList[ii].pbSilence.Update();

                                playerList[ii].CurrentPoison = 999;
                                playerList[ii].pbPoison.Image = Image.FromFile(Database.BaseResourceFolder + "Poison.bmp");
                                playerList[ii].pbPoison.Update();
                                UpdateBattleText(playerList[ii].Name + "は沈黙・猛毒にかかった！\r\n");
                            }
                        }
                        break;
                    case 2:
                        // 特殊能力とみなし、Negateの対象にはしない。
                        UpdateBattleText(ec1.Name + "は死霊と共にDarkBlastを連続詠唱した！\r\n");
                        PlayerSpellDarkBlast(ec1, target, 500);
                        PlayerSpellDarkBlast(ec1, target, 500);
                        PlayerSpellDarkBlast(ec1, target, 500);
                        break;
                    case 3:
                        if (ec1.CurrentShadowPact <= 0)
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }
                            ec1.Target = ec1;
                            PlayerSpellShadowPact(ec1);
                        }
                        else
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }
                            PlayerSpellDarkBlast(ec1, target, 500);
                        }
                        break; ;
                }
            }
            else if (ec1.Name == "カオス・ワーデン")
            {
                switch(rd4.Next(1, 4))
                {
                    case 1:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        PlayerSpellVolcanicWave(ec1, target, 500);
                        break;
                    case 2:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        PlayerSpellWordOfPower(ec1, target, 500);
                        break;
                    case 3:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        PlayerSpellWhiteOut(ec1, target, 500);
                        break;
                }
            }
            #endregion
            #region "四階の守護者：Altomo"
            else if (ec1.Name == "四階の守護者：Altomo")
            {
                switch (randomData)
                {
                    case 1:
                        if (ec1.CurrentWordOfFortune <= 0)
                        {
                            if (ec1.CurrentAbsoluteZero > 0)
                            {
                                PlayerNormalAttack(ec1, target, 0, 0, true);
                            }
                            else
                            {
                                EnemySpellWordOfFortune(ec1, ec1);
                            }
                        }
                        else
                        {
                            PlayerNormalAttack(ec1, target, 0, 0, true);
                        }
                        break;
                    case 2:
                        if (target.CurrentSilence <= 0)
                        {
                            if (ec1.CurrentAbsoluteZero > 0)
                            {
                                PlayerNormalAttack(ec1, target, 0, 0, true);
                            }
                            else
                            {
                                UpdateBattleText(ec1.Name + "：ッそこの呪文、止めてくれるわ！　サイレンスショット！！\r\n", 1000);
                                int effectValue = 0;
                                effectValue = rd.Next(ec1.TotalStrength,
                                                      ec1.TotalStrength + ec1.TotalIntelligence);
                                if (target.CurrentDeflection > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(62), 1000);
                                    target.CurrentDeflection = 0;
                                    target.pbDeflection.Image = null;
                                    target.pbDeflection.Update();
                                    UpdateBattleText(ec1.GetCharacterSentence(110));
                                }

                                effectValue -= rd.Next(target.MainArmor.MinValue, target.MainArmor.MaxValue + 1);
                                if (effectValue <= 0) effectValue = 0;
                                if (target.CurrentAbsorbWater > 0 && ec1.CurrentTruthVision <= 0)
                                {
                                    effectValue = (int)((float)effectValue / 1.2F);
                                }
                                if (target.CurrentEternalPresence > 0 && ec1.CurrentTruthVision <= 0)
                                {
                                    effectValue = (int)((float)effectValue / 1.3F);
                                }
                                if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                                {
                                    if (target.CurrentAbsoluteZero > 0)
                                    {
                                        UpdateBattleText(target.GetCharacterSentence(88));
                                    }
                                    else
                                    {
                                        UpdateBattleText(ec1.GetCharacterSentence(116));
                                        //effectValue = (int)((float)effectValue / 3.0F);
                                    }
                                }
                                if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                                {
                                    if (target.CurrentAbsoluteZero > 0)
                                    {
                                        UpdateBattleText(target.GetCharacterSentence(88));
                                    }
                                    else
                                    {
                                        UpdateBattleText(ec1.Name + "：物理攻撃無効など効くとでも思ったか！　ぬるい！！\r\n");
                                        //effectValue = 0;
                                    }
                                }

                                GroundOne.PlaySoundEffect("EnemyAttack1.mp3");

                                if (CheckDodge(target, true)) { return; }

                                if (this.difficulty == 1)
                                {
                                    target.CurrentSilence = 3; // ２ターン継続のためには、初期値は２＋１
                                }
                                else
                                {
                                    target.CurrentSilence = 4; // ３ターン継続のためには、初期値は３＋１
                                }
                                target.pbSilence.Image = Image.FromFile(Database.BaseResourceFolder + "Silence.bmp");
                                target.pbSilence.Update();
                                target.CurrentLife -= effectValue;
                                UpdateBattleText(String.Format(ec1.GetCharacterSentence(75), target.Name));
                                UpdateBattleText("  " + effectValue.ToString() + "ダメージ\r\n", 300);
                                UpdateLife(target);
                            }
                        }
                        else
                        {
                            PlayerNormalAttack(ec1, target, 0, 0, true);
                        }
                        break;
                    case 3:
                        if (ec1.CurrentAbsoluteZero > 0)
                        {
                            PlayerNormalAttack(ec1, target, 0, 0, true);
                        }
                        else
                        {
                            if (this.difficulty == 1)
                            {
                                PlayerNormalAttack(ec1, target, 0, 2, true); // Scatter Shotで１ターン気絶させる。
                            }
                            else
                            {
                                PlayerNormalAttack(ec1, target, 0, 3, true); // Scatter Shotで２ターン気絶させる。
                            }
                        }
                        break;

                    case 6:
                    case 7:
                        if (ec1.CurrentAbsoluteZero > 0)
                        {
                            PlayerNormalAttack(ec1, target, 0, 0, true);
                        }
                        else
                        {
                            UpdateBattleText(ec1.Name + "：っふん、攻撃こそ最大だ！　ブラックファイア・アロー！\r\n" + target.Name + "に・・・\r\n", 1000);
                            bool alreadyPressure = false;
                            for (int ii = 0; ii < 2; ii++)
                            {
                                int effectValue = 0;
                                effectValue = rd.Next(ec1.TotalStrength,
                                                      ec1.TotalStrength + ec1.TotalAgility);
                                if (target.CurrentDeflection > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(62), 1000);
                                    target.CurrentDeflection = 0;
                                    target.pbDeflection.Image = null;
                                    target.pbDeflection.Update();
                                    UpdateBattleText(ec1.GetCharacterSentence(110));
                                }

                                effectValue -= rd.Next(target.MainArmor.MinValue, target.MainArmor.MaxValue + 1);
                                if (effectValue <= 0) effectValue = 0;

                                if ((target.Accessory != null && target.Accessory.Name == "炎授天使の護符"))
                                {
                                    effectValue -= target.Accessory.MinValue;
                                    if (effectValue <= 0) effectValue = 0;
                                }
                                if ((target.Accessory != null && target.Accessory.Name == "シールオブアクア＆ファイア"))
                                {
                                    effectValue = (int)((float)effectValue * (100.0F - (float)target.Accessory.MinValue) / 100.0F);
                                }
                                if (target.CurrentAbsorbWater > 0 && ec1.CurrentTruthVision <= 0)
                                {
                                    effectValue = (int)((float)effectValue / 1.2F);
                                }
                                if (target.CurrentEternalPresence > 0 && ec1.CurrentTruthVision <= 0)
                                {
                                    effectValue = (int)((float)effectValue / 1.3F);
                                }
                                if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                                {
                                    if (target.CurrentAbsoluteZero > 0)
                                    {
                                        UpdateBattleText(target.GetCharacterSentence(88));
                                    }
                                    else
                                    {
                                        if (!alreadyPressure)
                                        {
                                            alreadyPressure = true;
                                            UpdateBattleText(ec1.GetCharacterSentence(116));
                                        }
                                        //effectValue = (int)((float)effectValue / 3.0F);
                                    }
                                }
                                if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                                {
                                    if (target.CurrentAbsoluteZero > 0)
                                    {
                                        UpdateBattleText(target.GetCharacterSentence(88));
                                    }
                                    else
                                    {
                                        if (!alreadyPressure)
                                        {
                                            alreadyPressure = true;
                                            UpdateBattleText(ec1.Name + "：物理攻撃無効など効くとでも思ったか！　ぬるい！！\r\n");
                                        }
                                        //effectValue = 0;
                                    }
                                }

                                if (CheckDodge(target, true)) { continue; }

                                target.CurrentLife -= effectValue;
                                UpdateLife(target);
                                UpdateBattleText("  " + effectValue.ToString() + "ダメージ", 300);
                            }
                            UpdateBattleText("\r\n", 0);
                            UpdateLife(target);
                        }
                        break;
                    case 4:
                    case 5:
                        if (ec1.CurrentTruthVision <= 0)
                        {
                            if (ec1.CurrentAbsoluteZero > 0)
                            {
                                PlayerNormalAttack(ec1, target, 0, 0, true);
                            }
                            else
                            {
                                PlayerSkillTruthVision(ec1, ec1);
                            }
                        }
                        else
                        {
                            PlayerNormalAttack(ec1, target, 0, 0, true);
                        }
                        break;
                    case 8:
                        if (ec1.CurrentAbsoluteZero > 0)
                        {
                            PlayerNormalAttack(ec1, target, 0, 0, true);
                        }
                        else
                        {
                            UpdateBattleText(ec1.Name + "：この一発に全てを込める！　キルショット！！！\r\n", 1000);
                            int effectValue1 = 0;
                            effectValue1 = rd.Next(ec1.TotalStrength,
                                                  ec1.TotalStrength + ec1.TotalAgility);
                            if (this.difficulty == 1)
                            {
                                effectValue1 = (int)((double)effectValue1 * 1.5F); // 2.2F 100%クリティカルだが、ダメージ1.5倍にとどめる
                            }
                            else
                            {
                                effectValue1 = (int)((double)effectValue1 * 3.0F); // 100%クリティカル
                            }
                            if (target.CurrentDeflection > 0)
                            {
                                UpdateBattleText(target.GetCharacterSentence(62), 1000);
                                target.CurrentDeflection = 0;
                                target.pbDeflection.Image = null;
                                target.pbDeflection.Update();
                                UpdateBattleText(ec1.GetCharacterSentence(110));
                            }

                            effectValue1 -= rd.Next(target.MainArmor.MinValue, target.MainArmor.MaxValue + 1);
                            if (effectValue1 <= 0) effectValue1 = 0;
                            if (target.CurrentAbsorbWater > 0 && ec1.CurrentTruthVision <= 0)
                            {
                                effectValue1 = (int)((float)effectValue1 / 1.2F);
                            }
                            if (target.CurrentEternalPresence > 0 && ec1.CurrentTruthVision <= 0)
                            {
                                effectValue1 = (int)((float)effectValue1 / 1.3F);
                            }
                            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    UpdateBattleText(ec1.GetCharacterSentence(116));
                                    //effectValue = (int)((float)effectValue / 3.0F);
                                }
                            }
                            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                            {
                                if (target.CurrentAbsoluteZero > 0)
                                {
                                    UpdateBattleText(target.GetCharacterSentence(88));
                                }
                                else
                                {
                                    UpdateBattleText(ec1.Name + "：物理攻撃無効など効くとでも思ったか！　ぬるい！！\r\n");
                                    //effectValue = 0;
                                }
                            }

                            if (CheckDodge(target, true)) { return; }

                            target.CurrentLife -= effectValue1;
                            UpdateLife(target);
                            UpdateBattleText("  " + effectValue1.ToString() + "ダメージ\r\n");
                        }
                        break;
                    case 9:
                    case 10:
                        PlayerNormalAttack(ec1, target, 0, 0, true);
                        break;
                }
            }
            #endregion
            #region "五階の雑魚キャラ"
            else if (ec1.Name == "Phoenix")
            {
                switch(rd4.Next(1, 4))
                {
                    case 1:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }

                        if (ec1.CurrentAbsoluteZero > 0)
                        {
                            UpdateBattleText(ec1.GetCharacterSentence(76));
                            return;
                        }

                        if (this.difficulty == 1)
                        {
                            PlayerSpellLavaAnnihilation(ec1, target, 1000, 1400);
                        }
                        else
                        {
                            PlayerSpellLavaAnnihilation(ec1, target, 1000, 0);
                        }
                        break;
                    case 2:
                        if (target.CurrentParalyze <= 0)
                        {
                            if (ec1.CurrentAbsoluteZero > 0)
                            {
                                UpdateBattleText(ec1.GetCharacterSentence(87));
                                return;
                            }

                            UpdateBattleText(ec1.Name + "は激しいカナキリ声が挙げ、大翼を羽ばたかせた！\r\n", 1000);
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                if (!playerList[ii].Dead)
                                {
                                    playerList[ii].CurrentParalyze = 2;
                                    playerList[ii].pbParalyze.Image = Image.FromFile(Database.BaseResourceFolder + "Paralyze.bmp");
                                    playerList[ii].pbParalyze.Update();
                                    UpdateBattleText(playerList[ii].Name + "は麻痺にかかった！\r\n");
                                }
                            }
                        }
                        else
                        {
                            PlayerNormalAttack(ec1, target, 0, 0, false);
                        }
                        break;
                    case 3:
                        if (ec1.CurrentAbsoluteZero > 0)
                        {
                            UpdateBattleText(ec1.GetCharacterSentence(87));
                            return;
                        }

                        PlayerSkillSoulInfinity(ec1, target, (float)(1.0F / 10.0F), false); // [コメント]：強すぎ。SoulInfinitiyは将来修正必須
                        break;
                }

            }
            else if (ec1.Name == "Nine Tail")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }

                        if (ec1.CurrentAbsoluteZero > 0)
                        {
                            UpdateBattleText(ec1.GetCharacterSentence(76));
                            return;
                        }

                        PlayerSpellDevouringPlague(ec1, target);
                        break;
                    case 2:
                        if (ec1.Target.CurrentStunning <= 0)
                        {
                            if (ec1.CurrentAbsoluteZero > 0)
                            {
                                UpdateBattleText(ec1.GetCharacterSentence(87));
                                return;
                            }

                            UpdateBattleText(ec1.Name + "は９つの尾を円環状に模様を描き、雄叫びをあげた！\r\n", 1000);
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                if (!playerList[ii].Dead)
                                {
                                    if ((playerList[ii].Accessory != null) && (playerList[ii].Accessory.Name == "鋼鉄の石像"))
                                    {
                                        Random rd3 = new Random(DateTime.Now.Millisecond * Environment.TickCount);
                                        if (rd3.Next(1, 101) <= playerList[ii].Accessory.MinValue)
                                        {
                                            UpdateBattleText(playerList[ii].Name + "が装備している鋼鉄の石像が光り輝いた！\r\n", 1000);
                                            UpdateBattleText(playerList[ii].Name + "はスタン状態に陥らなかった。\r\n");
                                        }
                                        else
                                        {
                                            if (playerList[ii].CurrentStunning <= 0)
                                            {
                                                playerList[ii].CurrentStunning = 2;
                                            }
                                            playerList[ii].pbStun.Image = Image.FromFile(Database.BaseResourceFolder + "Stunning.bmp");
                                            playerList[ii].pbStun.Update();
                                            UpdateBattleText(playerList[ii].Name + "はスタン状態になった！\r\n");
                                        }
                                    }
                                    else
                                    {
                                        if (playerList[ii].CurrentStunning <= 0)
                                        {
                                            playerList[ii].CurrentStunning = 2;
                                        }
                                        playerList[ii].pbStun.Image = Image.FromFile(Database.BaseResourceFolder + "Stunning.bmp");
                                        playerList[ii].pbStun.Update();
                                        UpdateBattleText(playerList[ii].Name + "はスタン状態になった！\r\n");
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }

                            if (ec1.CurrentAbsoluteZero > 0)
                            {
                                UpdateBattleText(ec1.GetCharacterSentence(76));
                                return;
                            }
                            PlayerSpellDevouringPlague(ec1, target);
                        }
                        break;
                    case 3:
                        if (ec1.CurrentAbsoluteZero > 0)
                        {
                            UpdateBattleText(ec1.GetCharacterSentence(87));
                            return;
                        }

                        PlayerSkillOboroImpact(ec1, target, 0.4F, false);
                        break;
                }
            }
            else if (ec1.Name == "Emerard Dragon")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                        UpdateBattleText(ec1.Name + "は急降下と共に激しく突進してきた！\r\n");
                        PlayerNormalAttack(ec1, target, 1.5F, 0, false);
                        PlayerNormalAttack(ec1, target, 1.5F, 0, false);
                        break;
                    case 2:
                        if (ec1.Target.CurrentFrozen <= 0)
                        {
                            if (ec1.CurrentAbsoluteZero > 0)
                            {
                                UpdateBattleText(ec1.GetCharacterSentence(87));
                                return;
                            }

                            UpdateBattleText(ec1.Name + "はエメラルドの息吹を吐いてきた！\r\n", 1000);
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                if (!playerList[ii].Dead)
                                {
                                    playerList[ii].CurrentFrozen = 2;
                                    playerList[ii].pbFrozen.Image = Image.FromFile(Database.BaseResourceFolder + "Frozen.bmp");
                                    playerList[ii].pbFrozen.Update();
                                    UpdateBattleText(playerList[ii].Name + "は凍結状態になった！\r\n");
                                }
                            }
                        }
                        else
                        {
                            UpdateBattleText(ec1.Name + "は急降下と共に激しく突進してきた！\r\n");
                            PlayerNormalAttack(ec1, target, 1.5F, 0, false);
                            PlayerNormalAttack(ec1, target, 1.5F, 0, false);
                        }
                        break;
                    case 3:
                        if (ec1.Target.CurrentAbsoluteZero <= 0)
                        {
                            if (ec1.CurrentAbsoluteZero > 0)
                            {
                                UpdateBattleText(ec1.GetCharacterSentence(87));
                                return;
                            }

                            UpdateBattleText(ec1.Name + "は凍りつく息吹を吐いてきた！\r\n", 1000);
                            // Negateによるスペル詠唱キャンセル
                            for (int ii = 0; ii < playerList.Length; ii++)
                            {
                                // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                                {
                                    if (ec1.CurrentNothingOfNothingness > 0)
                                    {
                                        UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                        //playerList[ii].CurrentStanceOfEyes = false;
                                        playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                        //playerList[ii].CurrentNegate = false;
                                        playerList[ii].RemoveNegate(); // 後編編集
                                        //playerList[ii].CurrentCounterAttack = false;
                                        playerList[ii].RemoveCounterAttack(); // 後編編集
                                    }
                                    else
                                    {
                                        UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                        return;
                                    }
                                }
                            }
                            PlayerSpellAbsoluteZero(ec1, target, 500);
                        }
                        else
                        {
                            UpdateBattleText(ec1.Name + "は急降下と共に激しく突進してきた！\r\n");
                            PlayerNormalAttack(ec1, target, 1.5F, 0, false);
                            PlayerNormalAttack(ec1, target, 1.5F, 0, false);
                        }
                        break;
                }
            }
            else if (ec1.Name == "Judgement")
            {
                switch (rd4.Next(1, 4))
                {
                    case 1:
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }

                        if (ec1.CurrentAbsoluteZero > 0)
                        {
                            UpdateBattleText(ec1.GetCharacterSentence(76));
                            return;
                        }

                        if (ec1.CurrentLife < ec1.MaxLife / 2)
                        {
                            ec1.Target = ec1;
                        }
                        else
                        {
                            ec1.Target = target;
                        }
                        PlayerSpellCelestialNova(ec1);
                        break;
                    case 2:
                        if (ec1.CurrentProtection <= 0)
                        {
                            // 特殊能力とみなし、Negateの対象にはしない。ただしスキル扱いとする。
                            if (ec1.CurrentAbsoluteZero > 0)
                            {
                                UpdateBattleText(ec1.GetCharacterSentence(87));
                                return;
                            }

                            UpdateBattleText(ec1.Name + "は神々しい天の光に包まれた！\r\n", 1000);
                            ec1.Target = ec1;
                            PlayerSpellProtection(ec1);
                            PlayerSpellSaintPower(ec1);
                            PlayerSpellGlory(ec1);
                        }
                        else
                        {
                            if (ec1.CurrentAbsoluteZero > 0)
                            {
                                UpdateBattleText(ec1.GetCharacterSentence(87));
                                return;
                            }

                            PlayerSkillDoubleSlash(ec1, target, false);
                        }
                        break;
                    case 3:
                        if (ec1.CurrentAbsoluteZero > 0)
                        {
                            UpdateBattleText(ec1.GetCharacterSentence(87));
                            return;
                        }

                        PlayerSkillDoubleSlash(ec1, target, false);
                        break;
                }
            }
            #endregion
            #region "五階の守護者：Bystander"
            else if (ec1.Name == "五階の守護者：Bystander")
            {
                // Negateによるスペル詠唱キャンセル
                for (int ii = 0; ii < playerList.Length; ii++)
                {
                    // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                    if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                    {
                        if (ec1.CurrentNothingOfNothingness > 0)
                        {
                            UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                            //playerList[ii].CurrentStanceOfEyes = false;
                            playerList[ii].RemoveStanceOfEyes(); // 後編編集
                            //playerList[ii].CurrentNegate = false;
                            playerList[ii].RemoveNegate(); // 後編編集
                            //playerList[ii].CurrentCounterAttack = false;
                            playerList[ii].RemoveCounterAttack(); // 後編編集
                        }
                        else
                        {
                            UpdateBattleText(playerList[ii].GetCharacterSentence(105));
                            //playerList[ii].CurrentStanceOfEyes = false;
                            playerList[ii].RemoveStanceOfEyes(); // 後編編集
                            //playerList[ii].CurrentNegate = false;
                            playerList[ii].RemoveNegate(); // 後編編集
                            //playerList[ii].CurrentCounterAttack = false;
                            playerList[ii].RemoveCounterAttack(); // 後編編集
                        }
                    }
                }

                // MirrorImage、Deflectionに対してカウンタートリガー的に割り込みをいれてください。
                if (this.ActivateTimeStop > 0)
                {
                    switch (this.ActivateTimeStop)
                    {
                        case 10: // 魔法DarkBlast
                            UpdateBattleText(ec1.Name + "：『闇魔法』  －　『DarkBlast』\r\n");
                            break;
                        case 9: // 魔法FireBall
                            UpdateBattleText(ec1.Name + "：『火魔法』  －　『FireBall』\r\n");
                            break;
                        case 8: // 魔法IceNeedle
                            UpdateBattleText(ec1.Name + "：『水魔法』  －　『IceNeedle』\r\n");
                            break;
                        case 7: // 魔法HolyShock
                            UpdateBattleText(ec1.Name + "：『聖魔法』  －　『HolyShock』\r\n");
                            break;
                        case 6: // 魔法FlameStrike
                            UpdateBattleText(ec1.Name + "：『火魔法』  －　『FlameStrike』\r\n");
                            break;
                        case 5: // 魔法FrozenLance
                            UpdateBattleText(ec1.Name + "：『水魔法』  －　『FrozenLance』\r\n");
                            break;
                        case 4: // 魔法VocanicWave
                            UpdateBattleText(ec1.Name + "：『火魔法』  －　『VocanicWave』\r\n");
                            break;
                        case 3: // 魔法WhiteOut
                            UpdateBattleText(ec1.Name + "：『空魔法』  －　『WhiteOut』\r\n");
                            break;
                        case 2: // 魔法LavaAnnihilation
                            UpdateBattleText(ec1.Name + "：『火魔法』  －　『LavaAnnihilation』\r\n");
                            break;
                        case 1: // 魔法WordOfPower
                            UpdateBattleText(ec1.Name + "：『理魔法』  －　『WordOfPower』\r\n");
                            break;
                    }
                }
                else if (this.ActivateTrcky > 0)
                {
                    // Negateによるスペル詠唱キャンセル
                    for (int ii = 0; ii < playerList.Length; ii++)
                    {
                        // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                        if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                        {
                            if (ec1.CurrentNothingOfNothingness > 0)
                            {
                                UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                //playerList[ii].CurrentStanceOfEyes = false;
                                playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                //playerList[ii].CurrentNegate = false;
                                playerList[ii].RemoveNegate(); // 後編編集
                                //playerList[ii].CurrentCounterAttack = false;
                                playerList[ii].RemoveCounterAttack(); // 後編編集
                            }
                            else
                            {
                                UpdateBattleText(playerList[ii].GetCharacterSentence(105));
                                //playerList[ii].CurrentStanceOfEyes = false;
                                playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                //playerList[ii].CurrentNegate = false;
                                playerList[ii].RemoveNegate(); // 後編編集
                                //playerList[ii].CurrentCounterAttack = false;
                                playerList[ii].RemoveCounterAttack(); // 後編編集
                            }
                        }
                    }

                    // [警告] ターゲットがMainCharacter.Targetなのか、単なるTargetなのか混同されてきています。統一化を図って下さい。
                    switch (this.ActivateTrcky)
                    {
                        case 5: // 魔法Tranquility
                            PlayerSpellTranquility(ec1, ec1.Target, 200);
                            break;
                        case 4: // 魔法DispelMagic
                            EnemySpellDispelMagic(ec1, ec1.Target, 200);
                            break;
                        case 3: // 魔法Damnation
                            EnemySpellDamnation(ec1, ec1.Target, 200);
                            break;
                        case 2: // 魔法BlackContract
                            PlayerSpellBlackContract(ec1, ec1.Target, true);
                            break;
                        case 1: // 魔法AbsoluteZero
                            PlayerSpellAbsoluteZero(ec1, ec1.Target, 200);
                            break;
                    }
                }
                else if (this.ActivateGrowUp > 0)
                {
                    // Negateによるスペル詠唱キャンセル
                    for (int ii = 0; ii < playerList.Length; ii++)
                    {
                        // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                        if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                        {
                            if (ec1.CurrentNothingOfNothingness > 0)
                            {
                                UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                //playerList[ii].CurrentStanceOfEyes = false;
                                playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                //playerList[ii].CurrentNegate = false;
                                playerList[ii].RemoveNegate(); // 後編編集
                                //playerList[ii].CurrentCounterAttack = false;
                                playerList[ii].RemoveCounterAttack(); // 後編編集
                            }
                            else
                            {
                                UpdateBattleText(playerList[ii].GetCharacterSentence(105));
                                //playerList[ii].CurrentStanceOfEyes = false;
                                playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                //playerList[ii].CurrentNegate = false;
                                playerList[ii].RemoveNegate(); // 後編編集
                                //playerList[ii].CurrentCounterAttack = false;
                                playerList[ii].RemoveCounterAttack(); // 後編編集
                            }
                        }
                    }

                    ec1.Target = ec1;
                    switch (this.ActivateGrowUp)
                    {
                        case 9: // 魔法Protection
                            PlayerSpellProtection(ec1);
                            break;
                        case 8: // 魔法SaintPower
                            PlayerSpellSaintPower(ec1);
                            break;
                        case 7: // 魔法ShadowPact
                            PlayerSpellShadowPact(ec1);
                            break;
                        case 6: // 魔法AbsorbWater
                            PlayerSpellAbsorbWater(ec1);
                            break;
                        case 5: // 魔法BloodyVengeance
                            PlayerSpellBloodyVengeance(ec1);
                            break;
                        case 4: // 魔法HeatBoost
                            PlayerSpellHeatBoost(ec1);
                            break;
                        case 3: // 魔法RiseOfImage
                            PlayerSpellRiseOfImage(ec1);
                            break;
                        case 2: // 魔法PromisedKnowledge
                            PlayerSpellPromisedKnowledge(ec1);
                            break;
                        case 1: // 魔法EternalPresence
                            PlayerSpellEternalPresence(ec1);
                            break;
                    }
                }
                else if (this.ActivateSword > 0)
                {
                    // Negateによるスペル詠唱キャンセル
                    for (int ii = 0; ii < playerList.Length; ii++)
                    {
                        // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                        if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                        {
                            if (ec1.CurrentNothingOfNothingness > 0)
                            {
                                UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                //playerList[ii].CurrentStanceOfEyes = false;
                                playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                //playerList[ii].CurrentNegate = false;
                                playerList[ii].RemoveNegate(); // 後編編集
                                //playerList[ii].CurrentCounterAttack = false;
                                playerList[ii].RemoveCounterAttack(); // 後編編集
                            }
                            else
                            {
                                UpdateBattleText(playerList[ii].GetCharacterSentence(105));
                                //playerList[ii].CurrentStanceOfEyes = false;
                                playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                //playerList[ii].CurrentNegate = false;
                                playerList[ii].RemoveNegate(); // 後編編集
                                //playerList[ii].CurrentCounterAttack = false;
                                playerList[ii].RemoveCounterAttack(); // 後編編集
                            }
                        }
                    }

                    switch (this.ActivateSword)
                    {
                        case 4: // 魔法ImmortalRave
                            PlayerSpellImmortalRave(ec1);
                            break;
                        case 3: // 魔法AetherDrive
                            PlayerSpellAetherDrive(ec1);
                            break;
                        case 2: // 魔法FlameAura
                            MainCharacter temp = ec1.Target;
                            ec1.Target = ec1;
                            PlayerSpellFlameAura(ec1);
                            ec1.Target = temp;
                            break;
                        case 1: // 魔法GaleWind
                            if (ec1.CurrentGaleWind <= 0)
                            {
                                EnemySpellGaleWind(ec1, 1); // [警告]：これはBystander用のGaleWindです。一般化するためには要改版です。
                            }
                            break;
                    }
                }
                else if (this.ActivateEverGreen > 0)
                {
                    // Negateによるスペル詠唱キャンセル
                    for (int ii = 0; ii < playerList.Length; ii++)
                    {
                        // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                        if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                        {
                            if (ec1.CurrentNothingOfNothingness > 0)
                            {
                                UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                //playerList[ii].CurrentStanceOfEyes = false;
                                playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                //playerList[ii].CurrentNegate = false;
                                playerList[ii].RemoveNegate(); // 後編編集
                                //playerList[ii].CurrentCounterAttack = false;
                                playerList[ii].RemoveCounterAttack(); // 後編編集
                            }
                            else
                            {
                                UpdateBattleText(playerList[ii].GetCharacterSentence(105));
                                //playerList[ii].CurrentStanceOfEyes = false;
                                playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                //playerList[ii].CurrentNegate = false;
                                playerList[ii].RemoveNegate(); // 後編編集
                                //playerList[ii].CurrentCounterAttack = false;
                                playerList[ii].RemoveCounterAttack(); // 後編編集
                            }
                        }
                    }

                    ec1.Target = ec1;
                    switch (this.ActivateEverGreen)
                    {
                        case 3: // 魔法WordOfLife
                            PlayerSpellWordOfLife(ec1);
                            break;
                        case 2: // 魔法FreshHeal
                            EnemySpellFreshHeal(ec1, ec1);
                            break;
                        case 1: // 魔法CelestialNova
                            PlayerSpellCelestialNova(ec1);
                            break;
                    }
                }
                else if (this.ActivateTotalEndTime > 0)
                {
                    switch (this.ActivateTotalEndTime)
                    {
                        case 9: // 魔法ShadowPact
                            MainCharacter temp = ec1.Target;
                            ec1.Target = ec1;
                            PlayerSpellShadowPact(ec1);
                            ec1.Target = temp;
                            break;
                        case 8: // 魔法PromisedKnowledge
                            temp = ec1.Target;
                            ec1.Target = ec1;
                            PlayerSpellPromisedKnowledge(ec1);
                            ec1.Target = temp;
                            break;
                        case 7: // 魔法EternalPresence
                            temp = ec1.Target;
                            ec1.Target = ec1;
                            PlayerSpellEternalPresence(ec1);
                            ec1.Target = temp;
                            break;
                        case 6: // 魔法MirrorImage
                            temp = ec1.Target;
                            ec1.Target = ec1;
                            PlayerSpellMirrorImage(ec1);
                            ec1.Target = temp;
                            break;
                        case 5: // 魔法Deflection
                            temp = ec1.Target;
                            ec1.Target = ec1;
                            PlayerSpellDeflection(ec1);
                            ec1.Target = temp;
                            break;
                        case 4: // 魔法DispelMagic
                            for (int ii = 0; ii < this.playerList.Length; ii++)
                            {
                                if (!playerList[ii].Dead)
                                {
                                    temp = ec1.Target;
                                    ec1.Target = playerList[ii];
                                    EnemySpellDispelMagic(ec1, playerList[ii], 200);
                                    ec1.Target = temp;
                                }
                            }
                            break;
                        case 3: // 魔法Damnation
                            for (int ii = 0; ii < this.playerList.Length; ii++)
                            {
                                if (!playerList[ii].Dead)
                                {
                                    temp = ec1.Target;
                                    ec1.Target = playerList[ii];
                                    EnemySpellDamnation(ec1, playerList[ii], 200);
                                    ec1.Target = temp;
                                }
                            }
                            break;
                        case 2: // 魔法AbsoluteZero
                            for (int ii = 0; ii < this.playerList.Length; ii++)
                            {
                                if (!playerList[ii].Dead)
                                {
                                    temp = ec1.Target;
                                    ec1.Target = playerList[ii];
                                    PlayerSpellAbsoluteZero(ec1, playerList[ii], 200);
                                    ec1.Target = temp;
                                }
                            }
                            break;
                        case 1: // 魔法LavaAnnihilation
                            UpdateBattleText(ec1.Name + "：『火魔法』  －　『LavaAnnihilation』\r\n");
                            break;
                    }
                }
                else
                {
                    UpdateBattleText(ec1.Name + "は微動だにせず、椅子に座っている。 \r\n");
                }
            }
            #endregion
            #region "ヴェルゼ・アーティ"
            // [警告]：微調整でBattleEnemySupport2のSetupEnemyActionで随分強くなりました。原罪：Verze Artieを実装時は究極のロジックを目指してください。
            else if (ec1.Name == "ヴェルゼ・アーティ")
            {
                this.bossAlreadyActionNum = randomData;
                switch (randomData)
                {
                    case 1:
                        PlayerNormalAttack(ec1, target, 0, 0, false);
                        break;

                    case 2:
                        if (ec1.CurrentSkillPoint < Database.COUNTER_ATTACK_COST)
                        {
                            UpdateBattleText(ec1.GetCharacterSentence(0));
                            return;
                        }
                        ec1.CurrentSkillPoint -= Database.COUNTER_ATTACK_COST;
                        UpdateBattleText(ec1.Name + "はカウンターの構えをとっている。\r\n");
                        break;

                    case 3:
                        if (ec1.CurrentSkillPoint < Database.STANCE_OF_STANDING_COST)
                        {
                            UpdateBattleText(ec1.GetCharacterSentence(0));
                            return;
                        }
                        ec1.CurrentSkillPoint -= Database.STANCE_OF_STANDING_COST;
                        PlayerSkillStanceOfStanding(ec1, target);
                        break;

                    case 4:
                        if (ec1.CurrentSkillPoint < Database.DOUBLE_SLASH_COST)
                        {
                            UpdateBattleText(ec1.GetCharacterSentence(0));
                            return;
                        }
                        ec1.CurrentSkillPoint -= Database.DOUBLE_SLASH_COST;
                        EnemySkillDoubleSlash(ec1, target, false);
                        break;

                    case 5:
                        if (ec1.CurrentSkillPoint < Database.CRUSHING_BLOW_COST)
                        {
                            UpdateBattleText(ec1.GetCharacterSentence(0));
                            return;
                        }
                        ec1.CurrentSkillPoint -= Database.CRUSHING_BLOW_COST;
                        CurrentCrushingBlowEnemy = 2; // １ターン継続のためには、初期値は１＋１
                        PlayerNormalAttack(ec1, target, 0, 2, false);
                        break;

                    case 6:
                        if (ec1.CurrentMana < Database.FRESH_HEAL_COST)
                        {
                            UpdateBattleText(ec1.GetCharacterSentence(0));
                            return;
                        }
                        ec1.CurrentMana -= Database.FRESH_HEAL_COST;
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        EnemySpellFreshHeal(ec1, ec1);
                        break;

                    case 7:
                        if (ec1.CurrentMana < Database.ONE_IMMUNITY_COST)
                        {
                            UpdateBattleText(ec1.GetCharacterSentence(0));
                            return;
                        }
                        ec1.CurrentMana -= Database.ONE_IMMUNITY_COST;
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentParalyze <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        EnemySpellOneImmunity(ec1);
                        break;

                    case 8:
                        if (ec1.CurrentSkillPoint < Database.CARNAGE_RUSH_COST)
                        {
                            UpdateBattleText(ec1.GetCharacterSentence(0));
                            return;
                        }
                        ec1.CurrentSkillPoint -= Database.CARNAGE_RUSH_COST;
                        PlayerSkillCarnageRush(ec1, target, false);
                        break;

                    case 9:
                        if (ec1.CurrentMana < Database.GALE_WIND_COST)
                        {
                            UpdateBattleText(ec1.GetCharacterSentence(0));
                            return;
                        }
                        ec1.CurrentMana -= Database.GALE_WIND_COST;
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        EnemySpellGaleWind(ec1);
                        break;

                    case 10:
                        if (ec1.CurrentMana < Database.DISPEL_MAGIC_COST)
                        {
                            UpdateBattleText(ec1.GetCharacterSentence(0));
                            return;
                        }
                        ec1.CurrentMana -= Database.DISPEL_MAGIC_COST;
                        // Negateによるスペル詠唱キャンセル
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            // [警告]：Negateが使えないと思われる状態は増える可能性があります。随時確認してください。
                            if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentTemptation <= 0 && playerList[ii].CurrentNegate > 0)
                            {
                                if (ec1.CurrentNothingOfNothingness > 0)
                                {
                                    UpdateBattleText("しかし、" + ec1.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                                    //playerList[ii].CurrentStanceOfEyes = false;
                                    playerList[ii].RemoveStanceOfEyes(); // 後編編集
                                    //playerList[ii].CurrentNegate = false;
                                    playerList[ii].RemoveNegate(); // 後編編集
                                    //playerList[ii].CurrentCounterAttack = false;
                                    playerList[ii].RemoveCounterAttack(); // 後編編集
                                }
                                else
                                {
                                    UpdateBattleText(playerList[ii].GetCharacterSentence(104));
                                    return;
                                }
                            }
                        }
                        EnemySpellDispelMagic(ec1, target, 1000);
                        break;
                }
                UpdateMCMana(ec1);
                UpdateMCSkillPoint(ec1);
            }
            #endregion
            #region "ダミー素振り君"
            else if (ec1.Name == "ダミー素振り君")
            {
                switch (randomData)
                {
                    case 1:
                        PlayerNormalAttack(ec1, target, 0, 3, false);
                        break;
                    case 8:
                        ec1.Target = ec1;
                        PlayerSpellFlameAura(ec1);
                        PlayerSpellGlory(ec1);
                        if (ec1.CurrentImmortalRave <= 0)
                        {
                            PlayerSpellImmortalRave(ec1);
                        }
                        ec1.Target = target;
                        PlayerSkillStraightSmash(ec1, target, false);
                        break;
                    case 7:
                        UpdateBattleText(ec1.Name + "は防御の姿勢をとっている。\r\n");
                        ec1.PA = PlayerAction.Defense;// [警告]本来Defenseを行動に移すための記述だが、このフラグが選択された時点で効果が発揮されてしまっている。
                        break;
                    case 6:
                        ec1.Target = ec1;
                        PlayerSpellWordOfLife(ec1);
                        break;
                    case 5:
                        ec1.Target = ec1;
                        PlayerSpellFreshHeal(ec1);
                        break;
                    case 4:
                        PlayerSpellGaleWind(ec1);
                        break;
                    case 3:
                        PlayerSkillDoubleSlash(ec1, target, false);
                        break;
                    case 2:
                        PlayerSkillCarnageRush(ec1, target, false);
                        break;
                    default:
                        UpdateBattleText(ec1.Name + "はボーっと突っ立っている。\r\n");
                        break;
                }
            }
            #endregion
            else
            {
                PlayerNormalAttack(ec1, target, 0, 0, false);
            }
        }

        #region "魔法詠唱"
        private void PlayerSpellLifeTap(MainCharacter player)
        {
            Random rd = new Random(DateTime.Now.Millisecond);
            int lifeGain = player.TotalIntelligence * 4 + rd.Next(1, 10);

            if (player.Target != null)
            {
                if (player.Target.Dead)
                {
                    UpdateBattleText("しかし" + player.Target.Name + "は死んでしまっているため効果が無かった！\r\n");
                }
                else if (player.Target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(player.Target.GetCharacterSentence(119));
                    //UpdateBattleText("しかし" + player.Target.Name + "は絶対零度効果によりライフ回復できない！\r\n");
                }
                else
                {
                    GroundOne.PlaySoundEffect("LifeTap.mp3");
                    player.Target.CurrentLife += lifeGain;
                    UpdateLife(player.Target);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(9), lifeGain.ToString()));
                }
            }
            else
            {
                UpdateBattleText(player.GetCharacterSentence(53));
            }
        }

        private void PlayerSpellFreshHeal(MainCharacter player)
        {
            Random rd = new Random(DateTime.Now.Millisecond);
            int lifeGain = player.TotalIntelligence * 4 + rd.Next(1, 20);

            if (player.Target != null)
            {
                if ( (player.Target != ec1) || 
                     (player == ec1 && player.Target == ec1))
                {
                    if (player.Target.Dead)
                    {
                        UpdateBattleText("しかし" + player.Target.Name + "は死んでしまっているため効果が無かった！\r\n");
                    }
                    else if (player.Target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(player.Target.GetCharacterSentence(119));
                    }
                    else
                    {
                        GroundOne.PlaySoundEffect("FreshHeal.mp3");
                        player.Target.CurrentLife += lifeGain;
                        UpdateLife(player.Target);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(9), lifeGain.ToString()));
                    }
                }
                else
                {
                    UpdateBattleText(player.GetCharacterSentence(53));
                }
            }
            else
            {
                if (player.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(player.GetCharacterSentence(119));
                }
                else
                {
                    GroundOne.PlaySoundEffect("FreshHeal.mp3");
                    player.CurrentLife += lifeGain;
                    UpdateLife(player);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(9), lifeGain.ToString()));
                }
            }
        }

        // [情報]：全てのＢＵＦＦ＿ＵＰ魔法は、ここへ集約されるようにしてください。
        // [警告]：ここに集約されている情報は味方プレイヤーのみを対象としています。敵味方区別無くいけるようにしてください。
        private void PlayerBuffAbstract(MainCharacter player, int effectTime, string spellName)
        {
            if (player.Target != null)
            {
                if (   (player.Target != ec1)
                    || (player.Target == ec1 && player == ec1) )
                {
                    int effectValue = 0;
                    switch (spellName)
                    {
                        case "Protection.bmp":
                            player.Target.CurrentProtection = effectTime;
                            player.Target.pbProtection.Image = Image.FromFile(Database.BaseResourceFolder + spellName);
                            player.Target.pbProtection.Update();
                            UpdateBattleText(player.GetCharacterSentence(18));
                            break;
                        case "AbsorbWater.bmp":
                            player.Target.CurrentAbsorbWater = effectTime;
                            player.Target.pbAbsorbWater.Image = Image.FromFile(Database.BaseResourceFolder + spellName);
                            player.Target.pbAbsorbWater.Update();
                            UpdateBattleText(player.GetCharacterSentence(19));
                            break;
                        case "SaintPower.bmp":
                            player.Target.CurrentSaintPower = effectTime;
                            player.Target.pbSaintPower.Image = Image.FromFile(Database.BaseResourceFolder + spellName);
                            player.Target.pbSaintPower.Update();
                            UpdateBattleText(player.GetCharacterSentence(20));
                            break;
                        case "ShadowPact.bmp": // [警告]：ShadowPactは魔法攻撃ＵＰであり、知力ＵＰではない。仕様を抑えてください。
                            //effectValue = (int)(((float)player.Intelligence + (float)player.Intelligence) * (float)player.Intelligence / 50.0F);
                            //player.Target.BuffIntelligence += effectValue;
                            player.Target.CurrentShadowPact = effectTime;
                            player.Target.pbShadowPact.Image = Image.FromFile(Database.BaseResourceFolder + spellName);
                            player.Target.pbShadowPact.Update();
                            UpdateBattleText(player.GetCharacterSentence(21));
                            break;
                            
                        case "BloodyVengeance.bmp":
                            effectValue = player.StandardIntelligence / 2;
                            if ((effectValue - player.Target.BuffStrength_BloodyVengeance) > 0)
                            {
                                player.Target.CurrentBloodyVengeance = effectTime;
                                player.Target.pbBloodyVengeance.Image = Image.FromFile(Database.BaseResourceFolder + spellName);
                                player.Target.pbBloodyVengeance.Update();
                                UpdateBattleText(String.Format(player.GetCharacterSentence(22), Convert.ToString(effectValue - player.Target.BuffStrength_BloodyVengeance)));
                                player.Target.BuffStrength_BloodyVengeance += effectValue;
                            }
                            else
                            {
                                UpdateBattleText(player.GetCharacterSentence(82));
                            }
                            break;

                        case "HeatBoost.bmp":
                            effectValue = player.StandardIntelligence / 2;
                            if ((effectValue - player.Target.BuffAgility_HeatBoost) > 0)
                            {
                                player.Target.CurrentHeatBoost = effectTime;
                                player.Target.pbHeatBoost.Image = Image.FromFile(Database.BaseResourceFolder + spellName);
                                player.Target.pbHeatBoost.Update();
                                UpdateBattleText(String.Format(player.GetCharacterSentence(38), Convert.ToString(effectValue - player.Target.BuffAgility_HeatBoost)));
                                player.Target.BuffAgility_HeatBoost += effectValue;
                            }
                            else
                            {
                                UpdateBattleText(player.GetCharacterSentence(82));
                            }
                            break;

                        case "PromisedKnowledge.bmp":
                            effectValue = player.StandardIntelligence / 2;
                            if ((effectValue - player.Target.BuffIntelligence_PromisedKnowledge) > 0)
                            {
                                player.Target.CurrentPromisedKnowledge = effectTime;
                                player.Target.pbPromisedKnowledge.Image = Image.FromFile(Database.BaseResourceFolder + spellName);
                                player.Target.pbPromisedKnowledge.Update();
                                UpdateBattleText(String.Format(player.GetCharacterSentence(83), Convert.ToString(effectValue - player.Target.BuffIntelligence_PromisedKnowledge)));
                                player.Target.BuffIntelligence_PromisedKnowledge += effectValue;
                            }
                            else
                            {
                                UpdateBattleText(player.GetCharacterSentence(82));
                            }
                            break;

                        case "RiseOfImage.bmp":
                            effectValue = player.StandardIntelligence / 2;
                            if ((effectValue - player.Target.BuffMind_RiseOfImage) > 0)
                            {
                                player.Target.CurrentRiseOfImage = effectTime;
                                player.Target.pbRiseOfImage.Image = Image.FromFile(Database.BaseResourceFolder + spellName);
                                player.Target.pbRiseOfImage.Update();
                                UpdateBattleText(String.Format(player.GetCharacterSentence(49), Convert.ToString(effectValue - player.Target.BuffMind_RiseOfImage)));
                                player.Target.BuffMind_RiseOfImage += effectValue;
                            }
                            else
                            {
                                UpdateBattleText(player.GetCharacterSentence(82));
                            }
                            break;

                        case "FlameAura.bmp":
                            player.Target.CurrentFlameAura = effectTime;
                            player.Target.pbFlameAura.Image = Image.FromFile(Database.BaseResourceFolder + spellName);
                            player.Target.pbFlameAura.Update();
                            UpdateBattleText(player.GetCharacterSentence(36));
                            break;


                        case "WordOfLife.bmp":
                            player.Target.CurrentWordOfLife = effectTime;
                            player.Target.pbWordOfLife.Image = Image.FromFile(Database.BaseResourceFolder + spellName);
                            player.Target.pbWordOfLife.Update();
                            UpdateBattleText(player.GetCharacterSentence(41));
                            break;

                        case "WordOfFortune.bmp":
                            player.Target.CurrentWordOfFortune = effectTime;
                            player.Target.pbWordOfFortune.Image = Image.FromFile(Database.BaseResourceFolder + spellName);
                            player.Target.pbWordOfFortune.Update();
                            UpdateBattleText(player.GetCharacterSentence(42));
                            break;

                        case "EternalPresence.bmp":
                            player.Target.CurrentEternalPresence = effectTime;
                            player.Target.pbEternalPresence.Image = Image.FromFile(Database.BaseResourceFolder + spellName);
                            player.Target.pbEternalPresence.Update();
                            UpdateBattleText(player.GetCharacterSentence(44), 1000);
                            UpdateBattleText(player.GetCharacterSentence(45));
                            break;

                        case "MirrorImage.bmp":
                            player.Target.CurrentMirrorImage = effectTime;
                            player.Target.pbMirrorImage.Image = Image.FromFile(Database.BaseResourceFolder + spellName);
                            player.Target.pbMirrorImage.Update();
                            UpdateBattleText(String.Format(player.GetCharacterSentence(57), player.Target.Name));
                            break;

                        case "Deflection.bmp":
                            player.Target.CurrentDeflection = effectTime;
                            player.Target.pbDeflection.Image = Image.FromFile(Database.BaseResourceFolder + spellName);
                            player.Target.pbDeflection.Update();
                            UpdateBattleText(String.Format(player.GetCharacterSentence(60), player.Target.Name));
                            break;

                        //case "Damnation.bmp":
                        //    player.Target.CurrentDamnation = effectTime;
                        //    player.Target.pbDamnation.Image = Image.FromFile(Database.BaseResourceFolder + spellName);
                        //    player.Target.pbDamnation.Update();
                        //    UpdateBattleText(player.GetCharacterSentence(37));
                        //    break;

                        //case "PainfulInsanity.bmp":
                        //    player.Target.CurrentPainfulInsanity = effectTime;
                        //    player.Target.pbPainfulInsanity.Image = Image.FromFile(Database.BaseResourceFolder + spellName);
                        //    player.Target.pbPainfulInsanity.Update();
                        //    UpdateBattleText(player.GetCharacterSentence(4));
                        //    break;

                        case "TruthVision.bmp":
                            player.Target.CurrentTruthVision = effectTime;
                            player.Target.pbTruthVision.Image = Image.FromFile(Database.BaseResourceFolder + spellName);
                            player.Target.pbTruthVision.Update();
                            UpdateBattleText(player.GetCharacterSentence(63));
                            break;

                        case "HighEmotionality.bmp":
                            if (player.CurrentHighEmotionality <= 0)
                            {
                                player.BuffStrength_HighEmotionality = player.Strength / 3;
                                player.BuffAgility_HighEmotionality = player.Agility / 3;
                                player.BuffIntelligence_HighEmotionality = player.Intelligence / 3;
                                //player.BuffStamina_HighEmotionality = player.Stamina / 3;
                                player.BuffMind_HighEmotionality = player.Mind / 3;

                                player.CurrentHighEmotionality = effectTime;
                                player.pbHighEmotionality.Image = Image.FromFile(Database.BaseResourceFolder + spellName);
                                player.pbHighEmotionality.Update();
                                UpdateBattleText(player.GetCharacterSentence(85), 1000);
                                UpdateBattleText(player.GetCharacterSentence(86));
                            }
                            else
                            {
                                UpdateBattleText(player.GetCharacterSentence(82));
                            }
                            break;

                        case "AntiStun.bmp":
                            if (player.CurrentAntiStun <= 0)
                            {
                                player.CurrentAntiStun = effectTime;
                                player.pbAntiStun.Image = Image.FromFile(Database.BaseResourceFolder + spellName);
                                player.pbAntiStun.Update();
                                UpdateBattleText(player.GetCharacterSentence(93));
                            }
                            else
                            {
                                UpdateBattleText(player.GetCharacterSentence(92));
                            }
                            break;

                        case "StanceOfDeath.bmp":
                            if (player.CurrentStanceOfDeath <= 0)
                            {
                                player.CurrentStanceOfDeath = effectTime;
                                player.pbStanceOfDeath.Image = Image.FromFile(Database.BaseResourceFolder + spellName);
                                player.pbStanceOfDeath.Update();
                                UpdateBattleText(player.GetCharacterSentence(95));
                            }
                            else
                            {
                                UpdateBattleText(player.GetCharacterSentence(92));
                            }
                            break;

                        case "NothingOfNothingness.bmp":
                            if (player.CurrentNothingOfNothingness <= 0)
                            {
                                player.CurrentNothingOfNothingness = effectTime;
                                player.pbNothingOfNothingness.Image = Image.FromFile(Database.BaseResourceFolder + spellName);
                                player.pbNothingOfNothingness.Update();
                                UpdateBattleText(player.GetCharacterSentence(106), 1000);
                                UpdateBattleText(player.GetCharacterSentence(107));
                            }
                            else
                            {
                                UpdateBattleText(player.GetCharacterSentence(92));
                            }
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    UpdateBattleText(player.GetCharacterSentence(53));
                }
            }
            else
            {
                UpdateBattleText(player.GetCharacterSentence(53));
            }           
        }

        private void PlayerSpellProtection(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("Protection.mp3");
            PlayerBuffAbstract(player, 999, "Protection.bmp");
        }

        private void PlayerSpellAbsorbWater(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("AbsorbWater.mp3");
            PlayerBuffAbstract(player, 999, "AbsorbWater.bmp");
        }

        private void PlayerSpellSaintPower(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("SaintPower.mp3");
            PlayerBuffAbstract(player, 999, "SaintPower.bmp");
        }

        private void PlayerSpellShadowPact(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("ShadowPact.mp3");
            PlayerBuffAbstract(player, 999, "ShadowPact.bmp");
        }

        private void PlayerSpellBloodyVengeance(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("BloodyVengeance.mp3");
            PlayerBuffAbstract(player, 999, "BloodyVengeance.bmp");
        }

        private void PlayerSpellHeatBoost(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("HeatBoost.mp3");
            PlayerBuffAbstract(player, 999, "HeatBoost.bmp");
        }

        private void PlayerSpellEternalPresence(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("EternalPresence.mp3");
            PlayerBuffAbstract(player, 999, "EternalPresence.bmp");
        }

        private void PlayerSpellRiseOfImage(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("RiseOfImage.mp3");
            PlayerBuffAbstract(player, 999, "RiseOfImage.bmp");
        }

        protected void PlayerSpellPromisedKnowledge(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("PromisedKnowledge.mp3");
            PlayerBuffAbstract(player, 999, "PromisedKnowledge.bmp");
        }

        private void PlayerSpellFlameAura(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("FlameAura.mp3");
            PlayerBuffAbstract(player, 999, "FlameAura.bmp");
        }

        private void PlayerSpellWordOfLife(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("WordOfLife.mp3");
            PlayerBuffAbstract(player, 999, "WordOfLife.bmp");
        }

        private void PlayerSpellWordOfFortune(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("WordOfFortune.mp3");
            PlayerBuffAbstract(player, 2, "WordOfFortune.bmp"); // １ターン継続のためには、初期値は１＋１
        }

        private void EnemySpellWordOfFortune(MainCharacter enemy, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("WordOfFortune.mp3");
            enemy.Target = target;
            enemy.Target.CurrentWordOfFortune = 2; // １ターン継続のためには、初期値は１＋１
            enemy.Target.pbWordOfFortune.Image = Image.FromFile(Database.BaseResourceFolder + "WordOfFortune.bmp");
            enemy.Target.pbWordOfFortune.Update();
            UpdateBattleText(enemy.GetCharacterSentence(42));
        }

        private void PlayerSpellMirrorImage(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("MirrorImage.mp3");
            PlayerBuffAbstract(player, 999, "MirrorImage.bmp");
        }

        private void PlayerSpellDeflection(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("Deflection.mp3");
            PlayerBuffAbstract(player, 999, "Deflection.bmp");
        }

        private void PlayerSpellDamnation(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("Damnation.mp3");
            player.Target = ec1;
            player.Target.CurrentDamnation = 999;
            player.Target.pbDamnation.Image = Image.FromFile(Database.BaseResourceFolder + "Damnation.bmp");
            player.Target.pbDamnation.Update();
            UpdateBattleText(player.GetCharacterSentence(37));
        }

        private void EnemySpellDamnation(MainCharacter enemy, MainCharacter target, int interval)
        {
            GroundOne.PlaySoundEffect("Damnation.mp3");
            enemy.Target.CurrentDamnation = 999;
            enemy.Target.pbDamnation.Image = Image.FromFile(Database.BaseResourceFolder + "Damnation.bmp");
            enemy.Target.pbDamnation.Update();
            UpdateBattleText(enemy.GetCharacterSentence(37), interval);
        }

        private void PlayerSpellCelestialNova(MainCharacter player)
        {
            Random rd = new Random(DateTime.Now.Millisecond);
            int effectValue = 400 + player.Intelligence * 5 + rd.Next(player.Mind, player.Mind * 2);
            if (player.CurrentShadowPact > 0)
            {
                effectValue = (int)((float)effectValue * 1.5F);
            }
            if (player.CurrentEternalPresence > 0)
            {
                effectValue = (int)((float)effectValue * 1.3F);
            }

            if (player.Target != null)
            {
                if (((player != ec1) && (player.Target != ec1)) ||
                    ((player == ec1) && (player.Target == ec1)))
                {
                    if (player.Target.Dead)
                    {
                        UpdateBattleText("しかし" + player.Target.Name + "は死んでしまっているため効果が無かった！\r\n");
                    }
                    else if (player.Target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(player.Target.GetCharacterSentence(119));
                    }
                    else
                    {
                        GroundOne.PlaySoundEffect("CelestialNova.mp3");
                        player.Target.CurrentLife += effectValue;
                        UpdateLife(player.Target);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(25), effectValue));
                    }
                }
                else
                {
                    if (player.Target.CurrentAbsorbWater > 0 && player.CurrentTruthVision <= 0)
                    {
                        effectValue = (int)((float)effectValue / 1.2F);
                    }
                    if (player.Target.PA == PlayerAction.Defense || player.Target.CurrentStanceOfStanding > 0)
                    {
                        if (player.Target.CurrentAbsoluteZero > 0)
                        {
                            UpdateBattleText(player.Target.GetCharacterSentence(88));
                        }
                        else
                        {
                            effectValue = (int)((float)effectValue / 3.0F);
                        }
                    }
                    if (player.Target.CurrentOneImmunity > 0 && (player.Target.PA == PlayerAction.Defense || player.Target.CurrentStanceOfStanding > 0))
                    {
                        if (player.Target.CurrentAbsoluteZero > 0)
                        {
                            UpdateBattleText(player.Target.GetCharacterSentence(88));
                        }
                        else
                        {
                            effectValue = 0;
                        }
                    }

                    if (CheckDodge(player.Target)) { return; }

                    GroundOne.PlaySoundEffect("CelestialNova.mp3");
                    player.Target.CurrentLife -= effectValue;
                    UpdateLife(player.Target);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(26), effectValue.ToString()));
                }
            }
            else
            {
                UpdateBattleText(player.GetCharacterSentence(53));
            }
        }

        private void PlayerSpellResurrection(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("Resurrection.mp3");
            UpdateBattleText(player.GetCharacterSentence(52), 3000);

            if (player.Target != null)
            {
                if (player.Target != ec1)
                {
                    if (player.Target == player)
                    {
                        UpdateBattleText(player.GetCharacterSentence(55));
                    }
                    else if (!player.Target.Dead)
                    {
                        UpdateBattleText(player.GetCharacterSentence(54));
                    }
                    else if (this.CannotResurrect)
                    {
                        UpdateBattleText("しかし、完全絶対時間律【終焉】の効果により復活ができない！\r\n");
                    }
                    else
                    {
                        player.Target.Dead = false;
                        player.Target.CurrentLife += player.Target.MaxLife / 2;
                        UpdateLife(player.Target);
                        player.Target.labelLife.ForeColor = Color.Black;
                        player.Target.labelMana.ForeColor = Color.Black;
                        player.Target.labelName.ForeColor = Color.Black;
                        player.Target.labelSkill.ForeColor = Color.Black;
                        this.Update();
                        UpdateBattleText(player.Target.Name + "は復活した！\r\n");
                    }
                }
                else
                {
                    UpdateBattleText(player.GetCharacterSentence(53));
                }
            }
            else
            {
                UpdateBattleText(player.GetCharacterSentence(53));
            }
        }

        private void PlayerSpellGlory(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("Glory.mp3");
            player.CurrentGlory = 4; // ３ターン継続のためには、初期値は３＋１
            player.pbGlory.Image = Image.FromFile(Database.BaseResourceFolder + "Glory.bmp");
            player.pbGlory.Update();
            UpdateBattleText(player.GetCharacterSentence(24));
        }

        private void PlayerSpellBlackContract(MainCharacter player, MainCharacter target, bool fromEnemy)
        {
            GroundOne.PlaySoundEffect("BlackContract.mp3");
            target.CurrentBlackContract = 4; // ３ターン継続のためには、初期値は３＋１
            target.pbBlackContract.Image = Image.FromFile(Database.BaseResourceFolder + "BlackContract.bmp");
            target.pbBlackContract.Update();
            if (fromEnemy)
            {
                UpdateBattleText(player.GetCharacterSentence(35));
            }
            else
            {
                UpdateBattleText(target.GetCharacterSentence(35));
            }
        }

        private void PlayerSpellImmortalRave(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("ImmortalRave.mp3");
            player.CurrentImmortalRave = 4; // ３ターン継続のためには、初期値は３＋１
            player.pbImmortalRave.Image = Image.FromFile(Database.BaseResourceFolder + "ImmortalRave.bmp");
            player.pbImmortalRave.Update();
            UpdateBattleText(player.GetCharacterSentence(39));
        }

        private void PlayerSpellGaleWind(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("GaleWind.mp3");
            player.CurrentGaleWind = 2; // １ターン継続のためには、初期値は１＋１
            player.pbGaleWind.Image = Image.FromFile(Database.BaseResourceFolder + "GaleWind.bmp");
            player.pbGaleWind.Update();
            UpdateBattleText(player.GetCharacterSentence(40));
        }

        private void EnemySpellGaleWind(MainCharacter enemy)
        {
            EnemySpellGaleWind(enemy, 0);
        }
        private void EnemySpellGaleWind(MainCharacter enemy, int changeTime)
        {
            if (changeTime > 0)
            {
                enemy.CurrentGaleWind = changeTime;
            }
            else
            {
                enemy.CurrentGaleWind = 2; // １ターン継続のためには、初期値は１＋１
            }
            enemy.pbGaleWind.Image = Image.FromFile(Database.BaseResourceFolder + "GaleWind.bmp");
            enemy.pbGaleWind.Update();
            UpdateBattleText(enemy.GetCharacterSentence(40));
        }
        private void PlayerSpellAetherDrive(MainCharacter player)
        {
            player.CurrentAetherDrive = 4; // ３ターン継続のためには、初期値は３＋１
            player.pbAetherDrive.Image = Image.FromFile(Database.BaseResourceFolder + "AetherDrive.bmp");
            player.pbAetherDrive.Update();
            GroundOne.PlaySoundEffect("AetherDrive.mp3");
            UpdateBattleText(player.GetCharacterSentence(43));
        }

        private void PlayerSpellOneImmunity(MainCharacter player)
        {
            player.CurrentOneImmunity = 4; // ３ターン継続のためには、初期値は３＋１
            player.pbOneImmunity.Image = Image.FromFile(Database.BaseResourceFolder + "OneImmunity.bmp");
            player.pbOneImmunity.Update();
            GroundOne.PlaySoundEffect("OneImmunity.mp3");
            UpdateBattleText(player.GetCharacterSentence(46));
        }

        private void EnemySpellOneImmunity(MainCharacter enemy)
        {
            enemy.CurrentOneImmunity = 4; // ３ターン継続のためには、初期値は３＋１
            enemy.pbOneImmunity.Image = Image.FromFile(Database.BaseResourceFolder + "OneImmunity.bmp");
            enemy.pbOneImmunity.Update();
            GroundOne.PlaySoundEffect("OneImmunity.mp3");
            UpdateBattleText(enemy.GetCharacterSentence(46));
        }

        private void PlayerSpellHolyShock(MainCharacter player, MainCharacter target, int interval)
        {
            player.Target = target;
            Random rd = new Random(DateTime.Now.Millisecond);
            int effectValue = player.TotalIntelligence * 2 + rd.Next(120, 135);
            if (player.CurrentShadowPact > 0)
            {
                effectValue = (int)((float)effectValue * 1.5F);
            }
            if (player.CurrentEternalPresence > 0)
            {
                effectValue = (int)((float)effectValue * 1.3F);
            }
            if (target.CurrentAbsorbWater > 0 && player.CurrentTruthVision <= 0)
            {
                effectValue = (int)((float)effectValue / 1.2F);
            }
            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = (int)((float)effectValue / 3.0F);
                }
            }
            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = 0;
                }
            }

            // MirrorImageによる効果
            if (target.CurrentMirrorImage > 0)
            {
                player.CurrentLife -= effectValue;
                UpdateLife(player);
                UpdateBattleText(String.Format(target.GetCharacterSentence(58), effectValue.ToString(), player.Name), 1000);

                target.CurrentMirrorImage = 0;
                target.pbMirrorImage.Image = null;
                target.pbMirrorImage.Update();
                return;
            }

            if (CheckDodge(target)) { return; }

            GroundOne.PlaySoundEffect("HolyShock.mp3");
            player.Target.CurrentLife -= effectValue;
            UpdateLife(player.Target);
            UpdateBattleText(String.Format(player.GetCharacterSentence(23), target.Name, effectValue.ToString()), interval);
        }

        private void PlayerSpellDarkBlast(MainCharacter player, MainCharacter target, int interval)
        {
            player.Target = target;
            Random rd = new Random(DateTime.Now.Millisecond);
            int effectValue = player.Intelligence * 2 + rd.Next(30, 35);
            if (player.CurrentShadowPact > 0)
            {
                effectValue = (int)((float)effectValue * 1.5F);
            }
            if (player.CurrentEternalPresence > 0)
            {
                effectValue = (int)((float)effectValue * 1.3F);
            }
            if (target.CurrentAbsorbWater > 0 && player.CurrentTruthVision <= 0)
            {
                effectValue = (int)((float)effectValue / 1.2F);
            }
            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = (int)((float)effectValue / 3.0F);
                }
            }
            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = 0;
                }
            }

            GroundOne.PlaySoundEffect("DarkBlast.mp3");

            // MirrorImageによる効果
            if (target.CurrentMirrorImage > 0)
            {
                player.CurrentLife -= effectValue;
                UpdateLife(player);
                UpdateBattleText(String.Format(target.GetCharacterSentence(58), effectValue.ToString(), player.Name), 1000);

                target.CurrentMirrorImage = 0;
                target.pbMirrorImage.Image = null;
                target.pbMirrorImage.Update();
                return;
            }

            if (CheckDodge(target)) { return; }

            target.CurrentLife -= effectValue;
            UpdateLife(target);
            UpdateBattleText(String.Format(player.GetCharacterSentence(27), target.Name, effectValue.ToString()), interval);
        }

        private void PlayerSpellFireBall(MainCharacter player, MainCharacter target, int interval)
        {
            player.Target = target;
            Random rd = new Random(DateTime.Now.Millisecond);
            int effectValue = player.TotalIntelligence * 2 + rd.Next(30, 35);
            if (player.CurrentShadowPact > 0)
            {
                effectValue = (int)((float)effectValue * 1.5F);
            }
            if (player.CurrentEternalPresence > 0)
            {
                effectValue = (int)((float)effectValue * 1.3F);
            }
            if ((target.Accessory != null && target.Accessory.Name == "炎授天使の護符"))
            {
                effectValue -= target.Accessory.MinValue;
                if (effectValue <= 0) effectValue = 0;
            }
            if ((target.Accessory != null && target.Accessory.Name == "シールオブアクア＆ファイア"))
            {
                effectValue = (int)((float)effectValue * (100.0F - (float)target.Accessory.MinValue) / 100.0F);
            }
            if (target.CurrentAbsorbWater > 0 && player.CurrentTruthVision <= 0)
            {
                effectValue = (int)((float)effectValue / 1.2F);
            }
            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = (int)((float)effectValue / 3.0F);
                }
            }
            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = 0;
                }
            }

            GroundOne.PlaySoundEffect("FireBall.mp3");

            // MirrorImageによる効果
            if (target.CurrentMirrorImage > 0)
            {
                player.CurrentLife -= effectValue;
                UpdateLife(player);
                UpdateBattleText(String.Format(target.GetCharacterSentence(58), effectValue.ToString(), player.Name), 1000);

                target.CurrentMirrorImage = 0;
                target.pbMirrorImage.Image = null;
                target.pbMirrorImage.Update();
                return;
            }

            if (CheckDodge(target)) { return; }

            target.CurrentLife -= effectValue;
            UpdateLife(target);
            UpdateBattleText(String.Format(player.GetCharacterSentence(10), target.Name, effectValue.ToString()), interval);
        }

        private void PlayerSpellFlameStrike(MainCharacter player, MainCharacter target, int interval)
        {
            player.Target = target;
            Random rd = new Random(DateTime.Now.Millisecond);
            int effectValue = player.Intelligence * 3 + rd.Next(110, 125);
            if (player.CurrentShadowPact > 0)
            {
                effectValue = (int)((float)effectValue * 1.5F);
            }
            if (player.CurrentEternalPresence > 0)
            {
                effectValue = (int)((float)effectValue * 1.3F);
            }
            if ((target.Accessory != null && target.Accessory.Name == "炎授天使の護符"))
            {
                effectValue -= target.Accessory.MinValue;
                if (effectValue <= 0) effectValue = 0;
            }
            if ((target.Accessory != null && target.Accessory.Name == "シールオブアクア＆ファイア"))
            {
                effectValue = (int)((float)effectValue * (100.0F - (float)target.Accessory.MinValue) / 100.0F);
            }
            if (target.CurrentAbsorbWater > 0 && player.CurrentTruthVision <= 0)
            {
                effectValue = (int)((float)effectValue / 1.2F);
            }
            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = (int)((float)effectValue / 3.0F);
                }
            }
            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = 0;
                }
            }

            // MirrorImageによる効果
            if (target.CurrentMirrorImage > 0)
            {
                player.CurrentLife -= effectValue;
                UpdateLife(player);
                UpdateBattleText(String.Format(target.GetCharacterSentence(58), effectValue.ToString(), player.Name), 1000);

                target.CurrentMirrorImage = 0;
                target.pbMirrorImage.Image = null;
                target.pbMirrorImage.Update();
                return;
            }

            if (CheckDodge(target)) { return; }

            GroundOne.PlaySoundEffect("FlameStrike.mp3");
            target.CurrentLife -= effectValue;
            UpdateLife(target);
            UpdateBattleText(String.Format(player.GetCharacterSentence(11), target.Name, effectValue.ToString()), interval);
        }


        private void PlayerSpellVolcanicWave(MainCharacter player, MainCharacter target, int interval)
        {
            player.Target = target;
            Random rd = new Random(DateTime.Now.Millisecond);
            int effectValue = player.TotalIntelligence * 4 + rd.Next(450, 500);
            if (player.CurrentShadowPact > 0)
            {
                effectValue = (int)((float)effectValue * 1.5F);
            }
            if (player.CurrentEternalPresence > 0)
            {
                effectValue = (int)((float)effectValue * 1.3F);
            }
            if ((target.Accessory != null && target.Accessory.Name == "炎授天使の護符"))
            {
                effectValue -= target.Accessory.MinValue;
                if (effectValue <= 0) effectValue = 0;
            }
            if ((target.Accessory != null && target.Accessory.Name == "シールオブアクア＆ファイア"))
            {
                effectValue = (int)((float)effectValue * (100.0F - (float)target.Accessory.MinValue) / 100.0F);
            }
            if (target.CurrentAbsorbWater > 0 && player.CurrentTruthVision <= 0)
            {
                effectValue = (int)((float)effectValue / 1.2F);
            }
            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = (int)((float)effectValue / 3.0F);
                }
            }
            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = 0;
                }
            }

            // MirrorImageによる効果
            if (target.CurrentMirrorImage > 0)
            {
                player.CurrentLife -= effectValue;
                UpdateLife(player);
                UpdateBattleText(String.Format(target.GetCharacterSentence(58), effectValue.ToString(), player.Name), 1000);

                target.CurrentMirrorImage = 0;
                target.pbMirrorImage.Image = null;
                target.pbMirrorImage.Update();
                return;
            }

            if (CheckDodge(target)) { return; }

            GroundOne.PlaySoundEffect("VolcanicWave.mp3");
            target.CurrentLife -= effectValue;
            UpdateLife(target);
            UpdateBattleText(String.Format(player.GetCharacterSentence(12), target.Name, effectValue.ToString()), interval);
        }

        private void PlayerSpellLavaAnnihilation(MainCharacter player, MainCharacter target, int interval)
        {
            PlayerSpellLavaAnnihilation(player, target, interval, 0);
        }
        private void PlayerSpellLavaAnnihilation(MainCharacter player, MainCharacter target, int interval, int reduceValue)
        {
            player.Target = target;
            Random rd = new Random(DateTime.Now.Millisecond);
            GroundOne.PlaySoundEffect("LavaAnnihilation.mp3");
            if (player == ec1)
            {
                for (int ii = 0; ii < playerList.Length; ii++)
                {
                    int effectValue = player.TotalIntelligence * 5 + rd.Next(1250, 1500) - reduceValue;
                    if (!playerList[ii].Dead)
                    {
                        if (player.CurrentShadowPact > 0)
                        {
                            effectValue = (int)((float)effectValue * 1.5F);
                        }
                        if (player.CurrentEternalPresence > 0)
                        {
                            effectValue = (int)((float)effectValue * 1.3F);
                        }
                        if ((playerList[ii].Accessory != null && playerList[ii].Accessory.Name == "炎授天使の護符"))
                        {
                            effectValue -= playerList[ii].Accessory.MinValue;
                            if (effectValue <= 0) effectValue = 0;
                        }
                        if ((playerList[ii].Accessory != null && playerList[ii].Accessory.Name == "シールオブアクア＆ファイア"))
                        {
                            effectValue = (int)((float)effectValue * (100.0F - (float)playerList[ii].Accessory.MinValue) / 100.0F);
                        }
                        if (playerList[ii].CurrentAbsorbWater > 0 && player.CurrentTruthVision <= 0)
                        {
                            effectValue = (int)((float)effectValue / 1.2F);
                        }
                        if (playerList[ii].PA == PlayerAction.Defense || playerList[ii].CurrentStanceOfStanding > 0)
                        {
                            if (playerList[ii].CurrentAbsoluteZero > 0)
                            {
                                UpdateBattleText(playerList[ii].GetCharacterSentence(88));
                            }
                            else
                            {
                                effectValue = (int)((float)effectValue / 3.0F);
                            }
                        }
                        if (playerList[ii].CurrentOneImmunity > 0 && (playerList[ii].PA == PlayerAction.Defense || playerList[ii].CurrentStanceOfStanding > 0))
                        {
                            if (playerList[ii].CurrentAbsoluteZero > 0)
                            {
                                UpdateBattleText(playerList[ii].GetCharacterSentence(88));
                            }
                            else
                            {
                                effectValue = 0;
                            }
                        }

                        // MirrorImageによる効果
                        //if (target.CurrentMirrorImage > 0)
                        //{
                        //    player.CurrentLife -= effectValue;
                        //    UpdateLife(player);
                        //    UpdateBattleText(String.Format(target.GetCharacterSentence(58), effectValue.ToString(), player.Name), 1000);

                        //    target.CurrentMirrorImage = 0;
                        //    target.pbMirrorImage.Image = null;
                        //    target.pbMirrorImage.Update();
                        //}
                        //else

                        if (CheckDodge(playerList[ii])){ continue;}

                        playerList[ii].CurrentLife -= effectValue;
                        UpdateLife(playerList[ii]);
                        UpdateBattleText(String.Format(player.GetCharacterSentence(28), playerList[ii].Name, effectValue.ToString()), interval);
                    }
                }
            }
            else
            {
                int effectValue = player.TotalIntelligence * 5 + rd.Next(1250, 1500);
                if (!target.Dead)
                {
                    if (player.CurrentShadowPact > 0)
                    {
                        effectValue = (int)((float)effectValue * 1.5F);
                    }
                    if (player.CurrentEternalPresence > 0)
                    {
                        effectValue = (int)((float)effectValue * 1.3F);
                    }
                    if ((target.Accessory != null && target.Accessory.Name == "炎授天使の護符"))
                    {
                        effectValue -= target.Accessory.MinValue;
                        if (effectValue <= 0) effectValue = 0;
                    }
                    if ((target.Accessory != null && target.Accessory.Name == "シールオブアクア＆ファイア"))
                    {
                        effectValue = (int)((float)effectValue * (100.0F - (float)target.Accessory.MinValue) / 100.0F);
                    }
                    if (target.CurrentAbsorbWater > 0 && player.CurrentTruthVision <= 0)
                    {
                        effectValue = (int)((float)effectValue / 1.2F);
                    }
                    if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                    {
                        if (target.CurrentAbsoluteZero > 0)
                        {
                            UpdateBattleText(target.GetCharacterSentence(88));
                        }
                        else
                        {
                            effectValue = (int)((float)effectValue / 3.0F);
                        }
                    }
                    if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                    {
                        if (target.CurrentAbsoluteZero > 0)
                        {
                            UpdateBattleText(target.GetCharacterSentence(88));
                        }
                        else
                        {
                            effectValue = 0;
                        }
                    }

                    // MirrorImageによる効果
                    //if (target.CurrentMirrorImage > 0)
                    //{
                    //    player.CurrentLife -= effectValue;
                    //    UpdateLife(player);
                    //    UpdateBattleText(String.Format(target.GetCharacterSentence(58), effectValue.ToString(), player.Name), 1000);

                    //    target.CurrentMirrorImage = 0;
                    //    target.pbMirrorImage.Image = null;
                    //    target.pbMirrorImage.Update();
                    //    return;
                    //}

                    if (CheckDodge(target)) { return; }

                    target.CurrentLife -= effectValue;
                    UpdateLife(target);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(28), target.Name, effectValue.ToString()), interval);
                }
            }
        }

        private void PlayerSpellDevouringPlague(MainCharacter player, MainCharacter target)
        {
            Random rd = new Random(DateTime.Now.Millisecond);
            int effectValue = player.TotalIntelligence * 2 + rd.Next(150, 200);
            if (player.CurrentShadowPact > 0)
            {
                effectValue = (int)((float)effectValue * 1.5F);
            }
            if (player.CurrentEternalPresence > 0)
            {
                effectValue = (int)((float)effectValue * 1.3F);
            }
            if (target.CurrentAbsorbWater > 0 && player.CurrentTruthVision <= 0)
            {
                effectValue = (int)((float)effectValue / 1.2F);
            }
            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = (int)((float)effectValue / 3.0F);
                }
            }
            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = 0;
                }
            }

            if (CheckDodge(target)) { return; }

            GroundOne.PlaySoundEffect("DevouringPlague.mp3");
            if (player.CurrentAbsoluteZero > 0)
            {
                UpdateBattleText(player.GetCharacterSentence(119));
            }
            else
            {
                player.CurrentLife += effectValue;
                UpdateLife(player);
            }
            target.CurrentLife -= effectValue;
            UpdateLife(target);
            UpdateBattleText(String.Format(player.GetCharacterSentence(29), effectValue.ToString()));
        }

        private void PlayerSpellIceNeedle(MainCharacter player, MainCharacter target, int interval)
        {
            player.Target = target;
            Random rd = new Random(DateTime.Now.Millisecond);
            int effectValue = player.Intelligence * 2 + rd.Next(30, 35);
            if (player.CurrentShadowPact > 0)
            {
                effectValue = (int)((float)effectValue * 1.5F);
            }
            if (player.CurrentEternalPresence > 0)
            {
                effectValue = (int)((float)effectValue * 1.3F);
            }
            if ((target.Accessory != null && target.Accessory.Name == "シールオブアクア＆ファイア"))
            {
                effectValue = (int)((float)effectValue * (100.0F - (float)target.Accessory.MaxValue) / 100.0F);
            }
            if (target.CurrentAbsorbWater > 0 && player.CurrentTruthVision <= 0)
            {
                effectValue = (int)((float)effectValue / 1.2F);
            }
            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = (int)((float)effectValue / 3.0F);
                }
            }
            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = 0;
                }
            }

            // MirrorImageによる効果
            if (target.CurrentMirrorImage > 0)
            {
                player.CurrentLife -= effectValue;
                UpdateLife(player);
                UpdateBattleText(String.Format(target.GetCharacterSentence(58), effectValue.ToString(), player.Name), 1000);

                target.CurrentMirrorImage = 0;
                target.pbMirrorImage.Image = null;
                target.pbMirrorImage.Update();
                return;
            }

            if (CheckDodge(target)) { return; }

            GroundOne.PlaySoundEffect("IceNeedle.mp3");
            target.CurrentLife -= effectValue;
            UpdateLife(target);
            UpdateBattleText(String.Format(player.GetCharacterSentence(30), target.Name, effectValue.ToString()), interval);
        }

        private void PlayerSpellFrozenLance(MainCharacter player, MainCharacter target, int interval)
        {
            Random rd = new Random(DateTime.Now.Millisecond);
            int effectValue = player.TotalIntelligence * 3 + rd.Next(110, 125);
            if (player.CurrentShadowPact > 0)
            {
                effectValue = (int)((float)effectValue * 1.5F);
            }
            if (player.CurrentEternalPresence > 0)
            {
                effectValue = (int)((float)effectValue * 1.3F);
            }
            if ((target.Accessory != null && target.Accessory.Name == "シールオブアクア＆ファイア"))
            {
                effectValue = (int)((float)effectValue * (100.0F - (float)target.Accessory.MaxValue) / 100.0F);
            }
            if (target.CurrentAbsorbWater > 0 && player.CurrentTruthVision <= 0)
            {
                effectValue = (int)((float)effectValue / 1.2F);
            }
            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = (int)((float)effectValue / 3.0F);
                }
            }
            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = 0;
                }
            }

            GroundOne.PlaySoundEffect("FrozenLance.mp3");

            // MirrorImageによる効果
            if (target.CurrentMirrorImage > 0)
            {
                player.CurrentLife -= effectValue;
                UpdateLife(player);
                UpdateBattleText(String.Format(target.GetCharacterSentence(58), effectValue.ToString(), player.Name), 1000);

                target.CurrentMirrorImage = 0;
                target.pbMirrorImage.Image = null;
                target.pbMirrorImage.Update();
                return;
            }

            if (CheckDodge(target)) { return; }

            target.CurrentLife -= effectValue;
            UpdateLife(target);
            UpdateBattleText(String.Format(player.GetCharacterSentence(31), target.Name, effectValue.ToString()), interval);
        }

        private void PlayerSpellWordOfPower(MainCharacter player, MainCharacter target, int interval)
        {
            player.Target = target;
            Random rd = new Random(DateTime.Now.Millisecond);
            int effectValue = (player.TotalStrength) * 2 + rd.Next(30, 35);
            if (player.CurrentShadowPact > 0)
            {
                effectValue = (int)((float)effectValue * 1.5F);
            }
            if (player.CurrentEternalPresence > 0)
            {
                effectValue = (int)((float)effectValue * 1.3F);
            }
            if (player.Target.CurrentAbsorbWater > 0 && player.CurrentTruthVision <= 0)
            {
                effectValue = (int)((float)effectValue / 1.2F);
            }
            // [情報]：WordOfPowerは防御できない

            // MirrorImageによる効果
            //if (target.CurrentMirrorImage > 0)
            //{
            //    player.CurrentLife -= effectValue;
            //    UpdateLife(player);
            //    UpdateBattleText(String.Format(target.GetCharacterSentence(58), effectValue.ToString(), player.Name), 1000);

            //    target.CurrentMirrorImage = 0;
            //    target.pbMirrorImage.Image = null;
            //    target.pbMirrorImage.Update();
            //    return;
            //}

            GroundOne.PlaySoundEffect("WordOfPower.mp3");
            if (CheckDodge(target)) { return; }

            player.Target.CurrentLife -= effectValue;
            UpdateLife(player.Target);
            UpdateBattleText(String.Format(player.GetCharacterSentence(33), target.Name, effectValue.ToString()), interval);
        }

        private void PlayerSpellWhiteOut(MainCharacter player, MainCharacter target, int interval)
        {
            player.Target = target;
            Random rd = new Random(DateTime.Now.Millisecond);
            int effectValue = player.TotalIntelligence * 4 + rd.Next(450, 500);
            if (player.CurrentShadowPact > 0)
            {
                effectValue = (int)((float)effectValue * 1.5F);
            }
            if (player.CurrentEternalPresence > 0)
            {
                effectValue = (int)((float)effectValue * 1.3F);
            }
            if (target.CurrentAbsorbWater > 0 && player.CurrentTruthVision <= 0)
            {
                effectValue = (int)((float)effectValue / 1.2F);
            }
            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = (int)((float)effectValue / 3.0F);
                }
            }
            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = 0;
                }
            }

            // MirrorImageによる効果
            if (target.CurrentMirrorImage > 0)
            {
                player.CurrentLife -= effectValue;
                UpdateLife(player);
                UpdateBattleText(String.Format(target.GetCharacterSentence(58), effectValue.ToString(), player.Name), 1000);

                target.CurrentMirrorImage = 0;
                target.pbMirrorImage.Image = null;
                target.pbMirrorImage.Update();
                return;
            }

            if (CheckDodge(target)) { return; }

            GroundOne.PlaySoundEffect("WhiteOut.mp3");
            target.CurrentLife -= effectValue;
            UpdateLife(target);
            UpdateBattleText(String.Format(player.GetCharacterSentence(34), target.Name, effectValue.ToString()), interval);
        }

        private void PlayerSpellTimeStop(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("TimeStop.mp3");
            CurrentTimeStop = 2; // １ターン継続のためには、初期値は１＋１
            UpdateBattleText(player.GetCharacterSentence(47));
        }

        private void PlayerSpellDispelMagic(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(48), 1000);

            if (target.CurrentNothingOfNothingness > 0)
            {
                UpdateBattleText("しかし、" + target.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                return;
            }

            target.CurrentProtection = 0;
            target.pbProtection.Image = null;
            target.pbProtection.Update();
            target.CurrentSaintPower = 0;
            target.pbSaintPower.Image = null;
            target.pbSaintPower.Update();
            target.CurrentShadowPact = 0;
            target.pbShadowPact.Image = null;
            target.pbShadowPact.Update();
            target.CurrentAbsorbWater = 0;
            target.pbAbsorbWater.Image = null;
            target.pbAbsorbWater.Update();
            target.CurrentEternalPresence = 0;
            target.pbEternalPresence.Image = null;
            target.pbEternalPresence.Update();

            target.CurrentBloodyVengeance = 0;
            target.BuffStrength_BloodyVengeance = 0;
            target.pbBloodyVengeance.Image = null;
            target.pbBloodyVengeance.Update();
            target.CurrentHeatBoost = 0;
            target.BuffAgility_HeatBoost = 0;
            target.pbHeatBoost.Image = null;
            target.pbHeatBoost.Update();
            target.CurrentPromisedKnowledge = 0;
            target.BuffIntelligence_PromisedKnowledge = 0;
            target.pbPromisedKnowledge.Image = null;
            target.pbPromisedKnowledge.Update();
            target.CurrentRiseOfImage = 0;
            target.BuffMind_RiseOfImage = 0;
            target.pbRiseOfImage.Image = null;
            target.pbRiseOfImage.Update();
            // [情報]：Staminaはありません。

            target.CurrentWordOfLife = 0;
            target.pbWordOfLife.Image = null;
            target.pbWordOfLife.Update();
            target.CurrentFlameAura = 0;
            target.pbFlameAura.Image = null;
            target.pbFlameAura.Update();

            GroundOne.PlaySoundEffect("DispelMagic.mp3");
            UpdateBattleText(ec1.Name + "の能力ＵＰ型効果を全て打ち消した！\r\n");
        }

        private void EnemySpellDispelMagic(MainCharacter enemy, MainCharacter target, int interval)
        {
            UpdateBattleText(enemy.GetCharacterSentence(48), interval);

            if (target.CurrentNothingOfNothingness > 0)
            {
                UpdateBattleText("しかし、" + target.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                return;
            }

            target.CurrentProtection = 0;
            target.pbProtection.Image = null;
            target.pbProtection.Update();
            target.CurrentSaintPower = 0;
            target.pbSaintPower.Image = null;
            target.pbSaintPower.Update();
            target.CurrentShadowPact = 0;
            target.pbShadowPact.Image = null;
            target.pbShadowPact.Update();
            target.CurrentAbsorbWater = 0;
            target.pbAbsorbWater.Image = null;
            target.pbAbsorbWater.Update();
            target.CurrentEternalPresence = 0;
            target.pbEternalPresence.Image = null;
            target.pbEternalPresence.Update();

            target.CurrentBloodyVengeance = 0;
            target.BuffStrength_BloodyVengeance = 0;
            target.pbBloodyVengeance.Image = null;
            target.pbBloodyVengeance.Update();
            target.CurrentHeatBoost = 0;
            target.BuffAgility_HeatBoost = 0;
            target.pbHeatBoost.Image = null;
            target.pbHeatBoost.Update();
            target.CurrentPromisedKnowledge = 0;
            target.BuffIntelligence_PromisedKnowledge = 0;
            target.pbPromisedKnowledge.Image = null;
            target.pbPromisedKnowledge.Update();
            target.CurrentRiseOfImage = 0;
            target.BuffMind_RiseOfImage = 0;
            target.pbRiseOfImage.Image = null;
            target.pbRiseOfImage.Update();
            // [情報]：Staminaはありません。

            target.CurrentWordOfLife = 0;
            target.pbWordOfLife.Image = null;
            target.pbWordOfLife.Update();
            target.CurrentFlameAura = 0;
            target.pbFlameAura.Image = null;
            target.pbFlameAura.Update();

            GroundOne.PlaySoundEffect("DispelMagic.mp3");
            UpdateBattleText(target.Name + "の能力ＵＰ型効果を全て打ち消した！\r\n", interval);
        }

        private void PlayerSpellTranquility(MainCharacter player, MainCharacter target, int interval)
        {
            UpdateBattleText(player.GetCharacterSentence(84), interval);

            if (target.CurrentNothingOfNothingness > 0)
            {
                UpdateBattleText("しかし、" + target.Name + "は無効化を無効にするオーラによって護られている！\r\n");
                return;
            }

            target.CurrentGlory = 0;
            target.pbGlory.Image = null;
            target.pbGlory.Update();
            target.CurrentBlackContract = 0;
            target.pbBlackContract.Image = null;
            target.pbBlackContract.Update();
            target.CurrentImmortalRave = 0;
            target.pbImmortalRave.Image = null;
            target.pbImmortalRave.Update();
            target.CurrentAbsoluteZero = 0;
            target.pbAbsoluteZero.Image = null;
            target.pbAbsoluteZero.Update();
            target.CurrentAetherDrive = 0;
            target.pbAetherDrive.Image = null;
            target.pbAetherDrive.Update();
            target.CurrentOneImmunity = 0;
            target.pbOneImmunity.Image = null;
            target.pbOneImmunity.Update();
            target.CurrentHighEmotionality = 0;
            target.pbHighEmotionality.Image = null;
            target.pbHighEmotionality.Update();

            GroundOne.PlaySoundEffect("Tranquility.mp3");
            UpdateBattleText(target.Name + "の一定時間付与効果を全て打ち消した！\r\n", interval);
        }

        private void PlayerSpellAbsoluteZero(MainCharacter player, MainCharacter target, int interval)
        {
            GroundOne.PlaySoundEffect("AbsoluteZero.mp3");
            if (target.CurrentAbsoluteZero <= 0)
            {
                target.CurrentAbsoluteZero = 4; // ３ターン継続のためには、初期値は３＋１
            }
            target.pbAbsoluteZero.Image = Image.FromFile(Database.BaseResourceFolder + "AbsoluteZero.bmp");
            target.pbAbsoluteZero.Update();
            UpdateBattleText(player.GetCharacterSentence(81), 200);
        }

        private void PlayerSpellCleansing(MainCharacter player)
        {
            if (player.CurrentStunning > 0 || player.CurrentSilence > 0 || player.CurrentPoison > 0 || player.CurrentParalyze > 0 || player.CurrentTemptation > 0 || player.CurrentFrozen > 0)
            {
                UpdateBattleText(player.GetCharacterSentence(109));
                return;
            }

            if (player.Target != ec1)
            {
                UpdateBattleText(player.GetCharacterSentence(77));
                player.Target.CurrentStunning = 0;
                player.Target.CurrentSilence = 0;
                player.Target.CurrentPoison = 0;
                player.Target.CurrentTemptation = 0;
                player.Target.CurrentFrozen = 0;
                player.Target.CurrentParalyze = 0;
                player.Target.pbStun.Image = null;
                player.Target.pbStun.Update();
                player.Target.pbSilence.Image = null;
                player.Target.pbSilence.Update();
                player.Target.pbPoison.Image = null;
                player.Target.pbPoison.Update();
                player.Target.pbTemptation.Image = null;
                player.Target.pbTemptation.Update();
                player.Target.pbFrozen.Image = null;
                player.Target.pbFrozen.Update();
                player.Target.pbParalyze.Image = null;
                player.Target.pbParalyze.Update();
                GroundOne.PlaySoundEffect("Cleansing.mp3");
                UpdateBattleText(player.Target.Name + "にかかっている負の影響が全て取り払われた。\r\n");
            }
            else
            {
                UpdateBattleText(player.GetCharacterSentence(53));
            }
        }

        // [警告]：敵の行動メソッドはプレイヤー側と同等なので、統一してください。
        private void EnemySpellFreshHeal(MainCharacter enemy, MainCharacter target)
        {
            Random rd = new Random(DateTime.Now.Millisecond);
            int lifeGain = enemy.TotalIntelligence * 4 + rd.Next(1, 20);

            if (enemy.CurrentAbsoluteZero > 0)
            {
                UpdateBattleText(enemy.GetCharacterSentence(119));
                //UpdateBattleText("しかし" + enemy.Name + "は絶対零度効果によりライフ回復できない！\r\n");
            }
            else
            {
                GroundOne.PlaySoundEffect("FreshHeal.mp3");
                enemy.CurrentLife += lifeGain;
                UpdateLife(enemy);
                UpdateBattleText(String.Format(enemy.GetCharacterSentence(9), lifeGain.ToString()));
            }
        }

        private void EnemySpellPutiFireBall(MainCharacter enemy, MainCharacter target)
        {
            Random rd = new Random(DateTime.Now.Millisecond);
            int effectValue = enemy.TotalIntelligence + rd.Next(3, 8);
            if (enemy.CurrentShadowPact > 0)
            {
                effectValue = (int)((float)effectValue * 1.5F);
            }
            if (enemy.CurrentEternalPresence > 0)
            {
                effectValue = (int)((float)effectValue * 1.3F);
            }
            if ((target.Accessory != null && target.Accessory.Name == "炎授天使の護符"))
            {
                effectValue -= target.Accessory.MinValue;
                if (effectValue <= 0) effectValue = 0;
            }
            if ((target.Accessory != null && target.Accessory.Name == "シールオブアクア＆ファイア"))
            {
                effectValue = (int)((float)effectValue * (100.0F - (float)target.Accessory.MinValue) / 100.0F);
            }
            if (target.CurrentAbsorbWater > 0)
            {
                effectValue = (int)((float)effectValue / 1.2F);
            }
            if (target.CurrentEternalPresence > 0)
            {
                effectValue = (int)((float)effectValue / 1.3F);
            }
            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = (int)((float)effectValue / 3.0F);
                }
            }
            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = 0;
                }
            }

            if (CheckDodge(target)) { return; }

            GroundOne.PlaySoundEffect("PutiFireBall.mp3");
            target.CurrentLife -= effectValue;
            UpdateLife(target);
            UpdateBattleText(ec1.Name + "はプチ・ファイア・ボールを唱えた！ " + target.Name + "に" + effectValue.ToString() + "のダメージ\r\n");
        }

        private void EnemySpellFlameStrike(MainCharacter enemy, MainCharacter target)
        {
            Random rd = new Random(DateTime.Now.Millisecond);
            int effectValue = enemy.TotalIntelligence + rd.Next(110, 125);
            if (enemy.CurrentShadowPact > 0)
            {
                effectValue = (int)((float)effectValue * 1.5F);
            }
            if (enemy.CurrentEternalPresence > 0)
            {
                effectValue = (int)((float)effectValue * 1.3F);
            }
            if ((target.Accessory != null && target.Accessory.Name == "炎授天使の護符"))
            {
                effectValue -= target.Accessory.MinValue;
                if (effectValue <= 0) effectValue = 0;
            }
            if ((target.Accessory != null && target.Accessory.Name == "シールオブアクア＆ファイア"))
            {
                effectValue = (int)((float)effectValue * (100.0F - (float)target.Accessory.MinValue) / 100.0F);
            }
            if (target.CurrentAbsorbWater > 0)
            {
                effectValue = (int)((float)effectValue / 1.2F);
            }
            if (target.CurrentEternalPresence > 0)
            {
                effectValue = (int)((float)effectValue / 1.3F);
            }
            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = (int)((float)effectValue / 3.0F);
                }
            }
            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = 0;
                }
            }

            if (CheckDodge(target)) { return; }

            GroundOne.PlaySoundEffect("FlameStrike.mp3");
            target.CurrentLife -= effectValue;
            UpdateLife(target);
            UpdateBattleText(String.Format(enemy.GetCharacterSentence(11), target.Name, effectValue.ToString()));
        }

        private void EnemySpellFrozenLance(MainCharacter enemy, MainCharacter target)
        {
            Random rd = new Random(DateTime.Now.Millisecond);
            int effectValue = enemy.TotalIntelligence + rd.Next(110, 125);
            if (enemy.CurrentShadowPact > 0)
            {
                effectValue = (int)((float)effectValue * 1.5F);
            }
            if (enemy.CurrentEternalPresence > 0)
            {
                effectValue = (int)((float)effectValue * 1.3F);
            }
            if ((target.Accessory != null && target.Accessory.Name == "シールオブアクア＆ファイア"))
            {
                effectValue = (int)((float)effectValue * (100.0F - (float)target.Accessory.MaxValue) / 100.0F);
            }
            if (target.CurrentAbsorbWater > 0)
            {
                effectValue = (int)((float)effectValue / 1.2F);
            }
            if (target.CurrentEternalPresence > 0)
            {
                effectValue = (int)((float)effectValue / 1.3F);
            }
            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = (int)((float)effectValue / 3.0F);
                }
            }
            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = 0;
                }
            }

            if (CheckDodge(target)) { return; }

            GroundOne.PlaySoundEffect("FrozenLance.mp3");
            target.CurrentLife -= effectValue;
            UpdateLife(target);
            UpdateBattleText(String.Format(enemy.GetCharacterSentence(31), target.Name, effectValue.ToString()));
        }

        private void EnemySpellHolyShock(MainCharacter enemy, MainCharacter target)
        {
            Random rd = new Random(DateTime.Now.Millisecond);
            int effectValue = enemy.TotalIntelligence + rd.Next(120, 135);
            if (enemy.CurrentShadowPact > 0)
            {
                effectValue = (int)((float)effectValue * 1.5F);
            }
            if (enemy.CurrentEternalPresence > 0)
            {
                effectValue = (int)((float)effectValue * 1.3F);
            }
            if (target.CurrentAbsorbWater > 0)
            {
                effectValue = (int)((float)effectValue / 1.2F);
            }
            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = (int)((float)effectValue / 3.0F);
                }
            }
            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = 0;
                }
            }

            if (CheckDodge(target)) { return; }

            GroundOne.PlaySoundEffect("HolyShock.mp3");
            target.CurrentLife -= effectValue;
            UpdateLife(target);
            UpdateBattleText(String.Format(enemy.GetCharacterSentence(23), target.Name, effectValue.ToString()));
        }

        private void EnemySpellWordOfPower(MainCharacter enemy, MainCharacter target)
        {
            // [警告]：物理攻撃かどうかで結果が変わるようにクラス構築側に属性を作って物理属性になるようにしてください。
            Random rd = new Random(DateTime.Now.Millisecond);
            int effectValue = (enemy.TotalStrength) + rd.Next(30, 35);
            if (enemy.CurrentShadowPact > 0)
            {
                effectValue = (int)((float)effectValue * 1.5F);
            }
            if (enemy.CurrentEternalPresence > 0)
            {
                effectValue = (int)((float)effectValue * 1.3F);
            }
            if (target.CurrentAbsorbWater > 0)
            {
                effectValue = (int)((float)effectValue / 1.2F);
            }
            // [情報]：WordOfPowerは防御できない

            if (CheckDodge(target)) { return; }

            GroundOne.PlaySoundEffect("WordOfPower.mp3");
            target.CurrentLife -= effectValue;
            UpdateLife(target);
            UpdateBattleText(String.Format(enemy.GetCharacterSentence(33), target.Name, effectValue.ToString()));
        }

        private void PlayerSpellGenesis(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("Genesis.mp3");
            UpdateBattleText(player.GetCharacterSentence(108));
            player.PA = player.BeforePA;
            player.CurrentUsingItem = player.BeforeUsingItem;
            player.CurrentSkillName = player.BeforeSkillName;
            player.CurrentSpellName = player.BeforeSpellName;
            player.Target = target;
            PlayerAttackPhase(player, true);
        }
        #endregion



        #region "スキル発動"
        #region "動"

        private void PlayerSkillStraightSmash(MainCharacter player, MainCharacter target, bool ignoreDefense)
        {
            // 相手：カウンターアタックが入っている場合
            if (target.CurrentCounterAttack > 0)
            {
                // 自分：相手の防御体制を無視できる場合
                if (ignoreDefense)
                {
                    UpdateBattleText(player.GetCharacterSentence(110));
                }
                else
                {
                    // 自分：NothingOfNothingnessによる無効化が張ってある場合
                    if (player.CurrentNothingOfNothingness > 0)
                    {
                        UpdateBattleText(player.Name + "が" + target.Name + "へStraightSmashを仕掛ける所で・・・\r\n", 500);
                        UpdateBattleText(target.GetCharacterSentence(114), 1000);
                        UpdateBattleText(player.Name + "は無効化オーラによって護られている！\r\n");
                        //target.CurrentStanceOfEyes = false;
                        target.RemoveStanceOfEyes(); // 後編編集
                        //target.CurrentNegate = false;
                        target.RemoveNegate(); // 後編編集
                        //target.CurrentCounterAttack = false;
                        target.RemoveCounterAttack(); // 後編編集
                    }
                    else
                    {
                        UpdateBattleText(player.Name + "が" + target.Name + "へStraightSmashを仕掛ける所で・・・\r\n", 1000);
                        UpdateBattleText(target.GetCharacterSentence(113));
                        PlayerNormalAttack(target, player, -1, 0, false);
                        return;
                    }
                }
            }

            // Gloryによる効果
            if (player.CurrentGlory > 0)
            {
                player.Target = player;
                PlayerSpellFreshHeal(player);
                player.Target = target;
            }

            Random rd = new Random(DateTime.Now.Millisecond);
            int atk = rd.Next((player.TotalStrength) + (player.TotalAgility * 2) + player.MainWeapon.MinValue * 1,
                              (player.TotalStrength) + (player.TotalAgility * 3) + player.MainWeapon.MaxValue * 2);
            if (player.CurrentSaintPower > 0)
            {
                atk = (int)((float)atk * 1.5F);
            }
            if (player.CurrentEternalPresence > 0)
            {
                atk = (int)((float)atk * 1.3F);
            }
            if (player.CurrentAetherDrive > 0)
            {
                atk = (int)((float)atk * 2.0F);
            }
            if (target.CurrentProtection > 0 && player.CurrentTruthVision <= 0)
            {
                atk = (int)((float)atk / 1.2F);
            }
            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    atk = (int)((float)atk / 3.0F);
                }
            }
            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(ec1.GetCharacterSentence(88));
                }
                else
                {
                    atk = 0;
                }
            }

            // Deflectionによる効果
            if (target.CurrentDeflection > 0)
            {
                player.CurrentLife -= atk;
                UpdateLife(player);
                UpdateBattleText(String.Format(target.GetCharacterSentence(61), atk.ToString(), player.Name), 1000);

                target.CurrentDeflection = 0;
                target.pbDeflection.Image = null;
                target.pbDeflection.Update();
                return;
            }

            // クリティカル判定
            bool detectCritical = false;
            Random rd2 = new Random(DateTime.Now.Millisecond);
            int result = rd.Next(1, 1001);
            if (player.CurrentWordOfFortune == 1)
            {
                result = 1;
            }
            if (result < 7 + (player.TotalAgility))
            {
                detectCritical = true;
                atk = (int)((float)atk * 3.0F);
            }

            if (CheckDodge(target)) { return; }

            target.CurrentLife -= atk;
            UpdateLife(target);
            UpdateBattleText(player.GetCharacterSentence(1));
            GroundOne.PlaySoundEffect("StraightSmash.mp3");

            if (detectCritical)
            {
                UpdateBattleText(player.GetCharacterSentence(117));
            }
            UpdateBattleText(player.Name + "のストレート・スマッシュがヒット。" + target.Name + "へ" + atk.ToString() + "のダメージ\r\n");

            // FlameAuraによる追加攻撃
            if (player.CurrentFlameAura > 0)
            {
                int additional = rd.Next(player.TotalIntelligence, player.TotalIntelligence * 3 + 10);
                if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                {
                    if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(88));
                    }
                    else
                    {
                        additional = (int)((float)additional / 3.0F);
                    }
                }
                if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                {
                    if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(88));
                    }
                    else
                    {
                        additional = 0;
                    }
                }
                target.CurrentLife -= additional;
                UpdateLife(target);
                UpdateBattleText(String.Format(player.GetCharacterSentence(14), additional.ToString()));
            }

            // ImmortalRaveによる追加攻撃
            if (player.CurrentImmortalRave == 3)
            {
                PlayerSpellFireBall(player, target, 500);
            }
            else if (player.CurrentImmortalRave == 2)
            {
                PlayerSpellFlameStrike(player, target, 500);
            }
            else if (player.CurrentImmortalRave == 1)
            {
                PlayerSpellVolcanicWave(player, target, 500);
            }
        }

        private void EnemySkillStraightSmash(MainCharacter player, MainCharacter target, bool ignoreDetailMessage, bool ignoreDefense)
        {
            // 相手：カウンターアタックが入っている場合
            if (target.CurrentCounterAttack > 0)
            {
                // 自分：相手の防御体制を無視できる場合
                if (ignoreDefense)
                {
                    UpdateBattleText(player.GetCharacterSentence(110));
                }
                else
                {
                    // 自分：NothingOfNothingnessによる無効化が張ってある場合
                    if (player.CurrentNothingOfNothingness > 0)
                    {
                        UpdateBattleText(player.Name + "が" + target.Name + "へStraightSmashを仕掛ける所で・・・\r\n", 500);
                        UpdateBattleText(target.GetCharacterSentence(114), 1000);
                        UpdateBattleText(player.Name + "は無効化オーラによって護られている！\r\n");
                        //target.CurrentStanceOfEyes = false;
                        target.RemoveStanceOfEyes(); // 後編編集
                        //target.CurrentNegate = false;
                        target.RemoveNegate(); // 後編編集
                        //target.CurrentCounterAttack = false;
                        target.RemoveCounterAttack(); // 後編編集
                    }
                    else
                    {
                        UpdateBattleText(player.Name + "が" + target.Name + "へStraightSmashを仕掛ける所で・・・\r\n", 1000);
                        UpdateBattleText(target.GetCharacterSentence(113));
                        PlayerNormalAttack(target, player, -1, 0, false);
                        return;
                    }
                }
            }

            // Gloryによる効果
            if (player.CurrentGlory > 0)
            {
                player.Target = player;
                PlayerSpellFreshHeal(player);
                player.Target = target;
            }

            Random rd = new Random(DateTime.Now.Millisecond);
            int effectValue = rd.Next((player.TotalStrength) + (player.TotalAgility * 1) /*+ enemy.MainWeapon.MinValue * 1*/,
                                      (player.TotalStrength) + (player.TotalAgility * 2) /*+ enemy.MainWeapon.MaxValue * 2*/); // [コメント]敵にも武器を付けたら復活させてテストしてください。
            if (player.CurrentSaintPower > 0)                                        // 本来*2*3だが、敵なので*1*2で調整
            {
                effectValue = (int)((float)effectValue * 1.5F);
            }
            if (player.CurrentEternalPresence > 0)
            {
                effectValue = (int)((float)effectValue * 1.3F);
            }
            if (player.CurrentAetherDrive > 0)
            {
                effectValue = (int)((float)effectValue * 2.0F);
            }
            if (target.CurrentProtection > 0)
            {
                effectValue = (int)((float)effectValue / 1.2F);
            }
            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = (int)((float)effectValue / 3.0F);
                }
            }
            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = 0;
                }
            }

            // Deflectionによる効果
            if (target.CurrentDeflection > 0)
            {
                player.CurrentLife -= effectValue;
                UpdateLife(player);
                UpdateBattleText(String.Format(target.GetCharacterSentence(61), effectValue.ToString(), player.Name), 1000);

                target.CurrentDeflection = 0;
                target.pbDeflection.Image = null;
                target.pbDeflection.Update();
                return;
            }

            // クリティカル判定
            bool detectCritical = false;
            Random rd2 = new Random(DateTime.Now.Millisecond);
            int result = rd.Next(1, 1001);
            if (player.CurrentWordOfFortune == 1)
            {
                result = 1;
            }
            if (result < 7 + (player.TotalAgility))
            {
                detectCritical = true;
                effectValue = (int)((float)effectValue * 3.0F);
            }

            if (CheckDodge(target)) { return; }

            target.CurrentLife -= effectValue;
            UpdateLife(target);
            if (!ignoreDetailMessage)
            {
                UpdateBattleText(player.GetCharacterSentence(1));
            }
            GroundOne.PlaySoundEffect("StraightSmash.mp3");

            if (detectCritical)
            {
                UpdateBattleText(player.GetCharacterSentence(117));
            }
            if (!ignoreDetailMessage)
            {
                UpdateBattleText(player.Name + "のストレート・スマッシュがヒット。" + target.Name + "へ" + effectValue.ToString() + "のダメージ\r\n");
            }
            else
            {
                UpdateBattleText(target.Name + "へ" + effectValue.ToString() + "のダメージ\r\n");
            }

            // FlameAuraによる追加攻撃
            if (player.CurrentFlameAura > 0)
            {
                int additional = rd.Next(player.TotalIntelligence, player.TotalIntelligence * 3 + 10);
                if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                {
                    if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(88));
                    }
                    else
                    {
                        additional = (int)((float)additional / 3.0F);
                    }
                }
                if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                {
                    if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(88));
                    }
                    else
                    {
                        additional = 0;
                    }
                }
                target.CurrentLife -= additional;
                UpdateLife(target);
                UpdateBattleText(String.Format(player.GetCharacterSentence(14), additional.ToString()));
            }

            // ImmortalRaveによる追加攻撃
            if (player.CurrentImmortalRave == 3)
            {
                PlayerSpellFireBall(player, target, 500);
            }
            else if (player.CurrentImmortalRave == 2)
            {
                PlayerSpellFlameStrike(player, target, 500);
            }
            else if (player.CurrentImmortalRave == 1)
            {
                PlayerSpellVolcanicWave(player, target, 500);
            }
        }

        private void PlayerSkillDoubleSlash(MainCharacter player, MainCharacter target, bool ignoreDefense)
        {
            // 相手：カウンターアタックが入っている場合
            if (target.CurrentCounterAttack > 0)
            {
                // 自分：相手の防御体制を無視できる場合
                if (ignoreDefense)
                {
                    UpdateBattleText(player.GetCharacterSentence(110));
                }
                else
                {
                    // 自分：NothingOfNothingnessによる無効化が張ってある場合
                    if (player.CurrentNothingOfNothingness > 0)
                    {
                        UpdateBattleText(player.Name + "が" + target.Name + "へ" + Database.DOUBLE_SLASH + "を仕掛ける所で・・・\r\n", 500);
                        UpdateBattleText(target.GetCharacterSentence(114), 1000);
                        UpdateBattleText(player.Name + "は無効化オーラによって護られている！\r\n");
                        //target.CurrentStanceOfEyes = false;
                        target.RemoveStanceOfEyes(); // 後編編集
                        //target.CurrentNegate = false;
                        target.RemoveNegate(); // 後編編集
                        //target.CurrentCounterAttack = false;
                        target.RemoveCounterAttack(); // 後編編集
                    }
                    else
                    {
                        UpdateBattleText(player.Name + "が" + target.Name + "へ" + Database.DOUBLE_SLASH + "を仕掛ける所で・・・\r\n", 1000);
                        UpdateBattleText(target.GetCharacterSentence(113));
                        PlayerNormalAttack(target, player, -1, 0, false);
                        return;
                    }
                }
            }

            UpdateBattleText(player.GetCharacterSentence(2));
            PlayerNormalAttack(player, target, -1, 0, false);
            UpdateBattleText(player.GetCharacterSentence(3), 100);
            PlayerNormalAttack(player, target, -1, 0, false);
        }

        private void EnemySkillDoubleSlash(MainCharacter player, MainCharacter target, bool ignoreDefense)
        {
            // 相手：カウンターアタックが入っている場合
            if (target.CurrentCounterAttack > 0)
            {
                // 自分：相手の防御体制を無視できる場合
                if (ignoreDefense)
                {
                    UpdateBattleText(player.GetCharacterSentence(110));
                }
                else
                {
                    // 自分：NothingOfNothingnessによる無効化が張ってある場合
                    if (player.CurrentNothingOfNothingness > 0)
                    {
                        UpdateBattleText(player.Name + "が" + target.Name + "へDoubleSlashを仕掛ける所で・・・\r\n", 500);
                        UpdateBattleText(target.GetCharacterSentence(114), 1000);
                        UpdateBattleText(player.Name + "は無効化オーラによって護られている！\r\n");
                        //target.CurrentStanceOfEyes = false;
                        target.RemoveStanceOfEyes(); // 後編編集
                        //target.CurrentNegate = false;
                        target.RemoveNegate(); // 後編編集
                        //target.CurrentCounterAttack = false;
                        target.RemoveCounterAttack(); // 後編編集
                    }
                    else
                    {
                        UpdateBattleText(player.Name + "が" + target.Name + "へDoubleSlashを仕掛ける所で・・・\r\n", 1000);
                        UpdateBattleText(target.GetCharacterSentence(113));
                        PlayerNormalAttack(target, player, -1, 0, false);
                        return;
                    }
                }
            }

            UpdateBattleText(player.GetCharacterSentence(2));
            EnemyNormalAttack(target);
            UpdateBattleText(player.GetCharacterSentence(3), 100);
            EnemyNormalAttack(target);
        }

        private void PlayerSkillCrushingBlow(MainCharacter player, EnemyCharacter1 ec1)
        {
            PlayerNormalAttack(player, ec1, -1, 2, false);
        }

        private void PlayerSkillSoulInfinity(MainCharacter player, MainCharacter target, float powerUpValue, bool ignoreDefense)
        {
            // 相手：カウンターアタックが入っている場合
            if (target.CurrentCounterAttack > 0)
            {
                // 自分：相手の防御体制を無視できる場合
                if (ignoreDefense)
                {
                    UpdateBattleText(player.GetCharacterSentence(110));
                }
                else
                {
                    // 自分：NothingOfNothingnessによる無効化が張ってある場合
                    if (player.CurrentNothingOfNothingness > 0)
                    {
                        UpdateBattleText(player.Name + "が" + target.Name + "へSoulInfinityを仕掛ける所で・・・\r\n", 500);
                        UpdateBattleText(target.GetCharacterSentence(114), 1000);
                        UpdateBattleText(player.Name + "は無効化オーラによって護られている！\r\n");
                        //target.CurrentStanceOfEyes = false;
                        target.RemoveStanceOfEyes(); // 後編編集
                        //target.CurrentNegate = false;
                        target.RemoveNegate(); // 後編編集
                        //target.CurrentCounterAttack = false;
                        target.RemoveCounterAttack(); // 後編編集
                    }
                    else
                    {
                        UpdateBattleText(player.Name + "が" + target.Name + "へSoulInfinityを仕掛ける所で・・・\r\n", 1000);
                        UpdateBattleText(target.GetCharacterSentence(113));
                        PlayerNormalAttack(target, player, -1, 0, false);
                        return;
                    }
                }
            }

            // Gloryによる効果
            if (player.CurrentGlory > 0)
            {
                player.Target = player;
                PlayerSpellFreshHeal(player);
                player.Target = target;
            }

            Random rd = new Random(DateTime.Now.Millisecond);
            int atk = rd.Next(((player.TotalStrength) * (player.TotalAgility) +
                              (player.TotalAgility) * (player.TotalIntelligence) +
                              (player.TotalIntelligence) * (player.TotalMind) +
                              (player.TotalMind) * (player.TotalStrength) +
                              (player.MainWeapon.MinValue) * (player.TotalMind)) / player.Level * 2,
                              ((player.TotalStrength) * (player.TotalAgility) +
                              (player.TotalAgility) * (player.TotalIntelligence) +
                              (player.TotalIntelligence) * (player.TotalMind) +
                              (player.TotalMind) * (player.TotalStrength) +
                              (player.MainWeapon.MaxValue) * (player.TotalMind)) / player.Level * 2);

            if (player.CurrentSaintPower > 0)
            {
                atk = (int)((float)atk * 1.5F);
            }
            if (player.CurrentEternalPresence > 0)
            {
                atk = (int)((float)atk * 1.3F);
            }
            if (player.CurrentAetherDrive > 0)
            {
                atk = (int)((float)atk * 2.0F);
            }
            if (target.CurrentProtection > 0 && player.CurrentTruthVision <= 0)
            {
                atk = (int)((float)atk / 1.2F);
            }
            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    atk = (int)((float)atk / 3.0F);
                }
            }
            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(ec1.GetCharacterSentence(88));
                }
                else
                {
                    atk = 0;
                }
            }

            if (powerUpValue > 0)
            {
                atk = (int)((float)atk * powerUpValue);
            }

            // Deflectionによる効果
            if (target.CurrentDeflection > 0)
            {
                player.CurrentLife -= atk;
                UpdateLife(player);
                UpdateBattleText(String.Format(target.GetCharacterSentence(61), atk.ToString(), player.Name), 1000);

                target.CurrentDeflection = 0;
                target.pbDeflection.Image = null;
                target.pbDeflection.Update();
                return;
            }

            // Deflectionによる効果
            if (target.CurrentDeflection > 0)
            {
                player.CurrentLife -= atk;
                UpdateLife(player);
                UpdateBattleText(String.Format(target.GetCharacterSentence(61), atk.ToString(), player.Name), 1000);

                target.CurrentDeflection = 0;
                target.pbDeflection.Image = null;
                target.pbDeflection.Update();
                return;
            }

            // クリティカル判定
            bool detectCritical = false;
            Random rd2 = new Random(DateTime.Now.Millisecond);
            int result = rd.Next(1, 1001);
            if (player.CurrentWordOfFortune == 1)
            {
                result = 1;
            }
            if (result < 7 + (player.TotalAgility))
            {
                detectCritical = true;
                atk = (int)((float)atk * 3.0F);
            }

            if (CheckDodge(target)) { return; }

            target.CurrentLife -= atk;
            UpdateLife(target);
            UpdateBattleText(player.GetCharacterSentence(73));
            GroundOne.PlaySoundEffect("Catastrophe.mp3");
            if (detectCritical)
            {
                UpdateBattleText(player.GetCharacterSentence(117));
            }
            UpdateBattleText(player.Name + "のSoulInfinityが炸裂！" + atk.ToString() + "のダメージ\r\n");

            // FlameAuraによる追加攻撃
            if (player.CurrentFlameAura > 0)
            {
                int additional = rd.Next(player.TotalIntelligence, player.TotalIntelligence * 3 + 10);
                if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                {
                    if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(88));
                    }
                    else
                    {
                        additional = (int)((float)additional / 3.0F);
                    }
                }
                if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                {
                    if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(88));
                    }
                    else
                    {
                        additional = 0;
                    }
                }
                target.CurrentLife -= additional;
                UpdateLife(target);
                UpdateBattleText(String.Format(player.GetCharacterSentence(14), additional.ToString()));
            }

            // ImmortalRaveによる追加攻撃
            if (player.CurrentImmortalRave == 3)
            {
                PlayerSpellFireBall(player, target, 500);
            }
            else if (player.CurrentImmortalRave == 2)
            {
                PlayerSpellFlameStrike(player, target, 500);
            }
            else if (player.CurrentImmortalRave == 1)
            {
                PlayerSpellVolcanicWave(player, target, 500);
            }
        }
        #endregion

        #region "静"

        private void PlayerSkillCounterAttack(MainCharacter player, EnemyCharacter1 ec1)
        {
            UpdateBattleText(player.Name + "はカウンターの構えをとっている。\r\n");
        }
        
        private void PlayerSkillPurePurification(MainCharacter player)
        {
            UpdateBattleText(player.GetCharacterSentence(78));
            player.CurrentStunning = 0;
            player.CurrentSilence = 0;
            player.CurrentPoison = 0;
            player.CurrentTemptation = 0;
            player.CurrentFrozen = 0;
            player.CurrentParalyze = 0;
            player.pbStun.Image = null;
            player.pbStun.Update();
            player.pbSilence.Image = null;
            player.pbSilence.Update();
            player.pbPoison.Image = null;
            player.pbPoison.Update();
            player.pbTemptation.Image = null;
            player.pbTemptation.Update();
            player.pbFrozen.Image = null;
            player.pbFrozen.Update();
            player.pbParalyze.Image = null;
            player.pbParalyze.Update();
            GroundOne.PlaySoundEffect("Cleansing.mp3");
            UpdateBattleText(player.Name + "にかかっている負の影響が全て取り払われた。\r\n");
        }
        
        private void PlayerSkillAntiStun(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("AntiStun.mp3");
            player.Target = target;
            PlayerBuffAbstract(player, 999, "AntiStun.bmp");
        }

        private void PlayerSkillStanceOfDeath(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("StanceOfDeath.mp3");
            player.Target = target;
            PlayerBuffAbstract(player, 999, "StanceOfDeath.bmp");
        }

        #endregion

        #region "柔"
        private void PlayerSkillStanceOfFlow(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("StanceOfFlow.mp3");
            player.CurrentStanceOfFlow = 4; // ３ターン継続のためには、初期値は３＋１
            player.pbStanceOfFlow.Image = Image.FromFile(Database.BaseResourceFolder + "StanceOfFlow.bmp");
            player.pbStanceOfFlow.Update();
            UpdateBattleText(player.GetCharacterSentence(64));
        }

        private void PlayerSkillEnigmaSence(MainCharacter player, MainCharacter target, bool ignoreDefense)
        {
            // 相手：カウンターアタックが入っている場合
            if (target.CurrentCounterAttack > 0)
            {
                // 自分：相手の防御体制を無視できる場合
                if (ignoreDefense)
                {
                    UpdateBattleText(player.GetCharacterSentence(110));
                }
                else
                {
                    // 自分：NothingOfNothingnessによる無効化が張ってある場合
                    if (player.CurrentNothingOfNothingness > 0)
                    {
                        UpdateBattleText(player.Name + "が" + target.Name + "へEnigmaSenceを仕掛ける所で・・・\r\n", 500);
                        UpdateBattleText(target.GetCharacterSentence(114), 1000);
                        UpdateBattleText(player.Name + "は無効化オーラによって護られている！\r\n");
                        //target.CurrentStanceOfEyes = false;
                        target.RemoveStanceOfEyes(); // 後編編集
                        //target.CurrentNegate = false;
                        target.RemoveNegate(); // 後編編集
                        //target.CurrentCounterAttack = false;
                        target.RemoveCounterAttack(); // 後編編集
                    }
                    else
                    {
                        UpdateBattleText(player.Name + "が" + target.Name + "へEnigmaSenceを仕掛ける所で・・・\r\n", 1000);
                        UpdateBattleText(target.GetCharacterSentence(113));
                        PlayerNormalAttack(target, player, -1, 0, false);
                        return;
                    }
                }
            }

            UpdateBattleText(player.GetCharacterSentence(72));
            int atkBase = Math.Max(player.TotalStrength,
                          Math.Max(player.TotalAgility,
                                   player.TotalIntelligence));
            PlayerNormalAttack(player, target, 0, 0, false, atkBase);
        }

        private void PlayerSkillSilentRush(MainCharacter player, MainCharacter target, bool ignoreDefense)
        {
            // 相手：カウンターアタックが入っている場合
            if (target.CurrentCounterAttack > 0)
            {
                // 自分：相手の防御体制を無視できる場合
                if (ignoreDefense)
                {
                    UpdateBattleText(player.GetCharacterSentence(110));
                }
                else
                {
                    // 自分：NothingOfNothingnessによる無効化が張ってある場合
                    if (player.CurrentNothingOfNothingness > 0)
                    {
                        UpdateBattleText(player.Name + "が" + target.Name + "へSilentRushを仕掛ける所で・・・\r\n", 500);
                        UpdateBattleText(target.GetCharacterSentence(114), 1000);
                        UpdateBattleText(player.Name + "は無効化オーラによって護られている！\r\n");
                        //target.CurrentStanceOfEyes = false;
                        target.RemoveStanceOfEyes(); // 後編編集
                        //target.CurrentNegate = false;
                        target.RemoveNegate(); // 後編編集
                        //target.CurrentCounterAttack = false;
                        target.RemoveCounterAttack(); // 後編編集
                    }
                    else
                    {
                        UpdateBattleText(player.Name + "が" + target.Name + "へSilentRushを仕掛ける所で・・・\r\n", 1000);
                        UpdateBattleText(target.GetCharacterSentence(113));
                        PlayerNormalAttack(target, player, -1, 0, false);
                        return;
                    }
                }
            }

            for (int ii = 0; ii < 3; ii++)
            {
                // Gloryによる効果
                if (player.CurrentGlory > 0)
                {
                    player.Target = player;
                    PlayerSpellFreshHeal(player);
                    player.Target = target;
                }

                Random rd = new Random(DateTime.Now.Millisecond * Environment.TickCount);
                int effectValue = rd.Next((player.TotalStrength) + (player.TotalAgility) * 1 + player.MainWeapon.MinValue,
                                          (player.TotalStrength) + (player.TotalAgility) * 2 + player.MainWeapon.MaxValue);
                if (player.CurrentSaintPower > 0)
                {
                    effectValue = (int)((float)effectValue * 1.5F);
                }
                if (player.CurrentEternalPresence > 0)
                {
                    effectValue = (int)((float)effectValue * 1.3F);
                }
                if (player.CurrentAetherDrive > 0)
                {
                    effectValue = (int)((float)effectValue * 2.0F);
                }
                if (target.CurrentProtection > 0)
                {
                    effectValue = (int)((float)effectValue / 1.2F);
                }
                if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                {
                    if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(88));
                    }
                    else
                    {
                        effectValue = (int)((float)effectValue / 3.0F);
                    }
                }
                if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                {
                    if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(88));
                    }
                    else
                    {
                        effectValue = 0;
                    }
                }

                // Deflectionによる効果
                if (target.CurrentDeflection > 0)
                {
                    player.CurrentLife -= effectValue;
                    UpdateLife(player);
                    UpdateBattleText(String.Format(target.GetCharacterSentence(61), effectValue.ToString(), player.Name), 1000);

                    target.CurrentDeflection = 0;
                    target.pbDeflection.Image = null;
                    target.pbDeflection.Update();
                    continue;
                }

                // クリティカル判定
                bool detectCritical = false;
                Random rd2 = new Random(DateTime.Now.Millisecond);
                int result = rd.Next(1, 1001);
                if (player.CurrentWordOfFortune == 1)
                {
                    result = 1;
                }
                if (result < 7 + (player.TotalAgility))
                {
                    detectCritical = true;
                    effectValue = (int)((float)effectValue * 3.0F);
                }

                if (CheckDodge(target)) { continue; }

                target.CurrentLife -= effectValue;
                UpdateLife(target);
                if (detectCritical)
                {
                    UpdateBattleText(player.GetCharacterSentence(117));
                }
                switch (ii)
                {
                    case 0:
                        GroundOne.PlaySoundEffect("Hit01.mp3");
                        UpdateBattleText(String.Format(player.GetCharacterSentence(89), effectValue.ToString()), 500);
                        break;
                    case 1:
                        GroundOne.PlaySoundEffect("Hit01.mp3");
                        UpdateBattleText(String.Format(player.GetCharacterSentence(90), effectValue.ToString()), 200);
                        break;
                    case 2:
                        GroundOne.PlaySoundEffect("Hit01.mp3");
                        UpdateBattleText(String.Format(player.GetCharacterSentence(91), effectValue.ToString()), 1000);
                        break;
                }

                // FlameAuraによる追加攻撃
                if (player.CurrentFlameAura > 0)
                {
                    int additional = rd.Next(player.Intelligence, player.Intelligence * 3 + 10);
                    if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                    {
                        if (target.CurrentAbsoluteZero > 0)
                        {
                            UpdateBattleText(target.GetCharacterSentence(88));
                        }
                        else
                        {
                            additional = (int)((float)effectValue / 3.0F);
                        }
                    }
                    if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                    {
                        if (target.CurrentAbsoluteZero > 0)
                        {
                            UpdateBattleText(target.GetCharacterSentence(88));
                        }
                        else
                        {
                            additional = 0;
                        }
                    }
                    target.CurrentLife -= additional;
                    UpdateLife(target);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(14), additional.ToString()));
                }

                // ImmortalRaveによる追加攻撃
                if (player.CurrentImmortalRave == 3)
                {
                    PlayerSpellFireBall(player, target, 500);
                }
                else if (player.CurrentImmortalRave == 2)
                {
                    PlayerSpellFlameStrike(player, target, 500);
                }
                else if (player.CurrentImmortalRave == 1)
                {
                    PlayerSpellVolcanicWave(player, target, 500);
                }
            }
        }

        private void PlayerSkillOboroImpact(MainCharacter player, MainCharacter target, float powerUpValue, bool ignoreDefense)
        {
            // 相手：カウンターアタックが入っている場合
            if (target.CurrentCounterAttack > 0)
            {
                // 自分：相手の防御体制を無視できる場合
                if (ignoreDefense)
                {
                    UpdateBattleText(player.GetCharacterSentence(110));
                }
                else
                {
                    // 自分：NothingOfNothingnessによる無効化が張ってある場合
                    if (player.CurrentNothingOfNothingness > 0)
                    {
                        UpdateBattleText(player.Name + "が" + target.Name + "へOboroImpactを仕掛ける所で・・・\r\n", 500);
                        UpdateBattleText(target.GetCharacterSentence(114), 1000);
                        UpdateBattleText(player.Name + "は無効化オーラによって護られている！\r\n");
                        //target.CurrentStanceOfEyes = false;
                        target.RemoveStanceOfEyes(); // 後編編集
                        //target.CurrentNegate = false;
                        target.RemoveNegate(); // 後編編集
                        //target.CurrentCounterAttack = false;
                        target.RemoveCounterAttack(); // 後編編集
                    }
                    else
                    {
                        UpdateBattleText(player.Name + "が" + target.Name + "へOboroImpactを仕掛ける所で・・・\r\n", 1000);
                        UpdateBattleText(target.GetCharacterSentence(113));
                        PlayerNormalAttack(target, player, -1, 0, false);
                        return;
                    }
                }
            }

            // Gloryによる効果
            if (player.CurrentGlory > 0)
            {
                player.Target = player;
                PlayerSpellFreshHeal(player);
                player.Target = target;
            }

            Random rd = new Random(DateTime.Now.Millisecond);
            int effectValue = rd.Next((player.TotalStrength * player.TotalAgility * player.TotalIntelligence * player.TotalMind * 1) / player.Level / 1000,
                                      (player.TotalStrength * player.TotalAgility * player.TotalIntelligence * player.TotalMind * 2) / player.Level / 1000);
            if (player.CurrentSaintPower > 0)
            {
                effectValue = (int)((float)effectValue * 1.5F);
            }
            if (player.CurrentEternalPresence > 0)
            {
                effectValue = (int)((float)effectValue * 1.3F);
            }
            if (player.CurrentAetherDrive > 0)
            {
                effectValue = (int)((float)effectValue * 2.0F);
            }
            if (target.CurrentProtection > 0)
            {
                effectValue = (int)((float)effectValue / 1.2F);
            }
            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = (int)((float)effectValue / 3.0F);
                }
            }
            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = 0;
                }
            }
            if (powerUpValue > 0)
            {
                effectValue = (int)((float)effectValue * powerUpValue);
            }

            // Deflectionによる効果
            if (target.CurrentDeflection > 0)
            {
                player.CurrentLife -= effectValue;
                UpdateLife(player);
                UpdateBattleText(String.Format(target.GetCharacterSentence(61), effectValue.ToString(), player.Name), 1000);

                target.CurrentDeflection = 0;
                target.pbDeflection.Image = null;
                target.pbDeflection.Update();
                return;
            }

            // クリティカル判定
            bool detectCritical = false;
            Random rd2 = new Random(DateTime.Now.Millisecond);
            int result = rd.Next(1, 1001);
            if (player.CurrentWordOfFortune == 1)
            {
                result = 1;
            }
            if (result < 7 + (player.TotalAgility))
            {
                detectCritical = true;
                effectValue = (int)((float)effectValue * 3.0F);
            }

            if (CheckDodge(target)) { return; }

            target.CurrentLife -= effectValue;
            UpdateLife(target);
            UpdateBattleText(player.GetCharacterSentence(96), 1000);
            GroundOne.PlaySoundEffect("Catastrophe.mp3");
            if (detectCritical)
            {
                UpdateBattleText(player.GetCharacterSentence(117));
            }
            UpdateBattleText(String.Format(player.GetCharacterSentence(97), target.Name, effectValue));

            // FlameAuraによる追加攻撃
            if (player.CurrentFlameAura > 0)
            {
                int additional = rd.Next(player.TotalIntelligence, player.TotalIntelligence * 3 + 10);
                if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                {
                    if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(88));
                    }
                    else
                    {
                        additional = (int)((float)additional / 3.0F);
                    }
                }
                if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                {
                    if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(88));
                    }
                    else
                    {
                        additional = 0;
                    }
                }
                target.CurrentLife -= additional;
                UpdateLife(target);
                UpdateBattleText(String.Format(player.GetCharacterSentence(14), additional.ToString()));
            }

            // ImmortalRaveによる追加攻撃
            if (player.CurrentImmortalRave == 3)
            {
                PlayerSpellFireBall(player, target, 500);
            }
            else if (player.CurrentImmortalRave == 2)
            {
                PlayerSpellFlameStrike(player, target, 500);
            }
            else if (player.CurrentImmortalRave == 1)
            {
                PlayerSpellVolcanicWave(player, target, 500);
            }
        }
        #endregion

        #region "剛"
        
        private void PlayerSkillStanceOfStanding(MainCharacter player, MainCharacter target)
        {
            UpdateBattleText(player.GetCharacterSentence(56));
            PlayerNormalAttack(player, target, 0, 0, false);
        }

        private void PlayerSkillInnerInspiration(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("InnerInspiration.mp3");
            Random rd = new Random(DateTime.Now.Millisecond);
            int result = rd.Next(15, 15 + 1 + (int)((float)player.TotalMind / (float)3.0F));
            UpdateBattleText(String.Format(player.GetCharacterSentence(51), result.ToString()));
            player.CurrentSkillPoint += result;
            UpdateMCSkillPoint(player);
        }

        private void PlayerSkillKineticSmash(MainCharacter player, MainCharacter target, bool ignoreDefense)
        {
            // 相手：カウンターアタックが入っている場合
            if (target.CurrentCounterAttack > 0)
            {
                // 自分：相手の防御体制を無視できる場合
                if (ignoreDefense)
                {
                    UpdateBattleText(player.GetCharacterSentence(110));
                }
                else
                {
                    // 自分：NothingOfNothingnessによる無効化が張ってある場合
                    if (player.CurrentNothingOfNothingness > 0)
                    {
                        UpdateBattleText(player.Name + "が" + target.Name + "へKineticSmashを仕掛ける所で・・・\r\n", 500);
                        UpdateBattleText(target.GetCharacterSentence(114), 1000);
                        UpdateBattleText(player.Name + "は無効化オーラによって護られている！\r\n");
                        //target.CurrentStanceOfEyes = false;
                        target.RemoveStanceOfEyes(); // 後編編集
                        //target.CurrentNegate = false;
                        target.RemoveNegate(); // 後編編集
                        //target.CurrentCounterAttack = false;
                        target.RemoveCounterAttack(); // 後編編集
                    }
                    else
                    {
                        UpdateBattleText(player.Name + "が" + target.Name + "へKineticSmashを仕掛ける所で・・・\r\n", 1000);
                        UpdateBattleText(target.GetCharacterSentence(113));
                        PlayerNormalAttack(target, player, -1, 0, false);
                        return;
                    }
                }
            }

            // Gloryによる効果
            if (player.CurrentGlory > 0)
            {
                player.Target = player;
                PlayerSpellFreshHeal(player);
                player.Target = target;
            }

            Random rd = new Random(DateTime.Now.Millisecond);
            int atk = rd.Next((int)((float)(player.TotalStrength + player.MainWeapon.MinValue) *
                              (float)(player.TotalMind) / ((float)(10.0F))),
                              (int)((float)(player.TotalStrength + player.MainWeapon.MaxValue) *
                              (float)(player.TotalMind) / ((float)(10.0F))));
            if (player.CurrentSaintPower > 0)
            {
                atk = (int)((float)atk * 1.5F);
            }
            if (player.CurrentEternalPresence > 0)
            {
                atk = (int)((float)atk * 1.3F);
            }
            if (player.CurrentAetherDrive > 0)
            {
                atk = (int)((float)atk * 2.0F);
            }
            if (target.CurrentProtection > 0 && player.CurrentTruthVision <= 0)
            {
                atk = (int)((float)atk / 1.2F);
            }
            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(ec1.GetCharacterSentence(88));
                }
                else
                {
                    atk = (int)((float)atk / 3.0F);
                }
            }
            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(ec1.GetCharacterSentence(88));
                }
                else
                {
                    atk = 0;
                }
            }

            // Deflectionによる効果
            if (target.CurrentDeflection > 0)
            {
                player.CurrentLife -= atk;
                UpdateLife(player);
                UpdateBattleText(String.Format(target.GetCharacterSentence(61), atk.ToString(), player.Name), 1000);

                target.CurrentDeflection = 0;
                target.pbDeflection.Image = null;
                target.pbDeflection.Update();
                return;
            }

            // クリティカル判定
            bool detectCritical = false;
            Random rd2 = new Random(DateTime.Now.Millisecond);
            int result = rd.Next(1, 1001);
            if (player.CurrentWordOfFortune == 1)
            {
                result = 1;
            }
            if (result < 7 + (player.TotalAgility))
            {
                detectCritical = true;
                atk = (int)((float)atk * 3.0F);
            }

            if (CheckDodge(target)) { return; }

            target.CurrentLife -= atk;
            UpdateLife(target);
            UpdateBattleText(player.GetCharacterSentence(74));
            GroundOne.PlaySoundEffect("KineticSmash.mp3");

            if (detectCritical)
            {
                UpdateBattleText(player.GetCharacterSentence(117));
            }
            UpdateBattleText(player.Name + "のキネティック・スマッシュがヒット。" + target.Name + "へ" + atk.ToString() + "のダメージ\r\n");

            // FlameAuraによる追加攻撃
            if (player.CurrentFlameAura > 0)
            {
                int additional = rd.Next(player.TotalIntelligence, player.TotalIntelligence * 3 + 10);
                if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                {
                    if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(88));
                    }
                    else
                    {
                        additional = (int)((float)additional / 3.0F);
                    }
                }
                if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                {
                    if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(88));
                    }
                    else
                    {
                        additional = 0;
                    }
                }
                target.CurrentLife -= additional;
                UpdateLife(target);
                UpdateBattleText(String.Format(player.GetCharacterSentence(14), additional.ToString()));
            }

            // ImmortalRaveによる追加攻撃
            if (player.CurrentImmortalRave == 3)
            {
                PlayerSpellFireBall(player, target, 500);
            }
            else if (player.CurrentImmortalRave == 2)
            {
                PlayerSpellFlameStrike(player, target, 500);
            }
            else if (player.CurrentImmortalRave == 1)
            {
                PlayerSpellVolcanicWave(player, target, 500);
            }
        }

        private void PlayerSkillCatastrophe(MainCharacter player, MainCharacter target, bool ignoreDefense)
        {
            // 相手：カウンターアタックが入っている場合
            if (target.CurrentCounterAttack > 0)
            {
                // 自分：相手の防御体制を無視できる場合
                if (ignoreDefense)
                {
                    UpdateBattleText(player.GetCharacterSentence(110));
                }
                else
                {
                    // 自分：NothingOfNothingnessによる無効化が張ってある場合
                    if (player.CurrentNothingOfNothingness > 0)
                    {
                        UpdateBattleText(player.Name + "が" + target.Name + "へCatastropheを仕掛ける所で・・・\r\n", 500);
                        UpdateBattleText(target.GetCharacterSentence(114), 1000);
                        UpdateBattleText(player.Name + "は無効化オーラによって護られている！\r\n");
                        //target.CurrentStanceOfEyes = false;
                        target.RemoveStanceOfEyes(); // 後編編集
                        //target.CurrentNegate = false;
                        target.RemoveNegate(); // 後編編集
                        //target.CurrentCounterAttack = false;
                        target.RemoveCounterAttack(); // 後編編集
                    }
                    else
                    {
                        UpdateBattleText(player.Name + "が" + target.Name + "へCatastropheを仕掛ける所で・・・\r\n", 1000);
                        UpdateBattleText(target.GetCharacterSentence(113));
                        PlayerNormalAttack(target, player, -1, 0, false);
                        return;
                    }
                }
            }

            // Gloryによる効果
            if (player.CurrentGlory > 0)
            {
                player.Target = player;
                PlayerSpellFreshHeal(player);
                player.Target = target;
            }

            Random rd = new Random(DateTime.Now.Millisecond);
            int effectValue = (int)((float)((float)(1.0f / (float)(player.TotalStrength))
                            + (float)(1.0f / (float)(player.TotalAgility))
                            + (float)(1.0f / (float)(player.TotalIntelligence)))
                            * (float)(player.TotalMind) * (float)(player.TotalMind) * (float)(player.CurrentSkillPoint) / 10.0f);
            //(player.TotalStrength + player.TotalAgility + player.TotalIntelligence) * player.TotalMind * player.CurrentSkillPoint / 100;
            if (player.CurrentSaintPower > 0)
            {
                effectValue = (int)((float)effectValue * 1.5F);
            }
            if (player.CurrentEternalPresence > 0)
            {
                effectValue = (int)((float)effectValue * 1.3F);
            }
            if (player.CurrentAetherDrive > 0)
            {
                effectValue = (int)((float)effectValue * 2.0F);
            }
            if (target.CurrentProtection > 0)
            {
                effectValue = (int)((float)effectValue / 1.2F);
            }
            if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = (int)((float)effectValue / 3.0F);
                }
            }
            if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
            {
                if (target.CurrentAbsoluteZero > 0)
                {
                    UpdateBattleText(target.GetCharacterSentence(88));
                }
                else
                {
                    effectValue = 0;
                }
            }

            // Deflectionによる効果
            if (target.CurrentDeflection > 0)
            {
                player.CurrentLife -= effectValue;
                UpdateLife(player);
                UpdateBattleText(String.Format(target.GetCharacterSentence(61), effectValue.ToString(), player.Name), 1000);

                target.CurrentDeflection = 0;
                target.pbDeflection.Image = null;
                target.pbDeflection.Update();
                return;
            }

            // クリティカル判定
            bool detectCritical = false;
            Random rd2 = new Random(DateTime.Now.Millisecond);
            int result = rd.Next(1, 1001);
            if (player.CurrentWordOfFortune == 1)
            {
                result = 1;
            }
            if (result < 7 + (player.TotalAgility))
            {
                detectCritical = true;
                effectValue = (int)((float)effectValue * 3.0F);
            }

            if (CheckDodge(target)) { return; }

            target.CurrentLife -= effectValue;
            UpdateLife(target);
            UpdateBattleText(player.GetCharacterSentence(98), 1000);
            GroundOne.PlaySoundEffect("Catastrophe.mp3");
            if (detectCritical)
            {
                UpdateBattleText(player.GetCharacterSentence(117));
            }
            UpdateBattleText(String.Format(player.GetCharacterSentence(99), effectValue));

            // FlameAuraによる追加攻撃
            if (player.CurrentFlameAura > 0)
            {
                int additional = rd.Next(player.TotalIntelligence, player.TotalIntelligence * 3 + 10);
                if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                {
                    if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(88));
                    }
                    else
                    {
                        additional = (int)((float)additional / 3.0F);
                    }
                }
                if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                {
                    if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(88));
                    }
                    else
                    {
                        additional = 0;
                    }
                }
                target.CurrentLife -= additional;
                UpdateLife(target);
                UpdateBattleText(String.Format(player.GetCharacterSentence(14), additional.ToString()));
            }

            // ImmortalRaveによる追加攻撃
            if (player.CurrentImmortalRave == 3)
            {
                PlayerSpellFireBall(player, target, 500);
            }
            else if (player.CurrentImmortalRave == 2)
            {
                PlayerSpellFlameStrike(player, target, 500);
            }
            else if (player.CurrentImmortalRave == 1)
            {
                PlayerSpellVolcanicWave(player, target, 500);
            }
        }

        #endregion

        #region "心眼"
        private void PlayerSkillTruthVision(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("TruthVision.mp3");
            player.Target = target;
            PlayerBuffAbstract(player, 999, "TruthVision.bmp");
        }

        private void PlayerSkillHighEmotionality(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("HighEmotionality.mp3");
            player.Target = player;
            PlayerBuffAbstract(player, 4, "HighEmotionality.bmp");
        }
        
        private void PlayerSkillStanceOfEyes(MainCharacter player, MainCharacter target)
        {
            player.Target = target;
            player.CurrentStanceOfEyes = 999; // 後編編集
            UpdateBattleText(player.GetCharacterSentence(100));
        }

        private void PlayerSkillPainfulInsanity(MainCharacter player)
        {
            GroundOne.PlaySoundEffect("PainfulInsanity.mp3");
            //PlayerBuffAbstract(player, 999, "PainfulInsanity.bmp");
            player.CurrentPainfulInsanity = 999;
            player.pbPainfulInsanity.Image = Image.FromFile(Database.BaseResourceFolder + "PainfulInsanity.bmp");
            player.pbPainfulInsanity.Update();
            UpdateBattleText(player.GetCharacterSentence(4));
        }

        #endregion

        #region "無心"

        private void PlayerSkillNegate(MainCharacter player, MainCharacter target)
        {
            player.Target = target;
            //player.CurrentNegate = true;
            player.CurrentNegate = 1; // 後編編集
            UpdateBattleText(player.GetCharacterSentence(103));
        }

        private void PlayerSkillVoidExtraction(MainCharacter player)
        {
            string fileExt = ".bmp";
            GroundOne.PlaySoundEffect("VoidExtraction.mp3");
            int effectValue = Math.Max(player.Strength, Math.Max(player.Agility, Math.Max(player.Intelligence, player.Mind)));
            string maxParameter = String.Empty;
            if (effectValue == player.Strength)
            {
                if ((effectValue - player.BuffStrength_VoidExtraction) > 0)
                {
                    player.CurrentVoidExtraction = 999;
                    player.pbVoidExtraction.Image = Image.FromFile(Database.BaseResourceFolder + Database.VOID_EXTRACTION + fileExt);
                    player.pbVoidExtraction.Update();
                    UpdateBattleText(String.Format(player.GetCharacterSentence(79), "力", (effectValue - player.BuffStrength_VoidExtraction)));
                    player.BuffStrength_VoidExtraction += effectValue;
                }
                else
                {
                    UpdateBattleText(player.GetCharacterSentence(82));
                }
            }
            else if (effectValue == player.Agility)
            {
                if ((effectValue - player.BuffAgility_VoidExtraction) > 0)
                {
                    player.CurrentVoidExtraction = 999;
                    player.pbVoidExtraction.Image = Image.FromFile(Database.BaseResourceFolder + Database.VOID_EXTRACTION + fileExt);
                    player.pbVoidExtraction.Update();
                    UpdateBattleText(String.Format(player.GetCharacterSentence(79), "技", (effectValue - player.BuffAgility_VoidExtraction)));
                    player.BuffAgility_VoidExtraction += effectValue;
                }
                else
                {
                    UpdateBattleText(player.GetCharacterSentence(82));
                }
            }
            else if (effectValue == player.Intelligence)
            {
                if ((effectValue - player.BuffIntelligence_VoidExtraction) > 0)
                {
                    player.CurrentVoidExtraction = 999;
                    player.pbVoidExtraction.Image = Image.FromFile(Database.BaseResourceFolder + Database.VOID_EXTRACTION + fileExt);
                    player.pbVoidExtraction.Update();
                    UpdateBattleText(String.Format(player.GetCharacterSentence(79), "知", (effectValue - player.BuffIntelligence_VoidExtraction)));
                    player.BuffIntelligence_VoidExtraction += effectValue;
                }
                else
                {
                    UpdateBattleText(player.GetCharacterSentence(82));
                }
            }
            else
            {
                if ((effectValue - player.Mind) > 0)
                {
                    player.CurrentVoidExtraction = 999;
                    player.pbVoidExtraction.Image = Image.FromFile(Database.BaseResourceFolder + Database.VOID_EXTRACTION + fileExt);
                    player.pbVoidExtraction.Update();
                    UpdateBattleText(String.Format(player.GetCharacterSentence(79), "心", (effectValue - player.BuffMind_VoidExtraction)));
                    player.BuffMind_VoidExtraction += effectValue;
                }
                else
                {
                    UpdateBattleText(player.GetCharacterSentence(82));
                }
            }
        }

        private void PlayerSkillCarnageRush(MainCharacter player, MainCharacter target, bool ignoreDefense)
        {
            // 相手：カウンターアタックが入っている場合
            if (target.CurrentCounterAttack > 0)
            {
                // 自分：相手の防御体制を無視できる場合
                if (ignoreDefense)
                {
                    UpdateBattleText(player.GetCharacterSentence(110));
                }
                else
                {
                    // 自分：NothingOfNothingnessによる無効化が張ってある場合
                    if (player.CurrentNothingOfNothingness > 0)
                    {
                        UpdateBattleText(player.Name + "が" + target.Name + "へCarnageRushを仕掛ける所で・・・\r\n", 500);
                        UpdateBattleText(target.GetCharacterSentence(114), 1000);
                        UpdateBattleText(player.Name + "は無効化オーラによって護られている！\r\n");
                        //target.CurrentStanceOfEyes = false;
                        target.RemoveStanceOfEyes(); // 後編編集
                        //target.CurrentNegate = false;
                        target.RemoveNegate(); // 後編編集
                        //target.CurrentCounterAttack = false;
                        target.RemoveCounterAttack(); // 後編編集
                    }
                    else
                    {
                        UpdateBattleText(player.Name + "が" + target.Name + "へCarnageRushを仕掛ける所で・・・\r\n", 1000);
                        UpdateBattleText(target.GetCharacterSentence(113));
                        PlayerNormalAttack(target, player, -1, 0, false);
                        return;
                    }
                }
            }

            for (int ii = 0; ii < 5; ii++)
            {
                // Gloryによる効果
                if (player.CurrentGlory > 0)
                {
                    player.Target = player;
                    PlayerSpellFreshHeal(player);
                    player.Target = target;
                }

                Random rd = new Random(DateTime.Now.Millisecond * Environment.TickCount);
                int effectValue = rd.Next((player.TotalStrength) + (player.TotalAgility) * 1 + player.MainWeapon.MinValue,
                                          (player.TotalStrength) + (player.TotalAgility) * 2 + player.MainWeapon.MaxValue);
                if (player.CurrentSaintPower > 0)
                {
                    effectValue = (int)((float)effectValue * 1.5F);
                }
                if (player.CurrentEternalPresence > 0)
                {
                    effectValue = (int)((float)effectValue * 1.3F);
                }
                if (player.CurrentAetherDrive > 0)
                {
                    effectValue = (int)((float)effectValue * 2.0F);
                }
                if (target.CurrentProtection > 0)
                {
                    effectValue = (int)((float)effectValue / 1.2F);
                }
                if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                {
                    if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(88));
                    }
                    else
                    {
                        effectValue = (int)((float)effectValue / 3.0F);
                    }
                }
                if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                {
                    if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(88));
                    }
                    else
                    {
                        effectValue = 0;
                    }
                }

                // Deflectionによる効果
                if (target.CurrentDeflection > 0)
                {
                    player.CurrentLife -= effectValue;
                    UpdateLife(player);
                    UpdateBattleText(String.Format(target.GetCharacterSentence(61), effectValue.ToString(), player.Name), 1000);

                    target.CurrentDeflection = 0;
                    target.pbDeflection.Image = null;
                    target.pbDeflection.Update();
                    continue;
                }

                if (CheckDodge(target)) { continue; }

                // クリティカル判定
                bool detectCritical = false;
                Random rd2 = new Random(DateTime.Now.Millisecond);
                int result = rd.Next(1, 1001);
                if (player.CurrentWordOfFortune == 1)
                {
                    result = 1;
                }
                if (result < 7 + (player.TotalAgility))
                {
                    detectCritical = true;
                    effectValue = (int)((float)effectValue * 3.0F);
                }

                target.CurrentLife -= effectValue;
                UpdateLife(target);
                if (detectCritical)
                {
                    UpdateBattleText(player.GetCharacterSentence(117));
                }
                switch (ii)
                {
                    case 0:
                        GroundOne.PlaySoundEffect("Hit01.mp3");
                        UpdateBattleText(String.Format(player.GetCharacterSentence(65), effectValue.ToString()), 500);
                        break;
                    case 1:
                        GroundOne.PlaySoundEffect("Hit01.mp3");
                        UpdateBattleText(String.Format(player.GetCharacterSentence(66), effectValue.ToString()), 50);
                        break;
                    case 2:
                        GroundOne.PlaySoundEffect("Hit01.mp3");
                        UpdateBattleText(String.Format(player.GetCharacterSentence(67), effectValue.ToString()), 50);
                        break;
                    case 3:
                        GroundOne.PlaySoundEffect("Hit01.mp3");
                        UpdateBattleText(String.Format(player.GetCharacterSentence(68), effectValue.ToString()), 50);
                        break;
                    case 4:
                        GroundOne.PlaySoundEffect("KineticSmash.mp3");
                        UpdateBattleText(String.Format(player.GetCharacterSentence(69), effectValue.ToString()), 500);
                        break;
                }

                // FlameAuraによる追加攻撃
                if (player.CurrentFlameAura > 0)
                {
                    int additional = rd.Next(player.Intelligence, player.Intelligence * 3 + 10);
                    if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                    {
                        if (target.CurrentAbsoluteZero > 0)
                        {
                            UpdateBattleText(target.GetCharacterSentence(88));
                        }
                        else
                        {
                            additional = (int)((float)effectValue / 3.0F);
                        }
                    }
                    if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                    {
                        if (target.CurrentAbsoluteZero > 0)
                        {
                            UpdateBattleText(target.GetCharacterSentence(88));
                        }
                        else
                        {
                            additional = 0;
                        }
                    }
                    target.CurrentLife -= additional;
                    UpdateLife(target);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(14), additional.ToString()));
                }

                // ImmortalRaveによる追加攻撃
                if (player.CurrentImmortalRave == 3)
                {
                    PlayerSpellFireBall(player, target, 500);
                }
                else if (player.CurrentImmortalRave == 2)
                {
                    PlayerSpellFlameStrike(player, target, 500);
                }
                else if (player.CurrentImmortalRave == 1)
                {
                    PlayerSpellVolcanicWave(player, target, 500);
                }
            }
        }

        private void PlayerSkillNothingOfNothingness(MainCharacter player, MainCharacter target)
        {
            GroundOne.PlaySoundEffect("NothingOfNothingness.mp3");
            player.Target = target;
            PlayerBuffAbstract(player, 999, "NothingOfNothingness.bmp");
        }

        #endregion
        #endregion

        #region "通常攻撃"
        private void PlayerNormalAttack(MainCharacter player)
        {
            PlayerNormalAttack(player, ec1, -1, 0, false);
        }

        private void PlayerNormalAttack(MainCharacter player, MainCharacter target, float powerUpValue, int crushingBlow, bool ignoreDefense)
        {
            PlayerNormalAttack(player, target, powerUpValue, crushingBlow, ignoreDefense, player.TotalStrength);
        }
        private void PlayerNormalAttack(MainCharacter player, MainCharacter target, float powerUpValue, int crushingBlow, bool ignoreDefense, int atkBase)
        {
            try
            {
                // 相手：カウンターアタックが入っている場合
                if (target.CurrentCounterAttack > 0)
                {
                    // 自分：相手の防御体制を無視できる場合
                    if (ignoreDefense)
                    {
                        UpdateBattleText(player.GetCharacterSentence(110));
                    }
                    else
                    {
                        // 自分：NothingOfNothingnessによる無効化が張ってある場合
                        if (player.CurrentNothingOfNothingness > 0)
                        {
                            UpdateBattleText(player.Name + "が" + target.Name + "へ攻撃を仕掛ける所で・・・\r\n", 500);
                            UpdateBattleText(target.GetCharacterSentence(114), 1000);
                            UpdateBattleText(player.Name + "は無効化オーラによって護られている！\r\n");
                            //target.CurrentStanceOfEyes = false;
                            target.RemoveStanceOfEyes(); // 後編編集
                            //target.CurrentNegate = false;
                            target.RemoveNegate(); // 後編編集
                            //target.CurrentCounterAttack = false;
                            target.RemoveCounterAttack(); // 後編編集
                        }
                        else
                        {
                            UpdateBattleText(player.Name + "が" + target.Name + "へ攻撃を仕掛ける所で・・・\r\n", 1000);
                            UpdateBattleText(target.GetCharacterSentence(113));
                            PlayerNormalAttack(target, player, -1, 0, false);
                            return;
                        }
                    }
                }

                // Gloryによる効果
                if (player.CurrentGlory > 0)
                {
                    MainCharacter tempTarget = player.Target;
                    player.Target = player;
                    PlayerSpellFreshHeal(player);
                    player.Target = tempTarget;
                }

                // 通常攻撃
                Random rd = new Random(DateTime.Now.Millisecond);
                int atk = rd.Next(atkBase + player.MainWeapon.MinValue, atkBase + player.MainWeapon.MaxValue + player.Level * 2);

                // 各種BUFFによる増減効果
                if (player.CurrentSaintPower > 0)
                {
                    atk = (int)((float)atk * 1.5F);
                }
                if (player.CurrentAetherDrive > 0)
                {
                    atk = (int)((float)atk * 2.0F);
                }
                if (player.CurrentEternalPresence > 0)
                {
                    atk = (int)((float)atk * 1.3F);
                }
                if (target.CurrentProtection > 0 && player.CurrentTruthVision <= 0)
                {
                    atk = (int)((float)atk / 1.2F);
                }
                if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                {
                    if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(88));
                    }
                    else
                    {
                        if (ignoreDefense)
                        {
                            UpdateBattleText(player.GetCharacterSentence(116));
                        }
                        else
                        {
                            atk = (int)((float)atk / 3.0F);
                        }
                    }
                }
                if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                {
                    if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(ec1.GetCharacterSentence(88));
                    }
                    else
                    {
                        if (ignoreDefense)
                        {
                            UpdateBattleText(player.GetCharacterSentence(116));
                        }
                        else
                        {
                            atk = 0;
                        }
                    }
                }

                // Deflectionによる効果
                if (target.CurrentDeflection > 0)
                {
                    player.CurrentLife -= atk;
                    UpdateLife(player);
                    UpdateBattleText(String.Format(target.GetCharacterSentence(61), atk.ToString(), player.Name), 1000);

                    target.CurrentDeflection = 0;
                    target.pbDeflection.Image = null;
                    target.pbDeflection.Update();
                    return;
                }

                if (powerUpValue > 0.0F)
                {
                    atk = (int)((float)atk * powerUpValue); // [コメント]：-1以外だとBUFFUPDOWN効果が無視されるのでこの処理は撤廃するべきです。
                }

                // クリティカル判定
                bool detectCritical = false;
                Random rd2 = new Random(DateTime.Now.Millisecond);
                int result = rd.Next(1, 1001);
                if (player.CurrentWordOfFortune == 1)
                {
                    result = 1;
                }
                if (result < 7 + (player.TotalAgility) && crushingBlow <= 0)
                {
                    detectCritical = true;
                    if (player == ec1 && this.difficulty == 1)
                    {
                        atk = (int)((float)atk * 1.5F);
                    }
                    else
                    {
                        atk = (int)((float)atk * 3.0F);
                    }
                }

                if (CheckDodge(target, ignoreDefense)) { return; }

                target.CurrentLife -= atk;
                UpdateLife(target);

                if (player == ec1)
                {
                    if (crushingBlow > 0)
                    {
                        GroundOne.PlaySoundEffect("CrushingBlow.mp3");
                    }
                    else
                    {
                        GroundOne.PlaySoundEffect("EnemyAttack1.mp3");
                    }
                }
                else
                {
                    if (crushingBlow > 0)
                    {
                        GroundOne.PlaySoundEffect("CrushingBlow.mp3");
                    }
                    else
                    {
                        GroundOne.PlaySoundEffect("SwordSlash1.mp3");
                    }
                    //SecondaryBuffer shotsound;
                    //ArrayList soundlist = new ArrayList();
                    //soundDevice.SetCooperativeLevel(this, CooperativeLevel.Normal);

                    //BufferDescription description = new BufferDescription();
                    //description.ControlEffects = false;
                    //shotsound = new SecondaryBuffer(@"full_path.mp3", description, soundDevice);
                    //shotsound.Play(0, BufferPlayFlags.Default);
                }

                if (crushingBlow > 0)
                {
                    UpdateBattleText(String.Format(player.GetCharacterSentence(70), target.Name, atk.ToString()));
                    if (target.CurrentAntiStun > 0)
                    {
                        target.CurrentAntiStun = 0;
                        target.pbAntiStun.Image = null;
                        target.pbAntiStun.Update();
                        UpdateBattleText(target.GetCharacterSentence(94));
                    }
                    else
                    {
                        if ((target.Accessory != null) && (target.Accessory.Name == "鋼鉄の石像"))
                        {
                            Random rd3 = new Random(DateTime.Now.Millisecond * Environment.TickCount);
                            if (rd3.Next(1, 101) <= target.Accessory.MinValue)
                            {
                                UpdateBattleText(target.Name + "が装備している鋼鉄の石像が光り輝いた！\r\n", 1000);
                                UpdateBattleText(target.Name + "はスタン状態に陥らなかった。\r\n");
                            }
                            else
                            {
                                if (target.CurrentStunning <= 0)
                                {
                                    target.CurrentStunning = crushingBlow;
                                }
                                target.pbStun.Image = Image.FromFile(Database.BaseResourceFolder + "Stunning.bmp");
                                target.pbStun.Update();
                                UpdateBattleText(target.Name + "は気絶した。\r\n");
                            }
                        }
                        else
                        {
                            if (target.CurrentStunning <= 0)
                            {
                                target.CurrentStunning = crushingBlow;
                            }
                            target.pbStun.Image = Image.FromFile(Database.BaseResourceFolder + "Stunning.bmp");
                            target.pbStun.Update();
                            UpdateBattleText(target.Name + "は気絶した。\r\n");
                        }
                    }
                }
                else
                {
                    if (detectCritical)
                    {
                        UpdateBattleText(String.Format(player.GetCharacterSentence(13), target.Name, atk.ToString()));
                    }
                    else
                    {
                        UpdateBattleText(String.Format(player.GetCharacterSentence(115), target.Name, atk.ToString()));
                    }
                }

                // FlameAuraによる追加攻撃
                if (player.CurrentFlameAura > 0)
                {
                    int additional = rd.Next(player.TotalIntelligence, player.TotalIntelligence * 3 + 10);
                    if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                    {
                        if (target.CurrentAbsoluteZero > 0)
                        {
                            UpdateBattleText(target.GetCharacterSentence(88));
                        }
                        else
                        {
                            additional = (int)((float)additional / 3.0F);
                        }
                    }
                    if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                    {
                        if (target.CurrentAbsoluteZero > 0)
                        {
                            UpdateBattleText(target.GetCharacterSentence(88));
                        }
                        else
                        {
                            additional = 0;
                        }
                    }
                    target.CurrentLife -= additional;
                    UpdateLife(target);
                    UpdateBattleText(String.Format(player.GetCharacterSentence(14), additional.ToString()));
                }

                // ImmortalRaveによる追加攻撃
                if (player.CurrentImmortalRave == 3)
                {
                    PlayerSpellFireBall(player, target, 500);
                }
                else if (player.CurrentImmortalRave == 2)
                {
                    PlayerSpellFlameStrike(player, target, 500);
                }
                else if (player.CurrentImmortalRave == 1)
                {
                    PlayerSpellVolcanicWave(player, target, 500);
                }
            }
            catch (Exception ex)
            {
                UpdateBattleText("exception: " + ex.ToString());
            }
        }


        private void EnemyNormalAttack(MainCharacter target)
        {
            EnemyNormalAttack(ec1, target, -1, 0, false);
        }
        private void EnemyNormalAttack(MainCharacter target, int crushingBlow)
        {
            EnemyNormalAttack(ec1, target, -1, crushingBlow, false);
        }
        private void EnemyNormalAttack(MainCharacter target, int crushingBlow, bool ignoreDefense)
        {
            EnemyNormalAttack(ec1, target, -1, crushingBlow, ignoreDefense);
        }
        private void EnemyNormalAttack(MainCharacter player, MainCharacter target, int alreadyValue, int crushingBlow, bool ignoreDefense)
        {
            // 相手：カウンターアタックが入っている場合
            if (target.CurrentCounterAttack > 0)
            {
                // 自分：相手の防御体制を無視できる場合
                if (ignoreDefense)
                {
                    UpdateBattleText(player.GetCharacterSentence(110));
                }
                else
                {
                    // 自分：NothingOfNothingnessによる無効化が張ってある場合
                    if (player.CurrentNothingOfNothingness > 0)
                    {
                        UpdateBattleText(player.Name + "が" + target.Name + "へ攻撃を仕掛ける所で・・・\r\n", 500);
                        UpdateBattleText(target.GetCharacterSentence(114), 1000);
                        UpdateBattleText(player.Name + "は無効化オーラによって護られている！\r\n");
                        //target.CurrentStanceOfEyes = false;
                        target.RemoveStanceOfEyes(); // 後編編集
                        //target.CurrentNegate = false;
                        target.RemoveNegate(); // 後編編集
                        //target.CurrentCounterAttack = false;
                        target.RemoveCounterAttack(); // 後編編集
                    }
                    else
                    {
                        UpdateBattleText(player.Name + "が" + target.Name + "へ攻撃を仕掛ける所で・・・\r\n", 1000);
                        UpdateBattleText(target.GetCharacterSentence(113));
                        PlayerNormalAttack(target, player, -1, 0, false);
                        return;
                    }
                }
            }

            Random rd = new Random(DateTime.Now.Millisecond);
            int enemyAtk = rd.Next((ec1.TotalStrength), (ec1.TotalStrength) + ec1.Level * 2);
            // Deflectionによる効果
            if (target.CurrentDeflection > 0)
            {
                ec1.CurrentLife -= enemyAtk;
                UpdateLife(ec1);
                UpdateBattleText(String.Format(target.GetCharacterSentence(61), enemyAtk.ToString(), ec1.Name), 1000);

                target.CurrentDeflection = 0;
                target.pbDeflection.Image = null;
                target.pbDeflection.Update();
            }
            else
            {
                enemyAtk -= rd.Next(target.MainArmor.MinValue, target.MainArmor.MaxValue + 1);
                if (enemyAtk <= 0) enemyAtk = 0;
                if (ec1.CurrentSaintPower > 0)
                {
                    enemyAtk = (int)((float)enemyAtk * 1.5F);
                }
                if (target.CurrentProtection > 0)
                {
                    enemyAtk = (int)((float)enemyAtk / 1.2F);
                }
                if (target.CurrentAetherDrive > 0)
                {
                    enemyAtk = (int)((float)enemyAtk / 2.0F);
                }
                if (target.CurrentEternalPresence > 0)
                {
                    enemyAtk = (int)((float)enemyAtk / 1.3F);
                }
                if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                {
                    if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(88));
                    }
                    else
                    {
                        if (ignoreDefense)
                        {
                            UpdateBattleText(ec1.GetCharacterSentence(116));
                        }
                        else
                        {
                            enemyAtk = (int)((float)enemyAtk / 3.0F);
                        }
                    }
                }
                if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                {
                    if (target.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(target.GetCharacterSentence(88));
                    }
                    else
                    {
                        if (ignoreDefense)
                        {
                            UpdateBattleText(ec1.Name + "：物理攻撃無効など効くとでも思ったか！　ぬるい！！\r\n");
                        }
                        else
                        {
                            enemyAtk = 0;
                        }
                    }
                }

                bool detectCritical = false;
                Random rd2 = new Random(DateTime.Now.Millisecond);
                int result = rd.Next(1, 1001);
                if (ec1.CurrentWordOfFortune == 1)
                {
                    result = 1;
                }
                if (result < 7 + (ec1.TotalAgility) && crushingBlow <= 0)
                {
                    detectCritical = true;
                    enemyAtk = (int)((float)enemyAtk * 3.0F);
                }

                target.CurrentLife -= enemyAtk;
                UpdateLife(target);
                GroundOne.PlaySoundEffect("EnemyAttack1.mp3");

                if (crushingBlow > 0)
                {
                    UpdateBattleText(String.Format(ec1.GetCharacterSentence(70), target.Name, enemyAtk.ToString()));
                    if (target.CurrentAntiStun > 0)
                    {
                        target.CurrentAntiStun = 0;
                        target.pbAntiStun.Image = null;
                        target.pbAntiStun.Update();
                        UpdateBattleText(target.GetCharacterSentence(94));
                    }
                    else
                    {
                        if ((target.Accessory != null) && (target.Accessory.Name == "鋼鉄の石像"))
                        {
                            Random rd3 = new Random(DateTime.Now.Millisecond * Environment.TickCount);
                            if (rd3.Next(1, 101) <= target.Accessory.MinValue)
                            {
                                UpdateBattleText(target.Name + "が装備している鋼鉄の石像が光り輝いた！\r\n", 1000);
                                UpdateBattleText(target.Name + "はスタン状態に陥らなかった。\r\n");
                            }
                            else
                            {
                                if (target.CurrentStunning <= 0)
                                {
                                    target.CurrentStunning = crushingBlow;
                                }
                                target.pbStun.Image = Image.FromFile(Database.BaseResourceFolder + "Stunning.bmp");
                                target.pbStun.Update();
                                UpdateBattleText(target.Name + "は気絶した。\r\n");
                            }
                        }
                        if (target.CurrentStunning <= 0)
                        {
                            target.CurrentStunning = crushingBlow;
                        }
                        target.pbStun.Image = Image.FromFile(Database.BaseResourceFolder + "Stunning.bmp");
                        target.pbStun.Update();
                        UpdateBattleText(target.Name + "は気絶した。\r\n");
                    }
                }
                else
                {
                    if (detectCritical)
                    {
                        UpdateBattleText(String.Format(ec1.GetCharacterSentence(13), target.Name, enemyAtk.ToString()));
                    }
                    else
                    {
                        UpdateBattleText(ec1.Name + "からの攻撃！" + target.Name + "へ" + enemyAtk.ToString() + "のダメージ\r\n");
                    }
                }

                // FlameAuraによる追加攻撃
                if (ec1.CurrentFlameAura > 0)
                {
                    int additional = rd.Next(ec1.TotalIntelligence, ec1.TotalIntelligence * 3 + 10);
                    if (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0)
                    {
                        if (target.CurrentAbsoluteZero > 0)
                        {
                            UpdateBattleText(target.GetCharacterSentence(88));
                        }
                        else
                        {
                            additional = (int)((float)additional / 3.0F);
                        }
                    }
                    if (target.CurrentOneImmunity > 0 && (target.PA == PlayerAction.Defense || target.CurrentStanceOfStanding > 0))
                    {
                        if (target.CurrentAbsoluteZero > 0)
                        {
                            UpdateBattleText(target.GetCharacterSentence(88));
                        }
                        else
                        {
                            additional = 0;
                        }
                    }
                    target.CurrentLife -= additional;
                    UpdateLife(target);
                    UpdateBattleText(String.Format(ec1.GetCharacterSentence(14), additional.ToString()));
                }

                // ImmortalRaveによる追加攻撃
                if (ec1.CurrentImmortalRave == 3)
                {
                    PlayerSpellFireBall(ec1, target, 500);
                }
                else if (ec1.CurrentImmortalRave == 2)
                {
                    PlayerSpellFlameStrike(ec1, target, 500);
                }
                else if (ec1.CurrentImmortalRave == 1)
                {
                    PlayerSpellVolcanicWave(ec1, target, 500);
                }
            }
        }
        #endregion

        private void PlayerAttackPhase(MainCharacter player)
        {
            if (player.CurrentBlackContract > 0)
            {
                PlayerAttackPhase(player, true);
            }
            else
            {
                PlayerAttackPhase(player, false);
            }
        }

        private void PlayerAttackPhase(MainCharacter player, bool withoutCost)
        {
            if (this.ActivateTimeStop > 0 || this.ActivateTrcky > 0 || this.ActivateGrowUp > 0 || this.ActivateSword > 0 || this.ActivateEverGreen > 0 || this.ActivateTotalEndTime > 0)
            {
                //UpdateBattleText(player.Name + "は時間経過を認識できないまま時間が過ぎ去る！！\r\n", 100);
                return;
            }

            if (player.CurrentStunning > 0)
            {
                UpdateBattleText(player.Name + "は気絶している。\r\n");
                return;
            }

            if (player.CurrentParalyze > 0)
            {
                UpdateBattleText(player.Name + "は麻痺している。\r\n");
                return;
            }

            if (player.CurrentFrozen > 0)
            {
                UpdateBattleText(player.Name + "は凍結している。\r\n");
                return;
            }

            // [警告]：インスタント属性で割り込みが入る行動は戦闘ロジックの一つとして昇華させてください。
            if (ec1.Name == "四階の守護者：Altomo")
            {
                bool exec = true; // 一人も凍結していなければ実行するため、初期値TRUE
                for (int ii = 0; ii < this.playerList.Length; ii++)
                {
                    if (playerList[ii].CurrentFrozen > 0)
                    {
                        exec = false;
                    }
                }

                if (exec && (player.PA == PlayerAction.NormalAttack || player.PA == PlayerAction.UseSkill || player.PA == PlayerAction.UseSpell))
                {
                    if (player.CurrentFrozen <= 0)
                    {
                        Random rd = new Random(Environment.TickCount * DateTime.Now.Millisecond);
                        int threshold = 20;
                        if (this.difficulty == 1)
                        {
                            threshold = 3;
                        }
                        if (rd.Next(1, 101) <= threshold)
                        {
                            UpdateBattleText(player.Name + "の行動中・・・\r\n", 1000);
                            if (this.difficulty == 1)
                            {
                                player.CurrentFrozen = 2; // １ターン継続のためには、初期値は２＋１
                            }
                            else
                            {
                                player.CurrentFrozen = 6; // ５ターン継続のためには、初期値は５＋１
                            }
                            player.pbFrozen.Image = Image.FromFile(Database.BaseResourceFolder + "Frozen.bmp");
                            player.pbFrozen.Update();
                            if (ec1.CurrentStunning > 0)
                            {
                                UpdateBattleText(ec1.Name + "は気絶する直前に罠を仕掛けていた！" + player.Name + "がFrozenTrapで凍結された！！\r\n", 2000);
                            }
                            else if (ec1.CurrentParalyze > 0)
                            {
                                UpdateBattleText(ec1.Name + "は麻痺する直前に罠を仕掛けていた！" + player.Name + "がFrozenTrapで凍結された！！\r\n", 2000);
                            }
                            else
                            {
                                UpdateBattleText(ec1.Name + "：かかったな！　" + player.Name + "がFrozenTrapで凍結された！！\r\n", 2000);
                            }
                            return;
                        }
                    }
                }
            }
                
            switch (player.PA)
            {
                case PlayerAction.NormalAttack: // 通常攻撃
                    PlayerNormalAttack(player);
                    break;

                case PlayerAction.UseSkill:
                    if (player.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(player.GetCharacterSentence(87));
                        return;
                    }

                    switch (player.CurrentSkillName)
                    {
                        // 動
                        case Database.STRAIGHT_SMASH:
                            if (!withoutCost)
                            {
                                if (player.CurrentSkillPoint < Database.STRAIGHT_SMASH_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(0));
                                    return;
                                }
                                player.CurrentSkillPoint -= Database.STRAIGHT_SMASH_COST;
                                UpdateMCSkillPoint(player);
                            }

                            PlayerSkillStraightSmash(player, ec1, false);
                            break;

                        case Database.DOUBLE_SLASH:
                            if (!withoutCost)
                            {
                                if (player.CurrentSkillPoint < Database.DOUBLE_SLASH_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(0));
                                    return;
                                }
                                player.CurrentSkillPoint -= Database.DOUBLE_SLASH_COST;
                                UpdateMCSkillPoint(player);
                            }

                            PlayerSkillDoubleSlash(player, ec1, false);
                            break;

                        case Database.CRUSHING_BLOW:
                            if (!withoutCost)
                            {
                                if (player.CurrentSkillPoint < Database.CRUSHING_BLOW_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(0));
                                    return;
                                }
                                player.CurrentSkillPoint -= Database.CRUSHING_BLOW_COST;
                                UpdateMCSkillPoint(player);
                            }

                            PlayerSkillCrushingBlow(player, ec1);
                            break;

                        case Database.SOUL_INFINITY:
                            if (!withoutCost)
                            {
                                if (player.CurrentSkillPoint < Database.SOUL_INFINITY_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(0));
                                    return;
                                }
                                player.CurrentSkillPoint -= Database.SOUL_INFINITY_COST;
                                UpdateMCSkillPoint(player);
                            }

                            PlayerSkillSoulInfinity(player, ec1, 0, false);
                            break;

                        // 静
                        case Database.COUNTER_ATTACK:
                            if (!withoutCost)
                            {
                                if (player.CurrentSkillPoint < Database.COUNTER_ATTACK_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(0));
                                    return;
                                }
                                player.CurrentSkillPoint -= Database.COUNTER_ATTACK_COST;
                                UpdateMCSkillPoint(player);
                            }

                            PlayerSkillCounterAttack(player, ec1);
                            break;

                        case Database.PURE_PURIFICATION:
                            if (!withoutCost)
                            {
                                if (player.CurrentSkillPoint < Database.PURE_PURIFICATION_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(0));
                                    return;
                                }
                                player.CurrentSkillPoint -= Database.PURE_PURIFICATION_COST;
                                UpdateMCSkillPoint(player);
                            }

                            PlayerSkillPurePurification(player);
                            break;

                        case Database.ANTI_STUN:
                            if (!withoutCost)
                            {
                                if (player.CurrentSkillPoint < Database.ANTI_STUN_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(0));
                                    return;
                                }
                                player.CurrentSkillPoint -= Database.ANTI_STUN_COST;
                                UpdateMCSkillPoint(player);
                            }

                            PlayerSkillAntiStun(player, player);
                            break;

                        case Database.STANCE_OF_DEATH:
                            if (!withoutCost)
                            {
                                if (player.CurrentSkillPoint < Database.STANCE_OF_DEATH_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(0));
                                    return;
                                }
                                player.CurrentSkillPoint -= Database.STANCE_OF_DEATH_COST;
                                UpdateMCSkillPoint(player);
                            }

                            PlayerSkillStanceOfDeath(player, player);
                            break;

                        // 柔
                        case Database.STANCE_OF_FLOW:
                            if (!withoutCost)
                            {
                                if (player.CurrentSkillPoint < Database.STANCE_OF_FLOW_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(0));
                                    return;
                                }
                                player.CurrentSkillPoint -= Database.STANCE_OF_FLOW_COST;
                                UpdateMCSkillPoint(player);
                            }
                            PlayerSkillStanceOfFlow(player);
                            break;

                        case Database.ENIGMA_SENSE:
                            if (!withoutCost)
                            {
                                if (player.CurrentSkillPoint < Database.ENIGMA_SENSE_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(0));
                                    return;
                                }
                                player.CurrentSkillPoint -= Database.ENIGMA_SENSE_COST;
                                UpdateMCSkillPoint(player);
                            }
                            PlayerSkillEnigmaSence(player, ec1, false);
                            break;

                        case Database.SILENT_RUSH:
                            if (!withoutCost)
                            {
                                if (player.CurrentSkillPoint < Database.SILENT_RUSH_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(0));
                                    return;
                                }
                                player.CurrentSkillPoint -= Database.SILENT_RUSH_COST;
                                UpdateMCSkillPoint(player);
                            }
                            PlayerSkillSilentRush(player, ec1, false);
                            break;

                        case Database.OBORO_IMPACT:
                            if (!withoutCost)
                            {
                                if (player.CurrentSkillPoint < Database.OBORO_IMPACT_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(0));
                                    return;
                                }
                                player.CurrentSkillPoint -= Database.OBORO_IMPACT_COST;
                                UpdateMCSkillPoint(player);
                            }
                            PlayerSkillOboroImpact(player, ec1, 0, false);
                            break;

                        // 剛
                        case Database.STANCE_OF_STANDING:
                            if (!withoutCost)
                            {
                                if (player.CurrentSkillPoint < Database.STANCE_OF_STANDING_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(0));
                                    return;
                                }
                                player.CurrentSkillPoint -= Database.STANCE_OF_STANDING_COST;
                                UpdateMCSkillPoint(player);
                            }
                            PlayerSkillStanceOfStanding(player, ec1);
                            break;

                        case Database.INNER_INSPIRATION:
                            // スキルポイント回復のため、スキルポイントは消費しない。
                            PlayerSkillInnerInspiration(player);
                            break;

                        case Database.KINETIC_SMASH:
                            if (!withoutCost)
                            {
                                if (player.CurrentSkillPoint < Database.KINETIC_SMASH_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(0));
                                    return;
                                }
                                player.CurrentSkillPoint -= Database.KINETIC_SMASH_COST;
                                UpdateMCSkillPoint(player);
                            }
                            PlayerSkillKineticSmash(player, ec1, false);
                            break;

                        case Database.CATASTROPHE:
                            // 全スキルポイントを消費するため、ここでポイントチェックしない。
                            PlayerSkillCatastrophe(player, ec1, false);
                            // こちらでスキル全消費
                            player.CurrentSkillPoint = 0;
                            UpdateMCSkillPoint(player);
                            break;

                        // 心眼
                        case Database.TRUTH_VISION:
                            if (!withoutCost)
                            {
                                if (player.CurrentSkillPoint < Database.TRUTH_VISION_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(0));
                                    return;
                                }
                                player.CurrentSkillPoint -= Database.TRUTH_VISION_COST;
                                UpdateMCSkillPoint(player);
                            }

                            PlayerSkillTruthVision(player, player);
                            break;

                        case Database.HIGH_EMOTIONALITY:
                            if (!withoutCost)
                            {
                                if (player.CurrentSkillPoint < Database.HIGH_EMOTIONALITY_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(0));
                                    return;
                                }
                                player.CurrentSkillPoint -= Database.HIGH_EMOTIONALITY_COST;
                                UpdateMCSkillPoint(player);
                            }

                            PlayerSkillHighEmotionality(player, player);
                            break;

                        case Database.STANCE_OF_EYES:
                            if (!withoutCost)
                            {
                                if (player.CurrentSkillPoint < Database.STANCE_OF_EYES_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(0));
                                    return;
                                }
                                player.CurrentSkillPoint -= Database.STANCE_OF_EYES_COST;
                                UpdateMCSkillPoint(player);
                            }

                            PlayerSkillStanceOfEyes(player, ec1);
                            break;

                        case Database.PAINFUL_INSANITY:
                            if (!withoutCost)
                            {
                                if (player.CurrentSkillPoint < Database.PAINFUL_INSANITY_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(0));
                                    return;
                                }
                                player.CurrentSkillPoint -= Database.PAINFUL_INSANITY_COST;
                                UpdateMCSkillPoint(player);
                            }
                            PlayerSkillPainfulInsanity(player);
                            break;

                        // 無心
                        case Database.NEGATE:
                            if (!withoutCost)
                            {
                                if (player.CurrentSkillPoint < Database.NEGATE_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(0));
                                    return;
                                }
                                player.CurrentSkillPoint -= Database.NEGATE_COST;
                                UpdateMCSkillPoint(player);
                            }

                            PlayerSkillNegate(player, ec1);
                            break;

                        case Database.VOID_EXTRACTION:
                            if (!withoutCost)
                            {
                                if (player.CurrentSkillPoint < Database.VOID_EXTRACTION_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(0));
                                    return;
                                }
                                player.CurrentSkillPoint -= Database.VOID_EXTRACTION_COST;
                                UpdateMCSkillPoint(player);
                            }

                            PlayerSkillVoidExtraction(player);
                            break;

                        case Database.CARNAGE_RUSH:
                            if (!withoutCost)
                            {
                                if (player.CurrentSkillPoint < Database.CARNAGE_RUSH_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(0));
                                    return;
                                }
                                player.CurrentSkillPoint -= Database.CARNAGE_RUSH_COST;
                                UpdateMCSkillPoint(player);
                            }

                            PlayerSkillCarnageRush(player, ec1, false);
                            break;

                        case Database.NOTHING_OF_NOTHINGNESS:
                            if (!withoutCost)
                            {
                                if (player.CurrentSkillPoint < Database.NOTHING_OF_NOTHINGNESS_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(0));
                                    return;
                                }
                                player.CurrentSkillPoint -= Database.NOTHING_OF_NOTHINGNESS_COST;
                                UpdateMCSkillPoint(player);
                            }

                            PlayerSkillNothingOfNothingness(player, player);
                            break;

                        default:
                            UpdateBattleText(player.GetCharacterSentence(5));
                            break;
                    }
                    break;

                case PlayerAction.Defense: // 防御する
                    UpdateBattleText(player.Name + "は防御姿勢をとった。\r\n");
                    break;

                case PlayerAction.RunAway: // 逃げる
                    break;

                case PlayerAction.UseItem:
                    switch (player.CurrentUsingItem)
                    {
                        case "小さい赤ポーション":
                        case "普通の赤ポーション":
                        case "大きな赤ポーション":
                        case "特大赤ポーション":
                        case "豪華な赤ポーション":
                            ItemBackPack item = new ItemBackPack(player.CurrentUsingItem);
                            int effect = item.UseIt(); // [警告]：汎用性の高いメソッド名だが、実質ポーション専用になっています。
                            if (player.CurrentAbsoluteZero > 0)
                            {
                                UpdateBattleText(player.GetCharacterSentence(119));
                            }
                            else
                            {
                                UpdateBattleText(player.Name + "は" + item.Name + "を使った。" + effect.ToString() + " ライフ回復\r\n");
                                player.CurrentLife += effect;
                                UpdateLife(player);
                            }
                            player.DeleteBackPack(item);
                            break;

                        case "小さい青ポーション":
                        case "普通の青ポーション":
                        case "大きな青ポーション":
                        case "特大青ポーション":
                        case "豪華な青ポーション":
                            item = new ItemBackPack(player.CurrentUsingItem);
                            effect = item.UseIt();
                            UpdateBattleText(player.Name + "は" + item.Name + "を使った。" + effect.ToString() + " マナ回復\r\n");
                            player.CurrentMana += effect;
                            UpdateMCMana(player);
                            player.DeleteBackPack(item);
                            break;

                        case "神聖水": // ２階アイテム
                            if (!we.AlreadyUseSyperSaintWater)
                            {
                                item = new ItemBackPack(player.CurrentUsingItem);
                                we.AlreadyUseSyperSaintWater = true;
                                player.CurrentLife += (int)((double)player.MaxLife * 0.3F);
                                player.CurrentMana += (int)((double)player.MaxMana * 0.3F);
                                player.CurrentSkillPoint += (int)((double)player.MaxSkillPoint * 0.3F);
                                UpdateLife(player);
                                UpdateMCMana(player);
                                UpdateMCSkillPoint(player);
                                UpdateBattleText(player.GetCharacterSentence(111));
                            }
                            else
                            {
                                UpdateBattleText(player.GetCharacterSentence(112));
                            }
                            break;

                        case "遠見の青水晶":
                            UpdateBattleText(player.GetCharacterSentence(15));
                            break;
                        case "リーベストランクポーション":
                            ItemBackPack item2 = new ItemBackPack(player.CurrentUsingItem);
                            UpdateBattleText(player.GetCharacterSentence(71));
                            UpdateBattleText(ec1.Name + "は" + player.Name + "への誘惑にかられた！\r\n");
                            ec1.CurrentTemptation = 3;
                            player.DeleteBackPack(item2);
                            break;

                        case "アカシジアの実":
                            ItemBackPack item3 = new ItemBackPack(player.CurrentUsingItem);
                            UpdateBattleText(player.GetCharacterSentence(80));
                            UpdateBattleText(player.Name + "にかかっている猛毒とスタン効果が解除された。\r\n");
                            player.CurrentStunning = 0;
                            player.pbStun.Image = null;
                            player.pbStun.Update();
                            player.CurrentPoison = 0;
                            player.pbPoison.Image = null;
                            player.pbPoison.Update();
                            player.DeleteBackPack(item3);
                            break;

                        case "リヴァイヴポーション":
                            if (!we.AlreadyUseRevivePotion)
                            {
                                ItemBackPack item4 = new ItemBackPack(player.CurrentUsingItem);
                                GroundOne.PlaySoundEffect("Resurrection.mp3");
                                UpdateBattleText(player.GetCharacterSentence(118), 3000);

                                if (!player.Target.Dead)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(54));
                                }
                                else
                                {
                                    we.AlreadyUseRevivePotion = true;
                                    player.Target.Dead = false;
                                    player.Target.CurrentLife += player.Target.MaxLife / 2;
                                    UpdateLife(player.Target);
                                    player.Target.labelLife.ForeColor = Color.Black;
                                    player.Target.labelMana.ForeColor = Color.Black;
                                    player.Target.labelName.ForeColor = Color.Black;
                                    player.Target.labelSkill.ForeColor = Color.Black;
                                    this.Update();
                                    UpdateBattleText(player.Target.Name + "は復活した！\r\n");
                                }
                            }
                            else
                            {
                                UpdateBattleText(player.GetCharacterSentence(112));
                            }                            
                            break;

                        default:
                            UpdateBattleText(player.GetCharacterSentence(16));
                            break;
                    }
                    break;
                    
                case PlayerAction.UseSpell:
                    if (player.CurrentSilence > 0)
                    {
                        UpdateBattleText(player.GetCharacterSentence(76));
                        return;
                    }

                    if (player.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(player.GetCharacterSentence(76));
                        return;
                    }

                    switch (player.CurrentSpellName)
                    {
                        // 聖
                        case Database.FRESH_HEAL:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.FRESH_HEAL_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.FRESH_HEAL_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellFreshHeal(player);
                            break;

                        case Database.PROTECTION:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.PROTECTION_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.PROTECTION_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellProtection(player);
                            break;

                        case Database.HOLY_SHOCK:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.HOLY_SHOCK_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.HOLY_SHOCK_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellHolyShock(player, ec1, 500);
                            break;

                        case Database.SAINT_POWER:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.SAINT_POWER_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.SAINT_POWER_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellSaintPower(player);
                            break;

                        case Database.GLORY:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.GLORY_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.GLORY_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellGlory(player);
                            break;

                        case Database.RESURRECTION:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.RESURRECTION_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.RESURRECTION_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellResurrection(player);
                            break;

                        case Database.CELESTIAL_NOVA:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.CELESTIAL_NOVA_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.CELESTIAL_NOVA_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellCelestialNova(player);
                            break;

                        // 闇
                        case Database.DARK_BLAST:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.DARK_BLAST_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.DARK_BLAST_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellDarkBlast(player, ec1, 500);
                            break;

                        case Database.SHADOW_PACT:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.SHADOW_PACT_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.SHADOW_PACT_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellShadowPact(player);
                            break;

                        case Database.LIFE_TAP:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.LIFE_TAP_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.LIFE_TAP_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellLifeTap(player);
                            break;

                        case Database.BLACK_CONTRACT:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.BLACK_CONTRACT_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.BLACK_CONTRACT_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellBlackContract(player, player, false);
                            break;

                        case Database.DEVOURING_PLAGUE:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.DEVOURING_PLAGUE_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.DEVOURING_PLAGUE_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellDevouringPlague(player, ec1);
                            break;

                        case Database.BLOODY_VENGEANCE:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.BLOODY_VENGEANCE_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.BLOODY_VENGEANCE_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellBloodyVengeance(player);
                            break;

                        case Database.DAMNATION:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.DAMNATION_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.DAMNATION_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellDamnation(player);
                            break;

                        // 火
                        case Database.FIRE_BALL:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.FIRE_BALL_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.FIRE_BALL_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellFireBall(player, ec1, 500);
                            break;

                        case Database.FLAME_AURA:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.FLAME_AURA_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.FLAME_AURA_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellFlameAura(player);
                            break;

                        case Database.HEAT_BOOST:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.HEAT_BOOST_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.HEAT_BOOST_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellHeatBoost(player);
                            break;

                        case Database.FLAME_STRIKE:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.FLAME_STRIKE_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.FLAME_STRIKE_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellFlameStrike(player, ec1, 500);
                            break;

                        case Database.VOLCANIC_WAVE:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.VOLCANIC_WAVE_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.VOLCANIC_WAVE_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellVolcanicWave(player, ec1, 500);
                            break;

                        case Database.IMMORTAL_RAVE:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.IMMORTAL_RAVE_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.IMMORTAL_RAVE_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellImmortalRave(player);
                            break;

                        case Database.LAVA_ANNIHILATION:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.LAVA_ANNIHILATION_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.LAVA_ANNIHILATION_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellLavaAnnihilation(player, ec1, 1000);
                            break;

                        // 水
                        case Database.ICE_NEEDLE:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.ICE_NEEDLE_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.ICE_NEEDLE_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellIceNeedle(player, ec1, 500);
                            break;

                        case Database.ABSORB_WATER:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.ABSORB_WATER_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.ABSORB_WATER_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellAbsorbWater(player);
                            break;

                        case Database.CLEANSING:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.CLEANSING_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.CLEANSING_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellCleansing(player);
                            break;

                        case Database.FROZEN_LANCE:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.FROZEN_LANCE_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.FROZEN_LANCE_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellFrozenLance(player, ec1, 500);
                            break;

                        case Database.MIRROR_IMAGE:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.MIRROR_IMAGE_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.MIRROR_IMAGE_COST;
                                UpdateMCMana(player);
                            }
                            PlayerSpellMirrorImage(player);
                            break;

                        case Database.PROMISED_KNOWLEDGE:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.PROMISED_KNOWLEDGE_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.PROMISED_KNOWLEDGE_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellPromisedKnowledge(player);
                            break;

                        case Database.ABSOLUTE_ZERO:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.ABSOLUTE_ZERO_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.ABSOLUTE_ZERO_COST;
                                UpdateMCMana(player);
                            }
                            PlayerSpellAbsoluteZero(player, ec1, 500);
                            break;

                        // 理
                        case Database.WORD_OF_POWER:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.WORD_OF_POWER_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.WORD_OF_POWER_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellWordOfPower(player, ec1, 500);
                            break;

                        case Database.GALE_WIND:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.GALE_WIND_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.GALE_WIND_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellGaleWind(player);
                            break;

                        case Database.WORD_OF_LIFE:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.WORD_OF_LIFE_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.WORD_OF_LIFE_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellWordOfLife(player);
                            break;

                        case Database.WORD_OF_FORTUNE:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.WORD_OF_FORTUNE_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.WORD_OF_FORTUNE_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellWordOfFortune(player);
                            break;

                        case Database.AETHER_DRIVE:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.AETHER_DRIVE_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.AETHER_DRIVE_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellAetherDrive(player);
                            break;


                        case Database.GENESIS:
                            // コスト０である。
                            PlayerSpellGenesis(player, player.BeforeTarget);
                            break;

                        case Database.ETERNAL_PRESENCE:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.ETERNAL_PRESENCE_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.ETERNAL_PRESENCE_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellEternalPresence(player);
                            break;

                        // 空
                        case Database.DISPEL_MAGIC:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.DISPEL_MAGIC_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.DISPEL_MAGIC_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellDispelMagic(player, ec1);
                            break;

                        case Database.RISE_OF_IMAGE:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.RISE_OF_IMAGE_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.RISE_OF_IMAGE_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellRiseOfImage(player);
                            break;

                        case Database.DEFLECTION:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.DEFLECTION_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.DEFLECTION_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellDeflection(player);
                            break;

                        case Database.TRANQUILITY:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.TRANQUILITY_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.TRANQUILITY_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellTranquility(player, ec1, 500);
                            break;

                        case Database.ONE_IMMUNITY:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.ONE_IMMUNITY_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.ONE_IMMUNITY_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellOneImmunity(player);
                            break;

                        case Database.WHITE_OUT:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.WHITE_OUT_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.WHITE_OUT_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellWhiteOut(player, ec1, 500);
                            break;

                        case Database.TIME_STOP:
                            if (!withoutCost)
                            {
                                if (player.CurrentMana < Database.TIME_STOP_COST)
                                {
                                    UpdateBattleText(player.GetCharacterSentence(17));
                                    return;
                                }
                                player.CurrentMana -= Database.TIME_STOP_COST;
                                UpdateMCMana(player);
                            }

                            PlayerSpellTimeStop(player);
                            break;

                        default:
                            UpdateBattleText(player.GetCharacterSentence(50));
                            break;
                    } 
                    break;
            }
        }




        private void UpkeepStep()
        {
            textBox1.Text = "";
            currentPlayerLabel.Text = "";

            Attack.Enabled = false;
            SpellBox.Enabled = false;
            SkillBox.Enabled = false;
            ItemBox.Enabled = false;
            Defense.Enabled = false;
            RunAway.Enabled = false;
            playerBack.Enabled = false;

            this.Update();
            System.Threading.Thread.Sleep(0);
        }

        int CurrentWheelOfTime = 0;
        int ActivateTimeStop = 0;
        int CurrentWheelOfTricky = 0;
        int ActivateTrcky = 0;
        int CurrentWheelOfGrowUp = 0;
        int ActivateGrowUp = 0;
        int CurrentWheelOfSword = 0;
        int ActivateSword = 0;
        int CurrentWheelOfEverGreen = 0;
        int ActivateEverGreen = 0;
        int CurrentWheelOfTotalEndTime = 0;
        int ActivateTotalEndTime = 0;
        bool CannotResurrect = false;
        bool AlreadyActiveEverGreen = false;
        private void AfterBattleEffect()
        {
            if (ec1.Name == "五階の守護者：Bystander")
            {
                int result = 0;
                while (true)
                {
                    Random ActionNumber = new Random(DateTime.Now.Millisecond * Environment.TickCount);
                    result = ActionNumber.Next(1, 6);
                    if ((ec1.CurrentLife <= (ec1.MaxLife / 3)) && !this.CannotResurrect)
                    {
                        result = 6;
                    }
                    if (ec1.CurrentProtection > 0 && result == 3)
                    {
                        System.Threading.Thread.Sleep(1);
                        continue;
                    }
                    else if (result == 5 && AlreadyActiveEverGreen)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                switch (result)
                {
                    case 1:
                        if (CurrentWheelOfTime <= 0 && CurrentWheelOfTricky <= 0 && CurrentWheelOfGrowUp <= 0 && CurrentWheelOfSword <= 0 && CurrentWheelOfEverGreen <= 0 && CurrentWheelOfTotalEndTime <= 0 &&
                            ActivateTimeStop <= 0 && ActivateTrcky <= 0 && ActivateGrowUp <= 0 && ActivateSword <= 0 && ActivateEverGreen <= 0 && ActivateTotalEndTime <= 0)
                        {
                            AlreadyActiveEverGreen = false;
                            UpdateBattleText(ec1.Name + "の後方にある『時間律（憎業）の歯車』が静かに回り始めた。　\r\n", 1000);
                            if (this.difficulty == 1)
                            {
                                CurrentWheelOfTime = 4;
                            }
                            else
                            {
                                CurrentWheelOfTime = 3;
                            }
                        }
                        break;
                    case 2:
                        if (CurrentWheelOfTime <= 0 && CurrentWheelOfTricky <= 0 && CurrentWheelOfGrowUp <= 0 && CurrentWheelOfSword <= 0 && CurrentWheelOfEverGreen <= 0 && CurrentWheelOfTotalEndTime <= 0 &&
                            ActivateTimeStop <= 0 && ActivateTrcky <= 0 && ActivateGrowUp <= 0 && ActivateSword <= 0 && ActivateEverGreen <= 0 && ActivateTotalEndTime <= 0)
                        {
                            AlreadyActiveEverGreen = false;
                            UpdateBattleText(ec1.Name + "の後方にある『時間律（零空）の歯車』が静かに回り始めた。　\r\n", 1000);
                            if (this.difficulty == 1)
                            {
                                CurrentWheelOfTricky = 3;
                            }
                            else
                            {
                                CurrentWheelOfTricky = 2;
                            }
                        }
                        break;
                    case 3:
                        if (CurrentWheelOfTime <= 0 && CurrentWheelOfTricky <= 0 && CurrentWheelOfGrowUp <= 0 && CurrentWheelOfSword <= 0 && CurrentWheelOfEverGreen <= 0 && CurrentWheelOfTotalEndTime <= 0 &&
                            ActivateTimeStop <= 0 && ActivateTrcky <= 0 && ActivateGrowUp <= 0 && ActivateSword <= 0 && ActivateEverGreen <= 0 && ActivateTotalEndTime <= 0)
                        {
                            AlreadyActiveEverGreen = false;
                            UpdateBattleText(ec1.Name + "の後方にある『時間律（盛栄）の歯車』が静かに回り始めた。  \r\n", 1000);
                            if (this.difficulty == 1)
                            {
                                CurrentWheelOfGrowUp = 2;
                            }
                            else
                            {
                                CurrentWheelOfGrowUp = 1;
                            }
                        }
                        break;
                    case 4:
                        if (CurrentWheelOfTime <= 0 && CurrentWheelOfTricky <= 0 && CurrentWheelOfGrowUp <= 0 && CurrentWheelOfSword <= 0 && CurrentWheelOfEverGreen <= 0 && CurrentWheelOfTotalEndTime <= 0 &&
                            ActivateTimeStop <= 0 && ActivateTrcky <= 0 && ActivateGrowUp <= 0 && ActivateSword <= 0 && ActivateEverGreen <= 0 && ActivateTotalEndTime <= 0)
                        {
                            AlreadyActiveEverGreen = false;
                            UpdateBattleText(ec1.Name + "の後方にある『時間律（絶剣）の歯車』が静かに回り始めた。  \r\n", 1000);
                            if (this.difficulty == 1)
                            {
                                CurrentWheelOfSword = 4;
                            }
                            else
                            {
                                CurrentWheelOfSword = 3;
                            }
                        }
                        break;
                    case 5:
                        if (CurrentWheelOfTime <= 0 && CurrentWheelOfTricky <= 0 && CurrentWheelOfGrowUp <= 0 && CurrentWheelOfSword <= 0 && CurrentWheelOfEverGreen <= 0 && CurrentWheelOfTotalEndTime <= 0 &&
                            ActivateTimeStop <= 0 && ActivateTrcky <= 0 && ActivateGrowUp <= 0 && ActivateSword <= 0 && ActivateEverGreen <= 0 && ActivateTotalEndTime <= 0)
                        {
                            AlreadyActiveEverGreen = true;
                            UpdateBattleText(ec1.Name + "の後方にある『時間律（緑永）の歯車』が静かに回り始めた。  \r\n", 1000);
                            if (this.difficulty == 1)
                            {
                                CurrentWheelOfEverGreen = 3;
                            }
                            else
                            {
                                CurrentWheelOfEverGreen = 2;
                            }
                        }
                        break;
                    case 6:
                        if (CurrentWheelOfTime <= 0 && CurrentWheelOfTricky <= 0 && CurrentWheelOfGrowUp <= 0 && CurrentWheelOfSword <= 0 && CurrentWheelOfEverGreen <= 0 && CurrentWheelOfTotalEndTime <= 0 &&
                            ActivateTimeStop <= 0 && ActivateTrcky <= 0 && ActivateGrowUp <= 0 && ActivateSword <= 0 && ActivateEverGreen <= 0 && ActivateTotalEndTime <= 0)
                        {
                            AlreadyActiveEverGreen = false;
                            UpdateBattleText(ec1.Name + "の後方にある『完全絶対時間律（終焉）の歯車』が静かに回り始めた。  \r\n", 1000);
                            if (this.difficulty == 1)
                            {
                                CurrentWheelOfTotalEndTime = 5;
                            }
                            else
                            {
                                CurrentWheelOfTotalEndTime = 4;
                            }
                        }
                        break;
                }

                if (this.CurrentWheelOfTime > 0)
                {
                    this.CurrentWheelOfTime--;
                    UpdateBattleText("『時間律（憎業）の歯車』が回り続けている　・・・　" + CurrentWheelOfTime.ToString() + "\r\n");
                    if (this.CurrentWheelOfTime <= 0)
                    {
                        UpdateBattleText("『時間律（憎業）の歯車』が共鳴音を発し、激しい光と共に高速に回り始める！\r\n", 1000);
                        UpdateBattleText(ec1.Name + "：『現在時間停止』　TimeStopを発動する。我の番だ。\r\n", 2000);
                        ActivateTimeStop = 10; // ９回行動のため９＋１
                    }
                }

                if (this.CurrentWheelOfTricky > 0)
                {
                    this.CurrentWheelOfTricky--;
                    UpdateBattleText("『時間律（零空）の歯車』が回り続けている　・・・　" + CurrentWheelOfTricky.ToString() + "\r\n");
                    if (this.CurrentWheelOfTricky <= 0)
                    {
                        UpdateBattleText("『時間律（零空）の歯車』が共鳴音を発し、激しい光と共に高速に回り始める！\r\n", 1000);
                        UpdateBattleText(ec1.Name + "：『現在時間停止』　TimeStopを発動する。我の番だ。\r\n", 2000);
                        ActivateTrcky = 6; // ５回行動のため５＋１
                    }
                }

                if (this.CurrentWheelOfGrowUp > 0)
                {
                    this.CurrentWheelOfGrowUp--;
                    UpdateBattleText("『時間律（盛栄）の歯車』が回り続けている　・・・　" + CurrentWheelOfGrowUp.ToString() + "\r\n");
                    if (this.CurrentWheelOfGrowUp <= 0)
                    {
                        UpdateBattleText("『時間律（盛栄）の歯車』が共鳴音を発し、激しい光と共に高速に回り始める！\r\n", 1000);
                        UpdateBattleText(ec1.Name + "：『現在時間停止』　TimeStopを発動する。我の番だ。\r\n", 2000);
                        ActivateGrowUp = 10; // ９回行動のため９＋１
                    }
                }

                if (this.CurrentWheelOfSword > 0)
                {
                    this.CurrentWheelOfSword--;
                    UpdateBattleText("『時間律（絶剣）の歯車』が回り続けている　・・・　" + CurrentWheelOfSword.ToString() + "\r\n");
                    if (this.CurrentWheelOfSword <= 0)
                    {
                        UpdateBattleText("『時間律（絶剣）の歯車』が共鳴音を発し、激しい光と共に高速に回り始める！\r\n", 1000);
                        UpdateBattleText(ec1.Name + "：『現在時間停止』　TimeStopを発動する。我の番だ。\r\n", 2000);
                        ActivateSword = 5; // ４回行動のため４＋１
                    }
                }

                if (this.CurrentWheelOfEverGreen > 0)
                {
                    this.CurrentWheelOfEverGreen--;
                    UpdateBattleText("『時間律（緑永）の歯車』が回り続けている　・・・　" + CurrentWheelOfEverGreen.ToString() + "\r\n");
                    if (this.CurrentWheelOfEverGreen <= 0)
                    {
                        UpdateBattleText("『時間律（緑永）の歯車』が共鳴音を発し、激しい光と共に高速に回り始める！\r\n", 1000);
                        UpdateBattleText(ec1.Name + "：『現在時間停止』　TimeStopを発動する。我の番だ。\r\n", 2000);
                        ActivateEverGreen = 4; // ３回行動のため３＋１
                    }
                }

                if (this.CurrentWheelOfTotalEndTime > 0)
                {
                    this.CurrentWheelOfTotalEndTime--;
                    UpdateBattleText("『完全絶対時間律（終焉）の歯車』が回り続けている　・・・　" + CurrentWheelOfTotalEndTime.ToString() + "\r\n");
                    if (this.CurrentWheelOfTotalEndTime <= 0)
                    {
                        UpdateBattleText("『完全絶対時間律【終焉】の歯車』が共鳴音を発し、激しい光と共に高速に回り始める！\r\n", 1000);
                        UpdateBattleText(ec1.Name + "：『現在時間停止』　TimeStopを発動する。我の番だ。\r\n", 2000);
                        ActivateTotalEndTime = 10; // ９回行動のため９＋１
                    }
                }
                //if (this.CurrentWheelOfVoid > 0)
                //{
                //    this.CurrentWheelOfVoid--;
                //    UpdateBattleText("『空虚の歯車』が回り続けている　・・・　" + CurrentWheelOfVoid.ToString() + "\r\n");
                //    if (this.CurrentWheelOfVoid <= 0)
                //    {
                //        UpdateBattleText("『空虚の歯車』が共鳴音を発し、激しい光と共に高速に回り始める！\r\n", 1500);
                //        UpdateBattleText(ec1.Name + "：『全て』は『無』へと還る。\r\n");
                //        EnemySpellDispelMagic(mc);
                //        EnemySpellDispelMagic(sc);
                //        EnemySpellDispelMagic(tc);
                //    }
                //}

                if (this.ActivateTimeStop > 0)
                {
                    this.ActivateTimeStop--;
                    if (this.ActivateTimeStop <= 0)
                    {
                        UpdateBattleText("『時間律（憎業）の歯車』が黒い光を発し、突如回転を止めた！\r\n", 1000);
                        UpdateBattleText(ec1.Name + "：時間だ。　時が再び動きだす。　『十』の『魔法』、食らうといい。\r\n", 2000);

                        MainCharacter target = null;
                        Random rd3 = new Random(DateTime.Now.Millisecond);
                        int totalAliveNum = 0;

                        string lastFoundTarget = string.Empty;
                        bool activeTarget = false;
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            if (!playerList[ii].Dead)
                            {
                                lastFoundTarget = playerList[ii].Name;
                            }
                            activeTarget = !playerList[ii].Dead;

                            if (activeTarget)
                            {
                                totalAliveNum++;
                            }
                        }

                        int targetNum = rd3.Next(0, totalAliveNum);
                        int throughNum = 0;
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            activeTarget = !playerList[ii].Dead;
                            if (activeTarget)
                            {
                                if ((targetNum + throughNum) == ii)
                                {
                                    target = playerList[ii];
                                    break;
                                }
                            }
                            else
                            {
                                throughNum++;
                            }
                        }

                        if (bossSpecialActionTarget != null)
                        {
                            if (!bossSpecialActionTarget.Dead)
                            {
                                target = bossSpecialActionTarget;
                            }
                        }

                        if (target == null)
                        {
                            UpdateBattleText(ec1.Name + "は対象を見失っている。何もできない\r\n");
                            return;
                        }

                        ec1.Target = target;
                        PlayerSpellDarkBlast(ec1, ec1.Target, 200);
                        this.Update();
                        PlayerSpellFireBall(ec1, ec1.Target, 200);
                        this.Update();
                        PlayerSpellIceNeedle(ec1, ec1.Target, 200);
                        this.Update();
                        PlayerSpellHolyShock(ec1, ec1.Target, 200);
                        this.Update();
                        PlayerSpellFlameStrike(ec1, ec1.Target, 200);
                        this.Update();
                        PlayerSpellFrozenLance(ec1, ec1.Target, 200);
                        this.Update();
                        PlayerSpellVolcanicWave(ec1, ec1.Target, 200);
                        this.Update();
                        PlayerSpellWhiteOut(ec1, ec1.Target, 200);
                        this.Update();
                        if (this.difficulty == 1)
                        {
                            PlayerSpellLavaAnnihilation(ec1, ec1.Target, 1000, 750);
                        }
                        else
                        {
                            PlayerSpellLavaAnnihilation(ec1, ec1.Target, 1000, 0);
                        }
                        this.Update();
                        PlayerSpellWordOfPower(ec1, ec1.Target, 200);
                        this.Update();

                        PlayerDeathCheck(true); // [警告]：メソッド呼び出し層が誤っています。再構築してください。
                    }
                }

                if (this.ActivateTrcky > 0)
                {
                    this.ActivateTrcky--;
                    if (this.ActivateTrcky <= 0)
                    {
                        UpdateBattleText("『時間律（零空）の歯車』が黒い光を発し、突如回転を止めた！\r\n", 2000);
                        UpdateBattleText(ec1.Name + "：時間だ。　時が再び動きだす。『効果』の『発動』を『開始』する。\r\n", 1000);
                    }
                }

                if (this.ActivateGrowUp > 0)
                {
                    this.ActivateGrowUp--;
                    if (this.ActivateGrowUp <= 0)
                    {
                        UpdateBattleText("『時間律（盛栄）の歯車』が黒い光を発し、突如回転を止めた！\r\n", 2000);
                        UpdateBattleText(ec1.Name + "：時間だ。　時が再び動きだす。『効果』の『適用』は既に『完了』した。\r\n", 1000);
                    }
                }

                if (this.ActivateSword > 0)
                {
                    this.ActivateSword--;
                    if (this.ActivateSword <= 0)
                    {
                        UpdateBattleText("『時間律（絶剣）の歯車』が黒い光を発し、突如回転を止めた！\r\n", 1000);
                        UpdateBattleText(ec1.Name + "：時間だ。　時が再び動きだす。『一』にして『十』の『攻撃』、受け止めるがいい。\r\n", 2000);
                        UpdateBattleText(ec1.Target.Name + "に・・・", 1000);

                        PlayerSkillCarnageRush(ec1, ec1.Target, false);
                        UpdateBattleText("\r\n" + ec1.Name + "：そして『GaleWind』、再び、『地獄』なり。\r\n\r\n", 2000);
                        PlayerSkillCarnageRush(ec1, ec1.Target, false); // [警告]：GaleWindで２回行動する場合、本来この記述方法ではダメ。反則です。

                        PlayerDeathCheck(); // [警告]：メソッド呼び出し層が誤っています。再構築してください。
                    }
                }

                if (this.ActivateEverGreen > 0)
                {
                    this.ActivateEverGreen--;
                    if (this.ActivateEverGreen <= 0)
                    {
                        UpdateBattleText("『時間律（緑永）の歯車』が黒い光を発し、突如回転を止めた！\r\n", 1000);
                        UpdateBattleText(ec1.Name + "：時間だ。　時が再び動きだす。『永遠』なる『生命』とは、私そのもの。\r\n", 2000);
                    }
                }

                if (this.ActivateTotalEndTime > 0)
                {
                    this.ActivateTotalEndTime--;
                    if (this.ActivateTotalEndTime <= 0)
                    {
                        UpdateBattleText("『完全絶対時間律【終焉】の歯車』が黒い光を発し、突如回転を止めた！\r\n", 1000);
                        UpdateBattleText(ec1.Name + "：時間だ。　時が再び動きだす。『絶対』なる『訪れ』    　『終焉』を『受諾』せよ。\r\n", 2000);

                        if (this.difficulty == 1)
                        {
                            PlayerSpellLavaAnnihilation(ec1, ec1.Target, 1000, 750);
                        }
                        else
                        {
                            PlayerSpellLavaAnnihilation(ec1, ec1.Target, 1000, 0);
                        }

                        UpdateBattleText("周囲に設置されている歯車が全て回り始めた！！\r\n", 1000);
                        this.CannotResurrect = true;
                        for (int ii = 0; ii < playerList.Length; ii++)
                        {
                            playerList[ii].CurrentNoResurrection = 999;
                            playerList[ii].pbNoResurrection.Image = Image.FromFile(Database.BaseResourceFolder + "NoResurrection.bmp");
                            playerList[ii].pbNoResurrection.Update();
                        }
                        UpdateBattleText(ec1.Name + "：『終焉』　すなわち　『蘇生』は『叶わず』  『全て』が『死』を『受諾』する\r\n", 1000);
                        UpdateBattleText("はResurrectionをしても蘇生ができなくなった。\r\n");
                        PlayerDeathCheck(true); // [警告]：メソッド呼び出し層が誤っています。再構築してください。
                    }
                }
            }

            if (this.ec1 != null)
            {
                if (!ec1.Dead)
                {
                    if (ec1.CurrentWordOfLife > 0)
                    {
                        if (ec1.CurrentAbsoluteZero > 0)
                        {
                            UpdateBattleText(ec1.GetCharacterSentence(119));
                        }
                        else
                        {
                            int effectValue = ec1.TotalMind + ec1.TotalIntelligence;
                            UpdateBattleText("大自然から" + ec1.Name + "へ力強い脈動が行き渡る。" + effectValue.ToString() + "ライフ回復\r\n");
                            ec1.CurrentLife += effectValue;
                            UpdateLife(ec1);
                        }
                    }
                }
            }

            if (this.ec1 != null)
            {
                if (!ec1.Dead)
                {
                    if (ec1.CurrentBlackContract > 0)
                    {
                        UpdateBattleText(ec1.Name + "は悪魔への代償を支払う。" + Convert.ToString((int)((float)ec1.MaxLife / 10.0F)) + "ライフが削り取られる。\r\n");
                        ec1.CurrentLife -= (int)((float)ec1.MaxLife / 10.0F);
                        UpdateLife(ec1);
                    }
                }
            }

            if (ec1 != null)
            {
                if (!ec1.Dead)
                {
                    if (ec1.CurrentDamnation > 0)
                    {
                        Decimal effectValue = Convert.ToDecimal(Math.Ceiling((float)ec1.MaxLife / ec1.TotalMind));
                        UpdateBattleText("黒が" + this.ec1.Name + "の存在している空間を歪ませてくる。" + effectValue.ToString() + "のダメージ\r\n");
                        ec1.CurrentLife -= Convert.ToInt32(effectValue);
                        UpdateLife(ec1);
                    }

                    if (ec1.CurrentPoison > 0)
                    {
                        UpdateBattleText(ec1.Name + "は猛毒によりライフが削られてゆく・・・" + Convert.ToInt32((float)ec1.MaxLife / 10.0F).ToString() + "のダメージ\r\n");
                        ec1.CurrentLife -= Convert.ToInt32((float)ec1.MaxLife / 10.0F);
                        UpdateLife(ec1);
                    }
                }
            }


            if (this.ActivateTimeStop > 0 || this.ActivateTrcky > 0 || this.ActivateGrowUp > 0 || this.ActivateSword > 0 || this.ActivateEverGreen > 0 || this.ActivateTotalEndTime > 0)
            {
                return; // プレイヤーエフェクトは時間停止によりかからなくなる。
            }



            // プレイヤーへのエフェクト
            for (int ii = 0; ii < playerList.Length; ii++)
            {
                if (!playerList[ii].Dead)
                {
                    if ((playerList[ii].Accessory != null) && (playerList[ii].Accessory.Name == "再生の紋章"))
                    {
                        if (playerList[ii].CurrentAbsoluteZero > 0)
                        {
                            UpdateBattleText("しかし" + playerList[ii].Name + "は絶対零度効果により自然の恩恵を受けとれない！\r\n");
                        }
                        else
                        {
                            int effectValue = (playerList[ii].MaxLife * playerList[ii].Accessory.MaxValue / 100);
                            UpdateBattleText(playerList[ii].Accessory.Name + "から生命の力が流れてくる。" + effectValue.ToString() + "ライフ回復\r\n");
                            playerList[ii].CurrentLife += effectValue;
                            UpdateLife(playerList[ii]);
                        }
                    }

                    if (playerList[ii].CurrentWordOfLife > 0 && !playerList[ii].Dead)
                    {
                        if (playerList[ii].CurrentAbsoluteZero > 0)
                        {
                            UpdateBattleText("しかし" + playerList[ii].Name + "は絶対零度効果により自然の恩恵を受けとれない！\r\n");
                        }
                        else
                        {
                            int effectValue = playerList[ii].TotalMind + playerList[ii].TotalIntelligence;
                            UpdateBattleText("大自然から" + playerList[ii].Name + "へ力強い脈動が行き渡る。" + effectValue.ToString() + "ライフ回復\r\n");
                            playerList[ii].CurrentLife += effectValue;
                            UpdateLife(playerList[ii]);
                        }
                    }

                    if (playerList[ii].CurrentPoison > 0)
                    {
                        UpdateBattleText(playerList[ii].Name + "は猛毒によりライフが削られてゆく・・・" + Convert.ToInt32((float)playerList[ii].MaxLife / 10.0F).ToString() + "のダメージ\r\n");
                        playerList[ii].CurrentLife -= Convert.ToInt32((float)playerList[ii].MaxLife / 10.0F);
                        UpdateLife(playerList[ii]);
                    }
                }
            }

            //if (this.mc != null)
            //{
            //    if (!mc.Dead)
            //    {
            //        if (mc.CurrentWordOfLife > 0 && !mc.Dead)
            //        {
            //            UpdateBattleText("大自然から" + mc.Name + "へ力強い脈動が行き渡る。" + Convert.ToString((mc.Mind + mc.BuffMind) * 1 + mc.Intelligence * 1) + "ライフ回復\r\n");
            //            mc.CurrentLife += (mc.Mind + mc.BuffMind) * 1 + mc.Intelligence * 1;
            //            UpdateLife(mc);
            //        }
            //    }
            //}
            //if (this.sc != null)
            //{
            //    if (!sc.Dead)
            //    {
            //        if (sc.CurrentWordOfLife > 0 && !sc.Dead)
            //        {
            //            UpdateBattleText("大自然から" + sc.Name + "へ力強い脈動が行き渡る。" + Convert.ToString((sc.Mind + sc.BuffMind) * 1 + sc.Intelligence * 1) + "ライフ回復\r\n");
            //            sc.CurrentLife += (sc.Mind + sc.BuffMind) * 1 + sc.Intelligence * 1;
            //            UpdateLife(sc);
            //        }
            //    }
            //}

            for (int ii = 0; ii < playerList.Length; ii++)
            {
                if (!playerList[ii].Dead)
                {
                    if (playerList[ii].CurrentBlackContract > 0 && !playerList[ii].Dead)
                    {
                        Decimal effectValue = Convert.ToDecimal(Math.Ceiling((float)playerList[ii].MaxLife / 10.0F));//playerList[ii].TotalMind));
                        UpdateBattleText(playerList[ii].Name + "は悪魔への代償を支払う。" + effectValue.ToString() + "ライフが削り取られる。\r\n");
                        playerList[ii].CurrentLife -= (int)(effectValue);
                        UpdateLife(playerList[ii]);
                    }
                }
            }
            //if (this.mc != null)
            //{
            //    if (!mc.Dead)
            //    {
            //        if (mc.CurrentBlackContract > 0 && !mc.Dead)
            //        {
            //            UpdateBattleText(mc.Name + "は悪魔への代償を支払う。" + Convert.ToString((int)((float)mc.MaxLife / 10.0F)) + "ライフが削り取られる。\r\n");
            //            mc.CurrentLife -= (int)((float)mc.MaxLife / 10.0F);
            //            UpdateLife(mc);
            //        }
            //    }
            //}
            //if (this.sc != null)
            //{
            //    if (!sc.Dead)
            //    {
            //        if (sc.CurrentBlackContract > 0 && !sc.Dead)
            //        {
            //            UpdateBattleText(sc.Name + "は悪魔への代償を支払う。" + Convert.ToString((int)((float)sc.MaxLife / 10.0F)) + "ライフが削り取られる。\r\n");
            //            sc.CurrentLife -= (int)((float)sc.MaxLife / 10.0F);
            //            UpdateLife(sc);
            //        }
            //    }
            //}
            // エネミーへのエフェクト
            for (int ii = 0; ii < playerList.Length; ii++)
            {
                if (playerList[ii].CurrentPainfulInsanity > 0 && !playerList[ii].Dead)
                {
                    int effectValue = playerList[ii].TotalMind * 3;
                    UpdateBattleText(playerList[ii].Name + "は" + this.ec1.Name + "の心へ直接的なダメージを発生させている。" + effectValue.ToString() + "のダメージ\r\n");
                    ec1.CurrentLife -= effectValue;
                    UpdateLife(ec1);
                }
            }
            //if (this.CurentPainfulInsanity)
            //{
            //    UpdateBattleText(mc.Name + "は" + this.ec1.Name + "の心へ直接的なダメージを発生させている。" + (mc.Mind + mc.BuffMind) * 3 + "のダメージ\r\n");
            //    ec1.CurrentLife -= (mc.Mind + mc.BuffMind) * 3;
            //    UpdateLife(ec1);
            //}

            for (int ii = 0; ii < playerList.Length; ii++)
            {
                if (playerList[ii].CurrentDamnation > 0 && !playerList[ii].Dead)
                {
                    UpdateBattleText("黒が" + playerList[ii].Name + "の存在している空間を歪ませてくる。" + Convert.ToInt32((float)playerList[ii].MaxLife / 10.0F).ToString() + "のダメージ\r\n");
                    playerList[ii].CurrentLife -= Convert.ToInt32((float)playerList[ii].MaxLife / 10.0F);
                    UpdateLife(playerList[ii]);
                }
            }

            //if (this.CurrentDamnation > 0)
            //{
            //    UpdateBattleText("黒が" + this.ec1.Name + "の存在している空間を歪ませてくる。" + mc.Intelligence * 3 + "のダメージ\r\n");
            //    ec1.CurrentLife -= mc.Intelligence * 3;
            //    UpdateLife(ec1);
            //}
        }

        private void CleanUpStep()
        {
            if (this.ec1 != null)
            {
                if (ec1.Name == "五階の守護者：Bystander")
                {
                    if (ec1.CurrentStunning > 0)
                    {
                        UpdateBattleText(ec1.Name + "の後方にある『常極の歯車』が１度回転した。スタン効果が消えている！\r\n");
                        ec1.RecoverStunning();
                    }
                    if (ec1.CurrentSilence > 0)
                    {
                        UpdateBattleText(ec1.Name + "の後方にある『常極の歯車』が１度回転した。沈黙の効果が消えている！\r\n");
                        ec1.RecoverSilence();
                    }
                    if (ec1.CurrentPoison > 0)
                    {
                        UpdateBattleText(ec1.Name + "の後方にある『常極の歯車』が１度回転した。猛毒の効果が消えている！\r\n");
                        ec1.RecoverPoison();
                    }
                    if (ec1.CurrentTemptation > 0)
                    {
                        UpdateBattleText(ec1.Name + "の後方にある『常極の歯車』が１度回転した。誘惑の効果が消えている！\r\n");
                        ec1.RecoverTemptation();
                    }
                    if (ec1.CurrentFrozen > 0)
                    {
                        UpdateBattleText(ec1.Name + "の後方にある『常極の歯車』が１度回転した。凍結の効果が消えている！\r\n");
                        ec1.RecoverFrozen();
                    }
                    if (ec1.CurrentParalyze > 0)
                    {
                        UpdateBattleText(ec1.Name + "の後方にある『常極の歯車』が１度回転した。麻痺の効果が消えている！\r\n");
                        ec1.RecoverParalyze();
                    }

                    if (ec1.CurrentAbsoluteZero > 0)
                    {
                        UpdateBattleText(ec1.Name + "の後方にある『常極の歯車』が" + ec1.CurrentAbsoluteZero.ToString() + "度回転した。\r\n");
                        ec1.CurrentAbsoluteZero = 0;
                        ec1.pbAbsoluteZero.Image = null;
                        ec1.pbAbsoluteZero.Update();
                        UpdateBattleText("AbsoluteZeroの永続期間が瞬時に終了された！\r\n", 1000);
                    }
                }

                MainCharacter beforeTarget = ec1.Target;
                ec1.CurrentSkillPoint++;
                UpdateMCSkillPoint(ec1);
                ec1.CleanUpEffect();
                if (this.ActivateTimeStop > 0 || this.ActivateTrcky > 0 || this.ActivateGrowUp > 0 || this.ActivateSword > 0 || this.ActivateEverGreen > 0 || this.ActivateTotalEndTime > 0)
                {
                    Application.DoEvents();

                    Attack.Enabled = false;
                    SpellBox.Enabled = false;
                    SkillBox.Enabled = false;
                    ItemBox.Enabled = false;
                    Defense.Enabled = false;
                    RunAway.Enabled = false;
                    playerBack.Enabled = false;

                    ec1.Target = beforeTarget;
                    currentPlayerLabel.Text = "時間停止";
                    timerPlayerThrough.Interval = 300;
                    timerPlayerThrough.Enabled = true;
                    timerPlayerThrough.Start();
                    return; // 時間停止効果
                }
            }

            for (int ii = 0; ii < playerList.Length; ii++)
            {
                if (!playerList[ii].Dead)
                {
                    playerList[ii].CurrentSkillPoint++;
                    UpdateMCSkillPoint(playerList[ii]);

                    if ((playerList[ii].Accessory != null) && (playerList[ii].Accessory.Name == "鋼鉄の石像"))
                    {
                        Random rd = new Random(DateTime.Now.Millisecond * Environment.TickCount);
                        if (rd.Next(1, 101) <= playerList[ii].Accessory.MinValue)
                        {
                            if (playerList[ii].CurrentStunning > 0)
                            {
                                UpdateBattleText(playerList[ii].Name + "が装備している鋼鉄の石像が光り輝いた！\r\n", 1000);
                                playerList[ii].CurrentStunning = 0;
                                playerList[ii].pbStun.Image = null;
                                playerList[ii].pbStun.Update();
                                UpdateBattleText(playerList[ii].Name + "にかかっているスタン効果が解除された。\r\n");
                            }
                        }
                    }

                    if ((playerList[ii].Accessory != null) && (playerList[ii].Accessory.Name == "天使の契約書"))
                    {
                        if ((playerList[ii].CurrentStunning > 0)
                            || (playerList[ii].CurrentSilence > 0)
                            || (playerList[ii].CurrentPoison > 0)
                            || (playerList[ii].CurrentTemptation > 0)
                            || (playerList[ii].CurrentFrozen > 0)
                            || (playerList[ii].CurrentParalyze > 0))
                        {
                            UpdateBattleText(playerList[ii].Name + "が装備している天使の契約書が光り輝いた！\r\n", 1000);
                            playerList[ii].CurrentStunning = 0;
                            playerList[ii].CurrentSilence = 0;
                            playerList[ii].CurrentPoison = 0;
                            playerList[ii].CurrentTemptation = 0;
                            playerList[ii].CurrentFrozen = 0;
                            playerList[ii].CurrentParalyze = 0;
                            playerList[ii].pbStun.Image = null;
                            playerList[ii].pbStun.Update();
                            playerList[ii].pbSilence.Image = null;
                            playerList[ii].pbSilence.Update();
                            playerList[ii].pbPoison.Image = null;
                            playerList[ii].pbPoison.Update();
                            playerList[ii].pbTemptation.Image = null;
                            playerList[ii].pbTemptation.Update();
                            playerList[ii].pbFrozen.Image = null;
                            playerList[ii].pbFrozen.Update();
                            playerList[ii].pbParalyze.Image = null;
                            playerList[ii].pbParalyze.Update();
                            UpdateBattleText(playerList[ii].Name + "にかかっている負の影響が全て解除された。\r\n");
                        }
                    }
                }
                playerList[ii].CleanUpEffect();
            }
            //if (this.mc != null)
            //{
            //    mc.CurrentSkillPoint++;
            //    UpdateMCSkillPoint(mc);
            //    mc.CleanUpEffect();
            //}

            //if (this.sc != null)
            //{
            //    sc.CurrentSkillPoint++;
            //    UpdateMCSkillPoint(sc);
            //    sc.CleanUpEffect();
            //}
        

            if (CurrentTimeStop > 0)
            {
                CurrentTimeStop--;
            }

            if (CurrentCrushingBlowEnemy > 0)
            {
                CurrentCrushingBlowEnemy--;
            }

            this.bossAlreadyActionNum = -1;

            UpdateCurrentPlayerLabel();

        }

        private bool ignoreApplicationDoEvent = false;
        public bool IgnoreApplicationDoEvent
        {
            get { return ignoreApplicationDoEvent; }
            set { ignoreApplicationDoEvent = value; }
        }
        private void UpdateCurrentPlayerLabel()
        {
            for (int ii = 0; ii < playerList.Length; ii++)
            {
                if (!playerList[ii].Dead && playerList[ii].CurrentFrozen <= 0 && playerList[ii].CurrentStunning <= 0 && playerList[ii].CurrentParalyze <= 0)
                {
                    currentPlayerLabel.Text = playerList[ii].Name;
                    if (!ignoreApplicationDoEvent)
                    {
                        Application.DoEvents();
                    }
                    else
                    {
                        ignoreApplicationDoEvent = true;
                    }
                    Attack.Enabled = true;
                    SpellBox.Enabled = true;
                    SkillBox.Enabled = true;
                    ItemBox.Enabled = true;
                    Defense.Enabled = true;
                    RunAway.Enabled = true;
                    playerBack.Enabled = true;
                    UpdateCurrentFocus(playerList[ii].BeforePA);
                    return;
                }
            }

            Attack.Enabled = false;
            SpellBox.Enabled = false;
            SkillBox.Enabled = false;
            ItemBox.Enabled = false;
            Defense.Enabled = false;
            RunAway.Enabled = false;
            playerBack.Enabled = false;

            currentPlayerLabel.Text = "行動不可";
            timerPlayerThrough.Interval = 1000;
            timerPlayerThrough.Enabled = true;
            timerPlayerThrough.Start();
            return;
            //if (mc == null && sc == null)
            //{
            //    // ありえないケース
            //    currentPlayerLabel.Text = String.Empty;
            //}
            //else if (mc != null && sc == null)
            //{
            //    if (!mc.Dead)
            //    {
            //        currentPlayerLabel.Text = mc.Name;
            //    }
            //    else
            //    {
            //        currentPlayerLabel.Text = String.Empty;
            //    }
            //}
            //else if (mc == null && sc != null)
            //{
            //    if (!sc.Dead)
            //    {
            //        currentPlayerLabel.Text = sc.Name;
            //    }
            //    else
            //    {
            //        currentPlayerLabel.Text = String.Empty;
            //    }
            //}
            //else if (mc != null && sc != null)
            //{
            //    if (!mc.Dead && !sc.Dead)
            //    {
            //        currentPlayerLabel.Text = mc.Name;
            //    }
            //    else if (mc.Dead && !sc.Dead)
            //    {
            //        currentPlayerLabel.Text = sc.Name;
            //    }
            //    else if (!mc.Dead && sc.Dead)
            //    {
            //        currentPlayerLabel.Text = mc.Name;
            //    }
            //    else if (mc.Dead && sc.Dead)
            //    {
            //        currentPlayerLabel.Text = String.Empty;
            //    }
            //}
        }

        private void timerPlayerThrough_Tick(object sender, EventArgs e)
        {
            timerPlayerThrough.Stop();
            timerPlayerThrough.Enabled = false;

            BattleStart();
        }

        private bool RunAwayStep(PlayerAction pa)
        {
            if (pa == PlayerAction.RunAway)
            {
                UpdateBattleText(ec1.Name + "から逃げようとした・・・\r\n");
                Random rd = new Random();
                if (rd.Next(0, 11) >= 2) // [警告] 逃げられる確率を操作してください。
                {
                    return true;
                }
                else
                {
                    UpdateBattleText("周り込まれてしまった！逃げられなかった！\r\n");
                    return false;
                }
            }

            return false;
        }

        private bool PlayerDeathCheck()
        {
            return PlayerDeathCheck(false);
        }
        private bool PlayerDeathCheck(bool ignoreCheckAllDie)
        {
            for (int ii = 0; ii < playerList.Length; ii++)
            {
                if (playerList[ii].CurrentLife <= 0 && !playerList[ii].Dead)
                {
                    if (playerList[ii].CurrentStanceOfDeath > 0)
                    {
                        playerList[ii].CurrentStanceOfDeath = 0;
                        playerList[ii].pbStanceOfDeath.Image = null;
                        playerList[ii].pbStanceOfDeath.Update();

                        UpdateBattleText(playerList[ii].Name + "は致死の狭間で生き残った！！\r\n");
                        playerList[ii].CurrentLife = 1;
                        UpdateLife(playerList[ii]);
                    }
                    else
                    {
                        UpdateBattleText(playerList[ii].Name + "は死んでしまった！！\r\n");
                        playerList[ii].Dead = true;
                        playerList[ii].labelName.ForeColor = Color.Red;
                        playerList[ii].labelLife.ForeColor = Color.Red;
                        playerList[ii].labelSkill.ForeColor = Color.Red;
                        playerList[ii].labelMana.ForeColor = Color.Red;
                        playerList[ii].labelName.Update();
                        playerList[ii].labelLife.Update();
                        playerList[ii].labelSkill.Update();
                        playerList[ii].labelMana.Update();
                    }
                }
            }

            //if (mc != null)
            //{
            //    if (mc.CurrentLife <= 0 && !mc.Dead)
            //    {
            //        UpdateBattleText(mc.Name + "は死んでしまった！！\r\n");
            //        mc.Dead = true;
            //        mc.labelName.ForeColor = Color.Red;
            //        mc.labelLife.ForeColor = Color.Red;
            //        mc.labelSkill.ForeColor = Color.Red;
            //        mc.labelMana.ForeColor = Color.Red;
            //    }
            //}

            //if (sc != null)
            //{
            //    if (sc.CurrentLife <= 0 && !sc.Dead)
            //    {
            //        UpdateBattleText(sc.Name + "は死んでしまった！！\r\n");
            //        sc.Dead = true;
            //        sc.labelName.ForeColor = Color.Red;
            //        sc.labelLife.ForeColor = Color.Red;
            //        sc.labelSkill.ForeColor = Color.Red;
            //        sc.labelMana.ForeColor = Color.Red;
            //    }
            //}

            // [警告]：もう少しきれいになりませんか？人数が増えてきたらむちゃくちゃになります。
            //bool allDead = false;
            //if (mc == null && sc == null)
            //{
            //    // ありえないケース
            //}
            //else if (mc != null && sc == null)
            //{
            //    if (mc.Dead) allDead = true;
            //}
            //else if (mc == null && sc != null)
            //{
            //    if (sc.Dead) allDead = true;
            //}
            //else if (mc != null && sc != null)
            //{
            //    if (mc.Dead && sc.Dead) allDead = true;
            //}
            for (int ii = 0; ii < playerList.Length; ii++)
            {
                if (!playerList[ii].Dead)
                {
                    return false; // いずれかのプレイヤーが生きている場合は全滅とみなさずにココで返す。
                }
            }

            //if (allDead)
            //{
            if (!this.acceptLose && !ignoreCheckAllDie)
            {
                UpdateBattleText("全滅しました・・・もう一度始めからやり直しますか？\r\n");
                using (YesNoRequest yesno = new YesNoRequest())
                {
                    yesno.StartPosition = FormStartPosition.CenterParent;
                    yesno.ShowDialog();
                    if (yesno.DialogResult == DialogResult.Yes)
                    {
                        this.DialogResult = DialogResult.Retry;
                        return true;
                    }
                    else
                    {
                        this.DialogResult = DialogResult.Ignore;
                        //Application.Exit();
                        return true;
                    }
                }
            }
            else
            {
                this.DialogResult = DialogResult.Ignore;
                return true;
            }
            //}
            //return false;
        }

        private bool EnemyDeathCheck()
        {
            if (this.ec1.CurrentLife <= 0)
            {
                enemyNameLabel1.Text = "";
                enemyNameLabel1.Update();
                UpdateBattleText(ec1.Name + " を倒した。\r\n", 0);
                UpdateBattleText(ec1.Gold + " Goldを獲得。\r\n", 0);
                UpdateBattleText(ec1.Exp + " 経験値を獲得\r\n");
                System.Threading.Thread.Sleep(1500);
                this.Close();
                return true;
            }

            return false;
        }

        private void UpdateBattleText(string text)
        {
            UpdateBattleText(text, 500);
        }
        private void UpdateBattleText(string text, int sleepTime)
        {
            textBox1.Text += text;
            //カレット位置を末尾に移動
            textBox1.SelectionStart = textBox1.Text.Length;
            //テキストボックスにフォーカスを移動
            textBox1.Focus();
            //カレット位置までスクロール
            textBox1.ScrollToCaret();
            this.Update();

            //int counter = 0;
            //while (true)
            //{
            //    counter++;
            //    System.Threading.Thread.Sleep(1);
            //    // これを入れておくと、ボタンDisable時の不要なマウスイベントや戦闘テキスト表示中のマウス連打バグに対応できるが、低スペック＋並列動作マシンに対して悪影響。
            //    //Application.DoEvents();
            //    if (counter >= (sleepTime / this.battleSpeed))
            //    {
            //        break;
            //    }
            //}
            System.Threading.Thread.Sleep(sleepTime / this.battleSpeed);
            Application.DoEvents(); // 更新タイミングを軽減するため、Sleep内ではなく、外で１回にすることとした。
        }

        private void UpdateLife(MainCharacter target)
        {
            if (target == this.mc)
            {
                lifeLabel1.Text = this.mc.CurrentLife.ToString() + " / " + this.mc.MaxLife.ToString();
                lifeLabel1.Update();
            }
            else if (target == this.sc)
            {
                lifeLabel2.Text = this.sc.CurrentLife.ToString() + " / " + this.sc.MaxLife.ToString();
                lifeLabel2.Update();
            }
            else if (target == this.tc)
            {
                lifeLabel3.Text = this.tc.CurrentLife.ToString() + " / " + this.tc.MaxLife.ToString();
                lifeLabel3.Update();
            }
            else if (target == this.ec1)
            {
                enemyLifeLabel1.Text = ec1.CurrentLife.ToString() + " / " + ec1.MaxLife.ToString();
                enemyLifeLabel1.Update();
            }
        }
        private void UpdateMCSkillPoint(MainCharacter target)
        {
            if (target == this.mc)
            {
                this.skillLabel1.Text = mc.CurrentSkillPoint.ToString() + " / " + mc.MaxSkillPoint.ToString();
                this.skillLabel1.Update();
            }
            else if (target == this.sc)
            {
                this.skillLabel2.Text = sc.CurrentSkillPoint.ToString() + " / " + sc.MaxSkillPoint.ToString();
                this.skillLabel2.Update();
            }
            else if (target == this.tc)
            {
                this.skillLabel3.Text = tc.CurrentSkillPoint.ToString() + " / " + tc.MaxSkillPoint.ToString();
                this.skillLabel3.Update();
            }
            else if (target == this.ec1)
            {
                enemySkillLabel1.Text = ec1.CurrentSkillPoint.ToString() + " / " + ec1.MaxSkillPoint.ToString();
                enemySkillLabel1.Update();
            }
        }
        private void UpdateMCMana(MainCharacter target)
        {
            if (target == this.mc)
            {
                this.manaLabel1.Text = mc.CurrentMana.ToString() + " / " + mc.MaxMana.ToString();
                this.manaLabel1.Update();
            }
            else if (target == this.sc)
            {
                this.manaLabel2.Text = sc.CurrentMana.ToString() + " / " + sc.MaxMana.ToString();
                this.manaLabel2.Update();
            }
            else if (target == this.tc)
            {
                this.manaLabel3.Text = tc.CurrentMana.ToString() + " / " + tc.MaxMana.ToString();
                this.manaLabel3.Update();
            }
            else if (target == this.ec1)
            {
                enemyManaLabel1.Text = ec1.CurrentMana.ToString() + " / " + ec1.MaxMana.ToString();
                enemyManaLabel1.Update();
            }
        }

        private void BattleEnemy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                playerBack_Click(sender, e);
            }
        }

        private PopUpMini popupInfo;

        private void pbBuffGroup_MouseLeave(object sender, EventArgs e)
        {
            if (popupInfo != null)
            {
                popupInfo.Close();
                popupInfo = null;
            }
        }

        private void pbBuffGroup_MouseMove(object sender, MouseEventArgs e)
        {
            if (popupInfo == null)
            {
                popupInfo = new PopUpMini();
            }

            MainCharacter[] playerList = new MainCharacter[4];
            playerList[0] = mc;
            playerList[1] = sc;
            playerList[2] = tc;
            playerList[3] = ec1;
            // [警告]：将来新しい魔法・スキルを構築していく場合、以下の記述が漏れてしまう可能性があるので、記述方法を再検討してください。
            for (int ii = 0; ii < playerList.Length; ii++)
            {
                if (playerList[ii] == null) continue;
                if (playerList[ii].pbAbsorbWater.Equals(sender) && playerList[ii].pbAbsorbWater.Image == null) return;
                if (playerList[ii].pbProtection.Equals(sender) && playerList[ii].pbProtection.Image == null) return;
                if (playerList[ii].pbSaintPower.Equals(sender) && playerList[ii].pbSaintPower.Image == null) return;
                if (playerList[ii].pbShadowPact.Equals(sender) && playerList[ii].pbShadowPact.Image == null) return;
                if (playerList[ii].pbWordOfLife.Equals(sender) && playerList[ii].pbWordOfLife.Image == null) return;
                if (playerList[ii].pbGlory.Equals(sender) && playerList[ii].pbGlory.Image == null) return;
                if (playerList[ii].pbFlameAura.Equals(sender) && playerList[ii].pbFlameAura.Image == null) return;
                if (playerList[ii].pbOneImmunity.Equals(sender) && playerList[ii].pbOneImmunity.Image == null) return;
                if (playerList[ii].pbGaleWind.Equals(sender) && playerList[ii].pbGaleWind.Image == null) return;
                if (playerList[ii].pbWordOfFortune.Equals(sender) && playerList[ii].pbWordOfFortune.Image == null) return;
                if (playerList[ii].pbHeatBoost.Equals(sender) && playerList[ii].pbHeatBoost.Image == null) return;
                if (playerList[ii].pbBloodyVengeance.Equals(sender) && playerList[ii].pbBloodyVengeance.Image == null) return;
                if (playerList[ii].pbRiseOfImage.Equals(sender) && playerList[ii].pbRiseOfImage.Image == null) return;
                if (playerList[ii].pbImmortalRave.Equals(sender) && playerList[ii].pbImmortalRave.Image == null) return;
                if (playerList[ii].pbBlackContract.Equals(sender) && playerList[ii].pbBlackContract.Image == null) return;
                if (playerList[ii].pbAetherDrive.Equals(sender) && playerList[ii].pbAetherDrive.Image == null) return;
                if (playerList[ii].pbEternalPresence.Equals(sender) && playerList[ii].pbEternalPresence.Image == null) return;
                if (playerList[ii].pbMirrorImage.Equals(sender) && playerList[ii].pbMirrorImage.Image == null) return;
                if (playerList[ii].pbTruthVision.Equals(sender) && playerList[ii].pbTruthVision.Image == null) return;
                if (playerList[ii].pbStanceOfFlow.Equals(sender) && playerList[ii].pbStanceOfFlow.Image == null) return;
                if (playerList[ii].pbPromisedKnowledge.Equals(sender) && playerList[ii].pbPromisedKnowledge.Image == null) return;
                if (playerList[ii].pbHighEmotionality.Equals(sender) && playerList[ii].pbHighEmotionality.Image == null) return;
                if (playerList[ii].pbVoidExtraction.Equals(sender) && playerList[ii].pbVoidExtraction.Image == null) return;
                if (playerList[ii].pbAntiStun.Equals(sender) && playerList[ii].pbAntiStun.Image == null) return;
                if (playerList[ii].pbStanceOfDeath.Equals(sender) && playerList[ii].pbStanceOfDeath.Image == null) return;
                if (playerList[ii].pbDeflection.Equals(sender) && playerList[ii].pbDeflection.Image == null) return;

                if (playerList[ii].pbPainfulInsanity.Equals(sender) && playerList[ii].pbPainfulInsanity.Image == null) return;
                if (playerList[ii].pbDamnation.Equals(sender) && playerList[ii].pbDamnation.Image == null) return;
                if (playerList[ii].pbAbsoluteZero.Equals(sender) && playerList[ii].pbAbsoluteZero.Image == null) return;
                if (playerList[ii].pbNothingOfNothingness.Equals(sender) && playerList[ii].pbNothingOfNothingness.Image == null) return;

                if (playerList[ii].pbPoison.Equals(sender) && playerList[ii].pbPoison.Image == null) return;
                if (playerList[ii].pbStun.Equals(sender) && playerList[ii].pbStun.Image == null) return;
                if (playerList[ii].pbSilence.Equals(sender) && playerList[ii].pbSilence.Image == null) return;
                if (playerList[ii].pbParalyze.Equals(sender) && playerList[ii].pbParalyze.Image == null) return;
                if (playerList[ii].pbFrozen.Equals(sender) && playerList[ii].pbFrozen.Image == null) return;
                if (playerList[ii].pbTemptation.Equals(sender) && playerList[ii].pbTemptation.Image == null) return;
                if (playerList[ii].pbNoResurrection.Equals(sender) && playerList[ii].pbNoResurrection.Image == null) return;
                //////////////////////////////////////////////////////////////////////////////////////////////////////////////
                if (playerList[ii].pbAbsorbWater.Equals(sender)) { popupInfo.CurrentInfo = "魔法防御率UP"; break; }
                if (playerList[ii].pbProtection.Equals(sender)) { popupInfo.CurrentInfo = "物理防御率UP"; break; }
                if (playerList[ii].pbSaintPower.Equals(sender)) { popupInfo.CurrentInfo = "物理攻撃力UP"; break; }
                if (playerList[ii].pbShadowPact.Equals(sender)) { popupInfo.CurrentInfo = "魔法攻撃力UP"; break; }
                if (playerList[ii].pbWordOfLife.Equals(sender)) { popupInfo.CurrentInfo = "毎ターン自然回復"; break; }
                if (playerList[ii].pbGlory.Equals(sender)) { popupInfo.CurrentInfo = "ライフ回復＋攻撃"; break; }
                if (playerList[ii].pbFlameAura.Equals(sender)) { popupInfo.CurrentInfo = "攻撃＋炎ダメージ"; break; }
                if (playerList[ii].pbOneImmunity.Equals(sender)) { popupInfo.CurrentInfo = "ダメージ無効化"; break; }
                if (playerList[ii].pbGaleWind.Equals(sender)) { popupInfo.CurrentInfo = "二回行動"; break; }
                if (playerList[ii].pbWordOfFortune.Equals(sender)) { popupInfo.CurrentInfo = "100%クリティカル"; break; }
                if (playerList[ii].pbHeatBoost.Equals(sender)) { popupInfo.CurrentInfo = "技パラメタUP"; break; }
                if (playerList[ii].pbBloodyVengeance.Equals(sender)) { popupInfo.CurrentInfo = "力パラメタUP"; break; }
                if (playerList[ii].pbRiseOfImage.Equals(sender)) { popupInfo.CurrentInfo = "心パラメタUP"; break; }
                if (playerList[ii].pbImmortalRave.Equals(sender)) { popupInfo.CurrentInfo = "攻撃＋炎魔法（弱・中・強）"; break; }
                if (playerList[ii].pbBlackContract.Equals(sender)) { popupInfo.CurrentInfo = "魔法・スキルコスト０"; break; }
                if (playerList[ii].pbAetherDrive.Equals(sender)) { popupInfo.CurrentInfo = "物理攻撃力2倍、物理防御率2倍"; break; }
                if (playerList[ii].pbEternalPresence.Equals(sender)) { popupInfo.CurrentInfo = "物理攻撃UP、物理防御UP、魔法攻撃UP、魔法防御UP"; break; }
                if (playerList[ii].pbMirrorImage.Equals(sender)) { popupInfo.CurrentInfo = "魔法ダメージ反射"; break; }
                if (playerList[ii].pbTruthVision.Equals(sender)) { popupInfo.CurrentInfo = "対象パラメタUP無効化"; break; }
                if (playerList[ii].pbStanceOfFlow.Equals(sender)) { popupInfo.CurrentInfo = "100%後攻"; break; }
                if (playerList[ii].pbPromisedKnowledge.Equals(sender)) { popupInfo.CurrentInfo = "知パラメタUP"; break; }
                if (playerList[ii].pbHighEmotionality.Equals(sender)) { popupInfo.CurrentInfo = "力・技・知・心パラメタUP"; break; }
                if (playerList[ii].pbVoidExtraction.Equals(sender)) { popupInfo.CurrentInfo = "力 or 技 or 知 or 心パラメタUP"; break; }
                if (playerList[ii].pbAntiStun.Equals(sender)) { popupInfo.CurrentInfo = "スタン無効化"; break; }
                if (playerList[ii].pbStanceOfDeath.Equals(sender)) { popupInfo.CurrentInfo = "致死ダメージ死亡回避"; break; }
                if (playerList[ii].pbDeflection.Equals(sender)) { popupInfo.CurrentInfo = "物理ダメージ反射"; break; }

                if (playerList[ii].pbPainfulInsanity.Equals(sender)) { popupInfo.CurrentInfo = "毎ターン相手へダメージ"; break; }
                if (playerList[ii].pbDamnation.Equals(sender)) { popupInfo.CurrentInfo = "毎ターン自分にダメージ"; break; }
                if (playerList[ii].pbAbsoluteZero.Equals(sender)) { popupInfo.CurrentInfo = "ライフ・マナ・スキル回復不可、魔法不可、スキル不可、防御不可"; break; } // 後編編集
                if (playerList[ii].pbNothingOfNothingness.Equals(sender)) { popupInfo.CurrentInfo = "無効魔法・無効スキルを無効化"; break; }

                if (playerList[ii].pbPoison.Equals(sender)) { popupInfo.CurrentInfo = "【猛毒】毎ターンダメージ"; break; }
                if (playerList[ii].pbStun.Equals(sender)) { popupInfo.CurrentInfo = "【スタン】行動不可"; break; }
                if (playerList[ii].pbSilence.Equals(sender)) { popupInfo.CurrentInfo = "【沈黙】魔法詠唱不可"; break; }
                if (playerList[ii].pbParalyze.Equals(sender)) { popupInfo.CurrentInfo = "【麻痺】行動不可"; break; }
                if (playerList[ii].pbFrozen.Equals(sender)) { popupInfo.CurrentInfo = "【凍結】行動不可"; break; }
                if (playerList[ii].pbTemptation.Equals(sender)) { popupInfo.CurrentInfo = "【誘惑】行動不可"; break; }
                if (playerList[ii].pbNoResurrection.Equals(sender)) { popupInfo.CurrentInfo = "【死印】蘇生不可"; break; }
            }


            popupInfo.StartPosition = FormStartPosition.Manual;
            popupInfo.Location = new Point(this.Location.X + ((PictureBox)sender).Location.X + e.X, this.Location.Y + ((PictureBox)sender).Location.Y + e.Y + 30);
            popupInfo.Show();
        }

        private bool CheckDodge(MainCharacter player)
        {
            return CheckDodge(player, false);
        }

        private bool CheckDodge(MainCharacter player, bool ignoreDodge)
        {
            if ((player.Accessory != null) && (player.Accessory.Name == "身かわしのマント") && 
                (player.CurrentStunning <= 0) &&
                (player.CurrentFrozen <= 0) &&
                (player.CurrentParalyze <= 0) &&
                (player.CurrentTemptation <= 0))
            {
                Random rd = new Random(DateTime.Now.Millisecond * Environment.TickCount);
                if (rd.Next(1, 101) <= player.Accessory.MinValue)
                {
                    UpdateBattleText(player.Name + "は" + player.Accessory.Name + "の効果で素早く身をかわした！\r\n");
                    return true;
                }
            }
            return false;
        }
        
        private void BattleEnemy_MouseDown(object sender, MouseEventArgs e)
        {
            if (!Attack.Enabled)
            {
                return;
            }
        }

        private void CommandButton_Enter(object sender, EventArgs e)
        {
            // 芋プログラミングで対応
            if (sender.Equals(Attack))
            {
                Attack.BackColor = Color.Khaki;
                SkillBox.BackColor = Color.AliceBlue;
                SpellBox.BackColor = Color.AliceBlue;
                ItemBox.BackColor = Color.AliceBlue;
                Defense.BackColor = Color.AliceBlue;
                RunAway.BackColor = Color.AliceBlue;
                playerBack.BackColor = Color.AliceBlue;
            }
            else if (sender.Equals(SkillBox))
            {
                Attack.BackColor = Color.AliceBlue;
                SkillBox.BackColor = Color.Khaki;
                SpellBox.BackColor = Color.AliceBlue;
                ItemBox.BackColor = Color.AliceBlue;
                Defense.BackColor = Color.AliceBlue;
                RunAway.BackColor = Color.AliceBlue;
                playerBack.BackColor = Color.AliceBlue;
            }
            else if (sender.Equals(SpellBox))
            {
                Attack.BackColor = Color.AliceBlue;
                SkillBox.BackColor = Color.AliceBlue;
                SpellBox.BackColor = Color.Khaki;
                ItemBox.BackColor = Color.AliceBlue;
                Defense.BackColor = Color.AliceBlue;
                RunAway.BackColor = Color.AliceBlue;
                playerBack.BackColor = Color.AliceBlue;
            }
            else if (sender.Equals(ItemBox))
            {
                Attack.BackColor = Color.AliceBlue;
                SkillBox.BackColor = Color.AliceBlue;
                SpellBox.BackColor = Color.AliceBlue;
                ItemBox.BackColor = Color.Khaki;
                Defense.BackColor = Color.AliceBlue;
                RunAway.BackColor = Color.AliceBlue;
                playerBack.BackColor = Color.AliceBlue;
            }
            else if (sender.Equals(Defense))
            {
                Attack.BackColor = Color.AliceBlue;
                SkillBox.BackColor = Color.AliceBlue;
                SpellBox.BackColor = Color.AliceBlue;
                ItemBox.BackColor = Color.AliceBlue;
                Defense.BackColor = Color.Khaki;
                RunAway.BackColor = Color.AliceBlue;
                playerBack.BackColor = Color.AliceBlue;
            }
            else if (sender.Equals(RunAway))
            {
                Attack.BackColor = Color.AliceBlue;
                SkillBox.BackColor = Color.AliceBlue;
                SpellBox.BackColor = Color.AliceBlue;
                ItemBox.BackColor = Color.AliceBlue;
                Defense.BackColor = Color.AliceBlue;
                RunAway.BackColor = Color.Khaki;
                playerBack.BackColor = Color.AliceBlue;
            }
            else if (sender.Equals(playerBack))
            {
                Attack.BackColor = Color.AliceBlue;
                SkillBox.BackColor = Color.AliceBlue;
                SpellBox.BackColor = Color.AliceBlue;
                ItemBox.BackColor = Color.AliceBlue;
                Defense.BackColor = Color.AliceBlue;
                RunAway.BackColor = Color.AliceBlue;
                playerBack.BackColor = Color.Khaki;
            }
        }

        private void CommandButton_Leave(object sender, EventArgs e)
        {
            ((Button)sender).BackColor = Color.AliceBlue;
        }

    }
}