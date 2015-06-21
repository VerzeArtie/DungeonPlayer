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
        None = 0, // �ҋ@�i�Ȃɂ����Ȃ��j
        NormalAttack = 1, // �ʏ�U��
        UseSkill = 2, // �X�L���g�p
        RunAway = 3, // ������
        UseItem = 4, // �A�C�e���g�p
        UseSpell = 5, // ���@�g�p
        Defense = 6, // �h��
        SpecialSkill = 7, // ��Ғǉ� �G��p�X�L��
        Charge = 8, // ��Ғǉ� ��ł��߂�
        Archetype = 9, // ��Ғǉ� ���j�g�p
    }

    public enum PlayerStance
    {
        None = 0,
        FrontOffence = 1, // �A�C���A�I���A���i�A���F���[
        FrontDefense = 2, // �A�C���A�I��
        BackOffence = 3, // ���i�A���F���[
        BackSupport = 4, // �A�C���A���i�A�I���A���F���[
        AllRounder = 5, // ���F���[��p
    }

    public enum AdditionalSpellType
    {
        None, // �A�C���A�������@�����𖢑I��
        Type1, // ��F�A�C���u���v��ǉ��I��
        Type2, // ��F�A�C���u�Łv��ǉ��I��
    }

    public enum AdditionalSkillType
    {
        None, // �A�C���A�����X�L�������𖢑I��
        Type1, // ��F�A�C���u�Áv��ǉ��I��
        Type2, // ��F�A�C���u�_�v��ǉ��I��
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

        protected int baseResistFire = 0; // ��Ғǉ�
        protected int baseResistIce = 0; // ��Ғǉ�
        protected int baseResistLight = 0; // ��Ғǉ�
        protected int baseResistShadow = 0; // ��Ғǉ�
        protected int baseResistForce = 0; // ��Ғǉ�
        protected int baseResistWill = 0; // ��Ғǉ�

        // Debuff�ϐ�
        protected bool battleResistStun = false; // ��Ғǉ�
        protected bool battleResistSilence = false; // ��Ғǉ�
        protected bool battleResistPoison = false; // ��Ғǉ�
        protected bool battleResistTemptation = false; // ��Ғǉ�
        protected bool battleResistFrozen = false; // ��Ғǉ�
        protected bool battleResistParalyze = false; // ��Ғǉ�
        protected bool battleResistSlow = false; // ��Ғǉ�
        protected bool battleResistBlind = false; // ��Ғǉ�
        protected bool battleResistSlip = false; // ��Ғǉ�
        protected bool battleResistNoResurrection = false; // ��Ғǉ�

        protected int level = 1;
        protected int experience = 0;
        protected int baseLife = 50;
        protected int currentLife = 50;
        protected int baseSkillPoint = 100;
        protected int currentSkillPoint = 100;
        protected int baseInstantPoint = 1000; // ��Ғǉ�
        protected int baseSpecialInstant = 20000; // ��Ғǉ�
        protected double currentInstantPoint = 0; // ��Ғǉ�// �u�R�����g�v���������ł�MAX�l�ɖ߂��Ă����ق��������Ǝv�������A�v���C���Ă݂Ă͂��߂͂O�̂ق����A�Q�[�����͖ʔ�����������Ǝv�����B
        protected double currentSpecialInstant = 0; // ��Ғǉ�
        protected int gold = 0;
        protected PlayerStance stance = PlayerStance.None; // ��Ғǉ�
        protected AdditionalSpellType additionSpellType = AdditionalSpellType.None; // ��Ғǉ�
        protected AdditionalSkillType additionSkillType = AdditionalSkillType.None; // ��Ғǉ�

        protected ItemBackPack iw;
        protected ItemBackPack sw; // ��Ғǉ�
        protected ItemBackPack ia;
        protected ItemBackPack accessory;
        protected ItemBackPack accessory2; // ��Ғǉ�
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
        protected string currentArchetypeName = String.Empty; // ��Ғǉ�
        protected MainCharacter target = null;
        protected MainCharacter target2 = null; // ��Ғǉ�
        protected PlayerAction beforePA = PlayerAction.None;
        protected string beforeUsingItem = String.Empty;
        protected string beforeSkillName = String.Empty;
        protected string beforeSpellName = String.Empty;
        protected string beforeArchetypeName = String.Empty; // ��Ғǉ�
        protected MainCharacter beforeTarget = null;
        protected MainCharacter beforeTarget2 = null; // ��Ғǉ�
        protected bool alreadyPlayArchetype = false; // ��Ғǉ�

        // ��
        // ��
        protected int currentCounterAttack = 0; // ��ҕҏW
        protected int currentStanceOfStanding = 0; // ��ҕҏW
        protected int currentAntiStun = 0;
        protected int currentStanceOfDeath = 0;
        // �_
        protected int currentStanceOfFlow = 0;
        // ��
        // �S��
        protected int currentTruthVision = 0;
        protected int currentHighEmotionality = 0;
        protected int currentStanceOfEyes = 0;  // ��ҕҏW
        protected int currentPainfulInsanity = 0;
        // ���S
        protected int currentNegate = 0; // ��ҕҏW
        protected int currentVoidExtraction = 0;
        protected int currentNothingOfNothingness = 0;
        // ���{��
        protected int currentStanceOfDouble = 0;
        // ���{�_
        protected int currentSwiftStep = 0;
        protected int currentVigorSense = 0;
        // ���{��
        protected int currentRisingAura = 0;
        // ���{�S��
        protected int currentOnslaughtHit = 0;
        // ���{���S
        protected int currentSmoothingMove = 0;
        protected int currentAscensionAura = 0;
        // �Á{�_
        protected int currentFutureVision = 0;
        // �Á{��
        protected int currentReflexSpirit = 0;
        // �Á{�S��
        protected int currentConcussiveHit = 0;
        // �Á{���S
        protected int currentTrustSilence = 0;
        // �_�{��
        protected int currentStanceOfMystic = 0;
        // �_�{�S��
        protected int currentNourishSense = 0;
        // �_�{���S
        protected int currentImpulseHit = 0;
        // ���{�S��
        protected int currentOneAuthority = 0;
        // ���{���S
        protected bool currentHardestParry = false;
        // �S��{���S
        protected bool currentStanceOfSuddenness = false;

        // ��
        protected int currentProtection = 0;
        protected int currentSaintPower = 0;
        protected int currentGlory = 0;
        // ��
        protected int currentShadowPact = 0;
        protected int currentBlackContract = 0;
        protected int currentBloodyVengeance = 0;
        protected int currentDamnation = 0;
        protected int currentDamnationFactor = 0;
        // ��
        protected int currentFlameAura = 0;
        protected int currentHeatBoost = 0;
        protected int currentImmortalRave = 0;
        // ��
        protected int currentAbsorbWater = 0;
        protected int currentMirrorImage = 0;
        protected int currentPromisedKnowledge = 0;
        protected int currentAbsoluteZero = 0;
        // ��
        protected int currentGaleWind = 0;
        protected int currentWordOfLife = 0;
        protected int currentWordOfFortune = 0;
        protected int currentAetherDrive = 0;
        protected int currentEternalPresence = 0;
        // ��
        protected int currentRiseOfImage = 0;
        protected int currentDeflection = 0;
        protected int currentOneImmunity = 0;
        protected int currentTimeStop = 0; // ��Ғǉ�
        protected bool currentTimeStopImmediate = false; // ��Ғǉ�

        // s ��Ғǉ�
        // ���{��
        protected int currentPsychicTrance = 0;
        protected int currentBlindJustice = 0;
        protected int currentTranscendentWish = 0;
        // ���{��
        protected int currentSkyShield = 0;
        protected int currentEverDroplet = 0;
        // ���{��
        protected int currentHolyBreaker = 0;
        protected int currentExaltedField = 0;
        protected int currentHymnContract = 0;
        // ���{��
        protected int currentStarLightning = 0;
        // �Ł{��
        protected int currentBlackFire = 0;
        protected int currentBlazingField = 0;
        protected int currentBlazingFieldFactor = 0;
        // �Ł{��
        protected bool currentDeepMirror = false;
        // �Ł{��
        protected int currentWordOfMalice = 0;
        protected int currentSinFortune = 0;
        // �Ł{��
        protected int currentDarkenField = 0;
        protected int currentEclipseEnd = 0;
        // �΁{��
        protected int currentFrozenAura = 0;
        // �΁{��
        protected int currentEnrageBlast = 0;
        protected int currentSigilOfHomura = 0;
        // �΁{��
        protected int currentImmolate = 0;
        protected int currentPhantasmalWind = 0;
        protected int currentRedDragonWill = 0;
        // ���{��
        protected int currentStaticBarrier = 0;
        protected int currentAusterityMatrix = 0;
        // ���{��
        protected int currentBlueDragonWill = 0;
        // ���{��
        protected int currentSeventhMagic = 0;
        protected int currentParadoxImage = 0;
        // e ��Ғǉ�

        // �W���ƒf��
        protected int currentSyutyu_Danzetsu = 0;
        protected int currentJunkan_Seiyaku = 0;        

        // ������L��BUFF
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

        // ���Օi���L��BUFF
        protected int currentSagePotionMini = 0;
        protected int currentGenseiTaima = 0;
        protected int currentShiningAether = 0;
        protected int currentBlackElixir = 0;
        protected int currentElementalSeal = 0;
        protected int currentColoressAntidote = 0;

        // �ŏI�탉�C�t�J�E���g
        protected int currentLifeCount = 0;

        // �ŏI�탔�F���[�̃J�I�e�B�b�N�X�L�[�}
        protected int currentChaoticSchema = 0;

        // �e���@�E�X�L���E�A�N�Z�T���ɂ��p�����^�t�o
        protected int buffStrength_BloodyVengeance = 0;
        protected int buffAgility_HeatBoost = 0;
        protected int buffIntelligence_PromisedKnowledge = 0;
        protected int buffStamina_Unknown = 0; // [����]�FStamina��BUFFUP�X�y�����\�z
        protected int buffMind_RiseOfImage = 0;

        protected int buffStrength_VoidExtraction = 0;
        protected int buffAgility_VoidExtraction = 0;
        protected int buffIntelligence_VoidExtraction = 0;
        protected int buffStamina_VoidExtraction = 0; // [����]�FStamina��BuffUP�X�L�����\�z
        protected int buffMind_VoidExtraction = 0;

        protected int buffStrength_HighEmotionality = 0;
        protected int buffAgility_HighEmotionality = 0;
        protected int buffIntelligence_HighEmotionality = 0;
        protected int buffStamina_HighEmotionality = 0; // [����]�FStamina��BuffUP�X�L�����\�z
        protected int buffMind_HighEmotionality = 0;

        // s ��Ғǉ�
        protected int buffStrength_TranscendentWish = 0;
        protected int buffAgility_TranscendentWish = 0;
        protected int buffIntelligence_TranscendentWish = 0;
        protected int buffStamina_TranscendentWish = 0;
        protected int buffMind_TranscendentWish = 0;
        // e ��Ғǉ�

        // s ��Ғǉ�
        protected int buffStrength_Hiyaku_Kassei = 0;
        protected int buffAgility_Hiyaku_Kassei = 0;
        protected int buffIntelligence_Hiyaku_Kassei = 0;
        protected int buffStamina_Hiyaku_Kassei = 0;
        protected int buffMind_Hiyaku_Kassei = 0;
        // e ��Ғǉ�

        // s ��Ғǉ�
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
        // e ��Ғǉ�

        protected int buffStrength_Accessory = 0;
        protected int buffAgility_Accessory = 0;
        protected int buffIntelligence_Accessory = 0;
        protected int buffStamina_Accessory = 0;
        protected int buffMind_Accessory = 0;
        // s ��Ғǉ�
        protected int buffStrength_Accessory2 = 0;
        protected int buffAgility_Accessory2 = 0;
        protected int buffIntelligence_Accessory2 = 0;
        protected int buffStamina_Accessory2 = 0;
        protected int buffMind_Accessory2 = 0;
        // e ��Ғǉ�

        // s ��Ғǉ�
        protected int buffStrength_Food = 0;
        protected int buffAgility_Food = 0;
        protected int buffIntelligence_Food = 0;
        protected int buffStamina_Food = 0;
        protected int buffMind_Food = 0;
        // e ��Ғǉ�

        // �퓬���̒l����
        protected double amplifyPhysicalAttack = 0.0f;
        protected double amplifyMagicAttack = 0.0f;
        protected double amplifyPhysicalDefense = 0.0f;
        protected double amplifyMagicDefense = 0.0f;
        protected double amplifyBattleSpeed = 0.0f;
        protected double amplifyBattleResponse = 0.0f;
        protected double amplifyPotential = 0.0f;

        // ���̉e�����ʁi�{�X��p�A�Ώۍs�������Ǝ��s���X�^���ɂȂ�j
        protected int currentPreStunning = 0;
        // ���̉e������
        protected int currentStunning = 0;
        protected int currentSilence = 0;
        protected int currentPoison = 0;
        protected int currentTemptation = 0;
        protected int currentFrozen = 0;
        protected int currentParalyze = 0;
        protected int currentNoResurrection = 0; // ��ҕҏW�i�X�y���C���j
        protected int currentSlow = 0; // ��Ғǉ�
        protected int currentBlind = 0; // ��Ғǉ�
        protected int currentSlip = 0; // ��Ғǉ�
        protected int currentNoGainLife = 0; // ��Ғǉ�
        // ���̉e������
        protected int currentBlinded = 0; // ��Ғǉ�
        protected int currentSpeedBoost = 0; // ��Ғǉ� (�ꎞ�I�ɃX�s�[�hUP������C�̌��ʂ����҂������́j
        protected int currentChargeCount = 0; // ��Ғǉ�
        protected int currentPhysicalChargeCount = 0; // ��Ғǉ�

        // ���̉e�����ʂ̏d�˂�������
        protected int currentPoisonValue = 0; // ��Ғǉ�
        protected int currentConcussiveHitValue = 0; // ��Ғǉ�
        protected int currentOnslaughtHitValue = 0; // ��Ғǉ�
        protected int currentImpulseHitValue = 0; // ��Ғǉ�

        // ���̉e�����ʂ̏d�˂�������
        protected int currentSkyShieldValue = 0; // ��Ғǉ�
        protected int currentStaticBarrierValue = 0; // ��Ғǉ�
        protected int currentStanceOfMysticValue = 0; // ��Ғǉ�

        // ������L�̏d�˂�������
        protected int currentFeltusValue = 0; // ��Ғǉ�
        protected int currentJuzaPhantasmalValue = 0; // ��Ғǉ�
        protected int currentEternalFateRingValue = 0; // ��Ғǉ�
        protected int currentLightServantValue = 0; // ��Ғǉ�
        protected int currentShadowServantValue = 0; // ��Ғǉ�
        protected int currentAdilBlueBurnValue = 0; // ��Ғǉ�
        protected int currentMazeCubeValue = 0; // ��Ғǉ�

        // ���Օi���L�̏d�˂�������
        protected int currentBlackElixirValue = 0; // ��Ғǉ�

        // �ŏI�퓬�̃��C�t�J�E���g
        protected int currentLifeCountValue = 0; // ��Ғǉ�

        // s ��Ғǉ�
        // �ȉ��A�{�X��ȂǓG���ėp�I�Ƀp���[�A�b�v���悤�Ƃ��č쐬��������
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

        protected int currentAfterReviveHalf = 0; // �G�O�[�L���[�V���i�[�̎��S���㔼�����C�t�񕜂őh��
        protected int currentFireDamage2 = 0; // �ƁE�t���C���X���b�V���[�̉Βǉ����ʃ_���[�W
        protected int currentBlackMagic = 0; // �f�r���E�`���h������2�񖂖@BUFF
        protected int currentChaosDesperate = 0; // �J�I�X�E���[�f���̎��S�\��
        protected int currentChaosDesperateValue = 0; // �J�I�X�E���[�f���̎��S�\���J�E���g�l
        protected int currentIchinaruHomura = 0; // ���M�B���A�[�[�̈�Ȃ鉋
        protected int currentAbyssFire = 0; // ���M�B���A�[�[�̃A�r�X�E�t�@�C�A
        protected int currentLightAndShadow = 0; // ���M�B���A�[�[�̃��C�g�E�A���h�E�V���h�E
        protected int currentEternalDroplet = 0; // ���M�B���A�[�[�̃G�^�[�i���h���b�v���b�g
        protected int currentAusterityMatrixOmega = 0; // ���M�B���A�[�[�̃A�E�X�e���e�B�E�}�g���N�X�E��
        protected int currentVoiceOfAbyss = 0; // ���M�B���A�[�[�̃��H�C�X�E�I�u�E�A�r�X
        protected int currentAbyssWill = 0; // ���M�B���A�[�[�̃A�r�X�̈ӎu
        protected int currentAbyssWillValue = 0; // ���M�B���A�[�[�̃A�r�X�̈ӎu�̗ݐϒl
        protected int currentTheAbyssWall = 0; // ���M�B���A�[�[�̃A�r�X�h��

        protected int poolLifeConsumption = 0; // ���`�̔t�ŁA1�^�[���̃_���[�W/����ʂ��K�v�ɂȂ���
        protected int poolManaConsumption = 0; // ���`�̔t�ŁA1�^�[���̃_���[�W/����ʂ��K�v�ɂȂ���
        protected int poolSkillConsumption = 0; // ���`�̔t�ŁA1�^�[���̃_���[�W/����ʂ��K�v�ɂȂ���

        // �ȉ��A�������@FlashBlaze����ǉ��_���[�W�𓖂Ă悤�Ƃ��čl��������
        protected int currentFlashBlazeCount = 0; // ��Ғǉ�
        protected int currentFlashBlazeFactor = 0; // ��Ғǉ�

        protected bool dead = false;
        protected bool deadSignForTranscendentWish = false; // ��Ғǉ�

        // s ��Ғǉ�
        protected bool actionDecision = false; // �G���A�N�V���������肵�����ǂ����������t���O
        protected int decisionTiming = 0; // �G���A�N�V���������肷��^�C�~���O
        // e ��Ғǉ�

        // s ��Ғǉ�
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
        // e ��Ғǉ�

        protected string reserveBattleCommand = String.Empty; // ��Ғǉ� �^�[�Q�b�g���w�肷��ꍇ�A�ꎞ�I�Ƀo�g���R�}���h���L������
        protected bool nowExecActionFlag = false; // ��Ғǉ��@���ݎ������s�����s���ł��邱�Ƃ������t���O

        protected bool realTimeBattle = false; // ��Ғǉ�

        protected bool stackActivation = false; // ��Ғǉ�
        protected MainCharacter stackActivePlayer = null; // �X�^�b�N�C���U�R�}���h�p�̃A�N�e�B�u�v���C���[�i�[��
        protected MainCharacter stackTarget = null; // �X�^�b�N�C���U�R�}���h�p�̑Ώۃv���C���[�i�[��
        protected PlayerAction stackPlayerAction = PlayerAction.None; // �X�^�b�N�C���U�R�}���h�p�̃v���C���[�A�N�V�����i�[��
        protected string stackCommandString = String.Empty; // �X�^�b�N�C���U�R�}���h�p�̃R�}���h������i�[��

        //protected MainCharacter shadowStackActivePlayer = null;
        //protected MainCharacter shadowStackTarget = null;
        //protected PlayerAction shadowStackPlayerAction = PlayerAction.None;
        //protected string shadowStackCommandString = String.Empty;

        protected List<string> actionCommandStackList = new List<string>(); // Bystander���^�C���X�g�b�v���A�r����ݐς����邽�߂ɗp����A�N�V�����R�}���h���X�g
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

        #region "���@�X�y��"
        // �� Light
        protected bool freshHeal = false; // ���C�t��
        public bool FreshHeal
        {
            get { return freshHeal; }
            set { freshHeal = value; }
        }
        protected bool protection = false; // �����h��UP
        public bool Protection
        {
            get { return protection; }
            set { protection = value; }
        }
        protected bool holyshock = false; // �_���[�W
        public bool HolyShock
        {
            get { return holyshock; }
            set { holyshock = value; }
        }
        protected bool saintpower = false; // �����U��UP
        public bool SaintPower
        {
            get { return saintpower; }
            set { saintpower = value; }
        }
        protected bool glory = false; // ���̂R�^�[���A���C�t�񕜂ƍU�����s��
        public bool Glory
        {
            get { return glory; }
            set { glory = value; }
        }
        protected bool resurrection = false; // ���񂾒��Ԃ𕜊�
        public bool Resurrection
        {
            get { return resurrection; }
            set { resurrection = value; }
        }
        protected bool celestialnova = false; // �G�S�̃_���[�W�A�܂��́A�����S�̃��C�t��
        public bool CelestialNova
        {
            get { return celestialnova; }
            set { celestialnova = value; }
        }

        // �� Shadow
        protected bool darkblast = false; // ���@�U��
        public bool DarkBlast
        {
            get { return darkblast; }
            set { darkblast = value; }
        }
        protected bool shadowpact = false; // ���@�U��UP
        public bool ShadowPact
        {
            get { return shadowpact; }
            set { shadowpact = value; }
        }
        protected bool lifeTap = false; // �}�i�l�A�X�L���l������ă��C�t��
        public bool LifeTap
        {
            get { return lifeTap; }
            set { lifeTap = value; }
        }
        protected bool blackContract = false; // ����ő�_���[�WUP
        public bool BlackContract
        {
            get { return blackContract; }
            set { blackContract = value; }
        }
        protected bool devouringPlague = false; // �G�Ƀ_���[�W�{�������C�t��
        public bool DevouringPlague
        {
            get { return devouringPlague; }
            set { devouringPlague = value; }
        }
        protected bool bloodyvengeance = false; // �͂Ƒ�UP
        public bool BloodyVengeance
        {
            get { return bloodyvengeance; }
            set { bloodyvengeance = value; }
        }
        protected bool damnation = false; // ���^�[���A�_���[�W
        public bool Damnation
        {
            get { return damnation; }
            set { damnation = value; }
        }

        // �� Fire
        protected bool fireball = false; // ���@�U��
        public bool FireBall
        {
            get { return fireball; }
            set { fireball = value; }
        }
        protected bool flameaura = false; // �ʏ�U���t���A�ǉ��_���[�W
        public bool FlameAura
        {
            get { return flameaura; }
            set { flameaura = value; }
        }
        protected bool heatboost = false; // �Z��UP
        public bool HeatBoost
        {
            get { return heatboost; }
            set { heatboost = value; }
        }
        protected bool flamestrike = false; // ���_���[�W
        public bool FlameStrike
        {
            get { return flamestrike; }
            set { flamestrike = value; }
        }
        protected bool volcanicwave = false; // ��_���[�W
        public bool VolcanicWave
        {
            get { return volcanicwave; }
            set { volcanicwave = value; }
        }
        protected bool immortalrave = false; // ���̂R�^�[���A�΍U���X�y���i�R�X�g�O�j�{���ڍU�����s��
        public bool ImmortalRave
        {
            get { return immortalrave; }
            set { immortalrave = value; }
        }
        protected bool lavaannihilation = false; // �S�̃_���[�W
        public bool LavaAnnihilation
        {
            get { return lavaannihilation; }
            set { lavaannihilation = value; }
        }

        // �� Ice
        protected bool iceneedle = false; // �_���[�W
        public bool IceNeedle
        {
            get { return iceneedle; }
            set { iceneedle = value; }
        }
        protected bool absorbwater = false; // ���@�h��UP
        public bool AbsorbWater
        {
            get { return absorbwater; }
            set { absorbwater = value; }
        }
        protected bool cleansing = false; // ������l�ɑ΂��āA���̉e������������
        public bool Cleansing
        {
            get { return cleansing; }
            set { cleansing = value; }
        }
        protected bool frozenlance = false; // ���_���[�W
        public bool FrozenLance
        {
            get { return frozenlance; }
            set { frozenlance = value; }
        }
        protected bool mirrorimage = false; // ���@�U���𔽎ˁiAbsoluteZero��WordOfPower�͔��˂ł��Ȃ��j
        public bool MirrorImage
        {
            get { return mirrorimage; }
            set { mirrorimage = value; }
        }
        protected bool promisedknowledge = false; // �m��UP
        public bool PromisedKnowledge
        {
            get { return promisedknowledge; }
            set { promisedknowledge = value; }
        }
        protected bool absolutezero = false; // ���̂R�^�[���A�G�̓��C�t�񕜕s�A���˕t�^�𖳌����A�X�y���r���s�iTranquility�͑ΏۊO�j�A�h��s��
        public bool AbsoluteZero
        {
            get { return absolutezero; }
            set { absolutezero = value; }
        }

        // �� Force
        protected bool wordofpower = false; // �����_���[�W�i���@�_���[�W�ł͂Ȃ��j
        public bool WordOfPower
        {
            get { return wordofpower; }
            set { wordofpower = value; }
        }
        protected bool galewind = false; // ���̃^�[���A�U���܂��̓X�y���r�����A���Q��ɂȂ�
        public bool GaleWind
        {
            get { return galewind; }
            set { galewind = value; }
        }
        protected bool wordoflife = false; // ���^�[�����C�t��
        public bool WordOfLife
        {
            get { return wordoflife; }
            set { wordoflife = value; }
        }
        protected bool wordoffortune = false; // ���^�[���A100%�N���e�B�J��
        public bool WordOfFortune
        {
            get { return wordoffortune; }
            set { wordoffortune = value; }
        }
        protected bool aetherdrive = false; // ���̂R�^�[���A�G����̕����_���[�W�����A�����̕����_���[�W�Q�{
        public bool AetherDrive
        {
            get { return aetherdrive; }
            set { aetherdrive = value; }
        }
        protected bool genesis = false; // �O��Ƃ����s���Ɠ����s�����s���B�X�y���A�X�L���R�X�g�͂O�B�������L�����Z���ΏۂɂȂ�����͌��ʂ������Ȃ��B
        public bool Genesis
        {
            get { return genesis; }
            set { genesis = value; }
        }
        protected bool eternalpresence = false; // �����U���A�����h��A���@�U���A���@�h��UP
        public bool EternalPresence
        {
            get { return eternalpresence; }
            set { eternalpresence = value; }
        }

        // �� Will
        protected bool dispelmagic = false; // �t�^���ꂽ���@������
        public bool DispelMagic
        {
            get { return dispelmagic; }
            set { dispelmagic = value; }
        }
        protected bool riseofimage = false; // �SUP
        public bool RiseOfImage
        {
            get { return riseofimage; }
            set { riseofimage = value; }
        }
        protected bool deflection = false; // �����U���𔽎�
        public bool Deflection
        {
            get { return deflection; }
            set { deflection = value; }
        }
        protected bool tranquility = false; // Glory�ABlackContract�AImmortalRave�AAbsoluteZero�AAetherDrive�ADeflection�AMirrorImage�𖳌���
        public bool Tranquility
        {
            get { return tranquility; }
            set { tranquility = value; }
        }
        protected bool oneimmunity = false; // ���̃^�[���A�h�䂵�Ă���Ԃ́A�S�_���[�W�������B
        public bool OneImmunity
        {
            get { return oneimmunity; }
            set { oneimmunity = value; }
        }
        protected bool whiteout = false; // ��_���[�W
        public bool WhiteOut
        {
            get { return whiteout; }
            set { whiteout = value; }
        }
        protected bool timestop = false; // ���̃^�[���A����̃^�[�����΂�
        public bool TimeStop
        {
            get { return timestop; }
            set { timestop = value; }
        }

        // s ��Ғǉ�
        // ���{�Łi���S�t�j
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

        // ���{��
        protected bool flashBlaze = false; // ���@�_���[�W�{�ǉ����@�_���[�W
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

        // ���{��
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

        // ���{��
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

        // ���{��
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

        // �Ł{��
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

        // �Ł{��
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

        // �Ł{��
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

        // �Ł{��        
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

        // �΁{���i���S�t�j
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

        // �΁{��
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

        // �΁{��
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

        // ���{��
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

        // ���{��
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

        // ��{��(���S�t)
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
        // e ��Ғǉ�
        #endregion

        #region "�X�L��"
        // ��
        protected bool straightSmash = false; // �́{�Z�ɂ�钼�ڍU��
        public bool StraightSmash
        {
            get { return straightSmash; }
            set { straightSmash = value; }
        }
        protected bool doubleSlash = false; // 2��U��
        public bool DoubleSlash
        {
            get { return doubleSlash; }
            set { doubleSlash = value; }
        }
        protected bool crushingBlow = false; // ���ڍU���{�P�^�[���̊ԃX�^������
        public bool CrushingBlow
        {
            get { return crushingBlow; }
            set { crushingBlow = value; }
        }
        protected bool soulInfinity = false; // �́{�Z�{�m�ɂ�钼�ڍU��
        public bool SoulInfinity
        {
            get { return soulInfinity; }
            set { soulInfinity = value; }
        }

        // ��
        protected bool counterAttack = false; // ���ڍU���������ꍇ�A����𖳌������āA���ڍU����Ԃ�
        public bool CounterAttack
        {
            get { return counterAttack; }
            set { counterAttack = value; }
        }
        protected bool purePurification = false; // ������l�ɑ΂��āA���̉e������������
        public bool PurePurification
        {
            get { return purePurification; }
            set { purePurification = value; }
        }
        protected bool antiStun = false; // �X�^������U����H������ꍇ�A�X�^�����Ȃ�
        public bool AntiStun
        {
            get { return antiStun; }
            set { antiStun = value; }
        }
        protected bool stanceOfDeath = false; // �ꌂ����H�炤���ʂ����������ꍇ�A������������
        public bool StanceOfDeath
        {
            get { return stanceOfDeath; }
            set { stanceOfDeath = value; }
        }

        // �_
        protected bool stanceOfFlow = false; // ���̂R�^�[���A�K����U�����B
        public bool StanceOfFlow
        {
            get { return stanceOfFlow; }
            set { stanceOfFlow = value; }
        }
        protected bool enigmaSense = false; // �́E�Z�E�m������̒l�̒��ň�ԍ����l����Ƃ��Ē��ڍU��
        public bool EnigmaSence
        {
            get { return enigmaSense; }
            set { enigmaSense = value; }
        }
        protected bool silentRush = false; // �R��U��
        public bool SilentRush
        {
            get { return silentRush; }
            set { silentRush = value; }
        }
        protected bool oboroImpact = false; // �́~�S�{�Z�~�S�{�m�~�S�ɂ��_���[�W
        public bool OboroImpact
        {
            get { return oboroImpact; }
            set { oboroImpact = value; }
        }

        // ��
        protected bool stanceOfStanding = false; // �h��̍\���������܂ܒʏ�U��
        public bool StanceOfStanding
        {
            get { return stanceOfStanding; }
            set { stanceOfStanding = value; }
        }
        protected bool innerInspiration = false; // �X�L���|�C���g����
        public bool InnerInspiration
        {
            get { return innerInspiration; }
            set { innerInspiration = value; }
        }
        protected bool kineticSmash = false; // �́{�m�ɂ�钼�ڍU��
        public bool KineticSmash
        {
            get { return kineticSmash; }
            set { kineticSmash = value; }
        }
        protected bool catastrophe = false; // �S�X�L���|�C���g���g�p���đ�_���[�W
        public bool Catastrophe
        {
            get { return catastrophe; }
            set { catastrophe = value; }
        }

        // �S��
        protected bool truthVision = false; // �G�̃p�����^UP��Ԃ𖳎������X�e�[�^�X��ΏۂƂ���
        public bool TruthVision
        {
            get { return truthVision; }
            set { truthVision = value; }
        }
        protected bool highEmotionality = false; // ���̂R�^�[���A�S�p�����^UP
        public bool HighEmotionality
        {
            get { return highEmotionality; }
            set { highEmotionality = value; }
        }
        protected bool stanceOfEyes = false; // ����̍s�����L�����Z������ōU������
        public bool StanceOfEyes
        {
            get { return stanceOfEyes; }
            set { stanceOfEyes = value; }
        }
        protected bool painfulInsanity = false; // ���^�[���A�S�l�ɂ��i���_���[�W
        public bool PainfulInsanity
        {
            get { return painfulInsanity; }
            set { painfulInsanity = value; }
        }

        // ���S
        protected bool negate = false; // ����̃X�y���r�����L�����Z������
        public bool Negate
        {
            get { return negate; }
            set { negate = value; }
        }
        protected bool voidExtraction = false; // �ł������p�����^���Q�{�Ɉ����グ��
        public bool VoidExtraction
        {
            get { return voidExtraction; }
            set { voidExtraction = value; }
        }
        protected bool carnageRush = false; // �T�A���̖������U���i�������Ȃ��j
        public bool CarnageRush
        {
            get { return carnageRush; }
            set { carnageRush = value; }
        }
        protected bool nothingOfNothingness = false; // DispelMagic��Tranquility�𖳌�������BNegate�AStanceOfEyes�ACounterAttack�𖳌�������B
        public bool NothingOfNothingness
        {
            get { return nothingOfNothingness; }
            set { nothingOfNothingness = value; }
        }

        // s ��Ғǉ�
        // ���{�� �i���S�t�j
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

        // ���{�_
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

        // ���{��
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

        // ���{�S��
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

        // ���{���S
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

        // �Á{�_
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

        // �Á{��
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

        // �Á{�S��
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

        // �Á{���S
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

        // �_�{�� �i���S�t�j
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

        // �_�{�S��
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

        // �_�{���S
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

        // ���{�S��
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

        // ���{���S
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

        // �S��{���S �i���S�t�j
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
        // e ��Ғǉ�
        #endregion

        #region "���j"
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

        #region "�X�e�[�^�X"
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
            // s ��ҕҏW
            get 
            {
                int result = baseLife;
                result += TotalStamina * 10;
                result += CurrentBlackElixirValue;
                return result; 
            }
            // e ��ҕҏW
        }
        public int BaseMana
        {
            get { return baseMana; }
            set { baseMana = value; }
        }
        public int MaxMana
        {
            get { return baseMana + TotalIntelligence * 10; } // ��ҕҏW
        }
        public int BaseSkillPoint
        {
            get { return baseSkillPoint; }
            set { baseSkillPoint = value; }
        }
        public int MaxSkillPoint
        {
            // s ��ҕҏW
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
            // e ��ҕҏW
        }
        // s ��Ғǉ�
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
        // e ��Ғǉ�
        // s ��Ғǉ�
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
        // e ��Ғǉ�
        // s ��Ғǉ�
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
        // e ��Ғǉ�
        // s ��Ғǉ�
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
        // e ��Ғǉ�
        // s ��Ғǉ�
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
        // e ��Ғǉ�

        // s ��Ғǉ�
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
        // e ��Ғǉ�

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
        // s ��Ғǉ�
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
        // e ��Ғǉ�
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
        // s ��Ғǉ�
        public bool AvailableArchitect
        {
            get { return availableArchitect; }
            set { availableArchitect = value; }
        }
        // e ��Ғǉ�
        public int Gold
        {
            get { return gold; }
            set { gold = value; }
        }
        // s ��Ғǉ�
        public PlayerStance Stance
        {
            get { return stance; }
            set { stance = value; }
        }
        // e ��Ғǉ�
        // s ��Ғǉ�
        public AdditionalSpellType AdditionSpellType
        {
            get { return additionSpellType; }
            set { additionSpellType = value; }
        }
        public AdditionalSkillType AdditionSkillType
        {
            get { return additionSkillType; }
            set { additionSkillType = value; }
        }        // e ��Ғǉ�
        #endregion


        public ItemBackPack MainWeapon
        {
            get { return iw; }
            set
            { 
                iw = value;
                // s ��ҕҏW
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
                // e ��ҕҏW
            }
        }
        // s ��Ғǉ�
        public ItemBackPack SubWeapon
        {
            get { return sw; }
            set
            {
                sw = value;
                // s ��ҕҏW
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
                // e ��ҕҏW
            }
        }
        // e ��Ғǉ�
        public ItemBackPack MainArmor
        {
            get { return ia; }
            set
            {
                ia = value; 
                // s ��ҕҏW
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
                // e ��ҕҏW
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
                    // s ��ҕҏW
                    this.buffStrength_Accessory = accessory.BuffUpStrength;
                    this.buffAgility_Accessory = accessory.BuffUpAgility;
                    this.buffIntelligence_Accessory = accessory.BuffUpIntelligence;
                    this.buffStamina_Accessory = accessory.BuffUpStamina;
                    this.buffMind_Accessory = accessory.BuffUpMind;
                    // e ��ҕҏW
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
        // s ��Ғǉ�
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
        // e ��Ғǉ�

        #region "�퓬����p"
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
        // s ��Ғǉ�
        public string CurrentArchetypeName
        {
            get { return currentArchetypeName; }
            set { currentArchetypeName = value; }
        }
        // e ��Ғǉ�
        public MainCharacter Target
        {
            get { return target; }
            set { target = value; }
        }
        // s ��Ғǉ�
        public MainCharacter Target2
        {
            get { return target2; }
            set { target2 = value; }
        }
        // e ��Ғǉ�
        public PlayerAction BeforePA
        {
            get { return beforePA; }
            set { beforePA = value; } // ��Ғǉ�
        }
        public string BeforeUsingItem
        {
            get { return beforeUsingItem; }
            set { beforeUsingItem = value; } // ��Ғǉ�
        }
        public string BeforeSkillName
        {
            get { return beforeSkillName; }
            set { beforeSkillName = value; } // ��Ғǉ�
        }
        public string BeforeSpellName
        {
            get { return beforeSpellName; }
            set { beforeSpellName = value; } // ��Ғǉ�
        }
        // s ��Ғǉ�
        public string BeforeArchetypeName
        {
            get { return beforeArchetypeName; }
            set { beforeArchetypeName = value; }
        }
        // e ��Ғǉ�
        public MainCharacter BeforeTarget
        {
            get { return beforeTarget; }
            set { beforeTarget = value; }
        }
        // s ��Ғǉ�
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
        // e ��Ғǉ�

        // s ��Ғǉ�
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
        // e ��Ғǉ�


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
        // s ��Ғǉ�
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
        // e ��Ғǉ�
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
        // s ��Ғǉ�
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
        // e ��Ғǉ�

        // s ��Ғǉ�
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
        // e ��Ғǉ�

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

        // s ��Ғǉ�
        public int CurrentStanceOfEyes
        {
            get { return currentStanceOfEyes; }
            set { currentStanceOfEyes = value; }
        }
        public TruthImage pbStanceOfEyes = null;
        // e ��Ғǉ�

        public int CurrentVoidExtraction
        {
            get { return currentVoidExtraction; }
            set { currentVoidExtraction = value; }
        }
        public TruthImage pbVoidExtraction = null;

        // s ��Ғǉ�
        public int CurrentNegate
        {
            get { return currentNegate; }
            set { currentNegate = value; }
        }
        public TruthImage pbNegate = null;
        // e ��Ғǉ�

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

        // ���{��
        public int CurrentStanceOfDouble
        {
            get { return currentStanceOfDouble; }
            set { currentStanceOfDouble = value; }
        }
        // ���{�_
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
        // ���{��
        // ���{�S��
        public int CurrentOnslaughtHit
        {
            get { return currentOnslaughtHit; }
            set { currentOnslaughtHit = value; }
        }
        // ���{���S
        public int CurrentSmoothingMove
        {
            get { return currentSmoothingMove; }
            set { currentSmoothingMove = value; }
        }
        // �Á{�_
        public int CurrentFutureVision
        {
            get { return currentFutureVision; }
            set { currentFutureVision = value; }
        }
        // �Á{��
        public int CurrentReflexSpirit
        {
            get { return currentReflexSpirit; }
            set { currentReflexSpirit = value; }
        }
        // �Á{�S��
        public int CurrentConcussiveHit
        {
            get { return currentConcussiveHit; }
            set { currentConcussiveHit = value; }
        }
        // �Á{���S
        public int CurrentTrustSilence
        {
            get { return currentTrustSilence; }
            set { currentTrustSilence = value; }
        }
        // �_�{��
        // �_�{�S��
        // �_�{���S
        public int CurrentImpulseHit
        {
            get { return currentImpulseHit; }
            set { currentImpulseHit = value; }
        }
        // ���{�S��
        // ���{���S
        // �S��{���S

        // ������L��BUFF
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
        // s ��Ғǉ�
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
        // e ��Ғǉ�
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
        public TruthImage pbSlow = null; // ��Ғǉ�
        public TruthImage pbBlind = null; // ��Ғǉ�
        public TruthImage pbSlip = null; // ��Ғǉ�
        public TruthImage pbNoGainLife = null; // ��Ғǉ�
        public TruthImage pbBlinded = null; // ��Ғǉ�

        // s ��Ғǉ�
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
        // e ��Ғǉ�

        // s ��Ғǉ�
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

        // e ��Ғǉ�
        // s ��Ғǉ�
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
        // e ��Ғǉ�
        // s ��Ғǉ�
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

        // ���{��
        public TruthImage pbStanceOfDouble = null;
        // ���{�_
        public TruthImage pbSwiftStep = null;
        public TruthImage pbVigorSense = null;
        // ���{��
        // ���{�S��
        public TruthImage pbOnslaughtHit = null;
        // ���{���S
        public TruthImage pbSmoothingMove = null;
        // �Á{�_
        public TruthImage pbFutureVision = null;
        // �Á{��
        public TruthImage pbReflexSpirit = null;
        // �Á{�S��
        public TruthImage pbConcussiveHit = null;
        // �Á{���S
        public TruthImage pbTrustSilence = null;
        // �_�{��
        public TruthImage pbStanceOfMystic = null;
        // �_�{�S��
        public TruthImage pbNourishSense = null;
        // �_�{���S
        public TruthImage pbImpulseHit = null;
        // ���{�S��
        // ���{���S
        // �S��{���S

        // ������LBUFF
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
        // ���Օi���LBUFF
        public TruthImage pbSagePotionMini = null;
        public TruthImage pbGenseiTaima = null;
        public TruthImage pbShiningAether = null;
        public TruthImage pbBlackElixir = null;
        public TruthImage pbElementalSeal = null;
        public TruthImage pbColoressAntidote = null;

        // �ŏI�탉�C�t�J�E���g
        public TruthImage pbLifeCount = null;

        // ���F���[�ŏI��J�I�e�B�b�N�X�L�[�}
        public TruthImage pbChaoticSchema = null;

        // �W���ƒf��
        public TruthImage pbSyutyuDanzetsu = null;
        // �z�Ɛ���
        public TruthImage pbJunkanSeiyaku = null;
        // e ��Ғǉ�

        // s ��Ғǉ� (����͏�������ϐ��ł���A���g�p�j
        public TruthImage pbBuff1 = null;
        public TruthImage pbBuff2 = null;
        public TruthImage pbBuff3 = null;
        // e ��Ғǉ�

        public System.Windows.Forms.Label labelName = null;
        public System.Windows.Forms.Label labelLife = null;
        public System.Windows.Forms.Label labelSkill = null;
        public System.Windows.Forms.Label labelMana = null;

        // s ��Ғǉ�
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

        public System.Drawing.Bitmap MainFaceArrow = null; // ��Ғǉ�
        public System.Drawing.Bitmap ShadowFaceArrow2 = null; // ��Ғǉ�
        public System.Drawing.Bitmap ShadowFaceArrow3 = null; // ��Ғǉ�
        public double BattleBarPos = 0; // ��Ғǉ�
        public double BattleBarPos2 = 0; // ��Ғǉ�
        public double BattleBarPos3 = 0; // ��Ғǉ�

        public System.Windows.Forms.Label DamageLabel = null;
        public System.Windows.Forms.Label CriticalLabel = null;

        public System.Windows.Forms.Panel BuffPanel = null;
        public int BuffNumber = 0;
        public TruthImage[] BuffElement = null; // �u�x���v�F��҂ł͂����BUFF���т𐮗񂷂�B�ŏI�I�ɂ͌�BUFF��TruthImage�͑S�ĕs�v�ɂȂ�B
        public System.Windows.Forms.TextBox TextBattleMessage = null;
        // e ��Ғǉ�

        // s ��Ғǉ�
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
        // e ��Ғǉ�

        // s ��Ғǉ�
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
        // e ��Ғǉ�
        #endregion

        public bool Dead
        {
            get { return dead; }
            set { dead = value; }
        }
        // s ��Ғǉ�
        public bool DeadSignForTranscendentWish
        {
            get { return deadSignForTranscendentWish; }
            set { deadSignForTranscendentWish = value; }
        }
        // e ��Ғǉ�

        // s ��Ғǉ�
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
        // e ��Ғǉ�

        // s ��Ғǉ�
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
        // e ��Ғǉ�

        // s ��Ғǉ�
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
        // e ��Ғǉ�

        protected bool emotionAngry = false; // [�x��]�F�{���Ă��邱�Ƃ������l�AEnum�ł��낢������������������m��Ȃ����A�����܂ŊJ���ł���Ƃ͎v���Ȃ��̂ŕۗ��B
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

        // s ��Ғǉ�
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
        // e ��Ғǉ�

        public int BuffStrength_Accessory
        {
            get { return buffStrength_MainWeapon + buffStrength_SubWeapon + buffStrength_Armor + buffStrength_Accessory + buffStrength_Accessory2; } // c ��Ғǉ�
        }
        public int BuffAgility_Accessory
        {
            get { return buffAgility_MainWeapon + buffAgility_SubWeapon + buffAgility_Armor + buffAgility_Accessory + buffAgility_Accessory2; } // c ��Ғǉ�
        }
        public int BuffIntelligence_Accessory
        {
            get { return buffIntelligence_MainWeapon + buffIntelligence_SubWeapon + buffIntelligence_Armor + buffIntelligence_Accessory + buffIntelligence_Accessory2; } // c ��Ғǉ�
        }
        public int BuffStamina_Accessory
        {
            get { return buffStamina_MainWeapon + buffStamina_SubWeapon + buffStamina_Armor + buffStamina_Accessory + buffStamina_Accessory2; } // c ��Ғǉ�
        }
        public int BuffMind_Accessory
        {
            get { return buffMind_MainWeapon + buffMind_SubWeapon + buffMind_Armor + buffMind_Accessory + buffMind_Accessory2; } // c ��Ғǉ�
        }

        // Standard�̓x�[�X�l�{��݌^BUFFUP�{�H�����BUFFUP�̍��v�l
        public int StandardStrength
        {
            get { return this.baseStrength + this.buffStrength_MainWeapon + this.buffStrength_Accessory + this.buffStrength_Accessory2 + this.buffStrength_Food; } // c ��Ғǉ�
        }
        public int StandardAgility
        {
            get { return this.baseAgility + this.buffAgility_MainWeapon + this.buffAgility_Accessory + this.buffAgility_Accessory2 + this.buffAgility_Food; } // c ��Ғǉ�
        }
        public int StandardIntelligence
        {
            get { return this.baseIntelligence + this.buffIntelligence_MainWeapon + this.buffIntelligence_Accessory + this.buffIntelligence_Accessory2 + this.buffIntelligence_Food; } // c ��Ғǉ�
        }
        public int StandardStamina
        {
            get { return this.baseStamina + this.buffStamina_MainWeapon + this.buffStamina_Accessory + this.buffStamina_Accessory2 + this.buffStamina_Food; } // c ��Ғǉ�
        }
        public int StandardMind
        {
            get { return this.baseMind + this.buffMind_MainWeapon + this.buffMind_Accessory + this.buffMind_Accessory2 + this.buffMind_Food; } // c ��Ғǉ�
        }

        // Total�̓x�[�X�l�{�퓬��UP�{��݌^BUFFUP�̍��v�l
        public int TotalStrength
        {
            get { return this.baseStrength +
                this.buffStrength_BloodyVengeance +
                this.buffStrength_HighEmotionality +
                this.buffStrength_VoidExtraction + 
                this.buffStrength_TranscendentWish + // ��Ғǉ�
                this.buffStrength_Hiyaku_Kassei + // c ��Ғǉ�
                this.buffStrength_MainWeapon + // ��Ғǉ�
                this.buffStrength_SubWeapon + // ��Ғǉ�
                this.buffStrength_Armor +  // ��Ғǉ�
                this.buffStrength_Accessory +
                this.buffStrength_Accessory2 + // ��Ғǉ�
                this.buffStrength_Food + // ��Ғǉ�
                this.currentStrengthUpValue; // ��Ғǉ�    
            } // c ��Ғǉ�
        }
        public int TotalAgility
        {
            get { return this.baseAgility +
                this.buffAgility_HeatBoost +
                this.buffAgility_HighEmotionality +
                this.buffAgility_VoidExtraction +
                this.buffAgility_TranscendentWish + // ��Ғǉ�
                this.buffAgility_Hiyaku_Kassei + // c ��Ғǉ�
                this.buffAgility_MainWeapon + // ��Ғǉ�
                this.buffAgility_SubWeapon + // ��Ғǉ�
                this.buffAgility_Armor + // ��Ғǉ�
                this.buffAgility_Accessory +
                this.buffAgility_Accessory2 + // ��Ғǉ�
                this.buffAgility_Food + // ��Ғǉ�
                this.currentAgilityUpValue; } // c ��Ғǉ�
        }
        public int TotalIntelligence
        {
            get { return this.baseIntelligence +
                this.buffIntelligence_PromisedKnowledge +
                this.buffIntelligence_HighEmotionality +
                this.buffIntelligence_VoidExtraction +
                this.buffIntelligence_TranscendentWish + // ��Ғǉ�
                this.buffIntelligence_Hiyaku_Kassei + // c ��Ғǉ�
                this.buffIntelligence_MainWeapon + // ��Ғǉ�
                this.buffIntelligence_SubWeapon + // ��Ғǉ�
                this.buffIntelligence_Armor + // ��Ғǉ�
                this.buffIntelligence_Accessory +
                this.buffIntelligence_Accessory2 + // ��Ғǉ�
                this.buffIntelligence_Food + // ��Ғǉ�
                this.currentIntelligenceUpValue; } // c ��Ғǉ�
        }
        public int TotalStamina
        {
            get { return this.baseStamina + 
                this.buffStamina_Unknown +
                this.buffStamina_HighEmotionality +
                this.buffStamina_VoidExtraction +
                this.buffStamina_TranscendentWish + // ��Ғǉ�
                this.buffStamina_Hiyaku_Kassei + // c ��Ғǉ�
                this.buffStamina_MainWeapon + // ��Ғǉ�
                this.buffStamina_SubWeapon + // ��Ғǉ�
                this.buffStamina_Armor + // ��Ғǉ�
                this.buffStamina_Accessory +
                this.buffStamina_Accessory2 + // ��Ғǉ�
                this.buffStamina_Food + // ��Ғǉ�
                this.currentStaminaUpValue; } // c ��Ғǉ�
        }
        public int TotalMind
        {
            get
            {
                return this.baseMind +
                    this.buffMind_RiseOfImage +
                    this.buffMind_HighEmotionality +
                    this.buffMind_VoidExtraction +
                    this.buffMind_TranscendentWish + // ��Ғǉ�
                    this.buffMind_Hiyaku_Kassei + // c ��Ғǉ�
                    this.buffMind_MainWeapon + // ��Ғǉ�
                    this.buffMind_SubWeapon + // ��Ғǉ�
                    this.buffMind_Armor + // ��Ғǉ�
                    this.buffMind_Accessory +
                    this.buffMind_Accessory2 + // ��Ғǉ�
                    this.buffMind_Food + // ��Ғǉ�
                    this.currentMindUpValue + // c ��Ғǉ�
                    this.currentFeltusValue * (int)(PrimaryLogic.FeltusValue(this)); // ��Ғǉ�
            }
        }

        #region "�o�b�N�p�b�N����֘A"
        public MainCharacter()
        {
            backpack = new ItemBackPack[Database.MAX_BACKPACK_SIZE]; // ��ҕҏW
        }

        // s ��ҕҏW
        /// <summary>
        /// �o�b�N�p�b�N�ɃA�C�e����ǉ����܂��B
        /// </summary>
        /// <param name="item"></param>
        /// <returns>TRUE:�ǉ������AFALSE:���t�̂��ߒǉ��ł��Ȃ�</returns>
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
            for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++) // ��ҕҏW
            {
                // �܂������Ă��Ȃ��ꍇ�A�P�ڂƂ��Đ�������B
                if (this.backpack[ii] == null)
                {
                    // ����A����T������Ɠ����A�C�e���������Ă��邩������Ȃ��̂ŁA�܂���������B
                    for (int jj = ii + 1; jj < Database.MAX_BACKPACK_SIZE; jj++) // ��ҕҏW
                    {
                        if (CheckBackPackExist(item, jj) > 0)
                        {
                            // �X�^�b�N����ȏ�̏ꍇ�A�ʂ̃A�C�e���Ƃ��Ēǉ�����B
                            if (this.backpack[jj].StackValue >= item.LimitValue)
                            {
                                // ���̃A�C�e�����X�g�փX���[
                                break;
                            }
                            else
                            {
                                // �X�^�b�N����𒴂��Ă��Ȃ��Ă��A�����ǉ��ŏ���𒴂��Ă��܂��ꍇ
                                if (this.backpack[jj].StackValue + addValue > item.LimitValue)
                                {
                                    // ���̃A�C�e�����X�g�փX���[
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

                    // ��͂�T�����Ă����������̂ŁA���̂܂ܒǉ�����B
                    this.backpack[ii] = item;
                    this.backpack[ii].StackValue = addValue;
                    addedNumber = ii;
                    return true;
                }
                else
                {
                    // ���Ɏ����Ă���ꍇ�A�X�^�b�N�ʂ𑝂₷�B
                    if (this.backpack[ii].Name == item.Name)
                    {
                        // �X�^�b�N����ȏ�̏ꍇ�A�ʂ̃A�C�e���Ƃ��Ēǉ�����B
                        if (this.backpack[ii].StackValue >= item.LimitValue)
                        {
                            // ���̃A�C�e�����X�g�փX���[
                        }
                        else
                        {
                            // �X�^�b�N����𒴂��Ă��Ȃ��Ă��A�����ǉ��ŏ���𒴂��Ă��܂��ꍇ
                            if (this.backpack[ii].StackValue + addValue > item.LimitValue)
                            {
                                // ���̃A�C�e�����X�g�փX���[
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
        /// �o�b�N�p�b�N�̃A�C�e�����폜���܂��B
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool DeleteBackPack(ItemBackPack item)
        {
            return DeleteBackPack(item, 0);
        }
        /// <summary>
        /// �o�b�N�p�b�N�̃A�C�e�����w�肵���������폜���܂��B
        /// </summary>
        /// <param name="item"></param>
        /// <param name="deleteValue">�폜���鐔 �O�F�S�č폜�A���l�F�w�萔�����폜</param>
        /// <returns></returns>
        public bool DeleteBackPack(ItemBackPack item, int deleteValue)
        {
            for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++) // ��ҕҏW
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
                            // �X�^�b�N�ʂ����l�̏ꍇ�A�w�肳�ꂽ�X�^�b�N�ʂ����炷�B
                            this.backpack[ii].StackValue -= deleteValue;
                            if (this.backpack[ii].StackValue <= 0) // ���ʓI�ɃX�^�b�N�ʂ��O�ɂȂ����ꍇ�̓I�u�W�F�N�g�������B
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
                    // �X�^�b�N�ʂ��P�ȉ��̏ꍇ�A��������Ă���I�u�W�F�N�g�������B
                    if (this.backpack[ii].StackValue <= 1)
                    {
                        this.backpack[ii] = null;
                    }
                    else
                    {
                        // �X�^�b�N�ʂ��P���傫���ꍇ�A�w�肳�ꂽ�X�^�b�N�ʂ����炷�B
                        this.backpack[ii].StackValue -= deleteValue;
                        if (this.backpack[ii].StackValue <= 0) // ���ʓI�ɃX�^�b�N�ʂ��O�ɂȂ����ꍇ�̓I�u�W�F�N�g�������B
                        {
                            this.backpack[ii] = null;
                        }
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// �o�b�N�p�b�N�ɑΏۂ̃A�C�e�����܂܂�Ă��鐔�������܂��B
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
        /// �A�C�e�����e��S�ʓI�ɓ���ւ��܂��B
        /// </summary>
        /// <param name="item"></param>
        public void ReplaceBackPack(ItemBackPack[] item)
        {
            this.backpack = null;
            this.backpack = item;
        }

        /// <summary>
        /// �o�b�N�p�b�N�̓��e���ꊇ�őS�Ď擾���܂��B
        /// </summary>
        /// <returns></returns>
        public ItemBackPack[] GetBackPackInfo()
        {
            return backpack;
        }
        // e ��ҕҏW

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
            ItemBackPack[] newBackPack = new ItemBackPack[Database.MAX_BACKPACK_SIZE]; // ��ҕҏW
            int jj = 0;

            for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++) // ��ҕҏW
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
                    // s ��Ғǉ�
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
                    // e ��Ғǉ�
                }

                if (this.level < 35)
                {
                    return nextValue / 4; // �n�[�h���[�h�̏ꍇ�A�Q
                }
                else
                {
                    return nextValue / 2; // �n�[�h���[�h�̏ꍇ�A�P
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

        // s ��Ғǉ�
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
        // e ��Ғǉ�

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
            // 0 ~ 999 �퓬��
            // 10000 ~ 99999 �퓬��(��Ғǉ�)
            // 1000 ~ 1999 �z�[���^�E����b
            // 2000 ~ 2999 �X�e�[�^�X�\����
            // 3000 ~ 3999 �K���c����X��b
            // 4000 ~ 4999 ��Ґ퓬�̐퓬�R�}���h�ݒ莞
            #region "�A�C��"
            if (this.name == "�A�C��")
            {
                switch (sentenceNumber)
                {
                    case 0: // �X�L���s��
                        return this.name + "�F���܂����I�X�L���|�C���g������˂��I\r\n";
                    case 1: // Straight Smash
                        return this.name + "�F�s�����I�I���@�I�I\r\n";
                    case 2: // Double Slash 1
                        return this.name + "�F�ق��I\r\n";
                    case 3: // Double Slash 2
                        return this.name + "�F������������I\r\n";
                    case 4: // Painful Insanity
                        return this.name + "�F�y�S�ቜ�`�z�����̒ɂ݂�H�炢�����ȁI\r\n";
                    case 5: // empty skill
                        return this.name + "�F���܂����B�X�L����I�����Ă˂��I�I\r\n";
                    case 6: // ���݂��t�����V�X�̕K�E��H�������
                        return this.name + "�F���Ă��ȁI�I�N�\�I\r\n";
                    case 7: // Lizenos�̕K�E��H�������
                        return this.name + ": �S�R�E�E�E�����������˂��E�E�E\r\n";
                    case 8: // Minflore�̕K�E��H�������
                        return this.name + "�F�b�O�A�K�n�A�@�@�I�I�I\r\n";
                    case 9: // Fresh Heal�ɂ�郉�C�t��
                        return this.name + "�F{0} �񕜂������B\r\n";
                    case 10: // Fire Ball
                        return this.name + "�F���̉������炦�IFireBall�I {0} �� {1} �̃_���[�W\r\n";
                    case 11: // Flame Strike
                        return this.name + "�F�Ă�������IFlameStrike�I {0} �� {1} �̃_���[�W\r\n";
                    case 12: // Volcanic Wave
                        return this.name + "�F�ܔM�̋Ɖ΁I���炦�IVolcanicWave�I {0} �� {1} �̃_���[�W\r\n";
                    case 13: // �ʏ�U���N���e�B�J���q�b�g
                        return this.name + "�F�I���@�A�N���e�B�J���q�b�g�I {0} �� {1}�̃_���[�W\r\n";
                    case 14: // FlameAura�ɂ��ǉ��U��
                        return this.name + "�F�R����I {0}�̒ǉ��_���[�W\r\n";
                    case 15: //�����̐�����퓬���Ɏg�����Ƃ�
                        return this.name + "�F�퓬���͎g���˂����R���B\r\n";
                    case 16: // ���ʂ𔭊����Ȃ��A�C�e����퓬���Ɏg�����Ƃ�
                        return this.name + "�F�E�E�E�H�H �悭������Ȃ��A�C�e�����B���������˂��I\r\n";
                    case 17: // ���@�Ń}�i�s��
                        return this.name + "�F�}�i������˂��I\r\n";
                    case 18: // Protection
                        return this.name + "�F���_�̉���AProtection�I�����h��́F�t�o\r\n";
                    case 19: // Absorb Water
                        return this.name + "�F�����_�̉���E�E�EAbsorbWater�I ���@�h��́F�t�o�B\r\n";
                    case 20: // Saint Power
                        return this.name + "�F�͂����S�āASaintPower�I �����U���́F�t�o\r\n";
                    case 21: // Shadow Pact
                        return this.name + "�F�łƂ̌_��E�E�EShadowPact�I ���@�U���́F�t�o\r\n";
                    case 22: // Bloody Vengeance
                        return this.name + "�F�ł̎g�҂�͂������E�E�EBloodyVengeance�I �̓p�����^�� {0} �t�o\r\n";
                    case 23: // Holy Shock
                        return this.name + "�F���Ȃ�S�ƁAHolyShock�I {0} �� {1} �̃_���[�W\r\n";
                    case 24: // Glory
                        return this.name + "�F��ɉh������BGlory�I ���ڍU���{FreshHeal�A�g�̃I�[��\r\n";
                    case 25: // CelestialNova 1
                        return this.name + "�F���Ȃ�g���ACelestialNova�I {0} �񕜂������B\r\n";
                    case 26: // CelestialNova 2
                        return this.name + "�F���Ȃ�ق���H�炦�ACelestialNova�I {0} �̃_���[�W\r\n";
                    case 27: // Dark Blast
                        return this.name + "�F���̔g����H�炦�ADarkBlast�I {0} �� {1} �̃_���[�W\r\n";
                    case 28: // Lava Annihilation
                        return this.name + "�F�����V�g�A���ҁI���炦�ILavaAnnihilation�I {0} �� {1} �̃_���[�W\r\n";
                    case 10028: // Lava Annihilation���
                        return this.name + "�F�����V�g�A���ҁI���炦�ILavaAnnihilation�I\r\n";
                    case 29: // Devouring Plague
                        return this.name + "�F�̗͂�H�炢�s�������BDevouringPlague�I {0} ���C�t���z�������\r\n";
                    case 30: // Ice Needle
                        return this.name + "�F�X�̐n��H�炦�AIceNeedle�I {0} �� {1} �̃_���[�W\r\n";
                    case 31: // Frozen Lance
                        return this.name + "�F����̑��łԂ��������AFrozenLance�I {0} �� {1} �̃_���[�W\r\n";
                    case 32: // Tidal Wave
                        return this.name + "�F��C����I���̗͂������t����ATidalWave�I {0} �̃_���[�W\r\n";
                    case 33: // Word of Power
                        return this.name + "�F���̏Ռ��g�ł��H�炦�AWordOfPower�I {0} �� {1} �̃_���[�W\r\n";
                    case 34: // White Out
                        return this.name + "�F�S�Ă̊��o���疕�E�����߂�AWhiteOut�I {0} �� {1} �̃_���[�W\r\n";
                    case 35: // Black Contract
                        return this.name + "�F����������ABlackContract�I " + this.name + "�̃X�L���A���@�R�X�g�͂O�ɂȂ�B\r\n";
                    case 36: // Flame Aura�r��
                        return this.name + "�F���̃V���{���旈����AFlameAura�I ���ڍU���ɉ��̒ǉ����ʂ��t�^�����B\r\n";
                    case 37: // Damnation
                        return this.name + "�F�����ʂĂ�ADamnation�I �������o�ł鍕����Ԃ�c�܂���B\r\n";
                    case 38: // Heat Boost
                        return this.name + "�F�����V�g��A���̐����؂�邺�AHeatBoost�I �Z�p�����^�� {0} �t�o�B\r\n";
                    case 39: // Immortal Rave
                        return this.name + "�F�I���I���I���@�������AImmortalRave�I " + this.name + "�̎���ɂR�̉����h�����B\r\n";
                    case 40: // Gale Wind
                        return this.name + "�F�����ɍs�����AGaleWind�I ������l��" + this.name + "�����ꂽ�B\r\n";
                    case 41: // Word of Life
                        return this.name + "�F���R�Ƌ��ɂ���AWordOfLife�I �厩�R����̋�����������������悤�ɂȂ����B\r\n";
                    case 42: // Word of Fortune
                        return this.name + "�F���������������AWordOfFortune�I �����̃I�[�����N���オ�����B\r\n";
                    case 43: // Aether Drive
                        return this.name + "�F��z�̕����͂������AAetherDrive�I ���͑S�̂ɋ�z�����͂��݂Ȃ���B\r\n";
                    case 44: // Eternal Presence 1
                        return this.name + "�F�@���ƌ������ĂыN�������AEternalPresence�I " + this.name + "�̎���ɐV�����@�����\�z�����B\r\n";
                    case 45: // Eternal Presence 2
                        return this.name + "�̕����U���A�����h��A���@�U���A���@�h�䂪�t�o�����I\r\n";
                    case 46: // One Immunity
                        return this.name +  "�F��ԏ�ǁAOneImmunity�I " + this.name + "�̎��͂ɖڂɌ����Ȃ���ǂ������B\r\n";
                    case 47: // Time Stop
                        return this.name + "�F�S�Ă͎��󊱏���E�E�ETimeStop�I �G�̎���������􂫎��Ԓ�~�������B\r\n";
                    case 48: // Dispel Magic
                        return this.name + "�F�E�U���Ă����ʂ͏����Ė����Ȃ�ADispelMagic�I \r\n";
                    case 49: // Rise of Image
                        return this.name + "�F����̎x�z�҂��V���Ȃ�C���[�W���؂�邺�E�E�ERiseOfImage�I �S�p�����^�� {0} �t�o\r\n";
                    case 50: // ��r��
                        return this.name + "�F���܂����B��r�����I�I\r\n";
                    case 51: // Inner Inspiration
                        return this.name + "�F�͐��ݏW���͂����߂��B���_���������܂����B {0} �X�L���|�C���g��\r\n";
                    case 52: // Resurrection 1
                        return this.name + "�F�̑�Ȃ鐹�̗́B��ՋN�������AResurrection�I�I\r\n";
                    case 53: // Resurrection 2
                        return this.name + "�F���܂����I�Ώۂ��ԈႦ������˂����I�I\r\n";
                    case 54: // Resurrection 3
                        return this.name + "�F���܂����I����ł˂����I�I\r\n";
                    case 55: // Resurrection 4
                        return this.name + "�F���������A�����ɂ���Ă��Ӗ��˂�����ȁE�E�E\r\n";
                    case 56: // Stance Of Standing
                        return this.name + "�F���̑̐��̂܂܁E�E�E������A�U�����I\r\n";
                    case 57: // Mirror Image
                        return this.name + "�F���@���͂˕Ԃ����AMirrorImage�I {0}�̎��͂ɐ���Ԃ����������B\r\n";
                    case 58: // Mirror Image 2
                        return this.name + "�F��������A�͂˕Ԃ��I {0} �_���[�W�� {1} �ɔ��˂����I\r\n";
                    case 59: // Mirror Image 3
                        return this.name + "�F��������A�͂˕Ԃ��I �y�������A���͂ȈЗ͂ł͂˕Ԃ��Ȃ��I�z" + this.name + "�F�o�J�ȁI�H\r\n";
                    case 60: // Deflection
                        return this.name + "�F�����U�����͂˕Ԃ����ADeflection�I {0}�̎��͂ɔ�����Ԃ����������B\r\n";
                    case 61: // Deflection 2
                        return this.name + "�F��������A�͂˕Ԃ��I {0} �_���[�W�� {1} �ɔ��˂����I\r\n";
                    case 62: // Deflection 3
                        return this.name + "�F��������A�͂˕Ԃ��I �y�������A���͂ȈЗ͂ł͂˕Ԃ��Ȃ��I�z" + this.name + "�F�b�O�A�O�A�@�I\r\n";
                    case 63: // Truth Vision
                        return this.name + "�F�{�������؂��Ă�邺�ATruthVision�I�@" + this.name + "�͑Ώۂ̃p�����^�t�o�𖳎�����悤�ɂȂ����B\r\n";
                    case 64: // Stance Of Flow
                        return this.name + "�F���͂����肾���ǂȁE�E�EStanceOfFlow�I�@" + this.name + "�͎��R�^�[���A�K����U�����悤�ɍ\�����B\r\n";
                    case 65: // Carnage Rush 1
                        return this.name + "�F�I���I���A�A�����b�V���������ACarnageRush�I �ق��I {0}�_���[�W�E�E�E   ";
                    case 66: // Carnage Rush 2
                        return "������������I {0}�_���[�W   ";
                    case 67: // Carnage Rush 3
                        return "�I���I���@�I {0}�_���[�W   ";
                    case 68: // Carnage Rush 4
                        return "�܂��܂��܂��I {0}�_���[�W   ";
                    case 69: // Carnage Rush 5
                        return "�H�炦�����I�I {0}�̃_���[�W\r\n";
                    case 70: // Crushing Blow
                        return this.name + "�F�K�c���ƈꌂ�~�߂Č����邺�ACrushingBlow�I  {0} �� {1} �̃_���[�W�B\r\n";
                    case 71: // ���[�x�X�g�����N�|�[�V�����퓬�g�p��
                        return this.name + "�F����ł��E�E�E�H�炢�ȁI\r\n";
                    case 72: // Enigma Sence
                        return this.name + "�F�͈ȊO�̃`�J�����Ă̌����Ă�邺�AEnigmaSennce�I\r\n";
                    case 73: // Soul Infinity
                        return this.name + "�F���̑S�Ă𒍂����ނ��I�I���A�A�@�@�I�I SoulInfinity�I�I�I\r\n";
                    case 74: // Kinetic Smash
                        return this.name + "�F���ɂ��U���A�ő���ɂ���Ă�邺�I�I���@�AKineticSmash�I\r\n";
                    case 75: // Silence Shot (Altomo��p)
                        return "";
                    case 76: // Silence Shot�AAbsoluteZero���قɂ��r�����s
                        return this.name + "�F�E�E�E�b�N�\�I�r�����s���I�I\r\n";
                    case 77: // Cleansing
                        return this.name + "�F���Ȍ��ʂ͑S������Ő􂢗������ACleansing�I\r\n";
                    case 78: // Pure Purification
                        return this.name + "�F���_������������ۂĂΒ�����͂����APurePurification�E�E�E\r\n";
                    case 79: // Void Extraction
                        return this.name + "�F�ő���E�l�A����ɒ����邺�BVoidExtraction�I" + this.name + "�� {0} �� {1}�t�o�I\r\n";
                    case 80: // �A�J�V�W�A�̎��g�p��
                        return this.name + "�F�ق��A����ŏ����͖ڂ����߂邾��B\r\n";
                    case 81: // Absolute Zero
                        return this.name + "�F�X�t���ɂ��Ă�邺�I�@AbsoluteZero�I �X������ʂɂ��A���C�t�񕜕s�A�X�y���r���s�A�X�L���g�p�s�A�h��s�ƂȂ����B\r\n";
                    case 82: // BUFFUP���ʂ��]�߂Ȃ��ꍇ
                        return "�������A���ɂ��̃p�����^�͏㏸���Ă��邽�߁A���ʂ��Ȃ������B\r\n";
                    case 83: // Promised Knowledge
                        return this.name + "�F�m������̗͂����APromiesdKnowledge�I�@�m�p�����^�� {0} �t�o\r\n";
                    case 84: // Tranquility
                        return this.name + "�F�����Ƃ��������ʂ��ȁB�������܂��ȁATranquility�I\r\n";
                    case 85: // High Emotionality 1
                        return this.name + "�F���̍ő�\�͂͂���Ȃ��̂���˂��A�E�I�I�H�H�AHighEmotionality�I\r\n";
                    case 86: // High Emotionality 2
                        return this.name + "�̗́A�Z�A�m�A�S�p�����^���t�o�����I\r\n";
                    case 87: // AbsoluteZero�ŃX�L���g�p���s
                        return this.name + "�F���E�E�E���߂��A�S�������˂��I�@�X�L���g�p�~�X�������B\r\n";
                    case 88: // AbsoluteZero�ɂ��h�䎸�s
                        return this.name + "�F�����E�E�E�̂��E�E�E�h��ł��˂��I \r\n";
                    case 89: // Silent Rush 1
                        return this.name + "�F���̂R�A�ŁA�󂯂Ă݂�ASilentRush�I �ق��I {0}�_���[�W�E�E�E   ";
                    case 90: // Silent Rush 2
                        return "������������I {0}�_���[�W   ";
                    case 91: // Silent Rush 3
                        return "�R���ڂ��I���炟�I {0}�̃_���[�W\r\n";
                    case 92: // BUFFUP�ȊO�̉i�����ʂ����ɂ��Ă���ꍇ
                        return "�������A���ɂ��̌��ʂ͕t�^����Ă���B\r\n";
                    case 93: // Anti Stun
                        return this.name + "�F�X�^�����ʂ͉��ɂ͌����˂����AAntiStun�I " + this.name + "�̓X�^�����ʂւ̑ϐ����t����\r\n";
                    case 94: // Anti Stun�ɂ��X�^�����
                        return this.name + "�F���Ă��E�E�E�����A�X�^�����Ȃ����B\r\n";
                    case 95: // Stance Of Death
                        return this.name + "�F���͑ς��Č����邺�AStanceOfDeath�I " + this.name + "�͒v�����P�x����ł���悤�ɂȂ���\r\n";
                    case 96: // Oboro Impact 1
                        return this.name + "�F�O�A���ނ��y���ɉ��`�zOboro Impact�I�I\r\n";
                    case 97: // Oboro Impact 2
                        return this.name + "�F�I���A�A�@�@�@�I�I�@ {0}��{1}�̃_���[�W\r\n";
                    case 98: // Catastrophe 1
                        return this.name + "�F�c����j�󂵂Ă�邺�y���ɉ��`�zCatastrophe�I\r\n";
                    case 99: // Catastrophe 2
                        return this.name + "�F�H�炢�₪�ꂥ�I�I�@ {0}�̃_���[�W\r\n";
                    case 100: // Stance Of Eyes
                        return this.name + "�F���̍s���A���؂��Ă݂��邺�A StanceOfEyes�I " + this.name + "�́A����̍s���ɔ����Ă���E�E�E\r\n";
                    case 101: // Stance Of Eyes�ɂ��L�����Z����
                        return this.name + "�F������A�\�R���I�I�@" + this.name + "�͑���̃��[�V���������؂��āA�s���L�����Z�������I\r\n";
                    case 102: // Stance Of Eyes�ɂ��L�����Z�����s��
                        return this.name + "�F�ʖڂ��E�E�E�S�R���؂�˂��E�E�E�@" + this.name + "�͑���̃��[�V���������؂�Ȃ������I\r\n";
                    case 103: // Negate
                        return this.name + "�F�X�y���r���Ȃ񂩂����邩��ANegate�I�@" + this.name + "�͑���̃X�y���r���ɔ����Ă���E�E�E\r\n";
                    case 104: // Negate�ɂ��X�y���r���L�����Z����
                        return this.name + "�F�����ʂ������I" + this.name + "�͑���̃X�y���r����e�����I\r\n";
                    case 105: // Negate�ɂ��X�y���r���L�����Z�����s��
                        return this.name + "�F�������A�S�R�r���^�C�~���O���E�E�E" + this.name + "�͑���̃X�y���r�������؂�Ȃ������I\r\n";
                    case 106: // Nothing Of Nothingness 1
                        return this.name + "�F�����[���Ɋ҂����肳���˂����A�y���ɉ��`�zNothingOfNothingness�I " + this.name + "�ɖ��F�̃I�[�����h��n�߂�I \r\n";
                    case 107: // Nothing Of Nothingness 2
                        return this.name + "�͖��������@�𖳌����A�@����������X�L���𖳌�������悤�ɂȂ����I\r\n";
                    case 108: // Genesis
                        return this.name + "�F�s�������̋N���A���ݍ���ł��邺�AGenesis�I  " + this.name + "�͑O��̍s���������ւƓ��e�������I\r\n";
                    case 109: // Cleansing�r�����s��
                        return this.name + "�F�������E�E�E���q���v�킵���˂��A������Cleansing�͏o���˂��B\r\n";
                    case 110: // CounterAttack�𖳎�������
                        return this.Name + "�F���̍\���͂������O�������I\r\n";
                    case 111: // �_�����g�p��
                        return this.name + "�F���C�t�E�X�L���E�}�i��30%���񕜂������B\r\n";
                    case 112: // �_�����A���Ɏg�p�ς݂̏ꍇ
                        return this.name + "�F�����͂�Ă��܂��Ă邺�B����P�񂾂����ȁB\r\n";
                    case 113: // CounterAttack�ɂ�锽�����b�Z�[�W
                        return this.name + "�F��������A���؂����I�J�E���^�[���I\r\n";
                    case 114: // CounterAttack�ɑ΂��锽����NothingOfNothingness�ɂ���Ėh���ꂽ��
                        return this.name + "�F�b�N�\�A�ǂ��Ȃ��Ă񂾁B���؂�˂��I\r\n";
                    case 115: // �ʏ�U���̃q�b�g
                        return this.name + "�̍U�����q�b�g�B {0} �� {1} �̃_���[�W\r\n";
                    case 116: // �h��𖳎����čU�����鎞
                        return this.name + "�F�h��Ȃ�Ă����ʂ������I�ђʂ���I�I\r\n";
                    case 117: // StraightSmash�Ȃǂ̃X�L���N���e�B�J��
                        return this.name + "�F������I�N���e�B�J�������I\r\n";
                    case 118: // �퓬���A�����@�C���|�[�V�����ɂ�镜���̂�����
                        return this.name + "�F������I����ŕ��������I\r\n";
                    case 119: // Absolute Zero�ɂ�胉�C�t�񕜂ł��Ȃ��ꍇ
                        return this.name + "�F�������E�E�E���C�t���񕜂ł��˂��E�E�E\r\n";
                    case 120: // ���@�U���̃q�b�g
                        return "{0} �� {1} �̃_���[�W\r\n";
                    case 121: // Absolute Zero�ɂ��}�i�񕜂ł��Ȃ��ꍇ
                        return this.name + "�F�������E�E�E�}�i���񕜂ł��˂��E�E�E\r\n";
                    case 122: // �u���߂�v�s����
                        return this.name + "�F���́A�~�������Ă���������B\r\n";
                    case 123: // �u���߂�v�s���ŗ��܂肫���Ă���ꍇ
                        return this.name + "�F���͂̒~�ς͂��ꂪ����݂������ȁB\r\n";
                    case 124: // StraightSmash�̃_���[�W�l
                        return this.name + "�̃X�g���[�g�E�X�}�b�V�����q�b�g�B {0} �� {1}�̃_���[�W\r\n";
                    case 125: // �A�C�e���g�p�Q�[�W�����܂��ĂȂ��Ƃ�
                        return this.name + "�F�A�C�e���Q�[�W�����܂��ĂȂ��ƃA�C�e���͎g���Ȃ����B\r\n";
                    case 126: // FlashBlase
                        return this.name + "�F���̑M���ł��H�炢�ȁAFlashBlaze�I {0} �� {1} �̃_���[�W\r\n";
                    case 127: // �������@�ŃC���X�^���g�s��
                        return this.name + "�F�������A�܂��C���X�^���g�l������˂��I\r\n";
                    case 128: // �������@�̓C���X�^���g�^�C�~���O�ł��ĂȂ�
                        return this.name + "�F���߂��A����̓C���X�^���g�Ŕ����ł��˂��B\r\n";
                    case 129: // PsychicTrance
                        return this.name + "�F�����̋]���𕥂��Ăł����@�U���́A�X�ɋ������邺�IPsychicTrance�I\r\n";
                    case 130: // BlindJustice
                        return this.name + "�F�ł������o�傾�B�����U���A�X�ɋ������邺�IBlindJustice�I\r\n";
                    case 131: // TranscendentWish
                        return this.name + "�F���̈�u�ŃP��������ATranscendentWish�I\r\n";
                    case 132: // LightDetonator
                        return this.name + "�F���̃t�B�[���h�S�́A��������I LightDetonator�I\r\n";
                    case 133: // AscendantMeteor
                        return this.name + "�F�H�炦�I�{���̂P�O�A���AAscendantMeteor�I\r\n";
                    case 134: // SkyShield
                        return this.name + "�F��Ɛ�������𓾂邺�ASkyShield�I\r\n";
                    case 135: // SacredHeal
                        return this.name + "�F�S���񕜂��ƁI�@SacredHeal�I\r\n";
                    case 136: // EverDroplet
                        return this.name + "�F�}�i�̏z����������AEverDroplet�I\r\n";
                    case 137: // FrozenAura
                        return this.name + "�F�X�̖��@���𔭓����邺�AFrozenAura�I\r\n";
                    case 138: // ChillBurn
                        return this.name + "�F������I ChillBurn�I\r\n";
                    case 139: // ZetaExplosion
                        return this.name + "�F���ɁI ZetaExplosion�I\r\n";
                    case 140: // FrozenAura�ǉ����ʃ_���[�W��
                        return this.name + "�F������ȁI {0}�̒ǉ��_���[�W\r\n";
                    case 141: // StarLightning
                        return this.name + "�F�R���ŋC�₵�ȁAStarLightning�I\r\n";
                    case 142: // WordOfMalice
                        return this.name + "�F������݂������邺�AWordOfMalice�I\r\n";
                    case 143: // BlackFire
                        return this.name + "�F���@�h�䗎�Ƃ��Ă�邺�ABlackFire�I\r\n";
                    case 144: // EnrageBlast
                        return this.name + "�F�u�`���܂����AEnrageBlast�I\r\n";
                    case 145: // Immolate
                        return this.name + "�F�����h�䗎�Ƃ��Ă�邺�AImmolate�I\r\n";
                    case 146: // VanishWave
                        return this.name + "�F�ق点�Ă�邺�AVanishWave�I\r\n";
                    case 147: // WordOfAttitude
                        return this.name + "�F�C���X�^���g�񕜂����邺�AWordOfAttitude�I\r\n";
                    case 148: // HolyBreaker
                        return this.name + "�F����Ń_���[�W���[�X��D�ʂɂ��Ă݂���BHolyBreaker�I\r\n";
                    case 149: // DarkenField
                        return this.name + "�F�h��͂��K�^�����ɂ��Ă�邺�ADarkenField�I\r\n";
                    case 150: // SeventhMagic
                        return this.name + "�F���A�S�Ă𕢂����ASeventhMagic�I\r\n";
                    case 151: // BlueBullet
                        return this.name + "�F�X�̒e�ۂ�A�˂��邺�ABlueBullet�I\r\n";
                    case 152: // NeutralSmash
                        return this.name + "�F������ANeutralSmash�I\r\n";
                    case 153: // SwiftStep
                        return this.name + "�F���x�l�グ�Ă����ASwiftStep�I\r\n";
                    case 154: // CircleSlash
                        return this.name + "�F�R���ł��H�炢�ȁACircleSlash�I\r\n";
                    case 155: // RumbleShout
                        return this.name + "�F�ǂ����Ă�A�R�b�`���I\r\n";
                    case 156: // SmoothingMove
                        return this.name + "�F�X���C�h���퓬�@�ASmoothingMove�I\r\n";
                    case 157: // FutureVision
                        return this.name + "�F���̃^�[���A���؂������BFutureVision�I\r\n";
                    case 158: // ReflexSpirit
                        return this.name + "�F�X�^���▃Ⴢ̓S���������AReflexSpirit�I\r\n";
                    case 159: // SharpGlare
                        return this.name + "�F�ق点�Ă�邺�ASharpGlare�I\r\n";
                    case 160: // TrustSilence
                        return this.name + "�F���ق�U�f�ɂ������Ă��邩�ATrustSilence�I\r\n";
                    case 161: // SurpriseAttack
                        return this.name + "�F�S���R�U�炵�Ă�邺�ASurpriseAttack�I\r\n";
                    case 162: // PsychicWave
                        return this.name + "�F���̋Z�͎~�߂��₵�˂����APsychicWave�I\r\n";
                    case 163: // Recover
                        return this.name + "�F�������肵��IRecover�I\r\n";
                    case 164: // ViolentSlash
                        return this.name + "�F���؂�˂�����R�C�c�́AViolentSlash�I\r\n";
                    case 165: // OuterInspiration
                        return this.name + "�F�|�e���V�����A�����߂����AOuterInspiration�I\r\n";
                    case 166: // StanceOfSuddenness
                        return this.name + "�F������A�\�����IStanceOfSuddenness�I\r\n";
                    case 167: // �C���X�^���g�ΏۂŔ����s��
                        return this.name + "�F�_�����A�����̓C���X�^���g�Ώې�p���B\r\n";
                    case 168: // StanceOfMystic
                        return this.name + "�F�����_���[�W�͋���˂��AStanceOfMystic�I\r\n";
                    case 169: // HardestParry
                        return this.name + "�F�u�Ԃő����Ă݂���BHardestParry�I\r\n";
                    case 170: // ConcussiveHit
                        return this.name + "�F����ł��H�炢�ȁA�R���J�b�V���E�q�b�g�I\r\n";
                    case 171: // Onslaught hit
                        return this.name + "�F����ł��H�炢�ȁA�I���X���[�g�E�q�b�g�I\r\n";
                    case 172: // Impulse hit
                        return this.name + "�F����ł��H�炢�ȁA�C���p���X�E�q�b�g�I\r\n";
                    case 173: // Fatal Blow
                        return this.name + "�F�ӂ��U��A�t�F�C�^���E�u���[�I\r\n";
                    case 174: // Exalted Field
                        return this.name + "�F�^���̏���\�z���邺�A�C�O�U���e�B�b�h�E�t�B�[���h�I\r\n";
                    case 175: // Rising Aura
                        return this.name + "�F�K���K���s�����A���C�W���O�E�I�[���I\r\n";
                    case 176: // Ascension Aura
                        return this.name + "�F�ǂ�ǂ�Ԃ����܂����A�A�Z���V�����E�I�[���I\r\n";
                    case 177: // Angel Breath
                        return this.name + "�F������A�S���̏�Ԃ�߂����A�G���W�F���E�u���X�I\r\n";
                    case 178: // Blazing Field
                        return this.name + "�F�R�₵�s�������A�u���C�W���O�E�t�B�[���h�I\r\n";
                    case 179: // Deep Mirror
                        return this.name + "�F���̎�͒ʂ��Ȃ����A�f�B�[�v�E�~���[�I\r\n";
                    case 180: // Abyss Eye
                        return this.name + "�F�[���̊�A�A�r�X�E�A�C�I\r\n";
                    case 181: // Doom Blade
                        return this.name + "�F���_�܂Ő؂荏�ނ��A�h�D�[���E�u���C�h�I\r\n";
                    case 182: // Piercing Flame
                        return this.name + "�F������A����ł��H�炢�ȁI�s�A�b�V���O�E�t���C���I\r\n";
                    case 183: // Phantasmal Wind
                        return this.name + "�F�����グ�Ă������A�t�@���^�Y�}���E�E�B���h�I\r\n";
                    case 184: // Paradox Image
                        return this.name + "�F�E�E�E�p���h�b�N�X�E�C���[�W�I\r\n";
                    case 185: // Vortex Field
                        return this.name + "�F���̉Q�̒��ɖ����ꂳ���Ă�邺�A���H���e�b�N�X�E�t�B�[���h�I\r\n";
                    case 186: // Static Barrier
                        return this.name + "�F���Ɨ�������𓾂邺�A�X�^�e�B�b�N�E�o���A�I\r\n";
                    case 187: // Unknown Shock
                        return this.name + "�F�Ӗڂɂ��Ă�邺�A�A���m�E���E�V���b�N�I\r\n";
                    case 188: // SoulExecution
                        return this.name + "�F���`�@�\�E���E�G�O�[�L���[�V�����I\r\n";
                    case 189: // SoulExecution hit 01
                        return this.name + "�F�b���I\r\n";
                    case 190: // SoulExecution hit 02
                        return this.name + "�F���@�I\r\n";
                    case 191: // SoulExecution hit 03
                        return this.name + "�F�b�V���I\r\n";
                    case 192: // SoulExecution hit 04
                        return this.name + "�F�b�e�B�I\r\n";
                    case 193: // SoulExecution hit 05
                        return this.name + "�F�g�H�I\r\n";
                    case 194: // SoulExecution hit 06
                        return this.name + "�F�b�t�I\r\n";
                    case 195: // SoulExecution hit 07
                        return this.name + "�F�b�n�I\r\n";
                    case 196: // SoulExecution hit 08
                        return this.name + "�F�b�n�@�I\r\n";
                    case 197: // SoulExecution hit 09
                        return this.name + "�F�I���@�I\r\n";
                    case 198: // SoulExecution hit 10
                        return this.name + "�F�I��肾�I�I�I\r\n";
                    case 199: // Nourish Sense
                        return this.name + "�F����ɉ񕜂��Ă������A�m���b�V���E�Z���X�I\r\n";
                    case 200: // Mind Killing
                        return this.name + "�F���_�؂荏�ނ��A�}�C���h�E�L�����O�I\r\n";
                    case 201: // Vigor Sense
                        return this.name + "�F�����l�グ�Ă������A���B�S�[�E�Z���X�I\r\n";
                    case 202: // ONE Authority
                        return this.name + "�F��������A�S���グ�Ă��������I�����E�I�[�\���e�B�I\r\n";
                    case 203: // �W���ƒf��
                        return this.name + "�F�y�W���ƒf��z�@�����B\r\n";
                    case 204: // �y���j�z�����ς�
                        return this.name + "�F�y���j�z�͍��������g�����܂������B\r\n";
                    case 205: // �y���j�z�ʏ�s���I����
                        return this.name + "�F�y���j�z�̓C���X�^���g�^�C�~���O���肾�ȁB\r\n";
                    case 206: // Sigil Of Homura
                        return this.name + "�F���̈З́A�v���m��I�V�M���E�I�u�E�z�����I\r\n";
                    case 207: // Austerity Matrix
                        return this.name + "�F�x�z���������Ă��炤���A�A�D�X�e���e�B�E�}�g���N�X�I\r\n";
                    case 208: // Red Dragon Will
                        return this.name + "�F�΂̗���A���ɍX�Ȃ�͂��I���b�h�E�h���S���E�E�B���I\r\n";
                    case 209: // Blue Dragon Will
                        return this.name + "�F���̗���A���ɍX�Ȃ�͂��I�u���[�E�h���S���E�E�B���I\r\n";
                    case 210: // Eclipse End
                        return this.name + "�F���ɂ̑S�����A�G�N���v�X�E�G���h�I\r\n";
                    case 211: // Sin Fortune
                        return this.name + "�F���Ō��߂邺�A�V���E�t�H�[�`�����I\r\n";
                    case 212: // AfterReviveHalf
                        return this.name + "�F���ϐ��i�n�[�t�j��t���邺�I\r\n";
                    case 213: // Demonic Ignite
                        return this.name + "�F���̉������̐g�Ŏ󂯂�I�f�[���j�b�N�E�C�O�i�C�g�I\r\n";
                    case 214: // Death Deny
                        return this.name + "�F���҂����S�Ȃ��Ԃőh�������邺�A�f�X�E�f�B�i�C�I\r\n";
                    case 215: // Stance of Double
                        return this.name + "�F���ɂ̍s�������A�X�^���X�E�I�u�E�_�u���I  " + this.name + "�͑O��s�������������̕��g�𔭐��������I\r\n";
                    case 216: // �ŏI�탉�C�t�J�E���g����
                        return this.name + "�F�܂����E�E�E�܂��A�������˂��񂾁I�I\r\n";
                    case 217: // �ŏI�탉�C�t�J�E���g���Ŏ�
                        return this.name + "�F�E�E�E�b�O�E�E�E�����E�E�E\r\n";

                    case 2001: // �|�[�V�����܂��͖��@�ɂ��񕜎�
                        return this.name + "�F�悵�A{0} �񕜂������B";
                    case 2002: // ���x���A�b�v�I���Ñ�
                        return this.name + "�F���x���A�b�v���Ă���ɂ��悤���B";
                    case 2003: // �ו������点��Ñ�
                        return this.name + "�F{0}�A�����ו������炵�Ă�����B�A�C�e�����n���˂����B";
                    case 2004: // �������f
                        return this.name + "�F�悵�A�������Ƃ����H";
                    case 2005: // ��������
                        return this.name + "�F�I�[�P�[�A���������I";
                    case 2006: // �����̐������g�p
                        return this.name + "�F��������A���֖߂邩�H";
                    case 2007: // ���p��p�i�ɑ΂���ꌾ
                        return this.name + "�F���p��p�i���ȁB";
                    case 2008: // ��퓬���̃}�i�s��
                        return this.name + "�F�}�i�s�����ȁB";
                    case 2009: // �_�����g�p��
                        return this.name + "�F���C�t�E�X�L���E�}�i��30%���񕜂������B";
                    case 2010: // �_�����A���Ɏg�p�ς݂̏ꍇ
                        return this.name + "�F�����͂�Ă��܂��Ă邺�B����P�񂾂����ȁB";
                    case 2011: // ���[�x�X�g�����N�|�[�V������퓬�g�p��
                        return this.name + "�F�퓬����p�i���ȁB";
                    case 2012: // �����̐������g�p�i���؍ݎ��j
                        return this.name + "�F���łɒ��̒����B�g���������ʂ��ȁB";
                    case 2013: // �����̐������̂Ă��Ȃ����̑䎌
                        return this.name + "�F���������A�������ɂ�����̂Ă���ʖڂ���B";
                    case 2014: // ��퓬���̃X�L���s��
                        return this.name + "�F�X�L���s�����ȁB";
                    case 2015: // ���v���C���[���̂ĂĂ��܂����Ƃ����ꍇ
                        return this.name + "�F�����ƁA����͎̂ĂĂ�������።�邺�B";
                    case 2016: // �����@�C���|�[�V�����ɂ�镜��
                        return this.name + "�F������A���������I�T���L���[�I";
                    case 2017: // �����@�C���|�[�V�����s�v�Ȏ�
                        return this.name + "�F{0}�͂܂������Ă邶��˂����B�g�p����K�v�͂Ȃ����B";
                    case 2018: // �����@�C���|�[�V�����Ώۂ������̎�
                        return this.name + "�F�����g�Ɏg���Ă��Ӗ��Ȃ�����B";
                    case 2019: // �����s�̃A�C�e���𑕔����悤�Ƃ�����
                        return this.name + "�F���ɂ͑����o���Ȃ��悤���B";
                    case 2020: // ���X�{�X���j�̌�A�����̐������g�p�s��
                        return this.name + "�F����A���R���͎g������͂˂�";
                    case 2021: // �A�C�e���̂Ă�̍Ñ�
                        return this.name + "�F�o�b�N�p�b�N�̐������ɂ��悤���B";
                    case 2022: // �I�[�o�[�V�t�e�B���O�g�p�J�n��
                        return this.name + "�F�������E�E�E�g�̂̃p�����^���č\�z����Ă����E�E�E";
                    case 2023: // �I�[�o�[�V�t�e�B���O�ɂ��p�����^����U�莞
                        return this.name + "�F�I�[�o�[�V�t�e�B���O�ɂ�銄��U��𑁂��g�R����Ă��܂������B";
                    case 2024: // �������L�b�h���g�p������
                        return this.name + "�F�y{0}�z�p�����^�� {1} �㏸�������I";
                    case 2025:
                        return this.name + "�F���蕐��������Ă�ꍇ�A�T�u�͑����ł��˂����B";
                    case 2026:
                        return this.name + "�F����i���C���j�ɂ܂������������悤���B";
                    case 2027: // �������g�p��
                        return this.name + "�F���C�t��100%�񕜂������B";
                    case 2028: // �������A���Ɏg�p�ς݂̏ꍇ
                        return this.name + "�F�����͂�Ă��܂��Ă邺�B����P�񂾂����ȁB";
                    case 2029: // �ו���t�ő������O���Ȃ���
                        return this.name + "�F�o�b�N�p�b�N�������ς����B�����O���O�Ƀo�b�N�p�b�N�������悤���B";
                    case 2030: // �ו���t�ŉ������̂Ă鎞�̑䎌
                        return this.name + "�F{0}���̂ĂĐV�����A�C�e������肷�邩�H";
                    case 2031: // �퓬���̃A�C�e���g�p����
                        return this.name + "�F�퓬�������A�A�C�e���g�p�ɏW�����悤���B";
                    case 2032: // �퓬���A�A�C�e���g�p�ł��Ȃ��A�C�e����I�������Ƃ�
                        return this.name + "�F���̃A�C�e���͐퓬���Ɏg�p�͏o���Ȃ����B";
                    case 2033: // �a�����Ȃ��ꍇ
                        return this.name + "�F�����a���Ă����킯�ɂ͂����˂���ȁB";
                    case 2034: // �A�C�e�������ς��ŗa���菊��������o���Ȃ��ꍇ
                        return this.name + "�F�����Ɖו��������ς������B";
                    case 2035: // Sacred Heal
                        return this.name + "�F�����A�񕜂������B";

                    case 3000: // �X�ɓ��������̑䎌
                        return this.name + "�F�{���ɒN��������������Ȃ��񂾂ȁB";
                    case 3001: // �x�����v����
                        return this.name + "�F�����A���� {0} �𔃂����B {1} Gold�����Ηǂ��񂾂ȁH";
                    case 3002: // �����������ς��Ŕ����Ȃ���
                        return this.name + "�F���܂����A�������������ς����B�莝���̃A�C�e���𐮗����Ă��炾�ȁB";
                    case 3003: // �w��������
                        return this.name + "�F�������A���������I�E�E�E����ȁH";
                    case 3004: // Gold�s���ōw���ł��Ȃ��ꍇ
                        return this.name + "�F�N�\�AGold���܂�{0}����˂��E�E�E";
                    case 3005: // �w�������L�����Z�������ꍇ
                        return this.name + "�F���̃A�C�e���ł����Ă݂邩";
                    case 3006: // ����Ȃ��A�C�e���𔄂낤�Ƃ����ꍇ
                        return this.name + "�F����͎�����킯�ɂ͂����˂��ȁB";
                    case 3007: // �A�C�e�����p��
                        return this.name + "�F���肪���˂����E�E�E{0}��u���āA{1}Gold�E�E�E�����������H";
                    case 3008: // ����̓y���_���g���p��
                        return this.name + "�F�R���͂�����Ɣ���̂͐S���Ђ��邪�E�E�E{0}Gold�����������H"; 
                    case 3009: // ����X���o�鎞
                        return this.name + "�F�K���c�f������A������l���炢�t����Ƃ����̂ɂȁE�E�E";
                    case 3010: // �K���c�s�ݎ��̔��肫��t�F���g�D�[�V�������Ĉꌾ
                        return this.name + "�F���X�����Ă�킯���˂��񂾂�ȁE�E�E";
                    case 3011: // �����\�Ȃ��̂��w�����ꂽ��
                        return this.name + "�F������A�����ő������Ă������H";
                    case 3012: // �������Ă������𔄋p�Ώۂ��ǂ���������
                        return this.name + "�F���������Ă���́A{0}���B{1}Gold���炢�Ŕ�������Ă��炦��񂶂�˂����H";

                    case 4001: // �ʏ�U����I��
                        return this.name + "�F������A�U�����I\r\n";
                    case 4002: // �h���I��
                        return this.name + "�F�h��p���ō\���Ă������B\r\n";
                    case 4003: // �ҋ@��I��
                        return this.name + "�F���������ҋ@�ƍs�����E�E�E\r\n";
                    case 4004: // �t���b�V���q�[����I��
                        return this.name + "�F�����̓t���b�V���q�[���ŉ񕜂��B\r\n";
                    case 4005: // �v���e�N�V������I��
                        return this.name + "�F�h��͏グ�邺�A�v���e�N�V�������B\r\n";
                    case 4006: // �t�@�C�A�E�{�[����I��
                        return this.name + "�F�t�@�C�A�{�[�����I\r\n";
                    case 4007: // �t���C���E�I�[����I��
                        return this.name + "�F�����́A�t���C����t���Ă������B\r\n";
                    case 4008: // �X�g���[�g�E�X�}�b�V����I��
                        return this.name + "�F�������I�X�g���[�g�X�}�b�V���I\r\n";
                    case 4009: // �_�u���E�X�}�b�V����I��
                        return this.name + "�F�Q�A���������A�_�u���X�}�b�V���I\r\n";
                    case 4010: // �X�^���X�E�I�u�E�X�^���f�B���O��I��
                        return this.name + "�F����čU�߂�A�X�^���f�B���O�̍\�����B\r\n";
                    case 4011: // �A�C�X�E�j�[�h����I��
                        return this.name + "�F�A�C�X�j�[�h���ł������I\r\n";
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
                        return this.name + "�F" + this.ActionLabel.Text + "�ł������B\r\n";

                    case 4072:
                        return this.name + "�F����͓G�ɂ������Ȃ����B\r\n";
                    case 4073:
                        return this.name + "�F����͓G�ɂ������Ȃ����B\r\n";
                    case 4074:
                        return this.name + "�F����͖����ɂ��������͂˂��B\r\n";
                    case 4075:
                        return this.name + "�F����͖����ɂ��������͂˂��B\r\n";
                    case 4076:
                        return this.name + "�F�����ɍU���͎d�|�����Ȃ����B\r\n";
                    case 4077: // �u���߂�v�R�}���h
                        return this.name + "�F�p���[�����߂邺�B\r\n";
                    case 4078: // ���픭���u���C���v
                        return this.name + "�F���C������̌��ʂ𔭓������邺�B\r\n";
                    case 4079: // ���픭���u�T�u�v
                        return this.name + "�F�T�u����̌��ʂ𔭓������邺�B\r\n";
                    case 4080: // �A�N�Z�T���P����
                        return this.name + "�F�A�N�Z�T���P�̌��ʂ𔭓������邺�B\r\n";
                    case 4081: // �A�N�Z�T���Q����
                        return this.name + "�F�A�N�Z�T���Q�̌��ʂ𔭓������邺�B\r\n";

                    case 4082: // FlashBlaze
                        return this.name + "�F�t���b�V���u���C�Y�������I\r\n";

                    // ����U��
                    case 5001:
                        return this.name + "�F����ł��H�炢�ȁI�G�A���E�X���b�V���I {0} �� {1} �̃_���[�W\r\n";
                    case 5002:
                        return this.name + "�F{0} �񕜂��ƁI \r\n";
                    case 5003:
                        return this.name + "�F{0}�}�i�񕜂��ƁI\r\n";
                    case 5004:
                        return this.name + "�F������I�A�C�V�N���E�X���b�V���I {0} �� {1} �̃_���[�W\r\n";
                    case 5005:
                        return this.name + "�F�ӂ���I�G���N�g���E�u���[�I {0} �� {1} �̃_���[�W\r\n";
                    case 5006:
                        return this.name + "�F����ł��H�炢�ȁI�u���[�E���C�g�j���O�I {0} �� {1} �̃_���[�W\r\n";
                    case 5007:
                        return this.name + "�F����ł��H�炢�ȁI�o�[�j���O�E�N���C���A�I {0} �� {1} �̃_���[�W\r\n";
                    case 5008:
                        return this.name + "�F���̑��̉��ł��H�炤�񂾂ȁI {0} �� {1} �̃_���[�W\r\n";
                    case 5009:
                        return this.name + "�F{0}�X�L���|�C���g�񕜂��ƁI\r\n";
                    case 5010:
                        return this.name + "���������Ă���w�ւ�����ɑ�������������I {0} �� {1} �̃_���[�W\r\n";
                }
            }
            #endregion
            #region "���i"
            else if (this.name == "���i")
            {
                switch (sentenceNumber)
                {
                    case 0: // �X�L���s��
                        return this.name + "�F��������ƁA�X�L���|�C���g������Ȃ�����Ȃ��I\r\n";
                    case 1: // Straight Smash
                        return this.name + "�F�������B\r\n";
                    case 2: // Double Slash 1
                        return this.name + "�F�n�C�b�I\r\n";
                    case 3: // Double Slash 2
                        return this.name + "�F�Z�C�b�I\r\n";
                    case 4: // Painful Insanity
                        return this.name + "�F�y�S�ቜ�`�z�I���Ȃ��ɂ݁A�󂯑����邪�ǂ���B\r\n";
                    case 5: // empty skill
                        return this.name + "�F��������ƁA�X�L���I�����ĂȂ�����Ȃ��I\r\n";
                    case 6: // ���݂��t�����V�X�̕K�E��H�������
                        return this.name + "�F�ɂ��I�@�h����ˁE�E�E\r\n";
                    case 7: // Lizenos�̕K�E��H�������
                        return this.name + ": �ʖځE�E�E������S�R�ǂ��Ȃ���E�E�E\r\n";
                    case 8: // Minflore�̕K�E��H�������
                        return this.name + "�F����A�����؂�Ȃ��I�b�L���A�A�@�I�I�I\r\n";
                    case 9: // Fresh Heal�ɂ�郉�C�t��
                        return this.name + "�F{0} �񕜂ˁB\r\n";
                    case 10: // Fire Ball
                        return this.name + "�F���̋ʁAFireBall��B {0} �� {1} �̃_���[�W\r\n";
                    case 11: // Flame Strike
                        return this.name + "�F���̑��AFlameStrike��B {0} �� {1} �̃_���[�W\r\n";
                    case 12: // Volcanic Wave
                        return this.name + "�F�^�g�̋Ɖ΁AVolcanicWave��B {0} �� {1} �̃_���[�W\r\n";
                    case 13: // �ʏ�U���N���e�B�J���q�b�g
                        return this.name + "�F�N���e�B�J���q�b�g��I {0} �� {1} �̃_���[�W\r\n";
                    case 14: // FlameAura�ɂ��ǉ��U��
                        return this.name + "�F�t���C���I {0}�̒ǉ��_���[�W\r\n";
                    case 15: // �����̐�����퓬���Ɏg�����Ƃ�
                        return this.name + "�F�퓬���Ɏg���Ă��Ӗ��Ȃ��̂�ˁB\r\n";
                    case 16: // ���ʂ𔭊����Ȃ��A�C�e����퓬���Ɏg�����Ƃ�
                        return this.name + "�F�z���g�A�g���Ȃ��A�C�e���ˁB�����N���Ȃ���B\r\n";
                    case 17: // ���@�Ń}�i�s��
                        return this.name + "�F���A�}�i�s����������B\r\n";
                    case 18: // Protection
                        return this.name + "�F���Ȃ�_�ɂ�����AProtection��B�����h��́F�t�o\r\n";
                    case 19: // Absorb Water
                        return this.name + "�F���̏��_�ɂ�����E�E�EAbsorbWater��B ���@�h��́F�t�o�B\r\n";
                    case 20: // Saint Power
                        return this.name + "�F���Ȃ�_���ɗ͂��ASaintPower�B �����U���́F�t�o\r\n";
                    case 21: // Shadow Pact
                        return this.name + "�F�łƂ̌_��AShadowPact��B ���@�U���́F�t�o\r\n";
                    case 22: // Bloody Vengeance
                        return this.name + "�F�ł̎g�҂ɂ�����E�E�EBloodyVengeance��B �̓p�����^�� {0} �t�o\r\n";
                    case 23: // Holy Shock
                        return this.name + "�F�n���}�[�Ńh�J�����Ƃˁ� HolyShock�I {0} �� {1} �̃_���[�W\r\n";
                    case 24: // Glory
                        return this.name + "�F�h���̌����Ƃ点�AGlory��B ���ڍU���{FreshHeal�A�g�̃I�[��\r\n";
                    case 25: // CelestialNova 1
                        return this.name + "�F���Ȃ���ŕ�ݍ��ނ�ACelestialNova��B {0} �񕜂ˁB\r\n";
                    case 26: // CelestialNova 2
                        return this.name + "�F���Ȃ�ق��̌��ACelestialNova��B {0} �̃_���[�W\r\n";
                    case 27: // Dark Blast
                        return this.name + "�F�����g���A�ς����邩����ADarkBlast��B {0} �� {1} �̃_���[�W\r\n";
                    case 28: // Lava Annihilation
                        return this.name + "�F���ꂪ���̉����V�g�l��ALavaAnnihilation�I {0} �� {1} �̃_���[�W\r\n";
                    case 10028: // Lava Annihilation���
                        return this.name + "�F���ꂪ���̉����V�g�l��ALavaAnnihilation�I\r\n";
                    case 29: // Devouring Plague
                        return this.name + "�F�̗͂𒸂��Ƃ����ADevouringPlague��B {0} ���C�t���z�������\r\n";
                    case 30: // Ice Needle
                        return this.name + "�F������A�X�̐j��� IceNeedle�B {0} �� {1} �̃_���[�W\r\n";
                    case 31: // Frozen Lance
                        return this.name + "�F�������ꂽ���A�󂯂Ă݂Ȃ����AFrozenLance��B {0} �� {1} �̃_���[�W\r\n";
                    case 32: // Tidal Wave
                        return this.name + "�F��n�������ݍ��ޑs��Ȃ�X��ATidalWave��I {0} �̃_���[�W\r\n";
                    case 33: // Word of Power
                        return this.name + "�F���t�ɂ��͂͂����AWordOfPower��B {0} �� {1} �̃_���[�W\r\n";
                    case 34: // White Out
                        return this.name + "�F�܊��̑S�Ă���������u�ԁAWhiteOut��B {0} �� {1} �̃_���[�W\r\n";
                    case 35: // Black Contract
                        return this.name + "�F��������̊m�񂳂ꂽ�́ABlackContract��B " + this.name + "�̃X�L���A���@�R�X�g�͂O�ɂȂ�B\r\n";
                    case 36: // Flame Aura�r��
                        return this.name + "�F�G���`�����g�����ĕ֗��ˁAFlameAura��B ���ڍU���ɉ��̒ǉ����ʂ��t�^�����B\r\n";
                    case 37: // Damnation
                        return this.name + "�F�ł̐[���A������Ȃ����ADamnation�I �������o�ł鍕����Ԃ�c�܂���B\r\n";
                    case 38: // Heat Boost
                        return this.name + "�F�����V�g������𓾂�AHeatBoost��B �Z�p�����^�� {0} �t�o�B\r\n";
                    case 39: // Immortal Rave
                        return this.name + "�F���p�����AImmortalRave��I " + this.name + "�̎���ɂR�̉����h�����B\r\n";
                    case 40: // Gale Wind
                        return this.name + "�F�^���̌��e�AGaleWind��B ������l��" + this.name + "�����ꂽ�B\r\n";
                    case 41: // Word of Life
                        return this.name + "�F�i�v�Ȃ鎩�R�̉��b�AWordOfLife��B �厩�R����̋�����������������悤�ɂȂ����B\r\n";
                    case 42: // Word of Fortune
                        return this.name + "�F�������A�����A�m���WordOfFortune��B �����̃I�[�����N���オ�����B\r\n";
                    case 43: // Aether Drive
                        return this.name + "�F�n����̕����g�킹�Ă��炤��AAetherDrive�I ���͑S�̂ɋ�z�����͂��݂Ȃ���B\r\n";
                    case 44: // Eternal Presence 1
                        return this.name + "�F�V�����n���ƌ����\�z�AEternalPresence��B " + this.name + "�̎���ɐV�����@�����\�z�����B\r\n";
                    case 45: // Eternal Presence 2
                        return this.name + "�̕����U���A�����h��A���@�U���A���@�h�䂪�t�o�����I\r\n";
                    case 46: // One Immunity
                        return this.name + "�F��Ԃ̕ǂ��ĂƂ�������� OneImmunity��B " + this.name + "�̎��͂ɖڂɌ����Ȃ���ǂ������B\r\n";
                    case 47: // Time Stop
                        return this.name + "�F����f��A�F�����̂��΂����ATimeStop�I �G�̎���������􂫎��Ԓ�~�������B\r\n";
                    case 48: // Dispel Magic
                        return this.name + "�F�i���I���ʂ͖��ɋA��ׂ��ˁADispelMagic��B \r\n";
                    case 49: // Rise of Image
                        return this.name + "�F����̎x�z�҂�����𓾂�ARiseOfImage��B �S�p�����^�� {0} �t�o\r\n";
                    case 50: // ��r��
                        return this.name + "�F��������ƁA��r������Ȃ��́I\r\n";
                    case 51: // Inner Inspiration
                        return this.name + "�F�͐��ݏW���͂����߂��B���_���������܂����B {0} �X�L���|�C���g��\r\n";
                    case 52: // Resurrection 1
                        return this.name + "�F���̗͂͊�Ղ���N������̂�AResurrection�I�I\r\n";
                    case 53: // Resurrection 2
                        return this.name + "�F��������ƁA�ΏۊԈႦ�����������Ȃ��I�I\r\n";
                    case 54: // Resurrection 3
                        return this.name + "�F���A�����Ă��E�E�E�S�����ˁ�\r\n";
                    case 55: // Resurrection 4
                        return this.name + "�F�����ɂ���Ă����ʂȂ��̂�ˁE�E�E\r\n";
                    case 56: // Stance Of Standing
                        return this.name + "�F�h��̐����ێ������܂܁E�E�E�U����I\r\n";
                    case 57: // Mirror Image
                        return this.name + "�F�����̏��_��薂�@���˂̉���𓾂�AMirrorImage��B{0}�̎��͂ɐ���Ԃ����������B\r\n";
                    case 58: // Mirror Image 2
                        return this.name + "�F������A�͂˕Ԃ��̂�I {0} �_���[�W�� {1} �ɔ��˂����I\r\n";
                    case 59: // Mirror Image 3
                        return this.name + "�F������A�͂˕Ԃ��̂�I �y�������A���͂ȈЗ͂ł͂˕Ԃ��Ȃ��I�z" + this.name + "�F�����A�E�\�I�H\r\n";
                    case 60: // Deflection
                        return this.name + "�F�����̎g�҂�蕨�����˂̉���𓾂�ADeflection��B {0}�̎��͂ɔ�����Ԃ����������B\r\n";
                    case 61: // Deflection 2
                        return this.name + "�F������A�͂˕Ԃ��̂�I {0} �_���[�W�� {1} �ɔ��˂����I\r\n";
                    case 62: // Deflection 3
                        return this.name + "�F������A�͂˕Ԃ��̂�I �y�������A���͂ȈЗ͂ł͂˕Ԃ��Ȃ��I�z" + this.name + "�F�L���A�A�b�I�I\r\n";
                    case 63: // Truth Vision
                        return this.name + "�F�{������������Ε|���Ȃ���ATruthVision��B�@" + this.name + "�͑Ώۂ̃p�����^�t�o�𖳎�����悤�ɂȂ����B\r\n";
                    case 64: // Stance Of Flow
                        return this.name + "�F���K���A�K������Ă݂����AStanceOfFlow��B�@" + this.name + "�͎��R�^�[���A�K����U�����悤�ɍ\�����B\r\n";
                    case 65: // Carnage Rush 1
                        return this.name + "�F���̘A���R���{�A�ς����邩����HCarnageRush��B �b�n�C�I {0}�_���[�W�E�E�E   ";
                    case 66: // Carnage Rush 2
                        return "�b�Z�C�I {0}�_���[�W   ";
                    case 67: // Carnage Rush 3
                        return "���@�@�I {0}�_���[�W   ";
                    case 68: // Carnage Rush 4
                        return "�b�t�I {0}�_���[�W   ";
                    case 69: // Carnage Rush 5
                        return "�n�A�A�@�@�@�I�I {0}�̃_���[�W\r\n";
                    case 70: // Crushing Blow
                        return this.name + "�F���̈ꌂ�œ������~�߂Č������BCrushingBlow��B  {0} �� {1} �̃_���[�W�B\r\n";
                    case 71: // ���[�x�X�g�����N�|�[�V�����퓬�g�p��
                        return this.name + "�F�b�t�t�A����ł��H�炢�Ȃ����B\r\n";
                    case 72: // Enigma Sence
                        return this.name + "�F�͂̌��͐l�ɂ���ĈႤ�̂�AEnigmaSence�I\r\n";
                    case 73: // Soul Infinity
                        return this.name + "�F���̔\�͂�S�Ďg���Ē@�����ނ�B�b�n�A�A�@�@�@�E�E�ESoulInfinity�I�I\r\n";
                    case 74: // Kinetic Smash
                        return this.name + "�F���̌��A�ő���̉^�����������o���Ă݂����AKineticSmash��I\r\n";
                    case 75: // Silence Shot (Altomo��p)
                        return "";
                    case 76: // Silence Shot�AAbsoluteZero���قɂ��r�����s
                        return this.name + "�F�E�E�E�b�_���I�r���ł��Ȃ�������I�I\r\n";
                    case 77: // Cleansing
                        return this.name + "�F�����̏��_�ɂ��򉻁ACleansing��B\r\n";
                    case 78: // Pure Purification
                        return this.name + "�F���_���݂���̏򉻁APurePurification�E�E�E\r\n";
                    case 79: // Void Extraction
                        return this.name + "�F�ő�̔\�́A�ő���Ɉ����o����AVoidExtraction��B" + this.name + "�� {0} �� {1}�t�o�I\r\n";
                    case 80: // �A�J�V�W�A�̎��g�p��
                        return this.name + "�F������A����ŋC�t���ɂȂ���B\r\n";
                    case 81: // Absolute Zero
                        return this.name + "�F�X�̏��_����Η�x���󂯂Ȃ����BAbsoluteZero�I �X������ʂɂ��A���C�t�񕜕s�A�X�y���r���s�A�X�L���g�p�s�A�h��s�ƂȂ����B\r\n";
                    case 82: // BUFFUP���ʂ��]�߂Ȃ��ꍇ
                        return "�������A���ɂ��̃p�����^�͏㏸���Ă��邽�߁A���ʂ��Ȃ������B\r\n";
                    case 83: // Promised Knowledge
                        return this.name + "�F��̏��_��A�����Ȃ�m�b���������܂��APromiesdKnowledge��B�@�m�p�����^�� {0} �t�o\r\n";
                    case 84: // Tranquility
                        return this.name + "�F���̌��ʂ����A�����Ă��܂���������I Tranquility��I\r\n";
                    case 85: // High Emotionality 1
                        return this.name + "�F���̐��ݔ\�͂͂���Ȃ��̂���Ȃ���AHighEmotionality��I\r\n";
                    case 86: // High Emotionality 2
                        return this.name + "�̗́A�Z�A�m�A�S�p�����^���t�o�����I\r\n";
                    case 87: // AbsoluteZero�ŃX�L���g�p���s
                        return this.name + "�F�S�R�E�E�E�����Ȃ��E�E�E�A�X�L���g�p�ł��Ȃ���B\r\n";
                    case 88: // AbsoluteZero�ɂ��h�䎸�s
                        return this.name + "�F�h��́E�E�E�\�������Ȃ���I \r\n";
                    case 89: // Silent Rush 1
                        return this.name + "�F�������ŏ����ɗ}�����A���U���ASilentRush��B�@�b�n�C�I {0}�_���[�W�E�E�E�@";
                    case 90: // Silent Rush 2
                        return "�b�Z�C�I {0}�_���[�W   ";
                    case 91: // Silent Rush 3
                        return "���A�@�@�I {0}�̃_���[�W\r\n";
                    case 92: // BUFFUP�ȊO�̉i�����ʂ����ɂ��Ă���ꍇ
                        return "�������A���ɂ��̌��ʂ͕t�^����Ă���B\r\n";
                    case 93: // Anti Stun
                        return this.name + "�F���̍\���ŃX�^����h����AAntiStun��B " + this.name + "�̓X�^�����ʂւ̑ϐ����t����\r\n";
                    case 94: // Anti Stun�ɂ��X�^�����
                        return this.name + "�F�����A�ɂ���ˁB�ł��X�^���͔�������B\r\n";
                    case 95: // Stance Of Death
                        return this.name + "�F�����ȒP�ɂ���Ȃ����AStanceOfDeath�I " + this.name + "�͒v�����P�x����ł���悤�ɂȂ���\r\n";
                    case 96: // Oboro Impact 1
                        return this.name + "�F�O�̗e�A�y���ɉ��`�zOboro Impact��I\r\n";
                    case 97: // Oboro Impact 2
                        return this.name + "�F�b�n�A�@�@�E�E�E�b�Z�C�I�I�@ {0}��{1}�̃_���[�W\r\n";
                    case 98: // Catastrophe 1
                        return this.name + "�F�g�̂̍����ւƓ`���E�E�E�y���ɉ��`�zCatastrophe��I\r\n";
                    case 99: // Catastrophe 2
                        return this.name + "�F�b�Z�G�G�G�G�F�I�I�@ {0}�̃_���[�W\r\n";
                    case 100: // Stance Of Eyes
                        return this.name + "�F�s���ɂ͕K�����[�V����������͂��A StanceOfEyes��B " + this.name + "�́A����̍s���ɔ����Ă���E�E�E\r\n";
                    case 101: // Stance Of Eyes�ɂ��L�����Z����
                        return this.name + "�F������ˁI�I�@" + this.name + "�͑���̃��[�V���������؂��āA�s���L�����Z�������I\r\n";
                    case 102: // Stance Of Eyes�ɂ��L�����Z�����s��
                        return this.name + "�F�������E�E�E�������`�Ղ�������E�E�E�@" + this.name + "�͑���̃��[�V���������؂�Ȃ������I\r\n";
                    case 103: // Negate
                        return this.name + "�F�r���Ȃ񂩂����Ȃ���ANegate��B" + this.name + "�͑���̃X�y���r���ɔ����Ă���E�E�E\r\n";
                    case 104: // Negate�ɂ��X�y���r���L�����Z����
                        return this.name + "�F�r������Ƃ��A��������I" + this.name + "�͑���̃X�y���r����e�����I\r\n";
                    case 105: // Negate�ɂ��X�y���r���L�����Z�����s��
                        return this.name + "�F�������E�E�E�r���^�C�~���O���킩��Ȃ��E�E�E" + this.name + "�͑���̃X�y���r�������؂�Ȃ������I\r\n";
                    case 106: // Nothing Of Nothingness 1
                        return this.name + "�F���ꂪ�A���S�Ȃ�ے薂�@�y���ɉ��`�zNothingOfNothingness��I "+ this.name + "�ɖ��F�̃I�[�����h��n�߂�I \r\n";
                    case 107: // Nothing Of Nothingness 2
                        return this.name + "�͖��������@�𖳌����A�@����������X�L���𖳌�������悤�ɂȂ����I\r\n";
                    case 108: // Genesis
                        return this.name + "�F�S�Ă̍s����������x�ĂыN������AGenesis��B  " + this.name + "�͑O��̍s���������ւƓ��e�������I\r\n";
                    case 109: // Cleansing�r�����s��
                        return this.name + "�F�����E�E�E�ʖځA���q��������Cleansing�͏o�������ɂȂ���B\r\n";
                    case 110: // CounterAttack�𖳎�������
                        return this.Name + "�F�Â���ˁA���̍\���͊��ɂ����ʂ���B\r\n";
                    case 111: // �_�����g�p��
                        return this.name + "�F���C�t�E�X�L���E�}�i��30%���񕜂������B\r\n";
                    case 112: // �_�����A���Ɏg�p�ς݂̏ꍇ
                        return this.name + "�F�����͂�Ă��܂��Ă��B����P�񂾂��̂悤�ˁB\r\n";
                    case 113: // CounterAttack�ɂ�锽�����b�Z�[�W
                        return this.name + "�F���̓������������A�J�E���^�[��B\r\n";
                    case 114: // CounterAttack�ɑ΂��锽����NothingOfNothingness�ɂ���Ėh���ꂽ��
                        return this.name + "�F�ʖځE�E�E���؂�Ȃ���B\r\n";
                    case 115: // �ʏ�U���̃q�b�g
                        return this.name + "�̍U�����q�b�g�B {0} �� {1} �̃_���[�W\r\n";
                    case 116: // �h��𖳎����čU�����鎞
                        return this.name + "�F�h��̍\���͖��ʂ�I\r\n";
                    case 117: // StraightSmash�Ȃǂ̃X�L���N���e�B�J��
                        return this.name + "�F�N���e�B�J���q�b�g��I\r\n";
                    case 118: // �퓬���A�����@�C���|�[�V�����ɂ�镜���̂�����
                        return this.name + "�F���̃|�[�V�����ŕ�����B�b�n�C�I\r\n";
                    case 119: // Absolute Zero�ɂ�胉�C�t�񕜂ł��Ȃ��ꍇ
                        return this.name + "�F���C�t���E�E�E�񕜂ł��Ȃ���E�E�E\r\n";
                    case 120: // ���@�U���̃q�b�g
                        return "{0} �� {1} �̃_���[�W\r\n";
                    case 121: // Absolute Zero�ɂ��}�i�񕜂ł��Ȃ��ꍇ
                        return this.name + "�F�}�i���E�E�E�񕜂ł��Ȃ���E�E�E\r\n";
                    case 122: // �u���߂�v�s����
                        return this.name + "�F���́A�~�������Ă���������B\r\n";
                    case 123: // �u���߂�v�s���ŗ��܂肫���Ă���ꍇ
                        return this.name + "�F����ȏ�A���͂͒~�����Ȃ��݂����ˁB\r\n";
                    case 124: // StraightSmash�̃_���[�W�l
                        return this.name + "�̃X�g���[�g�E�X�}�b�V�����q�b�g�B {0} �� {1}�̃_���[�W\r\n";
                    case 125: // �A�C�e���g�p�Q�[�W�����܂��ĂȂ��Ƃ�
                        return this.name + "�F�A�C�e���Q�[�W�����܂��ĂȂ���ˁB�A�C�e���͂܂��g���Ȃ����B\r\n";
                    case 126: // FlashBlase
                        return this.name + "�F���Ȃ鉊�ŏĂ��Ă������AFlashBlaze��B {0} �� {1} �̃_���[�W\r\n";
                    case 127: // �������@�ŃC���X�^���g�s��
                        return this.name + "�F�����I�C���X�^���g�l����Ȃ�����Ȃ��I\r\n";
                    case 128: // �������@�̓C���X�^���g�^�C�~���O�ł��ĂȂ�
                        return this.name + "�F�C���X�^���g�^�C�~���O���ᔭ���ł��Ȃ���B\r\n";
                    case 129: // PsychicTrance
                        return this.name + "�F�����|�����ǁE�E�E���@�U���͋�����APsychicTrance�B\r\n";
                    case 130: // BlindJustice
                        return this.name + "�F���̖��@�댯�����ǁE�E�E�����U��������ABlindJustice�B\r\n";
                    case 131: // TranscendentWish
                        return this.name + "�F���肢�A�P����t�������Ă��傤�����ATranscendentWish��\r\n";
                    case 132: // LightDetonator
                        return this.name + "�F�����ˁA�t�B�[���h�W�J�I LightDetonator��\r\n";
                    case 133: // AscendantMeteor
                        return this.name + "�F�A�b�n�n�n�A���˂Ηǂ���B�@AscendantMeteor��\r\n";
                    case 134: // SkyShield
                        return this.name + "�F��Ɛ����������󂯂��ASkyShield��\r\n";
                    case 135: // SacredHeal
                        return this.name + "�F�S���񕜂������ASacredHeal\r\n";
                    case 136: // EverDroplet
                        return this.name + "�F����Ń}�i�͊��̐S�z�͕s�v�ˁAEverDroplet��\r\n";
                    case 137: // FrozenAura
                        return this.name + "�F�X������t�^�����AFrozenAura�B\r\n";
                    case 138: // ChillBurn
                        return this.name + "�F�������Ă��傤�����AChillBurn�B\r\n";
                    case 139: // ZetaExplosion
                        return this.name + "�F�Â̋֎�AZetaExplosion��I\r\n";
                    case 140: // FrozenAura�ǉ����ʃ_���[�W��
                        return this.name + "�F�����āI {0}�̒ǉ��_���[�W\r\n";
                    case 141: // StarLightning
                        return this.name + "�F���肢�B�C�₵�āAStarLightning��I\r\n";
                    case 142: // WordOfMalice
                        return this.name + "�F������݉������Ă������AWordOfMalice�B\r\n";
                    case 143: // BlackFire
                        return this.name + "�F���@�h���򉻂����Ă������ABlackFire�B\r\n";
                    case 144: // EnrageBlast
                        return this.name + "�F�ł��グ�ԉ΁�@EnrageBlast��@\r\n";
                    case 145: // Immolate
                        return this.name + "�F�����h���򉻂����Ă������AImmolate�B\r\n";
                    case 146: // VanishWave
                        return this.name + "�F�����ق��ĂĂ��傤�����AVanishWave�I\r\n";
                    case 147: // WordOfAttitude
                        return this.name + "�F�C���X�^���g�񕜂������AWordOfAttitude��B\r\n";
                    case 148: // HolyBreaker
                        return this.name + "�F�_���[�W�̈����������������AHolyBreaker�I\r\n";
                    case 149: // DarkenField
                        return this.name + "�F�h��͂�S�ʃ_�E�������Ă�����ADarkenField�I\r\n";
                    case 150: // SeventhMagic
                        return this.name + "�F���_�̔��]���s����ASeventhMagic�I\r\n";
                    case 151: // BlueBullet
                        return this.name + "�F�X���@��A�˂����ABlueBullet��B\r\n";
                    case 152: // NeutralSmash
                        return this.name + "�FNeutralSmash��A�n�C�I\r\n";
                    case 153: // SwiftStep
                        return this.name + "�F�퓬�̑��x���グ����ASwiftStep�B\r\n";
                    case 154: // CircleSlash
                        return this.name + "�F�݂�Ȏז���ACircleSlash�I\r\n";
                    case 155: // RumbleShout
                        return this.name + "�F�ǂ����Ă�̂�I�R�b�`��I\r\n";
                    case 156: // SmoothingMove
                        return this.name + "�F�����悤�ɍU�߂����ASmoothingMove�I\r\n";
                    case 157: // FutureVision
                        return this.name + "�F���̃^�[���A���������Ȃ����AFutureVision�I\r\n";
                    case 158: // ReflexSpirit
                        return this.name + "�F�X�^���n�͐�Ή�������AReflexSpirit��B\r\n";
                    case 159: // SharpGlare
                        return this.name + "�F�����ق��ĂĂ��傤�����ASharpGlare��B\r\n";
                    case 160: // TrustSilence
                        return this.name + "�F���ق�U�f�Ƃ��ʓ|�����ˁATrustSilence��B\r\n";
                    case 161: // SurpriseAttack
                        return this.name + "�F�S��������΂��Ă������ASurpriseAttack��I\r\n";
                    case 162: // PsychicWave
                        return this.name + "�F���̋Z�͎~�߂��邩����APsychicWave��B\r\n";
                    case 163: // Recover
                        return this.name + "�F�������肵�Ă�ˁARecover��B\r\n";
                    case 164: // ViolentSlash
                        return this.name + "�F�b�t�t�A������邩����AViolentSlash��I\r\n";
                    case 165: // OuterInspiration
                        return this.name + "�F�X�e�[�^�X�����ʂ�ɁAOuterInspiration�I\r\n";
                    case 166: // StanceOfSuddenness
                        return this.name + "�F�b�R�R�ˁIStanceOfSuddenness�I\r\n";
                    case 167: // �C���X�^���g�ΏۂŔ����s��
                        return this.name + "�F����̓C���X�^���g�Ώې�p��ˁB\r\n";
                    case 168: // StanceOfMystic
                        return this.name + "�F���Ă悤�Ƃ��Ă��A�������ʂ�BStanceOfMystic�I\r\n";
                    case 169: // HardestParry
                        return this.name + "�F���̍U���A�����Ă݂����BHardestParry��B\r\n";
                    case 170: // ConcussiveHit
                        return this.name + "�F�H�炢�Ȃ����AConcussiveHit�I\r\n";
                    case 171: // Onslaught hit
                        return this.name + "�F�H�炢�Ȃ����AOnslaughtHit�I\r\n";
                    case 172: // Impulse hit
                        return this.name + "�F�H�炢�Ȃ����AImpulseHit�I\r\n";
                    case 173: // Fatal Blow
                        return this.name + "�F����ŏI����AFatalBlow�I\r\n";
                    case 174: // Exalted Field
                        return this.name + "�F�^���̐����W����AExaltedField�I\r\n";
                    case 175: // Rising Aura
                        return this.name + "�F�����U���A�グ�čs�����BRisingAura�I\r\n";
                    case 176: // Ascension Aura
                        return this.name + "�F���@�U���A�グ�čs�����BAscensionAura�I\r\n";
                    case 177: // Angel Breath
                        return this.name + "�F�݂�Ȋ撣���āA���̏�ԂɁB�@AngelBreath\r\n";
                    case 178: // Blazing Field
                        return this.name + "�F���̉��ŔR�₵�Ă������ABlazingField�I\r\n";
                    case 179: // Deep Mirror
                        return this.name + "�F����͒ʂ��Ȃ����ADeepMirror�I\r\n";
                    case 180: // Abyss Eye
                        return this.name + "�F�[���̊Ⴉ�瓦����Ȃ����AAbyssEye�I\r\n";
                    case 181: // Doom Blade
                        return this.name + "�F���_�͂���Ƃ�����������ADoomBlade�I\r\n";
                    case 182: // Piercing Flame
                        return this.name + "�F�ђʂ̉΂�ł�����ł������APiercingFlame�I\r\n";
                    case 183: // Phantasmal Wind
                        return this.name + "�F�����͂��グ���APhantasmalWind��B\r\n";
                    case 184: // Paradox Image
                        return this.name + "�F���ݔ\�͂������o����AParadoxImage��B\r\n";
                    case 185: // Vortex Field
                        return this.name + "�F����ŊF��ݑ��ɂ����ˁAVortexField�B\r\n";
                    case 186: // Static Barrier
                        return this.name + "�F���Ɨ����������󂯂��AStaticBarrier��\r\n";
                    case 187: // Unknown Shock
                        return this.name + "�F�^���ÂȒ��Ő키�Ƃ�����AUnknownShock��\r\n";
                    case 188: // SoulExecution
                        return this.name + "�F�s�����A���`�@SoulExecusion�I\r\n";
                    case 189: // SoulExecution hit 01
                        return this.name + "�F�b�Z�B�I\r\n";
                    case 190: // SoulExecution hit 02
                        return this.name + "�F�b�n�I\r\n";
                    case 191: // SoulExecution hit 03
                        return this.name + "�F�b�n�C�I\r\n";
                    case 192: // SoulExecution hit 04
                        return this.name + "�F�b�t�I\r\n";
                    case 193: // SoulExecution hit 05
                        return this.name + "�F�b�Z�C�I\r\n";
                    case 194: // SoulExecution hit 06
                        return this.name + "�F�b�n�I\r\n";
                    case 195: // SoulExecution hit 07
                        return this.name + "�F�b�t�I\r\n";
                    case 196: // SoulExecution hit 08
                        return this.name + "�F�b�n�A�@�I\r\n";
                    case 197: // SoulExecution hit 09
                        return this.name + "�F�Z�G�F�F�B�I\r\n";
                    case 198: // SoulExecution hit 10
                        return this.name + "�F���߂��I�I�I\r\n";
                    case 199: // Nourish Sense
                        return this.name + "�F�񕜗ʂ������Ă�����ANourishSense��B\r\n";
                    case 200: // Mind Killing
                        return this.name + "�F���_��؂荏��ł������AMindKilling�I\r\n";
                    case 201: // Vigor Sense
                        return this.name + "�F�����l�グ����AVigorSense�I\r\n";
                    case 202: // ONE Authority
                        return this.name + "�F�݂�ȁA�グ�Ă������BOneAuthority�I\r\n";
                    case 203: // �W���ƒf��
                        return this.name + "�F�y�W���ƒf��z�@�����B\r\n";
                    case 204: // �y���j�z�����ς�
                        return this.name + "�F�y���j�z�͍��������g���Ă��܂��Ă��B\r\n";
                    case 205: // �y���j�z�ʏ�s���I����
                        return this.name + "�F�y���j�z�̓C���X�^���g�^�C�~���O�Ŏg�p������̂�B\r\n";
                    case 206: // Sigil Of Homura
                        return this.name + "�F���̈���󂯂Ȃ����ASigilOfHomura�I\r\n";
                    case 207: // Austerity Matrix
                        return this.name + "�F�x�z�͂�؂点�Ă��炤��AAusterityMatrix��B\r\n";
                    case 208: // Red Dragon Will
                        return this.name + "�F�Η���A���ɗ͂�^����ARedDragonWill�I\r\n";
                    case 209: // Blue Dragon Will
                        return this.name + "�F������A���ɗ͂�^����ABlueDragonWill�I\r\n";
                    case 210: // Eclipse End
                        return this.name + "�F�S�Ă𖕏����������������ɁAEclipseEnd�I\r\n";
                    case 211: // Sin Fortune
                        return this.name + "�F���̃q�b�g�Ō��߂Ă݂����ASinFortune��B\r\n";
                    case 212: // AfterReviveHalf
                        return this.name + "�F���ϐ��i�n�[�t�j��t�^�����ˁB\r\n";
                    case 213: // Demonic Ignite
                        return this.name + "�F�������瓦����Ȃ����ADemonicIgnite�I\r\n";
                    case 214: // Death Deny
                        return this.name + "�F���҂����S�ɕ�����������ADeathDeny�I\r\n";
                    case 215: // Stance of Double
                        return this.name + "�F���ɍs�������AStanceOfDouble�I  " + this.name + "�͑O��s�������������̕��g�𔭐��������I\r\n";
                    case 216: // �ŏI�탉�C�t�J�E���g����
                        return this.name + "�F�����E�E�E�܂���E�E�E�܂��|���킯�ɂ͂����Ȃ���I\r\n";
                    case 217: // �ŏI�탉�C�t�J�E���g���Ŏ�
                        return this.name + "�F�E�E�E���E�E�E���߂�E�E�E�ȁE�E���E�E�E\r\n";
                        
                    case 1001: // Home Town 1 �R�~���j�P�[�V�����ςŁA�x�ޑO�̃A�C����l��Ώ�
                        return this.name + "�F�����͂����x��ŁA�����ɔ�������H";
                    case 1002: // Home Town 2 �R�~���j�P�[�V�����ςŁA�x�񂾌�̃A�C����l��Ώ�
                        return this.name + "�F�z���z���A�Ƃ��Ƃƍs���ė�����";
                    case 1003: // Home Town 1 �R�~���j�P�[�V�����ςŁA�x�ޑO�̃A�C���E���i�Q�l��Ώ�
                        return this.name + "�F����A���͈�U�߂�Ƃ����ˁB�����ɔ����ċx�݂܂���B";
                    case 1004: // Home Town 2 �R�~���j�P�[�V�����ςŁA�x�񂾌�̃A�C���E���i�Q�l��Ώ�
                        return this.name + "�F�������o������A�Ƃ��Ƃƍs������";

                    case 2001: // �|�[�V�����񕜎�
                        return this.name + "�F{0} �񕜂������B";
                    case 2002: // ���x���A�b�v�I���Ñ�
                        return this.name + "�F���x���A�b�v����ˁB";
                    case 2003: // �ו������点��Ñ�
                        return this.name + "�F{0}�A�ו������炵�Ă�������H�A�C�e�����n���Ȃ����B";
                    case 2004: // �������f
                        return this.name + "�F����A�������悤���H";
                    case 2005: // ��������
                        return this.name + "�F����������";
                    case 2006: // �����̐������g�p
                        return this.name + "�F���ɖ߂�Ƃ��܂�����";
                    case 2007: // ���p��p�i�ɑ΂���ꌾ
                        return this.name + "�F���p��p�̕i���ˁB";
                    case 2008: // ��퓬���̃}�i�s��
                        return this.name + "�F�}�i���s�����Ă��ˁB";
                    case 2009: // �_�����g�p��
                        return this.name + "�F���C�t�E�X�L���E�}�i��30%���񕜂������B";
                    case 2010: // �_�����A���Ɏg�p�ς݂̏ꍇ
                        return this.name + "�F�����͂�Ă��܂��Ă��B����P�񂾂��̂悤�ˁB";
                    case 2011: // ���[�x�X�g�����N�|�[�V������퓬�g�p��
                        return this.name + "�F�퓬����p�i�̂悤�ˁB";
                    case 2012: // �����̐������g�p�i���؍ݎ��j
                        return this.name + "�F���͒��̒��ɂ��邩��g���Ă��Ӗ��Ȃ����B";
                    case 2013: // �����̐������̂Ă��Ȃ����̑䎌
                        return this.name + "�F������̂Ă��玄�B�A�����ڂɉ���B";
                    case 2014: // ��퓬���̃X�L���s��
                        return this.name + "�F�X�L�����s�����Ă��ˁB";
                    case 2015: // ���v���C���[���̂ĂĂ��܂����Ƃ����ꍇ
                        return this.name + "�F���A������ƁA����͎̂ĂȂ��ł�B";
                    case 2016: // �����@�C���|�[�V�����ɂ�镜��
                        return this.name + "�F�悵�A�����ł�����B�z���g���肪�Ɓ�";
                    case 2017: // �����@�C���|�[�V�����s�v�Ȏ�
                        return this.name + "�F{0}�͂܂�����ł͂��Ȃ���B�g�p����K�v�͂Ȃ������ˁB";
                    case 2018: // �����@�C���|�[�V�����Ώۂ������̎�
                        return this.name + "�F���͂܂������Ă���B����ˁB";
                    case 2019: // �����s�̃A�C�e���𑕔����悤�Ƃ�����
                        return this.name + "�F�����ᑕ���ł��Ȃ��݂����ˁB";
                    case 2020: // ���X�{�X���j�̌�A�����̐������g�p�s��
                        return this.name + "�F���߂�Ȃ����A���͂�������͎g��Ȃ����肾����B";
                    case 2021: // �A�C�e���̂Ă�̍Ñ�
                        return this.name + "�F�o�b�N�p�b�N�̐��������ˁB";
                    case 2022: // �I�[�o�[�V�t�e�B���O�g�p�J�n��
                        return this.name + "�F������E�E�E�g�̔\�͂��č\�z����Ă����̂��������B";
                    case 2023: // �I�[�o�[�V�t�e�B���O�ɂ��p�����^����U�莞
                        return this.name + "�F�I�[�o�[�V�t�e�B���O�̊���U����ɂ��܂���B";
                    case 2024: // �������L�b�h���g�p������
                        return this.name + "�F�y{0}�z�p�����^�� {1} �㏸�������";
                    case 2025:
                        return this.name + "�F���͗��蕐��𑕔����Ă����B�T�u�͑����ł��Ȃ���ˁB";
                    case 2026:
                        return this.name + "�F����i���C���j�ɉ����������Ă���ɂ��܂���B";
                    case 2027: // �������g�p��
                        return this.name + "�F���C�t��100%�񕜂������B";
                    case 2028: // �������A���Ɏg�p�ς݂̏ꍇ
                        return this.name + "�F�����͂�Ă��܂��Ă��B����P�񂾂��̂悤�ˁB";
                    case 2029: // �ו���t�ő������O���Ȃ���
                        return this.name + "�F�o�b�N�p�b�N�������ς��݂�������B�����͊O���Ȃ���ˁB";
                    case 2030: // �ו���t�ŉ������̂Ă鎞�̑䎌
                        return this.name + "�F{0}���̂Ă��ˁH";
                    case 2031: // �퓬���̃A�C�e���g�p����
                        return this.name + "�F���͐퓬����B���̃p�����^�Ȃ񂩒T���Ă�]�T�͖�����B";
                    case 2032: // �퓬���A�A�C�e���g�p�ł��Ȃ��A�C�e����I�������Ƃ�
                        return this.name + "�F���̃A�C�e���͐퓬���Ɏg�p�͏o���Ȃ��݂����ˁB";
                    case 2033: // �a�����Ȃ��ꍇ
                        return this.name + "�F���`��A������Ƃ���͎茳����O���Ȃ���ˁB";
                    case 2034: // �A�C�e�������ς��ŗa���菊��������o���Ȃ��ꍇ
                        return this.name + "�F���`��A�ו��͂��������ς��݂����ˁB";
                    case 2035: // Sacred Heal
                        return this.name + "�F����A�S���񕜂������B";

                    case 3000: // �X�ɓ��������̑䎌
                        return this.name + "�F�z���b�g�N�����Ȃ���ˁB";
                    case 3001: // �x�����v����
                        return this.name + "�F���A����{0}���~������B {1}Gold�u���Ă����H";
                    case 3002: // �����������ς��Ŕ����Ȃ���
                        return this.name + "�F�������������ς��̂悤�ˁB�����A�C�e�����������B";
                    case 3003: // �w��������
                        return this.name + "�F����Ŕ����������Ă���ˁH�K���c�f������B";
                    case 3004: // Gold�s���ōw���ł��Ȃ��ꍇ
                        return this.name + "�FGold��{0}����Ȃ���ˁE�E�E";
                    case 3005: // �w�������L�����Z�������ꍇ
                        return this.name + "�F���̃A�C�e�������ĉ��܂���B";
                    case 3006: // ����Ȃ��A�C�e���𔄂낤�Ƃ����ꍇ
                        return this.name + "�F���̃A�C�e���͎�����Ȃ���ˁB";
                    case 3007: // �A�C�e�����p��
                        return this.name + "�F{0}��u���Ă����A{1}Gold������ėǂ��͂���ˁH";
                    case 3008: // ����̓y���_���g���p��
                        return this.name + "�F������ƃR����������������������̂�B�܂��A{0}Gold���炢�͂��炦���������ǁH";
                    case 3009: // ����X���o�鎞
                        return this.name + "�F�K���c�f������A����̃��[�c������Ƃ�����ˁB";
                    case 3010: // �K���c�s�ݎ��̔��肫��t�F���g�D�[�V�������Ĉꌾ
                        return this.name + "�F���̌��͌��X����؂���Ęb����Ȃ����ˁE�E�E";
                    case 3011: // �����\�Ȃ��̂��w�����ꂽ��
                        return this.name + "�F�����ő������Ă������B�ǂ����ˁH";
                    case 3012: // �������Ă������𔄋p�Ώۂ��ǂ���������
                        return this.name + "�F���ƂƁA���������Ă�{0}�������Ă���������ˁB{1}Gold���炢��ˁA�K���c�f������H";

                    case 3013: // �X�̒S���҂Ƃ��Ă��}���̈��A
                        return this.name + "�F�ǂ������������߂���������";
                    case 3014: // �X�̒S���҂Ƃ��Ă��ʂ�̈��A
                        return this.name + "�F�܂��A�����ł��������܂���";
                    case 3015: // �X�̒S���҂Ƃ��Ă������グ���q�A�����O����Ƃ�
                        return this.name + "�F{0}�ł��ˁB{1}Gold�ł����A�������グ�ɂȂ�܂����H";
                    case 3016: // �X�̒S���҂Ƃ��āAGold���s�����Ă�Ƃ��̑䎌
                        return this.name + "�F�����܂��񂪁A��{0}Gold�����s�����Ă���܂��B";
                    case 3017: // �X�̒S���҂Ƃ��āA�����肪�w������E���������Ƃ�
                        return this.name + "�F���肪�Ƃ��������܂�����";
                    case 3018: // ������̎������������ς��ł��鎖�����`������Ƃ�
                        return this.name + "�F���́A�����܂��񂪉ו��������ς��̂悤�ł��B";
                    case 3019: // �����肪�w�������L�����Z�����ꂽ�ꍇ
                        return this.name + "�F���ɂ��ǂ���΁A���čs���Ă��������܂���";
                    case 3020: // �����肪���p�s�\�Ȃ��̂�񎦂��Ă����ꍇ
                        return this.name + "�F�����܂��񂪁A���̕i���͔���肪�ł��܂���B";
                    case 3021: // �����肪���p�\�Ȃ��̂�񎦂��Ă����ꍇ
                        return this.name + "�F{0}�ł��ˁB{1}Gold�ł̔���点�Ă��������܂��傤���H";

                    case 4001: // �ʏ�U����I��
                        return this.name + "�F���ʂɍU���ˁB\r\n";
                    case 4002: // �h���I��
                        return this.name + "�F��Ȃ��Ƃ��͖h�䂩�ȁB\r\n";
                    case 4003: // �ҋ@��I��
                        return this.name + "�F�ҋ@�Ŏ��ɔ�������B\r\n";
                    case 4004: // �t���b�V���q�[����I��
                        return this.name + "�F�t���b�V���q�[���ōs����������B\r\n";
                    case 4005: // �v���e�N�V������I��
                        return this.name + "�F�h��͂t�o�A�v���e�N�V������B\r\n";
                    case 4006: // �t�@�C�A�E�{�[����I��
                        return this.name + "�F�t�@�C�A�{�[�����������ނ��B\r\n";
                    case 4007: // �t���C���E�I�[����I��
                        return this.name + "�F�t���C�������U���̏����ˁB\r\n";
                    case 4008: // �X�g���[�g�E�X�}�b�V����I��
                        return this.name + "�F���A�X�g���[�g�X�}�b�V���s�����B\r\n";
                    case 4009: // �_�u���E�X�}�b�V����I��
                        return this.name + "�F�Q��U���A�_�u���X�}�b�V���s�����B\r\n";
                    case 4010: // �X�^���X�E�I�u�E�X�^���f�B���O��I��
                        return this.name + "�F�X�^���f�B���O�̍\���B����čU�߂���B\r\n";
                    case 4011: // �A�C�X�E�j�[�h����I��
                        return this.name + "�F�A�C�X�j�[�h�����������ނ��B\r\n";
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
                        return this.name + "�F" + this.ActionLabel.Text + "�ɂ����ˁB\r\n";
                    case 4072:
                        return this.name + "�F�G�ɕ��������Ȃ���ˁB\r\n";
                    case 4073:
                        return this.name + "�F�G�ɕ��������Ȃ���ˁB\r\n";
                    case 4074:
                        return this.name + "�F�����ɕ��킯�ɂ͍s���Ȃ���B\r\n";
                    case 4075:
                        return this.name + "�F�����ɕ��킯�ɂ͍s���Ȃ���B\r\n";
                    case 4076:
                        return this.name + "�F�����ɍU������킯�ɂ͍s���Ȃ���B\r\n";
                    case 4077: // �u���߂�v�R�}���h
                        return this.name + "�F���͂����߂��B\r\n";
                    case 4078: // ���픭���u���C���v
                        return this.name + "�F���C������̌��ʂ𔭓��������ˁB\r\n";
                    case 4079: // ���픭���u�T�u�v
                        return this.name + "�F�T�u����̌��ʂ𔭓��������ˁB\r\n";
                    case 4080: // �A�N�Z�T���P����
                        return this.name + "�F�A�N�Z�T���P�̌��ʂ𔭓��������ˁB\r\n";
                    case 4081: // �A�N�Z�T���Q����
                        return this.name + "�F�A�N�Z�T���Q�̌��ʂ𔭓��������ˁB\r\n";

                    case 4082: // FlashBlaze
                        return this.name + "�F�t���b�V���u���C�Y�ōs����������B\r\n";

                    // ����U��
                    case 5001:
                        return this.name + "�F���Ő؂�􂭂��A�G�A���E�X���b�V���I {0} �� {1} �̃_���[�W\r\n";
                    case 5002:
                        return this.name + "�F{0} �񕜂� \r\n";
                    case 5003:
                        return this.name + "�F{0} �}�i�񕜂� \r\n";
                    case 5004:
                        return this.name + "�F��������ǂ���I�A�C�V�N���E�X���b�V���I {0} �� {1} �̃_���[�W\r\n";
                    case 5005:
                        return this.name + "�F�s�����I�G���N�g���E�u���[�I {0} �� {1} �̃_���[�W\r\n";
                    case 5006:
                        return this.name + "�F�������I�u���[�E���C�g�j���O�I {0} �� {1} �̃_���[�W\r\n";
                    case 5007:
                        return this.name + "�F�������I�o�[�j���O�E�N���C���A�I {0} �� {1} �̃_���[�W\r\n";
                    case 5008:
                        return this.name + "�F�ԑ�������̉��A�H�炤�킪������I {0} �� {1} �̃_���[�W\r\n";
                    case 5009:
                        return this.name + "�F{0} �X�L���|�C���g�񕜂� \r\n";
                    case 5010:
                        return this.name + "���������Ă���w�ւ�����ɑ�������������I {0} �� {1} �̃_���[�W\r\n";
                }
            }
            #endregion
            #region "���F���["
            else if (this.name == "���F���[" || this.name == "���F���[�E�A�[�e�B")
            {
                switch (sentenceNumber)
                {
                    case 0: // �X�L���s��
                        return this.name + "�F�X�L���|�C���g������܂���ˁB\r\n";
                    case 1: // Straight Smash
                        return this.name + "�F�b�n�A�A�@�@�@�@�@�I�I�I\r\n";
                    case 2: // Double Slash 1 Carnage Rush 1
                        return this.name + "�F�ЂƂ�\r\n";
                    case 3: // Double Slash 2 Carnage Rush 2
                        return this.name + "�F�ӂ���\r\n";
                    case 4: // Painful Insanity
                        return this.name + "�F�y�S�ቜ�`�z���Ȃ��ɂ͕�����Ȃ��ł��傤�A�����̋ꂵ�݁B\r\n";
                    case 5: // empty skill
                        return this.name + "�F�X�L���I���~�X�ł��ˁB\r\n";
                    case 6: // ���݂��t�����V�X�̕K�E��H�������
                        return this.name + "�F���āA����́E�E�E�Ȃ��Ȃ��B\r\n";
                    case 7: // Lizenos�̕K�E��H�������
                        return this.name + ": ���̃{�N�ł����E�E�E�܂����������܂���E�E�E\r\n";
                    case 8: // Minflore�̕K�E��H�������
                        return this.name + "�F���E�E�E���̌��A�������E�E��E�E�E\r\n";
                    case 9: // Fresh Heal�ɂ�郉�C�t��
                        return this.name + "�F{0} �񕜂ł��B\r\n";
                    case 10: // Fire Ball
                        return this.name + "�F�y���ǂ��ł��傤�AFireBall�ł��B {0} �� {1} �̃_���[�W\r\n";
                    case 11: // Flame Strike
                        return this.name + "�F����ȐԂ����̂͂������ł��傤�AFlameStrike�ł��B {0} �� {1} �̃_���[�W\r\n";
                    case 12: // Volcanic Wave
                        return this.name + "�F�Ɖ΂��炢�債�����Ȃ��ł��傤�H VolcanicWave�ł��B {0} �� {1} �̃_���[�W\r\n";
                    case 13: // �ʏ�U���N���e�B�J���q�b�g
                        return this.name + "�F�N���e�B�J���q�b�g�ł��B�b�n�A�A�@�@�@�I�I {0} �� {1}�̃_���[�W\r\n";
                    case 14: // FlameAura�ɂ��ǉ��U��
                        return this.name + "�F�΂̕��ł��B {0}�̒ǉ��_���[�W\r\n";
                    case 15: // �����̐�����퓬���Ɏg�����Ƃ�
                        return this.name + "�F����͐퓬���ł͎g���܂���ˁB\r\n";
                    case 16: // ���ʂ𔭊����Ȃ��A�C�e����퓬���Ɏg�����Ƃ�
                        return this.name + "�F�b�`�A�𗧂����A�C�e�����B�@����Ȃ��̎g���˂��I�I\r\n";
                    case 17: // ���@�Ń}�i�s��
                        return this.name + "�F�}�i���s�����Ă��܂��ˁB\r\n";
                    case 18: // Protection
                        return this.name + "�F���̖h��~�AProtection�ł��B�����h��́F�t�o\r\n";
                    case 19: // Absorb Water
                        return this.name + "�F���̖h��~�AAbsorbWater�ł��B ���@�h��́F�t�o�B\r\n";
                    case 20: // Saint Power
                        return this.name + "�F���̍U���~�ASaintPower�ł��B �����U���́F�t�o\r\n";
                    case 21: // Shadow Pact
                        return this.name + "�F�N�N�b�A�ł���̗́AShadowPact�ł��B ���@�U���́F�t�o\r\n";
                    case 22: // Bloody Vengeance
                        return this.name + "�F�ł̎g�҂��{�N�ɗ͂������BBloodyVengeance�ł��B �̓p�����^�� {0} �t�o\r\n";
                    case 23: // Holy Shock
                        return this.name + "�F�S�ƂȂ�Ă������ł����AHolyShock�ł��B {0} �� {1} �̃_���[�W\r\n";
                    case 24: // Glory
                        return this.name + "�F�N�ł����P�����オ��������ł���BGlory�ł��B ���ڍU���{FreshHeal�A�g�̃I�[��\r\n";
                    case 25: // CelestialNova 1
                        return this.name + "�F���ɂ������ACelestialNova�ł��B {0} �񕜂ł��B\r\n";
                    case 26: // CelestialNova 2
                        return this.name + "�F�b�n�b�n�n�n�I�ق��H�炦�₠�������I�@CelestialNova�I�I {0} �̃_���[�W\r\n";
                    case 27: // Dark Blast
                        return this.name + "�F�N�N�N�A�ł̔g�������S�āADarkBlast�ł��B {0} �� {1} �̃_���[�W\r\n";
                    case 28: // Lava Annihilation
                        return this.name + "�F�����V�g���炢����������ALavaAnnihilation�ł��B {0} �� {1} �̃_���[�W\r\n";
                    case 10028: // Lava Annihilation���
                        return this.name + "�F�����V�g���炢����������ALavaAnnihilation�ł��B\r\n";
                    case 29: // Devouring Plague
                        return this.name + "�F�̗͋z�����A���ˎ��ˎ��˂��IDevouringPlague�I {0} ���C�t���z�������\r\n";
                    case 30: // Ice Needle
                        return this.name + "�F�����̕X�ł��H����Ă��������AIceNeedle�ł��B {0} �� {1} �̃_���[�W\r\n";
                    case 31: // Frozen Lance
                        return this.name + "�F�ǂ��ł��A�X�̑��ŋ��h�����Ă̂́H�@FrozenLance�ł��B {0} �� {1} �̃_���[�W\r\n";
                    case 32: // Tidal Wave
                        return this.name + "�F���ɓۂ܂�Ď��񂶂܂��您�������I�I�ITidalWave�I {0} �̃_���[�W\r\n";
                    case 33: // Word of Power
                        return this.name + "�F���Ȃ����̖��@�~�߂�܂����AWordOfPower�ł��B {0} �� {1} �̃_���[�W\r\n";
                    case 34: // White Out
                        return this.name + "�F�܁E�E�E�܊��������������������I�@WhiteOut�I�I�I {0} �� {1} �̃_���[�W\r\n";
                    case 35: // Black Contract
                        return this.name + "�F�����A�Ȃ�Ĕ������������E�E�E�ABlackContract�ł��B " + this.name + "�̃X�L���A���@�R�X�g�͂O�ɂȂ�B\r\n";
                    case 36: // Flame Aura�r��
                        return this.name + "�F�����Ŋۏł��ɂ��Ă�邺���������I�IFlameAura�I�I�I ���ڍU���ɉ��̒ǉ����ʂ��t�^�����B\r\n";
                    case 37: // Damnation
                        return this.name + "�F�ňňňňňňňňŁE�E�E��]�E�E�EDamnation�B �������o�ł鍕����Ԃ�c�܂���B\r\n";
                    case 38: // Heat Boost
                        return this.name + "�F�����V�g�̉����~�AHeatBoost�ł��B �Z�p�����^�� {0} �t�o�B\r\n";
                    case 39: // Immortal Rave
                        return this.name + "�F���p�̕��Ȃ�āA�ȒP������E�E�EImmortalRave�ł��B " + this.name + "�̎���ɂR�̉����h�����B\r\n";
                    case 40: // Gale Wind
                        return this.name + "�F�S�����{�N�͏�ɂ��̏�Ԃ������AGaleWind�ł��B ������l��" + this.name + "�����ꂽ�B\r\n";
                    case 41: // Word of Life
                        return this.name + "�F�b�N�N�A���R�̗͂����������������IWordOfLife�I�I�I �厩�R����̋�����������������悤�ɂȂ����B\r\n";
                    case 42: // Word of Fortune
                        return this.name + "�F�S�����{�N�͂���Ȗ��@�s�v�ł����AWordOfFortune�ł��B �����̃I�[�����N���オ�����B\r\n";
                    case 43: // Aether Drive
                        return this.name + "�F��z�����Ȃ�đ債�����̂��Ⴀ��܂����HAetherDrive�ł��B ���͑S�̂ɋ�z�����͂��݂Ȃ���B\r\n";
                    case 44: // Eternal Presence 1
                        return this.name + "�F�n���ƌ����Ȃ�đ傰���߂���A���ʂł���BEternalPresence�B " + this.name + "�̎���ɐV�����@�����\�z�����B\r\n";
                    case 45: // Eternal Presence 2
                        return this.name + "�̕����U���A�����h��A���@�U���A���@�h�䂪�t�o�����I\r\n";
                    case 46: // One Immunity
                        return this.name + "�F�Q��s���ł����Ƃ�����ŋ����Ǝv���܂���H OneImmunity�ł��B " + this.name + "�̎��͂ɖڂɌ����Ȃ���ǂ������B\r\n";
                    case 47: // Time Stop
                        return this.name + "�F�A�b�n�n�n�n�n�I����̘c�ɏ������܂��Ȃ��ITimeStop�I�I�I �G�̎���������􂫎��Ԓ�~�������B\r\n";
                    case 48: // Dispel Magic
                        return this.name + "�F���X�̓J���b�|�̂����ɁA�b�N�N�N�BDispelMagic�ł��B\r\n";
                    case 49: // Rise of Image
                        return this.name + "�F����̎x�z�҂̏㏸�~�ARiseOfImage�ł��B �S�p�����^�� {0} �t�o\r\n";
                    case 50: // ��r��
                        return this.name + "�F��r���ł��ˁB\r\n";
                    case 51: // Inner Inspiration
                        return this.name + "�F�͐��ݏW���͂����߂��B���_���������܂����B {0} �X�L���|�C���g��\r\n";
                    case 52: // Resurrection 1
                        return this.name + "�F��ՁH���̂��炢�̐��X�y�����ʂł��傤�AResurrection�ł��B\r\n";
                    case 53: // Resurrection 2
                        return this.name + "�F�Ă߂��Ȃ񂩑Ώۂɂ���킯�˂����남�����I�I\r\n";
                    case 54: // Resurrection 3
                        return this.name + "�F�����Ă܂����ˁB�����܂���ł����B\r\n";
                    case 55: // Resurrection 4
                        return this.name + "�F�{�N���g�͐����Ă�̂��H�Ƃ����u���b�N�W���[�N�ł��A�Ӗ��͂���܂���B\r\n";
                    case 56: // Stance Of Standing
                        return this.name + "�F�h�䌓�U���Ƃ́A��������ł���B\r\n";
                    case 57: // Mirror Image
                        return this.name + "�F���_��葓�̖��@���ˉ~�AMirrorImage�ł��B{0}�̎��͂ɐ���Ԃ����������B\r\n";
                    case 58: // Mirror Image 2
                        return this.name + "�F�b�N�N�A�|�������ȁI {0} �_���[�W�� {1} �ɔ��˂����I\r\n";
                    case 59: // Mirror Image 3
                        return this.name + "�F�b�N�N�A�|�������ȁI �y�������A���͂ȈЗ͂ł͂˕Ԃ��Ȃ��I�z" + this.name + "�F���A���܂����{�N�Ƃ������������I\r\n";
                    case 60: // Deflection
                        return this.name + "�F���̎҂�蕨�����ˉ~�ADeflection�ł��B {0}�̎��͂ɔ�����Ԃ����������B\r\n";
                    case 61: // Deflection 2
                        return this.name + "�F�b�N�N�A�|�������ȁI {0} �_���[�W�� {1} �ɔ��˂����I\r\n";
                    case 62: // Deflection 3
                        return this.name + "�F�b�N�N�A�|�������ȁI �y�������A���͂ȈЗ͂ł͂˕Ԃ��Ȃ��I�z" + this.name + "�F���܂����A�{�N�Ƃ������Ƃ������I\r\n";
                    case 63: // Truth Vision
                        return this.name + "�F�{���H����킯�Ȃ��ł��傤�ATruthVision�ł��B�@" + this.name + "�͑Ώۂ̃p�����^�t�o�𖳎�����悤�ɂȂ����B\r\n";
                    case 64: // Stance Of Flow
                        return this.name + "�F����̓����A���E���Ă݂��܂��傤�BStanceOfFlow�ł��B�@" + this.name + "�͎��R�^�[���A�K����U�����悤�ɍ\�����B\r\n";
                    case 65: // Carnage Rush 1
                        return this.name + "�F�Z������Ηe�Ղ��R���{�ł��傤�ACarnageRush�ł��B �ЂƂ� {0}�_���[�W�E�E�E   ";
                    case 66: // Carnage Rush 2
                        return "�ӂ��� {0}�_���[�W   ";
                    case 67: // Carnage Rush 3
                        return "�݂��� {0}�_���[�W   ";
                    case 68: // Carnage Rush 4
                        return "����� {0}�_���[�W   ";
                    case 69: // Carnage Rush 5
                        return "�����čŌ�ł��B�n�A�A�@�@�@�I�I {0}�̃_���[�W\r\n";
                    case 70: // Crushing Blow
                        return this.name + "�F�����Q�ĂĂ��������BCrushingBlow�ł��B  {0} �� {1} �̃_���[�W�B\r\n";
                    case 71: // ���[�x�X�g�����N�|�[�V�����퓬�g�p��
                        return this.name + "�F����ȃA�C�e���������ł���A�������ł��傤�B\r\n";
                    case 72: // Enigma Sence
                        return this.name + "�F�͂͌��������悾�Ǝv���܂��񂩁HEnigmaSennce�ł�\r\n";
                    case 73: // Soul Infinity
                        return this.name + "�F���ꂪ�{�N�̑S�Ă̔\�͂𒍂����񂾃p���[�ł��BSoulInfinity�I\r\n";
                    case 74: // Kinetic Smash
                        return this.name + "�F�����^���ɉ������ő���̍U���Ƃ͂����ł����ˁBKineticSmash�ł��B\r\n";
                    case 75: // Silence Shot (Altomo��p)
                        return "";
                    case 76: // Silence Shot�AAbsoluteZero���قɂ��r�����s
                        return this.name + "�F�E�E�E�����̃^�C�~���O�ŁE�E�E�I�H�@�r���~�X�ł��B\r\n";
                    case 77: // Cleansing
                        return this.name + "�F�򉻂Ƃ������A���X����������ł���BCleansing�ł��B\r\n";
                    case 78: // Pure Purification
                        return this.name + "�F���_�̐􂢒������玡���鎖�������ł��APurePurification�E�E�E\r\n";
                    case 79: // Void Extraction
                        return this.name + "�F�{�N�Ɍ��E�Ȃ�Ė�����ł���AVoidExtraction�ł��B" + this.name + "�� {0} �� {1}�t�o�I\r\n";
                    case 80: // �A�J�V�W�A�̎��g�p��
                        return this.name + "�F�A�J�V�W�A�̎��ł��B�ǂ��C�t���ɂȂ�ł��傤�B\r\n";
                    case 81: // Absolute Zero
                        return this.name + "�F��Η�x�œ���t�����������������BAbsoluteZero�I �X������ʂɂ��A���C�t�񕜕s�A�X�y���r���s�A�X�L���g�p�s�A�h��s�ƂȂ����B\r\n";
                    case 82: // BUFFUP���ʂ��]�߂Ȃ��ꍇ
                        return "�������A���ɂ��̃p�����^�͏㏸���Ă��邽�߁A���ʂ��Ȃ������B\r\n";
                    case 83: // Promised Knowledge
                        return this.name + "�F�m�b�ƒm���̑g�ݍ��킹�ŋ����Ǝv���܂���H PromiesdKnowledge�ł��B�@�m�p�����^�� {0} �t�o\r\n";
                    case 84: // Tranquility
                        return this.name + "�F�����Ɩ��A�ǂ���������Ȃ�ł��傤�ˁATranquility�ł��B\r\n";
                    case 85: // High Emotionality 1
                        return this.name + "�F�b�n�A�A�A�A�A�A�A�@�@�@�@�I�I�I�@HighEmotionality�I\r\n";
                    case 86: // High Emotionality 2
                        return this.name + "�̗́A�Z�A�m�A�S�p�����^���t�o�����I\r\n";
                    case 87: // AbsoluteZero�ŃX�L���g�p���s
                        return this.name + "�F�E�E�E�����A�v���悤�ɓ����܂���E�E�E�A�X�L���g�p�~�X�ł��B\r\n";
                    case 88: // AbsoluteZero�ɂ��h�䎸�s
                        return this.name + "�F�����E�E�E�h�䂪�E�E�E�v���悤�ɂł��܂���I \r\n";
                    case 89: // Silent Rush 1
                        return this.name + "�F�M���͎p���瑨�����Ȃ��ł��傤�ˁASilentRush�ł��B�@��� {0}�_���[�W�E�E�E�@";
                    case 90: // Silent Rush 2
                        return "�ӂ��� {0}�_���[�W   ";
                    case 91: // Silent Rush 3
                        return "�����āA�݂��߂ł��B�n�A�@�@�@�I {0}�̃_���[�W\r\n";
                    case 92: // BUFFUP�ȊO�̉i�����ʂ����ɂ��Ă���ꍇ
                        return "�������A���ɂ��̌��ʂ͕t�^����Ă���B\r\n";
                    case 93: // Anti Stun
                        return this.name + "�F�X�^�����ʂ̓{�N�ɂ͌����܂����BAntiStun�ł��B " + this.name + "�̓X�^�����ʂւ̑ϐ����t����\r\n";
                    case 94: // Anti Stun�ɂ��X�^�����
                        return this.name + "�F�������͂��ł��B�{�N�ɃX�^���͌����Ȃ��B\r\n";
                    case 95: // Stance Of Death
                        return this.name + "�F���E�E�E�j�ł̌��t�ł���A�b�N�N�N�AStanceOfDeath�I " + this.name + "�͒v�����P�x����ł���悤�ɂȂ���\r\n";
                    case 96: // Oboro Impact 1
                        return this.name + "�F�����܂����H���̗e���A�y���ɉ��`�zOboro Impact�ł��B\r\n";
                    case 97: // Oboro Impact 2
                        return this.name + "�F�b�n�A�A�@�@�I�I�@ {0}��{1}�̃_���[�W\r\n";
                    case 98: // Catastrophe 1
                        return this.name + "�F�S�Ă�j��E�E�E�j�󂵂Ă����܂��傤�y���ɉ��`�zCatastrophe�ł�\r\n";
                    case 99: // Catastrophe 2
                        return this.name + "�F�b�n�A�A�@�@�@�A�A�A�I�I�@ {0}�̃_���[�W\r\n";
                    case 100: // Stance Of Eyes
                        return this.name + "�F���ʂł��B�S�Č��؂��Ă��������܂��傤�A StanceOfEyes�ł��B " + this.name + "�́A����̍s���ɔ����Ă���E�E�E\r\n";
                    case 101: // Stance Of Eyes�ɂ��L�����Z����
                        return this.name + "�F�\�R�̃��[�V�����ł��ˁA�@" + this.name + "�͑���̃��[�V���������؂��āA�s���L�����Z�������I\r\n";
                    case 102: // Stance Of Eyes�ɂ��L�����Z�����s��
                        return this.name + "�F���΂��ȁI�H�������܂������ǂ߂Ȃ��I�@" + this.name + "�͑���̃��[�V���������؂�Ȃ������I\r\n";
                    case 103: // Negate
                        return this.name + "�F�r���Ƃ͕K����������܂��BNegate�ł��B" + this.name + "�͑���̃X�y���r���ɔ����Ă���E�E�E\r\n";
                    case 104: // Negate�ɂ��X�y���r���L�����Z����
                        return this.name + "�F�b�n�n�n�I�r�����삪���������ł���I" + this.name + "�͑���̃X�y���r����e�����I\r\n";
                    case 105: // Negate�ɂ��X�y���r���L�����Z�����s��
                        return this.name + "�F���΂��ȁI�E�E�E�����̊Ԃɉr�����I�H" + this.name + "�͑���̃X�y���r�������؂�Ȃ������I\r\n";
                    case 106: // Nothing Of Nothingness 1
                        return this.name + "�F�b�n�n�n�n�n�I�I�^�Ȃ�[���y���ɉ��`�zNothingOfNothingness�I " + this.name + "�ɖ��F�̃I�[�����h��n�߂�I \r\n";
                    case 107: // Nothing Of Nothingness 2
                        return this.name + "�͖��������@�𖳌����A�@����������X�L���𖳌�������悤�ɂȂ����I\r\n";
                    case 108: // Genesis
                        return this.name + "�F���ꂪ���R����N���AGenesis�ł��B  " + this.name + "�͑O��̍s���������ւƓ��e�������I\r\n";
                    case 109: // Cleansing�r�����s��
                        return this.name + "�F�����A�����܂���E�E�E���q��������Cleansing�������ł��܂���B\r\n";
                    case 110: // CounterAttack�𖳎�������
                        return this.Name + "�F�b�n�n�n�n�I����ȍ\���A���������ł���I\r\n";
                    case 111: // �_�����g�p��
                        return this.name + "�F���C�t�E�X�L���E�}�i��30%���񕜂ł��B\r\n";
                    case 112: // �_�����A���Ɏg�p�ς݂̏ꍇ
                        return this.name + "�F�������͎c���ĂȂ��悤�ł��B����҂��Ȃ��Ƒʖڂł��ˁB\r\n";
                    case 113: // CounterAttack�ɂ�锽�����b�Z�[�W
                        return this.name + "�F�Â��ł��ˁA�J�E���^�[�ł��B\r\n";
                    case 114: // CounterAttack�ɑ΂��锽����NothingOfNothingness�ɂ���Ėh���ꂽ��
                        return this.name + "�F�b�o�A�o�J�ȁI���̃{�N�����؂�Ȃ��Ȃ�āI\r\n";
                    case 115: // �ʏ�U���̃q�b�g
                        return this.name + "�̍U�����q�b�g�B {0} �� {1} �̃_���[�W\r\n";
                    case 116: // �h��𖳎����čU�����鎞
                        return this.name + "�F�h��o����E�E�E�Ƃł��v���܂������H\r\n";
                    case 117: // StraightSmash�Ȃǂ̃X�L���N���e�B�J��
                        return this.name + "�F�N���e�B�J���q�b�g�ł��B\r\n";
                    case 118: // �퓬���A�����@�C���|�[�V�����ɂ�镜���̂�����
                        return this.name + "�F�����@�C���|�[�V�����ł��B����ŕ����ł��ˁB\r\n";
                    case 119: // Absolute Zero�ɂ�胉�C�t�񕜂ł��Ȃ��ꍇ
                        return this.name + "�F���̓��Ă������E�E�E���C�t�񕜂ł��܂���B\r\n";
                    case 120: // ���@�U���̃q�b�g
                        return "{0} �� {1} �̃_���[�W\r\n";
                    case 121: // Absolute Zero�ɂ��}�i�񕜂ł��Ȃ��ꍇ
                        return this.name + "�F���̓��Ă������E�E�E�}�i�񕜂ł��܂���B\r\n";
                    case 122: // �u���߂�v�s����
                        return this.name + "�F�b�n�n�n�A����ȊȒP�ɖ��͂�~�������Ă��炦��Ƃ́B\r\n";
                    case 123: // �u���߂�v�s���ŗ��܂肫���Ă���ꍇ
                        return this.name + "�F���͂͏\���~���Ă���܂��B�����\���ł��傤�B\r\n";
                    case 124: // StraightSmash�̃_���[�W�l
                        return this.name + "�̃X�g���[�g�E�X�}�b�V�����q�b�g�B {0} �� {1}�̃_���[�W\r\n";
                    case 125: // �A�C�e���g�p�Q�[�W�����܂��ĂȂ��Ƃ�
                        return this.name + "�F�A�C�e���Q�[�W�����܂��ĂȂ��ԁA�A�C�e���͎g���܂���ˁB\r\n";
                    case 126: // FlashBlase
                        return this.name + "�F����͒ɂ��ł��傤�ˁAFlashBlaze�ł��B {0} �� {1} �̃_���[�W\r\n";
                    case 127: // �������@�ŃC���X�^���g�s��
                        return this.name + "�F�{�N�Ƃ������Ƃ��E�E�E�C���X�^���g�l������܂���\r\n";
                    case 128: // �������@�̓C���X�^���g�^�C�~���O�ł��ĂȂ�
                        return this.name + "�F�C���X�^���g�̃^�C�~���O�Ŕ������������ł����B\r\n";
                    case 129: // PsychicTrance
                        return this.name + "�F�b�n�n�n�A�X�ɖ��@�U�������APsychicTrance�B\r\n";
                    case 130: // BlindJustice
                        return this.name + "�F�b�N�N�N�A�X�ɕ����U�������ABlindJustice�B\r\n";
                    case 131: // TranscendentWish
                        return this.name + "�F���Ȃǋ���܂����ATranscendentWish�B\r\n";
                    case 132: // LightDetonator
                        return this.name + "�F�����ɍs���ƕ������Ă܂����ALightDetonator�B\r\n";
                    case 133: // AscendantMeteor
                        return this.name + "�Ă��ł��邪�ǂ��AAscendantMeteor\r\n";
                    case 134: // SkyShield
                        return this.name + "�F�󐹂̉���ASkyShield\r\n";
                    case 135: // SacredHeal
                        return this.name + "�F�S���񕜂ł��ASacredHeal\r\n";
                    case 136: // EverDroplet
                        return this.name + "�F�b�N�N�N�A����������Ƃ͋����ȁAEverDroplet\r\n";
                    case 137: // FrozenAura
                        return this.name + "�F�X������t�^�AFrozenAura�ł��B\r\n";
                    case 138: // ChillBurn
                        return this.name + "�FChillBurn�œ������Ă��������B\r\n";
                    case 139: // ZetaExplosion
                        return this.name + "�F��݂�����AZeta�I�@�b�n�n�E�E�E�b�n�n�n�n�I�I�I\r\n";
                    case 140: // FrozenAura�ǉ����ʃ_���[�W��
                        return this.name + "�F�����Ă��������B {0}�̒ǉ��_���[�W\r\n";
                    case 141: // StarLightning
                        return this.name + "�F�ق�̈�u�ł��AStarLightning�B\r\n";
                    case 142: // WordOfMalice
                        return this.name + "�F�n�n�n�A����ɒx���Ȃ�܂���AWordOfMalice�B\r\n";
                    case 143: // BlackFire
                        return this.name + "�F���@�h��ቺ�ABlackFire�B\r\n";
                    case 144: // EnrageBlast
                        return this.name + "�F���āA�Ă���Ă��炢�܂��傤�AEnrageBlast�B\r\n";
                    case 145: // Immolate
                        return this.name + "�F�����h��ቺ�AImmolate�B\r\n";
                    case 146: // VanishWave
                        return this.name + "�F�ق��Ă��Ă���܂��񂩁AVanishWave�B\r\n";
                    case 147: // WordOfAttitude
                        return this.name + "�F�ڋ��ƌĂ�ł�����č\���܂����AWordOfAttitude�B\r\n";
                    case 148: // HolyBreaker
                        return this.name + "�F�_���[�W�̍���t���܂��傤�AHolyBreaker�B\r\n";
                    case 149: // DarkenField
                        return this.name + "�F�h��͑S�ʒቺ�ADarkenField�B\r\n";
                    case 150: // SeventhMagic
                        return this.name + "�F�����𕢂��Ƃ��܂��傤�ASeventhMagic�B\r\n";
                    case 151: // BlueBullet
                        return this.name + "�F�X�̔��I�ABlueBullet�ł��B\r\n";
                    case 152: // NeutralSmash
                        return this.name + "�FNeutralSmash�A�b�n�@�I\r\n";
                    case 153: // SwiftStep
                        return this.name + "�F���x�グ�����Ă��炢�܂��ASwiftStep�B\r\n";
                    case 154: // CircleSlash
                        return this.name + "�F�ז��ł��˂ǂ��Ă��������ACircleSlash�B\r\n";
                    case 155: // RumbleShout
                        return this.name + "�F�ǂ������Ă��ł����H�R�`���ł��B\r\n";
                    case 156: // SmoothingMove
                        return this.name + "�F�b�N�N�N�A�قږ����R���{�ł��ASmoothingMove�B\r\n";
                    case 157: // FutureVision
                        return this.name + "�F�{�N��������Ȃ��n�Y���Ȃ��AFutureVision�B\r\n";
                    case 158: // ReflexSpirit
                        return this.name + "�F�X�^���n�͎��O����Ɍ���܂��AReflexSpirit�B\r\n";
                    case 159: // SharpGlare
                        return this.name + "�F�ق��Ă��Ă���܂��񂩁ASharpGlare�B\r\n";
                    case 160: // TrustSilence
                        return this.name + "�F���قȂǃ{�N�ɂ͖����ł��ˁATrustSilence�B\r\n";
                    case 161: // SurpriseAttack
                        return this.name + "�b�n�n�n�n�A����ŋC�₵���܂��Ȃ��ISurpriseAttack�I�I\r\n";
                    case 162: // PsychicWave
                        return this.name + "�F�b�n�n�n�A�~�߂��Ȃ��ł��傤�APsychicWave�B\r\n";
                    case 163: // Recover
                        return this.name + "�F�������肵�Ă��������ARecover�ł��B\r\n";
                    case 164: // ViolentSlash
                        return this.name + "�F�g�h���F�F�F�I�I�@ViolentSlash�I�I�I\r\n";
                    case 165: // OuterInspiration
                        return this.name + "�F���āA����Ō��ʂ�ł��ˁAOuterInspiration�B\r\n";
                    case 166: // StanceOfSuddenness
                        return this.name + "�F�b�n�n�n�n�IStanceOfSuddenness�I�b�n�b�n�n�n�n�I�I\r\n";
                    case 167: // �C���X�^���g�ΏۂŔ����s��
                        return this.name + "�F�Ώۂ̃C���X�^���g�R�}���h�������ł��ˁB\r\n";
                    case 168: // StanceOfMystic
                        return this.name + "�F���؂�������ł��傤���A�Â��ł��BStanceOfMystic�B\r\n";
                    case 169: // HardestParry
                        return this.name + "�F���̍U���A�u�Ԃŉ�����Ă݂��܂��傤�BHardestParry�ł��B\r\n";
                    case 170: // ConcussiveHit
                        return this.name + "�F�h�䗦DOWN�ł��AConcussiveHit�B\r\n";
                    case 171: // Onslaught hit
                        return this.name + "�F�U����DOWN�ł��AOnslaughtHit�B\r\n";
                    case 172: // Impulse hit
                        return this.name + "�F���x��DOWN�ł��AImpulseHit�B\r\n";
                    case 173: // Fatal Blow
                        return this.name + "�F���āA����ł��炢�܂��傤�AFatalBlow�B\r\n";
                    case 174: // Exalted Field
                        return this.name + "�F���̏�ɂčX�Ȃ鑝���������܂��AExaltedField�B\r\n";
                    case 175: // Rising Aura
                        return this.name + "�F�X�ɍU���𑝂��܂���ARisingAura�ł��B\r\n";
                    case 176: // Ascension Aura
                        return this.name + "�F�X�ɖ��@�U�����グ�܂��傤���AAscensionAura�ł��B\r\n";
                    case 177: // Angel Breath
                        return this.name + "�F�V�g�̉���ɂ���Ԉُ�񕜂��AAngelBreath�ł��B\r\n";
                    case 178: // Blazing Field
                        return this.name + "�F�����ɔR�₵�s�����Ă����܂���ABlazingField�B\r\n";
                    case 179: // Deep Mirror
                        return this.name + "�F���ꂪ�ʂ�Ƃ͎v���ĂȂ��ł��傤�ADeepMirror�ł��B\r\n";
                    case 180: // Abyss Eye
                        return this.name + "�F���̊�����������Ō�ł��AAbyssEye�B\r\n";
                    case 181: // Doom Blade
                        return this.name + "�F�}�i��f���؂点�Ă��炢�܂��ADoomBlade�B\r\n";
                    case 182: // Piercing Flame
                        return this.name + "�F�����͊ђʂ̉΂��g���܂��傤�APiercingFlame�B\r\n";
                    case 183: // Phantasmal Wind
                        return this.name + "�F����ɏグ�Ă����܂��APhantasmalWind�ł��B\r\n";
                    case 184: // Paradox Image
                        return this.name + "�F���݂̌��AParadoxImage�ł��B\r\n";
                    case 185: // Vortex Field
                        return this.name + "�F�F����ݑ��ɂȂ��Ă��������AVortexField�ł��B\r\n";
                    case 186: // Static Barrier
                        return this.name + "�F�����̉���AStaticBarrier\r\n";
                    case 187: // Unknown Shock
                        return this.name + "�F���Ȃ����ÈłŐ킦��Ƃ͎v���܂��񂯂ǂˁAUnknownShock�ł��B\r\n";
                    case 188: // SoulExecution
                        return this.name + "�F�b�N�N�E�E�ESoulExecusion�B\r\n";
                    case 189: // SoulExecution hit 01
                        return this.name + "�F�g�D�I\r\n";
                    case 190: // SoulExecution hit 02
                        return this.name + "�F�V�b�I\r\n";
                    case 191: // SoulExecution hit 03
                        return this.name + "�F�c�F�I\r\n";
                    case 192: // SoulExecution hit 04
                        return this.name + "�F�Z�B�I\r\n";
                    case 193: // SoulExecution hit 05
                        return this.name + "�F�X�D�I\r\n";
                    case 194: // SoulExecution hit 06
                        return this.name + "�F�t�b�I\r\n";
                    case 195: // SoulExecution hit 07
                        return this.name + "�F�h�D�I\r\n";
                    case 196: // SoulExecution hit 08
                        return this.name + "�F�Z�C�I\r\n";
                    case 197: // SoulExecution hit 09
                        return this.name + "�F�n�A�A�@�@�I\r\n";
                    case 198: // SoulExecution hit 10
                        return this.name + "�F�g�h���ł��I�n�A�@�@�@�I�I�I\r\n";
                    case 199: // Nourish Sense
                        return this.name + "�F�񕜗ʂ��グ�����Ă��炢�܂��傤�ANourishSense�ł��B\r\n";
                    case 200: // Mind Killing
                        return this.name + "�F���_����U�߂����Ă��炢�܂��傤�AMindKilling�B\r\n";
                    case 201: // Vigor Sense
                        return this.name + "�F�����l���X�ɏグ�����Ă��炢�܂��AVigorSense�ł��B\r\n";
                    case 202: // ONE Authority
                        return this.name + "�F�S���グ�Ă����܂��傤���AOneAuthority�B\r\n";
                    case 203: // �W���ƒf��
                        return this.name + "�F�y�W���ƒf��z�@�����B\r\n";
                    case 204: // �y���j�z�����ς�
                        return this.name + "�F���݂܂��񂪁y���j�z�́A�������łɔ����ς݂ł��B\r\n";
                    case 205: // �y���j�z�ʏ�s���I����
                        return this.name + "�F�y���j�z�̓C���X�^���g�^�C�~���O����ł��B\r\n";
                    case 206: // Sigil Of Homura
                        return this.name + "�F���ꂪ���܂�����A�قڏI���ł��ˁBSigilOfHomura�ł��B\r\n";
                    case 207: // Austerity Matrix
                        return this.name + "�F�x�z�́A�ς������Ă��炢�܂��AAusterityMatrix�B\r\n";
                    case 208: // Red Dragon Will
                        return this.name + "�F�Η���A�{�N�Ɏd����ARedDragonWill�B\r\n";
                    case 209: // Blue Dragon Will
                        return this.name + "�F������A�{�N�Ɏd����ABlueDragonWill�B\r\n";
                    case 210: // Eclipse End
                        return this.name + "�F�b�N�N�A����őS�Ă����ʂƂȂ�܂��ˁAEclipseEnd�B\r\n";
                    case 211: // Sin Fortune
                        return this.name + "�F���̃N���e�B�J���A�o�債�Ă��������ASinFortune�ł��B\r\n";
                    case 212: // AfterReviveHalf
                        return this.name + "�F���ϐ��i�n�[�t�j��t�^�����Ă��炢�܂��B\r\n";
                    case 213: // Demonic Ignite
                        return this.name + "�F����łقڋl�݂ł��ˁADemonicIgnite�ł��B\r\n";
                    case 214: // Death Deny
                        return this.name + "�F�`���ł͂Ȃ����S�Ȃ�h��ł��ADeathDeny�B\r\n";
                    case 215: // Stance of Double
                        return this.name + "�F���ꂼ���Ɍ����AStanceOfDouble�ł��B  " + this.name + "�͑O��s�������������̕��g�𔭐��������I\r\n";
                    case 216: // �ŏI�탉�C�t�J�E���g����
                        return this.name + "�F�_���I��ɉi���̐������I�I�b�n�A�A�A�@�@�@�I�I�I\r\n";
                    case 217: // �ŏI�탉�C�t�J�E���g���Ŏ�
                        return this.name + "�F�b�O�E�E�E�Z�E�E�E�Z�t�B�E�E�E�l�E�E�E\r\n";

                    case 2001: // �|�[�V�����񕜎�
                        return this.name + "�F{0} �񕜂ł��B";
                    case 2002: // ���x���A�b�v�I���Ñ�
                        return this.name + "�F���x���A�b�v���ɂ��Ă��������B";
                    case 2003: // �ו������点��Ñ�
                        return this.name + "�F{0}�A�ו������ς��ł��ˁH�A�C�e�������炵�Ă���܂������Ă��������B";
                    case 2004: // �������f
                        return this.name + "�F�������Ă������ł����H";
                    case 2005: // ��������
                        return this.name + "�F���������ł��B";
                    case 2006: // �����̐������g�p
                        return this.name + "�F��������A���ɖ߂�܂��傤���H";
                    case 2007: // ���p��p�i�ɑ΂���ꌾ
                        return this.name + "�F���p��p�̕i���ł��B";
                    case 2008: // ��퓬���̃}�i�s��
                        return this.name + "�F�}�i���s�����Ă��܂��ˁB";
                    case 2009: // �_�����g�p��
                        return this.name + "�F���C�t�E�X�L���E�}�i��30%���񕜂ł��B";
                    case 2010: // �_�����A���Ɏg�p�ς݂̏ꍇ
                        return this.name + "�F�������͎c���ĂȂ��悤�ł��B����҂��Ȃ��Ƒʖڂł��ˁB";
                    case 2011: // ���[�x�X�g�����N�|�[�V������퓬�g�p��
                        return this.name + "�F�퓬����p�i�ł��B";
                    case 2012: // �����̐������g�p�i���؍ݎ��j
                        return this.name + "�F���̒�����g���Ă��Ӗ��͂���܂���ˁB";
                    case 2013: // �����̐������̂Ă��Ȃ����̑䎌
                        return this.name + "�F����͂������Ɏ̂Ă��܂���B";
                    case 2014: // ��퓬���̃X�L���s��
                        return this.name + "�F�X�L�����s�����Ă��܂��ˁB";
                    case 2015: // ���v���C���[���̂ĂĂ��܂����Ƃ����ꍇ
                        return this.name + "�F�����܂���A����͂�����Ǝ̂ĂȂ��ł��������B";
                    case 2016: // �����@�C���|�[�V�����ɂ�镜��
                        return this.name + "�F�������ŕ����ł��܂����B���肪�Ƃ��������܂��B";
                    case 2017: // �����@�C���|�[�V�����s�v�Ȏ�
                        return this.name + "�F{0}�͐����ĂȂ��̂ł́H�Ƃ����u���b�N�W���[�N�ł��B";
                    case 2018: // �����@�C���|�[�V�����Ώۂ������̎�
                        return this.name + "�F�����{�N������ł�����g���s�׎��̂ł��Ȃ��͂��B�ʔ����W���[�N�ł��ˁB";
                    case 2019: // �����s�̃A�C�e���𑕔����悤�Ƃ�����
                        return this.name + "�F����̓{�N�ł͑����ł��܂���ˁB";
                    case 2020: // ���X�{�X���j�̌�A�����̐������g�p�s��
                        return this.name + "�F�����܂���A���̏󋵂ł��̃A�C�e�����g���K�v�͂Ȃ������ł��B";
                    case 2021: // �A�C�e���̂Ă�̍Ñ�
                        return this.name + "�F��Ƀo�b�N�p�b�N�̐��������܂��񂩁H";
                    case 2022: // �I�[�o�[�V�t�e�B���O�g�p�J�n��
                        return this.name + "�F���A����́E�E�E�g�̔\�͂��č\�z�����Ƃ́E�E�E�f���炵���ł��ˁB";
                    case 2023: // �I�[�o�[�V�t�e�B���O�ɂ��p�����^����U�莞
                        return this.name + "�F�I�[�o�[�V�t�e�B���O�ɂ�銄��U�肪�I����Ă���ɂ��܂��傤�B";
                    case 2024: // �������L�b�h���g�p������
                        return this.name + "�F�y{0}�z�p�����^�A {1} �㏸���܂����ˁB";
                    case 2025:
                        return this.name + "�F���蕐��𑕔����Ă��܂�����ˁB�T�u�͑����ł��܂���B";
                    case 2026:
                        return this.name + "�F����i���C���j���͂��߂ɉ����������Ȃ��Ƃ����܂���ˁB";
                    case 2027: // �������g�p��
                        return this.name + "�F���C�t��100%�񕜂��܂����ˁB";
                    case 2028: // �������A���Ɏg�p�ς݂̏ꍇ
                        return this.name + "�F�������͎c���ĂȂ���ł��B����҂��Ȃ��Ƒʖڂł��ˁB";
                    case 2029: // �ו���t�ő������O���Ȃ���
                        return this.name + "�F�o�b�N�p�b�N���󂯂܂��傤�B���̂܂܂ł͑����͊O���܂���ˁB";
                    case 2030: // �ו���t�ŉ������̂Ă鎞�̑䎌
                        return this.name + "�F{0}���̂Ă܂��傤�B�����ł��ˁH";
                    case 2031: // �퓬���̃A�C�e���g�p����
                        return this.name + "�F�퓬���ł��B�g�p����A�C�e���𑁂����߂܂��񂩁H";
                    case 2032: // �퓬���A�A�C�e���g�p�ł��Ȃ��A�C�e����I�������Ƃ�
                        return this.name + "�F���̃A�C�e���͐퓬���Ɏg�p�͏o���܂���ˁB";
                    case 2033: // �a�����Ȃ��ꍇ
                        return this.name + "�F�����a����͓̂���ł͂���܂���ˁB";
                    case 2034: // �A�C�e�������ς��ŗa���菊��������o���Ȃ��ꍇ
                        return this.name + "�F�����A����ȏ�͎����čs���K�v�͂���܂���ˁB";
                    case 2035: // Sacred Heal
                        return this.name + "�F�S���񕜂��܂����ˁB";

                    case 3000: // �X�ɓ��������̑䎌
                        return this.name + "�F���h���ȏ�Ԃł��ˁE�E�E";
                    case 3001: // �x�����v����
                        return this.name + "�F{0}���w�����܂��傤�B {1}Gold�u���܂��傤�B";
                    case 3002: // �����������ς��Ŕ����Ȃ���
                        return this.name + "�F�A�C�e���������ς��̂悤�ł��B�������������Ă��������B";
                    case 3003: // �w��������
                        return this.name + "�F����ŁE�E�E���������Ƃ����Ă��������B";
                    case 3004: // Gold�s���ōw���ł��Ȃ��ꍇ
                        return this.name + "�FGold������{0}����Ȃ��悤�ł��B";
                    case 3005: // �w�������L�����Z�������ꍇ
                        return this.name + "�F�����������Ă��������B";
                    case 3006: // ����Ȃ��A�C�e���𔄂낤�Ƃ����ꍇ
                        return this.name + "�F�����܂���A���̃A�C�e���͔��p�ł��܂���B";
                    case 3007: // �A�C�e�����p��
                        return this.name + "�F{0}�𔄋p���܂��B�܂�A{1}Gold�����Ă��ǂ��n�Y�ł��ˁB";
                    case 3008: // ����̓y���_���g���p��
                        return this.name + "�F�R���̓��i����̍�����A�N�Z�T���ł��ˁB{0}Gold�Ŗ{���ɔ���̂ł��傤���H";
                    case 3009: // ����X���o�鎞
                        return this.name + "�F�܂��E�E�E�����A���Ă��Ă��������B";
                    case 3010: // �K���c�s�ݎ��̔��肫��t�F���g�D�[�V�������Ĉꌾ
                        return this.name + "�F���̌��́E�E�E�m���ɔ���؂�ł��ˁB";
                    case 3011: // �����\�Ȃ��̂��w�����ꂽ��
                        return this.name + "�F���̏�ő������Ă��ǂ��ł��傤���H";
                    case 3012: // �������Ă������𔄋p�Ώۂ��ǂ���������
                        return this.name + "�F���݂́A{0}�𑕔����Ă��܂��B�Ӓ�͕s���ӂł���{1}Gold���炢�Ŕ����ł��傤�B";

                    case 4001: // �ʏ�U����I��
                    case 4002: // �h���I��
                    case 4003: // �ҋ@��I��
                    case 4004: // �t���b�V���q�[����I��
                    case 4005: // �v���e�N�V������I��
                    case 4006: // �t�@�C�A�E�{�[����I��
                    case 4007: // �t���C���E�I�[����I��
                    case 4008: // �X�g���[�g�E�X�}�b�V����I��
                    case 4009: // �_�u���E�X�}�b�V����I��
                    case 4010: // �X�^���X�E�I�u�E�X�^���f�B���O��I��
                    case 4011: // �A�C�X�E�j�[�h����I��
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
                        return this.name + "�F" + this.ActionLabel.Text + "�ł����܂��B\r\n";
                    case 4072:
                        return this.name + "�F�G��Ώۂɂ���킯�ɂ͂����܂���ˁB\r\n";
                    case 4073:
                        return this.name + "�F�G��Ώۂɂ���킯�ɂ͂����܂���ˁB\r\n";
                    case 4074:
                        return this.name + "�F������Ώۂɂ͂ł��܂���ˁB\r\n";
                    case 4075:
                        return this.name + "�F������Ώۂɂ͂ł��܂���ˁB\r\n";
                    case 4076:
                        return this.name + "�F�����ɍU�����������͂���܂���B\r\n";
                    case 4077: // �u���߂�v�R�}���h
                        return this.name + "�F���͂����ߍ��݂܂��B\r\n";
                    case 4078: // ���픭���u���C���v
                        return this.name + "�F���C������̌��ʂ𔭓������܂��B\r\n";
                    case 4079: // ���픭���u�T�u�v
                        return this.name + "�F�T�u����̌��ʂ𔭓������܂��B\r\n";
                    case 4080: // �A�N�Z�T���P����
                        return this.name + "�F�A�N�Z�T���P�̌��ʂ𔭓������܂��B\r\n";
                    case 4081: // �A�N�Z�T���Q����
                        return this.name + "�F�A�N�Z�T���Q�̌��ʂ𔭓������܂��B\r\n";

                    case 4082: // FlashBlaze
                        return this.name + "�F�����̓t���b�V���u���C�Y�ł��ˁB\r\n";

                    // ����U��
                    case 5001:
                        return this.name + "�F�G�A���E�X���b�V���ł��B�n�A�A�@�I�I {0} �� {1} �̃_���[�W\r\n";
                    case 5002:
                        return this.name + "�F{0} �񕜂ł� \r\n";
                    case 5003:
                        return this.name + "�F{0} �}�i�񕜂ł� \r\n";
                    case 5004:
                        return this.name + "�F�A�C�V�N���E�X���b�V���A�s���܂��I {0} �� {1} �̃_���[�W\r\n";
                    case 5005:
                        return this.name + "�F�G���N�g���E�u���[�ł��A�n�A�@�@�I {0} �� {1} �̃_���[�W\r\n";
                    case 5006:
                        return this.name + "�F�u���[�E���C�g�j���O�ł��B�n�A�@�@�I {0} �� {1} �̃_���[�W\r\n";
                    case 5007:
                        return this.name + "�F�o�[�j���O�E�N���C���A�ł��B�n�A�@�@�I {0} �� {1} �̃_���[�W\r\n";
                    case 5008:
                        return this.name + "�F�ԑ�������̉��ł��B�n�A�@�@�I {0} �� {1} �̃_���[�W\r\n";
                    case 5009:
                        return this.name + "�F{0} �X�L���|�C���g�񕜂ł� \r\n";
                    case 5010:
                        return this.name + "���������Ă���w�ւ�����ɑ�������������I {0} �� {1} �̃_���[�W\r\n";
                }
            }
            #endregion
            #region "�����f�B�X"
            if (this.name == "�����f�B�X")
            {
                switch (sentenceNumber)
                {
                    case 0: // �X�L���s��
                        return this.name + "�F�b�`�E�E�E�X�L�����˂�\r\n";
                    case 1: // Straight Smash
                        return this.name + "�F�b���@�I�I\r\n";
                    case 2: // Double Slash 1
                        return this.name + "�F�b���I\r\n";
                    case 3: // Double Slash 2
                        return this.name + "�F�b�h���@�I\r\n";
                    case 4: // Painful Insanity
                        return this.name + "�F���_�����ŕ�����C�͂��˂��A�������B\r\n";
                    case 5: // empty skill
                        return this.name + "�F�b�`�E�E�E�X�L������Ƃ́E�E�E\r\n";
                    case 6: // ���݂��t�����V�X�̕K�E��H�������
                        return this.name + "�F�������˂��Ȃ��E�E�E\r\n";
                    case 7: // Lizenos�̕K�E��H�������
                        return this.name + "�F��邶��˂����E�E�E\r\n";
                    case 8: // Minflore�̕K�E��H�������
                        return this.name + "�F�b�P�E�E�E�h�W�������E�E�E\r\n";
                    case 9: // Fresh Heal�ɂ�郉�C�t��
                        return this.name + "�F{0} �񕜂��Ƃ������B\r\n";
                    case 10: // Fire Ball
                        return this.name + "�F�t�@�C�A�I {0} �� {1} �̃_���[�W\r\n";
                    case 11: // Flame Strike
                        return this.name + "�F�t���X�g�I {0} �� {1} �̃_���[�W\r\n";
                    case 12: // Volcanic Wave
                        return this.name + "�F���H���J�j�B�I {0} �� {1} �̃_���[�W\r\n";
                    case 13: // �ʏ�U���N���e�B�J���q�b�g
                        return this.name + "�F�b�V���I���A�����������I {0} �� {1}�̃_���[�W\r\n";
                    case 14: // FlameAura�ɂ��ǉ��U��
                        return this.name + "�F�I�����I {0}�̒ǉ��_���[�W\r\n";
                    case 15: //�����̐�����퓬���Ɏg�����Ƃ�
                        return this.name + "�F�R�C�c�͐퓬���g���˂��B\r\n";
                    case 16: // ���ʂ𔭊����Ȃ��A�C�e����퓬���Ɏg�����Ƃ�
                        return this.name + "�F�b�`�B�I�g���˂��A�C�e�����ȁI\r\n";
                    case 17: // ���@�Ń}�i�s��
                        return this.name + "�F�b�P�E�E�E�����}�i�͂˂�\r\n";
                    case 18: // Protection
                        return this.name + "�F�v���b�c�I �����h��グ�Ƃ����B\r\n";
                    case 19: // Absorb Water
                        return this.name + "�F�@���\�[���I ���@�h��グ�Ƃ����B\r\n";
                    case 20: // Saint Power
                        return this.name + "�F�Z�C���g�I �����U���グ�Ƃ����B\r\n";
                    case 21: // Shadow Pact
                        return this.name + "�F�V���h�E�I ���@�U���グ�Ƃ����Br\n";
                    case 22: // Bloody Vengeance
                        return this.name + "�F���F���W�F�I �y�́z�� {0} �グ�Ƃ����B\r\n";
                    case 23: // Holy Shock
                        return this.name + "�F�V���b�N�I {0} �� {1} �̃_���[�W\r\n";
                    case 24: // Glory
                        return this.name + "�F�O���[���I ���ڍU���{FreshHeal�A�g�̃I�[��\r\n";
                    case 25: // CelestialNova 1
                        return this.name + "�F�m���@�I {0} �񕜂��Ƃ������B\r\n";
                    case 26: // CelestialNova 2
                        return this.name + "�F�m���@�I {0} �̃_���[�W\r\n";
                    case 27: // Dark Blast
                        return this.name + "�F�u���X�g�I {0} �� {1} �̃_���[�W\r\n";
                    case 28: // Lava Annihilation
                        return this.name + "�F�_�[�b�n�b�n�b�n�I�R�����܂��Ȃ��I {0} �� {1} �̃_���[�W\r\n";
                    case 10028: // Lava Annihilation���
                        return this.name + "�F�_�[�b�n�b�n�b�n�I�R�����܂��Ȃ��I\r\n";
                    case 29: // Devouring Plague
                        return this.name + "�F�f���H�v���@�I {0} ���C�t���z�������\r\n";
                    case 30: // Ice Needle
                        return this.name + "�F�j�[�h���I {0} �� {1} �̃_���[�W\r\n";
                    case 31: // Frozen Lance
                        return this.name + "�F�����X�I {0} �� {1} �̃_���[�W\r\n";
                    case 32: // Tidal Wave
                        return this.name + "�F�E�F�C���I {0} �̃_���[�W\r\n";
                    case 33: // Word of Power
                        return this.name + "�F���[�p���[�I {0} �� {1} �̃_���[�W\r\n";
                    case 34: // White Out
                        return this.name + "�F�z���C�A�E�g�I {0} �� {1} �̃_���[�W\r\n";
                    case 35: // Black Contract
                        return this.name + "�F�b�J�b�J�b�J�A�R�C�c�Ō������肾�B " + this.name + "�̃X�L���A���@�R�X�g�͂O�ɂȂ�B\r\n";
                    case 36: // Flame Aura�r��
                        return this.name + "�F���łԂ����a��B�@ ���ڍU���ɉ��̒ǉ����ʂ��t�^�����B\r\n";
                    case 37: // Damnation
                        return this.name + "�F�\�R�Ŏ���ł낧�A�_���l�[�V�����I�@�@�������o�ł鍕����Ԃ�c�܂���B\r\n";
                    case 38: // Heat Boost
                        return this.name + "�F�u�[�X�g�I �y�Z�z�� {0} �グ�Ƃ����B\r\n";
                    case 39: // Immortal Rave
                        return this.name + "�F���A�A�@�@�@�@�I���C���I�I " + this.name + "�̎���ɂR�̉����h�����B\r\n";
                    case 40: // Gale Wind
                        return this.name + "�F�Q�C���I�@ ������l��" + this.name + "�����ꂽ�B\r\n";
                    case 41: // Word of Life
                        return this.name + "�F���[���C�t�I �厩�R����̋�����������������悤�ɂȂ����B\r\n";
                    case 42: // Word of Fortune
                        return this.name + "�F���[�t�H�[�`���I �����̃I�[�����N���オ�����B\r\n";
                    case 43: // Aether Drive
                        return this.name + "�F�h���C���I ���͑S�̂ɋ�z�����͂��݂Ȃ���B\r\n";
                    case 44: // Eternal Presence 1
                        return this.name + "�F�b�[���X�I " + this.name + "�̎���ɐV�����@�����\�z�����B\r\n";
                    case 45: // Eternal Presence 2
                        return this.name + "�̕����U���A�����h��A���@�U���A���@�h�䂪�t�o�����I\r\n";
                    case 46: // One Immunity
                        return this.name + "�F�����C���I " + this.name + "�̎��͂ɖڂɌ����Ȃ���ǂ������B\r\n";
                    case 47: // Time Stop
                        return this.name + "�F�X�g�b�v�I �G�̎���������􂫎��Ԓ�~�������B\r\n";
                    case 48: // Dispel Magic
                        return this.name + "�F�f�B�X�y���I \r\n";
                    case 49: // Rise of Image
                        return this.name + "�F���C�Y�I �y�S�z�� {0} �グ�Ƃ����B\r\n";
                    case 50: // ��r��
                        return this.name + "�F�b�J�E�E�E�r�����Ă˂����E�E�E\r\n";
                    case 51: // Inner Inspiration
                        return this.name + "�F�͐��ݏW���͂����߂��B���_���������܂����B {0} �X�L���|�C���g��\r\n";
                    case 52: // Resurrection 1
                        return this.name + "�F���U���N�I\r\n";
                    case 53: // Resurrection 2
                        return this.name + "�F���H�P���I�Ώۂ������I�I\r\n";
                    case 54: // Resurrection 3
                        return this.name + "�F���H�P���I����ł˂��I�I\r\n";
                    case 55: // Resurrection 4
                        return this.name + "�F���H�P����T�ɂ���B\r\n";
                    case 56: // Stance Of Standing
                        return this.name + "�F�b�t���I\r\n";
                    case 57: // Mirror Image
                        return this.name + "�F�~���C���I {0}�̎��͂ɐ���Ԃ����������B\r\n";
                    case 58: // Mirror Image 2
                        return this.name + "�F�Ԃ����I���@�I {0} �_���[�W�� {1} �ɔ��˂����I\r\n";
                    case 59: // Mirror Image 3
                        return this.name + "�F�Ԃ����I���@�I �y�������A���͂ȈЗ͂ł͂˕Ԃ��Ȃ��I�z" + this.name + "�F�b�P�A�N�\��\r\n";
                    case 60: // Deflection
                        return this.name + "�F�f�t���N�I {0}�̎��͂ɔ�����Ԃ����������B\r\n";
                    case 61: // Deflection 2
                        return this.name + "�F�Ԃ����I���@�I {0} �_���[�W�� {1} �ɔ��˂����I\r\n";
                    case 62: // Deflection 3
                        return this.name + "�F�Ԃ����I���@�I �y�������A���͂ȈЗ͂ł͂˕Ԃ��Ȃ��I�z" + this.name + "�F�b�P�A�N�\��\r\n";
                    case 63: // Truth Vision
                        return this.name + "�F�g���D�X�I�@" + this.name + "�͑Ώۂ̃p�����^�t�o�𖳎�����悤�ɂȂ����B\r\n";
                    case 64: // Stance Of Flow
                        return this.name + "�F�X�^���t���E�I�@" + this.name + "�͎��R�^�[���A�K����U�����悤�ɍ\�����B\r\n";
                    case 65: // Carnage Rush 1
                        return this.name + "�F�I���@�I {0}�_���[�W�E�E�E   ";
                    case 66: // Carnage Rush 2
                        return "�b���@�I {0}�_���[�W   ";
                    case 67: // Carnage Rush 3
                        return "�b���A�@�I {0}�_���[�W   ";
                    case 68: // Carnage Rush 4
                        return "�b���A�@�@�I {0}�_���[�W   ";
                    case 69: // Carnage Rush 5
                        return "�E�H���A�@�@�@�I�I {0}�̃_���[�W\r\n";
                    case 70: // Crushing Blow
                        return this.name + "�F�N���b�V���I  {0} �� {1} �̃_���[�W�B\r\n";
                    case 71: // ���[�x�X�g�����N�|�[�V�����퓬�g�p��
                        return this.name + "�F�R�C�c�ł�����łȁB\r\n";
                    case 72: // Enigma Sence
                        return this.name + "�F�G�j�O�}�I\r\n";
                    case 73: // Soul Infinity
                        return this.name + "�F�u�b�b�b�b�^�a��A���@�@�@�I�I�I\r\n";
                    case 74: // Kinetic Smash
                        return this.name + "�F�X�}�@�b�V���I�I\r\n";
                    case 75: // Silence Shot (Altomo��p)
                        return "";
                    case 76: // Silence Shot�AAbsoluteZero���قɂ��r�����s
                        return this.name + "�F�E�E�E�b�P�A�r���W�Q���A������˂��B\r\n";
                    case 77: // Cleansing
                        return this.name + "�F�N���[���I\r\n";
                    case 78: // Pure Purification
                        return this.name + "�F�s�����t�@�C�I\r\n";
                    case 79: // Void Extraction
                        return this.name + "�F���H�C�f�N�X�I" + this.name + "�� {0} �� {1}�t�o�I\r\n";
                    case 80: // �A�J�V�W�A�̎��g�p��
                        return this.name + "�F�����͖ڂ���܂��B\r\n";
                    case 81: // Absolute Zero
                        return this.name + "�F�A�u�X�[���I �X������ʂɂ��A���C�t�񕜕s�A�X�y���r���s�A�X�L���g�p�s�A�h��s�ƂȂ����B\r\n";
                    case 82: // BUFFUP���ʂ��]�߂Ȃ��ꍇ
                        return "�������A���ɂ��̃p�����^�͏㏸���Ă��邽�߁A���ʂ��Ȃ������B\r\n";
                    case 83: // Promised Knowledge
                        return this.name + "�F�v���i���I�@�y�m�z�� {0} �グ�Ƃ����B\r\n";
                    case 84: // Tranquility
                        return this.name + "�F�g�����L�B�I\r\n";
                    case 85: // High Emotionality 1
                        return this.name + "�F�n�C�G���I�@�����������I�I\r\n";
                    case 86: // High Emotionality 2
                        return this.name + "�̗́A�Z�A�m�A�S�p�����^���t�o�����I\r\n";
                    case 87: // AbsoluteZero�ŃX�L���g�p���s
                        return this.name + "�F�b�P�E�E�E�g���˂��Ȃ��B\r\n";
                    case 88: // AbsoluteZero�ɂ��h�䎸�s
                        return this.name + "�F�b�J�E�E�E�h��ł��˂��Ƃ͂ȁB \r\n";
                    case 89: // Silent Rush 1
                        return this.name + "�F���b�V���I�I {0}�_���[�W�E�E�E   ";
                    case 90: // Silent Rush 2
                        return "�b�t���I {0}�_���[�W   ";
                    case 91: // Silent Rush 3
                        return "�b���A�@�@�I {0}�̃_���[�W\r\n";
                    case 92: // BUFFUP�ȊO�̉i�����ʂ����ɂ��Ă���ꍇ
                        return "�������A���ɂ��̌��ʂ͕t�^����Ă���B\r\n";
                    case 93: // Anti Stun
                        return this.name + "�F�A���X�^�I " + this.name + "�̓X�^�����ʂւ̑ϐ����t����\r\n";
                    case 94: // Anti Stun�ɂ��X�^�����
                        return this.name + "�F�X�^��������炢�Ƃ����񂾁B\r\n";
                    case 95: // Stance Of Death
                        return this.name + "�F�X�^���f�X�I " + this.name + "�͒v�����P�x����ł���悤�ɂȂ���\r\n";
                    case 96: // Oboro Impact 1
                        return this.name + "�F�I�{���s�����E�E�E�b�t�E�E�E�D�D�D�D�E�E�E\r\n";
                    case 97: // Oboro Impact 2
                        return this.name + "�F�E�I�H�H���A�A�@�I�I�@ {0}��{1}�̃_���[�W\r\n";
                    case 98: // Catastrophe 1
                        return this.name + "�F�������R�b�p�~�W�����A�J�^�X�g�I\r\n";
                    case 99: // Catastrophe 2
                        return this.name + "�F����ł����₠�������I�I�@ {0}�̃_���[�W\r\n";
                    case 100: // Stance Of Eyes
                        return this.name + "�F�X�^���A�C�I " + this.name + "�́A����̍s���ɔ����Ă���E�E�E\r\n";
                    case 101: // Stance Of Eyes�ɂ��L�����Z����
                        return this.name + "�F�������ȁI�I�@" + this.name + "�͑���̃��[�V���������؂��āA�s���L�����Z�������I\r\n";
                    case 102: // Stance Of Eyes�ɂ��L�����Z�����s��
                        return this.name + "�F�b�P�E�E�E�@" + this.name + "�͑���̃��[�V���������؂�Ȃ������I\r\n";
                    case 103: // Negate
                        return this.name + "�F�j�Q�C�g�I�@" + this.name + "�͑���̃X�y���r���ɔ����Ă���E�E�E\r\n";
                    case 104: // Negate�ɂ��X�y���r���L�����Z����
                        return this.name + "�F���������I" + this.name + "�͑���̃X�y���r����e�����I\r\n";
                    case 105: // Negate�ɂ��X�y���r���L�����Z�����s��
                        return this.name + "�F�b�J�E�E�E" + this.name + "�͑���̃X�y���r�������؂�Ȃ������I\r\n";
                    case 106: // Nothing Of Nothingness 1
                        return this.name + "�F�i�b�V���O�I " + this.name + "�ɖ��F�̃I�[�����h��n�߂�I \r\n";
                    case 107: // Nothing Of Nothingness 2
                        return this.name + "�͖��������@�𖳌����A�@����������X�L���𖳌�������悤�ɂȂ����I\r\n";
                    case 108: // Genesis
                        return this.name + "�F�W�F�l�I  " + this.name + "�͑O��̍s���������ւƓ��e�������I\r\n";
                    case 109: // Cleansing�r�����s��
                        return this.name + "�F�N���[���W���O�~�X���B\r\n";
                    case 110: // CounterAttack�𖳎�������
                        return this.Name + "�F���ʂ��ȁB\r\n";
                    case 111: // �_�����g�p��
                        return this.name + "�F���C�t�E�X�L���E�}�i��30%���񕜂��Ƃ������B\r\n";
                    case 112: // �_�����A���Ɏg�p�ς݂̏ꍇ
                        return this.name + "�F����P�񂾁B\r\n";
                    case 113: // CounterAttack�ɂ�锽�����b�Z�[�W
                        return this.name + "�F�������I�J�E���^�[�I\r\n";
                    case 114: // CounterAttack�ɑ΂��锽����NothingOfNothingness�ɂ���Ėh���ꂽ��
                        return this.name + "�F�b�J�E�E�E�b�X�������E�E�E\r\n";
                    case 115: // �ʏ�U���̃q�b�g
                        return this.name + "�̍U�����q�b�g�B {0} �� {1} �̃_���[�W\r\n";
                    case 116: // �h��𖳎����čU�����鎞
                        return this.name + "�F���ʂ��ȁB\r\n";
                    case 117: // StraightSmash�Ȃǂ̃X�L���N���e�B�J��
                        return this.name + "�F�b�V�����@�I�N���e�B�J���I\r\n";
                    case 118: // �퓬���A�����@�C���|�[�V�����ɂ�镜���̂�����
                        return this.name + "�F���������邼�B\r\n";
                    case 119: // Absolute Zero�ɂ�胉�C�t�񕜂ł��Ȃ��ꍇ
                        return this.name + "�F�b�P�E�E�E�񕜂����ʂ�������\r\n";
                    case 120: // ���@�U���̃q�b�g
                        return "{0} �� {1} �̃_���[�W\r\n";
                    case 121: // Absolute Zero�ɂ��}�i�񕜂ł��Ȃ��ꍇ
                        return this.name + "�F�b�J�E�E�E�}�i�񕜂����ʂ�������\r\n";
                    case 122: // �u���߂�v�s����
                        return this.name + "�F���́A�~���邼�B\r\n";
                    case 123: // �u���߂�v�s���ŗ��܂肫���Ă���ꍇ
                        return this.name + "�F�~�Ϗ�����B\r\n";
                    case 124: // StraightSmash�̃_���[�W�l
                        return this.name + "�̃X�g���[�g�E�X�}�b�V�����q�b�g�B {0} �� {1}�̃_���[�W\r\n";
                    case 125: // �A�C�e���g�p�Q�[�W�����܂��ĂȂ��Ƃ�
                        return this.name + "�F�A�C�e���Q�[�W���܂����B\r\n";
                    case 126: // FlashBlase
                        return this.name + "�F�u���C�Y�I {0} �� {1} �̃_���[�W\r\n";
                    case 127: // �������@�ŃC���X�^���g�s��
                        return this.name + "�F���H�P���A�C���X�^���g����˂����낧���I\r\n";
                    case 128: // �������@�̓C���X�^���g�^�C�~���O�ł��ĂȂ�
                        return this.name + "�F�R�C�c�̓C���X�^���g���B\r\n";
                    case 129: // PsychicTrance
                        return this.name + "�F�T�C�g���I�@���@�U���������B\r\n";
                    case 130: // BlindJustice
                        return this.name + "�F�W���X�e�B�X�I�@�����U���������B\r\n";
                    case 131: // TranscendentWish
                        return this.name + "�F�g���b�Z���I�@\r\n";
                    case 132: // LightDetonator
                        return this.name + "�F�f�g�l�C�g�I\r\n";
                    case 133: // AscendantMeteor
                        return this.name + "�F�b���A�A�@�@�I�I�@���e�I�I�I\r\n";
                    case 134: // SkyShield
                        return this.name + "�F�V�[���h�I\r\n";
                    case 135: // SacredHeal
                        return this.name + "�F�T�[�N���b�h�I\r\n";
                    case 136: // EverDroplet
                        return this.name + "�F�G���@�h���[�I\r\n";
                    case 137: // FrozenAura
                        return this.name + "�F�A�C�X�I�[���I\r\n";
                    case 138: // ChillBurn
                        return this.name + "�F�H�炦�A�b�o�[���I\r\n";
                    case 139: // ZetaExplosion
                        return this.name + "�F�[�[�^�I\r\n";
                    case 140: // FrozenAura�ǉ����ʃ_���[�W��
                        return this.name + "�F�����I {0}�̒ǉ��_���[�W\r\n";
                    case 141: // StarLightning
                        return this.name + "�F���C�g�j���O�I\r\n";
                    case 142: // WordOfMalice
                        return this.name + "�F���[�}���X�I\r\n";
                    case 143: // BlackFire
                        return this.name + "�F�u���b�N�t�@�C�A�I\r\n";
                    case 144: // EnrageBlast
                        return this.name + "�F�G�����C�W�I\r\n";
                    case 145: // Immolate
                        return this.name + "�F�C�����I\r\n";
                    case 146: // VanishWave
                        return this.name + "�F���@�j�b�V���I\r\n";
                    case 147: // WordOfAttitude
                        return this.name + "�F���[�A�b�e�B�I\r\n";
                    case 148: // HolyBreaker
                        return this.name + "�F�u���C�J�[�I\r\n";
                    case 149: // DarkenField
                        return this.name + "�F�_�[�P���I\r\n";
                    case 150: // SeventhMagic
                        return this.name + "�F�Z���F���X�I\r\n";
                    case 151: // BlueBullet
                        return this.name + "�F�u���o���I\r\n";
                    case 152: // NeutralSmash
                        return this.name + "�F�b�V���@�I�X�}�b�V���I\r\n";
                    case 153: // SwiftStep
                        return this.name + "�F�X�E�B�t�g�I\r\n";
                    case 154: // CircleSlash
                        return this.name + "�F�T�[�N���I\r\n";
                    case 155: // RumbleShout
                        return this.name + "�F�R�b�`���A�I���@�I\r\n";
                    case 156: // SmoothingMove
                        return this.name + "�F�X���[�W���I\r\n";
                    case 157: // FutureVision
                        return this.name + "�F�t���[�`���[���B�I\r\n";
                    case 158: // ReflexSpirit
                        return this.name + "�F���t���I\r\n";
                    case 159: // SharpGlare
                        return this.name + "�F�V���[�O���I\r\n";
                    case 160: // TrustSilence
                        return this.name + "�F�g���b�T�C�����I\r\n";
                    case 161: // SurpriseAttack
                        return this.name + "�F�b�v���C�Y�I\r\n";
                    case 162: // PsychicWave
                        return this.name + "�F�T�C�L�b�N�I\r\n";
                    case 163: // Recover
                        return this.name + "�F���J�o�I\r\n";
                    case 164: // ViolentSlash
                        return this.name + "�F�b���@�C�I�����I\r\n";
                    case 165: // OuterInspiration
                        return this.name + "�F�A�E�X�s�I\r\n";
                    case 166: // StanceOfSuddenness
                        return this.name + "�F�b�T�h���I\r\n";
                    case 167: // �C���X�^���g�ΏۂŔ����s��
                        return this.name + "�F�R�C�c�̓C���X�^���g�Ώې�p���B\r\n";
                    case 168: // StanceOfMystic
                        return this.name + "�F�~�X�e�B�b�I\r\n";
                    case 169: // HardestParry
                        return this.name + "�F�b�J�A�ʓ|�������A�n�[�h�p���B�I\r\n";
                    case 170: // ConcussiveHit
                        return this.name + "�F�J�b�V���I\r\n";
                    case 171: // Onslaught hit
                        return this.name + "�F�I���b�X���I";
                    case 172: // Impulse hit
                        return this.name + "�F�p���X�I";
                    case 173: // Fatal Blow
                        return this.name + "�F�t�F�^���I";
                    case 174: // Exalted Field
                        return this.name + "�F�b�n�@�A�����ȁI�@�C�O�b�U���c�I\r\n";
                    case 175: // Rising Aura
                        return this.name + "�F���C�W���b�I\r\n";
                    case 176: // Ascension Aura
                        return this.name + "�F�Z�b�V�����I\r\n";
                    case 177: // Angel Breath
                        return this.name + "�F�b�J�A�ʓ|�������A�u���X�I\r\n";
                    case 178: // Blazing Field
                        return this.name + "�F���C�W���E�t�B�[�I\r\n";
                    case 179: // Deep Mirror
                        return this.name + "�F�f�B�v�~���I\r\n";
                    case 180: // Abyss Eye
                        return this.name + "�F�A�r�b�X�@�I\r\n";
                    case 181: // Doom Blade
                        return this.name + "�F�h�D�[���E���C�I\r\n";
                    case 182: // Piercing Flame
                        return this.name + "�F�s�A�b�E�t���C�I\r\n";
                    case 183: // Phantasmal Wind
                        return this.name + "�F�t�@���^�Y�}�I\r\n";
                    case 184: // Paradox Image
                        return this.name + "�F�h�b�N�X�I\r\n";
                    case 185: // Vortex Field
                        return this.name + "�F�e�b�N�X�E�t�B�[���I\r\n";
                    case 186: // Static Barrier
                        return this.name + "�F�X�^�b�N�E�o���A�I\r\n";
                    case 187: // Unknown Shock
                        return this.name + "�F�A���E�V���b�N�I\r\n";
                    case 188: // SoulExecution
                        return this.name + "�F�b�n�[�[�b�n�b�n�b�n�@�I�@�\�E���E�G�O�[�b�I�I�I\r\n";
                    case 189: // SoulExecution hit 01
                        return this.name + "�F���@�I\r\n";
                    case 190: // SoulExecution hit 02
                        return this.name + "�F�b���@�I\r\n";
                    case 191: // SoulExecution hit 03
                        return this.name + "�F�I���@�I\r\n";
                    case 192: // SoulExecution hit 04
                        return this.name + "�F�I���A�@�I\r\n";
                    case 193: // SoulExecution hit 05
                        return this.name + "�F�b���I\r\n";
                    case 194: // SoulExecution hit 06
                        return this.name + "�F�b�����I\r\n";
                    case 195: // SoulExecution hit 07
                        return this.name + "�F�b�����A�@�@�I\r\n";
                    case 196: // SoulExecution hit 08
                        return this.name + "�F�I�������A�A�@�I\r\n";
                    case 197: // SoulExecution hit 09
                        return this.name + "�F�I���I���I���I\r\n";
                    case 198: // SoulExecution hit 10
                        return this.name + "�F�H�炦�₠�I���A�A�@�@�I�I�I\r\n";
                    case 199: // Nourish Sense
                        return this.name + "�F�m�b�Z���X�I\r\n";
                    case 200: // Mind Killing
                        return this.name + "�F�}�C���E�b�L���I\r\n";
                    case 201: // Vigor Sense
                        return this.name + "�F���B�S�[�I\r\n";
                    case 202: // ONE Authority
                        return this.name + "�F�����E�I�[�X�I\r\n";
                    case 203: // �W���ƒf��
                        return this.name + "�F�y�W���ƒf��z�@�����B\r\n";
                    case 204: // �y���j�z�����ς�
                        return this.name + "�F�b�P�A����ȉ��x���������Ă��˂����B\r\n";
                    case 205: // �y���j�z�ʏ�s���I����
                        return this.name + "�F��Ȃ́A���ł���������B\r\n";
                    case 206: // Sigil Of Homura
                        return this.name + "�F�V�M�B���I\r\n";
                    case 207: // Austerity Matrix
                        return this.name + "�F�A�D�X�E�g�D���b�N�X�I\r\n";
                    case 208: // Red Dragon Will
                        return this.name + "�F���b�h���@�I\r\n";
                    case 209: // Blue Dragon Will
                        return this.name + "�F�u���[�h���@�I\r\n";
                    case 210: // Eclipse End
                        return this.name + "�F�C�N�X�E�G���b�I\r\n";
                    case 211: // Sin Fortune
                        return this.name + "�F�b�V���I\r\n";
                    case 212: // AfterReviveHalf
                        return this.name + "�F�f�b�h�E���W�E�n�[�t�I\r\n";
                    case 213: // Demonic Ignite
                        return this.name + "�F�f�B�[���b�I\r\n";
                    case 214: // Death Deny
                        return this.name + "�F�b�f�B�i�I\r\n";
                    case 215: // Stance of Double
                        return this.name + "�F�X�^���b�_�u���I  " + this.name + "�͑O��s�������������̕��g�𔭐��������I\r\n";
                    case 216: // �ŏI�탉�C�t�J�E���g����
                        return this.name + "�F�b�n�A���̒��x�ł����΂�Ǝv���Ȃ�B\r\n";
                    case 217: // �ŏI�탉�C�t�J�E���g���Ŏ�
                        return this.name + "�F�E�E�E�@�b�E�E�E\r\n";

                    case 2001: // �|�[�V�����܂��͖��@�ɂ��񕜎�
                        return this.name + "�F{0} �񕜂��Ƃ������B";
                    case 2002: // ���x���A�b�v�I���Ñ�
                        return this.name + "�F���x���A�b�v������B";
                    case 2003: // �ו������点��Ñ�
                        return this.name + "�F{0}�A�ו������点�B�A�C�e����n������B";
                    case 2004: // �������f
                        return this.name + "�F�������ȁH";
                    case 2005: // ��������
                        return this.name + "�F�����������B";
                    case 2006: // �����̐������g�p
                        return this.name + "�F���߂邩�H";
                    case 2007: // ���p��p�i�ɑ΂���ꌾ
                        return this.name + "�F���p��p�i���B";
                    case 2008: // ��퓬���̃}�i�s��
                        return this.name + "�F�}�i�s�����B";
                    case 2009: // �_�����g�p��
                        return this.name + "�F���C�t�E�X�L���E�}�i��30%���񕜂��Ƃ������B";
                    case 2010: // �_�����A���Ɏg�p�ς݂̏ꍇ
                        return this.name + "�F�����͂�Ă�B����P�񂾁B";
                    case 2011: // ���[�x�X�g�����N�|�[�V������퓬�g�p��
                        return this.name + "�F�퓬����p�i���B";
                    case 2012: // �����̐������g�p�i���؍ݎ��j
                        return this.name + "�F���񒆂��ᖳ�ʂ��B";
                    case 2013: // �����̐������̂Ă��Ȃ����̑䎌
                        return this.name + "�F�R�C�c�͎̂Ă�˂��B";
                    case 2014: // ��퓬���̃X�L���s��
                        return this.name + "�F�X�L���s�����B";
                    case 2015: // ���v���C���[���̂ĂĂ��܂����Ƃ����ꍇ
                        return this.name + "�F�b�I�C�A�\�C�c�͎̂Ă�ȁB";
                    case 2016: // �����@�C���|�[�V�����ɂ�镜��
                        return this.name + "�F�����I";
                    case 2017: // �����@�C���|�[�V�����s�v�Ȏ�
                        return this.name + "�F{0}�ɃR�C�c�͕s�v���B";
                    case 2018: // �����@�C���|�[�V�����Ώۂ������̎�
                        return this.name + "�F���H�P����T�ɂ���B";
                    case 2019: // �����s�̃A�C�e���𑕔����悤�Ƃ�����
                        return this.name + "�F��Ȃ���A����������ȁB";
                    case 2020: // ���X�{�X���j�̌�A�����̐������g�p�s��
                        return this.name + "�F���͂���Ȏ�����˂��B";
                    case 2021: // �A�C�e���̂Ă�̍Ñ�
                        return this.name + "�F�o�b�N�p�b�N�������炢����B";
                    case 2022: // �I�[�o�[�V�t�e�B���O�g�p�J�n��
                        return this.name + "�F�p�����^�č\�z�����Ă��炤���B";
                    case 2023: // �I�[�o�[�V�t�e�B���O�ɂ��p�����^����U�莞
                        return this.name + "�F�I�[�o�[�V�t�e�B���O����U�肪��ɂ�����B";
                    case 2024: // �������L�b�h���g�p������
                        return this.name + "�F�y{0}�z�p�����^ {1} �㏸�B";
                    case 2025:
                        return this.name + "�F���蕐��Ȃ�łȁA�T�u�͑����ł��˂��B";
                    case 2026:
                        return this.name + "�F����i���C���j�ɂ܂��������B";
                    case 2027: // �������g�p��
                        return this.name + "�F���C�t100%�񕜂��Ƃ������B";
                    case 2028: // �������A���Ɏg�p�ς݂̏ꍇ
                        return this.name + "�F�����͂�Ă��܂��Ă�ȁB����P�񂾁B";
                    case 2029: // �ו���t�ő������O���Ȃ���
                        return this.name + "�F�o�b�N�p�b�N���t���B�����O���O�ɐ������Ƃ��B";
                    case 2030: // �ו���t�ŉ������̂Ă鎞�̑䎌
                        return this.name + "�F{0}�̂ĂĂ��̃A�C�e���A���肩�H";
                    case 2031: // �퓬���̃A�C�e���g�p����
                        return this.name + "�F�퓬�����B�A�C�e���g�p�ɏW������B";
                    case 2032: // �퓬���A�A�C�e���g�p�ł��Ȃ��A�C�e����I�������Ƃ�
                        return this.name + "�F���̃A�C�e���͐퓬���͎g���˂��B";
                    case 2033: // �a�����Ȃ��ꍇ
                        return this.name + "�F�����A���ŗa���Ȃ���Ȃ�˂��񂾂�B";
                    case 2034: // �A�C�e�������ς��ŗa���菊��������o���Ȃ��ꍇ
                        return this.name + "�F����ȏ�͖ʓ|�������B";
                    case 2035: // Sacred Heal
                        return this.name + "�F�S���񕜂������B";

                    case 3000: // �X�ɓ��������̑䎌
                        return this.name + "�F�W�W�B�E�E�E�N�����˂��̂͂�ׂ����낪�E�E�E�B";
                    case 3001: // �x�����v����
                        return this.name + "�F {0} �𔃂��B {1} Gold�������H";
                    case 3002: // �����������ς��Ŕ����Ȃ���
                        return this.name + "�F�������������ς�����˂����B�A�C�e���������炢���Ƃ��B";
                    case 3003: // �w��������
                        return this.name + "�F�W�W�B�A�����������B";
                    case 3004: // Gold�s���ōw���ł��Ȃ��ꍇ
                        return this.name + "�FGold��{0}����˂��ȁB";
                    case 3005: // �w�������L�����Z�������ꍇ
                        return this.name + "�F���̃A�C�e����������B";
                    case 3006: // ����Ȃ��A�C�e���𔄂낤�Ƃ����ꍇ
                        return this.name + "�F����𔄂�C�͂˂��B";
                    case 3007: // �A�C�e�����p��
                        return this.name + "�F{0}�Ŕ��邺�B{1}Gold������Ă����A�W�W�B�B";
                    case 3008: // ����̓y���_���g���p��
                        return this.name + "�F�}�W�Ŕ���̂��A{0}Gold�����H";
                    case 3009: // ����X���o�鎞
                        return this.name + "�F�W�W�B�A�������Ɍ�����t����E�E�E";
                    case 3010: // �K���c�s�ݎ��̔��肫��t�F���g�D�[�V�������Ĉꌾ
                        return this.name + "�F�b�J�E�E�E�U�R�A�C�����C�t�����ǂ������ȁB";
                    case 3011: // �����\�Ȃ��̂��w�����ꂽ��
                        return this.name + "�F�������邼�H";
                    case 3012: // �������Ă������𔄋p�Ώۂ��ǂ���������
                        return this.name + "�F���̑����́A{0}���B���̃W�W�B�Ȃ�{1}Gold�Ŕ��������ăg�R���B";

                    case 4001: // �ʏ�U����I��
                        return this.name + "�F�U�߂邺�B\r\n";
                    case 4002: // �h���I��
                        return this.name + "�F�h�䂳����B\r\n";
                    case 4003: // �ҋ@��I��
                        return this.name + "�F�������˂����B\r\n";
                    case 4004: // �t���b�V���q�[����I��
                    case 4005: // �v���e�N�V������I��
                    case 4006: // �t�@�C�A�E�{�[����I��
                    case 4007: // �t���C���E�I�[����I��
                    case 4008: // �X�g���[�g�E�X�}�b�V����I��
                    case 4009: // �_�u���E�X�}�b�V����I��
                    case 4010: // �X�^���X�E�I�u�E�X�^���f�B���O��I��
                    case 4011: // �A�C�X�E�j�[�h����I��
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
                        return this.name + "�F" + this.ActionLabel.Text + "���B\r\n";

                    case 4072:
                        return this.name + "�F�G�ɂ�����˂��B\r\n";
                    case 4073:
                        return this.name + "�F�G�ɂ�����˂��B\r\n";
                    case 4074:
                        return this.name + "�F�����ɂ͂�����˂��B\r\n";
                    case 4075:
                        return this.name + "�F�����ɂ͂�����˂��B\r\n";
                    case 4076:
                        return this.name + "�F�����ɍU���͂ł��˂��B\r\n";
                    case 4077: // �u���߂�v�R�}���h
                        return this.name + "�F���߂邼�B\r\n";
                    case 4078: // ���픭���u���C���v
                        return this.name + "�F���C�����������邼�B\r\n";
                    case 4079: // ���픭���u�T�u�v
                        return this.name + "�F�T�u���������邼�B\r\n";
                    case 4080: // �A�N�Z�T���P����
                        return this.name + "�F�A�N�Z�P���������邼�B\r\n";
                    case 4081: // �A�N�Z�T���Q����
                        return this.name + "�F�A�N�Z�Q���������邼�B\r\n";

                    // ����U��
                    case 5001:
                        return this.name + "�F�G�A���I {0} �� {1} �̃_���[�W\r\n";
                    case 5002:
                        return this.name + "�F{0} �񕜂��Ƃ������B \r\n";
                    case 5003:
                        return this.name + "�F{0} �}�i�񕜂��Ƃ������B\r\n";
                    case 5004:
                        return this.name + "�F�A�C�V�N���I {0} �� {1} �̃_���[�W\r\n";
                    case 5005:
                        return this.name + "�F�G���N�g���I {0} �� {1} �̃_���[�W\r\n";
                    case 5006:
                        return this.name + "�F�u���[���C�g�I {0} �� {1} �̃_���[�W\r\n";
                    case 5007:
                        return this.name + "�F�o�[�j���I {0} �� {1} �̃_���[�W\r\n";
                    case 5008:
                        return this.name + "�F�u���[�t�@�C�A�I {0} �� {1} �̃_���[�W\r\n";
                    case 5009:
                        return this.name + "�F{0} �X�L���|�C���g�񕜂��Ƃ������B\r\n";
                    case 5010:
                        return this.name + "���������Ă���w�ւ�����ɑ�������������I {0} �� {1} �̃_���[�W\r\n";
                }
            }
            #endregion
            #region "�J�[���n���c"
            if (this.name == Database.SINIKIA_KAHLHANZ)
            {
                switch (sentenceNumber)
                {
                    case 0: // �X�L���s��
                        return this.name + "�F�b�t�E�E�E�X�L���s�����B\r\n";
                    case 1: // Straight Smash
                        return this.name + "�F�X�g���[�g�E�X�}�b�V����H�炦���I\r\n";
                    case 2: // Double Slash 1
                        return this.name + "�F���ނ���I�I\r\n";
                    case 3: // Double Slash 2
                        return this.name + "�F���ނ������I\r\n";
                    case 4: // Painful Insanity
                        return this.name + "�F�y�S�ቜ�`�z�y�C���t���E�C���T�j�e�B��H�炦���I\r\n";
                    case 5: // empty skill
                        return this.name + "�F�b�t�E�E�E�X�L���̑I����Y��Ă�������B\r\n";
                    case 6: // ���݂��t�����V�X�̕K�E��H�������
                        return this.name + "�F�b�E�E�E\r\n";
                    case 7: // Lizenos�̕K�E��H�������
                        return this.name + ": �b�E�E�E\r\n";
                    case 8: // Minflore�̕K�E��H�������
                        return this.name + "�F�b�I�E�E�E\r\n";
                    case 9: // Fresh Heal�ɂ�郉�C�t��
                        return this.name + "�F{0} �񕜂����Ă���������B\r\n";
                    case 10: // Fire Ball
                        return this.name + "�F�H�炦���I�t�@�C���[�E�{�[���I {0} �� {1} �̃_���[�W\r\n";
                    case 11: // Flame Strike
                        return this.name + "�F���ނ���I�t���[���E�X�g���C�N�I {0} �� {1} �̃_���[�W\r\n";
                    case 12: // Volcanic Wave
                        return this.name + "�F�ʂ������I�{���J�j�b�N�E�E�F�[�u�I {0} �� {1} �̃_���[�W\r\n";
                    case 13: // �ʏ�U���N���e�B�J���q�b�g
                        return this.name + "�F���������A�N���e�B�J���_���[�W�I {0} �� {1}�̃_���[�W\r\n";
                    case 14: // FlameAura�ɂ��ǉ��U��
                        return this.name + "�F�x�ꉊ��I {0}�̒ǉ��_���[�W\r\n";
                    case 15: //�����̐�����퓬���Ɏg�����Ƃ�
                        return this.name + "�F�b�t�E�E�E�퓬���Ɏg���͂����Ȃ��낤�B\r\n";
                    case 16: // ���ʂ𔭊����Ȃ��A�C�e����퓬���Ɏg�����Ƃ�
                        return this.name + "�F�b�t�E�E�E�g����A�C�e���������ȁB\r\n";
                    case 17: // ���@�Ń}�i�s��
                        return this.name + "�F�b�t�E�E�E�}�i�s���Ƃ́B\r\n";
                    case 18: // Protection
                        return this.name + "�F���̉���A�v���e�N�V�����I�����h��́F�t�o\r\n";
                    case 19: // Absorb Water
                        return this.name + "�F���̉���A�A�u�\�[�u�E�E�I�[�^�[�I ���@�h��́F�t�o�B\r\n";
                    case 20: // Saint Power
                        return this.name + "�F���Ȃ�́A�Z�C���g�E�p���[�I �����U���́F�t�o\r\n";
                    case 21: // Shadow Pact
                        return this.name + "�F�ł̌_��A�V���h�[�E�p�N�g�I ���@�U���́F�t�o\r\n";
                    case 22: // Bloody Vengeance
                        return this.name + "�F���Q�̗́A�u���b�f�[�E�x���W�A���X�I �̓p�����^�� {0} �t�o\r\n";
                    case 23: // Holy Shock
                        return this.name + "�F���Ȃ�Ռ��A�z�[���[�E�V���b�N�I {0} �� {1} �̃_���[�W\r\n";
                    case 24: // Glory
                        return this.name + "�F�h���A�O���[���[�I ���ڍU���{FreshHeal�A�g�̃I�[��\r\n";
                    case 25: // CelestialNova 1
                        return this.name + "�F�����̋P���A�Z���X�e�A���E�m�o�I {0} �񕜂����Ă���������B\r\n";
                    case 26: // CelestialNova 2
                        return this.name + "�F�����ւ̍ق��A�Z���X�e�A���E�m�o�I {0} �̃_���[�W\r\n";
                    case 27: // Dark Blast
                        return this.name + "�F�ł̏Ռ��A�_�[�N�E�u���X�g�I {0} �� {1} �̃_���[�W\r\n";
                    case 28: // Lava Annihilation
                        return this.name + "�F���̓V�g��I�Ă��������I���o�E�A�j�q���[�V�����I {0} �� {1} �̃_���[�W\r\n";
                    case 10028: // Lava Annihilation���
                        return this.name + "�F���̓V�g��I�Ă��������I���o�E�A�j�q���[�V�����I\r\n";
                    case 29: // Devouring Plague
                        return this.name + "�F�����z�������߂�I�f�{�[�����O�E�v���O�[�I {0} ���C�t���z�������\r\n";
                    case 30: // Ice Needle
                        return this.name + "�F�X�̐n�A�A�C�X�E�j�[�h���I {0} �� {1} �̃_���[�W\r\n";
                    case 31: // Frozen Lance
                        return this.name + "�F�X�����A�t���[�Y���E�����X�I {0} �� {1} �̃_���[�W\r\n";
                    case 32: // Tidal Wave
                        return this.name + "�F���ނ������I�^�C�_���E�E�F�C�u�I {0} �̃_���[�W\r\n";
                    case 33: // Word of Power
                        return this.name + "�F�͂̏Քg�A���[�h�E�I�u�E�p���[�I {0} �� {1} �̃_���[�W\r\n";
                    case 34: // White Out
                        return this.name + "�F���ɋA����A�z���C�g�E�A�E�g�I {0} �� {1} �̃_���[�W\r\n";
                    case 35: // Black Contract
                        return this.name + "�F�_��̗͂�����A�u���b�N�E�R���g���N�g�I " + this.name + "�̃X�L���A���@�R�X�g�͂O�ɂȂ�B\r\n";
                    case 36: // Flame Aura�r��
                        return this.name + "�F����������I�t���C���E�I�[���I ���ڍU���ɉ��̒ǉ����ʂ��t�^�����B\r\n";
                    case 37: // Damnation
                        return this.name + "�F�łтȂ�A�_���l�[�V�����I �������o�ł鍕����Ԃ�c�܂���B\r\n";
                    case 38: // Heat Boost
                        return this.name + "�F���ނ���I�q�[�g�E�u�[�X�g�I �Z�p�����^�� {0} �t�o�B\r\n";
                    case 39: // Immortal Rave
                        return this.name + "�F���ɏh�肵�́I�C���[�^���E���C�u�I " + this.name + "�̎���ɂR�̉����h�����B\r\n";
                    case 40: // Gale Wind
                        return this.name + "�F�䂪���g�A�����I�Q�[���E�E�C���h�I ������l��" + this.name + "�����ꂽ�B\r\n";



                }
            }
            #endregion
            #region "�K���c"
            else if (this.name == "�K���c")
            {
                switch (sentenceNumber)
                {
                    case 3000: // �X�ɓ��������̑䎌
                        return this.name + "�F������茩�Ă����������B";
                    case 3001: // �x�����v����
                        return this.name + "�F{0}��{1}Gold���B�������ˁH";
                    case 3002: // �����������ς��Ŕ����Ȃ���
                        return this.name + "�F����A�ו��������ς�����B�莝�������炵�Ă���܂����Ȃ����B";
                    case 3003: // �w��������
                        return this.name + "�F�͂���A�܂��ǂ���B";
                    case 3004: // Gold�s���ōw���ł��Ȃ��ꍇ
                        return this.name + "�F���܂Ȃ��A������������łȁB��{0}�K�v���B";
                    case 3005: // �w�������L�����Z�������ꍇ
                        return this.name + "�F���̂����Ă����������B";
                    case 3006: // ����Ȃ��A�C�e���𔄂낤�Ƃ����ꍇ
                        return this.name + "�F���܂Ȃ����A����͔�������B";
                    case 3007: // �A�C�e�����p��
                        return this.name + "�F�ӂށA{0}���ȁB{1}Gold�Ŕ�����낤���B";
                    case 3008: // ����̓y���_���g���p��
                        return this.name + "�F�ӂށE�E�E�ǂ��o����̃A�N�Z�T�����B{0}Gold�����A�{���ɔ�������ėǂ��̂��H";
                    case 3009: // ����X���o�鎞
                        return this.name + "�F�܂����ł����Ȃ����B";
                    case 3010: // �K���c�s�ݎ��̔��肫��t�F���g�D�[�V�������Ĉꌾ
                        return "";
                    case 3011: // �����\�Ȃ��̂��w�����ꂽ��
                        return this.name + "�F�ӂށA�������ő������Ă������ˁH";
                    case 3012: // �������Ă������𔄋p�Ώۂ��ǂ���������
                        return this.name + "�F���݂̑����i�𔄋p���邩�ˁH{0}��{1}Gold�Ŕ�����낤�B";
                    case 3013: // ���莝��������������A�T�u����𔄋p�����A�茳�Ɏc�����Ƃ��āA�o�b�N�p�b�N�������ς��̎�
                        return this.name + "�F�ו��������ς��̂悤���ȁB{0}�̓n���i�̏h���q�ɂɌ�ő����Ă������B";
                }
            }
            #endregion
            #region "�O�K�̎��ҁFMinflore"
            else if (this.name == "�O�K�̎��ҁFMinflore")
            {
                switch (sentenceNumber)
                {
                    case 2: // Double Slash 1 Carnage Rush 1
                        return this.name + "�F�b�n�I\r\n";
                    case 3: // Double Slash 2 Carnage Rush 2
                        return this.name + "�F�b���@�I\r\n";
                    case 9: // Fresh Heal�ɂ�郉�C�t��
                        return this.name + "�F���C�t�񕜂����Ă��炤��AFreshHeal�B{0} �񕜁B\r\n";
                    case 13: // �ʏ�U���N���e�B�J���q�b�g
                        return this.name + "�F���Ȃ��̂���_�A���؂�����I�N���e�B�J���I�I {0} �� {1}�̃_���[�W\r\n";
                    case 18: // Protection
                        return this.name + "�F���Ȃ�_��A���ɗ^����AProtection�B �����h��́F�t�o\r\n";
                    case 19: // Absorb Water
                        return this.name + "�F���̏��_��A���ɗ^����AAbsorbWater�B ���@�h��́F�t�o�B\r\n";
                    case 20: // Saint Power
                        return this.Name + "�F�͂��S�Ă𕢂���ASaintPower�B�����U���́F�t�o\r\n";
                    case 24: // Glory
                        return this.name + "�F�����Ƃ炵�A�h�������ɁIGlory�I�@ ���ڍU���{FreshHeal�A�g�̃I�[��\r\n";
                    case 70: // Crushing Blow
                        return this.name + "�F����ł��H�炢�Ȃ����ACrushingBlow�B  {0} �� {1} �̃_���[�W�B\r\n";
                    case 115: // �ʏ�U���̃q�b�g
                        return this.name + "����̍U���B {0} �� {1} �̃_���[�W\r\n";
                    case 117: // StraightSmash�Ȃǂ̃X�L���N���e�B�J��
                        return this.name + "�F�����ŃN���e�B�J���q�b�g��I\r\n";
                }
            }
            #endregion
            #region "�l�K�̎��ҁFAltomo"
            else if (this.name == "�l�K�̎��ҁFAltomo")
            {
                switch (sentenceNumber)
                {
                    case 13: // �ʏ�U���N���e�B�J���q�b�g
                        return this.name + "�F������I�I�N���e�B�J���I�I {0} �� {1} �̃_���[�W\r\n";
                    case 42: // Word of Fortune
                        return this.name + "�F���͕K�E���A��������ł��炨�����I �����̃I�[�����N���オ�����B\r\n";
                    case 63: // Truth Vision
                        return this.name + "�F�p�����^�t�o�ȂǂƂ����������ȁI�@�Ώۂ̃p�����^�t�o�𖳎�����悤�ɂȂ����B\r\n";
                    case 70: // Scatter Shot (Crushing Blow)
                        return this.name + "�F�ז����A�Q�Ă�B�@ScatterShot�I {0} �� {1}�̃_���[�W�B\r\n";
                    case 75: // Silence Shot (Altomo��p)
                        return "{0} �͖��@�r�����ł��Ȃ��Ȃ����I\r\n";
                    case 76: // Silence Shot�AAbsoluteZero���قɂ��r�����s
                        return this.name + "�F�����E�E�E���̒��x�̖��@�ŁE�E�E\r\n";
                    case 87: // AbsoluteZero�ŃX�L���g�p���s
                        return this.name + "�F�����E�E�E���̒��x�̖��@�ŁE�E�E\r\n";
                    case 88: // AbsoluteZero�ɂ��h�䎸�s
                        return this.name + "�F�����E�E�E���̒��x�̖��@�ŁE�E�E\r\n";
                    case 110: // CounterAttack�𖳎�������
                        return this.Name + "�F����ȍ\�����炢�����ʂ����I�@�����킯���Ȃ����낤�I�I\r\n";
                    case 115: // �ʏ�U���̃q�b�g
                        return this.name + "����̍U���B {0} �� {1} �̃_���[�W\r\n";
                    case 116: // �h��𖳎����čU�����鎞
                        return this.name + "�F�h��Ȃǖ��ʂȍs�ׂ��I�@�Â��ȁI�I\r\n";
                    case 117: // StraightSmash�Ȃǂ̃X�L���N���e�B�J��
                        return this.name + "�F���ӂ�A�N���e�B�J�����I\r\n";
                    case 119: // Absolute Zero�ɂ�胉�C�t�񕜂ł��Ȃ��ꍇ
                        return this.name + "�F���ӂ�A���Ƃ��ƃ��C�t�񕜂ȂǂƁA�א�����͂��Ȃ��낤�I�I\r\n";
                }
            }
            #endregion
            #region "�܊K�̎��ҁFBystander"
            else if (this.name == "�܊K�̎��ҁFBystander")
            {
                switch (sentenceNumber)
                {
                    case 18: // Protection
                        return this.name + "�F�w�����@�x  �|�@�wProtection�x�@�����h��́F�t�o\r\n";
                    case 19: // Absorb Water
                        return this.name + "�F�w�����@�x  �|�@�wAbsorbWater�x�@���@�h��́F�t�o�B\r\n";
                    case 20: // Saint Power
                        return this.name + "�F�w�����@�x  �|�@�wSaintPower�x�@�����U���́F�t�o\r\n";
                    case 21: // Shadow Pact
                        return this.name + "�F�w�Ŗ��@�x  �|�@�wShadowPact�x�@���@�U���́F�t�o\r\n";
                    case 22: // Bloody Vengeance
                        return this.name + "�F�w�Ŗ��@�x  �|�@�wBloodyVengeance�x�@�̓p�����^�� {0} �t�o\r\n";
                    case 38: // Heat Boost
                        return this.name + "�F�w�Ζ��@�x  �|�@�wHeatBoost�x�@�Z�p�����^�� {0} �t�o�B\r\n";
                    case 44: // Eternal Presence 1
                        return this.name + "�F�w�����@�x  �|�@�wEternalPresence�x�@" + this.name + "�̎���ɐV�����@�����\�z�����B\r\n";
                    case 45: // Eternal Presence 2
                        return this.name + "�̕����U���A�����h��A���@�U���A���@�h�䂪�t�o�����I\r\n";
                    case 49: // Rise of Image
                        return this.name + "�F�w�󖂖@�x  �|�@�wRiseOfImage�x�@�S�p�����^�� {0} �t�o\r\n";
                    case 83: // Promised Knowledge
                        return this.name + "�F�w�����@�x  �|�@�wPromisedKnowledge�x�@�m�p�����^�� {0} �t�o\r\n";

                    case 48: // Dispel Magic
                        return this.name + "�F�w�󖂖@�x  �|�@�wDispelMagic�x\r\n";
                    case 84: // Tranquility
                        return this.name + "�F�w�󖂖@�x  �|�@�wTranquility�x\r\n";
                    case 37: // Damnation
                        return this.name + "�F�w�Ŗ��@�x�@�|�@�wDamnation�x\r\n";
                    case 35: // Black Contract
                        return this.name + "�F�w�Ŗ��@�x�@�|�@�wBlackContract�x\r\n";
                    case 81: // Absolute Zero
                        return this.name + "�F�w�����@�x�@�|�@�wAbsoluteZero�x�@�Ώۂ̓��C�t�񕜕s�A�X�y���r���s�A�X�L���g�p�s�A�h��s�ɂȂ�B\r\n";

                    case 43: // Aether Drive
                        return this.name + "�F�wAetherDrive�x  ���͑S�̂ɋ�z�����͂��݂Ȃ���B\r\n";
                    case 39: // Immortal Rave
                        return this.name + "�F�wImmortalRave�x " + this.name + "�̎���ɂR�̉����h�����B\r\n";
                    case 40: // Gale Wind
                        return this.name + "�F�wGaleWind�x  ������l��Bystander�����ꂽ�B\r\n";
                    case 24: // Glory
                        return this.name + "�F�wGlory�x  ���ڍU���{FreshHeal�A�g�̃I�[��\r\n";
                    case 14: // FlameAura�ɂ��ǉ��U��
                        return "�wFlameAura�x���P��������I {0} �_���[�W\r\n";
                    case 65: // Carnage Rush 1
                        return this.name + "�F�wCarnageRush�x �w��x {0}�_���[�W   \r\n";
                    case 66: // Carnage Rush 2
                        return this.name + "�F�wCarnageRush�x �w��x {0}�_���[�W   \r\n";
                    case 67: // Carnage Rush 3
                        return this.name + "�F�wCarnageRush�x �w�Q�x {0}�_���[�W   \r\n";
                    case 68: // Carnage Rush 4
                        return this.name + "�F�wCarnageRush�x �w�l�x {0}�_���[�W   \r\n";
                    case 69: // Carnage Rush 5
                        return this.name + "�F�wCarnageRush�x �w�I�x {0}�̃_���[�W\r\n";
                    case 53: // �Ώەs�K��
                        return this.name + "�F���Ɂw�Ώہx�́w���Łx���Ă���B\r\n";
                    case 9: // Fresh Heal�ɂ�郉�C�t��
                        return this.name + "�F{0} �w�񕜁x\r\n";

                    case 57: // Mirror Image
                        return this.name + "�w�����@�x�@�|�@�wMirrorImage�x {0}�̎��͂ɐ���Ԃ����������B\r\n";
                    case 58: // Mirror Image 2
                        return this.name + "�͖��@���͂����Ԃ����Ƃ����B {0} �_���[�W�� {1} �ɔ��˂����I\r\n";
                    case 59: // Mirror Image 3
                        return this.name + "�͖��@���͂����Ԃ����Ƃ����B �y�������A���͂ȈЗ͂ł͂˕Ԃ��Ȃ��I�z\r\n";
                    case 60: // Deflection
                        return this.name + "�w�󖂖@�x�@�|�@�wAbsoluteZero�x {0}�̎��͂ɔ�����Ԃ����������B\r\n";
                    case 61: // Deflection 2
                        return this.name + "�͕����_���[�W���͂����Ԃ����Ƃ����I {0} �_���[�W�� {1} �ɔ��˂����I\r\n";
                    case 62: // Deflection 3
                        return this.name + "�͕����_���[�W���͂����Ԃ����Ƃ����I �y�������A���͂ȈЗ͂ł͂˕Ԃ��Ȃ��I�z\r\n";
                    case 82: // BUFFUP���ʂ��]�߂Ȃ��ꍇ
                        return "�������A���ɂ��̃p�����^�͏㏸���Ă��邽�߁A���ʂ��Ȃ������B\r\n";

                    case 27: // Dark Blast
                        return "�wDarkBlast�x���P��������I {0} �� {1} �̃_���[�W\r\n";
                    case 23: // Holy Shock
                        return "�wHolyShock�x���P��������I {0} �� {1} �̃_���[�W\r\n";
                    case 30: // Ice Needle
                        return "�wIceNeedle�x���P��������I {0} �� {1} �̃_���[�W\r\n";
                    case 10: // Fire Ball
                        return "�wFireBall�x���P��������I {0} �� {1} �̃_���[�W\r\n";
                    case 33: // Word of Power
                        return "�wWordOfPower�x���P��������I {0} �� {1} �̃_���[�W\r\n";
                    case 11: // Flame Strike
                        return "�wFlameStrike�x���P��������I {0} �� {1} �̃_���[�W\r\n";
                    case 31: // Frozen Lance
                        return "�wFrozenLance�x���P��������I {0} �� {1} �̃_���[�W\r\n";
                    case 12: // Volcanic Wave
                        return "�wVolcanicWave�x���P��������I {0} �� {1} �̃_���[�W\r\n";
                    case 34: // White Out
                        return "�wWhiteOut�x���P��������I {0} �� {1} �̃_���[�W\r\n";
                    case 28: // Lava Annihilation
                        return "�wLavaAnnihilation�x���P��������I {0} �� {1} �̃_���[�W\r\n";

                    case 41: // Word of Life
                        return this.name + "�F�wWordOfLife�x�@�厩�R����̋�����������������悤�ɂȂ����B\r\n";
                    case 25: // CelestialNova 1
                        return this.name + "�F�wCelestialNova�x�@{0} �w�񕜁x\r\n";

                    case 36: // Flame Aura�r��
                        return this.name + "�F�w�Ζ��@�x  �|�@�wFlameAura�x ���ڍU���ɉ��̒ǉ����ʂ��t�^�����B\r\n";

                    case 106: // Nothing Of Nothingness 1
                        return this.name + "�F�w���S�X�L���x  �|�@�wNothingOfNothingness�x�@���F�̃I�[�����h��n�߂�I \r\n";
                    case 107: // Nothing Of Nothingness 2
                        return this.name + "�͖��������@�𖳌����A�@����������X�L���𖳌�������悤�ɂȂ����I\r\n";

                    case 115: // �ʏ�U���̃q�b�g
                        return this.name + "����̍U���B {0} �� {1} �̃_���[�W\r\n";
                    case 117: // StraightSmash�Ȃǂ̃X�L���N���e�B�J��
                        return this.name + "�F�w�N���e�B�J���q�b�g�x\r\n";
                }
            }
            #endregion
            #region "�s���葽��"
            else
            {
                switch (sentenceNumber)
                {
                    case 0: // �X�L���s��
                        return this.name + "�̃X�L���|�C���g������Ȃ��I\r\n";
                    case 1: // Straight Smash
                        return this.name + "�̓X�g���[�g�E�X�}�b�V�����J��o�����B\r\n";
                    case 2: // Double Slash 1
                        return this.name + "  �P��ڂ̍U���I\r\n";
                    case 3: // Double Slash 2
                        return this.name + "  �Q��ڂ̍U���I\r\n";
                    case 4: // Painful Insanity
                        return this.name + "�́y�S�ቜ�`�zPainfulInsanity���J��o�����I\r\n";
                    case 5: // empty skill
                        return this.name + "�̓X�L���I�����Y��Ă����I\r\n";
                    case 6: // ���݂��t�����V�X�̕K�E��H�������
                        return this.name + "�͕K���Ɋ����Ă���E�E�E\r\n";
                    case 7: // Lizenos�̕K�E��H�������
                        return this.name + "�͕K���Ɋ����Ă���E�E�E\r\n";
                    case 8: // Minflore�̕K�E��H�������
                        return this.name + "�͕K���Ɋ����Ă���E�E�E\r\n";
                    case 9: // Fresh Heal�ɂ�郉�C�t��
                        return this.name + "��{0} �񕜂����B\r\n";
                    case 10: // Fire Ball
                        return this.name + "��FireBall���������I {0} �� {1} �̃_���[�W\r\n";
                    case 11: // Flame Strike
                        return this.name + "��FlameStrike���������I {0} �� {1} �̃_���[�W\r\n";
                    case 12: // Volcanic Wave
                        return this.name + "��VolcanicWave���������I {0} �� {1} �̃_���[�W\r\n";
                    case 13: // �ʏ�U���N���e�B�J���q�b�g
                        return this.name + "����̃N���e�B�J���q�b�g�I {0} �� {1} �̃_���[�W\r\n";
                    case 14: // FlameAura�ɂ��ǉ��U��
                        return this.name + "��FlameAura�ɂ�� {0} �̒ǉ��_���[�W\r\n";
                    case 15: // �����̐�����퓬���Ɏg�����Ƃ�
                        return this.name + "�͂�݂����ɃA�C�e�����g�����B\r\n";
                    case 16: // ���ʂ𔭊����Ȃ��A�C�e����퓬���Ɏg�����Ƃ�
                        return this.name + "�͂�݂����ɃA�C�e�����g�����B\r\n";
                    case 17: // ���@�Ń}�i�s��
                        return this.name + "�̃}�i������Ȃ��I\r\n";
                    case 18: // Protection
                        return this.name + "��Protection���������I �����h��́F�t�o\r\n";
                    case 19: // Absorb Water
                        return this.name + "��AbsorbWater���������I ���@�h��́F�t�o�B\r\n";
                    case 20: // Saint Power
                        return this.name + "��SaintPower���������I �����U���́F�t�o\r\n";
                    case 21: // Shadow Pact
                        return this.name + "��ShadowPact���������I ���@�U���́F�t�o\r\n";
                    case 22: // Bloody Vengeance
                        return this.name + "��BloodyVengeance���������I �̓p�����^�� {0} �t�o\r\n";
                    case 23: // Holy Shock
                        return this.name + "��HolyShock���������I {0} �� {1} �̃_���[�W\r\n";
                    case 24: // Glory
                        return this.name + "��Glory���������I ���ڍU���{FreshHeal�A�g�̃I�[��\r\n";
                    case 25: // CelestialNova 1
                        return this.name + "��CelestialNova���������I {0} �񕜁B\r\n";
                    case 26: // CelestialNova 2
                        return this.name + "��CelestialNova���������I {0} �̃_���[�W\r\n";
                    case 27: // Dark Blast
                        return this.name + "��DarkBlast���������I {0} �� {1} �̃_���[�W\r\n";
                    case 28: // Lava Annihilation
                        return this.name + "��LavaAnnihilation���������I {0} �� {1} �̃_���[�W\r\n";
                    case 29: // Devouring Plague
                        return this.name + "��DevouringPlague���������I {0} ���C�t���z�������\r\n";
                    case 30: // Ice Needle
                        return this.name + "��IceNeedle���������I {0} �� {1} �̃_���[�W\r\n";
                    case 31: // Frozen Lance
                        return this.name + "��FrozenLance���������I {0} �� {1} �̃_���[�W\r\n";
                    case 32: // Tidal Wave
                        return this.name + "��TidalWave���������I {0} �̃_���[�W\r\n";
                    case 33: // Word of Power
                        return this.name + "��WordOfPower���������I {0} �� {1} �̃_���[�W\r\n";
                    case 34: // White Out
                        return this.name + "��WhiteOut���������I {0} �� {1} �̃_���[�W\r\n";
                    case 35: // Black Contract
                        return this.name + "��BlackContract���������I " + this.name + "�̃X�L���A���@�R�X�g�͂O�ɂȂ�B\r\n";
                    case 36: // Flame Aura�r��
                        return this.name + "��FlameAura���������I ���ڍU���ɉ��̒ǉ����ʂ��t�^�����B\r\n";
                    case 37: // Damnation
                        return this.name + "��Damnation���������I �������o�ł鍕����Ԃ�c�܂���B\r\n";
                    case 38: // Heat Boost
                        return this.name + "��HeatBoost���������I �Z�p�����^�� {0} �t�o�B\r\n";
                    case 39: // Immortal Rave
                        return this.name + "��ImmortalRave���������I " + this.name + "�̎���ɂR�̉����h�����B\r\n";
                    case 40: // Gale Wind
                        return this.name + "��GaleWind���������I ������l��" + this.name + "�����ꂽ�B\r\n";
                    case 41: // Word of Life
                        return this.name + "��WordOfLife���������I �厩�R����̋�����������������悤�ɂȂ����B\r\n";
                    case 42: // Word of Fortune
                        return this.name + "��WordOfFortune���������I �����̃I�[�����N���オ�����B\r\n";
                    case 43: // Aether Drive
                        return this.name + "��AetherDrive���������I ���͑S�̂ɋ�z�����͂��݂Ȃ���B\r\n";
                    case 44: // Eternal Presence 1
                        return this.name + "��EternalPresence���������I " + this.name + "�̎���ɐV�����@�����\�z�����B\r\n";
                    case 45: // Eternal Presence 2
                        return this.name + "�̕����U���A�����h��A���@�U���A���@�h�䂪�t�o�����I\r\n";
                    case 46: // One Immunity
                        return this.name + "��OneImmunity���������I " + this.name + "�̎��͂ɖڂɌ����Ȃ���ǂ������B\r\n";
                    case 47: // Time Stop
                        return this.name + "��TimeStop���������I ����̎���������􂫎��Ԓ�~�������B\r\n";
                    case 48: // Dispel Magic
                        return this.name + "��DispelMagic���������I \r\n";
                    case 49: // Rise of Image
                        return this.name + "��RiseOfImage���������I �S�p�����^�� {0} �t�o\r\n";
                    case 50: // ��r��
                        return this.name + "�͉r���Ɏ��s�����I\r\n";
                    case 51: // Inner Inspiration
                        return this.name + "��InnerInspiration���J��o�����I {0} �X�L���|�C���g��\r\n";
                    case 52: // Resurrection 1
                        return this.name + "��Resurrection���������I�I\r\n";
                    case 53: // Resurrection 2
                        return this.name + "�͑Ώۂ��ԈႦ�Ă����I�I\r\n";
                    case 54: // Resurrection 3
                        return this.name + "�͑Ώۂ��ԈႦ�Ă����I�I\r\n";
                    case 55: // Resurrection 4
                        return this.name + "�͑Ώۂ��ԈႦ�Ă����I�I\r\n";
                    case 56: // Stance Of Standing
                        return this.name + "��StanceOfStanding���J��o�����I\r\n";
                    case 57: // Mirror Image
                        return this.name + "��MirrorImage���������I {0}�̎��͂ɐ���Ԃ����������B\r\n";
                    case 58: // Mirror Image 2
                        return this.name + "�͖��@���͂����Ԃ����Ƃ����B {0} �_���[�W�� {1} �ɔ��˂����I\r\n";
                    case 59: // Mirror Image 3
                        return this.name + "�͖��@���͂����Ԃ����Ƃ����B �y�������A���͂ȈЗ͂ł͂˕Ԃ��Ȃ��I�z\r\n";
                    case 60: // Deflection
                        return this.name + "��Deflection���������I {0}�̎��͂ɔ�����Ԃ����������B\r\n";
                    case 61: // Deflection 2
                        return this.name + "�͕����_���[�W���͂����Ԃ����Ƃ����I {0} �_���[�W�� {1} �ɔ��˂����I\r\n";
                    case 62: // Deflection 3
                        return this.name + "�͕����_���[�W���͂����Ԃ����Ƃ����I �y�������A���͂ȈЗ͂ł͂˕Ԃ��Ȃ��I�z\r\n";
                    case 63: // Truth Vision
                        return this.name + "��TruthVision���J��o�����I " + this.name + "�͑Ώۂ̃p�����^�t�o�𖳎�����悤�ɂȂ����B\r\n";
                    case 64: // Stance Of Flow
                        return this.name + "��StanceOfFlow���J��o�����I " + this.name + "�͎��R�^�[���A�K����U�����悤�ɍ\�����B\r\n";
                    case 65: // Carnage Rush 1
                        return this.name + "��CarnageRush���J��o�����I �P����{0}�_���[�W�E�E�E   ";
                    case 66: // Carnage Rush 2
                        return " �Q����{0}�_���[�W   ";
                    case 67: // Carnage Rush 3
                        return " �R����{0}�_���[�W   ";
                    case 68: // Carnage Rush 4
                        return " �S����{0}�_���[�W   ";
                    case 69: // Carnage Rush 5
                        return " �T����{0}�̃_���[�W\r\n";
                    case 70: // Crushing Blow
                        return this.name + "��CrushingBlow���J��o�����I  {0} �� {1} �̃_���[�W�B\r\n";
                    case 71: // ���[�x�X�g�����N�|�[�V�����퓬�g�p��
                        return this.name + "�̓A�C�e�����g�p���Ă����I\r\n";
                    case 72: // Enigma Sence
                        return this.name + "��EnigmaSence���J��o�����I\r\n";
                    case 73: // Soul Infinity
                        return this.name + "��SoulInfinity���J��o�����I\r\n";
                    case 74: // Kinetic Smash
                        return this.name + "��KineticSmash���J��o�����I\r\n";
                    case 75: // Silence Shot (Altomo��p)
                        return "";
                    case 76: // Silence Shot�AAbsoluteZero���قɂ��r�����s
                        return this.name + "�͖��@�r���Ɏ��s�����I�I\r\n";
                    case 77: // Cleansing
                        return this.name + "��Cleansing���������I\r\n";
                    case 78: // Pure Purification
                        return this.name + "��PurePurification���J��o�����I\r\n";
                    case 79: // Void Extraction
                        return this.name + "��VoidExtraction���J��o�����I " + this.name + "�� {0} �� {1}�t�o�I\r\n";
                    case 80: // �A�J�V�W�A�̎��g�p��
                        return this.name + "�̓A�C�e�����g�p���Ă����I\r\n";
                    case 81: // Absolute Zero
                        return this.name + "��AbsoluteZero���������I �X������ʂɂ��A���C�t�񕜕s�A�X�y���r���s�A�X�L���g�p�s�A�h��s�ƂȂ����B\r\n";
                    case 82: // BUFFUP���ʂ��]�߂Ȃ��ꍇ
                        return "�������A���ɂ��̃p�����^�͏㏸���Ă��邽�߁A���ʂ��Ȃ������B\r\n";
                    case 83: // Promised Knowledge
                        return this.name + "��PromiesdKnowledge���������I �m�p�����^�� {0} �t�o\r\n";
                    case 84: // Tranquility
                        return this.name + "��Tranquility���������I\r\n";
                    case 85: // High Emotionality 1
                        return this.name + "��HighEmotionality���J��o�����I\r\n";
                    case 86: // High Emotionality 2
                        return this.name + "�̗́A�Z�A�m�A�S�p�����^���t�o�����I\r\n";
                    case 87: // AbsoluteZero�ŃX�L���g�p���s
                        return this.name + "�͐�Η�x���ʂɂ��A�X�L���̎g�p�Ɏ��s�����I\r\n";
                    case 88: // AbsoluteZero�ɂ��h�䎸�s
                        return this.name + "�͐�Η�x���ʂɂ��A�h��ł��Ȃ��܂܂ł���I \r\n";
                    case 89: // Silent Rush 1
                        return this.name + "��SilentRush���J��o�����I �P���� {0}�_���[�W�E�E�E�@";
                    case 90: // Silent Rush 2
                        return "�Q���� {0}�_���[�W   ";
                    case 91: // Silent Rush 3
                        return "�R���� {0}�̃_���[�W\r\n";
                    case 92: // BUFFUP�ȊO�̉i�����ʂ����ɂ��Ă���ꍇ
                        return "�������A���ɂ��̌��ʂ͕t�^����Ă���B\r\n";
                    case 93: // Anti Stun
                        return this.name + "��AntiStun���J��o�����I " + this.name + "�̓X�^�����ʂւ̑ϐ����t����\r\n";
                    case 94: // Anti Stun�ɂ��X�^�����
                        return this.name + "��AntiStun���ʂɂ��X�^������������B\r\n";
                    case 95: // Stance Of Death
                        return this.name + "��StanceOfDeath���J��o�����I " + this.name + "�͒v�����P�x����ł���悤�ɂȂ���\r\n";
                    case 96: // Oboro Impact 1
                        return this.name + "�́y���ɉ��`�zOboroImpact���J��o�����I\r\n";
                    case 97: // Oboro Impact 2
                        return "{0}��{1}�̃_���[�W\r\n";
                    case 98: // Catastrophe 1
                        return this.name + "�́y���ɉ��`�zCatastrophe���J��o�����I\r\n";
                    case 99: // Catastrophe 2
                        return this.name + " {0}�̃_���[�W\r\n";
                    case 100: // Stance Of Eyes
                        return this.name + "��StanceOfEyes���J��o�����I " + this.name + "�́A����̍s���ɔ����Ă���E�E�E\r\n";
                    case 101: // Stance Of Eyes�ɂ��L�����Z����
                        return this.name + "�͑���̃��[�V���������؂��āA�s���L�����Z�������I\r\n";
                    case 102: // Stance Of Eyes�ɂ��L�����Z�����s��
                        return this.name + "�͑���̃��[�V���������؂낤�Ƃ������A����̃��[�V���������؂�Ȃ������I\r\n";
                    case 103: // Negate
                        return this.name + "��Negate���J��o�����I " + this.name + "�͑���̃X�y���r���ɔ����Ă���E�E�E\r\n";
                    case 104: // Negate�ɂ��X�y���r���L�����Z����
                        return this.name + "�͑���̃X�y���r����e�����I\r\n";
                    case 105: // Negate�ɂ��X�y���r���L�����Z�����s��
                        return this.name + "�͑���̃X�y���r����e�����Ƃ������A����̃X�y���r�������؂�Ȃ������I\r\n";
                    case 106: // Nothing Of Nothingness 1
                        return this.name + "�́y���ɉ��`�zNothingOfNothingness���J��o�����I " + this.name + "�ɖ��F�̃I�[�����h��n�߂�I \r\n";
                    case 107: // Nothing Of Nothingness 2
                        return this.name + "�͖��������@�𖳌����A�@����������X�L���𖳌�������悤�ɂȂ����I\r\n";
                    case 108: // Genesis
                        return this.name + "��Genesis���������I " + this.name + "�͑O��̍s���������ւƓ��e�������I\r\n";
                    case 109: // Cleansing�r�����s��
                        return this.name + "�͒��q���������߁ACleansing�̉r���Ɏ��s�����B\r\n";
                    case 110: // CounterAttack�𖳎�������
                        return this.Name + "��CounterAttack�̍\���𖳎������B\r\n";
                    case 111: // �_�����g�p��
                        return this.name + "�̓A�C�e�����g�p���Ă����I\r\n";
                    case 112: // �_�����A���Ɏg�p�ς݂̏ꍇ
                        return this.name + "���g�����A�C�e���͌��ʂ��Ȃ������I\r\n";
                    case 113: // CounterAttack�ɂ�锽�����b�Z�[�W
                        return this.name + "�̓J�E���^�[�A�^�b�N���J��o�����I\r\n";
                    case 114: // CounterAttack�ɑ΂��锽����NothingOfNothingness�ɂ���Ėh���ꂽ��
                        return this.name + "�̓J�E���^�[�ł��Ȃ������I\r\n";
                    case 115: // �ʏ�U���̃q�b�g
                        return this.name + "����̍U���B {0} �� {1} �̃_���[�W\r\n";
                    case 116: // �h��𖳎����čU�����鎞
                        return this.name + "�͖h��𖳎����čU�����Ă����I\r\n";
                    case 117: // StraightSmash�Ȃǂ̃X�L���N���e�B�J��
                        return this.name + "����̃N���e�B�J���q�b�g�I\r\n";
                    case 118: // �퓬���A�����@�C���|�[�V�����ɂ�镜���̂�����
                        return this.name + "�̓����@�C���|�[�V�������g�p�����I";
                    case 119: // Absolute Zero�ɂ�胉�C�t�񕜂ł��Ȃ��ꍇ
                        return this.name + "�͐�Η�x���ʂɂ�胉�C�t�񕜂ł��Ȃ��I\r\n";
                    case 120: // ���@�U���̃q�b�g
                        return "{0} �� {1} �̃_���[�W\r\n";
                    case 121: // Absolute Zero�ɂ��}�i�񕜂ł��Ȃ��ꍇ
                        return this.name + "�͓��Ă������ɂ��}�i�񕜂��ł��Ȃ��B\r\n";
                    case 122: // �u���߂�v�s����
                        return this.name + "�͖��͂�~�����B\r\n";
                    case 123: // �u���߂�v�s���ŗ��܂肫���Ă���ꍇ
                        return this.name + "�͖��͂�~���悤�Ƃ������A����ȏ�~�����Ȃ��ł���B\r\n";
                    case 124: // StraightSmash�̃_���[�W�l
                        return this.name + "�̃X�g���[�g�E�X�}�b�V�����q�b�g�B {0} �� {1}�̃_���[�W\r\n";
                    case 125: // �A�C�e���g�p�Q�[�W�����܂��ĂȂ��Ƃ�
                        return this.name + "�̓A�C�e���Q�[�W�����܂��ĂȂ��ԁA�A�C�e�����g���Ȃ��ł���B\r\n";
                    case 126: // FlashBlase
                        return this.name + "�F��FlashBlaze���������I {0} �� {1} �̃_���[�W\r\n";
                    case 127: // �������@�ŃC���X�^���g�s��
                        return this.name + "�̃C���X�^���g�l������Ȃ��I\r\n";
                    case 128: // �������@�̓C���X�^���g�^�C�~���O�ł��ĂȂ�
                        return this.name + "�̓C���X�^���g�^�C�~���O�Ŕ����ł��Ȃ��ł���B\r\n";
                    case 129: // PsychicTrance
                        return this.name + "��PsychicTrance���������I\r\n";
                    case 130: // BlindJustice
                        return this.name + "��BlindJustice���������I\r\n";
                    case 131: // TranscendentWish
                        return this.name + "��TranscendentWish���������I\r\n";
                    case 132: // LightDetonator
                        return this.name + "��LightDetonator���������I\r\n";
                    case 133: // AscendantMeteor
                        return this.name + "��AscendantMeteor���������I\r\n";
                    case 134: // SkyShield
                        return this.name + "��SkyShield���������I\r\n";
                    case 135: // SacredHeal
                        return this.name + "��SacredHeal���������I\r\n";
                    case 136: // EverDroplet
                        return this.name + "��EverDroplet���������I\r\n";
                    case 137: // FrozenAura
                        return this.name + "��FrozenAura���������I\r\n";
                    case 138: // ChillBurn
                        return this.name + "��ChillBurn���������I\r\n";
                    case 139: // ZetaExplosion
                        return this.name + "��ZetaExplosion���������I\r\n";
                    case 140: // FrozenAura�ǉ����ʃ_���[�W��
                        return this.name + "��FrozenAura�ɂ�� {0} �̒ǉ��_���[�W\r\n";
                    case 141: // StarLightning
                        return this.name + "��StarLightning���������I\r\n";
                    case 142: // WordOfMalice
                        return this.name + "��WordOfMalice���������I\r\n";
                    case 143: // BlackFire
                        return this.name + "��BlackFire���������I\r\n";
                    case 144: // EnrageBlast
                        return this.name + "��EnrageBlast���������I\r\n";
                    case 145: // Immolate
                        return this.name + "��Immolate���������I\r\n";
                    case 146: // VanishWave
                        return this.name + "��VanishWave���������I\r\n";
                    case 147: // WordOfAttitude
                        return this.name + "��WordOfAttitude���������I\r\n";
                    case 148: // HolyBreaker
                        return this.name + "��HolyBreaker���������I\r\n";
                    case 149: // DarkenField
                        return this.name + "��DarkenField���������I\r\n";
                    case 150: // SeventhMagic
                        return this.name + "��SeventhMagic���������I\r\n";
                    case 151: // BlueBullet
                        return this.name + "��BlueBullet���������I\r\n";
                    case 152: // NeutralSmash
                        return this.name + "��NeutralSmash���J��o�����I\r\n";
                    case 153: // SwiftStep
                        return this.name + "��SwiftStep���J��o�����I\r\n";
                    case 154: // CircleSlash
                        return this.name + "��CircleSlash���J��o�����I\r\n";
                    case 155: // RumbleShout
                        return this.name + "��RumbleShout���J��o�����I\r\n";
                    case 156: // SmoothingMove
                        return this.name + "��SmoothingMove���J��o�����I\r\n";
                    case 157: // FutureVision
                        return this.name + "��FutureVision���J��o�����I\r\n";
                    case 158: // ReflexSpirit
                        return this.name + "��ReflexSpirit���J��o�����I\r\n";
                    case 159: // SharpGlare
                        return this.name + "��SharpGlare���J��o�����I\r\n";
                    case 160: // TrustSilence
                        return this.name + "��TrustSilence���J��o�����I\r\n";
                    case 161: // SurpriseAttack
                        return this.name + "��SurpriseAttack���J��o�����I\r\n";
                    case 162: // PsychicWave
                        return this.name + "��PsychicWave���J��o�����I\r\n";
                    case 163: // Recover
                        return this.name + "��Recover���J��o�����I\r\n";
                    case 164: // ViolentSlash
                        return this.name + "��ViolentSlash���J��o�����I\r\n";
                    case 165: // OuterInspiration
                        return this.name + "��OuterInspiration���J��o�����I\r\n";
                    case 166: // StanceOfSuddenness
                        return this.name + "��StanceOfSuddenness���J��o�����I\r\n";
                    case 167: // �C���X�^���g�ΏۂŔ����s��
                        return this.name + "�͑Ώۂ̃C���X�^���g�R�}���h�������˘f���Ă���B\r\n";
                    case 168: // StanceOfMystic
                        return this.name + "��StanceOfMystic���J��o�����I\r\n";
                    case 169: // HardestParry
                        return this.name + "��HardestParry���J��o�����I\r\n";
                    case 170: // ConcussiveHit
                        return this.name + "��ConcussiveHit���J��o�����I\r\n";
                    case 171: // Onslaught hit
                        return this.name + "��OnslaughtHit���J��o�����I\r\n";
                    case 172: // Impulse hit
                        return this.name + "��ImpulseHit���J��o�����I\r\n";
                    case 173: // Fatal Blow
                        return this.name + "��FatalBlow���J��o�����I\r\n";
                    case 174: // Exalted Field
                        return this.name + "��ExaltedField���������I\r\n";
                    case 175: // Rising Aura
                        return this.name + "��RisingAura���J��o�����I\r\n";
                    case 176: // Ascension Aura
                        return this.name + "��AscensioAura���J��o�����I\r\n";
                    case 177: // Angel Breath
                        return this.name + "��AngelBreath���������I\r\n";
                    case 178: // Blazing Field
                        return this.name + "��BlazingField���������I\r\n";
                    case 179: // Deep Mirror
                        return this.name + "��DeepMirror���������I\r\n";
                    case 180: // Abyss Eye
                        return this.name + "��AbyssEye���������I\r\n";
                    case 181: // Doom Blade
                        return this.name + "��DoomBlade���J��o�����I\r\n";
                    case 182: // Piercing Flame
                        return this.name + "��PiercingFlame���������I\r\n";
                    case 183: // Phantasmal Wind
                        return this.name + "��PhantasmalWind���������I\r\n";
                    case 184: // Paradox Image
                        return this.name + "��ParadoxImage���������I\r\n";
                    case 185: // Vortex Field
                        return this.name + "��VortexField���������I\r\n";
                    case 186: // Static Barrier
                        return this.name + "��StaticBarrier���������I\r\n";
                    case 187: // Unknown Shock
                        return this.name + "��UnknownShock���J��o�����I\r\n";
                    case 188: // SoulExecution
                        return this.name + "�́y���ɉ��`�zSoulExecution���J��o�����I\r\n";
                    case 189: // SoulExecution hit 01
                        return this.name + "�P���ځI\r\n";
                    case 190: // SoulExecution hit 02
                        return this.name + "�Q���ځI\r\n";
                    case 191: // SoulExecution hit 03
                        return this.name + "�R���ځI\r\n";
                    case 192: // SoulExecution hit 04
                        return this.name + "�S���ځI\r\n";
                    case 193: // SoulExecution hit 05
                        return this.name + "�T���ځI\r\n";
                    case 194: // SoulExecution hit 06
                        return this.name + "�U���ځI\r\n";
                    case 195: // SoulExecution hit 07
                        return this.name + "�V���ځI\r\n";
                    case 196: // SoulExecution hit 08
                        return this.name + "�W���ځI\r\n";
                    case 197: // SoulExecution hit 09
                        return this.name + "�X���ځI\r\n";
                    case 198: // SoulExecution hit 10
                        return this.name + "�P�O���ځI\r\n";
                    case 199: // Nourish Sense
                        return this.name + "��NourishSense���J��o�����I\r\n";
                    case 200: // Mind Killing
                        return this.name + "��MindKilling���J��o�����I\r\n";
                    case 201: // Vigor Sense
                        return this.name + "��VigorSense���J��o�����I\r\n";
                    case 202: // ONE Authority
                        return this.name + "��OneAuthority���J��o�����I\r\n";
                    case 203: // �W���ƒf��
                        return this.name + "�́y�W���ƒf��z�𔭓������I\r\n";
                    case 204: // �y���j�z�����ς�
                        return this.name + "�͊��Ɂy���j�z���g�p���Ă���A����ȏ㔭�����s���Ȃ������B\r\n";
                    case 205: // �y���j�z�ʏ�s���I����
                        return this.name + "�́y���j�z�̓C���X�^���g�^�C�~���O����ł���A�ʏ�s���ɑI���ł��Ȃ������B\r\n";
                    case 206: // Sigil Of Homura
                        return this.name + "��SigilOfHomura���������I\r\n";
                    case 207: // Austerity Matrix
                        return this.name + "��AusterityMatrix���������I\r\n";
                    case 208: // Red Dragon Will
                        return this.name + "��RedDragonWill���������I\r\n";
                    case 209: // Blue Dragon Will
                        return this.name + "��BlueDragonWill���������I\r\n";
                    case 210: // Eclipse End
                        return this.name + "��EclipseEnd���������I\r\n";
                    case 211: // Sin Fortune
                        return this.name + "��SinFortune���������I\r\n";
                    case 212: // AfterReviveHalf
                        return this.name + "�Ɏ��ϐ��i�n�[�t�j���t�^���ꂽ�I\r\n";
                    case 213: // DemonicIgnite
                        return this.name + "��DemonicIgnite���������I\r\n";
                    case 214: // Death Deny
                        return this.name + "��DeathDeny���������I\r\n";
                    case 215: // Stance of Double
                        return this.name + "��StanceOfDouble��������I�@" + this.name + "�͑O��s�������������̕��g�𔭐��������I\r\n";
                    case 216: // �ŏI�탉�C�t�J�E���g����
                        return this.name + "�̐��������Ƃ������ɁA���̏�Ő����c�����I\r\n";
                    case 217: // �ŏI�탉�C�t�J�E���g���Ŏ�
                        return this.name + "�̐����͊��S�ɏ��ł����炽�E�E�E\r\n";

                    case 4001: // �ʏ�U����I��
                    case 4002: // �h���I��
                    case 4003: // �ҋ@��I��
                    case 4004: // �t���b�V���q�[����I��
                    case 4005: // �v���e�N�V������I��
                    case 4006: // �t�@�C�A�E�{�[����I��
                    case 4007: // �t���C���E�I�[����I��
                    case 4008: // �X�g���[�g�E�X�}�b�V����I��
                    case 4009: // �_�u���E�X�}�b�V����I��
                    case 4010: // �X�^���X�E�I�u�E�X�^���f�B���O��I��
                    case 4011: // �A�C�X�E�j�[�h����I��
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
                        return this.name + "��" + this.ActionLabel.Text + "��I�������B\r\n";
                    case 4072:
                        return this.name + "�͓G��Ώۂɂ���̂����߂���Ă���B\r\n";
                    case 4073:
                        return this.name + "�͓G��Ώۂɂ���̂����߂���Ă���B\r\n";
                    case 4074:
                        return this.name + "�͖�����Ώۂɂ���̂����߂���Ă���B\r\n";
                    case 4075:
                        return this.name + "�͖�����Ώۂɂ���̂����߂���Ă���B\r\n";
                    case 4076:
                        return this.name + "�͖����ɍU������̂����߂���Ă���B\r\n";
                    case 4077: // �u���߂�v�R�}���h
                        return this.name + "�͖��͂����ߍ��ݎn�߂��B\r\n";
                    case 4078: // ���픭���u���C���v
                        return this.name + "�̓��C������̌��ʔ�����I�������B\r\n";
                    case 4079: // ���픭���u�T�u�v
                        return this.name + "�̓T�u����̌��ʔ�����I�������B\r\n";
                    case 4080: // �A�N�Z�T���P����
                        return this.name + "�̓A�N�Z�T���P�̌��ʔ�����I�������B\r\n";
                    case 4081: // �A�N�Z�T���Q����
                        return this.name + "�̓A�N�Z�T���Q�̌��ʔ�����I�������B\r\n";

                    case 4082: // FlashBlaze
                        return this.name + "�̓t���b�V���u���C�Y��I�������B\r\n";

                    // ����U��
                    case 5001:
                        return this.name + "�̓G�A���E�X���b�V����������B {0} �� {1} �̃_���[�W\r\n";
                    case 5002:
                        return this.name + "��{0}���C�t�񕜂����B \r\n";
                    case 5003:
                        return this.name + "��{0}�}�i�񕜂��� \r\n";
                    case 5004:
                        return this.name + "�̓A�C�V�N���E�X���b�V����������B {0} �� {1} �̃_���[�W\r\n";
                    case 5005:
                        return this.name + "�̓G���N�g���E�u���[��������B {0} �� {1} �̃_���[�W\r\n";
                    case 5006:
                        return this.name + "�̓u���[�E���C�g�j���O��������B {0} �� {1} �̃_���[�W\r\n";
                    case 5007:
                        return this.name + "�̓o�[�j���O�E�N���C���A��������B {0} �� {1} �̃_���[�W\r\n";
                    case 5008:
                        return this.name + "�͐ԑ����̏񂩂瑓�̉���������B {0} �� {1} �̃_���[�W\r\n";
                    case 5009:
                        return this.name + "��{0}�X�L���|�C���g�񕜂����B\r\n";
                    case 5010:
                        return this.name + "���������Ă���w�ւ�����ɑ�������������I {0} �� {1} �̃_���[�W\r\n";
                }
            }
            #endregion
            return this.name + "�F\r\n";
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
            currentPoisonValue = 0; // ��Ғǉ�
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

        // s ��Ғǉ��i�ȉ��A�O�҂ƃ��\�b�h���A���Ԃ�Ȃ��悤�ɂ������B�������Ԃ�ƁADebuff���\�b�h�ŗ�O�G���[�ƂȂ邽�߂������������j
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
        // s ��Ғǉ�
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
        // e ��Ғǉ�
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

        // ������L
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
        // e ��Ғǉ�


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
            if�@(imageData != null)
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
            this.beforeTarget2 = target2; // ��Ғǉ�
            // this.alreadyPlayArchetype = false; [���j�����t���O�͈�������Ȃ��Ɩ߂�Ȃ�]
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
                    flag1 = true; // [�x��]:deadSignForTranscendentWish��true�ɂ��邽�߂ɍ̗p�����t���O�����A���̓W�J���l�������A�f�t�H���gtrue�w�肷��悤�ɂȂ�̂ŗ��ӕK�{�B
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

        // [�x��]�FBattleEnemy_Load�AMainCharacter:CleanUpEffect, MainCharacter:CleanUpBattleEnd�̓W�J�~�X�����������Ă��܂��B
        public void CleanUpEffect()
        {
            CleanUpEffect(true, true);
        }
        public void CleanUpEffect(bool ClearActionInfo, bool ClearBeforeAction)
        {
            if (PA == PlayerAction.UseSkill && currentSkillName == Database.GENESIS)
            {
                // Genesis�X�L���͑O��̏����N���A���Ȃ����Ƃł�����g�������܂��B
            }
            else if (PA == PlayerAction.UseSkill && currentSkillName == Database.STANCE_OF_DOUBLE)
            {
                // StanceOfDouble�X�L���͑O��̏����N���A���Ȃ����Ƃł�����g�������܂��B
            }
            else
            {
                if (ClearBeforeAction == true)
                {
                    this.beforePA = PA;
                    this.beforeUsingItem = currentUsingItem;
                    this.beforeSkillName = currentSkillName;
                    this.beforeSpellName = currentSpellName;
                    this.beforeArchetypeName = currentArchetypeName; // ��Ғǉ�
                    this.beforeTarget = target;
                    this.beforeTarget2 = target2; // ��Ғǉ�
                    // this.alreadyPlayArchetype = false; [���j�����t���O�͈�������Ȃ��Ɩ߂�Ȃ�]
                }
            }

            if (ClearActionInfo)
            {
                CurrentUsingItem = String.Empty;
                CurrentSkillName = String.Empty;
                CurrentSpellName = String.Empty;
                CurrentArchetypeName = String.Empty; // ��Ғǉ�
                PA = PlayerAction.None;
                target = null;
                target2 = null; // ��Ғǉ�
            }

            // �z�Ɛ��񂪂������Ă���ꍇ�A�J�E���g�_�E�����Ȃ��B
            if (this.CurrentJunkan_Seiyaku <= 0)
            {
                // change start ��ҕҏW�i���ۉ����\�b�h���g���Ĉ�s�ŕ\���l�A�C���j
                // ��
                // ��
                if (this.realTimeBattle == false) // ��Ғǉ��A���A���^�C���ł́A�^�[���I���ŉ��������A�ʂ̃t�F�[�Y�ŉ�������B
                {
                    CurrentCounterAttack = 0; // ��ҕҏW
                }
                // s ��Ғǉ�
                else
                {
                    AbstractCountDownBuff(pbCounterAttack, ref currentCounterAttack);
                }
                // e ��Ғǉ�
                AbstractCountDownBuff(pbAntiStun, ref currentAntiStun);
                AbstractCountDownBuff(pbStanceOfDeath, ref currentStanceOfDeath);

                // �_
                AbstractCountDownBuff(pbStanceOfFlow, ref currentStanceOfFlow);
                // ��
                AbstractCountDownBuff(pbStanceOfStanding, ref currentStanceOfStanding); // ��ҕҏW
                // �S��
                AbstractCountDownBuff(pbTruthVision, ref currentTruthVision);
                AbstractCountDownBuff(pbHighEmotionality, ref currentHighEmotionality, ref buffStrength_HighEmotionality, ref buffAgility_HighEmotionality, ref buffIntelligence_HighEmotionality, ref buffStamina_HighEmotionality, ref buffMind_HighEmotionality);
                AbstractCountDownBuff(pbStanceOfEyes, ref currentStanceOfEyes); // ��ҕҏW
                AbstractCountDownBuff(pbPainfulInsanity, ref currentPainfulInsanity);
                // ���S
                AbstractCountDownBuff(pbNegate, ref currentNegate); // ��ҕҏW
                AbstractCountDownBuff(pbVoidExtraction, ref currentVoidExtraction, ref buffStrength_VoidExtraction, ref buffAgility_VoidExtraction, ref buffIntelligence_VoidExtraction, ref buffStamina_VoidExtraction, ref buffMind_VoidExtraction);
                AbstractCountDownBuff(pbNothingOfNothingness, ref currentNothingOfNothingness);

                // ��
                AbstractCountDownBuff(pbProtection, ref currentProtection);
                AbstractCountDownBuff(pbSaintPower, ref currentSaintPower);
                AbstractCountDownBuff(pbGlory, ref currentGlory);
                // ��
                AbstractCountDownBuff(pbShadowPact, ref currentShadowPact);
                AbstractCountDownBuff(pbBlackContract, ref currentBlackContract);
                AbstractCountDownBuff(pbBloodyVengeance, ref currentBloodyVengeance, ref buffStrength_BloodyVengeance);
                AbstractCountDownBuff(pbDamnation, ref currentDamnation);                
                // ��
                AbstractCountDownBuff(pbFlameAura, ref currentFlameAura);
                AbstractCountDownBuff(pbHeatBoost, ref currentHeatBoost, ref buffAgility_HeatBoost);
                AbstractCountDownBuff(pbImmortalRave, ref currentImmortalRave);
                // ��
                AbstractCountDownBuff(pbAbsorbWater, ref currentAbsorbWater);
                AbstractCountDownBuff(pbMirrorImage, ref currentMirrorImage);
                AbstractCountDownBuff(pbPromisedKnowledge, ref currentPromisedKnowledge, ref buffIntelligence_PromisedKnowledge);
                AbstractCountDownBuff(pbAbsoluteZero, ref currentAbsoluteZero);
                // ��
                AbstractCountDownBuff(pbGaleWind, ref currentGaleWind);
                AbstractCountDownBuff(pbWordOfLife, ref currentWordOfLife);
                AbstractCountDownBuff(pbWordOfFortune, ref currentWordOfFortune);
                AbstractCountDownBuff(pbAetherDrive, ref currentAetherDrive);
                AbstractCountDownBuff(pbEternalPresence, ref currentEternalPresence);
                // ��
                AbstractCountDownBuff(pbRiseOfImage, ref currentRiseOfImage, ref buffMind_RiseOfImage);
                AbstractCountDownBuff(pbDeflection, ref currentDeflection);
                AbstractCountDownBuff(pbOneImmunity, ref currentOneImmunity);
                AbstractCountDownBuff(pbTimeStop, ref currentTimeStop);

                // ���{��
                AbstractCountDownBuff(pbPsychicTrance, ref currentPsychicTrance);
                AbstractCountDownBuff(pbBlindJustice, ref currentBlindJustice);
                AbstractCountDownBuff(pbTranscendentWish, ref currentTranscendentWish, ref buffStrength_TranscendentWish, ref buffAgility_TranscendentWish, ref buffIntelligence_TranscendentWish, ref buffStamina_TranscendentWish, ref buffMind_TranscendentWish, ref deadSignForTranscendentWish);
                // ���{��
                AbstractCountDownBuff(pbFlashBlaze, ref currentFlashBlazeCount);
                // ���{��
                AbstractCountDownBuff(pbSkyShield, ref currentSkyShield, ref currentSkyShieldValue);
                AbstractCountDownBuff(pbEverDroplet, ref currentEverDroplet);
                // ���{��
                AbstractCountDownBuff(pbHolyBreaker, ref currentHolyBreaker);
                AbstractCountDownBuff(pbExaltedField, ref currentExaltedField);
                AbstractCountDownBuff(pbHymnContract, ref currentHymnContract);
                // ���{��
                AbstractCountDownBuff(pbStarLightning, ref currentStarLightning);
                // �Ł{��
                AbstractCountDownBuff(pbBlackFire, ref currentBlackFire);
                AbstractCountDownBuff(pbBlazingField, ref currentBlazingField, ref currentBlazingFieldFactor);
                // �Ł{��
                currentDeepMirror = false;
                // �Ł{��
                AbstractCountDownBuff(pbWordOfMalice, ref currentWordOfMalice);
                AbstractCountDownBuff(pbSinFortune, ref currentSinFortune);
                // �Ł{��
                AbstractCountDownBuff(pbDarkenField, ref currentDarkenField);
                AbstractCountDownBuff(pbEclipseEnd, ref currentEclipseEnd);
                // �΁{��
                AbstractCountDownBuff(pbFrozenAura, ref currentFrozenAura);
                // �΁{��
                AbstractCountDownBuff(pbEnrageBlast, ref currentEnrageBlast);
                AbstractCountDownBuff(pbSigilOfHomura, ref currentSigilOfHomura);
                // �΁{��
                AbstractCountDownBuff(pbImmolate, ref currentImmolate);
                AbstractCountDownBuff(pbPhantasmalWind, ref currentPhantasmalWind);
                AbstractCountDownBuff(pbRedDragonWill, ref currentRedDragonWill);
                // ���{��
                AbstractCountDownBuff(pbStaticBarrier, ref currentStaticBarrier, ref currentStaticBarrierValue);
                AbstractCountDownBuff(pbAusterityMatrix, ref currentAusterityMatrix);
                // ���{��
                AbstractCountDownBuff(pbBlueDragonWill, ref currentBlueDragonWill);
                // ���{��
                AbstractCountDownBuff(pbSeventhMagic, ref currentSeventhMagic);
                AbstractCountDownBuff(pbParadoxImage, ref currentParadoxImage);
                // ���{��
                AbstractCountDownBuff(pbStanceOfDouble, ref currentStanceOfDouble);
                // ���{�_
                AbstractCountDownBuff(pbSwiftStep, ref currentSwiftStep);
                AbstractCountDownBuff(pbVigorSense, ref currentVigorSense);
                // ���{��
                AbstractCountDownBuff(pbRisingAura, ref currentRisingAura);
                // ���{�S��
                AbstractCountDownBuff(pbOnslaughtHit, ref currentOnslaughtHit, ref currentOnslaughtHitValue);
                // ���{���S
                AbstractCountDownBuff(pbSmoothingMove, ref currentSmoothingMove);
                AbstractCountDownBuff(pbAscensionAura, ref currentAscensionAura);
                // �Á{�_
                AbstractCountDownBuff(pbFutureVision, ref currentFutureVision);
                // �Á{��
                AbstractCountDownBuff(pbReflexSpirit, ref currentReflexSpirit);
                // �Á{�S��
                AbstractCountDownBuff(pbConcussiveHit, ref currentConcussiveHit, ref currentConcussiveHitValue);
                // �Á{���S
                AbstractCountDownBuff(pbTrustSilence, ref currentTrustSilence);
                // �_�{��
                AbstractCountDownBuff(pbStanceOfMystic, ref currentStanceOfMystic, ref currentStanceOfMysticValue);
                // �_�{�S��
                AbstractCountDownBuff(pbNourishSense, ref currentNourishSense);
                // �_�{���S
                AbstractCountDownBuff(pbImpulseHit, ref currentImpulseHit, ref currentImpulseHitValue);
                // ���{�S��
                AbstractCountDownBuff(pbOneAuthority, ref currentOneAuthority);
                // ���{���S
                currentHardestParry = false;
                // �S��{���S
                currentStanceOfSuddenness = false;

                // ������L
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

                // �ŏI�탉�C�t�J�E���g
                //AbstractCountDownBuff(pbLifeCount, ref currentLifeCount, ref currentLifeCountValue); // �^�[���̃N���[���i�b�v�ŃJ�E���g�_�E���͂��Ȃ�
                AbstractCountDownBuff(pbChaoticSchema, ref currentChaoticSchema);

                // ���̉e������
                AbstractCountDownBuff(pbBlinded, ref currentBlinded);
                // SpeedBoost��BattleEnemy�t�H�[�����Ń}�C�i�X�����܂��B
                // CurrentChargeCount�́u���߂�v�R�}���h�̂��߁ACleanUpEffect�ł͌��Z���܂���B
                // CurrentPhysicalChargeCount�́u���߂�v�R�}���h�̂��߁ACleanUpEffect�ł͌��Z���܂���B
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

                // �W���ƒf��
                AbstractCountDownBuff(pbSyutyuDanzetsu, ref currentSyutyu_Danzetsu);               
            }

            // �z�Ɛ���(�R�����g�́A�y�z�Ɛ���z���ʑΏۊO�j
            AbstractCountDownBuff(pbJunkanSeiyaku, ref currentJunkan_Seiyaku);

            // ���̉e������(�y�z�Ɛ���z���ʑΏۊO�j
            AbstractCountDownBuff(pbPreStunning, ref currentPreStunning);
            AbstractCountDownBuff(pbStun, ref currentStunning);
            AbstractCountDownBuff(pbSilence, ref currentSilence);
            AbstractCountDownBuff(pbPoison, ref currentPoison, ref currentPoisonValue);
            AbstractCountDownBuff(pbTemptation, ref currentTemptation);
            AbstractCountDownBuff(pbFrozen, ref currentFrozen);
            AbstractCountDownBuff(pbParalyze, ref currentParalyze);
            AbstractCountDownBuff(pbNoResurrection, ref currentNoResurrection);
            // s ��Ғǉ�
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

            // pbStrengthDown�n�����݂��Ȃ�

            AbstractCountDownBuff(pbLightDown, ref currentLightDown, ref currentLightDownValue);
            AbstractCountDownBuff(pbShadowDown, ref currentShadowDown, ref currentShadowDownValue);
            AbstractCountDownBuff(pbFireDown, ref currentFireDown, ref currentFireDownValue);
            AbstractCountDownBuff(pbIceDown, ref currentIceDown, ref currentIceDownValue);
            AbstractCountDownBuff(pbForceDown, ref currentForceDown, ref currentForceDownValue);
            AbstractCountDownBuff(pbWillDown, ref currentWillDown, ref currentWillDownValue);

            // pbResistLightDown�n�����݂��Ȃ�
            
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
                    //currentChaosDesperate = 0; �O���N���X�Ŕ���ޗ��ɂ��邽�߁A�����ăR�����g�A�E�g
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
            // e ��Ғǉ�

            poolLifeConsumption = 0;
            poolManaConsumption = 0;
            poolSkillConsumption = 0;

            // ���
            // s ��ҁu�R�����g�v�ȉ��́A�퓬���i���ł���A�J�E���g�_�E�����܂߂Ȃ��B
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
            // ��
            AbstractCountUpBuff(pbCounterAttack, ref currentCounterAttack); // ��Ғǉ�
            AbstractCountUpBuff(pbAntiStun, ref currentAntiStun);
            AbstractCountUpBuff(pbStanceOfDeath, ref currentStanceOfDeath);
            // �_
            AbstractCountUpBuff(pbStanceOfFlow, ref currentStanceOfFlow);
            // ��
            AbstractCountUpBuff(pbStanceOfStanding, ref currentStanceOfStanding); // ��Ғǉ�
            // �S��
            AbstractCountUpBuff(pbTruthVision, ref currentTruthVision);
            AbstractCountUpBuff(pbHighEmotionality, ref currentHighEmotionality);
            AbstractCountUpBuff(pbStanceOfEyes, ref currentStanceOfEyes); // ��ҕҏW
            AbstractCountUpBuff(pbPainfulInsanity, ref currentPainfulInsanity);
            // ���S
            AbstractCountUpBuff(pbNegate, ref currentNegate); // ��Ғǉ�
            AbstractCountUpBuff(pbVoidExtraction, ref currentVoidExtraction);
            AbstractCountUpBuff(pbNothingOfNothingness, ref currentNothingOfNothingness);

            // ��
            AbstractCountUpBuff(pbProtection, ref currentProtection);
            AbstractCountUpBuff(pbSaintPower, ref currentSaintPower);
            AbstractCountUpBuff(pbGlory, ref currentGlory);
            // ��
            AbstractCountUpBuff(pbShadowPact, ref currentShadowPact);
            AbstractCountUpBuff(pbBlackContract, ref currentBlackContract);
            AbstractCountUpBuff(pbBloodyVengeance, ref currentBloodyVengeance);
            //AbstractCountUpBuff(pbDamnation, ref currentDamnation); // ���̉e��
            // ��
            AbstractCountUpBuff(pbFlameAura, ref currentFlameAura);
            AbstractCountUpBuff(pbHeatBoost, ref currentHeatBoost);
            AbstractCountUpBuff(pbImmortalRave, ref currentImmortalRave);
            // ��
            AbstractCountUpBuff(pbAbsorbWater, ref currentAbsorbWater);
            AbstractCountUpBuff(pbMirrorImage, ref currentMirrorImage);
            AbstractCountUpBuff(pbPromisedKnowledge, ref currentPromisedKnowledge);
            //AbstractCountUpBuff(pbAbsoluteZero, ref currentAbsoluteZero); // ���̉e��
            // ��
            AbstractCountUpBuff(pbGaleWind, ref currentGaleWind);
            AbstractCountUpBuff(pbWordOfLife, ref currentWordOfLife);
            AbstractCountUpBuff(pbWordOfFortune, ref currentWordOfFortune);
            AbstractCountUpBuff(pbAetherDrive, ref currentAetherDrive);
            AbstractCountUpBuff(pbEternalPresence, ref currentEternalPresence);
            // ��
            AbstractCountUpBuff(pbRiseOfImage, ref currentRiseOfImage);
            AbstractCountUpBuff(pbDeflection, ref currentDeflection);
            AbstractCountUpBuff(pbOneImmunity, ref currentOneImmunity);
            AbstractCountUpBuff(pbTimeStop, ref currentTimeStop);

            // ���{��
            AbstractCountUpBuff(pbPsychicTrance, ref currentPsychicTrance);
            AbstractCountUpBuff(pbBlindJustice, ref currentBlindJustice);
            AbstractCountUpBuff(pbTranscendentWish, ref currentTranscendentWish);
            // ���{��
            //AbstractCountUpBuff(pbFlashBlaze, ref currentFlashBlazeCount); // ���̉e��
            // ���{��
            AbstractCountUpBuff(pbSkyShield, ref currentSkyShield);
            AbstractCountUpBuff(pbEverDroplet, ref currentEverDroplet);
            // ���{��
            AbstractCountUpBuff(pbHolyBreaker, ref currentHolyBreaker);
            AbstractCountUpBuff(pbExaltedField, ref currentExaltedField);
            AbstractCountUpBuff(pbHymnContract, ref currentHymnContract);
            // ���{��
            //AbstractCountUpBuff(pbStarLightning, ref currentStarLightning); // ���̉e��
            // �Ł{��
            //AbstractCountUpBuff(pbBlackFire, ref currentBlackFire); // ���̉e��
            //AbstractCountUpBuff(pbBlazingField, ref currentBlazingField); // ���̉e��
            // �Ł{��
            // �Ł{��
            //AbstractCountUpBuff(pbWordOfMalice, ref currentWordOfMalice); // ���̉e��
            AbstractCountUpBuff(pbSinFortune, ref currentSinFortune);
            // �Ł{��
            //AbstractCountUpBuff(pbDarkenField, ref currentDarkenField); // ���̉e��
            AbstractCountUpBuff(pbEclipseEnd, ref currentEclipseEnd);
            // �΁{��
            AbstractCountUpBuff(pbFrozenAura, ref currentFrozenAura);
            // �΁{��
            //AbstractCountUpBuff(pbEnrageBlast, ref currentEnrageBlast); // ���̉e��
            //AbstractCountUpBuff(pbSigilOfHomura, ref currentSigilOfHomura); // ���̉e��
            // �΁{��
            //AbstractCountUpBuff(pbImmolate, ref currentImmolate); // ���̉e��
            AbstractCountUpBuff(pbPhantasmalWind, ref currentPhantasmalWind);
            AbstractCountUpBuff(pbRedDragonWill, ref currentRedDragonWill);
            // ���{��
            AbstractCountUpBuff(pbStaticBarrier, ref currentStaticBarrier);
            //AbstractCountUpBuff(pbAusterityMatrix, ref currentAusterityMatrix); // ���̉e��
            // ���{��
            AbstractCountUpBuff(pbBlueDragonWill, ref currentBlueDragonWill);
            // ���{��
            AbstractCountUpBuff(pbSeventhMagic, ref currentSeventhMagic);
            AbstractCountUpBuff(pbParadoxImage, ref currentParadoxImage);
            // ���{��
            AbstractCountUpBuff(pbStanceOfDouble, ref currentStanceOfDouble);
            // ���{�_
            AbstractCountUpBuff(pbSwiftStep, ref currentSwiftStep);
            AbstractCountUpBuff(pbVigorSense, ref currentVigorSense);
            // ���{��
            AbstractCountUpBuff(pbRisingAura, ref currentRisingAura);
            // ���{�S��
            //AbstractCountUpBuff(pbOnslaughtHit, ref currentOnslaughtHit); // ���̉e��
            // ���{���S
            AbstractCountUpBuff(pbSmoothingMove, ref currentSmoothingMove);
            AbstractCountUpBuff(pbAscensionAura, ref currentAscensionAura);
            // �Á{�_
            AbstractCountUpBuff(pbFutureVision, ref currentFutureVision);
            // �Á{��
            AbstractCountUpBuff(pbReflexSpirit, ref currentReflexSpirit);
            // �Á{�S��
            //AbstractCountUpBuff(pbConcussiveHit, ref currentConcussiveHit); // ���̉e��
            // �Á{���S
            AbstractCountUpBuff(pbTrustSilence, ref currentTrustSilence);
            // �_�{��
            AbstractCountUpBuff(pbStanceOfMystic, ref currentStanceOfMystic);
            // �_�{�S��
            AbstractCountUpBuff(pbNourishSense, ref currentNourishSense);
            // �_�{���S
            //AbstractCountUpBuff(pbImpulseHit, ref currentImpulseHit); // ���̉e��
            // ���{�S��
            AbstractCountUpBuff(pbOneAuthority, ref currentOneAuthority);
            // ���{���S
            // �S��{���S

            // ������L // ����܂ŉe�����y�ڂ��Ȃ�
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

            // �ŏI�탉�C�t�J�E���g
            //AbstractCountUpBuff(pbLifeCount, ref currentLifeCountValue); �J�E���g�A�b�v����Ώۂł͂Ȃ����߁A�R�����g�A�E�g
            //AbstractCountUpBuff(pbChaoticSchema, ref currentChaoticSchema);  �J�E���g�A�b�v����Ώۂł͂Ȃ����߁A�R�����g�A�E�g
            
            // ���̉e������
            AbstractCountUpBuff(pbBlinded, ref currentBlinded);
            // SpeedBoost��BattleEnemy�t�H�[�����Ń}�C�i�X�����܂��B
            // CurrentChargeCount�́u���߂�v�R�}���h�̂��߁ACleanUpEffect�ł͌��Z���܂���B
            // CurrentPhysicalChargeCount�́u���߂�v�R�}���h�̂��߁ACleanUpEffect�ł͌��Z���܂���B
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

            // �W���ƒf��
            AbstractCountUpBuff(pbSyutyuDanzetsu, ref currentSyutyu_Danzetsu);

            // �z�Ɛ���(�R�����g�́A�y�z�Ɛ���z���ʑΏۊO�j
            AbstractCountUpBuff(pbJunkanSeiyaku, ref currentJunkan_Seiyaku);

            // ���̉e������(�y�z�Ɛ���z���ʑΏۊO�j
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

            // pbStrengthDown�n�����݂��Ȃ�

            //AbstractCountUpBuff(pbLightDown, ref currentLightDown);
            //AbstractCountUpBuff(pbShadowDown, ref currentShadowDown);
            //AbstractCountUpBuff(pbFireDown, ref currentFireDown);
            //AbstractCountUpBuff(pbIceDown, ref currentIceDown);
            //AbstractCountUpBuff(pbForceDown, ref currentForceDown);
            //AbstractCountUpBuff(pbWillDown, ref currentWillDown);

            // pbResistLightDown�n�����݂��Ȃ�

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

        // s ��Ғǉ�
        // �{�X���̉e���������J�o�[
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
        // e ��Ғǉ�

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

        // [�x��]�FBattleEnemy_Load�AMainCharacter:CleanUpEffect, MainCharacter:CleanUpBattleEnd�̓W�J�~�X�����������Ă��܂��B
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
            this.beforeArchetypeName = String.Empty; // ��Ғǉ�
            this.beforeTarget = null;
            this.beforeTarget2 = null; // ��Ғǉ�
            // this.alreadyPlayArchetype = false; [���j�����t���O�͈�������Ȃ��Ɩ߂�Ȃ�]
            PA = PlayerAction.None;
            CurrentUsingItem = String.Empty;
            CurrentSkillName = String.Empty;
            CurrentSpellName = String.Empty;
            CurrentArchetypeName = String.Empty; // ��Ғǉ�
            target = null;
            target2 = null; // ��Ғǉ�

            // �{�X���̉e���������J�o�[
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

            // ��
            // ��
            CurrentCounterAttack = 0; // ��ҕҏW
            CurrentAntiStun = 0;
            CurrentStanceOfDeath = 0;
            // �_
            CurrentStanceOfFlow = 0;
            // ��
            CurrentStanceOfStanding = 0; // ��ҕҏW
            // �S��
            CurrentTruthVision = 0;
            CurrentHighEmotionality = 0;
            CurrentStanceOfEyes = 0; // ��ҕҏW
            CurrentPainfulInsanity = 0;
            // ���S
            CurrentNegate = 0; // ��ҕҏW
            CurrentVoidExtraction = 0;
            CurrentNothingOfNothingness = 0;

            // ��
            CurrentProtection = 0;
            CurrentSaintPower = 0;
            CurrentGlory = 0;
            // ��
            CurrentShadowPact = 0;
            CurrentBlackContract = 0;
            CurrentBloodyVengeance = 0;
            CurrentDamnation = 0;
            // ��
            CurrentFlameAura = 0;
            CurrentHeatBoost = 0;
            CurrentImmortalRave = 0;
            // ��
            CurrentAbsorbWater = 0;
            CurrentMirrorImage = 0;
            CurrentAbsoluteZero = 0;
            CurrentPromisedKnowledge = 0;
            // ��
            CurrentGaleWind = 0;
            CurrentWordOfLife = 0;
            CurrentWordOfFortune = 0;
            CurrentAetherDrive = 0;
            CurrentEternalPresence = 0;
            // ��
            CurrentRiseOfImage = 0;
            CurrentOneImmunity = 0;
            CurrentDeflection = 0;
            CurrentTimeStop = 0;
            CurrentTimeStopImmediate = false;

            // s ��Ғǉ�
            // ���{��
            CurrentPsychicTrance = 0;
            CurrentBlindJustice = 0;
            CurrentTranscendentWish = 0;
            // ���{��
            CurrentFlashBlazeCount = 0;
            // ���{��
            CurrentSkyShield = 0;
            CurrentSkyShieldValue = 0;
            CurrentEverDroplet = 0;
            // ���{��
            CurrentHolyBreaker = 0;
            CurrentExaltedField = 0;
            CurrentHymnContract = 0;
            // ���{��
            CurrentStarLightning = 0;
            // �Ł{��
            CurrentBlackFire = 0;
            CurrentBlazingField = 0;
            CurrentBlazingFieldFactor = 0;
            // �Ł{��
            CurrentDeepMirror = false;
            // �Ł{��
            CurrentWordOfMalice = 0;
            CurrentSinFortune = 0;
            // �Ł{��
            CurrentDarkenField = 0;
            CurrentEclipseEnd = 0;
            // �΁{��
            CurrentFrozenAura = 0;
            // �΁{��
            CurrentEnrageBlast = 0;
            CurrentSigilOfHomura = 0;
            // �΁{��
            CurrentImmolate = 0;
            CurrentPhantasmalWind = 0;
            CurrentRedDragonWill = 0;
            // ���{��
            CurrentStaticBarrier = 0;
            CurrentStaticBarrierValue = 0;
            CurrentAusterityMatrix = 0;
            // ���{��
            CurrentBlueDragonWill = 0;
            // ���{��
            CurrentSeventhMagic = 0;
            CurrentParadoxImage = 0;

            // ���{��
            CurrentStanceOfDouble = 0;
            // ���{�_
            CurrentSwiftStep = 0;
            CurrentVigorSense = 0;
            // ���{��
            CurrentRisingAura = 0;
            // ���{�S��
            CurrentOnslaughtHit = 0;
            CurrentOnslaughtHitValue = 0;
            // ���{���S
            CurrentSmoothingMove = 0;
            CurrentAscensionAura = 0;
            // �Á{�_
            CurrentFutureVision = 0;
            // �Á{��
            CurrentReflexSpirit = 0;
            // �Á{�S��
            CurrentConcussiveHit = 0;
            CurrentConcussiveHitValue = 0;
            // �Á{���S
            CurrentTrustSilence = 0;
            // �_�{��
            CurrentStanceOfMystic = 0;
            CurrentStanceOfMysticValue = 0;
            // �_�{�S��
            CurrentNourishSense = 0;
            // �_�{���S
            CurrentImpulseHit = 0;
            CurrentImpulseHitValue = 0;
            // ���{�S��
            CurrentOneAuthority = 0;
            // ���{���S
            CurrentHardestParry = false;
            // �S��{���S
            CurrentStanceOfSuddenness = false;

            // ������L
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

            // ���Օi���L
            CurrentSagePotionMini = 0;
            CurrentGenseiTaima = 0;
            CurrentShiningAether = 0;
            CurrentBlackElixir = 0;
            CurrentBlackElixirValue = 0;
            CurrentElementalSeal = 0;
            CurrentColoressAntidote = 0;
            
            // �ŏI�탉�C�t�J�E���g
            CurrentLifeCount = 0;
            CurrentLifeCountValue = 0;

            // ���F���[�ŏI��J�I�e�B�b�N�X�L�[�}
            CurrentChaoticSchema = 0;

            // �W���ƒf��
            CurrentSyutyu_Danzetsu = 0;
            CurrentJunkan_Seiyaku = 0;
            // e ��Ғǉ�

            // ��
            // ��
            if (pbCounterAttack != null) { pbCounterAttack.Image = null; pbCounterAttack.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } // ��Ғǉ�
            if (pbAntiStun != null) { pbAntiStun.Image = null; pbAntiStun.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbStanceOfDeath != null) { pbStanceOfDeath.Image = null; pbStanceOfDeath.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // �_
            if (pbStanceOfFlow != null) { pbStanceOfFlow.Image = null; pbStanceOfFlow.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // ��
            if (pbStanceOfStanding != null) { pbStanceOfStanding.Image = null; pbStanceOfStanding.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } // ��Ғǉ�
            // �S��
            if (pbTruthVision != null) { pbTruthVision.Image = null; pbTruthVision.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbHighEmotionality != null) { pbHighEmotionality.Image = null; pbHighEmotionality.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbStanceOfEyes != null) { pbStanceOfEyes.Image = null; pbStanceOfEyes.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } // ��Ғǉ�
            if (pbPainfulInsanity != null) { pbPainfulInsanity.Image = null; pbPainfulInsanity.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // ���S
            if (pbNegate != null) { pbNegate.Image = null; pbNegate.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } // ��Ғǉ�
            if (pbVoidExtraction != null) { pbVoidExtraction.Image = null; pbVoidExtraction.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbNothingOfNothingness != null) { pbNothingOfNothingness.Image = null; pbNothingOfNothingness.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            // ��
            if (pbProtection != null) { pbProtection.Image = null; pbProtection.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbSaintPower != null) { pbSaintPower.Image = null; pbSaintPower.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbGlory != null) { pbGlory.Image = null; pbGlory.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // ��
            if (pbShadowPact != null) { pbShadowPact.Image = null; pbShadowPact.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbBlackContract != null) { pbBlackContract.Image = null; pbBlackContract.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbBloodyVengeance != null) { pbBloodyVengeance.Image = null; pbBloodyVengeance.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbDamnation != null) { pbDamnation.Image = null; pbDamnation.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // ��
            if (pbFlameAura != null) { pbFlameAura.Image = null; pbFlameAura.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbHeatBoost != null) { pbHeatBoost.Image = null; pbHeatBoost.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbImmortalRave != null) { pbImmortalRave.Image = null; pbImmortalRave.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // ��
            if (pbAbsorbWater != null) { pbAbsorbWater.Image = null; pbAbsorbWater.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbMirrorImage != null) { pbMirrorImage.Image = null; pbMirrorImage.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbAbsoluteZero != null) { pbAbsoluteZero.Image = null; pbAbsoluteZero.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbPromisedKnowledge != null) { pbPromisedKnowledge.Image = null; pbPromisedKnowledge.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // ��
            if (pbGaleWind != null) { pbGaleWind.Image = null; pbGaleWind.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbWordOfLife != null) { pbWordOfLife.Image = null; pbWordOfLife.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbWordOfFortune != null) { pbWordOfFortune.Image = null; pbWordOfFortune.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbAetherDrive != null) { pbAetherDrive.Image = null; pbAetherDrive.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbEternalPresence != null) { pbEternalPresence.Image = null; pbEternalPresence.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // ��
            if (pbRiseOfImage != null) { pbRiseOfImage.Image = null; pbRiseOfImage.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbOneImmunity != null) { pbOneImmunity.Image = null; pbOneImmunity.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbDeflection != null) { pbDeflection.Image = null; pbDeflection.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbTimeStop != null) { pbTimeStop.Image = null; pbTimeStop.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // s ��Ғǉ�
            // ���{��
            if (pbPsychicTrance != null) { pbPsychicTrance.Image = null; pbPsychicTrance.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbBlindJustice != null) { pbBlindJustice.Image = null; pbBlindJustice.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbTranscendentWish != null) { pbTranscendentWish.Image = null; pbTranscendentWish.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // ���{��
            if (pbFlashBlaze != null) { pbFlashBlaze.Image = null; pbFlashBlaze.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } // ��Ғǉ�
            // ���{��
            if (pbSkyShield != null) { pbSkyShield.Image = null; pbSkyShield.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbEverDroplet != null) { pbEverDroplet.Image = null; pbEverDroplet.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // ���{��
            if (pbHolyBreaker != null) { pbHolyBreaker.Image = null; pbHolyBreaker.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbExaltedField != null) { pbExaltedField.Image = null; pbExaltedField.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbHymnContract != null) { pbHymnContract.Image = null; pbHymnContract.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // ���{��
            if (pbStarLightning != null) { pbStarLightning.Image = null; pbStarLightning.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // �Ł{��
            if (pbBlackFire != null) { pbBlackFire.Image = null; pbBlackFire.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbBlazingField != null) { pbBlazingField.Image = null; pbBlazingField.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // �Ł{��
            // �Ł{��
            if (pbWordOfMalice != null) { pbWordOfMalice.Image = null; pbWordOfMalice.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbSinFortune != null) { pbSinFortune.Image = null; pbSinFortune.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // �Ł{��
            if (pbDarkenField != null) { pbDarkenField.Image = null; pbDarkenField.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbEclipseEnd != null) { pbEclipseEnd.Image = null; pbEclipseEnd.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // �΁{��
            if (pbFrozenAura != null) { pbFrozenAura.Image = null; pbFrozenAura.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // �΁{��
            if (pbEnrageBlast != null) { pbEnrageBlast.Image = null; pbEnrageBlast.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbSigilOfHomura != null) { pbSigilOfHomura.Image = null; pbSigilOfHomura.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // �΁{��
            if (pbImmolate != null) { pbImmolate.Image = null; pbImmolate.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbPhantasmalWind != null) { pbPhantasmalWind.Image = null; pbPhantasmalWind.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbRedDragonWill != null) { pbRedDragonWill.Image = null; pbRedDragonWill.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // ���{��
            if (pbStaticBarrier != null) { pbStaticBarrier.Image = null; pbStaticBarrier.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbAusterityMatrix != null) { pbAusterityMatrix.Image = null; pbAusterityMatrix.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }           
            // ���{��
            if (pbVanishWave != null) { pbVanishWave.Image = null; pbVanishWave.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbBlueDragonWill != null) { pbBlueDragonWill.Image = null; pbBlueDragonWill.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // ���{��
            if (pbSeventhMagic != null) { pbSeventhMagic.Image = null; pbSeventhMagic.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbParadoxImage != null) { pbParadoxImage.Image = null; pbParadoxImage.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            // ���{��
            if (pbStanceOfDouble != null) { pbStanceOfDouble.Image = null; pbStanceOfDouble.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // ���{�_
            if (pbSwiftStep != null) { pbSwiftStep.Image = null; pbSwiftStep.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbVigorSense != null) { pbVigorSense.Image = null; pbVigorSense.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // ���{��
            if (pbRisingAura != null) { pbRisingAura.Image = null; pbRisingAura.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // ���{�S��
            if (pbOnslaughtHit != null) { pbOnslaughtHit.Image = null; pbOnslaughtHit.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // ���{���S
            if (pbSmoothingMove != null) { pbSmoothingMove.Image = null; pbSmoothingMove.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbAscensionAura != null) { pbAscensionAura.Image = null; pbAscensionAura.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // �Á{�_
            if (pbFutureVision != null) { pbFutureVision.Image = null; pbFutureVision.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // �Á{��
            if (pbReflexSpirit != null) { pbReflexSpirit.Image = null; pbReflexSpirit.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // �Á{�S��
            if (pbConcussiveHit != null) { pbConcussiveHit.Image = null; pbConcussiveHit.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // �Á{���S
            if (pbTrustSilence != null) { pbTrustSilence.Image = null; pbTrustSilence.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // �_�{��
            if (pbStanceOfMystic != null) { pbStanceOfMystic.Image = null; pbStanceOfMystic.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // �_�{�S��
            if (pbNourishSense != null) { pbNourishSense.Image = null; pbNourishSense.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // �_�{���S
            if (pbImpulseHit != null) { pbImpulseHit.Image = null; pbImpulseHit.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // ���{�S��
            if (pbOneAuthority != null) { pbOneAuthority.Image = null; pbOneAuthority.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // ���{���S
            // �S��{���S

            // ������LBUFF
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

            // ���Օi���L
            if (pbSagePotionMini != null) { pbSagePotionMini.Image = null; pbSagePotionMini.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbGenseiTaima != null) { pbGenseiTaima.Image = null; pbGenseiTaima.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbShiningAether != null) { pbShiningAether.Image = null; pbShiningAether.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbBlackElixir != null) { pbBlackElixir.Image = null; pbBlackElixir.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbElementalSeal != null) { pbElementalSeal.Image = null; pbElementalSeal.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbColoressAntidote != null) { pbColoressAntidote.Image = null; pbColoressAntidote.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            // �W���ƒf��
            if (pbSyutyuDanzetsu != null) { pbSyutyuDanzetsu.Image = null; pbSyutyuDanzetsu.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // �z�Ɛ���
            if (pbJunkanSeiyaku != null) { pbJunkanSeiyaku.Image = null; pbJunkanSeiyaku.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            // �ŏI�탉�C�t�J�E���g
            if (pbLifeCount != null) { pbLifeCount.Image = null; pbLifeCount.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }

            // ���F���[�ŏI��J�I�e�B�b�N�X�L�[�}
            if (pbChaoticSchema != null) { pbChaoticSchema.Image = null; pbChaoticSchema.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // e ��Ғǉ�

            // ���̉e������
            CurrentPreStunning = 0;
            CurrentStunning = 0;
            CurrentSilence = 0;
            CurrentPoison = 0;
            CurrentTemptation = 0;
            CurrentFrozen = 0;
            CurrentParalyze = 0;
            CurrentNoResurrection = 0;
            CurrentSlow = 0; // ��Ғǉ�
            CurrentBlind = 0; // ��Ғǉ�
            CurrentSlip = 0; // ��Ғǉ�
            CurrentNoGainLife = 0; // ��Ғǉ�
            CurrentBlinded = 0; // ��Ғǉ�

            // s ��Ғǉ�
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
            // e ��Ғǉ�

            CurrentPoisonValue = 0; // ��Ғǉ�
           
            DeadSignForTranscendentWish = false; // ��Ғǉ�

            // ���̉e������
            CurrentSpeedBoost = 0; // ��Ғǉ�
            CurrentBlinded = 0; // ��Ғǉ�
            CurrentChargeCount = 0; // ��Ғǉ�
            CurrentPhysicalChargeCount = 0; // ��Ғǉ�

            // s ��Ғǉ�
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

            currentStrengthUp = 0; // ��Ғǉ�
            currentStrengthUpValue = 0; // ��Ғǉ�

            currentAgilityUp = 0; // ��Ғǉ�
            currentAgilityUpValue = 0; // ��Ғǉ�

            currentIntelligenceUp = 0; // ��Ғǉ�
            currentIntelligenceUpValue = 0; // ��Ғǉ�

            currentStaminaUp = 0; // ��Ғǉ�
            currentStaminaUpValue = 0; // ��Ғǉ�

            currentMindUp = 0; // ��Ғǉ�
            currentMindUpValue = 0; // ��Ғǉ�

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
            // e ��Ғǉ�

            // s ��Ғǉ�
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
            // e ��Ғǉ�

            if (pbPreStunning != null) { pbPreStunning.Image = null; pbPreStunning.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbStun != null) { pbStun.Image = null; pbStun.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbSilence != null) { pbSilence.Image = null; pbSilence.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbPoison != null) { pbPoison.Image = null; pbPoison.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbTemptation != null) { pbTemptation.Image = null; pbTemptation.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbFrozen != null) { pbFrozen.Image = null; pbFrozen.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbParalyze != null) { pbParalyze.Image = null; pbParalyze.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbNoResurrection != null) { pbNoResurrection.Image = null; pbNoResurrection.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbSlow != null) { pbSlow.Image = null; pbSlow.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } // ��Ғǉ�
            if (pbBlind != null) { pbBlind.Image = null; pbBlind.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } // ��Ғǉ�
            if (pbSlip != null) { pbSlip.Image = null; pbSlip.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } // ��Ғǉ�
            if (pbNoGainLife != null) { pbNoGainLife.Image = null; pbNoGainLife.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); } // ��Ғǉ�

            // s ��Ғǉ�
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
            // e ��Ғǉ�

            // s ��Ғǉ�
            // BUFFUP���ʂ������i�G��p)
            if (pbBuff1 != null) { pbBuff1.Image = null; pbBuff1.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbBuff2 != null) { pbBuff2.Image = null; pbBuff2.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            if (pbBuff3 != null) { pbBuff3.Image = null; pbBuff3.Location = new Point(Database.BUFFPANEL_BUFF_WIDTH, 0); }
            // e ��Ғǉ�

            // BUFFUP���ʂ�����
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

            // s ��Ғǉ�
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
            // e ��Ғǉ�

            // [�H�����ʂ͐퓬�I������p�������] ��Ғǉ�
            // buffStrength_Food = 0;
            // buffAgility_Food = 0;
            // buffIntelligence_Food = 0;
            // buffStamina_Food = 0;
            // buffMind_Food = 0;

            actionDecision = false; // ��Ғǉ�
            decisionTiming = 0; // ��Ғǉ�
            currentInstantPoint = 0; // ��Ғǉ� // �u�R�����g�v���������ł�MAX�l�ɖ߂��Ă����ق��������Ǝv�������A�v���C���Ă݂Ă͂��߂͂O�̂ق����A�Q�[�����͖ʔ�����������Ǝv�����B
            realTimeBattle = false; // ��Ғǉ�
            stackActivation = false; // ��Ғǉ�
            stackActivePlayer = null; // ��Ғǉ�
            stackTarget = null; // ��Ғǉ�
            stackPlayerAction = PlayerAction.None; // ��Ғǉ�
            stackCommandString = string.Empty; // ��Ғǉ�
            //shadowStackActivePlayer = null; // ��Ғǉ�
            //shadowStackTarget = null; // ��Ғǉ�
            //shadowStackPlayerAction = PlayerAction.None; // ��Ғǉ�
            //shadowStackCommandString = String.Empty; // ��Ғǉ�
            BuffNumber = 0; // ��Ғǉ�

            amplifyPhysicalAttack = 0.0f; // ��Ғǉ�
            amplifyPhysicalDefense = 0.0f; // ��Ғǉ�
            amplifyMagicAttack = 0.0f; // ��Ғǉ�
            amplifyMagicDefense = 0.0f; // ��Ғǉ�
            amplifyBattleSpeed = 0.0f; // ��Ғǉ�
            amplifyBattleResponse = 0.0f; // ��Ғǉ�
            amplifyPotential = 0.0f; // ��Ғǉ�

            currentLifeCountValue = 0; // ��Ғǉ�

            reserveBattleCommand = String.Empty; // ��Ғǉ�
            nowExecActionFlag = false; // ��Ғǉ�

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

        // s ��Ғǉ�
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
                // �y�v�����z���U���N�V�����Ώۂɂ��邽�߁A�����Ɋւ�炸�A�ΏۂƂ��鎖������
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
        /// ���̃X�L�����ʂ������[�u
        /// </summary>
        public void RemoveBuffSkill()
        {
            // ��{�X�L��
            this.RemoveAntiStun();
            this.RemoveStanceOfDeath();
            this.RemoveTruthVision();
            this.RemovePainfulInsanity(); // �_���[�W�n�������Ώۂ͎������g�Ȃ̂�UP����
            this.RemoveVoidExtraction();
            this.RemoveNothingOfNothingness();
            // �����X�L��
            this.RemoveRisingAura();
            this.RemoveAscensionAura();
            this.RemoveReflexSpirit();
            this.RemoveTrustSilence();
            this.RemoveStanceOfMystic();
            this.RemoveNourishSense();
        }

        /// <summary>
        /// ���̃X�L�����ʂ������[�u
        /// </summary>
        public void RemoveDebuffSkill()
        {
            // ��{�X�L��
            // �����X�L��
            this.RemoveOnslaughtHit();
            this.RemoveConcussiveHit();
            this.RemoveImpulseHit();
        }

        /// <summary>
        /// ���̖��@���ʂ������[�u
        /// </summary>
        public void RemoveBuffSpell()
        {
            // ��{�X�y��
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
            // �����X�y��
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
        /// ���̖��@���ʂ������[�u
        /// </summary>
        public void RemoveDebuffSpell()
        {
            // ��{�X�y��
            this.RemoveDamnation();
            this.RemoveAbsoluteZero();
            // �����X�y��
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
        /// ���̃p�����^���ʂ������[�u
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
        /// ���̃p�����^���ʂ������[�u
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
        /// ���̉e�����ʂ������[�u
        /// </summary>
        public void RemoveBuffEffect()
        {
            this.RemoveBlinded();
            this.RemoveSpeedBoost();
            this.RemoveChargeCount();
            this.RemovePhysicalChargeCount();
        }

        /// <summary>
        /// ���̉e�����ʂ������[�u
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
        /// �̓p�����^�㏸BUFF
        /// </summary>
        /// <param name="effectValue">���ʂ̒l</param>
        /// <param name="turn">�^�[�����i�w�肵�Ȃ��ꍇ��999�^�[���j</param>
        public void BuffUpStrength(double effectValue, int turn = 999)
        {
            UpdateBattleText(this.Name + "�́y�́z��" + ((int)effectValue).ToString() + "�㏸\r\n");
            this.CurrentStrengthUp = turn;
            this.CurrentStrengthUpValue = (int)effectValue;
            this.ActivateBuff(this.pbStrengthUp, Database.BaseResourceFolder + Database.BUFF_STRENGTH_UP, turn);
        }

        /// <summary>
        /// �Z�p�����^�㏸BUFF
        /// </summary>
        /// <param name="effectValue">���ʂ̒l</param>
        /// <param name="turn">�^�[�����i�w�肵�Ȃ��ꍇ��999�^�[���j</param>
        public void BuffUpAgility(double effectValue, int turn = 999)
        {
            UpdateBattleText(this.Name + "�́y�Z�z��" + ((int)effectValue).ToString() + "�㏸\r\n");
            this.CurrentAgilityUp = turn;
            this.CurrentAgilityUpValue = (int)effectValue;
            this.ActivateBuff(this.pbAgilityUp, Database.BaseResourceFolder + Database.BUFF_AGILITY_UP, turn);
        }

        /// <summary>
        /// �m�p�����^�㏸BUFF
        /// </summary>
        /// <param name="effectValue">���ʂ̒l</param>
        /// <param name="turn">�^�[�����i�w�肵�Ȃ��ꍇ��999�^�[���j</param>
        public void BuffUpIntelligence(double effectValue, int turn = 999)
        {
            UpdateBattleText(this.Name + "�́y�m�z��" + ((int)effectValue).ToString() + "�㏸\r\n");
            this.CurrentIntelligenceUp = turn;
            this.CurrentIntelligenceUpValue = (int)effectValue;
            this.ActivateBuff(this.pbIntelligenceUp, Database.BaseResourceFolder + Database.BUFF_INTELLIGENCE_UP, turn);
        }

        /// <summary>
        /// �̃p�����^�㏸BUFF
        /// </summary>
        /// <param name="effectValue">���ʂ̒l</param>
        /// <param name="turn">�^�[�����i�w�肵�Ȃ��ꍇ��999�^�[���j</param>
        public void BuffUpStamina(double effectValue, int turn = 999)
        {
            UpdateBattleText(this.Name + "�́y�́z��" + ((int)effectValue).ToString() + "�㏸\r\n");
            this.CurrentStaminaUp = turn;
            this.CurrentStaminaUpValue = (int)effectValue;
            this.ActivateBuff(this.pbStaminaUp, Database.BaseResourceFolder + Database.BUFF_STAMINA_UP, turn);
        }

        /// <summary>
        /// �S�p�����^�㏸BUFF
        /// </summary>
        /// <param name="effectValue">���ʂ̒l</param>
        /// <param name="turn">�^�[�����i�w�肵�Ȃ��ꍇ��999�^�[���j</param>
        public void BuffUpMind(double effectValue, int turn = 999)
        {
            UpdateBattleText(this.Name + "�́y�S�z��" + ((int)effectValue).ToString() + "�㏸\r\n");
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
        // e ��Ғǉ�
    }
}
