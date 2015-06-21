using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DungeonPlayer
{
    public static class Database
    {
        public static int VERSION = 1; // // 640 x 480����1024 x 768�֕ύX�A�v���C���[�ʒu���݊�
        public static int WIDTH_1024 = 1024; // ��Ғǉ�
        public static int WIDTH_OK_BUTTON = 120; // ��Ғǉ�
        public static int HEIGHT_768 = 768; // ��Ғǉ�
        public static int HEIGHT_MAIN_MESSAGE = 60; // ��Ғǉ�
        
        public static int MAX_SAVEDATA = 10; // �Z�[�u�f�[�^�ő�ێ���

        public static int DUNGEON_ROW = 20; // �s
        public static int DUNGEON_COLUMN = 30; // ��

        public static int DUNGEON_MOVE_LEN = 25;
        public static int DUNGEON_BASE_X = 0;
        public static int DUNGEON_BASE_Y = 0;

        public static int ENCOUNT_ENEMY = 0;// �G�ւ̑�����
        public static int FIRST_POS = 2; // ���֖߂��O��2 ���݂��t�����V�X��O��325 �Q�K�K�i��O��598�@Lizenos��O��82, �R�K�K�i��O��22

        public static int MAX_BACKPACK_SIZE = 20; // 10->20 ��ҕҏW
        public static int MAX_ITEM_STACK_SIZE = 10; // ��Ғǉ�

        public static string NODE_MAINPLAYERSTATUS = "MainPlayerStatus";
        public static string NODE_SECONDPLAYERSTATUS = "SecondPlayerStatus";
        public static string NODE_THIRDPLAYERSTATUS = "ThirdPlayerStatus";

        public static int MAX_BATTLE_NUMBER = 7;

        public static int MAX_GAMEDAY = 100;

        public static int MAX_ITEM_BANK = 100;

        public static int TOTAL_COMMAND_NUM = 150; // ��Ғǉ� (8 + 42 + 24 + 45 + 30 + 1)
        public static int TOTAL_SPELL_NUM = 87; // ��Ғǉ��i42 + 45)
        public static int TOTAL_SKILL_NUM = 54; // ��Ғǉ��i24 + 30�j

        public static int BASE_TIMER_BAR_LENGTH = 500;

        public static int TIMEUP_FIRST_RESPONSE = 600; // ��Ғǉ�

        // s ��Ғǉ�
        #region "�L�����N�^�[����"
        public const string EIN_WOLENCE = "�A�C��";
        public const string EIN_WOLENCE_FULL = "�A�C���E�E�H�[�����X";

        public const string RANA_AMILIA = "���i";
        public const string RANA_AMILIA_FULL = "���i�E�A�~���A";

        public const string VERZE_ARTIE = "���F���[";
        public const string VERZE_ARTIE_FULL = "���F���[�E�A�[�e�B";

        public const string OL_LANDIS = "�����f�B�X";
        public const string OL_LANDIS_FULL = "�I���E�����f�B�X";

        public const string SINIKIA_KAHLHANZ = "�J�[���n���c";
        public const string SINIKIA_KAHLHANZ_FULL = "�V�j�L�A�E�J�[���n���c";
        #endregion
        #region "�L�����N�^�[�J���["
        public static Color COLOR_EIN = Color.LightSkyBlue;
        public static Color COLOR_RANA = Color.Pink;
        public static Color COLOR_VERZE = Color.Silver;
        public static Color COLOR_OL = Color.Gold;
        public static Color COLOR_KAHL = Color.SlateBlue;

        public static Color COLOR_BOX_EIN = Color.Blue;
        public static Color COLOR_BOX_RANA = Color.Red;
        public static Color COLOR_BOX_VERZE = Color.Gray;
        public static Color COLOR_BOX_OL = Color.Yellow;
        public static Color COLOR_BOX_KAHL = Color.Purple;

        public static Color COLOR_BATTLE_EIN = Color.DeepSkyBlue;
        public static Color COLOR_BATTLE_RANA = Color.Violet;
        public static Color COLOR_BATTLE_VERZE = Color.SlateGray;
        public static Color COLOR_BATTLE_OL = Color.Yellow;
        public static Color COLOR_BATTLE_KAHL = Color.SlateBlue;

        public static Color COLOR_BATTLE_TARGET1_EIN = Color.DarkBlue;
        public static Color COLOR_BATTLE_TARGET1_RANA = Color.HotPink;
        public static Color COLOR_BATTLE_TARGET1_VERZE = Color.SlateGray;
        public static Color COLOR_BATTLE_TARGET1_OL = Color.Gold;
        public static Color COLOR_BATTLE_TARGET1_KAHL = Color.DarkSlateBlue;

        public static Color COLOR_BATTLE_TARGET2_EIN = Color.Blue;
        public static Color COLOR_BATTLE_TARGET2_RANA = Color.Pink;
        public static Color COLOR_BATTLE_TARGET2_VERZE = Color.LightGray;
        public static Color COLOR_BATTLE_TARGET2_OL = Color.Yellow;
        public static Color COLOR_BATTLE_TARGET2_KAHL = Color.SlateBlue;
        #endregion
        // e ��Ғǉ�

        // s ��Ғǉ�
        public const string STRING_LIGHT = "�y���ϐ��z";
        public const string STRING_SHADOW = "�y�őϐ��z";
        public const string STRING_FIRE = "�y�Αϐ��z";
        public const string STRING_ICE = "�y���ϐ��z";
        public const string STRING_FORCE = "�y���ϐ��z";
        public const string STRING_WILL = "�y��ϐ��z";
        public const string STRING_ACTIVE = "�y���z";
        public const string STRING_PASSIVE = "�y�Áz";
        public const string STRING_SOFT = "�y�_�z";
        public const string STRING_HARD = "�y���z";
        public const string STRING_TRUTH = "�y�S��z";
        public const string STRING_VOID = "�y���S�z";

        public const string STRING_STUNNING = "�y�X�^���z";
        public const string STRING_SILENCE = "�y���فz";
        public const string STRING_POISON = "�y�ғŁz";
        public const string STRING_TEMPTATION = "�y�U�f�z";
        public const string STRING_FROZEN = "�y�����z";
        public const string STRING_PARALYZE = "�y��Ⴡz";
        public const string STRING_SLOW = "�y�݉��z";
        public const string STRING_BLIND = "�y�ÈŁz";
        public const string STRING_SLIP = "�y�X���b�v�z";
        public const string STRING_NORESURRECTION = "�y�����s�z";
        public const string STRING_NOGAIN_LIFE = "�y���C�t�񕜕s�z";
        // e ��Ғǉ�

        public static string POWER_STR = "�y�́z";
        public static string POWER_AGL = "�y�Z�z";
        public static string POWER_INT = "�y�m�z";
        public static string POWER_STM = "�y�́z";
        public static string POWER_MND = "�y�S�z";

        #region "���@����"
        // [��]
        public const string FRESH_HEAL = "FreshHeal";
        public const string PROTECTION = "Protection";
        public const string HOLY_SHOCK = "HolyShock";
        public const string SAINT_POWER = "SaintPower";
        public const string GLORY = "Glory";
        public const string RESURRECTION = "Resurrection";
        public const string CELESTIAL_NOVA = "CelestialNova";
        // [��]
        public const string DARK_BLAST = "DarkBlast";
        public const string SHADOW_PACT = "ShadowPact";
        public const string LIFE_TAP = "Lifetap";
        public const string BLACK_CONTRACT = "BlackContract";
        public const string DEVOURING_PLAGUE = "DevouringPlague"; // e ��ҕҏW�i�X�y���~�X�j
        public const string BLOODY_VENGEANCE = "BloodyVengeance";
        public const string DAMNATION = "Damnation";
        // [��]
        public const string FIRE_BALL = "FireBall";
        public const string FLAME_AURA = "FlameAura";
        public const string HEAT_BOOST = "HeatBoost";
        public const string FLAME_STRIKE = "FlameStrike";
        public const string VOLCANIC_WAVE = "VolcanicWave";
        public const string IMMORTAL_RAVE = "ImmortalRave";
        public const string LAVA_ANNIHILATION = "LavaAnnihilation";
        // [��]
        public const string ICE_NEEDLE = "IceNeedle";
        public const string ABSORB_WATER = "AbsorbWater";
        public const string CLEANSING = "Cleansing";
        public const string FROZEN_LANCE = "FrozenLance";
        public const string MIRROR_IMAGE = "MirrorImage";
        public const string PROMISED_KNOWLEDGE = "PromisedKnowledge";
        public const string ABSOLUTE_ZERO = "AbsoluteZero";
        // [��]
        public const string WORD_OF_POWER = "WordOfPower";
        public const string GALE_WIND = "GaleWind";
        public const string WORD_OF_LIFE = "WordOfLife";
        public const string WORD_OF_FORTUNE = "WordOfFortune";
        public const string AETHER_DRIVE = "AetherDrive";
        public const string GENESIS = "Genesis";
        public const string ETERNAL_PRESENCE = "EternalPresence";
        // [��]
        public const string DISPEL_MAGIC = "DispelMagic";
        public const string RISE_OF_IMAGE = "RiseOfImage";
        public const string DEFLECTION = "Deflection";
        public const string TRANQUILITY = "Tranquility";
        public const string ONE_IMMUNITY = "OneImmunity";
        public const string WHITE_OUT = "WhiteOut";
        public const string TIME_STOP = "TimeStop";

        // s ��Ғǉ�
        // [��]
        public const string FRESH_HEAL_JP = "�t���b�V���E�q�[��";
        public const string PROTECTION_JP = "�v���e�N�V����";
        public const string HOLY_SHOCK_JP = "�z�[���[�E�V���b�N";
        public const string SAINT_POWER_JP = "�Z�C���g�E�p���[";
        public const string GLORY_JP = "�O���[���[";
        public const string RESURRECTION_JP = "���U���N�V����";
        public const string CELESTIAL_NOVA_JP = "�Z���X�e�B�A���E�m���@";
        // [��]
        public const string DARK_BLAST_JP = "�_�[�N�E�u���X�g";
        public const string SHADOW_PACT_JP = "�V���h�E�E�p�N�g";
        public const string LIFE_TAP_JP = "���C�t�E�^�b�v";
        public const string BLACK_CONTRACT_JP = "�u���b�N�E�R���g���N�g";
        public const string DEVOURING_PLAGUE_JP = "�f���H�����O�E�v���O�[";
        public const string BLOODY_VENGEANCE_JP = "�u���b�f�B�E���F���W�F���X";
        public const string DAMNATION_JP = "�_���l�[�V����";
        // [��]
        public const string FIRE_BALL_JP = "�t�@�C�A�E�{�[��";
        public const string FLAME_AURA_JP = "�t���C���E�I�[��";
        public const string HEAT_BOOST_JP = "�q�[�g�E�u�[�X�g";
        public const string FLAME_STRIKE_JP = "�t���C���E�X�g���C�N";
        public const string VOLCANIC_WAVE_JP = "���H���J�j�b�N�E�E�F�C�u";
        public const string IMMORTAL_RAVE_JP = "�C���[�^���E���C�u";
        public const string LAVA_ANNIHILATION_JP = "�����@�E�A�j�q���[�V����";
        // [��]
        public const string ICE_NEEDLE_JP = "�A�C�X�E�j�[�h��";
        public const string ABSORB_WATER_JP = "�A�u�\�[�u�E�E�H�[�^�[";
        public const string CLEANSING_JP = "�N���[���W���O";
        public const string FROZEN_LANCE_JP = "�t���[�Y���E�����X";
        public const string MIRROR_IMAGE_JP = "�~���[�E�C���[�W";
        public const string PROMISED_KNOWLEDGE_JP = "�v���~�X�h�E�i���b�W";
        public const string ABSOLUTE_ZERO_JP = "�A�u�\�����[�g�E�[��";
        // [��]
        public const string WORD_OF_POWER_JP = "���[�h�E�I�u�E�p���[";
        public const string GALE_WIND_JP = "�Q�C���E�E�B���h";
        public const string WORD_OF_LIFE_JP = "���[�h�E�I�u�E���C�t";
        public const string WORD_OF_FORTUNE_JP = "���[�h�E�I�u�E�t�H�[�`����";
        public const string AETHER_DRIVE_JP = "�G�[�e���E�h���C�u";
        public const string GENESIS_JP = "�W�F�l�V�X";
        public const string ETERNAL_PRESENCE_JP = "�G�^�[�i���E�v���[���X";
        // [��]
        public const string DISPEL_MAGIC_JP = "�f�B�X�y���E�}�W�b�N";
        public const string RISE_OF_IMAGE_JP = "���C�Y�E�I�u�E�C���[�W";
        public const string DEFLECTION_JP = "�f�t���N�V����";
        public const string TRANQUILITY_JP = "�g�����L�B���e�B";
        public const string ONE_IMMUNITY_JP = "�����E�C���[�j�e�B";
        public const string WHITE_OUT_JP = "�z���C�g�E�A�E�g";
        public const string TIME_STOP_JP = "�^�C���X�g�b�v";
        // e ��Ғǉ�

        // s ��Ғǉ�
        // [���@��]�i���S�t�j
        public const string PSYCHIC_TRANCE = "PsychicTrance";
        public const string PSYCHIC_TRANCE_JP = "�T�C�L�b�N�E�g�����X";
        public const string BLIND_JUSTICE = "BlindJustice";
        public const string BLIND_JUSTICE_JP = "�u���C���h�E�W���X�e�B�X";
        public const string TRANSCENDENT_WISH = "TranscendentWish";
        public const string TRANSCENDENT_WISH_JP = "�g���b�Z���f���g�E�E�B�b�V��";

        // [���@��]
        public const string FLASH_BLAZE = "FlashBlaze";
        public const string FLASH_BLAZE_JP = "�t���b�V���E�u���C�Y";
        public const string LIGHT_DETONATOR = "LightDetonator";
        public const string LIGHT_DETONATOR_JP = "���C�g�E�f�g�l�C�^�[";
        public const string ASCENDANT_METEOR = "AscendantMeteor";
        public const string ASCENDANT_METEOR_JP = "�A�Z���_���g�E���e�I";

        // [���@��]
        public const string SKY_SHIELD = "SkyShield";
        public const string SKY_SHIELD_JP = "�X�J�C�E�V�[���h";
        public const string SACRED_HEAL = "SacredHeal";
        public const string SACRED_HEAL_JP = "�T�[�N���b�h�E�q�[��";
        public const string EVER_DROPLET = "EverDroplet";
        public const string EVER_DROPLET_JP = "�G���@�[�E�h���b�v���b�g";

        // [���@��]
        public const string HOLY_BREAKER = "HolyBreaker";
        public const string HOLY_BREAKER_JP = "�z�[���[�E�u���C�J�[";
        public const string EXALTED_FIELD = "ExaltedField";
        public const string EXALTED_FIELD_JP = "�G�O�U���e�B�b�h�E�t�B�[���h";
        public const string HYMN_CONTRACT = "HymnContract";
        public const string HYMN_CONTRACT_JP = "�q�����E�R���g���N�g";

        // [���@��]
        public const string STAR_LIGHTNING = "StarLightning";
        public const string STAR_LIGHTNING_JP = "�X�^�[�E���C�g�j���O";
        public const string ANGEL_BREATH = "AngelBreath";
        public const string ANGEL_BREATH_JP = "�G���W�F���E�u���X";
        public const string ENDLESS_ANTHEM = "EndlessAnthem";
        public const string ENDLESS_ANTHEM_JP = "�G���h���X�E�A���Z��";

        // [�Ł@��]
        public const string BLACK_FIRE = "BlackFire";
        public const string BLACK_FIRE_JP = "�u���b�N�E�t�@�C�A";
        public const string BLAZING_FIELD = "BlazingField";
        public const string BLAZING_FIELD_JP = "�u���C�W���O�E�t�B�[���h";
        public const string DEMONIC_IGNITE = "DemonicIgnite";
        public const string DEMONIC_IGNITE_JP = "�f�[���j�b�N�E�C�O�i�C�g";

        // [�Ł@��]
        public const string BLUE_BULLET = "BlueBullet";
        public const string BLUE_BULLET_JP = "�u���[�E�o���b�g";
        public const string DEEP_MIRROR = "DeepMirror";
        public const string DEEP_MIRROR_JP = "�f�B�[�v�E�~���[";
        public const string DEATH_DENY = "DeathDeny";
        public const string DEATH_DENY_JP = "�f�X�E�f�B�i�C";

        // [�Ł@��]
        public const string WORD_OF_MALICE = "WordOfMalice";
        public const string WORD_OF_MALICE_JP = "���[�h�E�I�u�E�}���X";
        public const string ABYSS_EYE = "AbyssEye";
        public const string ABYSS_EYE_JP = "�A�r�X�E�A�C";
        public const string SIN_FORTUNE = "SinFortune";
        public const string SIN_FORTUNE_JP = "�V���E�t�H�[�`����";

        // [�Ł@��]
        public const string DARKEN_FIELD = "DarkenField";
        public const string DARKEN_FIELD_JP = "�_�[�P���E�t�B�[���h";
        public const string DOOM_BLADE = "DoomBlade";
        public const string DOOM_BLADE_JP = "�h�D�[���E�u���C�h";
        public const string ECLIPSE_END = "EclipseEnd";
        public const string ECLIPSE_END_JP = "�G�N���v�X�E�G���h";

        // [�΁@��]�i���S�t�j
        public const string FROZEN_AURA = "FrozenAura";
        public const string FROZEN_AURA_JP = "�t���[�Y���E�I�[��";
        public const string CHILL_BURN = "ChillBurn";
        public const string CHILL_BURN_JP = "�`���E�o�[��";
        public const string ZETA_EXPLOSION = "ZetaExplosion";
        public const string ZETA_EXPLOSION_JP = "�[�[�^�E�G�N�X�v���[�W����";

        // [�΁@��]
        public const string ENRAGE_BLAST = "EnrageBlast";
        public const string ENRAGE_BLAST_JP = "�G�����C�W�E�u���X�g";
        public const string PIERCING_FLAME = "PiercingFlame";
        public const string PIERCING_FLAME_JP = "�s�A�[�V���O�E�t���C��";
        public const string SIGIL_OF_HOMURA = "SigilOfHomura";
        public const string SIGIL_OF_HOMURA_JP = "���̏���";

        // [�΁@��]
        public const string IMMOLATE = "Immolate";
        public const string IMMOLATE_JP = "�C���[���C�g";
        public const string PHANTASMAL_WIND = "PhantasmalWind";
        public const string PHANTASMAL_WIND_JP = "�t�@���^�Y�}���E�E�B���h";
        public const string RED_DRAGON_WILL = "RedDragonWill";
        public const string RED_DRAGON_WILL_JP = "���b�h�E�h���S���E�E�B��";

        // [���@��]
        public const string WORD_OF_ATTITUDE = "WordOfAttitude";
        public const string WORD_OF_ATTITUDE_JP = "���[�h�E�I�u�E�A�e�B�`���[�h";
        public const string STATIC_BARRIER = "StaticBarrier";
        public const string STATIC_BARRIER_JP = "�X�^�e�B�b�N�E�o���A";
        public const string AUSTERITY_MATRIX = "AusterityMatrix";
        public const string AUSTERITY_MATRIX_JP = "�A�D�X�e���e�B�E�}�g���N�X";

        // [���@��]
        public const string VANISH_WAVE = "VanishWave";
        public const string VANISH_WAVE_JP = "���@�j�b�V���E�E�F�C��";
        public const string VORTEX_FIELD = "VortexField";
        public const string VORTEX_FIELD_JP = "���H���e�N�X�E�t�B�[���h";
        public const string BLUE_DRAGON_WILL = "BlueDragonWill";
        public const string BLUE_DRAGON_WILL_JP = "�u���[�E�h���S���E�E�B��";

        // [���@��]�i���S�t�j
        public const string SEVENTH_MAGIC = "SeventhMagic";
        public const string SEVENTH_MAGIC_JP = "�Z�u���X�E�}�W�b�N";
        public const string PARADOX_IMAGE = "ParadoxImage";
        public const string PARADOX_IMAGE_JP = "�p���h�b�N�X�E�C���[�W";
        public const string WARP_GATE = "WarpGate";
        public const string WARP_GATE_JP = "���[�v�E�Q�[�g";
        // e ��Ғǉ�
        #endregion

        #region "�X�L������"
        // [��]
        public const string STRAIGHT_SMASH = "StraightSmash";
        public const string DOUBLE_SLASH = "DoubleSlash";
        public const string CRUSHING_BLOW = "CrushingBlow";
        public const string SOUL_INFINITY = "SoulInfinity";
        // [��]
        public const string COUNTER_ATTACK = "CounterAttack";
        public const string PURE_PURIFICATION = "PurePurification";
        public const string ANTI_STUN = "AntiStun";
        public const string STANCE_OF_DEATH = "StanceOfDeath";
        // [�_]
        public const string STANCE_OF_FLOW = "StanceOfFlow";
        public const string ENIGMA_SENSE = "EnigmaSense";
        public const string SILENT_RUSH = "SilentRush";
        public const string OBORO_IMPACT = "OboroImpact";
        // [��]
        public const string STANCE_OF_STANDING = "StanceOfStanding";
        public const string INNER_INSPIRATION = "InnerInspiration";
        public const string KINETIC_SMASH = "KineticSmash";
        public const string CATASTROPHE = "Catastrophe";
        // [�S��]
        public const string TRUTH_VISION = "TruthVision";
        public const string HIGH_EMOTIONALITY = "HighEmotionality";
        public const string STANCE_OF_EYES = "StanceOfEyes";
        public const string PAINFUL_INSANITY = "PainfulInsanity";
        // [���S]
        public const string NEGATE = "Negate"; // e ��ҕҏW
        public const string VOID_EXTRACTION = "VoidExtraction";
        public const string CARNAGE_RUSH = "CarnageRush";
        public const string NOTHING_OF_NOTHINGNESS = "NothingOfNothingness";

        // s ��Ғǉ�
        // [��]
        public const string STRAIGHT_SMASH_JP = "�X�g���[�g�E�X�}�b�V��";
        public const string DOUBLE_SLASH_JP = "�_�u���E�X���b�V��";
        public const string CRUSHING_BLOW_JP = "�N���b�V���O�E�u���[";
        public const string SOUL_INFINITY_JP = "�\�E���E�C���t�B�j�e�B";
        // [��]
        public const string COUNTER_ATTACK_JP = "�J�E���^�[�E�A�^�b�N";
        public const string PURE_PURIFICATION_JP = "�s���A�E�s�����t�@�C�P�[�V����";
        public const string ANTI_STUN_JP = "�A���`�E�X�^��";
        public const string STANCE_OF_DEATH_JP = "�X�^���X�E�I�u�E�f�X";
        // [�_]
        public const string STANCE_OF_FLOW_JP = "�X�^���X�E�I�u�E�t���[";
        public const string ENIGMA_SENSE_JP = "�G�j�O�}�E�Z���X";
        public const string SILENT_RUSH_JP = "�T�C�����g�E���b�V��";
        public const string OBORO_IMPACT_JP = "�O�E�C���p�N�g";
        // [��]
        public const string STANCE_OF_STANDING_JP = "�X�^���X�E�I�u�E�X�^���f�B���O";
        public const string INNER_INSPIRATION_JP = "�C���i�[�E�C���X�s���[�V����";
        public const string KINETIC_SMASH_JP = "�L�l�e�B�b�N�E�X�}�b�V��";
        public const string CATASTROPHE_JP = "�J�^�X�g���t�B";
        // [�S��]
        public const string TRUTH_VISION_JP = "�g�D���X�E���B�W����";
        public const string HIGH_EMOTIONALITY_JP = "�n�C�E�G���[�V���i���e�B";
        public const string STANCE_OF_EYES_JP = "�X�^���X�E�I�u�E�A�C�Y";
        public const string PAINFUL_INSANITY_JP = "�y�C���t���E�C���T�j�e�B";
        // [���S]
        public const string NEGATE_JP = "�j�Q�C�g";
        public const string VOID_EXTRACTION_JP = "���H�C�h�E�G�N�X�g���N�V����";
        public const string CARNAGE_RUSH_JP = "�J���l�[�W�E���b�V��";
        public const string NOTHING_OF_NOTHINGNESS_JP = "�i�b�V���O�E�I�u�E�i�b�V���O�l�X";
        // e ��Ғǉ�

        // s ��Ғǉ�
        // [���@��]�i���S�t�j
        public const string NEUTRAL_SMASH = "NeutralSmash";
        public const string NEUTRAL_SMASH_JP = "�j���[�g�����E�X�}�b�V��";
        public const string STANCE_OF_DOUBLE = "StanceOfDouble";
        public const string STANCE_OF_DOUBLE_JP = "�X�^���X�E�I�u�E�_�u��";

        // [���@�_]
        public const string SWIFT_STEP = "SwiftStep";
        public const string SWIFT_STEP_JP = "�X�E�B�t�g�E�X�e�b�v";
        public const string VIGOR_SENSE = "VigorSense";
        public const string VIGOR_SENSE_JP = "���B�S�[�E�Z���X";

        // [���@��]
        public const string CIRCLE_SLASH = "CircleSlash";
        public const string CIRCLE_SLASH_JP = "�T�[�N���E�X���b�V��";
        public const string RISING_AURA = "RisingAura";
        public const string RISING_AURA_JP = "���C�W���O�E�I�[��";

        // [���@�S��]
        public const string RUMBLE_SHOUT = "RumbleShout";
        public const string RUMBLE_SHOUT_JP = "�����u���E�V���E�g";
        public const string ONSLAUGHT_HIT = "OnslaughtHit";
        public const string ONSLAUGHT_HIT_JP = "�I���X���[�g�E�q�b�g";

        // [���@���S]
        public const string SMOOTHING_MOVE = "SmoothingMove";
        public const string SMOOTHING_MOVE_JP = "�X���[�W���O�E���[�u";
        public const string ASCENSION_AURA = "AscensionAura";
        public const string ASCENSION_AURA_JP = "�A�Z���V�����E�I�[��";

        // [�Á@�_]
        public const string FUTURE_VISION = "FutureVision";
        public const string FUTURE_VISION_JP = "�t���[�`���[�E���B�W����";
        public const string UNKNOWN_SHOCK = "UnknownShock";
        public const string UNKNOWN_SHOCK_JP = "�A���m�E���E�V���b�N";

        // [�Á@��]
        public const string REFLEX_SPIRIT = "ReflexSpirit";
        public const string REFLEX_SPIRIT_JP = "���t���b�N�X�E�X�s���b�g";
        public const string FATAL_BLOW = "FatalBlow";
        public const string FATAL_BLOW_JP = "�t�F�C�^���E�u���[";

        // [�Á@�S��]
        public const string SHARP_GLARE = "SharpGlare";
        public const string SHARP_GLARE_JP = "�V���[�v�E�O���A";
        public const string CONCUSSIVE_HIT = "ConcussiveHit";
        public const string CONCUSSIVE_HIT_JP = "�R���J�b�V���E�q�b�g";

        // [�Á@���S]
        public const string TRUST_SILENCE = "TrustSilence";
        public const string TRUST_SILENCE_JP = "�g���X�g�E�T�C�����X";
        public const string MIND_KILLING = "MindKilling";
        public const string MIND_KILLING_JP = "�}�C���h�E�L�����O";

        // [�_�@��]�i���S�t�j
        public const string SURPRISE_ATTACK = "SurpriseAttack";
        public const string SURPRISE_ATTACK_JP = "�T�v���C�Y�E�A�^�b�N";
        public const string STANCE_OF_MYSTIC = "StanceOfMystic";
        public const string STANCE_OF_MYSTIC_JP = "�X�^���X�E�I�u�E�~�X�e�B�b�N";

        // [�_�@�S��]
        public const string PSYCHIC_WAVE = "PsychicWave";
        public const string PSYCHIC_WAVE_JP = "�T�C�L�b�N�E�E�F�C��";
        public const string NOURISH_SENSE = "NourishSense";
        public const string NOURISH_SENSE_JP = "�m���b�V���E�Z���X";

        // [�_�@���S]
        public const string RECOVER = "Recover";
        public const string RECOVER_JP = "���J�o�[";
        public const string IMPULSE_HIT = "ImpulseHit";
        public const string IMPULSE_HIT_JP = "�C���p���X�E�q�b�g";

        // [���@�S��]
        public const string VIOLENT_SLASH = "ViolentSlash";
        public const string VIOLENT_SLASH_JP = "���@�C�I�����g�E�X���b�V��";
        public const string ONE_AUTHORITY = "OneAuthority";
        public const string ONE_AUTHORITY_JP = "�����E�I�[�\���e�B";

        // [���@���S]
        public const string OUTER_INSPIRATION = "OuterInspiration";
        public const string OUTER_INSPIRATION_JP = "�A�E�^�[�E�C���X�s���[�V����";
        public const string HARDEST_PARRY = "HardestParry";
        public const string HARDEST_PARRY_JP = "�n�[�f�X�g�E�p���B";

        // [�S��@���S]�i���S�t�j
        public const string STANCE_OF_SUDDENNESS = "StanceOfSuddenness";
        public const string STANCE_OF_SUDDENNESS_JP = "�X�^���X�E�I�u�E�T�h���l�X";
        public const string SOUL_EXECUTION = "SoulExecution";
        public const string SOUL_EXECUTION_JP = "�\�E���E�G�O�[�L���[�V����";

        // e ��Ғǉ�
        #endregion

        #region "�u���j�v����"
        public const string ARCHETYPE_COMMAND = "���݉��`�y���j�z";
        public const string ARCHETYPE_COMMAND_EN = "Archetype Command";

        // �p��L�q�͕s�v�B��ɓ������̂�\������B
        public const string ARCHETYPE_EIN = "SYUTYU-DANZETSU";
        public const string ARCHETYPE_RANA = "JUNKAN-SEIYAKU";
        public const string ARCHETYPE_OL = "ORA-ORA-ORAAA!";
        public const string ARCHETYPE_VERZE = "SHINZITSU-HAKAI";
        public const string ARCHETYPE_EIN_JP = "�W���ƒf��";
        public const string ARCHETYPE_RANA_JP = "�z�̐���";
        public const string ARCHETYPE_OL_JP = "�I���I���I���@�I";
        public const string ARCHETYPE_VERZE_JP = "�^���̔j��";
        #endregion

        #region "�A�C�e���E�R�}���h"
        // �����i�R�}���h
        public const string ITEMCOMMAND_FELTUS = "Feltus";
        public const string ITEMCOMMAND_JUZA_PHANTASMAL = "JuzaPhantasmal";
        public const string ITEMCOMMAND_ETERNAL_FATE = "EternalFate";
        public const string ITEMCOMMAND_LIGHT_SERVANT = "LightServant";
        public const string ITEMCOMMAND_SHADOW_SERVANT = "ShadowServant";
        public const string ITEMCOMMAND_MAZE_CUBE = "MazeCube";
        public const string ITEMCOMMAND_ADIL_RING_BLUE_BURN = "AdilBlueBurn";
        public const string ITEMCOMMAND_DETACHMENT_ORB = "DetachmentOrb";
        public const string ITEMCOMMAND_DEVIL_SUMMONER_TOME = "DevilSummonerTome";
        public const string ITEMCOMMAND_VOID_HYMNSONIA = "VoidHymnsonia";
        // ���Օi�R�}���h
        public const string ITEMCOMMAND_GENSEI_TAIMA = "GenseiTaima";
        public const string ITEMCOMMAND_SHINING_AETHER = "ShiningAether";
        public const string ITEMCOMMAND_BLACK_ELIXIR = "BlackElixir";
        public const string ITEMCOMMAND_ELEMENTAL_SEAL = "ElementalSeal";
        public const string ITEMCOMMAND_COLORESS_ANTIDOTE = "ColoressAntidote";
        #endregion

        #region "���@����R�X�g"
        // [��]
        public static int FRESH_HEAL_COST = 20;
        public static int PROTECTION_COST = 30;
        public static int HOLY_SHOCK_COST = 45;
        public static int SAINT_POWER_COST = 55;
        public static int GLORY_COST = 60;
        public static int RESURRECTION_COST = 200;
        public static int CELESTIAL_NOVA_COST = 150;
        // [��]
        public static int DARK_BLAST_COST = 15;
        public static int SHADOW_PACT_COST = 30;
        public static int LIFE_TAP_COST = 45;
        public static int BLACK_CONTRACT_COST = 55;
        public static int DEVOURING_PLAGUE_COST = 40;
        public static int BLOODY_VENGEANCE_COST = 120;
        public static int DAMNATION_COST = 200;
        // [��]
        public static int FIRE_BALL_COST = 15;
        public static int FLAME_AURA_COST = 30;
        public static int HEAT_BOOST_COST = 30;
        public static int FLAME_STRIKE_COST = 45;
        public static int VOLCANIC_WAVE_COST = 75;
        public static int IMMORTAL_RAVE_COST = 100;
        public static int LAVA_ANNIHILATION_COST = 400;
        // [��]
        public static int ICE_NEEDLE_COST = 15;
        public static int ABSORB_WATER_COST = 30;
        public static int CLEANSING_COST = 50;
        public static int FROZEN_LANCE_COST = 45;
        public static int MIRROR_IMAGE_COST = 120;
        public static int PROMISED_KNOWLEDGE_COST = 100;
        public static int ABSOLUTE_ZERO_COST = 250;
        // [��]
        public static int WORD_OF_POWER_COST = 30;
        public static int GALE_WIND_COST = 45;
        public static int WORD_OF_LIFE_COST = 45;
        public static int WORD_OF_FORTUNE_COST = 65;
        public static int AETHER_DRIVE_COST = 120;
        public static int GENESIS_COST = 0;
        public static int ETERNAL_PRESENCE_COST = 350;
        // [��]
        public static int DISPEL_MAGIC_COST = 50;
        public static int RISE_OF_IMAGE_COST = 30;
        public static int DEFLECTION_COST = 120;
        public static int TRANQUILITY_COST = 50;
        public static int ONE_IMMUNITY_COST = 65;
        public static int WHITE_OUT_COST = 95;
        public static int TIME_STOP_COST = 300;
        #endregion

        // s ��Ғǉ�
        #region "�������@����R�X�g"
        // [����]�i���S�t�j
        public static int PSYCHIC_TRANCE_COST = 1600;
        public static int BLIND_JUSTICE_COST = 1600;
        public static int TRANSCENDENT_WISH_COST = 3600;

        // [����]
        public static int FLASH_BLAZE_COST = 250;
        public static int LIGHT_DETONATOR_COST = 650;
        public static int ASCENDANT_METEOR_COST = 0; // SpellCost = ( MaxMana / 2 )

        // [����]
        public static int SKY_SHIELD_COST = 350;
        public static int SACRED_HEAL_COST = 1100;
        public static int EVER_DROPLET_COST = 500;

        // [����]
        public static int HOLY_BREAKER_COST = 210;
        public static int EXALTED_FIELD_COST = 650;
        public static int HYMN_CONTRACT_COST = 1200;

        // [����]
        public static int STAR_LIGHTNING_COST = 400;
        public static int ANGEL_BREATH_COST = 1100;
        public static int ENDLESS_ANTHEM_COST = 1500;

        // [�ŉ�]
        public static int BLACK_FIRE_COST = 350;
        public static int BLAZING_FIELD_COST = 1000;
        public static int DEMONIC_IGNITE_COST = 1600;

        // [�Ő�]
        public static int BLUE_BULLET_COST = 200;
        public static int DEEP_MIRROR_COST = 700;
        public static int DEATH_DENY_COST = 2500;

        // [�ŗ�]
        public static int WORD_OF_MALICE_COST = 250;
        public static int ABYSS_EYE_COST = 1000;
        public static int SIN_FORTUNE_COST = 2700;

        // [�ŋ�]
        public static int DARKEN_FIELD_COST = 500;
        public static int DOOM_BLADE_COST = 1200;
        public static int ECLIPSE_END_COST = 2500;

        // [�ΐ�]�i���S�t�j
        public static int FROZEN_AURA_COST = 1500;
        public static int CHILL_BURN_COST = 3500;
        public static int ZETA_EXPLOSION_COST = 5000;

        // [�Η�]
        public static int ENRAGE_BLAST_COST = 250;
        public static int PIERCING_FLAME_COST = 650;
        public static int SIGIL_OF_HOMURA_COST = 1600;

        // [�΋�]
        public static int IMMOLATE_COST = 300;
        public static int PHANTASMAL_WIND_COST = 500;
        public static int RED_DRAGON_WILL_COST = 2200;

        // [����]
        public static int WORD_OF_ATTITUDE_COST = 200;
        public static int STATIC_BARRIER_COST = 900;
        public static int AUSTERITY_MATRIX_COST = 1800;

        // [����]
        public static int VANISH_WAVE_COST = 500;
        public static int VORTEX_FIELD_COST = 1100;
        public static int BLUE_DRAGON_WILL_COST = 2200;

        // [����]�i���S�t�j
        public static int SEVENTH_MAGIC_COST = 1200;
        public static int PARADOX_IMAGE_COST = 1800;
        public static int WARP_GATE_COST = 3500;
        #endregion
        // e ��Ғǉ�

        #region "�X�L������R�X�g"
        // [��]
        public static int STRAIGHT_SMASH_COST = 15;
        public static int DOUBLE_SLASH_COST = 20;
        public static int CRUSHING_BLOW_COST = 35;
        public static int SOUL_INFINITY_COST = 85;
        // [��]
        public static int COUNTER_ATTACK_COST = 5;
        public static int PURE_PURIFICATION_COST = 20;
        public static int ANTI_STUN_COST = 10;
        public static int STANCE_OF_DEATH_COST = 30;
        // [�_]
        public static int STANCE_OF_FLOW_COST = 5;
        public static int ENIGMA_SENSE_COST = 10;
        public static int SILENT_RUSH_COST = 45;
        public static int OBORO_IMPACT_COST = 90;
        // [��]
        public static int STANCE_OF_STANDING_COST = 5;
        public static int INNER_INSPIRATION_COST = 0;
        public static int KINETIC_SMASH_COST = 55;
        public static int CATASTROPHE_COST = 0; // �S�����Ӗ��łO�Ƃ��Ă���B
        // [�S��]
        public static int TRUTH_VISION_COST = 20;
        public static int HIGH_EMOTIONALITY_COST = 25;
        public static int STANCE_OF_EYES_COST = 30;
        public static int PAINFUL_INSANITY_COST = 80;
        // [���S]
        public static int NEGATE_COST = 5;
        public static int VOID_EXTRACTION_COST = 25;
        public static int CARNAGE_RUSH_COST = 60;
        public static int NOTHING_OF_NOTHINGNESS_COST = 100;

        // s ��Ғǉ�
        #region "�����X�L���R�X�g"
        // [���@��]�i���S�t�j
        public static int NEUTRAL_SMASH_COST = 0; // SkillCost = 0
        public static int STANCE_OF_DOUBLE_COST = 50;

        // [���@�_]
        public static int SWIFT_STEP_COST = 20;
        public static int VIGOR_SENSE_COST = 10;

        // [���@��]
        public static int CIRCLE_SLASH_COST = 30;
        public static int RISING_AURA_COST = 60;

        // [���@�S��]
        public static int RUMBLE_SHOUT_COST = 5;
        public static int ONSLAUGHT_HIT_COST = 15;

        // [���@���S]
        public static int SMOOTHING_MOVE_COST = 5;
        public static int ASCENSION_AURA_COST = 15;

        // [�Á@�_]
        public static int FUTURE_VISION_COST = 10;
        public static int UNKNOWN_SHOCK_COST = 20;

        // [�Á@��]
        public static int REFLEX_SPIRIT_COST = 10;
        public static int FATAL_BLOW_COST = 35;

        // [�Á@�S��]
        public static int SHARP_GLARE_COST = 15;
        public static int CONCUSSIVE_HIT_COST = 15;

        // [�Á@���S]
        public static int TRUST_SILENCE_COST = 40;
        public static int MIND_KILLING_COST = 5;

        // [�_�@��]�i���S�t�j
        public static int SURPRISE_ATTACK_COST = 40;
        public static int IMPULSE_HIT_COST = 20;

        // [�_�@�S��]
        public static int PSYCHIC_WAVE_COST = 10;
        public static int NOURISH_SENSE_COST = 25;

        // [�_�@���S]
        public static int RECOVER_COST = 10;
        public static int STANCE_OF_MYSTIC_COST = 30;

        // [���@�S��]
        public static int VIOLENT_SLASH_COST = 25;
        public static int ONE_AUTHORITY_COST = 10;

        // [���@���S]
        public static int OUTER_INSPIRATION_COST = 10;
        public static int HARDEST_PARRY_COST = 20;

        // [�S��@���S]�i���S�t�j
        public static int STANCE_OF_SUDDENNESS_COST = 40;
        public static int SOUL_EXECUTION_COST = 105;
        #endregion
        // e ��Ғǉ�
        #endregion

        #region "�u���j�v����R�X�g"
        public static int ARCHITECT_EIN_COST = 0;
        public static int ARCHITECT_RANA_COST = 0;
        public static int ARCHITECT_OL_COST = 0;
        public static int ARCHITECT_VERZE_COST = 0;
        #endregion

        #region "���y�f�[�^��"
        public static string BaseMusicFolder = Environment.CurrentDirectory + @"\BGM\";
        public static string BGM01 = "01_town_silently.mp3";
        public static int BGM01LoopBegin = 0;
        public static string BGM02 = "02_dungeon_seeking.mp3";
        public static int BGM02LoopBegin = 106500;
        public static string BGM03 = "03_battle_warning.mp3";
        public static int BGM03LoopBegin = 0;
        public static string BGM04 = "04_The_Flame.mp3";
        public static int BGM04LoopBegin = 0;
        public static string BGM05 = "05_Finally_Bystander.mp3";
        public static int BGM05LoopBegin = 0;
        public static string BGM06 = "06_Refuse.mp3";
        public static int BGM06LoopBegin = 0;
        public static string BGM07 = "07_Systematic_Dominance.mp3";
        public static int BGM07LoopBegin = 0;
        public static string BGM08 = "02_dungeon_seeking.mp3";
        public static int BGM08LoopBegin = 0;
        public static string BGM09 = "09_Sea_Ground_Sky.mp3";
        public static int BGM09LoopBegin = 0;
        public static string BGM10 = "10_WindOfVerze.mp3";
        public static int BGM10LoopBegin = 0;
        public static string BGM11 = "11_DUEL_FACE_AND_FACE.mp3";
        public static int BGM11LoopBegin = 0;
        public static string BGM12 = "12_Opening_Live_and_Space.mp3";
        public static int BGM12LoopBegin = 0;
        public static string BGM13 = "13_Entrance_And_Walls.mp3";
        public static int BGM13LoopBegin = 0;
        public static string BGM14 = "14_I_Will_Go_Dungeon.mp3";
        public static int BGM14LoopBegin = 0;
        public static string BGM15 = "15_The_Ear_Ring_Remember.mp3";
        public static int BGM15LoopBegin = 0;
        public static string BGM16 = "16_Wanna_Understand.mp3";
        public static int BGM16LoopBegin = 0;
        public static string BGM17 = "17_wake_up_and_fight.mp3";
        public static int BGM17LoopBegin = 0;
        public static string BGM18 = "18_MatrixDragon.mp3";
        public static int BGM18LoopBegin = 0;
        public static string BGM19 = "19_Silent_Moving.mp3";
        public static int BGM19LoopBegin = 0;
        public static string BGM20 = "20_TimeEnd_StartReason.mp3";
        public static int BGM20LoopBegin = 0;
        public static string BGM21 = "21_The_Best_Battle.mp3";
        public static int BGM21LoopBegin = 0;
        public static string BGM22 = "22_TimeEnd_FanFare.mp3";
        public static int BGM22LoopBegin = 0;
        public static string BGM23 = "23_Verse1_StartReason.mp3";
        public static int BGM23LoopBegin = 0;
        #endregion

        public static string BaseSoundFolder = Environment.CurrentDirectory + @"\Sound\";
        public static string BaseSaveFolder = Environment.CurrentDirectory + @"\Save\";
        public static string BaseResourceFolder = Environment.CurrentDirectory + @"\Resource\";

        public static string[] FloorFolder = { @"Floor1\", @"Floor2\", @"Floor3\", @"Floor4\", @"Floor5\" };

        public static string GameSettingFileName = Environment.CurrentDirectory + @"\" + "GameSetting.xml";

        #region "�퓬�A�G��BuffUp�ėp"
        public const string BUFF_PHYSICAL_ATTACK_UP = "BuffPhysicalAttackUp.bmp";
        public const string BUFF_PHYSICAL_ATTACK_DOWN = "BuffPhysicalAttackDown.bmp";

        public const string BUFF_PHYSICAL_DEFENSE_UP = "BuffPhysicalDefenseUp.bmp";
        public const string BUFF_PHYSICAL_DEFENSE_DOWN = "BuffPhysicalDefenseDown.bmp";

        public const string BUFF_MAGIC_ATTACK_UP = "BuffMagicAttackUp.bmp";
        public const string BUFF_MAGIC_ATTACK_DOWN = "BuffMagicAttackDown.bmp";

        public const string BUFF_MAGIC_DEFENSE_UP = "BuffMagicDefenseUp.bmp";
        public const string BUFF_MAGIC_DEFENSE_DOWN = "BuffMagicDefenseDown.bmp";

        public const string BUFF_SPEED_UP = "BuffSpeedUp.bmp";
        public const string BUFF_SPEED_DOWN = "BuffSpeedDown.bmp";

        public const string BUFF_REACTION_UP = "BuffReactionUp.bmp";
        public const string BUFF_REACTION_DOWN = "BuffReactionDown.bmp";

        public const string BUFF_POTENTIAL_UP = "BuffPotentialUp.bmp";
        public const string BUFF_POTENTIAL_DOWN = "BuffPotentialDown.bmp";

        public const string BUFF_STRENGTH_UP = "BuffStrengthUp.bmp";
        public const string BUFF_AGILITY_UP = "BuffAgilityUp.bmp";
        public const string BUFF_INTELLIGENCE_UP = "BuffIntelligenceUp.bmp";
        public const string BUFF_STAMINA_UP = "BuffStaminaUp.bmp";
        public const string BUFF_MIND_UP = "BuffMindUp.bmp";

        public const string BUFF_LIGHT_UP = "BuffLightUp";
        public const string BUFF_LIGHT_DOWN = "BuffLightDown";

        public const string BUFF_SHADOW_UP = "BuffShadowUp";
        public const string BUFF_SHADOW_DOWN = "BuffShadowDown";

        public const string BUFF_FIRE_UP = "BuffFireUp";
        public const string BUFF_FIRE_DOWN = "BuffFireDown";

        public const string BUFF_ICE_UP = "BuffIceUp";
        public const string BUFF_ICE_DOWN = "BuffIcedown";

        public const string BUFF_FORCE_UP = "BuffForceUp";
        public const string BUFF_FORCE_DOWN = "BuffForceDown";

        public const string BUFF_WILL_UP = "BuffWillUp";
        public const string BUFF_WILL_DOWN = "BuffWillDown";
        #endregion

        #region "�f�ރt�@�C���W"
        public const string JUNK_FIREDAMAGE = "FireDamage.bmp";
        #endregion

        public static int MAINPLAYER_FIRST_STRENGTH = 5;
        public static int MAINPLAYER_FIRST_AGILITY = 3;
        public static int MAINPLAYER_FIRST_INTELLIGENCE = 2;
        public static int MAINPLAYER_FIRST_STAMINA = 4;
        public static int MAINPLAYER_FIRST_MIND = 3;

        public static int SECONDPLAYER_FIRST_STRENGTH = 2;
        public static int SECONDPLAYER_FIRST_AGILITY = 3;
        public static int SECONDPLAYER_FIRST_INTELLIGENCE = 5;
        public static int SECONDPLAYER_FIRST_STAMINA = 3;
        public static int SECONDPLAYER_FIRST_MIND = 4;
        public static int SECONDPLAYER_END_STRENGTH = 950;
        public static int SECONDPLAYER_END_AGILITY = 1200;
        public static int SECONDPLAYER_END_INTELLIGENCE = 900;
        public static int SECONDPLAYER_END_STAMINA = 700;
        public static int SECONDPLAYER_END_MIND = 325;

        public static int THIRDPLAYER_FIRST_STRENGTH = 4;
        public static int THIRDPLAYER_FIRST_AGILITY = 5;
        public static int THIRDPLAYER_FIRST_INTELLIGENCE = 4;
        public static int THIRDPLAYER_FIRST_STAMINA = 3;
        public static int THIRDPLAYER_FIRST_MIND = 1;

        // s ��Ғǉ�
        public static int OL_LANDIS_FIRST_STRENGTH = 147; // 5;
        public static int OL_LANDIS_FIRST_AGILITY = 88; // 3;
        public static int OL_LANDIS_FIRST_INTELLIGENCE = 118; // 4;
        public static int OL_LANDIS_FIRST_STAMINA = 29; // 1;
        public static int OL_LANDIS_FIRST_MIND = 118; // 4;

        public static int OL_LANDIS_STRENGTH_2 = 2100;
        public static int OL_LANDIS_AGILITY_2 = 800;
        public static int OL_LANDIS_INTELLIGENCE_2 = 5;
        public static int OL_LANDIS_STAMINA_2 = 599;
        public static int OL_LANDIS_MIND_2 = 350;

        public static int VERZE_ARTIE_SECOND_STRENGTH = 10;
        public static int VERZE_ARTIE_SECOND_AGILITY = 300;
        public static int VERZE_ARTIE_SECOND_INTELLIGENCE = 120;
        public static int VERZE_ARTIE_SECOND_STAMINA = 65;
        public static int VERZE_ARTIE_SECOND_MIND = 5;

        public static int VERZE_ARTIE_STRENGTH_3 = 10;
        public static int VERZE_ARTIE_AGILITY_3 = 2600;
        public static int VERZE_ARTIE_INTELLIGENCE_3 = 120;
        public static int VERZE_ARTIE_STAMINA_3 = 1000;
        public static int VERZE_ARTIE_MIND_3 = 124;

        public static int SINIKIA_KAHLHANTZ_FIRST_STRENGTH = 5;
        public static int SINIKIA_KAHLHANTZ_FIRST_AGILITY = 150;
        public static int SINIKIA_KAHLHANTZ_FIRST_INTELLIGENCE = 900; 
        public static int SINIKIA_KAHLHANTZ_FIRST_STAMINA = 500;
        public static int SINIKIA_KAHLHANTZ_FIRST_MIND = 50;

        public static int SINIKIA_KAHLHANTZ_STRENGTH_2 = 5;
        public static int SINIKIA_KAHLHANTZ_AGILITY_2 = 600;
        public static int SINIKIA_KAHLHANTZ_INTELLIGENCE_2 = 2200;
        public static int SINIKIA_KAHLHANTZ_STAMINA_2 = 999;
        public static int SINIKIA_KAHLHANTZ_MIND_2 = 50;
        // e ��Ғǉ�

        public static string TEXT_VIGILANCE_MODE = "�x�����[�h";
        public static string TEXT_FINDENEMY_MODE = "���G���[�h";

        public static string InstallComponentError = "�����f���������Ă���܂��BSlimDX�R���|�[�l���g�A�܂��́ADirectX�R���|�[�l���g�̏������Ɏ��s���܂����B\r\nBGM�ƌ��ʉ����I�t�ɂ��܂��B\r\n��ς��萔�ł����A���g���̂o�b�����m�F���Ă��������B";
        public static string BattleRoutineError = "�퓬���ɃG���[���������܂����B��ς����f���������Ă���܂��B���萔�ł����A�ċN�����Ă�蒼���Ă��������B";

        #region "��җp�ɐV�����쐬�����f�[�^�W"
        public static int MAX_PARTY_MEMBER = 3;

        public static int CHARACTER_MAX_LEVEL1 = 20;
        public static int CHARACTER_MAX_LEVEL2 = 35; // ��҂Q�K�܂�
        public static int CHARACTER_MAX_LEVEL3 = 50; // ��҂R�K�܂�
        public static int CHARACTER_MAX_LEVEL4 = 60; // ��҂S�K�܂�
        public static int CHARACTER_MAX_LEVEL5 = 65; // ��҂T�K�܂�
        public static int CHARACTER_MAX_LEVEL6 = 70; // ��Ґ^�����E�܂�

        public static int SPELL_TYPE_NUM = 6;
        public static int SKILL_TYPE_NUM = 6;
        public static int SPELL_ONETYPE_MAX_NUM = 7;
        public static int SKILL_ONETYPE_MAX_NUM = 4;
        public static int SPELL_MAX_NUM = 42;
        public static int SKILL_MAX_NUM = 24;
        public static int BATTLE_COMMAND_MAX = 9; // 3F����g���A7����9��

        public static int MAX_PARAMETER = 10000; // �p�����^�ő�l�i�Ō�̃{�X�d�l�ɉe��������܂��j
        public static int TRUTH_FIRST_POS = DUNGEON_MOVE_LEN * 13 + 39; // ��҃_���W�����J�n�ʒu
        public static int TRUTH_DUNGEON_COLUMN = 60; // ��҃_���W�����̗�
        public static int TRUTH_DUNGEON_ROW = 40; // ��҃_���W�����̍s��

        public static int BUFFPANEL_BUFF_WIDTH = -25;
        public static int BUFF_NUM = 200; // [�x��] component����200*6=1200, 1000����6000�ŃX���[�_�E�����ۂɂȂ���
        public static int BUFF_SIZE_X = 25;
        public static int BUFF_SIZE_Y = 40;
        public static int INFINITY = 9999;

        public static int TILE_AREA_NUM = 4;
        public static string TREASURE_BOX = "TreasureBox.bmp";
        public static string TREASURE_BOX_OPEN = "TreasureBoxOpen.bmp";
        public static string BOARD = "Board.bmp";
        public static string UPSTAIR = "Upstair.bmp";
        public static string DOWNSTAIR = "Downstair.bmp";
        public static string MIRROR = "Mirror.bmp";
        public static string BLUEORB = "BlueOrb.bmp";
        public static string FOUNTAIN = "Fountain.bmp";


        #region "��ʃR�}���h����"
        public const string ATTACK_JP = "�U��";
        public const string ATTACK_EN = "Attack";
        public const string DEFENSE_JP = "�h��";
        public const string DEFENSE_EN = "DEFENSE";
        public const string STAY_JP = "�ҋ@";
        public const string STAY_EN = "Stay";
        public const string WEAPON_SPECIAL_JP = "����\�́i���C���j";
        public const string WEAPON_SPECIAL_EN = "Weapon Special Main";
        public const string WEAPON_SPECIAL_LEFT_JP = "����\�́i�T�u�j";
        public const string WEAPON_SPECIAL_LEFT_EN = "Weapon Special Sub";
        public const string TAMERU_JP = "���߂�";
        public const string TAMERU_EN = "Power Charge";
        public const string ACCESSORY_SPECIAL_JP = "�A�N�Z�T���y�P�z";
        public const string ACCESSORY_SPECIAL_EN = "Accessory 1";
        public const string ACCESSORY_SPECIAL2_JP = "�A�N�Z�T���y�Q�z";
        public const string ACCESSORY_SPECIAL2_EN = "Accessory 2";

        public const string TOSSIN = "�ːi";
        public const string BUFFUP_STRENGTH = "�p���[�A�b�v�y�́z";
        public const string MAGIC_ATTACK = "���@�U��";

        // �퓬���̃A�j���[�V��������
        public const string MISS_ACTION = "�s���~�X�I";
        public const string MISS_SPELL = "�r���~�X�I";
        public const string MISS_SKILL = "�X�L�����s�I";
        public const string FAIL_COUNTER = "�J�E���^�[�~�X�I";
        public const string FAIL_DEFLECTION = "�������˃~�X�I";
        public const string SUCCESS_COUNTER = "�J�E���^�[�����I";

        // ��
        public const string EFFECT_PRESTUNNING = "���|";
        public const string EFFECT_STUN = "�X�^��";
        public const string EFFECT_SILENCE = "����";
        public const string EFFECT_POISON = "�ғ�";
        public const string EFFECT_TEMPTATION = "�U�f";
        public const string EFFECT_FROZEN = "����";
        public const string EFFECT_PARALYZE = "���";
        public const string EFFECT_SLOW = "�݉�";
        public const string EFFECT_BLIND = "�È�";
        public const string EFFECT_SLIP = "�X���b�v";
        public const string EFFECT_NORESURRECTION = "�����s��";
        public const string EFFECT_NOGAIN_LIFE = "���C�t�񕜕s��";

        public const string EFFECT_CANNOT_BUFF = "���E";
        
        // ��
        public const string EFFECT_BLINDED = "�ޔ����";

        public const string RESIST_STUN = "�X�^���ϐ��I";
        public const string RESIST_SILENCE = "���ّϐ��I";
        public const string RESIST_POISON = "�ғőϐ��I";
        public const string RESIST_TEMPTATION = "�U�f�ϐ��I";
        public const string RESIST_FROZEN = "�����ϐ��I";
        public const string RESIST_PARALYZE = "��ბϐ��I";
        public const string RESIST_SLOW = "�݉��ϐ��I";
        public const string RESIST_BLIND = "�Èőϐ��I";
        public const string RESIST_SLIP = "�X���b�v�ϐ��I";
        public const string RESIST_NORESURRECTION = "�����s�ϐ��I";

        public const string RESIST_LIFE_DOWN = "���C�t�_�E���ϐ��I";
 
        public const string RESIST_LIGHT_UP = "���ϐ��t�^";
        public const string RESIST_SHADOW_UP = "�őϐ��t�^";
        public const string RESIST_FIRE_UP = "�Αϐ��t�^";
        public const string RESIST_ICE_UP = "���ϐ��t�^";
        public const string RESIST_FORCE_UP = "���ϐ��t�^";
        public const string RESIST_WILL_UP = "��ϐ��t�^";

        public const string BUFF_DANZAI_KAGO = "�f�߂̉���";
        public const string BUFF_FIREDAMAGE2 = "���_���[�W";
        public const string BUFF_BLACK_MAGIC = "���@2��r��";
        public const string BUFF_CHAOS_DESPERATE = "���S�\��";

        public const string BUFF_FELTUS = "�y�_�z�̒~��";
        public const string BUFF_JUZA_PHANTASMAL = "�y�D�z�̒~��";
        public const string BUFF_ETERNAL_FATE_RING = "�y���z�̒~��";
        public const string BUFF_LIGHT_SERVANT = "�y���z�̒~��";
        public const string BUFF_SHADOW_SERVANT = "�y�Łz�̒~��";
        public const string BUFF_ADIL_BLUE_BURN = "�y���z�̒~��";
        public const string BUFF_MAZE_CUBE = "�y���z�̒~��";
        public const string BUFF_SHADOW_BIBLE = "�y���S���ɑh���z";
        public const string BUFF_DETACHMENT_ORB = "�y�S�_���[�W�����z";
        public const string BUFF_DEVIL_SUMMONER_TOME = "�y�����������z";
        public const string BUFF_VOID_HYMNSONIA = "�y�􂢁z�S -1000";

        public const string BUFF_SAGE_POTION_MINI = "�S�ϐ��t�^";
        public const string BUFF_GENSEI_TAIMA = "�y�����z����^���S�����C�t������";
        public const string BUFF_SHINING_AETHER  = "���j�X�L�������^�S�_���[�W����";
        public const string BUFF_BLACK_ELIXIR = "�ő僉�C�t�����^���C�t�_�E���ϐ�";
        public const string BUFF_ELEMENTAL_SEAL = "���̉e���ϐ�";
        public const string BUFF_COLORESS_ANTIDOTE = "�f�o�t�ϐ�";

        public const string BUFF_ICHINARU_HOMURA = "���^�[���A���_���[�W";
        public const string BUFF_ABYSS_FIRE = "�����U�������@�U�����s���x�ɁA�A�r�X�_���[�W";
        public const string BUFF_LIGHT_AND_SHADOW = "�����^���@�_���[�W�O";
        public const string BUFF_ETERNAL_DROPLET = "���C�t�^�}�i��";
        public const string BUFF_AUSTERITY_MATRIX_OMEGA = "�v���XBUFF����";
        public const string BUFF_VOICE_OF_ABYSS = "���C�t�񕜂O";
        public const string BUFF_ABYSS_WILL = "���^�A�r�X�_���[�W�㏸";
        public const string BUFF_THE_ABYSS_WALL = "�_���[�W�z���o���A";

        public const string BUFF_TIME_SEQUENCE_1 = "���ԗ��y���Ɓz";
        public const string BUFF_TIME_SEQUENCE_2 = "���ԗ��y���z";
        public const string BUFF_TIME_SEQUENCE_3 = "���ԗ��y���h�z";
        public const string BUFF_TIME_SEQUENCE_4 = "���ԗ��y�⌕�z";
        public const string BUFF_TIME_SEQUENCE_5 = "���ԗ��y�Ήi�z";
        public const string BUFF_TIME_SEQUENCE_6 = "���S��Ύ��ԗ��y�I���z";

        public const string PHYSICAL_ATTACK_UP = "�����U��UP";
        public const string PHYSICAL_ATTACK_DOWN = "�����U��DOWN";
        public const string PHYSICAL_DEFENSE_UP = "�����h��UP";
        public const string PHYSICAL_DEFENSE_DOWN = "�����h��DOWN";
        public const string MAGIC_ATTACK_UP = "���@�U��UP";
        public const string MAGIC_ATTACK_DOWN = "���@�U��DOWN";
        public const string MAGIC_DEFENSE_UP = "���@�ϐ�UP";
        public const string MAGIC_DEFENSE_DOWN = "���@�ϐ�DOWN";
        public const string BATTLE_SPEED_UP = "�퓬���xUP";
        public const string BATTLE_SPEED_DOWN = "�퓬���xDOWN";
        public const string BATTLE_REACTION_UP = "�퓬����UP";
        public const string BATTLE_REACTION_DOWN = "�퓬����DOWN";
        public const string POTENTIAL_UP = "���ݔ\��UP";
        public const string POTENTIAL_DOWN = "���ݔ\��DOWN";

        public const string EFFECT_STRENGTH_UP = "�y�́zUP";
        public const string EFFECT_AGILITY_UP = "�y�Z�zUP";
        public const string EFFECT_INTELLIGENCE_UP = "�y�m�zUP";
        public const string EFFECT_STAMINA_UP = "�y�́zUP";
        public const string EFFECT_MIND_UP = "�y�S�zUP";
             
        public const string IMMUNE_DAMAGE = "�_���[�W�����I";

        public const string BROKEN_ITEM = "�A�C�e���j��";
        #endregion

        #region "�_���W�����^�C���f�[�^��"
        public const string TILEINFO_1 = "Downstair.bmp";
        public const string TILEINFO_2 = "Downstair-WallLRB.bmp";
        public const string TILEINFO_3 = "Downstair-WallT.bmp";
        public const string TILEINFO_4 = "Downstair-WallTLB.bmp";
        public const string TILEINFO_5 = "Downstair-WallTRB.bmp";
        public const string TILEINFO_6 = "Upstair-WallLRB.bmp";
        public const string TILEINFO_7 = "Upstair-WallRB.bmp";
        public const string TILEINFO_8 = "Upstair-WallTLR.bmp";
        public const string TILEINFO_9 = "TreasureBox.bmp";
        public const string TILEINFO_10 = "UnknownTile.bmp";
        public const string TILEINFO_10_2 = "UnknownTile_Check.bmp";
        public const string TILEINFO_11 = "Upstair.bmp";
        public const string TILEINFO_12 = "Board.bmp";

        public const string TILEINFO_13 = "Tile1.bmp";
        public const string TILEINFO_14 = "Tile1-WallB.bmp";
        public const string TILEINFO_15 = "Tile1-WallB-DummyB.bmp";
        public const string TILEINFO_16 = "Tile1-WallL.bmp";
        public const string TILEINFO_17 = "Tile1-WallLB.bmp";
        public const string TILEINFO_18 = "Tile1-WallLR.bmp";
        public const string TILEINFO_19 = "Tile1-WallLRB.bmp";
        public const string TILEINFO_20 = "Tile1-WallLR-DummyL.bmp";
        public const string TILEINFO_21 = "Tile1-WallR.bmp";
        public const string TILEINFO_22 = "Tile1-WallRB.bmp";
        public const string TILEINFO_23 = "Tile1-WallR-DummyR.bmp";
        public const string TILEINFO_24 = "Tile1-WallT.bmp";
        public const string TILEINFO_25 = "Tile1-WallTB.bmp";
        public const string TILEINFO_26 = "Tile1-WallTL.bmp";
        public const string TILEINFO_27 = "Tile1-WallTLB.bmp";
        public const string TILEINFO_28 = "Tile1-WallTLR.bmp";
        public const string TILEINFO_29 = "Tile1-WallTLRB.bmp";
        public const string TILEINFO_30 = "Tile1-WallTR.bmp";
        public const string TILEINFO_31 = "Tile1-WallTRB.bmp";

        public const string TILEINFO_32 = "Tile1-WallT-NumT1.bmp";
        public const string TILEINFO_33 = "Tile1-WallL-NumT2.bmp";
        public const string TILEINFO_34 = "Tile1-NumT3.bmp";
        public const string TILEINFO_35 = "Tile1-WallLB-NumT4.bmp";
        public const string TILEINFO_36 = "Tile1-Wall-NumT5.bmp";
        public const string TILEINFO_37 = "Tile1-WallLB-NumT6.bmp";
        public const string TILEINFO_38 = "Tile1-WallT-NumT7.bmp";
        public const string TILEINFO_39 = "Tile1-WallR-NumT8.bmp";

        public const string TILEINFO_40 = "Tile1-Blue.bmp";
        public const string TILEINFO_41 = "Tile1-Black.bmp";

        public const string TILEINFO_42 = "Tile1-WallRB-DummyB.bmp";

        public const string TILEINFO_43 = "Mirror.bmp";

        public const string TILEINFO_44 = "BlueOrb.bmp";
        #endregion

        #region "�A�C�e������"
        #region "����"
        // �A�C�e�����̂��e�\�[�X�R�[�h�ɋL�q����ƌ�肪�������邽�߁A������ŋL�ڂ��܂��B
        public const string RARE_TOOMI_BLUE_SUISYOU = "�����̐���";
        public const string RARE_EARRING_OF_LANA = "���i�̃C�������O";
        public const string POOR_PRACTICE_SWORD = "���K�p�̌�";
        public const string POOR_PRACTICE_SHILED = "���K�p�̏�";
        public const string POOR_COTE_OF_PLATE = "�R�[�g�E�I�u�E�v���[�g";
        #endregion

        #region "�P�K"
        public const string POOR_HINJAKU_ARMRING = "�n��Șr��";
        public const string POOR_USUYOGORETA_FEATHER = "�����ꂽ�t���H";
        public const string POOR_NON_BRIGHT_ORB = "�P���̖����I�[�u";
        public const string POOR_KUKEI_BANGLE = "��`�̃o���O��";
        public const string POOR_SUTERARESHI_EMBLEM = "�̂Ă�ꂵ���";
        public const string POOR_ARIFURETA_STATUE = "����ӂꂽ����";
        public const string POOR_NON_ADJUST_BELT = "�����ł��Ȃ��x���g";
        public const string POOR_SIMPLE_EARRING = "�V���v���ȃC�������O";
        public const string POOR_KATAKUZURESHITA_FINGERRING = "�^���ꂵ���w��";
        public const string POOR_IROASETA_CHOKER = "�F�򂹂��`���[�J�[";
        public const string POOR_YOREYORE_MANTLE = "�����̃}���g";
        public const string POOR_NON_HINSEI_CROWN = "�i���̂Ȃ�����";
        public const string POOR_TUKAIFURUSARETA_SWORD = "�g���Â��ꂽ��";
        public const string POOR_TUKAINIKUI_LONGSWORD = "�g���ɂ�������";
        public const string POOR_GATAGAKITERU_ARMOR = "�K�^�����Ă�Z";
        public const string POOR_FESTERING_ARMOR = "�t�F�X�^�����O�E�A�[�}�[";
        public const string POOR_HINSO_SHIELD = "�n�e�ȏ�";
        public const string POOR_MUDANIOOKII_SHIELD = "���ʂɑ傫����";

        public const string COMMON_RED_PENDANT = "���b�h�E�y���_���g";
        public const string COMMON_BLUE_PENDANT = "�u���[�E�y���_���g";
        public const string COMMON_PURPLE_PENDANT = "�p�[�v���E�y���_���g";
        public const string COMMON_GREEN_PENDANT = "�O���[���E�y���_���g";
        public const string COMMON_YELLOW_PENDANT = "�C�G���[�E�y���_���g";
        public const string COMMON_SISSO_ARMRING = "���f�Șr��";
        public const string COMMON_FINE_FEATHER = "�t�@�C���E�t�F�U�[";
        public const string COMMON_KIREINA_ORB = "�Y��ȃI�[�u";
        public const string COMMON_FIT_BANGLE = "�t�B�b�g�E�o���O��";
        public const string COMMON_PRISM_EMBLEM = "�v���Y���E�G���u����";
        public const string COMMON_FINE_SWORD = "�t�@�C���E�\�[�h";
        public const string COMMON_TWEI_SWORD = "�c���@�C�E�\�[�h";
        public const string COMMON_FINE_ARMOR = "�t�@�C���E�A�[�}�[";
        public const string COMMON_GOTHIC_PLATE = "�S�V�b�N�E�v���[�g";
        public const string COMMON_FINE_SHIELD = "�t�@�C���E�V�[���h";
        public const string COMMON_GRIPPING_SHIELD = "�O���b�s���O�E�V�[���h";

        public const string RARE_JOUSITU_BLUE_POWERRING = "�㎿�̐��p���[�����O";
        public const string RARE_KOUJOUSINYADORU_RED_ORB = "����S�̏h��Ԃ��I�[�u";
        public const string RARE_MAGICIANS_MANTLE = "�}�W�V�����Y�E�}���g";
        public const string RARE_BEATRUSH_BANGLE = "�r�[�g���b�V���E�o���O��";
        public const string RARE_AERO_BLADE = "�G�A���E�u���[�h";

        public const string POOR_OLD_USELESS_ROD = "�Âڂ�����";
        public const string POOR_KISSAKI_MARUI_TUME = "�ؐ悪�ۂ���";
        public const string POOR_BATTLE_HUMUKI_BUTOUGI = "�퓬�ɕs�����ȕ�����";
        public const string POOR_SIZE_AWANAI_ROBE = "�T�C�Y������Ȃ����[�u";
        public const string POOR_NO_CONCEPT_RING = "�����������Ă���r��";
        public const string POOR_HIGHCOLOR_MANTLE = "�n�C�J���ȃ}���g";
        public const string POOR_EIGHT_PENDANT = "���p�y���_���g";
        public const string POOR_GOJASU_BELT = "�S�[�W���X�x���g";
        public const string POOR_EGARA_HUMEI_EMBLEM = "�G���s���̃y���_���g";
        public const string POOR_HAYATOTIRI_ORB = "�͂�Ƃ���ȃI�[�u";

        public const string COMMON_COPPER_RING_TORA = "���̘r�ցw�Ձx";
        public const string COMMON_COPPER_RING_IRUKA = "���̘r�ցw�C���J�x";
        public const string COMMON_COPPER_RING_UMA = "���̘r�ցw�n�x";
        public const string COMMON_COPPER_RING_KUMA = "���̘r�ցw�F�x";
        public const string COMMON_COPPER_RING_HAYABUSA = "���̘r�ցw���x";
        public const string COMMON_COPPER_RING_TAKO = "���̘r�ցw�^�R�x";
        public const string COMMON_COPPER_RING_USAGI = "���̘r�ցw�e�x";
        public const string COMMON_COPPER_RING_KUMO = "���̘r�ցw�w偁x";
        public const string COMMON_COPPER_RING_SHIKA = "���̘r�ցw���x";
        public const string COMMON_COPPER_RING_ZOU = "���̘r�ցw�ہx";
        public const string COMMON_RED_AMULET = "�A�~�����b�g�w�ԁx";
        public const string COMMON_BLUE_AMULET = "�A�~�����b�g�w�x";
        public const string COMMON_PURPLE_AMULET = "�A�~�����b�g�w���x";
        public const string COMMON_GREEN_AMULET = "�A�~�����b�g�w�΁x";
        public const string COMMON_YELLOW_AMULET = "�A�~�����b�g�w���x";
        public const string COMMON_SHARP_CLAW = "�V���[�v�E�N���[";
        public const string COMMON_LIGHT_CLAW = "���C�g�E�N���[";
        public const string COMMON_WOOD_ROD = "�E�b�h�E���b�h";
        public const string COMMON_SHORT_SWORD = "�V���[�g�E�\�[�h";
        public const string COMMON_BASTARD_SWORD = "�o�X�^�[�h�E�\�[�h";
        public const string COMMON_LETHER_CLOTHING = "���U�[�E�N���X";
        public const string COMMON_COTTON_ROBE = "�R�b�g���E���[�u";
        public const string COMMON_COPPER_ARMOR = "���̊Z";
        public const string COMMON_HEAVY_ARMOR = "�w���B�E�A�[�}�[";
        public const string COMMON_IRON_SHIELD = "�A�C�A���E�V�[���h";

        public const string RARE_SINTYUU_RING_KUROHEBI = "�^�J�̘r�ցw���ցx";
        public const string RARE_SINTYUU_RING_HAKUTYOU = "�^�J�̘r�ցw�����x";
        public const string RARE_SINTYUU_RING_AKAHYOU = "�^�J�̘r�ցw�ԕ^�x";
        public const string RARE_ICE_SWORD = "�A�C�V�N���E�\�[�h";
        public const string RARE_RISING_KNUCKLE = "���C�W���O�E�i�b�N��";
        public const string RARE_AUTUMN_ROD = "�I�[�^�����E���b�h";
        public const string RARE_SUN_BRAVE_ARMOR = "�T���E�u���C�u�A�[�}�[";
        public const string RARE_ESMERALDA_SHIELD = "�V�[���h�E�I�u�E�G�X�������_";
        #endregion

        #region "�_���W�����P�K�̕�"
        public const string POOR_HARD_SHOES = "�d���V���[�Y";
        public const string COMMON_SIMPLE_BRACELET = "�ȑf�ȃu���X���b�g";
        public const string COMMON_SEAL_OF_POSION = "�V�[���E�I�u�E�|�C�Y��";
        public const string COMMON_GREEN_EGG_KAIGARA = "�Η��̊L�k";
        public const string COMMON_CHARM_OF_FIRE_ANGEL = "�����V�g�̌아";
        public const string COMMON_DREAM_POWDER = "�h���[���E�p�E�_�[";
        public const string COMMON_VIKING_SWORD = "�o�C�L���O�E�\�[�h";
        public const string COMMON_NEBARIITO_KUMO = "�y�w偂̔S�莅";
        public const string COMMON_SUN_PRISM = "���z�̃v���Y��";
        public const string COMMON_POISON_EKISU = "�|�C�Y���E�G�L�X";
        public const string COMMON_SOLID_CLAW = "�\���b�h�E�N���[";
        public const string COMMON_GREEN_LEEF_CHARM = "�Ηt�̖�����";
        public const string COMMON_WARRIOR_MEDAL = "��m�̍���";
        public const string COMMON_PALADIN_MEDAL = "�p���f�B���̍���";
        public const string COMMON_KASHI_ROD = "�~�̏�";
        public const string COMMON_HAYATE_ORB = "�����̕��";
        public const string COMMON_BLUE_COPPER = "����";
        public const string COMMON_ORANGE_MATERIAL = "�I�����W�E�}�e���A��";
        public const string RARE_TOTAL_HIYAKU_KASSEI = "�������w�����x";
        public const string RARE_ZEPHER_FETHER = "�[�t�B�[���E�t�F�U�[";
        public const string RARE_LIFE_SWORD = "�\�[�h�E�I�u�E���C�t";
        public const string RARE_PURE_WATER = "������";
        public const string RARE_PURE_GREEN_SILK_ROBE = "���΂̃V���N���[�u";
        #endregion

        #region "�Q�K�̃����_���h���b�v"
        public const string POOR_HUANTEI_RING = "�s����ȃ����O";
        public const string POOR_DEPRESS_FEATHER = "�f�v���X�E�t�F�U�[";
        public const string POOR_DAMAGED_ORB = "���A���̃I�[�u";
        public const string POOR_SHIMETSUKE_BELT = "���ߕt���x���g";
        public const string POOR_NOGENKEI_EMBLEM = "���^�̖������";
        public const string POOR_MAGICLIGHT_FIRE = "�}�W�b�N���C�g�w�΁x";
        public const string POOR_MAGICLIGHT_ICE = "�}�W�b�N���C�g�w���x";
        public const string POOR_MAGICLIGHT_SHADOW = "�}�W�b�N���C�g�w�Łx";
        public const string POOR_MAGICLIGHT_LIGHT = "�}�W�b�N���C�g�w���x";
        // �����Poor�͕s�v�B
        public const string COMMON_RED_CHARM = "���b�h�E�`���[��";
        public const string COMMON_BLUE_CHARM = "�u���[�E�`���[��";
        public const string COMMON_PURPLE_CHARM = "�p�[�v���E�`���[��";
        public const string COMMON_GREEN_CHARM = "�O���[���E�`���[��";
        public const string COMMON_YELLOW_CHARM = "�C�G���[�E�`���[��";
        public const string COMMON_THREE_COLOR_COMPASS = "�R�F�R���p�X";
        public const string COMMON_SANGO_CROWN = "�X��̊�";
        public const string COMMON_SMOOTHER_BOOTS = "�X���[�U�[�E�u�[�c";
        public const string COMMON_SHIOKAZE_MANTLE = "�����̊O��";
        public const string COMMON_SMART_SWORD = "�X�}�[�g�E�\�[�h";
        public const string COMMON_SMART_CLAW = "�X�}�[�g�E�N���[";
        public const string COMMON_SMART_ROD = "�X�}�[�g�E���b�h";
        public const string COMMON_SMART_SHIELD = "�X�}�[�g�E�V�[���h";
        public const string COMMON_RAUGE_SWORD = "���E�W�F�E�\�[�h";
        public const string COMMON_SMART_CLOTHING = "�X�}�[�g�E�N���X";
        public const string COMMON_SMART_ROBE = "�X�}�[�g�E���[�u";
        public const string COMMON_SMART_PLATE = "�X�}�[�g�E�v���[�g";

        public const string RARE_WRATH_SERVEL_CLAW = "���X�E�T�[���F���E�N���[";
        public const string RARE_BLUE_LIGHTNING = "�u���[�E���C�g�j���O";
        public const string RARE_DIRGE_ROBE = "�_�[�W�F�E���[�u";
        public const string RARE_DUNSID_PLATE = "�_���V�b�h�E�v���[�g";
        public const string RARE_BURNING_CLAYMORE = "�o�[�j���O�E�N���C���A";

        public const string POOR_CURSE_EARRING = "�J�[�X�E�C�������O";
        public const string POOR_CURSE_BOOTS = "���ꂽ�u�[�c";
        public const string POOR_BLOODY_STATUE = "�����߂̒���";
        public const string POOR_FALLEN_MANTLE = "������}���g";
        public const string POOR_SIHAIRYU_SIKOTU = "�x�z���̎w��";
        public const string POOR_OLD_TREE_KAREHA = "�Ñ�h���̌͂�t";
        public const string POOR_GALEWIND_KONSEKI = "�Q�C���E�E�B���h�̍���";
        public const string POOR_SIN_CRYSTAL_KAKERA = "�V���E�N���X�^���̌���";
        public const string POOR_EVERMIND_ZANSHI = "�G�o�[�E�}�C���h�̎c�v";
        // �����Poor�͕s�v�B

        public const string COMMON_BRONZE_RING_KIBA = "���̘r�ցw��x";
        public const string COMMON_BRONZE_RING_SASU = "���̘r�ցw�h�x";
        public const string COMMON_BRONZE_RING_KU = "���̘r�ցw��x";
        public const string COMMON_BRONZE_RING_NAGURI = "���̘r�ցw���x";
        public const string COMMON_BRONZE_RING_TOBI = "���̘r�ցw��x";
        public const string COMMON_BRONZE_RING_KARAMU = "���̘r�ցw���x";
        public const string COMMON_BRONZE_RING_HANERU = "���̘r�ցw���x";
        public const string COMMON_BRONZE_RING_TORU = "���̘r�ցw��x";
        public const string COMMON_BRONZE_RING_MIRU = "���̘r�ցw���x";
        public const string COMMON_BRONZE_RING_KATAI = "���̘r�ցw���x";
        public const string COMMON_RED_KOKUIN = "�Ԃ̍���";
        public const string COMMON_BLUE_KOKUIN = "�̍���";
        public const string COMMON_PURPLE_KOKUIN = "���̍���";
        public const string COMMON_GREEN_KOKUIN = "�΂̍���";
        public const string COMMON_YELLOW_KOKUIN = "���̍���";
        public const string COMMON_SISSEI_MANTLE = "�����̃}���g";
        public const string COMMON_KAISEI_EMBLEM = "�����̖��";
        public const string COMMON_SAZANAMI_EARRING = "�����Ȃ݃C�������O";
        public const string COMMON_AMEODORI_STATUE = "�J�x��̒���";
        public const string COMMON_SMASH_BLADE = "�X�}�b�V���E�u���[�h";
        public const string COMMON_POWERED_BUSTER = "�p���[�h�E�o�X�^�[";
        public const string COMMON_STONE_CLAW = "�X�g�[���E�N���[";
        public const string COMMON_DENDOU_ROD = "�d�����b�h";
        public const string COMMON_SERPENT_ARMOR = "�T�[�y���g�E�A�[�}�[";
        public const string COMMON_SWIFT_CROSS = "�X�E�B�t�g�E�N���X";
        public const string COMMON_CHIFFON_ROBE = "�V�t�H���E���[�u";
        public const string COMMON_PURE_BRONZE_SHIELD = "�����̏�";

        public const string RARE_RING_BRONZE_RING_KONSHIN = "�Ӑ��̘r�ցw�Ӑg�x";
        public const string RARE_RING_BRONZE_RING_SYUNSOKU = "�Ӑ��̘r�ցw�r���x";
        public const string RARE_RING_BRONZE_RING_JUKURYO = "�Ӑ��̘r�ցw�n���x";
        public const string RARE_RING_BRONZE_RING_SOUGEN = "�Ӑ��̘r�ցw�u���x";
        public const string RARE_RING_BRONZE_RING_YUUDAI = "�Ӑ��̘r�ցw�Y��x";
        public const string RARE_MEIUN_BOX = "���^�̃v���Y���{�b�N�X";
        public const string RARE_WILL_HOLY_HAT = "�E�B���E�z�[���[�Y�E�n�b�g";
        public const string RARE_EMBLEM_BLUESTAR = "�G���u�����E�I�u�E�u���[�X�^�[";
        public const string RARE_SEAL_OF_DEATH = "�V�[���E�I�u�E�f�X";
        public const string RARE_DARKNESS_SWORD = "�\�[�h�E�I�u�E�_�[�N�l�X";
        public const string RARE_BLUE_RED_ROD = "�ԑ����̏�";
        public const string RARE_SHARKSKIN_ARMOR = "�V���[�N�X�L���E�A�[�}�[";
        public const string RARE_RED_THUNDER_ROBE = "���[�u�E�I�u�E���b�h�T���_�[";
        public const string RARE_BLACK_MAGICIAN_CROSS = "�����p�t�̕�����";
        public const string RARE_BLUE_SKY_SHIELD = "�u���[�X�J�C�E�V�[���h";
        #endregion

        #region "�_���W�����Q�K�̕�"
        public const string COMMON_PUZZLE_BOX = "�p�Y���E�{�b�N�X";
        public const string COMMON_CHIENOWA_RING = "�m�b�̗փ����O";
        public const string RARE_MASTER_PIECE = "�}�X�^�[�E�s�[�X";
        public const string COMMON_TUMUJIKAZE_BOX = "�ނ����̔�";
        public const string COMMON_ROCKET_DASH = "���P�b�g�E�_�b�V��";
        public const string COMMON_CLAW_OF_SPRING = "�t���̒�";
        public const string COMMON_SOUKAI_DRINK_SS = "�u���h�����N�y�r���r�z";
        public const string COMMON_BREEZE_CROSS = "���敗�̕�����";
        public const string COMMON_GUST_SWORD = "�\�[�h�E�I�u�E�K�X�g";
        //public const string RARE_PURE_GREEN_WATER = "���N��"; // Duel�A�W�F�_�E�A���X�̎������͂����œ���\�Ƃ���B
        public const string COMMON_BLANK_BOX = "�󔒂̔�";
        public const string RARE_SPIRIT_OF_HEART = "�S�̐��t�y�n�[�g�z";
        public const string COMMON_FUSION_BOX = "�t���[�W�����E�{�b�N�X";
        public const string COMMON_WAR_DRUM = "�E�H�[�h����";
        public const string COMMON_KOBUSHI_OBJE = "���^�̃I�u�W�F";
        public const string COMMON_TIGER_BLADE = "�^�C�K�[�E�u���C�h";
        public const string COMMON_TUUKAI_DRINK_DD = "�u���h�����N�y�c���c�z";
        public const string RARE_ROD_OF_STRENGTH = "�͂̏�";
        public const string RARE_SOUJUTENSHI_NO_GOFU = "�����V�g�̌아";
        #endregion

        #region "�R�K�̃����_���h���b�v"
        // �����Poor�͕s�v�B
        public const string POOR_DIRTY_ANGEL_CONTRACT = "�{���{���ɂȂ����V�g�̌_��";
        public const string POOR_JUNK_TARISMAN_FROZEN = "�W�����N�E�^���X�}���y�����z";
        public const string POOR_JUNK_TARISMAN_PARALYZE = "�W�����N�E�^���X�}���y��Ⴡz";
        public const string POOR_JUNK_TARISMAN_STUN = "�W�����N�E�^���X�}���y�X�^���z";
        public const string COMMON_RED_STONE = "���b�h�E�X�g�[��";
        public const string COMMON_BLUE_STONE = "�u���[�E�X�g�[��";
        public const string COMMON_PURPLE_STONE = "�p�[�v���E�X�g�[��";
        public const string COMMON_GREEN_STONE = "�O���[���E�X�g�[��";
        public const string COMMON_YELLOW_STONE = "�C�G���[�E�X�g�[��";
        public const string COMMON_EXCELLENT_SWORD = "�G�N�Z�����g�E�\�[�h";
        public const string COMMON_EXCELLENT_KNUCKLE = "�G�N�Z�����g�E�i�b�N��";
        public const string COMMON_EXCELLENT_ROD = "�G�N�Z�����g�E���b�h";
        public const string COMMON_EXCELLENT_BUSTER = "�G�N�Z�����g�E�o�X�^�[";
        public const string COMMON_EXCELLENT_ARMOR = "�G�N�Z�����g�E�A�[�}�[";
        public const string COMMON_EXCELLENT_CROSS = "�G�N�Z�����g�E�N���X";
        public const string COMMON_EXCELLENT_ROBE = "�G�N�Z�����g�E���[�u";
        public const string COMMON_EXCELLENT_SHIELD = "�G�N�Z�����g�E�V�[���h";
        public const string RARE_MENTALIZED_FORCE_CLAW = "�����^���C�Y�h�E�t�H�[�X�E�N���[";
        public const string RARE_ADERKER_FALSE_ROD = "�A�_�[�J�[�E�t�H���X�E���b�h";
        public const string RARE_BLACK_ICE_SWORD = "���X��";
        public const string RARE_CLAYMORE_ZUKS = "�N���C���A�E�I�u�E�U�b�N�X";
        public const string RARE_DRAGONSCALE_ARMOR = "�h���S���X�P�C���E�A�[�}�[";
        public const string RARE_LIGHT_BLIZZARD_ROBE = "���C�g�u���U�[�h�E���[�u";

        public const string POOR_MIGAWARI_DOOL = "�g����l�`";
        public const string POOR_ONE_DROPLET_KESSYOU = "��H�̎�����";
        public const string POOR_MOMENTARY_FLASH_LIGHT = "���[�����^���E�t���b�V��";
        public const string POOR_SUN_YUME_KAKERA = "���̖��̌���";
        public const string COMMON_STEEL_RING_1 = "�|�̘r�ցw�p���[�x";
        public const string COMMON_STEEL_RING_2 = "�|�̘r�ցw�Z���X�x";
        public const string COMMON_STEEL_RING_3 = "�|�̘r�ցw�^�t�x";
        public const string COMMON_STEEL_RING_4 = "�|�̘r�ցw���b�N�x";
        public const string COMMON_STEEL_RING_5 = "�|�̘r�ցw�t�@�X�g�x";
        public const string COMMON_STEEL_RING_6 = "�|�̘r�ցw�V���[�v�x";
        public const string COMMON_STEEL_RING_7 = "�|�̘r�ցw�n�C�x";
        public const string COMMON_STEEL_RING_8 = "�|�̘r�ցw�f�B�[�v�x";
        public const string COMMON_STEEL_RING_9 = "�|�̘r�ցw�o�E���h�x";
        public const string COMMON_STEEL_RING_10 = "�|�̘r�ցw�G���[�g�x";
        public const string COMMON_RED_MASEKI = "�Ԃ̖���";
        public const string COMMON_BLUE_MASEKI = "�̖���";
        public const string COMMON_PURPLE_MASEKI = "���̖���";
        public const string COMMON_GREEN_MASEKI = "�΂̖���";
        public const string COMMON_YELLOW_MASEKI = "���̖���";
        public const string COMMON_DESCENED_BLADE = "�f�B�b�Z���h�E�u���[�h";
        public const string COMMON_FALSET_CLAW = "�t�@���Z�b�g�̒�";
        public const string COMMON_SEKIGAN_ROD = "�Ǌ�̏�";
        public const string COMMON_ROCK_BUSTER = "���b�N�E�o�X�^�[";
        public const string COMMON_COLD_STEEL_PLATE = "�R�[���h�E�X�`�[���E�v���[�g";
        public const string COMMON_AIR_HARE_CROSS = "�󐰂̕�����";
        public const string COMMON_FLOATING_ROBE = "�t���[�e�B���O�E���[�u";
        public const string COMMON_SNOW_CRYSTAL_SHIELD = "�ጋ���̏�";
        public const string COMMON_WING_STEP_FEATHER = "�E�B���O�X�e�b�v�E�t�F�U�[";
        public const string COMMON_SNOW_FAIRY_BREATH = "�X�m�[�t�F�A���[�̑���";
        public const string COMMON_STASIS_RING = "�X�e�C�V�X�E�����O";
        public const string COMMON_SIHAIRYU_KIBA = "�x�z���̉�";
        public const string COMMON_OLD_TREE_JUSHI = "�Ñ�h���̎���";
        public const string COMMON_GALEWIND_KIZUATO = "�Q�C���E�E�B���h�̏���";
        public const string COMMON_SIN_CRYSTAL_QUATZ = "�V���E�N���X�^���E�N�H�[�c";
        public const string COMMON_EVERMIND_OMEN = "�G�o�[�E�}�C���h�E�I�[����";
        public const string RARE_FROZEN_LAVA = "���������n��";
        public const string RARE_WHITE_TIGER_ANGEL_GOHU = "�ߌՓV�g�̌아";
        public const string RARE_POWER_STEEL_RING_SOLID = "���c�|�̘r�ցw�\���b�h�x";
        public const string RARE_POWER_STEEL_RING_VAPOR = "���c�|�̘r�ցw���F�C�p�[�x";
        public const string RARE_POWER_STEEL_RING_ERASTIC = "���c�|�̘r�ցw�G���X�g�x";
        public const string RARE_POWER_STEEL_RING_TORAREITION = "���c�|�̘r�ցw�g�����C�X�x";
        public const string RARE_SYUURENSYA_KUROOBI = "�C���҂̍���";
        public const string RARE_SHIHANDAI_KUROOBI = "�t�͑�̍���";
        public const string RARE_SYUUDOUSOU_KUROOBI = "�C���m�̍���";
        public const string RARE_KUGYOUSYA_KUROOBI = "��s�҂̍���";
        public const string RARE_TEARS_END = "�e�B�A�[�Y�E�G���h";
        public const string RARE_SKY_COLD_BOOTS = "�X�J�C�E�R�[���h�E�u�[�c";
        public const string RARE_EARTH_BREAKERS_SIGIL = "�A�[�X�E�u���C�J�[�Y�E�V�M��";
        public const string RARE_AERIAL_VORTEX = "�G�A���A���E���H���e�b�N�X";
        public const string RARE_LIVING_GROWTH_SEED = "�����B���O�E�O���[�X�E�V�[�h";
        public const string RARE_SHARPNEL_SPIN_BLADE = "�V���[�v�l���E�X�s���E�u���C�h";
        public const string RARE_BLUE_LIGHT_MOON_CLAW = "�������鑓����";
        public const string RARE_BLIZZARD_SNOW_ROD = "�u���U�[�h�E�X�m�[�E���b�h";
        public const string RARE_SHAERING_BONE_CRUSHER = "�V�A�����O�E�{�[���E�N���b�V���[";
        public const string RARE_SCALE_BLUERAGE = "�X�P�C���E�I�u�E�u���[���C�W";
        public const string RARE_BLUE_REFLECT_ROBE = "�u���[�E���t���N�g�E���[�u";
        public const string RARE_SLIDE_THROUGH_SHIELD = "�X���C�h�E�X���[�E�V�[���h";
        public const string RARE_ELEMENTAL_STAR_SHIELD = "�G�������^���E�X�^�[�E�V�[���h";
        #endregion

        #region "�_���W�����R�K�̕�"
        public const string COMMON_ESSENCE_OF_EARTH = "�G�b�Z���X�E�I�u�E�A�[�X";
        public const string COMMON_KESSYOU_SEA_WATER_SALT = "�����������C����";
        public const string COMMON_STAR_DUST_RING = "�X�^�[�_�X�g�E�����O";
        public const string COMMON_RED_ONION = "�ԃ^�}�l�M";
        public const string RARE_TAMATEBAKO_AKIDAMA = "�ʎ蔠�w�H�ʁx";
        public const string RARE_HARDEST_FIT_BOOTS = "�n�[�f�X�g�E�t�B�b�g�E�u�[�c";
        public const string COMMON_WATERY_GROBE = "�E�H�[�^���[�E�O���[��";
        public const string COMMON_WHITE_POWDER = "�z���C�g�E�p�E�_�[";
        public const string COMMON_SILENT_BOWL = "�T�C�����g�E�{�[��";
        public const string RARE_SEAL_OF_ICE = "�V�[���E�I���E�A�C�X";
        public const string RARE_SWORD_OF_DIVIDE = "�\�[�h�E�I�u�E�f�B�o�C�h";
        public const string EPIC_OLD_TREE_MIKI_DANPEN = "�Ñ�h���̊��̒f��";
        #endregion

        #region "�S�K�̃����_���h���b�v"
        // �����Poor�͕s�v�B
        //public const string RARE_PURPLE_ABYSSAL_SWORD = "�p�[�v���E�A���B�b�T���E�\�[�h";
        //public const string RARE_BLACK_HIEN_CLAW = "���򉍂̒�";
        //public const string POOR_DIRTY_ANGEL_CONTRACT = "�{���{���ɂȂ����V�g�̌_��";
        //public const string POOR_JUNK_TARISMAN_FROZEN = "�W�����N�E�^���X�}���y�����z";
        //public const string POOR_JUNK_TARISMAN_PARALYZE = "�W�����N�E�^���X�}���y��Ⴡz";
        //public const string POOR_JUNK_TARISMAN_STUN = "�W�����N�E�^���X�}���y�X�^���z";
        public const string COMMON_RED_MEDALLION = "���b�h�E���_���I��";
        public const string COMMON_BLUE_MEDALLION = "�u���[�E���_���I��";
        public const string COMMON_PURPLE_MEDALLION = "�p�[�v���E���_���I��";
        public const string COMMON_GREEN_MEDALLION = "�O���[���E���_���I��";
        public const string COMMON_YELLOW_MEDALLION = "�C�G���[�E���_���I��";
        public const string COMMON_SOCIETY_SYMBOL = "�\�T�G�e�B�E�V���{��";
        public const string COMMON_SILVER_FEATHER_BRACELET = "��H����̘r��";
        public const string COMMON_BIRD_SONG_LUTE = "�o�[�h�E�\���O�E�����[�g";
        public const string COMMON_MAZE_CUBE = "���C�Y�E�L���[�u";
        public const string COMMON_LIGHT_SERVANT = "���C�g�E�T�[���@���g";
        public const string COMMON_SHADOW_SERVANT = "�V���h�E�E�T�[���@���g";
        public const string COMMON_ROYAL_GUARD_RING = "���C�����E�K�[�h�E�����O";
        public const string COMMON_ELEMENTAL_GUARD_RING = "�G�������^���E�K�[�h�E�����O";
        public const string COMMON_HAYATE_GUARD_RING = "�n���e�E�K�[�h�E�����O";
        public const string RARE_SPELL_COMPASS = "�r���̗��j��";
        public const string RARE_SHADOW_BIBLE = "�ł̃o�C�u��";
        public const string RARE_DETACHMENT_ORB = "�f�^�b�`�����g�E�I�[�u";
        public const string RARE_BLIND_NEEDLE = "�Ӗڎ҂̐j";
        public const string RARE_CORE_ESSENCE_CHANNEL = "�R�A�E�G�b�Z���X�E�`���l��";
        public const string COMMON_MASTER_SWORD = "�}�X�^�[�E�\�[�h";
        public const string COMMON_MASTER_KNUCKLE = "�}�X�^�[�E�i�b�N��";
        public const string COMMON_MASTER_ROD = "�}�X�^�[�E���b�h";
        public const string COMMON_MASTER_AXE = "�}�X�^�[�E�A�b�N�X";
        public const string COMMON_MASTER_ARMOR = "�}�X�^�[�E�A�[�}�[";
        public const string COMMON_MASTER_CROSS = "�}�X�^�[�E�N���X";
        public const string COMMON_MASTER_ROBE = "�}�X�^�[�E���[�u";
        public const string COMMON_MASTER_SHIELD = "�}�X�^�[�E�V�[���h";
        public const string RARE_ASTRAL_VOID_BLADE = "�A�X�g�����E���H�C�h�E�u���[�h";
        public const string RARE_VERDANT_SONIC_CLAW = "���F���_���g�E�\�j�b�N�E�N���[";
        public const string RARE_PRISONER_BREAKING_AXE = "�v���Y�i�[�E�u���C�L���O�E�A�b�N�X";
        public const string RARE_INVISIBLE_STATE_ROD = "�C�����B�W�u���E�X�e�C�g�E���b�h";
        public const string RARE_DOMINATION_BRAVE_ARMOR = "�h�~�l�[�V�����E�u���C�u�E�A�[�}�[";

        public const string COMMON_RED_FLOAT_STONE = "�Ԃ̕��V��";
        public const string COMMON_BLUE_FLOAT_STONE = "�̕��V��";
        public const string COMMON_PURPLE_FLOAT_STONE = "���̕��V��";
        public const string COMMON_GREEN_FLOAT_STONE = "�΂̕��V��";
        public const string COMMON_YELLOW_FLOAT_STONE = "���̕��V��";
        public const string COMMON_SILVER_RING_1 = "��̘r�ցy�Ɖ΁z";
        public const string COMMON_SILVER_RING_2 = "��̘r�ցy�Ôg�z";
        public const string COMMON_SILVER_RING_3 = "��̘r�ցy�H�J�z";
        public const string COMMON_SILVER_RING_4 = "��̘r�ցy�M�g�z";
        public const string COMMON_SILVER_RING_5 = "��̘r�ցy���z";
        public const string COMMON_SILVER_RING_6 = "��̘r�ցy����z";
        public const string COMMON_SILVER_RING_7 = "��̘r�ցy�����z";
        public const string COMMON_SILVER_RING_8 = "��̘r�ցy�����z";
        public const string COMMON_SILVER_RING_9 = "��̘r�ցy����z";
        public const string COMMON_SILVER_RING_10 = "��̘r�ցy�z���z";
        public const string COMMON_MUKEI_SAKAZUKI = "���`�̔u";
        public const string COMMON_RAINBOW_TUBE = "���C���{�[�E�`���[�u";
        public const string COMMON_ELDER_PERSPECTIVE_GRASS = "�G���_�[�E�p�[�X�y�N�e�B�u�E�O���X";
        public const string COMMON_DEVIL_SEALED_VASE = "���������̚�";
        public const string COMMON_FLOATING_WHITE_BALL = "�t���[�e�B���O�E�z���C�g�E�{�[��";
        public const string RARE_SEAL_OF_ASSASSINATION = "�V�[���E�I�u�E�A�T�V�l�[�V����";
        public const string RARE_EMBLEM_OF_VALKYRIE = "�G���u�����E�I�u�E���@���L���[";
        public const string RARE_EMBLEM_OF_HADES = "�G���u�����E�I�u�E�n�f�X";
        public const string RARE_SIHAIRYU_KATAUDE = "�x�z���̕Иr";
        public const string RARE_OLD_TREE_SINKI = "�Ñ�h���̐c��";
        public const string RARE_GALEWIND_IBUKI = "�Q�C���E�E�B���h�̑���";
        public const string RARE_SIN_CRYSTAL_SOLID = "�V���E�N���X�^���E�\���b�h";
        public const string RARE_EVERMIND_SENSE = "�G�o�[�E�}�C���h�E�Z���X";
        public const string RARE_DEVIL_SUMMONER_TOME = "�f�r���E�T���i�[�Y�E�g�[��";
        public const string RARE_ANGEL_CONTRACT = "�V�g�̌_��";
        public const string RARE_ARCHANGEL_CONTRACT = "��V�g�̌_��";
        public const string RARE_DARKNESS_COIN = "�Í��̒ʉ�";
        public const string RARE_SOUSUI_HIDENSYO = "�����̔�`��";
        public const string RARE_MEEK_HIDENSYO = "��҂̔�`��";
        public const string RARE_JUKUTATUSYA_HIDENSYO = "�n�B�҂̔�`��";
        public const string RARE_KYUUDOUSYA_HIDENSYO = "�����҂̔�`��";
        public const string RARE_DANZAI_ANGEL_GOHU = "�f�ߓV�g�̌아";
        public const string EPIC_ETERNAL_HOMURA_RING = "�s���Ȃ鉋�΂̃����O";

        public const string COMMON_INITIATE_SWORD = "�C�j�V�G�C�g�E�\�[�h";
        public const string COMMON_BULLET_KNUCKLE = "�o���b�g�E�i�b�N��";
        public const string COMMON_KENTOUSI_SWORD = "�����m�̑匕";
        public const string COMMON_ELECTRO_ROD = "�G���N�g���E���b�h";
        public const string COMMON_FORTIFY_SCALE = "�t�H�[�e�B�t�@�C�E�X�P�C��";
        public const string COMMON_MURYOU_CROSS = "���ʂ̕�����";
        public const string COMMON_COLORLESS_ROBE = "�J�����X�E���[�u";
        public const string COMMON_LOGISTIC_SHIELD = "���W�X�e�B�b�N�E�V�[���h";
        public const string RARE_ETHREAL_EDGE_SABRE = "�C�X���A���E�G�b�W�E�T�[�x��";
        public const string RARE_SHINGETUEN_CLAW = "�[�����̒�";
        public const string RARE_BLOODY_DIRTY_SCYTHE = "�u���b�f�B�E�_�[�e�B�E�T�C";
        public const string RARE_ALL_ELEMENTAL_ROD = "�I�[���E�G�������^���E���b�h";
        public const string RARE_BLOOD_BLAZER_CROSS = "�u���b�h�E�u���C�U�[�E�N���X";
        public const string RARE_DARK_ANGEL_ROBE = "�_�[�N�E�G���W�F���E���[�u";
        public const string RARE_MAJEST_HAZZARD_SHIELD = "�}�W�F�X�g�E�n�U�[�h�E�V�[���h";
        public const string RARE_WHITE_DIAMOND_SHIELD = "�����F�̃_�C���E�V�[���h";
        public const string RARE_VAPOR_SOLID_SHIELD = "���F�C�p�[�E�\���b�h�E�V�[���h";
        #endregion

        #region "�_���W�����S�K�̕�"
        public const string COMMON_BLACK_SALT = "�����ϐF�������̉�";
        public const string COMMON_FEBL_ANIS = "�t�F�u���E�A�j�X";
        public const string COMMON_SMORKY_HUNNY = "�X���[�L�[�E�n�j�[";
        public const string COMMON_ANGEL_DUST = "�G���W�F���E�_�X�g";
        public const string COMMON_SUN_TARAGON = "�T���E�^���S��";
        public const string COMMON_ECHO_BEAST_MEAT = "�G�R�[�r�[�X�g�̂�����";
        public const string COMMON_CHAOS_TONGUE = "�J�I�X�E���[�f���̐�";
        public const string RARE_JOUKA_TANZOU = "��΂̒b��";
        public const string RARE_ESSENCE_OF_ADAMANTINE = "�G�b�Z���X�E�I�u�E�A�_�}���e�B";
        #endregion

        #region "�T�K�̃����_���h���b�v"
        public const string COMMON_RED_CRYSTAL = "�^�g�̃N���X�^��";
        public const string COMMON_BLUE_CRYSTAL = "�ڗ��̃N���X�^��";
        public const string COMMON_PURPLE_CRYSTAL = "�����̃N���X�^��";
        public const string COMMON_GREEN_CRYSTAL = "�Ő��̃N���X�^��";
        public const string COMMON_YELLOW_CRYSTAL = "���߂̃N���X�^��";
        public const string COMMON_PLATINUM_RING_1 = "�����̘r�ցy���Ձz";
        public const string COMMON_PLATINUM_RING_2 = "�����̘r�ցy���@���L���[�z";
        public const string COMMON_PLATINUM_RING_3 = "�����̘r�ցy�i�C�g���A�z";
        public const string COMMON_PLATINUM_RING_4 = "�����̘r�ցy�i���V���n�z";
        public const string COMMON_PLATINUM_RING_5 = "�����̘r�ցy�鐝�z";
        public const string COMMON_PLATINUM_RING_6 = "�����̘r�ցy�E���{���X�z";
        public const string COMMON_PLATINUM_RING_7 = "�����̘r�ցy�i�C���e�C���z";
        public const string COMMON_PLATINUM_RING_8 = "�����̘r�ցy�x�q���X�z";
        public const string COMMON_PLATINUM_RING_9 = "�����̘r�ցy���z";
        public const string COMMON_PLATINUM_RING_10 = "�����̘r�ցy�����z";
        #endregion

        #region "�_���W�����������E�̕�"
        public const string EPIC_GOLD_POTION = "�������̔��";
        public const string EPIC_OVER_SHIFTING = "�I�[�o�[�V�t�e�B���O";
        #endregion

        #region "�e�K�̃{�X���j��"
        public const string EPIC_ORB_GROW_GREEN = "�V���΂̕��";
        public const string EPIC_ORB_GROUNDSEA_STAR = "�C�����̕��";
        public const string EPIC_ORB_SILENT_COLD_ICE = "�X���̕��";
        public const string EPIC_ORB_DESTRUCT_FIRE = "���򍭂̕��"; // "�Ō����̕��"
        #endregion

        #region "EPIC"
        // EPIC�P
        public const string EPIC_RING_OF_OSCURETE = "Ring of the Oscurete";
        public const string EPIC_MERGIZD_SOL_BLADE = "Mergizd Sol Blade";
        // EPIC�Q
        public const string EPIC_GARVANDI_ADILORB = "AdilOrb of the Garvandi";
        public const string EPIC_MAXCARN_X_BUSTER = "Maxcarn the X-BUSTER";
        public const string EPIC_JUZA_ARESTINE_SLICER = "Arestine-Slicer of Juza";
        // EPIC�R
        public const string EPIC_SHEZL_MYSTIC_FORTUNE = "Shezl the Mystic Fortune";
        public const string EPIC_FLOW_FUNNEL_OF_THE_ZVELDOZE = "Flow Funnel of the Zveldose";
        public const string EPIC_MERGIZD_DAV_AGITATED_BLADE = "Mergizd DAV-Agitated Blade";
        // EPIC�S
        public const string EPIC_EZEKRIEL_ARMOR_SIGIL = "Ezekriel the Armor of Sigil";
        public const string EPIC_SHEZL_THE_MIRAGE_LANCER = "Shezl the Mirage Lancer";
        public const string EPIC_JUZA_THE_PHANTASMAL_CLAW = "Juza the Phantasmal Claw";
        public const string EPIC_ADILRING_OF_BLUE_BURN = "AdilRing of the Blue Burn";
        #endregion

        // �W�F��
        // �X�g�[��
        // �W���G��
        // �t�����x���W��
        #region "������"
        // �����܁i�P�K�j
        public const string GROWTH_LIQUID_STRENGTH = "�������L�b�h�y�́z";
        public const string GROWTH_LIQUID_AGILITY = "�������L�b�h�y�Z�z";
        public const string GROWTH_LIQUID_INTELLIGENCE = "�������L�b�h�y�m�z";
        public const string GROWTH_LIQUID_STAMINA = "�������L�b�h�y�́z";
        public const string GROWTH_LIQUID_MIND = "�������L�b�h�y�S�z";
        // �����܁i�Q�K�j
        public const string GROWTH_LIQUID2_STRENGTH = "�������L�b�h�U�y�́z";
        public const string GROWTH_LIQUID2_AGILITY = "�������L�b�h�U�y�Z�z";
        public const string GROWTH_LIQUID2_INTELLIGENCE = "�������L�b�h�U�y�m�z";
        public const string GROWTH_LIQUID2_STAMINA = "�������L�b�h�U�y�́z";
        public const string GROWTH_LIQUID2_MIND = "�������L�b�h�U�y�S�z";
        // �����܁i�R�K�j
        public const string GROWTH_LIQUID3_STRENGTH = "�������L�b�h�V�y�́z";
        public const string GROWTH_LIQUID3_AGILITY = "�������L�b�h�V�y�Z�z";
        public const string GROWTH_LIQUID3_INTELLIGENCE = "�������L�b�h�V�y�m�z";
        public const string GROWTH_LIQUID3_STAMINA = "�������L�b�h�V�y�́z";
        public const string GROWTH_LIQUID3_MIND = "�������L�b�h�V�y�S�z";
        // �����܁i�S�K�j
        public const string GROWTH_LIQUID4_STRENGTH = "�������L�b�h�W�y�́z";
        public const string GROWTH_LIQUID4_AGILITY = "�������L�b�h�W�y�Z�z";
        public const string GROWTH_LIQUID4_INTELLIGENCE = "�������L�b�h�W�y�m�z";
        public const string GROWTH_LIQUID4_STAMINA = "�������L�b�h�W�y�́z";
        public const string GROWTH_LIQUID4_MIND = "�������L�b�h�W�y�S�z";
        // �����܁i�T�K�j
        public const string GROWTH_LIQUID5_STRENGTH = "�������L�b�h�X�y�́z";
        public const string GROWTH_LIQUID5_AGILITY = "�������L�b�h�X�y�Z�z";
        public const string GROWTH_LIQUID5_INTELLIGENCE = "�������L�b�h�X�y�m�z";
        public const string GROWTH_LIQUID5_STAMINA = "�������L�b�h�X�y�́z";
        public const string GROWTH_LIQUID5_MIND = "�������L�b�h�X�y�S�z";
        #endregion

        #region "�����l�A�C�e��"
        public const string POOR_BLACK_MATERIAL = "�u���b�N�}�e���A��";
        public const string POOR_BLACK_MATERIAL2 = "�u���b�N�}�e���A���y���z";
        public const string POOR_BLACK_MATERIAL3 = "�u���b�N�}�e���A���y���z";
        public const string POOR_BLACK_MATERIAL4 = "�u���b�N�}�e���A���y���z";
        public const string POOR_BLACK_MATERIAL5 = "�u���b�N�}�e���A���y�ҁz";
        #endregion

        public const string RARE_SEAL_AQUA_FIRE = "�V�[���I�u�A�N�A���t�@�C�A";

        #region "��ҁA�P�K�̑f�ތn�h���b�v�A�C�e��"
        public const string COMMON_YELLOW_MATERIAL = "�C�G���[�}�e���A��";
        public const string COMMON_WARM_NO_KOUKAKU = "���[���̍b�k";
        public const string COMMON_BEATLE_TOGATTA_TUNO = "�r�[�g���̐�����p";
        public const string COMMON_GREEN_SIKISO = "�Ή��F�f";
        public const string COMMON_MANDORAGORA_ROOT = "�}���h���S���̍�";

        public const string COMMON_SUN_LEAF = "���z�̗t";
        public const string COMMON_INAGO = "�";
        public const string COMMON_SPIDER_SILK = "�X�p�C�_�[�V���N";
        public const string COMMON_ALRAUNE_KAHUN = "�A�����E�l�̉ԕ�";
        public const string RARE_MARY_KISS = "�}���[�L�b�X";

        public const string COMMON_RABBIT_KEGAWA = "�E�T�M�̖є�";
        public const string COMMON_RABBIT_MEAT = "�E�T�M�̓�";
        public const string COMMON_TAKA_FETHER = "��̔��H";
        public const string COMMON_PLANTNOID_SEED = "�v�����g�m�C�h�̎�";
        public const string COMMON_TOGE_HAETA_SYOKUSYU = "�h�̐������G��";
        public const string RARE_HYUI_SEED = "�q���[�C�̎�";

        public const string COMMON_OOKAMI_FANG = "�T�̉�";
        public const string COMMON_BRILLIANT_RINPUN = "�P���̗ӕ�";
        public const string COMMON_RED_HOUSI = "�Ԃ��E�q";
        public const string RARE_MOSSGREEN_EKISU = "���X�O���[���̃G�L�X";
        #endregion

        #region "��ҁA�Q�K�A�f�ރh���b�v"
        public const string COMMON_DAGGERFISH_UROKO = "�勛�̗�";
        public const string COMMON_SIPPUU_HIRE = "�����̃q��";
        public const string COMMON_WHITE_MAGATAMA = "���̌���";
        public const string COMMON_BLUE_MAGATAMA = "�̌���";
        public const string COMMON_KURIONE_ZOUMOTU = "�N���I�l�̑���";
        public const string COMMON_BLUEWHITE_SHARP_TOGE = "���̉s���g�Q";
        public const string COMMON_RENEW_AKAMI = "�V�N�ȐԐg";
        public const string COMMON_SEA_WASI_KUTIBASI = "�C�h�̂����΂�";
        public const string COMMON_WASI_BLUE_FEATHER = "�h�̐H";
        public const string COMMON_GANGAME_EGG = "��T�̗�";
        public const string RARE_JOE_TONGUE = "�W���[�̐�";
        public const string RARE_JOE_ARM = "�W���[�̘r";
        public const string RARE_JOE_LEG = "�W���[�̑�";
        public const string COMMON_SOFT_BIG_HIRE = "�_�炩����q��";
        public const string COMMON_PURE_WHITE_BIGEYE = "�^�����ȑ�ڋ�";
        public const string COMMON_HALF_TRANSPARENT_ROCK_ASH = "�������̐ΊD";
        public const string COMMON_GOTUGOTU_KARA = "�S�c�S�c�����k";
        public const string RARE_SEKIKASSYOKU_HASAMI = "�Ԋ��F�̃n�T�~";
        public const string COMMON_KOUSITUKA_MATERIAL = "�d�����f��";
        public const string COMMON_NANAIRO_SYOKUSYU = "���F�̐G��";
        public const string COMMON_AOSAME_KENSHI = "�L�̌���";
        public const string COMMON_AOSAME_UROKO = "�L�̗�";
        public const string COMMON_EIGHTEIGHT_KUROSUMI = "�G�C�g�E�G�C�g�̍��n";
        public const string COMMON_EIGHTEIGHT_KYUUBAN = "�G�C�g�E�G�C�g�̋z��";
        #endregion

        #region "��ҁA�R�K�A�f�ރh���b�v"
        public const string COMMON_ORC_MOMONIKU = "�I�[�N�̂�����";
        public const string COMMON_SNOW_CAT_KEGAWA = "��L�̖є�";
        public const string COMMON_BIG_HIZUME = "�傫�Ȓ�";
        public const string COMMON_FAIRY_POWDER = "�d���p�E�_�[";
        public const string COMMON_GOTUGOTU_KONBOU = "�S�c�S�c�������_";
        public const string COMMON_LIZARD_UROKO = "���U�[�h�̗�";
        public const string COMMON_EMBLEM_OF_PENGUIN = "�G���u�����E�I�u�E�y���M��";
        public const string COMMON_SHARPNESS_TIGER_TOOTH = "�s��������Չ�";
        public const string RARE_BEAR_CLAW_KAKERA = "�x�A�N���[�̌���";
        public const string COMMON_TOUMEI_SNOW_CRYSTAL = "�����Ȑጋ��";
        public const string COMMON_WHITE_AZARASHI_MEAT = "���A�U���V�̓�";
        public const string COMMON_ARGONIAN_PURPLE_UROKO = "�A���S�j�A���̎���";
        public const string COMMON_BLUE_DANGAN_KAKERA = "�����e�ۂ̌���";
        public const string RARE_PURE_CRYSTAL = "�s���A�E�N���X�^��";
        public const string COMMON_WOLF_KEGAWA = "�E���t�̖є�";
        public const string COMMON_FROZEN_HEART = "���������S��";
        public const string COMMON_ESSENCE_OF_WIND = "�G�b�Z���X�E�I���E�E�B���h";
        public const string RARE_TUNDRA_DEER_HORN = "�Ñ�c���h�����̊p";
        #endregion

        #region "��ҁA�S�K�A�f�ރh���b�v"
        public const string COMMON_HUNTER_SEVEN_TOOL = "�n���^�[�̎�����";
        public const string COMMON_BEAST_KEGAWA = "�ҏb�̖є�";
        public const string COMMON_EXECUTIONER_ROBE = "���s�l�̉��ꂽ���[�u";
        public const string RARE_ANGEL_SILK = "�V�g�̃V���N";
        public const string COMMON_SABI_BUGU = "�K�t�����K���N�^����";
        public const string RARE_ESSENCE_OF_DARK = "�G�b�Z���X�E�I�u�E�_�[�N";
        public const string COMMON_SEEKER_HEAD = "�V�[�J�[�̓��W��";
        public const string RARE_MASTERBLADE_KAKERA = "�}�X�^�[�u���C�h�̔j��";
        public const string RARE_ESSENCE_OF_FLAME = "�G�b�Z���X�E�I�u�E�t���C��";
        public const string COMMON_GREAT_JEWELCROWN = "���؂ȃW���G���N���E��";
        public const string COMMON_ONRYOU_HAKO = "���씠";
        public const string COMMON_KUMITATE_TENBIN = "�g�ݗ��đf�ށ@�V��";
        public const string COMMON_KUMITATE_TENBIN_DOU = "�g�ݗ��đf�ށ@�V����";
        public const string COMMON_KUMITATE_TENBIN_BOU = "�g�ݗ��đf�ށ@�V���_";
        public const string RARE_DOOMBRINGER_TUKA = "�h�D�[���u�����K�[�̕�";
        public const string RARE_DOOMBRINGER_KAKERA = "�h�D�[���u�����K�[�̌���";

        public const string RARE_BLOOD_DAGGER_KAKERA = "���h��ꂽ�_�K�[�̔j��";
        public const string RARE_DEMON_HORN = "�f�[�����z�[��";
        public const string RARE_ESSENCE_OF_SHINE = "�G�b�Z���X�E�I�u�E�V���C��";
        public const string RARE_BLACK_SEAL_IMPRESSION = "���̈��";
        public const string RARE_CHAOS_SIZUKU = "���ׂ̎�";

        public const string RARE_MASTERBLADE_FIRE = "�}�X�^�[�u���C�h�̎c���";
        #endregion

        #region "��ҁA�K���c�̕���P�K"
        // ����
        public const string COMMON_BRONZE_SWORD = "�u�����Y�E�\�[�h";
        public const string COMMON_FIT_ARMOR = "�t�B�b�g�E�A�[�}�[";
        public const string COMMON_LIGHT_SHIELD = "���C�g�E�V�[���h";
        public const string COMMON_FINE_SWORD_1 = "�t�@�C���E�\�[�h�y�{�P�z";
        public const string COMMON_FINE_ARMOR_1 = "�t�@�C���E�A�[�}�[�y�{�P�z";
        public const string COMMON_FINE_SHIELD_1 = "�t�@�C���E�V�[���h�y�{�P�z";
        public const string COMMON_LIGHT_CLAW_1 = "���C�g�E�N���[�y�{�P�z";
        public const string COMMON_KASHI_ROD_1 = "�~�̏�y�{�P�z";
        public const string COMMON_LETHER_CLOTHING_1 = "���U�[�E�N���X�y�{�P�z";
        public const string COMMON_BASTARD_SWORD_1 = "�o�X�^�[�h�E�\�[�h�y�{�P�z";
        public const string COMMON_IRON_SWORD = "�A�C�A���E�\�[�h�y�{�Q�z";
        public const string COMMON_KUSARI_KATABIRA = "�������т�y�{�P�z";
        public const string RARE_FLOWER_WAND = "�t�����[�E�����h";
        public const string COMMON_SUPERIOR_CROSS = "�X�y���I���E�N���X";
        public const string COMMON_SILK_ROBE = "�V���N�E���[�u";
        public const string COMMON_SURVIVAL_CLAW = "�T�o�C�o���E�N���[";
        public const string COMMON_BLACER_OF_SYOJIN = "���i�̃u���X���b�g";
        public const string COMMON_ZIAI_PENDANT = "�����̃y���_���g";
        // ����
        public const string COMMON_KOUKAKU_ARMOR = "�b�k�̊Z";
        public const string COMMON_SISSO_TUKEHANE = "���f�ȕt���H";
        public const string COMMON_BLUE_COPPER_ARMOR_KAI = "���̊Z�y���z";
        public const string COMMON_RABBIT_SHOES = "���r�b�g�E�V���[�Y";
        public const string RARE_WAR_WOLF_BLADE = "���[�E���t�E�u���[�h";
        #endregion
        #region "��ҁA�K���c�̕���Q�K"
        // �Q�K
        // ����
        public const string COMMON_SMART_SWORD_2 = "�X�}�[�g�E�\�[�h�y�{�Q�z";
        public const string COMMON_SMART_PLATE_2 = "�X�}�[�g�E�v���[�g�y�{�Q�z";
        public const string COMMON_SMART_SHIELD_2 = "�X�}�[�g�E�V�[���h�y�{�Q�z";
        public const string COMMON_RAUGE_SWORD_2 = "���E�W�F�E�\�[�h�y�{�Q�z";
        public const string COMMON_SMART_CLAW_2 = "�X�}�[�g�E�N���[�y�{�Q�z";
        public const string COMMON_SMART_ROD_2 = "�X�}�[�g�E���b�h�y�{�Q�z";
        public const string COMMON_SMART_CLOTHING_2 = "�X�}�[�g�E�N���X�y�{�Q�z";
        public const string COMMON_SMART_ROBE_2 = "�X�}�[�g�E���[�u�y�{�Q�z";
        public const string COMMON_STEEL_SWORD = "�X�`�[���E�\�[�h�y�{�R�z";
        public const string COMMON_FACILITY_CLAW = "�t�@�V���e�B�E�N���[�y�{�R�z";
        public const string COMMON_MIX_HINOKI_ROD = "�����w�̏�";
        public const string COMMON_BERSERKER_PLATE = "�o�[�T�[�J�[�v���[�g";
        public const string COMMON_BRIGHTNESS_ROBE = "�u���C�g�l�X�E���[�u";
        public const string RARE_WILD_HEART_SPADE = "���C���h�E�n�[�g�y�X�y�[�h�z";

        // ����
        public const string COMMON_WHITE_WAVE_RING = "���g�̎w��";
        public const string COMMON_NEEDLE_FEATHER = "�j�[�h���t�F�U�[";
        public const string COMMON_KOUSHITU_ORB = "�d���̃I�[�u";
        public const string RARE_RED_ARM_BLADE = "���b�h�A�[���u���[�h";
        public const string RARE_STRONG_SERPENT_CLAW = "���x�ȃT�[�y���g�N���[";
        public const string RARE_STRONG_SERPENT_SHIELD = "���x�ȃT�[�y���g�V�[���h";
        #endregion
        #region "��ҁA�K���c�̕���R�K"
        // ����
        public const string COMMON_EXCELLENT_SWORD_3 = "�G�N�Z�����g�E�\�[�h�y�{�R�z";
        public const string COMMON_EXCELLENT_KNUCKLE_3 = "�G�N�Z�����g�E�i�b�N���y�{�R�z";
        public const string COMMON_EXCELLENT_ROD_3 = "�G�N�Z�����g�E���b�h�y�{�R�z";
        public const string COMMON_EXCELLENT_BUSTER_3 = "�G�N�Z�����g�E�o�X�^�[�y�{�R�z";
        public const string COMMON_EXCELLENT_ARMOR_3 = "�G�N�Z�����g�E�A�[�}�[�y�{�R�z";
        public const string COMMON_EXCELLENT_CROSS_3 = "�G�N�Z�����g�E�N���X�y�{�R�z";
        public const string COMMON_EXCELLENT_ROBE_3 = "�G�N�Z�����g�E���[�u�y�{�R�z";
        public const string COMMON_EXCELLENT_SHIELD_3 = "�G�N�Z�����g�E�V�[���h�y�{�R�z";
        public const string COMMON_WINTERS_HORN = "�E�B���^�[�Y�E�z�[��";
        public const string RARE_CHILL_BONE_SHIELD = "�`���E�{�[���E�V�[���h";               
        // ����
        public const string COMMON_STEEL_BLADE = "�X�`�[���E�u���[�h�y�{�S�z";
        public const string COMMON_SNOW_GUARD = "�X�m�[�K�[�h";
        public const string COMMON_LIZARDSCALE_ARMOR = "���U�[�h�X�P�C���E�A�[�}�[�y�{�S�z";
        public const string COMMON_PENGUIN_OF_PENGUIN = "�y���M���E�I�u�E�y���M��";
        public const string COMMON_ARGNIAN_TUNIC = "�A���S�j�A���E�`���j�b�N";
        public const string COMMON_WOLF_BATTLE_CLOTH = "�E���t���̕�����";
        public const string RARE_SPLASH_BARE_CLAW = "�X�v���b�V���E�x�A�N���[";
        public const string EPIC_GATO_HAWL_OF_GREAT = "�K�g�D�E�n�E���E�I�u�E�O���C�g";
        #endregion
        #region "��ҁA�K���c�̕���S�K"
        // ����
        public const string COMMON_MASTER_SWORD_4 = "�}�X�^�[�E�\�[�h�y�{�S�z";
        public const string COMMON_MASTER_KNUCKLE_4 = "�}�X�^�[�E�i�b�N���y�{�S�z";
        public const string COMMON_MASTER_ROD_4 = "�}�X�^�[�E���b�h�y�{�S�z";
        public const string COMMON_MASTER_AXE_4 = "�}�X�^�[�E�A�b�N�X�y�{�S�z";
        public const string COMMON_MASTER_ARMOR_4 = "�}�X�^�[�E�A�[�}�[�y�{�S�z";
        public const string COMMON_MASTER_CROSS_4 = "�}�X�^�[�E�N���X�y�{�S�z";
        public const string COMMON_MASTER_ROBE_4 = "�}�X�^�[�E���[�u�y�{�S�z";
        public const string COMMON_MASTER_SHIELD_4 = "�}�X�^�[�E�V�[���h�y�{�S�z";
        public const string RARE_SUPERIOR_CHOSEN_ROD = "�X�y���I���E�`���[�Y���E���b�h";
        public const string RARE_TYOU_KOU_SWORD = "���d�̌��y�{�U�z";
        public const string RARE_TYOU_KOU_ARMOR = "���d�̊Z�y�{�U�z";
        public const string RARE_TYOU_KOU_SHIELD = "���d�̏��y�{�U�z";
        public const string RARE_WHITE_GOLD_CROSS = "�z���C�g�E�S�[���h�E�N���X";
        // ����
        public const string RARE_HUNTERS_EYE = "�n���^�[�Y�E�A�C";
        public const string RARE_ONEHUNDRED_BUTOUGI = "�S�b��̕�����";
        public const string RARE_DARKANGEL_CROSS = "�_�[�N�G���W�F���E�N���X";
        public const string RARE_DEVIL_KILLER = "�f�r���E�L���[";
        public const string RARE_TRUERED_MASTER_BLADE = "�^�g���E�}�X�^�[�u���C�h";
        public const string RARE_VOID_HYMNSONIA = "���H�C�h�E�q���\�j�A";
        public const string RARE_SEAL_OF_BALANCE = "�V�[���E�I�u�E�o�����X";
        public const string RARE_DOOMBRINGER = "�h�D�[���E�u�����K�[";
        public const string EPIC_MEIKOU_DOOMBRINGER = "�����E�h�D�[���u�����K�[";             
        #endregion
        #region "��ҁA�K���c�̕���T�K"
        #endregion
        #region "�^�����E"
        #endregion

        #region "��ҁA���i�̃|�[�V�����X�P�K"
        // �|�[�V�����n �O�҂��炠��̂��������Ă��邪�A��҂ł͂����Ő錾
        // ����
        public const string POOR_SMALL_RED_POTION = "�������ԃ|�[�V����";
        public const string POOR_SMALL_BLUE_POTION = "�������|�[�V����";
        public const string POOR_SMALL_GREEN_POTION = "�������΃|�[�V����";
        public const string POOR_POTION_CURE_POISON = "��Ŗ�";
        public const string COMMON_REVIVE_POTION_MINI = "�����@�C���|�[�V�����E�~�j";
        // ����
        public const string COMMON_POTION_NATURALIZE = "�i�`�������C�Y�E�|�[�V����";
        public const string COMMON_POTION_MAGIC_SEAL = "�}�W�b�N�E�V�[����";
        public const string COMMON_POTION_ATTACK_SEAL = "�A�^�b�N�E�V�[����";
        public const string COMMON_POTION_CURE_BLIND = "�L���A�E�u���C���h";
        public const string RARE_POTION_MOSSGREEN_DREAM = "���X�O���[���E�h���[��";
        #endregion
        #region "��ҁA���i�̃|�[�V�����X�Q�K"
        // �Q�K
        // ����
        public const string COMMON_NORMAL_RED_POTION = "�ԃ|�[�V�����y�ʏ�z";
        public const string COMMON_NORMAL_BLUE_POTION = "�|�[�V�����y�ʏ�z";
        public const string COMMON_NORMAL_GREEN_POTION = "�΃|�[�V�����y�ʏ�z";
        public const string COMMON_RESIST_POISON = "�ω�Ń|�[�V����"; // �_���W�����Q�K�i�ޏ��Ń��i���i�i
        // ����
        public const string COMMON_POTION_OVER_GROWTH = "�I�[�o�[�E�O���[�X";
        public const string COMMON_POTION_RAINBOW_IMPACT = "���C���{�[�E�C���p�N�g";
        public const string COMMON_POTION_BLACK_GAST = "�u���b�N�E�K�X�g";
        #endregion
        #region "��ҁA���i�̃|�[�V�����X�R�K"
        // �R�K
        // ����
        public const string COMMON_LARGE_RED_POTION = "�ԃ|�[�V�����y��z";
        public const string COMMON_LARGE_BLUE_POTION = "�|�[�V�����y��z";
        public const string COMMON_LARGE_GREEN_POTION = "�΃|�[�V�����y��z";
        // ����
        public const string COMMON_FAIRY_BREATH = "�t�F�A���[�E�u���X";
        public const string COMMON_HEART_ACCELERATION = "�n�[�g�E�A�N�Z�����[�V����";
        public const string RARE_SAGE_POTION_MINI = "���҂̔��y�~�j�z";
        #endregion
        #region "��ҁA���i�̃|�[�V�����X�S�K"
        // �S�K
        // ����
        public const string COMMON_HUGE_RED_POTION = "�ԃ|�[�V�����y����z";
        public const string COMMON_HUGE_BLUE_POTION = "�|�[�V�����y����z";
        public const string COMMON_HUGE_GREEN_POTION = "�΃|�[�V�����y����z";
        // ����
        public const string RARE_POWER_SURGE = "�p���[�E�T�[�W";
        public const string RARE_ELEMENTAL_SEAL = "�G�������^���E�V�[��";
        public const string RARE_GENSEI_MAGIC_BOTTLE = "�����̖��͍�";
        public const string RARE_GENSEI_TAIMA_KUSURI = "�����̑ޖ���";
        public const string RARE_MIND_ILLUSION = "�}�C���h�E�C�����[�W����";
        public const string RARE_SHINING_AETHER = "�V���C�j���O�E�G�[�e��";       
        public const string RARE_ZETTAI_STAMINAUP = "�m��̗̑͑�����";
        public const string RARE_BLACK_ELIXIR = "�u���b�N�E�G���N�V�[��";        
        public const string RARE_ZEPHER_BREATH = "�[�t�B�[���E�u���X";
        public const string RARE_COLORESS_ANTIDOTE = "�J�����X�E�A���`�h�[�e";
        
        #endregion
        #region "��ҁA���i�̃|�[�V�����X�T�K"
        // �T�K
        // ����
        public const string COMMON_GORGEOUS_RED_POTION = "�ԃ|�[�V�����y���ؔŁz";
        public const string COMMON_GORGEOUS_BLUE_POTION = "�|�[�V�����y���ؔŁz";
        public const string COMMON_GORGEOUS_GREEN_POTION = "�΃|�[�V�����y���ؔŁz";
        #endregion
        #region "�^�����E"
        public const string RARE_PERFECT_RED_POTION = "�ԃ|�[�V�����y���S�Łz";
        public const string RARE_PERFECT_BLUE_POTION = "�|�[�V�����y���S�Łz";
        public const string RARE_PERFECT_GREEN_POTION = "�΃|�[�V�����y���S�Łz";
        #endregion

        // ��ҁA�n���i�̗�����
        #region "�H��"
        // �P�K
        public const string FOOD_BIFUKATU = "�r�[�t�J�c��H";
        public const string FOOD_GEKIKARA_CURRY = "���h�J���[";
        public const string FOOD_INAGO = "�C�i�S�̒ώϒ�H";
        public const string FOOD_USAGI = "�E�T�M���̃V�`���[";
        public const string FOOD_SANMA = "�T���}��H�i�ϕ��Y���j";
        // �Q�K
        public const string FOOD_FISH_GURATAN = "�t�B�b�V���E�O���^��";
        public const string FOOD_SEA_TENPURA = "�C�N�T�N�T�N�V�v��";
        public const string FOOD_TRUTH_YAMINABE_1 = "�^���̈œ�i�p�[�g�P�j";
        public const string FOOD_OSAKANA_ZINGISKAN = "�����W���M�X�J��";
        public const string FOOD_RED_HOT_SPAGHETTI = "���b�h�z�b�g�E�X�p�Q�b�e�B";
        // �R�K
        public const string FOOD_HINYARI_YASAI = "�q�������E�J�����Ɩ�ؒ�H";
        public const string FOOD_AZARASI_SHIOYAKI = "���A�U���V�̉��Ă�";
        public const string FOOD_WINTER_BEEF_CURRY = "�E�B���^�[�E�r�[�t�J���[";
        public const string FOOD_GATTURI_GOZEN = "�K�b�c��������V";
        public const string FOOD_KOGOERU_DESSERT = "�g��������E�u���[�f�U�[�g";
        // �S�K
        public const string FOOD_BLACK_BUTTER_SPAGHETTI = "�u���b�N�o�^�[�E�X�p�Q�b�e�B";
        public const string FOOD_KOROKORO_PIENUS_HAMBURG = "�R���R���E�s�[�i�b�c�E�n���o�[�O";
        public const string FOOD_PIRIKARA_HATIMITSU_STEAK = "�s���h�E�n�`�~�c�X�e�[�L��H";
        public const string FOOD_HUNWARI_ORANGE_TOAST = "�ӂ���E�I�����W�g�[�X�g";
        public const string FOOD_TRUTH_YAMINABE_2 = "�^���̈œ�i�p�[�g�Q�j";
        #endregion

        #endregion

        #region "�摜�t�@�C��"
        public const string IMAGE_DRAGON_BRIYARD = "Dragon_Briyard.png";
        public const string IMAGE_DRAGON_DEEPSEA = "Dragon_DeepSea.png";
        public const string IMAGE_DRAGON_AZOLD = "Dragon_Azold.png";
        public const string IMAGE_DRAGON_ZEED = "Dragon_Zeed.png";
        public const string IMAGE_DRAGON_ETULA = "Dragon_Etula.png";
        #endregion

        #region "���ʉ��f�[�^�t�@�C����"
        public const string SOUND_FIREBALL = "FireBall.mp3";
        public const string SOUND_ICENEEDLE = "IceNeedle.mp3";
        public const string SOUND_ENEMY_ATTACK1 = "EnemyAttack1.mp3";
        public const string SOUND_SWORD_SLASH1 = "SwordSlash1.mp3";
        public const string SOUND_STRAIGHT_SMASH = "StraightSmash.mp3";
        public const string SOUND_FRESH_HEAL = "FreshHeal.mp3";
        public const string SOUND_CELESTIAL_NOVA = "CelestialNova.mp3";
        public const string SOUND_MAGIC_ATTACK = "MagicAttack.mp3";
        public const string SOUND_KINETIC_SMASH = "KineticSmash.mp3";
        public const string SOUND_ARCANE_DESTRUCTION = "KineticSmash.mp3";
        public const string SOUND_CRUSHING_BLOW = "CrushingBlow.mp3";
        public const string SOUND_SOUL_INFINITY = "Catastrophe.mp3";
        public const string SOUND_CATASTROPHE = "Catastrophe.mp3";
        public const string SOUND_OBORO_IMPACT = "Catastrophe.mp3";
        public const string SOUND_ABYSS_EYE = "WhiteOut.mp3";
        public const string SOUND_DARK_BLAST = "DarkBlast.mp3";
        public const string SOUND_DOOM_BLADE = "DarkBlast.mp3";
        public const string SOUND_PHANTASMAL_WIND = "HeatBoost.mp3";
        public const string SOUND_PARADOX_IMAGE = "RiseOfImage.mp3";
        public const string SOUND_PIERCING_FLAME = "FireBall.mp3";
        public const string SOUND_DEMONIC_IGNITE = "FireBall.mp3";
        public const string SOUND_VORTEX_FIELD = "DispelMagic.mp3";
        public const string SOUND_GLORY = "Glory.mp3";
        public const string SOUND_STATIC_BARRIER = "Glory.mp3";
        public const string SOUND_NOURISH_SENSE = "WordOfLife.mp3";
        public const string SOUND_LIFE_TAP = "LifeTap.mp3";
        public const string SOUND_SYUTYU_DANZETSU = "NothingOfNothingness.mp3";
        public const string SOUND_JUNKAN_SEIYAKU = "NothingOfNothingness.mp3";
        public const string SOUND_ORA_ORA_ORAAA = "Catastrophe.mp3";
        public const string SOUND_SHINZITSU_HAKAI = "Catastrophe.mp3";
        public const string SOUND_HYMN_CONTRACT = "Resurrection.mp3";
        public const string SOUND_ENDLESS_ANTHEM = "Resurrection.mp3";
        public const string SOUND_FLAME_STRIKE = "FlameStrike.mp3";
        public const string SOUND_SIGIL_OF_HOMURA = "FlameStrike.mp3";
        public const string SOUND_AUSTERITY_MATRIX = "OneImmunity.mp3";
        public const string SOUND_RED_DRAGON_WILL = "StraightSmash.mp3";
        public const string SOUND_BLUE_DRAGON_WILL = "StraightSmash.mp3";
        public const string SOUND_ECLIPSE_END = "BlackContract.mp3";
        public const string SOUND_SIN_FORTUNE = "BlackContract.mp3";
        public const string SOUND_BLACK_FLARE = "BlackContract.mp3";
        public const string SOUND_DEATH_DENY = "BlackContract.mp3";
        public const string SOUND_DEATH = "BlackContract.mp3";
        public const string SOUND_RISINGKNUCKLE = "RisingKnuckle.mp3";
        public const string SOUND_DAMNATION = "Damnation.mp3";
        public const string SOUND_CHOSEN_SACRIFY = "Damnation.mp3";
        public const string SOUND_ABSOLUTE_ZERO = "AbsoluteZero.mp3";
        public const string SOUND_LAVA_ANNIHILATION = "LavaAnnihilation.mp3";
        public const string SOUND_KOKUEN_BLUE_EXPLODE = "LavaAnnihilation.mp3";
        public const string SOUND_VOLCANICWAVE = "VolcanicWave.mp3";
        public const string SOUND_MEGID_BLAZE = "VolcanicWave.mp3";
        public const string SOUND_FROZENLANCE = "FrozenLance.mp3";
        public const string SOUND_SHARPNEL_NEEDLE = "FrozenLance.mp3";
        public const string SOUND_WHITEOUT = "Whiteout.mp3";
        public const string SOUND_WORD_OF_POWER = "WordOfPower.mp3";
        public const string SOUND_TIME_STOP = "TimeStop.mp3";
        public const string SOUND_WARP_GATE = "TimeStop.mp3";
        public const string SOUND_GENESIS = "Genesis.mp3";
        public const string SOUND_STANCE_OF_DOUBLE = "Genesis.mp3";
        public const string SOUND_ZETA_EXPLOSION = "LavaAnnihilation.mp3";

        public const string SOUND_GET_EPIC_ITEM = "GetEpicItem.mp3";
        public const string SOUND_GET_RARE_ITEM = "GetRareItem.mp3";

        public const string SOUND_LVUP_FELTUS = "LvupFeltus.mp3";

        // �������牺�̓T�E���h�t�@�C�����̂𒼐ڋL�q�������̂��i���o�����O
        public const string SOUND_1 = "AbsoluteZero.mp3";
        public const string SOUND_2 = "AbsorbWater.mp3";
        public const string SOUND_3 = "AeroBlade.mp3";
        public const string SOUND_4 = "AetherDrive.mp3";
        public const string SOUND_5 = "AntiStun.mp3";
        public const string SOUND_6 = "BlackContract.mp3";
        public const string SOUND_7 = "BloodyVengeance.mp3";
        public const string SOUND_8 = "BlueLightning.mp3";
        public const string SOUND_9 = "Catastrophe.mp3";
        public const string SOUND_10 = "CelestialNova.mp3";
        public const string SOUND_11 = "Cleansing.mp3";
        public const string SOUND_12 = "CrushingBlow.mp3";
        public const string SOUND_13 = "Damnation.mp3";
        public const string SOUND_14 = "DarkBlast.mp3";
        public const string SOUND_15 = "Deflection.mp3";
        public const string SOUND_16 = "DevouringPlague.mp3";
        public const string SOUND_17 = "DispelMagic.mp3";
        public const string SOUND_18 = "EnemyAttack1.mp3";
        public const string SOUND_19 = "EternalPresence.mp3";
        public const string SOUND_20 = "FireBall.mp3";
        public const string SOUND_21 = "FlameAura.mp3";
        public const string SOUND_22 = "FlameStrike.mp3";
        public const string SOUND_23 = "footstep.mp3";
        public const string SOUND_24 = "FreshHeal.mp3";
        public const string SOUND_25 = "FrozenLance.mp3";
        public const string SOUND_26 = "GaleWind.mp3";
        public const string SOUND_27 = "Genesis.mp3";
        public const string SOUND_28 = "GetEpicItem.mp3";
        public const string SOUND_29 = "GetRareItem.mp3";
        public const string SOUND_30 = "Glory.mp3";
        public const string SOUND_31 = "HeatBoost.mp3";
        public const string SOUND_32 = "HighEmotionality.mp3";
        public const string SOUND_33 = "Hit01.mp3";
        public const string SOUND_34 = "HolyShock.mp3";
        public const string SOUND_35 = "IceNeedle.mp3";
        public const string SOUND_36 = "ImmortalRave.mp3";
        public const string SOUND_37 = "InnerInspiration.mp3";
        public const string SOUND_38 = "KineticSmash.mp3";
        public const string SOUND_39 = "LavaAnnihilation.mp3";
        public const string SOUND_40 = "LifeTap.mp3";
        public const string SOUND_41 = "LvUp.mp3";
        public const string SOUND_42 = "LvupFeltus.mp3";
        public const string SOUND_43 = "MagicAttack.mp3";
        public const string SOUND_44 = "MirrorImage.mp3";
        public const string SOUND_45 = "NothingOfNothingness.mp3";
        public const string SOUND_46 = "OneImmunity.mp3";
        public const string SOUND_47 = "PainfulInsanity.mp3";
        public const string SOUND_48 = "PromisedKnowledge.mp3";
        public const string SOUND_49 = "Protection.mp3";
        public const string SOUND_50 = "PutiFireBall.mp3";
        public const string SOUND_51 = "RestInn.mp3";
        public const string SOUND_52 = "Resurrection.mp3";
        public const string SOUND_53 = "RiseOfImage.mp3";
        public const string SOUND_54 = "RisingKnuckle.mp3";
        public const string SOUND_55 = "SaintPower.mp3";
        public const string SOUND_56 = "ShadowPact.mp3";
        public const string SOUND_57 = "SpecialHit.mp3";
        public const string SOUND_58 = "StanceOfDeath.mp3";
        public const string SOUND_59 = "StanceOfFlow.mp3";
        public const string SOUND_60 = "StraightSmash.mp3";
        public const string SOUND_61 = "SwordSlash1.mp3";
        public const string SOUND_62 = "TimeStop.mp3";
        public const string SOUND_63 = "Tranquility.mp3";
        public const string SOUND_64 = "TruthVision.mp3";
        public const string SOUND_65 = "VoidExtraction.mp3";
        public const string SOUND_66 = "VolcanicWave.mp3";
        public const string SOUND_67 = "WallHit.mp3";
        public const string SOUND_68 = "WhiteOut.mp3";
        public const string SOUND_69 = "WordOfFortune.mp3";
        public const string SOUND_70 = "WordOfLife.mp3";
        public const string SOUND_71 = "WordOfPower.mp3";

        #endregion

        #region "�G�̖��O"
        #region "�_���W�����P�K"
        public const string ENEMY_KOUKAKU_WURM = "�b�k���[��";
        public const string ENEMY_HIYOWA_BEATLE = "�Ў�ȃr�[�g��";
        public const string ENEMY_GREEN_CHILD = "�O���[���E�`���C���h";
        public const string ENEMY_MANDRAGORA = "�}���h���S��";

        public const string ENEMY_SUN_FLOWER = "�T���E�t�����[";
        public const string ENEMY_RED_HOPPER = "���b�h�E�z�b�p�[";
        public const string ENEMY_EARTH_SPIDER = "�A�[�X�p�C�_�[";
        public const string ENEMY_ALRAUNE = "�A�����E�l";
        public const string ENEMY_POISON_MARY = "�|�C�Y���E�}���[";

        public const string ENEMY_SPEEDY_TAKA = "�r�q�ȑ�";
        public const string ENEMY_ZASSYOKU_RABBIT = "�G�H�E�T�M";
        public const string ENEMY_WONDER_SEED = "�����_�[�E�V�[�h";
        public const string ENEMY_FLANSIS_KNIGHT = "�t�����V�X�E�i�C�g";
        public const string ENEMY_SHOTGUN_HYUI = "�V���b�g�K���E�q���[�C";

        public const string ENEMY_BRILLIANT_BUTTERFLY = "�u�����A���g�E�o�^�t���C";
        public const string ENEMY_WAR_WOLF = "�ԘT";
        public const string ENEMY_BLOOD_MOSS = "�u���b�h�E���X";
        public const string ENEMY_MOSSGREEN_DADDY = "���X�O���[���E�_�f�B";

        public const string ENEMY_BOSS_KARAMITUKU_FLANSIS = "��K�̎��ҁF���݂��t�����V�X";

        public const string ENEMY_DRAGON_SOKUBAKU_BRIYARD = "��������҃u���C���[�h(The Restrainer)";
        #endregion
        #region "�_���W�����Q�K"

        public const string ENEMY_DAGGER_FISH = "�_�K�[�t�B�b�V��";
        public const string ENEMY_SIPPU_FLYING_FISH = "�����E�t���C���O�t�B�b�V��";
        public const string ENEMY_ORB_SHELLFISH = "�I�[�u�E�V�F���t�B�b�V��";
        public const string ENEMY_SPLASH_KURIONE = "�X�v���b�V���E�N���I�l";

        public const string ENEMY_ROLLING_MAGURO = "���[�����O�E�}�O��";
        public const string ENEMY_RANBOU_SEA_ARTINE = "���\�ȃV�[�E�A�[�`��";
        public const string ENEMY_BLUE_SEA_WASI = "�C�h";
        public const string ENEMY_GANGAME = "��T";
        public const string ENEMY_BIGMOUSE_JOE = "�r�b�O�}�E�X�E�W���[";

        public const string ENEMY_MOGURU_MANTA = "���[�O���E�}���^";
        public const string ENEMY_FLOATING_GOLD_FISH = "���V����S�[���h�t�B�b�V��";
        public const string ENEMY_GOEI_HERMIT_CLUB = "��q���E�n�[�~�b�g�N���u";
        public const string ENEMY_VANISHING_CORAL = "�o�j�b�V���O�E�R�[����";
        public const string ENEMY_CASSY_CANCER = "�L���V�[�E�U�E�L�����T�[";

        public const string ENEMY_BLACK_STARFISH = "�u���b�N�E�X�^�[�t�B�b�V��";
        public const string ENEMY_RAINBOW_ANEMONE = "���C���{�[�E�A�l���l";
        public const string ENEMY_EDGED_HIGH_SHARK = "�G�b�W�h�E�n�C�E�V���[�N";
        public const string ENEMY_EIGHT_EIGHT = "�G�C�g�E�G�C�g";

        public const string ENEMY_BRILLIANT_SEA_PRINCE = "�P����C�̉��q";
        public const string ENEMY_ORIGIN_STAR_CORAL_QUEEN = "�����E�X��̏���";
        public const string ENEMY_SHELL_SWORD_KNIGHT = "�V�F���E�U�E�\�[�h�i�C�g";
        public const string ENEMY_JELLY_EYE_BRIGHT_RED = "�W�F���[�A�C�E�M��";
        public const string ENEMY_JELLY_EYE_DEEP_BLUE = "�W�F���[�A�C�E����";
        public const string ENEMY_SEA_STAR_KNIGHT_AEGIRU = "�C���R�m�E�G�[�M��";
        public const string ENEMY_SEA_STAR_KNIGHT_AMARA = "�C���R�m�E�A�}��";
        public const string ENEMY_SEA_STAR_ORIGIN_KING = "�C�����̉�";

        public const string ENEMY_BOSS_LEVIATHAN = "��K�̎��ҁF��C�փ����B�A�T��";

        public const string ENEMY_DRAGON_TINKOU_DEEPSEA = "���~�����҃f�B�[�v�V�[";//(The Akashic)";
        #endregion
        #region "�_���W�����R�K"
        public const string ENEMY_TOSSIN_ORC = "�ːi�I�[�N";
        public const string ENEMY_SNOW_CAT = "�X�m�[�E�L���b�g";
        public const string ENEMY_WAR_MAMMOTH = "�E�H�[�E�}�����X";
        public const string ENEMY_WINGED_COLD_FAIRY = "�E�B���O�h�E�R�[���h�t�F�A���[";

        public const string ENEMY_BRUTAL_OGRE = "�u���[�^���E�I�[�K";
        public const string ENEMY_HYDRO_LIZARD = "�n�C�h���E���U�[�h";
        public const string ENEMY_PENGUIN_STAR = "�y���M���X�^�[";
        public const string ENEMY_SWORD_TOOTH_TIGER = "������";
        public const string ENEMY_FEROCIOUS_RAGE_BEAR = "�t�F���V�A�X�E���C�W�x�A";

        public const string ENEMY_WINTER_ORB = "�E�B���^�[�E�I�[��";
        public const string ENEMY_PATHFINDING_LIGHTNING_AZARASI = "�Ǐ]���闋�A�U���V";
        public const string ENEMY_INTELLIGENCE_ARGONIAN = "�m�I�ȃA���S�j�A��";
        public const string ENEMY_MAGIC_HYOU_RIFLE = "���@蹌��e";
        public const string ENEMY_PURE_BLIZZARD_CRYSTAL = "�s���A�E�u���U�[�h�E�N���X�^��";

        public const string ENEMY_PURPLE_EYE_WARE_WOLF = "���ځE�E�F�A�E���t";
        public const string ENEMY_FROST_HEART = "�t���X�g�E�n�[�g";
        public const string ENEMY_WIND_BREAKER = "�E�B���h�E�u���C�J�[";
        public const string ENEMY_TUNDRA_LONGHORN_DEER = "�c���h���E�����O�z�[���E�f�B�A";

        public const string ENEMY_BOSS_HOWLING_SEIZER = "�O�K�̎��ҁF����n�E�����O�E�V�[�U�[";

        public const string ENEMY_DRAGON_DESOLATOR_AZOLD = "���Ă��҃A�]���h";//(The Desolate)";
        #endregion
        #region "�_���W�����S�K"
        public const string ENEMY_GENAN_HUNTER = "���Ãn���^�[";
        public const string ENEMY_BEAST_MASTER = "�r�[�X�g�}�X�^�[";
        public const string ENEMY_ELDER_ASSASSIN = "�G���_�[�A�T�V��";
        public const string ENEMY_FALLEN_SEEKER = "�t�H�[�����E�V�[�J�[";

        public const string ENEMY_MASTER_LOAD = "�}�X�^�[���[�h";
        public const string ENEMY_EXECUTIONER = "�G�O�[�L���[�V���i�[";
        public const string ENEMY_DARK_MESSENGER = "�ł��ő�";
        public const string ENEMY_BLACKFIRE_MASTER_BLADE = "�����}�X�^�[�u���C�h";
        public const string ENEMY_SIN_THE_DARKELF = "�V���E�U�E�_�[�N�G���t";

        public const string ENEMY_SUN_STRIDER = "�T���E�X�g���C�_�[";
        public const string ENEMY_ARC_DEMON = "�A�[�N�f�[����";
        public const string ENEMY_BALANCE_IDLE = "�V�����i���";
        public const string ENEMY_GO_FLAME_SLASHER = "�ƁE�t���C���X���b�V���[";
        public const string ENEMY_DEVIL_CHILDREN = "�f�r���E�`���h����";
        
        public const string ENEMY_HOWLING_HORROR = "�n�E�����O�z���[";
        public const string ENEMY_PAIN_ANGEL = "�y�C���G���W�F��";
        public const string ENEMY_CHAOS_WARDEN = "�J�I�X�E���[�f��";
        public const string ENEMY_DOOM_BRINGER = "�h�D�[���u�����K�[";

        public const string ENEMY_BOSS_LEGIN_ARZE = "�l�K�̎��ҁF�ŉ����M�B���E�A�[�[";
        public const string ENEMY_BOSS_LEGIN_ARZE_1 = "�l�K�̎��ҁF�ŉ����M�B���E�A�[�[�yᏋC�z";
        public const string ENEMY_BOSS_LEGIN_ARZE_2 = "�l�K�̎��ҁF�ŉ����M�B���E�A�[�[�y�����z";
        public const string ENEMY_BOSS_LEGIN_ARZE_3 = "�l�K�̎��ҁF�ŉ����M�B���E�A�[�[�y�[���z";

        public const string ENEMY_DRAGON_IDEA_CAGE_ZEED = "�ٍl�����҃W�[�h";
        #endregion
        #region "�_���W�����T�K"
        public const string ENEMY_PHOENIX = "Phoenix";
        public const string ENEMY_NINE_TAIL = "Nine Tail";
        public const string ENEMY_JUDGEMENT = "Judgement";
        public const string ENEMY_EMERALD_DRAGON = "Emerald Dragon";

        public const string ENEMY_BOSS_BYSTANDER_EMPTINESS = "�x�@�z�@��";
        public const string ENEMY_DRAGON_ALAKH_VES_T_ETULA = "AlakhVes T Etula"; 
        
        #endregion
        #region "�^�����E"
        public const string ENEMY_LAST_RANA_AMILIA = "���i�E�A�~���A ";
        public const string ENEMY_LAST_OL_LANDIS = "�I���E�����f�B�X ";
        public const string ENEMY_LAST_SINIKIA_KAHLHANZ = "�V�j�L�A�E�J�[���n���c ";
        public const string ENEMY_LAST_VERZE_ARTIE = "���F���[�E�A�[�e�B ";
        public const string ENEMY_LAST_SIN_VERZE_ARTIE = "�y���߁z���F���[�E�A�[�e�B";
        #endregion
        #region "Duel���Z��"
        public const string DUEL_SIN_OSCURETE = "�V���E�I�X�L�����[�e"; // 60
        public const string DUEL_LADA_MYSTORUS = "���_�E�~�X�g�D���X"; // 58
        public const string DUEL_OHRYU_GENMA = "�I�E�����E�E�Q���}"; // 56
        public const string DUEL_VAN_HEHGUSTEL = "���@���E�w�[�O�X�e��"; // 54
        public const string DUEL_RVEL_ZELKIS = "���x���E�[���L�X"; // 52

        public const string DUEL_SHUVALTZ_FLORE = "�V�����@���c�F�E�t���[��"; // 50
        public const string DUEL_SUN_YU = "�T���E���E"; // 47
        public const string DUEL_CALMANS_OHN = "�J���}���Y�E�I�[��"; // 44
        public const string DUEL_ANNA_HAMILTON = "�A���i�E�n�~���g��"; // 41
        public const string DUEL_BILLY_RAKI = "�r���[�E���L"; // 38

        public const string DUEL_KILT_JORJU = "�L���g�E�W�����W��"; // 35
        public const string DUEL_PERMA_WARAMY = "�y���}�E�����~�B"; // 32
        public const string DUEL_SCOTY_ZALGE = "�X�R�[�e�B�E�U���Q"; // 29
        public const string DUEL_LENE_COLTOS = "���l�E�R���g�X"; // 26
        public const string DUEL_ADEL_BRIGANDY = "�A�f���E�u���K���f�B"; // 23

        public const string DUEL_SINIKIA_VEILHANTU = "�V�j�L�A�E���F�C���n���c"; // 20
        public const string DUEL_JEDA_ARUS = "�W�F�_�E�A���X"; // 16
        public const string DUEL_KARTIN_MAI = "�J�[�e�B���E�}�C"; // 13
        public const string DUEL_SELMOI_RO = "�Z�����C�E���E"; // 10
        public const string DUEL_MAGI_ZELKIS = "�}�[�M�E�[���L�X"; // 7
        public const string DUEL_EONE_FULNEA = "�G�I�l�E�t���l�A"; // 4
        #endregion
        public const string DUEL_DUMMY_SUBURI = "�_�~�[�f�U��N";

        // Duelist�B�̑���
        public const string EPIC_LADA_ACHROMATIC_ORB = "���_�E�A�N���}�e�B�b�N�E�I�[�u";
        public const string COMMON_SWORD_OF_RVEL = "���x���̑匕";
        public const string COMMON_ARMOR_OF_RVEL = "���x���̊Z";

        public const string COMMON_ZELKIS_SWORD = "�[���L�X�̌�";
        public const string COMMON_ZELKIS_ARMOR = "�[���L�X�̊Z";
        public const string COMMON_WHITE_ROD = "�z���C�g�E���b�h";
        public const string COMMON_BLUE_ROBE = "�����[�u";
        public const string COMMON_FROZEN_BALL = "�����̋�";
        public const string RARE_PURE_GREEN_WATER = "���N��"; // Duel�A�W�F�_�E�A���X�̎����������A�����v���C���[�ɂ�����\�ɂ���B
        public const string EPIC_DEVIL_EYE_ROD = "Rod of D-Eye";
        public const string COMMON_ZALGE_CLAW = "�U���Q�̒�";
        public const string EPIC_FAZIL_ORB_1 = "�t�@�[�W���̕��y�����z";
        public const string EPIC_FAZIL_ORB_2 = "�t�@�[�W���̕��y�n���z";

        public const string EPIC_SHUVALTZ_FLORE_SWORD = "��Ȃ镑����";
        public const string EPIC_SHUVALTZ_FLORE_SHIELD = "�΂Ȃ閳����";
        public const string EPIC_SHUVALTZ_FLORE_ARMOR = "���ւ̍�����";
        public const string EPIC_SHUVALTZ_FLORE_ACCESSORY1 = "�t�@�[�W���̕��y������z";
        public const string EPIC_SHUVALTZ_FLORE_ACCESSORY2 = "�t�@�[�W���̕��y�i�z�z";

        // ���j�K�����̃V�j�L�A�E�J�[���n���c
        public const string DUEL_SINIKIA_KAHLHANZ = "�V�j�L�A�E�J�[���n���c";
        public const string LEGENDARY_DARKMAGIC_DEVIL_EYE = "�����f�r���A�C";
        public const string LEGENDARY_DARKMAGIC_DEVIL_EYE_REPLICA = "�����f�r���A�C�i���v���J�j";
        public const string EPIC_YAMITUYUKUSA_MOON_ROBE= "�ŘI���̉~����";
        public const string LEGENDARY_ZVELDOSE_DEVIL_FIRE_RING = "Zveldose the Devil Fire Ring";
        public const string LEGENDARY_ANASTELISA_INNOCENT_FIRE_RING = "Anastelisa the Innocent Fire Ring";

        // �ŉ��w�z���O�����̃V�j�L�A�E�J�[���n���c
        public const string EPIC_DARKMAGIC_DEVIL_EYE_2 = "�����f�r���A�C(2ND-Ed)";
        public const string EPIC_YAMITUYUKUSA_MOON_ROBE_2 = "�ŘI�� �~���̈�";
        public const string LEGENDARY_ZVELDOSE_DEVIL_FIRE_RING_2 = "Zveldose the Devil-Fire Ring";
        public const string LEGENDARY_ANASTELISA_INNOCENT_FIRE_RING_2 = "Anastelisa the Innocent-Fire Ring";

        // �I���E�����f�B�X�A���Ԃɂ���O
        public const string DUEL_OL_LANDIS = "�I���E�����f�B�X";
        public const string LEGENDARY_GOD_FIRE_GLOVE = "���_�O���[�u";
        public const string POOR_GOD_FIRE_GLOVE_REPLICA = "���_�O���[�u�i���v���J�j";
        public const string COMMON_AURA_ARMOR = "�I�[���E�A�[�}�[";
        public const string EPIC_AURA_ARMOR_OMEGA = "�i���F�G�^�[�i���E�I�[���A�[�}�[";
        public const string COMMON_FATE_RING = "�t�F�C�g�E�����O";
        public const string EPIC_FATE_RING_OMEGA = "�i�J�F�G�^�[�i���E�t�F�C�g�����O";
        public const string COMMON_LOYAL_RING = "���C�����E�����O";
        public const string EPIC_LOYAL_RING_OMEGA = "�i���F�G�^�[�i���E���C���������O";

        // ���F���[�E�A�[�e�B�A���Ԃɂ���O
        public const string LEGENDARY_TAU_WHITE_SILVER_SWORD_0 = "[��] White Silver Sword0";
        public const string LEGENDARY_LAMUDA_BLACK_AERIAL_ARMOR_0 = "[��] Black Aerial Armor0";
        public const string LEGENDARY_EPSIRON_HEAVENLY_SKY_WING_0 = "[��] Heavenly Sky Wing0";

        // ���F���[�E�A�[�e�B���Ԃɂ��鎞
        public const string RARE_WHITE_SILVER_SWORD_REPLICA = "����̌�(���v���J)";
        public const string RARE_BLACK_AERIAL_ARMOR_REPLICA = "���^��̊Z(���v���J)";
        public const string RARE_HEAVENLY_SKY_WING_REPLICA = "�V��̗�(���v���J)";

        // �ŉ��w�z���O�����̃��F���[�E�A�[�e�B
        public const string EPIC_WHITE_SILVER_SWORD_REPLICA = "�y�сz ����̌�";
        public const string EPIC_BLACK_AERIAL_ARMOR_REPLICA = "�y�Ɂz ���^��̊Z";
        public const string EPIC_HEAVENLY_SKY_WING_REPLICA = "�y�Áz �V��̗�";

        // ���F���[�E�A�[�e�B�ŏI����
        public const string LEGENDARY_TAU_WHITE_SILVER_SWORD = "[��] White Silver Sword";
        public const string EPIC_COLORESS_ETERNAL_BREAKER = "�y�V���z�G�^�[�i���E�u���C�J�[";
        public const string LEGENDARY_LAMUDA_BLACK_AERIAL_ARMOR = "[��] Black Aerial Armor";
        public const string LEGENDARY_EPSIRON_HEAVENLY_SKY_WING = "[��] Heavenly Sky Wing";
        public const string LEGENDARY_SEFINE_HYMNUS_RING = "�y�i���z�Z�t�B�[�l�̘r��";

        // �_���W�����Q�K�i�ޏ��ŃK���c���i�i
        public const string POOR_PRACTICE_SWORD_ZERO = "���K�p�̌��y���z";
        public const string POOR_PRACTICE_SWORD_1 = "���K�p�̌��yLv1�z";
        public const string POOR_PRACTICE_SWORD_2 = "���K�p�̌��yLv2�z";
        public const string COMMON_PRACTICE_SWORD_3 = "���K�p�̌��yLv3�z";
        public const string COMMON_PRACTICE_SWORD_4 = "���K�p�̌��yLv4�z";
        public const string RARE_PRACTICE_SWORD_5 = "���K�p�̌��yLv5�z";
        public const string RARE_PRACTICE_SWORD_6 = "���K�p�̌��yLv6�z";
        public const string EPIC_PRACTICE_SWORD_7 = "���K�p�̌��yLv7�z";
        public const string LEGENDARY_FELTUS = "�_��  �t�F���g�D�[�V��";
        #endregion

        #region "���E�[���E��A�t�@�[�W���{�a��"
        public const string BACKGROUND_MORNING = "hometown.jpg";
        public const string BACKGROUND_EVENING = "hometown_evening.jpg";
        public const string BACKGROUND_NIGHT = "hometown2.jpg";
        public const string BACKGROUND_SECRETFIELD_OF_FAZIL = "SecretFieldOfFazil.jpg";
        public const string BACKGROUND_FIELD_OF_FIRSTPLACE = "Field1.jpg";
        public const string BACKGROUND_FAZIL_CASTLE = "FazilCastle.jpg";
        #endregion

        public const string OK_BUTTON_IMAGE = "OkButton.png";
        public const string OK_BUTTON_IMAGE_BLACK = "OkButton_Black.png";

        public const string DUNGEON_BACKGROUND = "background.png";

        public const string WE2_FILE = "TruthWorldEnvironment.xml";
        #endregion

        // BUFF�ǉ������t�@�C����
        public const string AFTER_REVIVE_HALF = "AfterReviveHalf";
        public const string FIRE_DAMAGE_2 = "FireDamage2";
        public const string BLACK_MAGIC = "BlackMagic";
        public const string CHAOS_DESPERATE = "ChaosDesperate";
        public const string ICHINARU_HOMURA = "IchinaruHomura";
        public const string ABYSS_FIRE = "AbyssFire";
        public const string LIGHT_AND_SHADOW = "LightAndShadow";
        public const string ETERNAL_DROPLET = "EternalDroplet";
        public const string AUSTERITY_MATRIX_OMEGA = "AusterityMatrixOmega";
        public const string VOICE_OF_ABYSS = "VoiceOfAbyss";
        public const string ABYSS_WILL = "AbyssWill";
        public const string THE_ABYSS_WALL = "TheAbyssWall";

        // ���M�B���A�[�[�}�i�R�X�g
        public const int COST_ICHINARU_HOMURA = 35000;
        public const int COST_ABYSS_FIRE = 32000;
        public const int COST_LIGHT_AND_SHADOW = 60000;
        public const int COST_ETERNAL_DROPLET = 48000;
        public const int COST_AUSTERITY_MATRIX_OMEGA = 95000;
        public const int COST_VOICE_OF_ABYSS = 87000;
        public const int COST_ABYSS_WILL = 25000;
        public const int COST_THE_ABYSS_WALL = 100000;

        // �ŏI��[����]���F���[�E�A�[�e�B
        public const string FINAL_ADEST_ESPELANTIE = "AdestEspelantie";
        public const string FINAL_INVISIBLE_HUNDRED_CUTTER = "InvisibleHundredCutter";
        public const string FINAL_ZERO_INNOCENT_SIN = "ZeroInnocentSin";
        public const string FINAL_LADARYNTE_CHAOTIC_SCHEMA = "LadarynteChaoticSchema";
        public const string FINAL_SEFINE_PAINFUL_HYMNUS = "SefinePainfulHymnus";
        public const string FINAL_PERFECT_FALSE_DIMENSION = "PerfectFalseDimension";

        // �ŏI�탉�C�t�J�E���g
        public const string LIFE_COUNT = "LifePoint";
        public const string BUFF_LIFE_COUNT = "����";
        public const string CHAOTIC_SCHEMA = "ChaoticSchema";
        public const string BUFF_CHAOTIC_SCHEMA = "�J�I�X���g";

        // �f�ޔ��ʕ���
        public const string DESCRIPTION_SELL_ONLY = "�y���p��p�i�z\r\n";
        public const string DESCRIPTION_EQUIP_MATERIAL = "�y����f�ށz\r\n";
        public const string DESCRIPTION_POTION_MATERIAL = "�y�|�[�V�����f�ށz\r\n";
        public const string DESCRIPTION_FOOD_MATERIAL = "�y�H�ށz\r\n";

        public const string MUGEN_LOOP = "�X�W�R�Q�U";

        public const string VINSGALDE = "���B���X�K���f";
    }
}