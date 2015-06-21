using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Reflection;
using System.Collections;
//using Microsoft.DirectX.DirectSound;
using System.Runtime.InteropServices;
using System.Threading;
using System.Media;

namespace DungeonPlayer
{
    public partial class Form1 : MotherForm
    {
        private int basePosX = 1; // 161
        private int basePosY = 1;
        private int moveLen = 16;

        MainCharacter mc = null; // 主人公：アイン
        MainCharacter sc = null; // ヒロイン：ラナ
        MainCharacter tc = null; // シャドウ：ヴェルゼ
        WorldEnvironment we = null; // ダンジョン進行状況

        private PictureBox[] dungeonTile = null;
        private PictureBox[] unknownTile = null;

        string[] tileInfo = null;
        string[] tileInfo2 = null;
        string[] tileInfo3 = null;
        string[] tileInfo4 = null;
        string[] tileInfo5 = null;
        bool[] knownTileInfo = null;
        bool[] knownTileInfo2 = null;
        bool[] knownTileInfo3 = null;
        bool[] knownTileInfo4 = null;
        bool[] knownTileInfo5 = null;

        //Microsoft.DirectX.DirectSound.Device soundDevice = new Microsoft.DirectX.DirectSound.Device();
        //SecondaryBuffer shotsound = null;

        Thread th; // BGM用のスレッド
        bool endSign; // BGM用スレッドの終了サイン
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

        public bool NewGameFlag { get; set; }

        private int stepCounter = 0; // 敵エンカウント率調整の値

        public Form1()
        {
            InitializeComponent();

            tileInfo = new string[Database.DUNGEON_ROW * Database.DUNGEON_COLUMN];
            tileInfo2 = new string[Database.DUNGEON_ROW * Database.DUNGEON_COLUMN];
            tileInfo3 = new string[Database.DUNGEON_ROW * Database.DUNGEON_COLUMN];
            tileInfo4 = new string[Database.DUNGEON_ROW * Database.DUNGEON_COLUMN];
            tileInfo5 = new string[Database.DUNGEON_ROW * Database.DUNGEON_COLUMN];

            dungeonTile = new PictureBox[Database.DUNGEON_ROW * Database.DUNGEON_COLUMN];
            unknownTile = new PictureBox[Database.DUNGEON_ROW * Database.DUNGEON_COLUMN];

            //th = new Thread(new System.Threading.ThreadStart(UpdateXAudio));
            //th.IsBackground = true;
            //th.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PreInitialize()
        {
            this.Hide();
            this.firstLoadIgnoreMusic = false;
            GroundOne.StopDungeonMusic();
            this.mainMessage.Text = "";
            this.mainMessage.Update();
            for (int ii = 0; ii < this.tileInfo.Length; ii++)
            {
                tileInfo[ii] = "";
                tileInfo2[ii] = "";
                tileInfo3[ii] = "";
                tileInfo4[ii] = "";
                tileInfo5[ii] = "";
            }


            for (int ii = 0; ii < this.dungeonTile.Length; ii++)
            {
                this.dungeonTile[ii].Image = null;
                this.Controls.Remove(dungeonTile[ii]);
                this.unknownTile[ii].Image = null;
                this.Controls.Remove(unknownTile[ii]);
            }
            this.Player.BackgroundImage = null;
            this.Player.Update();
            Form1_Load(null, null);
            this.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (mc != null)//(System.IO.File.Exists(Database.SaveDataFile1))
            {
                // SaveLoadのLoadフェーズに移行しました。
            }
            else
            {
                // SaveLoadのNewGameフェーズに移行しました。
            }

            #region "移動改善前のデータを対象とする場合、位置情報を適切に読み込む必要があります"
            // 新しくゲームを始めた場合
            if (this.NewGameFlag)
            {
                this.Player.Location = new Point(we.DungeonPosX, we.DungeonPosY);
            }
            else
            {
                // ゲームロードした場合
                if (we.DungeonPosX2 == 0 && we.DungeonPosY2 == 0) // 過去に新しい環境でセーブしたデータがないため・・・
                {
                    // 過去のセーブがある場合はX座標をマイナスする。
                    this.Player.Location = new Point(we.DungeonPosX - 160, we.DungeonPosY);
                }
                else
                {
                    // 過去に新しい環境でセーブしたデータがある場合、それをそのまま使う。
                    this.Player.Location = new Point(we.DungeonPosX2, we.DungeonPosY2);
                }
            }
            #endregion

            this.dayLabel.Text = we.GameDay.ToString() + "日目";
            this.dungeonAreaLabel.Text = we.DungeonArea.ToString() + "　階";

            for (int ii = 0; ii < Database.DUNGEON_ROW * Database.DUNGEON_COLUMN; ii++)
            {
                tileInfo[ii] = "Tile1.bmp";
                tileInfo2[ii] = "Tile1.bmp";
                tileInfo3[ii] = "Tile1.bmp";
                tileInfo4[ii] = "Tile1.bmp";
                tileInfo5[ii] = "Tile1.bmp";
                dungeonTile[ii] = new PictureBox();
                dungeonTile[ii].Size = new Size(Database.DUNGEON_MOVE_LEN, Database.DUNGEON_MOVE_LEN);
                dungeonTile[ii].Location = new Point(Database.DUNGEON_BASE_X + (ii % Database.DUNGEON_COLUMN) * Database.DUNGEON_MOVE_LEN,
                                                     Database.DUNGEON_BASE_Y + (ii / Database.DUNGEON_COLUMN) * Database.DUNGEON_MOVE_LEN);
                //dungeonField.Controls.Add(dungeonTile[ii]); // dungeonFieldのPictureBoxで一括表示します。

                unknownTile[ii] = new PictureBox();
                unknownTile[ii].Size = new Size(Database.DUNGEON_MOVE_LEN, Database.DUNGEON_MOVE_LEN);
                unknownTile[ii].Location = new Point(Database.DUNGEON_BASE_X + (ii % Database.DUNGEON_COLUMN) * Database.DUNGEON_MOVE_LEN,
                                                     Database.DUNGEON_BASE_Y + (ii / Database.DUNGEON_COLUMN) * Database.DUNGEON_MOVE_LEN);
                unknownTile[ii].Visible = !knownTileInfo[ii]; // 反対ですが意味付けは同じ本質です。
                //dungeonField.Controls.Add(unknownTile[ii]); // dungeonFieldのPictureBoxで一括表示します。
            }

            ConstructDungeonMap();

            for (int ii = 0; ii < Database.DUNGEON_ROW * Database.DUNGEON_COLUMN; ii++)
            {
                dungeonTile[ii].Image = Image.FromFile(Database.BaseResourceFolder + Database.FloorFolder[we.DungeonArea - 1] + tileInfo[ii]);
                unknownTile[ii].BringToFront();
            }
            SetupDungeonMapping(we.DungeonArea);
            if (!we.SaveByDungeon)
            {
                firstLoadIgnoreMusic = true;
                CallHomeTown(false);
                mainMessage.Text = "";
            }
            if (we.CompleteSlayBoss5)
            {
                firstLoadIgnoreMusic = true;
            }
            else
            {
                GroundOne.PlayDungeonMusic(Database.BGM02, Database.BGM02LoopBegin);
            }

            SetupPlayerStatus();
        }


        private void dungeonField_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int ii = 0; ii < Database.DUNGEON_ROW * Database.DUNGEON_COLUMN; ii++)
            {
                g.DrawImage(dungeonTile[ii].Image, (float)dungeonTile[ii].Location.X, (float)dungeonTile[ii].Location.Y);
                if (unknownTile[ii].Visible)
                {
                    g.DrawImage(unknownTile[ii].Image, (float)unknownTile[ii].Location.X, (float)unknownTile[ii].Location.Y);
                }
            }
        }

        private bool firstLoadIgnoreMusic = false;

        private void ConstructDungeonMap()
        {
            #region "ダンジョン１階"
            // ０行目
            tileInfo[0] = "Tile1-WallTL.bmp";
            tileInfo[1] = "Tile1-WallTB.bmp";
            tileInfo[2] = "Tile1-WallT.bmp";
            tileInfo[3] = "Tile1-WallTB.bmp";
            tileInfo[4] = "Tile1-WallTB.bmp";
            tileInfo[5] = "Tile1-WallTB.bmp";
            tileInfo[6] = "Tile1-WallTB.bmp";
            tileInfo[7] = "Tile1-WallTB.bmp";
            tileInfo[8] = "Tile1-WallTB.bmp";
            tileInfo[9] = "Tile1-WallTRB.bmp";
            tileInfo[10] = "Tile1-WallT.bmp";
            tileInfo[11] = "Tile1-WallT.bmp";
            tileInfo[12] = "Tile1-WallT.bmp";
            tileInfo[13] = "Tile1-WallT.bmp";
            tileInfo[14] = "Tile1-WallTL.bmp";
            tileInfo[15] = "Tile1-WallTB.bmp";
            tileInfo[16] = "Tile1-WallTB.bmp";
            tileInfo[17] = "Tile1-WallTB.bmp";
            tileInfo[18] = "Tile1-WallTB.bmp";
            tileInfo[19] = "Tile1-WallTB.bmp";
            tileInfo[20] = "Tile1-WallTB.bmp";
            tileInfo[21] = "Tile1-WallTB.bmp";
            tileInfo[22] = "Tile1-WallTB.bmp";
            tileInfo[23] = "Tile1-WallTB.bmp";
            tileInfo[24] = "Tile1-WallTB.bmp";
            tileInfo[25] = "Tile1-WallTB.bmp";
            tileInfo[26] = "Tile1-WallTR.bmp";
            tileInfo[27] = "Tile1-WallT.bmp";
            tileInfo[28] = "Tile1-WallT.bmp";
            tileInfo[29] = "Tile1-WallTLR.bmp";

            // １行目
            tileInfo[30] = "Tile1-WallLR.bmp";
            tileInfo[32] = "Upstair-WallLRB.bmp";
            tileInfo[44] = "Tile1-WallL.bmp";
            tileInfo[45] = "Tile1-WallTB.bmp";
            tileInfo[46] = "Tile1-WallTB.bmp";
            tileInfo[47] = "Tile1-WallTB.bmp";
            tileInfo[48] = "Tile1-WallTB.bmp";
            tileInfo[49] = "Tile1-WallTB.bmp";
            tileInfo[50] = "Tile1-WallTB.bmp";
            tileInfo[51] = "Tile1-WallTR.bmp";
            tileInfo[56] = "Tile1-WallLR.bmp";
            tileInfo[59] = "Tile1-WallLR.bmp";

            // ２行目
            tileInfo[60] = "Tile1-WallLR.bmp";
            tileInfo[74] = "Tile1-WallL.bmp";
            tileInfo[75] = "Tile1-WallTB.bmp";
            tileInfo[76] = "Tile1-WallTB.bmp";
            tileInfo[77] = "Tile1-WallTB.bmp";
            tileInfo[78] = "Tile1-WallTR.bmp";
            tileInfo[81] = "Tile1-WallLR.bmp";
            tileInfo[86] = "Tile1-WallLR.bmp";
            tileInfo[89] = "Tile1-WallLR.bmp";

            // ３行目
            tileInfo[90] = "Tile1-WallLR.bmp";
            tileInfo[104] = "Tile1-WallLR.bmp";
            tileInfo[108] = "Tile1-WallLR.bmp";
            tileInfo[111] = "Tile1-WallLR.bmp";
            tileInfo[114] = "Tile1-WallTL.bmp";
            tileInfo[115] = "Tile1-WallT.bmp";
            tileInfo[116] = "Tile1.bmp";
            tileInfo[117] = "Tile1-WallT.bmp";
            tileInfo[118] = "Tile1-WallT.bmp";
            tileInfo[119] = "Tile1-WallR.bmp";

            // ４行目
            tileInfo[120] = "Tile1-WallLR.bmp";
            tileInfo[134] = "Tile1-WallLR.bmp";
            tileInfo[136] = "Tile1-WallTL.bmp";
            tileInfo[137] = "Tile1-WallTB.bmp";
            tileInfo[138] = "Tile1-WallRB.bmp";
            tileInfo[141] = "Tile1-WallLR.bmp";
            tileInfo[144] = "Tile1-WallL.bmp";
            tileInfo[149] = "Tile1-WallR.bmp";

            // ５行目
            tileInfo[150] = "Tile1-WallLB.bmp";
            tileInfo[151] = "Tile1-WallTB.bmp";
            tileInfo[152] = "Tile1-WallTB.bmp";
            tileInfo[153] = "Tile1-WallTB.bmp";
            tileInfo[154] = "Tile1-WallTB.bmp";
            tileInfo[155] = "Tile1-WallTB.bmp";
            tileInfo[156] = "Tile1-WallT.bmp";
            tileInfo[157] = "Tile1-WallTB.bmp";
            tileInfo[158] = "Tile1-WallTB.bmp";
            tileInfo[159] = "Tile1-WallTB.bmp";
            tileInfo[160] = "Tile1-WallTB.bmp";
            tileInfo[161] = "Tile1-WallTB.bmp";
            tileInfo[162] = "Tile1-WallTRB.bmp";
            tileInfo[164] = "Tile1-WallLR.bmp";
            tileInfo[166] = "Tile1-WallLR.bmp";
            tileInfo[171] = "Tile1-WallLR.bmp";
            tileInfo[174] = "Tile1-WallL.bmp";
            tileInfo[179] = "Tile1-WallR.bmp";

            // ６行目
            tileInfo[180] = "Tile1-WallL.bmp";
            tileInfo[186] = "Tile1-WallLR.bmp";
            tileInfo[194] = "Tile1-WallLR.bmp";
            tileInfo[196] = "Tile1-WallLB.bmp";
            tileInfo[197] = "Tile1-WallTB.bmp";
            tileInfo[198] = "Tile1-WallTB.bmp";
            tileInfo[199] = "Tile1-WallTR.bmp";
            tileInfo[201] = "Tile1-WallLR.bmp";
            tileInfo[204] = "Tile1-WallLB.bmp";
            tileInfo[205] = "Tile1-WallB.bmp";
            tileInfo[206] = "Tile1-WallB.bmp";
            tileInfo[207] = "Tile1-WallB.bmp";
            tileInfo[208] = "Tile1-WallB.bmp";
            tileInfo[209] = "Tile1-WallRB.bmp";

            // ７行目
            tileInfo[210] = "Tile1-WallL.bmp";
            tileInfo[216] = "Tile1-WallLR.bmp";
            tileInfo[224] = "Tile1-WallLR.bmp";
            tileInfo[229] = "Tile1-WallLR.bmp";
            tileInfo[231] = "Tile1-WallLR.bmp";
            tileInfo[239] = "Tile1-WallR.bmp";

            // ８行目
            tileInfo[240] = "Tile1-WallL.bmp";
            tileInfo[246] = "Tile1-WallLR.bmp";
            tileInfo[254] = "Tile1-WallLR.bmp";
            tileInfo[259] = "Tile1-WallLR.bmp";
            tileInfo[261] = "Tile1-WallLR.bmp";
            tileInfo[269] = "Tile1-WallR.bmp";

            // ９行目
            tileInfo[270] = "Tile1-WallL.bmp";
            tileInfo[273] = "Tile1-WallTL.bmp";
            tileInfo[274] = "Tile1-WallT.bmp";
            tileInfo[275] = "Tile1-WallT.bmp";
            tileInfo[276] = "Tile1.bmp";
            tileInfo[277] = "Tile1-WallT.bmp";
            tileInfo[278] = "Tile1-WallT.bmp";
            tileInfo[279] = "Tile1-WallTR.bmp";
            tileInfo[284] = "Tile1-WallLR.bmp";
            tileInfo[286] = "Tile1-WallTL.bmp";
            tileInfo[287] = "Tile1-WallTB.bmp";
            tileInfo[288] = "Tile1-WallTB.bmp";
            tileInfo[289] = "Tile1-WallRB.bmp";
            tileInfo[291] = "Tile1-WallLR.bmp";
            tileInfo[299] = "Tile1-WallR.bmp";

            // １０行目
            tileInfo[300] = "Tile1-WallL.bmp";
            tileInfo[303] = "Tile1-WallL.bmp";
            tileInfo[309] = "Tile1-WallR.bmp";
            tileInfo[314] = "Tile1-WallLR.bmp";
            tileInfo[316] = "Tile1-WallLR.bmp";
            tileInfo[321] = "Tile1-WallLB.bmp";
            tileInfo[322] = "Tile1-WallTB.bmp";
            tileInfo[323] = "Tile1-WallTB.bmp";
            tileInfo[324] = "Tile1-WallTB.bmp";
            tileInfo[325] = "Tile1-WallTR.bmp";
            tileInfo[329] = "Tile1-WallR.bmp";

            // １１行目
            tileInfo[330] = "Tile1-WallL.bmp";
            tileInfo[333] = "Tile1-WallL.bmp";
            tileInfo[339] = "Tile1-WallR-DummyR.bmp";
            tileInfo[340] = "Tile1-WallTB.bmp";
            tileInfo[341] = "Tile1-WallTB.bmp";
            tileInfo[342] = "Tile1-WallTR.bmp";
            tileInfo[344] = "Tile1-WallLR.bmp";
            tileInfo[346] = "Tile1-WallLR.bmp";
            tileInfo[355] = "Tile1-WallLR.bmp";
            tileInfo[359] = "Tile1-WallR.bmp";

            // １２行目
            tileInfo[360] = "Tile1-WallL.bmp";
            tileInfo[361] = "Tile1-WallTL.bmp";
            tileInfo[362] = "Tile1-WallTB.bmp";
            tileInfo[363] = "Tile1-WallB.bmp";
            tileInfo[364] = "Tile1-WallB.bmp";
            tileInfo[365] = "Tile1-WallB.bmp";
            tileInfo[366] = "Tile1.bmp";
            tileInfo[367] = "Tile1-WallB.bmp";
            tileInfo[368] = "Tile1-WallB.bmp";
            tileInfo[369] = "Tile1-WallRB.bmp";
            tileInfo[372] = "Tile1-WallLR.bmp";
            tileInfo[374] = "Tile1-WallLR.bmp";
            tileInfo[376] = "Tile1-WallLB.bmp";
            tileInfo[377] = "Tile1-WallTB.bmp";
            tileInfo[378] = "Tile1-WallTR.bmp";
            tileInfo[381] = "Tile1-WallTL.bmp";
            tileInfo[382] = "Tile1-WallT.bmp";
            tileInfo[383] = "Tile1-WallT.bmp";
            tileInfo[384] = "Tile1-WallT.bmp";
            tileInfo[385] = "Tile1.bmp";
            tileInfo[386] = "Tile1-WallT.bmp";
            tileInfo[387] = "Tile1-WallT.bmp";
            tileInfo[388] = "Tile1-WallT.bmp";
            tileInfo[389] = "Tile1-WallTR.bmp";

            // １３行目
            tileInfo[390] = "Tile1-WallL.bmp";
            tileInfo[391] = "Tile1-WallLR.bmp";
            tileInfo[396] = "Tile1-WallLR.bmp";
            tileInfo[402] = "Tile1-WallLR.bmp";
            tileInfo[404] = "Tile1-WallLR.bmp";
            tileInfo[408] = "Tile1-WallLR.bmp";
            tileInfo[411] = "Tile1-WallL.bmp";
            tileInfo[419] = "Tile1-WallR.bmp";

            // １４行目
            tileInfo[420] = "Tile1-WallTL.bmp";
            tileInfo[421] = "Tile1-WallRB.bmp";
            tileInfo[426] = "Tile1-WallLR.bmp";
            tileInfo[432] = "Tile1-WallLR.bmp";
            tileInfo[434] = "Tile1-WallLR.bmp";
            tileInfo[438] = "Tile1-WallLR.bmp";
            tileInfo[441] = "Tile1-WallL.bmp";
            tileInfo[449] = "Tile1-WallR.bmp";

            // １５行目
            tileInfo[450] = "Tile1-WallLR.bmp";
            tileInfo[456] = "Tile1-WallLR.bmp";
            tileInfo[458] = "Tile1-WallTL.bmp";
            tileInfo[459] = "Tile1-WallT.bmp";
            tileInfo[460] = "Tile1-WallTR.bmp";
            tileInfo[462] = "Tile1-WallLR.bmp";
            tileInfo[464] = "Tile1-WallLR.bmp";
            tileInfo[468] = "Tile1-WallLR.bmp";
            tileInfo[471] = "Tile1-WallL.bmp";
            tileInfo[479] = "Tile1-WallR.bmp";

            // １６行目
            tileInfo[480] = "Tile1-WallLR.bmp";
            tileInfo[486] = "Tile1-WallLR.bmp";
            tileInfo[488] = "Tile1-WallL.bmp";
            tileInfo[491] = "Tile1-WallTB.bmp";
            tileInfo[492] = "Tile1-WallRB.bmp";
            tileInfo[494] = "Tile1-WallLR.bmp";
            tileInfo[498] = "Tile1-WallLR.bmp";
            tileInfo[501] = "Tile1-WallL.bmp";
            tileInfo[509] = "Tile1-WallR.bmp";

            // １７行目
            tileInfo[510] = "Tile1-WallLR.bmp";
            tileInfo[513] = "Tile1-WallTL.bmp";
            tileInfo[514] = "Tile1-WallTB.bmp";
            tileInfo[515] = "Tile1-WallTB.bmp";
            tileInfo[516] = "Tile1-WallRB.bmp";
            tileInfo[518] = "Tile1-WallLB.bmp";
            tileInfo[519] = "Tile1-WallB.bmp";
            tileInfo[520] = "Tile1-WallRB.bmp";
            tileInfo[524] = "Tile1-WallLR.bmp";
            tileInfo[528] = "Tile1-WallLRB.bmp";
            tileInfo[531] = "Tile1-WallL.bmp";
            tileInfo[539] = "Tile1-WallR.bmp";

            // １８行目
            tileInfo[540] = "Tile1-WallLR.bmp";
            tileInfo[543] = "Tile1-WallLR.bmp";
            tileInfo[554] = "Tile1-WallLR.bmp";
            tileInfo[561] = "Tile1-WallLB.bmp";
            tileInfo[562] = "Tile1.bmp";
            tileInfo[563] = "Tile1-WallB.bmp";
            tileInfo[564] = "Tile1-WallB.bmp";
            tileInfo[565] = "Tile1-WallB.bmp";
            tileInfo[566] = "Tile1-WallB.bmp";
            tileInfo[567] = "Tile1-WallB.bmp";
            tileInfo[568] = "Tile1-WallB.bmp";
            tileInfo[569] = "Tile1-WallRB.bmp";

            // １９行目
            tileInfo[570] = "Tile1-WallLB.bmp";
            tileInfo[571] = "Tile1-WallTRB.bmp";
            tileInfo[572] = "Tile1-WallB.bmp";
            tileInfo[573] = "Tile1-WallLB.bmp";
            tileInfo[574] = "Tile1-WallTB.bmp";
            tileInfo[575] = "Tile1-WallTB.bmp";
            tileInfo[576] = "Tile1-WallTB.bmp";
            tileInfo[577] = "Tile1-WallTB.bmp";
            tileInfo[578] = "Tile1-WallTB.bmp";
            tileInfo[579] = "Tile1-WallTB.bmp";
            tileInfo[580] = "Tile1-WallTB.bmp";
            tileInfo[581] = "Tile1-WallTB.bmp";
            tileInfo[582] = "Tile1-WallTB.bmp";
            tileInfo[583] = "Tile1-WallTB.bmp";
            tileInfo[584] = "Tile1-WallRB.bmp";
            tileInfo[585] = "Tile1-WallB.bmp";
            tileInfo[586] = "Tile1-WallB.bmp";
            tileInfo[587] = "Tile1-WallB.bmp";
            tileInfo[588] = "Tile1-WallB.bmp";
            tileInfo[589] = "Tile1-WallB.bmp";
            tileInfo[590] = "Tile1-WallB.bmp";
            tileInfo[591] = "Tile1-WallB.bmp";
            tileInfo[592] = "Tile1-WallLB.bmp";
            tileInfo[593] = "Tile1-WallTB.bmp";
            tileInfo[594] = "Tile1-WallTB.bmp";
            tileInfo[595] = "Tile1-WallTB.bmp";
            tileInfo[596] = "Tile1-WallTB.bmp";
            tileInfo[597] = "Tile1-WallTB.bmp";
            tileInfo[598] = "Tile1-WallTB.bmp";
            tileInfo[599] = "Downstair-WallTRB.bmp";

            #endregion
            #region "ダンジョン２階"
            // ０行目
            tileInfo2[0] = "Tile1-WallTL.bmp";
            tileInfo2[1] = "Tile1-WallTL.bmp";
            tileInfo2[2] = "Tile1-WallT.bmp";
            tileInfo2[3] = "Tile1-WallT.bmp";
            tileInfo2[4] = "Tile1-WallTR.bmp";
            tileInfo2[5] = "Tile1-WallT.bmp";
            tileInfo2[6] = "Tile1-WallTL.bmp";
            tileInfo2[7] = "Tile1-WallT.bmp";
            tileInfo2[8] = "Tile1-WallT.bmp";
            tileInfo2[9] = "Tile1-WallTR.bmp";
            tileInfo2[10] = "Tile1-WallT.bmp";
            tileInfo2[11] = "Tile1-WallTL.bmp";
            tileInfo2[12] = "Tile1-WallT.bmp";
            tileInfo2[13] = "Tile1-WallT.bmp";
            tileInfo2[14] = "Tile1-WallTR.bmp";
            tileInfo2[15] = "Tile1-WallT.bmp";
            tileInfo2[16] = "Tile1-WallTL.bmp";
            tileInfo2[17] = "Tile1-WallT.bmp";
            tileInfo2[18] = "Tile1-WallT.bmp";
            tileInfo2[19] = "Tile1-WallTR.bmp";
            tileInfo2[20] = "Tile1-WallT.bmp";
            tileInfo2[21] = "Tile1-WallTL.bmp";
            tileInfo2[22] = "Tile1-WallT.bmp";
            tileInfo2[23] = "Downstair-WallT.bmp";
            tileInfo2[24] = "Tile1-WallTR.bmp";
            tileInfo2[25] = "Tile1-WallT.bmp";
            tileInfo2[26] = "Tile1-WallTL.bmp";
            tileInfo2[27] = "Tile1-WallT.bmp";
            tileInfo2[28] = "Tile1-WallT.bmp";
            tileInfo2[29] = "Tile1-WallTR.bmp";

            // １行目
            tileInfo2[30] = "Tile1-WallL.bmp";
            tileInfo2[31] = "Tile1-WallLB.bmp";
            tileInfo2[32] = "Tile1-WallB.bmp";
            tileInfo2[34] = "Tile1-WallRB.bmp";
            tileInfo2[36] = "Tile1-WallL.bmp";
            tileInfo2[37] = "Tile1-WallB.bmp";
            tileInfo2[38] = "Tile1-WallB.bmp";
            tileInfo2[39] = "Tile1-WallRB.bmp";
            tileInfo2[41] = "Tile1-WallLB.bmp";
            tileInfo2[43] = "Tile1-WallB.bmp";
            tileInfo2[44] = "Tile1-WallRB.bmp";
            tileInfo2[46] = "Tile1-WallL.bmp";
            tileInfo2[47] = "Tile1-WallB.bmp";
            tileInfo2[48] = "Tile1-WallB.bmp";
            tileInfo2[49] = "Tile1-WallRB.bmp";
            tileInfo2[51] = "Tile1-WallLB.bmp";
            tileInfo2[53] = "Tile1-WallB.bmp";
            tileInfo2[54] = "Tile1-WallRB.bmp";
            tileInfo2[56] = "Tile1-WallL.bmp";
            tileInfo2[57] = "Tile1-WallB.bmp";
            tileInfo2[58] = "Tile1-WallB.bmp";
            tileInfo2[59] = "Tile1-WallRB.bmp";

            // ２行目
            tileInfo2[60] = "Tile1-WallL.bmp";
            tileInfo2[63] = "Tile1-WallLR.bmp";
            tileInfo2[66] = "Tile1-WallLR.bmp";
            tileInfo2[72] = "Tile1-WallLR.bmp";
            tileInfo2[76] = "Tile1-WallLR.bmp";
            tileInfo2[82] = "Tile1-WallLR.bmp";
            tileInfo2[86] = "Tile1-WallLR.bmp";
            tileInfo2[89] = "Tile1-WallR.bmp";

            // ３行目
            tileInfo2[90] = "Tile1-WallL.bmp";
            tileInfo2[93] = "Tile1-WallLR.bmp";
            tileInfo2[96] = "Tile1-WallLR.bmp";
            tileInfo2[102] = "Tile1-WallLR.bmp";
            tileInfo2[106] = "Tile1-WallLR.bmp";
            tileInfo2[112] = "Tile1-WallLR.bmp";
            tileInfo2[116] = "Tile1-WallLR.bmp";
            tileInfo2[119] = "Tile1-WallR.bmp";

            // ４行目
            tileInfo2[120] = "Tile1-WallL.bmp";
            tileInfo2[123] = "Tile1-WallLR.bmp";
            tileInfo2[126] = "Tile1-WallLR.bmp";
            tileInfo2[132] = "Tile1-WallLR.bmp";
            tileInfo2[136] = "Tile1-WallLR.bmp";
            tileInfo2[142] = "Tile1-WallLR.bmp";
            tileInfo2[146] = "Tile1-WallLR.bmp";
            tileInfo2[149] = "Tile1-WallR.bmp";

            // ５行目
            tileInfo2[150] = "Tile1-WallL.bmp";
            tileInfo2[151] = "Tile1-WallTL.bmp";
            tileInfo2[152] = "Tile1-WallTB.bmp";
            if (we.SolveArea24 && we.CompleteArea24)
            {
                tileInfo2[153] = "Tile1.bmp";
            }
            else
            {
                if (we.FailArea242)
                {
                    tileInfo2[153] = "Tile1-WallT-Num4.bmp";
                }
                else
                {
                    tileInfo2[153] = "Tile1-WallT.bmp";
                }
            }
            tileInfo2[154] = "Tile1-WallTB.bmp";
            if (we.FailArea242 && !we.CompleteArea24)
            {
                tileInfo2[155] = "Tile1-WallT-Num1.bmp";
            }
            else
            {
                tileInfo2[155] = "Tile1-WallT.bmp";
            }
            if (we.SolveArea24 && we.CompleteArea24)
            {
                tileInfo2[156] = "Tile1-WallB.bmp";
            }
            else
            {
                tileInfo2[156] = "Tile1-WallTB.bmp";
            }
            tileInfo2[157] = "Tile1-WallTR.bmp";
            tileInfo2[160] = "Tile1-WallTL.bmp";
            tileInfo2[161] = "Tile1-WallT.bmp";
            if (we.SolveArea23 && we.CompleteArea23)
            {
                tileInfo2[162] = "Tile1.bmp";
            }
            else
            {
                tileInfo2[162] = "Tile1-WallT.bmp";
            }
            tileInfo2[163] = "Tile1-WallT.bmp";
            tileInfo2[164] = "Tile1-WallT.bmp";
            tileInfo2[165] = "Tile1-WallT.bmp";
            if (we.SolveArea23 && we.CompleteArea23)
            {
                tileInfo2[166] = "Tile1-WallR.bmp";
            }
            else
            {
                tileInfo2[166] = "Tile1-WallTR.bmp";
            }
            tileInfo2[169] = "Tile1-WallTL.bmp";
            tileInfo2[170] = "Tile1-WallT.bmp";
            tileInfo2[171] = "Tile1-WallT.bmp";
            if (we.SolveArea26 && we.CompleteArea26)
            {
                tileInfo2[172] = "Tile1.bmp";
            }
            else
            {
                tileInfo2[172] = "Tile1-WallT.bmp";
            }
            tileInfo2[173] = "Tile1-WallT.bmp";
            tileInfo2[174] = "Tile1-WallT.bmp";
            tileInfo2[175] = "Tile1-WallTR.bmp";
            tileInfo2[176] = "Tile1-WallLR.bmp";
            tileInfo2[179] = "Tile1-WallR.bmp";

            // ６行目
            tileInfo2[180] = "Tile1-WallL.bmp";
            tileInfo2[181] = "Tile1-WallLR.bmp";
            tileInfo2[183] = "Tile1-WallLR.bmp";
            tileInfo2[185] = "Tile1-WallLR.bmp";
            tileInfo2[187] = "Tile1-WallLR.bmp";
            tileInfo2[190] = "Tile1-WallL.bmp";
            tileInfo2[196] = "Tile1-WallR.bmp";
            tileInfo2[199] = "Tile1-WallL.bmp";
            tileInfo2[205] = "Tile1-WallR.bmp";
            tileInfo2[206] = "Tile1-WallLR.bmp";
            tileInfo2[209] = "Tile1-WallR.bmp";

            // ７行目
            tileInfo2[210] = "Tile1-WallL.bmp";
            if (we.FailArea242 && !we.CompleteArea24)
            {
                tileInfo2[211] = "Tile1-WallL-Num5.bmp";
            }
            else
            {
                tileInfo2[211] = "Tile1-WallL.bmp";
            }
            tileInfo2[212] = "Tile1-WallTB.bmp";
            tileInfo2[213] = "Tile1.bmp";
            tileInfo2[214] = "Tile1-WallTB.bmp";
            if (we.FailArea242 && !we.CompleteArea24)
            {
                tileInfo2[215] = "Tile1-Num3.bmp";
            }
            else
            {
                tileInfo2[215] = "Tile1.bmp";
            }
            tileInfo2[216] = "Tile1-WallTB.bmp";
            tileInfo2[217] = "Tile1.bmp";
            tileInfo2[218] = "Tile1-WallTB.bmp";
            tileInfo2[219] = "Tile1-WallTB.bmp";
            if (we.SolveArea23 && we.CompleteArea23)
            {
                tileInfo2[220] = "Tile1.bmp";
            }
            else
            {
                tileInfo2[220] = "Tile1-WallL.bmp";
            }
            if (we.SolveArea23 && we.CompleteArea23)
            {
                tileInfo2[226] = "Tile1.bmp";
            }
            else
            {
                tileInfo2[226] = "Tile1-WallR.bmp";
            }
            tileInfo2[227] = "Tile1-WallTB.bmp";
            if (we.SolveArea25 && we.CompleteArea25)
            {
                tileInfo2[228] = "Tile1-WallTB.bmp";
            }
            else
            {
                tileInfo2[228] = "Tile1-WallTRB.bmp";
            }
            if (we.SolveArea26 && we.CompleteArea26)
            {
                tileInfo2[235] = "Tile1.bmp";
            }
            else
            {
                tileInfo2[235] = "Tile1-WallR.bmp";
            }
            tileInfo2[236] = "Tile1-WallRB.bmp";
            tileInfo2[239] = "Tile1-WallR.bmp";

            // ８行目
            tileInfo2[240] = "Tile1-WallL.bmp";
            tileInfo2[241] = "Tile1-WallLR.bmp";
            tileInfo2[243] = "Tile1-WallLR.bmp";
            tileInfo2[245] = "Tile1-WallLR.bmp";
            tileInfo2[247] = "Tile1-WallLR.bmp";
            tileInfo2[250] = "Tile1-WallL.bmp";
            tileInfo2[256] = "Tile1-WallR.bmp";
            tileInfo2[259] = "Tile1-WallL.bmp";
            tileInfo2[265] = "Tile1-WallR.bmp";
            tileInfo2[269] = "Tile1-WallR.bmp";

            // ９行目
            tileInfo2[270] = "Tile1-WallL.bmp";
            tileInfo2[271] = "Tile1-WallLB.bmp";
            tileInfo2[272] = "Tile1-WallTB.bmp";

            if (we.FailArea242 && !we.CompleteArea24)
            {
                tileInfo2[273] = "Tile1-WallB-Num6.bmp";
            }
            else
            {
                tileInfo2[273] = "Tile1-WallB.bmp";
            }

            if (we.SolveArea24 && we.CompleteArea24)
            {
                tileInfo2[274] = "Tile1-WallT.bmp";
            }
            else
            {
                tileInfo2[274] = "Tile1-WallTB.bmp";
            }
            tileInfo2[275] = "Tile1-WallB.bmp";
            tileInfo2[276] = "Tile1-WallTB.bmp";
            if (we.FailArea242 && !we.CompleteArea24)
            {
                tileInfo2[277] = "Tile1-WallRB-Num2.bmp";
            }
            else
            {
                tileInfo2[277] = "Tile1-WallRB.bmp";
            }
            
            tileInfo2[280] = "Tile1-WallLB.bmp";
            tileInfo2[281] = "Tile1-WallB.bmp";
            tileInfo2[282] = "Tile1-WallB.bmp";
            tileInfo2[283] = "Tile1.bmp";
            tileInfo2[284] = "Tile1-WallB.bmp";
            tileInfo2[285] = "Tile1-WallB.bmp";
            tileInfo2[286] = "Tile1-WallRB.bmp";
            tileInfo2[289] = "Tile1-WallLB.bmp";
            tileInfo2[290] = "Tile1-WallB.bmp";
            tileInfo2[291] = "Tile1-WallB.bmp";
            if (we.SolveArea26 && we.CompleteArea26)
            {
                tileInfo2[292] = "Tile1.bmp";
            }
            else
            {
                tileInfo2[292] = "Tile1-WallB.bmp";
            }
            tileInfo2[293] = "Tile1-WallB.bmp";
            tileInfo2[294] = "Tile1-WallB.bmp";
            tileInfo2[295] = "Tile1-WallRB.bmp";
            tileInfo2[299] = "Tile1-WallR.bmp";

            // １０行目
            tileInfo2[300] = "Tile1-WallL.bmp";
            tileInfo2[304] = "Tile1-WallLR.bmp";
            tileInfo2[313] = "Tile1-WallLR.bmp";
            if (we.SolveArea26 && we.CompleteArea26)
            {
                tileInfo2[322] = "Tile1-WallLR.bmp";
            }
            else
            {
                tileInfo2[322] = "Tile1-WallTLR.bmp";
            }
            tileInfo2[329] = "Tile1-WallR.bmp";

            // １１行目
            tileInfo2[330] = "Tile1-WallL.bmp";
            tileInfo2[334] = "Tile1-WallLR.bmp";
            tileInfo2[343] = "Tile1-WallLR.bmp";
            tileInfo2[352] = "Tile1-WallLR.bmp";
            tileInfo2[359] = "Tile1-WallR.bmp";

            // １２行目
            tileInfo2[360] = "Tile1-WallL.bmp";
            tileInfo2[361] = "Tile1-WallTL.bmp";
            tileInfo2[362] = "Tile1-WallT.bmp";
            tileInfo2[363] = "Tile1-WallT.bmp";
            tileInfo2[364] = "Tile1.bmp";
            tileInfo2[365] = "Tile1-WallT.bmp";
            tileInfo2[366] = "Tile1-WallT.bmp";
            tileInfo2[367] = "Tile1-WallTR.bmp";
            tileInfo2[370] = "Tile1-WallTL.bmp";
            tileInfo2[371] = "Tile1-WallT.bmp";
            tileInfo2[372] = "Tile1-WallT.bmp";
            if (we.SolveArea22 && we.CompleteArea22)
            {
                tileInfo2[373] = "Tile1.bmp";
            }
            else
            {
                tileInfo2[373] = "Tile1-WallT.bmp";
            }
            tileInfo2[374] = "Tile1-WallT.bmp";
            tileInfo2[375] = "Tile1-WallT.bmp";
            tileInfo2[376] = "Tile1-WallTR.bmp";
            tileInfo2[379] = "Tile1-WallTL.bmp";
            tileInfo2[380] = "Tile1-WallT.bmp";
            tileInfo2[381] = "Tile1-WallT.bmp";
            if (we.SolveArea21 && we.CompleteArea21)
            {
                tileInfo2[382] = "Tile1.bmp";
            }
            else
            {
                tileInfo2[382] = "Tile1-WallT.bmp";
            }
            tileInfo2[383] = "Tile1-WallT.bmp";
            tileInfo2[384] = "Tile1-WallT.bmp";
            tileInfo2[385] = "Tile1-WallTR.bmp";
            tileInfo2[389] = "Tile1-WallR.bmp";

            // １３行目
            tileInfo2[390] = "Tile1-WallL.bmp";
            tileInfo2[391] = "Tile1-WallL.bmp";
            tileInfo2[397] = "Tile1-WallR.bmp";
            tileInfo2[400] = "Tile1-WallL.bmp";
            tileInfo2[406] = "Tile1-WallR.bmp";
            tileInfo2[409] = "Tile1-WallL.bmp";
            tileInfo2[415] = "Tile1-WallR.bmp";
            tileInfo2[419] = "Tile1-WallR.bmp";

            // １４行目
            tileInfo2[420] = "Tile1-WallL.bmp";
            tileInfo2[421] = "Tile1-WallL.bmp";
            if (we.SolveArea26 && we.CompleteArea26)
            {
                tileInfo2[427] = "Tile1.bmp";
            }
            else
            {
                tileInfo2[427] = "Tile1-WallR.bmp";
            }
            if (we.SolveArea26 && we.CompleteArea26)
            {
                tileInfo2[428] = "Tile1-WallTB.bmp";
            }
            else
            {
                tileInfo2[428] = "Tile1-WallTLB.bmp";
            }
            tileInfo2[429] = "Tile1-WallTB.bmp";
            if (we.SolveArea22 && we.CompleteArea22)
            {
                tileInfo2[430] = "Tile1.bmp";
            }
            else
            {
                tileInfo2[430] = "Tile1-WallL.bmp";
            }
            tileInfo2[437] = "Tile1-WallTB.bmp";
            tileInfo2[438] = "Tile1-WallTB.bmp";
            if (we.SolveArea21 && we.CompleteArea21)
            {
                tileInfo2[439] = "Tile1.bmp";
            }
            else
            {
                tileInfo2[439] = "Tile1-WallL.bmp";
            }
            tileInfo2[446] = "Tile1-WallTB.bmp";
            tileInfo2[447] = "Tile1-WallTB.bmp";
            tileInfo2[448] = "Tile1-WallTB.bmp";
            tileInfo2[449] = "Tile1-WallTR.bmp";

            // １５行目
            tileInfo2[450] = "Tile1-WallL.bmp";
            tileInfo2[451] = "Tile1-WallL.bmp";
            tileInfo2[457] = "Tile1-WallR.bmp";
            tileInfo2[460] = "Tile1-WallL.bmp";
            tileInfo2[466] = "Tile1-WallR.bmp";
            tileInfo2[469] = "Tile1-WallL.bmp";
            tileInfo2[475] = "Tile1-WallR.bmp";
            tileInfo2[479] = "Tile1-WallLR.bmp";

            // １６行目
            tileInfo2[480] = "Tile1-WallL.bmp";
            tileInfo2[481] = "Tile1-WallLB.bmp";
            tileInfo2[482] = "Tile1-WallB.bmp";
            tileInfo2[483] = "Tile1-WallB.bmp";
            tileInfo2[484] = "Tile1-WallB.bmp";
            tileInfo2[485] = "Tile1-WallB.bmp";
            tileInfo2[486] = "Tile1-WallB-DummyB.bmp";
            tileInfo2[487] = "Tile1-WallRB.bmp";
            tileInfo2[490] = "Tile1-WallLB.bmp";
            tileInfo2[491] = "Tile1-WallB.bmp";
            tileInfo2[492] = "Tile1-WallB.bmp";
            tileInfo2[493] = "Tile1-WallB.bmp";
            tileInfo2[494] = "Tile1-WallB.bmp";
            tileInfo2[495] = "Tile1-WallB.bmp";
            tileInfo2[496] = "Tile1-WallRB.bmp";
            tileInfo2[499] = "Tile1-WallLB.bmp";
            tileInfo2[500] = "Tile1-WallB.bmp";
            tileInfo2[501] = "Tile1-WallB.bmp";
            tileInfo2[502] = "Tile1-WallB.bmp";
            tileInfo2[503] = "Tile1-WallB.bmp";
            tileInfo2[504] = "Tile1-WallB.bmp";
            tileInfo2[505] = "Tile1-WallRB.bmp";
            tileInfo2[509] = "Tile1-WallLR.bmp";

            // １７行目
            tileInfo2[510] = "Tile1-WallL.bmp";
            tileInfo2[516] = "Tile1-WallLB.bmp";
            tileInfo2[517] = "Tile1-WallTB.bmp";
            tileInfo2[518] = "Tile1-WallTB.bmp";
            tileInfo2[519] = "Tile1-WallTB.bmp";
            tileInfo2[520] = "Tile1-WallTB.bmp";
            tileInfo2[521] = "Tile1-WallTB.bmp";
            tileInfo2[522] = "Tile1-WallTB.bmp";
            tileInfo2[523] = "Tile1-WallTB.bmp";
            tileInfo2[524] = "Tile1-WallTB.bmp";
            tileInfo2[525] = "Tile1-WallTB.bmp";
            tileInfo2[526] = "Tile1-WallTB.bmp";
            tileInfo2[527] = "Tile1-WallTB.bmp";
            tileInfo2[528] = "Tile1-WallTB.bmp";
            tileInfo2[529] = "Tile1-WallTB.bmp";
            tileInfo2[530] = "Tile1-WallTB.bmp";
            tileInfo2[531] = "Tile1-WallTB.bmp";
            tileInfo2[532] = "Tile1-WallTB.bmp";
            tileInfo2[533] = "Tile1-WallTB.bmp";
            tileInfo2[534] = "Tile1-WallTB.bmp";
            tileInfo2[535] = "Tile1-WallTB.bmp";
            tileInfo2[536] = "Tile1-WallTB.bmp";
            tileInfo2[537] = "Tile1-WallTRB.bmp";
            tileInfo2[539] = "Tile1-WallLR.bmp";

            // １８行目
            tileInfo2[540] = "Tile1-WallL.bmp";
            tileInfo2[541] = "Tile1-WallTLR.bmp";
            tileInfo2[569] = "Tile1-WallLR.bmp";

            // １９行目
            tileInfo2[570] = "Tile1-WallLRB.bmp";
            tileInfo2[571] = "Tile1-WallLB.bmp";
            tileInfo2[572] = "Tile1-WallTB.bmp";
            tileInfo2[573] = "Tile1-WallTB.bmp";
            tileInfo2[574] = "Tile1-WallTB.bmp";
            tileInfo2[575] = "Tile1-WallTB.bmp";
            tileInfo2[576] = "Tile1-WallTB.bmp";
            tileInfo2[577] = "Tile1-WallTB.bmp";
            tileInfo2[578] = "Tile1-WallTB.bmp";
            tileInfo2[579] = "Tile1-WallTB.bmp";
            tileInfo2[580] = "Tile1-WallTB.bmp";
            tileInfo2[581] = "Tile1-WallTB.bmp";
            tileInfo2[582] = "Tile1-WallTB.bmp";
            tileInfo2[583] = "Tile1-WallTB.bmp";
            tileInfo2[584] = "Tile1-WallTB.bmp";
            tileInfo2[585] = "Tile1-WallTB.bmp";
            tileInfo2[586] = "Tile1-WallTB.bmp";
            tileInfo2[587] = "Tile1-WallTB.bmp";
            tileInfo2[588] = "Tile1-WallTB.bmp";
            tileInfo2[589] = "Tile1-WallTB.bmp";
            tileInfo2[590] = "Tile1-WallTB.bmp";
            tileInfo2[591] = "Tile1-WallTB.bmp";
            tileInfo2[592] = "Tile1-WallTB.bmp";
            tileInfo2[593] = "Tile1-WallTB.bmp";
            tileInfo2[594] = "Tile1-WallTB.bmp";
            tileInfo2[595] = "Tile1-WallTB.bmp";
            tileInfo2[596] = "Tile1-WallTB.bmp";
            tileInfo2[597] = "Tile1-WallTB.bmp";
            tileInfo2[598] = "Tile1-WallTB.bmp";
            tileInfo2[599] = "Upstair-WallRB.bmp";
            #endregion
            #region "ダンジョン３階"
            // ０行目
            tileInfo3[0] = "Tile1-WallTL.bmp";
            tileInfo3[1] = "Tile1-WallTB.bmp";
            tileInfo3[2] = "Tile1-WallTB.bmp";
            tileInfo3[3] = "Tile1-WallTB.bmp";
            tileInfo3[4] = "Tile1-WallTB.bmp";
            tileInfo3[5] = "Tile1-WallTB.bmp";
            tileInfo3[6] = "Tile1-WallTB.bmp";
            tileInfo3[7] = "Tile1-WallTB.bmp";
            tileInfo3[8] = "Tile1-WallTB.bmp";
            tileInfo3[9] = "Tile1-WallTB.bmp";
            tileInfo3[10] = "Tile1-WallTR.bmp";
            tileInfo3[23] = "Upstair-WallTLR.bmp";

            // １行目
            tileInfo3[30] = "Tile1-WallLR.bmp";
            tileInfo3[31] = "Tile1-WallTL.bmp";
            tileInfo3[32] = "Tile1-WallTB.bmp";
            tileInfo3[33] = "Tile1-WallTB.bmp";
            tileInfo3[34] = "Tile1-WallTB.bmp";
            tileInfo3[35] = "Tile1-WallTB.bmp";
            tileInfo3[36] = "Tile1-WallTR.bmp";
            tileInfo3[37] = "Tile1-WallTL.bmp";
            tileInfo3[38] = "Tile1-WallTR.bmp";
            tileInfo3[39] = "Tile1-WallTLR.bmp";
            tileInfo3[40] = "Tile1-WallLB.bmp";
            tileInfo3[41] = "Tile1-WallT.bmp";
            tileInfo3[42] = "Tile1-WallTB.bmp";
            tileInfo3[43] = "Tile1-WallTB.bmp";
            tileInfo3[44] = "Tile1-WallTB.bmp";
            tileInfo3[45] = "Tile1-WallTB.bmp";
            tileInfo3[46] = "Tile1-WallTB.bmp";
            tileInfo3[47] = "Tile1-WallTB.bmp";
            tileInfo3[48] = "Tile1-WallT.bmp";
            tileInfo3[49] = "Tile1-WallTB.bmp";
            tileInfo3[50] = "Tile1-WallTB.bmp";
            tileInfo3[51] = "Tile1-WallT.bmp";
            tileInfo3[52] = "Tile1-WallTB.bmp";
            tileInfo3[53] = "Tile1-WallB.bmp";
            tileInfo3[54] = "Tile1-WallT.bmp";
            tileInfo3[55] = "Tile1-WallTB.bmp";
            tileInfo3[56] = "Tile1-WallTB.bmp";
            tileInfo3[57] = "Tile1-WallTB.bmp";
            tileInfo3[58] = "Tile1-WallTB.bmp";
            tileInfo3[59] = "Tile1-WallTR.bmp";

            // ２行目
            tileInfo3[60] = "Tile1-WallLR.bmp";
            tileInfo3[61] = "Tile1-WallLR.bmp";
            tileInfo3[62] = "Tile1-WallTL.bmp";
            tileInfo3[63] = "Tile1-WallT.bmp";
            tileInfo3[64] = "Tile1-WallT.bmp";
            tileInfo3[65] = "Tile1-WallTR.bmp";
            tileInfo3[66] = "Tile1-WallLR.bmp";
            tileInfo3[67] = "Tile1-WallL.bmp";
            tileInfo3[68] = "Tile1-WallR.bmp";
            tileInfo3[69] = "Tile1-WallLR.bmp";
            tileInfo3[70] = "Tile1-WallTLR.bmp";
            tileInfo3[71] = "Tile1-WallLR.bmp";
            tileInfo3[72] = "Tile1-WallTLB.bmp";
            tileInfo3[73] = "Tile1-WallTB.bmp";
            tileInfo3[74] = "Tile1-WallTB.bmp";
            tileInfo3[75] = "Tile1-WallTB.bmp";
            tileInfo3[76] = "Tile1-WallTB.bmp";
            tileInfo3[77] = "Tile1-WallTRB.bmp";
            tileInfo3[78] = "Tile1-WallLR.bmp";
            tileInfo3[79] = "Tile1-WallTL.bmp";
            tileInfo3[80] = "Tile1-WallTR.bmp";
            tileInfo3[81] = "Tile1-WallLR.bmp";
            tileInfo3[82] = "Tile1-WallTL.bmp";
            tileInfo3[83] = "Tile1-WallTR.bmp";
            tileInfo3[84] = "Tile1-WallLR.bmp";
            tileInfo3[85] = "Tile1-WallTL.bmp";
            tileInfo3[86] = "Tile1-WallT.bmp";
            tileInfo3[87] = "Tile1-WallT.bmp";
            tileInfo3[88] = "Tile1-WallTR.bmp";
            tileInfo3[89] = "Tile1-WallLR.bmp";

            // ３行目
            tileInfo3[90] = "Tile1-WallLR.bmp";
            tileInfo3[91] = "Tile1-WallLRB.bmp";
            tileInfo3[92] = "Tile1-WallL.bmp";
            tileInfo3[93] = "Tile1.bmp";
            tileInfo3[94] = "Tile1.bmp";
            tileInfo3[95] = "Tile1-WallR.bmp";
            tileInfo3[96] = "Tile1-WallLR.bmp";
            tileInfo3[97] = "Tile1-WallLB.bmp";
            tileInfo3[98] = "Tile1-WallRB.bmp";
            tileInfo3[99] = "Tile1-WallLR.bmp";
            tileInfo3[100] = "Tile1-WallLR.bmp";
            if (!we.CompleteArea32)
            {
                tileInfo3[101] = "Tile1-WallLB.bmp";
            }
            else
            {
                tileInfo3[101] = "Tile1-WallL.bmp";
            }
            tileInfo3[102] = "Tile1-WallTB.bmp";
            tileInfo3[103] = "Tile1-WallT.bmp";
            tileInfo3[104] = "Tile1-WallTB.bmp";
            tileInfo3[105] = "Tile1-WallTB.bmp";
            tileInfo3[106] = "Tile1-WallTB.bmp";
            tileInfo3[107] = "Tile1-WallTB.bmp";
            tileInfo3[108] = "Tile1-WallR.bmp";
            tileInfo3[109] = "Tile1-WallL.bmp";
            tileInfo3[110] = "Tile1-WallR.bmp";
            tileInfo3[111] = "Tile1-WallLR.bmp";
            tileInfo3[112] = "Tile1-WallL.bmp";
            tileInfo3[113] = "Tile1-WallR.bmp";
            tileInfo3[114] = "Tile1-WallLR.bmp";
            tileInfo3[115] = "Tile1-WallL.bmp";
            tileInfo3[116] = "Tile1.bmp";
            tileInfo3[117] = "Tile1.bmp";
            tileInfo3[118] = "Tile1-WallR.bmp";
            tileInfo3[119] = "Tile1-WallLR.bmp";

            // ４行目
            tileInfo3[120] = "Tile1-WallLR.bmp";
            tileInfo3[121] = "Tile1-WallTLR.bmp";
            tileInfo3[122] = "Tile1-WallL.bmp";
            tileInfo3[123] = "Tile1.bmp";
            tileInfo3[124] = "Tile1.bmp";
            tileInfo3[125] = "Tile1-WallR.bmp";
            tileInfo3[126] = "Tile1-WallL.bmp";
            tileInfo3[127] = "Tile1-WallTB.bmp";
            tileInfo3[128] = "Tile1-WallTB.bmp";
            tileInfo3[129] = "Tile1-WallR.bmp";
            tileInfo3[130] = "Tile1-WallLR.bmp";
            if (!we.CompleteArea32)
            {
                tileInfo3[131] = "Tile1-WallTLR.bmp";
            }
            else
            {
                tileInfo3[131] = "Tile1-WallLR.bmp";
            }
            tileInfo3[132] = "Tile1-WallTLR.bmp";
            tileInfo3[133] = "Tile1-WallLR.bmp";
            tileInfo3[134] = "Tile1-WallTL.bmp";
            tileInfo3[135] = "Tile1-WallT.bmp";
            tileInfo3[136] = "Tile1-WallT.bmp";
            tileInfo3[137] = "Tile1-WallTR.bmp";
            tileInfo3[138] = "Tile1-WallLR.bmp";
            tileInfo3[139] = "Tile1-WallL.bmp";
            tileInfo3[140] = "Tile1-WallR.bmp";
            tileInfo3[141] = "Tile1-WallLR.bmp";
            tileInfo3[142] = "Tile1-WallL.bmp";
            tileInfo3[143] = "Tile1-WallR.bmp";
            tileInfo3[144] = "Tile1-WallLR.bmp";
            tileInfo3[145] = "Tile1-WallL.bmp";
            tileInfo3[146] = "Tile1.bmp";
            tileInfo3[147] = "Tile1.bmp";
            tileInfo3[148] = "Tile1-WallR.bmp";
            tileInfo3[149] = "Tile1-WallLR.bmp";

            // ５行目
            tileInfo3[150] = "Tile1-WallLR.bmp";
            tileInfo3[151] = "Tile1-WallLR.bmp";
            tileInfo3[152] = "Tile1-WallLB.bmp";
            tileInfo3[153] = "Tile1-WallB.bmp";
            tileInfo3[154] = "Tile1-WallB.bmp";
            tileInfo3[155] = "Tile1-WallRB.bmp";
            tileInfo3[156] = "Tile1-WallLR.bmp";
            tileInfo3[157] = "Tile1-WallTL.bmp";
            tileInfo3[158] = "Tile1-WallTR.bmp";
            tileInfo3[159] = "Tile1-WallLR.bmp";
            tileInfo3[160] = "Tile1-WallLR.bmp";
            tileInfo3[161] = "Tile1-WallLR.bmp";
            tileInfo3[162] = "Tile1-WallLR.bmp";
            tileInfo3[163] = "Tile1-WallLR.bmp";
            tileInfo3[164] = "Tile1-WallL.bmp";
            tileInfo3[165] = "Tile1.bmp";
            tileInfo3[166] = "Tile1.bmp";
            tileInfo3[167] = "Tile1-WallR.bmp";
            tileInfo3[168] = "Tile1-WallLR.bmp";
            tileInfo3[169] = "Tile1-WallL.bmp";
            tileInfo3[170] = "Tile1-WallR.bmp";
            tileInfo3[171] = "Tile1-WallLR.bmp";
            tileInfo3[172] = "Tile1-WallL.bmp";
            tileInfo3[173] = "Tile1-WallR.bmp";
            tileInfo3[174] = "Tile1-WallLR.bmp";
            tileInfo3[175] = "Tile1-WallL.bmp";
            tileInfo3[176] = "Tile1.bmp";
            tileInfo3[177] = "Tile1.bmp";
            tileInfo3[178] = "Tile1-WallR.bmp";
            tileInfo3[179] = "Tile1-WallLR.bmp";

            // ６行目
            tileInfo3[180] = "Tile1-WallLR.bmp";
            tileInfo3[181] = "Tile1-WallL.bmp";
            tileInfo3[182] = "Tile1-WallTB.bmp";
            tileInfo3[183] = "Tile1-WallT.bmp";
            tileInfo3[184] = "Tile1-WallTB.bmp";
            tileInfo3[185] = "Tile1-WallTB.bmp";
            tileInfo3[186] = "Tile1-WallR.bmp";
            tileInfo3[187] = "Tile1-WallL.bmp";
            tileInfo3[188] = "Tile1-WallR.bmp";
            tileInfo3[189] = "Tile1-WallLR.bmp";
            tileInfo3[190] = "Tile1-WallLR.bmp";
            tileInfo3[191] = "Tile1-WallLR.bmp";
            tileInfo3[192] = "Tile1-WallLR.bmp";
            tileInfo3[193] = "Tile1-WallLR.bmp";
            tileInfo3[194] = "Tile1-WallL.bmp";
            tileInfo3[195] = "Tile1.bmp";
            tileInfo3[196] = "Tile1.bmp";
            tileInfo3[197] = "Tile1-WallR.bmp";
            tileInfo3[198] = "Tile1-WallLR.bmp";
            tileInfo3[199] = "Tile1-WallL.bmp";
            tileInfo3[200] = "Tile1-WallR.bmp";
            tileInfo3[201] = "Tile1-WallLR.bmp";
            tileInfo3[202] = "Tile1-WallLB.bmp";
            tileInfo3[203] = "Tile1-WallRB.bmp";
            tileInfo3[204] = "Tile1-WallLR.bmp";
            tileInfo3[205] = "Tile1-WallLB.bmp";
            tileInfo3[206] = "Tile1-WallB.bmp";
            tileInfo3[207] = "Tile1-WallB.bmp";
            tileInfo3[208] = "Tile1-WallRB.bmp";
            tileInfo3[209] = "Tile1-WallLR.bmp";

            // ７行目
            tileInfo3[210] = "Tile1-WallLR.bmp";
            tileInfo3[211] = "Tile1-WallLR.bmp";
            tileInfo3[213] = "Tile1-WallLR.bmp";
            tileInfo3[214] = "Tile1-WallTLR.bmp";
            tileInfo3[215] = "Tile1-WallTLR.bmp";
            tileInfo3[216] = "Tile1-WallLR.bmp";
            tileInfo3[217] = "Tile1-WallL.bmp";
            tileInfo3[218] = "Tile1-WallR.bmp";
            tileInfo3[219] = "Tile1-WallLR.bmp";
            tileInfo3[220] = "Tile1-WallLR.bmp";
            tileInfo3[221] = "Tile1-WallLR.bmp";
            tileInfo3[222] = "Tile1-WallLR.bmp";
            tileInfo3[223] = "Tile1-WallLR.bmp";
            tileInfo3[224] = "Tile1-WallL.bmp";
            tileInfo3[225] = "Tile1.bmp";
            tileInfo3[226] = "Tile1.bmp";
            tileInfo3[227] = "Tile1-WallR.bmp";
            tileInfo3[228] = "Tile1-WallLR.bmp";
            tileInfo3[229] = "Tile1-WallL.bmp";
            tileInfo3[230] = "Tile1-WallR.bmp";
            tileInfo3[231] = "Tile1-WallL.bmp";
            tileInfo3[232] = "Tile1-WallTB.bmp";
            tileInfo3[233] = "Tile1-WallTB.bmp";
            tileInfo3[234] = "Tile1-WallB.bmp";
            tileInfo3[235] = "Tile1-WallTB.bmp";
            tileInfo3[236] = "Tile1-WallTB.bmp";
            tileInfo3[237] = "Tile1-WallT.bmp";
            tileInfo3[238] = "Tile1-WallTB.bmp";
            tileInfo3[239] = "Tile1-WallR.bmp";

            // ８行目
            tileInfo3[240] = "Tile1-WallLR.bmp";
            tileInfo3[241] = "Tile1-WallLB.bmp";
            tileInfo3[242] = "Tile1-WallTB.bmp";
            tileInfo3[243] = "Tile1-WallRB.bmp";
            tileInfo3[244] = "Tile1-WallLR.bmp";
            tileInfo3[245] = "Tile1-WallLR.bmp";
            tileInfo3[246] = "Tile1-WallLR.bmp";
            tileInfo3[247] = "Tile1-WallL.bmp";
            tileInfo3[248] = "Tile1-WallR.bmp";
            tileInfo3[249] = "Tile1-WallLR.bmp";
            tileInfo3[250] = "Tile1-WallLR.bmp";
            tileInfo3[251] = "Tile1-WallLR.bmp";
            tileInfo3[252] = "Tile1-WallLR.bmp";
            tileInfo3[253] = "Tile1-WallLR.bmp";
            tileInfo3[254] = "Tile1-WallLB.bmp";
            tileInfo3[255] = "Tile1-WallB.bmp";
            tileInfo3[256] = "Tile1-WallB.bmp";
            tileInfo3[257] = "Tile1-WallRB.bmp";
            tileInfo3[258] = "Tile1-WallLR.bmp";
            tileInfo3[259] = "Tile1-WallLB.bmp";
            tileInfo3[260] = "Tile1-WallRB.bmp";
            tileInfo3[261] = "Tile1-WallLR.bmp";
            tileInfo3[262] = "Tile1-WallTL.bmp";
            tileInfo3[263] = "Tile1-WallT.bmp";
            tileInfo3[264] = "Tile1-WallT.bmp";
            tileInfo3[265] = "Tile1-WallT.bmp";
            tileInfo3[266] = "Tile1-WallTR.bmp";
            tileInfo3[267] = "Tile1-WallLR.bmp";
            tileInfo3[269] = "Tile1-WallLR.bmp";

            // ９行目
            tileInfo3[270] = "Tile1-WallLB.bmp";
            tileInfo3[271] = "Tile1-WallTB.bmp";
            tileInfo3[272] = "Tile1-WallTRB.bmp";
            tileInfo3[273] = "Tile1-WallTLR.bmp";
            tileInfo3[274] = "Tile1-WallLR.bmp";
            tileInfo3[275] = "Tile1-WallLR.bmp";
            tileInfo3[276] = "Tile1-WallLR.bmp";
            tileInfo3[277] = "Tile1-WallL.bmp";
            tileInfo3[278] = "Tile1-WallR.bmp";
            tileInfo3[279] = "Tile1-WallLR.bmp";
            tileInfo3[280] = "Tile1-WallLR.bmp";
            tileInfo3[281] = "Tile1-WallLR.bmp";
            tileInfo3[282] = "Tile1-WallLRB.bmp";
            tileInfo3[283] = "Tile1-WallLB.bmp";
            tileInfo3[284] = "Tile1-WallTB.bmp";
            tileInfo3[285] = "Tile1-WallTB.bmp";
            tileInfo3[286] = "Tile1-WallTB.bmp";
            tileInfo3[287] = "Tile1-WallTB.bmp";
            tileInfo3[288] = "Tile1-WallB.bmp";
            tileInfo3[289] = "Tile1-WallTB.bmp";
            tileInfo3[290] = "Tile1-WallTB.bmp";
            tileInfo3[291] = "Tile1-WallR.bmp";
            tileInfo3[292] = "Tile1-WallL.bmp";
            tileInfo3[293] = "Tile1.bmp";
            tileInfo3[294] = "Tile1.bmp";
            tileInfo3[295] = "Tile1.bmp";
            tileInfo3[296] = "Tile1-WallR.bmp";
            tileInfo3[297] = "Tile1-WallL.bmp";
            tileInfo3[298] = "Tile1-WallTB.bmp";
            tileInfo3[299] = "Tile1-WallR.bmp";

            // １０行目
            tileInfo3[300] = "Tile1-WallTL.bmp";
            tileInfo3[301] = "Tile1-WallT.bmp";
            tileInfo3[302] = "Tile1-WallTR.bmp";
            tileInfo3[303] = "Tile1-WallLR.bmp";
            tileInfo3[304] = "Tile1-WallLR.bmp";
            tileInfo3[305] = "Tile1-WallLR.bmp";
            tileInfo3[306] = "Tile1-WallLR.bmp";
            tileInfo3[307] = "Tile1-WallL.bmp";
            tileInfo3[308] = "Tile1-WallR.bmp";
            tileInfo3[309] = "Tile1-WallLR.bmp";
            tileInfo3[310] = "Tile1-WallLR.bmp";
            tileInfo3[311] = "Tile1-WallLB.bmp";
            tileInfo3[312] = "Tile1-WallTR.bmp";
            tileInfo3[313] = "Tile1-WallTL.bmp";
            tileInfo3[314] = "Tile1-WallTB.bmp";
            tileInfo3[315] = "Tile1-WallTB.bmp";
            tileInfo3[316] = "Tile1-WallTB.bmp";
            tileInfo3[317] = "Tile1-WallTB.bmp";
            tileInfo3[318] = "Tile1-WallTB.bmp";
            tileInfo3[319] = "Tile1-WallTB.bmp";
            tileInfo3[320] = "Tile1-WallTRB.bmp";
            tileInfo3[321] = "Tile1-WallLR.bmp";
            tileInfo3[322] = "Tile1-WallLB.bmp";
            tileInfo3[323] = "Tile1-WallB.bmp";
            tileInfo3[324] = "Tile1-WallB.bmp";
            tileInfo3[325] = "Tile1-WallB.bmp";
            tileInfo3[326] = "Tile1-WallRB.bmp";
            tileInfo3[327] = "Tile1-WallLR.bmp";
            tileInfo3[329] = "Tile1-WallLR.bmp";

            // １１行目
            tileInfo3[330] = "Tile1-WallL.bmp";
            tileInfo3[331] = "Tile1.bmp";
            tileInfo3[332] = "Tile1-WallR.bmp";
            tileInfo3[333] = "Tile1-WallLR.bmp";
            tileInfo3[334] = "Tile1-WallLR.bmp";
            tileInfo3[335] = "Tile1-WallLR.bmp";
            tileInfo3[336] = "Tile1-WallLR.bmp";
            tileInfo3[337] = "Tile1-WallLB.bmp";
            tileInfo3[338] = "Tile1-WallRB.bmp";
            tileInfo3[339] = "Tile1-WallLR.bmp";
            tileInfo3[340] = "Tile1-WallLB.bmp";
            tileInfo3[341] = "Tile1-WallTR.bmp";
            tileInfo3[342] = "Tile1-WallLR.bmp";
            tileInfo3[343] = "Tile1-WallLR.bmp";
            tileInfo3[344] = "Tile1-WallTLR.bmp";
            tileInfo3[345] = "Tile1-WallTL.bmp";
            tileInfo3[346] = "Tile1-WallTB.bmp";
            tileInfo3[347] = "Tile1-WallTB.bmp";
            tileInfo3[348] = "Tile1-WallTB.bmp";
            tileInfo3[349] = "Tile1-WallTB.bmp";
            tileInfo3[350] = "Tile1-WallTB.bmp";
            tileInfo3[351] = "Tile1-WallB.bmp";
            tileInfo3[352] = "Tile1-WallTB.bmp";
            tileInfo3[353] = "Tile1-WallT.bmp";
            tileInfo3[354] = "Tile1-WallTB.bmp";
            tileInfo3[355] = "Tile1-WallTB.bmp";
            tileInfo3[356] = "Tile1-WallTB.bmp";
            tileInfo3[357] = "Tile1-WallB.bmp";
            tileInfo3[358] = "Tile1-WallTB.bmp";
            tileInfo3[359] = "Tile1-WallRB.bmp";

            // １２行目
            tileInfo3[360] = "Tile1-WallL.bmp";
            tileInfo3[361] = "Tile1.bmp";
            tileInfo3[362] = "Tile1-WallR.bmp";
            tileInfo3[363] = "Tile1-WallLR.bmp";
            tileInfo3[364] = "Tile1-WallLR.bmp";
            tileInfo3[365] = "Tile1-WallLR.bmp";
            tileInfo3[366] = "Tile1-WallL.bmp";
            tileInfo3[367] = "Tile1-WallTB.bmp";
            tileInfo3[368] = "Tile1-WallTB.bmp";
            tileInfo3[369] = "Tile1-WallB.bmp";
            tileInfo3[370] = "Tile1-WallTR.bmp";
            tileInfo3[371] = "Tile1-WallLR.bmp";
            tileInfo3[372] = "Tile1-WallLR.bmp";
            tileInfo3[373] = "Tile1-WallLR.bmp";
            tileInfo3[374] = "Tile1-WallLR.bmp";
            tileInfo3[375] = "Tile1-WallLR.bmp";
            tileInfo3[376] = "Tile1-WallTL.bmp";
            tileInfo3[377] = "Tile1-WallT.bmp";
            tileInfo3[378] = "Tile1-WallT.bmp";
            tileInfo3[379] = "Tile1-WallT.bmp";
            tileInfo3[380] = "Tile1-WallT.bmp";
            tileInfo3[381] = "Tile1-WallT.bmp";
            tileInfo3[382] = "Tile1-WallTR.bmp";
            tileInfo3[383] = "Tile1-WallLR.bmp";
            tileInfo3[384] = "Tile1-WallTL.bmp";
            tileInfo3[385] = "Tile1-WallT.bmp";
            tileInfo3[386] = "Tile1-WallT.bmp";
            tileInfo3[387] = "Tile1-WallT.bmp";
            tileInfo3[388] = "Tile1-WallT.bmp";
            tileInfo3[389] = "Tile1-WallTR.bmp";

            // １３行目
            tileInfo3[390] = "Tile1-WallLB.bmp";
            tileInfo3[391] = "Tile1-WallB.bmp";
            tileInfo3[392] = "Tile1-WallRB.bmp";
            tileInfo3[393] = "Tile1-WallLR.bmp";
            tileInfo3[394] = "Tile1-WallLR.bmp";
            tileInfo3[395] = "Tile1-WallLRB.bmp";
            tileInfo3[396] = "Tile1-WallLR.bmp";
            tileInfo3[397] = "Tile1-WallTL.bmp";
            tileInfo3[398] = "Tile1-WallT.bmp";
            tileInfo3[399] = "Tile1-WallTR.bmp";
            tileInfo3[400] = "Tile1-WallLR.bmp";
            tileInfo3[401] = "Tile1-WallLR.bmp";
            tileInfo3[402] = "Tile1-WallLR.bmp";
            tileInfo3[403] = "Tile1-WallLR.bmp";
            tileInfo3[404] = "Tile1-WallLR.bmp";
            tileInfo3[405] = "Tile1-WallLRB.bmp";
            tileInfo3[406] = "Tile1-WallL.bmp";
            tileInfo3[407] = "Tile1.bmp";
            tileInfo3[408] = "Tile1.bmp";
            tileInfo3[409] = "Tile1.bmp";
            tileInfo3[410] = "Tile1.bmp";
            tileInfo3[411] = "Tile1.bmp";
            tileInfo3[412] = "Tile1-WallR.bmp";
            tileInfo3[413] = "Tile1-WallLR.bmp";
            tileInfo3[414] = "Tile1-WallLB.bmp";
            tileInfo3[415] = "Tile1-WallB.bmp";
            tileInfo3[416] = "Tile1-WallB.bmp";
            tileInfo3[417] = "Tile1-WallB.bmp";
            tileInfo3[418] = "Tile1-WallB.bmp";
            tileInfo3[419] = "Tile1-WallRB.bmp";

            // １４行目
            tileInfo3[420] = "Tile1-WallTL.bmp";
            tileInfo3[421] = "Tile1-WallTB.bmp";
            tileInfo3[422] = "Tile1-WallTB.bmp";
            tileInfo3[423] = "Tile1-WallLR-DummyL.bmp";
            tileInfo3[424] = "Tile1-WallLB.bmp";
            tileInfo3[425] = "Tile1-WallTB.bmp";
            tileInfo3[426] = "Tile1-WallR.bmp";
            tileInfo3[427] = "Tile1-WallL.bmp";
            tileInfo3[428] = "Tile1.bmp";
            tileInfo3[429] = "Tile1-WallR.bmp";
            tileInfo3[430] = "Tile1-WallLR.bmp";
            tileInfo3[431] = "Tile1-WallLR.bmp";
            tileInfo3[432] = "Tile1-WallLR.bmp";
            tileInfo3[433] = "Tile1-WallLRB.bmp";
            tileInfo3[434] = "Tile1-WallLB.bmp";
            tileInfo3[435] = "Tile1-WallTR.bmp";
            tileInfo3[436] = "Tile1-WallL.bmp";
            tileInfo3[437] = "Tile1.bmp";
            tileInfo3[438] = "Tile1.bmp";
            tileInfo3[439] = "Tile1.bmp";
            tileInfo3[440] = "Tile1.bmp";
            tileInfo3[441] = "Tile1.bmp";
            tileInfo3[442] = "Tile1-WallR.bmp";
            tileInfo3[443] = "Tile1-WallL.bmp";
            tileInfo3[444] = "Tile1-WallTB.bmp";
            tileInfo3[445] = "Tile1-WallTB.bmp";
            tileInfo3[446] = "Tile1-WallTB.bmp";
            tileInfo3[447] = "Tile1-WallTB.bmp";
            tileInfo3[448] = "Tile1-WallT.bmp";
            tileInfo3[449] = "Tile1-WallTRB.bmp";

            // １５行目
            tileInfo3[450] = "Tile1-WallLR.bmp";
            tileInfo3[451] = "Tile1-WallTL.bmp";
            tileInfo3[452] = "Tile1-WallT.bmp";
            tileInfo3[453] = "Tile1.bmp";
            tileInfo3[454] = "Tile1-WallT.bmp";
            tileInfo3[455] = "Tile1-WallTR.bmp";
            tileInfo3[456] = "Tile1-WallLR.bmp";
            tileInfo3[457] = "Tile1-WallL.bmp";
            tileInfo3[458] = "Tile1.bmp";
            tileInfo3[459] = "Tile1-WallR.bmp";
            tileInfo3[460] = "Tile1-WallLR.bmp";
            tileInfo3[461] = "Tile1-WallLR.bmp";
            tileInfo3[462] = "Tile1-WallLB.bmp";
            tileInfo3[463] = "Tile1-WallTR.bmp";
            tileInfo3[464] = "Tile1-WallTLR.bmp";
            tileInfo3[465] = "Tile1-WallLR.bmp";
            tileInfo3[466] = "Tile1-WallL.bmp";
            tileInfo3[467] = "Tile1.bmp";
            tileInfo3[468] = "Tile1.bmp";
            tileInfo3[469] = "Tile1.bmp";
            tileInfo3[470] = "Tile1.bmp";
            tileInfo3[471] = "Tile1.bmp";
            tileInfo3[472] = "Tile1-WallR.bmp";
            tileInfo3[473] = "Tile1-WallLR.bmp";
            tileInfo3[474] = "Tile1-WallTL.bmp";
            tileInfo3[475] = "Tile1-WallT.bmp";
            tileInfo3[476] = "Tile1-WallT.bmp";
            tileInfo3[477] = "Tile1-WallTR.bmp";
            tileInfo3[478] = "Tile1-WallLR.bmp";
            tileInfo3[479] = "Tile1-WallTLR.bmp";

            // １６行目
            tileInfo3[480] = "Tile1-WallLR.bmp";
            tileInfo3[481] = "Tile1-WallL.bmp";
            tileInfo3[485] = "Tile1-WallR.bmp";
            tileInfo3[486] = "Tile1-WallLR.bmp";
            tileInfo3[487] = "Tile1-WallLB.bmp";
            tileInfo3[488] = "Tile1-WallB.bmp";
            tileInfo3[489] = "Tile1-WallRB.bmp";
            tileInfo3[490] = "Tile1-WallLR.bmp";
            tileInfo3[491] = "Tile1-WallLB.bmp";
            tileInfo3[492] = "Tile1-WallTR.bmp";
            tileInfo3[493] = "Tile1-WallLR.bmp";
            tileInfo3[494] = "Tile1-WallLR.bmp";
            tileInfo3[495] = "Tile1-WallLR.bmp";
            tileInfo3[496] = "Tile1-WallL.bmp";
            tileInfo3[497] = "Tile1.bmp";
            tileInfo3[498] = "Tile1.bmp";
            tileInfo3[499] = "Tile1.bmp";
            tileInfo3[500] = "Tile1.bmp";
            tileInfo3[501] = "Tile1.bmp";
            tileInfo3[502] = "Tile1-WallR.bmp";
            tileInfo3[503] = "Tile1-WallLR.bmp";
            tileInfo3[504] = "Tile1-WallL.bmp";
            tileInfo3[505] = "Tile1.bmp";
            tileInfo3[506] = "Tile1.bmp";
            tileInfo3[507] = "Tile1-WallR.bmp";
            tileInfo3[508] = "Tile1-WallLR.bmp";
            tileInfo3[509] = "Tile1-WallLR.bmp";

            // １７行目
            tileInfo3[510] = "Tile1-WallLR.bmp";
            tileInfo3[511] = "Tile1-WallL.bmp";
            tileInfo3[515] = "Tile1-WallR.bmp";
            tileInfo3[516] = "Tile1-WallLB.bmp";
            tileInfo3[517] = "Tile1-WallT.bmp";
            tileInfo3[518] = "Tile1-WallTB.bmp";
            tileInfo3[519] = "Tile1-WallTB.bmp";
            tileInfo3[520] = "Tile1-WallB.bmp";
            tileInfo3[521] = "Tile1-WallTR.bmp";
            tileInfo3[522] = "Tile1-WallLR.bmp";
            tileInfo3[523] = "Tile1-WallLR.bmp";
            tileInfo3[524] = "Tile1-WallLR.bmp";
            tileInfo3[525] = "Tile1-WallLR.bmp";
            tileInfo3[526] = "Tile1-WallL.bmp";
            tileInfo3[527] = "Tile1.bmp";
            tileInfo3[528] = "Tile1.bmp";
            tileInfo3[529] = "Tile1.bmp";
            tileInfo3[530] = "Tile1.bmp";
            tileInfo3[531] = "Tile1.bmp";
            tileInfo3[532] = "Tile1-WallR.bmp";
            tileInfo3[533] = "Tile1-WallLR.bmp";
            tileInfo3[534] = "Tile1-WallL.bmp";
            tileInfo3[535] = "Tile1.bmp";
            tileInfo3[536] = "Tile1.bmp";
            tileInfo3[537] = "Tile1-WallR.bmp";
            tileInfo3[538] = "Tile1-WallLR.bmp";
            tileInfo3[539] = "Tile1-WallLR.bmp";

            // １８行目
            tileInfo3[540] = "Tile1-WallLR.bmp";
            tileInfo3[541] = "Tile1-WallLB.bmp";
            tileInfo3[542] = "Tile1-WallB.bmp";
            tileInfo3[543] = "Tile1-WallB.bmp";
            tileInfo3[544] = "Tile1-WallB.bmp";
            tileInfo3[545] = "Tile1-WallB.bmp";
            tileInfo3[546] = "Tile1-WallTR.bmp";
            tileInfo3[547] = "Tile1-WallLR.bmp";
            tileInfo3[548] = "Tile1-WallTLB.bmp";
            tileInfo3[549] = "Tile1-WallTB.bmp";
            tileInfo3[550] = "Tile1-WallTRB.bmp";
            tileInfo3[551] = "Tile1-WallLR.bmp";
            tileInfo3[552] = "Tile1-WallLRB.bmp";
            tileInfo3[553] = "Tile1-WallLR.bmp";
            tileInfo3[554] = "Tile1-WallLR.bmp";
            tileInfo3[555] = "Tile1-WallLR.bmp";
            tileInfo3[556] = "Tile1-WallLB.bmp";
            tileInfo3[557] = "Tile1-WallB.bmp";
            tileInfo3[558] = "Tile1-WallB.bmp";
            tileInfo3[559] = "Tile1-WallB.bmp";
            tileInfo3[560] = "Tile1-WallB.bmp";
            tileInfo3[561] = "Tile1-WallB.bmp";
            tileInfo3[562] = "Tile1-WallRB.bmp";
            tileInfo3[563] = "Tile1-WallLR.bmp";
            tileInfo3[564] = "Tile1-WallLB.bmp";
            tileInfo3[565] = "Tile1-WallB.bmp";
            tileInfo3[566] = "Tile1-WallB.bmp";
            tileInfo3[567] = "Tile1-WallRB.bmp";
            tileInfo3[568] = "Tile1-WallLR.bmp";
            tileInfo3[569] = "Tile1-WallLR.bmp";

            // １９行目
            tileInfo3[570] = "Tile1-WallLB.bmp";
            tileInfo3[571] = "Tile1-WallTB.bmp";
            tileInfo3[572] = "Tile1-WallTB.bmp";
            tileInfo3[573] = "Tile1-WallTB.bmp";
            tileInfo3[574] = "Tile1-WallTB.bmp";
            tileInfo3[575] = "Tile1-WallTRB.bmp";
            tileInfo3[576] = "Downstair-WallLRB.bmp";
            tileInfo3[577] = "Tile1-WallLB.bmp";
            tileInfo3[578] = "Tile1-WallTB.bmp";
            tileInfo3[579] = "Tile1-WallTB.bmp";
            tileInfo3[580] = "Tile1-WallTB.bmp";
            tileInfo3[581] = "Tile1-WallB.bmp";
            tileInfo3[582] = "Tile1-WallTB.bmp";
            tileInfo3[583] = "Tile1-WallRB.bmp";
            tileInfo3[584] = "Tile1-WallLRB.bmp";
            tileInfo3[585] = "Tile1-WallLB.bmp";
            tileInfo3[586] = "Tile1-WallTB.bmp";
            tileInfo3[587] = "Tile1-WallTB.bmp";
            tileInfo3[588] = "Tile1-WallTB.bmp";
            tileInfo3[589] = "Tile1-WallTB.bmp";
            tileInfo3[590] = "Tile1-WallTB.bmp";
            tileInfo3[591] = "Tile1-WallTB.bmp";
            tileInfo3[592] = "Tile1-WallTB.bmp";
            tileInfo3[593] = "Tile1-WallB.bmp";
            tileInfo3[594] = "Tile1-WallTB.bmp";
            tileInfo3[595] = "Tile1-WallTB.bmp";
            tileInfo3[596] = "Tile1-WallTB.bmp";
            tileInfo3[597] = "Tile1-WallTB.bmp";
            tileInfo3[598] = "Tile1-WallRB.bmp";
            tileInfo3[599] = "Tile1-WallLRB.bmp";
            #endregion
            #region "ダンジョン４階"
            // ０行目
            tileInfo4[0] = "Tile1-WallTL.bmp";
            tileInfo4[1] = "Tile1-WallTB.bmp";
            tileInfo4[2] = "Tile1-WallTB.bmp";
            tileInfo4[3] = "Tile1-WallTB.bmp";
            tileInfo4[4] = "Tile1-WallTB.bmp";
            tileInfo4[5] = "Tile1-WallTB.bmp";
            tileInfo4[6] = "Tile1-WallTB.bmp";
            tileInfo4[7] = "Tile1-WallTB.bmp";
            tileInfo4[8] = "Tile1-WallTB.bmp";
            tileInfo4[9] = "Tile1-WallTB.bmp";
            tileInfo4[10] = "Tile1-WallTB.bmp";
            tileInfo4[11] = "Tile1-WallTB.bmp";
            tileInfo4[12] = "Tile1-WallTB.bmp";
            tileInfo4[13] = "Tile1-WallTB.bmp";
            tileInfo4[14] = "Tile1-WallTB.bmp";
            tileInfo4[15] = "Tile1-WallTB.bmp";
            tileInfo4[16] = "Tile1-WallTB.bmp";
            tileInfo4[17] = "Tile1-WallTB.bmp";
            tileInfo4[18] = "Tile1-WallTB.bmp";
            tileInfo4[19] = "Tile1-WallTR.bmp";
            tileInfo4[20] = "Downstair-WallTLB.bmp";
            tileInfo4[21] = "Tile1-WallTB.bmp";
            tileInfo4[22] = "Tile1-WallTB.bmp";
            tileInfo4[23] = "Tile1-WallTB.bmp";
            tileInfo4[24] = "Tile1-WallTB.bmp";
            tileInfo4[25] = "Tile1-WallTB.bmp";
            tileInfo4[26] = "Tile1-WallTB.bmp";
            tileInfo4[27] = "Tile1-WallTB.bmp";
            tileInfo4[28] = "Tile1-WallTB.bmp";
            tileInfo4[29] = "Tile1-WallTR.bmp";
            // １行目
            tileInfo4[30] = "Tile1-WallLR.bmp";
            tileInfo4[31] = "Tile1-WallTL.bmp";
            tileInfo4[32] = "Tile1-WallTR.bmp";
            tileInfo4[33] = "Tile1-WallTL.bmp";
            tileInfo4[34] = "Tile1-WallTR.bmp";
            tileInfo4[35] = "Tile1-WallTL.bmp";
            tileInfo4[36] = "Tile1-WallTR.bmp";
            tileInfo4[37] = "Tile1-WallTL.bmp";
            tileInfo4[38] = "Tile1-WallTR.bmp";
            tileInfo4[39] = "Tile1-WallTL.bmp";
            tileInfo4[40] = "Tile1-WallTR.bmp";
            tileInfo4[41] = "Tile1-WallTL.bmp";
            tileInfo4[42] = "Tile1-WallTR.bmp";
            tileInfo4[43] = "Tile1-WallTL.bmp";
            tileInfo4[44] = "Tile1-WallTR.bmp";
            tileInfo4[45] = "Tile1-WallTL.bmp";
            tileInfo4[46] = "Tile1-WallTR.bmp";
            tileInfo4[47] = "Tile1-WallTL.bmp";
            tileInfo4[48] = "Tile1-WallTR.bmp";
            tileInfo4[49] = "Tile1-WallLR.bmp";
            tileInfo4[50] = "Tile1-WallTL.bmp";
            tileInfo4[51] = "Tile1-WallTB.bmp";
            tileInfo4[52] = "Tile1-WallTB.bmp";
            tileInfo4[53] = "Tile1-WallTB.bmp";
            tileInfo4[54] = "Tile1-WallTR.bmp";
            tileInfo4[55] = "Tile1-WallTL.bmp";
            tileInfo4[56] = "Tile1-WallTR.bmp";
            tileInfo4[57] = "Tile1-WallTL.bmp";
            tileInfo4[58] = "Tile1-WallTR.bmp";
            tileInfo4[59] = "Tile1-WallLR.bmp";
            // ２行目
            tileInfo4[60] = "Tile1-WallLB.bmp";
            tileInfo4[61] = "Tile1-WallRB.bmp";
            tileInfo4[62] = "Tile1-WallLB.bmp";
            tileInfo4[63] = "Tile1-WallRB.bmp";
            tileInfo4[64] = "Tile1-WallLB.bmp";
            tileInfo4[65] = "Tile1-WallRB.bmp";
            tileInfo4[66] = "Tile1-WallLB.bmp";
            tileInfo4[67] = "Tile1-WallRB.bmp";
            tileInfo4[68] = "Tile1-WallLB.bmp";
            tileInfo4[69] = "Tile1-WallRB.bmp";
            tileInfo4[70] = "Tile1-WallLB.bmp";
            tileInfo4[71] = "Tile1-WallRB.bmp";
            tileInfo4[72] = "Tile1-WallLB.bmp";
            tileInfo4[73] = "Tile1-WallRB.bmp";
            tileInfo4[74] = "Tile1-WallLB.bmp";
            tileInfo4[75] = "Tile1-WallRB.bmp";
            tileInfo4[76] = "Tile1-WallLR.bmp";
            tileInfo4[77] = "Tile1-WallLR.bmp";
            tileInfo4[78] = "Tile1-WallLB.bmp";
            tileInfo4[79] = "Tile1-WallRB.bmp";
            tileInfo4[80] = "Tile1-WallLB.bmp";
            tileInfo4[81] = "Tile1-WallTB.bmp";
            tileInfo4[82] = "Tile1-WallTB.bmp";
            tileInfo4[83] = "Tile1-WallTR.bmp";
            tileInfo4[84] = "Tile1-WallLB.bmp";
            tileInfo4[85] = "Tile1-WallRB.bmp";
            tileInfo4[86] = "Tile1-WallLB.bmp";
            tileInfo4[87] = "Tile1-WallRB.bmp";
            tileInfo4[88] = "Tile1-WallLB.bmp";
            tileInfo4[89] = "Tile1-WallRB.bmp";
            // ３行目
            tileInfo4[90] = "Tile1-WallTL.bmp";
            tileInfo4[91] = "Tile1-WallTB.bmp";
            tileInfo4[92] = "Tile1-WallTB.bmp";
            tileInfo4[93] = "Tile1-WallTB.bmp";
            tileInfo4[94] = "Tile1-WallTB.bmp";
            tileInfo4[95] = "Tile1-WallTB.bmp";
            tileInfo4[96] = "Tile1-WallTB.bmp";
            tileInfo4[97] = "Tile1-WallTB.bmp";
            tileInfo4[98] = "Tile1-WallTB.bmp";
            tileInfo4[99] = "Tile1-WallTB.bmp";
            tileInfo4[100] = "Tile1-WallTB.bmp";
            tileInfo4[101] = "Tile1-WallTB.bmp";
            tileInfo4[102] = "Tile1-WallTB.bmp";
            tileInfo4[103] = "Tile1-WallTR.bmp";
            tileInfo4[104] = "Tile1-WallTL.bmp";
            tileInfo4[105] = "Tile1-WallTR.bmp";
            tileInfo4[106] = "Tile1-WallLR.bmp";
            tileInfo4[107] = "Tile1-WallLB.bmp";
            tileInfo4[108] = "Tile1-WallTB.bmp";
            tileInfo4[109] = "Tile1-WallTB.bmp";
            tileInfo4[110] = "Tile1-WallTB.bmp";
            tileInfo4[111] = "Tile1-WallTB.bmp";
            tileInfo4[112] = "Tile1-WallTR.bmp";
            tileInfo4[113] = "Tile1-WallLR.bmp";
            tileInfo4[114] = "Tile1-WallTL.bmp";
            tileInfo4[115] = "Tile1-WallTR.bmp";
            tileInfo4[116] = "Tile1-WallTL.bmp";
            tileInfo4[117] = "Tile1-WallTR.bmp";
            tileInfo4[118] = "Tile1-WallTL.bmp";
            tileInfo4[119] = "Tile1-WallTR.bmp";
            // ４行目
            tileInfo4[120] = "Tile1-WallLR.bmp";
            tileInfo4[121] = "Tile1-WallTL.bmp";
            tileInfo4[122] = "Tile1-WallTB.bmp";
            tileInfo4[123] = "Tile1-WallTB.bmp";
            tileInfo4[124] = "Tile1-WallTB.bmp";
            tileInfo4[125] = "Tile1-WallTB.bmp";
            tileInfo4[126] = "Tile1-WallTB.bmp";
            tileInfo4[127] = "Tile1-WallTB.bmp";
            tileInfo4[128] = "Tile1-WallTB.bmp";
            tileInfo4[129] = "Tile1-WallTB.bmp";
            tileInfo4[130] = "Tile1-WallTB.bmp";
            tileInfo4[131] = "Tile1-WallTB.bmp";
            tileInfo4[132] = "Tile1-WallTR.bmp";
            tileInfo4[133] = "Tile1-WallLR.bmp";
            tileInfo4[134] = "Tile1-WallLR.bmp";
            tileInfo4[135] = "Tile1-WallLR.bmp";
            tileInfo4[136] = "Tile1-WallLR.bmp";
            tileInfo4[137] = "Tile1-WallTL.bmp";
            tileInfo4[138] = "Tile1-WallTB.bmp";
            tileInfo4[139] = "Tile1-WallTB.bmp";
            tileInfo4[140] = "Tile1-WallTB.bmp";
            tileInfo4[141] = "Tile1-WallTR.bmp";
            tileInfo4[142] = "Tile1-WallLR.bmp";
            tileInfo4[143] = "Tile1-WallLR.bmp";
            tileInfo4[144] = "Tile1-WallLR.bmp";
            tileInfo4[145] = "Tile1-WallLR.bmp";
            tileInfo4[146] = "Tile1-WallLR.bmp";
            tileInfo4[147] = "Tile1-WallLR.bmp";
            tileInfo4[148] = "Tile1-WallLR.bmp";
            tileInfo4[149] = "Tile1-WallLR.bmp";
            // ５行目
            tileInfo4[150] = "Tile1-WallLR.bmp";
            tileInfo4[151] = "Tile1-WallLR.bmp";
            tileInfo4[152] = "Tile1-WallTL.bmp";
            tileInfo4[153] = "Tile1-WallTB.bmp";
            tileInfo4[154] = "Tile1-WallTB.bmp";
            tileInfo4[155] = "Tile1-WallTB.bmp";
            tileInfo4[156] = "Tile1-WallTB.bmp";
            tileInfo4[157] = "Tile1-WallTB.bmp";
            tileInfo4[158] = "Tile1-WallTB.bmp";
            tileInfo4[159] = "Tile1-WallTB.bmp";
            tileInfo4[160] = "Tile1-WallTB.bmp";
            tileInfo4[161] = "Tile1-WallTR.bmp";
            tileInfo4[162] = "Tile1-WallLR.bmp";
            tileInfo4[163] = "Tile1-WallLR.bmp";
            tileInfo4[164] = "Tile1-WallLR.bmp";
            tileInfo4[165] = "Tile1-WallLR.bmp";
            tileInfo4[166] = "Tile1-WallLR.bmp";
            tileInfo4[167] = "Tile1-WallLR.bmp";
            tileInfo4[168] = "Tile1-WallTL.bmp";
            tileInfo4[169] = "Tile1-WallTB.bmp";
            tileInfo4[170] = "Tile1-WallTR.bmp";
            tileInfo4[171] = "Tile1-WallLR.bmp";
            tileInfo4[172] = "Tile1-WallLR.bmp";
            tileInfo4[173] = "Tile1-WallLR.bmp";
            tileInfo4[174] = "Tile1-WallLR.bmp";
            tileInfo4[175] = "Tile1-WallLR.bmp";
            tileInfo4[176] = "Tile1-WallLR.bmp";
            tileInfo4[177] = "Tile1-WallLR.bmp";
            tileInfo4[178] = "Tile1-WallLR.bmp";
            tileInfo4[179] = "Tile1-WallLR.bmp";
            // ６行目
            tileInfo4[180] = "Tile1-WallLR.bmp";
            tileInfo4[181] = "Tile1-WallLR.bmp";
            tileInfo4[182] = "Tile1-WallLR.bmp";
            tileInfo4[183] = "Tile1-WallTL.bmp";
            tileInfo4[184] = "Tile1-WallTB.bmp";
            tileInfo4[185] = "Tile1-WallTB.bmp";
            tileInfo4[186] = "Tile1-WallTB.bmp";
            tileInfo4[187] = "Tile1-WallTB.bmp";
            tileInfo4[188] = "Tile1-WallTB.bmp";
            tileInfo4[189] = "Tile1-WallTB.bmp";
            tileInfo4[190] = "Tile1-WallTR.bmp";
            tileInfo4[191] = "Tile1-WallLR.bmp";
            tileInfo4[192] = "Tile1-WallLR.bmp";
            tileInfo4[193] = "Tile1-WallLR.bmp";
            tileInfo4[194] = "Tile1-WallLR.bmp";
            tileInfo4[195] = "Tile1-WallLB.bmp";
            tileInfo4[196] = "Tile1-WallRB.bmp";
            tileInfo4[197] = "Tile1-WallLR.bmp";
            tileInfo4[198] = "Tile1-WallLR.bmp";
            tileInfo4[199] = "Tile1-WallTL.bmp";
            tileInfo4[200] = "Tile1-WallRB.bmp";
            tileInfo4[201] = "Tile1-WallLR.bmp";
            tileInfo4[202] = "Tile1-WallLR.bmp";
            tileInfo4[203] = "Tile1-WallLR.bmp";
            tileInfo4[204] = "Tile1-WallLR.bmp";
            tileInfo4[205] = "Tile1-WallLR.bmp";
            tileInfo4[206] = "Tile1-WallLR.bmp";
            tileInfo4[207] = "Tile1-WallLR.bmp";
            tileInfo4[208] = "Tile1-WallLR.bmp";
            tileInfo4[209] = "Tile1-WallLR.bmp";
            // ７行目
            tileInfo4[210] = "Tile1-WallLR.bmp";
            tileInfo4[211] = "Tile1-WallLR.bmp";
            tileInfo4[212] = "Tile1-WallLR.bmp";
            tileInfo4[213] = "Tile1-WallLR.bmp";
            tileInfo4[214] = "Tile1-WallTL.bmp";
            tileInfo4[215] = "Tile1-WallTB.bmp";
            tileInfo4[216] = "Tile1-WallTB.bmp";
            tileInfo4[217] = "Tile1-WallTB.bmp";
            tileInfo4[218] = "Tile1-WallTB.bmp";
            tileInfo4[219] = "Tile1-WallTR.bmp";
            tileInfo4[220] = "Tile1-WallLR.bmp";
            tileInfo4[221] = "Tile1-WallLR.bmp";
            tileInfo4[222] = "Tile1-WallLR.bmp";
            tileInfo4[223] = "Tile1-WallLR.bmp";
            tileInfo4[224] = "Tile1-WallLR.bmp";
            tileInfo4[225] = "Tile1-WallTL.bmp";
            tileInfo4[226] = "Tile1-WallTR.bmp";
            tileInfo4[227] = "Tile1-WallLR.bmp";
            tileInfo4[228] = "Tile1-WallLR.bmp";
            tileInfo4[229] = "Tile1-WallLB.bmp";
            tileInfo4[230] = "Tile1-WallTB.bmp";
            tileInfo4[231] = "Tile1-WallRB.bmp";
            tileInfo4[232] = "Tile1-WallLR.bmp";
            tileInfo4[233] = "Tile1-WallLR.bmp";
            tileInfo4[234] = "Tile1-WallLR.bmp";
            tileInfo4[235] = "Tile1-WallLR.bmp";
            tileInfo4[236] = "Tile1-WallLR.bmp";
            tileInfo4[237] = "Tile1-WallLR.bmp";
            tileInfo4[238] = "Tile1-WallLR.bmp";
            tileInfo4[239] = "Tile1-WallLR.bmp";
            // ８行目
            tileInfo4[240] = "Tile1-WallLR.bmp";
            tileInfo4[241] = "Tile1-WallLR.bmp";
            tileInfo4[242] = "Tile1-WallLR.bmp";
            tileInfo4[243] = "Tile1-WallLR.bmp";
            tileInfo4[244] = "Tile1-WallLR.bmp";
            tileInfo4[245] = "Tile1-WallTL.bmp";
            tileInfo4[246] = "Tile1-WallTB.bmp";
            tileInfo4[247] = "Tile1-WallTB.bmp";
            tileInfo4[248] = "Tile1-WallTR.bmp";
            tileInfo4[249] = "Tile1-WallLR.bmp";
            tileInfo4[250] = "Tile1-WallLR.bmp";
            tileInfo4[251] = "Tile1-WallLR.bmp";
            tileInfo4[252] = "Tile1-WallLR.bmp";
            tileInfo4[253] = "Tile1-WallLR.bmp";
            tileInfo4[254] = "Tile1-WallLR.bmp";
            tileInfo4[255] = "Tile1-WallLR.bmp";
            tileInfo4[256] = "Tile1-WallLR.bmp";
            tileInfo4[257] = "Tile1-WallLR.bmp";
            tileInfo4[258] = "Tile1-WallLB.bmp";
            tileInfo4[259] = "Tile1-WallTB.bmp";
            tileInfo4[260] = "Tile1-WallTB.bmp";
            tileInfo4[261] = "Tile1-WallTB.bmp";
            tileInfo4[262] = "Tile1-WallRB.bmp";
            tileInfo4[263] = "Tile1-WallLB.bmp";
            tileInfo4[264] = "Tile1-WallRB.bmp";
            tileInfo4[265] = "Tile1-WallLB.bmp";
            tileInfo4[266] = "Tile1-WallRB.bmp";
            tileInfo4[267] = "Tile1-WallLB.bmp";
            tileInfo4[268] = "Tile1-WallRB.bmp";
            tileInfo4[269] = "Tile1-WallLR.bmp";
            // ９行目
            tileInfo4[270] = "Tile1-WallLR.bmp";
            tileInfo4[271] = "Tile1-WallLR.bmp";
            tileInfo4[272] = "Tile1-WallLR.bmp";
            tileInfo4[273] = "Tile1-WallLR.bmp";
            tileInfo4[274] = "Tile1-WallLR.bmp";
            tileInfo4[275] = "Tile1-WallLR.bmp";
            tileInfo4[276] = "Tile1-WallTL.bmp";
            tileInfo4[277] = "Tile1-WallTR.bmp";
            tileInfo4[278] = "Tile1-WallLR.bmp";
            tileInfo4[279] = "Tile1-WallLR.bmp";
            tileInfo4[280] = "Tile1-WallLR.bmp";
            tileInfo4[281] = "Tile1-WallLR.bmp";
            tileInfo4[282] = "Tile1-WallLR.bmp";
            tileInfo4[283] = "Tile1-WallLR.bmp";
            tileInfo4[284] = "Tile1-WallLR.bmp";
            tileInfo4[285] = "Tile1-WallLR.bmp";
            tileInfo4[286] = "Tile1-WallLR.bmp";
            tileInfo4[287] = "Tile1-WallLB.bmp";
            tileInfo4[288] = "Tile1-WallTB.bmp";
            tileInfo4[289] = "Tile1-WallTB.bmp";
            tileInfo4[290] = "Tile1-WallTB.bmp";
            tileInfo4[291] = "Tile1-WallTB.bmp";
            tileInfo4[292] = "Tile1-WallTB.bmp";
            tileInfo4[293] = "Tile1-WallTB.bmp";
            tileInfo4[294] = "Tile1-WallTB.bmp";
            tileInfo4[295] = "Tile1-WallTR.bmp";
            tileInfo4[296] = "Tile1-WallTL.bmp";
            tileInfo4[297] = "Tile1-WallTB.bmp";
            tileInfo4[298] = "Tile1-WallTB.bmp";
            tileInfo4[299] = "Tile1-WallRB.bmp";
            // １０行目
            tileInfo4[300] = "Tile1-WallLR.bmp";
            tileInfo4[301] = "Tile1-WallLR.bmp";
            tileInfo4[302] = "Tile1-WallLR.bmp";
            tileInfo4[303] = "Tile1-WallLR.bmp";
            tileInfo4[304] = "Tile1-WallLR.bmp";
            tileInfo4[305] = "Tile1-WallLR.bmp";
            tileInfo4[306] = "Tile1-WallLR.bmp";
            tileInfo4[307] = "Tile1-WallLR.bmp";
            tileInfo4[308] = "Tile1-WallLR.bmp";
            tileInfo4[309] = "Tile1-WallLR.bmp";
            tileInfo4[310] = "Tile1-WallLR.bmp";
            tileInfo4[311] = "Tile1-WallLR.bmp";
            tileInfo4[312] = "Tile1-WallLR.bmp";
            tileInfo4[313] = "Tile1-WallLR.bmp";
            tileInfo4[314] = "Tile1-WallLB.bmp";
            tileInfo4[315] = "Tile1-WallRB.bmp";
            tileInfo4[316] = "Tile1-WallLR.bmp";
            tileInfo4[317] = "Tile1-WallTL.bmp";
            tileInfo4[318] = "Tile1-WallTB.bmp";
            tileInfo4[319] = "Tile1-WallTB.bmp";
            tileInfo4[320] = "Tile1-WallTB.bmp";
            tileInfo4[321] = "Tile1-WallTB.bmp";
            tileInfo4[322] = "Tile1-WallTB.bmp";
            tileInfo4[323] = "Tile1-WallTB.bmp";
            tileInfo4[324] = "Tile1-WallTR.bmp";
            tileInfo4[325] = "Tile1-WallLR.bmp";
            tileInfo4[326] = "Tile1-WallLR.bmp";
            tileInfo4[327] = "Tile1-WallTL.bmp";
            tileInfo4[328] = "Tile1-WallTB.bmp";
            tileInfo4[329] = "Tile1-WallTR.bmp";
            // １１行目
            tileInfo4[330] = "Tile1-WallLR.bmp";
            tileInfo4[331] = "Tile1-WallLR.bmp";
            tileInfo4[332] = "Tile1-WallLR.bmp";
            tileInfo4[333] = "Tile1-WallLR.bmp";
            tileInfo4[334] = "Tile1-WallLR.bmp";
            tileInfo4[335] = "Tile1-WallLR.bmp";
            tileInfo4[336] = "Tile1-WallLR.bmp";
            tileInfo4[337] = "Tile1-WallLB.bmp";
            tileInfo4[338] = "Tile1-WallRB.bmp";
            tileInfo4[339] = "Tile1-WallLR.bmp";
            tileInfo4[340] = "Tile1-WallLR.bmp";
            tileInfo4[341] = "Tile1-WallLR.bmp";
            tileInfo4[342] = "Tile1-WallLR.bmp";
            tileInfo4[343] = "Tile1-WallLR.bmp";
            tileInfo4[344] = "Tile1-WallTL.bmp";
            tileInfo4[345] = "Tile1-WallTR.bmp";
            tileInfo4[346] = "Tile1-WallLR.bmp";
            tileInfo4[347] = "Tile1-WallLR.bmp";
            tileInfo4[348] = "Tile1-WallTL.bmp";
            tileInfo4[349] = "Tile1-WallTB.bmp";
            tileInfo4[350] = "Tile1-WallTB.bmp";
            tileInfo4[351] = "Tile1-WallTB.bmp";
            tileInfo4[352] = "Tile1-WallTB.bmp";
            tileInfo4[353] = "Tile1-WallTR.bmp";
            tileInfo4[354] = "Tile1-WallLR.bmp";
            tileInfo4[355] = "Tile1-WallLR.bmp";
            tileInfo4[356] = "Tile1-WallLR.bmp";
            tileInfo4[357] = "Tile1-WallLB.bmp";
            tileInfo4[358] = "Tile1-WallTR.bmp";
            tileInfo4[359] = "Tile1-WallLR.bmp";
            // １２行目
            tileInfo4[360] = "Tile1-WallLR.bmp";
            tileInfo4[361] = "Tile1-WallLR.bmp";
            tileInfo4[362] = "Tile1-WallLR.bmp";
            tileInfo4[363] = "Tile1-WallLR.bmp";
            tileInfo4[364] = "Tile1-WallLR.bmp";
            tileInfo4[365] = "Tile1-WallLR.bmp";
            tileInfo4[366] = "Tile1-WallLB.bmp";
            tileInfo4[367] = "Tile1-WallTB.bmp";
            tileInfo4[368] = "Tile1-WallTB.bmp";
            tileInfo4[369] = "Tile1-WallRB.bmp";
            tileInfo4[370] = "Tile1-WallLR.bmp";
            tileInfo4[371] = "Tile1-WallLR.bmp";
            tileInfo4[372] = "Tile1-WallLR.bmp";
            tileInfo4[373] = "Tile1-WallLR.bmp";
            tileInfo4[374] = "Tile1-WallLR.bmp";
            tileInfo4[375] = "Tile1-WallLR.bmp";
            tileInfo4[376] = "Tile1-WallLR.bmp";
            tileInfo4[377] = "Tile1-WallLR.bmp";
            tileInfo4[378] = "Tile1-WallLR.bmp";
            tileInfo4[379] = "Tile1-WallTL.bmp";
            tileInfo4[380] = "Tile1-WallTB.bmp";
            tileInfo4[381] = "Tile1-WallTB.bmp";
            tileInfo4[382] = "Tile1-WallTR.bmp";
            tileInfo4[383] = "Tile1-WallLR.bmp";
            tileInfo4[384] = "Tile1-WallLR.bmp";
            tileInfo4[385] = "Tile1-WallLR.bmp";
            tileInfo4[386] = "Tile1-WallLB.bmp";
            tileInfo4[387] = "Tile1-WallTB.bmp";
            tileInfo4[388] = "Tile1-WallRB.bmp";
            tileInfo4[389] = "Tile1-WallLR.bmp";
            // １３行目
            tileInfo4[390] = "Tile1-WallLR.bmp";
            tileInfo4[391] = "Tile1-WallLR.bmp";
            tileInfo4[392] = "Tile1-WallLR.bmp";
            tileInfo4[393] = "Tile1-WallLR.bmp";
            tileInfo4[394] = "Tile1-WallLR.bmp";
            tileInfo4[395] = "Tile1-WallLB.bmp";
            tileInfo4[396] = "Tile1-WallTB.bmp";
            tileInfo4[397] = "Tile1-WallTB.bmp";
            tileInfo4[398] = "Tile1-WallTB.bmp";
            tileInfo4[399] = "Tile1-WallTB.bmp";
            tileInfo4[400] = "Tile1-WallRB.bmp";
            tileInfo4[401] = "Tile1-WallLR.bmp";
            tileInfo4[402] = "Tile1-WallLR.bmp";
            tileInfo4[403] = "Tile1-WallLR.bmp";
            tileInfo4[404] = "Tile1-WallLR.bmp";
            tileInfo4[405] = "Tile1-WallLR.bmp";
            tileInfo4[406] = "Tile1-WallLR.bmp";
            tileInfo4[407] = "Tile1-WallLR.bmp";
            tileInfo4[408] = "Tile1-WallLR.bmp";
            tileInfo4[409] = "Tile1-WallLR.bmp";
            tileInfo4[410] = "Tile1-WallTL.bmp";
            tileInfo4[411] = "Tile1-WallTR.bmp";
            tileInfo4[412] = "Tile1-WallLR.bmp";
            tileInfo4[413] = "Tile1-WallLR.bmp";
            tileInfo4[414] = "Tile1-WallLR.bmp";
            tileInfo4[415] = "Tile1-WallLR.bmp";
            tileInfo4[416] = "Tile1-WallTL.bmp";
            tileInfo4[417] = "Tile1-WallTB.bmp";
            tileInfo4[418] = "Tile1-WallTR.bmp";
            tileInfo4[419] = "Tile1-WallLR.bmp";
            // １４行目
            tileInfo4[420] = "Tile1-WallLR.bmp";
            tileInfo4[421] = "Tile1-WallLR.bmp";
            tileInfo4[422] = "Tile1-WallLR.bmp";
            tileInfo4[423] = "Tile1-WallLR.bmp";
            tileInfo4[424] = "Tile1-WallLB.bmp";
            tileInfo4[425] = "Tile1-WallTB.bmp";
            tileInfo4[426] = "Tile1-WallTB.bmp";
            tileInfo4[427] = "Tile1-WallTB.bmp";
            tileInfo4[428] = "Tile1-WallTB.bmp";
            tileInfo4[429] = "Tile1-WallTB.bmp";
            tileInfo4[430] = "Tile1-WallTB.bmp";
            tileInfo4[431] = "Tile1-WallRB.bmp";
            tileInfo4[432] = "Tile1-WallLR.bmp";
            tileInfo4[433] = "Tile1-WallLR.bmp";
            tileInfo4[434] = "Tile1-WallLR.bmp";
            tileInfo4[435] = "Tile1-WallLB.bmp";
            tileInfo4[436] = "Tile1-WallRB.bmp";
            tileInfo4[437] = "Tile1-WallLR.bmp";
            tileInfo4[438] = "Tile1-WallLR.bmp";
            tileInfo4[439] = "Tile1-WallLR.bmp";
            tileInfo4[440] = "Tile1-WallLR.bmp";
            tileInfo4[441] = "Tile1-WallLB.bmp";
            tileInfo4[442] = "Tile1-WallRB.bmp";
            tileInfo4[443] = "Tile1-WallLR.bmp";
            tileInfo4[444] = "Tile1-WallLR.bmp";
            tileInfo4[445] = "Tile1-WallLR.bmp";
            tileInfo4[446] = "Tile1-WallLR.bmp";
            tileInfo4[447] = "Tile1-WallTL.bmp";
            tileInfo4[448] = "Tile1-WallRB.bmp";
            tileInfo4[449] = "Tile1-WallLR.bmp";
            // １５行目
            tileInfo4[450] = "Tile1-WallLR.bmp";
            tileInfo4[451] = "Tile1-WallLR.bmp";
            tileInfo4[452] = "Tile1-WallLR.bmp";
            tileInfo4[453] = "Tile1-WallLB.bmp";
            tileInfo4[454] = "Tile1-WallTB.bmp";
            tileInfo4[455] = "Tile1-WallTB.bmp";
            tileInfo4[456] = "Tile1-WallTB.bmp";
            tileInfo4[457] = "Tile1-WallTB.bmp";
            tileInfo4[458] = "Tile1-WallTB.bmp";
            tileInfo4[459] = "Tile1-WallTB.bmp";
            tileInfo4[460] = "Tile1-WallTB.bmp";
            tileInfo4[461] = "Tile1-WallTB.bmp";
            tileInfo4[462] = "Tile1-WallRB.bmp";
            tileInfo4[463] = "Tile1-WallLR.bmp";
            tileInfo4[464] = "Tile1-WallLR.bmp";
            tileInfo4[465] = "Tile1-WallTL.bmp";
            tileInfo4[466] = "Tile1-WallTR.bmp";
            tileInfo4[467] = "Tile1-WallLR.bmp";
            tileInfo4[468] = "Tile1-WallLR.bmp";
            tileInfo4[469] = "Tile1-WallLR.bmp";
            tileInfo4[470] = "Tile1-WallLB.bmp";
            tileInfo4[471] = "Tile1-WallTB.bmp";
            tileInfo4[472] = "Tile1-WallTB.bmp";
            tileInfo4[473] = "Tile1-WallRB.bmp";
            tileInfo4[474] = "Tile1-WallLR.bmp";
            tileInfo4[475] = "Tile1-WallLR.bmp";
            tileInfo4[476] = "Tile1-WallLR.bmp";
            tileInfo4[477] = "Tile1-WallLB.bmp";
            tileInfo4[478] = "Tile1-WallTB.bmp";
            tileInfo4[479] = "Tile1-WallRB.bmp";
            // １６行目
            tileInfo4[480] = "Tile1-WallLR.bmp";
            tileInfo4[481] = "Tile1-WallLR.bmp";
            tileInfo4[482] = "Tile1-WallLB.bmp";
            tileInfo4[483] = "Tile1-WallTB.bmp";
            tileInfo4[484] = "Tile1-WallTB.bmp";
            tileInfo4[485] = "Tile1-WallTB.bmp";
            tileInfo4[486] = "Tile1-WallTB.bmp";
            tileInfo4[487] = "Tile1-WallTB.bmp";
            tileInfo4[488] = "Tile1-WallTB.bmp";
            tileInfo4[489] = "Tile1-WallTB.bmp";
            tileInfo4[490] = "Tile1-WallTB.bmp";
            tileInfo4[491] = "Tile1-WallTB.bmp";
            tileInfo4[492] = "Tile1-WallTB.bmp";
            tileInfo4[493] = "Tile1-WallRB.bmp";
            tileInfo4[494] = "Tile1-WallLR.bmp";
            tileInfo4[495] = "Tile1-WallLR.bmp";
            tileInfo4[496] = "Tile1-WallLR.bmp";
            tileInfo4[497] = "Tile1-WallLR.bmp";
            tileInfo4[498] = "Tile1-WallLR.bmp";
            tileInfo4[499] = "Tile1-WallLB.bmp";
            tileInfo4[500] = "Tile1-WallTB.bmp";
            tileInfo4[501] = "Tile1-WallTB.bmp";
            tileInfo4[502] = "Tile1-WallTB.bmp";
            tileInfo4[503] = "Tile1-WallTB.bmp";
            tileInfo4[504] = "Tile1-WallRB.bmp";
            tileInfo4[505] = "Tile1-WallLR.bmp";
            tileInfo4[506] = "Tile1-WallLR.bmp";
            tileInfo4[507] = "Tile1-WallTL.bmp";
            tileInfo4[508] = "Tile1-WallTB.bmp";
            tileInfo4[509] = "Tile1-WallTR.bmp";
            // １７行目
            tileInfo4[510] = "Tile1-WallLR.bmp";
            tileInfo4[511] = "Tile1-WallLB.bmp";
            tileInfo4[512] = "Tile1-WallTB.bmp";
            tileInfo4[513] = "Tile1-WallTB.bmp";
            tileInfo4[514] = "Tile1-WallTB.bmp";
            tileInfo4[515] = "Tile1-WallTB.bmp";
            tileInfo4[516] = "Tile1-WallTB.bmp";
            tileInfo4[517] = "Tile1-WallTB.bmp";
            tileInfo4[518] = "Tile1-WallTB.bmp";
            tileInfo4[519] = "Tile1-WallTB.bmp";
            tileInfo4[520] = "Tile1-WallTB.bmp";
            tileInfo4[521] = "Tile1-WallTB.bmp";
            tileInfo4[522] = "Tile1-WallTB.bmp";
            tileInfo4[523] = "Tile1-WallTR.bmp";
            tileInfo4[524] = "Tile1-WallLR.bmp";
            tileInfo4[525] = "Tile1-WallLR.bmp";
            tileInfo4[526] = "Tile1-WallLR.bmp";
            tileInfo4[527] = "Tile1-WallLR.bmp";
            tileInfo4[528] = "Tile1-WallLB.bmp";
            tileInfo4[529] = "Tile1-WallTB.bmp";
            tileInfo4[530] = "Tile1-WallTB.bmp";
            tileInfo4[531] = "Tile1-WallTB.bmp";
            tileInfo4[532] = "Tile1-WallTB.bmp";
            tileInfo4[533] = "Tile1-WallTB.bmp";
            tileInfo4[534] = "Tile1-WallTB.bmp";
            tileInfo4[535] = "Tile1-WallRB.bmp";
            tileInfo4[536] = "Tile1-WallLR.bmp";
            tileInfo4[537] = "Tile1-WallLB.bmp";
            tileInfo4[538] = "Tile1-WallTR.bmp";
            tileInfo4[539] = "Tile1-WallLR.bmp";
            // １８行目
            tileInfo4[540] = "Tile1-WallLR.bmp";
            tileInfo4[541] = "Tile1-WallTL.bmp";
            tileInfo4[542] = "Tile1-WallTR.bmp";
            tileInfo4[543] = "Tile1-WallTL.bmp";
            tileInfo4[544] = "Tile1-WallTR.bmp";
            tileInfo4[545] = "Tile1-WallTL.bmp";
            if (!we.InfoArea412)
            {
                tileInfo4[546] = "Tile1-WallTR.bmp";
            }
            else
            {
                tileInfo4[546] = "Tile1-WallT.bmp";
            }
            if (!we.InfoArea412)
            {
                tileInfo4[547] = "Tile1-WallTL.bmp";
            }
            else
            {
                tileInfo4[547] = "Tile1-WallT.bmp";
            }
            tileInfo4[548] = "Tile1-WallTB.bmp";
            tileInfo4[549] = "Tile1-WallTB.bmp";
            tileInfo4[550] = "Tile1-WallTB.bmp";
            tileInfo4[551] = "Tile1-WallTB.bmp";
            tileInfo4[552] = "Tile1-WallTB.bmp";
            tileInfo4[553] = "Tile1-WallRB.bmp";
            tileInfo4[554] = "Tile1-WallLB.bmp";
            tileInfo4[555] = "Tile1-WallRB.bmp";
            tileInfo4[556] = "Tile1-WallLR.bmp";
            tileInfo4[557] = "Tile1-WallLR.bmp";
            tileInfo4[558] = "Tile1-WallTL.bmp";
            tileInfo4[559] = "Tile1-WallTR.bmp";
            tileInfo4[560] = "Tile1-WallTL.bmp";
            tileInfo4[561] = "Tile1-WallTR.bmp";
            tileInfo4[562] = "Tile1-WallTL.bmp";
            tileInfo4[563] = "Tile1-WallTR.bmp";
            tileInfo4[564] = "Tile1-WallTL.bmp";
            tileInfo4[565] = "Tile1-WallTR.bmp";
            tileInfo4[566] = "Tile1-WallLB.bmp";
            tileInfo4[567] = "Tile1-WallTB.bmp";
            tileInfo4[568] = "Tile1-WallRB.bmp";
            tileInfo4[569] = "Tile1-WallLR.bmp";
            // １９行目
            tileInfo4[570] = "Tile1-WallLB.bmp";
            tileInfo4[571] = "Tile1-WallRB.bmp";
            tileInfo4[572] = "Tile1-WallLB.bmp";
            tileInfo4[573] = "Tile1-WallRB.bmp";
            tileInfo4[574] = "Tile1-WallLB.bmp";
            tileInfo4[575] = "Tile1-WallRB.bmp";
            tileInfo4[576] = "Upstair-WallLRB.bmp";
            tileInfo4[577] = "Tile1-WallLB.bmp";
            tileInfo4[578] = "Tile1-WallTB.bmp";
            tileInfo4[579] = "Tile1-WallTB.bmp";
            tileInfo4[580] = "Tile1-WallTB.bmp";
            tileInfo4[581] = "Tile1-WallTB.bmp";
            tileInfo4[582] = "Tile1-WallTB.bmp";
            tileInfo4[583] = "Tile1-WallTB.bmp";
            tileInfo4[584] = "Tile1-WallTB.bmp";
            tileInfo4[585] = "Tile1-WallTB.bmp";
            if (!we.InfoArea418)
            {
                tileInfo4[586] = "Tile1-WallRB.bmp";
            }
            else
            {
                tileInfo4[586] = "Tile1-WallB.bmp";
            }
            if (!we.InfoArea418)
            {
                tileInfo4[587] = "Tile1-WallLB.bmp";
            }
            else
            {
                tileInfo4[587] = "Tile1-WallB.bmp";
            }
            tileInfo4[588] = "Tile1-WallRB.bmp";
            tileInfo4[589] = "Tile1-WallLB.bmp";
            tileInfo4[590] = "Tile1-WallRB.bmp";
            tileInfo4[591] = "Tile1-WallLB.bmp";
            tileInfo4[592] = "Tile1-WallRB.bmp";
            tileInfo4[593] = "Tile1-WallLB.bmp";
            tileInfo4[594] = "Tile1-WallRB.bmp";
            tileInfo4[595] = "Tile1-WallLB.bmp";
            tileInfo4[596] = "Tile1-WallTB.bmp";
            tileInfo4[597] = "Tile1-WallTB.bmp";
            tileInfo4[598] = "Tile1-WallTB.bmp";
            tileInfo4[599] = "Tile1-WallRB.bmp";
            #endregion
            #region "ダンジョン５階"
            // ０行目
            tileInfo5[0] = "Tile1-WallTL.bmp";
            tileInfo5[1] = "Tile1-WallT.bmp";
            tileInfo5[2] = "Tile1-WallT.bmp";
            tileInfo5[3] = "Tile1-WallT.bmp";
            tileInfo5[4] = "Tile1-WallT.bmp";
            tileInfo5[5] = "Tile1-WallT.bmp";
            tileInfo5[6] = "Tile1-WallT.bmp";
            tileInfo5[7] = "Tile1-WallT.bmp";
            tileInfo5[8] = "Tile1-WallT.bmp";
            tileInfo5[9] = "Tile1-WallT.bmp";
            tileInfo5[10] = "Tile1-WallT.bmp";
            tileInfo5[11] = "Tile1-WallT.bmp";
            tileInfo5[12] = "Tile1-WallT.bmp";
            tileInfo5[13] = "Tile1-WallT.bmp";
            tileInfo5[14] = "Tile1-WallT.bmp";
            tileInfo5[15] = "Tile1-WallT.bmp";
            tileInfo5[16] = "Tile1-WallT.bmp";
            tileInfo5[17] = "Tile1-WallT.bmp";
            tileInfo5[18] = "Tile1-WallT.bmp";
            tileInfo5[19] = "Tile1-WallT.bmp";
            tileInfo5[20] = "Upstair-WallTLR.bmp";
            tileInfo5[21] = "Tile1-WallT.bmp";
            tileInfo5[22] = "Tile1-WallT.bmp";
            tileInfo5[23] = "Tile1-WallT.bmp";
            tileInfo5[24] = "Tile1-WallT.bmp";
            tileInfo5[25] = "Tile1-WallT.bmp";
            tileInfo5[26] = "Tile1-WallT.bmp";
            tileInfo5[27] = "Tile1-WallT.bmp";
            tileInfo5[28] = "Tile1-WallT.bmp";
            tileInfo5[29] = "Tile1-WallTR.bmp";
            // １行目
            tileInfo5[30] = "Tile1-WallL.bmp";
            tileInfo5[50] = "Tile1-WallLR.bmp";
            tileInfo5[59] = "Tile1-WallR.bmp";
            // ２行目
            tileInfo5[60] = "Tile1-WallL.bmp";
            tileInfo5[80] = "Tile1-WallLR.bmp";
            tileInfo5[89] = "Tile1-WallR.bmp";
            // ３行目
            tileInfo5[90] = "Tile1-WallL.bmp";
            tileInfo5[110] = "Tile1-WallLR.bmp";
            tileInfo5[119] = "Tile1-WallR.bmp";
            // ４行目
            tileInfo5[120] = "Tile1-WallL.bmp";
            tileInfo5[140] = "Tile1-WallLR.bmp";
            tileInfo5[149] = "Tile1-WallR.bmp";
            // ５行目
            tileInfo5[150] = "Tile1-WallL.bmp";
            tileInfo5[164] = "Tile1-WallTL.bmp";
            tileInfo5[165] = "Tile1-WallT.bmp";
            tileInfo5[166] = "Tile1-WallTR.bmp";
            tileInfo5[170] = "Tile1-WallLR.bmp";
            tileInfo5[179] = "Tile1-WallR.bmp";
            // ６行目
            tileInfo5[180] = "Tile1-WallTL.bmp";
            tileInfo5[181] = "Tile1-WallT.bmp";
            tileInfo5[182] = "Tile1-WallT.bmp";
            tileInfo5[183] = "Tile1-WallT.bmp";
            tileInfo5[184] = "Tile1-WallTR.bmp";
            tileInfo5[194] = "Tile1-WallL.bmp";
            tileInfo5[196] = "Tile1-WallR.bmp";
            tileInfo5[200] = "Tile1-WallLR.bmp";
            tileInfo5[209] = "Tile1-WallR.bmp";
            // ７行目
            tileInfo5[210] = "Tile1-WallL.bmp";
            tileInfo5[214] = "Tile1-WallR.bmp";
            tileInfo5[224] = "Tile1-WallLB.bmp";
            tileInfo5[225] = "Tile1-WallB.bmp";
            tileInfo5[226] = "Tile1-WallB.bmp";
            tileInfo5[227] = "Tile1-WallTB.bmp";
            tileInfo5[228] = "Tile1-WallTB.bmp";
            tileInfo5[229] = "Tile1-WallTB.bmp";
            tileInfo5[230] = "Tile1-WallLR-DummyL.bmp";
            tileInfo5[239] = "Tile1-WallR.bmp";
            // ８行目
            tileInfo5[240] = "Tile1-WallL.bmp";
            tileInfo5[244] = "Tile1-WallR.bmp";
            tileInfo5[260] = "Tile1-WallLR.bmp";
            tileInfo5[269] = "Tile1-WallR.bmp";
            // ９行目
            tileInfo5[270] = "Tile1-WallL.bmp";
            tileInfo5[275] = "Tile1-WallT.bmp";
            tileInfo5[276] = "Tile1-WallT.bmp";
            tileInfo5[277] = "Tile1-WallT.bmp";
            tileInfo5[278] = "Tile1-WallT.bmp";
            tileInfo5[279] = "Tile1-WallT.bmp";
            tileInfo5[280] = "Tile1-WallT.bmp";
            tileInfo5[281] = "Tile1-WallT.bmp";
            tileInfo5[282] = "Tile1-WallT.bmp";
            tileInfo5[283] = "Tile1-WallT.bmp";
            tileInfo5[284] = "Tile1-WallT.bmp";
            tileInfo5[285] = "Tile1-WallT.bmp";
            tileInfo5[286] = "Tile1-WallT.bmp";
            tileInfo5[287] = "Tile1-WallT.bmp";
            tileInfo5[288] = "Tile1-WallT.bmp";
            tileInfo5[289] = "Tile1-WallT.bmp";
            tileInfo5[290] = "Tile1.bmp";
            tileInfo5[291] = "Tile1-WallT.bmp";
            tileInfo5[292] = "Tile1-WallT.bmp";
            tileInfo5[293] = "Tile1-WallT.bmp";
            tileInfo5[294] = "Tile1-WallT.bmp";
            tileInfo5[295] = "Tile1-WallT.bmp";
            tileInfo5[296] = "Tile1-WallT.bmp";
            tileInfo5[297] = "Tile1-WallT.bmp";
            tileInfo5[298] = "Tile1-WallT.bmp";
            tileInfo5[299] = "Tile1-WallTR.bmp";
            // １０行目
            tileInfo5[300] = "Tile1-WallL.bmp";
            tileInfo5[305] = "Tile1-WallB.bmp";
            tileInfo5[306] = "Tile1-WallB.bmp";
            tileInfo5[307] = "Tile1-WallB.bmp";
            tileInfo5[308] = "Tile1-WallB.bmp";
            tileInfo5[309] = "Tile1-WallB.bmp";
            tileInfo5[310] = "Tile1-WallB.bmp";
            tileInfo5[311] = "Tile1-WallB.bmp";
            tileInfo5[312] = "Tile1-WallB.bmp";
            tileInfo5[313] = "Tile1-WallB.bmp";
            tileInfo5[314] = "Tile1-WallB.bmp";
            tileInfo5[315] = "Tile1-WallB.bmp";
            tileInfo5[316] = "Tile1-WallB.bmp";
            tileInfo5[317] = "Tile1-WallB.bmp";
            tileInfo5[318] = "Tile1-WallB.bmp";
            tileInfo5[319] = "Tile1-WallB.bmp";
            tileInfo5[320] = "Tile1.bmp";
            tileInfo5[321] = "Tile1-WallB.bmp";
            tileInfo5[322] = "Tile1-WallB.bmp";
            tileInfo5[323] = "Tile1-WallB.bmp";
            tileInfo5[324] = "Tile1-WallB.bmp";
            tileInfo5[325] = "Tile1-WallB.bmp";
            tileInfo5[326] = "Tile1-WallB.bmp";
            tileInfo5[327] = "Tile1-WallB.bmp";
            tileInfo5[328] = "Tile1-WallB.bmp";
            tileInfo5[329] = "Tile1-WallRB.bmp";
            // １１行目
            tileInfo5[330] = "Tile1-WallL.bmp";
            tileInfo5[334] = "Tile1-WallR.bmp";
            tileInfo5[350] = "Tile1-WallLR.bmp";
            tileInfo5[359] = "Tile1-WallR.bmp";
            // １２行目
            tileInfo5[360] = "Tile1-WallL.bmp";
            tileInfo5[364] = "Tile1-WallR.bmp";
            tileInfo5[374] = "Tile1-WallTL.bmp";
            tileInfo5[375] = "Tile1-WallT.bmp";
            tileInfo5[376] = "Tile1-WallT.bmp";
            tileInfo5[377] = "Tile1-WallTB.bmp";
            tileInfo5[378] = "Tile1-WallTB.bmp";
            tileInfo5[379] = "Tile1-WallTB.bmp";
            tileInfo5[380] = "Tile1-WallR.bmp";
            tileInfo5[389] = "Tile1-WallR.bmp";
            // １３行目
            tileInfo5[390] = "Tile1-WallLB.bmp";
            tileInfo5[391] = "Tile1-WallB.bmp";
            tileInfo5[392] = "Tile1-WallB.bmp";
            tileInfo5[393] = "Tile1-WallB.bmp";
            tileInfo5[394] = "Tile1-WallRB.bmp";
            tileInfo5[404] = "Tile1-WallL.bmp";
            tileInfo5[405] = "Tile1.bmp";
            tileInfo5[406] = "Tile1-WallR.bmp";
            tileInfo5[410] = "Tile1-WallLR.bmp";
            tileInfo5[419] = "Tile1-WallR.bmp";
            // １４行目
            tileInfo5[420] = "Tile1-WallL.bmp";
            tileInfo5[434] = "Tile1-WallLB.bmp";
            tileInfo5[435] = "Tile1-WallB.bmp";
            tileInfo5[436] = "Tile1-WallRB.bmp";
            tileInfo5[440] = "Tile1-WallLR.bmp";
            tileInfo5[449] = "Tile1-WallR.bmp";
            // １５行目
            tileInfo5[450] = "Tile1-WallL.bmp";
            tileInfo5[470] = "Tile1-WallLR.bmp";
            tileInfo5[479] = "Tile1-WallR.bmp";
            // １６行目
            tileInfo5[480] = "Tile1-WallL.bmp";
            tileInfo5[500] = "Tile1-WallLR.bmp";
            tileInfo5[509] = "Tile1-WallR.bmp";
            // １７行目
            tileInfo5[510] = "Tile1-WallL.bmp";
            tileInfo5[530] = "Tile1-WallLR.bmp";
            tileInfo5[539] = "Tile1-WallR.bmp";
            // １８行目
            tileInfo5[540] = "Tile1-WallL.bmp";
            tileInfo5[560] = "Tile1-WallLR.bmp";
            tileInfo5[569] = "Tile1-WallR.bmp";
            // １９行目
            tileInfo5[570] = "Tile1-WallLB.bmp";
            tileInfo5[571] = "Tile1-WallB.bmp";
            tileInfo5[572] = "Tile1-WallB.bmp";
            tileInfo5[573] = "Tile1-WallB.bmp";
            tileInfo5[574] = "Tile1-WallB.bmp";
            tileInfo5[575] = "Tile1-WallB.bmp";
            tileInfo5[576] = "Tile1-WallB.bmp";
            tileInfo5[577] = "Tile1-WallB.bmp";
            tileInfo5[578] = "Tile1-WallB.bmp";
            tileInfo5[579] = "Tile1-WallB.bmp";
            tileInfo5[580] = "Tile1-WallB.bmp";
            tileInfo5[581] = "Tile1-WallB.bmp";
            tileInfo5[582] = "Tile1-WallB.bmp";
            tileInfo5[583] = "Tile1-WallB.bmp";
            tileInfo5[584] = "Tile1-WallB.bmp";
            tileInfo5[585] = "Tile1-WallB.bmp";
            tileInfo5[586] = "Tile1-WallB.bmp";
            tileInfo5[587] = "Tile1-WallB.bmp";
            tileInfo5[588] = "Tile1-WallB.bmp";
            tileInfo5[589] = "Tile1-WallB.bmp";
            tileInfo5[590] = "Tile1-WallLRB.bmp";
            tileInfo5[591] = "Tile1-WallB.bmp";
            tileInfo5[592] = "Tile1-WallB.bmp";
            tileInfo5[593] = "Tile1-WallB.bmp";
            tileInfo5[594] = "Tile1-WallB.bmp";
            tileInfo5[595] = "Tile1-WallB.bmp";
            tileInfo5[596] = "Tile1-WallB.bmp";
            tileInfo5[597] = "Tile1-WallB.bmp";
            tileInfo5[598] = "Tile1-WallB.bmp";
            tileInfo5[599] = "Tile1-WallRB.bmp";

            #endregion
        }

        private void CallHomeTown()
        {
            CallHomeTown(false);
        }
        private void CallHomeTown(bool noFirstMusic)
        {
            stepCounter = 0;
            GroundOne.StopDungeonMusic();

            using (HomeTown ht = new HomeTown())
            {
                // ホームタウンに入る前は、遠見の青水晶を使ってくる場合もあるため、スタート地点へ移動しておく事とする。
                UpdatePlayerLocationInfo(basePosX + moveLen * 2, basePosY + moveLen * 1);
                SetupDungeonMapping(1);

                ht.MC = this.MC;
                ht.SC = this.SC;
                ht.TC = this.TC;
                ht.WE = this.WE;
                ht.KnownTileInfo = this.knownTileInfo;
                ht.KnownTileInfo2 = this.knownTileInfo2;
                ht.KnownTileInfo3 = this.knownTileInfo3;
                ht.KnownTileInfo4 = this.knownTileInfo4;
                ht.KnownTileInfo5 = this.knownTileInfo5;
                //ht.SoundDevice = this.soundDevice;
                ht.NoFirstMusic = noFirstMusic;
                ht.BattleSpeed = this.battleSpeed;
                ht.Difficulty = this.difficulty;

                this.Hide();
                ht.ShowDialog();
                this.Show();

                // s 後編追加（潜在バグ対応)
                this.mc = ht.MC;
                this.sc = ht.SC;
                this.tc = ht.TC;
                this.we = ht.WE;
                // e 後編追加（潜在バグ対応)

                if (ht.DialogResult == DialogResult.Retry)
                {
                    this.mc = ht.MC;
                    this.sc = ht.SC;
                    this.tc = ht.TC;
                    this.we = ht.WE;
                    this.knownTileInfo = ht.KnownTileInfo;
                    this.knownTileInfo2 = ht.KnownTileInfo2;
                    this.knownTileInfo3 = ht.KnownTileInfo3;
                    this.knownTileInfo4 = ht.KnownTileInfo4;
                    this.knownTileInfo5 = ht.KnownTileInfo5;
                    SetupPlayerStatus();
                    PreInitialize();
                }
                else if (ht.DialogResult == DialogResult.Cancel)
                {
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                }
                else
                {
                    // ダンジョンに戻ってきたため、フラグを立てる
                    this.WE.SaveByDungeon = true;
                    // ホームタウンから出てきたら、その日のコミュニケーションフラグを落とす
                    this.WE.AlreadyCommunicate = false;
                    this.WE.AlreadyEquipShop = false;
                    this.WE.AlreadyRest = false;
                    this.dayLabel.Text = we.GameDay.ToString() + "日目";
                    this.dungeonAreaLabel.Text = we.DungeonArea.ToString() + "　階";

                    switch (ht.TargetDungeon)
                    {
                        case 1:
                            UpdatePlayerLocationInfo(basePosX + moveLen * 2, basePosY + moveLen * 1);
                            break;
                        case 2:
                            UpdatePlayerLocationInfo(basePosX + moveLen * 29, basePosY + moveLen * 19);
                            break;
                        case 3:
                            UpdatePlayerLocationInfo(basePosX + moveLen * 23, basePosY + moveLen * 0);
                            break;
                        case 4:
                            UpdatePlayerLocationInfo(basePosX + moveLen * 6, basePosY + moveLen * 19);
                            break;
                        case 5:
                            UpdatePlayerLocationInfo(basePosX + moveLen * 20, basePosY + moveLen * 0);
                            break;
                    }
                    SetupDungeonMapping(ht.TargetDungeon);

                    UpdateMainMessage("", true);
                    GroundOne.PlayDungeonMusic(Database.BGM02, Database.BGM02LoopBegin);
                    SetupPlayerStatus();
                }
            }
        }

        bool keyDown = false;
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                // プレイヤーの動作を示す
                case Keys.NumPad8:
                case Keys.Up:
                    if (!keyDown)
                    {
                        UpdatePlayersKeyEvents(0);
                    }
                    break;
                case Keys.NumPad4:
                case Keys.Left:
                    if (!keyDown)
                    {
                        UpdatePlayersKeyEvents(1);
                    }
                    break;
                case Keys.NumPad6:
                case Keys.Right:
                    if (!keyDown)
                    {
                        UpdatePlayersKeyEvents(2);
                    }
                    break;
                case Keys.NumPad2:
                case Keys.Down:
                    if (!keyDown)
                    {
                        UpdatePlayersKeyEvents(3);
                    }
                    break;

                // ESCメニューを表示する
                case Keys.Escape:
                    using (ESCMenu esc = new ESCMenu())
                    {
                        esc.MC = this.MC;
                        esc.SC = this.SC;
                        esc.TC = this.TC;
                        esc.WE = this.WE;
                        esc.KnownTileInfo = this.knownTileInfo;
                        esc.KnownTileInfo2 = this.knownTileInfo2;
                        esc.KnownTileInfo3 = this.knownTileInfo3;
                        esc.KnownTileInfo4 = this.knownTileInfo4;
                        esc.KnownTileInfo5 = this.knownTileInfo5;
                        esc.StartPosition = FormStartPosition.CenterParent;
                        esc.ShowDialog(this);
                        if (esc.DialogResult == DialogResult.Abort)
                        {
                            CallHomeTown();
                        }
                        else if (esc.DialogResult == DialogResult.Retry)
                        {
                            this.mc = esc.MC;
                            this.sc = esc.SC;
                            this.tc = esc.TC;
                            this.we = esc.WE;
                            this.knownTileInfo = esc.KnownTileInfo;
                            this.knownTileInfo2 = esc.KnownTileInfo2;
                            this.knownTileInfo3 = esc.KnownTileInfo3;
                            this.knownTileInfo4 = esc.KnownTileInfo4;
                            this.knownTileInfo5 = esc.KnownTileInfo5;

                            PreInitialize();
                        }
                        else if (esc.DialogResult == DialogResult.Cancel)
                        {
                            this.DialogResult = DialogResult.Cancel;
                        }
                    }
                    break;
            }
        }

        private void UpdatePlayersKeyEvents(int direction)
        {
            int moveX = 0;
            int moveY = 0;

            //keyDown = true; [警告]：開発途中で戦闘終了後、イベント発生後などでキーダウンで効かない場合があった。押下しっぱなしだと進められるように仕様変更となるので、別の不具合が出た場合はまた再検討してください。
            if (CheckWall(direction))
            {
                System.Threading.Thread.Sleep(200);
                return;
            }

            if (direction == 0) moveY = -moveLen;
            else if (direction == 1) moveX = -moveLen;
            else if (direction == 2) moveX = moveLen;
            else if (direction == 3) moveY = moveLen;
                 
            this.Player.Visible = false;
            UpdatePlayerLocationInfo(this.Player.Location.X + moveX, this.Player.Location.Y + moveY, false);
            this.Player.Visible = true;
            bool lowSpeed = UpdateUnknownTile();
            dungeonField.Invalidate();
            this.Update();
            ExecSomeEvents();

            if (lowSpeed)
            {
                System.Threading.Thread.Sleep(200);
            }
            else
            {
                System.Threading.Thread.Sleep(100);
            }
        }

        private void UpdatePlayerLocationInfo(int x, int y)
        {
            UpdatePlayerLocationInfo(x, y, true);
        }
        private void UpdatePlayerLocationInfo(int x, int y, bool noSound)
        {
            we.DungeonPosX = x;
            we.DungeonPosX2 = x;
            we.DungeonPosY = y;
            we.DungeonPosY2 = y;
            this.Player.Visible = false;
            this.Player.Location = new Point(x, y);
            this.Player.Visible = true;
            if (!noSound)
            {
                //GroundOne.PlaySoundEffect("footstep.mp3");
            }
        }

        private void ExecSomeEvents()
        {
            bool detectEvent = false;
            using (OKRequest ok = new OKRequest())
            {
                ok.StartPosition = FormStartPosition.Manual;
                ok.Location = new Point(this.Location.X + 540, this.Location.Y + 440);

                #region "ダンジョン１階イベント"
                if (we.DungeonArea == 1)
                {
                    for (int ii = 0; ii < 9; ii++)
                    {
                        if (CheckTriggeredEvent(ii))
                        {
                            detectEvent = true;
                            switch (ii)
                            {
                                case 0:
                                    if (!we.TruthWord1)
                                    {
                                        mainMessage.Text = "アイン：看板があるな・・・なになに？";
                                        ok.ShowDialog();
                                        mainMessage.Text = "　　　　『真実の言葉１：　　俺達はまだダンジョンの中にいる』";
                                        ok.ShowDialog();
                                        mainMessage.Text = "アイン：誰だ、こんな当たり前の事を書いたやつは。";
                                        we.TruthWord1 = true;
                                    }
                                    else
                                    {
                                        UpdateMainMessage("　　　　『真実の言葉１：　　俺達はまだダンジョンの中にいる』", true);
                                    }
                                    return;

                                case 1:
                                    this.Update();
                                    mainMessage.Text = "アイン：ボスとの戦闘だ！気を引き締めていくぜ！";
                                    mainMessage.Update();
                                    bool result = EncountBattle("一階の守護者：絡みつくフランシス");
                                    if (!result)
                                    {
                                        UpdatePlayerLocationInfo(this.Player.Location.X, this.Player.Location.Y - moveLen);
                                    }
                                    else
                                    {
                                        we.CompleteSlayBoss1 = true;
                                    }
                                    mainMessage.Text = "";
                                    return;

                                case 2:
                                    we.Treasure1 = GetTreasure("ブルーマテリアル");
                                    return;

                                case 3:
                                    we.Treasure2 = GetTreasure("チャクラオーブ");
                                    return;

                                case 4:
                                    we.Treasure3 = GetTreasure("炎授天使の護符");
                                    return;

                                case 5:
                                    mainMessage.Text = "アイン：ユングの町に戻るか？";
                                    using (YesNoRequestMini ynr = new YesNoRequestMini())
                                    {
                                        ynr.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                        ynr.ShowDialog();
                                        if (ynr.DialogResult == DialogResult.Yes)
                                        {
                                            CallHomeTown();
                                            mainMessage.Text = "";
                                        }
                                        else
                                        {
                                            mainMessage.Text = "";
                                        }
                                    }
                                    return;

                                case 6:
                                    mainMessage.Text = "アイン：下り階段発見！さっそく降りるとするか？";
                                    using (YesNoRequestMini ynr = new YesNoRequestMini())
                                    {
                                        bool tempCompleteArea1 = we.CompleteArea1;
                                        ynr.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                        ynr.ShowDialog();
                                        if (ynr.DialogResult == DialogResult.Yes)
                                        {
                                            SetupDungeonMapping(2);
                                            mainMessage.Text = "";
                                            if (!tempCompleteArea1)
                                            {
                                                mainMessage.Text = "アイン：おし、１階制覇した事だし、一度ユングの町へ戻るとするか。";
                                                ok.ShowDialog();
                                                CallHomeTown();
                                            }
                                        }
                                        else
                                        {
                                            mainMessage.Text = "";
                                        }

                                    }
                                    return;

                                case 7:
                                    if (!we.InfoArea11)
                                    {
                                        UpdateMainMessage("アイン：この壁・・・やはりそうか。");

                                        UpdateMainMessage("アイン：色が若干変色しているのは劣化のせいじゃねえ。");

                                        UpdateMainMessage("アイン：コイツは意図的に隠された場所って事か・・・");

                                        UpdateMainMessage("アイン：っしゃ、先に進んでみるか。何かあるかも知れねえな。");

                                        we.InfoArea11 = true;
                                    }
                                    return;

                                case 8:
                                    if (!we.SpecialInfo1)
                                    {
                                        UpdateMainMessage("アイン：・・・ん？何だ、何もねえじゃねえか・・・くそ。");

                                        GroundOne.StopDungeonMusic();
                                        this.BackColor = Color.Black;
                                        UpdateMainMessage("　　　【その瞬間、アインの脳裏に激しい激痛が襲った！周囲の感覚が麻痺する！！】");

                                        UpdateMainMessage("　　＜＜＜　力こそが全てである ＞＞＞");

                                        UpdateMainMessage("アイン：っな・・・なん・・・だ・・・これ・・・・・・");

                                        UpdateMainMessage("　　＜＜＜　力は力にあらず、力は全てである　＞＞＞");

                                        UpdateMainMessage("アイン：うっ・・・せええ・・・全てって・・・ッイツツツ、頭が・・・");

                                        UpdateMainMessage("　　＜＜＜　負けられない勝負。　しかし心は満たず。　＞＞＞");

                                        UpdateMainMessage("アイン：し、知るか・・・んだよ、・・・・　イテテテ・・・");

                                        UpdateMainMessage("　　＜＜＜　力のみに依存するな。心を対にせよ。　＞＞＞");

                                        UpdateMainMessage("アイン：こ・・・心だと？・・・ッグ！！");

                                        UpdateMainMessage("　　　【アインに対する激しい激痛は少しずつ引いていった。】");

                                        this.BackColor = Color.RoyalBlue;

                                        UpdateMainMessage("アイン：・・・ッグ・・・くっそ、いってえ・・・");

                                        UpdateMainMessage("アイン：何だったんだ、ここは。何もねえくせに。");

                                        UpdateMainMessage("アイン：「力は力にあらず」とか・・・っけ、何言ってやがる。");

                                        UpdateMainMessage("アイン：「力こそが全てだ」言葉通りの意味だろ。");

                                        UpdateMainMessage("アイン：ったく・・・俺に心がねえとでも思ってんのか。");

                                        UpdateMainMessage("アイン：・・・まあ、こんな所でひとり言を言っててもしょうがねえ。");

                                        UpdateMainMessage("アイン：収穫はなしっと。さて、戻ってダンジョンを進めるとするか。");

                                        GroundOne.PlayDungeonMusic(Database.BGM02, Database.BGM02LoopBegin);
                                        we.SpecialInfo1 = true;
                                    }
                                    return;

                                default:
                                    mainMessage.Text = "アイン：ん？特に何もなかったと思うが。";
                                    return;
                            }
                        }
                    }
                }
                #endregion
                #region "ダンジョン２階イベント"
                else if (we.DungeonArea == 2)
                {
                    for (int ii = 0; ii < 47; ii++)
                    {
                        if (CheckTriggeredEvent(ii))
                        {
                            detectEvent = true;
                            switch (ii)
                            {
                                case 0:
                                    mainMessage.Text = "アイン：１階へ戻る階段だな。ここは一旦戻るか？";
                                    using (YesNoRequestMini ynr = new YesNoRequestMini())
                                    {
                                        ynr.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                        ynr.ShowDialog();
                                        if (ynr.DialogResult == DialogResult.Yes)
                                        {
                                            SetupDungeonMapping(1);
                                            mainMessage.Text = "";
                                        }
                                        else
                                        {
                                            mainMessage.Text = "";
                                        }
                                    }
                                    return;
                                case 1:
                                    if (!we.TruthWord2)
                                    {
                                        mainMessage.Text = "アイン：看板があるな・・・なになに？";
                                        ok.ShowDialog();
                                        mainMessage.Text = "　　　　『真実の言葉２：　　願いがかなう場所へ、願いが終わる場所へ』";
                                        ok.ShowDialog();
                                        mainMessage.Text = "アイン：最深部へ到達できりゃ本望だな。";
                                        we.TruthWord2 = true;
                                    }
                                    else
                                    {
                                        UpdateMainMessage("　　　　『真実の言葉２：　　願いがかなう場所へ、願いが終わる場所へ』", true);
                                    }
                                    return;
                                case 2:
                                    mainMessage.Text = "アイン：よっしゃ３階への階段！降りてみようぜ？";
                                    using (YesNoRequestMini ynr = new YesNoRequestMini())
                                    {
                                        bool tempCompleteArea2 = we.CompleteArea2;
                                        ynr.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                        ynr.ShowDialog();
                                        if (ynr.DialogResult == DialogResult.Yes)
                                        {
                                            SetupDungeonMapping(3);
                                            mainMessage.Text = "";
                                            if (!tempCompleteArea2)
                                            {
                                                UpdateMainMessage("アイン：おし、２階制覇だぜ！ラナ、いったんユングの町へ戻るか？");

                                                UpdateMainMessage("ラナ：そうね、３階を進みたい気持ちも分かるけど、いったん戻りましょ。");

                                                UpdateMainMessage("アイン：っしゃ、じゃあ使うぜ、遠見の青水晶！");
                                                CallHomeTown();
                                            }
                                        }
                                        else
                                        {
                                            mainMessage.Text = "";
                                        }
                                    }
                                    return;
                                case 3:
                                    this.Update();
                                    mainMessage.Text = "アイン：ボスとの戦闘だ！気を引き締めていくぜ！";
                                    mainMessage.Update();
                                    bool result = EncountBattle("二階の守護者：Lizenos");
                                    if (!result)
                                    {
                                        UpdatePlayerLocationInfo(this.Player.Location.X, this.Player.Location.Y + moveLen);
                                    }
                                    else
                                    {
                                        we.CompleteSlayBoss2 = true;
                                    }
                                    mainMessage.Text = "";
                                    return;

                                case 4:
                                    we.Treasure4 = GetTreasure("身かわしのマント");
                                    return;

                                case 5:
                                    we.Treasure5 = GetTreasure("神聖水");
                                    return;

                                case 6:
                                    we.Treasure6 = GetTreasure("鷹の刻印");
                                    return;

                                case 7:
                                    we.Treasure7 = GetTreasure("真鍮の鎧");
                                    return;

                                case 8: // 一旦戻る
                                    #region "看板突破イベント 2-1"
                                    if (!we.SolveArea21)
                                    {
                                        if (!we.InfoArea21)
                                        {
                                            mainMessage.Text = "アイン：看板があるな・・・なになに？";
                                            ok.ShowDialog();
                                            mainMessage.Text = "　　　　『この先、行き止まり。　一旦、立ち去るがよい。』";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：マジかよ。だったらもうココが最下層だってのか？";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：何言ってんのよ。きっと仕掛けがあるのよ。まずは探索しましょ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：オーケー！";
                                            we.InfoArea21 = true;
                                        }
                                        else
                                        {
                                            mainMessage.Text = "　　　　『この先、行き止まり。　一旦、立ち去るがよい。』";
                                        }
                                    }
                                    else
                                    {
                                        if (!we.CompleteArea21)
                                        {
                                            mainMessage.Text = "　　　　『この先、左へ進むべし。上へ進むべからず。』";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：！？おい、ラナ。この看板、文字が変わってるぞ！";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：やっぱりそうよ。一旦戻れって意味だったのよ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：そうだったのか。いや、助かったぜ、ホント。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：でも伝説のFiveSeekerは１日で制覇したのよね。どうやったのかしら。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：カール爵の事だ。一人だけ一旦ユングの町へ戻ったんじゃねえか？";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：なるほど、全員で立ち去れとは一言も書いてないわね。冴えてるじゃない、アイン。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：それを初見で見抜いてんだよな。やっぱとんでもねえ人だよ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：確かにそうね・・・私は探索が先って考えちゃったもんね。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：いいや、それが普通だろ。まあ、進めるんだ！先へ進もうぜ！！";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：じゃ、看板の通り、左に進もうか♪";
                                            we.CompleteArea21 = true;
                                            ConstructDungeonMap();
                                            SetupDungeonMapping(2);
                                        }
                                        else
                                        {
                                            if (!we.CompleteArea26)
                                            {
                                                mainMessage.Text = "　　　　『この先、左へ進むべし。上へ進むべからず。』";
                                            }
                                            else
                                            {
                                                mainMessage.Text = "看板はもう無くなっている。";
                                            }
                                        }
                                    }
                                    return;
                                    #endregion

                                case 9: // 剣と杖
                                    #region "看板突破イベント 2-2"
                                    if (!we.SolveArea22)
                                    {
                                        if (!we.InfoArea22)
                                        {
                                            mainMessage.Text = "アイン：また、看板があるぜ・・・なになに？";
                                            ok.ShowDialog();
                                            mainMessage.Text = "　　　　『この先、行き止まり。　各々の強さを示せ。　』";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：おっしゃ！任せておけって！！";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：バカが調子に乗り始めたわね・・・まあお手並み拝見よ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：オラァ！ストレート・スマッシュ！！！";
                                            ok.ShowDialog();
                                            mainMessage.Text = "　　　　『シュッ、ドオオォォオン！！！』";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：・・・・・・";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：何も起きないわね。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：まあ、任せておけって・・・ダブル・スラッシュ！！！";
                                            ok.ShowDialog();
                                            mainMessage.Text = "　　　　『ドゴォォン！　バキィイン！！』";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：・・・木製の看板なのに、なんで壊れないんだ？";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：そういう意味の強さじゃなさそうね。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：じゃあ、何だって言うんだ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：そこまで分からないわよ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：まあ、お手上げだぜ。やっぱり探索と行かねえか？";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：そうね。やっぱり一度回ってみるとしますか。";
                                            ok.ShowDialog();
                                            we.InfoArea22 = true;
                                        }
                                        else
                                        {
                                            mainMessage.Text = "　　　　『この先、行き止まり。　各々の強さを示せ。　』";
                                        }
                                    }
                                    else
                                    {
                                        if (!we.CompleteArea22)
                                        {
                                            mainMessage.Text = "　　　　『この先、上へ進むべし。左へ進むべからず。　』";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：おっしゃ！どうやら、ココもクリアみてえだな。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：ところで、アイン・・・";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：なんだよ？";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：上へ進めって書いてあるけど。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：なんだ、左へ行ってみたいのか？";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：指示通り進むのが正しいのかどうか、悩まない？";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：何で悩むんだ？";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：その看板が私達を罠へ落とすためっていう可能性は考えないの？";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：考えてるさ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：ウソ、それで悩まないなんて、あんたバカじゃないの？";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：ラナ、お前見切れるのか？";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：え？ええっとと、見切れるワケじゃないけど。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：だったら上だ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：・・・え、何でそうなるわけ？";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：・・・ッハッハッハッハ！";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：っな、何よ突然笑い出して、気持ち悪いわね。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：考えても無駄って事さ。せっかく上って言ってんだ、上で良いだろ？";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：ん、まあそう言われるとそうね。ッフフ、たまには良いこというわね。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：さあ、とっとと行くぜ！";
                                            ok.ShowDialog();
                                            we.CompleteArea22 = true;
                                            ConstructDungeonMap();
                                            SetupDungeonMapping(2);
                                        }
                                        else
                                        {
                                            if (!we.CompleteArea26)
                                            {
                                                mainMessage.Text = "　　　　『この先、上へ進むべし。左へ進むべからず。　』";
                                            }
                                            else
                                            {
                                                mainMessage.Text = "看板はもう無くなっている。";
                                            }
                                        }
                                    }
                                    return;
                                    #endregion

                                case 10: // 剣
                                    #region "看板突破イベント 2-2剣"
                                    if (!we.SolveArea221)
                                    {
                                        if (!we.InfoArea221)
                                        {
                                            mainMessage.Text = "ラナ：アイン、ちょっとちょっと。何かあるわよ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：ん？どれどれ、何だこりゃ。剣マークの看板に、０～９の数字が並んでるな。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：ビシビシっと。お、何か数字が入力されていくぜ。８２５０・・・";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：ちょっと、勝手に何でも触らないでよね。トラップかもしれないのに。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：普通こういうのがあったら押してしまうだろ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：だからトラップなんでしょうが、気をつけてよ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：何かの数字を入力するんだろうな・・・";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：ちょっとこれだけじゃ分かんないわね。もう少し探してみましょ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：まあ、待てよ。天才のオレ様が初見でクリアしてやるぜ！";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：バカの初見とやら、お手並み拝見と行きますか♪";
                                            ok.ShowDialog();
                                            using (RequestInput ri = new RequestInput())
                                            {
                                                ri.StartPosition = FormStartPosition.CenterParent;
                                                ri.ShowDialog();
                                                if (ri.InputData == mc.Strength.ToString())
                                                {
                                                    // [警告]：ここで１回目クリアしている場合、新しいフラグを立ててください。
                                                    mainMessage.Text = "　　　　『ッゴゴゴゴゴ・・・ガコン！』";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：見ろ！！剣マークが無くなったぜ！！";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：ウソ・・・ウソでしょ！？";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：ッハッハッハ！";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：へえ、やるじゃない。大したものね。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：剣マークはどう考えても腕力だろ。つまり今の自分の力を示せば良いのさ。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：なるほど、今のアインの力値は、" + mc.Strength.ToString() + "ね。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：そういう事だ。っさ、他に何かないか見て回ろうぜ。";
                                                    ok.ShowDialog();
                                                    we.SolveArea221 = true;
                                                    if (we.SolveArea222) we.SolveArea22 = true;
                                                }
                                                else
                                                {
                                                    mainMessage.Text = "　　　　『ッブブー！！』";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：・・・";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：っさ、探索探索♪";
                                                    ok.ShowDialog();
                                                }
                                            }
                                            we.InfoArea221 = true;
                                        }
                                        else
                                        {
                                            mainMessage.Text = "　　　　『剣マークの看板が立ててある。下に０～９の数字が並んでいる。』";
                                            using (RequestInput ri = new RequestInput())
                                            {
                                                ri.StartPosition = FormStartPosition.CenterParent;
                                                ri.ShowDialog();
                                                if (ri.InputData == mc.Strength.ToString())
                                                {
                                                    mainMessage.Text = "　　　　『ッゴゴゴゴゴ・・・ガコン！』";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：見ろ！！剣マークが無くなったぜ！！";
                                                    ok.ShowDialog();
                                                    if (!we.FailArea221)
                                                    {
                                                        mainMessage.Text = "ラナ：へえ、大したものね。一体何だったのよ？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：剣マークはどう考えても腕力だろ。つまり今の自分の力を示せば良いのさ。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：なるほど、今のアインの力値は、" + mc.Strength.ToString() + "ね。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：そういう事だ。っさ、他に何かないか見て回ろうぜ。";
                                                        ok.ShowDialog();
                                                    }
                                                    else
                                                    {
                                                        mainMessage.Text = "ラナ：ホラ、やっぱり力値でビンゴだったわね♪";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：剣マークは直接攻撃力じゃねえのかよ？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：直接攻撃力は武器の最小値と最大値があって、不定になるでしょ？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：剣マークはストレート・スマッシュじゃねえのかよ？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：ストレート・スマッシュは技値も影響してそうじゃない？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：剣マークは剣じゃねえのかよ！？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：よし、そんなに私のキックが食らいたいって事ね♪";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：い、いやいや！ライトニング・キックは勘弁な。分かった！力値でビンゴ！";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：実際ビンゴだったんだしさ。次行きましょ♪";
                                                        ok.ShowDialog();
                                                    }
                                                    we.SolveArea221 = true;
                                                    if (we.SolveArea222) we.SolveArea22 = true;
                                                }
                                                else
                                                {
                                                    if (we.FailArea221)
                                                    {
                                                        mainMessage.Text = "　　　　『ッブブー！！』";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：・・・キックで“激痛”を味わいたい？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：・・・" + mc.Strength.ToString() + "だな。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：早く解いちゃってよ。";
                                                        ok.ShowDialog();
                                                    }
                                                    else
                                                    {
                                                        mainMessage.Text = "　　　　『ッブブー！！』";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：っくそ。オラァ！ストレート・スマッシュ！！";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "　　　　『ッブー！！　ッブブー！！！』";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：うぉ！意外と反応すんのかよ、コレ！";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：剣マークなんだから、やっぱり力関係だと思わない？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：まあ、そんな気はしてるんだがな。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：アイン、あなた今のステータスを見て力値はいくつなわけ？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：今は、" + mc.Strength.ToString() + "って所だな。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：単純にそれじゃない？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：ッハッハッハ。俺もそう思った所だぜ、任せときな！";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：・・・ほんとバカ・・・";
                                                        ok.ShowDialog();
                                                        we.FailArea221 = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        mainMessage.Text = "　　　　『剣マークの看板はもう無くなっている。　』";
                                    }
                                    return;
                                    #endregion

                                case 11: // 杖
                                    #region "看板突破イベント 2-2杖"
                                    if (!we.SolveArea222)
                                    {
                                        if (!we.InfoArea222)
                                        {
                                            mainMessage.Text = "ラナ：アイン、ここに何かあるわよ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：お、何だこりゃ。杖マークの看板の下に０～９の数字が並んでるぜ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：さっそく入力といくか！７７７・・・";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：何勝手に操作してるのよ！トラップでドガシャアアァァァン！！ってなったらどうするの？";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：それはその時だ。ッハッハッハ！";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：・・・死ぬわよ、ホントに。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：ま、まあ見ろ見ろ、この⇒を押さなきゃ大丈夫だって。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：なるほどね、でも入れる値が分からない以上どうしようもないんじゃない？探索しましょ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：まあ待て。このオレが初見で見事に正解を当ててみせるぜ！";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：バカがどこまで解析できてるか見物だわ、お手並み拝見♪";
                                            ok.ShowDialog();
                                            using (RequestInput ri = new RequestInput())
                                            {
                                                ri.StartPosition = FormStartPosition.CenterParent;
                                                ri.ShowDialog();
                                                if (ri.InputData == mc.Intelligence.ToString())
                                                {
                                                    // [警告]：ここで１回目クリアしている場合、新しいフラグを立ててください。
                                                    mainMessage.Text = "　　　　『ッゴゴゴゴゴ・・・ガコン！』";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：杖マークが消滅っと。オッケー！";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：えーーーーーーーえぇ！？";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：ッハッハッハ！！";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：ッく・・・どうなってんのよ。アインに限ってありえないわ。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：杖マークと言ったら、魔力だろ。違うか？今の俺は、" + mc.Intelligence.ToString() + "だ。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：なるほど、つまり魔力の元となるアインの知力値を入力したわけね。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：そういう事だ。っさ、他に何かあるかも知れねえ。見て回ろうぜ。";
                                                    ok.ShowDialog();
                                                    we.SolveArea222 = true;
                                                    if (we.SolveArea221) we.SolveArea22 = true;
                                                }
                                                else
                                                {
                                                    mainMessage.Text = "　　　　『ッブブー！！』";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：・・・";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：っさ、探索探索♪";
                                                    ok.ShowDialog();
                                                }
                                            }
                                            we.InfoArea222 = true;
                                        }
                                        else
                                        {
                                            mainMessage.Text = "　　　　『杖マークの看板が立ててある。下に０～９の数字が並んでいる。』";
                                            using (RequestInput ri = new RequestInput())
                                            {
                                                ri.StartPosition = FormStartPosition.CenterParent;
                                                ri.ShowDialog();
                                                if (ri.InputData == mc.Intelligence.ToString())
                                                {
                                                    mainMessage.Text = "　　　　『ッゴゴゴゴゴ・・・ガコン！』";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：見ろ！！杖マークが無くなったぜ！！";
                                                    ok.ShowDialog();
                                                    if (!we.FailArea222)
                                                    {
                                                        mainMessage.Text = "ラナ：へえ、大したものね。一体何だったのよ？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：杖マークはどう考えても魔力の証だ。つまり今の自分の知力を示せば良いのさ。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：なるほど、今のアインの知力は、" + mc.Intelligence.ToString() + "ね。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：そういう事だ。っさ、他に何かないか見て回ろうぜ。";
                                                        ok.ShowDialog();
                                                    }
                                                    else
                                                    {
                                                        mainMessage.Text = "ラナ：ホラ、やっぱり知力でビンゴだったわね♪";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：杖マークじゃなくて脳みそマークじゃねえか？普通なら。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：絵柄を見て間違える人も居るからじゃない？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：杖マークを知力だと分からないヤツだっているだろ？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：それ、アインだけじゃない？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：っち・・・そういう事にしておいてやるぜ。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：ワケわかんない所で意地はらないで、ホラホラ次行きましょ♪";
                                                        ok.ShowDialog();
                                                    }
                                                    we.SolveArea222 = true;
                                                    if (we.SolveArea221) we.SolveArea22 = true;
                                                }
                                                else
                                                {
                                                    if (we.FailArea222)
                                                    {
                                                        mainMessage.Text = "　　　　『ッブブー！！』";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：・・・撃つわよ、ダーク・ブラスト。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：お・・・オーケー、" + mc.Intelligence.ToString() + "だったな。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：早く解いちゃってよ。";
                                                        ok.ShowDialog();
                                                    }
                                                    else
                                                    {
                                                        mainMessage.Text = "　　　　『ッブブー！！』";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：っくそ。オラァ！ファイア・ボール！！";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "　　　　『ッブブブー！！　最大警告！！！　ブブーー！！』";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：うぉ！意外と反応すんのかよ、コレ！";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：杖マークなんだから、やっぱり知力関係だと思わない？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：まあ、そんな気はしてるんだがな。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：アイン、あなた今のステータスを見て知力はいくつなわけ？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：今は、" + mc.Intelligence.ToString() + "って所だな。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：単純にそれじゃない？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：ッハッハッハ。俺もそう思った所だぜ、任せときな！";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：ホント、しっかりしてよね。";
                                                        ok.ShowDialog();
                                                        we.FailArea222 = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        mainMessage.Text = "　　　　『杖マークの看板はもう無くなっている。　』";
                                    }
                                    return;
                                    #endregion

                                case 12: // １０問クイズ
                                    #region "看板突破イベント 2-3"
                                   if (!we.SolveArea23)
                                   {
                                       if (!we.InfoArea23)
                                       {
                                           mainMessage.Text = "アイン：例の看板のお出ましだな・・・なになに？";
                                           ok.ShowDialog();
                                           mainMessage.Text = "　　　　『この先、行き止まり。　汝自身に問いかけよ。　』";
                                           ok.ShowDialog();
                                           mainMessage.Text = "アイン：おい、問いかければ良いみたいだぜ、楽勝だな！";
                                           ok.ShowDialog();
                                           mainMessage.Text = "ラナ：どう楽勝なのよ。やってみせて。";
                                           ok.ShowDialog();
                                           mainMessage.Text = "アイン：オイ、そこの俺！！";
                                           ok.ShowDialog();
                                           mainMessage.Text = "アイン：・・・・・・";
                                           ok.ShowDialog();
                                           mainMessage.Text = "ラナ：っちょ・・・";
                                           ok.ShowDialog();
                                           mainMessage.Text = "アイン：・・・";
                                           ok.ShowDialog();
                                           mainMessage.Text = "ラナ：・・・ップ。アッハハハハ！バカじゃないのホントに、ッフフ、アハハハ♪";
                                           ok.ShowDialog();
                                           mainMessage.Text = "アイン：・・・じゃどうしろと！？";
                                           ok.ShowDialog();
                                           mainMessage.Text = "ラナ：いや、それは分かんないけど、あまりにもバカ過ぎて、あ～お腹痛♪";
                                           ok.ShowDialog();
                                           mainMessage.Text = "アイン：ったく、さすがに意味不明になってきたな。少し探索でもするか。";
                                           ok.ShowDialog();
                                           mainMessage.Text = "ラナ：アッハハハハ、そうね。ップ・・・行きま・・・ップ、フフフ。";
                                           ok.ShowDialog();
                                           mainMessage.Text = "アイン：くそっ、いつかこの借りは必ず返すぜ。覚えてろよ。";
                                           ok.ShowDialog();
                                           we.InfoArea23 = true;
                                       }
                                       else
                                       {
                                           int success = 0;
                                           mainMessage.Text = "　　　　『この先、行き止まり。　汝自身に問いかけよ。　』";
                                           ok.ShowDialog();
                                           if (!we.FailArea23)
                                           {
                                               mainMessage.Text = "アイン：・・・・・・";
                                               ok.ShowDialog();
                                               mainMessage.Text = "ラナ：どうしたのよ、いきなり黙ったまま突っ立ったままで。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：今まで言葉通りのものばかりだよな？";
                                               ok.ShowDialog();
                                               mainMessage.Text = "ラナ：まあそうね、変な文字かけクイズや、なぞなぞ系でもないわ。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：言葉通り行くとすれば、やっぱ自分に問いかけるしかねえわけだ。そこでだ。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：ラナ、俺に関する事、何でも良いから聞いてみてくれよ。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "ラナ：そんなので道が開けるの？";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：さあな、でもやってみるしかないだろ。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "ラナ：うーん、アインに関する事ね？分かった、じゃ行くわよ。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：ああ、何でも来いってんだ！";
                                               ok.ShowDialog();
                                           }
                                           else
                                           {
                                               mainMessage.Text = "ラナ：アイン、頼んだわよ。アナタがちゃんと答えれば道は開けるはずよ。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：ああ、今度こそやってやるぜ！";
                                               ok.ShowDialog();
                                           }
                                           mainMessage.Text = "ラナ：【１問目】アイン、あなたの師匠の名前は？";
                                           ok.ShowDialog();
                                           using (SelectAction sa = new SelectAction())
                                           {
                                               sa.StartPosition = FormStartPosition.CenterParent;
                                               sa.ForceChangeWidth = 300;
                                               sa.ElementA = "ガンツ・ギメルガ";
                                               sa.ElementB = "オル・ランディス";
                                               sa.ElementC = "ラナ・アミリア";
                                               sa.ShowDialog();
                                               if (sa.TargetNum == 1)
                                               {
                                                   mainMessage.Text = "ラナ：正解ね♪";
                                                   ok.ShowDialog();
                                                   success++;
                                               }
                                               else if (sa.TargetNum == 2)
                                               {
                                                   mainMessage.Text = "ラナ：まー当然の事よね♪　今の場合、ハズレだけど。";
                                                   ok.ShowDialog();
                                               }
                                               else
                                               {
                                                   mainMessage.Text = "ラナ：ハズレよ。あんたバカじゃない？";
                                                   ok.ShowDialog();
                                               }
                                           }
                                           mainMessage.Text = "ラナ：【２問目】アインが今装備している武器は何という名前？";
                                           ok.ShowDialog();
                                           using (SelectAction sa = new SelectAction())
                                           {
                                               sa.StartPosition = FormStartPosition.CenterParent;
                                               sa.ForceChangeWidth = 300;
                                               sa.ElementA = mc.MainWeapon.Name;
                                               sa.ElementB = "極剣  ゼムルギアス";
                                               sa.ElementC = "プラチナソード";
                                               sa.ShowDialog();
                                               if (sa.TargetNum == 0)
                                               {
                                                   mainMessage.Text = "ラナ：正解ね♪";
                                                   ok.ShowDialog();
                                                   success++;
                                               }
                                               else
                                               {
                                                   mainMessage.Text = "ラナ：ハズレよ。あんたバカじゃない？";
                                                   ok.ShowDialog();
                                               }
                                           }
                                           mainMessage.Text = "ラナ：【３問目】アインがＬＶ５の時、習得したのは何？";
                                           ok.ShowDialog();
                                           using (SelectAction sa = new SelectAction())
                                           {
                                               sa.StartPosition = FormStartPosition.CenterParent;
                                               sa.ForceChangeWidth = 300;
                                               sa.ElementA = "ファイア・ボール";
                                               sa.ElementB = "ストレート・スマッシュ";
                                               sa.ElementC = "リザレクション";
                                               sa.ShowDialog();
                                               if (sa.TargetNum == 0)
                                               {
                                                   mainMessage.Text = "ラナ：正解ね♪";
                                                   ok.ShowDialog();
                                                   success++;
                                               }
                                               else
                                               {
                                                   mainMessage.Text = "ラナ：ハズレよ。あんたバカじゃない？";
                                                   ok.ShowDialog();
                                               }
                                           }
                                           mainMessage.Text = "ラナ：【４問目】アインはどちらかと言えば・・・？";
                                           ok.ShowDialog();
                                           using (SelectAction sa = new SelectAction())
                                           {
                                               sa.StartPosition = FormStartPosition.CenterParent;
                                               sa.ForceChangeWidth = 300;
                                               sa.ElementA = "テクニック派";
                                               sa.ElementB = "魔法攻撃派";
                                               sa.ElementC = "直接攻撃派";
                                               sa.ShowDialog();
                                               if (sa.TargetNum == 0)
                                               {
                                                   if (mc.Agility >= mc.Strength && mc.Agility >= mc.Intelligence)
                                                   {
                                                       mainMessage.Text = "ラナ：正解ね♪";
                                                       ok.ShowDialog();
                                                       success++;
                                                   }
                                                   else
                                                   {
                                                       mainMessage.Text = "ラナ：ハズレよ。自分の特性ぐらいちゃんと掴んでおきなさいよ。";
                                                       ok.ShowDialog();
                                                   }
                                               }
                                               else if (sa.TargetNum == 1)
                                               {
                                                   if (mc.Intelligence >= mc.Strength && mc.Intelligence >= mc.Agility)
                                                   {
                                                       mainMessage.Text = "ラナ：正解ね♪";
                                                       ok.ShowDialog();
                                                       success++;
                                                   }
                                                   else
                                                   {
                                                       mainMessage.Text = "ラナ：ハズレよ。自分の特性ぐらいちゃんと掴んでおきなさいよ。";
                                                       ok.ShowDialog();
                                                   }
                                               }
                                               else if (sa.TargetNum == 2)
                                               {
                                                   if (mc.Strength >= mc.Intelligence && mc.Strength >= mc.Agility)
                                                   {
                                                       mainMessage.Text = "ラナ：正解ね♪";
                                                       ok.ShowDialog();
                                                       success++;
                                                   }
                                                   else
                                                   {
                                                       mainMessage.Text = "ラナ：ハズレよ。自分の特性ぐらいちゃんと掴んでおきなさいよ。";
                                                       ok.ShowDialog();
                                                   }
                                               }
                                               else
                                               {
                                                   mainMessage.Text = "ラナ：ハズレよ。あんたバカじゃない？";
                                                   ok.ShowDialog();
                                               }
                                           }
                                           mainMessage.Text = "ラナ：【５問目】アインの前に立ちふさがった１階のボスの必殺技は？";
                                           ok.ShowDialog();
                                           using (SelectAction sa = new SelectAction())
                                           {
                                               sa.StartPosition = FormStartPosition.CenterParent;
                                               sa.ForceChangeWidth = 300;
                                               sa.ElementA = "エターナル・ブラスター";
                                               sa.ElementB = "ファイア・ボール";
                                               sa.ElementC = "キル・スピニングランサー";
                                               sa.ShowDialog();
                                               if (sa.TargetNum == 2)
                                               {
                                                   mainMessage.Text = "ラナ：正解ね♪";
                                                   ok.ShowDialog();
                                                   success++;
                                               }
                                               else
                                               {
                                                   mainMessage.Text = "ラナ：ハズレよ。あんたバカじゃない？";
                                                   ok.ShowDialog();
                                               }
                                           }
                                           mainMessage.Text = "ラナ：【６問目】アインがこの２階に到達したのは何日目？";
                                           ok.ShowDialog();
                                           using (RequestInput ri = new RequestInput())
                                           {
                                               ri.StartPosition = FormStartPosition.CenterParent;
                                               ri.ShowDialog();
                                               if (ri.InputData == we.CompleteArea1Day.ToString())
                                               {
                                                   mainMessage.Text = "ラナ：正解ね♪";
                                                   ok.ShowDialog();
                                                   success++;
                                               }
                                               else
                                               {
                                                   mainMessage.Text = "ラナ：ハズレよ。あんたバカじゃない？";
                                                   ok.ShowDialog();
                                               }
                                           }
                                           mainMessage.Text = "ラナ：【７問目】アインのファイア・ボールとフレッシュ・ヒール。知力でＵＰ比率が高いのは？";
                                           ok.ShowDialog();
                                           using (SelectAction sa = new SelectAction())
                                           {
                                               sa.StartPosition = FormStartPosition.CenterParent;
                                               sa.ForceChangeWidth = 300;
                                               sa.ElementA = "ファイア・ボール";
                                               sa.ElementB = "フレッシュ・ヒール";
                                               sa.ElementC = "どちらも同じ";
                                               sa.ShowDialog();
                                               if (sa.TargetNum == 1)
                                               {
                                                   mainMessage.Text = "ラナ：正解ね♪";
                                                   ok.ShowDialog();
                                                   success++;
                                               }
                                               else
                                               {
                                                   mainMessage.Text = "ラナ：ハズレよ。あんたバカじゃない？";
                                                   ok.ShowDialog();
                                               }
                                           }
                                           mainMessage.Text = "ラナ：【８問目】アイン、私たちはまだダンジョンの中よ。覚えてる？";
                                           ok.ShowDialog();
                                           mainMessage.Text = "アイン：なんだそりゃ？？";
                                           ok.ShowDialog();
                                           mainMessage.Text = "ラナ：ちゃんと答えなさい。";
                                           ok.ShowDialog();
                                           mainMessage.Text = "アイン：いやまあ、じゃあ答えるとするか。";
                                           ok.ShowDialog();
                                           using (SelectAction sa = new SelectAction())
                                           {
                                               sa.StartPosition = FormStartPosition.CenterParent;
                                               sa.ForceChangeWidth = 300;
                                               sa.ElementA = "ああ、まだダンジョンの中だ。";
                                               sa.ElementB = "いいや、実は・・・外だ。";
                                               sa.ElementC = "覚えてるぜ。";
                                               sa.ShowDialog();
                                               if (sa.TargetNum == 0)
                                               {
                                                   mainMessage.Text = "ラナ：正解ね♪";
                                                   ok.ShowDialog();
                                                   success++;
                                               }
                                               else if (sa.TargetNum == 2)
                                               {
                                                   mainMessage.Text = "ラナ：正解ね♪";
                                                   ok.ShowDialog();
                                                   success++;
                                                   // [警告]：１回クリアしている場合、追加質問してください。
                                               }
                                               else
                                               {
                                                   mainMessage.Text = "ラナ：ハズレよ。あんたバカじゃない？";
                                                   ok.ShowDialog();
                                               }
                                           }
                                           mainMessage.Text = "ラナ：【９問目】アインと言えばやっぱりバカ？";
                                           ok.ShowDialog();
                                           mainMessage.Text = "アイン：待て、それは質問じゃねえだろ？";
                                           ok.ShowDialog();
                                           mainMessage.Text = "ラナ：っさ、回答解答♪";
                                           ok.ShowDialog();
                                           mainMessage.Text = "アイン：くそっ、絶対に回避してやるぜ・・・選択肢を見せてみろってんだ！";
                                           ok.ShowDialog();
                                           using (SelectAction sa = new SelectAction())
                                           {
                                               sa.StartPosition = FormStartPosition.CenterParent;
                                               sa.ForceChangeWidth = 300;
                                               sa.ElementA = "バカ！";
                                               sa.ElementB = "バカ♪";
                                               sa.ElementC = "・・・バカ";
                                               sa.ShowDialog();
                                               if (sa.TargetNum == -1)
                                               {
                                                   mainMessage.Text = "ラナ：っな！？何で回避出来るのよ。ハズレね！";
                                                   ok.ShowDialog();
                                               }
                                               else
                                               {
                                                   mainMessage.Text = "ラナ：正解ね♪";
                                                   ok.ShowDialog();
                                                   mainMessage.Text = "アイン：待て待て待て！いろいろ何かと間違ってるだろ、今のは！";
                                                   ok.ShowDialog();
                                                   mainMessage.Text = "ラナ：良いじゃない、正解なんだから正解よ♪";
                                                   ok.ShowDialog();
                                                   mainMessage.Text = "アイン：っく、神よ・・・このラナ・アミリアという人間に慈しみの心を・・・";
                                                   ok.ShowDialog();
                                                   success++;
                                               }
                                           }

                                           if (!we.FailArea23)
                                           {
                                               mainMessage.Text = "ラナ：【１０問目】じゃ・・・じゃあ最後よ。実は、好きな人がいる。相手は誰？";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：・・・何でも良いとは言ったが、何でも良いわけじゃねえ。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "ラナ：正直に答えなさいよ。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：おいおいおい、マジかよ。何て質問しやがる。";
                                               ok.ShowDialog();
                                               while (true)
                                               {
                                                   using (SelectAction sa = new SelectAction())
                                                   {
                                                       sa.StartPosition = FormStartPosition.CenterParent;
                                                       sa.ForceChangeWidth = 300;
                                                       sa.ElementA = "オル・ランディス";
                                                       sa.ElementB = "ラナ・アミリア";
                                                       sa.ElementC = "ファラ・フローレ王妃";
                                                       sa.ShowDialog();
                                                       if (sa.TargetNum == -1)
                                                       {
                                                           mainMessage.Text = "ラナ：駄目よ、ちゃんと答えること。";
                                                           ok.ShowDialog();
                                                       }
                                                       else if (sa.TargetNum == 0)
                                                       {
                                                           mainMessage.Text = "ラナ：うーん・・・そうだったんだ。なんだかんだ言ってもね♪";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "アイン：おい、今のは選択ミスだ。誰があんなクソ師匠を。";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "ラナ：まあまあ、そういう事にしておきますか。ッハイ！じゃ結果発表！";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "アイン：おお、結果を聞かせてくれ。";
                                                           ok.ShowDialog();
                                                           break;
                                                       }
                                                       else if (sa.TargetNum == 1)
                                                       {
                                                           mainMessage.Text = "ラナ：・・・・・・死ね。バカアイン。";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "アイン：ワケわかんねえ質問してんじゃねえよ！だ、大体今の正解なのかよ！？";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "ラナ：超トップシークレットね。あんたバカだから、一生分からずじまいよ。";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "アイン：普通クエスチョンアンドアンサーは正解が分かる方式だろ！";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "ラナ：あんたが聞けって言うから聞いてるだけよ。";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "アイン：っく・・・あぁ、分かった分かった。ハズレなんだな。分かったよ。";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "ラナ：べっ、別にハズレなんて言ってないじゃない。";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "アイン：そうなのか？じゃあ・・・ひょっとして正";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "　　　『ッバキャアアァァァ！！！』（ライトニングキックがアインに炸裂）　　";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "アイン：ッグ、ッウゴオォォォ・・・モロに入っ・・・ッガハアァァ・・・";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "ラナ：ッハイ！じゃ結果発表行くわよ♪";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "アイン：ッゲホ！ッゲホ！ァ、ぁぁああイッテェ・・・おお、結果を聞かせてくれ。";
                                                           ok.ShowDialog();
                                                           success++;
                                                           break;
                                                       }
                                                       else if (sa.TargetNum == 2)
                                                       {
                                                           mainMessage.Text = "ラナ：うわっ・・・ヘンタイ！！";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "アイン：そんな選択肢出してっから、無難なのはそれしかねえだろ！";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "ラナ：ファラ様を選ぶのが無難？？ヘンタイ通り越して犯罪者よ。";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "アイン：ランディのヤツなんかを好きなんて言ってみろ。自殺行為だろ！？";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "ラナ：・・・なーんだ。消去法で選んじゃったんだ。";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "アイン：いや、消去法ってわけじゃねえが。";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "ラナ：・・・じゃ、何々法で選んだのよ？";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "アイン：何だって良いだろうがそんな事は。どうなんだよ、正解なのかよ！？";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "ラナ：教えるわけ無いじゃない。あんたバカだから、一生分からずじまいよ。";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "アイン：っく・・・まあどっちでも良いさ。こんなもんどうせ正解もクソもねえんだろ。";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "ラナ：・・・え、えっとと、そういうわけじゃ・・・じゃあ結果発表よ♪";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "アイン：おお、結果を聞かせてくれ。";
                                                           ok.ShowDialog();
                                                           break;
                                                       }
                                                   }
                                               }
                                           }
                                           else
                                           {
                                               mainMessage.Text = "ラナ：【１０問目】じゃあ最後よ。ガンツ・ギメルガ叔父さんの妻は誰？";
                                               ok.ShowDialog();
                                               using (SelectAction sa = new SelectAction())
                                               {
                                                   sa.StartPosition = FormStartPosition.CenterParent;
                                                   sa.ForceChangeWidth = 300;
                                                   sa.ElementA = "ファラ・フローレ";
                                                   sa.ElementB = "ラナ・アミリア";
                                                   sa.ElementC = "ハンナ・ギメルガ";
                                                   sa.ShowDialog();
                                                   if (sa.TargetNum == 2)
                                                   {
                                                       mainMessage.Text = "ラナ：正解ね♪";
                                                       ok.ShowDialog();
                                                       success++;
                                                   }
                                                   else
                                                   {
                                                       mainMessage.Text = "ラナ：ハズレよ。あんたバカじゃない？";
                                                       ok.ShowDialog();
                                                   }
                                               }
                                           }

                                           if (success >= 10 && !we.FailArea23)
                                           {
                                               // [警告]：特別フラグがあると物語の面白さが膨らみます。
                                           }

                                           if (success >= 8)
                                           {
                                               mainMessage.Text = "ラナ：っお、結構やるじゃないの♪";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：どうだ、これなら看板の文字が変わるはずだろ。見てみようぜ！";
                                               ok.ShowDialog();
                                               mainMessage.Text = "　　　　『この先、左へ進むべし。右へ進むべからず。　』";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：よし来た！っふう・・・もうこういう類は、これっきりにして欲しいぜ。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "ラナ：でも、ダンジョンといえば謎かけは付きものよ。トラップも無さそうだし良いじゃない。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：まあ、そうだけどな。よし、じゃあ左へ進むとするか！";
                                               ok.ShowDialog();
                                               mainMessage.Text = "ラナ：アイン、前回も聞いたけど、何で看板通り進むのよ？";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：まだそんな事言ってるのか。ラナ、お前は昔っから疑り深かったよな。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "ラナ：アインがバカ過ぎるのよ。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：・・・ラナ、いいかよく聞け。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "ラナ：？　何よ真面目な顔なんかしちゃって。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：そこの看板がもし本当の事が書いてあるとしようじゃないか。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：だったらそのまま進めば良いだけの事だ。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "ラナ：まあそうね。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：本当の事が書いてあるのに、俺たちが妙な事考えて反対に行ったとすれば。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：それは裏切ったと言う事になる。ムダな行為だ。わかるな？";
                                               ok.ShowDialog();
                                               mainMessage.Text = "ラナ：裏切った？どういう意味よ？";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：看板は人じゃねえが、もし人なら、次から本当の事は言わなくなるって事さ。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "ラナ：よくわかんないわね、看板は看板よ。もし、嘘だったらどうするのよ？";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：じゃあ、もし嘘が書いてあるとしよう。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：だとすれば反対に行くのが、当然正解だ。だが";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：それは俺たちがその看板の性質を見破れてるかどうかが鍵だ。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：残念だが、俺たちは今の所それを見破る術を持ち合わせてねえ。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：ラナ、嘘かどうかなんてわかんねえだろ？";
                                               ok.ShowDialog();
                                               mainMessage.Text = "ラナ：まあ、行ってみない事には嘘かどうかは結局分からないわね。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：そうだ。こういった場合結局行ってみるという行為しか、選択肢は残されてねえんだよ。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：裏切って、ムダな行為を選択するか。それとも信じて騙される方を選択するか。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：信じて騙された場合、挽回は出来る。何とか返す方法が残ってるはずだ。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：だが裏切ってムダな行為をした時は取り返しがつかねえ。師匠からそう教わったんだ。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "ラナ：・・・ダメ、私そういうの全然わかんない。でもランディスお師匠さんがそう言ったのね？";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：ああそうだ。ヤツは支離滅裂で無茶苦茶だが、不思議とスジが通ってるしな。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "ラナ：アインのバカ話は全然わかんないけど、ランディスお師匠さんを信じる事にするわ♪";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：くそ、俺の解説はなんだったんだよ・・・まあ良いか、行こうぜ！左へ！";
                                               ok.ShowDialog();
                                               mainMessage.Text = "ラナ：そうね、じゃ左へ行きましょ♪";
                                               ok.ShowDialog();

                                               we.SolveArea23 = true;
                                               we.CompleteArea23 = true;
                                               ConstructDungeonMap();
                                               SetupDungeonMapping(2);
                                           }
                                           else
                                           {
                                               mainMessage.Text = "ラナ：ちょっと、全然話にならないじゃない。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：そうか？こんなもんだろ？";
                                               ok.ShowDialog();
                                               mainMessage.Text = "ラナ：駄目ね、やり直しよ。";
                                               ok.ShowDialog();
                                               mainMessage.Text = "アイン：ったく、なんて面倒くせえ所だ・・・っしゃ、次こそ正解連発だぜ！";
                                               ok.ShowDialog();
                                               mainMessage.Text = "ラナ：ホント頼むわよ。";
                                               ok.ShowDialog();
                                               we.FailArea23 = true;
                                           }
                                       }
                                   }
                                   else
                                   {
                                       if (!we.CompleteArea26)
                                       {
                                           mainMessage.Text = "　　　　『この先、左へ進むべし。右へ進むべからず。　』";
                                       }
                                       else
                                       {
                                           mainMessage.Text = "看板はもう無くなっている。";
                                       }
                                   }
                                   return;
                                   #endregion

                                case 13: // 
                                    #region "看板突破イベント 2-4"
                                    if (!we.SolveArea24)
                                    {
                                        if (!we.InfoArea24)
                                        {
                                            mainMessage.Text = "アイン：お、やっぱり看板があるぜ・・・なになに？";
                                            mainMessage.Update();
                                            ok.ShowDialog();
                                            mainMessage.Text = "　　　　『この先、行き止まり。　正しき順序、正しき道筋を示せ。　』";
                                            mainMessage.Update();
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：駄目だ。俺はもうこの“正しき”という響きが気にいらねえ。";
                                            mainMessage.Update();
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：何言ってるのよ。解かない限り先に進めないんでしょ？";
                                            mainMessage.Update();
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：ふううぅぅぅ・・・じゃ探索と行くか。";
                                            mainMessage.Update();
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：ッフフ、なんだか疲れてるみたいね。私がメインでやる？";
                                            mainMessage.Update();
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：ああ、俺は今回パスとさせてもらうぜ、頼んだぜラナ。";
                                            mainMessage.Update();
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：よし、じゃアインは休んでて良いわよ。さっそく探索しますか♪";
                                            mainMessage.Update();
                                            ok.ShowDialog();
                                            we.InfoArea24 = true;
                                        }
                                        else
                                        {
                                            if (knownTileInfo2[151] && knownTileInfo2[152] && knownTileInfo2[153] && knownTileInfo2[154] && knownTileInfo2[155] && knownTileInfo2[156] && knownTileInfo2[157] &&
                                                knownTileInfo2[181] && knownTileInfo2[183] && knownTileInfo2[185] && knownTileInfo2[187] &&
                                                knownTileInfo2[211] && knownTileInfo2[212] && knownTileInfo2[213] && knownTileInfo2[214] && knownTileInfo2[215] && knownTileInfo2[216] && knownTileInfo2[217] &&
                                                knownTileInfo2[241] && knownTileInfo2[243] && knownTileInfo2[245] && knownTileInfo2[247] &&
                                                knownTileInfo2[271] && knownTileInfo2[272] && knownTileInfo2[273] && knownTileInfo2[274] && knownTileInfo2[275] && knownTileInfo2[276] && knownTileInfo2[277])
                                            {
                                                mainMessage.Text = "ラナ：一通りまわったわね。・・・あれ？ちょっとアイン。";
                                                mainMessage.Update();
                                                ok.ShowDialog();
                                                mainMessage.Text = "アイン：なんだ、どうした？";
                                                mainMessage.Update();
                                                ok.ShowDialog();
                                                mainMessage.Text = "ラナ：看板・・・看板自体が消えて無くなってるわ。";
                                                mainMessage.Update();
                                                ok.ShowDialog();
                                                mainMessage.Text = "アイン：なに！？って事はもうクリアしたのか！？";
                                                mainMessage.Update();
                                                ok.ShowDialog();
                                                mainMessage.Text = "ラナ：んなワケないでしょ。今までは大抵文字が変わってたじゃない。";
                                                mainMessage.Update();
                                                ok.ShowDialog();
                                                mainMessage.Text = "アイン：そういや、そうだな。";
                                                mainMessage.Update();
                                                ok.ShowDialog();
                                                mainMessage.Text = "ラナ：もう一度周りをくまなく探してみましょ。";
                                                mainMessage.Update();
                                                ok.ShowDialog();
                                                mainMessage.Text = "アイン：ああ、良いぜ。";
                                                mainMessage.Update();
                                                ok.ShowDialog();
                                                we.SolveArea24 = true; // [警告]：解決とコンプリートが別なのを別観点で流用しています。混乱の元になるようであれば新しいフラグを追記してください。
                                            }
                                            else
                                            {
                                                mainMessage.Text = "　　　　『この先、行き止まり。　正しき順序、正しき道筋を示せ。　』";
                                                mainMessage.Update();
                                            }
                                        }
                                    }
                                    else
                                    {
                                    }
                                    return;
                                    #endregion

                                #region "看板突破イベント 2-4 通過性 case14 - case 25"
                                case 14:
                                    if (we.SolveArea24 && !we.CompleteArea24)
                                    {
                                        mainMessage.Text = "    <<< 1 >>>";
                                        mainMessage.Update();
                                        we.ProgressArea241 = true;
                                        we.ProgressArea2412 = false;
                                        we.ProgressArea2413 = false;
                                        we.ProgressArea242 = false;
                                        we.ProgressArea2422 = false;
                                        we.ProgressArea243 = false;
                                        we.ProgressArea2432 = false;
                                        we.ProgressArea244 = false;
                                        we.ProgressArea2442 = false;
                                        we.ProgressArea245 = false;
                                        we.ProgressArea2452 = false;
                                        we.ProgressArea246 = false;
                                    }
                                    return;

                                case 15:
                                    if (we.SolveArea24 && !we.CompleteArea24)
                                    {
                                        if (we.ProgressArea241 && !we.ProgressArea2412)
                                        {
                                            we.ProgressArea2412 = true;
                                            we.ProgressArea2413 = false;
                                            we.ProgressArea242 = false;
                                            we.ProgressArea2422 = false;
                                            we.ProgressArea243 = false;
                                            we.ProgressArea2432 = false;
                                            we.ProgressArea244 = false;
                                            we.ProgressArea2442 = false;
                                            we.ProgressArea245 = false;
                                            we.ProgressArea2452 = false;
                                            we.ProgressArea246 = false;
                                        }
                                        else
                                        {
                                            we.ProgressArea241 = false;
                                            we.ProgressArea2412 = false;
                                            we.ProgressArea2413 = false;
                                            we.ProgressArea242 = false;
                                            we.ProgressArea2422 = false;
                                            we.ProgressArea243 = false;
                                            we.ProgressArea2432 = false;
                                            we.ProgressArea244 = false;
                                            we.ProgressArea2442 = false;
                                            we.ProgressArea245 = false;
                                            we.ProgressArea2452 = false;
                                            we.ProgressArea246 = false;
                                        }
                                    }
                                    return;

                                case 16:
                                    if (we.SolveArea24 && !we.CompleteArea24)
                                    {
                                        if (we.ProgressArea241 && we.ProgressArea2412 && !we.ProgressArea2413)
                                        {
                                            we.ProgressArea2413 = true;
                                            we.ProgressArea242 = false;
                                            we.ProgressArea2422 = false;
                                            we.ProgressArea243 = false;
                                            we.ProgressArea2432 = false;
                                            we.ProgressArea244 = false;
                                            we.ProgressArea2442 = false;
                                            we.ProgressArea245 = false;
                                            we.ProgressArea2452 = false;
                                            we.ProgressArea246 = false;
                                        }
                                        else
                                        {
                                            we.ProgressArea241 = false;
                                            we.ProgressArea2412 = false;
                                            we.ProgressArea2413 = false;
                                            we.ProgressArea242 = false;
                                            we.ProgressArea2422 = false;
                                            we.ProgressArea243 = false;
                                            we.ProgressArea2432 = false;
                                            we.ProgressArea244 = false;
                                            we.ProgressArea2442 = false;
                                            we.ProgressArea245 = false;
                                            we.ProgressArea2452 = false;
                                            we.ProgressArea246 = false;
                                        }
                                    }
                                    return;

                                case 17:
                                    if (we.SolveArea24 && !we.CompleteArea24)
                                    {
                                        mainMessage.Text = "    <<< 2 >>>";
                                        mainMessage.Update();
                                        if (we.ProgressArea241 && we.ProgressArea2412 && we.ProgressArea2413 && !we.ProgressArea242)
                                        {
                                            we.ProgressArea242 = true;
                                            we.ProgressArea2422 = false;
                                            we.ProgressArea243 = false;
                                            we.ProgressArea2432 = false;
                                            we.ProgressArea244 = false;
                                            we.ProgressArea2442 = false;
                                            we.ProgressArea245 = false;
                                            we.ProgressArea2452 = false;
                                            we.ProgressArea246 = false;
                                        }
                                        else
                                        {
                                            we.ProgressArea241 = false;
                                            we.ProgressArea2412 = false;
                                            we.ProgressArea2413 = false;
                                            we.ProgressArea242 = false;
                                            we.ProgressArea2422 = false;
                                            we.ProgressArea243 = false;
                                            we.ProgressArea2432 = false;
                                            we.ProgressArea244 = false;
                                            we.ProgressArea2442 = false;
                                            we.ProgressArea245 = false;
                                            we.ProgressArea2452 = false;
                                            we.ProgressArea246 = false;
                                        }
                                    }
                                    return;

                                case 18:
                                    if (we.SolveArea24 && !we.CompleteArea24)
                                    {
                                        if (we.ProgressArea241 && we.ProgressArea2412 && we.ProgressArea2413 && we.ProgressArea242 && !we.ProgressArea2422)
                                        {
                                            we.ProgressArea2422 = true;
                                            we.ProgressArea243 = false;
                                            we.ProgressArea2432 = false;
                                            we.ProgressArea244 = false;
                                            we.ProgressArea2442 = false;
                                            we.ProgressArea245 = false;
                                            we.ProgressArea2452 = false;
                                            we.ProgressArea246 = false;
                                        }
                                        else
                                        {
                                            we.ProgressArea241 = false;
                                            we.ProgressArea2412 = false;
                                            we.ProgressArea2413 = false;
                                            we.ProgressArea242 = false;
                                            we.ProgressArea2422 = false;
                                            we.ProgressArea243 = false;
                                            we.ProgressArea2432 = false;
                                            we.ProgressArea244 = false;
                                            we.ProgressArea2442 = false;
                                            we.ProgressArea245 = false;
                                            we.ProgressArea2452 = false;
                                            we.ProgressArea246 = false;
                                        }
                                    }
                                    return;

                                case 19:
                                    if (we.SolveArea24 && !we.CompleteArea24)
                                    {
                                        mainMessage.Text = "    <<< 3 >>>";
                                        mainMessage.Update();
                                        if (we.ProgressArea241 && we.ProgressArea2412 && we.ProgressArea2413 && we.ProgressArea242 && we.ProgressArea2422 && !we.ProgressArea243)
                                        {
                                            we.ProgressArea243 = true;
                                            we.ProgressArea2432 = false;
                                            we.ProgressArea244 = false;
                                            we.ProgressArea2442 = false;
                                            we.ProgressArea245 = false;
                                            we.ProgressArea2452 = false;
                                            we.ProgressArea246 = false;
                                        }
                                        else
                                        {
                                            we.ProgressArea241 = false;
                                            we.ProgressArea2412 = false;
                                            we.ProgressArea2413 = false;
                                            we.ProgressArea242 = false;
                                            we.ProgressArea2422 = false;
                                            we.ProgressArea243 = false;
                                            we.ProgressArea2432 = false;
                                            we.ProgressArea244 = false;
                                            we.ProgressArea2442 = false;
                                            we.ProgressArea245 = false;
                                            we.ProgressArea2452 = false;
                                            we.ProgressArea246 = false;
                                        }
                                    }
                                    return;

                                case 20:
                                    if (we.SolveArea24 && !we.CompleteArea24)
                                    {
                                        if (we.ProgressArea241 && we.ProgressArea2412 && we.ProgressArea2413 && we.ProgressArea242 && we.ProgressArea2422 && we.ProgressArea243 && !we.ProgressArea2432)
                                        {
                                            we.ProgressArea2432 = true;
                                            we.ProgressArea244 = false;
                                            we.ProgressArea2442 = false;
                                            we.ProgressArea245 = false;
                                            we.ProgressArea2452 = false;
                                            we.ProgressArea246 = false;
                                        }
                                        else
                                        {
                                            we.ProgressArea241 = false;
                                            we.ProgressArea2412 = false;
                                            we.ProgressArea2413 = false;
                                            we.ProgressArea242 = false;
                                            we.ProgressArea2422 = false;
                                            we.ProgressArea243 = false;
                                            we.ProgressArea2432 = false;
                                            we.ProgressArea244 = false;
                                            we.ProgressArea2442 = false;
                                            we.ProgressArea245 = false;
                                            we.ProgressArea2452 = false;
                                            we.ProgressArea246 = false;
                                        }
                                    }
                                    return;

                                case 21:
                                    if (we.SolveArea24 && !we.CompleteArea24)
                                    {
                                        mainMessage.Text = "    <<< 4 >>>";
                                        mainMessage.Update();
                                        if (we.ProgressArea241 && we.ProgressArea2412 && we.ProgressArea2413 && we.ProgressArea242 && we.ProgressArea2422 && we.ProgressArea243 && we.ProgressArea2432 && !we.ProgressArea244)
                                        {
                                            we.ProgressArea244 = true;
                                            we.ProgressArea2442 = false;
                                            we.ProgressArea245 = false;
                                            we.ProgressArea2452 = false;
                                            we.ProgressArea246 = false;
                                        }
                                        else
                                        {
                                            we.ProgressArea241 = false;
                                            we.ProgressArea2412 = false;
                                            we.ProgressArea2413 = false;
                                            we.ProgressArea242 = false;
                                            we.ProgressArea2422 = false;
                                            we.ProgressArea243 = false;
                                            we.ProgressArea2432 = false;
                                            we.ProgressArea244 = false;
                                            we.ProgressArea2442 = false;
                                            we.ProgressArea245 = false;
                                            we.ProgressArea2452 = false;
                                            we.ProgressArea246 = false;
                                        }
                                    }
                                    return;

                                case 22:
                                    if (we.SolveArea24 && !we.CompleteArea24)
                                    {
                                        if (we.ProgressArea241 && we.ProgressArea2412 && we.ProgressArea2413 && we.ProgressArea242 && we.ProgressArea2422 && we.ProgressArea243 && we.ProgressArea2432 && we.ProgressArea244 && !we.ProgressArea2442)
                                        {
                                            we.ProgressArea2442 = true;
                                            we.ProgressArea245 = false;
                                            we.ProgressArea2452 = false;
                                            we.ProgressArea246 = false;
                                        }
                                        else
                                        {
                                            we.ProgressArea241 = false;
                                            we.ProgressArea2412 = false;
                                            we.ProgressArea2413 = false;
                                            we.ProgressArea242 = false;
                                            we.ProgressArea2422 = false;
                                            we.ProgressArea243 = false;
                                            we.ProgressArea2432 = false;
                                            we.ProgressArea244 = false;
                                            we.ProgressArea2442 = false;
                                            we.ProgressArea245 = false;
                                            we.ProgressArea2452 = false;
                                            we.ProgressArea246 = false;
                                        }
                                    }
                                    return;

                                case 23:
                                    if (we.SolveArea24 && !we.CompleteArea24)
                                    {
                                        mainMessage.Text = "    <<< 5 >>>";
                                        mainMessage.Update();
                                        if (we.ProgressArea241 && we.ProgressArea2412 && we.ProgressArea2413 && we.ProgressArea242 && we.ProgressArea2422 && we.ProgressArea243 && we.ProgressArea2432 && we.ProgressArea244 && we.ProgressArea2442 && !we.ProgressArea245)
                                        {
                                            we.ProgressArea245 = true;
                                            we.ProgressArea2452 = false;
                                            we.ProgressArea246 = false;
                                        }
                                        else
                                        {
                                            we.ProgressArea241 = false;
                                            we.ProgressArea2412 = false;
                                            we.ProgressArea2413 = false;
                                            we.ProgressArea242 = false;
                                            we.ProgressArea2422 = false;
                                            we.ProgressArea243 = false;
                                            we.ProgressArea2432 = false;
                                            we.ProgressArea244 = false;
                                            we.ProgressArea2442 = false;
                                            we.ProgressArea245 = false;
                                            we.ProgressArea2452 = false;
                                            we.ProgressArea246 = false;
                                        }
                                    }
                                    return;

                                case 24:
                                    if (we.SolveArea24 && !we.CompleteArea24)
                                    {
                                        if (we.ProgressArea241 && we.ProgressArea2412 && we.ProgressArea2413 && we.ProgressArea242 && we.ProgressArea2422 && we.ProgressArea243 && we.ProgressArea2432 && we.ProgressArea244 && we.ProgressArea2442 && we.ProgressArea245 && !we.ProgressArea2452)
                                        {
                                            we.ProgressArea2452 = true;
                                            we.ProgressArea246 = false;
                                        }
                                        else
                                        {
                                            we.ProgressArea241 = false;
                                            we.ProgressArea2412 = false;
                                            we.ProgressArea2413 = false;
                                            we.ProgressArea242 = false;
                                            we.ProgressArea2422 = false;
                                            we.ProgressArea243 = false;
                                            we.ProgressArea2432 = false;
                                            we.ProgressArea244 = false;
                                            we.ProgressArea2442 = false;
                                            we.ProgressArea245 = false;
                                            we.ProgressArea2452 = false;
                                            we.ProgressArea246 = false;
                                        }
                                    }
                                    return;

                                case 25:
                                    if (we.SolveArea24 && !we.CompleteArea24)
                                    {
                                        mainMessage.Text = "    <<< 6 >>>";
                                        mainMessage.Update();
                                        if (we.ProgressArea241 && we.ProgressArea2412 && we.ProgressArea2413 && we.ProgressArea242 && we.ProgressArea2422 && we.ProgressArea243 && we.ProgressArea2432 && we.ProgressArea244 && we.ProgressArea2442 && we.ProgressArea245 && we.ProgressArea2452 && !we.ProgressArea246)
                                        {
                                            we.ProgressArea246 = true;
                                        }
                                        else
                                        {
                                            we.ProgressArea241 = false;
                                            we.ProgressArea2412 = false;
                                            we.ProgressArea2413 = false;
                                            we.ProgressArea242 = false;
                                            we.ProgressArea2422 = false;
                                            we.ProgressArea243 = false;
                                            we.ProgressArea2432 = false;
                                            we.ProgressArea244 = false;
                                            we.ProgressArea2442 = false;
                                            we.ProgressArea245 = false;
                                            we.ProgressArea2452 = false;
                                            we.ProgressArea246 = false;
                                        }
                                    }
                                    return;
                                #endregion 

                                case 26:
                                    #region "看板突破イベント 2-4 ２"
                                    if (we.SolveArea24)
                                    {
                                        if (!we.CompleteArea24)
                                        {
                                            if (we.ProgressArea241 && we.ProgressArea2412 && we.ProgressArea2413 && we.ProgressArea242 && we.ProgressArea2422 && we.ProgressArea243 && we.ProgressArea2432 && we.ProgressArea244 && we.ProgressArea2442 && we.ProgressArea245 && we.ProgressArea2452)
                                            {
                                                if (!we.FirstProcessArea24)
                                                {
                                                    // [警告]：ココは何か報酬を設けた方が物語りとして盛り上がります。
                                                    mainMessage.Text = "　　　　『ッゴゴゴゴゴ・・・ズウウゥゥン！』";
                                                    ok.ShowDialog();
                                                    we.CompleteArea24 = true;
                                                    ConstructDungeonMap();
                                                    SetupDungeonMapping(2);
                                                    mainMessage.Text = "　　　　『この先、下へ進むべし。』";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：マジかよ！？看板見つけたと思ったら、突然道が開けたぞ！？";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：ッフフ、アインじゃこうは行かないでしょ♪";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：行かないも何も、マジで分からなかったぜ。どういう順序だったんだ？";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：床下に数字があったでしょ？あれは大体想像がつくと思うけど、正しき順序の事ね。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：ああ、確かに１とか４とかあったよな。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：床下の数字は１～６まであったから、１，２，３，４，５，６の順序って事よ。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：なるほど、その数字は道のりの順序って事だったのか。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：それから看板の位置だけど。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：そうだ。看板の位置は今始めて見つけた場所だぞ？";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：私のダンジョンマッピングメモによれば、この区画はあと下しか残ってないわ。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：だから下の壁の中央、つまり今居る場所が次の道が開ける場所って事よ。そこが看板の位置だと思わない？";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：そんな推測まで成り立つのか・・・お前、ひょっとしてカール爵のレベルなんじゃねえの？";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：たまたまよ。カール爵だったら確かに一発クリアしてそうね。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：じゃあ残るは、正しい道筋って表現だが・・・";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：この区画のレイアウトを見れば分かるけど、明らかに柱と道に分かれてるでしょ。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：だから数字の１がスタート、２，３，４，５，６を通るように道筋を経て、最後は当然看板ね。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：一発解きなんてあり得るのかよ。ラナ、お前やっぱすげえ！連れて来て大正解だな！";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：ッフフ、ホラホラ、まだ途中なんだから、次々進みましょ。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：ああ、次は下だな！了解了解！";
                                                    ok.ShowDialog();
                                                }
                                                else
                                                {
                                                    if (!we.FailArea241)
                                                    {
                                                        mainMessage.Text = "　　　　『ッゴゴゴゴゴ・・・ズウウゥゥン！』";
                                                        ok.ShowDialog();
                                                        we.CompleteArea24 = true;
                                                        ConstructDungeonMap();
                                                        SetupDungeonMapping(2);
                                                        mainMessage.Text = "　　　　『この先、下へ進むべし。』";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：マジかよ！？どういう事だったんだ。からくりを教えてくれよ。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：秘密よ♪";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：駄目だ、すっげえ気になる！ラナ様お願いします、今回だけは教えてくれ・・・";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：床下に数字があったでしょ？あれは大体想像がつくと思うけど、正しき順序の事ね。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：どういう意味だ？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：だから、床下の数字が１～６まであるから、１，２，３，４，５，６の順序って事よ。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：分かった、１，２，３，４，５，６の順序って事だな。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：同じ事繰り返し言ってどうすんのよ。それから看板の位置だけど、";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：何だ、あんなものに意味があったっていうのか？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：言ってみれば、ゴール地点ね。元の場所にあったんじゃ、どうしても正しい道筋が作れないわ。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：その正しい道筋って何だったんだよ？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：この区画のレイアウトを見れば分かるけど、明らかに柱と道に分かれてるでしょ。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：だから数字の１がスタート、２，３，４，５，６と来て・・・看板がゴールになるって事。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：・・・は？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：１がスタート、２，３，４，５，６と正しい順序どおりの道筋を経て、看板へ到着するとゴールよ。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：・・・は？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：１，２，３，４，５，６、看板って言ってるじゃない。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：・・・は？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：さーて、新しい奥義でも編み出して欲しいのかしら♪";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：オーケー、オーケー！たった今、分かったぜ。さすがラナ！";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：まったく、ホントに分かって言ってるのかしら。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：下だって言ってるんだ、早速下に行こうぜ！";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：ええ、次へ進みましょ。";
                                                        ok.ShowDialog();
                                                    }
                                                    else
                                                    {
                                                        mainMessage.Text = "　　　　『ッゴゴゴゴゴ・・・ズウウゥゥン！』";
                                                        ok.ShowDialog();
                                                        we.CompleteArea24 = true;
                                                        ConstructDungeonMap();
                                                        SetupDungeonMapping(2);
                                                        mainMessage.Text = "　　　　『この先、下へ進むべし。』";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：お！やったじゃねえか、ラナ！";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：何とかクリアしたわね♪";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：１がスタート、２，３，４，５，６と正しい順序どおりの道筋を経て、看板へ到着するとゴールだったわけね。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：後はこっから看板が指し示す通り、下に行くだけだな。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：ええ、次へ進みましょ。";
                                                        ok.ShowDialog();
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (!we.FailArea24)
                                                {
                                                    mainMessage.Text = "　　　　『この先、行き止まり。　正しき順序、正しき道筋を示せ。　』";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：こんな所に移動してたのね。文字は変わってないみたい。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：何でいちいち場所を変えてきたんだ？";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：そんな事知らなわいよ。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：ところで、ここに来るときに変な番号が床下に書いてあったな。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：最初探索した時は書いてなかったし、きっと意味があるはずだわ。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：看板の位置は変わる。変な数字は浮き上がる。どうなってんだココは？";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：・・・分かったわ。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：マジかよ！？どう分かったんだよ！？";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：看板の位置と数字を見れば一発ね。まあ、任せて。ついて来て♪";
                                                    ok.ShowDialog();
                                                    we.FailArea24 = true;
                                                }
                                                else
                                                {
                                                    if (!we.FailArea241)
                                                    {
                                                        mainMessage.Text = "　　　　『この先、行き止まり。　正しき順序、正しき道筋を示せ。　』";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：・・・おかしいわね。変わってないわね。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：何だ、駄目だったのか？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：そうね。失敗だわ。何が駄目だったのかしら。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：よくわかんねぇけどさ。あの床下の数字ってやっぱり１から６って順序を示してるのか？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：うん、多分それは合ってるわ。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：だとしたら、順序よく踏んでなかったって事だろ。もういっぺんやってみねえか？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：そうね、もう一度やってみましょ♪";
                                                        ok.ShowDialog();
                                                        we.FailArea241 = true;
                                                    }
                                                    else
                                                    {
                                                        if (!we.FailArea242)
                                                        {
                                                            mainMessage.Text = "　　　　『この先、行き止まり。　正しき順序、正しき道筋を示せ。　』";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "ラナ：・・・おかしいわね。正しい順序で通ったと思ったんだけど。";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "アイン：何だろうな・・・そういや、正しき道とか書いてあるぞ。";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "ラナ：順序通りだけじゃ不足って事かしら。順序も道筋も似たようなモノと思ったんだけど。";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "アイン：まあ似たようなもんだよな。";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "ラナ：いや待って。ここの区画、よく見れば、柱と道の構成になってたよね。";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "アイン：ん？ああ、そういやそうだったけな。それがどうかしたのか？";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "ラナ：分かったわ、正しき道筋よ。つまり、道筋を作るように進めば良いのよ。";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "アイン：道筋を・・・作る？";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "ラナ：そう、一旦通った道を２回またいだり、来た道を引き返したり、重複しないように通過すればいいのよ。";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "アイン：なるほど、やるじゃねえか。それで当たりじゃねえか？";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "ラナ：１から順番通りで、１本の道筋を作るようにする事。もう一度やってみましょ♪";
                                                            ok.ShowDialog();
                                                            we.FailArea242 = true;
                                                        }
                                                        else
                                                        {
                                                            mainMessage.Text = "　　　　『この先、行き止まり。　正しき順序、正しき道筋を示せ。　』";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "ラナ：っちょっと！何で駄目なの！？";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "アイン：落ち着いて整理しようぜ。まず正しき順序だが、";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "ラナ：１～６の番号が床下にあるから、その順番どおり通れば良いのよ。";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "アイン：次の正しき道筋って意味だが、";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "ラナ：道筋を作るように通過する。";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "アイン：２回またいでも駄目で";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "ラナ：来た道を引き返すのも駄目。";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "アイン：重複するような通過も駄目ってワケだ。";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "ラナ：もう一度やってみるわ。";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "アイン：あぁ、ラナ。お前を信用してるぜ。";
                                                            ok.ShowDialog();
                                                            ConstructDungeonMap();
                                                            SetupDungeonMapping(2);
                                                        }
                                                    }
                                                }
                                                we.FirstProcessArea24 = true;
                                            }
                                        }
                                        else
                                        {
                                            if (!we.CompleteArea26)
                                            {
                                                mainMessage.Text = "　　　　『この先、下へ進むべし。』";
                                            }
                                            else
                                            {
                                                mainMessage.Text = "看板はもう無くなっている。";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // 看板位置移行前は何も発生しない。
                                    }
                                    return;
                                    #endregion

                                case 27:
                                    #region "看板突破イベント 2-5"
                                    if (!we.SolveArea25)
                                    {
                                        mainMessage.Text = "アイン：本当にココはいくつ看板があるんだ・・・なになに？";
                                        ok.ShowDialog();
                                        mainMessage.Text = "　　　　『この先、行き止まり。　最後の区画へ進むべし。』";
                                        ok.ShowDialog();
                                        mainMessage.Text = "アイン：何だこれ、最後の区画ってなんだよ？";
                                        ok.ShowDialog();
                                        mainMessage.Text = "ラナ：最後の区画・・・ちょっと待ってね。ダンジョンマップメモ開いてみるから。";
                                        ok.ShowDialog();
                                        mainMessage.Text = "アイン：んだ、それ！？そんなもん書いてきたのかよ？";
                                        ok.ShowDialog();
                                        mainMessage.Text = "ラナ：当たり前じゃない。大体ダンジョンってのは行った所を把握してるかどうかが鍵よ。";
                                        ok.ShowDialog();
                                        mainMessage.Text = "アイン：っで、どうなんだ。最後の区画ってのはどこら辺か分かるのか？";
                                        ok.ShowDialog();
                                        mainMessage.Text = "ラナ：んんっと、多分この・・・右上あたりよ。ココだけ未到達領域なわけだし。";
                                        ok.ShowDialog();
                                        mainMessage.Text = "アイン：なるほど、役に立つもんだな。さすがだぜ！";
                                        ok.ShowDialog();
                                        mainMessage.Text = "アイン：この区画では何もしてないが、これで終わりなのか？やけにアッサリしてるな。";
                                        ok.ShowDialog();
                                        mainMessage.Text = "ラナ：そうね、でも最後の区画ってのが分からなければ、それ自体が問いかけになってるわ。";
                                        ok.ShowDialog();
                                        mainMessage.Text = "アイン：何か問題があると思い込んでると解けねえって事か？";
                                        ok.ShowDialog();
                                        mainMessage.Text = "ラナ：そんなトコじゃないかしら。ッフフ、ラナ様のダンジョンマップメモが役に立ったってコトよ。";
                                        ok.ShowDialog();
                                        mainMessage.Text = "アイン：ああ、それがあったからこそ、速攻クリアだったわけだしな。感謝してるぜ、ホント。";
                                        ok.ShowDialog();
                                        mainMessage.Text = "ラナ：じゃあ、最後の区画へ進みましょ、いよいよラストね♪";
                                        ok.ShowDialog();
                                        mainMessage.Text = "アイン：おっしゃ、ラストもサクサク解くぜ！";
                                        ok.ShowDialog();
                                        we.InfoArea25 = true;
                                        we.SolveArea25 = true;
                                        we.CompleteArea25 = true;
                                        ConstructDungeonMap();
                                        SetupDungeonMapping(2);
                                    }
                                    else
                                    {
                                        if (!we.CompleteArea26)
                                        {
                                            mainMessage.Text = "　　　　『この先、行き止まり。　最後の区画へ進むべし。』";
                                            mainMessage.Update();
                                        }
                                        else
                                        {
                                            mainMessage.Text = "看板はもう無くなっている。";
                                            mainMessage.Update();
                                        }
                                    }
                                    return;
                                    #endregion

                                case 28:
                                    #region "看板突破イベント 2-6"
                                    if (!we.SolveArea26)
                                    {
                                        if (!we.InfoArea26)
                                        {
                                            mainMessage.Text = "アイン：おし、これがラスト看板だな・・・なになに？";
                                            ok.ShowDialog();
                                            mainMessage.Text = "　　　　『この先、行き止まり。』";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：っな・・・どういう事よ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：おいおい・・・行き止まりとしか書いてねえぞ？";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：・・・ヒント無しじゃない。どうすんのよコレ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：まいったな。手の打ちようがなさそうだが。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：探索よ。きっと何かヒントがある筈でしょ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：そうだな、まずは周りを回ってみるとするか。";
                                            ok.ShowDialog();
                                            we.InfoArea26 = true;
                                        }
                                        else
                                        {
                                            if (knownTileInfo2[169] && knownTileInfo2[170] && knownTileInfo2[171] && knownTileInfo2[172] && knownTileInfo2[173] && knownTileInfo2[174] && knownTileInfo2[175] &&
                                                knownTileInfo2[199] && knownTileInfo2[200] && knownTileInfo2[201] && knownTileInfo2[202] && knownTileInfo2[203] && knownTileInfo2[204] && knownTileInfo2[205] &&
                                                knownTileInfo2[229] && knownTileInfo2[230] && knownTileInfo2[231] && knownTileInfo2[232] && knownTileInfo2[233] && knownTileInfo2[234] && knownTileInfo2[235] &&
                                                knownTileInfo2[259] && knownTileInfo2[260] && knownTileInfo2[261] && knownTileInfo2[262] && knownTileInfo2[263] && knownTileInfo2[264] && knownTileInfo2[265] &&
                                                knownTileInfo2[289] && knownTileInfo2[290] && knownTileInfo2[291] && knownTileInfo2[292] && knownTileInfo2[293] && knownTileInfo2[294] && knownTileInfo2[295])
                                            {
                                                if (!we.FailArea26)
                                                {
                                                    mainMessage.Text = "　　　　『この先、行き止まり。』";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：駄目だ。周ってみたが何もねえぞ。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：冗談でしょ、本当に行き止まりだって言うの？";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：・・・わかんねえ。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：・・・・・・降参よ。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：ああ、降参だなさすがに。どうする、一旦町へ戻るか？";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：ほんっと、疲れたわさすがに。一旦戻りましょう。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：一旦町に戻ったら、看板が変わってたりすると良いな。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：それは無いわね。最初と同じ仕掛けとは思えないわ。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：じゃあ、まあ一旦戻るか！【遠見の青水晶】を使おうぜ。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：ええ・・・そうしましょ。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "    『アイン達は、遠見の青水晶を覗き込んだ。ユングの町へとワープする』";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    we.FailArea26 = true;
                                                    CallHomeTown();
                                                }
                                                else
                                                {
                                                    mainMessage.Text = "　　　　『この先、行き止まり。』";
                                                }
                                            }
                                            else
                                            {
                                                mainMessage.Text = "　　　　『この先、行き止まり。』";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (!we.ProgressArea26)
                                        {
                                            mainMessage.Text = "　　　　『この先、行き止まり。』";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：さてと、ここからだな。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：アイン、本当に解答があるの？";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：わかんねえよ。でも、もう一度入念に探してみようぜ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：でも、何も見つけられなかったら？";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：そんときはもう一度、くまなく探すまでさ。任せとけって！";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：・・・うん、分かったわ。じゃ、手分けして探してみましょ♪";
                                            ok.ShowDialog();
                                            we.ProgressArea26 = true;
                                        }
                                        else
                                        {
                                            if (we.ProgressArea261 && we.ProgressArea262 && we.ProgressArea263 && !we.FailArea261)
                                            {
                                                mainMessage.Text = "　　　　『この先、行き止まり。』";
                                                ok.ShowDialog();
                                                mainMessage.Text = "ラナ：駄目ね。やっぱり何もないわ。";
                                                ok.ShowDialog();
                                                mainMessage.Text = "アイン：・・・いや、そうでもねえ。";
                                                ok.ShowDialog();
                                                mainMessage.Text = "ラナ：え？何か見つかったの？";
                                                ok.ShowDialog();
                                                mainMessage.Text = "アイン：いや、まだ答えは出ねえ。もう一回だ。探索を続けようぜ。";
                                                ok.ShowDialog();
                                                mainMessage.Text = "ラナ：ええ、良いわよ。アインがやるっていうなら私は止めないわ。";
                                                ok.ShowDialog();
                                                mainMessage.Text = "アイン：ああ、任せときな！";
                                                ok.ShowDialog();
                                                we.FailArea261 = true;
                                            }
                                            else
                                            {
                                                if (we.ProgressArea264 && we.ProgressArea265 && we.ProgressArea266 && we.ProgressArea267 && !we.FailArea262)
                                                {
                                                    mainMessage.Text = "　　　　『この先、行き止まり。』";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：アイン、どう。何か分かりそう？";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：いや、まだ駄目だ。わかんねえままだ。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "ラナ：アイン、頑張ってね。私、応援するぐらいしかできない見たいだし。";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "アイン：もう一回・・・もう一回だ。探索を続るぜ。";
                                                    ok.ShowDialog();
                                                    we.FailArea262 = true;
                                                }
                                                else
                                                {
                                                    if (we.ProgressArea268 && we.ProgressArea269 && we.ProgressArea2610 && we.ProgressArea2611 && !we.FailArea263)
                                                    {
                                                        mainMessage.Text = "　　　　『この先、行き止まり。』";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：アイン、疲れて来てない？汗びっしょりよ。一回、遠見の青水晶で戻ろうか？";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：駄目だ・・・１回戻ったら忘れちまう。今この場、この瞬間で解くしかねえ。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "ラナ：分かったわ、アイン。もう１回探索するのね。";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "アイン：ああ、もう一回だ。何度でも探索を続けるぜ。";
                                                        ok.ShowDialog();
                                                        we.FailArea263 = true;
                                                    }
                                                    else
                                                    {
                                                        if (we.ProgressArea2612 && we.ProgressArea2613 && we.ProgressArea2614 && we.ProgressArea2615 && !we.FailArea264)
                                                        {
                                                            mainMessage.Text = "　　　　『この先、行き止まり。』";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "ラナ：アイン・・・大丈夫？";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "アイン：うっせえ！うっせぇな・・・どれがホントなんだよ・・・くそ、黙ってろ！！";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "ラナ：アイン、呑まれちゃ駄目よ。私が傍に居るわ。気をしっかり。";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "アイン：あ、ああ、すまねえ。ヒントが無いなんて、冗談じゃねえぜ。";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "ラナ：どういう意味よ？";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "アイン：ここ・・・ヒントだらけだぜ。壁の隙間、床下の傷口、天井の挿絵、空間に当たる光の錯覚。";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "ラナ：ウソ、私・・・全然気付かないわ。";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "アイン：見えたり聞こえたりしてないのか？俺には自然に目と耳に直接入ってくるぜ。";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "ラナ：ううん、良いのよ。アインだけに見えたり聞こえたりしてるのかもしれないわ。";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "アイン：まあ良い。後は中央だ。全てにおける連続、全てにおける不連続に対する解が出来上がる。";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "ラナ：うん・・・頑張って。ヘンなこと言ってるし、どうしても疲れたらまた声かけてよね。";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "アイン：ああ、中央へ行くぜ。そこで俺は答えを示す。できる気がするんだ。";
                                                            ok.ShowDialog();
                                                            we.FailArea264 = true;
                                                        }
                                                        else
                                                        {
                                                            if (!we.ProgressArea2616)
                                                            {
                                                                mainMessage.Text = "　　　　『この先、行き止まり。』";
                                                            }
                                                            else
                                                            {
                                                                mainMessage.Text = "看板はもう無くなっている。";
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    return;

                                case 29:
                                    if (!we.ProgressArea261 &&
                                        !we.FailArea261 && we.SolveArea26)
                                    {
                                        mainMessage.Text = "アイン：・・・";
                                        ok.ShowDialog();
                                        we.ProgressArea261 = true;
                                    }
                                    return;

                                case 30:
                                    if (!we.ProgressArea262 &&
                                        !we.FailArea261 && we.SolveArea26)
                                    {
                                        mainMessage.Text = "アイン：・・・";
                                        ok.ShowDialog();
                                        we.ProgressArea262 = true;
                                    }
                                    return;
                                    
                                case 31:
                                    if (!we.ProgressArea263 &&
                                        !we.FailArea261 && we.SolveArea26)
                                    {
                                        mainMessage.Text = "アイン：・・・";
                                        ok.ShowDialog();
                                        we.ProgressArea263 = true;
                                    }
                                    return;

                                case 32:
                                    if (we.ProgressArea261 && we.ProgressArea262 && we.ProgressArea263 &&
                                        !we.ProgressArea264 && we.FailArea261 && !we.FailArea262 && we.SolveArea26)
                                    {
                                        mainMessage.Text = "アイン：・・・";
                                        ok.ShowDialog();
                                        we.ProgressArea264 = true;
                                    }
                                    return;

                                case 33:
                                    if (we.ProgressArea261 && we.ProgressArea262 && we.ProgressArea263 &&
                                        !we.ProgressArea265 && we.FailArea261 && !we.FailArea262 && we.SolveArea26)
                                    {
                                        mainMessage.Text = "アイン：・・・";
                                        ok.ShowDialog();
                                        we.ProgressArea265 = true;
                                    }
                                    return;

                                case 34:
                                    if (we.ProgressArea261 && we.ProgressArea262 && we.ProgressArea263 &&
                                        !we.ProgressArea266 && we.FailArea261 && !we.FailArea262 && we.SolveArea26)
                                    {
                                        mainMessage.Text = "アイン：・・・";
                                        ok.ShowDialog();
                                        we.ProgressArea266 = true;
                                    }
                                    return;

                                case 35:
                                    if (we.ProgressArea261 && we.ProgressArea262 && we.ProgressArea263 &&
                                        !we.ProgressArea267 && we.FailArea261 && !we.FailArea262 && we.SolveArea26)
                                    {
                                        mainMessage.Text = "アイン：・・・";
                                        ok.ShowDialog();
                                        we.ProgressArea267 = true;
                                    }
                                    return;

                                case 36:
                                    if (we.ProgressArea261 && we.ProgressArea262 && we.ProgressArea263 &&
                                        we.ProgressArea264 && we.ProgressArea265 && we.ProgressArea266 && we.ProgressArea267 &&
                                        !we.ProgressArea268 && we.FailArea262 && !we.FailArea263 && we.SolveArea26)
                                    {
                                        mainMessage.Text = "アイン：・・・";
                                        ok.ShowDialog();
                                        we.ProgressArea268 = true;
                                    }
                                    return;

                                case 37:
                                    if (we.ProgressArea261 && we.ProgressArea262 && we.ProgressArea263 &&
                                        we.ProgressArea264 && we.ProgressArea265 && we.ProgressArea266 && we.ProgressArea267 &&
                                        !we.ProgressArea269 && we.FailArea262 && !we.FailArea263 && we.SolveArea26)
                                    {
                                        mainMessage.Text = "アイン：・・・";
                                        ok.ShowDialog();
                                        we.ProgressArea269 = true;
                                    }                                   
                                    return;

                                case 38:
                                    if (we.ProgressArea261 && we.ProgressArea262 && we.ProgressArea263 &&
                                        we.ProgressArea264 && we.ProgressArea265 && we.ProgressArea266 && we.ProgressArea267 &&
                                        !we.ProgressArea2610 && we.FailArea262 && !we.FailArea263 && we.SolveArea26)
                                    {
                                        mainMessage.Text = "アイン：・・・";
                                        ok.ShowDialog();
                                        we.ProgressArea2610 = true;
                                    }
                                    return;

                                case 39:
                                    if (we.ProgressArea261 && we.ProgressArea262 && we.ProgressArea263 && 
                                        we.ProgressArea264 && we.ProgressArea265 && we.ProgressArea266 && we.ProgressArea267 &&
                                        !we.ProgressArea2611 && we.FailArea262 && !we.FailArea263 && we.SolveArea26)
                                    {
                                        mainMessage.Text = "アイン：・・・";
                                        ok.ShowDialog();
                                        we.ProgressArea2611 = true;
                                    }
                                    return;

                                case 40:
                                    if (we.ProgressArea261 && we.ProgressArea262 && we.ProgressArea263 &&
                                        we.ProgressArea264 && we.ProgressArea265 && we.ProgressArea266 && we.ProgressArea267 &&
                                        we.ProgressArea268 && we.ProgressArea269 && we.ProgressArea2610 && we.ProgressArea2611 &&
                                        !we.ProgressArea2612 && we.FailArea263 && !we.FailArea264 && we.SolveArea26)
                                    {
                                        mainMessage.Text = "アイン：・・・";
                                        ok.ShowDialog();
                                        we.ProgressArea2612 = true;
                                    }
                                    return;

                                case 41:
                                    if (we.ProgressArea261 && we.ProgressArea262 && we.ProgressArea263 &&
                                        we.ProgressArea264 && we.ProgressArea265 && we.ProgressArea266 && we.ProgressArea267 &&
                                        we.ProgressArea268 && we.ProgressArea269 && we.ProgressArea2610 && we.ProgressArea2611 &&
                                        !we.ProgressArea2613 && we.FailArea263 && !we.FailArea264 && we.SolveArea26)
                                    {
                                        mainMessage.Text = "アイン：・・・";
                                        ok.ShowDialog();
                                        we.ProgressArea2613 = true;
                                    }
                                    return;

                                case 42:
                                    if (we.ProgressArea261 && we.ProgressArea262 && we.ProgressArea263 &&
                                        we.ProgressArea264 && we.ProgressArea265 && we.ProgressArea266 && we.ProgressArea267 &&
                                        we.ProgressArea268 && we.ProgressArea269 && we.ProgressArea2610 && we.ProgressArea2611 &&
                                        !we.ProgressArea2614 && we.FailArea263 && !we.FailArea264 && we.SolveArea26)
                                    {
                                        mainMessage.Text = "アイン：・・・";
                                        ok.ShowDialog();
                                        we.ProgressArea2614 = true;
                                    }
                                    return;

                                case 43:
                                    if (we.ProgressArea261 && we.ProgressArea262 && we.ProgressArea263 &&
                                        we.ProgressArea264 && we.ProgressArea265 && we.ProgressArea266 && we.ProgressArea267 &&
                                        we.ProgressArea268 && we.ProgressArea269 && we.ProgressArea2610 && we.ProgressArea2611 &&
                                        !we.ProgressArea2615 && we.FailArea263 && !we.FailArea264 && we.SolveArea26)
                                    {
                                        mainMessage.Text = "アイン：・・・";
                                        ok.ShowDialog();
                                        we.ProgressArea2615 = true;
                                    }
                                    return;

                                case 44:
                                    if (we.ProgressArea261 && we.ProgressArea262 && we.ProgressArea263 &&
                                        we.ProgressArea264 && we.ProgressArea265 && we.ProgressArea266 && we.ProgressArea267 &&
                                        we.ProgressArea268 && we.ProgressArea269 && we.ProgressArea2610 && we.ProgressArea2611 &&
                                        we.ProgressArea2612 && we.ProgressArea2613 && we.ProgressArea2614 && we.ProgressArea2615 &&
                                        we.FailArea261 && we.FailArea262 && we.FailArea263 && we.FailArea264 && we.SolveArea26)
                                    {
                                        if (!we.ProgressArea2616)
                                        {
                                            GroundOne.StopDungeonMusic();

                                            mainMessage.Text = "　　　　『絶対試練：汝、答えを示せ。』";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：アイン・・・頑張って・・・";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：任せとけ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "　　　　『突如台座がアインの前に浮き上がった。直後、薄黄色い空間にアインが包まれた。』";
                                            ok.ShowDialog();

                                            GroundOne.PlayDungeonMusic(Database.BGM09, Database.BGM09LoopBegin);
                                            
                                            mainMessage.Text = "アイン：【壱　鳥が歌い、木々が囁き始める】";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：【弐　天は青く照らし、地は新緑を謳歌する】";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：【参　闇が夜へと誘い、光が昼へと道を示す】";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：【四　水、流れ落ち、偉大なる海、天へと還り、無限循環】";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：【五　火、あらゆる場所、可能な場を生めつくし、創元浄化】";
                                            ok.ShowDialog();
                                            mainMessage.Text = "　　　　『アインを包んでいる空間が濃い赤色へと激しく変わった！！』";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：アイン・・・大丈夫・・・大丈夫よね・・・";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：【六　嵐、万物なる生成要素、一から零へと変化させる】";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：【七　死、この世における絶対的な平等の象徴】";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：【八　生、偉大なる母、厳格なる父より永久の確約】";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：【九　神、全創生、全法則、全にして無条件の存在】";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：【十　人、誤り、恐れ、喚き、屈し、失い、揺らぎ続ける存在】";
                                            ok.ShowDialog();
                                            mainMessage.Text = "　　　　『アインを包んでいる空間が黒褐色へと激しく変わった！！』";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：ちょっと・・・何も見えないよ・・・死なないでよ・・・";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：【十壱　理、神と人、鳥、木々、全生物における連続の理そこに見つけたり】";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：【十弐　空、在るべきもの、成るべくして成り、在るべくして現存と見つけたり】";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：【十参　相、完全調和への導き、交わることの無い絶対双極、見つけたり】";
                                            ok.ShowDialog();
                                            mainMessage.Text = "　　　　『アインを包んでいる空間が真っ白な純白色へと激しく変わった！！』";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：・・・まだあるの？早く・・・早く終わらせてあげてよ・・・！";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：【十四　永遠、終わらない所へ、終わりと始まりが連続する永遠環】";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：【終極　世界、あなた、そしてわたしが居た場所へ。無限に続くこの世界】";
                                            ok.ShowDialog();

                                            GroundOne.StopDungeonMusic();

                                            mainMessage.Text = "　　　　『空間が激しくフラッシュし、凝縮された空間へと連続的に小さくなる！！！』";
                                            ok.ShowDialog();
                                            mainMessage.Text = "　　　　『パパパパパパパ！！！！ッバシュウウウゥゥゥン！！！！！！！』";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：キャアアアアア！！！！";
                                            ok.ShowDialog();
                                            mainMessage.Text = "　　　　『空間は弾け飛んだ後、台座の前にアインの倒れた姿があった』";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：・・・っつぅ・・・";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：アイン！！大丈夫なの！？しっかりしてよ！！！";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：・・・ぉ・・・おお、おお、大丈夫だ。いってててて・・・";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：アイン！！あんた、バカじゃないの！？変なことばっかりいってさ・・・";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：い、いやいや、その辺にあった法則に従ってだな・・・";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：バカじゃないの！？死んだらどうするのよ！？何が法則よ！バカじゃないの！！バカ！！！";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：バカでしょ！？やっぱりバカよ！！バカバカバカ！！！";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：いてっ！いってぇって！叩くなって。いってえなあ・・・ッハハハ";
                                            ok.ShowDialog();
                                            using (MessageDisplay md = new MessageDisplay())
                                            {
                                                md.StartPosition = FormStartPosition.CenterParent;
                                                md.Message = "アインとラナはしばらくその場で休息を取った。";
                                                md.ShowDialog();
                                            }
                                            we.ProgressArea2616 = true;
                                            we.CompleteArea26 = true;
                                            ConstructDungeonMap();
                                            SetupDungeonMapping(2);
                                            mainMessage.Text = "ラナ：・・・見て、道が開けているわ・・・";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：さすが、俺だろ。ッハッハッハ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：ホント、何回も何回も何を見てたのよ？この部屋で。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：歌だ。歌が聞こえて来たんだよ、壁の中や床下の傷を一つ見つけるたびに。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：歌？へえ、どんな歌だったの？";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：全然聞いた事無いけどな。不思議と昔から知ってるような感じだったぜ。すげえ綺麗だった。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：それを繋げると、あんなヘンテコな言葉になったっていうわけ？";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：正直な所、あの言葉は俺が喋ってたんじゃねえ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：何言ってるのよ、間違いなくアインの声だったわ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：いや、言ってるのは確かに俺だ。だが、俺はあんな言葉は紡ぎ出せねえ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：誰かがアインに言わせたっていうの？";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：そういうわけでもねえ。。。すまねえ、ホントによくわからねえんだ。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：んまあ、良いわ。とにかくおめでとう。";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：っあ！忘れてたわ！！";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：うお、何だよ！？急に立ち上がって。。。っあ！しまったぜ！！";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アインとラナ：ひょっとして、この先ボスが居るん（じゃねえか）（じゃないの）！？";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：いや、大丈夫さ。今の俺達ならやれる。そうだろ？";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：そうね、ボスなんかちゃっちゃっと倒しちゃいましょ♪";
                                            ok.ShowDialog();
                                            mainMessage.Text = "アイン：おっしゃ、休むのはもう終わりだ。この２階制覇するぜ！！";
                                            ok.ShowDialog();
                                            mainMessage.Text = "ラナ：ええ、行きましょう。階段はもうすぐソコよ♪";
                                            ok.ShowDialog();
                                            GroundOne.PlayDungeonMusic(Database.BGM02, Database.BGM02LoopBegin);
                                        }
                                        else
                                        {
                                            mainMessage.Text = "台座にはこう書かれている。　『汝、汝自身の答え、見つけたり。次へ進め。』";
                                        }
                                    }
                                    return;
                                    #endregion

                                case 45:
                                    if (!we.InfoArea27)
                                    {
                                        UpdateMainMessage("アイン：うおっとと！な、なんだココは！");

                                        UpdateMainMessage("ラナ：アイン！？ちょっと何やってるの！？壁にめり込んじゃったワケ！？");

                                        UpdateMainMessage("アイン：壁の色が妙に他と違ってたからさ。試しに触ろうとしたら、すり抜けたぜ。");

                                        UpdateMainMessage("ラナ：わ、私もすり抜けらるかしら？");

                                        UpdateMainMessage("アイン：ああ、大丈夫じゃねえか？単なる見せかけの壁だ。早く来いよ。");

                                        UpdateMainMessage("ラナ：じゃあ行ってみるわね・・・");

                                        UpdateMainMessage("ラナ：っよっと・・・ホント、抜けられたわ♪");

                                        UpdateMainMessage("アイン：なあ、この先きっとスゲエお宝があるに違いないぜ！！");

                                        UpdateMainMessage("ラナ：なにそんなバカみたいにはしゃいでるのよ。っさ、行くわよ。");

                                        UpdateMainMessage("アイン：おっ宝お宝！！");

                                        we.InfoArea27 = true;
                                    }
                                    return;

                                case 46:
                                    if (!we.SpecialInfo2)
                                    {
                                        UpdateMainMessage("アイン：っち・・・行き止まりか・・・何も無かったのかよ。");

                                        GroundOne.StopDungeonMusic();
                                        this.BackColor = Color.Black;
                                        UpdateMainMessage("　　　【その瞬間、アインの脳裏に激しい激痛が襲った！周囲の感覚が麻痺する！！】");

                                        UpdateMainMessage("アイン：ッグア！！・・・あ、頭が・・・！！");

                                        UpdateMainMessage("　　＜＜＜　どこで落としたんだ？ ＞＞＞");

                                        UpdateMainMessage("アイン：ッツツツ・・落としたって・・・な・・・に・・・が・・・");

                                        UpdateMainMessage("　　＜＜＜　盗まれたのだとしたら、どこだ？　＞＞＞");

                                        UpdateMainMessage("アイン：盗まれ・・・何を・・・グ、グアア！！！");

                                        UpdateMainMessage("　　＜＜＜　２階から３階へ行く所だったとしたら？　＞＞＞");

                                        UpdateMainMessage("アイン：し、知るか・・・って・・・");

                                        UpdateMainMessage("　　＜＜＜　クリアと終焉には向かうな。詩とキーを見つけろ。　＞＞＞");

                                        UpdateMainMessage("アイン：何？・・・・ど、どういう・・・");

                                        UpdateMainMessage("　　　【アインに対する激しい激痛は少しずつ引いていった。】");

                                        this.BackColor = Color.RoyalBlue;

                                        UpdateMainMessage("ラナ：アイン・・・アイン！！");

                                        UpdateMainMessage("アイン：・・・ん、んん・・・よう");

                                        UpdateMainMessage("ラナ：アンタ何でこんな所で倒れてんのよ？");

                                        UpdateMainMessage("アイン：いや、倒れてたわけじゃねえ。何か聞こえてきたんだ。");

                                        UpdateMainMessage("ラナ：実際倒れてたのに、何言ってるんだか。何を聞いたのよ？");

                                        UpdateMainMessage("アイン：何だったかな・・・「盗まれたのはどこだ」とか");

                                        UpdateMainMessage("アイン：クリアに向かうな、詩と・・・ッツ！・・・イッテテテ・・・");

                                        UpdateMainMessage("アイン：せっかくお宝だと思ったのに、何だか拍子抜けだぜ。");

                                        UpdateMainMessage("ラナ：まあ幸い害も無さそうだし、戻りましょ。");

                                        UpdateMainMessage("アイン：ちぇ・・・お宝・・・");

                                        GroundOne.PlayDungeonMusic(Database.BGM02, Database.BGM02LoopBegin);
                                        we.SpecialInfo2 = true;
                                    }
                                    return;

                                default:
                                    mainMessage.Text = "アイン：ん？特に何もなかったと思うが。";
                                    return;
                            }
                        }
                    }
                }
                #endregion
                #region "ダンジョン３階イベント"
                else if (we.DungeonArea == 3)
                {
                    for (int ii = 0; ii < 58; ii++)
                    {
                        if (CheckTriggeredEvent(ii))
                        {
                            detectEvent = true;
                            switch (ii)
                            {
                                case 0:
                                    mainMessage.Text = "アイン：２階へ戻る階段だな。ここは一旦戻るか？";
                                    using (YesNoRequestMini ynr = new YesNoRequestMini())
                                    {
                                        ynr.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                        ynr.ShowDialog();
                                        if (ynr.DialogResult == DialogResult.Yes)
                                        {
                                            SetupDungeonMapping(2);
                                            mainMessage.Text = "";
                                        }
                                        else
                                        {
                                            mainMessage.Text = "";
                                        }
                                    }
                                    return;
                                case 1:
                                    if (!we.TruthWord3)
                                    {
                                        mainMessage.Text = "アイン：看板があるな・・・なになに？";
                                        ok.ShowDialog();
                                        mainMessage.Text = "　　　　『真実の言葉３：　　救われる事、報われる事』";
                                        ok.ShowDialog();
                                        mainMessage.Text = "アイン：救われる？誰のことを書いてんだ、コレは。";
                                        we.TruthWord3 = true;
                                    }
                                    else
                                    {
                                        UpdateMainMessage("　　　　『真実の言葉３：　　救われる事、報われる事』");
                                    }
                                    return;
                                case 2:
                                    mainMessage.Text = "アイン：よっしゃ４階への階段！降りてみようぜ？";
                                    using (YesNoRequestMini ynr = new YesNoRequestMini())
                                    {
                                        bool tempCompleteArea3 = we.CompleteArea3;
                                        ynr.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                        ynr.ShowDialog();
                                        if (ynr.DialogResult == DialogResult.Yes)
                                        {
                                            SetupDungeonMapping(4);
                                            mainMessage.Text = "";
                                            if (!tempCompleteArea3)
                                            {
                                                UpdateMainMessage("アイン：３階制覇完了っと！ラナ、ヴェルゼ、ユングの町へ一回帰還しておこうぜ。");

                                                UpdateMainMessage("ヴェルゼ：ええ、ボクは賛成ですよ。");

                                                UpdateMainMessage("ラナ：じゃあ早速戻りましょ。");

                                                UpdateMainMessage("アイン：おし、じゃあこの遠見の青水晶で！");
                                                CallHomeTown();
                                            }
                                        }
                                        else
                                        {
                                            mainMessage.Text = "";
                                        }
                                    }
                                    return;
                                case 3:
                                    this.Update();
                                    mainMessage.Text = "アイン：ボスとの戦闘だ！気を引き締めていくぜ！";
                                    mainMessage.Update();
                                    bool result = EncountBattle("三階の守護者：Minflore");
                                    if (!result)
                                    {
                                        UpdatePlayerLocationInfo(this.Player.Location.X, this.Player.Location.Y - moveLen);
                                    }
                                    else
                                    {
                                        we.CompleteSlayBoss3 = true;
                                    }
                                    mainMessage.Text = "";
                                    return;

                                case 4:
                                    we.Treasure8 = GetTreasure("レッドマテリアル");
                                    return;

                                case 5:
                                    we.Treasure9 = GetTreasure("ライオンハート");
                                    return;

                                case 6:
                                    we.Treasure10 = GetTreasure("オーガの腕章");
                                    return;

                                case 7:
                                    we.Treasure11 = GetTreasure("鋼鉄の石像");
                                    return;

                                case 8:
                                    we.Treasure12 = GetTreasure("ファラ様信仰のシール");
                                    return;

                                case 9:
                                    #region "第一関門"
                                    if (!we.InfoArea31)
                                    {
                                        UpdateMainMessage("アイン：うお！いきなり看板があるぜ。また質問は勘弁だな・・・");

                                        UpdateMainMessage("ヴェルゼ：「また」とは、どういう意味です？");

                                        UpdateMainMessage("ラナ：２階では立て続けに謎かけ看板が出てきて、それに解答する方式だったんですよ。");

                                        UpdateMainMessage("ヴェルゼ：なるほど、でもこの看板は質問というわけでは無さそうですよ。");

                                        UpdateMainMessage("　　　　『鏡から鏡へ。感じたままに触れよ。感じたままに進め。』");

                                        UpdateMainMessage("アイン：鏡から鏡？ほら見ろ・・・やっぱ謎かけじゃねえか。");

                                        UpdateMainMessage("ラナ：感じたままに触れて進めとも書いてあるわ。確かに質問ではなさそうね。");

                                        UpdateMainMessage("ヴェルゼ：ここは本当に面白いですね。ボクが来た時と全然内容が違います。");

                                        UpdateMainMessage("ラナ：えっ、そうなんですか？");

                                        UpdateMainMessage("ヴェルゼ：このダンジョンは、メンバー、パーティ構成によって内容が変わるんですよ");

                                        UpdateMainMessage("アイン：確かにそうかもな。だって俺一人で最初入った時は謎解きなんて無かったぜ。");

                                        UpdateMainMessage("ラナ：そりゃ、アインがバカだから１人で謎解きなんてさせたら、ゼッタイ解けないし♪");

                                        UpdateMainMessage("アイン：ッチ・・・バカはバカなりに考えてんだよ。謎解きぐらい何ともねえっての。");

                                        UpdateMainMessage("ラナ：「バカはバカなりに」って・・・まあ良いわ。今回は謎解きじゃなさそうだしね。");

                                        UpdateMainMessage("ヴェルゼ：さて、進みましょう。鏡を探せば良いという事でしょう。");

                                        UpdateMainMessage("アイン：ああ、じゃあさっそく探索して鏡を探して行こうぜ！");
                                        we.InfoArea31 = true;
                                    }
                                    else
                                    {
                                        if (!we.CompleteArea34)
                                        {
                                            UpdateMainMessage("　　　　『鏡から鏡へ。感じたままに触れよ。感じたままに進め。』", true);
                                        }
                                        else
                                        {
                                            UpdateMainMessage("看板はもう無くなっている。", true);
                                        }
                                    }
                                    return;

                                case 10:
                                    if (!we.InfoArea311S)
                                    {
                                        if (!we.InfoArea312S && !we.InfoArea313S)
                                        {
                                            UpdateMainMessage("ラナ：あっ、ちょっとアイン。");

                                            UpdateMainMessage("アイン：ん？何だ？");

                                            UpdateMainMessage("ラナ：あったわよ、ホラそこに鏡。");

                                            UpdateMainMessage("アイン：？・・・おお、ホントだ。よく見つけたなラナ。");

                                            UpdateMainMessage("ヴェルゼ：こ、っこれは！！");

                                            UpdateMainMessage("アイン：ん？ヴェルゼどうかしたのか？");

                                            UpdateMainMessage("ヴェルゼ：この形、ファージル宮殿内にある【秤の三面鏡】にそっくりです。");

                                            UpdateMainMessage("ラナ：秤の三面鏡って聞いたことがあるわ。本人の想いを計るとか。");

                                            UpdateMainMessage("ヴェルゼ：そのとおり。本人が迷い、思考・判断を整理したい時によく使われています。");

                                            UpdateMainMessage("アイン：よし、では早速・・・感じるままに覗いてみるか。");

                                            UpdateMainMessage("アイン：・・・何も起きないぞ。ただの鏡じゃねえか？");

                                            UpdateMainMessage("ラナ：迷ったり、悩んでる人じゃないと駄目なんじゃない？アインじゃ元々無理よ。");

                                            UpdateMainMessage("アイン：ラナ、俺だって時として悩んだりするんだ。");

                                            UpdateMainMessage("ラナ：そういう事言ってる間は、悩んでるうちに入らないわよ。");

                                            UpdateMainMessage("ヴェルゼ：・・・ボクでも駄目みたいですね。すみませんが何も起きません。");

                                            UpdateMainMessage("ラナ：ヴェルゼさんは洗練されてるんですよ♪　卓越してる人も駄目みたい。");

                                            UpdateMainMessage("アイン：どうしてそこで評価がまったく逆になってんだよ。");

                                            UpdateMainMessage("アイン：っと、じゃあ残るはラナ、おまえだぜ？");

                                            UpdateMainMessage("ラナ：・・・何がよ？");

                                            UpdateMainMessage("アイン：俺、ヴェルゼとくれば後はお前だけだろ。何が、「何がよ？」なんだよ。");

                                            UpdateMainMessage("ラナ：・・・え、ぇ、ええっとね。");

                                            UpdateMainMessage("アイン：何戸惑ってんだ。駄目元で良いからやってみてくれよ？");

                                            UpdateMainMessage("ヴェルゼ：アイン君、女の子にそういう言い方は失礼ですよ。");

                                            UpdateMainMessage("ラナ：あ、ヴェルゼさん、良いんです。じゃ、やってみるわよ。");

                                            UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");

                                            UpdateMainMessage("アイン：うわっ、ま、眩し！！");

                                            UpdatePlayerLocationInfo(basePosX + moveLen * 5, basePosY + moveLen * 7);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("　　　『ッバシュ！！！』　　");

                                            UpdateMainMessage("アイン：・・・ん？か、鏡が無くなってるぞ。");

                                            UpdateMainMessage("ヴェルゼ：いや、鏡が無くなったわけじゃなさそうです。");

                                            UpdateMainMessage("ラナ：ダンジョン壁の構成が違う・・・どこか違う場所に来たのね。");

                                            UpdateMainMessage("アイン：飛ばされたって事か！？");

                                            UpdateMainMessage("ヴェルゼ：どうやらそのようですね。一種のワープ装置みたいなものでしょうか。");

                                            UpdateMainMessage("アイン：ちくしょう、これじゃラナのダンジョンマップメモも役に立たないんじゃないか？");

                                            UpdateMainMessage("ラナ：同じ場所に飛ばされるわけじゃないから役に立たないワケじゃないけど。");

                                            UpdateMainMessage("ラナ：でも、道のりに進めて埋めていく方法が通用しないわね。");

                                            UpdateMainMessage("アイン：厄介だな。でも進むしか道はねえな。");

                                            UpdateMainMessage("ヴェルゼ：幸いここは一本道に見えます。行きましょう。");
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ラナ：あっ、ここに鏡が設置されてるわね。");

                                            UpdateMainMessage("ヴェルゼ：ラナさん、鏡をお願いします。");

                                            UpdateMainMessage("アイン：別の所だからな。一応警戒しながら進もうぜ。");

                                            UpdateMainMessage("ラナ：ええ、じゃ行くわよ。");

                                            UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");

                                            UpdatePlayerLocationInfo(basePosX + moveLen * 5, basePosY + moveLen * 7);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                        }

                                        we.InfoArea311S = true;
                                    }
                                    else
                                    {
                                        if (!we.CompleteArea32)
                                        {
                                            UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                            UpdatePlayerLocationInfo(basePosX + moveLen * 5, basePosY + moveLen * 7);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                        }
                                        else
                                        {
                                            if (!we.CompleteArea34)
                                            {
                                                UpdateMainMessage("ラナ：ここは一度突破しているわ。どうする、また入ってみる？");

                                                using (YesNoRequestMini yesno = new YesNoRequestMini())
                                                {
                                                    yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                                    yesno.ShowDialog();
                                                    if (yesno.DialogResult == DialogResult.Yes)
                                                    {
                                                        UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                                        UpdatePlayerLocationInfo(basePosX + moveLen * 5, basePosY + moveLen * 7);
                                                        SetupDungeonMapping(3, false);
                                                        UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                                    }
                                                    else
                                                    {
                                                        UpdateMainMessage("", true);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (!we.CompleteJumpArea34)
                                                {
                                                    we.CompleteJumpArea34 = true;
                                                    UpdateMainMessage("ラナ：そうそう、台座の所で教えてもらった行き先なんだけど。");

                                                    UpdateMainMessage("アイン：ん、なんだ？");

                                                    UpdateMainMessage("ラナ：この鏡からでも行けるようになったみたいね♪");

                                                    UpdateMainMessage("アイン：おお、やるじゃねえか！神様も捨てたもんじゃねえな！");

                                                    UpdateMainMessage("ラナ：どうするのよ。直接行く？");
                                                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                                                    {
                                                        yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                                        yesno.ShowDialog();
                                                        if (yesno.DialogResult == DialogResult.Yes)
                                                        {
                                                            UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                                            UpdatePlayerLocationInfo(basePosX + moveLen * 3, basePosY + moveLen * 9);
                                                            SetupDungeonMapping(3, false);
                                                            UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                                        }
                                                        else
                                                        {
                                                            UpdateMainMessage("", true);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    UpdateMainMessage("ラナ：最後の部屋へ直接行く？");
                                                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                                                    {
                                                        yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                                        yesno.ShowDialog();
                                                        if (yesno.DialogResult == DialogResult.Yes)
                                                        {
                                                            UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                                            UpdatePlayerLocationInfo(basePosX + moveLen * 3, basePosY + moveLen * 9);
                                                            SetupDungeonMapping(3, false);
                                                            UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                                        }
                                                        else
                                                        {
                                                            UpdateMainMessage("ラナ：最初ワープしていた地点へ入ってみる？");

                                                            yesno.ShowDialog();
                                                            if (yesno.DialogResult == DialogResult.Yes)
                                                            {
                                                                UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                                                UpdatePlayerLocationInfo(basePosX + moveLen * 5, basePosY + moveLen * 7);
                                                                SetupDungeonMapping(3, false);
                                                                UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                                            }
                                                            else
                                                            {
                                                                UpdateMainMessage("", true);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    return;
                                    
                                case 11:
                                    if (!we.InfoArea311E)
                                    {
                                        if (!we.InfoArea312E && !we.InfoArea312E)
                                        {
                                            UpdateMainMessage("ラナ：あった、ここにも鏡が設置されているわ。");

                                            UpdateMainMessage("アイン：しかし、見つけるのが早いなラナ。言われて見れば分かるが。");

                                            UpdateMainMessage("ヴェルゼ：容は違えど、ボク達の時も唯一女性であるファラが見つけるのは早かったです。");

                                            UpdateMainMessage("アイン：ファラ・・・ファラ王妃の事ですか？");

                                            UpdateMainMessage("ヴェルゼ：ええ、ファラも直感が利く人でした。");

                                            UpdateMainMessage("ヴェルゼ：皆が見落としそうなモノを通り過ぎず、気付き、拾い救うように、見つけてあげるんです。");

                                            UpdateMainMessage("ラナ：あ、そういえば、ヴェルゼさんとファラ様って同い年なんですか？");

                                            UpdateMainMessage("ヴェルゼ：そうですよ。FiveSeekerの５人は皆同い年です。");

                                            UpdateMainMessage("ラナ：え、えーそうなんだ！？ちょっとビックリ・・・");

                                            UpdateMainMessage("アイン：ランディのボケも同い年って事か？");

                                            UpdateMainMessage("ヴェルゼ：はい、残念ながらそういう事になります。");

                                            UpdateMainMessage("アイン：ッケ、アイツはガキッぽいクセに意外と年は食ってんだな。");

                                            UpdateMainMessage("ラナ：また、ファラ様とかのお話、宿屋で聞かせてくださいね♪");

                                            UpdateMainMessage("ヴェルゼ：・・・ラナさん、鏡をお願いします。");

                                            UpdateMainMessage("ラナ：え？え、ええ・・・じゃ、鏡を発動させるわね。");

                                            UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");

                                            UpdatePlayerLocationInfo(basePosX + moveLen * 29, basePosY + moveLen * 1);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("　　　『ッバシュ！！！』　　");

                                            UpdateMainMessage("アイン：お？まだ鏡は傍にあるぜ。失敗したんじゃねえのか？");

                                            UpdateMainMessage("ラナ：ここは・・・どうやら元の位置に戻ったみたいね。");

                                            UpdateMainMessage("ヴェルゼ：この調子で先へ進んでみましょう。");

                                            UpdateMainMessage("アイン：ああ、まだまだ鏡はあるかも知れねえが、とりあえずガンガン進むぜ！");
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ラナ：鏡、あったわよ。");

                                            UpdateMainMessage("アイン：ラナ、頼んだぜ。");

                                            UpdateMainMessage("ラナ：ええ、じゃ行くわよ。");

                                            UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");

                                            UpdatePlayerLocationInfo(basePosX + moveLen * 29, basePosY + moveLen * 1);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                        }

                                        we.InfoArea311E = true;
                                    }
                                    else
                                    {
                                        UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");

                                        UpdatePlayerLocationInfo(basePosX + moveLen * 29, basePosY + moveLen * 1);
                                        SetupDungeonMapping(3, false);
                                        UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    }
                                    return;

                                case 12:
                                    if (!we.InfoArea312S)
                                    {
                                        if (!we.InfoArea311S && !we.InfoArea313S)
                                        {
                                            UpdateMainMessage("ラナ：あっ、ちょっとアイン。");

                                            UpdateMainMessage("アイン：ん？何だ？");

                                            UpdateMainMessage("ラナ：あったわよ、ホラそこに鏡。");

                                            UpdateMainMessage("アイン：？・・・おお、ホントだ。よく見つけたなラナ。");

                                            UpdateMainMessage("ヴェルゼ：こ、っこれは！！");

                                            UpdateMainMessage("アイン：ん？ヴェルゼどうかしたのか？");

                                            UpdateMainMessage("ヴェルゼ：この形、ファージル宮殿内にある【秤の三面鏡】にそっくりです。");

                                            UpdateMainMessage("ラナ：秤の三面鏡って聞いたことがあるわ。本人の想いを計るとか。");

                                            UpdateMainMessage("ヴェルゼ：そのとおり。本人が迷い、思考・判断を整理したい時によく使われています。");

                                            UpdateMainMessage("アイン：よし、では早速・・・感じるままに覗いてみるか。");

                                            UpdateMainMessage("アイン：・・・何も起きないぞ。ただの鏡じゃねえか？");

                                            UpdateMainMessage("ラナ：迷ったり、悩んでる人じゃないと駄目なんじゃない？アインじゃ元々無理よ。");

                                            UpdateMainMessage("アイン：ラナ、俺だって時として悩んだりするんだ。");

                                            UpdateMainMessage("ラナ：そういう事言ってる間は、悩んでるうちに入らないわよ。");

                                            UpdateMainMessage("ヴェルゼ：・・・ボクでも駄目みたいですね。すみませんが何も起きません。");

                                            UpdateMainMessage("ラナ：ヴェルゼさんは洗練されてるんですよ。卓越してる人も駄目みたい。");

                                            UpdateMainMessage("アイン：どうしてそこで評価がまったく逆になってんだよ。");

                                            UpdateMainMessage("アイン：っと、じゃあ残るはラナ、おまえだぜ？");

                                            UpdateMainMessage("ラナ：・・・何がよ？");

                                            UpdateMainMessage("アイン：俺、ヴェルゼとくれば後はお前だけだろ。何が、「何がよ？」なんだよ。");

                                            UpdateMainMessage("ラナ：・・・え、ぇ、ええっとね。");

                                            UpdateMainMessage("アイン：何戸惑ってんだ。駄目元で良いからやってみてくれよ？");

                                            UpdateMainMessage("ヴェルゼ：アイン君、女の子にそういう言い方は失礼ですよ。");

                                            UpdateMainMessage("ラナ：あ、ヴェルゼさん、良いんです。じゃ、やってみるわよ。");

                                            UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");

                                            UpdateMainMessage("アイン：うわっ、ま、眩し！！");

                                            UpdatePlayerLocationInfo(basePosX + moveLen * 14, basePosY + moveLen * 19);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("　　　『ッバシュ！！！』　　");

                                            UpdateMainMessage("アイン：・・・ん？か、鏡が無くなってるぞ。");

                                            UpdateMainMessage("ヴェルゼ：いや、鏡が無くなったわけじゃなさそうです。");

                                            UpdateMainMessage("ラナ：ダンジョン壁の構成が違う・・・どこか違う場所に来たのね。");

                                            UpdateMainMessage("アイン：飛ばされたって事か！？");

                                            UpdateMainMessage("ヴェルゼ：どうやらそのようですね。一種のワープ装置みたいなものでしょうか。");

                                            UpdateMainMessage("アイン：ちくしょう、これじゃラナのダンジョンマップメモも役に立たないんじゃないか？");

                                            UpdateMainMessage("ラナ：同じ場所に飛ばされるわけじゃないから役に立たないワケじゃないけど。");

                                            UpdateMainMessage("ラナ：でも、道のりに進めて埋めていく方法が通用しないわね。");

                                            UpdateMainMessage("アイン：厄介だな。でも進むしか道はねえな。");

                                            UpdateMainMessage("ヴェルゼ：幸いここは一本道に見えます。行きましょう。");
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ラナ：あっ、ここに鏡が設置されてるわね。");

                                            UpdateMainMessage("ヴェルゼ：ラナさん、鏡をお願いします。");

                                            UpdateMainMessage("アイン：別の所だからな。一応警戒しながら進もうぜ。");

                                            UpdateMainMessage("ラナ：ええ、じゃ行くわよ。");

                                            UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");

                                            UpdatePlayerLocationInfo(basePosX + moveLen * 14, basePosY + moveLen * 19);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                        }
                                        we.InfoArea312S = true;
                                    }
                                    else
                                    {
                                        if (!we.CompleteArea32)
                                        {
                                            UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                            UpdatePlayerLocationInfo(basePosX + moveLen * 14, basePosY + moveLen * 19);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                        }
                                        else
                                        {
                                            if (!we.CompleteArea34)
                                            {
                                                UpdateMainMessage("ラナ：ここは一度突破しているわ。どうする、また入ってみる？");

                                                using (YesNoRequestMini yesno = new YesNoRequestMini())
                                                {
                                                    yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                                    yesno.ShowDialog();
                                                    if (yesno.DialogResult == DialogResult.Yes)
                                                    {
                                                        UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                                        UpdatePlayerLocationInfo(basePosX + moveLen * 14, basePosY + moveLen * 19);
                                                        SetupDungeonMapping(3, false);
                                                        UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                                    }
                                                    else
                                                    {
                                                        UpdateMainMessage("", true);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (!we.CompleteJumpArea34)
                                                {
                                                    we.CompleteJumpArea34 = true;
                                                    UpdateMainMessage("ラナ：そうそう、台座の所で教えてもらった行き先なんだけど。");

                                                    UpdateMainMessage("アイン：ん、なんだ？");

                                                    UpdateMainMessage("ラナ：この鏡からでも行けるようになったみたいね♪");

                                                    UpdateMainMessage("アイン：おお、やるじゃねえか！神様も捨てたもんじゃねえな！");

                                                    UpdateMainMessage("ラナ：どうするのよ。直接行く？");
                                                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                                                    {
                                                        yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                                        yesno.ShowDialog();
                                                        if (yesno.DialogResult == DialogResult.Yes)
                                                        {
                                                            UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                                            UpdatePlayerLocationInfo(basePosX + moveLen * 3, basePosY + moveLen * 9);
                                                            SetupDungeonMapping(3, false);
                                                            UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                                        }
                                                        else
                                                        {
                                                            UpdateMainMessage("", true);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    UpdateMainMessage("ラナ：最後の部屋へ直接行く？");
                                                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                                                    {
                                                        yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                                        yesno.ShowDialog();
                                                        if (yesno.DialogResult == DialogResult.Yes)
                                                        {
                                                            UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                                            UpdatePlayerLocationInfo(basePosX + moveLen * 3, basePosY + moveLen * 9);
                                                            SetupDungeonMapping(3, false);
                                                            UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                                        }
                                                        else
                                                        {
                                                            UpdateMainMessage("ラナ：最初ワープしていた地点へ入ってみる？");

                                                            yesno.ShowDialog();
                                                            if (yesno.DialogResult == DialogResult.Yes)
                                                            {
                                                                UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                                                UpdatePlayerLocationInfo(basePosX + moveLen * 14, basePosY + moveLen * 19);
                                                                SetupDungeonMapping(3, false);
                                                                UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                                            }
                                                            else
                                                            {
                                                                UpdateMainMessage("", true);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    return;

                                case 13:
                                    if (!we.InfoArea312E)
                                    {
                                        if (!we.InfoArea311E && !we.InfoArea313E)
                                        {
                                            UpdateMainMessage("ラナ：あった、ここにも鏡が設置されているわ。");

                                            UpdateMainMessage("アイン：しかし、見つけるのが早いなラナ。言われて見れば分かるが。");

                                            UpdateMainMessage("ヴェルゼ：容は違えど、ボク達の時も唯一女性であるファラが見つけるのは早かったです。");

                                            UpdateMainMessage("アイン：ファラ・・・ファラ王妃の事ですか？");

                                            UpdateMainMessage("ヴェルゼ：ええ、ファラも直感が利く人でした。");

                                            UpdateMainMessage("ヴェルゼ：皆が見落としそうなモノを通り過ぎず、気付き、拾い救うように、見つけてあげるんです。");

                                            UpdateMainMessage("ラナ：あ、そういえば、ヴェルゼさんとファラ様って同い年なんですか？");

                                            UpdateMainMessage("ヴェルゼ：そうですよ。FiveSeekerの５人は皆同い年です。");

                                            UpdateMainMessage("ラナ：え、えーそうなんだ！？ちょっとビックリ・・・");

                                            UpdateMainMessage("アイン：ランディのボケも同い年って事か？");

                                            UpdateMainMessage("ヴェルゼ：はい、残念ながらそういう事になります。");

                                            UpdateMainMessage("アイン：ッケ、アイツはガキッぽいクセに意外と年は食ってんだな。");

                                            UpdateMainMessage("ラナ：また、ファラ様とかのお話、宿屋で聞かせてくださいね♪");

                                            UpdateMainMessage("ヴェルゼ：・・・ラナさん、鏡をお願いします。");

                                            UpdateMainMessage("ラナ：え？え、ええ・・・じゃ、鏡を発動させるわね。");

                                            UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");

                                            UpdatePlayerLocationInfo(basePosX + moveLen * 24, basePosY + moveLen * 4);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("　　　『ッバシュ！！！』　　");

                                            UpdateMainMessage("アイン：お？まだ鏡は傍にあるぜ。失敗したんじゃねえのか？");

                                            UpdateMainMessage("ラナ：ここは・・・どうやら元の位置に戻ったみたいね。");

                                            UpdateMainMessage("ヴェルゼ：この調子で先へ進んでみましょう。");

                                            UpdateMainMessage("アイン：ああ、まだまだ鏡はあるかも知れねえが、とりあえずガンガン進むぜ！");
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ラナ：鏡、あったわよ。");

                                            UpdateMainMessage("アイン：ラナ、頼んだぜ。");

                                            UpdateMainMessage("ラナ：ええ、じゃ行くわよ。");

                                            UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");

                                            UpdatePlayerLocationInfo(basePosX + moveLen * 24, basePosY + moveLen * 4);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                        }
                                        we.InfoArea312E = true;
                                    }
                                    else
                                    {
                                        UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");

                                        UpdatePlayerLocationInfo(basePosX + moveLen * 24, basePosY + moveLen * 4);
                                        SetupDungeonMapping(3, false);
                                        UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    }
                                    return;

                                case 14:
                                    if (!we.InfoArea313S)
                                    {
                                        if (!we.InfoArea311S && !we.InfoArea312S)
                                        {
                                            UpdateMainMessage("ラナ：あっ、ちょっとアイン。");

                                            UpdateMainMessage("アイン：ん？何だ？");

                                            UpdateMainMessage("ラナ：あったわよ、ホラそこに鏡。");

                                            UpdateMainMessage("アイン：？・・・おお、ホントだ。よく見つけたなラナ。");

                                            UpdateMainMessage("ヴェルゼ：こ、っこれは！！");

                                            UpdateMainMessage("アイン：ん？ヴェルゼどうかしたのか？");

                                            UpdateMainMessage("ヴェルゼ：この形、ファージル宮殿内にある【秤の三面鏡】にそっくりです。");

                                            UpdateMainMessage("ラナ：秤の三面鏡って聞いたことがあるわ。本人の想いを計るとか。");

                                            UpdateMainMessage("ヴェルゼ：そのとおり。本人が迷い、思考・判断を整理したい時によく使われています。");

                                            UpdateMainMessage("アイン：よし、では早速・・・感じるままに覗いてみるか。");

                                            UpdateMainMessage("アイン：・・・何も起きないぞ。ただの鏡じゃねえか？");

                                            UpdateMainMessage("ラナ：迷ったり、悩んでる人じゃないと駄目なんじゃない？アインじゃ元々無理よ。");

                                            UpdateMainMessage("アイン：ラナ、俺だって時として悩んだりするんだ。");

                                            UpdateMainMessage("ラナ：そういう事言ってる間は、悩んでるうちに入らないわよ。");

                                            UpdateMainMessage("ヴェルゼ：・・・ボクでも駄目みたいですね。すみませんが何も起きません。");

                                            UpdateMainMessage("ラナ：ヴェルゼさんは洗練されてるんですよ。卓越してる人も駄目みたい。");

                                            UpdateMainMessage("アイン：どうしてそこで評価がまったくの真逆になってんだよ。");

                                            UpdateMainMessage("アイン：っと、じゃあ残るはラナ、おまえだぜ？");

                                            UpdateMainMessage("ラナ：・・・何がよ？");

                                            UpdateMainMessage("アイン：俺、ヴェルゼとくれば後はお前だけだろ。何が、「何がよ？」なんだよ。");

                                            UpdateMainMessage("ラナ：・・・え、ぇ、ええっとね。");

                                            UpdateMainMessage("アイン：何戸惑ってんだ。駄目元で良いからやってみてくれよ？");

                                            UpdateMainMessage("ヴェルゼ：アイン君、女の子にそういう言い方は失礼ですよ。");

                                            UpdateMainMessage("ラナ：あ、ヴェルゼさん、良いんです。じゃ、やってみるわよ。");

                                            UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");

                                            UpdateMainMessage("アイン：うわっ、ま、眩し！！");

                                            UpdatePlayerLocationInfo(basePosX + moveLen * 7, basePosY + moveLen * 3);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("　　　『ッバシュ！！！』　　");

                                            UpdateMainMessage("アイン：・・・ん？か、鏡が無くなってるぞ。");

                                            UpdateMainMessage("ヴェルゼ：いや、鏡が無くなったわけじゃなさそうです。");

                                            UpdateMainMessage("ラナ：ダンジョン壁の構成が違う・・・どこか違う場所に来たのね。");

                                            UpdateMainMessage("アイン：飛ばされたって事か！？");

                                            UpdateMainMessage("ヴェルゼ：どうやらそのようですね。一種のワープ装置みたいなものでしょうか。");

                                            UpdateMainMessage("アイン：ちくしょう、これじゃラナのダンジョンマップメモも役に立たないんじゃないか？");

                                            UpdateMainMessage("ラナ：同じ場所に飛ばされるわけじゃないから役に立たないワケじゃないけど。");

                                            UpdateMainMessage("ラナ：でも、道のりに進めて埋めていく方法が通用しないわね。");

                                            UpdateMainMessage("アイン：厄介だな。でも進むしか道はねえな。");

                                            UpdateMainMessage("ヴェルゼ：幸いここは一本道に見えます。行きましょう。");
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ラナ：あっ、ここに鏡が設置されてるわね。");

                                            UpdateMainMessage("ヴェルゼ：ラナさん、鏡をお願いします。");

                                            UpdateMainMessage("アイン：別の所だからな。一応警戒しながら進もうぜ。");

                                            UpdateMainMessage("ラナ：ええ、じゃ行くわよ。");

                                            UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");

                                            UpdatePlayerLocationInfo(basePosX + moveLen * 7, basePosY + moveLen * 3);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                        }
                                        we.InfoArea313S = true;
                                    }
                                    else
                                    {
                                        if (!we.CompleteArea32)
                                        {
                                            UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                            UpdatePlayerLocationInfo(basePosX + moveLen * 7, basePosY + moveLen * 3);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                        }
                                        else
                                        {
                                            if (!we.CompleteArea34)
                                            {
                                                UpdateMainMessage("ラナ：ここは一度突破しているわ。どうする、また入ってみる？");

                                                using (YesNoRequestMini yesno = new YesNoRequestMini())
                                                {
                                                    yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                                    yesno.ShowDialog();
                                                    if (yesno.DialogResult == DialogResult.Yes)
                                                    {
                                                        UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                                        UpdatePlayerLocationInfo(basePosX + moveLen * 7, basePosY + moveLen * 3);
                                                        SetupDungeonMapping(3, false);
                                                        UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                                    }
                                                    else
                                                    {
                                                        UpdateMainMessage("", true);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (!we.CompleteJumpArea34)
                                                {
                                                    we.CompleteJumpArea34 = true;
                                                    UpdateMainMessage("ラナ：そうそう、台座の所で教えてもらった行き先なんだけど。");

                                                    UpdateMainMessage("アイン：ん、なんだ？");

                                                    UpdateMainMessage("ラナ：この鏡からでも行けるようになったみたいね♪");

                                                    UpdateMainMessage("アイン：おお、やるじゃねえか！神様も捨てたもんじゃねえな！");

                                                    UpdateMainMessage("ラナ：どうするのよ。直接行く？");
                                                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                                                    {
                                                        yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                                        yesno.ShowDialog();
                                                        if (yesno.DialogResult == DialogResult.Yes)
                                                        {
                                                            UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                                            UpdatePlayerLocationInfo(basePosX + moveLen * 3, basePosY + moveLen * 9);
                                                            SetupDungeonMapping(3, false);
                                                            UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                                        }
                                                        else
                                                        {
                                                            UpdateMainMessage("", true);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    UpdateMainMessage("ラナ：最後の部屋へ直接行く？");
                                                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                                                    {
                                                        yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                                        yesno.ShowDialog();
                                                        if (yesno.DialogResult == DialogResult.Yes)
                                                        {
                                                            UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                                            UpdatePlayerLocationInfo(basePosX + moveLen * 3, basePosY + moveLen * 9);
                                                            SetupDungeonMapping(3, false);
                                                            UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                                        }
                                                        else
                                                        {
                                                            UpdateMainMessage("ラナ：最初ワープしていた地点へ入ってみる？");

                                                            yesno.ShowDialog();
                                                            if (yesno.DialogResult == DialogResult.Yes)
                                                            {
                                                                UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                                                UpdatePlayerLocationInfo(basePosX + moveLen * 7, basePosY + moveLen * 3);
                                                                SetupDungeonMapping(3, false);
                                                                UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                                            }
                                                            else
                                                            {
                                                                UpdateMainMessage("", true);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    return;

                                case 15:
                                    if (!we.InfoArea313E)
                                    {
                                        if (!we.InfoArea311E && !we.InfoArea312E)
                                        {
                                            UpdateMainMessage("ラナ：あった、ここにも鏡が設置されているわ。");

                                            UpdateMainMessage("アイン：しかし、見つけるのが早いなラナ。言われて見れば分かるが。");

                                            UpdateMainMessage("ヴェルゼ：容は違えど、ボク達の時も唯一女性であるファラが見つけるのは早かったです。");

                                            UpdateMainMessage("アイン：ファラ・・・ファラ王妃の事ですか？");

                                            UpdateMainMessage("ヴェルゼ：ええ、ファラも直感が利く人でした。");

                                            UpdateMainMessage("ヴェルゼ：皆が見落としそうなモノを通り過ぎず、気付き、拾い救うように、見つけてあげるんです。");

                                            UpdateMainMessage("ラナ：あ、そういえば、ヴェルゼさんとファラ様って同い年なんですか？");

                                            UpdateMainMessage("ヴェルゼ：そうですよ。FiveSeekerの５人は皆同い年です。");

                                            UpdateMainMessage("ラナ：え、えーそうなんだ！？ちょっとビックリ・・・");

                                            UpdateMainMessage("アイン：ランディのボケも同い年って事か？");

                                            UpdateMainMessage("ヴェルゼ：はい、残念ながらそういう事になります。");

                                            UpdateMainMessage("アイン：ッケ、アイツはガキッぽいクセに意外と年は食ってんだな。");

                                            UpdateMainMessage("ラナ：また、ファラ様とかのお話、宿屋で聞かせてくださいね♪");

                                            UpdateMainMessage("ヴェルゼ：・・・ラナさん、鏡をお願いします。");

                                            UpdateMainMessage("ラナ：え？え、ええ・・・じゃ、鏡を発動させるわね。");

                                            UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");

                                            UpdatePlayerLocationInfo(basePosX + moveLen * 21, basePosY + moveLen * 1);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("　　　『ッバシュ！！！』　　");

                                            UpdateMainMessage("アイン：お？まだ鏡は傍にあるぜ。失敗したんじゃねえのか？");

                                            UpdateMainMessage("ラナ：ここは・・・どうやら元の位置に戻ったみたいね。");

                                            UpdateMainMessage("ヴェルゼ：この調子で先へ進んでみましょう。");

                                            UpdateMainMessage("アイン：ああ、まだまだ鏡はあるかも知れねえが、とりあえずガンガン進むぜ！");

                                        }
                                        else
                                        {
                                            UpdateMainMessage("ラナ：鏡、あったわよ。");

                                            UpdateMainMessage("アイン：ラナ、頼んだぜ。");

                                            UpdateMainMessage("ラナ：ええ、じゃ行くわよ。");

                                            UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");

                                            UpdatePlayerLocationInfo(basePosX + moveLen * 21, basePosY + moveLen * 1);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                        }
                                        we.InfoArea313E = true;
                                    }
                                    else
                                    {
                                        UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");

                                        UpdatePlayerLocationInfo(basePosX + moveLen * 21, basePosY + moveLen * 1);
                                        SetupDungeonMapping(3, false);
                                        UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    }
                                    return;
                                    #endregion

                                case 16: // ワープ開始４
                                    #region "第二関門"
                                    if (!we.InfoArea324S)
                                    {
                                        UpdateMainMessage("ラナ：あ、あったあった。鏡発見っと♪");

                                        UpdateMainMessage("アイン：なあ、ヴェルゼ。ファージル宮殿にある・・・なんだっけ・・・");

                                        UpdateMainMessage("ヴェルゼ：秤の三面鏡のことですか？");

                                        UpdateMainMessage("アイン：それそれ。ファージル宮殿内にあるやつも、ワープ装置なのか？");

                                        UpdateMainMessage("ヴェルゼ：いえ、ワープ装置というわけではありませんよ。単なる鏡と言えば・・・");

                                        UpdateMainMessage("ラナ：単なる鏡ではないんですか？");

                                        UpdateMainMessage("ヴェルゼ：すいません、単なる鏡ではありません。");

                                        UpdateMainMessage("アイン：でも内容は教えられない・・・って所か？");

                                        UpdateMainMessage("ヴェルゼ：はい、すいません。");

                                        UpdateMainMessage("ラナ：まあまあ、今は明らかにワープ装置よね♪　じゃ早速使いましょ♪");

                                        UpdateMainMessage("アイン：おいラナ。何でお前そんな楽しそうなんだよ？");

                                        UpdateMainMessage("ラナ：ん？何でだろうね。何となくこれに触れるたびに、気分が洗われる感じなのよ。");

                                        UpdateMainMessage("ヴェルゼ：・・・さあ、ラナさんお願いします。");

                                        UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");

                                        UpdatePlayerLocationInfo(basePosX + moveLen * 15, basePosY + moveLen * 6);
                                        SetupDungeonMapping(3, false);
                                        UpdateMainMessage("　　　『ッバシュ！！！』　　");

                                        UpdateMainMessage("アイン：どうやら若干広い区画に出たようだな。");

                                        UpdateMainMessage("ラナ：わあ！見て見て！４隅にきれいに鏡が置いてあるわ。");

                                        UpdateMainMessage("アイン：っな、お前もうそこまで見えてるのかよ！？");

                                        UpdateMainMessage("ラナ：え？ええ、遠くからでも見えるわよ、よーく見ればね。");

                                        UpdateMainMessage("アイン：そうか？・・・　・・・　・・・　まあ、確かにそうかもな。");

                                        UpdateMainMessage("ラナ：アイン、どこから行く？");

                                        UpdateMainMessage("アイン：ラナの好きな所からで良いぞ。");

                                        UpdateMainMessage("ラナ：ふうん、意外と優しいのね。");

                                        UpdateMainMessage("アイン：何ワケわかんねえ事言ってるんだ。ほら、さっさと好きに選べ。");

                                        UpdateMainMessage("ラナ：途端にバカになった・・・良いわ、好きに選ぶから。");

                                        we.InfoArea324S = true;
                                    }
                                    else
                                    {
                                        if (!we.CompleteArea32)
                                        {
                                            UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                            UpdatePlayerLocationInfo(basePosX + moveLen * 15, basePosY + moveLen * 6);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ラナ：ここは一度突破しているわ。どうする、また入ってみる？");

                                            using (YesNoRequestMini yesno = new YesNoRequestMini())
                                            {
                                                yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                                yesno.ShowDialog();
                                                if (yesno.DialogResult == DialogResult.Yes)
                                                {
                                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                                    UpdatePlayerLocationInfo(basePosX + moveLen * 15, basePosY + moveLen * 6);
                                                    SetupDungeonMapping(3, false);
                                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                                }
                                                else
                                                {
                                                    UpdateMainMessage("", true);
                                                }
                                            }
                                        }
                                    }
                                    return;

                                case 17: // ワープ開始５
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 12, basePosY + moveLen * 2);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    return;

                                case 18: // ワープ開始６
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 26, basePosY + moveLen * 17);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    return;

                                case 19: // ワープ開始７
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 24, basePosY + moveLen * 9);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    return;

                                case 20: // ワープ開始８
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 8, basePosY + moveLen * 14);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    return;

                                case 21: // ワープ開始９
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 10, basePosY + moveLen * 18);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    return;

                                case 22: // ワープ開始１０
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 8, basePosY + moveLen * 8);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    return;

                                case 23: // ワープ開始１１
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 29, basePosY + moveLen * 15);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    return;

                                case 24: // ワープ開始１２
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 10, basePosY + moveLen * 2);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    return;

                                case 25: // ワープ開始１３
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 12, basePosY + moveLen * 4);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    return;

                                case 26: // ワープ開始１４
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 13, basePosY + moveLen * 14);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    return;

                                case 27: // 終了ワープ４
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 23, basePosY + moveLen * 14);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");

                                    if (!we.InfoArea324E)
                                    {

                                        UpdateMainMessage("ヴェルゼ：ここは・・・やりましたね。戻ってこれたようです。");

                                        UpdateMainMessage("ラナ：・・・ッハアアアァァ・・・疲れたわ。");

                                        UpdateMainMessage("アイン：何だよ、やけにしんどそうだな。");

                                        UpdateMainMessage("アイン：さっきから思ってたが、ラナ。何でお前そんなにリキ入ってんだよ？");

                                        UpdateMainMessage("ラナ：アンタに関係ないでしょ、ほっといてよ！");

                                        UpdateMainMessage("アイン：コワッ・・・何でキレてんだよ。意味わかんねえな。");

                                        UpdateMainMessage("ヴェルゼ：・・・秤の三面鏡のせいですね？");

                                        UpdateMainMessage("ラナ：違います。大丈夫ですから。");

                                        UpdateMainMessage("アイン：一体どういう事だよ？");

                                        UpdateMainMessage("ヴェルゼ：迷いや悩みを無くしていく・・・とても辛い事だと思いませんか？");

                                        UpdateMainMessage("アイン：悩んでる方がシンドイだろ、どう考えても。ヴェルゼもおかしなやつだな。");

                                        UpdateMainMessage("ラナ：ヴェルゼさん、そこのバカには一生理解出来ない内容ですから、放っといてあげてください。");

                                        UpdateMainMessage("アイン：ッチ・・・ええ、ええ、俺にはどうせ分かりませんよ。");

                                        UpdateMainMessage("ヴェルゼ：迷っている間は、選択肢は残されています。");

                                        UpdateMainMessage("ヴェルゼ：そして悩んでいる間は、意志決定を先送りできます。");

                                        UpdateMainMessage("アイン：そうか、その三面鏡はひょっとして。");

                                        UpdateMainMessage("ラナ：っさ♪　もうすっかり元気になったわ♪　まだまだ途中なんだからさっさと進むわよ♪");

                                        UpdateMainMessage("　　　『ッドバキイィィィ！！！』（ライトニングキックがアインに炸裂）　　");

                                        UpdateMainMessage("アイン：ッテエエェェェ！！！　お前、蹴るタイミングおかしいだろ！？");

                                        UpdateMainMessage("ラナ：良いじゃない、減るもんでもないし♪");

                                        UpdateMainMessage("アイン：いや、俺の体力が減る・・・理由なき暴力だ。ランディを思い出すぜ・・・いてて・・・");

                                        UpdateMainMessage("ラナ：ッフフ、何でもないって言ってるじゃない。っさ、行きましょ♪");
                                        we.InfoArea324E = true;
                                    }
                                    return;

                                case 28: // 失敗ワープ１
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 29, basePosY + moveLen * 1);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");

                                    if (!we.CompleteArea32)
                                    {
                                        if (!we.FailArea321)
                                        {
                                            if (!we.FailArea322 && !we.FailArea323)
                                            {
                                                UpdateMainMessage("ラナ：あ、あれ？");

                                                UpdateMainMessage("アイン：ん、どうしたんだ？");

                                                UpdateMainMessage("ヴェルゼ：どうやら、ここは最初の場所のようですね。");

                                                UpdateMainMessage("アイン：っち、そう簡単に進ませてはくれねえって事か。");

                                                UpdateMainMessage("ラナ：アイン、ゴメンね。次は上手く選んでみせるから。");

                                                UpdateMainMessage("アイン：何言ってる。特に死亡トラップに引っかかったワケでもねえ。気にせずもう１回だ。");

                                                UpdateMainMessage("ラナ：ええ、もう一回さっきの場所に行きましょう。");
                                            }
                                            else
                                            {
                                                UpdateMainMessage("ラナ：あ、あれ？");

                                                UpdateMainMessage("アイン：ん、どこにワープしたんだ？");

                                                UpdateMainMessage("ヴェルゼ：前回と少し位置は違ってますが、最初の場所のようですね。");

                                                UpdateMainMessage("ラナ：何度もゴメンね。次は上手く選んでみせるから。");

                                                UpdateMainMessage("アイン：何言ってる。前の所と違うポイントじゃねえか。しょうがねえだろ、気にせずもう１回だ。");

                                                UpdateMainMessage("ラナ：ええ、もう一回さっきの場所に行きましょう。");
                                            }
                                            we.FailArea321 = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ラナ：あ、あれ？");

                                            UpdateMainMessage("アイン：ココは・・・また最初の場所に戻されちまったな。");

                                            UpdateMainMessage("ラナ：同じ所なのに、何度もゴメンね。");

                                            UpdateMainMessage("アイン：気にするなって、大体おまえが謝ってる姿を見ると調子が狂うぜ。");

                                            UpdateMainMessage("ヴェルゼ：ラナさん、落ち着いて行きましょう。もう一度です。");

                                            UpdateMainMessage("ラナ：ええ、もう一回さっきの場所に行きましょう。");
                                        }
                                    }
                                    return;

                                case 29: // 失敗ワープ２
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 24, basePosY + moveLen * 4);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");

                                    if (!we.CompleteArea32)
                                    {
                                        if (!we.FailArea322)
                                        {
                                            if (!we.FailArea321 && !we.FailArea323)
                                            {
                                                UpdateMainMessage("ラナ：あ、あれ？");

                                                UpdateMainMessage("アイン：ん、どうしたんだ？");

                                                UpdateMainMessage("ヴェルゼ：どうやら、ここは最初の場所のようですね。");

                                                UpdateMainMessage("アイン：っち、そう簡単に進ませてはくれねえって事か。");

                                                UpdateMainMessage("ラナ：アイン、ゴメンね。次は上手く選んでみせるから。");

                                                UpdateMainMessage("アイン：何言ってる。特に死亡トラップに引っかかったワケでもねえ。気にせずもう１回だ。");

                                                UpdateMainMessage("ラナ：ええ、もう一回さっきの場所に行きましょう。");
                                            }
                                            else
                                            {
                                                UpdateMainMessage("ラナ：あ、あれ？");

                                                UpdateMainMessage("アイン：ん、どこにワープしたんだ？");

                                                UpdateMainMessage("ヴェルゼ：前回と少し位置は違ってますが、最初の場所のようですね。");

                                                UpdateMainMessage("ラナ：何度もゴメンね。次は上手く選んでみせるから。");

                                                UpdateMainMessage("アイン：何言ってる。前の所と違うポイントじゃねえか。しょうがねえだろ、気にせずもう１回だ。");

                                                UpdateMainMessage("ラナ：ええ、もう一回さっきの場所に行きましょう。");
                                            }
                                            we.FailArea322 = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ラナ：あ、あれ？");

                                            UpdateMainMessage("アイン：ココは・・・また最初の場所に戻されちまったな。");

                                            UpdateMainMessage("ラナ：同じ所なのに、何度もゴメンね。");

                                            UpdateMainMessage("アイン：気にするなって、大体おまえが謝ってる姿を見ると調子が狂うぜ。");

                                            UpdateMainMessage("ヴェルゼ：ラナさん、落ち着いて行きましょう。もう一度です。");

                                            UpdateMainMessage("ラナ：ええ、もう一回さっきの場所に行きましょう。");
                                        }
                                    }
                                    return;

                                case 30: // 失敗ワープ３
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 21, basePosY + moveLen * 1);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");

                                    if (!we.CompleteArea32)
                                    {
                                        if (!we.FailArea323)
                                        {
                                            if (!we.FailArea321 && !we.FailArea322)
                                            {
                                                UpdateMainMessage("ラナ：あ、あれ？");

                                                UpdateMainMessage("アイン：ん、どうしたんだ？");

                                                UpdateMainMessage("ヴェルゼ：どうやら、ここは最初の場所のようですね。");

                                                UpdateMainMessage("アイン：っち、そう簡単に進ませてはくれねえって事か。");

                                                UpdateMainMessage("ラナ：アイン、ゴメンね。次は上手く選んでみせるから。");

                                                UpdateMainMessage("アイン：何言ってる。特に死亡トラップに引っかかったワケでもねえ。気にせずもう１回だ。");

                                                UpdateMainMessage("ラナ：ええ、もう一回さっきの場所に行きましょう。");
                                            }
                                            else
                                            {
                                                UpdateMainMessage("ラナ：あ、あれ？");

                                                UpdateMainMessage("アイン：ん、どこにワープしたんだ？");

                                                UpdateMainMessage("ヴェルゼ：前回と少し位置は違ってますが、最初の場所のようですね。");

                                                UpdateMainMessage("ラナ：何度もゴメンね。次は上手く選んでみせるから。");

                                                UpdateMainMessage("アイン：何言ってる。前の所と違うポイントじゃねえか。しょうがねえだろ、気にせずもう１回だ。");

                                                UpdateMainMessage("ラナ：ええ、もう一回さっきの場所に行きましょう。");
                                            }
                                            we.FailArea323 = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ラナ：あ、あれ？");

                                            UpdateMainMessage("アイン：ココは・・・また最初の場所に戻されちまったな。");

                                            UpdateMainMessage("ラナ：同じ所なのに、何度もゴメンね。");

                                            UpdateMainMessage("アイン：気にするなって、大体おまえが謝ってる姿を見ると調子が狂うぜ。");

                                            UpdateMainMessage("ヴェルゼ：ラナさん、落ち着いて行きましょう。もう一度です。");

                                            UpdateMainMessage("ラナ：ええ、もう一回さっきの場所に行きましょう。");
                                        }
                                    }
                                    return;

                                case 31: // 失敗ワープ４１
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 15, basePosY + moveLen * 6);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");

                                    if (!we.CompleteArea32)
                                    {
                                        if (!we.FailArea3241)
                                        {
                                            UpdateMainMessage("ラナ：あれ、ここって例の４隅に鏡がある部屋？");

                                            UpdateMainMessage("アイン：ん？・・・おお、確かにそうだな。");

                                            UpdateMainMessage("ヴェルゼ：どうやら、振り出しのようですね。");

                                            UpdateMainMessage("アイン：でもまあ、アイテムがあったしな。悪い気はしないぜ。");

                                            UpdateMainMessage("ラナ：他の所へ行ってみることにするわね。");

                                            UpdateMainMessage("アイン：ああ、頼んだぜ！");
                                            we.FailArea3241 = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ラナ：あれ、ひょっとしてまた振り出し？");

                                            UpdateMainMessage("アイン：ラナ、ココは前に通ったルートだ。落ち着け。");

                                            UpdateMainMessage("ラナ：ゴメンね。次は同じ所を通らないようにするわ。");

                                            UpdateMainMessage("アイン：謝なくても良いぜ。ったく、何か調子狂うな。");
                                        }
                                    }
                                    return;

                                case 32: // 失敗ワープ４２
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 15, basePosY + moveLen * 6);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");

                                    if (!we.CompleteArea32)
                                    {
                                        if (!we.FailArea3242)
                                        {
                                            UpdateMainMessage("ラナ：あれ、ここって例の４隅に鏡がある部屋？");

                                            UpdateMainMessage("アイン：ん？・・・おお、確かにそうだな。");

                                            UpdateMainMessage("ヴェルゼ：どうやら、振り出しのようですね。");

                                            UpdateMainMessage("アイン：でもまあ、アイテムがあったしな。悪い気はしないぜ。");

                                            UpdateMainMessage("ラナ：他の所へ行ってみることにするわね。");

                                            UpdateMainMessage("アイン：ああ、頼んだぜ！");
                                            we.FailArea3242 = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ラナ：あれ、ひょっとしてまた振り出し？");

                                            UpdateMainMessage("アイン：ラナ、ココは前に通ったルートだ。落ち着け。");

                                            UpdateMainMessage("ラナ：ゴメンね。次は同じ所を通らないようにするわ。");

                                            UpdateMainMessage("アイン：謝なくても良いぜ。ったく、何か調子狂うな。");
                                        }
                                    }
                                    return;

                                case 33: // 開始ワープ８２
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 9, basePosY + moveLen * 14);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    return;

                                case 34: // 失敗ワープ４３
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 15, basePosY + moveLen * 6);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");

                                    if (!we.CompleteArea32)
                                    {
                                        if (!we.FailArea3243)
                                        {
                                            UpdateMainMessage("ラナ：あれ、ここって例の４隅に鏡がある部屋？");

                                            UpdateMainMessage("アイン：ん？・・・おお、確かにそうだな。");

                                            UpdateMainMessage("ヴェルゼ：どうやら、振り出しのようですね。");

                                            UpdateMainMessage("アイン：でもまあ、アイテムがあったしな。悪い気はしないぜ。");

                                            UpdateMainMessage("ラナ：他の所へ行ってみることにするわね。");

                                            UpdateMainMessage("アイン：ああ、頼んだぜ！");
                                            we.FailArea3243 = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ラナ：あれ、ひょっとしてまた振り出し？");

                                            UpdateMainMessage("アイン：ラナ、ココは前に通ったルートだ。落ち着け。");

                                            UpdateMainMessage("ラナ：ゴメンね。次は同じ所を通らないようにするわ。");

                                            UpdateMainMessage("アイン：謝なくても良いぜ。ったく、何か調子狂うな。");
                                        }
                                    }
                                    return;

                                case 35: // 第二関門クリア看板
                                    if (!we.CompleteArea32)
                                    {
                                        UpdateMainMessage("アイン：なあ、ちょっと一息つかないか？");

                                        UpdateMainMessage("ラナ：何言ってるのよ。全然終わってなさそうじゃない。まだまだこれからよ。");

                                        UpdateMainMessage("アイン：そうは言ってもな。このワープってのは調子狂うぜホント。");

                                        UpdateMainMessage("アイン：俺は一旦休むぜ・・・ココ、都合よく普通の椅子があるな。座らせてもらうぜ。");

                                        UpdateMainMessage("アイン：よいせっと。");

                                        UpdateMainMessage("　　　　『ッゴゴゴゴ・・・ドオオォォォン・・・』");

                                        we.CompleteArea32 = true;
                                        ConstructDungeonMap();
                                        SetupDungeonMapping(3);

                                        UpdateMainMessage("ラナ：・・・ちょっと・・・何か音がしたわよ。");

                                        UpdateMainMessage("ヴェルゼ：何か壁が動くような音でしたね。");

                                        UpdateMainMessage("アイン：ん？この椅子なんか書いてあるぞ。");

                                        UpdateMainMessage("　　　　『恐れる事なく鏡に手を伸ばせ。迷いなく鏡に触れるものに道が拓ける。』");

                                        UpdateMainMessage("ラナ：じゃあ、今の音ってどこか道が開通したと思えばいいのかしら？");

                                        UpdateMainMessage("ヴェルゼ：おそらく、そうでしょうね。");

                                        UpdateMainMessage("アイン：おっしゃ、運も実力のうちだぜ。ところでどの辺の道が開けたんだ？");

                                        UpdateMainMessage("ラナ：調べないと分からないけど、この連続ワープの中ではないわね。");

                                        UpdateMainMessage("アイン：何でそんな事が分かるんだ？お前昔からそうだが、何で知ってるかのような喋りなんだよ。");

                                        UpdateMainMessage("ラナ：何でって言われても困るわ。大体そんなもんでしょ？");

                                        UpdateMainMessage("ヴェルゼ：まあ、仕掛けは解除されたようですし、この先へ進みませんか？");

                                        UpdateMainMessage("アイン：そうだな、この道どうやら一本道のようだし、おそらく出口だろ。");

                                        UpdateMainMessage("ラナ：っささ、行きましょ♪");
                                    }
                                    else
                                    {
                                        UpdateMainMessage("　　　　『恐れる事なく鏡に手を伸ばせ。迷いなく鏡に触れるものに道が拓ける。』", true);
                                    }
                                    return;
#endregion

                                case 36: // 開始ワープ３１５
                                    #region "第三関門"
                                    if (!we.InfoArea3315S)
                                    {
                                        UpdateMainMessage("ラナ：見て、鏡があるわ。");

                                        UpdateMainMessage("ヴェルゼ：ラナさん、本当に体の方は大丈夫ですか？");

                                        UpdateMainMessage("ラナ：心配ないわ。っさ、行くわよ。");

                                        UpdateMainMessage("アイン：おい、ラナ。本当だろうな？");

                                        UpdateMainMessage("ラナ：何よ、アインまで真剣な顔しちゃって。大丈夫よ、少しずつ慣れて来てるから。");

                                        UpdateMainMessage("アイン：そうか。じゃ任せたぜ！！");

                                        UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                        UpdatePlayerLocationInfo(basePosX + moveLen * 2, basePosY + moveLen * 4);
                                        SetupDungeonMapping(3, false);
                                        UpdateMainMessage("　　　『ッバシュ！！！』　　");

                                        UpdateMainMessage("ラナ：鏡は合計４つ、正面に規則正しく並んでるわ。コレといったヒントもなさそうね。");

                                        UpdateMainMessage("アイン：そ、そうなのか？相変わらず早いな。");

                                        UpdateMainMessage("ヴェルゼ：ラナさん、少し鎮めた方が良いです。ボクから見ても早すぎな感じがします。");

                                        UpdateMainMessage("アイン：なあ、教えてくれよ。一体どうやって部屋を見てるんだ？今来た瞬間だぞ。");

                                        UpdateMainMessage("ラナ：見るという感覚とは少し違うわ。把握とは少し違うの。感覚的に雰囲気を掴むようにするのよ。");

                                        UpdateMainMessage("アイン：・・・よく分かんねえな、まあ俺はじっくり見ていくとするか。");

                                        UpdateMainMessage("ヴェルゼ：・・・どうです。どこから行くべきでしょうか？");

                                        UpdateMainMessage("ラナ：ここ、枝分かれしているくせに、一本道なのは変わんないのね。奇妙な作りよ。");

                                        UpdateMainMessage("ヴェルゼ：そうなんですか？私には正直どうすれば良いか分かりません。ラナさんが決めてください。");

                                        UpdateMainMessage("ラナ：もちろんよ、アイン、ヴェルゼさん、ついて来て。");

                                        UpdateMainMessage("アイン：ああ、任せたぜ、ラナ。");

                                        we.InfoArea3315S = true;
                                    }
                                    else
                                    {
                                        if (!we.CompleteArea33)
                                        {
                                            if (we.FailArea331 && !we.FailArea332) // ちょっとヘタクソなフラグ構築ですね。
                                            {
                                                UpdateMainMessage("ラナ：ゴメン、もう一回やらせて。次は当ててみせるから。");

                                                UpdateMainMessage("アイン：ああ、特に手痛いしっぺ返しや二度と戻らないトラップじゃねえ。安心してやっていいぞ。");

                                                UpdateMainMessage("ヴェルゼ：・・・では、ラナさんお願いします。");

                                                UpdateMainMessage("ラナ：っさ、じゃあ行くわよ。");
                                            }
                                            else
                                            {
                                                if (!we.FailArea332)
                                                {
                                                    UpdateMainMessage("ラナ：駄目ね、最後は必ず同じ部屋に来て、同じ鏡じゃない？一体どういう事なのかしら。");

                                                    UpdateMainMessage("ヴェルゼ：ひょっとしたらですが、途中経過も全て含まれているのではないでしょうか？");

                                                    UpdateMainMessage("ラナ：最後の例の部屋に来た瞬間に実は分かってるのよね。今回も駄目な感じがするって・・・");

                                                    UpdateMainMessage("アイン：十分時間はあるんだ。じっくりアテを探すようにやろうぜ。っな！");

                                                    UpdateMainMessage("ラナ：ええ、何だか逆に気を使わせちゃってるみたい。アリガト。");

                                                    UpdateMainMessage("ラナ：っさ、じゃあ今度こそ行くわよ。");
                                                }
                                                else
                                                {
                                                    if (!we.FailArea333)
                                                    {
                                                        UpdateMainMessage("ラナ：また駄目ね。ゴメンね、何度も。");

                                                        UpdateMainMessage("アイン：気にするな。正直これは全然わかんねえ。");

                                                        UpdateMainMessage("ヴェルゼ：最初は４つ、次は２、その次は２、そしてまた２、最後は１．");

                                                        UpdateMainMessage("ヴェルゼ：これだと単純計算なら３２通りです。３２通りやるつもりで気楽に探しましょう。");

                                                        UpdateMainMessage("ラナ：そんなんじゃ駄目よ。");

                                                        UpdateMainMessage("アイン：何そんな突っかかってんだ、ラナ。お前らしくねえぞ。");

                                                        UpdateMainMessage("ラナ：駄目なの。こんな風になってちゃ駄目なのよ。");

                                                        UpdateMainMessage("ヴェルゼ：・・・ラナさん、それではお願いします。");

                                                        UpdateMainMessage("アイン：ヴェルゼまで何でそんな圧迫するんだ。良いじゃねえか３２回やれば。");

                                                        UpdateMainMessage("ヴェルゼ：さて、ラナさんは真剣みたいです。邪魔しないでおきましょう。");

                                                        UpdateMainMessage("ラナ：っさ、行くわよ。当てて見せるわ。");
                                                    }
                                                    else
                                                    {
                                                        if (!we.FailArea334)
                                                        {
                                                            UpdateMainMessage("ラナ：おかしいわね・・・何で何回やっても駄目なのよ。");

                                                            UpdateMainMessage("アイン：・・・わかんねえ、しらみ潰しだけが頼りってのはシンドイもんだな。");

                                                            UpdateMainMessage("ラナ：ねえ、途中で何か居なかった？変な光みたいなの。");

                                                            UpdateMainMessage("アイン：いいや？何の変哲もねえ部屋ばかりだぞ。");

                                                            UpdateMainMessage("ラナ：最初の４つ鏡の部屋だけはうっすらと見えたの。隅の方に。");

                                                            UpdateMainMessage("アイン：何か光る虫でも居るんだろ。");

                                                            UpdateMainMessage("ヴェルゼ：光る虫ですか？でもボクが見る限り虫は居ませんでしたよ。");

                                                            UpdateMainMessage("ラナ：見えたの。今度も確かめてみるわ。っさ、行きましょう。");

                                                            UpdateMainMessage("アイン：・・・ああ。");
                                                        }
                                                        else
                                                        {
                                                            if (!we.FailArea335)
                                                            {
                                                                UpdateMainMessage("ラナ：変な光、また見えてたわ。間違いなく、最初は正面上隅よ。");

                                                                UpdateMainMessage("アイン：・・・ラナ、気持ちは分かるが、そんなもんはいねえ。");

                                                                UpdateMainMessage("ラナ：何よ、アインのバカには見えないだけでしょ。");

                                                                UpdateMainMessage("ヴェルゼ：ボクも見かけられませんでした。ラナさん本当に見かけたんですか？");

                                                                UpdateMainMessage("ラナ：・・・");

                                                                UpdateMainMessage("アイン：ラナ、いいか落ち着け。そんな迷い事は良いから");

                                                                UpdateMainMessage("ラナ：いいの、見えてるから。２つ目の部屋も正面の上隅がうっすら光ってたわ。");

                                                                UpdateMainMessage("アイン：・・・お前、大丈夫なんだよな？心配だから言ってるんだぞ。");

                                                                UpdateMainMessage("ヴェルゼ：・・・では、ラナさんお願いします。");

                                                                UpdateMainMessage("ラナ：今、随分気分が良いの。っさ、行くわよ♪");
                                                            }
                                                            else
                                                            {
                                                                if (!we.FailArea336)
                                                                {
                                                                    UpdateMainMessage("アイン：ラナ、お前顔色が凄く良いな。");

                                                                    UpdateMainMessage("ラナ：そう？まあこれだけ失敗しちゃったけど、気持ちは良いわよ♪");

                                                                    UpdateMainMessage("ヴェルゼ：・・・ラナさん、期待してますよ。頑張ってください。");

                                                                    UpdateMainMessage("ラナ：ええ！任せてちょうだいよ♪　３つ目の部屋は中央右よりが光ってたわ♪");

                                                                    UpdateMainMessage("アイン：おい、ヴェルゼ。ちょっといいか。何でそんなけしかけるんだ？");

                                                                    UpdateMainMessage("ヴェルゼ：アイン君、貴方は優しすぎるんです。");

                                                                    UpdateMainMessage("アイン：どういう意味だ。ラナは確かに顔色が良い、テンションも良さそうだ。");

                                                                    UpdateMainMessage("アイン：だがな、あんなラナは見たことがねえ。俺は心配なんだよ。");

                                                                    UpdateMainMessage("ヴェルゼ：それはアイン君がラナさんの事を今までよく見ていないだけです。");

                                                                    UpdateMainMessage("アイン：何？どういうことだ？");

                                                                    UpdateMainMessage("ラナ：っさ、アイン行くわよ。今度こそ当ててあげるわ。任せなさい♪");

                                                                    UpdateMainMessage("アイン：あ？あ、あぁ・・・っしゃ行くか。");
                                                                }
                                                                else
                                                                {
                                                                    if (!we.FailArea337)
                                                                    {
                                                                        UpdateMainMessage("ラナ：アイン、見えるの。もうハッキリと。");

                                                                        UpdateMainMessage("ラナ：１つ目の部屋、右上隅。");

                                                                        UpdateMainMessage("ラナ：２つ目の部屋、右上隅。");

                                                                        UpdateMainMessage("ラナ：３つ目の部屋、中央右、そして。");

                                                                        UpdateMainMessage("ラナ：４つ目の部屋、右上隅。");

                                                                        UpdateMainMessage("アイン：お前・・・顔色が明るすぎるぞ。本当にラナか？");

                                                                        UpdateMainMessage("ヴェルゼ：右上隅、右上隅、中央右、右上隅。行きましょう。");

                                                                        UpdateMainMessage("ラナ：アイン、何そんな不安そうな顔してんのよ。ホラホラ、さっさと行くわよ♪♪");

                                                                        UpdateMainMessage("アイン：お、おぉ分かったってそんな腕引っ張るなよ、分かった、分かったからさ。");
                                                                    }
                                                                    else
                                                                    {
                                                                        UpdateMainMessage("ラナ：右上隅、右上隅、中央右、右上隅。気持ち良い光♪");

                                                                        UpdateMainMessage("アイン：・・・ああ、そうだな。");

                                                                        UpdateMainMessage("ラナ：今度こそ間違えないように、っさ、行きましょう♪");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                            UpdatePlayerLocationInfo(basePosX + moveLen * 2, basePosY + moveLen * 4);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ラナ：ここは一度突破しているわ。どうする、また入ってみる？");

                                            using (YesNoRequestMini yesno = new YesNoRequestMini())
                                            {
                                                yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                                yesno.ShowDialog();
                                                if (yesno.DialogResult == DialogResult.Yes)
                                                {
                                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                                    UpdatePlayerLocationInfo(basePosX + moveLen * 2, basePosY + moveLen * 4);
                                                    SetupDungeonMapping(3, false);
                                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                                }
                                                else
                                                {
                                                    UpdateMainMessage("", true);
                                                }
                                            }
                                        }
                                    }
                                    we.ProgressArea3316 = false;
                                    we.ProgressArea3317 = false;
                                    we.ProgressArea3318 = false;
                                    we.ProgressArea3319 = false;
                                    we.ProgressArea3320 = false;
                                    we.ProgressArea3321 = false;
                                    we.ProgressArea3322 = false;
                                    we.ProgressArea3323 = false;
                                    we.ProgressArea3324 = false;
                                    we.ProgressArea3325 = false;
                                    we.ProgressArea3326 = false;
                                    we.ProgressArea3327 = false;
                                    return;

                                case 37: // 開始ワープ３１６
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 19, basePosY + moveLen * 8);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    we.ProgressArea3316 = true;
                                    return;

                                case 38: // 開始ワープ３１７
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 20, basePosY + moveLen * 8);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    we.ProgressArea3317 = true;
                                    return;

                                case 39: // 開始ワープ３１８
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 24, basePosY + moveLen * 12);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    we.ProgressArea3318 = true;
                                    return;

                                case 40: // 開始ワープ３１９
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 24, basePosY + moveLen * 13);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    we.ProgressArea3319 = true;
                                    return;

                                case 41: // 開始ワープ３２０
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 28, basePosY + moveLen * 2);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    we.ProgressArea3320 = true;
                                    return;

                                case 42: // 開始ワープ３２１
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 25, basePosY + moveLen * 6);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    we.ProgressArea3321 = true;
                                    return;

                                case 43: // 開始ワープ３２２
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 25, basePosY + moveLen * 2);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    we.ProgressArea3322 = true;
                                    return;

                                case 44: // 開始ワープ３２３
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 28, basePosY + moveLen * 6);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    we.ProgressArea3323 = true;
                                    return;

                                case 45: // 開始ワープ３２４
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 22, basePosY + moveLen * 2);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    we.ProgressArea3324 = true;
                                    return;

                                case 46: // 開始ワープ３２５
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 23, basePosY + moveLen * 6);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    we.ProgressArea3325 = true;
                                    return;

                                case 47: // 開始ワープ３２６
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 0, basePosY + moveLen * 13);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    we.ProgressArea3326 = true;
                                    return;

                                case 48: // 開始ワープ３２７
                                    UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 2, basePosY + moveLen * 13);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    we.ProgressArea3327 = true;
                                    return;

                                case 49: // 開始ワープ３２８
                                    if (we.ProgressArea3319 && we.ProgressArea3322 && we.ProgressArea3325 && we.ProgressArea3326)
                                    {
                                        UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                        UpdatePlayerLocationInfo(basePosX + moveLen * 6, basePosY + moveLen * 6);
                                        SetupDungeonMapping(3, false);
                                        UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                        if (!we.FailArea331)
                                        {
                                            UpdateMainMessage("ラナ：やったわ！！一発クリアよ♪♪");

                                            // [警告]：特別フラグがあると物語の面白さが膨らみます。

                                            UpdateMainMessage("アイン：す・・・すげえ、何だこれ・・・すげえじゃねえか！ラナ！！");
                                        }
                                        else
                                        {
                                            UpdateMainMessage("ヴェルゼ：やりましたね。ココは元の場所ですよ。戻ってこれたんですね。");

                                            UpdateMainMessage("アイン：おっしゃ！何度かミスったもののやったじゃねえか！ラナ！！");
                                        }

                                        UpdateMainMessage("アイン：！　ラナ！？");

                                        UpdateMainMessage("        『ラナの眼は普段のトパーズ色から薄白色になっていた。』");

                                        UpdateMainMessage("ラナ：アイン、とても気分が良いわ。行きましょう、最後の関門へ。");

                                        UpdateMainMessage("ラナ：その前に、ヒントを示す看板があるからそこをまず見ましょう。");

                                        UpdateMainMessage("アイン：ラナお前・・・いや、分かった。まずヒント看板に向かうとするか。");

                                        we.CompleteArea33 = true;
                                    }
                                    else
                                    {
                                        UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                        UpdatePlayerLocationInfo(basePosX + moveLen * 4, basePosY + moveLen * 7);
                                        SetupDungeonMapping(3, false);
                                        UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                        if (!we.FailArea331)
                                        {
                                            we.FailArea331 = true;
                                        }
                                        else if (!we.FailArea332)
                                        {
                                            we.FailArea332 = true;
                                        }
                                        else if (!we.FailArea333)
                                        {
                                            we.FailArea333 = true;
                                        }
                                        else if (!we.FailArea334)
                                        {
                                            we.FailArea334 = true;
                                        }
                                        else if (!we.FailArea335)
                                        {
                                            we.FailArea335 = true;
                                        }
                                        else if (!we.FailArea336)
                                        {
                                            we.FailArea336 = true;
                                        }
                                        else if (!we.FailArea337)
                                        {
                                            we.FailArea337 = true;
                                        }
                                    }
                                    return;
                                #endregion

                                case 50:
                                    #region "第四関門"
                                    if (!we.InfoArea34)
                                    {
                                        UpdateMainMessage("アイン：っと、本当に看板があるぜ、なになに？");

                                        UpdateMainMessage("        『思うがままにイメージされた光を見つけよ。』");

                                        UpdateMainMessage("アイン：光・・・おい、ラナが見えてる変な光の事じゃねえだろうな？");

                                        UpdateMainMessage("ラナ：ついてきて、コッチよ♪");

                                        UpdateMainMessage("アイン：もう行くのかよ。お前ちゃんと読んだのかよ？いつものメモはどうしたんだ。");

                                        UpdateMainMessage("ラナ：いいから良いから、ッネ♪");

                                        UpdateMainMessage("アイン：『ッネ♪』ってどんな台詞だよ、ありえねえ語尾になってるぞ・・・");
                                        we.InfoArea34 = true;
                                    }
                                    else
                                    {
                                        if (!we.CompleteArea34)
                                        {
                                            UpdateMainMessage("        『思うがままにイメージされた光を見つけよ。』");
                                        }
                                        else
                                        {
                                            UpdateMainMessage("看板はもう無くなっている。", true);
                                        }
                                    }
                                    return;

                                case 51:
                                    if (!we.SolveArea34)
                                    {
                                        UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                        UpdatePlayerLocationInfo(basePosX + moveLen * 19, basePosY + moveLen * 15);
                                        SetupDungeonMapping(3, false);
                                        UpdateMainMessage("　　　『ッバシュ！！！』　　");

                                        GroundOne.StopDungeonMusic();

                                        UpdateMainMessage("アイン：っな・・・何だここはああぁぁぁ！！！！！！？");

                                        UpdateMainMessage("        『部屋一面にギッシリと鏡が並んでいる。』");

                                        UpdateMainMessage("ヴェルゼ：部屋一面・・・鏡が・・・こ、これは・・・10000箇所はくだらない。");

                                        UpdateMainMessage("アイン：おい、しかも看板があるぞ・・・なになに？");

                                        UpdateMainMessage("        『絶対試練：汝、答えを示せ。無限変化。』");

                                        UpdateMainMessage("アイン：何だこれ、どういう事だよ？");

                                        UpdateMainMessage("ヴェルゼ：無限変化・・・つまり、一度ダンジョンを出たら答えが変わるという事でしょう。");

                                        UpdateMainMessage("アイン：バカな！？失敗したら答えが変わるってのかよ！？");

                                        UpdateMainMessage("ヴェルゼ：そうですね。これでは10000回やったとしても意味がありません。");

                                        UpdateMainMessage("アイン：ラナ、どうする・・・・・・ラナ、おいラナ？");

                                        UpdateMainMessage("ラナ：答え、示してあげるわ。どいて。");

                                        UpdateMainMessage("アイン：ラナ・・・・・・");

                                        UpdateMainMessage("　　　　『突如台座がラナの前に浮き上がった。直後、真紅の空間にラナが包まれた。』");

                                        GroundOne.PlayDungeonMusic(Database.BGM09, Database.BGM09LoopBegin);

                                        UpdateMainMessage("ラナ：【序説Ａ　私が迷ったわけじゃない。私が迷ってるわけがない。】");

                                        UpdateMainMessage("ラナ：【序説Ｂ　あなたが迷ったのでしょう？あなたが迷い続けるからでしょう？】");

                                        UpdateMainMessage("ラナ：【序説Ｃ　決められたくない。決めたくない。決めてしまいたくない。決められない。】");

                                        UpdateMainMessage("ラナ：【序説Ｄ　決めたの。もう決めたのよ。決めた事は決定された事実になるの。】");

                                        UpdateMainMessage("　　　　『ラナを包んでいる空間が緑黄色へと激しく変わった！！』");

                                        UpdateMainMessage("アイン：おい、何だこれ。ふざけんじゃねえ、こんなの止めろ！");

                                        UpdateMainMessage("ヴェルゼ：アイン君、離れて。今のラナさんに触れては駄目です。");

                                        UpdateMainMessage("ラナ：【続説Ａ　決めたんでしょう？あなた自身が決めたんでしょう？あなた自身が決めてしまったんでしょう？】");

                                        UpdateMainMessage("ラナ：【続説Ｂ　私が決めたの。自分で決めたの。自分の判断で決めたの。私の意志で決めたの。】");

                                        UpdateMainMessage("ラナ：【続説Ｃ　楽になれるはず。不安にならないはず。安心できるはず。迷わなくなるはず。】");

                                        UpdateMainMessage("ラナ：【続説Ｄ　楽になったわ。決めてよかった。払拭できてよかった。もう迷わないの。】");

                                        UpdateMainMessage("ラナ：【続説Ｅ　よかったのよ。楽になれて、決められて、払拭できて、迷いが無くなって。】");

                                        UpdateMainMessage("　　　　『ラナを包んでいる空間が翡翠色へと激しく変わった！！』");

                                        UpdateMainMessage("アイン：止めろ・・・良いからラナ！止めろ！何言ってんだお前止めろって！！！");

                                        UpdateMainMessage("ヴェルゼ：アイン君、大丈夫です・・・大丈夫ですから。離れてください！");

                                        UpdateMainMessage("ラナ：【転説Ａ　だから失敗するの。だからミスするの。だから駄目なの。だから暗闇なの。】");

                                        UpdateMainMessage("ラナ：【転説Ｂ　誰のせい？あなたでしょう？あなたが決定するからでしょう？あなたが悪いのよ？】");

                                        UpdateMainMessage("ラナ：【転説Ｃ　そう、私のせいよ、私の決定のせいよ。私が悪いの。誰も悪くないの。】");

                                        UpdateMainMessage("ラナ：【転説Ｄ　分かったの。これで良いの。誰も傷つかないの。誰も嫌な思いもしないの。】");

                                        UpdateMainMessage("ラナ：【転説Ｅ　不幸と幸福は等しきもの。失敗も成功も等しきもの。良いも悪いも等しきもの。】");

                                        UpdateMainMessage("　　　　『ラナを包んでいる空間が内部が見えない透明色へと激しく変わった！！』");

                                        UpdateMainMessage("アイン：止めてくれ・・・止めてくれよ・・・！　こんなのラナじゃねえ・・・！！");

                                        UpdateMainMessage("ヴェルゼ：アイン君・・・もうすぐです。辛抱してください。");

                                        UpdateMainMessage("ラナ：【終説Ａ　光が見え、同時に闇を抱えるの。何も無い所に全てが詰まっているの。】");

                                        UpdateMainMessage("ラナ：【終説Ｂ　終わらせたくなかったの。だから私が全部抱えてあげるの。そうすれば終わらない。】");

                                        UpdateMainMessage("ラナ：【終説Ｃ　あなたの居ない場所、あなたが居た場所、あなたが来る場所、あなたが来た場所へ。】");

                                        GroundOne.StopDungeonMusic();

                                        UpdateMainMessage("　　　　『空間が激しくフラッシュし、凝縮された空間へと連続的に小さくなる！！！』");

                                        UpdateMainMessage("　　　　『パパパパパパパ！！！！ッバシュウウウゥゥゥン！！！！！！！』");

                                        UpdateMainMessage("アイン：うおぉ！ウアアアアァァァァ！！！！");

                                        UpdateMainMessage("ヴェルゼ：こ、これは！！！アアアァァァァ！！！");

                                        UpdateMainMessage("　　　　『空間は弾け飛んだ後、台座の前にラナの倒れた姿があった』");

                                        UpdateMainMessage("アイン：ラナ！！！");

                                        UpdateMainMessage("ラナ：・・・・っちょ・・・動けないわ・・・何？");

                                        UpdateMainMessage("アイン：おい、っおい！ラナしっかりしろ！ラナ！！");

                                        UpdateMainMessage("ラナ：わ、わわ！何よっちょっと、近いっつうの！どきなさいよ！この！！");

                                        UpdateMainMessage("アイン：お前がそんな・・・そんな悩むなよ！そんな苦しむなよ！何でそんな考えてんだよ！！");

                                        UpdateMainMessage("アイン：俺がバカやってんだろうが！笑えよ！リラックスしろよ！！そんな苦しい顔すんなよ！！！");

                                        UpdateMainMessage("ラナ：わ、何言ってんのコイツ。ちょ何でライトニングキック効かないのよ。倒れなさいよ、っもう・・・");

                                        using (MessageDisplay md = new MessageDisplay())
                                        {
                                            md.StartPosition = FormStartPosition.CenterParent;
                                            md.Message = "アイン、ラナ、ヴェルゼはしばらくその場で休息を取った。";
                                            md.ShowDialog();
                                        }
                                        we.SolveArea34 = true;

                                        UpdateMainMessage("アイン：なあ、ラナ。お前もう大丈夫なのか？");

                                        UpdateMainMessage("ラナ：んん～・・・ちょっと・・・疲れたかもね。");

                                        UpdateMainMessage("アイン：俺さ、お前がそんな悩んでるとこ、見たくなかっただけなのかもしれねえ。");

                                        UpdateMainMessage("ラナ：っえ？私が悩んでる所なんて見せたっけ？");

                                        UpdateMainMessage("アイン：何言ってる。さっきの台座に居たお前、すげえ自問自答ラッシュだったぞ。");

                                        UpdateMainMessage("ラナ：っあ、アレの事？あれはね。");

                                        UpdateMainMessage("ヴェルゼ：秤の三面鏡ですね。");

                                        UpdateMainMessage("ラナ：そう、あの鏡に何度も触れているウチに聞こえてきてたの。");

                                        UpdateMainMessage("ラナ：誰かと誰かが喋っているのよ。私はそれを横から見てるの。");

                                        UpdateMainMessage("アイン：誰かと誰かって誰だよ？");

                                        UpdateMainMessage("ラナ：そんなの分かるわけないじゃない。何かモヤモヤっとしたイメージだけが残ってるの。");

                                        UpdateMainMessage("ラナ：あの台座の前で急にモヤモヤっとしたイメージがクリーンになって");

                                        UpdateMainMessage("ヴェルゼ：それで、今まで鏡で聞いて来た内容がそのまま出たのですね？");

                                        UpdateMainMessage("ラナ：うーん、でも何かよく思い出せないのよね。あ、忘れてたわ。そういえば。");

                                        UpdateMainMessage("ラナ：この部屋の答え、知ってるわよ。ついてきて♪");

                                        UpdateMainMessage("アイン：ラナ、お前・・・もう眼の方、大丈夫だよな？");

                                        UpdateMainMessage("        『ラナの眼は普段のトパーズ色に戻っていた。』");

                                        UpdateMainMessage("ラナ：何よ？ちょ見ないでよ。　アインの方が変よ？アンタバカに磨きかかったんじゃない？");

                                        UpdateMainMessage("アイン：・・・ッフ・・・ッハッハッハッハ！！ああ、バカさ！！！");

                                        UpdateMainMessage("ラナ：うわ・・・自分で認めてるし。大丈夫かしらコイツ。　っささ、こっちよ。");

                                        UpdateMainMessage("ヴェルゼ：ラナさん、こんな沢山の鏡があるのに、本当に分かるのですか？");

                                        UpdateMainMessage("ラナ：最後に台座が消える時にね、教えてもらったの、綺麗な声だったわ。大丈夫よ♪");

                                        UpdateMainMessage("アイン：おっしゃ、じゃあ行ってみようぜ！");

                                        GroundOne.PlayDungeonMusic(Database.BGM02, Database.BGM02LoopBegin);
                                    }
                                    else
                                    {
                                        UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                        UpdatePlayerLocationInfo(basePosX + moveLen * 19, basePosY + moveLen * 15);
                                        SetupDungeonMapping(3, false);
                                        UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    }
                                    return;

                                case 52:
                                    if (!we.CompleteArea34)
                                    {
                                        UpdateMainMessage("ラナ：変な光・・・もう見えなくなっちゃった。残念だな♪");

                                        UpdateMainMessage("アイン：ん？何か言ったか？");

                                        UpdateMainMessage("ラナ：ううん、何でも無いわ。っさ、行くわよ♪");

                                        UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                        UpdatePlayerLocationInfo(basePosX + moveLen * 3, basePosY + moveLen * 9);
                                        SetupDungeonMapping(3, false);
                                        UpdateMainMessage("　　　『ッバシュ！！！』　　");

                                        UpdateMainMessage("アイン：おお、別の地点に出たな。やったなラナ！お前ホント出来るやつだよな！ッハッハッハ！");

                                        GroundOne.StopDungeonMusic();

                                        UpdateMainMessage("ラナ：・・・・・・　（パタッ）");

                                        UpdateMainMessage("        『ラナは何も言わないまま、その場で静かに倒れ込んでしまった。』");

                                        UpdateMainMessage("アイン：ラナ！！！！！！！！");

                                        UpdateMainMessage("ヴェルゼ：アイン君！！【遠見の青水晶】を早く！！");

                                        UpdateMainMessage("アイン：あ、そうだな！早く連れて戻るぞ！じゃすぐ使うぜ！");

                                        CallHomeTown(true);
                                    }
                                    else
                                    {
                                        UpdateMainMessage("　　　『ラナが秤の三面鏡を覗き込み、手を伸ばした瞬間、鏡が白く輝き始めた！』　　");
                                        UpdatePlayerLocationInfo(basePosX + moveLen * 3, basePosY + moveLen * 9);
                                        SetupDungeonMapping(3, false);
                                        UpdateMainMessage("　　　『ッバシュ！！！』　　");
                                    }
                                    return;

                                case 53:
                                    we.Treasure121 = GetTreasure("プレート・アーマー");
                                    return;

                                case 54:
                                    we.Treasure122 = GetTreasure("ラメラ・アーマー");
                                    return;

                                case 55:
                                    we.Treasure123 = GetTreasure("シャムシール");
                                    return;
                                #endregion

                                case 56:
                                    if (!we.InfoArea35)
                                    {
                                        UpdateMainMessage("アイン：おお！？なんだココは！？");

                                        UpdateMainMessage("ヴェルゼ：これは・・・どうやら隠し通路のようですね。");

                                        UpdateMainMessage("ラナ：よくこんな所見つけたわね。");

                                        UpdateMainMessage("アイン：ふと横の壁が気になってな。試しに押してみたら開けたってわけさ。");

                                        UpdateMainMessage("ラナ：またテキトーな所触って変なトラップ発動させないでよね？");

                                        UpdateMainMessage("アイン：まあ、良いじゃねえか。たまにはこういうのもアリだろ。");

                                        UpdateMainMessage("ヴェルゼ：隠し通路系統は、貴重なアイテム、知恵、情報などが隠されている場合があります。");

                                        UpdateMainMessage("アイン：っしゃ！行ってみようぜ！");

                                        we.InfoArea35 = true;
                                    }
                                    return;

                                case 57:
                                    if (!we.SpecialInfo3)
                                    {
                                        UpdateMainMessage("アイン：ん？何か書いてあるな・・・");

                                        GroundOne.StopDungeonMusic();
                                        this.BackColor = Color.Black;
                                        UpdateMainMessage("　　　【その瞬間、アインの脳裏に激しい激痛が襲った！周囲の感覚が麻痺する！！】");

                                        UpdateMainMessage("アイン：ッグ・・・なんだ・・・これ・・・グアアア！");

                                        UpdateMainMessage("　　＜＜＜　全てが裏返しだとしたら？ ＞＞＞");

                                        UpdateMainMessage("アイン：ッテ・・・イテテテ・・・誰だ！ちくしょう！");

                                        UpdateMainMessage("　　＜＜＜　この光景が全て幻想だとしたら？　＞＞＞");

                                        UpdateMainMessage("アイン：幻想・・・だと！？・・・イッツツツ・・・何言って・・・");

                                        UpdateMainMessage("　　＜＜＜　初めから全てが間違っているのだとしたら？　＞＞＞");

                                        UpdateMainMessage("アイン：間違ってる・・・・んな・・・わけが・・・グアア！");

                                        UpdateMainMessage("　　＜＜＜　終わりへと足を運ぶな。　始まりへと足を進めろ。　＞＞＞");

                                        UpdateMainMessage("アイン：何・・・言って・・・やが・・・");

                                        UpdateMainMessage("　　　【アインに対する激しい激痛は少しずつ引いていった。】");

                                        this.BackColor = Color.RoyalBlue;

                                        UpdateMainMessage("ラナ：アイン？ちょっとアイン！？");

                                        UpdateMainMessage("アイン：・・・っつう・・・ん？");

                                        UpdateMainMessage("ラナ：っあ、ようやく起きたわね。大丈夫？");

                                        UpdateMainMessage("アイン：ん、おお、大丈夫だ。心配するな。");

                                        UpdateMainMessage("ラナ：今まで伏せてたヤツがどう大丈夫だって言うのよ、本当に大丈夫？");

                                        UpdateMainMessage("ヴェルゼ：アイン君、一体何を見たんですか？ここには何も書かれてないようですが");

                                        UpdateMainMessage("アイン：いや、良く覚えてねえが、「終わりへと足を運ぶな」とか何とか");

                                        UpdateMainMessage("ヴェルゼ：・・・");

                                        UpdateMainMessage("アイン：まあ何かのトラップだったんだろ。ったく、タチの悪い嫌がらせだな。");

                                        UpdateMainMessage("ヴェルゼ：・・・ええ、隠し通路だからと言って必ずしもお宝というわけにはいかなそうですね。");

                                        UpdateMainMessage("ラナ：まあ、大した事なさそうで良かったわ。っさ、戻りましょ？");

                                        UpdateMainMessage("アイン：ああ、そうだな。");

                                        GroundOne.PlayDungeonMusic(Database.BGM02, Database.BGM02LoopBegin);
                                        we.SpecialInfo3 = true;
                                    }
                                    return;

                                default:
                                    mainMessage.Text = "アイン：ん？特に何もなかったと思うが。";
                                    return;
                            }
                        }
                    }
                }
                #endregion
                #region "ダンジョン４階イベント"
                else if (we.DungeonArea == 4)
                {
                    for (int ii = 0; ii < 35; ii++)
                    {
                        if (CheckTriggeredEvent(ii))
                        {
                            detectEvent = true;
                            switch (ii)
                            {
                                case 0:
                                    mainMessage.Text = "アイン：３階へ戻る階段だな。ここは一旦戻るか？";
                                    using (YesNoRequestMini ynr = new YesNoRequestMini())
                                    {
                                        ynr.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                        ynr.StartPosition = FormStartPosition.CenterParent;
                                        ynr.ShowDialog();
                                        if (ynr.DialogResult == DialogResult.Yes)
                                        {
                                            SetupDungeonMapping(3);
                                            mainMessage.Text = "";
                                        }
                                        else
                                        {
                                            mainMessage.Text = "";
                                        }
                                    }
                                    return;
                                case 1:
                                    if (!we.InfoArea420)
                                    {
                                        UpdateMainMessage("アイン：・・・おい、あったぞ。");

                                        UpdateMainMessage("ラナ：・・・");

                                        UpdateMainMessage("ヴェルゼ：・・・看板・・・ですね？");

                                        UpdateMainMessage("アイン：ああ。");

                                        UpdateMainMessage("ヴェルゼ：アイン君、何と書いてあります？");

                                        UpdateMainMessage("アイン：・・・こう書いてあるぜ。");

                                        if (!we.FailArea4211 && !we.FailArea4212)
                                        {
                                            UpdateMainMessage("　　　　『真実の言葉４：　　生と死　　　』");
                                            we.SpecialInfo4 = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("　　　　『真実の言葉４：　　死　　』");
                                        }
                                        UpdateMainMessage("アイン：・・・　・・・　ラナ。");

                                        UpdateMainMessage("ラナ：・・・");

                                        UpdateMainMessage("アイン：この看板の言葉、とてもじゃねえが軽い冗談とは思えねえ。");

                                        UpdateMainMessage("ラナ：・・・");

                                        UpdateMainMessage("ヴェルゼ：アイン君、一旦最下層の５階へ降りて、ダンジョンの外へ出ましょう。");

                                        UpdateMainMessage("アイン：あ、ああ、そうだな・・・そうするか。話は外へ出てからだ。");
                                        we.TruthWord4 = true;
                                        we.InfoArea420 = true;
                                    }
                                    else
                                    {
                                        if (we.SpecialInfo4)
                                        {
                                            UpdateMainMessage("　　　　『真実の言葉４：　　生と死　　』", true);
                                        }
                                        else
                                        {
                                            UpdateMainMessage("　　　　『真実の言葉４：　　死　　』", true);
                                        }
                                    }
                                    return;
                                case 2:
                                    mainMessage.Text = "アイン：最下層５階への階段だ。降りるぜ？";
                                    using (YesNoRequestMini ynr = new YesNoRequestMini())
                                    {
                                        ynr.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                        ynr.ShowDialog();
                                        if (ynr.DialogResult == DialogResult.Yes)
                                        {
                                            SetupDungeonMapping(5);
                                            mainMessage.Text = "";

                                            if (!we.InfoArea51)
                                            {
                                                UpdateMainMessage("アイン：よし、じゃあ【遠見の青水晶】を使うぜ。");
                                                we.InfoArea51 = true;
                                                CallHomeTown(true);
                                            }
                                        }
                                        else
                                        {
                                            mainMessage.Text = "";
                                        }
                                    }
                                    return;
                                case 3:
                                    this.Update();
                                    mainMessage.Text = "アイン：ボスとの戦闘だ！気を引き締めていくぜ！";
                                    mainMessage.Update();
                                    bool result = EncountBattle("四階の守護者：Altomo");
                                    if (!result)
                                    {
                                        UpdatePlayerLocationInfo(this.Player.Location.X + moveLen, this.Player.Location.Y);
                                    }
                                    else
                                    {
                                        we.CompleteSlayBoss4 = true;
                                    }
                                    mainMessage.Text = "";
                                    return;
                                    
                                case 4:
                                    if (!we.InfoArea41)
                                    {
                                        UpdateMainMessage("アイン：・・・何もねえな。看板とか出てこないのかよ？");

                                        UpdateMainMessage("ラナ：良いじゃない、別に何も無ければ。何か物足りないわけ？");

                                        UpdateMainMessage("アイン：いや、このまま最後まで行かせてくれればラッキーだなと思ってよ。");

                                        UpdateMainMessage("ラナ：気にしすぎよ。ココまで来た人自体が少ないんだし、意外とこんな物なんじゃない？");

                                        UpdateMainMessage("アイン：ああ、そうだな。おっしゃ、このまま進もうぜ！");

                                        UpdateMainMessage("ラナ：ええ、行きましょ。");

                                        UpdateMainMessage("        「ラナが前へ少し進むのを見た後、アインの後ろから声がした。」");

                                        UpdateMainMessage("ヴェルゼ：アイン君。ちょっとこちらへ。");

                                        UpdateMainMessage("アイン：おお、ヴェルゼ。何の用だ？");

                                        UpdateMainMessage("ヴェルゼ：アイン君にはどうしても話しておかねばならない事があります。");

                                        UpdateMainMessage("アイン：何の話だ？言ってみてくれよ。");

                                        UpdateMainMessage("ヴェルゼ：・・・ラナさんの事です。");

                                        UpdateMainMessage("アイン：ラナ？アイツが何かあるのか？");

                                        UpdateMainMessage("ヴェルゼ：彼女はこのダンジョンを進む上で、重大な事実を隠してます。");

                                        UpdateMainMessage("アイン：重大な事実？一体どういう事だ？");

                                        UpdateMainMessage("ヴェルゼ：それはですね・・・");

                                        UpdateMainMessage("        「ラナ：オーイ、そこのバカー！いつまで突っ立ってんのよ？ヴェルゼさんも早く♪」");

                                        UpdateMainMessage("アイン：お、おお！今行くぜ！");

                                        we.InfoArea41 = true;
                                    }
                                    return;

                                case 5:
                                    if (!we.InfoArea42)
                                    {
                                        UpdateMainMessage("ラナ：・・・ねえ、バカアイン。");

                                        UpdateMainMessage("アイン：普通の調子でバカを付けるな。何だ？");

                                        UpdateMainMessage("ラナ：うーん、何でも無いわ。");

                                        UpdateMainMessage("アイン：何だ、何でもねえのか。じゃあこのまま先へ進むぞ！");

                                        UpdateMainMessage("ラナ：うん、ッササっと進みましょ♪");

                                        UpdateMainMessage("        「アインが前へ少し進むのを見た後、ラナの横後ろから声がした。」");

                                        UpdateMainMessage("ヴェルゼ：ラナさん、少しこちらへ。");

                                        UpdateMainMessage("ラナ：何？ヴェルゼさん。");

                                        UpdateMainMessage("ヴェルゼ：アイン君は、何故このダンジョンへ挑んでるのですか？");

                                        UpdateMainMessage("ラナ：何故？う、うーん、最下層に到達したいとか単純な理由よ。");

                                        UpdateMainMessage("ヴェルゼ：ひょっとしたらですが、そうでは無いのでは？");

                                        UpdateMainMessage("ラナ：そうでは無い？別の目的があるって事？");

                                        UpdateMainMessage("ヴェルゼ：はい。ボクは３階から途中参加なので分かりませんが。");

                                        UpdateMainMessage("ヴェルゼ：アイン君はどことなくですが、ボクに何か隠していますね。");

                                        UpdateMainMessage("ラナ：うーん、そうかしら？あのバカに限ってあり得ないと思うんだけど。");

                                        UpdateMainMessage("ヴェルゼ：一度それとなく聞いてみてください。杞憂かもしれませんが。");

                                        UpdateMainMessage("        「アイン：ん？おい、何やってんだ？早く行くぞ！隊列を乱すな。」");

                                        UpdateMainMessage("ラナ：分かったわ、後で聞いてみます。ありがとう、ヴェルゼさん。");

                                        UpdateMainMessage("ヴェルゼ：行きましょう。まだまだ先は長そうです。");

                                        we.InfoArea42 = true;
                                    }
                                    return;

                                case 6:
                                    if (!we.InfoArea43)
                                    {
                                        UpdateMainMessage("アイン：何かグルグルした作りだな。これ本当に進んでるのか？");

                                        UpdateMainMessage("ラナ：本当よね。何だか進んでるのか戻ってるのか錯覚しそうになるわね。");

                                        UpdateMainMessage("アイン：なあ、ラナ。");

                                        UpdateMainMessage("ラナ：何よ？");

                                        UpdateMainMessage("アイン：お前、ひょっとしてさ。");

                                        UpdateMainMessage("ラナ：っあ、そうそう！　アインてさ、ヴェルゼさんとDUELやったんでしょ？");

                                        UpdateMainMessage("アイン：ん？ああ、負けちまったけどな。");

                                        UpdateMainMessage("ヴェルゼ：この４階に入る前にダンジョンゲート裏で偶然会いましてね。");

                                        UpdateMainMessage("アイン：ヴェルゼの全盛期ってどんな感じだったんだ？");

                                        UpdateMainMessage("ヴェルゼ：・・・そうですね、強いて言えば“真空”のようなイメージをしてください。");

                                        UpdateMainMessage("ヴェルゼ：基本的にダメージレースはやりません。一撃目でほとんど勝負を決める感じです。");

                                        UpdateMainMessage("ラナ：ヴェルゼさんって凄い初期モーションが早いですよね！あれ教えてくれませんか♪");

                                        UpdateMainMessage("ヴェルゼ：ええ、良いですよ。まず体の重心と手首の方向は常に重なるようにしてください。");

                                        UpdateMainMessage("ヴェルゼ：それから、足の踏み込む方向も普段から意識してください。");

                                        UpdateMainMessage("ヴェルゼ：良いですか？ゆっくりやってみます。良く見ててくださいね。");

                                        UpdateMainMessage("ヴェルゼ：・・・ッハイ・・・ファイア・ボールです。");

                                        UpdateMainMessage("アイン：なるほどな、確かに今のなら俺にも見えたぜ。");

                                        UpdateMainMessage("ラナ：十分早いわね。どこでこんなのを覚えたんですか？");

                                        UpdateMainMessage("ヴェルゼ：ハハ、そう言われると困るんですが、何となくです。");

                                        UpdateMainMessage("ヴェルゼ：アイン君もラナさんも十分素質はあります。またダンジョンゲート裏に来てください。");

                                        UpdateMainMessage("ラナ：ええ、行ってみるわ。よろしくお願いします♪");

                                        UpdateMainMessage("アイン：なあ、ラナ。えっと、お前ひょっとしてさ。");

                                        UpdateMainMessage("ラナ：ッササ、行きましょ♪");

                                        UpdateMainMessage("アイン：あ、ああ・・・");

                                        we.InfoArea43 = true;
                                    }
                                    return;

                                case 7:
                                    if (!we.InfoArea44)
                                    {
                                        UpdateMainMessage("アイン：どうやら反時計回りに変わったみたいだな。");

                                        UpdateMainMessage("ラナ：・・・そうだね。・・・ねえ、バカアイン。");

                                        UpdateMainMessage("アイン：普通のリアクションにバカを織り交ぜるな。何だ？");

                                        UpdateMainMessage("ラナ：うーん、何でも無いわ。");

                                        UpdateMainMessage("アイン：おい、何だか探られていない感触が余計気になるぞ。");

                                        UpdateMainMessage("ラナ：アイン、ちゃんと答えなさい。良いわね？");

                                        UpdateMainMessage("アイン：おお、何だ？言ってみろ。");

                                        UpdateMainMessage("ラナ：あんた、何か隠してるでしょ？");

                                        UpdateMainMessage("アイン：何かって何だ？");

                                        UpdateMainMessage("ラナ：そりゃアインが隠してる事よ。っさ、答えてちょうだい。");

                                        UpdateMainMessage("アイン：どんだけ無理やりなんだよ。そんな無理やりな質問には答えられねえ。");

                                        UpdateMainMessage("ラナ：何よ、やっぱり教えられない内容なんだ。わかったわ。");

                                        UpdateMainMessage("アイン：おい待て待て。本当に何の事を言ってるんだ？");

                                        UpdateMainMessage("ラナ：イイわよ。本当の事なんか答える気ないんでしょ。");

                                        UpdateMainMessage("アイン：っな！・・・じゃあ聞くがな、ラナ。");

                                        UpdateMainMessage("ラナ：何よ？");

                                        UpdateMainMessage("アイン：お前の方こそ、このダンジョンに入る際、何か重大な事実を隠してるだろ？");

                                        UpdateMainMessage("ラナ：っええぇえええぇぇええ！？！？！？！？！？");

                                        UpdateMainMessage("アイン：うおぁ！デケエ声出すな。何そんな驚いた顔してんだよ。こっちが驚いたぜ。");

                                        UpdateMainMessage("アイン：ったく、ほら見ろ。俺よりもな、まず先にお前から話せよ。");

                                        UpdateMainMessage("ラナ：知らない！！");
                                        
                                        UpdateMainMessage("ラナ：私、知らないからね！！");
                                        
                                        UpdateMainMessage("ラナ：絶対知らないわよ！！！");

                                        UpdateMainMessage("ヴェルゼ：ラナさん、落ち着いて下さい。アイン君も、少し女性には優しく接してください。");

                                        UpdateMainMessage("アイン：あ、ああ・・・いや、しかしすげえ動揺っぷりだな。いやいや、悪かった。");

                                        UpdateMainMessage("ラナ：・・・行きましょ。");
                                        
                                        we.InfoArea44 = true;
                                    }
                                    return;

                                case 8:
                                    if (!we.InfoArea45)
                                    {
                                        UpdateMainMessage("アイン：なあ・・・ラナ。");

                                        UpdateMainMessage("ラナ：何？知らないって言ってるじゃない！？");

                                        UpdateMainMessage("アイン：い、いやいや、違う違う。お前、ガンツ叔父さんに何教えてもらってんだ？");

                                        UpdateMainMessage("ラナ：え？何ってそりゃあ、武術よ。");

                                        UpdateMainMessage("アイン：ガンツ叔父は武器一筋だと思ってたんだけどな。武術も出来るのか。");

                                        UpdateMainMessage("ラナ：そうね、意外だったわ。最初は少し疑問に感じたんだけど。");

                                        UpdateMainMessage("ラナ：最初の手合わせしてもらった時にピンと来たの。");

                                        UpdateMainMessage("ラナ：自然体の構え、厳格だけどとても柔らかい雰囲気。何となく誰かに似てる気がするのよね。");

                                        UpdateMainMessage("ヴェルゼ：おや？知らないようですね。ガンツ・ギメルガはエルミの師匠ですよ。");

                                        UpdateMainMessage("アインとラナ：っええええぇぇぇええええ！！！！");

                                        UpdateMainMessage("ヴェルゼ：物凄いサラウンド騒音ですね・・・すいません、驚かせるつもりはなかったのですが。");

                                        UpdateMainMessage("ラナ：ポーション作成、武具作成、武術鍛錬、何事も心が大事っていつも言うの。");

                                        UpdateMainMessage("ラナ：あとはそうね。　（声マネ）『ガンツ：正々堂々と戦いなさい』って渋い顔で言うわ。");

                                        UpdateMainMessage("ヴェルゼ：エルミのフェア精神。きっとガンツ・ギメルガの影響でしょう。");

                                        UpdateMainMessage("アイン：ヴェルゼとエルミは同い年なんだろ？ヴェルゼは教えてもらわなかったのか？");

                                        UpdateMainMessage("ヴェルゼ：（声マネ）『ガンツ：君に教える事はない。君自身の問題だ』と渋い顔で断られました。");

                                        UpdateMainMessage("ラナ：ヴェルゼさん、何でも出来るしガンツ叔父さんは十分だと判断したんじゃないかしら？");

                                        UpdateMainMessage("ヴェルゼ：ハハ、買いかぶりです。当時ボクは貧弱でしたから。素質無しと見られたんでしょう。");

                                        UpdateMainMessage("アイン：お前ら良いよな。俺なんか　（声マネ）『ガンツ：アイン、精進しなさい』だけだぜ？");

                                        UpdateMainMessage("ラナ：ッフフ、ガンツ叔父さんってアインにはそればっかりよね♪");

                                        UpdateMainMessage("アイン：おお、まったくだぜ。俺にも武術の一つぐらい教えて欲しいもんだ。");

                                        UpdateMainMessage("ラナ：アインは剣が似合ってるじゃない、そういう事なんじゃないの？");

                                        UpdateMainMessage("アイン：まあ正直、拳武術ってのは性に合わねえな。そういう事かも知れねえな。");

                                        UpdateMainMessage("アイン：でもエルミ国王も剣だったんだろう？");

                                        UpdateMainMessage("ヴェルゼ：いえ、当時は拳武術でしたよ。エルミは斬る行為そのものを嫌ってましたから。");

                                        UpdateMainMessage("アイン：そうなのか。最初は拳武術でそこから剣術に転向したんだな。");

                                        UpdateMainMessage("ラナ：国王エルミ様ってどんな剣術なのかしら。一度会ってみたいわ。");

                                        UpdateMainMessage("ヴェルゼ：公式・非公式を問わず、申請すればいいんですよ。１００％受けてくれますよ。");

                                        UpdateMainMessage("ラナ：っえ！本当ですか！？　やったわアイン！申請しましょ♪");

                                        UpdateMainMessage("アイン：ッハッハッハ！ダンジョンが終わってから申請してみるとするか！！");

                                        UpdateMainMessage("ラナ：っあ・・・・・・");

                                        UpdateMainMessage("ラナ：っえ、ええそうね・・・・・・ダンジョンが終わったらね。");

                                        UpdateMainMessage("アイン：ん？　何だ、急に。");

                                        UpdateMainMessage("ラナ：っう、ううん、良いの良いの。っさ、進みましょ。");

                                        UpdateMainMessage("アイン：おお、ガンガン進むとするか！");
                                        we.InfoArea45 = true;
                                    }
                                    return;

                                case 9:
                                    if (!we.InfoArea46)
                                    {
                                        UpdateMainMessage("アイン：ふう、どうやらやっとグルグルした所を抜けたみたいだな・・・正直疲れたぜ。");

                                        UpdateMainMessage("ラナ：・・・アイン。");

                                        UpdateMainMessage("アイン：バカは付けなくて良いのか？ 何だよ。");

                                        UpdateMainMessage("ラナ：いっぺんさ。遠見の青水晶で町へ戻らない？");

                                        UpdateMainMessage("アイン：何？戻りたいのか？せっかくグルグルし終えた所なんだがな。");

                                        UpdateMainMessage("ラナ：私も何だか・・・凄く疲れちゃって。　どう、いっぺん休憩しない？");

                                        UpdateMainMessage("アイン：ヴェルゼはどうだ？");

                                        UpdateMainMessage("ヴェルゼ：ボクはどちらでも構いませんよ。");

                                        UpdateMainMessage("アイン：そうだなあ・・・どうすっかな。");

                                        // [警告]：２週目は戻る・戻らないの選択肢を出してください。

                                        UpdateMainMessage("アイン：そうだな、ラナの体調を無視は出来ねえ。一旦戻るとするか。");

                                        UpdateMainMessage("ヴェルゼ：賢明な判断だと思います。ボクも賛成ですね。");

                                        UpdateMainMessage("アイン：っしゃ、じゃあ使うぜ！！　『アインは遠見の青水晶を使った』");

                                        we.InfoArea46 = true;

                                        CallHomeTown();
                                    }
                                    return;

                                case 10:
                                    if (!we.InfoArea48)
                                    {
                                        UpdateMainMessage("アイン：長えよな、やっぱりここ。");

                                        UpdateMainMessage("ラナ：ほんと、先が見えないわね。まだこれでも半分ぐらいしか進めてないわ。");

                                        UpdateMainMessage("アイン：ラナ、ちょっと良いか？ここで一休憩だ。");

                                        UpdateMainMessage("ラナ：え、ええ良いわよ。");

                                        UpdateMainMessage("アイン：ラナ、一つだけ教えておこうと思う。実は昨日いろいろ思い出してみた。");

                                        UpdateMainMessage("ラナ：え？");

                                        UpdateMainMessage("アイン：俺の隠し事についてだ。");

                                        UpdateMainMessage("ラナ：あ、ああ、良いわよ良いわよ。どうせ大した内容でもないだろうし。");

                                        UpdateMainMessage("アイン：正直に言おう。俺は何も隠してねえ。本当だ。");

                                        UpdateMainMessage("ラナ：うん、分かったわ・・・");

                                        UpdateMainMessage("アイン：実は昨日、夢を見た。とても奇妙な夢だ。");

                                        UpdateMainMessage("ヴェルゼ：・・・どんな夢です？");

                                        UpdateMainMessage("アイン：それがな、全然理解できねえ。意味不明だらけだった。");

                                        UpdateMainMessage("アイン：元々夢なんてもの自体、意味不明だけどな。だがこれだけは言える。");

                                        UpdateMainMessage("アイン：あの夢の内容が、俺が隠してる事実だ。");

                                        UpdateMainMessage("ラナ：もう良いの、済んだ事だし。ッササ、先に行きましょ♪");

                                        UpdateMainMessage("アイン：待てよラナ。夢の内容、ほんの少しだけ覚えてるんだ、聞いてくれるか？");

                                        UpdateMainMessage("ヴェルゼ：アイン君、十分休めましたからね。ここは先に進みましょう。");

                                        UpdateMainMessage("アイン：ああ・・・そうするか。じゃ先へ進むぞ！");

                                        we.InfoArea48 = true;
                                    }
                                    return;

                                case 11:
                                    if (!we.InfoArea49)
                                    {
                                        UpdateMainMessage("アイン：随分とグネグネした道だ。面倒くせえな・・・");

                                        UpdateMainMessage("ラナ：うん、そうだね。");

                                        UpdateMainMessage("アイン：・・・イヤリングだ。");

                                        UpdateMainMessage("ラナ：・・・");

                                        UpdateMainMessage("アイン：ほんの僅かだが、目にかすめた。あれは確かにイヤリングだった。");

                                        UpdateMainMessage("ラナ：・・・そう。誰かのイヤリング？それともお店で売ってるような？");

                                        UpdateMainMessage("アイン：分からねえ。そこを思考追跡すると、ひどく頭痛がするんだ。");

                                        UpdateMainMessage("ヴェルゼ：アイン君、無理をして思い出す必要はありません。");

                                        UpdateMainMessage("アイン：ラナ、お前何か知らないか？");

                                        UpdateMainMessage("ラナ：し、知らないわ！！知らないって言ってるじゃないのよ！！");

                                        UpdateMainMessage("アイン：あ、いや悪い。悪かったな・・・先へ進むか。");

                                        UpdateMainMessage("ラナ：ご・・・ごめんね。");

                                        UpdateMainMessage("アイン：良いから、気にするなって。さあ、先進むぞ！");

                                        UpdateMainMessage("ラナ：うん。");

                                        we.InfoArea49 = true;
                                    }
                                    return;

                                case 12:
                                    if (!we.InfoArea410)
                                    {
                                        UpdateMainMessage("ヴェルゼ：アイン君。");

                                        UpdateMainMessage("アイン：何だ！？ヴェルゼ！？");

                                        UpdateMainMessage("ヴェルゼ：そうピリピリしないでください。今ちょうど半分まで進み終わった所ですよ。");

                                        UpdateMainMessage("アイン：・・・はああぁぁぁぁ・・・まだ半分かよ！？");

                                        UpdateMainMessage("アイン：ラナ、体調の方は大丈夫か？");

                                        UpdateMainMessage("        『アインがラナの方を振り向いた時、ラナは顔を両手で伏せていた。』");

                                        UpdateMainMessage("アイン：っな・・・ラナ！大丈夫かよ！？");

                                        UpdateMainMessage("ラナ：アイン、ご・・・ごめんなさい。・・・私・・・ごめんなさい。");

                                        UpdateMainMessage("アイン：っなに謝ってんだ。さっきの事か？全然気にしてねえからさ。っな？");

                                        UpdateMainMessage("ラナ：ううん、違うの。アイン、私が悪いの。ゴメンなさい。");

                                        UpdateMainMessage("アイン：なあお前さ。昔っから変なトコで謝るな。イイって言ってるじゃねえか、元気出せよ？");

                                        UpdateMainMessage("ラナ：・・・うん・・・うん・・・。");

                                        UpdateMainMessage("        『ラナは、まだ両手を顔から離していない。』");

                                        UpdateMainMessage("ヴェルゼ：アイン君、一旦戻りませんか？このまま進んでは危険です。");

                                        UpdateMainMessage("アイン：ああ、一旦遠見の青水晶を使うぜ。ラナ、一旦戻るぞ？");

                                        UpdateMainMessage("ラナ：・・・・・・");

                                        // [警告]：２週目は戻る・戻らないの選択肢を出してください。

                                        UpdateMainMessage("アイン：っそれ！遠見の青水晶！");

                                        we.InfoArea410 = true;

                                        CallHomeTown();
                                    }
                                    return;

                                case 13:
                                    we.Treasure41 = GetTreasure("エスパダス");
                                    return;

                                case 14:
                                    we.Treasure42 = GetTreasure("グリーンマテリアル");
                                    return;

                                case 15:
                                    we.Treasure43 = GetTreasure("特大青ポーション");
                                    return;

                                case 16:
                                    we.Treasure44 = GetTreasure("ロリカ・セグメンタータ");
                                    return;

                                case 17:
                                    we.Treasure45 = GetTreasure("ブリガンダィン");
                                    return;

                                case 18:
                                    we.Treasure46 = GetTreasure("アヴォイド・クロス");
                                    return;

                                case 19:
                                    we.Treasure47 = GetTreasure("夢見の印章");
                                    return;

                                case 20:
                                    we.Treasure48 = GetTreasure("天使の契約書");
                                    return;

                                case 21:
                                    if (we.InfoArea411 && !we.InfoArea412) // InfoArea411はHomeTownで立ちます。
                                    {
                                        UpdateMainMessage("ヴェルゼ：アイン君、ラナさん。");

                                        UpdateMainMessage("アイン：どうした、ヴェルゼ。");

                                        UpdateMainMessage("ヴェルゼ：ここ、実は隠し扉になっていたようです。見てください。");

                                        we.InfoArea412 = true;
                                        ConstructDungeonMap();
                                        SetupDungeonMapping(4);

                                        UpdateMainMessage("ラナ：あ、本当だわ。意外な所にあったものね。");

                                        UpdateMainMessage("ヴェルゼ：あの回廊、何度も通る気にはなれませんからね。ひょっとしたらと思って。");

                                        UpdateMainMessage("アイン：いやあ、助かるぜ！サンキュー、ヴェルゼ！");

                                        UpdateMainMessage("ヴェルゼ：いえいえ、このダンジョン構成に感謝しましょう。");

                                        UpdateMainMessage("ラナ：じゃあ、進みましょうか♪");

                                        UpdateMainMessage("アイン：ああ、先を急ぐぞ！");
                                    }
                                    return;

                                case 22:
                                    if (!we.InfoArea413)
                                    {
                                        UpdateMainMessage("アイン：俺さ・・・何となくだけど。");

                                        UpdateMainMessage("ラナ：・・・");

                                        UpdateMainMessage("アイン：昨日、ずっと考えてたんだ。");

                                        UpdateMainMessage("ラナ：・・・何を？");

                                        UpdateMainMessage("アイン：俺はひょっとしたら、ラナの事何にも知らねえのかもな。");

                                        UpdateMainMessage("ラナ：何それ。何でも知ってたらそれこそ気持ち悪いわよ。");

                                        UpdateMainMessage("アイン：ッハッハッハ！確かにそりゃそうだな。");

                                        UpdateMainMessage("アイン：俺は正直な所、さっぱり検討がつかねえからさ。");

                                        UpdateMainMessage("ラナ：うん。");

                                        UpdateMainMessage("アイン：４階制覇だ！まずはそれをキッチリやってしまおうと思う！");

                                        UpdateMainMessage("ラナ：うん、うん。");

                                        UpdateMainMessage("アイン：それで、どんな内容が待ち受けていようとも、必ず受け止めるぜ。");

                                        UpdateMainMessage("ラナ：そうね・・・その方がいつものバカアインっぽくて良いと思うわ。");

                                        UpdateMainMessage("アイン：ああ、バカで結構！　っさ、行くぞ！　ッハッハッハ！！");

                                        UpdateMainMessage("ヴェルゼ：アイン君・・・いずれボクからも話をさせてください。");

                                        UpdateMainMessage("アイン：何だよ、ヴェルゼまで隠し事か？");

                                        UpdateMainMessage("アイン：じゃあ４階制覇する時に全員でカミングアウトだな。覚悟しておけよ。");

                                        UpdateMainMessage("ヴェルゼ：ええ、そちらこそ心の準備をしておいてください。");

                                        UpdateMainMessage("ラナ：終わりも近くなってきてるわ。行きましょう。");
                                        we.InfoArea413 = true;
                                    }
                                    return;

                                case 23:
                                    if (!we.InfoArea414)
                                    {
                                        UpdateMainMessage("アイン：しかし、このグルグル回廊は何かと不安にさせる仕組みだな。");

                                        UpdateMainMessage("ラナ：マップメモしているとは言え、何となく不安になってしまうわね。");

                                        UpdateMainMessage("アイン：一本道ってのも結構しんどいとは思わないか？寄り道できる箇所が一つもねえからな。");

                                        UpdateMainMessage("ヴェルゼ：アイン君でも迷う場合があるんですね。");

                                        UpdateMainMessage("アイン：んだそれ。まるで俺が迷わない人間みたいな言い方だな、ヴェルゼ。");

                                        UpdateMainMessage("ヴェルゼ：ッハハハ。これは失礼しました。");

                                        UpdateMainMessage("ラナ：アインはさ、迷った時ってどうしてるわけ？");

                                        UpdateMainMessage("アイン：迷った時か？そうだな。");

                                        UpdateMainMessage("アイン：食べて、寝る！　ッハッハッハ！");

                                        UpdateMainMessage("ラナ：はああぁぁぁ・・・何でまともな答えが返ってこないのかしら。");

                                        UpdateMainMessage("アイン：何となくだけどよ。考えて良いときと悪い場合ってのがあるのさ。");

                                        UpdateMainMessage("ラナ：そう？");

                                        UpdateMainMessage("アイン：ああ、今回の４階みたいなのは特に考えるだけ深みにハマるだけだ。");

                                        UpdateMainMessage("アイン：解いてしまおうぜ、話はそれからだ。じゃねえと進めねえ。だろ？");

                                        UpdateMainMessage("ラナ：それってやっぱり“悩んでる”とは少し違うわね・・・");

                                        UpdateMainMessage("アイン：カタイ事言うなって、ほら先行くぜ！");
                                        we.InfoArea414 = true;
                                    }
                                    return;

                                case 24:
                                    if (!we.InfoArea415)
                                    {
                                        UpdateMainMessage("ラナ：ねえ・・・アイン。");

                                        UpdateMainMessage("アイン：ん？何だ？");

                                        UpdateMainMessage("ラナ：アンタ少し、フラついてない？");

                                        UpdateMainMessage("アイン：ん？ん、ああ。");

                                        UpdateMainMessage("ラナ：とぼけないで、ちゃんとこっち見て言いなさいよ、ホラ。");

                                        UpdateMainMessage("ヴェルゼ：ラナさん。ちょっとこちらへ。");

                                        UpdateMainMessage("ラナ：っえ？え、ええ・・・");

                                        UpdateMainMessage("ヴェルゼ：アイン君の様子、少しおかしいと思いませんか？");

                                        UpdateMainMessage("        『アインの足取りがおぼつかない。』");

                                        UpdateMainMessage("ラナ：うーん、さすがにバテて来てる感じはするけどね。");

                                        UpdateMainMessage("ヴェルゼ：・・・どうしても駄目なら、ラナさんが遠見の青水晶を使ってください。");

                                        UpdateMainMessage("ラナ：ええ、わかったわ。");

                                        UpdateMainMessage("        「アイン：ん？オラ、お前ら置いて行くぞ！」");

                                        UpdateMainMessage("ラナ：足取りフッラフラのくせして・・・まったく大丈夫なのかしら。");
                                        we.InfoArea415 = true;
                                    }
                                    return;

                                case 25:
                                    if (!we.InfoArea416)
                                    {
                                        UpdateMainMessage("        『アインの足取りがフラフラしている。』");

                                        UpdateMainMessage("ヴェルゼ：アイン君・・・大丈夫ですか？");

                                        UpdateMainMessage("アイン：到達・・・するんだ・・・あの場所へ・・・");

                                        UpdateMainMessage("ラナ：おい、そこのバカ。ちゃんとこっち向きなさいよ！");

                                        UpdateMainMessage("アイン：ラナ・・・俺が・・・");
                                        
                                        UpdateMainMessage("ラナ：え？");

                                        UpdateMainMessage("アイン：連れて・・・　・・・　だから　・・・　泣くな　・・・");

                                        UpdateMainMessage("ヴェルゼ：いけない。　ラナさん。");

                                        UpdateMainMessage("ラナ：う、うん。アイン、一回戻るわよ？");

                                        UpdateMainMessage("アイン：・・・　・・・　ほら　・・・　こっちだ　・・・");

                                        UpdateMainMessage("ラナ：ッハイ！　遠見の青水晶！");

                                        // [警告]：２週目は戻る・戻らないの選択肢を出してください。

                                        we.InfoArea416 = true;
                                        CallHomeTown();
                                    }
                                    return;

                                case 26:
                                    if (we.InfoArea417 && !we.InfoArea418) // InfoArea417はHomeTownで立ちます。
                                    {
                                        UpdateMainMessage("ヴェルゼ：アイン君");

                                        UpdateMainMessage("アイン：おお、ヴェルゼ。心配しなくても、俺はもう大丈夫だ。");

                                        UpdateMainMessage("ヴェルゼ：いえ、そうではなくて。ここにも隠し扉があります。");

                                        we.InfoArea418 = true;
                                        ConstructDungeonMap();
                                        SetupDungeonMapping(4);

                                        UpdateMainMessage("アイン：マジかよ！？いやあ、助かるぜ！");

                                        UpdateMainMessage("ヴェルゼ：このダンジョン構成、本当に感服しますね。");

                                        UpdateMainMessage("ラナ：まるで中間ポイントまで進んだ人のために作られているって感じよね。");

                                        UpdateMainMessage("アイン：まあまあ良いじゃねえかよ。変な行き止まりやワープよりはるかにマシだぜ。");

                                        UpdateMainMessage("ヴェルゼ：そうですね。さあ、先へ進みましょう。");

                                        we.ProgressArea4212 = true;
                                    }
                                    return;

                                case 27:
                                    if (!we.InfoArea419)
                                    {
                                        UpdateMainMessage("ヴェルゼ：アイン君、ラナさん、前を見てください。");

                                        UpdateMainMessage("ヴェルゼ：紛れもなく、あれは４階の守護者です。");

                                        UpdateMainMessage("        『４階の守護者：Altomoは堂々とした姿で待ち受けている。　異常な視線と殺気を感じた。』");

                                        UpdateMainMessage("アイン：っけ、やる気満々じゃねえか。殺気がビリビリと伝わってくるぜ。");

                                        UpdateMainMessage("ラナ：こんな遠くなのに、物凄く獣に睨まれている感じよね。");

                                        UpdateMainMessage("アイン：ラナ。この後、いろいろと教えてくれよ。お前が隠してる事。");

                                        UpdateMainMessage("ラナ：うん。わかったわ。");

                                        UpdateMainMessage("アイン：ヴェルゼ。お前の話も聞かせてくれ。");

                                        UpdateMainMessage("ヴェルゼ：はい。いいですよ。");

                                        UpdateMainMessage("アイン：俺が見る夢の話もするから、聞いてくれよな。");

                                        UpdateMainMessage("ラナ：ええ、ちゃんと聞くわ。");

                                        UpdateMainMessage("アイン：じゃあ・・・行くぜ！");

                                        we.InfoArea419 = true;
                                    }
                                    return;

                                case 28:
                                    we.Treasure49 = GetTreasure("ソード・オブ・ブルールージュ");
                                    return;

                                case 29:
                                    if (!we.FailArea4211 && !we.ProgressArea4211 && !we.TruthWord4)
                                    {
                                        we.ProgressArea4211 = true;
                                    }
                                    return;
                                    
                                case 30:
                                    if (!we.FailArea4211 && we.ProgressArea4211)
                                    {
                                        we.ProgressArea4211 = false;
                                    }
                                    return;

                                case 31:
                                    if (!we.FailArea4211 && we.ProgressArea4211)
                                    {
                                        we.FailArea4211 = true;
                                    }
                                    return;

                                case 32:
                                    if (!we.FailArea4212 && !we.ProgressArea4212 && !we.TruthWord4)
                                    {
                                        we.ProgressArea4212 = true;
                                    }
                                    return;

                                case 33:
                                    if (!we.FailArea4212 && we.ProgressArea4212)
                                    {
                                        we.ProgressArea4212 = false;
                                    }
                                    return;

                                case 34:
                                    if (!we.FailArea4212 && we.ProgressArea4212)
                                    {
                                        we.FailArea4212 = true;
                                    }
                                    return;
                            }
                        }
                    }
                }
                #endregion
                #region "ダンジョン５階イベント"
                else if (we.DungeonArea == 5)
                {
                    for (int ii = 0; ii < 14; ii++)
                    {
                        if (CheckTriggeredEvent(ii))
                        {
                            detectEvent = true;
                            switch (ii)
                            {
                                case 0:
                                    mainMessage.Text = "アイン：４階へ戻る階段だな。ここは一旦戻るか？";
                                    using (YesNoRequestMini ynr = new YesNoRequestMini())
                                    {
                                        ynr.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                        ynr.ShowDialog();
                                        if (ynr.DialogResult == DialogResult.Yes)
                                        {
                                            SetupDungeonMapping(4);
                                            mainMessage.Text = "";
                                        }
                                        else
                                        {
                                            mainMessage.Text = "";
                                        }
                                    }
                                    return;
                                case 1:
                                    if (!we.TruthWord5)
                                    {
                                        UpdateMainMessage("アイン：見ろ・・・看板だ。読むぜ。");

                                        UpdateMainMessage("　　　　『真実の言葉５：　　幻想世界。　終わりにして始まり。』");

                                        we.TruthWord5 = true;

                                        if (we.SpecialInfo4)
                                        {
                                            UpdateMainMessage("アイン：生と死から・・・幻想世界か。終わってから・・・始まり？");
                                        }
                                        else
                                        {
                                            UpdateMainMessage("アイン：死から・・・幻想世界か。終わってから・・・始まり？");
                                        }

                                        UpdateMainMessage("ラナ：この先、どんな事があっても");

                                        UpdateMainMessage("アイン：ん？");

                                        UpdateMainMessage("ラナ：私はアインと一緒に居るから。絶対に。");

                                        UpdateMainMessage("ヴェルゼ：・・・");

                                        UpdateMainMessage("アイン：・・・ッハッハッハ！");

                                        UpdateMainMessage("ラナ：っちょ、何がおかしいのよ？");

                                        UpdateMainMessage("アイン：・・・ッハッハッハッハッハッハッハ！！");

                                        UpdateMainMessage("ラナ：何笑ってるのよ、せっかく最下層に来たんだからもう少し真面目にやんなさいよね。");

                                        UpdateMainMessage("アイン：ラナ、感謝してるぜ。");

                                        UpdateMainMessage("ラナ：もう少し気の利いた言葉ぐらい考えなさいよ。");

                                        UpdateMainMessage("アイン：そうか？じゃあそうだなあ・・・");

                                        UpdateMainMessage("アイン：これ終わったらさ。");

                                        UpdateMainMessage("アイン：もう１回ファージル宮殿行こうぜ。");

                                        UpdateMainMessage("アイン：エルミ王、ファラ様も良いけど、伝説のFiveSeekerヴェルゼにもう１回案内してもらうのさ。");

                                        UpdateMainMessage("アイン：いいだろ？ヴェルゼ。");

                                        UpdateMainMessage("ヴェルゼ：・・・ええ、喜んで。ラナさん、いかがですか？");

                                        UpdateMainMessage("ラナ：そこのバカアイン、ファージル宮殿に着いて飯ばっかりじゃ駄目だからね。");

                                        UpdateMainMessage("アイン：いやいや、飯ばっかりじゃねえぜ。ちゃんと観光もする。");

                                        UpdateMainMessage("ラナ：観光気分じゃなくて、案内をしっかり聞く事。まったく。");

                                        UpdateMainMessage("ラナ：大体アンタが飯の後で寝てたから、ファラ様が気を使って置いてったんじゃない。");

                                        UpdateMainMessage("アイン：マジかよ？あの寝てる間に案内してもらってたのかよ！？");

                                        UpdateMainMessage("ラナ：それすら認識してなかったわけね。本当にバカなんだから。");

                                        UpdateMainMessage("アイン：わ、分かった分かった。次からは寝ないようにするぜ。");

                                        UpdateMainMessage("ラナ：普通は寝・ま・せ・ん・からね。当然の事を当然のようにやって欲しいわ。");

                                        UpdateMainMessage("アイン：分かったって。っしゃ、さっさと最下層のラスボスを倒すぜ！");

                                        UpdateMainMessage("ラナ：待って、アイン。");

                                        UpdateMainMessage("アイン：何だよ？");

                                        // ２周目で最大のヒントとなる発言をさせてください。
                                        UpdateMainMessage("ラナ：・・・ううん、何でもないわ。ッササ、行きましょ♪");

                                        UpdateMainMessage("アイン：何だ引っかかる言い方だな、相変わらず。ッハハハ、さあ行くぜ！！");
                                    }
                                    else
                                    {
                                        UpdateMainMessage("　　　　『真実の言葉５：　　幻想世界。　終わりにして始まり。』", true);
                                    }
                                    return;

                                case 2:
                                    if (!we.InfoArea52)
                                    {
                                        UpdateMainMessage("アイン：・・・居たぜ。どうやらアイツが最後のラスボスってわけだ。");

                                        UpdateMainMessage("        『５階の守護者：Bystanderはその場にある玉座に静かに座している。』");
                                        
                                        UpdateMainMessage("        『その後方には、巨大な歯車と幾つかの小さい歯車がある。』");

                                        // ２周目はパラメタによってストーリー変化を加えてください。
                                        UpdateMainMessage("Bystander：アインのＬｖ、　" + mc.Level.ToString());

                                        UpdateMainMessage("アイン：！？");

                                        UpdateMainMessage("Bystander：アインの力、　" + mc.Strength.ToString());

                                        UpdateMainMessage("Bystander：アインの技、　" + mc.Agility.ToString());

                                        UpdateMainMessage("Bystander：アインの知、　" + mc.Intelligence.ToString());

                                        UpdateMainMessage("Bystander：アインの体、　" + mc.Stamina.ToString());

                                        UpdateMainMessage("Bystander：アインの心、　" + mc.Mind.ToString());

                                        if (mc.MainWeapon != null)
                                        {
                                            UpdateMainMessage("Bystander：アインの装備武器、　" + mc.MainWeapon.Name.ToString());
                                        }
                                        if (mc.MainArmor != null)
                                        {
                                            UpdateMainMessage("Bystander：アインの装備武器、　" + mc.MainArmor.Name.ToString());
                                        }
                                        if (mc.Accessory != null)
                                        {
                                            UpdateMainMessage("Bystander：アインの装備アクセサリ、　" + mc.Accessory.Name.ToString());
                                        }

                                        UpdateMainMessage("アイン：俺のパラメタと装備を・・・全部！？");

                                        UpdateMainMessage("Bystander：『願い』は『叶わず』　　『強さ』は『及ばず』　　『精神』は『満たず』");

                                        UpdateMainMessage("アイン：なんだと？");

                                        UpdateMainMessage("Bystander：アインよ。『何故』、『幻想』で『願い』を『乞う』か？");

                                        UpdateMainMessage("アイン：あ？どういう意味だ、それ？");

                                        UpdateMainMessage("Bystander：アインよ。『所詮』は『幻想』にしか『在らず』");

                                        UpdateMainMessage("アイン：何が幻想だってんだよ？言ってみろよ。");

                                        UpdateMainMessage("Bystander：『死』に対する『願い』、『救われる事』への『願い』、『現実』への『願い』");

                                        UpdateMainMessage("Bystander：『遠く』　『及ばず』　、　『決して』　『及ばず』　、　『永遠』に『及ばず』");

                                        UpdateMainMessage("アイン：・・・・・・っけ・・・すげえムカつく言い方だな。");

                                        UpdateMainMessage("Bystander：アインよ。授けよう。『真実』と『死』");

                                        UpdateMainMessage("Bystander：そして、『自我』が『要求』し続けている『永遠』の『願い』を");

                                        UpdateMainMessage("        『Bystanderが肘を付くと同時に、後方の全ての歯車が激しい光と共に回り始めた！！』");

                                        UpdateMainMessage("アイン：・・・っは、もったいぶった演出しやがって・・・上等だ！！");

                                        UpdateMainMessage("アイン：そこまで言うなら、教えてもらおうじゃねえか！　真実や願いとやら、全てな！！");

                                        UpdateMainMessage("アイン：ラナ、ヴェルゼ、行くぜ！！");

                                        UpdateMainMessage("ラナ：ええ、いつでも良いわよ！");

                                        UpdateMainMessage("ヴェルゼ：いつでも、行けますよ。");

                                        UpdateMainMessage("アイン：おっしゃ！　行くぞ！！！");
                                        we.InfoArea52 = true;
                                    }
                                    else if (we.CompleteSlayBoss5)
                                    {
                                        UpdateMainMessage("アイン：いや・・・もう戻る必要はねえ。");
                                        UpdatePlayerLocationInfo(this.Player.Location.X - moveLen, this.Player.Location.Y);
                                    }
                                    return;

                                case 3:
                                    this.Update();
                                    UpdateMainMessage("アイン：最下層ラスボスだ。俺たちは絶対にヤツを倒す！", true);
                                    bool result = EncountBattle("五階の守護者：Bystander");
                                    if (!result)
                                    {
                                        UpdatePlayerLocationInfo(this.Player.Location.X + moveLen, this.Player.Location.Y);
                                    }
                                    else
                                    {
                                        GroundOne.StopDungeonMusic();
                                        we.CompleteSlayBoss5 = true;
                                        UpdateMainMessage("Bystander：アインよ。『真実』は『汝自身』の『原型』に『存在』する。");

                                        UpdateMainMessage("Bystander：アインよ。『真実』は『深層』への『領域』に『存在』する。");

                                        UpdateMainMessage("Bystander：アインよ。『願い』は『潜在』と『創生』によって『形成』される。");

                                        UpdateMainMessage("Bystander：アインよ。『真実』に対する『願い』は、『私』の後ろにある。");

                                        UpdateMainMessage("Bystander：アインよ。受け取るが良い。　　そして");

                                        UpdateMainMessage("Bystander：アインよ。『終わり』　　が　　　　『今』　　　　　『始まる』");
                                    }
                                    mainMessage.Text = "";
                                    return;

                                case 4:
                                    we.Treasure51 = GetTreasure("オーバーシフティング");
                                    return;

                                case 5:
                                    if (!we.TruthEventForLana)
                                    {
                                        GroundOne.PlayDungeonMusic(Database.BGM06, Database.BGM06LoopBegin);
                                        UpdateMainMessage("アイン：ん・・・これは・・・");

                                        // GetTreasure("ラナのイヤリング"); //ここで荷物いっぱいの時、どう考えても矛盾を防げません。２周目開始時点で持たせます。

                                        UpdateMainMessage("『ラナのイヤリング』を手に入れました。");

                                        UpdateMainMessage("アイン：こ、これはどこかで・・・");

                                        UpdateMainMessage("アイン：！！！！！！！！");

                                        UpdateMainMessage("アイン：っま！　まさかっ！！！！！");

                                        UpdateMainMessage("アイン：お、俺は・・・夢を見てたんじゃねえ・・・");

                                        UpdateMainMessage("アイン：あれが・・・夢じゃねえんだとしたら・・・・・・");

                                        this.BackColor = Color.Red;
                                        UpdateMainMessage("　　　　『アインの脳裏に耐えられない激痛が走った！！！』");

                                        UpdateMainMessage("アイン：ッグ・・・グアアアァァアァアアァァァ！！！！！！");

                                        UpdateMainMessage("　　　　『アインの脳裏に激痛と共に、ある光景が浮かび上がる』");

                                        // [警告]：真実世界の描写は何度も何度も何度も何度も何度も推敲してください。
                                        UpdateMainMessage("　（（　っしまっ！　避けろラナ！！！　））");

                                        UpdateMainMessage("　（（　・・・っえ？　））");

                                        UpdateMainMessage("　（（　ッグサ！！！　））");

                                        UpdateMainMessage("　（（　　『神剣フェルトゥーシュがラナの心臓に刺さった』　））");

                                        UpdateMainMessage("　（（　ラナ！！！　しっかりしろ！！！　））");

                                        UpdateMainMessage("アイン：アアアアアァァァァ！！　ウワワッワッワ・・・ワアアァァァァ！！！！");

                                        UpdateMainMessage("　　　　『アインの脳裏に更なる激痛が走る！！！　続きの光景が浮かび上がる』");

                                        UpdateMainMessage("　（（　アイン・・・ごめんね。ごめんなさい。・・・ご・・・ごめんなさい・・・私　））");

                                        UpdateMainMessage("　（（　やめろ、喋るな！！　））");

                                        UpdateMainMessage("　（（　私はアインと一緒に居るから。絶対に。　））");

                                        UpdateMainMessage("　（（　　『ッブシュ、ブシュウウウゥゥ！！！』（床一面が赤に染まっていく！）　　））");

                                        UpdateMainMessage("　（（　わ・・・私ね。ファ・・・ファージル・・・宮殿・・・　））");

                                        UpdateMainMessage("　（（　分かった。分かったって！！　今直してやる。　フレッシュヒール！！　））");

                                        UpdateMainMessage("　（（　　『神剣フェルトゥーシュの傷痕は、ヒーリング効果を打ち消している。』　））");

                                        UpdateMainMessage("　（（　っくそ、何だこれ！！！　どうなってんだ！！！　フレッシュヒール！！！　））");

                                        UpdateMainMessage("　（（　　『神剣フェルトゥーシュの傷痕は、ヒーリング効果を打ち消し続けている。』　））");

                                        UpdateMainMessage("　（（　ア、アイン・・・宮殿・・・で・・・鏡、見た・・・わ・・・　））");

                                        UpdateMainMessage("　（（　んな鏡どうだって良いだろ！！　さあ、今すぐ治してやるぞ。　））");

                                        UpdateMainMessage("　（（　なあに、し、し、心配すんなって、ッハッハッハ！！！　））");

                                        UpdateMainMessage("　（（　鏡・・・と・・・ヴェ・・・ヴェル・・・ゼ・・・さん・・・　））");

                                        UpdateMainMessage("アイン：や、や、止めて、止めてくれえええぇぇぇぇ！！！！　ワアァァァアアァァァァ！！！！");

                                        UpdateMainMessage("　　　　『アインの脳裏に死にも等しい激痛が走る！！！　続きの光景が浮かび上がる』");

                                        UpdateMainMessage("　（（　ヴェ・・・ヴェル・・・ゼ？　ヴェルゼがなんだ。　））");

                                        UpdateMainMessage("　（（　鏡は・・・ウソ・・・・・・ヴェル・・・ゼ・・・さん・・・が・・・　））");

                                        UpdateMainMessage("　（（　　『ラナは一つ笑ってから・・・』））");

                                        UpdateMainMessage("　（（　・・・アイン、好きだよ。大好きだからね。　））");

                                        UpdateMainMessage("　（（　お、おいおい、冗談は止めろ！！　そんなジョークは治ってから聞いてやる！　な！））");

                                        UpdateMainMessage("　（（　　『ラナの耳からイヤリングが一つ落ちた。』））");

                                        UpdateMainMessage("　（（　・・・ラナ・・・ラナ・・・ラナ！！！しっかりしろ！！！　））");

                                        UpdateMainMessage("　（（　ラナ！！！　ラナァァァァ！！！！　ワアアアアアァァァッァァァァァア！！！　））");

                                        UpdateMainMessage("・・・　・・・　・・・　・・・　・・・");

                                        we.CompleteArea5 = true;
                                        we.TruthEventForLana = true;
                                        this.Hide();
                                        CallHomeTown(true);
                                    }
                                    return;

                                case 6:
                                    if (!we.InfoArea53)
                                    {
                                        UpdateMainMessage("アイン：うお！！！すり抜けられたぜ！？");

                                        UpdateMainMessage("ラナ：ホント、今の全然分からなかったわね。どうやって見つけたのよ？アイン");

                                        UpdateMainMessage("アイン：いや、偶然だが壁の色が微妙に薄かったからな。");

                                        UpdateMainMessage("ヴェルゼ：ここはおそらく隠し部屋と呼ばれる場所ですね。");

                                        UpdateMainMessage("アイン：そうなのか？");

                                        UpdateMainMessage("ヴェルゼ：はい、ボクが探検した時も似たようなケースが幾つかありましたよ。");

                                        UpdateMainMessage("ヴェルゼ：貴重なアイテム、知恵、情報などが隠されている場合がほとんどです。");

                                        UpdateMainMessage("アイン：おっしゃ！楽しみだな！さっそく奥まで行ってみようぜ！");

                                        we.InfoArea53 = true;
                                    }
                                    return;

                                case 7:
                                    we.Treasure52 = GetTreasure("タイム・オブ・ルーセ");
                                    return;

                                case 8:
                                    we.Treasure53 = GetTreasure("リヴァイヴポーション");
                                    return;
                                case 9:
                                    we.Treasure54 = GetTreasure("レジェンド・レッドホース");
                                    return;
                                case 10:
                                    we.Treasure55 = GetTreasure("ルナ・エグゼキュージョナー");
                                    return;
                                case 11:
                                    we.Treasure56 = GetTreasure("蒼黒・氷大蛇の爪");
                                    return;
                                case 12:
                                    we.Treasure57 = GetTreasure("ファージル・ジ・エスペランザ");
                                    return;
                            }
                        }
                    }
                }
                #endregion

                if (!detectEvent)
                {
                    EncountEnemy();
                }
            }
        }

        private bool GetTreasure(string targetItemName)
        {
            using (OKRequest ok = new OKRequest())
            {
                ok.StartPosition = FormStartPosition.Manual;
                ok.Location = new Point(this.Location.X + 540, this.Location.Y + 440);
                mainMessage.Text = "アイン：よっしゃ！お宝だぜ！";
                ok.ShowDialog();
                ItemBackPack backpackData = new ItemBackPack(targetItemName);
                // [警告]：芋プログラミングです。整備してください。
                bool result1 = mc.AddBackPack(backpackData);
                if (result1)
                {
                    mainMessage.Text = "『" + backpackData.Name + "を手に入れました』";
                    ok.ShowDialog();
                    return true;
                }
                else
                {
                    if (we.AvailableSecondCharacter)
                    {
                        bool result2 = sc.AddBackPack(backpackData);
                        if (result2)
                        {
                            mainMessage.Text = "『" + backpackData.Name + "を手に入れました』";
                            ok.ShowDialog();
                            return true;
                        }
                        else
                        {
                            if (we.AvailableThirdCharacter)
                            {
                                bool result3 = tc.AddBackPack(backpackData);
                                if (result3)
                                {
                                    mainMessage.Text = "『" + backpackData.Name + "を手に入れました』";
                                    ok.ShowDialog();
                                    return true;
                                }
                                else
                                {
                                    mainMessage.Text = "荷物がいっぱいです。" + backpackData.Name + "を入手できませんでした。";
                                    ok.ShowDialog();
                                    return false;
                                }
                            }
                            else
                            {
                                mainMessage.Text = "荷物がいっぱいです。" + backpackData.Name + "を入手できませんでした。";
                                ok.ShowDialog();
                                return false;
                            }
                        }
                    }
                    else
                    {
                        mainMessage.Text = "荷物がいっぱいです。" + backpackData.Name + "を入手できませんでした。";
                        ok.ShowDialog();
                        return false;
                    }
                }
            }
        }

        private bool CheckTriggeredEvent(int eventNum)
        {
            switch(we.DungeonArea)
            {
                #region "ダンジョン１階　発生位置"
                case 1:
                    // 真実の言葉１
                    if (Player.Location == new Point(basePosX + moveLen * 9, basePosY + moveLen * 0) && eventNum == 0)
                    {
                        return true;
                    }

                    // ボス１との対戦
                    if (Player.Location == new Point(basePosX + moveLen * 25, basePosY + moveLen * 12) && !we.CompleteSlayBoss1 && eventNum == 1)
                    {
                        return true;
                    }

                    // 宝箱１－１
                    if (Player.Location == new Point(basePosX + moveLen * 1, basePosY + moveLen * 19) && !we.Treasure1 && eventNum == 2)
                    {
                        return true;
                    }

                    // 宝箱１－２
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 0) && !we.Treasure2 && eventNum == 3)
                    {
                        return true;
                    }

                    // 宝箱１－３
                    if (Player.Location == new Point(basePosX + moveLen * 18, basePosY + moveLen * 17) && !we.Treasure3 && eventNum == 4)
                    {
                        return true;
                    }

                    // 町への帰還
                    if (Player.Location == new Point(basePosX + moveLen * 2, basePosY + moveLen * 1) && eventNum == 5)
                    {
                        return true;
                    }

                    // ２階への降り階段
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 19) && eventNum == 6)
                    {
                        return true;
                    }

                    // 隠し通路発見時
                    if (Player.Location == new Point(basePosX + moveLen * 10, basePosY + moveLen * 11) && !we.InfoArea11 && eventNum == 7)
                    {
                        return true;
                    }

                    // スペシャル情報１
                    if (Player.Location == new Point(basePosX + moveLen * 9, basePosY + moveLen * 16) && !we.SpecialInfo1 && eventNum == 8)
                    {
                        return true;
                    }

                    break;
                #endregion
                #region "ダンジョン２階　発生位置"
                case 2:
                    // １階への上り階段
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 19) && eventNum == 0)
                    {
                        return true;
                    }

                    // 真実の言葉２
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 0) && eventNum == 1)
                    {
                        return true;
                    }

                    // ３階への下り階段
                    if (Player.Location == new Point(basePosX + moveLen * 23, basePosY + moveLen * 0) && eventNum == 2)
                    {
                        return true;
                    }

                    // ボス２との対戦
                    if (Player.Location == new Point(basePosX + moveLen * 22, basePosY + moveLen * 1) && !we.CompleteSlayBoss2 && eventNum == 3)
                    {
                        return true;
                    }

                    // 宝箱２－１
                    if (Player.Location == new Point(basePosX + moveLen * 1, basePosY + moveLen * 18) && !we.Treasure4 && eventNum == 4)
                    {
                        return true;
                    }

                    // 宝箱２－２
                    if (Player.Location == new Point(basePosX + moveLen * 3, basePosY + moveLen * 1) && !we.Treasure5 && eventNum == 5)
                    {
                        return true;
                    }

                    // 宝箱２－３
                    if (Player.Location == new Point(basePosX + moveLen * 14, basePosY + moveLen * 0) && !we.Treasure6 && eventNum == 6)
                    {
                        return true;
                    }

                    // 宝箱２－４
                    if (Player.Location == new Point(basePosX + moveLen * 2, basePosY + moveLen * 16) && !we.Treasure7 && eventNum == 7)
                    {
                        return true;
                    }

                    // 必須イベント2-1
                    if (Player.Location == new Point(basePosX + moveLen * 25, basePosY + moveLen * 14) && eventNum == 8)
                    {
                        return true;
                    }

                    // 必須イベント2-2-1
                    if (Player.Location == new Point(basePosX + moveLen * 16, basePosY + moveLen * 14) && eventNum == 9)
                    {
                        return true;
                    }

                    // 必須イベント2-2-2
                    if (Player.Location == new Point(basePosX + moveLen * 10, basePosY + moveLen * 12) && eventNum == 10)
                    {
                        return true;
                    }

                    // 必須イベント2-2-3
                    if (Player.Location == new Point(basePosX + moveLen * 10, basePosY + moveLen * 16) && eventNum == 11)
                    {
                        return true;
                    }

                    // 必須イベント2-3
                    if (Player.Location == new Point(basePosX + moveLen * 13, basePosY + moveLen * 9) && eventNum == 12)
                    {
                        return true;
                    }

                    // 必須イベント2-4
                    if (Player.Location == new Point(basePosX + moveLen * 7, basePosY + moveLen * 7) && eventNum == 13 && !we.SolveArea24) // [警告]：イレギュラーケース、混乱の元になる場合は再検討してください。
                    {
                        return true;
                    }

                    // 必須イベント2-4-1
                    if (Player.Location == new Point(basePosX + moveLen * 5, basePosY + moveLen * 5) && eventNum == 14)
                    {
                        return true;
                    }

                    // 必須イベント2-4-1-2
                    if (Player.Location == new Point(basePosX + moveLen * 7, basePosY + moveLen * 5) && eventNum == 15)
                    {
                        return true;
                    }

                    // 必須イベント2-4-1-3
                    if (Player.Location == new Point(basePosX + moveLen * 7, basePosY + moveLen * 7) && eventNum == 16)
                    {
                        return true;
                    }

                    // 必須イベント2-4-2
                    if (Player.Location == new Point(basePosX + moveLen * 7, basePosY + moveLen * 9) && eventNum == 17)
                    {
                        return true;
                    }

                    // 必須イベント2-4-2-2
                    if (Player.Location == new Point(basePosX + moveLen * 5, basePosY + moveLen * 9) && eventNum == 18)
                    {
                        return true;
                    }

                    // 必須イベント2-4-3
                    if (Player.Location == new Point(basePosX + moveLen * 5, basePosY + moveLen * 7) && eventNum == 19)
                    {
                        return true;
                    }

                    // 必須イベント2-4-3-2
                    if (Player.Location == new Point(basePosX + moveLen * 3, basePosY + moveLen * 7) && eventNum == 20)
                    {
                        return true;
                    }

                    // 必須イベント2-4-4
                    if (Player.Location == new Point(basePosX + moveLen * 3, basePosY + moveLen * 5) && eventNum == 21)
                    {
                        return true;
                    }

                    // 必須イベント2-4-4-2
                    if (Player.Location == new Point(basePosX + moveLen * 1, basePosY + moveLen * 5) && eventNum == 22)
                    {
                        return true;
                    }

                    // 必須イベント2-4-5
                    if (Player.Location == new Point(basePosX + moveLen * 1, basePosY + moveLen * 7) && eventNum == 23)
                    {
                        return true;
                    }

                    // 必須イベント2-4-5-2
                    if (Player.Location == new Point(basePosX + moveLen * 1, basePosY + moveLen * 9) && eventNum == 24)
                    {
                        return true;
                    }

                    // 必須イベント2-4-6
                    if (Player.Location == new Point(basePosX + moveLen * 3, basePosY + moveLen * 9) && eventNum == 25)
                    {
                        return true;
                    }

                    // 必須イベント2-4-0
                    if (Player.Location == new Point(basePosX + moveLen * 4, basePosY + moveLen * 9) && eventNum == 26)
                    {
                        return true;
                    }

                    // 必須イベント2-5
                    if (Player.Location == new Point(basePosX + moveLen * 4, basePosY + moveLen * 12) && eventNum == 27)
                    {
                        return true;
                    }

                    // 必須イベント2-6
                    if (Player.Location == new Point(basePosX + moveLen * 19, basePosY + moveLen * 7) && eventNum == 28)
                    {
                        return true;
                    }

                    // 必須イベント2-6-1
                    if (Player.Location == new Point(basePosX + moveLen * 22, basePosY + moveLen * 5) && eventNum == 29)
                    {
                        return true;
                    }

                    // 必須イベント2-6-2
                    if (Player.Location == new Point(basePosX + moveLen * 25, basePosY + moveLen * 7) && eventNum == 30)
                    {
                        return true;
                    }

                    // 必須イベント2-6-3
                    if (Player.Location == new Point(basePosX + moveLen * 22, basePosY + moveLen * 9) && eventNum == 31)
                    {
                        return true;
                    }

                    // 必須イベント2-6-4
                    if (Player.Location == new Point(basePosX + moveLen * 19, basePosY + moveLen * 5) && eventNum == 32)
                    {
                        return true;
                    }

                    // 必須イベント2-6-5
                    if (Player.Location == new Point(basePosX + moveLen * 25, basePosY + moveLen * 5) && eventNum == 33)
                    {
                        return true;
                    }

                    // 必須イベント2-6-6
                    if (Player.Location == new Point(basePosX + moveLen * 25, basePosY + moveLen * 9) && eventNum == 34)
                    {
                        return true;
                    }

                    // 必須イベント2-6-7
                    if (Player.Location == new Point(basePosX + moveLen * 19, basePosY + moveLen * 9) && eventNum == 35)
                    {
                        return true;
                    }

                    // 必須イベント2-6-8
                    if (Player.Location == new Point(basePosX + moveLen * 20, basePosY + moveLen * 6) && eventNum == 36)
                    {
                        return true;
                    }

                    // 必須イベント2-6-9
                    if (Player.Location == new Point(basePosX + moveLen * 24, basePosY + moveLen * 6) && eventNum == 37)
                    {
                        return true;
                    }

                    // 必須イベント2-6-10
                    if (Player.Location == new Point(basePosX + moveLen * 24, basePosY + moveLen * 8) && eventNum == 38)
                    {
                        return true;
                    }

                    // 必須イベント2-6-11
                    if (Player.Location == new Point(basePosX + moveLen * 20, basePosY + moveLen * 8) && eventNum == 39)
                    {
                        return true;
                    }

                    // 必須イベント2-6-12
                    if (Player.Location == new Point(basePosX + moveLen * 22, basePosY + moveLen * 6) && eventNum == 40)
                    {
                        return true;
                    }

                    // 必須イベント2-6-13
                    if (Player.Location == new Point(basePosX + moveLen * 23, basePosY + moveLen * 7) && eventNum == 41)
                    {
                        return true;
                    }

                    // 必須イベント2-6-14
                    if (Player.Location == new Point(basePosX + moveLen * 22, basePosY + moveLen * 8) && eventNum == 42)
                    {
                        return true;
                    }

                    // 必須イベント2-6-15
                    if (Player.Location == new Point(basePosX + moveLen * 21, basePosY + moveLen * 7) && eventNum == 43)
                    {
                        return true;
                    }

                    // 必須イベント2-6-16
                    if (Player.Location == new Point(basePosX + moveLen * 22, basePosY + moveLen * 7) && eventNum == 44)
                    {
                        return true;
                    }

                    // 隠し通路発見時
                    if (Player.Location == new Point(basePosX + moveLen * 6, basePosY + moveLen * 17) && !we.InfoArea27 && eventNum == 45)
                    {
                        return true;
                    }

                    // スペシャル情報２
                    if (Player.Location == new Point(basePosX + moveLen * 27, basePosY + moveLen * 17) && !we.SpecialInfo2 && eventNum == 46)
                    {
                        return true;
                    }

                    break;
                #endregion
                #region "ダンジョン３階　発生位置"
                case 3:
                    // ２階への上り階段
                    if (Player.Location == new Point(basePosX + moveLen * 23, basePosY + moveLen * 0) && eventNum == 0)
                    {
                        return true;
                    }

                    // 真実の言葉３
                    if (Player.Location == new Point(basePosX + moveLen * 14, basePosY + moveLen * 11) && eventNum == 1)
                    {
                        return true;
                    }

                    // ４階への下り階段
                    if (Player.Location == new Point(basePosX + moveLen * 6, basePosY + moveLen * 19) && eventNum == 2)
                    {
                        return true;
                    }

                    // ボス３との対戦
                    if (Player.Location == new Point(basePosX + moveLen * 3, basePosY + moveLen * 15) && !we.CompleteSlayBoss3 && eventNum == 3)
                    {
                        return true;
                    }

                    // 宝箱３－１
                    if (Player.Location == new Point(basePosX + moveLen * 2, basePosY + moveLen * 9) && !we.Treasure8 && eventNum == 4)
                    {
                        return true;
                    }

                    // 宝箱３－２
                    if (Player.Location == new Point(basePosX + moveLen * 28, basePosY + moveLen * 9) && !we.Treasure9 && eventNum == 5)
                    {
                        return true;
                    }

                    // 宝箱３－３
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 14) && !we.Treasure10 && eventNum == 6)
                    {
                        return true;
                    }

                    // 宝箱３－４
                    if (Player.Location == new Point(basePosX + moveLen * 15, basePosY + moveLen * 13) && !we.Treasure11 && eventNum == 7)
                    {
                        return true;
                    }

                    // 宝箱３－５
                    if (Player.Location == new Point(basePosX + moveLen * 1, basePosY + moveLen * 3) && !we.Treasure12 && eventNum == 8)
                    {
                        return true;
                    }

                    // 看板情報１
                    if (Player.Location == new Point(basePosX + moveLen * 23, basePosY + moveLen * 1) && eventNum == 9)
                    {
                        return true;
                    }

                    // ワープ開始１１
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 1) && eventNum == 10)
                    {
                        return true;
                    }

                    // ワープ終了１１
                    if (Player.Location == new Point(basePosX + moveLen * 5, basePosY + moveLen * 13) && eventNum == 11)
                    {
                        return true;
                    }

                    // ワープ開始１２
                    if (Player.Location == new Point(basePosX + moveLen * 24, basePosY + moveLen * 4) && eventNum == 12)
                    {
                        return true;
                    }

                    // ワープ終了１２
                    if (Player.Location == new Point(basePosX + moveLen * 14, basePosY + moveLen * 15) && eventNum == 13)
                    {
                        return true;
                    }

                    // ワープ開始１３
                    if (Player.Location == new Point(basePosX + moveLen * 21, basePosY + moveLen * 1) && eventNum == 14)
                    {
                        return true;
                    }

                    // ワープ終了１３
                    if (Player.Location == new Point(basePosX + moveLen * 8, basePosY + moveLen * 1) && eventNum == 15)
                    {
                        return true;
                    }

                    // ワープ開始２４
                    if (Player.Location == new Point(basePosX + moveLen * 23, basePosY + moveLen * 14) && eventNum == 16)
                    {
                        return true;
                    }

                    // ワープ開始２５
                    if (Player.Location == new Point(basePosX + moveLen * 17, basePosY + moveLen * 8) && eventNum == 17)
                    {
                        return true;
                    }

                    // ワープ開始２６
                    if (Player.Location == new Point(basePosX + moveLen * 17, basePosY + moveLen * 4) && eventNum == 18)
                    {
                        return true;
                    }

                    // ワープ開始２７
                    if (Player.Location == new Point(basePosX + moveLen * 14, basePosY + moveLen * 4) && eventNum == 19)
                    {
                        return true;
                    }

                    // ワープ開始２８
                    if (Player.Location == new Point(basePosX + moveLen * 14, basePosY + moveLen * 8) && eventNum == 20)
                    {
                        return true;
                    }

                    // ワープ開始２９
                    if (Player.Location == new Point(basePosX + moveLen * 26, basePosY + moveLen * 8) && eventNum == 21)
                    {
                        return true;
                    }

                    // ワープ開始２１０
                    if (Player.Location == new Point(basePosX + moveLen * 22, basePosY + moveLen * 10) && eventNum == 22)
                    {
                        return true;
                    }

                    // ワープ開始２１１
                    if (Player.Location == new Point(basePosX + moveLen * 27, basePosY + moveLen * 15) && eventNum == 23)
                    {
                        return true;
                    }

                    // ワープ開始２１２
                    if (Player.Location == new Point(basePosX + moveLen * 8, basePosY + moveLen * 11) && eventNum == 24)
                    {
                        return true;
                    }

                    // ワープ開始２１３
                    if (Player.Location == new Point(basePosX + moveLen * 9, basePosY + moveLen * 13) && eventNum == 25)
                    {
                        return true;
                    }

                    // ワープ開始２１４
                    if (Player.Location == new Point(basePosX + moveLen * 7, basePosY + moveLen * 16) && eventNum == 26)
                    {
                        return true;
                    }

                    // ワープ終了２４
                    if (Player.Location == new Point(basePosX + moveLen * 12, basePosY + moveLen * 18) && eventNum == 27)
                    {
                        return true;
                    }

                    // ワープ終了（失敗）１
                    if (Player.Location == new Point(basePosX + moveLen * 24, basePosY + moveLen * 18) && eventNum == 28)
                    {
                        return true;
                    }

                    // ワープ終了（失敗）２
                    if (Player.Location == new Point(basePosX + moveLen * 12, basePosY + moveLen * 9) && eventNum == 29)
                    {
                        return true;
                    }

                    // ワープ終了（失敗）３
                    if (Player.Location == new Point(basePosX + moveLen * 7, basePosY + moveLen * 5) && eventNum == 30)
                    {
                        return true;
                    }

                    // ワープ終了（失敗）４１
                    if (Player.Location == new Point(basePosX + moveLen * 17, basePosY + moveLen * 2) && eventNum == 31)
                    {
                        return true;
                    }

                    // ワープ終了（失敗）４２
                    if (Player.Location == new Point(basePosX + moveLen * 20, basePosY + moveLen * 10) && eventNum == 32)
                    {
                        return true;
                    }

                    // ワープ開始８２
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 19) && eventNum == 33)
                    {
                        return true;
                    }

                    // ワープ終了（失敗）４３
                    if (Player.Location == new Point(basePosX + moveLen * 8, basePosY + moveLen * 18) && eventNum == 34)
                    {
                        return true;
                    }

                    // 第二関門クリア看板
                    if (Player.Location == new Point(basePosX + moveLen * 11, basePosY + moveLen * 11) && eventNum == 35)
                    {
                        return true;
                    }

                    // ワープ開始３１５
                    if (Player.Location == new Point(basePosX + moveLen * 6, basePosY + moveLen * 6) && eventNum == 36)
                    {
                        return true;
                    }

                    // ワープ開始３１６
                    if (Player.Location == new Point(basePosX + moveLen * 5, basePosY + moveLen * 5) && eventNum == 37)
                    {
                        return true;
                    }

                    // ワープ開始３１７
                    if (Player.Location == new Point(basePosX + moveLen * 5, basePosY + moveLen * 4) && eventNum == 38)
                    {
                        return true;
                    }

                    // ワープ開始３１８
                    if (Player.Location == new Point(basePosX + moveLen * 5, basePosY + moveLen * 3) && eventNum == 39)
                    {
                        return true;
                    }

                    // ワープ開始３１９
                    if (Player.Location == new Point(basePosX + moveLen * 5, basePosY + moveLen * 2) && eventNum == 40)
                    {
                        return true;
                    }

                    // ワープ開始３２０
                    if (Player.Location == new Point(basePosX + moveLen * 19, basePosY + moveLen * 2) && eventNum == 41)
                    {
                        return true;
                    }

                    // ワープ開始３２１
                    if (Player.Location == new Point(basePosX + moveLen * 20, basePosY + moveLen * 2) && eventNum == 42)
                    {
                        return true;
                    }

                    // ワープ開始３２２
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 12) && eventNum == 43)
                    {
                        return true;
                    }

                    // ワープ開始３２３
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 13) && eventNum == 44)
                    {
                        return true;
                    }

                    // ワープ開始３２４
                    if (Player.Location == new Point(basePosX + moveLen * 26, basePosY + moveLen * 4) && eventNum == 45)
                    {
                        return true;
                    }

                    // ワープ開始３２５
                    if (Player.Location == new Point(basePosX + moveLen * 27, basePosY + moveLen * 4) && eventNum == 46)
                    {
                        return true;
                    }

                    // ワープ開始３２６
                    if (Player.Location == new Point(basePosX + moveLen * 23, basePosY + moveLen * 2) && eventNum == 47)
                    {
                        return true;
                    }

                    // ワープ開始３２７
                    if (Player.Location == new Point(basePosX + moveLen * 22, basePosY + moveLen * 6) && eventNum == 48)
                    {
                        return true;
                    }

                    // ワープ開始３２８
                    if (Player.Location == new Point(basePosX + moveLen * 1, basePosY + moveLen * 10) && eventNum == 49)
                    {
                        return true;
                    }

                    // 看板情報４
                    if (Player.Location == new Point(basePosX + moveLen * 3, basePosY + moveLen * 6) && eventNum == 50)
                    {
                        return true;
                    }

                    // ワープ開始４１
                    if (Player.Location == new Point(basePosX + moveLen * 1, basePosY + moveLen * 4) && eventNum == 51)
                    {
                        return true;
                    }

                    // ワープ終了４１
                    if (Player.Location == new Point(basePosX + moveLen * 17, basePosY + moveLen * 12) && eventNum == 52)
                    {
                        return true;
                    }
                    if (Player.Location == new Point(basePosX + moveLen * 18, basePosY + moveLen * 12) && eventNum == 52)
                    {
                        return true;
                    }
                    if (Player.Location == new Point(basePosX + moveLen * 19, basePosY + moveLen * 12) && eventNum == 52)
                    {
                        return true;
                    }
                    if (Player.Location == new Point(basePosX + moveLen * 20, basePosY + moveLen * 12) && eventNum == 52)
                    {
                        return true;
                    }
                    if (Player.Location == new Point(basePosX + moveLen * 21, basePosY + moveLen * 12) && eventNum == 52)
                    {
                        return true;
                    }
                    if (Player.Location == new Point(basePosX + moveLen * 16, basePosY + moveLen * 13) && eventNum == 52)
                    {
                        return true;
                    }
                    if (Player.Location == new Point(basePosX + moveLen * 16, basePosY + moveLen * 14) && eventNum == 52)
                    {
                        return true;
                    }
                    if (Player.Location == new Point(basePosX + moveLen * 16, basePosY + moveLen * 15) && eventNum == 52)
                    {
                        return true;
                    }
                    if (Player.Location == new Point(basePosX + moveLen * 16, basePosY + moveLen * 16) && eventNum == 52)
                    {
                        return true;
                    }
                    if (Player.Location == new Point(basePosX + moveLen * 16, basePosY + moveLen * 17) && eventNum == 52)
                    {
                        return true;
                    }
                    if (Player.Location == new Point(basePosX + moveLen * 22, basePosY + moveLen * 13) && eventNum == 52)
                    {
                        return true;
                    }
                    if (Player.Location == new Point(basePosX + moveLen * 22, basePosY + moveLen * 14) && eventNum == 52)
                    {
                        return true;
                    }
                    if (Player.Location == new Point(basePosX + moveLen * 22, basePosY + moveLen * 15) && eventNum == 52)
                    {
                        return true;
                    }
                    if (Player.Location == new Point(basePosX + moveLen * 22, basePosY + moveLen * 16) && eventNum == 52)
                    {
                        return true;
                    }
                    if (Player.Location == new Point(basePosX + moveLen * 22, basePosY + moveLen * 17) && eventNum == 52)
                    {
                        return true;
                    }
                    if (Player.Location == new Point(basePosX + moveLen * 17, basePosY + moveLen * 18) && eventNum == 52)
                    {
                        return true;
                    }
                    if (Player.Location == new Point(basePosX + moveLen * 18, basePosY + moveLen * 18) && eventNum == 52)
                    {
                        return true;
                    }
                    if (Player.Location == new Point(basePosX + moveLen * 19, basePosY + moveLen * 18) && eventNum == 52)
                    {
                        return true;
                    }
                    if (Player.Location == new Point(basePosX + moveLen * 20, basePosY + moveLen * 18) && eventNum == 52)
                    {
                        return true;
                    }
                    if (Player.Location == new Point(basePosX + moveLen * 21, basePosY + moveLen * 18) && eventNum == 52)
                    {
                        return true;
                    }
                    if (Player.Location == new Point(basePosX + moveLen * 18, basePosY + moveLen * 14) && eventNum == 52)
                    {
                        return true;
                    }
                    if (Player.Location == new Point(basePosX + moveLen * 20, basePosY + moveLen * 14) && eventNum == 52)
                    {
                        return true;
                    }
                    if (Player.Location == new Point(basePosX + moveLen * 20, basePosY + moveLen * 16) && eventNum == 52)
                    {
                        return true;
                    }
                    if (Player.Location == new Point(basePosX + moveLen * 18, basePosY + moveLen * 16) && eventNum == 52)
                    {
                        return true;
                    }

                    // 宝箱３－６
                    if (Player.Location == new Point(basePosX + moveLen * 9, basePosY + moveLen * 18) && !we.Treasure121 && eventNum == 53)
                    {
                        return true;
                    }

                    // 宝箱３－７
                    if (Player.Location == new Point(basePosX + moveLen * 19, basePosY + moveLen * 10) && !we.Treasure122 && eventNum == 54)
                    {
                        return true;
                    }

                    // 宝箱３－８
                    if (Player.Location == new Point(basePosX + moveLen * 14, basePosY + moveLen * 2) && !we.Treasure123 && eventNum == 55)
                    {
                        return true;
                    }

                    // 隠し通路発見時
                    if (Player.Location == new Point(basePosX + moveLen * 2, basePosY + moveLen * 14) && !we.InfoArea35 && eventNum == 56)
                    {
                        return true;
                    }

                    // スペシャル情報３
                    if (Player.Location == new Point(basePosX + moveLen * 5, basePosY + moveLen * 19) && !we.SpecialInfo3 && eventNum == 57)
                    {
                        return true;
                    }

                    break;
                #endregion
                #region "ダンジョン４階　発生位置"
                case 4:
                    // ３階への上り階段
                    if (Player.Location == new Point(basePosX + moveLen * 6, basePosY + moveLen * 19) && eventNum == 0)
                    {
                        return true;
                    }

                    // 真実の言葉４
                    if (Player.Location == new Point(basePosX + moveLen * 22, basePosY + moveLen * 0) && eventNum == 1)
                    {
                        return true;
                    }

                    // ５階への下り階段
                    if (Player.Location == new Point(basePosX + moveLen * 20, basePosY + moveLen * 0) && eventNum == 2)
                    {
                        return true;
                    }

                    // ボス４との対戦
                    if (Player.Location == new Point(basePosX + moveLen * 26, basePosY + moveLen * 0) && !we.CompleteSlayBoss4 && eventNum == 3)
                    {
                        return true;
                    }

                    // 会話１
                    if (Player.Location == new Point(basePosX + moveLen * 2, basePosY + moveLen * 3) && eventNum == 4)
                    {
                        return true;
                    }

                    // 会話２
                    if (Player.Location == new Point(basePosX + moveLen * 12, basePosY + moveLen * 16) && eventNum == 5)
                    {
                        return true;
                    }

                    // 会話３
                    if (Player.Location == new Point(basePosX + moveLen * 4, basePosY + moveLen * 13) && eventNum == 6)
                    {
                        return true;
                    }

                    // 会話４
                    if (Player.Location == new Point(basePosX + moveLen * 10, basePosY + moveLen * 13) && eventNum == 7)
                    {
                        return true;
                    }

                    // 会話５
                    if (Player.Location == new Point(basePosX + moveLen * 11, basePosY + moveLen * 15) && eventNum == 8)
                    {
                        return true;
                    }

                    // 会話６
                    if (Player.Location == new Point(basePosX + moveLen * 13, basePosY + moveLen * 19) && eventNum == 9)
                    {
                        return true;
                    }

                    // 会話７
                    if (Player.Location == new Point(basePosX + moveLen * 16, basePosY + moveLen * 18) && !we.InfoArea48 && eventNum == 10)
                    {
                        return true;
                    }

                    // 会話８
                    if (Player.Location == new Point(basePosX + moveLen * 16, basePosY + moveLen * 1) && eventNum == 11)
                    {
                        return true;
                    }

                    // 会話９
                    if (Player.Location == new Point(basePosX + moveLen * 16, basePosY + moveLen * 0) && eventNum == 12)
                    {
                        return true;
                    }

                    // 宝箱４－１
                    if (Player.Location == new Point(basePosX + moveLen * 7, basePosY + moveLen * 10) && !we.Treasure41 && eventNum == 13)
                    {
                        return true;
                    }

                    // 宝箱４－２
                    if (Player.Location == new Point(basePosX + moveLen * 1, basePosY + moveLen * 17) && !we.Treasure42 && eventNum == 14)
                    {
                        return true;
                    }

                    // 宝箱４－３
                    if (Player.Location == new Point(basePosX + moveLen * 9, basePosY + moveLen * 1) && !we.Treasure43 && eventNum == 15)
                    {
                        return true;
                    }

                    // 宝箱４－４
                    if (Player.Location == new Point(basePosX + moveLen * 20, basePosY + moveLen * 6) && !we.Treasure44 && eventNum == 16)
                    {
                        return true;
                    }

                    // 宝箱４－５
                    if (Player.Location == new Point(basePosX + moveLen * 21, basePosY + moveLen * 13) && !we.Treasure45 && eventNum == 17)
                    {
                        return true;
                    }

                    // 宝箱４－６
                    if (Player.Location == new Point(basePosX + moveLen * 26, basePosY + moveLen * 19) && !we.Treasure46 && eventNum == 18)
                    {
                        return true;
                    }

                    // 宝箱４－７
                    if (Player.Location == new Point(basePosX + moveLen * 26, basePosY + moveLen * 5) && !we.Treasure47 && eventNum == 19)
                    {
                        return true;
                    }

                    // 宝箱４－８
                    if (Player.Location == new Point(basePosX + moveLen * 26, basePosY + moveLen * 1) && !we.Treasure48 && eventNum == 20)
                    {
                        return true;
                    }

                    // 近道１
                    if (Player.Location == new Point(basePosX + moveLen * 6, basePosY + moveLen * 18) && !we.InfoArea412 && eventNum == 21) 
                    {
                        return true;
                    }

                    // 会話１０
                    if (Player.Location == new Point(basePosX + moveLen * 18, basePosY + moveLen * 9) && eventNum == 22)
                    {
                        return true;
                    }

                    // 会話１１
                    if (Player.Location == new Point(basePosX + moveLen * 22, basePosY + moveLen * 14) && eventNum == 23)
                    {
                        return true;
                    }

                    // 会話１２
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 19) && eventNum == 24)
                    {
                        return true;
                    }

                    // 会話１３
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 3) && eventNum == 25)
                    {
                        return true;
                    }

                    // 近道２
                    if (Player.Location == new Point(basePosX + moveLen * 16, basePosY + moveLen * 19) && !we.InfoArea418 && eventNum == 26)
                    {
                        return true;
                    }

                    // 会話１４
                    if (Player.Location == new Point(basePosX + moveLen * 27, basePosY + moveLen * 0) && eventNum == 27)
                    {
                        return true;
                    }

                    // 宝箱４－９
                    if (Player.Location == new Point(basePosX + moveLen * 27, basePosY + moveLen * 10) && !we.Treasure49 && eventNum == 28)
                    {
                        return true;
                    }

                    // 近道失敗選択の前地点１
                    if (Player.Location == new Point(basePosX + moveLen * 6, basePosY + moveLen * 18) && eventNum == 29)
                    {
                        return true;
                    }

                    // 近道選択しない成功地点１
                    if (Player.Location == new Point(basePosX + moveLen * 5, basePosY + moveLen * 18) && eventNum == 30)
                    {
                        return true;
                    }

                    // 近道失敗選択後の判定地点１
                    if (Player.Location == new Point(basePosX + moveLen * 7, basePosY + moveLen * 18) && eventNum == 31)
                    {
                        return true;
                    }

                    // 近道失敗選択の前地点２
                    if (Player.Location == new Point(basePosX + moveLen * 16, basePosY + moveLen * 19) && eventNum == 32)
                    {
                        return true;
                    }

                    // 近道選択しない成功地点２
                    if (Player.Location == new Point(basePosX + moveLen * 16, basePosY + moveLen * 18) && eventNum == 33)
                    {
                        return true;
                    }

                    // 近道失敗選択後の判定地点２
                    if (Player.Location == new Point(basePosX + moveLen * 17, basePosY + moveLen * 19) && eventNum == 34)
                    {
                        return true;
                    }
                    break;
#endregion
                #region "ダンジョン５階　発生位置
                case 5:
                    // ４階への上り階段
                    if (Player.Location == new Point(basePosX + moveLen * 20, basePosY + moveLen * 0) && eventNum == 0)
                    {
                        return true;
                    }

                    // 真実の言葉５
                    if (Player.Location == new Point(basePosX + moveLen * 20, basePosY + moveLen * 9) && eventNum == 1)
                    {
                        return true;
                    }

                    // ６階への下り階段
                    //if (Player.Location == new Point(basePosX + moveLen * 20, basePosY + moveLen * 0) && eventNum == 2)
                    //{
                    //    return true;
                    //}

                    // 会話イベント５－１
                    if ( (Player.Location == new Point(basePosX + moveLen * 5, basePosY + moveLen * 9) && eventNum == 2) ||
                         (Player.Location == new Point(basePosX + moveLen * 5, basePosY + moveLen * 10) && eventNum == 2) )
                    {
                        return true;
                    }

                    // ボス５との対戦
                    if ( (Player.Location == new Point(basePosX + moveLen * 4, basePosY + moveLen * 9) && !we.CompleteSlayBoss5 && eventNum == 3) ||
                         (Player.Location == new Point(basePosX + moveLen * 4, basePosY + moveLen * 10) && !we.CompleteSlayBoss5 && eventNum == 3) )
                    {
                        return true;
                    }

                    // 宝箱５－１
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 10) && !we.Treasure51 && eventNum == 4)
                    {
                        return true;
                    }

                    // ラナのトゥルーイベント到達への破片１
                    if (Player.Location == new Point(basePosX + moveLen * 0, basePosY + moveLen * 9) &&  eventNum == 5)
                    {
                        return true;
                    }

                    // 隠し通路発見時
                    if (Player.Location == new Point(basePosX + moveLen * 19, basePosY + moveLen * 7) && eventNum == 6)
                    {
                        return true;
                    }

                    // 宝箱５－２
                    if (Player.Location == new Point(basePosX + moveLen * 14, basePosY + moveLen * 5) && !we.Treasure52 && eventNum == 7)
                    {
                        return true;
                    }

                    // 宝箱５－３
                    if (Player.Location == new Point(basePosX + moveLen * 19, basePosY + moveLen * 9) && !we.Treasure53 && eventNum == 8)
                    {
                        return true;
                    }

                    // 宝箱５－４
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 9) && !we.Treasure54 && eventNum == 9)
                    {
                        return true;
                    }

                    // 宝箱５－５
                    if (Player.Location == new Point(basePosX + moveLen * 14, basePosY + moveLen * 12) && !we.Treasure55 && eventNum == 10)
                    {
                        return true;
                    }

                    // 宝箱５－６
                    if (Player.Location == new Point(basePosX + moveLen * 16, basePosY + moveLen * 14) && !we.Treasure56 && eventNum == 11)
                    {
                        return true;
                    }

                    // 宝箱５－７
                    if (Player.Location == new Point(basePosX + moveLen * 20, basePosY + moveLen * 19) && !we.Treasure57 && eventNum == 12)
                    {
                        return true;
                    }
                    break;
                #endregion

            }
            return false;
        }

        private int GetTileNumber(Point pos)
        {
            int number = ((pos.X - basePosX) / moveLen) % Database.DUNGEON_COLUMN + ((pos.Y - basePosY) / moveLen) * Database.DUNGEON_COLUMN;
            return number;
            //int posX = pos.X - 1;
            //int posY = pos.Y - 1;
            //for (int ii = 0; ii < Database.DUNGEON_ROW * Database.DUNGEON_COLUMN; ii++)
            //{
            //    if (posX == dungeonTile[ii].Location.X && posY == dungeonTile[ii].Location.Y)
            //    {
            //        return ii;
            //    }
            //}
            //return -1;
        }

        private bool CheckWall(int direction) // 0:↑ 1:← 2:→ 3:↓
        {
            string[] targetTileInfo = null;
            if (we.DungeonArea == 1)
            {
                targetTileInfo = tileInfo;
            }
            else if (we.DungeonArea == 2)
            {
                targetTileInfo = tileInfo2;
            }
            else if (we.DungeonArea == 3)
            {
                targetTileInfo = tileInfo3;
            }
            else if (we.DungeonArea == 4)
            {
                targetTileInfo = tileInfo4;
            }
            else if (we.DungeonArea == 5)
            {
                targetTileInfo = tileInfo5;
            }

            // プレイヤーの位置に対応しているタイル情報を取得する。
            // タイル情報にある壁情報を取得して
            // 壁情報とプレイヤー動作方向に対して壁情報が一致する場合
            switch (targetTileInfo[GetTileNumber(Player.Location)])
            {
                case "Tile1.bmp":
                    break;
                case "Tile1-WallT.bmp":
                    if (direction == 0)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallT-Num1.bmp":
                    if (direction == 0)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallT-Num4.bmp":
                    if (direction == 0)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallL.bmp":
                    if (direction == 1)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallL-Num5.bmp":
                    if (direction == 1)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallR.bmp":
                    if (direction == 2)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallB.bmp":
                    if (direction == 3)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallB-Num6.bmp":
                    if (direction == 3)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallLR-DummyL.bmp":
                    if (direction == 2)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallTL.bmp":
                    if (direction == 0 || direction == 1)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallTR.bmp":
                    if (direction == 0 || direction == 2)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallTB.bmp":
                    if (direction == 0 || direction == 3)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallLR.bmp":
                    if (direction == 1 || direction == 2)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallLB.bmp":
                    if (direction == 1 || direction == 3)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallRB.bmp":
                    if (direction == 2 || direction == 3)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallRB-Num2.bmp":
                    if (direction == 2 || direction == 3)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallTLR.bmp":
                    if (direction == 0 || direction == 1 || direction == 2)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallTLB.bmp":
                    if (direction == 0 || direction == 1 || direction == 3)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallTRB.bmp":
                    if (direction == 0 || direction == 2 || direction == 3)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallLRB.bmp":
                    if (direction == 1 || direction == 2 || direction == 3)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallTLRB.bmp":
                    if (direction == 0 || direction == 1 || direction == 2 || direction == 3)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Upstair-WallLRB.bmp":
                    if (direction == 1 || direction == 2 || direction == 3)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Upstair-WallRB.bmp":
                    if (direction == 2 || direction == 3)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Upstair-WallTLR.bmp":
                    if (direction == 0 || direction == 1 || direction == 2)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Downstair-WallTRB.bmp":
                    if (direction == 0 || direction == 2 || direction == 3)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Downstair-WallT.bmp":
                    if (direction == 0)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Downstair-WallLRB.bmp":
                    if (direction == 1 || direction == 2 || direction == 3)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Downstair-WallTLB.bmp":
                    if (direction == 0 || direction == 1 || direction == 3)
                    {
                        mainMessage.Text = "アイン：いてぇ！";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
            }
            mainMessage.Text = "";
            return false;
        }


        private void EncountEnemy()
        {
            Random rd = new Random(DateTime.Now.Millisecond * Environment.TickCount);
            int resultValue = rd.Next(1, 101);
            if (we.CompleteSlayBoss5) resultValue = 100;

            if (labelVigilance.Text == Database.TEXT_VIGILANCE_MODE)
            {
                stepCounter += 1;
            }
            else
            {
                stepCounter += 3;
            }
            int encountBorder = 0;
            if (labelVigilance.Text == Database.TEXT_VIGILANCE_MODE)
            {
                if (we.DungeonArea == 4)
                {
                    encountBorder = Database.ENCOUNT_ENEMY + (int)(stepCounter / 20);
                }
                else
                {
                    encountBorder = Database.ENCOUNT_ENEMY + (int)(stepCounter / 10);
                }
            }
            else

            {
                encountBorder = Database.ENCOUNT_ENEMY + (int)(stepCounter / 5);
            }

            if (resultValue <= encountBorder)
            {
                stepCounter = 0;
                string enemyName = "";
                Random rd2 = new Random(DateTime.Now.Millisecond);
                int randomValue = rd2.Next(1, 13);
                int areaBorderX = 13;
                int areaBorderY = 10;
                string[] monsterName = new string[16];
                // １階は左上：エリア１、左下：エリア２、右上：エリア３、右下：エリア４
                if (we.DungeonArea == 1)
                {
                    // 左上
                    monsterName[0] = "ヤング・ゴブリン";
                    monsterName[1] = "薄汚れた盗賊";
                    monsterName[2] = "ひ弱なビートル";
                    monsterName[3] = "幼いエルフ";
                    // 左下
                    monsterName[4] = "落ちぶれた騎士";
                    monsterName[5] = "小さなイノシシ";
                    monsterName[6] = "睨む岩石";
                    monsterName[7] = "ブルースライム";
                    // 右上
                    monsterName[8] = "ウェアウルフ";
                    monsterName[9] = "シャドウハンター";
                    monsterName[10] = "俊敏な鷹";
                    monsterName[11] = "卑屈なオーク";
                    // 右下
                    monsterName[12] = "ブラックナイト";
                    monsterName[13] = "ホワイトナイト";
                    monsterName[14] = "番狼";
                    monsterName[15] = "着こなしの良いエルフ";

                    areaBorderX = 15;
                    areaBorderY = 13;
                }
                // ２階は右下：エリア１、左下：エリア２、左上：エリア３、右上：エリア４、
                else if (we.DungeonArea == 2)
                {
                    // 左上
                    monsterName[0] = "サバンナ・ライオン";
                    monsterName[1] = "獰猛なハゲタカ";
                    monsterName[2] = "ゴブリン・チーフ";
                    monsterName[3] = "荒れ狂ったドワーフ";
                    // 左下
                    monsterName[4] = "オールドツリー";
                    monsterName[5] = "小さなオーガ";
                    monsterName[6] = "エルヴィッシュ・シャーマン";
                    monsterName[7] = "正装をした神官";
                    // 右上
                    monsterName[8] = "異形の信奉者";
                    monsterName[9] = "マンイーター";
                    monsterName[10] = "ヴァンパイア";
                    monsterName[11] = "赤いフードをかぶった人間";
                    // 右下
                    monsterName[12] = "狂戦士バーサーカー";
                    monsterName[13] = "青隼";
                    monsterName[14] = "黒ビートル";
                    monsterName[15] = "悪意を向ける人間";

                    areaBorderX = 15;
                    areaBorderY = 10;
                }
                // ３階は右上：エリア１、右下：エリア２、左上：エリア３、左下：エリア４、
                else if (we.DungeonArea == 3)
                {
                    // 左上
                    monsterName[0] = "デビルメージ";
                    monsterName[1] = "悪魔崇拝者";
                    monsterName[2] = "アプレンティス・ロード";
                    monsterName[3] = "エルヴィッシュ神官";
                    // 左下
                    monsterName[4] = "聖騎士";
                    monsterName[5] = "フォールンシーカー";
                    monsterName[6] = "アイオブザドラゴン";
                    monsterName[7] = "生まれたての悪魔";
                    // 右上
                    monsterName[8] = "イビルメージ";
                    monsterName[9] = "ダークシーフ";
                    monsterName[10] = "アークドルイド";
                    monsterName[11] = "シャドウソーサラー";
                    // 右下
                    monsterName[12] = "忍者";
                    monsterName[13] = "エグゼキュージョナー";
                    monsterName[14] = "パワー";
                    monsterName[15] = "ブラックアイ";
                }
                // ４階は左下：エリア１、左上：エリア２、右下：エリア３、右上：エリア４
                else if (we.DungeonArea == 4)
                {
                    // 左上
                    monsterName[0] = "ブルータルオーガ";
                    monsterName[1] = "マスターロード";
                    monsterName[2] = "シン・ザ・ダークエルフ";
                    monsterName[3] = "ウィンドブレイカー";
                    // 左下
                    monsterName[4] = "ゴルゴン";
                    monsterName[5] = "ビーストマスター";
                    monsterName[6] = "ヒュージスパイダー";
                    monsterName[7] = "エルダーアサシン";
                    // 右上
                    monsterName[8] = "ペインエンジェル";
                    monsterName[9] = "ドゥームブリンガー";
                    monsterName[10] = "ハウリングホラー";
                    monsterName[11] = "カオス・ワーデン";
                    // 右下
                    monsterName[12] = "アークデーモン";
                    monsterName[13] = "サン・ストライダー";
                    monsterName[14] = "天秤を司る者";
                    monsterName[15] = "レイジ・イフリート";

                    areaBorderX = 16;
                    areaBorderY = 2;
                }
                // ５階はエリア分けせず、どこも同じとする。
                else if (we.DungeonArea == 5)
                {
                    // 左上
                    monsterName[0] = "Phoenix";
                    monsterName[1] = "Emerard Dragon";
                    monsterName[2] = "Nine Tail";
                    monsterName[3] = "Judgement";
                    // 左下
                    monsterName[4] = "Phoenix";
                    monsterName[5] = "Emerard Dragon";
                    monsterName[6] = "Nine Tail";
                    monsterName[7] = "Judgement";
                    // 右上
                    monsterName[8] = "Phoenix";
                    monsterName[9] = "Emerard Dragon";
                    monsterName[10] = "Nine Tail";
                    monsterName[11] = "Judgement";
                    // 右下
                    monsterName[12] = "Phoenix";
                    monsterName[13] = "Emerard Dragon";
                    monsterName[14] = "Nine Tail";
                    monsterName[15] = "Judgement";
                }

                if (Player.Location.X <= basePosX + moveLen * areaBorderX && Player.Location.Y <= basePosY + moveLen * areaBorderY) // 左上
                {
                    switch (randomValue)
                    {
                        case 1:
                        case 2:
                        case 8:
                            enemyName = monsterName[0];
                            break;
                        case 4:
                        case 9:
                        case 12:
                            enemyName = monsterName[1];
                            break;
                        case 5:
                        case 10:
                        case 7:
                            enemyName = monsterName[2];
                            break;
                        case 3:
                        case 6:
                        case 11:
                            enemyName = monsterName[3];
                            break;
                    }
                }
                else if (Player.Location.X <= basePosX + moveLen * areaBorderX && Player.Location.Y > basePosY + moveLen * areaBorderY) // 左下
                {
                    switch (randomValue)
                    {
                        case 1:
                        case 2:
                        case 8:
                            enemyName = monsterName[4];
                            break;
                        case 4:
                        case 9:
                        case 12:
                            enemyName = monsterName[5];
                            break;
                        case 5:
                        case 10:
                        case 7:
                            enemyName = monsterName[6];
                            break;
                        case 3:
                        case 6:
                        case 11:
                            enemyName = monsterName[7];
                            break;
                    }
                }
                else if (Player.Location.X > basePosX + moveLen * areaBorderX && Player.Location.Y <= basePosY + moveLen * areaBorderY) // 右上
                {
                    switch (randomValue)
                    {
                        case 1:
                        case 2:
                        case 8:
                            enemyName = monsterName[8];
                            break;
                        case 4:
                        case 9:
                        case 12:
                            enemyName = monsterName[9];
                            break;
                        case 5:
                        case 10:
                        case 7:
                            enemyName = monsterName[10];
                            break;
                        case 3:
                        case 6:
                        case 11:
                            enemyName = monsterName[11];
                            break;
                    }
                }
                else if (Player.Location.X > basePosX + moveLen * areaBorderX && Player.Location.Y > basePosY + moveLen * areaBorderY) // 右下
                {
                    switch (randomValue)
                    {
                        case 1:
                        case 2:
                        case 8:
                            enemyName = monsterName[12];
                            break;
                        case 4:
                        case 9:
                        case 12:
                            enemyName = monsterName[13];
                            break;
                        case 5:
                        case 10:
                        case 7:
                            enemyName = monsterName[14];
                            break;
                        case 3:
                        case 6:
                        case 11:
                            enemyName = monsterName[15];
                            break;
                    }

                }
                this.Update();
                mainMessage.Text = "アイン：敵と遭遇だ！";
                mainMessage.Update();
                EncountBattle(enemyName);
                mainMessage.Text = "";
            }
        }

        // [警告]：HomeTown.csにコピペしました。改版時はHomeTown.csもお忘れなく。
        private bool EncountBattle(string enemyName)
        {
            GroundOne.StopDungeonMusic();

            bool endFlag = false;
            while (!endFlag)
            {
                System.Threading.Thread.Sleep(1000);
                using (BattleEnemy be = new BattleEnemy())
                {
                    MainCharacter tempMC = new MainCharacter();
                    MainCharacter tempSC = new MainCharacter();
                    MainCharacter tempTC = new MainCharacter();
                    WorldEnvironment tempWE = new WorldEnvironment();

                    tempMC.MainArmor = this.MC.MainArmor;
                    tempMC.MainWeapon = this.MC.MainWeapon;
                    tempMC.Accessory = this.MC.Accessory;
                    tempSC.MainArmor = this.SC.MainArmor;
                    tempSC.MainWeapon = this.SC.MainWeapon;
                    tempSC.Accessory = this.SC.Accessory;
                    tempTC.MainArmor = this.TC.MainArmor;
                    tempTC.MainWeapon = this.TC.MainWeapon;
                    tempTC.Accessory = this.TC.Accessory;

                    ItemBackPack[] tempBackPack = new ItemBackPack[this.MC.GetBackPackInfo().Length];
                    tempBackPack = mc.GetBackPackInfo();
                    be.MC = tempMC;
                    for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
                    {
                        if (tempBackPack[ii] != null)
                        {
                            be.MC.AddBackPack(tempBackPack[ii]);
                        }
                    }

                    if (we.AvailableSecondCharacter)
                    {
                        ItemBackPack[] tempBackPack2 = new ItemBackPack[this.SC.GetBackPackInfo().Length];
                        tempBackPack2 = sc.GetBackPackInfo();
                        be.SC = tempSC;
                        for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
                        {
                            if (tempBackPack2[ii] != null)
                            {
                                be.SC.AddBackPack(tempBackPack2[ii]);
                            }
                        }
                    }
                    else
                    {
                        be.SC = null;
                    }

                    if (we.AvailableThirdCharacter)
                    {
                        ItemBackPack[] tempBackPack3 = new ItemBackPack[this.TC.GetBackPackInfo().Length];
                        tempBackPack3 = tc.GetBackPackInfo();
                        be.TC = tempTC;
                        for (int ii = 0; ii < Database.MAX_BACKPACK_SIZE; ii++)
                        {
                            if (tempBackPack3[ii] != null)
                            {
                                be.TC.AddBackPack(tempBackPack3[ii]);
                            }
                        }
                    }
                    else
                    {
                        be.TC = null;
                    }

                    Type type = tempMC.GetType();
                    foreach (PropertyInfo pi in type.GetProperties())
                    {
                        // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                        if (pi.PropertyType == typeof(System.Int32))
                        {
                            try
                            {
                                pi.SetValue(tempMC, (System.Int32)(type.GetProperty(pi.Name).GetValue(this.MC, null)), null);
                                pi.SetValue(tempSC, (System.Int32)(type.GetProperty(pi.Name).GetValue(this.SC, null)), null);
                                pi.SetValue(tempTC, (System.Int32)(type.GetProperty(pi.Name).GetValue(this.TC, null)), null);
                            }
                            catch { }
                        }
                        else if (pi.PropertyType == typeof(System.String))
                        {
                            try
                            {
                                pi.SetValue(tempMC, (string)(type.GetProperty(pi.Name).GetValue(this.MC, null)), null);
                                pi.SetValue(tempSC, (string)(type.GetProperty(pi.Name).GetValue(this.SC, null)), null);
                                pi.SetValue(tempTC, (string)(type.GetProperty(pi.Name).GetValue(this.TC, null)), null);
                            }
                            catch { }
                        }
                        else if (pi.PropertyType == typeof(System.Boolean))
                        {
                            try
                            {
                                pi.SetValue(tempMC, (System.Boolean)(type.GetProperty(pi.Name).GetValue(this.MC, null)), null);
                                pi.SetValue(tempSC, (System.Boolean)(type.GetProperty(pi.Name).GetValue(this.SC, null)), null);
                                pi.SetValue(tempTC, (System.Boolean)(type.GetProperty(pi.Name).GetValue(this.TC, null)), null);
                            }
                            catch { }
                        }
                    }


                    Type type2 = tempWE.GetType();
                    foreach (PropertyInfo pi in type2.GetProperties())
                    {
                        // [警告]：catch構文はSetプロパティがない場合だが、それ以外のケースも見えなくなってしまうので要分析方法検討。
                        if (pi.PropertyType == typeof(System.Int32))
                        {
                            try
                            {
                                pi.SetValue(tempWE, (System.Int32)(type2.GetProperty(pi.Name).GetValue(this.WE, null)), null);
                            }
                            catch { }
                        }
                        else if (pi.PropertyType == typeof(System.String))
                        {
                            try
                            {
                                pi.SetValue(tempWE, (string)(type2.GetProperty(pi.Name).GetValue(this.WE, null)), null);
                            }
                            catch { }
                        }
                        else if (pi.PropertyType == typeof(System.Boolean))
                        {
                            try
                            {
                                pi.SetValue(tempWE, (System.Boolean)(type2.GetProperty(pi.Name).GetValue(this.WE, null)), null);
                            }
                            catch { }
                        }
                    }

                    EnemyCharacter1 ec1 = new EnemyCharacter1(enemyName, this.difficulty);
                    be.EC1 = ec1;
                    be.WE = tempWE;
                    be.StartPosition = FormStartPosition.CenterParent;
                    be.BattleSpeed = this.battleSpeed;
                    be.Difficulty = this.difficulty;
                    be.IgnoreApplicationDoEvent = true;
                    be.ShowDialog();
                    if (be.DialogResult == DialogResult.Retry)
                    {
                        // 死亡時、再挑戦する場合、はじめから呼びなおす。
                        this.Update();
                        continue;
                    }
                    if (be.DialogResult == DialogResult.Abort)
                    {
                        // 逃げた時、経験値とゴールドは入らない。
                        this.MC = tempMC;
                        this.MC.ReplaceBackPack(tempMC.GetBackPackInfo());
                        if (we.AvailableSecondCharacter)
                        {
                            this.SC = tempSC;
                            this.SC.ReplaceBackPack(tempSC.GetBackPackInfo());
                        }
                        if (we.AvailableThirdCharacter)
                        {
                            this.TC = tempTC;
                            this.TC.ReplaceBackPack(tempTC.GetBackPackInfo());
                        }
                        this.WE = tempWE;

                        GroundOne.PlayDungeonMusic(Database.BGM02, Database.BGM02LoopBegin);
                        SetupPlayerStatus();
                        return false;
                    }
                    else if (be.DialogResult == DialogResult.Ignore)
                    {
                        using (YesNoReqWithMessage yerw = new YesNoReqWithMessage())
                        {
                            if (ec1.Name == "一階の守護者：絡みつくフランシス")
                            {
                                UpdatePlayerLocationInfo(this.Player.Location.X, this.Player.Location.Y - Database.DUNGEON_MOVE_LEN);
                            }
                            if (ec1.Name == "二階の守護者：Lizenos")
                            {
                                UpdatePlayerLocationInfo(this.Player.Location.X, this.Player.Location.Y + Database.DUNGEON_MOVE_LEN);
                            }
                            if (ec1.Name == "三階の守護者：Minflore")
                            {
                                UpdatePlayerLocationInfo(this.Player.Location.X, this.Player.Location.Y - Database.DUNGEON_MOVE_LEN);
                            }
                            if (ec1.Name == "四階の守護者：Altomo")
                            {
                                UpdatePlayerLocationInfo(this.Player.Location.X + Database.DUNGEON_MOVE_LEN, this.Player.Location.Y);
                            }
                            if (ec1.Name == "五階の守護者：Bystander")
                            {
                                UpdatePlayerLocationInfo(this.Player.Location.X + Database.DUNGEON_MOVE_LEN, this.Player.Location.Y);
                            }
                            yerw.StartPosition = FormStartPosition.CenterParent;
                            yerw.MainMessage = "タイトルへ戻ります。今までのデータをセーブしますか？";
                            yerw.ShowDialog();
                            if (yerw.DialogResult == DialogResult.Yes)
                            {
                                using (ESCMenu esc = new ESCMenu())
                                {
                                    esc.MC = this.MC;
                                    esc.SC = this.SC;
                                    esc.TC = this.TC;
                                    esc.WE = this.WE;
                                    esc.KnownTileInfo = this.knownTileInfo;
                                    esc.KnownTileInfo2 = this.knownTileInfo2;
                                    esc.KnownTileInfo3 = this.knownTileInfo3;
                                    esc.KnownTileInfo4 = this.knownTileInfo4;
                                    esc.KnownTileInfo5 = this.knownTileInfo5;
                                    esc.StartPosition = FormStartPosition.CenterParent;
                                    esc.OnlySave = true;
                                    esc.ShowDialog();
                                }
                            }
                        }
                        endFlag = true;
                        this.Visible = false;
                    }
                    else
                    {
                        bool alreadyPlayBackMusic = false;
                        if (we.AvailableFirstCharacter)
                        {
                            this.MC = tempMC;
                            this.MC.ReplaceBackPack(tempMC.GetBackPackInfo());
                            if (mc.Level < 40) // [警告]：前編はＬＶ４０をＭＡＸとする。
                            {
                                mc.Exp += be.EC1.Exp;
                            }
                            mc.Gold += be.EC1.Gold;

                            int levelUpPoint = 0;
                            int cumultiveLvUpValue = 0;
                            while (true)
                            {
                                if (mc.Exp >= mc.NextLevelBorder && mc.Level < 40)
                                {
                                    levelUpPoint += mc.LevelUpPoint;
                                    // [警告]：レベルアップのMAXライフが常に２０、マナが１５で良いかどうか検討してください。
                                    mc.BaseLife += 20;
                                    mc.BaseMana += 15;
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
                                GroundOne.PlaySoundEffect("LvUp.mp3");
                                if (!alreadyPlayBackMusic)
                                {
                                    alreadyPlayBackMusic = true;
                                    GroundOne.PlayDungeonMusic(Database.BGM02, Database.BGM02LoopBegin);
                                }
                                mainMessage.Text = "アイン：レベルアップだぜ！！";
                                using (StatusPlayer sp = new StatusPlayer())
                                {
                                    sp.WE = we;
                                    sp.MC = mc;
                                    sp.SC = sc;
                                    sp.TC = tc;
                                    sp.CurrentStatusView = Color.LightSkyBlue;
                                    sp.LevelUp = true;
                                    sp.UpPoint = levelUpPoint;
                                    sp.CumultiveLvUpValue = cumultiveLvUpValue;
                                    sp.StartPosition = FormStartPosition.CenterParent;
                                    sp.ShowDialog();
                                }
                            }
                        }

                        if (we.AvailableSecondCharacter)
                        {
                            this.SC = tempSC;
                            this.SC.ReplaceBackPack(tempSC.GetBackPackInfo());
                            if (sc.Level < 40) // [警告]：前編はＬＶ４０をＭＡＸとする。
                            {
                                sc.Exp += be.EC1.Exp;
                            }
                            //SC.Gold += be.EC1.Gold; // [警告]：ゴールドの所持は別クラスにするべきです。

                            int levelUpPoint = 0;
                            int cumultiveLvUpValue = 0;
                            while (true)
                            {
                                if (sc.Exp >= sc.NextLevelBorder && sc.Level < 40)
                                {
                                    levelUpPoint += sc.LevelUpPoint;
                                    // [警告]：レベルアップのMAXライフが常に２０、マナが１５で良いかどうか検討してください。
                                    sc.BaseLife += 20;
                                    sc.BaseMana += 15;
                                    sc.Exp = sc.Exp - sc.NextLevelBorder;
                                    sc.Level += 1;
                                    cumultiveLvUpValue++;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            if (cumultiveLvUpValue > 0)
                            {
                                GroundOne.PlaySoundEffect("LvUp.mp3");
                                if (!alreadyPlayBackMusic)
                                {
                                    alreadyPlayBackMusic = true;
                                    GroundOne.PlayDungeonMusic(Database.BGM02, Database.BGM02LoopBegin);
                                }
                                mainMessage.Text = "ラナ：来たわ、レベルアップ♪";
                                using (StatusPlayer sp = new StatusPlayer())
                                {
                                    sp.WE = we;
                                    sp.MC = mc;
                                    sp.SC = sc;
                                    sp.TC = tc;
                                    sp.CurrentStatusView = Color.Pink;
                                    sp.LevelUp = true;
                                    sp.UpPoint = levelUpPoint;
                                    sp.CumultiveLvUpValue = cumultiveLvUpValue;
                                    sp.StartPosition = FormStartPosition.CenterParent;
                                    sp.ShowDialog();
                                }
                            }
                        }

                        if (we.AvailableThirdCharacter)
                        {
                            this.TC = tempTC;
                            this.TC.ReplaceBackPack(tempTC.GetBackPackInfo());
                            if (tc.Level < 40) // [警告]：前編はＬＶ４０をＭＡＸとする。
                            {
                                tc.Exp += be.EC1.Exp;
                            }
                            //TC.Gold += be.EC1.Gold; // [警告]：ゴールドの所持は別クラスにするべきです。

                            int levelUpPoint = 0;
                            int cumultiveLvUpValue = 0;
                            while (true)
                            {
                                if (tc.Exp >= tc.NextLevelBorder && tc.Level < 40)
                                {
                                    levelUpPoint += tc.LevelUpPoint;
                                    // [警告]：レベルアップのMAXライフが常に２０、マナが１５で良いかどうか検討してください。
                                    tc.BaseLife += 20;
                                    tc.BaseMana += 15;
                                    tc.Exp = tc.Exp - tc.NextLevelBorder;
                                    tc.Level += 1;
                                    cumultiveLvUpValue++;
                                }
                                else
                                {
                                    break;
                                }
                            }

                            if (cumultiveLvUpValue > 0)
                            {
                                GroundOne.PlaySoundEffect("LvUp.mp3");
                                if (!alreadyPlayBackMusic)
                                {
                                    alreadyPlayBackMusic = true;
                                    GroundOne.PlayDungeonMusic(Database.BGM02, Database.BGM02LoopBegin);
                                }
                                mainMessage.Text = "ヴェルゼ：レベルアップですね。";
                                using (StatusPlayer sp = new StatusPlayer())
                                {
                                    sp.WE = we;
                                    sp.MC = mc;
                                    sp.SC = sc;
                                    sp.TC = tc;
                                    sp.CurrentStatusView = Color.Silver;
                                    sp.LevelUp = true;
                                    sp.UpPoint = levelUpPoint;
                                    sp.CumultiveLvUpValue = cumultiveLvUpValue;
                                    sp.StartPosition = FormStartPosition.CenterParent;
                                    sp.ShowDialog();
                                }
                            }
                        }
                        this.WE = tempWE;

                        if (!alreadyPlayBackMusic && ec1.Name != "五階の守護者：Bystander")
                        {
                            alreadyPlayBackMusic = true;
                            GroundOne.PlayDungeonMusic(Database.BGM02, Database.BGM02LoopBegin);
                        }
                        SetupPlayerStatus();
                        return true;
                    }
                }
            }

            this.DialogResult = DialogResult.Cancel;
            return false;
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            keyDown = false;
        }

        private bool UpdateUnknownTile()
        {
            bool newUpdate = false; // 新しくタイルが拓けた事を示すフラグ
            int currentPosNum = GetTileNumber(this.Player.Location);
            string currentTileInfo = "";
            bool[] targetKnownTileInfo = null;
            if (we.DungeonArea == 1)
            {
                currentTileInfo = tileInfo[currentPosNum];
                targetKnownTileInfo = knownTileInfo;
            }
            else if (we.DungeonArea == 2)
            {
                currentTileInfo = tileInfo2[currentPosNum];
                targetKnownTileInfo = knownTileInfo2;
            }
            else if (we.DungeonArea == 3)
            {
                currentTileInfo = tileInfo3[currentPosNum];
                targetKnownTileInfo = knownTileInfo3;
            }
            else if (we.DungeonArea == 4)
            {
                currentTileInfo = tileInfo4[currentPosNum];
                targetKnownTileInfo = knownTileInfo4;
            }
            else if (we.DungeonArea == 5)
            {
                currentTileInfo = tileInfo5[currentPosNum];
                targetKnownTileInfo = knownTileInfo5;
            }

            if (unknownTile[currentPosNum].Visible)
            {
                newUpdate = true;
            }
            unknownTile[currentPosNum].Visible = false;
            targetKnownTileInfo[currentPosNum] = true;

            // 上の可視化
            if (currentPosNum >= Database.DUNGEON_COLUMN &&
                (currentTileInfo != "Tile1-WallT.bmp" &&
                 currentTileInfo != "Tile1-WallT-Num1.bmp" &&
                 currentTileInfo != "Tile1-WallT-Num4.bmp" &&
                 currentTileInfo != "Tile1-WallTL.bmp" &&
                 currentTileInfo != "Tile1-WallTR.bmp" &&
                 currentTileInfo != "Tile1-WallTB.bmp" &&
                 currentTileInfo != "Tile1-WallTLR.bmp" &&
                 currentTileInfo != "Tile1-WallTLB.bmp" &&
                 currentTileInfo != "Tile1-WallTRB.bmp" &&
                 currentTileInfo != "Tile1-WallTLRB.bmp" &&
                 currentTileInfo != "Upstair-WallTLR.bmp" &&
                 currentTileInfo != "Downstair-WallTRB.bmp" &&
                 currentTileInfo != "Downstair-WallTLB.bmp" &&
                 currentTileInfo != "Downstair-WallT.bmp"))
            {
                if (unknownTile[currentPosNum - Database.DUNGEON_COLUMN].Visible)
                {
                    newUpdate = true;
                }
                unknownTile[currentPosNum - Database.DUNGEON_COLUMN].Visible = false;
                targetKnownTileInfo[currentPosNum - Database.DUNGEON_COLUMN] = true;
            }

            // 左の可視化
            if (currentPosNum % Database.DUNGEON_COLUMN != 0 &&
                (currentTileInfo != "Tile1-WallL.bmp" &&
                 currentTileInfo != "Tile1-WallL-Num5.bmp" &&
                 currentTileInfo != "Tile1-WallTL.bmp" &&
                 currentTileInfo != "Tile1-WallLR.bmp" &&
                 currentTileInfo != "Tile1-WallLB.bmp" &&
                 currentTileInfo != "Tile1-WallTLR.bmp" &&
                 currentTileInfo != "Tile1-WallTLB.bmp" &&
                 currentTileInfo != "Tile1-WallLRB.bmp" &&
                 currentTileInfo != "Tile1-WallTLRB.bmp" &&
                 currentTileInfo != "Upstair-WallLRB.bmp" &&
                 currentTileInfo != "Upstair-WallTLR.bmp" &&
                 currentTileInfo != "Downstair-WallTLB.bmp" &&
                 currentTileInfo != "Downstair-WallLRB.bmp" &&
                 currentTileInfo != "Tile1-WallLR-DummyL.bmp"))
            {
                if (unknownTile[currentPosNum - 1].Visible)
                {
                    newUpdate = true;
                }
                unknownTile[currentPosNum - 1].Visible = false;
                targetKnownTileInfo[currentPosNum - 1] = true;
            }

            // 右の可視化
            if (currentPosNum % Database.DUNGEON_COLUMN != (Database.DUNGEON_COLUMN - 1) &&
                (currentTileInfo != "Tile1-WallR.bmp" &&
                 currentTileInfo != "Tile1-WallTR.bmp" &&
                 currentTileInfo != "Tile1-WallLR.bmp" &&
                 currentTileInfo != "Tile1-WallRB.bmp" &&
                 currentTileInfo != "Tile1-WallRB-Num2.bmp" &&
                 currentTileInfo != "Tile1-WallTLR.bmp" &&
                 currentTileInfo != "Tile1-WallTRB.bmp" &&
                 currentTileInfo != "Tile1-WallLRB.bmp" &&
                 currentTileInfo != "Tile1-WallTLRB.bmp" &&
                 currentTileInfo != "Upstair-WallLRB.bmp" &&
                 currentTileInfo != "Upstair-WallRB.bmp" &&
                 currentTileInfo != "Upstair-WallTLR.bmp" &&
                 currentTileInfo != "Downstair-WallTRB.bmp" &&
                 currentTileInfo != "Downstair-WallLRB.bmp" &&
                 currentTileInfo != "Tile1-WallLR-DummyL.bmp" &&
                 currentTileInfo != "Tile1-WallR-DummyR.bmp"))
            {
                if (unknownTile[currentPosNum + 1].Visible)
                {
                    newUpdate = true;
                }
                unknownTile[currentPosNum + 1].Visible = false;
                targetKnownTileInfo[currentPosNum + 1] = true;
            }

            // 下の可視化
            if (currentPosNum < (Database.DUNGEON_COLUMN * (Database.DUNGEON_ROW - 1)) &&
                (currentTileInfo != "Tile1-WallB.bmp" &&
                 currentTileInfo != "Tile1-WallB-Num6.bmp" &&
                 currentTileInfo != "Tile1-WallTB.bmp" &&
                 currentTileInfo != "Tile1-WallLB.bmp" &&
                 currentTileInfo != "Tile1-WallRB.bmp" &&
                 currentTileInfo != "Tile1-WallRB-Num2.bmp" &&
                 currentTileInfo != "Tile1-WallTLB.bmp" &&
                 currentTileInfo != "Tile1-WallTRB.bmp" &&
                 currentTileInfo != "Tile1-WallLRB.bmp" &&
                 currentTileInfo != "Tile1-WallTLRB.bmp" &&
                 currentTileInfo != "Upstair-WallLRB.bmp" &&
                 currentTileInfo != "Upstair-WallRB.bmp" &&
                 currentTileInfo != "Downstair-WallTRB.bmp" &&
                 currentTileInfo != "Downstair-WallTLB.bmp" &&
                 currentTileInfo != "Downstair-WallLRB.bmp" &&
                 currentTileInfo != "Tile1-WallB-DummyB.bmp"))
            {
                if (unknownTile[currentPosNum + Database.DUNGEON_COLUMN].Visible)
                {
                    newUpdate = true;
                }
                unknownTile[currentPosNum + Database.DUNGEON_COLUMN].Visible = false;
                targetKnownTileInfo[currentPosNum + Database.DUNGEON_COLUMN] = true;
            }
            this.Update();

            return newUpdate;
        }

        private void SetupDungeonMapping(int area)
        {
            SetupDungeonMapping(area, true);
        }
        private void SetupDungeonMapping(int area, bool NeedLoadImage)
        {
            we.DungeonArea = area;
            this.dungeonAreaLabel.Text = we.DungeonArea.ToString() + "　階";
            string[] targetTileInfo = null;
            bool[] targetKnownTileInfo = null;

            switch (area)
            {
                case 1:
                    targetTileInfo = tileInfo;
                    targetKnownTileInfo = knownTileInfo;
                    break;
                case 2:
                    targetTileInfo = tileInfo2;
                    targetKnownTileInfo = knownTileInfo2;
                    if (!we.CompleteArea1)
                    {
                        we.CompleteArea1 = true;
                        we.CompleteArea1Day = we.GameDay;
                    }
                    break;
                case 3:
                    targetTileInfo = tileInfo3;
                    targetKnownTileInfo = knownTileInfo3;
                    if (!we.CompleteArea2)
                    {
                        we.CompleteArea2 = true;
                        we.CompleteArea2Day = we.GameDay;
                    }
                    break;
                case 4:
                    targetTileInfo = tileInfo4;
                    targetKnownTileInfo = knownTileInfo4;
                    if (!we.CompleteArea3)
                    {
                        we.CompleteArea3 = true;
                        we.CompleteArea3Day = we.GameDay;
                    }
                    break;
                case 5:
                    targetTileInfo = tileInfo5;
                    targetKnownTileInfo = knownTileInfo5;
                    if (!we.CompleteArea4)
                    {
                        we.CompleteArea4 = true;
                        we.CompleteArea4Day = we.GameDay;
                    }
                    break;
                default:
                    break;
            }

            if (NeedLoadImage)
            {
                for (int ii = 0; ii < Database.DUNGEON_ROW * Database.DUNGEON_COLUMN; ii++)
                {
                    this.Player.BackgroundImage = Image.FromFile(Database.BaseResourceFolder + Database.FloorFolder[we.DungeonArea - 1] + "Player.bmp");
                    dungeonTile[ii].Image = Image.FromFile(Database.BaseResourceFolder + Database.FloorFolder[we.DungeonArea - 1] + targetTileInfo[ii]);
                    unknownTile[ii].Image = Image.FromFile(Database.BaseResourceFolder + Database.FloorFolder[we.DungeonArea - 1] + "UnknownTile.bmp");
                    unknownTile[ii].Visible = !targetKnownTileInfo[ii]; // 反対ですが意味付けは同じ本質です。
                }
            }

            UpdateUnknownTile();
            dungeonField.Invalidate();
            this.Update();
        }

        private void UpdateMainMessage(string message)
        {
            UpdateMainMessage(message, false);
        }
        private void UpdateMainMessage(string message, bool ignoreOk)
        {
            mainMessage.Text = message;
            mainMessage.Update();

            if (!ignoreOk)
            {
                using (OKRequest ok = new OKRequest())
                {
                    ok.StartPosition = FormStartPosition.Manual;
                    ok.Location = new Point(this.Location.X + 540, this.Location.Y + 440);
                    ok.ShowDialog();
                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (!this.firstLoadIgnoreMusic)
            {
                //GroundOne.PlayDungeonMusic(Database.BGM02, Database.BGM02LoopBegin);
            }
        }

        private void SetupPlayerStatus()
        {
            if (we.AvailableFirstCharacter)
            {
                FirstPlayerPanel.Visible = true;
                if (mc.AvailableSkill)
                {
                    currentSkillPoint1.Visible = true;
                }
                else
                {
                    currentSkillPoint1.Visible = false;
                }

                if (mc.AvailableMana)
                {
                    currentManaPoint1.Visible = true;
                }
                else
                {
                    currentManaPoint1.Visible = false;
                }

                if (!mc.AvailableSkill && !mc.AvailableMana)
                {
                    FirstPlayerPanel.Height = 39;
                }
                else if (mc.AvailableSkill && !mc.AvailableMana)
                {
                    FirstPlayerPanel.Height = 52;
                }
                else
                {
                    FirstPlayerPanel.Height = 73;
                }

                currentLife1.Width = (int)((double)((double)mc.CurrentLife / (double)mc.MaxLife) * 100.0f);
                currentSkillPoint1.Width = (int)((double)((double)mc.CurrentSkillPoint / (double)mc.MaxSkillPoint) * 100.0f);
                currentManaPoint1.Width = (int)((double)((double)mc.CurrentMana / (double)mc.MaxMana) * 100.0f);
            }
            else
            {
                FirstPlayerPanel.Visible = false;               
            }

            if (we.AvailableSecondCharacter)
            {
                SecondPlayerPanel.Visible = true;
                if (sc.AvailableSkill)
                {
                    currentSkillPoint2.Visible = true;
                }
                else
                {
                    currentSkillPoint2.Visible = false;
                }

                if (sc.AvailableMana)
                {
                    currentManaPoint2.Visible = true;
                }
                else
                {
                    currentManaPoint2.Visible = false;
                }

                if (!sc.AvailableSkill && !sc.AvailableMana)
                {
                    SecondPlayerPanel.Height = 39;
                }
                else if (sc.AvailableSkill && !sc.AvailableMana)
                {
                    SecondPlayerPanel.Height = 52;
                }
                else
                {
                    SecondPlayerPanel.Height = 73;
                }
                currentLife2.Width = (int)((double)((double)sc.CurrentLife / (double)sc.MaxLife) * 100.0f);
                currentSkillPoint2.Width = (int)((double)((double)sc.CurrentSkillPoint / (double)sc.MaxSkillPoint) * 100.0f);
                currentManaPoint2.Width = (int)((double)((double)sc.CurrentMana / (double)sc.MaxMana) * 100.0f);
            }
            else
            {
                SecondPlayerPanel.Visible = false;
            }

            if (we.AvailableThirdCharacter)
            {
                ThirdPlayerPanel.Visible = true;
                if (tc.AvailableSkill)
                {
                    currentSkillPoint3.Visible = true;
                }
                else
                {
                    currentSkillPoint3.Visible = false;
                }

                if (tc.AvailableMana)
                {
                    currentManaPoint3.Visible = true;
                }
                else
                {
                    currentManaPoint3.Visible = false;
                }

                if (!tc.AvailableSkill && !tc.AvailableMana)
                {
                    ThirdPlayerPanel.Height = 39;
                }
                else if (sc.AvailableSkill && !sc.AvailableMana)
                {
                    ThirdPlayerPanel.Height = 52;
                }
                else
                {
                    ThirdPlayerPanel.Height = 73;
                }
                currentLife3.Width = (int)((double)((double)tc.CurrentLife / (double)tc.MaxLife) * 100.0f);
                currentSkillPoint3.Width = (int)((double)((double)tc.CurrentSkillPoint / (double)tc.MaxSkillPoint) * 100.0f);
                currentManaPoint3.Width = (int)((double)((double)tc.CurrentMana / (double)tc.MaxMana) * 100.0f);
            }
            else
            {
                ThirdPlayerPanel.Visible = false;
            }
        }

        private void labelVigilance_Click(object sender, EventArgs e)
        {
            if (labelVigilance.Text == Database.TEXT_VIGILANCE_MODE)
            {
                labelVigilance.BackColor = Color.Violet;
                labelVigilance.Text = Database.TEXT_FINDENEMY_MODE;
            }
            else
            {
                labelVigilance.BackColor = Color.AliceBlue;
                labelVigilance.Text = Database.TEXT_VIGILANCE_MODE;
            }
        }

        private void DungeonPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (GroundOne.sound != null) // 後編編集
            {
                GroundOne.sound.StopMusic(); // 後編編集
                //this.sound.Disactive(); // 後編削除
            }
        }
    }
}
