using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public static class TruthActionCommand
    {

        public enum TimingType
        {
            Sorcery, // 行動時のみ発動可能。加えてインスタント値を消費する。
            Normal, // 行動時のみ発動可能。インスタント値は消費しない。
            Instant, // 任意のタイミングで発動可能。インスタント値を消費して、インスタントで発動可能
        }

        public enum Attribute
        {
            None, // 特殊武器やモンスター魔法として用いる。
            Spell,
            Skill,
            Archetype,
        }

        public enum MagicType
        {
            None, // 特殊武器やモンスター魔法として用いる。
            Light,
            Shadow,
            Fire,
            Ice,
            Force,
            Will,
            Light_Shadow,
            Light_Fire,
            Light_Ice,
            Light_Force,
            Light_Will,
            Shadow_Fire,
            Shadow_Ice,
            Shadow_Force,
            Shadow_Will,
            Fire_Ice,
            Fire_Force,
            Fire_Will,
            Ice_Force,
            Ice_Will,
            Force_Will,
        }

        public enum SkillType
        {
            None, // 特殊武器やモンスター魔法として用いる。
            Active,
            Passive,
            Soft,
            Hard,
            Truth,
            Void,
            Active_Passive,
            Active_Soft,
            Active_Hard,
            Active_Truth,
            Active_Void,
            Passive_Soft,
            Passive_Hard,
            Passive_Truth,
            Passive_Void,
            Soft_Hard,
            Soft_Truth,
            Soft_Void,
            Hard_Truth,
            Hard_Void,
            Truth_Void,
        }

        public enum TargetType
        {
            NoTarget, // 対象を取らない。
            Ally, // 味方１体
            Enemy, // 敵１体
            AllyOrEnemy, // 味方１対か敵１体
            Own, // 自分自身
            AllyGroup, // 味方全員
            EnemyGroup, // 敵全員
            AllMember, // 敵味方全員
            InstantTarget, // インスタント行動を対象
        }

        public enum IgnoreType
        {
            None,
            IgnorePhysicalDefense,
            IgnoreMagicDefense,
            IgnoreAllDefense,
        }

        public enum DispelType
        {
            None,
            Dispel,
            EclipseEnd,
            AusterityMatrix,
            FutureVision,
            SpecialDispel,
        }

        public enum BuffType
        {
            None,
            Up,
            Down,
            TurnUp,
            TurnDown,
        }

        //public static string CommandName = String.Empty; // 名前（コマンド文字列そのもの）
        //public static string CommandName_JA = String.Empty; // 名前（日本語)

        //public static int Cost = 0; // 消費コスト
        //public static TimingType timing = TimingType.Instant; // インスタント or ソーサリーなどを特定
        //public static Attribute attribute = Attribute.None; // 魔法 or スキル などを特定
        //public static MagicType magicType = MagicType.None; // 魔法タイプを特定
        //public static SkillType skillType = SkillType.None; // スキルタイプを特定
        //public static TargetType targetType = TargetType.NoTarget; // ターゲットのタイプを特定
        ////public static bool CantBeCountered = false; // カウンターを無視できるかどうかを示す
        //public static bool CantBePrevented = false; // ダメージ軽減を無視できるかどうかを示す
        //public static IgnoreType ignoreType = IgnoreType.None; // 防御率を無視するダメージかどうかを示す
        //public static DispelType dispelType = DispelType.None; // ディスペル系統かどうかを示す
        //// public static bool IsDamage = false; // ダメージ系かどうかを示す
        //public static bool IsHeal = false; // ヒール系がどうかを示す
        //public static bool IsCounter = false; // カウンター系かどうかを示す

        public static bool[] GetAvailableActionList(MainCharacter currentPlayer)
        {
            bool[] ssAvailable = new bool[Database.TOTAL_COMMAND_NUM];

            // 基本
            ssAvailable[0] = true;
            ssAvailable[1] = true;
            ssAvailable[2] = true;
            ssAvailable[3] = true;
            ssAvailable[4] = true;
            ssAvailable[5] = true;
            ssAvailable[6] = true;
            ssAvailable[7] = true;

            // 聖
            ssAvailable[8] = currentPlayer.FreshHeal;
            ssAvailable[9] = currentPlayer.Protection;
            ssAvailable[10] = currentPlayer.HolyShock;
            ssAvailable[11] = currentPlayer.SaintPower;
            ssAvailable[12] = currentPlayer.Glory;
            ssAvailable[13] = currentPlayer.Resurrection;
            ssAvailable[14] = currentPlayer.CelestialNova;

            // 闇
            ssAvailable[15] = currentPlayer.DarkBlast;
            ssAvailable[16] = currentPlayer.ShadowPact;
            ssAvailable[17] = currentPlayer.LifeTap;
            ssAvailable[18] = currentPlayer.BlackContract;
            ssAvailable[19] = currentPlayer.DevouringPlague;
            ssAvailable[20] = currentPlayer.BloodyVengeance;
            ssAvailable[21] = currentPlayer.Damnation;

            // 火
            ssAvailable[22] = currentPlayer.FireBall;
            ssAvailable[23] = currentPlayer.FlameAura;
            ssAvailable[24] = currentPlayer.HeatBoost;
            ssAvailable[25] = currentPlayer.FlameStrike;
            ssAvailable[26] = currentPlayer.VolcanicWave;
            ssAvailable[27] = currentPlayer.ImmortalRave;
            ssAvailable[28] = currentPlayer.LavaAnnihilation;

            // 水
            ssAvailable[29] = currentPlayer.IceNeedle;
            ssAvailable[30] = currentPlayer.AbsorbWater;
            ssAvailable[31] = currentPlayer.Cleansing;
            ssAvailable[32] = currentPlayer.FrozenLance;
            ssAvailable[33] = currentPlayer.MirrorImage;
            ssAvailable[34] = currentPlayer.PromisedKnowledge;
            ssAvailable[35] = currentPlayer.AbsoluteZero;

            // 理
            ssAvailable[36] = currentPlayer.WordOfPower;
            ssAvailable[37] = currentPlayer.GaleWind;
            ssAvailable[38] = currentPlayer.WordOfLife;
            ssAvailable[39] = currentPlayer.WordOfFortune;
            ssAvailable[40] = currentPlayer.AetherDrive;
            ssAvailable[41] = currentPlayer.Genesis;
            ssAvailable[42] = currentPlayer.EternalPresence;

            // 空
            ssAvailable[43] = currentPlayer.DispelMagic;
            ssAvailable[44] = currentPlayer.RiseOfImage;
            ssAvailable[45] = currentPlayer.Deflection;
            ssAvailable[46] = currentPlayer.Tranquility;
            ssAvailable[47] = currentPlayer.OneImmunity;
            ssAvailable[48] = currentPlayer.WhiteOut;
            ssAvailable[49] = currentPlayer.TimeStop;

            // 動
            ssAvailable[50] = currentPlayer.StraightSmash;
            ssAvailable[51] = currentPlayer.DoubleSlash;
            ssAvailable[52] = currentPlayer.CrushingBlow;
            ssAvailable[53] = currentPlayer.SoulInfinity;

            // 静
            ssAvailable[54] = currentPlayer.CounterAttack;
            ssAvailable[55] = currentPlayer.PurePurification;
            ssAvailable[56] = currentPlayer.AntiStun;
            ssAvailable[57] = currentPlayer.StanceOfDeath;

            // 柔
            ssAvailable[58] = currentPlayer.StanceOfFlow;
            ssAvailable[59] = currentPlayer.EnigmaSence;
            ssAvailable[60] = currentPlayer.SilentRush;
            ssAvailable[61] = currentPlayer.OboroImpact;

            // 剛
            ssAvailable[62] = currentPlayer.StanceOfStanding;
            ssAvailable[63] = currentPlayer.InnerInspiration;
            ssAvailable[64] = currentPlayer.KineticSmash;
            ssAvailable[65] = currentPlayer.Catastrophe;

            // 心眼
            ssAvailable[66] = currentPlayer.TruthVision;
            ssAvailable[67] = currentPlayer.HighEmotionality;
            ssAvailable[68] = currentPlayer.StanceOfEyes;
            ssAvailable[69] = currentPlayer.PainfulInsanity;

            // 無心
            ssAvailable[70] = currentPlayer.Negate;
            ssAvailable[71] = currentPlayer.VoidExtraction;
            ssAvailable[72] = currentPlayer.CarnageRush;
            ssAvailable[73] = currentPlayer.NothingOfNothingness;

            // 聖＋闇（以降複合魔法）
            ssAvailable[74] = currentPlayer.PsychicTrance;
            ssAvailable[75] = currentPlayer.BlindJustice;
            ssAvailable[76] = currentPlayer.TranscendentWish;
            ssAvailable[77] = currentPlayer.FlashBlaze;
            ssAvailable[78] = currentPlayer.LightDetonator;
            ssAvailable[79] = currentPlayer.AscendantMeteor;
            ssAvailable[80] = currentPlayer.SkyShield;
            ssAvailable[81] = currentPlayer.SacredHeal;
            ssAvailable[82] = currentPlayer.EverDroplet;
            ssAvailable[83] = currentPlayer.HolyBreaker;
            ssAvailable[84] = currentPlayer.ExaltedField;
            ssAvailable[85] = currentPlayer.HymnContract;
            ssAvailable[86] = currentPlayer.StarLightning;
            ssAvailable[87] = currentPlayer.AngelBreath;
            ssAvailable[88] = currentPlayer.EndlessAnthem;
            ssAvailable[89] = currentPlayer.BlackFire;
            ssAvailable[90] = currentPlayer.BlazingField;
            ssAvailable[91] = currentPlayer.DemonicIgnite;
            ssAvailable[92] = currentPlayer.BlueBullet;
            ssAvailable[93] = currentPlayer.DeepMirror;
            ssAvailable[94] = currentPlayer.DeathDeny;
            ssAvailable[95] = currentPlayer.WordOfMalice;
            ssAvailable[96] = currentPlayer.AbyssEye;
            ssAvailable[97] = currentPlayer.SinFortune;
            ssAvailable[98] = currentPlayer.DarkenField;
            ssAvailable[99] = currentPlayer.DoomBlade;
            ssAvailable[100] = currentPlayer.EclipseEnd;
            ssAvailable[101] = currentPlayer.FrozenAura;
            ssAvailable[102] = currentPlayer.ChillBurn;
            ssAvailable[103] = currentPlayer.ZetaExplosion;
            ssAvailable[104] = currentPlayer.EnrageBlast;
            ssAvailable[105] = currentPlayer.PiercingFlame;
            ssAvailable[106] = currentPlayer.SigilOfHomura;
            ssAvailable[107] = currentPlayer.Immolate;
            ssAvailable[108] = currentPlayer.PhantasmalWind;
            ssAvailable[109] = currentPlayer.RedDragonWill;
            ssAvailable[110] = currentPlayer.WordOfAttitude;
            ssAvailable[111] = currentPlayer.StaticBarrier;
            ssAvailable[112] = currentPlayer.AusterityMatrix;
            ssAvailable[113] = currentPlayer.VanishWave;
            ssAvailable[114] = currentPlayer.VortexField;
            ssAvailable[115] = currentPlayer.BlueDragonWill;
            ssAvailable[116] = currentPlayer.SeventhMagic;
            ssAvailable[117] = currentPlayer.ParadoxImage;
            ssAvailable[118] = currentPlayer.WarpGate;
            // 動＋静（以降複合スキル）
            ssAvailable[119] = currentPlayer.NeutralSmash;
            ssAvailable[120] = currentPlayer.StanceOfDouble;
            ssAvailable[121] = currentPlayer.SwiftStep;
            ssAvailable[122] = currentPlayer.VigorSense;
            ssAvailable[123] = currentPlayer.CircleSlash;
            ssAvailable[124] = currentPlayer.RisingAura;
            ssAvailable[125] = currentPlayer.RumbleShout;
            ssAvailable[126] = currentPlayer.OnslaughtHit;
            ssAvailable[127] = currentPlayer.SmoothingMove;
            ssAvailable[128] = currentPlayer.AscensionAura;
            ssAvailable[129] = currentPlayer.FutureVision;
            ssAvailable[130] = currentPlayer.UnknownShock;
            ssAvailable[131] = currentPlayer.ReflexSpirit;
            ssAvailable[132] = currentPlayer.FatalBlow;
            ssAvailable[133] = currentPlayer.SharpGlare;
            ssAvailable[134] = currentPlayer.ConcussiveHit;
            ssAvailable[135] = currentPlayer.TrustSilence;
            ssAvailable[136] = currentPlayer.MindKilling;
            ssAvailable[137] = currentPlayer.SurpriseAttack;
            ssAvailable[138] = currentPlayer.StanceOfMystic;
            ssAvailable[139] = currentPlayer.PsychicWave;
            ssAvailable[140] = currentPlayer.NourishSense;
            ssAvailable[141] = currentPlayer.Recover;
            ssAvailable[142] = currentPlayer.ImpulseHit;
            ssAvailable[143] = currentPlayer.ViolentSlash;
            ssAvailable[144] = currentPlayer.ONEAuthority;
            ssAvailable[145] = currentPlayer.OuterInspiration;
            ssAvailable[146] = currentPlayer.HardestParry;
            ssAvailable[147] = currentPlayer.StanceOfSuddenness;
            ssAvailable[148] = currentPlayer.SoulExecution;

            if (currentPlayer.Name == Database.EIN_WOLENCE) ssAvailable[149] = currentPlayer.Syutyu_Danzetsu;
            if (currentPlayer.Name == Database.RANA_AMILIA) ssAvailable[149] = currentPlayer.Junkan_Seiyaku;
            if (currentPlayer.Name == Database.OL_LANDIS) ssAvailable[149] = currentPlayer.Ora_Ora_Oraaa;
            if (currentPlayer.Name == Database.VERZE_ARTIE) ssAvailable[149] = currentPlayer.Shinzitsu_Hakai;

            return ssAvailable;
        }

        public static string[] GetActionList(MainCharacter player)
        {
            string[] ssName = new string[Database.TOTAL_COMMAND_NUM];

            // 基本
            ssName[0] = Database.ATTACK_EN;
            ssName[1] = Database.DEFENSE_EN;
            ssName[2] = Database.STAY_EN;
            ssName[3] = Database.WEAPON_SPECIAL_EN;
            ssName[4] = Database.WEAPON_SPECIAL_LEFT_EN;
            ssName[5] = Database.TAMERU_EN;
            ssName[6] = Database.ACCESSORY_SPECIAL_EN;
            ssName[7] = Database.ACCESSORY_SPECIAL2_EN;

            // 聖
            ssName[8] = Database.FRESH_HEAL;
            ssName[9] = Database.PROTECTION;
            ssName[10] = Database.HOLY_SHOCK;
            ssName[11] = Database.SAINT_POWER;
            ssName[12] = Database.GLORY;
            ssName[13] = Database.RESURRECTION;
            ssName[14] = Database.CELESTIAL_NOVA;

            // 闇
            ssName[15] = Database.DARK_BLAST;
            ssName[16] = Database.SHADOW_PACT;
            ssName[17] = Database.LIFE_TAP;
            ssName[18] = Database.BLACK_CONTRACT;
            ssName[19] = Database.DEVOURING_PLAGUE;
            ssName[20] = Database.BLOODY_VENGEANCE;
            ssName[21] = Database.DAMNATION;

            // 火
            ssName[22] = Database.FIRE_BALL;
            ssName[23] = Database.FLAME_AURA;
            ssName[24] = Database.HEAT_BOOST;
            ssName[25] = Database.FLAME_STRIKE;
            ssName[26] = Database.VOLCANIC_WAVE;
            ssName[27] = Database.IMMORTAL_RAVE;
            ssName[28] = Database.LAVA_ANNIHILATION;

            // 水
            ssName[29] = Database.ICE_NEEDLE;
            ssName[30] = Database.ABSORB_WATER;
            ssName[31] = Database.CLEANSING;
            ssName[32] = Database.FROZEN_LANCE;
            ssName[33] = Database.MIRROR_IMAGE;
            ssName[34] = Database.PROMISED_KNOWLEDGE;
            ssName[35] = Database.ABSOLUTE_ZERO;

            // 理
            ssName[36] = Database.WORD_OF_POWER;
            ssName[37] = Database.GALE_WIND;
            ssName[38] = Database.WORD_OF_LIFE;
            ssName[39] = Database.WORD_OF_FORTUNE;
            ssName[40] = Database.AETHER_DRIVE;
            ssName[41] = Database.GENESIS;
            ssName[42] = Database.ETERNAL_PRESENCE;

            // 空
            ssName[43] = Database.DISPEL_MAGIC;
            ssName[44] = Database.RISE_OF_IMAGE;
            ssName[45] = Database.DEFLECTION;
            ssName[46] = Database.TRANQUILITY;
            ssName[47] = Database.ONE_IMMUNITY;
            ssName[48] = Database.WHITE_OUT;
            ssName[49] = Database.TIME_STOP;

            // 動
            ssName[50] = Database.STRAIGHT_SMASH;
            ssName[51] = Database.DOUBLE_SLASH;
            ssName[52] = Database.CRUSHING_BLOW;
            ssName[53] = Database.SOUL_INFINITY;

            // 静
            ssName[54] = Database.COUNTER_ATTACK;
            ssName[55] = Database.PURE_PURIFICATION;
            ssName[56] = Database.ANTI_STUN;
            ssName[57] = Database.STANCE_OF_DEATH;

            // 柔
            ssName[58] = Database.STANCE_OF_FLOW;
            ssName[59] = Database.ENIGMA_SENSE;
            ssName[60] = Database.SILENT_RUSH;
            ssName[61] = Database.OBORO_IMPACT;

            // 剛
            ssName[62] = Database.STANCE_OF_STANDING;
            ssName[63] = Database.INNER_INSPIRATION;
            ssName[64] = Database.KINETIC_SMASH;
            ssName[65] = Database.CATASTROPHE;

            // 心眼
            ssName[66] = Database.TRUTH_VISION;
            ssName[67] = Database.HIGH_EMOTIONALITY;
            ssName[68] = Database.STANCE_OF_EYES;
            ssName[69] = Database.PAINFUL_INSANITY;

            // 無心
            ssName[70] = Database.NEGATE;
            ssName[71] = Database.VOID_EXTRACTION;
            ssName[72] = Database.CARNAGE_RUSH;
            ssName[73] = Database.NOTHING_OF_NOTHINGNESS;

            // 聖＋闇（以降複合魔法）
            ssName[74] = Database.PSYCHIC_TRANCE;
            ssName[75] = Database.BLIND_JUSTICE;
            ssName[76] = Database.TRANSCENDENT_WISH;
            ssName[77] = Database.FLASH_BLAZE;
            ssName[78] = Database.LIGHT_DETONATOR;
            ssName[79] = Database.ASCENDANT_METEOR;
            ssName[80] = Database.SKY_SHIELD;
            ssName[81] = Database.SACRED_HEAL;
            ssName[82] = Database.EVER_DROPLET;
            ssName[83] = Database.HOLY_BREAKER;
            ssName[84] = Database.EXALTED_FIELD;
            ssName[85] = Database.HYMN_CONTRACT;
            ssName[86] = Database.STAR_LIGHTNING;
            ssName[87] = Database.ANGEL_BREATH;
            ssName[88] = Database.ENDLESS_ANTHEM;
            ssName[89] = Database.BLACK_FIRE;
            ssName[90] = Database.BLAZING_FIELD;
            ssName[91] = Database.DEMONIC_IGNITE;
            ssName[92] = Database.BLUE_BULLET;
            ssName[93] = Database.DEEP_MIRROR;
            ssName[94] = Database.DEATH_DENY;
            ssName[95] = Database.WORD_OF_MALICE;
            ssName[96] = Database.ABYSS_EYE;
            ssName[97] = Database.SIN_FORTUNE;
            ssName[98] = Database.DARKEN_FIELD;
            ssName[99] = Database.DOOM_BLADE;
            ssName[100] = Database.ECLIPSE_END;
            ssName[101] = Database.FROZEN_AURA;
            ssName[102] = Database.CHILL_BURN;
            ssName[103] = Database.ZETA_EXPLOSION;
            ssName[104] = Database.ENRAGE_BLAST;
            ssName[105] = Database.PIERCING_FLAME;
            ssName[106] = Database.SIGIL_OF_HOMURA;
            ssName[107] = Database.IMMOLATE;
            ssName[108] = Database.PHANTASMAL_WIND;
            ssName[109] = Database.RED_DRAGON_WILL;
            ssName[110] = Database.WORD_OF_ATTITUDE;
            ssName[111] = Database.STATIC_BARRIER;
            ssName[112] = Database.AUSTERITY_MATRIX;
            ssName[113] = Database.VANISH_WAVE;
            ssName[114] = Database.VORTEX_FIELD;
            ssName[115] = Database.BLUE_DRAGON_WILL;
            ssName[116] = Database.SEVENTH_MAGIC;
            ssName[117] = Database.PARADOX_IMAGE;
            ssName[118] = Database.WARP_GATE;
            // 動＋静（以降複合スキル）
            ssName[119] = Database.NEUTRAL_SMASH;
            ssName[120] = Database.STANCE_OF_DOUBLE;
            ssName[121] = Database.SWIFT_STEP;
            ssName[122] = Database.VIGOR_SENSE;
            ssName[123] = Database.CIRCLE_SLASH;
            ssName[124] = Database.RISING_AURA;
            ssName[125] = Database.RUMBLE_SHOUT;
            ssName[126] = Database.ONSLAUGHT_HIT;
            ssName[127] = Database.SMOOTHING_MOVE;
            ssName[128] = Database.ASCENSION_AURA;
            ssName[129] = Database.FUTURE_VISION;
            ssName[130] = Database.UNKNOWN_SHOCK;
            ssName[131] = Database.REFLEX_SPIRIT;
            ssName[132] = Database.FATAL_BLOW;
            ssName[133] = Database.SHARP_GLARE;
            ssName[134] = Database.CONCUSSIVE_HIT;
            ssName[135] = Database.TRUST_SILENCE;
            ssName[136] = Database.MIND_KILLING;
            ssName[137] = Database.SURPRISE_ATTACK;
            ssName[138] = Database.STANCE_OF_MYSTIC;
            ssName[139] = Database.PSYCHIC_WAVE;
            ssName[140] = Database.NOURISH_SENSE;
            ssName[141] = Database.RECOVER;
            ssName[142] = Database.IMPULSE_HIT;
            ssName[143] = Database.VIOLENT_SLASH;
            ssName[144] = Database.ONE_AUTHORITY;
            ssName[145] = Database.OUTER_INSPIRATION;
            ssName[146] = Database.HARDEST_PARRY;
            ssName[147] = Database.STANCE_OF_SUDDENNESS;
            ssName[148] = Database.SOUL_EXECUTION;

            // 元核
            if (player.Name == Database.EIN_WOLENCE) ssName[149] = Database.ARCHETYPE_EIN;
            if (player.Name == Database.RANA_AMILIA) ssName[149] = Database.ARCHETYPE_RANA;
            if (player.Name == Database.VERZE_ARTIE) ssName[149] = Database.ARCHETYPE_VERZE;
            if (player.Name == Database.OL_LANDIS) ssName[149] = Database.ARCHETYPE_OL;

            return ssName;
        }

        public static string[] GetSpellList()
        {
            string[] ssName = new string[Database.TOTAL_SPELL_NUM];
            int counter = 0;

            // 聖
            ssName[counter] = Database.FRESH_HEAL; counter++;
            ssName[counter] = Database.PROTECTION; counter++;
            ssName[counter] = Database.HOLY_SHOCK; counter++;
            ssName[counter] = Database.SAINT_POWER; counter++;
            ssName[counter] = Database.GLORY; counter++;
            ssName[counter] = Database.RESURRECTION; counter++;
            ssName[counter] = Database.CELESTIAL_NOVA; counter++;

            // 闇
            ssName[counter] = Database.DARK_BLAST; counter++;
            ssName[counter] = Database.SHADOW_PACT; counter++;
            ssName[counter] = Database.LIFE_TAP; counter++;
            ssName[counter] = Database.BLACK_CONTRACT; counter++;
            ssName[counter] = Database.DEVOURING_PLAGUE; counter++;
            ssName[counter] = Database.BLOODY_VENGEANCE; counter++;
            ssName[counter] = Database.DAMNATION; counter++;

            // 火
            ssName[counter] = Database.FIRE_BALL; counter++;
            ssName[counter] = Database.FLAME_AURA; counter++;
            ssName[counter] = Database.HEAT_BOOST; counter++;
            ssName[counter] = Database.FLAME_STRIKE; counter++;
            ssName[counter] = Database.VOLCANIC_WAVE; counter++;
            ssName[counter] = Database.IMMORTAL_RAVE; counter++;
            ssName[counter] = Database.LAVA_ANNIHILATION; counter++;

            // 水
            ssName[counter] = Database.ICE_NEEDLE; counter++;
            ssName[counter] = Database.ABSORB_WATER; counter++;
            ssName[counter] = Database.CLEANSING; counter++;
            ssName[counter] = Database.FROZEN_LANCE; counter++;
            ssName[counter] = Database.MIRROR_IMAGE; counter++;
            ssName[counter] = Database.PROMISED_KNOWLEDGE; counter++;
            ssName[counter] = Database.ABSOLUTE_ZERO; counter++;

            // 理
            ssName[counter] = Database.WORD_OF_POWER; counter++;
            ssName[counter] = Database.GALE_WIND; counter++;
            ssName[counter] = Database.WORD_OF_LIFE; counter++;
            ssName[counter] = Database.WORD_OF_FORTUNE; counter++;
            ssName[counter] = Database.AETHER_DRIVE; counter++;
            ssName[counter] = Database.GENESIS; counter++;
            ssName[counter] = Database.ETERNAL_PRESENCE; counter++;

            // 空
            ssName[counter] = Database.DISPEL_MAGIC; counter++;
            ssName[counter] = Database.RISE_OF_IMAGE; counter++;
            ssName[counter] = Database.DEFLECTION; counter++;
            ssName[counter] = Database.TRANQUILITY; counter++;
            ssName[counter] = Database.ONE_IMMUNITY; counter++;
            ssName[counter] = Database.WHITE_OUT; counter++;
            ssName[counter] = Database.TIME_STOP; counter++;

            // 聖＋闇（以降複合魔法）
            ssName[counter] = Database.PSYCHIC_TRANCE; counter++;
            ssName[counter] = Database.BLIND_JUSTICE; counter++;
            ssName[counter] = Database.TRANSCENDENT_WISH; counter++;
            ssName[counter] = Database.FLASH_BLAZE; counter++;
            ssName[counter] = Database.LIGHT_DETONATOR; counter++;
            ssName[counter] = Database.ASCENDANT_METEOR; counter++;
            ssName[counter] = Database.SKY_SHIELD; counter++;
            ssName[counter] = Database.SACRED_HEAL; counter++;
            ssName[counter] = Database.EVER_DROPLET; counter++;
            ssName[counter] = Database.HOLY_BREAKER; counter++;
            ssName[counter] = Database.EXALTED_FIELD; counter++;
            ssName[counter] = Database.HYMN_CONTRACT; counter++;
            ssName[counter] = Database.STAR_LIGHTNING; counter++;
            ssName[counter] = Database.ANGEL_BREATH; counter++;
            ssName[counter] = Database.ENDLESS_ANTHEM; counter++;
            ssName[counter] = Database.BLACK_FIRE; counter++;
            ssName[counter] = Database.BLAZING_FIELD; counter++;
            ssName[counter] = Database.DEMONIC_IGNITE; counter++;
            ssName[counter] = Database.BLUE_BULLET; counter++;
            ssName[counter] = Database.DEEP_MIRROR; counter++;
            ssName[counter] = Database.DEATH_DENY; counter++;
            ssName[counter] = Database.WORD_OF_MALICE; counter++;
            ssName[counter] = Database.ABYSS_EYE; counter++;
            ssName[counter] = Database.SIN_FORTUNE; counter++;
            ssName[counter] = Database.DARKEN_FIELD; counter++;
            ssName[counter] = Database.DOOM_BLADE; counter++;
            ssName[counter] = Database.ECLIPSE_END; counter++;
            ssName[counter] = Database.FROZEN_AURA; counter++;
            ssName[counter] = Database.CHILL_BURN; counter++;
            ssName[counter] = Database.ZETA_EXPLOSION; counter++;
            ssName[counter] = Database.ENRAGE_BLAST; counter++;
            ssName[counter] = Database.PIERCING_FLAME; counter++;
            ssName[counter] = Database.SIGIL_OF_HOMURA; counter++;
            ssName[counter] = Database.IMMOLATE; counter++;
            ssName[counter] = Database.PHANTASMAL_WIND; counter++;
            ssName[counter] = Database.RED_DRAGON_WILL; counter++;
            ssName[counter] = Database.WORD_OF_ATTITUDE; counter++;
            ssName[counter] = Database.STATIC_BARRIER; counter++;
            ssName[counter] = Database.AUSTERITY_MATRIX; counter++;
            ssName[counter] = Database.VANISH_WAVE; counter++;
            ssName[counter] = Database.VORTEX_FIELD; counter++;
            ssName[counter] = Database.BLUE_DRAGON_WILL; counter++;
            ssName[counter] = Database.SEVENTH_MAGIC; counter++;
            ssName[counter] = Database.PARADOX_IMAGE; counter++;
            ssName[counter] = Database.WARP_GATE; counter++;

            return ssName;
        }

        public static string[] GetSkillList()
        {
            string[] ssName = new string[Database.TOTAL_SKILL_NUM];
            int counter = 0;

            ssName[counter] = Database.STRAIGHT_SMASH; counter++;
            ssName[counter] = Database.DOUBLE_SLASH; counter++;
            ssName[counter] = Database.CRUSHING_BLOW; counter++;
            ssName[counter] = Database.SOUL_INFINITY; counter++;

            ssName[counter] = Database.COUNTER_ATTACK; counter++;
            ssName[counter] = Database.PURE_PURIFICATION; counter++;
            ssName[counter] = Database.ANTI_STUN; counter++;
            ssName[counter] = Database.STANCE_OF_DEATH; counter++;

            ssName[counter] = Database.STANCE_OF_FLOW; counter++;
            ssName[counter] = Database.ENIGMA_SENSE; counter++;
            ssName[counter] = Database.SILENT_RUSH; counter++;
            ssName[counter] = Database.OBORO_IMPACT; counter++;

            ssName[counter] = Database.STANCE_OF_STANDING; counter++;
            ssName[counter] = Database.INNER_INSPIRATION; counter++;
            ssName[counter] = Database.KINETIC_SMASH; counter++;
            ssName[counter] = Database.CATASTROPHE; counter++;

            ssName[counter] = Database.TRUTH_VISION; counter++;
            ssName[counter] = Database.HIGH_EMOTIONALITY; counter++;
            ssName[counter] = Database.STANCE_OF_EYES; counter++;
            ssName[counter] = Database.PAINFUL_INSANITY; counter++;

            ssName[counter] = Database.NEGATE; counter++;
            ssName[counter] = Database.VOID_EXTRACTION; counter++;
            ssName[counter] = Database.CARNAGE_RUSH; counter++;
            ssName[counter] = Database.NOTHING_OF_NOTHINGNESS; counter++;

            // （以降複合魔法）
            ssName[counter] = Database.NEUTRAL_SMASH; counter++;
            ssName[counter] = Database.STANCE_OF_DOUBLE; counter++;
            ssName[counter] = Database.SWIFT_STEP; counter++;
            ssName[counter] = Database.VIGOR_SENSE; counter++;
            ssName[counter] = Database.CIRCLE_SLASH; counter++;
            ssName[counter] = Database.RISING_AURA; counter++;
            ssName[counter] = Database.RUMBLE_SHOUT; counter++;
            ssName[counter] = Database.ONSLAUGHT_HIT; counter++;
            ssName[counter] = Database.SMOOTHING_MOVE; counter++;
            ssName[counter] = Database.ASCENSION_AURA; counter++;
            ssName[counter] = Database.FUTURE_VISION; counter++;
            ssName[counter] = Database.UNKNOWN_SHOCK; counter++;
            ssName[counter] = Database.REFLEX_SPIRIT; counter++;
            ssName[counter] = Database.FATAL_BLOW; counter++;
            ssName[counter] = Database.SHARP_GLARE; counter++;
            ssName[counter] = Database.CONCUSSIVE_HIT; counter++;
            ssName[counter] = Database.TRUST_SILENCE; counter++;
            ssName[counter] = Database.MIND_KILLING; counter++;
            ssName[counter] = Database.SURPRISE_ATTACK; counter++;
            ssName[counter] = Database.STANCE_OF_MYSTIC; counter++;
            ssName[counter] = Database.PSYCHIC_WAVE; counter++;
            ssName[counter] = Database.NOURISH_SENSE; counter++;
            ssName[counter] = Database.RECOVER; counter++;
            ssName[counter] = Database.IMPULSE_HIT; counter++;
            ssName[counter] = Database.VIOLENT_SLASH; counter++;
            ssName[counter] = Database.ONE_AUTHORITY; counter++;
            ssName[counter] = Database.OUTER_INSPIRATION; counter++;
            ssName[counter] = Database.HARDEST_PARRY; counter++;
            ssName[counter] = Database.STANCE_OF_SUDDENNESS; counter++;
            ssName[counter] = Database.SOUL_EXECUTION; counter++;

            return ssName;
        }

        public static Attribute GetAttribute(string command)
        {
            if (CheckPlayerActionFromString(command) == PlayerAction.UseSpell)
            {
                return Attribute.Spell;
            }
            else if (CheckPlayerActionFromString(command) == PlayerAction.UseSkill)
            {
                return Attribute.Skill;
            }
            else if (CheckPlayerActionFromString(command) == PlayerAction.Archetype)
            {
                return Attribute.Archetype;
            }
            return Attribute.None;
        }

        // ディスペル系統かどうか
        public static DispelType GetDispelType(string commandName)
        {
            if (commandName == Database.ATTACK_EN)
            {
                return 0;
            }
            else if (commandName == Database.DEFENSE_EN)
            {
                return 0;
            }
            else if (commandName == Database.STAY_EN)
            {
                return 0;
            }
            else if (commandName == Database.TAMERU_EN)
            {
                return 0;
            }
            else if (commandName == Database.FRESH_HEAL)
            {
                return DispelType.None;
            }
            else if (commandName == Database.PROTECTION)
            {
                return DispelType.None;
            }
            else if (commandName == Database.HOLY_SHOCK)
            {
                return DispelType.None;
            }
            else if (commandName == Database.SAINT_POWER)
            {
                return DispelType.None;
            }
            else if (commandName == Database.GLORY)
            {
                return DispelType.None;
            }
            else if (commandName == Database.RESURRECTION)
            {
                return DispelType.None;
            }
            else if (commandName == Database.CELESTIAL_NOVA)
            {
                return DispelType.None;
            }
            else if (commandName == Database.DARK_BLAST)
            {
                return DispelType.None;
            }
            else if (commandName == Database.SHADOW_PACT)
            {
                return DispelType.None;
            }
            else if (commandName == Database.LIFE_TAP)
            {
                return DispelType.None;
            }
            else if (commandName == Database.BLACK_CONTRACT)
            {
                return DispelType.None;
            }
            else if (commandName == Database.DEVOURING_PLAGUE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.BLOODY_VENGEANCE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.DAMNATION)
            {
                return DispelType.None;
            }
            else if (commandName == Database.FIRE_BALL)
            {
                return DispelType.None;
            }
            else if (commandName == Database.FLAME_AURA)
            {
                return DispelType.None;
            }
            else if (commandName == Database.HEAT_BOOST)
            {
                return DispelType.None;
            }
            else if (commandName == Database.FLAME_STRIKE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.VOLCANIC_WAVE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.IMMORTAL_RAVE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.LAVA_ANNIHILATION)
            {
                return DispelType.None;
            }
            else if (commandName == Database.ICE_NEEDLE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.ABSORB_WATER)
            {
                return DispelType.None;
            }
            else if (commandName == Database.CLEANSING)
            {
                return DispelType.None;
            }
            else if (commandName == Database.FROZEN_LANCE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.MIRROR_IMAGE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.PROMISED_KNOWLEDGE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.ABSOLUTE_ZERO)
            {
                return DispelType.None;
            }
            else if (commandName == Database.WORD_OF_POWER)
            {
                return DispelType.None;
            }
            else if (commandName == Database.GALE_WIND)
            {
                return DispelType.None;
            }
            else if (commandName == Database.WORD_OF_LIFE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.WORD_OF_FORTUNE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.AETHER_DRIVE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.GENESIS)
            {
                return DispelType.None;
            }
            else if (commandName == Database.ETERNAL_PRESENCE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.DISPEL_MAGIC)
            {
                return DispelType.Dispel;
            }
            else if (commandName == Database.RISE_OF_IMAGE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.DEFLECTION)
            {
                return DispelType.None;
            }
            else if (commandName == Database.TRANQUILITY)
            {
                return DispelType.Dispel;
            }
            else if (commandName == Database.ONE_IMMUNITY)
            {
                return DispelType.None;
            }
            else if (commandName == Database.WHITE_OUT)
            {
                return DispelType.None;
            }
            else if (commandName == Database.TIME_STOP)
            {
                return DispelType.None;
            }
            else if (commandName == Database.STRAIGHT_SMASH)
            {
                return DispelType.None;
            }
            else if (commandName == Database.DOUBLE_SLASH)
            {
                return DispelType.None;
            }
            else if (commandName == Database.CRUSHING_BLOW)
            {
                return DispelType.None;
            }
            else if (commandName == Database.SOUL_INFINITY)
            {
                return DispelType.None;
            }
            else if (commandName == Database.COUNTER_ATTACK)
            {
                return DispelType.Dispel;
            }
            else if (commandName == Database.PURE_PURIFICATION)
            {
                return DispelType.None;
            }
            else if (commandName == Database.ANTI_STUN)
            {
                return DispelType.None;
            }
            else if (commandName == Database.STANCE_OF_DEATH)
            {
                return DispelType.None;
            }
            else if (commandName == Database.STANCE_OF_FLOW)
            {
                return DispelType.None;
            }
            else if (commandName == Database.ENIGMA_SENSE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.SILENT_RUSH)
            {
                return DispelType.None;
            }
            else if (commandName == Database.OBORO_IMPACT)
            {
                return DispelType.None;
            }
            else if (commandName == Database.STANCE_OF_STANDING)
            {
                return DispelType.None;
            }
            else if (commandName == Database.INNER_INSPIRATION)
            {
                return DispelType.None;
            }
            else if (commandName == Database.KINETIC_SMASH)
            {
                return DispelType.None;
            }
            else if (commandName == Database.CATASTROPHE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.TRUTH_VISION)
            {
                return DispelType.None;
            }
            else if (commandName == Database.HIGH_EMOTIONALITY)
            {
                return DispelType.None;
            }
            else if (commandName == Database.STANCE_OF_EYES)
            {
                return DispelType.Dispel;
            }
            else if (commandName == Database.PAINFUL_INSANITY)
            {
                return DispelType.None;
            }
            else if (commandName == Database.NEGATE)
            {
                return DispelType.Dispel;
            }
            else if (commandName == Database.VOID_EXTRACTION)
            {
                return DispelType.None;
            }
            else if (commandName == Database.CARNAGE_RUSH)
            {
                return DispelType.None;
            }
            else if (commandName == Database.NOTHING_OF_NOTHINGNESS)
            {
                return DispelType.None;
            }
            else if (commandName == Database.PSYCHIC_TRANCE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.BLIND_JUSTICE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.TRANSCENDENT_WISH)
            {
                return DispelType.None;
            }
            else if (commandName == Database.FLASH_BLAZE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.LIGHT_DETONATOR)
            {
                return DispelType.None;
            }
            else if (commandName == Database.ASCENDANT_METEOR)
            {
                return DispelType.None;
            }
            else if (commandName == Database.SKY_SHIELD)
            {
                return DispelType.None;
            }
            else if (commandName == Database.SACRED_HEAL)
            {
                return DispelType.None;
            }
            else if (commandName == Database.EVER_DROPLET)
            {
                return DispelType.None;
            }
            else if (commandName == Database.HOLY_BREAKER)
            {
                return DispelType.None;
            }
            else if (commandName == Database.EXALTED_FIELD)
            {
                return DispelType.None;
            }
            else if (commandName == Database.HYMN_CONTRACT)
            {
                return DispelType.None;
            }
            else if (commandName == Database.STAR_LIGHTNING)
            {
                return DispelType.None;
            }
            else if (commandName == Database.ANGEL_BREATH)
            {
                return DispelType.None;
            }
            else if (commandName == Database.ENDLESS_ANTHEM)
            {
                return DispelType.None;
            }
            else if (commandName == Database.BLACK_FIRE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.BLAZING_FIELD)
            {
                return DispelType.None;
            }
            else if (commandName == Database.DEMONIC_IGNITE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.BLUE_BULLET)
            {
                return DispelType.None;
            }
            else if (commandName == Database.DEEP_MIRROR)
            {
                return DispelType.None;
            }
            else if (commandName == Database.DEATH_DENY)
            {
                return DispelType.None;
            }
            else if (commandName == Database.WORD_OF_MALICE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.ABYSS_EYE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.SIN_FORTUNE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.DARKEN_FIELD)
            {
                return DispelType.None;
            }
            else if (commandName == Database.DOOM_BLADE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.ECLIPSE_END)
            {
                return DispelType.EclipseEnd;
            }
            else if (commandName == Database.FROZEN_AURA)
            {
                return DispelType.None;
            }
            else if (commandName == Database.CHILL_BURN)
            {
                return DispelType.None;
            }
            else if (commandName == Database.ZETA_EXPLOSION)
            {
                return DispelType.None;
            }
            else if (commandName == Database.ENRAGE_BLAST)
            {
                return DispelType.None;
            }
            else if (commandName == Database.PIERCING_FLAME)
            {
                return DispelType.None;
            }
            else if (commandName == Database.SIGIL_OF_HOMURA)
            {
                return DispelType.None;
            }
            else if (commandName == Database.IMMOLATE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.PHANTASMAL_WIND)
            {
                return DispelType.None;
            }
            else if (commandName == Database.RED_DRAGON_WILL)
            {
                return DispelType.None;
            }
            else if (commandName == Database.WORD_OF_ATTITUDE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.STATIC_BARRIER)
            {
                return DispelType.None;
            }
            else if (commandName == Database.AUSTERITY_MATRIX)
            {
                return DispelType.AusterityMatrix;
            }
            else if (commandName == Database.VANISH_WAVE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.VORTEX_FIELD)
            {
                return DispelType.None;
            }
            else if (commandName == Database.BLUE_DRAGON_WILL)
            {
                return DispelType.None;
            }
            else if (commandName == Database.SEVENTH_MAGIC)
            {
                return DispelType.None;
            }
            else if (commandName == Database.PARADOX_IMAGE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.WARP_GATE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.NEUTRAL_SMASH)
            {
                return DispelType.None;
            }
            else if (commandName == Database.STANCE_OF_DOUBLE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.SWIFT_STEP)
            {
                return DispelType.None;
            }
            else if (commandName == Database.VIGOR_SENSE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.CIRCLE_SLASH)
            {
                return DispelType.None;
            }
            else if (commandName == Database.RISING_AURA)
            {
                return DispelType.None;
            }
            else if (commandName == Database.RUMBLE_SHOUT)
            {
                return DispelType.None;
            }
            else if (commandName == Database.ONSLAUGHT_HIT)
            {
                return DispelType.None;
            }
            else if (commandName == Database.SMOOTHING_MOVE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.ASCENSION_AURA)
            {
                return DispelType.None;
            }
            else if (commandName == Database.FUTURE_VISION)
            {
                return DispelType.FutureVision;
            }
            else if (commandName == Database.UNKNOWN_SHOCK)
            {
                return DispelType.None;
            }
            else if (commandName == Database.REFLEX_SPIRIT)
            {
                return DispelType.None;
            }
            else if (commandName == Database.FATAL_BLOW)
            {
                return DispelType.None;
            }
            else if (commandName == Database.SHARP_GLARE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.CONCUSSIVE_HIT)
            {
                return DispelType.None;
            }
            else if (commandName == Database.TRUST_SILENCE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.MIND_KILLING)
            {
                return DispelType.None;
            }
            else if (commandName == Database.SURPRISE_ATTACK)
            {
                return DispelType.None;
            }
            else if (commandName == Database.STANCE_OF_MYSTIC)
            {
                return DispelType.SpecialDispel; // 本スキルはダメージ置き換えでありカウンター系統ではないため、Special扱いとする。
            }
            else if (commandName == Database.PSYCHIC_WAVE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.NOURISH_SENSE)
            {
                return DispelType.None;
            }
            else if (commandName == Database.RECOVER)
            {
                return DispelType.None;
            }
            else if (commandName == Database.IMPULSE_HIT)
            {
                return DispelType.None;
            }
            else if (commandName == Database.VIOLENT_SLASH)
            {
                return DispelType.None;
            }
            else if (commandName == Database.ONE_AUTHORITY)
            {
                return DispelType.None;
            }
            else if (commandName == Database.OUTER_INSPIRATION)
            {
                return DispelType.None; // 能力低下状態を解除するのは、Dispel扱いだが負の効果をDispelのため、Noneとしておく。
            }
            else if (commandName == Database.HARDEST_PARRY)
            {
                return DispelType.SpecialDispel; // 本スキルはダメージ置き換えでありカウンター系統ではないため、Special扱いとする。
            }
            else if (commandName == Database.STANCE_OF_SUDDENNESS)
            {
                return DispelType.None;
            }
            else if (commandName == Database.SOUL_EXECUTION)
            {
                return DispelType.None;
            }
            else if (commandName == Database.ARCHETYPE_EIN_JP)
            {
                return DispelType.None;
            }
            else if (commandName == Database.ARCHETYPE_RANA_JP)
            {
                return DispelType.None;
            }
            else if (commandName == Database.ARCHETYPE_OL_JP)
            {
                return DispelType.None;
            }
            else if (commandName == Database.ARCHETYPE_VERZE_JP)
            {
                return DispelType.None;
            }
            else
            {
                return DispelType.None; // 何も判定できなかった場合は、Noneを返す
            }

            
        }

        // 消費コスト
        public static int GetCost(string commandName)
        {
            if (commandName == Database.ATTACK_EN)
            {
                return 0;
            }
            else if (commandName == Database.DEFENSE_EN)
            {
                return 0;
            }
            else if (commandName == Database.STAY_EN)
            {
                return 0;
            }
            else if (commandName == Database.TAMERU_EN)
            {
                return 0;
            }
            else if (commandName == Database.FRESH_HEAL)
            {
                return Database.FRESH_HEAL_COST;
            }
            else if (commandName == Database.PROTECTION)
            {
                return Database.PROTECTION_COST;
            }
            else if (commandName == Database.HOLY_SHOCK)
            {
                return Database.HOLY_SHOCK_COST;
            }
            else if (commandName == Database.SAINT_POWER)
            {
                return Database.SAINT_POWER_COST;
            }
            else if (commandName == Database.GLORY)
            {
                return Database.GLORY_COST;
            }
            else if (commandName == Database.RESURRECTION)
            {
                return Database.RESURRECTION_COST;
            }
            else if (commandName == Database.CELESTIAL_NOVA)
            {
                return Database.CELESTIAL_NOVA_COST;
            }
            else if (commandName == Database.DARK_BLAST)
            {
                return Database.DARK_BLAST_COST;
            }
            else if (commandName == Database.SHADOW_PACT)
            {
                return Database.SHADOW_PACT_COST;
            }
            else if (commandName == Database.LIFE_TAP)
            {
                return Database.LIFE_TAP_COST;
            }
            else if (commandName == Database.BLACK_CONTRACT)
            {
                return Database.BLACK_CONTRACT_COST;
            }
            else if (commandName == Database.DEVOURING_PLAGUE)
            {
                return Database.DEVOURING_PLAGUE_COST;
            }
            else if (commandName == Database.BLOODY_VENGEANCE)
            {
                return Database.BLOODY_VENGEANCE_COST;
            }
            else if (commandName == Database.DAMNATION)
            {
                return Database.DAMNATION_COST;
            }
            else if (commandName == Database.FIRE_BALL)
            {
                return Database.FIRE_BALL_COST;
            }
            else if (commandName == Database.FLAME_AURA)
            {
                return Database.FLAME_AURA_COST;
            }
            else if (commandName == Database.HEAT_BOOST)
            {
                return Database.HEAT_BOOST_COST;
            }
            else if (commandName == Database.FLAME_STRIKE)
            {
                return Database.FLAME_STRIKE_COST;
            }
            else if (commandName == Database.VOLCANIC_WAVE)
            {
                return Database.VOLCANIC_WAVE_COST;
            }
            else if (commandName == Database.IMMORTAL_RAVE)
            {
                return Database.IMMORTAL_RAVE_COST;
            }
            else if (commandName == Database.LAVA_ANNIHILATION)
            {
                return Database.LAVA_ANNIHILATION_COST;
            }
            else if (commandName == Database.ICE_NEEDLE)
            {
                return Database.ICE_NEEDLE_COST;
            }
            else if (commandName == Database.ABSORB_WATER)
            {
                return Database.ABSORB_WATER_COST;
            }
            else if (commandName == Database.CLEANSING)
            {
                return Database.CLEANSING_COST;
            }
            else if (commandName == Database.FROZEN_LANCE)
            {
                return Database.FROZEN_LANCE_COST;
            }
            else if (commandName == Database.MIRROR_IMAGE)
            {
                return Database.MIRROR_IMAGE_COST;
            }
            else if (commandName == Database.PROMISED_KNOWLEDGE)
            {
                return Database.PROMISED_KNOWLEDGE_COST;
            }
            else if (commandName == Database.ABSOLUTE_ZERO)
            {
                return Database.ABSOLUTE_ZERO_COST;
            }
            else if (commandName == Database.WORD_OF_POWER)
            {
                return Database.WORD_OF_POWER_COST;
            }
            else if (commandName == Database.GALE_WIND)
            {
                return Database.GALE_WIND_COST;
            }
            else if (commandName == Database.WORD_OF_LIFE)
            {
                return Database.WORD_OF_LIFE_COST;
            }
            else if (commandName == Database.WORD_OF_FORTUNE)
            {
                return Database.WORD_OF_FORTUNE_COST;
            }
            else if (commandName == Database.AETHER_DRIVE)
            {
                return Database.AETHER_DRIVE_COST;
            }
            else if (commandName == Database.GENESIS)
            {
                return Database.GENESIS_COST;
            }
            else if (commandName == Database.ETERNAL_PRESENCE)
            {
                return Database.ETERNAL_PRESENCE_COST;
            }
            else if (commandName == Database.DISPEL_MAGIC)
            {
                return Database.DISPEL_MAGIC_COST;
            }
            else if (commandName == Database.RISE_OF_IMAGE)
            {
                return Database.RISE_OF_IMAGE_COST;
            }
            else if (commandName == Database.DEFLECTION)
            {
                return Database.DEFLECTION_COST;
            }
            else if (commandName == Database.TRANQUILITY)
            {
                return Database.TRANQUILITY_COST;
            }
            else if (commandName == Database.ONE_IMMUNITY)
            {
                return Database.ONE_IMMUNITY_COST;
            }
            else if (commandName == Database.WHITE_OUT)
            {
                return Database.WHITE_OUT_COST;
            }
            else if (commandName == Database.TIME_STOP)
            {
                return Database.TIME_STOP_COST;
            }
            else if (commandName == Database.STRAIGHT_SMASH)
            {
                return Database.STRAIGHT_SMASH_COST;
            }
            else if (commandName == Database.DOUBLE_SLASH)
            {
                return Database.DOUBLE_SLASH_COST;
            }
            else if (commandName == Database.CRUSHING_BLOW)
            {
                return Database.CRUSHING_BLOW_COST;
            }
            else if (commandName == Database.SOUL_INFINITY)
            {
                return Database.SOUL_INFINITY_COST;
            }
            else if (commandName == Database.COUNTER_ATTACK)
            {
                return Database.COUNTER_ATTACK_COST;
            }
            else if (commandName == Database.PURE_PURIFICATION)
            {
                return Database.PURE_PURIFICATION_COST;
            }
            else if (commandName == Database.ANTI_STUN)
            {
                return Database.ANTI_STUN_COST;
            }
            else if (commandName == Database.STANCE_OF_DEATH)
            {
                return Database.STANCE_OF_DEATH_COST;
            }
            else if (commandName == Database.STANCE_OF_FLOW)
            {
                return Database.STANCE_OF_FLOW_COST;
            }
            else if (commandName == Database.ENIGMA_SENSE)
            {
                return Database.ENIGMA_SENSE_COST;
            }
            else if (commandName == Database.SILENT_RUSH)
            {
                return Database.SILENT_RUSH_COST;
            }
            else if (commandName == Database.OBORO_IMPACT)
            {
                return Database.OBORO_IMPACT_COST;
            }
            else if (commandName == Database.STANCE_OF_STANDING)
            {
                return Database.STANCE_OF_STANDING_COST;
            }
            else if (commandName == Database.INNER_INSPIRATION)
            {
                return Database.INNER_INSPIRATION_COST;
            }
            else if (commandName == Database.KINETIC_SMASH)
            {
                return Database.KINETIC_SMASH_COST;
            }
            else if (commandName == Database.CATASTROPHE)
            {
                return Database.CATASTROPHE_COST;
            }
            else if (commandName == Database.TRUTH_VISION)
            {
                return Database.TRUTH_VISION_COST;
            }
            else if (commandName == Database.HIGH_EMOTIONALITY)
            {
                return Database.HIGH_EMOTIONALITY_COST;
            }
            else if (commandName == Database.STANCE_OF_EYES)
            {
                return Database.STANCE_OF_EYES_COST;
            }
            else if (commandName == Database.PAINFUL_INSANITY)
            {
                return Database.PAINFUL_INSANITY_COST;
            }
            else if (commandName == Database.NEGATE)
            {
                return Database.NEGATE_COST;
            }
            else if (commandName == Database.VOID_EXTRACTION)
            {
                return Database.VOID_EXTRACTION_COST;
            }
            else if (commandName == Database.CARNAGE_RUSH)
            {
                return Database.CARNAGE_RUSH_COST;
            }
            else if (commandName == Database.NOTHING_OF_NOTHINGNESS)
            {
                return Database.NOTHING_OF_NOTHINGNESS_COST;
            }
            else if (commandName == Database.PSYCHIC_TRANCE)
            {
                return Database.PSYCHIC_TRANCE_COST;
            }
            else if (commandName == Database.BLIND_JUSTICE)
            {
                return Database.BLIND_JUSTICE_COST;
            }
            else if (commandName == Database.TRANSCENDENT_WISH)
            {
                return Database.TRANSCENDENT_WISH_COST;
            }
            else if (commandName == Database.FLASH_BLAZE)
            {
                return Database.FLASH_BLAZE_COST;
            }
            else if (commandName == Database.LIGHT_DETONATOR)
            {
                return Database.LIGHT_DETONATOR_COST;
            }
            else if (commandName == Database.ASCENDANT_METEOR)
            {
                return Database.ASCENDANT_METEOR_COST;
            }
            else if (commandName == Database.SKY_SHIELD)
            {
                return Database.SKY_SHIELD_COST;
            }
            else if (commandName == Database.SACRED_HEAL)
            {
                return Database.SACRED_HEAL_COST;
            }
            else if (commandName == Database.EVER_DROPLET)
            {
                return Database.EVER_DROPLET_COST;
            }
            else if (commandName == Database.HOLY_BREAKER)
            {
                return Database.HOLY_BREAKER_COST;
            }
            else if (commandName == Database.EXALTED_FIELD)
            {
                return Database.EXALTED_FIELD_COST;
            }
            else if (commandName == Database.HYMN_CONTRACT)
            {
                return Database.HYMN_CONTRACT_COST;
            }
            else if (commandName == Database.STAR_LIGHTNING)
            {
                return Database.STAR_LIGHTNING_COST;
            }
            else if (commandName == Database.ANGEL_BREATH)
            {
                return Database.ANGEL_BREATH_COST;
            }
            else if (commandName == Database.ENDLESS_ANTHEM)
            {
                return Database.ENDLESS_ANTHEM_COST;
            }
            else if (commandName == Database.BLACK_FIRE)
            {
                return Database.BLACK_FIRE_COST;
            }
            else if (commandName == Database.BLAZING_FIELD)
            {
                return Database.BLAZING_FIELD_COST;
            }
            else if (commandName == Database.DEMONIC_IGNITE)
            {
                return Database.DEMONIC_IGNITE_COST;
            }
            else if (commandName == Database.BLUE_BULLET)
            {
                return Database.BLUE_BULLET_COST;
            }
            else if (commandName == Database.DEEP_MIRROR)
            {
                return Database.DEEP_MIRROR_COST;
            }
            else if (commandName == Database.DEATH_DENY)
            {
                return Database.DEATH_DENY_COST;
            }
            else if (commandName == Database.WORD_OF_MALICE)
            {
                return Database.WORD_OF_MALICE_COST;
            }
            else if (commandName == Database.ABYSS_EYE)
            {
                return Database.ABYSS_EYE_COST;
            }
            else if (commandName == Database.SIN_FORTUNE)
            {
                return Database.SIN_FORTUNE_COST;
            }
            else if (commandName == Database.DARKEN_FIELD)
            {
                return Database.DARKEN_FIELD_COST;
            }
            else if (commandName == Database.DOOM_BLADE)
            {
                return Database.DOOM_BLADE_COST;
            }
            else if (commandName == Database.ECLIPSE_END)
            {
                return Database.ECLIPSE_END_COST;
            }
            else if (commandName == Database.FROZEN_AURA)
            {
                return Database.FROZEN_AURA_COST;
            }
            else if (commandName == Database.CHILL_BURN)
            {
                return Database.CHILL_BURN_COST;
            }
            else if (commandName == Database.ZETA_EXPLOSION)
            {
                return Database.ZETA_EXPLOSION_COST;
            }
            else if (commandName == Database.ENRAGE_BLAST)
            {
                return Database.ENRAGE_BLAST_COST;
            }
            else if (commandName == Database.PIERCING_FLAME)
            {
                return Database.PIERCING_FLAME_COST;
            }
            else if (commandName == Database.SIGIL_OF_HOMURA)
            {
                return Database.SIGIL_OF_HOMURA_COST;
            }
            else if (commandName == Database.IMMOLATE)
            {
                return Database.IMMOLATE_COST;
            }
            else if (commandName == Database.PHANTASMAL_WIND)
            {
                return Database.PHANTASMAL_WIND_COST;
            }
            else if (commandName == Database.RED_DRAGON_WILL)
            {
                return Database.RED_DRAGON_WILL_COST;
            }
            else if (commandName == Database.WORD_OF_ATTITUDE)
            {
                return Database.WORD_OF_ATTITUDE_COST;
            }
            else if (commandName == Database.STATIC_BARRIER)
            {
                return Database.STATIC_BARRIER_COST;
            }
            else if (commandName == Database.AUSTERITY_MATRIX)
            {
                return Database.AUSTERITY_MATRIX_COST;
            }
            else if (commandName == Database.VANISH_WAVE)
            {
                return Database.VANISH_WAVE_COST;
            }
            else if (commandName == Database.VORTEX_FIELD)
            {
                return Database.VORTEX_FIELD_COST;
            }
            else if (commandName == Database.BLUE_DRAGON_WILL)
            {
                return Database.BLUE_DRAGON_WILL_COST;
            }
            else if (commandName == Database.SEVENTH_MAGIC)
            {
                return Database.SEVENTH_MAGIC_COST;
            }
            else if (commandName == Database.PARADOX_IMAGE)
            {
                return Database.PARADOX_IMAGE_COST;
            }
            else if (commandName == Database.WARP_GATE)
            {
                return Database.WARP_GATE_COST;
            }
            else if (commandName == Database.NEUTRAL_SMASH)
            {
                return Database.NEUTRAL_SMASH_COST;
            }
            else if (commandName == Database.STANCE_OF_DOUBLE)
            {
                return Database.STANCE_OF_DOUBLE_COST;
            }
            else if (commandName == Database.SWIFT_STEP)
            {
                return Database.SWIFT_STEP_COST;
            }
            else if (commandName == Database.VIGOR_SENSE)
            {
                return Database.VIGOR_SENSE_COST;
            }
            else if (commandName == Database.CIRCLE_SLASH)
            {
                return Database.CIRCLE_SLASH_COST;
            }
            else if (commandName == Database.RISING_AURA)
            {
                return Database.RISING_AURA_COST;
            }
            else if (commandName == Database.RUMBLE_SHOUT)
            {
                return Database.RUMBLE_SHOUT_COST;
            }
            else if (commandName == Database.ONSLAUGHT_HIT)
            {
                return Database.ONSLAUGHT_HIT_COST;
            }
            else if (commandName == Database.SMOOTHING_MOVE)
            {
                return Database.SMOOTHING_MOVE_COST;
            }
            else if (commandName == Database.ASCENSION_AURA)
            {
                return Database.ASCENSION_AURA_COST;
            }
            else if (commandName == Database.FUTURE_VISION)
            {
                return Database.FUTURE_VISION_COST;
            }
            else if (commandName == Database.UNKNOWN_SHOCK)
            {
                return Database.UNKNOWN_SHOCK_COST;
            }
            else if (commandName == Database.REFLEX_SPIRIT)
            {
                return Database.REFLEX_SPIRIT_COST;
            }
            else if (commandName == Database.FATAL_BLOW)
            {
                return Database.FATAL_BLOW_COST;
            }
            else if (commandName == Database.SHARP_GLARE)
            {
                return Database.SHARP_GLARE_COST;
            }
            else if (commandName == Database.CONCUSSIVE_HIT)
            {
                return Database.CONCUSSIVE_HIT_COST;
            }
            else if (commandName == Database.TRUST_SILENCE)
            {
                return Database.TRUST_SILENCE_COST;
            }
            else if (commandName == Database.MIND_KILLING)
            {
                return Database.MIND_KILLING_COST;
            }
            else if (commandName == Database.SURPRISE_ATTACK)
            {
                return Database.SURPRISE_ATTACK_COST;
            }
            else if (commandName == Database.STANCE_OF_MYSTIC)
            {
                return Database.STANCE_OF_MYSTIC_COST;
            }
            else if (commandName == Database.PSYCHIC_WAVE)
            {
                return Database.PSYCHIC_WAVE_COST;
            }
            else if (commandName == Database.NOURISH_SENSE)
            {
                return Database.NOURISH_SENSE_COST;
            }
            else if (commandName == Database.RECOVER)
            {
                return Database.RECOVER_COST;
            }
            else if (commandName == Database.IMPULSE_HIT)
            {
                return Database.IMPULSE_HIT_COST;
            }
            else if (commandName == Database.VIOLENT_SLASH)
            {
                return Database.VIOLENT_SLASH_COST;
            }
            else if (commandName == Database.ONE_AUTHORITY)
            {
                return Database.ONE_AUTHORITY_COST;
            }
            else if (commandName == Database.OUTER_INSPIRATION)
            {
                return Database.OUTER_INSPIRATION_COST;
            }
            else if (commandName == Database.HARDEST_PARRY)
            {
                return Database.HARDEST_PARRY_COST;
            }
            else if (commandName == Database.STANCE_OF_SUDDENNESS)
            {
                return Database.STANCE_OF_SUDDENNESS_COST;
            }
            else if (commandName == Database.SOUL_EXECUTION)
            {
                return Database.SOUL_EXECUTION_COST;
            }
            else if (commandName == Database.ARCHETYPE_EIN_JP)
            {
                return Database.ARCHITECT_EIN_COST;
            }
            else if (commandName == Database.ARCHETYPE_RANA_JP)
            {
                return Database.ARCHITECT_RANA_COST;
            }
            else if (commandName == Database.ARCHETYPE_OL_JP)
            {
                return Database.ARCHITECT_OL_COST;
            }
            else if (commandName == Database.ARCHETYPE_VERZE_JP)
            {
                return Database.ARCHITECT_VERZE_COST;
            }
            else
            {
                return 0; // 何も判定できなかった場合は、０を返す
            }
        }

        // コマンド名を英語に変換
        public static string ConvertToEnglish(string commandName)
        {
            if (commandName == Database.ARCHETYPE_EIN_JP)
            {
                return Database.ARCHETYPE_EIN;
            }
            else if (commandName == Database.ARCHETYPE_RANA_JP)
            {
                return Database.ARCHETYPE_RANA;
            }
            else if (commandName == Database.ARCHETYPE_OL_JP)
            {
                return Database.ARCHETYPE_OL;
            }
            else if (commandName == Database.ARCHETYPE_VERZE_JP)
            {
                return Database.ARCHETYPE_VERZE;
            }
            else
            {
                return commandName; // 何も判定できなかった場合は、そのまま値を返す
            }
        }

        // コマンド名を日本語に変換
        public static string ConvertToJapanese(string commandName)
        {
            if (commandName == Database.ATTACK_EN)
            {
                return Database.ATTACK_JP;
            }
            else if (commandName == Database.DEFENSE_EN)
            {
                return Database.DEFENSE_JP;
            }
            else if (commandName == Database.STAY_EN)
            {
                return Database.STAY_JP;
            }
            else if (commandName == Database.TAMERU_EN)
            {
                return Database.TAMERU_JP;
            }
            else if (commandName == Database.FRESH_HEAL)
            {
                return Database.FRESH_HEAL_JP;
            }
            else if (commandName == Database.PROTECTION)
            {
                return Database.PROTECTION_JP;
            }
            else if (commandName == Database.HOLY_SHOCK)
            {
                return Database.HOLY_SHOCK_JP;
            }
            else if (commandName == Database.SAINT_POWER)
            {
                return Database.SAINT_POWER_JP;
            }
            else if (commandName == Database.GLORY)
            {
                return Database.GLORY_JP;
            }
            else if (commandName == Database.RESURRECTION)
            {
                return Database.RESURRECTION_JP;
            }
            else if (commandName == Database.CELESTIAL_NOVA)
            {
                return Database.CELESTIAL_NOVA_JP;
            }
            else if (commandName == Database.DARK_BLAST)
            {
                return Database.DARK_BLAST_JP;
            }
            else if (commandName == Database.SHADOW_PACT)
            {
                return Database.SHADOW_PACT_JP;
            }
            else if (commandName == Database.LIFE_TAP)
            {
                return Database.LIFE_TAP_JP;
            }
            else if (commandName == Database.BLACK_CONTRACT)
            {
                return Database.BLACK_CONTRACT_JP;
            }
            else if (commandName == Database.DEVOURING_PLAGUE)
            {
                return Database.DEVOURING_PLAGUE_JP;
            }
            else if (commandName == Database.BLOODY_VENGEANCE)
            {
                return Database.BLOODY_VENGEANCE_JP;
            }
            else if (commandName == Database.DAMNATION)
            {
                return Database.DAMNATION_JP;
            }
            else if (commandName == Database.FIRE_BALL)
            {
                return Database.FIRE_BALL_JP;
            }
            else if (commandName == Database.FLAME_AURA)
            {
                return Database.FLAME_AURA_JP;
            }
            else if (commandName == Database.HEAT_BOOST)
            {
                return Database.HEAT_BOOST_JP;
            }
            else if (commandName == Database.FLAME_STRIKE)
            {
                return Database.FLAME_STRIKE_JP;
            }
            else if (commandName == Database.VOLCANIC_WAVE)
            {
                return Database.VOLCANIC_WAVE_JP;
            }
            else if (commandName == Database.IMMORTAL_RAVE)
            {
                return Database.IMMORTAL_RAVE_JP;
            }
            else if (commandName == Database.LAVA_ANNIHILATION)
            {
                return Database.LAVA_ANNIHILATION_JP;
            }
            else if (commandName == Database.ICE_NEEDLE)
            {
                return Database.ICE_NEEDLE_JP;
            }
            else if (commandName == Database.ABSORB_WATER)
            {
                return Database.ABSORB_WATER_JP;
            }
            else if (commandName == Database.CLEANSING)
            {
                return Database.CLEANSING_JP;
            }
            else if (commandName == Database.FROZEN_LANCE)
            {
                return Database.FROZEN_LANCE_JP;
            }
            else if (commandName == Database.MIRROR_IMAGE)
            {
                return Database.MIRROR_IMAGE_JP;
            }
            else if (commandName == Database.PROMISED_KNOWLEDGE)
            {
                return Database.PROMISED_KNOWLEDGE_JP;
            }
            else if (commandName == Database.ABSOLUTE_ZERO)
            {
                return Database.ABSOLUTE_ZERO_JP;
            }
            else if (commandName == Database.WORD_OF_POWER)
            {
                return Database.WORD_OF_POWER_JP;
            }
            else if (commandName == Database.GALE_WIND)
            {
                return Database.GALE_WIND_JP;
            }
            else if (commandName == Database.WORD_OF_LIFE)
            {
                return Database.WORD_OF_LIFE_JP;
            }
            else if (commandName == Database.WORD_OF_FORTUNE)
            {
                return Database.WORD_OF_FORTUNE_JP;
            }
            else if (commandName == Database.AETHER_DRIVE)
            {
                return Database.AETHER_DRIVE_JP;
            }
            else if (commandName == Database.GENESIS)
            {
                return Database.GENESIS_JP;
            }
            else if (commandName == Database.ETERNAL_PRESENCE)
            {
                return Database.ETERNAL_PRESENCE_JP;
            }
            else if (commandName == Database.DISPEL_MAGIC)
            {
                return Database.DISPEL_MAGIC_JP;
            }
            else if (commandName == Database.RISE_OF_IMAGE)
            {
                return Database.RISE_OF_IMAGE_JP;
            }
            else if (commandName == Database.DEFLECTION)
            {
                return Database.DEFLECTION_JP;
            }
            else if (commandName == Database.TRANQUILITY)
            {
                return Database.TRANQUILITY_JP;
            }
            else if (commandName == Database.ONE_IMMUNITY)
            {
                return Database.ONE_IMMUNITY_JP;
            }
            else if (commandName == Database.WHITE_OUT)
            {
                return Database.WHITE_OUT_JP;
            }
            else if (commandName == Database.TIME_STOP)
            {
                return Database.TIME_STOP_JP;
            }
            else if (commandName == Database.STRAIGHT_SMASH)
            {
                return Database.STRAIGHT_SMASH_JP;
            }
            else if (commandName == Database.DOUBLE_SLASH)
            {
                return Database.DOUBLE_SLASH_JP;
            }
            else if (commandName == Database.CRUSHING_BLOW)
            {
                return Database.CRUSHING_BLOW_JP;
            }
            else if (commandName == Database.SOUL_INFINITY)
            {
                return Database.SOUL_INFINITY_JP;
            }
            else if (commandName == Database.COUNTER_ATTACK)
            {
                return Database.COUNTER_ATTACK_JP;
            }
            else if (commandName == Database.PURE_PURIFICATION)
            {
                return Database.PURE_PURIFICATION_JP;
            }
            else if (commandName == Database.ANTI_STUN)
            {
                return Database.ANTI_STUN_JP;
            }
            else if (commandName == Database.STANCE_OF_DEATH)
            {
                return Database.STANCE_OF_DEATH_JP;
            }
            else if (commandName == Database.STANCE_OF_FLOW)
            {
                return Database.STANCE_OF_FLOW_JP;
            }
            else if (commandName == Database.ENIGMA_SENSE)
            {
                return Database.ENIGMA_SENSE_JP;
            }
            else if (commandName == Database.SILENT_RUSH)
            {
                return Database.SILENT_RUSH_JP;
            }
            else if (commandName == Database.OBORO_IMPACT)
            {
                return Database.OBORO_IMPACT_JP;
            }
            else if (commandName == Database.STANCE_OF_STANDING)
            {
                return Database.STANCE_OF_STANDING_JP;
            }
            else if (commandName == Database.INNER_INSPIRATION)
            {
                return Database.INNER_INSPIRATION_JP;
            }
            else if (commandName == Database.KINETIC_SMASH)
            {
                return Database.KINETIC_SMASH_JP;
            }
            else if (commandName == Database.CATASTROPHE)
            {
                return Database.CATASTROPHE_JP;
            }
            else if (commandName == Database.TRUTH_VISION)
            {
                return Database.TRUTH_VISION_JP;
            }
            else if (commandName == Database.HIGH_EMOTIONALITY)
            {
                return Database.HIGH_EMOTIONALITY_JP;
            }
            else if (commandName == Database.STANCE_OF_EYES)
            {
                return Database.STANCE_OF_EYES_JP;
            }
            else if (commandName == Database.PAINFUL_INSANITY)
            {
                return Database.PAINFUL_INSANITY_JP;
            }
            else if (commandName == Database.NEGATE)
            {
                return Database.NEGATE_JP;
            }
            else if (commandName == Database.VOID_EXTRACTION)
            {
                return Database.VOID_EXTRACTION_JP;
            }
            else if (commandName == Database.CARNAGE_RUSH)
            {
                return Database.CARNAGE_RUSH_JP;
            }
            else if (commandName == Database.NOTHING_OF_NOTHINGNESS)
            {
                return Database.NOTHING_OF_NOTHINGNESS_JP;
            }
            else if (commandName == Database.PSYCHIC_TRANCE)
            {
                return Database.PSYCHIC_TRANCE_JP;
            }
            else if (commandName == Database.BLIND_JUSTICE)
            {
                return Database.BLIND_JUSTICE_JP;
            }
            else if (commandName == Database.TRANSCENDENT_WISH)
            {
                return Database.TRANSCENDENT_WISH_JP;
            }
            else if (commandName == Database.FLASH_BLAZE)
            {
                return Database.FLASH_BLAZE_JP;
            }
            else if (commandName == Database.LIGHT_DETONATOR)
            {
                return Database.LIGHT_DETONATOR_JP;
            }
            else if (commandName == Database.ASCENDANT_METEOR)
            {
                return Database.ASCENDANT_METEOR_JP;
            }
            else if (commandName == Database.SKY_SHIELD)
            {
                return Database.SKY_SHIELD_JP;
            }
            else if (commandName == Database.SACRED_HEAL)
            {
                return Database.SACRED_HEAL_JP;
            }
            else if (commandName == Database.EVER_DROPLET)
            {
                return Database.EVER_DROPLET_JP;
            }
            else if (commandName == Database.HOLY_BREAKER)
            {
                return Database.HOLY_BREAKER_JP;
            }
            else if (commandName == Database.EXALTED_FIELD)
            {
                return Database.EXALTED_FIELD_JP;
            }
            else if (commandName == Database.HYMN_CONTRACT)
            {
                return Database.HYMN_CONTRACT_JP;
            }
            else if (commandName == Database.STAR_LIGHTNING)
            {
                return Database.STAR_LIGHTNING_JP;
            }
            else if (commandName == Database.ANGEL_BREATH)
            {
                return Database.ANGEL_BREATH_JP;
            }
            else if (commandName == Database.ENDLESS_ANTHEM)
            {
                return Database.ENDLESS_ANTHEM_JP;
            }
            else if (commandName == Database.BLACK_FIRE)
            {
                return Database.BLACK_FIRE_JP;
            }
            else if (commandName == Database.BLAZING_FIELD)
            {
                return Database.BLAZING_FIELD_JP;
            }
            else if (commandName == Database.DEMONIC_IGNITE)
            {
                return Database.DEMONIC_IGNITE_JP;
            }
            else if (commandName == Database.BLUE_BULLET)
            {
                return Database.BLUE_BULLET_JP;
            }
            else if (commandName == Database.DEEP_MIRROR)
            {
                return Database.DEEP_MIRROR_JP;
            }
            else if (commandName == Database.DEATH_DENY)
            {
                return Database.DEATH_DENY_JP;
            }
            else if (commandName == Database.WORD_OF_MALICE)
            {
                return Database.WORD_OF_MALICE_JP;
            }
            else if (commandName == Database.ABYSS_EYE)
            {
                return Database.ABYSS_EYE_JP;
            }
            else if (commandName == Database.SIN_FORTUNE)
            {
                return Database.SIN_FORTUNE_JP;
            }
            else if (commandName == Database.DARKEN_FIELD)
            {
                return Database.DARKEN_FIELD_JP;
            }
            else if (commandName == Database.DOOM_BLADE)
            {
                return Database.DOOM_BLADE_JP;
            }
            else if (commandName == Database.ECLIPSE_END)
            {
                return Database.ECLIPSE_END_JP;
            }
            else if (commandName == Database.FROZEN_AURA)
            {
                return Database.FROZEN_AURA_JP;
            }
            else if (commandName == Database.CHILL_BURN)
            {
                return Database.CHILL_BURN_JP;
            }
            else if (commandName == Database.ZETA_EXPLOSION)
            {
                return Database.ZETA_EXPLOSION_JP;
            }
            else if (commandName == Database.ENRAGE_BLAST)
            {
                return Database.ENRAGE_BLAST_JP;
            }
            else if (commandName == Database.PIERCING_FLAME)
            {
                return Database.PIERCING_FLAME_JP;
            }
            else if (commandName == Database.SIGIL_OF_HOMURA)
            {
                return Database.SIGIL_OF_HOMURA_JP;
            }
            else if (commandName == Database.IMMOLATE)
            {
                return Database.IMMOLATE_JP;
            }
            else if (commandName == Database.PHANTASMAL_WIND)
            {
                return Database.PHANTASMAL_WIND_JP;
            }
            else if (commandName == Database.RED_DRAGON_WILL)
            {
                return Database.RED_DRAGON_WILL_JP;
            }
            else if (commandName == Database.WORD_OF_ATTITUDE)
            {
                return Database.WORD_OF_ATTITUDE_JP;
            }
            else if (commandName == Database.STATIC_BARRIER)
            {
                return Database.STATIC_BARRIER_JP;
            }
            else if (commandName == Database.AUSTERITY_MATRIX)
            {
                return Database.AUSTERITY_MATRIX_JP;
            }
            else if (commandName == Database.VANISH_WAVE)
            {
                return Database.VANISH_WAVE_JP;
            }
            else if (commandName == Database.VORTEX_FIELD)
            {
                return Database.VORTEX_FIELD_JP;
            }
            else if (commandName == Database.BLUE_DRAGON_WILL)
            {
                return Database.BLUE_DRAGON_WILL_JP;
            }
            else if (commandName == Database.SEVENTH_MAGIC)
            {
                return Database.SEVENTH_MAGIC_JP;
            }
            else if (commandName == Database.PARADOX_IMAGE)
            {
                return Database.PARADOX_IMAGE_JP;
            }
            else if (commandName == Database.WARP_GATE)
            {
                return Database.WARP_GATE_JP;
            }
            else if (commandName == Database.NEUTRAL_SMASH)
            {
                return Database.NEUTRAL_SMASH_JP;
            }
            else if (commandName == Database.STANCE_OF_DOUBLE)
            {
                return Database.STANCE_OF_DOUBLE_JP;
            }
            else if (commandName == Database.SWIFT_STEP)
            {
                return Database.SWIFT_STEP_JP;
            }
            else if (commandName == Database.VIGOR_SENSE)
            {
                return Database.VIGOR_SENSE_JP;
            }
            else if (commandName == Database.CIRCLE_SLASH)
            {
                return Database.CIRCLE_SLASH_JP;
            }
            else if (commandName == Database.RISING_AURA)
            {
                return Database.RISING_AURA_JP;
            }
            else if (commandName == Database.RUMBLE_SHOUT)
            {
                return Database.RUMBLE_SHOUT_JP;
            }
            else if (commandName == Database.ONSLAUGHT_HIT)
            {
                return Database.ONSLAUGHT_HIT_JP;
            }
            else if (commandName == Database.SMOOTHING_MOVE)
            {
                return Database.SMOOTHING_MOVE_JP;
            }
            else if (commandName == Database.ASCENSION_AURA)
            {
                return Database.ASCENSION_AURA_JP;
            }
            else if (commandName == Database.FUTURE_VISION)
            {
                return Database.FUTURE_VISION_JP;
            }
            else if (commandName == Database.UNKNOWN_SHOCK)
            {
                return Database.UNKNOWN_SHOCK_JP;
            }
            else if (commandName == Database.REFLEX_SPIRIT)
            {
                return Database.REFLEX_SPIRIT_JP;
            }
            else if (commandName == Database.FATAL_BLOW)
            {
                return Database.FATAL_BLOW_JP;
            }
            else if (commandName == Database.SHARP_GLARE)
            {
                return Database.SHARP_GLARE_JP;
            }
            else if (commandName == Database.CONCUSSIVE_HIT)
            {
                return Database.CONCUSSIVE_HIT_JP;
            }
            else if (commandName == Database.TRUST_SILENCE)
            {
                return Database.TRUST_SILENCE_JP;
            }
            else if (commandName == Database.MIND_KILLING)
            {
                return Database.MIND_KILLING_JP;
            }
            else if (commandName == Database.SURPRISE_ATTACK)
            {
                return Database.SURPRISE_ATTACK_JP;
            }
            else if (commandName == Database.STANCE_OF_MYSTIC)
            {
                return Database.STANCE_OF_MYSTIC_JP;
            }
            else if (commandName == Database.PSYCHIC_WAVE)
            {
                return Database.PSYCHIC_WAVE_JP;
            }
            else if (commandName == Database.NOURISH_SENSE)
            {
                return Database.NOURISH_SENSE_JP;
            }
            else if (commandName == Database.RECOVER)
            {
                return Database.RECOVER_JP;
            }
            else if (commandName == Database.IMPULSE_HIT)
            {
                return Database.IMPULSE_HIT_JP;
            }
            else if (commandName == Database.VIOLENT_SLASH)
            {
                return Database.VIOLENT_SLASH_JP;
            }
            else if (commandName == Database.ONE_AUTHORITY)
            {
                return Database.ONE_AUTHORITY_JP;
            }
            else if (commandName == Database.OUTER_INSPIRATION)
            {
                return Database.OUTER_INSPIRATION_JP;
            }
            else if (commandName == Database.HARDEST_PARRY)
            {
                return Database.HARDEST_PARRY_JP;
            }
            else if (commandName == Database.STANCE_OF_SUDDENNESS)
            {
                return Database.STANCE_OF_SUDDENNESS_JP;
            }
            else if (commandName == Database.SOUL_EXECUTION)
            {
                return Database.SOUL_EXECUTION_JP;
            }
            else if (commandName == Database.ARCHETYPE_EIN)
            {
                return Database.ARCHETYPE_EIN_JP;
            }
            else if (commandName == Database.ARCHETYPE_RANA)
            {
                return Database.ARCHETYPE_RANA_JP;
            }
            else if (commandName == Database.ARCHETYPE_OL)
            {
                return Database.ARCHETYPE_OL_JP;
            }
            else if (commandName == Database.ARCHETYPE_VERZE)
            {
                return Database.ARCHETYPE_VERZE_JP;
            }
            else
            {
                return commandName; // 何も判定できなかった場合は、そのまま値を返す
            }
        }

        // プレイヤーアクションタイプを判別
        public static PlayerAction CheckPlayerActionFromString(String commandName)
        {
            if (commandName == Database.ATTACK_EN)
            {
                return PlayerAction.NormalAttack;
            }
            else if (commandName == Database.DEFENSE_EN)
            {
                return PlayerAction.Defense;
            }
            else if (commandName == Database.STAY_EN)
            {
                return PlayerAction.None;
            }
            else if (commandName == Database.TAMERU_EN)
            {
                return PlayerAction.Charge;
            }
            else if ((commandName == Database.FRESH_HEAL) ||
                     (commandName == Database.PROTECTION) ||
                     (commandName == Database.HOLY_SHOCK) ||
                     (commandName == Database.SAINT_POWER) ||
                     (commandName == Database.GLORY) ||
                     (commandName == Database.RESURRECTION) ||
                     (commandName == Database.CELESTIAL_NOVA) ||
                     (commandName == Database.DARK_BLAST) ||
                     (commandName == Database.SHADOW_PACT) ||
                     (commandName == Database.LIFE_TAP) ||
                     (commandName == Database.BLACK_CONTRACT) ||
                     (commandName == Database.DEVOURING_PLAGUE) ||
                     (commandName == Database.BLOODY_VENGEANCE) ||
                     (commandName == Database.DAMNATION) ||
                     (commandName == Database.FIRE_BALL) ||
                     (commandName == Database.FLAME_AURA) ||
                     (commandName == Database.HEAT_BOOST) ||
                     (commandName == Database.FLAME_STRIKE) ||
                     (commandName == Database.VOLCANIC_WAVE) ||
                     (commandName == Database.IMMORTAL_RAVE) ||
                     (commandName == Database.LAVA_ANNIHILATION) ||
                     (commandName == Database.ICE_NEEDLE) ||
                     (commandName == Database.ABSORB_WATER) ||
                     (commandName == Database.CLEANSING) ||
                     (commandName == Database.FROZEN_LANCE) ||
                     (commandName == Database.MIRROR_IMAGE) ||
                     (commandName == Database.PROMISED_KNOWLEDGE) ||
                     (commandName == Database.ABSOLUTE_ZERO) ||
                     (commandName == Database.WORD_OF_POWER) ||
                     (commandName == Database.GALE_WIND) ||
                     (commandName == Database.WORD_OF_LIFE) ||
                     (commandName == Database.WORD_OF_FORTUNE) ||
                     (commandName == Database.AETHER_DRIVE) ||
                     (commandName == Database.GENESIS) ||
                     (commandName == Database.ETERNAL_PRESENCE) ||
                     (commandName == Database.DISPEL_MAGIC) ||
                     (commandName == Database.RISE_OF_IMAGE) ||
                     (commandName == Database.DEFLECTION) ||
                     (commandName == Database.TRANQUILITY) ||
                     (commandName == Database.ONE_IMMUNITY) ||
                     (commandName == Database.WHITE_OUT) ||
                     (commandName == Database.TIME_STOP) ||
                     (commandName == Database.PSYCHIC_TRANCE) ||
                     (commandName == Database.BLIND_JUSTICE) ||
                     (commandName == Database.TRANSCENDENT_WISH) ||
                     (commandName == Database.FLASH_BLAZE) ||
                     (commandName == Database.LIGHT_DETONATOR) ||
                     (commandName == Database.ASCENDANT_METEOR) ||
                     (commandName == Database.SKY_SHIELD) ||
                     (commandName == Database.SACRED_HEAL) ||
                     (commandName == Database.EVER_DROPLET) ||
                     (commandName == Database.HOLY_BREAKER) ||
                     (commandName == Database.EXALTED_FIELD) ||
                     (commandName == Database.HYMN_CONTRACT) ||
                     (commandName == Database.STAR_LIGHTNING) ||
                     (commandName == Database.ANGEL_BREATH) ||
                     (commandName == Database.ENDLESS_ANTHEM) ||
                     (commandName == Database.BLACK_FIRE) ||
                     (commandName == Database.BLAZING_FIELD) ||
                     (commandName == Database.DEMONIC_IGNITE) ||
                     (commandName == Database.BLUE_BULLET) ||
                     (commandName == Database.DEEP_MIRROR) ||
                     (commandName == Database.DEATH_DENY) ||
                     (commandName == Database.WORD_OF_MALICE) ||
                     (commandName == Database.ABYSS_EYE) ||
                     (commandName == Database.SIN_FORTUNE) ||
                     (commandName == Database.DARKEN_FIELD) ||
                     (commandName == Database.DOOM_BLADE) ||
                     (commandName == Database.ECLIPSE_END) ||
                     (commandName == Database.DARKEN_FIELD) ||
                     (commandName == Database.DOOM_BLADE) ||
                     (commandName == Database.ECLIPSE_END) ||
                     (commandName == Database.FROZEN_AURA) ||
                     (commandName == Database.CHILL_BURN) ||
                     (commandName == Database.ZETA_EXPLOSION) ||
                     (commandName == Database.ENRAGE_BLAST) ||
                     (commandName == Database.PIERCING_FLAME) ||
                     (commandName == Database.SIGIL_OF_HOMURA) ||
                     (commandName == Database.IMMOLATE) ||
                     (commandName == Database.PHANTASMAL_WIND) ||
                     (commandName == Database.RED_DRAGON_WILL) ||
                     (commandName == Database.WORD_OF_ATTITUDE) ||
                     (commandName == Database.STATIC_BARRIER) ||
                     (commandName == Database.AUSTERITY_MATRIX) ||
                     (commandName == Database.VANISH_WAVE) ||
                     (commandName == Database.VORTEX_FIELD) ||
                     (commandName == Database.BLUE_DRAGON_WILL) ||
                     (commandName == Database.SEVENTH_MAGIC) ||
                     (commandName == Database.PARADOX_IMAGE) ||
                     (commandName == Database.WARP_GATE)
                )
            {
                return PlayerAction.UseSpell;
            }

            else if ((commandName == Database.STRAIGHT_SMASH) ||
                     (commandName == Database.DOUBLE_SLASH) ||
                     (commandName == Database.CRUSHING_BLOW) ||
                     (commandName == Database.SOUL_INFINITY) ||
                     (commandName == Database.COUNTER_ATTACK) ||
                     (commandName == Database.PURE_PURIFICATION) ||
                     (commandName == Database.ANTI_STUN) ||
                     (commandName == Database.STANCE_OF_DEATH) ||
                     (commandName == Database.STANCE_OF_FLOW) ||
                     (commandName == Database.ENIGMA_SENSE) ||
                     (commandName == Database.SILENT_RUSH) ||
                     (commandName == Database.OBORO_IMPACT) ||
                     (commandName == Database.STANCE_OF_STANDING) ||
                     (commandName == Database.INNER_INSPIRATION) ||
                     (commandName == Database.KINETIC_SMASH) ||
                     (commandName == Database.CATASTROPHE) ||
                     (commandName == Database.TRUTH_VISION) ||
                     (commandName == Database.HIGH_EMOTIONALITY) ||
                     (commandName == Database.STANCE_OF_EYES) ||
                     (commandName == Database.PAINFUL_INSANITY) ||
                     (commandName == Database.NEGATE) ||
                     (commandName == Database.VOID_EXTRACTION) ||
                     (commandName == Database.CARNAGE_RUSH) ||
                     (commandName == Database.NOTHING_OF_NOTHINGNESS) ||
                     (commandName == Database.NEUTRAL_SMASH) ||
                     (commandName == Database.STANCE_OF_DOUBLE) ||
                     (commandName == Database.SWIFT_STEP) ||
                     (commandName == Database.VIGOR_SENSE) ||
                     (commandName == Database.CIRCLE_SLASH) ||
                     (commandName == Database.RISING_AURA) ||
                     (commandName == Database.RUMBLE_SHOUT) ||
                     (commandName == Database.ONSLAUGHT_HIT) ||
                     (commandName == Database.SMOOTHING_MOVE) ||
                     (commandName == Database.ASCENSION_AURA) ||
                     (commandName == Database.FUTURE_VISION) ||
                     (commandName == Database.UNKNOWN_SHOCK) ||
                     (commandName == Database.REFLEX_SPIRIT) ||
                     (commandName == Database.FATAL_BLOW) ||
                     (commandName == Database.SHARP_GLARE) ||
                     (commandName == Database.CONCUSSIVE_HIT) ||
                     (commandName == Database.TRUST_SILENCE) ||
                     (commandName == Database.MIND_KILLING) ||
                     (commandName == Database.SURPRISE_ATTACK) ||
                     (commandName == Database.STANCE_OF_MYSTIC) ||
                     (commandName == Database.PSYCHIC_WAVE) ||
                     (commandName == Database.NOURISH_SENSE) ||
                     (commandName == Database.RECOVER) ||
                     (commandName == Database.IMPULSE_HIT) ||
                     (commandName == Database.VIOLENT_SLASH) ||
                     (commandName == Database.ONE_AUTHORITY) ||
                     (commandName == Database.OUTER_INSPIRATION) ||
                     (commandName == Database.HARDEST_PARRY) ||
                     (commandName == Database.STANCE_OF_SUDDENNESS) ||
                     (commandName == Database.SOUL_EXECUTION)
                )
            {
                return PlayerAction.UseSkill;
            }
            else if ((commandName == Database.ARCHETYPE_EIN) ||
                     (commandName == Database.ARCHETYPE_RANA) ||
                     (commandName == Database.ARCHETYPE_OL) ||
                     (commandName == Database.ARCHETYPE_VERZE))
            {
                return PlayerAction.Archetype;
            }
            else
            {
                return PlayerAction.UseItem; // 任意の文字は全てアイテム使用と判断する。
            }
        }

        // 対象のコマンドがカウンター不可能かどうかを判別
        public static bool CantBeCountered(string command)
        {
            if ((command == Database.ZETA_EXPLOSION) ||
                (command == Database.WORD_OF_POWER) ||
                (command == Database.VIOLENT_SLASH) ||
                (command == Database.STANCE_OF_MYSTIC) ||
                (command == Database.PSYCHIC_WAVE) ||
                (command == Database.ARCHETYPE_EIN) ||
                (command == Database.ARCHETYPE_RANA) ||
                (command == Database.ARCHETYPE_OL) ||
                (command == Database.ARCHETYPE_VERZE))
            {
                return true;
            }
            return false;
        }

        private static double ItemEffect_SkillCost(string command, ItemBackPack item)
        {
            double reduce = 0.00f;
            if ((item != null))
            {
                reduce += item.SkillCostReduction;
                if (IsActive(GetSkillType(command)))
                {
                    reduce += item.SkillCostReductionActive;
                }
                if (IsPassive(GetSkillType(command)))
                {
                    reduce += item.SkillCostReductionPassive;
                }
                if (IsSoft(GetSkillType(command)))
                {
                    reduce += item.SkillCostReductionSoft;
                }
                if (IsHard(GetSkillType(command)))
                {
                    reduce += item.SkillCostReductionHard;
                }
                if (IsTruth(GetSkillType(command)))
                {
                    reduce += item.SkillCostReductionTruth;
                }
                if (IsVoid(GetSkillType(command)))
                {
                    reduce += item.SkillCostReductionVoid;
                }
            }
            if (reduce >= 1.00f) { reduce = 1.00f; }

            return reduce;
        }

        private static double ItemEffect_ManaCost(string command, ItemBackPack item)
        {
            double reduce = 0.00f;
            if ((item != null))
            {
                reduce += item.ManaCostReduction;
                if (IsLight(GetMagicType(command)))
                {
                    reduce += item.ManaCostReductionLight;
                }
                if (IsShadow(GetMagicType(command)))
                {
                    reduce += item.ManaCostReductionShadow;
                }
                if (IsFire(GetMagicType(command)))
                {
                    reduce += item.ManaCostReductionFire;
                }
                if (IsIce(GetMagicType(command)))
                {
                    reduce += item.ManaCostReductionIce;
                }
                if (IsForce(GetMagicType(command)))
                {
                    reduce += item.ManaCostReductionForce;
                }
                if (IsWill(GetMagicType(command)))
                {
                    reduce += item.ManaCostReductionWill;
                }
            }
            if (reduce >= 1.00f) { reduce = 1.00f; }

            return reduce;
        }

        // コマンドのコスト
        public static int Cost(string command, MainCharacter player)
        {
            double manaReduce = 0;
            double skillReduce = 0;
            if (player != null)
            {
                manaReduce += ItemEffect_ManaCost(command, player.MainWeapon);
                manaReduce += ItemEffect_ManaCost(command, player.SubWeapon);
                manaReduce += ItemEffect_ManaCost(command, player.MainArmor);
                manaReduce += ItemEffect_ManaCost(command, player.Accessory);
                manaReduce += ItemEffect_ManaCost(command, player.Accessory2);

                skillReduce += ItemEffect_SkillCost(command, player.MainWeapon);
                skillReduce += ItemEffect_SkillCost(command, player.SubWeapon);
                skillReduce += ItemEffect_SkillCost(command, player.MainArmor);
                skillReduce += ItemEffect_SkillCost(command, player.Accessory);
                skillReduce += ItemEffect_SkillCost(command, player.Accessory2);
            }
            if (manaReduce >= 1.00f) { manaReduce = 1.00f; }
            if (skillReduce >= 1.00f) { skillReduce = 1.00f; }

            if (command == Database.ATTACK_EN) { return 0; }

            if (command == Database.FRESH_HEAL) { return (int)((double)Database.FRESH_HEAL_COST * (1.00f - manaReduce)); }
            if (command == Database.PROTECTION) { return (int)((double)Database.PROTECTION_COST * (1.00f - manaReduce)); }
            if (command == Database.HOLY_SHOCK) { return (int)((double)Database.HOLY_SHOCK_COST * (1.00f - manaReduce)); }
            if (command == Database.SAINT_POWER) { return (int)((double)Database.SAINT_POWER_COST * (1.00f - manaReduce)); }
            if (command == Database.GLORY) { return (int)((double)Database.GLORY_COST * (1.00f - manaReduce)); }
            if (command == Database.RESURRECTION) { return (int)((double)Database.RESURRECTION_COST * (1.00f - manaReduce)); }
            if (command == Database.CELESTIAL_NOVA) { return (int)((double)Database.CELESTIAL_NOVA_COST * (1.00f - manaReduce)); }

            if (command == Database.DARK_BLAST) { return (int)((double)Database.DARK_BLAST_COST * (1.00f - manaReduce)); }
            if (command == Database.SHADOW_PACT) { return (int)((double)Database.SHADOW_PACT_COST * (1.00f - manaReduce)); }
            if (command == Database.LIFE_TAP) { return (int)((double)Database.LIFE_TAP_COST * (1.00f - manaReduce)); }
            if (command == Database.BLACK_CONTRACT) { return (int)((double)Database.BLACK_CONTRACT_COST * (1.00f - manaReduce)); }
            if (command == Database.DEVOURING_PLAGUE) { return (int)((double)Database.DEVOURING_PLAGUE_COST * (1.00f - manaReduce)); }
            if (command == Database.BLOODY_VENGEANCE) { return (int)((double)Database.BLOODY_VENGEANCE_COST * (1.00f - manaReduce)); }
            if (command == Database.DAMNATION) { return (int)((double)Database.DAMNATION_COST * (1.00f - manaReduce)); }

            if (command == Database.FIRE_BALL) { return (int)((double)Database.FIRE_BALL_COST * (1.00f - manaReduce)); }
            if (command == Database.FLAME_AURA) { return (int)((double)Database.FLAME_AURA_COST * (1.00f - manaReduce)); }
            if (command == Database.HEAT_BOOST) { return (int)((double)Database.HEAT_BOOST_COST * (1.00f - manaReduce)); }
            if (command == Database.FLAME_STRIKE) { return (int)((double)Database.FLAME_STRIKE_COST * (1.00f - manaReduce)); }
            if (command == Database.VOLCANIC_WAVE) { return (int)((double)Database.VOLCANIC_WAVE_COST * (1.00f - manaReduce)); }
            if (command == Database.IMMORTAL_RAVE) { return (int)((double)Database.IMMORTAL_RAVE_COST * (1.00f - manaReduce)); }
            if (command == Database.LAVA_ANNIHILATION) { return (int)((double)Database.LAVA_ANNIHILATION_COST * (1.00f - manaReduce)); }

            if (command == Database.ICE_NEEDLE) { return (int)((double)Database.ICE_NEEDLE_COST * (1.00f - manaReduce)); }
            if (command == Database.ABSORB_WATER) { return (int)((double)Database.ABSORB_WATER_COST * (1.00f - manaReduce)); }
            if (command == Database.CLEANSING) { return (int)((double)Database.CLEANSING_COST * (1.00f - manaReduce)); }
            if (command == Database.FROZEN_LANCE) { return (int)((double)Database.FROZEN_LANCE_COST * (1.00f - manaReduce)); }
            if (command == Database.MIRROR_IMAGE) { return (int)((double)Database.MIRROR_IMAGE_COST * (1.00f - manaReduce)); }
            if (command == Database.PROMISED_KNOWLEDGE) { return (int)((double)Database.PROMISED_KNOWLEDGE_COST * (1.00f - manaReduce)); }
            if (command == Database.ABSOLUTE_ZERO) { return (int)((double)Database.ABSOLUTE_ZERO_COST * (1.00f - manaReduce)); }

            if (command == Database.WORD_OF_POWER) { return (int)((double)Database.WORD_OF_POWER_COST * (1.00f - manaReduce)); }
            if (command == Database.GALE_WIND) { return (int)((double)Database.GALE_WIND_COST * (1.00f - manaReduce)); }
            if (command == Database.WORD_OF_LIFE) { return (int)((double)Database.WORD_OF_LIFE_COST * (1.00f - manaReduce)); }
            if (command == Database.WORD_OF_FORTUNE) { return (int)((double)Database.WORD_OF_FORTUNE_COST * (1.00f - manaReduce)); }
            if (command == Database.AETHER_DRIVE) { return (int)((double)Database.AETHER_DRIVE_COST * (1.00f - manaReduce)); }
            if (command == Database.GENESIS) { return (int)((double)Database.GENESIS_COST * (1.00f - manaReduce)); }
            if (command == Database.ETERNAL_PRESENCE) { return (int)((double)Database.ETERNAL_PRESENCE_COST * (1.00f - manaReduce)); }

            if (command == Database.DISPEL_MAGIC) { return (int)((double)Database.DISPEL_MAGIC_COST * (1.00f - manaReduce)); }
            if (command == Database.RISE_OF_IMAGE) { return (int)((double)Database.RISE_OF_IMAGE_COST * (1.00f - manaReduce)); }
            if (command == Database.DEFLECTION) { return (int)((double)Database.DEFLECTION_COST * (1.00f - manaReduce)); }
            if (command == Database.TRANQUILITY) { return (int)((double)Database.TRANQUILITY_COST * (1.00f - manaReduce)); }
            if (command == Database.ONE_IMMUNITY) { return (int)((double)Database.ONE_IMMUNITY_COST * (1.00f - manaReduce)); }
            if (command == Database.WHITE_OUT) { return (int)((double)Database.WHITE_OUT_COST * (1.00f - manaReduce)); }
            if (command == Database.TIME_STOP) { return (int)((double)Database.TIME_STOP_COST * (1.00f - manaReduce)); }

            if (command == Database.PSYCHIC_TRANCE) { return (int)((double)Database.PSYCHIC_TRANCE_COST * (1.00f - manaReduce)); }
            if (command == Database.BLIND_JUSTICE) { return (int)((double)Database.BLIND_JUSTICE_COST * (1.00f - manaReduce)); }
            if (command == Database.TRANSCENDENT_WISH) { return (int)((double)Database.TRANSCENDENT_WISH_COST * (1.00f - manaReduce)); }

            if (command == Database.FLASH_BLAZE) { return (int)((double)Database.FLASH_BLAZE_COST * (1.00f - manaReduce)); }
            if (command == Database.LIGHT_DETONATOR) { return (int)((double)Database.LIGHT_DETONATOR_COST * (1.00f - manaReduce)); }
            if (command == Database.ASCENDANT_METEOR) { return (int)((double)Database.ASCENDANT_METEOR_COST * (1.00f - manaReduce)); }

            if (command == Database.SKY_SHIELD) { return (int)((double)Database.SKY_SHIELD_COST * (1.00f - manaReduce)); }
            if (command == Database.SACRED_HEAL) { return (int)((double)Database.SACRED_HEAL_COST * (1.00f - manaReduce)); }
            if (command == Database.EVER_DROPLET) { return (int)((double)Database.EVER_DROPLET_COST * (1.00f - manaReduce)); }

            if (command == Database.HOLY_BREAKER) { return (int)((double)Database.HOLY_BREAKER_COST * (1.00f - manaReduce)); }
            if (command == Database.EXALTED_FIELD) { return (int)((double)Database.EXALTED_FIELD_COST * (1.00f - manaReduce)); }
            if (command == Database.HYMN_CONTRACT) { return (int)((double)Database.HYMN_CONTRACT_COST * (1.00f - manaReduce)); }

            if (command == Database.STAR_LIGHTNING) { return (int)((double)Database.STAR_LIGHTNING_COST * (1.00f - manaReduce)); }
            if (command == Database.ANGEL_BREATH) { return (int)((double)Database.ANGEL_BREATH_COST * (1.00f - manaReduce)); }
            if (command == Database.ENDLESS_ANTHEM) { return (int)((double)Database.ENDLESS_ANTHEM_COST * (1.00f - manaReduce)); }

            if (command == Database.BLACK_FIRE) { return (int)((double)Database.BLACK_FIRE_COST * (1.00f - manaReduce)); }
            if (command == Database.BLAZING_FIELD) { return (int)((double)Database.BLAZING_FIELD_COST * (1.00f - manaReduce)); }
            if (command == Database.DEMONIC_IGNITE) { return (int)((double)Database.DEMONIC_IGNITE_COST * (1.00f - manaReduce)); }

            if (command == Database.BLUE_BULLET) { return (int)((double)Database.BLUE_BULLET_COST * (1.00f - manaReduce)); }
            if (command == Database.DEEP_MIRROR) { return (int)((double)Database.DEEP_MIRROR_COST * (1.00f - manaReduce)); }
            if (command == Database.DEATH_DENY) { return (int)((double)Database.DEATH_DENY_COST * (1.00f - manaReduce)); }

            if (command == Database.WORD_OF_MALICE) { return (int)((double)Database.WORD_OF_MALICE_COST * (1.00f - manaReduce)); }
            if (command == Database.ABYSS_EYE) { return (int)((double)Database.ABYSS_EYE_COST * (1.00f - manaReduce)); }
            if (command == Database.SIN_FORTUNE) { return (int)((double)Database.SIN_FORTUNE_COST * (1.00f - manaReduce)); }

            if (command == Database.DARKEN_FIELD) { return (int)((double)Database.DARKEN_FIELD_COST * (1.00f - manaReduce)); }
            if (command == Database.DOOM_BLADE) { return (int)((double)Database.DOOM_BLADE_COST * (1.00f - manaReduce)); }
            if (command == Database.ECLIPSE_END) { return (int)((double)Database.ECLIPSE_END_COST * (1.00f - manaReduce)); }

            if (command == Database.FROZEN_AURA) { return (int)((double)Database.FROZEN_AURA_COST * (1.00f - manaReduce)); }
            if (command == Database.CHILL_BURN) { return (int)((double)Database.CHILL_BURN_COST * (1.00f - manaReduce)); }
            if (command == Database.ZETA_EXPLOSION) { return (int)((double)Database.ZETA_EXPLOSION_COST * (1.00f - manaReduce)); }

            if (command == Database.ENRAGE_BLAST) { return (int)((double)Database.ENRAGE_BLAST_COST * (1.00f - manaReduce)); }
            if (command == Database.PIERCING_FLAME) { return (int)((double)Database.PIERCING_FLAME_COST * (1.00f - manaReduce)); }
            if (command == Database.SIGIL_OF_HOMURA) { return (int)((double)Database.SIGIL_OF_HOMURA_COST * (1.00f - manaReduce)); }

            if (command == Database.IMMOLATE) { return (int)((double)Database.IMMOLATE_COST * (1.00f - manaReduce)); }
            if (command == Database.PHANTASMAL_WIND) { return (int)((double)Database.PHANTASMAL_WIND_COST * (1.00f - manaReduce)); }
            if (command == Database.RED_DRAGON_WILL) { return (int)((double)Database.RED_DRAGON_WILL_COST * (1.00f - manaReduce)); }

            if (command == Database.WORD_OF_ATTITUDE) { return (int)((double)Database.WORD_OF_ATTITUDE_COST * (1.00f - manaReduce)); }
            if (command == Database.STATIC_BARRIER) { return (int)((double)Database.STATIC_BARRIER_COST * (1.00f - manaReduce)); }
            if (command == Database.AUSTERITY_MATRIX) { return (int)((double)Database.AUSTERITY_MATRIX_COST * (1.00f - manaReduce)); }

            if (command == Database.VANISH_WAVE) { return (int)((double)Database.VANISH_WAVE_COST * (1.00f - manaReduce)); }
            if (command == Database.VORTEX_FIELD) { return (int)((double)Database.VORTEX_FIELD_COST * (1.00f - manaReduce)); }
            if (command == Database.BLUE_DRAGON_WILL) { return (int)((double)Database.BLUE_DRAGON_WILL_COST * (1.00f - manaReduce)); }

            if (command == Database.SEVENTH_MAGIC) { return (int)((double)Database.SEVENTH_MAGIC_COST * (1.00f - manaReduce)); }
            if (command == Database.PARADOX_IMAGE) { return (int)((double)Database.PARADOX_IMAGE_COST * (1.00f - manaReduce)); }
            if (command == Database.WARP_GATE) { return (int)((double)Database.WARP_GATE_COST * (1.00f - manaReduce)); }
            // スキル
            if (command == Database.STRAIGHT_SMASH) { return (int)((double)Database.STRAIGHT_SMASH_COST * (1.00f - skillReduce)); }
            if (command == Database.DOUBLE_SLASH) { return (int)((double)Database.DOUBLE_SLASH_COST * (1.00f - skillReduce)); }
            if (command == Database.CRUSHING_BLOW) { return (int)((double)Database.CRUSHING_BLOW_COST * (1.00f - skillReduce)); }
            if (command == Database.SOUL_INFINITY) { return (int)((double)Database.SOUL_INFINITY_COST * (1.00f - skillReduce)); }

            if (command == Database.COUNTER_ATTACK) { return (int)((double)Database.COUNTER_ATTACK_COST * (1.00f - skillReduce)); }
            if (command == Database.PURE_PURIFICATION) { return (int)((double)Database.PURE_PURIFICATION_COST * (1.00f - skillReduce)); }
            if (command == Database.ANTI_STUN) { return (int)((double)Database.ANTI_STUN_COST * (1.00f - skillReduce)); }
            if (command == Database.STANCE_OF_DEATH) { return (int)((double)Database.STANCE_OF_DEATH_COST * (1.00f - skillReduce)); }

            if (command == Database.STANCE_OF_FLOW) { return (int)((double)Database.STANCE_OF_FLOW_COST * (1.00f - skillReduce)); }
            if (command == Database.ENIGMA_SENSE) { return (int)((double)Database.ENIGMA_SENSE_COST * (1.00f - skillReduce)); }
            if (command == Database.SILENT_RUSH) { return (int)((double)Database.SILENT_RUSH_COST * (1.00f - skillReduce)); }
            if (command == Database.OBORO_IMPACT) { return (int)((double)Database.OBORO_IMPACT_COST * (1.00f - skillReduce)); }

            if (command == Database.STANCE_OF_STANDING) { return (int)((double)Database.STANCE_OF_STANDING_COST * (1.00f - skillReduce)); }
            if (command == Database.INNER_INSPIRATION) { return (int)((double)Database.INNER_INSPIRATION_COST * (1.00f - skillReduce)); }
            if (command == Database.KINETIC_SMASH) { return (int)((double)Database.KINETIC_SMASH_COST * (1.00f - skillReduce)); }
            if (command == Database.CATASTROPHE) { return (int)((double)Database.CATASTROPHE_COST * (1.00f - skillReduce)); }

            if (command == Database.TRUTH_VISION) { return (int)((double)Database.TRUTH_VISION_COST * (1.00f - skillReduce)); }
            if (command == Database.HIGH_EMOTIONALITY) { return (int)((double)Database.HIGH_EMOTIONALITY_COST * (1.00f - skillReduce)); }
            if (command == Database.STANCE_OF_EYES) { return (int)((double)Database.STANCE_OF_EYES_COST * (1.00f - skillReduce)); }
            if (command == Database.PAINFUL_INSANITY) { return (int)((double)Database.PAINFUL_INSANITY_COST * (1.00f - skillReduce)); }

            if (command == Database.NEGATE) { return (int)((double)Database.NEGATE_COST * (1.00f - skillReduce)); }
            if (command == Database.VOID_EXTRACTION) { return (int)((double)Database.VOID_EXTRACTION_COST * (1.00f - skillReduce)); }
            if (command == Database.CARNAGE_RUSH) { return (int)((double)Database.CARNAGE_RUSH_COST * (1.00f - skillReduce)); }
            if (command == Database.NOTHING_OF_NOTHINGNESS) { return (int)((double)Database.NOTHING_OF_NOTHINGNESS_COST * (1.00f - skillReduce)); }

            if (command == Database.NEUTRAL_SMASH) { return (int)((double)Database.NEUTRAL_SMASH_COST * (1.00f - skillReduce)); }
            if (command == Database.STANCE_OF_DOUBLE) { return (int)((double)Database.STANCE_OF_DOUBLE_COST * (1.00f - skillReduce)); }

            if (command == Database.SWIFT_STEP) { return (int)((double)Database.SWIFT_STEP_COST * (1.00f - skillReduce)); }
            if (command == Database.VIGOR_SENSE) { return (int)((double)Database.VIGOR_SENSE_COST * (1.00f - skillReduce)); }

            if (command == Database.CIRCLE_SLASH) { return (int)((double)Database.CIRCLE_SLASH_COST * (1.00f - skillReduce)); }
            if (command == Database.RISING_AURA) { return (int)((double)Database.RISING_AURA_COST * (1.00f - skillReduce)); }

            if (command == Database.RUMBLE_SHOUT) { return (int)((double)Database.RUMBLE_SHOUT_COST * (1.00f - skillReduce)); }
            if (command == Database.ONSLAUGHT_HIT) { return (int)((double)Database.ONSLAUGHT_HIT_COST * (1.00f - skillReduce)); }

            if (command == Database.SMOOTHING_MOVE) { return (int)((double)Database.SMOOTHING_MOVE_COST * (1.00f - skillReduce)); }
            if (command == Database.ASCENSION_AURA) { return (int)((double)Database.ASCENSION_AURA_COST * (1.00f - skillReduce)); }

            if (command == Database.FUTURE_VISION) { return (int)((double)Database.FUTURE_VISION_COST * (1.00f - skillReduce)); }
            if (command == Database.UNKNOWN_SHOCK) { return (int)((double)Database.UNKNOWN_SHOCK_COST * (1.00f - skillReduce)); }

            if (command == Database.REFLEX_SPIRIT) { return (int)((double)Database.REFLEX_SPIRIT_COST * (1.00f - skillReduce)); }
            if (command == Database.FATAL_BLOW) { return (int)((double)Database.FATAL_BLOW_COST * (1.00f - skillReduce)); }

            if (command == Database.SHARP_GLARE) { return (int)((double)Database.SHARP_GLARE_COST * (1.00f - skillReduce)); }
            if (command == Database.CONCUSSIVE_HIT) { return (int)((double)Database.CONCUSSIVE_HIT_COST * (1.00f - skillReduce)); }

            if (command == Database.TRUST_SILENCE) { return (int)((double)Database.TRUST_SILENCE_COST * (1.00f - skillReduce)); }
            if (command == Database.MIND_KILLING) { return (int)((double)Database.MIND_KILLING_COST * (1.00f - skillReduce)); }

            if (command == Database.SURPRISE_ATTACK) { return (int)((double)Database.SURPRISE_ATTACK_COST * (1.00f - skillReduce)); }
            if (command == Database.STANCE_OF_MYSTIC) { return (int)((double)Database.STANCE_OF_MYSTIC_COST * (1.00f - skillReduce)); }

            if (command == Database.PSYCHIC_WAVE) { return (int)((double)Database.PSYCHIC_WAVE_COST * (1.00f - skillReduce)); }
            if (command == Database.NOURISH_SENSE) { return (int)((double)Database.NOURISH_SENSE_COST * (1.00f - skillReduce)); }

            if (command == Database.RECOVER) { return (int)((double)Database.RECOVER_COST * (1.00f - skillReduce)); }
            if (command == Database.IMPULSE_HIT) { return (int)((double)Database.IMPULSE_HIT_COST * (1.00f - skillReduce)); }

            if (command == Database.VIOLENT_SLASH) { return (int)((double)Database.VIOLENT_SLASH_COST * (1.00f - skillReduce)); }
            if (command == Database.ONE_AUTHORITY) { return (int)((double)Database.ONE_AUTHORITY_COST * (1.00f - skillReduce)); }

            if (command == Database.OUTER_INSPIRATION) { return (int)((double)Database.OUTER_INSPIRATION_COST * (1.00f - skillReduce)); }
            if (command == Database.HARDEST_PARRY) { return (int)((double)Database.HARDEST_PARRY_COST * (1.00f - skillReduce)); }

            if (command == Database.STANCE_OF_SUDDENNESS) { return (int)((double)Database.STANCE_OF_SUDDENNESS_COST * (1.00f - skillReduce)); }
            if (command == Database.SOUL_EXECUTION) { return (int)((double)Database.SOUL_EXECUTION_COST * (1.00f - skillReduce)); }

            if (command == Database.ARCHETYPE_EIN) { return (int)((double)Database.ARCHITECT_EIN_COST * (1.00f - skillReduce)); }
            if (command == Database.ARCHETYPE_RANA) { return (int)((double)Database.ARCHITECT_RANA_COST * (1.00f - skillReduce)); }
            if (command == Database.ARCHETYPE_OL) { return (int)((double)Database.ARCHITECT_OL_COST * (1.00f - skillReduce)); }
            if (command == Database.ARCHETYPE_VERZE) { return (int)((double)Database.ARCHITECT_VERZE_COST * (1.00f - skillReduce)); }

            return 0;
        }

        // 誘惑による行動不可かどうかを判別
        public static bool IsTemptationEffect(string commandName)
        {
            if (commandName == Database.ATTACK_EN ||
                //commandName == Database.FRESH_HEAL ||
                commandName == Database.HOLY_SHOCK ||
                commandName == Database.CELESTIAL_NOVA || // [警告]敵味方選択性だがどうする？
                commandName == Database.DARK_BLAST ||
                commandName == Database.DEVOURING_PLAGUE ||
                commandName == Database.FIRE_BALL ||
                //commandName == Database.FLAME_AURA || // 追加効果もダメージ系とみなす
                commandName == Database.FLAME_STRIKE ||
                commandName == Database.VOLCANIC_WAVE ||
                commandName == Database.LAVA_ANNIHILATION ||
                commandName == Database.ICE_NEEDLE ||
                commandName == Database.FROZEN_LANCE ||
                commandName == Database.WORD_OF_POWER ||
                commandName == Database.WHITE_OUT ||
                commandName == Database.FLASH_BLAZE ||
                commandName == Database.LIGHT_DETONATOR ||
                commandName == Database.ASCENDANT_METEOR ||
                commandName == Database.STAR_LIGHTNING ||
                commandName == Database.BLACK_FIRE ||
                commandName == Database.DEMONIC_IGNITE ||
                commandName == Database.BLUE_BULLET ||
                commandName == Database.WORD_OF_MALICE ||
                commandName == Database.ABYSS_EYE ||
                commandName == Database.DOOM_BLADE ||
                //commandName == Database.FROZEN_AURA || // 追加効果もダメージ系とみなす
                commandName == Database.CHILL_BURN ||
                commandName == Database.ZETA_EXPLOSION ||
                commandName == Database.ENRAGE_BLAST ||
                commandName == Database.PIERCING_FLAME ||
                commandName == Database.IMMOLATE ||
                commandName == Database.VANISH_WAVE ||
                commandName == Database.STRAIGHT_SMASH ||
                commandName == Database.DOUBLE_SLASH ||
                commandName == Database.CRUSHING_BLOW ||
                commandName == Database.SOUL_INFINITY ||
                //commandName == Database.COUNTER_ATTACK ||
                commandName == Database.ENIGMA_SENSE ||
                commandName == Database.SILENT_RUSH ||
                commandName == Database.OBORO_IMPACT ||
                commandName == Database.STANCE_OF_STANDING ||
                commandName == Database.KINETIC_SMASH ||
                commandName == Database.CATASTROPHE ||
                commandName == Database.CARNAGE_RUSH ||
                commandName == Database.NEUTRAL_SMASH ||
                commandName == Database.CIRCLE_SLASH ||
                commandName == Database.ONSLAUGHT_HIT ||
                commandName == Database.FATAL_BLOW ||
                commandName == Database.CONCUSSIVE_HIT ||
                commandName == Database.MIND_KILLING ||
                commandName == Database.SURPRISE_ATTACK ||
                commandName == Database.IMPULSE_HIT ||
                commandName == Database.PSYCHIC_WAVE ||
                commandName == Database.VIOLENT_SLASH ||
                commandName == Database.SOUL_EXECUTION)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsHeal(string commandName)
        {
            if (commandName == Database.FRESH_HEAL ||
                commandName == Database.CELESTIAL_NOVA ||
                commandName == Database.LIFE_TAP ||
                commandName == Database.DEVOURING_PLAGUE ||
                commandName == Database.SACRED_HEAL)
            {
                return true;
            }

            return false;
        }

        // ダメージ系かどうかを判別
        public static bool IsDamage(string commandName)
        {
            if (commandName == Database.ATTACK_EN ||
                //commandName == Database.FRESH_HEAL || 回復系はダメージ系とみなさない（IsHealに移行）
                commandName == Database.HOLY_SHOCK ||
                commandName == Database.CELESTIAL_NOVA ||
                commandName == Database.DARK_BLAST ||
                commandName == Database.DEVOURING_PLAGUE ||
                commandName == Database.FIRE_BALL ||
                commandName == Database.FLAME_AURA || // 追加効果もダメージ系とみなす
                commandName == Database.FLAME_STRIKE ||
                commandName == Database.VOLCANIC_WAVE ||
                commandName == Database.LAVA_ANNIHILATION ||
                commandName == Database.ICE_NEEDLE ||
                commandName == Database.FROZEN_LANCE ||
                commandName == Database.WORD_OF_POWER ||
                commandName == Database.WHITE_OUT ||
                commandName == Database.FLASH_BLAZE ||
                commandName == Database.LIGHT_DETONATOR ||
                commandName == Database.ASCENDANT_METEOR ||
                commandName == Database.STAR_LIGHTNING ||
                commandName == Database.BLACK_FIRE ||
                commandName == Database.DEMONIC_IGNITE ||
                commandName == Database.BLUE_BULLET ||
                commandName == Database.WORD_OF_MALICE ||
                commandName == Database.ABYSS_EYE ||
                commandName == Database.DOOM_BLADE ||
                commandName == Database.FROZEN_AURA || // 追加効果もダメージ系とみなす
                commandName == Database.CHILL_BURN ||
                commandName == Database.ZETA_EXPLOSION ||
                commandName == Database.ENRAGE_BLAST ||
                commandName == Database.PIERCING_FLAME ||
                commandName == Database.IMMOLATE ||
                commandName == Database.VANISH_WAVE ||
                commandName == Database.STRAIGHT_SMASH ||
                commandName == Database.DOUBLE_SLASH ||
                commandName == Database.CRUSHING_BLOW ||
                commandName == Database.SOUL_INFINITY ||
                commandName == Database.COUNTER_ATTACK ||
                commandName == Database.ENIGMA_SENSE ||
                commandName == Database.SILENT_RUSH ||
                commandName == Database.OBORO_IMPACT ||
                commandName == Database.STANCE_OF_STANDING ||
                commandName == Database.KINETIC_SMASH ||
                commandName == Database.CATASTROPHE ||
                commandName == Database.CARNAGE_RUSH ||
                commandName == Database.NEUTRAL_SMASH ||
                commandName == Database.CIRCLE_SLASH ||
                commandName == Database.ONSLAUGHT_HIT ||
                commandName == Database.FATAL_BLOW ||
                commandName == Database.SHARP_GLARE ||
                commandName == Database.CONCUSSIVE_HIT ||
                commandName == Database.MIND_KILLING ||
                commandName == Database.SURPRISE_ATTACK ||
                commandName == Database.IMPULSE_HIT ||
                commandName == Database.PSYCHIC_WAVE ||
                commandName == Database.VIOLENT_SLASH ||
                commandName == Database.SOUL_EXECUTION ||
                commandName == Database.ARCHETYPE_EIN ||
                commandName == Database.ARCHETYPE_OL)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // 発動タイミングを判別
        public static TimingType GetTimingType(string command)
        {

            if (command == Database.ATTACK_EN) { return TimingType.Normal; }

            if (command == Database.FRESH_HEAL) { return TimingType.Instant; }
            if (command == Database.PROTECTION) { return TimingType.Instant; }
            if (command == Database.HOLY_SHOCK) { return TimingType.Instant; }
            if (command == Database.SAINT_POWER) { return TimingType.Instant; }
            if (command == Database.GLORY) { return TimingType.Instant; }
            if (command == Database.RESURRECTION) { return TimingType.Sorcery; }
            if (command == Database.CELESTIAL_NOVA) { return TimingType.Instant; }

            if (command == Database.DARK_BLAST) { return TimingType.Instant; }
            if (command == Database.SHADOW_PACT) { return TimingType.Instant; }
            if (command == Database.LIFE_TAP) { return TimingType.Instant; }
            if (command == Database.BLACK_CONTRACT) { return TimingType.Instant; }
            if (command == Database.DEVOURING_PLAGUE) { return TimingType.Instant; }
            if (command == Database.BLOODY_VENGEANCE) { return TimingType.Instant; }
            if (command == Database.DAMNATION) { return TimingType.Sorcery; }

            if (command == Database.FIRE_BALL) { return TimingType.Instant; }
            if (command == Database.FLAME_AURA) { return TimingType.Instant; }
            if (command == Database.HEAT_BOOST) { return TimingType.Instant; }
            if (command == Database.FLAME_STRIKE) { return TimingType.Instant; }
            if (command == Database.VOLCANIC_WAVE) { return TimingType.Instant; }
            if (command == Database.IMMORTAL_RAVE) { return TimingType.Instant; }
            if (command == Database.LAVA_ANNIHILATION) { return TimingType.Sorcery; }

            if (command == Database.ICE_NEEDLE) { return TimingType.Instant; }
            if (command == Database.ABSORB_WATER) { return TimingType.Instant; }
            if (command == Database.CLEANSING) { return TimingType.Instant; }
            if (command == Database.FROZEN_LANCE) { return TimingType.Instant; }
            if (command == Database.MIRROR_IMAGE) { return TimingType.Instant; }
            if (command == Database.PROMISED_KNOWLEDGE) { return TimingType.Instant; }
            if (command == Database.ABSOLUTE_ZERO) { return TimingType.Sorcery; }

            if (command == Database.WORD_OF_POWER) { return TimingType.Instant; }
            if (command == Database.GALE_WIND) { return TimingType.Instant; }
            if (command == Database.WORD_OF_LIFE) { return TimingType.Instant; }
            if (command == Database.WORD_OF_FORTUNE) { return TimingType.Instant; }
            if (command == Database.AETHER_DRIVE) { return TimingType.Instant; }
            if (command == Database.GENESIS) { return TimingType.Instant; }
            if (command == Database.ETERNAL_PRESENCE) { return TimingType.Instant; }

            if (command == Database.DISPEL_MAGIC) { return TimingType.Instant; }
            if (command == Database.RISE_OF_IMAGE) { return TimingType.Instant; }
            if (command == Database.DEFLECTION) { return TimingType.Instant; }
            if (command == Database.TRANQUILITY) { return TimingType.Instant; }
            if (command == Database.ONE_IMMUNITY) { return TimingType.Instant; }
            if (command == Database.WHITE_OUT) { return TimingType.Instant; }
            if (command == Database.TIME_STOP) { return TimingType.Instant; }

            if (command == Database.PSYCHIC_TRANCE) { return TimingType.Instant; }
            if (command == Database.BLIND_JUSTICE) { return TimingType.Instant; }
            if (command == Database.TRANSCENDENT_WISH) { return TimingType.Sorcery; }

            if (command == Database.FLASH_BLAZE) { return TimingType.Instant; }
            if (command == Database.LIGHT_DETONATOR) { return TimingType.Instant; }
            if (command == Database.ASCENDANT_METEOR) { return TimingType.Sorcery; }

            if (command == Database.SKY_SHIELD) { return TimingType.Sorcery; }
            if (command == Database.SACRED_HEAL) { return TimingType.Sorcery; }
            if (command == Database.EVER_DROPLET) { return TimingType.Instant; }

            if (command == Database.HOLY_BREAKER) { return TimingType.Sorcery; }
            if (command == Database.EXALTED_FIELD) { return TimingType.Sorcery; }
            if (command == Database.HYMN_CONTRACT) { return TimingType.Sorcery; }

            if (command == Database.STAR_LIGHTNING) { return TimingType.Sorcery; }
            if (command == Database.ANGEL_BREATH) { return TimingType.Sorcery; }
            if (command == Database.ENDLESS_ANTHEM) { return TimingType.Sorcery; }

            if (command == Database.BLACK_FIRE) { return TimingType.Instant; }
            if (command == Database.BLAZING_FIELD) { return TimingType.Sorcery; }
            if (command == Database.DEMONIC_IGNITE) { return TimingType.Instant; }

            if (command == Database.BLUE_BULLET) { return TimingType.Instant; }
            if (command == Database.DEEP_MIRROR) { return TimingType.Instant; }
            if (command == Database.DEATH_DENY) { return TimingType.Instant; }

            if (command == Database.WORD_OF_MALICE) { return TimingType.Instant; }
            if (command == Database.ABYSS_EYE) { return TimingType.Sorcery; }
            if (command == Database.SIN_FORTUNE) { return TimingType.Instant; }

            if (command == Database.DARKEN_FIELD) { return TimingType.Sorcery; }
            if (command == Database.DOOM_BLADE) { return TimingType.Instant; }
            if (command == Database.ECLIPSE_END) { return TimingType.Sorcery; }

            if (command == Database.FROZEN_AURA) { return TimingType.Instant; }
            if (command == Database.CHILL_BURN) { return TimingType.Instant; }
            if (command == Database.ZETA_EXPLOSION) { return TimingType.Sorcery; }

            if (command == Database.ENRAGE_BLAST) { return TimingType.Instant; }
            if (command == Database.PIERCING_FLAME) { return TimingType.Instant; }
            if (command == Database.SIGIL_OF_HOMURA) { return TimingType.Instant; }

            if (command == Database.IMMOLATE) { return TimingType.Instant; }
            if (command == Database.PHANTASMAL_WIND) { return TimingType.Instant; }
            if (command == Database.RED_DRAGON_WILL) { return TimingType.Instant; }

            if (command == Database.WORD_OF_ATTITUDE) { return TimingType.Instant; }
            if (command == Database.STATIC_BARRIER) { return TimingType.Sorcery; }
            if (command == Database.AUSTERITY_MATRIX) { return TimingType.Sorcery; }

            if (command == Database.VANISH_WAVE) { return TimingType.Instant; }
            if (command == Database.VORTEX_FIELD) { return TimingType.Sorcery; }
            if (command == Database.BLUE_DRAGON_WILL) { return TimingType.Instant; }

            if (command == Database.SEVENTH_MAGIC) { return TimingType.Instant; }
            if (command == Database.PARADOX_IMAGE) { return TimingType.Instant; }
            if (command == Database.WARP_GATE) { return TimingType.Instant; }
            // スキル
            if (command == Database.STRAIGHT_SMASH) { return TimingType.Instant; }
            if (command == Database.DOUBLE_SLASH) { return TimingType.Instant; }
            if (command == Database.CRUSHING_BLOW) { return TimingType.Sorcery; }
            if (command == Database.SOUL_INFINITY) { return TimingType.Sorcery; }

            if (command == Database.COUNTER_ATTACK) { return TimingType.Instant; }
            if (command == Database.PURE_PURIFICATION) { return TimingType.Instant; }
            if (command == Database.ANTI_STUN) { return TimingType.Instant; }
            if (command == Database.STANCE_OF_DEATH) { return TimingType.Instant; }

            if (command == Database.STANCE_OF_FLOW) { return TimingType.Instant; }
            if (command == Database.ENIGMA_SENSE) { return TimingType.Instant; }
            if (command == Database.SILENT_RUSH) { return TimingType.Instant; }
            if (command == Database.OBORO_IMPACT) { return TimingType.Sorcery; }

            if (command == Database.STANCE_OF_STANDING) { return TimingType.Instant; }
            if (command == Database.INNER_INSPIRATION) { return TimingType.Instant; }
            if (command == Database.KINETIC_SMASH) { return TimingType.Instant; }
            if (command == Database.CATASTROPHE) { return TimingType.Sorcery; }

            if (command == Database.TRUTH_VISION) { return TimingType.Instant; }
            if (command == Database.HIGH_EMOTIONALITY) { return TimingType.Instant; }
            if (command == Database.STANCE_OF_EYES) { return TimingType.Instant; }
            if (command == Database.PAINFUL_INSANITY) { return TimingType.Sorcery; }

            if (command == Database.NEGATE) { return TimingType.Instant; }
            if (command == Database.VOID_EXTRACTION) { return TimingType.Instant; }
            if (command == Database.CARNAGE_RUSH) { return TimingType.Sorcery; }
            if (command == Database.NOTHING_OF_NOTHINGNESS) { return TimingType.Sorcery; }

            if (command == Database.NEUTRAL_SMASH) { return TimingType.Instant; }
            if (command == Database.STANCE_OF_DOUBLE) { return TimingType.Sorcery; }

            if (command == Database.SWIFT_STEP) { return TimingType.Instant; }
            if (command == Database.VIGOR_SENSE) { return TimingType.Instant; }

            if (command == Database.CIRCLE_SLASH) { return TimingType.Sorcery; }
            if (command == Database.RISING_AURA) { return TimingType.Sorcery; }

            if (command == Database.RUMBLE_SHOUT) { return TimingType.Instant; }
            if (command == Database.ONSLAUGHT_HIT) { return TimingType.Instant; }

            if (command == Database.SMOOTHING_MOVE) { return TimingType.Sorcery; }
            if (command == Database.ASCENSION_AURA) { return TimingType.Sorcery; }

            if (command == Database.FUTURE_VISION) { return TimingType.Sorcery; }
            if (command == Database.UNKNOWN_SHOCK) { return TimingType.Sorcery; }

            if (command == Database.REFLEX_SPIRIT) { return TimingType.Instant; }
            if (command == Database.FATAL_BLOW) { return TimingType.Sorcery; }

            if (command == Database.SHARP_GLARE) { return TimingType.Instant; }
            if (command == Database.CONCUSSIVE_HIT) { return TimingType.Instant; }

            if (command == Database.TRUST_SILENCE) { return TimingType.Instant; }
            if (command == Database.MIND_KILLING) { return TimingType.Instant; }

            if (command == Database.SURPRISE_ATTACK) { return TimingType.Instant; }
            if (command == Database.STANCE_OF_MYSTIC) { return TimingType.Instant; }

            if (command == Database.PSYCHIC_WAVE) { return TimingType.Instant; }
            if (command == Database.NOURISH_SENSE) { return TimingType.Instant; }

            if (command == Database.RECOVER) { return TimingType.Instant; }
            if (command == Database.IMPULSE_HIT) { return TimingType.Instant; }

            if (command == Database.VIOLENT_SLASH) { return TimingType.Instant; }
            if (command == Database.ONE_AUTHORITY) { return TimingType.Sorcery; }

            if (command == Database.OUTER_INSPIRATION) { return TimingType.Instant; }
            if (command == Database.HARDEST_PARRY) { return TimingType.Instant; }

            if (command == Database.STANCE_OF_SUDDENNESS) { return TimingType.Instant; }
            if (command == Database.SOUL_EXECUTION) { return TimingType.Sorcery; }

            if (command == Database.ARCHETYPE_EIN) { return TimingType.Instant; }
            if (command == Database.ARCHETYPE_RANA) { return TimingType.Instant; }
            if (command == Database.ARCHETYPE_OL) { return TimingType.Instant; }
            if (command == Database.ARCHETYPE_VERZE) { return TimingType.Instant; }

            return TimingType.Normal;
        }

        // 対象タイプを判別
        public static TargetType GetTargetType(string command)
        {
            if (command == Database.ATTACK_EN) { return TargetType.Enemy; }
            if (command == Database.DEFENSE_EN) { return TargetType.Own; }
            if (command == Database.TAMERU_EN) { return TargetType.Own; }
            if (command == Database.STAY_EN) { return TargetType.Own; }

            if (command == Database.FRESH_HEAL) { return TargetType.Ally; }
            if (command == Database.PROTECTION) { return TargetType.Ally; }
            if (command == Database.HOLY_SHOCK) { return TargetType.Enemy; }
            if (command == Database.SAINT_POWER) { return TargetType.Ally; }
            if (command == Database.GLORY) { return TargetType.Own; }
            if (command == Database.RESURRECTION) { return TargetType.Ally; }
            if (command == Database.CELESTIAL_NOVA) { return TargetType.AllyOrEnemy; }

            if (command == Database.DARK_BLAST) { return TargetType.Enemy; }
            if (command == Database.SHADOW_PACT) { return TargetType.Ally; }
            if (command == Database.LIFE_TAP) { return TargetType.Ally; }
            if (command == Database.BLACK_CONTRACT) { return TargetType.Own; }
            if (command == Database.DEVOURING_PLAGUE) { return TargetType.Enemy; }
            if (command == Database.BLOODY_VENGEANCE) { return TargetType.Ally; }
            if (command == Database.DAMNATION) { return TargetType.Enemy; }

            if (command == Database.FIRE_BALL) { return TargetType.Enemy; }
            if (command == Database.FLAME_AURA) { return TargetType.Ally; }
            if (command == Database.HEAT_BOOST) { return TargetType.Ally; }
            if (command == Database.FLAME_STRIKE) { return TargetType.Enemy; }
            if (command == Database.VOLCANIC_WAVE) { return TargetType.Enemy; }
            if (command == Database.IMMORTAL_RAVE) { return TargetType.Own; }
            if (command == Database.LAVA_ANNIHILATION) { return TargetType.EnemyGroup; }

            if (command == Database.ICE_NEEDLE) { return TargetType.Enemy; }
            if (command == Database.ABSORB_WATER) { return TargetType.Ally; }
            if (command == Database.CLEANSING) { return TargetType.Ally; }
            if (command == Database.FROZEN_LANCE) { return TargetType.Enemy; }
            if (command == Database.MIRROR_IMAGE) { return TargetType.Ally; }
            if (command == Database.PROMISED_KNOWLEDGE) { return TargetType.Ally; }
            if (command == Database.ABSOLUTE_ZERO) { return TargetType.Enemy; }

            if (command == Database.WORD_OF_POWER) { return TargetType.Enemy; }
            if (command == Database.GALE_WIND) { return TargetType.Own; }
            if (command == Database.WORD_OF_LIFE) { return TargetType.Ally; }
            if (command == Database.WORD_OF_FORTUNE) { return TargetType.Ally; }
            if (command == Database.AETHER_DRIVE) { return TargetType.Own; }
            if (command == Database.GENESIS) { return TargetType.Own; }
            if (command == Database.ETERNAL_PRESENCE) { return TargetType.Ally; }

            if (command == Database.DISPEL_MAGIC) { return TargetType.Enemy; }
            if (command == Database.RISE_OF_IMAGE) { return TargetType.Ally; }
            if (command == Database.DEFLECTION) { return TargetType.Ally; }
            if (command == Database.TRANQUILITY) { return TargetType.Enemy; }
            if (command == Database.ONE_IMMUNITY) { return TargetType.Own; }
            if (command == Database.WHITE_OUT) { return TargetType.Enemy; }
            if (command == Database.TIME_STOP) { return TargetType.AllMember; }

            if (command == Database.PSYCHIC_TRANCE) { return TargetType.Ally; }
            if (command == Database.BLIND_JUSTICE) { return TargetType.Ally; }
            if (command == Database.TRANSCENDENT_WISH) { return TargetType.Own; }

            if (command == Database.FLASH_BLAZE) { return TargetType.Enemy; }
            if (command == Database.LIGHT_DETONATOR) { return TargetType.EnemyGroup; }
            if (command == Database.ASCENDANT_METEOR) { return TargetType.Enemy; }

            if (command == Database.SKY_SHIELD) { return TargetType.Ally; }
            if (command == Database.SACRED_HEAL) { return TargetType.AllyGroup; }
            if (command == Database.EVER_DROPLET) { return TargetType.Ally; }

            if (command == Database.HOLY_BREAKER) { return TargetType.Ally; }
            if (command == Database.EXALTED_FIELD) { return TargetType.AllyGroup; }
            if (command == Database.HYMN_CONTRACT) { return TargetType.Own; }

            if (command == Database.STAR_LIGHTNING) { return TargetType.Enemy; }
            if (command == Database.ANGEL_BREATH) { return TargetType.AllyGroup; }
            if (command == Database.ENDLESS_ANTHEM) { return TargetType.AllyGroup; }

            if (command == Database.BLACK_FIRE) { return TargetType.Enemy; }
            if (command == Database.BLAZING_FIELD) { return TargetType.EnemyGroup; }
            if (command == Database.DEMONIC_IGNITE) { return TargetType.Enemy; }

            if (command == Database.BLUE_BULLET) { return TargetType.Enemy; }
            if (command == Database.DEEP_MIRROR) { return TargetType.InstantTarget; }
            if (command == Database.DEATH_DENY) { return TargetType.Ally; }

            if (command == Database.WORD_OF_MALICE) { return TargetType.Enemy; }
            if (command == Database.ABYSS_EYE) { return TargetType.Enemy; }
            if (command == Database.SIN_FORTUNE) { return TargetType.Own; }

            if (command == Database.DARKEN_FIELD) { return TargetType.EnemyGroup; }
            if (command == Database.DOOM_BLADE) { return TargetType.Enemy; }
            if (command == Database.ECLIPSE_END) { return TargetType.AllMember; }

            if (command == Database.FROZEN_AURA) { return TargetType.Ally; }
            if (command == Database.CHILL_BURN) { return TargetType.Enemy; }
            if (command == Database.ZETA_EXPLOSION) { return TargetType.Enemy; }

            if (command == Database.ENRAGE_BLAST) { return TargetType.EnemyGroup; }
            if (command == Database.PIERCING_FLAME) { return TargetType.Enemy; }
            if (command == Database.SIGIL_OF_HOMURA) { return TargetType.Enemy; }

            if (command == Database.IMMOLATE) { return TargetType.Enemy; }
            if (command == Database.PHANTASMAL_WIND) { return TargetType.Ally; }
            if (command == Database.RED_DRAGON_WILL) { return TargetType.Ally; }

            if (command == Database.WORD_OF_ATTITUDE) { return TargetType.Ally; }
            if (command == Database.STATIC_BARRIER) { return TargetType.Ally; }
            if (command == Database.AUSTERITY_MATRIX) { return TargetType.Enemy; }

            if (command == Database.VANISH_WAVE) { return TargetType.Enemy; }
            if (command == Database.VORTEX_FIELD) { return TargetType.EnemyGroup; }
            if (command == Database.BLUE_DRAGON_WILL) { return TargetType.Ally; }

            if (command == Database.SEVENTH_MAGIC) { return TargetType.Own; }
            if (command == Database.PARADOX_IMAGE) { return TargetType.Own; }
            if (command == Database.WARP_GATE) { return TargetType.Own; }
            // スキル
            if (command == Database.STRAIGHT_SMASH) { return TargetType.Enemy; }
            if (command == Database.DOUBLE_SLASH) { return TargetType.Enemy; }
            if (command == Database.CRUSHING_BLOW) { return TargetType.Enemy; }
            if (command == Database.SOUL_INFINITY) { return TargetType.Enemy; }

            if (command == Database.COUNTER_ATTACK) { return TargetType.Enemy; }
            if (command == Database.PURE_PURIFICATION) { return TargetType.Own; }
            if (command == Database.ANTI_STUN) { return TargetType.Own; }
            if (command == Database.STANCE_OF_DEATH) { return TargetType.Own; }

            if (command == Database.STANCE_OF_FLOW) { return TargetType.Own; }
            if (command == Database.ENIGMA_SENSE) { return TargetType.Enemy; }
            if (command == Database.SILENT_RUSH) { return TargetType.Enemy; }
            if (command == Database.OBORO_IMPACT) { return TargetType.Enemy; }

            if (command == Database.STANCE_OF_STANDING) { return TargetType.Enemy; }
            if (command == Database.INNER_INSPIRATION) { return TargetType.Own; }
            if (command == Database.KINETIC_SMASH) { return TargetType.Enemy; }
            if (command == Database.CATASTROPHE) { return TargetType.Enemy; }

            if (command == Database.TRUTH_VISION) { return TargetType.Own; }
            if (command == Database.HIGH_EMOTIONALITY) { return TargetType.Own; }
            if (command == Database.STANCE_OF_EYES) { return TargetType.Enemy; }
            if (command == Database.PAINFUL_INSANITY) { return TargetType.Own; }

            if (command == Database.NEGATE) { return TargetType.Enemy; }
            if (command == Database.VOID_EXTRACTION) { return TargetType.Own; }
            if (command == Database.CARNAGE_RUSH) { return TargetType.Enemy; }
            if (command == Database.NOTHING_OF_NOTHINGNESS) { return TargetType.Own; }

            if (command == Database.NEUTRAL_SMASH) { return TargetType.Enemy; }
            if (command == Database.STANCE_OF_DOUBLE) { return TargetType.Own; }

            if (command == Database.SWIFT_STEP) { return TargetType.Own; }
            if (command == Database.VIGOR_SENSE) { return TargetType.Own; }

            if (command == Database.CIRCLE_SLASH) { return TargetType.EnemyGroup; }
            if (command == Database.RISING_AURA) { return TargetType.AllyGroup; }

            if (command == Database.RUMBLE_SHOUT) { return TargetType.Enemy; }
            if (command == Database.ONSLAUGHT_HIT) { return TargetType.Enemy; }

            if (command == Database.SMOOTHING_MOVE) { return TargetType.Enemy; }
            if (command == Database.ASCENSION_AURA) { return TargetType.AllyGroup; }

            if (command == Database.FUTURE_VISION) { return TargetType.Own; }
            if (command == Database.UNKNOWN_SHOCK) { return TargetType.EnemyGroup; }

            if (command == Database.REFLEX_SPIRIT) { return TargetType.Own; }
            if (command == Database.FATAL_BLOW) { return TargetType.Enemy; }

            if (command == Database.SHARP_GLARE) { return TargetType.Enemy; }
            if (command == Database.CONCUSSIVE_HIT) { return TargetType.Enemy; }

            if (command == Database.TRUST_SILENCE) { return TargetType.Own; }
            if (command == Database.MIND_KILLING) { return TargetType.Enemy; }

            if (command == Database.SURPRISE_ATTACK) { return TargetType.Enemy; }
            if (command == Database.STANCE_OF_MYSTIC) { return TargetType.Own; }

            if (command == Database.PSYCHIC_WAVE) { return TargetType.Enemy; }
            if (command == Database.NOURISH_SENSE) { return TargetType.Own; }

            if (command == Database.RECOVER) { return TargetType.Ally; }
            if (command == Database.IMPULSE_HIT) { return TargetType.Enemy; }

            if (command == Database.VIOLENT_SLASH) { return TargetType.Enemy; }
            if (command == Database.ONE_AUTHORITY) { return TargetType.AllyGroup; }

            if (command == Database.OUTER_INSPIRATION) { return TargetType.Own; }
            if (command == Database.HARDEST_PARRY) { return TargetType.InstantTarget; }

            if (command == Database.STANCE_OF_SUDDENNESS) { return TargetType.InstantTarget; }
            if (command == Database.SOUL_EXECUTION) { return TargetType.Enemy; }

            if (command == Database.ARCHETYPE_EIN) { return TargetType.Own; }
            if (command == Database.ARCHETYPE_RANA) { return TargetType.AllyGroup; }
            if (command == Database.ARCHETYPE_OL) { return TargetType.EnemyGroup; }
            if (command == Database.ARCHETYPE_VERZE) { return TargetType.InstantTarget; }

            // アイテム
            if (command == Database.RARE_RISING_KNUCKLE) { return TargetType.Enemy; }
            if (command == Database.RARE_ICE_SWORD) { return TargetType.Enemy; }
            if (command == Database.RARE_AERO_BLADE) { return TargetType.Enemy; }
            if (command == Database.RARE_LIFE_SWORD) { return TargetType.Own; }
            if (command == Database.COMMON_HAYATE_ORB) { return TargetType.Own; }
            if (command == Database.RARE_AUTUMN_ROD) { return TargetType.Own; }
            if (command == Database.RARE_BLUE_LIGHTNING) { return TargetType.Enemy; }
            if (command == Database.RARE_BURNING_CLAYMORE) { return TargetType.Own; }
            if (command == Database.COMMON_CHIENOWA_RING) { return TargetType.Own; }
            if (command == Database.COMMON_ROCKET_DASH) { return TargetType.Own; }
            if (command == Database.COMMON_WAR_DRUM) { return TargetType.AllyGroup; }
            if (command == Database.RARE_ROD_OF_STRENGTH) { return TargetType.Own; }
            if (command == Database.RARE_WRATH_SERVEL_CLAW) { return TargetType.Enemy; }
            if (command == Database.RARE_BLUE_RED_ROD) { return TargetType.Enemy; } // マナ回復自分自身はココから指定ではなく、直接コード記述となっている。
            if (command == Database.RARE_MEIUN_BOX) { return TargetType.Own; }
            if (command == Database.RARE_FROZEN_LAVA) { return TargetType.Enemy; }
            if (command == Database.COMMON_STAR_DUST_RING) { return TargetType.Enemy; }

            return TargetType.NoTarget;
        }

        // 解説部
        public static string PowerResult(string factor, double power, int min, int max)
        {
            if (min == 0 && max == 0)
            {
                return "《" + factor + "ｘ" + power.ToString("0.0")+"》";
            }
            else
            {
                return "《" + factor + "ｘ" + power.ToString("0.0") + " + (" + min + "～" + max + ")》";
            }
        }
        public static string PowerResult(string factor, double power)
        {
            return "《" + factor + "ｘ" + power.ToString("0.0") + "》";
        }
        public static string PowerResult(double power)
        {
            return power.ToString("0.0") + "倍";
        }
        public static string TurnPlusBuff()
        {
            return "Glory、GaleWind、WordOfFortune、BlackContract、HymnContract、ImmortalRave、AetherDrive、OneImmunity、HighEmotionality、StanceOfFlow、StanceOfDouble、SwiftStep、VigorSense、SmoothingMove、FutureVision、OneAuthority";
        }
        public static string MinusBuff()
        {
            return "【恐怖】【スタン】【沈黙】【猛毒】【誘惑】【凍結】【麻痺】【スロウ】【暗闇】【スリップ】";
        }
        public static string GetDescription(string command)
        {
            if (command == Database.ATTACK_EN) { return "敵対象：通常攻撃を行う。"; }
            if (command == Database.DEFENSE_EN) { return "自分対象：防御体制をとる。"; }
            if (command == Database.STAY_EN) { return "待機体制をとる。"; }
            if (command == Database.WEAPON_SPECIAL_EN) { return "武器（メイン）に付随している能力を発揮する。\n武器（メイン）に【特殊能力：有】がある時、使用可能。"; }
            if (command == Database.WEAPON_SPECIAL_LEFT_EN) { return "武器（サブ）に付随している能力を発揮する。\n武器（サブ）に【特殊能力：有】がある時、使用可能。"; }
            if (command == Database.TAMERU_EN) { return "自分対象：魔力をためる。\n武器（メイン）に杖を装備している時、使用可能。"; }
            if (command == Database.ACCESSORY_SPECIAL_EN) { return "アクセサリ【１】に付随している能力を発揮する。"; }
            if (command == Database.ACCESSORY_SPECIAL2_EN) { return "アクセサリ【２】に付随している能力を発揮する。"; }
            if (command == Database.ARCHETYPE_COMMAND) { return "潜在奥義【元核】を発動する。"; }

            if (command == Database.FRESH_HEAL) { return "味方対象：対象のライフを"+PowerResult("知", 4.0, 40, 50)+"の分だけ回復する。"; }
            if (command == Database.PROTECTION) { return "味方対象：対象の物理防御力を" + PowerResult(1.5) + "上昇させる。"; }
            if (command == Database.HOLY_SHOCK) { return "敵対象：対象に" + PowerResult("知", 2.2, 120, 135) + "の【聖】ダメージを与える。"; }
            if (command == Database.SAINT_POWER) { return "味方対象：対象の物理攻撃を"+PowerResult(1.5)+"上昇させる。"; }
            if (command == Database.GLORY) { return "自分対象：本効果が持続している間、直接攻撃する度に自分にFreshHealをかける。FreshHealの消費コストは０である。"; }
            if (command == Database.RESURRECTION) { return "味方対象：対象をライフ１/２の状態で蘇生する。"; }
            if (command == Database.CELESTIAL_NOVA) { return "敵または味方対象：対象が敵の場合、対象に" + PowerResult("知", 4.5, 4000, 5000) + "の【聖】ダメージを与える。対象が味方の場合、対象のライフを" + PowerResult("知", 5.0, 8000, 10000) + "回復する。"; }

            if (command == Database.DARK_BLAST) { return "敵対象：対象に"+PowerResult("知", 2.6, 30, 35)+"の【闇】ダメージを与える。"; }
            if (command == Database.SHADOW_PACT) { return "味方対象：魔法攻撃力を"+PowerResult(1.5)+"上昇させる。"; }
            if (command == Database.LIFE_TAP) { return "味方対象：対象のライフを"+PowerResult("知", 4.0, 40, 50)+"の分だけ回復する。"; }
            if (command == Database.BLACK_CONTRACT) { return "自分対象：本効果が持続している間、ターンが進む度に、10%のライフを失う。\r\n\r\nスキル、魔法の発動コストを０にする。"; }
            if (command == Database.DEVOURING_PLAGUE) { return "敵対象：敵に"+PowerResult("知", 2.0, 120, 135)+"の【闇】ダメージあたえ、その分だけ自分のライフを回復する。"; }
            if (command == Database.BLOODY_VENGEANCE) { return "味方対象：【力】パラメタを" + PowerResult("知", 0.5) + "の分だけ上昇させる。"; }
            if (command == Database.DAMNATION) { return "敵対象：ターンが進む度に、対象に《最大ライフ／心》の【闇】ダメージを与える。\r\n心が1以上：10 + 10*(心/100)\r\n心が100以上：20 + 20*(心/300)\r\n心が400以上：34 + 18*(心/600)\r\n心が1000以上：52 + 22*(心/2500)\r\n心が3500以上：74 + 26*(心/6500)\r\n"; }

            if (command == Database.FIRE_BALL) { return "敵対象：対象に" + PowerResult("知", 3.0, 30, 35) + "の【火】ダメージを与える。"; }
            if (command == Database.FLAME_AURA) { return "味方対象：直接攻撃がヒットする度に、追加効果で" + PowerResult("知", 3.0, 30, 35) + "の【火】ダメージを与える。"; }
            if (command == Database.HEAT_BOOST) { return "味方対象：【技】パラメタを" + PowerResult("知", 0.5) + "の分だけ上昇させる。"; }
            if (command == Database.FLAME_STRIKE) { return "敵対象：対象に" + PowerResult("知", 3.5, 750, 1000) + "の【火】ダメージを与える。"; }
            if (command == Database.VOLCANIC_WAVE) { return "敵対象：対象に" + PowerResult("知", 4.0, 1200, 1600) + "の【火】ダメージを与える。"; }
            if (command == Database.IMMORTAL_RAVE) { return "自分対象：本効果が持続している間、直接攻撃＋1ターン目はFireBall、２ターン目はFlameStrike、3ターン目はVolcanicWaveをコスト０で行う。"; }
            if (command == Database.LAVA_ANNIHILATION) { return "敵全体：" + PowerResult("知", 5.0, 7000, 8000) + "の【火】ダメージを与える。"; }

            if (command == Database.ICE_NEEDLE) { return "敵対象：対象に" + PowerResult("知", 2.8, 30, 35) + "の【水】ダメージを与える。"; }
            if (command == Database.ABSORB_WATER) { return "味方対象：魔法防御力を" + PowerResult(1.3) + "上昇させる。"; }
            if (command == Database.CLEANSING) { return "味方対象：負のＢＵＦＦ効果を全て解除する。\r\n\r\n負のＢＵＦＦ効果には以下が含まれる。\r\n" + MinusBuff(); }
            if (command == Database.FROZEN_LANCE) { return "敵対象：対象に" + PowerResult("知", 3.3, 750, 1000) + "の【水】ダメージを与える。"; }
            if (command == Database.MIRROR_IMAGE) { return "味方対象：ダメージ源を有する魔法攻撃が向けられた場合、それを反射する。ただし、WordOfPowerは反射できない。"; }
            if (command == Database.PROMISED_KNOWLEDGE) { return "味方対象：【知】パラメタを" + PowerResult("知", 0.5) + "上昇させる。"; }
            if (command == Database.ABSOLUTE_ZERO) { return "敵対象：本効果が持続している間、対象はライフ・マナ・スキル回復不可、スペル詠唱不可、スキル使用不可、防御不可となる。"; }

            if (command == Database.WORD_OF_POWER) { return "敵対象：対象の防御を無視した上で、対象に" + PowerResult("力", 2.4, 30, 35) + "の【理】ダメージを与える。\r\nこの魔法は物理ダメージとして扱われる。\r\nこの魔法はMirrorImageの対象とならない。\r\nこの魔法はカウンターされない。"; }
            if (command == Database.GALE_WIND) { return "自分対象：本効果が持続している間、行動を行う際、同一コマンドを連続で２回行動する。\r\nこの効果は、メイン行動／インスタント行動のいずれにも適用される。"; }
            if (command == Database.WORD_OF_LIFE) { return "味方対象：ターンが進む度に、ライフを" + PowerResult("知", 1.0, 30, 35) + "＋" + PowerResult("心", 1.0, 0, 0) + "の分だけ回復する。\r\n\r\nこの回復量は潜在能力値に応じて増幅する。"; }
            if (command == Database.WORD_OF_FORTUNE) { return "味方対象：本効果が持続している間、必ずクリティカルヒットが発生する。\r\nこの効果は物理／魔法のいずれにも適用される。"; }
            if (command == Database.AETHER_DRIVE) { return "自分対象：本効果が持続している間、自分に対する物理ダメージを半減させる。\r\nこれに加え、敵に対する物理ダメージを２倍にする。"; }
            if (command == Database.GENESIS) { return "自分対象：前回とった行動と同じ行動を行う。前回消費した魔法／スキルのコストは０として扱われる。"; }
            if (command == Database.ETERNAL_PRESENCE) { return "味方対象：物理攻撃力、物理防御力、魔法攻撃力、魔法防御力をそれぞれ" + PowerResult(1.3) + "上昇させる。"; }

            if (command == Database.DISPEL_MAGIC) { return "敵対象：正のＢＵＦＦ効果を全て解除する。\r\n\r\n正のＢＵＦＦ効果には以下が含まれる。\r\nProtection、SaintPower、AbsorbWater、ShadowPact、EternalPresence、BloodyVengeance、HeatBoost、PromisedKnowledge、RiseOfImage、WordOfLife、FlameAura、PsychicTrance、BlindJustice、SkyShield、EverDroplet、HolyBreaker、ExaltedField、FrozenAura、PhantasmalWind、RedDragonWill、StaticBarrier、BlueDragonWill、SeventhMagic、ParadoxImage"; }
            if (command == Database.RISE_OF_IMAGE) { return "味方対象：心パラメタを" + PowerResult("知", 0.5) + "上昇させる。"; }
            if (command == Database.DEFLECTION) { return "味方対象：ダメージ源を有する物理攻撃が向けられた場合、それを反射する。ただし、WordOfPowerは反射できない。"; }
            if (command == Database.TRANQUILITY) { return "敵対象：効果継続性のある正のＢＵＦＦ効果を全て解除する。\r\n\r\n効果継続性のある正のＢＵＦＦ効果には以下が含まれる。\r\n" + TurnPlusBuff(); }
            if (command == Database.ONE_IMMUNITY) { return "自分対象：本効果が持続している間、防御している間、全ダメージを無効化する。\r\nただし、WordOfPowerとPsychicWaveはダメージを無効化する事はできない。"; }
            if (command == Database.WHITE_OUT) { return "敵対象：対象に" + PowerResult("知", 3.8, 1200, 1600) + "の【空】ダメージを与える。"; }
            if (command == Database.TIME_STOP) { return "敵味方対象：本効果が持続している間、自分を除く全員の行動を停止させる。\r\n本効果は１ターン持続される。"; }

            if (command == Database.PSYCHIC_TRANCE) { return "味方対象：魔法攻撃力を" + PowerResult(1.7) + "上昇させ、魔法防御力を" + PowerResult(0.7) + "に減少させる。"; }
            if (command == Database.BLIND_JUSTICE) { return "味方対象：物理攻撃力を" + PowerResult(1.7) + "上昇させ、物理防御力を" + PowerResult(0.7) + "に減少させる。"; }
            if (command == Database.TRANSCENDENT_WISH) { return "自分対象：本効果が持続している間、力/技/知/体/心パラメタをそれぞれ" + PowerResult(1.5) + "上昇させる。本効果が切れた場合、対象者は死亡する。"; }

            if (command == Database.FLASH_BLAZE) { return "敵対象：対象に" + PowerResult("知", 2.0, 200, 300) + "の【聖/火】ダメージを与える。そして、対象者に【フラッシュブレイズ】カウンターが置かれる。\r\n\r\nターン進行により【フラッシュブレイズ】カウンターが取り除かれた時、対象に" + PowerResult("知", 2.0, 200, 300) + "の【聖/火】ダメージを与える。"; }
            if (command == Database.LIGHT_DETONATOR) { return "敵全体：対象者全員に" + PowerResult("知", 3.0, 750, 1000) + "の【聖/火】ダメージを与える。"; }
            if (command == Database.ASCENDANT_METEOR) { return "敵対象：対象に" + PowerResult("知", 1.5, 2000, 3000) + "の【聖/火】ダメージを１０回連続で与える。"; }

            if (command == Database.SKY_SHIELD) { return "味方対象：ダメージ源を有する魔法攻撃が向けられた場合、その魔法ダメージを０に軽減する。この効果は、３回分まで累積が可能である。"; }
            if (command == Database.SACRED_HEAL) { return "味方全体：対象者全員のライフを" + PowerResult("知", 3.5, 4000, 6000) + "の分だけ回復する。"; }
            if (command == Database.EVER_DROPLET) { return "味方対象：ターンが進む度に、対象のマナポイントを最大マナの1/30だけ回復する。"; }

            if (command == Database.HOLY_BREAKER) { return "味方対象：対象者が物理ダメージを受けた場合、それと同等の分だけのダメージを発生させた者に与える。"; }
            if (command == Database.EXALTED_FIELD) { return "味方全体：対象者全員の物理防御力と魔法防御力を" + PowerResult(1.4) + "上昇させる。"; }
            if (command == Database.HYMN_CONTRACT) { return "自分対象：本効果が持続している間、ターンが進む度に、10%のライフを失う。\r\n\r\n魔法／スキルの発動はカウンターされなくなる。"; }

            if (command == Database.STAR_LIGHTNING) { return "敵対象：対象に" + PowerResult("知", 1.0, 200, 300) + "の【聖/空】ダメージを与える。\r\n加えて、対象者に【スタン】効果を与える。\r\n本効果は１ターン持続される。"; }
            if (command == Database.ANGEL_BREATH) { return "味方全体：対象者全員の負のＢＵＦＦ効果を全て解除する。\r\n\r\n負のＢＵＦＦ効果には以下が含まれる。\r\n" + MinusBuff(); }
            if (command == Database.ENDLESS_ANTHEM) { return "味方全体：効果継続性のある正のＢＵＦＦ効果を、もう１ターン追加で継続させる。\r\n\r\n効果継続性のある正のＢＵＦＦ効果には以下が含まれる。\r\n" + TurnPlusBuff(); }

            if (command == Database.BLACK_FIRE) { return "敵対象：対象に" + PowerResult("知", 1.0, 200, 300) + "の【闇/火】ダメージを与える。加えて、対象の魔法防御力を" + PowerResult(0.8) + "に減少させる。"; }
            if (command == Database.BLAZING_FIELD) { return "敵全体：ターンが進む度に、対象者全員に" + PowerResult("知", 2.5, 1200, 1500) + "の【闇/火】ダメージを与える。"; }
            if (command == Database.DEMONIC_IGNITE) { return "敵対象：対象に" + PowerResult("知", 4.5, 6000, 7000) + "の【闇/火】ダメージを与える。加えて、対象者に【ライフ回復不可】効果を与える。本効果は１ターン持続される。"; }

            if (command == Database.BLUE_BULLET) { return "敵対象：対象に" + PowerResult("知", 0.9, 200, 300) + "の【水/闇】ダメージを３回連続で与える。"; }
            if (command == Database.DEEP_MIRROR) { return "インスタント対象：対象のインスタントがダメージ源を有しない魔法の場合、それをカウンターする。"; }
            if (command == Database.DEATH_DENY) { return "味方対象：対象をライフ／マナ／スキルが全快の状態で蘇生する。加えて、対象者に【蘇生不可】効果を与える。"; }

            if (command == Database.WORD_OF_MALICE) { return "敵対象：対象に" + PowerResult("知", 1.0, 200, 300) + "の【闇/理】ダメージを与える。加えて、対象の戦闘反応力を" + PowerResult(0.7) + "に減少させる。"; }
            if (command == Database.ABYSS_EYE) { return "敵対象：対象を７０％の確率で死亡させる。\r\nそうでない場合、対象に" + PowerResult("知", 2.8, 4500, 6000) + "の【闇/理】ダメージを与える。"; }
            if (command == Database.SIN_FORTUNE) { return "自分対象：次の行動時においてクリティカルダメージが発生した場合、そのクリティカルダメージ値を１．５倍にする。\r\nこの効果は一度発動すると解除される。"; }

            if (command == Database.DARKEN_FIELD) { return "敵全体：対象者全員の物理防御力と魔法防御力を" + PowerResult(0.8) + "に減少させる。"; }
            if (command == Database.DOOM_BLADE) { return "敵対象：対象に" + PowerResult("知", 2.8, 2000, 3500) + "の【闇/空】ダメージを与える。\r\n加えて、対象に" + PowerResult("知", 1.5, 2000, 3000) + "のＭＰダメージを与える。"; }
            if (command == Database.ECLIPSE_END) { return "敵味方全体：対象者全員の全BUFF、全DEBUFF効果を解除する。そして、【エクリプスエンド】カウンターが置かれる。\r\n\r\n本効果が持続している間、全ダメージを無効化する。本効果は２ターン持続される。"; }

            if (command == Database.FROZEN_AURA) { return "味方対象：直接攻撃がヒットする度に、追加効果で" + PowerResult("知", 2.8, 30, 35) + "の【水】ダメージを与える。"; }
            if (command == Database.CHILL_BURN) { return "敵対象：対象に" + PowerResult("知", 1.0, 0, 0) + "の【火/水】ダメージを与える。\r\n加えて、対象者に【凍結】効果を与える。本効果は１ターン持続される。"; }
            if (command == Database.ZETA_EXPLOSION) { return "敵対象：対象に" + PowerResult("知", 6.0, 8000, 12000) +"の【火／水】ダメージを与える。\r\n対象の防御姿勢を無視する。\r\nこの魔法はカウンターされない。\r\nこの魔法は軽減できない。"; }

            if (command == Database.ENRAGE_BLAST) { return "敵全体：対象者全員に" + PowerResult("知", 1.0, 200, 300) + "の【火/理】ダメージを与える。\r\nその後、ターンが進む度に、対象者全員に" + PowerResult("知", 1.0, 200, 300) + "の【火/理】ダメージを与える。"; }
            if (command == Database.PIERCING_FLAME) { return "敵対象：対象に" + PowerResult("知", 4.5, 3500, 5000) + "の【火/理】ダメージを与える。\r\n対象の防御姿勢を無視する。\r\nこの魔法は軽減できない。"; }
            if (command == Database.SIGIL_OF_HOMURA) { return "敵対象：対象に【焔の刻印】カウンターを置く。\r\n\r\n【焔の刻印】カウンターを置かれた者は、【火】属性のダメージを受けた場合、加えて、そのダメージと同等の追加ダメージを受ける。\r\n\r\n火属性防御による軽減が適用されない。\r\n\r\nこのBUFF効果は解除できない。"; }

            if (command == Database.IMMOLATE) { return "敵対象：対象に" + PowerResult("知", 1.0, 200, 300) + "の【火/空】ダメージを与える。加えて、対象の物理防御力を" + PowerResult(0.8) + "に減少させる。"; }
            if (command == Database.PHANTASMAL_WIND) { return "味方対象：対象の戦闘反応力を" + PowerResult(1.2) + "上昇させる。"; }
            if (command == Database.RED_DRAGON_WILL) { return "味方対象：【火】属性のダメージを" + PowerResult(1.5) + "上昇させる。"; }

            if (command == Database.WORD_OF_ATTITUDE) { return "味方対象：対象のインスタントポイントを全快にする。"; }
            if (command == Database.STATIC_BARRIER) { return "味方対象：ダメージ源を有する物理攻撃または魔法攻撃が向けられた場合、そのダメージを半分にする。この効果は３回まで累積が可能である。"; }
            if (command == Database.AUSTERITY_MATRIX) { return "敵対象：対象の正のＢＵＦＦ効果を全て解除する。\r\n\r\nこの後、正のＢＵＦＦ効果は付与できなくなる。\r\n\r\n負のＢＵＦＦ効果は付与できる。"; }

            if (command == Database.VANISH_WAVE) { return "敵対象：対象に" + PowerResult("知", 1.0, 200, 300) + "の【水/空】ダメージを与える。\r\n加えて、対象に【沈黙】効果を与える。本効果は３ターン持続される。"; }
            if (command == Database.VORTEX_FIELD) { return "敵全体：対象者全員に【鈍化】効果を与える。本効果は４ターン持続される。"; }
            if (command == Database.BLUE_DRAGON_WILL) { return "味方対象：【水】属性のダメージを" + PowerResult(1.5) + "上昇させる。"; }

            if (command == Database.SEVENTH_MAGIC) { return "自分対象：物理攻撃の基を【力】から【知】へ転換する。\r\n\r\n魔法攻撃の基を【知】から【力】へ転換する。"; }
            if (command == Database.PARADOX_IMAGE) { return "自分対象：潜在能力を" + PowerResult(1.2) + "上昇させる。"; }
            if (command == Database.WARP_GATE) { return "自分対象：行動ゲージバーを半分進める。\r\n\r\nもし、行動フェーズを超える場合、現在選択している魔法／スキルをコスト消費する事なく行動する。その後行動ゲージを超えた分だけ、行動ゲージバーを進める。"; }
            // スキル
            if (command == Database.STRAIGHT_SMASH) { return "敵対象：対象に" + PowerResult("力", 1.0, 0, 0) + " ＋ " + PowerResult("技", 2.0, 0, 0) + " ＋ " + PowerResult("武器", 1.0, 0, 0) + "の物理ダメージを与える。"; }
            if (command == Database.DOUBLE_SLASH) { return "敵対象：対象に" + PowerResult("力", 1.0, 0, 0) + "の物理ダメージを２回連続で与える。"; }
            if (command == Database.CRUSHING_BLOW) { return "敵対象：対象に" + PowerResult("力", 1.0, 0, 0) + "の物理ダメージを与える。\r\n加えて、対象者に【スタン】効果を与える。本効果は２ターン持続される。"; }
            if (command == Database.SOUL_INFINITY) { return "敵対象：対象に" + PowerResult( "力", 1.2, 0, 0) + " ＋ " + PowerResult("技", 1.2, 0, 0) + " ＋ " + PowerResult("知", 1.2, 0, 0) + " ＋ " + PowerResult("武器", 1.0, 6000, 8000) + "の物理ダメージを与える。\r\n\r\nこのダメージは潜在能力値に応じて増幅する。"; }

            if (command == Database.COUNTER_ATTACK) { return "自分対象：ダメージ源を有する物理攻撃が向けられた場合、それをカウンターする。\r\nこの効果は一度発動すると解除される。"; }
            if (command == Database.PURE_PURIFICATION) { return "自分対象：負のＢＵＦＦ効果を全て解除する。\r\n\r\n負のＢＵＦＦ効果には以下が含まれる。\r\n" + MinusBuff(); }
            if (command == Database.ANTI_STUN) { return "自分対象：【スタン】効果に対して、耐性（効果を防ぐ）を付与する。\r\nこの効果は一度発動すると解除される。"; }
            if (command == Database.STANCE_OF_DEATH) { return "自分対象：致死ダメージを食らった場合、死亡を回避し、ライフ１で残る。\r\nこの効果は一度発動すると解除される。"; }

            if (command == Database.STANCE_OF_FLOW) { return "自分対象：本効果が持続している間、必ず後攻をとる。\r\n\r\n（敵がメイン行動を取るまでの間、自分のメイン行動が発動する直前で行動ゲージが停止する。）\r\n\r\n本効果は３ターン持続される。"; }
            if (command == Database.ENIGMA_SENSE) { return "敵対象：【力】【技】【知】のうち最も高い値を《最大》として、対象に" + PowerResult("最大", 2.0, 0, 0) + "の物理ダメージを与える。"; }
            if (command == Database.SILENT_RUSH) { return "敵対象：対象に" + PowerResult("力", 1.0, 0, 0) + "の物理ダメージを３回連続で与える。"; }
            if (command == Database.OBORO_IMPACT) { return "敵対象：【力】【技】【知】のうち最も高い値を《最大》、中間の値を《中間》、最も低い値を《最小》として、対象に" + PowerResult("最大", 1.5, 0, 0) + " ＋ " + PowerResult("中間", 1.0, 0, 0) + " ＋ " + PowerResult("最小", 0.5, 0, 0) + " ＋ " + PowerResult("武器", 1.0, 6000, 8000) + "の物理ダメージを与える。\r\n\r\nこのダメージは潜在能力値に応じて増幅する。"; }

            if (command == Database.STANCE_OF_STANDING) { return "敵対象：対象に" + PowerResult("力", 1.0, 0, 0) + "の物理ダメージを与える。\r\n加えて、本効果が持続している間、防御体制が維持される。本効果は１ターン持続される。"; }
            if (command == Database.INNER_INSPIRATION) { return "自分対象：スキルポイントを《【心】+10》の分だけ回復する。\r\n心が1以上：0 + 心/10\r\n心が100以上：10 + (心-100)/90\r\n心が1000以上：20 + (心-1000)/900"; }
            if (command == Database.KINETIC_SMASH) { return "敵対象：対象に" + PowerResult("力", 1.0, 0, 0) + " ＋ " + PowerResult("心", 1.0, 0, 0) + " ＋ " + PowerResult("武器", 3.0, 2000, 3000) + "の物理ダメージを与える。\r\n\r\nこのダメージは潜在能力値に応じて増幅する。"; }
            if (command == Database.CATASTROPHE) { return "敵対象：【力】【技】【知】のうち最も低い値を《最小》として、対象に" + PowerResult("最小", 5.0, 0, 0) + " ＋ " + PowerResult("武器", 1.0, 6000, 8000) + "の物理ダメージを与える。\r\n\r\nこのダメージは潜在能力値に応じて増幅する。"; }

            if (command == Database.TRUTH_VISION) { return "自分対象：敵が正のＢＵＦＦ効果を受けている場合、その効果によるパラメタ上昇を無視する。"; }
            if (command == Database.HIGH_EMOTIONALITY) { return "自分対象：本効果が持続している間、力/技/知/心パラメタをそれぞれ" + PowerResult(1.3) + "上昇させる。"; }
            if (command == Database.STANCE_OF_EYES) { return "敵対象：本効果が持続している間、相手の行動をカウンターする。\r\nこの効果は一度発動されると解除される。"; }
            if (command == Database.PAINFUL_INSANITY) { return "自分対象：ターンが進む度に、それぞれの敵へ" + PowerResult("心", 3.0, 2000, 3000) + "のダメージを与える。\r\nこのスキルは魔法ダメージとして扱われる。"; }

            if (command == Database.NEGATE) { return "敵対象：敵からスペル詠唱が行われた場合、そのスペル詠唱をカウンターする。"; }
            if (command == Database.VOID_EXTRACTION) { return "自分対象：力、技、知、心のうち、最も高いパラメタを２倍にする。"; }
            if (command == Database.CARNAGE_RUSH) { return "敵対象：対象に" + PowerResult("力", 1.0, 0, 0) + "の物理ダメージを５回連続で与える。"; }
            if (command == Database.NOTHING_OF_NOTHINGNESS) { return "自分対象：魔法およびスキルがカウンターされなくなる。\r\n\r\n正のＢＵＦＦ効果が解除されなくなる。\r\n\r\nAusterityMatrixのBUFF効果は付与されなくなる。"; }

            if (command == Database.NEUTRAL_SMASH) { return "敵対象：通常攻撃を行う。このスキルはスキルポイントを消費しない。"; }
            if (command == Database.STANCE_OF_DOUBLE) { return "自分対象：本効果が持続している間、メイン行動を行う直前に、前回の行動を行う。本効果は１ターン持続される。"; }

            if (command == Database.SWIFT_STEP) { return "自分対象：本効果が持続している間、戦闘速度を" + PowerResult(1.3) + "上昇させる。本効果は３ターン持続される。"; }
            if (command == Database.VIGOR_SENSE) { return "自分対象：本効果が持続している間、戦闘反応を" + PowerResult(1.4) + "上昇させる。本効果は３ターン持続される。"; }

            if (command == Database.CIRCLE_SLASH) { return "敵全体：対象者全員に" + PowerResult("力", 1.0, 0, 0) + "の物理ダメージを与える。"; }
            if (command == Database.RISING_AURA) { return "味方全体：対象者全員の物理攻撃力を" + PowerResult(1.4) + "上昇させる。"; }

            if (command == Database.RUMBLE_SHOUT) { return "敵対象：対象相手が自分以外の単一対象を取っている場合、その対象を自分に変更する。"; }
            if (command == Database.ONSLAUGHT_HIT) { return "敵対象：対象に" + PowerResult("力", 1.0, 0, 0) + "の物理ダメージを与える。\r\n加えて、物理攻撃力と魔法攻撃力を累積数に応じて低下させる。この効果は３回まで累積が可能である。\r\n累積数１：１５％ダウン\r\n累積数２：３０％ダウン\r\n累積数３：４５％ダウン"; }

            if (command == Database.SMOOTHING_MOVE) { return "敵対象：対象に" + PowerResult("力", 1.0, 0, 0) + "の物理ダメージを与える。\r\n加えて、その直後だけ自分の戦闘速度を2.0倍にする。"; }
            if (command == Database.ASCENSION_AURA) { return "味方全体：対象者全員の魔法攻撃力を" + PowerResult(1.4) + "上昇させる。"; }

            if (command == Database.FUTURE_VISION) { return "自分対象：本効果が持続している間、それぞれの敵がインスタント行動した場合、それをカウンターする。本効果は２ターン持続される。"; }
            if (command == Database.UNKNOWN_SHOCK) { return "敵全体：対象に" + PowerResult("力", 1.0, 0, 0) + "の物理ダメージを与える。加えて、対象者に【暗闇】効果を与える。本効果は３ターン持続される。"; }

            if (command == Database.REFLEX_SPIRIT) { return "自分対象：【スタン】/【麻痺】/【凍結】効果に対する耐性（効果を防ぐ）を付与する。\r\nこの効果は一度発動すると解除される。"; }
            if (command == Database.FATAL_BLOW) { return "敵対象：対象を３３％の確率で死亡させる。\r\nそうでない場合、対象にクリティカルダメージを与える。"; }

            if (command == Database.SHARP_GLARE) { return "敵全体：対象に" + PowerResult("力", 1.0, 0, 0) + "の物理ダメージを与える。加えて、対象者に【沈黙】効果を与える。本効果は３ターン持続される。"; }
            if (command == Database.CONCUSSIVE_HIT) { return "敵対象：対象に" + PowerResult("力", 1.0, 0, 0) + "の物理ダメージを与える。\r\n加えて、物理防御力と魔法防御力を累積数に応じて低下させる。この効果は３回まで累積が可能である。\r\n累積数１：１５％ダウン\r\n累積数２：３０％ダウン\r\n累積数３：４５％ダウン"; }

            if (command == Database.TRUST_SILENCE) { return "自分対象：【沈黙】/【暗闇】/【誘惑】効果に対する耐性（効果を防ぐ）を付与する。\r\nこの効果は一度発動すると解除される。"; }
            if (command == Database.MIND_KILLING) { return "敵対象：対象に" + PowerResult("力", 1.0, 100, 200) + "のＭＰダメージを与える。"; }

            if (command == Database.SURPRISE_ATTACK) { return "敵全体：対象に" + PowerResult("力", 1.0, 0, 0) + "の物理ダメージを与える。加えて、対象者に【麻痺】効果を与える。本効果は１ターン持続される。"; }
            if (command == Database.STANCE_OF_MYSTIC) { return "自分対象：ダメージ源を有する物理攻撃または魔法攻撃が向けられた場合、ダメージを無効化し、それに付随する効果を回避する。\r\n\r\nこのスキルはカウンターされない。\r\nこの効果は３回まで累積が可能である。"; }

            if (command == Database.PSYCHIC_WAVE) { return "敵対象：対象の防御を無視した上で、対象に" + PowerResult("知", 2.0, 0, 0) + "のダメージを与える。\r\nこのスキルは魔法ダメージとして扱われる。\r\nこの魔法はDeflectionの対象とならない。\r\nこのスキルはカウンターされない。"; }
            if (command == Database.NOURISH_SENSE) { return "自分対象：ライフ回復を受ける際、回復量が通常の" + PowerResult(1.3) + "になる。"; }

            if (command == Database.RECOVER) { return "自分対象：スタン、麻痺、凍結を解除する。この効果はスタン、麻痺、凍結状態においても発動できる。"; }
            if (command == Database.IMPULSE_HIT) { return "敵対象：対象に" + PowerResult("力", 1.0, 0, 0) + "の物理ダメージを与える。\r\n加えて、戦闘速度と戦闘反応を累積数に応じて低下させる。この効果は３回まで累積が可能である。\r\n累積数１：１５％ダウン\r\n累積数２：３０％ダウン\r\n累積数３：４５％ダウン"; }

            if (command == Database.VIOLENT_SLASH) { return "敵対象：対象に" + PowerResult("力" , 2.5, 0, 0) + "の物理ダメージを与える。\r\nこのスキルはカウンターされない。"; }
            if (command == Database.ONE_AUTHORITY) { return "味方全体：本効果が持続している間、ターンが進む度に《【心】+10》の分だけスキルポイントが回復する。本効果は３ターン持続する。\r\n\r\n心が１以上：0 + 心/4\r\n心が100以上：25 + (心-100)/30\r\n心が1000以上：55 + (心-1000)/300"; }

            if (command == Database.OUTER_INSPIRATION) { return "自分対象：物理攻撃/物理防御/魔法攻撃/魔法防御/戦闘速度/戦闘反応/潜在能力に対する負のＢＵＦＦ効果を解除する。"; }
            if (command == Database.HARDEST_PARRY) { return "インスタント対象：対象のインスタント行動がダメージ源を有している場合、ダメージを無効化し、それに付随する効果を回避する。"; }

            if (command == Database.STANCE_OF_SUDDENNESS) { return "インスタント対象：対象のインスタント行動を打ち消す。\r\nこのスキルはスタックの対象とならず即座に効果を発揮する。"; }
            if (command == Database.SOUL_EXECUTION) { return "敵対象：対象に《力 ｘ 攻撃倍率》の物理ダメージをTruthVisionがかかった状態として１０回連続で与える。\r\n\r\n攻撃倍率は以下の通りである。\r\n１撃：1.0倍　２撃：1.1倍　３撃：1.2倍　４撃：1.3倍　５撃：1.5倍　６撃：1.7倍　７撃：1.9倍　８撃：2.2倍　９撃：2.5倍　１０撃：3.0倍"; }

            // SOUL_EXECUTIONはダメージ計算値を出さないと駄目。

            if (command == Database.ARCHETYPE_EIN) { return "自分対象：次に当たる「物理/魔法」ダメージをＸ倍にした上で、クリティカルダメージを与える。Ｘは心パラメタに依存する。一日に一度しか使用できない。"; }
            //if (command == Database.ARCHETYPE_RANA) { return "味方全体：ターン制依存のBUFF効果をＸターン追加で継続する。Ｘは心パラメタに依存する。一日に一度しか使用できない。"; }
            //if (command == Database.ARCHETYPE_OL) { return "敵全体：Ｘ回の物理ダメージ。Ｘは心パラメタに依存する。一日に一度しか使用できない。"; }
            //if (command == Database.ARCHETYPE_VERZE) { return "インスタント対象：自分のインスタント行動をＸ回行う。Ｘは心パラメタに依存する。一日に一度しか使用できない。"; }

            return String.Empty;
        }

        //public static string GetPowerFactor(string command)
        //{
        //    if (command == Database.FIRE_BALL) { return Database.POWER_INT; }
        //    else if (command == Database.ICE_NEEDLE) { return Database.POWER_INT; }
        //    else if (command == Database.DARK_BLAST) { return Database.POWER_INT; }
        //    else if (command == Database.WORD_OF_POWER) { return Database.POWER_STR; }
        //    else if (command == Database.FLAME_AURA) { return Database.POWER_INT; }
        //    else if (command == Database.FROZEN_AURA) { return Database.POWER_INT; }
        //    else if (command == Database.HOLY_SHOCK) { return Database.POWER_INT; }
        //    else if (command == Database.DEVOURING_PLAGUE) { return Database.POWER_INT; }
        //    else if (command == Database.FLASH_BLAZE) { return Database.POWER_INT; }
        //    else if (command == Database.ENRAGE_BLAST) { return Database.POWER_INT; }
        //    else if (command == Database.BLACK_FIRE) { return Database.POWER_INT; }
        //    else if (command == Database.WORD_OF_MALICE) { return Database.POWER_INT; }
        //    else if (command == Database.IMMOLATE) { return Database.POWER_INT; }
        //    else if (command == Database.VANISH_WAVE) { return Database.POWER_INT; }
        //    else if (command == Database.STAR_LIGHTNING) { return Database.POWER_INT; }
        //    else if (command == Database.BLUE_BULLET) { return Database.POWER_INT; }
        //    else if (command == Database.FLAME_STRIKE) { return Database.POWER_INT; }
        //    else if (command == Database.FROZEN_LANCE) { return Database.POWER_INT; }
        //    else if (command == Database.LIGHT_DETONATOR) { return Database.POWER_INT; }
        //    else if (command == Database.VOLCANIC_WAVE) { return Database.POWER_INT; }
        //    else if (command == Database.WHITE_OUT) { return Database.POWER_INT; }
        //    else if (command == Database.BLAZING_FIELD) { return Database.POWER_INT; }
        //    else if (command == Database.PIERCING_FLAME) { return Database.POWER_INT; }
        //    else if (command == Database.DOOM_BLADE) { return Database.POWER_INT; }
        //    else if (command == Database.ABYSS_EYE) { return Database.POWER_INT; }
        //    else if (command == Database.ASCENDANT_METEOR) { return Database.POWER_INT; }
        //    else if (command == Database.CELESTIAL_NOVA) { return Database.POWER_INT; }
        //    else if (command == Database.DEMONIC_IGNITE) { return Database.POWER_INT; }
        //    else if (command == Database.LAVA_ANNIHILATION) { return Database.POWER_INT; }
        //    else if (command == Database.ZETA_EXPLOSION) { return Database.POWER_INT; }
        //    else if (command == Database.CHILL_BURN) { return Database.POWER_INT; }
        //    else if (command == Database.DAMNATION) { return Database.POWER_MND; }
        //    // 回復系
        //    else if (command == Database.FRESH_HEAL) { return Database.POWER_INT; }
        //    else if (command == Database.LIFE_TAP) { return Database.POWER_INT; }
        //    else if (command == Database.WORD_OF_LIFE) { return Database.POWER_MND; }
        //    else if (command == Database.SACRED_HEAL) { return Database.POWER_INT; }


        //    return String.Empty;
        //}
        //public static double GetPowerValue(string command)
        //{
        //    if (command == Database.FIRE_BALL) { return 3.0f; }
        //    else if (command == Database.ICE_NEEDLE) { return 2.8f; }
        //    else if (command == Database.DARK_BLAST) { return 2.6f; }
        //    else if (command == Database.WORD_OF_POWER) { return 2.4f; }
        //    else if (command == Database.FLAME_AURA) { return 3.0f; }
        //    else if (command == Database.FROZEN_AURA) { return 2.8f; }
        //    else if (command == Database.HOLY_SHOCK) { return 2.2f; }
        //    else if (command == Database.DEVOURING_PLAGUE) { return 2.0f; }
        //    else if (command == Database.FLASH_BLAZE) { return 2.0f; }
        //    else if (command == Database.ENRAGE_BLAST) { return 1.0f; }
        //    else if (command == Database.BLACK_FIRE) { return 1.0f; }
        //    else if (command == Database.WORD_OF_MALICE) { return 1.0f; }
        //    else if (command == Database.IMMOLATE) { return 1.0f; }
        //    else if (command == Database.VANISH_WAVE) { return 1.0f; }
        //    else if (command == Database.STAR_LIGHTNING) { return 1.0f; }
        //    else if (command == Database.BLUE_BULLET) { return 0.9f; }
        //    else if (command == Database.FLAME_STRIKE) { return 3.5f; }
        //    else if (command == Database.FROZEN_LANCE) { return 3.3f; }
        //    else if (command == Database.LIGHT_DETONATOR) { return 3.0f; }
        //    else if (command == Database.VOLCANIC_WAVE) { return 4.0f; }
        //    else if (command == Database.WHITE_OUT) { return 3.8f; }
        //    else if (command == Database.BLAZING_FIELD) { return 2.5f; }
        //    else if (command == Database.PIERCING_FLAME) { return 4.5f; }
        //    else if (command == Database.DOOM_BLADE) { return 2.8f; } // ダメージの方
        //    else if (command == Database.ABYSS_EYE) { return 2.8f; }
        //    else if (command == Database.ASCENDANT_METEOR) { return 1.5f; }
        //    else if (command == Database.CELESTIAL_NOVA) { return 4.5f; } // ダメージの方
        //    else if (command == Database.DEMONIC_IGNITE) { return 4.5f; }
        //    else if (command == Database.LAVA_ANNIHILATION) { return 5.0f; }
        //    else if (command == Database.ZETA_EXPLOSION) { return 6.0f; }
        //    else if (command == Database.CHILL_BURN) { return 1.0f; }
        //    else if (command == Database.DAMNATION) { return 0.0f; }
        //    // 回復系
        //    else if (command == Database.FRESH_HEAL) { return 4.0f; }
        //    else if (command == Database.LIFE_TAP) { return 4.0f; }
        //    else if (command == Database.WORD_OF_LIFE) { return 1.0f; }
        //    else if (command == Database.SACRED_HEAL) { return 3.5f; }
        //    // スキル
        //    else if (command == Database.STRAIGHT_SMASH) { return }
        //    return 0.0f;
        //}
        //public static int GetPowerPlus(string command)
        //{
        //    if (command == Database.FIRE_BALL) { return 30; }
        //    else if (command == Database.ICE_NEEDLE) { return 30; }
        //    else if (command == Database.DARK_BLAST) { return 30; }
        //    else if (command == Database.WORD_OF_POWER) { return 30; }
        //    else if (command == Database.FLAME_AURA) { return 30; }
        //    else if (command == Database.FROZEN_AURA) { return 30; }
        //    else if (command == Database.HOLY_SHOCK) { return 120; }
        //    else if (command == Database.DEVOURING_PLAGUE) { return 120; }
        //    else if (command == Database.FLASH_BLAZE) { return 200; }
        //    else if (command == Database.ENRAGE_BLAST) { return 200; }
        //    else if (command == Database.BLACK_FIRE) { return 200; }
        //    else if (command == Database.WORD_OF_MALICE) { return 200; }
        //    else if (command == Database.IMMOLATE) { return 200; }
        //    else if (command == Database.VANISH_WAVE) { return 200; }
        //    else if (command == Database.STAR_LIGHTNING) { return 200; }
        //    else if (command == Database.BLUE_BULLET) { return 200; }
        //    else if (command == Database.FLAME_STRIKE) { return 750; }
        //    else if (command == Database.FROZEN_LANCE) { return 750; }
        //    else if (command == Database.LIGHT_DETONATOR) { return 750; }
        //    else if (command == Database.VOLCANIC_WAVE) { return 1200; }
        //    else if (command == Database.WHITE_OUT) { return 1200; }
        //    else if (command == Database.BLAZING_FIELD) { return 1200; }
        //    else if (command == Database.PIERCING_FLAME) { return 3500; }
        //    else if (command == Database.DOOM_BLADE) { return 2000; } // ダメージの方
        //    else if (command == Database.ABYSS_EYE) { return 4500; }
        //    else if (command == Database.ASCENDANT_METEOR) { return 2000; }
        //    else if (command == Database.CELESTIAL_NOVA) { return 4000; } // ダメージの方
        //    else if (command == Database.DEMONIC_IGNITE) { return 6000; }
        //    else if (command == Database.LAVA_ANNIHILATION) { return 7000; }
        //    else if (command == Database.ZETA_EXPLOSION) { return 8000; }
        //    else if (command == Database.CHILL_BURN) { return 0; }
        //    else if (command == Database.DAMNATION) { return 0; }
        //    // 回復系
        //    else if (command == Database.FRESH_HEAL) { return 40; }
        //    else if (command == Database.LIFE_TAP) { return 40; }
        //    else if (command == Database.WORD_OF_LIFE) { return 40; }
        //    else if (command == Database.SACRED_HEAL) { return 4000; }

        //    return 0;
        //}
        //public static int GetPowerCount(string command)
        //{
        //    if (command == Database.FIRE_BALL) { return 1; }
        //    else if (command == Database.ICE_NEEDLE) { return 1; }
        //    else if (command == Database.DARK_BLAST) { return 1; }
        //    else if (command == Database.WORD_OF_POWER) { return 1; }
        //    else if (command == Database.FLAME_AURA) { return 1; }
        //    else if (command == Database.FROZEN_AURA) { return 1; }
        //    else if (command == Database.HOLY_SHOCK) { return 1; }
        //    else if (command == Database.DEVOURING_PLAGUE) { return 1; }
        //    else if (command == Database.FLASH_BLAZE) { return 2; } // 後付けでもう1回当たる仕様
        //    else if (command == Database.ENRAGE_BLAST) { return 1; }
        //    else if (command == Database.BLACK_FIRE) { return 1; }
        //    else if (command == Database.WORD_OF_MALICE) { return 1; }
        //    else if (command == Database.IMMOLATE) { return 1; }
        //    else if (command == Database.VANISH_WAVE) { return 1; }
        //    else if (command == Database.STAR_LIGHTNING) { return 1; }
        //    else if (command == Database.BLUE_BULLET) { return 3; }
        //    else if (command == Database.FLAME_STRIKE) { return 1; }
        //    else if (command == Database.FROZEN_LANCE) { return 1; }
        //    else if (command == Database.LIGHT_DETONATOR) { return 1; }
        //    else if (command == Database.VOLCANIC_WAVE) { return 1; }
        //    else if (command == Database.WHITE_OUT) { return 1; }
        //    else if (command == Database.BLAZING_FIELD) { return 1; }
        //    else if (command == Database.PIERCING_FLAME) { return 1; }
        //    else if (command == Database.DOOM_BLADE) { return 1; } // ダメージの方
        //    else if (command == Database.ABYSS_EYE) { return 1; }
        //    else if (command == Database.ASCENDANT_METEOR) { return 10; }
        //    else if (command == Database.CELESTIAL_NOVA) { return 1; } // ダメージの方
        //    else if (command == Database.DEMONIC_IGNITE) { return 1; }
        //    else if (command == Database.LAVA_ANNIHILATION) { return 1; }
        //    else if (command == Database.ZETA_EXPLOSION) { return 1; }
        //    else if (command == Database.CHILL_BURN) { return 1; }
        //    else if (command == Database.DAMNATION) { return 1; }
        //    // 回復系
        //    else if (command == Database.FRESH_HEAL) { return 1; }
        //    else if (command == Database.LIFE_TAP) { return 1; }
        //    else if (command == Database.WORD_OF_LIFE) { return 1; }
        //    else if (command == Database.SACRED_HEAL) { return 1; }

        //    return 1; // デフォルトは1回
        //}

        // 魔法属性を判別
        public static MagicType GetMagicType(string command)
        {
            switch (command)
            {
                // [聖]
                case Database.FRESH_HEAL:
                case Database.PROTECTION:
                case Database.HOLY_SHOCK:
                case Database.SAINT_POWER:
                case Database.GLORY:
                case Database.RESURRECTION:
                case Database.CELESTIAL_NOVA:
                    return MagicType.Light;

                // [闇]
                case Database.DARK_BLAST:
                case Database.SHADOW_PACT:
                case Database.LIFE_TAP:
                case Database.BLACK_CONTRACT:
                case Database.DEVOURING_PLAGUE:
                case Database.BLOODY_VENGEANCE:
                case Database.DAMNATION:
                    return MagicType.Shadow;

                // [火]
                case Database.FIRE_BALL:
                case Database.FLAME_AURA:
                case Database.HEAT_BOOST:
                case Database.FLAME_STRIKE:
                case Database.VOLCANIC_WAVE:
                case Database.IMMORTAL_RAVE:
                case Database.LAVA_ANNIHILATION:
                    return MagicType.Fire;

                // [水]
                case Database.ICE_NEEDLE:
                case Database.ABSORB_WATER:
                case Database.CLEANSING:
                case Database.FROZEN_LANCE:
                case Database.MIRROR_IMAGE:
                case Database.PROMISED_KNOWLEDGE:
                case Database.ABSOLUTE_ZERO:
                    return MagicType.Ice;

                // [理]
                case Database.WORD_OF_POWER:
                case Database.GALE_WIND:
                case Database.WORD_OF_LIFE:
                case Database.WORD_OF_FORTUNE:
                case Database.AETHER_DRIVE:
                case Database.GENESIS:
                case Database.ETERNAL_PRESENCE:
                    return MagicType.Force;

                // [空]
                case Database.DISPEL_MAGIC:
                case Database.RISE_OF_IMAGE:
                case Database.DEFLECTION:
                case Database.TRANQUILITY:
                case Database.ONE_IMMUNITY:
                case Database.WHITE_OUT:
                case Database.TIME_STOP:
                    return MagicType.Will;

                // [聖闇]（完全逆）
                case Database.PSYCHIC_TRANCE:
                case Database.BLIND_JUSTICE:
                case Database.TRANSCENDENT_WISH:
                    return MagicType.Light_Shadow;

                // [聖火]
                case Database.FLASH_BLAZE:
                case Database.LIGHT_DETONATOR:
                case Database.ASCENDANT_METEOR:
                    return MagicType.Light_Fire;

                // [聖水]
                case Database.SKY_SHIELD:
                case Database.SACRED_HEAL:
                case Database.EVER_DROPLET:
                    return MagicType.Light_Ice;

                // [聖理]
                case Database.HOLY_BREAKER:
                case Database.EXALTED_FIELD:
                case Database.HYMN_CONTRACT:
                    return MagicType.Light_Force;

                // [聖空]
                case Database.STAR_LIGHTNING:
                case Database.ANGEL_BREATH:
                case Database.ENDLESS_ANTHEM:
                    return MagicType.Light_Will;

                // [闇火]
                case Database.BLACK_FIRE:
                case Database.BLAZING_FIELD:
                case Database.DEMONIC_IGNITE:
                    return MagicType.Shadow_Fire;

                // [闇　水]
                case Database.BLUE_BULLET:
                case Database.DEEP_MIRROR:
                case Database.DEATH_DENY:
                    return MagicType.Shadow_Ice;

                // [闇　理]
                case Database.WORD_OF_MALICE:
                case Database.ABYSS_EYE:
                case Database.SIN_FORTUNE:
                    return MagicType.Shadow_Force;

                // [闇　空]
                case Database.DARKEN_FIELD:
                case Database.DOOM_BLADE:
                case Database.ECLIPSE_END:
                    return MagicType.Shadow_Will;

                // [火　水]（完全逆）
                case Database.FROZEN_AURA:
                case Database.CHILL_BURN:
                case Database.ZETA_EXPLOSION:
                    return MagicType.Fire_Ice;

                // [火　理]
                case Database.ENRAGE_BLAST:
                case Database.PIERCING_FLAME:
                case Database.SIGIL_OF_HOMURA:
                    return MagicType.Fire_Force;

                // [火　空]
                case Database.IMMOLATE:
                case Database.PHANTASMAL_WIND:
                case Database.RED_DRAGON_WILL:
                    return MagicType.Fire_Will;

                // [水　理]
                case Database.WORD_OF_ATTITUDE:
                case Database.STATIC_BARRIER:
                case Database.AUSTERITY_MATRIX:
                    return MagicType.Ice_Force;

                // [水　空]
                case Database.VANISH_WAVE:
                case Database.VORTEX_FIELD:
                case Database.BLUE_DRAGON_WILL:
                    return MagicType.Ice_Will;

                // [理　空]（完全逆）
                case Database.SEVENTH_MAGIC:
                case Database.PARADOX_IMAGE:
                case Database.WARP_GATE:
                    return MagicType.Force_Will;
            }
            return MagicType.None;
        }

        // スキル属性を判別
        public static SkillType GetSkillType(string command)
        {
            switch (command)
            {
                // [動]
                case Database.STRAIGHT_SMASH:
                case Database.DOUBLE_SLASH:
                case Database.CRUSHING_BLOW:
                case Database.SOUL_INFINITY:
                    return SkillType.Active;

                // [静]
                case Database.COUNTER_ATTACK:
                case Database.PURE_PURIFICATION:
                case Database.ANTI_STUN:
                case Database.STANCE_OF_DEATH:
                    return SkillType.Passive;

                // [柔]
                case Database.STANCE_OF_FLOW:
                case Database.ENIGMA_SENSE:
                case Database.SILENT_RUSH:
                case Database.OBORO_IMPACT:
                    return SkillType.Soft;

                // [剛]
                case Database.STANCE_OF_STANDING:
                case Database.INNER_INSPIRATION:
                case Database.KINETIC_SMASH:
                case Database.CATASTROPHE:
                    return SkillType.Hard;

                // [心眼]
                case Database.TRUTH_VISION:
                case Database.HIGH_EMOTIONALITY:
                case Database.STANCE_OF_EYES:
                case Database.PAINFUL_INSANITY:
                    return SkillType.Truth;

                // [無心]
                case Database.NEGATE:
                case Database.VOID_EXTRACTION:
                case Database.CARNAGE_RUSH:
                case Database.NOTHING_OF_NOTHINGNESS:
                    return SkillType.Void;

                // [動　静]（完全逆）
                case Database.NEUTRAL_SMASH:
                case Database.STANCE_OF_DOUBLE:
                    return SkillType.Active_Passive;

                // [動　柔]
                case Database.SWIFT_STEP:
                case Database.VIGOR_SENSE:
                    return SkillType.Active_Soft;

                // [動　剛]
                case Database.CIRCLE_SLASH:
                case Database.RISING_AURA:
                    return SkillType.Active_Hard;

                // [動　心眼]
                case Database.RUMBLE_SHOUT:
                case Database.ONSLAUGHT_HIT:
                    return SkillType.Active_Truth;

                // [動　無心]
                case Database.SMOOTHING_MOVE:
                case Database.ASCENSION_AURA:
                    return SkillType.Active_Void;

                // [静　柔]
                case Database.FUTURE_VISION:
                case Database.UNKNOWN_SHOCK:
                    return SkillType.Passive_Soft;

                // [静　剛]
                case Database.REFLEX_SPIRIT:
                case Database.FATAL_BLOW:
                    return SkillType.Passive_Hard;

                // [静　心眼]
                case Database.SHARP_GLARE:
                case Database.CONCUSSIVE_HIT:
                    return SkillType.Passive_Truth;

                // [静　無心]
                case Database.TRUST_SILENCE:
                case Database.MIND_KILLING:
                    return SkillType.Passive_Void;

                // [柔　剛]（完全逆）
                case Database.SURPRISE_ATTACK:
                case Database.STANCE_OF_MYSTIC:
                    return SkillType.Soft_Hard;

                // [柔　心眼]
                case Database.PSYCHIC_WAVE:
                case Database.NOURISH_SENSE:
                    return SkillType.Soft_Truth;

                // [柔　無心]
                case Database.RECOVER:
                case Database.IMPULSE_HIT:
                    return SkillType.Soft_Void;

                // [剛　心眼]
                case Database.VIOLENT_SLASH:
                case Database.ONE_AUTHORITY:
                    return SkillType.Hard_Truth;

                // [剛　無心]
                case Database.OUTER_INSPIRATION:
                case Database.HARDEST_PARRY:
                    return SkillType.Hard_Void;

                // [心眼　無心]（完全逆）
                case Database.STANCE_OF_SUDDENNESS:
                case Database.SOUL_EXECUTION:
                    return SkillType.Truth_Void;
            }
            return SkillType.None;
        }

        // BUFFタイプを判別
        public static BuffType GetBuffType(string command)
        {
            switch (command)
            {
                case Database.PROTECTION:
                case Database.SAINT_POWER:
                case Database.SHADOW_PACT:
                case Database.BLOODY_VENGEANCE:
                case Database.FLAME_AURA:
                case Database.HEAT_BOOST:
                case Database.ABSORB_WATER:
                case Database.MIRROR_IMAGE:
                case Database.PROMISED_KNOWLEDGE:
                case Database.WORD_OF_LIFE:
                case Database.ETERNAL_PRESENCE:
                case Database.RISE_OF_IMAGE:
                case Database.DEFLECTION:
                case Database.PSYCHIC_TRANCE:
                case Database.BLIND_JUSTICE:
                case Database.TRANSCENDENT_WISH:
                case Database.SKY_SHIELD:
                case Database.EVER_DROPLET:
                case Database.HOLY_BREAKER:
                case Database.EXALTED_FIELD:
                case Database.SIN_FORTUNE:
                case Database.FROZEN_AURA:
                case Database.PHANTASMAL_WIND:
                case Database.RED_DRAGON_WILL:
                case Database.STATIC_BARRIER:
                case Database.BLUE_DRAGON_WILL:
                case Database.SEVENTH_MAGIC:
                case Database.PARADOX_IMAGE:
                case Database.ANTI_STUN:
                case Database.STANCE_OF_DEATH:
                case Database.TRUTH_VISION:
                case Database.PAINFUL_INSANITY:
                case Database.VOID_EXTRACTION:
                case Database.NOTHING_OF_NOTHINGNESS:
                case Database.RISING_AURA:
                case Database.ASCENSION_AURA:
                case Database.REFLEX_SPIRIT:
                case Database.TRUST_SILENCE:
                case Database.STANCE_OF_MYSTIC:
                case Database.NOURISH_SENSE:
                    return BuffType.Up;

                case Database.DAMNATION:
                case Database.BLACK_FIRE:
                case Database.BLAZING_FIELD:
                case Database.DEMONIC_IGNITE:
                case Database.WORD_OF_MALICE:
                case Database.DARKEN_FIELD:
                case Database.ENRAGE_BLAST:
                case Database.SIGIL_OF_HOMURA:
                case Database.IMMOLATE:
                case Database.AUSTERITY_MATRIX:
                case Database.ONSLAUGHT_HIT:
                case Database.CONCUSSIVE_HIT:
                case Database.IMPULSE_HIT:
                    return BuffType.Down;

                case Database.GLORY:
                case Database.BLACK_CONTRACT:
                case Database.IMMORTAL_RAVE:
                case Database.GALE_WIND:
                case Database.WORD_OF_FORTUNE:
                case Database.AETHER_DRIVE:
                case Database.ONE_IMMUNITY:
                case Database.HYMN_CONTRACT:
                case Database.STANCE_OF_FLOW:
                case Database.HIGH_EMOTIONALITY:
                case Database.STANCE_OF_DOUBLE:
                case Database.SWIFT_STEP:
                case Database.VIGOR_SENSE:
                case Database.FUTURE_VISION:
                case Database.ONE_AUTHORITY:
                    return BuffType.Up; //BuffType.TurnUp; ターンUP制が結局DispelやAusterityMatrixの対象にならないのは実践上理不尽に感じたため、単にUpとする。

                case Database.ABSOLUTE_ZERO:
                case Database.FLASH_BLAZE:
                case Database.STAR_LIGHTNING:
                case Database.CHILL_BURN:
                case Database.VANISH_WAVE:
                case Database.VORTEX_FIELD:
                case Database.CRUSHING_BLOW:
                case Database.UNKNOWN_SHOCK:
                case Database.SHARP_GLARE:
                case Database.SURPRISE_ATTACK:
                    return BuffType.TurnDown;
            }
            return BuffType.None;
        }

        public static bool IsLight(MagicType type)
        {
            if ((type == MagicType.Light) ||
                (type == MagicType.Light_Fire) ||
                (type == MagicType.Light_Force) ||
                (type == MagicType.Light_Ice) ||
                (type == MagicType.Light_Shadow) ||
                (type == MagicType.Light_Will))
            {
                return true;
            }
            return false;
        }

        public static bool IsShadow(MagicType type)
        {
            if ((type == MagicType.Shadow) ||
                (type == MagicType.Shadow_Fire) ||
                (type == MagicType.Shadow_Force) ||
                (type == MagicType.Shadow_Ice) ||
                (type == MagicType.Shadow_Will) ||
                (type == MagicType.Light_Shadow))
            {
                return true;
            }
            return false;
        }

        public static bool IsFire(MagicType type)
        {
            if ((type == MagicType.Fire) ||
                (type == MagicType.Fire_Force) ||
                (type == MagicType.Fire_Ice) ||
                (type == MagicType.Fire_Will) ||
                (type == MagicType.Light_Fire) ||
                (type == MagicType.Shadow_Fire))
            {
                return true;
            }
            return false;
        }

        public static bool IsIce(MagicType type)
        {
            if ((type == MagicType.Ice) ||
                (type == MagicType.Ice_Force) ||
                (type == MagicType.Ice_Will) ||
                (type == MagicType.Light_Ice) ||
                (type == MagicType.Shadow_Ice) ||
                (type == MagicType.Fire_Ice))
            {
                return true;
            }
            return false;
        }

        public static bool IsForce(MagicType type)
        {
            if ((type == MagicType.Force) ||
                (type == MagicType.Force_Will) ||
                (type == MagicType.Light_Force) ||
                (type == MagicType.Shadow_Force) ||
                (type == MagicType.Fire_Force) ||
                (type == MagicType.Ice_Force))
            {
                return true;
            }
            return false;
        }

        public static bool IsWill(MagicType type)
        {
            if ((type == MagicType.Will) ||
                (type == MagicType.Light_Will) ||
                (type == MagicType.Shadow_Will) ||
                (type == MagicType.Fire_Will) ||
                (type == MagicType.Ice_Will) ||
                (type == MagicType.Force_Will))
            {
                return true;
            }
            return false;
        }

        public static bool IsActive(SkillType type)
        {
            if ((type == SkillType.Active) ||
                (type == SkillType.Active_Hard) ||
                (type == SkillType.Active_Passive) ||
                (type == SkillType.Active_Soft) ||
                (type == SkillType.Active_Truth) ||
                (type == SkillType.Active_Void))
            {
                return true;
            }
            return false;
        }

        public static bool IsPassive(SkillType type)
        {
            if ((type == SkillType.Passive) ||
                (type == SkillType.Passive_Hard) ||
                (type == SkillType.Passive_Soft) ||
                (type == SkillType.Passive_Truth) ||
                (type == SkillType.Passive_Void) ||
                (type == SkillType.Active_Passive))
            {
                return true;
            }
            return false;
        }

        public static bool IsSoft(SkillType type)
        {
            if ((type == SkillType.Soft) ||
                (type == SkillType.Soft_Hard) ||
                (type == SkillType.Soft_Truth) ||
                (type == SkillType.Soft_Void) ||
                (type == SkillType.Active_Soft) ||
                (type == SkillType.Passive_Soft))
            {
                return true;
            }
            return false;
        }

        public static bool IsHard(SkillType type)
        {
            if ((type == SkillType.Hard) ||
                (type == SkillType.Hard_Truth) ||
                (type == SkillType.Hard_Void) ||
                (type == SkillType.Active_Hard) ||
                (type == SkillType.Passive_Hard) ||
                (type == SkillType.Soft_Hard))
            {
                return true;
            }
            return false;
        }

        public static bool IsTruth(SkillType type)
        {
            if ((type == SkillType.Truth) ||
                (type == SkillType.Truth_Void) ||
                (type == SkillType.Active_Truth) ||
                (type == SkillType.Hard_Truth) ||
                (type == SkillType.Passive_Truth) ||
                (type == SkillType.Soft_Truth))
            {
                return true;
            }
            return false;
        }

        public static bool IsVoid(SkillType type)
        {
            if ((type == SkillType.Void) ||
                (type == SkillType.Active_Void) ||
                (type == SkillType.Hard_Void) ||
                (type == SkillType.Passive_Void) ||
                (type == SkillType.Soft_Void) ||
                (type == SkillType.Truth_Void))
            {
                return true;
            }
            return false;
        }
    }
}
