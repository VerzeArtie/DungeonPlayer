using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Reflection;
using System.Windows.Forms;

namespace DungeonPlayer
{
    public class Method
    {
        public static void ShowActiveSkillSpell(MainCharacter player, string skillSpellName)
        {
            using (TruthSkillSpellDesc skillSpell = new TruthSkillSpellDesc())
            {
                skillSpell.StartPosition = FormStartPosition.CenterParent;
                skillSpell.SkillSpellName = skillSpellName;
                skillSpell.Player = player;
                skillSpell.ShowDialog();
            }
        }

        public static void PlaySoundEffect(string soundName, bool enableSoundEffect, bool enableBGM)
        {
            try
            {
                if (enableSoundEffect)
                {
                    if (GroundOne.sound == null)
                    {
                        GroundOne.sound = new XepherPlayer();
                    }
                    GroundOne.sound.PlayMP3(soundName);
                }
            }
            catch
            {
                enableSoundEffect = false;
                enableBGM = false;
                System.Windows.Forms.MessageBox.Show(Database.InstallComponentError);
            }
        }

        public static void PlayDungeonMusic(string targetMusicName, int loopBegin, bool enableSoundEffect, bool enableBGM)
        {
            try
            {
                if (enableBGM)
                {
                    if (GroundOne.sound == null)
                    {
                        GroundOne.sound = new XepherPlayer();
                    }
                    else
                    {
                        GroundOne.sound.StopMusic();
                    }
                    GroundOne.sound.PlayMusic(targetMusicName, loopBegin);
                }
            }
            catch
            {
                enableSoundEffect = false;
                enableBGM = false;
                System.Windows.Forms.MessageBox.Show(Database.InstallComponentError);
            }
        }

        // 戦闘終了後のレベルアップと、ホームタウンのデバッグ用レベルアップを結合
        public static void LevelUpCharacter(WorldEnvironment we, MainCharacter mc, MainCharacter sc, MainCharacter tc, bool alreadyPlayBackMusic, System.Drawing.Color color)
        {
            int levelUpPoint = 0;
            int cumultiveLvUpValue = 0;
            while (true)
            {
                if (mc.Exp >= mc.NextLevelBorder && mc.Level < Database.CHARACTER_MAX_LEVEL5)
                {
                    levelUpPoint += mc.LevelUpPointTruth;
                    mc.BaseLife += mc.LevelUpLifeTruth;
                    mc.BaseMana += mc.LevelUpManaTruth;
                    mc.Exp = mc.Exp - mc.NextLevelBorder;
                    mc.Level += 1;
                    cumultiveLvUpValue++;
                }
                else
                {
                    break;
                }
            }

            if (cumultiveLvUpValue > 0)
            {
                PlaySoundEffect("LvUp.mp3", true, true);
                if (!alreadyPlayBackMusic)
                {
                    alreadyPlayBackMusic = true;
                    PlayDungeonMusic(Database.BGM14, Database.BGM14LoopBegin, true, true);
                }
                using (TruthStatusPlayer sp = new TruthStatusPlayer())
                {
                    sp.WE = we;
                    sp.MC = mc;
                    sp.SC = sc;
                    sp.TC = tc;
                    sp.CurrentStatusView = color;
                    sp.LevelUp = true;
                    sp.UpPoint = levelUpPoint;
                    sp.CumultiveLvUpValue = cumultiveLvUpValue;
                    sp.StartPosition = FormStartPosition.CenterParent;
                    sp.ShowDialog();
                }

                #region "アイン・レベルアップ習得表"
                if ((mc.Level >= 3) && (!mc.StraightSmash)) { mc.AvailableSkill = true; mc.StraightSmash = true; ShowActiveSkillSpell(mc, Database.STRAIGHT_SMASH); mc.BattleActionCommand3 = Database.STRAIGHT_SMASH; }
                if ((mc.Level >= 4) && (!mc.FreshHeal)) { mc.AvailableMana = true; mc.FreshHeal = true; ShowActiveSkillSpell(mc, Database.FRESH_HEAL); mc.BattleActionCommand4 = Database.FRESH_HEAL; }
                if ((mc.Level >= 5) && (!mc.FireBall)) { mc.FireBall = true; ShowActiveSkillSpell(mc, Database.FIRE_BALL); mc.BattleActionCommand5 = Database.FIRE_BALL; }
                if ((mc.Level >= 6) && (!mc.Protection)) { mc.Protection = true; ShowActiveSkillSpell(mc, Database.PROTECTION); mc.BattleActionCommand6 = Database.PROTECTION; }
                if ((mc.Level >= 7) && (!mc.DoubleSlash)) { mc.DoubleSlash = true; ShowActiveSkillSpell(mc, Database.DOUBLE_SLASH); mc.BattleActionCommand7 = Database.DOUBLE_SLASH; }
                if ((mc.Level >= 8) && (!mc.FlameAura)) { mc.FlameAura = true; ShowActiveSkillSpell(mc, Database.FLAME_AURA); mc.BattleActionCommand8 = Database.FLAME_AURA; }
                if ((mc.Level >= 9) && (!mc.StanceOfStanding)) { mc.StanceOfStanding = true; ShowActiveSkillSpell(mc, Database.STANCE_OF_STANDING); mc.BattleActionCommand9 = Database.STANCE_OF_STANDING; }
                if ((mc.Level >= 10) && (!mc.WordOfPower)) { mc.WordOfPower = true; ShowActiveSkillSpell(mc, Database.WORD_OF_POWER); }
                if ((mc.Level >= 11) && (!mc.HolyShock)) { mc.HolyShock = true; ShowActiveSkillSpell(mc, Database.HOLY_SHOCK); }
                if ((mc.Level >= 12) && (!mc.TruthVision)) { mc.TruthVision = true; ShowActiveSkillSpell(mc, Database.TRUTH_VISION); }
                if ((mc.Level >= 13) && (!mc.HeatBoost)) { mc.HeatBoost = true; ShowActiveSkillSpell(mc, Database.HEAT_BOOST); }
                if ((mc.Level >= 14) && (!mc.SaintPower)) { mc.SaintPower = true; ShowActiveSkillSpell(mc, Database.SAINT_POWER); }
                if ((mc.Level >= 15) && (!mc.GaleWind)) { mc.GaleWind = true; ShowActiveSkillSpell(mc, Database.GALE_WIND); }
                if ((mc.Level >= 16) && (!mc.InnerInspiration)) { mc.InnerInspiration = true; ShowActiveSkillSpell(mc, Database.INNER_INSPIRATION); }
                if ((mc.Level >= 17) && (!mc.WordOfLife)) { mc.WordOfLife = true; ShowActiveSkillSpell(mc, Database.WORD_OF_LIFE); }
                if ((mc.Level >= 18) && (!mc.FlameStrike)) { mc.FlameStrike = true; ShowActiveSkillSpell(mc, Database.FLAME_STRIKE); }
                if ((mc.Level >= 19) && (!mc.HighEmotionality)) { mc.HighEmotionality = true; ShowActiveSkillSpell(mc, Database.HIGH_EMOTIONALITY); }
                if ((mc.Level >= 20) && (!mc.WordOfFortune)) { mc.WordOfFortune = true; ShowActiveSkillSpell(mc, Database.WORD_OF_FORTUNE); }
                // [警告] ここで一気にレベルを挙げられると、複合魔法・スキルの習得に違和感が出てしまう。
                // 複合魔法・スキルはガンツ武具屋のテレポート先、カール爵より習得するようにする。
                if ((mc.Level >= 24) && (!mc.Glory)) { mc.Glory = true; ShowActiveSkillSpell(mc, Database.GLORY); }
                if ((mc.Level >= 25) && (!mc.VolcanicWave)) { mc.VolcanicWave = true; ShowActiveSkillSpell(mc, Database.VOLCANIC_WAVE); }
                if ((mc.Level >= 26) && (!mc.AetherDrive)) { mc.AetherDrive = true; ShowActiveSkillSpell(mc, Database.AETHER_DRIVE); }

                if ((mc.Level >= 36) && (!mc.CrushingBlow)) { mc.CrushingBlow = true; ShowActiveSkillSpell(mc, Database.CRUSHING_BLOW); }
                if ((mc.Level >= 37) && (!mc.KineticSmash)) { mc.KineticSmash = true; ShowActiveSkillSpell(mc, Database.KINETIC_SMASH); }
                if ((mc.Level >= 38) && (!mc.StanceOfEyes)) { mc.StanceOfEyes = true; ShowActiveSkillSpell(mc, Database.STANCE_OF_EYES); }
                if ((mc.Level >= 39) && (!mc.Resurrection)) { mc.Resurrection = true; ShowActiveSkillSpell(mc, Database.RESURRECTION); }
                if ((mc.Level >= 41) && (!mc.StaticBarrier)) { mc.StaticBarrier = true; ShowActiveSkillSpell(mc, Database.STATIC_BARRIER); }
                if ((mc.Level >= 42) && (!mc.Genesis)) { mc.Genesis = true; ShowActiveSkillSpell(mc, Database.GENESIS); }
                if ((mc.Level >= 43) && (!mc.LightDetonator)) { mc.LightDetonator = true; ShowActiveSkillSpell(mc, Database.LIGHT_DETONATOR); }
                if ((mc.Level >= 44) && (!mc.ImmortalRave)) { mc.ImmortalRave = true; ShowActiveSkillSpell(mc, Database.IMMORTAL_RAVE); }
                if ((mc.Level >= 45) && (!mc.ExaltedField)) { mc.ExaltedField = true; ShowActiveSkillSpell(mc, Database.EXALTED_FIELD); }
                if ((mc.Level >= 46) && (!mc.PiercingFlame)) { mc.PiercingFlame = true; ShowActiveSkillSpell(mc, Database.PIERCING_FLAME); }
                if ((mc.Level >= 47) && (!mc.SacredHeal)) { mc.SacredHeal = true; ShowActiveSkillSpell(mc, Database.SACRED_HEAL); }
                if ((mc.Level >= 48) && (!mc.RisingAura)) { mc.RisingAura = true; ShowActiveSkillSpell(mc, Database.RISING_AURA); }
                if ((mc.Level >= 49) && (!mc.ChillBurn)) { mc.ChillBurn = true; ShowActiveSkillSpell(mc, Database.CHILL_BURN); }
                if ((mc.Level >= 50) && (!mc.SoulInfinity)) { mc.SoulInfinity = true; ShowActiveSkillSpell(mc, Database.SOUL_INFINITY); }

                if ((mc.Level >= 51) && (!mc.HymnContract)) { mc.HymnContract = true; ShowActiveSkillSpell(mc, Database.HYMN_CONTRACT); }
                if ((mc.Level >= 52) && (!mc.Catastrophe)) { mc.Catastrophe = true; ShowActiveSkillSpell(mc, Database.CATASTROPHE); }
                if ((mc.Level >= 53) && (!mc.CelestialNova)) { mc.CelestialNova = true; ShowActiveSkillSpell(mc, Database.CELESTIAL_NOVA); }
                if ((mc.Level >= 54) && (!mc.OnslaughtHit)) { mc.OnslaughtHit = true; ShowActiveSkillSpell(mc, Database.ONSLAUGHT_HIT); }
                if ((mc.Level >= 55) && (!mc.PainfulInsanity)) { mc.PainfulInsanity = true; ShowActiveSkillSpell(mc, Database.PAINFUL_INSANITY); }
                if ((mc.Level >= 56) && (!mc.LavaAnnihilation)) { mc.LavaAnnihilation = true; ShowActiveSkillSpell(mc, Database.LAVA_ANNIHILATION); }
                if ((mc.Level >= 57) && (!mc.ConcussiveHit)) { mc.ConcussiveHit = true; ShowActiveSkillSpell(mc, Database.CONCUSSIVE_HIT); }
                if ((mc.Level >= 58) && (!mc.EternalPresence)) { mc.EternalPresence = true; ShowActiveSkillSpell(mc, Database.ETERNAL_PRESENCE); }
                if ((mc.Level >= 59) && (!mc.AusterityMatrix)) { mc.AusterityMatrix = true; ShowActiveSkillSpell(mc, Database.AUSTERITY_MATRIX); }
                if ((mc.Level >= 60) && (!mc.SigilOfHomura)) { mc.SigilOfHomura = true; ShowActiveSkillSpell(mc, Database.SIGIL_OF_HOMURA); }

                if ((mc.Level >= 61) && (!mc.EverDroplet)) { mc.EverDroplet = true; ShowActiveSkillSpell(mc, Database.EVER_DROPLET); }
                if ((mc.Level >= 62) && (!mc.ONEAuthority)) { mc.ONEAuthority = true; ShowActiveSkillSpell(mc, Database.ONE_AUTHORITY); }
                if ((mc.Level >= 63) && (!mc.AscendantMeteor)) { mc.AscendantMeteor = true; ShowActiveSkillSpell(mc, Database.ASCENDANT_METEOR); }
                if ((mc.Level >= 64) && (!mc.FatalBlow)) { mc.FatalBlow = true; ShowActiveSkillSpell(mc, Database.FATAL_BLOW); }
                if ((mc.Level >= 65) && (!mc.StanceOfDouble)) { mc.StanceOfDouble = true; ShowActiveSkillSpell(mc, Database.STANCE_OF_DOUBLE); }
                if ((mc.Level >= 66) && (!mc.ZetaExplosion)) { mc.ZetaExplosion = true; ShowActiveSkillSpell(mc, Database.ZETA_EXPLOSION); }
                #endregion
            }
        }
        
        // 現実世界の自動セーブ
        public static void AutoSaveRealWorld(MainCharacter MC, MainCharacter SC, MainCharacter TC, WorldEnvironment WE, bool[] knownTileInfo, bool[] knownTileInfo2, bool[] knownTileInfo3, bool[] knownTileInfo4, bool[] knownTileInfo5, bool[] Truth_KnownTileInfo, bool[] Truth_KnownTileInfo2, bool[] Truth_KnownTileInfo3, bool[] Truth_KnownTileInfo4, bool[] Truth_KnownTileInfo5)
        {
            using (SaveLoad sl = new SaveLoad())
            {
                sl.MC = MC;
                sl.SC = SC;
                sl.TC = TC;
                sl.WE = WE;
                sl.KnownTileInfo = knownTileInfo;
                sl.KnownTileInfo2 = knownTileInfo2;
                sl.KnownTileInfo3 = knownTileInfo3;
                sl.KnownTileInfo4 = knownTileInfo4;
                sl.KnownTileInfo5 = knownTileInfo5;
                sl.Truth_KnownTileInfo = Truth_KnownTileInfo;
                sl.Truth_KnownTileInfo2 = Truth_KnownTileInfo2;
                sl.Truth_KnownTileInfo3 = Truth_KnownTileInfo3;
                sl.Truth_KnownTileInfo4 = Truth_KnownTileInfo4;
                sl.Truth_KnownTileInfo5 = Truth_KnownTileInfo5;
                sl.SaveMode = true;
                sl.RealWorldSave();
            }
        }
        // 通常セーブ、現実世界の自動セーブ、タイトルSeekerモードの自動セーブを結合
        public static void AutoSaveTruthWorldEnvironment()
        {
            XmlTextWriter xmlWriter2 = new XmlTextWriter(Database.WE2_FILE, Encoding.UTF8);
            try
            {
                xmlWriter2.WriteStartDocument();
                xmlWriter2.WriteWhitespace("\r\n");

                xmlWriter2.WriteStartElement("Body");
                xmlWriter2.WriteElementString("DateTime", DateTime.Now.ToString());
                xmlWriter2.WriteWhitespace("\r\n");
                    
                // ワールド環境
                xmlWriter2.WriteStartElement("TruthWorldEnvironment");
                xmlWriter2.WriteWhitespace("\r\n");
                if (GroundOne.WE2 != null)
                {
                    Type typeWE2 = GroundOne.WE2.GetType();
                    foreach (PropertyInfo pi in typeWE2.GetProperties())
                    {
                        if (pi.PropertyType == typeof(System.Int32))
                        {
                            xmlWriter2.WriteElementString(pi.Name, ((System.Int32)(pi.GetValue(GroundOne.WE2, null))).ToString());
                            xmlWriter2.WriteWhitespace("\r\n");
                        }
                        else if (pi.PropertyType == typeof(System.String))
                        {
                            xmlWriter2.WriteElementString(pi.Name, (string)(pi.GetValue(GroundOne.WE2, null)));
                            xmlWriter2.WriteWhitespace("\r\n");
                        }
                        else if (pi.PropertyType == typeof(System.Boolean))
                        {
                            xmlWriter2.WriteElementString(pi.Name, ((System.Boolean)pi.GetValue(GroundOne.WE2, null)).ToString());
                            xmlWriter2.WriteWhitespace("\r\n");
                        }
                    }
                }
                xmlWriter2.WriteEndElement();
                xmlWriter2.WriteWhitespace("\r\n");

                xmlWriter2.WriteEndElement();
                xmlWriter2.WriteWhitespace("\r\n");
                xmlWriter2.WriteEndDocument();
            }
            finally
            {
                xmlWriter2.Close();
            }
        }

        public enum NewItemCategory
        {
            Battle,
            Lottery,
        }

        // 戦闘終了後のアイテムゲット、ファージル宮殿お楽しみ抽選券のアイテムゲットを統合
        public static string GetNewItem(NewItemCategory category, MainCharacter mc, TruthEnemyCharacter ec1 = null, int dungeonArea = 0)
        {
            string targetItemName = String.Empty;
            int debugCounter1 = 0;
            int debugCounter2 = 0;
            int debugCounter3 = 0;
            int debugCounter4 = 0;
            int debugCounter5 = 0;
            int debugCounter6 = 0;
            int debugCounter7 = 0;

            int debugCounter8 = 0;

            for (int zzz = 0; zzz < 1; zzz++)
            {
                System.Threading.Thread.Sleep(1);

                // ドロップアイテムを出現させる
                Random rd = new Random(Environment.TickCount * DateTime.Now.Millisecond);
                int param1 = 1000; // 素材
                int param2 = 600; // 武具POOR
                int param3 = 350; // 武具COMMON
                int param4 = 50; // 武具RARE
                int param5 = 20; // パラメタUP
                int param6 = 10; // EPIC
                int param7 = 200; // ハズレ

                // param1 は固定でいくこと
                // param2 + param3 + param4 は1000とすること
                // param7はBlack以外は0とすること
                if (ec1 != null)
                {
                    switch (ec1.Rare)
                    {
                        case TruthEnemyCharacter.RareString.Black:
                            param1 = 1000;
                            param2 = 600;
                            param3 = 350;
                            param4 = 50;
                            param5 = 20;
                            param6 = 10 + GroundOne.WE2.KillingEnemy; // EPICを少し出しやすくする味付け
                            param7 = 200;
                            break;
                        case TruthEnemyCharacter.RareString.Blue:
                            param1 = 1000;
                            param2 = 100;
                            param3 = 700;
                            param4 = 200;
                            param5 = 60;
                            param6 = 20 + GroundOne.WE2.KillingEnemy * 3; // EPICを少し出しやすくする味付け
                            param7 = 0;
                            break;
                        case TruthEnemyCharacter.RareString.Red:
                            param1 = 1000;
                            param2 = 0;
                            param3 = 500;
                            param4 = 500;
                            param5 = 120;
                            param6 = 40 + GroundOne.WE2.KillingEnemy * 5; // EPICを少し出しやすくする味付け
                            param7 = 0;
                            break;
                        case TruthEnemyCharacter.RareString.Gold: // 階層ボスは固定ドロップアイテムだが、通常ボスはランダム扱い
                            param1 = 0; // ボスレベルで素材は無い事とする。
                            param2 = 0;
                            param3 = 600;
                            param4 = 1200;
                            param5 = 400;
                            param6 = 80 + GroundOne.WE2.KillingEnemy * 5; // EPICを少し出しやすくする味付け
                            param7 = 0;
                            break;
                        case TruthEnemyCharacter.RareString.Purple: // 支配竜は固定ドロップアイテム
                            break;
                    }
                }
                else if (category == NewItemCategory.Lottery)
                {
                    param1 = 0; // 抽選券、モンスター素材ではない。
                    param2 = 0; // 抽選券、POORは無しとする
                    param3 = 600;
                    param4 = 240;
                    param5 = 100;
                    param6 = 7;
                    param7 = 0; // 抽選券、ハズレは無しとする
                    debugCounter8++;
                }

                int randomValue = rd.Next(1, param1 + param2 + param3 + param4 + param5 + param6 + param7 + 1);
                int randomValue2 = rd.Next(1, 1 + param1 + param2 + param3 + param4);
                int randomValue21 = rd.Next(1, 19);
                int randomValue22 = rd.Next(1, 11);
                int randomValue3 = rd.Next(1, 17);
                int randomValue32 = rd.Next(1, 26);
                int randomValue4 = rd.Next(1, 6);
                int randomValue42 = rd.Next(1, 9);
                int randomValue5 = rd.Next(1, 6);
                int randomValue6 = rd.Next(1, 3);
                int randomValue7 = rd.Next(1, 101);
                #region "エリア毎のアイテム総数に応じた値を設定"
                // 1階は上述宣言時の値そのもの
                if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area11) ||
                    (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area12) ||
                    (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area13) ||
                    (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area14) ||
                    (category == NewItemCategory.Lottery && dungeonArea == 1))
                {
                    randomValue21 = rd.Next(1, 19);
                    randomValue22 = rd.Next(1, 11);
                    randomValue3 = rd.Next(1, 17);
                    randomValue32 = rd.Next(1, 26);
                    randomValue4 = rd.Next(1, 6);
                    randomValue42 = rd.Next(1, 9);
                    randomValue5 = rd.Next(1, 6);
                    randomValue6 = rd.Next(1, 3);
                    randomValue7 = rd.Next(1, 101);
                }
                // 2階
                else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area21) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area22) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area23) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area24) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss21) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss22) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss23) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss24) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss25) ||
                         (category == NewItemCategory.Lottery && dungeonArea == 2))
                {
                    randomValue21 = rd.Next(1, 10);
                    randomValue22 = rd.Next(1, 10);
                    randomValue3 = rd.Next(1, 18);
                    randomValue32 = rd.Next(1, 28);
                    randomValue4 = rd.Next(1, 6);
                    randomValue42 = rd.Next(1, 16);
                    randomValue5 = rd.Next(1, 6);
                    randomValue6 = rd.Next(1, 4);
                    randomValue7 = rd.Next(1, 101);
                }
                // 3階
                else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area31) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area32) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area33) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area34) ||
                         (category == NewItemCategory.Lottery && dungeonArea == 3))
                {
                    randomValue21 = rd.Next(1, 5);
                    randomValue22 = rd.Next(1, 5);
                    randomValue3 = rd.Next(1, 14);
                    randomValue32 = rd.Next(1, 32);
                    randomValue4 = rd.Next(1, 7);
                    randomValue42 = rd.Next(1, 24);
                    randomValue5 = rd.Next(1, 6);
                    randomValue6 = rd.Next(1, 4);
                    randomValue7 = rd.Next(1, 101);
                }
                // 4階
                else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area41) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area42) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area43) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area44) ||
                         (category == NewItemCategory.Lottery && dungeonArea == 4))
                {
                    randomValue21 = 0;
                    randomValue22 = 0;
                    randomValue3 = rd.Next(1, 23);
                    randomValue32 = rd.Next(1, 29);
                    randomValue4 = rd.Next(1, 11);
                    randomValue42 = rd.Next(1, 27);
                    randomValue5 = rd.Next(1, 6);
                    randomValue6 = rd.Next(1, 6);
                    randomValue7 = rd.Next(1, 101);
                }
                // 現実世界４層ラスト
                else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area46) ||
                         (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area51))
                {
                    param1 = 0;
                    param2 = 0;

                    randomValue21 = 0;
                    randomValue22 = 0;
                    randomValue3 = rd.Next(1, 16);
                    randomValue32 = 0;
                    randomValue4 = rd.Next(1, 6);
                    randomValue42 = 0;
                    randomValue5 = rd.Next(1, 6);
                    randomValue6 = rd.Next(1, 6);
                    randomValue7 = 0;
                }
                #endregion

                #region "モンスター毎の素材ドロップ"
                if (1 <= randomValue && randomValue <= param1) // 44.84 %
                {
                    int DropItemNumber = 0;
                    for (int ii = 0; ii < ec1.DropItem.Length; ii++)
                    {
                        if (ec1.DropItem[ii] != String.Empty)
                        {
                            DropItemNumber++;
                        }
                    }
                    if (DropItemNumber <= 0) // 素材登録が無い場合、ハズレ
                    {
                        targetItemName = String.Empty;
                    }
                    else
                    {
                        int randomValue1 = AP.Math.RandomInteger(DropItemNumber);
                        targetItemName = ec1.DropItem[randomValue1];
                    }

                    debugCounter1++;
                }
                #endregion
                #region "ダンジョンエリア毎の汎用装備品"
                else if (param1 < randomValue && randomValue <= (param1 + param2 + param3 + param4)) // 44.84%
                {
                    if (1 <= randomValue2 && randomValue2 <= param2) // Poor 60.00%
                    {
                        #region "１階エリア１－２　３－４"
                        if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area11) ||
                            (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area12) ||
                            (category == NewItemCategory.Lottery && dungeonArea == 1))
                        {
                            switch (randomValue21)
                            {
                                case 1:
                                    targetItemName = Database.POOR_HINJAKU_ARMRING;
                                    break;
                                case 2:
                                    targetItemName = Database.POOR_USUYOGORETA_FEATHER;
                                    break;
                                case 3:
                                    targetItemName = Database.POOR_NON_BRIGHT_ORB;
                                    break;
                                case 4:
                                    targetItemName = Database.POOR_KUKEI_BANGLE;
                                    break;
                                case 5:
                                    targetItemName = Database.POOR_SUTERARESHI_EMBLEM;
                                    break;
                                case 6:
                                    targetItemName = Database.POOR_ARIFURETA_STATUE;
                                    break;
                                case 7:
                                    targetItemName = Database.POOR_NON_ADJUST_BELT;
                                    break;
                                case 8:
                                    targetItemName = Database.POOR_SIMPLE_EARRING;
                                    break;
                                case 9:
                                    targetItemName = Database.POOR_KATAKUZURESHITA_FINGERRING;
                                    break;
                                case 10:
                                    targetItemName = Database.POOR_IROASETA_CHOKER;
                                    break;
                                case 11:
                                    targetItemName = Database.POOR_YOREYORE_MANTLE;
                                    break;
                                case 12:
                                    targetItemName = Database.POOR_NON_HINSEI_CROWN;
                                    break;
                                case 13:
                                    targetItemName = Database.POOR_TUKAIFURUSARETA_SWORD;
                                    break;
                                case 14:
                                    targetItemName = Database.POOR_TUKAINIKUI_LONGSWORD;
                                    break;
                                case 15:
                                    targetItemName = Database.POOR_GATAGAKITERU_ARMOR;
                                    break;
                                case 16:
                                    targetItemName = Database.POOR_FESTERING_ARMOR;
                                    break;
                                case 17:
                                    targetItemName = Database.POOR_HINSO_SHIELD;
                                    break;
                                case 18:
                                    targetItemName = Database.POOR_MUDANIOOKII_SHIELD;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area13) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area14) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 1))
                        {
                            switch (randomValue22)
                            {
                                case 1:
                                    targetItemName = Database.POOR_OLD_USELESS_ROD;
                                    break;
                                case 2:
                                    targetItemName = Database.POOR_KISSAKI_MARUI_TUME;
                                    break;
                                case 3:
                                    targetItemName = Database.POOR_BATTLE_HUMUKI_BUTOUGI;
                                    break;
                                case 4:
                                    targetItemName = Database.POOR_SIZE_AWANAI_ROBE;
                                    break;
                                case 5:
                                    targetItemName = Database.POOR_NO_CONCEPT_RING;
                                    break;
                                case 6:
                                    targetItemName = Database.POOR_HIGHCOLOR_MANTLE;
                                    break;
                                case 7:
                                    targetItemName = Database.POOR_EIGHT_PENDANT;
                                    break;
                                case 8:
                                    targetItemName = Database.POOR_GOJASU_BELT;
                                    break;
                                case 9:
                                    targetItemName = Database.POOR_EGARA_HUMEI_EMBLEM;
                                    break;
                                case 10:
                                    targetItemName = Database.POOR_HAYATOTIRI_ORB;
                                    break;
                            }
                        }
                        #endregion
                        #region "２階エリア１－２　３－４"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area21) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area22) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 2))
                        {
                            switch (randomValue21)
                            {
                                case 1:
                                    targetItemName = Database.POOR_HUANTEI_RING;
                                    break;
                                case 2:
                                    targetItemName = Database.POOR_DEPRESS_FEATHER;
                                    break;
                                case 3:
                                    targetItemName = Database.POOR_DAMAGED_ORB;
                                    break;
                                case 4:
                                    targetItemName = Database.POOR_SHIMETSUKE_BELT;
                                    break;
                                case 5:
                                    targetItemName = Database.POOR_NOGENKEI_EMBLEM;
                                    break;
                                case 6:
                                    targetItemName = Database.POOR_MAGICLIGHT_FIRE;
                                    break;
                                case 7:
                                    targetItemName = Database.POOR_MAGICLIGHT_ICE;
                                    break;
                                case 8:
                                    targetItemName = Database.POOR_MAGICLIGHT_SHADOW;
                                    break;
                                case 9:
                                    targetItemName = Database.POOR_MAGICLIGHT_LIGHT;
                                    break;

                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area23) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area24) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss21) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss22) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss23) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss24) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss25) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 2))
                        {
                            switch (randomValue22)
                            {
                                case 1:
                                    targetItemName = Database.POOR_CURSE_EARRING;
                                    break;
                                case 2:
                                    targetItemName = Database.POOR_CURSE_BOOTS;
                                    break;
                                case 3:
                                    targetItemName = Database.POOR_BLOODY_STATUE;
                                    break;
                                case 4:
                                    targetItemName = Database.POOR_FALLEN_MANTLE;
                                    break;
                                case 5:
                                    targetItemName = Database.POOR_SIHAIRYU_SIKOTU;
                                    break;
                                case 6:
                                    targetItemName = Database.POOR_OLD_TREE_KAREHA;
                                    break;
                                case 7:
                                    targetItemName = Database.POOR_GALEWIND_KONSEKI;
                                    break;
                                case 8:
                                    targetItemName = Database.POOR_SIN_CRYSTAL_KAKERA;
                                    break;
                                case 9:
                                    targetItemName = Database.POOR_EVERMIND_ZANSHI;
                                    break;
                            }
                        }
                        #endregion
                        #region "３階エリア１－２　３－４"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area31) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area32) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 3))
                        {
                            switch (randomValue21)
                            {
                                case 1:
                                    targetItemName = Database.POOR_DIRTY_ANGEL_CONTRACT;
                                    break;
                                case 2:
                                    targetItemName = Database.POOR_JUNK_TARISMAN_FROZEN;
                                    break;
                                case 3:
                                    targetItemName = Database.POOR_JUNK_TARISMAN_PARALYZE;
                                    break;
                                case 4:
                                    targetItemName = Database.POOR_JUNK_TARISMAN_STUN;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area33) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area34) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 3))
                        {
                            switch (randomValue22)
                            {
                                case 1:
                                    targetItemName = Database.POOR_MIGAWARI_DOOL;
                                    break;
                                case 2:
                                    targetItemName = Database.POOR_ONE_DROPLET_KESSYOU;
                                    break;
                                case 3:
                                    targetItemName = Database.POOR_MOMENTARY_FLASH_LIGHT;
                                    break;
                                case 4:
                                    targetItemName = Database.POOR_SUN_YUME_KAKERA;
                                    break;
                            }
                        }
                        #endregion
                        #region "４階エリア１－２　３－４"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area41) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area42) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 4))
                        {
                            targetItemName = String.Empty;
                        }
                        else if (ec1.Area == TruthEnemyCharacter.MonsterArea.Area43 ||
                            ec1.Area == TruthEnemyCharacter.MonsterArea.Area44)
                        {
                            targetItemName = String.Empty;
                        }
                        #endregion
                        #region "５階エリア or 現実世界ラスト４階"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area51) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area46) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 5))
                        {
                            targetItemName = String.Empty;
                        }
                        #endregion
                        debugCounter2++;
                    }
                    // ダンジョンエリア毎のコモン汎用装備品
                    else if (param2 < randomValue2 && randomValue2 <= (param2 + param3)) // Common 35.00%
                    {
                        #region "１階エリア１－２　３－４"
                        if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area11) ||
                            (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area12) ||
                            (category == NewItemCategory.Lottery && dungeonArea == 1))
                        {
                            switch (randomValue3)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_RED_PENDANT;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_BLUE_PENDANT;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_PURPLE_PENDANT;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_GREEN_PENDANT;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_YELLOW_PENDANT;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_SISSO_ARMRING;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_FINE_FEATHER;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_KIREINA_ORB;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_FIT_BANGLE;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_PRISM_EMBLEM;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_FINE_SWORD;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_TWEI_SWORD;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_FINE_ARMOR;
                                    break;
                                case 14:
                                    targetItemName = Database.COMMON_GOTHIC_PLATE;
                                    break;
                                case 15:
                                    targetItemName = Database.COMMON_FINE_SHIELD;
                                    break;
                                case 16:
                                    targetItemName = Database.COMMON_GRIPPING_SHIELD;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area13) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area14) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 1))
                        {
                            switch (randomValue32)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_COPPER_RING_TORA;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_COPPER_RING_IRUKA;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_COPPER_RING_UMA;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_COPPER_RING_KUMA;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_COPPER_RING_HAYABUSA;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_COPPER_RING_TAKO;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_COPPER_RING_USAGI;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_COPPER_RING_KUMO;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_COPPER_RING_SHIKA;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_COPPER_RING_ZOU;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_RED_AMULET;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_BLUE_AMULET;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_PURPLE_AMULET;
                                    break;
                                case 14:
                                    targetItemName = Database.COMMON_GREEN_AMULET;
                                    break;
                                case 15:
                                    targetItemName = Database.COMMON_YELLOW_AMULET;
                                    break;
                                case 16:
                                    targetItemName = Database.COMMON_SHARP_CLAW;
                                    break;
                                case 17:
                                    targetItemName = Database.COMMON_LIGHT_CLAW;
                                    break;
                                case 18:
                                    targetItemName = Database.COMMON_WOOD_ROD;
                                    break;
                                case 19:
                                    targetItemName = Database.COMMON_SHORT_SWORD;
                                    break;
                                case 20:
                                    targetItemName = Database.COMMON_BASTARD_SWORD;
                                    break;
                                case 21:
                                    targetItemName = Database.COMMON_LETHER_CLOTHING;
                                    break;
                                case 22:
                                    targetItemName = Database.COMMON_COTTON_ROBE;
                                    break;
                                case 23:
                                    targetItemName = Database.COMMON_COPPER_ARMOR;
                                    break;
                                case 24:
                                    targetItemName = Database.COMMON_HEAVY_ARMOR;
                                    break;
                                case 25:
                                    targetItemName = Database.COMMON_IRON_SHIELD;
                                    break;
                            }
                        }
                        #endregion
                        #region "２階エリア１－２　３－４"
                        if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area21) ||
                            (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area22) ||
                            (category == NewItemCategory.Lottery && dungeonArea == 2))
                        {
                            switch (randomValue3)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_RED_CHARM;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_BLUE_CHARM;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_PURPLE_CHARM;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_GREEN_CHARM;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_YELLOW_CHARM;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_THREE_COLOR_COMPASS;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_SANGO_CROWN;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_SMOOTHER_BOOTS;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_SHIOKAZE_MANTLE;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_SMART_SWORD;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_SMART_CLAW;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_SMART_ROD;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_SMART_SHIELD;
                                    break;
                                case 14:
                                    targetItemName = Database.COMMON_RAUGE_SWORD;
                                    break;
                                case 15:
                                    targetItemName = Database.COMMON_SMART_CLOTHING;
                                    break;
                                case 16:
                                    targetItemName = Database.COMMON_SMART_ROBE;
                                    break;
                                case 17:
                                    targetItemName = Database.COMMON_SMART_PLATE;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area23) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area24) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss21) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss22) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss23) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss24) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss25) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 2))
                        {
                            switch (randomValue32)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_BRONZE_RING_KIBA;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_BRONZE_RING_SASU;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_BRONZE_RING_KU;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_BRONZE_RING_NAGURI;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_BRONZE_RING_TOBI;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_BRONZE_RING_KARAMU;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_BRONZE_RING_HANERU;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_BRONZE_RING_TORU;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_BRONZE_RING_MIRU;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_BRONZE_RING_KATAI;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_RED_KOKUIN;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_BLUE_KOKUIN;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_PURPLE_KOKUIN;
                                    break;
                                case 14:
                                    targetItemName = Database.COMMON_GREEN_KOKUIN;
                                    break;
                                case 15:
                                    targetItemName = Database.COMMON_YELLOW_KOKUIN;
                                    break;
                                case 16:
                                    targetItemName = Database.COMMON_SISSEI_MANTLE;
                                    break;
                                case 17:
                                    targetItemName = Database.COMMON_KAISEI_EMBLEM;
                                    break;
                                case 18:
                                    targetItemName = Database.COMMON_SAZANAMI_EARRING;
                                    break;
                                case 19:
                                    targetItemName = Database.COMMON_AMEODORI_STATUE;
                                    break;
                                case 20:
                                    targetItemName = Database.COMMON_SMASH_BLADE;
                                    break;
                                case 21:
                                    targetItemName = Database.COMMON_POWERED_BUSTER;
                                    break;
                                case 22:
                                    targetItemName = Database.COMMON_STONE_CLAW;
                                    break;
                                case 23:
                                    targetItemName = Database.COMMON_DENDOU_ROD;
                                    break;
                                case 24:
                                    targetItemName = Database.COMMON_SERPENT_ARMOR;
                                    break;
                                case 25:
                                    targetItemName = Database.COMMON_SWIFT_CROSS;
                                    break;
                                case 26:
                                    targetItemName = Database.COMMON_CHIFFON_ROBE;
                                    break;
                                case 27:
                                    targetItemName = Database.COMMON_PURE_BRONZE_SHIELD;
                                    break;
                            }
                        }
                        #endregion
                        #region "３階エリア１－２　３－４"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area31) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area32) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 3))
                        {
                            switch (randomValue3)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_RED_STONE;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_BLUE_STONE;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_PURPLE_STONE;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_GREEN_STONE;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_YELLOW_STONE;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_EXCELLENT_SWORD;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_EXCELLENT_KNUCKLE;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_EXCELLENT_ROD;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_EXCELLENT_BUSTER;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_EXCELLENT_ARMOR;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_EXCELLENT_CROSS;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_EXCELLENT_ROBE;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_EXCELLENT_SHIELD;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area33) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area34) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 3))
                        {
                            switch (randomValue32)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_STEEL_RING_1;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_STEEL_RING_2;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_STEEL_RING_3;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_STEEL_RING_4;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_STEEL_RING_5;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_STEEL_RING_6;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_STEEL_RING_7;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_STEEL_RING_8;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_STEEL_RING_9;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_STEEL_RING_10;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_RED_MASEKI;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_BLUE_MASEKI;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_PURPLE_MASEKI;
                                    break;
                                case 14:
                                    targetItemName = Database.COMMON_GREEN_MASEKI;
                                    break;
                                case 15:
                                    targetItemName = Database.COMMON_YELLOW_MASEKI;
                                    break;
                                case 16:
                                    targetItemName = Database.COMMON_DESCENED_BLADE;
                                    break;
                                case 17:
                                    targetItemName = Database.COMMON_FALSET_CLAW;
                                    break;
                                case 18:
                                    targetItemName = Database.COMMON_SEKIGAN_ROD;
                                    break;
                                case 19:
                                    targetItemName = Database.COMMON_ROCK_BUSTER;
                                    break;
                                case 20:
                                    targetItemName = Database.COMMON_COLD_STEEL_PLATE;
                                    break;
                                case 21:
                                    targetItemName = Database.COMMON_AIR_HARE_CROSS;
                                    break;
                                case 22:
                                    targetItemName = Database.COMMON_FLOATING_ROBE;
                                    break;
                                case 23:
                                    targetItemName = Database.COMMON_SNOW_CRYSTAL_SHIELD;
                                    break;
                                case 24:
                                    targetItemName = Database.COMMON_WING_STEP_FEATHER;
                                    break;
                                case 25:
                                    targetItemName = Database.COMMON_SNOW_FAIRY_BREATH;
                                    break;
                                case 26:
                                    targetItemName = Database.COMMON_STASIS_RING;
                                    break;
                                case 27:
                                    targetItemName = Database.COMMON_SIHAIRYU_KIBA;
                                    break;
                                case 28:
                                    targetItemName = Database.COMMON_OLD_TREE_JUSHI;
                                    break;
                                case 29:
                                    targetItemName = Database.COMMON_GALEWIND_KIZUATO;
                                    break;
                                case 30:
                                    targetItemName = Database.COMMON_SIN_CRYSTAL_QUATZ;
                                    break;
                                case 31:
                                    targetItemName = Database.COMMON_EVERMIND_OMEN;
                                    break;
                            }
                        }
                        #endregion
                        #region "４階エリア１－２　３－４"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area41) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area42) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 4))
                        {
                            switch (randomValue3)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_RED_MEDALLION;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_BLUE_MEDALLION;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_PURPLE_MEDALLION;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_GREEN_MEDALLION;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_YELLOW_MEDALLION;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_SOCIETY_SYMBOL;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_SILVER_FEATHER_BRACELET;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_BIRD_SONG_LUTE;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_MAZE_CUBE;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_LIGHT_SERVANT;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_SHADOW_SERVANT;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_ROYAL_GUARD_RING;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_ELEMENTAL_GUARD_RING;
                                    break;
                                case 14:
                                    targetItemName = Database.COMMON_HAYATE_GUARD_RING;
                                    break;
                                case 15:
                                    targetItemName = Database.COMMON_MASTER_SWORD;
                                    break;
                                case 16:
                                    targetItemName = Database.COMMON_MASTER_KNUCKLE;
                                    break;
                                case 17:
                                    targetItemName = Database.COMMON_MASTER_ROD;
                                    break;
                                case 18:
                                    targetItemName = Database.COMMON_MASTER_AXE;
                                    break;
                                case 19:
                                    targetItemName = Database.COMMON_MASTER_ARMOR;
                                    break;
                                case 20:
                                    targetItemName = Database.COMMON_MASTER_CROSS;
                                    break;
                                case 21:
                                    targetItemName = Database.COMMON_MASTER_ROBE;
                                    break;
                                case 22:
                                    targetItemName = Database.COMMON_MASTER_SHIELD;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area43) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area44) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 4))
                        {
                            switch (randomValue32)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_SILVER_RING_1;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_SILVER_RING_2;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_SILVER_RING_3;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_SILVER_RING_4;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_SILVER_RING_5;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_SILVER_RING_6;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_SILVER_RING_7;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_SILVER_RING_8;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_SILVER_RING_9;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_SILVER_RING_10;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_RED_FLOAT_STONE;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_BLUE_FLOAT_STONE;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_PURPLE_FLOAT_STONE;
                                    break;
                                case 14:
                                    targetItemName = Database.COMMON_GREEN_FLOAT_STONE;
                                    break;
                                case 15:
                                    targetItemName = Database.COMMON_YELLOW_FLOAT_STONE;
                                    break;
                                case 16:
                                    targetItemName = Database.COMMON_MUKEI_SAKAZUKI;
                                    break;
                                case 17:
                                    targetItemName = Database.COMMON_RAINBOW_TUBE;
                                    break;
                                case 18:
                                    targetItemName = Database.COMMON_ELDER_PERSPECTIVE_GRASS;
                                    break;
                                case 19:
                                    targetItemName = Database.COMMON_DEVIL_SEALED_VASE;
                                    break;
                                case 20:
                                    targetItemName = Database.COMMON_FLOATING_WHITE_BALL;
                                    break;
                                case 21:
                                    targetItemName = Database.COMMON_INITIATE_SWORD;
                                    break;
                                case 22:
                                    targetItemName = Database.COMMON_BULLET_KNUCKLE;
                                    break;
                                case 23:
                                    targetItemName = Database.COMMON_KENTOUSI_SWORD;
                                    break;
                                case 24:
                                    targetItemName = Database.COMMON_ELECTRO_ROD;
                                    break;
                                case 25:
                                    targetItemName = Database.COMMON_FORTIFY_SCALE;
                                    break;
                                case 26:
                                    targetItemName = Database.COMMON_MURYOU_CROSS;
                                    break;
                                case 27:
                                    targetItemName = Database.COMMON_COLORLESS_ROBE;
                                    break;
                                case 28:
                                    targetItemName = Database.COMMON_LOGISTIC_SHIELD;
                                    break;
                            }
                        }
                        #endregion
                        #region "５階エリア or 現実世界ラスト４階"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area51) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area46) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 5))
                        {
                            switch (randomValue3)
                            {
                                case 1:
                                    targetItemName = Database.COMMON_RED_CRYSTAL;
                                    break;
                                case 2:
                                    targetItemName = Database.COMMON_BLUE_CRYSTAL;
                                    break;
                                case 3:
                                    targetItemName = Database.COMMON_PURPLE_CRYSTAL;
                                    break;
                                case 4:
                                    targetItemName = Database.COMMON_GREEN_CRYSTAL;
                                    break;
                                case 5:
                                    targetItemName = Database.COMMON_YELLOW_CRYSTAL;
                                    break;
                                case 6:
                                    targetItemName = Database.COMMON_PLATINUM_RING_1;
                                    break;
                                case 7:
                                    targetItemName = Database.COMMON_PLATINUM_RING_2;
                                    break;
                                case 8:
                                    targetItemName = Database.COMMON_PLATINUM_RING_3;
                                    break;
                                case 9:
                                    targetItemName = Database.COMMON_PLATINUM_RING_4;
                                    break;
                                case 10:
                                    targetItemName = Database.COMMON_PLATINUM_RING_5;
                                    break;
                                case 11:
                                    targetItemName = Database.COMMON_PLATINUM_RING_6;
                                    break;
                                case 12:
                                    targetItemName = Database.COMMON_PLATINUM_RING_7;
                                    break;
                                case 13:
                                    targetItemName = Database.COMMON_PLATINUM_RING_8;
                                    break;
                                case 14:
                                    targetItemName = Database.COMMON_PLATINUM_RING_9;
                                    break;
                                case 15:
                                    targetItemName = Database.COMMON_PLATINUM_RING_10;
                                    break;
                            }
                        }
                        #endregion
                        debugCounter3++;
                    }
                    // ダンジョンエリア毎のレア汎用装備品
                    else if ((param2 + param3) < randomValue2 && randomValue2 <= (param2 + param3 + param4)) // Rare 5.00%
                    {
                        #region "１階エリア１－２　３－４"
                        if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area11) ||
                            (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area12) ||
                            (category == NewItemCategory.Lottery && dungeonArea == 1))
                        {
                            switch (randomValue4)
                            {
                                case 1:
                                    targetItemName = Database.RARE_JOUSITU_BLUE_POWERRING;
                                    break;
                                case 2:
                                    targetItemName = Database.RARE_KOUJOUSINYADORU_RED_ORB;
                                    break;
                                case 3:
                                    targetItemName = Database.RARE_MAGICIANS_MANTLE;
                                    break;
                                case 4:
                                    targetItemName = Database.RARE_BEATRUSH_BANGLE;
                                    break;
                                case 5:
                                    targetItemName = Database.RARE_AERO_BLADE;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area13) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area14) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 1))
                        {
                            switch (randomValue42)
                            {
                                case 1:
                                    targetItemName = Database.RARE_SINTYUU_RING_KUROHEBI;
                                    break;
                                case 2:
                                    targetItemName = Database.RARE_SINTYUU_RING_HAKUTYOU;
                                    break;
                                case 3:
                                    targetItemName = Database.RARE_SINTYUU_RING_AKAHYOU;
                                    break;
                                case 4:
                                    targetItemName = Database.RARE_ICE_SWORD;
                                    break;
                                case 5:
                                    targetItemName = Database.RARE_RISING_KNUCKLE;
                                    break;
                                case 6:
                                    targetItemName = Database.RARE_AUTUMN_ROD;
                                    break;
                                case 7:
                                    targetItemName = Database.RARE_SUN_BRAVE_ARMOR;
                                    break;
                                case 8:
                                    targetItemName = Database.RARE_ESMERALDA_SHIELD;
                                    break;
                            }
                        }
                        #endregion
                        #region "２階エリア１－２　３－４"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area21) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area22) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 2))
                        {
                            switch (randomValue4)
                            {
                                case 1:
                                    targetItemName = Database.RARE_WRATH_SERVEL_CLAW;
                                    break;
                                case 2:
                                    targetItemName = Database.RARE_BLUE_LIGHTNING;
                                    break;
                                case 3:
                                    targetItemName = Database.RARE_DIRGE_ROBE;
                                    break;
                                case 4:
                                    targetItemName = Database.RARE_DUNSID_PLATE;
                                    break;
                                case 5:
                                    targetItemName = Database.RARE_BURNING_CLAYMORE;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area23) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area24) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss21) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss22) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss23) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss24) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss25) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 2))
                        {
                            switch (randomValue42)
                            {
                                case 1:
                                    targetItemName = Database.RARE_RING_BRONZE_RING_KONSHIN;
                                    break;
                                case 2:
                                    targetItemName = Database.RARE_RING_BRONZE_RING_SYUNSOKU;
                                    break;
                                case 3:
                                    targetItemName = Database.RARE_RING_BRONZE_RING_JUKURYO;
                                    break;
                                case 4:
                                    targetItemName = Database.RARE_RING_BRONZE_RING_SOUGEN;
                                    break;
                                case 5:
                                    targetItemName = Database.RARE_RING_BRONZE_RING_YUUDAI;
                                    break;
                                case 6:
                                    targetItemName = Database.RARE_MEIUN_BOX;
                                    break;
                                case 7:
                                    targetItemName = Database.RARE_WILL_HOLY_HAT;
                                    break;
                                case 8:
                                    targetItemName = Database.RARE_EMBLEM_BLUESTAR;
                                    break;
                                case 9:
                                    targetItemName = Database.RARE_SEAL_OF_DEATH;
                                    break;
                                case 10:
                                    targetItemName = Database.RARE_DARKNESS_SWORD;
                                    break;
                                case 11:
                                    targetItemName = Database.RARE_BLUE_RED_ROD;
                                    break;
                                case 12:
                                    targetItemName = Database.RARE_SHARKSKIN_ARMOR;
                                    break;
                                case 13:
                                    targetItemName = Database.RARE_RED_THUNDER_ROBE;
                                    break;
                                case 14:
                                    targetItemName = Database.RARE_BLACK_MAGICIAN_CROSS;
                                    break;
                                case 15:
                                    targetItemName = Database.RARE_BLUE_SKY_SHIELD;
                                    break;
                            }
                        }
                        #endregion
                        #region "３階エリア１－２　３－４"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area31) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area32) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 3))
                        {
                            switch (randomValue4)
                            {
                                case 1:
                                    targetItemName = Database.RARE_MENTALIZED_FORCE_CLAW;
                                    break;
                                case 2:
                                    targetItemName = Database.RARE_ADERKER_FALSE_ROD;
                                    break;
                                case 3:
                                    targetItemName = Database.RARE_BLACK_ICE_SWORD;
                                    break;
                                case 4:
                                    targetItemName = Database.RARE_CLAYMORE_ZUKS;
                                    break;
                                case 5:
                                    targetItemName = Database.RARE_DRAGONSCALE_ARMOR;
                                    break;
                                case 6:
                                    targetItemName = Database.RARE_LIGHT_BLIZZARD_ROBE;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area33) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area34) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 3))
                        {
                            switch (randomValue42)
                            {
                                case 1:
                                    targetItemName = Database.RARE_FROZEN_LAVA;
                                    break;
                                case 2:
                                    targetItemName = Database.RARE_WHITE_TIGER_ANGEL_GOHU;
                                    break;
                                case 3:
                                    targetItemName = Database.RARE_POWER_STEEL_RING_SOLID;
                                    break;
                                case 4:
                                    targetItemName = Database.RARE_POWER_STEEL_RING_VAPOR;
                                    break;
                                case 5:
                                    targetItemName = Database.RARE_POWER_STEEL_RING_ERASTIC;
                                    break;
                                case 6:
                                    targetItemName = Database.RARE_POWER_STEEL_RING_TORAREITION;
                                    break;
                                case 7:
                                    targetItemName = Database.RARE_SYUURENSYA_KUROOBI;
                                    break;
                                case 8:
                                    targetItemName = Database.RARE_SHIHANDAI_KUROOBI;
                                    break;
                                case 9:
                                    targetItemName = Database.RARE_SYUUDOUSOU_KUROOBI;
                                    break;
                                case 10:
                                    targetItemName = Database.RARE_KUGYOUSYA_KUROOBI;
                                    break;
                                case 11:
                                    targetItemName = Database.RARE_TEARS_END;
                                    break;
                                case 12:
                                    targetItemName = Database.RARE_SKY_COLD_BOOTS;
                                    break;
                                case 13:
                                    targetItemName = Database.RARE_EARTH_BREAKERS_SIGIL;
                                    break;
                                case 14:
                                    targetItemName = Database.RARE_AERIAL_VORTEX;
                                    break;
                                case 15:
                                    targetItemName = Database.RARE_LIVING_GROWTH_SEED;
                                    break;
                                case 16:
                                    targetItemName = Database.RARE_SHARPNEL_SPIN_BLADE;
                                    break;
                                case 17:
                                    targetItemName = Database.RARE_BLUE_LIGHT_MOON_CLAW;
                                    break;
                                case 18:
                                    targetItemName = Database.RARE_BLIZZARD_SNOW_ROD;
                                    break;
                                case 19:
                                    targetItemName = Database.RARE_SHAERING_BONE_CRUSHER;
                                    break;
                                case 20:
                                    targetItemName = Database.RARE_SCALE_BLUERAGE;
                                    break;
                                case 21:
                                    targetItemName = Database.RARE_BLUE_REFLECT_ROBE;
                                    break;
                                case 22:
                                    targetItemName = Database.RARE_SLIDE_THROUGH_SHIELD;
                                    break;
                                case 23:
                                    targetItemName = Database.RARE_ELEMENTAL_STAR_SHIELD;
                                    break;
                            }
                        }
                        #endregion
                        #region "４階エリア１－２　３－４"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area41) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area42) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 4))
                        {
                            switch (randomValue4)
                            {
                                case 1:
                                    targetItemName = Database.RARE_SPELL_COMPASS;
                                    break;
                                case 2:
                                    targetItemName = Database.RARE_SHADOW_BIBLE;
                                    break;
                                case 3:
                                    targetItemName = Database.RARE_DETACHMENT_ORB;
                                    break;
                                case 4:
                                    targetItemName = Database.RARE_BLIND_NEEDLE;
                                    break;
                                case 5:
                                    targetItemName = Database.RARE_CORE_ESSENCE_CHANNEL;
                                    break;
                                case 6:
                                    targetItemName = Database.RARE_ASTRAL_VOID_BLADE;
                                    break;
                                case 7:
                                    targetItemName = Database.RARE_VERDANT_SONIC_CLAW;
                                    break;
                                case 8:
                                    targetItemName = Database.RARE_PRISONER_BREAKING_AXE;
                                    break;
                                case 9:
                                    targetItemName = Database.RARE_INVISIBLE_STATE_ROD;
                                    break;
                                case 10:
                                    targetItemName = Database.RARE_DOMINATION_BRAVE_ARMOR;
                                    break;
                            }
                        }
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area43) ||
                                 (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area44) ||
                                 (category == NewItemCategory.Lottery && dungeonArea == 4))
                        {
                            switch (randomValue42)
                            {
                                case 1:
                                    targetItemName = Database.RARE_SEAL_OF_ASSASSINATION;
                                    break;
                                case 2:
                                    targetItemName = Database.RARE_EMBLEM_OF_VALKYRIE;
                                    break;
                                case 3:
                                    targetItemName = Database.RARE_EMBLEM_OF_HADES;
                                    break;
                                case 4:
                                    targetItemName = Database.RARE_SIHAIRYU_KATAUDE;
                                    break;
                                case 5:
                                    targetItemName = Database.RARE_OLD_TREE_SINKI;
                                    break;
                                case 6:
                                    targetItemName = Database.RARE_GALEWIND_IBUKI;
                                    break;
                                case 7:
                                    targetItemName = Database.RARE_SIN_CRYSTAL_SOLID;
                                    break;
                                case 8:
                                    targetItemName = Database.RARE_EVERMIND_SENSE;
                                    break;
                                case 9:
                                    targetItemName = Database.RARE_DEVIL_SUMMONER_TOME;
                                    break;
                                case 10:
                                    targetItemName = Database.RARE_ANGEL_CONTRACT;
                                    break;
                                case 11:
                                    targetItemName = Database.RARE_ARCHANGEL_CONTRACT;
                                    break;
                                case 12:
                                    targetItemName = Database.RARE_DARKNESS_COIN;
                                    break;
                                case 13:
                                    targetItemName = Database.RARE_SOUSUI_HIDENSYO;
                                    break;
                                case 14:
                                    targetItemName = Database.RARE_MEEK_HIDENSYO;
                                    break;
                                case 15:
                                    targetItemName = Database.RARE_JUKUTATUSYA_HIDENSYO;
                                    break;
                                case 16:
                                    targetItemName = Database.RARE_KYUUDOUSYA_HIDENSYO;
                                    break;
                                case 17:
                                    targetItemName = Database.RARE_DANZAI_ANGEL_GOHU;
                                    break;
                                case 18:
                                    targetItemName = Database.RARE_ETHREAL_EDGE_SABRE;
                                    break;
                                case 19:
                                    targetItemName = Database.RARE_SHINGETUEN_CLAW;
                                    break;
                                case 20:
                                    targetItemName = Database.RARE_BLOODY_DIRTY_SCYTHE;
                                    break;
                                case 21:
                                    targetItemName = Database.RARE_ALL_ELEMENTAL_ROD;
                                    break;
                                case 22:
                                    targetItemName = Database.RARE_BLOOD_BLAZER_CROSS;
                                    break;
                                case 23:
                                    targetItemName = Database.RARE_DARK_ANGEL_ROBE;
                                    break;
                                case 24:
                                    targetItemName = Database.RARE_MAJEST_HAZZARD_SHIELD;
                                    break;
                                case 25:
                                    targetItemName = Database.RARE_WHITE_DIAMOND_SHIELD;
                                    break;
                                case 26:
                                    targetItemName = Database.RARE_VAPOR_SOLID_SHIELD;
                                    break;
                            }
                        }
                        #endregion
                        #region "５階エリア or 現実世界ラスト４階"
                        else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area51) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area46) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 5))
                        {
                            switch (randomValue4)
                            {
                                case 1:
                                    targetItemName = Database.GROWTH_LIQUID5_STRENGTH;
                                    break;
                                case 2:
                                    targetItemName = Database.GROWTH_LIQUID5_AGILITY;
                                    break;
                                case 3:
                                    targetItemName = Database.GROWTH_LIQUID5_INTELLIGENCE;
                                    break;
                                case 4:
                                    targetItemName = Database.GROWTH_LIQUID5_STAMINA;
                                    break;
                                case 5:
                                    targetItemName = Database.GROWTH_LIQUID5_MIND;
                                    break;
                            }
                        }
                        #endregion
                        debugCounter4++;
                    }
                }
                #endregion
                #region "ダンジョン階層依存のパワーアップアイテム"
                else if ((param1 + param2 + param3 + param4) < randomValue && randomValue <= (param1 + param2 + param3 + param4 + param5)) // Rare Use Item 0.90%
                {
                    #region "１階全エリア"
                    if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area11) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area12) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area13) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area14) ||
                        (category == NewItemCategory.Lottery && dungeonArea == 1))
                    {
                        switch (randomValue5)
                        {
                            case 1:
                                targetItemName = Database.GROWTH_LIQUID_STRENGTH;
                                break;
                            case 2:
                                targetItemName = Database.GROWTH_LIQUID_AGILITY;
                                break;
                            case 3:
                                targetItemName = Database.GROWTH_LIQUID_INTELLIGENCE;
                                break;
                            case 4:
                                targetItemName = Database.GROWTH_LIQUID_STAMINA;
                                break;
                            case 5:
                                targetItemName = Database.GROWTH_LIQUID_MIND;
                                break;
                        }
                    }
                    #endregion
                    #region "２階全エリア"
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area21) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area22) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area23) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area24) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss21) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss22) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss23) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss24) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss25) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 2))
                    {
                        switch (randomValue5)
                        {
                            case 1:
                                targetItemName = Database.GROWTH_LIQUID2_STRENGTH;
                                break;
                            case 2:
                                targetItemName = Database.GROWTH_LIQUID2_AGILITY;
                                break;
                            case 3:
                                targetItemName = Database.GROWTH_LIQUID2_INTELLIGENCE;
                                break;
                            case 4:
                                targetItemName = Database.GROWTH_LIQUID2_STAMINA;
                                break;
                            case 5:
                                targetItemName = Database.GROWTH_LIQUID2_MIND;
                                break;
                        }
                    }
                    #endregion
                    #region "３階全エリア"
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area31) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area32) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area33) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area34) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 3))
                    {
                        switch (randomValue5)
                        {
                            case 1:
                                targetItemName = Database.GROWTH_LIQUID3_STRENGTH;
                                break;
                            case 2:
                                targetItemName = Database.GROWTH_LIQUID3_AGILITY;
                                break;
                            case 3:
                                targetItemName = Database.GROWTH_LIQUID3_INTELLIGENCE;
                                break;
                            case 4:
                                targetItemName = Database.GROWTH_LIQUID3_STAMINA;
                                break;
                            case 5:
                                targetItemName = Database.GROWTH_LIQUID3_MIND;
                                break;
                        }
                    }
                    #endregion
                    #region "４階全エリア"
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area41) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area42) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area43) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area44) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 4))
                    {
                        switch (randomValue5)
                        {
                            case 1:
                                targetItemName = Database.GROWTH_LIQUID4_STRENGTH;
                                break;
                            case 2:
                                targetItemName = Database.GROWTH_LIQUID4_AGILITY;
                                break;
                            case 3:
                                targetItemName = Database.GROWTH_LIQUID4_INTELLIGENCE;
                                break;
                            case 4:
                                targetItemName = Database.GROWTH_LIQUID4_STAMINA;
                                break;
                            case 5:
                                targetItemName = Database.GROWTH_LIQUID4_MIND;
                                break;
                        }
                    }
                    #endregion
                    #region "５階エリア or 現実世界ラスト４階"
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area51) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area46) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 5))
                    {
                        switch (randomValue5)
                        {
                            case 1:
                                targetItemName = Database.GROWTH_LIQUID5_STRENGTH;
                                break;
                            case 2:
                                targetItemName = Database.GROWTH_LIQUID5_AGILITY;
                                break;
                            case 3:
                                targetItemName = Database.GROWTH_LIQUID5_INTELLIGENCE;
                                break;
                            case 4:
                                targetItemName = Database.GROWTH_LIQUID5_STAMINA;
                                break;
                            case 5:
                                targetItemName = Database.GROWTH_LIQUID5_MIND;
                                break;
                        }
                    }
                    #endregion
                    debugCounter5++;
                }
                #endregion
                #region "ダンジョン階層依存の高級装備品"
                else if ((param1 + param2 + param3 + param4 + param5) < randomValue && randomValue <= (param1 + param2 + param3 + param4 + param5 + param6)) // EPIC 0.45%
                {
                    #region "１階全エリア"
                    if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area11) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area12) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area13) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area14) ||
                        (category == NewItemCategory.Lottery && dungeonArea == 1))
                    {
                        // 低レベルの間に取得できてしまうのは、逆に拍子抜けしてしまうため、ブロックする。
                        if (mc.Level <= 10)
                        {
                            switch (randomValue5)
                            {
                                case 1:
                                    targetItemName = Database.GROWTH_LIQUID_STRENGTH;
                                    break;
                                case 2:
                                    targetItemName = Database.GROWTH_LIQUID_AGILITY;
                                    break;
                                case 3:
                                    targetItemName = Database.GROWTH_LIQUID_INTELLIGENCE;
                                    break;
                                case 4:
                                    targetItemName = Database.GROWTH_LIQUID_STAMINA;
                                    break;
                                case 5:
                                    targetItemName = Database.GROWTH_LIQUID_MIND;
                                    break;
                            }
                        }
                        else
                        {
                            switch (randomValue6)
                            {
                                case 1:
                                    targetItemName = Database.EPIC_RING_OF_OSCURETE;
                                    break;
                                case 2:
                                    targetItemName = Database.EPIC_MERGIZD_SOL_BLADE;
                                    break;
                            }
                            GroundOne.WE2.KillingEnemy = 0; // EPIC出現後、ボーナス値をリセットしておく。
                        }
                    }
                    #endregion
                    #region "２階全エリア"
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area21) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area22) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area23) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area24) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss21) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss22) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss23) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss24) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss25) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 2))
                    {
                        // 低レベルの間に取得できてしまうのは、逆に拍子抜けしてしまうため、ブロックする。
                        if (mc.Level <= 27)
                        {
                            switch (randomValue5)
                            {
                                case 1:
                                    targetItemName = Database.GROWTH_LIQUID2_STRENGTH;
                                    break;
                                case 2:
                                    targetItemName = Database.GROWTH_LIQUID2_AGILITY;
                                    break;
                                case 3:
                                    targetItemName = Database.GROWTH_LIQUID2_INTELLIGENCE;
                                    break;
                                case 4:
                                    targetItemName = Database.GROWTH_LIQUID2_STAMINA;
                                    break;
                                case 5:
                                    targetItemName = Database.GROWTH_LIQUID2_MIND;
                                    break;
                            }
                        }
                        else
                        {
                            switch (randomValue6)
                            {
                                case 1:
                                    targetItemName = Database.EPIC_GARVANDI_ADILORB;
                                    break;
                                case 2:
                                    targetItemName = Database.EPIC_MAXCARN_X_BUSTER;
                                    break;
                                case 3:
                                    targetItemName = Database.EPIC_JUZA_ARESTINE_SLICER;
                                    break;
                            }
                            GroundOne.WE2.KillingEnemy = 0; // EPIC出現後、ボーナス値をリセットしておく。
                        }
                    }
                    #endregion
                    #region "３階全エリア"
                    if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area31) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area32) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area33) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area34) ||
                        (category == NewItemCategory.Lottery && dungeonArea == 3))
                    {
                        // 低レベルの間に取得できてしまうのは、逆に拍子抜けしてしまうため、ブロックする。
                        if (mc.Level <= 45)
                        {
                            switch (randomValue5)
                            {
                                case 1:
                                    targetItemName = Database.GROWTH_LIQUID3_STRENGTH;
                                    break;
                                case 2:
                                    targetItemName = Database.GROWTH_LIQUID3_AGILITY;
                                    break;
                                case 3:
                                    targetItemName = Database.GROWTH_LIQUID3_INTELLIGENCE;
                                    break;
                                case 4:
                                    targetItemName = Database.GROWTH_LIQUID3_STAMINA;
                                    break;
                                case 5:
                                    targetItemName = Database.GROWTH_LIQUID3_MIND;
                                    break;
                            }
                        }
                        else
                        {
                            switch (randomValue6)
                            {
                                case 1:
                                    targetItemName = Database.EPIC_SHEZL_MYSTIC_FORTUNE;
                                    break;
                                case 2:
                                    targetItemName = Database.EPIC_FLOW_FUNNEL_OF_THE_ZVELDOZE;
                                    break;
                                case 3:
                                    targetItemName = Database.EPIC_MERGIZD_DAV_AGITATED_BLADE;
                                    break;
                            }
                            GroundOne.WE2.KillingEnemy = 0; // EPIC出現後、ボーナス値をリセットしておく。
                        }
                    }
                    #endregion
                    #region "４階全エリア"
                    if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area41) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area42) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area43) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area44) ||
                        (category == NewItemCategory.Lottery && dungeonArea == 4))
                    {
                        // 低レベルの間に取得できてしまうのは、逆に拍子抜けしてしまうため、ブロックする。
                        if (mc.Level <= 55)
                        {
                            switch (randomValue5)
                            {
                                case 1:
                                    targetItemName = Database.GROWTH_LIQUID4_STRENGTH;
                                    break;
                                case 2:
                                    targetItemName = Database.GROWTH_LIQUID4_AGILITY;
                                    break;
                                case 3:
                                    targetItemName = Database.GROWTH_LIQUID4_INTELLIGENCE;
                                    break;
                                case 4:
                                    targetItemName = Database.GROWTH_LIQUID4_STAMINA;
                                    break;
                                case 5:
                                    targetItemName = Database.GROWTH_LIQUID4_MIND;
                                    break;
                            }
                        }
                        else
                        {
                            switch (randomValue6)
                            {
                                case 1:
                                    targetItemName = Database.EPIC_ETERNAL_HOMURA_RING;
                                    break;
                                case 2:
                                    targetItemName = Database.EPIC_EZEKRIEL_ARMOR_SIGIL;
                                    break;
                                case 3:
                                    targetItemName = Database.EPIC_SHEZL_THE_MIRAGE_LANCER;
                                    break;
                                case 4:
                                    targetItemName = Database.EPIC_JUZA_THE_PHANTASMAL_CLAW;
                                    break;
                                case 5:
                                    targetItemName = Database.EPIC_ADILRING_OF_BLUE_BURN;
                                    break;
                            }
                            GroundOne.WE2.KillingEnemy = 0; // EPIC出現後、ボーナス値をリセットしておく。
                        }
                    }
                    #endregion
                    #region "５階エリア or 現実世界ラスト４階"
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area51) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area46) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 5))
                    {
                        // 低レベル制限はかけない。
                        switch (randomValue6)
                        {
                            case 1:
                                targetItemName = Database.EPIC_ETERNAL_HOMURA_RING;
                                break;
                            case 2:
                                targetItemName = Database.EPIC_EZEKRIEL_ARMOR_SIGIL;
                                break;
                            case 3:
                                targetItemName = Database.EPIC_SHEZL_THE_MIRAGE_LANCER;
                                break;
                            case 4:
                                targetItemName = Database.EPIC_JUZA_THE_PHANTASMAL_CLAW;
                                break;
                            case 5:
                                targetItemName = Database.EPIC_ADILRING_OF_BLUE_BURN;
                                break;
                        }
                    }
                    #endregion
                    debugCounter6++;
                }
                #endregion
                #region "ハズレ"
                else if ((param1 + param2 + param3 + param4 + param5 + param6) < randomValue && randomValue <= (param1 + param2 + param3 + param4 + param5 + param6 + param7)) // ハズレ 8.97 %
                {
                    targetItemName = String.Empty;
                    debugCounter7++;
                }
                else // 万が一規定外の値はハズレ
                {
                    targetItemName = String.Empty;
                }
                #endregion

                #region "ハズレは、不用品をランダムドロップ"
                if (targetItemName == string.Empty)
                {
                    if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area11) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area12) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area13) ||
                        (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area14) ||
                        (category == NewItemCategory.Lottery && dungeonArea == 1))
                    {
                        if (1 <= randomValue7 && randomValue7 <= 50)
                        {
                            targetItemName = Database.POOR_BLACK_MATERIAL;
                        }
                    }
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area21) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area22) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area23) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area24) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss21) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss22) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss23) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss24) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Boss25) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 2))
                    {
                        if (1 <= randomValue7 && randomValue7 <= 50)
                        {
                            targetItemName = Database.POOR_BLACK_MATERIAL2;
                        }
                    }
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area31) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area32) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area33) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area34) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 3))
                    {
                        if (1 <= randomValue7 && randomValue7 <= 50)
                        {
                            targetItemName = Database.POOR_BLACK_MATERIAL3;
                        }
                    }
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area41) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area42) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area43) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area44) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 4))
                    {
                        if (1 <= randomValue7 && randomValue7 <= 50)
                        {
                            targetItemName = Database.POOR_BLACK_MATERIAL4;
                        }
                    }
                    else if ((category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area51) ||
                             (category == NewItemCategory.Battle && ec1 != null && ec1.Area == TruthEnemyCharacter.MonsterArea.Area46) ||
                             (category == NewItemCategory.Lottery && dungeonArea == 5))
                    {
                        if (1 <= randomValue7 && randomValue7 <= 50)
                        {
                            targetItemName = Database.POOR_BLACK_MATERIAL5;
                        }
                    }
                }
                #endregion
            }

            //MessageBox.Show(debugCounter1.ToString() + "(" + Convert.ToString((double)(((double)debugCounter1 / 10000.0f) * 100.0f)) + "\r\n" +
            //                debugCounter2.ToString() + "(" + Convert.ToString((double)(((double)debugCounter2 / 10000.0f) * 100.0f)) + "\r\n" +
            //                debugCounter3.ToString() + "(" + Convert.ToString((double)(((double)debugCounter3 / 10000.0f) * 100.0f)) + "\r\n" +
            //                debugCounter4.ToString() + "(" + Convert.ToString((double)(((double)debugCounter4 / 10000.0f) * 100.0f)) + "\r\n" +
            //                debugCounter5.ToString() + "(" + Convert.ToString((double)(((double)debugCounter5 / 10000.0f) * 100.0f)) + "\r\n" +
            //                debugCounter6.ToString() + "(" + Convert.ToString((double)(((double)debugCounter6 / 10000.0f) * 100.0f)) + "\r\n" +
            //                debugCounter7.ToString() + "(" + Convert.ToString((double)(((double)debugCounter7 / 10000.0f) * 100.0f)) + "\r\n" +
            //                debugCounter8.ToString() + "\r\n");

            #region "ボス撃破、固定ドロップアイテム"
            if ((category == NewItemCategory.Battle && ec1 != null && ec1.Name == Database.ENEMY_BOSS_KARAMITUKU_FLANSIS) ||
                (category == NewItemCategory.Battle && ec1 != null && ec1.Name == Database.ENEMY_BOSS_LEVIATHAN) ||
                (category == NewItemCategory.Battle && ec1 != null && ec1.Name == Database.ENEMY_BOSS_HOWLING_SEIZER) ||
                (category == NewItemCategory.Battle && ec1 != null && ec1.Name == Database.ENEMY_BOSS_LEGIN_ARZE_1))
            {
                targetItemName = ec1.DropItem[0];
            }
            #endregion

            return targetItemName;
        }

        public static void AddItemBank(WorldEnvironment we, string itemName)
        {
            string[] itemBank = new string[Database.MAX_ITEM_BANK];
            int[] itemBankStack = new int[Database.MAX_ITEM_BANK];
            int current = 0;

            string[] beforeItem = new string[Database.MAX_ITEM_BANK];
            int[] beforeStack = new int[Database.MAX_ITEM_BANK];
            we.LoadItemBankData(ref beforeItem, ref beforeStack);
            for (int ii = 0; ii < beforeItem.Length; ii++)
            {
                if (beforeItem[ii] == String.Empty || beforeItem[ii] == "" || beforeItem[ii] == null)
                {
                    // 空っぽの場合、何も追加しない。
                }
                else
                {
                    itemBank[current] = beforeItem[ii];
                    itemBankStack[current] = beforeStack[ii];
                    current++;
                }
            }

            itemBank[current] = itemName;
            itemBankStack[current] = 1;
            current++;

            we.UpdateItemBankData(itemBank, itemBankStack);
        }

        // 街でオル・ランディスが外れる、４階最初でヴェルゼが外れる、４階エリア３でラナが外れるのを統合
        public static void RemoveParty(WorldEnvironment we, MainCharacter player)
        {
            if (we.AvailableThirdCharacter)
            {
                we.AvailableThirdCharacter = false;
            }
            else if (we.AvailableSecondCharacter)
            {
                we.AvailableSecondCharacter = false;
            }

            string[] itemBank = new string[Database.MAX_ITEM_BANK];
            int[] itemBankStack = new int[Database.MAX_ITEM_BANK];
            int current = 0;

            string[] beforeItem = new string[Database.MAX_ITEM_BANK];
            int[] beforeStack = new int[Database.MAX_ITEM_BANK];
            we.LoadItemBankData(ref beforeItem, ref beforeStack);
            for (int ii = 0; ii < beforeItem.Length; ii++)
            {
                if (beforeItem[ii] == String.Empty || beforeItem[ii] == "" || beforeItem[ii] == null)
                {
                    // 空っぽの場合、何も追加しない。
                }
                else
                {
                    itemBank[current] = beforeItem[ii];
                    itemBankStack[current] = beforeStack[ii];
                    current++;
                }
            }

            if (player.MainWeapon != null)
            {
                if ((player.MainWeapon.Name != Database.POOR_GOD_FIRE_GLOVE_REPLICA) &&
                    (player.MainWeapon.Name != Database.RARE_WHITE_SILVER_SWORD_REPLICA) &&
                    (player.MainWeapon.Name != String.Empty))
                {
                    itemBank[current] = player.MainWeapon.Name;
                    itemBankStack[current] = 1;
                    current++;
                }
            }
            if (player.SubWeapon != null)
            {
                if ((player.SubWeapon.Name != Database.POOR_GOD_FIRE_GLOVE_REPLICA) && 
                    (player.SubWeapon.Name != Database.RARE_WHITE_SILVER_SWORD_REPLICA) &&
                    (player.SubWeapon.Name != String.Empty))
                {
                    itemBank[current] = player.SubWeapon.Name;
                    itemBankStack[current] = 1;
                    current++;
                }
            }
            if (player.MainArmor != null)
            {
                if ((player.MainArmor.Name != Database.COMMON_AURA_ARMOR) &&
                    (player.MainArmor.Name != Database.RARE_BLACK_AERIAL_ARMOR_REPLICA) &&
                    (player.MainWeapon.Name != String.Empty))
                {
                    itemBank[current] = player.MainArmor.Name;
                    itemBankStack[current] = 1;
                    current++;
                }
            }
            if (player.Accessory != null)
            {
                if ((player.Accessory.Name != Database.COMMON_FATE_RING) && 
                    (player.Accessory.Name != Database.COMMON_LOYAL_RING) &&
                    (player.Accessory.Name != Database.RARE_HEAVENLY_SKY_WING_REPLICA) &&
                    (player.MainWeapon.Name != String.Empty))
                {
                    itemBank[current] = player.Accessory.Name;
                    itemBankStack[current] = 1;
                    current++;
                }
            }
            if (player.Accessory2 != null)
            {
                if ((player.Accessory2.Name != Database.COMMON_FATE_RING) &&
                    (player.Accessory2.Name != Database.COMMON_LOYAL_RING) &&
                    (player.Accessory2.Name != Database.RARE_HEAVENLY_SKY_WING_REPLICA) &&
                    (player.MainWeapon.Name != String.Empty))
                {
                    itemBank[current] = player.Accessory2.Name;
                    itemBankStack[current] = 1;
                    current++;
                }
            }
            ItemBackPack[] backpackInfo = player.GetBackPackInfo();
            for (int ii = 0; ii < backpackInfo.Length; ii++)
            {
                if (backpackInfo[ii] != null)
                {
                    if ((backpackInfo[ii].Name != Database.POOR_GOD_FIRE_GLOVE_REPLICA) &&
                        (backpackInfo[ii].Name != Database.COMMON_AURA_ARMOR) &&
                        (backpackInfo[ii].Name != Database.COMMON_FATE_RING) &&
                        (backpackInfo[ii].Name != Database.COMMON_LOYAL_RING) &&
                        (backpackInfo[ii].Name != Database.RARE_WHITE_SILVER_SWORD_REPLICA) &&
                        (backpackInfo[ii].Name != Database.RARE_BLACK_AERIAL_ARMOR_REPLICA) &&
                        (backpackInfo[ii].Name != Database.RARE_HEAVENLY_SKY_WING_REPLICA))
                    {
                        itemBank[current] = backpackInfo[ii].Name;
                        itemBankStack[current] = backpackInfo[ii].StackValue;
                        current++;
                    }
                }
            }
            we.UpdateItemBankData(itemBank, itemBankStack);
        }
    }
}
