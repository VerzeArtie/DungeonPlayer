using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Reflection;

namespace DungeonPlayer
{
    public partial class SaveLoad : MotherForm
    {
        private string gameDayString = "\r\n経過日数：";
        private string gameDayString2 = "日 ";
        private string archiveAreaString = "到達階層：";
        private string archiveAreaString2 = "階";
        private string archiveAreaString3 = "制覇";
        private MainCharacter mc = null;
        private MainCharacter sc = null;
        private MainCharacter tc = null;
        private WorldEnvironment we = null;
        private bool[] knownTileInfo = null;
        private bool[] knownTileInfo2 = null;
        private bool[] knownTileInfo3 = null;
        private bool[] knownTileInfo4 = null;
        private bool[] knownTileInfo5 = null;

        public MainCharacter MC
        {
            get { return mc; }
            set { mc = value; }
        }
        public MainCharacter SC
        {
            get { return sc; }
            set { sc = value; }
        }
        public MainCharacter TC
        {
            get { return tc; }
            set { tc = value; }
        }

        public WorldEnvironment WE
        {
            get { return we; }
            set { we = value; }
        }

        public bool[] KnownTileInfo
        {
            get { return knownTileInfo; }
            set { knownTileInfo = value; }
        }
        public bool[] KnownTileInfo2
        {
            get { return knownTileInfo2; }
            set { knownTileInfo2 = value; }
        }
        public bool[] KnownTileInfo3
        {
            get { return knownTileInfo3; }
            set { knownTileInfo3 = value; }
        }
        public bool[] KnownTileInfo4
        {
            get { return knownTileInfo4; }
            set { knownTileInfo4 = value; }
        }
        public bool[] KnownTileInfo5
        {
            get { return knownTileInfo5; }
            set { knownTileInfo5 = value; }
        }

        protected bool saveMode = false;
        public bool SaveMode
        {
            get { return saveMode; }
            set { saveMode = value; }
        }

        public bool[] Truth_KnownTileInfo { get; set; } // 後編追加
        public bool[] Truth_KnownTileInfo2 { get; set; } // 後編追加
        public bool[] Truth_KnownTileInfo3 { get; set; } // 後編追加
        public bool[] Truth_KnownTileInfo4 { get; set; } // 後編追加
        public bool[] Truth_KnownTileInfo5 { get; set; } // 後編追加

        public SaveLoad()
        {
            InitializeComponent();
        }

        // s 後編追加
        public bool CheckCompleteData()
        {
            foreach (string filename in System.IO.Directory.GetFiles(Database.BaseSaveFolder, "*.xml"))
            {
                string targetString = System.IO.Path.GetFileName(filename);
                if (targetString.Substring(20, 1) == "6")
                {
                    return true;
                }
            }

            return false;
        }
        // e 後編追加

        private void SaveLoad_Load(object sender, EventArgs e)
        {
            Button newDateTimeButton = null;
            DateTime newDateTime = new DateTime(1, 1, 1, 0, 0, 0);

            foreach (string filename in System.IO.Directory.GetFiles(Database.BaseSaveFolder, "*.xml"))
            {
                Button targetButton = null;
                string targetString = System.IO.Path.GetFileName(filename);
                if (targetString.Contains("01_"))
                {
                    targetButton = data1;
                }
                else if (targetString.Contains("02_"))
                {
                    targetButton = data2;
                }
                else if (targetString.Contains("03_"))
                {
                    targetButton = data3;
                }
                else if (targetString.Contains("04_"))
                {
                    targetButton = data4;
                }
                else if (targetString.Contains("05_"))
                {
                    targetButton = data5;
                }
                else if (targetString.Contains("06_"))
                {
                    targetButton = data6;
                }
                else if (targetString.Contains("07_"))
                {
                    targetButton = data7;
                }
                else if (targetString.Contains("08_"))
                {
                    targetButton = data8;
                }
                else if (targetString.Contains("09_"))
                {
                    targetButton = data9;
                }
                else if (targetString.Contains("10_"))
                {
                    targetButton = data10;
                }
                else
                {
                    continue; // 後編追加（11番のオートセーブを拡張したため）
                }

                string DateTimeString = targetString.Substring(3, 4) + "/" + targetString.Substring(7, 2) + "/" + targetString.Substring(9, 2) + " " + targetString.Substring(11, 2) + ":" + targetString.Substring(13, 2) + ":" + targetString.Substring(15, 2);
                DateTime targetDateTime = DateTime.Parse(DateTimeString);
                if (targetDateTime > newDateTime)
                {
                    newDateTime = targetDateTime;
                    newDateTimeButton = targetButton;
                }

                targetButton.Text = targetString.Substring(3, 4) + "/" + targetString.Substring(7, 2) + "/" + targetString.Substring(9, 2) + " " + targetString.Substring(11, 2) + ":" + targetString.Substring(13, 2) + ":" + targetString.Substring(15, 2) + this.gameDayString + targetString.Substring(17, 3) + this.gameDayString2 + archiveAreaString;

                if (targetString.Substring(20, 1) == "6")
                {
                    targetButton.Text += this.archiveAreaString3;
                }
                else
                {
                    targetButton.Text += targetString.Substring(20, 1) + this.archiveAreaString2;
                }

                if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd)
                {
                    targetButton.Text = "";
                }
            }

            if (newDateTimeButton != null)// && GroundOne.WE2.RealWorld == false)
            {
                newDateTimeButton.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "SaveLoadNew2.png");
                newDateTimeButton.Select();
                newDateTimeButton.Focus();
            }

            if (saveMode)
            {
                label1.Text = "SAVE";
                this.BackColor = Color.Salmon;
            }
        }
        private void SaveLoad_Shown(object sender, EventArgs e)
        {
            if (GroundOne.WE2.RealWorld && !GroundOne.WE2.SeekerEnd)
            {
                using (MessageDisplay md = new MessageDisplay())
                {
                    md.StartPosition = FormStartPosition.Manual;
                    md.Location = new Point(this.Location.X, this.Location.Y + this.Size.Height / 2);
                    md.Message = "アイン・ウォーレンスが並行世界へ突入している事により、ロードを行う事はできません";
                    md.ShowDialog();
                }
                this.Close();
            }
        }
        private void LoadButton_Click(Object sender, EventArgs e)
        {
            //
            // セーブ！！！
            //
            if (saveMode)
            {
                string targetFileName = String.Empty;
                if (sender.Equals(data1))
                {
                    targetFileName = "01_";
                }
                else if (sender.Equals(data2))
                {
                    targetFileName = "02_";
                }
                else if (sender.Equals(data3))
                {
                    targetFileName = "03_";
                }
                else if (sender.Equals(data4))
                {
                    targetFileName = "04_";
                }
                else if (sender.Equals(data5))
                {
                    targetFileName = "05_";
                }
                else if (sender.Equals(data6))
                {
                    targetFileName = "06_";
                }
                else if (sender.Equals(data7))
                {
                    targetFileName = "07_";
                }
                else if (sender.Equals(data8))
                {
                    targetFileName = "08_";
                }
                else if (sender.Equals(data9))
                {
                    targetFileName = "09_";
                }
                else if (sender.Equals(data10))
                {
                    targetFileName = "10_";
                }
                ExecSave((Button)sender, targetFileName, false); // 後編移動
            }
            //
            // ロード！！！
            //
            else
            {
                if (((Button)sender).Text == String.Empty) return;

                string targetFileName = String.Empty;
                if (sender.Equals(data1))
                {
                    targetFileName = "01_";
                }
                else if (sender.Equals(data2))
                {
                    targetFileName = "02_";
                }
                else if (sender.Equals(data3))
                {
                    targetFileName = "03_";
                }
                else if (sender.Equals(data4))
                {
                    targetFileName = "04_";
                }
                else if (sender.Equals(data5))
                {
                    targetFileName = "05_";
                }
                else if (sender.Equals(data6))
                {
                    targetFileName = "06_";
                }
                else if (sender.Equals(data7))
                {
                    targetFileName = "07_";
                }
                else if (sender.Equals(data8))
                {
                    targetFileName = "08_";
                }
                else if (sender.Equals(data9))
                {
                    targetFileName = "09_";
                }
                else if (sender.Equals(data10))
                {
                    targetFileName = "10_";
                } 
                ExecLoad((Button)sender, targetFileName, false); // 後編移動

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void SaveLoad_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
            }

        }

        // s 後編追加
        public void RealWorldSave()
        {
            ExecSave(null, "11_", true);
        }
        public void RealWorldLoad()
        {
            ExecLoad(null, "11_", true);
        }
        // e 後編追加

        // move-out(s) 後編追加
        private void ExecSave(Button sender, string targetFileName, bool forceSave)
        {
            DateTime now = DateTime.Now;

            foreach (string overwriteData in System.IO.Directory.GetFiles(Database.BaseSaveFolder, "*.xml"))
            {
                if (overwriteData.Contains(targetFileName))
                {
                    if (forceSave == false) // if 後編追加
                    {
                        using (YesNoReqWithMessage yerw = new YesNoReqWithMessage())
                        {
                            yerw.StartPosition = FormStartPosition.CenterParent;
                            yerw.MainMessage = "既にデータが存在します。\r\n上書きしてセーブしますか？";
                            yerw.ShowDialog();
                            if (yerw.DialogResult == DialogResult.No)
                            {
                                return;
                            }
                            else
                            {
                                System.IO.File.Delete(overwriteData);
                            }
                        }
                    }
                    else
                    {
                        System.IO.File.Delete(overwriteData); // 後編追加
                    }
                }
            }

            targetFileName += now.Year.ToString("D4") + now.Month.ToString("D2") + now.Day.ToString("D2") + now.Hour.ToString("D2") + now.Minute.ToString("D2") + now.Second.ToString("D2") + we.GameDay.ToString("D3");
            if (we.CompleteArea5 || we.TruthCompleteArea5) // 後編編集
            {
                targetFileName += Convert.ToString(6);
            }
            else if (we.CompleteArea4 || we.TruthCompleteArea4) // 後編編集
            {
                targetFileName += Convert.ToString(5);
            }
            else if (we.CompleteArea3 || we.TruthCompleteArea3) // 後編編集
            {
                targetFileName += Convert.ToString(4);
            }
            else if (we.CompleteArea2 || we.TruthCompleteArea2) // 後編編集
            {
                targetFileName += Convert.ToString(3);
            }
            else if (we.CompleteArea1 || we.TruthCompleteArea1) // 後編編集
            {
                targetFileName += Convert.ToString(2);
            }
            else
            {
                targetFileName += Convert.ToString(1);
            }
            targetFileName += ".xml";

            XmlTextWriter xmlWriter = new XmlTextWriter(Database.BaseSaveFolder + targetFileName, Encoding.UTF8);
            try
            {
                xmlWriter.WriteStartDocument();
                xmlWriter.WriteWhitespace("\r\n");

                xmlWriter.WriteStartElement("Body");
                xmlWriter.WriteElementString("DateTime", DateTime.Now.ToString());
                xmlWriter.WriteElementString("Version", Database.VERSION.ToString());
                xmlWriter.WriteWhitespace("\r\n");

                // メインプレイヤー状態
                xmlWriter.WriteStartElement(Database.NODE_MAINPLAYERSTATUS);
                xmlWriter.WriteWhitespace("\r\n");
                // [昇華]：本記載テクニックを横展開してください。
                Type type = MC.GetType();
                foreach (PropertyInfo pi in type.GetProperties())
                {
                    if (pi.PropertyType == typeof(System.Int32))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((System.Int32)(pi.GetValue(mc, null))).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(System.String))
                    {
                        xmlWriter.WriteElementString(pi.Name, (string)(pi.GetValue(mc, null)));
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(System.Boolean))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((System.Boolean)pi.GetValue(mc, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    // s 後編追加
                    else if (pi.PropertyType == typeof(PlayerStance))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((PlayerStance)pi.GetValue(mc, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    // e 後編追加
                    // s 後編追加
                    else if (pi.PropertyType == typeof(AdditionalSpellType))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((AdditionalSpellType)pi.GetValue(mc, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(AdditionalSkillType))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((AdditionalSkillType)pi.GetValue(mc, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    // e 後編追加
                }

                // プレイヤー装備
                if (mc.MainWeapon != null)
                {
                    xmlWriter.WriteElementString("MainWeapon", mc.MainWeapon.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // s 後編追加
                if (mc.SubWeapon != null)
                {
                    xmlWriter.WriteElementString("SubWeapon", mc.SubWeapon.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // e 後編追加
                if (mc.MainArmor != null)
                {
                    xmlWriter.WriteElementString("MainArmor", mc.MainArmor.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                if (mc.Accessory != null)
                {
                    xmlWriter.WriteElementString("Accessory", mc.Accessory.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // s 後編追加
                if (mc.Accessory2 != null)
                {
                    xmlWriter.WriteElementString("Accessory2", mc.Accessory2.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // e 後編追加

                // バックパック
                if (mc != null)
                {
                    ItemBackPack[] backpackInfo = mc.GetBackPackInfo();
                    for (int ii = 0; ii < backpackInfo.Length; ii++)
                    {
                        if (backpackInfo[ii] != null)
                        {
                            // s 後編編集
                            xmlWriter.WriteElementString("BackPack" + ii.ToString(), backpackInfo[ii].Name);
                            xmlWriter.WriteWhitespace("\r\n");
                            xmlWriter.WriteElementString("BackPackStack" + ii.ToString(), backpackInfo[ii].StackValue.ToString());
                            xmlWriter.WriteWhitespace("\r\n");
                            // e 後編編集
                        }
                    }
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteWhitespace("\r\n");

                // セカンドプレイヤー状態
                xmlWriter.WriteStartElement(Database.NODE_SECONDPLAYERSTATUS);
                xmlWriter.WriteWhitespace("\r\n");
                type = SC.GetType();
                foreach (PropertyInfo pi in type.GetProperties())
                {
                    if (pi.PropertyType == typeof(System.Int32))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((System.Int32)(pi.GetValue(sc, null))).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(System.String))
                    {
                        xmlWriter.WriteElementString(pi.Name, (string)(pi.GetValue(sc, null)));
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(System.Boolean))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((System.Boolean)pi.GetValue(sc, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    // s 後編追加
                    else if (pi.PropertyType == typeof(PlayerStance))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((PlayerStance)pi.GetValue(sc, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    // e 後編追加
                    // s 後編追加
                    else if (pi.PropertyType == typeof(AdditionalSpellType))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((AdditionalSpellType)pi.GetValue(sc, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(AdditionalSkillType))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((AdditionalSkillType)pi.GetValue(sc, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    // e 後編追加   
                }

                // プレイヤー装備
                if (sc.MainWeapon != null)
                {
                    xmlWriter.WriteElementString("MainWeapon", sc.MainWeapon.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // s 後編追加
                if (sc.SubWeapon != null)
                {
                    xmlWriter.WriteElementString("SubWeapon", sc.SubWeapon.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }                    // e 後編追加
                if (sc.MainArmor != null)
                {
                    xmlWriter.WriteElementString("MainArmor", sc.MainArmor.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                if (sc.Accessory != null)
                {
                    xmlWriter.WriteElementString("Accessory", sc.Accessory.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // s 後編追加
                if (sc.Accessory2 != null)
                {
                    xmlWriter.WriteElementString("Accessory2", sc.Accessory2.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // e 後編追加

                // バックパック
                if (sc != null)
                {
                    ItemBackPack[] backpackInfo = sc.GetBackPackInfo();
                    for (int ii = 0; ii < backpackInfo.Length; ii++)
                    {
                        if (backpackInfo[ii] != null)
                        {
                            // s 後編編集
                            xmlWriter.WriteElementString("BackPack" + ii.ToString(), backpackInfo[ii].Name);
                            xmlWriter.WriteWhitespace("\r\n");
                            xmlWriter.WriteElementString("BackPackStack" + ii.ToString(), backpackInfo[ii].StackValue.ToString());
                            xmlWriter.WriteWhitespace("\r\n");
                            // e 後編編集
                        }
                    }
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteWhitespace("\r\n");


                // サードプレイヤー状態
                xmlWriter.WriteStartElement(Database.NODE_THIRDPLAYERSTATUS);
                xmlWriter.WriteWhitespace("\r\n");
                type = TC.GetType();
                foreach (PropertyInfo pi in type.GetProperties())
                {
                    if (pi.PropertyType == typeof(System.Int32))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((System.Int32)(pi.GetValue(tc, null))).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(System.String))
                    {
                        xmlWriter.WriteElementString(pi.Name, (string)(pi.GetValue(tc, null)));
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(System.Boolean))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((System.Boolean)pi.GetValue(tc, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    // s 後編追加
                    else if (pi.PropertyType == typeof(PlayerStance))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((PlayerStance)pi.GetValue(tc, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    // e 後編追加
                    // s 後編追加
                    else if (pi.PropertyType == typeof(AdditionalSpellType))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((AdditionalSpellType)pi.GetValue(tc, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    else if (pi.PropertyType == typeof(AdditionalSkillType))
                    {
                        xmlWriter.WriteElementString(pi.Name, ((AdditionalSkillType)pi.GetValue(tc, null)).ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    // e 後編追加
                }

                // プレイヤー装備
                if (tc.MainWeapon != null)
                {
                    xmlWriter.WriteElementString("MainWeapon", tc.MainWeapon.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // s 後編追加
                if (tc.SubWeapon != null)
                {
                    xmlWriter.WriteElementString("SubWeapon", tc.SubWeapon.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // e 後編追加
                if (tc.MainArmor != null)
                {
                    xmlWriter.WriteElementString("MainArmor", tc.MainArmor.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                if (tc.Accessory != null)
                {
                    xmlWriter.WriteElementString("Accessory", tc.Accessory.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // s 後編追加
                if (tc.Accessory2 != null)
                {
                    xmlWriter.WriteElementString("Accessory2", tc.Accessory2.Name);
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // e 後編追加

                // バックパック
                if (tc != null)
                {
                    ItemBackPack[] backpackInfo = tc.GetBackPackInfo();
                    for (int ii = 0; ii < backpackInfo.Length; ii++)
                    {
                        if (backpackInfo[ii] != null)
                        {
                            // s 後編編集
                            xmlWriter.WriteElementString("BackPack" + ii.ToString(), backpackInfo[ii].Name);
                            xmlWriter.WriteWhitespace("\r\n");
                            xmlWriter.WriteElementString("BackPackStack" + ii.ToString(), backpackInfo[ii].StackValue.ToString());
                            xmlWriter.WriteWhitespace("\r\n");
                            // e 後編編集
                        }
                    }
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteWhitespace("\r\n");

                // ワールド環境
                xmlWriter.WriteStartElement("WorldEnvironment");
                xmlWriter.WriteWhitespace("\r\n");
                if (we != null)
                {
                    Type typeWE = WE.GetType();
                    foreach (PropertyInfo pi in typeWE.GetProperties())
                    {
                        if (pi.PropertyType == typeof(System.Int32))
                        {
                            xmlWriter.WriteElementString(pi.Name, ((System.Int32)(pi.GetValue(we, null))).ToString());
                            xmlWriter.WriteWhitespace("\r\n");
                        }
                        else if (pi.PropertyType == typeof(System.String))
                        {
                            xmlWriter.WriteElementString(pi.Name, (string)(pi.GetValue(we, null)));
                            xmlWriter.WriteWhitespace("\r\n");
                        }
                        else if (pi.PropertyType == typeof(System.Boolean))
                        {
                            xmlWriter.WriteElementString(pi.Name, ((System.Boolean)pi.GetValue(we, null)).ToString());
                            xmlWriter.WriteWhitespace("\r\n");
                        }
                    }
                }
                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n");


                // ダンジョン１階の制覇情報
                // [警告]：作業落とし込みで終わるものの拡張性を考慮した設計に直してください。
                if (this.knownTileInfo != null) // 後編追加
                {
                    xmlWriter.WriteStartElement("DungeonOneInfo");
                    xmlWriter.WriteWhitespace("\r\n");
                    for (int ii = 0; ii < Database.DUNGEON_COLUMN * Database.DUNGEON_ROW; ii++)
                    {
                        xmlWriter.WriteElementString("tileOne" + ii, this.knownTileInfo[ii].ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n");
                }

                if (this.knownTileInfo2 != null) // 後編追加
                {
                    xmlWriter.WriteStartElement("DungeonTwoInfo");
                    xmlWriter.WriteWhitespace("\r\n");
                    for (int ii = 0; ii < Database.DUNGEON_COLUMN * Database.DUNGEON_ROW; ii++)
                    {
                        xmlWriter.WriteElementString("tileTwo" + ii, this.knownTileInfo2[ii].ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n");
                }

                if (this.knownTileInfo3 != null) // 後編追加
                {
                    xmlWriter.WriteStartElement("DungeonThreeInfo");
                    xmlWriter.WriteWhitespace("\r\n");
                    for (int ii = 0; ii < Database.DUNGEON_COLUMN * Database.DUNGEON_ROW; ii++)
                    {
                        xmlWriter.WriteElementString("tileThree" + ii, this.knownTileInfo3[ii].ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n");
                }

                if (this.knownTileInfo4 != null) // 後編追加
                {
                    xmlWriter.WriteStartElement("DungeonFourInfo");
                    xmlWriter.WriteWhitespace("\r\n");
                    for (int ii = 0; ii < Database.DUNGEON_COLUMN * Database.DUNGEON_ROW; ii++)
                    {
                        xmlWriter.WriteElementString("tileFour" + ii, this.knownTileInfo4[ii].ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n");
                }

                if (this.knownTileInfo5 != null) // 後編追加
                {
                    xmlWriter.WriteStartElement("DungeonFiveInfo");
                    xmlWriter.WriteWhitespace("\r\n");
                    for (int ii = 0; ii < Database.DUNGEON_COLUMN * Database.DUNGEON_ROW; ii++)
                    {
                        xmlWriter.WriteElementString("tileFive" + ii, this.knownTileInfo5[ii].ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n");
                }

                // s 後編追加
                if (this.Truth_KnownTileInfo != null)
                {
                    xmlWriter.WriteStartElement("TruthDungeonOneInfo");
                    xmlWriter.WriteWhitespace("\r\n");
                    for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
                    {
                        xmlWriter.WriteElementString("truthTileOne" + ii, this.Truth_KnownTileInfo[ii].ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n");
                }

                if (this.Truth_KnownTileInfo2 != null)
                {
                    xmlWriter.WriteStartElement("TruthDungeonTwoInfo");
                    xmlWriter.WriteWhitespace("\r\n");
                    for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
                    {
                        xmlWriter.WriteElementString("truthTileTwo" + ii, this.Truth_KnownTileInfo2[ii].ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n");
                }

                if (this.Truth_KnownTileInfo3 != null)
                {
                    xmlWriter.WriteStartElement("TruthDungeonThreeInfo");
                    xmlWriter.WriteWhitespace("\r\n");
                    for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
                    {
                        xmlWriter.WriteElementString("truthTileThree" + ii, this.Truth_KnownTileInfo3[ii].ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n");
                }

                if (this.Truth_KnownTileInfo4 != null)
                {
                    xmlWriter.WriteStartElement("TruthDungeonFourInfo");
                    xmlWriter.WriteWhitespace("\r\n");
                    for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
                    {
                        xmlWriter.WriteElementString("truthTileFour" + ii, this.Truth_KnownTileInfo4[ii].ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n");
                }

                if (this.Truth_KnownTileInfo5 != null)
                {
                    xmlWriter.WriteStartElement("TruthDungeonFiveInfo");
                    xmlWriter.WriteWhitespace("\r\n");
                    for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
                    {
                        xmlWriter.WriteElementString("truthTileFive" + ii, this.Truth_KnownTileInfo5[ii].ToString());
                        xmlWriter.WriteWhitespace("\r\n");
                    }
                    xmlWriter.WriteEndElement();
                    xmlWriter.WriteWhitespace("\r\n");
                }
                // e 後編追加

                xmlWriter.WriteEndElement();
                xmlWriter.WriteWhitespace("\r\n");
                xmlWriter.WriteEndDocument();
            }
            finally
            {
                xmlWriter.Close();

                if ((Button)sender != null) // if 後編追加
                {
                    ((Button)sender).Text = DateTime.Now.ToString() + "\r\n経過日数：" + WE.GameDay.ToString("D3") + "日 ";
                    if (we.CompleteArea5 || we.TruthCompleteArea5) // 後編編集
                    {
                        ((Button)sender).Text += archiveAreaString + archiveAreaString3;
                    }
                    else if (we.CompleteArea4 || we.TruthCompleteArea4) // 後編編集
                    {
                        ((Button)sender).Text += archiveAreaString + "5" + archiveAreaString2;
                    }
                    else if (we.CompleteArea3 || we.TruthCompleteArea3) // 後編編集
                    {
                        ((Button)sender).Text += archiveAreaString + "4" + archiveAreaString2;
                    }
                    else if (we.CompleteArea2 || we.TruthCompleteArea2) // 後編編集
                    {
                        ((Button)sender).Text += archiveAreaString + "3" + archiveAreaString2;
                    }
                    else if (we.CompleteArea1 || we.TruthCompleteArea1) // 後編編集
                    {
                        ((Button)sender).Text += archiveAreaString + "2" + archiveAreaString2;
                    }
                    else
                    {
                        ((Button)sender).Text += archiveAreaString + "1" + archiveAreaString2;
                    }

                    if (!((Button)sender).Equals(data1)) data1.BackgroundImage = null;
                    if (!((Button)sender).Equals(data2)) data2.BackgroundImage = null;
                    if (!((Button)sender).Equals(data3)) data3.BackgroundImage = null;
                    if (!((Button)sender).Equals(data4)) data4.BackgroundImage = null;
                    if (!((Button)sender).Equals(data5)) data5.BackgroundImage = null;
                    if (!((Button)sender).Equals(data6)) data6.BackgroundImage = null;
                    if (!((Button)sender).Equals(data7)) data7.BackgroundImage = null;
                    if (!((Button)sender).Equals(data8)) data8.BackgroundImage = null;
                    if (!((Button)sender).Equals(data9)) data9.BackgroundImage = null;
                    if (!((Button)sender).Equals(data10)) data10.BackgroundImage = null;
                    ((Button)sender).BackgroundImage = Image.FromFile(Database.BaseResourceFolder + "SaveLoadNew2.png");
                }

                // s 後編追加
                Method.AutoSaveTruthWorldEnvironment();
                // e 後編追加

                if (!forceSave) // if 後編追加
                {
                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "保存が完了しました。";
                        md.AutoKillTimer = 1500;
                        md.ShowDialog();
                    }
                }
            }
        }

        private void ExecLoad(Button sender, string targetFileName, bool forceLoad)
        {

            mc = new MainCharacter();
            sc = new MainCharacter();
            tc = new MainCharacter();
            we = new WorldEnvironment();
            knownTileInfo = new bool[Database.DUNGEON_ROW * Database.DUNGEON_COLUMN];
            knownTileInfo2 = new bool[Database.DUNGEON_ROW * Database.DUNGEON_COLUMN];
            knownTileInfo3 = new bool[Database.DUNGEON_ROW * Database.DUNGEON_COLUMN];
            knownTileInfo4 = new bool[Database.DUNGEON_ROW * Database.DUNGEON_COLUMN];
            knownTileInfo5 = new bool[Database.DUNGEON_ROW * Database.DUNGEON_COLUMN];
            Truth_KnownTileInfo = new bool[Database.TRUTH_DUNGEON_ROW * Database.TRUTH_DUNGEON_COLUMN]; // 後編追加
            Truth_KnownTileInfo2 = new bool[Database.TRUTH_DUNGEON_ROW * Database.TRUTH_DUNGEON_COLUMN]; // 後編追加
            Truth_KnownTileInfo3 = new bool[Database.TRUTH_DUNGEON_ROW * Database.TRUTH_DUNGEON_COLUMN]; // 後編追加
            Truth_KnownTileInfo4 = new bool[Database.TRUTH_DUNGEON_ROW * Database.TRUTH_DUNGEON_COLUMN]; // 後編追加
            Truth_KnownTileInfo5 = new bool[Database.TRUTH_DUNGEON_ROW * Database.TRUTH_DUNGEON_COLUMN]; // 後編追加


            XmlDocument xml = new XmlDocument();
            DateTime now = DateTime.Now;
            string yearData = String.Empty;
            string monthData = String.Empty;
            string dayData = String.Empty;
            string hourData = String.Empty;
            string minuteData = String.Empty;
            string secondData = String.Empty;
            string gamedayData = String.Empty;
            string completeareaData = String.Empty;

            if (((Button)sender) != null)
            {
                yearData = ((Button)sender).Text.Substring(0, 4);
                monthData = ((Button)sender).Text.Substring(5, 2);
                dayData = ((Button)sender).Text.Substring(8, 2);
                hourData = ((Button)sender).Text.Substring(11, 2);
                minuteData = ((Button)sender).Text.Substring(14, 2);
                secondData = ((Button)sender).Text.Substring(17, 2);
                gamedayData = ((Button)sender).Text.Substring(this.gameDayString.Length + 19, 3);
                completeareaData = ((Button)sender).Text.Substring(this.gameDayString.Length + this.gameDayString2.Length + this.archiveAreaString.Length + 22, 1);

                if (completeareaData == "制")
                {
                    using (MessageDisplay md = new MessageDisplay())
                    {
                        md.StartPosition = FormStartPosition.CenterParent;
                        md.Message = "ダンジョンシーカー（後編）用クリアデータです。本編ではロードできません。";
                        md.ShowDialog();
                    }
                    return;
                }
                targetFileName += yearData + monthData + dayData + hourData + minuteData + secondData + gamedayData + completeareaData + ".xml";
            }
            else
            {
                foreach (string currentFile in System.IO.Directory.GetFiles(Database.BaseSaveFolder, "*.xml"))
                {
                    if (currentFile.Contains("11_"))
                    {
                        targetFileName = System.IO.Path.GetFileName(currentFile);
                        break;
                    }
                }
            }


            xml.Load(Database.BaseSaveFolder + targetFileName);

            try
            {
                XmlNodeList currentList = xml.GetElementsByTagName("MainWeapon");
                foreach (XmlNode node in currentList)
                {
                    if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                    {
                        MC.MainWeapon = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                    {
                        SC.MainWeapon = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                    {
                        TC.MainWeapon = new ItemBackPack(node.InnerText);
                    }
                }
            }
            catch { }
            // s 後編追加
            try
            {
                XmlNodeList currentList = xml.GetElementsByTagName("SubWeapon");
                foreach (XmlNode node in currentList)
                {
                    if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                    {
                        MC.SubWeapon = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                    {
                        SC.SubWeapon = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                    {
                        TC.SubWeapon = new ItemBackPack(node.InnerText);
                    }
                }
            }
            catch { }
            // e 後編追加
            try
            {
                XmlNodeList currentList = xml.GetElementsByTagName("MainArmor");
                foreach (XmlNode node in currentList)
                {
                    if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                    {
                        MC.MainArmor = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                    {
                        SC.MainArmor = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                    {
                        TC.MainArmor = new ItemBackPack(node.InnerText);
                    }
                }
            }
            catch { }
            try
            {
                XmlNodeList currentList = xml.GetElementsByTagName("Accessory");
                foreach (XmlNode node in currentList)
                {
                    if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                    {
                        MC.Accessory = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                    {
                        SC.Accessory = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                    {
                        TC.Accessory = new ItemBackPack(node.InnerText);
                    }
                }
            }
            catch { }
            // s 後編追加
            try
            {
                XmlNodeList currentList = xml.GetElementsByTagName("Accessory2");
                foreach (XmlNode node in currentList)
                {
                    if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                    {
                        MC.Accessory2 = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                    {
                        SC.Accessory2 = new ItemBackPack(node.InnerText);
                    }
                    else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                    {
                        TC.Accessory2 = new ItemBackPack(node.InnerText);
                    }
                }
            }
            catch { }
            // e 後編追加

            //for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
            //{
            //    XmlNodeList temp = xml.GetElementsByTagName("BackPack" + ii.ToString());
            //    if (temp.Count <= 0)
            //    {
            //    }
            //    else
            //    {
            //        foreach (XmlNode node in temp)
            //        {
            //            if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
            //            {
            //                MC.AddBackPack(new ItemBackPack(node.InnerText));
            //            }
            //            else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
            //            {
            //                SC.AddBackPack(new ItemBackPack(node.InnerText));
            //            }
            //            else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
            //            {
            //                TC.AddBackPack(new ItemBackPack(node.InnerText));
            //            }
            //        }
            //    }
            //}

            // s 後編編集

            for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
            {
                XmlNodeList temp = xml.GetElementsByTagName("BackPack" + ii.ToString());
                foreach (XmlNode node in temp)
                {
                    if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                    {
                        XmlNodeList temp2 = xml.GetElementsByTagName("BackPackStack" + ii.ToString());
                        if (temp2.Count <= 0) // 旧互換の場合、必ずスタック量は１つである。
                        {
                            MC.AddBackPack(new ItemBackPack(node.InnerText));
                        }
                        else
                        {
                            foreach (XmlNode node2 in temp2)
                            {
                                if (node2.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                                {
                                    for (int kk = 0; kk < Convert.ToInt32(node2.InnerText); kk++)
                                    {
                                        MC.AddBackPack(new ItemBackPack(node.InnerText));
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                    {
                        XmlNodeList temp2 = xml.GetElementsByTagName("BackPackStack" + ii.ToString());
                        if (temp2.Count <= 0) // 旧互換の場合、必ずスタック量は１つである。
                        {
                            SC.AddBackPack(new ItemBackPack(node.InnerText));
                        }
                        else
                        {
                            foreach (XmlNode node2 in temp2)
                            {
                                if (node2.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                                {
                                    for (int kk = 0; kk < Convert.ToInt32(node2.InnerText); kk++)
                                    {
                                        SC.AddBackPack(new ItemBackPack(node.InnerText));
                                    }
                                    break;
                                }
                            }
                        }
                    }
                    else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                    {
                        XmlNodeList temp2 = xml.GetElementsByTagName("BackPackStack" + ii.ToString());
                        if (temp2.Count <= 0) // 旧互換の場合、必ずスタック量は１つである。
                        {
                            TC.AddBackPack(new ItemBackPack(node.InnerText));
                        }
                        else
                        {
                            foreach (XmlNode node2 in temp2)
                            {
                                if (node2.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                                {
                                    for (int kk = 0; kk < Convert.ToInt32(node2.InnerText); kk++)
                                    {
                                        TC.AddBackPack(new ItemBackPack(node.InnerText));
                                    }
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            // e 後編編集

            Type type = this.MC.GetType();
            foreach (PropertyInfo pi in type.GetProperties())
            {
                // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                if (pi.PropertyType == typeof(System.Int32))
                {
                    try
                    {
                        XmlNodeList currentList = xml.GetElementsByTagName(pi.Name);
                        foreach (XmlNode node in currentList)
                        {
                            if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                            {
                                pi.SetValue(MC, Convert.ToInt32(node.InnerText), null);
                            }
                            else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                            {
                                pi.SetValue(SC, Convert.ToInt32(node.InnerText), null);
                            }
                            else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                            {
                                pi.SetValue(TC, Convert.ToInt32(node.InnerText), null);
                            }
                        }
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.String))
                {
                    try
                    {
                        XmlNodeList currentList = xml.GetElementsByTagName(pi.Name);
                        foreach (XmlNode node in currentList)
                        {
                            if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                            {
                                pi.SetValue(MC, node.InnerText, null);
                            }
                            else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                            {
                                pi.SetValue(SC, node.InnerText, null);
                            }
                            else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                            {
                                pi.SetValue(TC, node.InnerText, null);
                            }
                        }
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.Boolean))
                {
                    try
                    {
                        XmlNodeList currentList = xml.GetElementsByTagName(pi.Name);
                        foreach (XmlNode node in currentList)
                        {
                            if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                            {
                                pi.SetValue(MC, Convert.ToBoolean(node.InnerText), null);
                            }
                            else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                            {
                                pi.SetValue(SC, Convert.ToBoolean(node.InnerText), null);
                            }
                            else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                            {
                                pi.SetValue(TC, Convert.ToBoolean(node.InnerText), null);
                            }
                        }
                    }
                    catch { }
                }
                // s 後編追加
                else if (pi.PropertyType == typeof(PlayerStance))
                {
                    try
                    {
                        XmlNodeList currentList = xml.GetElementsByTagName(pi.Name);
                        foreach (XmlNode node in currentList)
                        {
                            if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                            {
                                pi.SetValue(MC, (PlayerStance)Enum.Parse(typeof(PlayerStance), node.InnerText), null);
                            }
                            else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                            {
                                pi.SetValue(SC, (PlayerStance)Enum.Parse(typeof(PlayerStance), node.InnerText), null);
                            }
                            else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                            {
                                pi.SetValue(TC, (PlayerStance)Enum.Parse(typeof(PlayerStance), node.InnerText), null);
                            }
                        }
                    }
                    catch { }
                }
                // e 後編追加
                // s 後編追加
                else if (pi.PropertyType == typeof(AdditionalSpellType))
                {
                    try
                    {
                        XmlNodeList currentList = xml.GetElementsByTagName(pi.Name);
                        foreach (XmlNode node in currentList)
                        {
                            if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                            {
                                pi.SetValue(MC, (AdditionalSpellType)Enum.Parse(typeof(AdditionalSpellType), node.InnerText), null);
                            }
                            else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                            {
                                pi.SetValue(SC, (AdditionalSpellType)Enum.Parse(typeof(AdditionalSpellType), node.InnerText), null);
                            }
                            else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                            {
                                pi.SetValue(TC, (AdditionalSpellType)Enum.Parse(typeof(AdditionalSpellType), node.InnerText), null);
                            }
                        }
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(AdditionalSkillType))
                {
                    try
                    {
                        XmlNodeList currentList = xml.GetElementsByTagName(pi.Name);
                        foreach (XmlNode node in currentList)
                        {
                            if (node.ParentNode.Name == Database.NODE_MAINPLAYERSTATUS)
                            {
                                pi.SetValue(MC, (AdditionalSkillType)Enum.Parse(typeof(AdditionalSkillType), node.InnerText), null);
                            }
                            else if (node.ParentNode.Name == Database.NODE_SECONDPLAYERSTATUS)
                            {
                                pi.SetValue(SC, (AdditionalSkillType)Enum.Parse(typeof(AdditionalSkillType), node.InnerText), null);
                            }
                            else if (node.ParentNode.Name == Database.NODE_THIRDPLAYERSTATUS)
                            {
                                pi.SetValue(TC, (AdditionalSkillType)Enum.Parse(typeof(AdditionalSkillType), node.InnerText), null);
                            }
                        }
                    }
                    catch { }
                }
                // e 後編追加
            }


            Type typeWE = this.WE.GetType();
            #region "Tresureプロパティ名が誤っていたのを、Treasureに修正してしまったため、旧XMLファイル互換が取れないので、以下の対応を取る。今後このような安易なプロパティ名称改版をしないでください。
            try { WE.Treasure1 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure1")[0].InnerText); }
            catch { }
            try { WE.Treasure2 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure2")[0].InnerText); }
            catch { }
            try { WE.Treasure3 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure3")[0].InnerText); }
            catch { }
            try { WE.Treasure4 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure4")[0].InnerText); }
            catch { }
            try { WE.Treasure5 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure5")[0].InnerText); }
            catch { }
            try { WE.Treasure6 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure6")[0].InnerText); }
            catch { }
            try { WE.Treasure7 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure7")[0].InnerText); }
            catch { }
            try { WE.Treasure8 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure8")[0].InnerText); }
            catch { }
            try { WE.Treasure9 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure9")[0].InnerText); }
            catch { }
            try { WE.Treasure10 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure10")[0].InnerText); }
            catch { }
            try { WE.Treasure11 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure11")[0].InnerText); }
            catch { }
            try { WE.Treasure12 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure12")[0].InnerText); }
            catch { }
            try { WE.Treasure121 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure121")[0].InnerText); }
            catch { }
            try { WE.Treasure122 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure122")[0].InnerText); }
            catch { }
            try { WE.Treasure123 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure123")[0].InnerText); }
            catch { }
            try { WE.Treasure41 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure41")[0].InnerText); }
            catch { }
            try { WE.Treasure42 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure42")[0].InnerText); }
            catch { }
            try { WE.Treasure43 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure43")[0].InnerText); }
            catch { }
            try { WE.Treasure44 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure44")[0].InnerText); }
            catch { }
            try { WE.Treasure45 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure45")[0].InnerText); }
            catch { }
            try { WE.Treasure46 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure46")[0].InnerText); }
            catch { }
            try { WE.Treasure47 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure47")[0].InnerText); }
            catch { }
            try { WE.Treasure48 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure48")[0].InnerText); }
            catch { }
            try { WE.Treasure49 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure49")[0].InnerText); }
            catch { }
            try { WE.Treasure51 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure51")[0].InnerText); }
            catch { }
            try { WE.Treasure52 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure52")[0].InnerText); }
            catch { }
            try { WE.Treasure53 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure53")[0].InnerText); }
            catch { }
            try { WE.Treasure54 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure54")[0].InnerText); }
            catch { }
            try { WE.Treasure55 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure55")[0].InnerText); }
            catch { }
            try { WE.Treasure56 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure56")[0].InnerText); }
            catch { }
            try { WE.Treasure57 = Convert.ToBoolean(xml.GetElementsByTagName("Tresure57")[0].InnerText); }
            catch { }
            #endregion

            foreach (PropertyInfo pi in typeWE.GetProperties())
            {
                // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                if (pi.PropertyType == typeof(System.Int32))
                {
                    try
                    {
                        pi.SetValue(WE, Convert.ToInt32(xml.GetElementsByTagName(pi.Name)[0].InnerText), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.String))
                {
                    try
                    {
                        pi.SetValue(WE, (xml.GetElementsByTagName(pi.Name)[0].InnerText), null);
                    }
                    catch { }
                }
                else if (pi.PropertyType == typeof(System.Boolean))
                {
                    try
                    {
                        pi.SetValue(WE, Convert.ToBoolean(xml.GetElementsByTagName(pi.Name)[0].InnerText), null);
                    }
                    catch { }
                }
            }

            try // 後編追加 // [警告]：前編での読み込みバグが無く、かつ、後編では絶対に使わないことを前提とした記述。
            {
                for (int ii = 0; ii < Database.DUNGEON_COLUMN * Database.DUNGEON_ROW; ii++)
                {
                    knownTileInfo[ii] = Convert.ToBoolean(xml.GetElementsByTagName(("tileOne" + ii.ToString()))[0].InnerText);
                    knownTileInfo2[ii] = Convert.ToBoolean(xml.GetElementsByTagName(("tileTwo" + ii.ToString()))[0].InnerText);
                    knownTileInfo3[ii] = Convert.ToBoolean(xml.GetElementsByTagName(("tileThree" + ii.ToString()))[0].InnerText);
                    knownTileInfo4[ii] = Convert.ToBoolean(xml.GetElementsByTagName(("tileFour" + ii.ToString()))[0].InnerText);
                    knownTileInfo5[ii] = Convert.ToBoolean(xml.GetElementsByTagName(("tileFive" + ii.ToString()))[0].InnerText);
                }
            }
            catch { }

            // [必須] 最終的には全階層分のデータを一括取得するようになるので、このFor分割は不要となる。
            //string temp1 = DateTime.Now.ToString() + "  " + DateTime.Now.Millisecond.ToString();

            XmlNodeList list1 = xml.DocumentElement.SelectNodes("/Body/TruthDungeonOneInfo");
            XmlNodeList list2 = xml.DocumentElement.SelectNodes("/Body/TruthDungeonTwoInfo");
            XmlNodeList list3 = xml.DocumentElement.SelectNodes("/Body/TruthDungeonThreeInfo");
            XmlNodeList list4 = xml.DocumentElement.SelectNodes("/Body/TruthDungeonFourInfo");
            XmlNodeList list5 = xml.DocumentElement.SelectNodes("/Body/TruthDungeonFiveInfo");

            // s 後編追加
            try
            {
                for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
                {
                    Truth_KnownTileInfo[ii] = Convert.ToBoolean(list1[0]["truthTileOne" + ii.ToString()].InnerText);

                }
            }
            catch { }

            try
            {
                for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
                {
                    Truth_KnownTileInfo2[ii] = Convert.ToBoolean(list2[0]["truthTileTwo" + ii.ToString()].InnerText);
                }

            }
            catch { }

            try
            {
                for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
                {
                    Truth_KnownTileInfo3[ii] = Convert.ToBoolean(list3[0]["truthTileThree" + ii.ToString()].InnerText);
                }
            }
            catch { }

            try
            {
                for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
                {
                    Truth_KnownTileInfo4[ii] = Convert.ToBoolean(list4[0]["truthTileFour" + ii.ToString()].InnerText);
                }
            }
            catch { }

            try
            {
                for (int ii = 0; ii < Database.TRUTH_DUNGEON_COLUMN * Database.TRUTH_DUNGEON_ROW; ii++)
                {
                    Truth_KnownTileInfo5[ii] = Convert.ToBoolean(list5[0]["truthTileFive" + ii.ToString()].InnerText);
                }
            }
            catch { }            // e 後編追加
            //string tempZ = DateTime.Now.ToString() + "  " + DateTime.Now.Millisecond.ToString();
            // MessageBox.Show(temp1 + "\r\n" + tempZ);

            XmlDocument xml2 = new XmlDocument();
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

            if (forceLoad == false)
            {
                using (MessageDisplay md = new MessageDisplay())
                {
                    md.Message = "ゲームデータの読み込みが完了しました。";
                    md.StartPosition = FormStartPosition.CenterParent;
                    md.AutoKillTimer = 1500;
                    md.ShowDialog();
                }
            }

            this.DialogResult = DialogResult.OK;
        }
        // move-out(e) 後編追加

    }
}