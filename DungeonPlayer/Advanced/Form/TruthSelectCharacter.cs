using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public partial class TruthSelectCharacter : MotherForm
    {
        public List<MainCharacter> playerList = new List<MainCharacter>();
        private MainCharacter currentPlayer = null;
        public const int MAX_ADD = 2;
        
        private MainCharacter b_tc;
        private MainCharacter b_fc;

        MainCharacter sc;
        public MainCharacter SC
        {
            get { return sc; }
            set { sc = value; }
        }
        MainCharacter tc;
        public MainCharacter TC
        {
            get { return tc; }
            set { tc = value; }
        }
        private int remainSC = 0;
        private int addStrSC = 0;
        private int addAglSC = 0;
        private int addIntSC = 0;
        private int addStmSC = 0;
        private int addMndSC = 0;
        private int remainTC = 0;
        private int addStrTC = 0;
        private int addAglTC = 0;
        private int addIntTC = 0;
        private int addStmTC = 0;
        private int addMndTC = 0;
        private bool choiceSC = false;
        private bool choiceTC = false;
        public TruthSelectCharacter()
        {
            InitializeComponent();
        }

        private void TruthSelectCharacter_Load(object sender, EventArgs e)
        {
            this.basePhysicalLocation = this.PhysicalAttck.Location;

            this.buttonStrength.BackgroundImageLayout = ImageLayout.Stretch;
            this.buttonStrength.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "StrengthMark.bmp");
            this.buttonAgility.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "AgilityMark.bmp");
            this.buttonIntelligence.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "IntelligenceMark.bmp");
            this.buttonStamina.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "StaminaMark.bmp");
            this.buttonMind.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "MindMark.bmp");

            // 1人目
            if (this.sc.FullName == Database.RANA_AMILIA_FULL)
            {
                // データ引き継ぎを行う
            }
            else
            {
                // [警告] 本来は一旦データ引き継ぎを行った後、別メンバーが来た場合、ラナ・アミリアの更新後のデータをここで読み込まなければならない。
                //this.sc = new MainCharacter();
                //this.sc.FullName = Database.RANA_AMILIA_FULL;
                //this.sc.Name = Database.RANA_AMILIA;
                //this.sc.Strength = Database.OL_LANDIS_STRENGTH_2;
                //this.sc.Agility = Database.OL_LANDIS_AGILITY_2;
                //this.sc.Intelligence = Database.OL_LANDIS_INTELLIGENCE_2;
                //this.sc.Stamina = Database.OL_LANDIS_STAMINA_2;
                //this.sc.Mind = Database.OL_LANDIS_MIND_2;
                //this.sc.Level = 0;
                //this.sc.Exp = 0;
            }

            int baseLevel = this.sc.Level;
            if (baseLevel < 65)
            {
                for (int ii = baseLevel; ii < 65; ii++)
                {
                    this.sc.BaseLife += this.sc.LevelUpLifeTruth;
                    this.sc.BaseMana += this.sc.LevelUpManaTruth;
                    this.remainSC += this.sc.LevelUpPointTruth;
                    this.sc.Level++;
                }


                //using (TruthStatusPlayer TSP = new TruthStatusPlayer())
                //{

                //}
                //int totalParam = this.sc.Strength + this.sc.Agility + this.sc.Intelligence + this.sc.
                //double ampStrength = 
                this.sc.Strength += this.remainSC / 5;
                this.sc.Agility += this.remainSC / 5;
                this.sc.Intelligence += this.remainSC / 5;
                this.sc.Stamina += this.remainSC / 5;
                this.sc.Mind += this.remainSC / 5;
                this.sc.Mind += this.remainSC % 5;    
            }
            this.sc.AvailableMana = true;
            this.sc.AvailableSkill = true;

            this.sc.CurrentLife = this.sc.MaxLife;
            this.sc.CurrentSkillPoint = this.sc.MaxSkillPoint;
            this.sc.CurrentMana = this.sc.MaxMana;

            this.sc.DarkBlast = true;
            this.sc.ShadowPact = true;
            this.sc.LifeTap = true;
            this.sc.BlackContract = true;
            this.sc.DevouringPlague = true;
            this.sc.BloodyVengeance = true;
            this.sc.Damnation = true;
            this.sc.IceNeedle = true;
            this.sc.AbsorbWater = true;
            this.sc.Cleansing = true;
            this.sc.FrozenLance = true;
            this.sc.MirrorImage = true;
            this.sc.PromisedKnowledge = true;
            this.sc.AbsoluteZero = true;
            this.sc.DispelMagic = true;
            this.sc.RiseOfImage = true;
            this.sc.Deflection = true;
            this.sc.Tranquility = true;
            this.sc.OneImmunity = true;
            this.sc.WhiteOut = true;
            this.sc.TimeStop = true;

            this.sc.CounterAttack = true;
            this.sc.PurePurification = true;
            this.sc.AntiStun = true;
            this.sc.StanceOfDeath = true;
            this.sc.StanceOfFlow = true;
            this.sc.EnigmaSence = true;
            this.sc.SilentRush = true;
            this.sc.OboroImpact = true;
            this.sc.Negate = true;
            this.sc.VoidExtraction = true;
            this.sc.CarnageRush = true;
            this.sc.NothingOfNothingness = true;

            this.sc.BlueBullet = true;
            this.sc.DeepMirror = true;
            this.sc.DeathDeny = true;
            this.sc.VanishWave = true;
            this.sc.VortexField = true;
            this.sc.BlueDragonWill = true;
            this.sc.DarkenField = true;
            this.sc.DoomBlade = true;
            this.sc.EclipseEnd = true;
            this.sc.SkyShield = true;
            this.sc.SacredHeal = true;
            this.sc.EverDroplet = true;
            this.sc.StarLightning = true;
            this.sc.AngelBreath = true;
            this.sc.EndlessAnthem = true;
            this.sc.PsychicTrance = true;
            this.sc.BlindJustice = true;
            this.sc.TranscendentWish = true;

            this.sc.FutureVision = true;
            this.sc.UnknownShock = true;           
            this.sc.Recover = true;
            this.sc.ImpulseHit = true;
            this.sc.TrustSilence = true;
            this.sc.MindKilling = true;
            this.sc.PsychicWave = true;
            this.sc.NourishSense = true;
            this.sc.SharpGlare = true;
            this.sc.ConcussiveHit = true;            
            this.sc.StanceOfSuddenness = true;
            this.sc.SoulExecution = true;

            // 2人目
            this.tc = new MainCharacter();
            this.tc.FullName = Database.OL_LANDIS_FULL;
            this.tc.Name = Database.OL_LANDIS;
            this.tc.Strength = Database.OL_LANDIS_STRENGTH_2;
            this.tc.Agility = Database.OL_LANDIS_AGILITY_2;
            this.tc.Intelligence = Database.OL_LANDIS_INTELLIGENCE_2;
            this.tc.Stamina = Database.OL_LANDIS_STAMINA_2;
            this.tc.Mind = Database.OL_LANDIS_MIND_2;
            this.tc.Level = 0;
            this.tc.Exp = 0;
            for (int ii = 0; ii < 65; ii++)
            {
                this.tc.BaseLife += this.tc.LevelUpLifeTruth;
                this.tc.BaseMana += this.tc.LevelUpManaTruth;
                this.tc.Level++;
            }

            this.tc.MainWeapon = new ItemBackPack(Database.LEGENDARY_GOD_FIRE_GLOVE);
            this.tc.MainArmor = new ItemBackPack(Database.EPIC_AURA_ARMOR_OMEGA);
            this.tc.Accessory = new ItemBackPack(Database.EPIC_FATE_RING_OMEGA);
            this.tc.Accessory2 = new ItemBackPack(Database.EPIC_LOYAL_RING_OMEGA);
            this.tc.BattleActionCommand1 = Database.DEFENSE_EN;
            this.tc.BattleActionCommand2 = Database.ONE_IMMUNITY;
            this.tc.BattleActionCommand3 = Database.BLACK_CONTRACT;
            this.tc.BattleActionCommand4 = Database.CARNAGE_RUSH;
            this.tc.BattleActionCommand5 = Database.DEMONIC_IGNITE;
            this.tc.BattleActionCommand6 = Database.HARDEST_PARRY;
            this.tc.BattleActionCommand7 = Database.IMPULSE_HIT;
            this.tc.BattleActionCommand8 = Database.DISPEL_MAGIC;
            this.tc.BattleActionCommand9 = Database.SEVENTH_MAGIC;

            this.tc.AvailableMana = true;
            this.tc.AvailableSkill = true;

            this.tc.CurrentLife = this.tc.MaxLife;
            this.tc.CurrentSkillPoint = this.tc.MaxSkillPoint;
            this.tc.CurrentMana = this.tc.MaxMana;

            this.tc.FireBall = true;
            this.tc.FlameAura = true;
            this.tc.HeatBoost = true;
            this.tc.FlameStrike = true;
            this.tc.VolcanicWave = true;
            this.tc.ImmortalRave = true;
            this.tc.LavaAnnihilation = true;                 
            this.tc.DarkBlast = true;
            this.tc.ShadowPact = true;
            this.tc.LifeTap = true;
            this.tc.BlackContract = true;
            this.tc.DevouringPlague = true;
            this.tc.BloodyVengeance = true;
            this.tc.Damnation = true;            
            this.tc.DispelMagic = true;
            this.tc.RiseOfImage = true;
            this.tc.Deflection = true;
            this.tc.Tranquility = true;
            this.tc.OneImmunity = true;
            this.tc.WhiteOut = true;
            this.tc.TimeStop = true;
            
            this.tc.StraightSmash = true;
            this.tc.DoubleSlash = true;
            this.tc.CrushingBlow = true;
            this.tc.SoulInfinity = true;
            this.tc.StanceOfStanding = true;
            this.tc.InnerInspiration = true;
            this.tc.KineticSmash = true;
            this.tc.Catastrophe = true;                
            this.tc.Negate = true;
            this.tc.VoidExtraction = true;
            this.tc.CarnageRush = true;
            this.tc.NothingOfNothingness = true;

            this.tc.BlackFire = true;
            this.tc.BlazingField = true;
            this.tc.DemonicIgnite = true;
            this.tc.Immolate = true;
            this.tc.PhantasmalWind = true;
            this.tc.RedDragonWill = true;
            this.tc.DarkenField = true;
            this.tc.DoomBlade = true;
            this.tc.EclipseEnd = true;            
            this.tc.WordOfMalice = true;
            this.tc.AbyssEye = true;
            this.tc.SinFortune = true;
            this.tc.EnrageBlast = true;
            this.tc.PiercingFlame = true;
            this.tc.SigilOfHomura = true;
            this.tc.SeventhMagic = true;
            this.tc.ParadoxImage = true;
            this.tc.WarpGate = true;

            this.tc.SwiftStep = true;
            this.tc.VigorSense = true;
            this.tc.Recover = true;
            this.tc.ImpulseHit = true;
            this.tc.SurpriseAttack = true;
            this.tc.StanceOfMystic = true;
            this.tc.CircleSlash = true;
            this.tc.RisingAura = true;
            this.tc.OuterInspiration = true;
            this.tc.HardestParry = true;
            this.tc.SmoothingMove = true;
            this.tc.AscensionAura = true;
//            if (this.tc.MainWeapon != null) { if (this.tc.MainWeapon.Name == Database.POOR_GOD_FIRE_GLOVE_REPLICA) { this.tc.MainWeapon = new ItemBackPack(Database.LEGENDARY_GOD_FIRE_GLOVE); } }

            // 3人目
            this.b_tc = new MainCharacter();
            this.b_tc.FullName = Database.VERZE_ARTIE_FULL;
            this.b_tc.Name = Database.VERZE_ARTIE;
            this.b_tc.Strength = Database.VERZE_ARTIE_STRENGTH_3;
            this.b_tc.Agility = Database.VERZE_ARTIE_AGILITY_3;
            this.b_tc.Intelligence = Database.VERZE_ARTIE_INTELLIGENCE_3;
            this.b_tc.Stamina = Database.VERZE_ARTIE_STAMINA_3;
            this.b_tc.Mind = Database.VERZE_ARTIE_MIND_3;
            this.b_tc.Level = 0;
            this.b_tc.Exp = 0;
            for (int ii = 0; ii < 65; ii++)
            {
                this.b_tc.BaseLife += this.b_tc.LevelUpLifeTruth;
                this.b_tc.BaseMana += this.b_tc.LevelUpManaTruth;
                this.b_tc.Level++;
            }
            this.b_tc.MainWeapon = new ItemBackPack(Database.EPIC_WHITE_SILVER_SWORD_REPLICA);
            this.b_tc.MainArmor = new ItemBackPack(Database.EPIC_BLACK_AERIAL_ARMOR_REPLICA);
            this.b_tc.Accessory = new ItemBackPack(Database.EPIC_HEAVENLY_SKY_WING_REPLICA);
            this.b_tc.BattleActionCommand1 = Database.NEUTRAL_SMASH;
            this.b_tc.BattleActionCommand2 = Database.INNER_INSPIRATION;
            this.b_tc.BattleActionCommand3 = Database.MIRROR_IMAGE;
            this.b_tc.BattleActionCommand4 = Database.DEFLECTION;
            this.b_tc.BattleActionCommand5 = Database.STANCE_OF_FLOW;
            this.b_tc.BattleActionCommand6 = Database.GALE_WIND;
            this.b_tc.BattleActionCommand7 = Database.STRAIGHT_SMASH;
            this.b_tc.BattleActionCommand8 = Database.SURPRISE_ATTACK;
            this.b_tc.BattleActionCommand9 = Database.NEGATE;
            this.b_tc.AvailableMana = true;
            this.b_tc.AvailableSkill = true;

            this.b_tc.CurrentLife = this.b_tc.MaxLife;
            this.b_tc.BaseSkillPoint = 100;
            this.b_tc.CurrentSkillPoint = 100;
            this.b_tc.CurrentMana = this.b_tc.MaxMana;

            this.b_tc.FireBall = true;
            this.b_tc.StraightSmash = true;
            this.b_tc.CounterAttack = true;
            this.b_tc.FreshHeal = true;
            this.b_tc.StanceOfFlow = true;
            this.b_tc.DispelMagic = true;
            this.b_tc.WordOfPower = true;
            this.b_tc.EnigmaSence = true;
            this.b_tc.BlackContract = true;
            this.b_tc.Cleansing = true;
            this.b_tc.GaleWind = true;
            this.b_tc.Deflection = true;
            this.b_tc.Negate = true;
            this.b_tc.InnerInspiration = true;
            this.b_tc.FrozenLance = true;
            this.b_tc.Tranquility = true;
            this.b_tc.WordOfFortune = true;
            this.b_tc.SkyShield = true;
            this.b_tc.NeutralSmash = true;
            this.b_tc.Glory = true;
            this.b_tc.BlackFire = true;
            this.b_tc.SurpriseAttack = true;
            this.b_tc.MirrorImage = true;
            this.b_tc.WordOfMalice = true;
            this.b_tc.StanceOfSuddenness = true;
            this.b_tc.CrushingBlow = true;
            this.b_tc.Immolate = true;
            this.b_tc.AetherDrive = true;
            this.b_tc.TrustSilence = true;
            this.b_tc.WordOfAttitude = true;
            this.b_tc.OneImmunity = true;
            this.b_tc.AntiStun = true;
            this.b_tc.FutureVision = true;
            this.b_tc.StanceOfEyes = true;
            this.b_tc.SwiftStep = true;
            this.b_tc.Resurrection = true;
            this.b_tc.BlindJustice = true;
            this.b_tc.Genesis = true;
            this.b_tc.DeepMirror = true;
            this.b_tc.ImmortalRave = true;
            this.b_tc.DoomBlade = true;
            this.b_tc.CarnageRush = true;
            this.b_tc.ChillBurn = true;
            this.b_tc.WhiteOut = true;
            this.b_tc.PhantasmalWind = true;
            this.b_tc.PainfulInsanity = true;
            this.b_tc.FatalBlow = true;
            this.b_tc.StaticBarrier = true;
            this.b_tc.StanceOfDeath = true;
            this.b_tc.EverDroplet = true;
            this.b_tc.Catastrophe = true;
            this.b_tc.CelestialNova = true;
            this.b_tc.MindKilling = true;
            this.b_tc.NothingOfNothingness = true;
            this.b_tc.AbsoluteZero = true;
            this.b_tc.AusterityMatrix = true;
            this.b_tc.VigorSense = true;
            this.b_tc.LavaAnnihilation = true;
            this.b_tc.EclipseEnd = true;
            this.b_tc.TimeStop = true;
            this.b_tc.SinFortune = true;
            this.b_tc.DemonicIgnite = true;
            // 以下は65以降
            //this.tc.StanceOfDouble = true;
            //this.tc.WarpGate = true;
            //this.tc.StanceOfMystic = true;
            //this.tc.SoulExecution = true;
            //this.tc.ZetaExplosion = true;



            // 4人目
            this.b_fc = new MainCharacter();
            this.b_fc.FullName = Database.SINIKIA_KAHLHANZ_FULL;
            this.b_fc.Name = Database.SINIKIA_KAHLHANZ;
            this.b_fc.Strength = Database.SINIKIA_KAHLHANTZ_STRENGTH_2;
            this.b_fc.Agility = Database.SINIKIA_KAHLHANTZ_AGILITY_2;
            this.b_fc.Intelligence = Database.SINIKIA_KAHLHANTZ_INTELLIGENCE_2;
            this.b_fc.Stamina = Database.SINIKIA_KAHLHANTZ_STAMINA_2;
            this.b_fc.Mind = Database.SINIKIA_KAHLHANTZ_MIND_2;
            this.b_fc.Level = 0;
            this.b_fc.Exp = 0;
            for (int ii = 0; ii < 65; ii++)
            {
                this.b_fc.BaseLife += this.b_fc.LevelUpLifeTruth;
                this.b_fc.BaseMana += this.b_fc.LevelUpManaTruth;
                this.b_fc.Level++;
            }

            this.b_fc.MainWeapon = new ItemBackPack(Database.EPIC_DARKMAGIC_DEVIL_EYE_2);
            this.b_fc.MainArmor = new ItemBackPack(Database.EPIC_YAMITUYUKUSA_MOON_ROBE_2);
            this.b_fc.Accessory = new ItemBackPack(Database.LEGENDARY_ZVELDOSE_DEVIL_FIRE_RING_2);
            this.b_fc.Accessory2 = new ItemBackPack(Database.LEGENDARY_ANASTELISA_INNOCENT_FIRE_RING_2);
            this.b_fc.BattleActionCommand1 = Database.PIERCING_FLAME;
            this.b_fc.BattleActionCommand2 = Database.CELESTIAL_NOVA;
            this.b_fc.BattleActionCommand3 = Database.SACRED_HEAL;
            this.b_fc.BattleActionCommand4 = Database.GENESIS;
            this.b_fc.BattleActionCommand5 = Database.PROMISED_KNOWLEDGE;
            this.b_fc.BattleActionCommand6 = Database.PSYCHIC_TRANCE;
            this.b_fc.BattleActionCommand7 = Database.DISPEL_MAGIC;
            this.b_fc.BattleActionCommand8 = Database.ONE_IMMUNITY;
            this.b_fc.BattleActionCommand9 = Database.RESURRECTION;
            this.b_fc.AvailableMana = true;
            this.b_fc.AvailableSkill = true;

            this.b_fc.CurrentLife = this.b_fc.MaxLife;
            this.b_fc.BaseSkillPoint = 100;
            this.b_fc.CurrentSkillPoint = this.b_fc.MaxSkillPoint;
            this.b_fc.CurrentMana = this.b_fc.MaxMana;

            this.b_fc.FreshHeal = true;
            this.b_fc.Protection = true;
            this.b_fc.HolyShock = true;
            this.b_fc.SaintPower = true;
            this.b_fc.Glory = true;
            this.b_fc.Resurrection = true;
            this.b_fc.CelestialNova = true;
            this.b_fc.DarkBlast = true;
            this.b_fc.ShadowPact = true;
            this.b_fc.LifeTap = true;
            this.b_fc.BlackContract = true;
            this.b_fc.DevouringPlague = true;
            this.b_fc.BloodyVengeance = true;
            this.b_fc.Damnation = true;
            this.b_fc.FireBall = true;
            this.b_fc.FlameAura = true;
            this.b_fc.HeatBoost = true;
            this.b_fc.FlameStrike = true;
            this.b_fc.VolcanicWave = true;
            this.b_fc.ImmortalRave = true;
            this.b_fc.LavaAnnihilation = true;
            this.b_fc.IceNeedle = true;
            this.b_fc.AbsorbWater = true;
            this.b_fc.Cleansing = true;
            this.b_fc.FrozenLance = true;
            this.b_fc.MirrorImage = true;
            this.b_fc.PromisedKnowledge = true;
            this.b_fc.AbsoluteZero = true;
            this.b_fc.WordOfPower = true;
            this.b_fc.GaleWind = true;
            this.b_fc.WordOfLife = true;
            this.b_fc.WordOfFortune = true;
            this.b_fc.AetherDrive = true;
            this.b_fc.Genesis = true;
            this.b_fc.EternalPresence = true;
            this.b_fc.DispelMagic = true;
            this.b_fc.RiseOfImage = true;
            this.b_fc.Deflection = true;
            this.b_fc.Tranquility = true;
            this.b_fc.OneImmunity = true;
            this.b_fc.WhiteOut = true;
            this.b_fc.TimeStop = true;
            
            this.b_fc.CounterAttack = true;
            this.b_fc.StanceOfFlow = true;
            this.b_fc.Negate = true;

            this.b_fc.PsychicTrance = true;
            this.b_fc.BlindJustice = true;
            this.b_fc.TranscendentWish = true;
            this.b_fc.FlashBlaze = true;
            this.b_fc.LightDetonator = true;
            this.b_fc.AscendantMeteor = true;
            this.b_fc.SkyShield = true;
            this.b_fc.SacredHeal = true;
            this.b_fc.EverDroplet = true;
            this.b_fc.HolyBreaker = true;
            this.b_fc.ExaltedField = true;
            this.b_fc.HymnContract = true;
            this.b_fc.StarLightning = true;
            this.b_fc.AngelBreath = true;
            this.b_fc.EndlessAnthem = true;
            this.b_fc.BlackFire = true;
            this.b_fc.BlazingField = true;
            this.b_fc.DemonicIgnite = true;
            this.b_fc.BlueBullet = true;
            this.b_fc.DeepMirror = true;
            this.b_fc.DeathDeny = true;
            this.b_fc.WordOfMalice = true;
            this.b_fc.AbyssEye = true;
            this.b_fc.SinFortune = true;
            this.b_fc.DarkenField = true;
            this.b_fc.DoomBlade = true;
            this.b_fc.EclipseEnd = true;
            this.b_fc.FrozenAura = true;
            this.b_fc.ChillBurn = true;
            this.b_fc.ZetaExplosion = true;
            this.b_fc.EnrageBlast = true;
            this.b_fc.PiercingFlame = true;
            this.b_fc.SigilOfHomura = true;
            this.b_fc.Immolate = true;
            this.b_fc.PhantasmalWind = true;
            this.b_fc.RedDragonWill = true;
            this.b_fc.WordOfAttitude = true;
            this.b_fc.StaticBarrier = true;
            this.b_fc.AusterityMatrix = true;
            this.b_fc.VanishWave = true;
            this.b_fc.VortexField = true;
            this.b_fc.BlueDragonWill = true;
            this.b_fc.SeventhMagic = true;
            this.b_fc.ParadoxImage = true;
            this.b_fc.WarpGate = true;

            SelectOrAdd(this.sc);
        }

        private void CheckMaxAdd()
        {
            if (this.playerList.Count >= MAX_ADD)
            {
                btnPlayer1.Enabled = false;
                btnPlayer2.Enabled = false;
                btnPlayer3.Enabled = false;
                btnPlayer4.Enabled = false;
                choice.Enabled = false;
                btnFix.Enabled = true;
            }
        }

        private void PlayerAdd(string name)
        {
            Label current = selected1;
            this.playerList.Add(this.currentPlayer);
            if (this.playerList.Count == 1)
            {
                current = selected1;
            }
            else if (this.playerList.Count == 2)
            {
                current = selected2;
            }
            current.Text = this.currentPlayer.FullName;
            if (currentPlayer.FullName == Database.RANA_AMILIA_FULL) { current.BackColor = Database.COLOR_RANA; this.btnUpReset.Visible = false; }
            else if (currentPlayer.FullName == Database.OL_LANDIS_FULL) { current.BackColor = Database.COLOR_OL; this.btnUpReset.Visible = false; }
            else if (currentPlayer.FullName == Database.VERZE_ARTIE_FULL) { current.BackColor = Database.COLOR_VERZE; }
            else if (currentPlayer.FullName == Database.DUEL_SINIKIA_KAHLHANZ) { current.BackColor = Database.COLOR_KAHL; }
            CheckMaxAdd();
        }

        private void UpdateBtnUpReset()
        {
            if (currentPlayer.Equals(this.sc))
            {
                btnUpReset.Visible = !this.choiceSC;
            }
            else if (currentPlayer.Equals(this.tc))
            {
                btnUpReset.Visible = !this.choiceTC;
            }
            else
            {
                btnUpReset.Visible = false;
            }
        }
        private void SelectOrAdd(MainCharacter player)
        {
            if ((this.currentPlayer == null) ||
                (this.currentPlayer.FullName != player.FullName))
            {
                this.currentPlayer = player;
                if (currentPlayer.FullName == Database.RANA_AMILIA_FULL) { this.BackColor = Database.COLOR_RANA; }
                else if (currentPlayer.FullName == Database.OL_LANDIS_FULL) { this.BackColor = Database.COLOR_OL; }
                else if (currentPlayer.FullName == Database.VERZE_ARTIE_FULL) { this.BackColor = Database.COLOR_VERZE; }
                else if (currentPlayer.FullName == Database.DUEL_SINIKIA_KAHLHANZ) { this.BackColor = Database.COLOR_KAHL; }
                SettingCharacterData(player);
                RefreshPartyMembersBattleStatus(player);
            }
            else
            {
                if (this.playerList.Contains(player))
                {
                    // 何もしない
                }
                else
                {
                    if (player.Equals(this.sc))
                    {
                        if (this.remainSC > 0)
                        {
                            mainMessage.Text = "アイン：パラメタを先に割り振ろう";// this.sc.FullName + "のパラメタ割り振りを完了してください。";
                        }
                        else
                        {
                            this.choiceSC = true;
                            PlayerAdd(player.FullName);
                        }
                    }
                    else if (player.Equals(this.tc))
                    {
                        if (this.remainTC > 0)
                        {
                            mainMessage.Text = "アイン：パラメタを先に割り振ろう"; // this.tc.FullName + "のパラメタ割り振りを完了してください。";
                        }
                        else
                        {
                            this.choiceTC = true;
                            PlayerAdd(player.FullName);
                        }
                    }
                    else
                    {
                        PlayerAdd(player.FullName);
                    }
                }
            }
            UpdateBtnUpReset();
        }

        private void btnPlayer1_Click(object sender, EventArgs e)
        {
            SelectOrAdd(this.sc);
        }

        private void btnPlayer2_Click(object sender, EventArgs e)
        {
            SelectOrAdd(this.tc);
        }

        private void btnPlayer3_Click(object sender, EventArgs e)
        {
            SelectOrAdd(this.b_tc);
        }

        private void btnPlayer4_Click(object sender, EventArgs e)
        {
            SelectOrAdd(this.b_fc);
        }

        private void choice_Click(object sender, EventArgs e)
        {
            SelectOrAdd(this.currentPlayer);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            this.playerList.Clear();
            selected1.Text = "";
            selected2.Text = "";
            selected1.BackColor = Color.Cornsilk;
            selected2.BackColor = Color.Cornsilk;
            btnPlayer1.Enabled = true;
            btnPlayer2.Enabled = true;
            btnPlayer3.Enabled = true;
            btnPlayer4.Enabled = true;
            choice.Enabled = true;
            btnFix.Enabled = false;
            this.choiceSC = false;
            this.choiceTC = false;
            UpdateBtnUpReset();
        }

        private void btnFix_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        
        private void SettingCharacterData(MainCharacter chara)
        {
            this.playerName.Text = chara.FullName;
            this.level.Text = chara.Level.ToString();

            this.strength.Text = chara.Strength.ToString();
            this.addStrength.Text = " + " + chara.BuffStrength_Accessory.ToString();
            if (chara.BuffStrength_Food == 0) this.addStrength2.Text = "";
            else this.addStrength2.Text = " + " + chara.BuffStrength_Food.ToString();

            this.agility.Text = chara.Agility.ToString();
            this.addAgility.Text = " + " + chara.BuffAgility_Accessory.ToString();
            if (chara.BuffAgility_Food == 0) this.addAgility2.Text = "";
            else this.addAgility2.Text = " + " + chara.BuffAgility_Food.ToString();

            this.intelligence.Text = chara.Intelligence.ToString();
            this.addIntelligence.Text = " + " + chara.BuffIntelligence_Accessory.ToString();
            if (chara.BuffIntelligence_Food == 0) this.addIntelligence2.Text = "";
            else this.addIntelligence2.Text = " + " + chara.BuffIntelligence_Food.ToString();

            this.stamina.Text = chara.Stamina.ToString();
            this.addStamina.Text = " + " + chara.BuffStamina_Accessory.ToString();
            if (chara.BuffStamina_Food == 0) this.addStamina2.Text = "";
            else this.addStamina2.Text = " + " + chara.BuffStamina_Food.ToString();

            this.mindLabel.Text = chara.Mind.ToString();
            this.addMind.Text = " + " + chara.BuffMind_Accessory.ToString();
            if (chara.BuffMind_Food == 0) this.addMind2.Text = "";
            else this.addMind2.Text = " + " + chara.BuffMind_Food.ToString();

            if (chara.Equals(this.sc) || chara.Equals(this.tc))
            {
                plus1.Visible = true;
                plus10.Visible = true;
                plus100.Visible = true;
                plus1000.Visible = true;
                btnUpReset.Visible = true;
                lblRemain.Visible = true;
                if (chara.Equals(this.sc))
                {
                    lblRemain.Text = "残り　" + this.remainSC.ToString();
                }
                else
                {
                    lblRemain.Text = "残り　" + this.remainTC.ToString();
                }
            }
            else
            {
                plus1.Visible = false;
                plus10.Visible = false;
                plus100.Visible = false;
                plus1000.Visible = false;
                lblRemain.Visible = false;
                //btnUpReset.Visible = false;
            }
            UpdateBtnUpReset();
            RefreshPartyMembersLife();
            this.life.Text = chara.CurrentLife.ToString() + " / " + chara.MaxLife.ToString();

            if (chara.AvailableSkill)
            {
                label24.Visible = true;
                skill.Visible = true;
                if (chara.CurrentSkillPoint > chara.MaxSkillPoint)
                {
                    chara.CurrentSkillPoint = chara.MaxSkillPoint;
                }
                skill.Text = chara.CurrentSkillPoint.ToString() + " / " + chara.MaxSkillPoint.ToString();
            }
            else
            {
                label24.Visible = false;
                skill.Visible = false;
            }

            if (chara.AvailableMana)
            {
                label25.Visible = true;
                mana.Visible = true;
                mana.Text = chara.CurrentMana.ToString() + " / " + chara.MaxMana.ToString();
            }
            else
            {
                label25.Visible = false;
                mana.Visible = false;
            }

            this.weapon.Text = "";
            this.subWeapon.Text = "";
            this.armor.Text = "";
            this.accessory.Text = "";
            this.accessory2.Text = "";
            if (chara.MainWeapon != null)
            {
                if (chara.MainWeapon.Name == "")
                {
                    this.weapon.AutoSize = false;
                    this.weapon.BackColor = Color.Transparent;
                }
                else
                {
                    this.weapon.AutoSize = true;
                    this.weapon.Text = chara.MainWeapon.Name;
                    UpdateLabelColorForRare(ref this.weapon, chara.MainWeapon.Rare);
                }
            }
            else
            {
                this.weapon.AutoSize = false;
                this.weapon.BackColor = Color.Transparent;
            }

            if (chara.SubWeapon != null)
            {
                if (chara.SubWeapon.Name == "")
                {
                    this.subWeapon.AutoSize = false;
                    this.subWeapon.BackColor = Color.Transparent;
                }
                else
                {
                    this.subWeapon.AutoSize = true;
                    this.subWeapon.Text = chara.SubWeapon.Name;
                    UpdateLabelColorForRare(ref this.subWeapon, chara.SubWeapon.Rare);
                }
            }
            else
            {
                this.subWeapon.AutoSize = false;
                this.subWeapon.BackColor = Color.Transparent;
            }

            if (chara.MainArmor != null)
            {
                if (chara.MainArmor.Name == "")
                {
                    this.armor.AutoSize = false;
                    this.armor.BackColor = Color.Transparent;
                }
                else
                {
                    this.armor.AutoSize = true;
                    this.armor.Text = chara.MainArmor.Name;
                    UpdateLabelColorForRare(ref this.armor, chara.MainArmor.Rare);
                }
            }
            else
            {
                this.armor.AutoSize = false;
                this.armor.BackColor = Color.Transparent;
            }

            if (chara.Accessory != null)
            {
                if (chara.Accessory.Name == "")
                {
                    this.accessory.AutoSize = false;
                    this.accessory.BackColor = Color.Transparent;
                }
                else
                {
                    this.accessory.AutoSize = true;
                    this.accessory.Text = chara.Accessory.Name;
                    UpdateLabelColorForRare(ref this.accessory, chara.Accessory.Rare);
                }
            }
            else
            {
                this.accessory.AutoSize = false;
                this.accessory.BackColor = Color.Transparent;
            }

            if (chara.Accessory2 != null)
            {
                if (chara.Accessory2.Name == "")
                {
                    this.accessory2.AutoSize = false;
                    this.accessory2.BackColor = Color.Transparent;
                }
                else
                {
                    this.accessory2.AutoSize = true;
                    this.accessory2.Text = chara.Accessory2.Name;
                    UpdateLabelColorForRare(ref this.accessory2, chara.Accessory2.Rare);
                }
            }
            else
            {
                this.accessory2.AutoSize = false;
                this.accessory2.BackColor = Color.Transparent;
            }
        }

        Point basePhysicalLocation;
        private void RefreshPartyMembersBattleStatus(MainCharacter player)
        {
            double temp1 = 0;
            double temp2 = 0;
            temp1 = PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Min, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false);
            temp2 = PrimaryLogic.PhysicalAttackValue(player, PrimaryLogic.NeedType.Max, 1.0F, 0.0F, 0.0F, 0.0F, 1.0F, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false);
            PhysicalAttck.Text = temp1.ToString("F2");
            PhysicalAttck.Text += " - " + temp2.ToString("F2");

            temp1 = PrimaryLogic.SubAttackValue(player, PrimaryLogic.NeedType.Min, 1.0F, 0, 0, 0, 1.0F, PlayerStance.None, false);
            temp2 = PrimaryLogic.SubAttackValue(player, PrimaryLogic.NeedType.Max, 1.0F, 0, 0, 0, 1.0F, PlayerStance.None, false);
            if (temp1 > 0)
            {
                PhysicalAttck.Location = new Point(this.basePhysicalLocation.X, this.basePhysicalLocation.Y - 10);
                PhysicalAttck.Text += "\r\n" + temp1.ToString("F2");
                PhysicalAttck.Text += " - " + temp2.ToString("F2");
            }
            else
            {
                PhysicalAttck.Location = new Point(this.basePhysicalLocation.X, this.basePhysicalLocation.Y);
            }

            temp1 = PrimaryLogic.PhysicalDefenseValue(player, PrimaryLogic.NeedType.Min, false);
            temp2 = PrimaryLogic.PhysicalDefenseValue(player, PrimaryLogic.NeedType.Max, false);
            PhysicalDefense.Text = temp1.ToString("F2");
            PhysicalDefense.Text += " - " + temp2.ToString("F2");

            temp1 = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Min, 1.0f, 0.0f, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false);
            temp2 = PrimaryLogic.MagicAttackValue(player, PrimaryLogic.NeedType.Max, 1.0f, 0.0f, PlayerStance.None, PrimaryLogic.SpellSkillType.Standard, false, false);
            MagicAttack.Text = temp1.ToString("F2");
            MagicAttack.Text += " - " + temp2.ToString("F2");

            temp1 = PrimaryLogic.MagicDefenseValue(player, PrimaryLogic.NeedType.Min, false);
            temp2 = PrimaryLogic.MagicDefenseValue(player, PrimaryLogic.NeedType.Max, false);
            MagicDefense.Text = temp1.ToString("F2");
            MagicDefense.Text += " - " + temp2.ToString("F2");

            temp1 = PrimaryLogic.BattleSpeedValue(player, false);
            BattleSpeed.Text = temp1.ToString("F2");

            temp1 = PrimaryLogic.BattleResponseValue(player, false);
            BattleResponse.Text = temp1.ToString("F2");

            temp1 = PrimaryLogic.PotentialValue(player, false);
            Potential.Text = temp1.ToString("F2");
        }
        
        private void UpdateLabelColorForRare(ref Label label, ItemBackPack.RareLevel rareLevel)
        {
            switch (rareLevel)
            {
                case ItemBackPack.RareLevel.Poor:
                    label.BackColor = Color.Gray;
                    label.ForeColor = Color.White;
                    break;
                case ItemBackPack.RareLevel.Common:
                    label.BackColor = Color.Green;
                    label.ForeColor = Color.White;
                    break;
                case ItemBackPack.RareLevel.Rare:
                    label.BackColor = Color.DarkBlue;
                    label.ForeColor = Color.White;
                    break;
                case ItemBackPack.RareLevel.Epic:
                    label.BackColor = Color.Purple;
                    label.ForeColor = Color.White;
                    break;
                case ItemBackPack.RareLevel.Legendary:
                    label.BackColor = Color.OrangeRed;
                    label.ForeColor = Color.White;
                    break;
            }
        }
        
        private void RefreshPartyMembersLife()
        {
            this.life.Text = this.currentPlayer.CurrentLife.ToString() + " / " + this.currentPlayer.MaxLife.ToString();
        }

        private void StatusPlayer_MouseEnter(object sender, EventArgs e)
        {
            ItemBackPack temp = new ItemBackPack(((Label)sender).Text);
            if (temp.Description != "")
            {
                mainMessage.Text = temp.Description;
                return;
            }
        }

        private void StatusPlayer_MouseLeave(object sender, EventArgs e)
        {
        }

        enum upType
        {
            Strength,
            Agility,
            Intelligence,
            Stamina,
            Mind
        }
        private void GoLevelUpPoint(upType type, int plus, ref MainCharacter player, ref int remain, ref int addStr, ref int addAgl, ref int addInt, ref int addStm, ref int addMnd)
        {
            if (remain > 0 && remain >= plus)
            {
                remain -= plus;
                if (type == upType.Strength)
                {
                    addStr += plus;
                    player.Strength += plus;
                    strength.Text = player.Strength.ToString(); 
                }
                else if (type == upType.Agility) 
                {
                    addAgl += plus;
                    player.Agility += plus;
                    agility.Text = player.Agility.ToString(); 
                }
                else if (type == upType.Intelligence)
                {
                    addInt += plus;
                    player.Intelligence += plus;
                    intelligence.Text = player.Intelligence.ToString(); 
                }
                else if (type == upType.Stamina)
                {
                    addStm += plus;
                    player.Stamina += plus;
                    stamina.Text = player.Stamina.ToString();
                }
                else if (type == upType.Mind)
                {
                    addMnd += plus;
                    player.Mind += plus;
                    mindLabel.Text = player.Mind.ToString();
                }
                RefreshPartyMembersBattleStatus(player);
                RefreshPartyMembersLife();
                lblRemain.Text = "残り " + remain.ToString();
            }
        }
        upType number = upType.Strength;
        private void buttonStrength_Click(object sender, EventArgs e)
        {
            this.number = upType.Strength;
            grpParameter.Invalidate();
        }

        private void buttonAgility_Click(object sender, EventArgs e)
        {
            this.number = upType.Agility;
            grpParameter.Invalidate();
        }

        private void buttonIntelligence_Click(object sender, EventArgs e)
        {
            this.number = upType.Intelligence;
            grpParameter.Invalidate();
        }

        private void buttonStamina_Click(object sender, EventArgs e)
        {
            this.number = upType.Stamina;
            grpParameter.Invalidate();
        }

        private void buttonMind_Click(object sender, EventArgs e)
        {
            this.number = upType.Mind;
            grpParameter.Invalidate();
        }

        private void plus1_Click(object sender, EventArgs e)
        {
            int plus = 0;
            if ((sender.Equals(plus1))) { plus = 1; }
            else if ((sender.Equals(plus10))) { plus = 10; }
            else if ((sender.Equals(plus100))) { plus = 100; }
            else if ((sender.Equals(plus1000))) { plus = 1000; }

            if (currentPlayer.Equals(this.sc))
            {
                GoLevelUpPoint(this.number, plus, ref this.sc, ref this.remainSC, ref this.addStrSC, ref this.addAglSC, ref this.addIntSC, ref this.addStmSC, ref this.addMndSC);
            }
            else if (currentPlayer.Equals(this.tc))
            {
                GoLevelUpPoint(this.number, plus, ref this.tc, ref this.remainTC, ref this.addStrTC, ref this.addAglTC, ref this.addIntTC, ref this.addStmTC, ref this.addMndTC);
            }
        }

        private void buttonStrength_Paint(object sender, PaintEventArgs e)
        {
        }

        private void grpParameter_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            int BluePenWidth = 4;
            int SkyBluePenWidth = 2;
            Pen BluePen = new Pen(Brushes.DarkBlue, BluePenWidth);
            Pen SkyBluePen = new Pen(Brushes.Blue, SkyBluePenWidth);
            int basePosX = 0; // 味方側のＸライン
            int basePosY = 0; // 味方：１人目のYライン(
            int len = 58; // 四角枠の横長さ
            int len2 = 54; // 四角枠の縦長さ

            if (this.number == upType.Strength) { basePosX = buttonStrength.Location.X; basePosY = buttonStrength.Location.Y; }
            else if (this.number == upType.Agility) { basePosX = buttonAgility.Location.X; basePosY = buttonAgility.Location.Y; }
            else if (this.number == upType.Intelligence) { basePosX = buttonIntelligence.Location.X; basePosY = buttonIntelligence.Location.Y; }
            else if (this.number == upType.Stamina) { basePosX = buttonStamina.Location.X; basePosY = buttonStamina.Location.Y; }
            else if (this.number == upType.Mind) { basePosX = buttonMind.Location.X; basePosY = buttonMind.Location.Y; }

            g.DrawRectangle(BluePen, new Rectangle(basePosX - 4, basePosY - 4, len, len));
            g.DrawRectangle(SkyBluePen, new Rectangle(basePosX - 2, basePosY - 2, len2, len2));
        }

        private void ResetParameter(ref MainCharacter player, ref int remain, ref int addStr, ref int addAgl, ref int addInt, ref int addStm, ref int addMnd)
        {
            remain += addStr; player.Strength -= addStr; addStr = 0;
            remain += addAgl; player.Agility -= addAgl; addAgl = 0;
            remain += addInt; player.Intelligence -= addInt; addInt = 0;
            remain += addStm; player.Stamina -= addStm; addStm = 0;
            remain += addMnd; player.Mind -= addMnd; addMnd = 0;
            lblRemain.Text = "残り　" + remain.ToString();
        }
        private void btnUpReset_Click(object sender, EventArgs e)
        {
            if (this.currentPlayer.Equals(this.sc))
            {
                ResetParameter(ref this.sc, ref this.remainSC, ref this.addStrSC, ref this.addAglSC, ref this.addIntSC, ref this.addStmSC, ref this.addMndSC);
            }
            else if (this.currentPlayer.Equals(this.tc))
            {
                ResetParameter(ref this.tc, ref this.remainTC, ref this.addStrTC, ref this.addAglTC, ref this.addIntTC, ref this.addStmTC, ref this.addMndTC);
            }
            SettingCharacterData(this.currentPlayer);
            RefreshPartyMembersBattleStatus(this.currentPlayer);
            RefreshPartyMembersLife();
        }
    }
}
