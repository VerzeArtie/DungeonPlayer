using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public enum PlayerAction 
    {
        None = 0, // 待機（なにもしない）
        NormalAttack = 1, // 通常攻撃
        UseSkill = 2, // スキル使用
        RunAway = 3, // 逃げる
        UseItem = 4, // アイテム使用
        UseSpell = 5, // 魔法使用
        Defense = 6, // 防御
        SpecialSkill = 7, // 後編追加 敵専用スキル
        Charge = 8, // 後編追加 杖でためる
        Archetype = 9, // 後編追加 元核使用
    }

    public enum PlayerStance
    {
        None = 0,
        FrontOffence = 1, // アイン、オル、ラナ、ヴェルゼ
        FrontDefense = 2, // アイン、オル
        BackOffence = 3, // ラナ、ヴェルゼ
        BackSupport = 4, // アイン、ラナ、オル、ヴェルゼ
        AllRounder = 5, // ヴェルゼ専用
    }

    public enum AdditionalSpellType
    {
        None, // アイン、複合魔法属性を未選択
        Type1, // 例：アイン「水」を追加選択
        Type2, // 例：アイン「闇」を追加選択
    }

    public enum AdditionalSkillType
    {
        None, // アイン、複合スキル属性を未選択
        Type1, // 例：アイン「静」を追加選択
        Type2, // 例：アイン「柔」を追加選択
    }

    public class MainCharacter
    {
        protected string fullName = String.Empty;
        protected string name = String.Empty;
        protected int baseStrength = 5;
        protected int baseAgility = 3;
        protected int baseIntelligence = 2;
        protected int baseStamina = 4;
        protected int baseMind = 3;

        protected int baseResistFire = 0; // 後編追加
        protected int baseResistIce = 0; // 後編追加
        protected int baseResistLight = 0; // 後編追加
        protected int baseResistShadow = 0; // 後編追加
        protected int baseResistForce = 0; // 後編追加
        protected int baseResistWill = 0; // 後編追加

        // Debuff耐性
        protected bool battleResistStun = false; // 後編追加
        protected bool battleResistSilence = false; // 後編追加
        protected bool battleResistPoison = false; // 後編追加
        protected bool battleResistTemptation = false; // 後編追加
        protected bool battleResistFrozen = false; // 後編追加
        protected bool battleResistParalyze = false; // 後編追加
        protected bool battleResistSlow = false; // 後編追加
        protected bool battleResistBlind = false; // 後編追加
        protected bool battleResistSlip = false; // 後編追加
        protected bool battleResistNoResurrection = false; // 後編追加

        protected int level = 1;
        protected int experience = 0;
        protected int baseLife = 50;
        protected int currentLife = 50;
        protected int baseSkillPoint = 100;
        protected int currentSkillPoint = 100;
        protected int baseInstantPoint = 1000; // 後編追加
        protected int baseSpecialInstant = 20000; // 後編追加
        protected double currentInstantPoint = 0; // 後編追加// 「コメント」初期直感ではMAX値に戻しておくほうがいいと思ったが、プレイしてみてはじめは０のほうが、ゲーム性は面白く感じられると思った。
        protected double currentSpecialInstant = 0; // 後編追加
        protected int gold = 0;
        protected PlayerStance stance = PlayerStance.None; // 後編追加
        protected AdditionalSpellType additionSpellType = AdditionalSpellType.None; // 後編追加
        protected AdditionalSkillType additionSkillType = AdditionalSkillType.None; // 後編追加

        protected ItemBackPack iw;
        protected ItemBackPack sw; // 後編追加
        protected ItemBackPack ia;
        protected ItemBackPack accessory;
        protected ItemBackPack accessory2; // 後編追加
        protected ItemBackPack[] backpack;

        protected int MAX_LEVEL = 40;

        protected int baseMana = 80;
        protected int currentMana = 80;
        protected bool availableSkill = false;
        protected bool availableMana = false;
        protected bool availableArchitect = false;

        protected PlayerAction pa = PlayerAction.None;
        protected string currentUsingItem = String.Empty;
        protected string currentSkillName = String.Empty;
        protected string currentSpellName = String.Empty;
        protected string currentArchetypeName = String.Empty; // 後編追加
        protected MainCharacter target = null;
        protected MainCharacter target2 = null; // 後編追加
        protected PlayerAction beforePA = PlayerAction.None;
        protected string beforeUsingItem = String.Empty;
        protected string beforeSkillName = String.Empty;
        protected string beforeSpellName = String.Empty;
        protected string beforeArchetypeName = String.Empty; // 後編追加
        protected MainCharacter beforeTarget = null;
        protected MainCharacter beforeTarget2 = null; // 後編追加
        protected bool alreadyPlayArchetype = false; // 後編追加

        // 動
        // 静
        protected int currentCounterAttack = 0; // 後編編集
        protected int currentStanceOfStanding = 0; // 後編編集
        protected int currentAntiStun = 0;
        protected int currentStanceOfDeath = 0;
        // 柔
        protected int currentStanceOfFlow = 0;
        // 剛
        // 心眼
        protected int currentTruthVision = 0;
        protected int currentHighEmotionality = 0;
        protected int currentStanceOfEyes = 0;  // 後編編集
        protected int currentPainfulInsanity = 0;
        // 無心
        protected int currentNegate = 0; // 後編編集
        protected int currentVoidExtraction = 0;
        protected int currentNothingOfNothingness = 0;
        // 動＋静
        protected int currentStanceOfDouble = 0;
        // 動＋柔
        protected int currentSwiftStep = 0;
        protected int currentVigorSense = 0;
        // 動＋剛
        protected int currentRisingAura = 0;
        // 動＋心眼
        protected int currentOnslaughtHit = 0;
        // 動＋無心
        protected int currentSmoothingMove = 0;
        protected int currentAscensionAura = 0;
        // 静＋柔
        protected int currentFutureVision = 0;
        // 静＋剛
        protected int currentReflexSpirit = 0;
        // 静＋心眼
        protected int currentConcussiveHit = 0;
        // 静＋無心
        protected int currentTrustSilence = 0;
        // 柔＋剛
        protected int currentStanceOfMystic = 0;
        // 柔＋心眼
        protected int currentNourishSense = 0;
        // 柔＋無心
        protected int currentImpulseHit = 0;
        // 剛＋心眼
        protected int currentOneAuthority = 0;
        // 剛＋無心
        protected bool currentHardestParry = false;
        // 心眼＋無心
        protected bool currentStanceOfSuddenness = false;

        // 聖
        protected int currentProtection = 0;
        protected int currentSaintPower = 0;
        protected int currentGlory = 0;
        // 闇
        protected int currentShadowPact = 0;
        protected int currentBlackContract = 0;
        protected int currentBloodyVengeance = 0;
        protected int currentDamnation = 0;
        protected int currentDamnationFactor = 0;
        // 火
        protected int currentFlameAura = 0;
        protected int currentHeatBoost = 0;
        protected int currentImmortalRave = 0;
        // 水
        protected int currentAbsorbWater = 0;
        protected int currentMirrorImage = 0;
        protected int currentPromisedKnowledge = 0;
        protected int currentAbsoluteZero = 0;
        // 理
        protected int currentGaleWind = 0;
        protected int currentWordOfLife = 0;
        protected int currentWordOfFortune = 0;
        protected int currentAetherDrive = 0;
        protected int currentEternalPresence = 0;
        // 空
        protected int currentRiseOfImage = 0;
        protected int currentDeflection = 0;
        protected int currentOneImmunity = 0;
        protected int currentTimeStop = 0; // 後編追加
        protected bool currentTimeStopImmediate = false; // 後編追加

        // s 後編追加
        // 聖＋闇
        protected int currentPsychicTrance = 0;
        protected int currentBlindJustice = 0;
        protected int currentTranscendentWish = 0;
        // 聖＋水
        protected int currentSkyShield = 0;
        protected int currentEverDroplet = 0;
        // 聖＋理
        protected int currentHolyBreaker = 0;
        protected int currentExaltedField = 0;
        protected int currentHymnContract = 0;
        // 聖＋空
        protected int currentStarLightning = 0;
        // 闇＋火
        protected int currentBlackFire = 0;
        protected int currentBlazingField = 0;
        protected int currentBlazingFieldFactor = 0;
        // 闇＋水
        protected bool currentDeepMirror = false;
        // 闇＋理
        protected int currentWordOfMalice = 0;
        protected int currentSinFortune = 0;
        // 闇＋空
        protected int currentDarkenField = 0;
        protected int currentEclipseEnd = 0;
        // 火＋水
        protected int currentFrozenAura = 0;
        // 火＋理
        protected int currentEnrageBlast = 0;
        protected int currentSigilOfHomura = 0;
        // 火＋空
        protected int currentImmolate = 0;
        protected int currentPhantasmalWind = 0;
        protected int currentRedDragonWill = 0;
        // 水＋理
        protected int currentStaticBarrier = 0;
        protected int currentAusterityMatrix = 0;
        // 水＋空
        protected int currentBlueDragonWill = 0;
        // 理＋空
        protected int currentSeventhMagic = 0;
        protected int currentParadoxImage = 0;
        // e 後編追加

        // 集中と断絶
        protected int currentSyutyu_Danzetsu = 0;
        protected int currentJunkan_Seiyaku = 0;        

        // 武器特有のBUFF
        protected int currentFeltus = 0;
        protected int currentJuzaPhantasmal = 0;
        protected int currentEternalFateRing = 0;
        protected int currentLightServant = 0;
        protected int currentShadowServant = 0;
        protected int currentAdilBlueBurn = 0;
        protected int currentMazeCube = 0;
        protected int currentShadowBible = 0;
        protected int currentDetachmentOrb = 0;
        protected int currentDevilSummonerTome = 0;
        protected int currentVoidHymnsonia = 0;

        // 消耗品特有のBUFF
        protected int currentSagePotionMini = 0;
        protected int currentGenseiTaima = 0;
        protected int currentShiningAether = 0;
        protected int currentBlackElixir = 0;
        protected int currentElementalSeal = 0;
        protected int currentColoressAntidote = 0;

        // 最終戦ライフカウント
        protected int currentLifeCount = 0;

        // 最終戦ヴェルゼのカオティックスキーマ
        protected int currentChaoticSchema = 0;

        // 各魔法・スキル・アクセサリによるパラメタＵＰ
        protected int buffStrength_BloodyVengeance = 0;
        protected int buffAgility_HeatBoost = 0;
        protected int buffIntelligence_PromisedKnowledge = 0;
        protected int buffStamina_Unknown = 0; // [将来]：StaminaのBUFFUPスペルを構築
        protected int buffMind_RiseOfImage = 0;

        protected int buffStrength_VoidExtraction = 0;
        protected int buffAgility_VoidExtraction = 0;
        protected int buffIntelligence_VoidExtraction = 0;
        protected int buffStamina_VoidExtraction = 0; // [将来]：StaminaのBuffUPスキルを構築
        protected int buffMind_VoidExtraction = 0;

        protected int buffStrength_HighEmotionality = 0;
        protected int buffAgility_HighEmotionality = 0;
        protected int buffIntelligence_HighEmotionality = 0;
        protected int buffStamina_HighEmotionality = 0; // [将来]：StaminaのBuffUPスキルを構築
        protected int buffMind_HighEmotionality = 0;

        // s 後編追加
        protected int buffStrength_TranscendentWish = 0;
        protected int buffAgility_TranscendentWish = 0;
        protected int buffIntelligence_TranscendentWish = 0;
        protected int buffStamina_TranscendentWish = 0;
        protected int buffMind_TranscendentWish = 0;
        // e 後編追加

        // s 後編追加
        protected int buffStrength_Hiyaku_Kassei = 0;
        protected int buffAgility_Hiyaku_Kassei = 0;
        protected int buffIntelligence_Hiyaku_Kassei = 0;
        protected int buffStamina_Hiyaku_Kassei = 0;
        protected int buffMind_Hiyaku_Kassei = 0;
        // e 後編追加

        // s 後編追加
        protected int buffStrength_MainWeapon = 0;
        protected int buffAgility_MainWeapon = 0;
        protected int buffIntelligence_MainWeapon = 0;
        protected int buffStamina_MainWeapon = 0;
        protected int buffMind_MainWeapon = 0;

        protected int buffStrength_SubWeapon = 0;
        protected int buffAgility_SubWeapon = 0;
        protected int buffIntelligence_SubWeapon = 0;
        protected int buffStamina_SubWeapon = 0;
        protected int buffMind_SubWeapon = 0;

        protected int buffStrength_Armor = 0;
        protected int buffAgility_Armor = 0;
        protected int buffIntelligence_Armor = 0;
        protected int buffStamina_Armor = 0;
        protected int buffMind_Armor = 0;
        // e 後編追加

        protected int buffStrength_Accessory = 0;
        protected int buffAgility_Accessory = 0;
        protected int buffIntelligence_Accessory = 0;
        protected int buffStamina_Accessory = 0;
        protected int buffMind_Accessory = 0;
        // s 後編追加
        protected int buffStrength_Accessory2 = 0;
        protected int buffAgility_Accessory2 = 0;
        protected int buffIntelligence_Accessory2 = 0;
        protected int buffStamina_Accessory2 = 0;
        protected int buffMind_Accessory2 = 0;
        // e 後編追加

        // s 後編追加
        protected int buffStrength_Food = 0;
        protected int buffAgility_Food = 0;
        protected int buffIntelligence_Food = 0;
        protected int buffStamina_Food = 0;
        protected int buffMind_Food = 0;
        // e 後編追加

        // 戦闘中の値増幅
        protected double amplifyPhysicalAttack = 0.0f;
        protected double amplifyMagicAttack = 0.0f;
        protected double amplifyPhysicalDefense = 0.0f;
        protected double amplifyMagicDefense = 0.0f;
        protected double amplifyBattleSpeed = 0.0f;
        protected double amplifyBattleResponse = 0.0f;
        protected double amplifyPotential = 0.0f;

        // 負の影響効果（ボス専用、対象行動されると失敗しスタンになる）
        protected int currentPreStunning = 0;
        // 負の影響効果
        protected int currentStunning = 0;
        protected int currentSilence = 0;
        protected int currentPoison = 0;
        protected int currentTemptation = 0;
        protected int currentFrozen = 0;
        protected int currentParalyze = 0;
        protected int currentNoResurrection = 0; // 後編編集（スペル修正）
        protected int currentSlow = 0; // 後編追加
        protected int currentBlind = 0; // 後編追加
        protected int currentSlip = 0; // 後編追加
        protected int currentNoGainLife = 0; // 後編追加
        // 正の影響効果
        protected int currentBlinded = 0; // 後編追加
        protected int currentSpeedBoost = 0; // 後編追加 (一時的にスピードUPさせる靴の効果を期待したもの）
        protected int currentChargeCount = 0; // 後編追加
        protected int currentPhysicalChargeCount = 0; // 後編追加

        // 負の影響効果の重ねがけ効果
        protected int currentPoisonValue = 0; // 後編追加
        protected int currentConcussiveHitValue = 0; // 後編追加
        protected int currentOnslaughtHitValue = 0; // 後編追加
        protected int currentImpulseHitValue = 0; // 後編追加

        // 正の影響効果の重ねがけ効果
        protected int currentSkyShieldValue = 0; // 後編追加
        protected int currentStaticBarrierValue = 0; // 後編追加
        protected int currentStanceOfMysticValue = 0; // 後編追加

        // 武器特有の重ねがけ効果
        protected int currentFeltusValue = 0; // 後編追加
        protected int currentJuzaPhantasmalValue = 0; // 後編追加
        protected int currentEternalFateRingValue = 0; // 後編追加
        protected int currentLightServantValue = 0; // 後編追加
        protected int currentShadowServantValue = 0; // 後編追加
        protected int currentAdilBlueBurnValue = 0; // 後編追加
        protected int currentMazeCubeValue = 0; // 後編追加

        // 消耗品特有の重ねがけ効果
        protected int currentBlackElixirValue = 0; // 後編追加

        // 最終戦闘のライフカウント
        protected int currentLifeCountValue = 0; // 後編追加

        // s 後編追加
        // 以下、ボス戦など敵が汎用的にパワーアップしようとして作成したもの
        protected int currentPhysicalAttackUp = 0;
        protected int currentPhysicalAttackUpValue = 0;
        protected int currentPhysicalAttackDown = 0;
        protected int currentPhysicalAttackDownValue = 0;

        protected int currentPhysicalDefenseUp = 0;
        protected int currentPhysicalDefenseUpValue = 0;
        protected int currentPhysicalDefenseDown = 0;
        protected int currentPhysicalDefenseDownValue = 0;

        protected int currentMagicAttackUp = 0;
        protected int currentMagicAttackUpValue = 0;
        protected int currentMagicAttackDown = 0;
        protected int currentMagicAttackDownValue = 0;

        protected int currentMagicDefenseUp = 0;
        protected int currentMagicDefenseUpValue = 0;
        protected int currentMagicDefenseDown = 0;
        protected int currentMagicDefenseDownValue = 0;

        protected int currentSpeedUp = 0;
        protected int currentSpeedUpValue = 0;
        protected int currentSpeedDown = 0;
        protected int currentSpeedDownValue = 0;

        protected int currentReactionUp = 0;
        protected int currentReactionUpValue = 0;
        protected int currentReactionDown = 0;
        protected int currentReactionDownValue = 0;

        protected int currentPotentialUp = 0;
        protected int currentPotentialUpValue = 0;
        protected int currentPotentialDown = 0;
        protected int currentPotentialDownValue = 0;

        protected int currentStrengthUp = 0;
        protected int currentStrengthUpValue = 0;

        protected int currentAgilityUp = 0;
        protected int currentAgilityUpValue = 0;

        protected int currentIntelligenceUp = 0;
        protected int currentIntelligenceUpValue = 0;
        
        protected int currentStaminaUp = 0;
        protected int currentStaminaUpValue = 0;
        
        protected int currentMindUp = 0;
        protected int currentMindUpValue = 0;

        protected int currentLightUp = 0;
        protected int currentLightUpValue = 0;
        protected int currentLightDown = 0;
        protected int currentLightDownValue = 0;

        protected int currentShadowUp = 0;
        protected int currentShadowUpValue = 0;
        protected int currentShadowDown = 0;
        protected int currentShadowDownValue = 0;

        protected int currentFireUp = 0;
        protected int currentFireUpValue = 0;
        protected int currentFireDown = 0;
        protected int currentFireDownValue = 0;

        protected int currentIceUp = 0;
        protected int currentIceUpValue = 0;
        protected int currentIceDown = 0;
        protected int currentIceDownValue = 0;

        protected int currentForceUp = 0;
        protected int currentForceUpValue = 0;
        protected int currentForceDown = 0;
        protected int currentForceDownValue = 0;

        protected int currentWillUp = 0;
        protected int currentWillUpValue = 0;
        protected int currentWillDown = 0;
        protected int currentWillDownValue = 0;

        protected int currentResistLightUp = 0;
        protected int currentResistLightUpValue = 0;

        protected int currentResistShadowUp = 0;
        protected int currentResistShadowUpValue = 0;

        protected int currentResistFireUp = 0;
        protected int currentResistFireUpValue = 0;

        protected int currentResistIceUp = 0;
        protected int currentResistIceUpValue = 0;

        protected int currentResistForceUp = 0;
        protected int currentResistForceUpValue = 0;

        protected int currentResistWillUp = 0;
        protected int currentResistWillUpValue = 0;

        protected int currentAfterReviveHalf = 0; // エグゼキューショナーの死亡直後半分ライフ回復で蘇生
        protected int currentFireDamage2 = 0; // 業・フレイムスラッシャーの火追加効果ダメージ
        protected int currentBlackMagic = 0; // デビル・チルドレンの2回魔法BUFF
        protected int currentChaosDesperate = 0; // カオス・ワーデンの死亡予告
        protected int currentChaosDesperateValue = 0; // カオス・ワーデンの死亡予告カウント値
        protected int currentIchinaruHomura = 0; // レギィンアーゼの壱なる焔
        protected int currentAbyssFire = 0; // レギィンアーゼのアビス・ファイア
        protected int currentLightAndShadow = 0; // レギィンアーゼのライト・アンド・シャドウ
        protected int currentEternalDroplet = 0; // レギィンアーゼのエターナルドロップレット
        protected int currentAusterityMatrixOmega = 0; // レギィンアーゼのアウステリティ・マトリクス・Ω
        protected int currentVoiceOfAbyss = 0; // レギィンアーゼのヴォイス・オブ・アビス
        protected int currentAbyssWill = 0; // レギィンアーゼのアビスの意志
        protected int currentAbyssWillValue = 0; // レギィンアーゼのアビスの意志の累積値
        protected int currentTheAbyssWall = 0; // レギィンアーゼのアビス防壁

        protected int poolLifeConsumption = 0; // 無形の杯で、1ターンのダメージ/消費量が必要になった
        protected int poolManaConsumption = 0; // 無形の杯で、1ターンのダメージ/消費量が必要になった
        protected int poolSkillConsumption = 0; // 無形の杯で、1ターンのダメージ/消費量が必要になった

        // 以下、複合魔法FlashBlazeから追加ダメージを当てようとして考えたもの
        protected int currentFlashBlazeCount = 0; // 後編追加
        protected int currentFlashBlazeFactor = 0; // 後編追加

        protected bool dead = false;
        protected bool deadSignForTranscendentWish = false; // 後編追加

        // s 後編追加
        protected bool actionDecision = false; // 敵がアクションを決定したかどうかを示すフラグ
        protected int decisionTiming = 0; // 敵がアクションを決定するタイミング
        // e 後編追加

        // s 後編追加
        protected string battleActionCommand1 = String.Empty;
        protected string battleActionCommand2 = String.Empty;
        protected string battleActionCommand3 = String.Empty;
        protected string battleActionCommand4 = String.Empty;
        protected string battleActionCommand5 = String.Empty;
        protected string battleActionCommand6 = String.Empty;
        protected string battleActionCommand7 = String.Empty;
        protected string battleActionCommand8 = String.Empty;
        protected string battleActionCommand9 = String.Empty;
        protected string[] battleActionCommandList = null;
        // e 後編追加

        protected string reserveBattleCommand = String.Empty; // 後編追加 ターゲットを指定する場合、一時的にバトルコマンドを記憶する
        protected bool nowExecActionFlag = false; // 後編追加　現在自分が行動実行中であることを示すフラグ

        protected bool realTimeBattle = false; // 後編追加

        protected bool stackActivation = false; // 後編追加
        protected MainCharacter stackActivePlayer = null; // スタックインザコマンド用のアクティブプレイヤー格納庫
        protected MainCharacter stackTarget = null; // スタックインザコマンド用の対象プレイヤー格納庫
        protected PlayerAction stackPlayerAction = PlayerAction.None; // スタックインザコマンド用のプレイヤーアクション格納庫
        protected string stackCommandString = String.Empty; // スタックインザコマンド用のコマンド文字列格納庫

        //protected MainCharacter shadowStackActivePlayer = null;
        //protected MainCharacter shadowStackTarget = null;
        //protected PlayerAction shadowStackPlayerAction = PlayerAction.None;
        //protected string shadowStackCommandString = String.Empty;

        protected List<string> actionCommandStackList = new List<string>(); // Bystanderがタイムストップ時、詠唱を累積させるために用いるアクションコマンドリスト
        public List<string> ActionCommandStackList
        {
            get { return actionCommandStackList; }
            set { actionCommandStackList = value; }
        }
        protected List<MainCharacter> actionCommandStackTarget = new List<MainCharacter>();
        public List<MainCharacter> ActionCommandStackTarget
        {
            get { return actionCommandStackTarget; }
            set { actionCommandStackTarget = value; }
        }

        #region "魔法スペル"
        // 聖 Light
        protected bool freshHeal = false; // ライフ回復
        public bool FreshHeal
        {
            get { return freshHeal; }
            set { freshHeal = value; }
        }
        protected bool protection = false; // 物理防御UP
        public bool Protection
        {
            get { return protection; }
            set { protection = value; }
        }
        protected bool holyshock = false; // ダメージ
        public bool HolyShock
        {
            get { return holyshock; }
            set { holyshock = value; }
        }
        protected bool saintpower = false; // 物理攻撃UP
        public bool SaintPower
        {
            get { return saintpower; }
            set { saintpower = value; }
        }
        protected bool glory = false; // 次の３ターン、ライフ回復と攻撃を行う
        public bool Glory
        {
            get { return glory; }
            set { glory = value; }
        }
        protected bool resurrection = false; // 死んだ仲間を復活
        public bool Resurrection
        {
            get { return resurrection; }
            set { resurrection = value; }
        }
        protected bool celestialnova = false; // 敵全体ダメージ、または、味方全体ライフ回復
        public bool CelestialNova
        {
            get { return celestialnova; }
            set { celestialnova = value; }
        }

        // 闇 Shadow
        protected bool darkblast = false; // 魔法攻撃
        public bool DarkBlast
        {
            get { return darkblast; }
            set { darkblast = value; }
        }
        protected bool shadowpact = false; // 魔法攻撃UP
        public bool ShadowPact
        {
            get { return shadowpact; }
            set { shadowpact = value; }
        }
        protected bool lifeTap = false; // マナ値、スキル値を消費してライフ回復
        public bool LifeTap
        {
            get { return lifeTap; }
            set { lifeTap = value; }
        }
        protected bool blackContract = false; // 武器最大ダメージUP
        public bool BlackContract
        {
            get { return blackContract; }
            set { blackContract = value; }
        }
        protected bool devouringPlague = false; // 敵にダメージ＋自分ライフ回復
        public bool DevouringPlague
        {
            get { return devouringPlague; }
            set { devouringPlague = value; }
        }
        protected bool bloodyvengeance = false; // 力と体UP
        public bool BloodyVengeance
        {
            get { return bloodyvengeance; }
            set { bloodyvengeance = value; }
        }
        protected bool damnation = false; // 毎ターン、ダメージ
        public bool Damnation
        {
            get { return damnation; }
            set { damnation = value; }
        }

        // 火 Fire
        protected bool fireball = false; // 魔法攻撃
        public bool FireBall
        {
            get { return fireball; }
            set { fireball = value; }
        }
        protected bool flameaura = false; // 通常攻撃付随、追加ダメージ
        public bool FlameAura
        {
            get { return flameaura; }
            set { flameaura = value; }
        }
        protected bool heatboost = false; // 技量UP
        public bool HeatBoost
        {
            get { return heatboost; }
            set { heatboost = value; }
        }
        protected bool flamestrike = false; // 中ダメージ
        public bool FlameStrike
        {
            get { return flamestrike; }
            set { flamestrike = value; }
        }
        protected bool volcanicwave = false; // 大ダメージ
        public bool VolcanicWave
        {
            get { return volcanicwave; }
            set { volcanicwave = value; }
        }
        protected bool immortalrave = false; // 次の３ターン、火攻撃スペル（コスト０）＋直接攻撃を行う
        public bool ImmortalRave
        {
            get { return immortalrave; }
            set { immortalrave = value; }
        }
        protected bool lavaannihilation = false; // 全体ダメージ
        public bool LavaAnnihilation
        {
            get { return lavaannihilation; }
            set { lavaannihilation = value; }
        }

        // 水 Ice
        protected bool iceneedle = false; // ダメージ
        public bool IceNeedle
        {
            get { return iceneedle; }
            set { iceneedle = value; }
        }
        protected bool absorbwater = false; // 魔法防御UP
        public bool AbsorbWater
        {
            get { return absorbwater; }
            set { absorbwater = value; }
        }
        protected bool cleansing = false; // 味方一人に対して、負の影響を解除する
        public bool Cleansing
        {
            get { return cleansing; }
            set { cleansing = value; }
        }
        protected bool frozenlance = false; // 中ダメージ
        public bool FrozenLance
        {
            get { return frozenlance; }
            set { frozenlance = value; }
        }
        protected bool mirrorimage = false; // 魔法攻撃を反射（AbsoluteZeroとWordOfPowerは反射できない）
        public bool MirrorImage
        {
            get { return mirrorimage; }
            set { mirrorimage = value; }
        }
        protected bool promisedknowledge = false; // 知力UP
        public bool PromisedKnowledge
        {
            get { return promisedknowledge; }
            set { promisedknowledge = value; }
        }
        protected bool absolutezero = false; // 次の３ターン、敵はライフ回復不可、反射付与を無効化、スペル詠唱不可（Tranquilityは対象外）、防御不可
        public bool AbsoluteZero
        {
            get { return absolutezero; }
            set { absolutezero = value; }
        }

        // 理 Force
        protected bool wordofpower = false; // 物理ダメージ（魔法ダメージではない）
        public bool WordOfPower
        {
            get { return wordofpower; }
            set { wordofpower = value; }
        }
        protected bool galewind = false; // 次のターン、攻撃またはスペル詠唱が連続２回になる
        public bool GaleWind
        {
            get { return galewind; }
            set { galewind = value; }
        }
        protected bool wordoflife = false; // 毎ターンライフ回復
        public bool WordOfLife
        {
            get { return wordoflife; }
            set { wordoflife = value; }
        }
        protected bool wordoffortune = false; // 次ターン、100%クリティカル
        public bool WordOfFortune
        {
            get { return wordoffortune; }
            set { wordoffortune = value; }
        }
        protected bool aetherdrive = false; // 次の３ターン、敵からの物理ダメージ半減、味方の物理ダメージ２倍
        public bool AetherDrive
        {
            get { return aetherdrive; }
            set { aetherdrive = value; }
        }
        protected bool genesis = false; // 前回とった行動と同じ行動を行う。スペル、スキルコストは０。ただしキャンセル対象になった後は効果が続かない。
        public bool Genesis
        {
            get { return genesis; }
            set { genesis = value; }
        }
        protected bool eternalpresence = false; // 物理攻撃、物理防御、魔法攻撃、魔法防御UP
        public bool EternalPresence
        {
            get { return eternalpresence; }
            set { eternalpresence = value; }
        }

        // 空 Will
        protected bool dispelmagic = false; // 付与された魔法を除去
        public bool DispelMagic
        {
            get { return dispelmagic; }
            set { dispelmagic = value; }
        }
        protected bool riseofimage = false; // 心UP
        public bool RiseOfImage
        {
            get { return riseofimage; }
            set { riseofimage = value; }
        }
        protected bool deflection = false; // 物理攻撃を反射
        public bool Deflection
        {
            get { return deflection; }
            set { deflection = value; }
        }
        protected bool tranquility = false; // Glory、BlackContract、ImmortalRave、AbsoluteZero、AetherDrive、Deflection、MirrorImageを無効化
        public bool Tranquility
        {
            get { return tranquility; }
            set { tranquility = value; }
        }
        protected bool oneimmunity = false; // 次のターン、防御している間は、全ダメージ無効化。
        public bool OneImmunity
        {
            get { return oneimmunity; }
            set { oneimmunity = value; }
        }
        protected bool whiteout = false; // 大ダメージ
        public bool WhiteOut
        {
            get { return whiteout; }
            set { whiteout = value; }
        }
        protected bool timestop = false; // 次のターン、相手のターンを飛ばす
        public bool TimeStop
        {
            get { return timestop; }
            set { timestop = value; }
        }

        // s 後編追加
        // 聖＋闇（完全逆）
        protected bool psychicTrance = false;
        public bool PsychicTrance
        {
            get { return psychicTrance; }
            set { psychicTrance = value; }
        }
        protected bool blindJustice = false;
        public bool BlindJustice
        {
            get { return blindJustice; }
            set { blindJustice = value; }
        }
        protected bool transcendentWish = false;
        public bool TranscendentWish
        {
            get { return transcendentWish; }
            set { transcendentWish = value; }
        }

        // 聖＋火
        protected bool flashBlaze = false; // 魔法ダメージ＋追加魔法ダメージ
        public bool FlashBlaze
        {
            get { return flashBlaze; }
            set { flashBlaze = value; }
        }
        protected bool lightDetonator = false;
        public bool LightDetonator
        {
            get { return lightDetonator; }
            set { lightDetonator = value; }
        }
        protected bool ascendantMeteor = false;
        public bool AscendantMeteor
        {
            get { return ascendantMeteor; }
            set { ascendantMeteor = value; }
        }

        // 聖＋水
        protected bool skyShield = false;
        public bool SkyShield
        {
            get { return skyShield; }
            set { skyShield = value; }
        }
        protected bool sacredHeal = false;
        public bool SacredHeal
        {
            get { return sacredHeal; }
            set { sacredHeal = value; }
        }
        protected bool everDroplet = false;
        public bool EverDroplet
        {
            get { return everDroplet; }
            set { everDroplet = value; }
        }

        // 聖＋理
        protected bool holyBreaker = false;
        public bool HolyBreaker
        {
            get { return holyBreaker; }
            set { holyBreaker = value; }
        }
        protected bool exaltedField = false;
        public bool ExaltedField
        {
            get { return exaltedField; }
            set { exaltedField = value; }
        }
        protected bool hymnContract = false;
        public bool HymnContract
        {
            get { return hymnContract; }
            set { hymnContract = value; }
        }

        // 聖＋空
        protected bool starLightning = false;
        public bool StarLightning
        {
            get { return starLightning; }
            set { starLightning = value; }
        }
        protected bool angelBreath = false;
        public bool AngelBreath
        {
            get { return angelBreath; }
            set { angelBreath = value; }
        }
        protected bool endlessAnthem = false;
        public bool EndlessAnthem
        {
            get { return endlessAnthem; }
            set { endlessAnthem = value; }
        }

        // 闇＋火
        protected bool blackFire = false;
        public bool BlackFire
        {
            get { return blackFire; }
            set { blackFire = value; }
        }
        protected bool blazingField = false;
        public bool BlazingField
        {
            get { return blazingField; }
            set { blazingField = value; }
        }
        protected bool demonicIgnite = false;
        public bool DemonicIgnite
        {
            get { return demonicIgnite; }
            set { demonicIgnite = value; }
        }

        // 闇＋水
        protected bool blueBullet = false;
        public bool BlueBullet
        {
            get { return blueBullet; }
            set { blueBullet = value; }
        }
        protected bool deepMirror = false;
        public bool DeepMirror
        {
            get { return deepMirror; }
            set { deepMirror = value; }
        }
        protected bool deathDeny = false;
        public bool DeathDeny
        {
            get { return deathDeny; }
            set { deathDeny = value; }
        }

        // 闇＋理
        protected bool wordofMalice = false;
        public bool WordOfMalice
        {
            get { return wordofMalice; }
            set { wordofMalice = value; }
        }
        protected bool abyssEye = false;
        public bool AbyssEye
        {
            get { return abyssEye; }
            set { abyssEye = value; }
        }
        protected bool sinFortune = false;
        public bool SinFortune
        {
            get { return sinFortune; }
            set { sinFortune = value; }
        }

        // 闇＋空        
        protected bool darkenField = false;
        public bool DarkenField
        {
            get { return darkenField; }
            set { darkenField = value; }
        }
        protected bool doomBlade = false;
        public bool DoomBlade
        {
            get { return doomBlade; }
            set { doomBlade = value; }
        }
        protected bool eclipseEnd = false;
        public bool EclipseEnd
        {
            get { return eclipseEnd; }
            set { eclipseEnd = value; }
        }

        // 火＋水（完全逆）
        protected bool frozenAura = false;
        public bool FrozenAura
        {
            get { return frozenAura; }
            set { frozenAura = value; }
        }
        protected bool chillBurn = false;
        public bool ChillBurn
        {
            get { return chillBurn; }
            set { chillBurn = value; }
        }
        protected bool zetaExplosion = false;
        public bool ZetaExplosion
        {
            get { return zetaExplosion; }
            set { zetaExplosion = value; }
        }

        // 火＋理
        protected bool enrageBlast = false;
        public bool EnrageBlast
        {
            get { return enrageBlast; }
            set { enrageBlast = value; }
        }
        protected bool piercingFlame = false;
        public bool PiercingFlame
        {
            get { return piercingFlame; }
            set { piercingFlame = value; }
        }
        protected bool sigilofHomura = false;
        public bool SigilOfHomura
        {
            get { return sigilofHomura; }
            set { sigilofHomura = value; }
        }

        // 火＋空
        protected bool immolate = false;
        public bool Immolate
        {
            get { return immolate; }
            set { immolate = value; }
        }
        protected bool phantasmalWind = false;
        public bool PhantasmalWind
        {
            get { return phantasmalWind; }
            set { phantasmalWind = value; }
        }
        protected bool redDragonWill = false;
        public bool RedDragonWill
        {
            get { return redDragonWill; }
            set { redDragonWill = value; }
        }

        // 水＋理
        protected bool wordofAttitude = false;
        public bool WordOfAttitude
        {
            get { return wordofAttitude; }
            set { wordofAttitude = value; }
        }
        protected bool staticBarrier = false;
        public bool StaticBarrier
        {
            get { return staticBarrier; }
            set { staticBarrier = value; }
        }
        protected bool austerityMatrix = false;
        public bool AusterityMatrix
        {
            get { return austerityMatrix; }
            set { austerityMatrix = value; }
        }

        // 水＋空
        protected bool vanishWave = false;
        public bool VanishWave
        {
            get { return vanishWave; }
            set { vanishWave = value; }
        }
        protected bool vortexField = false;
        public bool VortexField
        {
            get { return vortexField; }
            set { vortexField = value; }
        }
        protected bool blueDragonWill = false;
        public bool BlueDragonWill
        {
            get { return blueDragonWill; }
            set { blueDragonWill = value; }
        }

        // 空＋理(完全逆)
        protected bool seventhMagic = false;
        public bool SeventhMagic
        {
            get { return seventhMagic; }
            set { seventhMagic = value; }
        }
        protected bool paradoxImage = false;
        public bool ParadoxImage
        {
            get { return paradoxImage; }
            set { paradoxImage = value; }
        }
        protected bool warpGate = false;
        public bool WarpGate
        {
            get { return warpGate; }
            set { warpGate = value; }
        }
        // e 後編追加
        #endregion

        #region "スキル"
        // 動
        protected bool straightSmash = false; // 力＋技による直接攻撃
        public bool StraightSmash
        {
            get { return straightSmash; }
            set { straightSmash = value; }
        }
        protected bool doubleSlash = false; // 2回攻撃
        public bool DoubleSlash
        {
            get { return doubleSlash; }
            set { doubleSlash = value; }
        }
        protected bool crushingBlow = false; // 直接攻撃＋１ターンの間スタン効果
        public bool CrushingBlow
        {
            get { return crushingBlow; }
            set { crushingBlow = value; }
        }
        protected bool soulInfinity = false; // 力＋技＋知による直接攻撃
        public bool SoulInfinity
        {
            get { return soulInfinity; }
            set { soulInfinity = value; }
        }

        // 静
        protected bool counterAttack = false; // 直接攻撃が来た場合、それを無効化して、直接攻撃を返す
        public bool CounterAttack
        {
            get { return counterAttack; }
            set { counterAttack = value; }
        }
        protected bool purePurification = false; // 味方一人に対して、負の影響を解除する
        public bool PurePurification
        {
            get { return purePurification; }
            set { purePurification = value; }
        }
        protected bool antiStun = false; // スタンする攻撃を食らった場合、スタンしない
        public bool AntiStun
        {
            get { return antiStun; }
            set { antiStun = value; }
        }
        protected bool stanceOfDeath = false; // 一撃死を食らう効果が当たった場合、それを回避する
        public bool StanceOfDeath
        {
            get { return stanceOfDeath; }
            set { stanceOfDeath = value; }
        }

        // 柔
        protected bool stanceOfFlow = false; // 次の３ターン、必ず後攻を取る。
        public bool StanceOfFlow
        {
            get { return stanceOfFlow; }
            set { stanceOfFlow = value; }
        }
        protected bool enigmaSense = false; // 力・技・知いずれの値の中で一番高い値を基準として直接攻撃
        public bool EnigmaSence
        {
            get { return enigmaSense; }
            set { enigmaSense = value; }
        }
        protected bool silentRush = false; // ３回攻撃
        public bool SilentRush
        {
            get { return silentRush; }
            set { silentRush = value; }
        }
        protected bool oboroImpact = false; // 力×心＋技×心＋知×心によるダメージ
        public bool OboroImpact
        {
            get { return oboroImpact; }
            set { oboroImpact = value; }
        }

        // 剛
        protected bool stanceOfStanding = false; // 防御の構えをしたまま通常攻撃
        public bool StanceOfStanding
        {
            get { return stanceOfStanding; }
            set { stanceOfStanding = value; }
        }
        protected bool innerInspiration = false; // スキルポイントを回復
        public bool InnerInspiration
        {
            get { return innerInspiration; }
            set { innerInspiration = value; }
        }
        protected bool kineticSmash = false; // 力＋知による直接攻撃
        public bool KineticSmash
        {
            get { return kineticSmash; }
            set { kineticSmash = value; }
        }
        protected bool catastrophe = false; // 全スキルポイントを使用して大ダメージ
        public bool Catastrophe
        {
            get { return catastrophe; }
            set { catastrophe = value; }
        }

        // 心眼
        protected bool truthVision = false; // 敵のパラメタUP状態を無視したステータスを対象とする
        public bool TruthVision
        {
            get { return truthVision; }
            set { truthVision = value; }
        }
        protected bool highEmotionality = false; // 次の３ターン、全パラメタUP
        public bool HighEmotionality
        {
            get { return highEmotionality; }
            set { highEmotionality = value; }
        }
        protected bool stanceOfEyes = false; // 相手の行動をキャンセルた上で攻撃する
        public bool StanceOfEyes
        {
            get { return stanceOfEyes; }
            set { stanceOfEyes = value; }
        }
        protected bool painfulInsanity = false; // 毎ターン、心値による永続ダメージ
        public bool PainfulInsanity
        {
            get { return painfulInsanity; }
            set { painfulInsanity = value; }
        }

        // 無心
        protected bool negate = false; // 相手のスペル詠唱をキャンセルする
        public bool Negate
        {
            get { return negate; }
            set { negate = value; }
        }
        protected bool voidExtraction = false; // 最も高いパラメタを２倍に引き上げる
        public bool VoidExtraction
        {
            get { return voidExtraction; }
            set { voidExtraction = value; }
        }
        protected bool carnageRush = false; // ５連続の無属性攻撃（属性がない）
        public bool CarnageRush
        {
            get { return carnageRush; }
            set { carnageRush = value; }
        }
        protected bool nothingOfNothingness = false; // DispelMagicとTranquilityを無効化する。Negate、StanceOfEyes、CounterAttackを無効化する。
        public bool NothingOfNothingness
        {
            get { return nothingOfNothingness; }
            set { nothingOfNothingness = value; }
        }

        // s 後編追加
        // 動＋静 （完全逆）
        protected bool neutralSmash = false;
        public bool NeutralSmash
        {
            get { return neutralSmash; }
            set { neutralSmash = value; }
        }
        protected bool stanceofDouble = false;
        public bool StanceOfDouble
        {
            get { return stanceofDouble; }
            set { stanceofDouble = value; }
        }

        // 動＋柔
        protected bool swiftStep = false;
        public bool SwiftStep
        {
            get { return swiftStep; }
            set { swiftStep = value; }
        }
        protected bool vigorSense = false;
        public bool VigorSense
        {
            get { return vigorSense; }
            set { vigorSense = value; }
        }

        // 動＋剛
        protected bool circleSlash = false;
        public bool CircleSlash
        {
            get { return circleSlash; }
            set { circleSlash = value; }
        }
        protected bool risingAura = false;
        public bool RisingAura
        {
            get { return risingAura; }
            set { risingAura = value; }
        }

        // 動＋心眼
        protected bool rumbleShout = false;
        public bool RumbleShout
        {
            get { return rumbleShout; }
            set { rumbleShout = value; }
        }
        protected bool onslaughtHit = false;
        public bool OnslaughtHit
        {
            get { return onslaughtHit; }
            set { onslaughtHit = value; }
        }

        // 動＋無心
        protected bool smoothingMove = false;
        public bool SmoothingMove
        {
            get { return smoothingMove; }
            set { smoothingMove = value; }
        }
        protected bool ascensionAura = false;
        public bool AscensionAura
        {
            get { return ascensionAura; }
            set { ascensionAura = value; }
        }

        // 静＋柔
        protected bool futureVision = false;
        public bool FutureVision
        {
            get { return futureVision; }
            set { futureVision = value; }
        }
        protected bool unknownShock = false;
        public bool UnknownShock
        {
            get { return unknownShock; }
            set { unknownShock = value; }
        }

        // 静＋剛
        protected bool reflexSpirit = false;
        public bool ReflexSpirit
        {
            get { return reflexSpirit; }
            set { reflexSpirit = value; }
        }
        protected bool fatalBlow = false;
        public bool FatalBlow
        {
            get { return fatalBlow; }
            set { fatalBlow = value; }
        }

        // 静＋心眼
        protected bool sharpGlare = false;
        public bool SharpGlare
        {
            get { return sharpGlare; }
            set { sharpGlare = value; }
        }
        protected bool concussiveHit = false;
        public bool ConcussiveHit
        {
            get { return concussiveHit; }
            set { concussiveHit = value; }
        }

        // 静＋無心
        protected bool trustSilence = false;
        public bool TrustSilence
        {
            get { return trustSilence; }
            set { trustSilence = value; }
        }
        protected bool mindKilling = false;
        public bool MindKilling
        {
            get { return mindKilling; }
            set { mindKilling = value; }
        }

        // 柔＋剛 （完全逆）
        protected bool surpriseAttack = false;
        public bool SurpriseAttack
        {
            get { return surpriseAttack; }
            set { surpriseAttack = value; }
        }
        protected bool impulseHit = false;
        public bool ImpulseHit
        {
            get { return impulseHit; }
            set { impulseHit = value; }
        }

        // 柔＋心眼
        protected bool psychicWave = false;
        public bool PsychicWave
        {
            get { return psychicWave; }
            set { psychicWave = value; }
        }
        protected bool nourishSense = false;
        public bool NourishSense
        {
            get { return nourishSense; }
            set { nourishSense = value; }
        }

        // 柔＋無心
        protected bool recover = false;
        public bool Recover
        {
            get { return recover; }
            set { recover = value; }
        }
        protected bool stanceofMystic = false;
        public bool StanceOfMystic
        {
            get { return stanceofMystic; }
            set { stanceofMystic = value; }
        }

        // 剛＋心眼
        protected bool violentSlash = false;
        public bool ViolentSlash
        {
            get { return violentSlash; }
            set { violentSlash = value; }
        }
        protected bool oneAuthority = false;
        public bool ONEAuthority
        {
            get { return oneAuthority; }
            set { oneAuthority = value; }
        }

        // 剛＋無心
        protected bool outerInspiration = false;
        public bool OuterInspiration
        {
            get { return outerInspiration; }
            set { outerInspiration = value; }
        }
        protected bool hardestParry = false;
        public bool HardestParry
        {
            get { return hardestParry; }
            set { hardestParry = value; }
        }

        // 心眼＋無心 （完全逆）
        protected bool stanceofSuddenness = false;
        public bool StanceOfSuddenness
        {
            get { return stanceofSuddenness; }
            set { stanceofSuddenness = value; }
        }
        protected bool soulExecution = false;
        public bool SoulExecution
        {
            get { return soulExecution; }
            set { soulExecution = value; }
        }
        // e 後編追加
        #endregion

        #region "元核"
        protected bool syutyu_danzetsu = false;
        public bool Syutyu_Danzetsu
        {
            get { return syutyu_danzetsu; }
            set { syutyu_danzetsu = value; }
        }
        
        protected bool junkan_seiyaku = false;
        public bool Junkan_Seiyaku
        {
            get { return junkan_seiyaku; }
            set { junkan_seiyaku = value; }
        }

        protected bool ora_ora_oraaa = false;
        public bool Ora_Ora_Oraaa
        {
            get { return ora_ora_oraaa; }
            set { ora_ora_oraaa = value; }
        }

        protected bool shinzitsu_hakai = false;
        public bool Shinzitsu_Hakai
        {
            get { return shinzitsu_hakai; }
            set { shinzitsu_hakai = value; }
        }
        #endregion

        #region "ステータス"
        public string FullName
        {
            get { return fullName; }
            set { fullName = value; }
        }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Strength
        {
            get { return baseStrength; }
            set { baseStrength = value; }
        }
        public int Agility
        {
            get { return baseAgility; }
            set { baseAgility = value; }
        }
        public int Intelligence
        {
            get { return baseIntelligence; }
            set { baseIntelligence = value; }
        }
        public int Stamina
        {
            get { return baseStamina; }
            set { baseStamina = value; }
        }
        public int Mind
        {
            get
            {
                if (((this.accessory != null) && (this.accessory.Name == Database.RARE_VOID_HYMNSONIA)) ||
                    ((this.accessory2 != null) && (this.accessory2.Name == Database.RARE_VOID_HYMNSONIA)))
                {
                    return 1;
                }
                else
                {
                    return baseMind;
                }
            }
            set { baseMind = value; }
        }
        public int BaseLife
        {
            get { return baseLife; }
            set { baseLife = value; }
        }
        public int MaxLife
        {
            // s 後編編集
            get 
            {
                int result = baseLife;
                result += TotalStamina * 10;
                result += CurrentBlackElixirValue;
                return result; 
            }
            // e 後編編集
        }
        public int BaseMana
        {
            get { return baseMana; }
            set { baseMana = value; }
        }
        public int MaxMana
        {
            get { return baseMana + TotalIntelligence * 10; } // 後編編集
        }
        public int BaseSkillPoint
        {
            get { return baseSkillPoint; }
            set { baseSkillPoint = value; }
        }
        public int MaxSkillPoint
        {
            // s 後編編集
            get
            {
                int result = baseSkillPoint;
                if (this.accessory != null)
                {
                    result += (int)this.accessory.EffectValue1;
                }
                if (this.accessory2 != null)
                {
                    result += (int)this.accessory2.EffectValue1;
                }

                if (this.currentSkillPoint > result) this.currentSkillPoint = result;
                return result; 
            }
            // e 後編編集
        }
        // s 後編追加
        public int BaseInstantPoint
        {
            get { return baseInstantPoint; }
            set { baseInstantPoint = value; }
        }
        public int MaxInstantPoint
        {
            get
            {
                if (baseInstantPoint < 1000) baseInstantPoint = 1000;
                return baseInstantPoint;
            }
        }
        public int BaseSpecialInstant
        {
            get { return baseSpecialInstant; }
            set { baseSpecialInstant = value; }
        }
        public int MaxSpecialInstant
        {
            get
            {
                if (baseSpecialInstant < 1) baseSpecialInstant = 1;
                return baseSpecialInstant;
            }
        }
        // e 後編追加
        // s 後編追加
        public int ResistFire
        {
            get { return baseResistFire; }
            set { baseResistFire = value; }
        }
        public int ResistIce
        {
            get { return baseResistIce; }
            set { baseResistIce = value; }
        }
        public int ResistLight
        {
            get { return baseResistLight; }
            set { baseResistLight = value; }
        }
        public int ResistShadow
        {
            get { return baseResistShadow; }
            set { baseResistShadow = value; }
        }
        public int ResistForce
        {
            get { return baseResistForce; }
            set { baseResistForce = value; }
        }
        public int ResistWill
        {
            get { return baseResistWill; }
            set { baseResistWill = value; }
        }
        // e 後編追加
        // s 後編追加
        public int TotalResistLight
        {
            get
            {
                int result = baseResistLight;
                if (this.currentResistLightUp > 0) result += currentResistLightUpValue;
                if (this.MainWeapon != null) result += this.MainWeapon.ResistLight;
                if (this.SubWeapon != null) result += this.SubWeapon.ResistLight;
                if (this.MainArmor != null) result += this.MainArmor.ResistLight;
                if (this.Accessory != null) result += this.Accessory.ResistLight;
                if (this.Accessory2 != null) result += this.Accessory2.ResistLight;
                if (result <= 0) result = 0;
                return result;
            }
        }
        public int TotalResistShadow
        {
            get
            {
                int result = baseResistShadow;
                if (this.currentResistShadowUp > 0) result += currentResistShadowUpValue;
                if (this.MainWeapon != null) result += this.MainWeapon.ResistShadow;
                if (this.SubWeapon != null) result += this.SubWeapon.ResistShadow;
                if (this.MainArmor != null) result += this.MainArmor.ResistShadow;
                if (this.Accessory != null) result += this.Accessory.ResistShadow;
                if (this.Accessory2 != null) result += this.Accessory2.ResistShadow;
                if (result <= 0) result = 0;
                return result;
            }
        }
        public int TotalResistFire
        {
            get
            {
                int result = baseResistFire;
                if (this.currentResistFireUp > 0) result += currentResistFireUpValue;
                if (this.MainWeapon != null) result += this.MainWeapon.ResistFire;
                if (this.SubWeapon != null) result += this.SubWeapon.ResistFire;
                if (this.MainArmor != null) result += this.MainArmor.ResistFire;
                if (this.Accessory != null) result += this.Accessory.ResistFire;
                if (this.Accessory2 != null) result += this.Accessory2.ResistFire;
                if (result <= 0) result = 0;
                return result;
            }
        }
        public int TotalResistIce
        {
            get
            {
                int result = baseResistIce;
                if (this.currentResistIceUp > 0) result += currentResistIceUpValue;
                if (this.MainWeapon != null) result += this.MainWeapon.ResistIce;
                if (this.SubWeapon != null) result += this.SubWeapon.ResistIce;
                if (this.MainArmor != null) result += this.MainArmor.ResistIce;
                if (this.Accessory != null) result += this.Accessory.ResistIce;
                if (this.Accessory2 != null) result += this.Accessory2.ResistIce;
                if (result <= 0) result = 0;
                return result;
            }
        }
        public int TotalResistForce
        {
            get
            {
                int result = baseResistForce;
                if (this.currentResistForceUp > 0) result += currentResistForceUpValue;
                if (this.MainWeapon != null) result += this.MainWeapon.ResistForce;
                if (this.SubWeapon != null) result += this.SubWeapon.ResistForce;
                if (this.MainArmor != null) result += this.MainArmor.ResistForce;
                if (this.Accessory != null) result += this.Accessory.ResistForce;
                if (this.Accessory2 != null) result += this.Accessory2.ResistForce;
                if (result <= 0) result = 0;
                return result;
            }
        }
        public int TotalResistWill
        {
            get
            {
                int result = baseResistWill;
                if (this.currentResistWillUp > 0) result += currentResistWillUpValue;
                if (this.MainWeapon != null) result += this.MainWeapon.ResistWill;
                if (this.SubWeapon != null) result += this.SubWeapon.ResistWill;
                if (this.MainArmor != null) result += this.MainArmor.ResistWill;
                if (this.Accessory != null) result += this.Accessory.ResistWill;
                if (this.Accessory2 != null) result += this.Accessory2.ResistWill;
                if (result <= 0) result = 0;
                return result;
            }
        }

        public bool CheckResistStun
        {
            get
            {
                if (this.battleResistStun) return true;
                if (this.currentSagePotionMini > 0) return true;
                if (this.currentElementalSeal > 0) return true;
                if ((this.MainWeapon != null) && (MainWeapon.ResistStun)) return true;
                if ((this.SubWeapon != null) && (SubWeapon.ResistStun)) return true;
                if ((this.MainArmor != null) && (MainArmor.ResistStun)) return true;
                if ((this.Accessory != null) && (Accessory.ResistStun)) return true;
                if ((this.Accessory2 != null) && (Accessory2.ResistStun)) return true;
                return false;
            }
        }
        public bool CheckResistSilence
        {
            get
            {
                if (this.battleResistSilence) return true;
                if (this.currentSagePotionMini > 0) return true;
                if (this.currentElementalSeal > 0) return true;
                if ((this.MainWeapon != null) && (MainWeapon.ResistSilence)) return true;
                if ((this.SubWeapon != null) && (SubWeapon.ResistSilence)) return true;
                if ((this.MainArmor != null) && (MainArmor.ResistSilence)) return true;
                if ((this.Accessory != null) && (Accessory.ResistSilence)) return true;
                if ((this.Accessory2 != null) && (Accessory2.ResistSilence)) return true;
                return false;
            }
        }
        public bool CheckResistPoison
        {
            get
            {
                if (this.battleResistPoison) return true;
                if (this.currentSagePotionMini > 0) return true;
                if (this.currentElementalSeal > 0) return true;
                if ((this.MainWeapon != null) && (MainWeapon.ResistPoison)) return true;
                if ((this.SubWeapon != null) && (SubWeapon.ResistPoison)) return true;
                if ((this.MainArmor != null) && (MainArmor.ResistPoison)) return true;
                if ((this.Accessory != null) && (Accessory.ResistPoison)) return true;
                if ((this.Accessory2 != null) && (Accessory2.ResistPoison)) return true;
                return false;
            }
        }
        public bool CheckResistTemptation
        {
            get
            {
                if (this.battleResistTemptation) return true;
                if (this.currentSagePotionMini > 0) return true;
                if (this.currentElementalSeal > 0) return true;
                if ((this.MainWeapon != null) && (MainWeapon.ResistTemptation)) return true;
                if ((this.SubWeapon != null) && (SubWeapon.ResistTemptation)) return true;
                if ((this.MainArmor != null) && (MainArmor.ResistTemptation)) return true;
                if ((this.Accessory != null) && (Accessory.ResistTemptation)) return true;
                if ((this.Accessory2 != null) && (Accessory2.ResistTemptation)) return true;
                return false;
            }
        }
        public bool CheckResistFrozen
        {
            get
            {
                if (this.battleResistFrozen) return true;
                if (this.currentSagePotionMini > 0) return true;
                if (this.currentElementalSeal > 0) return true;
                if ((this.MainWeapon != null) && (MainWeapon.ResistFrozen)) return true;
                if ((this.SubWeapon != null) && (SubWeapon.ResistFrozen)) return true;
                if ((this.MainArmor != null) && (MainArmor.ResistFrozen)) return true;
                if ((this.Accessory != null) && (Accessory.ResistFrozen)) return true;
                if ((this.Accessory2 != null) && (Accessory2.ResistFrozen)) return true;
                return false;
            }
        }
        public bool CheckResistParalyze
        {
            get
            {
                if (this.battleResistParalyze) return true;
                if (this.currentSagePotionMini > 0) return true;
                if (this.currentElementalSeal > 0) return true;
                if ((this.MainWeapon != null) && (MainWeapon.ResistParalyze)) return true;
                if ((this.SubWeapon != null) && (SubWeapon.ResistParalyze)) return true;
                if ((this.MainArmor != null) && (MainArmor.ResistParalyze)) return true;
                if ((this.Accessory != null) && (Accessory.ResistParalyze)) return true;
                if ((this.Accessory2 != null) && (Accessory2.ResistParalyze)) return true;
                return false;
            }
        }
        public bool CheckResistSlow
        {
            get
            {
                if (this.battleResistSlow) return true;
                if (this.currentSagePotionMini > 0) return true;
                if (this.currentElementalSeal > 0) return true;
                if ((this.MainWeapon != null) && (MainWeapon.ResistSlow)) return true;
                if ((this.SubWeapon != null) && (SubWeapon.ResistSlow)) return true;
                if ((this.MainArmor != null) && (MainArmor.ResistSlow)) return true;
                if ((this.Accessory != null) && (Accessory.ResistSlow)) return true;
                if ((this.Accessory2 != null) && (Accessory2.ResistSlow)) return true;
                return false;
            }
        }
        public bool CheckResistBlind
        {
            get
            {
                if (this.battleResistBlind) return true;
                if (this.currentSagePotionMini > 0) return true;
                if (this.currentElementalSeal > 0) return true;
                if ((this.MainWeapon != null) && (MainWeapon.ResistBlind)) return true;
                if ((this.SubWeapon != null) && (SubWeapon.ResistBlind)) return true;
                if ((this.MainArmor != null) && (MainArmor.ResistBlind)) return true;
                if ((this.Accessory != null) && (Accessory.ResistBlind)) return true;
                if ((this.Accessory2 != null) && (Accessory2.ResistBlind)) return true;
                return false;
            }
        }
        public bool CheckResistSlip
        {
            get
            {
                if (this.battleResistSlip) return true;
                if (this.currentSagePotionMini > 0) return true;
                if (this.currentElementalSeal > 0) return true;
                if ((this.MainWeapon != null) && (MainWeapon.ResistSlip)) return true;
                if ((this.SubWeapon != null) && (SubWeapon.ResistSlip)) return true;
                if ((this.MainArmor != null) && (MainArmor.ResistSlip)) return true;
                if ((this.Accessory != null) && (Accessory.ResistSlip)) return true;
                if ((this.Accessory2 != null) && (Accessory2.ResistSlip)) return true;
                return false;
            }
        }
        public bool CheckResistNoResurrection
        {
            get
            {
                if (this.battleResistNoResurrection) return true;
                if (this.currentSagePotionMini > 0) return true;
                //if (this.currentElementalSeal > 0) return true;
                if ((this.MainWeapon != null) && (MainWeapon.ResistNoResurrection)) return true;
                if ((this.SubWeapon != null) && (SubWeapon.ResistNoResurrection)) return true;
                if ((this.MainArmor != null) && (MainArmor.ResistNoResurrection)) return true;
                if ((this.Accessory != null) && (Accessory.ResistNoResurrection)) return true;
                if ((this.Accessory2 != null) && (Accessory2.ResistNoResurrection)) return true;
                return false;
            }
        }

        public bool CheckResistPhysicalAttackDown
        {
            get
            {
                if (this.currentColoressAntidote > 0) return true;
                return false;
            }
        }
        public bool CheckResistPhysicalDefenseDown
        {
            get
            {
                if (this.currentColoressAntidote > 0) return true;
                return false;
            }
        }
        public bool CheckResistMagicAttackDown
        {
            get
            {
                if (this.currentColoressAntidote > 0) return true;
                return false;
            }
        }
        public bool CheckResistMagicDefenseDown
        {
            get
            {
                if (this.currentColoressAntidote > 0) return true;
                return false;
            }
        }
        public bool CheckResistBattleSpeedDown
        {
            get
            {
                if (this.currentColoressAntidote > 0) return true;
                return false;
            }
        }
        public bool CheckResistBattleResponseDown
        {
            get
            {
                if (this.currentColoressAntidote > 0) return true;
                return false;
            }
        }
        public bool CheckResistPotentialDown
        {
            get
            {
                if (this.currentColoressAntidote > 0) return true;
                return false;
            }
        }
        // e 後編追加
        // s 後編追加
        public bool ResistStun
        {
            get { return battleResistStun; }
            set { battleResistStun = value; }
        }
        public bool ResistSilence
        {
            get { return battleResistSilence; }
            set { battleResistSilence = value; }
        }
        public bool ResistPoison
        {
            get { return battleResistPoison; }
            set { battleResistPoison = value; }
        }
        public bool ResistTemptation
        {
            get { return battleResistTemptation; }
            set { battleResistTemptation = value; }
        }
        public bool ResistFrozen
        {
            get { return battleResistFrozen; }
            set { battleResistFrozen = value; }
        }
        public bool ResistParalyze
        {
            get { return battleResistParalyze; }
            set { battleResistParalyze = value; }
        }
        public bool ResistNoResurrection
        {
            get { return battleResistNoResurrection; }
            set { battleResistNoResurrection = value; }
        }
        public bool ResistSlow
        {
            get { return battleResistSlow; }
            set { battleResistSlow = value; }
        }
        public bool ResistBlind
        {
            get { return battleResistBlind; }
            set { battleResistBlind = value; }
        }
        public bool ResistSlip
        {
            get { return battleResistSlip; }
            set { battleResistSlip = value; }
        }
        // e 後編追加
        // s 後編追加
        public int BuffStrength_Food
        {
            get { return buffStrength_Food; }
            set { buffStrength_Food = value; }
        }
        public int BuffAgility_Food
        {
            get { return buffAgility_Food; }
            set { buffAgility_Food = value; }
        }
        public int BuffIntelligence_Food
        {
            get { return buffIntelligence_Food; }
            set { buffIntelligence_Food = value; }
        }
        public int BuffStamina_Food
        {
            get { return buffStamina_Food; }
            set { buffStamina_Food = value; }
        }
        public int BuffMind_Food
        {
            get { return buffMind_Food; }
            set { buffMind_Food = value; }
        }
        // e 後編追加

        // s 後編追加
        public double AmplifyPhysicalAttack
        {
            get { return amplifyPhysicalAttack; }
            set { amplifyPhysicalAttack = value; }
        }
        public double AmplifyMagicAttack
        {
            get { return amplifyMagicAttack; }
            set { amplifyMagicAttack = value; }
        }
        public double AmplifyPhysicalDefense
        {
            get { return amplifyPhysicalDefense; }
            set { amplifyPhysicalDefense = value; }
        }
        public double AmplifyMagicDefense
        {
            get { return amplifyMagicDefense; }
            set { amplifyMagicDefense = value; }
        }
        public double AmplifyBattleSpeed
        {
            get { return amplifyBattleSpeed; }
            set { amplifyBattleSpeed = value; }
        }
        public double AmplifyBattleResponse
        {
            get { return amplifyBattleResponse; }
            set { amplifyBattleResponse = value; }
        }
        public double AmplifyPotential
        {
            get { return amplifyPotential; }
            set { amplifyPotential = value; }
        }
        // e 後編追加

        public int Level
        {
            get { return level; }
            set { level = value; }
        }
        public int Exp
        {
            get { return experience; }
            set 
            {
                if (value <= 0)
                {
                    experience = 0;
                }
                else
                {
                    experience = value;
                }
            }
        }
        public int CurrentLife
        {
            get { return currentLife; }
            set
            {
                if (value >= MaxLife)
                {
                    value = MaxLife;
                }
                if (value <= 0)
                {
                    value = 0;
                }
                currentLife = value;
            }
        }
        public int CurrentMana
        {
            get { return currentMana; }
            set
            {
                if (value >= MaxMana)
                {
                    value = MaxMana;
                }
                if (value <= 0)
                {
                    value = 0;
                }
                currentMana = value;
            }
        }
        public int CurrentSkillPoint
        {
            get { return currentSkillPoint; }
            set
            {
                if (value >= MaxSkillPoint)
                {
                    value = MaxSkillPoint;
                }
                currentSkillPoint = value;
            }
        }
        // s 後編追加
        public double CurrentInstantPoint
        {
            get { return currentInstantPoint; }
            set
            {
                if (value >= MaxInstantPoint)
                {
                    value = MaxInstantPoint;
                }
                currentInstantPoint = value;
            }
        }
        public double CurrentSpecialInstant
        {
            get { return currentSpecialInstant; }
            set
            {
                if (value >= MaxSpecialInstant)
                {
                    value = MaxSpecialInstant;
                }
                currentSpecialInstant = value;
            }
        }
        // e 後編追加
        public bool AvailableSkill
        {
            get { return availableSkill; }
            set { availableSkill = value; }
        }
        public bool AvailableMana
        {
            get { return availableMana; }
            set { availableMana = value; }
        }
        // s 後編追加
        public bool AvailableArchitect
        {
            get { return availableArchitect; }
            set { availableArchitect = value; }
        }
        // e 後編追加
        public int Gold
        {
            get { return gold; }
            set { gold = value; }
        }
        // s 後編追加
        public PlayerStance Stance
        {
            get { return stance; }
            set { stance = value; }
        }
        // e 後編追加
        // s 後編追加
        public AdditionalSpellType AdditionSpellType
        {
            get { return additionSpellType; }
            set { additionSpellType = value; }
        }
        public AdditionalSkillType AdditionSkillType
        {
            get { return additionSkillType; }
            set { additionSkillType = value; }
        }        // e 後編追加
        #endregion


        public ItemBackPack MainWeapon
        {
            get { return iw; }
            set
            { 
                iw = value;
                // s 後編編集
                if (iw != null)
                {
                    this.buffStrength_MainWeapon = iw.BuffUpStrength;
                    this.buffAgility_MainWeapon = iw.BuffUpAgility;
                    this.buffIntelligence_MainWeapon = iw.BuffUpIntelligence;
                    this.buffStamina_MainWeapon = iw.BuffUpStamina;
                    this.buffMind_MainWeapon = iw.BuffUpMind;
                }
                else
                {
                    this.buffStrength_MainWeapon = 0;
                    this.buffAgility_MainWeapon = 0;
                    this.buffIntelligence_MainWeapon = 0;
                    this.buffStamina_MainWeapon = 0;
                    this.buffMind_MainWeapon = 0;
                }
                if (this.currentLife > this.MaxLife) this.currentLife = this.MaxLife;
                // e 後編編集
            }
        }
        // s 後編追加
        public ItemBackPack SubWeapon
        {
            get { return sw; }
            set
            {
                sw = value;
                // s 後編編集
                if (sw != null)
                {
                    this.buffStrength_SubWeapon = sw.BuffUpStrength;
                    this.buffAgility_SubWeapon = sw.BuffUpAgility;
                    this.buffIntelligence_SubWeapon = sw.BuffUpIntelligence;
                    this.buffStamina_SubWeapon = sw.BuffUpStamina;
                    this.buffMind_SubWeapon = sw.BuffUpMind;
                }
                else
                {
                    this.buffStrength_SubWeapon = 0;
                    this.buffAgility_SubWeapon = 0;
                    this.buffIntelligence_SubWeapon = 0;
                    this.buffStamina_SubWeapon = 0;
                    this.buffMind_SubWeapon = 0;
                }
                if (this.currentLife > this.MaxLife) this.currentLife = this.MaxLife;
                // e 後編編集
            }
        }
        // e 後編追加
        public ItemBackPack MainArmor
        {
            get { return ia; }
            set
            {
                ia = value; 
                // s 後編編集
                if (ia != null)
                {
                    this.buffStrength_Armor = ia.BuffUpStrength;
                    this.buffAgility_Armor = ia.BuffUpAgility;
                    this.buffIntelligence_Armor = ia.BuffUpIntelligence;
                    this.buffStamina_Armor = ia.BuffUpStamina;
                    this.buffMind_Armor = ia.BuffUpMind;
                }
                else
                {
                    this.buffStrength_Armor = 0;
                    this.buffAgility_Armor = 0;
                    this.buffIntelligence_Armor = 0;
                    this.buffStamina_Armor = 0;
                    this.buffMind_Armor = 0;
                }
                if (this.currentLife > this.MaxLife) this.currentLife = this.MaxLife;
                // e 後編編集
            }
        }
        public ItemBackPack Accessory
        {
            get { return accessory; }
            set
            {
                accessory = value;
                if (accessory != null)
                {
                    // s 後編編集
                    this.buffStrength_Accessory = accessory.BuffUpStrength;
                    this.buffAgility_Accessory = accessory.BuffUpAgility;
                    this.buffIntelligence_Accessory = accessory.BuffUpIntelligence;
                    this.buffStamina_Accessory = accessory.BuffUpStamina;
                    this.buffMind_Accessory = accessory.BuffUpMind;
                    // e 後編編集
                }
                else
                {
                    this.buffStrength_Accessory = 0;
                    this.buffAgility_Accessory = 0;
                    this.buffIntelligence_Accessory = 0;
                    this.buffStamina_Accessory = 0;
                    this.buffMind_Accessory = 0;
                }
                if (this.currentLife > this.MaxLife) this.currentLife = this.MaxLife;
            }
        }
        // s 後編追加
        public ItemBackPack Accessory2
        {
            get { return accessory2; }
            set
            {
                accessory2 = value;
                if (accessory2 != null)
                {
                    this.buffStrength_Accessory2 = accessory2.BuffUpStrength;
                    this.buffAgility_Accessory2 = accessory2.BuffUpAgility;
                    this.buffIntelligence_Accessory2 = accessory2.BuffUpIntelligence;
                    this.buffStamina_Accessory2 = accessory2.BuffUpStamina;
                    this.buffMind_Accessory2 = accessory2.BuffUpMind;
                }
                else
                {
                    this.buffStrength_Accessory2 = 0;
                    this.buffAgility_Accessory2 = 0;
                    this.buffIntelligence_Accessory2 = 0;
                    this.buffStamina_Accessory2 = 0;
                    this.buffMind_Accessory2 = 0;
                }
                if (this.currentLife > this.MaxLife) this.currentLife = this.MaxLife;
            }
        }
        // e 後編追加

        #region "戦闘中専用"
        public PlayerAction PA
        {
            get { return pa; }
            set { pa = value; }
        }
        public string CurrentUsingItem
        {
            get { return currentUsingItem; }
            set { currentUsingItem = value; }
        }
        public string CurrentSkillName
        {
            get { return currentSkillName; }
            set { currentSkillName = value; }
        }
        public string CurrentSpellName
        {
            get { return currentSpellName; }
            set { currentSpellName = value; }
        }
        // s 後編追加
        public string CurrentArchetypeName
        {
            get { return currentArchetypeName; }
            set { currentArchetypeName = value; }
        }
        // e 後編追加
        public MainCharacter Target
        {
            get { return target; }
            set { target = value; }
        }
        // s 後編追加
        public MainCharacter Target2
        {
            get { return target2; }
            set { target2 = value; }
        }
        // e 後編追加
        public PlayerAction BeforePA
        {
            get { return beforePA; }
            set { beforePA = value; } // 後編追加
        }
        public string BeforeUsingItem
        {
            get { return beforeUsingItem; }
            set { beforeUsingItem = value; } // 後編追加
        }
        public string BeforeSkillName
        {
            get { return beforeSkillName; }
            set { beforeSkillName = value; } // 後編追加
        }
        public string BeforeSpellName
        {
            get { return beforeSpellName; }
            set { beforeSpellName = value; } // 後編追加
        }
        // s 後編追加
        public string BeforeArchetypeName
        {
            get { return beforeArchetypeName; }
            set { beforeArchetypeName = value; }
        }
        // e 後編追加
        public MainCharacter BeforeTarget
        {
            get { return beforeTarget; }
            set { beforeTarget = value; }
        }
        // s 後編追加
        public MainCharacter BeforeTarget2
        {
            get { return beforeTarget2; }
            set { beforeTarget2 = value; }
        }
        public bool AlreadyPlayArchetype
        {
            get { return alreadyPlayArchetype; }
            set { alreadyPlayArchetype = value; }
        }
        // e 後編追加

        // s 後編追加
        public bool CurrentDeepMirror
        {
            get { return currentDeepMirror; }
            set { currentDeepMirror = value; }
        }
        public bool CurrentStanceOfSuddenness
        {
            get { return currentStanceOfSuddenness; }
            set { currentStanceOfSuddenness = value; }
        }
        public bool CurrentHardestParry
        {
            get { return currentHardestParry; }
            set { currentHardestParry = value; }
        }
        public int CurrentStanceOfMystic
        {
            get { return currentStanceOfMystic; }
            set { currentStanceOfMystic = value; }
        }
        public int CurrentNourishSense
        {
            get { return currentNourishSense; }
            set { currentNourishSense = value; }
        }
        // e 後編追加


        public int CurrentAbsorbWater
        {
            get { return currentAbsorbWater; }
            set { currentAbsorbWater = value; }
        }
        public TruthImage pbAbsorbWater = null;
        public int CurrentProtection
        {
            get { return currentProtection; }
            set { currentProtection = value; }
        }
        public TruthImage pbProtection = null;
        public int CurrentSaintPower
        {
            get { return currentSaintPower; }
            set { currentSaintPower = value; }
        }
        public TruthImage pbSaintPower = null;
        public int CurrentShadowPact
        {
            get { return currentShadowPact; }
            set { currentShadowPact = value; }
        }
        public TruthImage pbShadowPact = null;
        public int CurrentWordOfLife
        {
            get { return currentWordOfLife; }
            set { currentWordOfLife = value; }
        }
        public TruthImage pbWordOfLife = null;
        public int CurrentGlory
        {
            get { return currentGlory; }
            set { currentGlory = value; }
        }
        public TruthImage pbGlory = null;
        public int CurrentFlameAura
        {
            get { return currentFlameAura; }
            set { currentFlameAura = value; }
        }
        public TruthImage pbFlameAura = null;
        public int CurrentOneImmunity
        {
            get { return currentOneImmunity; }
            set { currentOneImmunity = value; }
        }
        public TruthImage pbOneImmunity = null;
        // s 後編追加
        public int CurrentTimeStop
        {
            get { return currentTimeStop; }
            set { currentTimeStop = value; }
        }
        public bool CurrentTimeStopImmediate
        {
            get { return currentTimeStopImmediate; }
            set { currentTimeStopImmediate = value; }
        }
        public TruthImage pbTimeStop = null;
        // e 後編追加
        public int CurrentGaleWind
        {
            get { return currentGaleWind; }
            set { currentGaleWind = value; }
        }
        public TruthImage pbGaleWind = null;
        public int CurrentWordOfFortune
        {
            get { return currentWordOfFortune; }
            set { currentWordOfFortune = value; }
        }
        public TruthImage pbWordOfFortune = null;
        public int CurrentHeatBoost
        {
            get { return currentHeatBoost; }
            set { currentHeatBoost = value; }
        }
        public TruthImage pbHeatBoost = null;
        public int CurrentBloodyVengeance
        {
            get { return currentBloodyVengeance; }
            set { currentBloodyVengeance = value; }
        }
        public TruthImage pbBloodyVengeance = null;
        public int CurrentRiseOfImage
        {
            get { return currentRiseOfImage; }
            set { currentRiseOfImage = value; }
        }
        public TruthImage pbRiseOfImage = null;
        public int CurrentImmortalRave
        {
            get { return currentImmortalRave; }
            set { currentImmortalRave = value; }
        }
        public TruthImage pbImmortalRave = null;
        public int CurrentBlackContract
        {
            get { return currentBlackContract; }
            set { currentBlackContract = value; }
        }
        public TruthImage pbBlackContract = null;
        public int CurrentAetherDrive
        {
            get { return currentAetherDrive; }
            set { currentAetherDrive = value; }
        }
        public TruthImage pbAetherDrive = null;
        public int CurrentEternalPresence
        {
            get { return currentEternalPresence; }
            set { currentEternalPresence = value; }
        }
        public TruthImage pbEternalPresence = null;
        public int CurrentMirrorImage
        {
            get { return currentMirrorImage; }
            set { currentMirrorImage = value; }
        }
        public TruthImage pbMirrorImage = null;
        public int CurrentDeflection
        {
            get { return currentDeflection; }
            set { currentDeflection = value; }
        }
        public TruthImage pbDeflection = null;
        public int CurrentPainfulInsanity
        {
            get { return currentPainfulInsanity; }
            set { currentPainfulInsanity = value; }
        }
        public TruthImage pbPainfulInsanity = null;
        public int CurrentDamnation
        {
            get { return currentDamnation; }
            set { currentDamnation = value; }
        }
        public TruthImage pbDamnation = null;
        public int CurrentTruthVision
        {
            get { return currentTruthVision; }
            set { currentTruthVision = value; }
        }
        // s 後編追加
        public TruthImage pbPsychicTrance = null;
        public int CurrentPsychicTrance
        {
            get { return currentPsychicTrance; }
            set { currentPsychicTrance = value; }
        }
        public TruthImage pbBlindJustice = null;
        public int CurrentBlindJustice
        {
            get { return currentBlindJustice; }
            set { currentBlindJustice = value; }
        }
        public TruthImage pbTranscendentWish = null;
        public int CurrentTranscendentWish
        {
            get { return currentTranscendentWish; }
            set { currentTranscendentWish = value; }
        }
        public int CurrentSkyShield
        {
            get { return currentSkyShield; }
            set { currentSkyShield = value; }
        }
        public int CurrentStaticBarrier
        {
            get { return currentStaticBarrier; }
            set { currentStaticBarrier = value; }
        }
        public int CurrentAusterityMatrix
        {
            get { return currentAusterityMatrix; }
            set { currentAusterityMatrix = value; }
        }
        public int CurrentEverDroplet
        {
            get { return currentEverDroplet; }
            set { currentEverDroplet = value; }
        }
        public int CurrentFrozenAura
        {
            get { return currentFrozenAura; }
            set { currentFrozenAura = value; }
        }
        public int CurrentHolyBreaker
        {
            get { return currentHolyBreaker; }
            set { currentHolyBreaker = value; }
        }
        public int CurrentHymnContract
        {
            get { return currentHymnContract; }
            set { currentHymnContract = value; }
        }
        public int CurrentStarLightning
        {
            get { return currentStarLightning; }
            set { currentStarLightning = value; }
        }
        public int CurrentBlackFire
        {
            get { return currentBlackFire; }
            set { currentBlackFire = value; }
        }
        public int CurrentBlazingField
        {
            get { return currentBlazingField; }
            set { currentBlazingField = value; }
        }
        public int CurrentBlazingFieldFactor
        {
            get { return currentBlazingFieldFactor; }
            set { currentBlazingFieldFactor = value; }
        }

        public int CurrentWordOfMalice
        {
            get { return currentWordOfMalice; }
            set { currentWordOfMalice = value; }
        }
        public int CurrentSinFortune
        {
            get { return currentSinFortune; }
            set { currentSinFortune = value; }
        }
        public int CurrentDarkenField
        {
            get { return currentDarkenField; }
            set { currentDarkenField = value; }
        }
        public int CurrentEclipseEnd
        {
            get { return currentEclipseEnd; }
            set { currentEclipseEnd = value; }
        }
        public int CurrentExaltedField
        {
            get { return currentExaltedField; }
            set { currentExaltedField = value; }
        }
        public int CurrentOneAuthority
        {
            get { return currentOneAuthority; }
            set { currentOneAuthority = value; }
        }
        public int CurrentRisingAura
        {
            get { return currentRisingAura; }
            set { currentRisingAura = value; }
        }
        public int CurrentAscensionAura
        {
            get { return currentAscensionAura; }
            set { currentAscensionAura = value; }
        }
        public int CurrentEnrageBlast
        {
            get { return currentEnrageBlast; }
            set { currentEnrageBlast = value; }
        }
        public int CurrentSigilOfHomura
        {
            get { return currentSigilOfHomura; }
            set { currentSigilOfHomura = value; }
        }
        public int CurrentImmolate
        {
            get { return currentImmolate; }
            set { currentImmolate = value; }
        }
        public int CurrentPhantasmalWind
        {
            get { return currentPhantasmalWind; }
            set { currentPhantasmalWind = value; }
        }
        public int CurrentRedDragonWill
        {
            get { return currentRedDragonWill; }
            set { currentRedDragonWill = value; }
        }
        public int CurrentBlueDragonWill
        {
            get { return currentBlueDragonWill; }
            set { currentBlueDragonWill = value; }
        }
        public int CurrentSeventhMagic
        {
            get { return currentSeventhMagic; }
            set { currentSeventhMagic = value; }
        }
        public int CurrentParadoxImage
        {
            get { return currentParadoxImage; }
            set { currentParadoxImage = value; }
        }

        public int CurrentSyutyu_Danzetsu
        {
            get { return currentSyutyu_Danzetsu; }
            set { currentSyutyu_Danzetsu = value; }
        }
        public int CurrentJunkan_Seiyaku
        {
            get { return currentJunkan_Seiyaku; }
            set { currentJunkan_Seiyaku = value; }
        }
        // e 後編追加

        // s 後編追加
        public int CurrentCounterAttack
        {
            get { return currentCounterAttack; }
            set { currentCounterAttack = value; }
        }
        public TruthImage pbCounterAttack = null;

        public int CurrentStanceOfStanding
        {
            get { return currentStanceOfStanding; }
            set { currentStanceOfStanding = value; }
        }
        public TruthImage pbStanceOfStanding = null;
        // e 後編追加

        public TruthImage pbTruthVision = null;

        public int CurrentStanceOfFlow
        {
            get { return currentStanceOfFlow; }
            set { currentStanceOfFlow = value; }
        }
        public TruthImage pbStanceOfFlow = null;

        public int CurrentAbsoluteZero
        {
            get { return  currentAbsoluteZero; }
            set { currentAbsoluteZero = value; }
        }
        public TruthImage pbAbsoluteZero = null;

        public int CurrentPromisedKnowledge
        {
            get { return currentPromisedKnowledge; }
            set { currentPromisedKnowledge = value; }
        }
        public TruthImage pbPromisedKnowledge = null;

        public int CurrentHighEmotionality
        {
            get { return currentHighEmotionality; }
            set { currentHighEmotionality = value; }
        }
        public TruthImage pbHighEmotionality = null;

        // s 後編追加
        public int CurrentStanceOfEyes
        {
            get { return currentStanceOfEyes; }
            set { currentStanceOfEyes = value; }
        }
        public TruthImage pbStanceOfEyes = null;
        // e 後編追加

        public int CurrentVoidExtraction
        {
            get { return currentVoidExtraction; }
            set { currentVoidExtraction = value; }
        }
        public TruthImage pbVoidExtraction = null;

        // s 後編追加
        public int CurrentNegate
        {
            get { return currentNegate; }
            set { currentNegate = value; }
        }
        public TruthImage pbNegate = null;
        // e 後編追加

        public int CurrentAntiStun
        {
            get { return currentAntiStun; }
            set { currentAntiStun = value; }
        }
        public TruthImage pbAntiStun = null;

        public int CurrentStanceOfDeath
        {
            get { return currentStanceOfDeath; }
            set { currentStanceOfDeath = value; }
        }
        public TruthImage pbStanceOfDeath = null;

        public int CurrentNothingOfNothingness
        {
            get { return currentNothingOfNothingness; }
            set { currentNothingOfNothingness = value; }
        }
        public TruthImage pbNothingOfNothingness = null;

        // 動＋静
        public int CurrentStanceOfDouble
        {
            get { return currentStanceOfDouble; }
            set { currentStanceOfDouble = value; }
        }
        // 動＋柔
        public int CurrentSwiftStep
        {
            get { return currentSwiftStep; }
            set { currentSwiftStep = value; }
        }
        public int CurrentVigorSense
        {
            get { return currentVigorSense; }
            set { currentVigorSense = value; }
        }
        // 動＋剛
        // 動＋心眼
        public int CurrentOnslaughtHit
        {
            get { return currentOnslaughtHit; }
            set { currentOnslaughtHit = value; }
        }
        // 動＋無心
        public int CurrentSmoothingMove
        {
            get { return currentSmoothingMove; }
            set { currentSmoothingMove = value; }
        }
        // 静＋柔
        public int CurrentFutureVision
        {
            get { return currentFutureVision; }
            set { currentFutureVision = value; }
        }
        // 静＋剛
        public int CurrentReflexSpirit
        {
            get { return currentReflexSpirit; }
            set { currentReflexSpirit = value; }
        }
        // 静＋心眼
        public int CurrentConcussiveHit
        {
            get { return currentConcussiveHit; }
            set { currentConcussiveHit = value; }
        }
        // 静＋無心
        public int CurrentTrustSilence
        {
            get { return currentTrustSilence; }
            set { currentTrustSilence = value; }
        }
        // 柔＋剛
        // 柔＋心眼
        // 柔＋無心
        public int CurrentImpulseHit
        {
            get { return currentImpulseHit; }
            set { currentImpulseHit = value; }
        }
        // 剛＋心眼
        // 剛＋無心
        // 心眼＋無心

        // 武器特有のBUFF
        public int CurrentFeltus
        {
            get { return currentFeltus; }
            set { currentFeltus = value; }
        }
        public int CurrentJuzaPhantasmal
        {
            get { return currentJuzaPhantasmal; }
            set { currentJuzaPhantasmal = value; }
        }
        public int CurrentEternalFateRing
        {
            get { return currentEternalFateRing; }
            set { currentEternalFateRing = value; }
        }
        public int CurrentLightServant
        {
            get { return currentLightServant; }
            set { currentLightServant = value; }
        }
        public int CurrentShadowServant
        {
            get { return currentShadowServant; }
            set { currentShadowServant = value; }
        }
        public int CurrentAdilBlueBurn
        {
            get { return currentAdilBlueBurn; }
            set { currentAdilBlueBurn = value; }
        }
        public int CurrentMazeCube
        {
            get { return currentMazeCube; }
            set { currentMazeCube = value; }
        }
        public int CurrentShadowBible
        {
            get { return currentShadowBible; }
            set { currentShadowBible = value; }    
        }
        public int CurrentDetachmentOrb
        {
            get { return currentDetachmentOrb; }
            set { currentDetachmentOrb = value; }
        }
        public int CurrentDevilSummonerTome
        {
            get { return currentDevilSummonerTome; }
            set { currentDevilSummonerTome = value; }
        }
        public int CurrentVoidHymnsonia
        {
            get { return currentVoidHymnsonia; }
            set { currentVoidHymnsonia = value; }
        }

        public int CurrentSagePotionMini
        {
            get { return currentSagePotionMini; }
            set { currentSagePotionMini = value; }
        }
        public int CurrentGenseiTaima
        {
            get { return currentGenseiTaima; }
            set { currentGenseiTaima = value; }
        }
        public int CurrentShiningAether
        {
            get { return currentShiningAether; }
            set { currentShiningAether = value; }
        }
        public int CurrentBlackElixir
        {
            get { return currentBlackElixir; }
            set { currentBlackElixir = value; }
        }
        public int CurrentElementalSeal
        {
            get { return currentElementalSeal; }
            set { currentElementalSeal = value; }
        }
        public int CurrentColoressAntidote
        {
            get { return currentColoressAntidote; }
            set { currentColoressAntidote = value; }
        }

        public int CurrentLifeCount
        {
            get { return currentLifeCount; }
            set { currentLifeCount = value; }
        }
        
        public int CurrentChaoticSchema
        {
            get { return currentChaoticSchema; }
            set { currentChaoticSchema = value; }
        }

        public int CurrentPreStunning
        {
            get { return currentPreStunning; }
            set { currentPreStunning = value; }
        }
        public TruthImage pbPreStunning = null;
        public int CurrentStunning
        {
            get { return currentStunning; }
            set { currentStunning = value; }
        }
        public TruthImage pbStun = null;
        public int CurrentSilence
        {
            get { return currentSilence; }
            set { currentSilence = value; }
        }
        public TruthImage pbSilence = null;
        public int CurrentPoison
        {
            get { return currentPoison; }
            set { currentPoison = value; }
        }
        // s 後編追加
        public int CurrentPoisonValue
        {
            get { return currentPoisonValue; }
            set { if (value <= 5) { currentPoisonValue = value; } }
        }
        public int CurrentConcussiveHitValue
        {
            get { return currentConcussiveHitValue; }
            set { if (value <= 3) { currentConcussiveHitValue = value; } }
        }
        public int CurrentOnslaughtHitValue
        {
            get { return currentOnslaughtHitValue; }
            set { if (value <= 3) { currentOnslaughtHitValue = value; } }
        }
        public int CurrentImpulseHitValue
        {
            get { return currentImpulseHitValue; }
            set { if (value <= 3) { currentImpulseHitValue = value; } }
        }

        public int CurrentSkyShieldValue
        {
            get { return currentSkyShieldValue; }
            set { if (value <= 3) { currentSkyShieldValue = value; } }
        }
        public int CurrentStaticBarrierValue
        {
            get { return currentStaticBarrierValue; }
            set { if (value <= 3) { currentStaticBarrierValue = value; } }
        }
        public int CurrentStanceOfMysticValue
        {
            get { return currentStanceOfMysticValue; }
            set { if (value <= 3) { currentStanceOfMysticValue = value; } }
        }
        public int CurrentFeltusValue
        {
            get { return currentFeltusValue; }
            set { if (value <= 30) { currentFeltusValue = value; } }
        }
        public int CurrentJuzaPhantasmalValue
        {
            get { return currentJuzaPhantasmalValue; }
            set { if (value <= 10) { currentJuzaPhantasmalValue = value; } }
        }
        public int CurrentEternalFateRingValue
        {
            get { return currentEternalFateRingValue; }
            set { if (value <= 10) { currentEternalFateRingValue = value; } }
        }
        public int CurrentLightServantValue
        {
            get { return currentLightServantValue; }
            set { if (value <= 3) { currentLightServantValue = value; } }
        }
        public int CurrentShadowServantValue
        {
            get { return currentShadowServantValue; }
            set { if (value <= 3) { currentShadowServantValue = value; } }
        }
        public int CurrentAdilBlueBurnValue
        {
            get { return currentAdilBlueBurnValue; }
            set { if (value <= 30) { currentAdilBlueBurnValue = value; } }
        }
        public int CurrentMazeCubeValue
        {
            get { return currentMazeCubeValue; }
            set { if (value <= 10) { currentMazeCubeValue = value; } }
        }
        public int CurrentBlackElixirValue
        {
            get { return currentBlackElixirValue; }
            set { currentBlackElixirValue = value; }
        }
        public int CurrentLifeCountValue
        {
            get { return currentLifeCountValue; }
            set { if (value <= 3) { currentLifeCountValue = value; } }
        }
        // e 後編追加
        public TruthImage pbPoison = null;
        public int CurrentTemptation
        {
            get { return currentTemptation; }
            set { currentTemptation = value; }
        }
        public TruthImage pbTemptation = null;
        public int CurrentFrozen
        {
            get { return currentFrozen; }
            set { currentFrozen = value; }
        }
        public TruthImage pbFrozen = null;
        public int CurrentParalyze
        {
            get { return currentParalyze; }
            set { currentParalyze = value; }
        }
        public TruthImage pbParalyze = null;
        public int CurrentNoResurrection
        {
            get { return currentNoResurrection; }
            set { currentNoResurrection = value; }
        }
        public TruthImage pbNoResurrection = null;
        public TruthImage pbSlow = null; // 後編追加
        public TruthImage pbBlind = null; // 後編追加
        public TruthImage pbSlip = null; // 後編追加
        public TruthImage pbNoGainLife = null; // 後編追加
        public TruthImage pbBlinded = null; // 後編追加

        // s 後編追加
        public TruthImage pbPhysicalAttackUp = null;
        public TruthImage pbPhysicalAttackDown = null;
        public TruthImage pbPhysicalDefenseUp = null;
        public TruthImage pbPhysicalDefenseDown = null;
        public TruthImage pbMagicAttackUp = null;
        public TruthImage pbMagicAttackDown = null;
        public TruthImage pbMagicDefenseUp = null;
        public TruthImage pbMagicDefenseDown = null;
        public TruthImage pbSpeedUp = null;
        public TruthImage pbSpeedDown = null;
        public TruthImage pbReactionUp = null;
        public TruthImage pbReactionDown = null;
        public TruthImage pbPotentialUp = null;
        public TruthImage pbPotentialDown = null;

        public TruthImage pbStrengthUp = null;
        public TruthImage pbAgilityUp = null;
        public TruthImage pbIntelligenceUp = null;
        public TruthImage pbStaminaUp = null;
        public TruthImage pbMindUp = null;

        public TruthImage pbLightUp = null;
        public TruthImage pbLightDown = null;
        public TruthImage pbShadowUp = null;
        public TruthImage pbShadowDown = null;
        public TruthImage pbFireUp = null;
        public TruthImage pbFireDown = null;
        public TruthImage pbIceUp = null;
        public TruthImage pbIceDown = null;
        public TruthImage pbForceUp = null;
        public TruthImage pbForceDown = null;
        public TruthImage pbWillUp = null;
        public TruthImage pbWillDown = null;
        // e 後編追加

        // s 後編追加
        public TruthImage pbResistLightUp = null;
        public TruthImage pbResistShadowUp = null;
        public TruthImage pbResistFireUp = null;
        public TruthImage pbResistIceUp = null;
        public TruthImage pbResistForceUp = null;
        public TruthImage pbResistWillUp = null;

        public TruthImage pbAfterReviveHalf = null;
        public TruthImage pbFireDamage2 = null;
        public TruthImage pbBlackMagic = null;
        public TruthImage pbChaosDesperate = null;
        public TruthImage pbIchinaruHomura = null;
        public TruthImage pbAbyssFire = null;
        public TruthImage pbLightAndShadow = null;
        public TruthImage pbEternalDroplet = null;
        public TruthImage pbAusterityMatrixOmega = null;
        public TruthImage pbVoiceOfAbyss = null;
        public TruthImage pbAbyssWill = null;
        public TruthImage pbTheAbyssWall = null;

        // e 後編追加
        // s 後編追加
        public TruthImage pbResistStun = null;
        public TruthImage pbResistSilence = null;
        public TruthImage pbResistPoison = null;
        public TruthImage pbResistTemptation = null;
        public TruthImage pbResistFrozen = null;
        public TruthImage pbResistParalyze = null;
        public TruthImage pbResistSlow = null;
        public TruthImage pbResistBlind = null;
        public TruthImage pbResistSlip = null;
        public TruthImage pbResistNoResurrection = null;
        // e 後編追加
        // s 後編追加
        public TruthImage pbFlashBlaze = null;
        public TruthImage pbSkyShield = null;
        public TruthImage pbStaticBarrier = null;
        public TruthImage pbAusterityMatrix = null;
        public TruthImage pbEverDroplet = null;
        public TruthImage pbFrozenAura = null;
        public TruthImage pbStarLightning = null;
        public TruthImage pbWordOfMalice = null;
        public TruthImage pbSinFortune = null;       
        public TruthImage pbBlackFire = null;
        public TruthImage pbBlazingField = null;
        public TruthImage pbEnrageBlast = null;
        public TruthImage pbSigilOfHomura = null;
        public TruthImage pbImmolate = null;
        public TruthImage pbVanishWave = null;
        public TruthImage pbHolyBreaker = null;
        public TruthImage pbHymnContract = null;
        public TruthImage pbDarkenField = null;
        public TruthImage pbEclipseEnd = null;
        public TruthImage pbExaltedField = null;
        public TruthImage pbOneAuthority = null;
        public TruthImage pbRisingAura = null;
        public TruthImage pbAscensionAura = null;
        public TruthImage pbSeventhMagic = null;
        public TruthImage pbPhantasmalWind = null;
        public TruthImage pbRedDragonWill = null;
        public TruthImage pbBlueDragonWill = null;
        public TruthImage pbParadoxImage = null;

        // 動＋静
        public TruthImage pbStanceOfDouble = null;
        // 動＋柔
        public TruthImage pbSwiftStep = null;
        public TruthImage pbVigorSense = null;
        // 動＋剛
        // 動＋心眼
        public TruthImage pbOnslaughtHit = null;
        // 動＋無心
        public TruthImage pbSmoothingMove = null;
        // 静＋柔
        public TruthImage pbFutureVision = null;
        // 静＋剛
        public TruthImage pbReflexSpirit = null;
        // 静＋心眼
        public TruthImage pbConcussiveHit = null;
        // 静＋無心
        public TruthImage pbTrustSilence = null;
        // 柔＋剛
        public TruthImage pbStanceOfMystic = null;
        // 柔＋心眼
        public TruthImage pbNourishSense = null;
        // 柔＋無心
        public TruthImage pbImpulseHit = null;
        // 剛＋心眼
        // 剛＋無心
        // 心眼＋無心

        // 武器特有BUFF
        public TruthImage pbFeltus = null;
        public TruthImage pbJuzaPhantasmal = null;
        public TruthImage pbEternalFateRing = null;
        public TruthImage pbLightServant = null;
        public TruthImage pbShadowServant = null;
        public TruthImage pbAdilBlueBurn = null;
        public TruthImage pbMazeCube = null;
        public TruthImage pbShadowBible = null;
        public TruthImage pbDetachmentOrb = null;
        public TruthImage pbDevilSummonerTome = null;
        public TruthImage pbVoidHymnsonia = null;
        // 消耗品特有BUFF
        public TruthImage pbSagePotionMini = null;
        public TruthImage pbGenseiTaima = null;
        public TruthImage pbShiningAether = null;
        public TruthImage pbBlackElixir = null;
        public TruthImage pbElementalSeal = null;
        public TruthImage pbColoressAntidote = null;

        // 最終戦ライフカウント
        public TruthImage pbLifeCount = null;

        // ヴェルゼ最終戦カオティックスキーマ
        public TruthImage pbChaoticSchema = null;

        // 集中と断絶
        public TruthImage pbSyutyuDanzetsu = null;
        // 循環と誓約
        public TruthImage pbJunkanSeiyaku = null;
        // e 後編追加

        // s 後編追加 (これは初期試作変数であり、未使用）
        public TruthImage pbBuff1 = null;
        public TruthImage pbBuff2 = null;
        public TruthImage pbBuff3 = null;
        // e 後編追加

        public System.Windows.Forms.Label labelName = null;
        public System.Windows.Forms.Label labelLife = null;
        public System.Windows.Forms.Label labelSkill = null;
        public System.Windows.Forms.Label labelMana = null;

        // s 後編追加
        public System.Windows.Forms.Label labelCurrentSkillPoint = null;
        public System.Windows.Forms.Label labelCurrentManaPoint = null;
        public System.Windows.Forms.Label labelCurrentInstantPoint = null;
        public System.Windows.Forms.Label labelCurrentSpecialInstant = null;

        public TruthImage ActionButton1 = null;
        public TruthImage ActionButton2 = null;
        public TruthImage ActionButton3 = null;
        public TruthImage ActionButton4 = null;
        public TruthImage ActionButton5 = null;
        public TruthImage ActionButton6 = null;
        public TruthImage ActionButton7 = null;
        public TruthImage ActionButton8 = null;
        public TruthImage ActionButton9 = null;
        protected TruthImage[] actionButtonList = null;
        public TruthImage[] ActionButtonList
        {
            get { return actionButtonList; }
            set { actionButtonList = value; }
        }

        public System.Windows.Forms.Label ActionKeyNum1 = null;
        public System.Windows.Forms.Label ActionKeyNum2 = null;
        public System.Windows.Forms.Label ActionKeyNum3 = null;
        public System.Windows.Forms.Label ActionKeyNum4 = null;
        public System.Windows.Forms.Label ActionKeyNum5 = null;
        public System.Windows.Forms.Label ActionKeyNum6 = null;
        public System.Windows.Forms.Label ActionKeyNum7 = null;
        public System.Windows.Forms.Label ActionKeyNum8 = null;
        public System.Windows.Forms.Label ActionKeyNum9 = null;

        public System.Windows.Forms.PictureBox IsSorceryMark1 = null;
        public System.Windows.Forms.PictureBox IsSorceryMark2 = null;
        public System.Windows.Forms.PictureBox IsSorceryMark3 = null;
        public System.Windows.Forms.PictureBox IsSorceryMark4 = null;
        public System.Windows.Forms.PictureBox IsSorceryMark5 = null;
        public System.Windows.Forms.PictureBox IsSorceryMark6 = null;
        public System.Windows.Forms.PictureBox IsSorceryMark7 = null;
        public System.Windows.Forms.PictureBox IsSorceryMark8 = null;
        public System.Windows.Forms.PictureBox IsSorceryMark9 = null;

        public System.Windows.Forms.Label ActionLabel = null;

        public System.Windows.Forms.PictureBox MainObjectButton = null;
        public System.Drawing.Color MainColor = new Color();
        public System.Drawing.Color PlayerStatusColor
        {
            get
            {
                if (this.name == Database.EIN_WOLENCE) { return Database.COLOR_EIN; }
                else if (this.name == Database.RANA_AMILIA) { return Database.COLOR_RANA; }
                else if (this.name == Database.OL_LANDIS) { return Database.COLOR_OL; }
                else if (this.name == Database.SINIKIA_KAHLHANZ) { return Database.COLOR_KAHL; }
                else { return Database.COLOR_VERZE; }
            }
        }
        public System.Drawing.Color PlayerColor
        {
            get
            {
                if (this.name == Database.EIN_WOLENCE) { return Database.COLOR_BOX_EIN; }
                else if (this.name == Database.RANA_AMILIA) { return Database.COLOR_BOX_RANA; }
                else if (this.name == Database.OL_LANDIS) { return Database.COLOR_BOX_OL; }
                else if (this.name == Database.SINIKIA_KAHLHANZ) { return Database.COLOR_BOX_KAHL; }
                else { return Database.COLOR_BOX_VERZE; }
            }
        }
        public System.Drawing.Color PlayerBattleColor
        {
            get
            {
                if (this.name == Database.EIN_WOLENCE) { return Database.COLOR_BATTLE_EIN; }
                else if (this.name == Database.RANA_AMILIA) { return Database.COLOR_BATTLE_RANA; }
                else if (this.name == Database.OL_LANDIS) { return Database.COLOR_BATTLE_OL; }
                else if (this.name == Database.SINIKIA_KAHLHANZ) { return Database.COLOR_BATTLE_KAHL; }
                else { return Database.COLOR_BATTLE_VERZE; }
            }
        }
        public System.Drawing.Color PlayerBattleTargetColor1
        {
            get
            {
                if (this.name == Database.EIN_WOLENCE) { return Database.COLOR_BATTLE_TARGET1_EIN; }
                else if (this.name == Database.RANA_AMILIA) { return Database.COLOR_BATTLE_TARGET1_RANA; }
                else if (this.name == Database.OL_LANDIS) { return Database.COLOR_BATTLE_TARGET1_OL; }
                else if (this.name == Database.SINIKIA_KAHLHANZ) { return Database.COLOR_BATTLE_TARGET1_KAHL; }
                else { return Database.COLOR_BATTLE_TARGET1_VERZE; }
            }
        }
        public System.Drawing.Color PlayerBattleTargetColor2
        {
            get
            {
                if (this.name == Database.EIN_WOLENCE) { return Database.COLOR_BATTLE_TARGET2_EIN; }
                else if (this.name == Database.RANA_AMILIA) { return Database.COLOR_BATTLE_TARGET2_RANA; }
                else if (this.name == Database.OL_LANDIS) { return Database.COLOR_BATTLE_TARGET2_OL; }
                else if (this.name == Database.SINIKIA_KAHLHANZ) { return Database.COLOR_BATTLE_TARGET2_KAHL; }
                else { return Database.COLOR_BATTLE_TARGET2_VERZE; }
            }
        }
        public System.Windows.Forms.PictureBox pbTargetTarget = null;

        public System.Drawing.Bitmap MainFaceArrow = null; // 後編追加
        public System.Drawing.Bitmap ShadowFaceArrow2 = null; // 後編追加
        public System.Drawing.Bitmap ShadowFaceArrow3 = null; // 後編追加
        public double BattleBarPos = 0; // 後編追加
        public double BattleBarPos2 = 0; // 後編追加
        public double BattleBarPos3 = 0; // 後編追加

        public System.Windows.Forms.Label DamageLabel = null;
        public System.Windows.Forms.Label CriticalLabel = null;

        public System.Windows.Forms.Panel BuffPanel = null;
        public int BuffNumber = 0;
        public TruthImage[] BuffElement = null; // 「警告」：後編ではこれでBUFF並びを整列する。最終的には個別BUFFのTruthImageは全て不要になる。
        public System.Windows.Forms.TextBox TextBattleMessage = null;
        // e 後編追加

        // s 後編追加
        public int CurrentBlinded
        {
            get { return currentBlinded; }
            set { currentBlinded = value; }
        }
        public int CurrentSlip
        {
            get { return currentSlip; }
            set { currentSlip = value; }
        }
        public int CurrentNoGainLife
        {
            get { return currentNoGainLife; }
            set { currentNoGainLife = value; }
        }

        public int CurrentSlow
        {
            get { return currentSlow; }
            set { currentSlow = value; }
        }
        public int CurrentBlind
        {
            get { return currentBlind; }
            set { currentBlind = value; }
        }
        public int CurrentSpeedBoost
        {
            get { return currentSpeedBoost; }
            set { currentSpeedBoost = value; }
        }
        public int CurrentChargeCount
        {
            get { return currentChargeCount; }
            set { currentChargeCount = value; }
        }
        public int CurrentPhysicalChargeCount
        {
            get { return currentPhysicalChargeCount; }
            set { currentPhysicalChargeCount = value; }
        }
        // e 後編追加

        // s 後編追加
        public int CurrentPhysicalAttackUp
        {
            get { return currentPhysicalAttackUp; }
            set { currentPhysicalAttackUp = value; }
        }
        public int CurrentPhysicalAttackUpValue
        {
            get { return currentPhysicalAttackUpValue; }
            set { currentPhysicalAttackUpValue = value; }
        }
        public int CurrentPhysicalAttackDown
        {
            get { return currentPhysicalAttackDown; }
            set { currentPhysicalAttackDown = value; }
        }
        public int CurrentPhysicalAttackDownValue
        {
            get { return currentPhysicalAttackDownValue; }
            set { currentPhysicalAttackDownValue = value; }
        }

        public int CurrentPhysicalDefenseUp
        {
            get { return currentPhysicalDefenseUp; }
            set { currentPhysicalDefenseUp = value; }
        }
        public int CurrentPhysicalDefenseUpValue
        {
            get { return currentPhysicalDefenseUpValue; }
            set { currentPhysicalDefenseUpValue = value; }
        }
        public int CurrentPhysicalDefenseDown
        {
            get { return currentPhysicalDefenseDown; }
            set { currentPhysicalDefenseDown = value; }
        }
        public int CurrentPhysicalDefenseDownValue
        {
            get { return currentPhysicalDefenseDownValue; }
            set { currentPhysicalDefenseDownValue = value; }
        }

        public int CurrentMagicAttackUp
        {
            get { return currentMagicAttackUp; }
            set { currentMagicAttackUp = value; }
        }
        public int CurrentMagicAttackUpValue
        {
            get { return currentMagicAttackUpValue; }
            set { currentMagicAttackUpValue = value; }
        }
        public int CurrentMagicAttackDown
        {
            get { return currentMagicAttackDown; }
            set { currentMagicAttackDown = value; }
        }
        public int CurrentMagicAttackDownValue
        {
            get { return currentMagicAttackDownValue; }
            set { currentMagicAttackDownValue = value; }
        }

        public int CurrentMagicDefenseUp
        {
            get { return currentMagicDefenseUp; }
            set { currentMagicDefenseUp = value; }
        }
        public int CurrentMagicDefenseUpValue
        {
            get { return currentMagicDefenseUpValue; }
            set { currentMagicDefenseUpValue = value; }
        }
        public int CurrentMagicDefenseDown
        {
            get { return currentMagicDefenseDown; }
            set { currentMagicDefenseDown = value; }
        }
        public int CurrentMagicDefenseDownValue
        {
            get { return currentMagicDefenseDownValue; }
            set { currentMagicDefenseDownValue = value; }
        }

        public int CurrentSpeedUp
        {
            get { return currentSpeedUp; }
            set { currentSpeedUp = value; }
        }
        public int CurrentSpeedUpValue
        {
            get { return currentSpeedUpValue; }
            set { currentSpeedUpValue = value; }
        }
        public int CurrentSpeedDown
        {
            get { return currentSpeedDown; }
            set { currentSpeedDown = value; }
        }
        public int CurrentSpeedDownValue
        {
            get { return currentSpeedDownValue; }
            set { currentSpeedDownValue = value; }
        }

        public int CurrentReactionUp
        {
            get { return currentReactionUp; }
            set { currentReactionUp = value; }
        }
        public int CurrentReactionUpValue
        {
            get { return currentReactionUpValue; }
            set { currentReactionUpValue = value; }
        }
        public int CurrentReactionDown
        {
            get { return currentReactionDown; }
            set { currentReactionDown = value; }
        }
        public int CurrentReactionDownValue
        {
            get { return currentReactionDownValue; }
            set { currentReactionDownValue = value; }
        }

        public int CurrentPotentialUp
        {
            get { return currentPotentialUp; }
            set { currentPotentialUp = value; }
        }
        public int CurrentPotentialUpValue
        {
            get { return currentPotentialUpValue; }
            set { currentPotentialUpValue = value; }
        }
        public int CurrentPotentialDown
        {
            get { return currentPotentialDown; }
            set { currentPotentialDown = value; }
        }
        public int CurrentPotentialDownValue
        {
            get { return currentPotentialDownValue; }
            set { currentPotentialDownValue = value; }
        }

        public int CurrentStrengthUp
        {
            get { return currentStrengthUp; }
            set { currentStrengthUp = value; }
        }
        public int CurrentStrengthUpValue
        {
            get { return currentStrengthUpValue; }
            set { currentStrengthUpValue = value; }
        }

        public int CurrentAgilityUp
        {
            get { return currentAgilityUp; }
            set { currentAgilityUp = value; }
        }
        public int CurrentAgilityUpValue
        {
            get { return currentAgilityUpValue; }
            set { currentAgilityUpValue = value; }
        }

        public int CurrentIntelligenceUp
        {
            get { return currentIntelligenceUp; }
            set { currentIntelligenceUp = value; }
        }
        public int CurrentIntelligenceUpValue
        {
            get { return currentIntelligenceUpValue; }
            set { currentIntelligenceUpValue = value; }
        }

        public int CurrentStaminaUp
        {
            get { return currentStaminaUp; }
            set { currentStaminaUp = value; }
        }
        public int CurrentStaminaUpValue
        {
            get { return currentStaminaUpValue; }
            set { currentStaminaUpValue = value; }
        }

        public int CurrentMindUp
        {
            get { return currentMindUp; }
            set { currentMindUp = value; }
        }
        public int CurrentMindUpValue
        {
            get { return currentMindUpValue; }
            set { currentMindUpValue = value; }
        }

        public int CurrentLightUp
        {
            get { return currentLightUp; }
            set { currentLightUp = value; }
        }
        public int CurrentLightUpValue
        {
            get { return currentLightUpValue; }
            set { currentLightUpValue = value; }
        }
        public int CurrentLightDown
        {
            get { return currentLightDown; }
            set { currentLightDown = value; }
        }
        public int CurrentLightDownValue
        {
            get { return currentLightDownValue; }
            set { currentLightDownValue = value; }
        }
        
        public int CurrentShadowUp
        {
            get { return currentShadowUp; }
            set { currentShadowUp = value; }
        }
        public int CurrentShadowUpValue
        {
            get { return currentShadowUpValue; }
            set { currentShadowUpValue = value; }
        }
        public int CurrentShadowDown
        {
            get { return currentShadowDown; }
            set { currentShadowDown = value; }
        }
        public int CurrentShadowDownValue
        {
            get { return currentShadowDownValue; }
            set { currentShadowDownValue = value; }     
        }

        public int CurrentFireUp
        {
            get { return currentFireUp; }
            set { currentFireUp = value; }
        }
        public int CurrentFireUpValue
        {
            get { return currentFireUpValue; }
            set { currentFireUpValue = value; }
        }
        public int CurrentFireDown
        {
            get { return currentFireDown; }
            set { currentFireDown = value; }
        }
        public int CurrentFireDownValue
        {
            get { return currentFireDownValue; }
            set { currentFireDownValue = value; }
        }

        public int CurrentIceUp
        {
            get { return currentIceUp; }
            set { currentIceUp = value; }
        }
        public int CurrentIceUpValue
        {
            get { return currentIceUpValue; }
            set { currentIceUpValue = value; }
        }
        public int CurrentIceDown
        {
            get { return currentIceDown; }
            set { currentIceDown = value; }
        }
        public int CurrentIceDownValue
        {
            get { return currentIceDownValue; }
            set { currentIceDownValue = value; }
        }

        public int CurrentForceUp
        {
            get { return currentForceUp; }
            set { currentForceUp = value; }
        }
        public int CurrentForceUpValue
        {
            get { return currentForceUpValue; }
            set { currentForceUpValue = value; }
        }
        public int CurrentForceDown
        {
            get { return currentForceDown; }
            set { currentForceDown = value; }
        }
        public int CurrentForceDownValue
        {
            get { return currentForceDownValue; }
            set { currentForceDownValue = value; }
        }

        public int CurrentWillUp
        {
            get { return currentWillUp; }
            set { currentWillUp = value; }
        }
        public int CurrentWillUpValue
        {
            get { return currentWillUpValue; }
            set { currentWillUpValue = value; }
        }
        public int CurrentWillDown
        {
            get { return currentWillDown; }
            set { currentWillDown = value; }
        }
        public int CurrentWillDownValue
        {
            get { return currentWillDownValue; }
            set { currentWillDownValue = value; }
        }

        public int CurrentResistLightUp
        {
            get { return currentResistLightUp; }
            set { currentResistLightUp = value; }
        }
        public int CurrentResistLightUpValue
        {
            get { return currentResistLightUpValue; }
            set { currentResistLightUpValue = value; }
        }

        public int CurrentResistShadowUp
        {
            get { return currentResistShadowUp; }
            set { currentResistShadowUp = value; }
        }
        public int CurrentResistShadowUpValue
        {
            get { return currentResistShadowUpValue; }
            set { currentResistShadowUpValue = value; }
        }

        public int CurrentResistFireUp
        {
            get { return currentResistFireUp; }
            set { currentResistFireUp = value; }
        }
        public int CurrentResistFireUpValue
        {
            get { return currentResistFireUpValue; }
            set { currentResistFireUpValue = value; }
        }

        public int CurrentResistIceUp
        {
            get { return currentResistIceUp; }
            set { currentResistIceUp = value; }
        }
        public int CurrentResistIceUpValue
        {
            get { return currentResistIceUpValue; }
            set { currentResistIceUpValue = value; }
        }

        public int CurrentResistForceUp
        {
            get { return currentResistForceUp; }
            set { currentResistForceUp = value; }
        }
        public int CurrentResistForceUpValue
        {
            get { return currentResistForceUpValue; }
            set { currentResistForceUpValue = value; }
        }

        public int CurrentResistWillUp
        {
            get { return currentResistWillUp; }
            set { currentResistWillUp = value; }
        }
        public int CurrentResistWillUpValue
        {
            get { return currentResistWillUpValue; }
            set { currentResistWillUpValue = value; }
        }

        public int CurrentAfterReviveHalf
        {
            get { return currentAfterReviveHalf; }
            set { currentAfterReviveHalf = value; }
        }

        public int CurrentFireDamage2
        {
            get { return currentFireDamage2; }
            set { currentFireDamage2 = value; }
        }

        public int CurrentBlackMagic
        {
            get { return currentBlackMagic; }
            set { currentBlackMagic = value; }
        }

        public int CurrentChaosDesperate
        {
            get { return currentChaosDesperate; }
            set { currentChaosDesperate = value; }
        }
        public int CurrentChaosDesperateValue
        {
            get { return currentChaosDesperateValue; }
            set { currentChaosDesperateValue = value; }
        }

        public int CurrentIchinaruHomura
        {
            get { return currentIchinaruHomura; }
            set { currentIchinaruHomura = value; }
        }

        public int CurrentAbyssFire
        {
            get { return currentAbyssFire; }
            set { currentAbyssFire = value; }
        }

        public int CurrentLightAndShadow
        {
            get { return currentLightAndShadow; }
            set { currentLightAndShadow = value; }
        }

        public int CurrentEternalDroplet
        {
            get { return currentEternalDroplet; }
            set { currentEternalDroplet = value; }
        }

        public int CurrentAusterityMatrixOmega
        {
            get { return currentAusterityMatrixOmega; }
            set { currentAusterityMatrixOmega = value; }
        }

        public int CurrentVoiceOfAbyss
        {
            get { return currentVoiceOfAbyss; }
            set { currentVoiceOfAbyss = value; }
        }

        public int CurrentAbyssWill
        {
            get { return currentAbyssWill; }
            set { currentAbyssWill = value; }
        }
        public int CurrentAbyssWillValue
        {
            get { return currentAbyssWillValue; }
            set { currentAbyssWillValue = value; }
        }

        public int CurrentTheAbyssWall
        {
            get { return currentTheAbyssWall; }
            set { currentTheAbyssWall = value; }
        }

        public int CurrentFlashBlazeCount
        {
            get { return currentFlashBlazeCount; }
            set { currentFlashBlazeCount = value; }
        }

        public int PoolLifeConsumption
        {
            get { return poolLifeConsumption; }
            set { poolLifeConsumption = value; }
        }

        public int PoolManaConsumption
        {
            get { return poolManaConsumption; }
            set { poolManaConsumption = value; }
        }

        public int PoolSkillConsumption
        {
            get { return poolSkillConsumption; }
            set { poolSkillConsumption = value; }
        }
        // e 後編追加
        #endregion

        public bool Dead
        {
            get { return dead; }
            set { dead = value; }
        }
        // s 後編追加
        public bool DeadSignForTranscendentWish
        {
            get { return deadSignForTranscendentWish; }
            set { deadSignForTranscendentWish = value; }
        }
        // e 後編追加

        // s 後編追加
        public bool ActionDecision
        {
            get { return actionDecision; }
            set { actionDecision = value; }
        }
        public int DecisionTiming
        {
            get { return decisionTiming; }
            set { decisionTiming = value; }
        }
        // e 後編追加

        // s 後編追加
        public string BattleActionCommand1
        {
            get { return battleActionCommand1; }
            set { battleActionCommand1 = value; }
        }
        public string BattleActionCommand2
        {
            get { return battleActionCommand2; }
            set { battleActionCommand2 = value; }
        }
        public string BattleActionCommand3
        {
            get { return battleActionCommand3; }
            set { battleActionCommand3 = value; }
        }
        public string BattleActionCommand4
        {
            get { return battleActionCommand4; }
            set { battleActionCommand4 = value; }
        }
        public string BattleActionCommand5
        {
            get { return battleActionCommand5; }
            set { battleActionCommand5 = value; }
        }
        public string BattleActionCommand6
        {
            get { return battleActionCommand6; }
            set { battleActionCommand6 = value; }
        }
        public string BattleActionCommand7
        {
            get { return battleActionCommand7; }
            set { battleActionCommand7 = value; }
        }
        public string BattleActionCommand8
        {
            get { return battleActionCommand8; }
            set { battleActionCommand8 = value; }
        }
        public string BattleActionCommand9
        {
            get { return battleActionCommand9; }
            set { battleActionCommand9 = value; }
        }
        public string[] BattleActionCommandList
        {
            get { return battleActionCommandList; }
            set { battleActionCommandList = value; }
        }

        public string ReserveBattleCommand
        {
            get { return reserveBattleCommand; }
            set { reserveBattleCommand = value; }
        }

        public bool NowExecActionFlag
        {
            get { return nowExecActionFlag; }
            set { nowExecActionFlag = value; }
        }
        // e 後編追加

        // s 後編追加
        public bool RealTimeBattle
        {
            get { return realTimeBattle; }
            set { realTimeBattle = value; }
        }
        public bool StackActivation
        {
            get { return stackActivation; }
            set { stackActivation = value; }
        }
        public MainCharacter StackActivePlayer
        {
            get { return stackActivePlayer; }
            set { stackActivePlayer = value; }
        }
        public MainCharacter StackTarget
        {
            get { return stackTarget; }
            set { stackTarget = value; }
        }
        public PlayerAction StackPlayerAction
        {
            get { return stackPlayerAction; }
            set { stackPlayerAction = value; }
        }
        public string StackCommandString
        {
            get { return stackCommandString; }
            set { stackCommandString = value; }
        }
        //public MainCharacter ShadowStackActivePlayer
        //{
        //    get { return shadowStackActivePlayer; }
        //    set { shadowStackActivePlayer = value; }
        //}
        //public MainCharacter ShadowStackTarget
        //{
        //    get { return shadowStackTarget; }
        //    set { shadowStackTarget = value; }
        //}
        //public PlayerAction ShadowStackPlayerAction
        //{
        //    get { return shadowStackPlayerAction; }
        //    set { shadowStackPlayerAction = value; }
        //}
        //public string ShadowStackCommandString
        //{
        //    get { return shadowStackCommandString; }
        //    set { shadowStackCommandString = value; }
        //}
        // e 後編追加

        protected bool emotionAngry = false; // [警告]：怒っていることを示す値、Enumでいろいろつけた方がいいかも知れないが、そこまで開発できるとは思えないので保留。
        public bool EmotionAngry
        {
            get { return emotionAngry; }
            set { emotionAngry = value; }
        }

        public int BuffStrength_BloodyVengeance
        {
            get { return buffStrength_BloodyVengeance; }
            set { buffStrength_BloodyVengeance = value; }
        }
        public int BuffAgility_HeatBoost
        {
            get { return buffAgility_HeatBoost; }
            set { buffAgility_HeatBoost = value; }
        }
        public int BuffIntelligence_PromisedKnowledge
        {
            get { return buffIntelligence_PromisedKnowledge; }
            set { buffIntelligence_PromisedKnowledge = value; }
        }
        public int BuffStamina_Unknown
        {
            get { return buffStamina_Unknown; }
            set { buffStamina_Unknown = value; }
        }
        public int BuffMind_RiseOfImage
        {
            get { return buffMind_RiseOfImage; }
            set { buffMind_RiseOfImage = value; }
        }

        public int BuffStrength_VoidExtraction
        {
            get { return buffStrength_VoidExtraction; }
            set { buffStrength_VoidExtraction = value; }
        }
        public int BuffAgility_VoidExtraction
        {
            get { return buffAgility_VoidExtraction; }
            set { buffAgility_VoidExtraction = value; }
        }
        public int BuffIntelligence_VoidExtraction
        {
            get { return buffIntelligence_VoidExtraction; }
            set { buffIntelligence_VoidExtraction = value; }
        }
        public int BuffStamina_VoidExtraction
        {
            get { return buffStamina_VoidExtraction; }
            set { buffStamina_VoidExtraction = value; }
        }
        public int BuffMind_VoidExtraction
        {
            get { return buffMind_VoidExtraction; }
            set { buffMind_VoidExtraction = value; }
        }

        public int BuffStrength_HighEmotionality
        {
            get { return buffStrength_HighEmotionality; }
            set { buffStrength_HighEmotionality = value; }
        }
        public int BuffAgility_HighEmotionality
        {
            get { return buffAgility_HighEmotionality; }
            set { buffAgility_HighEmotionality = value; }
        }
        public int BuffIntelligence_HighEmotionality
        {
            get { return buffIntelligence_HighEmotionality; }
            set { buffIntelligence_HighEmotionality = value; }
        }
        public int BuffStamina_HighEmotionality
        {
            get { return buffStamina_HighEmotionality; }
            set { buffStamina_HighEmotionality = value; }
        }
        public int BuffMind_HighEmotionality
        {
            get { return buffMind_HighEmotionality; }
            set { buffMind_HighEmotionality = value; }
        }

        // s 後編追加
        public int BuffStrength_TranscendentWish
        {
            get { return buffStrength_TranscendentWish; }
            set { buffStrength_TranscendentWish = value; }
        }
        public int BuffAgility_TranscendentWish
        {
            get { return buffAgility_TranscendentWish; }
            set { buffAgility_TranscendentWish = value; }
        }
        public int BuffIntelligence_TranscendentWish
        {
            get { return buffIntelligence_TranscendentWish; }
            set { buffIntelligence_TranscendentWish = value; }
        }
        public int BuffStamina_TranscendentWish
        {
            get { return buffStamina_TranscendentWish; }
            set { buffStamina_TranscendentWish = value; }
        }
        public int BuffMind_TranscendentWish
        {
            get { return buffMind_TranscendentWish; }
            set { buffMind_TranscendentWish = value; }
        }

        public int BuffStrength_Hiyaku_Kassei
        {
            get { return buffStrength_Hiyaku_Kassei; }
            set { buffStrength_Hiyaku_Kassei = value; }
        }
        public int BuffAgility_Hiyaku_Kassei
        {
            get { return buffAgility_Hiyaku_Kassei; }
            set { buffAgility_Hiyaku_Kassei = value; }
        }
        public int BuffIntelligence_Hiyaku_Kassei
        {
            get { return buffIntelligence_Hiyaku_Kassei; }
            set { buffIntelligence_Hiyaku_Kassei = value; }
        }
        public int BuffStamina_Hiyaku_Kassei
        {
            get { return buffStamina_Hiyaku_Kassei; }
            set { buffStamina_Hiyaku_Kassei = value; }
        }
        public int BuffMind_Hiyaku_Kassei
        {
            get { return buffMind_Hiyaku_Kassei; }
            set { buffMind_Hiyaku_Kassei = value; }
        }
        // e 後編追加

        public int BuffStrength_Accessory
        {
            get { return buffStrength_MainWeapon + buffStrength_SubWeapon + buffStrength_Armor + buffStrength_Accessory + buffStrength_Accessory2; } // c 後編追加
        }
        public int BuffAgility_Accessory
        {
            get { return buffAgility_MainWeapon + buffAgility_SubWeapon + buffAgility_Armor + buffAgility_Accessory + buffAgility_Accessory2; } // c 後編追加
        }
        public int BuffIntelligence_Accessory
        {
            get { return buffIntelligence_MainWeapon + buffIntelligence_SubWeapon + buffIntelligence_Armor + buffIntelligence_Accessory + buffIntelligence_Accessory2; } // c 後編追加
        }
        public int BuffStamina_Accessory
        {
            get { return buffStamina_MainWeapon + buffStamina_SubWeapon + buffStamina_Armor + buffStamina_Accessory + buffStamina_Accessory2; } // c 後編追加
        }
        public int BuffMind_Accessory
        {
            get { return buffMind_MainWeapon + buffMind_SubWeapon + buffMind_Armor + buffMind_Accessory + buffMind_Accessory2; } // c 後編追加
        }

        // Standardはベース値＋常在型BUFFUP＋食事常在BUFFUPの合計値
        public int StandardStrength
        {
            get { return this.baseStrength + this.buffStrength_MainWeapon + this.buffStrength_Accessory + this.buffStrength_Accessory2 + this.buffStrength_Food; } // c 後編追加
        }
        public int StandardAgility
        {
            get { return this.baseAgility + this.buffAgility_MainWeapon + this.buffAgility_Accessory + this.buffAgility_Accessory2 + this.buffAgility_Food; } // c 後編追加
        }
        public int StandardIntelligence
        {
            get { return this.baseIntelligence + this.buffIntelligence_MainWeapon + this.buffIntelligence_Accessory + this.buffIntelligence_Accessory2 + this.buffIntelligence_Food; } // c 後編追加
        }
        public int StandardStamina
        {
            get { return this.baseStamina + this.buffStamina_MainWeapon + this.buffStamina_Accessory + this.buffStamina_Accessory2 + this.buffStamina_Food; } // c 後編追加
        }
        public int StandardMind
        {
            get { return this.baseMind + this.buffMind_MainWeapon + this.buffMind_Accessory + this.buffMind_Accessory2 + this.buffMind_Food; } // c 後編追加
        }

        // Totalはベース値＋戦闘中UP＋常在型BUFFUPの合計値
        public int TotalStrength
        {
            get { return this.baseStrength +
                this.buffStrength_BloodyVengeance +
                this.buffStrength_HighEmotionality +
                this.buffStrength_VoidExtraction + 
                this.buffStrength_TranscendentWish + // 後編追加
                this.buffStrength_Hiyaku_Kassei + // c 後編追加
                this.buffStrength_MainWeapon + // 後編追加
                this.buffStrength_SubWeapon + // 後編追加
                this.buffStrength_Armor +  // 後編追加
                this.buffStrength_Accessory +
                this.buffStrength_Accessory2 + // 後編追加
                this.buffStrength_Food + // 後編追加
                this.currentStrengthUpValue; // 後編追加    
            } // c 後編追加
        }
        public int TotalAgility
        {
            get { return this.baseAgility +
                this.buffAgility_HeatBoost +
                this.buffAgility_HighEmotionality +
                this.buffAgility_VoidExtraction +
                this.buffAgility_TranscendentWish + // 後編追加
                this.buffAgility_Hiyaku_Kassei + // c 後編追加
                this.buffAgility_MainWeapon + // 後編追加
                this.buffAgility_SubWeapon + // 後編追加
                this.buffAgility_Armor + // 後編追加
                this.buffAgility_Accessory +
                this.buffAgility_Accessory2 + // 後編追加
                this.buffAgility_Food + // 後編追加
                this.currentAgilityUpValue; } // c 後編追加
        }
        public int TotalIntelligence
        {
            get { return this.baseIntelligence +
                this.buffIntelligence_PromisedKnowledge +
                this.buffIntelligence_HighEmotionality +
                this.buffIntelligence_VoidExtraction +
                this.buffIntelligence_TranscendentWish + // 後編追加
                this.buffIntelligence_Hiyaku_Kassei + // c 後編追加
                this.buffIntelligence_MainWeapon + // 後編追加
                this.buffIntelligence_SubWeapon + // 後編追加
                this.buffIntelligence_Armor + // 後編追加
                this.buffIntelligence_Accessory +
                this.buffIntelligence_Accessory2 + // 後編追加
                this.buffIntelligence_Food + // 後編追加
                this.currentIntelligenceUpValue; } // c 後編追加
        }
        public int TotalStamina
        {
            get { return this.baseStamina + 
                this.buffStamina_Unknown +
                this.buffStamina_HighEmotionality +
                this.buffStamina_VoidExtraction +
                this.buffStamina_TranscendentWish + // 後編追加
                this.buffStamina_Hiyaku_Kassei + // c 後編追加
                this.buffStamina_MainWeapon + // 後編追加
                this.buffStamina_SubWeapon + // 後編追加
                this.buffStamina_Armor + // 後編追加
                this.buffStamina_Accessory +
                this.buffStamina_Accessory2 + // 後編追加
                this.buffStamina_Food + // 後編追加
                this.currentStaminaUpValue; } // c 後編追加
        }
        public int TotalMind
        {
            get
            {
                return this.baseMind +
                    this.buffMind_RiseOfImage +
                    this.buffMind_HighEmotionality +
                    this.buffMind_VoidExtraction +
                    this.buffMind_TranscendentWish + // 後編追加
                    this.buffMind_Hiyaku_Kassei + // c 後編追加
                    this.buffMind_MainWeapon + // 後編追加
                    this.buffMind_SubWeapon + // 後編追加
                    this.buffMind_Armor + // 後編追加
                    this.buffMind_Accessory +
                    this.buffMind_Accessory2 + // 後編追加
                    this.buffMind_Food + // 後編追加
                    this.currentMindUpValue + // c 後編追加
                    this.currentFeltusValue * (int)(PrimaryLogic.FeltusValue(this)); // 後編追加
            }
        }

        #region "バックパック制御関連"
        public MainCharacter()
        {
            backpack = new ItemBackPack[Database.MAX_BACKPACK_SIZE]; // 後編編集
        }

        // s 後編編集
        /// <summary>
        /// バックパックにアイテムを追加します。
        /// </summary>
        /// <param name="item"></param>
        /// <returns>TRUE:追加完了、FALSE:満杯のため追加できない</returns>
        public bool AddBackPack(ItemBackPack item)
        {
            return AddBackPack(item, 1);
        }
        public bool AddBackPack(ItemBackPack item, int addValue)
        {
            int dummyValue = 0;
            return AddBackPack(item, addValue, ref dummyValue);
        }
        public bool AddBackPack(ItemBackPack item, int addValue, ref int addedNumber)
        {
            for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++) // 後編編集
            {
                // まだ持っていない場合、１つ目として生成する。
                if (this.backpack[ii] == null)
                {
                    // いや、次を探索すると同名アイテムを持っているかもしれないので、まず検索する。
                    for (int jj = ii + 1; jj < Database.MAX_BACKPACK_SIZE; jj++) // 後編編集
                    {
                        if (CheckBackPackExist(item, jj) > 0)
                        {
                            // スタック上限以上の場合、別のアイテムとして追加する。
                            if (this.backpack[jj].StackValue >= item.LimitValue)
                            {
                                // 次のアイテムリストへスルー
                                break;
                            }
                            else
                            {
                                // スタック上限を超えていなくても、多数追加で上限を超えてしまう場合
                                if (this.backpack[jj].StackValue + addValue > item.LimitValue)
                                {
                                    // 次のアイテムリストへスルー
                                    break;
                                }
                                else
                                {
                                    this.backpack[jj].StackValue += addValue;
                                    addedNumber = jj;
                                    return true;
                                }
                            }
                        }
                    }

                    // やはり探索しても無かったので、そのまま追加する。
                    this.backpack[ii] = item;
                    this.backpack[ii].StackValue = addValue;
                    addedNumber = ii;
                    return true;
                }
                else
                {
                    // 既に持っている場合、スタック量を増やす。
                    if (this.backpack[ii].Name == item.Name)
                    {
                        // スタック上限以上の場合、別のアイテムとして追加する。
                        if (this.backpack[ii].StackValue >= item.LimitValue)
                        {
                            // 次のアイテムリストへスルー
                        }
                        else
                        {
                            // スタック上限を超えていなくても、多数追加で上限を超えてしまう場合
                            if (this.backpack[ii].StackValue + addValue > item.LimitValue)
                            {
                                // 次のアイテムリストへスルー
                            }
                            else
                            {
                                this.backpack[ii].StackValue += addValue;
                                addedNumber = ii;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// バックパックのアイテムを削除します。
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool DeleteBackPack(ItemBackPack item)
        {
            return DeleteBackPack(item, 0);
        }
        /// <summary>
        /// バックパックのアイテムを指定した数だけ削除します。
        /// </summary>
        /// <param name="item"></param>
        /// <param name="deleteValue">削除する数 ０：全て削除、正値：指定数だけ削除</param>
        /// <returns></returns>
        public bool DeleteBackPack(ItemBackPack item, int deleteValue)
        {
            for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++) // 後編編集
            {
                if (this.backpack[ii] != null)
                {
                    if (this.backpack[ii].Name == item.Name)
                    {
                        if (deleteValue <= 0)
                        {
                            this.backpack[ii] = null;
                            break;
                        }
                        else
                        {
                            // スタック量が正値の場合、指定されたスタック量を減らす。
                            this.backpack[ii].StackValue -= deleteValue;
                            if (this.backpack[ii].StackValue <= 0) // 結果的にスタック量が０になった場合はオブジェクトを消す。
                            {
                                this.backpack[ii] = null;
                            }
                            break;
                        }
                    }
                }
            }
            return true;
        }
        public bool DeleteBackPack(ItemBackPack item, int deleteValue, int ii)
        {
            if (this.backpack[ii] != null)
            {
                if (this.backpack[ii].Name == item.Name)
                {
                    // スタック量が１以下の場合、生成されているオブジェクトを消す。
                    if (this.backpack[ii].StackValue <= 1)
                    {
                        this.backpack[ii] = null;
                    }
                    else
                    {
                        // スタック量が１より大きい場合、指定されたスタック量を減らす。
                        this.backpack[ii].StackValue -= deleteValue;
                        if (this.backpack[ii].StackValue <= 0) // 結果的にスタック量が０になった場合はオブジェクトを消す。
                        {
                            this.backpack[ii] = null;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// バックパックに対象のアイテムが含まれている数を示します。
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int CheckBackPackExist(ItemBackPack item, int ii)
        {
            if (this.backpack[ii] != null)
            {
                if (this.backpack[ii].Name == item.Name)
                {
                    return this.backpack[ii].StackValue;
                }
            }
            return 0;
        }

        /// <summary>
        /// アイテム内容を全面的に入れ替えます。
        /// </summary>
        /// <param name="item"></param>
        public void ReplaceBackPack(ItemBackPack[] item)
        {
            this.backpack = null;
            this.backpack = item;
        }

        /// <summary>
        /// バックパックの内容を一括で全て取得します。
        /// </summary>
        /// <returns></returns>
        public ItemBackPack[] GetBackPackInfo()
        {
            return backpack;
        }
        // e 後編編集

        public ItemBackPack[] SortByUsed()
        {
            return SortBySomeType(0);
        }

        public ItemBackPack[] SortByAccessory()
        {
            return SortBySomeType(1);
        }

        public ItemBackPack[] SortByWeapon()
        {
            return SortBySomeType(2);
        }

        public ItemBackPack[] SortByArmor()
        {
            return SortBySomeType(3);
        }

        public ItemBackPack[] SortByName()
        {
            return SortBySomeType(4);
        }

        public ItemBackPack[] SortByRare()
        {
            return SortBySomeType(5);
        }

        private ItemBackPack[] SortBySomeType(int type)
        {
            ItemBackPack[] newBackPack = new ItemBackPack[Database.MAX_BACKPACK_SIZE]; // 後編編集
            int jj = 0;

            for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++) // 後編編集
            {
                if (backpack[ii] != null)
                {
                    newBackPack[jj] = backpack[ii];
                    jj++;
                }
            }

            if (type == 0)
            {
                Array.Sort((Array)newBackPack, 0, jj, ItemBackPack.SortItemBackPackUsed());
            }
            else if (type == 1)
            {
                Array.Sort((Array)newBackPack, 0, jj, ItemBackPack.SortItemBackPackAccessory());
            }
            else if (type == 2)
            {
                Array.Sort((Array)newBackPack, 0, jj, ItemBackPack.SortItemBackPackWeapon());
            }
            else if (type == 3)
            {
                Array.Sort((Array)newBackPack, 0, jj, ItemBackPack.SortItemBackPackArmor());
            }
            else if (type == 4)
            {
                Array.Sort((Array)newBackPack, 0, jj, ItemBackPack.SortItemBackPackName());
            }
            else if (type == 5)
            {
                Array.Sort((Array)newBackPack, 0, jj, ItemBackPack.SortItemBackPackRare());
            }
            this.backpack = newBackPack;
            return newBackPack;
        }
        #endregion

        public int NextLevelBorder
        {
            get 
            {
                int nextValue = 0;
                switch (this.level)
                {
                    case 1:
                        nextValue = 400;
                        break;
                    case 2:
                        nextValue = 900;
                        break;
                    case 3:
                        nextValue = 1400;
                        break;
                    case 4:
                        nextValue = 2100;
                        break;
                    case 5:
                        nextValue = 2800;
                        break;
                    case 6:
                        nextValue = 3600;
                        break;
                    case 7:
                        nextValue = 4500;
                        break;
                    case 8:
                        nextValue = 5400;
                        break;
                    case 9:
                        nextValue = 6500;
                        break;
                    case 10:
                        nextValue = 7600;
                        break;
                    case 11:
                        nextValue = 8700;
                        break;
                    case 12:
                        nextValue = 9800;
                        break;
                    case 13:
                        nextValue = 11000;
                        break;
                    case 14:
                        nextValue = 12300;
                        break;
                    case 15:
                        nextValue = 13600;
                        break;
                    case 16:
                        nextValue = 15000;
                        break;
                    case 17:
                        nextValue = 16400;
                        break;
                    case 18:
                        nextValue = 17800;
                        break;
                    case 19:
                        nextValue = 19300;
                        break;
                    case 20:
                        nextValue = 20800;
                        break;
                    case 21:
                        nextValue = 22400;
                        break;
                    case 22:
                        nextValue = 24000;
                        break;
                    case 23:
                        nextValue = 25500;
                        break;
                    case 24:
                        nextValue = 27200;
                        break;
                    case 25:
                        nextValue = 28900;
                        break;
                    case 26:
                        nextValue = 30500;
                        break;
                    case 27:
                        nextValue = 32200;
                        break;
                    case 28:
                        nextValue = 33900;
                        break;
                    case 29:
                        nextValue = 36300;
                        break;
                    case 30:
                        nextValue = 38800;
                        break;
                    case 31:
                        nextValue = 41600;
                        break;
                    case 32:
                        nextValue = 44600;
                        break;
                    case 33:
                        nextValue = 48000;
                        break;
                    case 34:
                        nextValue = 51400;
                        break;
                    case 35:
                        nextValue = 55000;
                        break;
                    case 36:
                        nextValue = 58700;
                        break;
                    case 37:
                        nextValue = 62400;
                        break;
                    case 38:
                        nextValue = 66200;
                        break;
                    case 39:
                        nextValue = 70200;
                        break;
                    case 40:
                        nextValue = 74300;
                        break;  
                    // s 後編追加
                    case 41:
                        nextValue = 78500;
                        break;
                    case 42:
                        nextValue = 82800;
                        break;
                    case 43:
                        nextValue = 87100;
                        break;
                    case 44:
                        nextValue = 91600;
                        break;
                    case 45:
                        nextValue = 96300;
                        break;
                    case 46:
                        nextValue = 101000;
                        break;
                    case 47:
                        nextValue = 105800;
                        break;
                    case 48:
                        nextValue = 110700;
                        break;
                    case 49:
                        nextValue = 115700;
                        break;
                    case 50:
                        nextValue = 120900;
                        break;
                    case 51:
                        nextValue = 126100;
                        break;
                    case 52:
                        nextValue = 131500;
                        break;
                    case 53:
                        nextValue = 137000;
                        break;
                    case 54:
                        nextValue = 142500;
                        break;
                    case 55:
                        nextValue = 148200;
                        break;
                    case 56:
                        nextValue = 154000;
                        break;
                    case 57:
                        nextValue = 159900;
                        break;
                    case 58:
                        nextValue = 165800;
                        break;
                    case 59:
                        nextValue = 172000;
                        break;
                    case 60:
                        nextValue = 290000;
                        break;
                    case 61:
                        nextValue = 317000;
                        break;
                    case 62:
                        nextValue = 349000;
                        break;
                    case 63:
                        nextValue = 386000;
                        break;
                    case 64:
                        nextValue = 428000;
                        break;
                    case 65:
                        nextValue = 475000;
                        break;
                    case 66:
                        nextValue = 527000;
                        break;
                    case 67:
                        nextValue = 585000;
                        break;
                    case 68:
                        nextValue = 648000;
                        break;
                    case 69:
                        nextValue = 717000;
                        break;
                    case 70:
                        nextValue = 1523800;
                        break;
                    // e 後編追加
                }

                if (this.level < 35)
                {
                    return nextValue / 4; // ハードモードの場合、２
                }
                else
                {
                    return nextValue / 2; // ハードモードの場合、１
                }
            }
        }

        public int LevelUpPoint
        {
            get 
            {
                int upPoint = 0;
                switch (this.level)
                {
                    case 1:
                        upPoint = 6;
                        break;
                    case 2:
                        upPoint = 5;
                        break;
                    case 3:
                        upPoint = 6;
                        break;
                    case 4:
                        upPoint = 5;
                        break;
                    case 5:
                        upPoint = 7;
                        break;
                    case 6:
                        upPoint = 5;
                        break;
                    case 7:
                        upPoint = 6;
                        break;
                    case 8:
                        upPoint = 5;
                        break;
                    case 9:
                        upPoint = 5;
                        break;
                    case 10:
                        upPoint = 8;
                        break;
                    case 11:
                        upPoint = 8;
                        break;
                    case 12:
                        upPoint = 9;
                        break;
                    case 13:
                        upPoint = 8;
                        break;
                    case 14:
                        upPoint = 10;
                        break;
                    case 15:
                        upPoint = 8;
                        break;
                    case 16:
                        upPoint = 10;
                        break;
                    case 17:
                        upPoint = 9;
                        break;
                    case 18:
                        upPoint = 8;
                        break;
                    case 19:
                        upPoint = 9;
                        break;
                    case 20:
                        upPoint = 11;
                        break;
                    case 21:
                        upPoint = 12;
                        break;
                    case 22:
                        upPoint = 11;
                        break;
                    case 23:
                        upPoint = 11;
                        break;
                    case 24:
                        upPoint = 13;
                        break;
                    case 25:
                        upPoint = 11;
                        break;
                    case 26:
                        upPoint = 11;
                        break;
                    case 27:
                        upPoint = 12;
                        break;
                    case 28:
                        upPoint = 11;
                        break;
                    case 29:
                        upPoint = 12;
                        break;
                    case 30:
                        upPoint = 14;
                        break;
                    case 31:
                        upPoint = 14;
                        break;
                    case 32:
                        upPoint = 15;
                        break;
                    case 33:
                        upPoint = 14;
                        break;
                    case 34:
                        upPoint = 14;
                        break;
                    case 35:
                        upPoint = 15;
                        break;
                    case 36:
                        upPoint = 16;
                        break;
                    case 37:
                        upPoint = 14;
                        break;
                    case 38:
                        upPoint = 15;
                        break;
                    case 39:
                        upPoint = 14;
                        break;
                    case 40:
                        upPoint = 20;
                        break;
                }

                return upPoint;
            }
        }

        // s 後編追加
        public int LevelUpPointTruth
        {
            get
            {
                int upPoint = 0;
                switch (this.level)
                {
                    case 1:
                        upPoint = 5;
                        break;
                    case 2:
                        upPoint = 5;
                        break;
                    case 3:
                        upPoint = 5;
                        break;
                    case 4:
                        upPoint = 5;
                        break;
                    case 5:
                        upPoint = 5;
                        break;
                    case 6:
                        upPoint = 6;
                        break;
                    case 7:
                        upPoint = 6;
                        break;
                    case 8:
                        upPoint = 6;
                        break;
                    case 9:
                        upPoint = 7;
                        break;
                    case 10:
                        upPoint = 8;
                        break;
                    case 11:
                        upPoint = 8;
                        break;
                    case 12:
                        upPoint = 8;
                        break;
                    case 13:
                        upPoint = 8;
                        break;
                    case 14:
                        upPoint = 8;
                        break;
                    case 15:
                        upPoint = 9;
                        break;
                    case 16:
                        upPoint = 9;
                        break;
                    case 17:
                        upPoint = 9;
                        break;
                    case 18:
                        upPoint = 10;
                        break;
                    case 19:
                        upPoint = 10;
                        break;
                        
                    case 20:
                        upPoint = 12;
                        break;
                    case 21:
                        upPoint = 12;
                        break;
                    case 22:
                        upPoint = 12;
                        break;
                    case 23:
                        upPoint = 13;
                        break;
                    case 24:
                        upPoint = 14;
                        break;
                    case 25:
                        upPoint = 15;
                        break;
                    case 26:
                        upPoint = 16;
                        break;
                    case 27:
                        upPoint = 17;
                        break;
                    case 28:
                        upPoint = 19;
                        break;
                    case 29:
                        upPoint = 21;
                        break;

                    case 30:
                        upPoint = 25;
                        break;
                    case 31:
                        upPoint = 28;
                        break;
                    case 32:
                        upPoint = 31;
                        break;
                    case 33:
                        upPoint = 34;
                        break;
                    case 34:
                        upPoint = 37;
                        break;
                    case 35:
                        upPoint = 40;
                        break;
                    case 36:
                        upPoint = 43;
                        break;
                    case 37:
                        upPoint = 47;
                        break;
                    case 38:
                        upPoint = 51;
                        break;
                    case 39:
                        upPoint = 55;
                        break;

                    case 40:
                        upPoint = 61;
                        break;
                    case 41:
                        upPoint = 65;
                        break;
                    case 42:
                        upPoint = 69;
                        break;
                    case 43:
                        upPoint = 73;
                        break;
                    case 44:
                        upPoint = 77;
                        break;
                    case 45:
                        upPoint = 81;
                        break;
                    case 46:
                        upPoint = 86;
                        break;
                    case 47:
                        upPoint = 91;
                        break;
                    case 48:
                        upPoint = 96;
                        break;
                    case 49:
                        upPoint = 101;
                        break;

                    case 50:
                        upPoint = 109;
                        break;
                    case 51:
                        upPoint = 115;
                        break;
                    case 52:
                        upPoint = 121;
                        break;
                    case 53:
                        upPoint = 127;
                        break;
                    case 54:
                        upPoint = 133;
                        break;
                    case 55:
                        upPoint = 140;
                        break;
                    case 56:
                        upPoint = 147;
                        break;
                    case 57:
                        upPoint = 154;
                        break;
                    case 58:
                        upPoint = 162;
                        break;
                    case 59:
                        upPoint = 170;
                        break;

                    case 60:
                        upPoint = 180;
                        break;
                    case 61:
                        upPoint = 188;
                        break;
                    case 62:
                        upPoint = 196;
                        break;
                    case 63:
                        upPoint = 204;
                        break;
                    case 64:
                        upPoint = 212;
                        break;
                    case 65:
                        upPoint = 221;
                        break;
                    case 66:
                        upPoint = 230;
                        break;
                    case 67:
                        upPoint = 239;
                        break;
                    case 68:
                        upPoint = 248;
                        break;
                    case 69:
                        upPoint = 258;
                        break;
                }

                return upPoint;
            }
        }
        // e 後編追加

        public int LevelUpLifeTruth
        {
            get
            {
                int upPoint = 0;
                switch (this.level)
                {
                    case 1:
                        upPoint = 20;
                        break;
                    case 2:
                        upPoint = 20;
                        break;
                    case 3:
                        upPoint = 20;
                        break;
                    case 4:
                        upPoint = 20;
                        break;
                    case 5:
                        upPoint = 20;
                        break;
                    case 6:
                        upPoint = 20;
                        break;
                    case 7:
                        upPoint = 20;
                        break;
                    case 8:
                        upPoint = 20;
                        break;
                    case 9:
                        upPoint = 20;
                        break;
                    case 10:
                        upPoint = 20;
                        break;
                    case 11:
                        upPoint = 20;
                        break;
                    case 12:
                        upPoint = 20;
                        break;
                    case 13:
                        upPoint = 20;
                        break;
                    case 14:
                        upPoint = 20;
                        break;
                    case 15:
                        upPoint = 20;
                        break;
                    case 16:
                        upPoint = 20;
                        break;
                    case 17:
                        upPoint = 20;
                        break;
                    case 18:
                        upPoint = 20;
                        break;
                    case 19:
                        upPoint = 20;
                        break;
                        
                    case 20:
                        upPoint = 30;
                        break;
                    case 21:
                        upPoint = 40;
                        break;
                    case 22:
                        upPoint = 50;
                        break;
                    case 23:
                        upPoint = 60;
                        break;
                    case 24:
                        upPoint = 70;
                        break;
                    case 25:
                        upPoint = 80;
                        break;
                    case 26:
                        upPoint = 90;
                        break;
                    case 27:
                        upPoint = 100;
                        break;
                    case 28:
                        upPoint = 110;
                        break;
                    case 29:
                        upPoint = 120;
                        break;

                    case 30:
                        upPoint = 140;
                        break;
                    case 31:
                        upPoint = 160;
                        break;
                    case 32:
                        upPoint = 180;
                        break;
                    case 33:
                        upPoint = 200;
                        break;
                    case 34:
                        upPoint = 220;
                        break;
                    case 35:
                        upPoint = 240;
                        break;
                    case 36:
                        upPoint = 260;
                        break;
                    case 37:
                        upPoint = 280;
                        break;
                    case 38:
                        upPoint = 300;
                        break;
                    case 39:
                        upPoint = 320;
                        break;

                    case 40:
                        upPoint = 360;
                        break;
                    case 41:
                        upPoint = 400;
                        break;
                    case 42:
                        upPoint = 440;
                        break;
                    case 43:
                        upPoint = 480;
                        break;
                    case 44:
                        upPoint = 520;
                        break;
                    case 45:
                        upPoint = 560;
                        break;
                    case 46:
                        upPoint = 600;
                        break;
                    case 47:
                        upPoint = 640;
                        break;
                    case 48:
                        upPoint = 680;
                        break;
                    case 49:
                        upPoint = 720;
                        break;

                    case 50:
                        upPoint = 800;
                        break;
                    case 51:
                        upPoint = 880;
                        break;
                    case 52:
                        upPoint = 960;
                        break;
                    case 53:
                        upPoint = 1040;
                        break;
                    case 54:
                        upPoint = 1120;
                        break;
                    case 55:
                        upPoint = 1200;
                        break;
                    case 56:
                        upPoint = 1280;
                        break;
                    case 57:
                        upPoint = 1360;
                        break;
                    case 58:
                        upPoint = 1440;
                        break;
                    case 59:
                        upPoint = 1520;
                        break;

                    case 60:
                        upPoint = 1670;
                        break;
                    case 61:
                        upPoint = 1820;
                        break;
                    case 62:
                        upPoint = 1980;
                        break;
                    case 63:
                        upPoint = 2140;
                        break;
                    case 64:
                        upPoint = 2320;
                        break;
                    case 65:
                        upPoint = 2500;
                        break;
                    case 66:
                        upPoint = 2700;
                        break;
                    case 67:
                        upPoint = 2900;
                        break;
                    case 68:
                        upPoint = 3130;
                        break;
                    case 69:
                        upPoint = 3360;
                        break;
                }

                return upPoint;
            }
        }

        public int LevelUpManaTruth
        {
            get
            {
                int upPoint = 0;
                switch (this.level)
                {
                    case 1:
                        upPoint = 0;
                        break;
                    case 2:
                        upPoint = 15;
                        break;
                    case 3:
                        upPoint = 15;
                        break;
                    case 4:
                        upPoint = 15;
                        break;
                    case 5:
                        upPoint = 15;
                        break;
                    case 6:
                        upPoint = 15;
                        break;
                    case 7:
                        upPoint = 15;
                        break;
                    case 8:
                        upPoint = 15;
                        break;
                    case 9:
                        upPoint = 15;
                        break;
                    case 10:
                        upPoint = 15;
                        break;
                    case 11:
                        upPoint = 15;
                        break;
                    case 12:
                        upPoint = 15;
                        break;
                    case 13:
                        upPoint = 15;
                        break;
                    case 14:
                        upPoint = 15;
                        break;
                    case 15:
                        upPoint = 15;
                        break;
                    case 16:
                        upPoint = 15;
                        break;
                    case 17:
                        upPoint = 15;
                        break;
                    case 18:
                        upPoint = 15;
                        break;
                    case 19:
                        upPoint = 15;
                        break;

                    case 20:
                        upPoint = 20;
                        break;
                    case 21:
                        upPoint = 25;
                        break;
                    case 22:
                        upPoint = 30;
                        break;
                    case 23:
                        upPoint = 35;
                        break;
                    case 24:
                        upPoint = 40;
                        break;
                    case 25:
                        upPoint = 45;
                        break;
                    case 26:
                        upPoint = 50;
                        break;
                    case 27:
                        upPoint = 55;
                        break;
                    case 28:
                        upPoint = 60;
                        break;
                    case 29:
                        upPoint = 65;
                        break;

                    case 30:
                        upPoint = 80;
                        break;
                    case 31:
                        upPoint = 95;
                        break;
                    case 32:
                        upPoint = 110;
                        break;
                    case 33:
                        upPoint = 125;
                        break;
                    case 34:
                        upPoint = 140;
                        break;
                    case 35:
                        upPoint = 155;
                        break;
                    case 36:
                        upPoint = 170;
                        break;
                    case 37:
                        upPoint = 185;
                        break;
                    case 38:
                        upPoint = 200;
                        break;
                    case 39:
                        upPoint = 215;
                        break;

                    case 40:
                        upPoint = 245;
                        break;
                    case 41:
                        upPoint = 275;
                        break;
                    case 42:
                        upPoint = 305;
                        break;
                    case 43:
                        upPoint = 335;
                        break;
                    case 44:
                        upPoint = 365;
                        break;
                    case 45:
                        upPoint = 395;
                        break;
                    case 46:
                        upPoint = 425;
                        break;
                    case 47:
                        upPoint = 455;
                        break;
                    case 48:
                        upPoint = 485;
                        break;
                    case 49:
                        upPoint = 515;
                        break;

                    case 50:
                        upPoint = 565;
                        break;
                    case 51:
                        upPoint = 615;
                        break;
                    case 52:
                        upPoint = 665;
                        break;
                    case 53:
                        upPoint = 715;
                        break;
                    case 54:
                        upPoint = 765;
                        break;
                    case 55:
                        upPoint = 815;
                        break;
                    case 56:
                        upPoint = 865;
                        break;
                    case 57:
                        upPoint = 915;
                        break;
                    case 58:
                        upPoint = 965;
                        break;
                    case 59:
                        upPoint = 1015;
                        break;

                    case 60:
                        upPoint = 1105;
                        break;
                    case 61:
                        upPoint = 1195;
                        break;
                    case 62:
                        upPoint = 1295;
                        break;
                    case 63:
                        upPoint = 1395;
                        break;
                    case 64:
                        upPoint = 1510;
                        break;
                    case 65:
                        upPoint = 1625;
                        break;
                    case 66:
                        upPoint = 1760;
                        break;
                    case 67:
                        upPoint = 1895;
                        break;
                    case 68:
                        upPoint = 2065;
                        break;
                    case 69:
                        upPoint = 2240;
                        break;
                }

                return upPoint;
            }
        }

        public string GetCharacterSentence(int sentenceNumber)
        {
            // 0 ~ 999 戦闘中
            // 10000 ~ 99999 戦闘中(後編追加)
            // 1000 ~ 1999 ホームタウン会話
            // 2000 ~ 2999 ステータス表示時
            // 3000 ~ 3999 ガンツ武具店会話
            // 4000 ~ 4999 後編戦闘の戦闘コマンド設定時
            #region "アイン"
            if (this.name == "アイン")
            {
                switch (sentenceNumber)
                {
                    case 0: // スキル不足
                        return this.name + "：しまった！スキルポイントが足りねぇ！\r\n";
                    case 1: // Straight Smash
                        return this.name + "：行くぜ！オラァ！！\r\n";
                    case 2: // Double Slash 1
                        return this.name + "：ほらよ！\r\n";
                    case 3: // Double Slash 2
                        return this.name + "：もういっちょ！\r\n";
                    case 4: // Painful Insanity
                        return this.name + "：【心眼奥義】狂乱の痛みを食らい続けな！\r\n";
                    case 5: // empty skill
                        return this.name + "：しまった。スキルを選択してねえ！！\r\n";
                    case 6: // 絡みつくフランシスの必殺を食らった時
                        return this.name + "：ってぇな！！クソ！\r\n";
                    case 7: // Lizenosの必殺を食らった時
                        return this.name + ": 全然・・・動きが見えねえ・・・\r\n";
                    case 8: // Minfloreの必殺を食らった時
                        return this.name + "：ッグ、ガハアァァ！！！\r\n";
                    case 9: // Fresh Healによるライフ回復
                        return this.name + "：{0} 回復したぜ。\r\n";
                    case 10: // Fire Ball
                        return this.name + "：この炎をくらえ！FireBall！ {0} へ {1} のダメージ\r\n";
                    case 11: // Flame Strike
                        return this.name + "：焼け落ちろ！FlameStrike！ {0} へ {1} のダメージ\r\n";
                    case 12: // Volcanic Wave
                        return this.name + "：灼熱の業火！くらえ！VolcanicWave！ {0} へ {1} のダメージ\r\n";
                    case 13: // 通常攻撃クリティカルヒット
                        return this.name + "：オラァ、クリティカルヒット！ {0} へ {1}のダメージ\r\n";
                    case 14: // FlameAuraによる追加攻撃
                        return this.name + "：燃えろ！ {0}の追加ダメージ\r\n";
                    case 15: //遠見の青水晶を戦闘中に使ったとき
                        return this.name + "：戦闘中は使えねえぞコレ。\r\n";
                    case 16: // 効果を発揮しないアイテムを戦闘中に使ったとき
                        return this.name + "：・・・？？ よく分からないアイテムだ。何もおきねえ！\r\n";
                    case 17: // 魔法でマナ不足
                        return this.name + "：マナが足りねぇ！\r\n";
                    case 18: // Protection
                        return this.name + "：聖神の加護、Protection！物理防御力：ＵＰ\r\n";
                    case 19: // Absorb Water
                        return this.name + "：水女神の加護・・・AbsorbWater！ 魔法防御力：ＵＰ。\r\n";
                    case 20: // Saint Power
                        return this.name + "：力こそ全て、SaintPower！ 物理攻撃力：ＵＰ\r\n";
                    case 21: // Shadow Pact
                        return this.name + "：闇との契約・・・ShadowPact！ 魔法攻撃力：ＵＰ\r\n";
                    case 22: // Bloody Vengeance
                        return this.name + "：闇の使者よ力を示せ・・・BloodyVengeance！ 力パラメタが {0} ＵＰ\r\n";
                    case 23: // Holy Shock
                        return this.name + "：聖なる鉄槌、HolyShock！ {0} へ {1} のダメージ\r\n";
                    case 24: // Glory
                        return this.name + "：常に栄光あれ。Glory！ 直接攻撃＋FreshHeal連携のオーラ\r\n";
                    case 25: // CelestialNova 1
                        return this.name + "：聖なる波動、CelestialNova！ {0} 回復したぜ。\r\n";
                    case 26: // CelestialNova 2
                        return this.name + "：聖なる裁きを食らえ、CelestialNova！ {0} のダメージ\r\n";
                    case 27: // Dark Blast
                        return this.name + "：黒の波動を食らえ、DarkBlast！ {0} へ {1} のダメージ\r\n";
                    case 28: // Lava Annihilation
                        return this.name + "：炎授天使、召還！くらえ！LavaAnnihilation！ {0} へ {1} のダメージ\r\n";
                    case 10028: // Lava Annihilation後編
                        return this.name + "：炎授天使、召還！くらえ！LavaAnnihilation！\r\n";
                    case 29: // Devouring Plague
                        return this.name + "：体力を食らい尽くすぜ。DevouringPlague！ {0} ライフを吸い取った\r\n";
                    case 30: // Ice Needle
                        return this.name + "：氷の刃を食らえ、IceNeedle！ {0} へ {1} のダメージ\r\n";
                    case 31: // Frozen Lance
                        return this.name + "：凍りの槍でぶち抜かれろ、FrozenLance！ {0} へ {1} のダメージ\r\n";
                    case 32: // Tidal Wave
                        return this.name + "：大海原よ！その力を見せ付けろ、TidalWave！ {0} のダメージ\r\n";
                    case 33: // Word of Power
                        return this.name + "：この衝撃波でも食らえ、WordOfPower！ {0} へ {1} のダメージ\r\n";
                    case 34: // White Out
                        return this.name + "：全ての感覚から抹殺せしめろ、WhiteOut！ {0} へ {1} のダメージ\r\n";
                    case 35: // Black Contract
                        return this.name + "：悪魔取引だ、BlackContract！ " + this.name + "のスキル、魔法コストは０になる。\r\n";
                    case 36: // Flame Aura詠唱
                        return this.name + "：炎のシンボルよ来たれ、FlameAura！ 直接攻撃に炎の追加効果が付与される。\r\n";
                    case 37: // Damnation
                        return this.name + "：朽ち果てろ、Damnation！ 死淵より出でる黒が空間を歪ませる。\r\n";
                    case 38: // Heat Boost
                        return this.name + "：炎授天使よ、炎の勢い借りるぜ、HeatBoost！ 技パラメタが {0} ＵＰ。\r\n";
                    case 39: // Immortal Rave
                        return this.name + "：オラオラオラァいくぜ、ImmortalRave！ " + this.name + "の周りに３つの炎が宿った。\r\n";
                    case 40: // Gale Wind
                        return this.name + "：同時に行くぜ、GaleWind！ もう一人の" + this.name + "が現れた。\r\n";
                    case 41: // Word of Life
                        return this.name + "：自然と共にあれ、WordOfLife！ 大自然からの強い息吹を感じ取れるようになった。\r\n";
                    case 42: // Word of Fortune
                        return this.name + "：強き未来を示せ、WordOfFortune！ 決死のオーラが湧き上がった。\r\n";
                    case 43: // Aether Drive
                        return this.name + "：空想の物理力を示せ、AetherDrive！ 周囲全体に空想物理力がみなぎる。\r\n";
                    case 44: // Eternal Presence 1
                        return this.name + "：法則と原理を呼び起こすぜ、EternalPresence！ " + this.name + "の周りに新しい法則が構築される。\r\n";
                    case 45: // Eternal Presence 2
                        return this.name + "の物理攻撃、物理防御、魔法攻撃、魔法防御がＵＰした！\r\n";
                    case 46: // One Immunity
                        return this.name +  "：空間障壁、OneImmunity！ " + this.name + "の周囲に目に見えない障壁が発生。\r\n";
                    case 47: // Time Stop
                        return this.name + "：全ては時空干渉から・・・TimeStop！ 敵の時空を引き裂き時間停止させた。\r\n";
                    case 48: // Dispel Magic
                        return this.name + "：ウザってぇ効果は消えて無くなれ、DispelMagic！ \r\n";
                    case 49: // Rise of Image
                        return this.name + "：時空の支配者より新たなるイメージを借りるぜ・・・RiseOfImage！ 心パラメタが {0} ＵＰ\r\n";
                    case 50: // 空詠唱
                        return this.name + "：しまった。空詠唱だ！！\r\n";
                    case 51: // Inner Inspiration
                        return this.name + "：は潜在集中力を高めた。精神が研ぎ澄まされる。 {0} スキルポイント回復\r\n";
                    case 52: // Resurrection 1
                        return this.name + "：偉大なる聖の力。奇跡起こすぜ、Resurrection！！\r\n";
                    case 53: // Resurrection 2
                        return this.name + "：しまった！対象を間違えたじゃねえか！！\r\n";
                    case 54: // Resurrection 3
                        return this.name + "：しまった！死んでねぇぞ！！\r\n";
                    case 55: // Resurrection 4
                        return this.name + "：おいおい、自分にやっても意味ねえからな・・・\r\n";
                    case 56: // Stance Of Standing
                        return this.name + "：この体制のまま・・・っしゃ、攻撃だ！\r\n";
                    case 57: // Mirror Image
                        return this.name + "：魔法をはね返すぜ、MirrorImage！ {0}の周囲に青い空間が発生した。\r\n";
                    case 58: // Mirror Image 2
                        return this.name + "：おっしゃ、はね返せ！ {0} ダメージを {1} に反射した！\r\n";
                    case 59: // Mirror Image 3
                        return this.name + "：おっしゃ、はね返せ！ 【しかし、強力な威力ではね返せない！】" + this.name + "：バカな！？\r\n";
                    case 60: // Deflection
                        return this.name + "：物理攻撃をはね返すぜ、Deflection！ {0}の周囲に白い空間が発生した。\r\n";
                    case 61: // Deflection 2
                        return this.name + "：おっしゃ、はね返せ！ {0} ダメージを {1} に反射した！\r\n";
                    case 62: // Deflection 3
                        return this.name + "：おっしゃ、はね返せ！ 【しかし、強力な威力ではね返せない！】" + this.name + "：ッグ、グアァ！\r\n";
                    case 63: // Truth Vision
                        return this.name + "：本質を見切ってやるぜ、TruthVision！　" + this.name + "は対象のパラメタＵＰを無視するようになった。\r\n";
                    case 64: // Stance Of Flow
                        return this.name + "：俺はこれ苦手だけどな・・・StanceOfFlow！　" + this.name + "は次３ターン、必ず後攻を取るように構えた。\r\n";
                    case 65: // Carnage Rush 1
                        return this.name + "：オラオラ、連発ラッシュいくぜ、CarnageRush！ ほらよ！ {0}ダメージ・・・   ";
                    case 66: // Carnage Rush 2
                        return "もういっちょ！ {0}ダメージ   ";
                    case 67: // Carnage Rush 3
                        return "オラオラァ！ {0}ダメージ   ";
                    case 68: // Carnage Rush 4
                        return "まだまだまぁ！ {0}ダメージ   ";
                    case 69: // Carnage Rush 5
                        return "食らええぇ！！ {0}のダメージ\r\n";
                    case 70: // Crushing Blow
                        return this.name + "：ガツンと一撃止めて見せるぜ、CrushingBlow！  {0} へ {1} のダメージ。\r\n";
                    case 71: // リーベストランクポーション戦闘使用時
                        return this.name + "：これでも・・・食らいな！\r\n";
                    case 72: // Enigma Sence
                        return this.name + "：力以外のチカラっての見せてやるぜ、EnigmaSennce！\r\n";
                    case 73: // Soul Infinity
                        return this.name + "：俺の全てを注ぎ込むぜ！オラアアァァ！！ SoulInfinity！！！\r\n";
                    case 74: // Kinetic Smash
                        return this.name + "：剣による攻撃、最大限にやってやるぜ！オラァ、KineticSmash！\r\n";
                    case 75: // Silence Shot (Altomo専用)
                        return "";
                    case 76: // Silence Shot、AbsoluteZero沈黙による詠唱失敗
                        return this.name + "：・・・ックソ！詠唱失敗だ！！\r\n";
                    case 77: // Cleansing
                        return this.name + "：妙な効果は全部これで洗い流すぜ、Cleansing！\r\n";
                    case 78: // Pure Purification
                        return this.name + "：精神さえしっかり保てば直せるはずだ、PurePurification・・・\r\n";
                    case 79: // Void Extraction
                        return this.name + "：最大限界値、さらに超えるぜ。VoidExtraction！" + this.name + "の {0} が {1}ＵＰ！\r\n";
                    case 80: // アカシジアの実使用時
                        return this.name + "：ほらよ、これで少しは目が醒めるだろ。\r\n";
                    case 81: // Absolute Zero
                        return this.name + "：氷付けにしてやるぜ！　AbsoluteZero！ 氷吹雪効果により、ライフ回復不可、スペル詠唱不可、スキル使用不可、防御不可となった。\r\n";
                    case 82: // BUFFUP効果が望めない場合
                        return "しかし、既にそのパラメタは上昇しているため、効果がなかった。\r\n";
                    case 83: // Promised Knowledge
                        return this.name + "：知識も一つの力だぜ、PromiesdKnowledge！　知パラメタが {0} ＵＰ\r\n";
                    case 84: // Tranquility
                        return this.name + "：うっとおしい効果だな。消えちまいな、Tranquility！\r\n";
                    case 85: // High Emotionality 1
                        return this.name + "：俺の最大能力はこんなものじゃねえ、ウオオォォ、HighEmotionality！\r\n";
                    case 86: // High Emotionality 2
                        return this.name + "の力、技、知、心パラメタがＵＰした！\r\n";
                    case 87: // AbsoluteZeroでスキル使用失敗
                        return this.name + "：だ・・・だめだ、全く動けねえ！　スキル使用ミスったぜ。\r\n";
                    case 88: // AbsoluteZeroによる防御失敗
                        return this.name + "：くそ・・・体が・・・防御できねえ！ \r\n";
                    case 89: // Silent Rush 1
                        return this.name + "：この３連打、受けてみろ、SilentRush！ ほらよ！ {0}ダメージ・・・   ";
                    case 90: // Silent Rush 2
                        return "もういっちょ！ {0}ダメージ   ";
                    case 91: // Silent Rush 3
                        return "３発目だ！おらぁ！ {0}のダメージ\r\n";
                    case 92: // BUFFUP以外の永続効果が既についている場合
                        return "しかし、既にその効果は付与されている。\r\n";
                    case 93: // Anti Stun
                        return this.name + "：スタン効果は俺には効かねえぜ、AntiStun！ " + this.name + "はスタン効果への耐性が付いた\r\n";
                    case 94: // Anti Stunによるスタン回避
                        return this.name + "：ってぇ・・・だが、スタンしないぜ。\r\n";
                    case 95: // Stance Of Death
                        return this.name + "：俺は耐えて見せるぜ、StanceOfDeath！ " + this.name + "は致死を１度回避できるようになった\r\n";
                    case 96: // Oboro Impact 1
                        return this.name + "：朧、刻むぜ【究極奥義】Oboro Impact！！\r\n";
                    case 97: // Oboro Impact 2
                        return this.name + "：オラアアァァァ！！　 {0}へ{1}のダメージ\r\n";
                    case 98: // Catastrophe 1
                        return this.name + "：芯から破壊してやるぜ【究極奥義】Catastrophe！\r\n";
                    case 99: // Catastrophe 2
                        return this.name + "：食らいやがれぇ！！　 {0}のダメージ\r\n";
                    case 100: // Stance Of Eyes
                        return this.name + "：その行動、見切ってみせるぜ、 StanceOfEyes！ " + this.name + "は、相手の行動に備えている・・・\r\n";
                    case 101: // Stance Of Eyesによるキャンセル時
                        return this.name + "：っしゃ、ソコだ！！　" + this.name + "は相手のモーションを見切って、行動キャンセルした！\r\n";
                    case 102: // Stance Of Eyesによるキャンセル失敗時
                        return this.name + "：駄目だ・・・全然見切れねえ・・・　" + this.name + "は相手のモーションを見切れなかった！\r\n";
                    case 103: // Negate
                        return this.name + "：スペル詠唱なんかさせるかよ、Negate！　" + this.name + "は相手のスペル詠唱に備えている・・・\r\n";
                    case 104: // Negateによるスペル詠唱キャンセル時
                        return this.name + "：お見通しだぜ！" + this.name + "は相手のスペル詠唱を弾いた！\r\n";
                    case 105: // Negateによるスペル詠唱キャンセル失敗時
                        return this.name + "：っくそ、全然詠唱タイミングが・・・" + this.name + "は相手のスペル詠唱を見切れなかった！\r\n";
                    case 106: // Nothing Of Nothingness 1
                        return this.name + "：もうゼロに還したりさせねえぜ、【究極奥義】NothingOfNothingness！ " + this.name + "に無色のオーラが宿り始める！ \r\n";
                    case 107: // Nothing Of Nothingness 2
                        return this.name + "は無効化魔法を無効化、　無効化するスキルを無効化するようになった！\r\n";
                    case 108: // Genesis
                        return this.name + "：行動原理の起源、刻み込んであるぜ、Genesis！  " + this.name + "は前回の行動を自分へと投影させた！\r\n";
                    case 109: // Cleansing詠唱失敗時
                        return this.name + "：っくそ・・・調子が思わしくねえ、悪いがCleansingは出来ねえ。\r\n";
                    case 110: // CounterAttackを無視した時
                        return this.Name + "：その構えはもう見飽きたぜ！\r\n";
                    case 111: // 神聖水使用時
                        return this.name + "：ライフ・スキル・マナが30%ずつ回復したぜ。\r\n";
                    case 112: // 神聖水、既に使用済みの場合
                        return this.name + "：もう枯れてしまってるぜ。一日１回だけだな。\r\n";
                    case 113: // CounterAttackによる反撃メッセージ
                        return this.name + "：おっしゃ、見切った！カウンターだ！\r\n";
                    case 114: // CounterAttackに対する反撃がNothingOfNothingnessによって防がれた時
                        return this.name + "：ックソ、どうなってんだ。見切れねえ！\r\n";
                    case 115: // 通常攻撃のヒット
                        return this.name + "の攻撃がヒット。 {0} へ {1} のダメージ\r\n";
                    case 116: // 防御を無視して攻撃する時
                        return this.name + "：防御なんてお見通しだぜ！貫通しろ！！\r\n";
                    case 117: // StraightSmashなどのスキルクリティカル
                        return this.name + "：っしゃ！クリティカルだぜ！\r\n";
                    case 118: // 戦闘時、リヴァイヴポーションによる復活のかけ声
                        return this.name + "：っしゃ！これで復活だぜ！\r\n";
                    case 119: // Absolute Zeroによりライフ回復できない場合
                        return this.name + "：っくそ・・・ライフが回復できねえ・・・\r\n";
                    case 120: // 魔法攻撃のヒット
                        return "{0} へ {1} のダメージ\r\n";
                    case 121: // Absolute Zeroによりマナ回復できない場合
                        return this.name + "：っくそ・・・マナが回復できねえ・・・\r\n";
                    case 122: // 「ためる」行動時
                        return this.name + "：魔力、蓄えさせてもらったぜ。\r\n";
                    case 123: // 「ためる」行動で溜まりきっている場合
                        return this.name + "：魔力の蓄積はこれが上限みたいだな。\r\n";
                    case 124: // StraightSmashのダメージ値
                        return this.name + "のストレート・スマッシュがヒット。 {0} へ {1}のダメージ\r\n";
                    case 125: // アイテム使用ゲージが溜まってないとき
                        return this.name + "：アイテムゲージが溜まってないとアイテムは使えないぜ。\r\n";
                    case 126: // FlashBlase
                        return this.name + "：この閃光でも食らいな、FlashBlaze！ {0} へ {1} のダメージ\r\n";
                    case 127: // 複合魔法でインスタント不足
                        return this.name + "：っくそ、まだインスタント値が足りねぇ！\r\n";
                    case 128: // 複合魔法はインスタントタイミングでうてない
                        return this.name + "：だめだ、これはインスタントで発動できねえ。\r\n";
                    case 129: // PsychicTrance
                        return this.name + "：多少の犠牲を払ってでも魔法攻撃力、更に強化するぜ！PsychicTrance！\r\n";
                    case 130: // BlindJustice
                        return this.name + "：打ち合い覚悟だ。物理攻撃、更に強化するぜ！BlindJustice！\r\n";
                    case 131: // TranscendentWish
                        return this.name + "：この一瞬でケリをつける、TranscendentWish！\r\n";
                    case 132: // LightDetonator
                        return this.name + "：そのフィールド全体、もらった！ LightDetonator！\r\n";
                    case 133: // AscendantMeteor
                        return this.name + "：食らえ！怒涛の１０連撃、AscendantMeteor！\r\n";
                    case 134: // SkyShield
                        return this.name + "：空と聖より加護を得るぜ、SkyShield！\r\n";
                    case 135: // SacredHeal
                        return this.name + "：全員回復っと！　SacredHeal！\r\n";
                    case 136: // EverDroplet
                        return this.name + "：マナの循環もらったぜ、EverDroplet！\r\n";
                    case 137: // FrozenAura
                        return this.name + "：氷の魔法剣を発動するぜ、FrozenAura！\r\n";
                    case 138: // ChillBurn
                        return this.name + "：凍りつけ！ ChillBurn！\r\n";
                    case 139: // ZetaExplosion
                        return this.name + "：究極！ ZetaExplosion！\r\n";
                    case 140: // FrozenAura追加効果ダメージで
                        return this.name + "：凍りつきな！ {0}の追加ダメージ\r\n";
                    case 141: // StarLightning
                        return this.name + "：コレで気絶しな、StarLightning！\r\n";
                    case 142: // WordOfMalice
                        return this.name + "：動きを鈍くさせるぜ、WordOfMalice！\r\n";
                    case 143: // BlackFire
                        return this.name + "：魔法防御落としてやるぜ、BlackFire！\r\n";
                    case 144: // EnrageBlast
                        return this.name + "：ブチかますぜ、EnrageBlast！\r\n";
                    case 145: // Immolate
                        return this.name + "：物理防御落としてやるぜ、Immolate！\r\n";
                    case 146: // VanishWave
                        return this.name + "：黙らせてやるぜ、VanishWave！\r\n";
                    case 147: // WordOfAttitude
                        return this.name + "：インスタント回復させるぜ、WordOfAttitude！\r\n";
                    case 148: // HolyBreaker
                        return this.name + "：これでダメージレースを優位にしてみせる。HolyBreaker！\r\n";
                    case 149: // DarkenField
                        return this.name + "：防御力をガタ落ちにしてやるぜ、DarkenField！\r\n";
                    case 150: // SeventhMagic
                        return this.name + "：今、全てを覆すぜ、SeventhMagic！\r\n";
                    case 151: // BlueBullet
                        return this.name + "：氷の弾丸を連射するぜ、BlueBullet！\r\n";
                    case 152: // NeutralSmash
                        return this.name + "：っしゃ、NeutralSmash！\r\n";
                    case 153: // SwiftStep
                        return this.name + "：速度値上げてくぜ、SwiftStep！\r\n";
                    case 154: // CircleSlash
                        return this.name + "：コレでも食らいな、CircleSlash！\r\n";
                    case 155: // RumbleShout
                        return this.name + "：どこ見てる、コッチだ！\r\n";
                    case 156: // SmoothingMove
                        return this.name + "：スライド式戦闘法、SmoothingMove！\r\n";
                    case 157: // FutureVision
                        return this.name + "：次のターン、見切ったぜ。FutureVision！\r\n";
                    case 158: // ReflexSpirit
                        return this.name + "：スタンや麻痺はゴメンだぜ、ReflexSpirit！\r\n";
                    case 159: // SharpGlare
                        return this.name + "：黙らせてやるぜ、SharpGlare！\r\n";
                    case 160: // TrustSilence
                        return this.name + "：沈黙や誘惑にかかってられるか、TrustSilence！\r\n";
                    case 161: // SurpriseAttack
                        return this.name + "：全員蹴散らしてやるぜ、SurpriseAttack！\r\n";
                    case 162: // PsychicWave
                        return this.name + "：この技は止められやしねえぜ、PsychicWave！\r\n";
                    case 163: // Recover
                        return this.name + "：しっかりしろ！Recover！\r\n";
                    case 164: // ViolentSlash
                        return this.name + "：見切れねえだろコイツは、ViolentSlash！\r\n";
                    case 165: // OuterInspiration
                        return this.name + "：ポテンシャル、引き戻すぜ、OuterInspiration！\r\n";
                    case 166: // StanceOfSuddenness
                        return this.name + "：っしゃ、ソレだ！StanceOfSuddenness！\r\n";
                    case 167: // インスタント対象で発動不可
                        return this.name + "：ダメだ、こいつはインスタント対象専用だ。\r\n";
                    case 168: // StanceOfMystic
                        return this.name + "：もうダメージは喰らわねえ、StanceOfMystic！\r\n";
                    case 169: // HardestParry
                        return this.name + "：瞬間で捉えてみせる。HardestParry！\r\n";
                    case 170: // ConcussiveHit
                        return this.name + "：これでも食らいな、コンカッシヴ・ヒット！\r\n";
                    case 171: // Onslaught hit
                        return this.name + "：これでも食らいな、オンスロート・ヒット！\r\n";
                    case 172: // Impulse hit
                        return this.name + "：これでも食らいな、インパルス・ヒット！\r\n";
                    case 173: // Fatal Blow
                        return this.name + "：砕け散れ、フェイタル・ブロー！\r\n";
                    case 174: // Exalted Field
                        return this.name + "：賛美の場を構築するぜ、イグザルティッド・フィールド！\r\n";
                    case 175: // Rising Aura
                        return this.name + "：ガンガン行くぜ、ライジング・オーラ！\r\n";
                    case 176: // Ascension Aura
                        return this.name + "：どんどんぶちかますぜ、アセンション・オーラ！\r\n";
                    case 177: // Angel Breath
                        return this.name + "：っしゃ、全員の状態を戻すぜ、エンジェル・ブレス！\r\n";
                    case 178: // Blazing Field
                        return this.name + "：燃やし尽くすぜ、ブレイジング・フィールド！\r\n";
                    case 179: // Deep Mirror
                        return this.name + "：その手は通さないぜ、ディープ・ミラー！\r\n";
                    case 180: // Abyss Eye
                        return this.name + "：深淵の眼、アビス・アイ！\r\n";
                    case 181: // Doom Blade
                        return this.name + "：精神まで切り刻むぜ、ドゥーム・ブレイド！\r\n";
                    case 182: // Piercing Flame
                        return this.name + "：っしゃ、これでも食らいな！ピアッシング・フレイム！\r\n";
                    case 183: // Phantasmal Wind
                        return this.name + "：反応上げていくぜ、ファンタズマル・ウィンド！\r\n";
                    case 184: // Paradox Image
                        return this.name + "：・・・パラドックス・イメージ！\r\n";
                    case 185: // Vortex Field
                        return this.name + "：この渦の中に埋もれさせてやるぜ、ヴォルテックス・フィールド！\r\n";
                    case 186: // Static Barrier
                        return this.name + "：水と理より加護を得るぜ、スタティック・バリア！\r\n";
                    case 187: // Unknown Shock
                        return this.name + "：盲目にしてやるぜ、アンノウン・ショック！\r\n";
                    case 188: // SoulExecution
                        return this.name + "：奥義　ソウル・エグゼキューション！\r\n";
                    case 189: // SoulExecution hit 01
                        return this.name + "：ッラ！\r\n";
                    case 190: // SoulExecution hit 02
                        return this.name + "：ラァ！\r\n";
                    case 191: // SoulExecution hit 03
                        return this.name + "：ッシャ！\r\n";
                    case 192: // SoulExecution hit 04
                        return this.name + "：ッティ！\r\n";
                    case 193: // SoulExecution hit 05
                        return this.name + "：トォ！\r\n";
                    case 194: // SoulExecution hit 06
                        return this.name + "：ッフ！\r\n";
                    case 195: // SoulExecution hit 07
                        return this.name + "：ッハ！\r\n";
                    case 196: // SoulExecution hit 08
                        return this.name + "：ッハァ！\r\n";
                    case 197: // SoulExecution hit 09
                        return this.name + "：オラァ！\r\n";
                    case 198: // SoulExecution hit 10
                        return this.name + "：終わりだ！！！\r\n";
                    case 199: // Nourish Sense
                        return this.name + "：さらに回復していくぜ、ノリッシュ・センス！\r\n";
                    case 200: // Mind Killing
                        return this.name + "：精神切り刻むぜ、マインド・キリング！\r\n";
                    case 201: // Vigor Sense
                        return this.name + "：反応値上げていくぜ、ヴィゴー・センス！\r\n";
                    case 202: // ONE Authority
                        return this.name + "：おっしゃ、全員上げていこうぜ！ワン・オーソリティ！\r\n";
                    case 203: // 集中と断絶
                        return this.name + "：【集中と断絶】　発動。\r\n";
                    case 204: // 【元核】発動済み
                        return this.name + "：【元核】は今日もう使っちまったぜ。\r\n";
                    case 205: // 【元核】通常行動選択時
                        return this.name + "：【元核】はインスタントタイミング限定だな。\r\n";
                    case 206: // Sigil Of Homura
                        return this.name + "：焔の威力、思い知れ！シギル・オブ・ホムラ！\r\n";
                    case 207: // Austerity Matrix
                        return this.name + "：支配封じさせてもらうぜ、アゥステリティ・マトリクス！\r\n";
                    case 208: // Red Dragon Will
                        return this.name + "：火の竜よ、俺に更なる力を！レッド・ドラゴン・ウィル！\r\n";
                    case 209: // Blue Dragon Will
                        return this.name + "：水の竜よ、俺に更なる力を！ブルー・ドラゴン・ウィル！\r\n";
                    case 210: // Eclipse End
                        return this.name + "：究極の全抹消、エクリプス・エンド！\r\n";
                    case 211: // Sin Fortune
                        return this.name + "：次で決めるぜ、シン・フォーチュン！\r\n";
                    case 212: // AfterReviveHalf
                        return this.name + "：死耐性（ハーフ）を付けるぜ！\r\n";
                    case 213: // Demonic Ignite
                        return this.name + "：黒の焔をその身で受けろ！デーモニック・イグナイト！\r\n";
                    case 214: // Death Deny
                        return this.name + "：死者を完全なる状態で蘇生させるぜ、デス・ディナイ！\r\n";
                    case 215: // Stance of Double
                        return this.name + "：究極の行動原理、スタンス・オブ・ダブル！  " + this.name + "は前回行動を示す自分の分身を発生させた！\r\n";
                    case 216: // 最終戦ライフカウント消費
                        return this.name + "：まだだ・・・まだ、負けられねえんだ！！\r\n";
                    case 217: // 最終戦ライフカウント消滅時
                        return this.name + "：・・・ッグ・・・っく・・・\r\n";

                    case 2001: // ポーションまたは魔法による回復時
                        return this.name + "：よし、{0} 回復したぜ。";
                    case 2002: // レベルアップ終了催促
                        return this.name + "：レベルアップしてからにしようぜ。";
                    case 2003: // 荷物を減らせる催促
                        return this.name + "：{0}、少し荷物を減らしておけよ。アイテムが渡せねえぞ。";
                    case 2004: // 装備判断
                        return this.name + "：よし、装備しとくか？";
                    case 2005: // 装備完了
                        return this.name + "：オーケー、装備完了！";
                    case 2006: // 遠見の青水晶を使用
                        return this.name + "：いったん、町へ戻るか？";
                    case 2007: // 売却専用品に対する一言
                        return this.name + "：売却専用品だな。";
                    case 2008: // 非戦闘時のマナ不足
                        return this.name + "：マナ不足だな。";
                    case 2009: // 神聖水使用時
                        return this.name + "：ライフ・スキル・マナが30%ずつ回復したぜ。";
                    case 2010: // 神聖水、既に使用済みの場合
                        return this.name + "：もう枯れてしまってるぜ。一日１回だけだな。";
                    case 2011: // リーベストランクポーション非戦闘使用時
                        return this.name + "：戦闘中専用品だな。";
                    case 2012: // 遠見の青水晶を使用（町滞在時）
                        return this.name + "：すでに町の中だ。使うだけ無駄だな。";
                    case 2013: // 遠見の青水晶を捨てられない時の台詞
                        return this.name + "：おいおい、さすがにこれを捨てたら駄目だろ。";
                    case 2014: // 非戦闘時のスキル不足
                        return this.name + "：スキル不足だな。";
                    case 2015: // 他プレイヤーが捨ててしまおうとした場合
                        return this.name + "：おっと、それは捨ててもらっちゃ困るぜ。";
                    case 2016: // リヴァイヴポーションによる復活
                        return this.name + "：っしゃ、復活だぜ！サンキュー！";
                    case 2017: // リヴァイヴポーション不要な時
                        return this.name + "：{0}はまだ生きてるじゃねえか。使用する必要はないぜ。";
                    case 2018: // リヴァイヴポーション対象が自分の時
                        return this.name + "：俺自身に使っても意味ないだろ。";
                    case 2019: // 装備不可のアイテムを装備しようとした時
                        return this.name + "：俺には装備出来ないようだ。";
                    case 2020: // ラスボス撃破の後、遠見の青水晶を使用不可
                        return this.name + "：いや、今コレは使うつもりはねえ";
                    case 2021: // アイテム捨てるの催促
                        return this.name + "：バックパックの整理を先にしようぜ。";
                    case 2022: // オーバーシフティング使用開始時
                        return this.name + "：凄えぜ・・・身体のパラメタが再構築されていく・・・";
                    case 2023: // オーバーシフティングによるパラメタ割り振り時
                        return this.name + "：オーバーシフティングによる割り振りを早いトコやってしまおうぜ。";
                    case 2024: // 成長リキッドを使用した時
                        return this.name + "：【{0}】パラメタが {1} 上昇したぜ！";
                    case 2025:
                        return this.name + "：両手武器を持ってる場合、サブは装備できねえぜ。";
                    case 2026:
                        return this.name + "：武器（メイン）にまず何か装備しようぜ。";
                    case 2027: // 清透水使用時
                        return this.name + "：ライフが100%回復したぜ。";
                    case 2028: // 清透水、既に使用済みの場合
                        return this.name + "：もう枯れてしまってるぜ。一日１回だけだな。";
                    case 2029: // 荷物一杯で装備が外せない時
                        return this.name + "：バックパックがいっぱいだ。装備外す前にバックパック整理しようぜ。";
                    case 2030: // 荷物一杯で何かを捨てる時の台詞
                        return this.name + "：{0}を捨てて新しいアイテムを入手するか？";
                    case 2031: // 戦闘中のアイテム使用限定
                        return this.name + "：戦闘中だし、アイテム使用に集中しようぜ。";
                    case 2032: // 戦闘中、アイテム使用できないアイテムを選択したとき
                        return this.name + "：このアイテムは戦闘中に使用は出来ないぜ。";
                    case 2033: // 預けられない場合
                        return this.name + "：これを預けておくわけにはいかねえよな。";
                    case 2034: // アイテムいっぱいで預かり所から引き出せない場合
                        return this.name + "：おっと荷物がいっぱいだぜ。";
                    case 2035: // Sacred Heal
                        return this.name + "：おし、回復したぜ。";

                    case 3000: // 店に入った時の台詞
                        return this.name + "：本当に誰も見張り役が居ないんだな。";
                    case 3001: // 支払い要求時
                        return this.name + "：おし、この {0} を買うぜ。 {1} Gold払えば良いんだな？";
                    case 3002: // 持ち物いっぱいで買えない時
                        return this.name + "：しまった、持ち物がいっぱいだ。手持ちのアイテムを整理してからだな。";
                    case 3003: // 購入完了時
                        return this.name + "：よっしゃ、売買成立！・・・だよな？";
                    case 3004: // Gold不足で購入できない場合
                        return this.name + "：クソ、Goldがまだ{0}足りねえ・・・";
                    case 3005: // 購入せずキャンセルした場合
                        return this.name + "：他のアイテムでも見てみるか";
                    case 3006: // 売れないアイテムを売ろうとした場合
                        return this.name + "：これは手放すわけにはいかねえな。";
                    case 3007: // アイテム売却時
                        return this.name + "：相手がいねえが・・・{0}を置いて、{1}Gold・・・いただくぜ？";
                    case 3008: // 剣紋章ペンダント売却時
                        return this.name + "：コレはちょっと売るのは心がひけるが・・・{0}Goldいただくぜ？"; 
                    case 3009: // 武具店を出る時
                        return this.name + "：ガンツ叔父さん、見張り人ぐらい付けるといいのにな・・・";
                    case 3010: // ガンツ不在時の売りきれフェルトゥーシュを見て一言
                        return this.name + "：元々売ってるわけがねえんだよな・・・";
                    case 3011: // 装備可能なものが購入された時
                        return this.name + "：っしゃ、ここで装備していくぜ？";
                    case 3012: // 装備していた物を売却対象かどうか聞く時
                        return this.name + "：今装備しているは、{0}だ。{1}Goldぐらいで買い取ってもらえるんじゃねえか？";

                    case 4001: // 通常攻撃を選択
                        return this.name + "：っしゃ、攻撃だ！\r\n";
                    case 4002: // 防御を選択
                        return this.name + "：防御姿勢で構えておくか。\r\n";
                    case 4003: // 待機を選択
                        return this.name + "：何もせず待機と行くか・・・\r\n";
                    case 4004: // フレッシュヒールを選択
                        return this.name + "：ここはフレッシュヒールで回復だ。\r\n";
                    case 4005: // プロテクションを選択
                        return this.name + "：防御力上げるぜ、プロテクションだ。\r\n";
                    case 4006: // ファイア・ボールを選択
                        return this.name + "：ファイアボールだ！\r\n";
                    case 4007: // フレイム・オーラを選択
                        return this.name + "：ここは、フレイムを付けておくぜ。\r\n";
                    case 4008: // ストレート・スマッシュを選択
                        return this.name + "：いくぜ！ストレートスマッシュ！\r\n";
                    case 4009: // ダブル・スマッシュを選択
                        return this.name + "：２連撃いくぜ、ダブルスマッシュ！\r\n";
                    case 4010: // スタンス・オブ・スタンディングを選択
                        return this.name + "：守って攻める、スタンディングの構えだ。\r\n";
                    case 4011: // アイス・ニードルを選択
                        return this.name + "：アイスニードルでいくぜ！\r\n";
                    case 4012:
                    case 4013:
                    case 4014:
                    case 4015:
                    case 4016:
                    case 4017:
                    case 4018:
                    case 4019:
                    case 4020:
                    case 4021:
                    case 4022:
                    case 4023:
                    case 4024:
                    case 4025:
                    case 4026:
                    case 4027:
                    case 4028:
                    case 4029:
                    case 4030:
                    case 4031:
                    case 4032:
                    case 4033:
                    case 4034:
                    case 4035:
                    case 4036:
                    case 4037:
                    case 4038:
                    case 4039:
                    case 4040:
                    case 4041:
                    case 4042:
                    case 4043:
                    case 4044:
                    case 4045:
                    case 4046:
                    case 4047:
                    case 4048:
                    case 4049:
                    case 4050:
                    case 4051:
                    case 4052:
                    case 4053:
                    case 4054:
                    case 4055:
                    case 4056:
                    case 4057:
                    case 4058:
                    case 4059:
                    case 4060:
                    case 4061:
                    case 4062:
                    case 4063:
                    case 4064:
                    case 4065:
                    case 4066:
                    case 4067:
                    case 4068:
                    case 4069:
                    case 4070:
                    case 4071:
                        return this.name + "：" + this.ActionLabel.Text + "でいくぜ。\r\n";

                    case 4072:
                        return this.name + "：これは敵にかけられないぜ。\r\n";
                    case 4073:
                        return this.name + "：これは敵にかけられないぜ。\r\n";
                    case 4074:
                        return this.name + "：これは味方にかけたくはねえ。\r\n";
                    case 4075:
                        return this.name + "：これは味方にかけたくはねえ。\r\n";
                    case 4076:
                        return this.name + "：味方に攻撃は仕掛けられないぜ。\r\n";
                    case 4077: // 「ためる」コマンド
                        return this.name + "：パワーをためるぜ。\r\n";
                    case 4078: // 武器発動「メイン」
                        return this.name + "：メイン武器の効果を発動させるぜ。\r\n";
                    case 4079: // 武器発動「サブ」
                        return this.name + "：サブ武器の効果を発動させるぜ。\r\n";
                    case 4080: // アクセサリ１発動
                        return this.name + "：アクセサリ１の効果を発動させるぜ。\r\n";
                    case 4081: // アクセサリ２発動
                        return this.name + "：アクセサリ２の効果を発動させるぜ。\r\n";

                    case 4082: // FlashBlaze
                        return this.name + "：フラッシュブレイズいくぜ！\r\n";

                    // 武器攻撃
                    case 5001:
                        return this.name + "：これでも食らいな！エアロ・スラッシュ！ {0} へ {1} のダメージ\r\n";
                    case 5002:
                        return this.name + "：{0} 回復っと！ \r\n";
                    case 5003:
                        return this.name + "：{0}マナ回復っと！\r\n";
                    case 5004:
                        return this.name + "：凍りつけ！アイシクル・スラッシュ！ {0} へ {1} のダメージ\r\n";
                    case 5005:
                        return this.name + "：砕けろ！エレクトロ・ブロー！ {0} へ {1} のダメージ\r\n";
                    case 5006:
                        return this.name + "：これでも食らいな！ブルー・ライトニング！ {0} へ {1} のダメージ\r\n";
                    case 5007:
                        return this.name + "：これでも食らいな！バーニング・クレイモア！ {0} へ {1} のダメージ\r\n";
                    case 5008:
                        return this.name + "：この蒼の炎でも食らうんだな！ {0} へ {1} のダメージ\r\n";
                    case 5009:
                        return this.name + "：{0}スキルポイント回復っと！\r\n";
                    case 5010:
                        return this.name + "が装着している指輪が強烈に蒼い光を放った！ {0} へ {1} のダメージ\r\n";
                }
            }
            #endregion
            #region "ラナ"
            else if (this.name == "ラナ")
            {
                switch (sentenceNumber)
                {
                    case 0: // スキル不足
                        return this.name + "：っちょっと、スキルポイントが足りないじゃない！\r\n";
                    case 1: // Straight Smash
                        return this.name + "：いくわよ。\r\n";
                    case 2: // Double Slash 1
                        return this.name + "：ハイッ！\r\n";
                    case 3: // Double Slash 2
                        return this.name + "：セイッ！\r\n";
                    case 4: // Painful Insanity
                        return this.name + "：【心眼奥義】終わらない痛み、受け続けるが良いわ。\r\n";
                    case 5: // empty skill
                        return this.name + "：っちょっと、スキル選択してないじゃない！\r\n";
                    case 6: // 絡みつくフランシスの必殺を食らった時
                        return this.name + "：痛っ！　辛いわね・・・\r\n";
                    case 7: // Lizenosの必殺を食らった時
                        return this.name + ": 駄目・・・私じゃ全然追えないわ・・・\r\n";
                    case 8: // Minfloreの必殺を食らった時
                        return this.name + "：っよ、避け切れない！ッキャアアァ！！！\r\n";
                    case 9: // Fresh Healによるライフ回復
                        return this.name + "：{0} 回復ね。\r\n";
                    case 10: // Fire Ball
                        return this.name + "：炎の玉、FireBallよ。 {0} へ {1} のダメージ\r\n";
                    case 11: // Flame Strike
                        return this.name + "：炎の槍、FlameStrikeよ。 {0} へ {1} のダメージ\r\n";
                    case 12: // Volcanic Wave
                        return this.name + "：真紅の業火、VolcanicWaveよ。 {0} へ {1} のダメージ\r\n";
                    case 13: // 通常攻撃クリティカルヒット
                        return this.name + "：クリティカルヒットよ！ {0} へ {1} のダメージ\r\n";
                    case 14: // FlameAuraによる追加攻撃
                        return this.name + "：フレイム！ {0}の追加ダメージ\r\n";
                    case 15: // 遠見の青水晶を戦闘中に使ったとき
                        return this.name + "：戦闘中に使っても意味ないのよね。\r\n";
                    case 16: // 効果を発揮しないアイテムを戦闘中に使ったとき
                        return this.name + "：ホント、使えないアイテムね。何も起きないわ。\r\n";
                    case 17: // 魔法でマナ不足
                        return this.name + "：あ、マナ不足だったわ。\r\n";
                    case 18: // Protection
                        return this.name + "：聖なる神による加護、Protectionよ。物理防御力：ＵＰ\r\n";
                    case 19: // Absorb Water
                        return this.name + "：水の女神による加護・・・AbsorbWaterよ。 魔法防御力：ＵＰ。\r\n";
                    case 20: // Saint Power
                        return this.name + "：聖なる神よ我に力を、SaintPower。 物理攻撃力：ＵＰ\r\n";
                    case 21: // Shadow Pact
                        return this.name + "：闇との契約、ShadowPactよ。 魔法攻撃力：ＵＰ\r\n";
                    case 22: // Bloody Vengeance
                        return this.name + "：闇の使者による加護・・・BloodyVengeanceよ。 力パラメタが {0} ＵＰ\r\n";
                    case 23: // Holy Shock
                        return this.name + "：ハンマーでドカンっとね♪ HolyShock！ {0} へ {1} のダメージ\r\n";
                    case 24: // Glory
                        return this.name + "：栄光の光を照らせ、Gloryよ。 直接攻撃＋FreshHeal連携のオーラ\r\n";
                    case 25: // CelestialNova 1
                        return this.name + "：聖なる光で包み込むわ、CelestialNovaよ。 {0} 回復ね。\r\n";
                    case 26: // CelestialNova 2
                        return this.name + "：聖なる裁きの光、CelestialNovaよ。 {0} のダメージ\r\n";
                    case 27: // Dark Blast
                        return this.name + "：黒い波動、耐えられるかしら、DarkBlastよ。 {0} へ {1} のダメージ\r\n";
                    case 28: // Lava Annihilation
                        return this.name + "：これが私の炎授天使様よ、LavaAnnihilation！ {0} へ {1} のダメージ\r\n";
                    case 10028: // Lava Annihilation後編
                        return this.name + "：これが私の炎授天使様よ、LavaAnnihilation！\r\n";
                    case 29: // Devouring Plague
                        return this.name + "：体力を頂くとするわ、DevouringPlagueよ。 {0} ライフを吸い取った\r\n";
                    case 30: // Ice Needle
                        return this.name + "：それっ、氷の針よ♪ IceNeedle。 {0} へ {1} のダメージ\r\n";
                    case 31: // Frozen Lance
                        return this.name + "：凍結された槍、受けてみなさい、FrozenLanceよ。 {0} へ {1} のダメージ\r\n";
                    case 32: // Tidal Wave
                        return this.name + "：大地をも飲み込む壮大なる唸り、TidalWaveよ！ {0} のダメージ\r\n";
                    case 33: // Word of Power
                        return this.name + "：言葉にも力はあるわ、WordOfPowerよ。 {0} へ {1} のダメージ\r\n";
                    case 34: // White Out
                        return this.name + "：五感の全てが消え去る瞬間、WhiteOutよ。 {0} へ {1} のダメージ\r\n";
                    case 35: // Black Contract
                        return this.name + "：悪魔からの確約された力、BlackContractよ。 " + this.name + "のスキル、魔法コストは０になる。\r\n";
                    case 36: // Flame Aura詠唱
                        return this.name + "：エンチャント炎って便利ね、FlameAuraよ。 直接攻撃に炎の追加効果が付与される。\r\n";
                    case 37: // Damnation
                        return this.name + "：闇の深遠、逃れられないわよ、Damnation！ 死淵より出でる黒が空間を歪ませる。\r\n";
                    case 38: // Heat Boost
                        return this.name + "：炎授天使より加護を得る、HeatBoostよ。 技パラメタが {0} ＵＰ。\r\n";
                    case 39: // Immortal Rave
                        return this.name + "：炎術演舞、ImmortalRaveよ！ " + this.name + "の周りに３つの炎が宿った。\r\n";
                    case 40: // Gale Wind
                        return this.name + "：真実の幻影、GaleWindよ。 もう一人の" + this.name + "が現れた。\r\n";
                    case 41: // Word of Life
                        return this.name + "：永久なる自然の恩恵、WordOfLifeよ。 大自然からの強い息吹を感じ取れるようになった。\r\n";
                    case 42: // Word of Fortune
                        return this.name + "：強き理、未来、確定のWordOfFortuneよ。 決死のオーラが湧き上がった。\r\n";
                    case 43: // Aether Drive
                        return this.name + "：創造上の物理使わせてもらうわ、AetherDrive！ 周囲全体に空想物理力がみなぎる。\r\n";
                    case 44: // Eternal Presence 1
                        return this.name + "：新しき創造と原理構築、EternalPresenceよ。 " + this.name + "の周りに新しい法則が構築される。\r\n";
                    case 45: // Eternal Presence 2
                        return this.name + "の物理攻撃、物理防御、魔法攻撃、魔法防御がＵＰした！\r\n";
                    case 46: // One Immunity
                        return this.name + "：空間の壁ってとこかしら♪ OneImmunityよ。 " + this.name + "の周囲に目に見えない障壁が発生。\r\n";
                    case 47: // Time Stop
                        return this.name + "：時空断裂、認識自体を飛ばすわよ、TimeStop！ 敵の時空を引き裂き時間停止させた。\r\n";
                    case 48: // Dispel Magic
                        return this.name + "：永続的効果は無に帰るべきね、DispelMagicよ。 \r\n";
                    case 49: // Rise of Image
                        return this.name + "：時空の支配者より加護を得る、RiseOfImageよ。 心パラメタが {0} ＵＰ\r\n";
                    case 50: // 空詠唱
                        return this.name + "：っちょっと、空詠唱じゃないの！\r\n";
                    case 51: // Inner Inspiration
                        return this.name + "：は潜在集中力を高めた。精神が研ぎ澄まされる。 {0} スキルポイント回復\r\n";
                    case 52: // Resurrection 1
                        return this.name + "：聖の力は奇跡すら起こせるのよ、Resurrection！！\r\n";
                    case 53: // Resurrection 2
                        return this.name + "：っちょっと、対象間違えちゃったじゃない！！\r\n";
                    case 54: // Resurrection 3
                        return this.name + "：あ、生きてた・・・ゴメンね♪\r\n";
                    case 55: // Resurrection 4
                        return this.name + "：自分にやっても効果ないのよね・・・\r\n";
                    case 56: // Stance Of Standing
                        return this.name + "：防御体制を維持したまま・・・攻撃よ！\r\n";
                    case 57: // Mirror Image
                        return this.name + "：純蒼の女神より魔法反射の加護を得る、MirrorImageよ。{0}の周囲に青い空間が発生した。\r\n";
                    case 58: // Mirror Image 2
                        return this.name + "：来たわ、はね返すのよ！ {0} ダメージを {1} に反射した！\r\n";
                    case 59: // Mirror Image 3
                        return this.name + "：来たわ、はね返すのよ！ 【しかし、強力な威力ではね返せない！】" + this.name + "：っう、ウソ！？\r\n";
                    case 60: // Deflection
                        return this.name + "：純白の使者より物理反射の加護を得る、Deflectionよ。 {0}の周囲に白い空間が発生した。\r\n";
                    case 61: // Deflection 2
                        return this.name + "：来たわ、はね返すのよ！ {0} ダメージを {1} に反射した！\r\n";
                    case 62: // Deflection 3
                        return this.name + "：来たわ、はね返すのよ！ 【しかし、強力な威力ではね返せない！】" + this.name + "：キャアアッ！！\r\n";
                    case 63: // Truth Vision
                        return this.name + "：本質さえ見えれば怖くないわ、TruthVisionよ。　" + this.name + "は対象のパラメタＵＰを無視するようになった。\r\n";
                    case 64: // Stance Of Flow
                        return this.name + "：後手必勝、必ず会得してみせるわ、StanceOfFlowよ。　" + this.name + "は次３ターン、必ず後攻を取るように構えた。\r\n";
                    case 65: // Carnage Rush 1
                        return this.name + "：この連続コンボ、耐えられるかしら？CarnageRushよ。 ッハイ！ {0}ダメージ・・・   ";
                    case 66: // Carnage Rush 2
                        return "ッセイ！ {0}ダメージ   ";
                    case 67: // Carnage Rush 3
                        return "ヤァァ！ {0}ダメージ   ";
                    case 68: // Carnage Rush 4
                        return "ッフ！ {0}ダメージ   ";
                    case 69: // Carnage Rush 5
                        return "ハアアァァァ！！ {0}のダメージ\r\n";
                    case 70: // Crushing Blow
                        return this.name + "：この一撃で動きを止めて見せるわ。CrushingBlowよ。  {0} へ {1} のダメージ。\r\n";
                    case 71: // リーベストランクポーション戦闘使用時
                        return this.name + "：ッフフ、これでも食らいなさい。\r\n";
                    case 72: // Enigma Sence
                        return this.name + "：力の源は人によって違うのよ、EnigmaSence！\r\n";
                    case 73: // Soul Infinity
                        return this.name + "：私の能力を全て使って叩き込むわ。ッハアアァァァ・・・SoulInfinity！！\r\n";
                    case 74: // Kinetic Smash
                        return this.name + "：私の拳、最大限の運動性を引き出してみせるわ、KineticSmashよ！\r\n";
                    case 75: // Silence Shot (Altomo専用)
                        return "";
                    case 76: // Silence Shot、AbsoluteZero沈黙による詠唱失敗
                        return this.name + "：・・・ッダメ！詠唱できなかったわ！！\r\n";
                    case 77: // Cleansing
                        return this.name + "：純蒼の女神による浄化、Cleansingよ。\r\n";
                    case 78: // Pure Purification
                        return this.name + "：精神潜在からの浄化、PurePurification・・・\r\n";
                    case 79: // Void Extraction
                        return this.name + "：最大の能力、最大限に引き出すわ、VoidExtractionよ。" + this.name + "の {0} が {1}ＵＰ！\r\n";
                    case 80: // アカシジアの実使用時
                        return this.name + "：っそれ、これで気付けになるわよ。\r\n";
                    case 81: // Absolute Zero
                        return this.name + "：氷の女神より絶対零度を受けなさい。AbsoluteZero！ 氷吹雪効果により、ライフ回復不可、スペル詠唱不可、スキル使用不可、防御不可となった。\r\n";
                    case 82: // BUFFUP効果が望めない場合
                        return "しかし、既にそのパラメタは上昇しているため、効果がなかった。\r\n";
                    case 83: // Promised Knowledge
                        return this.name + "：雪の女神よ、聡明なる知恵を授けたまえ、PromiesdKnowledgeよ。　知パラメタが {0} ＵＰ\r\n";
                    case 84: // Tranquility
                        return this.name + "：その効果さえ、消えてしまうがいいわ！ Tranquilityよ！\r\n";
                    case 85: // High Emotionality 1
                        return this.name + "：私の潜在能力はこんなものじゃないわ、HighEmotionalityよ！\r\n";
                    case 86: // High Emotionality 2
                        return this.name + "の力、技、知、心パラメタがＵＰした！\r\n";
                    case 87: // AbsoluteZeroでスキル使用失敗
                        return this.name + "：全然・・・動けない・・・、スキル使用できないわ。\r\n";
                    case 88: // AbsoluteZeroによる防御失敗
                        return this.name + "：防御の・・・構えが取れないわ！ \r\n";
                    case 89: // Silent Rush 1
                        return this.name + "：動きを最小限に抑えた連続攻撃、SilentRushよ。　ッハイ！ {0}ダメージ・・・　";
                    case 90: // Silent Rush 2
                        return "ッセイ！ {0}ダメージ   ";
                    case 91: // Silent Rush 3
                        return "ヤアァァ！ {0}のダメージ\r\n";
                    case 92: // BUFFUP以外の永続効果が既についている場合
                        return "しかし、既にその効果は付与されている。\r\n";
                    case 93: // Anti Stun
                        return this.name + "：この構えでスタンを防ぐわ、AntiStunよ。 " + this.name + "はスタン効果への耐性が付いた\r\n";
                    case 94: // Anti Stunによるスタン回避
                        return this.name + "：っく、痛いわね。でもスタンは避けたわ。\r\n";
                    case 95: // Stance Of Death
                        return this.name + "：そう簡単にやられないわよ、StanceOfDeath！ " + this.name + "は致死を１度回避できるようになった\r\n";
                    case 96: // Oboro Impact 1
                        return this.name + "：朧の容、【究極奥義】Oboro Impactよ！\r\n";
                    case 97: // Oboro Impact 2
                        return this.name + "：ッハアァァ・・・ッセイ！！　 {0}へ{1}のダメージ\r\n";
                    case 98: // Catastrophe 1
                        return this.name + "：身体の根幹へと伝わる・・・【究極奥義】Catastropheよ！\r\n";
                    case 99: // Catastrophe 2
                        return this.name + "：ッセエエエエェ！！　 {0}のダメージ\r\n";
                    case 100: // Stance Of Eyes
                        return this.name + "：行動には必ずモーションがあるはず、 StanceOfEyesよ。 " + this.name + "は、相手の行動に備えている・・・\r\n";
                    case 101: // Stance Of Eyesによるキャンセル時
                        return this.name + "：っそれね！！　" + this.name + "は相手のモーションを見切って、行動キャンセルした！\r\n";
                    case 102: // Stance Of Eyesによるキャンセル失敗時
                        return this.name + "：っうそ・・・動いた形跡が無いわ・・・　" + this.name + "は相手のモーションを見切れなかった！\r\n";
                    case 103: // Negate
                        return this.name + "：詠唱なんかさせないわ、Negateよ。" + this.name + "は相手のスペル詠唱に備えている・・・\r\n";
                    case 104: // Negateによるスペル詠唱キャンセル時
                        return this.name + "：詠唱するとこ、見つけたわ！" + this.name + "は相手のスペル詠唱を弾いた！\r\n";
                    case 105: // Negateによるスペル詠唱キャンセル失敗時
                        return this.name + "：っうそ・・・詠唱タイミングがわからない・・・" + this.name + "は相手のスペル詠唱を見切れなかった！\r\n";
                    case 106: // Nothing Of Nothingness 1
                        return this.name + "：これが、完全なる否定魔法【究極奥義】NothingOfNothingnessよ！ "+ this.name + "に無色のオーラが宿り始める！ \r\n";
                    case 107: // Nothing Of Nothingness 2
                        return this.name + "は無効化魔法を無効化、　無効化するスキルを無効化するようになった！\r\n";
                    case 108: // Genesis
                        return this.name + "：全ての行動をもう一度呼び起こすわ、Genesisよ。  " + this.name + "は前回の行動を自分へと投影させた！\r\n";
                    case 109: // Cleansing詠唱失敗時
                        return this.name + "：っう・・・駄目、調子が悪くてCleansingは出来そうにないわ。\r\n";
                    case 110: // CounterAttackを無視した時
                        return this.Name + "：甘いわね、その構えは既にお見通しよ。\r\n";
                    case 111: // 神聖水使用時
                        return this.name + "：ライフ・スキル・マナが30%ずつ回復したわよ。\r\n";
                    case 112: // 神聖水、既に使用済みの場合
                        return this.name + "：もう枯れてしまってるわ。一日１回だけのようね。\r\n";
                    case 113: // CounterAttackによる反撃メッセージ
                        return this.name + "：その動きもらったわ、カウンターよ。\r\n";
                    case 114: // CounterAttackに対する反撃がNothingOfNothingnessによって防がれた時
                        return this.name + "：駄目・・・見切れないわ。\r\n";
                    case 115: // 通常攻撃のヒット
                        return this.name + "の攻撃がヒット。 {0} へ {1} のダメージ\r\n";
                    case 116: // 防御を無視して攻撃する時
                        return this.name + "：防御の構えは無駄よ！\r\n";
                    case 117: // StraightSmashなどのスキルクリティカル
                        return this.name + "：クリティカルヒットよ！\r\n";
                    case 118: // 戦闘時、リヴァイヴポーションによる復活のかけ声
                        return this.name + "：このポーションで復活よ。ッハイ！\r\n";
                    case 119: // Absolute Zeroによりライフ回復できない場合
                        return this.name + "：ライフが・・・回復できないわ・・・\r\n";
                    case 120: // 魔法攻撃のヒット
                        return "{0} へ {1} のダメージ\r\n";
                    case 121: // Absolute Zeroによりマナ回復できない場合
                        return this.name + "：マナが・・・回復できないわ・・・\r\n";
                    case 122: // 「ためる」行動時
                        return this.name + "：魔力、蓄えさせてもらったわよ。\r\n";
                    case 123: // 「ためる」行動で溜まりきっている場合
                        return this.name + "：これ以上、魔力は蓄えられないみたいね。\r\n";
                    case 124: // StraightSmashのダメージ値
                        return this.name + "のストレート・スマッシュがヒット。 {0} へ {1}のダメージ\r\n";
                    case 125: // アイテム使用ゲージが溜まってないとき
                        return this.name + "：アイテムゲージが溜まってないわね。アイテムはまだ使えないわよ。\r\n";
                    case 126: // FlashBlase
                        return this.name + "：聖なる炎で焼いてあげるわ、FlashBlazeよ。 {0} へ {1} のダメージ\r\n";
                    case 127: // 複合魔法でインスタント不足
                        return this.name + "：っあ！インスタント値足りないじゃない！\r\n";
                    case 128: // 複合魔法はインスタントタイミングでうてない
                        return this.name + "：インスタントタイミングじゃ発動できないわ。\r\n";
                    case 129: // PsychicTrance
                        return this.name + "：少し怖いけど・・・魔法攻撃力強化よ、PsychicTrance。\r\n";
                    case 130: // BlindJustice
                        return this.name + "：この魔法危険だけど・・・物理攻撃強化よ、BlindJustice。\r\n";
                    case 131: // TranscendentWish
                        return this.name + "：お願い、ケリを付けさせてちょうだい、TranscendentWishよ\r\n";
                    case 132: // LightDetonator
                        return this.name + "：ここね、フィールド展開！ LightDetonatorよ\r\n";
                    case 133: // AscendantMeteor
                        return this.name + "：アッハハハ、死ねば良いわ。　AscendantMeteorよ\r\n";
                    case 134: // SkyShield
                        return this.name + "：空と聖から加護を受けるわ、SkyShieldよ\r\n";
                    case 135: // SacredHeal
                        return this.name + "：全員回復させるわ、SacredHeal\r\n";
                    case 136: // EverDroplet
                        return this.name + "：これでマナ枯渇の心配は不要ね、EverDropletよ\r\n";
                    case 137: // FrozenAura
                        return this.name + "：氷属性を付与するわ、FrozenAura。\r\n";
                    case 138: // ChillBurn
                        return this.name + "：凍結してちょうだい、ChillBurn。\r\n";
                    case 139: // ZetaExplosion
                        return this.name + "：古の禁呪、ZetaExplosionよ！\r\n";
                    case 140: // FrozenAura追加効果ダメージで
                        return this.name + "：凍って！ {0}の追加ダメージ\r\n";
                    case 141: // StarLightning
                        return this.name + "：お願い。気絶して、StarLightningよ！\r\n";
                    case 142: // WordOfMalice
                        return this.name + "：動きを鈍化させてあげるわ、WordOfMalice。\r\n";
                    case 143: // BlackFire
                        return this.name + "：魔法防御を劣化させてあげるわ、BlackFire。\r\n";
                    case 144: // EnrageBlast
                        return this.name + "：打ち上げ花火♪　EnrageBlast♪　\r\n";
                    case 145: // Immolate
                        return this.name + "：物理防御を劣化させてあげるわ、Immolate。\r\n";
                    case 146: // VanishWave
                        return this.name + "：少し黙っててちょうだい、VanishWave！\r\n";
                    case 147: // WordOfAttitude
                        return this.name + "：インスタント回復させるわ、WordOfAttitudeよ。\r\n";
                    case 148: // HolyBreaker
                        return this.name + "：ダメージの引き換えをあげるわ、HolyBreaker！\r\n";
                    case 149: // DarkenField
                        return this.name + "：防御力を全面ダウンさせてあげる、DarkenField！\r\n";
                    case 150: // SeventhMagic
                        return this.name + "：原点の反転を行うわ、SeventhMagic！\r\n";
                    case 151: // BlueBullet
                        return this.name + "：氷魔法を連射するわ、BlueBulletよ。\r\n";
                    case 152: // NeutralSmash
                        return this.name + "：NeutralSmashよ、ハイ！\r\n";
                    case 153: // SwiftStep
                        return this.name + "：戦闘の速度を上げるわよ、SwiftStep。\r\n";
                    case 154: // CircleSlash
                        return this.name + "：みんな邪魔よ、CircleSlash！\r\n";
                    case 155: // RumbleShout
                        return this.name + "：どこ見てんのよ！コッチよ！\r\n";
                    case 156: // SmoothingMove
                        return this.name + "：流れるように攻められるわ、SmoothingMove！\r\n";
                    case 157: // FutureVision
                        return this.name + "：次のターン、何もさせないわよ、FutureVision！\r\n";
                    case 158: // ReflexSpirit
                        return this.name + "：スタン系は絶対回避するわ、ReflexSpiritよ。\r\n";
                    case 159: // SharpGlare
                        return this.name + "：少し黙っててちょうだい、SharpGlareよ。\r\n";
                    case 160: // TrustSilence
                        return this.name + "：沈黙や誘惑とか面倒だしね、TrustSilenceよ。\r\n";
                    case 161: // SurpriseAttack
                        return this.name + "：全員吹っ飛ばしてあげるわ、SurpriseAttackよ！\r\n";
                    case 162: // PsychicWave
                        return this.name + "：この技は止められるかしら、PsychicWaveよ。\r\n";
                    case 163: // Recover
                        return this.name + "：しっかりしてよね、Recoverよ。\r\n";
                    case 164: // ViolentSlash
                        return this.name + "：ッフフ、見きれるかしら、ViolentSlashよ！\r\n";
                    case 165: // OuterInspiration
                        return this.name + "：ステータスを元通りに、OuterInspiration！\r\n";
                    case 166: // StanceOfSuddenness
                        return this.name + "：ッココね！StanceOfSuddenness！\r\n";
                    case 167: // インスタント対象で発動不可
                        return this.name + "：これはインスタント対象専用よね。\r\n";
                    case 168: // StanceOfMystic
                        return this.name + "：当てようとしても、もう無駄よ。StanceOfMystic！\r\n";
                    case 169: // HardestParry
                        return this.name + "：その攻撃、捉えてみせるわ。HardestParryよ。\r\n";
                    case 170: // ConcussiveHit
                        return this.name + "：食らいなさい、ConcussiveHit！\r\n";
                    case 171: // Onslaught hit
                        return this.name + "：食らいなさい、OnslaughtHit！\r\n";
                    case 172: // Impulse hit
                        return this.name + "：食らいなさい、ImpulseHit！\r\n";
                    case 173: // Fatal Blow
                        return this.name + "：これで終わりよ、FatalBlow！\r\n";
                    case 174: // Exalted Field
                        return this.name + "：賛美の声を集うわ、ExaltedField！\r\n";
                    case 175: // Rising Aura
                        return this.name + "：物理攻撃、上げて行くわよ。RisingAura！\r\n";
                    case 176: // Ascension Aura
                        return this.name + "：魔法攻撃、上げて行くわよ。AscensionAura！\r\n";
                    case 177: // Angel Breath
                        return this.name + "：みんな頑張って、元の状態に。　AngelBreath\r\n";
                    case 178: // Blazing Field
                        return this.name + "：この炎で燃やしてあげるわ、BlazingField！\r\n";
                    case 179: // Deep Mirror
                        return this.name + "：それは通さないわよ、DeepMirror！\r\n";
                    case 180: // Abyss Eye
                        return this.name + "：深淵の眼から逃れられないわよ、AbyssEye！\r\n";
                    case 181: // Doom Blade
                        return this.name + "：精神力もろともいただくわ、DoomBlade！\r\n";
                    case 182: // Piercing Flame
                        return this.name + "：貫通の火を打ち込んであげるわ、PiercingFlame！\r\n";
                    case 183: // Phantasmal Wind
                        return this.name + "：反応力を上げるわ、PhantasmalWindよ。\r\n";
                    case 184: // Paradox Image
                        return this.name + "：潜在能力を引き出すわ、ParadoxImageよ。\r\n";
                    case 185: // Vortex Field
                        return this.name + "：これで皆を鈍足にするわね、VortexField。\r\n";
                    case 186: // Static Barrier
                        return this.name + "：水と理から加護を受けるわ、StaticBarrierよ\r\n";
                    case 187: // Unknown Shock
                        return this.name + "：真っ暗な中で戦うといいわ、UnknownShockよ\r\n";
                    case 188: // SoulExecution
                        return this.name + "：行くわよ、奥義　SoulExecusion！\r\n";
                    case 189: // SoulExecution hit 01
                        return this.name + "：ッセィ！\r\n";
                    case 190: // SoulExecution hit 02
                        return this.name + "：ッハ！\r\n";
                    case 191: // SoulExecution hit 03
                        return this.name + "：ッハイ！\r\n";
                    case 192: // SoulExecution hit 04
                        return this.name + "：ッフ！\r\n";
                    case 193: // SoulExecution hit 05
                        return this.name + "：ッセイ！\r\n";
                    case 194: // SoulExecution hit 06
                        return this.name + "：ッハ！\r\n";
                    case 195: // SoulExecution hit 07
                        return this.name + "：ッフ！\r\n";
                    case 196: // SoulExecution hit 08
                        return this.name + "：ッハアァ！\r\n";
                    case 197: // SoulExecution hit 09
                        return this.name + "：セエェェィ！\r\n";
                    case 198: // SoulExecution hit 10
                        return this.name + "：決めるわ！！！\r\n";
                    case 199: // Nourish Sense
                        return this.name + "：回復量をあげていくわ、NourishSenseよ。\r\n";
                    case 200: // Mind Killing
                        return this.name + "：精神を切り刻んであげるわ、MindKilling！\r\n";
                    case 201: // Vigor Sense
                        return this.name + "：反応値上げるわよ、VigorSense！\r\n";
                    case 202: // ONE Authority
                        return this.name + "：みんな、上げていくわよ。OneAuthority！\r\n";
                    case 203: // 集中と断絶
                        return this.name + "：【集中と断絶】　発動。\r\n";
                    case 204: // 【元核】発動済み
                        return this.name + "：【元核】は今日もう使ってしまってるわ。\r\n";
                    case 205: // 【元核】通常行動選択時
                        return this.name + "：【元核】はインスタントタイミングで使用するものよ。\r\n";
                    case 206: // Sigil Of Homura
                        return this.name + "：焔の印を受けなさい、SigilOfHomura！\r\n";
                    case 207: // Austerity Matrix
                        return this.name + "：支配力を切らせてもらうわ、AusterityMatrixよ。\r\n";
                    case 208: // Red Dragon Will
                        return this.name + "：火竜よ、私に力を与えよ、RedDragonWill！\r\n";
                    case 209: // Blue Dragon Will
                        return this.name + "：水竜よ、私に力を与えよ、BlueDragonWill！\r\n";
                    case 210: // Eclipse End
                        return this.name + "：全てを抹消せし無を今ここに、EclipseEnd！\r\n";
                    case 211: // Sin Fortune
                        return this.name + "：次のヒットで決めてみせるわ、SinFortuneよ。\r\n";
                    case 212: // AfterReviveHalf
                        return this.name + "：死耐性（ハーフ）を付与するわね。\r\n";
                    case 213: // Demonic Ignite
                        return this.name + "：黒炎から逃れられないわよ、DemonicIgnite！\r\n";
                    case 214: // Death Deny
                        return this.name + "：死者を完全に復活させるわよ、DeathDeny！\r\n";
                    case 215: // Stance of Double
                        return this.name + "：究極行動原理、StanceOfDouble！  " + this.name + "は前回行動を示す自分の分身を発生させた！\r\n";
                    case 216: // 最終戦ライフカウント消費
                        return this.name + "：うっ・・・まだよ・・・まだ倒れるわけにはいかないわ！\r\n";
                    case 217: // 最終戦ライフカウント消滅時
                        return this.name + "：・・・ご・・・ごめん・・・な・・さ・・・\r\n";
                        
                    case 1001: // Home Town 1 コミュニケーション済で、休む前のアイン一人を対象
                        return this.name + "：今日はもう休んで、明日に備えたら？";
                    case 1002: // Home Town 2 コミュニケーション済で、休んだ後のアイン一人を対象
                        return this.name + "：ホラホラ、とっとと行って来い♪";
                    case 1003: // Home Town 1 コミュニケーション済で、休む前のアイン・ラナ２人を対象
                        return this.name + "：じゃ、私は一旦戻るとするわね。明日に備えて休みましょ。";
                    case 1004: // Home Town 2 コミュニケーション済で、休んだ後のアイン・ラナ２人を対象
                        return this.name + "：準備が出来たら、とっとと行くわよ♪";

                    case 2001: // ポーション回復時
                        return this.name + "：{0} 回復したわよ。";
                    case 2002: // レベルアップ終了催促
                        return this.name + "：レベルアップが先ね。";
                    case 2003: // 荷物を減らせる催促
                        return this.name + "：{0}、荷物を減らしておいたら？アイテムが渡せないわよ。";
                    case 2004: // 装備判断
                        return this.name + "：じゃ、装備しようか？";
                    case 2005: // 装備完了
                        return this.name + "：装備完了♪";
                    case 2006: // 遠見の青水晶を使用
                        return this.name + "：町に戻るとしますか♪";
                    case 2007: // 売却専用品に対する一言
                        return this.name + "：売却専用の品物ね。";
                    case 2008: // 非戦闘時のマナ不足
                        return this.name + "：マナが不足してるわね。";
                    case 2009: // 神聖水使用時
                        return this.name + "：ライフ・スキル・マナが30%ずつ回復したわよ。";
                    case 2010: // 神聖水、既に使用済みの場合
                        return this.name + "：もう枯れてしまってるわ。一日１回だけのようね。";
                    case 2011: // リーベストランクポーション非戦闘使用時
                        return this.name + "：戦闘中専用品のようね。";
                    case 2012: // 遠見の青水晶を使用（町滞在時）
                        return this.name + "：今は町の中にいるから使っても意味ないわよ。";
                    case 2013: // 遠見の青水晶を捨てられない時の台詞
                        return this.name + "：これを捨てたら私達、酷い目に会うわよ。";
                    case 2014: // 非戦闘時のスキル不足
                        return this.name + "：スキルが不足してるわね。";
                    case 2015: // 他プレイヤーが捨ててしまおうとした場合
                        return this.name + "：あ、ちょっと、それは捨てないでよ。";
                    case 2016: // リヴァイヴポーションによる復活
                        return this.name + "：よし、復活できたわ。ホントありがと♪";
                    case 2017: // リヴァイヴポーション不要な時
                        return this.name + "：{0}はまだ死んではいないわ。使用する必要はなさそうね。";
                    case 2018: // リヴァイヴポーション対象が自分の時
                        return this.name + "：私はまだ生きてるわよ。失礼ね。";
                    case 2019: // 装備不可のアイテムを装備しようとした時
                        return this.name + "：私じゃ装備できないみたいね。";
                    case 2020: // ラスボス撃破の後、遠見の青水晶を使用不可
                        return this.name + "：ごめんなさい、今はもうこれは使わないつもりだから。";
                    case 2021: // アイテム捨てるの催促
                        return this.name + "：バックパックの整理が先よね。";
                    case 2022: // オーバーシフティング使用開始時
                        return this.name + "：凄いわ・・・身体能力が再構築されていくのを感じるわ。";
                    case 2023: // オーバーシフティングによるパラメタ割り振り時
                        return this.name + "：オーバーシフティングの割り振りを先にしましょ。";
                    case 2024: // 成長リキッドを使用した時
                        return this.name + "：【{0}】パラメタが {1} 上昇したわ♪";
                    case 2025:
                        return this.name + "：今は両手武器を装備しているわ。サブは装備できないわね。";
                    case 2026:
                        return this.name + "：武器（メイン）に何か装備してからにしましょ。";
                    case 2027: // 清透水使用時
                        return this.name + "：ライフが100%回復したわよ。";
                    case 2028: // 清透水、既に使用済みの場合
                        return this.name + "：もう枯れてしまってるわ。一日１回だけのようね。";
                    case 2029: // 荷物一杯で装備が外せない時
                        return this.name + "：バックパックがいっぱいみたいだわ。装備は外せないわね。";
                    case 2030: // 荷物一杯で何かを捨てる時の台詞
                        return this.name + "：{0}を捨てるわね？";
                    case 2031: // 戦闘中のアイテム使用限定
                        return this.name + "：今は戦闘中よ。他のパラメタなんか探ってる余裕は無いわ。";
                    case 2032: // 戦闘中、アイテム使用できないアイテムを選択したとき
                        return this.name + "：このアイテムは戦闘中に使用は出来ないみたいね。";
                    case 2033: // 預けられない場合
                        return this.name + "：う～ん、ちょっとこれは手元から外せないわね。";
                    case 2034: // アイテムいっぱいで預かり所から引き出せない場合
                        return this.name + "：う～ん、荷物はもういっぱいみたいね。";
                    case 2035: // Sacred Heal
                        return this.name + "：うん、全員回復したわよ。";

                    case 3000: // 店に入った時の台詞
                        return this.name + "：ホンット誰もいないわね。";
                    case 3001: // 支払い要求時
                        return this.name + "：私、この{0}が欲しいわ。 {1}Gold置いてくわよ？";
                    case 3002: // 持ち物いっぱいで買えない時
                        return this.name + "：持ち物がいっぱいのようね。少しアイテム整理するわ。";
                    case 3003: // 購入完了時
                        return this.name + "：これで売買成立してるわよね？ガンツ叔父さん。";
                    case 3004: // Gold不足で購入できない場合
                        return this.name + "：Goldが{0}足りないわね・・・";
                    case 3005: // 購入せずキャンセルした場合
                        return this.name + "：他のアイテムも見て回りましょ。";
                    case 3006: // 売れないアイテムを売ろうとした場合
                        return this.name + "：このアイテムは手放せないわね。";
                    case 3007: // アイテム売却時
                        return this.name + "：{0}を置いてくわよ、{1}Goldもらって良いはずよね？";
                    case 3008: // 剣紋章ペンダント売却時
                        return this.name + "：ちょっとコレせっかく私が作ったものよ。まあ、{0}Goldぐらいはもらえそうだけど？";
                    case 3009: // 武具店を出る時
                        return this.name + "：ガンツ叔父さん、時空のルーツ見つかるといいわね。";
                    case 3010: // ガンツ不在時の売りきれフェルトゥーシュを見て一言
                        return this.name + "：この剣は元々売り切れって話じゃないわよね・・・";
                    case 3011: // 装備可能なものが購入された時
                        return this.name + "：ここで装備していくわよ。良いわよね？";
                    case 3012: // 装備していた物を売却対象かどうか聞く時
                        return this.name + "：っとと、今装備してる{0}も売っておきたいわね。{1}Goldぐらいよね、ガンツ叔父さん？";

                    case 3013: // 店の担当者としてお迎えの挨拶
                        return this.name + "：どうぞお買い求めください♪";
                    case 3014: // 店の担当者としてお別れの挨拶
                        return this.name + "：また、おいでくださいませ♪";
                    case 3015: // 店の担当者としてお買い上げをヒアリングするとき
                        return this.name + "：{0}ですね。{1}Goldですが、お買い上げになりますか？";
                    case 3016: // 店の担当者として、Goldが不足してるときの台詞
                        return this.name + "：すいませんが、後{0}Goldだけ不足しております。";
                    case 3017: // 店の担当者として、買い手が購入決定・完了したとき
                        return this.name + "：ありがとうございました♪";
                    case 3018: // 買い手の持ち物がいっぱいである事をお伝えするとき
                        return this.name + "：あの、すいませんが荷物がいっぱいのようです。";
                    case 3019: // 買い手が購入せずキャンセルされた場合
                        return this.name + "：他にも良ければ、見て行ってくださいませ♪";
                    case 3020: // 買い手が売却不可能なものを提示してきた場合
                        return this.name + "：すいませんが、その品物は買取りができません。";
                    case 3021: // 買い手が売却可能なものを提示してきた場合
                        return this.name + "：{0}ですね。{1}Goldでの買取らせていただきましょうか？";

                    case 4001: // 通常攻撃を選択
                        return this.name + "：普通に攻撃ね。\r\n";
                    case 4002: // 防御を選択
                        return this.name + "：危ないときは防御かな。\r\n";
                    case 4003: // 待機を選択
                        return this.name + "：待機で次に備えるわよ。\r\n";
                    case 4004: // フレッシュヒールを選択
                        return this.name + "：フレッシュヒールで行こうかしら。\r\n";
                    case 4005: // プロテクションを選択
                        return this.name + "：防御力ＵＰ、プロテクションよ。\r\n";
                    case 4006: // ファイア・ボールを選択
                        return this.name + "：ファイアボールを撃ち込むわよ。\r\n";
                    case 4007: // フレイム・オーラを選択
                        return this.name + "：フレイム属性攻撃の準備ね。\r\n";
                    case 4008: // ストレート・スマッシュを選択
                        return this.name + "：次、ストレートスマッシュ行くわよ。\r\n";
                    case 4009: // ダブル・スマッシュを選択
                        return this.name + "：２回攻撃、ダブルスマッシュ行くわよ。\r\n";
                    case 4010: // スタンス・オブ・スタンディングを選択
                        return this.name + "：スタンディングの構え。守って攻めるわよ。\r\n";
                    case 4011: // アイス・ニードルを選択
                        return this.name + "：アイスニードルを撃ち込むわよ。\r\n";
                    case 4012:
                    case 4013:
                    case 4014:
                    case 4015:
                    case 4016:
                    case 4017:
                    case 4018:
                    case 4019:
                    case 4020:
                    case 4021:
                    case 4022:
                    case 4023:
                    case 4024:
                    case 4025:
                    case 4026:
                    case 4027:
                    case 4028:
                    case 4029:
                    case 4030:
                    case 4031:
                    case 4032:
                    case 4033:
                    case 4034:
                    case 4035:
                    case 4036:
                    case 4037:
                    case 4038:
                    case 4039:
                    case 4040:
                    case 4041:
                    case 4042:
                    case 4043:
                    case 4044:
                    case 4045:
                    case 4046:
                    case 4047:
                    case 4048:
                    case 4049:
                    case 4050:
                    case 4051:
                    case 4052:
                    case 4053:
                    case 4054:
                    case 4055:
                    case 4056:
                    case 4057:
                    case 4058:
                    case 4059:
                    case 4060:
                    case 4061:
                    case 4062:
                    case 4063:
                    case 4064:
                    case 4065:
                    case 4066:
                    case 4067:
                    case 4068:
                    case 4069:
                    case 4070:
                    case 4071:
                        return this.name + "：" + this.ActionLabel.Text + "にするわね。\r\n";
                    case 4072:
                        return this.name + "：敵に放ちたくないわね。\r\n";
                    case 4073:
                        return this.name + "：敵に放ちたくないわね。\r\n";
                    case 4074:
                        return this.name + "：味方に放つわけには行かないわ。\r\n";
                    case 4075:
                        return this.name + "：味方に放つわけには行かないわ。\r\n";
                    case 4076:
                        return this.name + "：味方に攻撃するわけには行かないわ。\r\n";
                    case 4077: // 「ためる」コマンド
                        return this.name + "：魔力をためるわ。\r\n";
                    case 4078: // 武器発動「メイン」
                        return this.name + "：メイン武器の効果を発動させるわね。\r\n";
                    case 4079: // 武器発動「サブ」
                        return this.name + "：サブ武器の効果を発動させるわね。\r\n";
                    case 4080: // アクセサリ１発動
                        return this.name + "：アクセサリ１の効果を発動させるわね。\r\n";
                    case 4081: // アクセサリ２発動
                        return this.name + "：アクセサリ２の効果を発動させるわね。\r\n";

                    case 4082: // FlashBlaze
                        return this.name + "：フラッシュブレイズで行こうかしら。\r\n";

                    // 武器攻撃
                    case 5001:
                        return this.name + "：風で切り裂くわよ、エアロ・スラッシュ！ {0} へ {1} のダメージ\r\n";
                    case 5002:
                        return this.name + "：{0} 回復よ \r\n";
                    case 5003:
                        return this.name + "：{0} マナ回復よ \r\n";
                    case 5004:
                        return this.name + "：凍りつくが良いわ！アイシクル・スラッシュ！ {0} へ {1} のダメージ\r\n";
                    case 5005:
                        return this.name + "：行くわよ！エレクトロ・ブロー！ {0} へ {1} のダメージ\r\n";
                    case 5006:
                        return this.name + "：いくわよ！ブルー・ライトニング！ {0} へ {1} のダメージ\r\n";
                    case 5007:
                        return this.name + "：いくわよ！バーニング・クレイモア！ {0} へ {1} のダメージ\r\n";
                    case 5008:
                        return this.name + "：赤蒼授からの炎、食らうわがいいわ！ {0} へ {1} のダメージ\r\n";
                    case 5009:
                        return this.name + "：{0} スキルポイント回復よ \r\n";
                    case 5010:
                        return this.name + "が装着している指輪が強烈に蒼い光を放った！ {0} へ {1} のダメージ\r\n";
                }
            }
            #endregion
            #region "ヴェルゼ"
            else if (this.name == "ヴェルゼ" || this.name == "ヴェルゼ・アーティ")
            {
                switch (sentenceNumber)
                {
                    case 0: // スキル不足
                        return this.name + "：スキルポイントが足りませんね。\r\n";
                    case 1: // Straight Smash
                        return this.name + "：ッハアアァァァァァ！！！\r\n";
                    case 2: // Double Slash 1 Carnage Rush 1
                        return this.name + "：ひとつ\r\n";
                    case 3: // Double Slash 2 Carnage Rush 2
                        return this.name + "：ふたつ\r\n";
                    case 4: // Painful Insanity
                        return this.name + "：【心眼奥義】あなたには分からないでしょう、無限の苦しみ。\r\n";
                    case 5: // empty skill
                        return this.name + "：スキル選択ミスですね。\r\n";
                    case 6: // 絡みつくフランシスの必殺を食らった時
                        return this.name + "：さて、これは・・・なかなか。\r\n";
                    case 7: // Lizenosの必殺を食らった時
                        return this.name + ": このボクでさえ・・・まったく見えません・・・\r\n";
                    case 8: // Minfloreの必殺を食らった時
                        return this.name + "：こ・・・この剣、強すぎ・・る・・・\r\n";
                    case 9: // Fresh Healによるライフ回復
                        return this.name + "：{0} 回復です。\r\n";
                    case 10: // Fire Ball
                        return this.name + "：軽くどうでしょう、FireBallです。 {0} へ {1} のダメージ\r\n";
                    case 11: // Flame Strike
                        return this.name + "：こんな赤いものはいかがでしょう、FlameStrikeです。 {0} へ {1} のダメージ\r\n";
                    case 12: // Volcanic Wave
                        return this.name + "：業火ぐらい大した事ないでしょう？ VolcanicWaveです。 {0} へ {1} のダメージ\r\n";
                    case 13: // 通常攻撃クリティカルヒット
                        return this.name + "：クリティカルヒットです。ッハアアァァァ！！ {0} へ {1}のダメージ\r\n";
                    case 14: // FlameAuraによる追加攻撃
                        return this.name + "：火の粉です。 {0}の追加ダメージ\r\n";
                    case 15: // 遠見の青水晶を戦闘中に使ったとき
                        return this.name + "：これは戦闘中では使えませんね。\r\n";
                    case 16: // 効果を発揮しないアイテムを戦闘中に使ったとき
                        return this.name + "：ッチ、役立たずアイテムが。　こんなもの使えねえ！！\r\n";
                    case 17: // 魔法でマナ不足
                        return this.name + "：マナが不足していますね。\r\n";
                    case 18: // Protection
                        return this.name + "：聖の防御円、Protectionです。物理防御力：ＵＰ\r\n";
                    case 19: // Absorb Water
                        return this.name + "：水の防御円、AbsorbWaterです。 魔法防御力：ＵＰ。\r\n";
                    case 20: // Saint Power
                        return this.name + "：聖の攻撃円、SaintPowerです。 物理攻撃力：ＵＰ\r\n";
                    case 21: // Shadow Pact
                        return this.name + "：ククッ、闇からの力、ShadowPactです。 魔法攻撃力：ＵＰ\r\n";
                    case 22: // Bloody Vengeance
                        return this.name + "：闇の使者がボクに力をくれる。BloodyVengeanceです。 力パラメタが {0} ＵＰ\r\n";
                    case 23: // Holy Shock
                        return this.name + "：鉄槌なんていかがですか、HolyShockです。 {0} へ {1} のダメージ\r\n";
                    case 24: // Glory
                        return this.name + "：誰でも光輝く時代があったんですよ。Gloryです。 直接攻撃＋FreshHeal連携のオーラ\r\n";
                    case 25: // CelestialNova 1
                        return this.name + "：光による癒し、CelestialNovaです。 {0} 回復です。\r\n";
                    case 26: // CelestialNova 2
                        return this.name + "：ッハッハハハ！裁き食らえやあぁぁぁ！　CelestialNova！！ {0} のダメージ\r\n";
                    case 27: // Dark Blast
                        return this.name + "：ククク、闇の波動こそ全て、DarkBlastです。 {0} へ {1} のダメージ\r\n";
                    case 28: // Lava Annihilation
                        return this.name + "：炎授天使ぐらい造作も無い、LavaAnnihilationです。 {0} へ {1} のダメージ\r\n";
                    case 10028: // Lava Annihilation後編
                        return this.name + "：炎授天使ぐらい造作も無い、LavaAnnihilationです。\r\n";
                    case 29: // Devouring Plague
                        return this.name + "：体力吸収だ、死ね死ね死ねぇ！DevouringPlague！ {0} ライフを吸い取った\r\n";
                    case 30: // Ice Needle
                        return this.name + "：無数の氷でも食らってください、IceNeedleです。 {0} へ {1} のダメージ\r\n";
                    case 31: // Frozen Lance
                        return this.name + "：どうです、氷の槍で串刺しってのは？　FrozenLanceです。 {0} へ {1} のダメージ\r\n";
                    case 32: // Tidal Wave
                        return this.name + "：水に呑まれて死んじまえよおおぉぉ！！！TidalWave！ {0} のダメージ\r\n";
                    case 33: // Word of Power
                        return this.name + "：あなたこの魔法止めれませんよ、WordOfPowerです。 {0} へ {1} のダメージ\r\n";
                    case 34: // White Out
                        return this.name + "：五・・・五感があああぁぁぁぁぁ！　WhiteOut！！！ {0} へ {1} のダメージ\r\n";
                    case 35: // Black Contract
                        return this.name + "：悪魔、なんて美しい強さだ・・・、BlackContractです。 " + this.name + "のスキル、魔法コストは０になる。\r\n";
                    case 36: // Flame Aura詠唱
                        return this.name + "：炎をで丸焦げにしてやるぜええぇぇ！！FlameAura！！！ 直接攻撃に炎の追加効果が付与される。\r\n";
                    case 37: // Damnation
                        return this.name + "：闇闇闇闇闇闇闇闇闇・・・絶望・・・Damnation。 死淵より出でる黒が空間を歪ませる。\r\n";
                    case 38: // Heat Boost
                        return this.name + "：炎授天使の加速円、HeatBoostです。 技パラメタが {0} ＵＰ。\r\n";
                    case 39: // Immortal Rave
                        return this.name + "：炎術の舞なんて、簡単すぎる・・・ImmortalRaveです。 " + this.name + "の周りに３つの炎が宿った。\r\n";
                    case 40: // Gale Wind
                        return this.name + "：全盛期ボクは常にこの状態だった、GaleWindです。 もう一人の" + this.name + "が現れた。\r\n";
                    case 41: // Word of Life
                        return this.name + "：ックク、自然の力いただくぜええぇ！WordOfLife！！！ 大自然からの強い息吹を感じ取れるようになった。\r\n";
                    case 42: // Word of Fortune
                        return this.name + "：全盛期ボクはこんな魔法不要でした、WordOfFortuneです。 決死のオーラが湧き上がった。\r\n";
                    case 43: // Aether Drive
                        return this.name + "：空想物理なんて大したものじゃありませんよ？AetherDriveです。 周囲全体に空想物理力がみなぎる。\r\n";
                    case 44: // Eternal Presence 1
                        return this.name + "：創造と原理なんて大げさ過ぎる、普通ですよ。EternalPresence。 " + this.name + "の周りに新しい法則が構築される。\r\n";
                    case 45: // Eternal Presence 2
                        return this.name + "の物理攻撃、物理防御、魔法攻撃、魔法防御がＵＰした！\r\n";
                    case 46: // One Immunity
                        return this.name + "：２回行動できたとしたら最強だと思いません？ OneImmunityです。 " + this.name + "の周囲に目に見えない障壁が発生。\r\n";
                    case 47: // Time Stop
                        return this.name + "：アッハハハハハ！時空の歪に消えちまいなぁ！TimeStop！！！ 敵の時空を引き裂き時間停止させた。\r\n";
                    case 48: // Dispel Magic
                        return this.name + "：元々はカラッポのくせに、ッククク。DispelMagicです。\r\n";
                    case 49: // Rise of Image
                        return this.name + "：時空の支配者の上昇円、RiseOfImageです。 心パラメタが {0} ＵＰ\r\n";
                    case 50: // 空詠唱
                        return this.name + "：空詠唱ですね。\r\n";
                    case 51: // Inner Inspiration
                        return this.name + "：は潜在集中力を高めた。精神が研ぎ澄まされる。 {0} スキルポイント回復\r\n";
                    case 52: // Resurrection 1
                        return this.name + "：奇跡？このぐらいの聖スペル普通でしょう、Resurrectionです。\r\n";
                    case 53: // Resurrection 2
                        return this.name + "：てめえなんか対象にするわけねえだろおおぉ！！\r\n";
                    case 54: // Resurrection 3
                        return this.name + "：生きてましたね。すいませんでした。\r\n";
                    case 55: // Resurrection 4
                        return this.name + "：ボク自身は生きてるのか？というブラックジョークです、意味はありません。\r\n";
                    case 56: // Stance Of Standing
                        return this.name + "：防御兼攻撃とは、こうやるんですよ。\r\n";
                    case 57: // Mirror Image
                        return this.name + "：女神より蒼の魔法反射円、MirrorImageです。{0}の周囲に青い空間が発生した。\r\n";
                    case 58: // Mirror Image 2
                        return this.name + "：ックク、掛かったな！ {0} ダメージを {1} に反射した！\r\n";
                    case 59: // Mirror Image 3
                        return this.name + "：ックク、掛かったな！ 【しかし、強力な威力ではね返せない！】" + this.name + "：し、しまったボクとした事がぁぁ！\r\n";
                    case 60: // Deflection
                        return this.name + "：白の者より物理反射円、Deflectionです。 {0}の周囲に白い空間が発生した。\r\n";
                    case 61: // Deflection 2
                        return this.name + "：ックク、掛かったな！ {0} ダメージを {1} に反射した！\r\n";
                    case 62: // Deflection 3
                        return this.name + "：ックク、掛かったな！ 【しかし、強力な威力ではね返せない！】" + this.name + "：しまった、ボクとしたことがあぁ！\r\n";
                    case 63: // Truth Vision
                        return this.name + "：本質？あるわけないでしょう、TruthVisionです。　" + this.name + "は対象のパラメタＵＰを無視するようになった。\r\n";
                    case 64: // Stance Of Flow
                        return this.name + "：相手の動き、封殺してみせましょう。StanceOfFlowです。　" + this.name + "は次３ターン、必ず後攻を取るように構えた。\r\n";
                    case 65: // Carnage Rush 1
                        return this.name + "：技があれば容易いコンボでしょう、CarnageRushです。 ひとつ {0}ダメージ・・・   ";
                    case 66: // Carnage Rush 2
                        return "ふたつ {0}ダメージ   ";
                    case 67: // Carnage Rush 3
                        return "みっつ {0}ダメージ   ";
                    case 68: // Carnage Rush 4
                        return "よっつ {0}ダメージ   ";
                    case 69: // Carnage Rush 5
                        return "そして最後です。ハアアァァァ！！ {0}のダメージ\r\n";
                    case 70: // Crushing Blow
                        return this.name + "：少し寝ててください。CrushingBlowです。  {0} へ {1} のダメージ。\r\n";
                    case 71: // リーベストランクポーション戦闘使用時
                        return this.name + "：こんなアイテムがあるんですよ、いかがでしょう。\r\n";
                    case 72: // Enigma Sence
                        return this.name + "：力は見せ方次第だと思いませんか？EnigmaSennceです\r\n";
                    case 73: // Soul Infinity
                        return this.name + "：これがボクの全ての能力を注ぎ込んだパワーです。SoulInfinity！\r\n";
                    case 74: // Kinetic Smash
                        return this.name + "：物理運動に沿った最大限の攻撃とはこうですかね。KineticSmashです。\r\n";
                    case 75: // Silence Shot (Altomo専用)
                        return "";
                    case 76: // Silence Shot、AbsoluteZero沈黙による詠唱失敗
                        return this.name + "：・・・っこのタイミングで・・・！？　詠唱ミスです。\r\n";
                    case 77: // Cleansing
                        return this.name + "：浄化というより、元々何も無いんですよ。Cleansingです。\r\n";
                    case 78: // Pure Purification
                        return this.name + "：精神の洗い直しから治せる事もあるんです、PurePurification・・・\r\n";
                    case 79: // Void Extraction
                        return this.name + "：ボクに限界なんて無いんですよ、VoidExtractionです。" + this.name + "の {0} が {1}ＵＰ！\r\n";
                    case 80: // アカシジアの実使用時
                        return this.name + "：アカシジアの実です。良い気付けになるでしょう。\r\n";
                    case 81: // Absolute Zero
                        return this.name + "：絶対零度で凍り付けえええぇぇぇぇ。AbsoluteZero！ 氷吹雪効果により、ライフ回復不可、スペル詠唱不可、スキル使用不可、防御不可となった。\r\n";
                    case 82: // BUFFUP効果が望めない場合
                        return "しかし、既にそのパラメタは上昇しているため、効果がなかった。\r\n";
                    case 83: // Promised Knowledge
                        return this.name + "：知恵と知識の組み合わせ最強だと思いません？ PromiesdKnowledgeです。　知パラメタが {0} ＵＰ\r\n";
                    case 84: // Tranquility
                        return this.name + "：平穏と無、どちらも同じなんでしょうね、Tranquilityです。\r\n";
                    case 85: // High Emotionality 1
                        return this.name + "：ッハアアアアアアアァァァァ！！！　HighEmotionality！\r\n";
                    case 86: // High Emotionality 2
                        return this.name + "の力、技、知、心パラメタがＵＰした！\r\n";
                    case 87: // AbsoluteZeroでスキル使用失敗
                        return this.name + "：・・・っく、思うように動けません・・・、スキル使用ミスです。\r\n";
                    case 88: // AbsoluteZeroによる防御失敗
                        return this.name + "：っく・・・防御が・・・思うようにできません！ \r\n";
                    case 89: // Silent Rush 1
                        return this.name + "：貴方は姿すら捉えられないでしょうね、SilentRushです。　一つ {0}ダメージ・・・　";
                    case 90: // Silent Rush 2
                        return "ふたつ {0}ダメージ   ";
                    case 91: // Silent Rush 3
                        return "そして、みっつめです。ハアァァァ！ {0}のダメージ\r\n";
                    case 92: // BUFFUP以外の永続効果が既についている場合
                        return "しかし、既にその効果は付与されている。\r\n";
                    case 93: // Anti Stun
                        return this.name + "：スタン効果はボクには効きませんよ。AntiStunです。 " + this.name + "はスタン効果への耐性が付いた\r\n";
                    case 94: // Anti Stunによるスタン回避
                        return this.name + "：言ったはずです。ボクにスタンは効かない。\r\n";
                    case 95: // Stance Of Death
                        return this.name + "：死・・・破滅の言葉ですよ、ッククク、StanceOfDeath！ " + this.name + "は致死を１度回避できるようになった\r\n";
                    case 96: // Oboro Impact 1
                        return this.name + "：見えますか？この容が、【究極奥義】Oboro Impactです。\r\n";
                    case 97: // Oboro Impact 2
                        return this.name + "：ッハアアァァ！！　 {0}へ{1}のダメージ\r\n";
                    case 98: // Catastrophe 1
                        return this.name + "：全てを破壊・・・破壊してあげましょう【究極奥義】Catastropheです\r\n";
                    case 99: // Catastrophe 2
                        return this.name + "：ッハアアァァァアアア！！　 {0}のダメージ\r\n";
                    case 100: // Stance Of Eyes
                        return this.name + "：無駄です。全て見切ってさしあげましょう、 StanceOfEyesです。 " + this.name + "は、相手の行動に備えている・・・\r\n";
                    case 101: // Stance Of Eyesによるキャンセル時
                        return this.name + "：ソコのモーションですね、　" + this.name + "は相手のモーションを見切って、行動キャンセルした！\r\n";
                    case 102: // Stance Of Eyesによるキャンセル失敗時
                        return this.name + "：っばかな！？動きがまったく読めない！　" + this.name + "は相手のモーションを見切れなかった！\r\n";
                    case 103: // Negate
                        return this.name + "：詠唱とは必ず隙があります。Negateです。" + this.name + "は相手のスペル詠唱に備えている・・・\r\n";
                    case 104: // Negateによるスペル詠唱キャンセル時
                        return this.name + "：ッハハハ！詠唱動作が見え見えですよ！" + this.name + "は相手のスペル詠唱を弾いた！\r\n";
                    case 105: // Negateによるスペル詠唱キャンセル失敗時
                        return this.name + "：っばかな！・・・何時の間に詠唱を！？" + this.name + "は相手のスペル詠唱を見切れなかった！\r\n";
                    case 106: // Nothing Of Nothingness 1
                        return this.name + "：ッハハハハハ！！真なるゼロ【究極奥義】NothingOfNothingness！ " + this.name + "に無色のオーラが宿り始める！ \r\n";
                    case 107: // Nothing Of Nothingness 2
                        return this.name + "は無効化魔法を無効化、　無効化するスキルを無効化するようになった！\r\n";
                    case 108: // Genesis
                        return this.name + "：これが超然たる起源、Genesisです。  " + this.name + "は前回の行動を自分へと投影させた！\r\n";
                    case 109: // Cleansing詠唱失敗時
                        return this.name + "：っす、すいません・・・調子が悪くてCleansingが発動できません。\r\n";
                    case 110: // CounterAttackを無視した時
                        return this.Name + "：ッハハハハ！そんな構え、見え見えですよ！\r\n";
                    case 111: // 神聖水使用時
                        return this.name + "：ライフ・スキル・マナが30%ずつ回復です。\r\n";
                    case 112: // 神聖水、既に使用済みの場合
                        return this.name + "：もう水は残ってないようです。一日待たないと駄目ですね。\r\n";
                    case 113: // CounterAttackによる反撃メッセージ
                        return this.name + "：甘いですね、カウンターです。\r\n";
                    case 114: // CounterAttackに対する反撃がNothingOfNothingnessによって防がれた時
                        return this.name + "：ッバ、バカな！このボクが見切れないなんて！\r\n";
                    case 115: // 通常攻撃のヒット
                        return this.name + "の攻撃がヒット。 {0} へ {1} のダメージ\r\n";
                    case 116: // 防御を無視して攻撃する時
                        return this.name + "：防御出来る・・・とでも思いましたか？\r\n";
                    case 117: // StraightSmashなどのスキルクリティカル
                        return this.name + "：クリティカルヒットです。\r\n";
                    case 118: // 戦闘時、リヴァイヴポーションによる復活のかけ声
                        return this.name + "：リヴァイヴポーションです。これで復活ですね。\r\n";
                    case 119: // Absolute Zeroによりライフ回復できない場合
                        return this.name + "：この凍てつく寒さ・・・ライフ回復できません。\r\n";
                    case 120: // 魔法攻撃のヒット
                        return "{0} へ {1} のダメージ\r\n";
                    case 121: // Absolute Zeroによりマナ回復できない場合
                        return this.name + "：この凍てつく寒さ・・・マナ回復できません。\r\n";
                    case 122: // 「ためる」行動時
                        return this.name + "：ッハハハ、こんな簡単に魔力を蓄えさせてもらえるとは。\r\n";
                    case 123: // 「ためる」行動で溜まりきっている場合
                        return this.name + "：魔力は十分蓄えてあります。もう十分でしょう。\r\n";
                    case 124: // StraightSmashのダメージ値
                        return this.name + "のストレート・スマッシュがヒット。 {0} へ {1}のダメージ\r\n";
                    case 125: // アイテム使用ゲージが溜まってないとき
                        return this.name + "：アイテムゲージが溜まってない間、アイテムは使えませんね。\r\n";
                    case 126: // FlashBlase
                        return this.name + "：これは痛いでしょうね、FlashBlazeです。 {0} へ {1} のダメージ\r\n";
                    case 127: // 複合魔法でインスタント不足
                        return this.name + "：ボクとしたことが・・・インスタント値が足りません\r\n";
                    case 128: // 複合魔法はインスタントタイミングでうてない
                        return this.name + "：インスタントのタイミングで発動したい所ですが。\r\n";
                    case 129: // PsychicTrance
                        return this.name + "：ッハハハ、更に魔法攻撃強化、PsychicTrance。\r\n";
                    case 130: // BlindJustice
                        return this.name + "：ッククク、更に物理攻撃強化、BlindJustice。\r\n";
                    case 131: // TranscendentWish
                        return this.name + "：死など恐れませんよ、TranscendentWish。\r\n";
                    case 132: // LightDetonator
                        return this.name + "：そこに行くと分かってました、LightDetonator。\r\n";
                    case 133: // AscendantMeteor
                        return this.name + "焼け焦げるが良い、AscendantMeteor\r\n";
                    case 134: // SkyShield
                        return this.name + "：空聖の加護、SkyShield\r\n";
                    case 135: // SacredHeal
                        return this.name + "：全員回復です、SacredHeal\r\n";
                    case 136: // EverDroplet
                        return this.name + "：ッククク、これを許すとは愚かな、EverDroplet\r\n";
                    case 137: // FrozenAura
                        return this.name + "：氷属性を付与、FrozenAuraです。\r\n";
                    case 138: // ChillBurn
                        return this.name + "：ChillBurnで凍結してください。\r\n";
                    case 139: // ZetaExplosion
                        return this.name + "：よみがえれ、Zeta！　ッハハ・・・ッハハハハ！！！\r\n";
                    case 140: // FrozenAura追加効果ダメージで
                        return this.name + "：凍ってください。 {0}の追加ダメージ\r\n";
                    case 141: // StarLightning
                        return this.name + "：ほんの一瞬です、StarLightning。\r\n";
                    case 142: // WordOfMalice
                        return this.name + "：ハハハ、さらに遅くなりますよ、WordOfMalice。\r\n";
                    case 143: // BlackFire
                        return this.name + "：魔法防御低下、BlackFire。\r\n";
                    case 144: // EnrageBlast
                        return this.name + "：さて、焼かれてもらいましょう、EnrageBlast。\r\n";
                    case 145: // Immolate
                        return this.name + "：物理防御低下、Immolate。\r\n";
                    case 146: // VanishWave
                        return this.name + "：黙っていてくれませんか、VanishWave。\r\n";
                    case 147: // WordOfAttitude
                        return this.name + "：卑怯と呼んでもらって構いませんよ、WordOfAttitude。\r\n";
                    case 148: // HolyBreaker
                        return this.name + "：ダメージの差を付けましょう、HolyBreaker。\r\n";
                    case 149: // DarkenField
                        return this.name + "：防御力全面低下、DarkenField。\r\n";
                    case 150: // SeventhMagic
                        return this.name + "：原則を覆すとしましょう、SeventhMagic。\r\n";
                    case 151: // BlueBullet
                        return this.name + "：氷の飛礫、BlueBulletです。\r\n";
                    case 152: // NeutralSmash
                        return this.name + "：NeutralSmash、ッハァ！\r\n";
                    case 153: // SwiftStep
                        return this.name + "：速度上げさせてもらいます、SwiftStep。\r\n";
                    case 154: // CircleSlash
                        return this.name + "：邪魔ですねどいてください、CircleSlash。\r\n";
                    case 155: // RumbleShout
                        return this.name + "：どこを見てるんですか？コチラです。\r\n";
                    case 156: // SmoothingMove
                        return this.name + "：ッククク、ほぼ無限コンボです、SmoothingMove。\r\n";
                    case 157: // FutureVision
                        return this.name + "：ボクが見きれないハズがない、FutureVision。\r\n";
                    case 158: // ReflexSpirit
                        return this.name + "：スタン系は事前回避に限ります、ReflexSpirit。\r\n";
                    case 159: // SharpGlare
                        return this.name + "：黙っていてくれませんか、SharpGlare。\r\n";
                    case 160: // TrustSilence
                        return this.name + "：沈黙などボクには無縁ですね、TrustSilence。\r\n";
                    case 161: // SurpriseAttack
                        return this.name + "ッハハハハ、これで気絶しちまいなぁ！SurpriseAttack！！\r\n";
                    case 162: // PsychicWave
                        return this.name + "：ッハハハ、止められないでしょう、PsychicWave。\r\n";
                    case 163: // Recover
                        return this.name + "：しっかりしてください、Recoverです。\r\n";
                    case 164: // ViolentSlash
                        return this.name + "：トドメェェェ！！　ViolentSlash！！！\r\n";
                    case 165: // OuterInspiration
                        return this.name + "：さて、これで元通りですね、OuterInspiration。\r\n";
                    case 166: // StanceOfSuddenness
                        return this.name + "：ッハハハハ！StanceOfSuddenness！ッハッハハハハ！！\r\n";
                    case 167: // インスタント対象で発動不可
                        return this.name + "：対象のインスタントコマンドが無いですね。\r\n";
                    case 168: // StanceOfMystic
                        return this.name + "：見切ったつもりでしょうが、甘いです。StanceOfMystic。\r\n";
                    case 169: // HardestParry
                        return this.name + "：その攻撃、瞬間で回避してみせましょう。HardestParryです。\r\n";
                    case 170: // ConcussiveHit
                        return this.name + "：防御率DOWNです、ConcussiveHit。\r\n";
                    case 171: // Onslaught hit
                        return this.name + "：攻撃率DOWNです、OnslaughtHit。\r\n";
                    case 172: // Impulse hit
                        return this.name + "：速度率DOWNです、ImpulseHit。\r\n";
                    case 173: // Fatal Blow
                        return this.name + "：さて、死んでもらいましょう、FatalBlow。\r\n";
                    case 174: // Exalted Field
                        return this.name + "：この場にて更なる増強をかけます、ExaltedField。\r\n";
                    case 175: // Rising Aura
                        return this.name + "：更に攻撃を増しますよ、RisingAuraです。\r\n";
                    case 176: // Ascension Aura
                        return this.name + "：更に魔法攻撃を上げましょうか、AscensionAuraです。\r\n";
                    case 177: // Angel Breath
                        return this.name + "：天使の加護による状態異常回復を、AngelBreathです。\r\n";
                    case 178: // Blazing Field
                        return this.name + "：すぐに燃やし尽くしてあげますよ、BlazingField。\r\n";
                    case 179: // Deep Mirror
                        return this.name + "：それが通るとは思ってないでしょう、DeepMirrorです。\r\n";
                    case 180: // Abyss Eye
                        return this.name + "：この眼を見た時が最後です、AbyssEye。\r\n";
                    case 181: // Doom Blade
                        return this.name + "：マナを断ち切らせてもらいます、DoomBlade。\r\n";
                    case 182: // Piercing Flame
                        return this.name + "：ここは貫通の火を使いましょう、PiercingFlame。\r\n";
                    case 183: // Phantasmal Wind
                        return this.name + "：さらに上げていきます、PhantasmalWindです。\r\n";
                    case 184: // Paradox Image
                        return this.name + "：潜在の源、ParadoxImageです。\r\n";
                    case 185: // Vortex Field
                        return this.name + "：皆さん鈍足になってください、VortexFieldです。\r\n";
                    case 186: // Static Barrier
                        return this.name + "：水理の加護、StaticBarrier\r\n";
                    case 187: // Unknown Shock
                        return this.name + "：あなたが暗闇で戦えるとは思えませんけどね、UnknownShockです。\r\n";
                    case 188: // SoulExecution
                        return this.name + "：ックク・・・SoulExecusion。\r\n";
                    case 189: // SoulExecution hit 01
                        return this.name + "：トゥ！\r\n";
                    case 190: // SoulExecution hit 02
                        return this.name + "：シッ！\r\n";
                    case 191: // SoulExecution hit 03
                        return this.name + "：ツェ！\r\n";
                    case 192: // SoulExecution hit 04
                        return this.name + "：セィ！\r\n";
                    case 193: // SoulExecution hit 05
                        return this.name + "：スゥ！\r\n";
                    case 194: // SoulExecution hit 06
                        return this.name + "：フッ！\r\n";
                    case 195: // SoulExecution hit 07
                        return this.name + "：ドゥ！\r\n";
                    case 196: // SoulExecution hit 08
                        return this.name + "：セイ！\r\n";
                    case 197: // SoulExecution hit 09
                        return this.name + "：ハアアァァ！\r\n";
                    case 198: // SoulExecution hit 10
                        return this.name + "：トドメです！ハアァァァ！！！\r\n";
                    case 199: // Nourish Sense
                        return this.name + "：回復量を上げさせてもらいましょう、NourishSenseです。\r\n";
                    case 200: // Mind Killing
                        return this.name + "：精神から攻めさせてもらいましょう、MindKilling。\r\n";
                    case 201: // Vigor Sense
                        return this.name + "：反応値を更に上げさせてもらいます、VigorSenseです。\r\n";
                    case 202: // ONE Authority
                        return this.name + "：全員上げていきましょうか、OneAuthority。\r\n";
                    case 203: // 集中と断絶
                        return this.name + "：【集中と断絶】　発動。\r\n";
                    case 204: // 【元核】発動済み
                        return this.name + "：すみませんが【元核】は、今日すでに発動済みです。\r\n";
                    case 205: // 【元核】通常行動選択時
                        return this.name + "：【元核】はインスタントタイミング限定です。\r\n";
                    case 206: // Sigil Of Homura
                        return this.name + "：これが決まったら、ほぼ終わりですね。SigilOfHomuraです。\r\n";
                    case 207: // Austerity Matrix
                        return this.name + "：支配力、変えさせてもらいます、AusterityMatrix。\r\n";
                    case 208: // Red Dragon Will
                        return this.name + "：火竜よ、ボクに仕えよ、RedDragonWill。\r\n";
                    case 209: // Blue Dragon Will
                        return this.name + "：水竜よ、ボクに仕えよ、BlueDragonWill。\r\n";
                    case 210: // Eclipse End
                        return this.name + "：ックク、これで全てが無駄となりますね、EclipseEnd。\r\n";
                    case 211: // Sin Fortune
                        return this.name + "：次のクリティカル、覚悟してください、SinFortuneです。\r\n";
                    case 212: // AfterReviveHalf
                        return this.name + "：死耐性（ハーフ）を付与させてもらいます。\r\n";
                    case 213: // Demonic Ignite
                        return this.name + "：これでほぼ詰みですね、DemonicIgniteです。\r\n";
                    case 214: // Death Deny
                        return this.name + "：冒涜ではなく完全なる蘇りです、DeathDeny。\r\n";
                    case 215: // Stance of Double
                        return this.name + "：これぞ究極原理、StanceOfDoubleです。  " + this.name + "は前回行動を示す自分の分身を発生させた！\r\n";
                    case 216: // 最終戦ライフカウント消費
                        return this.name + "：神具よ！我に永遠の生命を！！ッハアアアァァァ！！！\r\n";
                    case 217: // 最終戦ライフカウント消滅時
                        return this.name + "：ッグ・・・セ・・・セフィ・・・ネ・・・\r\n";

                    case 2001: // ポーション回復時
                        return this.name + "：{0} 回復です。";
                    case 2002: // レベルアップ終了催促
                        return this.name + "：レベルアップを先にしてください。";
                    case 2003: // 荷物を減らせる催促
                        return this.name + "：{0}、荷物いっぱいですね？アイテムを減らしてからまた言ってください。";
                    case 2004: // 装備判断
                        return this.name + "：装備してもいいですか？";
                    case 2005: // 装備完了
                        return this.name + "：装備完了です。";
                    case 2006: // 遠見の青水晶を使用
                        return this.name + "：いったん、町に戻りましょうか？";
                    case 2007: // 売却専用品に対する一言
                        return this.name + "：売却専用の品物です。";
                    case 2008: // 非戦闘時のマナ不足
                        return this.name + "：マナが不足していますね。";
                    case 2009: // 神聖水使用時
                        return this.name + "：ライフ・スキル・マナが30%ずつ回復です。";
                    case 2010: // 神聖水、既に使用済みの場合
                        return this.name + "：もう水は残ってないようです。一日待たないと駄目ですね。";
                    case 2011: // リーベストランクポーション非戦闘使用時
                        return this.name + "：戦闘中専用品です。";
                    case 2012: // 遠見の青水晶を使用（町滞在時）
                        return this.name + "：町の中から使っても意味はありませんね。";
                    case 2013: // 遠見の青水晶を捨てられない時の台詞
                        return this.name + "：これはさすがに捨てられません。";
                    case 2014: // 非戦闘時のスキル不足
                        return this.name + "：スキルが不足していますね。";
                    case 2015: // 他プレイヤーが捨ててしまおうとした場合
                        return this.name + "：すいません、それはちょっと捨てないでください。";
                    case 2016: // リヴァイヴポーションによる復活
                        return this.name + "：おかげで復活できました。ありがとうございます。";
                    case 2017: // リヴァイヴポーション不要な時
                        return this.name + "：{0}は生きてないのでは？というブラックジョークです。";
                    case 2018: // リヴァイヴポーション対象が自分の時
                        return this.name + "：もしボクが死んでいたら使う行為自体できないはず。面白いジョークですね。";
                    case 2019: // 装備不可のアイテムを装備しようとした時
                        return this.name + "：これはボクでは装備できませんね。";
                    case 2020: // ラスボス撃破の後、遠見の青水晶を使用不可
                        return this.name + "：すいません、今の状況でこのアイテムを使う必要はなさそうです。";
                    case 2021: // アイテム捨てるの催促
                        return this.name + "：先にバックパックの整理をしませんか？";
                    case 2022: // オーバーシフティング使用開始時
                        return this.name + "：こ、これは・・・身体能力が再構築されるとは・・・素晴らしいですね。";
                    case 2023: // オーバーシフティングによるパラメタ割り振り時
                        return this.name + "：オーバーシフティングによる割り振りが終わってからにしましょう。";
                    case 2024: // 成長リキッドを使用した時
                        return this.name + "：【{0}】パラメタ、 {1} 上昇しましたね。";
                    case 2025:
                        return this.name + "：両手武器を装備していますからね。サブは装備できません。";
                    case 2026:
                        return this.name + "：武器（メイン）をはじめに何か装備しないといけませんね。";
                    case 2027: // 清透水使用時
                        return this.name + "：ライフが100%回復しましたね。";
                    case 2028: // 清透水、既に使用済みの場合
                        return this.name + "：もう水は残ってないゆです。一日待たないと駄目ですね。";
                    case 2029: // 荷物一杯で装備が外せない時
                        return this.name + "：バックパックを空けましょう。今のままでは装備は外せませんね。";
                    case 2030: // 荷物一杯で何かを捨てる時の台詞
                        return this.name + "：{0}を捨てましょう。いいですね？";
                    case 2031: // 戦闘中のアイテム使用限定
                        return this.name + "：戦闘中です。使用するアイテムを早く決めませんか？";
                    case 2032: // 戦闘中、アイテム使用できないアイテムを選択したとき
                        return this.name + "：このアイテムは戦闘中に使用は出来ませんね。";
                    case 2033: // 預けられない場合
                        return this.name + "：これを預けるのは得策ではありませんね。";
                    case 2034: // アイテムいっぱいで預かり所から引き出せない場合
                        return this.name + "：いえ、これ以上は持って行く必要はありませんね。";
                    case 2035: // Sacred Heal
                        return this.name + "：全員回復しましたね。";

                    case 3000: // 店に入った時の台詞
                        return this.name + "：無防備な状態ですね・・・";
                    case 3001: // 支払い要求時
                        return this.name + "：{0}を購入しましょう。 {1}Gold置きましょう。";
                    case 3002: // 持ち物いっぱいで買えない時
                        return this.name + "：アイテムがいっぱいのようです。少し整理させてください。";
                    case 3003: // 購入完了時
                        return this.name + "：これで・・・売買成立とさせてください。";
                    case 3004: // Gold不足で購入できない場合
                        return this.name + "：Goldがあと{0}足りないようです。";
                    case 3005: // 購入せずキャンセルした場合
                        return this.name + "：他を見させてください。";
                    case 3006: // 売れないアイテムを売ろうとした場合
                        return this.name + "：すいません、このアイテムは売却できません。";
                    case 3007: // アイテム売却時
                        return this.name + "：{0}を売却します。つまり、{1}Gold頂いても良いハズですね。";
                    case 3008: // 剣紋章ペンダント売却時
                        return this.name + "：コレはラナさんの作ったアクセサリですね。{0}Goldで本当に売るのでしょうか？";
                    case 3009: // 武具店を出る時
                        return this.name + "：また・・・いつか帰ってきてください。";
                    case 3010: // ガンツ不在時の売りきれフェルトゥーシュを見て一言
                        return this.name + "：この剣は・・・確かに売り切れですね。";
                    case 3011: // 装備可能なものが購入された時
                        return this.name + "：この場で装備しても良いでしょうか？";
                    case 3012: // 装備していた物を売却対象かどうか聞く時
                        return this.name + "：現在は、{0}を装備しています。鑑定は不得意ですが{1}Goldぐらいで売れるでしょう。";

                    case 4001: // 通常攻撃を選択
                    case 4002: // 防御を選択
                    case 4003: // 待機を選択
                    case 4004: // フレッシュヒールを選択
                    case 4005: // プロテクションを選択
                    case 4006: // ファイア・ボールを選択
                    case 4007: // フレイム・オーラを選択
                    case 4008: // ストレート・スマッシュを選択
                    case 4009: // ダブル・スマッシュを選択
                    case 4010: // スタンス・オブ・スタンディングを選択
                    case 4011: // アイス・ニードルを選択
                    case 4012:
                    case 4013:
                    case 4014:
                    case 4015:
                    case 4016:
                    case 4017:
                    case 4018:
                    case 4019:
                    case 4020:
                    case 4021:
                    case 4022:
                    case 4023:
                    case 4024:
                    case 4025:
                    case 4026:
                    case 4027:
                    case 4028:
                    case 4029:
                    case 4030:
                    case 4031:
                    case 4032:
                    case 4033:
                    case 4034:
                    case 4035:
                    case 4036:
                    case 4037:
                    case 4038:
                    case 4039:
                    case 4040:
                    case 4041:
                    case 4042:
                    case 4043:
                    case 4044:
                    case 4045:
                    case 4046:
                    case 4047:
                    case 4048:
                    case 4049:
                    case 4050:
                    case 4051:
                    case 4052:
                    case 4053:
                    case 4054:
                    case 4055:
                    case 4056:
                    case 4057:
                    case 4058:
                    case 4059:
                    case 4060:
                    case 4061:
                    case 4062:
                    case 4063:
                    case 4064:
                    case 4065:
                    case 4066:
                    case 4067:
                    case 4068:
                    case 4069:
                    case 4070:
                    case 4071:
                        return this.name + "：" + this.ActionLabel.Text + "でいきます。\r\n";
                    case 4072:
                        return this.name + "：敵を対象にするわけにはいきませんね。\r\n";
                    case 4073:
                        return this.name + "：敵を対象にするわけにはいきませんね。\r\n";
                    case 4074:
                        return this.name + "：味方を対象にはできませんね。\r\n";
                    case 4075:
                        return this.name + "：味方を対象にはできませんね。\r\n";
                    case 4076:
                        return this.name + "：味方に攻撃をするつもりはありません。\r\n";
                    case 4077: // 「ためる」コマンド
                        return this.name + "：魔力をため込みます。\r\n";
                    case 4078: // 武器発動「メイン」
                        return this.name + "：メイン武器の効果を発動させます。\r\n";
                    case 4079: // 武器発動「サブ」
                        return this.name + "：サブ武器の効果を発動させます。\r\n";
                    case 4080: // アクセサリ１発動
                        return this.name + "：アクセサリ１の効果を発動させます。\r\n";
                    case 4081: // アクセサリ２発動
                        return this.name + "：アクセサリ２の効果を発動させます。\r\n";

                    case 4082: // FlashBlaze
                        return this.name + "：ここはフラッシュブレイズですね。\r\n";

                    // 武器攻撃
                    case 5001:
                        return this.name + "：エアロ・スラッシュです。ハアアァ！！ {0} へ {1} のダメージ\r\n";
                    case 5002:
                        return this.name + "：{0} 回復です \r\n";
                    case 5003:
                        return this.name + "：{0} マナ回復です \r\n";
                    case 5004:
                        return this.name + "：アイシクル・スラッシュ、行きます！ {0} へ {1} のダメージ\r\n";
                    case 5005:
                        return this.name + "：エレクトロ・ブローです、ハアァァ！ {0} へ {1} のダメージ\r\n";
                    case 5006:
                        return this.name + "：ブルー・ライトニングです。ハアァァ！ {0} へ {1} のダメージ\r\n";
                    case 5007:
                        return this.name + "：バーニング・クレイモアです。ハアァァ！ {0} へ {1} のダメージ\r\n";
                    case 5008:
                        return this.name + "：赤蒼授からの炎です。ハアァァ！ {0} へ {1} のダメージ\r\n";
                    case 5009:
                        return this.name + "：{0} スキルポイント回復です \r\n";
                    case 5010:
                        return this.name + "が装着している指輪が強烈に蒼い光を放った！ {0} へ {1} のダメージ\r\n";
                }
            }
            #endregion
            #region "ランディス"
            if (this.name == "ランディス")
            {
                switch (sentenceNumber)
                {
                    case 0: // スキル不足
                        return this.name + "：ッチ・・・スキルがねぇ\r\n";
                    case 1: // Straight Smash
                        return this.name + "：ッラァ！！\r\n";
                    case 2: // Double Slash 1
                        return this.name + "：ッラ！\r\n";
                    case 3: // Double Slash 2
                        return this.name + "：ッドラァ！\r\n";
                    case 4: // Painful Insanity
                        return this.name + "：精神勝負で負ける気はしねえ、朽ちろや。\r\n";
                    case 5: // empty skill
                        return this.name + "：ッチ・・・スキル未定とは・・・\r\n";
                    case 6: // 絡みつくフランシスの必殺を食らった時
                        return this.name + "：っきかねえなぁ・・・\r\n";
                    case 7: // Lizenosの必殺を食らった時
                        return this.name + "：やるじゃねぇか・・・\r\n";
                    case 8: // Minfloreの必殺を食らった時
                        return this.name + "：ッケ・・・ドジったか・・・\r\n";
                    case 9: // Fresh Healによるライフ回復
                        return this.name + "：{0} 回復しといたぞ。\r\n";
                    case 10: // Fire Ball
                        return this.name + "：ファイア！ {0} へ {1} のダメージ\r\n";
                    case 11: // Flame Strike
                        return this.name + "：フレスト！ {0} へ {1} のダメージ\r\n";
                    case 12: // Volcanic Wave
                        return this.name + "：ヴォルカニィ！ {0} へ {1} のダメージ\r\n";
                    case 13: // 通常攻撃クリティカルヒット
                        return this.name + "：ッシャオラ、来た来たぁ！ {0} へ {1}のダメージ\r\n";
                    case 14: // FlameAuraによる追加攻撃
                        return this.name + "：オラヨ！ {0}の追加ダメージ\r\n";
                    case 15: //遠見の青水晶を戦闘中に使ったとき
                        return this.name + "：コイツは戦闘中使えねぇ。\r\n";
                    case 16: // 効果を発揮しないアイテムを戦闘中に使ったとき
                        return this.name + "：ッチィ！使えねぇアイテムだな！\r\n";
                    case 17: // 魔法でマナ不足
                        return this.name + "：ッケ・・・もうマナはねぇ\r\n";
                    case 18: // Protection
                        return this.name + "：プロッツ！ 物理防御上げとくぞ。\r\n";
                    case 19: // Absorb Water
                        return this.name + "：ァヴソーヴ！ 魔法防御上げとくぞ。\r\n";
                    case 20: // Saint Power
                        return this.name + "：セイント！ 物理攻撃上げとくぞ。\r\n";
                    case 21: // Shadow Pact
                        return this.name + "：シャドウ！ 魔法攻撃上げとくぞ。r\n";
                    case 22: // Bloody Vengeance
                        return this.name + "：ヴェンジェ！ 【力】を {0} 上げとくぞ。\r\n";
                    case 23: // Holy Shock
                        return this.name + "：ショック！ {0} へ {1} のダメージ\r\n";
                    case 24: // Glory
                        return this.name + "：グローリ！ 直接攻撃＋FreshHeal連携のオーラ\r\n";
                    case 25: // CelestialNova 1
                        return this.name + "：ノヴァ！ {0} 回復しといたぞ。\r\n";
                    case 26: // CelestialNova 2
                        return this.name + "：ノヴァ！ {0} のダメージ\r\n";
                    case 27: // Dark Blast
                        return this.name + "：ブラスト！ {0} へ {1} のダメージ\r\n";
                    case 28: // Lava Annihilation
                        return this.name + "：ダーッハッハッハ！燃えちまいなぁ！ {0} へ {1} のダメージ\r\n";
                    case 10028: // Lava Annihilation後編
                        return this.name + "：ダーッハッハッハ！燃えちまいなぁ！\r\n";
                    case 29: // Devouring Plague
                        return this.name + "：デヴォプラァ！ {0} ライフを吸い取った\r\n";
                    case 30: // Ice Needle
                        return this.name + "：ニードル！ {0} へ {1} のダメージ\r\n";
                    case 31: // Frozen Lance
                        return this.name + "：ランス！ {0} へ {1} のダメージ\r\n";
                    case 32: // Tidal Wave
                        return this.name + "：ウェイヴ！ {0} のダメージ\r\n";
                    case 33: // Word of Power
                        return this.name + "：ワーパワー！ {0} へ {1} のダメージ\r\n";
                    case 34: // White Out
                        return this.name + "：ホワイアウト！ {0} へ {1} のダメージ\r\n";
                    case 35: // Black Contract
                        return this.name + "：ッカッカッカ、コイツで撃ち放題だ。 " + this.name + "のスキル、魔法コストは０になる。\r\n";
                    case 36: // Flame Aura詠唱
                        return this.name + "：炎でぶった斬る。　 直接攻撃に炎の追加効果が付与される。\r\n";
                    case 37: // Damnation
                        return this.name + "：ソコで死んでろぉ、ダムネーション！　　死淵より出でる黒が空間を歪ませる。\r\n";
                    case 38: // Heat Boost
                        return this.name + "：ブースト！ 【技】を {0} 上げとくぞ。\r\n";
                    case 39: // Immortal Rave
                        return this.name + "：ラアアァァァァ！レイヴ！！ " + this.name + "の周りに３つの炎が宿った。\r\n";
                    case 40: // Gale Wind
                        return this.name + "：ゲイル！　 もう一人の" + this.name + "が現れた。\r\n";
                    case 41: // Word of Life
                        return this.name + "：ワーライフ！ 大自然からの強い息吹を感じ取れるようになった。\r\n";
                    case 42: // Word of Fortune
                        return this.name + "：ワーフォーチュ！ 決死のオーラが湧き上がった。\r\n";
                    case 43: // Aether Drive
                        return this.name + "：ドライヴ！ 周囲全体に空想物理力がみなぎる。\r\n";
                    case 44: // Eternal Presence 1
                        return this.name + "：ッゼンス！ " + this.name + "の周りに新しい法則が構築される。\r\n";
                    case 45: // Eternal Presence 2
                        return this.name + "の物理攻撃、物理防御、魔法攻撃、魔法防御がＵＰした！\r\n";
                    case 46: // One Immunity
                        return this.name + "：ワンイム！ " + this.name + "の周囲に目に見えない障壁が発生。\r\n";
                    case 47: // Time Stop
                        return this.name + "：ストップ！ 敵の時空を引き裂き時間停止させた。\r\n";
                    case 48: // Dispel Magic
                        return this.name + "：ディスペル！ \r\n";
                    case 49: // Rise of Image
                        return this.name + "：ライズ！ 【心】を {0} 上げとくぞ。\r\n";
                    case 50: // 空詠唱
                        return this.name + "：ッカ・・・詠唱してねぇぞ・・・\r\n";
                    case 51: // Inner Inspiration
                        return this.name + "：は潜在集中力を高めた。精神が研ぎ澄まされる。 {0} スキルポイント回復\r\n";
                    case 52: // Resurrection 1
                        return this.name + "：リザレク！\r\n";
                    case 53: // Resurrection 2
                        return this.name + "：ヴォケが！対象ちげぇ！！\r\n";
                    case 54: // Resurrection 3
                        return this.name + "：ヴォケが！死んでねぇ！！\r\n";
                    case 55: // Resurrection 4
                        return this.name + "：ヴォケも大概にしろ。\r\n";
                    case 56: // Stance Of Standing
                        return this.name + "：ッフン！\r\n";
                    case 57: // Mirror Image
                        return this.name + "：ミライメ！ {0}の周囲に青い空間が発生した。\r\n";
                    case 58: // Mirror Image 2
                        return this.name + "：返すぞオラァ！ {0} ダメージを {1} に反射した！\r\n";
                    case 59: // Mirror Image 3
                        return this.name + "：返すぞオラァ！ 【しかし、強力な威力ではね返せない！】" + this.name + "：ッケ、クソが\r\n";
                    case 60: // Deflection
                        return this.name + "：デフレク！ {0}の周囲に白い空間が発生した。\r\n";
                    case 61: // Deflection 2
                        return this.name + "：返すぞオラァ！ {0} ダメージを {1} に反射した！\r\n";
                    case 62: // Deflection 3
                        return this.name + "：返すぞオラァ！ 【しかし、強力な威力ではね返せない！】" + this.name + "：ッケ、クソが\r\n";
                    case 63: // Truth Vision
                        return this.name + "：トルゥス！　" + this.name + "は対象のパラメタＵＰを無視するようになった。\r\n";
                    case 64: // Stance Of Flow
                        return this.name + "：スタンフロウ！　" + this.name + "は次３ターン、必ず後攻を取るように構えた。\r\n";
                    case 65: // Carnage Rush 1
                        return this.name + "：オラァ！ {0}ダメージ・・・   ";
                    case 66: // Carnage Rush 2
                        return "ッラァ！ {0}ダメージ   ";
                    case 67: // Carnage Rush 3
                        return "ッラアァ！ {0}ダメージ   ";
                    case 68: // Carnage Rush 4
                        return "ッラアァァ！ {0}ダメージ   ";
                    case 69: // Carnage Rush 5
                        return "ウォラアァァァ！！ {0}のダメージ\r\n";
                    case 70: // Crushing Blow
                        return this.name + "：クラッシュ！  {0} へ {1} のダメージ。\r\n";
                    case 71: // リーベストランクポーション戦闘使用時
                        return this.name + "：コイツでも飲んでな。\r\n";
                    case 72: // Enigma Sence
                        return this.name + "：エニグマ！\r\n";
                    case 73: // Soul Infinity
                        return this.name + "：ブッッッッタ斬る、ラァァァ！！！\r\n";
                    case 74: // Kinetic Smash
                        return this.name + "：スマァッシュ！！\r\n";
                    case 75: // Silence Shot (Altomo専用)
                        return "";
                    case 76: // Silence Shot、AbsoluteZero沈黙による詠唱失敗
                        return this.name + "：・・・ッケ、詠唱妨害か、くだらねぇ。\r\n";
                    case 77: // Cleansing
                        return this.name + "：クリーン！\r\n";
                    case 78: // Pure Purification
                        return this.name + "：ピュリファイ！\r\n";
                    case 79: // Void Extraction
                        return this.name + "：ヴォイデクス！" + this.name + "の {0} が {1}ＵＰ！\r\n";
                    case 80: // アカシジアの実使用時
                        return this.name + "：少しは目を醒ませ。\r\n";
                    case 81: // Absolute Zero
                        return this.name + "：アブスゼロ！ 氷吹雪効果により、ライフ回復不可、スペル詠唱不可、スキル使用不可、防御不可となった。\r\n";
                    case 82: // BUFFUP効果が望めない場合
                        return "しかし、既にそのパラメタは上昇しているため、効果がなかった。\r\n";
                    case 83: // Promised Knowledge
                        return this.name + "：プロナレ！　【知】を {0} 上げとくぞ。\r\n";
                    case 84: // Tranquility
                        return this.name + "：トランキィ！\r\n";
                    case 85: // High Emotionality 1
                        return this.name + "：ハイエモ！　来たきたぁ！！\r\n";
                    case 86: // High Emotionality 2
                        return this.name + "の力、技、知、心パラメタがＵＰした！\r\n";
                    case 87: // AbsoluteZeroでスキル使用失敗
                        return this.name + "：ッケ・・・使えねぇなぁ。\r\n";
                    case 88: // AbsoluteZeroによる防御失敗
                        return this.name + "：ッカ・・・防御できねぇとはな。 \r\n";
                    case 89: // Silent Rush 1
                        return this.name + "：ラッシュ！！ {0}ダメージ・・・   ";
                    case 90: // Silent Rush 2
                        return "ッフン！ {0}ダメージ   ";
                    case 91: // Silent Rush 3
                        return "ッラアァァ！ {0}のダメージ\r\n";
                    case 92: // BUFFUP以外の永続効果が既についている場合
                        return "しかし、既にその効果は付与されている。\r\n";
                    case 93: // Anti Stun
                        return this.name + "：アンスタ！ " + this.name + "はスタン効果への耐性が付いた\r\n";
                    case 94: // Anti Stunによるスタン回避
                        return this.name + "：スタン回避ぐらいとぉぜんだ。\r\n";
                    case 95: // Stance Of Death
                        return this.name + "：スタンデス！ " + this.name + "は致死を１度回避できるようになった\r\n";
                    case 96: // Oboro Impact 1
                        return this.name + "：オボロ行くぞ・・・ッフウウウゥゥゥゥ・・・\r\n";
                    case 97: // Oboro Impact 2
                        return this.name + "：ウオォォラアアァ！！　 {0}へ{1}のダメージ\r\n";
                    case 98: // Catastrophe 1
                        return this.name + "：悪いがコッパミジンだ、カタスト！\r\n";
                    case 99: // Catastrophe 2
                        return this.name + "：死んでこいやああぁぁ！！　 {0}のダメージ\r\n";
                    case 100: // Stance Of Eyes
                        return this.name + "：スタンアイ！ " + this.name + "は、相手の行動に備えている・・・\r\n";
                    case 101: // Stance Of Eyesによるキャンセル時
                        return this.name + "：おせぇな！！　" + this.name + "は相手のモーションを見切って、行動キャンセルした！\r\n";
                    case 102: // Stance Of Eyesによるキャンセル失敗時
                        return this.name + "：ッケ・・・　" + this.name + "は相手のモーションを見切れなかった！\r\n";
                    case 103: // Negate
                        return this.name + "：ニゲイト！　" + this.name + "は相手のスペル詠唱に備えている・・・\r\n";
                    case 104: // Negateによるスペル詠唱キャンセル時
                        return this.name + "：おせえぇ！" + this.name + "は相手のスペル詠唱を弾いた！\r\n";
                    case 105: // Negateによるスペル詠唱キャンセル失敗時
                        return this.name + "：ッカ・・・" + this.name + "は相手のスペル詠唱を見切れなかった！\r\n";
                    case 106: // Nothing Of Nothingness 1
                        return this.name + "：ナッシング！ " + this.name + "に無色のオーラが宿り始める！ \r\n";
                    case 107: // Nothing Of Nothingness 2
                        return this.name + "は無効化魔法を無効化、　無効化するスキルを無効化するようになった！\r\n";
                    case 108: // Genesis
                        return this.name + "：ジェネ！  " + this.name + "は前回の行動を自分へと投影させた！\r\n";
                    case 109: // Cleansing詠唱失敗時
                        return this.name + "：クリーンジングミスだ。\r\n";
                    case 110: // CounterAttackを無視した時
                        return this.Name + "：無駄だな。\r\n";
                    case 111: // 神聖水使用時
                        return this.name + "：ライフ・スキル・マナが30%ずつ回復しといたぞ。\r\n";
                    case 112: // 神聖水、既に使用済みの場合
                        return this.name + "：一日１回だ。\r\n";
                    case 113: // CounterAttackによる反撃メッセージ
                        return this.name + "：おせぇ！カウンター！\r\n";
                    case 114: // CounterAttackに対する反撃がNothingOfNothingnessによって防がれた時
                        return this.name + "：ッカ・・・ッスったか・・・\r\n";
                    case 115: // 通常攻撃のヒット
                        return this.name + "の攻撃がヒット。 {0} へ {1} のダメージ\r\n";
                    case 116: // 防御を無視して攻撃する時
                        return this.name + "：無駄だな。\r\n";
                    case 117: // StraightSmashなどのスキルクリティカル
                        return this.name + "：ッシャラァ！クリティカル！\r\n";
                    case 118: // 戦闘時、リヴァイヴポーションによる復活のかけ声
                        return this.name + "：復活させるぞ。\r\n";
                    case 119: // Absolute Zeroによりライフ回復できない場合
                        return this.name + "：ッケ・・・回復も無駄だったな\r\n";
                    case 120: // 魔法攻撃のヒット
                        return "{0} へ {1} のダメージ\r\n";
                    case 121: // Absolute Zeroによりマナ回復できない場合
                        return this.name + "：ッカ・・・マナ回復も無駄だったな\r\n";
                    case 122: // 「ためる」行動時
                        return this.name + "：魔力、蓄えるぞ。\r\n";
                    case 123: // 「ためる」行動で溜まりきっている場合
                        return this.name + "：蓄積上限だ。\r\n";
                    case 124: // StraightSmashのダメージ値
                        return this.name + "のストレート・スマッシュがヒット。 {0} へ {1}のダメージ\r\n";
                    case 125: // アイテム使用ゲージが溜まってないとき
                        return this.name + "：アイテムゲージがまだだ。\r\n";
                    case 126: // FlashBlase
                        return this.name + "：ブレイズ！ {0} へ {1} のダメージ\r\n";
                    case 127: // 複合魔法でインスタント不足
                        return this.name + "：ヴォケが、インスタント足りねぇだろぉが！\r\n";
                    case 128: // 複合魔法はインスタントタイミングでうてない
                        return this.name + "：コイツはインスタントだ。\r\n";
                    case 129: // PsychicTrance
                        return this.name + "：サイトラ！　魔法攻撃強化だ。\r\n";
                    case 130: // BlindJustice
                        return this.name + "：ジャスティス！　物理攻撃強化だ。\r\n";
                    case 131: // TranscendentWish
                        return this.name + "：トラッセン！　\r\n";
                    case 132: // LightDetonator
                        return this.name + "：デトネイト！\r\n";
                    case 133: // AscendantMeteor
                        return this.name + "：ッラアアァァ！！　メテオ！！\r\n";
                    case 134: // SkyShield
                        return this.name + "：シールド！\r\n";
                    case 135: // SacredHeal
                        return this.name + "：サークレッド！\r\n";
                    case 136: // EverDroplet
                        return this.name + "：エヴァドロー！\r\n";
                    case 137: // FrozenAura
                        return this.name + "：アイスオーラ！\r\n";
                    case 138: // ChillBurn
                        return this.name + "：食らえ、ッバーン！\r\n";
                    case 139: // ZetaExplosion
                        return this.name + "：ゼータ！\r\n";
                    case 140: // FrozenAura追加効果ダメージで
                        return this.name + "：凍れや！ {0}の追加ダメージ\r\n";
                    case 141: // StarLightning
                        return this.name + "：ライトニング！\r\n";
                    case 142: // WordOfMalice
                        return this.name + "：ワーマリス！\r\n";
                    case 143: // BlackFire
                        return this.name + "：ブラックファイア！\r\n";
                    case 144: // EnrageBlast
                        return this.name + "：エンレイジ！\r\n";
                    case 145: // Immolate
                        return this.name + "：イモレ！\r\n";
                    case 146: // VanishWave
                        return this.name + "：ヴァニッシュ！\r\n";
                    case 147: // WordOfAttitude
                        return this.name + "：ワーアッティ！\r\n";
                    case 148: // HolyBreaker
                        return this.name + "：ブレイカー！\r\n";
                    case 149: // DarkenField
                        return this.name + "：ダーケン！\r\n";
                    case 150: // SeventhMagic
                        return this.name + "：セヴェンス！\r\n";
                    case 151: // BlueBullet
                        return this.name + "：ブルバレ！\r\n";
                    case 152: // NeutralSmash
                        return this.name + "：ッシャァ！スマッシュ！\r\n";
                    case 153: // SwiftStep
                        return this.name + "：スウィフト！\r\n";
                    case 154: // CircleSlash
                        return this.name + "：サークル！\r\n";
                    case 155: // RumbleShout
                        return this.name + "：コッチだ、オラァ！\r\n";
                    case 156: // SmoothingMove
                        return this.name + "：スムージン！\r\n";
                    case 157: // FutureVision
                        return this.name + "：フューチャーヴィ！\r\n";
                    case 158: // ReflexSpirit
                        return this.name + "：リフレ！\r\n";
                    case 159: // SharpGlare
                        return this.name + "：シャーグレ！\r\n";
                    case 160: // TrustSilence
                        return this.name + "：トラッサイレン！\r\n";
                    case 161: // SurpriseAttack
                        return this.name + "：ップライズ！\r\n";
                    case 162: // PsychicWave
                        return this.name + "：サイキック！\r\n";
                    case 163: // Recover
                        return this.name + "：リカバ！\r\n";
                    case 164: // ViolentSlash
                        return this.name + "：ッヴァイオレン！\r\n";
                    case 165: // OuterInspiration
                        return this.name + "：アウスピ！\r\n";
                    case 166: // StanceOfSuddenness
                        return this.name + "：ッサドン！\r\n";
                    case 167: // インスタント対象で発動不可
                        return this.name + "：コイツはインスタント対象専用だ。\r\n";
                    case 168: // StanceOfMystic
                        return this.name + "：ミスティッ！\r\n";
                    case 169: // HardestParry
                        return this.name + "：ッカ、面倒くせぇ、ハードパリィ！\r\n";
                    case 170: // ConcussiveHit
                        return this.name + "：カッシヴ！\r\n";
                    case 171: // Onslaught hit
                        return this.name + "：オンッスロ！";
                    case 172: // Impulse hit
                        return this.name + "：パルス！";
                    case 173: // Fatal Blow
                        return this.name + "：フェタル！";
                    case 174: // Exalted Field
                        return this.name + "：ッハァ、悪ぃな！　イグッザルツ！\r\n";
                    case 175: // Rising Aura
                        return this.name + "：ライジンッ！\r\n";
                    case 176: // Ascension Aura
                        return this.name + "：セッション！\r\n";
                    case 177: // Angel Breath
                        return this.name + "：ッカ、面倒くせえ、ブレス！\r\n";
                    case 178: // Blazing Field
                        return this.name + "：レイジン・フィー！\r\n";
                    case 179: // Deep Mirror
                        return this.name + "：ディプミラ！\r\n";
                    case 180: // Abyss Eye
                        return this.name + "：アビッスァ！\r\n";
                    case 181: // Doom Blade
                        return this.name + "：ドゥーム・レイ！\r\n";
                    case 182: // Piercing Flame
                        return this.name + "：ピアッ・フレイ！\r\n";
                    case 183: // Phantasmal Wind
                        return this.name + "：ファンタズマ！\r\n";
                    case 184: // Paradox Image
                        return this.name + "：ドックス！\r\n";
                    case 185: // Vortex Field
                        return this.name + "：テックス・フィール！\r\n";
                    case 186: // Static Barrier
                        return this.name + "：スタック・バリア！\r\n";
                    case 187: // Unknown Shock
                        return this.name + "：アン・ショック！\r\n";
                    case 188: // SoulExecution
                        return this.name + "：ッハーーッハッハッハァ！　ソウル・エグゼッ！！！\r\n";
                    case 189: // SoulExecution hit 01
                        return this.name + "：ラァ！\r\n";
                    case 190: // SoulExecution hit 02
                        return this.name + "：ッラァ！\r\n";
                    case 191: // SoulExecution hit 03
                        return this.name + "：オラァ！\r\n";
                    case 192: // SoulExecution hit 04
                        return this.name + "：オラアァ！\r\n";
                    case 193: // SoulExecution hit 05
                        return this.name + "：ッラ！\r\n";
                    case 194: // SoulExecution hit 06
                        return this.name + "：ッララ！\r\n";
                    case 195: // SoulExecution hit 07
                        return this.name + "：ッララアァァ！\r\n";
                    case 196: // SoulExecution hit 08
                        return this.name + "：オラララアアァ！\r\n";
                    case 197: // SoulExecution hit 09
                        return this.name + "：オラオラオラ！\r\n";
                    case 198: // SoulExecution hit 10
                        return this.name + "：食らえやあ！ラアアァァ！！！\r\n";
                    case 199: // Nourish Sense
                        return this.name + "：ノッセンス！\r\n";
                    case 200: // Mind Killing
                        return this.name + "：マイン・ッキル！\r\n";
                    case 201: // Vigor Sense
                        return this.name + "：ヴィゴー！\r\n";
                    case 202: // ONE Authority
                        return this.name + "：ワン・オース！\r\n";
                    case 203: // 集中と断絶
                        return this.name + "：【集中と断絶】　発動。\r\n";
                    case 204: // 【元核】発動済み
                        return this.name + "：ッケ、そんな何度も発動してらんねぇぜ。\r\n";
                    case 205: // 【元核】通常行動選択時
                        return this.name + "：んなの、いつでもいいだろ。\r\n";
                    case 206: // Sigil Of Homura
                        return this.name + "：シギィル！\r\n";
                    case 207: // Austerity Matrix
                        return this.name + "：アゥス・トゥリックス！\r\n";
                    case 208: // Red Dragon Will
                        return this.name + "：レッドラァ！\r\n";
                    case 209: // Blue Dragon Will
                        return this.name + "：ブルードラァ！\r\n";
                    case 210: // Eclipse End
                        return this.name + "：イクス・エンッ！\r\n";
                    case 211: // Sin Fortune
                        return this.name + "：ッシン！\r\n";
                    case 212: // AfterReviveHalf
                        return this.name + "：デッド・レジ・ハーフ！\r\n";
                    case 213: // Demonic Ignite
                        return this.name + "：ディーモッ！\r\n";
                    case 214: // Death Deny
                        return this.name + "：ッディナ！\r\n";
                    case 215: // Stance of Double
                        return this.name + "：スタンッダブル！  " + this.name + "は前回行動を示す自分の分身を発生させた！\r\n";
                    case 216: // 最終戦ライフカウント消費
                        return this.name + "：ッハ、この程度でくたばると思うなよ。\r\n";
                    case 217: // 最終戦ライフカウント消滅時
                        return this.name + "：・・・　ッ・・・\r\n";

                    case 2001: // ポーションまたは魔法による回復時
                        return this.name + "：{0} 回復しといたぞ。";
                    case 2002: // レベルアップ終了催促
                        return this.name + "：レベルアップさせろ。";
                    case 2003: // 荷物を減らせる催促
                        return this.name + "：{0}、荷物を減らせ。アイテムを渡させろ。";
                    case 2004: // 装備判断
                        return this.name + "：装備だな？";
                    case 2005: // 装備完了
                        return this.name + "：装備したぞ。";
                    case 2006: // 遠見の青水晶を使用
                        return this.name + "：町戻るか？";
                    case 2007: // 売却専用品に対する一言
                        return this.name + "：売却専用品だ。";
                    case 2008: // 非戦闘時のマナ不足
                        return this.name + "：マナ不足だ。";
                    case 2009: // 神聖水使用時
                        return this.name + "：ライフ・スキル・マナが30%ずつ回復しといたぞ。";
                    case 2010: // 神聖水、既に使用済みの場合
                        return this.name + "：もう枯れてる。一日１回だ。";
                    case 2011: // リーベストランクポーション非戦闘使用時
                        return this.name + "：戦闘中専用品だ。";
                    case 2012: // 遠見の青水晶を使用（町滞在時）
                        return this.name + "：町ん中じゃ無駄だ。";
                    case 2013: // 遠見の青水晶を捨てられない時の台詞
                        return this.name + "：コイツは捨てれねぇ。";
                    case 2014: // 非戦闘時のスキル不足
                        return this.name + "：スキル不足だ。";
                    case 2015: // 他プレイヤーが捨ててしまおうとした場合
                        return this.name + "：ッオイ、ソイツは捨てるな。";
                    case 2016: // リヴァイヴポーションによる復活
                        return this.name + "：復活！";
                    case 2017: // リヴァイヴポーション不要な時
                        return this.name + "：{0}にコイツは不要だ。";
                    case 2018: // リヴァイヴポーション対象が自分の時
                        return this.name + "：ヴォケも大概にしろ。";
                    case 2019: // 装備不可のアイテムを装備しようとした時
                        return this.name + "：んなもん、装備させんな。";
                    case 2020: // ラスボス撃破の後、遠見の青水晶を使用不可
                        return this.name + "：今はそんな時じゃねぇ。";
                    case 2021: // アイテム捨てるの催促
                        return this.name + "：バックパック整備ぐらいしろ。";
                    case 2022: // オーバーシフティング使用開始時
                        return this.name + "：パラメタ再構築させてもらうぞ。";
                    case 2023: // オーバーシフティングによるパラメタ割り振り時
                        return this.name + "：オーバーシフティング割り振りが先にさせろ。";
                    case 2024: // 成長リキッドを使用した時
                        return this.name + "：【{0}】パラメタ {1} 上昇。";
                    case 2025:
                        return this.name + "：両手武器なんでな、サブは装備できねえ。";
                    case 2026:
                        return this.name + "：武器（メイン）にまず装備だ。";
                    case 2027: // 清透水使用時
                        return this.name + "：ライフ100%回復しといたぞ。";
                    case 2028: // 清透水、既に使用済みの場合
                        return this.name + "：もう枯れてしまってるな。一日１回だ。";
                    case 2029: // 荷物一杯で装備が外せない時
                        return this.name + "：バックパック満杯だ。装備外す前に整備しとけ。";
                    case 2030: // 荷物一杯で何かを捨てる時の台詞
                        return this.name + "：{0}捨ててこのアイテム、入手か？";
                    case 2031: // 戦闘中のアイテム使用限定
                        return this.name + "：戦闘中だ。アイテム使用に集中しろ。";
                    case 2032: // 戦闘中、アイテム使用できないアイテムを選択したとき
                        return this.name + "：このアイテムは戦闘中は使えねぇ。";
                    case 2033: // 預けられない場合
                        return this.name + "：おい、何で預けなきゃなんねぇんだよ。";
                    case 2034: // アイテムいっぱいで預かり所から引き出せない場合
                        return this.name + "：これ以上は面倒くせぇ。";
                    case 2035: // Sacred Heal
                        return this.name + "：全員回復したぞ。";

                    case 3000: // 店に入った時の台詞
                        return this.name + "：ジジィ・・・誰もいねえのはやべぇだろが・・・。";
                    case 3001: // 支払い要求時
                        return this.name + "： {0} を買う。 {1} Gold払うぞ？";
                    case 3002: // 持ち物いっぱいで買えない時
                        return this.name + "：持ち物がいっぱいじゃねぇか。アイテム整備ぐらいしとけ。";
                    case 3003: // 購入完了時
                        return this.name + "：ジジィ、売買成立だ。";
                    case 3004: // Gold不足で購入できない場合
                        return this.name + "：Goldが{0}足りねえな。";
                    case 3005: // 購入せずキャンセルした場合
                        return this.name + "：他のアイテムを見せろ。";
                    case 3006: // 売れないアイテムを売ろうとした場合
                        return this.name + "：これを売る気はねえ。";
                    case 3007: // アイテム売却時
                        return this.name + "：{0}で売るぜ。{1}Goldもらってくぞ、ジジィ。";
                    case 3008: // 剣紋章ペンダント売却時
                        return this.name + "：マジで売るのか、{0}Goldだが？";
                    case 3009: // 武具店を出る時
                        return this.name + "：ジジィ、さすがに見張り付けろ・・・";
                    case 3010: // ガンツ不在時の売りきれフェルトゥーシュを見て一言
                        return this.name + "：ッカ・・・ザコアインが気付くかどうかだな。";
                    case 3011: // 装備可能なものが購入された時
                        return this.name + "：装備するぞ？";
                    case 3012: // 装備していた物を売却対象かどうか聞く時
                        return this.name + "：今の装備は、{0}だ。あのジジィなら{1}Goldで買い取りってトコだ。";

                    case 4001: // 通常攻撃を選択
                        return this.name + "：攻めるぜ。\r\n";
                    case 4002: // 防御を選択
                        return this.name + "：防御させろ。\r\n";
                    case 4003: // 待機を選択
                        return this.name + "：何もしねぇぞ。\r\n";
                    case 4004: // フレッシュヒールを選択
                    case 4005: // プロテクションを選択
                    case 4006: // ファイア・ボールを選択
                    case 4007: // フレイム・オーラを選択
                    case 4008: // ストレート・スマッシュを選択
                    case 4009: // ダブル・スマッシュを選択
                    case 4010: // スタンス・オブ・スタンディングを選択
                    case 4011: // アイス・ニードルを選択
                    case 4012:
                    case 4013:
                    case 4014:
                    case 4015:
                    case 4016:
                    case 4017:
                    case 4018:
                    case 4019:
                    case 4020:
                    case 4021:
                    case 4022:
                    case 4023:
                    case 4024:
                    case 4025:
                    case 4026:
                    case 4027:
                    case 4028:
                    case 4029:
                    case 4030:
                    case 4031:
                    case 4032:
                    case 4033:
                    case 4034:
                    case 4035:
                    case 4036:
                    case 4037:
                    case 4038:
                    case 4039:
                    case 4040:
                    case 4041:
                    case 4042:
                    case 4043:
                    case 4044:
                    case 4045:
                    case 4046:
                    case 4047:
                    case 4048:
                    case 4049:
                    case 4050:
                    case 4051:
                    case 4052:
                    case 4053:
                    case 4054:
                    case 4055:
                    case 4056:
                    case 4057:
                    case 4058:
                    case 4059:
                    case 4060:
                    case 4061:
                    case 4062:
                    case 4063:
                    case 4064:
                    case 4065:
                    case 4066:
                    case 4067:
                    case 4068:
                    case 4069:
                    case 4070:
                    case 4071:
                    case 4082: // FlashBlaze
                        return this.name + "：" + this.ActionLabel.Text + "だ。\r\n";

                    case 4072:
                        return this.name + "：敵にかけれねぇ。\r\n";
                    case 4073:
                        return this.name + "：敵にかけれねぇ。\r\n";
                    case 4074:
                        return this.name + "：味方にはかけれねぇ。\r\n";
                    case 4075:
                        return this.name + "：味方にはかけれねぇ。\r\n";
                    case 4076:
                        return this.name + "：味方に攻撃はできねぇ。\r\n";
                    case 4077: // 「ためる」コマンド
                        return this.name + "：ためるぞ。\r\n";
                    case 4078: // 武器発動「メイン」
                        return this.name + "：メイン発動させるぞ。\r\n";
                    case 4079: // 武器発動「サブ」
                        return this.name + "：サブ発動させるぞ。\r\n";
                    case 4080: // アクセサリ１発動
                        return this.name + "：アクセ１発動させるぞ。\r\n";
                    case 4081: // アクセサリ２発動
                        return this.name + "：アクセ２発動させるぞ。\r\n";

                    // 武器攻撃
                    case 5001:
                        return this.name + "：エアロ！ {0} へ {1} のダメージ\r\n";
                    case 5002:
                        return this.name + "：{0} 回復しといたぞ。 \r\n";
                    case 5003:
                        return this.name + "：{0} マナ回復しといたぞ。\r\n";
                    case 5004:
                        return this.name + "：アイシクル！ {0} へ {1} のダメージ\r\n";
                    case 5005:
                        return this.name + "：エレクトロ！ {0} へ {1} のダメージ\r\n";
                    case 5006:
                        return this.name + "：ブルーライト！ {0} へ {1} のダメージ\r\n";
                    case 5007:
                        return this.name + "：バーニン！ {0} へ {1} のダメージ\r\n";
                    case 5008:
                        return this.name + "：ブルーファイア！ {0} へ {1} のダメージ\r\n";
                    case 5009:
                        return this.name + "：{0} スキルポイント回復しといたぞ。\r\n";
                    case 5010:
                        return this.name + "が装着している指輪が強烈に蒼い光を放った！ {0} へ {1} のダメージ\r\n";
                }
            }
            #endregion
            #region "カールハンツ"
            if (this.name == Database.SINIKIA_KAHLHANZ)
            {
                switch (sentenceNumber)
                {
                    case 0: // スキル不足
                        return this.name + "：ッフ・・・スキル不足か。\r\n";
                    case 1: // Straight Smash
                        return this.name + "：ストレート・スマッシュを食らえぃ！\r\n";
                    case 2: // Double Slash 1
                        return this.name + "：っむぅん！！\r\n";
                    case 3: // Double Slash 2
                        return this.name + "：っむおおぉ！\r\n";
                    case 4: // Painful Insanity
                        return this.name + "：【心眼奥義】ペインフル・インサニティを食らえぃ！\r\n";
                    case 5: // empty skill
                        return this.name + "：ッフ・・・スキルの選択を忘れておったわ。\r\n";
                    case 6: // 絡みつくフランシスの必殺を食らった時
                        return this.name + "：ッ・・・\r\n";
                    case 7: // Lizenosの必殺を食らった時
                        return this.name + ": ッ・・・\r\n";
                    case 8: // Minfloreの必殺を食らった時
                        return this.name + "：ッ！・・・\r\n";
                    case 9: // Fresh Healによるライフ回復
                        return this.name + "：{0} 回復させてもらったぞ。\r\n";
                    case 10: // Fire Ball
                        return this.name + "：食らえぃ！ファイヤー・ボール！ {0} へ {1} のダメージ\r\n";
                    case 11: // Flame Strike
                        return this.name + "：っむぅん！フレーム・ストライク！ {0} へ {1} のダメージ\r\n";
                    case 12: // Volcanic Wave
                        return this.name + "：ぬうおぉ！ボルカニック・ウェーブ！ {0} へ {1} のダメージ\r\n";
                    case 13: // 通常攻撃クリティカルヒット
                        return this.name + "：おおおぉ、クリティカルダメージ！ {0} へ {1}のダメージ\r\n";
                    case 14: // FlameAuraによる追加攻撃
                        return this.name + "：踊れ炎よ！ {0}の追加ダメージ\r\n";
                    case 15: //遠見の青水晶を戦闘中に使ったとき
                        return this.name + "：ッフ・・・戦闘中に使うはずがなかろう。\r\n";
                    case 16: // 効果を発揮しないアイテムを戦闘中に使ったとき
                        return this.name + "：ッフ・・・使えんアイテムだったな。\r\n";
                    case 17: // 魔法でマナ不足
                        return this.name + "：ッフ・・・マナ不足とは。\r\n";
                    case 18: // Protection
                        return this.name + "：聖の加護、プロテクション！物理防御力：ＵＰ\r\n";
                    case 19: // Absorb Water
                        return this.name + "：水の加護、アブソーブ・ウオーター！ 魔法防御力：ＵＰ。\r\n";
                    case 20: // Saint Power
                        return this.name + "：聖なる力、セイント・パワー！ 物理攻撃力：ＵＰ\r\n";
                    case 21: // Shadow Pact
                        return this.name + "：闇の契約、シャドー・パクト！ 魔法攻撃力：ＵＰ\r\n";
                    case 22: // Bloody Vengeance
                        return this.name + "：復讐の力、ブラッデー・ベンジアンス！ 力パラメタが {0} ＵＰ\r\n";
                    case 23: // Holy Shock
                        return this.name + "：聖なる衝撃、ホーリー・ショック！ {0} へ {1} のダメージ\r\n";
                    case 24: // Glory
                        return this.name + "：栄光、グローリー！ 直接攻撃＋FreshHeal連携のオーラ\r\n";
                    case 25: // CelestialNova 1
                        return this.name + "：生命の輝き、セレステアル・ノバ！ {0} 回復させてもらったぞ。\r\n";
                    case 26: // CelestialNova 2
                        return this.name + "：生命への裁き、セレステアル・ノバ！ {0} のダメージ\r\n";
                    case 27: // Dark Blast
                        return this.name + "：闇の衝撃、ダーク・ブラスト！ {0} へ {1} のダメージ\r\n";
                    case 28: // Lava Annihilation
                        return this.name + "：炎の天使よ！焼き払えぃ！ラバ・アニヒレーション！ {0} へ {1} のダメージ\r\n";
                    case 10028: // Lava Annihilation後編
                        return this.name + "：炎の天使よ！焼き払えぃ！ラバ・アニヒレーション！\r\n";
                    case 29: // Devouring Plague
                        return this.name + "：命を吸収せしめよ！デボーリング・プラグー！ {0} ライフを吸い取った\r\n";
                    case 30: // Ice Needle
                        return this.name + "：氷の刃、アイス・ニードル！ {0} へ {1} のダメージ\r\n";
                    case 31: // Frozen Lance
                        return this.name + "：氷結槍、フローズン・ランス！ {0} へ {1} のダメージ\r\n";
                    case 32: // Tidal Wave
                        return this.name + "：っむうおぉ！タイダル・ウェイブ！ {0} のダメージ\r\n";
                    case 33: // Word of Power
                        return this.name + "：力の衝波、ワード・オブ・パワー！ {0} へ {1} のダメージ\r\n";
                    case 34: // White Out
                        return this.name + "：無に帰せよ、ホワイト・アウト！ {0} へ {1} のダメージ\r\n";
                    case 35: // Black Contract
                        return this.name + "：契約の力を見よ、ブラック・コントラクト！ " + this.name + "のスキル、魔法コストは０になる。\r\n";
                    case 36: // Flame Aura詠唱
                        return this.name + "：おおぉ炎よ！フレイム・オーラ！ 直接攻撃に炎の追加効果が付与される。\r\n";
                    case 37: // Damnation
                        return this.name + "：滅びなり、ダムネーション！ 死淵より出でる黒が空間を歪ませる。\r\n";
                    case 38: // Heat Boost
                        return this.name + "：っむぅん！ヒート・ブースト！ 技パラメタが {0} ＵＰ。\r\n";
                    case 39: // Immortal Rave
                        return this.name + "：炎に宿りし力！イモータル・レイブ！ " + this.name + "の周りに３つの炎が宿った。\r\n";
                    case 40: // Gale Wind
                        return this.name + "：我が分身、現れよ！ゲール・ウインド！ もう一人の" + this.name + "が現れた。\r\n";



                }
            }
            #endregion
            #region "ガンツ"
            else if (this.name == "ガンツ")
            {
                switch (sentenceNumber)
                {
                    case 3000: // 店に入った時の台詞
                        return this.name + "：ゆっくり見ていくがいい。";
                    case 3001: // 支払い要求時
                        return this.name + "：{0}は{1}Goldだ。買うかね？";
                    case 3002: // 持ち物いっぱいで買えない時
                        return this.name + "：おや、荷物がいっぱいだよ。手持ちを減らしてからまた来なさい。";
                    case 3003: // 購入完了時
                        return this.name + "：はいよ、まいどあり。";
                    case 3004: // Gold不足で購入できない場合
                        return this.name + "：すまない、こちらも商売でな。後{0}必要だ。";
                    case 3005: // 購入せずキャンセルした場合
                        return this.name + "：他のも見ていくがいい。";
                    case 3006: // 売れないアイテムを売ろうとした場合
                        return this.name + "：すまないが、それは買い取れん。";
                    case 3007: // アイテム売却時
                        return this.name + "：ふむ、{0}だな。{1}Goldで買い取ろうか。";
                    case 3008: // 剣紋章ペンダント売却時
                        return this.name + "：ふむ・・・良い出来具合のアクセサリだ。{0}Goldだが、本当に買い取って良いのか？";
                    case 3009: // 武具店を出る時
                        return this.name + "：またいつでも寄りなさい。";
                    case 3010: // ガンツ不在時の売りきれフェルトゥーシュを見て一言
                        return "";
                    case 3011: // 装備可能なものが購入された時
                        return this.name + "：ふむ、今ここで装備していくかね？";
                    case 3012: // 装備していた物を売却対象かどうか聞く時
                        return this.name + "：現在の装備品を売却するかね？{0}は{1}Goldで買い取ろう。";
                    case 3013: // 両手持ち装備をした後、サブ武器を売却せず、手元に残そうとして、バックパックがいっぱいの時
                        return this.name + "：荷物がいっぱいのようだな。{0}はハンナの宿屋倉庫に後で送っておこう。";
                }
            }
            #endregion
            #region "三階の守護者：Minflore"
            else if (this.name == "三階の守護者：Minflore")
            {
                switch (sentenceNumber)
                {
                    case 2: // Double Slash 1 Carnage Rush 1
                        return this.name + "：ッハ！\r\n";
                    case 3: // Double Slash 2 Carnage Rush 2
                        return this.name + "：ッヤァ！\r\n";
                    case 9: // Fresh Healによるライフ回復
                        return this.name + "：ライフ回復させてもらうわ、FreshHeal。{0} 回復。\r\n";
                    case 13: // 通常攻撃クリティカルヒット
                        return this.name + "：あなたのが弱点、見切ったわ！クリティカル！！ {0} へ {1}のダメージ\r\n";
                    case 18: // Protection
                        return this.name + "：聖なる神よ、われに与えよ、Protection。 物理防御力：ＵＰ\r\n";
                    case 19: // Absorb Water
                        return this.name + "：水の女神よ、われに与えよ、AbsorbWater。 魔法防御力：ＵＰ。\r\n";
                    case 20: // Saint Power
                        return this.Name + "：力が全てを覆すわ、SaintPower。物理攻撃力：ＵＰ\r\n";
                    case 24: // Glory
                        return this.name + "：光を照らし、栄光を私に！Glory！　 直接攻撃＋FreshHeal連携のオーラ\r\n";
                    case 70: // Crushing Blow
                        return this.name + "：これでも食らいなさい、CrushingBlow。  {0} へ {1} のダメージ。\r\n";
                    case 115: // 通常攻撃のヒット
                        return this.name + "からの攻撃。 {0} へ {1} のダメージ\r\n";
                    case 117: // StraightSmashなどのスキルクリティカル
                        return this.name + "：ここでクリティカルヒットよ！\r\n";
                }
            }
            #endregion
            #region "四階の守護者：Altomo"
            else if (this.name == "四階の守護者：Altomo")
            {
                switch (sentenceNumber)
                {
                    case 13: // 通常攻撃クリティカルヒット
                        return this.name + "：当たれ！！クリティカル！！ {0} へ {1} のダメージ\r\n";
                    case 42: // Word of Fortune
                        return this.name + "：次は必殺だ、さあ死んでもらおうか！ 決死のオーラが湧き上がった。\r\n";
                    case 63: // Truth Vision
                        return this.name + "：パラメタＵＰなどとこざかしいな！　対象のパラメタＵＰを無視するようになった。\r\n";
                    case 70: // Scatter Shot (Crushing Blow)
                        return this.name + "：邪魔だ、寝てろ。　ScatterShot！ {0} へ {1}のダメージ。\r\n";
                    case 75: // Silence Shot (Altomo専用)
                        return "{0} は魔法詠唱ができなくなった！\r\n";
                    case 76: // Silence Shot、AbsoluteZero沈黙による詠唱失敗
                        return this.name + "：っこ・・・この程度の魔法で・・・\r\n";
                    case 87: // AbsoluteZeroでスキル使用失敗
                        return this.name + "：っこ・・・この程度の魔法で・・・\r\n";
                    case 88: // AbsoluteZeroによる防御失敗
                        return this.name + "：っこ・・・この程度の魔法で・・・\r\n";
                    case 110: // CounterAttackを無視した時
                        return this.Name + "：そんな構えぐらいお見通しだ！　効くわけがないだろう！！\r\n";
                    case 115: // 通常攻撃のヒット
                        return this.name + "からの攻撃。 {0} へ {1} のダメージ\r\n";
                    case 116: // 防御を無視して攻撃する時
                        return this.name + "：防御など無駄な行為だ！　甘いな！！\r\n";
                    case 117: // StraightSmashなどのスキルクリティカル
                        return this.name + "：っふん、クリティカルだ！\r\n";
                    case 119: // Absolute Zeroによりライフ回復できない場合
                        return this.name + "：っふん、もともとライフ回復などと、邪推するはずなかろう！！\r\n";
                }
            }
            #endregion
            #region "五階の守護者：Bystander"
            else if (this.name == "五階の守護者：Bystander")
            {
                switch (sentenceNumber)
                {
                    case 18: // Protection
                        return this.name + "：『聖魔法』  －　『Protection』　物理防御力：ＵＰ\r\n";
                    case 19: // Absorb Water
                        return this.name + "：『水魔法』  －　『AbsorbWater』　魔法防御力：ＵＰ。\r\n";
                    case 20: // Saint Power
                        return this.name + "：『聖魔法』  －　『SaintPower』　物理攻撃力：ＵＰ\r\n";
                    case 21: // Shadow Pact
                        return this.name + "：『闇魔法』  －　『ShadowPact』　魔法攻撃力：ＵＰ\r\n";
                    case 22: // Bloody Vengeance
                        return this.name + "：『闇魔法』  －　『BloodyVengeance』　力パラメタが {0} ＵＰ\r\n";
                    case 38: // Heat Boost
                        return this.name + "：『火魔法』  －　『HeatBoost』　技パラメタが {0} ＵＰ。\r\n";
                    case 44: // Eternal Presence 1
                        return this.name + "：『理魔法』  －　『EternalPresence』　" + this.name + "の周りに新しい法則が構築される。\r\n";
                    case 45: // Eternal Presence 2
                        return this.name + "の物理攻撃、物理防御、魔法攻撃、魔法防御がＵＰした！\r\n";
                    case 49: // Rise of Image
                        return this.name + "：『空魔法』  －　『RiseOfImage』　心パラメタが {0} ＵＰ\r\n";
                    case 83: // Promised Knowledge
                        return this.name + "：『水魔法』  －　『PromisedKnowledge』　知パラメタが {0} ＵＰ\r\n";

                    case 48: // Dispel Magic
                        return this.name + "：『空魔法』  －　『DispelMagic』\r\n";
                    case 84: // Tranquility
                        return this.name + "：『空魔法』  －　『Tranquility』\r\n";
                    case 37: // Damnation
                        return this.name + "：『闇魔法』　－　『Damnation』\r\n";
                    case 35: // Black Contract
                        return this.name + "：『闇魔法』　－　『BlackContract』\r\n";
                    case 81: // Absolute Zero
                        return this.name + "：『水魔法』　－　『AbsoluteZero』　対象はライフ回復不可、スペル詠唱不可、スキル使用不可、防御不可になる。\r\n";

                    case 43: // Aether Drive
                        return this.name + "：『AetherDrive』  周囲全体に空想物理力がみなぎる。\r\n";
                    case 39: // Immortal Rave
                        return this.name + "：『ImmortalRave』 " + this.name + "の周りに３つの炎が宿った。\r\n";
                    case 40: // Gale Wind
                        return this.name + "：『GaleWind』  もう一人のBystanderが現れた。\r\n";
                    case 24: // Glory
                        return this.name + "：『Glory』  直接攻撃＋FreshHeal連携のオーラ\r\n";
                    case 14: // FlameAuraによる追加攻撃
                        return "『FlameAura』が襲いかかる！ {0} ダメージ\r\n";
                    case 65: // Carnage Rush 1
                        return this.name + "：『CarnageRush』 『壱』 {0}ダメージ   \r\n";
                    case 66: // Carnage Rush 2
                        return this.name + "：『CarnageRush』 『弐』 {0}ダメージ   \r\n";
                    case 67: // Carnage Rush 3
                        return this.name + "：『CarnageRush』 『参』 {0}ダメージ   \r\n";
                    case 68: // Carnage Rush 4
                        return this.name + "：『CarnageRush』 『四』 {0}ダメージ   \r\n";
                    case 69: // Carnage Rush 5
                        return this.name + "：『CarnageRush』 『終』 {0}のダメージ\r\n";
                    case 53: // 対象不適切
                        return this.name + "：既に『対象』は『消滅』している。\r\n";
                    case 9: // Fresh Healによるライフ回復
                        return this.name + "：{0} 『回復』\r\n";

                    case 57: // Mirror Image
                        return this.name + "『水魔法』　－　『MirrorImage』 {0}の周囲に青い空間が発生した。\r\n";
                    case 58: // Mirror Image 2
                        return this.name + "は魔法をはじき返そうとした。 {0} ダメージを {1} に反射した！\r\n";
                    case 59: // Mirror Image 3
                        return this.name + "は魔法をはじき返そうとした。 【しかし、強力な威力ではね返せない！】\r\n";
                    case 60: // Deflection
                        return this.name + "『空魔法』　－　『AbsoluteZero』 {0}の周囲に白い空間が発生した。\r\n";
                    case 61: // Deflection 2
                        return this.name + "は物理ダメージをはじき返そうとした！ {0} ダメージを {1} に反射した！\r\n";
                    case 62: // Deflection 3
                        return this.name + "は物理ダメージをはじき返そうとした！ 【しかし、強力な威力ではね返せない！】\r\n";
                    case 82: // BUFFUP効果が望めない場合
                        return "しかし、既にそのパラメタは上昇しているため、効果がなかった。\r\n";

                    case 27: // Dark Blast
                        return "『DarkBlast』が襲いかかる！ {0} へ {1} のダメージ\r\n";
                    case 23: // Holy Shock
                        return "『HolyShock』が襲いかかる！ {0} へ {1} のダメージ\r\n";
                    case 30: // Ice Needle
                        return "『IceNeedle』が襲いかかる！ {0} へ {1} のダメージ\r\n";
                    case 10: // Fire Ball
                        return "『FireBall』が襲いかかる！ {0} へ {1} のダメージ\r\n";
                    case 33: // Word of Power
                        return "『WordOfPower』が襲いかかる！ {0} へ {1} のダメージ\r\n";
                    case 11: // Flame Strike
                        return "『FlameStrike』が襲いかかる！ {0} へ {1} のダメージ\r\n";
                    case 31: // Frozen Lance
                        return "『FrozenLance』が襲いかかる！ {0} へ {1} のダメージ\r\n";
                    case 12: // Volcanic Wave
                        return "『VolcanicWave』が襲いかかる！ {0} へ {1} のダメージ\r\n";
                    case 34: // White Out
                        return "『WhiteOut』が襲いかかる！ {0} へ {1} のダメージ\r\n";
                    case 28: // Lava Annihilation
                        return "『LavaAnnihilation』が襲いかかる！ {0} へ {1} のダメージ\r\n";

                    case 41: // Word of Life
                        return this.name + "：『WordOfLife』　大自然からの強い息吹を感じ取れるようになった。\r\n";
                    case 25: // CelestialNova 1
                        return this.name + "：『CelestialNova』　{0} 『回復』\r\n";

                    case 36: // Flame Aura詠唱
                        return this.name + "：『火魔法』  －　『FlameAura』 直接攻撃に炎の追加効果が付与される。\r\n";

                    case 106: // Nothing Of Nothingness 1
                        return this.name + "：『無心スキル』  －　『NothingOfNothingness』　無色のオーラが宿り始める！ \r\n";
                    case 107: // Nothing Of Nothingness 2
                        return this.name + "は無効化魔法を無効化、　無効化するスキルを無効化するようになった！\r\n";

                    case 115: // 通常攻撃のヒット
                        return this.name + "からの攻撃。 {0} へ {1} のダメージ\r\n";
                    case 117: // StraightSmashなどのスキルクリティカル
                        return this.name + "：『クリティカルヒット』\r\n";
                }
            }
            #endregion
            #region "不特定多数"
            else
            {
                switch (sentenceNumber)
                {
                    case 0: // スキル不足
                        return this.name + "のスキルポイントが足りない！\r\n";
                    case 1: // Straight Smash
                        return this.name + "はストレート・スマッシュを繰り出した。\r\n";
                    case 2: // Double Slash 1
                        return this.name + "  １回目の攻撃！\r\n";
                    case 3: // Double Slash 2
                        return this.name + "  ２回目の攻撃！\r\n";
                    case 4: // Painful Insanity
                        return this.name + "は【心眼奥義】PainfulInsanityを繰り出した！\r\n";
                    case 5: // empty skill
                        return this.name + "はスキル選択し忘れていた！\r\n";
                    case 6: // 絡みつくフランシスの必殺を食らった時
                        return this.name + "は必死に堪えている・・・\r\n";
                    case 7: // Lizenosの必殺を食らった時
                        return this.name + "は必死に堪えている・・・\r\n";
                    case 8: // Minfloreの必殺を食らった時
                        return this.name + "は必死に堪えている・・・\r\n";
                    case 9: // Fresh Healによるライフ回復
                        return this.name + "は{0} 回復した。\r\n";
                    case 10: // Fire Ball
                        return this.name + "はFireBallを唱えた！ {0} へ {1} のダメージ\r\n";
                    case 11: // Flame Strike
                        return this.name + "はFlameStrikeを唱えた！ {0} へ {1} のダメージ\r\n";
                    case 12: // Volcanic Wave
                        return this.name + "はVolcanicWaveを唱えた！ {0} へ {1} のダメージ\r\n";
                    case 13: // 通常攻撃クリティカルヒット
                        return this.name + "からのクリティカルヒット！ {0} へ {1} のダメージ\r\n";
                    case 14: // FlameAuraによる追加攻撃
                        return this.name + "のFlameAuraによる {0} の追加ダメージ\r\n";
                    case 15: // 遠見の青水晶を戦闘中に使ったとき
                        return this.name + "はやみくもにアイテムを使った。\r\n";
                    case 16: // 効果を発揮しないアイテムを戦闘中に使ったとき
                        return this.name + "はやみくもにアイテムを使った。\r\n";
                    case 17: // 魔法でマナ不足
                        return this.name + "のマナが足りない！\r\n";
                    case 18: // Protection
                        return this.name + "はProtectionを唱えた！ 物理防御力：ＵＰ\r\n";
                    case 19: // Absorb Water
                        return this.name + "はAbsorbWaterを唱えた！ 魔法防御力：ＵＰ。\r\n";
                    case 20: // Saint Power
                        return this.name + "はSaintPowerを唱えた！ 物理攻撃力：ＵＰ\r\n";
                    case 21: // Shadow Pact
                        return this.name + "はShadowPactを唱えた！ 魔法攻撃力：ＵＰ\r\n";
                    case 22: // Bloody Vengeance
                        return this.name + "はBloodyVengeanceを唱えた！ 力パラメタが {0} ＵＰ\r\n";
                    case 23: // Holy Shock
                        return this.name + "はHolyShockを唱えた！ {0} へ {1} のダメージ\r\n";
                    case 24: // Glory
                        return this.name + "はGloryを唱えた！ 直接攻撃＋FreshHeal連携のオーラ\r\n";
                    case 25: // CelestialNova 1
                        return this.name + "はCelestialNovaを唱えた！ {0} 回復。\r\n";
                    case 26: // CelestialNova 2
                        return this.name + "はCelestialNovaを唱えた！ {0} のダメージ\r\n";
                    case 27: // Dark Blast
                        return this.name + "はDarkBlastを唱えた！ {0} へ {1} のダメージ\r\n";
                    case 28: // Lava Annihilation
                        return this.name + "はLavaAnnihilationを唱えた！ {0} へ {1} のダメージ\r\n";
                    case 29: // Devouring Plague
                        return this.name + "はDevouringPlagueを唱えた！ {0} ライフを吸い取った\r\n";
                    case 30: // Ice Needle
                        return this.name + "はIceNeedleを唱えた！ {0} へ {1} のダメージ\r\n";
                    case 31: // Frozen Lance
                        return this.name + "はFrozenLanceを唱えた！ {0} へ {1} のダメージ\r\n";
                    case 32: // Tidal Wave
                        return this.name + "はTidalWaveを唱えた！ {0} のダメージ\r\n";
                    case 33: // Word of Power
                        return this.name + "はWordOfPowerを唱えた！ {0} へ {1} のダメージ\r\n";
                    case 34: // White Out
                        return this.name + "はWhiteOutを唱えた！ {0} へ {1} のダメージ\r\n";
                    case 35: // Black Contract
                        return this.name + "はBlackContractを唱えた！ " + this.name + "のスキル、魔法コストは０になる。\r\n";
                    case 36: // Flame Aura詠唱
                        return this.name + "はFlameAuraを唱えた！ 直接攻撃に炎の追加効果が付与される。\r\n";
                    case 37: // Damnation
                        return this.name + "はDamnationを唱えた！ 死淵より出でる黒が空間を歪ませる。\r\n";
                    case 38: // Heat Boost
                        return this.name + "はHeatBoostを唱えた！ 技パラメタが {0} ＵＰ。\r\n";
                    case 39: // Immortal Rave
                        return this.name + "はImmortalRaveを唱えた！ " + this.name + "の周りに３つの炎が宿った。\r\n";
                    case 40: // Gale Wind
                        return this.name + "はGaleWindを唱えた！ もう一人の" + this.name + "が現れた。\r\n";
                    case 41: // Word of Life
                        return this.name + "はWordOfLifeを唱えた！ 大自然からの強い息吹を感じ取れるようになった。\r\n";
                    case 42: // Word of Fortune
                        return this.name + "はWordOfFortuneを唱えた！ 決死のオーラが湧き上がった。\r\n";
                    case 43: // Aether Drive
                        return this.name + "はAetherDriveを唱えた！ 周囲全体に空想物理力がみなぎる。\r\n";
                    case 44: // Eternal Presence 1
                        return this.name + "はEternalPresenceを唱えた！ " + this.name + "の周りに新しい法則が構築される。\r\n";
                    case 45: // Eternal Presence 2
                        return this.name + "の物理攻撃、物理防御、魔法攻撃、魔法防御がＵＰした！\r\n";
                    case 46: // One Immunity
                        return this.name + "はOneImmunityを唱えた！ " + this.name + "の周囲に目に見えない障壁が発生。\r\n";
                    case 47: // Time Stop
                        return this.name + "はTimeStopを唱えた！ 相手の時空を引き裂き時間停止させた。\r\n";
                    case 48: // Dispel Magic
                        return this.name + "はDispelMagicを唱えた！ \r\n";
                    case 49: // Rise of Image
                        return this.name + "はRiseOfImageを唱えた！ 心パラメタが {0} ＵＰ\r\n";
                    case 50: // 空詠唱
                        return this.name + "は詠唱に失敗した！\r\n";
                    case 51: // Inner Inspiration
                        return this.name + "はInnerInspirationを繰り出した！ {0} スキルポイント回復\r\n";
                    case 52: // Resurrection 1
                        return this.name + "はResurrectionを唱えた！！\r\n";
                    case 53: // Resurrection 2
                        return this.name + "は対象を間違えていた！！\r\n";
                    case 54: // Resurrection 3
                        return this.name + "は対象を間違えていた！！\r\n";
                    case 55: // Resurrection 4
                        return this.name + "は対象を間違えていた！！\r\n";
                    case 56: // Stance Of Standing
                        return this.name + "はStanceOfStandingを繰り出した！\r\n";
                    case 57: // Mirror Image
                        return this.name + "はMirrorImageを唱えた！ {0}の周囲に青い空間が発生した。\r\n";
                    case 58: // Mirror Image 2
                        return this.name + "は魔法をはじき返そうとした。 {0} ダメージを {1} に反射した！\r\n";
                    case 59: // Mirror Image 3
                        return this.name + "は魔法をはじき返そうとした。 【しかし、強力な威力ではね返せない！】\r\n";
                    case 60: // Deflection
                        return this.name + "はDeflectionを唱えた！ {0}の周囲に白い空間が発生した。\r\n";
                    case 61: // Deflection 2
                        return this.name + "は物理ダメージをはじき返そうとした！ {0} ダメージを {1} に反射した！\r\n";
                    case 62: // Deflection 3
                        return this.name + "は物理ダメージをはじき返そうとした！ 【しかし、強力な威力ではね返せない！】\r\n";
                    case 63: // Truth Vision
                        return this.name + "はTruthVisionを繰り出した！ " + this.name + "は対象のパラメタＵＰを無視するようになった。\r\n";
                    case 64: // Stance Of Flow
                        return this.name + "はStanceOfFlowを繰り出した！ " + this.name + "は次３ターン、必ず後攻を取るように構えた。\r\n";
                    case 65: // Carnage Rush 1
                        return this.name + "はCarnageRushを繰り出した！ １発目{0}ダメージ・・・   ";
                    case 66: // Carnage Rush 2
                        return " ２発目{0}ダメージ   ";
                    case 67: // Carnage Rush 3
                        return " ３発目{0}ダメージ   ";
                    case 68: // Carnage Rush 4
                        return " ４発目{0}ダメージ   ";
                    case 69: // Carnage Rush 5
                        return " ５発目{0}のダメージ\r\n";
                    case 70: // Crushing Blow
                        return this.name + "はCrushingBlowを繰り出した！  {0} へ {1} のダメージ。\r\n";
                    case 71: // リーベストランクポーション戦闘使用時
                        return this.name + "はアイテムを使用してきた！\r\n";
                    case 72: // Enigma Sence
                        return this.name + "はEnigmaSenceを繰り出した！\r\n";
                    case 73: // Soul Infinity
                        return this.name + "はSoulInfinityを繰り出した！\r\n";
                    case 74: // Kinetic Smash
                        return this.name + "はKineticSmashを繰り出した！\r\n";
                    case 75: // Silence Shot (Altomo専用)
                        return "";
                    case 76: // Silence Shot、AbsoluteZero沈黙による詠唱失敗
                        return this.name + "は魔法詠唱に失敗した！！\r\n";
                    case 77: // Cleansing
                        return this.name + "はCleansingを唱えた！\r\n";
                    case 78: // Pure Purification
                        return this.name + "はPurePurificationを繰り出した！\r\n";
                    case 79: // Void Extraction
                        return this.name + "はVoidExtractionを繰り出した！ " + this.name + "の {0} が {1}ＵＰ！\r\n";
                    case 80: // アカシジアの実使用時
                        return this.name + "はアイテムを使用してきた！\r\n";
                    case 81: // Absolute Zero
                        return this.name + "はAbsoluteZeroを唱えた！ 氷吹雪効果により、ライフ回復不可、スペル詠唱不可、スキル使用不可、防御不可となった。\r\n";
                    case 82: // BUFFUP効果が望めない場合
                        return "しかし、既にそのパラメタは上昇しているため、効果がなかった。\r\n";
                    case 83: // Promised Knowledge
                        return this.name + "はPromiesdKnowledgeを唱えた！ 知パラメタが {0} ＵＰ\r\n";
                    case 84: // Tranquility
                        return this.name + "はTranquilityを唱えた！\r\n";
                    case 85: // High Emotionality 1
                        return this.name + "はHighEmotionalityを繰り出した！\r\n";
                    case 86: // High Emotionality 2
                        return this.name + "の力、技、知、心パラメタがＵＰした！\r\n";
                    case 87: // AbsoluteZeroでスキル使用失敗
                        return this.name + "は絶対零度効果により、スキルの使用に失敗した！\r\n";
                    case 88: // AbsoluteZeroによる防御失敗
                        return this.name + "は絶対零度効果により、防御できないままでいる！ \r\n";
                    case 89: // Silent Rush 1
                        return this.name + "はSilentRushを繰り出した！ １発目 {0}ダメージ・・・　";
                    case 90: // Silent Rush 2
                        return "２発目 {0}ダメージ   ";
                    case 91: // Silent Rush 3
                        return "３発目 {0}のダメージ\r\n";
                    case 92: // BUFFUP以外の永続効果が既についている場合
                        return "しかし、既にその効果は付与されている。\r\n";
                    case 93: // Anti Stun
                        return this.name + "はAntiStunを繰り出した！ " + this.name + "はスタン効果への耐性が付いた\r\n";
                    case 94: // Anti Stunによるスタン回避
                        return this.name + "はAntiStun効果によりスタンを回避した。\r\n";
                    case 95: // Stance Of Death
                        return this.name + "はStanceOfDeathを繰り出した！ " + this.name + "は致死を１度回避できるようになった\r\n";
                    case 96: // Oboro Impact 1
                        return this.name + "は【究極奥義】OboroImpactを繰り出した！\r\n";
                    case 97: // Oboro Impact 2
                        return "{0}へ{1}のダメージ\r\n";
                    case 98: // Catastrophe 1
                        return this.name + "は【究極奥義】Catastropheを繰り出した！\r\n";
                    case 99: // Catastrophe 2
                        return this.name + " {0}のダメージ\r\n";
                    case 100: // Stance Of Eyes
                        return this.name + "はStanceOfEyesを繰り出した！ " + this.name + "は、相手の行動に備えている・・・\r\n";
                    case 101: // Stance Of Eyesによるキャンセル時
                        return this.name + "は相手のモーションを見切って、行動キャンセルした！\r\n";
                    case 102: // Stance Of Eyesによるキャンセル失敗時
                        return this.name + "は相手のモーションを見切ろうとしたが、相手のモーションを見切れなかった！\r\n";
                    case 103: // Negate
                        return this.name + "はNegateを繰り出した！ " + this.name + "は相手のスペル詠唱に備えている・・・\r\n";
                    case 104: // Negateによるスペル詠唱キャンセル時
                        return this.name + "は相手のスペル詠唱を弾いた！\r\n";
                    case 105: // Negateによるスペル詠唱キャンセル失敗時
                        return this.name + "は相手のスペル詠唱を弾こうとしたが、相手のスペル詠唱を見切れなかった！\r\n";
                    case 106: // Nothing Of Nothingness 1
                        return this.name + "は【究極奥義】NothingOfNothingnessを繰り出した！ " + this.name + "に無色のオーラが宿り始める！ \r\n";
                    case 107: // Nothing Of Nothingness 2
                        return this.name + "は無効化魔法を無効化、　無効化するスキルを無効化するようになった！\r\n";
                    case 108: // Genesis
                        return this.name + "はGenesisを唱えた！ " + this.name + "は前回の行動を自分へと投影させた！\r\n";
                    case 109: // Cleansing詠唱失敗時
                        return this.name + "は調子が悪いため、Cleansingの詠唱に失敗した。\r\n";
                    case 110: // CounterAttackを無視した時
                        return this.Name + "はCounterAttackの構えを無視した。\r\n";
                    case 111: // 神聖水使用時
                        return this.name + "はアイテムを使用してきた！\r\n";
                    case 112: // 神聖水、既に使用済みの場合
                        return this.name + "が使ったアイテムは効果がなかった！\r\n";
                    case 113: // CounterAttackによる反撃メッセージ
                        return this.name + "はカウンターアタックを繰り出した！\r\n";
                    case 114: // CounterAttackに対する反撃がNothingOfNothingnessによって防がれた時
                        return this.name + "はカウンターできなかった！\r\n";
                    case 115: // 通常攻撃のヒット
                        return this.name + "からの攻撃。 {0} へ {1} のダメージ\r\n";
                    case 116: // 防御を無視して攻撃する時
                        return this.name + "は防御を無視して攻撃してきた！\r\n";
                    case 117: // StraightSmashなどのスキルクリティカル
                        return this.name + "からのクリティカルヒット！\r\n";
                    case 118: // 戦闘時、リヴァイヴポーションによる復活のかけ声
                        return this.name + "はリヴァイヴポーションを使用した！";
                    case 119: // Absolute Zeroによりライフ回復できない場合
                        return this.name + "は絶対零度効果によりライフ回復できない！\r\n";
                    case 120: // 魔法攻撃のヒット
                        return "{0} へ {1} のダメージ\r\n";
                    case 121: // Absolute Zeroによりマナ回復できない場合
                        return this.name + "は凍てつく寒さによりマナ回復ができない。\r\n";
                    case 122: // 「ためる」行動時
                        return this.name + "は魔力を蓄えた。\r\n";
                    case 123: // 「ためる」行動で溜まりきっている場合
                        return this.name + "は魔力を蓄えようとしたが、これ以上蓄えられないでいる。\r\n";
                    case 124: // StraightSmashのダメージ値
                        return this.name + "のストレート・スマッシュがヒット。 {0} へ {1}のダメージ\r\n";
                    case 125: // アイテム使用ゲージが溜まってないとき
                        return this.name + "はアイテムゲージが溜まってない間、アイテムを使えないでいる。\r\n";
                    case 126: // FlashBlase
                        return this.name + "：はFlashBlazeを唱えた！ {0} へ {1} のダメージ\r\n";
                    case 127: // 複合魔法でインスタント不足
                        return this.name + "のインスタント値が足りない！\r\n";
                    case 128: // 複合魔法はインスタントタイミングでうてない
                        return this.name + "はインスタントタイミングで発動できないでいる。\r\n";
                    case 129: // PsychicTrance
                        return this.name + "はPsychicTranceを唱えた！\r\n";
                    case 130: // BlindJustice
                        return this.name + "はBlindJusticeを唱えた！\r\n";
                    case 131: // TranscendentWish
                        return this.name + "はTranscendentWishを唱えた！\r\n";
                    case 132: // LightDetonator
                        return this.name + "はLightDetonatorを唱えた！\r\n";
                    case 133: // AscendantMeteor
                        return this.name + "はAscendantMeteorを唱えた！\r\n";
                    case 134: // SkyShield
                        return this.name + "はSkyShieldを唱えた！\r\n";
                    case 135: // SacredHeal
                        return this.name + "はSacredHealを唱えた！\r\n";
                    case 136: // EverDroplet
                        return this.name + "はEverDropletを唱えた！\r\n";
                    case 137: // FrozenAura
                        return this.name + "はFrozenAuraを唱えた！\r\n";
                    case 138: // ChillBurn
                        return this.name + "はChillBurnを唱えた！\r\n";
                    case 139: // ZetaExplosion
                        return this.name + "はZetaExplosionを唱えた！\r\n";
                    case 140: // FrozenAura追加効果ダメージで
                        return this.name + "のFrozenAuraによる {0} の追加ダメージ\r\n";
                    case 141: // StarLightning
                        return this.name + "はStarLightningを唱えた！\r\n";
                    case 142: // WordOfMalice
                        return this.name + "はWordOfMaliceを唱えた！\r\n";
                    case 143: // BlackFire
                        return this.name + "はBlackFireを唱えた！\r\n";
                    case 144: // EnrageBlast
                        return this.name + "はEnrageBlastを唱えた！\r\n";
                    case 145: // Immolate
                        return this.name + "はImmolateを唱えた！\r\n";
                    case 146: // VanishWave
                        return this.name + "はVanishWaveを唱えた！\r\n";
                    case 147: // WordOfAttitude
                        return this.name + "はWordOfAttitudeを唱えた！\r\n";
                    case 148: // HolyBreaker
                        return this.name + "はHolyBreakerを唱えた！\r\n";
                    case 149: // DarkenField
                        return this.name + "はDarkenFieldを唱えた！\r\n";
                    case 150: // SeventhMagic
                        return this.name + "はSeventhMagicを唱えた！\r\n";
                    case 151: // BlueBullet
                        return this.name + "はBlueBulletを唱えた！\r\n";
                    case 152: // NeutralSmash
                        return this.name + "はNeutralSmashを繰り出した！\r\n";
                    case 153: // SwiftStep
                        return this.name + "はSwiftStepを繰り出した！\r\n";
                    case 154: // CircleSlash
                        return this.name + "はCircleSlashを繰り出した！\r\n";
                    case 155: // RumbleShout
                        return this.name + "はRumbleShoutを繰り出した！\r\n";
                    case 156: // SmoothingMove
                        return this.name + "はSmoothingMoveを繰り出した！\r\n";
                    case 157: // FutureVision
                        return this.name + "はFutureVisionを繰り出した！\r\n";
                    case 158: // ReflexSpirit
                        return this.name + "はReflexSpiritを繰り出した！\r\n";
                    case 159: // SharpGlare
                        return this.name + "はSharpGlareを繰り出した！\r\n";
                    case 160: // TrustSilence
                        return this.name + "はTrustSilenceを繰り出した！\r\n";
                    case 161: // SurpriseAttack
                        return this.name + "はSurpriseAttackを繰り出した！\r\n";
                    case 162: // PsychicWave
                        return this.name + "はPsychicWaveを繰り出した！\r\n";
                    case 163: // Recover
                        return this.name + "はRecoverを繰り出した！\r\n";
                    case 164: // ViolentSlash
                        return this.name + "はViolentSlashを繰り出した！\r\n";
                    case 165: // OuterInspiration
                        return this.name + "はOuterInspirationを繰り出した！\r\n";
                    case 166: // StanceOfSuddenness
                        return this.name + "はStanceOfSuddennessを繰り出した！\r\n";
                    case 167: // インスタント対象で発動不可
                        return this.name + "は対象のインスタントコマンドが無く戸惑っている。\r\n";
                    case 168: // StanceOfMystic
                        return this.name + "はStanceOfMysticを繰り出した！\r\n";
                    case 169: // HardestParry
                        return this.name + "はHardestParryを繰り出した！\r\n";
                    case 170: // ConcussiveHit
                        return this.name + "はConcussiveHitを繰り出した！\r\n";
                    case 171: // Onslaught hit
                        return this.name + "はOnslaughtHitを繰り出した！\r\n";
                    case 172: // Impulse hit
                        return this.name + "はImpulseHitを繰り出した！\r\n";
                    case 173: // Fatal Blow
                        return this.name + "はFatalBlowを繰り出した！\r\n";
                    case 174: // Exalted Field
                        return this.name + "はExaltedFieldを唱えた！\r\n";
                    case 175: // Rising Aura
                        return this.name + "はRisingAuraを繰り出した！\r\n";
                    case 176: // Ascension Aura
                        return this.name + "はAscensioAuraを繰り出した！\r\n";
                    case 177: // Angel Breath
                        return this.name + "はAngelBreathを唱えた！\r\n";
                    case 178: // Blazing Field
                        return this.name + "はBlazingFieldを唱えた！\r\n";
                    case 179: // Deep Mirror
                        return this.name + "はDeepMirrorを唱えた！\r\n";
                    case 180: // Abyss Eye
                        return this.name + "はAbyssEyeを唱えた！\r\n";
                    case 181: // Doom Blade
                        return this.name + "はDoomBladeを繰り出した！\r\n";
                    case 182: // Piercing Flame
                        return this.name + "はPiercingFlameを唱えた！\r\n";
                    case 183: // Phantasmal Wind
                        return this.name + "はPhantasmalWindを唱えた！\r\n";
                    case 184: // Paradox Image
                        return this.name + "はParadoxImageを唱えた！\r\n";
                    case 185: // Vortex Field
                        return this.name + "はVortexFieldを唱えた！\r\n";
                    case 186: // Static Barrier
                        return this.name + "はStaticBarrierを唱えた！\r\n";
                    case 187: // Unknown Shock
                        return this.name + "はUnknownShockを繰り出した！\r\n";
                    case 188: // SoulExecution
                        return this.name + "は【究極奥義】SoulExecutionを繰り出した！\r\n";
                    case 189: // SoulExecution hit 01
                        return this.name + "１発目！\r\n";
                    case 190: // SoulExecution hit 02
                        return this.name + "２発目！\r\n";
                    case 191: // SoulExecution hit 03
                        return this.name + "３発目！\r\n";
                    case 192: // SoulExecution hit 04
                        return this.name + "４発目！\r\n";
                    case 193: // SoulExecution hit 05
                        return this.name + "５発目！\r\n";
                    case 194: // SoulExecution hit 06
                        return this.name + "６発目！\r\n";
                    case 195: // SoulExecution hit 07
                        return this.name + "７発目！\r\n";
                    case 196: // SoulExecution hit 08
                        return this.name + "８発目！\r\n";
                    case 197: // SoulExecution hit 09
                        return this.name + "９発目！\r\n";
                    case 198: // SoulExecution hit 10
                        return this.name + "１０発目！\r\n";
                    case 199: // Nourish Sense
                        return this.name + "はNourishSenseを繰り出した！\r\n";
                    case 200: // Mind Killing
                        return this.name + "はMindKillingを繰り出した！\r\n";
                    case 201: // Vigor Sense
                        return this.name + "はVigorSenseを繰り出した！\r\n";
                    case 202: // ONE Authority
                        return this.name + "はOneAuthorityを繰り出した！\r\n";
                    case 203: // 集中と断絶
                        return this.name + "は【集中と断絶】を発動した！\r\n";
                    case 204: // 【元核】発動済み
                        return this.name + "は既に【元核】を使用しており、これ以上発動が行えなかった。\r\n";
                    case 205: // 【元核】通常行動選択時
                        return this.name + "の【元核】はインスタントタイミング限定であり、通常行動に選択できなかった。\r\n";
                    case 206: // Sigil Of Homura
                        return this.name + "はSigilOfHomuraを唱えた！\r\n";
                    case 207: // Austerity Matrix
                        return this.name + "はAusterityMatrixを唱えた！\r\n";
                    case 208: // Red Dragon Will
                        return this.name + "はRedDragonWillを唱えた！\r\n";
                    case 209: // Blue Dragon Will
                        return this.name + "はBlueDragonWillを唱えた！\r\n";
                    case 210: // Eclipse End
                        return this.name + "はEclipseEndを唱えた！\r\n";
                    case 211: // Sin Fortune
                        return this.name + "はSinFortuneを唱えた！\r\n";
                    case 212: // AfterReviveHalf
                        return this.name + "に死耐性（ハーフ）が付与された！\r\n";
                    case 213: // DemonicIgnite
                        return this.name + "はDemonicIgniteを唱えた！\r\n";
                    case 214: // Death Deny
                        return this.name + "はDeathDenyを唱えた！\r\n";
                    case 215: // Stance of Double
                        return this.name + "はStanceOfDoubleを放った！　" + this.name + "は前回行動を示す自分の分身を発生させた！\r\n";
                    case 216: // 最終戦ライフカウント消費
                        return this.name + "の生命が削りとられる代わりに、その場で生き残った！\r\n";
                    case 217: // 最終戦ライフカウント消滅時
                        return this.name + "の生命は完全に消滅させらた・・・\r\n";

                    case 4001: // 通常攻撃を選択
                    case 4002: // 防御を選択
                    case 4003: // 待機を選択
                    case 4004: // フレッシュヒールを選択
                    case 4005: // プロテクションを選択
                    case 4006: // ファイア・ボールを選択
                    case 4007: // フレイム・オーラを選択
                    case 4008: // ストレート・スマッシュを選択
                    case 4009: // ダブル・スマッシュを選択
                    case 4010: // スタンス・オブ・スタンディングを選択
                    case 4011: // アイス・ニードルを選択
                    case 4012:
                    case 4013:
                    case 4014:
                    case 4015:
                    case 4016:
                    case 4017:
                    case 4018:
                    case 4019:
                    case 4020:
                    case 4021:
                    case 4022:
                    case 4023:
                    case 4024:
                    case 4025:
                    case 4026:
                    case 4027:
                    case 4028:
                    case 4029:
                    case 4030:
                    case 4031:
                    case 4032:
                    case 4033:
                    case 4034:
                    case 4035:
                    case 4036:
                    case 4037:
                    case 4038:
                    case 4039:
                    case 4040:
                    case 4041:
                    case 4042:
                    case 4043:
                    case 4044:
                    case 4045:
                    case 4046:
                    case 4047:
                    case 4048:
                    case 4049:
                    case 4050:
                    case 4051:
                    case 4052:
                    case 4053:
                    case 4054:
                    case 4055:
                    case 4056:
                    case 4057:
                    case 4058:
                    case 4059:
                    case 4060:
                    case 4061:
                    case 4062:
                    case 4063:
                    case 4064:
                    case 4065:
                    case 4066:
                    case 4067:
                    case 4068:
                    case 4069:
                    case 4070:
                    case 4071:
                        return this.name + "は" + this.ActionLabel.Text + "を選択した。\r\n";
                    case 4072:
                        return this.name + "は敵を対象にするのをためらっている。\r\n";
                    case 4073:
                        return this.name + "は敵を対象にするのをためらっている。\r\n";
                    case 4074:
                        return this.name + "は味方を対象にするのをためらっている。\r\n";
                    case 4075:
                        return this.name + "は味方を対象にするのをためらっている。\r\n";
                    case 4076:
                        return this.name + "は味方に攻撃するのをためらっている。\r\n";
                    case 4077: // 「ためる」コマンド
                        return this.name + "は魔力をため込み始めた。\r\n";
                    case 4078: // 武器発動「メイン」
                        return this.name + "はメイン武器の効果発動を選択した。\r\n";
                    case 4079: // 武器発動「サブ」
                        return this.name + "はサブ武器の効果発動を選択した。\r\n";
                    case 4080: // アクセサリ１発動
                        return this.name + "はアクセサリ１の効果発動を選択した。\r\n";
                    case 4081: // アクセサリ２発動
                        return this.name + "はアクセサリ２の効果発動を選択した。\r\n";

                    case 4082: // FlashBlaze
                        return this.name + "はフラッシュブレイズを選択した。\r\n";

                    // 武器攻撃
                    case 5001:
                        return this.name + "はエアロ・スラッシュを放った。 {0} へ {1} のダメージ\r\n";
                    case 5002:
                        return this.name + "は{0}ライフ回復した。 \r\n";
                    case 5003:
                        return this.name + "は{0}マナ回復した \r\n";
                    case 5004:
                        return this.name + "はアイシクル・スラッシュを放った。 {0} へ {1} のダメージ\r\n";
                    case 5005:
                        return this.name + "はエレクトロ・ブローを放った。 {0} へ {1} のダメージ\r\n";
                    case 5006:
                        return this.name + "はブルー・ライトニングを放った。 {0} へ {1} のダメージ\r\n";
                    case 5007:
                        return this.name + "はバーニング・クレイモアを放った。 {0} へ {1} のダメージ\r\n";
                    case 5008:
                        return this.name + "は赤蒼授の杖から蒼の炎を放った。 {0} へ {1} のダメージ\r\n";
                    case 5009:
                        return this.name + "は{0}スキルポイント回復した。\r\n";
                    case 5010:
                        return this.name + "が装着している指輪が強烈に蒼い光を放った！ {0} へ {1} のダメージ\r\n";
                }
            }
            #endregion
            return this.name + "：\r\n";
        }

        public void RecoverStunning()
        {
            CurrentStunning = 0;
            this.DeBuff(this.pbStun);
        }
        public void RecoverParalyze()
        {
            CurrentParalyze = 0;
            this.DeBuff(this.pbParalyze);
        }
        public void RecoverFrozen()
        {
            CurrentFrozen = 0;
            this.DeBuff(this.pbFrozen);
        }
        public void RecoverPoison()
        {
            CurrentPoison = 0;
            currentPoisonValue = 0; // 後編追加
            this.DeBuff(this.pbPoison);
        }
        public void RecoverSlow()
        {
            CurrentSlow = 0;
            this.DeBuff(this.pbSlow);
        }
        public void RecoverBlind()
        {
            CurrentBlind = 0;
            this.DeBuff(this.pbBlind);
        }
        public void RecoverSilence()
        {
            CurrentSilence = 0;
            this.DeBuff(this.pbSilence);
        }
        public void RecoverSlip()
        {
            CurrentSlip = 0;
            this.DeBuff(this.pbSlip);
        }
        public void RecoverTemptation()
        {
            currentTemptation = 0;
            this.DeBuff(this.pbTemptation);
        }
        public void RecoverPreStunning()
        {
            currentPreStunning = 0;
            this.DeBuff(this.pbPreStunning);
        }
        public void RecoverNoResurrection()
        {
            CurrentNoResurrection = 0;
            this.DeBuff(this.pbNoResurrection);
        }

        // s 後編追加（以下、前編とメソッドが、かぶらないようにしたい。もしかぶると、Debuffメソッドで例外エラーとなるためそれを避けたい）
        public void RemoveProtection()
        {
            this.CurrentProtection = 0;
            this.DeBuff(this.pbProtection);
        }
        public void RemoveSaintPower()
        {
            this.CurrentSaintPower = 0;
            this.DeBuff(this.pbSaintPower);
        }
        public void RemoveGlory()
        {
            this.CurrentGlory = 0;
            this.DeBuff(this.pbGlory);
        }
        public void RemoveShadowPact()
        {
            this.CurrentShadowPact = 0;
            this.DeBuff(this.pbShadowPact);
        }
        public void RemoveBlackContract()
        {
            this.CurrentBlackContract = 0;
            this.DeBuff(this.pbBlackContract);
        }
        public void RemoveDamnation()
        {
            this.CurrentDamnation = 0;
            this.DeBuff(this.pbDamnation);
        }
        public void RemoveFlameAura()
        {
            this.CurrentFlameAura = 0;
            this.DeBuff(this.pbFlameAura);
        }
        public void RemoveImmortalRave()
        {
            this.CurrentImmortalRave = 0;
            this.DeBuff(this.pbImmortalRave);
        }
        public void RemoveAbsorbWater()
        {
            this.CurrentAbsorbWater = 0;
            this.DeBuff(this.pbAbsorbWater);
        }
        public void RemoveMirrorImage()
        {
            this.CurrentMirrorImage = 0;
            this.DeBuff(this.pbMirrorImage);
        }
        public void RemoveAbsoluteZero()
        {
            this.CurrentAbsoluteZero = 0;
            this.DeBuff(this.pbAbsoluteZero);
        }
        public void RemoveGaleWind()
        {
            this.CurrentGaleWind = 0;
            this.DeBuff(this.pbGaleWind);
        }
        public void RemoveWordOfLife()
        {
            this.CurrentWordOfLife = 0;
            this.DeBuff(this.pbWordOfLife);
        }
        public void RemoveWordOfFortune()
        {
            this.CurrentWordOfFortune = 0;
            this.DeBuff(this.pbWordOfFortune);
        }
        public void RemoveAetherDrive()
        {
            this.CurrentAetherDrive = 0;
            this.DeBuff(this.pbAetherDrive);
        }
        public void RemoveEternalPresence()
        {
            this.CurrentEternalPresence = 0;
            this.DeBuff(this.pbEternalPresence);
        }
        public void RemoveDeflection()
        {
            this.CurrentDeflection = 0;
            this.DeBuff(this.pbDeflection);
        }
        public void RemoveOneImmunity()
        {
            this.CurrentOneImmunity = 0;
            this.DeBuff(this.pbOneImmunity);
        }
        public void RemoveTimeStop()
        {
            this.CurrentTimeStop = 0;
            this.CurrentTimeStopImmediate = false;
            this.DeBuff(this.pbTimeStop);
        }
        public void RemoveBloodyVengeance()
        {
            this.CurrentBloodyVengeance = 0;
            this.BuffStrength_BloodyVengeance = 0;
            this.DeBuff(this.pbBloodyVengeance);
        }
        public void RemoveHeatBoost()
        {
            this.CurrentHeatBoost = 0;
            this.BuffAgility_HeatBoost = 0;
            this.DeBuff(this.pbHeatBoost);
        }
        public void RemovePromisedKnowledge()
        {
            this.CurrentPromisedKnowledge = 0;
            this.BuffIntelligence_PromisedKnowledge = 0;
            this.DeBuff(this.pbPromisedKnowledge);
        }
        public void RemoveRiseOfImage()
        {
            this.CurrentRiseOfImage = 0;
            this.BuffMind_RiseOfImage = 0;
            this.DeBuff(this.pbRiseOfImage);
        }
        public void RemovePsychicTrance()
        {
            this.CurrentPsychicTrance = 0;
            this.DeBuff(this.pbPsychicTrance);
        }
        public void RemoveBlindJustice()
        {
            this.CurrentBlindJustice = 0;
            this.DeBuff(this.pbBlindJustice);
        }
        public void RemoveTranscendentWish()
        {
            this.CurrentTranscendentWish = 0;
            this.BuffStrength_TranscendentWish = 0;
            this.BuffAgility_TranscendentWish = 0;
            this.BuffIntelligence_TranscendentWish = 0;
            this.BuffStamina_TranscendentWish = 0;
            this.BuffMind_TranscendentWish = 0;
            this.DeBuff(this.pbTranscendentWish);
        }
        public void RemoveFlashBlaze()
        {
            this.CurrentFlashBlazeCount = 0;
            this.DeBuff(this.pbFlashBlaze);
        }
        public void RemoveSkyShield()
        {
            this.CurrentSkyShield = 0;
            this.CurrentSkyShieldValue = 0;
            this.DeBuff(this.pbSkyShield);
        }
        public void RemoveEverDroplet()
        {
            this.CurrentEverDroplet = 0;
            this.DeBuff(this.pbEverDroplet);
        }
        public void RemoveHolyBreaker()
        {
            this.CurrentHolyBreaker = 0;
            this.DeBuff(this.pbHolyBreaker);
        }
        public void RemoveExaltedField()
        {
            this.CurrentExaltedField = 0;
            this.DeBuff(this.pbExaltedField);
        }
        public void RemoveHymnContract()
        {
            this.CurrentHymnContract = 0;
            this.DeBuff(this.pbHymnContract);
        }
        public void RemoveStarLightning()
        {
            this.CurrentStarLightning = 0;
            this.DeBuff(this.pbStarLightning);
        }
        public void RemoveEndlessAnthem()
        {
            //this.CurrentEndlessAnthem = 0;
            //this.DeBuff(this.pbEndlessAnthem);
        }
        public void RemoveBlackFire()
        {
            this.CurrentBlackFire = 0;
            this.DeBuff(this.pbBlackFire);
        }
        public void RemoveBlazingField()
        {
            this.CurrentBlazingField = 0;
            this.CurrentBlazingFieldFactor = 0;
            this.DeBuff(this.pbBlazingField);
        }
        public void RemoveDemonicIgnite()
        {
            //this.CurrentDemonicIgnite = 0;
            //this.DeBuff(this.pbDemonicIgnite);
        }
        public void RemoveWordOfMalice()
        {
            this.CurrentWordOfMalice = 0;
            this.DeBuff(this.pbWordOfMalice);
        }
        public void RemoveSinFortune()
        {
            this.CurrentSinFortune = 0;
            this.DeBuff(this.pbSinFortune);
        }
        public void RemoveDarkenField()
        {
            this.CurrentDarkenField = 0;
            this.DeBuff(this.pbDarkenField);
        }
        public void RemoveEclipseEnd()
        {
            this.CurrentEclipseEnd = 0;
            this.DeBuff(this.pbEclipseEnd);
        }
        public void RemoveFrozenAura()
        {
            this.CurrentFrozenAura = 0;
            this.DeBuff(this.pbFrozenAura);
        }
        public void RemoveChillBurn()
        {
            //this.CurrentChillBurn = 0;
            //this.DeBuff(this.pbChillBurn);
        }
        public void RemoveEnrageBlast()
        {
            this.CurrentEnrageBlast = 0;
            this.DeBuff(this.pbEnrageBlast);
        }
        //public void RemoveSigilOfHomura()
        //{
        //    this.CurrentSigilOfHomura = 0;
        //    this.DeBuff(this.pbSigilOfHomura);
        //}
        public void RemoveImmolate()
        {
            this.CurrentImmolate = 0;
            this.DeBuff(this.pbImmolate);
        }
        public void RemovePhantasmalWind()
        {
            this.CurrentPhantasmalWind = 0;
            this.DeBuff(this.pbPhantasmalWind);
        }
        public void RemoveRedDragonWill()
        {
            this.CurrentRedDragonWill = 0;
            this.DeBuff(this.pbRedDragonWill);
        }
        public void RemoveStaticBarrier()
        {
            this.CurrentStaticBarrier = 0;
            this.CurrentStaticBarrierValue = 0;
            this.DeBuff(this.pbStaticBarrier);
        }
        public void RemoveAusterityMatrix()
        {
            this.CurrentAusterityMatrix = 0;
            this.DeBuff(this.pbAusterityMatrix);
        }
        public void RemoveVanishWave()
        {
            //this.CurrentVanishWave = 0;
            //this.DeBuff(this.pbVanishWave);
        }
        public void RemoveVortexField()
        {
            //this.CurrentVortexField = 0;
            //this.DeBuff(this.pbVortexField);
        }
        public void RemoveBlueDragonWill()
        {
            this.CurrentBlueDragonWill = 0;
            this.DeBuff(this.pbBlueDragonWill);
        }
        public void RemoveSeventhMagic()
        {
            this.CurrentSeventhMagic = 0;
            this.DeBuff(this.pbSeventhMagic);
        }
        public void RemoveParadoxImage()
        {
            this.CurrentParadoxImage = 0;
            this.DeBuff(this.pbParadoxImage);
        }

        public void RemoveAntiStun()
        {
            this.CurrentAntiStun = 0;
            this.DeBuff(this.pbAntiStun);
        }
        public void RemoveStanceOfDeath()
        {
            this.CurrentStanceOfDeath = 0;
            this.DeBuff(this.pbStanceOfDeath);
        }
        public void RemoveStanceOfFlow()
        {
            this.CurrentStanceOfFlow = 0;
            this.DeBuff(this.pbStanceOfFlow);
        }
        public void RemoveTruthVision()
        {
            this.CurrentTruthVision = 0;
            this.DeBuff(this.pbTruthVision);
        }
        public void RemoveHighEmotionality()
        {
            this.CurrentHighEmotionality = 0;
            this.BuffStrength_HighEmotionality = 0;
            this.BuffAgility_HighEmotionality = 0;
            this.BuffIntelligence_HighEmotionality = 0;
            this.BuffStamina_HighEmotionality = 0;
            this.BuffMind_HighEmotionality = 0;
            this.DeBuff(this.pbHighEmotionality);
        }
        // s 後編追加
        public void RemoveCounterAttack()
        {
            this.currentCounterAttack = 0;
            this.DeBuff(this.pbCounterAttack);
        }
        public void RemoveStanceOfEyes()
        {
            this.CurrentStanceOfEyes = 0;
            this.DeBuff(this.pbStanceOfEyes);
        }
        public void RemoveNegate()
        {
            this.CurrentNegate = 0;
            this.DeBuff(this.pbNegate);
        }
        public void RemoveStanceOfStanding()
        {
            this.CurrentStanceOfStanding = 0;
            this.DeBuff(this.pbStanceOfStanding);
        }
        // e 後編追加
        public void RemovePainfulInsanity()
        {
            this.CurrentPainfulInsanity = 0;
            this.DeBuff(this.pbPainfulInsanity);
        }
        public void RemoveVoidExtraction()
        {
            this.CurrentVoidExtraction = 0;
            this.BuffStrength_VoidExtraction = 0;
            this.BuffAgility_VoidExtraction = 0;
            this.BuffIntelligence_VoidExtraction = 0;
            this.BuffStamina_VoidExtraction = 0;
            this.BuffMind_VoidExtraction = 0;
            this.DeBuff(this.pbVoidExtraction);
        }
        public void RemoveNothingOfNothingness()
        {
            this.CurrentNothingOfNothingness = 0;
            this.DeBuff(this.pbNothingOfNothingness);
        }
        public void RemoveStanceOfDouble()
        {
            this.CurrentStanceOfDouble = 0;
            this.DeBuff(this.pbStanceOfDouble);
        }
        public void RemoveSwiftStep()
        {
            this.CurrentSwiftStep = 0;
            this.DeBuff(this.pbSwiftStep);
        }
        public void RemoveVigorSense()
        {
            this.CurrentVigorSense = 0;
            this.DeBuff(this.pbVigorSense);
        }
        public void RemoveRisingAura()
        {
            this.CurrentRisingAura = 0;
            this.DeBuff(this.pbRisingAura);
        }
        public void RemoveOnslaughtHit()
        {
            this.CurrentOnslaughtHit = 0;
            this.CurrentOnslaughtHitValue = 0;
            this.DeBuff(this.pbOnslaughtHit);
        }
        public void RemoveSmoothingMove()
        {
            this.CurrentSmoothingMove = 0;
            this.DeBuff(this.pbSmoothingMove);
        }
        public void RemoveAscensionAura()
        {
            this.CurrentAscensionAura = 0;
            this.DeBuff(this.pbAscensionAura);
        }
        public void RemoveFutureVision()
        {
            this.CurrentFutureVision = 0;
            this.DeBuff(this.pbFutureVision);
        }
        public void RemoveReflexSpirit()
        {
            this.CurrentReflexSpirit = 0;
            this.DeBuff(this.pbReflexSpirit);
        }
        public void RemoveConcussiveHit()
        {
            this.CurrentConcussiveHit = 0;
            this.CurrentConcussiveHitValue = 0;
            this.DeBuff(this.pbConcussiveHit);
        }
        public void RemoveTrustSilence()
        {
            this.CurrentTrustSilence = 0;
            this.DeBuff(this.pbTrustSilence);
        }
        public void RemoveStanceOfMystic()
        {
            this.CurrentStanceOfMystic = 0;
            this.CurrentStanceOfMysticValue = 0;
            this.DeBuff(this.pbStanceOfMystic);
        }
        public void RemoveNourishSense()
        {
            this.CurrentNourishSense = 0;
            this.DeBuff(this.pbNourishSense);
        }
        public void RemoveImpulseHit()
        {
            this.CurrentImpulseHit = 0;
            this.CurrentImpulseHitValue = 0;
            this.DeBuff(this.pbImpulseHit);
        }
        public void RemoveOneAuthority()
        {
            this.CurrentOneAuthority = 0;
            this.DeBuff(this.pbOneAuthority);
        }

        public void RemovePhysicalAttackUp()
        {
            currentPhysicalAttackUp = 0;
            currentPhysicalAttackUpValue = 0;
            DeBuff(pbPhysicalAttackUp);
        }
        public void RemovePhysicalAttackDown()
        {
            currentPhysicalAttackDown = 0;
            currentPhysicalAttackDownValue = 0;
            DeBuff(pbPhysicalAttackDown);
        }
        public void RemovePhysicalDefenseUp()
        {
            currentPhysicalDefenseUp = 0;
            currentPhysicalDefenseUpValue = 0;
            DeBuff(pbPhysicalDefenseUp);
        }
        public void RemovePhysicalDefenseDown()
        {
            currentPhysicalDefenseDown = 0;
            currentPhysicalDefenseDownValue = 0;
            DeBuff(pbPhysicalDefenseDown);
        }

        public void RemoveMagicAttackUp()
        {
            currentMagicAttackUp = 0;
            currentMagicAttackUpValue = 0;
            DeBuff(pbMagicAttackUp);
        }
        public void RemoveMagicAttackDown()
        {
            currentMagicAttackDown = 0;
            currentMagicAttackDownValue = 0;
            DeBuff(pbMagicAttackDown);
        }

        public void RemoveMagicDefenseUp()
        {
            currentMagicDefenseUp = 0;
            currentMagicDefenseUpValue = 0;
            DeBuff(pbMagicDefenseUp);
        }
        public void RemoveMagicDefenseDown()
        {
            currentMagicDefenseDown = 0;
            currentMagicDefenseDownValue = 0;
            DeBuff(pbMagicDefenseDown);
        }
        
        public void RemoveSpeedUp()
        {
            currentSpeedUp = 0;
            currentSpeedUpValue = 0;
            DeBuff(pbSpeedUp);
        }
        public void RemoveSpeedDown()
        {
            currentSpeedDown = 0;
            currentSpeedDownValue = 0;
            DeBuff(pbSpeedDown);
        }

        public void RemoveReactionUp()
        {
            currentReactionUp = 0;
            currentReactionUpValue = 0;
            DeBuff(pbReactionUp);
        }
        public void RemoveReactionDown()
        {
            currentReactionDown = 0;
            currentReactionDownValue = 0;
            DeBuff(pbReactionDown);
        }

        public void RemovePotentialUp()
        {
            currentPotentialUp = 0;
            currentPotentialUpValue = 0;
            DeBuff(pbPotentialUp);
        }
        public void RemovePotentialDown()
        {
            currentPotentialDown = 0;
            currentPotentialDownValue = 0;
            DeBuff(pbPotentialDown);
        }

        public void RemoveStrengthUp()
        {
            currentStrengthUp = 0;
            currentStrengthUpValue = 0;
            DeBuff(pbStrengthUp);
        }

        public void RemoveAgilityUp()
        {
            currentAgilityUp = 0;
            currentAgilityUpValue = 0;
            DeBuff(pbAgilityUp);
        }

        public void RemoveIntelligenceUp()
        {
            currentIntelligenceUp = 0;
            currentIntelligenceUpValue = 0;
            DeBuff(pbIntelligenceUp);
        }

        public void RemoveStaminaUp()
        {
            currentStaminaUp = 0;
            currentStaminaUpValue = 0;
            DeBuff(pbStaminaUp);
        }

        public void RemoveMindUp()
        {
            currentMindUp = 0;
            currentMindUpValue = 0;
            DeBuff(pbMindUp);
        }

        public void RemoveLightUp()
        {
            currentLightUp = 0;
            currentLightUpValue = 0;
            DeBuff(pbLightUp);
        }
        public void RemoveLightDown()
        {
            currentLightDown = 0;
            currentLightDownValue = 0;
            DeBuff(pbLightDown);
        }
        public void RemoveShadowUp()
        {
            currentShadowUp = 0;
            currentShadowUpValue = 0;
            DeBuff(pbShadowUp);
        }
        public void RemoveShadowDown()
        {
            currentShadowDown = 0;
            currentShadowDownValue = 0;
            DeBuff(pbShadowDown);
        }
        public void RemoveFireUp()
        {
            currentFireUp = 0;
            currentFireUpValue = 0;
            DeBuff(pbFireUp);
        }
        public void RemoveFireDown()
        {
            currentFireDown = 0;
            currentFireDownValue = 0;
            DeBuff(pbFireDown);
        }
        public void RemoveIceUp()
        {
            currentIceUp = 0;
            currentIceUpValue = 0;
            DeBuff(pbIceUp);
        }
        public void RemoveIceDown()
        {
            currentIceDown = 0;
            currentIceDownValue = 0;
            DeBuff(pbIceDown);
        }
        public void RemoveForceUp()
        {
            currentForceUp = 0;
            currentForceUpValue = 0;
            DeBuff(pbForceUp);
        }
        public void RemoveForceDown()
        {
            currentForceDown = 0;
            currentForceDownValue = 0;
            DeBuff(pbForceDown);
        }
        public void RemoveWillUp()
        {
            currentWillUp = 0;
            currentWillUpValue = 0;
            DeBuff(pbWillUp);
        }
        public void RemoveWillDown()
        {
            currentWillDown = 0;
            currentWillDownValue = 0;
            DeBuff(pbWillDown);
        }
        public void RemoveResistLightUp()
        {
            currentResistLightUp = 0;
            currentResistLightUpValue = 0;
            DeBuff(pbResistLightUp);
        }

        public void RemoveResistShadowUp()
        {
            currentResistShadowUp = 0;
            currentResistShadowUpValue = 0;
            DeBuff(pbResistShadowUp);
        }

        public void RemoveResistFireUp()
        {
            currentResistFireUp = 0;
            currentResistFireUpValue = 0;
            DeBuff(pbResistFireUp);
        }

        public void RemoveResistIceUp()
        {
            currentResistIceUp = 0;
            currentResistIceUpValue = 0;
            DeBuff(pbResistIceUp);
        }

        public void RemoveResistForceUp()
        {
            currentResistForceUp = 0;
            currentResistForceUpValue = 0;
            DeBuff(pbResistForceUp);
        }

        public void RemoveResistWillUp()
        {
            currentResistWillUp = 0;
            currentResistWillUpValue = 0;
            DeBuff(pbResistWillUp);
        }

        public void RemoveAfterReviveHalf()
        {
            currentAfterReviveHalf = 0;
            DeBuff(pbAfterReviveHalf);
        }
        public void RemoveFireDamage2()
        {
            currentFireDamage2 = 0;
            DeBuff(pbFireDamage2);
        }
        public void RemoveBlackMagic()
        {
            currentBlackMagic = 0;
            DeBuff(pbBlackMagic);
        }
        public void RemoveChaosDesperate()
        {
            currentChaosDesperate = 0;
            currentChaosDesperateValue = 0;
            DeBuff(pbChaosDesperate);
        }
        public void RemoveIchinaruHomura()
        {
            currentIchinaruHomura = 0;
            DeBuff(pbIchinaruHomura);
        }

        public void RemoveAbyssFire()
        {
            currentAbyssFire = 0;
            DeBuff(pbAbyssFire);
        }

        public void RemoveLightAndShadow()
        {
            currentLightAndShadow = 0;
            DeBuff(pbLightAndShadow);
        }

        public void RemoveEternalDroplet()
        {
            currentEternalDroplet = 0;
            DeBuff(pbEternalDroplet);
        }

        public void RemoveAusterityMatrixOmega()
        {
            currentAusterityMatrixOmega = 0;
            DeBuff(pbAusterityMatrixOmega);
        }

        public void RemoveVoiceOfAbyss()
        {
            currentVoiceOfAbyss = 0;
            DeBuff(pbVoiceOfAbyss);
        }

        public void RemoveAbyssWill()
        {
            currentAbyssWill = 0;
            currentAbyssWillValue = 0;
            DeBuff(pbAbyssWill);
        }

        public void RemoveTheAbyssWall()
        {
            currentTheAbyssWall = 0;
            DeBuff(pbTheAbyssWall);
        }

        public void RemovePreStunning()
        {
            this.CurrentPreStunning = 0;
            this.DeBuff(this.pbPreStunning);
        }
        public void RemoveStun()
        {
            this.CurrentStunning = 0;
            this.DeBuff(this.pbStun);
        }
        public void RemoveSilence()
        {
            this.CurrentSilence = 0;
            this.DeBuff(this.pbSilence);
        }
        public void RemovePoison()
        {
            this.CurrentPoison = 0;
            this.CurrentPoisonValue = 0;
            this.DeBuff(this.pbPoison);
        }
        public void RemoveTemptation()
        {
            this.CurrentTemptation = 0;
            this.DeBuff(this.pbTemptation);
        }
        public void RemoveFrozen()
        {
            this.CurrentFrozen = 0;
            this.DeBuff(this.pbFrozen);
        }
        public void RemoveParalyze()
        {
            this.CurrentParalyze = 0;
            this.DeBuff(this.pbParalyze);
        }
        public void RemoveNoResurrection()
        {
            this.CurrentNoResurrection = 0;
            this.DeBuff(this.pbNoResurrection);
        }
        public void RemoveSlow()
        {
            this.CurrentSlow = 0;
            this.DeBuff(this.pbSlow);
        }
        public void RemoveBlind()
        {
            this.CurrentBlind = 0;
            this.DeBuff(this.pbBlind);
        }
        public void RemoveSlip()
        {
            this.CurrentSlip = 0;
            this.DeBuff(this.pbSlip);
        }
        public void RemoveNoGainLife()
        {
            this.CurrentNoGainLife = 0;
            this.DeBuff(this.pbNoGainLife);
        }
        public void RemoveBlinded()
        {
            this.CurrentBlinded = 0;
            this.DeBuff(this.pbBlinded);
        }
        public void RemoveSpeedBoost()
        {
            currentSpeedBoost = 0;
        }

        public void RemoveChargeCount()
        {
            currentChargeCount = 0;
        }

        public void RemovePhysicalChargeCount()
        {
            currentPhysicalChargeCount = 0;
        }

        // 武器特有
        public void RemoveFeltus()
        {
            this.CurrentFeltus = 0;
            this.CurrentFeltusValue = 0;
            this.DeBuff(this.pbFeltus);
        }
        public void RemoveJuzaPhantasmal()
        {
            this.CurrentJuzaPhantasmal = 0;
            this.CurrentJuzaPhantasmalValue = 0;
            this.DeBuff(this.pbJuzaPhantasmal);
        }
        public void RemoveEternalFateRing()
        {
            this.CurrentEternalDroplet = 0;
            this.CurrentEternalFateRingValue = 0;
            this.DeBuff(this.pbEternalFateRing);
        }
        public void RemoveLightServant()
        {
            this.CurrentLightServant = 0;
            this.CurrentLightServantValue = 0;
            this.DeBuff(this.pbLightServant);
        }
        public void RemoveShadowServant()
        {
            this.CurrentShadowServant = 0;
            this.CurrentShadowServantValue = 0;
            this.DeBuff(this.pbShadowServant);
        }
        public void RemoveAdilBlueBurn()
        {
            this.CurrentAdilBlueBurn = 0;
            this.CurrentAdilBlueBurnValue = 0;
            this.DeBuff(this.pbAdilBlueBurn);
        }
        public void RemoveMazeCube()
        {
            this.CurrentMazeCube = 0;
            this.CurrentMazeCubeValue = 0;
            this.DeBuff(this.pbMazeCube);
        }
        public void RemoveShadowBible()
        {
            this.CurrentShadowBible = 0;
            this.DeBuff(this.pbShadowBible);
        }
        public void RemoveDetachmentOrb()
        {
            this.CurrentDetachmentOrb = 0;
            this.DeBuff(this.pbDetachmentOrb);
        }
        public void RemoveDevilSummonerTome()
        {
            this.CurrentDevilSummonerTome = 0;
            this.DeBuff(this.pbDevilSummonerTome);
        }
        public void RemoveVoidHymnsonia()
        {
            this.CurrentVoidHymnsonia = 0;
            this.DeBuff(this.pbVoidHymnsonia);
        }

        public void RemoveSagePotionMini()
        {
            this.currentSagePotionMini = 0;
            this.DeBuff(this.pbSagePotionMini);
        }
        public void RemoveGenseiTaima()
        {
            this.currentGenseiTaima = 0;
            this.DeBuff(this.pbGenseiTaima);
        }
        public void RemoveShiningAether()
        {
            this.currentShiningAether = 0;
            this.DeBuff(this.pbShiningAether);
        }
        public void RemoveBlackElixir()
        {
            this.currentBlackElixir = 0;
            this.currentBlackElixirValue = 0;
            this.DeBuff(this.pbBlackElixir);
        }
        public void RemoveElementalSeal()
        {
            this.currentElementalSeal = 0;
            this.DeBuff(this.pbElementalSeal);
        }
        public void RemoveColoressAntidote()
        {
            this.currentColoressAntidote = 0;
            this.DeBuff(this.pbColoressAntidote);
        }
        public void RemoveLifeCount()
        {
            this.currentLife = 0;
            this.currentLifeCount = 0;
            this.DeBuff(this.pbLifeCount);
        }
        public void RemoveChaoticSchema()
        {
            this.currentChaoticSchema = 0;
            this.DeBuff(this.pbChaoticSchema);
        }
        // e 後編追加


        public void ActivateBuff(TruthImage imageData, string imageName, int count)
        {
            if (imageData.Image == null)
            {
                imageData.Image = Image.FromFile(imageName);
                imageData.Location = new Point(0 - Database.BUFFPANEL_BUFF_WIDTH * this.BuffNumber, 0);
                imageData.Count = count;
                imageData.Invalidate();
                imageData.Update();
                this.BuffNumber++;
            }
        }

        public void ChangeBuffImage(TruthImage imageData, string imageName)
        {
            if　(imageData != null)
            {
                imageData.Image = Image.FromFile(imageName);
            }
        }
        public void DeBuff(TruthImage imageData)
        {
            if (imageData.Image != null)
            {
                RemoveOneBuff(imageData);
                this.BuffNumber--;
            }
        }

        public delegate void RemoveBuff();
        public void AbstractChangeStatus(string bmpName, int value, TruthImage pbData, RemoveBuff remove, int count)
        {
            if (value <= 0)
            {
                remove();
            }
            else if ((value == 1) && (pbData.Image == null))
            {
                this.ActivateBuff(pbData, Database.BaseResourceFolder + bmpName + ".bmp", count);
            }
            else
            {
                pbData.Invalidate();
            }
        }
        public void ChangePoisonStatus(int count)
        {
            pbPoison.Cumulative = currentPoisonValue;
            AbstractChangeStatus("Poison", this.CurrentPoisonValue, pbPoison, this.RemovePoison, count);
        }
        public void ChangeConcussiveHitStatus(int count)
        {
            pbConcussiveHit.Cumulative = currentConcussiveHitValue;
            AbstractChangeStatus(Database.CONCUSSIVE_HIT, this.CurrentConcussiveHitValue, pbConcussiveHit, this.RemoveConcussiveHit, count);
        }
        public void ChangeOnslaughtHitStatus(int count)
        {
            pbOnslaughtHit.Cumulative = currentOnslaughtHitValue;
            AbstractChangeStatus(Database.ONSLAUGHT_HIT, this.CurrentOnslaughtHitValue, pbOnslaughtHit, this.RemoveOnslaughtHit, count);
        }
        public void ChangeImpulseHitStatus(int count)
        {
            pbImpulseHit.Cumulative = currentImpulseHitValue;
            AbstractChangeStatus(Database.IMPULSE_HIT, this.CurrentImpulseHitValue, pbImpulseHit, this.RemoveImpulseHit, count);
        }
        public void ChangeSkyShieldStatus(int count)
        {
            pbSkyShield.Cumulative = currentSkyShieldValue;
            AbstractChangeStatus(Database.SKY_SHIELD, this.CurrentSkyShieldValue, pbSkyShield, this.RemoveSkyShield, count);
        }
        public void ChangeStaticBarrierStatus(int count)
        {
            pbStaticBarrier.Cumulative = currentStaticBarrierValue;
            AbstractChangeStatus(Database.STATIC_BARRIER, this.CurrentStaticBarrierValue, pbStaticBarrier, this.RemoveStaticBarrier, count);
        }
        public void ChangeStanceOfMysticStatus(int count)
        {
            pbStanceOfMystic.Cumulative = currentStanceOfMysticValue;
            AbstractChangeStatus(Database.STANCE_OF_MYSTIC, this.CurrentStanceOfMysticValue, pbStanceOfMystic, this.RemoveStanceOfMystic, count);
        }
        public void ChangeFeltusStatus(int count)
        {
            pbFeltus.Cumulative = currentFeltusValue;
            AbstractChangeStatus(Database.ITEMCOMMAND_FELTUS, this.currentFeltusValue, pbFeltus, this.RemoveFeltus, count);
        }
        public void ChangeJuzaPhantasmalStatus(int count)
        {
            pbJuzaPhantasmal.Cumulative = currentJuzaPhantasmalValue;
            AbstractChangeStatus(Database.ITEMCOMMAND_JUZA_PHANTASMAL, this.CurrentJuzaPhantasmalValue, pbJuzaPhantasmal, this.RemoveJuzaPhantasmal, count);
        }
        public void ChangeEternalFateRingStatus(int count)
        {
            pbEternalFateRing.Cumulative = currentEternalFateRingValue;
            AbstractChangeStatus(Database.ITEMCOMMAND_ETERNAL_FATE, this.currentEternalFateRingValue, pbEternalFateRing, this.RemoveEternalFateRing, count);
        }
        public void ChangeLightServantStatus(int count)
        {
            pbLightServant.Cumulative = currentLightServantValue;
            AbstractChangeStatus(Database.ITEMCOMMAND_LIGHT_SERVANT, this.CurrentLightServantValue, pbLightServant, this.RemoveLightServant, count);
        }
        public void ChangeShadowServantStatus(int count)
        {
            pbShadowServant.Cumulative = currentShadowServantValue;
            AbstractChangeStatus(Database.ITEMCOMMAND_SHADOW_SERVANT, this.CurrentShadowServantValue, pbShadowServant, this.RemoveShadowServant, count);
        }
        public void ChangeAdilBlueBurnStatus(int count)
        {
            pbAdilBlueBurn.Cumulative = currentAdilBlueBurnValue;
            AbstractChangeStatus(Database.ITEMCOMMAND_ADIL_RING_BLUE_BURN, this.CurrentAdilBlueBurnValue, pbAdilBlueBurn, this.RemoveAdilBlueBurn, count);
        }
        public void ChangeMazeCubeStatus(int count)
        {
            pbMazeCube.Cumulative = currentMazeCubeValue;
            AbstractChangeStatus(Database.ITEMCOMMAND_MAZE_CUBE, this.CurrentMazeCubeValue, pbMazeCube, this.RemoveMazeCube, count);
        }
        public void ChangeLifeCountStatus(int count)
        {
            pbLifeCount.Cumulative = currentLifeCountValue;
            AbstractChangeStatus(Database.LIFE_COUNT, this.CurrentLifeCountValue, pbLifeCount, this.RemoveLifeCount, count);
        }
        public void UpdateGenesisCommand(PlayerAction curPA, string spell, string skill, string item, string arche)
        {
            this.beforePA = curPA;
            if (this.beforePA == PlayerAction.UseSpell)
            {
                this.beforeSpellName = spell;
                this.beforeSkillName = String.Empty;
                this.beforeUsingItem = String.Empty;
                this.beforeArchetypeName = String.Empty;
            }
            if (this.beforePA == PlayerAction.UseSkill)
            {
                this.beforeSpellName = String.Empty;
                this.beforeSkillName = skill;
                this.beforeUsingItem = String.Empty;
                this.beforeArchetypeName = String.Empty;
            }
            if (this.beforePA == PlayerAction.UseItem)
            {
                this.beforeSpellName = String.Empty;
                this.beforeSkillName = String.Empty;
                this.beforeUsingItem = item;
                this.beforeArchetypeName = String.Empty;
            }
            if (this.beforePA == PlayerAction.Archetype)
            {
                this.beforeSpellName = String.Empty;
                this.beforeSkillName = String.Empty;
                this.beforeUsingItem = String.Empty ;
                this.beforeArchetypeName = arche;
            }
            this.beforeTarget = target;
            this.beforeTarget2 = target2; // 後編追加
            // this.alreadyPlayArchetype = false; [元核発動フラグは一日立たないと戻らない]
        }

        protected void AbstractCountUpBuff(TruthImage picture, ref int countBase)
        {
            if (countBase > 0)
            {
                countBase++;
                if (picture != null)
                {
                    picture.Count++;
                    picture.Invalidate();
                }
            }
        }

        protected void AbstractCountDownBuff(TruthImage picture, ref int countBase)
        {
            int dummy1 = 0;
            AbstractCountDownBuff(picture, ref countBase, ref dummy1);
        }
        protected void AbstractCountDownBuff(TruthImage picture, ref int countBase, ref int value1)
        {
            int value2 = 0;
            AbstractCountDownBuff(picture, ref countBase, ref value1, ref value2);
        }
        protected void AbstractCountDownBuff(TruthImage picture, ref int countBase, ref int value1, ref int value2)
        {
            int value3 = 0;
            AbstractCountDownBuff(picture, ref countBase, ref value1, ref value2, ref value3);
        }
        protected void AbstractCountDownBuff(TruthImage picture, ref int countBase, ref int value1, ref int value2, ref int value3)
        {
            int value4 = 0;
            AbstractCountDownBuff(picture, ref countBase, ref value1, ref value2, ref value3, ref value4);
        }
        protected void AbstractCountDownBuff(TruthImage picture, ref int countBase, ref int value1, ref int value2, ref int value3, ref int value4)
        {
            int value5 = 0;
            AbstractCountDownBuff(picture, ref countBase, ref value1, ref value2, ref value3, ref value4, ref value5);
        }
        protected void AbstractCountDownBuff(TruthImage picture, ref int countBase, ref int value1, ref int value2, ref int value3, ref int value4, ref int value5)
        {
            bool flag1 = false;
            AbstractCountDownBuff(picture, ref countBase, ref value1, ref value2, ref value3, ref value4, ref value5, ref flag1);
        }
        protected void AbstractCountDownBuff(TruthImage picture, ref int countBase, ref int value1, ref int value2, ref int value3, ref int value4, ref int value5, ref bool flag1)
        {
            if (countBase > 0)
            {
                countBase--;
                if (picture != null)
                {
                    picture.Count--;
                    picture.Invalidate();
                }
                if (countBase <= 0)
                {
                    flag1 = true; // [警告]:deadSignForTranscendentWishをtrueにするために採用したフラグだが、他の展開を考えた時、デフォルトtrue指定するようになるので留意必須。
                    value1 = 0;
                    value2 = 0;
                    value3 = 0;
                    value4 = 0;
                    value5 = 0;
                    if (picture != null)
                    {
                        RemoveOneBuff(picture);
                        this.BuffNumber--;
                    }
                }
            }
        }

        // [警告]：BattleEnemy_Load、MainCharacter:CleanUpEffect, MainCharacter:CleanUpBattleEndの展開ミスが増え続けています。
        public void CleanUpEffect()
        {
            CleanUpEffect(true, true);
        }
        public void CleanUpEffect(bool ClearActionInfo, bool ClearBeforeAction)
        {
            if (PA == PlayerAction.UseSkill && currentSkillName == Database.GENESIS)
            {
                // Genesisスキルは前回の情報をクリアしないことでそれを使い続けます。
            }
            else if (PA == PlayerAction.UseSkill && currentSkillName == Database.STANCE_OF_DOUBLE)
            {
                // StanceOfDoubleスキルは前回の情報をクリアしないことでそれを使い続けます。
            }
            else
            {
                if (ClearBeforeAction == true)
                {
                    this.beforePA = PA;
                    this.beforeUsingItem = currentUsingItem;
                    this.beforeSkillName = currentSkillName;
                    this.beforeSpellName = currentSpellName;
                    this.beforeArchetypeName = currentArchetypeName; // 後編追加
                    this.beforeTarget = target;
                    this.beforeTarget2 = target2; // 後編追加
                    // this.alreadyPlayArchetype = false; [元核発動フラグは一日立たないと戻らない]
                }
            }

            if (ClearActionInfo)
            {
                CurrentUsingItem = String.Empty;
                CurrentSkillName = String.Empty;
                CurrentSpellName = String.Empty;
                CurrentArchetypeName = String.Empty; // 後編追加
                PA = PlayerAction.None;
                target = null;
                target2 = null; // 後編追加
            }

            // 循環と誓約がかかっている場合、カウントダウンしない。
            if (this.CurrentJunkan_Seiyaku <= 0)
            {
                // change start 後編編集（抽象化メソッドを使って一行で表す様、修正）
                // 動
                // 静
                if (this.realTimeBattle == false) // 後編追加、リアルタイムでは、ターン終了で解除せず、別のフェーズで解除する。
                {
                    CurrentCounterAttack = 0; // 後編編集
                }
                // s 後編追加
                else
                {
                    AbstractCountDownBuff(pbCounterAttack, ref currentCounterAttack);
                }
                // e 後編追加
                AbstractCountDownBuff(pbAntiStun, ref currentAntiStun);
                AbstractCountDownBuff(pbStanceOfDeath, ref currentStanceOfDeath);

                // 柔
                AbstractCountDownBuff(pbStanceOfFlow, ref currentStanceOfFlow);
                // 剛
                AbstractCountDownBuff(pbStanceOfStanding, ref currentStanceOfStanding); // 後編編集
                // 心眼
                AbstractCountDownBuff(pbTruthVision, ref currentTruthVision);
                AbstractCountDownBuff(pbHighEmotionality, ref currentHighEmotionality, ref buffStrength_HighEmotionality, ref buffAgility_HighEmotionality, ref buffIntelligence_HighEmotionality, ref buffStamina_HighEmotionality, ref buffMind_HighEmotionality);
                AbstractCountDownBuff(pbStanceOfEyes, ref currentStanceOfEyes); // 後編編集
                AbstractCountDownBuff(pbPainfulInsanity, ref currentPainfulInsanity);
                // 無心
                AbstractCountDownBuff(pbNegate, ref currentNegate); // 後編編集
                AbstractCountDownBuff(pbVoidExtraction, ref currentVoidExtraction, ref buffStrength_VoidExtraction, ref buffAgility_VoidExtraction, ref buffIntelligence_VoidExtraction, ref buffStamina_VoidExtraction, ref buffMind_VoidExtraction);
                AbstractCountDownBuff(pbNothingOfNothingness, ref currentNothingOfNothingness);

                // 聖
                AbstractCountDownBuff(pbProtection, ref currentProtection);
                AbstractCountDownBuff(pbSaintPower, ref currentSaintPower);
                AbstractCountDownBuff(pbGlory, ref currentGlory);
                // 闇
                AbstractCountDownBuff(pbShadowPact, ref currentShadowPact);
                AbstractCountDownBuff(pbBlackContract, ref currentBlackContract);
                AbstractCountDownBuff(pbBloodyVengeance, ref currentBloodyVengeance, ref buffStrength_BloodyVengeance);
                AbstractCountDownBuff(pbDamnation, ref currentDamnation);                
                // 火
                AbstractCountDownBuff(pbFlameAura, ref currentFlameAura);
                AbstractCountDownBuff(pbHeatBoost, ref currentHeatBoost, ref buffAgility_HeatBoost);
                AbstractCountDownBuff(pbImmortalRave, ref currentImmortalRave);
                // 水
                AbstractCountDownBuff(pbAbsorbWater, ref currentAbsorbWater);
                AbstractCountDownBuff(pbMirrorImage, ref currentMirrorImage);
                AbstractCountDownBuff(pbPromisedKnowledge, ref currentPromisedKnowledge, ref buffIntelligence_PromisedKnowledge);
                AbstractCountDownBuff(pbAbsoluteZero, ref currentAbsoluteZero);
                // 理
                AbstractCountDownBuff(pbGaleWind, ref currentGaleWind);
                AbstractCountDownBuff(pbWordOfLife, ref currentWordOfLife);
                AbstractCountDownBuff(pbWordOfFortune, ref currentWordOfFortune);
                AbstractCountDownBuff(pbAetherDrive, ref currentAetherDrive);
                AbstractCountDownBuff(pbEternalPresence, ref currentEternalPresence);
                // 空
                AbstractCountDownBuff(pbRiseOfImage, ref currentRiseOfImage, ref buffMind_RiseOfImage);
                AbstractCountDownBuff(pbDeflection, ref currentDeflection);
                AbstractCountDownBuff(pbOneImmunity, ref currentOneImmunity);
                AbstractCountDownBuff(pbTimeStop, ref currentTimeStop);

                // 聖＋闇
                AbstractCountDownBuff(pbPsychicTrance, ref currentPsychicTrance);
                AbstractCountDownBuff(pbBlindJustice, ref currentBlindJustice);
                AbstractCountDownBuff(pbTranscendentWish, ref currentTranscendentWish, ref buffStrength_TranscendentWish, ref buffAgility_TranscendentWish, ref buffIntelligence_TranscendentWish, ref buffStamina_TranscendentWish, ref buffMind_TranscendentWish, ref deadSignForTranscendentWish);
                // 聖＋火
                AbstractCountDownBuff(pbFlashBlaze, ref currentFlashBlazeCount);
                // 聖＋水
                AbstractCountDownBuff(pbSkyShield, ref currentSkyShield, ref currentSkyShieldValue);
                AbstractCountDownBuff(pbEverDroplet, ref currentEverDroplet);
                // 聖＋理
                AbstractCountDownBuff(pbHolyBreaker, ref currentHolyBreaker);
                AbstractCountDownBuff(pbExaltedField, ref currentExaltedField);
                AbstractCountDownBuff(pbHymnContract, ref currentHymnContract);
                // 聖＋空
                AbstractCountDownBuff(pbStarLightning, ref currentStarLightning);
                // 闇＋火
                AbstractCountDownBuff(pbBlackFire, ref currentBlackFire);
                AbstractCountDownBuff(pbBlazingField, ref currentBlazingField, ref currentBlazingFieldFactor);
                // 闇＋水
                currentDeepMirror = false;
                // 闇＋理
                AbstractCountDownBuff(pbWordOfMalice, ref currentWordOfMalice);
                AbstractCountDownBuff(pbSinFortune, ref currentSinFortune);
                // 闇＋空
                AbstractCountDownBuff(pbDarkenField, ref currentDarkenField);
                AbstractCountDownBuff(pbEclipseEnd, ref currentEclipseEnd);
                // 火＋水
                AbstractCountDownBuff(pbFrozenAura, ref currentFrozenAura);
                // 火＋理
                AbstractCountDownBuff(pbEnrageBlast, ref currentEnrageBlast);
                AbstractCountDownBuff(pbSigilOfHomura, ref currentSigilOfHomura);
                // 火＋空
                AbstractCountDownBuff(pbImmolate, ref currentImmolate);
                AbstractCountDownBuff(pbPhantasmalWind, ref currentPhantasmalWind);
                AbstractCountDownBuff(pbRedDragonWill, ref currentRedDragonWill);
                // 水＋理
                AbstractCountDownBuff(pbStaticBarrier, ref currentStaticBarrier, ref currentStaticBarrierValue);
                AbstractCountDownBuff(pbAusterityMatrix, ref currentAusterityMatrix);
                // 水＋空
                AbstractCountDownBuff(pbBlueDragonWill, ref currentBlueDragonWill);
                // 理＋空
                AbstractCountDownBuff(pbSeventhMagic, ref currentSeventhMagic);
                AbstractCountDownBuff(pbParadoxImage, ref currentParadoxImage);
                // 動＋静
                AbstractCountDownBuff(pbStanceOfDouble, ref currentStanceOfDouble);
                // 動＋柔
                AbstractCountDownBuff(pbSwiftStep, ref currentSwiftStep);
                AbstractCountDownBuff(pbVigorSense, ref currentVigorSense);
                // 動＋剛
                AbstractCountDownBuff(pbRisingAura, ref currentRisingAura);
                // 動＋心眼
                AbstractCountDownBuff(pbOnslaughtHit, ref currentOnslaughtHit, ref currentOnslaughtHitValue);
                // 動＋無心
                AbstractCountDownBuff(pbSmoothingMove, ref currentSmoothingMove);
                AbstractCountDownBuff(pbAscensionAura, ref currentAscensionAura);
                // 静＋柔
                AbstractCountDownBuff(pbFutureVision, ref currentFutureVision);
                // 静＋剛
                AbstractCountDownBuff(pbReflexSpirit, ref currentReflexSpirit);
                // 静＋心眼
                AbstractCountDownBuff(pbConcussiveHit, ref currentConcussiveHit, ref currentConcussiveHitValue);
                // 静＋無心
                AbstractCountDownBuff(pbTrustSilence, ref currentTrustSilence);
                // 柔＋剛
                AbstractCountDownBuff(pbStanceOfMystic, ref currentStanceOfMystic, ref currentStanceOfMysticValue);
                // 柔＋心眼
                AbstractCountDownBuff(pbNourishSense, ref currentNourishSense);
                // 柔＋無心
                AbstractCountDownBuff(pbImpulseHit, ref currentImpulseHit, ref currentImpulseHitValue);
                // 剛＋心眼
                AbstractCountDownBuff(pbOneAuthority, ref currentOneAuthority);
                // 剛＋無心
                currentHardestParry = false;
                // 心眼＋無心
                currentStanceOfSuddenness = false;

                // 武器特有
                AbstractCountDownBuff(pbFeltus, ref currentFeltus, ref currentFeltusValue);
                AbstractCountDownBuff(pbJuzaPhantasmal, ref currentJuzaPhantasmal, ref currentJuzaPhantasmalValue);
                AbstractCountDownBuff(pbEternalFateRing, ref currentEternalFateRing, ref currentEternalFateRingValue);
                AbstractCountDownBuff(pbLightServant, ref currentLightServant, ref currentLightServantValue);
                AbstractCountDownBuff(pbShadowServant, ref currentShadowServant, ref currentShadowServantValue);
                AbstractCountDownBuff(pbAdilBlueBurn, ref currentAdilBlueBurn, ref currentAdilBlueBurnValue);
                AbstractCountDownBuff(pbMazeCube, ref currentMazeCube, ref currentMazeCubeValue);
                AbstractCountDownBuff(pbShadowBible, ref currentShadowBible);
                AbstractCountDownBuff(pbDetachmentOrb, ref currentDetachmentOrb);
                AbstractCountDownBuff(pbDevilSummonerTome, ref currentDevilSummonerTome);
                AbstractCountDownBuff(pbVoidHymnsonia, ref currentVoidHymnsonia);
                AbstractCountDownBuff(pbSagePotionMini, ref currentSagePotionMini);
                AbstractCountDownBuff(pbGenseiTaima, ref currentGenseiTaima);
                AbstractCountDownBuff(pbShiningAether, ref currentShiningAether);
                AbstractCountDownBuff(pbBlackElixir, ref currentBlackElixir, ref currentBlackElixirValue);
                AbstractCountDownBuff(pbElementalSeal, ref currentElementalSeal);
                AbstractCountDownBuff(pbColoressAntidote, ref currentColoressAntidote);

                // 最終戦ライフカウント
                //AbstractCountDownBuff(pbLifeCount, ref currentLifeCount, ref currentLifeCountValue); // ターンのクリーンナップでカウントダウンはしない
                AbstractCountDownBuff(pbChaoticSchema, ref currentChaoticSchema);

                // 正の影響効果
                AbstractCountDownBuff(pbBlinded, ref currentBlinded);
                // SpeedBoostはBattleEnemyフォーム側でマイナスさせます。
                // CurrentChargeCountは「ためる」コマンドのため、CleanUpEffectでは減算しません。
                // CurrentPhysicalChargeCountは「ためる」コマンドのため、CleanUpEffectでは減算しません。
                AbstractCountDownBuff(pbPhysicalAttackUp, ref currentPhysicalAttackUp, ref currentPhysicalAttackUpValue);
                AbstractCountDownBuff(pbPhysicalDefenseUp, ref currentPhysicalDefenseUp, ref currentPhysicalDefenseUpValue);
                AbstractCountDownBuff(pbMagicAttackUp, ref currentMagicAttackUp, ref currentMagicAttackUpValue);
                AbstractCountDownBuff(pbMagicDefenseUp, ref currentMagicDefenseUp, ref currentMagicDefenseUpValue);
                AbstractCountDownBuff(pbSpeedUp, ref currentSpeedUp, ref currentSpeedUpValue);
                AbstractCountDownBuff(pbReactionUp, ref currentReactionUp, ref currentReactionUpValue);
                AbstractCountDownBuff(pbPotentialUp, ref currentPotentialUp, ref currentPotentialUpValue);

                AbstractCountDownBuff(pbStrengthUp, ref currentStrengthUp, ref currentStrengthUpValue);
                AbstractCountDownBuff(pbAgilityUp, ref currentAgilityUp, ref currentAgilityUpValue);
                AbstractCountDownBuff(pbIntelligenceUp, ref currentIntelligenceUp, ref currentIntelligenceUpValue);
                AbstractCountDownBuff(pbStaminaUp, ref currentStaminaUp, ref currentStaminaUpValue);
                AbstractCountDownBuff(pbMindUp, ref currentMindUp, ref currentMindUpValue);

                AbstractCountDownBuff(pbLightUp, ref currentLightUp, ref currentLightUpValue);
                AbstractCountDownBuff(pbShadowUp, ref currentShadowUp, ref currentShadowUpValue);
                AbstractCountDownBuff(pbFireUp, ref currentFireUp, ref currentFireUpValue);
                AbstractCountDownBuff(pbIceUp, ref currentIceUp, ref currentIceUpValue);
                AbstractCountDownBuff(pbForceUp, ref currentForceUp, ref currentForceUpValue);
                AbstractCountDownBuff(pbWillUp, ref currentWillUp, ref currentWillUpValue);

                AbstractCountDownBuff(pbResistLightUp, ref currentResistLightUp, ref currentResistLightUpValue);
                AbstractCountDownBuff(pbResistShadowUp, ref currentResistShadowUp, ref currentResistShadowUpValue);
                AbstractCountDownBuff(pbResistFireUp, ref currentResistFireUp, ref currentResistFireUpValue);
                AbstractCountDownBuff(pbResistIceUp, ref currentResistIceUp, ref currentResistIceUpValue);
                AbstractCountDownBuff(pbResistForceUp, ref currentResistForceUp, ref currentResistForceUpValue);
                AbstractCountDownBuff(pbResistWillUp, ref currentResistWillUp, ref currentResistWillUpValue);

                // 集中と断絶
                AbstractCountDownBuff(pbSyutyuDanzetsu, ref currentSyutyu_Danzetsu);               
            }

            // 循環と誓約(コレ自身は、【循環と誓約】効果対象外）
            AbstractCountDownBuff(pbJunkanSeiyaku, ref currentJunkan_Seiyaku);

            // 負の影響効果(【循環と誓約】効果対象外）
            AbstractCountDownBuff(pbPreStunning, ref currentPreStunning);
            AbstractCountDownBuff(pbStun, ref currentStunning);
            AbstractCountDownBuff(pbSilence, ref currentSilence);
            AbstractCountDownBuff(pbPoison, ref currentPoison, ref currentPoisonValue);
            AbstractCountDownBuff(pbTemptation, ref currentTemptation);
            AbstractCountDownBuff(pbFrozen, ref currentFrozen);
            AbstractCountDownBuff(pbParalyze, ref currentParalyze);
            AbstractCountDownBuff(pbNoResurrection, ref currentNoResurrection);
            // s 後編追加
            AbstractCountDownBuff(pbSlip, ref currentSlip);
            AbstractCountDownBuff(pbSlow, ref currentSlow);
            AbstractCountDownBuff(pbNoGainLife, ref currentNoGainLife);
            AbstractCountDownBuff(pbBlind, ref currentBlind);

            AbstractCountDownBuff(pbPhysicalAttackDown, ref currentPhysicalAttackDown, ref currentPhysicalAttackDownValue);
            AbstractCountDownBuff(pbPhysicalDefenseDown, ref currentPhysicalDefenseDown, ref currentPhysicalDefenseDownValue);
            AbstractCountDownBuff(pbMagicAttackDown, ref currentMagicAttackDown, ref currentMagicAttackDownValue);
            AbstractCountDownBuff(pbMagicDefenseDown, ref currentMagicDefenseDown, ref currentMagicDefenseDownValue);
            AbstractCountDownBuff(pbSpeedDown, ref currentSpeedDown, ref currentSpeedDownValue);
            AbstractCountDownBuff(pbReactionDown, ref currentReactionDown, ref currentReactionDownValue);
            AbstractCountDownBuff(pbPotentialDown, ref currentPotentialDown, ref currentPotentialDownValue);

            // pbStrengthDown系が存在しない

            AbstractCountDownBuff(pbLightDown, ref currentLightDown, ref currentLightDownValue);
            AbstractCountDownBuff(pbShadowDown, ref currentShadowDown, ref currentShadowDownValue);
            AbstractCountDownBuff(pbFireDown, ref currentFireDown, ref currentFireDownValue);
            AbstractCountDownBuff(pbIceDown, ref currentIceDown, ref currentIceDownValue);
            AbstractCountDownBuff(pbForceDown, ref currentForceDown, ref currentForceDownValue);
            AbstractCountDownBuff(pbWillDown, ref currentWillDown, ref currentWillDownValue);

            // pbResistLightDown系が存在しない
            
            AbstractCountDownBuff(pbAfterReviveHalf, ref currentAfterReviveHalf);

            AbstractCountDownBuff(pbFireDamage2, ref currentFireDamage2);

            AbstractCountDownBuff(pbBlackMagic, ref currentBlackMagic);

            //AbstractCountDownBuff(pbChaosDesperate, ref currentChaosDesperate, ref currentChaosDesperateValue);
            if (currentChaosDesperate > 0)
            {
                currentChaosDesperate--;
                currentChaosDesperateValue--;
                if (currentChaosDesperate <= 0 || currentChaosDesperateValue <= 0)
                {
                    //currentChaosDesperate = 0; 外部クラスで判定材料にするため、あえてコメントアウト
                    currentChaosDesperateValue = 0;
                    if (pbChaosDesperate != null)
                    {
                        RemoveOneBuff(pbChaosDesperate);
                        this.BuffNumber--;
                    }
                }
            }

            AbstractCountDownBuff(pbIchinaruHomura, ref currentIchinaruHomura);
            AbstractCountDownBuff(pbAbyssFire, ref currentAbyssFire);
            AbstractCountDownBuff(pbLightAndShadow, ref currentLightAndShadow);
            AbstractCountDownBuff(pbEternalDroplet, ref currentEternalDroplet);
            AbstractCountDownBuff(pbAusterityMatrixOmega, ref currentAusterityMatrixOmega);
            AbstractCountDownBuff(pbVoiceOfAbyss, ref currentVoiceOfAbyss);
            AbstractCountDownBuff(pbAbyssWill, ref currentAbyssWill, ref currentAbyssWillValue);
            AbstractCountDownBuff(pbTheAbyssWall, ref currentTheAbyssWall);
            // e 後編追加

            poolLifeConsumption = 0;
            poolManaConsumption = 0;
            poolSkillConsumption = 0;

            // 後編
            // s 後編「コメント」以下は、戦闘中永続であり、カウントダウンを含めない。
            //amplifyPhysicalAttack;
            //amplifyPhysicalDefense;
            //amplifyMagicAttack;
            //amplifyMagicDefense;
            //amplifyBattleSpeed;
            //amplifyBattleResponse;
            //amplifyPotential;
        }

        public void BuffCountUp()
        {
            // 静
            AbstractCountUpBuff(pbCounterAttack, ref currentCounterAttack); // 後編追加
            AbstractCountUpBuff(pbAntiStun, ref currentAntiStun);
            AbstractCountUpBuff(pbStanceOfDeath, ref currentStanceOfDeath);
            // 柔
            AbstractCountUpBuff(pbStanceOfFlow, ref currentStanceOfFlow);
            // 剛
            AbstractCountUpBuff(pbStanceOfStanding, ref currentStanceOfStanding); // 後編追加
            // 心眼
            AbstractCountUpBuff(pbTruthVision, ref currentTruthVision);
            AbstractCountUpBuff(pbHighEmotionality, ref currentHighEmotionality);
            AbstractCountUpBuff(pbStanceOfEyes, ref currentStanceOfEyes); // 後編編集
            AbstractCountUpBuff(pbPainfulInsanity, ref currentPainfulInsanity);
            // 無心
            AbstractCountUpBuff(pbNegate, ref currentNegate); // 後編追加
            AbstractCountUpBuff(pbVoidExtraction, ref currentVoidExtraction);
            AbstractCountUpBuff(pbNothingOfNothingness, ref currentNothingOfNothingness);

            // 聖
            AbstractCountUpBuff(pbProtection, ref currentProtection);
            AbstractCountUpBuff(pbSaintPower, ref currentSaintPower);
            AbstractCountUpBuff(pbGlory, ref currentGlory);
            // 闇
            AbstractCountUpBuff(pbShadowPact, ref currentShadowPact);
            AbstractCountUpBuff(pbBlackContract, ref currentBlackContract);
            AbstractCountUpBuff(pbBloodyVengeance, ref currentBloodyVengeance);
            //AbstractCountUpBuff(pbDamnation, ref currentDamnation); // 負の影響
            // 火
            AbstractCountUpBuff(pbFlameAura, ref currentFlameAura);
            AbstractCountUpBuff(pbHeatBoost, ref currentHeatBoost);
            AbstractCountUpBuff(pbImmortalRave, ref currentImmortalRave);
            // 水
            AbstractCountUpBuff(pbAbsorbWater, ref currentAbsorbWater);
            AbstractCountUpBuff(pbMirrorImage, ref currentMirrorImage);
            AbstractCountUpBuff(pbPromisedKnowledge, ref currentPromisedKnowledge);
            //AbstractCountUpBuff(pbAbsoluteZero, ref currentAbsoluteZero); // 負の影響
            // 理
            AbstractCountUpBuff(pbGaleWind, ref currentGaleWind);
            AbstractCountUpBuff(pbWordOfLife, ref currentWordOfLife);
            AbstractCountUpBuff(pbWordOfFortune, ref currentWordOfFortune);
            AbstractCountUpBuff(pbAetherDrive, ref currentAetherDrive);
            AbstractCountUpBuff(pbEternalPresence, ref currentEternalPresence);
            // 空
            AbstractCountUpBuff(pbRiseOfImage, ref currentRiseOfImage);
            AbstractCountUpBuff(pbDeflection, ref currentDeflection);
            AbstractCountUpBuff(pbOneImmunity, ref currentOneImmunity);
            AbstractCountUpBuff(pbTimeStop, ref currentTimeStop);

            // 聖＋闇
            AbstractCountUpBuff(pbPsychicTrance, ref currentPsychicTrance);
            AbstractCountUpBuff(pbBlindJustice, ref currentBlindJustice);
            AbstractCountUpBuff(pbTranscendentWish, ref currentTranscendentWish);
            // 聖＋火
            //AbstractCountUpBuff(pbFlashBlaze, ref currentFlashBlazeCount); // 負の影響
            // 聖＋水
            AbstractCountUpBuff(pbSkyShield, ref currentSkyShield);
            AbstractCountUpBuff(pbEverDroplet, ref currentEverDroplet);
            // 聖＋理
            AbstractCountUpBuff(pbHolyBreaker, ref currentHolyBreaker);
            AbstractCountUpBuff(pbExaltedField, ref currentExaltedField);
            AbstractCountUpBuff(pbHymnContract, ref currentHymnContract);
            // 聖＋空
            //AbstractCountUpBuff(pbStarLightning, ref currentStarLightning); // 負の影響
            // 闇＋火
            //AbstractCountUpBuff(pbBlackFire, ref currentBlackFire); // 負の影響
            //AbstractCountUpBuff(pbBlazingField, ref currentBlazingField); // 負の影響
            // 闇＋水
            // 闇＋理
            //AbstractCountUpBuff(pbWordOfMalice, ref currentWordOfMalice); // 負の影響
            AbstractCountUpBuff(pbSinFortune, ref currentSinFortune);
            // 闇＋空
            //AbstractCountUpBuff(pbDarkenField, ref currentDarkenField); // 負の影響
            AbstractCountUpBuff(pbEclipseEnd, ref currentEclipseEnd);
            // 火＋水
            AbstractCountUpBuff(pbFrozenAura, ref currentFrozenAura);
            // 火＋理
            //AbstractCountUpBuff(pbEnrageBlast, ref currentEnrageBlast); // 負の影響
            //AbstractCountUpBuff(pbSigilOfHomura, ref currentSigilOfHomura); // 負の影響
            // 火＋空
            //AbstractCountUpBuff(pbImmolate, ref currentImmolate); // 負の影響
            AbstractCountUpBuff(pbPhantasmalWind, ref currentPhantasmalWind);
            AbstractCountUpBuff(pbRedDragonWill, ref currentRedDragonWill);
            // 水＋理
            AbstractCountUpBuff(pbStaticBarrier, ref currentStaticBarrier);
            //AbstractCountUpBuff(pbAusterityMatrix, ref currentAusterityMatrix); // 負の影響
            // 水＋空
            AbstractCountUpBuff(pbBlueDragonWill, ref currentBlueDragonWill);
            // 理＋空
            AbstractCountUpBuff(pbSeventhMagic, ref currentSeventhMagic);
            AbstractCountUpBuff(pbParadoxImage, ref currentParadoxImage);
            // 動＋静
            AbstractCountUpBuff(pbStanceOfDouble, ref currentStanceOfDouble);
            // 動＋柔
            AbstractCountUpBuff(pbSwiftStep, ref currentSwiftStep);
            AbstractCountUpBuff(pbVigorSense, ref currentVigorSense);
            // 動＋剛
            AbstractCountUpBuff(pbRisingAura, ref currentRisingAura);
            // 動＋心眼
            //AbstractCountUpBuff(pbOnslaughtHit, ref currentOnslaughtHit); // 負の影響
            // 動＋無心
            AbstractCountUpBuff(pbSmoothingMove, ref currentSmoothingMove);
            AbstractCountUpBuff(pbAscensionAura, ref currentAscensionAura);
            // 静＋柔
            AbstractCountUpBuff(pbFutureVision, ref currentFutureVision);
            // 静＋剛
            AbstractCountUpBuff(pbReflexSpirit, ref currentReflexSpirit);
            // 静＋心眼
            //AbstractCountUpBuff(pbConcussiveHit, ref currentConcussiveHit); // 負の影響
            // 静＋無心
            AbstractCountUpBuff(pbTrustSilence, ref currentTrustSilence);
            // 柔＋剛
            AbstractCountUpBuff(pbStanceOfMystic, ref currentStanceOfMystic);
            // 柔＋心眼
            AbstractCountUpBuff(pbNourishSense, ref currentNourishSense);
            // 柔＋無心
            //AbstractCountUpBuff(pbImpulseHit, ref currentImpulseHit); // 負の影響
            // 剛＋心眼
            AbstractCountUpBuff(pbOneAuthority, ref currentOneAuthority);
            // 剛＋無心
            // 心眼＋無心

            // 武器特有 // 武器まで影響を及ぼさない
            //AbstractCountUpBuff(pbFeltus, ref currentFeltus);
            //AbstractCountUpBuff(pbJuzaPhantasmal, ref currentJuzaPhantasmal);
            //AbstractCountUpBuff(pbEternalFateRing, ref currentEternalFateRing);
            //AbstractCountUpBuff(pbLightServant, ref currentLightServant);
            //AbstractCountUpBuff(pbShadowServant, ref currentShadowServant);
            //AbstractCountUpBuff(pbAdilBlueBurn, ref currentAdilBlueBurn);
            //AbstractCountUpBuff(pbMazeCube, ref currentMazeCube);
            //AbstractCountUpBuff(pbShadowBible, ref currentShadowBible);
            //AbstractCountUpBuff(pbDetachmentOrb, ref currentDetachmentOrb);
            //AbstractCountUpBuff(pbDevilSummonerTome, ref currentDevilSummonerTome);
            //AbstractCountUpBuff(pbVoidHymnsonia, ref currentVoidHymnsonia);
            //AbstractCountUpBuff(pbSagePotionMini, ref currentSagePotionMini);
            //AbstractCountUpBuff(pbGenseiTaima, ref currentGenseiTaima);
            //AbstractCountUpBuff(pbShiningAether, ref currentShiningAether);
            //AbstractCountUpBuff(pbBlackElixir, ref currentBlackElixir);
            //AbstractCountUpBuff(pbElementalSeal, ref currentElementalSeal);
            //AbstractCountUpBuff(pbColoressAntidote, ref currentColoressAntidote);

            // 最終戦ライフカウント
            //AbstractCountUpBuff(pbLifeCount, ref currentLifeCountValue); カウントアップする対象ではないため、コメントアウト
            //AbstractCountUpBuff(pbChaoticSchema, ref currentChaoticSchema);  カウントアップする対象ではないため、コメントアウト
            
            // 正の影響効果
            AbstractCountUpBuff(pbBlinded, ref currentBlinded);
            // SpeedBoostはBattleEnemyフォーム側でマイナスさせます。
            // CurrentChargeCountは「ためる」コマンドのため、CleanUpEffectでは減算しません。
            // CurrentPhysicalChargeCountは「ためる」コマンドのため、CleanUpEffectでは減算しません。
            AbstractCountUpBuff(pbPhysicalAttackUp, ref currentPhysicalAttackUp);
            AbstractCountUpBuff(pbPhysicalDefenseUp, ref currentPhysicalDefenseUp);
            AbstractCountUpBuff(pbMagicAttackUp, ref currentMagicAttackUp);
            AbstractCountUpBuff(pbMagicDefenseUp, ref currentMagicDefenseUp);
            AbstractCountUpBuff(pbSpeedUp, ref currentSpeedUp);
            AbstractCountUpBuff(pbReactionUp, ref currentReactionUp);
            AbstractCountUpBuff(pbPotentialUp, ref currentPotentialUp);

            AbstractCountUpBuff(pbStrengthUp, ref currentStrengthUp);
            AbstractCountUpBuff(pbAgilityUp, ref currentAgilityUp);
            AbstractCountUpBuff(pbIntelligenceUp, ref currentIntelligenceUp);
            AbstractCountUpBuff(pbStaminaUp, ref currentStaminaUp);
            AbstractCountUpBuff(pbMindUp, ref currentMindUp);

            AbstractCountUpBuff(pbLightUp, ref currentLightUp);
            AbstractCountUpBuff(pbShadowUp, ref currentShadowUp);
            AbstractCountUpBuff(pbFireUp, ref currentFireUp);
            AbstractCountUpBuff(pbIceUp, ref currentIceUp);
            AbstractCountUpBuff(pbForceUp, ref currentForceUp);
            AbstractCountUpBuff(pbWillUp, ref currentWillUp);

            AbstractCountUpBuff(pbResistLightUp, ref currentResistLightUp);
            AbstractCountUpBuff(pbResistShadowUp, ref currentResistShadowUp);
            AbstractCountUpBuff(pbResistFireUp, ref currentResistFireUp);
            AbstractCountUpBuff(pbResistIceUp, ref currentResistIceUp);
            AbstractCountUpBuff(pbResistForceUp, ref currentResistForceUp);
            AbstractCountUpBuff(pbResistWillUp, ref currentResistWillUp);

            // 集中と断絶
            AbstractCountUpBuff(pbSyutyuDanzetsu, ref currentSyutyu_Danzetsu);

            // 循環と誓約(コレ自身は、【循環と誓約】効果対象外）
            AbstractCountUpBuff(pbJunkanSeiyaku, ref currentJunkan_Seiyaku);

            // 負の影響効果(【循環と誓約】効果対象外）
            //AbstractCountUpBuff(pbPreStunning, ref currentPreStunning);
            //AbstractCountUpBuff(pbStun, ref currentStunning);
            //AbstractCountUpBuff(pbSilence, ref currentSilence);
            //AbstractCountUpBuff(pbPoison, ref currentPoison);
            //AbstractCountUpBuff(pbTemptation, ref currentTemptation);
            //AbstractCountUpBuff(pbFrozen, ref currentFrozen);
            //AbstractCountUpBuff(pbParalyze, ref currentParalyze);
            //AbstractCountUpBuff(pbNoResurrection, ref currentNoResurrection);
            //AbstractCountUpBuff(pbSlip, ref currentSlip);
            //AbstractCountUpBuff(pbSlow, ref currentSlow);
            //AbstractCountUpBuff(pbNoGainLife, ref currentNoGainLife);
            //AbstractCountUpBuff(pbBlind, ref currentBlind);

            //AbstractCountUpBuff(pbPhysicalAttackDown, ref currentPhysicalAttackDown);
            //AbstractCountUpBuff(pbPhysicalDefenseDown, ref currentPhysicalDefenseDown);
            //AbstractCountUpBuff(pbMagicAttackDown, ref currentMagicAttackDown);
            //AbstractCountUpBuff(pbMagicDefenseDown, ref currentMagicDefenseDown);
            //AbstractCountUpBuff(pbSpeedDown, ref currentSpeedDown);
            //AbstractCountUpBuff(pbReactionDown, ref currentReactionDown);
            //AbstractCountUpBuff(pbPotentialDown, ref currentPotentialDown);

            // pbStrengthDown系が存在しない

            //AbstractCountUpBuff(pbLightDown, ref currentLightDown);
            //AbstractCountUpBuff(pbShadowDown, ref currentShadowDown);
            //AbstractCountUpBuff(pbFireDown, ref currentFireDown);
            //AbstractCountUpBuff(pbIceDown, ref currentIceDown);
            //AbstractCountUpBuff(pbForceDown, ref currentForceDown);
            //AbstractCountUpBuff(pbWillDown, ref currentWillDown);

            // pbResistLightDown系が存在しない

            //AbstractCountUpBuff(pbAfterReviveHalf, ref currentAfterReviveHalf);

            //AbstractCountUpBuff(pbFireDamage2, ref currentFireDamage2);

            //AbstractCountUpBuff(pbBlackMagic, ref currentBlackMagic);

            //AbstractCountUpBuff(pbChaosDesperate, ref currentChaosDesperate);

            //AbstractCountUpBuff(pbIchinaruHomura, ref currentIchinaruHomura);
            //AbstractCountUpBuff(pbAbyssFire, ref currentAbyssFire);
            //AbstractCountUpBuff(pbLightAndShadow, ref currentLightAndShadow);
            //AbstractCountUpBuff(pbEternalDroplet, ref currentEternalDroplet);
            //AbstractCountUpBuff(pbAusterityMatrixOmega, ref currentAusterityMatrixOmega);
            //AbstractCountUpBuff(pbVoiceOfAbyss, ref currentVoiceOfAbyss);
            //AbstractCountUpBuff(pbAbyssWill, ref currentAbyssWill);
            //AbstractCountUpBuff(pbTheAbyssWall, ref currentTheAbyssWall);
        }

        // s 後編追加
        // ボス負の影響自動リカバー
        protected int autoRecoverStunning = 0;
        protected int autoRecoverSilence = 0;
        protected int autoRecoverPoison = 0;
        protected int autoRecoverTemptation = 0;
        protected int autoRecoverFrozen = 0;
        protected int autoRecoverParalyze = 0;
        protected int autoRecoverNoResurrection = 0;
        protected int autoRecoverSlow = 0;
        protected int autoRecoverBlind = 0;
        protected int autoRecoverSlip = 0;
        protected int autoRecoverNoGainLife = 0;
        // e 後編追加

        public void CleanUpEffectForBoss()
        {
            if (CurrentStunning > 0)
            {
                autoRecoverStunning++;
            }
            if (CurrentSilence > 0)
            {
                autoRecoverSilence++;
            }
            if (CurrentPoison > 0)
            {
                autoRecoverPoison++;
            }
            if (CurrentTemptation > 0)
            {
                autoRecoverTemptation++;
            }
            if (CurrentFrozen > 0)
            {
                autoRecoverFrozen++;
            }
            if (CurrentParalyze > 0)
            {
                autoRecoverParalyze++;
            }
            if (CurrentNoResurrection > 0)
            {
                autoRecoverNoResurrection++;
            }
            if (CurrentSlow > 0)
            {
                autoRecoverSlow++;
            }
            if (CurrentBlind > 0)
            {
                autoRecoverBlind++;
            }
            if (CurrentSlip > 0)
            {
                autoRecoverSlip++;
            }
            if (CurrentNoGainLife > 0)
            {
                autoRecoverNoGainLife++;
            }

            if (autoRecoverStunning >= Database.BASE_TIMER_BAR_LENGTH / 3)
            {
                autoRecoverStunning = 0;
                CurrentStunning--;
                if (CurrentStunning <= 0)
                {
                    if (pbStun != null)
                    {
                        RemoveOneBuff(pbStun);
                        this.BuffNumber--;
                    }
                }
            }
            if (autoRecoverSilence >= Database.BASE_TIMER_BAR_LENGTH / 3)
            {
                autoRecoverSilence = 0;
                CurrentSilence--;
                if (CurrentSilence <= 0)
                {
                    if (pbSilence != null)
                    {
                        RemoveOneBuff(pbSilence);
                        this.BuffNumber--;
                    }
                }
            }
            if (autoRecoverPoison >= Database.BASE_TIMER_BAR_LENGTH / 3)
            {
                autoRecoverPoison = 0;
                CurrentPoison--;
                if (CurrentPoison <= 0)
                {
                    if (pbPoison != null)
                    {
                        RemoveOneBuff(pbPoison);
                        this.BuffNumber--;
                    }
                }
            }
            if (autoRecoverTemptation >= Database.BASE_TIMER_BAR_LENGTH / 3)
            {
                autoRecoverTemptation = 0;
                CurrentTemptation--;
                if (CurrentTemptation <= 0)
                {
                    if (pbTemptation != null)
                    {
                        RemoveOneBuff(pbTemptation);
                        this.BuffNumber--;
                    }
                }
            }
            if (autoRecoverFrozen >= Database.BASE_TIMER_BAR_LENGTH / 3)
            {
                autoRecoverFrozen = 0;
                CurrentFrozen--;
                if (CurrentFrozen <= 0)
                {
                    if (pbFrozen != null)
                    {
                        RemoveOneBuff(pbFrozen);
                        this.BuffNumber--;
                    }
                }
            }
            if (autoRecoverParalyze >= Database.BASE_TIMER_BAR_LENGTH / 3)
            {
                autoRecoverParalyze = 0;
                CurrentParalyze--;
                if (CurrentParalyze <= 0)
                {
                    if (pbParalyze != null)
                    {
                        RemoveOneBuff(pbParalyze);
                        this.BuffNumber--;
                    }
                }
            }
            if (autoRecoverNoResurrection >= Database.BASE_TIMER_BAR_LENGTH / 3)
            {
                autoRecoverNoResurrection = 0;
                CurrentNoResurrection--;
                if (CurrentNoResurrection <= 0)
                {
                    if (pbNoResurrection != null)
                    {
                        RemoveOneBuff(pbNoResurrection);
                        this.BuffNumber--;
                    }
                }
            }
            if (autoRecoverSlow >= Database.BASE_TIMER_BAR_LENGTH / 3)
            {
                autoRecoverSlow = 0;
                CurrentSlow--;
                if (CurrentSlow <= 0)
                {
                    if (pbSlow != null)
                    {
                        RemoveOneBuff(pbSlow);
                        this.BuffNumber--;
                    }
                }
            }
            if (autoRecoverBlind >= Database.BASE_TIMER_BAR_LENGTH / 3)
            {
                autoRecoverBlind = 0;
                CurrentBlind--;
                if (CurrentBlind <= 0)
                {
                    if (pbBlind != null)
                    {
                        RemoveOneBuff(pbBlind);
                        this.BuffNumber--;
                    }
                }
            }
            if (autoRecoverSlip >= Database.BASE_TIMER_BAR_LENGTH / 3)
            {
                autoRecoverSlip = 0;
                CurrentSlip--;
                if (CurrentSlip <= 0)
                {
                    if (pbSlip != null)
                    {
                        RemoveOneBuff(pbSlip);
                        this.BuffNumber--;
                    }
                }
            }
            if (autoRecoverNoGainLife >= Database.BASE_TIMER_BAR_LENGTH / 3)
            {
                autoRecoverNoGainLife = 0;
                CurrentNoGainLife--;
                if (CurrentNoGainLife <= 0)
                {
                    if (pbNoGainLife != null)
                    {
                        RemoveOneBuff(pbNoGainLife);
                        this.BuffNumber--;
                    }
                }
            }

            if (this.CurrentTimeStopImmediate)
            {
                AbstractCountDownBuff(this.pbTimeStop, ref this.currentTimeStop);
            }

        }

        private void RemoveOneBuff(TruthImage imageBox)
        {
            Point tempPoint = new Point(imageBox.Location.X - Database.BUFFPANEL_BUFF_WIDTH, 0);
            imageBox.Count = 0;
            imageBox.Cumulative = 0;
            imageBox.Image = null;
            imageBox.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0);
            imageBox.Update();

            MoveNextBuff(tempPoint);
        }

        private void MoveNextBuff(Point tempPoint)
        {
            for (int ii = 0; ii < Database.BUFF_NUM; ii++)
            {
                if (tempPoint == this.BuffElement[ii].Location)
                {
                    this.BuffElement[ii].Location = new Point(this.BuffElement[ii].Location.X + Database.BUFFPANEL_BUFF_WIDTH, 0);
                    Point tempPointB = new Point(tempPoint.X - Database.BUFFPANEL_BUFF_WIDTH, 0);
                    MoveNextBuff(tempPointB);
                    break;
                }
            }

        }

        // [警告]：BattleEnemy_Load、MainCharacter:CleanUpEffect, MainCharacter:CleanUpBattleEndの展開ミスが増え続けています。
        public void CleanUpBattleEnd()
        {
            string dummy = String.Empty;
            CleanUpBattleEnd(ref dummy);
        }
        public void CleanUpBattleEnd(ref string brokenName)
        {
            this.beforePA = PlayerAction.None;
            this.beforeUsingItem = String.Empty;
            this.beforeSkillName = String.Empty;
            this.beforeSpellName = String.Empty;
            this.beforeArchetypeName = String.Empty; // 後編追加
            this.beforeTarget = null;
            this.beforeTarget2 = null; // 後編追加
            // this.alreadyPlayArchetype = false; [元核発動フラグは一日立たないと戻らない]
            PA = PlayerAction.None;
            CurrentUsingItem = String.Empty;
            CurrentSkillName = String.Empty;
            CurrentSpellName = String.Empty;
            CurrentArchetypeName = String.Empty; // 後編追加
            target = null;
            target2 = null; // 後編追加

            // ボス負の影響自動リカバー
            autoRecoverStunning = 0;
            autoRecoverSilence = 0;
            autoRecoverPoison = 0;
            autoRecoverTemptation = 0;
            autoRecoverFrozen = 0;
            autoRecoverParalyze = 0;
            autoRecoverNoResurrection = 0;
            autoRecoverSlow = 0;
            autoRecoverBlind = 0;
            autoRecoverSlip = 0;

            // 動
            // 静
            CurrentCounterAttack = 0; // 後編編集
            CurrentAntiStun = 0;
            CurrentStanceOfDeath = 0;
            // 柔
            CurrentStanceOfFlow = 0;
            // 剛
            CurrentStanceOfStanding = 0; // 後編編集
            // 心眼
            CurrentTruthVision = 0;
            CurrentHighEmotionality = 0;
            CurrentStanceOfEyes = 0; // 後編編集
            CurrentPainfulInsanity = 0;
            // 無心
            CurrentNegate = 0; // 後編編集
            CurrentVoidExtraction = 0;
            CurrentNothingOfNothingness = 0;

            // 聖
            CurrentProtection = 0;
            CurrentSaintPower = 0;
            CurrentGlory = 0;
            // 闇
            CurrentShadowPact = 0;
            CurrentBlackContract = 0;
            CurrentBloodyVengeance = 0;
            CurrentDamnation = 0;
            // 火
            CurrentFlameAura = 0;
            CurrentHeatBoost = 0;
            CurrentImmortalRave = 0;
            // 水
            CurrentAbsorbWater = 0;
            CurrentMirrorImage = 0;
            CurrentAbsoluteZero = 0;
            CurrentPromisedKnowledge = 0;
            // 理
            CurrentGaleWind = 0;
            CurrentWordOfLife = 0;
            CurrentWordOfFortune = 0;
            CurrentAetherDrive = 0;
            CurrentEternalPresence = 0;
            // 空
            CurrentRiseOfImage = 0;
            CurrentOneImmunity = 0;
            CurrentDeflection = 0;
            CurrentTimeStop = 0;
            CurrentTimeStopImmediate = false;

            // s 後編追加
            // 聖＋闇
            CurrentPsychicTrance = 0;
            CurrentBlindJustice = 0;
            CurrentTranscendentWish = 0;
            // 聖＋火
            CurrentFlashBlazeCount = 0;
            // 聖＋水
            CurrentSkyShield = 0;
            CurrentSkyShieldValue = 0;
            CurrentEverDroplet = 0;
            // 聖＋理
            CurrentHolyBreaker = 0;
            CurrentExaltedField = 0;
            CurrentHymnContract = 0;
            // 聖＋空
            CurrentStarLightning = 0;
            // 闇＋火
            CurrentBlackFire = 0;
            CurrentBlazingField = 0;
            CurrentBlazingFieldFactor = 0;
            // 闇＋水
            CurrentDeepMirror = false;
            // 闇＋理
            CurrentWordOfMalice = 0;
            CurrentSinFortune = 0;
            // 闇＋空
            CurrentDarkenField = 0;
            CurrentEclipseEnd = 0;
            // 火＋水
            CurrentFrozenAura = 0;
            // 火＋理
            CurrentEnrageBlast = 0;
            CurrentSigilOfHomura = 0;
            // 火＋空
            CurrentImmolate = 0;
            CurrentPhantasmalWind = 0;
            CurrentRedDragonWill = 0;
            // 水＋理
            CurrentStaticBarrier = 0;
            CurrentStaticBarrierValue = 0;
            CurrentAusterityMatrix = 0;
            // 水＋空
            CurrentBlueDragonWill = 0;
            // 理＋空
            CurrentSeventhMagic = 0;
            CurrentParadoxImage = 0;

            // 動＋静
            CurrentStanceOfDouble = 0;
            // 動＋柔
            CurrentSwiftStep = 0;
            CurrentVigorSense = 0;
            // 動＋剛
            CurrentRisingAura = 0;
            // 動＋心眼
            CurrentOnslaughtHit = 0;
            CurrentOnslaughtHitValue = 0;
            // 動＋無心
            CurrentSmoothingMove = 0;
            CurrentAscensionAura = 0;
            // 静＋柔
            CurrentFutureVision = 0;
            // 静＋剛
            CurrentReflexSpirit = 0;
            // 静＋心眼
            CurrentConcussiveHit = 0;
            CurrentConcussiveHitValue = 0;
            // 静＋無心
            CurrentTrustSilence = 0;
            // 柔＋剛
            CurrentStanceOfMystic = 0;
            CurrentStanceOfMysticValue = 0;
            // 柔＋心眼
            CurrentNourishSense = 0;
            // 柔＋無心
            CurrentImpulseHit = 0;
            CurrentImpulseHitValue = 0;
            // 剛＋心眼
            CurrentOneAuthority = 0;
            // 剛＋無心
            CurrentHardestParry = false;
            // 心眼＋無心
            CurrentStanceOfSuddenness = false;

            // 武器特有
            CurrentFeltus = 0;
            CurrentFeltusValue = 0;
            CurrentJuzaPhantasmal = 0;
            CurrentJuzaPhantasmalValue = 0;
            CurrentEternalFateRing = 0;
            CurrentEternalFateRingValue = 0;
            CurrentLightServant = 0;
            CurrentLightServantValue = 0;
            CurrentShadowServant = 0;
            CurrentShadowServantValue = 0;
            CurrentAdilBlueBurn = 0;
            CurrentAdilBlueBurnValue = 0;
            CurrentMazeCube = 0;
            CurrentMazeCubeValue = 0;
            CurrentShadowBible = 0;
            CurrentDetachmentOrb = 0;
            CurrentDevilSummonerTome = 0;
            CurrentVoidHymnsonia = 0;

            // 消耗品特有
            CurrentSagePotionMini = 0;
            CurrentGenseiTaima = 0;
            CurrentShiningAether = 0;
            CurrentBlackElixir = 0;
            CurrentBlackElixirValue = 0;
            CurrentElementalSeal = 0;
            CurrentColoressAntidote = 0;
            
            // 最終戦ライフカウント
            CurrentLifeCount = 0;
            CurrentLifeCountValue = 0;

            // ヴェルゼ最終戦カオティックスキーマ
            CurrentChaoticSchema = 0;

            // 集中と断絶
            CurrentSyutyu_Danzetsu = 0;
            CurrentJunkan_Seiyaku = 0;
            // e 後編追加

            // 動
            // 静
            if (pbCounterAttack != null) { pbCounterAttack.Image = null; pbCounterAttack.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } // 後編追加
            if (pbAntiStun != null) { pbAntiStun.Image = null; pbAntiStun.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbStanceOfDeath != null) { pbStanceOfDeath.Image = null; pbStanceOfDeath.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 柔
            if (pbStanceOfFlow != null) { pbStanceOfFlow.Image = null; pbStanceOfFlow.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 剛
            if (pbStanceOfStanding != null) { pbStanceOfStanding.Image = null; pbStanceOfStanding.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } // 後編追加
            // 心眼
            if (pbTruthVision != null) { pbTruthVision.Image = null; pbTruthVision.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbHighEmotionality != null) { pbHighEmotionality.Image = null; pbHighEmotionality.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbStanceOfEyes != null) { pbStanceOfEyes.Image = null; pbStanceOfEyes.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } // 後編追加
            if (pbPainfulInsanity != null) { pbPainfulInsanity.Image = null; pbPainfulInsanity.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 無心
            if (pbNegate != null) { pbNegate.Image = null; pbNegate.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } // 後編追加
            if (pbVoidExtraction != null) { pbVoidExtraction.Image = null; pbVoidExtraction.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbNothingOfNothingness != null) { pbNothingOfNothingness.Image = null; pbNothingOfNothingness.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            // 聖
            if (pbProtection != null) { pbProtection.Image = null; pbProtection.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbSaintPower != null) { pbSaintPower.Image = null; pbSaintPower.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbGlory != null) { pbGlory.Image = null; pbGlory.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 闇
            if (pbShadowPact != null) { pbShadowPact.Image = null; pbShadowPact.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbBlackContract != null) { pbBlackContract.Image = null; pbBlackContract.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbBloodyVengeance != null) { pbBloodyVengeance.Image = null; pbBloodyVengeance.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbDamnation != null) { pbDamnation.Image = null; pbDamnation.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 火
            if (pbFlameAura != null) { pbFlameAura.Image = null; pbFlameAura.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbHeatBoost != null) { pbHeatBoost.Image = null; pbHeatBoost.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbImmortalRave != null) { pbImmortalRave.Image = null; pbImmortalRave.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 水
            if (pbAbsorbWater != null) { pbAbsorbWater.Image = null; pbAbsorbWater.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbMirrorImage != null) { pbMirrorImage.Image = null; pbMirrorImage.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbAbsoluteZero != null) { pbAbsoluteZero.Image = null; pbAbsoluteZero.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbPromisedKnowledge != null) { pbPromisedKnowledge.Image = null; pbPromisedKnowledge.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 理
            if (pbGaleWind != null) { pbGaleWind.Image = null; pbGaleWind.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbWordOfLife != null) { pbWordOfLife.Image = null; pbWordOfLife.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbWordOfFortune != null) { pbWordOfFortune.Image = null; pbWordOfFortune.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbAetherDrive != null) { pbAetherDrive.Image = null; pbAetherDrive.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbEternalPresence != null) { pbEternalPresence.Image = null; pbEternalPresence.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 空
            if (pbRiseOfImage != null) { pbRiseOfImage.Image = null; pbRiseOfImage.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbOneImmunity != null) { pbOneImmunity.Image = null; pbOneImmunity.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbDeflection != null) { pbDeflection.Image = null; pbDeflection.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbTimeStop != null) { pbTimeStop.Image = null; pbTimeStop.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // s 後編追加
            // 聖＋闇
            if (pbPsychicTrance != null) { pbPsychicTrance.Image = null; pbPsychicTrance.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbBlindJustice != null) { pbBlindJustice.Image = null; pbBlindJustice.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbTranscendentWish != null) { pbTranscendentWish.Image = null; pbTranscendentWish.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 聖＋火
            if (pbFlashBlaze != null) { pbFlashBlaze.Image = null; pbFlashBlaze.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } // 後編追加
            // 聖＋水
            if (pbSkyShield != null) { pbSkyShield.Image = null; pbSkyShield.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbEverDroplet != null) { pbEverDroplet.Image = null; pbEverDroplet.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 聖＋理
            if (pbHolyBreaker != null) { pbHolyBreaker.Image = null; pbHolyBreaker.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbExaltedField != null) { pbExaltedField.Image = null; pbExaltedField.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbHymnContract != null) { pbHymnContract.Image = null; pbHymnContract.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 聖＋空
            if (pbStarLightning != null) { pbStarLightning.Image = null; pbStarLightning.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 闇＋火
            if (pbBlackFire != null) { pbBlackFire.Image = null; pbBlackFire.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbBlazingField != null) { pbBlazingField.Image = null; pbBlazingField.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 闇＋水
            // 闇＋理
            if (pbWordOfMalice != null) { pbWordOfMalice.Image = null; pbWordOfMalice.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbSinFortune != null) { pbSinFortune.Image = null; pbSinFortune.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 闇＋空
            if (pbDarkenField != null) { pbDarkenField.Image = null; pbDarkenField.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbEclipseEnd != null) { pbEclipseEnd.Image = null; pbEclipseEnd.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 火＋水
            if (pbFrozenAura != null) { pbFrozenAura.Image = null; pbFrozenAura.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 火＋理
            if (pbEnrageBlast != null) { pbEnrageBlast.Image = null; pbEnrageBlast.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbSigilOfHomura != null) { pbSigilOfHomura.Image = null; pbSigilOfHomura.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 火＋空
            if (pbImmolate != null) { pbImmolate.Image = null; pbImmolate.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbPhantasmalWind != null) { pbPhantasmalWind.Image = null; pbPhantasmalWind.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbRedDragonWill != null) { pbRedDragonWill.Image = null; pbRedDragonWill.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 水＋理
            if (pbStaticBarrier != null) { pbStaticBarrier.Image = null; pbStaticBarrier.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbAusterityMatrix != null) { pbAusterityMatrix.Image = null; pbAusterityMatrix.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }           
            // 水＋空
            if (pbVanishWave != null) { pbVanishWave.Image = null; pbVanishWave.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbBlueDragonWill != null) { pbBlueDragonWill.Image = null; pbBlueDragonWill.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 理＋空
            if (pbSeventhMagic != null) { pbSeventhMagic.Image = null; pbSeventhMagic.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbParadoxImage != null) { pbParadoxImage.Image = null; pbParadoxImage.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            // 動＋静
            if (pbStanceOfDouble != null) { pbStanceOfDouble.Image = null; pbStanceOfDouble.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 動＋柔
            if (pbSwiftStep != null) { pbSwiftStep.Image = null; pbSwiftStep.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbVigorSense != null) { pbVigorSense.Image = null; pbVigorSense.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 動＋剛
            if (pbRisingAura != null) { pbRisingAura.Image = null; pbRisingAura.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 動＋心眼
            if (pbOnslaughtHit != null) { pbOnslaughtHit.Image = null; pbOnslaughtHit.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 動＋無心
            if (pbSmoothingMove != null) { pbSmoothingMove.Image = null; pbSmoothingMove.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbAscensionAura != null) { pbAscensionAura.Image = null; pbAscensionAura.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 静＋柔
            if (pbFutureVision != null) { pbFutureVision.Image = null; pbFutureVision.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 静＋剛
            if (pbReflexSpirit != null) { pbReflexSpirit.Image = null; pbReflexSpirit.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 静＋心眼
            if (pbConcussiveHit != null) { pbConcussiveHit.Image = null; pbConcussiveHit.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 静＋無心
            if (pbTrustSilence != null) { pbTrustSilence.Image = null; pbTrustSilence.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 柔＋剛
            if (pbStanceOfMystic != null) { pbStanceOfMystic.Image = null; pbStanceOfMystic.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 柔＋心眼
            if (pbNourishSense != null) { pbNourishSense.Image = null; pbNourishSense.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 柔＋無心
            if (pbImpulseHit != null) { pbImpulseHit.Image = null; pbImpulseHit.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 剛＋心眼
            if (pbOneAuthority != null) { pbOneAuthority.Image = null; pbOneAuthority.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 剛＋無心
            // 心眼＋無心

            // 武器特有BUFF
            if (pbFeltus != null) { pbFeltus.Image = null; pbFeltus.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbJuzaPhantasmal != null) { pbJuzaPhantasmal.Image = null; pbJuzaPhantasmal.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbEternalFateRing != null) { pbEternalFateRing.Image = null; pbEternalFateRing.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbLightServant != null) { pbLightServant.Image = null; pbLightServant.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbShadowServant != null) { pbShadowServant.Image = null; pbShadowServant.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbAdilBlueBurn != null) { pbAdilBlueBurn.Image = null; pbAdilBlueBurn.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbMazeCube != null) { pbMazeCube.Image = null; pbMazeCube.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbShadowBible != null) { pbShadowBible.Image = null; pbShadowBible.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbDetachmentOrb != null) { pbDetachmentOrb.Image = null; pbDetachmentOrb.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbDevilSummonerTome != null) { pbDevilSummonerTome.Image = null; pbDevilSummonerTome.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbVoidHymnsonia != null) { pbVoidHymnsonia.Image = null; pbVoidHymnsonia.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            // 消耗品特有
            if (pbSagePotionMini != null) { pbSagePotionMini.Image = null; pbSagePotionMini.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbGenseiTaima != null) { pbGenseiTaima.Image = null; pbGenseiTaima.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbShiningAether != null) { pbShiningAether.Image = null; pbShiningAether.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbBlackElixir != null) { pbBlackElixir.Image = null; pbBlackElixir.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbElementalSeal != null) { pbElementalSeal.Image = null; pbElementalSeal.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbColoressAntidote != null) { pbColoressAntidote.Image = null; pbColoressAntidote.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            // 集中と断絶
            if (pbSyutyuDanzetsu != null) { pbSyutyuDanzetsu.Image = null; pbSyutyuDanzetsu.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // 循環と誓約
            if (pbJunkanSeiyaku != null) { pbJunkanSeiyaku.Image = null; pbJunkanSeiyaku.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            // 最終戦ライフカウント
            if (pbLifeCount != null) { pbLifeCount.Image = null; pbLifeCount.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            // ヴェルゼ最終戦カオティックスキーマ
            if (pbChaoticSchema != null) { pbChaoticSchema.Image = null; pbChaoticSchema.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // e 後編追加

            // 負の影響効果
            CurrentPreStunning = 0;
            CurrentStunning = 0;
            CurrentSilence = 0;
            CurrentPoison = 0;
            CurrentTemptation = 0;
            CurrentFrozen = 0;
            CurrentParalyze = 0;
            CurrentNoResurrection = 0;
            CurrentSlow = 0; // 後編追加
            CurrentBlind = 0; // 後編追加
            CurrentSlip = 0; // 後編追加
            CurrentNoGainLife = 0; // 後編追加
            CurrentBlinded = 0; // 後編追加

            // s 後編追加
            battleResistStun = false;
            battleResistSilence = false;
            battleResistPoison = false;
            battleResistTemptation = false;
            battleResistFrozen = false;
            battleResistParalyze = false;
            battleResistNoResurrection = false;
            battleResistSlow = false;
            battleResistBlind = false;
            battleResistSlip = false;
            // e 後編追加

            CurrentPoisonValue = 0; // 後編追加
           
            DeadSignForTranscendentWish = false; // 後編追加

            // 正の影響効果
            CurrentSpeedBoost = 0; // 後編追加
            CurrentBlinded = 0; // 後編追加
            CurrentChargeCount = 0; // 後編追加
            CurrentPhysicalChargeCount = 0; // 後編追加

            // s 後編追加
            currentPhysicalAttackUp = 0;
            currentPhysicalAttackUpValue = 0;
            currentPhysicalAttackDown = 0;
            currentPhysicalAttackDownValue = 0;

            currentPhysicalDefenseUp = 0;
            currentPhysicalDefenseUpValue = 0;
            currentPhysicalDefenseDown = 0;
            currentPhysicalDefenseDownValue = 0;
            
            currentMagicDefenseUp = 0;
            currentMagicDefenseUpValue = 0;
            currentMagicDefenseDown = 0;
            currentMagicDefenseDownValue = 0;
            
            currentMagicAttackUp = 0;
            currentMagicAttackUpValue = 0;
            currentMagicAttackDown = 0;
            currentMagicAttackDownValue = 0;

            currentSpeedUp = 0;
            currentSpeedUpValue = 0;
            currentSpeedDown = 0;
            currentSpeedDownValue = 0;

            currentReactionUp = 0;
            currentReactionUpValue = 0;
            currentReactionDown = 0;
            currentReactionDownValue = 0;

            currentPotentialUp = 0;
            currentPotentialUpValue = 0;
            currentPotentialDown = 0;
            currentPotentialDownValue = 0;

            currentStrengthUp = 0; // 後編追加
            currentStrengthUpValue = 0; // 後編追加

            currentAgilityUp = 0; // 後編追加
            currentAgilityUpValue = 0; // 後編追加

            currentIntelligenceUp = 0; // 後編追加
            currentIntelligenceUpValue = 0; // 後編追加

            currentStaminaUp = 0; // 後編追加
            currentStaminaUpValue = 0; // 後編追加

            currentMindUp = 0; // 後編追加
            currentMindUpValue = 0; // 後編追加

            currentLightUp = 0;
            currentLightUpValue = 0;
            currentLightDown = 0;
            currentLightDownValue = 0;

            currentShadowUp = 0;
            currentShadowUpValue = 0;
            currentShadowDown = 0;
            currentShadowDownValue = 0;

            currentFireUp = 0;
            currentFireUpValue = 0;
            currentFireDown = 0;
            currentFireDownValue = 0;

            currentIceUp = 0;
            currentIceUpValue = 0;
            currentIceDown = 0;
            currentIceDownValue = 0;

            currentForceUp = 0;
            currentForceUpValue = 0;
            currentForceDown = 0;
            currentForceDownValue = 0;

            currentWillUp = 0;
            currentWillUpValue = 0;
            currentWillDown = 0;
            currentWillDownValue = 0;
            // e 後編追加

            // s 後編追加
            currentResistLightUp = 0;
            currentResistLightUpValue = 0;

            currentResistShadowUp = 0;
            currentResistShadowUpValue = 0;

            currentResistFireUp = 0;
            currentResistFireUpValue = 0;

            currentResistIceUp = 0;
            currentResistIceUpValue = 0;

            currentResistForceUp = 0;
            currentResistForceUpValue = 0;

            currentResistWillUp = 0;
            currentResistWillUpValue = 0;

            currentAfterReviveHalf = 0;
            currentFireDamage2 = 0;
            currentBlackMagic = 0;

            currentChaosDesperate = 0;
            currentChaosDesperateValue = 0;

            currentIchinaruHomura = 0;
            currentAbyssFire = 0;
            currentLightAndShadow = 0;
            currentEternalDroplet = 0;
            currentAusterityMatrixOmega = 0;
            currentVoiceOfAbyss = 0;
            currentAbyssWill = 0;
            currentAbyssWillValue = 0;
            currentTheAbyssWall = 0;

            poolLifeConsumption = 0;
            poolManaConsumption = 0;
            poolSkillConsumption = 0;
            // e 後編追加

            if (pbPreStunning != null) { pbPreStunning.Image = null; pbPreStunning.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbStun != null) { pbStun.Image = null; pbStun.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbSilence != null) { pbSilence.Image = null; pbSilence.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbPoison != null) { pbPoison.Image = null; pbPoison.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbTemptation != null) { pbTemptation.Image = null; pbTemptation.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbFrozen != null) { pbFrozen.Image = null; pbFrozen.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbParalyze != null) { pbParalyze.Image = null; pbParalyze.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbNoResurrection != null) { pbNoResurrection.Image = null; pbNoResurrection.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbSlow != null) { pbSlow.Image = null; pbSlow.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } // 後編追加
            if (pbBlind != null) { pbBlind.Image = null; pbBlind.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } // 後編追加
            if (pbSlip != null) { pbSlip.Image = null; pbSlip.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } // 後編追加
            if (pbNoGainLife != null) { pbNoGainLife.Image = null; pbNoGainLife.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } // 後編追加

            // s 後編追加
            if (pbPhysicalAttackUp != null) { pbPhysicalAttackUp.Image = null; pbPhysicalAttackUp.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } 
            if (pbPhysicalAttackDown != null) { pbPhysicalAttackDown.Image = null; pbPhysicalAttackDown.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } 
            if (pbPhysicalDefenseUp != null) { pbPhysicalDefenseUp.Image = null; pbPhysicalDefenseUp.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } 
            if (pbPhysicalDefenseDown != null) { pbPhysicalDefenseDown.Image = null; pbPhysicalDefenseDown.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } 
            if (pbMagicAttackUp != null) { pbMagicAttackUp.Image = null; pbMagicAttackUp.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } 
            if (pbMagicAttackDown != null) { pbMagicAttackDown.Image = null; pbMagicAttackDown.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } 
            if (pbMagicDefenseUp != null) { pbMagicDefenseUp.Image = null; pbMagicDefenseUp.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } 
            if (pbMagicDefenseDown != null) { pbMagicDefenseDown.Image = null; pbMagicDefenseDown.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } 
            if (pbSpeedUp != null) { pbSpeedUp.Image = null; pbSpeedUp.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } 
            if (pbSpeedDown != null) { pbSpeedDown.Image = null; pbSpeedDown.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } 
            if (pbReactionUp != null) { pbReactionUp.Image = null; pbReactionUp.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } 
            if (pbReactionDown != null) { pbReactionDown.Image = null; pbReactionDown.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } 
            if (pbPotentialUp != null) { pbPotentialUp.Image = null; pbPotentialUp.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } 
            if (pbPotentialDown != null) { pbPotentialDown.Image = null; pbPotentialDown.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } 

            if (pbStrengthUp != null) { pbStrengthUp.Image = null; pbStrengthUp.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } 
            if (pbAgilityUp != null) { pbAgilityUp.Image = null; pbAgilityUp.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } 
            if (pbIntelligenceUp != null) { pbIntelligenceUp.Image = null; pbIntelligenceUp.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbStaminaUp != null) { pbStaminaUp.Image = null; pbStaminaUp.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbMindUp != null) { pbMindUp.Image = null; pbMindUp.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            if (pbLightUp != null) { pbLightUp.Image = null; pbLightUp.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbLightDown != null) { pbLightDown.Image = null; pbLightDown.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0);}
            if (pbShadowUp != null) { pbShadowUp.Image = null; pbShadowUp.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbShadowDown != null) { pbShadowDown.Image = null; pbShadowDown.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbFireUp != null) { pbFireUp.Image = null; pbFireUp.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbFireDown != null) { pbFireDown.Image = null; pbFireDown.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbIceUp != null) { pbIceUp.Image = null; pbIceUp.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbIceDown != null) { pbIceDown.Image = null; pbIceDown.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbForceUp != null) { pbForceUp.Image = null; pbForceUp.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbForceDown != null) { pbForceDown.Image = null; pbForceDown.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbWillUp != null) { pbWillUp.Image = null; pbWillUp.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbWillDown != null) { pbWillDown.Image = null; pbWillDown.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            if (pbResistLightUp != null) { pbResistLightUp.Image = null; pbResistLightUp.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistShadowUp != null) { pbResistShadowUp.Image = null; pbResistShadowUp.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistFireUp != null) { pbResistFireUp.Image = null; pbResistFireUp.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistIceUp != null) { pbResistIceUp.Image = null; pbResistIceUp.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistForceUp != null) { pbResistForceUp.Image = null; pbResistForceUp.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistWillUp != null) { pbResistWillUp.Image = null; pbResistWillUp.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            if (pbAfterReviveHalf != null) { pbAfterReviveHalf.Image = null; pbAfterReviveHalf.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbFireDamage2 != null) { pbFireDamage2.Image = null; pbFireDamage2.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbBlackMagic != null) { pbBlackMagic.Image = null; pbBlackMagic.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbChaosDesperate != null) { pbChaosDesperate.Image = null; pbChaosDesperate.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbIchinaruHomura != null) { pbIchinaruHomura.Image = null; pbIchinaruHomura.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbAbyssFire != null) { pbAbyssFire.Image = null; pbAbyssFire.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbLightAndShadow != null) { pbLightAndShadow.Image = null; pbLightAndShadow.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbEternalDroplet != null) { pbEternalDroplet.Image = null; pbEternalDroplet.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbAusterityMatrixOmega != null) { pbAusterityMatrixOmega.Image = null; pbAusterityMatrixOmega.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbVoiceOfAbyss != null) { pbVoiceOfAbyss.Image = null; pbVoiceOfAbyss.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbAbyssWill != null) { pbAbyssWill.Image = null; pbAbyssWill.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbTheAbyssWall != null) { pbTheAbyssWall.Image = null; pbTheAbyssWall.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            if (pbResistStun != null) { pbResistStun.Image = null; pbResistStun.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistSilence != null) { pbResistSilence.Image = null; pbResistSilence.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistPoison != null) { pbResistPoison.Image = null; pbResistPoison.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistTemptation != null) { pbResistTemptation.Image = null; pbResistTemptation.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistFrozen != null) { pbResistFrozen.Image = null; pbResistFrozen.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistParalyze != null) { pbResistParalyze.Image = null; pbResistParalyze.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistNoResurrection != null) { pbResistNoResurrection.Image = null; pbResistNoResurrection.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistSlow != null) { pbResistSlow.Image = null; pbResistSlow.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistBlind != null) { pbResistBlind.Image = null; pbResistBlind.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbResistSlip != null) { pbResistSlip.Image = null; pbResistSlip.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // e 後編追加

            // s 後編追加
            // BUFFUP効果を解除（敵専用)
            if (pbBuff1 != null) { pbBuff1.Image = null; pbBuff1.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbBuff2 != null) { pbBuff2.Image = null; pbBuff2.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbBuff3 != null) { pbBuff3.Image = null; pbBuff3.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // e 後編追加

            // BUFFUP効果を解除
            BuffStrength_BloodyVengeance = 0;
            BuffAgility_HeatBoost = 0;
            BuffIntelligence_PromisedKnowledge = 0;
            BuffStamina_Unknown = 0;
            BuffMind_RiseOfImage = 0;

            BuffStrength_HighEmotionality = 0;
            BuffAgility_HighEmotionality = 0;
            BuffIntelligence_HighEmotionality = 0;
            BuffStamina_HighEmotionality = 0;
            BuffMind_HighEmotionality = 0;

            BuffStrength_VoidExtraction = 0;
            BuffAgility_VoidExtraction = 0;
            BuffIntelligence_VoidExtraction = 0;
            BuffStamina_VoidExtraction = 0;
            BuffMind_VoidExtraction = 0;

            // s 後編追加
            BuffStrength_TranscendentWish = 0;
            BuffAgility_TranscendentWish = 0;
            BuffIntelligence_TranscendentWish = 0;
            BuffStamina_TranscendentWish = 0;
            BuffMind_TranscendentWish = 0;

            BuffStrength_Hiyaku_Kassei = 0;
            BuffAgility_Hiyaku_Kassei = 0;
            BuffIntelligence_Hiyaku_Kassei = 0;
            BuffStamina_Hiyaku_Kassei = 0;
            BuffMind_Hiyaku_Kassei = 0;
            // e 後編追加

            // [食事効果は戦闘終了後も継続される] 後編追加
            // buffStrength_Food = 0;
            // buffAgility_Food = 0;
            // buffIntelligence_Food = 0;
            // buffStamina_Food = 0;
            // buffMind_Food = 0;

            actionDecision = false; // 後編追加
            decisionTiming = 0; // 後編追加
            currentInstantPoint = 0; // 後編追加 // 「コメント」初期直感ではMAX値に戻しておくほうがいいと思ったが、プレイしてみてはじめは０のほうが、ゲーム性は面白く感じられると思った。
            realTimeBattle = false; // 後編追加
            stackActivation = false; // 後編追加
            stackActivePlayer = null; // 後編追加
            stackTarget = null; // 後編追加
            stackPlayerAction = PlayerAction.None; // 後編追加
            stackCommandString = string.Empty; // 後編追加
            //shadowStackActivePlayer = null; // 後編追加
            //shadowStackTarget = null; // 後編追加
            //shadowStackPlayerAction = PlayerAction.None; // 後編追加
            //shadowStackCommandString = String.Empty; // 後編追加
            BuffNumber = 0; // 後編追加

            amplifyPhysicalAttack = 0.0f; // 後編追加
            amplifyPhysicalDefense = 0.0f; // 後編追加
            amplifyMagicAttack = 0.0f; // 後編追加
            amplifyMagicDefense = 0.0f; // 後編追加
            amplifyBattleSpeed = 0.0f; // 後編追加
            amplifyBattleResponse = 0.0f; // 後編追加
            amplifyPotential = 0.0f; // 後編追加

            currentLifeCountValue = 0; // 後編追加

            reserveBattleCommand = String.Empty; // 後編追加
            nowExecActionFlag = false; // 後編追加

            if ((this.MainWeapon != null) && (this.MainWeapon.AfterBroken)) { brokenName = this.MainWeapon.Name; this.MainWeapon = null; }
            if ((this.SubWeapon != null) && (this.SubWeapon.AfterBroken)) { brokenName = this.SubWeapon.Name; this.SubWeapon = null; }
            if ((this.MainArmor != null) && (this.MainArmor.AfterBroken)) { brokenName = this.MainArmor.Name; this.MainArmor = null; }
            if ((this.Accessory != null) && (this.Accessory.AfterBroken)) { brokenName = this.Accessory.Name; this.Accessory = null; }
            if ((this.Accessory2 != null) && (this.Accessory2.AfterBroken)) { brokenName = this.Accessory2.Name; this.Accessory2 = null; }

            if (this.MainWeapon != null) { this.MainWeapon.CleanUpStatus(); }
            if (this.SubWeapon != null) { this.SubWeapon.CleanUpStatus(); }
            if (this.MainArmor != null) { this.MainArmor.CleanUpStatus(); }
            if (this.Accessory != null) { this.Accessory.CleanUpStatus(); }
            if (this.Accessory2 != null) { this.Accessory2.CleanUpStatus(); }

        }

        // s 後編追加
        public void ActivateCharacter()
        {
            if (this.labelName != null) this.labelName.Visible = true;
            if (this.labelLife != null) this.labelLife.Visible = true;
            if (this.labelCurrentSkillPoint != null) { this.labelCurrentSkillPoint.Visible = true; this.labelCurrentSkillPoint.BringToFront();}
            if (this.labelCurrentManaPoint != null) { this.labelCurrentManaPoint.Visible = true; this.labelCurrentManaPoint.BringToFront(); }
            if (this.labelCurrentInstantPoint != null) { this.labelCurrentInstantPoint.Visible = true; this.labelCurrentInstantPoint.BringToFront(); }
            if (this.labelCurrentSpecialInstant != null) { this.labelCurrentSpecialInstant.Visible = true; this.labelCurrentSpecialInstant.BringToFront(); }
            if (this.ActionButton1 != null) this.ActionButton1.Visible = true;
            if (this.ActionButton2 != null) this.ActionButton2.Visible = true;
            if (this.ActionButton3 != null) this.ActionButton3.Visible = true;
            if (this.ActionButton4 != null) this.ActionButton4.Visible = true;
            if (this.ActionButton5 != null) this.ActionButton5.Visible = true;
            if (this.ActionButton6 != null) this.ActionButton6.Visible = true;
            if (this.ActionButton7 != null) this.ActionButton7.Visible = true;
            if (this.ActionButton8 != null) this.ActionButton8.Visible = true;
            if (this.ActionButton9 != null) this.ActionButton9.Visible = true;
            if (this.ActionLabel != null) this.ActionLabel.Visible = true;
            if (this.MainObjectButton != null) this.MainObjectButton.Visible = true;
            //if (this.pbTargetTarget != null) this.pbTargetTarget.Visible = true;
            if (this.pbBuff1 != null) this.pbBuff1.Visible = true;
            if (this.pbBuff2 != null) this.pbBuff2.Visible = true;
            if (this.pbBuff3 != null) this.pbBuff3.Visible = true;
        }
        public void DisactiveCharacter()
        {
            if (this.labelName != null) this.labelName.Visible = false;
            if (this.labelLife != null) this.labelLife.Visible = false;
            if (this.labelCurrentSkillPoint != null) this.labelCurrentSkillPoint.Visible = false;
            if (this.labelCurrentManaPoint != null) this.labelCurrentManaPoint.Visible = false;
            if (this.labelCurrentInstantPoint != null) this.labelCurrentInstantPoint.Visible = false;
            if (this.labelCurrentSpecialInstant != null) this.labelCurrentSpecialInstant.Visible = false;
            if (this.ActionButton1 != null) this.ActionButton1.Visible = false;
            if (this.ActionButton2 != null) this.ActionButton2.Visible = false;
            if (this.ActionButton3 != null) this.ActionButton3.Visible = false;
            if (this.ActionButton4 != null) this.ActionButton4.Visible = false;
            if (this.ActionButton5 != null) this.ActionButton5.Visible = false;
            if (this.ActionButton6 != null) this.ActionButton6.Visible = false;
            if (this.ActionButton7 != null) this.ActionButton7.Visible = false;
            if (this.ActionButton8 != null) this.ActionButton8.Visible = false;
            if (this.ActionButton9 != null) this.ActionButton9.Visible = false;
            if (this.ActionLabel != null) this.ActionLabel.Visible = false;
            if (this.MainObjectButton != null) this.MainObjectButton.Visible = false;
            if (this.pbTargetTarget != null) this.pbTargetTarget.Visible = false;
            if (this.pbBuff1 != null) this.pbBuff1.Visible = false;
            if (this.pbBuff2 != null) this.pbBuff2.Visible = false;
            if (this.pbBuff3 != null) this.pbBuff3.Visible = false;
        }

        public void DeadPlayer()
        {
            this.Dead = true;
            this.deadSignForTranscendentWish = false;

            if (this.MainObjectButton != null)
            {
                // 【要検討】リザレクション対象にするため、生死に関わらず、対象とする事を許可
                //this.MainObjectButton.Enabled = false; // delete
                this.MainObjectButton.BackColor = Color.DarkSlateGray;
            }
            if (this.pbTargetTarget != null)
            {
                this.pbTargetTarget.BackColor = Color.DarkSlateGray;
            }
            if (this.labelName != null) this.labelName.ForeColor = System.Drawing.Color.Red;
            if (this.labelLife != null) this.labelLife.ForeColor = System.Drawing.Color.Red;
        }

        public void ResurrectPlayer(int life)
        {
            if (this.CurrentNoResurrection <= 0)
            {
                this.currentLife = life;
                this.dead = false;
                if (this.MainObjectButton != null)
                {
                    this.MainObjectButton.Enabled = true;
                    this.MainObjectButton.BackColor = this.MainColor;
                }
                if (this.pbTargetTarget != null)
                {
                    this.pbTargetTarget.BackColor = this.MainColor;
                }
                if (this.labelName != null) { this.labelName.ForeColor = System.Drawing.Color.Black; }
                if (this.labelLife != null) { this.labelLife.ForeColor = System.Drawing.Color.Black; this.labelLife.Text = this.currentLife.ToString(); }
            }
        }

        public double AmplifyMagicByEquipment(double damage, TruthActionCommand.MagicType type)
        {
            List<ItemBackPack> equipList = new List<ItemBackPack>();
            if (this.MainWeapon != null) { equipList.Add(this.MainWeapon); }
            if (this.SubWeapon != null) { equipList.Add(this.SubWeapon); }
            if (this.MainArmor != null) { equipList.Add(this.MainArmor); }
            if (this.Accessory != null) { equipList.Add(this.Accessory); }
            if (this.Accessory2 != null) { equipList.Add(this.Accessory2); }

            if (TruthActionCommand.IsLight(type))
            {
                for (int ii = 0; ii < equipList.Count; ii++)
                {
                    if (equipList[ii].AmplifyLight > 0) { damage = damage * equipList[ii].AmplifyLight; }
                }
            }
            if (TruthActionCommand.IsShadow(type))
            {
                for (int ii = 0; ii < equipList.Count; ii++)
                {
                    if (equipList[ii].AmplifyShadow > 0) { damage = damage * equipList[ii].AmplifyShadow; }
                }
            }
            if (TruthActionCommand.IsFire(type))
            {
                for (int ii = 0; ii < equipList.Count; ii++)
                {
                    if (equipList[ii].AmplifyFire > 0) { damage = damage * equipList[ii].AmplifyFire; }
                }
            }
            if (TruthActionCommand.IsIce(type))
            {
                for (int ii = 0; ii < equipList.Count; ii++)
                {
                    if (equipList[ii].AmplifyIce > 0) { damage = damage * equipList[ii].AmplifyIce; }
                }
            }
            if (TruthActionCommand.IsForce(type))
            {
                for (int ii = 0; ii < equipList.Count; ii++)
                {
                    if (equipList[ii].AmplifyForce > 0) { damage = damage * equipList[ii].AmplifyForce; }
                }
            }
            if (TruthActionCommand.IsWill(type))
            {
                for (int ii = 0; ii < equipList.Count; ii++)
                {
                    if (equipList[ii].AmplifyWill > 0) { damage = damage * equipList[ii].AmplifyWill; }
                }
            }

            return damage;
        }

        /// <summary>
        /// 正のスキル効果をリムーブ
        /// </summary>
        public void RemoveBuffSkill()
        {
            // 基本スキル
            this.RemoveAntiStun();
            this.RemoveStanceOfDeath();
            this.RemoveTruthVision();
            this.RemovePainfulInsanity(); // ダメージ系統だが対象は自分自身なのでUP扱い
            this.RemoveVoidExtraction();
            this.RemoveNothingOfNothingness();
            // 複合スキル
            this.RemoveRisingAura();
            this.RemoveAscensionAura();
            this.RemoveReflexSpirit();
            this.RemoveTrustSilence();
            this.RemoveStanceOfMystic();
            this.RemoveNourishSense();
        }

        /// <summary>
        /// 負のスキル効果をリムーブ
        /// </summary>
        public void RemoveDebuffSkill()
        {
            // 基本スキル
            // 複合スキル
            this.RemoveOnslaughtHit();
            this.RemoveConcussiveHit();
            this.RemoveImpulseHit();
        }

        /// <summary>
        /// 正の魔法効果をリムーブ
        /// </summary>
        public void RemoveBuffSpell()
        {
            // 基本スペル
            this.RemoveProtection();
            this.RemoveSaintPower();
            this.RemoveAbsorbWater();
            this.RemoveShadowPact();
            this.RemoveEternalPresence();
            this.RemoveBloodyVengeance();
            this.RemoveHeatBoost();
            this.RemovePromisedKnowledge();
            this.RemoveRiseOfImage();
            this.RemoveWordOfLife();
            this.RemoveFlameAura();
            // 複合スペル
            this.RemovePsychicTrance();
            this.RemoveBlindJustice();
            this.RemoveSkyShield();
            this.RemoveEverDroplet();
            this.RemoveHolyBreaker();
            this.RemoveExaltedField();
            this.RemoveFrozenAura();
            this.RemovePhantasmalWind();
            this.RemoveRedDragonWill();
            this.RemoveStaticBarrier();
            this.RemoveBlueDragonWill();
            this.RemoveSeventhMagic();
            this.RemoveParadoxImage();
        }

        /// <summary>
        /// 負の魔法効果をリムーブ
        /// </summary>
        public void RemoveDebuffSpell()
        {
            // 基本スペル
            this.RemoveDamnation();
            this.RemoveAbsoluteZero();
            // 複合スペル
            this.RemoveFlashBlaze();
            this.RemoveStarLightning();
            this.RemoveBlackFire();
            this.RemoveBlazingField();
            this.RemoveDemonicIgnite();
            this.RemoveWordOfMalice();
            this.RemoveDarkenField();
            this.RemoveChillBurn();
            this.RemoveEnrageBlast();
            //this.RemoveSigilOfHomura();
            this.RemoveImmolate();
            //this.RemoveAusterityMatrix();
            this.RemoveVanishWave();
            this.RemoveVortexField();
        }

        /// <summary>
        /// 正のパラメタ効果をリムーブ
        /// </summary>
        public void RemoveBuffParam()
        {
            this.RemovePhysicalAttackUp();
            this.RemovePhysicalDefenseUp();
            this.RemoveMagicAttackUp();
            this.RemoveMagicDefenseUp();
            this.RemoveSpeedUp();
            this.RemoveReactionUp();
            this.RemovePotentialUp();
        }


        /// <summary>
        /// 負のパラメタ効果をリムーブ
        /// </summary>
        public void RemoveDebuffParam()
        {
            this.RemovePhysicalAttackDown();
            this.RemovePhysicalDefenseDown();
            this.RemoveMagicAttackDown();
            this.RemoveMagicDefenseDown();
            this.RemoveSpeedDown();
            this.RemoveReactionDown();
            this.RemovePotentialDown();
        }

        /// <summary>
        /// 正の影響効果をリムーブ
        /// </summary>
        public void RemoveBuffEffect()
        {
            this.RemoveBlinded();
            this.RemoveSpeedBoost();
            this.RemoveChargeCount();
            this.RemovePhysicalChargeCount();
        }

        /// <summary>
        /// 負の影響効果をリムーブ
        /// </summary>
        public void RemoveDebuffEffect()
        {
            this.RemovePreStunning();
            this.RemoveStun();
            this.RemoveSilence();
            this.RemovePoison();
            this.RemoveTemptation();
            this.RemoveFrozen();
            this.RemoveParalyze();
            this.RemoveNoResurrection();
            this.RemoveSlow();
            this.RemoveBlind();
            this.RemoveSlip();
        }

        private void UpdateBattleText(string text)
        {
            this.TextBattleMessage.Text = this.TextBattleMessage.Text.Insert(0, text);
            this.TextBattleMessage.Update();
        }

        /// <summary>
        /// 力パラメタ上昇BUFF
        /// </summary>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        public void BuffUpStrength(double effectValue, int turn = 999)
        {
            UpdateBattleText(this.Name + "は【力】が" + ((int)effectValue).ToString() + "上昇\r\n");
            this.CurrentStrengthUp = turn;
            this.CurrentStrengthUpValue = (int)effectValue;
            this.ActivateBuff(this.pbStrengthUp, Database.BaseResourceFolder + Database.BUFF_STRENGTH_UP, turn);
        }

        /// <summary>
        /// 技パラメタ上昇BUFF
        /// </summary>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        public void BuffUpAgility(double effectValue, int turn = 999)
        {
            UpdateBattleText(this.Name + "は【技】が" + ((int)effectValue).ToString() + "上昇\r\n");
            this.CurrentAgilityUp = turn;
            this.CurrentAgilityUpValue = (int)effectValue;
            this.ActivateBuff(this.pbAgilityUp, Database.BaseResourceFolder + Database.BUFF_AGILITY_UP, turn);
        }

        /// <summary>
        /// 知パラメタ上昇BUFF
        /// </summary>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        public void BuffUpIntelligence(double effectValue, int turn = 999)
        {
            UpdateBattleText(this.Name + "は【知】が" + ((int)effectValue).ToString() + "上昇\r\n");
            this.CurrentIntelligenceUp = turn;
            this.CurrentIntelligenceUpValue = (int)effectValue;
            this.ActivateBuff(this.pbIntelligenceUp, Database.BaseResourceFolder + Database.BUFF_INTELLIGENCE_UP, turn);
        }

        /// <summary>
        /// 体パラメタ上昇BUFF
        /// </summary>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        public void BuffUpStamina(double effectValue, int turn = 999)
        {
            UpdateBattleText(this.Name + "は【体】が" + ((int)effectValue).ToString() + "上昇\r\n");
            this.CurrentStaminaUp = turn;
            this.CurrentStaminaUpValue = (int)effectValue;
            this.ActivateBuff(this.pbStaminaUp, Database.BaseResourceFolder + Database.BUFF_STAMINA_UP, turn);
        }

        /// <summary>
        /// 心パラメタ上昇BUFF
        /// </summary>
        /// <param name="effectValue">効果の値</param>
        /// <param name="turn">ターン数（指定しない場合は999ターン）</param>
        public void BuffUpMind(double effectValue, int turn = 999)
        {
            UpdateBattleText(this.Name + "は【心】が" + ((int)effectValue).ToString() + "上昇\r\n");
            this.CurrentMindUp = turn;
            this.CurrentMindUpValue = (int)effectValue;
            this.ActivateBuff(this.pbMindUp, Database.BaseResourceFolder + Database.BUFF_MIND_UP, turn);
        }

        public void BuffUpAmplifyPhysicalAttack(double effectValue, int turn = 999)
        {
            this.amplifyPhysicalAttack = effectValue;
            this.ActivateBuff(this.pbPhysicalAttackUp, Database.BaseResourceFolder + Database.BUFF_PHYSICAL_ATTACK_UP, turn);
        }

        public void BuffUpAmplifyPhysicalDefence(double effectValue, int turn = 999)
        {
            this.amplifyPhysicalDefense = effectValue;
            this.ActivateBuff(this.pbPhysicalDefenseUp, Database.BaseResourceFolder + Database.BUFF_PHYSICAL_DEFENSE_UP, turn);
        }

        public void BuffUpAmplifyMagicAttack(double effectValue, int turn = 999)
        {
            this.amplifyMagicAttack = effectValue;
            this.ActivateBuff(this.pbMagicAttackUp, Database.BaseResourceFolder + Database.BUFF_MAGIC_ATTACK_UP, turn);
        }

        public void BuffUpAmplifyMagicDefense(double effectValue, int turn = 999)
        {
            this.amplifyMagicDefense = effectValue;
            this.ActivateBuff(this.pbMagicDefenseUp, Database.BaseResourceFolder + Database.BUFF_MAGIC_DEFENSE_UP, turn);
        }

        public void BuffUpAmplifyBattleSpeed(double effectValue, int turn = 999)
        {
            this.amplifyBattleSpeed = effectValue;
            this.ActivateBuff(this.pbSpeedUp, Database.BaseResourceFolder + Database.BUFF_SPEED_UP, turn);
        }

        public void BuffUpAmplifyBattleResponse(double effectValue, int turn = 999)
        {
            this.amplifyBattleResponse = effectValue;
            this.ActivateBuff(this.pbReactionUp, Database.BaseResourceFolder + Database.BUFF_REACTION_UP, turn);                 
        }

        public void BuffUpAmplifyPotential(double effectValue, int turn = 999)
        {
            this.amplifyPotential = effectValue;
            this.ActivateBuff(this.pbPotentialUp, Database.BaseResourceFolder + Database.BUFF_POTENTIAL_UP, turn);
        }
        // e 後編追加
    }
}
