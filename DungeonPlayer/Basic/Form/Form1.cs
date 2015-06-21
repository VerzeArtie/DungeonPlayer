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

        MainCharacter mc = null; // ��l���F�A�C��
        MainCharacter sc = null; // �q���C���F���i
        MainCharacter tc = null; // �V���h�E�F���F���[
        WorldEnvironment we = null; // �_���W�����i�s��

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

        Thread th; // BGM�p�̃X���b�h
        bool endSign; // BGM�p�X���b�h�̏I���T�C��
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

        private int stepCounter = 0; // �G�G���J�E���g�������̒l

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
                // SaveLoad��Load�t�F�[�Y�Ɉڍs���܂����B
            }
            else
            {
                // SaveLoad��NewGame�t�F�[�Y�Ɉڍs���܂����B
            }

            #region "�ړ����P�O�̃f�[�^��ΏۂƂ���ꍇ�A�ʒu����K�؂ɓǂݍ��ޕK�v������܂�"
            // �V�����Q�[�����n�߂��ꍇ
            if (this.NewGameFlag)
            {
                this.Player.Location = new Point(we.DungeonPosX, we.DungeonPosY);
            }
            else
            {
                // �Q�[�����[�h�����ꍇ
                if (we.DungeonPosX2 == 0 && we.DungeonPosY2 == 0) // �ߋ��ɐV�������ŃZ�[�u�����f�[�^���Ȃ����߁E�E�E
                {
                    // �ߋ��̃Z�[�u������ꍇ��X���W���}�C�i�X����B
                    this.Player.Location = new Point(we.DungeonPosX - 160, we.DungeonPosY);
                }
                else
                {
                    // �ߋ��ɐV�������ŃZ�[�u�����f�[�^������ꍇ�A��������̂܂܎g���B
                    this.Player.Location = new Point(we.DungeonPosX2, we.DungeonPosY2);
                }
            }
            #endregion

            this.dayLabel.Text = we.GameDay.ToString() + "����";
            this.dungeonAreaLabel.Text = we.DungeonArea.ToString() + "�@�K";

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
                //dungeonField.Controls.Add(dungeonTile[ii]); // dungeonField��PictureBox�ňꊇ�\�����܂��B

                unknownTile[ii] = new PictureBox();
                unknownTile[ii].Size = new Size(Database.DUNGEON_MOVE_LEN, Database.DUNGEON_MOVE_LEN);
                unknownTile[ii].Location = new Point(Database.DUNGEON_BASE_X + (ii % Database.DUNGEON_COLUMN) * Database.DUNGEON_MOVE_LEN,
                                                     Database.DUNGEON_BASE_Y + (ii / Database.DUNGEON_COLUMN) * Database.DUNGEON_MOVE_LEN);
                unknownTile[ii].Visible = !knownTileInfo[ii]; // ���΂ł����Ӗ��t���͓����{���ł��B
                //dungeonField.Controls.Add(unknownTile[ii]); // dungeonField��PictureBox�ňꊇ�\�����܂��B
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
            #region "�_���W�����P�K"
            // �O�s��
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

            // �P�s��
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

            // �Q�s��
            tileInfo[60] = "Tile1-WallLR.bmp";
            tileInfo[74] = "Tile1-WallL.bmp";
            tileInfo[75] = "Tile1-WallTB.bmp";
            tileInfo[76] = "Tile1-WallTB.bmp";
            tileInfo[77] = "Tile1-WallTB.bmp";
            tileInfo[78] = "Tile1-WallTR.bmp";
            tileInfo[81] = "Tile1-WallLR.bmp";
            tileInfo[86] = "Tile1-WallLR.bmp";
            tileInfo[89] = "Tile1-WallLR.bmp";

            // �R�s��
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

            // �S�s��
            tileInfo[120] = "Tile1-WallLR.bmp";
            tileInfo[134] = "Tile1-WallLR.bmp";
            tileInfo[136] = "Tile1-WallTL.bmp";
            tileInfo[137] = "Tile1-WallTB.bmp";
            tileInfo[138] = "Tile1-WallRB.bmp";
            tileInfo[141] = "Tile1-WallLR.bmp";
            tileInfo[144] = "Tile1-WallL.bmp";
            tileInfo[149] = "Tile1-WallR.bmp";

            // �T�s��
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

            // �U�s��
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

            // �V�s��
            tileInfo[210] = "Tile1-WallL.bmp";
            tileInfo[216] = "Tile1-WallLR.bmp";
            tileInfo[224] = "Tile1-WallLR.bmp";
            tileInfo[229] = "Tile1-WallLR.bmp";
            tileInfo[231] = "Tile1-WallLR.bmp";
            tileInfo[239] = "Tile1-WallR.bmp";

            // �W�s��
            tileInfo[240] = "Tile1-WallL.bmp";
            tileInfo[246] = "Tile1-WallLR.bmp";
            tileInfo[254] = "Tile1-WallLR.bmp";
            tileInfo[259] = "Tile1-WallLR.bmp";
            tileInfo[261] = "Tile1-WallLR.bmp";
            tileInfo[269] = "Tile1-WallR.bmp";

            // �X�s��
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

            // �P�O�s��
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

            // �P�P�s��
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

            // �P�Q�s��
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

            // �P�R�s��
            tileInfo[390] = "Tile1-WallL.bmp";
            tileInfo[391] = "Tile1-WallLR.bmp";
            tileInfo[396] = "Tile1-WallLR.bmp";
            tileInfo[402] = "Tile1-WallLR.bmp";
            tileInfo[404] = "Tile1-WallLR.bmp";
            tileInfo[408] = "Tile1-WallLR.bmp";
            tileInfo[411] = "Tile1-WallL.bmp";
            tileInfo[419] = "Tile1-WallR.bmp";

            // �P�S�s��
            tileInfo[420] = "Tile1-WallTL.bmp";
            tileInfo[421] = "Tile1-WallRB.bmp";
            tileInfo[426] = "Tile1-WallLR.bmp";
            tileInfo[432] = "Tile1-WallLR.bmp";
            tileInfo[434] = "Tile1-WallLR.bmp";
            tileInfo[438] = "Tile1-WallLR.bmp";
            tileInfo[441] = "Tile1-WallL.bmp";
            tileInfo[449] = "Tile1-WallR.bmp";

            // �P�T�s��
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

            // �P�U�s��
            tileInfo[480] = "Tile1-WallLR.bmp";
            tileInfo[486] = "Tile1-WallLR.bmp";
            tileInfo[488] = "Tile1-WallL.bmp";
            tileInfo[491] = "Tile1-WallTB.bmp";
            tileInfo[492] = "Tile1-WallRB.bmp";
            tileInfo[494] = "Tile1-WallLR.bmp";
            tileInfo[498] = "Tile1-WallLR.bmp";
            tileInfo[501] = "Tile1-WallL.bmp";
            tileInfo[509] = "Tile1-WallR.bmp";

            // �P�V�s��
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

            // �P�W�s��
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

            // �P�X�s��
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
            #region "�_���W�����Q�K"
            // �O�s��
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

            // �P�s��
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

            // �Q�s��
            tileInfo2[60] = "Tile1-WallL.bmp";
            tileInfo2[63] = "Tile1-WallLR.bmp";
            tileInfo2[66] = "Tile1-WallLR.bmp";
            tileInfo2[72] = "Tile1-WallLR.bmp";
            tileInfo2[76] = "Tile1-WallLR.bmp";
            tileInfo2[82] = "Tile1-WallLR.bmp";
            tileInfo2[86] = "Tile1-WallLR.bmp";
            tileInfo2[89] = "Tile1-WallR.bmp";

            // �R�s��
            tileInfo2[90] = "Tile1-WallL.bmp";
            tileInfo2[93] = "Tile1-WallLR.bmp";
            tileInfo2[96] = "Tile1-WallLR.bmp";
            tileInfo2[102] = "Tile1-WallLR.bmp";
            tileInfo2[106] = "Tile1-WallLR.bmp";
            tileInfo2[112] = "Tile1-WallLR.bmp";
            tileInfo2[116] = "Tile1-WallLR.bmp";
            tileInfo2[119] = "Tile1-WallR.bmp";

            // �S�s��
            tileInfo2[120] = "Tile1-WallL.bmp";
            tileInfo2[123] = "Tile1-WallLR.bmp";
            tileInfo2[126] = "Tile1-WallLR.bmp";
            tileInfo2[132] = "Tile1-WallLR.bmp";
            tileInfo2[136] = "Tile1-WallLR.bmp";
            tileInfo2[142] = "Tile1-WallLR.bmp";
            tileInfo2[146] = "Tile1-WallLR.bmp";
            tileInfo2[149] = "Tile1-WallR.bmp";

            // �T�s��
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

            // �U�s��
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

            // �V�s��
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

            // �W�s��
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

            // �X�s��
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

            // �P�O�s��
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

            // �P�P�s��
            tileInfo2[330] = "Tile1-WallL.bmp";
            tileInfo2[334] = "Tile1-WallLR.bmp";
            tileInfo2[343] = "Tile1-WallLR.bmp";
            tileInfo2[352] = "Tile1-WallLR.bmp";
            tileInfo2[359] = "Tile1-WallR.bmp";

            // �P�Q�s��
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

            // �P�R�s��
            tileInfo2[390] = "Tile1-WallL.bmp";
            tileInfo2[391] = "Tile1-WallL.bmp";
            tileInfo2[397] = "Tile1-WallR.bmp";
            tileInfo2[400] = "Tile1-WallL.bmp";
            tileInfo2[406] = "Tile1-WallR.bmp";
            tileInfo2[409] = "Tile1-WallL.bmp";
            tileInfo2[415] = "Tile1-WallR.bmp";
            tileInfo2[419] = "Tile1-WallR.bmp";

            // �P�S�s��
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

            // �P�T�s��
            tileInfo2[450] = "Tile1-WallL.bmp";
            tileInfo2[451] = "Tile1-WallL.bmp";
            tileInfo2[457] = "Tile1-WallR.bmp";
            tileInfo2[460] = "Tile1-WallL.bmp";
            tileInfo2[466] = "Tile1-WallR.bmp";
            tileInfo2[469] = "Tile1-WallL.bmp";
            tileInfo2[475] = "Tile1-WallR.bmp";
            tileInfo2[479] = "Tile1-WallLR.bmp";

            // �P�U�s��
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

            // �P�V�s��
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

            // �P�W�s��
            tileInfo2[540] = "Tile1-WallL.bmp";
            tileInfo2[541] = "Tile1-WallTLR.bmp";
            tileInfo2[569] = "Tile1-WallLR.bmp";

            // �P�X�s��
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
            #region "�_���W�����R�K"
            // �O�s��
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

            // �P�s��
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

            // �Q�s��
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

            // �R�s��
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

            // �S�s��
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

            // �T�s��
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

            // �U�s��
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

            // �V�s��
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

            // �W�s��
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

            // �X�s��
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

            // �P�O�s��
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

            // �P�P�s��
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

            // �P�Q�s��
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

            // �P�R�s��
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

            // �P�S�s��
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

            // �P�T�s��
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

            // �P�U�s��
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

            // �P�V�s��
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

            // �P�W�s��
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

            // �P�X�s��
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
            #region "�_���W�����S�K"
            // �O�s��
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
            // �P�s��
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
            // �Q�s��
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
            // �R�s��
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
            // �S�s��
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
            // �T�s��
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
            // �U�s��
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
            // �V�s��
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
            // �W�s��
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
            // �X�s��
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
            // �P�O�s��
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
            // �P�P�s��
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
            // �P�Q�s��
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
            // �P�R�s��
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
            // �P�S�s��
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
            // �P�T�s��
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
            // �P�U�s��
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
            // �P�V�s��
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
            // �P�W�s��
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
            // �P�X�s��
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
            #region "�_���W�����T�K"
            // �O�s��
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
            // �P�s��
            tileInfo5[30] = "Tile1-WallL.bmp";
            tileInfo5[50] = "Tile1-WallLR.bmp";
            tileInfo5[59] = "Tile1-WallR.bmp";
            // �Q�s��
            tileInfo5[60] = "Tile1-WallL.bmp";
            tileInfo5[80] = "Tile1-WallLR.bmp";
            tileInfo5[89] = "Tile1-WallR.bmp";
            // �R�s��
            tileInfo5[90] = "Tile1-WallL.bmp";
            tileInfo5[110] = "Tile1-WallLR.bmp";
            tileInfo5[119] = "Tile1-WallR.bmp";
            // �S�s��
            tileInfo5[120] = "Tile1-WallL.bmp";
            tileInfo5[140] = "Tile1-WallLR.bmp";
            tileInfo5[149] = "Tile1-WallR.bmp";
            // �T�s��
            tileInfo5[150] = "Tile1-WallL.bmp";
            tileInfo5[164] = "Tile1-WallTL.bmp";
            tileInfo5[165] = "Tile1-WallT.bmp";
            tileInfo5[166] = "Tile1-WallTR.bmp";
            tileInfo5[170] = "Tile1-WallLR.bmp";
            tileInfo5[179] = "Tile1-WallR.bmp";
            // �U�s��
            tileInfo5[180] = "Tile1-WallTL.bmp";
            tileInfo5[181] = "Tile1-WallT.bmp";
            tileInfo5[182] = "Tile1-WallT.bmp";
            tileInfo5[183] = "Tile1-WallT.bmp";
            tileInfo5[184] = "Tile1-WallTR.bmp";
            tileInfo5[194] = "Tile1-WallL.bmp";
            tileInfo5[196] = "Tile1-WallR.bmp";
            tileInfo5[200] = "Tile1-WallLR.bmp";
            tileInfo5[209] = "Tile1-WallR.bmp";
            // �V�s��
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
            // �W�s��
            tileInfo5[240] = "Tile1-WallL.bmp";
            tileInfo5[244] = "Tile1-WallR.bmp";
            tileInfo5[260] = "Tile1-WallLR.bmp";
            tileInfo5[269] = "Tile1-WallR.bmp";
            // �X�s��
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
            // �P�O�s��
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
            // �P�P�s��
            tileInfo5[330] = "Tile1-WallL.bmp";
            tileInfo5[334] = "Tile1-WallR.bmp";
            tileInfo5[350] = "Tile1-WallLR.bmp";
            tileInfo5[359] = "Tile1-WallR.bmp";
            // �P�Q�s��
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
            // �P�R�s��
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
            // �P�S�s��
            tileInfo5[420] = "Tile1-WallL.bmp";
            tileInfo5[434] = "Tile1-WallLB.bmp";
            tileInfo5[435] = "Tile1-WallB.bmp";
            tileInfo5[436] = "Tile1-WallRB.bmp";
            tileInfo5[440] = "Tile1-WallLR.bmp";
            tileInfo5[449] = "Tile1-WallR.bmp";
            // �P�T�s��
            tileInfo5[450] = "Tile1-WallL.bmp";
            tileInfo5[470] = "Tile1-WallLR.bmp";
            tileInfo5[479] = "Tile1-WallR.bmp";
            // �P�U�s��
            tileInfo5[480] = "Tile1-WallL.bmp";
            tileInfo5[500] = "Tile1-WallLR.bmp";
            tileInfo5[509] = "Tile1-WallR.bmp";
            // �P�V�s��
            tileInfo5[510] = "Tile1-WallL.bmp";
            tileInfo5[530] = "Tile1-WallLR.bmp";
            tileInfo5[539] = "Tile1-WallR.bmp";
            // �P�W�s��
            tileInfo5[540] = "Tile1-WallL.bmp";
            tileInfo5[560] = "Tile1-WallLR.bmp";
            tileInfo5[569] = "Tile1-WallR.bmp";
            // �P�X�s��
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
                // �z�[���^�E���ɓ���O�́A�����̐������g���Ă���ꍇ�����邽�߁A�X�^�[�g�n�_�ֈړ����Ă������Ƃ���B
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

                // s ��Ғǉ��i���݃o�O�Ή�)
                this.mc = ht.MC;
                this.sc = ht.SC;
                this.tc = ht.TC;
                this.we = ht.WE;
                // e ��Ғǉ��i���݃o�O�Ή�)

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
                    // �_���W�����ɖ߂��Ă������߁A�t���O�𗧂Ă�
                    this.WE.SaveByDungeon = true;
                    // �z�[���^�E������o�Ă�����A���̓��̃R�~���j�P�[�V�����t���O�𗎂Ƃ�
                    this.WE.AlreadyCommunicate = false;
                    this.WE.AlreadyEquipShop = false;
                    this.WE.AlreadyRest = false;
                    this.dayLabel.Text = we.GameDay.ToString() + "����";
                    this.dungeonAreaLabel.Text = we.DungeonArea.ToString() + "�@�K";

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
                // �v���C���[�̓��������
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

                // ESC���j���[��\������
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

            //keyDown = true; [�x��]�F�J���r���Ő퓬�I����A�C�x���g������ȂǂŃL�[�_�E���Ō����Ȃ��ꍇ���������B���������ςȂ����Ɛi�߂���悤�Ɏd�l�ύX�ƂȂ�̂ŁA�ʂ̕s����o���ꍇ�͂܂��Č������Ă��������B
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

                #region "�_���W�����P�K�C�x���g"
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
                                        mainMessage.Text = "�A�C���F�Ŕ�����ȁE�E�E�ȂɂȂɁH";
                                        ok.ShowDialog();
                                        mainMessage.Text = "�@�@�@�@�w�^���̌��t�P�F�@�@���B�͂܂��_���W�����̒��ɂ���x";
                                        ok.ShowDialog();
                                        mainMessage.Text = "�A�C���F�N���A����ȓ�����O�̎�����������́B";
                                        we.TruthWord1 = true;
                                    }
                                    else
                                    {
                                        UpdateMainMessage("�@�@�@�@�w�^���̌��t�P�F�@�@���B�͂܂��_���W�����̒��ɂ���x", true);
                                    }
                                    return;

                                case 1:
                                    this.Update();
                                    mainMessage.Text = "�A�C���F�{�X�Ƃ̐퓬���I�C���������߂Ă������I";
                                    mainMessage.Update();
                                    bool result = EncountBattle("��K�̎��ҁF���݂��t�����V�X");
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
                                    we.Treasure1 = GetTreasure("�u���[�}�e���A��");
                                    return;

                                case 3:
                                    we.Treasure2 = GetTreasure("�`���N���I�[�u");
                                    return;

                                case 4:
                                    we.Treasure3 = GetTreasure("�����V�g�̌아");
                                    return;

                                case 5:
                                    mainMessage.Text = "�A�C���F�����O�̒��ɖ߂邩�H";
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
                                    mainMessage.Text = "�A�C���F����K�i�����I���������~���Ƃ��邩�H";
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
                                                mainMessage.Text = "�A�C���F�����A�P�K���e�����������A��x�����O�̒��֖߂�Ƃ��邩�B";
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
                                        UpdateMainMessage("�A�C���F���̕ǁE�E�E��͂肻�����B");

                                        UpdateMainMessage("�A�C���F�F���኱�ϐF���Ă���̂͗򉻂̂�������˂��B");

                                        UpdateMainMessage("�A�C���F�R�C�c�͈Ӑ}�I�ɉB���ꂽ�ꏊ���Ď����E�E�E");

                                        UpdateMainMessage("�A�C���F������A��ɐi��ł݂邩�B�������邩���m��˂��ȁB");

                                        we.InfoArea11 = true;
                                    }
                                    return;

                                case 8:
                                    if (!we.SpecialInfo1)
                                    {
                                        UpdateMainMessage("�A�C���F�E�E�E��H�����A�����˂�����˂����E�E�E�����B");

                                        GroundOne.StopDungeonMusic();
                                        this.BackColor = Color.Black;
                                        UpdateMainMessage("�@�@�@�y���̏u�ԁA�A�C���̔]���Ɍ��������ɂ��P�����I���͂̊��o����Ⴢ���I�I�z");

                                        UpdateMainMessage("�@�@�������@�͂������S�Ăł��� ������");

                                        UpdateMainMessage("�A�C���F���ȁE�E�E�Ȃ�E�E�E���E�E�E����E�E�E�E�E�E");

                                        UpdateMainMessage("�@�@�������@�͂͗͂ɂ��炸�A�͂͑S�Ăł���@������");

                                        UpdateMainMessage("�A�C���F�����E�E�E�������E�E�E�S�Ă��āE�E�E�b�C�c�c�c�A�����E�E�E");

                                        UpdateMainMessage("�@�@�������@�������Ȃ������B�@�������S�͖������B�@������");

                                        UpdateMainMessage("�A�C���F���A�m�邩�E�E�E�񂾂�A�E�E�E�E�@�C�e�e�e�E�E�E");

                                        UpdateMainMessage("�@�@�������@�݂͂̂Ɉˑ�����ȁB�S��΂ɂ���B�@������");

                                        UpdateMainMessage("�A�C���F���E�E�E�S���ƁH�E�E�E�b�O�I�I");

                                        UpdateMainMessage("�@�@�@�y�A�C���ɑ΂��錃�������ɂ͏����������Ă������B�z");

                                        this.BackColor = Color.RoyalBlue;

                                        UpdateMainMessage("�A�C���F�E�E�E�b�O�E�E�E�������A�����Ă��E�E�E");

                                        UpdateMainMessage("�A�C���F���������񂾁A�����́B�����˂������ɁB");

                                        UpdateMainMessage("�A�C���F�u�͂͗͂ɂ��炸�v�Ƃ��E�E�E�����A�������Ă₪��B");

                                        UpdateMainMessage("�A�C���F�u�͂������S�Ă��v���t�ʂ�̈Ӗ�����B");

                                        UpdateMainMessage("�A�C���F�������E�E�E���ɐS���˂��Ƃł��v���Ă�̂��B");

                                        UpdateMainMessage("�A�C���F�E�E�E�܂��A����ȏ��łЂƂ茾�������ĂĂ����傤���˂��B");

                                        UpdateMainMessage("�A�C���F���n�͂Ȃ����ƁB���āA�߂��ă_���W������i�߂�Ƃ��邩�B");

                                        GroundOne.PlayDungeonMusic(Database.BGM02, Database.BGM02LoopBegin);
                                        we.SpecialInfo1 = true;
                                    }
                                    return;

                                default:
                                    mainMessage.Text = "�A�C���F��H���ɉ����Ȃ������Ǝv�����B";
                                    return;
                            }
                        }
                    }
                }
                #endregion
                #region "�_���W�����Q�K�C�x���g"
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
                                    mainMessage.Text = "�A�C���F�P�K�֖߂�K�i���ȁB�����͈�U�߂邩�H";
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
                                        mainMessage.Text = "�A�C���F�Ŕ�����ȁE�E�E�ȂɂȂɁH";
                                        ok.ShowDialog();
                                        mainMessage.Text = "�@�@�@�@�w�^���̌��t�Q�F�@�@�肢�����Ȃ��ꏊ�ցA�肢���I���ꏊ�ցx";
                                        ok.ShowDialog();
                                        mainMessage.Text = "�A�C���F�Ő[���֓��B�ł����{�]���ȁB";
                                        we.TruthWord2 = true;
                                    }
                                    else
                                    {
                                        UpdateMainMessage("�@�@�@�@�w�^���̌��t�Q�F�@�@�肢�����Ȃ��ꏊ�ցA�肢���I���ꏊ�ցx", true);
                                    }
                                    return;
                                case 2:
                                    mainMessage.Text = "�A�C���F�������R�K�ւ̊K�i�I�~��Ă݂悤���H";
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
                                                UpdateMainMessage("�A�C���F�����A�Q�K���e�����I���i�A�������񃆃��O�̒��֖߂邩�H");

                                                UpdateMainMessage("���i�F�����ˁA�R�K��i�݂����C�����������邯�ǁA��������߂�܂���B");

                                                UpdateMainMessage("�A�C���F������A���Ⴀ�g�����A�����̐����I");
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
                                    mainMessage.Text = "�A�C���F�{�X�Ƃ̐퓬���I�C���������߂Ă������I";
                                    mainMessage.Update();
                                    bool result = EncountBattle("��K�̎��ҁFLizenos");
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
                                    we.Treasure4 = GetTreasure("�g���킵�̃}���g");
                                    return;

                                case 5:
                                    we.Treasure5 = GetTreasure("�_����");
                                    return;

                                case 6:
                                    we.Treasure6 = GetTreasure("��̍���");
                                    return;

                                case 7:
                                    we.Treasure7 = GetTreasure("�^�J�̊Z");
                                    return;

                                case 8: // ��U�߂�
                                    #region "�Ŕ˔j�C�x���g 2-1"
                                    if (!we.SolveArea21)
                                    {
                                        if (!we.InfoArea21)
                                        {
                                            mainMessage.Text = "�A�C���F�Ŕ�����ȁE�E�E�ȂɂȂɁH";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�@�@�@�@�w���̐�A�s���~�܂�B�@��U�A�������邪�悢�B�x";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�}�W����B������������R�R���ŉ��w�����Ă̂��H";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�������Ă�̂�B�����Ǝd�|��������̂�B�܂��͒T�����܂���B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�I�[�P�[�I";
                                            we.InfoArea21 = true;
                                        }
                                        else
                                        {
                                            mainMessage.Text = "�@�@�@�@�w���̐�A�s���~�܂�B�@��U�A�������邪�悢�B�x";
                                        }
                                    }
                                    else
                                    {
                                        if (!we.CompleteArea21)
                                        {
                                            mainMessage.Text = "�@�@�@�@�w���̐�A���֐i�ނׂ��B��֐i�ނׂ��炸�B�x";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�I�H�����A���i�B���̊ŔA�������ς���Ă邼�I";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F����ς肻����B��U�߂���ĈӖ��������̂�B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�����������̂��B����A�����������A�z���g�B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�ł��`����FiveSeeker�͂P���Ő��e�����̂�ˁB�ǂ�������̂�����B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�J�[���݂̎����B��l������U�����O�̒��֖߂����񂶂�˂����H";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�Ȃ�قǁA�S���ŗ�������Ƃ͈ꌾ�������ĂȂ���ˁB�Ⴆ�Ă邶��Ȃ��A�A�C���B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F����������Ō������Ă񂾂�ȁB����ςƂ�ł��˂��l����B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�m���ɂ����ˁE�E�E���͒T��������čl�������������ˁB";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F������A���ꂪ���ʂ���B�܂��A�i�߂�񂾁I��֐i�������I�I";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F����A�Ŕ̒ʂ�A���ɐi��������";
                                            we.CompleteArea21 = true;
                                            ConstructDungeonMap();
                                            SetupDungeonMapping(2);
                                        }
                                        else
                                        {
                                            if (!we.CompleteArea26)
                                            {
                                                mainMessage.Text = "�@�@�@�@�w���̐�A���֐i�ނׂ��B��֐i�ނׂ��炸�B�x";
                                            }
                                            else
                                            {
                                                mainMessage.Text = "�Ŕ͂��������Ȃ��Ă���B";
                                            }
                                        }
                                    }
                                    return;
                                    #endregion

                                case 9: // ���Ə�
                                    #region "�Ŕ˔j�C�x���g 2-2"
                                    if (!we.SolveArea22)
                                    {
                                        if (!we.InfoArea22)
                                        {
                                            mainMessage.Text = "�A�C���F�܂��A�Ŕ����邺�E�E�E�ȂɂȂɁH";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�@�@�@�@�w���̐�A�s���~�܂�B�@�e�X�̋����������B�@�x";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F��������I�C���Ă������āI�I";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�o�J�����q�ɏ��n�߂���ˁE�E�E�܂�������ݔq����B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�I���@�I�X�g���[�g�E�X�}�b�V���I�I�I";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�@�@�@�@�w�V���b�A�h�I�I�H�H�I���I�I�I�x";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�E�E�E�E�E�E";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�����N���Ȃ���ˁB";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�܂��A�C���Ă������āE�E�E�_�u���E�X���b�V���I�I�I";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�@�@�@�@�w�h�S�H�H���I�@�o�L�B�C���I�I�x";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�E�E�E�ؐ��̊ŔȂ̂ɁA�Ȃ�ŉ��Ȃ��񂾁H";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F���������Ӗ��̋�������Ȃ������ˁB";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F���Ⴀ�A�������Č����񂾁B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�����܂ŕ�����Ȃ����B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�܂��A����グ�����B����ς�T���ƍs���˂����H";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�����ˁB����ς��x����Ă݂�Ƃ��܂����B";
                                            ok.ShowDialog();
                                            we.InfoArea22 = true;
                                        }
                                        else
                                        {
                                            mainMessage.Text = "�@�@�@�@�w���̐�A�s���~�܂�B�@�e�X�̋����������B�@�x";
                                        }
                                    }
                                    else
                                    {
                                        if (!we.CompleteArea22)
                                        {
                                            mainMessage.Text = "�@�@�@�@�w���̐�A��֐i�ނׂ��B���֐i�ނׂ��炸�B�@�x";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F��������I�ǂ����A�R�R���N���A�݂Ă����ȁB";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�Ƃ���ŁA�A�C���E�E�E";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�Ȃ񂾂�H";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F��֐i�߂��ď����Ă��邯�ǁB";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�Ȃ񂾁A���֍s���Ă݂����̂��H";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�w���ʂ�i�ނ̂��������̂��ǂ����A�Y�܂Ȃ��H";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F���ŔY�ނ񂾁H";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F���̊Ŕ����B��㩂֗��Ƃ����߂��Ă����\���͍l���Ȃ��́H";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�l���Ă邳�B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�E�\�A����ŔY�܂Ȃ��Ȃ�āA���񂽃o�J����Ȃ��́H";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F���i�A���O���؂��̂��H";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F���H�������ƂƁA���؂�郏�P����Ȃ����ǁB";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F��������ゾ�B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�E�E�E���A���ł����Ȃ�킯�H";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�E�E�E�b�n�b�n�b�n�b�n�I";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F���ȁA����ˑR�΂��o���āA�C����������ˁB";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�l���Ă����ʂ��Ď����B������������Č����Ă񂾁A��ŗǂ�����H";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F��A�܂�����������Ƃ����ˁB�b�t�t�A���܂ɂ͗ǂ����Ƃ�����ˁB";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�����A�Ƃ��Ƃƍs�����I";
                                            ok.ShowDialog();
                                            we.CompleteArea22 = true;
                                            ConstructDungeonMap();
                                            SetupDungeonMapping(2);
                                        }
                                        else
                                        {
                                            if (!we.CompleteArea26)
                                            {
                                                mainMessage.Text = "�@�@�@�@�w���̐�A��֐i�ނׂ��B���֐i�ނׂ��炸�B�@�x";
                                            }
                                            else
                                            {
                                                mainMessage.Text = "�Ŕ͂��������Ȃ��Ă���B";
                                            }
                                        }
                                    }
                                    return;
                                    #endregion

                                case 10: // ��
                                    #region "�Ŕ˔j�C�x���g 2-2��"
                                    if (!we.SolveArea221)
                                    {
                                        if (!we.InfoArea221)
                                        {
                                            mainMessage.Text = "���i�F�A�C���A������Ƃ�����ƁB����������B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F��H�ǂ�ǂ�A���������B���}�[�N�̊ŔɁA�O�`�X�̐���������ł�ȁB";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�r�V�r�V���ƁB���A�������������͂���Ă������B�W�Q�T�O�E�E�E";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F������ƁA����ɉ��ł��G��Ȃ��ł�ˁB�g���b�v��������Ȃ��̂ɁB";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F���ʂ��������̂��������牟���Ă��܂�����B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F������g���b�v�Ȃ�ł��傤���A�C�����Ă�B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�����̐�������͂���񂾂낤�ȁE�E�E";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F������Ƃ��ꂾ�����ᕪ����Ȃ���ˁB���������T���Ă݂܂���B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�܂��A�҂Ă�B�V�˂̃I���l�������ŃN���A���Ă�邺�I";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�o�J�̏����Ƃ��A������ݔq���ƍs���܂�����";
                                            ok.ShowDialog();
                                            using (RequestInput ri = new RequestInput())
                                            {
                                                ri.StartPosition = FormStartPosition.CenterParent;
                                                ri.ShowDialog();
                                                if (ri.InputData == mc.Strength.ToString())
                                                {
                                                    // [�x��]�F�����łP��ڃN���A���Ă���ꍇ�A�V�����t���O�𗧂ĂĂ��������B
                                                    mainMessage.Text = "�@�@�@�@�w�b�S�S�S�S�S�E�E�E�K�R���I�x";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F����I�I���}�[�N�������Ȃ������I�I";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F�E�\�E�E�E�E�\�ł���I�H";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F�b�n�b�n�b�n�I";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F�ւ��A��邶��Ȃ��B�債�����̂ˁB";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F���}�[�N�͂ǂ��l���Ă��r�͂���B�܂荡�̎����̗͂������Ηǂ��̂��B";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F�Ȃ�قǁA���̃A�C���̗͒l�́A" + mc.Strength.ToString() + "�ˁB";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F�������������B�����A���ɉ����Ȃ������ĉ�낤���B";
                                                    ok.ShowDialog();
                                                    we.SolveArea221 = true;
                                                    if (we.SolveArea222) we.SolveArea22 = true;
                                                }
                                                else
                                                {
                                                    mainMessage.Text = "�@�@�@�@�w�b�u�u�[�I�I�x";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F�E�E�E";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F�����A�T���T����";
                                                    ok.ShowDialog();
                                                }
                                            }
                                            we.InfoArea221 = true;
                                        }
                                        else
                                        {
                                            mainMessage.Text = "�@�@�@�@�w���}�[�N�̊Ŕ����ĂĂ���B���ɂO�`�X�̐���������ł���B�x";
                                            using (RequestInput ri = new RequestInput())
                                            {
                                                ri.StartPosition = FormStartPosition.CenterParent;
                                                ri.ShowDialog();
                                                if (ri.InputData == mc.Strength.ToString())
                                                {
                                                    mainMessage.Text = "�@�@�@�@�w�b�S�S�S�S�S�E�E�E�K�R���I�x";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F����I�I���}�[�N�������Ȃ������I�I";
                                                    ok.ShowDialog();
                                                    if (!we.FailArea221)
                                                    {
                                                        mainMessage.Text = "���i�F�ւ��A�債�����̂ˁB��̉��������̂�H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F���}�[�N�͂ǂ��l���Ă��r�͂���B�܂荡�̎����̗͂������Ηǂ��̂��B";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�Ȃ�قǁA���̃A�C���̗͒l�́A" + mc.Strength.ToString() + "�ˁB";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F�������������B�����A���ɉ����Ȃ������ĉ�낤���B";
                                                        ok.ShowDialog();
                                                    }
                                                    else
                                                    {
                                                        mainMessage.Text = "���i�F�z���A����ς�͒l�Ńr���S��������ˁ�";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F���}�[�N�͒��ڍU���͂���˂��̂���H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F���ڍU���͕͂���̍ŏ��l�ƍő�l�������āA�s��ɂȂ�ł���H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F���}�[�N�̓X�g���[�g�E�X�}�b�V������˂��̂���H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�X�g���[�g�E�X�}�b�V���͋Z�l���e�����Ă�������Ȃ��H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F���}�[�N�͌�����˂��̂���I�H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�悵�A����ȂɎ��̃L�b�N���H�炢�������Ď��ˁ�";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F���A���₢��I���C�g�j���O�E�L�b�N�͊��قȁB���������I�͒l�Ńr���S�I";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F���ۃr���S�������񂾂����B���s���܂����";
                                                        ok.ShowDialog();
                                                    }
                                                    we.SolveArea221 = true;
                                                    if (we.SolveArea222) we.SolveArea22 = true;
                                                }
                                                else
                                                {
                                                    if (we.FailArea221)
                                                    {
                                                        mainMessage.Text = "�@�@�@�@�w�b�u�u�[�I�I�x";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�E�E�E�L�b�N�Łg���Ɂh�𖡂킢�����H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F�E�E�E" + mc.Strength.ToString() + "���ȁB";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F��������������Ă�B";
                                                        ok.ShowDialog();
                                                    }
                                                    else
                                                    {
                                                        mainMessage.Text = "�@�@�@�@�w�b�u�u�[�I�I�x";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F�������B�I���@�I�X�g���[�g�E�X�}�b�V���I�I";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�@�@�@�@�w�b�u�[�I�I�@�b�u�u�[�I�I�I�x";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F�����I�ӊO�Ɣ�������̂���A�R���I";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F���}�[�N�Ȃ񂾂���A����ς�͊֌W���Ǝv��Ȃ��H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F�܂��A����ȋC�͂��Ă�񂾂��ȁB";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�A�C���A���Ȃ����̃X�e�[�^�X�����ė͒l�͂����Ȃ킯�H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F���́A" + mc.Strength.ToString() + "���ď����ȁB";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�P���ɂ��ꂶ��Ȃ��H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F�b�n�b�n�b�n�B���������v�����������A�C���Ƃ��ȁI";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�E�E�E�ق�ƃo�J�E�E�E";
                                                        ok.ShowDialog();
                                                        we.FailArea221 = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        mainMessage.Text = "�@�@�@�@�w���}�[�N�̊Ŕ͂��������Ȃ��Ă���B�@�x";
                                    }
                                    return;
                                    #endregion

                                case 11: // ��
                                    #region "�Ŕ˔j�C�x���g 2-2��"
                                    if (!we.SolveArea222)
                                    {
                                        if (!we.InfoArea222)
                                        {
                                            mainMessage.Text = "���i�F�A�C���A�����ɉ���������B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F���A���������B��}�[�N�̊Ŕ̉��ɂO�`�X�̐���������ł邺�B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�����������͂Ƃ������I�V�V�V�E�E�E";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F������ɑ��삵�Ă�̂�I�g���b�v�Ńh�K�V���A�A�@�@�@���I�I���ĂȂ�����ǂ�����́H";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F����͂��̎����B�b�n�b�n�b�n�I";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�E�E�E���ʂ��A�z���g�ɁB";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�܁A�܂����댩��A���́˂������Ȃ�����v�����āB";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�Ȃ�قǂˁA�ł������l��������Ȃ��ȏ�ǂ����悤���Ȃ��񂶂�Ȃ��H�T�����܂���B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�܂��҂āB���̃I���������Ō����ɐ����𓖂ĂĂ݂��邺�I";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�o�J���ǂ��܂ŉ�͂ł��Ă邩��������A������ݔq����";
                                            ok.ShowDialog();
                                            using (RequestInput ri = new RequestInput())
                                            {
                                                ri.StartPosition = FormStartPosition.CenterParent;
                                                ri.ShowDialog();
                                                if (ri.InputData == mc.Intelligence.ToString())
                                                {
                                                    // [�x��]�F�����łP��ڃN���A���Ă���ꍇ�A�V�����t���O�𗧂ĂĂ��������B
                                                    mainMessage.Text = "�@�@�@�@�w�b�S�S�S�S�S�E�E�E�K�R���I�x";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F��}�[�N�����ł��ƁB�I�b�P�[�I";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F���[�[�[�[�[�[�[�����I�H";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F�b�n�b�n�b�n�I�I";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F�b���E�E�E�ǂ��Ȃ��Ă�̂�B�A�C���Ɍ����Ă��肦�Ȃ���B";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F��}�[�N�ƌ�������A���͂���B�Ⴄ���H���̉��́A" + mc.Intelligence.ToString() + "���B";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F�Ȃ�قǁA�܂薂�͂̌��ƂȂ�A�C���̒m�͒l����͂����킯�ˁB";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F�������������B�����A���ɉ������邩���m��˂��B���ĉ�낤���B";
                                                    ok.ShowDialog();
                                                    we.SolveArea222 = true;
                                                    if (we.SolveArea221) we.SolveArea22 = true;
                                                }
                                                else
                                                {
                                                    mainMessage.Text = "�@�@�@�@�w�b�u�u�[�I�I�x";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F�E�E�E";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F�����A�T���T����";
                                                    ok.ShowDialog();
                                                }
                                            }
                                            we.InfoArea222 = true;
                                        }
                                        else
                                        {
                                            mainMessage.Text = "�@�@�@�@�w��}�[�N�̊Ŕ����ĂĂ���B���ɂO�`�X�̐���������ł���B�x";
                                            using (RequestInput ri = new RequestInput())
                                            {
                                                ri.StartPosition = FormStartPosition.CenterParent;
                                                ri.ShowDialog();
                                                if (ri.InputData == mc.Intelligence.ToString())
                                                {
                                                    mainMessage.Text = "�@�@�@�@�w�b�S�S�S�S�S�E�E�E�K�R���I�x";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F����I�I��}�[�N�������Ȃ������I�I";
                                                    ok.ShowDialog();
                                                    if (!we.FailArea222)
                                                    {
                                                        mainMessage.Text = "���i�F�ւ��A�債�����̂ˁB��̉��������̂�H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F��}�[�N�͂ǂ��l���Ă����͂̏؂��B�܂荡�̎����̒m�͂������Ηǂ��̂��B";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�Ȃ�قǁA���̃A�C���̒m�͂́A" + mc.Intelligence.ToString() + "�ˁB";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F�������������B�����A���ɉ����Ȃ������ĉ�낤���B";
                                                        ok.ShowDialog();
                                                    }
                                                    else
                                                    {
                                                        mainMessage.Text = "���i�F�z���A����ς�m�͂Ńr���S��������ˁ�";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F��}�[�N����Ȃ��Ĕ]�݂��}�[�N����˂����H���ʂȂ�B";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�G�������ĊԈႦ��l�����邩�炶��Ȃ��H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F��}�[�N��m�͂��ƕ�����Ȃ����c�����Ă��邾��H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F����A�A�C����������Ȃ��H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F�����E�E�E�����������ɂ��Ă����Ă�邺�B";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F���P�킩��Ȃ����ňӒn�͂�Ȃ��ŁA�z���z�����s���܂����";
                                                        ok.ShowDialog();
                                                    }
                                                    we.SolveArea222 = true;
                                                    if (we.SolveArea221) we.SolveArea22 = true;
                                                }
                                                else
                                                {
                                                    if (we.FailArea222)
                                                    {
                                                        mainMessage.Text = "�@�@�@�@�w�b�u�u�[�I�I�x";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�E�E�E�����A�_�[�N�E�u���X�g�B";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F���E�E�E�I�[�P�[�A" + mc.Intelligence.ToString() + "�������ȁB";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F��������������Ă�B";
                                                        ok.ShowDialog();
                                                    }
                                                    else
                                                    {
                                                        mainMessage.Text = "�@�@�@�@�w�b�u�u�[�I�I�x";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F�������B�I���@�I�t�@�C�A�E�{�[���I�I";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�@�@�@�@�w�b�u�u�u�[�I�I�@�ő�x���I�I�I�@�u�u�[�[�I�I�x";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F�����I�ӊO�Ɣ�������̂���A�R���I";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F��}�[�N�Ȃ񂾂���A����ς�m�͊֌W���Ǝv��Ȃ��H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F�܂��A����ȋC�͂��Ă�񂾂��ȁB";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�A�C���A���Ȃ����̃X�e�[�^�X�����Ēm�͂͂����Ȃ킯�H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F���́A" + mc.Intelligence.ToString() + "���ď����ȁB";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�P���ɂ��ꂶ��Ȃ��H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F�b�n�b�n�b�n�B���������v�����������A�C���Ƃ��ȁI";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�z���g�A�������肵�Ă�ˁB";
                                                        ok.ShowDialog();
                                                        we.FailArea222 = true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        mainMessage.Text = "�@�@�@�@�w��}�[�N�̊Ŕ͂��������Ȃ��Ă���B�@�x";
                                    }
                                    return;
                                    #endregion

                                case 12: // �P�O��N�C�Y
                                    #region "�Ŕ˔j�C�x���g 2-3"
                                   if (!we.SolveArea23)
                                   {
                                       if (!we.InfoArea23)
                                       {
                                           mainMessage.Text = "�A�C���F��̊Ŕ̂��o�܂����ȁE�E�E�ȂɂȂɁH";
                                           ok.ShowDialog();
                                           mainMessage.Text = "�@�@�@�@�w���̐�A�s���~�܂�B�@�����g�ɖ₢������B�@�x";
                                           ok.ShowDialog();
                                           mainMessage.Text = "�A�C���F�����A�₢������Ηǂ��݂��������A�y�����ȁI";
                                           ok.ShowDialog();
                                           mainMessage.Text = "���i�F�ǂ��y���Ȃ̂�B����Ă݂��āB";
                                           ok.ShowDialog();
                                           mainMessage.Text = "�A�C���F�I�C�A�����̉��I�I";
                                           ok.ShowDialog();
                                           mainMessage.Text = "�A�C���F�E�E�E�E�E�E";
                                           ok.ShowDialog();
                                           mainMessage.Text = "���i�F������E�E�E";
                                           ok.ShowDialog();
                                           mainMessage.Text = "�A�C���F�E�E�E";
                                           ok.ShowDialog();
                                           mainMessage.Text = "���i�F�E�E�E�b�v�B�A�b�n�n�n�n�I�o�J����Ȃ��̃z���g�ɁA�b�t�t�A�A�n�n�n��";
                                           ok.ShowDialog();
                                           mainMessage.Text = "�A�C���F�E�E�E����ǂ�����ƁI�H";
                                           ok.ShowDialog();
                                           mainMessage.Text = "���i�F����A����͕�����Ȃ����ǁA���܂�ɂ��o�J�߂��āA���`�����Ɂ�";
                                           ok.ShowDialog();
                                           mainMessage.Text = "�A�C���F�������A�������ɈӖ��s���ɂȂ��Ă����ȁB�����T���ł����邩�B";
                                           ok.ShowDialog();
                                           mainMessage.Text = "���i�F�A�b�n�n�n�n�A�����ˁB�b�v�E�E�E�s���܁E�E�E�b�v�A�t�t�t�B";
                                           ok.ShowDialog();
                                           mainMessage.Text = "�A�C���F�������A�������̎؂�͕K���Ԃ����B�o���Ă��B";
                                           ok.ShowDialog();
                                           we.InfoArea23 = true;
                                       }
                                       else
                                       {
                                           int success = 0;
                                           mainMessage.Text = "�@�@�@�@�w���̐�A�s���~�܂�B�@�����g�ɖ₢������B�@�x";
                                           ok.ShowDialog();
                                           if (!we.FailArea23)
                                           {
                                               mainMessage.Text = "�A�C���F�E�E�E�E�E�E";
                                               ok.ShowDialog();
                                               mainMessage.Text = "���i�F�ǂ������̂�A�����Ȃ�ق����܂ܓ˂��������܂܂ŁB";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F���܂Ō��t�ʂ�̂��̂΂��肾��ȁH";
                                               ok.ShowDialog();
                                               mainMessage.Text = "���i�F�܂������ˁA�ςȕ��������N�C�Y��A�Ȃ��Ȃ��n�ł��Ȃ���B";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F���t�ʂ�s���Ƃ���΁A����ώ����ɖ₢�����邵���˂��킯���B�����ł��B";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F���i�A���Ɋւ��鎖�A���ł��ǂ����畷���Ă݂Ă����B";
                                               ok.ShowDialog();
                                               mainMessage.Text = "���i�F����Ȃ̂œ����J����́H";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F�����ȁA�ł�����Ă݂邵���Ȃ�����B";
                                               ok.ShowDialog();
                                               mainMessage.Text = "���i�F���[��A�A�C���Ɋւ��鎖�ˁH���������A����s�����B";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F�����A���ł��������Ă񂾁I";
                                               ok.ShowDialog();
                                           }
                                           else
                                           {
                                               mainMessage.Text = "���i�F�A�C���A���񂾂��B�A�i�^�������Ɠ�����Γ��͊J����͂���B";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F�����A���x��������Ă�邺�I";
                                               ok.ShowDialog();
                                           }
                                           mainMessage.Text = "���i�F�y�P��ځz�A�C���A���Ȃ��̎t���̖��O�́H";
                                           ok.ShowDialog();
                                           using (SelectAction sa = new SelectAction())
                                           {
                                               sa.StartPosition = FormStartPosition.CenterParent;
                                               sa.ForceChangeWidth = 300;
                                               sa.ElementA = "�K���c�E�M�����K";
                                               sa.ElementB = "�I���E�����f�B�X";
                                               sa.ElementC = "���i�E�A�~���A";
                                               sa.ShowDialog();
                                               if (sa.TargetNum == 1)
                                               {
                                                   mainMessage.Text = "���i�F�����ˁ�";
                                                   ok.ShowDialog();
                                                   success++;
                                               }
                                               else if (sa.TargetNum == 2)
                                               {
                                                   mainMessage.Text = "���i�F�܁[���R�̎���ˁ�@���̏ꍇ�A�n�Y�������ǁB";
                                                   ok.ShowDialog();
                                               }
                                               else
                                               {
                                                   mainMessage.Text = "���i�F�n�Y����B���񂽃o�J����Ȃ��H";
                                                   ok.ShowDialog();
                                               }
                                           }
                                           mainMessage.Text = "���i�F�y�Q��ځz�A�C�������������Ă��镐��͉��Ƃ������O�H";
                                           ok.ShowDialog();
                                           using (SelectAction sa = new SelectAction())
                                           {
                                               sa.StartPosition = FormStartPosition.CenterParent;
                                               sa.ForceChangeWidth = 300;
                                               sa.ElementA = mc.MainWeapon.Name;
                                               sa.ElementB = "�Ɍ�  �[�����M�A�X";
                                               sa.ElementC = "�v���`�i�\�[�h";
                                               sa.ShowDialog();
                                               if (sa.TargetNum == 0)
                                               {
                                                   mainMessage.Text = "���i�F�����ˁ�";
                                                   ok.ShowDialog();
                                                   success++;
                                               }
                                               else
                                               {
                                                   mainMessage.Text = "���i�F�n�Y����B���񂽃o�J����Ȃ��H";
                                                   ok.ShowDialog();
                                               }
                                           }
                                           mainMessage.Text = "���i�F�y�R��ځz�A�C�����k�u�T�̎��A�K�������͉̂��H";
                                           ok.ShowDialog();
                                           using (SelectAction sa = new SelectAction())
                                           {
                                               sa.StartPosition = FormStartPosition.CenterParent;
                                               sa.ForceChangeWidth = 300;
                                               sa.ElementA = "�t�@�C�A�E�{�[��";
                                               sa.ElementB = "�X�g���[�g�E�X�}�b�V��";
                                               sa.ElementC = "���U���N�V����";
                                               sa.ShowDialog();
                                               if (sa.TargetNum == 0)
                                               {
                                                   mainMessage.Text = "���i�F�����ˁ�";
                                                   ok.ShowDialog();
                                                   success++;
                                               }
                                               else
                                               {
                                                   mainMessage.Text = "���i�F�n�Y����B���񂽃o�J����Ȃ��H";
                                                   ok.ShowDialog();
                                               }
                                           }
                                           mainMessage.Text = "���i�F�y�S��ځz�A�C���͂ǂ��炩�ƌ����΁E�E�E�H";
                                           ok.ShowDialog();
                                           using (SelectAction sa = new SelectAction())
                                           {
                                               sa.StartPosition = FormStartPosition.CenterParent;
                                               sa.ForceChangeWidth = 300;
                                               sa.ElementA = "�e�N�j�b�N�h";
                                               sa.ElementB = "���@�U���h";
                                               sa.ElementC = "���ڍU���h";
                                               sa.ShowDialog();
                                               if (sa.TargetNum == 0)
                                               {
                                                   if (mc.Agility >= mc.Strength && mc.Agility >= mc.Intelligence)
                                                   {
                                                       mainMessage.Text = "���i�F�����ˁ�";
                                                       ok.ShowDialog();
                                                       success++;
                                                   }
                                                   else
                                                   {
                                                       mainMessage.Text = "���i�F�n�Y����B�����̓������炢�����ƒ͂�ł����Ȃ�����B";
                                                       ok.ShowDialog();
                                                   }
                                               }
                                               else if (sa.TargetNum == 1)
                                               {
                                                   if (mc.Intelligence >= mc.Strength && mc.Intelligence >= mc.Agility)
                                                   {
                                                       mainMessage.Text = "���i�F�����ˁ�";
                                                       ok.ShowDialog();
                                                       success++;
                                                   }
                                                   else
                                                   {
                                                       mainMessage.Text = "���i�F�n�Y����B�����̓������炢�����ƒ͂�ł����Ȃ�����B";
                                                       ok.ShowDialog();
                                                   }
                                               }
                                               else if (sa.TargetNum == 2)
                                               {
                                                   if (mc.Strength >= mc.Intelligence && mc.Strength >= mc.Agility)
                                                   {
                                                       mainMessage.Text = "���i�F�����ˁ�";
                                                       ok.ShowDialog();
                                                       success++;
                                                   }
                                                   else
                                                   {
                                                       mainMessage.Text = "���i�F�n�Y����B�����̓������炢�����ƒ͂�ł����Ȃ�����B";
                                                       ok.ShowDialog();
                                                   }
                                               }
                                               else
                                               {
                                                   mainMessage.Text = "���i�F�n�Y����B���񂽃o�J����Ȃ��H";
                                                   ok.ShowDialog();
                                               }
                                           }
                                           mainMessage.Text = "���i�F�y�T��ځz�A�C���̑O�ɗ����ӂ��������P�K�̃{�X�̕K�E�Z�́H";
                                           ok.ShowDialog();
                                           using (SelectAction sa = new SelectAction())
                                           {
                                               sa.StartPosition = FormStartPosition.CenterParent;
                                               sa.ForceChangeWidth = 300;
                                               sa.ElementA = "�G�^�[�i���E�u���X�^�[";
                                               sa.ElementB = "�t�@�C�A�E�{�[��";
                                               sa.ElementC = "�L���E�X�s�j���O�����T�[";
                                               sa.ShowDialog();
                                               if (sa.TargetNum == 2)
                                               {
                                                   mainMessage.Text = "���i�F�����ˁ�";
                                                   ok.ShowDialog();
                                                   success++;
                                               }
                                               else
                                               {
                                                   mainMessage.Text = "���i�F�n�Y����B���񂽃o�J����Ȃ��H";
                                                   ok.ShowDialog();
                                               }
                                           }
                                           mainMessage.Text = "���i�F�y�U��ځz�A�C�������̂Q�K�ɓ��B�����͉̂����ځH";
                                           ok.ShowDialog();
                                           using (RequestInput ri = new RequestInput())
                                           {
                                               ri.StartPosition = FormStartPosition.CenterParent;
                                               ri.ShowDialog();
                                               if (ri.InputData == we.CompleteArea1Day.ToString())
                                               {
                                                   mainMessage.Text = "���i�F�����ˁ�";
                                                   ok.ShowDialog();
                                                   success++;
                                               }
                                               else
                                               {
                                                   mainMessage.Text = "���i�F�n�Y����B���񂽃o�J����Ȃ��H";
                                                   ok.ShowDialog();
                                               }
                                           }
                                           mainMessage.Text = "���i�F�y�V��ځz�A�C���̃t�@�C�A�E�{�[���ƃt���b�V���E�q�[���B�m�͂łt�o�䗦�������̂́H";
                                           ok.ShowDialog();
                                           using (SelectAction sa = new SelectAction())
                                           {
                                               sa.StartPosition = FormStartPosition.CenterParent;
                                               sa.ForceChangeWidth = 300;
                                               sa.ElementA = "�t�@�C�A�E�{�[��";
                                               sa.ElementB = "�t���b�V���E�q�[��";
                                               sa.ElementC = "�ǂ��������";
                                               sa.ShowDialog();
                                               if (sa.TargetNum == 1)
                                               {
                                                   mainMessage.Text = "���i�F�����ˁ�";
                                                   ok.ShowDialog();
                                                   success++;
                                               }
                                               else
                                               {
                                                   mainMessage.Text = "���i�F�n�Y����B���񂽃o�J����Ȃ��H";
                                                   ok.ShowDialog();
                                               }
                                           }
                                           mainMessage.Text = "���i�F�y�W��ځz�A�C���A�������͂܂��_���W�����̒���B�o���Ă�H";
                                           ok.ShowDialog();
                                           mainMessage.Text = "�A�C���F�Ȃ񂾂����H�H";
                                           ok.ShowDialog();
                                           mainMessage.Text = "���i�F�����Ɠ����Ȃ����B";
                                           ok.ShowDialog();
                                           mainMessage.Text = "�A�C���F����܂��A���Ⴀ������Ƃ��邩�B";
                                           ok.ShowDialog();
                                           using (SelectAction sa = new SelectAction())
                                           {
                                               sa.StartPosition = FormStartPosition.CenterParent;
                                               sa.ForceChangeWidth = 300;
                                               sa.ElementA = "�����A�܂��_���W�����̒����B";
                                               sa.ElementB = "������A���́E�E�E�O���B";
                                               sa.ElementC = "�o���Ă邺�B";
                                               sa.ShowDialog();
                                               if (sa.TargetNum == 0)
                                               {
                                                   mainMessage.Text = "���i�F�����ˁ�";
                                                   ok.ShowDialog();
                                                   success++;
                                               }
                                               else if (sa.TargetNum == 2)
                                               {
                                                   mainMessage.Text = "���i�F�����ˁ�";
                                                   ok.ShowDialog();
                                                   success++;
                                                   // [�x��]�F�P��N���A���Ă���ꍇ�A�ǉ����₵�Ă��������B
                                               }
                                               else
                                               {
                                                   mainMessage.Text = "���i�F�n�Y����B���񂽃o�J����Ȃ��H";
                                                   ok.ShowDialog();
                                               }
                                           }
                                           mainMessage.Text = "���i�F�y�X��ځz�A�C���ƌ����΂���ς�o�J�H";
                                           ok.ShowDialog();
                                           mainMessage.Text = "�A�C���F�҂āA����͎��₶��˂�����H";
                                           ok.ShowDialog();
                                           mainMessage.Text = "���i�F�����A�񓚉𓚁�";
                                           ok.ShowDialog();
                                           mainMessage.Text = "�A�C���F�������A��΂ɉ�����Ă�邺�E�E�E�I�����������Ă݂���Ă񂾁I";
                                           ok.ShowDialog();
                                           using (SelectAction sa = new SelectAction())
                                           {
                                               sa.StartPosition = FormStartPosition.CenterParent;
                                               sa.ForceChangeWidth = 300;
                                               sa.ElementA = "�o�J�I";
                                               sa.ElementB = "�o�J��";
                                               sa.ElementC = "�E�E�E�o�J";
                                               sa.ShowDialog();
                                               if (sa.TargetNum == -1)
                                               {
                                                   mainMessage.Text = "���i�F���ȁI�H���ŉ���o����̂�B�n�Y���ˁI";
                                                   ok.ShowDialog();
                                               }
                                               else
                                               {
                                                   mainMessage.Text = "���i�F�����ˁ�";
                                                   ok.ShowDialog();
                                                   mainMessage.Text = "�A�C���F�҂đ҂đ҂āI���낢�뉽���ƊԈ���Ă邾��A���̂́I";
                                                   ok.ShowDialog();
                                                   mainMessage.Text = "���i�F�ǂ�����Ȃ��A�����Ȃ񂾂��琳�����";
                                                   ok.ShowDialog();
                                                   mainMessage.Text = "�A�C���F�����A�_��E�E�E���̃��i�E�A�~���A�Ƃ����l�ԂɎ����݂̐S���E�E�E";
                                                   ok.ShowDialog();
                                                   success++;
                                               }
                                           }

                                           if (!we.FailArea23)
                                           {
                                               mainMessage.Text = "���i�F�y�P�O��ځz����E�E�E���Ⴀ�Ō��B���́A�D���Ȑl������B����͒N�H";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F�E�E�E���ł��ǂ��Ƃ͌��������A���ł��ǂ��킯����˂��B";
                                               ok.ShowDialog();
                                               mainMessage.Text = "���i�F�����ɓ����Ȃ�����B";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F�������������A�}�W����B���Ď��₵�₪��B";
                                               ok.ShowDialog();
                                               while (true)
                                               {
                                                   using (SelectAction sa = new SelectAction())
                                                   {
                                                       sa.StartPosition = FormStartPosition.CenterParent;
                                                       sa.ForceChangeWidth = 300;
                                                       sa.ElementA = "�I���E�����f�B�X";
                                                       sa.ElementB = "���i�E�A�~���A";
                                                       sa.ElementC = "�t�@���E�t���[������";
                                                       sa.ShowDialog();
                                                       if (sa.TargetNum == -1)
                                                       {
                                                           mainMessage.Text = "���i�F�ʖڂ�A�����Ɠ����邱�ƁB";
                                                           ok.ShowDialog();
                                                       }
                                                       else if (sa.TargetNum == 0)
                                                       {
                                                           mainMessage.Text = "���i�F���[��E�E�E�����������񂾁B�Ȃ񂾂��񂾌����Ă��ˁ�";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "�A�C���F�����A���̂͑I���~�X���B�N������ȃN�\�t�����B";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "���i�F�܂��܂��A�����������ɂ��Ă����܂����B�b�n�C�I���ጋ�ʔ��\�I";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "�A�C���F�����A���ʂ𕷂����Ă���B";
                                                           ok.ShowDialog();
                                                           break;
                                                       }
                                                       else if (sa.TargetNum == 1)
                                                       {
                                                           mainMessage.Text = "���i�F�E�E�E�E�E�E���ˁB�o�J�A�C���B";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "�A�C���F���P�킩��˂����₵�Ă񂶂�˂���I���A��̍��̐����Ȃ̂���I�H";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "���i�F���g�b�v�V�[�N���b�g�ˁB���񂽃o�J������A�ꐶ�����炸���܂���B";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "�A�C���F���ʃN�G�X�`�����A���h�A���T�[�͐������������������I";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "���i�F���񂽂��������Č������畷���Ă邾����B";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "�A�C���F�����E�E�E�����A�����������������B�n�Y���Ȃ񂾂ȁB����������B";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "���i�F�ׂ��A�ʂɃn�Y���Ȃ�Č����ĂȂ�����Ȃ��B";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "�A�C���F�����Ȃ̂��H���Ⴀ�E�E�E�Ђ���Ƃ��Đ�";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "�@�@�@�w�b�o�L���A�A�@�@�@�I�I�I�x�i���C�g�j���O�L�b�N���A�C�����y��j�@�@";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "�A�C���F�b�O�A�b�E�S�I�H�H�H�E�E�E�����ɓ����E�E�E�b�K�n�A�@�@�E�E�E";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "���i�F�b�n�C�I���ጋ�ʔ��\�s������";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "�A�C���F�b�Q�z�I�b�Q�z�I�@�A���������C�b�e�F�E�E�E�����A���ʂ𕷂����Ă���B";
                                                           ok.ShowDialog();
                                                           success++;
                                                           break;
                                                       }
                                                       else if (sa.TargetNum == 2)
                                                       {
                                                           mainMessage.Text = "���i�F������E�E�E�w���^�C�I�I";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "�A�C���F����ȑI�����o���Ă�����A����Ȃ̂͂��ꂵ���˂�����I";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "���i�F�t�@���l��I�Ԃ̂�����H�H�w���^�C�ʂ�z���Ĕƍߎ҂�B";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "�A�C���F�����f�B�̃��c�Ȃ񂩂��D���Ȃ�Č����Ă݂�B���E�s�ׂ���I�H";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "���i�F�E�E�E�ȁ[�񂾁B�����@�őI�񂶂�����񂾁B";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "�A�C���F����A�����@���Ă킯����˂����B";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "���i�F�E�E�E����A���X�@�őI�񂾂̂�H";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "�A�C���F�������ėǂ����낤������Ȏ��́B�ǂ��Ȃ񂾂�A�����Ȃ̂���I�H";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "���i�F������킯��������Ȃ��B���񂽃o�J������A�ꐶ�����炸���܂���B";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "�A�C���F�����E�E�E�܂��ǂ����ł��ǂ����B����Ȃ���ǂ����������N�\���˂��񂾂�B";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "���i�F�E�E�E���A�����ƂƁA���������킯����E�E�E���Ⴀ���ʔ��\���";
                                                           ok.ShowDialog();
                                                           mainMessage.Text = "�A�C���F�����A���ʂ𕷂����Ă���B";
                                                           ok.ShowDialog();
                                                           break;
                                                       }
                                                   }
                                               }
                                           }
                                           else
                                           {
                                               mainMessage.Text = "���i�F�y�P�O��ځz���Ⴀ�Ō��B�K���c�E�M�����K�f������̍Ȃ͒N�H";
                                               ok.ShowDialog();
                                               using (SelectAction sa = new SelectAction())
                                               {
                                                   sa.StartPosition = FormStartPosition.CenterParent;
                                                   sa.ForceChangeWidth = 300;
                                                   sa.ElementA = "�t�@���E�t���[��";
                                                   sa.ElementB = "���i�E�A�~���A";
                                                   sa.ElementC = "�n���i�E�M�����K";
                                                   sa.ShowDialog();
                                                   if (sa.TargetNum == 2)
                                                   {
                                                       mainMessage.Text = "���i�F�����ˁ�";
                                                       ok.ShowDialog();
                                                       success++;
                                                   }
                                                   else
                                                   {
                                                       mainMessage.Text = "���i�F�n�Y����B���񂽃o�J����Ȃ��H";
                                                       ok.ShowDialog();
                                                   }
                                               }
                                           }

                                           if (success >= 10 && !we.FailArea23)
                                           {
                                               // [�x��]�F���ʃt���O������ƕ���̖ʔ������c��݂܂��B
                                           }

                                           if (success >= 8)
                                           {
                                               mainMessage.Text = "���i�F�����A���\��邶��Ȃ��́�";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F�ǂ����A����Ȃ�Ŕ̕������ς��͂�����B���Ă݂悤���I";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�@�@�@�@�w���̐�A���֐i�ނׂ��B�E�֐i�ނׂ��炸�B�@�x";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F�悵�����I���ӂ��E�E�E�������������ނ́A���������ɂ��ė~�������B";
                                               ok.ShowDialog();
                                               mainMessage.Text = "���i�F�ł��A�_���W�����Ƃ����Γ䂩���͕t�����̂�B�g���b�v���������������ǂ�����Ȃ��B";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F�܂��A���������ǂȁB�悵�A���Ⴀ���֐i�ނƂ��邩�I";
                                               ok.ShowDialog();
                                               mainMessage.Text = "���i�F�A�C���A�O������������ǁA���ŊŔʂ�i�ނ̂�H";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F�܂�����Ȏ������Ă�̂��B���i�A���O�͐̂�����^��[��������ȁB";
                                               ok.ShowDialog();
                                               mainMessage.Text = "���i�F�A�C�����o�J�߂���̂�B";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F�E�E�E���i�A�������悭�����B";
                                               ok.ShowDialog();
                                               mainMessage.Text = "���i�F�H�@����^�ʖڂȊ�Ȃ񂩂�������āB";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F�����̊Ŕ������{���̎��������Ă���Ƃ��悤����Ȃ����B";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F�������炻�̂܂ܐi�߂Ηǂ������̎����B";
                                               ok.ShowDialog();
                                               mainMessage.Text = "���i�F�܂������ˁB";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F�{���̎��������Ă���̂ɁA�����������Ȏ��l���Ĕ��΂ɍs�����Ƃ���΁B";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F����͗��؂����ƌ������ɂȂ�B���_�ȍs�ׂ��B�킩��ȁH";
                                               ok.ShowDialog();
                                               mainMessage.Text = "���i�F���؂����H�ǂ������Ӗ���H";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F�Ŕ͐l����˂����A�����l�Ȃ�A������{���̎��͌���Ȃ��Ȃ���Ď����B";
                                               ok.ShowDialog();
                                               mainMessage.Text = "���i�F�悭�킩��Ȃ���ˁA�Ŕ͊Ŕ�B�����A�R��������ǂ�����̂�H";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F���Ⴀ�A�����R�������Ă���Ƃ��悤�B";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F���Ƃ���Δ��΂ɍs���̂��A���R�������B����";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F����͉����������̊Ŕ̐��������j��Ă邩�ǂ����������B";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F�c�O�����A�������͍��̏���������j��p���������킹�Ă˂��B";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F���i�A�R���ǂ����Ȃ�Ă킩��˂�����H";
                                               ok.ShowDialog();
                                               mainMessage.Text = "���i�F�܂��A�s���Ă݂Ȃ����ɂ͉R���ǂ����͌��Ǖ�����Ȃ���ˁB";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F�������B�����������ꍇ���Ǎs���Ă݂�Ƃ����s�ׂ����A�I�����͎c����Ă˂��񂾂�B";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F���؂��āA���_�ȍs�ׂ�I�����邩�B����Ƃ��M�����x��������I�����邩�B";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F�M�����x���ꂽ�ꍇ�A�҉�͏o����B���Ƃ��Ԃ����@���c���Ă�͂����B";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F�������؂��ă��_�ȍs�ׂ��������͎��Ԃ������˂��B�t�����炻����������񂾁B";
                                               ok.ShowDialog();
                                               mainMessage.Text = "���i�F�E�E�E�_���A�����������̑S�R�킩��Ȃ��B�ł������f�B�X���t�����񂪂����������̂ˁH";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F�����������B���c�͎x���ŗ�Ŗ����ꒃ�����A�s�v�c�ƃX�W���ʂ��Ă邵�ȁB";
                                               ok.ShowDialog();
                                               mainMessage.Text = "���i�F�A�C���̃o�J�b�͑S�R�킩��Ȃ����ǁA�����f�B�X���t�������M���鎖�ɂ�����";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F�����A���̉���͂Ȃ񂾂����񂾂�E�E�E�܂��ǂ����A�s�������I���ցI";
                                               ok.ShowDialog();
                                               mainMessage.Text = "���i�F�����ˁA���፶�֍s���܂����";
                                               ok.ShowDialog();

                                               we.SolveArea23 = true;
                                               we.CompleteArea23 = true;
                                               ConstructDungeonMap();
                                               SetupDungeonMapping(2);
                                           }
                                           else
                                           {
                                               mainMessage.Text = "���i�F������ƁA�S�R�b�ɂȂ�Ȃ�����Ȃ��B";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F�������H����Ȃ��񂾂�H";
                                               ok.ShowDialog();
                                               mainMessage.Text = "���i�F�ʖڂˁA��蒼����B";
                                               ok.ShowDialog();
                                               mainMessage.Text = "�A�C���F�������A�Ȃ�Ėʓ|�����������E�E�E������A����������A�������I";
                                               ok.ShowDialog();
                                               mainMessage.Text = "���i�F�z���g���ނ��B";
                                               ok.ShowDialog();
                                               we.FailArea23 = true;
                                           }
                                       }
                                   }
                                   else
                                   {
                                       if (!we.CompleteArea26)
                                       {
                                           mainMessage.Text = "�@�@�@�@�w���̐�A���֐i�ނׂ��B�E�֐i�ނׂ��炸�B�@�x";
                                       }
                                       else
                                       {
                                           mainMessage.Text = "�Ŕ͂��������Ȃ��Ă���B";
                                       }
                                   }
                                   return;
                                   #endregion

                                case 13: // 
                                    #region "�Ŕ˔j�C�x���g 2-4"
                                    if (!we.SolveArea24)
                                    {
                                        if (!we.InfoArea24)
                                        {
                                            mainMessage.Text = "�A�C���F���A����ς�Ŕ����邺�E�E�E�ȂɂȂɁH";
                                            mainMessage.Update();
                                            ok.ShowDialog();
                                            mainMessage.Text = "�@�@�@�@�w���̐�A�s���~�܂�B�@�����������A���������؂������B�@�x";
                                            mainMessage.Update();
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�ʖڂ��B���͂������́g�������h�Ƃ����������C�ɂ���˂��B";
                                            mainMessage.Update();
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�������Ă�̂�B�����Ȃ������ɐi�߂Ȃ���ł���H";
                                            mainMessage.Update();
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�ӂ����������E�E�E����T���ƍs�����B";
                                            mainMessage.Update();
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�b�t�t�A�Ȃ񂾂����Ă�݂����ˁB�������C���ł��H";
                                            mainMessage.Update();
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�����A���͍���p�X�Ƃ����Ă��炤���A���񂾂����i�B";
                                            mainMessage.Update();
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�悵�A����A�C���͋x��łėǂ����B���������T�����܂�����";
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
                                                mainMessage.Text = "���i�F��ʂ�܂������ˁB�E�E�E����H������ƃA�C���B";
                                                mainMessage.Update();
                                                ok.ShowDialog();
                                                mainMessage.Text = "�A�C���F�Ȃ񂾁A�ǂ������H";
                                                mainMessage.Update();
                                                ok.ShowDialog();
                                                mainMessage.Text = "���i�F�ŔE�E�E�Ŕ��̂������Ė����Ȃ��Ă��B";
                                                mainMessage.Update();
                                                ok.ShowDialog();
                                                mainMessage.Text = "�A�C���F�ȂɁI�H���Ď��͂����N���A�����̂��I�H";
                                                mainMessage.Update();
                                                ok.ShowDialog();
                                                mainMessage.Text = "���i�F��ȃ��P�Ȃ��ł���B���܂ł͑������ς���Ă�����Ȃ��B";
                                                mainMessage.Update();
                                                ok.ShowDialog();
                                                mainMessage.Text = "�A�C���F��������A�������ȁB";
                                                mainMessage.Update();
                                                ok.ShowDialog();
                                                mainMessage.Text = "���i�F������x��������܂Ȃ��T���Ă݂܂���B";
                                                mainMessage.Update();
                                                ok.ShowDialog();
                                                mainMessage.Text = "�A�C���F�����A�ǂ����B";
                                                mainMessage.Update();
                                                ok.ShowDialog();
                                                we.SolveArea24 = true; // [�x��]�F�����ƃR���v���[�g���ʂȂ̂�ʊϓ_�ŗ��p���Ă��܂��B�����̌��ɂȂ�悤�ł���ΐV�����t���O��ǋL���Ă��������B
                                            }
                                            else
                                            {
                                                mainMessage.Text = "�@�@�@�@�w���̐�A�s���~�܂�B�@�����������A���������؂������B�@�x";
                                                mainMessage.Update();
                                            }
                                        }
                                    }
                                    else
                                    {
                                    }
                                    return;
                                    #endregion

                                #region "�Ŕ˔j�C�x���g 2-4 �ʉߐ� case14 - case 25"
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
                                    #region "�Ŕ˔j�C�x���g 2-4 �Q"
                                    if (we.SolveArea24)
                                    {
                                        if (!we.CompleteArea24)
                                        {
                                            if (we.ProgressArea241 && we.ProgressArea2412 && we.ProgressArea2413 && we.ProgressArea242 && we.ProgressArea2422 && we.ProgressArea243 && we.ProgressArea2432 && we.ProgressArea244 && we.ProgressArea2442 && we.ProgressArea245 && we.ProgressArea2452)
                                            {
                                                if (!we.FirstProcessArea24)
                                                {
                                                    // [�x��]�F�R�R�͉�����V��݂������������Ƃ��Đ���オ��܂��B
                                                    mainMessage.Text = "�@�@�@�@�w�b�S�S�S�S�S�E�E�E�Y�E�E�D�D���I�x";
                                                    ok.ShowDialog();
                                                    we.CompleteArea24 = true;
                                                    ConstructDungeonMap();
                                                    SetupDungeonMapping(2);
                                                    mainMessage.Text = "�@�@�@�@�w���̐�A���֐i�ނׂ��B�x";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F�}�W����I�H�Ŕ������Ǝv������A�ˑR�����J�������I�H";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F�b�t�t�A�A�C�����Ⴑ���͍s���Ȃ��ł����";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F�s���Ȃ��������A�}�W�ŕ�����Ȃ��������B�ǂ����������������񂾁H";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F�����ɐ������������ł���H����͑�̑z�������Ǝv�����ǁA�����������̎��ˁB";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F�����A�m���ɂP�Ƃ��S�Ƃ���������ȁB";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F�����̐����͂P�`�U�܂ł���������A�P�C�Q�C�R�C�S�C�T�C�U�̏������Ď���B";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F�Ȃ�قǁA���̐����͓��̂�̏������Ď��������̂��B";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F���ꂩ��Ŕ̈ʒu�����ǁB";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F�������B�Ŕ̈ʒu�͍��n�߂Č������ꏊ�����H";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F���̃_���W�����}�b�s���O�����ɂ��΁A���̋��͂��Ɖ������c���ĂȂ���B";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F�����牺�̕ǂ̒����A�܂荡����ꏊ�����̓����J����ꏊ���Ď���B�������Ŕ̈ʒu���Ǝv��Ȃ��H";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F����Ȑ����܂Ő��藧�̂��E�E�E���O�A�Ђ���Ƃ��ăJ�[���݂̃��x���Ȃ񂶂�˂��́H";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F���܂��܂�B�J�[���݂�������m���Ɉꔭ�N���A���Ă����ˁB";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F���Ⴀ�c��́A���������؂��ĕ\�������E�E�E";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F���̋��̃��C�A�E�g������Ε����邯�ǁA���炩�ɒ��Ɠ��ɕ�����Ă�ł���B";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F�����琔���̂P���X�^�[�g�A�Q�C�R�C�S�C�T�C�U��ʂ�悤�ɓ��؂��o�āA�Ō�͓��R�ŔˁB";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F�ꔭ�����Ȃ�Ă��蓾��̂���B���i�A���O����ς������I�A��ė��đ吳�����ȁI";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F�b�t�t�A�z���z���A�܂��r���Ȃ񂾂���A���X�i�݂܂���B";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F�����A���͉����ȁI���𗹉��I";
                                                    ok.ShowDialog();
                                                }
                                                else
                                                {
                                                    if (!we.FailArea241)
                                                    {
                                                        mainMessage.Text = "�@�@�@�@�w�b�S�S�S�S�S�E�E�E�Y�E�E�D�D���I�x";
                                                        ok.ShowDialog();
                                                        we.CompleteArea24 = true;
                                                        ConstructDungeonMap();
                                                        SetupDungeonMapping(2);
                                                        mainMessage.Text = "�@�@�@�@�w���̐�A���֐i�ނׂ��B�x";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F�}�W����I�H�ǂ��������������񂾁B���炭��������Ă����B";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�閧���";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F�ʖڂ��A���������C�ɂȂ�I���i�l���肢���܂��A���񂾂��͋����Ă���E�E�E";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�����ɐ������������ł���H����͑�̑z�������Ǝv�����ǁA�����������̎��ˁB";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F�ǂ������Ӗ����H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F������A�����̐������P�`�U�܂ł��邩��A�P�C�Q�C�R�C�S�C�T�C�U�̏������Ď���B";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F���������A�P�C�Q�C�R�C�S�C�T�C�U�̏������Ď����ȁB";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�������J��Ԃ������Ăǂ�����̂�B���ꂩ��Ŕ̈ʒu�����ǁA";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F�����A����Ȃ��̂ɈӖ������������Ă����̂��H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�����Ă݂�΁A�S�[���n�_�ˁB���̏ꏊ�ɂ������񂶂�A�ǂ����Ă����������؂����Ȃ���B";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F���̐��������؂��ĉ��������񂾂�H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F���̋��̃��C�A�E�g������Ε����邯�ǁA���炩�ɒ��Ɠ��ɕ�����Ă�ł���B";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�����琔���̂P���X�^�[�g�A�Q�C�R�C�S�C�T�C�U�Ɨ��āE�E�E�Ŕ��S�[���ɂȂ���Ď��B";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F�E�E�E�́H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�P���X�^�[�g�A�Q�C�R�C�S�C�T�C�U�Ɛ����������ǂ���̓��؂��o�āA�Ŕ֓�������ƃS�[����B";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F�E�E�E�́H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�P�C�Q�C�R�C�S�C�T�C�U�A�Ŕ��Č����Ă邶��Ȃ��B";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F�E�E�E�́H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F���[�āA�V�������`�ł��҂ݏo���ė~�����̂������";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F�I�[�P�[�A�I�[�P�[�I���������A�����������B���������i�I";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�܂������A�z���g�ɕ������Č����Ă�̂�����B";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F�������Č����Ă�񂾁A�������ɍs�������I";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�����A���֐i�݂܂���B";
                                                        ok.ShowDialog();
                                                    }
                                                    else
                                                    {
                                                        mainMessage.Text = "�@�@�@�@�w�b�S�S�S�S�S�E�E�E�Y�E�E�D�D���I�x";
                                                        ok.ShowDialog();
                                                        we.CompleteArea24 = true;
                                                        ConstructDungeonMap();
                                                        SetupDungeonMapping(2);
                                                        mainMessage.Text = "�@�@�@�@�w���̐�A���֐i�ނׂ��B�x";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F���I���������˂����A���i�I";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F���Ƃ��N���A������ˁ�";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�P���X�^�[�g�A�Q�C�R�C�S�C�T�C�U�Ɛ����������ǂ���̓��؂��o�āA�Ŕ֓�������ƃS�[���������킯�ˁB";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F��͂�������Ŕ��w�������ʂ�A���ɍs���������ȁB";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�����A���֐i�݂܂���B";
                                                        ok.ShowDialog();
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (!we.FailArea24)
                                                {
                                                    mainMessage.Text = "�@�@�@�@�w���̐�A�s���~�܂�B�@�����������A���������؂������B�@�x";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F����ȏ��Ɉړ����Ă��̂ˁB�����͕ς���ĂȂ��݂����B";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F���ł��������ꏊ��ς��Ă����񂾁H";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F����Ȏ��m��Ȃ킢��B";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F�Ƃ���ŁA�����ɗ���Ƃ��ɕςȔԍ��������ɏ����Ă������ȁB";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F�ŏ��T���������͏����ĂȂ��������A�����ƈӖ�������͂�����B";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F�Ŕ̈ʒu�͕ς��B�ςȐ����͕����オ��B�ǂ��Ȃ��Ă񂾃R�R�́H";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F�E�E�E����������B";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F�}�W����I�H�ǂ����������񂾂�I�H";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F�Ŕ̈ʒu�Ɛ���������Έꔭ�ˁB�܂��A�C���āB���ė��ā�";
                                                    ok.ShowDialog();
                                                    we.FailArea24 = true;
                                                }
                                                else
                                                {
                                                    if (!we.FailArea241)
                                                    {
                                                        mainMessage.Text = "�@�@�@�@�w���̐�A�s���~�܂�B�@�����������A���������؂������B�@�x";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�E�E�E����������ˁB�ς���ĂȂ���ˁB";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F�����A�ʖڂ������̂��H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�����ˁB���s����B�����ʖڂ������̂�����B";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F�悭�킩��˂����ǂ��B���̏����̐������Ă���ς�P����U���ď����������Ă�̂��H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F����A��������͍����Ă��B";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F���Ƃ�����A�����悭����łȂ��������Ď�����B���������؂����Ă݂˂����H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�����ˁA������x����Ă݂܂����";
                                                        ok.ShowDialog();
                                                        we.FailArea241 = true;
                                                    }
                                                    else
                                                    {
                                                        if (!we.FailArea242)
                                                        {
                                                            mainMessage.Text = "�@�@�@�@�w���̐�A�s���~�܂�B�@�����������A���������؂������B�@�x";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "���i�F�E�E�E����������ˁB�����������Œʂ����Ǝv�����񂾂��ǁB";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "�A�C���F�����낤�ȁE�E�E��������A���������Ƃ������Ă��邼�B";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "���i�F�����ʂ肾������s�����Ď�������B���������؂������悤�ȃ��m�Ǝv�����񂾂��ǁB";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "�A�C���F�܂������悤�Ȃ��񂾂�ȁB";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "���i�F����҂��āB�����̋��A�悭����΁A���Ɠ��̍\���ɂȂ��Ă���ˁB";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "�A�C���F��H�����A�������₻�����������ȁB���ꂪ�ǂ��������̂��H";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "���i�F����������A���������؂�B�܂�A���؂����悤�ɐi�߂Ηǂ��̂�B";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "�A�C���F���؂��E�E�E���H";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "���i�F�����A��U�ʂ��������Q��܂�������A�������������Ԃ�����A�d�����Ȃ��悤�ɒʉ߂���΂����̂�B";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "�A�C���F�Ȃ�قǁA��邶��˂����B����œ����肶��˂����H";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "���i�F�P���珇�Ԓʂ�ŁA�P�{�̓��؂����悤�ɂ��鎖�B������x����Ă݂܂����";
                                                            ok.ShowDialog();
                                                            we.FailArea242 = true;
                                                        }
                                                        else
                                                        {
                                                            mainMessage.Text = "�@�@�@�@�w���̐�A�s���~�܂�B�@�����������A���������؂������B�@�x";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "���i�F��������ƁI���őʖڂȂ́I�H";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "�A�C���F���������Đ������悤���B�܂����������������A";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "���i�F�P�`�U�̔ԍ��������ɂ��邩��A���̏��Ԃǂ���ʂ�Ηǂ��̂�B";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "�A�C���F���̐��������؂��ĈӖ������A";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "���i�F���؂����悤�ɒʉ߂���B";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "�A�C���F�Q��܂����ł��ʖڂ�";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "���i�F�������������Ԃ��̂��ʖځB";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "�A�C���F�d������悤�Ȓʉ߂��ʖڂ��ă��P���B";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "���i�F������x����Ă݂��B";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "�A�C���F�����A���i�B���O��M�p���Ă邺�B";
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
                                                mainMessage.Text = "�@�@�@�@�w���̐�A���֐i�ނׂ��B�x";
                                            }
                                            else
                                            {
                                                mainMessage.Text = "�Ŕ͂��������Ȃ��Ă���B";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // �Ŕʒu�ڍs�O�͉����������Ȃ��B
                                    }
                                    return;
                                    #endregion

                                case 27:
                                    #region "�Ŕ˔j�C�x���g 2-5"
                                    if (!we.SolveArea25)
                                    {
                                        mainMessage.Text = "�A�C���F�{���ɃR�R�͂����Ŕ�����񂾁E�E�E�ȂɂȂɁH";
                                        ok.ShowDialog();
                                        mainMessage.Text = "�@�@�@�@�w���̐�A�s���~�܂�B�@�Ō�̋��֐i�ނׂ��B�x";
                                        ok.ShowDialog();
                                        mainMessage.Text = "�A�C���F��������A�Ō�̋����ĂȂ񂾂�H";
                                        ok.ShowDialog();
                                        mainMessage.Text = "���i�F�Ō�̋��E�E�E������Ƒ҂��ĂˁB�_���W�����}�b�v�����J���Ă݂邩��B";
                                        ok.ShowDialog();
                                        mainMessage.Text = "�A�C���F�񂾁A����I�H����Ȃ��񏑂��Ă����̂���H";
                                        ok.ShowDialog();
                                        mainMessage.Text = "���i�F������O����Ȃ��B��̃_���W�������Ă͍̂s��������c�����Ă邩�ǂ���������B";
                                        ok.ShowDialog();
                                        mainMessage.Text = "�A�C���F���ŁA�ǂ��Ȃ񂾁B�Ō�̋����Ă̂͂ǂ���ӂ�������̂��H";
                                        ok.ShowDialog();
                                        mainMessage.Text = "���i�F�����ƁA�������́E�E�E�E�゠�����B�R�R���������B�̈�Ȃ킯�����B";
                                        ok.ShowDialog();
                                        mainMessage.Text = "�A�C���F�Ȃ�قǁA���ɗ����񂾂ȁB�����������I";
                                        ok.ShowDialog();
                                        mainMessage.Text = "�A�C���F���̋��ł͉������ĂȂ����A����ŏI���Ȃ̂��H�₯�ɃA�b�T�����Ă�ȁB";
                                        ok.ShowDialog();
                                        mainMessage.Text = "���i�F�����ˁA�ł��Ō�̋����Ă̂�������Ȃ���΁A���ꎩ�̂��₢�����ɂȂ��Ă��B";
                                        ok.ShowDialog();
                                        mainMessage.Text = "�A�C���F������肪����Ǝv������ł�Ɖ����˂����Ď����H";
                                        ok.ShowDialog();
                                        mainMessage.Text = "���i�F����ȃg�R����Ȃ�������B�b�t�t�A���i�l�̃_���W�����}�b�v���������ɗ��������ăR�g��B";
                                        ok.ShowDialog();
                                        mainMessage.Text = "�A�C���F�����A���ꂪ���������炱���A���U�N���A�������킯�����ȁB���ӂ��Ă邺�A�z���g�B";
                                        ok.ShowDialog();
                                        mainMessage.Text = "���i�F���Ⴀ�A�Ō�̋��֐i�݂܂���A���悢�惉�X�g�ˁ�";
                                        ok.ShowDialog();
                                        mainMessage.Text = "�A�C���F��������A���X�g���T�N�T�N�������I";
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
                                            mainMessage.Text = "�@�@�@�@�w���̐�A�s���~�܂�B�@�Ō�̋��֐i�ނׂ��B�x";
                                            mainMessage.Update();
                                        }
                                        else
                                        {
                                            mainMessage.Text = "�Ŕ͂��������Ȃ��Ă���B";
                                            mainMessage.Update();
                                        }
                                    }
                                    return;
                                    #endregion

                                case 28:
                                    #region "�Ŕ˔j�C�x���g 2-6"
                                    if (!we.SolveArea26)
                                    {
                                        if (!we.InfoArea26)
                                        {
                                            mainMessage.Text = "�A�C���F�����A���ꂪ���X�g�Ŕ��ȁE�E�E�ȂɂȂɁH";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�@�@�@�@�w���̐�A�s���~�܂�B�x";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F���ȁE�E�E�ǂ���������B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F���������E�E�E�s���~�܂�Ƃ��������Ă˂����H";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�E�E�E�q���g��������Ȃ��B�ǂ�����̂�R���B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�܂������ȁB��̑ł��悤���Ȃ����������B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�T����B�����Ɖ����q���g�����锤�ł���B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�������ȁA�܂��͎��������Ă݂�Ƃ��邩�B";
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
                                                    mainMessage.Text = "�@�@�@�@�w���̐�A�s���~�܂�B�x";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F�ʖڂ��B�����Ă݂��������˂����B";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F��k�ł���A�{���ɍs���~�܂肾���Č����́H";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F�E�E�E�킩��˂��B";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F�E�E�E�E�E�E�~�Q��B";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F�����A�~�Q���Ȃ������ɁB�ǂ�����A��U���֖߂邩�H";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F�ق���ƁA��ꂽ�킳�����ɁB��U�߂�܂��傤�B";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F��U���ɖ߂�����A�Ŕ��ς���Ă��肷��Ɨǂ��ȁB";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F����͖�����ˁB�ŏ��Ɠ����d�|���Ƃ͎v���Ȃ���B";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F���Ⴀ�A�܂���U�߂邩�I�y�����̐����z���g�������B";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F�����E�E�E�������܂���B";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "    �w�A�C���B�́A�����̐�����`�����񂾁B�����O�̒��ւƃ��[�v����x";
                                                    mainMessage.Update();
                                                    ok.ShowDialog();
                                                    we.FailArea26 = true;
                                                    CallHomeTown();
                                                }
                                                else
                                                {
                                                    mainMessage.Text = "�@�@�@�@�w���̐�A�s���~�܂�B�x";
                                                }
                                            }
                                            else
                                            {
                                                mainMessage.Text = "�@�@�@�@�w���̐�A�s���~�܂�B�x";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (!we.ProgressArea26)
                                        {
                                            mainMessage.Text = "�@�@�@�@�w���̐�A�s���~�܂�B�x";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F���ĂƁA�������炾�ȁB";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�A�C���A�{���ɉ𓚂�����́H";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�킩��˂���B�ł��A������x���O�ɒT���Ă݂悤���B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�ł��A�����������Ȃ�������H";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F����Ƃ��͂�����x�A���܂Ȃ��T���܂ł��B�C���Ƃ����āI";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�E�E�E����A����������B����A�蕪�����ĒT���Ă݂܂����";
                                            ok.ShowDialog();
                                            we.ProgressArea26 = true;
                                        }
                                        else
                                        {
                                            if (we.ProgressArea261 && we.ProgressArea262 && we.ProgressArea263 && !we.FailArea261)
                                            {
                                                mainMessage.Text = "�@�@�@�@�w���̐�A�s���~�܂�B�x";
                                                ok.ShowDialog();
                                                mainMessage.Text = "���i�F�ʖڂˁB����ς艽���Ȃ���B";
                                                ok.ShowDialog();
                                                mainMessage.Text = "�A�C���F�E�E�E����A�����ł��˂��B";
                                                ok.ShowDialog();
                                                mainMessage.Text = "���i�F���H�������������́H";
                                                ok.ShowDialog();
                                                mainMessage.Text = "�A�C���F����A�܂������͏o�˂��B������񂾁B�T���𑱂��悤���B";
                                                ok.ShowDialog();
                                                mainMessage.Text = "���i�F�����A�ǂ����B�A�C���������Ă����Ȃ玄�͎~�߂Ȃ���B";
                                                ok.ShowDialog();
                                                mainMessage.Text = "�A�C���F�����A�C���Ƃ��ȁI";
                                                ok.ShowDialog();
                                                we.FailArea261 = true;
                                            }
                                            else
                                            {
                                                if (we.ProgressArea264 && we.ProgressArea265 && we.ProgressArea266 && we.ProgressArea267 && !we.FailArea262)
                                                {
                                                    mainMessage.Text = "�@�@�@�@�w���̐�A�s���~�܂�B�x";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F�A�C���A�ǂ��B���������肻���H";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F����A�܂��ʖڂ��B�킩��˂��܂܂��B";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "���i�F�A�C���A�撣���ĂˁB���A�������邮�炢�����ł��Ȃ������������B";
                                                    ok.ShowDialog();
                                                    mainMessage.Text = "�A�C���F�������E�E�E������񂾁B�T���𑱂邺�B";
                                                    ok.ShowDialog();
                                                    we.FailArea262 = true;
                                                }
                                                else
                                                {
                                                    if (we.ProgressArea268 && we.ProgressArea269 && we.ProgressArea2610 && we.ProgressArea2611 && !we.FailArea263)
                                                    {
                                                        mainMessage.Text = "�@�@�@�@�w���̐�A�s���~�܂�B�x";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F�A�C���A���ė��ĂȂ��H���т�������B���A�����̐����Ŗ߂낤���H";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F�ʖڂ��E�E�E�P��߂�����Y�ꂿ�܂��B�����̏�A���̏u�Ԃŉ��������˂��B";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "���i�F����������A�A�C���B�����P��T������̂ˁB";
                                                        ok.ShowDialog();
                                                        mainMessage.Text = "�A�C���F�����A������񂾁B���x�ł��T���𑱂��邺�B";
                                                        ok.ShowDialog();
                                                        we.FailArea263 = true;
                                                    }
                                                    else
                                                    {
                                                        if (we.ProgressArea2612 && we.ProgressArea2613 && we.ProgressArea2614 && we.ProgressArea2615 && !we.FailArea264)
                                                        {
                                                            mainMessage.Text = "�@�@�@�@�w���̐�A�s���~�܂�B�x";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "���i�F�A�C���E�E�E���v�H";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "�A�C���F���������I���������ȁE�E�E�ǂꂪ�z���g�Ȃ񂾂�E�E�E�����A�ق��Ă�I�I";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "���i�F�A�C���A�ۂ܂ꂿ��ʖڂ�B�����T�ɋ����B�C����������B";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "�A�C���F���A�����A���܂˂��B�q���g�������Ȃ�āA��k����˂����B";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "���i�F�ǂ������Ӗ���H";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "�A�C���F�����E�E�E�q���g���炯�����B�ǂ̌��ԁA�����̏����A�V��̑}�G�A��Ԃɓ�������̍��o�B";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "���i�F�E�\�A���E�E�E�S�R�C�t���Ȃ���B";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "�A�C���F�������蕷�������肵�ĂȂ��̂��H���ɂ͎��R�ɖڂƎ��ɒ��ړ����Ă��邺�B";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "���i�F������A�ǂ��̂�B�A�C�������Ɍ������蕷�������肵�Ă�̂�������Ȃ���B";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "�A�C���F�܂��ǂ��B��͒������B�S�Ăɂ�����A���A�S�Ăɂ�����s�A���ɑ΂�������o���オ��B";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "���i�F����E�E�E�撣���āB�w���Ȃ��ƌ����Ă邵�A�ǂ����Ă���ꂽ��܂��������Ă�ˁB";
                                                            ok.ShowDialog();
                                                            mainMessage.Text = "�A�C���F�����A�����֍s�����B�����ŉ��͓����������B�ł���C������񂾁B";
                                                            ok.ShowDialog();
                                                            we.FailArea264 = true;
                                                        }
                                                        else
                                                        {
                                                            if (!we.ProgressArea2616)
                                                            {
                                                                mainMessage.Text = "�@�@�@�@�w���̐�A�s���~�܂�B�x";
                                                            }
                                                            else
                                                            {
                                                                mainMessage.Text = "�Ŕ͂��������Ȃ��Ă���B";
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
                                        mainMessage.Text = "�A�C���F�E�E�E";
                                        ok.ShowDialog();
                                        we.ProgressArea261 = true;
                                    }
                                    return;

                                case 30:
                                    if (!we.ProgressArea262 &&
                                        !we.FailArea261 && we.SolveArea26)
                                    {
                                        mainMessage.Text = "�A�C���F�E�E�E";
                                        ok.ShowDialog();
                                        we.ProgressArea262 = true;
                                    }
                                    return;
                                    
                                case 31:
                                    if (!we.ProgressArea263 &&
                                        !we.FailArea261 && we.SolveArea26)
                                    {
                                        mainMessage.Text = "�A�C���F�E�E�E";
                                        ok.ShowDialog();
                                        we.ProgressArea263 = true;
                                    }
                                    return;

                                case 32:
                                    if (we.ProgressArea261 && we.ProgressArea262 && we.ProgressArea263 &&
                                        !we.ProgressArea264 && we.FailArea261 && !we.FailArea262 && we.SolveArea26)
                                    {
                                        mainMessage.Text = "�A�C���F�E�E�E";
                                        ok.ShowDialog();
                                        we.ProgressArea264 = true;
                                    }
                                    return;

                                case 33:
                                    if (we.ProgressArea261 && we.ProgressArea262 && we.ProgressArea263 &&
                                        !we.ProgressArea265 && we.FailArea261 && !we.FailArea262 && we.SolveArea26)
                                    {
                                        mainMessage.Text = "�A�C���F�E�E�E";
                                        ok.ShowDialog();
                                        we.ProgressArea265 = true;
                                    }
                                    return;

                                case 34:
                                    if (we.ProgressArea261 && we.ProgressArea262 && we.ProgressArea263 &&
                                        !we.ProgressArea266 && we.FailArea261 && !we.FailArea262 && we.SolveArea26)
                                    {
                                        mainMessage.Text = "�A�C���F�E�E�E";
                                        ok.ShowDialog();
                                        we.ProgressArea266 = true;
                                    }
                                    return;

                                case 35:
                                    if (we.ProgressArea261 && we.ProgressArea262 && we.ProgressArea263 &&
                                        !we.ProgressArea267 && we.FailArea261 && !we.FailArea262 && we.SolveArea26)
                                    {
                                        mainMessage.Text = "�A�C���F�E�E�E";
                                        ok.ShowDialog();
                                        we.ProgressArea267 = true;
                                    }
                                    return;

                                case 36:
                                    if (we.ProgressArea261 && we.ProgressArea262 && we.ProgressArea263 &&
                                        we.ProgressArea264 && we.ProgressArea265 && we.ProgressArea266 && we.ProgressArea267 &&
                                        !we.ProgressArea268 && we.FailArea262 && !we.FailArea263 && we.SolveArea26)
                                    {
                                        mainMessage.Text = "�A�C���F�E�E�E";
                                        ok.ShowDialog();
                                        we.ProgressArea268 = true;
                                    }
                                    return;

                                case 37:
                                    if (we.ProgressArea261 && we.ProgressArea262 && we.ProgressArea263 &&
                                        we.ProgressArea264 && we.ProgressArea265 && we.ProgressArea266 && we.ProgressArea267 &&
                                        !we.ProgressArea269 && we.FailArea262 && !we.FailArea263 && we.SolveArea26)
                                    {
                                        mainMessage.Text = "�A�C���F�E�E�E";
                                        ok.ShowDialog();
                                        we.ProgressArea269 = true;
                                    }                                   
                                    return;

                                case 38:
                                    if (we.ProgressArea261 && we.ProgressArea262 && we.ProgressArea263 &&
                                        we.ProgressArea264 && we.ProgressArea265 && we.ProgressArea266 && we.ProgressArea267 &&
                                        !we.ProgressArea2610 && we.FailArea262 && !we.FailArea263 && we.SolveArea26)
                                    {
                                        mainMessage.Text = "�A�C���F�E�E�E";
                                        ok.ShowDialog();
                                        we.ProgressArea2610 = true;
                                    }
                                    return;

                                case 39:
                                    if (we.ProgressArea261 && we.ProgressArea262 && we.ProgressArea263 && 
                                        we.ProgressArea264 && we.ProgressArea265 && we.ProgressArea266 && we.ProgressArea267 &&
                                        !we.ProgressArea2611 && we.FailArea262 && !we.FailArea263 && we.SolveArea26)
                                    {
                                        mainMessage.Text = "�A�C���F�E�E�E";
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
                                        mainMessage.Text = "�A�C���F�E�E�E";
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
                                        mainMessage.Text = "�A�C���F�E�E�E";
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
                                        mainMessage.Text = "�A�C���F�E�E�E";
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
                                        mainMessage.Text = "�A�C���F�E�E�E";
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

                                            mainMessage.Text = "�@�@�@�@�w��Ύ����F���A�����������B�x";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�A�C���E�E�E�撣���āE�E�E";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�C���Ƃ��B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�@�@�@�@�w�˔@������A�C���̑O�ɕ����オ�����B����A�����F����ԂɃA�C������܂ꂽ�B�x";
                                            ok.ShowDialog();

                                            GroundOne.PlayDungeonMusic(Database.BGM09, Database.BGM09LoopBegin);
                                            
                                            mainMessage.Text = "�A�C���F�y��@�����̂��A�؁X�������n�߂�z";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�y��@�V�͐��Ƃ炵�A�n�͐V�΂�搉̂���z";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�y�Q�@�ł���ւƗU���A�������ւƓ��������z";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�y�l�@���A���ꗎ���A�̑�Ȃ�C�A�V�ւƊ҂�A�����z�z";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�y�܁@�΁A������ꏊ�A�\�ȏ�𐶂߂����A�n���򉻁z";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�@�@�@�@�w�A�C������ł����Ԃ��Z���ԐF�ւƌ������ς�����I�I�x";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�A�C���E�E�E���v�E�E�E���v��ˁE�E�E";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�y�Z�@���A�����Ȃ鐶���v�f�A�ꂩ���ւƕω�������z";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�y���@���A���̐��ɂ������ΓI�ȕ����̏ے��z";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�y���@���A�̑�Ȃ��A���i�Ȃ镃���i�v�̊m��z";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�y��@�_�A�S�n���A�S�@���A�S�ɂ��Ė������̑��݁z";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�y�\�@�l�A���A����A�����A�����A�����A�h�炬�����鑶�݁z";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�@�@�@�@�w�A�C������ł����Ԃ������F�ւƌ������ς�����I�I�x";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F������ƁE�E�E���������Ȃ���E�E�E���ȂȂ��ł�E�E�E";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�y�\��@���A�_�Ɛl�A���A�؁X�A�S�����ɂ�����A���̗������Ɍ�������z";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�y�\��@��A�݂�ׂ����́A����ׂ����Đ���A�݂�ׂ����Č����ƌ�������z";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�y�\�Q�@���A���S���a�ւ̓����A����邱�Ƃ̖�����Αo�ɁA��������z";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�@�@�@�@�w�A�C������ł����Ԃ��^�����ȏ����F�ւƌ������ς�����I�I�x";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�E�E�E�܂�����́H�����E�E�E�����I��点�Ă����Ă�E�E�E�I";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�y�\�l�@�i���A�I���Ȃ����ցA�I���Ǝn�܂肪�A������i���z";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�y�I�Ɂ@���E�A���Ȃ��A�����Ă킽���������ꏊ�ցB�����ɑ������̐��E�z";
                                            ok.ShowDialog();

                                            GroundOne.StopDungeonMusic();

                                            mainMessage.Text = "�@�@�@�@�w��Ԃ��������t���b�V�����A�Ïk���ꂽ��ԂւƘA���I�ɏ������Ȃ�I�I�I�x";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�@�@�@�@�w�p�p�p�p�p�p�p�I�I�I�I�b�o�V���E�E�E�D�D�D���I�I�I�I�I�I�I�x";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�L���A�A�A�A�A�I�I�I�I";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�@�@�@�@�w��Ԃ͒e����񂾌�A����̑O�ɃA�C���̓|�ꂽ�p���������x";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�E�E�E�����E�E�E";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�A�C���I�I���v�Ȃ́I�H�������肵�Ă�I�I�I";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�E�E�E���E�E�E�����A�����A���v���B�����ĂĂĂāE�E�E";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�A�C���I�I���񂽁A�o�J����Ȃ��́I�H�ςȂ��Ƃ΂����肢���Ă��E�E�E";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F���A���₢��A���̕ӂɂ������@���ɏ]���Ă��ȁE�E�E";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�o�J����Ȃ��́I�H���񂾂�ǂ�����̂�I�H�����@����I�o�J����Ȃ��́I�I�o�J�I�I�I";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�o�J�ł���I�H����ς�o�J��I�I�o�J�o�J�o�J�I�I�I";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F���Ă��I�����Ă����āI�@���Ȃ��āB�����Ă��Ȃ��E�E�E�b�n�n�n";
                                            ok.ShowDialog();
                                            using (MessageDisplay md = new MessageDisplay())
                                            {
                                                md.StartPosition = FormStartPosition.CenterParent;
                                                md.Message = "�A�C���ƃ��i�͂��΂炭���̏�ŋx����������B";
                                                md.ShowDialog();
                                            }
                                            we.ProgressArea2616 = true;
                                            we.CompleteArea26 = true;
                                            ConstructDungeonMap();
                                            SetupDungeonMapping(2);
                                            mainMessage.Text = "���i�F�E�E�E���āA�����J���Ă����E�E�E";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�������A������B�b�n�b�n�b�n�B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�z���g�A�����������������Ă��̂�H���̕����ŁB";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�̂��B�̂��������ė����񂾂�A�ǂ̒��⏰���̏���������邽�тɁB";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�́H�ւ��A�ǂ�ȉ̂������́H";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�S�R���������������ǂȁB�s�v�c�Ɛ̂���m���Ă�悤�Ȋ������������B�������Y�킾�����B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F������q����ƁA����ȃw���e�R�Ȍ��t�ɂȂ������Ă����킯�H";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�����ȏ��A���̌��t�͉��������Ă��񂶂�˂��B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�������Ă�̂�A�ԈႢ�Ȃ��A�C���̐���������B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F����A�����Ă�̂͊m���ɉ����B�����A���͂���Ȍ��t�͖a���o���˂��B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�N�����A�C���Ɍ��킹�����Ă����́H";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F���������킯�ł��˂��B�B�B���܂˂��A�z���g�ɂ悭�킩��˂��񂾁B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F��܂��A�ǂ���B�Ƃɂ������߂łƂ��B";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�����I�Y��Ă���I�I";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F�����A������I�H�}�ɗ����オ���āB�B�B�����I���܂������I�I";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���ƃ��i�F�Ђ���Ƃ��āA���̐�{�X�������i����˂����j�i����Ȃ��́j�I�H";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F����A���v���B���̉��B�Ȃ����B��������H";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�����ˁA�{�X�Ȃ񂩂����������Ɠ|�����Ⴂ�܂����";
                                            ok.ShowDialog();
                                            mainMessage.Text = "�A�C���F��������A�x�ނ̂͂����I��肾�B���̂Q�K���e���邺�I�I";
                                            ok.ShowDialog();
                                            mainMessage.Text = "���i�F�����A�s���܂��傤�B�K�i�͂��������\�R���";
                                            ok.ShowDialog();
                                            GroundOne.PlayDungeonMusic(Database.BGM02, Database.BGM02LoopBegin);
                                        }
                                        else
                                        {
                                            mainMessage.Text = "����ɂ͂���������Ă���B�@�w���A�����g�̓����A��������B���֐i�߁B�x";
                                        }
                                    }
                                    return;
                                    #endregion

                                case 45:
                                    if (!we.InfoArea27)
                                    {
                                        UpdateMainMessage("�A�C���F�������ƂƁI�ȁA�Ȃ񂾃R�R�́I");

                                        UpdateMainMessage("���i�F�A�C���I�H������Ɖ�����Ă�́I�H�ǂɂ߂荞�񂶂�������P�I�H");

                                        UpdateMainMessage("�A�C���F�ǂ̐F�����ɑ��ƈ���Ă����炳�B�����ɐG�낤�Ƃ�����A���蔲�������B");

                                        UpdateMainMessage("���i�F��A�������蔲����邩����H");

                                        UpdateMainMessage("�A�C���F�����A���v����˂����H�P�Ȃ錩�������̕ǂ��B����������B");

                                        UpdateMainMessage("���i�F���Ⴀ�s���Ă݂��ˁE�E�E");

                                        UpdateMainMessage("���i�F������ƁE�E�E�z���g�A������ꂽ���");

                                        UpdateMainMessage("�A�C���F�Ȃ��A���̐悫���ƃX�Q�G���󂪂���ɈႢ�Ȃ����I�I");

                                        UpdateMainMessage("���i�F�Ȃɂ���ȃo�J�݂����ɂ͂��Ⴂ�ł�̂�B�����A�s�����B");

                                        UpdateMainMessage("�A�C���F�����󂨕�I�I");

                                        we.InfoArea27 = true;
                                    }
                                    return;

                                case 46:
                                    if (!we.SpecialInfo2)
                                    {
                                        UpdateMainMessage("�A�C���F�����E�E�E�s���~�܂肩�E�E�E�������������̂���B");

                                        GroundOne.StopDungeonMusic();
                                        this.BackColor = Color.Black;
                                        UpdateMainMessage("�@�@�@�y���̏u�ԁA�A�C���̔]���Ɍ��������ɂ��P�����I���͂̊��o����Ⴢ���I�I�z");

                                        UpdateMainMessage("�A�C���F�b�O�A�I�I�E�E�E���A�����E�E�E�I�I");

                                        UpdateMainMessage("�@�@�������@�ǂ��ŗ��Ƃ����񂾁H ������");

                                        UpdateMainMessage("�A�C���F�b�c�c�c�E�E���Ƃ������āE�E�E�ȁE�E�E�ɁE�E�E���E�E�E");

                                        UpdateMainMessage("�@�@�������@���܂ꂽ�̂��Ƃ�����A�ǂ����H�@������");

                                        UpdateMainMessage("�A�C���F���܂�E�E�E�����E�E�E�O�A�O�A�A�I�I�I");

                                        UpdateMainMessage("�@�@�������@�Q�K����R�K�֍s�����������Ƃ�����H�@������");

                                        UpdateMainMessage("�A�C���F���A�m�邩�E�E�E���āE�E�E");

                                        UpdateMainMessage("�@�@�������@�N���A�ƏI���ɂ͌������ȁB���ƃL�[��������B�@������");

                                        UpdateMainMessage("�A�C���F���H�E�E�E�E�ǁA�ǂ������E�E�E");

                                        UpdateMainMessage("�@�@�@�y�A�C���ɑ΂��錃�������ɂ͏����������Ă������B�z");

                                        this.BackColor = Color.RoyalBlue;

                                        UpdateMainMessage("���i�F�A�C���E�E�E�A�C���I�I");

                                        UpdateMainMessage("�A�C���F�E�E�E��A���E�E�E�悤");

                                        UpdateMainMessage("���i�F�A���^���ł���ȏ��œ|��Ă�̂�H");

                                        UpdateMainMessage("�A�C���F����A�|��Ă��킯����˂��B�����������Ă����񂾁B");

                                        UpdateMainMessage("���i�F���ۓ|��Ă��̂ɁA�������Ă�񂾂��B���𕷂����̂�H");

                                        UpdateMainMessage("�A�C���F�����������ȁE�E�E�u���܂ꂽ�̂͂ǂ����v�Ƃ�");

                                        UpdateMainMessage("�A�C���F�N���A�Ɍ������ȁA���ƁE�E�E�b�c�I�E�E�E�C�b�e�e�e�E�E�E");

                                        UpdateMainMessage("�A�C���F�����������󂾂Ǝv�����̂ɁA���������q���������B");

                                        UpdateMainMessage("���i�F�܂��K���Q���������������A�߂�܂���B");

                                        UpdateMainMessage("�A�C���F�����E�E�E����E�E�E");

                                        GroundOne.PlayDungeonMusic(Database.BGM02, Database.BGM02LoopBegin);
                                        we.SpecialInfo2 = true;
                                    }
                                    return;

                                default:
                                    mainMessage.Text = "�A�C���F��H���ɉ����Ȃ������Ǝv�����B";
                                    return;
                            }
                        }
                    }
                }
                #endregion
                #region "�_���W�����R�K�C�x���g"
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
                                    mainMessage.Text = "�A�C���F�Q�K�֖߂�K�i���ȁB�����͈�U�߂邩�H";
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
                                        mainMessage.Text = "�A�C���F�Ŕ�����ȁE�E�E�ȂɂȂɁH";
                                        ok.ShowDialog();
                                        mainMessage.Text = "�@�@�@�@�w�^���̌��t�R�F�@�@�~���鎖�A����鎖�x";
                                        ok.ShowDialog();
                                        mainMessage.Text = "�A�C���F�~����H�N�̂��Ƃ������Ă񂾁A�R���́B";
                                        we.TruthWord3 = true;
                                    }
                                    else
                                    {
                                        UpdateMainMessage("�@�@�@�@�w�^���̌��t�R�F�@�@�~���鎖�A����鎖�x");
                                    }
                                    return;
                                case 2:
                                    mainMessage.Text = "�A�C���F�������S�K�ւ̊K�i�I�~��Ă݂悤���H";
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
                                                UpdateMainMessage("�A�C���F�R�K���e�������ƁI���i�A���F���[�A�����O�̒��ֈ��A�҂��Ă��������B");

                                                UpdateMainMessage("���F���[�F�����A�{�N�͎^���ł���B");

                                                UpdateMainMessage("���i�F���Ⴀ�����߂�܂���B");

                                                UpdateMainMessage("�A�C���F�����A���Ⴀ���̉����̐����ŁI");
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
                                    mainMessage.Text = "�A�C���F�{�X�Ƃ̐퓬���I�C���������߂Ă������I";
                                    mainMessage.Update();
                                    bool result = EncountBattle("�O�K�̎��ҁFMinflore");
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
                                    we.Treasure8 = GetTreasure("���b�h�}�e���A��");
                                    return;

                                case 5:
                                    we.Treasure9 = GetTreasure("���C�I���n�[�g");
                                    return;

                                case 6:
                                    we.Treasure10 = GetTreasure("�I�[�K�̘r��");
                                    return;

                                case 7:
                                    we.Treasure11 = GetTreasure("�|�S�̐Α�");
                                    return;

                                case 8:
                                    we.Treasure12 = GetTreasure("�t�@���l�M�̃V�[��");
                                    return;

                                case 9:
                                    #region "���֖�"
                                    if (!we.InfoArea31)
                                    {
                                        UpdateMainMessage("�A�C���F�����I�����Ȃ�Ŕ����邺�B�܂�����͊��ق��ȁE�E�E");

                                        UpdateMainMessage("���F���[�F�u�܂��v�Ƃ́A�ǂ������Ӗ��ł��H");

                                        UpdateMainMessage("���i�F�Q�K�ł͗��đ����ɓ䂩���Ŕ��o�Ă��āA����ɉ𓚂��������������ł���B");

                                        UpdateMainMessage("���F���[�F�Ȃ�قǁA�ł����̊Ŕ͎���Ƃ����킯�ł͖��������ł���B");

                                        UpdateMainMessage("�@�@�@�@�w�����狾�ցB�������܂܂ɐG���B�������܂܂ɐi�߁B�x");

                                        UpdateMainMessage("�A�C���F�����狾�H�ق猩��E�E�E����ϓ䂩������˂����B");

                                        UpdateMainMessage("���i�F�������܂܂ɐG��Đi�߂Ƃ������Ă����B�m���Ɏ���ł͂Ȃ������ˁB");

                                        UpdateMainMessage("���F���[�F�����͖{���ɖʔ����ł��ˁB�{�N���������ƑS�R���e���Ⴂ�܂��B");

                                        UpdateMainMessage("���i�F�����A�����Ȃ�ł����H");

                                        UpdateMainMessage("���F���[�F���̃_���W�����́A�����o�[�A�p�[�e�B�\���ɂ���ē��e���ς���ł���");

                                        UpdateMainMessage("�A�C���F�m���ɂ��������ȁB�����ĉ���l�ōŏ����������͓�����Ȃ�Ė����������B");

                                        UpdateMainMessage("���i�F�����A�A�C�����o�J������P�l�œ�����Ȃ�Ă�������A�[�b�^�C�����Ȃ�����");

                                        UpdateMainMessage("�A�C���F�b�`�E�E�E�o�J�̓o�J�Ȃ�ɍl���Ă񂾂�B��������炢���Ƃ��˂����ẮB");

                                        UpdateMainMessage("���i�F�u�o�J�̓o�J�Ȃ�Ɂv���āE�E�E�܂��ǂ���B����͓��������Ȃ����������ˁB");

                                        UpdateMainMessage("���F���[�F���āA�i�݂܂��傤�B����T���Ηǂ��Ƃ������ł��傤�B");

                                        UpdateMainMessage("�A�C���F�����A���Ⴀ���������T�����ċ���T���čs�������I");
                                        we.InfoArea31 = true;
                                    }
                                    else
                                    {
                                        if (!we.CompleteArea34)
                                        {
                                            UpdateMainMessage("�@�@�@�@�w�����狾�ցB�������܂܂ɐG���B�������܂܂ɐi�߁B�x", true);
                                        }
                                        else
                                        {
                                            UpdateMainMessage("�Ŕ͂��������Ȃ��Ă���B", true);
                                        }
                                    }
                                    return;

                                case 10:
                                    if (!we.InfoArea311S)
                                    {
                                        if (!we.InfoArea312S && !we.InfoArea313S)
                                        {
                                            UpdateMainMessage("���i�F�����A������ƃA�C���B");

                                            UpdateMainMessage("�A�C���F��H�����H");

                                            UpdateMainMessage("���i�F���������A�z�������ɋ��B");

                                            UpdateMainMessage("�A�C���F�H�E�E�E�����A�z���g���B�悭�������ȃ��i�B");

                                            UpdateMainMessage("���F���[�F���A������́I�I");

                                            UpdateMainMessage("�A�C���F��H���F���[�ǂ��������̂��H");

                                            UpdateMainMessage("���F���[�F���̌`�A�t�@�[�W���{�a���ɂ���y���̎O�ʋ��z�ɂ�������ł��B");

                                            UpdateMainMessage("���i�F���̎O�ʋ����ĕ��������Ƃ������B�{�l�̑z�����v��Ƃ��B");

                                            UpdateMainMessage("���F���[�F���̂Ƃ���B�{�l�������A�v�l�E���f�𐮗����������ɂ悭�g���Ă��܂��B");

                                            UpdateMainMessage("�A�C���F�悵�A�ł͑����E�E�E������܂܂ɔ`���Ă݂邩�B");

                                            UpdateMainMessage("�A�C���F�E�E�E�����N���Ȃ����B�����̋�����˂����H");

                                            UpdateMainMessage("���i�F��������A�Y��ł�l����Ȃ��ƑʖڂȂ񂶂�Ȃ��H�A�C�����ጳ�X������B");

                                            UpdateMainMessage("�A�C���F���i�A�������Ď��Ƃ��ĔY�񂾂肷��񂾁B");

                                            UpdateMainMessage("���i�F���������������Ă�Ԃ́A�Y��ł邤���ɓ���Ȃ����B");

                                            UpdateMainMessage("���F���[�F�E�E�E�{�N�ł��ʖڂ݂����ł��ˁB���݂܂��񂪉����N���܂���B");

                                            UpdateMainMessage("���i�F���F���[����͐�������Ă��ł����@��z���Ă�l���ʖڂ݂����B");

                                            UpdateMainMessage("�A�C���F�ǂ����Ă����ŕ]�����܂������t�ɂȂ��Ă񂾂�B");

                                            UpdateMainMessage("�A�C���F���ƁA���Ⴀ�c��̓��i�A���܂������H");

                                            UpdateMainMessage("���i�F�E�E�E������H");

                                            UpdateMainMessage("�A�C���F���A���F���[�Ƃ���Ό�͂��O��������B�����A�u������H�v�Ȃ񂾂�B");

                                            UpdateMainMessage("���i�F�E�E�E���A���A�������ƂˁB");

                                            UpdateMainMessage("�A�C���F���˘f���Ă񂾁B�ʖڌ��ŗǂ��������Ă݂Ă����H");

                                            UpdateMainMessage("���F���[�F�A�C���N�A���̎q�ɂ��������������͎���ł���B");

                                            UpdateMainMessage("���i�F���A���F���[����A�ǂ���ł��B����A����Ă݂���B");

                                            UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");

                                            UpdateMainMessage("�A�C���F������A�܁Aῂ��I�I");

                                            UpdatePlayerLocationInfo(basePosX + moveLen * 5, basePosY + moveLen * 7);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");

                                            UpdateMainMessage("�A�C���F�E�E�E��H���A���������Ȃ��Ă邼�B");

                                            UpdateMainMessage("���F���[�F����A���������Ȃ����킯����Ȃ������ł��B");

                                            UpdateMainMessage("���i�F�_���W�����ǂ̍\�����Ⴄ�E�E�E�ǂ����Ⴄ�ꏊ�ɗ����̂ˁB");

                                            UpdateMainMessage("�A�C���F��΂��ꂽ���Ď����I�H");

                                            UpdateMainMessage("���F���[�F�ǂ���炻�̂悤�ł��ˁB���̃��[�v���u�݂����Ȃ��̂ł��傤���B");

                                            UpdateMainMessage("�A�C���F�������傤�A���ꂶ�჉�i�̃_���W�����}�b�v���������ɗ����Ȃ��񂶂�Ȃ����H");

                                            UpdateMainMessage("���i�F�����ꏊ�ɔ�΂����킯����Ȃ�������ɗ����Ȃ����P����Ȃ����ǁB");

                                            UpdateMainMessage("���i�F�ł��A���̂�ɐi�߂Ė��߂Ă������@���ʗp���Ȃ���ˁB");

                                            UpdateMainMessage("�A�C���F���ȁB�ł��i�ނ������͂˂��ȁB");

                                            UpdateMainMessage("���F���[�F�K�������͈�{���Ɍ����܂��B�s���܂��傤�B");
                                        }
                                        else
                                        {
                                            UpdateMainMessage("���i�F�����A�����ɋ����ݒu����Ă��ˁB");

                                            UpdateMainMessage("���F���[�F���i����A�������肢���܂��B");

                                            UpdateMainMessage("�A�C���F�ʂ̏�������ȁB�ꉞ�x�����Ȃ���i�������B");

                                            UpdateMainMessage("���i�F�����A����s�����B");

                                            UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");

                                            UpdatePlayerLocationInfo(basePosX + moveLen * 5, basePosY + moveLen * 7);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                        }

                                        we.InfoArea311S = true;
                                    }
                                    else
                                    {
                                        if (!we.CompleteArea32)
                                        {
                                            UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                            UpdatePlayerLocationInfo(basePosX + moveLen * 5, basePosY + moveLen * 7);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                        }
                                        else
                                        {
                                            if (!we.CompleteArea34)
                                            {
                                                UpdateMainMessage("���i�F�����͈�x�˔j���Ă����B�ǂ�����A�܂������Ă݂�H");

                                                using (YesNoRequestMini yesno = new YesNoRequestMini())
                                                {
                                                    yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                                    yesno.ShowDialog();
                                                    if (yesno.DialogResult == DialogResult.Yes)
                                                    {
                                                        UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                                        UpdatePlayerLocationInfo(basePosX + moveLen * 5, basePosY + moveLen * 7);
                                                        SetupDungeonMapping(3, false);
                                                        UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
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
                                                    UpdateMainMessage("���i�F���������A����̏��ŋ����Ă�������s����Ȃ񂾂��ǁB");

                                                    UpdateMainMessage("�A�C���F��A�Ȃ񂾁H");

                                                    UpdateMainMessage("���i�F���̋�����ł��s����悤�ɂȂ����݂����ˁ�");

                                                    UpdateMainMessage("�A�C���F�����A��邶��˂����I�_�l���̂Ă����񂶂�˂��ȁI");

                                                    UpdateMainMessage("���i�F�ǂ�����̂�B���ڍs���H");
                                                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                                                    {
                                                        yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                                        yesno.ShowDialog();
                                                        if (yesno.DialogResult == DialogResult.Yes)
                                                        {
                                                            UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                                            UpdatePlayerLocationInfo(basePosX + moveLen * 3, basePosY + moveLen * 9);
                                                            SetupDungeonMapping(3, false);
                                                            UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                                        }
                                                        else
                                                        {
                                                            UpdateMainMessage("", true);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    UpdateMainMessage("���i�F�Ō�̕����֒��ڍs���H");
                                                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                                                    {
                                                        yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                                        yesno.ShowDialog();
                                                        if (yesno.DialogResult == DialogResult.Yes)
                                                        {
                                                            UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                                            UpdatePlayerLocationInfo(basePosX + moveLen * 3, basePosY + moveLen * 9);
                                                            SetupDungeonMapping(3, false);
                                                            UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                                        }
                                                        else
                                                        {
                                                            UpdateMainMessage("���i�F�ŏ����[�v���Ă����n�_�֓����Ă݂�H");

                                                            yesno.ShowDialog();
                                                            if (yesno.DialogResult == DialogResult.Yes)
                                                            {
                                                                UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                                                UpdatePlayerLocationInfo(basePosX + moveLen * 5, basePosY + moveLen * 7);
                                                                SetupDungeonMapping(3, false);
                                                                UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
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
                                            UpdateMainMessage("���i�F�������A�����ɂ������ݒu����Ă����B");

                                            UpdateMainMessage("�A�C���F�������A������̂������ȃ��i�B�����Č���Ε����邪�B");

                                            UpdateMainMessage("���F���[�F�e�͈Ⴆ�ǁA�{�N�B�̎����B�ꏗ���ł���t�@����������̂͑��������ł��B");

                                            UpdateMainMessage("�A�C���F�t�@���E�E�E�t�@�����܂̎��ł����H");

                                            UpdateMainMessage("���F���[�F�����A�t�@���������������l�ł����B");

                                            UpdateMainMessage("���F���[�F�F�������Ƃ������ȃ��m��ʂ�߂����A�C�t���A�E���~���悤�ɁA�����Ă������ł��B");

                                            UpdateMainMessage("���i�F���A���������΁A���F���[����ƃt�@���l���ē����N�Ȃ�ł����H");

                                            UpdateMainMessage("���F���[�F�����ł���BFiveSeeker�̂T�l�͊F�����N�ł��B");

                                            UpdateMainMessage("���i�F���A���[�����Ȃ񂾁I�H������ƃr�b�N���E�E�E");

                                            UpdateMainMessage("�A�C���F�����f�B�̃{�P�������N���Ď����H");

                                            UpdateMainMessage("���F���[�F�͂��A�c�O�Ȃ��炻���������ɂȂ�܂��B");

                                            UpdateMainMessage("�A�C���F�b�P�A�A�C�c�̓K�L�b�ۂ��N�Z�ɈӊO�ƔN�͐H���Ă񂾂ȁB");

                                            UpdateMainMessage("���i�F�܂��A�t�@���l�Ƃ��̂��b�A�h���ŕ������Ă��������ˁ�");

                                            UpdateMainMessage("���F���[�F�E�E�E���i����A�������肢���܂��B");

                                            UpdateMainMessage("���i�F���H���A�����E�E�E����A���𔭓��������ˁB");

                                            UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");

                                            UpdatePlayerLocationInfo(basePosX + moveLen * 29, basePosY + moveLen * 1);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");

                                            UpdateMainMessage("�A�C���F���H�܂����͖T�ɂ��邺�B���s�����񂶂�˂��̂��H");

                                            UpdateMainMessage("���i�F�����́E�E�E�ǂ���猳�̈ʒu�ɖ߂����݂����ˁB");

                                            UpdateMainMessage("���F���[�F���̒��q�Ő�֐i��ł݂܂��傤�B");

                                            UpdateMainMessage("�A�C���F�����A�܂��܂����͂��邩���m��˂����A�Ƃ肠�����K���K���i�ނ��I");
                                        }
                                        else
                                        {
                                            UpdateMainMessage("���i�F���A���������B");

                                            UpdateMainMessage("�A�C���F���i�A���񂾂��B");

                                            UpdateMainMessage("���i�F�����A����s�����B");

                                            UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");

                                            UpdatePlayerLocationInfo(basePosX + moveLen * 29, basePosY + moveLen * 1);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                        }

                                        we.InfoArea311E = true;
                                    }
                                    else
                                    {
                                        UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");

                                        UpdatePlayerLocationInfo(basePosX + moveLen * 29, basePosY + moveLen * 1);
                                        SetupDungeonMapping(3, false);
                                        UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    }
                                    return;

                                case 12:
                                    if (!we.InfoArea312S)
                                    {
                                        if (!we.InfoArea311S && !we.InfoArea313S)
                                        {
                                            UpdateMainMessage("���i�F�����A������ƃA�C���B");

                                            UpdateMainMessage("�A�C���F��H�����H");

                                            UpdateMainMessage("���i�F���������A�z�������ɋ��B");

                                            UpdateMainMessage("�A�C���F�H�E�E�E�����A�z���g���B�悭�������ȃ��i�B");

                                            UpdateMainMessage("���F���[�F���A������́I�I");

                                            UpdateMainMessage("�A�C���F��H���F���[�ǂ��������̂��H");

                                            UpdateMainMessage("���F���[�F���̌`�A�t�@�[�W���{�a���ɂ���y���̎O�ʋ��z�ɂ�������ł��B");

                                            UpdateMainMessage("���i�F���̎O�ʋ����ĕ��������Ƃ������B�{�l�̑z�����v��Ƃ��B");

                                            UpdateMainMessage("���F���[�F���̂Ƃ���B�{�l�������A�v�l�E���f�𐮗����������ɂ悭�g���Ă��܂��B");

                                            UpdateMainMessage("�A�C���F�悵�A�ł͑����E�E�E������܂܂ɔ`���Ă݂邩�B");

                                            UpdateMainMessage("�A�C���F�E�E�E�����N���Ȃ����B�����̋�����˂����H");

                                            UpdateMainMessage("���i�F��������A�Y��ł�l����Ȃ��ƑʖڂȂ񂶂�Ȃ��H�A�C�����ጳ�X������B");

                                            UpdateMainMessage("�A�C���F���i�A�������Ď��Ƃ��ĔY�񂾂肷��񂾁B");

                                            UpdateMainMessage("���i�F���������������Ă�Ԃ́A�Y��ł邤���ɓ���Ȃ����B");

                                            UpdateMainMessage("���F���[�F�E�E�E�{�N�ł��ʖڂ݂����ł��ˁB���݂܂��񂪉����N���܂���B");

                                            UpdateMainMessage("���i�F���F���[����͐�������Ă��ł���B��z���Ă�l���ʖڂ݂����B");

                                            UpdateMainMessage("�A�C���F�ǂ����Ă����ŕ]�����܂������t�ɂȂ��Ă񂾂�B");

                                            UpdateMainMessage("�A�C���F���ƁA���Ⴀ�c��̓��i�A���܂������H");

                                            UpdateMainMessage("���i�F�E�E�E������H");

                                            UpdateMainMessage("�A�C���F���A���F���[�Ƃ���Ό�͂��O��������B�����A�u������H�v�Ȃ񂾂�B");

                                            UpdateMainMessage("���i�F�E�E�E���A���A�������ƂˁB");

                                            UpdateMainMessage("�A�C���F���˘f���Ă񂾁B�ʖڌ��ŗǂ��������Ă݂Ă����H");

                                            UpdateMainMessage("���F���[�F�A�C���N�A���̎q�ɂ��������������͎���ł���B");

                                            UpdateMainMessage("���i�F���A���F���[����A�ǂ���ł��B����A����Ă݂���B");

                                            UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");

                                            UpdateMainMessage("�A�C���F������A�܁Aῂ��I�I");

                                            UpdatePlayerLocationInfo(basePosX + moveLen * 14, basePosY + moveLen * 19);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");

                                            UpdateMainMessage("�A�C���F�E�E�E��H���A���������Ȃ��Ă邼�B");

                                            UpdateMainMessage("���F���[�F����A���������Ȃ����킯����Ȃ������ł��B");

                                            UpdateMainMessage("���i�F�_���W�����ǂ̍\�����Ⴄ�E�E�E�ǂ����Ⴄ�ꏊ�ɗ����̂ˁB");

                                            UpdateMainMessage("�A�C���F��΂��ꂽ���Ď����I�H");

                                            UpdateMainMessage("���F���[�F�ǂ���炻�̂悤�ł��ˁB���̃��[�v���u�݂����Ȃ��̂ł��傤���B");

                                            UpdateMainMessage("�A�C���F�������傤�A���ꂶ�჉�i�̃_���W�����}�b�v���������ɗ����Ȃ��񂶂�Ȃ����H");

                                            UpdateMainMessage("���i�F�����ꏊ�ɔ�΂����킯����Ȃ�������ɗ����Ȃ����P����Ȃ����ǁB");

                                            UpdateMainMessage("���i�F�ł��A���̂�ɐi�߂Ė��߂Ă������@���ʗp���Ȃ���ˁB");

                                            UpdateMainMessage("�A�C���F���ȁB�ł��i�ނ������͂˂��ȁB");

                                            UpdateMainMessage("���F���[�F�K�������͈�{���Ɍ����܂��B�s���܂��傤�B");
                                        }
                                        else
                                        {
                                            UpdateMainMessage("���i�F�����A�����ɋ����ݒu����Ă��ˁB");

                                            UpdateMainMessage("���F���[�F���i����A�������肢���܂��B");

                                            UpdateMainMessage("�A�C���F�ʂ̏�������ȁB�ꉞ�x�����Ȃ���i�������B");

                                            UpdateMainMessage("���i�F�����A����s�����B");

                                            UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");

                                            UpdatePlayerLocationInfo(basePosX + moveLen * 14, basePosY + moveLen * 19);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                        }
                                        we.InfoArea312S = true;
                                    }
                                    else
                                    {
                                        if (!we.CompleteArea32)
                                        {
                                            UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                            UpdatePlayerLocationInfo(basePosX + moveLen * 14, basePosY + moveLen * 19);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                        }
                                        else
                                        {
                                            if (!we.CompleteArea34)
                                            {
                                                UpdateMainMessage("���i�F�����͈�x�˔j���Ă����B�ǂ�����A�܂������Ă݂�H");

                                                using (YesNoRequestMini yesno = new YesNoRequestMini())
                                                {
                                                    yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                                    yesno.ShowDialog();
                                                    if (yesno.DialogResult == DialogResult.Yes)
                                                    {
                                                        UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                                        UpdatePlayerLocationInfo(basePosX + moveLen * 14, basePosY + moveLen * 19);
                                                        SetupDungeonMapping(3, false);
                                                        UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
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
                                                    UpdateMainMessage("���i�F���������A����̏��ŋ����Ă�������s����Ȃ񂾂��ǁB");

                                                    UpdateMainMessage("�A�C���F��A�Ȃ񂾁H");

                                                    UpdateMainMessage("���i�F���̋�����ł��s����悤�ɂȂ����݂����ˁ�");

                                                    UpdateMainMessage("�A�C���F�����A��邶��˂����I�_�l���̂Ă����񂶂�˂��ȁI");

                                                    UpdateMainMessage("���i�F�ǂ�����̂�B���ڍs���H");
                                                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                                                    {
                                                        yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                                        yesno.ShowDialog();
                                                        if (yesno.DialogResult == DialogResult.Yes)
                                                        {
                                                            UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                                            UpdatePlayerLocationInfo(basePosX + moveLen * 3, basePosY + moveLen * 9);
                                                            SetupDungeonMapping(3, false);
                                                            UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                                        }
                                                        else
                                                        {
                                                            UpdateMainMessage("", true);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    UpdateMainMessage("���i�F�Ō�̕����֒��ڍs���H");
                                                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                                                    {
                                                        yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                                        yesno.ShowDialog();
                                                        if (yesno.DialogResult == DialogResult.Yes)
                                                        {
                                                            UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                                            UpdatePlayerLocationInfo(basePosX + moveLen * 3, basePosY + moveLen * 9);
                                                            SetupDungeonMapping(3, false);
                                                            UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                                        }
                                                        else
                                                        {
                                                            UpdateMainMessage("���i�F�ŏ����[�v���Ă����n�_�֓����Ă݂�H");

                                                            yesno.ShowDialog();
                                                            if (yesno.DialogResult == DialogResult.Yes)
                                                            {
                                                                UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                                                UpdatePlayerLocationInfo(basePosX + moveLen * 14, basePosY + moveLen * 19);
                                                                SetupDungeonMapping(3, false);
                                                                UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
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
                                            UpdateMainMessage("���i�F�������A�����ɂ������ݒu����Ă����B");

                                            UpdateMainMessage("�A�C���F�������A������̂������ȃ��i�B�����Č���Ε����邪�B");

                                            UpdateMainMessage("���F���[�F�e�͈Ⴆ�ǁA�{�N�B�̎����B�ꏗ���ł���t�@����������̂͑��������ł��B");

                                            UpdateMainMessage("�A�C���F�t�@���E�E�E�t�@�����܂̎��ł����H");

                                            UpdateMainMessage("���F���[�F�����A�t�@���������������l�ł����B");

                                            UpdateMainMessage("���F���[�F�F�������Ƃ������ȃ��m��ʂ�߂����A�C�t���A�E���~���悤�ɁA�����Ă������ł��B");

                                            UpdateMainMessage("���i�F���A���������΁A���F���[����ƃt�@���l���ē����N�Ȃ�ł����H");

                                            UpdateMainMessage("���F���[�F�����ł���BFiveSeeker�̂T�l�͊F�����N�ł��B");

                                            UpdateMainMessage("���i�F���A���[�����Ȃ񂾁I�H������ƃr�b�N���E�E�E");

                                            UpdateMainMessage("�A�C���F�����f�B�̃{�P�������N���Ď����H");

                                            UpdateMainMessage("���F���[�F�͂��A�c�O�Ȃ��炻���������ɂȂ�܂��B");

                                            UpdateMainMessage("�A�C���F�b�P�A�A�C�c�̓K�L�b�ۂ��N�Z�ɈӊO�ƔN�͐H���Ă񂾂ȁB");

                                            UpdateMainMessage("���i�F�܂��A�t�@���l�Ƃ��̂��b�A�h���ŕ������Ă��������ˁ�");

                                            UpdateMainMessage("���F���[�F�E�E�E���i����A�������肢���܂��B");

                                            UpdateMainMessage("���i�F���H���A�����E�E�E����A���𔭓��������ˁB");

                                            UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");

                                            UpdatePlayerLocationInfo(basePosX + moveLen * 24, basePosY + moveLen * 4);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");

                                            UpdateMainMessage("�A�C���F���H�܂����͖T�ɂ��邺�B���s�����񂶂�˂��̂��H");

                                            UpdateMainMessage("���i�F�����́E�E�E�ǂ���猳�̈ʒu�ɖ߂����݂����ˁB");

                                            UpdateMainMessage("���F���[�F���̒��q�Ő�֐i��ł݂܂��傤�B");

                                            UpdateMainMessage("�A�C���F�����A�܂��܂����͂��邩���m��˂����A�Ƃ肠�����K���K���i�ނ��I");
                                        }
                                        else
                                        {
                                            UpdateMainMessage("���i�F���A���������B");

                                            UpdateMainMessage("�A�C���F���i�A���񂾂��B");

                                            UpdateMainMessage("���i�F�����A����s�����B");

                                            UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");

                                            UpdatePlayerLocationInfo(basePosX + moveLen * 24, basePosY + moveLen * 4);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                        }
                                        we.InfoArea312E = true;
                                    }
                                    else
                                    {
                                        UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");

                                        UpdatePlayerLocationInfo(basePosX + moveLen * 24, basePosY + moveLen * 4);
                                        SetupDungeonMapping(3, false);
                                        UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    }
                                    return;

                                case 14:
                                    if (!we.InfoArea313S)
                                    {
                                        if (!we.InfoArea311S && !we.InfoArea312S)
                                        {
                                            UpdateMainMessage("���i�F�����A������ƃA�C���B");

                                            UpdateMainMessage("�A�C���F��H�����H");

                                            UpdateMainMessage("���i�F���������A�z�������ɋ��B");

                                            UpdateMainMessage("�A�C���F�H�E�E�E�����A�z���g���B�悭�������ȃ��i�B");

                                            UpdateMainMessage("���F���[�F���A������́I�I");

                                            UpdateMainMessage("�A�C���F��H���F���[�ǂ��������̂��H");

                                            UpdateMainMessage("���F���[�F���̌`�A�t�@�[�W���{�a���ɂ���y���̎O�ʋ��z�ɂ�������ł��B");

                                            UpdateMainMessage("���i�F���̎O�ʋ����ĕ��������Ƃ������B�{�l�̑z�����v��Ƃ��B");

                                            UpdateMainMessage("���F���[�F���̂Ƃ���B�{�l�������A�v�l�E���f�𐮗����������ɂ悭�g���Ă��܂��B");

                                            UpdateMainMessage("�A�C���F�悵�A�ł͑����E�E�E������܂܂ɔ`���Ă݂邩�B");

                                            UpdateMainMessage("�A�C���F�E�E�E�����N���Ȃ����B�����̋�����˂����H");

                                            UpdateMainMessage("���i�F��������A�Y��ł�l����Ȃ��ƑʖڂȂ񂶂�Ȃ��H�A�C�����ጳ�X������B");

                                            UpdateMainMessage("�A�C���F���i�A�������Ď��Ƃ��ĔY�񂾂肷��񂾁B");

                                            UpdateMainMessage("���i�F���������������Ă�Ԃ́A�Y��ł邤���ɓ���Ȃ����B");

                                            UpdateMainMessage("���F���[�F�E�E�E�{�N�ł��ʖڂ݂����ł��ˁB���݂܂��񂪉����N���܂���B");

                                            UpdateMainMessage("���i�F���F���[����͐�������Ă��ł���B��z���Ă�l���ʖڂ݂����B");

                                            UpdateMainMessage("�A�C���F�ǂ����Ă����ŕ]�����܂������̐^�t�ɂȂ��Ă񂾂�B");

                                            UpdateMainMessage("�A�C���F���ƁA���Ⴀ�c��̓��i�A���܂������H");

                                            UpdateMainMessage("���i�F�E�E�E������H");

                                            UpdateMainMessage("�A�C���F���A���F���[�Ƃ���Ό�͂��O��������B�����A�u������H�v�Ȃ񂾂�B");

                                            UpdateMainMessage("���i�F�E�E�E���A���A�������ƂˁB");

                                            UpdateMainMessage("�A�C���F���˘f���Ă񂾁B�ʖڌ��ŗǂ��������Ă݂Ă����H");

                                            UpdateMainMessage("���F���[�F�A�C���N�A���̎q�ɂ��������������͎���ł���B");

                                            UpdateMainMessage("���i�F���A���F���[����A�ǂ���ł��B����A����Ă݂���B");

                                            UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");

                                            UpdateMainMessage("�A�C���F������A�܁Aῂ��I�I");

                                            UpdatePlayerLocationInfo(basePosX + moveLen * 7, basePosY + moveLen * 3);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");

                                            UpdateMainMessage("�A�C���F�E�E�E��H���A���������Ȃ��Ă邼�B");

                                            UpdateMainMessage("���F���[�F����A���������Ȃ����킯����Ȃ������ł��B");

                                            UpdateMainMessage("���i�F�_���W�����ǂ̍\�����Ⴄ�E�E�E�ǂ����Ⴄ�ꏊ�ɗ����̂ˁB");

                                            UpdateMainMessage("�A�C���F��΂��ꂽ���Ď����I�H");

                                            UpdateMainMessage("���F���[�F�ǂ���炻�̂悤�ł��ˁB���̃��[�v���u�݂����Ȃ��̂ł��傤���B");

                                            UpdateMainMessage("�A�C���F�������傤�A���ꂶ�჉�i�̃_���W�����}�b�v���������ɗ����Ȃ��񂶂�Ȃ����H");

                                            UpdateMainMessage("���i�F�����ꏊ�ɔ�΂����킯����Ȃ�������ɗ����Ȃ����P����Ȃ����ǁB");

                                            UpdateMainMessage("���i�F�ł��A���̂�ɐi�߂Ė��߂Ă������@���ʗp���Ȃ���ˁB");

                                            UpdateMainMessage("�A�C���F���ȁB�ł��i�ނ������͂˂��ȁB");

                                            UpdateMainMessage("���F���[�F�K�������͈�{���Ɍ����܂��B�s���܂��傤�B");
                                        }
                                        else
                                        {
                                            UpdateMainMessage("���i�F�����A�����ɋ����ݒu����Ă��ˁB");

                                            UpdateMainMessage("���F���[�F���i����A�������肢���܂��B");

                                            UpdateMainMessage("�A�C���F�ʂ̏�������ȁB�ꉞ�x�����Ȃ���i�������B");

                                            UpdateMainMessage("���i�F�����A����s�����B");

                                            UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");

                                            UpdatePlayerLocationInfo(basePosX + moveLen * 7, basePosY + moveLen * 3);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                        }
                                        we.InfoArea313S = true;
                                    }
                                    else
                                    {
                                        if (!we.CompleteArea32)
                                        {
                                            UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                            UpdatePlayerLocationInfo(basePosX + moveLen * 7, basePosY + moveLen * 3);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                        }
                                        else
                                        {
                                            if (!we.CompleteArea34)
                                            {
                                                UpdateMainMessage("���i�F�����͈�x�˔j���Ă����B�ǂ�����A�܂������Ă݂�H");

                                                using (YesNoRequestMini yesno = new YesNoRequestMini())
                                                {
                                                    yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                                    yesno.ShowDialog();
                                                    if (yesno.DialogResult == DialogResult.Yes)
                                                    {
                                                        UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                                        UpdatePlayerLocationInfo(basePosX + moveLen * 7, basePosY + moveLen * 3);
                                                        SetupDungeonMapping(3, false);
                                                        UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
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
                                                    UpdateMainMessage("���i�F���������A����̏��ŋ����Ă�������s����Ȃ񂾂��ǁB");

                                                    UpdateMainMessage("�A�C���F��A�Ȃ񂾁H");

                                                    UpdateMainMessage("���i�F���̋�����ł��s����悤�ɂȂ����݂����ˁ�");

                                                    UpdateMainMessage("�A�C���F�����A��邶��˂����I�_�l���̂Ă����񂶂�˂��ȁI");

                                                    UpdateMainMessage("���i�F�ǂ�����̂�B���ڍs���H");
                                                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                                                    {
                                                        yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                                        yesno.ShowDialog();
                                                        if (yesno.DialogResult == DialogResult.Yes)
                                                        {
                                                            UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                                            UpdatePlayerLocationInfo(basePosX + moveLen * 3, basePosY + moveLen * 9);
                                                            SetupDungeonMapping(3, false);
                                                            UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                                        }
                                                        else
                                                        {
                                                            UpdateMainMessage("", true);
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    UpdateMainMessage("���i�F�Ō�̕����֒��ڍs���H");
                                                    using (YesNoRequestMini yesno = new YesNoRequestMini())
                                                    {
                                                        yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                                        yesno.ShowDialog();
                                                        if (yesno.DialogResult == DialogResult.Yes)
                                                        {
                                                            UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                                            UpdatePlayerLocationInfo(basePosX + moveLen * 3, basePosY + moveLen * 9);
                                                            SetupDungeonMapping(3, false);
                                                            UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                                        }
                                                        else
                                                        {
                                                            UpdateMainMessage("���i�F�ŏ����[�v���Ă����n�_�֓����Ă݂�H");

                                                            yesno.ShowDialog();
                                                            if (yesno.DialogResult == DialogResult.Yes)
                                                            {
                                                                UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                                                UpdatePlayerLocationInfo(basePosX + moveLen * 7, basePosY + moveLen * 3);
                                                                SetupDungeonMapping(3, false);
                                                                UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
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
                                            UpdateMainMessage("���i�F�������A�����ɂ������ݒu����Ă����B");

                                            UpdateMainMessage("�A�C���F�������A������̂������ȃ��i�B�����Č���Ε����邪�B");

                                            UpdateMainMessage("���F���[�F�e�͈Ⴆ�ǁA�{�N�B�̎����B�ꏗ���ł���t�@����������̂͑��������ł��B");

                                            UpdateMainMessage("�A�C���F�t�@���E�E�E�t�@�����܂̎��ł����H");

                                            UpdateMainMessage("���F���[�F�����A�t�@���������������l�ł����B");

                                            UpdateMainMessage("���F���[�F�F�������Ƃ������ȃ��m��ʂ�߂����A�C�t���A�E���~���悤�ɁA�����Ă������ł��B");

                                            UpdateMainMessage("���i�F���A���������΁A���F���[����ƃt�@���l���ē����N�Ȃ�ł����H");

                                            UpdateMainMessage("���F���[�F�����ł���BFiveSeeker�̂T�l�͊F�����N�ł��B");

                                            UpdateMainMessage("���i�F���A���[�����Ȃ񂾁I�H������ƃr�b�N���E�E�E");

                                            UpdateMainMessage("�A�C���F�����f�B�̃{�P�������N���Ď����H");

                                            UpdateMainMessage("���F���[�F�͂��A�c�O�Ȃ��炻���������ɂȂ�܂��B");

                                            UpdateMainMessage("�A�C���F�b�P�A�A�C�c�̓K�L�b�ۂ��N�Z�ɈӊO�ƔN�͐H���Ă񂾂ȁB");

                                            UpdateMainMessage("���i�F�܂��A�t�@���l�Ƃ��̂��b�A�h���ŕ������Ă��������ˁ�");

                                            UpdateMainMessage("���F���[�F�E�E�E���i����A�������肢���܂��B");

                                            UpdateMainMessage("���i�F���H���A�����E�E�E����A���𔭓��������ˁB");

                                            UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");

                                            UpdatePlayerLocationInfo(basePosX + moveLen * 21, basePosY + moveLen * 1);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");

                                            UpdateMainMessage("�A�C���F���H�܂����͖T�ɂ��邺�B���s�����񂶂�˂��̂��H");

                                            UpdateMainMessage("���i�F�����́E�E�E�ǂ���猳�̈ʒu�ɖ߂����݂����ˁB");

                                            UpdateMainMessage("���F���[�F���̒��q�Ő�֐i��ł݂܂��傤�B");

                                            UpdateMainMessage("�A�C���F�����A�܂��܂����͂��邩���m��˂����A�Ƃ肠�����K���K���i�ނ��I");

                                        }
                                        else
                                        {
                                            UpdateMainMessage("���i�F���A���������B");

                                            UpdateMainMessage("�A�C���F���i�A���񂾂��B");

                                            UpdateMainMessage("���i�F�����A����s�����B");

                                            UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");

                                            UpdatePlayerLocationInfo(basePosX + moveLen * 21, basePosY + moveLen * 1);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                        }
                                        we.InfoArea313E = true;
                                    }
                                    else
                                    {
                                        UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");

                                        UpdatePlayerLocationInfo(basePosX + moveLen * 21, basePosY + moveLen * 1);
                                        SetupDungeonMapping(3, false);
                                        UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    }
                                    return;
                                    #endregion

                                case 16: // ���[�v�J�n�S
                                    #region "���֖�"
                                    if (!we.InfoArea324S)
                                    {
                                        UpdateMainMessage("���i�F���A�������������B���������Ɓ�");

                                        UpdateMainMessage("�A�C���F�Ȃ��A���F���[�B�t�@�[�W���{�a�ɂ���E�E�E�Ȃ񂾂����E�E�E");

                                        UpdateMainMessage("���F���[�F���̎O�ʋ��̂��Ƃł����H");

                                        UpdateMainMessage("�A�C���F���ꂻ��B�t�@�[�W���{�a���ɂ������A���[�v���u�Ȃ̂��H");

                                        UpdateMainMessage("���F���[�F�����A���[�v���u�Ƃ����킯�ł͂���܂����B�P�Ȃ鋾�ƌ����΁E�E�E");

                                        UpdateMainMessage("���i�F�P�Ȃ鋾�ł͂Ȃ���ł����H");

                                        UpdateMainMessage("���F���[�F�����܂���A�P�Ȃ鋾�ł͂���܂���B");

                                        UpdateMainMessage("�A�C���F�ł����e�͋������Ȃ��E�E�E���ď����H");

                                        UpdateMainMessage("���F���[�F�͂��A�����܂���B");

                                        UpdateMainMessage("���i�F�܂��܂��A���͖��炩�Ƀ��[�v���u��ˁ�@���ᑁ���g���܂����");

                                        UpdateMainMessage("�A�C���F�������i�B���ł��O����Ȋy�������Ȃ񂾂�H");

                                        UpdateMainMessage("���i�F��H���ł��낤�ˁB���ƂȂ�����ɐG��邽�тɁA�C��������銴���Ȃ̂�B");

                                        UpdateMainMessage("���F���[�F�E�E�E�����A���i���񂨊肢���܂��B");

                                        UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");

                                        UpdatePlayerLocationInfo(basePosX + moveLen * 15, basePosY + moveLen * 6);
                                        SetupDungeonMapping(3, false);
                                        UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");

                                        UpdateMainMessage("�A�C���F�ǂ����኱�L�����ɏo���悤���ȁB");

                                        UpdateMainMessage("���i�F�킠�I���Č��āI�S���ɂ��ꂢ�ɋ����u���Ă����B");

                                        UpdateMainMessage("�A�C���F���ȁA���O���������܂Ō����Ă�̂���I�H");

                                        UpdateMainMessage("���i�F���H�����A��������ł���������A��[������΂ˁB");

                                        UpdateMainMessage("�A�C���F�������H�E�E�E�@�E�E�E�@�E�E�E�@�܂��A�m���ɂ��������ȁB");

                                        UpdateMainMessage("���i�F�A�C���A�ǂ�����s���H");

                                        UpdateMainMessage("�A�C���F���i�̍D���ȏ�����ŗǂ����B");

                                        UpdateMainMessage("���i�F�ӂ���A�ӊO�ƗD�����̂ˁB");

                                        UpdateMainMessage("�A�C���F�����P�킩��˂��������Ă�񂾁B�ق�A�������ƍD���ɑI�ׁB");

                                        UpdateMainMessage("���i�F�r�[�Ƀo�J�ɂȂ����E�E�E�ǂ���A�D���ɑI�Ԃ���B");

                                        we.InfoArea324S = true;
                                    }
                                    else
                                    {
                                        if (!we.CompleteArea32)
                                        {
                                            UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                            UpdatePlayerLocationInfo(basePosX + moveLen * 15, basePosY + moveLen * 6);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                        }
                                        else
                                        {
                                            UpdateMainMessage("���i�F�����͈�x�˔j���Ă����B�ǂ�����A�܂������Ă݂�H");

                                            using (YesNoRequestMini yesno = new YesNoRequestMini())
                                            {
                                                yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                                yesno.ShowDialog();
                                                if (yesno.DialogResult == DialogResult.Yes)
                                                {
                                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                                    UpdatePlayerLocationInfo(basePosX + moveLen * 15, basePosY + moveLen * 6);
                                                    SetupDungeonMapping(3, false);
                                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                                }
                                                else
                                                {
                                                    UpdateMainMessage("", true);
                                                }
                                            }
                                        }
                                    }
                                    return;

                                case 17: // ���[�v�J�n�T
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 12, basePosY + moveLen * 2);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    return;

                                case 18: // ���[�v�J�n�U
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 26, basePosY + moveLen * 17);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    return;

                                case 19: // ���[�v�J�n�V
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 24, basePosY + moveLen * 9);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    return;

                                case 20: // ���[�v�J�n�W
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 8, basePosY + moveLen * 14);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    return;

                                case 21: // ���[�v�J�n�X
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 10, basePosY + moveLen * 18);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    return;

                                case 22: // ���[�v�J�n�P�O
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 8, basePosY + moveLen * 8);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    return;

                                case 23: // ���[�v�J�n�P�P
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 29, basePosY + moveLen * 15);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    return;

                                case 24: // ���[�v�J�n�P�Q
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 10, basePosY + moveLen * 2);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    return;

                                case 25: // ���[�v�J�n�P�R
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 12, basePosY + moveLen * 4);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    return;

                                case 26: // ���[�v�J�n�P�S
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 13, basePosY + moveLen * 14);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    return;

                                case 27: // �I�����[�v�S
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 23, basePosY + moveLen * 14);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");

                                    if (!we.InfoArea324E)
                                    {

                                        UpdateMainMessage("���F���[�F�����́E�E�E���܂����ˁB�߂��Ă��ꂽ�悤�ł��B");

                                        UpdateMainMessage("���i�F�E�E�E�b�n�A�A�A�@�@�E�E�E��ꂽ��B");

                                        UpdateMainMessage("�A�C���F������A�₯�ɂ���ǂ������ȁB");

                                        UpdateMainMessage("�A�C���F����������v���Ă����A���i�B���ł��O����ȂɃ��L�����Ă񂾂�H");

                                        UpdateMainMessage("���i�F�A���^�Ɋ֌W�Ȃ��ł���A�ق��Ƃ��Ă�I");

                                        UpdateMainMessage("�A�C���F�R���b�E�E�E���ŃL���Ă񂾂�B�Ӗ��킩��˂��ȁB");

                                        UpdateMainMessage("���F���[�F�E�E�E���̎O�ʋ��̂����ł��ˁH");

                                        UpdateMainMessage("���i�F�Ⴂ�܂��B���v�ł�����B");

                                        UpdateMainMessage("�A�C���F��̂ǂ�����������H");

                                        UpdateMainMessage("���F���[�F������Y�݂𖳂����Ă����E�E�E�ƂĂ��h�������Ǝv���܂��񂩁H");

                                        UpdateMainMessage("�A�C���F�Y��ł�����V���h�C����A�ǂ��l���Ă��B���F���[���������Ȃ���ȁB");

                                        UpdateMainMessage("���i�F���F���[����A�����̃o�J�ɂ͈ꐶ�����o���Ȃ����e�ł�����A�����Ƃ��Ă����Ă��������B");

                                        UpdateMainMessage("�A�C���F�b�`�E�E�E�����A�����A���ɂ͂ǂ���������܂����B");

                                        UpdateMainMessage("���F���[�F�����Ă���Ԃ́A�I�����͎c����Ă��܂��B");

                                        UpdateMainMessage("���F���[�F�����ĔY��ł���Ԃ́A�ӎu�����摗��ł��܂��B");

                                        UpdateMainMessage("�A�C���F�������A���̎O�ʋ��͂Ђ���Ƃ��āB");

                                        UpdateMainMessage("���i�F������@�����������茳�C�ɂȂ������@�܂��܂��r���Ȃ񂾂��炳�����Ɛi�ނ���");

                                        UpdateMainMessage("�@�@�@�w�b�h�o�L�C�B�B�B�I�I�I�x�i���C�g�j���O�L�b�N���A�C�����y��j�@�@");

                                        UpdateMainMessage("�A�C���F�b�e�G�G�F�F�F�I�I�I�@���O�A�R��^�C�~���O������������I�H");

                                        UpdateMainMessage("���i�F�ǂ�����Ȃ��A�������ł��Ȃ�����");

                                        UpdateMainMessage("�A�C���F����A���̗̑͂�����E�E�E���R�Ȃ��\�͂��B�����f�B���v���o�����E�E�E���ĂāE�E�E");

                                        UpdateMainMessage("���i�F�b�t�t�A���ł��Ȃ����Č����Ă邶��Ȃ��B�����A�s���܂����");
                                        we.InfoArea324E = true;
                                    }
                                    return;

                                case 28: // ���s���[�v�P
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 29, basePosY + moveLen * 1);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");

                                    if (!we.CompleteArea32)
                                    {
                                        if (!we.FailArea321)
                                        {
                                            if (!we.FailArea322 && !we.FailArea323)
                                            {
                                                UpdateMainMessage("���i�F���A����H");

                                                UpdateMainMessage("�A�C���F��A�ǂ������񂾁H");

                                                UpdateMainMessage("���F���[�F�ǂ����A�����͍ŏ��̏ꏊ�̂悤�ł��ˁB");

                                                UpdateMainMessage("�A�C���F�����A�����ȒP�ɐi�܂��Ă͂���˂����Ď����B");

                                                UpdateMainMessage("���i�F�A�C���A�S�����ˁB���͏�肭�I��ł݂��邩��B");

                                                UpdateMainMessage("�A�C���F�������Ă�B���Ɏ��S�g���b�v�Ɉ��������������P�ł��˂��B�C�ɂ��������P�񂾁B");

                                                UpdateMainMessage("���i�F�����A������񂳂����̏ꏊ�ɍs���܂��傤�B");
                                            }
                                            else
                                            {
                                                UpdateMainMessage("���i�F���A����H");

                                                UpdateMainMessage("�A�C���F��A�ǂ��Ƀ��[�v�����񂾁H");

                                                UpdateMainMessage("���F���[�F�O��Ə����ʒu�͈���Ă܂����A�ŏ��̏ꏊ�̂悤�ł��ˁB");

                                                UpdateMainMessage("���i�F���x���S�����ˁB���͏�肭�I��ł݂��邩��B");

                                                UpdateMainMessage("�A�C���F�������Ă�B�O�̏��ƈႤ�|�C���g����˂����B���傤���˂�����A�C�ɂ��������P�񂾁B");

                                                UpdateMainMessage("���i�F�����A������񂳂����̏ꏊ�ɍs���܂��傤�B");
                                            }
                                            we.FailArea321 = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("���i�F���A����H");

                                            UpdateMainMessage("�A�C���F�R�R�́E�E�E�܂��ŏ��̏ꏊ�ɖ߂��ꂿ�܂����ȁB");

                                            UpdateMainMessage("���i�F�������Ȃ̂ɁA���x���S�����ˁB");

                                            UpdateMainMessage("�A�C���F�C�ɂ���Ȃ��āA��̂��܂����ӂ��Ă�p������ƒ��q���������B");

                                            UpdateMainMessage("���F���[�F���i����A���������čs���܂��傤�B������x�ł��B");

                                            UpdateMainMessage("���i�F�����A������񂳂����̏ꏊ�ɍs���܂��傤�B");
                                        }
                                    }
                                    return;

                                case 29: // ���s���[�v�Q
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 24, basePosY + moveLen * 4);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");

                                    if (!we.CompleteArea32)
                                    {
                                        if (!we.FailArea322)
                                        {
                                            if (!we.FailArea321 && !we.FailArea323)
                                            {
                                                UpdateMainMessage("���i�F���A����H");

                                                UpdateMainMessage("�A�C���F��A�ǂ������񂾁H");

                                                UpdateMainMessage("���F���[�F�ǂ����A�����͍ŏ��̏ꏊ�̂悤�ł��ˁB");

                                                UpdateMainMessage("�A�C���F�����A�����ȒP�ɐi�܂��Ă͂���˂����Ď����B");

                                                UpdateMainMessage("���i�F�A�C���A�S�����ˁB���͏�肭�I��ł݂��邩��B");

                                                UpdateMainMessage("�A�C���F�������Ă�B���Ɏ��S�g���b�v�Ɉ��������������P�ł��˂��B�C�ɂ��������P�񂾁B");

                                                UpdateMainMessage("���i�F�����A������񂳂����̏ꏊ�ɍs���܂��傤�B");
                                            }
                                            else
                                            {
                                                UpdateMainMessage("���i�F���A����H");

                                                UpdateMainMessage("�A�C���F��A�ǂ��Ƀ��[�v�����񂾁H");

                                                UpdateMainMessage("���F���[�F�O��Ə����ʒu�͈���Ă܂����A�ŏ��̏ꏊ�̂悤�ł��ˁB");

                                                UpdateMainMessage("���i�F���x���S�����ˁB���͏�肭�I��ł݂��邩��B");

                                                UpdateMainMessage("�A�C���F�������Ă�B�O�̏��ƈႤ�|�C���g����˂����B���傤���˂�����A�C�ɂ��������P�񂾁B");

                                                UpdateMainMessage("���i�F�����A������񂳂����̏ꏊ�ɍs���܂��傤�B");
                                            }
                                            we.FailArea322 = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("���i�F���A����H");

                                            UpdateMainMessage("�A�C���F�R�R�́E�E�E�܂��ŏ��̏ꏊ�ɖ߂��ꂿ�܂����ȁB");

                                            UpdateMainMessage("���i�F�������Ȃ̂ɁA���x���S�����ˁB");

                                            UpdateMainMessage("�A�C���F�C�ɂ���Ȃ��āA��̂��܂����ӂ��Ă�p������ƒ��q���������B");

                                            UpdateMainMessage("���F���[�F���i����A���������čs���܂��傤�B������x�ł��B");

                                            UpdateMainMessage("���i�F�����A������񂳂����̏ꏊ�ɍs���܂��傤�B");
                                        }
                                    }
                                    return;

                                case 30: // ���s���[�v�R
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 21, basePosY + moveLen * 1);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");

                                    if (!we.CompleteArea32)
                                    {
                                        if (!we.FailArea323)
                                        {
                                            if (!we.FailArea321 && !we.FailArea322)
                                            {
                                                UpdateMainMessage("���i�F���A����H");

                                                UpdateMainMessage("�A�C���F��A�ǂ������񂾁H");

                                                UpdateMainMessage("���F���[�F�ǂ����A�����͍ŏ��̏ꏊ�̂悤�ł��ˁB");

                                                UpdateMainMessage("�A�C���F�����A�����ȒP�ɐi�܂��Ă͂���˂����Ď����B");

                                                UpdateMainMessage("���i�F�A�C���A�S�����ˁB���͏�肭�I��ł݂��邩��B");

                                                UpdateMainMessage("�A�C���F�������Ă�B���Ɏ��S�g���b�v�Ɉ��������������P�ł��˂��B�C�ɂ��������P�񂾁B");

                                                UpdateMainMessage("���i�F�����A������񂳂����̏ꏊ�ɍs���܂��傤�B");
                                            }
                                            else
                                            {
                                                UpdateMainMessage("���i�F���A����H");

                                                UpdateMainMessage("�A�C���F��A�ǂ��Ƀ��[�v�����񂾁H");

                                                UpdateMainMessage("���F���[�F�O��Ə����ʒu�͈���Ă܂����A�ŏ��̏ꏊ�̂悤�ł��ˁB");

                                                UpdateMainMessage("���i�F���x���S�����ˁB���͏�肭�I��ł݂��邩��B");

                                                UpdateMainMessage("�A�C���F�������Ă�B�O�̏��ƈႤ�|�C���g����˂����B���傤���˂�����A�C�ɂ��������P�񂾁B");

                                                UpdateMainMessage("���i�F�����A������񂳂����̏ꏊ�ɍs���܂��傤�B");
                                            }
                                            we.FailArea323 = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("���i�F���A����H");

                                            UpdateMainMessage("�A�C���F�R�R�́E�E�E�܂��ŏ��̏ꏊ�ɖ߂��ꂿ�܂����ȁB");

                                            UpdateMainMessage("���i�F�������Ȃ̂ɁA���x���S�����ˁB");

                                            UpdateMainMessage("�A�C���F�C�ɂ���Ȃ��āA��̂��܂����ӂ��Ă�p������ƒ��q���������B");

                                            UpdateMainMessage("���F���[�F���i����A���������čs���܂��傤�B������x�ł��B");

                                            UpdateMainMessage("���i�F�����A������񂳂����̏ꏊ�ɍs���܂��傤�B");
                                        }
                                    }
                                    return;

                                case 31: // ���s���[�v�S�P
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 15, basePosY + moveLen * 6);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");

                                    if (!we.CompleteArea32)
                                    {
                                        if (!we.FailArea3241)
                                        {
                                            UpdateMainMessage("���i�F����A�������ė�̂S���ɋ������镔���H");

                                            UpdateMainMessage("�A�C���F��H�E�E�E�����A�m���ɂ������ȁB");

                                            UpdateMainMessage("���F���[�F�ǂ����A�U��o���̂悤�ł��ˁB");

                                            UpdateMainMessage("�A�C���F�ł��܂��A�A�C�e�������������ȁB�����C�͂��Ȃ����B");

                                            UpdateMainMessage("���i�F���̏��֍s���Ă݂邱�Ƃɂ����ˁB");

                                            UpdateMainMessage("�A�C���F�����A���񂾂��I");
                                            we.FailArea3241 = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("���i�F����A�Ђ���Ƃ��Ă܂��U��o���H");

                                            UpdateMainMessage("�A�C���F���i�A�R�R�͑O�ɒʂ������[�g���B���������B");

                                            UpdateMainMessage("���i�F�S�����ˁB���͓�������ʂ�Ȃ��悤�ɂ����B");

                                            UpdateMainMessage("�A�C���F�ӂȂ��Ă��ǂ����B�������A�������q�����ȁB");
                                        }
                                    }
                                    return;

                                case 32: // ���s���[�v�S�Q
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 15, basePosY + moveLen * 6);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");

                                    if (!we.CompleteArea32)
                                    {
                                        if (!we.FailArea3242)
                                        {
                                            UpdateMainMessage("���i�F����A�������ė�̂S���ɋ������镔���H");

                                            UpdateMainMessage("�A�C���F��H�E�E�E�����A�m���ɂ������ȁB");

                                            UpdateMainMessage("���F���[�F�ǂ����A�U��o���̂悤�ł��ˁB");

                                            UpdateMainMessage("�A�C���F�ł��܂��A�A�C�e�������������ȁB�����C�͂��Ȃ����B");

                                            UpdateMainMessage("���i�F���̏��֍s���Ă݂邱�Ƃɂ����ˁB");

                                            UpdateMainMessage("�A�C���F�����A���񂾂��I");
                                            we.FailArea3242 = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("���i�F����A�Ђ���Ƃ��Ă܂��U��o���H");

                                            UpdateMainMessage("�A�C���F���i�A�R�R�͑O�ɒʂ������[�g���B���������B");

                                            UpdateMainMessage("���i�F�S�����ˁB���͓�������ʂ�Ȃ��悤�ɂ����B");

                                            UpdateMainMessage("�A�C���F�ӂȂ��Ă��ǂ����B�������A�������q�����ȁB");
                                        }
                                    }
                                    return;

                                case 33: // �J�n���[�v�W�Q
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 9, basePosY + moveLen * 14);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    return;

                                case 34: // ���s���[�v�S�R
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 15, basePosY + moveLen * 6);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");

                                    if (!we.CompleteArea32)
                                    {
                                        if (!we.FailArea3243)
                                        {
                                            UpdateMainMessage("���i�F����A�������ė�̂S���ɋ������镔���H");

                                            UpdateMainMessage("�A�C���F��H�E�E�E�����A�m���ɂ������ȁB");

                                            UpdateMainMessage("���F���[�F�ǂ����A�U��o���̂悤�ł��ˁB");

                                            UpdateMainMessage("�A�C���F�ł��܂��A�A�C�e�������������ȁB�����C�͂��Ȃ����B");

                                            UpdateMainMessage("���i�F���̏��֍s���Ă݂邱�Ƃɂ����ˁB");

                                            UpdateMainMessage("�A�C���F�����A���񂾂��I");
                                            we.FailArea3243 = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("���i�F����A�Ђ���Ƃ��Ă܂��U��o���H");

                                            UpdateMainMessage("�A�C���F���i�A�R�R�͑O�ɒʂ������[�g���B���������B");

                                            UpdateMainMessage("���i�F�S�����ˁB���͓�������ʂ�Ȃ��悤�ɂ����B");

                                            UpdateMainMessage("�A�C���F�ӂȂ��Ă��ǂ����B�������A�������q�����ȁB");
                                        }
                                    }
                                    return;

                                case 35: // ���֖�N���A�Ŕ�
                                    if (!we.CompleteArea32)
                                    {
                                        UpdateMainMessage("�A�C���F�Ȃ��A������ƈꑧ���Ȃ����H");

                                        UpdateMainMessage("���i�F�������Ă�̂�B�S�R�I����ĂȂ���������Ȃ��B�܂��܂����ꂩ���B");

                                        UpdateMainMessage("�A�C���F�����͌����Ă��ȁB���̃��[�v���Ă̂͒��q�������z���g�B");

                                        UpdateMainMessage("�A�C���F���͈�U�x�ނ��E�E�E�R�R�A�s���悭���ʂ̈֎q������ȁB���点�Ă��炤���B");

                                        UpdateMainMessage("�A�C���F�悢�����ƁB");

                                        UpdateMainMessage("�@�@�@�@�w�b�S�S�S�S�E�E�E�h�I�I�H�H�H���E�E�E�x");

                                        we.CompleteArea32 = true;
                                        ConstructDungeonMap();
                                        SetupDungeonMapping(3);

                                        UpdateMainMessage("���i�F�E�E�E������ƁE�E�E���������������B");

                                        UpdateMainMessage("���F���[�F�����ǂ������悤�ȉ��ł����ˁB");

                                        UpdateMainMessage("�A�C���F��H���̈֎q�Ȃ񂩏����Ă��邼�B");

                                        UpdateMainMessage("�@�@�@�@�w����鎖�Ȃ����Ɏ��L�΂��B�����Ȃ����ɐG�����̂ɓ����񂯂�B�x");

                                        UpdateMainMessage("���i�F���Ⴀ�A���̉����Ăǂ��������J�ʂ����Ǝv���΂����̂�����H");

                                        UpdateMainMessage("���F���[�F�����炭�A�����ł��傤�ˁB");

                                        UpdateMainMessage("�A�C���F��������A�^�����͂̂��������B�Ƃ���łǂ̕ӂ̓����J�����񂾁H");

                                        UpdateMainMessage("���i�F���ׂȂ��ƕ�����Ȃ����ǁA���̘A�����[�v�̒��ł͂Ȃ���ˁB");

                                        UpdateMainMessage("�A�C���F���ł���Ȏ���������񂾁H���O�̂��炻�������A���Œm���Ă邩�̂悤�Ȓ���Ȃ񂾂�B");

                                        UpdateMainMessage("���i�F���ł��Č����Ă������B��̂���Ȃ���ł���H");

                                        UpdateMainMessage("���F���[�F�܂��A�d�|���͉������ꂽ�悤�ł����A���̐�֐i�݂܂��񂩁H");

                                        UpdateMainMessage("�A�C���F�������ȁA���̓��ǂ�����{���̂悤�����A�����炭�o������B");

                                        UpdateMainMessage("���i�F�������A�s���܂����");
                                    }
                                    else
                                    {
                                        UpdateMainMessage("�@�@�@�@�w����鎖�Ȃ����Ɏ��L�΂��B�����Ȃ����ɐG�����̂ɓ����񂯂�B�x", true);
                                    }
                                    return;
#endregion

                                case 36: // �J�n���[�v�R�P�T
                                    #region "��O�֖�"
                                    if (!we.InfoArea3315S)
                                    {
                                        UpdateMainMessage("���i�F���āA���������B");

                                        UpdateMainMessage("���F���[�F���i����A�{���ɑ̂̕��͑��v�ł����H");

                                        UpdateMainMessage("���i�F�S�z�Ȃ���B�����A�s�����B");

                                        UpdateMainMessage("�A�C���F�����A���i�B�{�����낤�ȁH");

                                        UpdateMainMessage("���i�F����A�A�C���܂Ő^���Ȋ炵������āB���v��A����������ė��Ă邩��B");

                                        UpdateMainMessage("�A�C���F�������B����C�������I�I");

                                        UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                        UpdatePlayerLocationInfo(basePosX + moveLen * 2, basePosY + moveLen * 4);
                                        SetupDungeonMapping(3, false);
                                        UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");

                                        UpdateMainMessage("���i�F���͍��v�S�A���ʂɋK������������ł��B�R���Ƃ������q���g���Ȃ������ˁB");

                                        UpdateMainMessage("�A�C���F���A�����Ȃ̂��H���ς�炸�����ȁB");

                                        UpdateMainMessage("���F���[�F���i����A�������߂������ǂ��ł��B�{�N���猩�Ă��������Ȋ��������܂��B");

                                        UpdateMainMessage("�A�C���F�Ȃ��A�����Ă����B��̂ǂ�����ĕ��������Ă�񂾁H�������u�Ԃ����B");

                                        UpdateMainMessage("���i�F����Ƃ������o�Ƃ͏����Ⴄ��B�c���Ƃ͏����Ⴄ�́B���o�I�ɕ��͋C��͂ނ悤�ɂ���̂�B");

                                        UpdateMainMessage("�A�C���F�E�E�E�悭������˂��ȁA�܂����͂������茩�Ă����Ƃ��邩�B");

                                        UpdateMainMessage("���F���[�F�E�E�E�ǂ��ł��B�ǂ�����s���ׂ��ł��傤���H");

                                        UpdateMainMessage("���i�F�����A�}�����ꂵ�Ă��邭���ɁA��{���Ȃ͕̂ς��Ȃ��̂ˁB��ȍ���B");

                                        UpdateMainMessage("���F���[�F�����Ȃ�ł����H���ɂ͐����ǂ�����Ηǂ���������܂���B���i���񂪌��߂Ă��������B");

                                        UpdateMainMessage("���i�F��������A�A�C���A���F���[����A���ė��āB");

                                        UpdateMainMessage("�A�C���F�����A�C�������A���i�B");

                                        we.InfoArea3315S = true;
                                    }
                                    else
                                    {
                                        if (!we.CompleteArea33)
                                        {
                                            if (we.FailArea331 && !we.FailArea332) // ������ƃw�^�N�\�ȃt���O�\�z�ł��ˁB
                                            {
                                                UpdateMainMessage("���i�F�S�����A��������点�āB���͓��ĂĂ݂��邩��B");

                                                UpdateMainMessage("�A�C���F�����A���Ɏ�ɂ������ؕԂ����x�Ɩ߂�Ȃ��g���b�v����˂��B���S���Ă���Ă������B");

                                                UpdateMainMessage("���F���[�F�E�E�E�ł́A���i���񂨊肢���܂��B");

                                                UpdateMainMessage("���i�F�����A���Ⴀ�s�����B");
                                            }
                                            else
                                            {
                                                if (!we.FailArea332)
                                                {
                                                    UpdateMainMessage("���i�F�ʖڂˁA�Ō�͕K�����������ɗ��āA����������Ȃ��H��̂ǂ��������Ȃ̂�����B");

                                                    UpdateMainMessage("���F���[�F�Ђ���Ƃ�����ł����A�r���o�߂��S�Ċ܂܂�Ă���̂ł͂Ȃ��ł��傤���H");

                                                    UpdateMainMessage("���i�F�Ō�̗�̕����ɗ����u�ԂɎ��͕������Ă�̂�ˁB������ʖڂȊ�����������āE�E�E");

                                                    UpdateMainMessage("�A�C���F�\�����Ԃ͂���񂾁B��������A�e��T���悤�ɂ�낤���B���ȁI");

                                                    UpdateMainMessage("���i�F�����A�������t�ɋC���g�킹������Ă�݂����B�A���K�g�B");

                                                    UpdateMainMessage("���i�F�����A���Ⴀ���x�����s�����B");
                                                }
                                                else
                                                {
                                                    if (!we.FailArea333)
                                                    {
                                                        UpdateMainMessage("���i�F�܂��ʖڂˁB�S�����ˁA���x���B");

                                                        UpdateMainMessage("�A�C���F�C�ɂ���ȁB��������͑S�R�킩��˂��B");

                                                        UpdateMainMessage("���F���[�F�ŏ��͂S�A���͂Q�A���̎��͂Q�A�����Ă܂��Q�A�Ō�͂P�D");

                                                        UpdateMainMessage("���F���[�F���ꂾ�ƒP���v�Z�Ȃ�R�Q�ʂ�ł��B�R�Q�ʂ������ŋC�y�ɒT���܂��傤�B");

                                                        UpdateMainMessage("���i�F����Ȃ񂶂�ʖڂ�B");

                                                        UpdateMainMessage("�A�C���F������ȓ˂��������Ă񂾁A���i�B���O�炵���˂����B");

                                                        UpdateMainMessage("���i�F�ʖڂȂ́B����ȕ��ɂȂ��Ă���ʖڂȂ̂�B");

                                                        UpdateMainMessage("���F���[�F�E�E�E���i����A����ł͂��肢���܂��B");

                                                        UpdateMainMessage("�A�C���F���F���[�܂ŉ��ł���Ȉ�������񂾁B�ǂ�����˂����R�Q����΁B");

                                                        UpdateMainMessage("���F���[�F���āA���i����͐^���݂����ł��B�ז����Ȃ��ł����܂��傤�B");

                                                        UpdateMainMessage("���i�F�����A�s�����B���ĂČ������B");
                                                    }
                                                    else
                                                    {
                                                        if (!we.FailArea334)
                                                        {
                                                            UpdateMainMessage("���i�F����������ˁE�E�E���ŉ������Ă��ʖڂȂ̂�B");

                                                            UpdateMainMessage("�A�C���F�E�E�E�킩��˂��A����ݒׂ�������������Ă̂̓V���h�C���񂾂ȁB");

                                                            UpdateMainMessage("���i�F�˂��A�r���ŉ������Ȃ������H�ςȌ��݂����Ȃ́B");

                                                            UpdateMainMessage("�A�C���F������H���̕ϓN���˂������΂��肾���B");

                                                            UpdateMainMessage("���i�F�ŏ��̂S���̕��������͂�������ƌ������́B���̕��ɁB");

                                                            UpdateMainMessage("�A�C���F�������钎�ł�����񂾂�B");

                                                            UpdateMainMessage("���F���[�F���钎�ł����H�ł��{�N��������蒎�͋��܂���ł�����B");

                                                            UpdateMainMessage("���i�F�������́B���x���m���߂Ă݂��B�����A�s���܂��傤�B");

                                                            UpdateMainMessage("�A�C���F�E�E�E�����B");
                                                        }
                                                        else
                                                        {
                                                            if (!we.FailArea335)
                                                            {
                                                                UpdateMainMessage("���i�F�ςȌ��A�܂������Ă���B�ԈႢ�Ȃ��A�ŏ��͐��ʏ����B");

                                                                UpdateMainMessage("�A�C���F�E�E�E���i�A�C�����͕����邪�A����Ȃ���͂��˂��B");

                                                                UpdateMainMessage("���i�F����A�A�C���̃o�J�ɂ͌����Ȃ������ł���B");

                                                                UpdateMainMessage("���F���[�F�{�N�����������܂���ł����B���i����{���Ɍ���������ł����H");

                                                                UpdateMainMessage("���i�F�E�E�E");

                                                                UpdateMainMessage("�A�C���F���i�A���������������B����Ȗ������͗ǂ�����");

                                                                UpdateMainMessage("���i�F�����́A�����Ă邩��B�Q�ڂ̕��������ʂ̏����������������Ă���B");

                                                                UpdateMainMessage("�A�C���F�E�E�E���O�A���v�Ȃ񂾂�ȁH�S�z�����猾���Ă�񂾂��B");

                                                                UpdateMainMessage("���F���[�F�E�E�E�ł́A���i���񂨊肢���܂��B");

                                                                UpdateMainMessage("���i�F���A�����C�����ǂ��́B�����A�s������");
                                                            }
                                                            else
                                                            {
                                                                if (!we.FailArea336)
                                                                {
                                                                    UpdateMainMessage("�A�C���F���i�A���O��F�������ǂ��ȁB");

                                                                    UpdateMainMessage("���i�F�����H�܂����ꂾ�����s������������ǁA�C�����͗ǂ�����");

                                                                    UpdateMainMessage("���F���[�F�E�E�E���i����A���҂��Ă܂���B�撣���Ă��������B");

                                                                    UpdateMainMessage("���i�F�����I�C���Ă��傤�������@�R�ڂ̕����͒����E��肪�����Ă����");

                                                                    UpdateMainMessage("�A�C���F�����A���F���[�B������Ƃ������B���ł���Ȃ���������񂾁H");

                                                                    UpdateMainMessage("���F���[�F�A�C���N�A�M���͗D���������ł��B");

                                                                    UpdateMainMessage("�A�C���F�ǂ������Ӗ����B���i�͊m���Ɋ�F���ǂ��A�e���V�������ǂ��������B");

                                                                    UpdateMainMessage("�A�C���F�����ȁA����ȃ��i�͌������Ƃ��˂��B���͐S�z�Ȃ񂾂�B");

                                                                    UpdateMainMessage("���F���[�F����̓A�C���N�����i����̎������܂ł悭���Ă��Ȃ������ł��B");

                                                                    UpdateMainMessage("�A�C���F���H�ǂ��������Ƃ��H");

                                                                    UpdateMainMessage("���i�F�����A�A�C���s�����B���x�������ĂĂ������B�C���Ȃ�����");

                                                                    UpdateMainMessage("�A�C���F���H���A�����E�E�E������s�����B");
                                                                }
                                                                else
                                                                {
                                                                    if (!we.FailArea337)
                                                                    {
                                                                        UpdateMainMessage("���i�F�A�C���A������́B�����n�b�L���ƁB");

                                                                        UpdateMainMessage("���i�F�P�ڂ̕����A�E����B");

                                                                        UpdateMainMessage("���i�F�Q�ڂ̕����A�E����B");

                                                                        UpdateMainMessage("���i�F�R�ڂ̕����A�����E�A�����āB");

                                                                        UpdateMainMessage("���i�F�S�ڂ̕����A�E����B");

                                                                        UpdateMainMessage("�A�C���F���O�E�E�E��F�����邷���邼�B�{���Ƀ��i���H");

                                                                        UpdateMainMessage("���F���[�F�E����A�E����A�����E�A�E����B�s���܂��傤�B");

                                                                        UpdateMainMessage("���i�F�A�C���A������ȕs�������Ȋ炵�Ă�̂�B�z���z���A�������ƍs�������");

                                                                        UpdateMainMessage("�A�C���F���A���������������Ă���Șr��������Ȃ�A���������A�����������炳�B");
                                                                    }
                                                                    else
                                                                    {
                                                                        UpdateMainMessage("���i�F�E����A�E����A�����E�A�E����B�C�����ǂ�����");

                                                                        UpdateMainMessage("�A�C���F�E�E�E�����A�������ȁB");

                                                                        UpdateMainMessage("���i�F���x�����ԈႦ�Ȃ��悤�ɁA�����A�s���܂��傤��");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                            UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                            UpdatePlayerLocationInfo(basePosX + moveLen * 2, basePosY + moveLen * 4);
                                            SetupDungeonMapping(3, false);
                                            UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                        }
                                        else
                                        {
                                            UpdateMainMessage("���i�F�����͈�x�˔j���Ă����B�ǂ�����A�܂������Ă݂�H");

                                            using (YesNoRequestMini yesno = new YesNoRequestMini())
                                            {
                                                yesno.Location = new Point(this.Location.X + 440, this.Location.Y + 440);
                                                yesno.ShowDialog();
                                                if (yesno.DialogResult == DialogResult.Yes)
                                                {
                                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                                    UpdatePlayerLocationInfo(basePosX + moveLen * 2, basePosY + moveLen * 4);
                                                    SetupDungeonMapping(3, false);
                                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
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

                                case 37: // �J�n���[�v�R�P�U
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 19, basePosY + moveLen * 8);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    we.ProgressArea3316 = true;
                                    return;

                                case 38: // �J�n���[�v�R�P�V
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 20, basePosY + moveLen * 8);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    we.ProgressArea3317 = true;
                                    return;

                                case 39: // �J�n���[�v�R�P�W
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 24, basePosY + moveLen * 12);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    we.ProgressArea3318 = true;
                                    return;

                                case 40: // �J�n���[�v�R�P�X
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 24, basePosY + moveLen * 13);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    we.ProgressArea3319 = true;
                                    return;

                                case 41: // �J�n���[�v�R�Q�O
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 28, basePosY + moveLen * 2);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    we.ProgressArea3320 = true;
                                    return;

                                case 42: // �J�n���[�v�R�Q�P
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 25, basePosY + moveLen * 6);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    we.ProgressArea3321 = true;
                                    return;

                                case 43: // �J�n���[�v�R�Q�Q
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 25, basePosY + moveLen * 2);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    we.ProgressArea3322 = true;
                                    return;

                                case 44: // �J�n���[�v�R�Q�R
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 28, basePosY + moveLen * 6);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    we.ProgressArea3323 = true;
                                    return;

                                case 45: // �J�n���[�v�R�Q�S
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 22, basePosY + moveLen * 2);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    we.ProgressArea3324 = true;
                                    return;

                                case 46: // �J�n���[�v�R�Q�T
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 23, basePosY + moveLen * 6);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    we.ProgressArea3325 = true;
                                    return;

                                case 47: // �J�n���[�v�R�Q�U
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 0, basePosY + moveLen * 13);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    we.ProgressArea3326 = true;
                                    return;

                                case 48: // �J�n���[�v�R�Q�V
                                    UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                    UpdatePlayerLocationInfo(basePosX + moveLen * 2, basePosY + moveLen * 13);
                                    SetupDungeonMapping(3, false);
                                    UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    we.ProgressArea3327 = true;
                                    return;

                                case 49: // �J�n���[�v�R�Q�W
                                    if (we.ProgressArea3319 && we.ProgressArea3322 && we.ProgressArea3325 && we.ProgressArea3326)
                                    {
                                        UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                        UpdatePlayerLocationInfo(basePosX + moveLen * 6, basePosY + moveLen * 6);
                                        SetupDungeonMapping(3, false);
                                        UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                        if (!we.FailArea331)
                                        {
                                            UpdateMainMessage("���i�F�������I�I�ꔭ�N���A����");

                                            // [�x��]�F���ʃt���O������ƕ���̖ʔ������c��݂܂��B

                                            UpdateMainMessage("�A�C���F���E�E�E�������A��������E�E�E����������˂����I���i�I�I");
                                        }
                                        else
                                        {
                                            UpdateMainMessage("���F���[�F���܂����ˁB�R�R�͌��̏ꏊ�ł���B�߂��Ă��ꂽ��ł��ˁB");

                                            UpdateMainMessage("�A�C���F��������I���x���~�X�������̂̂��������˂����I���i�I�I");
                                        }

                                        UpdateMainMessage("�A�C���F�I�@���i�I�H");

                                        UpdateMainMessage("        �w���i�̊�͕��i�̃g�p�[�Y�F���甖���F�ɂȂ��Ă����B�x");

                                        UpdateMainMessage("���i�F�A�C���A�ƂĂ��C�����ǂ���B�s���܂��傤�A�Ō�֖̊�ցB");

                                        UpdateMainMessage("���i�F���̑O�ɁA�q���g�������Ŕ����邩�炻�����܂����܂��傤�B");

                                        UpdateMainMessage("�A�C���F���i���O�E�E�E����A���������B�܂��q���g�ŔɌ������Ƃ��邩�B");

                                        we.CompleteArea33 = true;
                                    }
                                    else
                                    {
                                        UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                        UpdatePlayerLocationInfo(basePosX + moveLen * 4, basePosY + moveLen * 7);
                                        SetupDungeonMapping(3, false);
                                        UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
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
                                    #region "��l�֖�"
                                    if (!we.InfoArea34)
                                    {
                                        UpdateMainMessage("�A�C���F���ƁA�{���ɊŔ����邺�A�ȂɂȂɁH");

                                        UpdateMainMessage("        �w�v�����܂܂ɃC���[�W���ꂽ����������B�x");

                                        UpdateMainMessage("�A�C���F���E�E�E�����A���i�������Ă�ςȌ��̎�����˂����낤�ȁH");

                                        UpdateMainMessage("���i�F���Ă��āA�R�b�`���");

                                        UpdateMainMessage("�A�C���F�����s���̂���B���O�����Ɠǂ񂾂̂���H�����̃����͂ǂ������񂾁B");

                                        UpdateMainMessage("���i�F��������ǂ�����A�b�l��");

                                        UpdateMainMessage("�A�C���F�w�b�l��x���Ăǂ�ȑ䎌����A���肦�˂�����ɂȂ��Ă邼�E�E�E");
                                        we.InfoArea34 = true;
                                    }
                                    else
                                    {
                                        if (!we.CompleteArea34)
                                        {
                                            UpdateMainMessage("        �w�v�����܂܂ɃC���[�W���ꂽ����������B�x");
                                        }
                                        else
                                        {
                                            UpdateMainMessage("�Ŕ͂��������Ȃ��Ă���B", true);
                                        }
                                    }
                                    return;

                                case 51:
                                    if (!we.SolveArea34)
                                    {
                                        UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                        UpdatePlayerLocationInfo(basePosX + moveLen * 19, basePosY + moveLen * 15);
                                        SetupDungeonMapping(3, false);
                                        UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");

                                        GroundOne.StopDungeonMusic();

                                        UpdateMainMessage("�A�C���F���ȁE�E�E���������͂����������I�I�I�I�I�I�H");

                                        UpdateMainMessage("        �w������ʂɃM�b�V���Ƌ�������ł���B�x");

                                        UpdateMainMessage("���F���[�F������ʁE�E�E�����E�E�E���A����́E�E�E10000�ӏ��͂�����Ȃ��B");

                                        UpdateMainMessage("�A�C���F�����A�������Ŕ����邼�E�E�E�ȂɂȂɁH");

                                        UpdateMainMessage("        �w��Ύ����F���A�����������B�����ω��B�x");

                                        UpdateMainMessage("�A�C���F��������A�ǂ�����������H");

                                        UpdateMainMessage("���F���[�F�����ω��E�E�E�܂�A��x�_���W�������o���瓚�����ς��Ƃ������ł��傤�B");

                                        UpdateMainMessage("�A�C���F�o�J�ȁI�H���s�����瓚�����ς����Ă̂���I�H");

                                        UpdateMainMessage("���F���[�F�����ł��ˁB����ł�10000�������Ƃ��Ă��Ӗ�������܂���B");

                                        UpdateMainMessage("�A�C���F���i�A�ǂ�����E�E�E�E�E�E���i�A�������i�H");

                                        UpdateMainMessage("���i�F�����A�����Ă������B�ǂ��āB");

                                        UpdateMainMessage("�A�C���F���i�E�E�E�E�E�E");

                                        UpdateMainMessage("�@�@�@�@�w�˔@��������i�̑O�ɕ����オ�����B����A�^�g�̋�ԂɃ��i����܂ꂽ�B�x");

                                        GroundOne.PlayDungeonMusic(Database.BGM09, Database.BGM09LoopBegin);

                                        UpdateMainMessage("���i�F�y�����`�@�����������킯����Ȃ��B���������Ă�킯���Ȃ��B�z");

                                        UpdateMainMessage("���i�F�y�����a�@���Ȃ����������̂ł��傤�H���Ȃ������������邩��ł��傤�H�z");

                                        UpdateMainMessage("���i�F�y�����b�@���߂�ꂽ���Ȃ��B���߂����Ȃ��B���߂Ă��܂������Ȃ��B���߂��Ȃ��B�z");

                                        UpdateMainMessage("���i�F�y�����c�@���߂��́B�������߂��̂�B���߂����͌��肳�ꂽ�����ɂȂ�́B�z");

                                        UpdateMainMessage("�@�@�@�@�w���i����ł����Ԃ��Ή��F�ւƌ������ς�����I�I�x");

                                        UpdateMainMessage("�A�C���F�����A��������B�ӂ����񂶂�˂��A����Ȃ̎~�߂�I");

                                        UpdateMainMessage("���F���[�F�A�C���N�A����āB���̃��i����ɐG��Ă͑ʖڂł��B");

                                        UpdateMainMessage("���i�F�y�����`�@���߂���ł��傤�H���Ȃ����g�����߂���ł��傤�H���Ȃ����g�����߂Ă��܂�����ł��傤�H�z");

                                        UpdateMainMessage("���i�F�y�����a�@�������߂��́B�����Ō��߂��́B�����̔��f�Ō��߂��́B���̈ӎu�Ō��߂��́B�z");

                                        UpdateMainMessage("���i�F�y�����b�@�y�ɂȂ��͂��B�s���ɂȂ�Ȃ��͂��B���S�ł���͂��B����Ȃ��Ȃ�͂��B�z");

                                        UpdateMainMessage("���i�F�y�����c�@�y�ɂȂ�����B���߂Ă悩�����B���@�ł��Ă悩�����B��������Ȃ��́B�z");

                                        UpdateMainMessage("���i�F�y�����d�@�悩�����̂�B�y�ɂȂ�āA���߂��āA���@�ł��āA�����������Ȃ��āB�z");

                                        UpdateMainMessage("�@�@�@�@�w���i����ł����Ԃ��Ő��F�ւƌ������ς�����I�I�x");

                                        UpdateMainMessage("�A�C���F�~�߂�E�E�E�ǂ����烉�i�I�~�߂�I�������Ă񂾂��O�~�߂���āI�I�I");

                                        UpdateMainMessage("���F���[�F�A�C���N�A���v�ł��E�E�E���v�ł�����B����Ă��������I");

                                        UpdateMainMessage("���i�F�y�]���`�@�����玸�s����́B������~�X����́B������ʖڂȂ́B������ÈłȂ́B�z");

                                        UpdateMainMessage("���i�F�y�]���a�@�N�̂����H���Ȃ��ł��傤�H���Ȃ������肷�邩��ł��傤�H���Ȃ��������̂�H�z");

                                        UpdateMainMessage("���i�F�y�]���b�@�����A���̂�����A���̌���̂�����B���������́B�N�������Ȃ��́B�z");

                                        UpdateMainMessage("���i�F�y�]���c�@���������́B����ŗǂ��́B�N�������Ȃ��́B�N�����Ȏv�������Ȃ��́B�z");

                                        UpdateMainMessage("���i�F�y�]���d�@�s�K�ƍK���͓��������́B���s�����������������́B�ǂ������������������́B�z");

                                        UpdateMainMessage("�@�@�@�@�w���i����ł����Ԃ������������Ȃ������F�ւƌ������ς�����I�I�x");

                                        UpdateMainMessage("�A�C���F�~�߂Ă���E�E�E�~�߂Ă����E�E�E�I�@����Ȃ̃��i����˂��E�E�E�I�I");

                                        UpdateMainMessage("���F���[�F�A�C���N�E�E�E���������ł��B�h�����Ă��������B");

                                        UpdateMainMessage("���i�F�y�I���`�@���������A�����Ɉł������́B�����������ɑS�Ă��l�܂��Ă���́B�z");

                                        UpdateMainMessage("���i�F�y�I���a�@�I��点�����Ȃ������́B�����玄���S�������Ă�����́B��������ΏI���Ȃ��B�z");

                                        UpdateMainMessage("���i�F�y�I���b�@���Ȃ��̋��Ȃ��ꏊ�A���Ȃ��������ꏊ�A���Ȃ�������ꏊ�A���Ȃ��������ꏊ�ցB�z");

                                        GroundOne.StopDungeonMusic();

                                        UpdateMainMessage("�@�@�@�@�w��Ԃ��������t���b�V�����A�Ïk���ꂽ��ԂւƘA���I�ɏ������Ȃ�I�I�I�x");

                                        UpdateMainMessage("�@�@�@�@�w�p�p�p�p�p�p�p�I�I�I�I�b�o�V���E�E�E�D�D�D���I�I�I�I�I�I�I�x");

                                        UpdateMainMessage("�A�C���F�������I�E�A�A�A�A�@�@�@�@�I�I�I�I");

                                        UpdateMainMessage("���F���[�F���A����́I�I�I�A�A�A�@�@�@�@�I�I�I");

                                        UpdateMainMessage("�@�@�@�@�w��Ԃ͒e����񂾌�A����̑O�Ƀ��i�̓|�ꂽ�p���������x");

                                        UpdateMainMessage("�A�C���F���i�I�I�I");

                                        UpdateMainMessage("���i�F�E�E�E�E������E�E�E�����Ȃ���E�E�E���H");

                                        UpdateMainMessage("�A�C���F�����A�������I���i�������肵��I���i�I�I");

                                        UpdateMainMessage("���i�F��A���I�����������ƁA�߂������́I�ǂ��Ȃ�����I���́I�I");

                                        UpdateMainMessage("�A�C���F���O������ȁE�E�E����ȔY�ނȂ�I����ȋꂵ�ނȂ�I���ł���ȍl���Ă񂾂�I�I");

                                        UpdateMainMessage("�A�C���F�����o�J����Ă񂾂낤���I�΂���I�����b�N�X�����I�I����ȋꂵ���炷��Ȃ�I�I�I");

                                        UpdateMainMessage("���i�F��A�������Ă�̃R�C�c�B���剽�Ń��C�g�j���O�L�b�N�����Ȃ��̂�B�|��Ȃ�����A�������E�E�E");

                                        using (MessageDisplay md = new MessageDisplay())
                                        {
                                            md.StartPosition = FormStartPosition.CenterParent;
                                            md.Message = "�A�C���A���i�A���F���[�͂��΂炭���̏�ŋx����������B";
                                            md.ShowDialog();
                                        }
                                        we.SolveArea34 = true;

                                        UpdateMainMessage("�A�C���F�Ȃ��A���i�B���O�������v�Ȃ̂��H");

                                        UpdateMainMessage("���i�F���`�E�E�E������ƁE�E�E��ꂽ�����ˁB");

                                        UpdateMainMessage("�A�C���F�����A���O������ȔY��ł�Ƃ��A�������Ȃ����������Ȃ̂�������˂��B");

                                        UpdateMainMessage("���i�F�����H�����Y��ł鏊�Ȃ�Č����������H");

                                        UpdateMainMessage("�A�C���F�������Ă�B�������̑���ɋ������O�A���������⎩�����b�V�����������B");

                                        UpdateMainMessage("���i�F�����A�A���̎��H����͂ˁB");

                                        UpdateMainMessage("���F���[�F���̎O�ʋ��ł��ˁB");

                                        UpdateMainMessage("���i�F�����A���̋��ɉ��x���G��Ă���E�`�ɕ������Ă��Ă��́B");

                                        UpdateMainMessage("���i�F�N���ƒN���������Ă���̂�B���͂���������猩�Ă�́B");

                                        UpdateMainMessage("�A�C���F�N���ƒN�����ĒN����H");

                                        UpdateMainMessage("���i�F����Ȃ̕�����킯�Ȃ�����Ȃ��B���������������Ƃ����C���[�W�������c���Ă�́B");

                                        UpdateMainMessage("���i�F���̑���̑O�ŋ}�Ƀ����������Ƃ����C���[�W���N���[���ɂȂ���");

                                        UpdateMainMessage("���F���[�F����ŁA���܂ŋ��ŕ����ė������e�����̂܂܏o���̂ł��ˁH");

                                        UpdateMainMessage("���i�F���[��A�ł������悭�v���o���Ȃ��̂�ˁB���A�Y��Ă���B���������΁B");

                                        UpdateMainMessage("���i�F���̕����̓����A�m���Ă���B���Ă��ā�");

                                        UpdateMainMessage("�A�C���F���i�A���O�E�E�E������̕��A���v����ȁH");

                                        UpdateMainMessage("        �w���i�̊�͕��i�̃g�p�[�Y�F�ɖ߂��Ă����B�x");

                                        UpdateMainMessage("���i�F����H���匩�Ȃ��ł�B�@�A�C���̕����ς�H�A���^�o�J�ɖ������������񂶂�Ȃ��H");

                                        UpdateMainMessage("�A�C���F�E�E�E�b�t�E�E�E�b�n�b�n�b�n�b�n�I�I�����A�o�J���I�I�I");

                                        UpdateMainMessage("���i�F����E�E�E�����ŔF�߂Ă邵�B���v������R�C�c�B�@�������A��������B");

                                        UpdateMainMessage("���F���[�F���i����A����ȑ�R�̋�������̂ɁA�{���ɕ�����̂ł����H");

                                        UpdateMainMessage("���i�F�Ō�ɑ���������鎞�ɂˁA�����Ă�������́A�Y��Ȑ���������B���v���");

                                        UpdateMainMessage("�A�C���F��������A���Ⴀ�s���Ă݂悤���I");

                                        GroundOne.PlayDungeonMusic(Database.BGM02, Database.BGM02LoopBegin);
                                    }
                                    else
                                    {
                                        UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                        UpdatePlayerLocationInfo(basePosX + moveLen * 19, basePosY + moveLen * 15);
                                        SetupDungeonMapping(3, false);
                                        UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    }
                                    return;

                                case 52:
                                    if (!we.CompleteArea34)
                                    {
                                        UpdateMainMessage("���i�F�ςȌ��E�E�E���������Ȃ��Ȃ���������B�c�O���ȁ�");

                                        UpdateMainMessage("�A�C���F��H�������������H");

                                        UpdateMainMessage("���i�F������A���ł�������B�����A�s������");

                                        UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                        UpdatePlayerLocationInfo(basePosX + moveLen * 3, basePosY + moveLen * 9);
                                        SetupDungeonMapping(3, false);
                                        UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");

                                        UpdateMainMessage("�A�C���F�����A�ʂ̒n�_�ɏo���ȁB������ȃ��i�I���O�z���g�o��������ȁI�b�n�b�n�b�n�I");

                                        GroundOne.StopDungeonMusic();

                                        UpdateMainMessage("���i�F�E�E�E�E�E�E�@�i�p�^�b�j");

                                        UpdateMainMessage("        �w���i�͉�������Ȃ��܂܁A���̏�ŐÂ��ɓ|�ꍞ��ł��܂����B�x");

                                        UpdateMainMessage("�A�C���F���i�I�I�I�I�I�I�I�I");

                                        UpdateMainMessage("���F���[�F�A�C���N�I�I�y�����̐����z�𑁂��I�I");

                                        UpdateMainMessage("�A�C���F���A�������ȁI�����A��Ė߂邼�I���Ⴗ���g�����I");

                                        CallHomeTown(true);
                                    }
                                    else
                                    {
                                        UpdateMainMessage("�@�@�@�w���i�����̎O�ʋ���`�����݁A���L�΂����u�ԁA���������P���n�߂��I�x�@�@");
                                        UpdatePlayerLocationInfo(basePosX + moveLen * 3, basePosY + moveLen * 9);
                                        SetupDungeonMapping(3, false);
                                        UpdateMainMessage("�@�@�@�w�b�o�V���I�I�I�x�@�@");
                                    }
                                    return;

                                case 53:
                                    we.Treasure121 = GetTreasure("�v���[�g�E�A�[�}�[");
                                    return;

                                case 54:
                                    we.Treasure122 = GetTreasure("�������E�A�[�}�[");
                                    return;

                                case 55:
                                    we.Treasure123 = GetTreasure("�V�����V�[��");
                                    return;
                                #endregion

                                case 56:
                                    if (!we.InfoArea35)
                                    {
                                        UpdateMainMessage("�A�C���F�����I�H�Ȃ񂾃R�R�́I�H");

                                        UpdateMainMessage("���F���[�F����́E�E�E�ǂ����B���ʘH�̂悤�ł��ˁB");

                                        UpdateMainMessage("���i�F�悭����ȏ���������ˁB");

                                        UpdateMainMessage("�A�C���F�ӂƉ��̕ǂ��C�ɂȂ��ĂȁB�����ɉ����Ă݂���J�������Ă킯���B");

                                        UpdateMainMessage("���i�F�܂��e�L�g�[�ȏ��G���ĕςȃg���b�v���������Ȃ��ł�ˁH");

                                        UpdateMainMessage("�A�C���F�܂��A�ǂ�����˂����B���܂ɂ͂��������̂��A������B");

                                        UpdateMainMessage("���F���[�F�B���ʘH�n���́A�M�d�ȃA�C�e���A�m�b�A���Ȃǂ��B����Ă���ꍇ������܂��B");

                                        UpdateMainMessage("�A�C���F������I�s���Ă݂悤���I");

                                        we.InfoArea35 = true;
                                    }
                                    return;

                                case 57:
                                    if (!we.SpecialInfo3)
                                    {
                                        UpdateMainMessage("�A�C���F��H���������Ă���ȁE�E�E");

                                        GroundOne.StopDungeonMusic();
                                        this.BackColor = Color.Black;
                                        UpdateMainMessage("�@�@�@�y���̏u�ԁA�A�C���̔]���Ɍ��������ɂ��P�����I���͂̊��o����Ⴢ���I�I�z");

                                        UpdateMainMessage("�A�C���F�b�O�E�E�E�Ȃ񂾁E�E�E����E�E�E�O�A�A�A�I");

                                        UpdateMainMessage("�@�@�������@�S�Ă����Ԃ����Ƃ�����H ������");

                                        UpdateMainMessage("�A�C���F�b�e�E�E�E�C�e�e�e�E�E�E�N���I�������傤�I");

                                        UpdateMainMessage("�@�@�������@���̌��i���S�Č��z���Ƃ�����H�@������");

                                        UpdateMainMessage("�A�C���F���z�E�E�E���ƁI�H�E�E�E�C�b�c�c�c�E�E�E�������āE�E�E");

                                        UpdateMainMessage("�@�@�������@���߂���S�Ă��Ԉ���Ă���̂��Ƃ�����H�@������");

                                        UpdateMainMessage("�A�C���F�Ԉ���Ă�E�E�E�E��ȁE�E�E�킯���E�E�E�O�A�A�I");

                                        UpdateMainMessage("�@�@�������@�I���ւƑ����^�ԂȁB�@�n�܂�ւƑ���i�߂�B�@������");

                                        UpdateMainMessage("�A�C���F���E�E�E�����āE�E�E�₪�E�E�E");

                                        UpdateMainMessage("�@�@�@�y�A�C���ɑ΂��錃�������ɂ͏����������Ă������B�z");

                                        this.BackColor = Color.RoyalBlue;

                                        UpdateMainMessage("���i�F�A�C���H������ƃA�C���I�H");

                                        UpdateMainMessage("�A�C���F�E�E�E�����E�E�E��H");

                                        UpdateMainMessage("���i�F�����A�悤�₭�N������ˁB���v�H");

                                        UpdateMainMessage("�A�C���F��A�����A���v���B�S�z����ȁB");

                                        UpdateMainMessage("���i�F���܂ŕ����Ă����c���ǂ����v�����Č����̂�A�{���ɑ��v�H");

                                        UpdateMainMessage("���F���[�F�A�C���N�A��̉���������ł����H�����ɂ͉���������ĂȂ��悤�ł���");

                                        UpdateMainMessage("�A�C���F����A�ǂ��o���Ă˂����A�u�I���ւƑ����^�Ԃȁv�Ƃ����Ƃ�");

                                        UpdateMainMessage("���F���[�F�E�E�E");

                                        UpdateMainMessage("�A�C���F�܂������̃g���b�v�������񂾂�B�������A�^�`�̈��������点���ȁB");

                                        UpdateMainMessage("���F���[�F�E�E�E�����A�B���ʘH������ƌ����ĕK����������Ƃ����킯�ɂ͂����Ȃ����ł��ˁB");

                                        UpdateMainMessage("���i�F�܂��A�債�����Ȃ������ŗǂ�������B�����A�߂�܂���H");

                                        UpdateMainMessage("�A�C���F�����A�������ȁB");

                                        GroundOne.PlayDungeonMusic(Database.BGM02, Database.BGM02LoopBegin);
                                        we.SpecialInfo3 = true;
                                    }
                                    return;

                                default:
                                    mainMessage.Text = "�A�C���F��H���ɉ����Ȃ������Ǝv�����B";
                                    return;
                            }
                        }
                    }
                }
                #endregion
                #region "�_���W�����S�K�C�x���g"
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
                                    mainMessage.Text = "�A�C���F�R�K�֖߂�K�i���ȁB�����͈�U�߂邩�H";
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
                                        UpdateMainMessage("�A�C���F�E�E�E�����A���������B");

                                        UpdateMainMessage("���i�F�E�E�E");

                                        UpdateMainMessage("���F���[�F�E�E�E�ŔE�E�E�ł��ˁH");

                                        UpdateMainMessage("�A�C���F�����B");

                                        UpdateMainMessage("���F���[�F�A�C���N�A���Ə����Ă���܂��H");

                                        UpdateMainMessage("�A�C���F�E�E�E���������Ă��邺�B");

                                        if (!we.FailArea4211 && !we.FailArea4212)
                                        {
                                            UpdateMainMessage("�@�@�@�@�w�^���̌��t�S�F�@�@���Ǝ��@�@�@�x");
                                            we.SpecialInfo4 = true;
                                        }
                                        else
                                        {
                                            UpdateMainMessage("�@�@�@�@�w�^���̌��t�S�F�@�@���@�@�x");
                                        }
                                        UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E�@���i�B");

                                        UpdateMainMessage("���i�F�E�E�E");

                                        UpdateMainMessage("�A�C���F���̊Ŕ̌��t�A�ƂĂ�����˂����y����k�Ƃ͎v���˂��B");

                                        UpdateMainMessage("���i�F�E�E�E");

                                        UpdateMainMessage("���F���[�F�A�C���N�A��U�ŉ��w�̂T�K�֍~��āA�_���W�����̊O�֏o�܂��傤�B");

                                        UpdateMainMessage("�A�C���F���A�����A�������ȁE�E�E�������邩�B�b�͊O�֏o�Ă��炾�B");
                                        we.TruthWord4 = true;
                                        we.InfoArea420 = true;
                                    }
                                    else
                                    {
                                        if (we.SpecialInfo4)
                                        {
                                            UpdateMainMessage("�@�@�@�@�w�^���̌��t�S�F�@�@���Ǝ��@�@�x", true);
                                        }
                                        else
                                        {
                                            UpdateMainMessage("�@�@�@�@�w�^���̌��t�S�F�@�@���@�@�x", true);
                                        }
                                    }
                                    return;
                                case 2:
                                    mainMessage.Text = "�A�C���F�ŉ��w�T�K�ւ̊K�i���B�~��邺�H";
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
                                                UpdateMainMessage("�A�C���F�悵�A���Ⴀ�y�����̐����z���g�����B");
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
                                    mainMessage.Text = "�A�C���F�{�X�Ƃ̐퓬���I�C���������߂Ă������I";
                                    mainMessage.Update();
                                    bool result = EncountBattle("�l�K�̎��ҁFAltomo");
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
                                        UpdateMainMessage("�A�C���F�E�E�E�����˂��ȁB�ŔƂ��o�Ă��Ȃ��̂���H");

                                        UpdateMainMessage("���i�F�ǂ�����Ȃ��A�ʂɉ���������΁B����������Ȃ��킯�H");

                                        UpdateMainMessage("�A�C���F����A���̂܂܍Ō�܂ōs�����Ă����΃��b�L�[���ȂƎv���Ă�B");

                                        UpdateMainMessage("���i�F�C�ɂ�������B�R�R�܂ŗ����l���̂����Ȃ��񂾂��A�ӊO�Ƃ���ȕ��Ȃ񂶂�Ȃ��H");

                                        UpdateMainMessage("�A�C���F�����A�������ȁB��������A���̂܂ܐi�������I");

                                        UpdateMainMessage("���i�F�����A�s���܂���B");

                                        UpdateMainMessage("        �u���i���O�֏����i�ނ̂�������A�A�C���̌�납�琺�������B�v");

                                        UpdateMainMessage("���F���[�F�A�C���N�B������Ƃ�����ցB");

                                        UpdateMainMessage("�A�C���F�����A���F���[�B���̗p���H");

                                        UpdateMainMessage("���F���[�F�A�C���N�ɂ͂ǂ����Ă��b���Ă����˂΂Ȃ�Ȃ���������܂��B");

                                        UpdateMainMessage("�A�C���F���̘b���H�����Ă݂Ă����B");

                                        UpdateMainMessage("���F���[�F�E�E�E���i����̎��ł��B");

                                        UpdateMainMessage("�A�C���F���i�H�A�C�c����������̂��H");

                                        UpdateMainMessage("���F���[�F�ޏ��͂��̃_���W������i�ޏ�ŁA�d��Ȏ������B���Ă܂��B");

                                        UpdateMainMessage("�A�C���F�d��Ȏ����H��̂ǂ����������H");

                                        UpdateMainMessage("���F���[�F����͂ł��ˁE�E�E");

                                        UpdateMainMessage("        �u���i�F�I�[�C�A�����̃o�J�[�I���܂œ˂������Ă�̂�H���F���[�����������v");

                                        UpdateMainMessage("�A�C���F���A�����I���s�����I");

                                        we.InfoArea41 = true;
                                    }
                                    return;

                                case 5:
                                    if (!we.InfoArea42)
                                    {
                                        UpdateMainMessage("���i�F�E�E�E�˂��A�o�J�A�C���B");

                                        UpdateMainMessage("�A�C���F���ʂ̒��q�Ńo�J��t����ȁB�����H");

                                        UpdateMainMessage("���i�F���[��A���ł�������B");

                                        UpdateMainMessage("�A�C���F�����A���ł��˂��̂��B���Ⴀ���̂܂ܐ�֐i�ނ��I");

                                        UpdateMainMessage("���i�F����A�b�T�T���Ɛi�݂܂����");

                                        UpdateMainMessage("        �u�A�C�����O�֏����i�ނ̂�������A���i�̉���납�琺�������B�v");

                                        UpdateMainMessage("���F���[�F���i����A����������ցB");

                                        UpdateMainMessage("���i�F���H���F���[����B");

                                        UpdateMainMessage("���F���[�F�A�C���N�́A���̂��̃_���W�����֒���ł�̂ł����H");

                                        UpdateMainMessage("���i�F���́H���A���[��A�ŉ��w�ɓ��B�������Ƃ��P���ȗ��R��B");

                                        UpdateMainMessage("���F���[�F�Ђ���Ƃ�����ł����A�����ł͖����̂ł́H");

                                        UpdateMainMessage("���i�F�����ł͖����H�ʂ̖ړI��������Ď��H");

                                        UpdateMainMessage("���F���[�F�͂��B�{�N�͂R�K����r���Q���Ȃ̂ŕ�����܂��񂪁B");

                                        UpdateMainMessage("���F���[�F�A�C���N�͂ǂ��ƂȂ��ł����A�{�N�ɉ����B���Ă��܂��ˁB");

                                        UpdateMainMessage("���i�F���[��A����������H���̃o�J�Ɍ����Ă��蓾�Ȃ��Ǝv���񂾂��ǁB");

                                        UpdateMainMessage("���F���[�F��x����ƂȂ������Ă݂Ă��������B�X�J��������܂��񂪁B");

                                        UpdateMainMessage("        �u�A�C���F��H�����A������Ă񂾁H�����s�����I����𗐂��ȁB�v");

                                        UpdateMainMessage("���i�F����������A��ŕ����Ă݂܂��B���肪�Ƃ��A���F���[����B");

                                        UpdateMainMessage("���F���[�F�s���܂��傤�B�܂��܂���͒������ł��B");

                                        we.InfoArea42 = true;
                                    }
                                    return;

                                case 6:
                                    if (!we.InfoArea43)
                                    {
                                        UpdateMainMessage("�A�C���F�����O���O��������肾�ȁB����{���ɐi��ł�̂��H");

                                        UpdateMainMessage("���i�F�{����ˁB�������i��ł�̂��߂��Ă�̂����o�������ɂȂ��ˁB");

                                        UpdateMainMessage("�A�C���F�Ȃ��A���i�B");

                                        UpdateMainMessage("���i�F����H");

                                        UpdateMainMessage("�A�C���F���O�A�Ђ���Ƃ��Ă��B");

                                        UpdateMainMessage("���i�F�����A���������I�@�A�C���Ă��A���F���[�����DUEL�������ł���H");

                                        UpdateMainMessage("�A�C���F��H�����A�������܂������ǂȁB");

                                        UpdateMainMessage("���F���[�F���̂S�K�ɓ���O�Ƀ_���W�����Q�[�g���ŋ��R��܂��ĂˁB");

                                        UpdateMainMessage("�A�C���F���F���[�̑S�������Ăǂ�Ȋ����������񂾁H");

                                        UpdateMainMessage("���F���[�F�E�E�E�����ł��ˁA�����Č����΁g�^��h�̂悤�ȃC���[�W�����Ă��������B");

                                        UpdateMainMessage("���F���[�F��{�I�Ƀ_���[�W���[�X�͂��܂���B�ꌂ�ڂłقƂ�Ǐ��������߂銴���ł��B");

                                        UpdateMainMessage("���i�F���F���[������Đ����������[�V�����������ł���ˁI���ꋳ���Ă���܂��񂩁�");

                                        UpdateMainMessage("���F���[�F�����A�ǂ��ł���B�܂��̂̏d�S�Ǝ��̕����͏�ɏd�Ȃ�悤�ɂ��Ă��������B");

                                        UpdateMainMessage("���F���[�F���ꂩ��A���̓��ݍ��ޕ��������i����ӎ����Ă��������B");

                                        UpdateMainMessage("���F���[�F�ǂ��ł����H����������Ă݂܂��B�ǂ����ĂĂ��������ˁB");

                                        UpdateMainMessage("���F���[�F�E�E�E�b�n�C�E�E�E�t�@�C�A�E�{�[���ł��B");

                                        UpdateMainMessage("�A�C���F�Ȃ�قǂȁA�m���ɍ��̂Ȃ牴�ɂ����������B");

                                        UpdateMainMessage("���i�F�\��������ˁB�ǂ��ł���Ȃ̂��o������ł����H");

                                        UpdateMainMessage("���F���[�F�n�n�A����������ƍ����ł����A���ƂȂ��ł��B");

                                        UpdateMainMessage("���F���[�F�A�C���N�����i������\���f���͂���܂��B�܂��_���W�����Q�[�g���ɗ��Ă��������B");

                                        UpdateMainMessage("���i�F�����A�s���Ă݂��B��낵�����肢���܂���");

                                        UpdateMainMessage("�A�C���F�Ȃ��A���i�B�����ƁA���O�Ђ���Ƃ��Ă��B");

                                        UpdateMainMessage("���i�F�b�T�T�A�s���܂����");

                                        UpdateMainMessage("�A�C���F���A�����E�E�E");

                                        we.InfoArea43 = true;
                                    }
                                    return;

                                case 7:
                                    if (!we.InfoArea44)
                                    {
                                        UpdateMainMessage("�A�C���F�ǂ���甽���v���ɕς�����݂������ȁB");

                                        UpdateMainMessage("���i�F�E�E�E�������ˁB�E�E�E�˂��A�o�J�A�C���B");

                                        UpdateMainMessage("�A�C���F���ʂ̃��A�N�V�����Ƀo�J��D�������ȁB�����H");

                                        UpdateMainMessage("���i�F���[��A���ł�������B");

                                        UpdateMainMessage("�A�C���F�����A�������T���Ă��Ȃ����G���]�v�C�ɂȂ邼�B");

                                        UpdateMainMessage("���i�F�A�C���A�����Ɠ����Ȃ����B�ǂ���ˁH");

                                        UpdateMainMessage("�A�C���F�����A�����H�����Ă݂�B");

                                        UpdateMainMessage("���i�F���񂽁A�����B���Ă�ł���H");

                                        UpdateMainMessage("�A�C���F�������ĉ����H");

                                        UpdateMainMessage("���i�F�����A�C�����B���Ă鎖��B�����A�����Ă��傤�����B");

                                        UpdateMainMessage("�A�C���F�ǂ񂾂��������Ȃ񂾂�B����Ȗ������Ȏ���ɂ͓������˂��B");

                                        UpdateMainMessage("���i�F����A����ς苳�����Ȃ����e�Ȃ񂾁B�킩������B");

                                        UpdateMainMessage("�A�C���F�����҂đ҂āB�{���ɉ��̎��������Ă�񂾁H");

                                        UpdateMainMessage("���i�F�C�C���B�{���̎��Ȃ񂩓�����C�Ȃ���ł���B");

                                        UpdateMainMessage("�A�C���F���ȁI�E�E�E���Ⴀ�������ȁA���i�B");

                                        UpdateMainMessage("���i�F����H");

                                        UpdateMainMessage("�A�C���F���O�̕������A���̃_���W�����ɓ���ہA�����d��Ȏ������B���Ă邾��H");

                                        UpdateMainMessage("���i�F�����������������������I�H�I�H�I�H�I�H�I�H");

                                        UpdateMainMessage("�A�C���F�������I�f�P�G���o���ȁB������ȋ������炵�Ă񂾂�B�����������������B");

                                        UpdateMainMessage("�A�C���F�������A�ق猩��B�������ȁA�܂���ɂ��O����b����B");

                                        UpdateMainMessage("���i�F�m��Ȃ��I�I");
                                        
                                        UpdateMainMessage("���i�F���A�m��Ȃ�����ˁI�I");
                                        
                                        UpdateMainMessage("���i�F��Βm��Ȃ����I�I�I");

                                        UpdateMainMessage("���F���[�F���i����A���������ĉ������B�A�C���N���A���������ɂ͗D�����ڂ��Ă��������B");

                                        UpdateMainMessage("�A�C���F���A�����E�E�E����A���������������h���Ղ肾�ȁB���₢��A���������B");

                                        UpdateMainMessage("���i�F�E�E�E�s���܂���B");
                                        
                                        we.InfoArea44 = true;
                                    }
                                    return;

                                case 8:
                                    if (!we.InfoArea45)
                                    {
                                        UpdateMainMessage("�A�C���F�Ȃ��E�E�E���i�B");

                                        UpdateMainMessage("���i�F���H�m��Ȃ����Č����Ă邶��Ȃ��I�H");

                                        UpdateMainMessage("�A�C���F���A���₢��A�Ⴄ�Ⴄ�B���O�A�K���c�f������ɉ������Ă�����Ă񂾁H");

                                        UpdateMainMessage("���i�F���H�����Ă���Ⴀ�A���p��B");

                                        UpdateMainMessage("�A�C���F�K���c�f���͕����؂��Ǝv���Ă��񂾂��ǂȁB���p���o����̂��B");

                                        UpdateMainMessage("���i�F�����ˁA�ӊO��������B�ŏ��͏����^��Ɋ������񂾂��ǁB");

                                        UpdateMainMessage("���i�F�ŏ��̎荇�킹���Ă���������Ƀs���Ɨ����́B");

                                        UpdateMainMessage("���i�F���R�̂̍\���A���i�����ǂƂĂ��_�炩�����͋C�B���ƂȂ��N���Ɏ��Ă�C������̂�ˁB");

                                        UpdateMainMessage("���F���[�F����H�m��Ȃ��悤�ł��ˁB�K���c�E�M�����K�̓G���~�̎t���ł���B");

                                        UpdateMainMessage("�A�C���ƃ��i�F�������������������������I�I�I�I");

                                        UpdateMainMessage("���F���[�F�������T���E���h�����ł��ˁE�E�E�����܂���A�����������͂Ȃ������̂ł����B");

                                        UpdateMainMessage("���i�F�|�[�V�����쐬�A����쐬�A���p�b�B�A�������S���厖���Ă��������́B");

                                        UpdateMainMessage("���i�F���Ƃ͂����ˁB�@�i���}�l�j�w�K���c�F���X���X�Ɛ킢�Ȃ����x���ďa����Ō�����B");

                                        UpdateMainMessage("���F���[�F�G���~�̃t�F�A���_�B�����ƃK���c�E�M�����K�̉e���ł��傤�B");

                                        UpdateMainMessage("�A�C���F���F���[�ƃG���~�͓����N�Ȃ񂾂�H���F���[�͋����Ă����Ȃ������̂��H");

                                        UpdateMainMessage("���F���[�F�i���}�l�j�w�K���c�F�N�ɋ����鎖�͂Ȃ��B�N���g�̖�肾�x�Əa����Œf���܂����B");

                                        UpdateMainMessage("���i�F���F���[����A���ł��o���邵�K���c�f������͏\�����Ɣ��f�����񂶂�Ȃ�������H");

                                        UpdateMainMessage("���F���[�F�n�n�A�������Ԃ�ł��B�����{�N�͕n��ł�������B�f�������ƌ���ꂽ��ł��傤�B");

                                        UpdateMainMessage("�A�C���F���O��ǂ���ȁB���Ȃ񂩁@�i���}�l�j�w�K���c�F�A�C���A���i���Ȃ����x���������H");

                                        UpdateMainMessage("���i�F�b�t�t�A�K���c�f��������ăA�C���ɂ͂���΂������ˁ�");

                                        UpdateMainMessage("�A�C���F�����A�܂����������B���ɂ����p�̈���炢�����ė~�������񂾁B");

                                        UpdateMainMessage("���i�F�A�C���͌����������Ă邶��Ȃ��A�����������Ȃ񂶂�Ȃ��́H");

                                        UpdateMainMessage("�A�C���F�܂������A�����p���Ă̂͐��ɍ���˂��ȁB���������������m��˂��ȁB");

                                        UpdateMainMessage("�A�C���F�ł��G���~���������������񂾂낤�H");

                                        UpdateMainMessage("���F���[�F�����A�����͌����p�ł�����B�G���~�͎a��s�ׂ��̂��̂������Ă܂�������B");

                                        UpdateMainMessage("�A�C���F�����Ȃ̂��B�ŏ��͌����p�ł������猕�p�ɓ]�������񂾂ȁB");

                                        UpdateMainMessage("���i�F�����G���~�l���Ăǂ�Ȍ��p�Ȃ̂�����B��x����Ă݂�����B");

                                        UpdateMainMessage("���F���[�F�����E��������킸�A�\������΂�����ł���B�P�O�O���󂯂Ă���܂���B");

                                        UpdateMainMessage("���i�F�����I�{���ł����I�H�@�������A�C���I�\�����܂����");

                                        UpdateMainMessage("�A�C���F�b�n�b�n�b�n�I�_���W�������I����Ă���\�����Ă݂�Ƃ��邩�I�I");

                                        UpdateMainMessage("���i�F�����E�E�E�E�E�E");

                                        UpdateMainMessage("���i�F�����A���������ˁE�E�E�E�E�E�_���W�������I�������ˁB");

                                        UpdateMainMessage("�A�C���F��H�@�����A�}�ɁB");

                                        UpdateMainMessage("���i�F�����A������A�ǂ��̗ǂ��́B�����A�i�݂܂���B");

                                        UpdateMainMessage("�A�C���F�����A�K���K���i�ނƂ��邩�I");
                                        we.InfoArea45 = true;
                                    }
                                    return;

                                case 9:
                                    if (!we.InfoArea46)
                                    {
                                        UpdateMainMessage("�A�C���F�ӂ��A�ǂ�������ƃO���O���������𔲂����݂������ȁE�E�E������ꂽ���B");

                                        UpdateMainMessage("���i�F�E�E�E�A�C���B");

                                        UpdateMainMessage("�A�C���F�o�J�͕t���Ȃ��ėǂ��̂��H ������B");

                                        UpdateMainMessage("���i�F�����؂񂳁B�����̐����Œ��֖߂�Ȃ��H");

                                        UpdateMainMessage("�A�C���F���H�߂肽���̂��H���������O���O�����I�������Ȃ񂾂��ȁB");

                                        UpdateMainMessage("���i�F�����������E�E�E������ꂿ����āB�@�ǂ��A�����؂�x�e���Ȃ��H");

                                        UpdateMainMessage("�A�C���F���F���[�͂ǂ����H");

                                        UpdateMainMessage("���F���[�F�{�N�͂ǂ���ł��\���܂����B");

                                        UpdateMainMessage("�A�C���F�������Ȃ��E�E�E�ǂ��������ȁB");

                                        // [�x��]�F�Q�T�ڂ͖߂�E�߂�Ȃ��̑I�������o���Ă��������B

                                        UpdateMainMessage("�A�C���F�������ȁA���i�̑̒��𖳎��͏o���˂��B��U�߂�Ƃ��邩�B");

                                        UpdateMainMessage("���F���[�F�����Ȕ��f���Ǝv���܂��B�{�N���^���ł��ˁB");

                                        UpdateMainMessage("�A�C���F������A���Ⴀ�g�����I�I�@�w�A�C���͉����̐������g�����x");

                                        we.InfoArea46 = true;

                                        CallHomeTown();
                                    }
                                    return;

                                case 10:
                                    if (!we.InfoArea48)
                                    {
                                        UpdateMainMessage("�A�C���F������ȁA����ς肱���B");

                                        UpdateMainMessage("���i�F�ق�ƁA�悪�����Ȃ���ˁB�܂�����ł��������炢�����i�߂ĂȂ���B");

                                        UpdateMainMessage("�A�C���F���i�A������Ɨǂ����H�����ň�x�e���B");

                                        UpdateMainMessage("���i�F���A�����ǂ����B");

                                        UpdateMainMessage("�A�C���F���i�A����������Ă������Ǝv���B���͍�����낢��v���o���Ă݂��B");

                                        UpdateMainMessage("���i�F���H");

                                        UpdateMainMessage("�A�C���F���̉B�����ɂ��Ă��B");

                                        UpdateMainMessage("���i�F���A�����A�ǂ����ǂ����B�ǂ����債�����e�ł��Ȃ����낤���B");

                                        UpdateMainMessage("�A�C���F�����Ɍ������B���͉����B���Ă˂��B�{�����B");

                                        UpdateMainMessage("���i�F����A����������E�E�E");

                                        UpdateMainMessage("�A�C���F���͍���A���������B�ƂĂ���Ȗ����B");

                                        UpdateMainMessage("���F���[�F�E�E�E�ǂ�Ȗ��ł��H");

                                        UpdateMainMessage("�A�C���F���ꂪ�ȁA�S�R�����ł��˂��B�Ӗ��s�����炯�������B");

                                        UpdateMainMessage("�A�C���F���X���Ȃ�Ă��̎��́A�Ӗ��s�������ǂȁB�������ꂾ���͌�����B");

                                        UpdateMainMessage("�A�C���F���̖��̓��e���A�����B���Ă鎖�����B");

                                        UpdateMainMessage("���i�F�����ǂ��́A�ς񂾎������B�b�T�T�A��ɍs���܂����");

                                        UpdateMainMessage("�A�C���F�҂Ă惉�i�B���̓��e�A�ق�̏��������o���Ă�񂾁A�����Ă���邩�H");

                                        UpdateMainMessage("���F���[�F�A�C���N�A�\���x�߂܂�������ˁB�����͐�ɐi�݂܂��傤�B");

                                        UpdateMainMessage("�A�C���F�����E�E�E�������邩�B�����֐i�ނ��I");

                                        we.InfoArea48 = true;
                                    }
                                    return;

                                case 11:
                                    if (!we.InfoArea49)
                                    {
                                        UpdateMainMessage("�A�C���F�����ƃO�l�O�l���������B�ʓ|�������ȁE�E�E");

                                        UpdateMainMessage("���i�F����A�������ˁB");

                                        UpdateMainMessage("�A�C���F�E�E�E�C�������O���B");

                                        UpdateMainMessage("���i�F�E�E�E");

                                        UpdateMainMessage("�A�C���F�ق�̋͂������A�ڂɂ����߂��B����͊m���ɃC�������O�������B");

                                        UpdateMainMessage("���i�F�E�E�E�����B�N���̃C�������O�H����Ƃ����X�Ŕ����Ă�悤�ȁH");

                                        UpdateMainMessage("�A�C���F������˂��B�������v�l�ǐՂ���ƁA�Ђǂ����ɂ�����񂾁B");

                                        UpdateMainMessage("���F���[�F�A�C���N�A���������Ďv���o���K�v�͂���܂���B");

                                        UpdateMainMessage("�A�C���F���i�A���O�����m��Ȃ����H");

                                        UpdateMainMessage("���i�F���A�m��Ȃ���I�I�m��Ȃ����Č����Ă邶��Ȃ��̂�I�I");

                                        UpdateMainMessage("�A�C���F���A���∫���B���������ȁE�E�E��֐i�ނ��B");

                                        UpdateMainMessage("���i�F���E�E�E���߂�ˁB");

                                        UpdateMainMessage("�A�C���F�ǂ�����A�C�ɂ���Ȃ��āB�����A��i�ނ��I");

                                        UpdateMainMessage("���i�F����B");

                                        we.InfoArea49 = true;
                                    }
                                    return;

                                case 12:
                                    if (!we.InfoArea410)
                                    {
                                        UpdateMainMessage("���F���[�F�A�C���N�B");

                                        UpdateMainMessage("�A�C���F�����I�H���F���[�I�H");

                                        UpdateMainMessage("���F���[�F�����s���s�����Ȃ��ł��������B�����傤�ǔ����܂Ői�ݏI��������ł���B");

                                        UpdateMainMessage("�A�C���F�E�E�E�͂������������E�E�E�܂���������I�H");

                                        UpdateMainMessage("�A�C���F���i�A�̒��̕��͑��v���H");

                                        UpdateMainMessage("        �w�A�C�������i�̕���U����������A���i�͊�𗼎�ŕ����Ă����B�x");

                                        UpdateMainMessage("�A�C���F���ȁE�E�E���i�I���v����I�H");

                                        UpdateMainMessage("���i�F�A�C���A���E�E�E���߂�Ȃ����B�E�E�E���E�E�E���߂�Ȃ����B");

                                        UpdateMainMessage("�A�C���F���ȂɎӂ��Ă񂾁B�������̎����H�S�R�C�ɂ��Ă˂����炳�B���ȁH");

                                        UpdateMainMessage("���i�F������A�Ⴄ�́B�A�C���A���������́B�S�����Ȃ����B");

                                        UpdateMainMessage("�A�C���F�Ȃ����O���B�̂�����ςȃg�R�Ŏӂ�ȁB�C�C���Č����Ă邶��˂����A���C�o����H");

                                        UpdateMainMessage("���i�F�E�E�E����E�E�E����E�E�E�B");

                                        UpdateMainMessage("        �w���i�́A�܂�������炩�痣���Ă��Ȃ��B�x");

                                        UpdateMainMessage("���F���[�F�A�C���N�A��U�߂�܂��񂩁H���̂܂ܐi��ł͊댯�ł��B");

                                        UpdateMainMessage("�A�C���F�����A��U�����̐������g�����B���i�A��U�߂邼�H");

                                        UpdateMainMessage("���i�F�E�E�E�E�E�E");

                                        // [�x��]�F�Q�T�ڂ͖߂�E�߂�Ȃ��̑I�������o���Ă��������B

                                        UpdateMainMessage("�A�C���F������I�����̐����I");

                                        we.InfoArea410 = true;

                                        CallHomeTown();
                                    }
                                    return;

                                case 13:
                                    we.Treasure41 = GetTreasure("�G�X�p�_�X");
                                    return;

                                case 14:
                                    we.Treasure42 = GetTreasure("�O���[���}�e���A��");
                                    return;

                                case 15:
                                    we.Treasure43 = GetTreasure("����|�[�V����");
                                    return;

                                case 16:
                                    we.Treasure44 = GetTreasure("�����J�E�Z�O�����^�[�^");
                                    return;

                                case 17:
                                    we.Treasure45 = GetTreasure("�u���K���_�B��");
                                    return;

                                case 18:
                                    we.Treasure46 = GetTreasure("�A���H�C�h�E�N���X");
                                    return;

                                case 19:
                                    we.Treasure47 = GetTreasure("�����̈��");
                                    return;

                                case 20:
                                    we.Treasure48 = GetTreasure("�V�g�̌_��");
                                    return;

                                case 21:
                                    if (we.InfoArea411 && !we.InfoArea412) // InfoArea411��HomeTown�ŗ����܂��B
                                    {
                                        UpdateMainMessage("���F���[�F�A�C���N�A���i����B");

                                        UpdateMainMessage("�A�C���F�ǂ������A���F���[�B");

                                        UpdateMainMessage("���F���[�F�����A���͉B�����ɂȂ��Ă����悤�ł��B���Ă��������B");

                                        we.InfoArea412 = true;
                                        ConstructDungeonMap();
                                        SetupDungeonMapping(4);

                                        UpdateMainMessage("���i�F���A�{������B�ӊO�ȏ��ɂ��������̂ˁB");

                                        UpdateMainMessage("���F���[�F���̉�L�A���x���ʂ�C�ɂ͂Ȃ�܂��񂩂�ˁB�Ђ���Ƃ�����Ǝv���āB");

                                        UpdateMainMessage("�A�C���F���₠�A�����邺�I�T���L���[�A���F���[�I");

                                        UpdateMainMessage("���F���[�F���������A���̃_���W�����\���Ɋ��ӂ��܂��傤�B");

                                        UpdateMainMessage("���i�F���Ⴀ�A�i�݂܂��傤����");

                                        UpdateMainMessage("�A�C���F�����A����}�����I");
                                    }
                                    return;

                                case 22:
                                    if (!we.InfoArea413)
                                    {
                                        UpdateMainMessage("�A�C���F�����E�E�E���ƂȂ������ǁB");

                                        UpdateMainMessage("���i�F�E�E�E");

                                        UpdateMainMessage("�A�C���F����A�����ƍl���Ă��񂾁B");

                                        UpdateMainMessage("���i�F�E�E�E�����H");

                                        UpdateMainMessage("�A�C���F���͂Ђ���Ƃ�����A���i�̎����ɂ��m��˂��̂����ȁB");

                                        UpdateMainMessage("���i�F������B���ł��m���Ă��炻�ꂱ���C�����������B");

                                        UpdateMainMessage("�A�C���F�b�n�b�n�b�n�I�m���ɂ���Ⴛ�����ȁB");

                                        UpdateMainMessage("�A�C���F���͐����ȏ��A�����ς茟�������˂����炳�B");

                                        UpdateMainMessage("���i�F����B");

                                        UpdateMainMessage("�A�C���F�S�K���e���I�܂��͂�����L�b�`������Ă��܂����Ǝv���I");

                                        UpdateMainMessage("���i�F����A����B");

                                        UpdateMainMessage("�A�C���F����ŁA�ǂ�ȓ��e���҂��󂯂Ă��悤�Ƃ��A�K���󂯎~�߂邺�B");

                                        UpdateMainMessage("���i�F�����ˁE�E�E���̕��������̃o�J�A�C�����ۂ��ėǂ��Ǝv����B");

                                        UpdateMainMessage("�A�C���F�����A�o�J�Ō��\�I�@�����A�s�����I�@�b�n�b�n�b�n�I�I");

                                        UpdateMainMessage("���F���[�F�A�C���N�E�E�E������{�N������b�������Ă��������B");

                                        UpdateMainMessage("�A�C���F������A���F���[�܂ŉB�������H");

                                        UpdateMainMessage("�A�C���F���Ⴀ�S�K���e���鎞�ɑS���ŃJ�~���O�A�E�g���ȁB�o�債�Ă�����B");

                                        UpdateMainMessage("���F���[�F�����A�����炱���S�̏��������Ă����Ă��������B");

                                        UpdateMainMessage("���i�F�I�����߂��Ȃ��Ă��Ă��B�s���܂��傤�B");
                                        we.InfoArea413 = true;
                                    }
                                    return;

                                case 23:
                                    if (!we.InfoArea414)
                                    {
                                        UpdateMainMessage("�A�C���F�������A���̃O���O����L�͉����ƕs���ɂ�����d�g�݂��ȁB");

                                        UpdateMainMessage("���i�F�}�b�v�������Ă���Ƃ͌����A���ƂȂ��s���ɂȂ��Ă��܂���ˁB");

                                        UpdateMainMessage("�A�C���F��{�����Ă̂����\����ǂ��Ƃ͎v��Ȃ����H��蓹�ł���ӏ�������˂�����ȁB");

                                        UpdateMainMessage("���F���[�F�A�C���N�ł������ꍇ�������ł��ˁB");

                                        UpdateMainMessage("�A�C���F�񂾂���B�܂�ŉ�������Ȃ��l�Ԃ݂����Ȍ��������ȁA���F���[�B");

                                        UpdateMainMessage("���F���[�F�b�n�n�n�B����͎��炵�܂����B");

                                        UpdateMainMessage("���i�F�A�C���͂��A�����������Ăǂ����Ă�킯�H");

                                        UpdateMainMessage("�A�C���F�����������H�������ȁB");

                                        UpdateMainMessage("�A�C���F�H�ׂāA�Q��I�@�b�n�b�n�b�n�I");

                                        UpdateMainMessage("���i�F�͂����������E�E�E���ł܂Ƃ��ȓ������Ԃ��Ă��Ȃ��̂�����B");

                                        UpdateMainMessage("�A�C���F���ƂȂ������ǂ�B�l���ėǂ��Ƃ��ƈ����ꍇ���Ă̂�����̂��B");

                                        UpdateMainMessage("���i�F�����H");

                                        UpdateMainMessage("�A�C���F�����A����̂S�K�݂����Ȃ͓̂��ɍl���邾���[�݂Ƀn�}�邾�����B");

                                        UpdateMainMessage("�A�C���F�����Ă��܂������A�b�͂��ꂩ�炾�B����˂��Ɛi�߂˂��B����H");

                                        UpdateMainMessage("���i�F������Ă���ς�g�Y��ł�h�Ƃ͏����Ⴄ��ˁE�E�E");

                                        UpdateMainMessage("�A�C���F�J�^�C�������Ȃ��āA�ق��s�����I");
                                        we.InfoArea414 = true;
                                    }
                                    return;

                                case 24:
                                    if (!we.InfoArea415)
                                    {
                                        UpdateMainMessage("���i�F�˂��E�E�E�A�C���B");

                                        UpdateMainMessage("�A�C���F��H�����H");

                                        UpdateMainMessage("���i�F�A���^�����A�t�����ĂȂ��H");

                                        UpdateMainMessage("�A�C���F��H��A�����B");

                                        UpdateMainMessage("���i�F�Ƃڂ��Ȃ��ŁA�����Ƃ��������Č����Ȃ�����A�z���B");

                                        UpdateMainMessage("���F���[�F���i����B������Ƃ�����ցB");

                                        UpdateMainMessage("���i�F�����H���A�����E�E�E");

                                        UpdateMainMessage("���F���[�F�A�C���N�̗l�q�A�������������Ǝv���܂��񂩁H");

                                        UpdateMainMessage("        �w�A�C���̑���肪���ڂ��Ȃ��B�x");

                                        UpdateMainMessage("���i�F���[��A�������Ƀo�e�ė��Ă銴���͂��邯�ǂˁB");

                                        UpdateMainMessage("���F���[�F�E�E�E�ǂ����Ă��ʖڂȂ�A���i���񂪉����̐������g���Ă��������B");

                                        UpdateMainMessage("���i�F�����A�킩������B");

                                        UpdateMainMessage("        �u�A�C���F��H�I���A���O��u���čs�����I�v");

                                        UpdateMainMessage("���i�F�����t�b���t���̂������āE�E�E�܂��������v�Ȃ̂�����B");
                                        we.InfoArea415 = true;
                                    }
                                    return;

                                case 25:
                                    if (!we.InfoArea416)
                                    {
                                        UpdateMainMessage("        �w�A�C���̑���肪�t���t�����Ă���B�x");

                                        UpdateMainMessage("���F���[�F�A�C���N�E�E�E���v�ł����H");

                                        UpdateMainMessage("�A�C���F���B�E�E�E����񂾁E�E�E���̏ꏊ�ցE�E�E");

                                        UpdateMainMessage("���i�F�����A�����̃o�J�B�����Ƃ����������Ȃ�����I");

                                        UpdateMainMessage("�A�C���F���i�E�E�E�����E�E�E");
                                        
                                        UpdateMainMessage("���i�F���H");

                                        UpdateMainMessage("�A�C���F�A��āE�E�E�@�E�E�E�@������@�E�E�E�@�����ȁ@�E�E�E");

                                        UpdateMainMessage("���F���[�F�����Ȃ��B�@���i����B");

                                        UpdateMainMessage("���i�F���A����B�A�C���A���߂���H");

                                        UpdateMainMessage("�A�C���F�E�E�E�@�E�E�E�@�ق�@�E�E�E�@���������@�E�E�E");

                                        UpdateMainMessage("���i�F�b�n�C�I�@�����̐����I");

                                        // [�x��]�F�Q�T�ڂ͖߂�E�߂�Ȃ��̑I�������o���Ă��������B

                                        we.InfoArea416 = true;
                                        CallHomeTown();
                                    }
                                    return;

                                case 26:
                                    if (we.InfoArea417 && !we.InfoArea418) // InfoArea417��HomeTown�ŗ����܂��B
                                    {
                                        UpdateMainMessage("���F���[�F�A�C���N");

                                        UpdateMainMessage("�A�C���F�����A���F���[�B�S�z���Ȃ��Ă��A���͂������v���B");

                                        UpdateMainMessage("���F���[�F�����A�����ł͂Ȃ��āB�����ɂ��B����������܂��B");

                                        we.InfoArea418 = true;
                                        ConstructDungeonMap();
                                        SetupDungeonMapping(4);

                                        UpdateMainMessage("�A�C���F�}�W����I�H���₠�A�����邺�I");

                                        UpdateMainMessage("���F���[�F���̃_���W�����\���A�{���Ɋ������܂��ˁB");

                                        UpdateMainMessage("���i�F�܂�Œ��ԃ|�C���g�܂Ői�񂾐l�̂��߂ɍ���Ă�����Ċ�����ˁB");

                                        UpdateMainMessage("�A�C���F�܂��܂��ǂ�����˂�����B�ςȍs���~�܂�⃏�[�v���͂邩�Ƀ}�V�����B");

                                        UpdateMainMessage("���F���[�F�����ł��ˁB�����A��֐i�݂܂��傤�B");

                                        we.ProgressArea4212 = true;
                                    }
                                    return;

                                case 27:
                                    if (!we.InfoArea419)
                                    {
                                        UpdateMainMessage("���F���[�F�A�C���N�A���i����A�O�����Ă��������B");

                                        UpdateMainMessage("���F���[�F������Ȃ��A����͂S�K�̎��҂ł��B");

                                        UpdateMainMessage("        �w�S�K�̎��ҁFAltomo�͓��X�Ƃ����p�ő҂��󂯂Ă���B�@�ُ�Ȏ����ƎE�C���������B�x");

                                        UpdateMainMessage("�A�C���F�����A���C���X����˂����B�E�C���r���r���Ɠ`����Ă��邺�B");

                                        UpdateMainMessage("���i�F����ȉ����Ȃ̂ɁA�������b���ɂ܂�Ă��銴����ˁB");

                                        UpdateMainMessage("�A�C���F���i�B���̌�A���낢��Ƌ����Ă����B���O���B���Ă鎖�B");

                                        UpdateMainMessage("���i�F����B�킩������B");

                                        UpdateMainMessage("�A�C���F���F���[�B���O�̘b���������Ă���B");

                                        UpdateMainMessage("���F���[�F�͂��B�����ł���B");

                                        UpdateMainMessage("�A�C���F�������閲�̘b�����邩��A�����Ă����ȁB");

                                        UpdateMainMessage("���i�F�����A�����ƕ�����B");

                                        UpdateMainMessage("�A�C���F���Ⴀ�E�E�E�s�����I");

                                        we.InfoArea419 = true;
                                    }
                                    return;

                                case 28:
                                    we.Treasure49 = GetTreasure("�\�[�h�E�I�u�E�u���[���[�W��");
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
                #region "�_���W�����T�K�C�x���g"
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
                                    mainMessage.Text = "�A�C���F�S�K�֖߂�K�i���ȁB�����͈�U�߂邩�H";
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
                                        UpdateMainMessage("�A�C���F����E�E�E�Ŕ��B�ǂނ��B");

                                        UpdateMainMessage("�@�@�@�@�w�^���̌��t�T�F�@�@���z���E�B�@�I���ɂ��Ďn�܂�B�x");

                                        we.TruthWord5 = true;

                                        if (we.SpecialInfo4)
                                        {
                                            UpdateMainMessage("�A�C���F���Ǝ�����E�E�E���z���E���B�I����Ă���E�E�E�n�܂�H");
                                        }
                                        else
                                        {
                                            UpdateMainMessage("�A�C���F������E�E�E���z���E���B�I����Ă���E�E�E�n�܂�H");
                                        }

                                        UpdateMainMessage("���i�F���̐�A�ǂ�Ȏ��������Ă�");

                                        UpdateMainMessage("�A�C���F��H");

                                        UpdateMainMessage("���i�F���̓A�C���ƈꏏ�ɋ��邩��B��΂ɁB");

                                        UpdateMainMessage("���F���[�F�E�E�E");

                                        UpdateMainMessage("�A�C���F�E�E�E�b�n�b�n�b�n�I");

                                        UpdateMainMessage("���i�F������A�������������̂�H");

                                        UpdateMainMessage("�A�C���F�E�E�E�b�n�b�n�b�n�b�n�b�n�b�n�b�n�I�I");

                                        UpdateMainMessage("���i�F���΂��Ă�̂�A���������ŉ��w�ɗ����񂾂�����������^�ʖڂɂ��Ȃ�����ˁB");

                                        UpdateMainMessage("�A�C���F���i�A���ӂ��Ă邺�B");

                                        UpdateMainMessage("���i�F���������C�̗��������t���炢�l���Ȃ�����B");

                                        UpdateMainMessage("�A�C���F�������H���Ⴀ�������Ȃ��E�E�E");

                                        UpdateMainMessage("�A�C���F����I������炳�B");

                                        UpdateMainMessage("�A�C���F�����P��t�@�[�W���{�a�s�������B");

                                        UpdateMainMessage("�A�C���F�G���~���A�t�@���l���ǂ����ǁA�`����FiveSeeker���F���[�ɂ����P��ē����Ă��炤�̂��B");

                                        UpdateMainMessage("�A�C���F��������H���F���[�B");

                                        UpdateMainMessage("���F���[�F�E�E�E�����A���ŁB���i����A�������ł����H");

                                        UpdateMainMessage("���i�F�����̃o�J�A�C���A�t�@�[�W���{�a�ɒ����Ĕт΂����肶��ʖڂ�����ˁB");

                                        UpdateMainMessage("�A�C���F���₢��A�т΂����肶��˂����B�����Ɗό�������B");

                                        UpdateMainMessage("���i�F�ό��C������Ȃ��āA�ē����������蕷�����B�܂������B");

                                        UpdateMainMessage("���i�F��̃A���^���т̌�ŐQ�Ă�����A�t�@���l���C���g���Ēu���Ă����񂶂�Ȃ��B");

                                        UpdateMainMessage("�A�C���F�}�W����H���̐Q�Ă�ԂɈē����Ă�����Ă��̂���I�H");

                                        UpdateMainMessage("���i�F���ꂷ��F�����ĂȂ������킯�ˁB�{���Ƀo�J�Ȃ񂾂���B");

                                        UpdateMainMessage("�A�C���F��A�����������������B������͐Q�Ȃ��悤�ɂ��邺�B");

                                        UpdateMainMessage("���i�F���ʂ͐Q�E�܁E���E��E����ˁB���R�̎��𓖑R�̂悤�ɂ���ė~������B");

                                        UpdateMainMessage("�A�C���F�����������āB������A�������ƍŉ��w�̃��X�{�X��|�����I");

                                        UpdateMainMessage("���i�F�҂��āA�A�C���B");

                                        UpdateMainMessage("�A�C���F������H");

                                        // �Q���ڂōő�̃q���g�ƂȂ锭���������Ă��������B
                                        UpdateMainMessage("���i�F�E�E�E������A���ł��Ȃ���B�b�T�T�A�s���܂����");

                                        UpdateMainMessage("�A�C���F�������������錾�������ȁA���ς�炸�B�b�n�n�n�A�����s�����I�I");
                                    }
                                    else
                                    {
                                        UpdateMainMessage("�@�@�@�@�w�^���̌��t�T�F�@�@���z���E�B�@�I���ɂ��Ďn�܂�B�x", true);
                                    }
                                    return;

                                case 2:
                                    if (!we.InfoArea52)
                                    {
                                        UpdateMainMessage("�A�C���F�E�E�E�������B�ǂ����A�C�c���Ō�̃��X�{�X���Ă킯���B");

                                        UpdateMainMessage("        �w�T�K�̎��ҁFBystander�͂��̏�ɂ���ʍ��ɐÂ��ɍ����Ă���B�x");
                                        
                                        UpdateMainMessage("        �w���̌���ɂ́A����Ȏ��ԂƊ���̏��������Ԃ�����B�x");

                                        // �Q���ڂ̓p�����^�ɂ���ăX�g�[���[�ω��������Ă��������B
                                        UpdateMainMessage("Bystander�F�A�C���̂k���A�@" + mc.Level.ToString());

                                        UpdateMainMessage("�A�C���F�I�H");

                                        UpdateMainMessage("Bystander�F�A�C���̗́A�@" + mc.Strength.ToString());

                                        UpdateMainMessage("Bystander�F�A�C���̋Z�A�@" + mc.Agility.ToString());

                                        UpdateMainMessage("Bystander�F�A�C���̒m�A�@" + mc.Intelligence.ToString());

                                        UpdateMainMessage("Bystander�F�A�C���̑́A�@" + mc.Stamina.ToString());

                                        UpdateMainMessage("Bystander�F�A�C���̐S�A�@" + mc.Mind.ToString());

                                        if (mc.MainWeapon != null)
                                        {
                                            UpdateMainMessage("Bystander�F�A�C���̑�������A�@" + mc.MainWeapon.Name.ToString());
                                        }
                                        if (mc.MainArmor != null)
                                        {
                                            UpdateMainMessage("Bystander�F�A�C���̑�������A�@" + mc.MainArmor.Name.ToString());
                                        }
                                        if (mc.Accessory != null)
                                        {
                                            UpdateMainMessage("Bystander�F�A�C���̑����A�N�Z�T���A�@" + mc.Accessory.Name.ToString());
                                        }

                                        UpdateMainMessage("�A�C���F���̃p�����^�Ƒ������E�E�E�S���I�H");

                                        UpdateMainMessage("Bystander�F�w�肢�x�́w���킸�x�@�@�w�����x�́w�y�΂��x�@�@�w���_�x�́w�������x");

                                        UpdateMainMessage("�A�C���F�Ȃ񂾂ƁH");

                                        UpdateMainMessage("Bystander�F�A�C����B�w���́x�A�w���z�x�Łw�肢�x���w��x���H");

                                        UpdateMainMessage("�A�C���F���H�ǂ������Ӗ����A����H");

                                        UpdateMainMessage("Bystander�F�A�C����B�w���F�x�́w���z�x�ɂ����w�݂炸�x");

                                        UpdateMainMessage("�A�C���F�������z�����Ă񂾂�H�����Ă݂��B");

                                        UpdateMainMessage("Bystander�F�w���x�ɑ΂���w�肢�x�A�w�~���鎖�x�ւ́w�肢�x�A�w�����x�ւ́w�肢�x");

                                        UpdateMainMessage("Bystander�F�w�����x�@�w�y�΂��x�@�A�@�w�����āx�@�w�y�΂��x�@�A�@�w�i���x�Ɂw�y�΂��x");

                                        UpdateMainMessage("�A�C���F�E�E�E�E�E�E�����E�E�E���������J�����������ȁB");

                                        UpdateMainMessage("Bystander�F�A�C����B�����悤�B�w�^���x�Ɓw���x");

                                        UpdateMainMessage("Bystander�F�����āA�w����x���w�v���x�������Ă���w�i���x�́w�肢�x��");

                                        UpdateMainMessage("        �wBystander���I��t���Ɠ����ɁA����̑S�Ă̎��Ԃ����������Ƌ��ɉ��n�߂��I�I�x");

                                        UpdateMainMessage("�A�C���F�E�E�E���́A���������Ԃ������o���₪���āE�E�E�㓙���I�I");

                                        UpdateMainMessage("�A�C���F�����܂Ō����Ȃ�A�����Ă��炨������˂����I�@�^����肢�Ƃ��A�S�ĂȁI�I");

                                        UpdateMainMessage("�A�C���F���i�A���F���[�A�s�����I�I");

                                        UpdateMainMessage("���i�F�����A���ł��ǂ����I");

                                        UpdateMainMessage("���F���[�F���ł��A�s���܂���B");

                                        UpdateMainMessage("�A�C���F��������I�@�s�����I�I�I");
                                        we.InfoArea52 = true;
                                    }
                                    else if (we.CompleteSlayBoss5)
                                    {
                                        UpdateMainMessage("�A�C���F����E�E�E�����߂�K�v�͂˂��B");
                                        UpdatePlayerLocationInfo(this.Player.Location.X - moveLen, this.Player.Location.Y);
                                    }
                                    return;

                                case 3:
                                    this.Update();
                                    UpdateMainMessage("�A�C���F�ŉ��w���X�{�X���B�������͐�΂Ƀ��c��|���I", true);
                                    bool result = EncountBattle("�܊K�̎��ҁFBystander");
                                    if (!result)
                                    {
                                        UpdatePlayerLocationInfo(this.Player.Location.X + moveLen, this.Player.Location.Y);
                                    }
                                    else
                                    {
                                        GroundOne.StopDungeonMusic();
                                        we.CompleteSlayBoss5 = true;
                                        UpdateMainMessage("Bystander�F�A�C����B�w�^���x�́w�����g�x�́w���^�x�Ɂw���݁x����B");

                                        UpdateMainMessage("Bystander�F�A�C����B�w�^���x�́w�[�w�x�ւ́w�̈�x�Ɂw���݁x����B");

                                        UpdateMainMessage("Bystander�F�A�C����B�w�肢�x�́w���݁x�Ɓw�n���x�ɂ���āw�`���x�����B");

                                        UpdateMainMessage("Bystander�F�A�C����B�w�^���x�ɑ΂���w�肢�x�́A�w���x�̌��ɂ���B");

                                        UpdateMainMessage("Bystander�F�A�C����B�󂯎�邪�ǂ��B�@�@������");

                                        UpdateMainMessage("Bystander�F�A�C����B�w�I���x�@�@���@�@�@�@�w���x�@�@�@�@�@�w�n�܂�x");
                                    }
                                    mainMessage.Text = "";
                                    return;

                                case 4:
                                    we.Treasure51 = GetTreasure("�I�[�o�[�V�t�e�B���O");
                                    return;

                                case 5:
                                    if (!we.TruthEventForLana)
                                    {
                                        GroundOne.PlayDungeonMusic(Database.BGM06, Database.BGM06LoopBegin);
                                        UpdateMainMessage("�A�C���F��E�E�E����́E�E�E");

                                        // GetTreasure("���i�̃C�������O"); //�����ŉו������ς��̎��A�ǂ��l���Ă�������h���܂���B�Q���ڊJ�n���_�Ŏ������܂��B

                                        UpdateMainMessage("�w���i�̃C�������O�x����ɓ���܂����B");

                                        UpdateMainMessage("�A�C���F���A����͂ǂ����ŁE�E�E");

                                        UpdateMainMessage("�A�C���F�I�I�I�I�I�I�I�I");

                                        UpdateMainMessage("�A�C���F���܁I�@�܂������I�I�I�I�I");

                                        UpdateMainMessage("�A�C���F���A���́E�E�E�������Ă��񂶂�˂��E�E�E");

                                        UpdateMainMessage("�A�C���F���ꂪ�E�E�E������˂��񂾂Ƃ�����E�E�E�E�E�E");

                                        this.BackColor = Color.Red;
                                        UpdateMainMessage("�@�@�@�@�w�A�C���̔]���ɑς����Ȃ����ɂ��������I�I�I�x");

                                        UpdateMainMessage("�A�C���F�b�O�E�E�E�O�A�A�A�@�@�A�@�A�A�@�@�@�I�I�I�I�I�I");

                                        UpdateMainMessage("�@�@�@�@�w�A�C���̔]���Ɍ��ɂƋ��ɁA������i�������яオ��x");

                                        // [�x��]�F�^�����E�̕`�ʂ͉��x�����x�����x�����x�����x�����Ȃ��Ă��������B
                                        UpdateMainMessage("�@�i�i�@�����܂��I�@�����냉�i�I�I�I�@�j�j");

                                        UpdateMainMessage("�@�i�i�@�E�E�E�����H�@�j�j");

                                        UpdateMainMessage("�@�i�i�@�b�O�T�I�I�I�@�j�j");

                                        UpdateMainMessage("�@�i�i�@�@�w�_���t�F���g�D�[�V�������i�̐S���Ɏh�������x�@�j�j");

                                        UpdateMainMessage("�@�i�i�@���i�I�I�I�@�������肵��I�I�I�@�j�j");

                                        UpdateMainMessage("�A�C���F�A�A�A�A�A�@�@�@�@�I�I�@�E�����b���b���E�E�E���A�A�@�@�@�@�I�I�I�I");

                                        UpdateMainMessage("�@�@�@�@�w�A�C���̔]���ɍX�Ȃ錃�ɂ�����I�I�I�@�����̌��i�������яオ��x");

                                        UpdateMainMessage("�@�i�i�@�A�C���E�E�E���߂�ˁB���߂�Ȃ����B�E�E�E���E�E�E���߂�Ȃ����E�E�E���@�j�j");

                                        UpdateMainMessage("�@�i�i�@��߂�A����ȁI�I�@�j�j");

                                        UpdateMainMessage("�@�i�i�@���̓A�C���ƈꏏ�ɋ��邩��B��΂ɁB�@�j�j");

                                        UpdateMainMessage("�@�i�i�@�@�w�b�u�V���A�u�V���E�E�E�D�D�I�I�I�x�i����ʂ��Ԃɐ��܂��Ă����I�j�@�@�j�j");

                                        UpdateMainMessage("�@�i�i�@��E�E�E���ˁB�t�@�E�E�E�t�@�[�W���E�E�E�{�a�E�E�E�@�j�j");

                                        UpdateMainMessage("�@�i�i�@���������B�����������āI�I�@�������Ă��B�@�t���b�V���q�[���I�I�@�j�j");

                                        UpdateMainMessage("�@�i�i�@�@�w�_���t�F���g�D�[�V���̏����́A�q�[�����O���ʂ�ł������Ă���B�x�@�j�j");

                                        UpdateMainMessage("�@�i�i�@�������A��������I�I�I�@�ǂ��Ȃ��Ă񂾁I�I�I�@�t���b�V���q�[���I�I�I�@�j�j");

                                        UpdateMainMessage("�@�i�i�@�@�w�_���t�F���g�D�[�V���̏����́A�q�[�����O���ʂ�ł����������Ă���B�x�@�j�j");

                                        UpdateMainMessage("�@�i�i�@�A�A�A�C���E�E�E�{�a�E�E�E�ŁE�E�E���A�����E�E�E��E�E�E�@�j�j");

                                        UpdateMainMessage("�@�i�i�@��ȋ��ǂ������ėǂ�����I�I�@�����A�����������Ă�邼�B�@�j�j");

                                        UpdateMainMessage("�@�i�i�@�Ȃ��ɁA���A���A�S�z����Ȃ��āA�b�n�b�n�b�n�I�I�I�@�j�j");

                                        UpdateMainMessage("�@�i�i�@���E�E�E�ƁE�E�E���F�E�E�E���F���E�E�E�[�E�E�E����E�E�E�@�j�j");

                                        UpdateMainMessage("�A�C���F��A��A�~�߂āA�~�߂Ă��ꂦ�������������I�I�I�I�@���A�@�@�@�A�A�@�@�@�@�I�I�I�I");

                                        UpdateMainMessage("�@�@�@�@�w�A�C���̔]���Ɏ��ɂ����������ɂ�����I�I�I�@�����̌��i�������яオ��x");

                                        UpdateMainMessage("�@�i�i�@���F�E�E�E���F���E�E�E�[�H�@���F���[���Ȃ񂾁B�@�j�j");

                                        UpdateMainMessage("�@�i�i�@���́E�E�E�E�\�E�E�E�E�E�E���F���E�E�E�[�E�E�E����E�E�E���E�E�E�@�j�j");

                                        UpdateMainMessage("�@�i�i�@�@�w���i�͈�΂��Ă���E�E�E�x�j�j");

                                        UpdateMainMessage("�@�i�i�@�E�E�E�A�C���A�D������B��D��������ˁB�@�j�j");

                                        UpdateMainMessage("�@�i�i�@���A���������A��k�͎~�߂�I�I�@����ȃW���[�N�͎����Ă��畷���Ă��I�@�ȁI�j�j");

                                        UpdateMainMessage("�@�i�i�@�@�w���i�̎�����C�������O����������B�x�j�j");

                                        UpdateMainMessage("�@�i�i�@�E�E�E���i�E�E�E���i�E�E�E���i�I�I�I�������肵��I�I�I�@�j�j");

                                        UpdateMainMessage("�@�i�i�@���i�I�I�I�@���i�@�@�@�@�I�I�I�I�@���A�A�A�A�A�@�@�@�b�@�@�@�@�@�A�I�I�I�@�j�j");

                                        UpdateMainMessage("�E�E�E�@�E�E�E�@�E�E�E�@�E�E�E�@�E�E�E");

                                        we.CompleteArea5 = true;
                                        we.TruthEventForLana = true;
                                        this.Hide();
                                        CallHomeTown(true);
                                    }
                                    return;

                                case 6:
                                    if (!we.InfoArea53)
                                    {
                                        UpdateMainMessage("�A�C���F�����I�I�I���蔲����ꂽ���I�H");

                                        UpdateMainMessage("���i�F�z���g�A���̑S�R������Ȃ�������ˁB�ǂ�����Č������̂�H�A�C��");

                                        UpdateMainMessage("�A�C���F����A���R�����ǂ̐F�������ɔ�����������ȁB");

                                        UpdateMainMessage("���F���[�F�����͂����炭�B�������ƌĂ΂��ꏊ�ł��ˁB");

                                        UpdateMainMessage("�A�C���F�����Ȃ̂��H");

                                        UpdateMainMessage("���F���[�F�͂��A�{�N���T���������������悤�ȃP�[�X���������܂�����B");

                                        UpdateMainMessage("���F���[�F�M�d�ȃA�C�e���A�m�b�A���Ȃǂ��B����Ă���ꍇ���قƂ�ǂł��B");

                                        UpdateMainMessage("�A�C���F��������I�y���݂��ȁI�����������܂ōs���Ă݂悤���I");

                                        we.InfoArea53 = true;
                                    }
                                    return;

                                case 7:
                                    we.Treasure52 = GetTreasure("�^�C���E�I�u�E���[�Z");
                                    return;

                                case 8:
                                    we.Treasure53 = GetTreasure("�����@�C���|�[�V����");
                                    return;
                                case 9:
                                    we.Treasure54 = GetTreasure("���W�F���h�E���b�h�z�[�X");
                                    return;
                                case 10:
                                    we.Treasure55 = GetTreasure("���i�E�G�O�[�L���[�W���i�[");
                                    return;
                                case 11:
                                    we.Treasure56 = GetTreasure("�����E�X��ւ̒�");
                                    return;
                                case 12:
                                    we.Treasure57 = GetTreasure("�t�@�[�W���E�W�E�G�X�y�����U");
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
                mainMessage.Text = "�A�C���F�������I���󂾂��I";
                ok.ShowDialog();
                ItemBackPack backpackData = new ItemBackPack(targetItemName);
                // [�x��]�F���v���O���~���O�ł��B�������Ă��������B
                bool result1 = mc.AddBackPack(backpackData);
                if (result1)
                {
                    mainMessage.Text = "�w" + backpackData.Name + "����ɓ���܂����x";
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
                            mainMessage.Text = "�w" + backpackData.Name + "����ɓ���܂����x";
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
                                    mainMessage.Text = "�w" + backpackData.Name + "����ɓ���܂����x";
                                    ok.ShowDialog();
                                    return true;
                                }
                                else
                                {
                                    mainMessage.Text = "�ו��������ς��ł��B" + backpackData.Name + "�����ł��܂���ł����B";
                                    ok.ShowDialog();
                                    return false;
                                }
                            }
                            else
                            {
                                mainMessage.Text = "�ו��������ς��ł��B" + backpackData.Name + "�����ł��܂���ł����B";
                                ok.ShowDialog();
                                return false;
                            }
                        }
                    }
                    else
                    {
                        mainMessage.Text = "�ו��������ς��ł��B" + backpackData.Name + "�����ł��܂���ł����B";
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
                #region "�_���W�����P�K�@�����ʒu"
                case 1:
                    // �^���̌��t�P
                    if (Player.Location == new Point(basePosX + moveLen * 9, basePosY + moveLen * 0) && eventNum == 0)
                    {
                        return true;
                    }

                    // �{�X�P�Ƃ̑ΐ�
                    if (Player.Location == new Point(basePosX + moveLen * 25, basePosY + moveLen * 12) && !we.CompleteSlayBoss1 && eventNum == 1)
                    {
                        return true;
                    }

                    // �󔠂P�|�P
                    if (Player.Location == new Point(basePosX + moveLen * 1, basePosY + moveLen * 19) && !we.Treasure1 && eventNum == 2)
                    {
                        return true;
                    }

                    // �󔠂P�|�Q
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 0) && !we.Treasure2 && eventNum == 3)
                    {
                        return true;
                    }

                    // �󔠂P�|�R
                    if (Player.Location == new Point(basePosX + moveLen * 18, basePosY + moveLen * 17) && !we.Treasure3 && eventNum == 4)
                    {
                        return true;
                    }

                    // ���ւ̋A��
                    if (Player.Location == new Point(basePosX + moveLen * 2, basePosY + moveLen * 1) && eventNum == 5)
                    {
                        return true;
                    }

                    // �Q�K�ւ̍~��K�i
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 19) && eventNum == 6)
                    {
                        return true;
                    }

                    // �B���ʘH������
                    if (Player.Location == new Point(basePosX + moveLen * 10, basePosY + moveLen * 11) && !we.InfoArea11 && eventNum == 7)
                    {
                        return true;
                    }

                    // �X�y�V�������P
                    if (Player.Location == new Point(basePosX + moveLen * 9, basePosY + moveLen * 16) && !we.SpecialInfo1 && eventNum == 8)
                    {
                        return true;
                    }

                    break;
                #endregion
                #region "�_���W�����Q�K�@�����ʒu"
                case 2:
                    // �P�K�ւ̏��K�i
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 19) && eventNum == 0)
                    {
                        return true;
                    }

                    // �^���̌��t�Q
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 0) && eventNum == 1)
                    {
                        return true;
                    }

                    // �R�K�ւ̉���K�i
                    if (Player.Location == new Point(basePosX + moveLen * 23, basePosY + moveLen * 0) && eventNum == 2)
                    {
                        return true;
                    }

                    // �{�X�Q�Ƃ̑ΐ�
                    if (Player.Location == new Point(basePosX + moveLen * 22, basePosY + moveLen * 1) && !we.CompleteSlayBoss2 && eventNum == 3)
                    {
                        return true;
                    }

                    // �󔠂Q�|�P
                    if (Player.Location == new Point(basePosX + moveLen * 1, basePosY + moveLen * 18) && !we.Treasure4 && eventNum == 4)
                    {
                        return true;
                    }

                    // �󔠂Q�|�Q
                    if (Player.Location == new Point(basePosX + moveLen * 3, basePosY + moveLen * 1) && !we.Treasure5 && eventNum == 5)
                    {
                        return true;
                    }

                    // �󔠂Q�|�R
                    if (Player.Location == new Point(basePosX + moveLen * 14, basePosY + moveLen * 0) && !we.Treasure6 && eventNum == 6)
                    {
                        return true;
                    }

                    // �󔠂Q�|�S
                    if (Player.Location == new Point(basePosX + moveLen * 2, basePosY + moveLen * 16) && !we.Treasure7 && eventNum == 7)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-1
                    if (Player.Location == new Point(basePosX + moveLen * 25, basePosY + moveLen * 14) && eventNum == 8)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-2-1
                    if (Player.Location == new Point(basePosX + moveLen * 16, basePosY + moveLen * 14) && eventNum == 9)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-2-2
                    if (Player.Location == new Point(basePosX + moveLen * 10, basePosY + moveLen * 12) && eventNum == 10)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-2-3
                    if (Player.Location == new Point(basePosX + moveLen * 10, basePosY + moveLen * 16) && eventNum == 11)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-3
                    if (Player.Location == new Point(basePosX + moveLen * 13, basePosY + moveLen * 9) && eventNum == 12)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-4
                    if (Player.Location == new Point(basePosX + moveLen * 7, basePosY + moveLen * 7) && eventNum == 13 && !we.SolveArea24) // [�x��]�F�C���M�����[�P�[�X�A�����̌��ɂȂ�ꍇ�͍Č������Ă��������B
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-4-1
                    if (Player.Location == new Point(basePosX + moveLen * 5, basePosY + moveLen * 5) && eventNum == 14)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-4-1-2
                    if (Player.Location == new Point(basePosX + moveLen * 7, basePosY + moveLen * 5) && eventNum == 15)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-4-1-3
                    if (Player.Location == new Point(basePosX + moveLen * 7, basePosY + moveLen * 7) && eventNum == 16)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-4-2
                    if (Player.Location == new Point(basePosX + moveLen * 7, basePosY + moveLen * 9) && eventNum == 17)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-4-2-2
                    if (Player.Location == new Point(basePosX + moveLen * 5, basePosY + moveLen * 9) && eventNum == 18)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-4-3
                    if (Player.Location == new Point(basePosX + moveLen * 5, basePosY + moveLen * 7) && eventNum == 19)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-4-3-2
                    if (Player.Location == new Point(basePosX + moveLen * 3, basePosY + moveLen * 7) && eventNum == 20)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-4-4
                    if (Player.Location == new Point(basePosX + moveLen * 3, basePosY + moveLen * 5) && eventNum == 21)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-4-4-2
                    if (Player.Location == new Point(basePosX + moveLen * 1, basePosY + moveLen * 5) && eventNum == 22)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-4-5
                    if (Player.Location == new Point(basePosX + moveLen * 1, basePosY + moveLen * 7) && eventNum == 23)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-4-5-2
                    if (Player.Location == new Point(basePosX + moveLen * 1, basePosY + moveLen * 9) && eventNum == 24)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-4-6
                    if (Player.Location == new Point(basePosX + moveLen * 3, basePosY + moveLen * 9) && eventNum == 25)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-4-0
                    if (Player.Location == new Point(basePosX + moveLen * 4, basePosY + moveLen * 9) && eventNum == 26)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-5
                    if (Player.Location == new Point(basePosX + moveLen * 4, basePosY + moveLen * 12) && eventNum == 27)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-6
                    if (Player.Location == new Point(basePosX + moveLen * 19, basePosY + moveLen * 7) && eventNum == 28)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-6-1
                    if (Player.Location == new Point(basePosX + moveLen * 22, basePosY + moveLen * 5) && eventNum == 29)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-6-2
                    if (Player.Location == new Point(basePosX + moveLen * 25, basePosY + moveLen * 7) && eventNum == 30)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-6-3
                    if (Player.Location == new Point(basePosX + moveLen * 22, basePosY + moveLen * 9) && eventNum == 31)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-6-4
                    if (Player.Location == new Point(basePosX + moveLen * 19, basePosY + moveLen * 5) && eventNum == 32)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-6-5
                    if (Player.Location == new Point(basePosX + moveLen * 25, basePosY + moveLen * 5) && eventNum == 33)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-6-6
                    if (Player.Location == new Point(basePosX + moveLen * 25, basePosY + moveLen * 9) && eventNum == 34)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-6-7
                    if (Player.Location == new Point(basePosX + moveLen * 19, basePosY + moveLen * 9) && eventNum == 35)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-6-8
                    if (Player.Location == new Point(basePosX + moveLen * 20, basePosY + moveLen * 6) && eventNum == 36)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-6-9
                    if (Player.Location == new Point(basePosX + moveLen * 24, basePosY + moveLen * 6) && eventNum == 37)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-6-10
                    if (Player.Location == new Point(basePosX + moveLen * 24, basePosY + moveLen * 8) && eventNum == 38)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-6-11
                    if (Player.Location == new Point(basePosX + moveLen * 20, basePosY + moveLen * 8) && eventNum == 39)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-6-12
                    if (Player.Location == new Point(basePosX + moveLen * 22, basePosY + moveLen * 6) && eventNum == 40)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-6-13
                    if (Player.Location == new Point(basePosX + moveLen * 23, basePosY + moveLen * 7) && eventNum == 41)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-6-14
                    if (Player.Location == new Point(basePosX + moveLen * 22, basePosY + moveLen * 8) && eventNum == 42)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-6-15
                    if (Player.Location == new Point(basePosX + moveLen * 21, basePosY + moveLen * 7) && eventNum == 43)
                    {
                        return true;
                    }

                    // �K�{�C�x���g2-6-16
                    if (Player.Location == new Point(basePosX + moveLen * 22, basePosY + moveLen * 7) && eventNum == 44)
                    {
                        return true;
                    }

                    // �B���ʘH������
                    if (Player.Location == new Point(basePosX + moveLen * 6, basePosY + moveLen * 17) && !we.InfoArea27 && eventNum == 45)
                    {
                        return true;
                    }

                    // �X�y�V�������Q
                    if (Player.Location == new Point(basePosX + moveLen * 27, basePosY + moveLen * 17) && !we.SpecialInfo2 && eventNum == 46)
                    {
                        return true;
                    }

                    break;
                #endregion
                #region "�_���W�����R�K�@�����ʒu"
                case 3:
                    // �Q�K�ւ̏��K�i
                    if (Player.Location == new Point(basePosX + moveLen * 23, basePosY + moveLen * 0) && eventNum == 0)
                    {
                        return true;
                    }

                    // �^���̌��t�R
                    if (Player.Location == new Point(basePosX + moveLen * 14, basePosY + moveLen * 11) && eventNum == 1)
                    {
                        return true;
                    }

                    // �S�K�ւ̉���K�i
                    if (Player.Location == new Point(basePosX + moveLen * 6, basePosY + moveLen * 19) && eventNum == 2)
                    {
                        return true;
                    }

                    // �{�X�R�Ƃ̑ΐ�
                    if (Player.Location == new Point(basePosX + moveLen * 3, basePosY + moveLen * 15) && !we.CompleteSlayBoss3 && eventNum == 3)
                    {
                        return true;
                    }

                    // �󔠂R�|�P
                    if (Player.Location == new Point(basePosX + moveLen * 2, basePosY + moveLen * 9) && !we.Treasure8 && eventNum == 4)
                    {
                        return true;
                    }

                    // �󔠂R�|�Q
                    if (Player.Location == new Point(basePosX + moveLen * 28, basePosY + moveLen * 9) && !we.Treasure9 && eventNum == 5)
                    {
                        return true;
                    }

                    // �󔠂R�|�R
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 14) && !we.Treasure10 && eventNum == 6)
                    {
                        return true;
                    }

                    // �󔠂R�|�S
                    if (Player.Location == new Point(basePosX + moveLen * 15, basePosY + moveLen * 13) && !we.Treasure11 && eventNum == 7)
                    {
                        return true;
                    }

                    // �󔠂R�|�T
                    if (Player.Location == new Point(basePosX + moveLen * 1, basePosY + moveLen * 3) && !we.Treasure12 && eventNum == 8)
                    {
                        return true;
                    }

                    // �Ŕ��P
                    if (Player.Location == new Point(basePosX + moveLen * 23, basePosY + moveLen * 1) && eventNum == 9)
                    {
                        return true;
                    }

                    // ���[�v�J�n�P�P
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 1) && eventNum == 10)
                    {
                        return true;
                    }

                    // ���[�v�I���P�P
                    if (Player.Location == new Point(basePosX + moveLen * 5, basePosY + moveLen * 13) && eventNum == 11)
                    {
                        return true;
                    }

                    // ���[�v�J�n�P�Q
                    if (Player.Location == new Point(basePosX + moveLen * 24, basePosY + moveLen * 4) && eventNum == 12)
                    {
                        return true;
                    }

                    // ���[�v�I���P�Q
                    if (Player.Location == new Point(basePosX + moveLen * 14, basePosY + moveLen * 15) && eventNum == 13)
                    {
                        return true;
                    }

                    // ���[�v�J�n�P�R
                    if (Player.Location == new Point(basePosX + moveLen * 21, basePosY + moveLen * 1) && eventNum == 14)
                    {
                        return true;
                    }

                    // ���[�v�I���P�R
                    if (Player.Location == new Point(basePosX + moveLen * 8, basePosY + moveLen * 1) && eventNum == 15)
                    {
                        return true;
                    }

                    // ���[�v�J�n�Q�S
                    if (Player.Location == new Point(basePosX + moveLen * 23, basePosY + moveLen * 14) && eventNum == 16)
                    {
                        return true;
                    }

                    // ���[�v�J�n�Q�T
                    if (Player.Location == new Point(basePosX + moveLen * 17, basePosY + moveLen * 8) && eventNum == 17)
                    {
                        return true;
                    }

                    // ���[�v�J�n�Q�U
                    if (Player.Location == new Point(basePosX + moveLen * 17, basePosY + moveLen * 4) && eventNum == 18)
                    {
                        return true;
                    }

                    // ���[�v�J�n�Q�V
                    if (Player.Location == new Point(basePosX + moveLen * 14, basePosY + moveLen * 4) && eventNum == 19)
                    {
                        return true;
                    }

                    // ���[�v�J�n�Q�W
                    if (Player.Location == new Point(basePosX + moveLen * 14, basePosY + moveLen * 8) && eventNum == 20)
                    {
                        return true;
                    }

                    // ���[�v�J�n�Q�X
                    if (Player.Location == new Point(basePosX + moveLen * 26, basePosY + moveLen * 8) && eventNum == 21)
                    {
                        return true;
                    }

                    // ���[�v�J�n�Q�P�O
                    if (Player.Location == new Point(basePosX + moveLen * 22, basePosY + moveLen * 10) && eventNum == 22)
                    {
                        return true;
                    }

                    // ���[�v�J�n�Q�P�P
                    if (Player.Location == new Point(basePosX + moveLen * 27, basePosY + moveLen * 15) && eventNum == 23)
                    {
                        return true;
                    }

                    // ���[�v�J�n�Q�P�Q
                    if (Player.Location == new Point(basePosX + moveLen * 8, basePosY + moveLen * 11) && eventNum == 24)
                    {
                        return true;
                    }

                    // ���[�v�J�n�Q�P�R
                    if (Player.Location == new Point(basePosX + moveLen * 9, basePosY + moveLen * 13) && eventNum == 25)
                    {
                        return true;
                    }

                    // ���[�v�J�n�Q�P�S
                    if (Player.Location == new Point(basePosX + moveLen * 7, basePosY + moveLen * 16) && eventNum == 26)
                    {
                        return true;
                    }

                    // ���[�v�I���Q�S
                    if (Player.Location == new Point(basePosX + moveLen * 12, basePosY + moveLen * 18) && eventNum == 27)
                    {
                        return true;
                    }

                    // ���[�v�I���i���s�j�P
                    if (Player.Location == new Point(basePosX + moveLen * 24, basePosY + moveLen * 18) && eventNum == 28)
                    {
                        return true;
                    }

                    // ���[�v�I���i���s�j�Q
                    if (Player.Location == new Point(basePosX + moveLen * 12, basePosY + moveLen * 9) && eventNum == 29)
                    {
                        return true;
                    }

                    // ���[�v�I���i���s�j�R
                    if (Player.Location == new Point(basePosX + moveLen * 7, basePosY + moveLen * 5) && eventNum == 30)
                    {
                        return true;
                    }

                    // ���[�v�I���i���s�j�S�P
                    if (Player.Location == new Point(basePosX + moveLen * 17, basePosY + moveLen * 2) && eventNum == 31)
                    {
                        return true;
                    }

                    // ���[�v�I���i���s�j�S�Q
                    if (Player.Location == new Point(basePosX + moveLen * 20, basePosY + moveLen * 10) && eventNum == 32)
                    {
                        return true;
                    }

                    // ���[�v�J�n�W�Q
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 19) && eventNum == 33)
                    {
                        return true;
                    }

                    // ���[�v�I���i���s�j�S�R
                    if (Player.Location == new Point(basePosX + moveLen * 8, basePosY + moveLen * 18) && eventNum == 34)
                    {
                        return true;
                    }

                    // ���֖�N���A�Ŕ�
                    if (Player.Location == new Point(basePosX + moveLen * 11, basePosY + moveLen * 11) && eventNum == 35)
                    {
                        return true;
                    }

                    // ���[�v�J�n�R�P�T
                    if (Player.Location == new Point(basePosX + moveLen * 6, basePosY + moveLen * 6) && eventNum == 36)
                    {
                        return true;
                    }

                    // ���[�v�J�n�R�P�U
                    if (Player.Location == new Point(basePosX + moveLen * 5, basePosY + moveLen * 5) && eventNum == 37)
                    {
                        return true;
                    }

                    // ���[�v�J�n�R�P�V
                    if (Player.Location == new Point(basePosX + moveLen * 5, basePosY + moveLen * 4) && eventNum == 38)
                    {
                        return true;
                    }

                    // ���[�v�J�n�R�P�W
                    if (Player.Location == new Point(basePosX + moveLen * 5, basePosY + moveLen * 3) && eventNum == 39)
                    {
                        return true;
                    }

                    // ���[�v�J�n�R�P�X
                    if (Player.Location == new Point(basePosX + moveLen * 5, basePosY + moveLen * 2) && eventNum == 40)
                    {
                        return true;
                    }

                    // ���[�v�J�n�R�Q�O
                    if (Player.Location == new Point(basePosX + moveLen * 19, basePosY + moveLen * 2) && eventNum == 41)
                    {
                        return true;
                    }

                    // ���[�v�J�n�R�Q�P
                    if (Player.Location == new Point(basePosX + moveLen * 20, basePosY + moveLen * 2) && eventNum == 42)
                    {
                        return true;
                    }

                    // ���[�v�J�n�R�Q�Q
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 12) && eventNum == 43)
                    {
                        return true;
                    }

                    // ���[�v�J�n�R�Q�R
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 13) && eventNum == 44)
                    {
                        return true;
                    }

                    // ���[�v�J�n�R�Q�S
                    if (Player.Location == new Point(basePosX + moveLen * 26, basePosY + moveLen * 4) && eventNum == 45)
                    {
                        return true;
                    }

                    // ���[�v�J�n�R�Q�T
                    if (Player.Location == new Point(basePosX + moveLen * 27, basePosY + moveLen * 4) && eventNum == 46)
                    {
                        return true;
                    }

                    // ���[�v�J�n�R�Q�U
                    if (Player.Location == new Point(basePosX + moveLen * 23, basePosY + moveLen * 2) && eventNum == 47)
                    {
                        return true;
                    }

                    // ���[�v�J�n�R�Q�V
                    if (Player.Location == new Point(basePosX + moveLen * 22, basePosY + moveLen * 6) && eventNum == 48)
                    {
                        return true;
                    }

                    // ���[�v�J�n�R�Q�W
                    if (Player.Location == new Point(basePosX + moveLen * 1, basePosY + moveLen * 10) && eventNum == 49)
                    {
                        return true;
                    }

                    // �Ŕ��S
                    if (Player.Location == new Point(basePosX + moveLen * 3, basePosY + moveLen * 6) && eventNum == 50)
                    {
                        return true;
                    }

                    // ���[�v�J�n�S�P
                    if (Player.Location == new Point(basePosX + moveLen * 1, basePosY + moveLen * 4) && eventNum == 51)
                    {
                        return true;
                    }

                    // ���[�v�I���S�P
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

                    // �󔠂R�|�U
                    if (Player.Location == new Point(basePosX + moveLen * 9, basePosY + moveLen * 18) && !we.Treasure121 && eventNum == 53)
                    {
                        return true;
                    }

                    // �󔠂R�|�V
                    if (Player.Location == new Point(basePosX + moveLen * 19, basePosY + moveLen * 10) && !we.Treasure122 && eventNum == 54)
                    {
                        return true;
                    }

                    // �󔠂R�|�W
                    if (Player.Location == new Point(basePosX + moveLen * 14, basePosY + moveLen * 2) && !we.Treasure123 && eventNum == 55)
                    {
                        return true;
                    }

                    // �B���ʘH������
                    if (Player.Location == new Point(basePosX + moveLen * 2, basePosY + moveLen * 14) && !we.InfoArea35 && eventNum == 56)
                    {
                        return true;
                    }

                    // �X�y�V�������R
                    if (Player.Location == new Point(basePosX + moveLen * 5, basePosY + moveLen * 19) && !we.SpecialInfo3 && eventNum == 57)
                    {
                        return true;
                    }

                    break;
                #endregion
                #region "�_���W�����S�K�@�����ʒu"
                case 4:
                    // �R�K�ւ̏��K�i
                    if (Player.Location == new Point(basePosX + moveLen * 6, basePosY + moveLen * 19) && eventNum == 0)
                    {
                        return true;
                    }

                    // �^���̌��t�S
                    if (Player.Location == new Point(basePosX + moveLen * 22, basePosY + moveLen * 0) && eventNum == 1)
                    {
                        return true;
                    }

                    // �T�K�ւ̉���K�i
                    if (Player.Location == new Point(basePosX + moveLen * 20, basePosY + moveLen * 0) && eventNum == 2)
                    {
                        return true;
                    }

                    // �{�X�S�Ƃ̑ΐ�
                    if (Player.Location == new Point(basePosX + moveLen * 26, basePosY + moveLen * 0) && !we.CompleteSlayBoss4 && eventNum == 3)
                    {
                        return true;
                    }

                    // ��b�P
                    if (Player.Location == new Point(basePosX + moveLen * 2, basePosY + moveLen * 3) && eventNum == 4)
                    {
                        return true;
                    }

                    // ��b�Q
                    if (Player.Location == new Point(basePosX + moveLen * 12, basePosY + moveLen * 16) && eventNum == 5)
                    {
                        return true;
                    }

                    // ��b�R
                    if (Player.Location == new Point(basePosX + moveLen * 4, basePosY + moveLen * 13) && eventNum == 6)
                    {
                        return true;
                    }

                    // ��b�S
                    if (Player.Location == new Point(basePosX + moveLen * 10, basePosY + moveLen * 13) && eventNum == 7)
                    {
                        return true;
                    }

                    // ��b�T
                    if (Player.Location == new Point(basePosX + moveLen * 11, basePosY + moveLen * 15) && eventNum == 8)
                    {
                        return true;
                    }

                    // ��b�U
                    if (Player.Location == new Point(basePosX + moveLen * 13, basePosY + moveLen * 19) && eventNum == 9)
                    {
                        return true;
                    }

                    // ��b�V
                    if (Player.Location == new Point(basePosX + moveLen * 16, basePosY + moveLen * 18) && !we.InfoArea48 && eventNum == 10)
                    {
                        return true;
                    }

                    // ��b�W
                    if (Player.Location == new Point(basePosX + moveLen * 16, basePosY + moveLen * 1) && eventNum == 11)
                    {
                        return true;
                    }

                    // ��b�X
                    if (Player.Location == new Point(basePosX + moveLen * 16, basePosY + moveLen * 0) && eventNum == 12)
                    {
                        return true;
                    }

                    // �󔠂S�|�P
                    if (Player.Location == new Point(basePosX + moveLen * 7, basePosY + moveLen * 10) && !we.Treasure41 && eventNum == 13)
                    {
                        return true;
                    }

                    // �󔠂S�|�Q
                    if (Player.Location == new Point(basePosX + moveLen * 1, basePosY + moveLen * 17) && !we.Treasure42 && eventNum == 14)
                    {
                        return true;
                    }

                    // �󔠂S�|�R
                    if (Player.Location == new Point(basePosX + moveLen * 9, basePosY + moveLen * 1) && !we.Treasure43 && eventNum == 15)
                    {
                        return true;
                    }

                    // �󔠂S�|�S
                    if (Player.Location == new Point(basePosX + moveLen * 20, basePosY + moveLen * 6) && !we.Treasure44 && eventNum == 16)
                    {
                        return true;
                    }

                    // �󔠂S�|�T
                    if (Player.Location == new Point(basePosX + moveLen * 21, basePosY + moveLen * 13) && !we.Treasure45 && eventNum == 17)
                    {
                        return true;
                    }

                    // �󔠂S�|�U
                    if (Player.Location == new Point(basePosX + moveLen * 26, basePosY + moveLen * 19) && !we.Treasure46 && eventNum == 18)
                    {
                        return true;
                    }

                    // �󔠂S�|�V
                    if (Player.Location == new Point(basePosX + moveLen * 26, basePosY + moveLen * 5) && !we.Treasure47 && eventNum == 19)
                    {
                        return true;
                    }

                    // �󔠂S�|�W
                    if (Player.Location == new Point(basePosX + moveLen * 26, basePosY + moveLen * 1) && !we.Treasure48 && eventNum == 20)
                    {
                        return true;
                    }

                    // �ߓ��P
                    if (Player.Location == new Point(basePosX + moveLen * 6, basePosY + moveLen * 18) && !we.InfoArea412 && eventNum == 21) 
                    {
                        return true;
                    }

                    // ��b�P�O
                    if (Player.Location == new Point(basePosX + moveLen * 18, basePosY + moveLen * 9) && eventNum == 22)
                    {
                        return true;
                    }

                    // ��b�P�P
                    if (Player.Location == new Point(basePosX + moveLen * 22, basePosY + moveLen * 14) && eventNum == 23)
                    {
                        return true;
                    }

                    // ��b�P�Q
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 19) && eventNum == 24)
                    {
                        return true;
                    }

                    // ��b�P�R
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 3) && eventNum == 25)
                    {
                        return true;
                    }

                    // �ߓ��Q
                    if (Player.Location == new Point(basePosX + moveLen * 16, basePosY + moveLen * 19) && !we.InfoArea418 && eventNum == 26)
                    {
                        return true;
                    }

                    // ��b�P�S
                    if (Player.Location == new Point(basePosX + moveLen * 27, basePosY + moveLen * 0) && eventNum == 27)
                    {
                        return true;
                    }

                    // �󔠂S�|�X
                    if (Player.Location == new Point(basePosX + moveLen * 27, basePosY + moveLen * 10) && !we.Treasure49 && eventNum == 28)
                    {
                        return true;
                    }

                    // �ߓ����s�I���̑O�n�_�P
                    if (Player.Location == new Point(basePosX + moveLen * 6, basePosY + moveLen * 18) && eventNum == 29)
                    {
                        return true;
                    }

                    // �ߓ��I�����Ȃ������n�_�P
                    if (Player.Location == new Point(basePosX + moveLen * 5, basePosY + moveLen * 18) && eventNum == 30)
                    {
                        return true;
                    }

                    // �ߓ����s�I����̔���n�_�P
                    if (Player.Location == new Point(basePosX + moveLen * 7, basePosY + moveLen * 18) && eventNum == 31)
                    {
                        return true;
                    }

                    // �ߓ����s�I���̑O�n�_�Q
                    if (Player.Location == new Point(basePosX + moveLen * 16, basePosY + moveLen * 19) && eventNum == 32)
                    {
                        return true;
                    }

                    // �ߓ��I�����Ȃ������n�_�Q
                    if (Player.Location == new Point(basePosX + moveLen * 16, basePosY + moveLen * 18) && eventNum == 33)
                    {
                        return true;
                    }

                    // �ߓ����s�I����̔���n�_�Q
                    if (Player.Location == new Point(basePosX + moveLen * 17, basePosY + moveLen * 19) && eventNum == 34)
                    {
                        return true;
                    }
                    break;
#endregion
                #region "�_���W�����T�K�@�����ʒu
                case 5:
                    // �S�K�ւ̏��K�i
                    if (Player.Location == new Point(basePosX + moveLen * 20, basePosY + moveLen * 0) && eventNum == 0)
                    {
                        return true;
                    }

                    // �^���̌��t�T
                    if (Player.Location == new Point(basePosX + moveLen * 20, basePosY + moveLen * 9) && eventNum == 1)
                    {
                        return true;
                    }

                    // �U�K�ւ̉���K�i
                    //if (Player.Location == new Point(basePosX + moveLen * 20, basePosY + moveLen * 0) && eventNum == 2)
                    //{
                    //    return true;
                    //}

                    // ��b�C�x���g�T�|�P
                    if ( (Player.Location == new Point(basePosX + moveLen * 5, basePosY + moveLen * 9) && eventNum == 2) ||
                         (Player.Location == new Point(basePosX + moveLen * 5, basePosY + moveLen * 10) && eventNum == 2) )
                    {
                        return true;
                    }

                    // �{�X�T�Ƃ̑ΐ�
                    if ( (Player.Location == new Point(basePosX + moveLen * 4, basePosY + moveLen * 9) && !we.CompleteSlayBoss5 && eventNum == 3) ||
                         (Player.Location == new Point(basePosX + moveLen * 4, basePosY + moveLen * 10) && !we.CompleteSlayBoss5 && eventNum == 3) )
                    {
                        return true;
                    }

                    // �󔠂T�|�P
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 10) && !we.Treasure51 && eventNum == 4)
                    {
                        return true;
                    }

                    // ���i�̃g�D���[�C�x���g���B�ւ̔j�ЂP
                    if (Player.Location == new Point(basePosX + moveLen * 0, basePosY + moveLen * 9) &&  eventNum == 5)
                    {
                        return true;
                    }

                    // �B���ʘH������
                    if (Player.Location == new Point(basePosX + moveLen * 19, basePosY + moveLen * 7) && eventNum == 6)
                    {
                        return true;
                    }

                    // �󔠂T�|�Q
                    if (Player.Location == new Point(basePosX + moveLen * 14, basePosY + moveLen * 5) && !we.Treasure52 && eventNum == 7)
                    {
                        return true;
                    }

                    // �󔠂T�|�R
                    if (Player.Location == new Point(basePosX + moveLen * 19, basePosY + moveLen * 9) && !we.Treasure53 && eventNum == 8)
                    {
                        return true;
                    }

                    // �󔠂T�|�S
                    if (Player.Location == new Point(basePosX + moveLen * 29, basePosY + moveLen * 9) && !we.Treasure54 && eventNum == 9)
                    {
                        return true;
                    }

                    // �󔠂T�|�T
                    if (Player.Location == new Point(basePosX + moveLen * 14, basePosY + moveLen * 12) && !we.Treasure55 && eventNum == 10)
                    {
                        return true;
                    }

                    // �󔠂T�|�U
                    if (Player.Location == new Point(basePosX + moveLen * 16, basePosY + moveLen * 14) && !we.Treasure56 && eventNum == 11)
                    {
                        return true;
                    }

                    // �󔠂T�|�V
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

        private bool CheckWall(int direction) // 0:�� 1:�� 2:�� 3:��
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

            // �v���C���[�̈ʒu�ɑΉ����Ă���^�C�������擾����B
            // �^�C�����ɂ���Ǐ����擾����
            // �Ǐ��ƃv���C���[��������ɑ΂��ĕǏ�񂪈�v����ꍇ
            switch (targetTileInfo[GetTileNumber(Player.Location)])
            {
                case "Tile1.bmp":
                    break;
                case "Tile1-WallT.bmp":
                    if (direction == 0)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallT-Num1.bmp":
                    if (direction == 0)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallT-Num4.bmp":
                    if (direction == 0)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallL.bmp":
                    if (direction == 1)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallL-Num5.bmp":
                    if (direction == 1)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallR.bmp":
                    if (direction == 2)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallB.bmp":
                    if (direction == 3)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallB-Num6.bmp":
                    if (direction == 3)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallLR-DummyL.bmp":
                    if (direction == 2)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallTL.bmp":
                    if (direction == 0 || direction == 1)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallTR.bmp":
                    if (direction == 0 || direction == 2)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallTB.bmp":
                    if (direction == 0 || direction == 3)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallLR.bmp":
                    if (direction == 1 || direction == 2)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallLB.bmp":
                    if (direction == 1 || direction == 3)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallRB.bmp":
                    if (direction == 2 || direction == 3)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallRB-Num2.bmp":
                    if (direction == 2 || direction == 3)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallTLR.bmp":
                    if (direction == 0 || direction == 1 || direction == 2)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallTLB.bmp":
                    if (direction == 0 || direction == 1 || direction == 3)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallTRB.bmp":
                    if (direction == 0 || direction == 2 || direction == 3)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallLRB.bmp":
                    if (direction == 1 || direction == 2 || direction == 3)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Tile1-WallTLRB.bmp":
                    if (direction == 0 || direction == 1 || direction == 2 || direction == 3)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Upstair-WallLRB.bmp":
                    if (direction == 1 || direction == 2 || direction == 3)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Upstair-WallRB.bmp":
                    if (direction == 2 || direction == 3)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Upstair-WallTLR.bmp":
                    if (direction == 0 || direction == 1 || direction == 2)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Downstair-WallTRB.bmp":
                    if (direction == 0 || direction == 2 || direction == 3)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Downstair-WallT.bmp":
                    if (direction == 0)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Downstair-WallLRB.bmp":
                    if (direction == 1 || direction == 2 || direction == 3)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
                        mainMessage.Update();
                        GroundOne.PlaySoundEffect("WallHit.mp3");
                        return true;
                    }
                    break;
                case "Downstair-WallTLB.bmp":
                    if (direction == 0 || direction == 1 || direction == 3)
                    {
                        mainMessage.Text = "�A�C���F���Ă��I";
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
                // �P�K�͍���F�G���A�P�A�����F�G���A�Q�A�E��F�G���A�R�A�E���F�G���A�S
                if (we.DungeonArea == 1)
                {
                    // ����
                    monsterName[0] = "�����O�E�S�u����";
                    monsterName[1] = "�����ꂽ����";
                    monsterName[2] = "�Ў�ȃr�[�g��";
                    monsterName[3] = "�c���G���t";
                    // ����
                    monsterName[4] = "�����Ԃꂽ�R�m";
                    monsterName[5] = "�����ȃC�m�V�V";
                    monsterName[6] = "�ɂފ��";
                    monsterName[7] = "�u���[�X���C��";
                    // �E��
                    monsterName[8] = "�E�F�A�E���t";
                    monsterName[9] = "�V���h�E�n���^�[";
                    monsterName[10] = "�r�q�ȑ�";
                    monsterName[11] = "�ڋ��ȃI�[�N";
                    // �E��
                    monsterName[12] = "�u���b�N�i�C�g";
                    monsterName[13] = "�z���C�g�i�C�g";
                    monsterName[14] = "�ԘT";
                    monsterName[15] = "�����Ȃ��̗ǂ��G���t";

                    areaBorderX = 15;
                    areaBorderY = 13;
                }
                // �Q�K�͉E���F�G���A�P�A�����F�G���A�Q�A����F�G���A�R�A�E��F�G���A�S�A
                else if (we.DungeonArea == 2)
                {
                    // ����
                    monsterName[0] = "�T�o���i�E���C�I��";
                    monsterName[1] = "�֖҂ȃn�Q�^�J";
                    monsterName[2] = "�S�u�����E�`�[�t";
                    monsterName[3] = "�r�ꋶ�����h���[�t";
                    // ����
                    monsterName[4] = "�I�[���h�c���[";
                    monsterName[5] = "�����ȃI�[�K";
                    monsterName[6] = "�G�����B�b�V���E�V���[�}��";
                    monsterName[7] = "�����������_��";
                    // �E��
                    monsterName[8] = "�ٌ`�̐M���";
                    monsterName[9] = "�}���C�[�^�[";
                    monsterName[10] = "���@���p�C�A";
                    monsterName[11] = "�Ԃ��t�[�h�����Ԃ����l��";
                    // �E��
                    monsterName[12] = "����m�o�[�T�[�J�[";
                    monsterName[13] = "��";
                    monsterName[14] = "���r�[�g��";
                    monsterName[15] = "���ӂ�������l��";

                    areaBorderX = 15;
                    areaBorderY = 10;
                }
                // �R�K�͉E��F�G���A�P�A�E���F�G���A�Q�A����F�G���A�R�A�����F�G���A�S�A
                else if (we.DungeonArea == 3)
                {
                    // ����
                    monsterName[0] = "�f�r�����[�W";
                    monsterName[1] = "�������q��";
                    monsterName[2] = "�A�v�����e�B�X�E���[�h";
                    monsterName[3] = "�G�����B�b�V���_��";
                    // ����
                    monsterName[4] = "���R�m";
                    monsterName[5] = "�t�H�[�����V�[�J�[";
                    monsterName[6] = "�A�C�I�u�U�h���S��";
                    monsterName[7] = "���܂ꂽ�Ă̈���";
                    // �E��
                    monsterName[8] = "�C�r�����[�W";
                    monsterName[9] = "�_�[�N�V�[�t";
                    monsterName[10] = "�A�[�N�h���C�h";
                    monsterName[11] = "�V���h�E�\�[�T���[";
                    // �E��
                    monsterName[12] = "�E��";
                    monsterName[13] = "�G�O�[�L���[�W���i�[";
                    monsterName[14] = "�p���[";
                    monsterName[15] = "�u���b�N�A�C";
                }
                // �S�K�͍����F�G���A�P�A����F�G���A�Q�A�E���F�G���A�R�A�E��F�G���A�S
                else if (we.DungeonArea == 4)
                {
                    // ����
                    monsterName[0] = "�u���[�^���I�[�K";
                    monsterName[1] = "�}�X�^�[���[�h";
                    monsterName[2] = "�V���E�U�E�_�[�N�G���t";
                    monsterName[3] = "�E�B���h�u���C�J�[";
                    // ����
                    monsterName[4] = "�S���S��";
                    monsterName[5] = "�r�[�X�g�}�X�^�[";
                    monsterName[6] = "�q���[�W�X�p�C�_�[";
                    monsterName[7] = "�G���_�[�A�T�V��";
                    // �E��
                    monsterName[8] = "�y�C���G���W�F��";
                    monsterName[9] = "�h�D�[���u�����K�[";
                    monsterName[10] = "�n�E�����O�z���[";
                    monsterName[11] = "�J�I�X�E���[�f��";
                    // �E��
                    monsterName[12] = "�A�[�N�f�[����";
                    monsterName[13] = "�T���E�X�g���C�_�[";
                    monsterName[14] = "�V�����i���";
                    monsterName[15] = "���C�W�E�C�t���[�g";

                    areaBorderX = 16;
                    areaBorderY = 2;
                }
                // �T�K�̓G���A���������A�ǂ��������Ƃ���B
                else if (we.DungeonArea == 5)
                {
                    // ����
                    monsterName[0] = "Phoenix";
                    monsterName[1] = "Emerard Dragon";
                    monsterName[2] = "Nine Tail";
                    monsterName[3] = "Judgement";
                    // ����
                    monsterName[4] = "Phoenix";
                    monsterName[5] = "Emerard Dragon";
                    monsterName[6] = "Nine Tail";
                    monsterName[7] = "Judgement";
                    // �E��
                    monsterName[8] = "Phoenix";
                    monsterName[9] = "Emerard Dragon";
                    monsterName[10] = "Nine Tail";
                    monsterName[11] = "Judgement";
                    // �E��
                    monsterName[12] = "Phoenix";
                    monsterName[13] = "Emerard Dragon";
                    monsterName[14] = "Nine Tail";
                    monsterName[15] = "Judgement";
                }

                if (Player.Location.X <= basePosX + moveLen * areaBorderX && Player.Location.Y <= basePosY + moveLen * areaBorderY) // ����
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
                else if (Player.Location.X <= basePosX + moveLen * areaBorderX && Player.Location.Y > basePosY + moveLen * areaBorderY) // ����
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
                else if (Player.Location.X > basePosX + moveLen * areaBorderX && Player.Location.Y <= basePosY + moveLen * areaBorderY) // �E��
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
                else if (Player.Location.X > basePosX + moveLen * areaBorderX && Player.Location.Y > basePosY + moveLen * areaBorderY) // �E��
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
                mainMessage.Text = "�A�C���F�G�Ƒ������I";
                mainMessage.Update();
                EncountBattle(enemyName);
                mainMessage.Text = "";
            }
        }

        // [�x��]�FHomeTown.cs�ɃR�s�y���܂����B���Ŏ���HomeTown.cs�����Y��Ȃ��B
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
                        // [�x��]�Fcatch�\����Set�v���p�e�B���Ȃ��ꍇ�����A����ȊO�̃P�[�X�������Ȃ��Ȃ��Ă��܂��̂ŗv���͕��@�����B
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
                        // [�x��]�Fcatch�\����Set�v���p�e�B���Ȃ��ꍇ�����A����ȊO�̃P�[�X�������Ȃ��Ȃ��Ă��܂��̂ŗv���͕��@�����B
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
                        // ���S���A�Ē��킷��ꍇ�A�͂��߂���ĂтȂ����B
                        this.Update();
                        continue;
                    }
                    if (be.DialogResult == DialogResult.Abort)
                    {
                        // ���������A�o���l�ƃS�[���h�͓���Ȃ��B
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
                            if (ec1.Name == "��K�̎��ҁF���݂��t�����V�X")
                            {
                                UpdatePlayerLocationInfo(this.Player.Location.X, this.Player.Location.Y - Database.DUNGEON_MOVE_LEN);
                            }
                            if (ec1.Name == "��K�̎��ҁFLizenos")
                            {
                                UpdatePlayerLocationInfo(this.Player.Location.X, this.Player.Location.Y + Database.DUNGEON_MOVE_LEN);
                            }
                            if (ec1.Name == "�O�K�̎��ҁFMinflore")
                            {
                                UpdatePlayerLocationInfo(this.Player.Location.X, this.Player.Location.Y - Database.DUNGEON_MOVE_LEN);
                            }
                            if (ec1.Name == "�l�K�̎��ҁFAltomo")
                            {
                                UpdatePlayerLocationInfo(this.Player.Location.X + Database.DUNGEON_MOVE_LEN, this.Player.Location.Y);
                            }
                            if (ec1.Name == "�܊K�̎��ҁFBystander")
                            {
                                UpdatePlayerLocationInfo(this.Player.Location.X + Database.DUNGEON_MOVE_LEN, this.Player.Location.Y);
                            }
                            yerw.StartPosition = FormStartPosition.CenterParent;
                            yerw.MainMessage = "�^�C�g���֖߂�܂��B���܂ł̃f�[�^���Z�[�u���܂����H";
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
                            if (mc.Level < 40) // [�x��]�F�O�҂͂k�u�S�O���l�`�w�Ƃ���B
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
                                    // [�x��]�F���x���A�b�v��MAX���C�t����ɂQ�O�A�}�i���P�T�ŗǂ����ǂ����������Ă��������B
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
                                mainMessage.Text = "�A�C���F���x���A�b�v�����I�I";
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
                            if (sc.Level < 40) // [�x��]�F�O�҂͂k�u�S�O���l�`�w�Ƃ���B
                            {
                                sc.Exp += be.EC1.Exp;
                            }
                            //SC.Gold += be.EC1.Gold; // [�x��]�F�S�[���h�̏����͕ʃN���X�ɂ���ׂ��ł��B

                            int levelUpPoint = 0;
                            int cumultiveLvUpValue = 0;
                            while (true)
                            {
                                if (sc.Exp >= sc.NextLevelBorder && sc.Level < 40)
                                {
                                    levelUpPoint += sc.LevelUpPoint;
                                    // [�x��]�F���x���A�b�v��MAX���C�t����ɂQ�O�A�}�i���P�T�ŗǂ����ǂ����������Ă��������B
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
                                mainMessage.Text = "���i�F������A���x���A�b�v��";
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
                            if (tc.Level < 40) // [�x��]�F�O�҂͂k�u�S�O���l�`�w�Ƃ���B
                            {
                                tc.Exp += be.EC1.Exp;
                            }
                            //TC.Gold += be.EC1.Gold; // [�x��]�F�S�[���h�̏����͕ʃN���X�ɂ���ׂ��ł��B

                            int levelUpPoint = 0;
                            int cumultiveLvUpValue = 0;
                            while (true)
                            {
                                if (tc.Exp >= tc.NextLevelBorder && tc.Level < 40)
                                {
                                    levelUpPoint += tc.LevelUpPoint;
                                    // [�x��]�F���x���A�b�v��MAX���C�t����ɂQ�O�A�}�i���P�T�ŗǂ����ǂ����������Ă��������B
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
                                mainMessage.Text = "���F���[�F���x���A�b�v�ł��ˁB";
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

                        if (!alreadyPlayBackMusic && ec1.Name != "�܊K�̎��ҁFBystander")
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
            bool newUpdate = false; // �V�����^�C�����񂯂����������t���O
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

            // ��̉���
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

            // ���̉���
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

            // �E�̉���
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

            // ���̉���
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
            this.dungeonAreaLabel.Text = we.DungeonArea.ToString() + "�@�K";
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
                    unknownTile[ii].Visible = !targetKnownTileInfo[ii]; // ���΂ł����Ӗ��t���͓����{���ł��B
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
            if (GroundOne.sound != null) // ��ҕҏW
            {
                GroundOne.sound.StopMusic(); // ��ҕҏW
                //this.sound.Disactive(); // ��ҍ폜
            }
        }
    }
}
