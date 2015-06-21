using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Reflection;
using DungeonPlayer;
using System.Windows.Forms;

namespace WpfApplication3
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private int BattleSpeed = 3; // デフォルトは３
        private int Difficulty = 1; // デフォルトは１
        private int StoryMode = 0; // デフォルトは０（前編）　１（後編） // 後編追加

        public MainWindow()
        {
            InitializeComponent();

            GroundOne.WE2 = new TruthWorldEnvironment();

            // サウンドデータを初期化（バッファに事前に配置する）
            GroundOne.InitializeSoundData();
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                XmlDocument xml = new XmlDocument();
                xml.Load(Database.GameSettingFileName);
                XmlNodeList node = xml.GetElementsByTagName("EnableBGM");
                GroundOne.EnableBGM = Convert.ToBoolean(node[0].InnerText);
                XmlNodeList node2 = xml.GetElementsByTagName("EnableSoundEffect");
                GroundOne.EnableSoundEffect = Convert.ToBoolean(node2[0].InnerText);
                XmlNodeList node3 = xml.GetElementsByTagName("BattleSpeed");
                this.BattleSpeed = Convert.ToInt32(node3[0].InnerText);
                XmlNodeList node4 = xml.GetElementsByTagName("Difficulty");
                this.Difficulty = Convert.ToInt32(node4[0].InnerText);
                GroundOne.Difficulty = this.Difficulty;
                XmlNodeList node5 = xml.GetElementsByTagName("StoryMode"); // 後編追加
                this.StoryMode = Convert.ToInt32(node5[0].InnerText); // 後編追加
            }
            catch { }

            // WE2はゲーム全体のセーブデータであり、ここで読み込んでおく。
            XmlDocument xml2 = new XmlDocument();
            if (System.IO.File.Exists(Database.WE2_FILE))
            {
                xml2.Load(Database.WE2_FILE);
                Type typeWE2 = GroundOne.WE2.GetType();
                foreach (PropertyInfo pi in typeWE2.GetProperties())
                {
                    // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                    if (pi.PropertyType == typeof(System.Int32))
                    {
                        try
                        {
                            pi.SetValue(GroundOne.WE2, Convert.ToInt32(xml2.GetElementsByTagName(pi.Name)[0].InnerText), null);
                        }
                        catch { }
                    }
                    else if (pi.PropertyType == typeof(System.String))
                    {
                        try
                        {
                            pi.SetValue(GroundOne.WE2, (xml2.GetElementsByTagName(pi.Name)[0].InnerText), null);
                        }
                        catch { }
                    }
                    else if (pi.PropertyType == typeof(System.Boolean))
                    {
                        try
                        {
                            pi.SetValue(GroundOne.WE2, Convert.ToBoolean(xml2.GetElementsByTagName(pi.Name)[0].InnerText), null);
                        }
                        catch { }
                    }
                }
            }
            else
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
                    TruthWorldEnvironment we2 = new TruthWorldEnvironment();
                    if (we2 != null)
                    {
                        Type typeWE2 = we2.GetType();
                        foreach (PropertyInfo pi in typeWE2.GetProperties())
                        {
                            if (pi.PropertyType == typeof(System.Int32))
                            {
                                xmlWriter2.WriteElementString(pi.Name, ((System.Int32)(pi.GetValue(we2, null))).ToString());
                                xmlWriter2.WriteWhitespace("\r\n");
                            }
                            else if (pi.PropertyType == typeof(System.String))
                            {
                                xmlWriter2.WriteElementString(pi.Name, (string)(pi.GetValue(we2, null)));
                                xmlWriter2.WriteWhitespace("\r\n");
                            }
                            else if (pi.PropertyType == typeof(System.Boolean))
                            {
                                xmlWriter2.WriteElementString(pi.Name, ((System.Boolean)pi.GetValue(we2, null)).ToString());
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

            // 「警告」前編・後編の仕掛けは最終製品版で。現時点では後編固定とする。
            // s 後編追加
            //try
            //{
            //    using (SaveLoad sl = new SaveLoad())
            //    {
            //        if (sl.CheckCompleteData())
            //        {
            //            pictureBox1.Image = Image.FromFile(Database.BaseResourceFolder + "Title2.bmp");
            //            button1.Text = "True Story";
            //            this.StoryMode = 1;
            //        }
            //    }
            //}
            //catch { }
            button1.Content = "True Story";
            this.StoryMode = 1;
            // e 後編追加 

            if (GroundOne.EnableBGM)
            {
                GroundOne.PlayDungeonMusic(Database.BGM12, Database.BGM12LoopBegin); // 後編追加    
            }
            if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd && GroundOne.WE2.SelectFalseStatue)
            {
                SettingRealWorldButtonLayout();
            }
        }
        private void SettingRealWorldButtonLayout()
        {
            //int Y = 708;
            //int WIDTH = 180;
            //int HEIGHT = 50;
            //int LEFT = 10;
            //int ADJUST = 10;
            button5.Visibility = System.Windows.Visibility.Visible;
            button2.Visibility = System.Windows.Visibility.Hidden;
            //button1.Size = new Size(WIDTH, HEIGHT);
            //button2.Size = new Size(WIDTH, HEIGHT);
            //button3.Size = new Size(WIDTH, HEIGHT);
            //button4.Size = new Size(WIDTH, HEIGHT);
            //button5.Size = new Size(WIDTH, HEIGHT);
            //button1.Location = new Point(LEFT + WIDTH * 0 + ADJUST * 0, Y);
            //button5.Location = new Point(LEFT + WIDTH * 1 + ADJUST * 1, Y);
            //button2.Location = new Point(LEFT + WIDTH * 2 + ADJUST * 2, Y);
            //button4.Location = new Point(LEFT + WIDTH * 3 + ADJUST * 3, Y);
            //button3.Location = new Point(LEFT + WIDTH * 4 + ADJUST * 4, Y);

        }

        private void GameStart_Click(object sender, RoutedEventArgs e)
        {
            if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd)
            {
                System.Windows.MessageBox.Show("アイン・ウォーレンスが並行世界へ突入している事により、新しく始める事はできません。");
                return;
            }

            this.Hide();

            GroundOne.StopDungeonMusic();

            GroundOne.WE2.StartSeeker = false;
            Method.AutoSaveTruthWorldEnvironment();

            // 前編の場合
            if (this.StoryMode == 0) // 後編追加
            {
                using (Form1 ds = new Form1())
                {
                    ds.StartPosition = FormStartPosition.CenterParent;
                    ds.MC = new MainCharacter();
                    ds.SC = new MainCharacter();
                    ds.TC = new MainCharacter();
                    ds.WE = new WorldEnvironment();
                    ds.KnownTileInfo = new bool[Database.DUNGEON_ROW * Database.DUNGEON_COLUMN];
                    ds.KnownTileInfo2 = new bool[Database.DUNGEON_ROW * Database.DUNGEON_COLUMN];
                    ds.KnownTileInfo3 = new bool[Database.DUNGEON_ROW * Database.DUNGEON_COLUMN];
                    ds.KnownTileInfo4 = new bool[Database.DUNGEON_ROW * Database.DUNGEON_COLUMN];
                    ds.KnownTileInfo5 = new bool[Database.DUNGEON_ROW * Database.DUNGEON_COLUMN];

                    // 初めてやる人のステータスはここで書く。
                    ds.MC.FullName = "アイン・ウォーレンス";
                    ds.MC.Name = "アイン";
                    ds.MC.Strength = Database.MAINPLAYER_FIRST_STRENGTH;
                    ds.MC.Agility = Database.MAINPLAYER_FIRST_AGILITY;
                    ds.MC.Intelligence = Database.MAINPLAYER_FIRST_INTELLIGENCE;
                    ds.MC.Stamina = Database.MAINPLAYER_FIRST_STAMINA;
                    ds.MC.Mind = Database.MAINPLAYER_FIRST_MIND;
                    ds.MC.Level = 1;
                    ds.MC.Exp = 0;
                    ds.MC.BaseLife = 50;
                    ds.MC.CurrentLife = 90;
                    ds.MC.BaseSkillPoint = 100;
                    ds.MC.CurrentSkillPoint = 100;
                    ds.MC.Gold = 10;
                    ds.MC.BaseMana = 30;
                    ds.MC.CurrentMana = 100;

                    ds.MC.MainWeapon = new ItemBackPack("練習用の剣");
                    ds.MC.MainArmor = new ItemBackPack("コート・オブ・プレート");
                    //ds.MC.Accessory;//アクセサリは無し

                    ds.SC.FullName = "ラナ・アミリア";
                    ds.SC.Name = "ラナ";
                    ds.SC.Strength = Database.SECONDPLAYER_FIRST_STRENGTH;
                    ds.SC.Agility = Database.SECONDPLAYER_FIRST_AGILITY;
                    ds.SC.Intelligence = Database.SECONDPLAYER_FIRST_INTELLIGENCE;
                    ds.SC.Stamina = Database.SECONDPLAYER_FIRST_STAMINA;
                    ds.SC.Mind = Database.SECONDPLAYER_FIRST_MIND;
                    ds.SC.Level = 1;
                    ds.SC.Exp = 0;
                    ds.SC.BaseLife = 50;
                    ds.SC.CurrentLife = 80;
                    ds.SC.BaseSkillPoint = 100;
                    ds.SC.CurrentSkillPoint = 100;
                    //ds.SC.Gold = 10; // [警告]：ゴールドの所持は別クラスにするべきです。
                    ds.SC.BaseMana = 30;
                    ds.SC.CurrentMana = 130;
                    ds.SC.MainWeapon = new ItemBackPack("ナックル");
                    ds.SC.MainArmor = new ItemBackPack("ライト・クロス");
                    ds.SC.Accessory = new ItemBackPack("珊瑚のブレスレット");

                    ds.TC.FullName = "ヴェルゼ・アーティ";
                    ds.TC.Name = "ヴェルゼ";
                    ds.TC.Strength = Database.THIRDPLAYER_FIRST_STRENGTH;
                    ds.TC.Agility = Database.THIRDPLAYER_FIRST_AGILITY;
                    ds.TC.Intelligence = Database.THIRDPLAYER_FIRST_INTELLIGENCE;
                    ds.TC.Stamina = Database.THIRDPLAYER_FIRST_STAMINA;
                    ds.TC.Mind = Database.THIRDPLAYER_FIRST_MIND;
                    ds.TC.Level = 1;
                    ds.TC.Exp = 0;
                    ds.TC.BaseLife = 50;
                    ds.TC.CurrentLife = 80;
                    ds.TC.BaseSkillPoint = 100;
                    ds.TC.CurrentSkillPoint = 100;
                    //ds.TC.Gold = 10; // [警告]：ゴールドの所持は別クラスにするべきです。
                    ds.TC.BaseMana = 30;
                    ds.TC.CurrentMana = 130;
                    ds.TC.MainWeapon = new ItemBackPack("白銀の剣（レプリカ）");
                    ds.TC.MainArmor = new ItemBackPack("黒真空の鎧（レプリカ）");
                    ds.TC.Accessory = new ItemBackPack("天空の翼（レプリカ）");

                    ds.WE.GameDay = 1; // 初めての場合１日目とする。
                    ds.WE.SaveByDungeon = true; // 初めての場合ダンジョンからスタートする。
                    ds.WE.DungeonPosX = 1 + (Database.FIRST_POS % Database.DUNGEON_COLUMN) * 16;
                    ds.WE.DungeonPosY = 1 + (Database.FIRST_POS / Database.DUNGEON_COLUMN) * 16;
                    ds.WE.DungeonArea = 1; // 初めての場合ダンジョンからスタートしているため、１とする。
                    ds.WE.AvailableFirstCharacter = true; // 初めての場合、主人公アインを登録しておく。
                    ds.NewGameFlag = true;

                    ds.BattleSpeed = this.BattleSpeed;
                    ds.Difficulty = this.Difficulty;
                    GroundOne.Difficulty = this.Difficulty;
                    ds.ShowDialog();
                }
            }
            else
            {
                // s 後編追加
                using (TruthDungeon td = new TruthDungeon())
                {
                    td.StartPosition = FormStartPosition.CenterParent;
                    td.MC = new MainCharacter();
                    td.SC = new MainCharacter();
                    td.TC = new MainCharacter();
                    td.WE = new WorldEnvironment();
                    td.Truth_KnownTileInfo = new bool[Database.TRUTH_DUNGEON_ROW * Database.TRUTH_DUNGEON_COLUMN];
                    td.Truth_KnownTileInfo2 = new bool[Database.TRUTH_DUNGEON_ROW * Database.TRUTH_DUNGEON_COLUMN];
                    td.Truth_KnownTileInfo3 = new bool[Database.TRUTH_DUNGEON_ROW * Database.TRUTH_DUNGEON_COLUMN];
                    td.Truth_KnownTileInfo4 = new bool[Database.TRUTH_DUNGEON_ROW * Database.TRUTH_DUNGEON_COLUMN];
                    td.Truth_KnownTileInfo5 = new bool[Database.TRUTH_DUNGEON_ROW * Database.TRUTH_DUNGEON_COLUMN];

                    // 初めてやる人のステータスはここで書く。
                    td.MC.FullName = Database.EIN_WOLENCE_FULL;
                    td.MC.Name = Database.EIN_WOLENCE;
                    td.MC.Strength = Database.MAINPLAYER_FIRST_STRENGTH;
                    td.MC.Agility = Database.MAINPLAYER_FIRST_AGILITY;
                    td.MC.Intelligence = Database.MAINPLAYER_FIRST_INTELLIGENCE;
                    td.MC.Stamina = Database.MAINPLAYER_FIRST_STAMINA;
                    td.MC.Mind = Database.MAINPLAYER_FIRST_MIND;
                    td.MC.Level = 1;
                    td.MC.Exp = 0;
                    td.MC.BaseLife = 50;
                    td.MC.CurrentLife = 90;
                    td.MC.BaseSkillPoint = 100;
                    td.MC.CurrentSkillPoint = 100;
                    td.MC.Gold = 10;
                    td.MC.BaseMana = 30;
                    td.MC.CurrentMana = 100;

                    td.MC.MainWeapon = new ItemBackPack(Database.POOR_PRACTICE_SWORD);
                    td.MC.SubWeapon = new ItemBackPack(Database.POOR_PRACTICE_SHILED);
                    td.MC.MainArmor = new ItemBackPack(Database.POOR_COTE_OF_PLATE);
                    SettingCharacterDefault(td.MC);
                    //td.MC.Accessory;//アクセサリは無し

                    td.SC.FullName = Database.RANA_AMILIA_FULL;
                    td.SC.Name = Database.RANA_AMILIA;
                    td.SC.Strength = Database.SECONDPLAYER_FIRST_STRENGTH;
                    td.SC.Agility = Database.SECONDPLAYER_FIRST_AGILITY;
                    td.SC.Intelligence = Database.SECONDPLAYER_FIRST_INTELLIGENCE;
                    td.SC.Stamina = Database.SECONDPLAYER_FIRST_STAMINA;
                    td.SC.Mind = Database.SECONDPLAYER_FIRST_MIND;
                    td.SC.Level = 1;
                    td.SC.Exp = 0;
                    td.SC.BaseLife = 50;
                    td.SC.CurrentLife = 80;
                    td.SC.BaseSkillPoint = 100;
                    td.SC.CurrentSkillPoint = 100;
                    //td.SC.Gold = 10; // [警告]：ゴールドの所持は別クラスにするべきです。
                    td.SC.BaseMana = 30;
                    td.SC.CurrentMana = 130;
                    td.SC.MainWeapon = new ItemBackPack("ナックル");
                    td.SC.MainArmor = new ItemBackPack("ライト・クロス");
                    td.SC.Accessory = new ItemBackPack("珊瑚のブレスレット");
                    SettingCharacterDefault(td.SC);

                    td.TC.FullName = Database.OL_LANDIS_FULL;
                    td.TC.Name = Database.OL_LANDIS;
                    td.TC.Strength = Database.OL_LANDIS_FIRST_STRENGTH;
                    td.TC.Agility = Database.OL_LANDIS_FIRST_AGILITY;
                    td.TC.Intelligence = Database.OL_LANDIS_FIRST_INTELLIGENCE;
                    td.TC.Stamina = Database.OL_LANDIS_FIRST_STAMINA;
                    td.TC.Mind = Database.OL_LANDIS_FIRST_MIND;
                    td.TC.Level = 1;
                    td.TC.Exp = 0;
                    td.TC.BaseLife = 50;
                    td.TC.CurrentLife = 80;
                    td.TC.BaseSkillPoint = 100;
                    td.TC.CurrentSkillPoint = 100;
                    //td.TC.Gold = 10; // [警告]：ゴールドの所持は別クラスにするべきです。
                    td.TC.BaseMana = 30;
                    td.TC.CurrentMana = 130;
                    td.TC.MainWeapon = new ItemBackPack(Database.POOR_GOD_FIRE_GLOVE_REPLICA);
                    td.TC.MainArmor = new ItemBackPack(Database.COMMON_AURA_ARMOR);
                    td.TC.Accessory = new ItemBackPack(Database.COMMON_FATE_RING);
                    td.TC.Accessory2 = new ItemBackPack(Database.COMMON_LOYAL_RING);
                    SettingCharacterDefault(td.TC);

                    td.WE.GameDay = 1; // 初めての場合１日目とする。
                    td.WE.SaveByDungeon = false; // 初めての場合、町からスタートする。
                    td.WE.DungeonPosX = 1 + Database.DUNGEON_BASE_X + (Database.FIRST_POS % Database.TRUTH_DUNGEON_COLUMN) * Database.DUNGEON_MOVE_LEN;
                    td.WE.DungeonPosY = 1 + Database.DUNGEON_BASE_Y + (Database.FIRST_POS / Database.TRUTH_DUNGEON_COLUMN) * Database.DUNGEON_MOVE_LEN;
                    td.WE.DungeonArea = 1; // 初めての場合ダンジョンからスタートしているため、１とする。
                    td.WE.AvailableFirstCharacter = true; // 初めての場合、主人公アインを登録しておく。

                    td.BattleSpeed = this.BattleSpeed;
                    td.Difficulty = this.Difficulty;
                    GroundOne.Difficulty = this.Difficulty;
                    td.StartPosition = FormStartPosition.CenterParent;
                    td.ShowDialog();
                }
                // e 後編追加
            }
            this.Show();
            GroundOne.PlayDungeonMusic(Database.BGM12, Database.BGM12LoopBegin);
        }

        private void SettingCharacterDefault(MainCharacter player)
        {
            player.BattleActionCommand1 = Database.ATTACK_EN;
            player.BattleActionCommand2 = Database.DEFENSE_EN;
            player.BattleActionCommand3 = Database.STAY_EN;
            player.BattleActionCommand4 = Database.STAY_EN;
            player.BattleActionCommand5 = Database.STAY_EN;
            player.BattleActionCommand6 = Database.STAY_EN;
            player.BattleActionCommand7 = Database.STAY_EN;
            player.BattleActionCommand8 = Database.STAY_EN;
            player.BattleActionCommand9 = Database.STAY_EN;
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            using (SaveLoad sl = new SaveLoad())
            {
                sl.StartPosition = FormStartPosition.CenterParent;
                sl.ShowDialog();
                if (sl.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                {
                    return;
                }
                else
                {
                    this.Hide();

                    GroundOne.StopDungeonMusic();
                    GroundOne.WE2.StartSeeker = false;
                    Method.AutoSaveTruthWorldEnvironment();

                    if (this.StoryMode == 0) // 後編追加
                    {
                        using (Form1 ds = new Form1())
                        {
                            ds.MC = sl.MC;
                            ds.SC = sl.SC;
                            ds.TC = sl.TC;
                            ds.WE = sl.WE;
                            ds.KnownTileInfo = sl.KnownTileInfo;
                            ds.KnownTileInfo2 = sl.KnownTileInfo2;
                            ds.KnownTileInfo3 = sl.KnownTileInfo3;
                            ds.KnownTileInfo4 = sl.KnownTileInfo4;
                            ds.KnownTileInfo5 = sl.KnownTileInfo5;
                            ds.BattleSpeed = this.BattleSpeed;
                            ds.Difficulty = this.Difficulty;
                            GroundOne.Difficulty = this.Difficulty;
                            ds.ShowDialog();
                        }
                    }
                    else
                    {
                        // s 後編追加
                        using (TruthDungeon td = new TruthDungeon())
                        {
                            td.MC = sl.MC;
                            td.SC = sl.SC;
                            td.TC = sl.TC;
                            td.WE = sl.WE;
                            td.Truth_KnownTileInfo = sl.Truth_KnownTileInfo;
                            td.Truth_KnownTileInfo2 = sl.Truth_KnownTileInfo2;
                            td.Truth_KnownTileInfo3 = sl.Truth_KnownTileInfo3;
                            td.Truth_KnownTileInfo4 = sl.Truth_KnownTileInfo4;
                            td.Truth_KnownTileInfo5 = sl.Truth_KnownTileInfo5;
                            td.BattleSpeed = this.BattleSpeed;
                            td.Difficulty = this.Difficulty;
                            GroundOne.Difficulty = this.Difficulty;
                            td.StartPosition = FormStartPosition.CenterParent;
                            td.ShowDialog();
                        }
                        // e 後編追加
                    }
                    GroundOne.PlayDungeonMusic(Database.BGM12, Database.BGM12LoopBegin);
                }
            }
            this.Show();
            if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd && GroundOne.WE2.SelectFalseStatue)
            {
                SettingRealWorldButtonLayout();
            }
        }

        private void Seeker_Click(object sender, RoutedEventArgs e)
        {
            GroundOne.StopDungeonMusic();
            using (SaveLoad sl = new SaveLoad())
            {
                sl.RealWorldLoad();
                GroundOne.WE2.StartSeeker = true;
                Method.AutoSaveTruthWorldEnvironment();

                using (TruthDungeon td = new TruthDungeon())
                {
                    td.MC = sl.MC;
                    td.SC = sl.SC;
                    td.TC = sl.TC;
                    td.WE = sl.WE;
                    td.Truth_KnownTileInfo = sl.Truth_KnownTileInfo;
                    td.Truth_KnownTileInfo2 = sl.Truth_KnownTileInfo2;
                    td.Truth_KnownTileInfo3 = sl.Truth_KnownTileInfo3;
                    td.Truth_KnownTileInfo4 = sl.Truth_KnownTileInfo4;
                    td.Truth_KnownTileInfo5 = sl.Truth_KnownTileInfo5;
                    td.BattleSpeed = this.BattleSpeed;
                    td.Difficulty = this.Difficulty;
                    GroundOne.Difficulty = this.Difficulty;
                    td.StartPosition = FormStartPosition.CenterParent;
                    td.ShowDialog();
                }
            }
            GroundOne.PlayDungeonMusic(Database.BGM12, Database.BGM12LoopBegin);
        }

        private void Config_Click(object sender, RoutedEventArgs e)
        {
            using (GameSetting gs = new GameSetting())
            {
                gs.BattleSpeed = this.BattleSpeed;
                gs.Difficulty = this.Difficulty;
                gs.StartPosition = FormStartPosition.CenterParent;
                //gs.owner = this;
                gs.ShowDialog();
                this.BattleSpeed = gs.BattleSpeed;
                this.Difficulty = gs.Difficulty;
                GroundOne.Difficulty = gs.Difficulty;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            GroundOne.StopDungeonMusic();
        }

        private void window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.F1)
            {
                if (GroundOne.information == null)
                {
                    GroundOne.information = new TruthInformation();
                    GroundOne.information.StartPosition = FormStartPosition.CenterParent;
                    GroundOne.information.ShowDialog();
                    GroundOne.information = null;
                    GC.Collect();
                }
            }
        }
    }
}
